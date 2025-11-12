# SpaceWar (Internal Dev README)

Краткое техническое описание проекта, чтобы быстрее погрузиться в код.

## Общая архитектура
Игра построена вокруг глобальной state machine (`GameStateMachine`), которая пошагово переводит приложение через стадии запуска, меню и геймплея. Все состояния создаются через фабрику состояний (`IStateFactory`) в `GameBootstrapper` и регистрируются при старте.

DI: Zenject используется для внедрения зависимостей (конфиги, сервисы, модели). Reactive подход реализован через UniRx, асинхронность — UniTask.

Основные слои:
- Infrastructure: жизненный цикл, загрузка ассетов, сцены, сохранения, окна, ввод.
- Services: логические сервисы (уровни, пауза, прогресс).
- Logic: игровые модели и компоненты (игрок).
- UI: меню, HUD, окна окончания уровня.

## Сцены
Перечисление сцен — `GameScene`:
1. Bootstrap (index 0) — стартовая, содержит `GameBootstrapper`.
2. Menu (index 1) — выбор уровня, отображение прогресса.
3. Gameplay (index 2) — активный уровень и игровой цикл.

Переход между сценами инкапсулирован в состоянии загрузки (LoadMenuState, LoadGameplayState) через `SceneLoader`.

## Цикл состояний
Последовательность при старте:
1. `BootstrapState` — загрузка базового AssetBundle (ProjectAssetBundle).
2. `LoadGameSaveState` — чтение файла прогресса (`SaveLoadService.Load`).
3. `LoadMenuState` → загрузка меню, ассетов меню, вызов `InformProgressReader`.
4. `MenuLoopState` — ожидание выбора уровня. При выборе уровня (`LevelChanger.ChangeLevel`) дергается `CurrentState.Value.Next()` и переход к геймплею.
5. `LoadGameplayState` — загрузка ассетов геймплея + сцены Gameplay.
6. `StartLevelState` — создание уровня (`LevelFactory.CreateLevel`), оповещение `ProgressReader`.
7. `LevelLoopState` — основной игровой цикл. Включается ввод, подписка на здоровье игрока. При смерти игрока или завершении — переход.
8. `EndLevelState(bool isLose)` — показ Win/Lose окна, обновление прогресса (при победе) и сохранение.
9. Возврат в меню (`LoadMenuState`) после закрытия окна.

## Ключевые сущности
- `GameBootstrapper` — точка входа, регистрирует состояния и запускает `BootstrapState`.
- `GameStateMachine` / `StateMachine` — хранит словарь зарегистрированных состояний, обеспечивает Enter/Exit/Next.
- Состояния (`IState` / `IPayloadState<T>`): BootstrapState, LoadGameSaveState, LoadMenuState, MenuLoopState, LoadGameplayState, StartLevelState, LevelLoopState, EndLevelState.
- `SceneLoader` — асинхронная загрузка сцены по enum индексу.
- `AssetLoader` (упоминается) — загрузка / выгрузка AssetBundle по типу (MenuAssetBundle, GameplayAssetBundle и т.д.).
- `SaveLoadService` — централизованное чтение/запись прогресса, рассылка в зарегистрированные `ISaveHandler` (`IProgressReader/IProgressWrite`).
- Прогресс уровней: `LevelProgressService` хранит массив состояний уровней (`LevelState`: Locked/Unlocked/Completed) и вычисляет доступность.
- `LevelFactory` — создает игрового персонажа (позиционирует внизу экрана по конфигу `PlayerConfig`).
- `LevelChanger` — устанавливает выбранный индекс уровня и инициирует переход через `stateMachine.CurrentState.Value.Next()`.
- `ILevelSetupModel` / `LevelSetupModel` — хранит текущий индекс выбранного уровня.
- `PauseService` — управление `Time.timeScale` (используется в EndLevelState).
- `WindowService` — показывает окна (LoseWindow, WinWindow, LevelDetail) через конфиг `WindowsConfig` и `UIRoot`.
- `InputService` — обертка над Unity Input Actions: движение (Vector2), событие выстрела (`OnShootPressed`).
- Игрок: `PlayerModel` (ReactiveProperty<float> Health, MoveSpeed, Cooldown, BulletPrefab); `PlayerMovement` — читает `MoveInput`, ограничивает позицию рамками камеры и горизонтально «wrap’ит».

## Управление
- Движение: WASD / стрелки (по `Player.Move` action → Vector2).
- Стрельба: удержание/нажатие привязанного action (`Player.Shoot`) → событие `OnShootPressed` (подписчики создают пули, логика не в этом README).
- Окна: кнопки Win/Lose вызывают переход в меню или рестарт (`LoseWindowDisplay.Restart` → `Enter<LoadGameplayState>()`).

## Прогресс и сохранения
- Файл прогресса читается при `LoadGameSaveState`.
- При победе: `LevelProgressService.SetLevelState(levelIndex, Completed)` затем `SaveLoadService.Save()`.
- Рассылка состояний UI/сервисам через `InformProgressReader()` в `LoadMenuState` и `StartLevelState`.

## Расширение
Чтобы добавить новое состояние:
1. Реализовать `IState` или `IPayloadState<T>`.
2. Зарегистрировать в `GameBootstrapper` перед запуском.
3. В нужном месте вызвать `Enter<NewState>()` или `CurrentState.Value.Next()`.

Добавление новой сцены:
1. Добавить в Build Settings, обновить enum `GameScene` (индекс должен совпасть).
2. Создать состояние загрузки (по аналогии с `LoadGameplayState`).

## Запуск проекта
1. Убедиться, что все необходимые сцены (`Boot`, `Menu`, `Gameplay`) присутствуют в Build Settings.
2. Открыть сцену `Boot`. (P.S. благодаря `AutoChangeScene` можно запускать любую сцену напрямую в редакторе.)
3. Нажать Play: `GameBootstrapper` зарегистрирует состояния и инициирует цепочку до меню.
4. В меню выбрать уровень (UI элемент вызывает `LevelChanger.ChangeLevel`).

## Жизненный цикл уровня
StartLevelState → LevelLoopState: ввод включен, отслеживается `PlayerModel.Health`. Когда здоровье <= 0 (проигрыш) или игровая логика сигнализирует завершение, вызывается `Next()` → `EndLevelState` → окно → возврат в меню.

## Отладка
- Логи цветные (реализация через `ILogger` + extension `this.Log`).
- Текущее состояние можно получить из `IGameStateMachine.CurrentState` (ReactiveProperty).
- При необходимости принудительного перехода в редакторе можно вызвать `Enter<TState>()` через полученный инстанс из контейнера.

## Минимальные изменения для нового контента
- Добавить конфиг в AssetContainer (ScriptableObject) → загрузить через AssetLoader в нужном состоянии.
- Зарегистрировать новые `IProgressReader/IProgressWriter` прогресса в `SaveLoadService.Register`.
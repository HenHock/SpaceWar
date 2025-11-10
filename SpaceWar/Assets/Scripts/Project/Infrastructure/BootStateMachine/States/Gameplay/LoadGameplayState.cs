using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.Data;
using Project.Infrastructure.Services.AssetManagement;
using Project.Infrastructure.Services.AssetManagement.AssetBundles.GameplayBundles;
using Project.Infrastructure.Services.SaveSystem;
using Project.Infrastructure.Services.SceneLoader;
using Project.Services.LevelServices.Factory;

namespace Project.Infrastructure.BootStateMachine.States.Gameplay
{
    /// <summary>
    /// State to load gameplay scene
    /// </summary>
    public class LoadGameplayState : IPayloadState<int>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetLoader _assetLoader;
        private readonly ILevelFactory _levelFactory;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISaveLoadService _saveLoadService;

        private int _currentLevel;

        public LoadGameplayState
        (
            ISceneLoader sceneLoader,
            IAssetLoader assetLoader,
            ILevelFactory levelFactory,
            IGameStateMachine stateMachine,
            ISaveLoadService saveLoadService
        )
        {
            _assetLoader = assetLoader;
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
            _levelFactory = levelFactory;
        }

        public void Enter(int levelIndex)
        {
            _currentLevel = levelIndex;
            _assetLoader.LoadBundle<GameplayAssetBundle>();
            
            _sceneLoader.Load(GameScene.Gameplay, CreateLevel);
        }

        public void Next() => _stateMachine.Enter<GameplayLoopState>();

        private void CreateLevel()
        {
            _levelFactory.CreateLevel(_currentLevel);
            _saveLoadService.InformProgressReader();
            
            Next();
        }
    }
}
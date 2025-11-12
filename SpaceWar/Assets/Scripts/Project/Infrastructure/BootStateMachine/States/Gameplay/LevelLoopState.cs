using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.SceneBootstrapper;
using Project.Infrastructure.Services.AssetManagement;
using Project.Infrastructure.Services.AssetManagement.AssetBundles.GameplayBundles;
using Project.Infrastructure.Services.Input;
using Project.Logic.Player.Model;
using UniRx;

namespace Project.Infrastructure.BootStateMachine.States.Gameplay
{
    public class LevelLoopState : IState
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IPlayerModel _playerModel;
        private readonly IInputService _inputService;
        private readonly IGameStateMachine _stateMachine;
        
        public LevelLoopState
        (
            IPlayerModel playerModel,
            IAssetLoader assetLoader,
            IInputService inputService,
            IGameStateMachine stateMachine
        )
        {
            _assetLoader = assetLoader;
            _playerModel = playerModel;
            _stateMachine = stateMachine;
            _inputService = inputService;
        }

        public void Enter()
        {
            _playerModel.Health
                .Where(IsDead)
                .Subscribe(ToEndLevelState);

            _inputService.EnableInputs();
        }


        public void Next()
        {
            _stateMachine.Enter<EndLevelState, bool>(_playerModel.Health.Value <= 0);
        }

        public void Exit()
        {
            _inputService.DisableInputs();
            _assetLoader.UnloadBundle<GameplayAssetBundle>();
        }

        private bool IsDead(float health) => health <= 0;
        private void ToEndLevelState(float _) => Next();
    }
}
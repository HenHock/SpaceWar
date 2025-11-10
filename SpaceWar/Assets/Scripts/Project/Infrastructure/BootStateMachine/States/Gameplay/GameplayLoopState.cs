using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.Services.AssetManagement;
using Project.Infrastructure.Services.AssetManagement.AssetBundles.GameplayBundles;
using Project.Infrastructure.Services.Input;

namespace Project.Infrastructure.BootStateMachine.States.Gameplay
{
    public class GameplayLoopState : IState
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IInputService _inputService;
        private readonly IGameStateMachine _stateMachine;

        public GameplayLoopState(IGameStateMachine stateMachine, IInputService inputService, IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
            _stateMachine = stateMachine;
            _inputService = inputService;
        }

        public void Enter() => _inputService.EnableInputs();
        public void Exit() => _assetLoader.UnloadBundle<GameplayAssetBundle>();

        public void Next()
        {

        }
    }
}
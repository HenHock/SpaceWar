using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.Services.AssetManagement;
using Project.Infrastructure.Services.AssetManagement.AssetBundles.BootBundles;

namespace Project.Infrastructure.BootStateMachine.States
{
    /// <summary>
    /// State to initialization all game life services.
    /// </summary>
    public class BootstrapState : IState
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IGameStateMachine _stateMachine;

        public BootstrapState(IGameStateMachine stateMachine, IAssetLoader assetLoader)
        {
            _stateMachine = stateMachine;
            _assetLoader = assetLoader;
        }

        public void Enter()
        {
            _assetLoader.LoadBundle<ProjectAssetBundle>();
            Next();
        }

        public void Next() => _stateMachine.Enter<LoadGameSaveState>();
    }
}
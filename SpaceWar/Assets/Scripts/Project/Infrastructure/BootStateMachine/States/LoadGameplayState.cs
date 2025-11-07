using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.Configs;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Infrastructure.Services.SceneLoader;

namespace Project.Infrastructure.BootStateMachine.States
{
    /// <summary>
    /// State to load gameplay scene
    /// </summary>
    public class LoadGameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _stateMachine;
        private readonly IReadAssetContainer _assetContainer;

        public LoadGameplayState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IReadAssetContainer assetContainer)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
            _assetContainer = assetContainer;
        }

        public void Enter()
        {
            var gameConfig = _assetContainer.GetConfig<GameConfig>();
            _sceneLoader.Load(gameConfig.GameplayScene, Next);
        }

        public void Next() => _stateMachine.Enter<LoopGameplayState>();
    }
}
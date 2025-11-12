using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.Data;
using Project.Infrastructure.Services.AssetManagement;
using Project.Infrastructure.Services.AssetManagement.AssetBundles.GameplayBundles;
using Project.Infrastructure.Services.SceneLoader;

namespace Project.Infrastructure.BootStateMachine.States.Gameplay
{
    /// <summary>
    /// State to load gameplay scene
    /// </summary>
    public class LoadGameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetLoader _assetLoader;
        private readonly IGameStateMachine _stateMachine;

        public LoadGameplayState
        (
            ISceneLoader sceneLoader,
            IAssetLoader assetLoader,
            IGameStateMachine stateMachine
        )
        {
            _assetLoader = assetLoader;
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _assetLoader.LoadBundle<GameplayAssetBundle>();
            _sceneLoader.Load(GameScene.Gameplay, Next);
        }

        public void Next() => _stateMachine.Enter<StartLevelState>();
    }
}
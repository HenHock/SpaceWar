using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.Data;
using Project.Infrastructure.Services.AssetManagement;
using Project.Infrastructure.Services.AssetManagement.AssetBundles.MenuBundles;
using Project.Infrastructure.Services.SaveSystem;
using Project.Infrastructure.Services.SceneLoader;

namespace Project.Infrastructure.BootStateMachine.States.Menu
{
    public class LoadMenuState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetLoader _assetLoader;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISaveLoadService _saveLoadService;

        public LoadMenuState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IAssetLoader assetLoader, ISaveLoadService saveLoadService)
        {
            _sceneLoader = sceneLoader;
            _assetLoader = assetLoader;
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            _assetLoader.LoadBundle<MenuAssetBundle>();
            _sceneLoader.Load(GameScene.Menu, Next);
        }

        public void Next()
        {
            _saveLoadService.InformProgressReader();
            _stateMachine.Enter<MenuLoopState>();
        }
    }
}
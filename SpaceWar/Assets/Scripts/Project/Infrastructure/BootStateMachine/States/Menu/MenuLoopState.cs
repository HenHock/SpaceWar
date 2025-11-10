using Project.Infrastructure.BootStateMachine.States.Gameplay;
using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.Models;
using Project.Infrastructure.Services.AssetManagement;
using Project.Infrastructure.Services.AssetManagement.AssetBundles.MenuBundles;
using Project.Services.LevelServices.LevelChanger;

namespace Project.Infrastructure.BootStateMachine.States.Menu
{
    public class MenuLoopState : IState
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IGameplayModel _gameplayModel;
        private readonly IGameStateMachine _stateMachine;

        public MenuLoopState(IGameStateMachine stateMachine, IAssetLoader assetLoader, IGameplayModel gameplayModel)
        {
            _gameplayModel = gameplayModel;
            _assetLoader = assetLoader;
            _stateMachine = stateMachine;
        }

        public void Enter() {}

        public void Exit() => _assetLoader.UnloadBundle<MenuAssetBundle>();

        public void Next() => _stateMachine.Enter<LoadGameplayState, int>(_gameplayModel.SelectedLevelIndex);
    }
}
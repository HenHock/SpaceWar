using Zenject;
using Project.Infrastructure.BootStateMachine;
using Project.Infrastructure.BootStateMachine.StateFactory;
using Project.Infrastructure.BootStateMachine.States.Gameplay;
using Project.UI;
using UnityEngine;

namespace Project.Infrastructure.SceneBootstrapper
{
    public class GameplayBootstrapper : MonoInstaller
    {
        [SerializeField] private UIRoot uiRoot;
        
        private IStateFactory _factory;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine, IStateFactory factory)
        {
            _factory = factory;
            _gameStateMachine = gameStateMachine;
        }

        public override void InstallBindings()
        {
            BindUIRoot();
            
            _gameStateMachine.RegisterState(_factory.Create<StartLevelState>());
            _gameStateMachine.RegisterState(_factory.Create<LevelLoopState>());
            _gameStateMachine.RegisterState(_factory.Create<EndLevelState>());
        }

        private void OnDestroy()
        {
            _gameStateMachine.UnregisterState<StartLevelState>();
            _gameStateMachine.UnregisterState<EndLevelState>();
            _gameStateMachine.UnregisterState<LevelLoopState>();
        }

        private void BindUIRoot() => Container
            .BindInterfacesAndSelfTo<UIRoot>()
            .FromInstance(uiRoot)
            .AsSingle();
    }
}
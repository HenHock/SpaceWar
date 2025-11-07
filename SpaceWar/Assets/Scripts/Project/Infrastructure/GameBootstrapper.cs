using Project.Extensions;
using Project.Infrastructure.BootStateMachine;
using Project.Infrastructure.BootStateMachine.StateFactory;
using Project.Infrastructure.BootStateMachine.States;
using Zenject;
using UnityEngine;
using ILogger = Project.Infrastructure.Logger.ILogger;
using Project.Infrastructure.Services.SaveSystem;

namespace Project.Infrastructure
{
    /// <summary>
    /// Class to initialize the game state machine and start the game.
    /// </summary>
    public class GameBootstrapper : MonoBehaviour, ILogger
    {
        public Color DefaultColor => Color.yellow;

        private IStateFactory _stateFactory;
        private ISaveLoadService _saveLoadService;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IStateFactory stateFactory, IGameStateMachine stateMachine, ISaveLoadService saveLoadService)
        {
            _stateFactory = stateFactory;
            _gameStateMachine = stateMachine;
            _saveLoadService = saveLoadService;
        }

        private void Start()
        {
            DontDestroyOnLoad(this);

            _gameStateMachine.RegisterState(_stateFactory.Create<BootstrapState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadGameSaveState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadGameplayState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoopGameplayState>());

            this.Log("Initialized GameStateMachine");

            _gameStateMachine.Enter<BootstrapState>();
        }

#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            _saveLoadService.Save();    
        }
#endif
    }
}
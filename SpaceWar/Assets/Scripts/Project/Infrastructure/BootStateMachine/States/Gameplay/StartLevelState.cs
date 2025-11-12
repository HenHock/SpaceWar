using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.Services.SaveSystem;
using Project.Services.LevelServices.Factory;

namespace Project.Infrastructure.BootStateMachine.States.Gameplay
{
    public class StartLevelState : IState
    {
        private readonly ILevelFactory _levelFactory;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISaveLoadService _saveLoadService;

        public StartLevelState(IGameStateMachine stateMachine, ILevelFactory levelFactory, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _levelFactory = levelFactory;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            _levelFactory.CreateLevel();
            _saveLoadService.InformProgressReader();;
            
            Next();
        }

        public void Next() => _stateMachine.Enter<LevelLoopState>();
    }
}
using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.Services.Input;

namespace Project.Infrastructure.BootStateMachine.States
{
    public class LoopGameplayState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IInputService _inputService;

        public LoopGameplayState(IGameStateMachine stateMachine, IInputService inputService)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
        }

        public void Enter() => _inputService.EnableInputs();

        public void Next()
        {

        }
    }
}
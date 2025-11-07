namespace Project.Infrastructure.BootStateMachine.States.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
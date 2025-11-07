namespace Project.Infrastructure.BootStateMachine.States.Interfaces
{
    public interface IPayloadState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}
namespace Project.Infrastructure.BootStateMachine.States.Interfaces
{
    public interface IExitableState
    {
        /// <summary>
        /// By default isn't invoke for all states. To activate should be invoke RegisterUpdateMethod from IStateMachine class.
        /// </summary>
        void Update() {}
        void Exit() {}
        void Next();
    }
}
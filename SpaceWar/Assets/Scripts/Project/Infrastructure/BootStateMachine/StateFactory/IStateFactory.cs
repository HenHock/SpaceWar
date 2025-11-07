using Project.Infrastructure.BootStateMachine.States.Interfaces;

namespace Project.Infrastructure.BootStateMachine.StateFactory
{
    public interface IStateFactory
    {
        /// <summary>
        /// Create a state with binding it in DI container. 
        /// </summary>
        /// <typeparam name="TState">Type of state</typeparam>
        /// <returns>The instance of the state</returns>
        TState Create<TState>() where TState : IExitableState;
    }
}
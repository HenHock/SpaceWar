using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Zenject;

namespace Project.Infrastructure.BootStateMachine.StateFactory
{
    /// <summary>
    /// Factory to create states and bind they in DI container.
    /// </summary>
    public class StateFactory : IStateFactory
    {
        private readonly IInstantiator _container;

        public StateFactory(IInstantiator container) => 
            _container = container;

        public TState Create<TState>() where TState : IExitableState =>
            _container.Instantiate<TState>();
    }
}
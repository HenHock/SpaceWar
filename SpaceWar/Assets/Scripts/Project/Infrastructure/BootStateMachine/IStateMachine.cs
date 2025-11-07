using System.Threading;
using Project.Infrastructure.BootStateMachine.States.Interfaces;
using UniRx;

namespace Project.Infrastructure.BootStateMachine
{
    public interface IStateMachine
    {
        IReadOnlyReactiveProperty<IExitableState> CurrentState { get; }
        void RegisterUpdateMethod(CancellationToken lifeTimeToken);
        void RegisterState<TState>(TState state) where TState : IExitableState;
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload = default) where TState : class, IPayloadState<TPayload>;
    }
}
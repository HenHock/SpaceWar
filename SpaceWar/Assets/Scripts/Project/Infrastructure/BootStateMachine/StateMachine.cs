using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Project.Extensions;
using Project.Infrastructure.BootStateMachine.States;
using Project.Infrastructure.BootStateMachine.States.Interfaces;
using UniRx;
using UnityEngine;
using ILogger = Project.Infrastructure.Logger.ILogger;

namespace Project.Infrastructure.BootStateMachine
{
    public abstract class StateMachine : IStateMachine, ILogger
    {
        public virtual bool IsActiveLogger => true;
        public virtual Color DefaultColor => Color.yellow;
        
        public IReadOnlyReactiveProperty<IExitableState> CurrentState => _currentState;

        private readonly Dictionary<Type, IExitableState> _registeredState = new();
        private readonly ReactiveProperty<IExitableState> _currentState = new();

        public void RegisterUpdateMethod(CancellationToken lifeTimeToken) => 
            Observable.EveryUpdate()
                .Subscribe(_ => CurrentState.Value.Update())
                .AddTo(lifeTimeToken);

        public void RegisterState<TState>(TState state) where TState : IExitableState
        {
            var key = typeof(TState);
            _registeredState.Add(key, state);
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState newState = ChangeState<TState>();
            
            this.Log($"Entered in {newState?.GetType().Name} state");
            newState?.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload = default) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            
            this.Log($"Entered in {state?.GetType().Name} state");

            state?.Enter(payload);
        }

        [CanBeNull]
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState.Value?.Exit();
            
            if (CurrentState.Value != null) 
                this.Log($"Exited from {CurrentState.Value.GetType().Name} state");

            var state = GetState<TState>();
            _currentState.Value = state;

            return state;
        }

        [CanBeNull]
        private TState GetState<TState>() where TState : class, IExitableState =>
            _registeredState[typeof(TState)] as TState;    
    }
}
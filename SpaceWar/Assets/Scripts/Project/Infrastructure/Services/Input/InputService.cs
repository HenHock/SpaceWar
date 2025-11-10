using UniRx;
using UnityEngine;

namespace Project.Infrastructure.Services.Input
{
    public sealed class InputService : IInputService
    {
        public Subject<Unit> OnShootPressed { get; } = new();
        public Vector2 MoveInput => _gameInputAction.Player.Move.ReadValue<Vector2>(); 
        
        private readonly GameInputAction _gameInputAction;

        public InputService()
        {
            _gameInputAction = new GameInputAction();

            Observable.EveryUpdate()
                .Where(IsShootPressed)
                .Subscribe(HandleShootPressed);
            
            EnableInputs();
        }

        public void EnableInputs()
        {
            _gameInputAction.Enable();
        }

        public void DisableInputs()
        {
            _gameInputAction.Disable();
        }

        private void HandleShootPressed(long _)
        {
            OnShootPressed.OnNext(Unit.Default);
        }

        private bool IsShootPressed(long arg) => _gameInputAction.Player.Shoot.IsPressed();
    }
}
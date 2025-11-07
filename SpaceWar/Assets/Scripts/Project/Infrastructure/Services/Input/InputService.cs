using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Infrastructure.Services.Input
{
    public sealed class InputService : IInputService
    {
        public ReactiveCommand OnShootPressed { get; } = new();
        public Vector2 MoveInput => _gameInputAction.Player.Move.ReadValue<Vector2>(); 
        
        private readonly GameInputAction _gameInputAction;

        public InputService()
        {
            _gameInputAction = new GameInputAction();
            _gameInputAction.Player.Shoot.performed += HandleShootPressed;
            
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

        private void HandleShootPressed(InputAction.CallbackContext context)
        {
            OnShootPressed.Execute();
        }
    }
}
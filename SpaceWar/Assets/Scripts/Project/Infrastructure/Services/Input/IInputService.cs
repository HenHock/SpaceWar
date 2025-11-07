using UniRx;
using UnityEngine;

namespace Project.Infrastructure.Services.Input
{
    public interface IInputService
    {
        /// <summary>
        /// Retrieves the current movement input as a Vector2 from the Game Input Actions asset.
        /// Represents horizontal (x) and vertical (y) movement, typically controlled by WASD or arrow keys.
        /// </summary>
        Vector2 MoveInput { get; }

        ReactiveCommand OnShootPressed { get; }

        void EnableInputs();
        void DisableInputs();
    }
}
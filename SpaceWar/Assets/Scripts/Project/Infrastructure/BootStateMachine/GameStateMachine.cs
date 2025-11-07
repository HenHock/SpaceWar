using UnityEngine;

namespace Project.Infrastructure.BootStateMachine
{
    /// <summary>
    /// Global state machine that switch states of the game.
    /// Binding in the project context to be available globally.
    /// GameBootstrapper class full this state machine by states. 
    /// </summary>
    public class GameStateMachine : StateMachine, IGameStateMachine
    {
        public override bool IsActiveLogger => true;
        public override Color DefaultColor => Color.yellow;
    }
}
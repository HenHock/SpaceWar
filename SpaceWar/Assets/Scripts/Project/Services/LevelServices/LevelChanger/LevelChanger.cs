using Project.Infrastructure.BootStateMachine;
using Project.Infrastructure.Models;

namespace Project.Services.LevelServices.LevelChanger
{
    public class LevelChanger : ILevelChanger
    {
        private readonly GameplayModel _model;
        private readonly IGameStateMachine _stateMachine;

        public LevelChanger(IGameStateMachine stateMachine, GameplayModel model)
        {
            _model = model;
            _stateMachine = stateMachine;
        }
        
        public void ChangeLevel(int selectedLevelIndex)
        {
            _model.SelectedLevelIndex = selectedLevelIndex;
            
            _stateMachine.CurrentState.Value.Next();
        }
    }
}
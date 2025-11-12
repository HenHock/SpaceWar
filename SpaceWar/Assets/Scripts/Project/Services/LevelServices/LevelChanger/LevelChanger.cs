using Project.Infrastructure.BootStateMachine;
using Project.Services.LevelServices.LevelChanger.Model;

namespace Project.Services.LevelServices.LevelChanger
{
    public class LevelChanger : ILevelChanger
    {
        private readonly LevelSetupModel _setupModel;
        private readonly IGameStateMachine _stateMachine;

        public LevelChanger(IGameStateMachine stateMachine, LevelSetupModel setupModel)
        {
            _setupModel = setupModel;
            _stateMachine = stateMachine;
        }
        
        public void ChangeLevel(int selectedLevelIndex)
        {
            _setupModel.LevelIndex = selectedLevelIndex;
            _stateMachine.CurrentState.Value.Next();
        }
    }
}
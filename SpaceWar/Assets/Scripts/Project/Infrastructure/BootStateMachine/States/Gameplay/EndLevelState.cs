using Project.Infrastructure.BootStateMachine.States.Interfaces;
using Project.Infrastructure.BootStateMachine.States.Menu;
using Project.Infrastructure.Services.SaveSystem;
using Project.Infrastructure.Services.Windows;
using Project.Infrastructure.Services.Windows.Data;
using Project.Services.LevelServices.LevelChanger.Model;
using Project.Services.LevelServices.LevelProgression;
using Project.Services.LevelServices.LevelProgression.Data;
using Project.Services.Pause;

namespace Project.Infrastructure.BootStateMachine.States.Gameplay
{
    public class EndLevelState : IPayloadState<bool>
    {
        private readonly IPauseService _pauseService;
        private readonly ILevelSetupModel _levelModel;
        private readonly IWindowService _windowService;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ILevelProgressService _levelProgressService;

        public EndLevelState
        (
            IPauseService pauseService,
            ILevelSetupModel levelModel,
            IWindowService windowService,
            IGameStateMachine stateMachine,
            ISaveLoadService saveLoadService,
            ILevelProgressService levelProgressService
        )
        {
            _pauseService = pauseService;
            _levelModel = levelModel;
            _windowService = windowService;
            _stateMachine = stateMachine;
            _saveLoadService = saveLoadService;
            _levelProgressService = levelProgressService;
        }

        public void Enter(bool isLose)
        {
            _pauseService.SetPause(true);
            
            if (isLose)
            {
                _windowService.Open(WindowType.LoseWindow);
            }
            else
            {
                _windowService.Open(WindowType.WinWindow);
                _levelProgressService.SetLevelState(_levelModel.LevelIndex, LevelState.Completed);
                _saveLoadService.Save();
            }
        }

        public void Next()
        {
            _pauseService.SetPause(false);
            _stateMachine.Enter<LoadMenuState>();
        }
    }
}
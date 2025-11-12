using Project.Services.LevelServices.LevelProgression;
using Zenject;
using UnityEngine;

namespace Project.Services.LevelServices.LevelChanger.View
{
    public class ChangeLevelButton : MonoBehaviour
    {
        private ILevelChanger _levelChanger;
        private ILevelProgressService _progressService;

        [Inject]
        private void Construct(ILevelChanger levelChanger, ILevelProgressService progressService)
        {
            _levelChanger = levelChanger;
            _progressService = progressService;
        }
        
        public void ChangeLevel()
        {
            _levelChanger.ChangeLevel(_progressService.LastCompletedLevelIndex + 1);
        }
    }
}
using Zenject;
using UnityEngine;

namespace Project.Services.LevelServices.LevelChanger.View
{
    public class ChangeLevelButton : MonoBehaviour
    {
        [SerializeField] private int levelIndex;
        
        private ILevelChanger _levelChanger;

        [Inject]
        private void Construct(ILevelChanger levelChanger)
        {
            _levelChanger = levelChanger;
        }
        
        public void ChangeLevel()
        {
            _levelChanger.ChangeLevel(levelIndex);
        }
    }
}
using Zenject;
using UnityEngine;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Services.LevelServices.LevelProgression;
using Project.Services.LevelServices.LevelProgression.Data;
using Project.Services.LevelServices.LevelSettingsProvider;
using Project.Services.LevelServices.LevelSettingsProvider.Data;

namespace Project.UI.Menu
{
    public class LevelElementsFactory : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private LevelElementDisplay levelElement;
        
        private IReadAssetContainer _assetContainer;
        private ILevelProgressService _progressService;

        [Inject]
        private void Construct(IReadAssetContainer assetContainer, ILevelProgressService progressService)
        {
            _assetContainer = assetContainer;
            _progressService = progressService;
        }

        private void Start()
        {
            var levelsConfig = _assetContainer.GetConfig<LevelsConfig>();

            for (int level = levelsConfig.TotalLevels; level > 0; level--)
            {
                LevelConfig levelConfig = levelsConfig.GetLevelConfig(level - 1);
                LevelState levelState = _progressService.GetLevelState(level - 1);
                
                LevelElementDisplay levelDisplay = Instantiate(levelElement, container);
                levelDisplay.Initialize(level, levelState, levelConfig.IsGeneratable);
            }
        }
    }
}
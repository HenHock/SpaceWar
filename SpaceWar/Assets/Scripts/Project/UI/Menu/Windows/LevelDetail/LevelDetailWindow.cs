using Project.Infrastructure.Services.Windows.View;
using Project.Services.LevelServices.LevelProgression;
using Project.Services.LevelServices.LevelSettingsProvider;
using Project.Services.LevelServices.LevelSettingsProvider.Data;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.UI.Menu.Windows.LevelDetail
{
    public class LevelDetailWindow : BaseWindow
    {
        private const string LevelDetailTitleFormat = "Level {0}";
        private const string AsteroidCountTextFormat = "Asteroid Count: {0}";
        private const string SpawnIntervalTextFormat = "Spawn Interval: {0:0.0} sec";
        
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private TextMeshProUGUI countTMP;
        [SerializeField] private TextMeshProUGUI spawnIntervalTMP;
        
        private ILevelProgressService _progressService;
        private ILevelSettingsProvider _levelSettingsProvider;

        [Inject]
        private void Construct(ILevelSettingsProvider levelSettingsProvider, ILevelProgressService progressService)
        {
            _progressService = progressService;
            _levelSettingsProvider = levelSettingsProvider;
        }
        
        public override void Open()
        {
            int selectedLevel = _progressService.LastCompletedLevelIndex + 1;
            LevelSettings levelSettings = _levelSettingsProvider.GetLevelSettings(selectedLevel);
            
            titleTMP.SetText(LevelDetailTitleFormat, selectedLevel + 1);
            countTMP.SetText(AsteroidCountTextFormat, levelSettings.AsteroidSpawnCount);
            spawnIntervalTMP.SetText(SpawnIntervalTextFormat, levelSettings.AsteroidSpawnInterval);
        }
    }
}
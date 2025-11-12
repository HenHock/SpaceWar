using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Services.LevelServices.LevelProgression;
using Project.Services.LevelServices.LevelSettingsProvider.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI.Menu.Windows.LevelDetail
{
    [RequireComponent(typeof(Button))]
    public class OpenLevelDetailButtonView : MonoBehaviour
    {
        private int _totalLevels;
        private ILevelProgressService _progressService;

        [Inject]
        private void Construct(ILevelProgressService progressService, IReadAssetContainer assetContainer)
        {
            _progressService = progressService;
            _totalLevels = assetContainer.GetConfig<LevelsConfig>().TotalLevels;
        }

        private void Start()
        {
            GetComponent<Button>().enabled = _progressService.LastCompletedLevelIndex < _totalLevels - 1;
        }
    }
}
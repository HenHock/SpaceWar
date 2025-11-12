using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Infrastructure.Services.SaveSystem.Data;
using Project.Infrastructure.Services.SaveSystem.SaveHandler;
using Project.Services.LevelServices.LevelSettingsProvider.Data;

namespace Project.Services.LevelServices.LevelSettingsProvider
{
    public class LevelSettingsProvider : SaveHandler, ILevelSettingsProvider
    {
        private readonly IReadAssetContainer _assetContainer;
        
        private Dictionary<int, LevelSettings> _cachedLevelSettings;

        public LevelSettingsProvider(IReadAssetContainer assetContainer)
        {
            _assetContainer = assetContainer;
        }

        public LevelSettings GetLevelSettings(int levelIndex)
        {
            if (TryGetCachedLevelSettings(levelIndex, out LevelSettings cachedLevelSettings))
                return cachedLevelSettings;

            return CreateLevelSettings(levelIndex);
        }
        
        public override void LoadProgress(GameProgress progress)
        {
            _cachedLevelSettings = progress.LevelSettings;
            _cachedLevelSettings ??= new Dictionary<int, LevelSettings>();
        }

        public override void UpdateProgress(GameProgress progress)
        {
            progress.LevelSettings = new SerializedDictionary<int, LevelSettings>(_cachedLevelSettings);
        }

        private bool TryGetCachedLevelSettings(int levelIndex, out LevelSettings levelSettings)
        {
            levelSettings = default;
            
            var levelConfig = _assetContainer
                .GetConfig<LevelsConfig>()
                .GetLevelConfig(levelIndex);
            
            if (_cachedLevelSettings != null && _cachedLevelSettings.TryGetValue(levelIndex, out levelSettings))
            {
                if (!levelConfig.IsGeneratable)
                {
                    _cachedLevelSettings.Remove(levelIndex);
                    levelSettings = CreateLevelSettings(levelIndex); 
                }
                
                if (!levelSettings.IsEqual(levelConfig))
                {
                    levelSettings = CreateLevelSettings(levelIndex);
                    _cachedLevelSettings[levelIndex] = levelSettings;
                }
                
                return true;
            }

            return false;
        }
        
        private LevelSettings CreateLevelSettings(int levelIndex)
        {
            var config = _assetContainer
                .GetConfig<LevelsConfig>()
                .GetLevelConfig(levelIndex);

            var levelSettings = new LevelSettings(config);

            if (config.IsGeneratable)
            {
                _cachedLevelSettings[levelIndex] = levelSettings;
            }
            
            return levelSettings;
        }
    }
}
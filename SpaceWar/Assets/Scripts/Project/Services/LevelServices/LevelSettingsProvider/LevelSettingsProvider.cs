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
        
        private Dictionary<int, LevelSettings> _cachedLevelData;

        public LevelSettingsProvider(IReadAssetContainer assetContainer)
        {
            _assetContainer = assetContainer;
        }

        public LevelSettings GetLevelSettings(int levelIndex)
        {
            if (TryGetCachedLevelData(levelIndex, out LevelSettings cachedLevelData))
                return cachedLevelData;

            return CreateLevelData(levelIndex);
        }
        
        public override void LoadProgress(GameProgress progress)
        {
            _cachedLevelData = progress.LevelsData;
            _cachedLevelData ??= new Dictionary<int, LevelSettings>();
        }

        public override void UpdateProgress(GameProgress progress)
        {
            progress.LevelsData = new SerializedDictionary<int, LevelSettings>(_cachedLevelData);
        }

        private bool TryGetCachedLevelData(int levelIndex, out LevelSettings levelSettings)
        {
            levelSettings = default;
            
            var levelConfig = _assetContainer
                .GetConfig<LevelsConfig>()
                .GetLevelConfig(levelIndex);
            
            if (_cachedLevelData != null && _cachedLevelData.TryGetValue(levelIndex, out levelSettings))
            {
                if (!levelConfig.IsGeneratable)
                {
                    _cachedLevelData.Remove(levelIndex);
                    levelSettings = CreateLevelData(levelIndex); 
                }
                
                if (!levelSettings.IsEqual(levelConfig))
                {
                    levelSettings = CreateLevelData(levelIndex);
                    _cachedLevelData[levelIndex] = levelSettings;
                }
                
                return true;
            }

            return false;
        }
        
        private LevelSettings CreateLevelData(int levelIndex)
        {
            var config = _assetContainer
                .GetConfig<LevelsConfig>()
                .GetLevelConfig(levelIndex);

            var levelData = new LevelSettings(config);

            if (config.IsGeneratable)
            {
                _cachedLevelData[levelIndex] = levelData;
            }
            
            return levelData;
        }
    }
}
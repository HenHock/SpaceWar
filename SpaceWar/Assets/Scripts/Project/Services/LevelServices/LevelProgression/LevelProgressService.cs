using System;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Infrastructure.Services.SaveSystem.Data;
using Project.Infrastructure.Services.SaveSystem.SaveHandler;
using Project.Services.LevelServices.LevelProgression.Data;
using Project.Services.LevelServices.LevelSettingsProvider.Data;

namespace Project.Services.LevelServices.LevelProgression
{
    public class LevelProgressService : SaveHandler, ILevelProgressService
    {
        public int LastCompletedLevelIndex { get; private set; }
        
        private readonly IReadAssetContainer _assetContainer;

        private LevelState[] _levelStates;

        private LevelProgressService(IReadAssetContainer assetContainer)
        {
            _assetContainer = assetContainer;
        }

        public void SetLevelState(int level, LevelState newState)
        {
            _levelStates[level] = newState;
            LastCompletedLevelIndex = level;
        }

        public LevelState GetLevelState(int levelIndex)
        {
            if (levelIndex == LastCompletedLevelIndex + 1)
                return LevelState.Unlocked;
            
            if (levelIndex <= LastCompletedLevelIndex)
                return LevelState.Completed;

            return LevelState.Locked;
        }

        public override void LoadProgress(GameProgress progress)
        {
            var levelsConfig = _assetContainer.GetConfig<LevelsConfig>();

            if (progress.LevelStates != null)
            {
                _levelStates = progress.LevelStates;
                LastCompletedLevelIndex = Array.FindLastIndex(_levelStates, state => state is LevelState.Completed);

                if (_levelStates.Length < levelsConfig.TotalLevels)
                {
                    _levelStates = new LevelState[levelsConfig.TotalLevels];
                    Array.Copy(progress.LevelStates, _levelStates, progress.LevelStates.Length);
                }
            }
            else
            {
                LastCompletedLevelIndex = -1;
                _levelStates = new LevelState[levelsConfig.TotalLevels];
            }
            
        }

        public override void UpdateProgress(GameProgress progress)
        {
            progress.LevelStates = _levelStates;
        }
    }
}
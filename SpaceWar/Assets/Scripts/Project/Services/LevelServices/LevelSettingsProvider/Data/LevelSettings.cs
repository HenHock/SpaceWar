using System;
using Project.Services.LevelServices.LevelSettingsProvider.Extensions;

namespace Project.Services.LevelServices.LevelSettingsProvider.Data
{
    /// <summary>
    /// A struct to hold generated data related to a level.
    /// </summary>
    [Serializable]
    public readonly struct LevelSettings
    {
        public readonly int AsteroidSpawnCount;
        public readonly float AsteroidSpawnInterval;

        public LevelSettings(LevelConfig config) : this()
        {
            AsteroidSpawnCount = config.AsteroidSpawnCount.GetValue();
            AsteroidSpawnInterval = config.AsteroidSpawnInterval.GetValue();
        }

        /// <summary>
        /// Returns true if the current <see cref="LevelSettings"/> matches the provided <see cref="LevelConfig"/>
        /// </summary>
        public bool IsEqual(LevelConfig levelConfig) =>
            this.IsValid(AsteroidSpawnCount, levelConfig.AsteroidSpawnCount) &&
            this.IsValid(AsteroidSpawnInterval, levelConfig.AsteroidSpawnInterval);
    }
}
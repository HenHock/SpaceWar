using System;
using Project.Logic.Asteroid.Data;
using UnityEngine;

namespace Project.Services.LevelServices.LevelSettingsProvider.Data
{
    [Serializable]
    public struct LevelConfig
    {
#if UNITY_EDITOR
        [HideInInspector]
        public string Name;
#endif
        
        public AsteroidType AsteroidTypes;
        public GeneratedLevelSetting<int> AsteroidSpawnCount;
        public GeneratedLevelSetting<float> AsteroidSpawnInterval;
        
        public bool IsGeneratable => AsteroidSpawnCount.IsGenerated || AsteroidSpawnInterval.IsGenerated;
    }
}
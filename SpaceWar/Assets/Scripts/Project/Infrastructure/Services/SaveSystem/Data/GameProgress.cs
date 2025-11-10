using System;
using AYellowpaper.SerializedCollections;
using Project.Services.LevelServices.LevelSettingsProvider.Data;

namespace Project.Infrastructure.Services.SaveSystem.Data
{
    [Serializable]
    public class GameProgress
    {
        /// <summary> Generated levels data.</summary>
        public SerializedDictionary<int, LevelSettings> LevelsData;
    }
}
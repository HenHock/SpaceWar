using System;
using AYellowpaper.SerializedCollections;
using Project.Services.LevelServices.LevelProgression.Data;
using Project.Services.LevelServices.LevelSettingsProvider.Data;

namespace Project.Infrastructure.Services.SaveSystem.Data
{
    [Serializable]
    public class GameProgress
    {
        /// <summary> Generated levels settings</summary>
        public SerializedDictionary<int, LevelSettings> LevelSettings;

        /// <summary> Store the <see cref="LevelState"/> data by each level</summary>
        public LevelState[] LevelStates;
    }
}
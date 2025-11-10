using Project.Services.LevelServices.LevelSettingsProvider.Data;
using UnityEngine;

namespace Project.Services.LevelServices.LevelSettingsProvider.Extensions
{
    public static class LevelDataExtension
    {
        public static bool IsValid(this LevelSettings source, int value, GeneratedLevelSetting<int> setting)
        {
            if (!setting.IsGenerated)
                return value == setting.PredefinedValue;

            float min = setting.MinMaxValue.x;
            float max = setting.MinMaxValue.y;
            return value >= min && value <= max;
        }

        public static bool IsValid(this LevelSettings source, float value, GeneratedLevelSetting<float> setting)
        {
            if (!setting.IsGenerated)
                return Mathf.Approximately(value, setting.PredefinedValue);

            float min = setting.MinMaxValue.x;
            float max = setting.MinMaxValue.y;
            return value >= min && value <= max;
        }
    }
}
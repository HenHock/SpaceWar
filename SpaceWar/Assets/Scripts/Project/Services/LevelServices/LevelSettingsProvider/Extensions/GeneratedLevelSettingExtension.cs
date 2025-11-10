using Project.Services.LevelServices.LevelSettingsProvider.Data;
using UnityEngine;

namespace Project.Services.LevelServices.LevelSettingsProvider.Extensions
{
    public static class GeneratedLevelSettingExtension
    {
        public static int GetValue(this GeneratedLevelSetting<int> setting)
        {
            if (!setting.IsGenerated) return setting.PredefinedValue;
            float min = setting.MinMaxValue.x;
            float max = setting.MinMaxValue.y;
            return Random.Range(Mathf.RoundToInt(min), Mathf.RoundToInt(max));
        }

        public static float GetValue(this GeneratedLevelSetting<float> setting)
        {
            if (!setting.IsGenerated) return setting.PredefinedValue;
            float min = setting.MinMaxValue.x;
            float max = setting.MinMaxValue.y;
            return Random.Range(min, max);
        }
    }
}
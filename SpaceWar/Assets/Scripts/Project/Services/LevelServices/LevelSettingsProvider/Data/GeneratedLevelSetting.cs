using System;
using NaughtyAttributes;
using UnityEngine;

namespace Project.Services.LevelServices.LevelSettingsProvider.Data
{
    [Serializable]
    public struct GeneratedLevelSetting<T> where T : struct
    {
        public bool IsGenerated;

        [AllowNesting, HideIf(nameof(IsGenerated))]
        public T PredefinedValue;

        [AllowNesting, MinMaxSlider(0, float.MaxValue), ShowIf(nameof(IsGenerated))]
        public Vector2 MinMaxValue;
    }
}
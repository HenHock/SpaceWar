using UnityEngine;

namespace Project.Services.LevelServices.LevelSettingsProvider.Data
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [Header("Levels")]
        [SerializeField] private LevelConfig[] levels;

        public int TotalLevels => levels.Length;

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (var index = 0; index < levels.Length; index++)
            {
                var level = levels[index];
                level.Name = $"Level {index + 1}";
                levels[index] = level;
            }
        }
#endif

        public LevelConfig GetLevelConfig(int levelIndex)
        {
            levelIndex = Mathf.Clamp(levelIndex, 0, levels.Length - 1);
            return levels[levelIndex];
        }
    }
}
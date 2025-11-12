using Project.Services.LevelServices.LevelProgression.Data;

namespace Project.Services.LevelServices.LevelProgression
{
    public interface ILevelProgressService
    {
        void SetLevelState(int level, LevelState newState);
        LevelState GetLevelState(int levelIndex);
        int LastCompletedLevelIndex { get; }
    }
}
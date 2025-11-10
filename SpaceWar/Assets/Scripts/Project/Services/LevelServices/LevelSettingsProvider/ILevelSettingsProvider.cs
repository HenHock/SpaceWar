using Project.Services.LevelServices.LevelSettingsProvider.Data;

namespace Project.Services.LevelServices.LevelSettingsProvider
{
    public interface ILevelSettingsProvider
    {
        LevelSettings GetLevelSettings(int levelIndex);
    }
}
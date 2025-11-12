using Project.Infrastructure.Services.AssetManagement.Configs;
using Project.Infrastructure.Services.Windows.Data;
using Project.Services.LevelServices.LevelSettingsProvider.Data;

namespace Project.Infrastructure.Services.AssetManagement.AssetBundles.BootBundles
{
    /// <summary>
    /// Class to load and unload the assets from resource for all game lifetime.
    /// </summary>
    public class ProjectAssetBundle : AssetBundle
    {
        public override void Load()
        {
            AddConfig<LevelsConfig>(AssetPaths.LevelsConfig);
            AddConfig<WindowsConfig>(AssetPaths.WindowsConfig);
        }

        public override void Unload()
        {
            RemoveConfig<LevelsConfig>();
            RemoveConfig<WindowsConfig>();
        }
    }
}
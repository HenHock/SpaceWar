using Project.Infrastructure.Configs;
using Project.Infrastructure.Services.AssetManagement.Configs;

namespace Project.Infrastructure.Services.AssetManagement.AssetBundles.BootBundles
{
    /// <summary>
    /// Class to load and unload the assets from resource for all game lifetime.
    /// </summary>
    public class ProjectAssetBundle : AssetBundle
    {
        public override void Load()
        {
            AddConfig<GameConfig>(AssetPaths.GameConfig);
        }

        public override void Unload()
        {
            RemoveConfig<GameConfig>();
        }
    }
}
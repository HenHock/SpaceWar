using Project.Infrastructure.Services.AssetManagement.Configs;
using Project.Logic.Player;
using Project.Logic.Player.Data;

namespace Project.Infrastructure.Services.AssetManagement.AssetBundles.GameplayBundles
{
    /// <summary>
    /// Main class to load and unload the gameplay assets from resource.
    /// </summary>
    public class GameplayAssetBundle : AssetBundle
    {
        public override void Load()
        {
            AddConfig<PlayerConfig>(AssetPaths.PlayerConfig);
        }

        public override void Unload()
        {
            RemoveConfig<PlayerConfig>();
        }
    }
}
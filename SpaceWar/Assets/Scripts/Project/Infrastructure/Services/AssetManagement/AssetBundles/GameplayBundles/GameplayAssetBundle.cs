using Project.Infrastructure.Services.AssetManagement.Configs;
using Project.Logic.Asteroid.Data;
using Project.Logic.Player.Data;
using Project.Services.LevelServices.LevelSettingsProvider.Data;

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
            AddConfig<LevelsConfig>(AssetPaths.LevelsConfig);
            AddConfig<AsteroidViewConfig>(AssetPaths.AsteroidViewConfig);
            AddConfig<AsteroidBalanceConfig>(AssetPaths.AsteroidBalanceConfig);
        }

        public override void Unload()
        {
            RemoveConfig<LevelsConfig>();
            RemoveConfig<PlayerConfig>();
            RemoveConfig<AsteroidViewConfig>();
            RemoveConfig<AsteroidBalanceConfig>();
        }
    }
}
using System;
using Project.Infrastructure.Services.AssetManagement;
using Project.Infrastructure.Services.AssetManagement.AssetBundles;
using Project.Infrastructure.Services.AssetManagement.AssetBundles.GameplayBundles;
using Project.Infrastructure.Services.SaveSystem;

namespace Project.Infrastructure.Installers.GameplayScene
{
    /// <summary>
    /// Class to initialize services and load save data before Unity Awake callbacks
    /// </summary>
    public class GameplaySceneBootstrapper : IDisposable
    {
        private readonly IAssetLoader _assetLoader;

        public GameplaySceneBootstrapper
        (
            IAssetLoader assetLoader,
            ISaveLoadService saveLoadService
        )
        {
            _assetLoader = assetLoader;
            _assetLoader.LoadBundle<GameplayAssetBundle>();
            
            saveLoadService.InformProgressReader();
        }

        // Dispose of resources when exit from scene or game.
        public void Dispose() => _assetLoader.UnloadBundle<GameplayAssetBundle>();
    }
}
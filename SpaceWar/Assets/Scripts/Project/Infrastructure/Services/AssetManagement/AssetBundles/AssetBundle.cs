using Project.Infrastructure.Services.AssetManagement.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Infrastructure.Services.AssetManagement.AssetBundles
{
    public abstract class AssetBundle : IAssetBundle
    {
        private AssetContainer _container;

        public void Initialize(AssetContainer assetContainer) => 
            _container = assetContainer;

        public abstract void Load();

        public abstract void Unload();

        protected void AddAsset<TAsset>(string key, string path) where TAsset : Object
        {
            var asset = Load<TAsset>(path);
            _container.ProvidedAssets.TryAdd(key, asset);
        }

        protected void AddConfig<TConfig>(string path) where TConfig : ScriptableObject
        {
            var config = Load<TConfig>(path);
            _container.ProvidedConfigs.TryAdd(config.GetType(), config);
        }

        protected void RemoveAsset<TObject>(string key) where TObject : Component
        {
            var asset = _container.GetAsset<TObject>(key);
            _container.ProvidedAssets.Remove(key);
            
            Resources.UnloadAsset(asset);
        }

        protected void RemoveAsset(string key)
        {
            var asset = _container.GetAsset(key);
            _container.ProvidedAssets.Remove(key);
            
            Resources.UnloadAsset(asset);
        }

        protected void RemoveConfig<TConfig>() where TConfig : ScriptableObject
        {
            var config = _container.GetConfig<TConfig>();
            _container.ProvidedConfigs.Remove(typeof(TConfig));
            
            Resources.UnloadAsset(config);
        }

        private TObject Load<TObject>(string assetPath) where TObject : Object
        {
            var asset = Resources.Load<TObject>(assetPath);
            Debug.Assert(asset != null,$"Couldn't load the asset {typeof(TObject).Name} by path {assetPath}");
            
            return asset;
        }
    }
}
using Project.Infrastructure.Services.AssetManagement.AssetBundles;

namespace Project.Infrastructure.Services.AssetManagement
{
    public interface IAssetLoader
    {
        void LoadBundle<TBundle>() where TBundle : IAssetBundle, new();
        void UnloadBundle<TBundle>() where TBundle : IAssetBundle, new();
    }
}
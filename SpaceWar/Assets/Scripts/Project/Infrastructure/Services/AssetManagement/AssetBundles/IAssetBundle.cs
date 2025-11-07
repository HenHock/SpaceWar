using Project.Infrastructure.Services.AssetManagement.Data;

namespace Project.Infrastructure.Services.AssetManagement.AssetBundles
{
    public interface IAssetBundle
    {
        void Load();
        void Unload();
        void Initialize(AssetContainer container);
    }
}
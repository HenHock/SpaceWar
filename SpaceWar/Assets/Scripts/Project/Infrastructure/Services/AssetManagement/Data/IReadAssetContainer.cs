using UnityEngine;

namespace Project.Infrastructure.Services.AssetManagement.Data
{
    public interface IReadAssetContainer
    {
        TAsset GetAsset<TAsset>(string key) where TAsset : Component;
        GameObject GetAsset(string key);
        TConfig GetConfig<TConfig>() where TConfig : ScriptableObject;
    }
}
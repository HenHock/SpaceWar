using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Infrastructure.Services.AssetManagement.Data
{
    public class AssetContainer : IReadAssetContainer
    {
        public readonly Dictionary<string, object> ProvidedAssets = new();
        public readonly Dictionary<Type, ScriptableObject> ProvidedConfigs = new();

        public GameObject GetAsset(string key)
        {
            if (ProvidedAssets.TryGetValue(key, out var asset))
                return (GameObject)asset;

            throw new Exception($"Error when tried to get GameObject asset by {key} key. Maybe you forget to load it before!");
        }

        public TAsset GetAsset<TAsset>(string key) where TAsset : Component
        {
            if (ProvidedAssets.TryGetValue(key, out var asset))
                return (TAsset)asset;

            throw new Exception($"Error when tried to get {typeof(TAsset)} asset by {key} key. Maybe you forget to load it before!");
        }

        public TConfig GetConfig<TConfig>() where TConfig : ScriptableObject
        {
            if (ProvidedConfigs.TryGetValue(typeof(TConfig), out var config))
                return (TConfig)config;

            throw new Exception($"Error when tried to get {typeof(TConfig)} config. Maybe you forget to load it before!");
        }
    }
}
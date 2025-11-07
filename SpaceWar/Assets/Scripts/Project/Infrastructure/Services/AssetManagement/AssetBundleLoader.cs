using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Project.Extensions;
using Project.Infrastructure.Services.AssetManagement.AssetBundles;
using Project.Infrastructure.Services.AssetManagement.Data;
using UnityEngine;
using ILogger = Project.Infrastructure.Logger.ILogger;

namespace Project.Infrastructure.Services.AssetManagement
{

    public class AssetBundleLoader : IAssetLoader, ILogger
    {
        public Color DefaultColor => Color.blue;
        public bool IsActiveLogger => true;

        private readonly AssetContainer _container;
        private readonly Dictionary<Type, IAssetBundle> _bundles;

        public AssetBundleLoader(AssetContainer container)
        {
            _container = container;
            _bundles = new Dictionary<Type, IAssetBundle>();

            this.Log("Asset provider initialized");
        }

        public void LoadBundle<TBundle>() where TBundle : IAssetBundle, new() =>
            GetBundle<TBundle>(true)?.Load();

        public void UnloadBundle<TBundle>() where TBundle : IAssetBundle, new()
        {
            var assetBundle = GetBundle<TBundle>(false);
            
            if (assetBundle != null)
            {
                assetBundle.Unload();
                _bundles.Remove(assetBundle.GetType());
            }
        }

        [CanBeNull]
        private IAssetBundle GetBundle<TBundle>(bool createIfDoesNotExist) where TBundle : IAssetBundle, new()
        {
            if (_bundles.TryGetValue(typeof(TBundle), out var bundle))
                return bundle;

            if (createIfDoesNotExist)
                return CreateBundle<TBundle>();

            return null;
        }

        private IAssetBundle CreateBundle<TBundle>() where TBundle : IAssetBundle, new() =>
            new TBundle().With(bundle => bundle.Initialize(_container));
    }
}
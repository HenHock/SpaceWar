using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Logic.Player.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Services.LevelServices.Factory
{
    public class LevelFactory : ILevelFactory
    {
        private readonly SceneContextRegistry _registry;
        private readonly IReadAssetContainer _assetContainer;

        private GameObject _player;
        private IInstantiator _instantiator;

        public LevelFactory(SceneContextRegistry registry, IReadAssetContainer assetContainer)
        {
            _registry = registry;
            _assetContainer = assetContainer;
        }

        public void CreateLevel(int levelIndex)
        {
            _instantiator = GetSceneInstantiator();
            
            _player = SpawnPlayer();
        }

        private GameObject SpawnPlayer()
        {
            var camera = Camera.main;
            var playerConfig = _assetContainer.GetConfig<PlayerConfig>();
            
            var halfHeight = camera.orthographicSize;
            var yPadding = playerConfig.Prefab.transform.localScale.y / 2f;
            var minY = camera.transform.position.y - halfHeight + yPadding;

            return _instantiator.InstantiatePrefab(playerConfig.Prefab, new Vector3(0, minY), Quaternion.identity, null);
        }

        private IInstantiator GetSceneInstantiator() => _registry.GetContainerForScene(SceneManager.GetActiveScene());
    }
}
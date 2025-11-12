using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Logic.Player.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Services.LevelServices.Factory
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IReadAssetContainer _assetContainer;

        private GameObject _player;

        public LevelFactory(IInstantiator instantiator, IReadAssetContainer assetContainer)
        {
            _instantiator = instantiator;
            _assetContainer = assetContainer;
        }

        public void CreateLevel()
        {
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
    }
}
using Project.Infrastructure.Pooling;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Logic.Asteroid;
using Project.Logic.Asteroid.Data;
using UnityEngine;
using Zenject;

namespace Project.Services.AsteroidServices.Factory
{
    public class AsteroidFactory : IAsteroidFactory, IInitializable
    {
        private readonly IInstantiator _instantiator;
        private readonly MonoPool<AsteroidBrain> _asteroidPool;
        
        private Camera _camera;

        public AsteroidFactory(IInstantiator instantiator, IReadAssetContainer assetContainer)
        {
            _instantiator = instantiator;
            _asteroidPool = CreateAsteroidPool(assetContainer.GetConfig<AsteroidViewConfig>().Prefab);
        }

        public void Initialize()
        {
            _camera = Camera.main;
        }

        public void Dispose()
        {
            _asteroidPool.Clear();
        }

        public AsteroidBrain SpawnAsteroid(AsteroidType asteroidType)
        {
            AsteroidBrain asteroid = _asteroidPool.Get();
            asteroid.Initialize(asteroidType);
            asteroid.OnReleased += ReturnToPool;
            asteroid.transform.position = GetSpawnPosition(asteroid);
            
            return asteroid;
        }

        private Vector3 GetSpawnPosition(AsteroidBrain asteroid)
        {
            Vector3 position = asteroid.transform.position;

            position.x = GetXPosition(asteroid.transform);
            position.y = GetYPosition(asteroid.transform);

            return position;
        }

        private float GetYPosition(Transform asteroid)
        {
            float yPadding = asteroid.localScale.y;
            float halfHeight = _camera.orthographicSize;
            float heightOffSet = _camera.transform.position.y + halfHeight + yPadding;
            
            return heightOffSet;
        }

        private float GetXPosition(Transform asteroid)
        {
            float xPadding = asteroid.localScale.x / 2;
            float halfHeight = _camera.orthographicSize;
            float halfWidth = _camera.aspect * halfHeight;

            float leftThreshold = _camera.transform.position.x - halfWidth + xPadding;
            float rightThreshold = _camera.transform.position.x + halfWidth - xPadding;

            return Random.Range(leftThreshold, rightThreshold);
        }

        private void ReturnToPool(AsteroidBrain asteroid) => _asteroidPool.Release(asteroid);

        private MonoPool<AsteroidBrain> CreateAsteroidPool(GameObject prefab)
        {
            var container = new GameObject("AsteroidsContainer").transform;
            return new MonoPool<AsteroidBrain>(_instantiator, prefab, container, 20);
        }
    }
}
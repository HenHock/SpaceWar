using UnityEngine;
using Zenject;
using Project.Infrastructure.Pooling;

namespace Project.Logic.Bullet.Factory
{
    public class BulletFactory
    {
        private readonly MonoPool<BulletBrain> _pool;

        public BulletFactory
        (
            IInstantiator instantiator,
            GameObject bulletPrefab,
            Transform container = null,
            int defaultSize = 10,
            int maxSize = 20
        )
        {
            _pool = new MonoPool<BulletBrain>(instantiator, bulletPrefab, container, defaultSize,  maxSize);
        }

        public BulletBrain Create(Vector3 position)
        {
            var bullet = _pool.Get();
            bullet.Initialize(position);
            bullet.OnReleased += ReturnToPool;
            
            return bullet;
        }

        public void ClearPool() => _pool.Clear();
        
        private void ReturnToPool(BulletBrain bullet) => _pool.Release(bullet);
    }
}

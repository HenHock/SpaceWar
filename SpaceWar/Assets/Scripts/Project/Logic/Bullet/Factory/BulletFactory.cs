using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Logic.Bullet.Factory
{
    public class BulletFactory
    {
        private readonly Transform _container;
        private readonly GameObject _bulletPrefab;
        private readonly IInstantiator _instantiator;
        private readonly ObjectPool<BulletBrain> _pool;

        public BulletFactory(IInstantiator instantiator, GameObject bulletPrefab, Transform container = null, int defaultSize = 10, int maxSize = 20)
        {
            Debug.Assert(bulletPrefab != null, "Bullet prefab can't be null.");

            _container = container;
            _bulletPrefab = bulletPrefab;
            _instantiator = instantiator;

            _pool = new ObjectPool<BulletBrain>(
                createFunc: Create,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroy,
                collectionCheck: false,
                defaultCapacity: Mathf.Max(0, defaultSize),
                maxSize: Mathf.Max(1, maxSize)
            );
        }

        public BulletBrain Create(Vector3 position)
        {
            var bullet = _pool.Get();
            
            if (bullet != null)
            {
                bullet.transform.position = position;
                
                if (_container != null && bullet.transform.parent != _container)
                {
                    bullet.transform.SetParent(_container);
                }
            }
            
            return bullet;
        }

        public void Release(BulletBrain bullet)
        {
            if (bullet == null) 
                return;
            
            _pool.Release(bullet);
        }

        public void ClearPool()
        {
            _pool.Clear();
        }

        private BulletBrain Create()
        {
            var bullet = _instantiator.InstantiatePrefabForComponent<BulletBrain>(_bulletPrefab, _container);
            bullet.gameObject.SetActive(false);
            
            return bullet;
        }

        private void OnGet(BulletBrain bullet)
        {
            if (bullet == null) 
                return;
            
            bullet.Initialize();
            bullet.gameObject.SetActive(true);
        }

        private void OnRelease(BulletBrain bullet)
        {
            if (bullet == null) return;
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroy(BulletBrain bullet)
        {
            if (bullet == null) 
                return;
            
            Object.Destroy(bullet.gameObject);
        }
    }
}

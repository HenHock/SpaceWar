using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;
using System;

namespace Project.Infrastructure.Pooling
{
    /// <summary>
    /// Generic reusable object pool for MonoBehaviour components instantiated from a prefab via Zenject.
    /// Supports composition via lifecycle delegate callbacks instead of inheritance.
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    public class MonoPool<T> where T : MonoBehaviour
    {
        public event Action<T> OnCreated;
        public event Action<T> OnBeforeGet;
        public event Action<T> OnBeforeRelease;
        public event Action<T> OnBeforeDestroy;
        
        protected readonly Transform Container;
        protected readonly GameObject Prefab;
        protected readonly IInstantiator Instantiator;
        
        private readonly ObjectPool<T> _pool;

        public MonoPool
        (
            IInstantiator instantiator,
            GameObject prefab,
            Transform container = null,
            int defaultSize = 10,
            int maxSize = 20,
            bool collectionCheck = false
        )
        {
            Debug.Assert(prefab != null, "Prefab can't be null.");
            Instantiator = instantiator;
            Prefab = prefab;
            Container = container;

            _pool = new ObjectPool<T>(
                createFunc: CreateInternal,
                actionOnGet: OnGetInternal,
                actionOnRelease: OnReleaseInternal,
                actionOnDestroy: OnDestroyInternal,
                collectionCheck: collectionCheck,
                defaultCapacity: Mathf.Max(0, defaultSize),
                maxSize: Mathf.Max(1, maxSize));
        }

        /// <summary>
        /// Acquire an instance from the pool.
        /// </summary>
        public T Get() => _pool.Get();

        /// <summary>
        /// Release an instance back to the pool.
        /// </summary>
        public void Release(T instance) { if (instance != null) _pool.Release(instance); }

        /// <summary>
        /// Clears pool (destroys all pooled objects).
        /// </summary>
        public void Clear() => _pool.Clear();

        private T CreateInternal()
        {
            var comp = Instantiator.InstantiatePrefabForComponent<T>(Prefab, Container);
            // Default behaviour: deactivate until first get
            if (comp != null)
                comp.gameObject.SetActive(false);
            OnCreated?.Invoke(comp);
            return comp;
        }

        private void OnGetInternal(T obj)
        {
            if (obj == null) return;
            // Default: activate
            obj.gameObject.SetActive(true);
            OnBeforeGet?.Invoke(obj);
        }

        private void OnReleaseInternal(T obj)
        {
            if (obj == null) return;
            OnBeforeRelease?.Invoke(obj);
            // Default: deactivate after callback so callback can still access active state if desired
            obj.gameObject.SetActive(false);
        }

        private void OnDestroyInternal(T obj)
        {
            if (obj == null) return;
            OnBeforeDestroy?.Invoke(obj);
            Object.Destroy(obj.gameObject);
        }
    }
}

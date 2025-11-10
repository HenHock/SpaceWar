using System;
using NaughtyAttributes;
using UnityEngine;

namespace Project.Logic.Behavior.Death___Life
{
    public class LifeTimeBehavior : MonoBehaviour
    {
#if UNITY_EDITOR
        [ShowNativeProperty]
        private float Timer => _timer;
        [ShowNativeProperty]
        private float LifeTime => _lifeTime;
#endif
        
        public event Action OnLifeTimeExpired;
        
        private float _timer;
        private float _lifeTime;
        
        public void Initialize(float lifeTime)
        {
            _timer = 0;
            _lifeTime = lifeTime;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            
            if (_timer >= _lifeTime)
            {
                OnLifeTimeExpired?.Invoke();
            }
        }
    }
}
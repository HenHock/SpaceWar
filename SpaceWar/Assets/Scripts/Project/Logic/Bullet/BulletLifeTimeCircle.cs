using System;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Logic.Player.Data;
using UnityEngine;
using Zenject;

namespace Project.Logic.Bullet
{
    public class BulletLifeTimeCircle : MonoBehaviour
    {
        public event Action OnLifeTimeEnded;
        
        private float _timer;
        private BulletConfig _bulletConfig;
        
        [Inject]
        private void Construct(IReadAssetContainer assetContainer)
        {
            _bulletConfig = assetContainer.GetConfig<PlayerConfig>().BulletConfig;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _bulletConfig.LifeTime)
            {
                _timer = 0;
                enabled = false;
                OnLifeTimeEnded?.Invoke();
            }
        }

        public void ResetTimer()
        {
            _timer = 0;
            enabled = true;
        }
    }
}
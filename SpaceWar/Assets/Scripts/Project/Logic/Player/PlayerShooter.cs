using System;
using NaughtyAttributes;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Infrastructure.Services.Input;
using Project.Logic.Bullet;
using Project.Logic.Bullet.Factory;
using Project.Logic.Player.Data;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Logic.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        private PlayerConfig _playerConfig;
        private IInputService _inputService;
        private BulletFactory _bulletFactory;

        [Inject]
        private void Construct(IInputService inputService, IReadAssetContainer assetContainer, IInstantiator instantiator)
        {
            _inputService = inputService;
            _playerConfig = assetContainer.GetConfig<PlayerConfig>();

            var container = new GameObject("BulletsContainer");
            _bulletFactory = new BulletFactory(instantiator, _playerConfig.BulletConfig.Prefab, container.transform);
        }

        private void Start()
        {
            _inputService.OnShootPressed
                .ThrottleFirst(TimeSpan.FromSeconds(_playerConfig.ShootCooldown))
                .Subscribe(Shoot)
                .AddTo(this);
        }

        private void OnDestroy()
        {
            _bulletFactory.ClearPool();
        }

        private void Shoot(Unit _)
        {
            var bullet = _bulletFactory.Create(transform.position);
            bullet.OnReleased += ReleaseBullet;
        }

        private void ReleaseBullet(BulletBrain bullet)
        {
            _bulletFactory.Release(bullet);
        }
    }
}
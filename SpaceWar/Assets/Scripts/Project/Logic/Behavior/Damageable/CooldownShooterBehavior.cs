using System;
using Project.Logic.Bullet.Factory;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Logic.Behavior.Damageable
{
    public class CooldownShooterBehavior : MonoBehaviour
    {
        private BulletFactory _bulletFactory;
        private IInstantiator _instantiator;
        
        [Inject]
        private void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void Initialize(float cooldown, GameObject bulletPrefab, Subject<Unit> fireCommand)
        {
            var container = new GameObject("BulletsContainer").transform;
            _bulletFactory = new BulletFactory(_instantiator, bulletPrefab, container.transform);
            
            fireCommand.ThrottleFirst(TimeSpan.FromSeconds(cooldown))
                .Subscribe(Shoot)
                .AddTo(this);
        }

        private void OnDestroy()
        {
            _bulletFactory.ClearPool();
        }

        private void Shoot(Unit _)
        {
            _bulletFactory.Create(transform.position);
        }
    }
}
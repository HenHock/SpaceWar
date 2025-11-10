using System;
using DG.Tweening;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Logic.Behavior;
using Project.Logic.Behavior.Damageable;
using Project.Logic.Behavior.Death___Life;
using Project.Logic.Behavior.Movement;
using Project.Logic.Player.Data;
using Project.Utilities;
using UnityEngine;
using Zenject;

namespace Project.Logic.Bullet
{
    public class BulletBrain : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private LifeTimeBehavior bulletLifeTime;
        [SerializeField] private LinerMoveBehavior bullerMovement;
        [SerializeField] private LayerDamageableBehavior damageableBehavior;
        
        private BulletConfig _bulletConfig;

        public event Action<BulletBrain> OnReleased;

        private void Awake()
        {
            damageableBehavior.OnHitTarget += Release;
            bulletLifeTime.OnLifeTimeExpired += Release;
        }

        [Inject]
        private void Construct(IReadAssetContainer assetContainer)
        {
            _bulletConfig = assetContainer.GetConfig<PlayerConfig>().BulletConfig;
        }

        public void Initialize(Vector3 position)
        {
            OnReleased = null;
            transform.position = position;
            
            triggerObserver.SetTrigger(true);
            damageableBehavior.Initialize(_bulletConfig.Damage);
            bulletLifeTime.Initialize(_bulletConfig.LifeTime);
            bullerMovement.Initialize(Vector2.up, _bulletConfig.MoveSpeed);
        }

        private void Release()
        {
            triggerObserver.SetTrigger(false);
            OnReleased?.Invoke(this);
        }
    }
}
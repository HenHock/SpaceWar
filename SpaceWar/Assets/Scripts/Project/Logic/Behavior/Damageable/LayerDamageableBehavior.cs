using System;
using Project.Logic.Behavior.Hittable;
using Project.Utilities;
using UniRx;
using UnityEngine;

namespace Project.Logic.Behavior.Damageable
{
    public class LayerDamageableBehavior : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        
        [Space]
        [SerializeField] private LayerMask enemyLayers;

        public event Action OnHitTarget;
        
        private float _damage;

        public void Initialize(float damage)
        {
            _damage = damage;
        }
        
        private void Start()
        {
            triggerObserver.OnTriggerEnter
                .Where(IsTarget)
                .Select(GetHittable)
                .Subscribe(Hit)
                .AddTo(this);
        }

        private void Hit(IHittable hittable)
        {
            hittable.TakeDamage(_damage);
            OnHitTarget?.Invoke();
        }

        private IHittable GetHittable(Collider2D other)
        {
            return other.GetComponent<TriggerProxy>().Hittable;
        }

        private bool IsTarget(Collider2D target)
        {
            return (enemyLayers.value & (1 << target.gameObject.layer)) != 0;
        }
    }
}
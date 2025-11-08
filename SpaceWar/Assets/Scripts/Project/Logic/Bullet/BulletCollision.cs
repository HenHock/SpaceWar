using System;
using Project.Utilities;
using UniRx;
using UnityEngine;

namespace Project.Logic.Bullet
{
    public class BulletCollision : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;

        public event Action OnHitTarget;

        private void Start()
        {
            triggerObserver.OnTriggerEnter
                .Where(IsTarget)
                .Subscribe(Hit)
                .AddTo(this);
        }

        public void SetCollision(bool isEnabled)
        {
            triggerObserver.SetTrigger(isEnabled);
        }
        
        private void Hit(Collider2D target)
        {
            Debug.Log($"Hit target {target.gameObject}");
            OnHitTarget?.Invoke();
        }

        private bool IsTarget(Collider2D target) => target.CompareTag("Asteroid");
    }
}
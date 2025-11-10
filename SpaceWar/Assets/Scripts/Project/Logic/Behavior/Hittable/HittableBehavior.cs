using Project.Logic.Behavior.Death___Life;
using TMPro;
using UniRx;
using UnityEngine;

namespace Project.Logic.Behavior.Hittable
{
    public class HittableBehavior : MonoBehaviour, IHittable
    {
        [SerializeField] private DeathBehavior deathBehavior;
        
        private ReactiveProperty<float> _health;

        public void Initialize(float health)
        {
            _health ??= new ReactiveProperty<float>(health);
            _health.Value = health;
        }
        
        public void Initialize(ReactiveProperty<float> health)
        {
            _health = health;
        }

        public void TakeDamage(float damage)
        {
            _health.Value -= damage;

            if (_health.Value <= damage)
            {
                deathBehavior.Die();
            }
        }
    }
}
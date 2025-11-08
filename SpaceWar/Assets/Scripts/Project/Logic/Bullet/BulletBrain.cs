using System;
using UnityEngine;

namespace Project.Logic.Bullet
{
    public class BulletBrain : MonoBehaviour
    {
        [SerializeField] private BulletCollision bulletCollision;
        [SerializeField] private BulletLifeTimeCircle bulletLifeTime;

        public event Action<BulletBrain> OnReleased;

        private void Awake()
        {
            bulletCollision.OnHitTarget += Release;
            bulletLifeTime.OnLifeTimeEnded += Release;
        }

        public void Initialize()
        {
            OnReleased = null;
            bulletLifeTime.ResetTimer();
            bulletCollision.SetCollision(true);
        }

        private void Release()
        {
            bulletCollision.SetCollision(false);
            
            OnReleased?.Invoke(this);
        }
    }
}
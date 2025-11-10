using System;
using UnityEngine;

namespace Project.Logic.Behavior.Death___Life
{
    public class DeathBehavior : MonoBehaviour
    {
        public event Action OnDeath;
        
        public void Die()
        {
            OnDeath?.Invoke();
        }
    }
}
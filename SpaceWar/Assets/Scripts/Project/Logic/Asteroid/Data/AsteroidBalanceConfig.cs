using UnityEngine;

namespace Project.Logic.Asteroid.Data
{
    [CreateAssetMenu(fileName = "AsteroidBalanceConfig", menuName = "Configs/Asteroid/BalanceConfig")]
    public class AsteroidBalanceConfig : ScriptableObject
    {
        [Header("Base Values")]
        public float BaseLifeTime = 2.5f;
        public float BaseSpeed = 15f;
        public float BaseScale = 0.6f;
        
        [Header("Exponents (1 = linear)")]
        public float LifeTimeExponent = 1f;
        public float SpeedExponent = 1f;
        public float ScaleExponent = 1f;
        
        [Header("Damage & Health")]
        public float Damage = 1;
        public int HitToDestroy = 1;
    }
}
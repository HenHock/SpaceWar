using Project.Logic.Asteroid.Data;
using UnityEngine;

namespace Project.Logic.Asteroid.StaticData
{
    public static class AsteroidBalancer
    {
        public static AsteroidStats GetStats(AsteroidBalanceConfig config, AsteroidType type)
        {
            int sizeIndex = (int)type + 1;

            var speed = config.BaseSpeed / Mathf.Pow(sizeIndex, config.SpeedExponent);
            var scale = config.BaseScale * Mathf.Pow(sizeIndex, config.ScaleExponent);
            var lifeTime = config.BaseLifeTime * Mathf.Pow(sizeIndex, config.LifeTimeExponent);

            return new AsteroidStats(speed, scale, lifeTime);
        }
    }
}
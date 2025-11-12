using System;
using Project.Logic.Asteroid;
using Project.Logic.Asteroid.Data;

namespace Project.Services.AsteroidServices.Factory
{
    public interface IAsteroidFactory : IDisposable
    {
        AsteroidBrain SpawnAsteroid(AsteroidType asteroidType);
        void ReturnToPool(AsteroidBrain asteroid);
        bool IsAllAsteroidsInPool { get; }
    }
}
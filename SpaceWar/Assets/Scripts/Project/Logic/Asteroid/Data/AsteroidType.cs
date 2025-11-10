using System;

namespace Project.Logic.Asteroid.Data
{
    [Flags]
    public enum AsteroidType
    {
        Small = 1 << 1,
        Medium = 1 << 2,
        Large = 1 << 3,
    }
}
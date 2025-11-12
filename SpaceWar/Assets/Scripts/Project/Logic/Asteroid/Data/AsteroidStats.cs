namespace Project.Logic.Asteroid.Data
{
    public readonly struct AsteroidStats
    {
        public readonly float Speed;
        public readonly float Scale;
        public readonly float Lifetime;

        public AsteroidStats(float speed, float scale, float lifetime)
        {
            Speed = speed;
            Scale = scale;
            Lifetime = lifetime;
        }
    }
}
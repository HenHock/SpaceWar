namespace Project.Logic.Asteroid.Data
{
    public struct AsteroidStats
    {
        public float Speed;
        public float Scale;
        public float Lifetime;

        public AsteroidStats(float speed, float scale, float lifetime)
        {
            Speed = speed;
            Scale = scale;
            Lifetime = lifetime;
        }
    }
}
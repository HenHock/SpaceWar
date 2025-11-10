using AYellowpaper.SerializedCollections;
using JetBrains.Annotations;
using UnityEngine;

namespace Project.Logic.Asteroid.Data
{
    [CreateAssetMenu(fileName = "AsteroidViewConfig", menuName = "Configs/Asteroid/ViewConfig")]
    public class AsteroidViewConfig : ScriptableObject
    {
        public GameObject Prefab;
        
        [Space]
        [SerializeField] 
        private SerializedDictionary<AsteroidType, Sprite> asteroidSprites;

        [CanBeNull]
        public Sprite GetSprite(AsteroidType type)
        {
            if (asteroidSprites.TryGetValue(type, out var sprite))
            {
                return sprite;
            }

            return GetDefaultSprite();
        }
        
        [CanBeNull]
        private Sprite GetDefaultSprite()
        {
            foreach (var spiteByType in asteroidSprites)
            {
                // -1 is the underlying value for 'Everything' enum
                if ((int)spiteByType.Key == -1)
                {
                    return spiteByType.Value;
                }
            }

            return null;
        }
    }
}
using UnityEngine;
using NaughtyAttributes;

namespace Project.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "Configs/GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Scene] 
        public int GameplayScene;
    }
}
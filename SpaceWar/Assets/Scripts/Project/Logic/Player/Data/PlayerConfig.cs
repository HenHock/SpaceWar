using NaughtyAttributes;
using UnityEngine;

namespace Project.Logic.Player.Data
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Visual")]
        public GameObject Prefab;
        
        [Header("Movement")]
        public float MoveSpeed = 5f;
        
        [Header("Health")]
        public float Health = 3f;

        [Header("Shooting")]
        public float ShootCooldown = 0.5f;
        
        [HorizontalLine, Expandable]
        public BulletConfig BulletConfig;
    }
}
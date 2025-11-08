using UnityEngine;

namespace Project.Logic.Player.Data
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("General")]
        public GameObject Prefab;
        
        [Header("Movement")]
        public float MoveSpeed = 5f;
        
        [Header("Shooting")]
        public GameObject BulletPrefab;
        
        [Space]
        public float ShootCooldown = 0.5f;
    }
}
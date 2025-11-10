using UnityEngine;

namespace Project.Logic.Player.Data
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        public GameObject Prefab;
        
        [Space]
        public float Damage = 1;
        public float MoveSpeed = 40;
        public float LifeTime = 0.8f;
    }
}
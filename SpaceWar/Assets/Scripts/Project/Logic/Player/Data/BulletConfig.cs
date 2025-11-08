using UnityEngine;

namespace Project.Logic.Player.Data
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        public GameObject Prefab;
        
        [Space]
        public float LifeTime;
        public float MoveSpeed;
    }
}
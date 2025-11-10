using Project.Logic.Player.Data;
using UniRx;
using UnityEngine;

namespace Project.Logic.Player.Model
{
    public class PlayerModel : IPlayerModel
    {
        public float Cooldown { get; }
        public float MoveSpeed { get; }
        public GameObject BulletPrefab { get; }
        public ReactiveProperty<float> Health { get; }

        public PlayerModel(PlayerConfig config)
        {
            Health = new ReactiveProperty<float>(config.Health);
            
            MoveSpeed = config.MoveSpeed;
            Cooldown = config.ShootCooldown;
            BulletPrefab = config.BulletConfig.Prefab;
        }
    }
}
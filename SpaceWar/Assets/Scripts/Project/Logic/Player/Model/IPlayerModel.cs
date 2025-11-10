using UniRx;
using UnityEngine;

namespace Project.Logic.Player.Model
{
    public interface IPlayerModel
    {
        ReactiveProperty<float> Health { get; }
        
        GameObject BulletPrefab { get; }
        float Cooldown { get; }
        float MoveSpeed { get; }
    }
}
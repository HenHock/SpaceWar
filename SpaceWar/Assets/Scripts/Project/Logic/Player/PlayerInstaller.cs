using System;
using Project.Infrastructure.Services.Input;
using Project.Logic.Behavior.Damageable;
using Project.Logic.Behavior.Hittable;
using Project.Logic.Player.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Logic.Player
{
    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private HittableBehavior hittableBehavior;
        [SerializeField] private CooldownShooterBehavior cooldownShooterBehavior;
        
        private IPlayerModel _playerModel;
        private IInputService _inputService;

        [Inject]
        private void Construct(IPlayerModel playerModel, IInputService inputService)
        {
            _playerModel = playerModel;
            _inputService = inputService;
        }

        private void Start()
        {
            hittableBehavior.Initialize(_playerModel.Health);
            playerMovement.Initialize(_playerModel.MoveSpeed);
            cooldownShooterBehavior.Initialize(_playerModel.Cooldown, _playerModel.BulletPrefab, _inputService.OnShootPressed);
        }
    }
}
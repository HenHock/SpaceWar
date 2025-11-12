using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Logic.Asteroid.Data;
using Project.Logic.Asteroid.StaticData;
using Project.Logic.Behavior.Damageable;
using Project.Logic.Behavior.Death___Life;
using Project.Logic.Behavior.Hittable;
using Project.Logic.Behavior.Movement;
using Project.Logic.Behavior.Renderer;
using Project.Utilities;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Logic.Asteroid
{
    public class AsteroidBrain : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private SpriteRendererViewBehavior viewBehavior;
        
        [Header("Damage & Life")]
        [SerializeField] private DeathBehavior deathBehavior;
        [SerializeField] private HittableBehavior hittableBehavior;
        [SerializeField] private LifeTimeBehavior asteroidLifeTime;
        [SerializeField] private LayerDamageableBehavior damageableBehavior;
        
        [Header("Movement")]
        [SerializeField] private LinerMoveBehavior asteroidMovement;
        
        [Header("Collision")]
        [SerializeField] private TriggerObserver triggerObserver;
        
        private AsteroidViewConfig _viewConfig;
        private AsteroidBalanceConfig _balanceConfig;

        public Subject<AsteroidBrain> OnReleased;
        
        private void Awake()
        {
            deathBehavior.OnDeath += Release;
            damageableBehavior.OnHitTarget += Release;
            asteroidLifeTime.OnLifeTimeExpired += Release;
        }

        [Inject]
        private void Construct(IReadAssetContainer assetContainer)
        {
            _viewConfig = assetContainer.GetConfig<AsteroidViewConfig>();
            _balanceConfig = assetContainer.GetConfig<AsteroidBalanceConfig>();
        }

        public void Initialize(AsteroidType asteroidType)
        {
            OnReleased = new Subject<AsteroidBrain>();

            AsteroidStats stats = AsteroidBalancer.GetStats(_balanceConfig, asteroidType);

            transform.localScale = Vector3.one * stats.Scale;
            
            triggerObserver.SetTrigger(true);
            asteroidLifeTime.Initialize(stats.Lifetime);
            asteroidMovement.Initialize(Vector2.down, stats.Speed);
            damageableBehavior.Initialize(_balanceConfig.Damage);
            viewBehavior.Initialize(_viewConfig.GetSprite(asteroidType));
            hittableBehavior.Initialize(_balanceConfig.HitToDestroy);
        }

        private void Release()
        {
            triggerObserver.SetTrigger(false);
            OnReleased?.OnNext(this);
        }
    }
}
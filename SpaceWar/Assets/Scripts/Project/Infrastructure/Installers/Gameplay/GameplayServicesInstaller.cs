using System.Threading;
using Project.Extensions;
using Project.Services.AsteroidServices.Factory;
using Project.Services.AsteroidServices.SpawnScheduler;
using UnityEngine;
using Zenject;
using ILogger = Project.Infrastructure.Logger.ILogger;

namespace Project.Infrastructure.Installers.Gameplay
{
    /// <summary>
    /// Installer to bind object or services from Gameplay scene.
    /// </summary>
    public class GameplayServicesInstaller : MonoInstaller, ILogger
    {
        public Color DefaultColor => Color.green;
        
        public override void InstallBindings()
        {
            this.Log("Start bind gameplay services");
            
            BindAsteroidFactory();
            BindAsteroidSpawnScheduler();
            BindUnloadSceneCancellationToken();
            
            this.Log("Completed bind gameplay services");
        }

        private void BindAsteroidSpawnScheduler() => Container
            .BindInterfacesTo<AsteroidSpawnScheduler>()
            .AsSingle();

        private void BindUnloadSceneCancellationToken() => Container
            .BindInterfacesAndSelfTo<CancellationToken>()
            .FromInstance(destroyCancellationToken)
            .AsSingle();

        private void BindAsteroidFactory() => Container
            .BindInterfacesTo<AsteroidFactory>()
            .AsSingle();
    }
}

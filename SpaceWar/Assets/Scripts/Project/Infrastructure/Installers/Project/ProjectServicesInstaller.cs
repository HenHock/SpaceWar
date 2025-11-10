using Project.Extensions;
using Project.Infrastructure.BootStateMachine;
using Project.Infrastructure.BootStateMachine.StateFactory;
using Project.Infrastructure.Services.AssetManagement;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Infrastructure.Services.Factories;
using Project.Infrastructure.Services.Input;
using Project.Infrastructure.Services.SaveSystem;
using Project.Infrastructure.Services.SaveSystem.PersistentProgressService;
using Project.Infrastructure.Services.SceneLoader;
using Project.Services.LevelServices.Factory;
using Project.Services.LevelServices.LevelChanger;
using Project.Services.LevelServices.LevelSettingsProvider;
using UnityEngine;
using Zenject;
using ILogger = Project.Infrastructure.Logger.ILogger;

namespace Project.Infrastructure.Installers.Project
{
    /// <summary>
    /// Class to bind services that will live throughout the entire game life.
    /// </summary>
    public class ProjectServicesInstaller : MonoInstaller, ILogger
    {
        public Color DefaultColor => Color.green;

        public override void InstallBindings()
        {
            this.Log("Start bind game services");

            BindInputService();
            BindSceneLoader();
            BindProgressService();
            BindSaveLoadService();
            BindStateFactory();
            BindGameStateMachine();
            BindUIFactory();
            BindLevelFactory();
            BindAssetContainerWithLoader();
            BindLevelChangerService();
            BindLevelDataProviderService();

            this.Log("Completed bind game services");
        }

        private void BindInputService() => Container
            .BindInterfacesTo<InputService>()
            .AsSingle();

        private void BindSceneLoader() => Container
            .BindInterfacesTo<SceneLoader>()
            .AsSingle();

        private void BindStateFactory() => Container
            .BindInterfacesTo<StateFactory>()
            .AsSingle();

        private void BindProgressService() => Container
            .BindInterfacesTo<PersistentProgressService>()
            .AsSingle();

        private void BindSaveLoadService() => Container
            .BindInterfacesTo<SaveLoadService>()
            .AsSingle();

        private void BindGameStateMachine() => Container
            .BindInterfacesTo<GameStateMachine>()
            .AsSingle();

        private void BindUIFactory() => Container
            .BindInterfacesTo<UIFactory>()
            .AsSingle()
            .CopyIntoAllSubContainers();

        private void BindLevelFactory() => Container
            .BindInterfacesTo<LevelFactory>()
            .AsSingle()
            .CopyIntoAllSubContainers();

        private void BindAssetContainerWithLoader()
        {
            var assetContainer = new AssetContainer();

            Container.Bind<IReadAssetContainer>()
                .FromInstance(assetContainer)
                .AsSingle();

            Container
                .BindInterfacesTo<AssetBundleLoader>()
                .AsSingle()
                .WithArguments(assetContainer);
        }

        private void BindLevelChangerService() => Container
            .BindInterfacesTo<LevelChanger>()
            .AsSingle();

        private void BindLevelDataProviderService() => Container
            .BindInterfacesTo<LevelSettingsProvider>()
            .AsSingle();
    }
}
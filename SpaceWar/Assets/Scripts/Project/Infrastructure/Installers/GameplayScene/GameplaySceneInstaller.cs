using Zenject;

namespace Project.Infrastructure.Installers.GameplayScene
{
    /// <summary>
    /// Installer to bind object or services from Gameplay scene.
    /// </summary>
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBootstrapper();
        }

        private void BindBootstrapper() => Container
            .BindInterfacesAndSelfTo<GameplaySceneBootstrapper>()
            .AsSingle()
            .NonLazy();
    }
}

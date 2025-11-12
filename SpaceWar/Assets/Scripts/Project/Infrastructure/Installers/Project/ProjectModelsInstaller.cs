using Project.Services.LevelServices.LevelChanger.Model;
using Zenject;

namespace Project.Infrastructure.Installers.Project
{
    public class ProjectModelsInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            BindGameplayModel();
        }

        private void BindGameplayModel() => Container
            .BindInterfacesAndSelfTo<LevelSetupModel>()
            .FromNew()
            .AsSingle();
    }
}
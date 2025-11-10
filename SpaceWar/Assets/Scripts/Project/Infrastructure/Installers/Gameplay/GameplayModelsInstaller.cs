using Project.Extensions;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Logic.Player.Data;
using Project.Logic.Player.Model;
using UnityEngine;
using Zenject;
using ILogger = Project.Infrastructure.Logger.ILogger;

namespace Project.Infrastructure.Installers.Gameplay
{
    public class GameplayModelsInstaller : MonoInstaller<GameplayModelsInstaller>, ILogger
    {
        public bool IsActiveLogger => true;
        public Color DefaultColor => Color.green;
        
        private IReadAssetContainer _assetContainer;

        [Inject]
        private void Construct(IReadAssetContainer assetContainer)
        {
            _assetContainer = assetContainer;
        }
        
        public override void InstallBindings()
        {
            this.Log("Start installing gameplay models...");
            
            BindPlayerModel();
            
            this.Log("Completed installing gameplay models.");
        }

        private void BindPlayerModel()
        {
            var config = _assetContainer.GetConfig<PlayerConfig>();
            var model = new PlayerModel(config);
            
            Container.BindInterfacesTo<PlayerModel>()
                .FromInstance(model)
                .AsSingle();
        }
    }
}
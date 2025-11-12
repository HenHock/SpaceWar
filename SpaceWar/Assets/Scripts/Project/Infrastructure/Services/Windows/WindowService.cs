using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Infrastructure.Services.Windows.Data;
using Project.Infrastructure.Services.Windows.View;
using Project.UI;
using Project.UI.Gameplay;
using Zenject;

namespace Project.Infrastructure.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly UIRoot _root;
        private readonly IInstantiator _instantiator;
        private readonly IReadAssetContainer _assetContainer;

        private BaseWindow _openedWindow;

        public WindowService(IInstantiator instantiator, IReadAssetContainer assetContainer, UIRoot root)
        {
            _root = root;
            _instantiator = instantiator;
            _assetContainer = assetContainer;
        }

        public void Open(WindowType window)
        {
            if (_openedWindow != null) 
                _openedWindow.Close();
            
            var windowPrefabs = _assetContainer.GetConfig<WindowsConfig>();

            BaseWindow prefab = windowPrefabs.GetWindowPrefab(window);
            CreateWindow(prefab);
        }
        
        private void CreateWindow(BaseWindow prefab)
        {
            _openedWindow = _instantiator.InstantiatePrefabForComponent<BaseWindow>(prefab, _root.transform);
            _openedWindow.Open();
        }
    }
}
using Project.Infrastructure.Services.Windows;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Infrastructure.Installers.Project
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot uiRoot;
        
        public override void InstallBindings()
        {
            BindWindowServices(uiRoot);
        }

        private void BindWindowServices(UIRoot root) => Container
            .BindInterfacesTo<WindowService>()
            .AsSingle()
            .WithArguments(root);
    }
}
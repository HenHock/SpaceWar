using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using JetBrains.Annotations;
using Project.Infrastructure.Services.Windows.View;
using UnityEngine;

namespace Project.Infrastructure.Services.Windows.Data
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Configs/WindowsConfig")]
    public class WindowsConfig : ScriptableObject
    {
        [SerializeField] 
        private SerializedDictionary<WindowType, BaseWindow> windows;

        [CanBeNull]
        public BaseWindow GetWindowPrefab(WindowType window)
        {
            var prefab = windows.GetValueOrDefault(window);
            Debug.Assert(prefab != null, $"Prefab not found for {window} window");
            return prefab;
        }
    }
}
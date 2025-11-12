using Project.Infrastructure.Services.Windows.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Infrastructure.Services.Windows.View
{
    [RequireComponent(typeof(Button))]
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private WindowType window;
        
        private IWindowService _windowService;

        [Inject]
        private void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }
        
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OpenWindow);
        }

        private void OpenWindow()
        {
            _windowService.Open(window);
        }
    }
}
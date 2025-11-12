using UnityEngine;

namespace Project.Infrastructure.Services.Windows.View
{
    public abstract class BaseWindow : MonoBehaviour
    {
        public abstract void Open();
        
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
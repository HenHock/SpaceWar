using UnityEngine;

namespace Project.Logic.Behavior.Renderer
{
    public class SpriteRendererViewBehavior : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void Initialize(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}
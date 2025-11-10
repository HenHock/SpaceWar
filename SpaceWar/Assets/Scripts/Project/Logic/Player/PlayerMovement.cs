using NaughtyAttributes;
using Project.Infrastructure.Services.AssetManagement.Data;
using Project.Infrastructure.Services.Input;
using Project.Logic.Player.Data;
using UnityEngine;
using Zenject;

namespace Project.Logic.Player
{
    public class PlayerMovement : MonoBehaviour
    {
#if UNITY_EDITOR
        [ShowNativeProperty] 
        private Vector2 MoveInput => _inputService?.MoveInput ?? Vector2.zero;
#endif
        
        private IInputService _inputService;
        
        private float _speed;
        private Camera _camera;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Initialize(float speed)
        {
            _speed = speed;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            var direction = new Vector3(MoveInput.x, MoveInput.y, 0);
            transform.Translate(_speed * Time.deltaTime * direction);

            if (_camera != null && _camera.orthographic)
            {
                var halfHeight = _camera.orthographicSize;
                var halfWidth = _camera.aspect * halfHeight;
                
                var limitedPosition = VerticalConstrain(transform.position, halfHeight);
                limitedPosition = HorizontalConstrain(limitedPosition, halfWidth);

                transform.position = limitedPosition;
            }
        }

        private Vector3 VerticalConstrain(Vector3 position, float halfHeight)
        {
            var yPadding = transform.localScale.y / 2f;
            var minY = _camera.transform.position.y - halfHeight + yPadding;
            var maxY = _camera.transform.position.y + halfHeight - yPadding;

            position.y = Mathf.Clamp(position.y, minY, maxY);
            
            return position;
        }

        private Vector3 HorizontalConstrain(Vector3 position, float halfWidth)
        {
            var leftThreshold = _camera.transform.position.x - halfWidth;
            var rightThreshold = _camera.transform.position.x + halfWidth;

            if (position.x < leftThreshold)
                position.x = rightThreshold;
            else if (position.x > rightThreshold) 
                position.x = leftThreshold;
            
            return position;
        }
    }
}
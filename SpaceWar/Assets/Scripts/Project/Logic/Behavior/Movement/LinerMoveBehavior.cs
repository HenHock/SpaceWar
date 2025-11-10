using UnityEngine;

namespace Project.Logic.Behavior.Movement
{
    public class LinerMoveBehavior : MonoBehaviour
    {
        private float _speed;
        private Vector2 _direction;

        public void Initialize(Vector2 direction, float speed)
        {
            _speed = speed;
            _direction = direction;
        }

        private void Update()
        {
            transform.Translate(_speed * Time.deltaTime * _direction);
        }
    }
}
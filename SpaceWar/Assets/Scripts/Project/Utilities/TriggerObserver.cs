using UniRx;
using UnityEngine;

namespace Project.Utilities
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerObserver : MonoBehaviour
    {
        public readonly Subject<Collider2D> OnTriggerEnter = new();
        public readonly Subject<Collider2D> OnTriggerStay = new();
        public readonly Subject<Collider2D> OnTriggerExit = new();

        private void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        public void SetTrigger(bool isEnabled)
        {
            GetComponent<Collider2D>().isTrigger = isEnabled;
        }

        private void OnTriggerEnter2D(Collider2D other) => OnTriggerEnter.OnNext(other);

        private void OnTriggerStay2D(Collider2D other) => OnTriggerStay.OnNext(other);

        private void OnTriggerExit2D(Collider2D other) => OnTriggerExit.OnNext(other);
    }
}
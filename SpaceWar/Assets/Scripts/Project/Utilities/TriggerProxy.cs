using Project.Logic.Behavior.Hittable;
using UnityEngine;

namespace Project.Utilities
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerProxy : MonoBehaviour
    {
        [SerializeField] private HittableBehavior hittableBehavior;
        
        public HittableBehavior Hittable => hittableBehavior;
    }
}
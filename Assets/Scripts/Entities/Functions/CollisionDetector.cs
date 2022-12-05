using System;
using UnityEngine;

namespace Entities.Functions
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionDetector : MonoBehaviour
    {
        public event Action<Collider2D> OnCollision;

        private void OnTriggerEnter2D(Collider2D collider) => OnCollision?.Invoke(collider);
    }
}

using System;
using UnityEngine;

namespace Entities.Functions
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionDetector : MonoBehaviour
    {
        public event Action OnCollision;
        
        private Collider2D _collider;

        private void Awake() => _collider = GetComponent<Collider2D>();
        private void OnCollisionEnter2D(Collision2D col) => OnCollision?.Invoke();
    }
}

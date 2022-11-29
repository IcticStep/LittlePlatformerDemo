using System;
using ClassExtensions;
using Entities.Data;
using UnityEngine;

namespace Entities.Functions
{
    [RequireComponent(typeof(Collider2D))]
    public class OffCameraDetector : MonoBehaviour
    {
        public event Action<Edge> OnEdgeLeft;

        private Vector2 _bounds;

        private void Awake() => _bounds = Camera.main.GetVisibleBounds();

        private void OnBecameInvisible()
        {
            var position = transform.position;

            if (Mathf.Abs(position.x) > _bounds.x)
                OnEdgeLeft?.Invoke(position.x > 0 ? Edge.Right : Edge.Left);
            if (Mathf.Abs(position.y) > _bounds.y)
                OnEdgeLeft?.Invoke(position.y > 0 ? Edge.Top : Edge.Bottom);
        }
    }
}
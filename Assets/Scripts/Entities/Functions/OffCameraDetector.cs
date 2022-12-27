using System;
using ClassExtensions;
using Entities.Data;
using UnityEngine;
using Zenject;

namespace Entities.Functions
{
    [RequireComponent(typeof(Collider2D))]
    public class OffCameraDetector : MonoBehaviour
    {
        public event Action<Edge> OnEdgeLeft;
        private Vector2 _bounds;

        [Inject]
        public void Construct(Camera newCamera) => _bounds = newCamera.GetVisibleBounds();

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
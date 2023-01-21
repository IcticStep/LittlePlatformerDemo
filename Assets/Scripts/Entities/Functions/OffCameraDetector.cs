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
        private Camera _camera;

        // [Inject]
        // public void Construct(Camera camera) => _camera = camera;

        private void OnBecameInvisible()
        {
            if(_camera is null)
                return;
            
            // var position = transform.position;
            // var bounds = _camera.GetVisibleBounds();

            // if (Mathf.Abs(position.x) > bounds.x)
            //     OnEdgeLeft?.Invoke(position.x > 0 ? Edge.Right : Edge.Left);
            // if (Mathf.Abs(position.y) > bounds.y)
            //     OnEdgeLeft?.Invoke(position.y > 0 ? Edge.Top : Edge.Bottom);
        }
    }
}
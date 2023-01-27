using Unity.VisualScripting;
using UnityEngine;

namespace ClassExtensions
{
    public static class CameraExtensions
    {
        public static Vector2 GetVisibleBounds(this Camera camera)
            => camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight));

        public static bool See(this Camera camera, GameObject gameObject)
        {
            Vector2 position = gameObject.transform.position;
            var bounds = camera.GetVisibleBounds();

            return position.x > -bounds.x 
                   && position.x < bounds.x 
                   && position.y > -bounds.y
                   && position.y < bounds.y;
        }
    }
}

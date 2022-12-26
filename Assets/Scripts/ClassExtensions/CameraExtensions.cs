using UnityEngine;

namespace ClassExtensions
{
    public static class CameraExtensions
    {
        public static Vector2 GetVisibleBounds(this Camera camera)
            => camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight));
    }
}

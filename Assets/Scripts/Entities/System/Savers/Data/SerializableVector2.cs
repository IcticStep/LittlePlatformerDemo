using UnityEngine;

namespace Entities.System.Savers.Data
{
    public class SerializableVector2
    {
        public float X;
        public float Y;

        public SerializableVector2(){}
        public SerializableVector2(float x, float y) => (X, Y) = (x, y);
        public SerializableVector2(Vector2 value) => (X, Y) = (value.x, value.y);
    }
}
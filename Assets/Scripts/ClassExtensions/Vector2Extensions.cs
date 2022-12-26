using UnityEngine;

namespace ClassExtensions
{
    public static class Vector2Extensions
    {
        public static Vector2 ReflectX(this Vector2 value) => new Vector2(-value.x, value.y);
        public static Vector2 ReflectY(this Vector2 value) => new Vector2(value.x, -value.y);
        public static Vector2 Reflect(this Vector2 value) => value.ReflectX().ReflectY();

        public static Vector2 WithAdjustedX(this Vector2 value, float xAddition) =>
            new Vector2(value.x + xAddition, value.y);
        public static Vector2 WithAdjustedY(this Vector2 value, float yAddition) =>
            new Vector2(value.x, value.y + yAddition);
    }
}
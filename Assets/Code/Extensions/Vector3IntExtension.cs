using UnityEngine;

namespace Code.Extensions
{
    public static class Vector3IntExtension
    {
        public static void Change(this Vector3Int vector)
        {
            vector.x = ChangeSideSize(vector.x);
            vector.y = ChangeSideSize(vector.y);
            vector.z = ChangeSideSize(vector.z);
        }

        public static int Multiply(this Vector3Int vector) =>
            vector.x * vector.y * vector.z;

        private static int ChangeSideSize(int size) =>
            size == 0 ? 1 : size;
    }
}
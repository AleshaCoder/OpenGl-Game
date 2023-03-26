using OpenTK;
using Assimp;

namespace Engine.Util
{
    public class Util
    {
        public static Vector3 ToVector3(Vector3D vector)
            => new Vector3(vector.X, vector.Y, vector.Z);

        public static Vector3[] ToVector3Array(Vector3D[] vector)
        {
            Vector3[] result = new Vector3[vector.Length];

            for (int i = 0; i < vector.Length; i++)
                result[i] = ToVector3(vector[i]);

            return result;
        }
    }
}

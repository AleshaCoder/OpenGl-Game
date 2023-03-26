using System;
using OpenTK;

namespace Engine.Util
{
    public class MathUtil
    {
        public static Quaternion CreateFromEuler(Vector3 euler)
        {
            Quaternion quaternion = Quaternion.Identity;

            float c1 = (float)Math.Cos(euler.X / 2);
            float s1 = (float)Math.Sin(euler.X / 2);
            float c2 = (float)Math.Cos(euler.Y / 2);
            float s2 = (float)Math.Sin(euler.Y / 2);
            float c3 = (float)Math.Cos(euler.Z / 2);
            float s3 = (float)Math.Sin(euler.Z / 2);
            float c1c2 = c1 * c2;
            float s1s2 = s1 * s2;
            quaternion.W = c1c2 * c3 - s1s2 * s3;
            quaternion.X = c1c2 * s3 + s1s2 * c3;
            quaternion.Y = s1 * c2 * c3 + c1 * s2 * s3;
            quaternion.Z = c1 * s2 * c3 - s1 * c2 * s3;

            return quaternion;
        }

        public static Vector3 EulerFromQuaternion(Quaternion quaternion)
        {
            Vector3 euler = Vector3.Zero;

            float sqw = quaternion.W * quaternion.W;
            float sqx = quaternion.X * quaternion.X;
            float sqy = quaternion.Y * quaternion.Y;
            float sqz = quaternion.Z * quaternion.Z;
            float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            float test = quaternion.X * quaternion.Y + quaternion.Z * quaternion.W;

            if (test > 0.499 * unit)
            { // singularity at north pole
                euler.X = 2 * (float)Math.Atan2(quaternion.X, quaternion.W);
                euler.Y = (float)Math.PI / 2;
                euler.Z = 0;
                return euler;
            }

            if (test < -0.499 * unit)
            { // singularity at south pole
                euler.X = -2 * (float)Math.Atan2(quaternion.X, quaternion.W);
                euler.Y = (float)-Math.PI / 2;
                euler.Z = 0;
                return euler;
            }

            euler.X = (float)Math.Atan2(2 * quaternion.Y * quaternion.W - 2 * quaternion.X * quaternion.Z, sqx - sqy - sqz + sqw);
            euler.Y = (float)Math.Asin(2 * test / unit);
            euler.Z = (float)Math.Atan2(2 * quaternion.X * quaternion.W - 2 * quaternion.Y * quaternion.Z, -sqx + sqy - sqz + sqw);

            return euler;
        }

        public static Quaternion CreateFromMatrix(ref Matrix4 matrix)
        {
            Quaternion quaternion;

            float trace = 1 + matrix.M11 + matrix.M22 + matrix.M33;
            float S = 0;
            float X = 0;
            float Y = 0;
            float Z = 0;
            float W = 0;

            if (trace > 0.0000001)
            {
                S = (float)Math.Sqrt(trace) * 2;
                X = (matrix.M23 - matrix.M32) / S;
                Y = (matrix.M31 - matrix.M13) / S;
                Z = (matrix.M12 - matrix.M21) / S;
                W = 0.25f * S;
            }
            else
            {
                if (matrix.M11 > matrix.M22 && matrix.M11 > matrix.M33)
                {
                    // Column 0: 
                    S = (float)Math.Sqrt(1.0 + matrix.M11 - matrix.M22 - matrix.M33) * 2;
                    X = 0.25f * S;
                    Y = (matrix.M12 + matrix.M21) / S;
                    Z = (matrix.M31 + matrix.M13) / S;
                    W = (matrix.M23 - matrix.M32) / S;
                }
                else if (matrix.M22 > matrix.M33)
                {
                    // Column 1: 
                    S = (float)Math.Sqrt(1.0 + matrix.M22 - matrix.M11 - matrix.M33) * 2;
                    X = (matrix.M12 + matrix.M21) / S;
                    Y = 0.25f * S;
                    Z = (matrix.M23 + matrix.M32) / S;
                    W = (matrix.M31 - matrix.M13) / S;
                }
                else
                {
                    // Column 2:
                    S = (float)Math.Sqrt(1.0 + matrix.M33 - matrix.M11 - matrix.M22) * 2;
                    X = (matrix.M31 + matrix.M13) / S;
                    Y = (matrix.M23 + matrix.M32) / S;
                    Z = 0.25f * S;
                    W = (matrix.M12 - matrix.M21) / S;
                }
            }
            quaternion = new Quaternion(X, Y, Z, W);
            return quaternion;
        }
    }
}

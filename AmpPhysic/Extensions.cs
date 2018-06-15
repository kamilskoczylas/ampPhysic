using System.Windows.Media.Media3D;
using System;

namespace AmpPhysic.Extensions
{
    public static class Vector3DExtensions
    {
        public static Vector3D Zero(this Vector3D vector3D)
        {
            return new Vector3D(0, 0, 0);
        }

        public static Vector3D Abs(this Vector3D vector3D)
        {
            return new Vector3D(
                    Math.Abs(vector3D.X),
                    Math.Abs(vector3D.Y),
                    Math.Abs(vector3D.Z)
                  );
        }
    }
    
}
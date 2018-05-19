using System.Windows.Media.Media3D;

namespace AmpPhysic
{
    public static class Vector3DExtensions
    {
        public static Vector3D Zero(this Vector3D vector3D)
        {
            return new Vector3D(0, 0, 0);
        }
    }
    
}
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace AmpPhysic.Collision
{
    interface ICollisableMesh
    {
        List<ICollisable> RegisterCollisableAt();
    }

    public class CollistionDetails
    {
        public Point3D intersectionPoint;
        public Vector3D hitforcevector;
        public Vector3D PlaneNormal;
        public Vector3D DistanceFromCenterOfMass;

        public CollistionDetails()
        {
            hitforcevector = new Vector3D { X = 0, Y = 0, Z = 0 };
            PlaneNormal = new Vector3D { X = 0, Y = 0, Z = 0 }; 
        }
    }

    public interface ICollisable
    {
        bool IsIn(Point3D l0, Vector3D l, out CollistionDetails CollisionFound);
        void Hit(Point3D intersectionPoint, Vector3D hitforcevector);
    }
}

using System.Windows.Media.Media3D;
using AmpPhysic.Interaction;

namespace AmpPhysic.Collision
{
    public class CollisionResponse
    {
        public Vector3D CollisionForceVector { get; private set; }
        public Vector3D PlaneNormal { get; private set; }
        public Point3D CollisionPoint3D { get; private set; }
        public ICollisable ObjectColliding;
        public Friction FrictionForce;
        public float CollisionDeltaTime;

        public CollisionResponse(CollistionDetails CollistionDetail)
        {
            this.CollisionForceVector = CollistionDetail.hitforcevector;
            this.CollisionPoint3D = CollistionDetail.intersectionPoint;
            this.PlaneNormal = CollistionDetail.PlaneNormal;

            FrictionForce = new Friction(
                    CollistionDetail.DistanceFromCenterOfMass,
                    CollisionForceVector
                );
        }

        public CollisionResponse(float deltaTime, Point3D IntersectionPoint)
        {
            this.CollisionDeltaTime = deltaTime;
            CollisionPoint3D = IntersectionPoint;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using AmpPhysic.Extensions;

namespace AmpPhysic.Collision.Combinations
{
    class SpherePlaneCollision
    {

        Point3D SphereCenter;

        public SpherePlaneCollision()
        {
            SphereCenter = new Point3D(0, 0, 0);
        }

        /**
         * <summary>
         * Sphere must be the first, not moving object in center 
         * just the second object can move
         * </summary>
         */
        public CollisionResponse CheckSimplified(CollisionSimplifiedScenario scenario)
        {
            CollisionResponse test = null;


            // 1. first escape scenario -point is too far to collide with sphere
            //      it may happen if (|point center| - R) ^2 <= movement ^2

            double R = scenario.ColliderShape1.CalculateMaximumRadius;
             
            Vector3D totalDisplacement;
            totalDisplacement = scenario.Linear.Velocity * scenario.Linear.DeltaTime;            

            Vector3D AnyPointOnPlaneToSphereCenter = SphereCenter - scenario.Linear.StartingPosition;
            Vector3D absoluteAnyPointOnPlaneToSphereCenter = AnyPointOnPlaneToSphereCenter.Abs();

            absoluteAnyPointOnPlaneToSphereCenter.X = Math.Max(0, absoluteAnyPointOnPlaneToSphereCenter.X - R);
            absoluteAnyPointOnPlaneToSphereCenter.Y = Math.Max(0, absoluteAnyPointOnPlaneToSphereCenter.Y - R);
            absoluteAnyPointOnPlaneToSphereCenter.Z = Math.Max(0, absoluteAnyPointOnPlaneToSphereCenter.Z - R);

            if (absoluteAnyPointOnPlaneToSphereCenter.LengthSquared > totalDisplacement.LengthSquared)
            {
                return test;
            }

            // 2. check direction of displacement. If point does not move towards the center
            //      escape, as can never collide with sphere
            Vector3D PointTowardsSphereCenter = AnyPointOnPlaneToSphereCenter;
            Vector3D VelocityDirectionNormalized = new Vector3D(totalDisplacement.X, totalDisplacement.Y, totalDisplacement.Z);
            VelocityDirectionNormalized.Normalize();

            double SphereCenterToPlaneMinimimDistance = Vector3D.DotProduct(VelocityDirectionNormalized, PointTowardsSphereCenter);

            if (SphereCenterToPlaneMinimimDistance < 0)
            {
                return test;
            }

            // too far to get to the plane
            double SphereEdgeToPlaneMinimimDistance = SphereCenterToPlaneMinimimDistance - R;
            if (SphereEdgeToPlaneMinimimDistance * SphereEdgeToPlaneMinimimDistance > totalDisplacement.LengthSquared)
            {
                return test;
            }

            // 3. Check calculation


            double lo = Vector3D.DotProduct(VelocityDirectionNormalized, PointTowardsSphereCenter);
            Vector3D TowardCenterAbsolute = PointTowardsSphereCenter.Abs();
            double sqareRootValue = lo * lo - TowardCenterAbsolute.LengthSquared + R * R;

            // 3a. escape condition
            if (sqareRootValue < 0)
            {
                return test;
            }

            double squareCalculatedValue = Math.Sqrt(sqareRootValue);
            double d1 = Math.Abs(-lo + squareCalculatedValue);
            double d2 = Math.Abs(-lo - squareCalculatedValue);
            double d = Math.Min(d1, d2);

            Vector3D FastestCollisionLength = d * VelocityDirectionNormalized;

            // the last escape, collision would happen, but in next frames
            if (FastestCollisionLength.LengthSquared > totalDisplacement.LengthSquared)
            {
                return test;
            }

            test = new CollisionResponse(
                        (float) d,
                        scenario.Linear.StartingPosition + FastestCollisionLength
                      );

            return test;
        }
    }
}

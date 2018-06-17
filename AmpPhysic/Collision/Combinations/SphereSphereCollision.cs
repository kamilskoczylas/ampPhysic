using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using AmpPhysic.Extensions;

namespace AmpPhysic.Collision.Combinations
{
    class SphereSphereCollision
    {
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

            double R1 = scenario.ColliderShape1.CalculateMaximumRadius;
            double R2 = scenario.ColliderShape2.CalculateMaximumRadius;

            Vector3D totalDisplacement;
            totalDisplacement = scenario.Linear.Velocity * scenario.Linear.DeltaTime;

            Vector3D DistanceToSphereCenter = new Point3D(0, 0, 0) - scenario.Linear.StartingPosition;
            Vector3D absoluteminimumDistanceToSphere = DistanceToSphereCenter.Abs();

            absoluteminimumDistanceToSphere.X = Math.Max(0, absoluteminimumDistanceToSphere.X - R1 - R2);
            absoluteminimumDistanceToSphere.Y = Math.Max(0, absoluteminimumDistanceToSphere.Y - R1 - R2);
            absoluteminimumDistanceToSphere.Z = Math.Max(0, absoluteminimumDistanceToSphere.Z - R1 - R2);

            if (absoluteminimumDistanceToSphere.LengthSquared > totalDisplacement.LengthSquared)
            {
                return test;
            }

            // 2. check direction of displacement. If point does not move towards the center
            //      escape, as can never collide with sphere

            Vector3D VelocityDirectionNormalized = new Vector3D(totalDisplacement.X, totalDisplacement.Y, totalDisplacement.Z);
            VelocityDirectionNormalized.Normalize();

            Vector3D PointTowardsSphereCenter = DistanceToSphereCenter;
            double displacementDirection = Vector3D.DotProduct(VelocityDirectionNormalized, PointTowardsSphereCenter);

            // does not move towards the sphere center
            if (displacementDirection < 0)
            {
                return test;
            }

            // will be close but too far to sphere            
            double RadiusSum = (R1 + R2);
            double RadiusSumSquared = RadiusSum * RadiusSum;

            double SquaredMinimumDistanceEverOnVelocityVector = DistanceToSphereCenter.LengthSquared - (displacementDirection * displacementDirection);

            if (SquaredMinimumDistanceEverOnVelocityVector > RadiusSumSquared)
            {
                return test;
            }

            //double MinimumDistanceEverOnVelocityVector = Math.Sqrt(SquaredMinimumDistanceEverOnVelocityVector);
            double MinimumCollisionLength = Math.Sqrt(RadiusSumSquared - SquaredMinimumDistanceEverOnVelocityVector);
            Vector3D MinimumCollisionVector = VelocityDirectionNormalized * (displacementDirection - MinimumCollisionLength); 
            

            // the last escape, collision would happen, but in next frames
            if (MinimumCollisionVector.LengthSquared > totalDisplacement.LengthSquared)
            {
                return test;
            }

            double k;
            if (scenario.Linear.Velocity.X != 0)
                k = MinimumCollisionVector.X / totalDisplacement.X;
            else
            if (scenario.Linear.Velocity.Y != 0)
                k = MinimumCollisionVector.Y / totalDisplacement.Y;
            else
            if (scenario.Linear.Velocity.Z != 0)
                k = MinimumCollisionVector.Z / totalDisplacement.Z;
            else
                k = 0;

            // spheres were intersecting before test
            if (k <= 0)
                return test;

            double deltaTime = k * scenario.Linear.DeltaTime;

            test = new CollisionResponse(
                        (float)deltaTime,
                        scenario.Linear.StartingPosition + MinimumCollisionVector
                      );

            return test;
        }
    }
}

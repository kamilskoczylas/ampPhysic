using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using AmpPhysic.Extensions;

namespace AmpPhysic.Collision.Combinations
{
    class SpherePointCollision
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

            double R = scenario.ColliderShape1.CalculateMaximumRadius;

            Vector3D totalDisplacement;
            totalDisplacement = scenario.Linear.Velocity * scenario.Linear.DeltaTime;            

            Vector3D minimumDistanceToSphere = scenario.Linear.StartingPosition - new Point3D(0, 0, 0);
            Vector3D absoluteminimumDistanceToSphere = minimumDistanceToSphere.Abs();

            absoluteminimumDistanceToSphere = absoluteminimumDistanceToSphere - new Vector3D(R, R, R);

            if (minimumDistanceToSphere.LengthSquared > totalDisplacement.LengthSquared)
            {
                return test;
            }

            // 2. check direction of displacement. If point does not move towards the center
            //      escape, as can never collide with sphere
            Vector3D PointTowardsSphereCenter = minimumDistanceToSphere;
            double displacementDirection = Vector3D.DotProduct(PointTowardsSphereCenter, totalDisplacement);

            if (displacementDirection < 0)
            {
                return test;
            }

            // 3. Check calculation
            Vector3D VelocityDirectionNormalized = new Vector3D(totalDisplacement.X, totalDisplacement.Y, totalDisplacement.Z);
            VelocityDirectionNormalized.Normalize();

            double lo = Vector3D.DotProduct(VelocityDirectionNormalized, PointTowardsSphereCenter);
            double sqareRootValue = lo * lo - PointTowardsSphereCenter.Abs().LengthSquared + R * R;

            // 3a. escape condition
            if (sqareRootValue < 0)
            {
                return test;
            }

            double squareCalculatedValue = Math.Sqrt(sqareRootValue);
            double d1 = -lo + squareCalculatedValue;
            double d2 = -lo - squareCalculatedValue;
            double d = Math.Min(d1, d2);

            test = new CollisionResponse(
                        (float) d,
                        scenario.Linear.StartingPosition + d * totalDisplacement
                      );

            return test;
        }
    }
}

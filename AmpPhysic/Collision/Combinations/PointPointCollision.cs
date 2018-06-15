using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AmpPhysic.Collision.Combinations
{
    class PointPointCollision
    {
        public CollisionResponse CheckSimplified(CollisionSimplifiedScenario scenario)
        {
            CollisionResponse test = null;

            /**
             * k: x/a = y/b = z/c 
             * for a, b, c != 0
             */

            if (scenario.Linear.Velocity.X == 0 && scenario.Linear.StartingPosition.X != 0
                || scenario.Linear.Velocity.Y == 0 && scenario.Linear.StartingPosition.Y != 0
                || scenario.Linear.Velocity.Z == 0 && scenario.Linear.StartingPosition.Z != 0)
            {            
                    return test;
            }

            double k;
            if (scenario.Linear.Velocity.X != 0)
                k = scenario.Linear.StartingPosition.X / scenario.Linear.Velocity.X;
            else
            if (scenario.Linear.Velocity.Y != 0)
                k = scenario.Linear.StartingPosition.Y / scenario.Linear.Velocity.Y;
            else
            if (scenario.Linear.Velocity.Z != 0)
                k = scenario.Linear.StartingPosition.Z / scenario.Linear.Velocity.Z;
            else
                k = 0;

            // collision cannot occur right on start on just after collision
            if (k == 0)
                return test;

            if (scenario.Linear.StartingPosition.X == k * scenario.Linear.Velocity.X
                || scenario.Linear.StartingPosition.Y == k * scenario.Linear.Velocity.Y
                || scenario.Linear.StartingPosition.Z == k * scenario.Linear.Velocity.Z
                )
            // there is collision on this time
            if (Math.Abs(k) < scenario.Linear.DeltaTime)
            {
                test = new CollisionResponse(
                    (float) Math.Abs(k), 
                    scenario.Linear.StartingPosition + scenario.Linear.Velocity * k
                    );
            }            

            return test;
        }
    }
}

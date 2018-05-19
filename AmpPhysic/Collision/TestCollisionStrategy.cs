using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmpPhysic.Collision.Shapes;
using AmpPhysic.Collision.Combinations;

namespace AmpPhysic.Collision
{
    class TestCollisionStrategy
    {

        PointPointCollision Point_Point;

        public TestCollisionStrategy()
        {
            Point_Point = new PointPointCollision();
        }

        public CollisionResponse Test(CollisionSimplifiedScenario Scenario)
        {

            switch (Scenario.ColliderShape1.GetType().Name)
            {
                case "PointColliderShape":
                    switch (Scenario.ColliderShape2.GetType().Name)
                    {
                        case "PointColliderShape":
                            return Point_Point.CheckSimplified(Scenario);                            
                    }
                break;
            }
            

            return null;
        }
    }
}

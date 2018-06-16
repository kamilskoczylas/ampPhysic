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
        SpherePointCollision Sphere_Point;
        SphereSphereCollision Sphere_Sphere;

        public TestCollisionStrategy()
        {
            Point_Point = new PointPointCollision();
            Sphere_Point = new SpherePointCollision();
            Sphere_Sphere = new SphereSphereCollision();
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

                            /* Don't activate;only sphere point will be handled
                             * case "SphereColliderShape":
                                return Sphere_Point.CheckSimplified(Scenario);*/
                    }
                    break;

                case "SphereColliderShape":
                    switch (Scenario.ColliderShape2.GetType().Name)
                    {
                        case "PointColliderShape":
                            return Sphere_Point.CheckSimplified(Scenario);

                        case "SphereColliderShape":
                            return Sphere_Sphere.CheckSimplified(Scenario);
                    }
                    break;
            }
            

            return null;
        }
    }
}

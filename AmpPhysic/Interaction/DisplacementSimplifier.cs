using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using AmpPhysic;
using AmpPhysic.Collision;

namespace AmpPhysic.Interaction
{
    /**
     * <summary>
     * This class tries to simplify displacements for two different objects
     * example:
     * ObjectA moves lineary forward with velocity A in direction V
     * ObjectB moves lineary forward with velocity A in direction V
     * Because they never collide, their displacement against themseves is nothing
     * </summary>
     */
    public class DisplacementSimplifier
    {
        private Vector3D Zero = new Vector3D(0, 0, 0);
        private Point3D Center = new Point3D(0, 0, 0);

        public DisplacementSimplifier()
        {
        }

        public CollisionSimplifiedScenario Simplify(
            List<IDisplacement> objectA_List,
            List<IDisplacement> objectB_List
            )
        {
            if (objectA_List.Count == 1 && objectB_List.Count == 1)
            {
                if (objectA_List[0].GetType().Name == objectB_List[0].GetType().Name)
                {
                    if (objectA_List[0].GetType() == typeof(LinearDisplacement))
                    {
                        return 
                            Simplify(
                                objectA_List[0] as LinearDisplacement, 
                                objectB_List[0] as LinearDisplacement
                                );
                    }
                }
            }

            return null;
        }

        public CollisionSimplifiedScenario Simplify(
            LinearDisplacement objectA, 
            LinearDisplacement objectB
        )
        {
            Point3D SecondObjectCenter = Point3D.Add(new Point3D(0, 0, 0), objectB.StartingPosition - objectA.StartingPosition);

            LinearDisplacement EffectiveLinearDisplacement =
                new LinearDisplacement
                (
                    objectB.Velocity - objectA.Velocity,
                    SecondObjectCenter,
                    objectA.DeltaTime
                );

            if (
                EffectiveLinearDisplacement.Velocity == Zero
                && SecondObjectCenter != Center
                )
            {
                return null;

            } else
            {
                CollisionSimplifiedScenario simplifiedScenario =
                new CollisionSimplifiedScenario(
                        objectA.PhysicObject.GetColliderShape(),
                        objectB.PhysicObject.GetColliderShape(),
                        EffectiveLinearDisplacement
                    );

                return simplifiedScenario;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
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
        public CollisionSimplifiedScenario Simplify(
            LinearDisplacement objectA, 
            LinearDisplacement objectB
        )
        {
            Point3D SecondObjectCenter = Point3D.Add(new Point3D(0, 0, 0), objectB.StartingPosition - objectA.StartingPosition);

            LinearDisplacement EffectiveLinearDisplacement =
                new LinearDisplacement
                (
                    objectA.Velocity - objectB.Velocity,
                    SecondObjectCenter,
                    objectA.DeltaTime
                );

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

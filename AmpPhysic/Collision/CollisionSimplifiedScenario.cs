using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmpPhysic.Interaction;

namespace AmpPhysic.Collision
{
    /**
     * <summary>
     * This class describes simplified collision scenario without specyfing how colliders
     * should behave.
     * For example when two objects move forward, we can stop one of them and calculate collision 
     * for simpler scenario
     * </summary>
     */
    public class CollisionSimplifiedScenario
    {

        public CollisionSimplifiedScenario(ColliderShape colliderShape1, ColliderShape colliderShape2, LinearDisplacement linear)
        {
            ColliderShape1 = colliderShape1;
            ColliderShape2 = colliderShape2;
            Linear = linear;
        }

        public ColliderShape ColliderShape1 { get; private set; }
        public ColliderShape ColliderShape2 { get; private set; }
        public LinearDisplacement Linear { get; private set; }
    }

    
}

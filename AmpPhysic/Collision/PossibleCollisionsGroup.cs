using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmpPhysic.Collision
{
    class PossibleCollisionsGroup
    {
        private CollisableArea AreaCovered;
        private List<CollisableArea> PossibleCollisions;

        public PossibleCollisionsGroup(CollisableArea Area)
        {
            AreaCovered = Area;
            PossibleCollisions = new List<CollisableArea>();
            PossibleCollisions.Add(Area);
        }

        public List<CollidingObjects> GetPossibleCollidingObjects()
        {
            var tmp = new List<CollidingObjects>();

            foreach (var testing_collision in PossibleCollisions)
                foreach (var cross_collision in PossibleCollisions)
                if (testing_collision != cross_collision)
                {
                        tmp.Add(
                                new CollidingObjects(
                                    testing_collision.LinkedEntity as KinematicBody, 
                                    cross_collision.LinkedEntity as KinematicBody
                                )
                        );
                }
            return tmp; 
        }

        private void Add(CollisableArea PossibleCollidingObject)
        {
            AreaCovered.Expand(PossibleCollidingObject);
            PossibleCollisions.Add(PossibleCollidingObject);
            IsActive = true;
        }
        
        public bool IsActive { get; private set; }

        public bool TestAndMaybeAdd(CollisableArea TestingCollidingObject)
        {
            if (AreaCovered.IsCollidingWith(TestingCollidingObject))
            {
                Add(TestingCollidingObject);
                return true;
            }

            return false;
        }
    }
}

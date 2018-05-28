using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmpPhysic.Collision
{
    /*
     * This class checks and groups possible collisions
     * operates on displacement sections
     */
    class CollisionEfficencyBooster
    {
        public CollisionEfficencyBooster()
        {
        }

        public List<PossibleCollisionsGroup> CheckAll(List<GameObject> list)
        {
            List<PossibleCollisionsGroup> results = new List<PossibleCollisionsGroup>();
            foreach (var element in list)
            {
                foreach (var kinematicBody in element.GetKinematics())
                {
                    if (results.Count > 0)
                    {
                        foreach (var section in results)
                        {
                            section.TestAndMaybeAdd(
                                    kinematicBody.GetCollisionArea()
                                );
                        }
                    }
                    else
                    {
                        results.Add(
                            new PossibleCollisionsGroup(
                                kinematicBody.GetCollisionArea()
                                )
                            );
                    }
                }
            }

            // return only posiible collisions, instead of all objects
            return results.Where( x => x.IsActive ).ToList();
        }
    }
}

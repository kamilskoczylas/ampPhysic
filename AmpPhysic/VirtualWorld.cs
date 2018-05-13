using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmpPhysic.Collision;

namespace AmpPhysic
{
    public class VirtualWorld
    {
        private List<GameObject> GameObjects;
        private List<IPhysic> PhysicObjects;

        private int PRECISION_3D_PER_ONE_UNIT_SIZE = 12; // min. to look nice 12

        private CollisionResponser Responser;

        public VirtualWorld(int MeshPrecision = 12)
        {
            GameObjects = new List<GameObject>();
            PhysicObjects = new List<IPhysic>();

            Responser = new CollisionResponser();
            PRECISION_3D_PER_ONE_UNIT_SIZE = MeshPrecision;
        }

        public CollisionResponser GetResponser()
        {
            return this.Responser;
        }


        /*public void AddObject(IControlledObject Object)
        {
            ControlledObjects.Add(Object);
        }
        */

        public void AddObject(GameObject anObject)
        {
            GameObjects.Add(anObject);
        }

        private CollisionResponse TryToMove()
        {
            return null;
        }

        public void Animate(float deltaTime)
        {

            // Calculate forces which affects velocity
            /*foreach (var ControlledObject in GameObjects.Where(x => x.CalculatePhysicYesNo))
            {
                ControlledObject.Animate(deltaTime);
            }*/

            // Generate movements
            foreach (var ControlledObject in GameObjects.Where( x => x.CalculatePhysicYesNo))
            {
                ControlledObject.Animate(deltaTime);
            }

            // Generate the fastest collision
            /*float fastestCollisionTime = deltaTime;
            foreach (var ControlledObject in GameObjects.Where(x => x.CalculateCollisionsYesNo))
            {
                ControlledObject.Animate(deltaTime);
            }

            // Recursively do the same
            deltaTime = deltaTime - fastestCollisionTime;
            if (deltaTime > 0)
                Animate(deltaTime);
                */
        }
    }
}

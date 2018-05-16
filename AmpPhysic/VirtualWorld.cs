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

        private int PRECISION_3D_PER_ONE_UNIT_SIZE = 12; // min. to look nice 12

        private CollisionResponser Responser;

        public VirtualWorld(int MeshPrecision = 12)
        {
            GameObjects = new List<GameObject>();

            Responser = new CollisionResponser();
            PRECISION_3D_PER_ONE_UNIT_SIZE = MeshPrecision;
        }

        public CollisionResponser GetResponser()
        {
            return this.Responser;
        }
        

        public void AddObject(GameObject anObject)
        {
            GameObjects.Add(anObject);
        }

        private CollisionResponse TryToMove()
        {
            return null;
        }

        private IEnumerable<GameObject> GetPhysicGameObjects()
        {
            // TODO: Speed up with cache in the future
            return GameObjects.Where(x => x.CalculatePhysicYesNo);
        }

        private IEnumerable<GameObject> GetCollisableGameObjects()
        {
            // TODO: Speed up with cache in the future
            return GameObjects.Where(x => x.CalculateCollisionsYesNo);
        }

        /**
         * <summary>
         * This is a separated from the Animate, because Animate executes recursively and is time consumpting
         * AnimateGravity can be executed once other calcultion in the frame are finished
         * </summary>
         */
        private void AnimateGravity()
        {

        }

        /**
         * <summary>
         * This is the main VirtualWorld procedure, which starts to moving all the required objects
         * Tries to find the first collision or no collision and animates the objects
         * </summary>
         */
        public void Animate(float deltaTime)
        {            

            // Generate movements
            foreach (var ControlledObject in GetPhysicGameObjects())
            {                
                ControlledObject.CalculateDisplacement(deltaTime);
            }

            // Generate the fastest collision
            float fastestCollisionTime = deltaTime;
            foreach (var ControlledObject in GetCollisableGameObjects())
            {
                //ControlledObject.Animate(deltaTime);
            }

            // commit the first collision
            // ...


            // When we have collision, animate only to the first one 
            // and recursively do the same
            if (deltaTime != fastestCollisionTime)
            {
                // Commit the movement
                foreach (var ControlledObject in GetPhysicGameObjects())
                {
                    ControlledObject.CommitDisplacementPartially(fastestCollisionTime, deltaTime);
                }

                Animate(deltaTime);

            }            

            // Commit the movement
            foreach (var ControlledObject in GetPhysicGameObjects())
            {
                ControlledObject.CommitDisplacement();
            }

            AnimateGravity();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmpPhysic.Collision;

namespace AmpPhysic
{
    public class VirtualWorld
    {
        private Dictionary<string, GameObject> GameObjects;

        private int PRECISION_3D_PER_ONE_UNIT_SIZE = 12; // min. to look nice 12

        private CollisionResponser CollisionResponser;        

        public VirtualWorld(int MeshPrecision = 12)
        {
            GameObjects = new Dictionary<string, GameObject>();

            CollisionResponser = new CollisionResponser();            
            PRECISION_3D_PER_ONE_UNIT_SIZE = MeshPrecision;
        }

        public CollisionResponser GetResponser()
        {
            return this.CollisionResponser;
        }
        
        public void RemoveObjects()
        {
            GameObjects.Clear();
        }

        public bool TryGetObject(string ObjectName, out GameObject gameObject)
        {            
            return GameObjects.TryGetValue(ObjectName, out gameObject);
        }

        public void AddObject(GameObject anObject, string ObjectName = "body")
        {
            // auto naming
            if (ObjectName == "body")
            {                
                for (char Letter = 'A'; Letter <= 'Z' ; Letter++)
                {
                    if (!GameObjects.ContainsKey(ObjectName + Letter))
                    {
                        ObjectName = ObjectName + Letter;
                        break;
                    }                        
                }
            }

            if (GameObjects.ContainsKey(ObjectName))
            {
                throw new InvalidOperationException(
                    String.Format(
                        "Cannot add the object named: {0} as it already exists", ObjectName
                        )
                    );
            }

            GameObjects.Add(ObjectName, anObject);
        }

        private CollisionResponse TryToMove()
        {
            return null;
        }

        private IEnumerable<GameObject> GetPhysicGameObjects()
        {
            // TODO: Speed up with cache in the future
            return GameObjects.Where(x => x.Value.CalculatePhysicYesNo).Select(x => x.Value);
        }

        private IEnumerable<GameObject> GetCollisableGameObjects()
        {
            // TODO: Speed up with cache in the future
            return GameObjects.Where(x => x.Value.CalculateCollisionsYesNo).Select(x => x.Value);
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
            var fastestCollision = CollisionResponser.DoTheFastestCollision(
                    GetCollisableGameObjects().Where(x => !x.CurrentlyStaticYesNo).ToList()
                );

            // commit the first collision
            // ...


            // When we have collision, animate only to the first one 
            // and recursively do the same
            if (fastestCollision != null)
            {
                // Commit the movement
                foreach (var ControlledObject in GetPhysicGameObjects())
                {
                    ControlledObject.CommitDisplacementPartially(fastestCollision.CollisionDeltaTime);
                    
                    // TODO: should recalculate displacements for both collisioned objects
                }

                Animate(deltaTime - fastestCollision.CollisionDeltaTime);

            }
            else // no collision
            {                
                // Commit the movement
                foreach (var ControlledObject in GetPhysicGameObjects())
                {
                    ControlledObject.CommitDisplacement();
                }

                AnimateGravity();
            }
        }
    }
}

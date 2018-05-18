using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmpPhysic.Collision
{
    public class FastestCollision
    {
        public FastestCollision(CollisionResponse CollisionResponse, GameObject GameObject)
        {
            this.CollisionResponse = CollisionResponse;
            this.GameObject = GameObject;
        }

        public CollisionResponse CollisionResponse { get; private set; }
        public GameObject GameObject { get; private set; }

        public float CollisionDeltaTime;
        
    }
}

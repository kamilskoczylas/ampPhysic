using System;
using AmpPhysic.Collision;

public class CollisionEventArgs : EventArgs
{

    public CollisionResponse CollisionResponse { get; private set; }

    public CollisionEventArgs(CollisionResponse collisionResponse)
    {
        CollisionResponse = collisionResponse;
    }
 
}

public delegate void ObjectCollisionHandler(object sender, CollisionEventArgs e);
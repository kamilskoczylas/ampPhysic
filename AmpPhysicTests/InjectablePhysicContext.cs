﻿using AmpPhysic;
using AmpPhysic.Collision.Shapes;

namespace AmpPhysicTests
{
    public class InjectablePhysicContext
    {
        public InjectablePhysicContext()
        {
            World = new VirtualWorld();
        }

        public GameObject CreateGameObject(float mass = 1, string ObjectName = "body")
        {
            var RidgidBody = new GameObject(
                new KinematicBody(mass)
                );

            World.AddObject(RidgidBody, ObjectName);
            return RidgidBody;
        }

        public GameObject CreateSphere(float mass = 1, float radius = 1, string ObjectName = "body")
        {
            var RidgidBody = new GameObject(
                new KinematicBody(mass, new SphereColliderShape(radius))
                );

            World.AddObject(RidgidBody, ObjectName);
            return RidgidBody;
        }

        public VirtualWorld World { get; private set; }
    }
}

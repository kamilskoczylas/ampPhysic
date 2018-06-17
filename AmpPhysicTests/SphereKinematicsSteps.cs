using AmpPhysic;
using System;
using TechTalk.SpecFlow;

namespace AmpPhysicTests
{
    [Binding]
    public class SphereKinematicsSteps
    {
        InjectablePhysicContext Context;

        private GameObject CarA;
        private float CarMass;
        private double EngineNewtonPower;

        public SphereKinematicsSteps(InjectablePhysicContext injectablePhysicContext)
        {
            Context = injectablePhysicContext;
        }

        [Given(@"A sphere in position \((.*), (.*), (.*)\) and radius (.*) and a sphere in position \((.*), (.*), (.*)\) and radius (.*)")]
        public void GivenASphereInPositionAndRadiusAndASphereInPositionAndRadius(int p0, int p1, int p2, float p3, int p4, int p5, int p6, float p7)
        {
            Context.World.RemoveObjects();

            var bodyA = Context.CreateSphere(1, p3);
            var bodyB = Context.CreateSphere(1, p7);

            bodyA.SetPosition(p0, p1, p2);
            bodyB.SetPosition(p4, p5, p6);
        }
    }
}

using System;
using TechTalk.SpecFlow;
using AmpPhysic;
using AmpPhysic.RigidBodies;
using AmpPhysic.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media.Media3D;

namespace AmpPhysicTests
{
    [Binding]
    public class PointStaticsSteps
    {
        private InjectablePhysicContext Context;        
        private GameObject PointA;

        public PointStaticsSteps(InjectablePhysicContext injectablePhysicContext)
        {
            Context = injectablePhysicContext;
        }

        [Given(@"A point in position (.*), (.*), (.*)")]
        public void GivenAPointInPosition(int p0, int p1, int p2)
        {
            PointA = new GameObject(new KinematicBody());
            PointA.SetPosition(p0, p1, p2);

            Context.World.AddObject(PointA);
        }
        
        [Given(@"Its velocity is (.*), (.*), (.*)")]
        public void GivenItsVelocityIs(int p0, int p1, int p2)
        {
            PointA.SetInitialVelocity(p0, p1, p2);            
        }

        [When(@"(.*) second passes")]
        public void WhenSecondPasses(int p0)
        {
            Context.World.Animate(p0);
        }

        [Given(@"There is a force affecting this body (.*) N in direction \((.*), (.*), (.*)\)")]
        public void GivenThereIsAForceAffectingThisBodyNInDirection(int p0, int p1, int p2, int p3)
        {
            PointA.AddForce(
                new Force(p0, p1, p2, p3, ForceType.constant)
                );
        }


        [Then(@"the point should be at position (.*), (.*), (.*)")]
        public void ThenThePointShouldBeAtPosition(int p0, int p1, int p2)
        {
            if (PointA != null)
                Assert.AreEqual(new Point3D(p0, p1, p2), PointA.Position);
            else
                Assert.Fail("BodyA nie zadeklarowane");
        }
    }
}

using System;
using TechTalk.SpecFlow;
using AmpPhysic;
using AmpPhysic.RigidBodies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media.Media3D;

namespace AmpPhysicTests
{
    [Binding]
    public class StaticPhysicPointSteps
    {
        private VirtualWorld World = new VirtualWorld();
        //private PhysicPoint bodyA;
        private GameObject PointA;

        [Given(@"A point in position (.*), (.*), (.*)")]
        public void GivenAPointInPosition(int p0, int p1, int p2)
        {
            PointA = new GameObject(new PhysicPoint(1, new Point3D(p0, p1, p2)));            
            World.AddObject(PointA);
        }
        
        [Given(@"Its velocity is (.*), (.*), (.*)")]
        public void GivenItsVelocityIs(int p0, int p1, int p2)
        {
            PointA.SetInitialVelocity(new Vector3D(p0, p1, p2));            
        }

        [When(@"(.*) second passes")]
        public void WhenSecondPasses(int p0)
        {           
            World.Animate(p0);
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

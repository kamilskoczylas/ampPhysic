using AmpPhysic;
using System;
using TechTalk.SpecFlow;
using AmpPhysic.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmpPhysicTests
{
    [Binding]
    public class DisplacementSimplifierSteps
    {
        private InjectablePhysicContext Context;

        public DisplacementSimplifierSteps(InjectablePhysicContext Context)
        {
            this.Context = Context;
        }

        [Given(@"I have bodyA and bodyB")]
        public void GivenIHaveBodyAAndBodyB()
        {
            Context.World.RemoveObjects();
            Context.CreateGameObject();
            Context.CreateGameObject();
        }
        
        [Given(@"bodyA velocity is \((.*), (.*), (.*)\)")]
        public void GivenBodyAVelocityIs(int p0, int p1, int p2)
        {
            if (Context.World.TryGetObject("bodyA", out GameObject gameObject))
            {
                gameObject.SetInitialVelocity(p0, p1, p2);
            }
        }
        
        [Given(@"bodyB velocity is \((.*), (.*), (.*)\)")]
        public void GivenBodyBVelocityIs(int p0, int p1, int p2)
        {
            if (Context.World.TryGetObject("bodyB", out GameObject gameObject))
            {
                gameObject.SetInitialVelocity(p0, p1, p2);
            }
        }
        
        [When(@"bodies are in different position")]
        public void WhenBodiesAreInDifferentPosition()
        {
            if (Context.World.TryGetObject("bodyA", out GameObject gameObject))
            {
                gameObject.SetPosition(1, 0, 0);
            }
        }
        
        [Then(@"Collision simplifier should remove their displacements")]
        public void ThenCollisionSimplifierShouldRemoveTheirDisplacements()
        {
            DisplacementSimplifier simplifierInstance = new DisplacementSimplifier();
            LinearDisplacement DisplacementA = null;
            LinearDisplacement DisplacementB = null;


            if (Context.World.TryGetObject("bodyA", out GameObject gameObject))
            {
                DisplacementA = new LinearDisplacement(gameObject.Velocity, gameObject.Position, 1);
            }

            if (Context.World.TryGetObject("bodyB", out gameObject))
            {
                DisplacementB = new LinearDisplacement(gameObject.Velocity, gameObject.Position, 1);
            }
            
            var simplified = simplifierInstance.Simplify(DisplacementA, DisplacementB);

            Assert.IsNull(simplified);
        }
    }
}

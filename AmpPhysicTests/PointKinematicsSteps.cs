using AmpPhysic;
using AmpPhysic.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Media.Media3D;
using TechTalk.SpecFlow;

namespace AmpPhysicTests
{
    [Binding]
    public class PointKinematicsSteps
    {
        InjectablePhysicContext Context;
        
        private GameObject CarA;
        private float CarMass;
        private double EngineNewtonPower;

        public PointKinematicsSteps(InjectablePhysicContext injectablePhysicContext)
        {
            Context = injectablePhysicContext;
        }
        

        [Given(@"A car with engine power (.*) HP and mass of (.*) kg")]
        public void GivenACarWithEnginePowerHPAndMassOfKg(int p0, int p1)
        {
            CarMass = p1;
            CarA = Context.CreateGameObject(CarMass);

            // change horsepower to newtons
            EngineNewtonPower = p0 * 735.49875;            
        }


        [Given(@"Its engine efficency is typical as usually (.*)%")]
        public void GivenItsEngineEfficencyIsTypicalAsUsually(int p0)
        {
            EngineNewtonPower = EngineNewtonPower * p0 / 100;
        }


        [Given(@"Its starting position is \((.*), (.*), (.*)\)")]
        public void GivenItsStartingPositionIs(int p0, int p1, int p2)
        {
            CarA.SetPosition(p0, p1, p2);
        }
        
        [Given(@"Its direction is \((.*), (.*), (.*)\)")]
        public void GivenItsDirectionIs(int p0, int p1, int p2)
        {
            CarA.AddForce(
                new Force
                    (EngineNewtonPower, p0, p1, p2, ForceType.constant)
                );
        }        

        [Given(@"Its air resistance factor is (.*) Surface of the front of car is (.*) sqare meters")]
        public void GivenItsAirResistanceFactorIsSurfaceOfTheFrontOfCarIsSqareMeters(Decimal p0, int p1)
        {
            // standard mass of the air under normal preasurre 1,168 kg/m3
            double StandardAirMassPerM3 = 1.168;

            // P= 0,5* Cx *g*A*V2
            // V2 will be replaced by X^3/3
            double VelocityFrom0To100Squared = 100 * 100 * 100 / 3;
            double AirResistanceOnTheRoad = (0.5 * Convert.ToDouble(p0) * p1 * StandardAirMassPerM3 * VelocityFrom0To100Squared);

            Vector3D NegateDirection = CarA.Direction;
            NegateDirection.Negate();

            CarA.AddForce(
                new Force
                    (AirResistanceOnTheRoad, -1, 0, 0, ForceType.constant)
                );
        }        


        [Then(@"Car velocity should be (.*) km/h and position \((.*), (.*), (.*)\)")]
        public void ThenCarVelocityShouldBeKmHAndPosition(int p0, int p1, int p2, int p3)
        {
            double speed_in_kmh = CarA.Velocity.X * 3.6;

            if (CarA.Position.X < p1 - 10 || CarA.Position.X > p1 + 10)
                Assert.Fail("Car distance: {0}m not acceptable, should be about {1}m", CarA.Position.X, p1);

            if (speed_in_kmh < p0 - 2 || speed_in_kmh > p0 + 2)
                Assert.Fail("Car speed: {0}km/h not acceptable, should be about {1}km/h", speed_in_kmh, p0);            
        }

        [Given(@"A bodyA in position \((.*), (.*), (.*)\) and bodyB in position \((.*), (.*), (.*)\)")]
        public void GivenABodyAInPositionAndBodyBInPosition(int p0, int p1, int p2, int p3, int p4, int p5)
        {
            Context.World.RemoveObjects();

            var bodyA = Context.CreateGameObject();
            var bodyB = Context.CreateGameObject();

            bodyA.SetPosition(p0, p1, p2);
            bodyB.SetPosition(p3, p4, p5);
        }

        [Given(@"The bodyA velocity is \((.*), (.*), (.*)\) and bodyB velocity is \((.*), (.*), (.*)\)")]
        public void GivenTheBodyAVelocityIsAndBodyBVelocityIs(int p0, int p1, int p2, int p3, int p4, int p5)
        {
            if (Context.World.TryGetObject("bodyA", out GameObject aBody))
            {
                aBody.SetInitialVelocity(p0, p1, p2);
            }

            if (Context.World.TryGetObject("bodyB", out aBody))
            {
                aBody.SetInitialVelocity(p3, p4, p5);
            }
        }

        [Given(@"After (.*) seconds nothing happens")]
        public void GivenAfterSecondsNothingHappens(int p0)
        {
            if (Context.World.TryGetObject("bodyA", out GameObject aBody))
            {
                aBody.CollisionEvent += ShouldNotReceiveTheCollision;
            }

            Context.World.Animate(p0);
            
        }

        private void ShouldNotReceiveTheCollision(object sender, CollisionEventArgs e)
        {            
            throw new InternalTestFailureException("There was a collision which should not occur");
        }

        [Then(@"After (.*) seconds I should recive collision event")]
        public void ThenAfterSecondsIShouldReciveCollisionEvent(int p0)
        {
            bool testSucceded = false;
            try
            {
                // Collision should occur somewhere here
                Context.World.Animate(p0);

            } catch (InternalTestFailureException)
            {
                testSucceded = true;
            }

            Assert.IsTrue(testSucceded);
        }


    }
}

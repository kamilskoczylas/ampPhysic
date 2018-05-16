using System;
using System.Windows.Media.Media3D;
using AmpPhysic.Interaction;

namespace AmpPhysic.RigidBodies
{
    class PhysicWheels
    {

        public PhysicWheel FrontLeft;
        public PhysicWheel FrontRight;
        public PhysicWheel RearLeft;
        public PhysicWheel RearRight;

        double R;
        public double CarFrontRearWheelDistanceInMeter { get; private set; }
        public double WheelDiameterInMeter { get { return R * 2; } }
        private double CurrentWheelAngle;

        public PhysicWheels(PhysicPoint Parent, Vector3D CarCenterToFrontWheelCenterInMeters, double R = 0.24, double CarFrontRearWheelDistanceInMeter = 4)
        {
            this.R = R;
            this.CarFrontRearWheelDistanceInMeter = CarFrontRearWheelDistanceInMeter; 

            FrontLeft = new PhysicWheel(R);
            FrontRight = new PhysicWheel(R);
            RearLeft = new PhysicWheel(R);
            RearRight = new PhysicWheel(R);

            /*Parent.Add(
                new PhysicConnector(
                    Parent,
                    FrontLeft, 
                    PhysicConnectorTypes.static_connection, 
                    new Vector3D(
                        -CarCenterToFrontWheelCenterInMeters.X, 
                        CarCenterToFrontWheelCenterInMeters.Y, 
                        -CarCenterToFrontWheelCenterInMeters.Z
                        ), 
                    new Vector3D(0, 1, 0))
                );

            Parent.Add(
                new PhysicConnector(
                    Parent,
                    FrontRight,
                    PhysicConnectorTypes.static_connection,
                    new Vector3D(
                        CarCenterToFrontWheelCenterInMeters.X,
                        CarCenterToFrontWheelCenterInMeters.Y,
                        -CarCenterToFrontWheelCenterInMeters.Z
                        ),
                    new Vector3D(0, 1, 0))
                );

            Parent.Add(
                new PhysicConnector(
                    Parent,
                    RearLeft,
                    PhysicConnectorTypes.static_connection,
                    new Vector3D(
                        -CarCenterToFrontWheelCenterInMeters.X,
                        CarCenterToFrontWheelCenterInMeters.Y,
                        -CarCenterToFrontWheelCenterInMeters.Z + CarFrontRearWheelDistanceInMeter
                        ),
                    new Vector3D(0, 1, 0))
                );

            Parent.Add(
                new PhysicConnector(
                    Parent,
                    RearRight,
                    PhysicConnectorTypes.static_connection,
                    new Vector3D(
                        CarCenterToFrontWheelCenterInMeters.X,
                        CarCenterToFrontWheelCenterInMeters.Y,
                        -CarCenterToFrontWheelCenterInMeters.Z + CarFrontRearWheelDistanceInMeter
                        ),
                    new Vector3D(0, 1, 0))
                );
                */

        }

        public Vector3D GetFrontRearTiresAngle()
        {
            return
                FrontLeft.CenterPosition - RearLeft.CenterPosition;
        }

        public Vector3D GetLeftRightTiresAngle()
        {
            return
                FrontLeft.CenterPosition - FrontRight.CenterPosition;
        }

        public void TryStop()
        {
            double RealVelocity = -(2 * AngleVelocity * Math.PI * R * 2) * 10;
            Vector3D TiresAngleVelocity = new Vector3D(Math.Cos(CurrentWheelAngle) * RealVelocity, 0, Math.Sin(CurrentWheelAngle) * RealVelocity);

            /*this.FrontLeft.AddForce(new Force(TiresAngleVelocity));
            this.FrontRight.AddForce(new Force(TiresAngleVelocity));
            */
        }

        public void TryToSetVelocity(double CurrentWheelAngle, double Velocity)
        {
            this.CurrentWheelAngle = CurrentWheelAngle;
            this.AngleVelocity = Velocity;
            double RealVelocity = (Velocity * Math.PI * R * 2) * 10;

            Vector3D TiresAngleVelocity = new Vector3D(Math.Cos(CurrentWheelAngle) * RealVelocity, 0, Math.Sin(CurrentWheelAngle) * RealVelocity);

            /*this.FrontLeft.AddForce(new Force(TiresAngleVelocity));
            this.FrontRight.AddForce(new Force(TiresAngleVelocity));
            */
        }
        
        public double Angle { get; private set; }
        public double AngleVelocity { get; private set; }

    }
}

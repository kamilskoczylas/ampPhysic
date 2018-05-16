using System;
using System.Windows.Media.Media3D;
using AmpPhysic.Graphic;

namespace AmpPhysic.RigidBodies
{
    class PhysicWheel : PhysicPoint
    {
        double WheelDirectionInRad;

        public PhysicWheel(double R)
            : base(R)
        {
        }

        public void SetWheelDirection(double WheelDirectionInRad)
        {
            this.WheelDirectionInRad = WheelDirectionInRad;
        }

        /*protected override void CalculateVelocity()
        {
            UpdateForce();

            if (LastCollisionNormal.LengthSquared != 0)
                Force += LastCollisionNormal * Force.Y * Math.Cos(Vector3D.AngleBetween(new Vector3D(0, 1, 0), new Vector3D(0, LastCollisionNormal.Y, 0)) / 180 * Math.PI);

            // don't add velocity vector of normal of last collision 
            _Velocity += Force / Mass;
        } */     
    }
}

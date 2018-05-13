using System;
using System.Windows.Media.Media3D;
using AmpPhysic.Interaction;

namespace AmpPhysic.RigidBodies
{
    class PhysicSphere : PhysicPoint
    {
        protected Vector3D R;

        public PhysicSphere(double R, double density = 1)
            : base(4 / 3 * Math.PI * R * R * R * density)
        {
            this.R = new Vector3D(R, R, R);
        }

        protected override void ChangeFrictionToForce(Friction FrictionForce)
        {
            AddForce(
                FrictionForce.GenerateForce()
                );
        }        
    }
}

using System.Windows.Media.Media3D;

namespace AmpPhysic.Interaction
{

    public class Friction
    {
        public Vector3D FrictionPoint_DistanceFromCenterOfMass;
        public double FrictionFactor;
        public Vector3D HitForce;

        public Friction(Vector3D FrictionPoint_DistanceFromCenterOfMass, Vector3D HitForce, double FrictionFactor = 1)
        {
            this.FrictionFactor = FrictionFactor;
            this.FrictionPoint_DistanceFromCenterOfMass = FrictionPoint_DistanceFromCenterOfMass;
        }

        public Force GenerateForce()
        {
            // T = fmgcosA
            return
                new Force(FrictionFactor, HitForce);
        }

    }
}

using System.Windows.Media.Media3D;
using AmpPhysic.Interaction;
using AmpPhysic.Collision;
using AmpPhysic.RigidBodies;

namespace AmpPhysic
{
    public interface IPhysic
    {
        //double Weight { get; set; }

        Vector3D Velocity { get; set;  }
        Vector3D SelfRotation { get; set; }
        Vector3D Force { get; }
        Vector3D CenterOfMass { get; }
        Point3D CenterPosition { get; }

        double Mass { get; }

        void UpdateForce();

        void AddForce(Force force);

        void Move(float deltaTime);
    }
}

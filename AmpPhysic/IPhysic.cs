using System.Windows.Media.Media3D;
using AmpPhysic.Interaction;
using AmpPhysic.Collision;
using AmpPhysic.RigidBodies;

namespace AmpPhysic
{
    public interface IPhysic
    {        

        Vector3D Velocity { get; set;  }
        Vector3D AngularRadVelocity { get; set; }
        Vector3D AngularPosition { get; set; }   
        Point3D CenterPosition { get; set; }

        double Mass { get; set; }
        
    }
}

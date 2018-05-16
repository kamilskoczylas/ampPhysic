
using System.Windows.Media.Media3D;

namespace AmpPhysic
{
    interface IPhysicShape
    {
        // Build

        // Get
        Vector3D GetCenterOfMass();
        double GetMass();

        // Handle Collisions
        
    }
}

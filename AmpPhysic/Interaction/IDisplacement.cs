using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AmpPhysic.Interaction
{
    public interface IDisplacement
    {
        Vector3D Velocity { get; }
        float DeltaTime { get; }
        Point3D StartingPosition { get; }

        IPhysicControl PhysicObject { get; }

        Vector3D GetPositionChange(float displacementTime = 0);
    }
}

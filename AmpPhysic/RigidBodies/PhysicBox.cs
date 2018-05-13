using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace AmpPhysic.RigidBodies
{
    public class PhysicBox : PhysicPoint
    {
        public PhysicBox(Vector3D Size, double density = 1)
            : base( Size.X * Size.Y * Size.Z * density )
        {
        }
    }
}

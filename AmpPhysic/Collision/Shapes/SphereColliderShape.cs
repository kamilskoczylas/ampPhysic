using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmpPhysic.Collision.Shapes
{
    public class SphereColliderShape : ColliderShape
    {
        float Radius;

        public SphereColliderShape(float R)
        {
            Radius = R;
        }

        public override float CalculateMaximumRadius
        {
            get
            {
                return Radius;
            }

        }
    }
}

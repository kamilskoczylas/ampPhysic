using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmpPhysic.Collision.Shapes
{
    class PointColliderShape : ColliderShape
    {
        public override float CalculateMaximumRadius {
            get
            {                
                return 0.0f;
            }

         }
    }
}

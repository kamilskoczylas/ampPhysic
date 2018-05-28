using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmpPhysic.Collision
{
    class CollidingObjects
    {
        IPhysicControl PrimaryObject;
        IPhysicControl SecondaryObject;

        public CollidingObjects(IPhysicControl primaryObject, IPhysicControl secondaryObject)
        {
            PrimaryObject = primaryObject;
            SecondaryObject = secondaryObject;
        }

        public IPhysicControl GetPrimary()
        {
            return PrimaryObject;
        }

        public IPhysicControl GetSecondary()
        {
            return SecondaryObject;
        }
    }
}

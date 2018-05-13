using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using AmpPhysic.RigidBodies;
using AmpPhysic.Interaction;

namespace AmpPhysic
{
    class PhysicStructure : PhysicPoint, IPhysic //, ICollisable
    {
        Rect3D BoundBox;
        List<IPhysic> Elements;
        private List<PhysicConnector> Children;

        public PhysicStructure() : base (0)
        {
            Elements = new List<IPhysic>();
        }

        Vector3D IPhysic.Velocity
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        Vector3D IPhysic.SelfRotation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        Vector3D IPhysic.Force
        {
            get { throw new NotImplementedException(); }
        }

        Vector3D IPhysic.CenterOfMass
        {
            get { throw new NotImplementedException(); }
        }

        Point3D IPhysic.CenterPosition
        {
            get { throw new NotImplementedException(); }
        }

        double IPhysic.Mass
        {
            get { throw new NotImplementedException(); }
        }

        void IPhysic.UpdateForce()
        {
            throw new NotImplementedException();
        }

        void IPhysic.AddForce(Force force)
        {
            throw new NotImplementedException();
        }        
    }
}

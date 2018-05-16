using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using AmpPhysic;
using AmpPhysic.Collision;
using AmpPhysic.Interaction;

namespace AmpPhysic.RigidBodies
{   
    public class PhysicPoint : IPhysic
    {           
        
        public Vector3D Velocity { get; set; }
        public Vector3D AngularRadVelocity { get; set; }

        public Vector3D AngularPosition
        {
            get;
            set;
        }

        public double Mass { get; set; }               

        public Point3D CenterPosition
        {
            get;
            set;
        }

        public PhysicPoint(double mass)
        {
            Mass = mass;
            CenterPosition = new Point3D(0, 0, 0);
            AngularPosition = new Vector3D(0, 0, 0);

            Velocity = new Vector3D(0, 0, 0);
            AngularRadVelocity = new Vector3D(0, 0, 0);
        }

        public PhysicPoint(double mass, Point3D CenterPosition) : this(mass)
        {            
            this.CenterPosition = CenterPosition;            
        }

        public PhysicPoint(double mass, Point3D CenterPosition, Vector3D Velocity) : this(mass, CenterPosition)
        {
            this.Velocity = Velocity;
        }          
            
    }
}

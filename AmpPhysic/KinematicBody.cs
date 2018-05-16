using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using AmpPhysic.RigidBodies;
using AmpPhysic.Interaction;
using AmpPhysic.Collision;

namespace AmpPhysic
{
    public class KinematicBody : IPhysicControl
    {
        Rect3D BoundBox;
        List<IPhysicShape> Elements;

        private List<PhysicConnector> Children;
        private List<Force> Forces;
        private List<Force> StillActiveForces = null;
        
        private Vector3D NetForce;
        

        public KinematicBody(double mass = 1)
        {
            Elements = new List<IPhysicShape>();
            Forces = new List<Force>();
            NetForce = new Vector3D(0, 0, 0);

            CenterOfMass = new PhysicPoint(mass);
            CenterPosition = new Point3D(0, 0, 0);
            AngularPosition = new Vector3D(0, 0, 0);

            PositionDisplacement = new Vector3D(0, 0, 0);
            AngularDisplacement = new Vector3D(0, 0, 0);
            AngularRadVelocity = new Vector3D(0, 0, 0);

            CalculateCenterOfMass();
        }

        private PhysicPoint CenterOfMass;

        public Vector3D Velocity
        {
            get { return CenterOfMass.Velocity; }
            set { CenterOfMass.Velocity = value; }
        }

        public Vector3D AngularRadVelocity
        {
            get;
            set;
        }


        public Vector3D PositionDisplacement
        {
            get;
            set;
        }
        public Vector3D AngularDisplacement
        {
            get;
            set;
        }

        /**
         * <summary>
         * Gets the center position of the structure, it is different than CenterOfMass position
         * </summary>
         */
        public Point3D CenterPosition
        {
            get;
            set;
        }

        public Vector3D AngularPosition
        {
            get;
            set;
        }

        public double Mass
        {
            get
            {
                return CenterOfMass.Mass;
            }

            set
            {
                CenterOfMass.Mass = value;
            }
        }

        protected void CalculateCenterOfMass()
        {
            if (CenterOfMass.Mass == 0)
                throw new DivideByZeroException(
                    "Mass of the physic body cannot be 0 because its acceleration would become indefinite"
                    );
        }

        public void AddForce(Force force)
        {
            Forces.Add(force);
        }

        public virtual void CalculateDisplacement(float deltaTime)
        {
            CalculateVelocity(deltaTime);

            PositionDisplacement = Velocity * deltaTime;
            AngularDisplacement = AngularRadVelocity * deltaTime;
        }

        public virtual void CommitDisplacement()
        {
            this.CenterPosition = this.CenterPosition + PositionDisplacement;
            this.AngularPosition = this.AngularPosition + AngularDisplacement;

            ResetDisplacement();
        }

        public virtual void CommitDisplacementPartially(float commitedTime, float totalDeltaTime)
        {
            float proportion = commitedTime / totalDeltaTime;
            float inverse_proportion = 1 - proportion;

            this.CenterPosition = this.CenterPosition + PositionDisplacement * proportion;
            this.AngularPosition = this.AngularPosition + AngularDisplacement * proportion;

            PositionDisplacement = inverse_proportion * PositionDisplacement;
            AngularDisplacement = inverse_proportion * AngularDisplacement;
        }

        public virtual void ResetDisplacement()
        {
            PositionDisplacement = new Vector3D(0, 0, 0);

            AngularDisplacement = new Vector3D(0, 0, 0);
        }

        public void Add(PhysicConnector Connector)
        {   
            this.Children.Add(Connector);
        }

        protected virtual void CalculateVelocity(float deltaTime)
        {
            UpdateForce(deltaTime);            

            Vector3D acceleration = NetForce / CenterOfMass.Mass;

            CenterOfMass.Velocity += acceleration * deltaTime;
        }

        public void UpdateForce(float deltaTime)
        {           

            NetForce *= 0;
                        
            // constant forces or still active
            if (StillActiveForces != null && StillActiveForces.Count > 0)
                Forces.AddRange(StillActiveForces);

            var tmp = new List<Force>();

            if (Forces != null && Forces.Count > 0)
                foreach (var force in Forces)
                {
                    NetForce += force.Direction * force.ForceNewtonsValue;
                    if (force.Type == ForceType.constant || (force.Type == ForceType.duration && force.Direction.LengthSquared > 0))
                    {
                        tmp.Add(force);
                    }
                }

            StillActiveForces = tmp;
            Forces.Clear();
        }

        //public bool HasCollision { get { return (LastCollisionNormal != null && LastCollisionNormal.LengthSquared > 0); } }

        public void HandleCollision(CollisionResponse collisionResponse)
        {
            throw new NotImplementedException();
        }
    }
}

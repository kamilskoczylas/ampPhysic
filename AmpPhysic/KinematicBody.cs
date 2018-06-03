using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using AmpPhysic.RigidBodies;
using AmpPhysic.Interaction;
using AmpPhysic.Collision;
using AmpPhysic.Collision.Shapes;

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
        private List<IDisplacement> DisplacementList;
        ColliderShape Shape;

        public float MaximumRadius { get; private set; }


        public KinematicBody(double mass = 1, ColliderShape shape = null)
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

            DisplacementList = new List<IDisplacement>();

            if (shape == null)
            {
                shape = new PointColliderShape();
            }

            this.Shape = shape;
            OnShapeChange();
            CalculateCenterOfMass();
        }

        private void OnShapeChange()
        {
            // Anylaze shape to calculate something like bounding box
            // this will be maximumRadius of the shape rotated in any direction,
            // so instead of bounding box will be bounding sphere
            MaximumRadius = Shape.CalculateMaximumRadius;
        }

        public CollisableArea GetCollisionArea()
        {
            var tmp = SummarizeDisplacements();
            double dX, dZ;

            if (tmp.X >= 0)
            {
                dX = MaximumRadius * 2 + tmp.X;
            }
            else
            {
                dX = tmp.X - MaximumRadius * 2;
            }

            if (tmp.Z >= 0)
            {
                dZ = MaximumRadius * 2 + tmp.Z;
            }
            else
            {
                dZ = tmp.Z - MaximumRadius * 2;
            }            

            return new CollisableArea(CenterPosition.X - MaximumRadius, CenterPosition.Z - MaximumRadius, dX, dZ, this);
        }

        public bool CurrentlyStaticYesNo
        {
            get { return !(Velocity.X != 0 || Velocity.Y != 0 || Velocity.Z != 0); }
        }

        public bool CurrentlyAcceleratingYesNo
        {
            get { return (Acceleration.X != 0 || Acceleration.Y != 0 || Acceleration.Z != 0); }
        }

        public List<IDisplacement> GetDisplacements()
        {
            return DisplacementList;
        }

        public ColliderShape GetColliderShape()
        {
            return Shape;
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

        public Vector3D Acceleration
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

        public GameObject GameObject { get; set; }

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
            DisplacementList.Add(
                    new LinearDisplacement(this, deltaTime)
                );            

            CalculateAcceleration(deltaTime);

            if (CurrentlyAcceleratingYesNo)
            {
                DisplacementList.Add(
                    new AcceleratedDisplacement(this, deltaTime)
                );
            }            
        }

        private Vector3D SummarizeDisplacements()
        {
            Vector3D TotalDisplacement = new Vector3D(0, 0, 0);
            foreach (var Displacement in DisplacementList)
            {
                TotalDisplacement += Displacement.GetPositionChange();
                //this.AngularPosition = this.AngularPosition + AngularDisplacement;
            }

            return TotalDisplacement;
        }

        
        public virtual void CommitDisplacement()
        {            
            this.CenterPosition = this.CenterPosition + SummarizeDisplacements();

            ResetDisplacement();
        }

        public virtual void CommitDisplacementPartially(float commitedTime)
        {
            foreach (var Displacement in DisplacementList)
            {
                this.CenterPosition = this.CenterPosition + Displacement.GetPositionChange(commitedTime);
            }

            // this happens only after collision
            ResetDisplacement();
        }

        public virtual void ResetDisplacement()
        {
            DisplacementList.Clear();            
        }

        public void Add(PhysicConnector Connector)
        {   
            this.Children.Add(Connector);
        }

        protected virtual void CalculateAcceleration(float deltaTime)
        {
            UpdateForce(deltaTime);

            Acceleration = NetForce / CenterOfMass.Mass;

            CenterOfMass.Velocity += Acceleration * deltaTime;
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

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
    public class PhysicPoint : IPhysic //, ICollisable
    {
        private Point3D centerPosition;        
        public Vector3D CenterOfMass { get; private set; }

        public double Weight { get; set; }

        protected Vector3D _Velocity;
        protected Vector3D LastCollisionNormal;

        public Vector3D Velocity { get; set; }
        public Vector3D SelfRotation { get; set; }
        public Vector3D Force { get; protected set; }       
        public double Mass { get; private set; }
        public bool HasCollision { get { return (LastCollisionNormal != null && LastCollisionNormal.LengthSquared > 0); } }

        private List<PhysicConnector> Children;

        public Point3D CenterPosition
        {
            get
            {
                return centerPosition;
            }
        }

        private List<Force> Forces;
        private List<Force> StillActiveForces = null;

        public PhysicPoint(double mass)
        {
            Mass = mass;
            Forces = new List<Force>();
            Children = new List<PhysicConnector>();
            CenterOfMass = new Vector3D { X = 0, Y = 0, Z = 0 };
        }

        public PhysicPoint(double mass, Point3D CenterPosition) : this(mass)
        {            
            centerPosition = CenterPosition;            
        }

        public PhysicPoint(double mass, Point3D CenterPosition, Vector3D Velocity) : this(mass, CenterPosition)
        {
            this.Velocity = Velocity;
        }
        

        public void Add(PhysicConnector Connector)
        {
            Mass += Connector.ChildObject.Mass;
            this.Children.Add(Connector);
        }

        public void AddGravity(double ForceY = -1)
        {
            var f = new Force(new Vector3D { X = 0, Y = ForceY, Z = 0 }, ForceType.constant);
            AddForce(f);
        }

        public void checkCollisions(List<GameObject> objects)
        {
            foreach (var phisicObj in objects)
            {
                if (isColision(phisicObj))
                {
                    doColision(phisicObj);
                }
            }
        }

        public virtual bool isColision(GameObject with_obj)
        {

            return false;
        }

        public virtual bool doColision(GameObject with_obj)
        {
            return false;
        }

        public void UpdateForce()
        {   
            /*
             * executed in Move() method
             * 
             */

            Force *= 0;

           if (Children.Count > 0)
                foreach (var Connector in Children)
                {
                    // constant forces add immediately
                    Connector.ChildObject.UpdateForce();
                    Force += Connector.ChildObject.Force;
                }
           

            // constant forces or still active
            if (StillActiveForces != null && StillActiveForces.Count > 0)
                Forces.AddRange(StillActiveForces);

            var tmp = new List<Force>();            
            
            if (Forces != null && Forces.Count > 0)
            foreach (var force in Forces)
            {
                Force += force.Value;
                if (force.Type == ForceType.constant || (force.Type == ForceType.duration && force.Value.LengthSquared > 0))
                {
                    tmp.Add(force);
                }
            }

            StillActiveForces = tmp;
            Forces.Clear();
        }

        protected virtual void CalculateVelocity()
        {
            UpdateForce();

            if (LastCollisionNormal.LengthSquared != 0)
                Force += LastCollisionNormal * Force.Y * Math.Cos(Vector3D.AngleBetween(new Vector3D(0, 1, 0), new Vector3D(0, LastCollisionNormal.Y, 0)) / 180 * Math.PI);

            // don't add velocity vector of normal of last collision 
            _Velocity += Force / Mass;
        }


        public virtual void Move(float deltaTime)
        {
            this.centerPosition += Velocity * deltaTime;
        }

        public void Move(CollisionResponser CollisionResponseClass)
        {
            
            if (Children.Count > 0)
                foreach (var Connector in Children)
                {
                    // constant forces add immediately
                    //Connector.ChildObject.Move(CollisionResponseClass);
                    //Force += Connector.ChildObject.Force;
                }
            CalculateVelocity();            
            
            Vector3D AddVelocity = Velocity;
            double RemainingVelocityLength = Velocity.Length;

            Point3D NewPosition =  CenterPosition;
            Point3D LastCollision = CenterPosition;            
            Vector3D VelocityDirection = Velocity;

            LastCollisionNormal = new Vector3D { X = 0, Y = 0, Z = 0 };    
            VelocityDirection.Normalize();
            
                NewPosition = CenterPosition + VelocityDirection * RemainingVelocityLength;
                var Response = TryToGoTo(NewPosition, CollisionResponseClass);

                while (Response != null)
                {   
                    VelocityDirection = Response.CollisionForceVector;
                    VelocityDirection.Normalize();

                    LastCollisionNormal = Response.PlaneNormal;

                    var AddPos = new Vector3D
                    {
                        X = Response.CollisionPoint3D.X - CenterPosition.X,
                        Y = Response.CollisionPoint3D.Y - CenterPosition.Y,
                        Z = Response.CollisionPoint3D.Z - CenterPosition.Z
                    };

                    ChangeFrictionToForce(Response.FrictionForce);
                    LastCollision = Response.CollisionPoint3D + Response.PlaneNormal * 0.0001;                    

                    // Update position to old one -before collision
                    centerPosition.X = LastCollision.X;
                    centerPosition.Y = LastCollision.Y;
                    centerPosition.Z = LastCollision.Z;

                    RemainingVelocityLength = RemainingVelocityLength - AddPos.Length;
                    if (RemainingVelocityLength < 0.0001)
                    {
                        NewPosition.X = LastCollision.X;
                        NewPosition.Y = LastCollision.Y;
                        NewPosition.Z = LastCollision.Z;

                        break;
                    }

                    _Velocity *= Math.Cos(Vector3D.AngleBetween(AddVelocity, VelocityDirection) / 180 * Math.PI);
                    //_Velocity += Response.PlaneNormal * RemainingVelocityLength;
                    NewPosition = LastCollision + RemainingVelocityLength * VelocityDirection;

                    // Calculate the new position
                    Response = TryToGoTo(NewPosition, CollisionResponseClass);
                }

                if (RemainingVelocityLength >= 0.0001)
                {
                    centerPosition.X = NewPosition.X;
                    centerPosition.Y = NewPosition.Y;
                    centerPosition.Z = NewPosition.Z;
                }
        }

        protected virtual void ChangeFrictionToForce(Friction FrictionForce)
        {            
            AddForce(
                FrictionForce.GenerateForce()
                );
        }

        public CollisionResponse TryToGoTo(Point3D NewPosition, CollisionResponser CollisionResponseClass)
        {
            List<CollisionResponse> Collisions = new List<CollisionResponse>();
            Vector3D min = new Vector3D(0, 0, 0);
            int ClosestCollision = -1;

            var tmp = CollisionResponseClass.isCollision(this, NewPosition);
            if (tmp != null)
            {
                Collisions.Add(tmp);

                min = Collisions[0].CollisionPoint3D - CenterPosition;
                ClosestCollision = 0;
            }

            // nie do końca prawda, bo podelementy w TryToGoTo też powinny mieć nową pozycję
            foreach (var ConnectorElement in Children)
            {
                tmp = CollisionResponseClass.isCollision(this, NewPosition);
                if (tmp != null)
                {
                    Collisions.Add(tmp);
                    int i = Collisions.Count - 1;
                    Vector3D test = Collisions[i].CollisionPoint3D - ConnectorElement.ChildObject.CenterPosition;
                    if (ClosestCollision == - 1 || min.Length > test.Length)
                    {
                        min = test;
                        ClosestCollision = i;
                    }
                }
            }

            if (ClosestCollision == -1)
                return null;

            return Collisions[ClosestCollision];
        }


        public void AddForce(Force force)
        {
            Forces.Add(force);
        }

        public bool IsIn(Point3D l0, Vector3D l, out Point3D intersectionPoint, out Vector3D hitforcevector, out Vector3D PlaneNormal)
        {
            throw new NotImplementedException();
        }

        public void Hit(Point3D intersectionPoint, Vector3D hitforcevector)
        {
            throw new NotImplementedException();
        }



        public CollisionResponse TryToMove(PhysicConnector Connector)
        {
            CollisionResponse Response = null;            
            this.centerPosition = Connector.ParentObject.CenterPosition + Connector.PositionFromParentCenter;

            return Response;
        }        
    }
}

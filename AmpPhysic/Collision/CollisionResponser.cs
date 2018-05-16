using System.Collections.Generic;
using System.Windows.Media.Media3D;
using AmpPhysic.Interaction;

namespace AmpPhysic.Collision
{
    public class CollisionResponser
    {
        private List<ICollisable> possible_objects;

        private List<StaticTriangle> staticTriangles;

        public CollisionResponser()
        {
            possible_objects = new List<ICollisable>();
            staticTriangles = new List<StaticTriangle>();

        }

        public void AddBox(double width, double height, double deep)
        {
        }

        public void RegisterObject(ICollisable collisionObject)
        {
            possible_objects.Add(collisionObject);
        }

        public void RegisterStatic(ICollisable collisionObject)
        {
        }

        public void RegisterStaticTriangle(Point3D t1, Point3D t2, Point3D t3)
        {
            staticTriangles.Add(
                new StaticTriangle(t1, t2, t3)
                );
        }

        public CollisionResponse isCollision(IPhysic MovingObject, Point3D NewPosition)
        {

            // for each shape look for colliding shape in other object
            foreach (var current_shape in possible_objects)
            {
            }

            Point3D IntersectionPoint;
            Vector3D HitDirection;
            Vector3D PlaneNormal;
            Vector3D MoveVector = new Vector3D
            {
                X = NewPosition.X - MovingObject.CenterPosition.X,
                Y = NewPosition.Y - MovingObject.CenterPosition.Y,
                Z = NewPosition.Z - MovingObject.CenterPosition.Z
            };


            // for each shape look for colliding shape in other object
            var Collisions = new List<CollisionResponse>();

            foreach (var triangle in staticTriangles)
            {
                CollistionDetails CollistionDetail;
                if (triangle.IsIn(MovingObject.CenterPosition, MoveVector, out CollistionDetail))
                {
                    Friction T = new Friction(
                            new Vector3D {
                                    X = NewPosition.X - MovingObject.CenterPosition.X,
                                    Y = NewPosition.Y - MovingObject.CenterPosition.Y,
                                    Z = NewPosition.Z - MovingObject.CenterPosition.Z
                                },
                                MoveVector
                            );

                    Collisions.Add(
                        new CollisionResponse(CollistionDetail)
                    );
                }                   
                    
            }

            if (Collisions.Count > 0)
            {
                if (Collisions.Count == 1)
                    return Collisions[0];

                Vector3D min = Collisions[0].CollisionPoint3D - MovingObject.CenterPosition;
                int ClosestCollision = 0;
                for (int i = 1; i < Collisions.Count; i++ )
                {
                    Vector3D test = Collisions[i].CollisionPoint3D - MovingObject.CenterPosition;
                    if (min.Length > test.Length)
                    {
                        min = test;
                        ClosestCollision = i;
                    }
                }

                return Collisions[ClosestCollision];
            }
                

            return null;
        }

        public void Update(Point3D center, double angle, double vertical_angle)
        {


        }

        /*public void Move(CollisionResponser CollisionResponseClass)
        {

            Point3D NewPosition = CenterPosition;
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
                    if (ClosestCollision == -1 || min.Length > test.Length)
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
        }*/
    }

    class StaticTriangle : ICollisable
    {
        TriangleCollision CollisionCheck;
        double FrictionFactor;

        public StaticTriangle(Point3D t1, Point3D t2, Point3D t3, double FrictionFactor = 1)
        {
            CollisionCheck = new TriangleCollision(t1, t2, t3);
            this.FrictionFactor = FrictionFactor;
        }

        public bool IsIn(Point3D l0, Vector3D l, out CollistionDetails CollisionFound)
        {             
            return this.CollisionCheck.IsIn(l0, l, out CollisionFound);
        }

        public void Hit(Point3D intersectionPoint, Vector3D hitforcevector)
        {
            
        }
    } 
}

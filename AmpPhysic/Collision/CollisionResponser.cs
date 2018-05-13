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

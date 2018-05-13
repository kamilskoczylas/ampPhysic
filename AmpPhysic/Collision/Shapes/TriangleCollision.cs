using System;
using System.Windows.Media.Media3D;


namespace AmpPhysic.Collision
{       
    enum Coordinates {XY, YZ, XZ} 

    public class TriangleCollision
    {

        private Vector3D _planeNormal;
        public Vector3D planeNormal { get { return _planeNormal; } }
        private Vector3D A, B;
        private Point3D p0, p1, p2;
        private Point _2Dp0, _2Dp1, _2Dp2;
        private Coordinates useCoordinates  = Coordinates.XY;
        private double Area;

        public TriangleCollision(Point3D p0, Point3D p1, Point3D p2)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;

            // plane pattern is:
            // (p - p0) * n = 0

            // calculating n - normal to plane
            A = new Vector3D { X = p1.X - p0.X, Y = p1.Y - p0.Y, Z = p1.Z - p0.Z };
            B = new Vector3D { X = p2.X - p0.X, Y = p2.Y - p0.Y, Z = p2.Z - p0.Z };
            

            _planeNormal = Vector3D.CrossProduct(A, B);
            _planeNormal.Normalize();

            if ((p0.X != p1.X || p0.X != p2.X) && 
                (p0.Z != p1.Z || p0.Z != p2.Z)
                )
            {
                Set2DPoints(p0.X, p0.Z, p1.X, p1.Z, p2.X, p2.Z);
                useCoordinates = Coordinates.XZ;
            }
            else if ((p0.Y != p1.Y || p0.Y != p2.Y) &&
                     (p0.Z != p1.Z || p0.Z != p2.Z)
                )
            {
                Set2DPoints(p0.Z, p0.Y, p1.Z, p1.Y, p2.Z, p2.Y);
                useCoordinates = Coordinates.YZ;
            }
            else
            {
                Set2DPoints(p0.Z, p0.Y, p1.Z, p1.Y, p2.Z, p2.Y);
                useCoordinates = Coordinates.XY;
            }

            Area = ( 0.5 * (-_2Dp1.Y * _2Dp2.X + _2Dp0.Y * (-_2Dp1.X + _2Dp2.X) + _2Dp0.X * (_2Dp1.Y - _2Dp2.Y) + _2Dp1.X * _2Dp2.Y) );
        }

        public bool IsIn(Point3D l0, Vector3D l, out CollistionDetails CollisionFound)
        {
            CollisionFound = new CollistionDetails();

            if (IsLineIntersection(l0, l, out CollisionFound.intersectionPoint))
                if (IsPointIn(CollisionFound.intersectionPoint))
                {
                    Vector3D u = Vector3D.DotProduct(l, this.planeNormal) * this.planeNormal;
                    Vector3D w = l - u;
                    CollisionFound.hitforcevector = w - u;
                    CollisionFound.PlaneNormal = planeNormal;

                    return true;
                }

            return false;
        }

        private void Set2DPoints(double x0, double y0, double x1, double y1, double x2, double y2)
        {
            _2Dp0.X = x0;
            _2Dp0.Y = y0;

            _2Dp1.X = x1;
            _2Dp1.Y = y1;

            _2Dp2.X = x2;
            _2Dp2.Y = y2;
        }

        private bool IsPointIn(Point3D intersectionPoint)
        {
            Point P = new Point();

            if (useCoordinates == Coordinates.XZ)
            {
                P.X = intersectionPoint.X;
                P.Y = intersectionPoint.Z;
            }
            else if (useCoordinates == Coordinates.YZ)
            {
                P.X = intersectionPoint.Z;
                P.Y = intersectionPoint.Y;
            }
            else                
            {
                P.X = intersectionPoint.X;
                P.Y = intersectionPoint.Y;
            }

            double s = 1 / (2 * Area) * (_2Dp0.Y * _2Dp2.X - _2Dp0.X * _2Dp2.Y + (_2Dp2.Y - _2Dp0.Y) * P.X + (_2Dp0.X - _2Dp2.X) * P.Y);
            double t = 1 / (2 * Area) * (_2Dp0.X * _2Dp1.Y - _2Dp0.Y * _2Dp1.X + (_2Dp0.Y - _2Dp1.Y) * P.X + (_2Dp1.X - _2Dp0.X) * P.Y);

            return (s <= 1 && t <= 1 && s >= 0 && t>= 0 && s + t <= 1);
        }        
        
        private bool IsLineIntersection(Point3D l0, Vector3D l, out Point3D intersectionPoint)
        {
            // line vector equotation is
            // p = dl + l0

            double dot_of_nl = Vector3D.DotProduct(this.planeNormal, l);
            double d;

            if (Math.Abs(dot_of_nl) <= 0.000001)
            {
                // if plane is pararel to line
                if (Math.Abs(Vector3D.DotProduct(this.p0 - l0, this.planeNormal)) <= 0.000001)
                {
                    intersectionPoint = l0;
                    return true;
                }
            }
            else
            {
                // if there is one point
                d = Vector3D.DotProduct(this.p0 - l0, this.planeNormal) / dot_of_nl;
                intersectionPoint = d * l + l0;
                Vector3D tmp = l / d;

                if (d >= 0 && d<= 1)
                {
                    return true;
                }
                else
                    return false;
            }

            // no intersection
            intersectionPoint = new Point3D { X = 0, Y = 0, Z = 0 };
            return false;
        }
    }    
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows;
using AmpPhysic.Collision;

namespace AmpPhysic.Graphic
{
    public interface I3DBuilder
    {
        void Build(Rect3D rect);
    }


    public class TerrainPoint
    {
        //public double z, x, y;
        public Point3D point3d;
        public double angle = Math.PI / 2;
        public double vertical_angle = 0;
    }


    public class RoadArc
    {
        public double left_x, left_z, right_x, right_z, left_y, right_y;
        public double left_x2, left_z2, right_x2, right_z2, left_y2, right_y2;
        public double angle = Math.PI / 2;
        public double vertical_angle = 0;
    }

    public class Builder3D
    {
    }

    public class BasicBuilder
    {
        protected PointCollection TextureCoordinates = new PointCollection();
        protected Int32Collection TriangleIndices = new Int32Collection();
        protected Point3DCollection Positions = new Point3DCollection();

        protected CollisionResponser Responser;
        protected Rect3D Frame;
        protected Model3DGroup Container;
        protected Setup Settings;

        public BasicBuilder(Model3DGroup Container, CollisionResponser Responser, Setup Settings)
        {
            this.Container = Container;
            this.Settings = Settings;
            this.Responser = Responser;
        }

        private Vector3D CalculateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            var v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            var v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }

        private void AddIndices(Int32[] indices, int start_pos = 0)
        {
            foreach (var triangle_point in indices)
            {
                //TriangleIndices.Add(triangle_point + start_pos);
            }
        }

        protected Brush LoadTexture()
        {
            try
            {
                Random rnd = new Random();
                string[] fileTxts = Directory.GetFiles(Settings.ApplicationPath + @"\graph\stage\", this.GetType().Name.Replace("Builder", "") + "*");

                ImageBrush texture = new ImageBrush();
                texture.ImageSource = new BitmapImage(new Uri(fileTxts[rnd.Next(fileTxts.Length)]));
                texture.TileMode = TileMode.Tile;
                texture.ViewportUnits = BrushMappingMode.Absolute;

                return texture;
            }
            catch (Exception)
            {
                return Brushes.Orange;
            }
        }

        public void AddGeometryToContainer()
        {
            var MeshGeometry = new MeshGeometry3D();
            var GeometryModel = new GeometryModel3D();

            MeshGeometry.Positions = this.Positions;
            MeshGeometry.TriangleIndices = this.TriangleIndices;
            MeshGeometry.TextureCoordinates = this.TextureCoordinates;

            GeometryModel.Geometry = MeshGeometry;
            GeometryModel.Material = new DiffuseMaterial(LoadTexture());

            //Container.Children.Add(GeometryModel);
        }

        public void Cube(Rect3D Rect)
        {
            /* ^ Y     _______
               |    LT/______/|
               |      |     | |
               |  Z   |     | |RB 
               | /    |_____|/   
               |/               X
               ------------------>
             */

            int start = Positions.Count;

            // Front

            /*
            for (int i = 0; i < 2; i++)
            {
                Positions.Add(new Point3D
                {
                    X = Rect.X,
                    Y = Rect.Y,
                    Z = Rect.Z + i * Rect.SizeZ
                });

                Positions.Add(new Point3D
                {
                    X = Rect.X,
                    Y = Rect.Y + Rect.SizeY,
                    Z = Rect.Z + i * Rect.SizeZ
                });

                Positions.Add(new Point3D
                {
                    X = Rect.X + Rect.SizeX,
                    Y = Rect.Y + Rect.SizeY,
                    Z = Rect.Z + i * Rect.SizeZ
                });

                Positions.Add(new Point3D
                {
                    X = Rect.X + Rect.SizeX,
                    Y = Rect.Y,
                    Z = Rect.Z + i * Rect.SizeZ
                });
            }*/

            /*Point3D p0 = Positions[a];
            Point3D p1 = Positions[b];
            Point3D p2 = Positions[c];

            //if (p0.Z < 1 || p1.Z < 1 || p2.Z < 1)
            Responser.RegisterStaticTriangle(
                        p0,
                        p1,
                        p2
                    );*/

            /*for (int i = 0; i < 16; i++)
            {*/
            /*TextureCoordinates.Add(new Point(0, 0));
            TextureCoordinates.Add(new Point(0, 1));
            TextureCoordinates.Add(new Point(1, 1));
            TextureCoordinates.Add(new Point(1, 0));*/
            //}



            Int32[] indices ={
            //front
                  0,1,2,
                  0,2,3,
            //back
                  4,7,6,
                  4,6,5,
            //Right
                  4,0,3,
                  4,3,7,
            //Top
                  1,5,6,
                  1,6,2,

                  1,0,4,
                  1,4,5,
            //Bottom
                  2,6,7,
                  2,7,3
            };

            if (Responser != null)
            {
                for (int i = 0; i < 12; i++)
                {
                    Point3D p0 = new Point3D(); //Positions[start + indices[i * 3]];
                    Point3D p1 = new Point3D(); //Positions[start + indices[i * 3 + 1]];
                    Point3D p2 = new Point3D(); //Positions[start + indices[i * 3 + 2]];*/

                    //if (p0.Z < 1 || p1.Z < 1 || p2.Z < 1)
                    /*Responser.RegisterStaticTriangle(
                                p0,
                                p1,
                                p2
                            );*/
                }
            }

            AddIndices(indices, start);
        }

        public void Sphere(Point3D Center, double R)
        {

            int SPHERE_PRECISION = 12;

            if (Settings != null)
                SPHERE_PRECISION = Settings.PRECISION_3D_PER_ONE_UNIT_SIZE;

            int start = Positions.Count;
            int precision = Convert.ToInt32(R * SPHERE_PRECISION);

            // Top
            /*Positions.Add(new Point3D
            {
                X = Center.X,
                Y = Center.Y + R,
                Z = Center.Z
            });

            // Bottom
            Positions.Add(new Point3D
            {
                X = Center.X,
                Y = Center.Y - R,
                Z = Center.Z
            });
            */
            double circle_part = Math.PI / 2 / precision;
            double X, Y, Z, Y2, current_R;

            int circle_precision = 1;
            int last_circle_precision;
            int last_circle_point = 0;
            int last_row_start_point = 0;
            int row_start_point = 0;

            double circle_part_horizontal = Math.PI * 2;
            double last_circle_part_horizontal;
            double previous_sphere_angle;
            double current_sphere_angle;

            // half of sphere 0-PI
            for (int i = 1; i <= precision; i++)
            {
                current_R = Math.Sin(circle_part * i) * R;
                Y = Math.Sin(Math.PI / 2 + circle_part * i) * R;
                Y2 = Math.Sin(Math.PI / 2 + circle_part * i) * R;

                // get info about the last data
                last_circle_precision = circle_precision;
                circle_precision = Math.Max(3, Convert.ToInt32(Math.Sin(circle_part * i) * R * SPHERE_PRECISION * 2));

                last_circle_part_horizontal = circle_part_horizontal;
                circle_part_horizontal = Math.PI * 2 / circle_precision;

                previous_sphere_angle = 0;
                current_sphere_angle = 0;
                last_circle_point = 0;

                last_row_start_point = row_start_point;
                row_start_point = Positions.Count;

                for (int j = 0; j <= circle_precision; j++)
                {

                    if (j < circle_precision)
                    {
                        X = Math.Sin(j * circle_part_horizontal) * current_R;
                        Z = Math.Cos(j * circle_part_horizontal + Math.PI) * current_R;

                        // from top
                        /*Positions.Add(new Point3D
                        {
                            X = Center.X + X,
                            Y = Center.Y + Y,
                            Z = Center.Z + Z
                        });

                        // from bottom
                        Positions.Add(new Point3D
                        {
                            X = Center.X + X,
                            Y = Center.Y - Y,
                            Z = Center.Z + Z
                        });*/
                    }

                    // two from bottom, one from top
                    if (j > 0)
                    {
                        AddIndices(new Int32[]
                        {
                            last_row_start_point + (last_circle_point % last_circle_precision) * 2,
                            row_start_point + (j % circle_precision) * 2,
                            row_start_point + ((j - 1)  % circle_precision) * 2
                        }
                        , start
                        );

                        AddIndices(new Int32[]
                        {
                            last_row_start_point + (last_circle_point % last_circle_precision) * 2 + 1,
                            row_start_point + ((j - 1)  % circle_precision) * 2 + 1,
                            row_start_point + (j % circle_precision) * 2 + 1
                        }
                        , start
                        );
                    }

                    // always increase
                    current_sphere_angle += circle_part_horizontal;

                    // increase if in current will be greater and there is greater
                    if ((current_sphere_angle > previous_sphere_angle + last_circle_part_horizontal / 2 && last_circle_point + 1 < last_circle_precision) || j == circle_precision)
                    {
                        previous_sphere_angle += last_circle_part_horizontal;
                        last_circle_point++;

                        // two from top, one from bottom
                        AddIndices(new Int32[]
                        {
                            row_start_point + (j % circle_precision) * 2,
                            last_row_start_point + ((last_circle_point - 1) % last_circle_precision) * 2,
                            last_row_start_point + (last_circle_point % last_circle_precision) * 2
                        }
                        , start
                        );

                        AddIndices(new Int32[]
                        {
                            last_row_start_point + ((last_circle_point - 1) % last_circle_precision) * 2 + 1,
                            row_start_point + (j % circle_precision) * 2 + 1,
                            last_row_start_point + (last_circle_point % last_circle_precision) * 2 + 1


                        }
                        , start
                        );
                    }
                }
            }
        }

        public void Plane(Point3D LeftTop, Point3D RightBottom)
        {
            /*int start = Positions.Count;
            Positions.Add(LeftTop);
            // RightTop
            Positions.Add(new Point3D 
            { 
                X = RightBottom.X,
                Y = LeftTop.Y,
                Z = LeftTop.Z
            });
            // LeftBottom
            Positions.Add(new Point3D
            {
                X = LeftTop.X,
                Y = RightBottom.Y,
                Z = RightBottom.Z
            });
            Positions.Add(RightBottom);

            TriangleIndices.Add(start + 0);
            TriangleIndices.Add(start + 1);
            TriangleIndices.Add(start + 3);

            TriangleIndices.Add(start + 3);
            TriangleIndices.Add(start + 2);
            TriangleIndices.Add(start + 0);

            TextureCoordinates.Add(new Point(0, 0));
            TextureCoordinates.Add(new Point(0, 1));
            TextureCoordinates.Add(new Point(1, 1));
            TextureCoordinates.Add(new Point(1, 0));

            if (Responser != null)
            {                
                Responser.RegisterStaticTriangle(
                            Positions[TriangleIndices[start + 0]],
                            Positions[TriangleIndices[start + 1]],
                            Positions[TriangleIndices[start + 3]]
                        );

                Responser.RegisterStaticTriangle(
                            Positions[TriangleIndices[start + 3]],
                            Positions[TriangleIndices[start + 2]],
                            Positions[TriangleIndices[start + 0]]
                        );
            }
        }*/
        }

        public class SeaBuilder : BasicBuilder
        {
            public SeaBuilder(Model3DGroup Container, CollisionResponser Responser, Setup Settings, double Y = 0)
                : base(Container, Responser, Settings)
            {
                this.Plane(new Point3D { X = 400, Y = Y, Z = -400 }, new Point3D { X = -400, Y = Y, Z = 400 });
                this.AddGeometryToContainer();
            }
        }

        public class BridgeBuilder : BasicBuilder, I3DBuilder
        {
            protected double HalfX;
            protected double HalfY;
            protected double HalfZ;

            protected double leftFoundXSize;

            public BridgeBuilder(Model3DGroup container, CollisionResponser Responser, Setup Settings)
                : base(container, Responser, Settings)
            {
            }

            public void Build(Rect3D rect)
            {

                /* ^ Y   ______________________
                   |    /____Top_____________/|
                   |    |   _____   _____   | |
                   |  Z |  /|    \ /|    \  | /
                   | /  |__|/    |_|/    |__|/
                   |/                          X
                   ---------------------------->
                 */

                this.Frame = rect;

                HalfX = Frame.X - Frame.SizeX / 2;
                HalfY = Frame.SizeY / 2;
                HalfZ = Frame.SizeZ / 2;

                leftFoundXSize = Frame.SizeX / 10;

                BuildTop();
                BuildFoundation();

                CollisionResponser tmp = this.Responser;

                Cube(new Rect3D(HalfX, rect.Y + rect.SizeY * 4 / 5, rect.Z, rect.SizeX, rect.SizeY / 5, rect.SizeZ));

                //this.Responser = null;

                Cube(new Rect3D(HalfX, rect.Y, rect.Z, rect.SizeX / 10, rect.SizeY * 4 / 5, rect.SizeZ));
                Cube(new Rect3D(HalfX + rect.SizeX / 2 - rect.SizeX / 20, rect.Y, rect.Z, rect.SizeX / 10, rect.SizeY * 4 / 5, rect.SizeZ));
                Cube(new Rect3D(HalfX + rect.SizeX - rect.SizeX / 10, rect.Y, rect.Z, rect.SizeX / 10, rect.SizeY * 4 / 5, rect.SizeZ));

                var MeshGeometry = new MeshGeometry3D();
                var GeometryModel = new GeometryModel3D();

                MeshGeometry.Positions = this.Positions;
                MeshGeometry.TriangleIndices = this.TriangleIndices;
                MeshGeometry.TextureCoordinates = this.TextureCoordinates;

                GeometryModel.Geometry = MeshGeometry;
                GeometryModel.Material = new DiffuseMaterial(LoadTexture());

                //Container.Children.Add(GeometryModel);
            }

            private void BuildTop()
            {


            }

            private void BuildFoundation()
            {

            }
        }

        public class StageBuilder
        {
            public double current_z, current_x, current_y;
            public double r = 4;
            public double curve_param = 0.8;
            public double max_vertical_param = Math.PI / 8;
            public TerrainPoint lastTP = null;
            public Rect3D BoundBox;
            private Point3DCollection points;
            private BasicBuilder MeshBuilder;
            private MeshGeometry3D Terrain3D;
            private Model3DGroup Container;
            private CollisionResponser Responser;
            private Setup Settings;
            public double current_rendered_point;
            private Random rnd = new Random(new System.DateTime().Millisecond);


            public enum current_method { straight_on, curve };

            public List<TerrainPoint> TerrainPoints;

            public StageBuilder(Model3DGroup Container, CollisionResponser Responser, Setup Settings)
            {
                this.Terrain3D = new MeshGeometry3D();
                this.MeshBuilder = new BasicBuilder(Container, Responser, Settings);
                this.Responser = Responser;
                this.Container = Container;
                this.Settings = Settings;

                var GeometryModel = new GeometryModel3D();
                GeometryModel.Geometry = Terrain3D;
                GeometryModel.Material = new DiffuseMaterial(LoadTexture());
                //GeometryModel.BackMaterial = new DiffuseMaterial(Brushes.Black);

                lastTP = new TerrainPoint();
                lastTP.point3d.X = 0;
                lastTP.point3d.Y = -1;
                lastTP.point3d.Z = 0;

                BoundBox = new Rect3D(0, 0, 0, 0, 0, 0);

                TerrainPoints = new List<TerrainPoint>(100);
                TerrainPoints.Add(lastTP);
                RandomTerrainPoints(100);

                //Container.Children.Add(GeometryModel);
            }

            private Brush LoadTexture()
            {
                try
                {
                    Random rnd = new Random();
                    string[] fileTxts = Directory.GetFiles(Settings.ApplicationPath + @"\textures", "road*");

                    ImageBrush texture = new ImageBrush();
                    texture.ImageSource = new BitmapImage(new Uri(fileTxts[rnd.Next(fileTxts.Length)]));
                    texture.TileMode = TileMode.Tile;
                    texture.ViewportUnits = BrushMappingMode.Absolute;

                    return texture;
                }
                catch (Exception)
                {
                    return Brushes.Orange;
                }
            }

            private void AddIndices(Int32Collection indices, int? start_pos = null)
            {
                //AddIndices(indices.ToArray(), start_pos);
            }

            private void AddIndices(Int32[] indices, int? start_pos = null)
            {
                if (!start_pos.HasValue)
                    start_pos = Terrain3D.Positions.Count;

                foreach (var triangle_point in indices)
                {
                    //this.Terrain3D.TriangleIndices.Add(triangle_point + start_pos.Value);
                }
            }

            private void AddTexturePoints(PointCollection TexturePoints, int? start_pos = null)
            {
                if (!start_pos.HasValue)
                    start_pos = Terrain3D.Positions.Count;

                /*foreach (var triangle_point in TexturePoints)
                {
                    this.Terrain3D.TextureCoordinates.Add(triangle_point);
                }*/
            }

            private void AddPositions(Point3DCollection points)
            {
                /*foreach (var triangle_point in points)
                {
                    this.Terrain3D.Positions.Add(triangle_point);
                }*/
            }


            public TerrainPoint RandomTerrainPoint(bool IsRandom = true)
            {
                TerrainPoint tmp = new TerrainPoint();

                tmp.angle = Math.Min(Math.PI * 2, Math.Max(lastTP.angle + (IsRandom ? (Settings.Random(curve_param) - curve_param / 2) : 0), -Math.PI));
                tmp.vertical_angle = Math.Min(max_vertical_param, Math.Max(lastTP.vertical_angle + (IsRandom ? ((Settings.Random() - 0.5) * max_vertical_param / 3) : 0), -max_vertical_param));

                tmp.point3d.Z = lastTP.point3d.Z + r * Math.Sin(tmp.angle);
                tmp.point3d.X = lastTP.point3d.X + r * Math.Cos(tmp.angle);
                tmp.point3d.Y = lastTP.point3d.Y + r / 4 * Math.Sin(tmp.vertical_angle);

                return tmp;
            }


            protected GameObject createTerrainObject()
            {
                return null;
            }


            /**
             * Calculates left and right side of the road
             */
            protected RoadArc CalculateHelperPoints(TerrainPoint calculating_point)
            {
                double distance = 5;
                RoadArc tmp_arc = new RoadArc();

                double add_x = Math.Cos(calculating_point.angle + Math.PI / 2) * distance;
                double add_z = Math.Sin(calculating_point.angle + Math.PI / 2) * distance;

                tmp_arc.left_x = calculating_point.point3d.X - add_x;
                tmp_arc.left_z = calculating_point.point3d.Z - add_z;

                tmp_arc.right_x = calculating_point.point3d.X + add_x;
                tmp_arc.right_z = calculating_point.point3d.Z + add_z;

                tmp_arc.left_x2 = calculating_point.point3d.X - add_x * 8;
                tmp_arc.left_z2 = calculating_point.point3d.Z - add_z;

                tmp_arc.right_x2 = calculating_point.point3d.X + add_x * 14;
                tmp_arc.right_z2 = calculating_point.point3d.Z + add_z;

                return tmp_arc;
            }

            public void RandomTerrainPoints(int count)
            {
                for (int i = 0; i < count; i++)
                {
                    TerrainPoint np = RandomTerrainPoint(i > 10);
                    lastTP = np;
                    TerrainPoints.Add(np);
                }
            }

            public void addRoad(TerrainPoint from, TerrainPoint to)
            {
            }

            public double getBorder(Point3D Where)
            {
                return 0;
            }

            public void addPointAndExtendBox(Point3D point)
            {
                if (BoundBox.X > point.X) BoundBox.X = point.X;
                if (BoundBox.Y > point.Y) BoundBox.Y = point.Y;
                if (BoundBox.Z > point.Z) BoundBox.Z = point.Z;

                if (BoundBox.SizeX < point.X - BoundBox.X) BoundBox.SizeX = point.X - BoundBox.X;
                if (BoundBox.SizeY < point.Y - BoundBox.Y) BoundBox.SizeY = point.Y - BoundBox.Y;
                if (BoundBox.SizeZ < point.Z - BoundBox.Z) BoundBox.SizeZ = point.Z - BoundBox.Z;

                //points.Add(point);
            }

            private void CreateTriangleFromPointsNumber(int a, int b, int c, int? start_from = null)
            {
                /* var col = new Int32Collection();
                 col.Add(a);
                 col.Add(b);
                 col.Add(c);

                 Point3D p0 = points[a];
                 Point3D p1 = points[b];
                 Point3D p2 = points[c];

                 //if (p0.Z < 1 || p1.Z < 1 || p2.Z < 1)
                 Responser.RegisterStaticTriangle(
                             p0,
                             p1,
                             p2
                         );

                 AddIndices(col, start_from);*/
            }

            public void getNext()
            {
                /* Point3D point;
                 //Int32Collection col = new Int32Collection();
                 PointCollection TexturePoints = new PointCollection(TerrainPoints.Count * 16);
                 points = new Point3DCollection(TerrainPoints.Count * 2);
                 int point_per_one = 4;

                 double ly = 10;
                 double ry = 10;          


                 Point3D BridgeStart = TerrainPoints[3].point3d;
                 BridgeStart.X += 5;

                 var Bridge = new BridgeBuilder(Container, Responser, Settings);
                 Bridge.Build(new Rect3D(BridgeStart, new Size3D { X = 25, Y = 4, Z = 3 }));
                 */
                /*var Sphere = new BasicBuilder(Container, Responser, Settings);
                Sphere.Sphere(new Point3D { X = 0, Y = 0.5, Z = 2 }, 2.4);
                Sphere.AddGeometryToContainer();*/

                var Sun = new BasicBuilder(Container, Responser, Settings);
                Sun.Sphere(new Point3D { X = 0, Y = 100, Z = 200 }, 5);
                Sun.AddGeometryToContainer();

                var Sea = new SeaBuilder(Container, Responser, Settings, -4);


                int start = Terrain3D.Positions.Count;
                //int start = 0;

                /*for (int i = 0; i < TerrainPoints.Count; i++)
                {

                    RoadArc tmp = CalculateHelperPoints(TerrainPoints[i]);

                    if (i > 5)
                    {
                        ly += Settings.Random() - 0.5; ly = Math.Min(30, Math.Max(ly, -30));
                        ry += Settings.Random() - 0.5; ry = Math.Min(30, Math.Max(ry, -30));
                    }

                    // droga
                    point = new Point3D(tmp.left_x, TerrainPoints[i].point3d.Y, tmp.left_z);
                    addPointAndExtendBox(point);

                    TexturePoints.Add(new Point(0.16, i));
                    point = new Point3D(tmp.right_x, TerrainPoints[i].point3d.Y, tmp.right_z);
                    addPointAndExtendBox(point);
                    TexturePoints.Add(new Point(0.84, i));


                    // lewa ściana
                    point = new Point3D(tmp.left_x2, TerrainPoints[i].point3d.Y + ly, tmp.left_z2);
                    addPointAndExtendBox(point);
                    TexturePoints.Add(new Point(0, i));

                    // prawa
                    point = new Point3D(tmp.right_x2, TerrainPoints[i].point3d.Y - ry, tmp.right_z2);
                    addPointAndExtendBox(point);
                    TexturePoints.Add(new Point(1, i));                
                }

                for (int i = 0; i < TerrainPoints.Count; i++)
                {

                    if (i < TerrainPoints.Count - 1)
                    {


                        CreateTriangleFromPointsNumber(
                            i * point_per_one + point_per_one,
                            i * point_per_one,
                            i * point_per_one + point_per_one + 1,
                            start
                            );

                        CreateTriangleFromPointsNumber(
                            i * point_per_one,
                            i * point_per_one + 1,
                            i * point_per_one + point_per_one + 1,
                            start
                            );

                        CreateTriangleFromPointsNumber(
                            i * point_per_one + 2,
                            i * point_per_one,
                            i * point_per_one + point_per_one,
                            start
                            );

                        CreateTriangleFromPointsNumber(
                            i * point_per_one + point_per_one,
                            i * point_per_one + point_per_one + 2,
                            i * point_per_one + 2,
                            start
                            );

                        CreateTriangleFromPointsNumber(
                            i * point_per_one + 1,
                            i * point_per_one + 3,
                            i * point_per_one + point_per_one + 3,
                            start
                            );

                        CreateTriangleFromPointsNumber(
                            i * point_per_one + point_per_one + 3,
                            i * point_per_one + point_per_one + 1,
                            i * point_per_one + 1,
                            start
                            );
                    }
                }*/

                AddPositions(points);
                //Terrain3D.TextureCoordinates = TexturePoints;
            }
        }
    }
}

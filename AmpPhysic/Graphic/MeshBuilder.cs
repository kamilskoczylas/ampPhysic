using System.Windows.Media.Media3D;

namespace AmpPhysic.Graphic
{
    public static class MeshBuilder
    {
        static public void GetSphere(Model3DGroup Container, Point3D Center, double R = 1)
        {
            var BasicBuild = new BasicBuilder(Container, null, null);
            BasicBuild.Sphere(Center, R);
            BasicBuild.AddGeometryToContainer();
        }
    }
}

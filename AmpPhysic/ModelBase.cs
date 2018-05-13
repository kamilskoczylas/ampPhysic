using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;

namespace AutoTest
{

    public class ModelBase
    {
        public Model3DGroup Visual3DModel { get; private set; }
        
        public ModelBase(string resourceKey)
        {

            Visual3DModel = new Model3DGroup();

            /*object testObject = Application.Current.Resources[resourceKey];
            if (testObject != null && testObject.GetType() == typeof(Model3DGroup))
                Visual3DModel.Children.Add(Application.Current.Resources[resourceKey] as Model3DGroup);*/
        }

        public ModelBase(Model3DGroup ModelGroup)
        {
            /*Visual3DModel = new Model3DGroup();            
            Visual3DModel.Children.Add(ModelGroup);*/
        }

        public void Move(Point3D CenterPosition, Vector3D Direction)
        {
            /*Transform3DGroup transform = new Transform3DGroup();
            RotateTransform3D rotateTrans = new RotateTransform3D();
            rotateTrans.Rotation = new AxisAngleRotation3D(Direction, Math.PI / 2);
            TranslateTransform3D translateTrans = new TranslateTransform3D(CenterPosition.X, CenterPosition.Y, CenterPosition.Z);
            transform.Children.Add(rotateTrans);
            transform.Children.Add(translateTrans);
            if (Visual3DModel != null)
                Visual3DModel.Transform = transform;*/
        }
    }
}

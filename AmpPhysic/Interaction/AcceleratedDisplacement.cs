using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AmpPhysic.Interaction
{
    class AcceleratedDisplacement : IDisplacement
    {
        public AcceleratedDisplacement(Point3D startingPoint, Vector3D acceleration, float deltaTime)
        {
            StartingPosition = startingPoint;
            Acceleration = acceleration;
            DeltaTime = deltaTime;
        }

        public Vector3D Acceleration { get; private set; }

        public Vector3D Velocity { get { return new Vector3D(0, 0, 0); }  }

        public float DeltaTime { get; private set; }

        public Point3D StartingPosition { get; private set; }

        public IPhysicControl PhysicObject => throw new NotImplementedException();
    }
}

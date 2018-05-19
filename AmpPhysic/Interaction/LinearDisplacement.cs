using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AmpPhysic.Interaction
{
    public class LinearDisplacement : IDisplacement
    {

        public LinearDisplacement(Vector3D velocity, Point3D startingPosition, float deltaTime)
        {
            Velocity = velocity;
            StartingPosition = startingPosition;
            DeltaTime = deltaTime;            
        }

        public LinearDisplacement(IPhysicControl physicControl, float deltaTime)
        {
            Velocity = physicControl.Velocity;
            StartingPosition = physicControl.CenterPosition;
            DeltaTime = deltaTime;
            PhysicObject = physicControl;
        }

        public Vector3D Velocity { get; private set; }

        public float DeltaTime { get; private set; }

        public Point3D StartingPosition { get; private set; }

        public IPhysicControl PhysicObject { get; private set; }

        public Vector3D GetPositionChange(float displacementTime = 0)
        {
            if (displacementTime == 0)
            {
                return Velocity * DeltaTime;
            }

            return Velocity * displacementTime;
        }
    }
}

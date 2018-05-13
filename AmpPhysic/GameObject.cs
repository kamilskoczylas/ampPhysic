using System.Windows.Media.Media3D;
using AmpPhysic.RigidBodies;
using AmpPhysic.Collision;

namespace AmpPhysic
{
    public class GameObject
    {

        protected IPhysic Physic;        

        public Point3D Position { get { return Physic.CenterPosition; } }
        public Vector3D Direction { get; set; }
        public Vector3D Velocity { get { return Physic.Velocity; } }
        public double Speed { get { return Physic.Velocity.Length; } }

        public bool CalculatePhysicYesNo;
        public bool CalculateCollisionsYesNo;
        public bool RenderMeshYesNo;


        public GameObject(bool RenderMeshYesNo = true, bool CalculatePhysicYesNo = true, bool CalculateCollisionsYesNo = true)
        {
            this.CalculatePhysicYesNo = CalculatePhysicYesNo;
            this.CalculateCollisionsYesNo = CalculateCollisionsYesNo;
            this.RenderMeshYesNo = RenderMeshYesNo;

            Physic = new PhysicPoint(1);

            Direction = new Vector3D { X = 0, Y = 0, Z = 1 };            
        }

        public GameObject(IPhysic PhysicRidgidBody) : this()
        {
            Physic = PhysicRidgidBody;
        }

        public void SetInitialVelocity(Vector3D StartingVelocity)
        {
            this.Physic.Velocity = StartingVelocity;
        }

        public void Animate(float deltaTime)
        {
            Physic.Move(deltaTime);
        }
    }
}

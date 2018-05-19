using System.Windows.Media.Media3D;
using AmpPhysic.RigidBodies;
using AmpPhysic.Collision;
using AmpPhysic.Interaction;
using System;
using System.Collections.Generic;

namespace AmpPhysic
{
    public class GameObject
    {

        protected IPhysicControl kinematicBody;        

        public Point3D Position { get { return kinematicBody.CenterPosition; } }
        public Vector3D Direction { get; set; }
        public Vector3D Velocity { get { return kinematicBody.Velocity; } }
        public double Speed { get { return kinematicBody.Velocity.Length; } }

        public bool CalculatePhysicYesNo;
        public bool CalculateCollisionsYesNo;
        public bool RenderMeshYesNo;
        public bool CurrentlyStaticYesNo { get { return kinematicBody.CurrentlyStaticYesNo; } }        


        public GameObject(bool RenderMeshYesNo = true, bool CalculatePhysicYesNo = true, bool CalculateCollisionsYesNo = true)
        {
            this.CalculatePhysicYesNo = CalculatePhysicYesNo;
            this.CalculateCollisionsYesNo = CalculateCollisionsYesNo;
            this.RenderMeshYesNo = RenderMeshYesNo;
            
            Direction = new Vector3D { X = 0, Y = 0, Z = 1 };            
        }

        public event ObjectCollisionHandler CollisionEvent;

        protected virtual void OnCollision(CollisionEventArgs e)
        {
            CollisionEvent?.Invoke(this, e);
        }

        public void Hit(CollisionResponse collisionResponse)
        {
            OnCollision(
                new CollisionEventArgs(collisionResponse)
                );
        }

        public GameObject(IPhysicControl PhysicRidgidBody) : this()
        {
            kinematicBody = PhysicRidgidBody;
        }

        public void SetInitialVelocity(double x, double y, double z)
        {
            kinematicBody.Velocity = new Vector3D(x, y, z);
        }

        public void SetPosition(double x, double y, double z)
        {
            kinematicBody.CenterPosition = new Point3D(x, y, z);
        }        

        public void CalculateDisplacement(float deltaTime)
        {            
            kinematicBody.CalculateDisplacement(deltaTime);
        }

        public List<IDisplacement> GetDisplacements()
        {
            return kinematicBody.GetDisplacements();
        }

        public void CommitDisplacement()
        {
            kinematicBody.CommitDisplacement();
        }

        public void CommitDisplacementPartially(float commitedTime)
        {
            kinematicBody.CommitDisplacementPartially(commitedTime);
        }

        public void AddForce(Force force)
        {
            kinematicBody.AddForce(force);
        }

        public void ResetDisplacement()
        {
            kinematicBody.ResetDisplacement();
        }
    }
}

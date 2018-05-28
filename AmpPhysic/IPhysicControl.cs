using AmpPhysic.Interaction;
using AmpPhysic.Collision;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace AmpPhysic
{
    public interface IPhysicControl : IPhysic
    {        

        void AddForce(Force force);

        void CalculateDisplacement(float deltaTime);
        void CommitDisplacement();
        void CommitDisplacementPartially(float commitedTime);
        void ResetDisplacement();

        CollisableArea GetCollisionArea();
        ColliderShape GetColliderShape();
        List<IDisplacement> GetDisplacements();
        Vector3D Acceleration { get; }

        void HandleCollision(CollisionResponse collisionResponse);
        bool CurrentlyStaticYesNo { get; }
            
        GameObject GameObject { get; set; }

    }
}

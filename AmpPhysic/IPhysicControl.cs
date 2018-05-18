using AmpPhysic.Interaction;
using AmpPhysic.Collision;

namespace AmpPhysic
{
    public interface IPhysicControl : IPhysic
    {        

        void AddForce(Force force);

        void CalculateDisplacement(float deltaTime);
        void CommitDisplacement();
        void CommitDisplacementPartially(float commitedTime, float totalDeltaTime);
        void ResetDisplacement();

        ColliderShape GetColliderShape();
        void HandleCollision(CollisionResponse collisionResponse);
        
    }
}

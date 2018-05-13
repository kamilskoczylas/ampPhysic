using System.Windows.Media.Media3D;

namespace AmpPhysic.RigidBodies
{
    public enum PhysicConnectorTypes { static_connection, dynamic_connection }

    public class PhysicConnector
    {
        public PhysicConnectorTypes Type;
        public Vector3D PositionFromParentCenter;
        public Vector3D TransferForceDirection;
        public IPhysic ParentObject;
        public IPhysic ChildObject;

        public PhysicConnector(IPhysic Parent, IPhysic Child, PhysicConnectorTypes ConnectionType, Vector3D PositionFromParentCenter, Vector3D TransferForceDirection)
        {
            this.ParentObject = Parent;
            this.ChildObject = Child;
            this.Type = ConnectionType;
            this.PositionFromParentCenter = PositionFromParentCenter;
            if (TransferForceDirection != null)
                this.TransferForceDirection = TransferForceDirection;
            else
                TransferForceDirection = new Vector3D(1, 1, 1);

            //Child.TryToMove(this);
        }
    }
}

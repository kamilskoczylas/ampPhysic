using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmpPhysic.Collision
{
    public class CollisableArea
    {
        private double X { get; set; }
        private double dX { get; set; }
        private double Z { get; set; }
        private double dZ { get; set; }
        private double X2 { get; set; }
        private double Z2 { get; set; }
        
        public object LinkedEntity { get; private set; }

        public CollisableArea(double X, double Z, double dX, double dZ, object linkedEntity)
        {
            // sections must be ordered to run fast collision algorythm
            // X < X + dX
            // Z < Z + dZ

            LinkedEntity = linkedEntity;

            if (dX > 0)
            { 
                this.X = X;
                this.dX = dX;
            } else
            {
                this.X = X + dX;
                this.dX = -dX;
            }

            if (dZ > 0)
            {
                this.Z = Z;
                this.dZ = dZ;
            }
            else
            {
                this.Z = Z + dZ;
                this.dZ = -dZ;
            }            

            X2 = X + dX;
            Z2 = Z + dZ;
        }

        public bool Expand(CollisableArea anotherSection)
        {
            if (IsCollidingWith(anotherSection))
            {
                if (anotherSection.X < X)                
                    X = anotherSection.X;

                if (anotherSection.Z < Z)
                    Z = anotherSection.Z;

                if (anotherSection.X2 > X2)
                    X2 = anotherSection.X2;

                if (anotherSection.Z2 < Z2)
                    Z2 = anotherSection.Z2;

                dX = X2 - X;
                dZ = Z2 - Z;


                return true;
            }
            else
                return false;

        }

        public bool IsCollidingWith(CollisableArea anotherSection)
        {
            return
                (X <= anotherSection.X2) && (anotherSection.X >= X2) &&
                (Z <= anotherSection.Z2) && (anotherSection.Z >= Z2);

        }
    }
}

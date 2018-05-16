using System.Windows.Media.Media3D;
using System.Diagnostics;

namespace AmpPhysic.Interaction
{

    public enum ForceType { once, duration, constant }

    public class Force
    {
        private Stopwatch CurrentTime;
        private Vector3D ForceVectorDirection;

        public ForceType Type { get; private set; }
        public double ForceNewtonsValue { get; private set; }

        private int ForceDurationMiliseconds;

        public Vector3D Direction { 
            get {
                if (Type == ForceType.duration)
                {
                    if (CurrentTime.ElapsedMilliseconds > ForceDurationMiliseconds)
                    {
                        CurrentTime.Stop();
                        CurrentTime = null;

                        ForceVectorDirection.X = 0;
                        ForceVectorDirection.Y = 0;
                        ForceVectorDirection.Z = 0;
                    }
                }
                return ForceVectorDirection;
            }

            private set {
                ForceVectorDirection = value;
            }
        }

        private void SetDuration(int DurationMiliseconds = 0)
        {
            if (DurationMiliseconds > 0)
            {
                Type = ForceType.duration;
                ForceDurationMiliseconds = DurationMiliseconds;
                CurrentTime = new Stopwatch();
                CurrentTime.Start();
            }
        }

        public Force(double ForceNewtonsValue, Vector3D Direction, ForceType type, int DurationMiliseconds = 0)
        {
            this.ForceNewtonsValue = ForceNewtonsValue;
            this.Direction = Direction;
            this.Direction.Normalize();

            Type = type;

            SetDuration(DurationMiliseconds);
        }

        public Force(double ForceNewtonsValue, double x, double y, double z, ForceType type, int DurationMiliseconds = 0)
        {
            this.Direction = new Vector3D(x, y, z);
            this.Direction.Normalize();

            this.ForceNewtonsValue = ForceNewtonsValue;
            Type = type;

            SetDuration(DurationMiliseconds);
        }
        

        public Force(double ForceNewtonsValue, Vector3D Direction, int DurationMiliseconds = 0)
        {
            this.Direction = Direction;
            this.Direction.Normalize();

            this.ForceNewtonsValue = ForceNewtonsValue;

            if (DurationMiliseconds > 0)
            {
                SetDuration(DurationMiliseconds);
            }
            else
            {                
                Type = ForceType.once;
            }
        }
    }
}

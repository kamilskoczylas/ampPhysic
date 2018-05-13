using System.Windows.Media.Media3D;
using System.Diagnostics;

namespace AmpPhysic.Interaction
{

    public enum ForceType { once, duration, constant }    

    public class Force
    {
        private Stopwatch CurrentTime;
        private Vector3D ForceVectorValue;        
        public ForceType Type { get; private set; }

        private int ForceDurationMiliseconds;

        public Vector3D Value { 
            get {
                if (Type == ForceType.duration)
                {
                    if (CurrentTime.ElapsedMilliseconds > ForceDurationMiliseconds)
                    {
                        CurrentTime.Stop();
                        CurrentTime = null;

                        ForceVectorValue.X = 0;
                        ForceVectorValue.Y = 0;
                        ForceVectorValue.Z = 0;
                    }
                }
                return ForceVectorValue;
            }

            private set {
                ForceVectorValue = value;
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

        public Force(Vector3D Direction, ForceType type, int DurationMiliseconds = 0)
        {
            Value = Direction;
            Type = type;

            SetDuration(DurationMiliseconds);
        }

        public Force(Vector3D Direction, int DurationMiliseconds = 0)
        {
            Value = Direction;

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

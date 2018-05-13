using System;

namespace AmpPhysic
{
    public class Setup
    {
        public string ApplicationPath { get; private set; }
        public int PRECISION_3D_PER_ONE_UNIT_SIZE = 12; // min. to look nice 12
        public int MUSIC_VOLUME = 1;
        public Random Randomizer;

        public Setup()
        {
            ApplicationPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location).Replace(@"\\", @"\");

            string tmp = @"bin\Debug";
            if (ApplicationPath.Substring(ApplicationPath.Length - tmp.Length) == tmp)
                ApplicationPath = ApplicationPath.Substring(0, ApplicationPath.Length - tmp.Length);

            Randomizer = new Random(System.DateTime.Now.Millisecond);
        }

        public double Random(double Max = 1)
        {
            return Max * Randomizer.NextDouble();
        }
    }
}

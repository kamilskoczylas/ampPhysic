This is a hobby & training project to imitate the static and dynamic of physic

A cpu physic engine basis for simple games
The goal is to use this engine for a car racing simple game

Tests are based on SpecFlow.


![Example in use]([http://url/to/img.png](https://github.com/kamilskoczylas/ampPhysic/blob/master/3dcar-engine.png))

'''
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.ComponentModel;

namespace AutoTest
{
    public class Game
    {
        public Setup Settings;
        public Stage stage;
        public MP3Player Music;
        private string[] MusicFiles;
        public Logger Log;

        public GameKeys Controls = new GameKeys();
        public Camera Camera;
        protected CarObject PlayerCar;

        private List<IAnimateObject> AnimatedObjects;
        private VirtualWorld World;

        public Game(PerspectiveCamera WpfCamera)
        {            
            /*Keys.Add(new KeyboardKey(System.Windows.Forms.Keys.));
            Keys.Add(new KeyboardKey(Controls.BRAKE, Controls));
            Keys.Add(new KeyboardKey(Controls.LEFT, Controls));
            Keys.Add(new KeyboardKey(Controls.RIGHT, Controls));*/            

            World = new VirtualWorld();
            AnimatedObjects = new List<IAnimateObject>();
            Settings = new Setup();

            Log = new Logger(Settings.ApplicationPath + "AutoTest.log");
            LoadFiles();
            Camera = new Camera(WpfCamera);
        }

        private void LoadFiles()
        {
            try
            {
                MusicFiles = Directory.GetFiles(Settings.ApplicationPath + @"music\", "*.mp3");
                Music = new MP3Player();
            }
            catch (Exception)
            {
            }
        }

        public void NewStage(Model3DGroup StageContainer)
        {
            stage = new Stage(StageContainer, World.GetResponser(), Settings);
            stage.Build();

            SetMusic();
        }

        public void Draw()
        {
            Camera.set();

            foreach (var AnimateObject in AnimatedObjects)
            {
                AnimateObject.Draw();
            }
        }

        private void SetMusic()
        {
            if (Settings.MUSIC_VOLUME == 0)
                return;

            Random rnd = new Random();
            Music.Open(MusicFiles[rnd.Next(MusicFiles.Length)]);
            Music.Play(true);
        }

        public INotifyPropertyChanged AddControlledCar(Model3DGroup Container)
        {
            //var CarVM = new BasicCar(Container, Settings, "car-1", World.GetResponser());
            var CarVM = new PhysicCar(Container);
            PlayerCar = new CarObject(Container, CarVM, World.GetResponser(), Settings);
            World.AddCar(PlayerCar);
            AnimatedObjects.Add(PlayerCar);
            return PlayerCar;
        }

        public void AddCar(CarObject Car)
        {
            this.World.AddCar(Car);
            AnimatedObjects.Add(Car);
        }

        public void AddBox(Box Box)
        {
            //this.World.AddObject(Box);
            //AnimatedObjects.Add(Car);
        }

        public void Animate()
        {
            World.Animate(Controls.Keys);
            Camera.go_to_object(PlayerCar);
        }
    }
}
'''

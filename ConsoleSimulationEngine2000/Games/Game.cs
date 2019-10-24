using System.Collections.Generic;

namespace ConsoleSimulationEngine2000.Games
{
    public class Game : Simulation
    {
        private readonly ConsoleGUI gui;

        public override List<BaseDisplay> Displays { get { return CurrentScene.Displays; } }

        public Game(ConsoleGUI gui)
        {
            this.gui = gui;
        }
        public Scene CurrentScene { get; private set; }

        public void SetScene(Scene scene)
        {
            gui.Input = scene.Input;
            scene.SetGame(this);
            CurrentScene = scene;
        }

        public override void Update(int deltaTime)
        {
            CurrentScene?.PassTime(deltaTime);

        }
    }
}

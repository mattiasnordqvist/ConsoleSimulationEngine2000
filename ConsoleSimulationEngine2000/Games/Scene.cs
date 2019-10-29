using System.Collections.Generic;

namespace ConsoleSimulationEngine2000.Games
{
    public abstract class Scene
    {
        public Game Game { get; private set; }
        public List<IDisplay> Displays { get; set; } = new List<IDisplay>();
        public IInput Input { get; set; } = new KeyInput();

        internal void SetGame(Game game)
        {
            Game = game;
        }

        protected internal abstract void Update(int deltaTime);
    }
}
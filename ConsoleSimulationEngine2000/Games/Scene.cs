using System.Collections.Generic;

namespace ConsoleSimulationEngine2000.Games
{
    public abstract class Scene
    {
        public Game Game { get; private set; }
        public List<BaseDisplay> Displays { get; set; } = new List<BaseDisplay>();
        public IInput Input { get; set; } = new KeyInput();

        internal void SetGame(Game game)
        {
            Game = game;
        }

        protected internal abstract void PassTime(int deltaTime);
    }
}
using System.Collections.Generic;


namespace ConsoleSimulationEngine2000
{
    public abstract class Simulation
    {
        /// <summary>
        /// Returns the displays to be printed in Console
        /// </summary>
        public abstract List<BaseDisplay> Displays { get; }

        /// <summary>
        /// Advances simulation state by deltaTime
        /// </summary>
        /// <param name="deltaTime">The number of milliseconds that have passed since last time time passed</param>
        public abstract void Update(int deltaTime);
    }
}

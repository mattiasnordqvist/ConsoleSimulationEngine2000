﻿using System.Collections.Generic;


namespace ConsoleSimulationEngine2000
{
    public abstract class Simulation
    {
        public int FPS { get; set; } = 20;
        public int SimulationTickPeriod { get; set; } = 1000;

        /// <summary>
        /// Returns the displays to be printed in Console
        /// </summary>
        public abstract List<BaseDisplay> Displays { get; }

        /// <summary>
        /// Gets the input
        /// </summary>
        public Input Input { get; internal set; }

        /// <summary>
        /// Advances simulation state by deltaTime
        /// </summary>
        /// <param name="deltaTime">The number of milliseconds that have passed since last time time passed</param>
        public abstract void PassTime(int deltaTime);
    }
}

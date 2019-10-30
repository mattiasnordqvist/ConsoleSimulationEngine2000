using System;
using System.Drawing;
using System.Linq;
using Pastel;

namespace ConsoleSimulationEngine2000
{
    public class DebugDisplay : BorderedDisplay
    {
        private ConsoleGUI gui;
        private readonly int averageSampleSize;

        public DebugDisplay(ConsoleGUI consoleGUI, int x, int y, int averageSampleSize = 100)
            : base(x, y, 20, 5)
        {
            gui = consoleGUI;
            this.averageSampleSize = averageSampleSize;
            l = new double[averageSampleSize];
            b = new double[averageSampleSize];
            msFromTargetUpdate = new double[averageSampleSize];
            UseCache = false;
        }
        private double[] l;
        private double[] b;
        private double[] msFromTargetUpdate;
        private int count = 0;
        private int index = 0;

        protected internal override void Update()
        {
            l[index] = gui.LastUpdateTime;
            b[index] = gui.BackBufferRenderTime;
            msFromTargetUpdate[index] = gui.LastUpdateDelta - gui.TargetUpdateTime;
            count++;
            index++;
            if (count > averageSampleSize)
            {
                count = averageSampleSize;
            }
            if (index == averageSampleSize)
            {
                index = 0;
            }
            Value =
                ($"Update: {Math.Round(l.Sum() / count)} {(gui.UpdateLag ? "(LAG)" : "")}{Environment.NewLine}" +
                $"Update diff: {Math.Round(msFromTargetUpdate.Sum() / count)} {Environment.NewLine}" +
                $"Render: {Math.Round(b.Sum() / count)} {(gui.RenderLag ? "(LAG)" : "")}").Pastel(Color.Goldenrod);
        }
    }
}

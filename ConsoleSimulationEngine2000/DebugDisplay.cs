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
            : base(x, y, 20, 7)
        {
            gui = consoleGUI;
            this.averageSampleSize = averageSampleSize;
            l = new double[averageSampleSize];
            b = new double[averageSampleSize];
            s = new double[averageSampleSize];
            msFromTargetUpdate = new double[averageSampleSize];
            UseCache = false;
        }
        private double[] l;
        private double[] b;
        private double[] s;
        private double[] msFromTargetUpdate;
        private int count = 0;
        private int index = 0;

        protected internal override string GetStringToDisplay()
        {
            l[index] = gui.LastUpdateTime.TotalMilliseconds;
            b[index] = gui.BackBufferRenderTime.TotalMilliseconds;
            s[index] = gui.ScreenRenderTime.TotalMilliseconds;
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
                ($"Update: {Math.Round(l.Sum() / count)} {Environment.NewLine}" +
                $"Update diff: {Math.Round(msFromTargetUpdate.Sum() / count)} {Environment.NewLine}" +
                $"Backbuffer: {Math.Round(b.Sum() / count)} {Environment.NewLine}" +
                $"Print: {Math.Round(s.Sum() / count)} {Environment.NewLine}" +
                $"Render total: {Math.Round((s.Sum()+ b.Sum()) / count)}").Pastel(Color.Goldenrod);
            return Value;

        }
    }
}

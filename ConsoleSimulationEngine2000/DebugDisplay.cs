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

        public DebugDisplay(ConsoleGUI consoleGUI, int x, int y, int w, int h, int averageSampleSize = 100) : base(x,y,w,h)
        {
            gui = consoleGUI;
            this.averageSampleSize = averageSampleSize;
            l = new double[averageSampleSize];
            b = new double[averageSampleSize];
            s = new double[averageSampleSize];
        }
        private double[] l;
        private double[] b;
        private double[] s;
        private int count = 0;
        private int index = 0;

        protected internal override string GetStringToDisplay()
        {
            l[index] = gui.LastUpdateTime.TotalMilliseconds;
            b[index] = gui.BackBufferRenderTime.TotalMilliseconds;
            s[index] = gui.ScreenRenderTime.TotalMilliseconds;
            count++;
            index++;
            if (count > averageSampleSize)
            {
                count = averageSampleSize;
            }
            if(index == averageSampleSize)
            {
                index = 0;
            }
            Value = 
                ($"Update: {Math.Round(l.Sum() / count)} {Environment.NewLine}" +
                $"Render: {Math.Round(b.Sum() / count)} {Environment.NewLine}" +
                $"Print: {Math.Round(s.Sum() / count)}").Pastel(Color.Goldenrod);
            return Value;
           
        }
    }
}

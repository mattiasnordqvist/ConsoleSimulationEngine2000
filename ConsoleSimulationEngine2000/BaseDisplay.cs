using System;
using System.Linq;
using System.Text;

namespace ConsoleSimulationEngine2000
{
    /// <summary>
    /// Creates a display to be shown at position (x, y) with the given width and height. 
    /// </summary>
    /// <param name="x">The x position of the display. Negative values are interpreted as from right side of window</param>
    /// <param name="y">The y position of the display. Negative values are interpreted as from bottom of window</param>
    /// <param name="width">Width of display. Negative values means width should be subtracted from Window width.</param>
    /// <param name="height">Height of display. Negative values means width should be subtracted from Window height.</param>
    public abstract class BaseDisplay
    {
        public BaseDisplay() { }
        public BaseDisplay(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            if (GetHeight() == 0)
            {
                throw new ArgumentException("Height cannot be zero");
            }
            if (GetWidth() == 0)
            {
                throw new ArgumentException("Width cannot be zero");
            }
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        internal CharMatrix GetCharMatrix()
        {
            var w = GetWidth();
            var h = GetHeight();
            var m = new char[h][];

            var lines = GetStringToDisplay(true).Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int y = 0; y < lines.Length; y++)
            {
                m[y] = lines[y].PadRight(w).ToCharArray();
            }
            for (int y = lines.Length; y < m.Length; y++)
            {
                m[y] = new char[w];
            }

            return new CharMatrix(m, GetX(), GetY(), w, h);
        }

        internal abstract string GetStringToDisplay(bool optimizedForPerformance);

        internal virtual int GetX()
        {
            if (X < 0)
            {
                return Console.WindowWidth + (X);
            }
            else
            {
                return X;
            }
        }

        internal virtual int GetY()
        {
            if (Y < 0)
            {
                return Console.WindowHeight + (Y);
            }
            else
            {
                return Y;
            }
        }

        internal virtual int GetWidth()
        {
            if (Width < 0)
            {
                return Console.WindowWidth + (Width + 1);
            }
            else
            {
                return Width;
            }
        }

        internal virtual int GetHeight()
        {
            if (Height < 0)
            {
                return Console.WindowHeight + (Height + 1);
            }
            else
            {
                return Height;
            }
        }
    }
}
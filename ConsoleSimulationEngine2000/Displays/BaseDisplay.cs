using System;
using System.Linq;

namespace ConsoleSimulationEngine2000
{
    /// <summary>
    /// Creates a display to be shown at position (x, y) with the given width and height. 
    /// </summary>
    /// <param name="x">The x position of the display. Negative values are interpreted as from right side of window</param>
    /// <param name="y">The y position of the display. Negative values are interpreted as from bottom of window</param>
    /// <param name="width">Width of display. Negative values means width should be subtracted from Window width.</param>
    /// <param name="height">Height of display. Negative values means width should be subtracted from Window height.</param>
    public abstract class BaseDisplay : IDisplay
    {
        private string value = "";
        private ICharMatrix cached;
        protected bool UseCache = true;

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

        public ICharMatrix GetCharMatrix()
        {
            Update();
            if (!UseCache || cached == null)
            {
                cached = GetCharMatrix(Value);
            }
            return cached;
        }

        protected internal virtual void Update()
        {
        }

        protected internal abstract ICharMatrix GetCharMatrix(string value);

        public string Value
        {
            get { return value; }
            set
            {
                cached = null;
                this.value = value;
            }
        }

        public virtual int GetX()
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

        public virtual int GetY()
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

        public virtual int GetWidth()
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

        public virtual int GetHeight()
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

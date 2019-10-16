using System;

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
            var m = new (char c, string pre, string post)[h][];

            var lines = GetStringToDisplay().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int y = 0; y < m.Length; y++)
            {
                m[y] = new (char c, string pre, string post)[w];
            }
            string lastColor = null;
            var inColor = false;
            var colorStart = -1;
            string color = null;
            var colorEnding = false;
            for (int y = 0; y < lines.Length; y++)
            {
                if (lastColor != null)
                {
                    color = lastColor;
                }
                var skipped = 0;
                for (int x = 0; x < w && x + skipped < lines[y].Length;)
                {
                    var c = lines[y][x + skipped];
                    if (!inColor)
                    {
                        if (c == '\u001b')
                        {
                            inColor = true;
                            if (colorStart == -1)
                            {
                                colorStart = x + skipped;
                                skipped += 6;
                            }
                            else
                            {
                                colorEnding = true;
                                skipped += 2;
                                colorStart = -1;

                            }
                            skipped++;
                        }
                        else
                        {
                            m[y][x] = (c, color, null);
                            color = null;
                            x++;
                        }
                    }
                    else
                    {
                        if (c == 'm')
                        {
                            if (colorStart > -1)
                            {
                                color = lines[y][colorStart..(x + skipped + 1)];
                                lastColor = color;
                            }
                            if (colorEnding)
                            {
                                if (color == null)
                                {
                                    m[y][x - 1].post = "\u001b[0m";
                                }
                                else
                                {
                                    color = null;
                                }
                                colorEnding = false;
                                lastColor = null;
                            }

                            inColor = false;
                        }
                        skipped++;
                    }
                }
                if (lastColor != null)
                {
                    m[y][^1].post = "\u001b[0m";
                }
                if (colorEnding)
                {
                    if (color == null)
                    {
                        m[y][^1].post = "\u001b[0m";
                    }
                    else
                    {
                        color = null;
                    }
                    colorEnding = false;
                }
              
            }

            return new CharMatrix(m, GetX(), GetY(), w, h);
        }

        protected internal abstract string GetStringToDisplay();

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
using System;
using System.Drawing;
using System.Linq;
using ArtisticPastelPainter;

namespace ConsoleSimulationEngine2000
{
    public class Display
    {
        /// <summary>
        /// Creates a display to be shown at position (x, y) with the given width and height. 
        /// </summary>
        /// <param name="x">The x position of the display. Negative values are interpreted as from right side of window</param>
        /// <param name="y">The y position of the display. Negative values are interpreted as from bottom of window</param>
        /// <param name="width">Width including border. Negative values means width should be subtracted from Window width.</param>
        /// <param name="height">Height including border. Negative values means width should be subtracted from Window height.</param>
        public Display(int x, int y, int width, int height)
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
        public int X { private get; set; }
        public int Y { private get; set; }

        /// <summary>
        /// The text to display.
        /// </summary>
        public string Value { get; set; } = "";
        public int Width { private get; set; }
        public int Height { private get; set; }

        /// <summary>
        /// Color of border
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Color of text
        /// </summary>
        public Color TextColor { get; set; } = Color.White;

        internal int GetX()
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

        internal int GetY()
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

        internal void Print(object cursorLock)
        {
            lock (cursorLock)
            {
                var previousCursorPosition = (Console.CursorLeft, Console.CursorTop);

                var (x,y) = (GetX(), GetY());
                Console.SetCursorPosition(x, y);
                Console.Write(new ArtisticString("#" + "-".PadRight(GetWidth() - 2, '-') + "#", Color));
                var lines = Value.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < GetHeight()-2; i++)
                {
                    Console.SetCursorPosition(x, y + i + 1);
                    if (lines.Count() > i)
                    {
                        Console.Write(new ArtisticString("| ", Color) + new ArtisticString(lines[i].PadRight(GetWidth() - 4).Substring(0, GetWidth() - 4), TextColor) + new ArtisticString(" |", Color));
                    }
                    else
                    {
                        Console.Write(new ArtisticString("| ", Color) + "".PadRight(GetWidth() - 4).Substring(0, GetWidth() - 4) + new ArtisticString(" |", Color));
                    }
                }
                Console.SetCursorPosition(x, y + GetHeight()-1);
                Console.Write(new ArtisticString("#" + "-".PadRight(GetWidth() - 2, '-') + "#", Color));
                Console.SetCursorPosition(previousCursorPosition.CursorLeft, previousCursorPosition.CursorTop);
            }
        }

        internal int GetWidth()
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

        internal int GetHeight()
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
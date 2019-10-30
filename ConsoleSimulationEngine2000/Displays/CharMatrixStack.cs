using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSimulationEngine2000
{
    public class CharMatrixStack : ICharMatrix
    {
        private List<ICharMatrix> stack;

        public int Y => stack.Min(x => x.Y);

        public int H => stack.Max(x => x.Y + x.H) - Y;

        public int X => stack.Min(x => x.X);

        public int W => stack.Max(x => x.X + x.W) - X;

        public CharMatrixStack(int size)
        {
            stack = new List<ICharMatrix>(size);
        }
        internal void Add(ICharMatrix cm)
        {
            stack.Add(cm);
        }

        public string this[int x, int y]
        {
            get
            {
                for (int l = stack.Count - 1; l >= 0; l--)
                {
                    var s = stack[l];
                    if (y >= s.Y && y < s.Y + s.H)
                    {
                        if (x >= s.X && x < s.X + s.W)
                        {
                            var c = s[x, y];
                            if (!(c == null || c.EndsWith('\0')))
                            {
                                return c;
                            }
                        }
                    }
                }
                return "\u001b[0m ";
            }
        }

        internal string ToString(int windowWidth, int windowHeight)
        {
            var sb = new StringBuilder();
            for (int y = 0; y < windowHeight; y++)
            {
                for (int x = 0; x < windowWidth; x++)
                {
                    var a = this[x, y];
                    sb.Append(a);

                }
                if (y != windowHeight - 1)
                { sb.AppendLine(); }
            }
            return sb.ToString();
        }
    }
}
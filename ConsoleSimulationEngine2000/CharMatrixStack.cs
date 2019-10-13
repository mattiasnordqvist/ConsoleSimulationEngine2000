using System.Collections.Generic;
using System.Text;

namespace ConsoleSimulationEngine2000
{
    internal class CharMatrixStack
    {
        private List<CharMatrix> stack;

        public CharMatrixStack(int size)
        {
            stack = new List<CharMatrix>(size);
        }
        internal void Add(CharMatrix cm)
        {
            stack.Add(cm);
        }

        internal char this[int x, int y]
        {
            get
            {
                for (int l = 1; l <= stack.Count; l++)
                {
                    var s = stack[^l];
                    if (y >= s.y && y < s.y + s.h)
                    {
                        if (x >= s.x && x < s.x + s.w)
                        {
                            var c = s.m[y - s.y][x - s.x];
                            if (c != ' ')
                            {
                                return c;
                            }
                        }
                    }
                }
                return ' ';
            }
        }

        internal string ToString(int windowWidth, int windowHeight)
        {
            var sb = new StringBuilder();
            for (int y = 0; y < windowHeight; y++)
            {
                for (int x = 0; x < windowWidth; x++)
                {
                    sb.Append(this[x, y]);
                }
                if (y != windowHeight - 1)
                { sb.AppendLine(); }
            }
            return sb.ToString();
        }
    }
}
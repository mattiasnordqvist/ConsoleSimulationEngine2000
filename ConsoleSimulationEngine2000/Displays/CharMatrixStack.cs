using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSimulationEngine2000
{
    public class CharMatrixStack : ICharMatrix
    {
        private List<ICharMatrix> stack;

        public int y => stack.Min(x => x.y);

        public int h => stack.Max(x => x.y + x.h) - y;

        public int x => stack.Min(x => x.x);

        public int w => stack.Max(x => x.x + x.w) - x;

        public CharMatrixStack(int size)
        {
            stack = new List<ICharMatrix>(size);
        }
        internal void Add(ICharMatrix cm)
        {
            stack.Add(cm);
        }

        public (char c, string pre, string post) this[int x, int y]
        {
            get
            {
                if (x < this.x || x > this.x + w || y < this.y || y > this.y + h)
                {
                    throw new IndexOutOfRangeException();
                }
                for (int l = 1; l <= stack.Count; l++)
                {
                    var s = stack[^l];
                    if (y >= s.y && y < s.y + s.h)
                    {
                        if (x >= s.x && x < s.x + s.w)
                        {
                            var c = s[x, y];
                            if (c.Item1 != '\0')
                            {
                                return c;
                            }
                        }
                    }
                }
                return (' ', null, null);
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
                    if (a.pre != null && a.post != null)
                    {
                        sb.Append(a.pre + a.c + a.post);
                    }
                    else if (a.post != null)
                    {
                        sb.Append(a.c + a.post);
                    }
                    else if (a.pre != null)
                    {
                        sb.Append(a.pre + a.c);
                    }
                    else
                    {
                        sb.Append(a.c);
                    }

                }
                if (y != windowHeight - 1)
                { sb.AppendLine(); }
            }
            return sb.ToString();
        }

        internal (char, string, string)[][] ToArray(int windowWidth, int windowHeight)
        {
            var a = new (char, string, string)[windowHeight][];
            for (int y = 0; y < windowHeight; y++)
            {
                a[y] = new (char, string, string)[windowWidth];
                for (int x = 0; x < windowWidth; x++)
                {
                    a[y][x] = this[x, y];
                }
            }
            return a;
        }
    }
}
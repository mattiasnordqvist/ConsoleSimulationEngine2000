using System;

namespace ConsoleSimulationEngine2000
{
    public class CharMatrix : ICharMatrix
    {
        private (char c, string pre, string post)[][] m;
        public int x { get; }
        public int y { get; }
        public int w { get; }
        public int h { get; }

        internal CharMatrix((char c, string pre, string post)[][] m, int x, int y, int w, int h)
        {
            this.m = m;
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public (char c, string pre, string post) this[int x, int y] => m[y][x];


        public static CharMatrix Create(string s, int x, int y, int w, int h)
        {
            var m = new (char c, string pre, string post)[h][];

            var lines = s.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int yi = 0; yi < m.Length; yi++)
            {
                m[yi] = new (char c, string pre, string post)[w];
            }

            string currentColor = null;
            for (int yi = 0; yi < lines.Length; yi++)
            {
                int xi = 0;
                foreach (var c in lines[yi].EnumerateWithColorInfo(w))
                {
                    if (c.pre != null)
                    {
                        currentColor = c.pre;
                    }
                    m[yi][xi] = c;
                    m[yi][xi].pre = currentColor;
                    if (currentColor != null && xi == w - 1)
                    {
                        m[yi][xi].post = "\u001b[0m";
                    }
                    if(c.post != null)
                    {
                        currentColor = null;
                    }
                    xi++;

                }
            }

            return new CharMatrix(m, x, y, w, h);
        }
    }
}
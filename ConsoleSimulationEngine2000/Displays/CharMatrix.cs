namespace ConsoleSimulationEngine2000
{
    public class CharMatrix : ICharMatrix
    {
        private (char c, string pre)[][] m;
        public int X { get; }
        public int Y { get; }
        public int W { get; }
        public int H { get; }

        internal CharMatrix((char c, string pre)[][] m, int x, int y, int w, int h)
        {
            this.m = m;
            this.X = x;
            this.Y = y;
            this.W = w;
            this.H = h;
        }

        public (char c, string pre) this[int x, int y] => m[y - this.Y][x - this.X];


        public static CharMatrix Create(string s, int x, int y, int w, int h)
        {
            var m = EmptyArray(w, h);
            int yi = 0, xi = 0;
            foreach (var c in s.EnumerateWithColorInfo())
            {
                if (c.c == '\r')
                {
                    continue;
                }
                if (c.c == '\n')
                {
                    xi = 0;
                    yi++;
                    continue;
                }
                if (yi >= m.Length || xi >= m[yi].Length)
                {
                    xi++;
                    continue;
                }
                m[yi][xi] = c;
                xi++;
            }
            return new CharMatrix(m, x, y, w, h);
        }

        private static (char c, string pre)[][] EmptyArray(int w, int h)
        {
            var m = new (char c, string pre)[h][];
            for (int yi = 0; yi < m.Length; yi++)
            {
                m[yi] = new (char c, string pre)[w];
                for (int xi = 0; xi < w; xi++)
                {
                    m[yi][xi].c = ' ';
                    m[yi][xi].pre = ColoredStringExt.End;

                }
            }
            return m;
        }
    }
}

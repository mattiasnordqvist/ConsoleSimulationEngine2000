namespace ConsoleSimulationEngine2000
{
    public class CharMatrix : ICharMatrix
    {
        private string[][] m;
        public int X { get; }
        public int Y { get; }
        public int W { get; }
        public int H { get; }

        internal CharMatrix(string[][] m, int x, int y, int w, int h)
        {
            this.m = m;
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public string this[int x, int y] => m[y - Y][x - X];


        public static CharMatrix Create(string s, int x, int y, int w, int h)
        {
            var m = EmptyArray(w, h);
            int yi = 0, xi = 0;
            foreach (var c in s.EnumerateWithColorInfo())
            {
                if (c.EndsWith('\r'))
                {
                    continue;
                }
                if (c.EndsWith('\n'))
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

        private static string[][] EmptyArray(int w, int h)
        {
            var d = ColoredStringExt.End + ' ';
            var m = new string[h][];
            for (int yi = 0; yi < m.Length; yi++)
            {
                m[yi] = new string[w];
                for (int xi = 0; xi < w; xi++)
                {
                    m[yi][xi] = d;

                }
            }
            return m;
        }
    }
}

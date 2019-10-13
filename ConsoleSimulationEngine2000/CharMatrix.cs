namespace ConsoleSimulationEngine2000
{
    internal class CharMatrix
    {
        internal char[][] m;
        internal int x;
        internal int y;
        internal int w;
        internal int h;

        internal CharMatrix(char[][] m, int x, int y, int w, int h)
        {
            this.m = m;
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
    }
}
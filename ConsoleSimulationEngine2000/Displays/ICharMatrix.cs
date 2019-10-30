namespace ConsoleSimulationEngine2000
{
    public interface ICharMatrix
    {
        int Y { get; }
        int H { get; }
        int X { get; }
        int W { get; }

        (char c, string pre) this[int x, int y] { get; }
    }
}
namespace ConsoleSimulationEngine2000
{
    public interface ICharMatrix
    {
        int y { get; }
        int h { get; }
        int x { get; }
        int w { get; }

        (char c, string pre) this[int x, int y] { get; }
    }
}
namespace ConsoleSimulationEngine2000
{
    public interface IDisplay
    {
        ICharMatrix GetCharMatrix();

        int GetX();
        int GetY();
        int GetWidth();
        int GetHeight();
    }
}
namespace ConsoleSimulationEngine2000
{
    public class BasicDisplay : BaseDisplay
    {
        public BasicDisplay(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }

        protected internal override ICharMatrix GetCharMatrix(string value)
        {
            return CharMatrix.Create(Value.Trim(), GetX(), GetY(), GetWidth(), GetHeight());

        }
    }
}
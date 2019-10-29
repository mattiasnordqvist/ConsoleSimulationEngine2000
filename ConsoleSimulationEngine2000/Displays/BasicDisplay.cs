namespace ConsoleSimulationEngine2000
{
    public class BasicDisplay : BaseDisplay
    {
        public BasicDisplay(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }

        public string Value { get; set; } = "";

        protected internal override string GetStringToDisplay()
        {
            return Value.Trim();
        }
    }
}
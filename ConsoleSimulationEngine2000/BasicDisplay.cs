namespace ConsoleSimulationEngine2000
{
    public class BasicDisplay : BaseDisplay
    {
        public BasicDisplay(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }

        /// <summary>
        /// The text to display.
        /// </summary>
        public string Value { get; set; } = "";

        internal override string GetStringToDisplay(bool optimizedForPerformance)
        {
            return Value.Trim();
        }
    }
}
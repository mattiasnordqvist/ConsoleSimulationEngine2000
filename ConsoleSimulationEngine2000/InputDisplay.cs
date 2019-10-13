using System;

namespace ConsoleSimulationEngine2000
{
    public class InputDisplay : BaseDisplay
    {
        private readonly Input input;

        public InputDisplay(Input input, int x, int y, int width, int height) : base(x, y, width, height)
        {
            this.input = input;
        }

        internal override string GetStringToDisplay()
        {
            string text = input.CurrentInput;
            if (input.Suggestion != null)
            {
                text = input.CurrentInput + input.Suggestion.Substring(input.CurrentInput.Length);
            }

            return "> " + text + "_" + "".PadRight(Console.WindowWidth);
        }
    }
}
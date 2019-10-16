using System;
using System.Drawing;
using Pastel;

namespace ConsoleSimulationEngine2000
{
    public class TextInputDisplay : BaseDisplay
    {
        private readonly TextInput input;

        public TextInputDisplay(TextInput input, int x, int y, int width) : base(x, y, width, 1)
        {
            this.input = input;
        }

        protected internal override string GetStringToDisplay(bool optimizedForPerformance)
        {

            string text = input.CurrentInput.Pastel(Color.White);
            if (input.Suggestion != null)
            {
                    text = input.CurrentInput.Pastel(Color.White) + input.Suggestion.Substring(input.CurrentInput.Length);
            }

            return "> " + text + "_" + "".PadRight(Console.WindowWidth);
        }
    }
}
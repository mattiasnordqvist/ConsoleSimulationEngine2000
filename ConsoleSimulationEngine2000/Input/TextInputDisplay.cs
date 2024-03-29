﻿using System;
using System.Drawing;
using Pastel;

namespace ConsoleSimulationEngine2000
{
    public class TextInputDisplay : BasicDisplay
    {
        private readonly TextInput input;

        public TextInputDisplay(TextInput input, int x, int y, int width) : base(x, y, width, 1)
        {
            this.input = input;
        }

        protected internal override void Update()
        {
            string text = input.CurrentInput.Pastel(Color.White);
            if (input.Suggestion != null)
            {
                text = input.CurrentInput.Pastel(Color.White) + input.Suggestion.Substring(input.CurrentInput.Length);
            }

            Value = "> " + text + "_" + "".PadRight(Console.WindowWidth);
        }
    }
}
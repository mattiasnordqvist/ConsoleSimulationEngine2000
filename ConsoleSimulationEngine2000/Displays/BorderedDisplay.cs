using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleSimulationEngine2000
{
    public class BorderedDisplay : BaseDisplay
    {
        private static string regexColorStart = @"(" + '\u001b' + @"\[\d\d;2;\d{1,3};\d{1,3};\d{1,3}m)";
        private static string regexColorEnd = @"(" + '\u001b' + @"\[0m)";
        private static Regex colorRegex = new Regex(regexColorStart + "|" + regexColorEnd, RegexOptions.Compiled);
        private string[] lines = new string[0];

        /// <summary>
        /// Creates a display to be shown at position (x, y) with the given width and height. 
        /// </summary>
        /// <param name="x">The x position of the display. Negative values are interpreted as from right side of window</param>
        /// <param name="y">The y position of the display. Negative values are interpreted as from bottom of window</param>
        /// <param name="width">Width. Negative values means width should be subtracted from Window width.</param>
        /// <param name="height">Height. Negative values means width should be subtracted from Window height.</param>
        public BorderedDisplay(int x, int y, int width, int height) : base(x, y, width, height)
        {

        }

        public string Value
        {
            set
            {
                if (value.Contains(Environment.NewLine) || value.Contains('\n') || value.Contains('\r'))
                {
                    throw new InvalidOperationException("Value can't contain newlines. Use the Lines property instead to set lines individually.");
                }
                else
                {
                    Lines = new string[1] { value };
                }
            }
        }
        public string[] Lines
        {
            get => lines; 
            set
            {
                if(value.Any(v => v.Contains(Environment.NewLine) || v.Contains('\n') || v.Contains('\r')))
                {
                    throw new InvalidOperationException("Lines can't contain newlines. Add one line at a time to the Lines property.");
                }
                lines = value;
            }
        }
        protected internal override string GetStringToDisplay()
        {

            var sb = new StringBuilder();
            sb.AppendLine("#" + "-".PadRight(GetWidth() - 2, '-') + "#");

            for (int i = 0; i < GetHeight() - 2; i++)
            {
                if (Lines.Count() > i)
                {
                    var lineLengthDiff = Lines[i].Length - colorRegex.Replace(Lines[i], "").Length;
                    sb.AppendLine("| " + Lines[i].PadRight(GetWidth() - 4 + lineLengthDiff).Substring(0, GetWidth() - 4 + lineLengthDiff) + " |");
                }
                else
                {
                    sb.AppendLine("| " + "".PadRight(GetWidth() - 4).Substring(0, GetWidth() - 4) + " |");
                }
            }
            sb.Append("#" + "-".PadRight(GetWidth() - 2, '-') + "#");
            return sb.ToString();
        }
    }
}
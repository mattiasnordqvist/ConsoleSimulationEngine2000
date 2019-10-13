using System;
using System.Linq;
using System.Text;

namespace ConsoleSimulationEngine2000
{
    public class BorderedDisplay : BasicDisplay
    {
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

        internal override string GetStringToDisplay()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("#" + "-".PadRight(GetWidth() - 2, '-') + "#");
            var lines = Value.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < GetHeight() - 2; i++)
            {
                if (lines.Count() > i)
                {
                    sb.AppendLine("| " + lines[i].PadRight(GetWidth() - 4).Substring(0, GetWidth() - 4) + " |");
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
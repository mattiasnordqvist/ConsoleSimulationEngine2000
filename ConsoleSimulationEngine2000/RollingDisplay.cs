using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSimulationEngine2000
{
    /// <summary>
    /// A rolling display with a max line count of height - 2
    /// </summary>
    public class RollingDisplay : BorderedDisplay
    {
        private List<string> lines = new List<string>();

        public RollingDisplay(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }

        public void Log(string message)
        {
            lines.Add(message);
            lines = lines.Skip(lines.Count - LogSize).ToList();
            var sb = new StringBuilder();
            foreach (var item in lines)
            {
                sb.AppendLine(item);
            }
            sb.Remove(sb.Length - Environment.NewLine.Length, Environment.NewLine.Length);
            Value = sb.ToString();
        }

        private int LogSize => GetHeight() - 2;
    }
}

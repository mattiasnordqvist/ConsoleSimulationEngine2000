using System.Text;

namespace ConsoleSimulationEngine2000
{
    public class BorderedDisplay : BaseDisplay
    {
        
        private (string frame, int w, int h)? _cachedFrame;

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
        

        protected internal override ICharMatrix GetCharMatrix(string value)
        {
            var ms = new CharMatrixStack(2);
            ms.Add(CharMatrix.Create(GetFrame(GetWidth(), GetHeight()), GetX(), GetY(), GetWidth(), GetHeight()));
            ms.Add(CharMatrix.Create(value, GetX() + 1, GetY() + 1, GetWidth() - 2, GetHeight() - 2));
            return ms;
        }

        private string GetFrame(int w, int h)
        {
            if (_cachedFrame == null || _cachedFrame.Value.w != w || _cachedFrame.Value.h != h)
            {
                var sb = new StringBuilder();
                sb.AppendLine("#" + "-".PadRight(w - 2, '-') + "#");
                for (int i = 0; i < h - 2; i++)
                {
                    sb.AppendLine("| " + "".PadRight(w - 4).Substring(0, w - 4) + " |");
                }
                sb.Append("#" + "-".PadRight(w - 2, '-') + "#");
                _cachedFrame = (sb.ToString(), w, h);
            }
            return _cachedFrame.Value.frame;
        }

    }
}
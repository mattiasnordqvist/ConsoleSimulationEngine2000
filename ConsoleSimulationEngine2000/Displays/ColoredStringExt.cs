using System.Collections.Generic;

namespace ConsoleSimulationEngine2000
{
    public static class ColoredStringExt
    {
        public static IEnumerable<(char c, string pre, string post)> EnumerateWithColorInfo(this string @this, int length)
        {
            bool empty = false;
            int skipped = 0;
            char c = ' ';
            string pre = null;
            string post = null;
            int x = 0;
            for (; x + skipped < @this.Length && x < length; x++)
            {
                empty = false;
                if (x + skipped < @this.Length && @this[x + skipped] == '\u001b')
                {
                    int start = x + skipped;
                    skipped += 10;
                    while (@this[x + skipped] != 'm')
                    {
                        skipped++;
                    }
                    skipped++;
                    pre = @this[start..(x + skipped)];
                }
                c = x + skipped < @this.Length ? @this[x + skipped] : ' ';
                if (c == '\u001b' && @this[x + skipped + 3] == 'm')
                {
                    empty = true;
                    skipped += 3;
                }
                if (x + skipped + 4 < @this.Length && @this[x + skipped + 1] == '\u001b' && @this[x + skipped + 4] == 'm')
                {
                    post = "\u001b[0m";
                    skipped += 4;
                }
                if (!empty)
                { yield return (c, pre, post); }
                if (post != null || empty) { post = null; pre = null; }
            }
            for (; x < length; x++)
            {
                yield return (' ', null, null);
            }
        }
    }
}

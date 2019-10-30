using System.Collections.Generic;

namespace ConsoleSimulationEngine2000
{
    public static class ColoredStringExt
    {
        public static char BeginAnsi = '\u001b';
        public static string End = "\u001b[0m";
        public static IEnumerable<string> EnumerateWithColorInfo(this string @this)
        {
            int skipped = 0;
            string pre = End;
            int x = 0;
            for (; x + skipped < @this.Length; x++)
            {
                if (x + skipped + 3 < @this.Length && @this[x + skipped] == BeginAnsi && @this[x + skipped + 3] == 'm')
                {
                    skipped += 4;
                    var c = CurrentChar();
                    if (c == BeginAnsi) { x--; continue; }
                    pre = End;
                    yield return pre + c;
                }
                else if (x + skipped < @this.Length && @this[x + skipped] == BeginAnsi)
                {
                    int start = x + skipped;
                    skipped += 10;
                    while (@this[x + skipped] != 'm')
                    {
                        skipped++;
                    }
                    skipped++;
                    var c = CurrentChar();
                    if (c == BeginAnsi) { x--; continue; }
                    pre = @this[start..(x + skipped)];
                    yield return pre + c;
                }
                else
                {
                    var c = CurrentChar();
                    yield return pre + c;
                }
            }

            char CurrentChar() => x + skipped < @this.Length ? @this[x + skipped] : BeginAnsi;
        }
    }
}
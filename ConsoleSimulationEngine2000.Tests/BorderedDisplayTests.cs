using System;
using System.Drawing;
using NUnit.Framework;
using Pastel;

namespace ConsoleSimulationEngine2000.Tests
{
    public class BorderedDisplayTests
    {
        [Test]
        public void Test1()
        {
            var d = new BorderedDisplay(0, 0, 6, 5);
            d.Value = $"Hej{Environment.NewLine}san".Pastel(Color.Red);
            var actual = d.GetCharMatrix();

            Assert.AreEqual(ColoredStringExt.End + '#', actual[0, 0]);
            Assert.AreEqual(ColoredStringExt.End + '-', actual[1, 0]);
            Assert.AreEqual(ColoredStringExt.End + '-', actual[2, 0]);
            Assert.AreEqual(ColoredStringExt.End + '-', actual[3, 0]);
            Assert.AreEqual(ColoredStringExt.End + '-', actual[4, 0]);
            Assert.AreEqual(ColoredStringExt.End + '#', actual[5, 0]);

            Assert.AreEqual(ColoredStringExt.End + '|', actual[0, 1]);
            Assert.AreEqual(EnumerateWithColorInfoTests.BeginRed + 'H', actual[1, 1]);
            Assert.AreEqual(EnumerateWithColorInfoTests.BeginRed + 'e', actual[2, 1]);
            Assert.AreEqual(EnumerateWithColorInfoTests.BeginRed + 'j', actual[3, 1]);
            Assert.AreEqual(ColoredStringExt.End+ ' ', actual[4, 1]);
            Assert.AreEqual(ColoredStringExt.End + '|', actual[5, 1]);

            Assert.AreEqual(ColoredStringExt.End + '#', actual[0, 4]);
            Assert.AreEqual(ColoredStringExt.End + '-', actual[1, 4]);
            Assert.AreEqual(ColoredStringExt.End + '-', actual[2, 4]);
            Assert.AreEqual(ColoredStringExt.End + '-', actual[3, 4]);
            Assert.AreEqual(ColoredStringExt.End + '-', actual[4, 4]);
            Assert.AreEqual(ColoredStringExt.End + '#', actual[5, 4]);

        }
    }
}

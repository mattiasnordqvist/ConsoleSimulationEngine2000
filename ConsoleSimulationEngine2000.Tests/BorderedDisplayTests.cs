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

            Assert.AreEqual('#',  actual[0, 0].c);
            Assert.AreEqual(null, actual[0, 0].pre);
            Assert.AreEqual(null, actual[0, 0].post);
            Assert.AreEqual('-',  actual[1, 0].c);
            Assert.AreEqual(null, actual[1, 0].pre);
            Assert.AreEqual(null, actual[1, 0].post);
            Assert.AreEqual('-',  actual[2, 0].c);
            Assert.AreEqual(null, actual[2, 0].pre);
            Assert.AreEqual(null, actual[2, 0].post);
            Assert.AreEqual('-',  actual[3, 0].c);
            Assert.AreEqual(null, actual[3, 0].pre);
            Assert.AreEqual(null, actual[3, 0].post);
            Assert.AreEqual('-',  actual[4, 0].c);
            Assert.AreEqual(null, actual[4, 0].pre);
            Assert.AreEqual(null, actual[4, 0].post);
            Assert.AreEqual('#',  actual[5, 0].c);
            Assert.AreEqual(null, actual[5, 0].pre);
            Assert.AreEqual(null, actual[5, 0].post);

            Assert.AreEqual('|',  actual[0, 1].c);
            Assert.AreEqual(null, actual[0, 1].pre);
            Assert.AreEqual(null, actual[0, 1].post);
            Assert.AreEqual('H',  actual[1, 1].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[1, 1].pre);
            Assert.AreEqual(null, actual[1, 1].post);
            Assert.AreEqual('e',  actual[2, 1].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[2, 1].pre);
            Assert.AreEqual(null, actual[2, 1].post);
            Assert.AreEqual('j',  actual[3, 1].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[3, 1].pre);
            Assert.AreEqual(null, actual[3, 1].post);
            Assert.AreEqual(' ',  actual[4, 1].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[4, 1].pre);
            Assert.AreEqual("\u001b[0m", actual[4, 1].post);
            Assert.AreEqual('|',  actual[5, 1].c);
            Assert.AreEqual(null, actual[5, 1].pre);
            Assert.AreEqual(null, actual[5, 1].post);

            Assert.AreEqual('#',  actual[0, 4].c);
            Assert.AreEqual(null, actual[0, 4].pre);
            Assert.AreEqual(null, actual[0, 4].post);
            Assert.AreEqual('-',  actual[1, 4].c);
            Assert.AreEqual(null, actual[1, 4].pre);
            Assert.AreEqual(null, actual[1, 4].post);
            Assert.AreEqual('-',  actual[2, 4].c);
            Assert.AreEqual(null, actual[2, 4].pre);
            Assert.AreEqual(null, actual[2, 4].post);
            Assert.AreEqual('-',  actual[3, 4].c);
            Assert.AreEqual(null, actual[3, 4].pre);
            Assert.AreEqual(null, actual[3, 4].post);
            Assert.AreEqual('-',  actual[4, 4].c);
            Assert.AreEqual(null, actual[4, 4].pre);
            Assert.AreEqual(null, actual[4, 4].post);
            Assert.AreEqual('#',  actual[5, 4].c);
            Assert.AreEqual(null, actual[5, 4].pre);
            Assert.AreEqual(null, actual[5, 4].post);

        }
    }
}

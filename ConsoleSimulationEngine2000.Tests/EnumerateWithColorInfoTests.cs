using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Pastel;

namespace ConsoleSimulationEngine2000.Tests
{
    public class EnumerateWithColorInfoTests
    {
        [Test]
        public void Test1()
        {
            var actual = "".EnumerateWithColorInfo(0).ToList();
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void Test2()
        {
            var actual = "!".EnumerateWithColorInfo(1).ToList();
            Assert.AreEqual('!', actual[0].c);
            Assert.AreEqual(null, actual[0].pre);
            Assert.AreEqual(null, actual[0].post);
        }

        [Test]
        public void Test3()
        {
            var actual = "!".EnumerateWithColorInfo(2).ToList();
            Assert.AreEqual('!', actual[0].c);
            Assert.AreEqual(null, actual[0].pre);
            Assert.AreEqual(null, actual[0].post);

            Assert.AreEqual(' ', actual[1].c);
            Assert.AreEqual(null, actual[1].pre);
            Assert.AreEqual(null, actual[1].post);
        }

        [Test]
        public void Test4()
        {
            var actual = "!".Pastel(Color.Red).EnumerateWithColorInfo(1).ToList();
            Assert.AreEqual('!', actual[0].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[0].pre);
            Assert.AreEqual("\u001b[0m", actual[0].post);
        }

        [Test]
        public void Test5()
        {
            var actual = ("."+("!".Pastel(Color.Red))+".").EnumerateWithColorInfo(3).ToList();
            Assert.AreEqual('.', actual[0].c);
            Assert.AreEqual(null, actual[0].pre);
            Assert.AreEqual(null, actual[0].post);

            Assert.AreEqual('!', actual[1].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[1].pre);
            Assert.AreEqual("\u001b[0m", actual[1].post);

            Assert.AreEqual('.', actual[2].c);
            Assert.AreEqual(null, actual[2].pre);
            Assert.AreEqual(null, actual[2].post);
        }

        [Test]
        public void Test6()
        {
            var actual = ("!".Pastel(Color.Red)+ "?".Pastel(Color.Green)).EnumerateWithColorInfo(2).ToList();
            Assert.AreEqual('!', actual[0].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[0].pre);
            Assert.AreEqual("\u001b[0m", actual[0].post);
            Assert.AreEqual('?', actual[1].c);
            Assert.AreEqual("\u001b[38;2;0;128;0m", actual[1].pre);
            Assert.AreEqual("\u001b[0m", actual[1].post);
        }

        [Test]
        public void Test7()
        {
            var actual = "!".Pastel(Color.Red).EnumerateWithColorInfo(2).ToList();
            Assert.AreEqual(2, actual.Count());
        }

        [Test]
        public void Test8()
        {
            var actual = "!".EnumerateWithColorInfo(2).ToList();
            Assert.AreEqual(2, actual.Count());
        }

        [Test]
        public void Test9()
        {
            var actual = "!!".Pastel(Color.Red).EnumerateWithColorInfo(2).ToList();
            Assert.AreEqual('!', actual[0].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[0].pre);
            Assert.AreEqual(null, actual[0].post);

            Assert.AreEqual('!', actual[1].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[1].pre);
            Assert.AreEqual("\u001b[0m", actual[1].post);
        }

        [Test]
        public void Test10()
        {
            var actual = $"12{Environment.NewLine}34".Pastel(Color.Red).EnumerateWithColorInfo(6).ToList();

            Assert.AreEqual('1', actual[0].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[0].pre);
            Assert.AreEqual(null, actual[0].post);

            Assert.AreEqual('2', actual[1].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[1].pre);
            Assert.AreEqual(null, actual[1].post);

            Assert.AreEqual('3', actual[4].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[4].pre);
            Assert.AreEqual(null, actual[4].post);

            Assert.AreEqual('4', actual[5].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[5].pre);
            Assert.AreEqual("\u001b[0m", actual[5].post);
        }

        [Test]
        public void Test11()
        {
            var actual = $"1".Pastel(Color.Red).EnumerateWithColorInfo(2).ToList();

            Assert.AreEqual('1', actual[0].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[0].pre);
            Assert.AreEqual("\u001b[0m", actual[0].post);

            Assert.AreEqual(' ', actual[1].c);
            Assert.AreEqual(null, actual[1].pre);
            Assert.AreEqual(null, actual[1].post);
        }

        [Test]
        public void Test12()
        {
            var actual = $"1{Environment.NewLine}2".Pastel(Color.Red).EnumerateWithColorInfo(4).ToList();

            Assert.AreEqual('1', actual[0].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[0].pre);
            Assert.AreEqual(null, actual[0].post);

            Assert.AreEqual('\r', actual[1].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[1].pre);
            Assert.AreEqual(null, actual[1].post);
           
            Assert.AreEqual('\n', actual[2].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[2].pre);
            Assert.AreEqual(null, actual[2].post);

            Assert.AreEqual('2', actual[3].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", actual[3].pre);
            Assert.AreEqual("\u001b[0m", actual[3].post);
        }
    }

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

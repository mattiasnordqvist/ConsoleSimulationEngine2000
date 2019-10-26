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

            Assert.AreEqual('\0', actual[1].c);
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
    }
}

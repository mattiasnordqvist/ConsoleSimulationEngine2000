using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Pastel;

namespace ConsoleSimulationEngine2000.Tests
{
    public class EnumerateWithColorInfoTests
    {
        public static string End = "\u001b[0m";
        public static string BeginRed = "\u001b[38;2;255;0;0m";
        public static string BeginGreen = "\u001b[38;2;0;128;0m";
        [Test]
        public void Test1()
        {
            var actual = "".EnumerateWithColorInfo().ToList();
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void Test2()
        {
            var actual = "!".EnumerateWithColorInfo().ToList();
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(End + '!', actual[0]);
        }

        [Test]
        public void Test3()
        {
            var actual = "!".Pastel(Color.Red).EnumerateWithColorInfo().ToList();
            Assert.AreEqual(BeginRed + '!', actual[0]);
        }

        [Test]
        public void Test4()
        {
            var actual = ("." + "!".Pastel(Color.Red) + ".").EnumerateWithColorInfo().ToList();
            Assert.AreEqual(End + '.', actual[0]);
            Assert.AreEqual(BeginRed + '!', actual[1]);
            Assert.AreEqual(End + '.', actual[2]);
        }

        [Test]
        public void Test5()
        {
            var actual = ("!".Pastel(Color.Red) + "?".Pastel(Color.Green)).EnumerateWithColorInfo().ToList();
            Assert.AreEqual(BeginRed + '!', actual[0]);
            Assert.AreEqual(BeginGreen + '?', actual[1]);
        }

        [Test]
        public void Test6()
        {
            var actual = "!".Pastel(Color.Red).EnumerateWithColorInfo().ToList();
            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void Test7()
        {
            var actual = "!!".Pastel(Color.Red).EnumerateWithColorInfo().ToList();
            Assert.AreEqual(BeginRed + '!', actual[0]);
            Assert.AreEqual(BeginRed + '!', actual[1]);
        }

        [Test]
        public void Test8()
        {
            var actual = $"12{Environment.NewLine}34".Pastel(Color.Red).EnumerateWithColorInfo().ToList();

            Assert.AreEqual(BeginRed + '1', actual[0]);
            Assert.AreEqual(BeginRed + '2', actual[1]);
            Assert.AreEqual(BeginRed + '3', actual[4]);
            Assert.AreEqual(BeginRed + '4', actual[5]);
        }

        [Test]
        public void Test9()
        {
            var actual = $"1".Pastel(Color.Red).EnumerateWithColorInfo().ToList();

            Assert.AreEqual(BeginRed + '1', actual[0]);
        }

        [Test]
        public void Test10()
        {
            var actual = $"1{Environment.NewLine}2".Pastel(Color.Red).EnumerateWithColorInfo().ToList();

            Assert.AreEqual(BeginRed + '1', actual[0]);
            Assert.AreEqual(BeginRed + '\r', actual[1]);
            Assert.AreEqual(BeginRed + '\n', actual[2]);
            Assert.AreEqual(BeginRed + '2', actual[3]);
        }

        [Test]
        public void Test11()
        {
            var actual = "".Pastel(Color.Red).EnumerateWithColorInfo().ToList();
            Assert.AreEqual(0, actual.Count());
        }
        [Test]
        public void Test12()
        {
            var actual = ("_" + "".Pastel(Color.Red) + "_").EnumerateWithColorInfo().ToList();
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(End + '_', actual[0]);
            Assert.AreEqual(End + '_', actual[1]);
        }
    }
}

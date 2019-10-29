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
            Assert.AreEqual('!', actual[0].c);
            Assert.AreEqual(End, actual[0].pre);
        }

        [Test]
        public void Test3()
        {
            var actual = "!".Pastel(Color.Red).EnumerateWithColorInfo().ToList();
            Assert.AreEqual('!', actual[0].c);
            Assert.AreEqual(BeginRed, actual[0].pre);
        }

        [Test]
        public void Test4()
        {
            var actual = ("." + "!".Pastel(Color.Red) + ".").EnumerateWithColorInfo().ToList();
            Assert.AreEqual('.', actual[0].c);
            Assert.AreEqual(End, actual[0].pre);

            Assert.AreEqual('!', actual[1].c);
            Assert.AreEqual(BeginRed, actual[1].pre);

            Assert.AreEqual('.', actual[2].c);
            Assert.AreEqual(End, actual[2].pre);
        }

        [Test]
        public void Test5()
        {
            var actual = ("!".Pastel(Color.Red)+ "?".Pastel(Color.Green)).EnumerateWithColorInfo().ToList();
            Assert.AreEqual('!', actual[0].c);
            Assert.AreEqual(BeginRed, actual[0].pre);
            Assert.AreEqual('?', actual[1].c);
            Assert.AreEqual(BeginGreen, actual[1].pre);
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
            Assert.AreEqual('!', actual[0].c);
            Assert.AreEqual(BeginRed, actual[0].pre);

            Assert.AreEqual('!', actual[1].c);
            Assert.AreEqual(BeginRed, actual[1].pre);
        }

        [Test]
        public void Test8()
        {
            var actual = $"12{Environment.NewLine}34".Pastel(Color.Red).EnumerateWithColorInfo().ToList();

            Assert.AreEqual('1', actual[0].c);
            Assert.AreEqual(BeginRed, actual[0].pre);

            Assert.AreEqual('2', actual[1].c);
            Assert.AreEqual(BeginRed, actual[1].pre);

            Assert.AreEqual('3', actual[4].c);
            Assert.AreEqual(BeginRed, actual[4].pre);

            Assert.AreEqual('4', actual[5].c);
            Assert.AreEqual(BeginRed, actual[5].pre);
        }

        [Test]
        public void Test9()
        {
            var actual = $"1".Pastel(Color.Red).EnumerateWithColorInfo().ToList();

            Assert.AreEqual('1', actual[0].c);
            Assert.AreEqual(BeginRed, actual[0].pre);
        }

        [Test]
        public void Test10()
        {
            var actual = $"1{Environment.NewLine}2".Pastel(Color.Red).EnumerateWithColorInfo().ToList();

            Assert.AreEqual('1', actual[0].c);
            Assert.AreEqual(BeginRed, actual[0].pre);

            Assert.AreEqual('\r', actual[1].c);
            Assert.AreEqual(BeginRed, actual[1].pre);
           
            Assert.AreEqual('\n', actual[2].c);
            Assert.AreEqual(BeginRed, actual[2].pre);

            Assert.AreEqual('2', actual[3].c);
            Assert.AreEqual(BeginRed, actual[3].pre);
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
            var actual = ("_"+"".Pastel(Color.Red)+"_").EnumerateWithColorInfo().ToList();
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual('_', actual[0].c);
            Assert.AreEqual('_', actual[1].c);
        }
    }
}

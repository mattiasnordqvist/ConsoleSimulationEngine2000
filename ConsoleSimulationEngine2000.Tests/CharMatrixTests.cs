using System;
using System.Drawing;
using NUnit.Framework;
using Pastel;

namespace ConsoleSimulationEngine2000.Tests
{
    public class CharMatrixTests
    {

        public static string End = "\u001b[0m";
        public static string BeginRed = "\u001b[38;2;255;0;0m";
        public static string BeginGreen = "\u001b[38;2;0;128;0m";
        [Test]
        public void Test1()
        {
            var cm = CharMatrix.Create("!", 0, 0, 1, 1);

            Assert.AreEqual(cm.X, 0);
            Assert.AreEqual(cm.Y, 0);
            Assert.AreEqual(cm.W, 1);
            Assert.AreEqual(cm.H, 1);

            Assert.AreEqual(End + '!', cm[0, 0]);
        }

        [Test]
        public void Test2()
        {
            var cm = CharMatrix.Create("!", 0, 0, 2, 1);

            Assert.AreEqual(cm.X, 0);
            Assert.AreEqual(cm.Y, 0);
            Assert.AreEqual(cm.W, 2);
            Assert.AreEqual(cm.H, 1);

            Assert.AreEqual(End + '!', cm[0, 0]);
            Assert.AreEqual(End + ' ', cm[1, 0]);
        }

        [Test]
        public void Test3()
        {
            var cm = CharMatrix.Create("!", 0, 0, 2, 2);

            Assert.AreEqual(cm.X, 0);
            Assert.AreEqual(cm.Y, 0);
            Assert.AreEqual(cm.W, 2);
            Assert.AreEqual(cm.H, 2);
            Assert.AreEqual(End + '!', cm[0, 0]);
            Assert.AreEqual(End + ' ', cm[1, 0]);
            Assert.AreEqual(End + ' ', cm[0, 1]);
            Assert.AreEqual(End + ' ', cm[1, 1]);
        }

        [Test]
        public void Test4()
        {
            var cm = CharMatrix.Create("!".Pastel(Color.Red), 0, 0, 2, 2);

            Assert.AreEqual(cm.X, 0);
            Assert.AreEqual(cm.Y, 0);
            Assert.AreEqual(cm.W, 2);
            Assert.AreEqual(cm.H, 2);

            Assert.AreEqual(BeginRed + '!', cm[0, 0]);
            Assert.AreEqual(End + ' ', cm[1, 0]);
            Assert.AreEqual(End + ' ', cm[0, 1]);
            Assert.AreEqual(End + ' ', cm[1, 1]);
        }

        [Test]
        public void Test5()
        {
            var cm = CharMatrix.Create($"12{Environment.NewLine}34".Pastel(Color.Red), 0, 0, 2, 2);

            Assert.AreEqual(cm.X, 0);
            Assert.AreEqual(cm.Y, 0);
            Assert.AreEqual(cm.W, 2);
            Assert.AreEqual(cm.H, 2);

            Assert.AreEqual(BeginRed + '1', cm[0, 0]);
            Assert.AreEqual(BeginRed + '2', cm[1, 0]);
            Assert.AreEqual(BeginRed + '3', cm[0, 1]);
            Assert.AreEqual(BeginRed + '4', cm[1, 1]);
        }

        [Test]
        public void Test6()
        {
            var cm = CharMatrix.Create("0" + $"12{Environment.NewLine}34".Pastel(Color.Red) + "0", 0, 0, 3, 2);

            Assert.AreEqual(cm.X, 0);
            Assert.AreEqual(cm.Y, 0);
            Assert.AreEqual(cm.W, 3);
            Assert.AreEqual(cm.H, 2);

            Assert.AreEqual(End + '0', cm[0, 0]);
            Assert.AreEqual(BeginRed + '1', cm[1, 0]);
            Assert.AreEqual(BeginRed + '2', cm[2, 0]);
            Assert.AreEqual(BeginRed + '3', cm[0, 1]);
            Assert.AreEqual(BeginRed + '4', cm[1, 1]);
            Assert.AreEqual(End + '0', cm[2, 1]);
        }

        [Test]
        public void Test7()
        {
            var actual = CharMatrix.Create("!!".Pastel(Color.Red), 0, 0, 2, 1);
            Assert.AreEqual(BeginRed + '!', actual[0, 0]);
            Assert.AreEqual(BeginRed + '!', actual[1, 0]);
        }

        [Test]
        public void Test8()
        {
            var actual = CharMatrix.Create("!".Pastel(Color.Red), 0, 0, 2, 1);
            Assert.AreEqual(BeginRed + '!', actual[0, 0]);
            Assert.AreEqual(End + ' ', actual[1, 0]);
        }

        [Test]
        public void Test9()
        {
            var actual = CharMatrix.Create($"1{Environment.NewLine}2".Pastel(Color.Red), 0, 0, 1, 2);

            Assert.AreEqual(BeginRed + '1', actual[0, 0]);
            Assert.AreEqual(BeginRed + '2', actual[0, 1]);
        }

        [Test]
        public void Test10()
        {
            var actual = CharMatrix.Create($"1{Environment.NewLine}2".Pastel(Color.Red), 0, 0, 2, 2);

            Assert.AreEqual(BeginRed + '1', actual[0, 0]);
            Assert.AreEqual(End + ' ', actual[1, 0]);
            Assert.AreEqual(BeginRed + '2', actual[0, 1]);
            Assert.AreEqual(End + ' ', actual[1, 1]);
        }

        [Test]
        public void Test11()
        {
            var cm = CharMatrix.Create("!", 1, 1, 1, 1);

            Assert.AreEqual(cm.X, 1);
            Assert.AreEqual(cm.Y, 1);
            Assert.AreEqual(cm.W, 1);
            Assert.AreEqual(cm.H, 1);

            Assert.AreEqual(End + '!', cm[1, 1]);
        }
    }
}

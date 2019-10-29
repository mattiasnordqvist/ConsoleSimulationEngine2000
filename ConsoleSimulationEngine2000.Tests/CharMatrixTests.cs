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

            Assert.AreEqual(cm.x, 0);
            Assert.AreEqual(cm.y, 0);
            Assert.AreEqual(cm.w, 1);
            Assert.AreEqual(cm.h, 1);

            Assert.AreEqual('!', cm[0, 0].c);
            Assert.AreEqual(End, cm[0, 0].pre);
        }

        [Test]
        public void Test2()
        {
            var cm = CharMatrix.Create("!", 0, 0, 2, 1);

            Assert.AreEqual(cm.x, 0);
            Assert.AreEqual(cm.y, 0);
            Assert.AreEqual(cm.w, 2);
            Assert.AreEqual(cm.h, 1);

            Assert.AreEqual('!', cm[0, 0].c);
            Assert.AreEqual(End, cm[0, 0].pre);

            Assert.AreEqual(' ', cm[1, 0].c);
            Assert.AreEqual(End, cm[1, 0].pre);
        }

        [Test]
        public void Test3()
        {
            var cm = CharMatrix.Create("!", 0, 0, 2, 2);

            Assert.AreEqual(cm.x, 0);
            Assert.AreEqual(cm.y, 0);
            Assert.AreEqual(cm.w, 2);
            Assert.AreEqual(cm.h, 2);

            Assert.AreEqual('!', cm[0, 0].c);
            Assert.AreEqual(End, cm[0, 0].pre);

            Assert.AreEqual(' ', cm[1, 0].c);
            Assert.AreEqual(End, cm[1, 0].pre);

            Assert.AreEqual(' ', cm[0, 1].c);
            Assert.AreEqual(End, cm[0, 1].pre);

            Assert.AreEqual(' ', cm[1, 1].c);
            Assert.AreEqual(End, cm[1, 1].pre);
        }

        [Test]
        public void Test4()
        {
            var cm = CharMatrix.Create("!".Pastel(Color.Red), 0, 0, 2, 2);

            Assert.AreEqual(cm.x, 0);
            Assert.AreEqual(cm.y, 0);
            Assert.AreEqual(cm.w, 2);
            Assert.AreEqual(cm.h, 2);

            Assert.AreEqual('!', cm[0, 0].c);
            Assert.AreEqual(BeginRed, cm[0, 0].pre);

            Assert.AreEqual(' ', cm[1, 0].c);
            Assert.AreEqual(End, cm[1, 0].pre);

            Assert.AreEqual(' ', cm[0, 1].c);
            Assert.AreEqual(End, cm[0, 1].pre);

            Assert.AreEqual(' ', cm[1, 1].c);
            Assert.AreEqual(End, cm[1, 1].pre);
        }

        [Test]
        public void Test5()
        {
            var cm = CharMatrix.Create($"12{Environment.NewLine}34".Pastel(Color.Red), 0, 0, 2, 2);

            Assert.AreEqual(cm.x, 0);
            Assert.AreEqual(cm.y, 0);
            Assert.AreEqual(cm.w, 2);
            Assert.AreEqual(cm.h, 2);

            Assert.AreEqual('1', cm[0, 0].c);
            Assert.AreEqual(BeginRed, cm[0, 0].pre);

            Assert.AreEqual('2', cm[1, 0].c);
            Assert.AreEqual(BeginRed, cm[1, 0].pre);

            Assert.AreEqual('3', cm[0, 1].c);
            Assert.AreEqual(BeginRed, cm[0, 1].pre);

            Assert.AreEqual('4', cm[1, 1].c);
            Assert.AreEqual(BeginRed, cm[1, 1].pre);
        }

        [Test]
        public void Test6()
        {
            var cm = CharMatrix.Create("0" + $"12{Environment.NewLine}34".Pastel(Color.Red) + "0", 0, 0, 3, 2);

            Assert.AreEqual(cm.x, 0);
            Assert.AreEqual(cm.y, 0);
            Assert.AreEqual(cm.w, 3);
            Assert.AreEqual(cm.h, 2);

            Assert.AreEqual('0', cm[0, 0].c);
            Assert.AreEqual(End, cm[0, 0].pre);

            Assert.AreEqual('1', cm[1, 0].c);
            Assert.AreEqual(BeginRed, cm[1, 0].pre);

            Assert.AreEqual('2', cm[2, 0].c);
            Assert.AreEqual(BeginRed, cm[2, 0].pre);

            Assert.AreEqual('3', cm[0, 1].c);
            Assert.AreEqual(BeginRed, cm[0, 1].pre);

            Assert.AreEqual('4', cm[1, 1].c);
            Assert.AreEqual(BeginRed, cm[1, 1].pre);

            Assert.AreEqual('0', cm[2, 1].c);
            Assert.AreEqual(End, cm[2, 1].pre);
        }

        [Test]
        public void Test7()
        {
            var actual = CharMatrix.Create("!!".Pastel(Color.Red), 0, 0, 2, 1);
            Assert.AreEqual('!', actual[0, 0].c);
            Assert.AreEqual(BeginRed, actual[0, 0].pre);

            Assert.AreEqual('!', actual[1, 0].c);
            Assert.AreEqual(BeginRed, actual[1, 0].pre);
        }

        [Test]
        public void Test8()
        {
            var actual = CharMatrix.Create("!".Pastel(Color.Red), 0, 0, 2, 1);
            Assert.AreEqual('!', actual[0, 0].c);
            Assert.AreEqual(BeginRed, actual[0, 0].pre);

            Assert.AreEqual(' ', actual[1, 0].c);
            Assert.AreEqual(End, actual[1, 0].pre);
        }

        [Test]
        public void Test9()
        {
            var actual = CharMatrix.Create($"1{Environment.NewLine}2".Pastel(Color.Red) ,0,0,1,2);

            Assert.AreEqual('1', actual[0,0].c);
            Assert.AreEqual(BeginRed, actual[0, 0].pre);

            Assert.AreEqual('2', actual[0,1].c);
            Assert.AreEqual(BeginRed, actual[0, 1].pre);
        }

        [Test]
        public void Test10()
        {
            var actual = CharMatrix.Create($"1{Environment.NewLine}2".Pastel(Color.Red), 0, 0, 2, 2);

            Assert.AreEqual('1', actual[0, 0].c);
            Assert.AreEqual(BeginRed, actual[0, 0].pre);

            Assert.AreEqual(' ', actual[1, 0].c);
            Assert.AreEqual(End, actual[1, 0].pre);

            Assert.AreEqual('2', actual[0, 1].c);
            Assert.AreEqual(BeginRed, actual[0, 1].pre);

            Assert.AreEqual(' ', actual[1, 1].c);
            Assert.AreEqual(End, actual[1, 1].pre);

        }

        [Test]
        public void Test11()
        {
            var cm = CharMatrix.Create("!", 1, 1, 1, 1);

            Assert.AreEqual(cm.x, 1);
            Assert.AreEqual(cm.y, 1);
            Assert.AreEqual(cm.w, 1);
            Assert.AreEqual(cm.h, 1);

            Assert.AreEqual('!', cm[1, 1].c);
            Assert.AreEqual(End, cm[1, 1].pre);
        }
    }
}

using System;
using System.Drawing;
using NUnit.Framework;
using Pastel;

namespace ConsoleSimulationEngine2000.Tests
{
    public class CharMatrixStackTests
    {
        [Test]
        public void Test1()
        {
            var cm = CharMatrix.Create("!", 0, 0, 1, 1);
            var cms = new CharMatrixStack(1);
            cms.Add(cm);
            Assert.AreEqual(cms.X, 0);
            Assert.AreEqual(cms.Y, 0);
            Assert.AreEqual(cms.W, 1);
            Assert.AreEqual(cms.H, 1);

            Assert.AreEqual(ColoredStringExt.End + '!', cms[0, 0]);
        }

        [Test]
        public void Test2()
        {
            var cm1 = CharMatrix.Create("!", 0, 0, 1, 1);
            var cm2 = CharMatrix.Create("?", 0, 0, 1, 1);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.X, 0);
            Assert.AreEqual(cms.Y, 0);
            Assert.AreEqual(cms.W, 1);
            Assert.AreEqual(cms.H, 1);

            Assert.AreEqual(ColoredStringExt.End + '?', cms[0, 0]);
        }

        [Test]
        public void Test3()
        {
            var cm1 = CharMatrix.Create("\0!", 0, 0, 2, 1);
            var cm2 = CharMatrix.Create("?\0", 0, 0, 2, 1);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.X, 0);
            Assert.AreEqual(cms.Y, 0);
            Assert.AreEqual(cms.W, 2);
            Assert.AreEqual(cms.H, 1);

            Assert.AreEqual(ColoredStringExt.End + '?', cms[0, 0]);
            Assert.AreEqual(ColoredStringExt.End + '!', cms[1, 0]);
        }

        [Test]
        public void Test4()
        {
            var cm1 = CharMatrix.Create("!!!", 0, 0, 3, 1);
            var cm2 = CharMatrix.Create("\0?\0", 0, 0, 3, 1);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.X, 0);
            Assert.AreEqual(cms.Y, 0);
            Assert.AreEqual(cms.W, 3);
            Assert.AreEqual(cms.H, 1);

            Assert.AreEqual(ColoredStringExt.End + '!', cms[0, 0]);
            Assert.AreEqual(ColoredStringExt.End + '?', cms[1, 0]);
            Assert.AreEqual(ColoredStringExt.End + '!', cms[2, 0]);
        }

        [Test]
        public void Test5()
        {
            var cm1 = CharMatrix.Create("!!!".Pastel(Color.Red), 0, 0, 3, 1);
            var cm2 = CharMatrix.Create("\0?\0".Pastel(Color.Green), 0, 0, 3, 1);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.X, 0);
            Assert.AreEqual(cms.Y, 0);
            Assert.AreEqual(cms.W, 3);
            Assert.AreEqual(cms.H, 1);

            Assert.AreEqual(EnumerateWithColorInfoTests.BeginRed + '!', cms[0, 0]);
            Assert.AreEqual(EnumerateWithColorInfoTests.BeginGreen + '?', cms[1, 0]);
            Assert.AreEqual(EnumerateWithColorInfoTests.BeginRed + '!', cms[2, 0]);
        }

        [Test]
        public void Test6()
        {
            var cm1 = CharMatrix.Create($"!!!{Environment.NewLine}!!!".Pastel(Color.Red), 0, 0, 3, 2);
            var cm2 = CharMatrix.Create($"\0?\0{Environment.NewLine}\0?\0".Pastel(Color.Green), 0, 0, 3, 2);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.X, 0);
            Assert.AreEqual(cms.Y, 0);
            Assert.AreEqual(cms.W, 3);
            Assert.AreEqual(cms.H, 2);

            Assert.AreEqual(EnumerateWithColorInfoTests.BeginRed + '!', cms[0, 0]);
            Assert.AreEqual(EnumerateWithColorInfoTests.BeginGreen + '?', cms[1, 0]);
            Assert.AreEqual(EnumerateWithColorInfoTests.BeginRed + '!', cms[2, 0]);

            Assert.AreEqual(EnumerateWithColorInfoTests.BeginRed + '!', cms[0, 1]);
            Assert.AreEqual(EnumerateWithColorInfoTests.BeginGreen + '?', cms[1, 1]);
            Assert.AreEqual(EnumerateWithColorInfoTests.BeginRed + '!', cms[2, 1]);
        }

        [Test]
        public void Test7()
        {
            var cm1 = CharMatrix.Create($"####{Environment.NewLine}#  #{Environment.NewLine}####", 0, 0, 4, 3);
            var cm2 = CharMatrix.Create($"?".Pastel(Color.Green), 1, 1, 1, 1);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.X, 0);
            Assert.AreEqual(cms.Y, 0);
            Assert.AreEqual(cms.W, 4);
            Assert.AreEqual(cms.H, 3);

            Assert.AreEqual(ColoredStringExt.End + '#', cms[0, 0]);
            Assert.AreEqual(ColoredStringExt.End + '#', cms[1, 0]);
            Assert.AreEqual(ColoredStringExt.End + '#', cms[2, 0]);
            Assert.AreEqual(ColoredStringExt.End + '#', cms[3, 0]);

            Assert.AreEqual(ColoredStringExt.End + '#', cms[0, 1]);
            Assert.AreEqual(EnumerateWithColorInfoTests.BeginGreen + '?', cms[1, 1]);
            Assert.AreEqual(ColoredStringExt.End + ' ', cms[2, 1]);
            Assert.AreEqual(ColoredStringExt.End + '#', cms[3, 1]);

            Assert.AreEqual(ColoredStringExt.End + '#', cms[0, 2]);
            Assert.AreEqual(ColoredStringExt.End + '#', cms[1, 2]);
            Assert.AreEqual(ColoredStringExt.End + '#', cms[2, 2]);
            Assert.AreEqual(ColoredStringExt.End + '#', cms[3, 2]);
        }
    }
}

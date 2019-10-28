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
            Assert.AreEqual(cms.x, 0);
            Assert.AreEqual(cms.y, 0);
            Assert.AreEqual(cms.w, 1);
            Assert.AreEqual(cms.h, 1);

            Assert.AreEqual('!', cms[0, 0].c);
            Assert.AreEqual(null, cms[0, 0].pre);
            Assert.AreEqual(null, cms[0, 0].post);
        }

        [Test]
        public void Test2()
        {
            var cm1 = CharMatrix.Create("!", 0, 0, 1, 1);
            var cm2 = CharMatrix.Create("?", 0, 0, 1, 1);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.x, 0);
            Assert.AreEqual(cms.y, 0);
            Assert.AreEqual(cms.w, 1);
            Assert.AreEqual(cms.h, 1);

            Assert.AreEqual('?', cms[0, 0].c);
            Assert.AreEqual(null, cms[0, 0].pre);
            Assert.AreEqual(null, cms[0, 0].post);
        }

        [Test]
        public void Test3()
        {
            var cm1 = CharMatrix.Create("\0!", 0, 0, 2, 1);
            var cm2 = CharMatrix.Create("?\0", 0, 0, 2, 1);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.x, 0);
            Assert.AreEqual(cms.y, 0);
            Assert.AreEqual(cms.w, 2);
            Assert.AreEqual(cms.h, 1);

            Assert.AreEqual('?', cms[0, 0].c);
            Assert.AreEqual(null, cms[0, 0].pre);
            Assert.AreEqual(null, cms[0, 0].post);
            Assert.AreEqual('!', cms[1, 0].c);
            Assert.AreEqual(null, cms[1, 0].pre);
            Assert.AreEqual(null, cms[1, 0].post);
        }

        [Test]
        public void Test4()
        {
            var cm1 = CharMatrix.Create("!!!", 0, 0, 3, 1);
            var cm2 = CharMatrix.Create("\0?\0 ", 0, 0, 3, 1);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.x, 0);
            Assert.AreEqual(cms.y, 0);
            Assert.AreEqual(cms.w, 3);
            Assert.AreEqual(cms.h, 1);

            Assert.AreEqual('!', cms[0, 0].c);
            Assert.AreEqual(null, cms[0, 0].pre);
            Assert.AreEqual(null, cms[0, 0].post);
            Assert.AreEqual('?', cms[1, 0].c);
            Assert.AreEqual(null, cms[1, 0].pre);
            Assert.AreEqual(null, cms[1, 0].post);
            Assert.AreEqual('!', cms[2, 0].c);
            Assert.AreEqual(null, cms[2, 0].pre);
            Assert.AreEqual(null, cms[2, 0].post);
        }

        [Test]
        public void Test5()
        {
            var cm1 = CharMatrix.Create("!!!".Pastel(Color.Red), 0, 0, 3, 1);
            var cm2 = CharMatrix.Create("\0?\0".Pastel(Color.Yellow), 0, 0, 3, 1);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.x, 0);
            Assert.AreEqual(cms.y, 0);
            Assert.AreEqual(cms.w, 3);
            Assert.AreEqual(cms.h, 1);

            Assert.AreEqual('!', cms[0, 0].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", cms[0, 0].pre);
            Assert.AreEqual(null, cms[0, 0].post);
            Assert.AreEqual('?', cms[1, 0].c);
            Assert.AreEqual("\u001b[38;2;255;255;0m", cms[1, 0].pre);
            Assert.AreEqual(null, cms[1, 0].post);
            Assert.AreEqual('!', cms[2, 0].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", cms[2, 0].pre);
            Assert.AreEqual("\u001b[0m", cms[2, 0].post);
        }

        [Test]
        public void Test6()
        {
            var cm1 = CharMatrix.Create($"!!!{Environment.NewLine}!!!".Pastel(Color.Red), 0, 0, 3, 2);
            var cm2 = CharMatrix.Create($"\0?\0{Environment.NewLine}\0?\0".Pastel(Color.Yellow), 0, 0, 3, 2);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.x, 0);
            Assert.AreEqual(cms.y, 0);
            Assert.AreEqual(cms.w, 3);
            Assert.AreEqual(cms.h, 2);

            Assert.AreEqual('!', cms[0, 0].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", cms[0, 0].pre);
            Assert.AreEqual(null, cms[0, 0].post);
            Assert.AreEqual('?', cms[1, 0].c);
            Assert.AreEqual("\u001b[38;2;255;255;0m", cms[1, 0].pre);
            Assert.AreEqual(null, cms[1, 0].post);
            Assert.AreEqual('!', cms[2, 0].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", cms[2, 0].pre);
            Assert.AreEqual("\u001b[0m", cms[2, 0].post);

            Assert.AreEqual('!', cms[0, 1].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", cms[0, 1].pre);
            Assert.AreEqual(null, cms[0, 1].post);
            Assert.AreEqual('?', cms[1, 1].c);
            Assert.AreEqual("\u001b[38;2;255;255;0m", cms[1, 1].pre);
            Assert.AreEqual(null, cms[1, 1].post);
            Assert.AreEqual('!', cms[2, 1].c);
            Assert.AreEqual("\u001b[38;2;255;0;0m", cms[2, 1].pre);
            Assert.AreEqual("\u001b[0m", cms[2, 1].post);
        }

        [Test]
        public void Test7()
        {
            var cm1 = CharMatrix.Create($"####{Environment.NewLine}#  #{Environment.NewLine}####", 0, 0, 4, 3);
            var cm2 = CharMatrix.Create($"?".Pastel(Color.Yellow), 1, 1, 1, 1);
            var cms = new CharMatrixStack(2);
            cms.Add(cm1);
            cms.Add(cm2);
            Assert.AreEqual(cms.x, 0);
            Assert.AreEqual(cms.y, 0);
            Assert.AreEqual(cms.w, 4);
            Assert.AreEqual(cms.h, 3);

            Assert.AreEqual('#', cms[0, 0].c);
            Assert.AreEqual(null, cms[0, 0].pre);
            Assert.AreEqual(null, cms[0, 0].post);
            Assert.AreEqual('#', cms[1, 0].c);
            Assert.AreEqual(null, cms[1, 0].pre);
            Assert.AreEqual(null, cms[1, 0].post);
            Assert.AreEqual('#', cms[2, 0].c);
            Assert.AreEqual(null, cms[2, 0].pre);
            Assert.AreEqual(null, cms[2, 0].post);
            Assert.AreEqual('#', cms[3, 0].c);
            Assert.AreEqual(null, cms[3, 0].pre);
            Assert.AreEqual(null, cms[3, 0].post);

            Assert.AreEqual('#', cms[0, 1].c);
            Assert.AreEqual(null, cms[0, 1].pre);
            Assert.AreEqual(null, cms[0, 1].post);
            Assert.AreEqual('?', cms[1, 1].c);
            Assert.AreEqual("\u001b[38;2;255;255;0m", cms[1, 1].pre);
            Assert.AreEqual("\u001b[0m", cms[1, 1].post);
            Assert.AreEqual(' ', cms[2, 1].c);
            Assert.AreEqual(null, cms[2, 1].pre);
            Assert.AreEqual(null, cms[2, 1].post);
            Assert.AreEqual('#', cms[3, 1].c);
            Assert.AreEqual(null, cms[3, 1].pre);
            Assert.AreEqual(null, cms[3, 1].post);

            Assert.AreEqual('#', cms[0, 2].c);
            Assert.AreEqual(null, cms[0, 2].pre);
            Assert.AreEqual(null, cms[0, 2].post);
            Assert.AreEqual('#', cms[1, 2].c);
            Assert.AreEqual(null, cms[1, 2].pre);
            Assert.AreEqual(null, cms[1, 2].post);
            Assert.AreEqual('#', cms[2, 2].c);
            Assert.AreEqual(null, cms[2, 2].pre);
            Assert.AreEqual(null, cms[2, 2].post);
            Assert.AreEqual('#', cms[3, 2].c);
            Assert.AreEqual(null, cms[3, 2].pre);
            Assert.AreEqual(null, cms[3, 2].post);
        }
    }
}

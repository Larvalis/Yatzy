using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YahtzeeLibrary;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestValueOnePair()
        {
            Yahtzee yahtzee = new Yahtzee();
            PrivateObject priv = new PrivateObject(yahtzee);
            int[] example = new int[5] { 1, 5, 5, 1, 1 };
            priv.SetField("dies", example);
            Assert.AreEqual(10, yahtzee.valueOnePair());
        }

        [TestMethod]
        public void TestValueTwoPair()
        {
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void TestValueSmallStraight()
        {
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void TestValueLargeStraight()
        {
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void TestValueFullHouse()
        {
            Assert.AreEqual(0, 0);
        }
    }
}

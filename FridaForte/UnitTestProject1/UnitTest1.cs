using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(ReadKey(), null);
        }
    }
}

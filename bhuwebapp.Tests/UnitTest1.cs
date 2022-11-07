using bhuwebapp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace bhuwebapp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private const string StringExpected = "11/7/2022 12:00:00 AM";
        private const long EpocExpected = 1667779200;
        [TestMethod]
        public void TestMethod1()
        {
            var result = Dateutils.DateAsString(new DateTime(2022, 11, 07));
            Assert.AreEqual(StringExpected, result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var result = Dateutils.DateAsTimestamp(new DateTime(2022, 11, 07));
            Assert.AreEqual(EpocExpected, result);
        }
    }
}

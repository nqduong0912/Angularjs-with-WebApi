using System;
using KTNB.Extended.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KTNB.Test.Extended.Extensions
{
    [TestClass]
    public class YearExtensionsUnitTest
    {
        [TestMethod]
        public void Nam2000Test()
        {
            int expected = 2000;
            int? value = 2000;
            int actual = value.GetYearOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NamIsNullTest()
        {
            DateTime today = DateTime.Today;
            int expected;
            if (today.Month >= 8)
            {
                expected = today.Year + 1;
            }
            else
            {
                expected = today.Year;
            }

            int? value = (int?)null;
            int actual = value.GetYearOrDefault();
            Assert.AreEqual(expected, actual);
        }
    }
}

using System;
using System.Collections.Generic;
using KTNB.Extended.Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KTNB.Test.Extended.Commons
{
    [TestClass]
    public class MiscUtilsUnitTest
    {
        [TestMethod]
        public void GetCurrentYearTest()
        {
            int expected = 2015;
            int actual = MiscUtils.GetCurrentYear();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAllYears_CheckTotalTest()
        {
            int expected = 11;
            List<int> years = MiscUtils.GetAllYears();
            var actual = years.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAllYears_CheckDataResultTest()
        {
            int currentYear = DateTime.Today.Year;
            List<int> expected = new List<int>() { currentYear + 1, currentYear, currentYear - 1, currentYear - 2, currentYear - 3, currentYear - 4, currentYear - 5, currentYear - 6, currentYear - 7, currentYear - 8, currentYear - 9 };
            List<int> actual = MiscUtils.GetAllYears();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}

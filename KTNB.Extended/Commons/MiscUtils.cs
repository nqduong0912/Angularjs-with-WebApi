using System;
using System.Collections.Generic;
using System.Linq;

namespace KTNB.Extended.Commons
{
    public static class MiscUtils
    {
        /// <summary>
        /// Lấy ra năm kiểm toán hiện tại
        /// </summary>
        /// <returns>Năm kiểm toán hiện tại</returns>
        [Obsolete("Thuộc tính này không còn được sử dụng, thay thế bởi hàm GetCurrentYear.")]
        public static string CurrentYear
        {
            get
            {
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                if (month > 8)
                {
                    year += 1;
                }

                return year.ToString();
            }
        }

        /// <summary>
        /// Lấy ra năm kiểm toán hiện tại
        /// </summary>
        /// <returns>Năm kiểm toán hiện tại</returns>
        public static int GetCurrentYear()
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            if (month > 8)
            {
                year += 1;
            }

            return year;
        }

        [Obsolete("Hàm này không còn được sử dụng, thay thế bởi hàm GetAllYears.")]
        public static List<string> GetAllYear()
        {
            List<string> lstYear = new List<string>();
            for (int y = DateTime.Now.Year + 1; y > DateTime.Now.Year - 10; y--)
            {
                lstYear.Add(y.ToString());
            }

            return lstYear;
        }

        /// <summary>
        /// Lấy ra danh sách 11 năm cần kiểm toán
        /// </summary>
        /// <returns>Danh sách năm</returns>
        public static List<int> GetAllYears()
        {
            int currentYear = DateTime.Today.Year;
            List<int> years = Enumerable.Range(currentYear - 9, 11).Reverse().ToList();

            return years;
        }
    }
}

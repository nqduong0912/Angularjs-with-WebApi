using KTNB.Extended.Commons;

namespace KTNB.Extended.Extensions
{
    public static class YearExtensions
    {
        /// <summary>
        /// Lấy ra năm kiểm toán.
        /// </summary>
        /// <param name="year">Năm kiểm toán.</param>
        /// <returns>Nếu year là null thì lấy ra năm kiểm toán hiện tại, ngược lại trả về giá trị hiện tại.</returns>
        public static int GetYearOrDefault(this int? year)
        {
            return year.GetValueOrDefault(MiscUtils.GetCurrentYear());
        }
    }
}
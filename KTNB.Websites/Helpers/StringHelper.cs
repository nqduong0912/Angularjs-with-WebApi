using System;
using System.Text.RegularExpressions;

namespace VPB_KTNB.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Left
        /// </summary>
        /// <param name="param"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

        /// <summary>
        /// Right
        /// </summary>
        /// <param name="param"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(string param, int length)
        {
            string result = "";
            try
            {
                result = param.Substring(param.Length - length, length);
            }
            catch (Exception ex)
            {
                result = param;
            }
            return result;
        }

        /// <summary>
        /// Mid
        /// </summary>
        /// <param name="param"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }

        /// <summary>
        /// Mid
        /// </summary>
        /// <param name="param"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string Mid(string param, int startIndex)
        {
            string result = param.Substring(startIndex);
            return result;
        }

        /// <summary>
        /// isSubstring
        /// </summary>
        /// <param name="main"></param>
        /// <param name="substr"></param>
        /// <returns></returns>
        public static bool isSubstring(string main, string substr)
        {
            Match theMatch = Regex.Match(main, substr);
            if (theMatch.Success)
                return true;
            else
                return false;
        }
    }
}
using System.Collections.Generic;
using ServiceStack.Text;

namespace KTNB.Extended.Commons.Helpers
{
    public class JsonHelper
    {
        public static string ToJSON<T>(T obj)
        {
            return JsonSerializer.SerializeToString(obj);
        }

        public static List<T> ToList<T>(string jsonString) where T : class, new()
        {
            return JsonSerializer.DeserializeFromString<List<T>>(jsonString);
        }
    }
}

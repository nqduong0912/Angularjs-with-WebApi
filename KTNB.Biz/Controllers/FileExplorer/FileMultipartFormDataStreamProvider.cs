using System.Net.Http;
using System.Net.Http.Headers;

namespace KTNB.Biz.Controllers.FileExplorer
{
    public class FileMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public FileMultipartFormDataStreamProvider(string path)
            : base(path)
        {
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            // Make the file name URL safe and then use it & is the only disallowed url character allowed in a windows filename
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            
            return name.Trim(new char[] { '"' }).Replace("&", "and");
        }
    }
}
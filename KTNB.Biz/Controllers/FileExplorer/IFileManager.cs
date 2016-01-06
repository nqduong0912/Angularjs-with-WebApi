using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KTNB.Extended.Entities.FileExplorer;

namespace KTNB.Biz.Controllers.FileExplorer
{
    public interface IFileManager
    {
        /// <summary>
        /// Lấy về thông tin của một file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>The file.</returns>
        Task<IEnumerable<FileResult>> Get(string fileName);

        /// <summary>
        /// Thêm files từ request.
        /// </summary>
        /// <param name="request">The http request.</param>
        /// <returns>Trạng thái khi thêm các file.</returns>
        Task<UploadResult> Add(HttpRequestMessage request);

        /// <summary>
        /// Xóa một file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>Trạng thái xóa file.</returns>
        Task<UploadResult> Delete(string fileName);

        /// <summary>
        /// Kiểm tra xem một file có tồn tại hay không.
        /// </summary>
        /// <param name="fileName">Đường dẫn file.</param>
        /// <returns>Trạng thái của file.</returns>
        bool FileExists(string fileName);
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace KTNB.Extended.Entities.FileExplorer
{
    /// <summary>
    /// Kết quả upload tệp.
    /// </summary>
    public class FileResult
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Tên tệp
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Đường dẫn đến file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Ngày tạo.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Ngày sửa cuối.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Kích cỡ tệp.
        /// </summary>
        public long Size { get; set; }
    }
}
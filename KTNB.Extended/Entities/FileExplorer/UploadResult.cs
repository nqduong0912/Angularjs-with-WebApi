using System;
using System.Collections.Generic;

namespace KTNB.Extended.Entities.FileExplorer
{
    /// <summary>
    /// Kết quả xử lý file.
    /// </summary>
    public class UploadResult
    {
        /// <summary>
        /// Trạng thái: True - Thành công, False - Không thành công.
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// Danh sách tệp được uploads.
        /// </summary>
        public List<FileResult> Files { get; set; }

        /// <summary>
        /// Thông điệp.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Biến môi trường
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Thời gian hiện tại trên server.
        /// </summary>
        public DateTime ServerTime { get; set; }
    }
}

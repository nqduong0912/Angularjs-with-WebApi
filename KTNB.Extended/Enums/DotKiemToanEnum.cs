using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Enums
{
    /// <summary>
    /// Trạng thái Đợt kiểm toán
    /// </summary>
    public enum DotKiemToanEnum
    {
        /// <summary>
        /// 0 - S1 - Khởi tạo
        /// </summary>
        KhoiTao = 0,

        /// <summary>
        /// 1 - S2 - Chưa duyệt
        /// </summary>
        ChuaDuyet = 1,

        /// <summary>
        /// 2 - S3 - Đã duyệt
        /// </summary>
        DaDuyet = 2,

        /// <summary>
        /// 4 - S4 - Chưa thực hiện
        /// </summary>
        ChuaThucHien = 4
    }
}

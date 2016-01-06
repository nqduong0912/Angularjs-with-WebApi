using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Enums
{
    /// <summary>
    /// Trạng thái của bộ tiêu chí kế hoạch năm
    /// </summary>
    public enum BoTieuChiKeHoachNamEnum
    {
        /// <summary>
        /// S1 - Khởi tạo
        /// </summary>
        KhoiTao = 1,

        /// <summary>
        /// S2 - Chưa rà soát
        /// </summary>
        ChuaRaSoat = 2,

        /// <summary>
        /// S3 - Đã rà soát
        /// </summary>
        DaRaSoat = 4,

        /// <summary>
        /// S4 - Đã duyệt
        /// </summary>
        DaDuyet = 8
    }
}

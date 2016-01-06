using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Enums
{
    /// <summary>
    /// Trạng thái của kế hoạch năm
    /// </summary>
    public enum KeHoachNamEnum
    {
        /// <summary>
        /// S1 - Khởi tạo
        /// </summary>
        KhoiTao = 0,

        /// <summary>
        /// S2 - Chưa duyệt
        /// </summary>
        ChuaDuyet = 1,

        /// <summary>
        /// S3 - BGD đã kiểm tra
        /// </summary>
        DaKiemTra = 2,

        /// <summary>
        /// S4 - Ban kiểm soát duyệt
        /// </summary>
        BanKiemSoatDuyet = 4,

        /// <summary>
        /// S5 - Điều chỉnh
        /// </summary>
        DieuChinh = 8
    }
}

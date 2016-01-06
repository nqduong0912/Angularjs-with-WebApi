using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Enums
{
    public enum ThuTucEnum
    {
        /// <summary>
        /// 0 - S1 - Chưa duyệt
        /// </summary>
        ChuaDuyet = 0,

        /// <summary>
        /// 1 - S2 - Chờ duyệt
        /// </summary>
        ChoDuyet = 1,

        /// <summary>
        /// 2 - S3 - Đã duyệt
        /// </summary>
        DaDuyet = 2,

        /// <summary>
        /// 4 - S4 - Cán bộ duyệt từ chối
        /// </summary>
        CanBoDuyetTuChoi = 4,

        /// <summary>
        /// 8 - S5 - Trưởng đoàn từ chối
        /// </summary>
        TruongDoanTuChoi = 8
    }
}

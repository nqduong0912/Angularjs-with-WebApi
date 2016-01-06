using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Entities.KhoiTaoJob
{
    public class ChonThanhVienDotKiemToanRequest
    {
        /// <summary>
        /// Danh sach thanh vien dot kiem toan
        /// </summary>
        public List<ThanhVienDotKiemToan> DsThanhVienDotKiemToan { get; set; }

        /// <summary>
        /// Danh sach file upload
        /// </summary>
        public List<UpLoadFileDotKiemToan> DsUpLoadFileDotKiemToan { get; set; }
    }
}

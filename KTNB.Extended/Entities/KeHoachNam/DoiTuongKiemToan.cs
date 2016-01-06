using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KTNB.Extended.Entities.KeHoachNam
{
    /// <summary>
    /// EX_QT_NAM_DTKT
    /// </summary>
    public class DoiTuongKiemToan
    {
        [Key]
        public int Id { get; set; }

        public int Nam { get; set; }

        public Guid? Rank { get; set; }

        public int? RankText { get; set; }

        public Guid? LoaiDTKT { get; set; }

        public string LoaiDTKTText { get; set; }

        public Guid? DTKT { get; set; }

        public string DTKTText { get; set; }

        public int? GiaTriGoc { get; set; }

        /// <summary>
        /// Điểm quy đổi
        /// </summary>
        public int? DiemQuyDoi { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ThoiGianKTGanNhat { get; set; }

        public string ThoiGianKTGanNhatText
        {
            get
            {
                if (ThoiGianKTGanNhat.HasValue)
                {
                    return ThoiGianKTGanNhat.Value.ToShortDateString();
                }

                return null;
            }
            set { /**/ }
        }

        public int? BoQuyMoId { get; set; }

        public string BoQuyMo { get; set; }

        public int? QuyMoId { get; set; }

        public string QuyMo { get; set; }

        [Range(1, 12)]
        public byte? ThangDuKienKT { get; set; }

        public Guid? TanSuat { get; set; }

        public string TanSuatText { get; set; }

        public DateTime? DotKTTiep1 { get; set; }

        public DateTime? DotKTTiep2 { get; set; }

        public string MucTieu { get; set; }

        public string PhamVi { get; set; }

        public Guid? Phong { get; set; }

        public string PhongText { get; set; }

        public Guid? TruongDoan { get; set; }

        public string TruongDoanText { get; set; }

        public Guid? Manager { get; set; }

        public string ManagerText { get; set; }

        public bool IsTrong3Nam { get; set; }
    }
}

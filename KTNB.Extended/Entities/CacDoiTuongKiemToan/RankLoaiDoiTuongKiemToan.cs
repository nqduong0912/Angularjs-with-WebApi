using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;

namespace KTNB.Extended.Entities.CacDoiTuongKiemToan
{
    [Alias("V_LIB_DM_RANK")]
    public class RankLoaiDoiTuongKiemToan : Lib_EntityDao
    {
        [Required]
        [Alias("Rank")]
        public int Rank { get; set; }

        [Alias("MarkFrom")]
        public int MarkFrom { get; set; }

        [Alias("MarkTo")]
        public int MarkTo { get; set; }

        [Alias("ID Loại DTKT")]
        public string IDLDTKT { get; set; }

        [Alias("Tên Loại DTKT")]
        [StringLength(500)]
        public string TenLDTKT { get; set; }
    }
}

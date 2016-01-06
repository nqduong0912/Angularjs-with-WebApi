using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Dal
{
    [Alias("EX_QT_QLYBOTIEUCHI")]
    public class qt_qlybotieuchi : EntityDao
    {
        [Required]
        public int Nam { get; set; }

        [Required]
        public string LoaiDTKT { get; set; }

        [Required]
        public int TrangThai { get; set; }

        [Required]
        public string BoTieuChiNam { get; set; }

        [Required]
        public DateTime NgayCapNhat { get; set; }

        [Required]
        public bool isActive { get; set; }
    }

    [Alias("EX_DM_BOTIEUCHI")]
    public class qt_dm_kehoachnam_botieuchi : EntityDao
    {
        [Required]
        public int Nam { get; set; }

        [Required]
        public Guid LDTKT { get; set; }

        [Required]
        public Guid BTC { get; set; }

        [Required]
        public string TenBTC { get; set; }

        [Required]
        public string Congthuc { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public int Status { get; set; }

        public int IsActive { get; set; }

        public int IsOn { get; set; }
    }
}

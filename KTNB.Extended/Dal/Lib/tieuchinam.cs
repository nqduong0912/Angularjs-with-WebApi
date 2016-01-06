using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTNB.Extended.Dal.Lib
{
    [Alias("V_LIB_DM_TCN_TP")]
    public class dm_li_kehoachnam_tieuchitp : Lib_EntityDao
    {
        [Required]
        [Alias("Tên")]
        [StringLength(500)]
        public string Ten { get; set; }

        [Alias("Diễn giải")]
        [StringLength(1000)]
        public string Diengiai { get; set; }

        [Alias("Tên tiêu chí chính")]
        [StringLength(500)]
        public string Tieuchichinh { get; set; }

        [Alias("_Tên tiêu chí chính")]
        public Guid _Tieuchichinh { get; set; }

        [Alias("Loại tiêu chí")]
        public int Loaitieuchi { get; set; }

        [Alias("Tỷ trọng")]
        public int Tytrong { get; set; }

        [Alias("Bộ tiêu chí năm")]
        public string Botieuchi { get; set; }
        [Alias("_Bộ tiêu chí năm")]
        public Guid _Botieuchi { get; set; }

        [Alias("Loại định tính")]
        public string Loaidinhtinh { get; set; }
        [Alias("_Loại định tính")]
        public Guid _Loaidinhtinh { get; set; }
        [Alias("Loại định lượng")]
        public string Loaidinhluong { get; set; }

    }
    [Alias("V_LIB_DM_TCN_TCC")]
    public class dm_li_kehoachnam_tieuchichinh : Lib_EntityDao
    {
        [Required]
        [Alias("Tên")]
        [StringLength(500)]
        public string Ten { get; set; }

        [Alias("Diễn giải")]
        [StringLength(1000)]
        public string Diengiai { get; set; }

        [Alias("_Tên bộ tiêu chí")]
        public Guid _Botieuchi { get; set; }

        [Alias("Tên bộ tiêu chí")]
        [StringLength(500)]
        public string Botieuchi { get; set; }

        [Alias("Công thức")]
        public string Congthuc { get; set; }

        [Alias("Tỷ trọng")]
        public int Tytrong { get; set; }
    }
    [Alias("V_LIB_DM_TCN_BTC")]
    public class dm_li_kehoachnam_botieuchi : Lib_EntityDao
    {

        [Required]
        [Alias("Tên bộ tiêu chí năm")]
        [StringLength(500)]
        public string Ten { get; set; }

        [Alias("Diễn giải")]
        [StringLength(1000)]
        public string Diengiai { get; set; }

        [Alias("Công thức")]
        [StringLength(500)]
        public string Congthuc { get; set; }
    }

}

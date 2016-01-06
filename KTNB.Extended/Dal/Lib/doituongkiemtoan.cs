using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTNB.Extended.Dal.Lib
{
    [Alias("V_LIB_DM_LOAIDTKT")]
    public class dm_li_loaidoituongkiemtoan : Lib_EntityDao
    {

        [Required]
        [Alias("Tên loại đối tượng kiểm toán")]
        [StringLength(500)]
        public string Ten { get; set; }

        [Alias("Diễn giải")]
        [StringLength(1000)]
        public string Diengiai { get; set; }

        [Alias("Số lượng ĐTKT")]
        public int SoLuongDTKT { get; set; }
    }

    [Alias("V_LIB_DM_DTKT")]
    public class dm_li_doituongkiemtoan : Lib_EntityDao
    {

        [Required]
        [Alias("Ten")]
        [StringLength(500)]
        public string Ten { get; set; }

        [Alias("Diengiai")]
        [StringLength(1000)]
        public string Diengiai { get; set; }

        [Alias("IDLDTKT")]
        public string IDLDTKT { get; set; }

        [Alias("TenLDTKT")]
        [StringLength(500)]
        public string TenLDTKT { get; set; }
    }
}

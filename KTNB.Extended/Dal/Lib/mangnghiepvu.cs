using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTNB.Extended.Dal.Lib
{
    [Alias("V_LIB_DM_MANGNV")]
    public class mangnghiepvu : Lib_EntityDao
    {

        [Required]
        [Alias("Ten")]
        [StringLength(500)]
        public string Ten { get; set; }

        [Alias("Diengiai")]
        [StringLength(1000)]
        public string Diengiai { get; set; }

        [Alias("IDLDTKT")]
        [StringLength(500)]
        public string IDLDTKT { get; set; }

        [Alias("TenLDTKT")]
        [StringLength(500)]
        public string TenLDTKT { get; set; }
    }
}

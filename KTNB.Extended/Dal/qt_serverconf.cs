using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Dal
{
    [Alias("V_QT_SERVERCONF")]
    public class qt_serverconf : Lib_EntityDao
    {
        [Required]
        public int CType { get; set; }

        [Required]
        [StringLength(128)]
        public string Ten { get; set; }

        [Required]
        public string Value { get; set; }
    }
}

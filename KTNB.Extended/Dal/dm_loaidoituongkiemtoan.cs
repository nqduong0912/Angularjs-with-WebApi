using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Dal
{
    [Alias("EX_DM_LOAIDTKT")]
    public class dm_loaidoituongkiemtoan : EntityDao
    {
        [Required]
        [StringLength(500)]
        public string Ten { get; set; }

        [StringLength(1000)]
        public string Diengiai { get; set; }

        public Guid SourceId { get; set; }

        [Required]
        public int Nam { get; set; }

        [StringLength(255)]
        public string Phongban { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Entities
{
    /// <summary>
    /// EX_DM_BOQUYMO
    /// </summary>
    public class BoQuyMo
    {
        [Key]
        public int Id { get; set; }

        public Guid SourceId { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        public int Nam { get; set; }

        public Guid? LoaiDTKT { get; set; }

        public bool TrangThai { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Entities
{
    public class LoaiDoiTuongKiemToan
    {
        [Key]
        public int Id { get; set; }

        public Guid SourceId { get; set; }

        [Required]
        [StringLength(500)]
        public string Ten { get; set; }

        [StringLength(2000)]
        public string DienGiai { get; set; }

        public int Nam { get; set; }

        [Required]
        [StringLength(255)]
        public string PhongBan { get; set; }
    }
}

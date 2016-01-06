using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Entities.KhoiTaoJob
{
    public class UpLoadFileDotKiemToan
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        public DateTime CreateDate { get; set; }
    }
}

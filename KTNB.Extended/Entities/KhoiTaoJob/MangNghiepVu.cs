using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Entities.KhoiTaoJob
{
    public class MangNghiepVu
    {
        [Key]
        public Guid PK_DocumentID { get; set; }

        public int Status { get; set; }

        [Required]
        public string Ten { get; set; }

        public string  DienGiai { get; set; }
    }
}

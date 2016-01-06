using KTNB.Extended.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Dal.Lib
{
    public class LoaiDoiTuongKiemToan
    {
        [Key]
        public Guid PK_DocumentID { get; set; }

        [Required]
        [StringLength(500)]
        public string Ten { get; set; }

        [StringLength(1000)]
        public string DienGiai { get; set; }

        public int SoLuongDTKT { get; set; }

        public int Status { get; set; }
    }

    public class DoiTuongKiemToan
    {
        [Key]
        public Guid PK_DocumentID { get; set; }

        [Required]
        [StringLength(500)]
        public string Ten { get; set; }

        [StringLength(1000)]
        public string DienGiai { get; set; }

        public string IDLDTKT { get; set; }

        [StringLength(500)]
        public string TenLDTKT { get; set; }

        public int Status { get; set; }
    }
}

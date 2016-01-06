using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace KTNB.Extended.Entities.KhoiTaoJob
{
    public class ThanhVienDotKiemToan
    {
        [Key]
        public Guid PK_UserID { get; set; }
        [Key]
        public int? IdDotKiemToan { get; set; }

        [StringLength(50)]
        public string UserCode { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        public string PhongBan { get; set; }

        public bool IsThanhVienDotKiemToan { get; set; }
    }
}

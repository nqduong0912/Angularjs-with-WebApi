using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace KTNB.Extended.Entities.KhoiTaoJob
{
    public class ThanhVienDotKT
    {
        [Key]
        public Guid? PK_UserID { get; set; }
        [Key]
        public int IdDotKiemToan { get; set; }

        public string UserName { get; set; }

        public Guid? QuyTrinh { get; set; }

        public string QuyTrinhText { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KTNB.Extended.Entities.CacDoiTuongKiemToan
{
    public class CreateQuyMoDoiTuongKiemToan
    {

        [Required]
        public string DocType { get; set; }

        // [Required]
        public string DocumentId { get; set; }

        // [DataType(DataType.EmailAddress)]
        // [StringLength(255, MinimumLength = 11)]
        public string LoaiDoiTuongKiemToan { get; set; }

        public int Nam { get; set; }

        public string TenBoQuyMo { get; set; }

        public string QuyMo { get; set; }
    }
}

using KTNB.Extended.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KTNB.Extended.Entities.KeHoachNam
{
    public class DuyetKeHoachNam
    {
        [Key]
        public int Id { get; set; }

        public int Nam { get; set; }

        /// <summary>
        /// Điểm quy đổi
        /// </summary>
        public int? DiemQuyDoi { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ThoiGianKTGanNhat { get; set; }

        public int? QuyMoId { get; set; }

        [Range(1, 12)]
        public int? ThangDuKienKT { get; set; }

        public Guid TanSuat { get; set; }

        public string MucTieu { get; set; }

        public string PhamVi { get; set; }

        public Guid Phong { get; set; }

        public Guid TruongDoan { get; set; }

        public Guid Manager { get; set; }

        public KeHoachNamEnum Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTNB.Extended.Extensions;

namespace KTNB.Extended.Entities
{
    [Table("EX_DM_PHONGBAN")]
    public class PhongBan
    {
        [Key]
        public int Id { get; set; }

        public Guid SourceId { get; set; }

        [Required]
        [StringLength(20)]
        public string MaDonVi { get; set; }

        [Required]
        [StringLength(250)]
        public string Ten { get; set; }

        public Guid MaTruongPhong { get; set; }

        public string TruongPhong { get; set; }

        public int NguonLuc { get; set; }

        public bool TrangThai { get; set; }

        [NotMapped]
        public string TrangThaiText
        {
            get { return TrangThai.GetTrangThai(); }
            set
            {
                // N/A
            }
        }

        public int Nam { get; set; }
    }
}

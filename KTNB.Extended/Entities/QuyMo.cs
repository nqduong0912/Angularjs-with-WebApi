using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Entities
{
    /// <summary>
    /// EX_DM_QUYMO
    /// </summary>
    public class QuyMo
    {
        [Key]
        public int Id { get; set; }

        public int BoQuyMoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        public int NguonLuc { get; set; }
    }
}

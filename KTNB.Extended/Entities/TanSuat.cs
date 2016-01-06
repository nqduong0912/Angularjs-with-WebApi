using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Entities
{
    /// <summary>
    /// EX_DM_TANSUAT
    /// </summary>
    public class TanSuat
    {
        [Key]
        public int Id { get; set; }

        public Guid SourceId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public int Value { get; set; }

        public int Nam { get; set; }
    }
}

using KTNB.Extended.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTNB.Extended.Extensions;

namespace KTNB.Extended.Entities
{
    /// <summary>
    /// EX_DM_NAM
    /// </summary>
    public class DanhMucNam
    {
        [Key]
        public int Id { get; set; }

        public int Nam { get; set; }

        public KeHoachNamEnum TrangThaiKeHoachNam { get; set; }

        public string TrangThaiKeHoachNamText
        {
            get
            {
                return TrangThaiKeHoachNam.GetText();
            }
            set
            {
                // N/A
            }
        }
    }
}

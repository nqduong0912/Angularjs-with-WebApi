using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;

namespace KTNB.Extended.Dal
{
    [Alias("EX_DM_QUYMO")]
    public class dm_quymo:EntityDao
    {
        public dm_quymo()
        {
        }

        public dm_quymo(string name, int resource)
        {
            this.Ten = name;
            this.NguonLuc = resource;
        }

        public int BoQuyMoId { get; set; }
        public string Ten { get; set; }
        public int NguonLuc { get; set; }
    }
}

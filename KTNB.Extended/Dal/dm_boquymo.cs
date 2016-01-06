using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;

namespace KTNB.Extended.Dal
{
    [Alias("EX_DM_BOQUYMO")]
    public class dm_boquymo : EntityDao
    {
        public dm_boquymo()
        {

            SourceId = Guid.NewGuid();
            Trangthai = false;
        }

        public string Ten { get; set; }

        public int Nam { get; set; }

        public string LoaiDTKT { get; set; }

        public bool Trangthai { get; set; }

        public Guid SourceId { get; set; }

        public List<dm_quymo> LstQuyMo { get; set; } 

    }
}

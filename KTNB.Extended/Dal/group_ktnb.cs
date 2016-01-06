using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;

namespace KTNB.Extended.Dal
{
    public class group_ktnb
    {
        [Alias("PK_GROUPID")]
        public Guid Pk_groupid { get; set; }

        [Alias("PK_ROLEID")]
        public Guid Fk_Roleid { get; set; }

        [Alias("NameGroup")]
        public string Id_group { get; set; }

        [Alias("Description")]
        public string Name { get; set; }

        [Alias("NameUser")]
        public string LeaderName { get; set; }

        [Alias("resource")]
        public int? Resource { get; set; }

        [Alias("IsActive")]
        public bool? IsActive { get; set; }
    }
}

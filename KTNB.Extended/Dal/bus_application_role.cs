using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Dal
{
    [Alias("T_APPLICATION_ROLE")]
    public class bus_application_role
    {
        [Required]
        public string FK_APPLICATIONID { get; set; }

        [Required]
        public string FK_ROLEID { get; set; }
    }
}

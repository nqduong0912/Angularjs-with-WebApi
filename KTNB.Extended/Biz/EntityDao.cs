using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Biz
{
    public class EntityDao
    {
        [AutoIncrement]
        public int Id { get; set; }
    }

    public class Lib_EntityDao
    {
        public Guid PK_DocumentID { get; set; }

        public int Status { get; set; }
    }
}

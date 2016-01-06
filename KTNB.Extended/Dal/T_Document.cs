using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ServiceStack.DataAnnotations;

namespace KTNB.Extended.Dal
{
    public class T_Document
    {
        [Alias("T_DOCUMENT")]
        public Guid Pk_documentid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid Fk_documentTypeid { get; set; }

        public Guid Fk_docspaceid { get; set; }

        public Guid fk_Parent { get; set; }

        public string Archivedpath { get; set; }

        public string Createby { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public string Company { get; set; }

        public int Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;

namespace KTNB.Extended.Dal
{
    [Alias("T_TYPE_DOC_PROPERTY")]
    public class T_TypeDocProperty : EntityDao
    {
        private DateTime _dateTimeval;

        public Guid fk_documentid { get; set; }

        public Guid fk_propertyid { get; set; }

        public int type { get; set; }

        public string textvalue { get; set; }

        public string extextvalue { get; set; }

        public int numberic { get; set; }

        public DateTime datetimevalue
        {
            get
            {
                if (_dateTimeval.Year < 1953)
                {
                    _dateTimeval = new DateTime(1954, 01, 01);
                }

                return this._dateTimeval;
            }
            set { _dateTimeval = value; }
        }
    }
}

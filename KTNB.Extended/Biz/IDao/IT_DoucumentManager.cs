using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;

namespace KTNB.Extended.Biz.IDao
{
    public interface IT_DoucumentManager : IEntityManager<T_Document>
    {
        void Insert(T_Document info, Guid doctype);
    }
}

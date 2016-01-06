using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;

namespace KTNB.Extended.Biz.IDao
{
    public interface IT_TypeDocProperty : IEntityManager<T_TypeDocProperty>
    {
        void AddNewDocProperty(T_TypeDocProperty info);

        string GetTextValue(string documentid);

        void UpdateTextValue(string documentid, string type);
    }
}

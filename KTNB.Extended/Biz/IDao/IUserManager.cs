using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;

namespace KTNB.Extended.Biz.IDao
{
    public interface IUserManager : IEntityManager<dm_nhansu>
    {
        int SetNhansu(dm_nhansu userNhansu, string pgd, string vaitro);
    }
}

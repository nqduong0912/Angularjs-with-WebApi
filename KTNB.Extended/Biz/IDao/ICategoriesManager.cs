using KTNB.Extended.Dal;
using KTNB.Extended.Entities.KhoiTaoJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTNB.Extended.Biz;

namespace KTNB.Extended.Biz.IDao
{
    public interface Iloaidoituongkiemtoan : IEntityManager<dm_loaidoituongkiemtoan>
    {
    }

    public interface Iserverconf : IEntityManager<qt_serverconf>
    {

    }

    public interface Iapplicationrole : IEntityManager<bus_application_role>
    {

    }

    public interface Iqlybotieuchi : IEntityManager<qt_qlybotieuchi>
    {

    }

    public interface Idotkiemtoan : IEntityManager<DotKiemToan>
    {

    }
}

using KTNB.Extended.Biz.IDao;
using KTNB.Extended.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTNB.Extended.Biz;
using KTNB.Extended.Entities.KhoiTaoJob;

namespace KTNB.Extended.Biz.Dao
{
    public class loaidoituongkiemtoan : EntityManager<dm_loaidoituongkiemtoan>, Iloaidoituongkiemtoan
    {

    }

    public class serverconf : EntityManager<qt_serverconf>, Iserverconf
    {

    }

    public class bus_app_role : EntityManager<bus_application_role>, Iapplicationrole
    {

    }

    public class qlybotieuchi : EntityManager<qt_qlybotieuchi>, Iqlybotieuchi
    {

    }

    public class dotkiemtoan : EntityManager<DotKiemToan>, Idotkiemtoan
    {

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;

namespace KTNB.Extended.Biz.IDao
{
    public interface IBoQuymoManager : IEntityManager<dm_boquymo>
    {
        List<dm_boquymo> GetListBoquymobyNamLoaiDoiTuongKiemToan(int nam, string loaidtkt);

        List<dm_boquymo> GetALLBoquymo();

        int UpdateBoQuyMo(dm_boquymo model);

        int InsertBoQuyMo(dm_boquymo info);

        dm_boquymo GetBoquymobyId(int id);

        int UpdateStatusBoQuyMo(dm_boquymo model);

        void InsertNewwBoQuyMo(dm_boquymo info);
    }
}

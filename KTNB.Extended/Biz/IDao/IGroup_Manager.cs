using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTNB.Extended.Dal;

namespace KTNB.Extended.Biz.IDao
{
    public interface IGroup_Manager
    {
        int AddNewGroup(group_ktnb info);

        List<group_ktnb> GetList_Donvi();

        group_ktnb GetListbyId_Donvi(string pk_groupid, string fk_Roleid, string leaderName);

        int UpdateGroup(group_ktnb model);
    }
}

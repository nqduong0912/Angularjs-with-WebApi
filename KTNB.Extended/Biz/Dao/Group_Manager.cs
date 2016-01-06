using KTNB.Extended.Biz.IDao;
using KTNB.Extended.Core;
using KTNB.Extended.Dal;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace KTNB.Extended.Biz.Dao
{
    public class Group_Manager : IGroup_Manager
    {
        public int AddNewGroup(group_ktnb info)
        {
            int reVal;
            using (var conn = DataConfig.GetConnection())
            {
                SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_UMS_AddNewGroup_KTNB";
                cmd.Parameters.Add(new SqlParameter("@PK_GROUPID", info.Pk_groupid));
                cmd.Parameters.Add(new SqlParameter("@PK_ROLEID", info.Fk_Roleid));
                cmd.Parameters.Add(new SqlParameter("@ID", info.Id_group));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", info.Name));
                cmd.Parameters.Add(new SqlParameter("@LEADERNAME", info.LeaderName));
                cmd.Parameters.Add(new SqlParameter("@RESOURCE", info.Resource));
                cmd.Parameters.Add(new SqlParameter("@ISACTIVE", info.IsActive));
                cmd.Parameters.Add(new SqlParameter("@RETURN", SqlDbType.Int));
                cmd.Parameters["@RETURN"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int.TryParse(cmd.Parameters["@RETURN"].Value.ToString(), out reVal);
            }

            return reVal;
        }

        public List<group_ktnb> GetList_Donvi()
        {
            using (var conn = DataConfig.GetConnection())
            {
                return conn.SqlList<group_ktnb>("EXEC EXT_UMS_GetListGroup_KTNB");
            }
        }

        public group_ktnb GetListbyId_Donvi(string pk_groupid, string fk_Roleid, string leaderName)
        {
            using (var conn = DataConfig.GetConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_UMS_GetListGroupbyID_KTNB";
                cmd.Parameters.Add(new SqlParameter("@PK_GROUPID", Guid.Parse(pk_groupid)));
                cmd.Parameters.Add(new SqlParameter("@PK_ROLEID", Guid.Parse(fk_Roleid)));
                cmd.Parameters.Add(new SqlParameter("@LEADERNAME", Guid.Parse(leaderName)));
                var dr = cmd.ExecuteReader();
                group_ktnb lstGroup = dr.ConvertTo<group_ktnb>();
                
                return lstGroup;
            }
        }

        public int UpdateGroup(group_ktnb model)
        {
            int reVal;
            using (var conn = DataConfig.GetConnection())
            {
                var cmd = (SqlCommand)conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_UMS_UpdateGroup_KTNB";
                cmd.Parameters.Add(new SqlParameter("@PK_GROUPID", model.Pk_groupid));
                cmd.Parameters.Add(new SqlParameter("@PK_ROLEID", model.Fk_Roleid));
                cmd.Parameters.Add(new SqlParameter("@ID", model.Id_group));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", model.Name));
                cmd.Parameters.Add(new SqlParameter("@LEADERNAME", model.LeaderName));
                cmd.Parameters.Add(new SqlParameter("@RESOURCE", model.Resource));
                cmd.Parameters.Add(new SqlParameter("@ISACTIVE", model.IsActive));
                cmd.Parameters.Add("@RETURN", SqlDbType.Int);
                cmd.Parameters["@RETURN"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int.TryParse(cmd.Parameters["@RETURN"].ToString(), out reVal);
            }

            return reVal;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using KTNB.Extended.Biz;
using KTNB.Extended.Biz.IDao;
using KTNB.Extended.Core;
using KTNB.Extended.Dal;

namespace KTNB.Extended.Biz.Dao
{
    public class T_DocumentManager : EntityManager<T_Document>, IT_DoucumentManager
    {
        public void Insert(T_Document info, Guid doctype)
        {
            using (var con = DataConfig.GetConnection())
            {
                SqlCommand cmd = (SqlCommand)con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_DMS_CreateDocument";
                cmd.Parameters.Add("PK_DOCUMENTID", SqlDbType.UniqueIdentifier).Value = info.Pk_documentid;
                cmd.Parameters.Add("FK_DOCUMENTTYPEID", SqlDbType.UniqueIdentifier).Value = info.Fk_documentTypeid;
                cmd.Parameters.Add("CREATEDBY", SqlDbType.VarChar).Value = info.Createby;
                cmd.Parameters.Add("YEAR", SqlDbType.Int).Value = info.Year;
                cmd.Parameters.Add("MONTH", SqlDbType.Int).Value = info.Month;
                cmd.Parameters.Add("STATUS", SqlDbType.Int).Value = info.Status;
                cmd.ExecuteNonQuery();
            }
        }
    }
}

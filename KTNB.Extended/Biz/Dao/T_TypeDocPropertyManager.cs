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
    public class T_TypeDocPropertyManager : EntityManager<T_TypeDocProperty>, IT_TypeDocProperty
    {
        public void AddNewDocProperty(T_TypeDocProperty info)
        {
            using (var conn = DataConfig.GetConnection())
            {
                SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_DMS_AddNewTypeProperty";
                cmd.Parameters.Add("@FK_DOCUMENTID", SqlDbType.UniqueIdentifier).Value = info.fk_documentid;
                cmd.Parameters.Add("@FK_PROPERTYID", SqlDbType.UniqueIdentifier).Value = info.fk_propertyid;
                cmd.Parameters.Add("@PROPERTYTYPE", SqlDbType.Int).Value = info.type;
                cmd.Parameters.Add("@TEXTVALUE", SqlDbType.NVarChar).Value = info.textvalue;
                cmd.Parameters.Add("@RETURNVALUE", SqlDbType.Int);
                cmd.Parameters["@RETURNVALUE"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int reVal = Convert.ToInt32(cmd.Parameters["@RETURNVALUE"].Value.ToString());
            }
        }

        public string GetTextValue(string documentid)
        {
            DataTable dt = new DataTable();
            string textvalue = string.Empty;
            SqlConnection conn = (SqlConnection)DataConfig.GetConnection();
            string sql = "SELECT TEXTVALUE FROM T_TYPE_DOC_PROPERTY WHERE FK_DOCUMENTID = '" + documentid +
                             "' AND FK_PROPERTYID = 'E4706D75-DFF3-475C-8C42-E60C31E30C78'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter dr = new SqlDataAdapter(cmd);
            dr.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                textvalue = dt.Rows[0]["TEXTVALUE"].ToString();
            }

            return textvalue;
        }

        public void UpdateTextValue(string documentid, string value)
        {
            using (var conn = DataConfig.GetConnection())
            {
                SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                string sql = "UPDATE T_TYPE_DOC_PROPERTY SET TEXTVALUE = N'" + value + "' WHERE FK_DOCUMENTID = '" + documentid +
                             "' AND  FK_PROPERTYID = 'E4706D75-DFF3-475C-8C42-E60C31E30C78'";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }
    }
}

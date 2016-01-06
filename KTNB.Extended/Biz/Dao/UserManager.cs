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
    public class usermanager : EntityManager<dm_nhansu>, IUserManager
    {
        //public int aasdfasd(dm_nhansu info, string pgd, string cn)
        //{
        //    var con = DataConfig.GetConnection();
        //    using (IDbTransaction trans = con.OpenTransaction(IsolationLevel.ReadCommitted))
        //    {
        //        int retVal = (int)con.Insert(info, selectIdentity: true);
        //        //thuc hien lan thao tac db thu 2
        //        trans.Commit();
        //    }
        //}

        public int SetNhansu(dm_nhansu userNhansu, string pgd, string vaitro)
        {
            int reVal;
            using (var con = DataConfig.GetConnection())
            {
                SqlCommand cmd = (SqlCommand)con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_Insert_User";
                cmd.Parameters.Add("PK_UserID", SqlDbType.UniqueIdentifier).Value = userNhansu.PK_UserID;
                cmd.Parameters.Add("UserCode", SqlDbType.NVarChar).Value = userNhansu.UserCode;
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = userNhansu.Name;
                cmd.Parameters.Add("PassWord", SqlDbType.NVarChar).Value = userNhansu.PassWord;
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = userNhansu.Description;
                cmd.Parameters.Add("IsExpired", SqlDbType.TinyInt).Value = userNhansu.IsExpired;
                cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = userNhansu.Email;
                cmd.Parameters.Add("MobilePhone", SqlDbType.NVarChar).Value = userNhansu.MobilePhone;
                cmd.Parameters.Add("Order_Number", SqlDbType.NVarChar).Value = userNhansu.Order_Number;
                cmd.Parameters.Add("Fullname", SqlDbType.NVarChar).Value = userNhansu.Fullname;
                cmd.Parameters.Add("Address", SqlDbType.NVarChar).Value = userNhansu.Address;
                cmd.Parameters.Add("IsAuthenticateSQL", SqlDbType.TinyInt).Value = userNhansu.IsAuthenticateSQL;
                cmd.Parameters.Add("AvatarURL", SqlDbType.NVarChar).Value = userNhansu.AvatarURL;
                cmd.Parameters.Add("JoinDate", SqlDbType.NVarChar).Value = userNhansu.JoinDate;
                cmd.Parameters.Add("BirthDate", SqlDbType.NVarChar).Value = userNhansu.BirthDate;
                cmd.Parameters.Add("EducationLevel", SqlDbType.NVarChar).Value = userNhansu.EducationLevel;
                cmd.Parameters.Add("@GroupID", SqlDbType.Char).Value = pgd;
                cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar).Value = vaitro;
                cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
                cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                reVal = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
            }

            return reVal;
        }
    }
}

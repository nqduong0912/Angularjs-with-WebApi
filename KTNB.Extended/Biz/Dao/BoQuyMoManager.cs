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
using ServiceStack.OrmLite;

namespace KTNB.Extended.Biz.Dao
{
    public class BoQuyMoManager : EntityManager<dm_boquymo>, IBoQuymoManager
    {
        public void InsertNewwBoQuyMo(dm_boquymo info)
        {
            string pgd = "";
            if (info.LstQuyMo.Count > 0)
            {
                pgd += "'";
                foreach (var item in info.LstQuyMo)
                {
                    pgd += item.Ten + "-" + item.NguonLuc.ToString() + ",";
                }
                pgd = pgd.Substring(0, pgd.Length - 2) +"', ','";
            }
            var con = DataConfig.GetConnection();
            using (IDbTransaction trans = con.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                int retVal = (int)con.Insert(info, selectIdentity: true);
                //retVal == ID cha
                con.SqlScalar<dm_quymo>("EXEC INSERT INTO EX_DM_QUYMO (Ten,NguonLuc, BoQuyMoId) SELECT col1 as Ten, col2 as NguonLuc, @IDCha from [SPLIT_EX_TEXT](@split, ',') ,@IDCha, @split", new { IDCha = retVal, split = pgd });
                //thuc hien lan thao tac db thu 2
                trans.Commit();
            }
        }
        public List<dm_boquymo> GetListBoquymobyNamLoaiDoiTuongKiemToan(int nam, string loaidtkt)
        {
            using (var conn = DataConfig.GetConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_DMS_GetList_Boquymo";
                cmd.Parameters.Add(new SqlParameter("@Nam",nam));
                cmd.Parameters.Add(new SqlParameter("@LoaiDTKT", loaidtkt));
                var dr = cmd.ExecuteReader();
                List<dm_boquymo> lstBoquymo = new List<dm_boquymo>();
                lstBoquymo = dr.ConvertToList<dm_boquymo>();
                return lstBoquymo;
            }
        }

        public List<dm_boquymo> GetALLBoquymo()
        {
            using (var conn = DataConfig.GetConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_DMS_GetALL_BoQuyMo";
                var dr = cmd.ExecuteReader();
                List<dm_boquymo> lstBoquymo = new List<dm_boquymo>();
                lstBoquymo = dr.ConvertToList<dm_boquymo>();
                return lstBoquymo;
            }
        }

        public int UpdateBoQuyMo(dm_boquymo model)
        {
            int reVal = 0;
            using (var conn = DataConfig.GetConnection())
            {
                SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_DMS_Update_BoQuyMo";
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = model.Id;
                cmd.Parameters.Add("@LoaiDTKT", SqlDbType.NVarChar).Value = model.LoaiDTKT;
                cmd.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = model.Ten;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = model.Nam;
                cmd.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = model.Trangthai;
                cmd.Parameters.Add("@RETURN", SqlDbType.Int);
                cmd.Parameters["@RETURN"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                reVal = Convert.ToInt32(cmd.Parameters["@RETURN"].Value.ToString());
            }
            return reVal;
        }

        public int InsertBoQuyMo(dm_boquymo info)
        {
            int reVal = 0;
            using (var conn = DataConfig.GetConnection())
            {
                SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_DMS_Insert_BoQuyMo";
                cmd.Parameters.Add("@LoaiDTKT", SqlDbType.NVarChar).Value = info.LoaiDTKT;
                cmd.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = info.Ten;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = info.Nam;
                cmd.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = info.Trangthai;
                cmd.Parameters.Add("@SourceId", SqlDbType.UniqueIdentifier).Value = info.SourceId;
                cmd.Parameters.Add("@ParrentId", SqlDbType.Int);
                cmd.Parameters["@ParrentId"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                reVal = Convert.ToInt32(cmd.Parameters["@ParrentId"].Value.ToString());
            }
            return reVal;
        }

        public dm_boquymo GetBoquymobyId(int id)
        {
            dm_boquymo boQuyMo = null;
            using (var conn = DataConfig.GetConnection())
            {
                SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_DMS_GetBoQuyMobyId";
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.ExecuteNonQuery();
                var dr = cmd.ExecuteReader();
                boQuyMo = dr.ConvertTo<dm_boquymo>();
            }
            return boQuyMo;
        }

        public int UpdateStatusBoQuyMo(dm_boquymo model)
        {
            int reVal = 0;
            using (var conn = DataConfig.GetConnection())
            {
                SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EXT_DMS_UpdateStatus_BoQuyMo";
                cmd.Parameters.Add(new SqlParameter("@ID", model.Id));
                cmd.Parameters.Add("@RETURN", SqlDbType.Int);
                cmd.Parameters["@RETURN"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                reVal = int.Parse(cmd.Parameters["@RETURN"].ToString());
            }
            return reVal;
        }
    }
}

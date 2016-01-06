using KTNB.Extended.Commons.Logs;
using KTNB.Extended.Core;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace KTNB.Extended.Biz
{
    public class EntityManager<TEntityType> : IEntityManager<TEntityType>
    {
        public int Insert(TEntityType entity)
        {
            using (var con = DataConfig.GetConnection())
            {
                //con.CreateTable<TEntityType>(false);
                return (int)con.Insert(entity, selectIdentity: true);
            }
        }

        public int Update(TEntityType entity)
        {
            using (var con = DataConfig.GetConnection())
            {
                return con.Update(entity);
            }
        }

        public int InsertOrUpdateAll(List<TEntityType> lstEntity)
        {
            using (var con = DataConfig.GetConnection())
            {
                return con.SaveAll(lstEntity);
            }
        }

        public int Delete(int id)
        {
            using (var con = DataConfig.GetConnection())
            {
                return con.Delete<TEntityType>(new { Id = id });
            }
        }

        public TEntityType GetInfo(int id)
        {
            using (var con = DataConfig.GetConnection())
            {
                return con.SingleById<TEntityType>(id);
            }
        }

        public TEntityType GetInfo(Expression<Func<TEntityType, bool>> func)
        {
            using (var con = DataConfig.GetConnection())
            {
                return con.Single<TEntityType>(func);
            }
        }

        public List<TEntityType> GetList(Expression<Func<TEntityType, bool>> func = null)
        {
            using (var con = DataConfig.GetConnection())
            {
                return con.Select<TEntityType>(x => x.Where(func));
            }
        }

        public List<TEntityType> GetList(int rows, Expression<Func<TEntityType, bool>> func = null, int skip = 0)
        {
            using (var con = DataConfig.GetConnection())
            {
                return con.Select<TEntityType>(x => x.Where(func).Limit(rows: rows, skip: skip));
            }
        }

        public List<TEntityType> GetList(int rows, out long total, Expression<Func<TEntityType, bool>> func = null, int skip = 0)
        {
            using (var con = DataConfig.GetConnection())
            {
                total = con.Count<TEntityType>(x => x.Where(func));
                return con.Select<TEntityType>(x => x.Where(func).Limit(rows: rows, skip: skip));
            }
        }

        //public List<TEntityType> GetDMFromLibrary(string docType, string docField, string proField, string condition)
        //{
        //    using (var con = DataConfig.GetConnection())
        //    {
        //        return con.SqlList<TEntityType>("EXEC EXT_DMS_GetDocument_List @DOCUMENTTYPEID, @DOC_FIELDS, @PROPERTY_FIELDS, @CRITERIA", new { DOCUMENTTYPEID = docType, DOC_FIELDS = docField, PROPERTY_FIELDS = proField, CRITERIA = condition });
        //    }
        //}

        public void SetupSite(bool isFirst = true)
        {
            try
            {
                using (var con = DataConfig.GetConnection())
                {
                    //con.CreateTable<Contacts>(false);
                    //con.CreateTable<Homepage>(false);
                    //con.CreateTable<Introduction>(false);
                    //con.CreateTable<News>(false);
                    //con.CreateTable<NewsCategories>(false);
                    //con.CreateTable<Products>(false);
                    //con.CreateTable<ProductsCategories>(false);
                    //Alter Table
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private static List<string> GetColumnNames(IDbConnection db, string tableName)
        {
            var columns = new List<string>();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "exec sp_columns " + tableName;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var ordinal = reader.GetOrdinal("COLUMN_NAME");
                    columns.Add(reader.GetString(ordinal));
                }

                reader.Close();
            }

            return columns;
        }
    }

    public interface IEntityManager<TEntityType>
    {
        int Insert(TEntityType entity);

        int Update(TEntityType entity);

        int InsertOrUpdateAll(List<TEntityType> lstEntity);

        int Delete(int id);

        TEntityType GetInfo(int id);

        TEntityType GetInfo(Expression<Func<TEntityType, bool>> func);

        List<TEntityType> GetList(Expression<Func<TEntityType, bool>> func = null);

        List<TEntityType> GetList(int rows, Expression<Func<TEntityType, bool>> func = null, int skip = 0);

        List<TEntityType> GetList(int rows, out long total, Expression<Func<TEntityType, bool>> func = null, int skip = 0);

        void SetupSite(bool isFirst = true);
    }
}
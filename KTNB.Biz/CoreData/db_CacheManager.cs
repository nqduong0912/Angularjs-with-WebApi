namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_CacheManager : ADBManager
    {
        private Database _db;
        private string mvarDBName;
        private static db_CacheManager mvarInstance;
        private UserContext mvarUserContext;

        public db_CacheManager(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            this.mvarDBName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(this.mvarDBName);
        }

        public db_CacheManager(UserContext usrCTX, string databaseName)
        {
            this.mvarUserContext = usrCTX;
            this.mvarDBName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(this.mvarDBName);
        }

        public override int addnewDataSet(DataSet ds_Document)
        {
            return -1;
        }

        public override int delete(string Condition)
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "DELETE FROM T_CACHE_MANAGER WHERE 1=1 ";
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + Condition;
            }
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText);
                    if (num == 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 0;
                    }
                    transaction.Commit();
                }
                catch
                {
                    num = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return num;
        }

        public override int deleteByID(string ID)
        {
            int num;
            string commandText = string.Empty;
            commandText = "DELETE FROM T_CACHE_MANAGER WHERE PK_CACHEID='" + ID + "' ";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText);
                    if (num == 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 0;
                    }
                    transaction.Commit();
                }
                catch
                {
                    num = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return num;
        }

        public override DataSet getByID(string ID, string Query)
        {
            try
            {
                string commandText = string.Empty;
                if (string.IsNullOrEmpty(Query))
                {
                    commandText = "select * ";
                    commandText = (commandText + " from T_CACHE_MANAGER") + " where PK_CACHEID='" + ID + "'";
                }
                else
                {
                    commandText = ("select " + Query + " from T_CACHE_MANAGER") + " where PK_CACHEID='" + ID + "'";
                }
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public override DataSet getEmpty(string Query)
        {
            try
            {
                string commandText = string.Empty;
                if (!string.IsNullOrEmpty(Query))
                {
                    commandText = "SELECT " + Query + " FROM T_CACHE_MANAGER WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_CACHE_MANAGER WHERE 1=2";
                }
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public override DataSet getList(string Condition, string Query)
        {
            string commandText = string.Empty;
            if (string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT * FROM T_CACHE_MANAGER";
            }
            else
            {
                commandText = "SELECT " + Query + " FROM T_CACHE_MANAGER";
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + Condition;
            }
            try
            {
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public IDataReader getlistReader(string Condition, string Query)
        {
            string commandText = string.Empty;
            if (string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT * FROM T_CACHE_MANAGER";
            }
            else
            {
                commandText = "SELECT " + Query + " FROM T_CACHE_MANAGER";
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + Condition;
            }
            try
            {
                return this._db.ExecuteReader(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public static db_CacheManager Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_CacheManager(usrCTX);
            }
            return mvarInstance;
        }

        public override int saveDataSet(DataSet ds_Document)
        {
            return -1;
        }

        public int Truncate()
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "TRUNCATE TABLE T_CACHE_MANAGER";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText);
                    if (num == 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 0;
                    }
                    transaction.Commit();
                }
                catch
                {
                    num = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return num;
        }
    }
}


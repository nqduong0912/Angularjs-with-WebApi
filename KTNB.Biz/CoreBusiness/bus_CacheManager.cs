namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using System.Data;
    using vpb.app.business.ktnb.CoreData;

    public class bus_CacheManager
    {
        private db_CacheManager _db;
        private string mvarDatabaseName;
        private static bus_CacheManager mvarInstance;
        private UserContext mvarUserContext;

        public bus_CacheManager(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_CacheManager(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_CacheManager(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_CacheManager(this.mvarUserContext, databaseName);
        }

        public int addnewDataSet(DataSet ds_Document)
        {
            return this._db.addnewDataSet(ds_Document);
        }

        public int delete(string Condition)
        {
            return this._db.delete(Condition);
        }

        public int deleteByID(string ID)
        {
            return this._db.deleteByID(ID);
        }

        public DataSet getByID(string ID, string Query)
        {
            return this._db.getByID(ID, Query);
        }

        public DataSet getEmpty(string Query)
        {
            return this._db.getEmpty(Query);
        }

        public DataSet getList(string Condition, string Query)
        {
            return this._db.getList(Condition, Query);
        }

        public IDataReader getlistReader(string Condition, string Query)
        {
            return this._db.getlistReader(Condition, Query);
        }

        public static bus_CacheManager Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_CacheManager(usrCTX);
            }
            return mvarInstance;
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }

        public int Truncate()
        {
            return this._db.Truncate();
        }
    }
}


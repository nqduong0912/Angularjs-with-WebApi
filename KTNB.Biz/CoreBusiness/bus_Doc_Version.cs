namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using System.Data;
    using vpb.app.business.ktnb.CoreData;

    public class bus_Doc_Version
    {
        private db_Doc_Version _db;
        private string mvarDatabaseName;
        private static bus_Doc_Version mvarInstance;
        private UserContext mvarUserContext;

        public bus_Doc_Version(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Doc_Version(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Doc_Version(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Doc_Version(this.mvarUserContext, databaseName);
        }

        public int addnewDataSet(DataSet ds)
        {
            return this._db.addnewDataSet(ds);
        }

        public bool Delete(Guid UserID)
        {
            return false;
        }

        public bool Delete(string UserName)
        {
            return false;
        }

        public DataSet getByID(string ID, string Query)
        {
            return this._db.getByID(ID, Query);
        }

        public DataSet GetByName(string UserName, string Query)
        {
            return null;
        }

        public DataSet getEmpty(string Query)
        {
            return this._db.getEmpty(Query);
        }

        public DataSet getList(string Condition, string Query)
        {
            return this._db.getList(Condition, Query);
        }

        public static bus_Doc_Version Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Doc_Version(usrCTX);
            }
            return mvarInstance;
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }
    }
}


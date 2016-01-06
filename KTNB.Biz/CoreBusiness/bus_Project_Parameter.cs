namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using vpb.app.business.ktnb.CoreData;
    using System.Data;

    public class bus_Project_Parameter
    {
        private db_Project_Parameter _db;
        private string mvarDatabaseName;
        private static bus_Project_Parameter mvarInstance;
        private UserContext mvarUserContext;

        public bus_Project_Parameter(UserContext usrCTX)
        {
            this.mvarDatabaseName = "";
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Project_Parameter(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Project_Parameter(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = "";
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Project_Parameter(this.mvarUserContext, databaseName);
        }

        public int addnewDataSet(DataSet ds_Document)
        {
            return this._db.addnewDataSet(ds_Document);
        }

        public int CreateParameter(string Name, string FullName, string Value)
        {
            return this._db.CreateParameter(Name, FullName, Value);
        }

        public int CreateParameter(string Name, string FullName, string Value, string GroupName)
        {
            return this._db.CreateParameter(Name, FullName, Value, GroupName);
        }

        public int CreateParameter(string Name, string FullName, string Value, string GroupName, string ApplicationID, string ParaType)
        {
            return this._db.CreateParameter(Name, FullName, Value, GroupName, ApplicationID, ParaType);
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

        public static bus_Project_Parameter Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Project_Parameter(usrCTX);
            }
            return mvarInstance;
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }

        public int UpdateParameter(string Name, string FullName, string Value)
        {
            return this._db.UpdateParameter(Name, FullName, Value);
        }

        public int UpdateParameter(string Name, string FullName, string Value, string GroupName)
        {
            return this._db.UpdateParameter(Name, FullName, Value, GroupName);
        }

        public int UpdateParameter(string Name, string FullName, string Value, string GroupName, string Application, string ParaType)
        {
            return this._db.UpdateParameter(Name, FullName, Value, GroupName, Application, ParaType);
        }
    }
}


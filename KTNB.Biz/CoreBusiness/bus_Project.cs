namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using System.Data;
    using vpb.app.business.ktnb.CoreData;

    public class bus_Project
    {
        private db_Project _db;
        private string mvarDatabaseName;
        private static bus_Project mvarInstance;
        private UserContext mvarUserContext;

        public bus_Project(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Project(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Project(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Project(this.mvarUserContext, databaseName);
        }

        public int addnewDataSet(DataSet ds_Document)
        {
            return this._db.addnewDataSet(ds_Document);
        }

        public int delete(string Condition)
        {
            return this._db.delete(Condition);
        }

        public int deleteApplicationGroup(string ApplicationID, string RoleID, string GroupID)
        {
            return this._db.deleteApplicationGroup(ApplicationID, RoleID, GroupID);
        }

        public int deleteByID(string ID)
        {
            return this._db.deleteByID(ID);
        }

        public DataSet getApplicationByRole(string RoleID, string Condition)
        {
            return this._db.getApplicationByRole(RoleID, Condition);
        }

        public DataSet getByID(string ID, string Query)
        {
            return this._db.getByID(ID, Query);
        }

        public DataSet getComponentByApplication(string ApplicationID, string RoleID)
        {
            return this._db.getComponentByApplication(ApplicationID, RoleID);
        }

        public DataSet getComponentByApplicationNGroup(string ApplicationID, string RoleID, string GroupID)
        {
            return this._db.getComponentByApplicationNGroup(ApplicationID, RoleID, GroupID);
        }

        public DataSet getEmpty(string Query)
        {
            return this._db.getEmpty(Query);
        }

        public DataSet getList(string Condition, string Query)
        {
            return this._db.getList(Condition, Query);
        }

        public IDataReader GetProjectInfo(string Condition, string Query)
        {
            return this._db.GetProjectInfo(Condition, Query);
        }

        public int GetProjectStatus(string ProjectID)
        {
            return this._db.GetProjectStatus(ProjectID);
        }

        public DataSet getRoleOnApplication(string ID)
        {
            return this._db.getRoleOnApplication(ID);
        }

        public static bus_Project Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Project(usrCTX);
            }
            return mvarInstance;
        }

        public int MapApplicationGroup(string ApplicationID, string RoleID, string GroupID)
        {
            return this._db.MapApplicationGroup(ApplicationID, RoleID, GroupID);
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }
    }
}


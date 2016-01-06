namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using vpb.app.business.ktnb.CoreData;
    using System.Data;

    public class bus_Property
    {
        private db_Property _db;
        private string mvarDatabaseName;
        private static bus_Property mvarInstance;
        private UserContext mvarUserContext;

        public bus_Property(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Property(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Property(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Property(this.mvarUserContext, databaseName);
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

        public DataSet GetLookUpValue(string PropertyID)
        {
            return this._db.GetLookUpValue(PropertyID);
        }

        public DataSet GetLookUpValue(string PropertyID, int DocStatus)
        {
            return this._db.GetLookUpValue(PropertyID, DocStatus);
        }

        public static bus_Property Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Property(usrCTX);
            }
            return mvarInstance;
        }

        public int RemoveLookupValue(string propertyID)
        {
            return this._db.RemoveLookupValue(propertyID);
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }

        public int SetLookUpProperty(string PropertyID, string LookUpFolderID, string LookUpDocTypeID, string LookUpValue)
        {
            return this._db.SetLookUpProperty(PropertyID, LookUpFolderID, LookUpDocTypeID, LookUpValue);
        }

        public int UpDateDocument(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }
    }
}


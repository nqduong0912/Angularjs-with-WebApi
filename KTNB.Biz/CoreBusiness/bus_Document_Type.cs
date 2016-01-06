namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using System.Data;
    using vpb.app.business.ktnb.CoreData;

    public class bus_Document_Type
    {
        private db_Document_Type _db;
        private string mvarDatabaseName;
        private static bus_Document_Type mvarInstance;
        private UserContext mvarUserContext;

        public bus_Document_Type(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Document_Type(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Document_Type(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Document_Type(this.mvarUserContext, databaseName);
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

        public DataSet GetDocSpaceList(string DocTypeID)
        {
            return this._db.GetDocSpaceList(DocTypeID);
        }

        public DataSet GetDocTypeByDocSpace(string DocSpaceID)
        {
            return this._db.GetDocTypeByDocSpace(DocSpaceID);
        }

        public DataSet getEmpty(string Query)
        {
            return this._db.getEmpty(Query);
        }

        public DataSet getList(string Condition, string Query)
        {
            return this._db.getList(Condition, Query);
        }

        public DataSet GetProperyList(string DocTypeID)
        {
            return this._db.GetProperyList(DocTypeID);
        }

        public static bus_Document_Type Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Document_Type(usrCTX);
            }
            return mvarInstance;
        }

        public bool IsPropertyTransformed2Table(string PropertyID, string DocumentTypeID)
        {
            return this._db.IsPropertyTransformed2Table(PropertyID, DocumentTypeID);
        }

        public int MapDocTypeDocSpace(string DocTypeID, string DocSpaceID, string ComponentID)
        {
            return this._db.MapDocTypeDocSpace(DocTypeID, DocSpaceID, ComponentID);
        }

        public int RemoveDocSpace(string DocSpaceID, string DocTypeID)
        {
            return this._db.RemoveDocSpace(DocSpaceID, DocTypeID);
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }

        public DataRow TransformDocType2Table(string DocumentTypeID)
        {
            return this._db.TransformDocType2Table(DocumentTypeID);
        }

        public DataRow TransformProperty2Table(string PropertyID, string DocumentTypeID)
        {
            return this._db.TransformProperty2Table(PropertyID, DocumentTypeID);
        }

        public int UnMapDocTypeDocSpace(string DocSpaceID, string DocTypeID)
        {
            return this._db.RemoveDocSpace(DocSpaceID, DocTypeID);
        }

        public int UpdateDocSpace(string DocSpaceID, string DocTypeID)
        {
            return this._db.UpdateDocSpace(DocSpaceID, DocTypeID);
        }

        public int VerifyDocTypes()
        {
            return this._db.VerifyDocTypes();
        }

        public int VerifyDocTypes(string DocumentTypeID)
        {
            return this._db.VerifyDocTypes(DocumentTypeID);
        }
    }
}


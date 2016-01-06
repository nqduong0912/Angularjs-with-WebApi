namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using vpb.app.business.ktnb.CoreData;
    using System.Data.Common;

    public class bus_Type_Doc_Property
    {
        private db_Type_Doc_Property _db;
        private string mvarDatabaseName;
        private static bus_Type_Doc_Property mvarInstance;
        private UserContext mvarUserContext;

        public bus_Type_Doc_Property(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Type_Doc_Property(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Type_Doc_Property(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Type_Doc_Property(this.mvarUserContext, databaseName);
        }

        public int addnewDataSet(DataSet ds)
        {
            int num = this._db.addnewDataSet(ds);
            if (num != 0)
            {
                string str = ds.Tables[0].Rows[0]["FK_DocumentID"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    bus_Document document = new bus_Document(this.mvarUserContext, this.mvarDatabaseName);
                    document.deleteByID(str);
                    document = null;
                }
            }
            return num;
        }

        public int CountPropertyValue(string PropertyID, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            return this._db.CountPropertyValue(PropertyID, value);
        }

        public int CountPropertyValue(string PropertyID, string value, string ExclusiveDocumentID)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            return this._db.CountPropertyValue(PropertyID, value, ExclusiveDocumentID);
        }

        public int CreateDocBodyXML(DataSet ds_DocumentProperty)
        {
            return this._db.CreateDocBodyXML(ds_DocumentProperty);
        }

        public int delete(string Condition)
        {
            return this._db.delete(Condition);
        }

        public int deleteByID(string ID)
        {
            return this._db.deleteByID(ID);
        }

        public int ExtractDataToTable(string DocumentID, DbTransaction transaction, Database _database)
        {
            return this._db.ExtractDataToTable(DocumentID, transaction, _database);
        }

        public DataSet getByID(string ID, string Query)
        {
            return this._db.getByID(ID, Query);
        }

        public DataSet getEmpty(string Query)
        {
            return this._db.getEmpty(Query);
        }

        public DataSet getEmptyXML(string Query)
        {
            return this._db.getEmptyXML(Query);
        }

        public DataSet getList(string Condition, string Query)
        {
            return this._db.getList(Condition, Query);
        }

        public string GetPropertyValueOnDocument(string DocumentID, string PropertyID)
        {
            return this._db.GetPropertyValueOnDocument(DocumentID, PropertyID);
        }

        public static bus_Type_Doc_Property Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Type_Doc_Property(usrCTX);
            }
            return mvarInstance;
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }
        public string updatePropertyValue(string DocumentID, string PropertyID, string PropertyValue)
        {
            return this._db.updatePropertyValue(DocumentID, PropertyID, PropertyValue);
        }
        public string updatePropertyValue(string DocumentID, string PropertyID, string PropertyValue, int propertyType)
        {
            return this._db.updatePropertyValue(DocumentID, PropertyID, PropertyValue, propertyType);
        }

        public int updatePropertyValueNumber(string DocumentiD, string PropertyID, int PropertyType, decimal PropertyValue)
        {
            return this._db.updatePropertyValueNumber(DocumentiD, PropertyID, PropertyType, PropertyValue);
        }
    }
}


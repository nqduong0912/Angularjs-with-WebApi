namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Type_Doc_Property : ADBManager
    {
        private Database _db;
        private string mvarDatabaseName;
        private static db_Type_Doc_Property mvarInstance;
        private UserContext mvarUserContext;

        public db_Type_Doc_Property(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Type_Doc_Property(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(databaseName);
        }

        public override int addnewDataSet(DataSet ds_DocumentProperty)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_AddNew_TypeDocProperty");
            this._db.AddInParameter(storedProcCommand, "FK_DocumentID", DbType.Guid, "FK_DocumentID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_PropertyID", DbType.Guid, "FK_PropertyID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "PropertyType", DbType.Byte, "PropertyType", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "TextValue", DbType.String, "TextValue", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ETextValue", DbType.String, "ETextValue", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NumericValue", DbType.Double, "NumericValue", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DateTimeValue", DbType.DateTime, "DateTimeValue", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 4);
            DbCommand updateCommand = null;
            DbCommand deleteCommand = null;
            string str = string.Empty;
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds_DocumentProperty, ds_DocumentProperty.Tables[0].TableName, storedProcCommand, updateCommand, deleteCommand, transaction);
                    parameterValue = (int)this._db.GetParameterValue(storedProcCommand, "ReturnValue");
                    if (parameterValue == 0)
                    {
                        transaction.Commit();
                        str = this._db.GetParameterValue(storedProcCommand, "FK_DocumentID").ToString();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception)
                {
                    parameterValue = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            if (!string.IsNullOrEmpty(str))
            {
                this.ExtractDataToTable(str, 0);
            }
            return parameterValue;
        }

        public int AddPropertyValue(string DocumentID, string PropertyID, int PropertyType, string PropertyValue)
        {
            try
            {
                return -1;
            }
            catch
            {
                return -1;
            }
        }

        public int CountPropertyValue(string PropertyID, string value)
        {
            string str = " AND FK_PROPERTYID='" + PropertyID + "'";
            DataSet set = db_Property.Instance(this.mvarUserContext).getByID(PropertyID, "Type");
            switch (int.Parse(set.Tables[0].Rows[0]["Type"].ToString()))
            {
                case 8:
                    str = str + " AND TEXTVALUE=N'" + value + "'";
                    break;

                case 6:
                    str = str + " AND NUMERICVALUE=" + value;
                    break;

                case 3:
                    str = str + " AND DATETIMEVALUE='" + value + "'";
                    break;
            }
            string commandText = "Select N=Count(1) From T_TYPE_DOC_PROPERTY WHERE 1=1" + str;
            return int.Parse(this._db.ExecuteScalar(CommandType.Text, commandText).ToString());
        }

        public int CountPropertyValue(string PropertyID, string value, string ExclusiveDocumentID)
        {
            string str = " AND FK_PROPERTYID='" + PropertyID + "'";
            DataSet set = db_Property.Instance(this.mvarUserContext).getByID(PropertyID, "Type");
            switch (int.Parse(set.Tables[0].Rows[0]["Type"].ToString()))
            {
                case 8:
                    str = str + " AND TEXTVALUE=N'" + value + "'";
                    break;

                case 6:
                    str = str + " AND NUMERICVALUE=" + value;
                    break;

                case 3:
                    str = str + " AND DATETIMEVALUE='" + value + "'";
                    break;
            }
            str = str + " AND FK_DocumentID <> '" + ExclusiveDocumentID + "'";
            string commandText = "Select N=Count(1) From T_TYPE_DOC_PROPERTY WHERE 1=1" + str;
            return int.Parse(this._db.ExecuteScalar(CommandType.Text, commandText).ToString());
        }

        public int CreateDocBodyXML(DataSet ds_DocumentProperty)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_AddNew_DocBodyXML");
            this._db.AddInParameter(storedProcCommand, "FK_DocumentID", DbType.Guid, "FK_DocumentID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Body", DbType.Xml, "Body", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 4);
            DbCommand updateCommand = null;
            DbCommand deleteCommand = null;
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds_DocumentProperty, ds_DocumentProperty.Tables[0].TableName, storedProcCommand, updateCommand, deleteCommand, transaction);
                    parameterValue = (int)this._db.GetParameterValue(storedProcCommand, "ReturnValue");
                    if (parameterValue == 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception)
                {
                    parameterValue = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return parameterValue;
        }

        public override int delete(string Condition)
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "DELETE FROM T_TYPE_DOC_PROPERTY WHERE 1=1 ";
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
            return -2;
        }

        private int ExtractDataToTable(string DocumentID)
        {
            string str = this.getDocumentTypeByDocument(DocumentID);
            try
            {
                DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.CORE_DATATOTABLE");
                this._db.AddInParameter(storedProcCommand, "DocumentTypeID", DbType.String, str);
                return this._db.ExecuteNonQuery(storedProcCommand);
            }
            catch
            {
                return -1;
            }
        }

        private int ExtractDataToTable(string DocumentID, byte isUpdate)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.CORE_DATATOTABLE_BY_DOCUMENT");
            this._db.AddInParameter(storedProcCommand, "DocumentID", DbType.String, DocumentID);
            this._db.AddInParameter(storedProcCommand, "isUpdate", DbType.Byte, isUpdate);
            return this._db.ExecuteNonQuery(storedProcCommand);
        }

        public int ExtractDataToTable(string DocumentID, DbTransaction transaction, Database _database)
        {
            try
            {
                string str = this.getDocumentTypeByDocument(DocumentID);
                DbCommand storedProcCommand = _database.GetStoredProcCommand("NTL_CORE_DATATOTABLE");
                _database.AddInParameter(storedProcCommand, "DocumentTypeID", DbType.String, str);
                _database.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 4);
                _database.ExecuteNonQuery(storedProcCommand, transaction);
                if (((int)_database.GetParameterValue(storedProcCommand, "ReturnValue")) == 0)
                {
                    return 0;
                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public override DataSet getByID(string ID, string Query)
        {
            return null;
        }

        public DataSet GetDocumentProperties(string DocumentID, string Condition)
        {
            return null;
        }

        private string getDocumentTypeByDocument(string DocumentID)
        {
            DataSet set = new db_Document(this.mvarUserContext, this.mvarDatabaseName).getByID(DocumentID, "FK_DOCUMENTTYPEID");
            if ((set != null) && (set.Tables[0].Rows.Count != 0))
            {
                return set.Tables[0].Rows[0]["FK_DOCUMENTTYPEID"].ToString();
            }
            return string.Empty;
        }

        public override DataSet getEmpty(string Query)
        {
            string commandText = string.Empty;
            if (!string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT " + Query + " FROM T_TYPE_DOC_PROPERTY WHERE 1=2";
            }
            else
            {
                commandText = "SELECT * FROM T_TYPE_DOC_PROPERTY WHERE 1=2";
            }
            return this._db.ExecuteDataSet(CommandType.Text, commandText);
        }

        public DataSet getEmptyXML(string Query)
        {
            string commandText = string.Empty;
            if (!string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT " + Query + " FROM T_DOC_BODY_XML WHERE 1=2";
            }
            else
            {
                commandText = "SELECT * FROM T_DOC_BODY_XML WHERE 1=2";
            }
            return this._db.ExecuteDataSet(CommandType.Text, commandText);
        }

        public override DataSet getList(string Condition, string Query)
        {
            string commandText = string.Empty;
            if (string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT * FROM T_TYPE_DOC_PROPERTY WHERE 1=1 ";
            }
            else
            {
                commandText = "SELECT " + Query + " FROM T_TYPE_DOC_PROPERTY WHERE 1=1 ";
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + Condition;
            }
            return this._db.ExecuteDataSet(CommandType.Text, commandText);
        }

        public string GetPropertyValueOnDocument(string DocumentID, string PropertyID)
        {
            string str = string.Empty;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_GetPropertyValue_ByDocumentID");
            this._db.AddInParameter(storedProcCommand, "PK_DocumentID", DbType.String, DocumentID);
            this._db.AddInParameter(storedProcCommand, "PK_PropertyID", DbType.String, PropertyID);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                try
                {
                    str = this._db.ExecuteScalar(storedProcCommand).ToString();
                }
                catch (Exception)
                {
                    str = "";
                }
            }
            return str;
        }

        public static db_Type_Doc_Property Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Type_Doc_Property(usrCTX);
            }
            return mvarInstance;
        }

        public override int saveDataSet(DataSet ds)
        {
            int parameterValue = 0;
            DbCommand insertCommand = null;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_TypeDocProperty");
            this._db.AddInParameter(storedProcCommand, "FK_DocumentID", DbType.Guid, "FK_DocumentID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_PropertyID", DbType.Guid, "FK_PropertyID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "PropertyType", DbType.Byte, "PropertyType", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "TextValue", DbType.String, "TextValue", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ETextValue", DbType.String, "ETextValue", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NumericValue", DbType.Double, "NumericValue", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DateTimeValue", DbType.DateTime, "DateTimeValue", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 4);
            DbCommand deleteCommand = null;
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds, ds.Tables[0].TableName, insertCommand, storedProcCommand, deleteCommand, transaction);
                    parameterValue = (int)this._db.GetParameterValue(storedProcCommand, "ReturnValue");
                    if (parameterValue == 0)
                    {
                        transaction.Commit();
                        this.ExtractDataToTable(this._db.GetParameterValue(storedProcCommand, "FK_DocumentID").ToString(), 1);
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception)
                {
                    parameterValue = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return parameterValue;
        }
        public string updatePropertyValue(string DocumentID, string PropertyID, string PropertyValue)
        {
            string message = string.Empty;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_Update_PropertyValue_OnDocument");
            this._db.AddInParameter(storedProcCommand, "DocumentID", DbType.String, DocumentID);
            this._db.AddInParameter(storedProcCommand, "PropertyID", DbType.String, PropertyID);
            this._db.AddInParameter(storedProcCommand, "PropertyValue", DbType.String, PropertyValue);
            DbConnection connection = this._db.CreateConnection();
            try
            {
                connection.Open();
                message = this._db.ExecuteScalar(storedProcCommand).ToString();
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
            return message;
        }
        public string updatePropertyValue(string DocumentID, string PropertyID, string PropertyValue, int PropertyType)
        {
            string message = string.Empty;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.EXT_Update_PropertyValue_OnDocument");
            this._db.AddInParameter(storedProcCommand, "DocumentID", DbType.String, DocumentID);
            this._db.AddInParameter(storedProcCommand, "PropertyID", DbType.String, PropertyID);
            this._db.AddInParameter(storedProcCommand, "PropertyValue", DbType.String, PropertyValue);
            this._db.AddInParameter(storedProcCommand, "PROPERTYTYPE", DbType.Int32, PropertyType);
            DbConnection connection = this._db.CreateConnection();
            try
            {
                connection.Open();
                message = this._db.ExecuteScalar(storedProcCommand).ToString();
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
            return message;
        }

        public int updatePropertyValueNumber(string DocumentID, string PropertyID, int PropertyType, decimal PropertyValue)
        {
            int num;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_Update_PropertyValue_OnDocument_Number");
            this._db.AddInParameter(storedProcCommand, "DocumentID", DbType.String, DocumentID);
            this._db.AddInParameter(storedProcCommand, "PropertyID", DbType.String, PropertyID);
            this._db.AddInParameter(storedProcCommand, "PropertyType", DbType.Int32, PropertyType);
            this._db.AddInParameter(storedProcCommand, "PropertyValue", DbType.Decimal, PropertyValue);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.ExecuteNonQuery(storedProcCommand, transaction);
                    transaction.Commit();
                    num = 0;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    num = -1;
                }
            }
            return num;
        }
    }
}


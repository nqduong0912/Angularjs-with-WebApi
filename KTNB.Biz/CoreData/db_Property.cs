namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Property : ADBManager
    {
        private Database _db;
        private static db_Property mvarInstance;
        private UserContext mvarUserContext;

        public db_Property(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Property(UserContext usrCTX, string databaseName)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(databaseName);
        }

        public override int addnewDataSet(DataSet ds)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_AddNew_Property");
            this._db.AddInParameter(storedProcCommand, "PK_PropertyID", DbType.Guid, "PK_PropertyID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Name", DbType.String, "Name", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Description", DbType.String, "Description", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Type", DbType.Int32, "Type", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DocTypeID", DbType.Guid, "FK_DocTypeID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_Folder_LookUpID", DbType.Guid, "FK_Folder_LookUpID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DocType_LookUpID", DbType.Guid, "FK_DocType_LookUpID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "LookUp_Value", DbType.Guid, "LookUp_Value", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 4);
            DbCommand updateCommand = null;
            DbCommand deleteCommand = null;
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds, ds.Tables[0].TableName, storedProcCommand, updateCommand, deleteCommand, transaction);
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
            int num;
            string commandText = string.Empty;
            commandText = "DELETE FROM T_PROPERTY WHERE 1=1 ";
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
            commandText = "DELETE FROM T_PROPERTY WHERE PK_PropertyID='" + ID + "' ";
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
                    commandText = "SELECT *";
                    commandText = (commandText + " FROM T_PROPERTY") + " WHERE PK_PROPERTYID='" + ID + "'";
                }
                else
                {
                    commandText = ("SELECT " + Query + " FROM T_PROPERTY") + " WHERE PK_PROPERTYID='" + ID + "'";
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
                    commandText = "SELECT " + Query + " FROM T_PROPERTY WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_PROPERTY WHERE 1=2";
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
            try
            {
                string commandText = string.Empty;
                if (string.IsNullOrEmpty(Query))
                {
                    commandText = "SELECT * FROM T_PROPERTY WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_PROPERTY WHERE 1=1 ";
                }
                if (!string.IsNullOrEmpty(Condition))
                {
                    commandText = commandText + Condition;
                }
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetLookUpValue(string PropertyID)
        {
            try
            {
                DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_GetLookup_Value");
                this._db.AddInParameter(storedProcCommand, "PropertyID", DbType.Guid, new Guid(PropertyID));
                return this._db.ExecuteDataSet(storedProcCommand);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataSet GetLookUpValue(string PropertyID, int DocStatus)
        {
            try
            {
                DbCommand storedProcCommand = this._db.GetStoredProcCommand("EXT_GetLookup_Value");
                this._db.AddInParameter(storedProcCommand, "PropertyID", DbType.Guid, new Guid(PropertyID));
                this._db.AddInParameter(storedProcCommand, "DocStatus", DbType.Int32, DocStatus);
                return this._db.ExecuteDataSet(storedProcCommand);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static db_Property Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Property(usrCTX);
            }
            return mvarInstance;
        }

        public int RemoveLookupValue(string propertyID)
        {
            int num;
            string commandText = string.Empty;
            commandText = "UPDATE T_PROPERTY SET LOOKUP_VALUE=NULL, FK_DOCTYPE_LOOKUPID=NULL WHERE LOOKUP_VALUE='" + propertyID + "'";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText);
                    transaction.Commit();
                    num = 0;
                }
                catch (Exception exception)
                {
                    num = -1;
                    transaction.Rollback();
                    throw new Exception(exception.Message);
                }
                connection.Close();
            }
            return num;
        }

        public override int saveDataSet(DataSet ds)
        {
            int parameterValue = 0;
            DbCommand insertCommand = null;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_Property");
            this._db.AddInParameter(storedProcCommand, "PK_PropertyID", DbType.Guid, "PK_PropertyID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Name", DbType.String, "Name", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Description", DbType.String, "Description", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Type", DbType.Int32, "Type", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_Folder_LookUpID", DbType.Guid, "FK_Folder_LookUpID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DocType_LookUpID", DbType.Guid, "FK_DocType_LookUpID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "LookUp_Value", DbType.Guid, "LookUp_Value", DataRowVersion.Current);
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
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch
                {
                    parameterValue = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return parameterValue;
        }

        public int SetLookUpProperty(string PropertyID, string LookUpFolderID, string LookUpDocTypeID, string LookUpValue)
        {
            try
            {
                string commandText = "UPDATE FROM T_PROPERTY SET FK_DocTypeID='" + LookUpDocTypeID + "' AND FK_Folder_LookUpID='" + LookUpFolderID + "' AND LookUp_Value='" + LookUpValue + "' WHERE PK_PropertyID='" + PropertyID + "'";
                this._db.ExecuteNonQuery(CommandType.Text, commandText);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
    }
}


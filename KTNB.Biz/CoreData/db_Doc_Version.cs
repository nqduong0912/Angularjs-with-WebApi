namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Doc_Version : ADBManager
    {
        private Database _db;
        private static db_Doc_Version mvarInstance;
        private UserContext mvarUserContext;

        public db_Doc_Version(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Doc_Version(UserContext usrCTX, string databaseName)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(databaseName);
        }

        public override int addnewDataSet(DataSet ds_Document)
        {
            int parameterValue;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_AddNew_DocVersion");
            this._db.AddInParameter(storedProcCommand, "PK_VersionID", DbType.Guid, "PK_VersionID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DocumentID", DbType.Guid, "FK_DocumentID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Comment", DbType.String, "Comment", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "VersionNumber", DbType.Int32, "VersionNumber", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "CreatedBy", DbType.Guid, "CreatedBy", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FileName", DbType.String, "FileName", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 4);
            DbCommand updateCommand = null;
            DbCommand deleteCommand = null;
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds_Document, ds_Document.Tables[0].TableName, storedProcCommand, updateCommand, deleteCommand, transaction);
                    parameterValue = (int)this._db.GetParameterValue(storedProcCommand, "ReturnValue");
                    if (parameterValue == 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        parameterValue = -1;
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
            commandText = "DELETE FROM T_DOC_VERSION WHERE 1=1 ";
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
            commandText = "DELETE FROM T_DOC_VERSION WHERE PK_VersionID='" + ID + "' ";
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
                    commandText = (commandText + " from T_DOC_VERSION") + " where PK_VersionID='" + ID + "'";
                }
                else
                {
                    commandText = ("select " + Query + " from T_DOC_VERSION") + " where PK_VersionID='" + ID + "'";
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
                    commandText = "SELECT " + Query + " FROM T_DOC_VERSION WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_DOC_VERSION WHERE 1=2";
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
                    commandText = "SELECT * FROM T_DOC_VERSION WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_DOC_VERSION WHERE 1=1 ";
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

        public static db_Doc_Version Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Doc_Version(usrCTX);
            }
            return mvarInstance;
        }

        public override int saveDataSet(DataSet ds_Document)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_DocVersion");
            this._db.AddInParameter(storedProcCommand, "PK_VersionID", DbType.Guid, "PK_VersionID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DocumentID", DbType.Guid, "FK_DocumentID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Comment", DbType.String, "Comment", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "VersionNumber", DbType.Int32, "VersionNumber", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "CreatedBy", DbType.String, "CreatedBy", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FileName", DbType.String, "FileName", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "CreatedDateTime", DbType.DateTime, "CreationDateTime", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 4);
            DbCommand insertCommand = null;
            DbCommand deleteCommand = null;
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds_Document, ds_Document.Tables[0].TableName, insertCommand, storedProcCommand, deleteCommand, transaction);
                    if (((int)this._db.GetParameterValue(storedProcCommand, "ReturnValue")) == 0)
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
                    transaction.Rollback();
                }
                connection.Close();
            }
            return 0;
        }
    }
}


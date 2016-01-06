namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Doc_Version_Body : ADBManager
    {
        private Database _db;
        private static db_Doc_Version_Body mvarInstance;
        private UserContext mvarUserContext;

        public db_Doc_Version_Body(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Doc_Version_Body(UserContext usrCTX, string databaseName)
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
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_AddNew_DocVersionBody");
            this._db.AddInParameter(storedProcCommand, "PK_DocversionbodyID", DbType.Guid, "PK_DocversionbodyID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DocversionID", DbType.Guid, "FK_DocversionID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DocumentID", DbType.Guid, "FK_DocumentID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NumberBody", DbType.Int32, "NumberBody", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FileName", DbType.String, "FileName", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FilePath", DbType.String, "FilePath", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Comment", DbType.String, "Description", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "isCheckOut", DbType.Int32, "isCheckOut", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DisplayFileName", DbType.String, "DisplayFileName", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ISLOCK", DbType.String, "ISLOCK", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "CheckingOutByUser", DbType.Guid, "CheckingOutByUser", DataRowVersion.Current);
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

        public int CheckIn(string DocumentID, string OldDocVersionBodyID, int NewVersion)
        {
            return -1;
        }

        public int CheckOut(string DocumentID, string DocVersionBodyID)
        {
            return -1;
        }

        public override int delete(string Condition)
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "DELETE FROM T_DOC_VERSION_BODY WHERE 1=1 ";
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
            commandText = "DELETE FROM T_DOC_VERSION_BODY WHERE PK_DocversionbodyID='" + ID + "' ";
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
                    commandText = (commandText + " from T_DOC_VERSION_BODY") + " where PK_DocversionbodyID='" + ID + "'";
                }
                else
                {
                    commandText = ("select " + Query + " from T_DOC_VERSION_BODY") + " where PK_DocversionbodyID='" + ID + "'";
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
                    commandText = "SELECT " + Query + " FROM T_DOC_VERSION_BODY WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_DOC_VERSION_BODY WHERE 1=2";
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
                    commandText = "SELECT * FROM T_DOC_VERSION_BODY WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_DOC_VERSION_BODY WHERE 1=1 ";
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

        public static db_Doc_Version_Body Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Doc_Version_Body(usrCTX);
            }
            return mvarInstance;
        }

        public DataRow IsCheckedOut(string DocumentID)
        {
            return null;
        }

        public override int saveDataSet(DataSet ds_Document)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_DocVersionBody");
            this._db.AddInParameter(storedProcCommand, "PK_DocversionbodyID", DbType.Guid, "PK_DocversionbodyID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DocversionID", DbType.Guid, "FK_DocversionID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DocumentID", DbType.Guid, "FK_DocumentID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NumberBody", DbType.Int32, "NumberBody", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FileName", DbType.String, "FileName", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FilePath", DbType.String, "FilePath", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Comment", DbType.String, "Comment", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "CreatedDate", DbType.DateTime, "CreatedDate", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "isCheckOut", DbType.Int32, "isCheckOut", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DisplayFileName", DbType.String, "DisplayFileName", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ISLOCK", DbType.String, "ISLOCK", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "CheckingOutByUser", DbType.String, "CheckingOutByUser", DataRowVersion.Current);
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

        public int UndoCheckOut(string DocVersionBodyID)
        {
            return -1;
        }
    }
}


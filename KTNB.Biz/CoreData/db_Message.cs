namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Message : ADBManager
    {
        private Database _db;
        private string mvarDatabaseName;
        private static db_Message mvarInstance;
        private UserContext mvarUserContext;

        public db_Message(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Message(UserContext usrCTX, string databaseName)
        {
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(this.mvarDatabaseName);
        }

        public override int addnewDataSet(DataSet ds)
        {
            int parameterValue = 0;
            DbCommand updateCommand = null;
            DbCommand deleteCommand = null;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.CREATE_MESSAGE");
            this._db.AddInParameter(storedProcCommand, "PK_MessageID", DbType.Guid, "PK_MessageID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_FromUserID", DbType.Guid, "FK_FromUserID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ToUserID", DbType.Guid, "FK_ToUserID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Content", DbType.String, "Content", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Title", DbType.String, "Title", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "LinkTo", DbType.String, "LinkTo", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "MessageType", DbType.String, "MessageType", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 4);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds, ds.Tables[0].TableName, storedProcCommand, updateCommand, deleteCommand, transaction);
                    parameterValue = (int)this._db.GetParameterValue(storedProcCommand, "ReturnValue");
                    transaction.Commit();
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
            commandText = "DELETE FROM T_MESSAGE WHERE 1=1 ";
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
            try
            {
                DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DELETE_MESSAGE");
                this._db.AddInParameter(storedProcCommand, "PK_MessageID", DbType.String, ID);
                this._db.ExecuteNonQuery(storedProcCommand);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public override DataSet getByID(string ID, string Query)
        {
            try
            {
                string commandText = string.Empty;
                if (string.IsNullOrEmpty(Query))
                {
                    commandText = "select *";
                    commandText = (commandText + " from T_MESSAGE") + " where PK_MessageID='" + ID + "'";
                }
                else
                {
                    commandText = ("select " + Query + " from T_MESSAGE") + " where PK_MessageID='" + ID + "'";
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
                    commandText = "SELECT " + Query + " FROM T_MESSAGE WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_MESSAGE WHERE 1=2";
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
                    commandText = "SELECT * FROM T_MESSAGE WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_MESSAGE WHERE 1=1 ";
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

        public static db_Message Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Message(usrCTX);
            }
            return mvarInstance;
        }

        public void PrepareMessage(Guid DocumentID, Guid FromUserID, string Content, string Title, string LinkTo, string MessageType)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.PREPARE_MESSAGE");
            this._db.AddInParameter(storedProcCommand, "DocumentID", DbType.Guid, DocumentID);
            this._db.AddInParameter(storedProcCommand, "FromUserID", DbType.Guid, FromUserID);
            this._db.AddInParameter(storedProcCommand, "Content", DbType.String, Content);
            this._db.AddInParameter(storedProcCommand, "Title", DbType.String, Title);
            this._db.AddInParameter(storedProcCommand, "Linkto", DbType.String, LinkTo);
            this._db.AddInParameter(storedProcCommand, "MessageType", DbType.String, MessageType);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.ExecuteNonQuery(storedProcCommand, transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
                connection.Close();
            }
        }

        public override int saveDataSet(DataSet ds)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.UPDATE_MESSAGE");
            this._db.AddInParameter(storedProcCommand, "PK_MessageID", DbType.Guid, "PK_MessageID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_FromUserID", DbType.Guid, "FK_FromUserID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ToUserID", DbType.Guid, "FK_ToUserID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Content", DbType.String, "Content", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Title", DbType.String, "Title", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "LinkTo", DbType.String, "LinkTo", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "MessageType", DbType.String, "MessageType", DataRowVersion.Current);
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
    }
}


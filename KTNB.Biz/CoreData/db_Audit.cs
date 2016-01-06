namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Audit : ADBManager
    {
        private Database _db;
        private static db_Audit mvarInstance;
        private UserContext mvarUserContext;

        public db_Audit(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Audit(UserContext usrCTX, string databaseName)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(databaseName);
        }

        public string AddAuditLog(string ProcessInstanceID, string ActivityInstanceID, string FK_ObjectID, byte ObjectType, byte OperationType, string AdditionalData1, string AdditionalData2)
        {
            string str = string.Empty;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_ADDLOG");
            this._db.AddInParameter(storedProcCommand, "FK_USERID", DbType.Guid, new Guid(this.mvarUserContext.UserID));
            if (!string.IsNullOrEmpty(ProcessInstanceID))
            {
                this._db.AddInParameter(storedProcCommand, "FK_PROCESSINSTANCEID", DbType.Guid, new Guid(ProcessInstanceID));
            }
            else
            {
                this._db.AddInParameter(storedProcCommand, "FK_PROCESSINSTANCEID", DbType.Guid, DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ActivityInstanceID))
            {
                this._db.AddInParameter(storedProcCommand, "FK_ACTIVITYINSTANCEID", DbType.Guid, new Guid(ActivityInstanceID));
            }
            else
            {
                this._db.AddInParameter(storedProcCommand, "FK_ACTIVITYINSTANCEID", DbType.Guid, DBNull.Value);
            }
            if (!string.IsNullOrEmpty(FK_ObjectID))
            {
                this._db.AddInParameter(storedProcCommand, "FK_OBJECTID", DbType.Guid, new Guid(FK_ObjectID));
            }
            else
            {
                this._db.AddInParameter(storedProcCommand, "FK_OBJECTID", DbType.Guid, DBNull.Value);
            }
            this._db.AddInParameter(storedProcCommand, "OBJECTTYPE", DbType.Int32, ObjectType);
            this._db.AddInParameter(storedProcCommand, "OPERATIONTYPE", DbType.Int32, OperationType);
            this._db.AddInParameter(storedProcCommand, "ADDITIONALDATA1", DbType.String, AdditionalData1);
            this._db.AddInParameter(storedProcCommand, "ADDITIONALDATA2", DbType.String, AdditionalData2);
            this._db.AddInParameter(storedProcCommand, "USERNAME", DbType.String, this.mvarUserContext.UserName);
            this._db.AddInParameter(storedProcCommand, "USERIP", DbType.String, this.mvarUserContext.UserIP);
            this._db.AddInParameter(storedProcCommand, "ROLENAME", DbType.String, ((Role)this.mvarUserContext.Roles[0]).RoleName);
            this._db.AddInParameter(storedProcCommand, "GROUPNAME", DbType.String, ((Group)this.mvarUserContext.Groups[0]).GroupName);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            using (DbConnection connection = this._db.CreateConnection())
            {
                try
                {
                    connection.Open();
                    this._db.ExecuteNonQuery(storedProcCommand);
                    return this._db.GetParameterValue(storedProcCommand, "RETURNVALUE").ToString();
                }
                catch (Exception exception)
                {
                    return exception.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
            return str;
        }

        public string AddAuditLog(string ProcessInstanceID, string ActivityInstanceID, string FK_ObjectID, byte ObjectType, byte OperationType, string AdditionalData1, string AdditionalData2, string UserID, string UserIP, string UserName, string RoleName, string GroupName)
        {
            string str = string.Empty;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_ADDLOG");
            this._db.AddInParameter(storedProcCommand, "FK_USERID", DbType.Guid, new Guid(UserID));
            if (!string.IsNullOrEmpty(ProcessInstanceID))
            {
                this._db.AddInParameter(storedProcCommand, "FK_PROCESSINSTANCEID", DbType.Guid, new Guid(ProcessInstanceID));
            }
            else
            {
                this._db.AddInParameter(storedProcCommand, "FK_PROCESSINSTANCEID", DbType.Guid, DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ActivityInstanceID))
            {
                this._db.AddInParameter(storedProcCommand, "FK_ACTIVITYINSTANCEID", DbType.Guid, new Guid(ActivityInstanceID));
            }
            else
            {
                this._db.AddInParameter(storedProcCommand, "FK_ACTIVITYINSTANCEID", DbType.Guid, DBNull.Value);
            }
            if (!string.IsNullOrEmpty(FK_ObjectID))
            {
                this._db.AddInParameter(storedProcCommand, "FK_OBJECTID", DbType.Guid, new Guid(FK_ObjectID));
            }
            else
            {
                this._db.AddInParameter(storedProcCommand, "FK_OBJECTID", DbType.Guid, DBNull.Value);
            }
            this._db.AddInParameter(storedProcCommand, "OBJECTTYPE", DbType.Int32, ObjectType);
            this._db.AddInParameter(storedProcCommand, "OPERATIONTYPE", DbType.Int32, OperationType);
            this._db.AddInParameter(storedProcCommand, "ADDITIONALDATA1", DbType.String, AdditionalData1);
            this._db.AddInParameter(storedProcCommand, "ADDITIONALDATA2", DbType.String, AdditionalData2);
            this._db.AddInParameter(storedProcCommand, "USERNAME", DbType.String, UserName);
            this._db.AddInParameter(storedProcCommand, "USERIP", DbType.String, UserIP);
            this._db.AddInParameter(storedProcCommand, "ROLENAME", DbType.String, RoleName);
            this._db.AddInParameter(storedProcCommand, "GROUPNAME", DbType.String, GroupName);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            using (DbConnection connection = this._db.CreateConnection())
            {
                try
                {
                    connection.Open();
                    this._db.ExecuteNonQuery(storedProcCommand);
                    return this._db.GetParameterValue(storedProcCommand, "RETURNVALUE").ToString();
                }
                catch (Exception exception)
                {
                    return exception.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
            return str;
        }

        public override int addnewDataSet(DataSet ds_Document)
        {
            int parameterValue;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_AddNew_Audit");
            this._db.AddInParameter(storedProcCommand, "PK_AuditID", DbType.Guid, "PK_AuditID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_UserID", DbType.Guid, "FK_UserID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ProcessInstanceID", DbType.Guid, "FK_ProcessInstanceID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ActivityInstanceID", DbType.Guid, "FK_ActivityInstanceID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ObjectID", DbType.Guid, "FK_ObjectID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ObjectType", DbType.Int32, "ObjectType", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "OperationType", DbType.Int32, "OperationType", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "OperationDateTime", DbType.DateTime, "OperationDateTime", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "AdditionalData1", DbType.String, "AdditionalData1", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "AdditionalData2", DbType.String, "AdditionalData2", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ProjectCode", DbType.String, "ProjectCode", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "UserName", DbType.String, "UserName", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "UserIP", DbType.String, "UserIP", DataRowVersion.Current);
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

        public override int delete(string Condition)
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "DELETE FROM T_AUDIT WHERE 1=1 ";
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
            commandText = "DELETE FROM T_AUDIT WHERE PK_AuditID='" + ID + "' ";
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
                    commandText = (commandText + " from T_AUDIT") + " where PK_AuditID='" + ID + "'";
                }
                else
                {
                    commandText = ("select " + Query + " from T_AUDIT") + " where PK_AuditID='" + ID + "'";
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
                    commandText = "SELECT " + Query + " FROM T_AUDIT WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_AUDIT WHERE 1=2";
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
            string commandText = string.Empty;
            if (string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT * FROM";
            }
            else
            {
                commandText = "SELECT " + Query + " FROM";
            }
            commandText = ((commandText + " (SELECT T_AUDIT.*,T_USER.NAME AS USERNAME,T_USER.FULLNAME AS USERFULLNAME, T_GROUP.NAME AS GROUPNAME, T_GROUP.DESCRIPTION AS DESCRIPTION") + " FROM T_AUDIT" + " LEFT JOIN T_USER ON T_AUDIT.FK_USERID=T_USER.PK_USERID") + " LEFT JOIN T_USER_IN_GROUP ON T_AUDIT.FK_USERID=T_USER_IN_GROUP.FK_USERID" + " LEFT JOIN T_GROUP ON T_USER_IN_GROUP.FK_GROUPID=T_GROUP.PK_GROUPID) AS TEMP_TABLE WHERE 1=1 ";
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + Condition;
            }
            try
            {
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getList_Audit(string Condition, string Query)
        {
            string commandText = string.Empty;
            if (string.IsNullOrEmpty(Query))
            {
                commandText = "select * from T_AUDIT where 1=1";
            }
            else
            {
                commandText = "select " + Query + " from T_AUDIT where 1=1";
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + Condition;
            }
            return this._db.ExecuteDataSet(CommandType.Text, commandText);
        }

        public static db_Audit Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Audit(usrCTX);
            }
            return mvarInstance;
        }

        public override int saveDataSet(DataSet ds_Document)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_Audit");
            this._db.AddInParameter(storedProcCommand, "PK_AuditID", DbType.Guid, "PK_AuditID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_UserID", DbType.Guid, "FK_UserID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ProcessInstanceID", DbType.Guid, "FK_ProcessInstanceID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ActivityInstanceID", DbType.Guid, "FK_ActivityInstanceID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ObjectID", DbType.Guid, "FK_ObjectID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ObjectType", DbType.Int32, "ObjectType", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ObjectType", DbType.Int32, "ObjectType", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "OperationDateTime", DbType.DateTime, "OperationDateTime", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "AdditionalData1", DbType.String, "AdditionalData1", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "AdditionalData2", DbType.String, "AdditionalData2", DataRowVersion.Current);
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


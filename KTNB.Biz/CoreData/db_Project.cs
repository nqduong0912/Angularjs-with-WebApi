namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Project : ADBManager
    {
        private Database _db;
        private static db_Project mvarInstance;
        private UserContext mvarUserContext;

        public db_Project(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Project(UserContext usrCTX, string databaseName)
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
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DBO.DMS_AddNew_Project");
            this._db.AddInParameter(storedProcCommand, "PK_PROJECTID", DbType.Guid, "PK_PROJECTID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "CODE", DbType.String, "CODE", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NAME", DbType.String, "NAME", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            DbCommand updateCommand = null;
            DbCommand deleteCommand = null;
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds, ds.Tables[0].TableName, storedProcCommand, updateCommand, deleteCommand, transaction);
                    parameterValue = (int)this._db.GetParameterValue(storedProcCommand, "RETURNVALUE");
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
            commandText = "DELETE FROM T_PROJECT WHERE 1=1 ";
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

        public int deleteApplicationGroup(string ApplicationID, string RoleID, string GroupID)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Delete_AppGroup");
            this._db.AddInParameter(storedProcCommand, "FK_ApplicationID", DbType.String, ApplicationID);
            this._db.AddInParameter(storedProcCommand, "FK_RoleID", DbType.String, RoleID);
            this._db.AddInParameter(storedProcCommand, "FK_GroupID", DbType.String, GroupID);
            this._db.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 4);
            DbCommand command = null;
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.ExecuteNonQuery(command, transaction);
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

        public override int deleteByID(string ID)
        {
            string storedProcedureName = "DMS_Delete_Application";
            DbCommand storedProcCommand = this._db.GetStoredProcCommand(storedProcedureName);
            this._db.AddInParameter(storedProcCommand, "PK_PROJECTID", DbType.String, ID);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            this._db.ExecuteNonQuery(storedProcCommand);
            return (int)this._db.GetParameterValue(storedProcCommand, "RETURNVALUE");
        }

        public DataSet getApplicationByRole(string RoleID, string Condition)
        {
            string str3;
            string str = " T2.PK_PROJECTID, T2.NAME, T2.DESCRIPTION, T2.ORDERINDEX, T2.LEFTRIGHTPOSITION, T2.URL, T2.IMAGENAME";
            if (!string.IsNullOrEmpty(Condition))
            {
                str3 = " AND FK_ROLEID='" + RoleID + "'" + Condition + " ORDER BY ORDERINDEX";
            }
            else
            {
                str3 = " AND FK_ROLEID='" + RoleID + "' ORDER BY ORDERINDEX";
            }
            string commandText = ("SELECT" + str) + " FROM T_PROJECT_ROLE T1 LEFT JOIN T_PROJECT T2 ON T1.FK_APPLICATIONID=T2.PK_PROJECTID WHERE 1=1" + str3;
            try
            {
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public override DataSet getByID(string ID, string Query)
        {
            try
            {
                string commandText = string.Empty;
                if (string.IsNullOrEmpty(Query))
                {
                    commandText = "SELECT *";
                    commandText = (commandText + " FROM T_PROJECT") + " WHERE PK_PROJECTID='" + ID + "'";
                }
                else
                {
                    commandText = ("SELECT " + Query + " FROM T_PROJECT") + " WHERE PK_PROJECTID='" + ID + "'";
                }
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getComponentByApplication(string ApplicationID, string RoleID)
        {
            string str = " T2.PK_COMPONENTID, T2.NAME AS COMPONENTNAME, T2.DESCRIPTION, T2.ORDERINDEX, T2.FK_APPLICATIONID,T3.NAME AS APPLICATIONNAME, T2.URL, T2.ONNEWWINDOW, T2.ICON";
            string str3 = " AND T1.FK_ROLEID='" + RoleID + "' AND T2.FK_APPLICATIONID='" + ApplicationID + "' ORDER BY T2.ORDERINDEX";
            string commandText = (("SELECT" + str + " FROM T_COMPONENT_ROLE T1") + " LEFT JOIN T_COMPONENT T2 ON T1.FK_COMPONENTID=T2.PK_COMPONENTID" + " LEFT JOIN T_PROJECT T3 ON T2.FK_APPLICATIONID=T3.PK_PROJECTID") + " WHERE 1=1" + str3;
            try
            {
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getComponentByApplicationNGroup(string ApplicationID, string RoleID, string GroupID)
        {
            string str = " T1.FK_GROUPID, T2.PK_COMPONENTID, T2.NAME AS COMPONENTNAME, T2.DESCRIPTION, T2.ORDERINDEX, T2.FK_APPLICATIONID,T3.NAME AS APPLICATIONNAME, T2.URL, T2.ONNEWWINDOW";
            string str3 = (" AND T1.FK_ROLEID='" + RoleID + "' AND T2.FK_APPLICATIONID='" + ApplicationID + "' AND CHARINDEX('" + GroupID + "',FK_GROUPID,1)>0") + " ORDER BY T2.ORDERINDEX";
            string commandText = (("SELECT" + str + " FROM T_COMPONENT_ROLE T1") + " LEFT JOIN T_COMPONENT T2 ON T1.FK_COMPONENTID=T2.PK_COMPONENTID" + " LEFT JOIN T_PROJECT T3 ON T2.FK_APPLICATIONID=T3.PK_PROJECTID") + " WHERE 1=1" + str3;
            try
            {
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
                    commandText = "SELECT " + Query + " FROM     WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_PROJECT WHERE 1=2";
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
                    commandText = "SELECT * FROM T_PROJECT WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_PROJECT WHERE 1=1 ";
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

        public IDataReader GetProjectInfo(string Condition, string Query)
        {
            string commandText = string.Empty;
            if (string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT * FROM T_PROJECT WHERE 1=1";
            }
            else
            {
                commandText = "SELECT " + Query + " FROM T_PROJECT WHERE 1=1";
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + Condition;
            }
            return this._db.ExecuteReader(CommandType.Text, commandText);
        }

        public int GetProjectStatus(string ProjectID)
        {
            string commandText = "SELECT ISNULL([STATUS],2) AS [STATUS] FROM T_PROJECT WHERE [PK_PROJECTID]='" + ProjectID + "'";
            return Convert.ToInt16(this._db.ExecuteScalar(CommandType.Text, commandText));
        }

        public DataSet getRoleOnApplication(string ID)
        {
            try
            {
                string commandText = "SELECT FK_ROLEID,NAME FROM T_PROJECT_ROLE T1 LEFT JOIN T_ROLE T2 ON T1.FK_ROLEID=T2.PK_ROLEID WHERE FK_APPLICATIONID='" + ID + "' ORDER BY NAME";
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public static db_Project Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Project(usrCTX);
            }
            return mvarInstance;
        }

        public int MapApplicationGroup(string ApplicationID, string RoleID, string GroupID)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_AddNew_AppGroup");
            this._db.AddInParameter(storedProcCommand, "FK_ApplicationID", DbType.String, ApplicationID);
            this._db.AddInParameter(storedProcCommand, "FK_RoleID", DbType.String, RoleID);
            this._db.AddInParameter(storedProcCommand, "FK_GroupID", DbType.String, GroupID);
            this._db.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 4);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.ExecuteNonQuery(storedProcCommand, transaction);
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

        public override int saveDataSet(DataSet ds)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_Application");
            storedProcCommand.CommandTimeout = 0;
            this._db.AddInParameter(storedProcCommand, "PK_PROJECTID", DbType.Guid, "PK_PROJECTID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NAME", DbType.String, "NAME", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DESCRIPTION", DbType.String, "DESCRIPTION", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ORDERINDEX", DbType.Int32, "ORDERINDEX", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "LEFTRIGHTPOSITION", DbType.Int32, "LEFTRIGHTPOSITION", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "IMAGENAME", DbType.String, "IMAGENAME", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "URL", DbType.String, "URL", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ONNEWWINDOW", DbType.Int32, "ONNEWWINDOW", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_PARENTAPPLICATIONID", DbType.Guid, "FK_PARENTAPPLICATIONID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ROLEID", DbType.String, "ROLEID", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            DbCommand deleteCommand = null;
            DbCommand insertCommand = null;
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds, ds.Tables[0].TableName, insertCommand, storedProcCommand, deleteCommand, transaction);
                    parameterValue = (int)this._db.GetParameterValue(storedProcCommand, "RETURNVALUE");
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


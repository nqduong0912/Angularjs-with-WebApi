namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Component : ADBManager
    {
        private Database _db;
        private static db_Component mvarInstance;
        private UserContext mvarUserContext;

        public db_Component(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Component(UserContext usrCTX, string databaseName)
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
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_AddNew_Component");
            this._db.AddInParameter(storedProcCommand, "PK_COMPONENTID", DbType.Guid, "PK_COMPONENTID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NAME", DbType.String, "NAME", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DESCRIPTION", DbType.String, "DESCRIPTION", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ORDERINDEX", DbType.Int32, "ORDERINDEX", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "COMPONENTTYPE", DbType.Int32, "COMPONENTTYPE", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_APPLICATIONID", DbType.Guid, "FK_APPLICATIONID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "URL", DbType.String, "URL", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ONNEWWINDOW", DbType.Int32, "ONNEWWINDOW", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ROLEID", DbType.String, "ROLEID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ICON", DbType.String, "ICON", DataRowVersion.Current);
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

        public int addRoleToComponent(string componentid, string roleid)
        {
            int num;
            string commandText = "INSERT INTO T_COMPONENT_ROLE (FK_COMPONENTID,FK_ROLEID) VALUES('" + componentid + "','" + roleid + "')";
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

        public int appendGroup(string FK_ComponentID, string FK_RoleID, string GroupID)
        {
            string commandText = "UPDATE T_COMPONENT_ROLE SET FK_GROUPID=ISNULL(FK_GROUPID,'') + '," + GroupID + "' WHERE FK_ComponentID='" + FK_ComponentID + "' AND FK_RoleId='" + FK_RoleID + "'";
            return this._db.ExecuteNonQuery(CommandType.Text, commandText);
        }

        public override int delete(string Condition)
        {
            int num;
            string commandText = string.Empty;
            commandText = "DELETE FROM T_COMPONENT WHERE 1=1 ";
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
            string storedProcedureName = "DMS_Delete_Component";
            DbCommand storedProcCommand = this._db.GetStoredProcCommand(storedProcedureName);
            this._db.AddInParameter(storedProcCommand, "PK_COMPONENTID", DbType.String, ID);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            this._db.ExecuteNonQuery(storedProcCommand);
            return (int)this._db.GetParameterValue(storedProcCommand, "RETURNVALUE");
        }

        public int deleteComponentGroup(string ComponentID, string RoleID, string GroupID)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Delete_CompGroup");
            this._db.AddInParameter(storedProcCommand, "FK_ComponentID", DbType.String, ComponentID);
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

        public int deleteRoleFromComponent(string componentid, string roleid)
        {
            int num;
            string commandText = "DELETE FROM T_COMPONENT_ROLE WHERE FK_COMPONENTID='" + componentid + "' AND FK_ROLEID='" + roleid + "'";
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
                    commandText = (commandText + " FROM T_COMPONENT") + " WHERE PK_COMPONENTID='" + ID + "'";
                }
                else
                {
                    commandText = ("SELECT " + Query + " FROM T_COMPONENT") + " WHERE PK_COMPONENTID='" + ID + "'";
                }
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getComponentNGroupMappedWithRole(string roleID)
        {
            try
            {
                string commandText = "SELECT * FROM T_COMPONENT_GROUP WHERE FK_ROLEID='" + roleID + "'";
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
                    commandText = "SELECT " + Query + " FROM T_COMPONENT WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_COMPONENT WHERE 1=2";
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
                    commandText = "SELECT * FROM T_COMPONENT WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_COMPONENT WHERE 1=1 ";
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

        public DataSet getListComponentRole(string Condition, string Query)
        {
            try
            {
                string commandText = string.Empty;
                if (string.IsNullOrEmpty(Query))
                {
                    commandText = "SELECT * FROM T_COMPONENT_ROLE WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_COMPONENT_ROLE WHERE 1=1 ";
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

        public DataSet getRoleOnComponent(string ID)
        {
            try
            {
                string commandText = "SELECT T_COMPONENT_ROLE.FK_ROLEID,T_ROLE.NAME";
                commandText = (commandText + " FROM T_COMPONENT_ROLE LEFT JOIN T_ROLE ON T_COMPONENT_ROLE.FK_ROLEID=T_ROLE.PK_ROLEID") + " WHERE T_COMPONENT_ROLE.FK_COMPONENTID='" + ID + "'";
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public static db_Component Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Component(usrCTX);
            }
            return mvarInstance;
        }

        public bool isRoleAssignedOnComponent(string componentid, string roleid)
        {
            string commandText = "SELECT COUNT(FK_ROLEID) AS VAL FROM T_COMPONENT_ROLE WHERE FK_COMPONENTID='" + componentid + "' AND FK_ROLEID='" + roleid + "'";
            if (((int)this._db.ExecuteScalar(CommandType.Text, commandText)) == 0)
            {
                return false;
            }
            return true;
        }

        public int mapComponentGroup(string ComponentID, string RoleID, string GroupID)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_AddNew_CompGroup");
            this._db.AddInParameter(storedProcCommand, "FK_ComponentID", DbType.String, ComponentID);
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
            DbCommand deleteCommand = null;
            DbCommand insertCommand = null;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_Component");
            this._db.AddInParameter(storedProcCommand, "PK_COMPONENTID", DbType.Guid, "PK_COMPONENTID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NAME", DbType.String, "NAME", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DESCRIPTION", DbType.String, "DESCRIPTION", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ORDERINDEX", DbType.Int32, "ORDERINDEX", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "COMPONENTTYPE", DbType.Int32, "COMPONENTTYPE", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_APPLICATIONID", DbType.Guid, "FK_APPLICATIONID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "URL", DbType.String, "URL", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ONNEWWINDOW", DbType.Int32, "ONNEWWINDOW", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ROLEID", DbType.String, "ROLEID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ICON", DbType.String, "ICON", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
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

        public int updateGroup(string FK_ComponentID, string FK_RoleID, string GroupID)
        {
            string commandText = "UPDATE T_COMPONENT_ROLE SET FK_GROUPID='" + GroupID + "' WHERE FK_ComponentID='" + FK_ComponentID + "' AND FK_RoleId='" + FK_RoleID + "'";
            return this._db.ExecuteNonQuery(CommandType.Text, commandText);
        }
    }
}


namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Permission : ADBManager
    {
        private Database _db;
        private static db_Permission mvarInstance;
        private UserContext mvarUserContext;

        public db_Permission(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Permission(UserContext usrCTX, string databaseName)
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
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_AddNew_Permission");
            this._db.AddInParameter(storedProcCommand, "FK_AppliedOnID", DbType.Guid, "TypeOfApplicant", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "TypeOfApplicant", DbType.Int32, "TypeOfApplicant", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_AppliedOnObjectID", DbType.Guid, "FK_AppliedOnObjectID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "TypeOfObject", DbType.Int32, "TypeOfObject", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_AdditionalObjectID", DbType.Guid, "FK_AdditionalObjectID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "TypeOfAdditionalObject", DbType.Int32, "TypeOfAdditionalObject", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ActivationDateTime", DbType.DateTime, "ActivationDateTime", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "GrantedRight", DbType.Int32, "GrantedRight", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DeniedRight", DbType.Int32, "DeniedRight", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ParentFolderIDOnPermission", DbType.Guid, "FK_ParentFolderIDOnPermission", DataRowVersion.Current);
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
                    transaction.Commit();
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

        public int changePermissionOnObject(string UserID, byte UserType, string ObjectID, byte TypeOfObject, byte GrantedRight)
        {
            string str;
            if (GrantedRight > 0)
            {
                str = "UPDATE T_PERMISSION";
                str = ((((str + " SET GRANTEDRIGHT=" + GrantedRight) + " WHERE FK_APPLIEDONID='" + UserID + "'") + " AND TYPEOFAPPLICANT=" + UserType) + " AND FK_APPLIEDONOBJECTID='" + ObjectID + "'") + " AND TYPEOFOBJECT=" + TypeOfObject;
            }
            else
            {
                str = "DELETE FROM T_PERMISSION";
                str = (((str + " WHERE FK_APPLIEDONID='" + UserID + "'") + " AND TYPEOFAPPLICANT=" + UserType) + " AND FK_APPLIEDONOBJECTID='" + ObjectID + "'") + " AND TYPEOFOBJECT=" + TypeOfObject;
            }
            try
            {
                this._db.ExecuteNonQuery(CommandType.Text, str);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public override int delete(string Condition)
        {
            int num;
            string commandText = string.Empty;
            commandText = "DELETE FROM T_PERMISSION WHERE 1=1 ";
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
            return -1;
        }

        public override DataSet getByID(string ID, string Query)
        {
            return null;
        }

        public override DataSet getEmpty(string Query)
        {
            string commandText = string.Empty;
            if (!string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT " + Query + " FROM T_PERMISSION WHERE 1=2";
            }
            else
            {
                commandText = "SELECT * FROM T_PERMISSION WHERE 1=2";
            }
            return this._db.ExecuteDataSet(CommandType.Text, commandText);
        }

        public override DataSet getList(string Condition, string Query)
        {
            string commandText = string.Empty;
            if (string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT * FROM T_PERMISSION WHERE 1=1 ";
            }
            else
            {
                commandText = "SELECT " + Query + " FROM T_PERMISSION WHERE 1=1 ";
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + Condition;
            }
            return this._db.ExecuteDataSet(CommandType.Text, commandText);
        }

        public int getPermissionOnObject(string ObjectID, string UserID)
        {
            string commandText = "SELECT GrantedRight FROM  T_PERMISSION WHERE FK_AppliedOnID='" + UserID + "' AND FK_AppliedOnObjectID='" + ObjectID + "'";
            try
            {
                return (int)this._db.ExecuteScalar(CommandType.Text, commandText);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int getPermissionOnObject(string ObjectID, string ApplicantID, byte TypeOfApplicant)
        {
            string commandText = "SELECT GrantedRight FROM  T_PERMISSION WHERE FK_AppliedOnID='" + ApplicantID + "' AND FK_AppliedOnObjectID='" + ObjectID + "' AND TypeOfApplicant=" + TypeOfApplicant.ToString();
            try
            {
                return (int)this._db.ExecuteScalar(CommandType.Text, commandText);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int grantPermission(string AppliedOnID, int TypeOfApplicant, string AppliedOnObjectID, int TypeOfObject, string AdditionalObjectID, int TypeOfAdditionalObject, int GrantedRight, int DeniedRight, string FolderID)
        {
            try
            {
                string commandText = "INSERT INTO T_PERMISSION(FK_AppliedOnID,TypeOfApplicant,FK_AppliedOnObjectID,TypeOfObject,FK_AdditionalObjectID,TypeOfAdditionalObject,GrantedRight,DeniedRight,FK_ParentFolderIDOnPermission) VALUE ";
                object obj2 = commandText;
                commandText = string.Concat(new object[] { 
                    obj2, "('", AppliedOnID, "',", TypeOfApplicant, ",'", AppliedOnObjectID, "',", TypeOfObject, ",'", AdditionalObjectID, "',", TypeOfAdditionalObject, ",", AdditionalObjectID, ",", 
                    GrantedRight, ",", DeniedRight, ",'", FolderID, "')"
                 });
                this._db.ExecuteNonQuery(CommandType.Text, commandText);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public static db_Permission Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Permission(usrCTX);
            }
            return mvarInstance;
        }

        public override int saveDataSet(DataSet ds)
        {
            int parameterValue = 1;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_Permission");
            this._db.AddInParameter(storedProcCommand, "FK_AppliedOnID", DbType.Guid, "TypeOfApplicant", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "TypeOfApplicant", DbType.Int32, "TypeOfApplicant", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_AppliedOnObjectID", DbType.Guid, "FK_AppliedOnObjectID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "TypeOfObject", DbType.Int32, "TypeOfObject", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_AdditionalObjectID", DbType.Guid, "FK_AdditionalObjectID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "TypeOfAdditionalObject", DbType.Int32, "TypeOfAdditionalObject", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ActivationDateTime", DbType.DateTime, "ActivationDateTime", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "GrantedRight", DbType.Int32, "GrantedRight", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DeniedRight", DbType.Int32, "DeniedRight", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ParentFolderIDOnPermission", DbType.Guid, "FK_ParentFolderIDOnPermission", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "ReturnValue", DbType.Int32, 2);
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
                catch
                {
                    parameterValue = 0;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return parameterValue;
        }

        public int updatePermissionOnObject(string UserID, byte UserType, string ObjectID, byte TypeOfObject, byte GrantedRight, string FK_ParentFolderIDOnPermission, string CreatedBy, string FinishedDate, byte Status)
        {
            string storedProcedureName = "DMS_AddNew_Permission";
            DbCommand storedProcCommand = this._db.GetStoredProcCommand(storedProcedureName);
            this._db.AddInParameter(storedProcCommand, "FK_APPLIEDONID", DbType.Guid, new Guid(UserID));
            this._db.AddInParameter(storedProcCommand, "TYPEOFAPPLICANT", DbType.Byte, UserType);
            this._db.AddInParameter(storedProcCommand, "FK_APPLIEDONOBJECTID", DbType.Guid, new Guid(ObjectID));
            this._db.AddInParameter(storedProcCommand, "TYPEOFOBJECT", DbType.Byte, TypeOfObject);
            this._db.AddInParameter(storedProcCommand, "GRANTEDRIGHT", DbType.Byte, GrantedRight);
            this._db.AddInParameter(storedProcCommand, "FK_PARENTFOLDERIDONPERMISSION", DbType.String, FK_ParentFolderIDOnPermission);
            this._db.AddInParameter(storedProcCommand, "CREATEDBY", DbType.String, CreatedBy);
            this._db.AddInParameter(storedProcCommand, "FINISHEDDATE", DbType.String, FinishedDate);
            this._db.AddInParameter(storedProcCommand, "STATUS", DbType.Byte, Status);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            this._db.ExecuteNonQuery(storedProcCommand);
            return (int)this._db.GetParameterValue(storedProcCommand, "RETURNVALUE");
        }
    }
}


namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_DocSpace : ADBManager
    {
        private Database _db;
        private static db_DocSpace mvarInstance;
        private UserContext mvarUserContext;

        public db_DocSpace(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_DocSpace(UserContext usrCTX, string databaseName)
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
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_AddNew_DocSpace");
            this._db.AddInParameter(storedProcCommand, "PK_DOCSPACEID", DbType.Guid, "PK_DOCSPACEID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NAME", DbType.String, "NAME", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DESCRIPTION", DbType.String, "DESCRIPTION", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ORDERNUMBER", DbType.Int32, "ORDERNUMBER", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ARCHIVEDPATH", DbType.String, "ARCHIVEDPATH", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "AUTOCREATEFOLDER", DbType.Int32, "AUTOCREATEFOLDER", DataRowVersion.Current);
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
            commandText = "DELETE FROM T_DOCSPACE_DOCTYPE WHERE 1=1 ";
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
            commandText = "DELETE FROM T_DOCSPACE_DOCTYPE WHERE FK_DOCSPACEID='" + ID + "' ";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    num = this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText);
                    commandText = "DELETE FROM T_DOCSPACE WHERE PK_DOCSPACEID='" + ID + "'";
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
                    commandText = "select *";
                    commandText = (commandText + " from T_DOCSPACE T") + " where PK_DocspaceID='" + ID + "'";
                }
                else
                {
                    commandText = ("select " + Query + " from T_DOCSPACE T") + " where PK_DocspaceID='" + ID + "'";
                }
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public DataSet getDocTypeList(string DocSpaceID)
        {
            try
            {
                string commandText = "SELECT T1.*, T2.NAME AS DOCTYPENAME FROM T_DOCSPACE_DOCTYPE AS T1 LEFT JOIN T_DOCUMENT_TYPE AS T2 ON T1.FK_DOCTYPEID = T2.PK_DOCUMENTTYPEID WHERE FK_DocSpaceID ='" + DocSpaceID + "'";
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
                    commandText = "SELECT " + Query + " FROM T_DOCSPACE WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_DOCSPACE WHERE 1=2";
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
                    commandText = "SELECT * FROM T_DOCSPACE WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_DOCSPACE WHERE 1=1 ";
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

        public static db_DocSpace Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_DocSpace(usrCTX);
            }
            return mvarInstance;
        }

        public override int saveDataSet(DataSet ds)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_DocSpace");
            this._db.AddInParameter(storedProcCommand, "PK_DOCSPACEID", DbType.Guid, "PK_DOCSPACEID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NAME", DbType.String, "NAME", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DESCRIPTION", DbType.String, "DESCRIPTION", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ORDERNUMBER", DbType.Int32, "ORDERNUMBER", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ARCHIVEDPATH", DbType.String, "ARCHIVEDPATH", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "AUTOCREATEFOLDER", DbType.Int32, "AUTOCREATEFOLDER", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            DbCommand insertCommand = null;
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


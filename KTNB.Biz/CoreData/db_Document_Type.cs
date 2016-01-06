namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Document_Type : ADBManager
    {
        private Database _db;
        private static db_Document_Type mvarInstance;
        private UserContext mvarUserContext;

        public db_Document_Type(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Document_Type(UserContext usrCTX, string databaseName)
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
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_AddNew_DocumentType");
            this._db.AddInParameter(storedProcCommand, "PK_DocumentTypeID", DbType.Guid, "PK_DocumentTypeID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Name", DbType.String, "Name", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Description", DbType.String, "Description", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ParentDocumentTypeID", DbType.Guid, "FK_ParentDocumentTypeID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "IsContainer", DbType.Int32, "IsContainer", DataRowVersion.Current);
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
            commandText = "DELETE FROM T_DOCUMENT_TYPE WHERE 1=1 ";
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

        private int Delete_DocType_DocSpace(string DocumentTypeID)
        {
            int num;
            string commandText = string.Empty;
            commandText = "DELETE FROM T_DOCSPACE_DOCTYPE WHERE FK_DOCTYPEID='" + DocumentTypeID + "'";
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
            commandText = "DELETE FROM T_DOCUMENT_TYPE WHERE PK_DocumentTypeID='" + ID + "' ";
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
                    this.Delete_DocType_DocSpace(ID);
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
                    commandText = "SELECT * FROM T_DOCUMENT_TYPE WHERE PK_DOCUMENTTYPEID='" + ID + "'";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_DOCUMENT_TYPE WHERE PK_DOCUMENTTYPEID='" + ID + "'";
                }
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataSet GetDocSpaceList(string DocTypeID)
        {
            try
            {
                string commandText = "SELECT FK_DocSpaceID, FK_DocTypeID, FK_ComponentID FROM T_DOCSPACE_DOCTYPE WHERE (FK_DocTypeID = '" + DocTypeID + "')";
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetDocTypeByDocSpace(string DocSpaceID)
        {
            string commandText = "SELECT FK_DOCTYPEID FROM T_DOCSPACE_DOCTYPE WHERE FK_DOCSPACEID='" + DocSpaceID + "'";
            return this._db.ExecuteDataSet(CommandType.Text, commandText);
        }

        public override DataSet getEmpty(string Query)
        {
            try
            {
                string commandText = string.Empty;
                if (!string.IsNullOrEmpty(Query))
                {
                    commandText = "SELECT " + Query + " FROM T_DOCUMENT_TYPE WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_DOCUMENT_TYPE WHERE 1=2";
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
            DataSet set2;
            try
            {
                string commandText = string.Empty;
                if (string.IsNullOrEmpty(Query))
                {
                    commandText = "SELECT * FROM T_DOCUMENT_TYPE WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_DOCUMENT_TYPE WHERE 1=1 ";
                }
                if (!string.IsNullOrEmpty(Condition))
                {
                    commandText = commandText + Condition;
                }
                set2 = this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return set2;
        }

        public DataSet GetProperyList(string DocTypeID)
        {
            try
            {
                string commandText = "SELECT * FROM T_PROPERTY WHERE FK_DocTypeID ='" + DocTypeID + "'";
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public static db_Document_Type Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Document_Type(usrCTX);
            }
            return mvarInstance;
        }

        public bool IsPropertyTransformed2Table(string PropertyID, string DocumentTypeID)
        {
            string str = PropertyID.Replace("-", "_");
            string str2 = DocumentTypeID.Replace("-", "_");
            string commandText = "SELECT COUNT(1) AS [COUNT] FROM SYS.COLUMNS WHERE [NAME]=N'" + str + "' AND [OBJECT_ID]=OBJECT_ID(N'" + str2 + "')";
            if (((int)this._db.ExecuteScalar(CommandType.Text, commandText)) == 0)
            {
                return false;
            }
            return true;
        }

        public int MapDocTypeDocSpace(string DocTypeID, string DocSpaceID, string ComponentID)
        {
            try
            {
                string commandText = "DELETE FROM T_DOCSPACE_DOCTYPE WHERE FK_DOCSPACEID='" + DocSpaceID + "' AND FK_DOCTYPEID='" + DocTypeID + "'";
                string str2 = "INSERT INTO T_DOCSPACE_DOCTYPE(FK_DOCSPACEID,FK_DOCTYPEID,FK_COMPONENTID) VALUES('" + DocSpaceID + "','" + DocTypeID + "','" + ComponentID + "')";
                this._db.ExecuteNonQuery(CommandType.Text, commandText);
                this._db.ExecuteNonQuery(CommandType.Text, str2);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int RemoveDocSpace(string DocSpaceID, string DocTypeID)
        {
            try
            {
                string str;
                if (!string.IsNullOrEmpty(DocTypeID))
                {
                    str = "DELETE FROM T_DOCSPACE_DOCTYPE WHERE FK_DocSpaceID = '" + DocSpaceID + "' AND FK_DocTypeID='" + DocTypeID + "'";
                }
                else
                {
                    str = "DELETE FROM T_DOCSPACE_DOCTYPE WHERE FK_DocSpaceID = '" + DocSpaceID + "'";
                }
                this._db.ExecuteNonQuery(CommandType.Text, str);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public override int saveDataSet(DataSet ds_Document)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_DocumentType");
            this._db.AddInParameter(storedProcCommand, "PK_DocumentTypeID", DbType.Guid, "PK_DocumentTypeID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Name", DbType.String, "Name", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Description", DbType.String, "Description", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_ParentDocumentTypeID", DbType.Guid, "FK_ParentDocumentTypeID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "IsContainer", DbType.Int32, "IsContainer", DataRowVersion.Current);
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

        public DataRow TransformDocType2Table(string DocumentTypeID)
        {
            DataRow row = null;
            using (DbConnection connection = this._db.CreateConnection())
            {
                DbCommand storedProcCommand = this._db.GetStoredProcCommand("[dbo].[Transform_Doctype_Table]");
                this._db.AddInParameter(storedProcCommand, "DocumentTypeID", DbType.String, DocumentTypeID);
                connection.Open();
                try
                {
                    try
                    {
                        row = this._db.ExecuteDataSet(storedProcCommand).Tables[0].Rows[0];
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    return row;
                }
                finally
                {
                    storedProcCommand.Dispose();
                }
            }
            return row;
        }

        public DataRow TransformProperty2Table(string PropertyID, string DocumentTypeID)
        {
            DataRow row = null;
            using (DbConnection connection = this._db.CreateConnection())
            {
                DbCommand storedProcCommand = this._db.GetStoredProcCommand("[dbo].[Transform_Property_Table]");
                this._db.AddInParameter(storedProcCommand, "PROPERTYID", DbType.String, PropertyID);
                this._db.AddInParameter(storedProcCommand, "DOCUMENTTYPEID", DbType.String, DocumentTypeID);
                connection.Open();
                try
                {
                    try
                    {
                        row = this._db.ExecuteDataSet(storedProcCommand).Tables[0].Rows[0];
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    return row;
                }
                finally
                {
                    storedProcCommand.Dispose();
                }
            }
            return row;
        }

        public int UnMapDocTypeDocSpace(string DocTypeID, string DocSpaceID)
        {
            string commandText = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(DocTypeID))
                {
                    commandText = "DELETE FROM T_DOCSPACE_DOCTYPE WHERE FK_DOCSPACEID='" + DocSpaceID + "'";
                }
                else if (string.IsNullOrEmpty(DocSpaceID))
                {
                    commandText = "DELETE FROM T_DOCSPACE_DOCTYPE WHERE FK_DOCTYPEID='" + DocTypeID + "'";
                }
                else
                {
                    commandText = "DELETE FROM T_DOCSPACE_DOCTYPE WHERE FK_DOCSPACEID = '" + DocSpaceID + "' AND FK_DOCTYPEID='" + DocTypeID + "'";
                }
                this._db.ExecuteNonQuery(CommandType.Text, commandText);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int UpdateDocSpace(string DocSpaceID, string DocTypeID)
        {
            try
            {
                string commandText = "UPDATE T_DOCSPACE_DOCTYPE SET FK_DocSpaceID ='" + DocSpaceID + "' WHERE FK_DocTypeID ='" + DocTypeID + "'";
                this._db.ExecuteNonQuery(CommandType.Text, commandText);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int VerifyDocTypes()
        {
            try
            {
                DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.CORE_REFRESHDOCTYPETABLES");
                this._db.ExecuteNonQuery(storedProcCommand);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int VerifyDocTypes(string DocumentTypeID)
        {
            try
            {
                DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.CORE_REFRESHDOCTYPETABLE");
                this._db.AddInParameter(storedProcCommand, "sDocTypeID", DbType.String, DocumentTypeID);
                this._db.ExecuteNonQuery(storedProcCommand);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}


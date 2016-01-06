namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CORE.HELPERS;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Document : ADBManager
    {
        private Database _db;
        private string _errormessage;
        private static db_Document mvarInstance;
        private UserContext mvarUserContext;

        public db_Document(UserContext usrCTX)
        {
            this._errormessage = string.Empty;
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Document(UserContext usrCTX, string databaseName)
        {
            this._errormessage = string.Empty;
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(databaseName);
        }

        public int addLink(string DocumentID, string DocLinkID)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_AddLink");
            this._db.AddInParameter(storedProcCommand, "FK_DOCUMENTID", DbType.Guid, new Guid(DocumentID));
            this._db.AddInParameter(storedProcCommand, "FK_DOCLINKID", DbType.Guid, new Guid(DocLinkID));
            this._db.AddInParameter(storedProcCommand, "FK_USERID", DbType.Guid, new Guid(this.mvarUserContext.UserID));
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.ExecuteNonQuery(storedProcCommand, transaction);
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

        public int addLink(string DocumentID, string DocLinkID, byte LinkType)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_AddLink");
            this._db.AddInParameter(storedProcCommand, "FK_DOCUMENTID", DbType.Guid, new Guid(DocumentID));
            this._db.AddInParameter(storedProcCommand, "FK_DOCLINKID", DbType.Guid, new Guid(DocLinkID));
            this._db.AddInParameter(storedProcCommand, "FK_USERID", DbType.Guid, new Guid(this.mvarUserContext.UserID));
            this._db.AddInParameter(storedProcCommand, "LINK_TYPE", DbType.Byte, LinkType);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.ExecuteNonQuery(storedProcCommand, transaction);
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

        public int addLink(string DocumentID, string DocLinkID, byte LinkType, string AdditionalData1, string AdditionalData2)
        {
            int num = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_AddLink_With_AdditionalInfo");
            this._db.AddInParameter(storedProcCommand, "FK_DOCUMENTID", DbType.Guid, new Guid(DocumentID));
            this._db.AddInParameter(storedProcCommand, "FK_DOCLINKID", DbType.Guid, new Guid(DocLinkID));
            this._db.AddInParameter(storedProcCommand, "FK_USERID", DbType.Guid, new Guid(this.mvarUserContext.UserID));
            this._db.AddInParameter(storedProcCommand, "LINK_TYPE", DbType.Byte, LinkType);
            this._db.AddInParameter(storedProcCommand, "ADDITIONAL_DATA1", DbType.String, AdditionalData1);
            this._db.AddInParameter(storedProcCommand, "ADDITIONAL_DATA2", DbType.String, AdditionalData2);
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
                    num = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return num;
        }

        public override int addnewDataSet(DataSet ds)
        {
            int parameterValue = 0;
            DbCommand updateCommand = null;
            DbCommand deleteCommand = null;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_AddNew_Document");
            this._db.AddInParameter(storedProcCommand, "PK_DOCUMENTID", DbType.Guid, "PK_DOCUMENTID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NAME", DbType.String, "NAME", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DESCRIPTION", DbType.String, "DESCRIPTION", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "BODY", DbType.Xml, "BODY", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DOCUMENTTYPEID", DbType.Guid, "FK_DOCUMENTTYPEID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DOCSPACEID", DbType.Guid, "FK_DOCSPACEID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_PARENTFOLDERID", DbType.Guid, "FK_PARENTFOLDERID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ARCHIVEDPATH", DbType.String, "ARCHIVEDPATH", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "CREATEDBY", DbType.String, "CREATEDBY", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "YEAR", DbType.Int32, "YEAR", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "MONTH", DbType.Byte, "MONTH", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_GroupID", DbType.Guid, "FK_GroupID", DataRowVersion.Current);
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        this._db.UpdateDataSet(ds, ds.Tables[0].TableName, storedProcCommand, updateCommand, deleteCommand, transaction);
                        parameterValue = (int)this._db.GetParameterValue(storedProcCommand, "RETURNVALUE");
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        parameterValue = -1;
                        transaction.Rollback();
                    }
                    return parameterValue;
                }
                finally
                {
                    connection.Close();
                }
            }
            return parameterValue;
        }

        public int CopyDocumentBody(string srcDpocumentID, string desDocumentID)
        {
            return -1;
        }

        public int countDocumentLink(string DocumentID)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_COUNT_DOCLINK");
            this._db.AddInParameter(storedProcCommand, "FK_DOCUMENTID", DbType.Guid, new Guid(DocumentID));
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                try
                {
                    this._db.ExecuteNonQuery(storedProcCommand);
                    parameterValue = (int)this._db.GetParameterValue(storedProcCommand, "RETURNVALUE");
                }
                catch (Exception)
                {
                    parameterValue = -1;
                }
                connection.Close();
            }
            return parameterValue;
        }

        public string createDocument(string DocumentID, string Name, string Description, string DoctypeID, string ParentFolderID, string ArchivedPath, string CreatedUser, string Company, int Year, int Month)
        {
            string message = string.Empty;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_CreateDocument");
            this._db.AddInParameter(storedProcCommand, "PK_DOCUMENTID", DbType.String, DocumentID);
            this._db.AddInParameter(storedProcCommand, "NAME", DbType.String, Name);
            this._db.AddInParameter(storedProcCommand, "DESCRIPTION", DbType.String, Description);
            this._db.AddInParameter(storedProcCommand, "FK_DOCUMENTTYPEID", DbType.String, DoctypeID);
            this._db.AddInParameter(storedProcCommand, "FK_PARENTFOLDERID", DbType.String, ParentFolderID);
            this._db.AddInParameter(storedProcCommand, "ARCHIVEDPATH", DbType.String, ArchivedPath);
            this._db.AddInParameter(storedProcCommand, "CREATEDBY", DbType.String, CreatedUser);
            this._db.AddInParameter(storedProcCommand, "YEAR", DbType.Int16, Year);
            this._db.AddInParameter(storedProcCommand, "MONTH", DbType.Int16, Month);
            this._db.AddInParameter(storedProcCommand, "COMPANY", DbType.String, Company);
            using (DbConnection connection = this._db.CreateConnection())
            {
                try
                {
                    try
                    {
                        connection.Open();
                        this._db.ExecuteNonQuery(storedProcCommand);
                    }
                    catch (Exception exception)
                    {
                        message = exception.Message;
                    }
                    return message;
                }
                finally
                {
                    connection.Close();
                }
            }
            return message;
        }

        public string createDocument(string DocumentID, string Name, string Description, string DoctypeID, string ParentFolderID, string ArchivedPath, string CreatedUser, string Company, int Year, int Month, int Status)
        {
            string message = string.Empty;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_CreateDocument");
            this._db.AddInParameter(storedProcCommand, "PK_DOCUMENTID", DbType.String, DocumentID);
            this._db.AddInParameter(storedProcCommand, "NAME", DbType.String, Name);
            this._db.AddInParameter(storedProcCommand, "DESCRIPTION", DbType.String, Description);
            this._db.AddInParameter(storedProcCommand, "FK_DOCUMENTTYPEID", DbType.String, DoctypeID);
            this._db.AddInParameter(storedProcCommand, "FK_PARENTFOLDERID", DbType.String, ParentFolderID);
            this._db.AddInParameter(storedProcCommand, "ARCHIVEDPATH", DbType.String, ArchivedPath);
            this._db.AddInParameter(storedProcCommand, "CREATEDBY", DbType.String, CreatedUser);
            this._db.AddInParameter(storedProcCommand, "YEAR", DbType.Int16, Year);
            this._db.AddInParameter(storedProcCommand, "MONTH", DbType.Int16, Month);
            this._db.AddInParameter(storedProcCommand, "COMPANY", DbType.String, Company);
            this._db.AddInParameter(storedProcCommand, "STATUS", DbType.Int16, Status);
            using (DbConnection connection = this._db.CreateConnection())
            {
                try
                {
                    try
                    {
                        connection.Open();
                        this._db.ExecuteNonQuery(storedProcCommand);
                    }
                    catch (Exception exception)
                    {
                        message = exception.Message;
                    }
                    return message;
                }
                finally
                {
                    connection.Close();
                }
            }
            return message;
        }

        public string createDocumentOnProcess(DataSet ds)
        {
            string str = string.Empty;
            DbCommand updateCommand = null;
            DbCommand deleteCommand = null;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_CreateDocumentOnProcess");
            this._db.AddInParameter(storedProcCommand, "PK_DOCUMENTID", DbType.Guid, "PK_DOCUMENTID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "NAME", DbType.String, "NAME", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "DESCRIPTION", DbType.String, "DESCRIPTION", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "BODY", DbType.Xml, "BODY", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DOCUMENTTYPEID", DbType.Guid, "FK_DOCUMENTTYPEID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DOCSPACEID", DbType.Guid, "FK_DOCSPACEID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_PARENTFOLDERID", DbType.Guid, "FK_PARENTFOLDERID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ARCHIVEDPATH", DbType.String, "ARCHIVEDPATH", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "CREATEDBY", DbType.String, "CREATEDBY", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "YEAR", DbType.Int32, "YEAR", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "MONTH", DbType.Byte, "MONTH", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_GroupID", DbType.Guid, "FK_GroupID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "PROCESS_INSTANCEID", DbType.Guid, "PROCESS_INSTANCEID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "PROCESS_DEFINITION", DbType.Guid, "PROCESS_DEFINITION", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "ACTIVITY_DEFINITIONID", DbType.Guid, "ACTIVITY_DEFINITIONID", DataRowVersion.Current);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds, ds.Tables[0].TableName, storedProcCommand, updateCommand, deleteCommand, transaction);
                    transaction.Commit();
                    return "";
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    return exception.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
            return str;
        }

        public string createDocumentProperties(string DocumentID, DataSet Properties)
        {
            string message = string.Empty;
            string str2 = string.Empty;
            //LOOKUP_VALUE: property lookup, 
            //FK_DOCTYPE_LOOKUPID: doctype lookup, 
            //DOCUMENT_LOOKUP_VALUE: documentId lookup
            string query = "INSERT INTO T_TYPE_DOC_PROPERTY(FK_DOCUMENTID, FK_PROPERTYID, PROPERTYTYPE, TEXTVALUE, ETEXTVALUE, NUMERICVALUE, DATETIMEVALUE, CREATEDDATETIME, LOOKUP_VALUE, FK_DOCTYPE_LOOKUPID, DOCUMENT_LOOKUP_VALUE)";
            query = query + " VALUES ";
            foreach (DataRow row in Properties.Tables[0].Rows)
            {
                //Lookup
                if (row["PROPERTYTYPE"].ToString() == "9")
                {
                    string str4 = str2;
                    //Check is lookup available

                    //is not available
                    str2 = str4 + ",('" + DocumentID + "','" + row["FK_PROPERTYID"].ToString() + "','8',N'',NULL,NULL,NULL,GetDate(),ISNULL((SELECT LOOKUP_VALUE FROM T_PROPERTY where PK_PROPERTYID = '" + row["FK_PROPERTYID"].ToString() + "'), NULL),ISNULL((SELECT FK_DOCTYPE_LOOKUPID FROM T_PROPERTY where PK_PROPERTYID = '" + row["FK_PROPERTYID"].ToString() + "'), NULL),'" + row["VALUE"].ToString() + "')";
                }
                //Text
                else if (row["PROPERTYTYPE"].ToString() == "8")
                {
                    string str4 = str2;
                    //Check is lookup available

                    //is not available
                    str2 = str4 + ",('" + DocumentID + "','" + row["FK_PROPERTYID"].ToString() + "','8',N'" + row["VALUE"].ToString() + "',NULL,NULL,NULL,GetDate(),NULL,NULL,NULL)";
                }
                //Numeric
                else if (row["PROPERTYTYPE"].ToString() == "6")
                {
                    string str5 = str2;
                    str2 = str5 + ",('" + DocumentID + "','" + row["FK_PROPERTYID"].ToString() + "','6',NULL,NULL,'" + (string.IsNullOrEmpty(row["VALUE"].ToString()) ? "0" : row["VALUE"].ToString()) + "',NULL,GetDate(),NULL,NULL,NULL)";
                    continue;
                }
                //Datetime
                else if (row["PROPERTYTYPE"].ToString() == "3")
                {
                    string str6 = str2;
                    str2 = str6 + ",('" + DocumentID + "','" + row["FK_PROPERTYID"].ToString() + "','3',NULL,NULL,NULL,CONVERT(DATETIME,'" + row["VALUE"].ToString() + "',103),GetDate(),NULL,NULL,NULL)";
                }
            }
            if (!string.IsNullOrEmpty(str2))
            {
                str2 = StringHelper.Right(str2, str2.Length - 1);
                query = query + str2;
                DbCommand sqlStringCommand = this._db.GetSqlStringCommand(query);
                using (DbConnection connection = this._db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        try
                        {
                            this._db.ExecuteNonQuery(sqlStringCommand, transaction);
                            transaction.Commit();
                        }
                        catch (Exception exception)
                        {
                            message = exception.Message;
                            transaction.Rollback();
                        }
                        return message;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            return message;
        }

        public string createDocumentXML(string DocumentId, string DocSpaceId, string ParentFolderId, string DocumentTypeId, string ArchivedPath, string Name, string Description, string Body, string FK_GroupId, string Company, string CreatedBy, int Year, int Month, byte Status)
        {
            this._errormessage = string.Empty;
            string commandText = "Insert Into T_Document (PK_DocumentId, Name, Description, Body, FK_DocumentTypeId, FK_DocSpaceId, FK_ParentFolderId, ArchivedPath, CreatedBy, CreatedDateTime, Year, Month, FK_GroupId, Company, Status)";
            string str2 = commandText;
            commandText = str2 + " Values ('" + DocumentId + "',N'" + Name + "',N'" + Description + "',N'" + Body + "', '" + DocumentTypeId + "', '" + DocSpaceId + "', '" + ParentFolderId + "', '" + ArchivedPath + "', '" + CreatedBy + "', GetDate(), " + Year.ToString() + ", " + Month.ToString() + ", '" + FK_GroupId + "', '" + Company + "', " + Status.ToString() + ")";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    this._errormessage = exception.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
            return this._errormessage;
        }

        public bool checkDocumentName(string DocumentName)
        {
            string commandText = "SELECT NAME FROM T_DOCUMENT WHERE NAME =N'" + DocumentName + "'";
            DataSet set = this._db.ExecuteDataSet(CommandType.Text, commandText);
            return ((set != null) && (set.Tables[0].Rows.Count != 0));
        }

        public override int delete(string Condition)
        {
            int num;
            string commandText = string.Empty;
            commandText = "DELETE FROM T_DOCUMENT WHERE 1=1 ";
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

        public string deleteDocument(string documentid)
        {
            string message = string.Empty;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Delete_Document");
            this._db.AddInParameter(storedProcCommand, "DocumentID", DbType.String, documentid);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        this._db.ExecuteNonQuery(storedProcCommand, transaction);
                        transaction.Commit();
                        message = "";
                    }
                    catch (Exception exception)
                    {
                        message = exception.Message;
                        transaction.Rollback();
                    }
                    return message;
                }
                finally
                {
                    connection.Close();
                }
            }
            return message;
        }

        public int deleteDocumentLink(string DoclinkID, string DocumentID)
        {
            try
            {
                DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_DeleteDocumentLink");
                this._db.AddInParameter(storedProcCommand, "DocumentID", DbType.String, DocumentID);
                this._db.AddInParameter(storedProcCommand, "DocLinkID", DbType.String, DoclinkID);
                return this._db.ExecuteNonQuery(storedProcCommand);
            }
            catch
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
                    commandText = (commandText + " from T_Document") + " where PK_DocumentID='" + ID + "'";
                }
                else
                {
                    commandText = ("select " + Query + " from T_Document") + " where PK_DocumentID='" + ID + "'";
                }
                return this._db.ExecuteDataSet(CommandType.Text, commandText);
            }
            catch
            {
                return null;
            }
        }

        public int getCountOnDocumentLink(string condition)
        {
            try
            {
                string commandText = "SELECT COUNT(*) FROM T_DOCLINK WHERE 1=1 " + condition;
                return (int)this._db.ExecuteScalar(CommandType.Text, commandText);
            }
            catch
            {
                return -1;
            }
        }

        public DataSet GetDocumentByDocType(string DocTypeID, string Condition, string Query)
        {
            return null;
        }

        public DataSet getDocumentInfoByDocLink(string DocumentlinkID, string DocumentTypeID, string Query, string Condition)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_GetDocumentInfo_By_DocLink");
            this._db.AddInParameter(storedProcCommand, "DocumentLinkID", DbType.Guid, new Guid(DocumentlinkID));
            this._db.AddInParameter(storedProcCommand, "DocumentTypeID", DbType.Guid, new Guid(DocumentTypeID));
            this._db.AddInParameter(storedProcCommand, "Query", DbType.String, Query);
            this._db.AddInParameter(storedProcCommand, "Condition", DbType.String, Condition);
            return this._db.ExecuteDataSet(storedProcCommand);
        }

        public DataSet getDocumentList(string DocumentTypeID, string DocFields, string PropertyFields, string Condition)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_GetDocument_List");
            this._db.AddInParameter(storedProcCommand, "DocumentTypeID", DbType.Guid, new Guid(DocumentTypeID));
            this._db.AddInParameter(storedProcCommand, "Doc_Fields", DbType.String, DocFields);
            this._db.AddInParameter(storedProcCommand, "Property_Fields", DbType.String, PropertyFields);
            this._db.AddInParameter(storedProcCommand, "Criteria", DbType.String, Condition);
            return this._db.ExecuteDataSet(storedProcCommand);
        }

        public DataSet getDocumentList_Process(string DocumentTypeID, string Query, string Condition)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_GetDocument_List_And_Process");
            this._db.AddInParameter(storedProcCommand, "DocumentTypeID", DbType.Guid, new Guid(DocumentTypeID));
            this._db.AddInParameter(storedProcCommand, "Query", DbType.String, Query);
            this._db.AddInParameter(storedProcCommand, "Condition", DbType.String, Condition);
            return this._db.ExecuteDataSet(storedProcCommand);
        }

        public DataSet getDocumentListAssignedActivity(string ActivityDefinition, string DocumentType, string Query, string Condition)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_GetDocument_List_Assigned_Activity");
            this._db.AddInParameter(storedProcCommand, "ActivityDefinition", DbType.String, ActivityDefinition);
            this._db.AddInParameter(storedProcCommand, "DocumentType", DbType.String, DocumentType);
            this._db.AddInParameter(storedProcCommand, "Query", DbType.String, Query);
            this._db.AddInParameter(storedProcCommand, "Condition", DbType.String, Condition);
            return this._db.ExecuteDataSet(storedProcCommand);
        }

        public override DataSet getEmpty(string Query)
        {
            string commandText = string.Empty;
            if (!string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT " + Query + " FROM T_DOCUMENT WHERE 1=2";
            }
            else
            {
                commandText = "SELECT * FROM T_DOCUMENT WHERE 1=2";
            }
            return this._db.ExecuteDataSet(CommandType.Text, commandText);
        }

        public DataSet getEmptyDocumentProcess(string Query)
        {
            string commandText = string.Empty;
            if (!string.IsNullOrEmpty(Query))
            {
                commandText = "SELECT " + Query + " FROM T_DOCUMENT WHERE 1=2";
            }
            else
            {
                commandText = "SELECT *,NEWID() AS [PROCESS_INSTANCEID],NEWID() AS [PROCESS_DEFINITION],NEWID() AS [ACTIVITY_DEFINITIONID] FROM T_DOCUMENT WHERE 1=2";
            }
            return this._db.ExecuteDataSet(CommandType.Text, commandText);
        }

        public DataSet GetGroupDocument(string PropertyId, string DocTypeID, string DocSpaceID)
        {
            try
            {
                return null;
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
                    commandText = "SELECT * FROM T_DOCUMENT WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_DOCUMENT WHERE 1=1 ";
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

        public int getPermissionOnObject(string ObjectID, string UserID)
        {
            string commandText = "SELECT GRANTEDRIGHT FROM T_PERMISSION WHERE FK_APPLIEDONID='" + UserID + "' AND FK_APPLIEDONOBJECTID='" + ObjectID + "'";
            DataSet set = this._db.ExecuteDataSet(CommandType.Text, commandText);
            if ((set != null) && (set.Tables[0].Rows.Count != 0))
            {
                return Convert.ToInt32(set.Tables[0].Rows[0]["GRANDTEDRIGHT"]);
            }
            return -1;
        }

        public static db_Document Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Document(usrCTX);
            }
            return mvarInstance;
        }

        public DataSet loadDocumentInfo(string DocumentID)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_LoadDocumentInfo");
            this._db.AddInParameter(storedProcCommand, "PK_DOCUMENTID", DbType.String, DocumentID);
            return this._db.ExecuteDataSet(storedProcCommand);
        }

        public string removeLink(string LinkID)
        {
            string message = string.Empty;
            string query = "Delete From T_DOCLINK Where PK_LinkID='" + LinkID + "'";
            DbCommand sqlStringCommand = this._db.GetSqlStringCommand(query);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        this._db.ExecuteNonQuery(sqlStringCommand, transaction);
                        transaction.Commit();
                        message = "";
                    }
                    catch (Exception exception)
                    {
                        message = exception.Message;
                        transaction.Rollback();
                    }
                    return message;
                }
                finally
                {
                    connection.Close();
                }
            }
            return message;
        }

        public int removeLink(string FK_DocLinkID, string FK_DocumentID)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_RemoveLink");
            this._db.AddInParameter(storedProcCommand, "FK_DocLinkID", DbType.Guid, new Guid(FK_DocLinkID));
            this._db.AddInParameter(storedProcCommand, "FK_Document", DbType.Guid, new Guid(FK_DocumentID));
            this._db.AddOutParameter(storedProcCommand, "RETURNVALUE", DbType.Int32, 4);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.ExecuteNonQuery(storedProcCommand, transaction);
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

        public override int saveDataSet(DataSet ds)
        {
            int parameterValue = 0;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("DMS_Update_Document");
            this._db.AddInParameter(storedProcCommand, "PK_DocumentID", DbType.Guid, "PK_DocumentID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Name", DbType.String, "Name", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Description", DbType.String, "Description", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DocumentTypeID", DbType.Guid, "FK_DocumentTypeID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Price", DbType.Double, "Price", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "Image", DbType.String, "Image", DataRowVersion.Current);
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

        public string updateDocumentBody(DataSet ds)
        {
            string str = string.Empty;
            DbCommand insertCommand = null;
            DbCommand deleteCommand = null;
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_UpdateDocumentBody");
            this._db.AddInParameter(storedProcCommand, "PK_DOCUMENTID", DbType.Guid, "PK_DOCUMENTID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "FK_DOCUMENTTYPEID", DbType.Guid, "FK_DOCUMENTTYPEID", DataRowVersion.Current);
            this._db.AddInParameter(storedProcCommand, "BODY", DbType.Xml, "BODY", DataRowVersion.Current);
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.UpdateDataSet(ds, ds.Tables[0].TableName, insertCommand, storedProcCommand, deleteCommand, transaction);
                    transaction.Commit();
                    return "";
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    return exception.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
            return str;
        }

        public void updateDocumentState(string DocumentID, int Status)
        {
            this._errormessage = string.Empty;
            string commandText = "Update T_Document Set Status=" + Status.ToString() + " Where PK_DocumentId='" + DocumentID + "'";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    this._errormessage = exception.Message;
                }
            }
        }

        public void updateNameDescription(string DocumentID, string Name, string Description)
        {
            DbCommand storedProcCommand = this._db.GetStoredProcCommand("dbo.DMS_Update_Document_NameDescription");
            this._db.AddInParameter(storedProcCommand, "DocumentID", DbType.String, DocumentID);
            this._db.AddInParameter(storedProcCommand, "Name", DbType.String, Name);
            this._db.AddInParameter(storedProcCommand, "Description", DbType.String, Description);
            this._db.ExecuteNonQuery(storedProcCommand);
        }

        public int updatePermissionOnObject(string objectID, string objectType, string applicantObjectID, string applicantObjectType, string Permission)
        {
            return -1;
        }
    }
}


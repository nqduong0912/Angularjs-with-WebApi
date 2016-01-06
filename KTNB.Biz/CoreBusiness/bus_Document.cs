namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using System.Data;
    using vpb.app.business.ktnb.CoreData;
    using System.IO;

    public class bus_Document
    {
        private db_Document _db;
        private string mvarDatabaseName;
        private static bus_Document mvarInstance;
        private UserContext mvarUserContext;

        public bus_Document(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Document(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Document(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Document(this.mvarUserContext, databaseName);
        }

        public int addLink(string DocumentID, string DocLinkID)
        {
            return this._db.addLink(DocumentID, DocLinkID);
        }

        public int addLink(string DocumentID, string DocLinkID, byte LinkType)
        {
            return this._db.addLink(DocumentID, DocLinkID, LinkType);
        }

        public int addLink(string DocumentID, string DocLinkID, byte LinkType, string AdditionalData1, string AdditionalData2)
        {
            return this._db.addLink(DocumentID, DocLinkID, LinkType, AdditionalData1, AdditionalData2);
        }

        public int addnewDataSet(DataSet ds_Document)
        {
            return this._db.addnewDataSet(ds_Document);
        }

        public int countDocumentLink(string DocumentID)
        {
            return this._db.countDocumentLink(DocumentID);
        }

        private void createArchivedPath(string ArchivedPath)
        {
            DirectoryInfo info = new DirectoryInfo(ArchivedPath);
            if (!info.Exists)
            {
                info.Create();
            }
        }

        public string createDocument(string DocumentID, string Name, string Description, string DoctypeID, string ParentFolderID, string ArchivedPath, string CreatedUser, string Company, int Year, int Month)
        {
            return this._db.createDocument(DocumentID, Name, Description, DoctypeID, ParentFolderID, ArchivedPath, CreatedUser, Company, Year, Month);
        }

        public string createDocument(string DocumentID, string Name, string Description, string DoctypeID, string ParentFolderID, string ArchivedPath, string CreatedUser, string Company, int Year, int Month, int Status)
        {
            return this._db.createDocument(DocumentID, Name, Description, DoctypeID, ParentFolderID, ArchivedPath, CreatedUser, Company, Year, Month, Status);
        }

        public string createDocumentOnProcess(DataSet ds)
        {
            return this._db.createDocumentOnProcess(ds);
        }

        public string createDocumentProperties(string DocumentID, DataSet Properties)
        {
            return this._db.createDocumentProperties(DocumentID, Properties);
        }

        public string createDocumentXML(string DocumentId, string DocSpaceId, string ParentFolderId, string DocumentTypeId, string ArchivedPath, string Name, string Description, string Body, string FK_GroupId, string Company, string CreatedBy, int Year, int Month, byte Status)
        {
            return this._db.createDocumentXML(DocumentId, DocSpaceId, ParentFolderId, DocumentTypeId, ArchivedPath, Name, Description, Body, FK_GroupId, Company, CreatedBy, Year, Month, Status);
        }

        private string createFolder(string FolderName, string DocspaceID, string DoctypeID, string ParentFolderID, string Year, string Month)
        {
            DataSet set = new bus_DocSpace(this.mvarUserContext, this.mvarDatabaseName).getByID(DocspaceID, "ARCHIVEDPATH");
            if ((set == null) || (set.Tables[0].Rows.Count == 0))
            {
                return string.Empty;
            }
            string str = set.Tables[0].Rows[0]["ARCHIVEDPATH"].ToString();
            Guid guid = Guid.NewGuid();
            bus_Document document = new bus_Document(this.mvarUserContext, this.mvarDatabaseName);
            DataSet set2 = document.getEmpty(string.Empty);
            DataRow row = set2.Tables[0].NewRow();
            row["PK_DOCUMENTID"] = guid;
            row["NAME"] = FolderName;
            row["DESCRIPTION"] = DBNull.Value;
            row["FK_DOCUMENTTYPEID"] = DoctypeID;
            row["FK_DOCSPACEID"] = DocspaceID;
            if (!string.IsNullOrEmpty(ParentFolderID))
            {
                row["MONTH"] = Month;
                row["YEAR"] = Year;
                row["FK_PARENTFOLDERID"] = ParentFolderID;
                row["ARCHIVEDPATH"] = str + @"\" + DocspaceID + @"\" + ParentFolderID;
            }
            else
            {
                row["YEAR"] = Year;
                row["FK_PARENTFOLDERID"] = DBNull.Value;
                row["ARCHIVEDPATH"] = str + @"\" + DocspaceID;
            }
            row["CREATEDBY"] = "00000000-0000-0000-0000-000000000000";
            set2.Tables[0].Rows.Add(row);
            if (document.addnewDataSet(set2) != 0)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(ParentFolderID))
            {
                this.createArchivedPath(row["ARCHIVEDPATH"].ToString());
            }
            else
            {
                this.createArchivedPath(row["ARCHIVEDPATH"].ToString() + @"\" + guid);
            }
            return guid.ToString();
        }

        public bool checkDocumentName(string DocumentName)
        {
            return this._db.checkDocumentName(DocumentName);
        }

        public int delete(string Condition)
        {
            return this._db.delete(Condition);
        }

        public int deleteByID(string ID)
        {
            return this._db.deleteByID(ID);
        }

        public string deleteDocument(string documentid)
        {
            return this._db.deleteDocument(documentid);
        }

        public DataSet getByID(string ID, string Query)
        {
            return this._db.getByID(ID, Query);
        }

        public int getCountOnDocumentLink(string condition)
        {
            return this._db.getCountOnDocumentLink(condition);
        }

        public DataSet getDocumentInfoByDocLink(string DocumentlinkID, string DocumentTypeID, string Query, string Condition)
        {
            return this._db.getDocumentInfoByDocLink(DocumentlinkID, DocumentTypeID, Query, Condition);
        }

        public DataSet getDocumentList(string DocumentTypeID, string DocFields, string PropertyFields, string Condition)
        {
            return this._db.getDocumentList(DocumentTypeID, DocFields, PropertyFields, Condition);
        }

        public DataSet getDocumentList_Process(string DocumentTypeID, string Query, string Condition)
        {
            return this._db.getDocumentList_Process(DocumentTypeID, Query, Condition);
        }

        public DataSet getDocumentListAssignedActivity(string ActivityDefinition, string DocumentType, string Query, string Condition)
        {
            return this._db.getDocumentListAssignedActivity(ActivityDefinition, DocumentType, Query, Condition);
        }

        public DataSet getEmpty(string Query)
        {
            return this._db.getEmpty(Query);
        }

        public DataSet getEmptyDocumentProcess(string Query)
        {
            return this._db.getEmptyDocumentProcess(Query);
        }

        public DataSet getList(string Condition, string Query)
        {
            return this._db.getList(Condition, Query);
        }

        public int getPermissionOnObject(string ObjectID, string UserID)
        {
            return this._db.getPermissionOnObject(ObjectID, UserID);
        }

        public static bus_Document Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Document(usrCTX);
            }
            return mvarInstance;
        }

        public DataSet loadDocumentInfo(string DocumentID)
        {
            return this._db.loadDocumentInfo(DocumentID);
        }

        public string newcheckFolder(string Year, string Month, string DocspaceID, string FolderDoctypeID)
        {
            string str3;
            string str4;
            bus_Document document = Instance(this.mvarUserContext);
            string userID = this.mvarUserContext.UserID;
            if (string.IsNullOrEmpty(userID))
            {
                userID = "00000000-0000-0000-0000-000000000000";
            }
            if (Month.Length == 1)
            {
                Month = "0" + Month;
            }
            string folderName = "Năm " + Year;
            string str2 = "Th\x00e1ng " + Month;
            string condition = "AND NAME=N'" + folderName + "' AND FK_DOCSPACEID='" + DocspaceID + "' AND FK_DOCUMENTTYPEID='" + FolderDoctypeID + "'";
            DataSet set = document.getList(condition, string.Empty);
            if ((set == null) || (set.Tables[0].Rows.Count == 0))
            {
                str3 = this.createFolder(folderName, DocspaceID, FolderDoctypeID, string.Empty, Year, Month);
                this.updatePermissionOnObject(userID, 1, str3, 12, 15);
            }
            else
            {
                str3 = set.Tables[0].Rows[0]["PK_DOCUMENTID"].ToString();
            }
            condition = "AND NAME=N'" + str2 + "' AND FK_PARENTFOLDERID='" + str3 + "' AND FK_DOCSPACEID='" + DocspaceID + "' AND FK_DOCUMENTTYPEID='" + FolderDoctypeID + "'";
            set = document.getList(condition, string.Empty);
            if ((set == null) || (set.Tables[0].Rows.Count == 0))
            {
                str4 = this.createFolder(str2, DocspaceID, FolderDoctypeID, str3, Year, Month);
                this.updatePermissionOnObject(userID, 1, str4, 12, 15);
            }
            else
            {
                str4 = set.Tables[0].Rows[0]["PK_DOCUMENTID"].ToString();
            }
            if (!string.IsNullOrEmpty(str4))
            {
                return str4;
            }
            return string.Empty;
        }

        public string removeLink(string LinkID)
        {
            return this._db.removeLink(LinkID);
        }

        public int removeLink(string FK_DocLinkID, string FK_DocumentID)
        {
            return this._db.removeLink(FK_DocLinkID, FK_DocumentID);
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }

        public int upDateDocument(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }

        public string updateDocumentBody(DataSet ds)
        {
            return this._db.updateDocumentBody(ds);
        }

        public void updateDocumentState(string DocumentID, int Status)
        {
            this._db.updateDocumentState(DocumentID, Status);
        }

        public void updateNameDescription(string DocumentID, string Name, string Description)
        {
            this._db.updateNameDescription(DocumentID, Name, Description);
        }

        public int updatePermissionOnObject(string UserID, byte UserType, string ObjectID, byte TypeOfObject, byte GrantedRight)
        {
            bus_Permission permission = new bus_Permission(this.mvarUserContext, this.mvarDatabaseName);
            return permission.updatePermissionOnObject(UserID, UserType, ObjectID, TypeOfObject, GrantedRight, "", "", "", 0);
        }
    }
}


namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using System.Data;
    using vpb.app.business.ktnb.CoreData;

    public class bus_Permission
    {
        private db_Permission _db;
        private string mvarDatabaseName;
        private static bus_Permission mvarInstance;
        private UserContext mvarUserContext;

        public bus_Permission(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Permission(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Permission(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Permission(this.mvarUserContext, databaseName);
        }

        public int changePermissionOnObject(string UserID, byte UserType, string ObjectID, byte TypeOfObject, byte GrantedRight)
        {
            return this._db.changePermissionOnObject(UserID, UserType, ObjectID, TypeOfObject, GrantedRight);
        }

        public int delete(string Condition)
        {
            return this._db.delete(Condition);
        }

        public DataSet getByID(string ID, string Query)
        {
            return this._db.getByID(ID, Query);
        }

        public DataSet GetByName(string UserName, string Query)
        {
            return null;
        }

        public DataSet getEmpty(string Query)
        {
            return this._db.getEmpty(Query);
        }

        public DataSet getList(string Condition, string Query)
        {
            return this._db.getList(Condition, Query);
        }

        public int getPermissionOnObject(string ObjectID, string UserID)
        {
            return this._db.getPermissionOnObject(ObjectID, UserID);
        }

        public int getPermissionOnObject(string ObjectID, string ApplicantID, byte TypeOfApplicant)
        {
            return this._db.getPermissionOnObject(ObjectID, ApplicantID, TypeOfApplicant);
        }

        public int grantPermission(string AppliedOnID, int TypeOfApplicant, string AppliedOnObjectID, int TypeOfObject, string AdditionalObjectID, int TypeOfAdditionalObject, int GrantedRight, int DeniedRight, string FolderID)
        {
            return this._db.grantPermission(AppliedOnID, TypeOfApplicant, AppliedOnObjectID, TypeOfObject, AdditionalObjectID, TypeOfAdditionalObject, GrantedRight, DeniedRight, FolderID);
        }

        public static bus_Permission Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Permission(usrCTX);
            }
            return mvarInstance;
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }

        public int updatePermissionOnObject(string UserID, byte UserType, string ObjectID, byte TypeOfObject, byte GrantedRight, string FK_ParentFolderIDOnPermission, string CreatedBy, string FinishedDate, byte Status)
        {
            return this._db.updatePermissionOnObject(UserID, UserType, ObjectID, TypeOfObject, GrantedRight, FK_ParentFolderIDOnPermission, CreatedBy, FinishedDate, Status);
        }
    }
}


namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using System.Data;
    using System.Diagnostics;
    using vpb.app.business.ktnb.CoreData;

    public class bus_Audit
    {
        private db_Audit _db;
        private string mvarDatabaseName;
        private static bus_Audit mvarInstance;
        private UserContext mvarUserContext;

        public bus_Audit(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Audit(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Audit(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Audit(this.mvarUserContext, databaseName);
        }

        public string AddAuditLog(string ProcessInstanceID, string ActivityInstanceID, string FK_ObjectID, byte ObjectType, byte OperationType, string AdditionalData1, string AdditionalData2)
        {
            return this._db.AddAuditLog(ProcessInstanceID, ActivityInstanceID, FK_ObjectID, ObjectType, OperationType, AdditionalData1, AdditionalData2);
        }

        public string AddAuditLog(string ProcessInstanceID, string ActivityInstanceID, string FK_ObjectID, byte ObjectType, byte OperationType, string AdditionalData1, string AdditionalData2, string UserID, string UserIP, string UserName, string RoleName, string GroupName)
        {
            return this._db.AddAuditLog(ProcessInstanceID, ActivityInstanceID, FK_ObjectID, ObjectType, OperationType, AdditionalData1, AdditionalData2, UserID, UserIP, UserName, RoleName, GroupName);
        }

        public int addnewDataSet(DataSet ds_Document)
        {
            return this._db.addnewDataSet(ds_Document);
        }

        public int delete(string Condition)
        {
            return this._db.delete(Condition);
        }

        public int deleteByID(string ID)
        {
            return this._db.deleteByID(ID);
        }

        public DataSet getByID(string ID, string Query)
        {
            return this._db.getByID(ID, Query);
        }

        public DataSet getEmpty(string Query)
        {
            return this._db.getEmpty(Query);
        }

        public DataSet getList(string Condition, string Query)
        {
            return this._db.getList(Condition, Query);
        }

        public DataSet getList_Audit(string Condition, string Query)
        {
            return this._db.getList_Audit(Condition, Query);
        }

        public static bus_Audit Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Audit(usrCTX);
            }
            return mvarInstance;
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }

        public void WriteEventLog(string Event)
        {
            string logName = "Application";
            string dBName = this.mvarUserContext.DBName;
            if (!EventLog.SourceExists(dBName))
            {
                EventLog.CreateEventSource(dBName, logName);
            }
            EventLog.WriteEntry(dBName, Event, EventLogEntryType.Information);
        }

        private void WriteEventLog(string sSource, string sLog, string sEvent)
        {
            if (!EventLog.SourceExists(sSource))
            {
                EventLog.CreateEventSource(sSource, sLog);
            }
            EventLog.WriteEntry(sSource, sEvent, EventLogEntryType.Information);
        }
    }
}


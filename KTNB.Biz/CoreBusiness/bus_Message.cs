namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using System.Data;
    using vpb.app.business.ktnb.CoreData;

    public class bus_Message
    {
        private db_Message _db;
        private string mvarDatabaseName;
        private static bus_Message mvarInstance;
        private UserContext mvarUserContext;

        public bus_Message(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Message(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Message(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Message(this.mvarUserContext, this.mvarDatabaseName);
        }

        public int addnewDataSet(DataSet ds)
        {
            return this._db.addnewDataSet(ds);
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

        public static bus_Message Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Message(usrCTX);
            }
            return mvarInstance;
        }

        public void PrepareMessage(Guid DocumentID, Guid FromUserID, string Content, string Title, string LinkTo, string MessageType)
        {
            this._db.PrepareMessage(DocumentID, FromUserID, Content, Title, LinkTo, MessageType);
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }
    }
}


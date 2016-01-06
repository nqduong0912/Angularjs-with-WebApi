namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using CORE.HELPERS;
    using System;
    using vpb.app.business.ktnb.CoreData;
    using System.Data;

    public class bus_Doc_Version_Body
    {
        private db_Doc_Version_Body _db;
        private string mvarDatabaseName;
        private static bus_Doc_Version_Body mvarInstance;
        private UserContext mvarUserContext;

        public bus_Doc_Version_Body(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Doc_Version_Body(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Doc_Version_Body(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Doc_Version_Body(this.mvarUserContext, databaseName);
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
            string str = string.Empty;
            string param = string.Empty;
            string query = "filename,filepath";
            DataSet set = this.getList(" and PK_DocversionbodyID='" + ID + "'", query);
            if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
            {
                DataRow row = set.Tables[0].Rows[0];
                str = row["filename"].ToString();
                param = row["filepath"].ToString();
                if (StringHelper.Right(param, 1) == @"\")
                {
                    param = StringHelper.Left(param, param.Length - 1);
                }
            }
            int num = this._db.deleteByID(ID);
            if ((num != -1) && !string.IsNullOrEmpty(str))
            {
                FileHelper.deleteFile(param + @"\" + str);
            }
            return num;
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

        public static bus_Doc_Version_Body Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Doc_Version_Body(usrCTX);
            }
            return mvarInstance;
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }
    }
}


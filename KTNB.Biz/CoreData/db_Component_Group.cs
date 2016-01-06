namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Component_Group : ADBManager
    {
        private Database _db;
        private static db_Component_Group mvarInstance;
        private UserContext mvarUserContext;

        public db_Component_Group(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Component_Group(UserContext usrCTX, string databaseName)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(databaseName);
        }

        public int addComponentGroup(string ComponentID, string RoleID, string GroupID)
        {
            int num;
            string commandText = string.Empty;
            commandText = "Insert Into dbo.T_COMPONENT_GROUP (FK_ComponentID,FK_RoleID,FK_GroupID)";
            string str2 = commandText;
            commandText = str2 + " Values('" + ComponentID + "','" + RoleID + "','" + GroupID + "')";
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

        public override int addnewDataSet(DataSet ds)
        {
            return 0;
        }

        public override int delete(string Condition)
        {
            int num;
            string commandText = string.Empty;
            commandText = "DELETE FROM dbo.T_COMPONENT_GROUP WHERE 1=1 ";
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
            return 0;
        }

        public override DataSet getByID(string ID, string Query)
        {
            return null;
        }

        public override DataSet getEmpty(string Query)
        {
            return null;
        }

        public override DataSet getList(string Condition, string Query)
        {
            try
            {
                string commandText = string.Empty;
                if (string.IsNullOrEmpty(Query))
                {
                    commandText = "SELECT * FROM dbo.T_COMPONENT_GROUP WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM dbo.T_COMPONENT_GROUP WHERE 1=1 ";
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

        public static db_Component_Group Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Component_Group(usrCTX);
            }
            return mvarInstance;
        }

        public override int saveDataSet(DataSet ds)
        {
            return 0;
        }
    }
}


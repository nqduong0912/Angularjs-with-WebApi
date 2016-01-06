namespace vpb.app.business.ktnb.CoreData
{
    using CORE.CoreObjectContext;
    using CoreInterface;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    internal class db_Project_Parameter : ADBManager
    {
        private Database _db;
        private static db_Project_Parameter mvarInstance;
        private UserContext mvarUserContext;

        public db_Project_Parameter(UserContext usrCTX)
        {
            this.mvarUserContext = usrCTX;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
        }

        public db_Project_Parameter(UserContext usrCTX, string databaseName)
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
            return -1;
        }

        public int CreateParameter(string Name, string FullName, string Value)
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "INSERT INTO T_PROJECT_PARAMETER (NAME,FULLNAME,VALUE) VALUES(N'" + Name + "',N'" + FullName + "',N'" + Value + "')";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText) == 1)
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
                    num = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return num;
        }

        public int CreateParameter(string Name, string FullName, string Value, string GroupName)
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "INSERT INTO T_PROJECT_PARAMETER (NAME,FULLNAME,VALUE,GROUPNAME) VALUES(N'" + Name + "',N'" + FullName + "',N'" + Value + "',N'" + GroupName + "')";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText) == 1)
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
                    num = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return num;
        }

        public int CreateParameter(string Name, string FullName, string Value, string GroupName, string ApplicationID, string ParaType)
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "INSERT INTO T_PROJECT_PARAMETER (NAME,FULLNAME,VALUE,GROUPNAME,FK_APPLICATIONID,PARAMETER_TYPE) VALUES(N'" + Name + "',N'" + FullName + "',N'" + Value + "',N'" + GroupName + "',N'" + ApplicationID + "',N'" + ParaType + "')";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText) == 1)
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
                    num = -1;
                    transaction.Rollback();
                }
                connection.Close();
            }
            return num;
        }

        public override int delete(string Condition)
        {
            int num;
            string commandText = string.Empty;
            commandText = "DELETE FROM T_PROJECT_PARAMETER WHERE 1=1 ";
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

        public override int deleteByID(string Name)
        {
            int num = 0;
            string commandText = "DELETE FROM T_PROJECT_PARAMETER WHERE NAME='" + Name + "'";
            if (this._db.ExecuteNonQuery(CommandType.Text, commandText) != 1)
            {
                num = -1;
            }
            return num;
        }

        public override DataSet getByID(string Name, string Query)
        {
            try
            {
                string commandText = string.Empty;
                if (string.IsNullOrEmpty(Query))
                {
                    commandText = "SELECT *";
                    commandText = (commandText + " FROM T_PROJECT_PARAMETER") + " WHERE NAME='" + Name + "'";
                }
                else
                {
                    commandText = ("SELECT " + Query + " FROM T_PROJECT_PARAMETER") + " WHERE NAME='" + Name + "'";
                }
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
                    commandText = "SELECT " + Query + " FROM T_PROJECT_PARAMETER WHERE 1=2";
                }
                else
                {
                    commandText = "SELECT * FROM T_PROJECT_PARAMETER WHERE 1=2";
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
                    commandText = "SELECT * FROM T_PROJECT_PARAMETER WHERE 1=1 ";
                }
                else
                {
                    commandText = "SELECT " + Query + " FROM T_PROJECT_PARAMETER WHERE 1=1 ";
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

        public static db_Project_Parameter Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new db_Project_Parameter(usrCTX);
            }
            return mvarInstance;
        }

        public override int saveDataSet(DataSet ds)
        {
            return -1;
        }

        public int UpdateParameter(string Name, string FullName, string Value)
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "UPDATE T_PROJECT_PARAMETER SET FULLNAME=N'" + FullName + "', VALUE=N'" + Value + "' WHERE NAME=N'" + Name + "'";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText) == 1)
                    {
                        transaction.Commit();
                    }
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

        public int UpdateParameter(string Name, string FullName, string Value, string GroupName)
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "UPDATE T_PROJECT_PARAMETER SET FULLNAME=N'" + FullName + "', VALUE=N'" + Value + "', GROUPNAME=N'" + GroupName + "' WHERE NAME=N'" + Name + "'";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText) == 1)
                    {
                        transaction.Commit();
                    }
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

        public int UpdateParameter(string Name, string FullName, string Value, string GroupName, string Application, string ParaType)
        {
            string commandText = string.Empty;
            int num = 0;
            commandText = "UPDATE T_PROJECT_PARAMETER SET FK_APPLICATIONID=N'" + Application + "', PARAMETER_TYPE=N'" + ParaType + "', FULLNAME=N'" + FullName + "', VALUE=N'" + Value + "', GROUPNAME=N'" + GroupName + "' WHERE NAME=N'" + Name + "'";
            using (DbConnection connection = this._db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (this._db.ExecuteNonQuery(transaction, CommandType.Text, commandText) == 1)
                    {
                        transaction.Commit();
                    }
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
    }
}


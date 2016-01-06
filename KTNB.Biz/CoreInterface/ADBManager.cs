namespace vpb.app.business.ktnb.CoreInterface
{
    using System;
    using System.Data;

    public abstract class ADBManager
    {
        protected ADBManager()
        {
        }

        public abstract int addnewDataSet(DataSet ds);

        public abstract int delete(string Condition);

        public abstract int deleteByID(string ID);

        public abstract DataSet getByID(string ID, string Query);

        public abstract DataSet getEmpty(string Query);

        public abstract DataSet getList(string Condition, string Query);

        public abstract int saveDataSet(DataSet ds);
    }
}


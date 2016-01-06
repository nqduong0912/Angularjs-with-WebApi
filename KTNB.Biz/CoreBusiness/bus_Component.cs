namespace vpb.app.business.ktnb.CoreBusiness
{
    using CORE.CoreObjectContext;
    using System;
    using System.Data;
    using vpb.app.business.ktnb.CoreData;

    public class bus_Component
    {
        private db_Component _db;
        private string mvarDatabaseName;
        private static bus_Component mvarInstance;
        private UserContext mvarUserContext;

        public bus_Component(UserContext usrCTX)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = usrCTX.DBName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Component(this.mvarUserContext, this.mvarDatabaseName);
        }

        public bus_Component(UserContext usrCTX, string databaseName)
        {
            this.mvarDatabaseName = string.Empty;
            this.mvarUserContext = usrCTX;
            this.mvarDatabaseName = databaseName;
            if (this.mvarUserContext == null)
            {
                throw new Exception("UserContext is null");
            }
            this._db = new db_Component(this.mvarUserContext, databaseName);
        }

        public int addnewDataSet(DataSet ds_Document)
        {
            return this._db.addnewDataSet(ds_Document);
        }

        public int addRoleToComponent(string componentid, string roleid)
        {
            return this._db.addRoleToComponent(componentid, roleid);
        }

        public int appendGroup(string FK_ComponentID, string FK_RoleID, string GroupID)
        {
            return this._db.appendGroup(FK_ComponentID, FK_RoleID, GroupID);
        }

        public int delete(string Condition)
        {
            return this._db.delete(Condition);
        }

        public int deleteByID(string ID)
        {
            return this._db.deleteByID(ID);
        }

        public int deleteComponentGroup(string ComponentID, string RoleID, string GroupID)
        {
            return this._db.deleteComponentGroup(ComponentID, RoleID, GroupID);
        }

        public int deleteRoleFromComponent(string componentid, string roleid)
        {
            return this._db.deleteRoleFromComponent(componentid, roleid);
        }

        public DataSet getByID(string ID, string Query)
        {
            return this._db.getByID(ID, Query);
        }

        public DataSet getComponentNGroupMappedWithRole(string roleID)
        {
            return this._db.getComponentNGroupMappedWithRole(roleID);
        }

        public DataSet getEmpty(string Query)
        {
            return this._db.getEmpty(Query);
        }

        public DataSet getList(string Condition, string Query)
        {
            return this._db.getList(Condition, Query);
        }

        public DataSet getListComponentRole(string Condition, string Query)
        {
            return this._db.getListComponentRole(Condition, Query);
        }

        public DataSet getRoleOnComponent(string ID)
        {
            return this._db.getRoleOnComponent(ID);
        }

        public static bus_Component Instance(UserContext usrCTX)
        {
            if (mvarInstance == null)
            {
                mvarInstance = new bus_Component(usrCTX);
            }
            return mvarInstance;
        }

        public bool isRoleAssignedOnComponent(string componentid, string roleid)
        {
            return this._db.isRoleAssignedOnComponent(componentid, roleid);
        }

        public int mapComponentGroup(string ComponentID, string RoleID, string GroupID)
        {
            return this._db.mapComponentGroup(ComponentID, RoleID, GroupID);
        }

        public int saveDataSet(DataSet ds)
        {
            return this._db.saveDataSet(ds);
        }

        public int updateGroup(string FK_ComponentID, string FK_RoleID, string GroupID)
        {
            return this._db.updateGroup(FK_ComponentID, FK_RoleID, GroupID);
        }
    }
}


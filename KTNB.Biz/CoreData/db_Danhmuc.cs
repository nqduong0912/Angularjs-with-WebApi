using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CORE.CoreObjectContext;

namespace vpb.app.business
{
    namespace ktnb.CoreData
    {
        internal class db_Danhmuc
        {
            #region Private variables
            private Database _db = null;
            private UserContext mvarUserContext;
            private static db_Danhmuc mvarInstance;
            private string _error_message = string.Empty;
            #endregion

            #region Constructor
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <returns></returns>
            public static db_Danhmuc Instance(UserContext usrCTX)
            {
                if (mvarInstance == null)
                    mvarInstance = new db_Danhmuc(usrCTX);
                return mvarInstance;
            }

            /// <summary>
            /// db_Danhmuc
            /// </summary>
            /// <param name="usrCTX"></param>
            public db_Danhmuc(UserContext usrCTX)
            {
                mvarUserContext = usrCTX;
                if (mvarUserContext != null)
                    _db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
                else
                    throw new Exception("UserContext is null");

            }

            /// <summary>
            /// db_Danhmuc
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <param name="databaseName"></param>
            public db_Danhmuc(UserContext usrCTX, string databaseName)
            {
                mvarUserContext = usrCTX;
                if (mvarUserContext != null)
                    _db = DatabaseFactory.CreateDatabase(databaseName);
                else
                    throw new Exception("UserContext is null");

            }

            /// <summary>
            /// 
            /// </summary>
            public string ErrorMessage
            {
                get { return _error_message; }
            }
            #endregion

            #region IDatabaseMgmt Members

            #endregion
        }
    }
}
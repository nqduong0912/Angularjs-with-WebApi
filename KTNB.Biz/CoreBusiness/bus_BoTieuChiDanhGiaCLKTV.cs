using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreData;
using System.Data;

namespace vpb.app.business
{
    namespace ktnb.CoreBusiness
    {
        public class bus_BoTieuChiDanhGiaCLKTV
        {
            db_BoTieuChiDanhGiaCLKTV _db = null;
            private UserContext mvarUserContext = null;
            private static bus_BoTieuChiDanhGiaCLKTV mvarInstance;

            #region Constructors
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <returns></returns>
            public static bus_BoTieuChiDanhGiaCLKTV Instance(UserContext usrCTX)
            {
                if (mvarInstance == null)
                    mvarInstance = new bus_BoTieuChiDanhGiaCLKTV(usrCTX);
                return mvarInstance;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            public bus_BoTieuChiDanhGiaCLKTV(UserContext usrCTX)
            {
                mvarUserContext = usrCTX;
                string mvarDatabaseName = usrCTX.DBName;
                if (mvarUserContext != null)
                    _db = new db_BoTieuChiDanhGiaCLKTV(mvarUserContext, mvarDatabaseName);
                else
                    throw new Exception("UserContext is null");

            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <param name="databaseName"></param>
            public bus_BoTieuChiDanhGiaCLKTV(UserContext usrCTX, string databaseName)
            {
                mvarUserContext = usrCTX;
                if (mvarUserContext != null)
                    _db = new db_BoTieuChiDanhGiaCLKTV(mvarUserContext, databaseName);
                else
                    throw new Exception("UserContext is null");

            }
            #endregion

            #region Interface methods

            /// <summary>
            /// Lay danh dach cac loai tieu chi
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachLoaiTieuChi(string BoTieuChiID)
            {
                return _db.DanhSachLoaiTieuChi(BoTieuChiID);
            }
            /// <summary>
            ///Set Active tat ca bo tieu chi tru bo tieu chi truyen vao
            /// </summary>
            /// <param name="exceptBoTieuChiID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public int SetInActiveAll(string exceptBoTieuChiID, string LoaiBoTieuChi)
            {
                return _db.SetInActiveAll(exceptBoTieuChiID, LoaiBoTieuChi);
            }
            #endregion
        }
    }

}

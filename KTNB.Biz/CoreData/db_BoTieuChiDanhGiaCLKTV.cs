using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CORE.CoreObjectContext;
using System.Data;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.CoreBusiness;

namespace vpb.app.business
{
    namespace ktnb.CoreData
    {
        internal class db_BoTieuChiDanhGiaCLKTV
        {
            #region Private variables
            private Database _db = null;
            private UserContext mvarUserContext;
            private static db_BoTieuChiDanhGiaCLKTV mvarInstance;
            private string _error_message = string.Empty;


            #endregion

            #region Constructor
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <returns></returns>
            public static db_BoTieuChiDanhGiaCLKTV Instance(UserContext usrCTX)
            {
                if (mvarInstance == null)
                    mvarInstance = new db_BoTieuChiDanhGiaCLKTV(usrCTX);
                return mvarInstance;
            }
            /// <summary>
            /// db_BoTieuChiDanhGiaCLKTV
            /// </summary>
            /// <param name="usrCTX"></param>
            public db_BoTieuChiDanhGiaCLKTV(UserContext usrCTX)
            {
                mvarUserContext = usrCTX;
                if (mvarUserContext != null)
                    _db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
                else
                    throw new Exception("UserContext is null");

            }
            /// <summary>
            /// db_BoTieuChiDanhGiaCLKTV
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <param name="databaseName"></param>
            public db_BoTieuChiDanhGiaCLKTV(UserContext usrCTX, string databaseName)
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

            /// <summary>
            /// Danh sách loại tiêu chí thuộc bộ tiêu chí
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachLoaiTieuChi(string BoTieuChiID)
            {
                string LOAI_TIEU_CHI = "186FFBD8-8695-4304-B2E9-C6BC951B60CC";
                string SQL = "Select FK_DOCUMENTID as LoaiTieuChiID, FK_DOCLINKID as BoTieuChiID, ADDTIONAL_DATA1 AS TYTRONG  "
                                + " From T_DOCLINK as dl inner join T_DOCUMENT as doc "
                                + " on dl.FK_DOCUMENTID = doc.PK_DOCUMENTID "
                                + " where dl.FK_DOCLINKID = '" + BoTieuChiID + "'"
                                + " and doc.FK_DOCUMENTTYPEID = '" + DOCTYPE.LOAI_TIEUCHI_DANHGIA_CHATLUONG_KTV + "'";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("loai_tieu_chi", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["loai_tieu_chi"] = getPropertyValueOnDocument(dtrow["LoaiTieuChiID"].ToString(), LOAI_TIEU_CHI);
                        }
                        ds.Tables[0].DefaultView.Sort = "loai_tieu_chi ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Set Active cac bo tieu chi boi Loai Bo Tieu Chi tru Bo tieu chi duoc truyen vao
            /// </summary>
            /// <param name="exceptBoTieuChiID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public int SetInActiveAll(string exceptBoTieuChiID, string LoaiBoTieuChi)
            {
                //string LOAI_TIEU_CHI = "186FFBD8-8695-4304-B2E9-C6BC951B60CC";
                int result = 0;
                string SQL = "update T_DOCUMENT "
                                + " set STATUS = 2 "
                                + " where PK_DOCUMENTID != '" + exceptBoTieuChiID + "' "
                                + " and PK_DOCUMENTID IN (select FK_DOCUMENTID from T_TYPE_DOC_PROPERTY where FK_PROPERTYID = 'FB2F72B5-27DC-458B-BB16-D7DC09E4C7B2'"
                                + " and TEXTVALUE = N'" + LoaiBoTieuChi + "')";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        result = _db.ExecuteNonQuery(CommandType.Text, SQL);
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return result;
            }
            public string getPropertyValueOnDocument(string documentID, string propertyID)
            {
                bus_Type_Doc_Property objTDP = bus_Type_Doc_Property.Instance(mvarUserContext);
                string svalue = objTDP.GetPropertyValueOnDocument(documentID, propertyID);
                objTDP = null;
                return svalue;
            }
            #endregion
        }
    }


}

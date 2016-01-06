using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreData;
using System.Data;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.CoreBusiness;

namespace vpb.app.business
{
    namespace ktnb.CoreBusiness
    {
        public class bus_LapKeHoach
        {
            db_HoSoPhanTichSoBo _db = null;
            private UserContext mvarUserContext = null;
            private static bus_LapKeHoach mvarInstance;

            #region Constructors
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <returns></returns>
            public static bus_LapKeHoach Instance(UserContext usrCTX)
            {
                if (mvarInstance == null)
                    mvarInstance = new bus_LapKeHoach(usrCTX);
                return mvarInstance;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            public bus_LapKeHoach(UserContext usrCTX)
            {
                mvarUserContext = usrCTX;
                string mvarDatabaseName = usrCTX.DBName;
                if (mvarUserContext != null)
                    _db = new db_HoSoPhanTichSoBo(mvarUserContext, mvarDatabaseName);
                else
                    throw new Exception("UserContext is null");

            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <param name="databaseName"></param>
            public bus_LapKeHoach(UserContext usrCTX, string databaseName)
            {
                mvarUserContext = usrCTX;
                if (mvarUserContext != null)
                    _db = new db_HoSoPhanTichSoBo(mvarUserContext, databaseName);
                else
                    throw new Exception("UserContext is null");

            }
            #endregion

            #region Interface methods

            /// <summary>
            /// lay cac ho so phan tich so bo
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet ChiTietHoSoPhanTichSoBo(string nhomKT)
            {
                return _db.ChiTietHoSoPhanTichSoBo(nhomKT);
            }
            /// <summary>
            /// Lấy các hồ sơ phân tích rủi ro được phụ trách bởi người đăng nhập
            /// </summary>
            /// <param name="nhomKT"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet ChiTietHoSoPhanTichRuiRo(string nhomKT, string username)
            {
                return _db.ChiTietHoSoPhanTichRuiRo(nhomKT, username);
            }
            /// <summary>
            /// lay cac ho so phan tich rui ro
            /// </summary>
            /// <param name="nhomKT"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet ChiTietHoSoPhanTichRuiRo(string nhomKT)
            {
                return _db.ChiTietHoSoPhanTichRuiRo(nhomKT);
            }
            /// <summary>
            /// danh sach mang nghiep vu duoc giao cho nhom
            /// </summary>
            /// <param name="userName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DSMangNghiepVuDuocGiaoChoNhom(string userName)
            {
                return _db.DSMangNghiepVuDuocGiaoChoNhom(userName);
            }
            /// <summary>
            /// Danh sach cac muc do rui ro
            /// </summary>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DSMucDoRuiRo()
            {
                return _db.DSMucDoRuiRo();
            }
            /// <summary>
            /// Kiem tra phan tich so bo da duoc submit hay chua
            /// </summary>
            /// <param name="tenMangNghiepVu"></param>
            /// <param name="nhomKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public bool IsPhanTichSoBoSubmitted(string tenMangNghiepVu, string nhomKiemToanID)
            {
                bool IsSubmitted = false;
                bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(mvarUserContext);
                DataSet dsPhanTichSoBo = lapKeHoach.ChiTietHoSoPhanTichSoBo(nhomKiemToanID);

                if (dsPhanTichSoBo != null)
                {
                    DataRow[] foundRows;
                    string expression = "STATUS=4 and  mang_nghiep_vu ='" + tenMangNghiepVu + "'";
                    foundRows = dsPhanTichSoBo.Tables[0].Select(expression);
                    if (foundRows.Count() > 0)
                        IsSubmitted = true;
                }

                return IsSubmitted;
            }
            /// <summary>
            /// Kiem tra Mang Nghiep Vu da duoc phan tich so bo chua
            /// </summary>
            /// <param name="tenMangNghiepVu"></param>
            /// <param name="nhomKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public bool IsHavePhanTichSoBoMangNghiepVu(string tenMangNghiepVu, string nhomKiemToanID)
            {
                bool IsHas = false;
                bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(mvarUserContext);
                DataSet dsPhanTichSoBo = lapKeHoach.ChiTietHoSoPhanTichSoBo(nhomKiemToanID);

                if (dsPhanTichSoBo != null)
                {
                    DataRow[] foundRows;
                    string expression = "mang_nghiep_vu ='" + tenMangNghiepVu + "'";
                    foundRows = dsPhanTichSoBo.Tables[0].Select(expression);
                    if (foundRows.Count() > 0)
                        IsHas = true;
                }

                return IsHas;
            }
            /// <summary>
            /// Lay cac thu tuc boi mang nghiep vu
            /// </summary>
            /// <param name="mangNghieVuID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetThuTucByMangNghiepVu(string mangNghieVuID)
            {
                DataTable dtThuTucByKiemSoat = new DataTable();
                string sTenMangNghiepVu = _db.getPropertyValueOnDocument(mangNghieVuID, "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");
                string sMucTieuKiemSoat_doctype = DOCTYPE.MUCTIEU_KIEMSOAT;
                //get cac muc tieu kiem soat by mang nghiep vu
                string DocFields = "PK_DocumentID,[Mảng nghiệp vụ],[Tên Mục tiêu kiểm soát]";
                string PropertyFields = "Mảng nghiệp vụ,Tên Mục tiêu kiểm soát";
                string Condition = " and [Mảng nghiệp vụ]=N'" + sTenMangNghiepVu + "'";
                bus_Document obj = bus_Document.Instance(mvarUserContext);
                DataSet dsMucTieuKiemSoatByMangNV = obj.getDocumentList(sMucTieuKiemSoat_doctype, DocFields, PropertyFields, Condition);
                DataTable dtMucTieuKiemSoatByMangNV = dsMucTieuKiemSoatByMangNV.Tables[0];
                //loop in list of muctieu kiem soat
                foreach (DataRow rowMucTieu in dtMucTieuKiemSoatByMangNV.Rows)
                {
                    string sTenMucTieuKiemSoat = rowMucTieu["Tên Mục tiêu kiểm soát"].ToString();
                    string sRuiRoType = DOCTYPE.RUIRO_KIEMSOAT;
                    //get cac rui ro by muc tieu kiem soat
                    string DocFieldsRuiRo = "PK_DocumentID,[Mục tiêu kiểm soát],[Tên rủi ro kiểm soát]";
                    string PropertyFieldsRuiRo = "Mục tiêu kiểm soát,Tên rủi ro kiểm soát";
                    string ConditionRuiRo = " and [Mục tiêu kiểm soát]=N'" + sTenMucTieuKiemSoat + "'";
                    DataSet dsRuiRoByMucTieuKS = obj.getDocumentList(sRuiRoType, DocFieldsRuiRo, PropertyFieldsRuiRo, ConditionRuiRo);
                    DataTable dtRuiRoByMucTieuKS = dsRuiRoByMucTieuKS.Tables[0];
                    //loop in list of ruiro
                    foreach (DataRow rowRuiRo in dtRuiRoByMucTieuKS.Rows)
                    {
                        string sTenRuiRo = rowRuiRo["Tên rủi ro kiểm soát"].ToString();
                        string sKiemSoatType = DOCTYPE.KIEMSOAT;
                        //get cac kiem soat by rui ro
                        string DocFieldsKiemSoat = "PK_DocumentID,[Tên rủi ro],[Tên kiểm soát]";
                        string PropertyFieldsKiemSoat = "Tên rủi ro,Tên kiểm soát";
                        string ConditionKiemSoat = " and [Tên rủi ro]=N'" + sTenRuiRo + "'";
                        DataSet dsKiemSoatByRuiRo = obj.getDocumentList(sKiemSoatType, DocFieldsKiemSoat, PropertyFieldsKiemSoat, ConditionKiemSoat);
                        DataTable dtKiemSoatByRuiRo = dsKiemSoatByRuiRo.Tables[0];
                        //loop in list of kiem soat
                        foreach (DataRow rowKiemSoat in dtKiemSoatByRuiRo.Rows)
                        {
                            string sTenKiemSoat = rowKiemSoat["Tên kiểm soát"].ToString();
                            string sThuTucType = DOCTYPE.THUTUC_KIEMTOAN;
                            //get cac kiem soat by rui ro
                            string DocFieldsThuTuc = "PK_DocumentID,[Tên kiểm soát],[Tên thủ tục kiểm toán]";
                            string PropertyFieldsThuTuc = "Tên kiểm soát,Tên thủ tục kiểm toán";
                            string ConditionThuTuc = " and [Tên kiểm soát]=N'" + sTenKiemSoat + "'";
                            DataSet dsThuTucByKiemSoat = obj.getDocumentList(sThuTucType, DocFieldsThuTuc, PropertyFieldsThuTuc, ConditionThuTuc);
                            dtThuTucByKiemSoat.Merge(dsThuTucByKiemSoat.Tables[0]);
                        }
                    }
                }
                DataSet dsResult = new DataSet();
                dsResult.Tables.Add(dtThuTucByKiemSoat);
                return dsResult;
            }
            #endregion
        }
    }

}

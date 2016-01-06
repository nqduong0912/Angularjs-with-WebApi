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
        internal class db_HoSoPhanTichSoBo
        {
            #region Private variables
            private Database _db = null;
            private UserContext mvarUserContext;
            private static db_HoSoPhanTichSoBo mvarInstance;
            private string _error_message = string.Empty;


            #endregion

            #region Constructor
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <returns></returns>
            public static db_HoSoPhanTichSoBo Instance(UserContext usrCTX)
            {
                if (mvarInstance == null)
                    mvarInstance = new db_HoSoPhanTichSoBo(usrCTX);
                return mvarInstance;
            }
            /// <summary>
            /// db_HoSoPhanTichSoBo
            /// </summary>
            /// <param name="usrCTX"></param>
            public db_HoSoPhanTichSoBo(UserContext usrCTX)
            {
                mvarUserContext = usrCTX;
                if (mvarUserContext != null)
                    _db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
                else
                    throw new Exception("UserContext is null");

            }
            /// <summary>
            /// db_HoSoPhanTichSoBo
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <param name="databaseName"></param>
            public db_HoSoPhanTichSoBo(UserContext usrCTX, string databaseName)
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
            /// Lấy DocumentID bởi giá trị truyền vào
            /// </summary>
            /// <param name="PropertyID"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public string GetDocIDByValue(string PropertyID, string value)
            {
                string SQL = "select FK_DOCUMENTID from T_TYPE_DOC_PROPERTY  "
                                + " where TEXTVALUE = N'" + value + "'"
                                + " and FK_PROPERTYID = '" + PropertyID + "' ";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);
                        if (ds != null)
                            return ds.Tables[0].Rows[0]["FK_DOCUMENTID"].ToString();
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return "";
            }
            /// <summary>
            /// Lấy các document của hồ sơ phân tích sơ bộ
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet ChiTietHoSoPhanTichSoBo(string nhomKT)
            {
                string mangNghieVu_propertyID = "AC4286FB-5B38-47C4-93F0-E075F171D8FC";
                string vandeQuanTam_propertyID = "CBA39743-AA0E-4965-9201-BE3BB5CF710A";
                string mucDoRuiRo_propertyID = "05B1B244-C5E3-4199-B523-96A3E131D83D";
                string chitiethssbDOCTYPE = "E0827C65-4C96-4B78-B778-CC49EDECA112";
                string SQL = "select hosochitiet.STATUS, hosochitiet.PK_DOCUMENTID as hoso "
                                + "from T_DOCLINK as hosochitiet_nhom  "
                                + "inner join T_DOCUMENT as hosochitiet "
                                + "on hosochitiet_nhom.FK_DOCUMENTID = hosochitiet.PK_DOCUMENTID "

                                + "where LINKTYPE = 11 "
                                + "and hosochitiet.FK_DOCUMENTTYPEID = '" + chitiethssbDOCTYPE + "' "
                                + "and hosochitiet_nhom.FK_DOCLINKID = '" + nhomKT + "' ";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("mang_nghiep_vu", typeof(String));
                        ds.Tables[0].Columns.Add("van_de_quan_tam", typeof(String));
                        ds.Tables[0].Columns.Add("muc_do_rui_ro", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["mang_nghiep_vu"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), mangNghieVu_propertyID);
                            dtrow["van_de_quan_tam"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), vandeQuanTam_propertyID);
                            dtrow["muc_do_rui_ro"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), mucDoRuiRo_propertyID);
                        }
                        ds.Tables[0].DefaultView.Sort = "mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// lay danh sach cac ho so phan tich rui ro ma nguoi dang nhap phu trach
            /// </summary>
            /// <param name="nhomKT"></param>
            /// <param name="username"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet ChiTietHoSoPhanTichRuiRo(string nhomKT, string username)
            {
                string mangNghieVu_propertyID = "6C1F699A-C48C-4B7D-8EC7-100C6BFC733A";
                string muctieuKiemSoat_propertyID = "1335A457-680D-4503-AD26-D5E8047E4FE8";
                string ruiRo_propertyID = "0995AAA6-A4E8-4648-A548-94DAE8507D2A";
                string xacXuat_propertyID = "26322953-20BB-4D2E-A651-289960B78202";
                string anhHuong_propertyID = "72812D71-8A97-4BCA-B495-E0664DC1B6CF";
                string hosoRuiRo_DOCTYPE = DOCTYPE.CHITIET_HOSO_RUIRO;
                string ruiRoConLai_propertyID = "58307D3D-0507-4D91-8F21-B3AB9BDFC3E4";
                string SQL = "select hosochitiet.STATUS, hosochitiet.PK_DOCUMENTID as hoso "
                                + "from T_DOCLINK as hosochitiet_nhom  "
                                + "inner join T_DOCUMENT as hosochitiet "
                                + "on hosochitiet_nhom.FK_DOCUMENTID = hosochitiet.PK_DOCUMENTID "

                                + "where hosochitiet.CREATEDBY='" + username + "' and LINKTYPE = 11 "
                                + "and hosochitiet.FK_DOCUMENTTYPEID = '" + hosoRuiRo_DOCTYPE + "' "
                                + "and hosochitiet_nhom.FK_DOCLINKID = '" + nhomKT + "' ";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("mang_nghiep_vu", typeof(String));
                        ds.Tables[0].Columns.Add("muc_tieu_kiem_soat", typeof(String));
                        ds.Tables[0].Columns.Add("rui_ro", typeof(String));
                        ds.Tables[0].Columns.Add("xac_suat", typeof(String));
                        ds.Tables[0].Columns.Add("anh_huong", typeof(String));
                        ds.Tables[0].Columns.Add("ruiroid", typeof(String));
                        ds.Tables[0].Columns.Add("rui_ro_con_lai", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["mang_nghiep_vu"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), mangNghieVu_propertyID);
                            dtrow["muc_tieu_kiem_soat"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), muctieuKiemSoat_propertyID);
                            dtrow["rui_ro"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), ruiRo_propertyID);
                            dtrow["ruiroid"] = GetDocIDByValue("33686451-E1A7-4F11-9D8D-A0D697D0D5B6", dtrow["rui_ro"].ToString());
                            //rui_ro_con_lai la Diem Kiem Soat
                            dtrow["rui_ro_con_lai"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), ruiRoConLai_propertyID);
                            dtrow["xac_suat"] = String.IsNullOrEmpty(getPropertyValueOnDocument(dtrow["hoso"].ToString(), xacXuat_propertyID)) ? "0" : getPropertyValueOnDocument(dtrow["hoso"].ToString(), xacXuat_propertyID);
                            dtrow["anh_huong"] = String.IsNullOrEmpty(getPropertyValueOnDocument(dtrow["hoso"].ToString(), anhHuong_propertyID)) ? "0" : getPropertyValueOnDocument(dtrow["hoso"].ToString(), anhHuong_propertyID);
                        }
                        ds.Tables[0].DefaultView.Sort = "mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// lay danh sach cac ho so phan tich rui ro
            /// </summary>
            /// <param name="nhomKT"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet ChiTietHoSoPhanTichRuiRo(string nhomKT)
            {
                string mangNghieVu_propertyID = "6C1F699A-C48C-4B7D-8EC7-100C6BFC733A";
                string muctieuKiemSoat_propertyID = "1335A457-680D-4503-AD26-D5E8047E4FE8";
                string ruiRo_propertyID = "0995AAA6-A4E8-4648-A548-94DAE8507D2A";
                string ruiRoConLai_propertyID = "58307D3D-0507-4D91-8F21-B3AB9BDFC3E4";
                string xacXuat_propertyID = "26322953-20BB-4D2E-A651-289960B78202";
                string anhHuong_propertyID = "72812D71-8A97-4BCA-B495-E0664DC1B6CF";
                string hosoRuiRo_DOCTYPE = DOCTYPE.CHITIET_HOSO_RUIRO;
                string SQL = "select hosochitiet.CREATEDBY,hosochitiet.STATUS, hosochitiet.PK_DOCUMENTID as hoso "
                                + "from T_DOCLINK as hosochitiet_nhom  "
                                + "inner join T_DOCUMENT as hosochitiet "
                                + "on hosochitiet_nhom.FK_DOCUMENTID = hosochitiet.PK_DOCUMENTID "

                                + "where LINKTYPE = 11 "
                                + "and hosochitiet.FK_DOCUMENTTYPEID = '" + hosoRuiRo_DOCTYPE + "' "
                                + "and hosochitiet_nhom.FK_DOCLINKID = '" + nhomKT + "' ";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("mang_nghiep_vu", typeof(String));
                        ds.Tables[0].Columns.Add("muc_tieu_kiem_soat", typeof(String));
                        ds.Tables[0].Columns.Add("rui_ro", typeof(String));
                        ds.Tables[0].Columns.Add("xac_suat", typeof(String));
                        ds.Tables[0].Columns.Add("anh_huong", typeof(String));
                        ds.Tables[0].Columns.Add("ruiroid", typeof(String));
                        ds.Tables[0].Columns.Add("rui_ro_con_lai", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["mang_nghiep_vu"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), mangNghieVu_propertyID);
                            dtrow["muc_tieu_kiem_soat"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), muctieuKiemSoat_propertyID);
                            dtrow["rui_ro"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), ruiRo_propertyID);
                            dtrow["ruiroid"] = GetDocIDByValue("33686451-E1A7-4F11-9D8D-A0D697D0D5B6", dtrow["rui_ro"].ToString());
                            //rui_ro_con_lai la Diem Kiem Soat
                            dtrow["rui_ro_con_lai"] = getPropertyValueOnDocument(dtrow["hoso"].ToString(), ruiRoConLai_propertyID);
                            dtrow["xac_suat"] = String.IsNullOrEmpty(getPropertyValueOnDocument(dtrow["hoso"].ToString(), xacXuat_propertyID)) ? "0" : getPropertyValueOnDocument(dtrow["hoso"].ToString(), xacXuat_propertyID);
                            dtrow["anh_huong"] = String.IsNullOrEmpty(getPropertyValueOnDocument(dtrow["hoso"].ToString(), anhHuong_propertyID)) ? "0" : getPropertyValueOnDocument(dtrow["hoso"].ToString(), anhHuong_propertyID);
                        }
                        ds.Tables[0].DefaultView.Sort = "mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            public string getPropertyValueOnDocument(string documentID, string propertyID)
            {
                bus_Type_Doc_Property objTDP = bus_Type_Doc_Property.Instance(mvarUserContext);
                string svalue = objTDP.GetPropertyValueOnDocument(documentID, propertyID);
                objTDP = null;
                return svalue;
            }
            /// <summary>
            /// lay danh sach mang nghiep vu duoc giao cho nhom
            /// </summary>
            /// <param name="userName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DSMangNghiepVuDuocGiaoChoNhom(string userName)
            {
                string tenNghiepVu_prop = "144DFC1C-5D45-4FE7-8772-CD573CEFD04F";
                string SQL = "select PK_DOCUMENTID as MangNghiepVuID "
                                + "from T_DOCUMENT where PK_DOCUMENTID IN "
                                + "(select addtional_data1 as MangNghiepVuID from T_DOCLINK "
                                + "inner join T_DOCUMENT as nhom "
                                + "on t_doclink.FK_DOCLINKID = nhom.PK_DOCUMENTID "
                                + "where nhom.FK_DOCUMENTTYPEID = '62B9BEBA-57FD-4F19-9FB3-01ACD60AEAB5' "
                                + "and nhom.name = '" + userName + "') ";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("ten_nghiep_vu", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["ten_nghiep_vu"] = getPropertyValueOnDocument(dtrow["MangNghiepVuID"].ToString(), tenNghiepVu_prop);
                        }
                        ds.Tables[0].DefaultView.Sort = "ten_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay danh sach muc do rui ro
            /// </summary>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DSMucDoRuiRo()
            {
                string tenMucDoRuiRo_prop = "F92EA020-7FB3-41CA-A233-8CDFE91D0C77";
                string SQL = "select PK_DOCUMENTID as MucDoRuiRoID "
                                + "from T_DOCUMENT where FK_DOCUMENTTYPEID = '5CB81D04-29BF-4045-B610-5AEA9BE54615'";

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("ten_muc_do_rui_ro", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["ten_muc_do_rui_ro"] = getPropertyValueOnDocument(dtrow["MucDoRuiRoID"].ToString(), tenMucDoRuiRo_prop);
                        }
                        ds.Tables[0].DefaultView.Sort = "ten_muc_do_rui_ro ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            #endregion
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CORE.CoreObjectContext;
using System.Data;
using vpb.app.business.ktnb.Definition.DMS;
using System.Data.SqlClient;
using vpb.app.business.ktnb.CoreBusiness;

namespace vpb.app.business
{
    namespace ktnb.CoreData
    {

        internal class db_Doankiemtoan
        {
            #region Private variables
            private Database _db = null;
            private UserContext mvarUserContext;
            private static db_Doankiemtoan mvarInstance;
            private string _error_message = string.Empty;
            private const int c1 = 5;

            #endregion

            #region Constructor
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <returns></returns>
            public static db_Doankiemtoan Instance(UserContext usrCTX)
            {
                if (mvarInstance == null)
                    mvarInstance = new db_Doankiemtoan(usrCTX);
                return mvarInstance;
            }
            /// <summary>
            /// db_Doankiemtoan
            /// </summary>
            /// <param name="usrCTX"></param>
            public db_Doankiemtoan(UserContext usrCTX)
            {
                mvarUserContext = usrCTX;
                if (mvarUserContext != null)
                    _db = DatabaseFactory.CreateDatabase(usrCTX.DBName);
                else
                    throw new Exception("UserContext is null");

            }
            /// <summary>
            /// db_Doankiemtoan
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <param name="databaseName"></param>
            public db_Doankiemtoan(UserContext usrCTX, string databaseName)
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
            /// Lay danh sach thanh vien boi doan kiem toan
            /// </summary>
            /// <param name="DoanKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachThanhVienDoanKT(string DoanKiemToanID)
            {
                string SQL = "select PK_LINKID,tu.PK_UserID,FK_DOCLINKID,name as username,ug.fullname,ug.GROUPNAME,ug.[Description] "
                    + " from t_doclink as dl inner join t_user as tu on "
                    + " dl.FK_DOCUMENTID = tu.PK_USERID inner join V_USER_GROUP as ug on tu.PK_UserID = ug.PK_USERID "
                    + " where fk_doclinkid = '" + DoanKiemToanID + "'";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }

            /// <summary>
            /// Lay danh sach thanh vien boi nhom kiem toan
            /// </summary>
            /// <param name="DoanKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachThanhVienNhomKT(string NhomKiemToanID)
            {
                string SQL = "select PK_LINKID,tu.PK_UserID,FK_DOCLINKID,ADDTIONAL_DATA1 as MangNghiepVu,name as username,ug.fullname,ug.GROUPNAME,ug.[Description] "
                    + " from t_doclink as dl inner join t_user as tu on "
                    + " dl.FK_DOCUMENTID = tu.PK_USERID inner join V_USER_GROUP as ug on tu.PK_UserID = ug.PK_USERID "
                    + " where fk_doclinkid = '" + NhomKiemToanID + "'";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }

            /// <summary>
            /// Get danh sach cac doan kiem toan ma nguoi dang nhap duoc phu trach
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachDoanKT(string UserName)
            {
                string NAM_PROPERTY = "23FB0D9B-6A02-44B0-A1AA-CEFA895CD9C1";
                string LOAI_DOI_TUONG_PROPERTY = "737694DF-DE17-4FF9-AE15-70E197C83593";
                string TEN_DOT_KIEMTOAN_PROPERTY = "63A0C4B1-2088-4994-B891-2FF65EB20265";
                string DOI_TUONG_KIEMTOAN_PROPERTY = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
                string DON_VI_THUC_HIEN_PROPERTY = "A8A3EDBA-F569-4C06-8A57-045FBFED55FD";
                string QUY_MO_PROPERTY = "06991CFA-158B-4460-8B7E-EC010C14DFE4";
                string THOI_GIAN_DU_KIEN_PROPERTY = "B23A439D-95D3-466E-8AD1-CDDAC260CB27";

                string SQL = "Select dot.STATUS As STATUSDOT, doc.STATUS, dl.FK_DOCLINKID as DotKiemToanID, doc.PK_DOCUMENTID as DoanKiemToanID "
                                + ",doc.NAME,doc.DESCRIPTION From T_DOCUMENT as doc "
                                + "inner join t_doclink as dl on doc.PK_DOCUMENTID = dl.FK_DOCUMENTID "
                                + "inner join T_DOCUMENT as dot on dot.PK_DOCUMENTID = dl.FK_DOCLINKID "
                                + "where dot.FK_DOCUMENTTYPEID = '7AEC7D63-31E1-41AF-B960-42A11117BF1B'"
                                + " and doc.NAME = '" + UserName + "' and  "
                                + " doc.FK_DOCUMENTTYPEID = '" + DOCTYPE.DOAN_KIEMTOAN + "'";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("nam", typeof(String));
                        ds.Tables[0].Columns.Add("loai_doi_tuong", typeof(String));
                        ds.Tables[0].Columns.Add("ten_dot_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("doi_tuong_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("don_vi_thuc_hien", typeof(String));
                        ds.Tables[0].Columns.Add("quy_mo", typeof(String));
                        ds.Tables[0].Columns.Add("thoi_gian_du_kien", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["nam"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), NAM_PROPERTY);
                            dtrow["loai_doi_tuong"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), LOAI_DOI_TUONG_PROPERTY);
                            dtrow["ten_dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), TEN_DOT_KIEMTOAN_PROPERTY);
                            dtrow["doi_tuong_kiem_toan"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), DOI_TUONG_KIEMTOAN_PROPERTY);
                            dtrow["don_vi_thuc_hien"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), DON_VI_THUC_HIEN_PROPERTY);
                            dtrow["quy_mo"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), QUY_MO_PROPERTY);
                            dtrow["thoi_gian_du_kien"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), THOI_GIAN_DU_KIEN_PROPERTY);
                        }
                        ds.Tables[0].DefaultView.Sort = "ten_dot_kiem_toan ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Get danh sách đoàn(đợt) để Ban lãnh đạo duyệt
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachDoanDotForBLD(int TrangThai)
            {
                string NAM_PROPERTY = "23FB0D9B-6A02-44B0-A1AA-CEFA895CD9C1";
                string LOAI_DOI_TUONG_PROPERTY = "737694DF-DE17-4FF9-AE15-70E197C83593";
                string TEN_DOT_KIEMTOAN_PROPERTY = "63A0C4B1-2088-4994-B891-2FF65EB20265";
                string DOI_TUONG_KIEMTOAN_PROPERTY = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
                string DON_VI_THUC_HIEN_PROPERTY = "A8A3EDBA-F569-4C06-8A57-045FBFED55FD";
                string QUY_MO_PROPERTY = "06991CFA-158B-4460-8B7E-EC010C14DFE4";
                string THOI_GIAN_DU_KIEN_PROPERTY = "B23A439D-95D3-466E-8AD1-CDDAC260CB27";

                string SQL = "Select doc.STATUS, dl.FK_DOCLINKID as DotKiemToanID, doc.PK_DOCUMENTID as DoanKiemToanID "
                                + ",doc.NAME,doc.DESCRIPTION From T_DOCUMENT as doc "
                                + "inner join t_doclink as dl on doc.PK_DOCUMENTID = dl.FK_DOCUMENTID "
                                + "inner join T_DOCUMENT as dot on dot.PK_DOCUMENTID = dl.FK_DOCLINKID "
                                + "where dot.STATUS = " + TrangThai + " "
                                + " and doc.FK_DOCUMENTTYPEID = '" + DOCTYPE.DOAN_KIEMTOAN + "'";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("nam", typeof(String));
                        ds.Tables[0].Columns.Add("loai_doi_tuong", typeof(String));
                        ds.Tables[0].Columns.Add("ten_dot_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("doi_tuong_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("don_vi_thuc_hien", typeof(String));
                        ds.Tables[0].Columns.Add("quy_mo", typeof(String));
                        ds.Tables[0].Columns.Add("thoi_gian_du_kien", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["nam"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), NAM_PROPERTY);
                            dtrow["loai_doi_tuong"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), LOAI_DOI_TUONG_PROPERTY);
                            dtrow["ten_dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), TEN_DOT_KIEMTOAN_PROPERTY);
                            dtrow["doi_tuong_kiem_toan"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), DOI_TUONG_KIEMTOAN_PROPERTY);
                            dtrow["don_vi_thuc_hien"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), DON_VI_THUC_HIEN_PROPERTY);
                            dtrow["quy_mo"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), QUY_MO_PROPERTY);
                            dtrow["thoi_gian_du_kien"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), THOI_GIAN_DU_KIEN_PROPERTY);
                        }
                        ds.Tables[0].DefaultView.Sort = "ten_dot_kiem_toan ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }

            /// <summary>
            /// Get danh sách đoàn(đợt) để Ban lãnh đạo duyệt
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachDoanDotForBLD()
            {
                string NAM_PROPERTY = "23FB0D9B-6A02-44B0-A1AA-CEFA895CD9C1";
                string LOAI_DOI_TUONG_PROPERTY = "737694DF-DE17-4FF9-AE15-70E197C83593";
                string TEN_DOT_KIEMTOAN_PROPERTY = "63A0C4B1-2088-4994-B891-2FF65EB20265";
                string DOI_TUONG_KIEMTOAN_PROPERTY = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
                string DON_VI_THUC_HIEN_PROPERTY = "A8A3EDBA-F569-4C06-8A57-045FBFED55FD";
                string QUY_MO_PROPERTY = "06991CFA-158B-4460-8B7E-EC010C14DFE4";
                string THOI_GIAN_DU_KIEN_PROPERTY = "B23A439D-95D3-466E-8AD1-CDDAC260CB27";

                string SQL = "Select dot.STATUS As STATUSDOT, doc.STATUS, dl.FK_DOCLINKID as DotKiemToanID, doc.PK_DOCUMENTID as DoanKiemToanID "
                                + ",doc.NAME,doc.DESCRIPTION From T_DOCUMENT as doc "
                                + "inner join t_doclink as dl on doc.PK_DOCUMENTID = dl.FK_DOCUMENTID "
                                + "inner join T_DOCUMENT as dot on dot.PK_DOCUMENTID = dl.FK_DOCLINKID "
                                + " where doc.FK_DOCUMENTTYPEID = '" + DOCTYPE.DOAN_KIEMTOAN + "'";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("nam", typeof(String));
                        ds.Tables[0].Columns.Add("loai_doi_tuong", typeof(String));
                        ds.Tables[0].Columns.Add("ten_dot_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("doi_tuong_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("don_vi_thuc_hien", typeof(String));
                        ds.Tables[0].Columns.Add("quy_mo", typeof(String));
                        ds.Tables[0].Columns.Add("thoi_gian_du_kien", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["nam"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), NAM_PROPERTY);
                            dtrow["loai_doi_tuong"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), LOAI_DOI_TUONG_PROPERTY);
                            dtrow["ten_dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), TEN_DOT_KIEMTOAN_PROPERTY);
                            dtrow["doi_tuong_kiem_toan"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), DOI_TUONG_KIEMTOAN_PROPERTY);
                            dtrow["don_vi_thuc_hien"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), DON_VI_THUC_HIEN_PROPERTY);
                            dtrow["quy_mo"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), QUY_MO_PROPERTY);
                            dtrow["thoi_gian_du_kien"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), THOI_GIAN_DU_KIEN_PROPERTY);
                        }
                        ds.Tables[0].DefaultView.Sort = "ten_dot_kiem_toan ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }

            /// <summary>
            /// Lay danh sach doan kiem toan boi dot kiem toan
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachDoanKTByDotKT(string DotKiemToanID)
            {
                string TEN_DOT_KIEMTOAN = "63A0C4B1-2088-4994-B891-2FF65EB20265";

                string SQL = "Select dl.FK_DOCLINKID as DotKiemToanID, doc.PK_DOCUMENTID as DoanKiemToanID "
                                + ",doc.NAME,doc.DESCRIPTION From T_DOCUMENT as doc "
                                + "inner join t_doclink as dl on doc.PK_DOCUMENTID = dl.FK_DOCUMENTID "
                                + "where dl.FK_DOCLINKID = '" + DotKiemToanID + "' and  "
                                + "FK_DOCUMENTTYPEID = '" + DOCTYPE.DOAN_KIEMTOAN + "'";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("ten_dot_kiem_toan", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["ten_dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), TEN_DOT_KIEMTOAN);
                        }
                        ds.Tables[0].DefaultView.Sort = "ten_dot_kiem_toan ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// get danh sach cac nhom 
            /// </summary>
            /// <param name="vaitro"></param>
            /// <param name="username"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachNhomKT(string vaitro, string username)
            {
                string TEN_DOT_KIEMTOAN = "63A0C4B1-2088-4994-B891-2FF65EB20265";

                string SQL = "select nhom.STATUS, nhom.PK_DOCUMENTID as NhomKiemToanID, nhom.[NAME] as TruongNhom, nhom.[DESCRIPTION] as TenNhom, "
                                + "doan.PK_DocumentID as DoanKiemToanID,doan.[NAME] as TruongDoan, doan.[DESCRIPTION] as TenDoan, "
                                + "dot.PK_DOCUMENTID as DotKiemToanID "
                                + "from T_DOCUMENT as nhom "
                                + "inner join T_DOCLINK as nhom_doan "
                                + "on nhom.PK_DOCUMENTID = nhom_doan.FK_DOCUMENTID "
                                + "inner join T_DOCUMENT as doan "
                                + "on nhom_doan.FK_DOCLINKID = doan.PK_DOCUMENTID "
                                + "inner join T_DOCLINK as doan_dot "
                                + "on doan.PK_DOCUMENTID = doan_dot.FK_DOCUMENTID "
                                + "inner join T_DOCUMENT as dot "
                                + "on doan_dot.FK_DOCLINKID = dot.PK_DOCUMENTID "
                                + "where nhom.FK_DOCUMENTTYPEID = '62B9BEBA-57FD-4F19-9FB3-01ACD60AEAB5' ";

                if (vaitro == "td")
                {
                    SQL += "and doan.name = '" + username + "'";
                }
                else if (vaitro == "tn")
                {
                    SQL += "and nhom.name = '" + username + "'";
                }
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("ten_dot_kiem_toan", typeof(String));
                        //ds.Tables[0].Columns.Add("ten_doan_kiem_toan", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["ten_dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), TEN_DOT_KIEMTOAN);
                        }
                        ds.Tables[0].DefaultView.Sort = "ten_dot_kiem_toan ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay danh sach mang nghiep vu ma nguoi dang nhap phu trach
            /// </summary>
            /// <param name="username"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachMangNghiepVuPhuTrach(string username)
            {
                string TEN_DOT_KIEMTOAN = "63A0C4B1-2088-4994-B891-2FF65EB20265";
                string TEN_MANG_NGHIEPVU = "144DFC1C-5D45-4FE7-8772-CD573CEFD04F";
                string SQL = "select dot.PK_DOCUMENTID as DotKiemToanID , "
                                + "doan.PK_DocumentID as DoanKiemToanID,doan.[NAME] as TruongDoan, doan.[DESCRIPTION] as TenDoan, "
                                + "tuser.PK_UserID, tuser.name as ThanhVien, nhom.[NAME] as TruongNhom, nhom.[DESCRIPTION] as TenNhom, "
                                + "user_nhom.ADDTIONAL_DATA1 as MangNghiepVuID "
                                + "from T_USER as tuser "
                                + "inner join T_DOCLINK as user_nhom "
                                + "on tuser.PK_UserID = user_nhom.FK_DOCUMENTID "
                                + "inner join T_DOCUMENT as nhom "
                                + "on user_nhom.FK_DOCLINKID = nhom.PK_DOCUMENTID "
                                + "inner join T_DOCLINK as nhom_doan "
                                + "on nhom.PK_DOCUMENTID = nhom_doan.FK_DOCUMENTID "
                                + "inner join T_DOCUMENT as doan "
                                + "on nhom_doan.FK_DOCLINKID = doan.PK_DOCUMENTID "
                                + "inner join T_DOCLINK as doan_dot "
                                + "on doan.PK_DOCUMENTID = doan_dot.FK_DOCUMENTID "
                                + "inner join T_DOCUMENT as dot  "
                                + "on doan_dot.FK_DOCLINKID = dot.PK_DOCUMENTID "
                                + "where nhom.FK_DOCUMENTTYPEID = '62B9BEBA-57FD-4F19-9FB3-01ACD60AEAB5' "
                                + "and tuser.name= '" + username + "'";

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("ten_dot_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("ten_mang_nghiep_vu", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["ten_dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["DotKiemToanID"].ToString(), TEN_DOT_KIEMTOAN);
                            dtrow["ten_mang_nghiep_vu"] = getPropertyValueOnDocument(dtrow["MangNghiepVuID"].ToString(), TEN_MANG_NGHIEPVU);
                        }
                        ds.Tables[0].DefaultView.Sort = "ten_dot_kiem_toan ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay thong tin dot kiem toan boi nhom kiem toan
            /// </summary>
            /// <param name="nhomkt"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDotKTByNhomKT(string nhomkt)
            {
                string TEN_DOT_KIEMTOAN = "63A0C4B1-2088-4994-B891-2FF65EB20265";
                string DOI_TUONG_KIEM_TOAN = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
                string SQL = "select nhom.PK_DOCUMENTID as nhomkt, "
                                + "doan.PK_DOCUMENTID as doankt, "
                                + "doan_dot.FK_DOCLINKID as dotkt, dot.STATUS as DotktStatus "
                                + "from T_DOCUMENT as nhom "
                                + "inner join T_DOCLINK as nhom_doan "
                                + "on nhom.PK_DOCUMENTID = nhom_doan.FK_DOCUMENTID "
                                + "inner join T_DOCUMENT as doan "
                                + "on nhom_doan.FK_DOCLINKID = doan.PK_DOCUMENTID "
                                + "inner join T_DOCLINK as doan_dot "
                                + "on doan.PK_DOCUMENTID = doan_dot.FK_DOCUMENTID "
                                + "inner join T_DOCUMENT as dot "
                                + "on doan_dot.FK_DOCLINKID = dot.PK_DOCUMENTID "

                                + "where nhom.FK_DOCUMENTTYPEID = '62B9BEBA-57FD-4F19-9FB3-01ACD60AEAB5' "
                                + "and nhom.PK_DOCUMENTID = '" + nhomkt + "'";

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("ten_dot_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("doi_tuong_kiem_toan", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["ten_dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["dotkt"].ToString(), TEN_DOT_KIEMTOAN);
                            dtrow["doi_tuong_kiem_toan"] = getPropertyValueOnDocument(dtrow["dotkt"].ToString(), DOI_TUONG_KIEM_TOAN);
                        }
                        ds.Tables[0].DefaultView.Sort = "ten_dot_kiem_toan ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay danh sach mang nghiep vu boi nhom kiem toan
            /// </summary>
            /// <param name="nhomkt"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetMangNVByNhomKT(string nhomkt)
            {
                string TEN_MANG_NGHIEP_VU = "144DFC1C-5D45-4FE7-8772-CD573CEFD04F";
                string SQL = "select thanhvien_nhom.FK_DOCUMENTID as UserID, nhom.PK_DOCUMENTID as nhomkt, "
                                + "thanhvien_nhom.addtional_data1 as mangnv "
                                + "from T_DOCUMENT as nhom "
                                + "inner join T_DOCLINK as thanhvien_nhom "
                                + "on nhom.PK_DOCUMENTID = thanhvien_nhom.FK_DOCLINKID "

                                + "where LINKTYPE = 13 "
                                + "and nhom.FK_DOCUMENTTYPEID = '62B9BEBA-57FD-4F19-9FB3-01ACD60AEAB5' "
                                + "and nhom.PK_DOCUMENTID = '" + nhomkt + "'";

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("ten_mang_nghiep_vu", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["ten_mang_nghiep_vu"] = getPropertyValueOnDocument(dtrow["mangnv"].ToString(), TEN_MANG_NGHIEP_VU);
                        }
                        ds.Tables[0].DefaultView.Sort = "ten_mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="nhomkt"></param>
            /// <returns></returns>
            /// author:quangna
            public DataSet GetAllMangNghiepVuDocLink()
            {
                string SQL = "SELECT T_DocLink.*,T_DOCUMENT.Name As TruongNhom FROM T_DocLink " +
                             " inner join T_Document on T_DocLink.FK_DocLinkID = T_DOCUMENT.PK_DocumentID " +
                             " where  isnull(ADDTIONAL_DATA1,'')!='' ";

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay danh sach nhom boi Doan Kiem Toan
            /// </summary>
            /// <param name="DoanKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachNhomByDoanKT(string DoanKiemToanID)
            {
                string SQL = "select nhom_doan.FK_DOCUMENTID as NhomID " +
                                " from T_DOCLINK as nhom_doan " +
                             " inner join T_DOCUMENT as nhom on nhom_doan.FK_DOCUMENTID = nhom.PK_DOCUMENTID " +
                             " where nhom_doan.FK_DOCLINKID = '" + DoanKiemToanID + "' " +
                             " and nhom.FK_DOCUMENTTYPEID = '62B9BEBA-57FD-4F19-9FB3-01ACD60AEAB5' ";

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay thong tin dot kiem toan boi cong viec
            /// </summary>
            /// <param name="DoanKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDotInfoByCongViec(string congviecID)
            {
                string TEN_DOT_KIEM_TOAN = "63A0C4B1-2088-4994-B891-2FF65EB20265";
                string DOI_TUONG_KIEM_TOAN = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
                string TEN_CONG_VIEC = "470105E3-B810-4982-A8EF-74E367441EBD";
                string SQL = "select dot.PK_DOCUMENTID as dotid, doan.PK_DOCUMENTID as doanid,  " +
                                " doan.[NAME],congviec.PK_DOCUMENTID as congviecid from T_DOCUMENT as congviec " +
                             " inner join T_DOCLINK as congviec_doan  " +
                             " on congviec.PK_DOCUMENTID = congviec_doan.FK_DOCUMENTID  " +

                             " inner join T_DOCUMENT as doan " +
                             " on congviec_doan.FK_DOCLINKID = doan.PK_DOCUMENTID " +

                             " inner join T_DOCLINK as doan_dot " +
                             " on doan.PK_DOCUMENTID = doan_dot.FK_DOCUMENTID " +

                             " inner join T_DOCUMENT as dot " +
                             " on doan_dot.FK_DOCLINKID = dot.PK_DOCUMENTID " +

                             " where congviec.PK_DOCUMENTID = '" + congviecID + "' " +
                             " and congviec.FK_DOCUMENTTYPEID = 'C0EB39DA-11B7-4507-B401-0CD9E0AE557B' ";

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("doi_tuong_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("dot_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("ten_cong_viec", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["doi_tuong_kiem_toan"] = getPropertyValueOnDocument(dtrow["dotid"].ToString(), DOI_TUONG_KIEM_TOAN);
                            dtrow["dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["dotid"].ToString(), TEN_DOT_KIEM_TOAN);
                            dtrow["ten_cong_viec"] = getPropertyValueOnDocument(dtrow["congviecid"].ToString(), TEN_CONG_VIEC);
                        }
                        //ds.Tables[0].DefaultView.Sort = "ten_mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay thong tin dot kiem toan boi phat hien
            /// </summary>
            /// <param name="PhatHienID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDotInfoByPhatHien(string PhatHienID)
            {
                string TEN_DOT_KIEM_TOAN = "63A0C4B1-2088-4994-B891-2FF65EB20265";
                string DOI_TUONG_KIEM_TOAN = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
                string TEN_CONG_VIEC = "470105E3-B810-4982-A8EF-74E367441EBD";
                string SQL = "select dot.PK_DOCUMENTID as dotid, doan.PK_DOCUMENTID as doanid,   doan.[NAME], " +
                                "congviec.PK_DOCUMENTID as congviecid, phathien.PK_DOCUMENTID as phathienid  " +
                             " from  T_DOCUMENT as phathien   " +

                             " inner join T_DOCLINK as phathien_congviec  " +
                             " on phathien.PK_DOCUMENTID = phathien_congviec.FK_DOCUMENTID  " +

                              " inner join T_DOCUMENT as congviec   " +
                             " on phathien_congviec.FK_DOCLINKID = congviec.PK_DOCUMENTID " +

                             " inner join T_DOCLINK as congviec_doan  " +
                             " on congviec.PK_DOCUMENTID = congviec_doan.FK_DOCUMENTID  " +

                             " inner join T_DOCUMENT as doan " +
                             " on congviec_doan.FK_DOCLINKID = doan.PK_DOCUMENTID " +

                             " inner join T_DOCLINK as doan_dot " +
                             " on doan.PK_DOCUMENTID = doan_dot.FK_DOCUMENTID " +

                             " inner join T_DOCUMENT as dot " +
                             " on doan_dot.FK_DOCLINKID = dot.PK_DOCUMENTID " +
                             " where phathien.PK_DOCUMENTID = '" + PhatHienID + "' ";


                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("doi_tuong_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("dot_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("ten_cong_viec", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["doi_tuong_kiem_toan"] = getPropertyValueOnDocument(dtrow["dotid"].ToString(), DOI_TUONG_KIEM_TOAN);
                            dtrow["dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["dotid"].ToString(), TEN_DOT_KIEM_TOAN);
                            dtrow["ten_cong_viec"] = getPropertyValueOnDocument(dtrow["congviecid"].ToString(), TEN_CONG_VIEC);
                        }
                        //ds.Tables[0].DefaultView.Sort = "ten_mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// lay danh sach phat hien boi cong viec
            /// </summary>
            /// <param name="congviecID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDanhSachPhatHien(string congviecID)
            {
                string PHAT_HIEN = "E48E3FFD-FE56-4B14-AC36-2C036873E1CD";
                string MUC_DO = "9AD08FC3-5CEE-41E0-9A72-73822CFB2F68";
                string CHI_TIET = "CEED9D99-A89C-4127-9F9A-BD149E0BBC58";
                string NGUYEN_NHAN = "3C648C41-DA03-48AB-AAD5-143D9D959C00";
                string ANH_HUONG = "2CD4A9CF-D1AC-4675-87A3-739AF5344AD8";
                string KHUYEN_NGHI = "6A1B7B08-BE4E-400A-A6CC-EC7897A89031";
                string GHI_CHU = "876310DC-68CE-43E2-A040-88D169B59D34";

                string SQL = "select phathien.PK_DOCUMENTID as phathienid from T_DOCUMENT as phathien  " +

                             " inner join T_DOCLINK as phathien_congviec " +
                             " on phathien.PK_DOCUMENTID = phathien_congviec.FK_DOCUMENTID " +

                             " where phathien_congviec.FK_DOCLINKID = '" + congviecID + "' " +
                             " and phathien.FK_DOCUMENTTYPEID =  'DB4C7D3D-130D-4FFE-8E84-5F1344CE9D6E' ";


                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("phat_hien", typeof(String));
                        ds.Tables[0].Columns.Add("muc_do", typeof(String));
                        ds.Tables[0].Columns.Add("chi_tiet", typeof(String));
                        ds.Tables[0].Columns.Add("nguyen_nhan", typeof(String));
                        ds.Tables[0].Columns.Add("anh_huong", typeof(String));
                        ds.Tables[0].Columns.Add("khuyen_nghi", typeof(String));
                        ds.Tables[0].Columns.Add("ghi_chu", typeof(String));
                        ds.Tables[0].Columns.Add("loai", typeof(String));

                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["phat_hien"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), PHAT_HIEN);
                            dtrow["muc_do"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), MUC_DO);
                            dtrow["chi_tiet"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), CHI_TIET);
                            dtrow["nguyen_nhan"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), NGUYEN_NHAN);
                            dtrow["anh_huong"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), ANH_HUONG);
                            dtrow["khuyen_nghi"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), KHUYEN_NGHI);
                            dtrow["ghi_chu"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), GHI_CHU);
                            dtrow["loai"] = "hethong";
                        }
                        //ds.Tables[0].DefaultView.Sort = "ten_mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay danh sach phat hien boi UserType
            /// </summary>
            /// <param name="congviecID"></param>
            /// <param name="UserType"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDanhSachPhatHienByUserType(string congviecID, string UserType)
            {
                string PHAT_HIEN = "E48E3FFD-FE56-4B14-AC36-2C036873E1CD";
                string MUC_DO = "9AD08FC3-5CEE-41E0-9A72-73822CFB2F68";
                string CHI_TIET = "CEED9D99-A89C-4127-9F9A-BD149E0BBC58";
                string NGUYEN_NHAN = "3C648C41-DA03-48AB-AAD5-143D9D959C00";
                string ANH_HUONG = "2CD4A9CF-D1AC-4675-87A3-739AF5344AD8";
                string KHUYEN_NGHI = "6A1B7B08-BE4E-400A-A6CC-EC7897A89031";
                string GHI_CHU = "876310DC-68CE-43E2-A040-88D169B59D34";
                string NHAN_XET = "17FE4A62-A92E-4921-A9DA-C7C7110EB8B3";

                string SQL = "select phathien.STATUS, phathien.PK_DOCUMENTID as phathienid from T_DOCUMENT as phathien  " +

                             " inner join T_DOCLINK as phathien_congviec " +
                             " on phathien.PK_DOCUMENTID = phathien_congviec.FK_DOCUMENTID " +

                             " where phathien_congviec.FK_DOCLINKID = '" + congviecID + "' " +
                             " and phathien.FK_DOCUMENTTYPEID =  'DB4C7D3D-130D-4FFE-8E84-5F1344CE9D6E' ";
                if (UserType.Equals("nguoiduyet1"))
                {
                    SQL += " and (phathien.STATUS = 2 or phathien.STATUS = 4 or phathien.STATUS = 16)";
                }
                else if (UserType.Equals("nguoiduyet2"))
                {
                    SQL += " and (phathien.STATUS = 4 or phathien.STATUS = 16)";
                }

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("phat_hien", typeof(String));
                        ds.Tables[0].Columns.Add("muc_do", typeof(String));
                        ds.Tables[0].Columns.Add("chi_tiet", typeof(String));
                        ds.Tables[0].Columns.Add("nguyen_nhan", typeof(String));
                        ds.Tables[0].Columns.Add("anh_huong", typeof(String));
                        ds.Tables[0].Columns.Add("khuyen_nghi", typeof(String));
                        ds.Tables[0].Columns.Add("ghi_chu", typeof(String));
                        ds.Tables[0].Columns.Add("nhan_xet", typeof(String));
                        ds.Tables[0].Columns.Add("loai", typeof(String));

                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["phat_hien"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), PHAT_HIEN);
                            dtrow["muc_do"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), MUC_DO);
                            dtrow["chi_tiet"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), CHI_TIET);
                            dtrow["nguyen_nhan"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), NGUYEN_NHAN);
                            dtrow["anh_huong"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), ANH_HUONG);
                            dtrow["khuyen_nghi"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), KHUYEN_NGHI);
                            dtrow["ghi_chu"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), GHI_CHU);
                            dtrow["nhan_xet"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), NHAN_XET);
                            dtrow["loai"] = "hethong";
                        }
                        //ds.Tables[0].DefaultView.Sort = "ten_mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay danh sach phat hien vi pham
            /// </summary>
            /// <param name="congviecID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDanhSachPhatHienViPham(string congviecID)
            {
                string PHAT_HIEN = "D5637BAF-54F3-4724-9E3B-8543EB93509A";
                string MUC_DO = "A4C22F5B-2271-46D8-AD8D-B88B7399317A";
                string CHI_TIET = "10A26FD0-3309-4E1E-B83B-AC55E8194C2F";
                string NGUYEN_NHAN = "0480690F-6373-4AC3-BA8C-FF61AA8763B7";
                string ANH_HUONG = "5D74DB08-2EFD-47DC-8970-0C015F89F05D";
                string KHUYEN_NGHI = "06271BD7-653E-4EF4-826F-991A408A2074";
                string GHI_CHU = "483F188D-3325-4EAB-8A9E-24B187EA5BE1";
                string NHAN_XET = "75AF3ED2-78F6-4DAE-BB20-157920BAC865";

                string SQL = "select phathien.PK_DOCUMENTID as phathienid from T_DOCUMENT as phathien  " +

                             " inner join T_DOCLINK as phathien_congviec " +
                             " on phathien.PK_DOCUMENTID = phathien_congviec.FK_DOCUMENTID " +

                             " where phathien_congviec.FK_DOCLINKID = '" + congviecID + "' " +
                             " and phathien.FK_DOCUMENTTYPEID =  'AF682797-B702-49C4-961F-B503CA2FB403' ";


                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("phat_hien", typeof(String));
                        ds.Tables[0].Columns.Add("muc_do", typeof(String));
                        ds.Tables[0].Columns.Add("chi_tiet", typeof(String));
                        ds.Tables[0].Columns.Add("nguyen_nhan", typeof(String));
                        ds.Tables[0].Columns.Add("anh_huong", typeof(String));
                        ds.Tables[0].Columns.Add("khuyen_nghi", typeof(String));
                        ds.Tables[0].Columns.Add("ghi_chu", typeof(String));
                        ds.Tables[0].Columns.Add("nhan_xet", typeof(String));
                        ds.Tables[0].Columns.Add("loai", typeof(String));

                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["phat_hien"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), PHAT_HIEN);
                            dtrow["muc_do"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), MUC_DO);
                            dtrow["chi_tiet"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), CHI_TIET);
                            dtrow["nguyen_nhan"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), NGUYEN_NHAN);
                            dtrow["anh_huong"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), ANH_HUONG);
                            dtrow["khuyen_nghi"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), KHUYEN_NGHI);
                            dtrow["ghi_chu"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), GHI_CHU);
                            dtrow["nhan_xet"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), NHAN_XET);
                            dtrow["loai"] = "vipham";

                        }
                        //ds.Tables[0].DefaultView.Sort = "ten_mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay danh sach phat hien vi pham boi UserType
            /// </summary>
            /// <param name="congviecID"></param>
            /// <param name="UserType"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDanhSachPhatHienViPhamByUserType(string congviecID, string UserType)
            {
                string PHAT_HIEN = "D5637BAF-54F3-4724-9E3B-8543EB93509A";
                string MUC_DO = "A4C22F5B-2271-46D8-AD8D-B88B7399317A";
                string CHI_TIET = "10A26FD0-3309-4E1E-B83B-AC55E8194C2F";
                string NGUYEN_NHAN = "0480690F-6373-4AC3-BA8C-FF61AA8763B7";
                string ANH_HUONG = "5D74DB08-2EFD-47DC-8970-0C015F89F05D";
                string KHUYEN_NGHI = "06271BD7-653E-4EF4-826F-991A408A2074";
                string GHI_CHU = "483F188D-3325-4EAB-8A9E-24B187EA5BE1";
                string NHAN_XET = "75AF3ED2-78F6-4DAE-BB20-157920BAC865";

                string SQL = "select  phathien.STATUS,phathien.PK_DOCUMENTID as phathienid from T_DOCUMENT as phathien  " +

                             " inner join T_DOCLINK as phathien_congviec " +
                             " on phathien.PK_DOCUMENTID = phathien_congviec.FK_DOCUMENTID " +

                             " where phathien_congviec.FK_DOCLINKID = '" + congviecID + "' " +
                             " and phathien.FK_DOCUMENTTYPEID =  'AF682797-B702-49C4-961F-B503CA2FB403' ";
                if (UserType.Equals("nguoiduyet1"))
                {
                    SQL += " and (phathien.STATUS = 2 or phathien.STATUS = 4  or phathien.STATUS = 16)";
                }
                else if (UserType.Equals("nguoiduyet2"))
                {
                    SQL += " and (phathien.STATUS = 4 or phathien.STATUS = 16)";
                }


                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("phat_hien", typeof(String));
                        ds.Tables[0].Columns.Add("muc_do", typeof(String));
                        ds.Tables[0].Columns.Add("chi_tiet", typeof(String));
                        ds.Tables[0].Columns.Add("nguyen_nhan", typeof(String));
                        ds.Tables[0].Columns.Add("anh_huong", typeof(String));
                        ds.Tables[0].Columns.Add("khuyen_nghi", typeof(String));
                        ds.Tables[0].Columns.Add("ghi_chu", typeof(String));
                        ds.Tables[0].Columns.Add("nhan_xet", typeof(String));
                        ds.Tables[0].Columns.Add("loai", typeof(String));

                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["phat_hien"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), PHAT_HIEN);
                            dtrow["muc_do"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), MUC_DO);
                            dtrow["chi_tiet"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), CHI_TIET);
                            dtrow["nguyen_nhan"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), NGUYEN_NHAN);
                            dtrow["anh_huong"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), ANH_HUONG);
                            dtrow["khuyen_nghi"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), KHUYEN_NGHI);
                            dtrow["ghi_chu"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), GHI_CHU);
                            dtrow["nhan_xet"] = getPropertyValueOnDocument(dtrow["phathienid"].ToString(), NHAN_XET);
                            dtrow["loai"] = "vipham";

                        }
                        //ds.Tables[0].DefaultView.Sort = "ten_mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay danh sach phan hoi boi phat hien
            /// </summary>
            /// <param name="phatHienID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDanhSachPhanHoi(string phatHienID)
            {
                string SQL = "select phanhoi.CREATEDDATETIME as ngay_gio, phanhoi.createdby as nguoi_nhap, phanhoi.status, " +
                             " phanhoi.PK_DOCUMENTID as phanhoiid, phanhoi.[DESCRIPTION] as NoiDung " +
                             " from T_DOCUMENT as phanhoi  " +
                             " inner join T_DOCLINK as phanhoi_phathien " +
                             " on phanhoi.PK_DOCUMENTID = phanhoi_phathien.FK_DOCUMENTID " +

                             " where phanhoi_phathien.FK_DOCLINKID = '" + phatHienID + "' ";
                //" and phathien.FK_DOCUMENTTYPEID =  'DB4C7D3D-130D-4FFE-8E84-5F1344CE9D6E' ";


                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }

            /// <summary>
            /// danh sach phan hoi theo usertype
            /// </summary>
            /// <param name="phatHienID">id phat hien</param>
            /// <param name="UserType">nguoithuchien,nguoiduyet1,nguoiduyet2</param>
            /// <returns></returns>
            /// <author>quangna</author>
            public DataSet GetDanhSachPhanHoi(string phatHienID, string UserType)
            {
                string SQL = "select phanhoi.CREATEDDATETIME as ngay_gio, phanhoi.createdby as nguoi_nhap, phanhoi.status, " +
                             " phanhoi.PK_DOCUMENTID as phanhoiid, phanhoi.[DESCRIPTION] as NoiDung " +
                             " from T_DOCUMENT as phanhoi  " +
                             " inner join T_DOCLINK as phanhoi_phathien " +
                             " on phanhoi.PK_DOCUMENTID = phanhoi_phathien.FK_DOCUMENTID " +

                             " where phanhoi_phathien.FK_DOCLINKID = '" + phatHienID + "' ";
                if (UserType.Equals("nguoiduyet1"))
                {
                    SQL += " and (phanhoi.STATUS = 2 or phanhoi.STATUS = 4 or phanhoi.STATUS = 16)";
                }
                else if (UserType.Equals("nguoiduyet2"))
                {
                    SQL += " and (phanhoi.STATUS = 4 or phanhoi.STATUS = 16)";
                }

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay thong tin dot kiem toan boi phan hoi
            /// </summary>
            /// <param name="PhanHoiID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDotInfoByPhanHoi(string PhanHoiID)
            {
                string TEN_DOT_KIEM_TOAN = "63A0C4B1-2088-4994-B891-2FF65EB20265";
                string DOI_TUONG_KIEM_TOAN = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
                string TEN_CONG_VIEC = "470105E3-B810-4982-A8EF-74E367441EBD";
                //string PHAT_HIEN = "470105E3-B810-4982-A8EF-74E367441EBD";
                string SQL = "select phanhoi.status,phanhoi.[DESCRIPTION] as NoiDung, phanhoi_phathien.FK_DOCLINKID as phanhoiid, dot.PK_DOCUMENTID as dotid, doan.PK_DOCUMENTID as doanid,   doan.[NAME], " +
                                "congviec.PK_DOCUMENTID as congviecid, phathien.PK_DOCUMENTID as phathienid  " +
                             " from  T_DOCUMENT as phathien   " +

                             " inner join T_DOCLINK as phathien_congviec  " +
                             " on phathien.PK_DOCUMENTID = phathien_congviec.FK_DOCUMENTID  " +

                              " inner join T_DOCUMENT as congviec   " +
                             " on phathien_congviec.FK_DOCLINKID = congviec.PK_DOCUMENTID " +

                             " inner join T_DOCLINK as congviec_doan  " +
                             " on congviec.PK_DOCUMENTID = congviec_doan.FK_DOCUMENTID  " +

                             " inner join T_DOCUMENT as doan " +
                             " on congviec_doan.FK_DOCLINKID = doan.PK_DOCUMENTID " +

                             " inner join T_DOCLINK as doan_dot " +
                             " on doan.PK_DOCUMENTID = doan_dot.FK_DOCUMENTID " +

                             " inner join T_DOCUMENT as dot " +
                             " on doan_dot.FK_DOCLINKID = dot.PK_DOCUMENTID " +

                             " inner join T_DOCLINK as phanhoi_phathien " +
                             " on phathien.PK_DOCUMENTID = phanhoi_phathien.FK_DOCLINKID " +

                             " inner join T_DOCUMENT as phanhoi " +
                             " on phanhoi.PK_DOCUMENTID = phanhoi_phathien.FK_DOCUMENTID " +
                             " where phanhoi.PK_DOCUMENTID = '" + PhanHoiID + "' ";


                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("doi_tuong_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("dot_kiem_toan", typeof(String));
                        ds.Tables[0].Columns.Add("ten_cong_viec", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["doi_tuong_kiem_toan"] = getPropertyValueOnDocument(dtrow["dotid"].ToString(), DOI_TUONG_KIEM_TOAN);
                            dtrow["dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["dotid"].ToString(), TEN_DOT_KIEM_TOAN);
                            dtrow["ten_cong_viec"] = getPropertyValueOnDocument(dtrow["congviecid"].ToString(), TEN_CONG_VIEC);
                        }
                        //ds.Tables[0].DefaultView.Sort = "ten_mang_nghiep_vu ASC";
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// lay danh sach file boi Document
            /// </summary>
            /// <param name="DocID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetFilesByDoc(string DocID)
            {
                string SQL = "select PK_docversionbodyid as FileID, FILEPATH, FILENAME,Description, FK_DOCUMENTID "
                    + "as DocID,DisplayFileName from T_DOC_VERSION_BODY "
                    + "where FK_DOCUMENTID= '" + DocID + "'";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lấy các phát hiện bởi mảng nghiệp vụ trong đợt kiểm toán
            /// </summary>
            /// <param name="DotKiemToanID"></param>
            /// <returns></returns>
            public DataSet GetPhatHienByMangNvInDotKT(string DotKiemToanID)
            {
                string SQL = "select distinct dot.PK_DOCUMENTID as DotID, phathien_congviec.FK_DOCLINKID as CongViecID " +
                                ", thutuc_congviec.ADDTIONAL_DATA1 as MangNghiepVuID  " +
                                ",phathien_congviec.FK_DOCUMENTID as PhatHienID " +
                             " from T_DOCUMENT as dot " +

                             " inner join T_DOCLINK as thutuc_congviec  " +
                             " on  thutuc_congviec.ADDTIONAL_DATA2 = convert(varchar(50), dot.PK_DOCUMENTID) " +

                              " inner join T_DOCLINK as phathien_congviec  " +
                             " on thutuc_congviec.FK_DOCLINKID = phathien_congviec.FK_DOCLINKID " +

                             " inner join T_DOCUMENT as phathien  " +
                             " on phathien_congviec.FK_DOCUMENTID = phathien.PK_DOCUMENTID " +

                             " where phathien.FK_DOCUMENTTYPEID = 'DB4C7D3D-130D-4FFE-8E84-5F1344CE9D6E' " +
                             " and dot.PK_DOCUMENTID = '" + DotKiemToanID + "' ";

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }
            /// <summary>
            /// Lay cac dot kiem toan ma nguoi dang nhap la truong doan
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDotKTCuaTruongDoan(string UserName)
            {
                string SQL = "select dot.PK_DOCUMENTID as DotID " +
                             " from T_DOCLINK as doan_dot " +

                             " inner join T_DOCUMENT as doan " +
                             " on doan.PK_DOCUMENTID = doan_dot.FK_DOCUMENTID " +

                             " inner join T_DOCUMENT as dot " +
                             " on doan_dot.FK_DOCLINKID = dot.PK_DOCUMENTID " +

                             " where doan.NAME = '" + UserName + "' " +
                             " and doan.FK_DOCUMENTTYPEID = '91845A1B-2BFA-4153-A661-2B5D43963FA4' ";
                //" and dot.STATUS >=21 and dot.STATUS <=26 ";
                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);
                    }
                    catch (Exception ex)
                    {
                        _error_message = ex.Message;
                    }
                }
                return ds;
            }

            /// <summary>
            /// Lay thong tin thutuc boi cong viec
            /// </summary>
            /// <param name="DoanKiemToanID"></param>
            /// <returns></returns>
            /// <author>quangna</author>
            public DataSet GetThuTucInfoByCongViec(string congviecID)
            {
                string ten_cong_viec = "470105E3-B810-4982-A8EF-74E367441EBD";
                string ten_thu_tuc = "98F450F3-824E-4BBD-9F9D-6C9845FD8186";
                string ten_mang_nghiep_vu = "144DFC1C-5D45-4FE7-8772-CD573CEFD04F";
                string SQL = "Select FK_DocumentID as thutuc_id,FK_DocLinkID as congviec_id,ADDTIONAL_DATA1 as mangnghiepvu_id " +
                " from T_DOCLINK where FK_DocLinkID ='" + congviecID + "'";

                DataSet ds = new DataSet();
                using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                {
                    try
                    {
                        objCon.Open();
                        ds = _db.ExecuteDataSet(CommandType.Text, SQL);

                        ds.Tables[0].Columns.Add("ten_thu_tuc", typeof(String));
                        ds.Tables[0].Columns.Add("ten_cong_viec", typeof(String));
                        ds.Tables[0].Columns.Add("ten_mang_nghiep_vu", typeof(String));
                        foreach (DataRow dtrow in ds.Tables[0].Rows)
                        {
                            dtrow["ten_thu_tuc"] = getPropertyValueOnDocument(dtrow["thutuc_id"].ToString(), ten_thu_tuc);
                            dtrow["ten_cong_viec"] = getPropertyValueOnDocument(dtrow["congviec_id"].ToString(), ten_cong_viec);
                            dtrow["ten_mang_nghiep_vu"] = getPropertyValueOnDocument(dtrow["mangnghiepvu_id"].ToString(), ten_mang_nghiep_vu);
                        }

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
            #endregion

            #region update T_DOC_VERSION_BODY

            /// <summary>
            /// update document version body
            /// </summary>
            /// <param name="PK_DOCVERSIONBODYID"></param>
            /// <param name="FK_DOCUMENTID"></param>
            /// <param name="FILENAME"></param>
            /// <param name="FILEPATH"></param>
            /// <param name="DISPLAYFILENAME"></param>
            /// <returns></returns>
            /// <author>quangna</author>
            public int AddDocVersionBody(string PK_DOCVERSIONBODYID, string FK_DOCUMENTID, string FILENAME, string FILEPATH, string DISPLAYFILENAME)
            {
                try
                {
                    using (System.Data.Common.DbConnection objCon = _db.CreateConnection())
                    {

                        string connectionString = objCon.ConnectionString;
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string strInsert = "INSERT INTO T_DOC_VERSION_BODY VALUES(" +
                                                "@PK_DOCVERSIONBODYID, @FK_DOCUMENTTYPEID, @FK_DOCVERSIONID,@FK_DOCUMENTID,@NUMBERBODY,@FILENAME,@FILEPATH," +
                                                "@CREATEDDATE,@ISCHECKOUT,@DISPLAYFILENAME,@ISLOCK,@CHECKINGOUTBYUSER,@DESCRIPTION)";
                            using (SqlCommand cmd = new SqlCommand(strInsert, conn))
                            {
                                cmd.Parameters.AddWithValue("@PK_DOCVERSIONBODYID", PK_DOCVERSIONBODYID);
                                cmd.Parameters.AddWithValue("@FK_DOCUMENTTYPEID", null);
                                cmd.Parameters.AddWithValue("@FK_DOCVERSIONID", null);
                                cmd.Parameters.AddWithValue("@FK_DOCUMENTID", FK_DOCUMENTID);
                                cmd.Parameters.AddWithValue("@NUMBERBODY", null);

                                cmd.Parameters.AddWithValue("@FILENAME", FILENAME);
                                cmd.Parameters.AddWithValue("@FILEPATH", FILEPATH);
                                cmd.Parameters.AddWithValue("@CREATEDDATE", DateTime.Now);
                                cmd.Parameters.AddWithValue("@ISCHECKOUT", null);
                                cmd.Parameters.AddWithValue("@DISPLAYFILENAME", DISPLAYFILENAME);
                                cmd.Parameters.AddWithValue("@ISLOCK", false);
                                cmd.Parameters.AddWithValue("@CHECKINGOUTBYUSER", null);
                                cmd.Parameters.AddWithValue("@DESCRIPTION", null);
                                int rows = cmd.ExecuteNonQuery();
                                return rows;
                                //rows number of record got inserted
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _error_message = ex.Message;
                }
                return -1;
            }

            #endregion
        }
    }
}
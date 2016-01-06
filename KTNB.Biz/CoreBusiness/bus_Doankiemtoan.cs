using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreData;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.DMS;
namespace vpb.app.business
{
    namespace ktnb.CoreBusiness
    {
        public class bus_Doankiemtoan
        {
            db_Doankiemtoan _db = null;
            private UserContext mvarUserContext = null;
            private static bus_Doankiemtoan mvarInstance;

            #region Constructors
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <returns></returns>
            public static bus_Doankiemtoan Instance(UserContext usrCTX)
            {
                if (mvarInstance == null)
                    mvarInstance = new bus_Doankiemtoan(usrCTX);
                return mvarInstance;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            public bus_Doankiemtoan(UserContext usrCTX)
            {
                mvarUserContext = usrCTX;
                string mvarDatabaseName = usrCTX.DBName;
                if (mvarUserContext != null)
                    _db = new db_Doankiemtoan(mvarUserContext, mvarDatabaseName);
                else
                    throw new Exception("UserContext is null");

            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="usrCTX"></param>
            /// <param name="databaseName"></param>
            public bus_Doankiemtoan(UserContext usrCTX, string databaseName)
            {
                mvarUserContext = usrCTX;
                if (mvarUserContext != null)
                    _db = new db_Doankiemtoan(mvarUserContext, databaseName);
                else
                    throw new Exception("UserContext is null");

            }
            #endregion

            #region Interface methods
            /// <summary>
            /// 
            /// </summary>
            /// <param name="DoanKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachThanhVienDoanKT(string DoanKiemToanID)
            {
                return _db.DanhSachThanhVienDoanKT(DoanKiemToanID);
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="DoanKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachThanhVienNhomKT(string NhomKiemToanID)
            {
                return _db.DanhSachThanhVienNhomKT(NhomKiemToanID);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachDoanKT(string UserName)
            {
                return _db.DanhSachDoanKT(UserName);
            }

            /// <summary>
            /// lay tat cac danh sach doan kiem toan cho BLD
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>quangna</author>
            public DataSet DanhSachDoanDotForBLD()
            {
                return _db.DanhSachDoanDotForBLD();
            }

            /// <summary>
            /// Get danh sách đoàn(đợt) để Ban lãnh đạo duyệt
            /// </summary>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachDoanDotForBLD(int TrangThai)
            {
                return _db.DanhSachDoanDotForBLD(TrangThai);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachNhomKT(string vaitro, string UserName)
            {
                return _db.DanhSachNhomKT(vaitro, UserName);
            }
            /// <summary>
            /// Lấy danh sách mảng nghiệp vụ mà người đăng nhập phụ trách
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachMangNghiepVuPhuTrach(string UserName)
            {
                return _db.DanhSachMangNghiepVuPhuTrach(UserName);

            }

            /// <summary>
            /// Lay thong tin thu tuc theo cong viec id
            /// </summary>
            /// <param name="congviecID"></param>
            /// <returns></returns>
            /// <author>quangna</author>
            public DataSet GetThuTucInfoByCongViec(string congviecID)
            {
                return _db.GetThuTucInfoByCongViec(congviecID);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="nhomkt"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDotKTByNhomKT(string nhomkt)
            {
                return _db.GetDotKTByNhomKT(nhomkt);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="nhomkt"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetMangNVByNhomKT(string nhomkt)
            {
                return _db.GetMangNVByNhomKT(nhomkt);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="nhomkt"></param>
            /// <param name="UserID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetMangNVByThanhVien(string nhomkt, string UserID)
            {
                DataSet dsMangNVByNhom = _db.GetMangNVByNhomKT(nhomkt);
                DataView dvMangMVByThanhVien = new DataView(dsMangNVByNhom.Tables[0]);
                dvMangMVByThanhVien.RowFilter = "UserID ='" + UserID + "'";
                DataTable dtMangNVByThanhVien = dvMangMVByThanhVien.ToTable();
                DataSet dsMangNVByThanhVien = new DataSet();
                dsMangNVByThanhVien.Tables.Add(dtMangNVByThanhVien);
                return dsMangNVByThanhVien;
            }

            public DataSet GetLoaiDoiTuongByName(string tenLoaiDoiTuong)
            {
                string DocFields = "PK_DocumentID,Status,[Tên loại đối tượng kiểm toán]";
                string PropertyFields = "Tên loại đối tượng kiểm toán";

                string Condition = " and [Tên loại đối tượng kiểm toán]=N'" + tenLoaiDoiTuong + "'";
                bus_Document obj = bus_Document.Instance(mvarUserContext);
                DataSet ds = obj.getDocumentList(DOCTYPE.LOAI_DOITUONG_KT, DocFields, PropertyFields, Condition);
                return ds;
            }
            /// <summary>
            /// Lấy các mảng nghiệp vụ bởi thủ tục
            /// </summary>
            /// <param name="TenThuTucKT"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetMangNghiepVuByThuTuc(string TenThuTucKT)
            {
                string sDoctype = "45BF910B-9A1E-4AF8-83BD-46651D010E59";//thutuc doc type
                string DocFields = "PK_DocumentID,[Tên thủ tục kiểm toán],[Tên kiểm soát]";
                string PropertyFields = "Tên thủ tục kiểm toán,Tên kiểm soát";
                string Condition = " and [Tên thủ tục kiểm toán]=N'" + TenThuTucKT + "'";
                bus_Document obj = bus_Document.Instance(mvarUserContext);
                DataSet dsResult = obj.getDocumentList(sDoctype, DocFields, PropertyFields, Condition);
                //
                sDoctype = "25A386F6-613E-4A71-9C19-C83D33E81D94";//kiem soat doc type
                DocFields = "PK_DocumentID,[Tên kiểm soát],[Tên rủi ro]";
                PropertyFields = "Tên kiểm soát,Tên rủi ro";
                Condition = " and [Tên kiểm soát]=N'" + dsResult.Tables[0].Rows[0]["Tên kiểm soát"] + "'";
                dsResult = obj.getDocumentList(sDoctype, DocFields, PropertyFields, Condition);
                //
                sDoctype = "F3F02F88-BEC8-469D-BEC0-80F9DD3ACF22";//rui ro doc type
                DocFields = "PK_DocumentID,[Tên rủi ro kiểm soát],[Mục tiêu kiểm soát]";
                PropertyFields = "Tên rủi ro kiểm soát,Mục tiêu kiểm soát";
                Condition = " and [Tên rủi ro kiểm soát]=N'" + dsResult.Tables[0].Rows[0]["Tên rủi ro"] + "'";
                dsResult = obj.getDocumentList(sDoctype, DocFields, PropertyFields, Condition);
                //
                sDoctype = "F39BF431-3EA0-49FE-AC44-023A65A99171";//muc tieu kiem soat doc type
                DocFields = "PK_DocumentID,[Tên Mục tiêu kiểm soát],[Mảng nghiệp vụ]";
                PropertyFields = "Tên Mục tiêu kiểm soát,Mảng nghiệp vụ";
                Condition = " and [Tên Mục tiêu kiểm soát]=N'" + dsResult.Tables[0].Rows[0]["Mục tiêu kiểm soát"] + "'";
                dsResult = obj.getDocumentList(sDoctype, DocFields, PropertyFields, Condition);
                //
                sDoctype = "34187F34-392F-492A-A8F6-6954DDF6DEA9";//mang nghiep vu doc type
                DocFields = "PK_DocumentID,[Tên mảng nghiệp vụ]";
                PropertyFields = "Tên mảng nghiệp vụ";
                Condition = " and [Tên mảng nghiệp vụ]=N'" + dsResult.Tables[0].Rows[0]["Mảng nghiệp vụ"] + "'";
                dsResult = obj.getDocumentList(sDoctype, DocFields, PropertyFields, Condition);

                return dsResult;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="DoanKiemToanID"></param>
            /// <returns></returns>
            /// <author>quangna</author>
            public DataSet GetAllMangNghiepVuDocLink()
            {
                return _db.GetAllMangNghiepVuDocLink();
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="DoanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet DanhSachNhomByDoanKT(string DoanID)
            {
                return _db.DanhSachNhomByDoanKT(DoanID);
            }
            /// <summary>
            /// Lấy danh sách đoàn kiểm toán bởi đợt kiểm toán
            /// </summary>
            /// <param name="DotKiemToanID"></param>
            /// <returns></returns>
            public DataSet DanhSachDoanKTByDotKT(string DotKiemToanID)
            {
                return _db.DanhSachDoanKTByDotKT(DotKiemToanID);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="DoanKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetMangNghiepVuByDoanKT(string DoanKiemToanID)
            {
                string DocFields = "PK_DocumentID,Status,[Tên mảng nghiệp vụ]";
                string PropertyFields = "Tên mảng nghiệp vụ";

                string Condition = " and Status = 4 and PK_DocumentID IN (select mangnv.PK_DOCUMENTID from T_DOCLINK as mangnv_doan "
                                    + "inner join T_DOCUMENT as mangnv "
                                    + "on mangnv_doan.FK_DOCUMENTID = mangnv.PK_DOCUMENTID "
                                    + "where mangnv_doan.FK_DOCLINKID = '" + DoanKiemToanID + "')";
                bus_Document obj = bus_Document.Instance(mvarUserContext);
                DataSet ds = obj.getDocumentList(DOCTYPE.MANG_NGHIEPVU, DocFields, PropertyFields, Condition);
                return ds;
            }
            /// <summary>
            /// Lay danh sach Mang nghiep vu boi Dot Kiem Toan
            /// </summary>
            /// <param name="DoanKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetMangNghiepVuByDotKT(string DotKiemToanID)
            {
                //lay danh sach doan
                DataSet dsDoanKiemToan = _db.DanhSachDoanKTByDotKT(DotKiemToanID);
                DataSet dsMangNghiepVu = new DataSet();
                if (dsDoanKiemToan != null)
                {
                    if (dsDoanKiemToan.Tables.Count > 0)
                    {
                        if (dsDoanKiemToan.Tables[0].Rows.Count > 0)
                        {
                            string sDoanKiemToanID = dsDoanKiemToan.Tables[0].Rows[0]["DoanKiemToanID"].ToString();
                            dsMangNghiepVu = GetMangNghiepVuByDoanKT(sDoanKiemToanID);
                        }
                    }
                }

                //string DocFields = "PK_DocumentID,Status,[Tên mảng nghiệp vụ]";
                //string PropertyFields = "Tên mảng nghiệp vụ";

                //string Condition = " and Status = 4 and PK_DocumentID IN (select mangnv.PK_DOCUMENTID from T_DOCLINK as mangnv_doan "
                //                    + "inner join T_DOCUMENT as mangnv "
                //                    + "on mangnv_doan.FK_DOCUMENTID = mangnv.PK_DOCUMENTID "
                //                    + "where mangnv_doan.FK_DOCLINKID = '" + DoanKiemToanID + "')";
                //bus_Document obj = bus_Document.Instance(mvarUserContext);
                //DataSet ds = obj.getDocumentList(DOCTYPE.MANG_NGHIEPVU, DocFields, PropertyFields, Condition);
                //return ds;
                return dsMangNghiepVu;
            }
            /// <summary>
            /// Lấy các mảng nghiệp vủ bởi loại đối tượng kiểm toán.
            /// </summary>
            /// <param name="LoaiDoiTuongID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetMangNghiepVuByLoaiDoiTuong(string LoaiDoiTuongID)
            {
                string DocFields = "PK_DocumentID,Status,[Tên mảng nghiệp vụ]";
                string PropertyFields = "Tên mảng nghiệp vụ";

                string Condition = " and Status=4 and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + LoaiDoiTuongID + "')";

                bus_Document obj = new bus_Document(mvarUserContext);
                DataSet ds = obj.getDocumentList(DOCTYPE.MANG_NGHIEPVU, DocFields, PropertyFields, Condition);
                return ds;
            }
            /// <summary>
            /// Lấy loại đối tượng kiểm toán bởi đợt kiểm toán
            /// </summary>
            /// <param name="DotKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public string GetLoaiDoiTuongByDotKT(string DotKiemToanID)
            {
                string tenLoaiDoiTuong = getPropertyValueOnDocument(DotKiemToanID, "737694DF-DE17-4FF9-AE15-70E197C83593");
                string LoaiDoiTuongID = string.Empty;
                bus_Doankiemtoan doankiemtoan = new bus_Doankiemtoan(mvarUserContext);
                DataSet dsLoaiDoiTuong = doankiemtoan.GetLoaiDoiTuongByName(tenLoaiDoiTuong);
                if (dsLoaiDoiTuong != null)
                {
                    LoaiDoiTuongID = dsLoaiDoiTuong.Tables[0].Rows[0]["PK_DOCUMENTID"].ToString();
                }
                return LoaiDoiTuongID;

            }
            /// <summary>
            /// Lấy thông tin đợt kiểm toán bởi công việc
            /// </summary>
            /// <param name="congviecID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDotInfoByCongViec(string congviecID)
            {
                DataSet dsDotInfo = _db.GetDotInfoByCongViec(congviecID);
                return dsDotInfo;
            }
            /// <summary>
            /// Lấy thông tin đợt kiểm toán bởi phát hiện
            /// </summary>
            /// <param name="phatHienID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDotInfoByPhatHien(string phatHienID)
            {
                DataSet dsDotInfo = _db.GetDotInfoByPhatHien(phatHienID);
                return dsDotInfo;
            }
            /// <summary>
            /// Lấy thông tin đợt kiểm toán bởi phản hồi
            /// </summary>
            /// <param name="phanHoiID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDotInfoByPhanHoi(string phanHoiID)
            {
                DataSet dsDotInfo = _db.GetDotInfoByPhanHoi(phanHoiID);
                return dsDotInfo;
            }

            /// <summary>
            /// lay danh sach phat hien theo cong viec
            /// </summary>
            /// <param name="congviecID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDanhSachPhatHien(string congviecID)
            {
                DataSet dsPhatHien = _db.GetDanhSachPhatHien(congviecID);
                return dsPhatHien;
            }
            /// <summary>
            /// lay danh sach phan hoi theo phat hien
            /// </summary>
            /// <param name="phatHienID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDanhSachPhanHoi(string phatHienID)
            {
                DataSet dsPhanHoi = _db.GetDanhSachPhanHoi(phatHienID);
                return dsPhanHoi;
            }

            /// <summary>
            /// ds phanhoi theo usertype
            /// </summary>
            /// <param name="phatHienID">id phathien</param>
            /// <param name="UserType">nguoithuchien,nguoiduyet1,nguoiduyet2</param>
            /// <returns></returns>
            /// <author>quangna</author>
            public DataSet GetDanhSachPhanHoiByUserType(string phatHienID, string UserType)
            {
                DataSet dsPhanHoi = _db.GetDanhSachPhanHoi(phatHienID, UserType);
                return dsPhanHoi;
            }

            /// <summary>
            /// Lay tat ca phat hien HT + VP theo congviec
            /// </summary>
            /// <param name="congviecID"></param>
            /// <returns></returns>
            /// <author>quangna</author>
            public DataSet GetTatCaDanhSachPhatHien(string congviecID)
            {
                DataSet dsPhatHien = _db.GetDanhSachPhatHien(congviecID);
                DataSet dsPhatHienViPham = _db.GetDanhSachPhatHienViPham(congviecID);
                if (dsPhatHienViPham != null)
                {
                    if (dsPhatHienViPham.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsPhatHienViPham.Tables[0].Rows)
                        {
                            dsPhatHien.Tables[0].ImportRow(row);
                        }
                    }
                }

                return dsPhatHien;
            }

            /// <summary>
            /// ds phathien theo usertype
            /// </summary>
            /// <param name="congviecID">congviecid</param>
            /// <param name="UserType">nguoithuchien,nguoiduyet1,nguoiduyet2</param>
            /// <returns></returns>
            /// <author>quangna</author>
            public DataSet GetTatCaDanhSachPhatHienByUserType(string congviecID, string UserType)
            {
                DataSet dsPhatHien = _db.GetDanhSachPhatHienByUserType(congviecID, UserType);
                DataSet dsPhatHienViPham = _db.GetDanhSachPhatHienViPhamByUserType(congviecID, UserType);
                if (dsPhatHienViPham != null)
                {
                    if (dsPhatHienViPham.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsPhatHienViPham.Tables[0].Rows)
                        {
                            dsPhatHien.Tables[0].ImportRow(row);
                        }
                    }
                }

                return dsPhatHien;
            }
            /// <summary>
            /// Lay thong tin chi tiet cac phat hien he thong cua dot kiem toan
            /// </summary>
            /// <param name="DotKiemToanID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetBienBanKetThucKTChiTiet(string DotKiemToanID)
            {
                string TEN_MANG_NGHIEP_VU = "144DFC1C-5D45-4FE7-8772-CD573CEFD04F";
                string PHAT_HIEN = "E48E3FFD-FE56-4B14-AC36-2C036873E1CD";
                string CHI_TIET = "CEED9D99-A89C-4127-9F9A-BD149E0BBC58";
                string MUC_DO = "9AD08FC3-5CEE-41E0-9A72-73822CFB2F68";
                //string TEN_CONG_VIEC = "470105E3-B810-4982-A8EF-74E367441EBD";
                //string PHAT_HIEN = "470105E3-B810-4982-A8EF-74E367441EBD";

                DataSet dsBienBanKetThucKT = _db.GetPhatHienByMangNvInDotKT(DotKiemToanID);

                dsBienBanKetThucKT.Tables[0].Columns.Add("ten_mang_nghiep_vu", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("phat_hien", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("chi_tiet", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("muc_do", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("phan_hoi", typeof(String));
                foreach (DataRow dtrow in dsBienBanKetThucKT.Tables[0].Rows)
                {
                    dtrow["ten_mang_nghiep_vu"] = getPropertyValueOnDocument(dtrow["MangNghiepVuID"].ToString(), TEN_MANG_NGHIEP_VU);
                    dtrow["phat_hien"] = getPropertyValueOnDocument(dtrow["PhatHienID"].ToString(), PHAT_HIEN);
                    dtrow["chi_tiet"] = getPropertyValueOnDocument(dtrow["PhatHienID"].ToString(), CHI_TIET);
                    dtrow["muc_do"] = getPropertyValueOnDocument(dtrow["PhatHienID"].ToString(), MUC_DO);
                    dtrow["phan_hoi"] = GetAllPhanHoi(dtrow["PhatHienID"].ToString());
                }

                return dsBienBanKetThucKT;
            }
            /// <summary>
            /// Lấy phản hồi cuối cùng của đơn vị bởi phát hiện
            /// </summary>
            /// <param name="PhatHienID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataRow[] GetPhanHoiCuoiCungByPhatHien(string PhatHienID)
            {
                DataSet dsPhanHoi = _db.GetDanhSachPhanHoi(PhatHienID);
                DataTable dt = dsPhanHoi.Tables[0];
                DataRow[] last = dt.Select("ngay_gio = Max(ngay_gio)");
                return last;
            }

            public string GetAllPhanHoi(string PhatHienID)
            {
                string str = string.Empty;
                DataSet dsPhanHoi = _db.GetDanhSachPhanHoi(PhatHienID);
                foreach (DataRow row in dsPhanHoi.Tables[0].Rows)
                {
                    if (row["NoiDung"] != null)
                        if (!string.IsNullOrEmpty(row["NoiDung"].ToString()))
                            str += "- " + row["NoiDung"].ToString() + "\n";
                }
                return str;
            }

            /// <summary>
            /// Lấy danh sách các đợt kiểm toán mà người đăng nhập là trưởng đoàn
            /// </summary>
            /// <param name="UserName"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetDotKTCuaTruongDoanChiTiet(string UserName)
            {
                string TEN_DOT_KIEM_TOAN = "63A0C4B1-2088-4994-B891-2FF65EB20265";
                string DOI_TUONG_KIEM_TOAN = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
                string DON_VI_THUC_HIEN = "A8A3EDBA-F569-4C06-8A57-045FBFED55FD";
                DataSet dsDotKT = _db.GetDotKTCuaTruongDoan(UserName);
                dsDotKT.Tables[0].Columns.Add("doi_tuong_kiem_toan", typeof(String));
                dsDotKT.Tables[0].Columns.Add("ten_dot_kiem_toan", typeof(String));
                dsDotKT.Tables[0].Columns.Add("don_vi_thuc_hien", typeof(String));
                foreach (DataRow dtrow in dsDotKT.Tables[0].Rows)
                {
                    dtrow["doi_tuong_kiem_toan"] = getPropertyValueOnDocument(dtrow["dotid"].ToString(), DOI_TUONG_KIEM_TOAN);
                    dtrow["ten_dot_kiem_toan"] = getPropertyValueOnDocument(dtrow["dotid"].ToString(), TEN_DOT_KIEM_TOAN);
                    dtrow["don_vi_thuc_hien"] = getPropertyValueOnDocument(dtrow["dotid"].ToString(), DON_VI_THUC_HIEN);
                }
                return dsDotKT;
            }
            /// <summary>
            /// Kiểm tra xem mảng nghiệp vụ đã được phân tích rủi ro chưa
            /// </summary>
            /// <param name="nhomkt"></param>
            /// <param name="mangnv"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            private bool KiemTraDaPTRR(string nhomkt, string mangnv)
            {
                bool status = false;
                string sTenMangNghiepVu = getPropertyValueOnDocument(mangnv, "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");

                bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(mvarUserContext);
                DataSet dsPhanTichRuiRo = lapKeHoach.ChiTietHoSoPhanTichRuiRo(nhomkt);

                if (dsPhanTichRuiRo != null)
                {
                    DataRow[] foundRowsByMangNghiepVu;
                    DataRow[] foundRowsDaDanhGia;

                    string expressionByMangNghiepVu = "Status = 4 and mang_nghiep_vu='" + sTenMangNghiepVu + "'";
                    string expressionDaDanhGia = "xac_suat > 0 and anh_huong > 0 and Status = 4 and mang_nghiep_vu='" + sTenMangNghiepVu + "'";

                    foundRowsByMangNghiepVu = dsPhanTichRuiRo.Tables[0].Select(expressionByMangNghiepVu);
                    foundRowsDaDanhGia = dsPhanTichRuiRo.Tables[0].Select(expressionDaDanhGia);

                    if ((foundRowsDaDanhGia.Count() == foundRowsByMangNghiepVu.Count()) && foundRowsDaDanhGia.Count() > 0)
                        status = true;
                }

                return status;
            }
            /// <summary>
            /// Lấy các file của Document
            /// </summary>
            /// <param name="DocID"></param>
            /// <returns></returns>
            /// <author>thangma</author>
            public DataSet GetFilesByDoc(string DocID)
            {
                return _db.GetFilesByDoc(DocID);
            }
            public string getPropertyValueOnDocument(string documentID, string propertyID)
            {
                bus_Type_Doc_Property objTDP = bus_Type_Doc_Property.Instance(mvarUserContext);
                string svalue = objTDP.GetPropertyValueOnDocument(documentID, propertyID);
                objTDP = null;
                return svalue;
            }

            #endregion

            #region T_DOC_VERSION_BODY
            /// <summary>
            /// Add docversionbody
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
                int value = _db.AddDocVersionBody(PK_DOCVERSIONBODYID, FK_DOCUMENTID, FILENAME, FILEPATH, DISPLAYFILENAME);
                return value;
            }

            #endregion

            #region phuc vu report chitiet(chinhthuc) + sobo
            /// <summary>
            /// Lay thong tin phuc vu bao cao chinhthuc theo dot kiem toan
            /// </summary>
            /// <param name="DotKiemToanID"></param>
            /// <returns></returns>
            /// <author>quangna</author>
            public DataSet GetBaoCaoChiTiet_SoBo(string DotKiemToanID)
            {
                string TEN_MANG_NGHIEP_VU = "144DFC1C-5D45-4FE7-8772-CD573CEFD04F";
                string PHAT_HIEN = "E48E3FFD-FE56-4B14-AC36-2C036873E1CD";

                string PHAT_HIEN_MUC_DO = "9AD08FC3-5CEE-41E0-9A72-73822CFB2F68";
                string PHAT_HIEN_DAN_CHIEU = "CEED9D99-A89C-4127-9F9A-BD149E0BBC58";//CHI TIET
                string PHAT_HIEN_NGUYEN_NHAN = "3C648C41-DA03-48AB-AAD5-143D9D959C00";

                string PHAT_HIEN_ANH_HUONG = "2CD4A9CF-D1AC-4675-87A3-739AF5344AD8";
                string PHAT_HIEN_KHUYEN_NGHI = "6A1B7B08-BE4E-400A-A6CC-EC7897A89031";

                DataSet dsBienBanKetThucKT = _db.GetPhatHienByMangNvInDotKT(DotKiemToanID);
                if (dsBienBanKetThucKT == null)
                    return null;
                if (dsBienBanKetThucKT.Tables.Count == 0)
                    return null;
                dsBienBanKetThucKT.Tables[0].Columns.Add("ten_mang_nghiep_vu", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("phat_hien", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("phat_hien_id", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("muc_do", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("dan_chieu", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("nguyen_nhan", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("anh_huong", typeof(String));
                dsBienBanKetThucKT.Tables[0].Columns.Add("khuyen_nghi", typeof(String));

                foreach (DataRow dtrow in dsBienBanKetThucKT.Tables[0].Rows)
                {
                    dtrow["ten_mang_nghiep_vu"] = getPropertyValueOnDocument(dtrow["MangNghiepVuID"].ToString(), TEN_MANG_NGHIEP_VU);
                    dtrow["phat_hien"] = getPropertyValueOnDocument(dtrow["PhatHienID"].ToString(), PHAT_HIEN);
                    dtrow["phat_hien_id"] = dtrow["PhatHienID"].ToString();

                    dtrow["muc_do"] = getPropertyValueOnDocument(dtrow["PhatHienID"].ToString(), PHAT_HIEN_MUC_DO);
                    dtrow["dan_chieu"] = getPropertyValueOnDocument(dtrow["PhatHienID"].ToString(), PHAT_HIEN_DAN_CHIEU);
                    dtrow["nguyen_nhan"] = getPropertyValueOnDocument(dtrow["PhatHienID"].ToString(), PHAT_HIEN_NGUYEN_NHAN);
                    dtrow["anh_huong"] = getPropertyValueOnDocument(dtrow["PhatHienID"].ToString(), PHAT_HIEN_ANH_HUONG);
                    dtrow["khuyen_nghi"] = getPropertyValueOnDocument(dtrow["PhatHienID"].ToString(), PHAT_HIEN_KHUYEN_NGHI);
                }

                return dsBienBanKetThucKT;
            }

            #endregion

        }
    }
}
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using CORE.WFS.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;

namespace VPB_KTNB.Helpers
{
    /// <summary>
    /// Summary description for DataSource
    /// </summary>
    public class DataSource
    {
        private static UserContext _objUserContext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];

        #region Can not remove

        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet SelectAllUserInGroup(string groupid, string loginname, bool searchall, string docspace)
        {
            string query = "*";
            string condition = " ";
            UserContext objUserContext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];
            bus_User_In_Group objug = bus_User_In_Group.Instance(objUserContext);

            if (searchall)
            {
                if (!string.IsNullOrEmpty(loginname))
                    condition = " AND UserName='" + loginname + "'";
            }
            else
            {
                if (!string.IsNullOrEmpty(loginname))
                {
                    //if (ROLES.LANHDAO_TTD.ToUpper().LastIndexOf(((Role)_objUserContext.Roles[0]).RoleID.ToUpper()) != -1)
                    //{
                    //    condition = " AND PK_ROLEID IN ('" + ROLES.CHUYENVIEN_TTD + "','" + ROLES.CHUYENGIA_PHEDUYET + "','" + ROLES.TOTRUONG_TTD + "','" + ROLES.THUKY_HDTD + "','" + ROLES.LANHDAO_TTD + "') AND ORDER_NUMBER = 0 AND UserName='" + loginname + "'";
                    //}
                    //else
                    //{
                    condition = " AND PK_GroupID='" + groupid + "' AND UserName='" + loginname + "'";
                    //}
                }
                else
                {
                    //if (ROLES.LANHDAO_TTD.ToUpper().LastIndexOf(((Role)_objUserContext.Roles[0]).RoleID.ToUpper()) != -1)
                    //{
                    //    condition = " AND PK_ROLEID IN ('" + ROLES.CHUYENVIEN_TTD + "','" + ROLES.CHUYENGIA_PHEDUYET + "','" + ROLES.TOTRUONG_TTD + "','" + ROLES.THUKY_HDTD + "','" + ROLES.LANHDAO_TTD + "')";
                    //}
                    //else
                    //{
                    condition = " AND PK_GroupID='" + groupid + "'";
                    //}
                }
            }

            if (docspace == "appuser")
                condition += " AND GROUPNAME NOT IN ('SysAdmin','AppAdmin')";
            else
            {
                string roleid = ((Role)objUserContext.Roles[0]).RoleID.ToUpper();
                if (roleid.ToUpper() != ROLES.SYS_ADMIN.ToUpper())
                    condition += " AND GROUPNAME NOT IN ('SysAdmin')";

            }


            condition += " ORDER BY UserName";
            return objug.getList(condition, query);
        }

        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet GetCompany(string query, string condition)
        {
            bus_Group objgroup = bus_Group.Instance(_objUserContext);
            return objgroup.getList(condition, query);
        }

        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet LoadRoleDetail(string condition, string query)
        {
            bus_User objur = bus_User.Instance(_objUserContext);
            DataSet ds = objur.getListOnView(condition, query);
            return ds;
        }

        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet GetItemsProcessingOnProcess(string DocspaceStatus, string ProcessDefinitionID, string Performer, string Year, string Month, string TransitionID)
        {
            bus_WF_Instance obj = bus_WF_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = new DataSet();
            if (DocspaceStatus.ToLower().LastIndexOf("myactivity") != -1)
                ds = obj.GetItemsProcessingOnProcess(DocspaceStatus, ProcessDefinitionID, Performer, int.Parse(Year), int.Parse(Month), TransitionID);
            else
                ds = obj.GetItemsProcessingOnProcess_ByCompany(DocspaceStatus, ProcessDefinitionID, Performer, int.Parse(Year), int.Parse(Month));
            obj = null;
            return ds;
        }

        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet MonitoringProcessInstance(string ProcessInstanceID)
        {
            bus_WF_Instance obj = bus_WF_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.MonitoringProcessInstance(ProcessInstanceID);
            obj = null;
            return ds;
        }

        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet getDocumentList(string DocumentTypeID, string DocFields, string PropertyFields, string Condition)
        {
            if (String.IsNullOrEmpty(DocumentTypeID))
                return null;
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(DocumentTypeID, DocFields, PropertyFields, Condition);
            obj = null;
            return ds;
        }
        #endregion


        #region ktnb
        /// <summary>
        /// tim kiem thong tin dot kiem toan
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        /// <param name="DocFields"></param>
        /// <param name="PropertyFields"></param>
        /// <param name="Condition"></param>
        /// <param name="TuNgay"></param>
        /// <param name="DenNgay"></param>
        /// <returns></returns>
        /// author:quangna
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getTimKiemDotKiemToan(string DocumentTypeID, string DocFields, string PropertyFields,
            string Condition, string TuNgay, string DenNgay, string TrangThai)
        {
            if (String.IsNullOrEmpty(TuNgay) || string.IsNullOrEmpty(DenNgay))
                return null;
            bus_Document doc = new bus_Document((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = doc.getDocumentList(DOCTYPE.DOT_KIEMTOAN, DocFields, PropertyFields, Condition);
            if (!isValidDataSet(ds))
                return null;
            //format dd/MM/yyyy
            DateTime dtTuNgay = Convert.ToDateTime(TuNgay);
            DateTime dtDenNgay = Convert.ToDateTime(DenNgay);

            DataTable dt = ds.Tables[0];
            dt.Columns.Add("ThoiGian", typeof(DateTime));
            foreach (DataRow row in dt.Rows)
            {
                string thoigian = String.Format("1/{0}/{1}", row["Thời gian dự kiến kiểm toán"].ToString().Replace("Tháng", "").Trim(), row["Năm"]);
                row["ThoiGian"] = String.Format("{0:M/d/yyyy}", Convert.ToDateTime(thoigian));
            }

            string filter = "ThoiGian >= '" + String.Format("{0:M/d/yyyy}", dtTuNgay) + "' AND ThoiGian <= '" + String.Format("{0:M/d/yyyy}", dtDenNgay) + "'";
            DataRow[] drs = dt.Select(filter);
            //make a new "results" datatable via clone to keep structure
            DataTable dt2 = dt.Clone();
            //Import the Rows
            foreach (DataRow d in drs)
                dt2.ImportRow(d);
            return dt2;
        }


        /// <summary>
        /// tim kiem thong tin dot kiem toan theo cong viec
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        /// <param name="DocFields"></param>
        /// <param name="PropertyFields"></param>
        /// <param name="Condition"></param>
        /// <param name="TuNgay"></param>
        /// <param name="DenNgay"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getTimKiemPhatHien(string TuNgay, string DenNgay, string DoiTuongKiemToan, string TrangThai)
        {
            if (String.IsNullOrEmpty(TuNgay) || string.IsNullOrEmpty(DenNgay) || string.IsNullOrEmpty(DoiTuongKiemToan))
                return null;

            //format dd/MM/yyyy
            DateTime dtTuNgay = Convert.ToDateTime(TuNgay);
            DateTime dtDenNgay = Convert.ToDateTime(DenNgay);

            DataTable dt = new DataTable();
            dt.Columns.Add("PK_DocumentID", typeof(string));
            dt.Columns.Add("PhatHien", typeof(string));
            dt.Columns.Add("Kieu", typeof(string));//Phan biet phan biet phat hien HeThong(HT),ViPham(VP)
            dt.Columns.Add("MucDo", typeof(string));
            dt.Columns.Add("ChiTiet", typeof(string));
            dt.Columns.Add("AnhHuong", typeof(string));
            dt.Columns.Add("GhiChu", typeof(string));
            dt.Columns.Add("KhuyenNghi", typeof(string));
            dt.Columns.Add("FileDinhKem", typeof(string));

            dt.Columns.Add("DotKiemToanID", typeof(string));
            dt.Columns.Add("DotKiemToan", typeof(string));
            dt.Columns.Add("LoaiDoiTuongKiemToan", typeof(string));
            dt.Columns.Add("DoiTuongKiemToan", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Nam", typeof(string));
            dt.Columns.Add("ThoiGianDuKien", typeof(string));

            dt.Columns.Add("ThoiGian", typeof(DateTime));

            //lay tat ca HeThong(co file dinhkem)
            string DocFields_HT = "PK_DocumentID,[Nguyên nhân],[Phát hiện],[Đối tượng kiểm toán],[Mức độ],[Ảnh hưởng],[Ghi chú],[Chi tiêt],[Khuyến nghị],[Đợt kiểm toán]";
            string PropertyFields_HT = "Nguyên nhân,Phát hiện,Đối tượng kiểm toán,Mức độ,Ảnh hưởng,Ghi chú,Chi tiêt,Khuyến nghị,Đợt kiểm toán";
            string Condition_HT = DoiTuongKiemToan == "-1" ? String.Empty : " And [Đối tượng kiểm toán]=N'" + DoiTuongKiemToan + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet dsHT = doc.getDocumentList(DOCTYPE.PHATHIEN_HETHONG, DocFields_HT, PropertyFields_HT, Condition_HT);
            if (isValidDataSet(dsHT))
            {
                foreach (DataRow rowHT in dsHT.Tables[0].Rows)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["PK_DocumentID"] = rowHT["PK_DocumentID"];
                    newRow["PhatHien"] = rowHT["Phát hiện"];
                    newRow["Kieu"] = "HT";
                    newRow["MucDo"] = rowHT["Mức độ"];
                    newRow["ChiTiet"] = rowHT["Chi tiêt"];
                    newRow["AnhHuong"] = rowHT["Ảnh hưởng"];
                    newRow["GhiChu"] = rowHT["Ghi chú"];
                    newRow["KhuyenNghi"] = rowHT["Khuyến nghị"];

                    newRow["DotKiemToanID"] = GetThongTinDotKiemToan(rowHT["Đợt kiểm toán"].ToString(), "PK_DocumentID"); ;
                    newRow["DotKiemToan"] = rowHT["Đợt kiểm toán"];
                    newRow["LoaiDoiTuongKiemToan"] = GetThongTinDotKiemToan(rowHT["Đợt kiểm toán"].ToString(), "Loại đối tượng kiểm toán");
                    newRow["DoiTuongKiemToan"] = rowHT["Đối tượng kiểm toán"];
                    newRow["Status"] = GetThongTinDotKiemToan(rowHT["Đợt kiểm toán"].ToString(), "Status");

                    string nam = GetThongTinDotKiemToan(rowHT["Đợt kiểm toán"].ToString(), "Năm");
                    string thoigiandukienkiemtoan = GetThongTinDotKiemToan(rowHT["Đợt kiểm toán"].ToString(), "Thời gian dự kiến kiểm toán");

                    newRow["Nam"] = nam;
                    newRow["ThoiGianDuKien"] = thoigiandukienkiemtoan;

                    if (!String.IsNullOrEmpty(nam) && !String.IsNullOrEmpty(thoigiandukienkiemtoan))
                    {
                        string thoigian = String.Format("1/{0}/{1}", thoigiandukienkiemtoan.Replace("Tháng", "").Trim(), nam);
                        newRow["ThoiGian"] = String.Format("{0:M/d/yyyy}", Convert.ToDateTime(thoigian));
                    }
                    dt.Rows.Add(newRow);
                }
            }
            //lay tat ca ViPham(ko file dinhkem)
            string DocFields_VP = "PK_DocumentID,[Nguyên nhân],[Phát hiện],[Đối tượng kiểm toán],[Mức độ],[Ảnh hưởng],[Ghi chú],[Chi tiết],[Khuyến nghị],[Đợt kiểm toán]";
            string PropertyFields_VP = "Nguyên nhân,Phát hiện,Đối tượng kiểm toán,Mức độ,Ảnh hưởng,Ghi chú,Chi tiết,Khuyến nghị,Đợt kiểm toán";
            string Condition_VP = DoiTuongKiemToan == "-1" ? String.Empty : " And [Đối tượng kiểm toán]=N'" + DoiTuongKiemToan + "'";
            DataSet dsVP = doc.getDocumentList(DOCTYPE.PHATHIEN_VIPHAM, DocFields_VP, PropertyFields_VP, Condition_VP);
            if (isValidDataSet(dsVP))
            {
                foreach (DataRow rowVP in dsVP.Tables[0].Rows)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["PK_DocumentID"] = rowVP["PK_DocumentID"];
                    newRow["PhatHien"] = rowVP["Phát hiện"];
                    newRow["Kieu"] = "VP";
                    newRow["MucDo"] = rowVP["Mức độ"];
                    newRow["ChiTiet"] = rowVP["Chi tiết"];
                    newRow["AnhHuong"] = rowVP["Ảnh hưởng"];
                    newRow["GhiChu"] = rowVP["Ghi chú"];
                    newRow["KhuyenNghi"] = rowVP["Khuyến nghị"];

                    newRow["DotKiemToanID"] = GetThongTinDotKiemToan(rowVP["Đợt kiểm toán"].ToString(), "PK_DocumentID"); ;
                    newRow["DotKiemToan"] = rowVP["Đợt kiểm toán"];
                    newRow["LoaiDoiTuongKiemToan"] = GetThongTinDotKiemToan(rowVP["Đợt kiểm toán"].ToString(), "Loại đối tượng kiểm toán");
                    newRow["Status"] = GetThongTinDotKiemToan(rowVP["Đợt kiểm toán"].ToString(), "Status");

                    newRow["DoiTuongKiemToan"] = rowVP["Đối tượng kiểm toán"];
                    string nam = GetThongTinDotKiemToan(rowVP["Đợt kiểm toán"].ToString(), "Năm");
                    string thoigiandukienkiemtoan = GetThongTinDotKiemToan(rowVP["Đợt kiểm toán"].ToString(), "Thời gian dự kiến kiểm toán");

                    newRow["Nam"] = nam;
                    newRow["ThoiGianDuKien"] = thoigiandukienkiemtoan;

                    if (!String.IsNullOrEmpty(nam) && !String.IsNullOrEmpty(thoigiandukienkiemtoan))
                    {
                        string thoigian = String.Format("1/{0}/{1}", thoigiandukienkiemtoan.Replace("Tháng", "").Trim(), nam);
                        newRow["ThoiGian"] = String.Format("{0:M/d/yyyy}", Convert.ToDateTime(thoigian));
                    }
                    dt.Rows.Add(newRow);
                }
            }

            string filter = "ThoiGian >= '" + String.Format("{0:M/d/yyyy}", dtTuNgay) + "' AND ThoiGian <= '" + String.Format("{0:M/d/yyyy}", dtDenNgay) + "'";
            string status = String.Empty;
            if (TrangThai == "-1")
            {

            }
            if (TrangThai == "1")
            {
                status = " AND (Status = 11 Or Status = 12 Or Status = 13 Or Status = 14 Or Status = 15 Or Status = 141) ";
                filter = filter + status;
            }
            if (TrangThai == "2")
            {
                status = " AND (Status = 21 Or Status = 22 Or Status = 23 Or Status =24 Or Status =25 Or Status = 26 Or Status = 231) ";
                filter = filter + status;
            }
            if (TrangThai == "3")
            {
                status = " AND (Status = 31) ";
                filter = filter + status;
            }
            if (TrangThai == "4")
            {
                status = " AND (Status = 41 Or Status = 42  Or Status = 43 Or Status =44) ";
                filter = filter + status;
            }
            if (TrangThai == "5")
            {
                status = " AND (Status = 51 Or Status = 52  Or Status = 53) ";
                filter = filter + status;
            }
            DataRow[] drs = dt.Select(filter);
            //make a new "results" datatable via clone to keep structure
            DataTable dt2 = dt.Clone();
            //Import the Rows
            foreach (DataRow d in drs)
                dt2.ImportRow(d);
            return dt2;
        }

        /// <summary>
        /// get thong tin dot kiem toan theo ten dotkt
        /// </summary>
        /// <param name="tenDotKT">ten dotkt</param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        static string GetThongTinDotKiemToan(string tenDotKT, string columnName)
        {
            string DocFields = "PK_DocumentID,[Tên đợt kiểm toán],[Đối tượng kiểm toán],[Quy mô đợt kiểm toán],[Đơn vị thực hiện],[Năm],[Thời gian dự kiến kiểm toán],[Đơn vị thực hiện],[Loại đối tượng kiểm toán],Status";
            string PropertyFields = "Tên đợt kiểm toán,Đối tượng kiểm toán,Quy mô đợt kiểm toán,Đơn vị thực hiện,Năm,Thời gian dự kiến kiểm toán,Đơn vị thực hiện,Loại đối tượng kiểm toán";
            string Condition = " And  [Tên đợt kiểm toán]=N'" + tenDotKT + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.DOT_KIEMTOAN, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
                return ds.Tables[0].Rows[0][columnName].ToString();
            return String.Empty;
        }


        /// <summary>
        /// danh sach thanh vien theo groupname
        /// </summary>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet getListDanhSanhThanhVienByGroupName(string GroupName)
        {
            if (String.IsNullOrEmpty(GroupName))
                return null;
            bus_User_In_Group group = new bus_User_In_Group((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = group.getList(" And GROUPNAME=N'" + GroupName + "'", "FullName,UserName,GroupName");
            if (isValidDataSet(ds))
                return ds;
            return null;
        }

        /// <summary>
        /// danh sach thanh vien doankt
        /// </summary>
        /// <param name="DoanKT">id doankt</param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet getListThanhVienDoanKiemToan(string DoanKT)
        {
            if (String.IsNullOrEmpty(DoanKT))
                return null;
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = doankiemtoan.DanhSachThanhVienDoanKT(DoanKT);
            if (isValidDataSet(ds))
                return ds;
            return null;
        }
        /// <summary>
        /// getListThanhVien()
        /// lấy danh sách các thành viên của 1 đoàn kiểm toán
        /// </summary>
        /// <param name="DoanKT"></param>
        /// <returns></returns>
        /// <auth>dungnt01</auth>
        /// <created>2014 Sep 10</created>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getListThanhVien(string DoanKT)
        {
            DataTable tbl = null;
            bus_User obj = bus_User.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            string query = "Name";
            string condition = " And PK_UserID In (SELECT FK_DOCUMENTID FROM T_DOCLINK WHERE FK_DOCLINKID='" + DoanKT + "' AND LINKTYPE=13)";
            tbl = obj.getList(condition, query).Tables[0];
            obj = null;
            return tbl;
        }
        /// <summary>
        /// danh sach mang nghiep vu theo doankt da dc cau hinh
        /// </summary>
        /// <param name="DoanKT"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getListMangNghiepVu(string DoanKT)
        {
            if (String.IsNullOrEmpty(DoanKT))
                return null;
            //lay tat ca cac mang nghiep vu thuoc doan kiem toan
            string DocFields = "PK_DocumentID,[Tên mảng nghiệp vụ],[Diễn giải],Status";
            string PropertyFields = "Tên mảng nghiệp vụ,Diễn giải";
            string Condition = " And PK_DocumentID In (Select FK_DocumentID from T_DocLink where FK_DocLinkID ='" + DoanKT + "')";

            bus_Document obj_mangnghiepvu = new bus_Document((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds_mangnghiepvu = obj_mangnghiepvu.getDocumentList(DOCTYPE.MANG_NGHIEPVU, DocFields, PropertyFields, Condition);

            //hien thi thong tin: lay tat ca thong tin truongnhom/thanhvien ma ton tai
            if (isValidDataSet(ds_mangnghiepvu) == false)
                return null;

            string DocFields_nhomkt = "PK_DocumentID,[Name]";
            string PropertyFields_nhomkt = "Name";
            string Condition_kt = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + DoanKT + "')";
            bus_Document obj_nhomkt = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds_nhomkt = obj_nhomkt.getDocumentList(DOCTYPE.NHOM_KIEMTOAN, DocFields_nhomkt, PropertyFields_nhomkt, Condition_kt);

            //Dinh nghia table mangnghiepvu
            DataTable dt_mangnghiepvu = ds_mangnghiepvu.Tables[0];
            dt_mangnghiepvu.Columns.Add("NhomKiemToan_ID", typeof(string));
            dt_mangnghiepvu.Columns.Add("NhomKiemToan", typeof(string));
            dt_mangnghiepvu.Columns.Add("ThanhVien", typeof(string));

            //neu ko co nhom kiem toan nao
            if (isValidDataSet(ds_nhomkt) == false)
            {
                foreach (DataRow row_mangnghiepvu in dt_mangnghiepvu.Rows)
                {
                    row_mangnghiepvu["NhomKiemToan_ID"] = String.Empty;
                    row_mangnghiepvu["NhomKiemToan"] = String.Empty;
                    row_mangnghiepvu["ThanhVien"] = String.Empty;
                }
            }
            else
            {
                bus_Doankiemtoan obj_doankt = new bus_Doankiemtoan((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds_mangnghiepvu_doclink = obj_doankt.GetAllMangNghiepVuDocLink();
                if (isValidDataSet(ds_mangnghiepvu_doclink))
                    foreach (DataRow row_mangnghiepvu in dt_mangnghiepvu.Rows)
                    {
                        DataRow row = GetMangNghiepVuAndNhomKT(ds_nhomkt, ds_mangnghiepvu_doclink, row_mangnghiepvu["PK_DocumentID"].ToString(), row_mangnghiepvu);
                        row_mangnghiepvu["NhomKiemToan_ID"] = row["NhomKiemToan_ID"];
                        row_mangnghiepvu["NhomKiemToan"] = row["NhomKiemToan"];
                        row_mangnghiepvu["ThanhVien"] = row["ThanhVien"];
                    }

            }
            return dt_mangnghiepvu;
        }

        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet getList(string Status)
        {
            //DSMangNVDuocPhuTrach
            if (Status == "1")
            {
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachMangNghiepVuPhuTrach(((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName);
                if (isValidDataSet(ds))
                    return ds;
            }
            //TruongDoan_XinDuyet HSRR
            if (Status == "2")
            {
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachDoanKT(((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName);
                if (isValidDataSet(ds))
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "STATUS = 4";
                    dv.Sort = "nam desc";
                    DataSet dsDoanKT = new DataSet();
                    dsDoanKT.Tables.Add(dv.ToTable());
                    if (isValidDataSet(dsDoanKT))
                        return dsDoanKT;
                }
            }
            //DSNhomKiemToanDuocPhuTrach
            if (Status == "3")
            {
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachNhomKT("tn", ((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName);
                if (isValidDataSet(ds))
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "STATUS = 4";
                    DataSet dsNhomKT = new DataSet();
                    dsNhomKT.Tables.Add(dv.ToTable());
                    if (isValidDataSet(dsNhomKT))
                        return dsNhomKT;
                }
            }
            //BLD_Duyet HSRR
            if (Status == "4")
            {
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachDoanDotForBLD((int)TrangThaiDacBiet.ChoDuyetHSRR);
                if (isValidDataSet(ds))
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "STATUS = 4";
                    dv.Sort = "nam ASC";
                    DataSet dsDoanKT = new DataSet();
                    dsDoanKT.Tables.Add(dv.ToTable());
                    if (isValidDataSet(dsDoanKT))
                        return dsDoanKT;
                }
            }
            //BLD_ReView Ketthuc dotkt
            if (Status == "5")
            {
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachDoanDotForBLD((int)TrangThaiDacBiet.ChoDuyetReview);
                if (isValidDataSet(ds))
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "STATUS = 4";
                    dv.Sort = "nam ASC";
                    DataSet dsDoanKT = new DataSet();
                    dsDoanKT.Tables.Add(dv.ToTable());
                    if (isValidDataSet(dsDoanKT))
                        return dsDoanKT;
                }

                //bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                //DataSet ds = doankiemtoan.DanhSachDoanDotForBLD();
                //if (isValidDataSet(ds))
                //{
                //    DataView dv = ds.Tables[0].DefaultView;
                //    dv.RowFilter = "STATUS = 4";
                //    dv.Sort = "nam DESC";
                //    DataSet dsDoanKT = new DataSet();
                //    dsDoanKT.Tables.Add(dv.ToTable());
                //    if (isValidDataSet(dsDoanKT))
                //        return dsDoanKT;
                //}
            }
            //BLD_ truoc khi xuat bien ban kiem toan
            if (Status == "6")
            {
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachDoanDotForBLD((int)TrangThaiDacBiet.ChoDuyetXuatBB);
                if (isValidDataSet(ds))
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "STATUS = 4";
                    dv.Sort = "nam ASC";
                    DataSet dsDoanKT = new DataSet();
                    dsDoanKT.Tables.Add(dv.ToTable());
                    if (isValidDataSet(dsDoanKT))
                        return dsDoanKT;
                }
            }
            //TruongDoan_XinDuyet HSRR
            if (Status == "7")
            {
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachDoanKT(((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName);
                if (isValidDataSet(ds))
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "STATUSDOT = 14";
                    dv.Sort = "nam desc";
                    DataSet dsDoanKT = new DataSet();
                    dsDoanKT.Tables.Add(dv.ToTable());
                    if (isValidDataSet(dsDoanKT))
                        return dsDoanKT;
                }
            }
            //TruongDoan_XinDuyet truong doan xin BLD pheduyet Xuat BBKT
            if (Status == "8")
            {
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachDoanKT(((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName);
                if (isValidDataSet(ds))
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "STATUSDOT = 23";//chi lay trang thai ruiroconlai
                    dv.Sort = "nam DESC";
                    DataSet dsDoanKT = new DataSet();
                    dsDoanKT.Tables.Add(dv.ToTable());
                    if (isValidDataSet(dsDoanKT))
                        return dsDoanKT;
                }
            }
            //Truong Doan cap nhat phan hoi
            if (Status == "9")
            {
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachDoanKT(((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName);
                if (isValidDataSet(ds))
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "STATUSDOT = 24";//chi lay trang thai ruiroconlai
                    dv.Sort = "nam DESC";
                    DataSet dsDoanKT = new DataSet();
                    dsDoanKT.Tables.Add(dv.ToTable());
                    if (isValidDataSet(dsDoanKT))
                        return dsDoanKT;
                }
            }
            if (Status == "10")
            {
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachDoanKT(((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName);
                if (isValidDataSet(ds))
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "STATUS = 4 And STATUSDOT=25";
                    dv.Sort = "nam desc";
                    DataSet dsDoanKT = new DataSet();
                    dsDoanKT.Tables.Add(dv.ToTable());
                    if (isValidDataSet(dsDoanKT))
                        return dsDoanKT;
                }
            }

            return null;
        }
        /// <summary>
        /// Lấy danh các phát hiện hệ thống và phát hiện vi phạm
        /// </summary>
        /// <param name="congviecID"></param>
        /// <param name="cv"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet getListPhatHien(string congviecID, string cv)
        {
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = doankiemtoan.GetTatCaDanhSachPhatHienByUserType(congviecID, cv);
            if (isValidDataSet(ds))
                return ds;
            return null;
        }
        /// <summary>
        /// Lấy danh sách nhom kiểm toán mà người đăng nhập phụ trách
        /// </summary>
        /// <param name="DocTypeNhomKT"></param>
        /// <param name="role"></param>
        /// <param name="currentUserID"></param>
        /// <param name="currentUserName"></param>
        /// <param name="task"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet getNhomKiemToanInfo(string DocTypeNhomKT, string role, string currentUserID, string currentUserName, string task, string sort)
        {
            var strExpr = string.Empty;
            var strSort = string.Empty;
            string DocFields = "PK_DocumentID,[Name],STATUS";
            string PropertyFields = "Name,STATUS";
            //string Condition = string.Empty;
            string Condition = string.Empty;
            //truong nhom
            if (role.Equals("tn"))
            {
                Condition = " and STATUS=4 and PK_DocumentID in (Select PK_DocumentID From T_Document Where name='" + currentUserName + "')";
            }
            //thanh vien
            else
            {
                Condition = " and STATUS=4  and PK_DocumentID in (select thanhvien_nhom.FK_DOCLINKID from T_DOCLINK as thanhvien_nhom where thanhvien_nhom.FK_DOCUMENTID ='" + currentUserID + "')";
            }

            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsNhomKiemToan = obj.getDocumentList(DocTypeNhomKT, DocFields, PropertyFields, Condition);
            DataTable dtNhomKiemToan = dsNhomKiemToan.Tables[0];
            dtNhomKiemToan.Columns.Add("doi_tuong_kiem_toan", typeof(String));
            dtNhomKiemToan.Columns.Add("ten_dot_kiem_toan", typeof(String));
            dtNhomKiemToan.Columns.Add("dotkt", typeof(String));
            dtNhomKiemToan.Columns.Add("dotktstatus", typeof(String));

            bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);

            foreach (DataRow row in dtNhomKiemToan.Rows)
            {
                DataSet dsDotKT = doanKiemToan.GetDotKTByNhomKT(row["PK_DocumentID"].ToString());
                if (isValidDataSet(dsDotKT))
                {
                    row["doi_tuong_kiem_toan"] = dsDotKT.Tables[0].Rows[0]["doi_tuong_kiem_toan"].ToString();
                    row["ten_dot_kiem_toan"] = dsDotKT.Tables[0].Rows[0]["ten_dot_kiem_toan"].ToString();
                    row["dotkt"] = dsDotKT.Tables[0].Rows[0]["dotkt"].ToString();
                    row["dotktstatus"] = dsDotKT.Tables[0].Rows[0]["DotktStatus"].ToString();
                }
            }
            if (task != null)
            {
                //kiểm tra trạng thái đợt kiểm toán
                strExpr = "(";
                int[] arrTrangThaiDotKhongThe = TrangThaiDotKiemToan.KhongDanhGiaRRCL();
                foreach (int trangthai in arrTrangThaiDotKhongThe)
                {
                    strExpr += trangthai.ToString() + ",";
                }
                strExpr = strExpr.Remove(strExpr.Length - 1);
                strExpr += ")";
                strExpr = "dotktstatus NOT IN " + strExpr;
                //strExpr = "dotktstatus >= 21 AND dotktstatus <= 26"; //code cu
                // Use the Select method to find all rows matching the filter.
                //DataRow[] foundRows = dsNhomKiemToan.Tables[0].Select(strExpr);  
                dsNhomKiemToan.Tables[0].DefaultView.RowFilter = strExpr;
            }
            DataView dv = dsNhomKiemToan.Tables[0].DefaultView;
            dv.RowFilter = strExpr;
            dv.Sort = sort;
            DataSet newDS = new DataSet();
            DataTable newDT = dv.ToTable();
            newDS.Tables.Add(newDT);
            return newDS;
        }

        /// <summary>
        /// danh sach phan hoi theo phat hienID
        /// </summary>
        /// <param name="phatHienID"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet getListPhanHoi(string phatHienID)
        {
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = doankiemtoan.GetDanhSachPhanHoi(phatHienID);
            if (isValidDataSet(ds))
                return ds;
            return null;
        }

        /// <summary>
        /// danh sach phan hoi theo phat hienID va kieu user(nguoiduyet+nguoithuc hien)
        /// </summary>
        /// <param name="phatHienID"></param>
        /// <param name="UserType">example: nguoiduyet,nguoi thuc hien</param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet getListPhanHoi(string phatHienID, string UserType)
        {
            UserContext currentUser = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = doankiemtoan.GetDanhSachPhanHoiByUserType(phatHienID, UserType);

            if (isValidDataSet(ds))
            {
                //if (UserType.Equals("nguoithuchien"))
                //{
                //    string strExpr = "nguoi_nhap = '" + currentUser.UserName + "'";
                //    DataView dv = ds.Tables[0].DefaultView;
                //    dv.RowFilter = strExpr;
                //    DataSet newDS = new DataSet();
                //    DataTable newDT = dv.ToTable();
                //    newDS.Tables.Add(newDT);
                //    return newDS;
                //}
                //else if (UserType.Equals("nguoiduyet"))
                //{

                //}
                return ds;
            }

            return null;
        }

        /// <summary>
        /// danh sach files theo documentID
        /// </summary>
        /// <param name="PhatHienID"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet getAttachFileList(string PhatHienID)
        {
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = doankiemtoan.GetFilesByDoc(PhatHienID);
            if (isValidDataSet(ds))
                return ds;
            return null;
        }

        /// <summary>
        /// lay ds thanhvien trong truong doan or truong nhom
        /// </summary>
        /// <param name="Status">Status=1(truongdoan),Status=2(Truongnhom)</param>
        /// <param name="TruongDoan">username truongphong</param>
        /// <param name="TruongNhom">username truongnhom</param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getListThanhVien(string Status, string TruongDoan, string TruongNhom)
        {
            //lay danh sach trong doan
            if (Status == "1")
            {
                if (String.IsNullOrEmpty(TruongDoan))
                    return null;
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachThanhVienDoanKT(TruongDoan);
                if (isValidDataSet(ds))
                {
                    return ds.Tables[0];
                }
            }
            //lay danh sach trong nhom
            if (Status == "2")
            {
                if (String.IsNullOrEmpty(TruongNhom))
                    return null;
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                DataSet ds = doankiemtoan.DanhSachThanhVienNhomKT(TruongNhom);
                if (!isValidDataSet(ds))
                    return null;
                DataTable dtDoanKT = ds.Tables[0];
                dtDoanKT.Columns.Add("TenMangNghiepVu", typeof(string));
                dtDoanKT.Columns.Add("MangNghiepVuID", typeof(string));
                foreach (DataRow row in dtDoanKT.Rows)
                {
                    row["TenMangNghiepVu"] = CommonFunc.getPropertyValueOnDocument(row["MangNghiepVu"].ToString(), "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");
                    row["MangNghiepVuID"] = row["MangNghiepVu"];
                }
                return dtDoanKT;
            }
            return null;
        }

        /// <summary>
        /// danh sach thu tuc theo cong viec
        /// </summary>
        /// <param name="DocumentID">ma cong viec</param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getListThuTucByCongViec(string DocumentID)
        {
            if (!String.IsNullOrEmpty(DocumentID))
            {
                string DocFields = "PK_DocumentID,[Tên thủ tục kiểm toán]";
                string PropertyFields = "Tên thủ tục kiểm toán";
                string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + DocumentID + "')";
                bus_Document obj = new bus_Document(_objUserContext);
                DataSet ds = obj.getDocumentList(DOCTYPE.THUTUC_KIEMTOAN, DocFields, PropertyFields, Condition);
                if (isValidDataSet(ds))
                {
                    DataTable dt = ds.Tables[0];
                    dt.Columns.Add("FK_DocLinkID", typeof(string));
                    foreach (DataRow row in dt.Rows)
                        row["FK_DocLinkID"] = DocumentID;
                    return dt;
                }
            }
            return null;
        }

        /// <summary>
        /// danh sach mangnghiepvu theo mot danhsach nhomkt
        /// </summary>
        /// <param name="ds_nhomkt">danhs sach nhomkt</param>
        /// <param name="ds_mangnghiepvu_doclink"></param>
        /// <param name="mangnghiepvu"></param>
        /// <param name="row_mangnghiepvu"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        static DataRow GetMangNghiepVuAndNhomKT(DataSet ds_nhomkt, DataSet ds_mangnghiepvu_doclink, string mangnghiepvu, DataRow row_mangnghiepvu)
        {
            foreach (DataRow row_nhomkt in ds_nhomkt.Tables[0].Rows)
            {
                DataRow isExist = isExistMangNghiepVuAndNhomKT(ds_mangnghiepvu_doclink, mangnghiepvu, row_nhomkt["PK_DocumentID"].ToString());
                if (isExist != null)
                {
                    row_mangnghiepvu["NhomKiemToan_ID"] = row_nhomkt["PK_DocumentID"];
                    row_mangnghiepvu["NhomKiemToan"] = isExist["TruongNhom"];
                    bus_User obj_user = new bus_User(_objUserContext);
                    DataSet dsUser = obj_user.getByID(isExist["FK_DocumentID"].ToString(), String.Empty);
                    if (isValidDataSet(dsUser))
                        row_mangnghiepvu["ThanhVien"] = dsUser.Tables[0].Rows[0]["Name"];
                    return row_mangnghiepvu;
                }
            }
            row_mangnghiepvu["NhomKiemToan_ID"] = String.Empty;
            row_mangnghiepvu["NhomKiemToan"] = String.Empty;
            row_mangnghiepvu["ThanhVien"] = String.Empty;
            return row_mangnghiepvu;
        }

        /// <summary>
        /// kiem trang mangnghiepvu co ton tai
        /// </summary>
        /// <param name="ds_mangnghiepvu_doclink"></param>
        /// <param name="mangnghiepvu"></param>
        /// <param name="nhomkiemtoan_ID"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        static DataRow isExistMangNghiepVuAndNhomKT(DataSet ds_mangnghiepvu_doclink, string mangnghiepvu, string nhomkiemtoan_ID)
        {
            DataRow[] rows = ds_mangnghiepvu_doclink.Tables[0].Select("ADDTIONAL_DATA1='" + mangnghiepvu + "' And FK_DocLinkID='" + nhomkiemtoan_ID + "'");
            return (rows.Length > 0) ? rows[0] : null;
        }

        /// <summary>
        /// lay dotkt ma exist congviec trong do
        /// </summary>
        /// <param name="formatUserAssign"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getDotKTByCongViec(string FormatUserAssign)
        {
            //lay dotkt
            string DocFields = "PK_DocumentID,[Tên đợt kiểm toán],[Đối tượng kiểm toán],[Quy mô đợt kiểm toán],[Đơn vị thực hiện],[Năm],[Thời gian dự kiến kiểm toán],[Đơn vị thực hiện],[Loại đối tượng kiểm toán],Status";
            string PropertyFields = "Tên đợt kiểm toán,Đối tượng kiểm toán,Quy mô đợt kiểm toán,Đơn vị thực hiện,Năm,Thời gian dự kiến kiểm toán,Đơn vị thực hiện,Loại đối tượng kiểm toán";
            string Condition = string.Empty;
            bus_Document doc = new bus_Document((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsDotKT = doc.getDocumentList(DOCTYPE.DOT_KIEMTOAN, DocFields, PropertyFields, Condition);
            if (!isValidDataSet(dsDotKT))
                return null;
            //Dinh nghia datatable co cau truc giong dotkt
            DataTable dtDotKT = dsDotKT.Tables[0].Clone();
            dtDotKT.Columns.Add("DoanKT", typeof(string));
            dtDotKT.Columns.Add("NguoiDuyet", typeof(string));
            dtDotKT.Columns.Add("TruongDoan", typeof(string));
            foreach (DataRow rowDotKT in dsDotKT.Tables[0].Rows)
            {
                //check doankt co ton tai nguoithuchien?
                string DocFields_DoanKT = "PK_DocumentID,[Name]";
                string PropertyFields_DoanKT = "Name";
                string Condition_DoanKT = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + rowDotKT["PK_DocumentID"].ToString() + "')";
                DataSet dsDoanKT = doc.getDocumentList(DOCTYPE.DOAN_KIEMTOAN, DocFields_DoanKT, PropertyFields_DoanKT, Condition_DoanKT);
                if (isValidDataSet(dsDoanKT))
                {
                    foreach (DataRow rowDoanKT in dsDoanKT.Tables[0].Rows)
                    {
                        if (isExistCongViecByDoanKT(rowDoanKT["PK_DocumentID"].ToString(), FormatUserAssign))
                        {
                            DataRow newRow = dtDotKT.NewRow();
                            newRow["PK_DocumentID"] = rowDotKT["PK_DocumentID"];
                            newRow["Tên đợt kiểm toán"] = rowDotKT["Tên đợt kiểm toán"];
                            newRow["Đối tượng kiểm toán"] = rowDotKT["Đối tượng kiểm toán"];
                            newRow["Quy mô đợt kiểm toán"] = rowDotKT["Quy mô đợt kiểm toán"];
                            newRow["Đơn vị thực hiện"] = rowDotKT["Đơn vị thực hiện"];
                            newRow["Năm"] = rowDotKT["Năm"];
                            newRow["Thời gian dự kiến kiểm toán"] = rowDotKT["Thời gian dự kiến kiểm toán"];
                            newRow["Đơn vị thực hiện"] = rowDotKT["Đơn vị thực hiện"];
                            newRow["Loại đối tượng kiểm toán"] = rowDotKT["Loại đối tượng kiểm toán"];
                            newRow["DoanKT"] = rowDoanKT["PK_DocumentID"];
                            newRow["TruongDoan"] = rowDoanKT["Name"];
                            dtDotKT.Rows.Add(newRow);
                            //break;
                        }
                    }
                }
            }
            //thangma
            //Sort kết quả theo năm
            DataView dv = dtDotKT.DefaultView;
            dv.Sort = "Năm desc";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;
        }

        /// <summary>
        /// formatUserAssign = 'Người thực hiện','Người duyệt 1','Người duyệt 2'
        /// </summary>
        /// <param name="doanKT">id doankt</param>
        /// <param name="formatUserAssign"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        static bool isExistCongViecByDoanKT(string doanKT, string formatUserAssign)
        {
            //if (doanKT == "a23287e7-6793-4de7-c5f6-00f9d0c63e95")
            //{
            //    string str = "";
            //}
            //truong hop nguoithuchien
            string DocFields_CV = "PK_DocumentID,[Tên công việc],[Người thực hiện],[Người duyệt 1],[Người duyệt 2],[Ngày bắt đầu],[Ngày kết thúc],Status";
            string PropertyFields_CV = "Tên công việc,Người thực hiện,Người duyệt 1,Người duyệt 2,Ngày bắt đầu,Ngày kết thúc,Status";
            string Condition_CV = String.Empty;
            string userNameLogin = ((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName;
            if (formatUserAssign == "Người thực hiện")
                Condition_CV = " and [" + formatUserAssign + "]=N'" + userNameLogin + "' and PK_DocumentID In (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doanKT + "') ";//and (Status>= 2 and Status<= 64)
            //if (formatUserAssign == "Người duyệt 1")
            //    Condition_CV = " and [" + formatUserAssign + "]=N'" + ((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName + "' and PK_DocumentID In (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doanKT + "') and (Status = 16 or Status = 32)";
            //if (formatUserAssign == "Người duyệt 2")
            //    Condition_CV = " and [" + formatUserAssign + "]=N'" + ((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName + "' and PK_DocumentID In (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doanKT + "') and (Status = 32 or Status = 64)";
            if (formatUserAssign == "Người duyệt")
            {
                Condition_CV = " and ([Người duyệt 1]=N'" + userNameLogin + "' " +
                               " or [Người duyệt 2]=N'" + userNameLogin + "') " +
                               "  and PK_DocumentID In (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doanKT + "') and (Status > 8)";//chi nhin thay nhung user ko bi rejected tro di//>4 
            }
            bus_Document doc = new bus_Document((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsCV = doc.getDocumentList(DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN, DocFields_CV, PropertyFields_CV, Condition_CV);
            if (!isValidDataSet(dsCV))
                return false;
            return dsCV.Tables[0].Rows.Count > 0 ? true : false;
        }

        /// <summary>
        /// lay phat hientheo dotkiemtoan
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        /// <param name="DocFields"></param>
        /// <param name="PropertyFields"></param>
        /// <param name="Condition"></param>
        /// <param name="TuNgay"></param>
        /// <param name="DenNgay"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getListPhatHien(string TenDotKiemToan, string DotKiemToan_ID, string DoanKiemToan_ID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PK_DocumentID", typeof(string));
            dt.Columns.Add("PhatHien", typeof(string));
            dt.Columns.Add("Kieu", typeof(string));//Phan biet phan biet phat hien HeThong(HT),ViPham(VP)
            dt.Columns.Add("MucDo", typeof(string));
            dt.Columns.Add("ChiTiet", typeof(string));
            dt.Columns.Add("AnhHuong", typeof(string));
            dt.Columns.Add("GhiChu", typeof(string));
            dt.Columns.Add("KhuyenNghi", typeof(string));
            dt.Columns.Add("FileDinhKem", typeof(string));
            dt.Columns.Add("Status", typeof(string));//trang thai phat hien

            dt.Columns.Add("DotKiemToanID", typeof(string));
            dt.Columns.Add("DotKiemToan", typeof(string));


            //lay tat ca HeThong(co file dinhkem)
            string DocFields_HT = "PK_DocumentID,[Nguyên nhân],[Phát hiện],[Đối tượng kiểm toán],[Mức độ],[Ảnh hưởng],[Ghi chú],[Chi tiêt],[Khuyến nghị],[Đợt kiểm toán],Status";
            string PropertyFields_HT = "Nguyên nhân,Phát hiện,Đối tượng kiểm toán,Mức độ,Ảnh hưởng,Ghi chú,Chi tiêt,Khuyến nghị,Đợt kiểm toán";
            string Condition_HT = " And [Đợt kiểm toán]=N'" + TenDotKiemToan + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet dsHT = doc.getDocumentList(DOCTYPE.PHATHIEN_HETHONG, DocFields_HT, PropertyFields_HT, Condition_HT);
            if (isValidDataSet(dsHT))
            {
                foreach (DataRow rowHT in dsHT.Tables[0].Rows)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["PK_DocumentID"] = rowHT["PK_DocumentID"];
                    newRow["PhatHien"] = rowHT["Phát hiện"];
                    newRow["Kieu"] = "HT";
                    newRow["MucDo"] = rowHT["Mức độ"];
                    newRow["ChiTiet"] = rowHT["Chi tiêt"];
                    newRow["AnhHuong"] = rowHT["Ảnh hưởng"];
                    newRow["GhiChu"] = rowHT["Ghi chú"];
                    newRow["KhuyenNghi"] = rowHT["Khuyến nghị"];
                    newRow["Status"] = rowHT["Status"];

                    newRow["DotKiemToanID"] = GetThongTinDotKiemToan(rowHT["Đợt kiểm toán"].ToString(), "PK_DocumentID");
                    newRow["DotKiemToan"] = rowHT["Đợt kiểm toán"];

                    dt.Rows.Add(newRow);
                }
            }
            //lay tat ca ViPham(ko file dinhkem)
            string DocFields_VP = "PK_DocumentID,[Nguyên nhân],[Phát hiện],[Đối tượng kiểm toán],[Mức độ],[Ảnh hưởng],[Ghi chú],[Chi tiết],[Khuyến nghị],[Đợt kiểm toán],Status";
            string PropertyFields_VP = "Nguyên nhân,Phát hiện,Đối tượng kiểm toán,Mức độ,Ảnh hưởng,Ghi chú,Chi tiết,Khuyến nghị,Đợt kiểm toán";
            string Condition_VP = " And [Đợt kiểm toán]=N'" + TenDotKiemToan + "'";
            DataSet dsVP = doc.getDocumentList(DOCTYPE.PHATHIEN_VIPHAM, DocFields_VP, PropertyFields_VP, Condition_VP);
            if (isValidDataSet(dsVP))
            {
                foreach (DataRow rowVP in dsVP.Tables[0].Rows)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["PK_DocumentID"] = rowVP["PK_DocumentID"];
                    newRow["PhatHien"] = rowVP["Phát hiện"];
                    newRow["Kieu"] = "VP";
                    newRow["MucDo"] = rowVP["Mức độ"];
                    newRow["ChiTiet"] = rowVP["Chi tiết"];
                    newRow["AnhHuong"] = rowVP["Ảnh hưởng"];
                    newRow["GhiChu"] = rowVP["Ghi chú"];
                    newRow["KhuyenNghi"] = rowVP["Khuyến nghị"];
                    newRow["Status"] = rowVP["Status"];

                    newRow["DotKiemToanID"] = GetThongTinDotKiemToan(rowVP["Đợt kiểm toán"].ToString(), "PK_DocumentID");
                    newRow["DotKiemToan"] = rowVP["Đợt kiểm toán"];

                    dt.Rows.Add(newRow);
                }
            }
            return dt;
        }

        /// <summary>
        /// lay HSRR theo congviec
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        /// <param name="DocFields"></param>
        /// <param name="PropertyFields"></param>
        /// <param name="Condition"></param>
        /// <param name="TuNgay"></param>
        /// <param name="DenNgay"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getHoSoRuiRoTheoCongViec(string CongViecID, string DoanKiemToan)
        {
            bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsThuTuc = doanKiemToan.GetThuTucInfoByCongViec(CongViecID);
            if (isValidDataSet(dsThuTuc) == false) return null;

            DataTable dtNhomKT = GetNhomKiemToanTheoCongViec(CongViecID, DoanKiemToan);
            if (dtNhomKT == null) return null;
            if (dtNhomKT.Rows.Count == 0) return null;

            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            //dinh nghia temp
            //DataTable dt = lapKeHoach.ChiTietHoSoPhanTichRuiRo("-1").Tables[0];
            DataTable dtHSRR = new DataTable();
            dtHSRR.Columns.Add("mang_nghiep_vu", typeof(String));
            dtHSRR.Columns.Add("muc_tieu_kiem_soat", typeof(String));
            dtHSRR.Columns.Add("rui_ro", typeof(String));
            dtHSRR.Columns.Add("xac_suat", typeof(String));
            dtHSRR.Columns.Add("anh_huong", typeof(String));
            dtHSRR.Columns.Add("ruiroid", typeof(String));
            dtHSRR.Columns.Add("rui_ro_con_lai", typeof(String));

            dtHSRR.Columns.Add("STATUS", typeof(String));
            dtHSRR.Columns.Add("hoso", typeof(String));

            foreach (DataRow rowNhomKT in dtNhomKT.Rows)
            {
                string nhomkt = rowNhomKT["NhomKiemToan_ID"].ToString();
                if (!String.IsNullOrEmpty(nhomkt))
                {
                    DataSet dsHoSoRuiRo = lapKeHoach.ChiTietHoSoPhanTichRuiRo(nhomkt);
                    if (isValidDataSet(dsHoSoRuiRo))
                    {
                        foreach (DataRow rowrr in dsHoSoRuiRo.Tables[0].Rows)
                        {
                            if (isExistMNV(rowrr["mang_nghiep_vu"].ToString(), dsThuTuc.Tables[0]))
                                dtHSRR.ImportRow(rowrr);
                        }

                    }
                }
            }
            return dtHSRR;
        }

        /// <summary>
        /// kiem trang mangnghiepvu co ton tai
        /// </summary>
        /// <param name="ds_mangnghiepvu_doclink"></param>
        /// <param name="mangnghiepvu"></param>
        /// <param name="nhomkiemtoan_ID"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        public static bool isExistMNV(string tenmangnghiepvu, DataTable dtThuTucTheoCV)
        {
            foreach (DataRow rowTT in dtThuTucTheoCV.Rows)
                if (rowTT["ten_mang_nghiep_vu"].ToString() == tenmangnghiepvu)
                    return true;
            return false;
        }

        public static DataTable GetNhomKiemToanTheoCongViec(string CongViecID, string DoanKiemToan)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NhomKiemToan_ID", typeof(string));

            //tu congviec->thutuc->mangnghiepvu->nhomkt
            bus_Doankiemtoan obj = new bus_Doankiemtoan((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsThuTuc = obj.GetThuTucInfoByCongViec(CongViecID);
            if (isValidDataSet(dsThuTuc))
            {
                DataTable dtThuTuc = dsThuTuc.Tables[0];
                //lay nhomkt theo mangnghiepvu;
                DataTable dtMangNghiepVu = DataSource.getListMangNghiepVu(DoanKiemToan);
                foreach (DataRow rowMNV in dtMangNghiepVu.Rows)
                {
                    foreach (DataRow rowTT in dtThuTuc.Rows)
                    {
                        if (rowMNV["PK_DocumentID"].ToString() == rowTT["mangnghiepvu_id"].ToString())
                        {
                            DataRow newRow = dt.NewRow();
                            newRow["NhomKiemToan_ID"] = rowMNV["NhomKiemToan_ID"].ToString();
                            dt.Rows.Add(newRow);
                        }
                    }
                }

            }
            if (dt.Rows.Count > 0)
            {
                DataView view = new DataView(dt);
                DataTable dtNew = view.ToTable(true, "NhomKiemToan_ID");
                return dtNew;
            }
            return dt;
        }

        /// <summary>
        /// danh sach mang nghiep cua dotkt
        /// </summary>
        /// <param name="DotKT"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet GetMangNghiepVuByDotKT(string DotKT)
        {
            if (String.IsNullOrEmpty(DotKT))
                return null;
            bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsMangNghiepVu = doanKiemToan.GetMangNghiepVuByDotKT(DotKT);
            if (isValidDataSet(dsMangNghiepVu))
                return dsMangNghiepVu;
            return null;
        }

        /// <summary>
        /// danh sach phantichsobo theo nhomkt
        /// </summary>
        /// <param name="NhomKiemToan"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataSet getListHoSoPhanTichSoBo(string NhomKiemToan)
        {
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = lapKeHoach.ChiTietHoSoPhanTichSoBo(NhomKiemToan);
            return ds;
        }

        /// <summary>
        /// danh sach phantichchitiet theo nhomkt
        /// </summary>
        /// <param name="NhomKiemToan"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getListHoSoPhanTichChiTiet(string NhomKiemToan, string sort)
        {
            if (String.IsNullOrEmpty(NhomKiemToan))
                return null;
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            UserContext uContext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];
            DataSet ds = lapKeHoach.ChiTietHoSoPhanTichRuiRo(NhomKiemToan, uContext.UserName);
            if (isValidDataSet(ds))
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = sort;
                return dv.ToTable();
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// danh sach phantichchitiet theo trưởng đoàn
        /// </summary>
        /// <param name="NhomKiemToan"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getListHoSoPhanTichChiTietByTruongDoan(string DotKiemToanID, string sort)
        {
            if (String.IsNullOrEmpty(DotKiemToanID))
                return null;

            //bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            //UserContext uContext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];
            //DataSet ds = lapKeHoach.ChiTietHoSoPhanTichRuiRo(NhomKiemToan, uContext.UserName);
            //if (isValidDataSet(ds))
            //{
            //    DataView dv = ds.Tables[0].DefaultView;
            //    dv.Sort = sort;
            //    return dv.ToTable();
            //}
            //return ds.Tables[0];
            //bool DaPhanTichHetPTRR = false;
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsDanhSachDoan = doankiemtoan.DanhSachDoanKTByDotKT(DotKiemToanID);
            DataSet dsResult = new DataSet();
            int count = 0;
            if (isValidDataSet(dsDanhSachDoan))
            {
                //lấy đoàn của đợt
                string DoanKiemToanID = dsDanhSachDoan.Tables[0].Rows[0]["DoanKiemToanID"].ToString();
                DataSet dsDanhSachNhom = doankiemtoan.DanhSachNhomByDoanKT(DoanKiemToanID);
                if (isValidDataSet(dsDanhSachNhom))
                {
                    foreach (DataRow rowNhom in dsDanhSachNhom.Tables[0].Rows)
                    {
                        string NhomKiemToanID = rowNhom["NhomID"].ToString();
                        //lấy các hồ sơ rủi ro
                        DataSet dsHoSoRuiRo = lapKeHoach.ChiTietHoSoPhanTichRuiRo(NhomKiemToanID);
                        if (count == 0)
                        {
                            dsResult = dsHoSoRuiRo.Copy();
                        }
                        else
                        {
                            foreach (DataRow row in dsHoSoRuiRo.Tables[0].Rows)
                            {
                                dsResult.Tables[0].ImportRow(row);
                            }
                        }
                        count++;
                    }
                }
            }
            return dsResult.Tables[0];
        }
        /// <summary>
        /// danh sach phantichchitiet theo nhomkt
        /// </summary>
        /// <param name="NhomKiemToan"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public static DataTable getListHoSoPhanTichRuiRo(string NhomKiemToan)
        {
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance(_objUserContext);
            DataSet ds = lapKeHoach.ChiTietHoSoPhanTichRuiRo(NhomKiemToan, ((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName);
            if (isValidDataSet(ds))
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = "mang_nghiep_vu ASC";
                return dv.ToTable();
            }
            return null;
        }

        #endregion

        #region Helper
        static bool isValidDataSet(DataSet ds)
        {
            bool valid = false;
            if (ds != null)
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        valid = true;
            return valid;
        }

        private static DataSet RemoveRow(DataTable dt, int rowIndex)
        {
            try
            {
                DataSet ds1 = new DataSet();
                DataTable dt1 = dt;
                dt1.Rows[rowIndex].Delete();
                dt1.AcceptChanges();
                ds1.Tables.Add(dt1.Copy());
                ds1.Tables[0].AcceptChanges();
                return ds1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static bool isExists(string fk_promotion_number, string cust_id, string rankid)
        {
            int n = 0;

            string SQL = "select count(1) from";
            SQL += " (select fk_promotion_number=fk_promotion_number,cust_id from t_new_number";
            SQL += " where new_number='" + fk_promotion_number + "' and cust_id='" + cust_id + "'";
            SQL += " union all";
            SQL += " select fk_promotion_number=new_number,cust_id from t_new_number";
            SQL += " where fk_promotion_number='" + fk_promotion_number + "' and cust_id='" + cust_id + "'";
            SQL += " union all";
            SQL += " select fk_promotion_number=fk_promotion_number,cust_id from t_promotion_rank_customer";
            SQL += " where fk_promotion_number='" + fk_promotion_number + "' and cust_id='" + cust_id + "') T";
            SQL += " inner join t_promotion_rank_customer rc on T.fk_promotion_number=rc.fk_promotion_number and T.cust_id=rc.cust_id";
            SQL += " where rc.fk_rankid='" + rankid + "'";

            SqlConnection objCon = new SqlConnection(_objUserContext.ConnectionString);
            SqlCommand objCmd = new SqlCommand(SQL, objCon);
            try
            {
                objCon.Open();
                n = Convert.ToInt32(objCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                objCon.Close();
            }
            return (n == 1 ? true : false);
        }
        #endregion
    }
}

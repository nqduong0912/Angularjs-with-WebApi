using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using CORE.UMS.CoreBusiness;
using System.Data;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class BienBanKetThucKiemToan : PageBase
    {
        protected string _action = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _nhomkt = string.Empty;
        protected string _doankt = string.Empty;
        protected string _reportformat = string.Empty;
        protected string _doctypeid = DOCTYPE.CHITIET_HOSO_PHANTICH_SOBO;
        protected string _hosoChiTiet = string.Empty;
        protected string _jsonBaocaoData = string.Empty;
        DataSet dsDotKTInfo = new DataSet();
        int _status_dotkt = 0;

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            #region get data submit
            _nhomkt = Request["doc"];
            _dotkt = Request["dotkt"];
            _action = Request["act"];
            _jsonBaocaoData = Request["jsonbaocaodata"];
            _reportformat = Request["reportformat"];
            _hosoChiTiet = Request["hosochitiet"];
            if (!string.IsNullOrEmpty(_nhomkt))
            {
                //bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
                //dsDotKTInfo = doanKiemToan.GetDotKTByNhomKT(_nhomkt);
                //if (isValidDataSet(dsDotKTInfo))
                //{
                //    _dotkt = dsDotKTInfo.Tables[0].Rows[0]["dotkt"].ToString();
                //    _doankt = dsDotKTInfo.Tables[0].Rows[0]["dotkt"].ToString();
                //}
            }

            #endregion

            //if (string.IsNullOrEmpty(_documentid))
            //    _viewtype = VIEWTYPE.ADDNEW;
            //else
            //    _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                else if (_action == "xoachitietphantich")
                {
                    FeedBackClient(xoaChiTietPhanTich(_hosoChiTiet, _nhomkt));
                }
                else if (_action == "submit")
                {
                    FeedBackClient(Submit(_nhomkt));
                }
                //len trang thai Phân tích sơ bộ
                else if (_action == "chuyentrangthaidotkt")
                {
                    //FeedBackClient(CommonFunc.ChuyenTrangThaiDotKT(_dotkt, 13));
                }
                else if (_action == "getdotinfo")
                {
                    //FeedBackClient(CommonFunc.ChuyenTrangThaiDotKT(_dotkt, 13));
                }
                else if (_action == "baocao")
                {
                    FeedBackClient(BaoCaoSuaPhatHien1());
                }
                else if (_action == "getdatamodel")
                {
                    FeedBackClient(GetDataModel());
                }
            }

            #endregion

            #region init form
            string caption = "Biên bản kết thúc kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            //_btnAddNew.Visible = true;
            //_btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            #endregion

            #region client control event handler
            //Truongphong->tao doan kiem toan
            //if (_m_roleID == ROLES.TRUONGPHONG_DONVI_KIEMTOAN)
            //    btnDongy.Visible = btnThem.Visible = true;
            //else
            //    btnDongy.Visible = btnThem.Visible = false;

            //btnThem.Attributes.Add("onclick", "{ThemThanhVien(); return false;}");
            //thangma
            _btnSave.Visible = false;
            _btnSave.Text = "Báo cáo";
            _btnSave.Attributes.Add("onclick", "{BaoCao(); return false;}");
            #endregion
        }
        public string xoaChiTietPhanTich(string hoso_chitiet, string nhomkt)
        {
            CommonFunc.RemoveDocLink(hoso_chitiet, nhomkt, _objUserContext);
            return string.Empty;
        }
        public string ReturnDotInfo(string DotKiemToanID)
        {
            string sDoiTuongKiemToan = CommonFunc.getPropertyValueOnDocument(DotKiemToanID, "");
            string sDonViKiemToan = CommonFunc.getPropertyValueOnDocument(DotKiemToanID, "");

            return string.Empty;
        }
        public string EditBaoCao()
        {
            string result = string.Empty;
            //kiem tra xem dot kiem toan co o trang thai 21 - 26 khong ?
            var DotStatus = CommonFunc.GetDocStatus(_dotkt);
            if (DotStatus >= 21 && DotStatus <= 26)
            {
                //lấy dữ liệu từ DB về cho báo cáo
                bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
                DataSet dsBienBan = obj.GetBienBanKetThucKTChiTiet(_dotkt);
                dsBienBan.Tables[0].TableName = "BienBanKTKT";

                //test JSON
                List<PhatHienBaoCaoBienBan> phatHienList = new List<PhatHienBaoCaoBienBan>();
                DataView view = new DataView(dsBienBan.Tables[0]);
                DataTable distinctValues = view.ToTable(true, "PhatHienID", "phat_hien", "muc_do", "chi_tiet", "phan_hoi");

                foreach (DataRow row in distinctValues.Rows)
                {
                    string trangthai = CommonFunc.GetTrangThaiPhatHien(row["PhatHienID"].ToString());
                    PhatHienBaoCaoBienBan phathien = new PhatHienBaoCaoBienBan(row["PhatHienID"].ToString(), row["phat_hien"].ToString(),
                        row["muc_do"].ToString(), row["chi_tiet"].ToString(), row["phan_hoi"].ToString(),trangthai);
                    phatHienList.Add(phathien);
                }
                result = JSONHelper.Serialize<List<PhatHienBaoCaoBienBan>>(phatHienList);
            }
            else
            {
                result = "0";
            }

            return result;
        }
        public string GetDataModel()
        {
            string result = string.Empty;
            //kiem tra xem dot kiem toan co o trang thai 21 - 26 khong ?
            var DotStatus = CommonFunc.GetDocStatus(_dotkt);
            if (DotStatus >= 21 && DotStatus <= 26)
            {
                //lấy dữ liệu từ DB về cho báo cáo
                bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
                DataSet dsBienBan = obj.GetBienBanKetThucKTChiTiet(_dotkt);
                dsBienBan.Tables[0].TableName = "BienBanKTKT";

                //test JSON
                List<MangNghiepVuInBienBan> mangNghiepVuList = new List<MangNghiepVuInBienBan>();
                DataView view = new DataView(dsBienBan.Tables[0]);
                DataTable distinctValues = view.ToTable(true, "MangNghiepVuID", "ten_mang_nghiep_vu");

                foreach (DataRow row in distinctValues.Rows)
                {
                    //PhatHienBaoCaoBienBan phathien = new PhatHienBaoCaoBienBan(row["PhatHienID"].ToString(), row["phat_hien"].ToString(),
                    //    row["muc_do"].ToString(), row["chi_tiet"].ToString(), row["phan_hoi"].ToString());
                    //phatHienList.Add(phathien);
                    MangNghiepVuInBienBan mangNghiepVu = new MangNghiepVuInBienBan();
                    mangNghiepVu.PhatHienBienBanList = new List<PhatHienBaoCaoBienBan>();
                    mangNghiepVu.MangNghiepVuID = row["MangNghiepVuID"].ToString();
                    mangNghiepVu.TenMangNghiepVu = row["ten_mang_nghiep_vu"].ToString();
                    DataRow[] foundRows;
                    string expression = "MangNghiepVuID = '" + row["MangNghiepVuID"].ToString() + "'";
                    foundRows = dsBienBan.Tables[0].Select(expression);
                    foreach (DataRow dtrow in foundRows)
                    {
                        //assign Phat hien Info
                        PhatHienBaoCaoBienBan phathien = new PhatHienBaoCaoBienBan();
                        phathien.DanChieu = dtrow["chi_tiet"].ToString();
                        phathien.MucDo = dtrow["muc_do"].ToString();
                        phathien.PhanHoiDonVi = dtrow["phan_hoi"].ToString();
                        phathien.PhatHienID = dtrow["PhatHienID"].ToString();
                        phathien.TenPhatHien = dtrow["phat_hien"].ToString();
                        phathien.TrangThai = CommonFunc.GetTrangThaiPhatHien(dtrow["PhatHienID"].ToString());
                        mangNghiepVu.PhatHienBienBanList.Add(phathien);
                    }
                    mangNghiepVuList.Add(mangNghiepVu);
                }
                result = JSONHelper.Serialize<List<MangNghiepVuInBienBan>>(mangNghiepVuList);
            }
            else
            {
                result = "0";
            }

            return result;
        }
        public string BaoCaoSuaPhatHien()
        {
            string ErrorMessage = string.Empty;
            //kiem tra xem dot kiem toan co o trang thai 21 - 26 khong ?
            var DotStatus = CommonFunc.GetDocStatus(_dotkt);
            if (DotStatus >= 21 && DotStatus <= 26)
            {
                string sTenDotKiemToan = CommonFunc.getPropertyValueOnDocument(_dotkt, "63A0C4B1-2088-4994-B891-2FF65EB20265");
                string sDoiTuongKiemToan = CommonFunc.getPropertyValueOnDocument(_dotkt, "2A4CA2AD-0282-4D57-86AC-D973D281EF54");
                string sTenDonViThucHienKiemToan = CommonFunc.getPropertyValueOnDocument(_dotkt, "A8A3EDBA-F569-4C06-8A57-045FBFED55FD");
                //lay du lieu phat hien da sua tu client
                if (_jsonBaocaoData.Length > 0)
                {
                    List<PhatHienBaoCaoBienBan> phatHienList = JSONHelper.Deserialize<List<PhatHienBaoCaoBienBan>>(_jsonBaocaoData);
                    string condition = string.Empty;
                    foreach (PhatHienBaoCaoBienBan phathien in phatHienList)
                    {
                        condition += "PhatHienID = '" + phathien.PhatHienID + "' OR ";
                    }
                    condition = condition.Remove(condition.Length - 3); ;


                    //lấy dữ liệu từ DB về cho báo cáo
                    bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
                    DataSet dsBienBan = obj.GetBienBanKetThucKTChiTiet(_dotkt);

                    dsBienBan.Tables[0].DefaultView.RowFilter = condition;

                    DataView dv = dsBienBan.Tables[0].DefaultView;
                    dv.RowFilter = condition;
                    DataSet newDS = new DataSet();
                    DataTable newDT = dv.ToTable();
                    newDS.Tables.Add(newDT);


                    foreach (DataRow row in newDS.Tables[0].Rows)
                    {
                        PhatHienBaoCaoBienBan phathien = phatHienList.Find(x => x.PhatHienID == row["PhatHienID"].ToString());
                        row["phat_hien"] = phathien.TenPhatHien;
                        row["chi_tiet"] = phathien.DanChieu;
                        row["muc_do"] = phathien.MucDo;
                        row["phan_hoi"] = phathien.PhanHoiDonVi;
                    }

                    //
                    newDS.Tables[0].TableName = "BienBanKTKT";

                    string export_path = "~/Modules/reports/client/";
                    //string export_type = "PDF";
                    string export_type = _reportformat;
                    string export_filename = "BienBanKTKT_" + Session.SessionID;
                    string rpt = "~/Modules/reports/data/rpt/BienBanKTKT.rpt";

                    //tham số báo cáo
                    SortedList<string, object> param = new SortedList<string, object>();
                    param.Add("TenDotKiemToan", sTenDotKiemToan);
                    param.Add("TenDoiTuongKiemToan", sDoiTuongKiemToan);
                    param.Add("TenDonViThucHienKiemToan", sTenDonViThucHienKiemToan);
                    DateTime today = DateTime.Now;
                    param.Add("Ngay", today.Day);
                    param.Add("Thang", today.Month);
                    param.Add("Nam", today.Year);
                    try
                    {
                        string urlreport = CommonFunc.do_report(rpt, export_path, newDS, param, export_type, export_filename, _objUserContext);
                        ErrorMessage = urlreport.Replace("~", "../..");
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = "-1:" + ex.Message;
                    }
                }
                else
                {
                    ErrorMessage = "0";
                }
            }
            else
            {
                ErrorMessage = "0";
            }

            return ErrorMessage;
        }
        public string BaoCaoSuaPhatHien1()
        {
            string ErrorMessage = string.Empty;
            //kiem tra xem dot kiem toan co o trang thai 21 - 26 khong ?
            var DotStatus = CommonFunc.GetDocStatus(_dotkt);
            if (DotStatus >= 21 && DotStatus <= 26)
            {
                string sTenDotKiemToan = CommonFunc.getPropertyValueOnDocument(_dotkt, "63A0C4B1-2088-4994-B891-2FF65EB20265");
                string sDoiTuongKiemToan = CommonFunc.getPropertyValueOnDocument(_dotkt, "2A4CA2AD-0282-4D57-86AC-D973D281EF54");
                string sTenDonViThucHienKiemToan = CommonFunc.getPropertyValueOnDocument(_dotkt, "A8A3EDBA-F569-4C06-8A57-045FBFED55FD");
                //lay du lieu phat hien da sua tu client
                if (_jsonBaocaoData.Length > 0)
                {
                    List<MangNghiepVuInBienBan> mangNghiepVuList = JSONHelper.Deserialize<List<MangNghiepVuInBienBan>>(_jsonBaocaoData);
                    //build DataSet for crystal report
                    DataSet dsReport = new DataSet();
                    DataTable dtReport = new DataTable();
                    dtReport.TableName = "BienBanKTKT";
                    dtReport.Columns.Add("DotID", typeof(String));
                    dtReport.Columns.Add("CongViecID", typeof(String));
                    dtReport.Columns.Add("MangNghiepVuID", typeof(String));
                    dtReport.Columns.Add("PhatHienID", typeof(String));
                    dtReport.Columns.Add("ten_mang_nghiep_vu", typeof(String));
                    dtReport.Columns.Add("phat_hien", typeof(String));
                    dtReport.Columns.Add("chi_tiet", typeof(String));
                    dtReport.Columns.Add("muc_do", typeof(String));
                    dtReport.Columns.Add("phan_hoi", typeof(String));
                    foreach (MangNghiepVuInBienBan mangnghiepvu in mangNghiepVuList)
                    {
                        foreach (PhatHienBaoCaoBienBan phathien in mangnghiepvu.PhatHienBienBanList)
                        {
                            DataRow rowReport = dtReport.NewRow();
                            rowReport["DotID"] = _dotkt;
                            rowReport["CongViecID"] = "";
                            rowReport["MangNghiepVuID"] = mangnghiepvu.MangNghiepVuID;
                            rowReport["PhatHienID"] = phathien.PhatHienID;
                            rowReport["ten_mang_nghiep_vu"] = mangnghiepvu.TenMangNghiepVu;
                            rowReport["phat_hien"] = phathien.TenPhatHien;
                            rowReport["chi_tiet"] = phathien.DanChieu;
                            rowReport["muc_do"] = phathien.MucDo;
                            rowReport["phan_hoi"] = phathien.PhanHoiDonVi;
                            dtReport.Rows.Add(rowReport);
                        }
                    }
                    dsReport.Tables.Add(dtReport);
                    string export_path = "~/Modules/reports/client/";
                    //string export_type = "PDF";
                    string export_type = _reportformat;
                    string export_filename = "BienBanKTKT_" + Session.SessionID;
                    string rpt = "~/Modules/reports/data/rpt/BienBanKTKT.rpt";

                    //tham số báo cáo
                    SortedList<string, object> param = new SortedList<string, object>();
                    param.Add("TenDotKiemToan", sTenDotKiemToan);
                    param.Add("TenDoiTuongKiemToan", sDoiTuongKiemToan);
                    param.Add("TenDonViThucHienKiemToan", sTenDonViThucHienKiemToan);
                    DateTime today = DateTime.Now;
                    param.Add("Ngay", today.Day);
                    param.Add("Thang", today.Month);
                    param.Add("Nam", today.Year);
                    try
                    {
                        string urlreport = CommonFunc.do_report(rpt, export_path, dsReport, param, export_type, export_filename, _objUserContext);
                        ErrorMessage = urlreport.Replace("~", "../..");
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = "-1:" + ex.Message;
                    }
                }
                else
                {
                    ErrorMessage = "0";
                }
            }
            else
            {
                ErrorMessage = "0";
            }

            return ErrorMessage;
        }
        public string BaoCao()
        {
            string ErrorMessage = string.Empty;
            //kiem tra xem dot kiem toan co o trang thai 21 - 26 khong ?
            var DotStatus = CommonFunc.GetDocStatus(_dotkt);
            if (DotStatus >= 21 && DotStatus <= 26)
            {
                string sTenDotKiemToan = CommonFunc.getPropertyValueOnDocument(_dotkt, "63A0C4B1-2088-4994-B891-2FF65EB20265");
                string sDoiTuongKiemToan = CommonFunc.getPropertyValueOnDocument(_dotkt, "2A4CA2AD-0282-4D57-86AC-D973D281EF54");
                string sTenDonViThucHienKiemToan = CommonFunc.getPropertyValueOnDocument(_dotkt, "A8A3EDBA-F569-4C06-8A57-045FBFED55FD");

                //lấy dữ liệu từ DB về cho báo cáo
                bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
                DataSet dsBienBan = obj.GetBienBanKetThucKTChiTiet(_dotkt);
                dsBienBan.Tables[0].TableName = "BienBanKTKT";

                string export_path = "~/Modules/reports/client/";
                //string export_type = "PDF";
                string export_type = _reportformat;
                string export_filename = "BienBanKTKT_" + Session.SessionID;
                string rpt = "~/Modules/reports/data/rpt/BienBanKTKT.rpt";

                //tham số báo cáo
                SortedList<string, object> param = new SortedList<string, object>();
                param.Add("TenDotKiemToan", sTenDotKiemToan);
                param.Add("TenDoiTuongKiemToan", sDoiTuongKiemToan);
                param.Add("TenDonViThucHienKiemToan", sTenDonViThucHienKiemToan);
                DateTime today = DateTime.Now;
                param.Add("Ngay", today.Day);
                param.Add("Thang", today.Month);
                param.Add("Nam", today.Year);
                try
                {
                    string urlreport = CommonFunc.do_report(rpt, export_path, dsBienBan, param, export_type, export_filename, _objUserContext);
                    ErrorMessage = urlreport.Replace("~", "../..");
                }
                catch (Exception ex)
                {
                    ErrorMessage = "-1:" + ex.Message;
                }
            }
            else
            {
                ErrorMessage = "0";
            }

            return ErrorMessage;
        }
        public string Submit(string nhomkt)
        {
            //CommonFunc.RemoveDocLink(hoso_chitiet, nhomkt, _objUserContext);
            bus_LapKeHoach lapkehoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsChiTietHoSoSoBo = lapkehoach.ChiTietHoSoPhanTichSoBo(nhomkt);
            if (isValidDataSet(dsChiTietHoSoSoBo))
            {
                foreach (DataRow row in dsChiTietHoSoSoBo.Tables[0].Rows)
                {
                    CommonFunc.UpdateDocStatus(row["hoso"].ToString(), 4);
                }
                //thangma: update Trang thai Dot kiem toan 
                //len trang thai phan tich chi tiet
                //_status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                //if (_status_dotkt < 14)
                //    CommonFunc.UpdateDocStatus(_dotkt, 14);
                //end
            }
            return string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindDotKiemToanCombobox();
                string selectedDotKT = ddlDotKiemToan.SelectedValue;
                //LoadDotKiemToanInfo(selectedDotKT);
                //BindDotInfo_MangNVGrid(selectedDotKT);
                List<KeyValuePair<string, string>> list = CommonFunc.MucDoPhatHienValue();
                string jsonMucDo = JSONHelper.Serialize<List<KeyValuePair<string, string>>>(list);
                hiddenMucDoPhatHien.Value = jsonMucDo;
            }

        }
        public void LoadDotKiemToanInfo(string DotKiemToanID)
        {
            txtDoiTuongKiemToan.Text = CommonFunc.getPropertyValueOnDocument(DotKiemToanID, "2A4CA2AD-0282-4D57-86AC-D973D281EF54");
            txtDonViKiemToan.Text = CommonFunc.getPropertyValueOnDocument(DotKiemToanID, "A8A3EDBA-F569-4C06-8A57-045FBFED55FD");
        }
        public void BindDotKiemToanCombobox()
        {
            ArrayList roles = _objUserContext.Roles;
            //if(roles.)
            CORE.CoreObjectContext.Role role = (CORE.CoreObjectContext.Role)roles[0];
            string roleID = role.RoleID;


            string DocFields_dotkt = "PK_DocumentID,[Đơn vị thực hiện],[Tên đợt kiểm toán],[Đối tượng kiểm toán]";
            string PropertyFields_dotkt = "Đơn vị thực hiện,Tên đợt kiểm toán,Đối tượng kiểm toán";
            string Condition_dotkt = string.Empty;

            //truong phong CSCC
            if (roleID.Equals("dc064bd2-35b2-4a39-a3f6-6b45f79a21b9"))
            {
                //lay tat ca cac dot kiem toan trong he thong

            }
            //truong phong KTNB
            else if (roleID.Equals("99a121fa-55c1-48be-b31b-c09c4102aba6"))
            {
                //lay cac dot kiem toan ma nguoi dang nhap la truong phong
                CORE.CoreObjectContext.Group group = (CORE.CoreObjectContext.Group)_objUserContext.Groups[0];
                string groupName = group.GroupName;

                Condition_dotkt = " and [Đơn vị thực hiện]=N'" + groupName + "'";

            }
            //truong doan
            else
            {
                bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
                DataSet dsDotKiemToan = doanKiemToan.GetDotKTCuaTruongDoanChiTiet(_objUserContext.UserName);
                if (isValidDataSet(dsDotKiemToan))
                {
                    //DataTable dtDotKiemToan = dsDotKiemToan.Tables[0].DefaultView.ToTable(true, new string[] { "DotID", "ten_dot_kiem_toan" });
                    DataTable dtDotKiemToan = dsDotKiemToan.Tables[0];
                    ListItem emptyItem = new ListItem { Text = "Chọn đợt kiểm toán", Value = "select" };
                    ddlDotKiemToan.Items.Add(emptyItem);
                    foreach (DataRow row in dtDotKiemToan.Rows)
                    {
                        ListItem item = new ListItem { Text = row["ten_dot_kiem_toan"].ToString(), Value = row["DotID"].ToString() };
                        item.Attributes.Add("doi-tuong-kiem-toan", row["doi_tuong_kiem_toan"].ToString());
                        item.Attributes.Add("don-vi-thuc-hien", row["don_vi_thuc_hien"].ToString());
                        ddlDotKiemToan.Items.Add(item);
                    }
                }
                return;
            }

            bus_Document obj_dotkt = bus_Document.Instance(_objUserContext);
            DataSet ds_dotkt = obj_dotkt.getDocumentList(DOCTYPE.DOT_KIEMTOAN, DocFields_dotkt, PropertyFields_dotkt, Condition_dotkt);
            //
            if (isValidDataSet(ds_dotkt))
            {
                ListItem itemDefault = new ListItem { Text = "Chọn đợt kiểm toán", Value = "select" };
                ddlDotKiemToan.Items.Add(itemDefault);
                foreach (DataRow row in ds_dotkt.Tables[0].Rows)
                {
                    ListItem item = new ListItem { Text = row["Tên đợt kiểm toán"].ToString(), Value = row["PK_DocumentID"].ToString() };
                    item.Attributes.Add("doi-tuong-kiem-toan", row["Đối tượng kiểm toán"].ToString());
                    item.Attributes.Add("don-vi-thuc-hien", row["Đơn vị thực hiện"].ToString());
                    ddlDotKiemToan.Items.Add(item);
                }
            }

        }
        public void BindMucDoRuiRo()
        {
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance(_objUserContext);
            DataSet ds = lapKeHoach.DSMucDoRuiRo();
            if (isValidDataSet(ds))
            {
                //ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataSource = ds.Tables[0];
                //ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataValueField = "ten_muc_do_rui_ro"; //"MucDoRuiRoID";
                //ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataTextField = "ten_muc_do_rui_ro";
                //ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataBind();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            string sDotKiemToanID = hiddenDotID.Value;
            BindDotInfo_MangNVGrid(sDotKiemToanID);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void BindDotInfo_MangNVGrid(string DotKiemToanID)
        {
            if (String.IsNullOrEmpty(DotKiemToanID))
            {
                dataCtrl.DataSource = null;
                dataCtrl.DataBind();
                return;
            }

            //get dot kiem toan info
            //txtDoiTuongKiemToan.Text = CommonFunc.getPropertyValueOnDocument(DotKiemToanID, "2A4CA2AD-0282-4D57-86AC-D973D281EF54");
            //txtDonViKiemToan.Text = CommonFunc.getPropertyValueOnDocument(DotKiemToanID, "A8A3EDBA-F569-4C06-8A57-045FBFED55FD");
            bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet dsMangNghiepVu = doanKiemToan.GetMangNghiepVuByDotKT(DotKiemToanID);
            if (isValidDataSet(dsMangNghiepVu))
            {
                dataCtrl.DataSource = dsMangNghiepVu;
                dataCtrl.DataBind();
            }
            else
            {
                dataCtrl.DataSource = null;
                dataCtrl.DataBind();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                //bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);

                //Label hoso = (Label)e.Item.FindControl("hoso") as Label;
                //string sTenMangNghiepVu = CommonFunc.getPropertyValueOnDocument(hoso.Text, "AC4286FB-5B38-47C4-93F0-E075F171D8FC");
                //bool IsSubmitted = false;
                //Image ImgSubmit = (Image)e.Item.FindControl("ImgSubmit") as Image;
                //Image ImgDelete = (Image)e.Item.FindControl("ImgDelete") as Image;
                //ImgSubmit.Visible = false;
                //IsSubmitted = lapKeHoach.IsPhanTichSoBoSubmitted(sTenMangNghiepVu, _nhomkt);
                //if (IsSubmitted)
                //{
                //    ImgSubmit.Visible = true;
                //    ImgDelete.Visible = false;
                //}

                //Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;

                //if (hoso != null)
                //{
                //    imgDelete.Attributes.Add("onclick", "{deleteChiTietPhanTich('" + hoso.Text + "')}");
                //}
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            string ErrorMessage = string.Empty;
            string sDotKiemToan = ddlDotKiemToan.SelectedValue;
            //lấy dữ liệu từ DB về cho báo cáo
            bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = obj.GetBienBanKetThucKTChiTiet("b19c0eb5-70a6-43c3-bde7-ae409939ebc7");
            ds.Tables[0].TableName = "BienBanKTKT";

            string export_path = "~/Modules/reports/client/";
            string export_type = "PDF";
            string export_filename = "BienBanKTKT_" + Session.SessionID;
            string rpt = "~/Modules/reports/data/rpt/BienBanKTKT.rpt";

            //tham số báo cáo
            SortedList<string, object> param = new SortedList<string, object>();
            //param.Add("BusinessDate", DateTime.Now.ToString());


            try
            {
                string urlreport = CommonFunc.do_report(rpt, export_path, ds, param, export_type, export_filename, _objUserContext);
                ErrorMessage = urlreport.Replace("~", "../..");
            }
            catch (Exception ex)
            {
                ErrorMessage = "-1:" + ex.Message;
            }
        }
    }
    //public class JSONHelper
    //{
    //    public static string Serialize<T>(T obj)
    //    {
    //        System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
    //        MemoryStream ms = new MemoryStream();
    //        serializer.WriteObject(ms, obj);
    //        string retVal = Encoding.Default.GetString(ms.ToArray());
    //        ms.Dispose();
    //        return retVal;
    //    }

    //    public static T Deserialize<T>(string json)
    //    {
    //        T obj = Activator.CreateInstance<T>();
    //        MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
    //        System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
    //        obj = (T)serializer.ReadObject(ms);
    //        ms.Close();
    //        ms.Dispose();
    //        return obj;
    //    }
    //}
    /// Our PhatHien object to Serialize/Deserialize to JSON

}
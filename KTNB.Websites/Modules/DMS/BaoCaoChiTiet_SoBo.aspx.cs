using System;
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
using System.Collections;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class BaoCaoChiTiet_SoBo : PageBase
    {
        protected string _action = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _nhomkt = string.Empty;
        protected string _doankt = string.Empty;
        protected string _doctypeid = DOCTYPE.CHITIET_HOSO_PHANTICH_SOBO;
        protected string _hosoChiTiet = string.Empty;
        int _status_dotkt = 0;
        protected string _action_report = string.Empty;
        protected string _report_format = "DOC";
        protected string _jsonBaocaoData = string.Empty;

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
            _doankt = Request["doankt"];
            _action = Request["act"];
            _action_report = Request["act_report"];
            _report_format = Request["report_format"];
            _jsonBaocaoData = Request["jsonBaocaoData"];

            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action_report))
            {
                if (_action_report == "baocao")
                    FeedBackClient(GetInfoDotKiemToan(_dotkt));
                if (_action_report == "xuatbaocao")
                    FeedBackClient(XuatBaoCaoDotKiemToan_1(_dotkt));
            }

            #endregion

            #region init form
            string caption = string.Empty;
            if (_action == "chitiet")
                caption = "Báo cáo chi tiết";
            if (_action == "sobo")
                caption = "Báo cáo sơ bộ";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
           
            #endregion

            #region client control event handler
            _btnSave.Visible = false;
            _btnSave.Text = "Xuất Báo cáo";
            _btnSave.Width = Unit.Pixel(100);
            _btnSave.CssClass = "PrintButton";
            _btnSave.Attributes.Add("onclick", "{XuatBaoCao(); return false;}");
            btnView.Attributes.Add("onclick", "{XemBaoCao(); return false;}");
            #endregion
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BuildDDLDotKT();

                List<KeyValuePair<string, string>> list = CommonFunc.MucDoPhatHienValue();
                string jsonMucDo = JSONHelper.Serialize<List<KeyValuePair<string, string>>>(list);
                hiddenMucDoPhatHien.Value = jsonMucDo;
            }

        }

        #region phucvubaocao

        protected void btnReport_Click(object sender, EventArgs e)
        {
            string message = hd_Message.Value;
            if (string.IsNullOrEmpty(message)) return;
            
            string ten_dotkt = message.Split('#')[0];
            string muctieu_dotkt = message.Split('#')[1];
            string phamvi_dotkt = message.Split('#')[2];
            string doituong_dotkt = message.Split('#')[3];
            string donvi_dotkt = message.Split('#')[4];

            return;

            string ErrorMessage = string.Empty;

            //lấy dữ liệu từ DB về cho báo cáo
            bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = obj.GetBienBanKetThucKTChiTiet("b19c0eb5-70a6-43c3-bde7-ae409939ebc7");
            ds.Tables[0].TableName = "BienBanKTKT";

            string export_path = "~/Modules/reports/client/";
            string export_type = "PDF";
            string export_filename = "BienBanKTKT_" + Session.SessionID;
            string rpt = "~/Modules/reports/data/rpt/BaoCaoSoBo.rpt";
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

        string GetInfoDotKiemToan(string dotkt)
        {
            string message = "";
            if (string.IsNullOrEmpty(dotkt))
                return message;
            if (dotkt == "-1")
                return message;

            string ten_dotkt = "63A0C4B1-2088-4994-B891-2FF65EB20265";
            string muctieu_dotkt = "7D31CEB6-69CC-4F8E-9BE6-38762D7C30C6";
            string phamvi_dotkt = "46C1BE6C-68F3-47B9-B0FB-DEC78831AEFF";
            string doituong_dotkt = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
            string donvi_dotkt = "A8A3EDBA-F569-4C06-8A57-045FBFED55FD";
            string danhsachphathien = GetDataModel();

            ten_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, ten_dotkt);
            muctieu_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, muctieu_dotkt);
            phamvi_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, phamvi_dotkt);
            doituong_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, doituong_dotkt);
            donvi_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, donvi_dotkt);
            message = ten_dotkt + "#" + muctieu_dotkt + "#" + phamvi_dotkt + "#" + doituong_dotkt + "#" + donvi_dotkt+"#" + danhsachphathien;
           
            return message;
        }

        string XuatBaoCaoDotKiemToan(string dotkt)
        {
            string message = "";
            if (string.IsNullOrEmpty(dotkt))
                return message;
                
            string ten_dotkt = "63A0C4B1-2088-4994-B891-2FF65EB20265";
            string muctieu_dotkt = "7D31CEB6-69CC-4F8E-9BE6-38762D7C30C6";
            string phamvi_dotkt = "46C1BE6C-68F3-47B9-B0FB-DEC78831AEFF";
            string doituong_dotkt = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
            string donvi_dotkt = "A8A3EDBA-F569-4C06-8A57-045FBFED55FD";

            ten_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, ten_dotkt);
            muctieu_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, muctieu_dotkt);
            phamvi_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, phamvi_dotkt);
            doituong_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, doituong_dotkt);
            donvi_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, donvi_dotkt);

            if (string.IsNullOrEmpty(_jsonBaocaoData))
            {
                message = "0";
                return message;
            }
            List<PhatHien_BaoCao_ChiTiet_SoBo> phatHienList = JSONHelper.Deserialize<List<PhatHien_BaoCao_ChiTiet_SoBo>>(_jsonBaocaoData);
            string condition = string.Empty;
            foreach (PhatHien_BaoCao_ChiTiet_SoBo phathien in phatHienList)
                condition += "PhatHienID = '" + phathien.PhatHienID + "' OR ";
            condition = condition.Remove(condition.Length - 3); ;

            //lấy dữ liệu từ DB về cho báo cáo
            bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet dsBienBan = obj.GetBaoCaoChiTiet_SoBo(_dotkt);

            dsBienBan.Tables[0].DefaultView.RowFilter = condition;
            DataView dv = dsBienBan.Tables[0].DefaultView;
            dv.RowFilter = condition;
            DataSet newDS = new DataSet();
            DataTable newDT = dv.ToTable();
            newDS.Tables.Add(newDT);

            foreach (DataRow row in newDS.Tables[0].Rows)
            {
                PhatHien_BaoCao_ChiTiet_SoBo phathien = phatHienList.Find(x => x.PhatHienID == row["PhatHienID"].ToString());
                row["phat_hien"] = phathien.TenPhatHien;
                row["dan_chieu"] = phathien.DanChieu;
                row["muc_do"] = phathien.MucDo;
                row["anh_huong"] = phathien.AnhHuong;
                row["khuyen_nghi"] = phathien.KhuyenNghi;
                row["nguyen_nhan"] = phathien.NguyenNhan;
            }

            //newDS.Tables[0].TableName = "BienBanKTKT";

            //lấy dữ liệu từ DB về cho báo cáo
            //bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
            //DataSet ds = obj.GetBaoCaoChiTiet_SoBo(dotkt);
            newDS.Tables[0].TableName = "BaoCaoChiTiet_SoBo";

            string export_path = "~/Modules/reports/client/";
            string export_type = _report_format;
            string export_filename = String.Empty;
          
            string rpt = "~/Modules/reports/data/rpt/BaoCaoSoBo.rpt";
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "chitiet")
                { 
                      export_filename = "Baocaochitiet_" + Session.SessionID;
                      rpt = "~/Modules/reports/data/rpt/BaoCaoChiTiet.rpt";
                }
                if (_action == "sobo")
                {
                    export_filename = "Baocaosobo_" + Session.SessionID;
                    rpt = "~/Modules/reports/data/rpt/BaoCaoSoBo.rpt";
                }
                    
                SortedList<string, object> param = new SortedList<string, object>();
                param.Add("p_ten_dotkt", ten_dotkt);
                param.Add("p_muctieu_dotkt", muctieu_dotkt);
                param.Add("p_phamvi_dotkt", phamvi_dotkt);
                param.Add("p_doituong_dotkt", doituong_dotkt);
                param.Add("p_donvi_dotkt", donvi_dotkt);
                try
                {
                    string urlreport = CommonFunc.do_report(rpt, export_path, newDS, param, export_type, export_filename, _objUserContext);
                    message = urlreport.Replace("~", "../..");
                }
                catch (Exception ex)
                {
                    message = "-1:" + ex.Message;
                }
                return message;
            }
            return message;
        }

        void BuildDDLDotKT()
        {
            ArrayList roles = _objUserContext.Roles;
            //if(roles.)
            CORE.CoreObjectContext.Role role = (CORE.CoreObjectContext.Role)roles[0];
            string roleID = role.RoleID;
            ListItem item1 = new ListItem { Text = "---Chọn đợt kiểm toán---", Value = "-1" };
            ddlDotKiemToan.Items.Add(item1);
            hd_IDDotKT.Value = "-1";

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
                    //ListItem emptyItem = new ListItem { Text = "Chọn đợt kiểm toán", Value = "select" };
                    //ddlDotKiemToan.Items.Add(emptyItem);
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
                
                foreach (DataRow row in ds_dotkt.Tables[0].Rows)
                {
                    ListItem item = new ListItem { Text = row["Tên đợt kiểm toán"].ToString(), Value = row["PK_DocumentID"].ToString() };
                    item.Attributes.Add("doi-tuong-kiem-toan", row["Đối tượng kiểm toán"].ToString());
                    item.Attributes.Add("don-vi-thuc-hien", row["Đơn vị thực hiện"].ToString());
                    ddlDotKiemToan.Items.Add(item);
                }
            }




            //bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
            //DataSet dsDotKiemToan = doanKiemToan.GetDotKTCuaTruongDoanChiTiet(_objUserContext.UserName);
            //ListItem item1 = new ListItem { Text = "---Chọn đợt kiểm toán---", Value = "-1" };
            //ddlDotKiemToan.Items.Add(item1);
            //hd_IDDotKT.Value = "-1";
            //if (isValidDataSet(dsDotKiemToan))
            //{
            //    foreach (DataRow row in dsDotKiemToan.Tables[0].Rows)
            //    {
            //        ListItem item = new ListItem { Text = row["ten_dot_kiem_toan"].ToString(), Value = row["DotID"].ToString() };
            //        ddlDotKiemToan.Items.Add(item);
            //    }
            //}

           
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="DocumentTypeID"></param>
        //private void BindDotInfo_MangNVGrid(string DotKiemToanID)
        //{
        //    if (DotKiemToanID == "-1")
        //    {
        //        dataCtrl.DataSource = null;
        //        dataCtrl.DataBind();

        //    }
        //    else
        //    {
        //        ObjectDataSource1.SelectParameters["DotKT"].DefaultValue = DotKiemToanID;
        //        dataCtrl.DataBind();
        //    }
            
        //}

        //protected void updatepanel1_OnLoad(object sender, EventArgs e)
        //{
        //    string sDotKiemToanID = hd_IDDotKT.Value;
        //    if(!string.IsNullOrEmpty(sDotKiemToanID))
        //        BindDotInfo_MangNVGrid(sDotKiemToanID);
        //}
        #endregion

        #region phucvu laybaocao sua phathien
        public string EditBaoCao()
        {
            string result = string.Empty;
            if (_dotkt == "-1")
                return result;
            //kiem tra xem dot kiem toan co o trang thai 21 - 26 khong ?
            //var DotStatus = CommonFunc.GetDocStatus(_dotkt);
            //if (DotStatus >= 21 && DotStatus <= 26)
            //{
            //lấy dữ liệu từ DB về cho báo cáo
            bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet dsBienBan = obj.GetBaoCaoChiTiet_SoBo(_dotkt);
            dsBienBan.Tables[0].TableName = "BienBanKTKT";

            //test JSON
            List<PhatHien_BaoCao_ChiTiet_SoBo> phatHienList = new List<PhatHien_BaoCao_ChiTiet_SoBo>();
            DataView view = new DataView(dsBienBan.Tables[0]);
            DataTable distinctValues = view.ToTable(true, "PhatHienID","phat_hien", "muc_do", "dan_chieu", "nguyen_nhan","anh_huong","khuyen_nghi");

            foreach (DataRow row in distinctValues.Rows)
            {
                string trangthai = CommonFunc.GetTrangThaiPhatHien(row["PhatHienID"].ToString());
                PhatHien_BaoCao_ChiTiet_SoBo phathien = new PhatHien_BaoCao_ChiTiet_SoBo(row["PhatHienID"].ToString(), row["phat_hien"].ToString(),
                row["muc_do"].ToString(), row["dan_chieu"].ToString(),string.Empty, row["nguyen_nhan"].ToString(), row["anh_huong"].ToString(), 
                row["khuyen_nghi"].ToString(),trangthai);
                phatHienList.Add(phathien);
            }
            result = JSONHelper.Serialize<List<PhatHien_BaoCao_ChiTiet_SoBo>>(phatHienList);
                
            return result;
        }

        public string GetDataModel()
        {
            string result = string.Empty;
            //kiem tra xem dot kiem toan co o trang thai 21 - 26 khong ?
            //var DotStatus = CommonFunc.GetDocStatus(_dotkt);
            //if (DotStatus >= 21 && DotStatus <= 26)
            {
                //lấy dữ liệu từ DB về cho báo cáo
                bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
                DataSet dsBienBan = obj.GetBaoCaoChiTiet_SoBo(_dotkt);
                dsBienBan.Tables[0].TableName = "BienBanKTKT";

                //test JSON
                List<MangNghiepVuInBaoCao_ChiTiet_SoBo> mangNghiepVuList = new List<MangNghiepVuInBaoCao_ChiTiet_SoBo>();
                DataView view = new DataView(dsBienBan.Tables[0]);
                DataTable distinctValues = view.ToTable(true, "MangNghiepVuID", "ten_mang_nghiep_vu");

                foreach (DataRow row in distinctValues.Rows)
                {
                    MangNghiepVuInBaoCao_ChiTiet_SoBo mangNghiepVu = new MangNghiepVuInBaoCao_ChiTiet_SoBo();
                    mangNghiepVu.PhatHienBienBanList = new List<PhatHien_BaoCao_ChiTiet_SoBo>();
                    mangNghiepVu.MangNghiepVuID = row["MangNghiepVuID"].ToString();
                    mangNghiepVu.TenMangNghiepVu = row["ten_mang_nghiep_vu"].ToString();
                    DataRow[] foundRows;
                    string expression = "MangNghiepVuID = '" + row["MangNghiepVuID"].ToString() + "'";
                    foundRows = dsBienBan.Tables[0].Select(expression);
                    foreach (DataRow dtrow in foundRows)
                    {
                        //assign Phat hien Info
                        PhatHien_BaoCao_ChiTiet_SoBo phathien = new PhatHien_BaoCao_ChiTiet_SoBo();
                        phathien.DanChieu = dtrow["dan_chieu"].ToString();
                        phathien.MucDo = dtrow["muc_do"].ToString();
                        phathien.AnhHuong = dtrow["anh_huong"].ToString();
                        phathien.KhuyenNghi = dtrow["khuyen_nghi"].ToString();
                        phathien.NguyenNhan = dtrow["nguyen_nhan"].ToString();
                        phathien.PhatHienID = dtrow["PhatHienID"].ToString();
                        phathien.TenPhatHien = dtrow["phat_hien"].ToString();
                        phathien.TrangThai = CommonFunc.GetTrangThaiPhatHien(dtrow["PhatHienID"].ToString());
                        mangNghiepVu.PhatHienBienBanList.Add(phathien);
                    }
                    mangNghiepVuList.Add(mangNghiepVu);
                }
                result = JSONHelper.Serialize<List<MangNghiepVuInBaoCao_ChiTiet_SoBo>>(mangNghiepVuList);
            }

            return result;
        }

        string XuatBaoCaoDotKiemToan_1(string dotkt)
        {
            string message = "";
            if (string.IsNullOrEmpty(dotkt))
                return message;
            string ten_dotkt = "63A0C4B1-2088-4994-B891-2FF65EB20265";
            string muctieu_dotkt = "7D31CEB6-69CC-4F8E-9BE6-38762D7C30C6";
            string phamvi_dotkt = "46C1BE6C-68F3-47B9-B0FB-DEC78831AEFF";
            string doituong_dotkt = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
            string donvi_dotkt = "A8A3EDBA-F569-4C06-8A57-045FBFED55FD";

            ten_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, ten_dotkt);
            muctieu_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, muctieu_dotkt);
            phamvi_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, phamvi_dotkt);
            doituong_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, doituong_dotkt);
            donvi_dotkt = CommonFunc.getPropertyValueOnDocument(dotkt, donvi_dotkt);

            if (string.IsNullOrEmpty(_jsonBaocaoData))
            {
                message = "0";
                return message;
            }
            List<MangNghiepVuInBaoCao_ChiTiet_SoBo> mangNghiepVuList = JSONHelper.Deserialize<List<MangNghiepVuInBaoCao_ChiTiet_SoBo>>(_jsonBaocaoData);
            //build DataSet for crystal report
            DataSet dsReport = new DataSet();
            DataTable dtReport = new DataTable();
            dtReport.TableName = "BaoCaoChiTiet_SoBo";
            dtReport.Columns.Add("DotID", typeof(String));
            dtReport.Columns.Add("MangNghiepVuID", typeof(String));
            dtReport.Columns.Add("PhatHienID", typeof(String));
            dtReport.Columns.Add("ten_mang_nghiep_vu", typeof(String));
            dtReport.Columns.Add("phat_hien", typeof(String));
            dtReport.Columns.Add("dan_chieu", typeof(String));
            dtReport.Columns.Add("muc_do", typeof(String));
            dtReport.Columns.Add("anh_huong", typeof(String));
            dtReport.Columns.Add("nguyen_nhan", typeof(String));
            dtReport.Columns.Add("khuyen_nghi", typeof(String));
            foreach (MangNghiepVuInBaoCao_ChiTiet_SoBo mangnghiepvu in mangNghiepVuList)
            {
                foreach (PhatHien_BaoCao_ChiTiet_SoBo phathien in mangnghiepvu.PhatHienBienBanList)
                {
                    DataRow rowReport = dtReport.NewRow();
                    rowReport["DotID"] = _dotkt;
                    rowReport["MangNghiepVuID"] = mangnghiepvu.MangNghiepVuID;
                    rowReport["PhatHienID"] = phathien.PhatHienID;
                    rowReport["ten_mang_nghiep_vu"] = mangnghiepvu.TenMangNghiepVu;
                    rowReport["phat_hien"] = phathien.TenPhatHien;
                    rowReport["dan_chieu"] = phathien.DanChieu;
                    rowReport["muc_do"] = phathien.MucDo;
                    rowReport["anh_huong"] = phathien.AnhHuong;
                    rowReport["nguyen_nhan"] = phathien.NguyenNhan;
                    rowReport["khuyen_nghi"] = phathien.KhuyenNghi;
                    dtReport.Rows.Add(rowReport);
                }
            }
            dsReport.Tables.Add(dtReport);
            string export_path = "~/Modules/reports/client/";
            string export_type = _report_format;
            string export_filename = String.Empty;
            string rpt = "~/Modules/reports/data/rpt/BaoCaoSoBo.rpt";
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "chitiet")
                {
                    export_filename = "Baocaochitiet_" + Session.SessionID;
                    rpt = "~/Modules/reports/data/rpt/BaoCaoChiTiet.rpt";
                }
                if (_action == "sobo")
                {
                    export_filename = "Baocaosobo_" + Session.SessionID;
                    rpt = "~/Modules/reports/data/rpt/BaoCaoSoBo.rpt";
                }
                SortedList<string, object> param = new SortedList<string, object>();
                param.Add("p_ten_dotkt", ten_dotkt);
                param.Add("p_muctieu_dotkt", muctieu_dotkt);
                param.Add("p_phamvi_dotkt", phamvi_dotkt);
                param.Add("p_doituong_dotkt", doituong_dotkt);
                param.Add("p_donvi_dotkt", donvi_dotkt);
                try
                {
                    string urlreport = CommonFunc.do_report(rpt, export_path, dsReport, param, export_type, export_filename, _objUserContext);
                    message = urlreport.Replace("~", "../..");
                }
                catch (Exception ex)
                {
                    message = "-1:" + ex.Message;
                }
                return message;
            }
            return message;
        }
        #endregion
    }
}
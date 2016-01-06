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
using CORE.CoreObjectContext;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class DanhGiaRuiRoConLai : PageBase
    {
        protected string _action = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _doankt = string.Empty;
        protected string _nhomkt = string.Empty;
        protected string _doctypeid = DOCTYPE.CHITIET_HOSO_PHANTICH_SOBO;
        protected string _doctypeid_kiemsoat = DOCTYPE.KIEMSOAT;
        protected string _doctypeid_ruiro = DOCTYPE.RUIRO_KIEMSOAT;
        protected string _doctypeid_thutuc = DOCTYPE.THUTUC_KIEMTOAN;
        protected string doiTuongKiemToan_propID = "2A4CA2AD-0282-4D57-86AC-D973D281EF54";
        protected string dotKiemToan_propID = "63A0C4B1-2088-4994-B891-2FF65EB20265";

        protected string _hosoChiTiet = string.Empty;

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
            _hosoChiTiet = Request["hosochitiet"];
            //if (string.IsNullOrEmpty(_dotkt))
            //{
            //    bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
            //    DataSet dsDotInfo = new DataSet();
            //    dsDotInfo = doanKiemToan.GetDotKTByNhomKT(_nhomkt);
            //}

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

            }

            #endregion

            #region init form
            string caption = "Đánh giá rủi ro còn lại";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            //_btnAddNew.Visible = true;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            ObjectDataSource1.SelectParameters["NhomKiemToan"].DefaultValue = _nhomkt;
            #endregion

            #region client control event handler
            //Truongphong->tao doan kiem toan
            //if (_m_roleID == ROLES.TRUONGPHONG_DONVI_KIEMTOAN)
            //    btnDongy.Visible = btnThem.Visible = true;
            //else
            //    btnDongy.Visible = btnThem.Visible = false;
            //btnThemVDQuanTam.Attributes.Add("onclick", "{ThemVDQuanTam('" + _doctypeid + "'); return false;}");
            //btnThem.Attributes.Add("onclick", "{ThemThanhVien(); return false;}");
            //_btnSave.Visible = true;
            //_btnSave.Text = "Submit";
            //_btnSave.Attributes.Add("onclick", "{Submit(); return false;}");
            #endregion
        }
        public string xoaChiTietPhanTich(string hoso_chitiet, string nhomkt)
        {
            CommonFunc.RemoveDocLink(hoso_chitiet, nhomkt, _objUserContext);
            return string.Empty;
        }
        public string Submit(string nhomkt)
        {
            bus_LapKeHoach lapkehoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsChiTietRuiRo = lapkehoach.ChiTietHoSoPhanTichRuiRo(nhomkt, _objUserContext.UserName);
            if (isValidDataSet(dsChiTietRuiRo))
            {
                foreach (DataRow row in dsChiTietRuiRo.Tables[0].Rows)
                {
                    CommonFunc.UpdateDocStatus(row["hoso"].ToString(), 4);
                }
            }
            return string.Empty;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDotKiemToanInfo();
                //BindRuiRoGrid();
                BindGrid(_nhomkt);
            }

        }
        public void LoadDotKiemToanInfo()
        {
            bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet dsDotKTInfo = doanKiemToan.GetDotKTByNhomKT(_nhomkt);
            if (isValidDataSet(dsDotKTInfo))
            {
                txtDoiTuongKiemToan.Text = dsDotKTInfo.Tables[0].Rows[0]["doi_tuong_kiem_toan"].ToString(); //CommonFunc.getPropertyValueOnDocument(_dotkt, doiTuongKiemToan_propID);
                txtDotKiemToan.Text = dsDotKTInfo.Tables[0].Rows[0]["ten_dot_kiem_toan"].ToString();
                _dotkt = dsDotKTInfo.Tables[0].Rows[0]["dotkt"].ToString();
            }
            //txtDoiTuongKiemToan.Text = CommonFunc.getPropertyValueOnDocument(_dotkt, doiTuongKiemToan_propID);
            //txtDotKiemToan.Text = CommonFunc.getPropertyValueOnDocument(_dotkt, dotKiemToan_propID);
        }
        //public void BindRuiRoGrid()
        //{
        //    bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
        //    DataSet dsMangNVByThanhVien = doanKiemToan.GetMangNVByThanhVien(_nhomkt, _objUserContext.UserID);
        //    //check neu chua co rui ro thi generate
        //    string _doctypeid_hosoruiro = DOCTYPE.CHITIET_HOSO_RUIRO;
        //    string DocFields = "PK_DocumentID,[Name]";
        //    string PropertyFields = "Name";
        //    string Condition = " and CREATEDBY='" + _objUserContext.UserName + "' and PK_DocumentID in ( select hosoruiro_nhom.FK_DOCUMENTID from T_DOCLINK as hosoruiro_nhom where hosoruiro_nhom.FK_DOCLINKID = '" + _nhomkt + "')";
        //    bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
        //    DataSet dsHoSoRuiRo = obj.getDocumentList(_doctypeid_hosoruiro, DocFields, PropertyFields, Condition);
        //    if (isValidDataSet(dsMangNVByThanhVien))
        //    {
        //        BindGridVanDeQuanTam(dsMangNVByThanhVien);
        //    }
        //}
        public bool CheckGenerated(string tenMangNghiepVu, string nhomkt)
        {
            bool IsGenerated = false;
            bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsPhanTichSoBo = lapKeHoach.ChiTietHoSoPhanTichRuiRo(nhomkt);
            if (isValidDataSet(dsPhanTichSoBo))
            {
                DataRow[] foundRows;
                string expression = "mang_nghiep_vu = '" + tenMangNghiepVu + "'";
                foundRows = dsPhanTichSoBo.Tables[0].Select(expression);
                if (foundRows.Count() > 0)
                    IsGenerated = true;
            }

            return IsGenerated;
        }
        
        private void BindGrid(string NhomID)
        {
            if (String.IsNullOrEmpty(NhomID))
                return;
            
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance(_objUserContext);
            DataSet ds = lapKeHoach.ChiTietHoSoPhanTichRuiRo(NhomID, _objUserContext.UserName);
            if (isValidDataSet(ds))
            {
                ObjectDataSource1.SelectParameters["NhomKiemToan"].DefaultValue = NhomID;
                dataCtrl.DataBind();
            }
        }
        private string CurrentSortDirection
        {
            get
            {
                if (sortDirection.Value == null)
                    sortDirection.Value = "asc";
                return sortDirection.Value;
            }
            set { sortDirection.Value = value; }
        }

        private string CurrentSortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                    ViewState["SortExpression"] = "rui_ro";
                return (string)ViewState["SortExpression"];
            }
            set { ViewState["SortExpression"] = value; }
        }
        protected void dataCtrl_OnSorting(object sender, C1SortingCommandEventArgs e)
        {
            CurrentSortExpression = e.SortExpression;

            // Ignore e.SortDirection since it is always Ascending, an apparent bug in .NET
            if (CurrentSortDirection == "asc")
                CurrentSortDirection = "desc";
            else
                CurrentSortDirection = "asc";

            string strSort = string.Empty;
            if (CurrentSortDirection == "desc")
                strSort = "DESC";
            //GetList(_doctypeid_nhomkiemtoan, _role, CurrentSortExpression + " " + strSort);
            ObjectDataSource1.SelectParameters["NhomKiemToan"].DefaultValue = _nhomkt;
            ObjectDataSource1.SelectParameters["sort"].DefaultValue = CurrentSortExpression + " " + strSort;
        }
        public bool CheckSubmitted(string tenMangNghiepVu, string nhomKiemToanID)
        {
            bool IsSubmitted = false;
            bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsPhanTichSoBo = lapKeHoach.ChiTietHoSoPhanTichSoBo(nhomKiemToanID);
            if (isValidDataSet(dsPhanTichSoBo))
            {
                DataRow[] foundRows;
                string expression = "STATUS=4 and  mang_nghiep_vu ='" + tenMangNghiepVu + "'";
                foundRows = dsPhanTichSoBo.Tables[0].Select(expression);
                if (foundRows.Count() > 0)
                    IsSubmitted = true;
            }

            return IsSubmitted;
        }
        protected void GridVanDeQuanTam_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                bool IsSubmitted = false;
                Label lblVanDeQuanTam = (Label)e.Item.FindControl("lblVanDeQuanTam") as Label;
                Image ImgSubmit = (Image)e.Item.FindControl("ImgSubmit") as Image;
                ImgSubmit.Visible = false;

                HiddenField hiddenTenMangNghiepVu = (HiddenField)e.Item.FindControl("hiddenTenMangNghiepVu") as HiddenField;
                string TenMangNghiepVu = hiddenTenMangNghiepVu.Value;
                IsSubmitted = CheckSubmitted(TenMangNghiepVu, _nhomkt);
                if (IsSubmitted)
                    ImgSubmit.Visible = true;
                //get Van de quan tam by Mang Nghiep Vu
                bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
                DataSet dsHoSoSoBo = lapKeHoach.ChiTietHoSoPhanTichSoBo(_nhomkt);
                if (isValidDataSet(dsHoSoSoBo))
                {
                    DataRow[] foundRows;
                    string expression = "mang_nghiep_vu ='" + TenMangNghiepVu + "'";
                    foundRows = dsHoSoSoBo.Tables[0].Select(expression);
                    lblVanDeQuanTam.Text += "<ul>";
                    foreach (DataRow row in foundRows)
                    {
                        lblVanDeQuanTam.Text += "<li>";
                        lblVanDeQuanTam.Text += row["van_de_quan_tam"].ToString();// +"<br/>";
                        lblVanDeQuanTam.Text += "</li>";

                    }
                    lblVanDeQuanTam.Text += "</ul>";
                }



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
                bool IsSubmitted = false;
                Image ImgSubmit = (Image)e.Item.FindControl("ImgSubmit") as Image;
                ImgSubmit.Visible = false;

                Label lblStatus = (Label)e.Item.FindControl("lblStatus") as Label;

                Label hoso = (Label)e.Item.FindControl("hoso") as Label;
                Label lblCommandXacSuat = (Label)e.Item.FindControl("lblCommandXacSuat") as Label;
                Label lblCommandAnhHuong = (Label)e.Item.FindControl("lblCommandAnhHuong") as Label;
                Label lblCommandRRCoHuu = (Label)e.Item.FindControl("lblCommandRRCoHuu") as Label;
                Label lblRuiRoID = (Label)e.Item.FindControl("lblRuiRoID") as Label;
                Label lblDiemKiemSoat = (Label)e.Item.FindControl("lblDiemKiemSoat") as Label;
                Label lblRRCL = (Label)e.Item.FindControl("lblRRCL") as Label;

                if (hoso != null)
                {
                    if (lblStatus.Text.Equals("4"))
                    {
                        ImgSubmit.Visible = true;
                    }
                    else
                    {
                        lblCommandRRCoHuu.Attributes.Add("onclick", "{LoadDocument('" + hoso.Text + "','ah')}");
                    }
                    if (lblDiemKiemSoat.Text == "")
                        lblDiemKiemSoat.Text = "Chưa đánh giá";
                    //lblDiemKiemSoat.Attributes.Add("onclick", "{LoadTinhDiemKiemSoat('" + hoso.Text + "','" + lblRuiRoID.Text + "')}");
                }
                TreeView treeViewThuTuc = (TreeView)e.Item.FindControl("treeViewThuTuc") as TreeView;
                string ruiro = e.Item.Cells[3].Text == null ? String.Empty : e.Item.Cells[3].Text;
                //if (treeViewKiemSoat != null)
                //    BuidTreeView(treeViewKiemSoat, ruiro, 0);
                if (treeViewThuTuc != null)
                    BuidTreeView(treeViewThuTuc, ruiro, 1);
                if (treeViewThuTuc.Nodes.Count > 0)
                    lblDiemKiemSoat.Attributes.Add("onclick", "{LoadTinhDiemKiemSoat('" + hoso.Text + "','" + lblRuiRoID.Text + "')}");
                else
                    lblDiemKiemSoat.Attributes.Add("onclick", "{alert('Rủi ro này không có kiểm soát để tính điểm');}");

                if (lblCommandRRCoHuu != null)
                    BuildRRCoHuu(lblCommandXacSuat.Text, lblCommandAnhHuong.Text, lblCommandRRCoHuu);
                if (lblDiemKiemSoat.Text != "" && lblDiemKiemSoat.Text.ToUpper() != "Chưa đánh giá".ToUpper() && lblCommandRRCoHuu.Text != "" && lblCommandRRCoHuu.Text.ToUpper() != "Đánh giá".ToUpper())
                    lblRRCL.Text = CommonFunc.GetRuiRoConLai(Int32.Parse(lblCommandRRCoHuu.Text), Int32.Parse(lblDiemKiemSoat.Text));
            }
        }

        /// <summary>
        /// tinh toan rui ro co huu
        /// </summary>
        /// <param name="xacsuat"></param>
        /// <param name="anhhuong"></param>
        /// <param name="lbl"></param>
        /// author:quangna
        void BuildRRCoHuu(string xacsuat, string anhhuong, Label lbl)
        {
            if (String.IsNullOrEmpty(xacsuat) || String.IsNullOrEmpty(anhhuong))
                return;
            if (!CommonFunc.IsNumber(xacsuat) || !CommonFunc.IsNumber(anhhuong))
                return;
            if (xacsuat.Equals("0") && anhhuong.Equals("0"))
                lbl.Text = "Đánh giá";
            else
                lbl.Text = (Int32.Parse(xacsuat) * Int32.Parse(anhhuong)).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="ruiro"></param>
        /// <param name="flag"></param>
        /// author:quangna
        void BuidTreeView(TreeView treeView, string ruiro, int flag)
        {
            if (String.IsNullOrEmpty(ruiro))
                return;
            //TreeNode nodeRuiRo = new TreeNode(ruiro);
            //treeView.Nodes.Add(nodeRuiRo);
            string DocFields = "PK_DocumentID,[Tên kiểm soát],[Tên rủi ro],[Diễn giải],Status";
            string PropertyFields = "Tên kiểm soát,Tên rủi ro,Diễn giải";
            string Condition = " And [Tên rủi ro]=N'" + ruiro + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(_doctypeid_kiemsoat, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds) == false)
                return;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                TreeNode nodeKiemSoat = new TreeNode();
                nodeKiemSoat.Text = row["Tên kiểm soát"].ToString();
                nodeKiemSoat.ToolTip = row["Tên kiểm soát"].ToString();
                nodeKiemSoat.SelectAction = TreeNodeSelectAction.None;
                treeView.Nodes.Add(nodeKiemSoat);
                if (flag == 1)
                {
                    string DocFields_TT = "PK_DocumentID,Status,[Tên kiểm soát],[Tên thủ tục kiểm toán],[Diễn giải]";
                    string PropertyFields_TT = "Tên kiểm soát,Tên thủ tục kiểm toán,Diễn giải";
                    string Condition_TT = " And [Tên kiểm soát]=N'" + row["Tên kiểm soát"].ToString() + "'";
                    DataSet dsThuTuc = doc.getDocumentList(_doctypeid_thutuc, DocFields_TT, PropertyFields_TT, Condition_TT);
                    if (isValidDataSet(dsThuTuc))
                        foreach (DataRow rowTT in dsThuTuc.Tables[0].Rows)
                        {
                            TreeNode nodeThuTuc = new TreeNode();
                            nodeThuTuc.Text = rowTT["Tên thủ tục kiểm toán"].ToString();
                            nodeThuTuc.ToolTip = rowTT["Tên thủ tục kiểm toán"].ToString();
                            nodeThuTuc.SelectAction = TreeNodeSelectAction.None;
                            nodeKiemSoat.ChildNodes.Add(nodeThuTuc);
                        }
                }
            }
        }

        protected void BindGridVanDeQuanTam(DataSet dsMangNVByThanhVien)
        {
            if (isValidDataSet(dsMangNVByThanhVien))
            {
                DataView dv = dsMangNVByThanhVien.Tables[0].DefaultView;
                dv.Sort = "ten_mang_nghiep_vu ASC";

                //GridVanDeQuanTam.DataSource = dv.ToTable();
                //GridVanDeQuanTam.DataBind();
            }

        }

    }
}
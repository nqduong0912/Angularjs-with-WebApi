using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using C1.Web.C1WebGrid;
using CORE.UMS.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class DanhSachPhatHien : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _phathienid = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _cv = string.Empty;
        protected int _status_dotkt = 0;
        protected int _status_cv = 0;
        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            _documentid = Request["doc"];
            _dotkt = Request["dotkt"];
            _cv = Request["cv"];
            _phathienid = Request["phathienid"];
            _action = Request["act"];
            _dotkt = Request["dotkt"];

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            if (!string.IsNullOrEmpty(_documentid))
                _status_cv = CommonFunc.GetDocStatus(_documentid);

            #region get data submit
            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "xoaphathien")
                    if (!string.IsNullOrEmpty(_phathienid) && !string.IsNullOrEmpty(_documentid))
                        FeedBackClient(XoaPhatHien(_phathienid, _documentid));
            }
            #endregion

            #region init form
            base.InitForm("Danh sách phát hiện", string.Empty, _doctypeid, 0);
            //_btnAddNew.Visible = true;
            //_btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            #endregion

            #region client control event handler
           

            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsDotInfo = doanKiemToan.GetDotInfoByCongViec(_documentid);
            if (isValidDataSet(dsDotInfo))
            {
                txtDotKiemToan.Text = dsDotInfo.Tables[0].Rows[0]["dot_kiem_toan"].ToString();
                txtDoiTuongKiemToan.Text = dsDotInfo.Tables[0].Rows[0]["doi_tuong_kiem_toan"].ToString();
                txtCongViec.Text = dsDotInfo.Tables[0].Rows[0]["ten_cong_viec"].ToString();
                _status_dotkt = CommonFunc.GetDocStatus(dsDotInfo.Tables[0].Rows[0]["dotid"].ToString());

                //kiểm tra trạng thái đợt kiểm toán xem có thể thêm phát hiện hay không
                bool IsCotheThemPhatHien = TrangThaiDotKiemToan.IsCoTheThemPhatHien(_status_dotkt);
                //hiddenIsCotheThemPhatHien.Value = IsCotheThemPhatHien.ToString();
                if (IsCotheThemPhatHien == false )
                {
                    tbThemPhatHien.Visible = false;
                }
            }
            GetList(_doctypeid);
        }
        #endregion

        private string XoaPhatHien(string PhatHienID, string CongViecID)
        {
            CommonFunc.RemoveDocLink(PhatHienID, CongViecID, _objUserContext);
            string errormessage = "2";
            return errormessage;
        }
        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID)
        {
            string DocFields = "PK_DocumentID,[Phát hiện]";
            string PropertyFields = "Phát hiện";
            string Condition = String.Empty;//" Order By [Loại đối tượng kiểm toán]";

            //ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
            //ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            //ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["congviecID"].DefaultValue = _documentid;
            //ObjectDataSource1.SelectParameters["cv"].DefaultValue = _cv;
            if (_cv == "nguoithuchien")
                ObjectDataSource1.SelectParameters["cv"].DefaultValue = _cv;
            else if (_cv == "nguoiduyet")
                ObjectDataSource1.SelectParameters["cv"].DefaultValue = CommonFunc.CheckNguoiDuyet(_documentid);
            dataCtrl.DataBind();
        }
        #endregion

        #region page button processing
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
                Label lblLoaiPhatHien = (Label)e.Item.FindControl("lblLoaiPhatHien") as Label;
                Label lblLoaiPhatHienText = (Label)e.Item.FindControl("lblLoaiPhatHienText") as Label;
                Label lblTongSoPhanHoi = (Label)e.Item.FindControl("lblTongSoPhanHoi") as Label;
                Label phathienid = (Label)e.Item.FindControl("phathienid") as Label;
                Label Status = (Label)e.Item.FindControl("Status") as Label;
                int StatusValue = Convert.ToInt32(Status.Text);
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Image imgNhanXet = (Image)e.Item.FindControl("imgNhanXet") as Image;
                imgNhanXet.Width = 16;
                imgNhanXet.Height = 16;
                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;
                imgDelete.Attributes.Add("onclick", "{xoaPhatHien('" + phathienid.Text + "')}");
                bool isCoTheXoa = TrangThaiDotKiemToan.IsCoTheXoaPhatHien(_status_dotkt);
                if (isCoTheXoa)
                    imgDelete.Visible = true;
                else
                    imgDelete.Visible = false;
                //get tong so phan hoi
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
                DataSet dsPhanHoi = new DataSet();
                DataSet dsPhanHoiTemp = new DataSet();
                //if (_cv == "nguoithuchien")
                //{
                //    dsPhanHoiTemp = doankiemtoan.GetDanhSachPhanHoiByUserType(phathienid.Text, _cv);
                //    string strExpr = "nguoi_nhap = '" + _objUserContext.UserName + "'";
                //    DataView dv = dsPhanHoiTemp.Tables[0].DefaultView;
                //    dv.RowFilter = strExpr;
                //    DataTable newDT = dv.ToTable();
                //    dsPhanHoi.Tables.Add(newDT);
                //}
                dsPhanHoi = doankiemtoan.GetDanhSachPhanHoiByUserType(phathienid.Text, _cv);
                if (_cv == "nguoiduyet")
                    dsPhanHoi = doankiemtoan.GetDanhSachPhanHoiByUserType(phathienid.Text, CommonFunc.CheckNguoiDuyet(_documentid));
                if (isValidDataSet(dsPhanHoi))
                    lblTongSoPhanHoi.Text = dsPhanHoi.Tables[0].Rows.Count.ToString();
                else
                    lblTongSoPhanHoi.Text = "0";
                //
                if (phathienid != null && lblLoaiPhatHien != null)
                {
                    lblTongSoPhanHoi.Attributes.Add("onclick", "{LoadPagePhanHoiDonVi('" + phathienid.Text + "')}");
                    if (lblLoaiPhatHien.Text.Equals("hethong"))
                    {
                        imgEdit.Attributes.Add("onclick", "{LoadDocumentHeThong('" + phathienid.Text + "')}");
                        imgNhanXet.Attributes.Add("onclick", "{LoadDocumentHeThong('" + phathienid.Text + "')}");
                        lblLoaiPhatHienText.Text = "Hệ thống";
                    }

                    else
                    {
                        imgEdit.Attributes.Add("onclick", "{LoadDocumentViPham('" + phathienid.Text + "')}");
                        imgNhanXet.Attributes.Add("onclick", "{LoadDocumentViPham('" + phathienid.Text + "')}");
                        lblLoaiPhatHienText.Text = "Vi Phạm";
                    }
                    //tooltip nhan xet
                    imgNhanXet.ToolTip = BuildNhanXet(phathienid.Text, lblLoaiPhatHien.Text);
                }
                if (Status != null)
                    CommonFunc.SetTrangThaiPhatHien(Status, Status.Text);
                
            }
        }
        public string BuildNhanXet(string PhatHienID, string LoaiPhatHien)
        {
            string strNhanXet = string.Empty;
            string jsonTuChoi = string.Empty;
            //grid tu choi
            if(LoaiPhatHien.Equals("hethong"))
                jsonTuChoi = CommonFunc.getPropertyValueOnDocument(PhatHienID, "17FE4A62-A92E-4921-A9DA-C7C7110EB8B3");
            else
                jsonTuChoi = CommonFunc.getPropertyValueOnDocument(PhatHienID, "75AF3ED2-78F6-4DAE-BB20-157920BAC865");
            if (jsonTuChoi.Length > 0)
            {
                try
                {
                    //bind Lich su tu choi
                    List<NhanXet> nhanXetTuChoiList = JSONHelper.Deserialize<List<NhanXet>>(jsonTuChoi);
                    //bind da nhan xet
                    List<NhanXet> nhanXetList = nhanXetTuChoiList.Where(s => s.HanhDong == "PheDuyet").ToList();
                    foreach (NhanXet nhanxet in nhanXetList)
                    {
                        strNhanXet += nhanxet.NguoiNhanXet + " : " + nhanxet.LyDo + "\n";
                    }
                    return strNhanXet;

                }
                catch (Exception ex)
                {

                }
            }
            return strNhanXet;  
        }
        #endregion
    }
}
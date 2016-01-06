using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class ChuongTrinhKiemToan : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN;
        protected string _doctypeid_thutuckiemtoan = DOCTYPE.THUTUC_KIEMTOAN;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        

        protected string _dotkt = string.Empty;
        protected string _doankt = string.Empty;
        protected bool _isTruongDoan = false;
        protected int _status_dotkt = 0;
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
            _doankt = Request["doankt"];

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;
            if (!string.IsNullOrEmpty(_dotkt))
                _status_dotkt = CommonFunc.GetDocStatus(_dotkt);
            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Thông tin chương trình kiểm toán", string.Empty, _doctypeid, 0);
            _isTruongDoan = CommonFunc.IsTruongDoan(_dotkt, _objUserContext.UserName);
            if (_isTruongDoan)
                _btnAddNew.Visible = true;
            else
                _btnAddNew.Visible = false;
            //if (_status_dotkt >= 31 || _status_dotkt < 14)//matran
            if (_status_dotkt >= 31 || _status_dotkt <=14 || _status_dotkt == 141 || _status_dotkt == 231)//matran
                _btnAddNew.Visible = false;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
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
            if (!IsPostBack)
            {
                //GetList();
                if (!String.IsNullOrEmpty(_dotkt))
                    CommonFunc.LoadDocInfo(_dotkt, Page.Master);
            }
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList()
        {
            if (String.IsNullOrEmpty(_doankt))
                return;
            string DocFields = "PK_DocumentID,[Tên công việc],[Người thực hiện],[Người duyệt 1],[Người duyệt 2],[Ngày bắt đầu],[Ngày kết thúc]";
            string PropertyFields = "Tên công việc,Người thực hiện,Người duyệt 1,Người duyệt 2,Ngày bắt đầu,Ngày kết thúc";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _doankt + "')";

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = _doctypeid;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
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
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                TreeView treeViewThuTuc = (TreeView)e.Item.FindControl("treeViewThuTuc") as TreeView;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                if (PK_DocumentID != null)
                {
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");
                    if (treeViewThuTuc != null)
                        BuildTreeView(PK_DocumentID.Text, treeViewThuTuc);
                }
                Label NgayBatDau = (Label)e.Item.FindControl("NgayBatDau") as Label;
                Label NgayKetThuc = (Label)e.Item.FindControl("NgayKetThuc") as Label;
                if (NgayBatDau != null && NgayKetThuc != null)
                {
                    NgayBatDau.Text = CommonFunc.formatDateTimeDDMMYYY(NgayBatDau.Text);
                    NgayKetThuc.Text = CommonFunc.formatDateTimeDDMMYYY(NgayKetThuc.Text);
                }
            }
        }


        void BuildTreeView(string documentID,TreeView treeView)
        {
            string DocFields = "PK_DocumentID,[Tên thủ tục kiểm toán]";
            string PropertyFields = "Tên thủ tục kiểm toán";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + documentID + "')";
            bus_Document obj = new bus_Document(_objUserContext);
            DataSet ds = obj.getDocumentList(_doctypeid_thutuckiemtoan, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            { 
                foreach(DataRow row in ds.Tables[0].Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Text = "- " + row["Tên thủ tục kiểm toán"].ToString();
                    node.ToolTip = row["Tên thủ tục kiểm toán"].ToString();
                    treeView.Nodes.Add(node);
                }
            }
        }


        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            GetList();
        }
        #endregion
    }
}
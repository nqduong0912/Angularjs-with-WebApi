using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using C1.Web.C1WebGrid;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class CongDuocPhuTrach_ReViewDotKiemToan : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN;
        protected string _doctypeid_thutuckiemtoan = DOCTYPE.THUTUC_KIEMTOAN;
        protected string _doctypeid_mangnghiepvu = DOCTYPE.MANG_NGHIEPVU;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _doankt = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _fk_doclinkID = string.Empty;
        protected string _thutuc = string.Empty;
        protected string _trangthai = string.Empty;
        protected string _nhanxet = String.Empty;
        protected string _cv = string.Empty;
        int _status_dotkt = 0;
        int _status = 0;
        string _type;

        protected bool _isSubmitted = false;
        //thangma
        protected int _docstatus = 0;
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

            #region get data submit
            _documentid = Request["doc"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];

            _doankt = Request["doankt"];
            _dotkt = Request["dotkt"];
            _fk_doclinkID = Request["fk_doclinkID"];
            _thutuc = Request["thutuc"];
            _trangthai = Request["trangthai"];
            _nhanxet = Request["nhanxet"];
            _cv = Request["cv"];
           
            #endregion

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
           
            //check doc status
            if (_documentid != null)
            {
                _docstatus = CommonFunc.GetDocStatus(_documentid);
            }
            #endregion

            #region init form
            string caption = "Thông tin về công việc";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin về công việc";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
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
            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrEmpty(_action))
                {
                    if (_action == "loaddoc")
                    {
                        BuildTrangThai();
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        BuildTreeViewThuTucKT(treeViewThuTucKT);
                    }
                }
            }
        }
       
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion

        #region helper
        void BuildTrangThai()
        {
            //CommonFunc.LoadDropDownList(this.DOCSTATUS, 4);
            this.txtStatus.Text = CommonFunc.GetTrangThaiCongViec(_documentid);
        }

        void BuildTreeViewThuTucKT(TreeView treeViewThuTucKT)
        {
            DataTable dt = GetDataTableThuTucByCongViec();
            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                    return;
                TreeNode nodeRoot = new TreeNode();
                nodeRoot.Text = "Thủ tục kiểm toán";
                nodeRoot.SelectAction = TreeNodeSelectAction.None;
                treeViewThuTucKT.Nodes.Add(nodeRoot);
                foreach (DataRow row in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Text = row["Tên thủ tục kiểm toán"].ToString();
                    node.ToolTip = row["Tên thủ tục kiểm toán"].ToString();
                    node.SelectAction = TreeNodeSelectAction.None;
                    nodeRoot.ChildNodes.Add(node);
                }
            }
        }

        DataTable GetDataTableThuTucByCongViec()
        {
            if (!String.IsNullOrEmpty(_documentid))
            {
                string DocFields = "PK_DocumentID,[Tên thủ tục kiểm toán]";
                string PropertyFields = "Tên thủ tục kiểm toán";
                string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _documentid + "')";
                bus_Document obj = new bus_Document(_objUserContext);
                DataSet ds = obj.getDocumentList(_doctypeid_thutuckiemtoan, DocFields, PropertyFields, Condition);
                if (isValidDataSet(ds))
                {
                    DataTable dt = ds.Tables[0];
                    dt.Columns.Add("FK_DocLinkID", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        row["FK_DocLinkID"] = _documentid;
                    }
                    return dt;
                }
                return null;
            }
            return null;
        }
        #endregion end helper
       
    }
}
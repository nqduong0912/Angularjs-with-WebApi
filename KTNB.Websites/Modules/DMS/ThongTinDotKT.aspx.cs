using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using System.Data;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class ThongTinDotKT : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOT_KIEMTOAN;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;

        protected string _doclink = string.Empty;
        protected string _doctypeid_doituongkt = DOCTYPE.DOITUONG_KT;
        //protected string _doctypeid_T_Group = DOCTYPE.G;

        protected string _BuildDDLDoiTuongKT = String.Empty;
        protected string _value = string.Empty;
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

            #region get data submit
            _documentid = Request["doc"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];
            //_doclink = Request["doclink"];
            _value = Request["value"];
            if (!string.IsNullOrEmpty(_documentid))
                _status_dotkt = CommonFunc.GetDocStatus(_documentid);
            _BuildDDLDoiTuongKT = Request["BuildDDLDoiTuongKT"];
            #endregion

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                else if (_action == "BuildDDLDoiTuongKT")
                    FeedBackClient(BuildDDLDoiTuongKT(_value));
            }
            #endregion

            #region init form
            string caption = "Thông tin đợt kiểm toán";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin đợt kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Visible = _btnEdit.Visible = _btnDelete.Visible = false;
            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            _btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
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
               
                if (!string.IsNullOrEmpty(_action))
                {
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        //GetListDanhSanhThanhVienByGroupName();
                        TxtTrangThai.Text = CommonFunc.GetTrangThaiDotKT(_status_dotkt);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// build 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// author:quangna
        public string BuildDDLDoiTuongKT(string value)
        {
            string str = String.Empty;
            string DocFields = "PK_DocumentID,[Tên đối tượng kiểm toán],[Loại đối tượng kiểm toán],Status";
            string PropertyFields = "Tên đối tượng kiểm toán,Loại đối tượng kiểm toán";
            string Condition = String.Empty;//" Order By [Loại đối tượng kiểm toán]";
            DataSet ds = DataSource.getDocumentList(_doctypeid_doituongkt, DocFields, PropertyFields, String.Empty);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    ////DataRow[] rows = dt.Select(dt.Columns[2]+"=" + _text + " AND Status = 4");
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[2].ToString() == value && row[3].ToString() == "4")
                        {
                            str = str + "||" + row[1].ToString();
                        }
                    }
                }
            }
            return str;
        }

        //public void GetListDanhSanhThanhVienByGroupName()
        //{
        //    string groupName = this.ID8_A8A3EDBA_F569_4C06_8A57_045FBFED55FD.Text;
        //    if (String.IsNullOrEmpty(groupName))
        //        return;
            
        //    ObjectDataSource1.SelectParameters["GroupName"].DefaultValue = groupName;
        //    dataCtrl.DataBind();
        //}


        //private void BuildDDLDonViThucHien()
        //{
        //    bus_Group objg = bus_Group.Instance(_objUserContext);
        //    string query = "[NAME],[DESCRIPTION]";
        //    string condition = " and FK_PARENTGROUPID='" + GROUPS.GROUP_OF_KTNB + "'";
        //    DataSet ds = objg.getList(condition, query);
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            this.ID8_A8A3EDBA_F569_4C06_8A57_045FBFED55FD.Items.Add(new ListItem(row[0].ToString()));
        //        }
        //    }

        //}

        #region page helper processing

        #endregion


        #region page button processing
        #endregion
    }
}
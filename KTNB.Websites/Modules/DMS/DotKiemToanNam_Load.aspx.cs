using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using System.Data;
using CORE.UMS.CoreBusiness;
using C1.Web.C1WebGrid;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class DotKiemToanNam_Load : PageBase
    {
        #region initiation page variables
        protected string _rootFile = System.Configuration.ConfigurationManager.AppSettings["RootFile"].Replace("\\", "/");
        protected string _http_body = System.Configuration.ConfigurationManager.AppSettings["HTTP_BODY"];

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
        protected string _timkiem = string.Empty;
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

            #region get data submit
            _documentid = Request["doc"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];
            //_doclink = Request["doclink"];
            _value = Request["value"];
            _timkiem = Request["timkiem"];
            _BuildDDLDoiTuongKT = Request["BuildDDLDoiTuongKT"];
            _status_dotkt = CommonFunc.GetDocStatus(_documentid);
            if (!string.IsNullOrEmpty(_documentid))
                _isTruongDoan = CommonFunc.IsTruongDoan(_documentid, _objUserContext.UserName);

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
                if (_action == "checkvalueupdate")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue,_documentid).ToString());
                else if (_action == "BuildDDLDoiTuongKT")
                     FeedBackClient(BuildDDLDoiTuongKT(_value));
                if (_action == "deletefile")
                    FeedBackClient(CommonFunc.deleteBody(_propertyvalue));
            }
            #endregion

            #region init form
            string caption = "Thông tin đợt kiểm toán";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin đợt kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
          

            _btnSave.Attributes.Add("onclick", "{preparesavedoc('"+System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _documentid + "'); return false;}");
            
            _btnDelete.Visible = false;//remove button delete
            _btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");

            //Truong phong chinh sach cong cu moi dc update
            if (_m_roleID == ROLES.TRUONGPHONG_CSCC)
            {
                if (_viewtype == VIEWTYPE.EDIT)
                    _btnEdit.Visible = true;
                //SetEnabledControl(true);
            }
            else
            {
                _btnEdit.Visible = false;
                SetEnabledControl(false);
            }
            if (_viewtype == VIEWTYPE.ADDNEW)
                tbAttachFileList.Visible = false;
            if (_isTruongDoan)
                btnFile.Visible = true;    
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
                 //Load Nam
                CommonFunc.LoadDropDownList(this.ID8_23FB0D9B_6A02_44B0_A1AA_CEFA895CD9C1, 3);

                //Load Thoi gian thuc hien
                CommonFunc.LoadDropDownList(this.ID8_B23A439D_95D3_466E_8AD1_CDDAC260CB27, 2);

                //Load Quy mo dot kiem toan
                CommonFunc.GetLookUpValue("06991CFA-158B-4460-8B7E-EC010C14DFE4", this.ID8_06991CFA_158B_4460_8B7E_EC010C14DFE4);

                //Load loai doi tuong kiem toan
                CommonFunc.GetLookUpValue("737694DF-DE17-4FF9-AE15-70E197C83593", this.ID8_737694DF_DE17_4FF9_AE15_70E197C83593);

                //Load doi tuong kiem toan
                if (this.ID8_737694DF_DE17_4FF9_AE15_70E197C83593.Items.Count > 0)
                {
                    BuildDDLDoiTuongKT(this.ID8_737694DF_DE17_4FF9_AE15_70E197C83593.SelectedValue);
                }

                BuildDDLDonViThucHien();
                if (_viewtype == VIEWTYPE.EDIT)
                    trTrangThai.Visible = true;
                if (!string.IsNullOrEmpty(_action))
                {
                    if (_action == "loaddoc")
                    {
                        //GetFilesList();
                        //trTrangThai.Visible = true;
                        lblTrangThai.Text = CommonFunc.GetTrangThaiDotKT(_status_dotkt);

                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        if (!String.IsNullOrEmpty(_timkiem))
                            if (_timkiem == "tk")
                                CommonFunc.SetEnableControl(false, Page.Master);
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
        public string  BuildDDLDoiTuongKT(string value)
        {
            string str = String.Empty;
            string DocFields = "PK_DocumentID,[Tên đối tượng kiểm toán],[Loại đối tượng kiểm toán],Status";
            string PropertyFields = "Tên đối tượng kiểm toán,Loại đối tượng kiểm toán";
            string Condition = String.Empty;//" Order By [Loại đối tượng kiểm toán]";
            DataSet ds = DataSource.getDocumentList(_doctypeid_doituongkt, DocFields, PropertyFields, String.Empty);
            if (isValidDataSet(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                    if (row[2].ToString() == value && row[3].ToString() == "4")
                        str = str + "||" + row[1].ToString();
            }
            return str;
        }

        private void BuildDDLDonViThucHien()
        {
            bus_Group objg = bus_Group.Instance(_objUserContext);
            string query = "[NAME],[DESCRIPTION]";
            string condition = " and FK_PARENTGROUPID='" + GROUPS.GROUP_OF_KTNB+"'";
            DataSet ds = objg.getList(condition,query);
            if (!isValidDataSet(ds)) 
                return;
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
                this.ID8_A8A3EDBA_F569_4C06_8A57_045FBFED55FD.Items.Add(new ListItem(row["NAME"].ToString()));

        }

        void SetEnabledControl(bool isEnable)
        {
            this.ID8_06991CFA_158B_4460_8B7E_EC010C14DFE4.Enabled = isEnable;
            this.ID8_23FB0D9B_6A02_44B0_A1AA_CEFA895CD9C1.Enabled = isEnable;
            this.ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54.Enabled = isEnable;
            this.ID8_63A0C4B1_2088_4994_B891_2FF65EB20265.Enabled = isEnable;
            this.ID8_737694DF_DE17_4FF9_AE15_70E197C83593.Enabled = isEnable;
            this.ID8_A8A3EDBA_F569_4C06_8A57_045FBFED55FD.Enabled = isEnable;
            this.ID8_B23A439D_95D3_466E_8AD1_CDDAC260CB27.Enabled = isEnable;
            this.ID8_7D31CEB6_69CC_4F8E_9BE6_38762D7C30C6.Enabled = isEnable;
            this.ID8_46C1BE6C_68F3_47B9_B0FB_DEC78831AEFF.Enabled = isEnable;
        }

        #region page helper processing

        #endregion


        #region page button processing
        #endregion

        #region files
        private void GetFilesList()
        {
            ObjectDataSource1.SelectParameters["PhatHienID"].DefaultValue = _documentid;
            dataCtrl.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            GetFilesList();
        }


        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label FileID = (Label)e.Item.FindControl("FileID") as Label;
                Label FILENAME = (Label)e.Item.FindControl("FILENAME") as Label;
                Label FILEPATH = (Label)e.Item.FindControl("FILEPATH") as Label;

                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;
                Image ImgEdit = (Image)e.Item.FindControl("ImgEdit") as Image;

                if (FileID != null)
                {
                    ImgEdit.Attributes.Add("onclick", "{download('" + FILENAME.Text + "','" + FILEPATH.Text.Replace("\\", "/") + "')}");
                    imgDelete.Attributes.Add("onclick", "{DeleteFile('" + FileID.Text + "')}");
                }
                imgDelete.Visible = false;
                if (!string.IsNullOrEmpty(_timkiem))
                    if (_timkiem == "tk")
                        imgDelete.Visible = false;
                if (_isTruongDoan)// chi la truongdoan moi dc nhin remove
                    imgDelete.Visible = true;
            }
        }

        #endregion
    }
}
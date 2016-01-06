using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using C1.Web.C1WebGrid;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;

namespace VPB_KTNB.Helpers
{
    /// <summary>
    /// Summary description for PageBase
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        #region variables for page that inherited

        protected Button _btnAddNew = null;
        protected Button _btnaddLink = null;
        protected Button _btnattackFile = null;
        protected Button _btnSave = null;
        protected Button _btnEdit = null;
        protected Button _btnDelete = null;
        protected Button _btnRemove = null;
        protected Button _btnCancel = null;
        protected Button _btnPrint = null;
        protected Button _btnFinish = null;
        protected Button _btnTransfer = null;
        protected Button _btnMonitoring = null;
        protected Button _btnContent = null;
        protected Button _btnExcel = null;
        protected Button _btnReOpen = null;
        protected Button _btnResolve = null;
        protected Button _btnClose = null;
        protected Button _btnCloseWindow = null;
        protected Button _btnSharePermission = null;
        protected ContentPlaceHolder _Form;

        protected CORE.CoreObjectContext.UserContext _objUserContext = null;
        protected string _dbName = string.Empty;
        protected bool _isSysAdmin = false;
        protected bool _isAppAdmin = false;
        protected string _m_roleID = string.Empty;
        protected string _m_groupid = string.Empty;
        protected byte _m_grouptype = 0;
        protected string _m_groupname = string.Empty;
        protected string _m_groupdescription = string.Empty;
        protected string _m_mnemonic = string.Empty;

        #endregion variables for page that inherited

        private ContentPlaceHolder ph = new ContentPlaceHolder();

        #region constructor

        /// <summary>
        /// PageBase
        /// </summary>
        public PageBase()
        {
            _dbName = System.Configuration.ConfigurationManager.AppSettings["DBName"].ToString();
        }

        #endregion constructor

        public string appID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["a"]) ? string.Empty : Request.QueryString["a"].ToString();
            }
        }

        public string curAppID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["curApp"]) ? appID : Request.QueryString["curApp"].ToString();
            }
        }

        #region init for each form instance

        /// <summary>
        /// InitForm
        /// </summary>
        /// <param name="Caption"></param>
        /// <param name="Icon"></param>
        /// <param name="ViewID"></param>
        /// <param name="ViewType"></param>
        protected virtual void InitForm(string Caption, string Icon = "", string DoctypeID = "", byte ViewType = 0)
        {
            #region Form instance

            ph = (ContentPlaceHolder)this.Master.Master.FindControl("ContentPlaceHolder1");
            Label _frmCaption = null;
            _frmCaption = (Label)ph.FindControl("lblFormCaption");
            _frmCaption.Text = Caption;

            #endregion Form instance

            #region Form buttons

            //SAVE button
            _btnSave = (Button)ph.FindControl("btnSAVE");
            _btnSave.Click += new EventHandler(SaveForm);

            //EDIT button
            _btnEdit = (Button)ph.FindControl("btnEDIT");
            _btnEdit.Click += new EventHandler(EditForm);

            //DELETE button
            _btnDelete = (Button)ph.FindControl("btnDELETE");
            _btnDelete.Click += new EventHandler(DeleteForm);

            //REMOVE button
            _btnRemove = (Button)ph.FindControl("btnREMOVE");
            _btnRemove.Click += new EventHandler(RemoveForm);

            //CANCEL button
            _btnCancel = (Button)ph.FindControl("btnCANCEL");
            _btnCancel.Click += new EventHandler(CancelForm);

            //PRINT button
            _btnPrint = (Button)ph.FindControl("btnPRINT");
            _btnPrint.Click += new EventHandler(PrintForm);

            //addLink button
            _btnaddLink = (Button)ph.FindControl("btnaddLink");
            _btnaddLink.Click += new EventHandler(addLinkForm);

            //attachFile button
            _btnattackFile = (Button)ph.FindControl("btnATTACHFILE");
            _btnattackFile.Click += new EventHandler(attachFileForm);

            //ADDNEW button
            _btnAddNew = (Button)ph.FindControl("btnADDNEW");
            _btnAddNew.Click += new EventHandler(AddnewForm);

            //FINISH button
            _btnFinish = (Button)ph.FindControl("btnFINISH");
            _btnFinish.Click += new EventHandler(FinishForm);

            //TRANSFER button
            _btnTransfer = (Button)ph.FindControl("btnTRANSFER");
            _btnTransfer.Click += new EventHandler(TransferForm);

            //MONITORING button
            _btnMonitoring = (Button)ph.FindControl("btnMONITORING");
            _btnMonitoring.Click += new EventHandler(MonitoringForm);

            //EXCEL button
            _btnExcel = (Button)ph.FindControl("btnEXCEL");
            _btnExcel.Click += new EventHandler(ExcelForm);

            //CONTENT button
            _btnContent = (Button)ph.FindControl("btnCONTENT");
            _btnContent.Click += new EventHandler(ContentForm);

            //REOPEN button
            _btnReOpen = (Button)ph.FindControl("btnREOPEN");
            _btnReOpen.Click += new EventHandler(ReOpenIssue);

            //RESOLVE button
            _btnResolve = (Button)ph.FindControl("btnRESOLVE");
            _btnResolve.Click += new EventHandler(ResolvedIssue);

            //CLOSE button
            _btnClose = (Button)ph.FindControl("btnCLOSE");
            _btnClose.Click += new EventHandler(CloseIssue);

            //CLOSEWIN button
            _btnCloseWindow = (Button)ph.FindControl("btnCLOSEWIN");
            _btnCloseWindow.Click += new EventHandler(CloseWindow);
            _btnCloseWindow.Attributes.Add("onclick", "{parent.window.close(true);return false;}");

            //SHAREPERMISSION button
            _btnSharePermission = (Button)ph.FindControl("btnSHAREPERMISSION");
            _btnSharePermission.Click += new EventHandler(SharePermission);

            #endregion Form buttons

            #region Form instance

            _Form = (ContentPlaceHolder)ph.FindControl("FormContent");
            FormHelper.Form = _Form;

            FormHelper.ViewID = DoctypeID;
            FormHelper.ViewType = ViewType;
            FormHelper.FolderID = DOCTYPE.THU_MUC;

            if (!string.IsNullOrEmpty(DoctypeID))
                FormHelper.DocSpaceID = GetDocSpaceByDocType(DoctypeID);
            else
                FormHelper.DocSpaceID = string.Empty;

            _btnCloseWindow.Visible = false;
            _btnRemove.Visible = false;

            if (ViewType == VIEWTYPE.ADDNEW)
            {
                _btnSave.Visible = true;
            }
            else if (ViewType == VIEWTYPE.EDIT)
            {
                _btnEdit.Visible = true;
                _btnDelete.Visible = true;
            }
            else if (ViewType == VIEWTYPE.EDIT_ON_PROCESS)
            {
                _btnEdit.Visible = true;
                _btnDelete.Visible = true;
                _btnTransfer.Visible = true;
                _btnattackFile.Visible = true;
                _btnMonitoring.Visible = true;
            }
            else if (ViewType == VIEWTYPE.SHOW)
            {
                _btnAddNew.Visible = true;
                _btnCloseWindow.Visible = true;
            }
            else if (ViewType == VIEWTYPE.REPORT)
            {
                _btnPrint.Visible = true;
            }

            _btnSave.Attributes.Add("onclick", "{ShowIndicator();return true;}");
            _btnEdit.Attributes.Add("onclick", "{ShowIndicator();return true;}");
            _btnDelete.Attributes.Add("onclick", "{ShowIndicator();return true;}");
            _btnRemove.Attributes.Add("onclick", "{ShowIndicator();return true;}");

            #endregion Form instance
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Caption"></param>
        protected virtual void SetFormCaption(string Caption)
        {
            ph = (ContentPlaceHolder)this.Master.Master.FindControl("ContentPlaceHolder1");
            Label _frmCaption = (Label)ph.FindControl("lblFormCaption") as Label;
            _frmCaption.Text = Caption;
        }

        /// <summary>
        /// FormTransactionError
        /// </summary>
        /// <param name="ErrMsg"></param>
        protected virtual void FormTransactionError(string ErrMsg)
        {
            ph = (ContentPlaceHolder)this.Master.Master.FindControl("ContentPlaceHolder1");
            Label lblMessage = (Label)ph.FindControl("lblMessage");
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Font.Bold = true;
            lblMessage.Text = ErrMsg;
        }

        /// <summary>
        /// FormTransactionError
        /// </summary>
        protected virtual void FormTransactionError()
        {
            ph = (ContentPlaceHolder)this.Master.Master.FindControl("ContentPlaceHolder1");
            HtmlTable tblMess = (HtmlTable)ph.FindControl("tblMess");
            tblMess.Visible = true;
            Label lblErrMess = (Label)ph.FindControl("lblErrMess");
            lblErrMess.Text = "Thao tác không thành công. Kiểm tra lại dữ liệu hoặc liên hệ với admin !";
        }

        /// <summary>
        /// FormTransactionSuccess
        /// </summary>
        /// <param name="ErrMsg"></param>
        protected virtual void FormTransactionSuccess(string ErrMsg)
        {
            if (!string.IsNullOrEmpty(ErrMsg))
            {
                ph = (ContentPlaceHolder)this.Master.Master.FindControl("ContentPlaceHolder1");
                HtmlTable tblMess = (HtmlTable)ph.FindControl("tblMess");
                tblMess.Visible = true;
                Label lblErrMess = (Label)ph.FindControl("lblMessage");
                lblErrMess.ForeColor = System.Drawing.Color.Blue;
                lblErrMess.Text = ErrMsg;
            }
        }

        /// <summary>
        /// FormTransactionSuccess
        /// </summary>
        protected virtual void FormTransactionSuccess()
        {
            ph = (ContentPlaceHolder)this.Master.Master.FindControl("ContentPlaceHolder1");
            HtmlTable tblMess = (HtmlTable)ph.FindControl("tblMess");
            tblMess.Visible = true;
            Label lblErrMess = (Label)ph.FindControl("lblErrMess");
            lblErrMess.ForeColor = System.Drawing.Color.Blue;
            lblErrMess.Text = "Thao tác thành công.";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            OnBeforeLoad();
            base.OnLoad(e);
            OnAfterLoad();
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual void OnBeforeLoad()
        {
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual void OnAfterLoad()
        {
        }

        #endregion init for each form instance

        #region virtual method for implementing by building each form

        protected virtual void SaveForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// EditForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifiedby></modifiedby>
        /// <modifieddate></modifieddate>
        protected virtual void EditForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// DeleteForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual void DeleteForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// RemoveForm
        /// </summary>
        protected virtual void RemoveForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// CancelForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual void CancelForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// AddnewForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual void AddnewForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// LinkForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void addLinkForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void attachFileForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// PrintForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual void PrintForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// FinishForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual void FinishForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// TransferForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual void TransferForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// MonitoringForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual void MonitoringForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ExcelForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ContentForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ContentForm(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ReOpenIssue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ReOpenIssue(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ResolvedIssue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ResolvedIssue(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// CloseIssue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void CloseIssue(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// SharePermission
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void SharePermission(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// CloseWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void CloseWindow(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// BindData2Tree
        /// </summary>
        /// <param name="tvw">treeview control on form</param>
        /// <param name="rootText">treeview's root text</param>
        /// <param name="ds">dataset containt data which will be map to treeview control</param>
        /// <param name="NodeID_FieldName">field id on dataset which will be map to each node as node id</param>
        /// <param name="NodeText_FieldName">field text on dataset which will be map to each node as node text</param>
        /// <param name="Url">url which will be directed to</param>
        /// <param name="Target">frame target to host Url above</param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual void BindData2Tree(TreeView tvw, string rootText, DataSet ds, string NodeID_FieldName, string NodeText_FieldName, string NodeTooltip_FieldName, string Url, string Target, string icon)
        {
            //make root node with text
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            tvw.Nodes.Add(rootNode);

            if (!isValidDataSet(ds)) return;

            //add child nodes
            foreach (DataRow R in ds.Tables[0].Rows)
            {
                TreeNode childNode = new TreeNode();
                string NodeID = R[NodeID_FieldName].ToString();
                string NodeText = R[NodeText_FieldName].ToString();
                childNode.Text = "<img src='../../Images/" + icon + "' border='0'></img><font color='blue'> " + NodeText + "</font>";
                childNode.ToolTip = R[NodeTooltip_FieldName].ToString();
                if (!string.IsNullOrEmpty(Url))
                    childNode.NavigateUrl = Url + "?id=" + NodeID + "&name=" + NodeText;
                if (!string.IsNullOrEmpty(Target))
                    childNode.Target = Target;
                rootNode.ChildNodes.Add(childNode);
            }
        }

        /// <summary>
        /// BindData2Tree
        /// </summary>
        /// <param name="tvw"></param>
        /// <param name="rootText"></param>
        /// <param name="ds"></param>
        /// <param name="NodeID_FieldName"></param>
        /// <param name="NodeText_FieldName"></param>
        /// <param name="NodeTooltip_FieldName"></param>
        /// <param name="Url"></param>
        /// <param name="Target"></param>
        protected virtual void BindData2Tree(TreeView tvw, string rootText, DataSet ds, string NodeID_FieldName, string NodeText_FieldName, string NodeTooltip_FieldName, string Url, string Target)
        {
            //make root node with text
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            tvw.Nodes.Add(rootNode);

            if (!isValidDataSet(ds)) return;

            //add child nodes
            foreach (DataRow R in ds.Tables[0].Rows)
            {
                TreeNode childNode = new TreeNode();
                string NodeID = R[NodeID_FieldName].ToString();
                string NodeText = R[NodeText_FieldName].ToString();
                childNode.Text = NodeText;
                childNode.ToolTip = R[NodeTooltip_FieldName].ToString();
                if (!string.IsNullOrEmpty(Url))
                    childNode.NavigateUrl = Url + "?id=" + NodeID + "&name=" + NodeText;
                if (!string.IsNullOrEmpty(Target))
                    childNode.Target = Target;
                rootNode.ChildNodes.Add(childNode);
            }
        }

        /// <summary>
        /// BindData2Combo
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="ds"></param>
        /// <param name="TextField"></param>
        /// <param name="ValueField"></param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual void BindData2Combo(DropDownList cbo, DataSet ds, string TextField, string ValueField, string TextDefault)
        {
            cbo.DataSource = ds;
            cbo.DataValueField = ValueField;
            cbo.DataTextField = TextField;
            cbo.DataBind();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="PropertyID"></param>
        protected virtual void BuildLookupValue(DropDownList cbo, string DefaultValue)
        {
            string PropertyID = StringHelper.Right(cbo.ID, 36);
            PropertyID = PropertyID.Replace("_", "-");
            bus_Property obj = bus_Property.Instance(_objUserContext);
            DataSet ds = obj.GetLookUpValue(PropertyID);
            obj = null;
            if (isValidDataSet(ds))
            {
                BindData2Combo(cbo, ds, "Value", "Value", "");
                if (!string.IsNullOrEmpty(DefaultValue))
                    cbo.SelectedValue = DefaultValue;
            }
        }

        protected virtual void BindData2ComboMonth(DropDownList cbo)
        {
            for (byte i = 1; i <= 12; i++)
            {
                string ii = i.ToString();
                if (ii.Length == 1) ii = "0" + ii;
                ListItem item = new ListItem(ii, ii);
                cbo.Items.Add(item);
            }
        }

        /// <summary>
        /// BindData2ListBox
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="ds"></param>
        /// <param name="TextField"></param>
        /// <param name="ValueField"></param>
        /// <param name="TextDefault"></param>
        protected virtual void BindData2ListBox(ListBox lst, DataSet ds, string TextField, string ValueField, string TextDefault)
        {
            lst.DataSource = ds;
            lst.DataValueField = ValueField;
            lst.DataTextField = TextField;
            lst.DataBind();
        }

        /// <summary>
        /// BindData2CheckBoxList
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="ds"></param>
        /// <param name="TextField"></param>
        /// <param name="ValueField"></param>
        /// <param name="TextDefault"></param>
        protected virtual void BindData2CheckBoxList(CheckBoxList lst, DataSet ds, string TextField, string ValueField, string TextDefault)
        {
            lst.DataSource = ds;
            lst.DataValueField = ValueField;
            lst.DataTextField = TextField;
            lst.DataBind();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="ds"></param>
        /// <param name="TextFields"></param>
        /// <param name="Seperator"></param>
        /// <param name="ValueField"></param>
        /// <param name="TextDefault"></param>
        /// <param name="hint"></param>
        protected virtual void BindData2Combo(DropDownList cbo, DataSet ds, string TextFields, string Seperator, string ValueField, string TextDefault, string hint)
        {
        }

        /// <summary>
        /// isValidDataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual bool isValidDataSet(DataSet ds)
        {
            bool valid = false;
            if (ds != null)
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        valid = true;
            return valid;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        protected virtual void FeedBackClient(string message)
        {
            HttpContext.Current.Response.Write(message);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// ReloadFolderView
        /// </summary>
        /// <param name="Url"></param>
        /// <author>dungnt</author>
        /// <createddate>20080828</createddate>
        /// <modifieddate></modifieddate>
        /// <modifiedby></modifiedby>
        protected virtual void ReloadFolderView(string Url)
        {
        }

        /// <summary>
        /// GetDocSpaceByDocType
        /// </summary>
        /// <param name="DocTypeID"></param>
        /// <returns></returns>
        protected virtual string GetDocSpaceByDocType(string DocTypeID)
        {
            bus_Document_Type objDocType = bus_Document_Type.Instance(_objUserContext); //bus_Document_Type objDocType = new bus_Document_Type(_objUserContext, _dbName);
            DataSet ds = objDocType.GetDocSpaceList(DocTypeID);
            if (!isValidDataSet(ds))
                return string.Empty;
            return ds.Tables[0].Rows[0]["FK_DOCSPACEID"].ToString();
        }

        /// <summary>
        /// SaveCT
        /// </summary>
        /// <param name="Form"></param>
        protected virtual void SaveCT(Control Form)
        {
            FormHelper.CT.Clear();
            foreach (Control ctl in Form.Controls)
            {
                //TEXTBOX
                if (ctl.GetType() == typeof(TextBox))
                {
                    FormHelper.CT.Add(ctl.ID, ((TextBox)ctl).Text);
                    continue;
                }

                //CHECKBOX
                if (ctl.GetType() == typeof(CheckBox))
                {
                    if (((CheckBox)ctl).Checked)
                        FormHelper.CT.Add(ctl.ID, ((CheckBox)ctl).Text);
                    else
                        FormHelper.CT.Add(ctl.ID, DBNull.Value);
                    continue;
                }

                //DROPDOWNLIST
                if (ctl.GetType() == typeof(DropDownList))
                {
                    FormHelper.CT.Add(ctl.ID, ((DropDownList)ctl).SelectedValue);
                    continue;
                }

                //RADIOBUTTONLIST
                if (ctl.GetType() == typeof(RadioButtonList))
                {
                    FormHelper.CT.Add(ctl.ID, ((RadioButtonList)ctl).SelectedValue);
                    continue;
                }
            }
        }

        /// <summary>
        /// LoadContentByDocType
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        /// <param name="Query"></param>
        /// <param name="Condition"></param>
        /// <param name="dataCtrl"></param>
        /// <param name="SqlDataSource1"></param>
        //protected virtual void LoadContentByDocType(string DocumentTypeID, string Query, string Condition, ABB.DMG.DataInput.ControlExtend.EntityGridViewWithFirstCheck dataCtrl, SqlDataSource SqlDataSource1)
        //{
        //    SqlDataSource1.ConnectionString = _objUserContext.ConnectionString;
        //    SqlDataSource1.SelectCommand = "DMS_GETDOCUMENT_LIST";
        //    SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

        //    SqlDataSource1.SelectParameters.Clear();

        //    SqlDataSource1.SelectParameters.Add(new Parameter("DocumentTypeID", TypeCode.String, DocumentTypeID));
        //    SqlDataSource1.SelectParameters.Add(new Parameter("Query", TypeCode.String, Query));
        //    SqlDataSource1.SelectParameters.Add(new Parameter("Condition", TypeCode.String, Condition));

        //    try
        //    {
        //        dataCtrl.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        VPB_CRM.Helper.FormHelper.FormWarning("Function warning",ex.Message + "<br/>" + ex.Source,"red");
        //        return;
        //    }
        //}
        /// <summary>
        /// LoadContentUserAsDoclink
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="Query"></param>
        /// <param name="dataCtrl"></param>
        /// <param name="SqlDataSource1"></param>
        protected virtual void LoadContentUserAsDoclink(string DocumentID, string Query, C1.Web.C1WebGrid.C1WebGrid dataCtrl, SqlDataSource SqlDataSource1)
        {
            SqlDataSource1.ConnectionString = _objUserContext.ConnectionString;
            SqlDataSource1.SelectCommand = "DBO.DMS_GetUser_AsDoclink";
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

            SqlDataSource1.SelectParameters.Clear();

            SqlDataSource1.SelectParameters.Add(new Parameter("DocumentID", TypeCode.String, DocumentID));

            try
            {
                dataCtrl.DataBind();
            }
            catch (Exception ex)
            {
                FormHelper.FormWarning("Function warning", ex.Message + "<br/>" + ex.Source, "red");
                return;
            }
        }

        /// <summary>
        /// LoadContentByDocType
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        /// <param name="Query"></param>
        /// <param name="Condition"></param>
        /// <param name="dataCtrl"></param>
        /// <param name="SqlDataSource1"></param>
        protected virtual void LoadContentByDocType(string DocumentTypeID, string Query, string Condition, C1.Web.C1WebGrid.C1WebGrid dataCtrl, SqlDataSource SqlDataSource1)
        {
            SqlDataSource1.ConnectionString = _objUserContext.ConnectionString;
            SqlDataSource1.SelectCommand = "DMS_GETDOCUMENT_LIST";
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

            SqlDataSource1.SelectParameters.Clear();

            SqlDataSource1.SelectParameters.Add(new Parameter("DocumentTypeID", TypeCode.String, DocumentTypeID));
            SqlDataSource1.SelectParameters.Add(new Parameter("Query", TypeCode.String, Query));
            SqlDataSource1.SelectParameters.Add(new Parameter("Condition", TypeCode.String, Condition));

            try
            {
                dataCtrl.DataBind();
            }
            catch (Exception ex)
            {
                FormHelper.FormWarning("Function warning", ex.Message + "<br/>" + ex.Source, "red");
                return;
            }
        }

        /// <summary>
        /// LoadContentByDocTypeAndProcess
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        /// <param name="Query"></param>
        /// <param name="Condition"></param>
        /// <param name="dataCtrl"></param>
        /// <param name="SqlDataSource1"></param>
        /// <author>dungnt</author>
        /// <createddate>20080930</createddate>
        //protected virtual void LoadContentByDocTypeAndProcess(string DocumentTypeID, string Query, string Condition, ABB.DMG.DataInput.ControlExtend.EntityGridViewWithFirstCheck dataCtrl, SqlDataSource SqlDataSource1)
        //{
        //    SqlDataSource1.ConnectionString = _objUserContext.ConnectionString;
        //    SqlDataSource1.SelectCommand = "DMS_GetDocument_List_And_Process";
        //    SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

        //    SqlDataSource1.SelectParameters.Clear();

        //    SqlDataSource1.SelectParameters.Add(new Parameter("DocumentTypeID", TypeCode.String, DocumentTypeID));
        //    SqlDataSource1.SelectParameters.Add(new Parameter("Query", TypeCode.String, Query));
        //    SqlDataSource1.SelectParameters.Add(new Parameter("Condition", TypeCode.String, Condition));

        //    try
        //    {
        //        dataCtrl.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        VPB_CRM.Helper.FormHelper.FormWarning("Function warning", ex.Message + "<br/>" + ex.Source, "red");
        //        return;
        //    }
        //}
        /// <summary>
        /// LoadContentByDocTypeAssignedActvity
        /// </summary>
        /// <param name="ActivityDefinitionID"></param>
        /// <param name="DocumentTypeID"></param>
        /// <param name="Query"></param>
        /// <param name="Condition"></param>
        /// <param name="dataCtrl"></param>
        /// <param name="SqlDataSource1"></param>
        //protected virtual void LoadContentByDocTypeAssignedActvity(string ActivityDefinitionID, string DocumentTypeID, string Query, string Condition, ABB.DMG.DataInput.ControlExtend.EntityGridViewWithFirstCheck dataCtrl, SqlDataSource SqlDataSource1)
        //{
        //    SqlDataSource1.ConnectionString = _objUserContext.ConnectionString;
        //    SqlDataSource1.SelectCommand = "DMS_GetDocument_List_Assigned_Activity";
        //    SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

        //    SqlDataSource1.SelectParameters.Clear();

        //    SqlDataSource1.SelectParameters.Add(new Parameter("ActivityDefinition", TypeCode.String, ActivityDefinitionID));
        //    SqlDataSource1.SelectParameters.Add(new Parameter("DocumentType", TypeCode.String, DocumentTypeID));
        //    SqlDataSource1.SelectParameters.Add(new Parameter("Query", TypeCode.String, Query));
        //    SqlDataSource1.SelectParameters.Add(new Parameter("Condition", TypeCode.String, Condition));

        //    try
        //    {
        //        dataCtrl.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        VPB_CRM.Helper.FormHelper.FormWarning("Function warning", ex.Message + "<br/>" + ex.Source, "red");
        //        return;
        //    }
        //}
        /// <summary>
        /// LoadContentByDocTypeAssignedActvity
        /// </summary>
        /// <param name="ActivityDefinitionID"></param>
        /// <param name="DocumentTypeID"></param>
        /// <param name="Query"></param>
        /// <param name="Condition"></param>
        /// <param name="dataCtrl"></param>
        /// <param name="SqlDataSource1"></param>
        protected virtual void LoadContentByDocTypeAssignedActvity(string ActivityDefinitionID, string DocumentTypeID, string Query, string Condition, C1WebGrid dataCtrl, SqlDataSource SqlDataSource1)
        {
            SqlDataSource1.ConnectionString = _objUserContext.ConnectionString;
            SqlDataSource1.SelectCommand = "DMS_GetDocument_List_Assigned_Activity";
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

            SqlDataSource1.SelectParameters.Clear();

            SqlDataSource1.SelectParameters.Add(new Parameter("ActivityDefinition", TypeCode.String, ActivityDefinitionID));
            SqlDataSource1.SelectParameters.Add(new Parameter("DocumentType", TypeCode.String, DocumentTypeID));
            SqlDataSource1.SelectParameters.Add(new Parameter("Query", TypeCode.String, Query));
            SqlDataSource1.SelectParameters.Add(new Parameter("Condition", TypeCode.String, Condition));

            try
            {
                dataCtrl.DataBind();
            }
            catch (Exception ex)
            {
                FormHelper.FormWarning("Function warning", ex.Message + "<br/>" + ex.Source, "red");
                return;
            }
        }

        /// <summary>
        /// LoadAuditInfo
        /// </summary>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="dataCtrl"></param>
        /// <param name="SqlDataSource1"></param>
        protected virtual void LoadAuditInfo(string condition, string query, C1WebGrid dataCtrl, SqlDataSource SqlDataSource1)
        {
            SqlDataSource1.CacheDuration = 300;
            SqlDataSource1.EnableCaching = true;

            SqlDataSource1.ConnectionString = _objUserContext.ConnectionString;
            SqlDataSource1.SelectCommand = "DMS_GET_AUDITINFO";
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

            SqlDataSource1.SelectParameters.Clear();

            SqlDataSource1.SelectParameters.Add(new Parameter("CONDITION", TypeCode.String, condition));
            SqlDataSource1.SelectParameters.Add(new Parameter("QUERY", TypeCode.String, query));

            try
            {
                dataCtrl.DataBind();
            }
            catch (Exception ex)
            {
                FormHelper.FormWarning("Function warning", ex.Message + "<br/>" + ex.Source, "red");
                return;
            }
        }

        /// <summary>
        /// dataCtrl_OnSorting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dataCtrl_OnSorting(object sender, GridViewSortEventArgs e)
        {
        }

        /// <summary>
        /// dataCtrl_OnRowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dataCtrl_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region these code must be inherited

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "{if(this.style.backgroundColor!=color_selected) {this.style.backgroundColor='#e6e6dc';}}");
                e.Row.Attributes.Add("onmouseout", "{if(this.style.backgroundColor!=color_selected) {this.style.backgroundColor='';document.body.style.cursor='default';}}");
                string ctl_parent = e.Row.Parent.ClientID;
                e.Row.Attributes.Add("onclick", "{color_selected='';if(this!=obj_selected) {this.style.backgroundColor=color_selected;if(obj_selected!=null) obj_selected.style.backgroundColor='white';obj_selected=this;}}");
            }

            #endregion these code must be inherited
        }

        /// <summary>
        /// dataCtrl_OnItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
        }

        /// <summary>
        /// C1WebGrid1_ItemCreated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void dataCtrl_OnItemCreated(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;

            #region grid's row mouse over

            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "{if(this.style.backgroundColor!=color_selected) {this.style.backgroundColor='#e6e6dc';}}");
                e.Item.Attributes.Add("onmouseout", "{if(this.style.backgroundColor!=color_selected) {this.style.backgroundColor='';document.body.style.cursor='default';}}");
                e.Item.Attributes.Add("onclick", "{color_selected='Moccasin';if(this!=obj_selected) {this.style.backgroundColor=color_selected;if(obj_selected!=null) obj_selected.style.backgroundColor='white';obj_selected=this;}}");
            }

            #endregion grid's row mouse over

            #region grid's pager format

            if (elemType == C1ListItemType.Pager)
            {
                TableCell pager = (TableCell)e.Item.Controls[0];

                // Loop through the pager buttons skipping over blanks
                // (Blanks are treated as LiteralControl(s)
                for (int i = 0; i < pager.Controls.Count; i += 2)
                {
                    Object o = pager.Controls[i];
                    if (o is LinkButton)
                    {
                        LinkButton h = (LinkButton)o;
                        h.Text = "[ " + h.Text + " ]";
                    }
                    else
                    {
                        if (o is HyperLink)
                        {
                            HyperLink h = (HyperLink)o;
                            h.Text = "[ " + h.Text + " ]";
                        }
                        else
                        {
                            Label l = (Label)o;

                            l.Text = "<strong>" + l.Text + "</strong>";
                        }
                    }
                }
            }

            #endregion grid's pager format
        }

        /// <summary>
        /// GetLookUpValue
        /// </summary>
        /// <param name="PropertyID"></param>
        /// <returns></returns>
        protected virtual DataSet GetLookUpValue(string PropertyID)
        {
            bus_Property objProperty = bus_Property.Instance(_objUserContext);
            DataSet ds = objProperty.GetLookUpValue(PropertyID);
            return ds;
        }

        /// <summary>
        /// LoadTreeFolderByDocSpace
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="rootText"></param>
        /// <param name="DocspaceID"></param>
        /// <param name="Url"></param>
        /// <param name="Target"></param>
        protected virtual void LoadTreeFolderByDocSpace(TreeView tvw, string rootText, string DocspaceID, string Url, string Target)
        {
            //add root node with text
            //TreeNode rootNode = new TreeNode();
            //rootNode.Text = rootText;
            //tvw.Nodes.Add(rootNode);

            ////get Docspace's doctype
            //string DocumentTypeID = GetDocTypeByDocSpace(DocspaceID);
            //if (string.IsNullOrEmpty(DocumentTypeID)) return;

            ////add child node as folder year
            //bus_Document objDocument = bus_Document.Instance(_objUserContext);  //bus_Document objDocument = new bus_Document(_objUserContext, _dbName);
            //DataSet dsFolderYear = objDocument.getDocumentList(DocumentTypeID, " DISTINCT YEAR", " AND FK_DOCSPACEID='" + DocspaceID + "' ORDER BY YEAR DESC");
            //if (!isValidDataSet(dsFolderYear)) return;
            //foreach (DataRow R in dsFolderYear.Tables[0].Rows)
            //{
            //    string year = R["YEAR"].ToString();
            //    TreeNode yearNode = new TreeNode();
            //    yearNode.Text = "Năm " + year;
            //    yearNode.NavigateUrl = "";
            //    yearNode.ToolTip = yearNode.Text;
            //    tvw.Nodes[0].ChildNodes.Add(yearNode);
            //    CreaMonthNode(yearNode, year,DocspaceID,DocumentTypeID);
            //}
        }

        /// <summary>
        /// getAppParam
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        protected virtual object getAppSetting(string Key)
        {
            return ConfigurationManager.AppSettings[Key];
        }

        /// <summary>
        /// AuthorizeUserCtx
        /// authorizing UserContext object for valid session
        /// </summary>
        /// <author>dungnt</author>
        /// <createddate>20090122</createddate>
        protected virtual void AuthorizeUserCtx()
        {
            _objUserContext = (UserContext)Session["objUserContext"];
            if (_objUserContext == null)
            {
                FormHelper.FormWarning("Hết thời gian phiên làm việc", "Phiên làm việc hiện tại của bạn đã hết thời gian. Bạn hãy đăng nhập lại.", "white");
            }

            _m_roleID = ((Role)_objUserContext.Roles[0]).RoleID.ToUpper();

            if (_m_roleID.ToUpper() == ROLES.SYS_ADMIN.ToUpper())
                _isSysAdmin = true;
            else if (_m_roleID.ToUpper() == ROLES.APP_ADMIN.ToUpper())
                _isAppAdmin = true;

            byte i = _objUserContext.GroupDefault;
            _m_grouptype = ((Group)_objUserContext.Groups[i]).GroupType;
            _m_groupname = ((Group)_objUserContext.Groups[i]).GroupName;
            _m_groupdescription = ((Group)_objUserContext.Groups[i]).GroupDescription;
            _m_groupid = ((Group)_objUserContext.Groups[i]).GroupID;
            _m_mnemonic = ((Group)_objUserContext.Groups[i]).Mnemonic;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="RoleID"></param>
        protected virtual bool ctxIsRole(string RoleID)
        {
            return RoleID.ToUpper().LastIndexOf(_m_roleID.ToUpper()) != -1 ? true : false;
        }

        #endregion virtual method for implementing by building each form

        #region Helper methods

        /// <summary>
        /// GetDocTypeByDocSpace
        /// </summary>
        /// <param name="DocspaceID"></param>
        /// <returns></returns>
        private string GetDocTypeByDocSpace(string DocspaceID)
        {
            bus_Document_Type objDocType = bus_Document_Type.Instance(_objUserContext);  //bus_Document_Type objDocType = new bus_Document_Type(_objUserContext, _dbName);
            DataSet ds = objDocType.GetDocTypeByDocSpace(DocspaceID);
            if (!isValidDataSet(ds)) return string.Empty;
            return ds.Tables[0].Rows[0]["FK_DOCTYPEID"].ToString();
        }

        /// <summary>
        /// CreaMonthNode
        /// </summary>
        /// <param name="yearNode"></param>
        /// <param name="year"></param>
        /// <param name="DocSpaceID"></param>
        /// <param name="DocumentTypeID"></param>
        private void CreaMonthNode(TreeNode yearNode, string year, string DocSpaceID, string DocumentTypeID)
        {
            //bus_Document objDocument = bus_Document.Instance(_objUserContext);  //bus_Document objDocument = new bus_Document(_objUserContext, _dbName);
            //DataSet dsFolderMonth = objDocument.getDocumentList(DocumentTypeID, " DISTINCT MONTH", " AND FK_DOCSPACEID='" + DocSpaceID + "' AND YEAR='" + year + "' ORDER BY MONTH DESC");
            //if (!isValidDataSet(dsFolderMonth)) return;
            //foreach (DataRow R in dsFolderMonth.Tables[0].Rows)
            //{
            //    string month = R["MONTH"].ToString().Trim();
            //    if (month.Length == 1) month = "0" + month;
            //    TreeNode monthNode = new TreeNode();
            //    monthNode.Text = "Tháng " + month;
            //    monthNode.ToolTip = monthNode.Text;
            //    monthNode.NavigateUrl = "DocViewer.aspx?id=" + DocumentTypeID + "&y=" + year + "&m=" + month;
            //    monthNode.Target = "fraTopic";
            //    yearNode.ChildNodes.Add(monthNode);
            //}
        }

        /// <summary>
        /// reLogin
        /// </summary>
        private void reLogin()
        {
            string url = "../../SignIn.aspx";
            StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\">");
            script.Append("window.open('" + url + "','_top');");
            script.Append("</script>");

            Response.Clear();
            Session.Contents.RemoveAll();
            Session.Abandon();
            Response.Write(script.ToString());
            Response.Flush();
            Response.End();
        }

        #endregion Helper methods

        #region for debuging

        /// <summary>
        /// TraceLn
        /// </summary>
        /// <param name="mess"></param>
        protected virtual void TraceLn(string mess)
        {
            Response.Write(mess + "<br/>");
        }

        /// <summary>
        /// Pause
        /// </summary>
        protected virtual void Pause()
        {
            Response.End();
        }

        /// <summary>
        /// OpenUrl
        /// </summary>
        /// <param name="url"></param>
        /// <param name="target"></param>
        protected virtual void OpenUrl(string url, string target)
        {
            System.Text.StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\"> alert('hi');");
            script.Append("window.open('" + url + "','" + target + "');");
            script.Append("</script>");
        }

        #endregion for debuging
    }
}
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.OPERATORS;
using vpb.app.business.ktnb.Definition.WFS;
using VPB_KTNB.Helpers;

public partial class Tab_tab : PageBase
{
    #region initiation page variables
    protected string _action = string.Empty;
    protected string _documentid = string.Empty;
    protected byte _viewtype = 0;
    protected string _sysreq = string.Empty;
    protected string _wf_instanceid = string.Empty;
    protected int _wf_instancestatus = 0;
    protected string _act_instanceid = string.Empty;
    protected int _act_instancestatus = 0;
    protected string _act_definitionid = string.Empty;
    TabHelper objTab = new TabHelper();
    #endregion

    #region page init & load
    protected override void OnPreInit(EventArgs e)
    {
        base.AuthorizeUserCtx();
        base.OnPreInit(e);
        
        #region get data submit
        _action = Request["act"];
        _documentid = Request["doc"];
        
        if (!string.IsNullOrEmpty(Request["viewtype"]))
            _viewtype = byte.Parse(Request["viewtype"]);

        _sysreq = Request["r"];
        _wf_instanceid = Request["processinstance"];
        _act_instanceid = Request["activityinstance"];
        
        if (!string.IsNullOrEmpty(_act_instanceid))
        {
            _act_definitionid = CommonFunc.getActivityDefinition(_act_instanceid);
            _act_instancestatus = CommonFunc.getActivityInstanceStatus(_act_instanceid);
        }
        if (!string.IsNullOrEmpty(_wf_instanceid))
            _wf_instancestatus = CommonFunc.getProcessInstanceStatus(_wf_instanceid);
        #endregion

        #region action handler
        if (!string.IsNullOrEmpty(_action))
        {
            if (_action == "dotkt")
            {
                BuildTab_DotKiemToan(_documentid);
            }
        }
        #endregion

        #region init form
        
        #endregion

        #region client control event handler
        #endregion

        #region building tabs process
        objTab.createTabs(this.litTab);
        objTab = null;
        #endregion

    }
    /// <summary>
    /// OnInit
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #endregion

    #region page helper processing
    /// <summary>
    /// 
    /// </summary>
    /// <param name="documentid"></param>
    /// <param name="wf_instanceid"></param>
    /// <param name="act_instanceid"></param>
    private void ViewDocumentOnProcess(string documentid, string wf_instanceid, string act_instanceid)
    {
        /*Thong tin chung*/
        string urlHoSo = "../../modules/dms/ChiTietHoSo.aspx?processinstance=" + wf_instanceid + "&processstatus=" + _wf_instancestatus + "&document=" + documentid + "&activityinstance=" + act_instanceid;
        urlHoSo += "&viewtype=" + _viewtype.ToString();
        objTab.defineTab("tabHoSo", "Thông tin hồ sơ", urlHoSo, true);

        /*Khai bao Tai san*/
        //if (_act_definitionid.ToUpper() == ACTIVITY_DEFINITION.KHOITAO.ToUpper() && _act_instancestatus == STATUS.ACTIVE)
        //{
        //    string urlTaiSan = "../../modules/dms/TaiSan.aspx?DocIDHoSo=" + documentid;
        //    urlTaiSan += "&viewtype=" + _viewtype.ToString();
        //    objTab.defineTab("tabTaiSan", "Bổ sung tài sản", urlTaiSan, false);
        //}
        /*Cac tai san da khai bao*/
        string urlTaiSanDaKB = "tab.aspx?act=listTS&document=" + documentid + "&activityinstance=" + act_instanceid + "&activityid=" + _act_definitionid;
        urlTaiSanDaKB += "&viewtype=" + _viewtype.ToString();
        objTab.defineTab("tabTaiSanDaKB", "Thông tin tài sản", urlTaiSanDaKB, false);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="documentid"></param>
    private void BuildTab_DotKiemToan(string DotKiemToan)
    {
        //bus_HoSo obj = bus_HoSo.Instance(_objUserContext);

        /*Thông tin chung*/
        string urlDotKiemToan = "../../modules/dms/DotKiemToan_Load.aspx?doc=" + DotKiemToan + "&tab=1&act=loaddoc";
        objTab.defineTab("tabThongTinChung", "THÔNG TIN ĐỢT KIỂM TOÁN", urlDotKiemToan, true);

        /*Đoàn kiểm toán*/
        string urlDoanKiemToan = "../../modules/dms/DoanKiemToan_Load.aspx?dotkt=" + DotKiemToan;
        objTab.defineTab("tabDoanKiemToan", "ĐOÀN KIỂM TOÁN", urlDoanKiemToan, false);

        /*Các nhóm*/
        string urlNhom = "../../modules/dms/NhomKiemToan_Load.aspx?dotkt=" + DotKiemToan;
        objTab.defineTab("tabNhom", "CÁC NHÓM LÀM VIỆC", urlNhom, false);
    }
    #endregion
}

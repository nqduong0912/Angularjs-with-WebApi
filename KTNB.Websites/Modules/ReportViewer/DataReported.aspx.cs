using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using C1.Web.C1WebGrid;


//using VPB_CRM.Helper.Constant;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.UMS.CoreBusiness;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using VPB_KTNB.Helpers;

public partial class Modules_Report_DataReported : PageBase
{
    #region initiation page variables
    protected string _viewid = string.Empty;
    protected byte _viewtype = 0;
    protected string _documentid = string.Empty;
    protected string _action = string.Empty;
    protected string _month = string.Empty;
    protected string _year = string.Empty;
    protected string _state = string.Empty;
    protected string _rank = string.Empty;
    protected string _sector = string.Empty;
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
        _month = Request["m"];
        _year = Request["y"];
        _state = Request["s"];
        _rank = Request["r"];
        _sector = Request["sec"];
        _action = Request["a"];
        #endregion

        #region action handler
        if (_action == "exportcsv")
        {
            base.FeedBackClient(ExportData2CSV());
        }
        #endregion

        #region init form
        base.InitForm("Báo cáo tổng hợp-chi tiết khách hàng cao cấp", "Pie Diagram.png", _viewid, _viewtype);
        _btnCloseWindow.Visible = true;
        _btnPrint.Visible = true; _btnPrint.Text = "Kết xuất file báo cáo"; _btnPrint.Width = Unit.Pixel(150);
        #endregion

        #region client control event handler
        _btnPrint.Attributes.Add("onclick", "{return GetData2CSV();}");
        #endregion

        getList();

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

    #region page button processing
    /// <summary>
    /// dataCtrl_OnItemDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
    {
        //C1ListItemType elemType = e.Item.ItemType;
        //if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
        //{
        //    Label PK_DOCUMENTID = (Label)e.Item.FindControl("PK_DOCUMENTID") as Label;
        //}
    }
    #endregion

    #region page helper processing
    /// <summary>
    /// 
    /// </summary>
    private void getList()
    {
        ObjectDataSource1.SelectParameters["MM"].DefaultValue = _month;
        ObjectDataSource1.SelectParameters["YYYY"].DefaultValue = _year;
        ObjectDataSource1.SelectParameters["State"].DefaultValue = _state;
        ObjectDataSource1.SelectParameters["Rank"].DefaultValue = _rank;
        ObjectDataSource1.SelectParameters["Sector"].DefaultValue = _sector;
        dataCtrl.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private string ExportData2CSV()
    {
        SqlConnection objcon = new SqlConnection(_objUserContext.ConnectionString);
        objcon.Open();

        if (objcon.State != ConnectionState.Open) return "-1";

        SqlCommand objcmd = new SqlCommand("dbo.RPT_CUS_GEN_TERM_STATE", objcon);
        objcmd.CommandType = CommandType.StoredProcedure;

        SqlParameter par;

        par = new SqlParameter("MONTH", _month);
        par.Direction = ParameterDirection.Input;
        objcmd.Parameters.Add(par);

        par = new SqlParameter("YEAR", _year);
        par.Direction = ParameterDirection.Input;
        objcmd.Parameters.Add(par);

        if (_state != "ALL")
            par = new SqlParameter("STATE", _state);
        else
            par = new SqlParameter("STATE", DBNull.Value);
        par.Direction = ParameterDirection.Input;
        objcmd.Parameters.Add(par);

        if (_rank != "ALL")
            par = new SqlParameter("RANK", _rank);
        else
            par = new SqlParameter("RANK", DBNull.Value);
        par.Direction = ParameterDirection.Input;
        objcmd.Parameters.Add(par);

        if (_sector != "ALL")
            par = new SqlParameter("SECTOR", _sector);
        else
            par = new SqlParameter("SECTOR", DBNull.Value);
        par.Direction = ParameterDirection.Input;
        objcmd.Parameters.Add(par);

        SqlDataAdapter ad = new SqlDataAdapter(objcmd);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        objcon.Close();

        string filename = _objUserContext.UserName + "_KHCC" + _year + _month + ".csv";
        string csv_file = Server.MapPath("~/Modules/Download" + "\\" + filename);
        if (DataSet2CSV(ds, csv_file, "#") == 0)
        {
            return filename;
        }
        return "-1";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="filename"></param>
    /// <param name="sepchar"></param>
    /// <returns></returns>
    private int DataSet2CSV(DataSet ds, string filename, string sepchar)
    {
        StreamWriter writer = new StreamWriter(filename);
        try
        {
            string sep = "";
            StringBuilder builder = new StringBuilder();

            //Excel row header
            foreach (DataColumn col in ds.Tables[0].Columns)
            {
                builder.Append(sep).Append(col.ColumnName);
                sep = sepchar;
            }
            writer.WriteLine(builder.ToString());

            //Excel row data
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    sep = "";
                    builder = new StringBuilder();
                    foreach (DataColumn col in table.Columns)
                    {
                        builder.Append(sep).Append(row[col.ColumnName]);
                        sep = sepchar;
                    }
                    writer.WriteLine(builder.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            return -1;
        }
        finally
        {
            if (writer != null)
                writer.Close();
        }
        return 0;
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.CFG
{
    public partial class Modules_CFG_cachesystem : PageBase
    {

        #region initiation page variables

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

            base.InitForm("system cache", "userman.png", string.Empty, 0);

            btnResetCache.Attributes.Add("onclick", "{return resetcache();}");
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            loadCacheInfo();
        }
        #endregion

        #region page helper processing
        protected void DeleSysCache(object sender, EventArgs e)
        {
            bus_CacheManager objcache = bus_CacheManager.Instance(_objUserContext);
            IDataReader reader = objcache.getlistReader(string.Empty, "PK_CacheID");
            while (reader.Read())
            {
                string cacheid = reader["PK_CacheID"].ToString();
            }
            objcache.delete(string.Empty);
            loadCacheInfo();
        }
        protected void loadCacheInfo()
        {
            string SQL = "SELECT PK_CACHEID,EXPIREMINUTES, CONVERT(VARCHAR(10),CREATEDDATETIME,103) + ' ' + CONVERT(VARCHAR(10),CREATEDDATETIME,108) CREATEDDATETIME FROM T_CACHE_MANAGER WHERE 1=1 ORDER BY CREATEDDATETIME DESC";
            SqlDataSource1.SelectCommand = SQL;
            SqlDataSource1.ConnectionString = _objUserContext.ConnectionString;
            dataCtrl.DataBind();
        }
        #endregion

        #region page button processing
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label lblOperationType = (Label)e.Item.FindControl("lblOperationType") as Label;
            }
        }

        #endregion








    }
}
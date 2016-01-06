using System.Text;
using System.Web.UI.WebControls;

namespace VPB_KTNB.Helpers
{
    /// <summary>
    /// Summary description for TabHelper
    /// </summary>
    public class TabHelper
    {
        #region initiation class variables
        string _beginMainTab = string.Empty;
        string _endMainTab = string.Empty;
        StringBuilder _scriptTabs;
        StringBuilder _initJs;
        #endregion

        #region class constructor

        public TabHelper()
        {
            _beginMainTab = "<div class='tabber' id='MainTab'>";
            _endMainTab = "</div>";

            _scriptTabs = new StringBuilder();
            _scriptTabs.Append(_beginMainTab);

            _initJs = new StringBuilder();
            _initJs.Append("<script type='text/javascript'>");
            _initJs.Append("tabberAutomatic(tabberOptions);");
            _initJs.Append("</script>");
        }

        #endregion

        #region class properties

        #endregion

        #region class interface methods

        public void defineTab(string tabID, string tabTitle, string url, bool tabDefault)
        {
            StringBuilder aTab = new StringBuilder();
            if (tabDefault)
                aTab.Append("<div id='tab" + tabID + "' class='tabbertab tabbertabdefault' title='" + tabTitle + "'>");
            else
                aTab.Append("<div id='tab" + tabID + "' class='tabbertab' title='" + tabTitle + "'>");

            aTab.Append("<iframe id='ifr" + tabID + "' src='" + url + "' width='100%' height='5000px' frameborder='0'></iframe>");
            aTab.Append("</div>");
            _scriptTabs.Append(aTab.ToString());
        }

        public void createTabs(Literal lit)
        {
            _scriptTabs.Append(_endMainTab);
            _scriptTabs.Append(_initJs.ToString());
            lit.Text = _scriptTabs.ToString();
        }

        #endregion

        #region class helper method

        #endregion

    }
}

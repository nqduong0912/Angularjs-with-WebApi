using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.CFG
{
    public partial class Modules_CFG_backupdb : PageBase
    {
        #region initiation page variables
        string _filepath=string.Empty;
        string _filename = string.Empty;
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

            #endregion

            #region action handler

            #endregion

            base.InitForm("Sao lưu dữ liệu", "dbbackup.png", string.Empty, 0);

            #region client control event handler
            this.btnBackUp.Text = "Sao lưu";
            this.btnBackUp.Width = Unit.Pixel(80);
            this.btnBackUp.Attributes.Add("onclick", "{return backup();}");
            #endregion

            _filename = PrepareFileBackup();
            _filepath = ParameterHelper.GetValue("DB_BACKUP_PATH", _objUserContext);
            _filename = _filepath + "\\" + _filename;

        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtFile.Text = _filename;
        }
        #endregion

        #region page helper processing
        protected string PrepareFileBackup()
        {
            string filename = ParameterHelper.GetValue("DB_BACKUP_FILE", _objUserContext);
            filename = ParameterHelper.ResolveNamingPattern(filename);
            return (filename.ToLower());
        }

        private void BackupDB(string filename)
        {
            //if (_objUserContext.BackupDB(_dbName, filename) == 0)
            //{
            //    this.lblStatus.Text = "Tiến trình sao lưu hoàn tất thành công.";
            //}
            //else
            //{
            //    this.lblStatus.Text = "Tiến trình sao lưu có lỗi. Sao lưu không thành công";
            //}

        }
        #endregion

        #region page button processing
        protected void BackUp(object sender, EventArgs e)
        {
            _filepath = ParameterHelper.GetValue("DB_BACKUP_PATH", _objUserContext);
            string filename = PrepareFileBackup();
            filename = _filepath + "\\" + filename;
            BackupDB(filename);
        }
        #endregion
    }
}
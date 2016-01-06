using System;
using System.Collections.Generic;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;

namespace VPB_KTNB.Helpers
{
    public class ReportHelper
    {

        #region Local Variables

        private string _datebaseName;
        private string _serverName;
        private string _userId;
        private string _password;
        private bool _integratedSecurity;

        private CrystalReportViewer _reportViewer;
        private ReportDocument _reportDocument;
        private DataSet _reportData;
        private string _reportFile;
        private bool _reportIsOpen;
        private SortedList<string, object> _parameters;

        #endregion

        #region Private methods

        public ReportHelper()
        {
            _parameters = new SortedList<string, object>();
        }

        private static void AssignTableConnection(CrystalDecisions.CrystalReports.Engine.Table table, ConnectionInfo connection)
        {
            // Cache the logon info block
            TableLogOnInfo logOnInfo = table.LogOnInfo;

            // Set the connection
            logOnInfo.ConnectionInfo = connection;

            // Apply the connection to the table!
            table.ApplyLogOnInfo(logOnInfo);
        }

        /// <summary>
        /// Assign the database connection to the table in all the report sections.
        /// </summary>
        private void AssignConnection()
        {
            ConnectionInfo connection = new ConnectionInfo();

            connection.DatabaseName = _datebaseName;
            connection.ServerName = _serverName;
            if (_integratedSecurity)
            {
                connection.IntegratedSecurity = _integratedSecurity;
            }
            else
            {
                connection.UserID = _userId;
                connection.Password = _password;
            }

            // First we assign the connection to all tables in the main report
            //
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in _reportDocument.Database.Tables)
            {
                AssignTableConnection(table, connection);
            }

            // Now loop through all the sections and its objects to do the same for the subreports
            //
            foreach (CrystalDecisions.CrystalReports.Engine.Section section in _reportDocument.ReportDefinition.Sections)
            {
                // In each section we need to loop through all the reporting objects
                foreach (CrystalDecisions.CrystalReports.Engine.ReportObject reportObject in section.ReportObjects)
                {
                    if (reportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        SubreportObject subReport = (SubreportObject)reportObject;
                        ReportDocument subDocument = subReport.OpenSubreport(subReport.SubreportName);

                        foreach (CrystalDecisions.CrystalReports.Engine.Table table in subDocument.Database.Tables)
                        {
                            AssignTableConnection(table, connection);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Assign the DataSet to the report.
        /// </summary>
        private void AssignDataSet()
        {
            DataSet reportData = _reportData.Copy();

            // Remove primary key info. CR9 does not appreciate this information!!!
            foreach (DataTable dataTable in reportData.Tables)
            {
                foreach (DataColumn dataColumn in dataTable.PrimaryKey)
                {
                    dataColumn.AutoIncrement = false;
                }
                dataTable.PrimaryKey = null;
            }

            // Now assign the dataset to all tables in the main report
            //
            _reportDocument.SetDataSource(reportData);

            // Now loop through all the sections and its objects to do the same for the subreports
            //
            foreach (CrystalDecisions.CrystalReports.Engine.Section section in _reportDocument.ReportDefinition.Sections)
            {
                // In each section we need to loop through all the reporting objects
                foreach (CrystalDecisions.CrystalReports.Engine.ReportObject reportObject in section.ReportObjects)
                {
                    if (reportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        SubreportObject subReport = (SubreportObject)reportObject;
                        ReportDocument subDocument = subReport.OpenSubreport(subReport.SubreportName);

                        subDocument.SetDataSource(reportData);
                    }
                }
            }
        }

        /// <summary>
        /// Create a ReportDocument object using a report file and store
        /// the name of the file.
        /// </summary>
        /// <param name="reportFile">Name of the CrystalReports file (*.rpt).</param>
        /// <returns>A valid ReportDocument object.</returns>
        private ReportDocument CreateReportDocument(string reportFile)
        {
            ReportDocument newDocument = new ReportDocument();

            _reportFile = reportFile;
            newDocument.Load(reportFile);

            return newDocument;
        }

        /// <summary>
        /// Sets the parameters that have been added using the SetParameter method
        /// </summary>
        private void SetParameters()
        {
            foreach (ParameterFieldDefinition parameter in _reportDocument.DataDefinition.ParameterFields)
            {
                try
                {
                    // Now get the current value for the parameter
                    CrystalDecisions.Shared.ParameterValues currentValues = parameter.CurrentValues;
                    currentValues.Clear();

                    // Create a value object for Crystal reports and assign the specified value.
                    CrystalDecisions.Shared.ParameterDiscreteValue newValue = new CrystalDecisions.Shared.ParameterDiscreteValue();

                    if (_parameters.ContainsKey(parameter.Name))
                    {
                        newValue.Value = _parameters[parameter.Name];
                    }

                    // Now add the new value to the values collection and apply the 
                    // collection to the report.
                    currentValues.Add(newValue);
                    parameter.ApplyCurrentValues(currentValues);
                }
                catch
                {
                    // Ignore any errors
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ReportHelper"/> class.
        /// </summary>


        /// <summary>
        /// Initializes a new instance of the <see cref="T:ReportHelper"/> class.
        /// </summary>
        /// <param name="report">The <see cref="T:ReportDocument"/> object for an embedded report.</param>
        public ReportHelper(ReportDocument report)
            : this()
        {
            _reportDocument = report;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        public ReportHelper(ReportDocument report, CrystalReportViewer viewer)
            : this()
        {
            _reportDocument = report;
            _reportViewer = viewer;

            // Setup the connection information 
            _serverName = "SERVER";
            _datebaseName = "DATABASE";
            _userId = "USER";
            _password = "PASSWORD";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ReportHelper"/> class.
        /// </summary>
        /// <param name="reportFile">Name and path for a CrystalReports (*.rpt) file.</param>
        public ReportHelper(string reportFile)
            : this()
        {
            _reportDocument = CreateReportDocument(reportFile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ReportHelper"/> class.
        /// </summary>
        /// <param name="report">The <see cref="T:ReportDocument"/> object for an embedded report.</param>
        /// <param name="serverName">Name of the database server.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="userId">The user id required for logon.</param>
        /// <param name="userPassword">The password for the specified user.</param>
        public ReportHelper(ReportDocument report, string serverName, string databaseName, string userId, string userPassword)
            : this()
        {
            _reportDocument = report;

            // Setup the connection information 
            _serverName = serverName;
            _datebaseName = databaseName;
            _userId = userId;
            _password = userPassword;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ReportHelper"/> class.
        /// </summary>
        /// <param name="report">The <see cref="T:ReportDocument"/> object for an embedded report.</param>
        /// <param name="serverName">Name of the database server.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="integratedSecurity">if set to <c>true</c> integrated security is used to connect to the database; false if otherwise.</param>
        public ReportHelper(ReportDocument report, string serverName, string databaseName, bool integratedSecurity)
            : this()
        {
            _reportDocument = report;

            // Setup the connection information 
            _serverName = serverName;
            _datebaseName = databaseName;
            _integratedSecurity = integratedSecurity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ReportHelper"/> class.
        /// </summary>
        /// <param name="reportFile">Name and path for a CrystalReports (*.rpt) file.</param>
        /// <param name="serverName">Name of the database server.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="userId">The user id required for logon.</param>
        /// <param name="userPassword">The password for the specified user.</param>
        public ReportHelper(string reportFile, string serverName, string databaseName, string userId, string userPassword)
            : this()
        {
            _reportDocument = CreateReportDocument(reportFile);

            // Setup the connection information 
            _serverName = serverName;
            _datebaseName = databaseName;
            _userId = userId;
            _password = userPassword;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ReportHelper"/> class.
        /// </summary>
        /// <param name="reportFile">Name and path for a CrystalReports (*.rpt) file.</param>
        /// <param name="serverName">Name of the database server.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="integratedSecurity">if set to <c>true</c> integrated security is used to connect to the database; false if otherwise.</param>
        public ReportHelper(string reportFile, string serverName, string databaseName, bool integratedSecurity)
            : this()
        {
            _reportDocument = CreateReportDocument(reportFile);

            // Setup the connection information 
            _serverName = serverName;
            _datebaseName = databaseName;
            _integratedSecurity = integratedSecurity;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the report source.
        /// </summary>
        /// <value>The report source.</value>
        /// <remarks>
        /// Use this property when you want to show the report in the 
        /// CrystalReportsViewer control.</remarks>
        public ReportDocument ReportSource
        {
            get
            {
                return _reportDocument;
            }
            set
            {
                if (_reportDocument != null)
                {
                    _reportDocument.Dispose();
                }
                _reportDocument = value;
                if (value == null)
                {
                    _reportFile = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the report file.
        /// </summary>
        /// <value>The report file.</value>
        public string ReportFile
        {
            get
            {
                return _reportFile;
            }
            set
            {
                if (_reportDocument != null)
                {
                    _reportDocument.Dispose();
                }
                if (value != null && value.Trim().Length > 0)
                {
                    _reportDocument = CreateReportDocument(value);
                }
                else
                {
                    _reportFile = value;
                    _reportDocument = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public DataSet DataSource
        {
            get
            {
                return _reportData;
            }
            set
            {
                if (_reportData != null)
                {
                    _reportData.Dispose();
                }
                _reportData = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        public string ServerName
        {
            get
            {
                return _serverName;
            }
            set
            {
                _serverName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>The name of the database.</value>
        public string DatabaseName
        {
            get
            {
                return _datebaseName;
            }
            set
            {
                _datebaseName = value;
            }
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [integrated security].
        /// </summary>
        /// <value><c>true</c> if integrated security is used to connect to the database; false if otherwise.</value>
        public bool IntegratedSecurity
        {
            get
            {
                return _integratedSecurity;
            }
            set
            {
                _integratedSecurity = value;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Set value for report parameters
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Value to be set for the specified parameter.</param>
        public void SetParameter(string name, object value)
        {
            if (_parameters.ContainsKey(name))
            {
                _parameters[name] = value;
            }
            else
            {
                _parameters.Add(name, value);
            }
        }

        /// <summary>
        /// Close the report.
        /// </summary>
        /// <remarks>
        /// This method will throw an exception when the Open() method is not yet called on this object.
        /// </remarks>
        public void Close()
        {
            if (!_reportIsOpen) throw new InvalidOperationException("The report is already closed.");

            _reportDocument.Close();
            _reportIsOpen = false;
        }

        /// <summary>
        /// Open the report.
        /// </summary>
        /// <remarks>
        /// <p>This method will first attempt to assign the DataSource if that was specified. When no DataSource has been
        /// assigned, a check will be made to see if database connection information has been specified. When this is the case
        /// this information will be assigned to the report.
        /// </p>
        /// <P>This method will throw an exception when:
        /// <list type="bullet">
        /// <item><description>The report (rpt) has not been assigned yet.</description></item>
        /// <item><description>The Open() method has already been called on this object.</description></item>
        /// <item><description>A table being used in the report does not exist in the dataset which has been assigned to this report. (only when a dataset has been assigned to the DataSource property)</description></item>
        /// <item><description>The ServerName, DatabaseName or UserId property has not been set. (only when the DataSource property has not been set)</description></item>
        /// <item><description>When no database connection or datset could be assignd.</description></item>
        /// </list>
        /// </P></remarks>
        public void Open()
        {
            SetParameters();

            // Check if the connection object exists. If so assign that
            // to the report.
            if (_reportData != null)
            {
                // Assign the dataset to the report.
                AssignDataSet();
            }
            else
            {
                if (_serverName.Length == 0 || (!_integratedSecurity && _userId.Length == 0) || _datebaseName.Length == 0)
                {
                    throw new Exception("Connection information is incomplete. Report could not be opened.");
                }
                AssignConnection();
            }
            _reportIsOpen = true;
        }

        /// <summary>
        /// Force a refresh of the data in the report. 
        /// </summary>
        /// <remarks>
        /// When the report is based on a DataSource, the report will be refreshed using 
        /// data in that DataSource. In case the report has it's own database connection 
        /// and uses SQL queries, the report will be refreshed using that information. 
        /// <br/>
        /// <p>
        /// This method will throw an exception when the Open() method is not yet called on this object.
        /// </p>
        /// </remarks>
        public void Refresh()
        {
            if (!_reportIsOpen) throw new InvalidOperationException("The report is not open.");

            _reportDocument.Refresh();
        }

        /// <summary>
        /// Print the report to the specified printer. 
        /// </summary>
        /// <param name="printerName">Name of the printer to print the information to.</param>
        /// <param name="nrCopies">The number of copies required.</param>
        /// <param name="collatePages">Indicates whether to collate the pages.</param>
        /// <param name="firstPage">First page to be printed. When less than 1, printing starts at the first page.</param>
        /// <param name="lastPage">Last page to be printed. When less than 1, or more than 9999, the last page will be printed as last page.</param>
        /// <remarks>
        /// This method will throw an exception when the Open() method is not yet called on this object.
        /// </remarks>
        public void Print(string printerName, int nrCopies, bool collatePages, int firstPage, int lastPage)
        {
            bool openedHere = false;

            if (!_reportIsOpen)
            {
                this.Open();
                openedHere = true;
            }

            _reportDocument.PrintOptions.PrinterName = printerName;

            if (firstPage < 1) firstPage = 0;
            if ((lastPage < 1) || (lastPage > 9999)) lastPage = 0;

            _reportDocument.PrintToPrinter(nrCopies, collatePages, firstPage, lastPage);
            if (openedHere)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Prints one copy of the entire report to the default printer.
        /// </summary>
        /// <remarks>
        /// This method will throw an exception when the Open() method is not yet called on this object.
        /// </remarks>
        public void Print()
        {
            this.Print(string.Empty, 1, false, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Show()
        {
            _reportViewer.ReportSource = _reportDocument;
            _reportViewer.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="format"></param>
        /// <param name="rptDoc"></param>
        /// <returns></returns>
        public void SaveRpt(string file, ExportFormatType format, ReportDocument rptDoc)
        {
            ExportOptions exportOpts = new ExportOptions();
            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();

            exportOpts.ExportFormatType = format;
            exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;

            diskOpts.DiskFileName = file;
            exportOpts.ExportDestinationOptions = diskOpts;
            
            try
            {
                rptDoc.Export(exportOpts);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose of this object's resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true); // as a service to those who might inherit from us
        }

        /// <summary>
        ///	Free the instance variables of this object.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> [disposing].</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_reportIsOpen)
                {
                    _reportDocument.Close();
                }
                if (_reportDocument != null)
                {
                    _reportDocument.Dispose();
                    _reportDocument = null;
                }

                if (_reportData != null)
                {
                    _reportData.Dispose();
                    _reportData = null;
                }
            }
        }

        #endregion

        #region Doiso_Thanh chu

        private string ReadGroup3(string G3)
        {
            string[] ReadDigit = new string[10] { " Không", " Một", " Hai", " Ba", " Bốn", " Năm", " Sáu", " Bảy", " Tám", " Chín" };
            string temp = "";
            if (G3 == "000") return "";

            //Đọc số hàng trăm
            temp = ReadDigit[int.Parse(G3[0].ToString())] + " Trăm";
            //Đọc số hàng chục
            if (G3[1].ToString() == "0")
                if (G3[2].ToString() == "0") return temp;
                else
                {
                    temp += " Lẻ" + ReadDigit[int.Parse(G3[2].ToString())];
                    return temp;
                }
            else
                temp += ReadDigit[int.Parse(G3[1].ToString())] + " Mươi";
            //--------------Đọc hàng đơn vị

            if (G3[2].ToString() == "5") temp += " Lăm";
            else if (G3[2].ToString() != "0") temp += ReadDigit[int.Parse(G3[2].ToString())];
            return temp;


        }
        private string ReadMoney(string Money)
        {
            string temp = "";
            // Cho đủ 12 số
            while (Money.Length < 12)
            {
                Money = "0" + Money;
            }
            string g1 = Money.Substring(0, 3);
            string g2 = Money.Substring(3, 3);
            string g3 = Money.Substring(6, 3);
            string g4 = Money.Substring(9, 3);

            //Đọc nhóm 1 ---------------------
            if (g1 != "000")
            {
                temp = ReadGroup3(g1);
                temp += " Tỷ";
            }
            //Đọc nhóm 2-----------------------
            if (g2 != "000")
            {
                temp += ReadGroup3(g2);
                temp += " Triệu";
            }
            //---------------------------------
            if (g3 != "000")
            {
                temp += ReadGroup3(g3);
                temp += " Ngàn";
            }

            temp = temp + ReadGroup3(g4);
            //---------------------------------
            // Tinh chỉnh
            temp = temp.Replace("Một Mươi", "Mười");
            temp = temp.Trim();
            if (temp.IndexOf("Không Trăm") == 0)
                temp = temp.Remove(0, 10);
            temp = temp.Trim();
            if (temp.IndexOf("Lẻ") == 0)
                temp = temp.Remove(0, 2);
            temp = temp.Trim();
            temp = temp.Replace("Mươi Một", "Mươi Mốt");
            temp = temp.Trim();
            //Change Case
            return temp.Substring(0, 1).ToUpper() + temp.Substring(1).ToLower();

        }
        //Đây là hàm đọc số thành chữ\
        //Luu ý Đây à hàm mới đọc đến chín trăm chính mươi chín tỷ đồng

        public string Get_Stringchu(string number)
        {
            string v_string = null;
            try
            {
                v_string = ReadMoney(number) + " đồng";
            }
            catch { }
            return v_string;
        }
        #endregion
    }
}

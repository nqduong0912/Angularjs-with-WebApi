using System;
using System.Data;
using System.Data.SqlClient;

namespace VPB_KTNB.Helpers
{
    /// <summary>
    /// Summary description for CustomerMediator
    /// </summary>
    [Serializable]
    public class CustomerMediator
    {
        #region fiedls
        private string _connectionstring = string.Empty;

        private string _ID = string.Empty;
        private string _name = string.Empty;
        private string _shortname = string.Empty;
        private string _sector = string.Empty;
        private string _industry = string.Empty;
        private string _address = string.Empty;
        private string _certid = string.Empty;
        private string _certdated = string.Empty;
        private string _certplaced = string.Empty;
        private string _certexpirydate = string.Empty;
        private string _vatid = string.Empty;
        private string _tel = string.Empty;
        private string _email = string.Empty;
        private string _residence = string.Empty;
        private string _subbranchid = string.Empty;
        private string _mainofficeid = string.Empty;
        private string _mainofficeaddress = string.Empty;
        private string _phone = string.Empty;
        private string _fax = string.Empty;
        private string _website = string.Empty;
        private string _decisionnumber = string.Empty;
        private string _decisiondated = string.Empty;
        private string _decisionplace = string.Empty;
        private string _monitorofficename = string.Empty;
        private string _businesslicence = string.Empty;
        private string _businessdated = string.Empty;
        private string _businessindustries = string.Empty;
        private string _generaldirectorname = string.Empty;
        private string _totaloflabor = string.Empty;
        private string _legalcapitalVND = string.Empty;
        private string _legalcapitalUSD = string.Empty;
        private string _dateofbirth = string.Empty;
        private string _wifeorhusbandname = string.Empty;
        private string _wifeorhusbandcertid = string.Empty;
        private string _wifeorhusbandcertdated = string.Empty;
        private string _countrycode = string.Empty;
        private string _provicecode = string.Empty;
        private string _status = string.Empty;
        private string _opendate = string.Empty;
        #endregion

        #region constructors
        /// <summary>
        /// CustomerMediator
        /// </summary>
        public CustomerMediator()
        {
            _connectionstring = System.Configuration.ConfigurationManager.AppSettings["VPB_T24DATA"].ToString();
        }
        /// <summary>
        /// CustomerMediator by customer id
        /// </summary>
        /// <param name="CustomerID"></param>
        public CustomerMediator(string CustomerID)
            : this()
        {
            SqlDataReader reader = GetCustomerInfo(CustomerID);
            //if(reader.HasRows)
            while (reader.Read())
            {
                _ID = CustomerID;
                _name = reader["NAME"].ToString();
                _shortname = reader["SHORTNAME"].ToString();
                _sector = reader["SECTOR_ID"].ToString();
                _industry = reader["INDUSTRY_ID"].ToString();
                _address = reader["ADDRESS"].ToString();
                _certid = reader["CERT_ID"].ToString();
                _certdated = reader["CERT_DATED"].ToString();
                _certplaced = reader["CERT_PLACE"].ToString();
                _certexpirydate = reader["CERT_EXPIRYDATE"].ToString();
                _vatid = reader["VAT_ID"].ToString();
                _tel = reader["TEL"].ToString();
                _email = reader["EMAIL"].ToString();
                _residence = reader["RESIDENCE"].ToString();
                _subbranchid = reader["SUBBRANCH_ID"].ToString();
                _mainofficeid = reader["MAINOFFICE_ID"].ToString();
                _mainofficeaddress = reader["MAINOFFICEADDRESS"].ToString();
                _phone = reader["PHONE"].ToString();
                _fax = reader["FAX"].ToString();
                _website = reader["WEBSITE"].ToString();
                _decisionnumber = reader["DECISIONNUMBER"].ToString();
                _decisiondated = reader["DECISIONDATED"].ToString();
                _decisionplace = reader["DECISIONPLACE"].ToString();
                _monitorofficename = reader["MONITOROFFICENAME"].ToString();
                _businesslicence = reader["BUSINESSLICENCE"].ToString();
                _businessdated = reader["BUSINESSDATED"].ToString();
                _businessindustries = reader["BUSINESSINDUSTRIES"].ToString();
                _generaldirectorname = reader["GERERALDIRECTORNAME"].ToString();
                _totaloflabor = reader["TOTALOFLABOR"].ToString();
                _legalcapitalVND = reader["LEGALCAPITAL_VND"].ToString();
                _legalcapitalUSD = reader["LEGALCAPITAL_USD"].ToString();
                _dateofbirth = reader["DATEOFBIRTH"].ToString();
                _wifeorhusbandname = reader["WIFEORHUSBANDNAME"].ToString();
                _wifeorhusbandcertid = reader["WIFEORHUSBANDCERT_ID"].ToString();
                _wifeorhusbandcertdated = reader["WIFEORHUSBANDCERT_DATED"].ToString();
                _countrycode = reader["COUNTRYCODE"].ToString();
                _provicecode = reader["PROVINCECODE"].ToString();
                _status = reader["STATUS"].ToString();
                _opendate = reader["OPEN_DATE"].ToString();
            }
        }
        #endregion

        #region properties
        /// <summary>
        /// get Ma khach hang (CUSTOMERID)
        /// </summary>
        public string ID
        {
            get { return _ID; }
        }
        /// <summary>
        /// get or set Ten day du cua khach hang
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// get or set Ten viet tat cua khach hang
        /// </summary>
        public string ShortName
        {
            set { _shortname = value; }
            get { return _shortname; }
        }
        /// <summary>
        /// get or set SectorID cua khach hang
        /// </summary>
        public string Sector
        {
            set { _sector = value; }
            get { return _sector; }
        }
        /// <summary>
        /// get or set Industry cua khach hang
        /// </summary>
        public string Industry
        {
            set { _industry = value; }
            get { return _industry; }
        }
        /// <summary>
        /// get or set Dia chi cua khach hang
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// get or set So CMTND cua khach hang
        /// </summary>
        public string CertID
        {
            set { _certid = value; }
            get { return _certid; }
        }
        /// <summary>
        /// get or set Ngay cap CMTND cua khach hang
        /// </summary>
        public string CertDated
        {
            set { _certdated = value; }
            get { return _certdated; }
        }
        /// <summary>
        /// get or set Noi cap CMTND cua khach hang
        /// </summary>
        public string CertPlace
        {
            set { _certplaced = value; }
            get { return _certplaced; }
        }
        /// <summary>
        /// get or set Ngay het han CMTND cua khach hang
        /// </summary>
        public string CertExpiryDate
        {
            set { _certexpirydate = value; }
            get { return _certexpirydate; }
        }
        /// <summary>
        /// get or set Ma so thue cua khach hang
        /// </summary>
        public string VATID
        {
            set { _vatid = value; }
            get { return _vatid; }
        }
        /// <summary>
        /// get or set So dien thoai cua khach hang
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// get or set Email cua khach hang
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// get or set Residence cua khach hang
        /// </summary>
        public string Residence
        {
            set { _residence = value; }
            get { return _residence; }
        }
        /// <summary>
        /// get or set Ma chi nhanh quan ly khach hang
        /// </summary>
        public string SubBranchID
        {
            set { _subbranchid = value; }
            get { return _subbranchid; }
        }
        /// <summary>
        /// get or set Ten co quan lam viec cua khach hang
        /// </summary>
        public string MainOfficeID
        {
            set { _mainofficeid = value; }
            get { return _mainofficeid; }
        }
        /// <summary>
        /// get or set Dia chi co quan lam viec cua khach hang
        /// </summary>
        public string MainOfficeAddress
        {
            set { _mainofficeaddress = value; }
            get { return _mainofficeaddress; }
        }
        /// <summary>
        /// get or set So dien thoai co quan cua khach hang
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// get or set So fax co quan cua khach hang
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// get or set Dia chi website co quan
        /// </summary>
        public string Website
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// get or set So quyet dinh thanh lap to chuc
        /// </summary>
        public string DecisionNumber
        {
            set { _decisionnumber = value; }
            get { return _decisionnumber; }
        }
        /// <summary>
        /// get or set Ngay nhan quyet dinh thanh lap to chuc
        /// </summary>
        public string DecisionDated
        {
            set { _decisiondated = value; }
            get { return _decisiondated; }
        }
        /// <summary>
        /// get or set Noi cap quyet dinh thanh lap
        /// </summary>
        public string DecisionPlace
        {
            set { _decisionplace = value; }
            get { return _decisionplace; }
        }
        /// <summary>
        /// get or set Co quan quan ly
        /// </summary>
        public string MonitorOfficeName
        {
            set { _monitorofficename = value; }
            get { return _monitorofficename; }
        }
        /// <summary>
        /// get or set So dang ky kinh doanh
        /// </summary>
        public string BusinessLicense
        {
            set { _businesslicence = value; }
            get { return _businesslicence; }
        }
        /// <summary>
        /// get or set Ngay cap dang ky kinh doanh
        /// </summary>
        public string BusinessDated
        {
            set { _businessdated = value; }
            get { return _businessdated; }
        }
        public string BusinessIndustries
        {
            set { _businessindustries = value; }
            get { return _businessindustries; }
        }
        public string GeneralDirectorName
        {
            set { _generaldirectorname = value; }
            get { return _generaldirectorname; }
        }
        public string TotalOfLabor
        {
            set { _totaloflabor = value; }
            get { return _totaloflabor; }
        }
        public string LegalCapitalVND
        {
            set { _legalcapitalVND = value; }
            get { return _legalcapitalVND; }
        }
        public string LegalCapitalUSD
        {
            set { _legalcapitalUSD = value; }
            get { return _legalcapitalUSD; }
        }
        public string DateOfBirth
        {
            set { _dateofbirth = value; }
            get { return _dateofbirth; }
        }
        public string WifeOrHusbandName
        {
            set { _wifeorhusbandname = value; }
            get { return _wifeorhusbandname; }
        }
        public string WifeOrHusbandNameCertID
        {
            set { _wifeorhusbandcertid = value; }
            get { return _wifeorhusbandcertid; }
        }
        public string WifeOrHusbandNameCertDated
        {
            set { _wifeorhusbandcertdated = value; }
            get { return _wifeorhusbandcertdated; }
        }
        public string CountryCode
        {
            set { _countrycode = value; }
            get { return _countrycode; }
        }
        public string ProviceCode
        {
            set { _provicecode = value; }
            get { return _provicecode; }
        }
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        public string OpenDate
        {
            set { _opendate = value; }
            get { return _opendate; }
        }
        #endregion

        #region interface
        #endregion

        #region helpers
        private SqlDataReader GetCustomerInfo(string CustomerID)
        {
            string Query = "ID";
            Query += ",NAME";
            Query += ",SHORTNAME";
            Query += ",SECTOR_ID";
            Query += ",INDUSTRY_ID";
            Query += ",ADDRESS";
            Query += ",CERT_ID";
            Query += ",CERT_DATED";
            Query += ",CERT_PLACE";
            Query += ",CERT_EXPIRYDATE";
            Query += ",VAT_ID";
            Query += ",TEL";
            Query += ",EMAIL";
            Query += ",RESIDENCE";
            Query += ",SUBBRANCH_ID";
            Query += ",MAINOFFICE_ID";
            Query += ",MAINOFFICEADDRESS";
            Query += ",PHONE";
            Query += ",FAX";
            Query += ",WEBSITE";
            Query += ",DECISIONNUMBER";
            Query += ",DECISIONDATED";
            Query += ",DECISIONPLACE";
            Query += ",MONITOROFFICENAME";
            Query += ",BUSINESSLICENCE";
            Query += ",BUSINESSDATED";
            Query += ",BUSINESSINDUSTRIES";
            Query += ",GERERALDIRECTORNAME";
            Query += ",TOTALOFLABOR";
            Query += ",LEGALCAPITAL_VND";
            Query += ",LEGALCAPITAL_USD";
            Query += ",DATEOFBIRTH";
            Query += ",WIFEORHUSBANDNAME";
            Query += ",WIFEORHUSBANDCERT_ID";
            Query += ",WIFEORHUSBANDCERT_DATED";
            Query += ",COUNTRYCODE";
            Query += ",PROVINCECODE";
            Query += ",STATUS";
            Query += ",OPEN_DATE";
            Query += ",CONVERT(CHAR(10),CREATEDDATETIME,103) AS CREATEDDATETIME";

            string SQL = "SELECT " + Query + " FROM FBNK_CUSTOMER WHERE ID='" + CustomerID + "'";

            SqlConnection con = new SqlConnection(_connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand(SQL, con);
            cmd.CommandType = CommandType.Text;
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion
    }

    [Serializable]
    public class CustomerBalanceMediator
    {
        #region fields
        private string _connectionstring = string.Empty;
        #endregion

        #region constructors
        public CustomerBalanceMediator()
        {
            _connectionstring = System.Configuration.ConfigurationManager.AppSettings["TFRLIVE"].ToString();
        }
        #endregion

        #region interface
        /// <summary>
        /// Get customer balance by condition
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public SqlDataReader GetBalance(string Condition)
        {
            string SQL = "SELECT * FROM CUST_SDBQ WHERE 1=1";
            if (!string.IsNullOrEmpty(Condition))
                SQL += Condition;
            SQL += " ORDER BY YEAR,MONTH";
            SqlConnection objcon = new SqlConnection(_connectionstring);

            try
            {
                objcon.Open();

                if (objcon.State != ConnectionState.Open)
                    return null;

                SqlCommand cmd = new SqlCommand(SQL, objcon);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                if (objcon.State != ConnectionState.Open) objcon.Close();
                FormHelper.FormWarning("Lỗi kết nối TFRLIVE", ex.Message, "white");
            }
            return null;
        }

        #endregion

        #region helpers

        #endregion
    }
}


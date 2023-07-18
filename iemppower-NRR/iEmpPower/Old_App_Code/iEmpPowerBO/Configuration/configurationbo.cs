using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

/// <summary>
/// Summary description for configurationbo
/// </summary>
public class configurationbo
{
    public configurationbo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int _iRequriesId = 0;
    private string _sDescription;
    string _sAllRequiredIds = string.Empty;

    private bool _bIsRequriesStatus = true;
    private bool _bIsDefalutsStatus = true;
    private string _bIsMandatoryStatus = string.Empty;
    private string _bIsDefalutStatus = string.Empty;
    private string _sCreatedBy = string.Empty;

    private DateTime _dtCreatedOn = DateTime.Now;

    private string _sModifiedBy = string.Empty;

    private DateTime _dtModifiedOn = DateTime.Now;

    private string _sPOPServer = string.Empty;
    private string _sIMAPServer = string.Empty;
    private string _sSMTPServre = string.Empty;
    private Binary _iLogo;
    private string _sEmployeePath = string.Empty;

    private string _sEmailId = string.Empty;
    private string _sUserName = string.Empty;
    private string _sPassword = string.Empty;
    private bool _bIsHRStatus = true;

    private string _sHRDescription;
    private string _sPort;
    private bool _bIsFinAdminStatus = false;
    private string _sFinAdminStatus = string.Empty;

    private int _iMoawb = 0;
    private string _strAwart = string.Empty;
    private string _strAtext = string.Empty;
    private bool _blEncashable = false;
    private string _strMoawbList = string.Empty;
    private string _strAwartList = string.Empty;
    private string _strAtextList = string.Empty;
    private string _strEncashableList = string.Empty;
    private string _persk = string.Empty;

    private string _strName = string.Empty;
    private string _strLastLoginDate = string.Empty;

    public string PORT
    {
        get { return _sPort; }
        set { _sPort = value; }
    }
    public string HR_DESCRIPTION
    {
        get { return _sHRDescription; }
        set { _sHRDescription = value; }
    }
    public bool HR_STATUS
    {
        get { return _bIsHRStatus; }
        set { _bIsHRStatus = value; }
    }
    public string EMAIL_ID
    {
        get { return _sEmailId; }
        set { _sEmailId = value; }
    }
    public string USER_NAME
    {
        get { return _sUserName; }
        set { _sUserName = value; }
    }
    public string PASSWORD
    {
        get { return _sPassword; }
        set { _sPassword = value; }
    }
    public Binary LOGO
    {
        get { return _iLogo; }
        set { _iLogo = value; }
    }
    public string EMPLOYEE_PATH
    {
        get { return _sEmployeePath; }
        set { _sEmployeePath = value; }
    }
    public string POP_SERVER
    {
        get { return _sPOPServer; }
        set { _sPOPServer = value; }
    }
    public string IMAP_SERVER
    {
        get { return _sIMAPServer; }
        set { _sIMAPServer = value; }
    }
    public string SMTP_SERVER
    {
        get { return _sSMTPServre; }
        set { _sSMTPServre = value; }
    }
    public string MANDATORY_VALUE
    {
        get { return _bIsMandatoryStatus; }
        set { _bIsMandatoryStatus = value; }
    }
    public string DEFAULT_VALUE
    {
        get { return _bIsDefalutStatus; }
        set { _bIsDefalutStatus = value; }
    }
    public int REQURIES_ID
    {
        get { return _iRequriesId; }
        set { _iRequriesId = value; }
    }
    public string ALL_REQURIES_IDS
    {
        get { return _sAllRequiredIds; }
        set { _sAllRequiredIds = value; }
    }
    public string DESCRIPTION
    {
        get { return _sDescription; }
        set { _sDescription = value; }
    }
    public bool REQUIRES_STATUS
    {
        get { return _bIsRequriesStatus; }
        set { _bIsRequriesStatus = value; }
    }
    public bool DEFAULT_STATUS
    {
        get { return _bIsDefalutsStatus; }
        set { _bIsDefalutsStatus = value; }
    }
    public DateTime CREATEDON
    {
        get { return _dtCreatedOn; }
        set { _dtCreatedOn = value; }
    }
    public DateTime MODIFIEDON
    {
        get { return _dtModifiedOn; }
        set { _dtModifiedOn = value; }
    }
    public string CREATEDBY
    {
        get { return _sCreatedBy; }
        set { _sCreatedBy = value; }
    }
    public string MODIFIEDBY
    {
        get { return _sModifiedBy; }
        set { _sModifiedBy = value; }
    }

    public bool FIN_ADMIN_STATUS
    {
        get { return _bIsFinAdminStatus; }
        set { _bIsFinAdminStatus = value; }
    }

    public string FIN_ADMIN_STATUS_DESC
    {
        get { return _sFinAdminStatus; }
        set { _sFinAdminStatus = value; }
    }

    public int MOAWB
    {
        get { return _iMoawb; }
        set { _iMoawb = value; }
    }

    public string AWART
    {
        get { return _strAwart; }
        set { _strAwart = value; }
    }

    public string ATEXT
    {
        get { return _strAtext; }
        set { _strAtext = value; }
    }

    public bool ENCASHABLE
    {
        get { return _blEncashable; }
        set { _blEncashable = value; }
    }

    public string MOAWB_STRING
    {
        get { return _strMoawbList; }
        set { _strMoawbList = value; }
    }

    public string AWART_STRING
    {
        get { return _strAwartList; }
        set { _strAwartList = value; }
    }

    public string ATEXT_STRING
    {
        get { return _strAtextList; }
        set { _strAtextList = value; }
    }

    public string ENCASHABLE_STRING
    {
        get { return _strEncashableList; }
        set { _strEncashableList = value; }
    }

    public string NAME
    {
        get { return _strName; }
        set { _strName = value; }
    }

    public string LASTLOGINDATE
    {
        get { return _strLastLoginDate; }
        set { _strLastLoginDate = value; }
    }

    public string PERSK
    {
        get { return _persk; }
        set { _persk = value; }
    }

    ////------------------------------------PayCompute-------------
    public string Company_Type_Txt { get; set; }
    public string Company_Code { get; set; }
    public string Company_Name { get; set; }
    public int? Company_Type { get; set; }
    public string Company_Address { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string District { get; set; }
    public decimal? Pincode { get; set; }
    public string Company_MailID { get; set; }
    public decimal? Company_ContactNum { get; set; }
    public string Created_By { get; set; }
    public DateTime? Created_On { get; set; }
    public bool IsLocked { get; set; }
    public string Updated_By { get; set; }
    public DateTime Updated_On { get; set; }
    public string EMPID { get; set; }
    public string CountryTxt { get; set; }
    public string StateTxt { get; set; }
    public int flag { get; set; }
    public int iscomp { get; set; }
    public string DDLTYPETEXT { get; set; }
    public string DDLTYPE { get; set; }
    public int DDLTYPEINT { get; set; }
    public string AppID { get; set; }
    public string LoginID { get; set; }
    public bool isAct { get; set; }
    public int desig { get; set; }
    public string desigTEXT { get; set; }

    public string leav { get; set; }
    public string leavTEXT { get; set; }

    public decimal period { get; set; }
    public decimal qu { get; set; }
    public string validity { get; set; }
    public string year { get; set; }
    public DateTime? Date{ get; set; }
    public string Descrip{ get; set; }
    public string TYPE{ get; set; }

    public string deptid { get; set; }
    public string deptdesc { get; set; }
    public bool? isDR { get; set; }
    public int WK { get; set; }

    public string CLOGO { get; set; }
    public int ID { get; set; }
    public int? CRYFWRD { get; set; }

    public DateTime frmdate { get; set; }
    public DateTime todate { get; set; }
    public DateTime? enddate { get; set; }
    public DateTime? dt1 { get; set; }
    public int id1 { get; set; }

    public string H_type { get; set; }
}
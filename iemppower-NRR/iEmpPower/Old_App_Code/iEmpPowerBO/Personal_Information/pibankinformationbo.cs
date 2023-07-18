using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bankinformation
/// </summary>
public class pibankinformationbo
{
	public pibankinformationbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //private string _sSupervisorId = string.Empty;
    private string _iPERNR = string.Empty;
    private string _strSUBTY= string.Empty;
    private string _strEMFTX = string.Empty;
    private string _strBKPLZ = string.Empty;
    private string _strBKORT = string.Empty;
    private string _strBANKS = string.Empty;
    private string _strBANKL = string.Empty;
    private string _strBANKN = string.Empty;
    private string _strZLSCH = string.Empty;
    private string _strZWECK = string.Empty;
    private string _strWAERS = string.Empty;
    private bool _bIsDefalutStatus;
    private string _sDescription;
    private string _strSTEXT = string.Empty; // for bank type name to show in the grid.
    private string _strLANDX = string.Empty; // for bank country name to show in the grid.
    private string _strTEXT1 = string.Empty; // for payment method name to show in the grid.
    private string _strLTEXT = string.Empty; // for payment currency name to show in the grid.
    private string _str_OLD_BANKN = string.Empty; // Old bank account number used during update to maintain uniquness.

    private bool _bIsActive = false;
    private bool _bIsApproved = false;
    private DateTime _dCreatedOn = DateTime.Now;
    private string _sCreatedBy = string.Empty;
    private DateTime _dApprovedOn  = DateTime.Now;
    private string _sApprovedBy = string.Empty;
    private DateTime _dModifiedOn  = DateTime.Now;
    private string _sModifiedBy = string.Empty;
    private string _sPkey = string.Empty;
    private string _sComments = string.Empty;
    private string _bIStatus;
    private bool _bIsUpdate = false;

    private Guid _ID = new Guid();
    private int? _Flag = 0;

    private int? _RecordCnt = 0;
    private int? _RowNumber = 0;
    private int? _PageIndex = 0;
    private int? _PageSize = 0;


    


    public bool ISUPDATE
    {
        get { return _bIsUpdate; }
        set { _bIsUpdate = value; }
    }
    public string STATUS
    {
        get { return _bIStatus; }
        set { _bIStatus = value; }
    }
    public string COMMENTS
    {
        get { return _sComments; }
        set { _sComments = value; }
    }
    public string PKEY
    {
        get { return _sPkey; }
        set { _sPkey = value; }
    }
    public string DESCRIPTION
    {
        get { return _sDescription; }
        set { _sDescription = value; }
    }
    public bool VALUE
    {
        get { return _bIsDefalutStatus; }
        set { _bIsDefalutStatus = value; }
    }
    
    public string PERNR
    {
        get { return _iPERNR; }
        set { _iPERNR = value; }
    }
    public string BANK_TYPE_ID
    {
        get { return _strSUBTY; }
        set { _strSUBTY = value; }
    }
    public string PAYEE
    {
        get { return _strEMFTX; }
        set { _strEMFTX = value; }
    }
    public string POSTAL_CODE
    {
        get { return _strBKPLZ; }
        set { _strBKPLZ = value; }
    }
    public string CITY
    {
        get { return _strBKORT; }
        set { _strBKORT = value; }
    }
    public string BANK_COUNTRY
    {
        get { return _strBANKS; }
        set { _strBANKS = value; }
    }
    public string BANK_KEY
    {
        get { return _strBANKL; }
        set { _strBANKL = value; }
    }

    public string BANK_ACCOUNT
    {
        get { return _strBANKN; }
        set { _strBANKN = value; }
    }
    public string PAYMENT_METHOD
    {
        get { return _strZLSCH; }
        set { _strZLSCH = value; }
    }
    public string PURPOSE
    {
        get { return _strZWECK; }
        set { _strZWECK = value; }
    }
   
    public string PAYMENT_CURRENCY
    {
        get { return _strWAERS; }
        set { _strWAERS = value; }
    }
    public string BANK_TYPE_NAME
    {
        get { return _strSTEXT; }
        set { _strSTEXT = value; }
    }
    public string COUNTRY_NAME
    {
        get { return _strLANDX; }
        set { _strLANDX = value; }
    }
    public string PAYMENT_METHOD_NAME
    {
        get { return _strTEXT1; }
        set { _strTEXT1 = value; }
    }
    public string PAYMENT_CURRENCY_NAME
    {
        get { return _strLTEXT; }
        set { _strLTEXT = value; }
    }
    public string OLD_BANK_ACCOUNT_NO
    {
        get { return _str_OLD_BANKN; }
        set { _str_OLD_BANKN = value; }
    }
    public DateTime CREATED_ON
    {
        get { return _dCreatedOn; }
        set { _dCreatedOn = value; }
    }
    public DateTime APPROVED_ON
    {
        get { return _dApprovedOn; }
        set { _dApprovedOn = value; }
    }
    public string CREATED_BY
    {
        get { return _sCreatedBy; }
        set { _sCreatedBy = value; }
    }
    public string APPROVED_BY
    {
        get { return _sApprovedBy; }
        set { _sApprovedBy = value; }
    }
    public bool ISACTIVE
    {
        get { return _bIsActive; }
        set { _bIsActive = value; }
    }
    public bool ISAPPROVED
    {
        get { return _bIsApproved; }
        set { _bIsApproved = value; }
    }
    public DateTime MODIFIEDON
    {
        get { return _dModifiedOn; }
        set { _dModifiedOn = value; }
    }
    public string MODIFIED_BY
    {
        get { return _sModifiedBy; }
        set { _sModifiedBy = value; }
    }

    public Guid ID
    {
        get { return _ID; }
        set { _ID = value; }
    }

    public int? Flag
    {
        get { return _Flag; }
        set { _Flag = value; }
    }

    public int? PageIndex
    {
        get { return _PageIndex; }
        set { _PageIndex = value; }
    }

    public int? PageSize
    {
        get { return _PageSize; }
        set { _PageSize = value; }
    }

    public int? RecordCnt
    {
        get { return _RecordCnt; }
        set { _RecordCnt = value; }
    }

    public int? RowNumber
    {
        get { return _RowNumber; }
        set { _RowNumber = value; }
    }
    
}
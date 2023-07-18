using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for pipersonalidsbo
/// </summary>
public class pipersonalidsbo
{
    public pipersonalidsbo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string _sSupervisorId = string.Empty;
    private string _sEmployeeId = string.Empty;
    private string _sTypeId = string.Empty;
    private string _sOldNo = string.Empty;
    private string _sIDNo = string.Empty;
    private string _sOldType = string.Empty;
    private string _sType = string.Empty;
    private DateTime _dModifiedOn;
    private string _sModifiedBy = string.Empty;
    private bool _bIsUpdate = false;
    private bool _bIsDefalutStatus;
    private string _sDescription;
    private bool _bIsActive = false;
    private bool _bIsApproved = false;
    private DateTime _dCreatedOn;
    private string _sCreatedBy = string.Empty;
    private DateTime _dApprovedOn;
    private string _sApprovedBy = string.Empty;
    private string _sComments = string.Empty;
    private string _bIStatus;
    private string _strIdTypeText = string.Empty;
    private string _strFRFLYNUM = string.Empty;
    private string _strEMPNAME = string.Empty;
    private string _strAIRLINE = string.Empty;
    private string _strVALSTATUS = string.Empty;
    private string _strPASNUM = string.Empty;
    private DateTime _strDOI;
    private DateTime _strDOE;
    private string _strPLISS;
    private string _strTRINSNO;
    private string _strPLAN1;
    private string _strPREMIUM;
    private string _strAGENT_NAME;
    private string _strVINUM;
    private string _strCOUNTRY;
    private string _strVISA_TYPE;
    private DateTime _sFPDAT;
    //private DateTime _sEXPID;
    private string _sAUTH1;

    private DateTime _BEGDA;
    private DateTime _ENDDA;

    private int? _ID = 0;
    private int? _Flag = 0;

    private int? _RecordCnt = 0;
    private int? _RowNumber = 0;
    private int? _PageIndex = 0;
    private int? _PageSize = 0;

    public int CC
    {
        get;
        set;
    }

    public string VINUM
    {
        get { return _strVINUM; }
        set { _strVINUM = value; }
    }

    public string COUNTRY
    {
        get { return _strCOUNTRY; }
        set { _strCOUNTRY = value; }
    }

    public string VISA_TYPE
    {
        get { return _strVISA_TYPE; }
        set { _strVISA_TYPE = value; }
    }


    public string TRINSNO
    {
        get { return _strTRINSNO; }
        set { _strTRINSNO = value; }
    }

    public string PLAN1
    {
        get { return _strPLAN1; }
        set { _strPLAN1 = value; }
    }

    public string PREMIUM
    {
        get { return _strPREMIUM; }
        set { _strPREMIUM = value; }
    }

    public string AGENT_NAME
    {
        get { return _strAGENT_NAME; }
        set { _strAGENT_NAME = value; }
    }


    public string PASNUM
    {
        get { return _strPASNUM; }
        set { _strPASNUM = value; }
    }

    public DateTime DOI
    {
        get { return _strDOI; }
        set { _strDOI = value; }
    }
    public DateTime DOE
    {
        get { return _strDOE; }
        set { _strDOE = value; }
    }

    public string PLISS
    {
        get { return _strPLISS; }
        set { _strPLISS = value; }
    }



    public string FRFLYNUM
    {
        get { return _strFRFLYNUM; }
        set { _strFRFLYNUM = value; }
    }
    public DateTime FPDAT //ISSUE_DATE
    {
        get { return _sFPDAT; }
        set { _sFPDAT = value; }
    }
    public string EMPNAME
    {
        get { return _strEMPNAME; }
        set { _strEMPNAME = value; }
    }

    public string AIRLINE
    {
        get { return _strAIRLINE; }
        set { _strAIRLINE = value; }
    }

    public string VALSTATUS
    {
        get { return _strVALSTATUS; }
        set { _strVALSTATUS = value; }
    }


    public string ID_TYPE_TEXT
    {
        get { return _strIdTypeText; }
        set { _strIdTypeText = value; }
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

    public string SUPERVISOREID
    {
        get { return _sSupervisorId; }
        set { _sSupervisorId = value; }
    }
    public string PERNR //Personal id
    {
        get { return _sEmployeeId; }
        set { _sEmployeeId = value; }
    }
    public string ICTYPE //ID type
    {
        get { return _sTypeId; }
        set { _sTypeId = value; }
    }
    public string ICNUM //ID number
    {
        get { return _sIDNo; }
        set { _sIDNo = value; }
    }
    public string OLD_ICNUM
    {
        get { return _sOldNo; }
        set { _sOldNo = value; }
    }
    public string OLD_ICTYPE
    {
        get { return _sOldType; }
        set { _sOldType = value; }
    }
    public DateTime MODIFIEDON
    {
        get { return _dModifiedOn; }
        set { _dModifiedOn = value; }
    }
   
    public bool ISUPDATE
    {
        get { return _bIsUpdate; }
        set { _bIsUpdate = value; }
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
    public string MODIFIED_BY
    {
        get { return _sModifiedBy; }
        set { _sModifiedBy = value; }
    }

    public int? ID
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

    public DateTime BEGDA
    {
        get { return _BEGDA; }
        set { _BEGDA = value; }
    }

    public DateTime ENDDA
    {
        get { return _ENDDA; }
        set { _ENDDA = value; }
    }

    public string AUTH1 //ISSUE_AUTHORITY
    {
        get { return _sAUTH1; }
        set { _sAUTH1 = value; }
    }

    public string TRANSSTATUS { get; set; }



    public string PKEY { get; set; }

    public string ENAME { get; set; }
    public string PARTICULARS { get; set; }

    public string GBLND { get; set; }

    public string SUPERVISOR_NAME { get; set; }

    public string EMPFNAME { get; set; }

    public string EMPLNAME { get; set; }



    public string CHANGE_APPROVAL { get; set; }

    public string TEXT { get; set; }
    public string VALUES { get; set; }

    public string Slno { get; set; }
    public DateTime? MODON { get; set; }
    public string docpath { get; set; }
    public string RCNO
    {
        get;
        set;
    }


    public string RCNAME
    {
        get;
        set;
    }

}
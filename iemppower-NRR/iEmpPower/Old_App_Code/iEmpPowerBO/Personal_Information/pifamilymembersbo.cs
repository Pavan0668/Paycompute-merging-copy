using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for pifamilymembersbo
/// </summary>
public class pifamilymembersbo
{
    public pifamilymembersbo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _sSupervisorId = string.Empty;
    private string _sEmployeeId = string.Empty;
    private string _sFName = string.Empty;
    private string _sLName = string.Empty;
    private DateTime? _dtDateOfBirth;
    private string _sMemberTypeId = string.Empty;
    private string _sMemberType = string.Empty;
    private string _sNameofBirth = string.Empty;
    private string _sPlaceOfBirth = string.Empty;
    private string _sCountryOfBirth = string.Empty;
    private string _iInitials = string.Empty;
    private string _iGender = string.Empty;
    private string _iGenderName = string.Empty;
    private string _sNationality = string.Empty;
    private string _sNationality3 = string.Empty;
    private DateTime _dtCreatedOn;
    private bool _bIsActive = false;
    string _sNationality2 = string.Empty;
    private bool _bIsApproved = false;
    private bool _bIsUpdate = false;
    private string _sOtherTitle = string.Empty;
    private string _sNamePrefix = string.Empty;
    private bool _bIsDefalutStatus;
    private string _sDescription;
    private string _sOtherAllowance = string.Empty;
    private string _sHostelAllowance = string.Empty;
    private string _sEdutionalAllowance = string.Empty;
    private string _sObjectID = string.Empty;

    private string _sCreatedBy = string.Empty;
    private DateTime _dApprovedOn;
    private string _sApprovedBy = string.Empty;
    private DateTime _dModifiedOn;
    private string _sModifiedBy = string.Empty;
    private string _sPkey = string.Empty;
    private string _sComments = string.Empty;
    private string _bIStatus;
    private string _strMemberType = string.Empty; // Member type name(Text)


    private int? _ID = 0;
    private int? _Flag = 0;

    private int? _RecordCnt = 0;
    private int? _RowNumber = 0;
    private int? _PageIndex = 0;
    private int? _PageSize = 0;

    private string _Subty = string.Empty;

    private DateTime? _Begda;
    private DateTime? _Endda;

    private string _Age = string.Empty;


    public string STEXT
    {
        get { return _strMemberType; }
        set { _strMemberType = value; }
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
    public string PENNR //Employer ID Or Personal number
    {
        get { return _sEmployeeId; }
        set { _sEmployeeId = value; }
    }
    public string FAMSA //Type of family record 
    {
        get { return _sMemberTypeId; }
        set { _sMemberTypeId = value; }
    }
    public string FAVOR //First name
    {
        get { return _sFName; }
        set { _sFName = value; }
    }
    public string FANAM  //Last name
    {
        get { return _sLName; }
        set { _sLName = value; }
    }
    public string FGBNA //Name of birth
    {
        get { return _sNameofBirth; }
        set { _sNameofBirth = value; }
    }
    public string FINIT //Employee's Initials
    {
        get { return _iInitials; }
        set { _iInitials = value; }
    }
    public string FNMZU //OtherTitle
    {
        get { return _sOtherTitle; }
        set { _sOtherTitle = value; }
    }
    public string FVRSW //Name Prefix   
    {
        get { return _sNamePrefix; }
        set { _sNamePrefix = value; }
    }
    public string FASEX //Gender key
    {
        get { return _iGender; }
        set { _iGender = value; }
    }

    public string FASEX_Name //Gender key
    {
        get { return _iGenderName; }
        set { _iGenderName = value; }
    }
    public DateTime? FGBDT //Date of birth
    {
        get { return _dtDateOfBirth; }
        set { _dtDateOfBirth = value; }
    }
    public string FGBOT //Place of birth
    {
        get { return _sPlaceOfBirth; }
        set { _sPlaceOfBirth = value; }
    }
    public string FGBLD //Country of Birth
    {
        get { return _sCountryOfBirth; }
        set { _sCountryOfBirth = value; }
    }
    public string FANAT //Nationality
    {
        get { return _sNationality; }
        set { _sNationality = value; }
    }
    public string FANA2 //Second Nationality
    {
        get { return _sNationality2; }
        set { _sNationality2 = value; }
    }
    public string FANA3 //Third Nationality
    {
        get { return _sNationality3; }
        set { _sNationality3 = value; }
    }
    public DateTime CREATEDON
    {
        get { return _dtCreatedOn; }
        set { _dtCreatedOn = value; }
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
    public bool ISUPDATE
    {
        get { return _bIsUpdate; }
        set { _bIsUpdate = value; }
    }
    //Other allowances 
    public string KDBSL
    {
        get { return _sOtherAllowance; }
        set { _sOtherAllowance = value; }
    }
    //Child Hostel Allowance 
    public string KDBGR
    {
        get { return _sHostelAllowance; }
        set { _sHostelAllowance = value; }
    }
    //Child Educational Allowance 
    public string KDZUL
    {
        get { return _sEdutionalAllowance; }
        set { _sEdutionalAllowance = value; }
    }
    //Object Identification 
    public string OBJPS
    {
        get { return _sObjectID; }
        set { _sObjectID = value; }
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

    public string SUBTY
    {
        get { return _Subty; }
        set { _Subty = value; }
    }


    public DateTime? BEGDA
    {
        get { return _Begda; }
        set { _Begda = value; }
    }

    public DateTime? ENDDA //Date of birth
    {
        get { return _Endda; }
        set { _Endda = value; }
    }

    public string AGE //Date of birth
    {
        get { return _Age; }
        set { _Age = value; }
    }


    public string TRANSSTATUS { get; set; }



 

    public string ENAME { get; set; }
    public string PARTICULARS { get; set; }

    public string GBLND { get; set; }


    public string SUPERVISOREID { get; set; }



    public string SUPERVISOR_NAME { get; set; }

    public string EMPFNAME { get; set; }

    public string EMPLNAME { get; set; }



    public string CHANGE_APPROVAL { get; set; }

    public string TEXT { get; set; }
    public string VALUES { get; set; }

    public string Slno { get; set; }

    public DateTime? MODON { get; set; } 
}
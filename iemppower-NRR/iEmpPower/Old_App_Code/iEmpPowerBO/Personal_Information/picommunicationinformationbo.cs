using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for picommunicationinformationbo
/// </summary>
public class picommunicationinformationbo
{
    public picommunicationinformationbo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _sEmployeeId = string.Empty;
    private string _sExtension = string.Empty;
    private string _sEmail = string.Empty;
    private string _sBuildingNo = string.Empty;
    private string _sRoomNo = string.Empty;
    private string _sLicencePlateNo = string.Empty;

    private string strPersonalNo = string.Empty;//Employer ID Or Personal number
    private string strusrty = string.Empty; //Subtype
    private string strusrid = string.Empty;//Valid from date

    private string strusr2 = string.Empty;
    private string strusr2id = string.Empty;
    private string strusr3 = string.Empty;
    private string strusr3id = string.Empty;
    private string strusr4 = string.Empty;
    private string strusr4id = string.Empty;
    private string strusr5 = string.Empty;
    private string strusr5id = string.Empty;
    private string strusr6 = string.Empty;
    private string strusr6id = string.Empty;
    private string strMPHN = string.Empty;
    private string struMPHNID = string.Empty;

    private bool _bIsActive = false;
    private bool _bIsApproved = false;
    private DateTime _dCreatedOn;
    private string _sCreatedBy = string.Empty;
    private DateTime _dApprovedOn;
    private string _sApprovedBy = string.Empty;
    private DateTime _dModifiedOn;
    private string _sModifiedBy = string.Empty;
    private string _sComments = string.Empty;
    private string _bIStatus;
    private bool _bIsUpdate = false;
    private string _sEmployeePath = string.Empty;
    private string _sDesignation = string.Empty;

    private DateTime _BEGDA;
    private DateTime _ENDDA;

    private int? _ID = 0;
    private int? _Flag = 0;

    private int? _RecordCnt = 0;
    private int? _RowNumber = 0;
    private int? _PageIndex = 0;
    private int? _PageSize = 0;

    private string _USTXT = string.Empty;


    public string MPHN
    {
        get { return strMPHN; }
        set { strMPHN = value; }
    }
    public string MPHN_ID
    {
        get { return struMPHNID; }
        set { struMPHNID = value; }
    }
    public string EMPLOYEE_PHOTO_PATH
    {
        get { return _sEmployeePath; }
        set { _sEmployeePath = value; }
    }
    public string DESIGNATION
    {
        get { return _sDesignation; }
        set { _sDesignation = value; }
    }
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
    public string USR2
    {
        get { return strusr2; }
        set { strusr2 = value; }
    }
    public string USR2ID
    {
        get { return strusr2id; }
        set { strusr2id = value; }
    }

    public string USR3
    {
        get { return strusr3; }
        set { strusr3 = value; }
    }
    public string USR3ID
    {
        get { return strusr3id; }
        set { strusr3id = value; }
    }
    public string USR4
    {
        get { return strusr4; }
        set { strusr4 = value; }
    }
    public string USR4ID
    {
        get { return strusr4id; }
        set { strusr4id = value; }
    }
    public string USR5
    {
        get { return strusr5; }
        set { strusr5 = value; }
    }
    public string USR5ID
    {
        get { return strusr5id; }
        set { strusr5id = value; }
    }
    public string USR6
    {
        get { return strusr6; }
        set { strusr6 = value; }
    }
    public string USR6ID
    {
        get { return strusr6id; }
        set { strusr6id = value; }
    }
  
    public string USRTY
    {
        get { return strusrty; }
        set { strusrty = value; }
    }
    public string PERNR
    {
        get { return strPersonalNo; }
        set { strPersonalNo = value; }
    }
    public string USRID
    {
        get { return strusrid; }
        set { strusrid = value; }
    }
 
    public string EMPLOYEE_ID
    {
        get { return _sEmployeeId; }
        set { _sEmployeeId = value; }
    }
    public string EXTENSION
    {
        get { return _sExtension; }
        set { _sExtension = value; }
    }
    public string EMAIL
    {
        get { return _sEmail; }
        set { _sEmail = value; }
    }
    public string BUILDING_NO
    {
        get { return _sBuildingNo; }
        set { _sBuildingNo = value; }
    }
    public string ROOM_NO
    {
        get { return _sRoomNo; }
        set { _sRoomNo = value; }
    }
    public string LICENCE_NO
    {
        get { return _sLicencePlateNo; }
        set { _sLicencePlateNo = value; }
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


    public string USTXT
    {
        get { return _USTXT; }
        set { _USTXT = value; }
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



   

    public string TRANSSTATUS { get; set; }

   

    public string PKEY { get; set; }

    public string ENAME { get; set; }
    public string PARTICULARS { get; set; }

    public string GBLND { get; set; }

    public string DESCRIPTION { get; set; }
 


    public bool VALUE { get; set; }

    public string SUPERVISOREID { get; set; }



    public string SUPERVISOR_NAME { get; set; }

    public string EMPFNAME { get; set; }

    public string EMPLNAME { get; set; }


    
    public string CHANGE_APPROVAL { get; set; }

    public string TEXT { get; set; }
    public string VALUES { get; set; }

    public string Slno { get; set; }
    public DateTime? MODON { get; set; }
    public string ALTEMAILID { get; set; }
    public string DEPDETAILS { get; set; }
    public string PERAREADESC { get; set; }
    public string JOININGDATE { get; set; }

    public string MODULE { get; set; }
    public string MNGRNAME { get; set; } 
   
}
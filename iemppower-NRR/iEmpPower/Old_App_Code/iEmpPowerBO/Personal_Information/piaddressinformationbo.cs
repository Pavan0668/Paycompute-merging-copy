using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for addressinformationbo
/// </summary>
public class piaddressinformationbo
{
    public piaddressinformationbo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //private string _sSupervisorId = string.Empty;
    //private string _sEmployeeId = string.Empty;
    //private string _sFName = string.Empty;
    //private string _sLName = string.Empty;
    //private DateTime _dtDateFrom;
    //private DateTime _dtDateTo;
    //private string _sAddressTypeId = string.Empty;
    //private string _sAddressL1 = string.Empty;
    //private string _sAddressL2 = string.Empty;
    //private string _sCity = string.Empty;
    //private string _sStateId = string.Empty;
    //private string _iPostalCode = string.Empty;
    //private string _iPhone = string.Empty;
    //private string _iMobile = string.Empty;
    //private bool _bIsActive = false;
    //string _sSuperVisorName = string.Empty;
    //private bool _bIsApproved = false;
    //private bool _bIsUpdate = false;
    //private bool _bIsDefalutStatus;
    //private string _sDescription;
    //private string _strGBLND = string.Empty;

    //private DateTime? _dCreatedOn = DateTime.Now;
    //private string _sCreatedBy = string.Empty;
    //private DateTime _dApprovedOn = DateTime.Now;
    //private string _sApprovedBy = string.Empty;
    //private DateTime? _dModifiedOn = DateTime.Now;
    //private string _sModifiedBy = string.Empty;
    //private string _sPkey = string.Empty;
    //private string _sComments = string.Empty;
    //private string _bIStatus;
    //private string _strAddrsTypeText = string.Empty;

    //private Guid _ID = new Guid();
    //private int? _Flag = 0;

    //private int? _RecordCnt = 0;
    //private int? _RowNumber = 0;
    //private int? _PageIndex = 0;
    //private int? _PageSize = 0;


    //private string _Subty = string.Empty;
    //private string _CountryTxt = string.Empty;
    //private string _StateTxt = string.Empty;


    public string ADDRESS_TYPE_TEXT { get; set; }

    public string STATUS { get; set; }
    public string TRANSSTATUS { get; set; }
     
    public string COMMENTS { get; set; }

    public string PKEY { get; set; }

    public string ENAME { get; set; }
    public string PARTICULARS { get; set; } 

    public string GBLND { get; set; }

    public string DESCRIPTION { get; set; }

    public bool VALUE { get; set; }

    public string SUPERVISOREID { get; set; }

    public string EMPLOYEE_ID { get; set; }

    public string SUPERVISOR_NAME { get; set; }

    public string EMPFNAME { get; set; }

    public string EMPLNAME { get; set; }

    public DateTime DATE_FROM { get; set; }

    public DateTime DATE_TO { get; set; }

    public string ADDRESS_TYPE_ID { get; set; }

    public string ADDRESSL1 { get; set; }

    public string ADDRESSL2 { get; set; }

    public string CITY { get; set; }

    public string STATE_ID { get; set; }

    public string POSTAL_CODE { get; set; }

    public string PHONENO { get; set; }

    public string MOBILENO { get; set; }

    public bool ISACTIVE { get; set; }

    public bool ISAPPROVED { get; set; }

    public bool ISUPDATE { get; set; }

    public DateTime? CREATED_ON { get; set; }

    public DateTime APPROVED_ON { get; set; }

    public string CREATED_BY { get; set; }

    public string APPROVED_BY { get; set; }

    public DateTime? MODIFIEDON { get; set; }

    public string MODIFIED_BY { get; set; }

    public int? ID { get; set; }

    public int? Flag { get; set; }

    public int? PageIndex { get; set; }

    public int? PageSize { get; set; }

    public int? RecordCnt { get; set; }

    public int? RowNumber { get; set; }

    public string SUBTY { get; set; }

    public string LANDX { get; set; }

    public string BEZEI { get; set; }
    public string CHANGE_APPROVAL { get; set; }

    public string TEXT { get; set; }
    public string VALUES { get; set; } 

    public string Slno { get; set; }


    public string CountyText { get; set; }

    public string StateText { get; set; }


    public DateTime? MODON { get; set; } 
    

}
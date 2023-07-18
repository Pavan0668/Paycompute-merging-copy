using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for leaverequestbo
/// </summary>
public class leaverequestbo
{
    public leaverequestbo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //  private string _iPERNR = string.Empty; // Personnel ID
    //  private string _strEMP_NAME = string.Empty;
    //  private DateTime? _dtBEGDA; // Leave start date
    //  private DateTime? _dtENDDA; // Leave end date
    //  private string _tmstrBEGUZ = string.Empty; // Leave start time
    //  private string _tmstrENDUZ = string.Empty; // Leave end time
    //  private string _strAWART = string.Empty; // Leave type ID
    //  private string _strSTDAZ = string.Empty; // Leave duration
    //  private string _strNOTE = string.Empty;
    //  private string _iAPPROVED_BY = string.Empty;
    //  private DateTime? _dtLeaveSince;
    //  private string _strATEXT = string.Empty; // Leave type text.
    //  private Guid _iLEAVE_REQ_ID = new Guid(); // Leave request Id.
    //  private string _strSTATUS = string.Empty; // Leave status - Like 'Approved','Deletion requested','Sent'
    //  private string _iANZHL = "0"; // Number of leaves(Quota) allowed for a leave type.
    //  private string _iKVERB = "0"; // Number of leaves deducted(used) from a leave type alloted for an employee. 
    //  private DateTime? _dtFROM_DATE; // Calendar 1 first date to get calendar markings
    //  private DateTime? _dtTO_DATE; // Calendar 3 last date to get calendar markings.
    //  private DateTime? _dtLEAVE_DATE; // leave date
    //  // Leave request approval related properties.
    //  private bool _bIsApproved = false;
    //  private bool _bIsUpdate = false;
    //  private bool _bIsActive = false;
    //  private string _sPkey = string.Empty;
    //  private string _sComments = string.Empty;
    //  private string _bEntryStatus;
    //  private string _strArppovedBy_Name = string.Empty;
    //  private DateTime _dtLeaveQuotaStartDate;
    //  private DateTime _dtLeaveQuotaEndDate;
    //  private string _iAvailable_Days = "0";
    //  private int? _Year = 0;

    //  private int? _RecordCnt = 0;
    //  private int? _RowNumber = 0;
    //  private int? _PageIndex = 0;
    //  private int? _PageSize = 0;

    //  private string _text = string.Empty;
    //  private string _value = string.Empty;
    //  private string _Slno = string.Empty;
    //private string _strHRSTATUS = string.Empty;



    public DateTime LEAVE_QUOTA_START_DATE { get; set; }
    public DateTime LEAVE_QUOTA_END_DATE { get; set; }
    // Personnel ID

    public string HR_STATUS { get; set; }
    public string APPROVED_BY_NAME { get; set; }
    public string EMPLOYEE_NAME { get; set; }
    // Personnel ID
    public string PERNR { get; set; }
    public DateTime? BEGDA { get; set; }
    public DateTime? ENDDA { get; set; }
    public string BEGUZ { get; set; }
    public string ENDUZ { get; set; }
    public string AWART { get; set; }
    public string ATEXT { get; set; }
    public string STDAZ { get; set; }
    public string NOTE { get; set; }
    public string APPROVED_BY { get; set; }
    public DateTime? LEAVESINCE { get; set; }
    public int? LEAVE_REQ_ID { get; set; }
    public string ANZHL { get; set; }
    public string KVERB { get; set; }
    public string STATUS { get; set; }
    public DateTime? FROM_DATE { get; set; }
    public DateTime? TO_DATE { get; set; }
    public DateTime? DATUM { get; set; }
    public bool ISACTIVE { get; set; }
    public bool ISAPPROVED { get; set; }
    public bool ISUPDATE { get; set; }
    public string ENTRY_STATUS { get; set; }
    public string COMMENTS { get; set; }
    public string PKEY { get; set; }
    public string AVAILABLE_DAYS { get; set; }
    public string TEXT { get; set; }
    public string VALUE { get; set; }
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
    public int? RecordCnt { get; set; }
    public int? RowNumber { get; set; }
    public int? YEAR { get; set; }
    public string Slno { get; set; }
    public decimal? TotalDays { get; set; }
    public string TABLETYPE { get; set; }
    public string LTYPE { get; set; }
     
    
    //--------------
    public int Flag { get; set; }
    public string VALUES { get; set; }

    public DateTime? APPROVED_ON { get; set; }
    public DateTime? CREATED_ON { get; set; }
    public string REMARKS { get; set; }
    public string DURATION { get; set; }
    public string DURATIONTEXT { get; set; }

    private DateTime? _holidaydate;

    private string _holiday;
  
    public string HOLIDAYS
    {
        get { return _holiday; }
        set { _holiday = value; }
    }

    public DateTime? HOLIDAYDATE
    {
        get { return _holidaydate; }
        set { _holidaydate = value; }
    } 


    //clock inout

    //----------- CLOCK-IN / CLOCK-OUT -----
    private long? _RowNumberCICO;
    private int? _TtlDays;
    private string _Punch_In, _Punch_Out, _TtlHours, _hcalid, _txt_long, _dates1;
    private DateTime? _dates;


    public string PUNCH_IN
    {
        get { return _Punch_In; }
        set { _Punch_In = value; }
    }

    public string PUNCH_OUT
    {
        get { return _Punch_Out; }
        set { _Punch_Out = value; }
    }

    public string TOTAL_HOURS
    {
        get { return _TtlHours; }
        set { _TtlHours = value; }
    }


    public string TXT_LONG
    {
        get { return _txt_long; }
        set { _txt_long = value; }
    }

    public long? ROWNUMBERCICO
    {
        get { return _RowNumberCICO; }
        set { _RowNumberCICO = value; }
    }

    public DateTime? DATES
    {
        get { return _dates; }
        set { _dates = value; }
    }

    public string DATES1
    {
        get { return _dates1; }
        set { _dates1 = value; }
    }

    public int? TOTAL_DAYS
    {

        get { return _TtlDays; }
        set { _TtlDays = value; }
    }

    public long PIPORECCOUNT { get; set; }

    public int LEAVECOUNT { get; set; }
   
    //-------------------ClockIN OUT Report-----------------------------

    public long? ROWNUMBERREPORT { get; set; } 
    public decimal? TWORKINGDAYS { get; set; }
    public decimal? TOTALHRS { get; set; }
    public decimal? TOLHRSBYEMP { get; set; }
    public decimal? AVGAVAILABILITY { get; set; }
    public decimal? PUNCHIN { get; set; }
    public decimal? PUNCHOUT { get; set; }
    public decimal? MISSEDPUNCH { get; set; }
    public decimal? LEAVECOUNTD { get; set; }
    public decimal? REGULARATTDNCE { get; set; }
    public string DATEOFJOINING { get; set; }
    public string KLASSTXT { get; set; }
    public bool? count { get; set; }
    public string brkhrs { get; set; }
    
}
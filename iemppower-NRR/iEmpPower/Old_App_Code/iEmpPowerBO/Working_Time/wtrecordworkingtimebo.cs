using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for recordworkingtimebo
/// </summary>
public class wtrecordworkingtimebo
{
    public wtrecordworkingtimebo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
  
    private string _lCurrentRecordNo;
    private string _sEmployeeId = string.Empty;
    private string _sDays = string.Empty;
    private string _sHours = string.Empty;
    private string _sAttendenceType = string.Empty;
    private string _sWorkingDates = string.Empty;
    private DateTime _dtFromDate;
    private DateTime _dtToDate;
    private string _sApprovedBy = string.Empty;
    private string _sPkey = string.Empty;
    private string _sComments = string.Empty;
    private bool _bIsApproved = false;
    private string _strStatus = string.Empty;
    private bool _bIsUpdate = false;
    private string sHursperworking = string.Empty;
    private string _strStatus1 = string.Empty;
    private string _sCostCenter = string.Empty;
    private string _sOrder = string.Empty;
    private string _strKTEXT = string.Empty; //cost center
    private string _strLTEXT = string.Empty; //Order
    private string _strRNPLNR = string.Empty; //Network
    private string _strLSTAR = string.Empty; //Activity Type
    private string _strRPROJ = string.Empty; //WBS
    private string _sts = string.Empty;

    //Network
    public string RNPLNR
    {
        get { return _strRNPLNR; }
        set { _strRNPLNR = value; }
    }

    public string STS
    {
        get { return _sts; }
        set { _sts = value; }
    }

    //Activity Type
    public string LSTAR
    {
        get { return _strLSTAR; }
        set { _strLSTAR = value; }
    }

    //WBS
    public string RPROJ
    {
        get { return _strRPROJ; }
        set { _strRPROJ = value; }
    }


    //Cost center
    public string LTEXT
    {
        get { return _strLTEXT; }
        set { _strLTEXT = value; }
    }
    //Internal order
    public string KTEXT
    {
        get { return _strKTEXT; }
        set { _strKTEXT = value; }
    }
    public string ORDER
    {
        get { return _sOrder; }
        set { _sOrder = value; }
    }
    public string COST_CENTER
    {
        get { return _sCostCenter; }
        set { _sCostCenter = value; }
    }
    public string STATUS1
    {
        get { return _strStatus1; }
        set { _strStatus1 = value; }
    }
    public string ARBST
    {
        get { return sHursperworking; }
        set { sHursperworking = value; }
    }
    public bool ISUPDATE
    {
        get { return _bIsUpdate; }
        set { _bIsUpdate = value; }
    }
    public string ENTRY_STATUS
    {
        get { return _strStatus; }
        set { _strStatus = value; }
    }
    public bool ISAPPROVED
    {
        get { return _bIsApproved; }
        set { _bIsApproved = value; }
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
    public string APPROVEDBY
    {
        get { return _sApprovedBy; }
        set { _sApprovedBy = value; }
    }
    public string CURRENTRECORD_NO
    {
        get { return _lCurrentRecordNo; }
        set { _lCurrentRecordNo = value; }
    }
    public DateTime TO_DATE
    {
        get { return _dtToDate; }
        set { _dtToDate = value; }
    }
    public DateTime FROM_DATE
    {
        get { return _dtFromDate; }
        set { _dtFromDate = value; }
    }
    public string WORKING_DATE
    {
        get { return _sWorkingDates; }
        set { _sWorkingDates = value; }
    }


    //Employer ID Or Personal number
    public string PERNR
    {
        get { return _sEmployeeId; }
        set { _sEmployeeId = value; }
    }
    public string DAYS
    {
        get { return _sDays; }
        set { _sDays = value; }
    }
    public string CATSHOURS
    {
        get { return _sHours; }
        set { _sHours = value; }
    }
    public string AWART
    {
        get { return _sAttendenceType; }
        set { _sAttendenceType = value; }
    }
    string _sSunday = string.Empty;
    string _sMonday = string.Empty;
    string _sTuesday = string.Empty;
    string _sWednesday = string.Empty;
    string _sThursday = string.Empty;
    string _sFriday = string.Empty;
    string _sSaturday = string.Empty;
    string _remarks = string.Empty;
    string _maxdate = string.Empty;
    string _mindate = string.Empty;
    public string SUNDAY
    {
        get { return _sSunday; }
        set { _sSunday = value; }
    }
    public string MONDAY
    {
        get { return _sMonday; }
        set { _sMonday = value; }
    }
    public string TUESDAY
    {
        get { return _sTuesday; }
        set { _sTuesday = value; }
    }
    public string WEDNESDAY
    {
        get { return _sWednesday; }
        set { _sWednesday = value; }
    }
    public string THURSDAY
    {
        get { return _sThursday; }
        set { _sThursday = value; }
    }
    public string FRIDAY
    {
        get { return _sFriday; }
        set { _sFriday = value; }
    }
    public string SATURDAY
    {
        get { return _sSaturday; }
        set { _sSaturday = value; }
    }

    public string REMARKS
    {
        get { return _remarks; }
        set { _remarks = value; }
    }

    public string MINDATERWT
    {
        get { return _mindate; }
        set { _mindate = value; }
    }

    public string MAXDATERWT
    {
        get { return _maxdate; }
        set { _maxdate = value; }
    }


    //-------------------------------------------------------------------

    //-------------------------Planned Activities Task ------------------------------------------

    public int TaskID { get; set; }
    public string EMPID { get; set; }
    public string ENAME { get; set; }
    public string MODULE { get; set; }
    public string TASKACTIVITY { get; set; }
    public string MONTASK { get; set; }
    public string TUETASK { get; set; }
    public string WEDTASK { get; set; }
    public string THURTASK { get; set; }
    public string FRITASK { get; set; }
    public string SATTASK { get; set; }
    public string SUNTASK { get; set; }
    public decimal MONHOURS { get; set; }
    public decimal TUEHOURS { get; set; }
    public decimal WEDHOURS { get; set; }
    public decimal THURHOURS { get; set; }
    public decimal FRIHOURS { get; set; }
    public decimal SATHOURS { get; set; }
    public decimal SUNHOURS { get; set; }
    public string ACTIVITYID { get; set; }
    public DateTime? MONWORKDATE { get; set; }
    public DateTime? TUEWORKDATE { get; set; }
    public DateTime? WEDWORKDATE { get; set; }
    public DateTime? THURWORKDATE { get; set; }
    public DateTime? FRIWORKDATE { get; set; }
    public string TASKWORKDATE { get; set; }
    public string TASKHOURS { get; set; }
    public string TASKWORK { get; set; }
    public string MNGRPERNR { get; set; }
    public string CREATED_BYPERNR { get; set; }
    public string MNGRUPDATEDBY { get; set; }
    public string EMPUPDATEDBY { get; set; }
    public DateTime? CREATEDONDATE { get; set; }
    public DateTime? MNGRUPDATEDON { get; set; }
    public DateTime? EMPUPDATEON { get; set; }
    public string TASKSTATUS { get; set; }
    public string TID { get; set; }
    public string PLNDHRS { get; set; }

    public string Ccode { get; set; }
}
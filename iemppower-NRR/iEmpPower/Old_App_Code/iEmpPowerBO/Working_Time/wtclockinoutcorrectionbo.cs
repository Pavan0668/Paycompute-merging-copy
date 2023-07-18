using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for piclockinoutcorrectionbo
/// </summary>
public class wtclockinoutcorrectionbo
{
	public wtclockinoutcorrectionbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private decimal? _dNumber;
    private string _sEmployeeId = string.Empty;
    private string _sApprovedBy = string.Empty;
    private DateTime _dtDate;
    private string _tsTime;
    private string _sTimeEventTypeID = string.Empty;
    private string _sTimeEventType = string.Empty;
    private string _sStatus = string.Empty;
    private string _sNote = string.Empty;
    private string _sAllNumbers = string.Empty;
    private string _sAllDate = string.Empty;
    private string _sAllTimes = string.Empty;
    private string _sAllTimeTypes = string.Empty;
    private DateTime _dtFromDate;
    private DateTime _dtToDate;
    private DateTime _dtCreatedOn;
    private DateTime _dtModifiedOn;
    private string _sPkey = string.Empty;
    private string _sComments = string.Empty;
    private bool _bIsApproved = false;
    private string _strEntryStatus = string.Empty;
    private bool _bIsUpdate = false;
    public bool ISUPDATE
    {
        get { return _bIsUpdate; }
        set { _bIsUpdate = value; }
    }
    public string ENTRY_STATUS
    {
        get { return _strEntryStatus; }
        set { _strEntryStatus = value; }
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
    //Sequential number for PDC message
    public decimal? PDSNR 
    {
        get { return _dNumber; }
        set { _dNumber = value; }
    }
    //Employer ID Or Personal number
    public string PERNR 
    {
        get { return _sEmployeeId; }
        set { _sEmployeeId = value; }
    }
    //Logical Date
    public DateTime LDATE 
    {
        get { return _dtDate; }
        set { _dtDate = value; }
    }
    //Logical Time
    public string LTIME 
    {
        get { return _tsTime; }
        set { _tsTime = value; }
    }
    //Time event type
    public string SATZA 
    {
        get { return _sTimeEventTypeID; }
        set { _sTimeEventTypeID = value; }
    }
    //Time event type name
    public string SATZA_TYPE 
    {
        get { return _sTimeEventType; }
        set { _sTimeEventType = value; }
    }
    //Status for clock in/out
    public string STATUS 
    {
        get { return _sStatus; }
        set { _sStatus = value; }
    }
    public string NOTE 
    {
        get { return _sNote; }
        set { _sNote = value; }
    }
    public string ALLNUMBERS
    {
        get { return _sAllNumbers; }
        set { _sAllNumbers = value; }
    }
    public string ALLDATES
    {
        get { return _sAllDate; }
        set { _sAllDate = value; }
    }
    public string ALLTIMES
    {
        get { return _sAllTimes; }
        set { _sAllTimes = value; }
    }
    public string ALLTIME_TYPES
    {
        get { return _sAllTimeTypes; }
        set { _sAllTimeTypes = value; }
    }

    public DateTime CREATEDON
    {
        get { return _dtCreatedOn; }
        set { _dtCreatedOn = value; }
    }
    public DateTime MODIFIEDON
    {
        get { return _dtCreatedOn; }
        set { _dtCreatedOn = value; }
    }
    public DateTime FROMDATE
    {
        get { return _dtFromDate; }
        set { _dtFromDate = value; }
    }
    public DateTime TODATE
    {
        get { return _dtToDate; }
        set { _dtToDate = value; }
    }

}
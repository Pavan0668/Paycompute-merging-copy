using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for piclockinoutcorrectionbo
/// </summary>
public class piclockinoutcorrectionbo
{
	public piclockinoutcorrectionbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _sEmployeeId = string.Empty;
    private DateTime _dtDate;
    private TimeSpan _tsTime;
    private string _sClockInOut = string.Empty;
    private string _sStatus = string.Empty;
    private string _sNote = string.Empty;
    private string _sAllDate = string.Empty;
    private string _sAllTimes = string.Empty;
    private DateTime _dtCreatedOn;

    public string PENNR //Employer ID Or Personal number
    {
        get { return _sEmployeeId; }
        set { _sEmployeeId = value; }
    }
    public DateTime LDATE //Date
    {
        get { return _dtDate; }
        set { _dtDate = value; }
    }
    public TimeSpan LTIME //Time
    {
        get { return _tsTime; }
        set { _tsTime = value; }
    }
    public string SATZA //Time type
    {
        get { return _sClockInOut; }
        set { _sClockInOut = value; }
    }
    public string STATUS //Status for clock in/out
    {
        get { return _sStatus; }
        set { _sStatus = value; }
    }
    public string NOTE 
    {
        get { return _sNote; }
        set { _sNote = value; }
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
    public DateTime CREATEDON
    {
        get { return _dtCreatedOn; }
        set { _dtCreatedOn = value; }
    }

}
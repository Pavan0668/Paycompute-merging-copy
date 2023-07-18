using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for msassignedtomebo
/// </summary>
public class msassignedtomebo
{
    public msassignedtomebo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string _strSuperviosrRoleID = string.Empty;
    private string _strEmployeeRoleID = string.Empty;
    private string _strSuperviosrID = string.Empty;
    private string _strEmployeeId = string.Empty;
    private string _strEmployeeEmaild = string.Empty;
    private string _strEmployeeName = string.Empty;
    private string _strSuperviosrName = string.Empty;
    private string _strSuperviosrEmailId = string.Empty;
    private string _strSuperviosrDesgination = string.Empty;
    private string _strEmployeeDesignation = string.Empty;
    private string _sPkey = string.Empty;
    private string _sComments = string.Empty;
    private bool _bIStatus;
    private DateTime _dtApprovedOn = DateTime.Now;
    private string _sApprovedBy = string.Empty;
    private string _strEntryStatus = string.Empty;
    private string _strchangeApproval = string.Empty;
    private string _strReview = string.Empty;
    private DateTime _dtLastActivityDate;
    private string _strPhn = string.Empty;

    public Guid Req_ID { get; set; }
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? RecCount { get; set; }
    public int? Flag { get; set; }
    public string Approver_Comment { get; set; }

    public string CHANGE_APPROVAL
    {
        get { return _strchangeApproval; }
        set { _strchangeApproval = value; }
    }
    public string REVIEW
    {
        get { return _strReview; }
        set { _strReview = value; }
    }
    public DateTime LAST_ACTIVITY_DATE
    {
        get { return _dtLastActivityDate; }
        set { _dtLastActivityDate = value; }
    }
    public string ENTRY_STATUS
    {
        get { return _strEntryStatus; }
        set { _strEntryStatus = value; }
    }
    public DateTime APPROVED_ON
    {
        get { return _dtApprovedOn; }
        set { _dtApprovedOn = value; }
    }
    public string APPROVED_BY
    {
        get { return _sApprovedBy; }
        set { _sApprovedBy = value; }
    }
    public bool STATUS
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
    public string S_PERNR
    {
        get { return _strSuperviosrID; }
        set { _strSuperviosrID = value; }
    }
    public string PERNR
    {
        get { return _strEmployeeId; }
        set { _strEmployeeId = value; }
    }
    public string ENAME
    {
        get { return _strEmployeeName; }
        set { _strEmployeeName = value; }
    }
    public string S_NAME
    {
        get { return _strSuperviosrName; }
        set { _strSuperviosrName = value; }
    }
    public string PLSXT
    {
        get { return _strEmployeeDesignation; }
        set { _strEmployeeDesignation = value; }
    }
    public string S_PLSXT
    {
        get { return _strSuperviosrDesgination; }
        set { _strSuperviosrDesgination = value; }
    }
    public string S_USRID
    {
        get { return _strSuperviosrEmailId; }
        set { _strSuperviosrEmailId = value; }
    }
    public string USRID
    {
        get { return _strEmployeeEmaild; }
        set { _strEmployeeEmaild = value; }
    }
    public string S_PLANS
    {
        get { return _strSuperviosrRoleID; }
        set { _strSuperviosrRoleID = value; }
    }
    public string PLANS
    {
        get { return _strEmployeeRoleID; }
        set { _strEmployeeRoleID = value; }
    }

    public string PHN
    {
        get { return _strPhn; }
        set { _strPhn = value; }
    }

    public int ID { get; set; }


    public DateTime MODIFIEDON { get; set; } 

    public string TableTyp { get; set; }

    public DateTime? MODON { get; set; }
    public DateTime? MMODON { get; set; }  
    public string AppByName { get; set; }

    public string Subtype { get; set; }
    public string EMPLOYEE_NO { get; set; }
    public string EMPLOYEE_NAME { get; set; }


    public string Manager { get; set; }
    public string Image { get; set; }
    public string DESIG { get; set; }
    public long? RowNumber { get; set; }
    public string ccode { get; set; }
    public DateTime? bdate { get; set; }
    public DateTime? edate { get; set; }
    public decimal? ttl { get; set; }
    public TimeSpan? stime { get; set; }
    public TimeSpan? etime { get; set; }
    public int? rid { get; set; }
    public DateTime? createdon { get; set; }
}
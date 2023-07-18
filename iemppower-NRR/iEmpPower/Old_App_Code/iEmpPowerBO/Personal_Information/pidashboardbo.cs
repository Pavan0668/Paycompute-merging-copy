using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for pidashboardbo
/// </summary>
public class pidashboardbo
{
    public pidashboardbo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string AppPERNR { get; set; }
    private string _strPkey = string.Empty;
    private string _strEmployeeId = string.Empty;
    private string _strManagerApproval = string.Empty;
    private string _strchangeApproval = string.Empty;
    private string _strReview = string.Empty;
    private DateTime _dtLastActivityDate;
    private string _strEntryStatus = string.Empty;

    private int? _PageIndex = 0;
    private int? _PageSize = 0;
    private int? _Total_Record = 0;

    public string ENTRY_STATUS
    {
        get { return _strEntryStatus; }
        set { _strEntryStatus = value; }
    }
    public string PKEY
    {
        get { return _strPkey; }
        set { _strPkey = value; }
    }
    public string PERNR
    {
        get { return _strEmployeeId; }
        set { _strEmployeeId = value; }
    }
    public string MANAGER_APPROVAL
    {
        get { return _strManagerApproval; }
        set { _strManagerApproval = value; }
    }
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

    public int? PageSize
    {
        get { return _PageSize; }
        set { _PageSize = value; }
    }
    public int? PageIndex
    {
        get { return _PageIndex; }
        set { _PageIndex = value; }
    }

    public int? Total_Record
    {

        get { return _Total_Record; }
        set { _Total_Record = value; }
    }
    public int ID { get; set; }
    public string AppByName { get; set; }
    public string TableTyp { get; set; }
    public DateTime MODIFIEDON { get; set; }
    public string Subtype { get; set; }
    public Int64? RowNumber { get; set; }
}
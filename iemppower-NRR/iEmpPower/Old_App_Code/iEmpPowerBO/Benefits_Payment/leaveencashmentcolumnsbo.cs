using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for leaveencashmentcolumnsbo
/// </summary>
public class leaveencashmentcolumnsbo
{
	public leaveencashmentcolumnsbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private int _iReferenceId = 0;
    private string _strPernr = string.Empty;
    private string _strLeaveType = string.Empty;
    private string _strLeaveTypeDesc = string.Empty;
    private decimal _fDaysOrHours = 0;
    private string _strCreatedBy = string.Empty;
    private DateTime _dtCreatedOn = Convert.ToDateTime("01/01/1900");
    private string _strModifiedBy = string.Empty;
    private DateTime _dtModifiedOn = Convert.ToDateTime("01/01/1900");
    private string _strCurrentStatus = string.Empty;
    private string _strOldLeaveType = string.Empty;
    private bool _is_Sup_Appr_Req = false;
    private bool _is_Hr_Appr_Req = false;
    private bool _is_PayrollAdmin_Appr_Req = false;
    private string _strApproved_By = string.Empty;
    private string _strEmpName = string.Empty;
    private string _strEmpMail = string.Empty;
    private DateTime _dtRequiredByDate = Convert.ToDateTime("01/01/1900");

    public int REFERENCE_ID
    {
        get { return _iReferenceId; }
        set { _iReferenceId = value; }
    }
    public string PERNR
    {
        get { return _strPernr; }
        set { _strPernr = value; }
    }
    public string LEAVE_TYPE
    {
        get { return _strLeaveType; }
        set { _strLeaveType = value; }
    }
    public decimal DAYS_OR_HOURS
    {
        get { return _fDaysOrHours; }
        set { _fDaysOrHours = value; }
    }
    public string CREATED_BY
    {
        get { return _strCreatedBy; }
        set { _strCreatedBy = value; }
    }
    public DateTime CREATED_ON
    {
        get { return _dtCreatedOn; }
        set { _dtCreatedOn = value; }
    }
    public string MODIFIED_BY
    {
        get { return _strModifiedBy; }
        set { _strModifiedBy = value; }
    }
    public DateTime MODIFIED_ON
    {
        get { return _dtModifiedOn; }
        set { _dtModifiedOn = value; }
    }
    public string CURRENT_STATUS
    {
        get { return _strCurrentStatus; }
        set { _strCurrentStatus = value; }
    }
    public string LEAVE_TYPE_DESC
    {
        get { return _strLeaveTypeDesc; }
        set { _strLeaveTypeDesc = value; }
    }
    public string OLD_LEAVE_TYPE
    {
        get { return _strOldLeaveType; }
        set { _strOldLeaveType = value; }
    }
    public bool IS_SUPERVISR_APPROVAL_REQ
    {
        get { return _is_Sup_Appr_Req; }
        set { _is_Sup_Appr_Req = value; }
    }
    public bool IS_HR_APPROVAL_REQ
    {
        get { return _is_Hr_Appr_Req; }
        set { _is_Hr_Appr_Req = value; }
    }
    public bool IS_PAYROLLADMIN_APPROVAL_REQ
    {
        get { return _is_PayrollAdmin_Appr_Req; }
        set { _is_PayrollAdmin_Appr_Req = value; }
    }
    public string APPROVED_BY
    {
        get { return _strApproved_By; }
        set { _strApproved_By = value; }
    }
    public string EMPLOYEE_NAME
    {
        get { return _strEmpName; }
        set { _strEmpName = value; }
    }
    public string EMPLOYEE_MAIL
    {
        get { return _strEmpMail; }
        set { _strEmpMail = value; }
    }

    public DateTime REQUIRED_BY_DATE
    {
        get { return _dtRequiredByDate; }
        set { _dtRequiredByDate = value; }
    }
}
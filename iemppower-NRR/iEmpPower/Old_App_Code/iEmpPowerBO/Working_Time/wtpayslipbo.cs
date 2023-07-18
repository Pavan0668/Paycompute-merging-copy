using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for wtpayslipbo
/// </summary>
public class wtpayslipbo
{
	public wtpayslipbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _iPERNR = string.Empty; // Personnel ID
    private string _strYear = string.Empty;
    private string _strMonth = string.Empty;
    private string _strPayslip = string.Empty;

    public string PERNR
    {
        get { return _iPERNR; }
        set { _iPERNR = value; }
    }
    public string YEAR
    {
        get { return _strYear; }
        set { _strYear = value; }
    }
    public string MONTH
    {
        get { return _strMonth; }
        set { _strMonth = value; }
    }
    public string PAYSLIP
    {
        get { return _strPayslip; }
        set { _strPayslip = value; }
    }
}
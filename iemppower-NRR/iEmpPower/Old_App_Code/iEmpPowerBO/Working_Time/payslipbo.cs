using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for payslipbo
/// </summary>
public class payslipbo
{
	public payslipbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private int _iPERNR = 0; // Personnel ID
    private string _strYear = string.Empty;
    private string _strMonth = string.Empty;
    private string _strPayslip = string.Empty;

    public int PERNR
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
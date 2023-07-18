using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for msemployeedetailsbo
/// </summary>
public class msemployeedetailsbo
{
    public msemployeedetailsbo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string _sEmployeeDetails = string.Empty;
    private string _sEmployeeId = string.Empty;
    private string _sbega = string.Empty;
    
    public string PERNR //Personal id
    {
        get { return _sEmployeeId; }
        set { _sEmployeeId = value; }
    }
    public string EMPLOYEE_DETAILS
    {
        get { return _sEmployeeDetails; }
        set { _sEmployeeDetails = value; }
    }

    public string BEGDA
    {
        get { return _sbega; }
        set { _sbega = value; }
    }
}
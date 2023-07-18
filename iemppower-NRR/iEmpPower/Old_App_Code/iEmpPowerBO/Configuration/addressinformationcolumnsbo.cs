using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for addressinformationcolumnsbo
/// </summary>
public class addressinformationcolumnsbo
{
	public addressinformationcolumnsbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string strPersonalNo = "PERNR"; //Employer ID Or Personal number
    private string strSubtype = "SUBTY"; //Subtype
    private string strValidFromDate = "BEGDA";//Valid from date
    private string strValidToDate = "ENDDA";//Valid To Date
    private string strAddressLine1 = "STRAS";//House number and street
    private string strAddressLine2 = "LOCAT";//2nd Address Line
    private string strRegion = "STATE";//Region
    private string strCity = "ORT01";//City
    private string strPostalCode = "PSTLZ";//Postal code
    private string strPhone = "TELNR";//Telephone number
    public string TELNR
    {
        get { return strPhone; }
    }
    public string PERNR
    {
        get { return strPersonalNo; }
    }
    public string SUBTY
    {
        get { return strSubtype; }
    }
    public string BEGDA
    {
        get { return strValidFromDate; }
    }
    public string ENDDA
    {
        get { return strValidToDate; }
    }
    public string STRAS
    {
        get { return strAddressLine1; }
    }
    public string LOCAT
    {
        get { return strAddressLine2; }
    }
    public string STATE
    {
        get { return strRegion; }
    }
    public string ORT01
    {
        get { return strCity; }
    }
    public string PSTLZ
    {
        get { return strPostalCode; }
    }
}
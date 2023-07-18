using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for personalidcolumnsbo
/// </summary>
public class personalidcolumnsbo
{
	public personalidcolumnsbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _sEmployeeId = "PERNR"; //Employer ID Or Personal number
    private string strIdType = "ICTYPE"; //ID type
    private string strIdNum = "ICNUM"; //ID number
    public string PERNR //Employer ID Or Personal number
    {
        get { return _sEmployeeId; }
    } 
    public string ICTYPE
    {
        get { return strIdType; }
    }
    public string ICNUM
    {
        get { return strIdNum; }
    }
}
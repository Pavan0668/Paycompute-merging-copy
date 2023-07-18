using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for appraisal_form
/// </summary>
public class appraisal_formbo
{
	public appraisal_formbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    //for drop down
    private string _strValueText;
    
    //for saving appraisal data
    private string _strAppraisalID;
    private string _strAppraisalName;
    private string _strElementName;
    private string _strValueTextDrp;
    private string _strTdLine;
    private string _strAppraiseeID;
    private string _strAppraiserID;
    private string _strStatusAppr;
    

    public string VALUE_TEXT
    {
        get { return _strValueText; }
        set { _strValueText = value; }
    }

     public string APPRAISAL_ID
    {
        get { return _strAppraisalID; }
        set { _strAppraisalID = value; }
    }

      public string APPRAISAL_NAME
    {
        get { return _strAppraisalName; }
        set { _strAppraisalName = value; }
    }

      public string ELEMENT_NAME
    {
        get { return _strElementName; }
        set { _strElementName = value; }
    }

      public string VALUE_TEXT_DRP
    {
        get { return _strValueTextDrp; }
        set { _strValueTextDrp = value; }
    }

      public string TDLINE
    {
        get { return _strTdLine; }
        set { _strTdLine = value; }
    }

      public string APPRAISEE_ID
    {
        get { return _strAppraiseeID; }
        set { _strAppraiseeID = value; }
    }

      public string APPRAISER_ID
    {
        get { return _strAppraiserID; }
        set { _strAppraiserID = value; }
    }

      public string STATUSAPPR
    {
        get { return _strStatusAppr; }
        set { _strStatusAppr = value; }
    }


}
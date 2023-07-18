using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PMSbo
/// </summary>
public class PMSbo
{
	public PMSbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}    

    //for apraisee
    private int _id;
    private string _strAppraisalID;
    private string _strAppraisalName;
    private string _strAppraisee_ID;
    private string _strAppraisee_Name;
    private string _strAppraiser_ID;
    private string _strAppraiser_Name;
    private string _strPart_ID1;
    private string _strPart_ID2;
    private string _strPart_ID3;
    private string _strPart_ID4;
    private string _strPart_ID5;
    private string _strPart_ID6;
    private string _strPart_ID7;
    private string _strOthers_ID;
    private DateTime _dtStartDate;
    private DateTime _dtEndDate;
    private int _iStatus;
    private int _iSub_Status;


    //for appraiser  
   
    public int id
    {
        get { return _id; }
        set { _id = value; }
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

    public string APPRAISEE_ID
    {
        get { return _strAppraisee_ID; }
        set { _strAppraisee_ID = value; }
    }

    public string APPRAISEE_NAME
    {
        get { return _strAppraisee_Name; }
        set { _strAppraisee_Name = value; }
    }

    public string APPRAISER_ID
    {
        get { return _strAppraiser_ID; }
        set { _strAppraiser_ID = value; }
    }

    public string APPRAISER_NAME
    {
        get { return _strAppraiser_Name; }
        set { _strAppraiser_Name = value; }
    }

    public string PART_ID1
    {
        get { return _strPart_ID1 ; }
        set { _strPart_ID1 = value; }
    }

    public string PART_ID2
    {
        get { return _strPart_ID2; }
        set { _strPart_ID2 = value; }
    }

    public string PART_ID3
    {
        get { return _strPart_ID3; }
        set { _strPart_ID3 = value; }
    }

    public string PART_ID4
    {
        get { return _strPart_ID4; }
        set { _strPart_ID4 = value; }
    }

    public string PART_ID5
    {
        get { return _strPart_ID5; }
        set { _strPart_ID5 = value; }
    }

    public string PART_ID6
    {
        get { return _strPart_ID6; }
        set { _strPart_ID6 = value; }
    }

    public string PART_ID7
    {
        get { return _strPart_ID7; }
        set { _strPart_ID7 = value; }
    }

    public string OTHERS_ID
    {
        get { return _strOthers_ID; }
        set { _strOthers_ID = value; }
    }

    public DateTime StartDate
    {
        get { return _dtStartDate; }
        set { _dtStartDate = value; }
    }

    public DateTime EndDate
    {
        get { return _dtEndDate; }
        set { _dtEndDate = value; }
    }

    public int STATUS
    {
        get { return _iStatus ; }
        set { _iStatus = value; }
    }

    public int SUBSTAT
    {
        get { return _iSub_Status; }
        set { _iSub_Status = value; }
    }
}
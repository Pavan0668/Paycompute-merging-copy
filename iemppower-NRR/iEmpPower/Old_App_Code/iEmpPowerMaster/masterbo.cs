using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for masterbo
/// </summary>
public class masterbo
{
	public masterbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int? pspnr { get; set; }
    private string _strLL = string.Empty;
    private string _sSBSEC = string.Empty; //Sub section code
    private string _sSBDIV = string.Empty; //Sub section division number
    private string _sSBDDS = string.Empty; //Division Description
    private string _iNationality = string.Empty;
    private string _sCountryId = string.Empty;
    private string _sCountryName = string.Empty;
    private string _sAddressId = string.Empty;
    private string _sAddressName = string.Empty;
    private string _sStateId = string.Empty;
    private string _sStateName = string.Empty;
    private string _sPersonalIdType = string.Empty;
    private string _sPersonalIdName = string.Empty;
    private string _sPersonalIdColour = string.Empty;
    private char _sNamePrefix;

    private string warestxt = string.Empty; // Currency code and text
    private string _sTitle = string.Empty;
    // object properties for Bank key
    private string _sBANKL = string.Empty; // Bank keys.
    private string _sBANKA = string.Empty; // Name of the bank.
    // object properties for Payment method
    private string _sZLSCH = string.Empty; // Payment method.
    private string _sTEXT1 = string.Empty; // Name of the payment method in language of the country.
    // Object properties for payment currency
    private string _sWAERS = string.Empty; // Currency key
    private string _sLTEXT = string.Empty; // Long text
    private string _sTC = string.Empty; //Time constraints
    // Object properties for language dropdown
    private string _strSPRSL = string.Empty; // Language key
    private string _strSPTXT = string.Empty; // Language name
    // Object properties for religion dropdown.
    private string _strKITXT = string.Empty; // Religious denomination.
    private string _strKTEXT = string.Empty; // Religious denomination long text.
    // Object properties for marital status dropdown.
    private string _strFAMST = string.Empty; // Marital status key.
    private string _strFTEXT = string.Empty; // Marital status.
    //object properties for attendence and absence dropdown
    private string _strAWART = string.Empty;
    private string _strATEXT = string.Empty;
    private string vernr;//Manager id for Project Code
    private string _strKOSTL = string.Empty; //cost center
    private string _strAUFNR = string.Empty; //Order






    private string _strRNPLNR = string.Empty; //Network
    private string _strActivityText = string.Empty;//Activity type TEXT
    private string _strLSTAR = string.Empty;//Activity type
    private string _strMCTXT = string.Empty;//Activity STEXT
    private string _strRPROJ = string.Empty;//WBS text
    private string _strPSPNR = string.Empty;//WBS No
    private string _strPOST1 = string.Empty;//WBS STEXT



    private string _strCcText = string.Empty;
    private string _strOrderText = string.Empty;
    private string _strKZTKT = string.Empty;
    private string _strTKTXT = string.Empty;

    //Variables for Travel Expenses report
    private string _strPROVIDER = string.Empty;
    private string _strNAME = string.Empty;
    private string _strBEREI = string.Empty;
    private string _strTEXT25 = string.Empty;
    private string _strPKWKL = string.Empty;
    private string _strKZREA = string.Empty;
    private string _strRETXT = string.Empty;
    private string _strKZPMF = string.Empty;
    private string _strFZTXT = string.Empty;
    private string _strSPKZL = string.Empty;
    private string matnr = string.Empty;//Item Id;
    private string verna = string.Empty;//Name of Manager Project Code
    private string maktx = string.Empty;//item desc 
    private string btext = string.Empty;//Sub Function PR
    private string adesc = string.Empty;//MIS GROUP A DESCRIPTION
    private string func_area = string.Empty;// Main Function PR
    private string bdesc = string.Empty;//MIS GROUP B DESCRIPTION
    private string meins = string.Empty;//unit of measurements
    private string isocode = string.Empty;
    private string isotxt = string.Empty;
    private string ekgrp = string.Empty;//Requestor Region ID PR
    private string eknam = string.Empty;// Requestor Region PR

    private string region_id = string.Empty;
    private string region_text = string.Empty;

    private string wbsid = string.Empty;
    private string project = string.Empty;
    private string wbs = string.Empty;

    private string cid = string.Empty;//MIS GROUP C ID
    private string cdesc = string.Empty;//MIS GROUP C DESCRIPTION
    private string lgart = string.Empty;//Expense Type ID
    private string lgtxt = string.Empty;//Expense Type Text
    private string posid = string.Empty;
    private string spart = string.Empty;//business unit id
    private string werks = string.Empty;//Bill and Ship to address id PR
    private string name1 = string.Empty; // Bill and Ship to address text PR
    private string vtext = string.Empty;//business unit Name
    private string region = string.Empty;
    //Properties for Travel Expenses Report
    public string PROVIDER
    {
        get { return _strPROVIDER; }
        set { _strPROVIDER = value; }
    }

    public string NAME
    {
        get { return _strNAME; }
        set { _strNAME = value; }
    }

    public string BEREI
    {
        get { return _strBEREI; }
        set { _strBEREI = value; }
    }
    public string RGION
    {
        get { return region; }
        set { region = value; }
    }
    public string TEXT25
    {
        get { return _strTEXT25; }
        set { _strTEXT25 = value; }
    }

    public string PKWKL
    {
        get { return _strPKWKL; }
        set { _strPKWKL = value; }
    }

    public string KZREA
    {
        get { return _strKZREA; }
        set { _strKZREA = value; }
    }

    public string RETXT
    {
        get { return _strRETXT; }
        set { _strRETXT = value; }
    }

    public string KZPMF
    {
        get { return _strKZPMF; }
        set { _strKZPMF = value; }
    }

    public string FZTXT
    {
        get { return _strFZTXT; }
        set { _strFZTXT = value; }
    }

    public string SPKZL
    {
        get { return _strSPKZL; }
        set { _strSPKZL = value; }
    }
    public string MATNR
    {
        get { return matnr; }
        set { matnr = value; }
    }
    public string VERNR
    {
        get { return vernr; }
        set { vernr = value; }
    }
    //Cost center
    public string AUFNR 
    {
        get { return _strAUFNR; }
        set { _strAUFNR = value; }
    }
    //Internal order
    public string KOSTL
    {
        get { return _strKOSTL; }
        set { _strKOSTL = value; }
    }

    public string SBSEC //Title
    {
        get { return _sSBSEC; }
        set { _sSBSEC = value; }
    }

    public string SBDIV //Title
    {
        get { return _sSBDIV; }
        set { _sSBDIV = value; }
    }
    public string SBDDS //Name prefix
    {
        get { return _sSBDDS; }
        set { _sSBDDS = value; }
    }

    public string TC //Title
    {
        get { return _sTC; }
        set { _sTC = value; }
    }

    public string TTOUT //Title
    {
        get { return _sTitle; }
        set { _sTitle = value; }
    }
    public char ART //Name prefix
    {
        get { return _sNamePrefix; }
        set { _sNamePrefix = value; }
    }
    public string LAND1 //Country Id
    {
        get { return _sCountryId; }
        set { _sCountryId = value; }
    }
    public string LANDX // Country name
    {
        get { return _sCountryName; }
        set { _sCountryName = value; }
    }
    public string NATIO //Nationality
    {
        get { return _iNationality; }
        set { _iNationality = value; }
    }
    public string SUBTY //Address type id and family type id
    {
        get { return _sAddressId; }
        set { _sAddressId = value; }
    }
    public string STEXT //Address type name and family type name
    {
        get { return _sAddressName; }
        set { _sAddressName = value; }
    }
    public string BLAND // State ID
    {
        get { return _sStateId; }
        set { _sStateId = value; }
    }
    public string BEZEI //State name
    {
        get { return _sStateName; }
        set { _sStateName = value; }
    }
    public string ICTYP // ID type
    {
        get { return _sPersonalIdType; }
        set { _sPersonalIdType = value; }
    }
    public string ICTXT //ID type name/text
    {
        get { return _sPersonalIdName; }
        set { _sPersonalIdName = value; }
    }
    public string ICCOL //ID type colour
    {
        get { return _sPersonalIdColour; }
        set { _sPersonalIdColour = value; }
    }
    // object properties for Bank key dropdown
    public string BANKL
    {
        get { return _sBANKL; }
        set { _sBANKL = value; }
    }
    public string BANKA
    {
        get { return _sBANKA; }
        set { _sBANKA = value; }
    }
    // object properties for payment method
    public string ZLSCH
    {
        get { return _sZLSCH; }
        set { _sZLSCH = value; }
    }
    public string TEXT1
    {
        get { return _sTEXT1; }
        set { _sTEXT1 = value; }
    }
    // object properties for payment currency
    public string WAERS
    {
        get { return _sWAERS; }
        set { _sWAERS = value; }
    }
    public string LTEXT
    {
        get { return _sLTEXT; }
        set { _sLTEXT = value; }
    }
    // Object properties for language dropdown
    // Language key
    public string SPRSL
    {
        get { return _strSPRSL; }
        set { _strSPRSL = value; }
    }
    // Language name
    public string SPTXT
    {
        get { return _strSPTXT; }
        set { _strSPTXT = value; }
    }
    // Object properties for Religion dropdown
    // Religious denomination key
    public string KITXT
    {
        get { return _strKITXT; }
        set { _strKITXT = value; }
    }
    // Relidious denomination long text.
    public string KTEXT
    {
        get { return _strKTEXT; }
        set { _strKTEXT = value; }
    }
    // Object properties for marital status dropdown.
    // Marital status key.
    public string FAMST
    {
        get { return _strFAMST; }
        set { _strFAMST = value; }
    }
    // Marital status
    public string FTEXT
    {
        get { return _strFTEXT; }
        set { _strFTEXT = value; }
    }

    //object properties for attendence and absence dropdown
    public string AWART // attendence or absent type
    {
        get { return _strAWART; }
        set { _strAWART = value; }
    }
    public string ATEXT // text for attendence or absent type
    {
        get { return _strATEXT; }
        set { _strATEXT = value; }
    }
    public string CC_TEXT
    {
        get { return _strCcText; }
        set { _strCcText = value; }
    }
    public string ORDER_TEXT
    {
        get { return _strOrderText; }
        set { _strOrderText = value; }
    }
    public string LL
    {
        get { return _strLL; }
        set { _strLL = value; }
    }

    public string ACTIVITY_TYPE
    {
        get
        { return _strKZTKT; }
        set
        { _strKZTKT = value; }
    }

    public string ACTIVITY
    {
        get { return _strTKTXT; }
        set { _strTKTXT = value; }
    }


    //Network
    public string RNPLNR
    {
        get { return _strRNPLNR; }
        set { _strRNPLNR = value; }
    }

    //Activity type 
    public string Activitytype
    {
        get { return _strActivityText; }
        set { _strActivityText = value; }
    }

    //Activity type No
    public string LSTAR
    {
        get { return _strLSTAR; }
        set { _strLSTAR = value; }
    }

    //Activity type STEXT
    public string MCTXT
    {
        get { return _strMCTXT; }
        set { _strMCTXT = value; }
    }

    //WBS
    public string RPROJ
    {
        get { return _strRPROJ; }
        set { _strRPROJ = value; }
    }

    //WBS No
    public string PSPNR
    {
        get { return _strPSPNR; }
        set { _strPSPNR = value; }
    }

    //WBS STEXT
    public string POST1
    {
        get { return _strPOST1; }
        set { _strPOST1 = value; }
    }

    public long? R_NUM
    {
        get;
        set;
    }
    public string T_Path
    {
        get;
        set;
    }

    public string T_icon
    {
        get;
        set;
    }

    public string T_Name
    {
        get;
        set;
    }

    public string PERNR
    {
        get;
        set;
    }
    public string VERNA
    {
        get { return verna; }
        set { verna = value; }
    }
    public string MAKTX
    {
        get { return maktx; }
        set { maktx = value; }
    }
    public string BTEXT
    {
        get { return btext; }
        set { btext = value; }
    }
    public string A_DESC
    {
        get { return adesc; }
        set { adesc = value; }
    }

    public string FUNC_AREA
    {
        get { return func_area; }
        set { func_area = value; }
    }
    public string B_DESC
    {
        get { return bdesc; }
        set { bdesc = value; }
    }
    public string MEINS
    {
        get { return meins; }
        set { meins = value; }
    }
    public string ISOCODE
    {
        get { return isocode; }
        set { isocode = value; }
    }
    public string ISOTXT
    {
        get { return isotxt; }
        set { isotxt = value; }
    }
    public string EKGRP
    {
        get { return ekgrp; }
        set { ekgrp = value; }
    }
    public string EKNAM
    {
        get { return eknam; }
        set { eknam = value; }
    }
    public string REGION_ID
    {
        get { return region_id; }
        set { region_id = value; }
    }
    public string REGION_TEXT
    {
        get { return region_text; }
        set { region_text = value; }
    }

    public string WARESTXT
    {
        get { return warestxt; }
        set { warestxt = value; }
    }
    public string WBSID
    {
        get { return wbsid; }
        set { wbsid = value; }
    }
    public string PROJECT
    {
        get { return project; }
        set { project = value; }
    }

    public string WBS
    {
        get { return wbs; }
        set { wbs = value; }
    }
    public string CID
    {
        get { return cid; }
        set { cid = value; }
    }
    public string C_DESC
    {
        get { return cdesc; }
        set { cdesc = value; }
    }
    public string LGART
    {
        get { return lgart; }
        set { lgart = value; }
    }

    public string LGTXT
    {
        get { return lgtxt; }
        set { lgtxt = value; }
    }
    public string POSID
    {
        get { return posid; }
        set { posid = value; }
    }
    public string SPART
    {
        get { return spart; }
        set { spart = value; }
    }
    public string WERKS
    {
        get { return werks; }
        set { werks = value; }
    }
    public string NAME1
    {
        get { return name1; }
        set { name1 = value; }
    }
    public string VTEXT
    {
        get { return vtext; }
        set { vtext = value; }
    }


    public int? id { get; set; }

}
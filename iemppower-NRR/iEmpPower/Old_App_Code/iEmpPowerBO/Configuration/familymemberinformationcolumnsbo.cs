using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for familymemberinformationcolumnsbo
/// </summary>
public class familymemberinformationcolumnsbo
{
    public familymemberinformationcolumnsbo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string _sEmployeeId = "PERNR"; //Employer ID Or Personal number
    private string _sMemberTypeId = "FAMSA"; //Type of family record 
    private string _sFName = "FAVOR"; //First name
    private string _sLName = "FANAM";  //Last name
    private string _sNameofBirth = "FGBNA"; //Name of birth
    private string _sInitials = "FINIT"; //Employee's Initials
    private string _sOtherTitle = "FNMZU"; //OtherTitle
    private string _sNamePrefix = "FVRSW"; //Name Prefix
    private string _sGender = "FASEX"; //Gender key
    private string _sDateOfBirth = "FGBDT"; //Date of birth
    private string _sPlaceOfBirth = "FGBOT"; //Place of birth
    private string _sCountryOfBirth = "FGBLD"; //Country of Birth
    private string _sNationality = "FANAT"; //Nationality
    private string _sNationality2 = "FANA2"; //Second Nationality
    private string _sNationality3 = "FANA3"; //Third Nationality
    public string PERNR //Employer ID Or Personal number
    {
        get { return _sEmployeeId; }
    } 
    public string FAMSA //Type of family record 
    {
        get { return _sMemberTypeId; }
    }
    public string FAVOR //First name
    {
        get { return _sFName; }
    }
    public string FANAM  //Last name
    {
        get { return _sLName; }
    }
    public string FGBNA //Name of birth
    {
        get { return _sNameofBirth; }
    }
    public string FINIT //Employee's Initials
    {
        get { return _sInitials; }
    }
    public string FNMZU //OtherTitle
    {
        get { return _sOtherTitle; }
    }
    public string FVRSW //Name Prefix   
    {
        get { return _sNamePrefix; }
    }
    public string FASEX //Gender key
    {
        get { return _sGender; }
    }
    public string FGBDT //Date of birth
    {
        get { return _sDateOfBirth; }
    }
    public string FGBOT //Place of birth
    {
        get { return _sPlaceOfBirth; }
    }
    public string FGBLD //Country of Birth
    {
        get { return _sCountryOfBirth; }
    }
    public string FANAT //Nationality
    {
        get { return _sNationality; }
    }
    public string FANA2 //Second Nationality
    {
        get { return _sNationality2; }
    }
    public string FANA3 //Third Nationality
    {
        get { return _sNationality3; }
    }
}
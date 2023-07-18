using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

/// <summary>
/// Summary description for personaldatacolumnsbo
/// </summary>
public class personaldatacolumnsbo
{
    public personaldatacolumnsbo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string _sEmployeeId = "PERNR"; //Employer ID Or Personal number
    private string _sLanguage = "SPRSL"; //Language key
    private string _sFName = "VORNA"; //First name
    private string _sLName = "NACHN";  //Last name
    private string _sNameofBirth = "NAME2"; //Name of birth
    private string _sInitials = "INITS"; //Employee's Initials
    private string _sTitle = "TITEL"; //OtherTitle
    private string _sKnownAs = "RUFNM"; //Known as
    private string _sGender = "GESCH"; //Gender key
    private string _sDateOfBirth = "GBDAT"; //Date of birth
    private string _sPlaceOfBirth = "GBORT"; //Place of birth
    private string _sCountryOfBirth = "GBLND"; //Country of Birth
    private string _sNationality = "NATIO"; //Nationality
    private string _sNationality2 = "NATI2"; //Second Nationality
    private string _sNationality3 = "NATI3"; //Third Nationality
    private string _sState = "GBDEP"; //State
    private string _sMaritalStatus = "FAMST"; //Marital status key
    private string _sValidMaritalStatus = "FAMDT"; //Valid from date of current marital status
    private string _sNoOfChildren = "ANZKD"; //Number of children
    private string _sReligious = "KITXT"; //Religious denomination key
   
    public string PERNR //Employer ID Or Personal number
    {
        get { return _sEmployeeId; }
    }
    public string SPRSL //Language key
    {
        get { return _sLanguage; }
    }
    public string VORNA //First name
    {
        get { return _sFName; }
    }
    public string NACHN  //Last name
    {
        get { return _sLName; }
    }
    public string NAME2 //Name of birth
    {
        get { return _sNameofBirth; }
    }
    public string INITS //Employee's Initials
    {
        get { return _sInitials; }
    }
    public string TITEL //Title
    {
        get { return _sTitle; }
    }
    public string RUFNM //Known as   
    {
        get { return _sKnownAs; }
    }
    public string GESCH //Gender key
    {
        get { return _sGender; }
    }
    public string GBDAT //Date of birth
    {
        get { return _sDateOfBirth; }
    }
    public string GBORT //Place of birth
    {
        get { return _sPlaceOfBirth; }
    }
    public string GBLND //Country of Birth
    {
        get { return _sCountryOfBirth; }
    }
    public string NATIO //Nationality
    {
        get { return _sNationality; }
    }
    public string NATI2 //Second Nationality
    {
        get { return _sNationality2; }
    }
    public string NATI3 //Third Nationality
    {
        get { return _sNationality3; }
    }
    public string GBDEP //State
    {
        get { return _sState; }
    }
    public string FAMST //Marital status key
    {
        get { return _sMaritalStatus; }
    }
    public string FAMDT //Valid from date of current marital status
    {
        get { return _sValidMaritalStatus; }
    }
    public string ANZKD //Number of children
    {
        get { return _sNoOfChildren; }
    }
    public string KITXT //Religious denomination key
    {
        get { return _sReligious; }
    }
}
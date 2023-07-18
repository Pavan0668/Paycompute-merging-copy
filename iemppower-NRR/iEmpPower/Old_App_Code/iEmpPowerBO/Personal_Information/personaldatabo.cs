using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

/// <summary>
/// Summary description for personaldatabo
/// </summary>
public class personaldatabo
{
    public personaldatabo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //private string _sEmployeeId = string.Empty; // Personnel ID
    //private string _iPERNR = string.Empty; // Personnel ID
    //private string _strTITEL = string.Empty; // Title
    //private string _strTITEL_Text = string.Empty; // Title
    //private string _strVORNA = string.Empty; // First name
    //private string _strNACHN = string.Empty; // Last name
    //private string _strNAME2 = string.Empty; // Name at birth
    //private string _strINITS = string.Empty; // Initials
    //private string _strRUFNM = string.Empty; // Nick name(Known as) 
    //private string _strSPRSL = string.Empty; // Communication language
    //private string _strGESCH = string.Empty; // Gender key
    //private string _Gender = string.Empty; // Gender key
    //private DateTime? _dtGBDAT; // Date of birth
    //private string _strGBORT = string.Empty;// Birth place
    //private string _strGBLND = string.Empty; // Country of birth
    //private string _strNATIO = string.Empty; // Nationality
    //private string _strNATIO_Txt = string.Empty; // Nationality
    //private string _strGBDEP = string.Empty; // State
    //private string _strNATI2 = string.Empty; // Second nationality
    //private string _strNATI3 = string.Empty; // Third nationality
    //private string _strFAMST = string.Empty; // Marital status key
    //private string _strFAMST_Text = string.Empty; // Marital status key
    //private DateTime? _strFAMDT; //  Valid from date of current marital status.
    //private Int16? _iANZKD = 0; // Number of children
    //private string _strKITXT = string.Empty; // Religious denomination key.
    //private string _strSPTXT = string.Empty; // Language name
    //private string _strBEZEI = string.Empty; // Region(State, province,county)
    //private bool _bIsDefalutStatus;
    //private string _sDescription;
    //private bool _bIsActive = false;
    //private bool _bIsApproved = false;
    //private string _sComments = string.Empty;
    //private string _bIStatus;
    //private bool _bIsUpdate = false;
    //private Binary _iLogo;
    //private string _sEmployeePath = string.Empty;
    //private Guid _ID = new Guid();
    //private int? _Flag = 0;
    //private string _Photo = string.Empty;
    //private string _Konfe = string.Empty;
    //private DateTime? _Begda;
    //private DateTime? _Endda;
    //private string _Landx = string.Empty;
    //private string _Landx2 = string.Empty;
    //private string _Landx3 = string.Empty;
    //private string _Nati2 = string.Empty;
    //private string _Nati3 = string.Empty;
    //private string _Bland = string.Empty;
    //private string _Konfe_Text = string.Empty;
    //private DateTime _dCreatedOn;
    //private string _sCreatedBy = string.Empty;
    //private DateTime _dApprovedOn;
    //private string _sApprovedBy = string.Empty;
    //private DateTime _dModifiedOn;
    //private string _sModifiedBy = string.Empty;


    public string EMPLOYEE_PHOTOPATH { get; set; }
    public Binary LOGO { get; set; }
    public string EMPLOYEE_ID { get; set; }
    public bool ISUPDATE { get; set; }
    public string STATUS { get; set; }
    public string COMMENTS { get; set; }
    public string DESCRIPTION { get; set; }
    public bool VALUE { get; set; } // Personnel ID
    public string PERNR { get; set; } // Title
    public string TITEL { get; set; } // Title Text
    public string TITEL_TEXT { get; set; } // First name
    public string VORNA { get; set; } // Last name
    public string NACHN { get; set; } // Name at birth
    public string NAME2 { get; set; } // Initials
    public string INITS { get; set; } // Nick name(Known as)
    public string RUFNM { get; set; }
    public string SPRSL { get; set; } // Communication language
    public string GESCH { get; set; } // Gender key
    public string Gender { get; set; }
    public DateTime? GBDAT { get; set; } // Date of birth
    public string GBORT { get; set; } // Birth place 
    public string GBLND { get; set; } // Country of birth
    public string NATIO { get; set; }
    public string NATIO_Txt { get; set; } // Nationality
    public string GBDEP { get; set; } // State
    public string NATI2 { get; set; } // Second nationality
    public string NATI3 { get; set; } // Third nationality
    public string FAMST { get; set; } // Marital status key
    public string FAMST_Text { get; set; }// Marital status key   
    public DateTime? FAMDT { get; set; }  //  Valid from date of current marital status.
    public Int16? ANZKD { get; set; }    // Number of children    
    public string KITXT { get; set; }// Religious denomination key.    
    public string SPTXT { get; set; }// Language name    
    public string BEZEI { get; set; }// Region(State,province,county)
    public bool ISAPPROVED { get; set; }
    public DateTime CREATED_ON { get; set; }
    public DateTime APPROVED_ON { get; set; }
    public string CREATED_BY { get; set; }
    public string APPROVED_BY { get; set; }
    public DateTime MODIFIEDON { get; set; }
    public string MODIFIED_BY { get; set; }
    public bool ISACTIVE { get; set; }
    public int? ID { get; set; }
    public int? Flag { get; set; }
    public string PHOTO { get; set; }
    public string KONFE { get; set; }
    public string KONFE_Txt { get; set; }
    public DateTime? BEGDA { get; set; }
    public DateTime? ENDDA { get; set; }
    public string LANDX { get; set; }
    public string LANDX2 { get; set; }
    public string LANDX3 { get; set; }
    public string BLAND { get; set; }
    public string PKEY { get; set; }



    public string TRANSSTATUS { get; set; }


    public string ENAME { get; set; }
    public string PARTICULARS { get; set; }

    public string SUPERVISOREID { get; set; }



    public string SUPERVISOR_NAME { get; set; }

    public string EMPFNAME { get; set; }

    public string EMPLNAME { get; set; }



    public string CHANGE_APPROVAL { get; set; }

    public string TEXT { get; set; }
    public string VALUES { get; set; }

    public string Slno { get; set; }

    public DateTime? MODON { get; set; } 
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class personal_informationBO
{
    public personal_informationBO()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int flag { get; set; }
    public int ID { get; set; }
    public string Comany_Code { get; set; }
    public string EmpID { get; set; }
    public string F_Name  { get; set; }
    public string M_Name  { get; set; }
    public string L_Name  { get; set; }
    public int Gender  { get; set; }
    public DateTime	DOB  { get; set; }
    public string Material_Status  { get; set; }
    public string Material_StatusTxt { get; set; }
    public string Father_Name  { get; set; }
    public string Mother_Name  { get; set; }
    public string Spouse_Name  { get; set; }
    public string Created_By { get; set; }
    public DateTime? Created_On { get; set; }
    public string Approved_By { get; set; }
    public DateTime? Approved_On { get; set; }
    public int Salutation { get; set; }
    public string Updated_By { get; set; }
    public DateTime Updated_On { get; set; }
    public string Emp_Photo { get; set; }
	
    public int Address_Type	 { get; set; }
    public string ResNo	 { get; set; }
    public string Street { get; set; }
    public string Locality	 { get; set; }
    public string District	 { get; set; }
    public string State	 { get; set; }
    public string Country { get; set; }
    public decimal? Pincode { get; set; }
    public int? STD_code	 { get; set; }
    public int? Ward_Num	 { get; set; }

    public string Address_Typetxt { get; set; }
    public string StateTxt { get; set; }
    public string CountryTxt { get; set; }

    public string Bank_Name	 { get; set; }
    public decimal Acc_Num	 { get; set; }
    public string IFSC_Code	 { get; set; }
    public string Bank_Branch { get; set; }	

    public bool ESI_Applicable { get; set; }
    public decimal? ESI_Num { get; set; }	
    public string ESI_Dispencary { get; set; }	
    public bool PF_Applicable { get; set; }	
    public string PF_Num { get; set; }	
    public string PF_Num_Dept_File	 { get; set; }	
    public bool Restrict_PF { get; set; }	
    public bool Zero_Pension { get; set; }
    public bool Zero_PT { get; set; }	

    public int Contact_Type	 { get; set; }
    public string Contact_Type_ID { get; set; }
    public string Contact_Type_TXT { get; set; }

    public int Doc_Type { get; set; }
    public string Doc_Type_ID { get; set; }
    public string Doc_Type_TXT { get; set; }

    public int? code { get; set; }

    public DateTime? startdate { get; set; }
    public DateTime? enddate { get; set; }
    public string docpath { get; set; }

}

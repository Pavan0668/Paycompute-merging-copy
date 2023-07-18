using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;

public class personal_informationBl
{
    personal_informationDALDataContext objPersonalDataContext = new personal_informationDALDataContext();

    public personal_informationCollBo Get_EmpInfo(personal_informationBO objPersonalBo)
    {
        personal_informationCollBo objPersonaldataList = new personal_informationCollBo();
        foreach (var vRow in objPersonalDataContext.payc_pi_load_Employee_info(objPersonalBo.Comany_Code, objPersonalBo.EmpID, objPersonalBo.Approved_By, objPersonalBo.flag))
        {
            personal_informationBO objBo = new personal_informationBO();
            objBo.ID = vRow.ID;
            objBo.EmpID = vRow.EmpID;
            //objBo.Comany_Code = vRow.Comany_Code;
            objBo.Emp_Photo = vRow.Emp_Photo;
            objBo.F_Name = vRow.F_Name;
            objBo.M_Name = vRow.L_Name;
            objBo.L_Name = vRow.M_Name;
            objBo.Gender = vRow.Gender;
            objBo.DOB = vRow.DOB;
            objBo.Material_Status = vRow.Material_Status;
            objBo.Material_StatusTxt = vRow.material_ststText;
            objBo.Father_Name = vRow.Father_Name;
            objBo.Mother_Name = vRow.Mother_Name; 
            objBo.Spouse_Name = vRow.Spouse_Name;
            objBo.Created_By = vRow.Created_By;
            objBo.Created_On = vRow.Created_On;
            objBo.Approved_By = vRow.Approved_By;
            objBo.Approved_On = vRow.Approved_On;
            objBo.Salutation = vRow.Salutation;
            objPersonaldataList.Add(objBo);
        }
        return objPersonaldataList;
    }


    public personal_informationCollBo Get_EmpDocsInfo(personal_informationBO objPersonalBo)
    {
        personal_informationCollBo objPersonaldataList = new personal_informationCollBo();
        foreach (var vRow in objPersonalDataContext.payc_pi_load_Employee_Document_info(objPersonalBo.Comany_Code, objPersonalBo.EmpID, objPersonalBo.Approved_By, objPersonalBo.flag))
        {
            personal_informationBO objBo = new personal_informationBO();
            objBo.ID = vRow.ID;
            objBo.Doc_Type = vRow.Doc_Types;
            objBo.Doc_Type_ID= vRow.Doc_Type_ID;
            objBo.Doc_Type_TXT = vRow.Doc_tye_text;
            objBo.Created_By = vRow.Created_By;
            objBo.Created_On = vRow.Created_On;
            objBo.Approved_By = vRow.Approved_By;
            objBo.Approved_On = vRow.Approved_On;
            objBo.docpath = vRow.Document_Path;
            objPersonaldataList.Add(objBo);
        }
        return objPersonaldataList;
    }

    public personal_informationCollBo Get_EmpContactsInfo(personal_informationBO objPersonalBo)
    {
        personal_informationCollBo objPersonaldataList = new personal_informationCollBo();
        foreach (var vRow in objPersonalDataContext.payc_pi_load_Employee_Contact_info(objPersonalBo.Comany_Code, objPersonalBo.EmpID, objPersonalBo.Approved_By, objPersonalBo.flag))
        {
            personal_informationBO objBo = new personal_informationBO();
            objBo.ID = vRow.ID;
            objBo.Contact_Type = vRow.Contact_Type;
            objBo.Contact_Type_ID= vRow.Contact_Type_ID;
            objBo.Contact_Type_TXT= vRow.Contact_tye_text;
            objBo.Created_By = vRow.Created_By;
            objBo.Created_On = vRow.Created_On;
            objBo.Approved_By = vRow.Approved_By;
            objBo.Approved_On = vRow.Approved_On;
            objPersonaldataList.Add(objBo);
        }
        return objPersonaldataList;
    }

    public personal_informationCollBo Get_EmpAddressInfo(personal_informationBO objPersonalBo)
    {
        personal_informationCollBo objPersonaldataList = new personal_informationCollBo();
        foreach (var vRow in objPersonalDataContext.payc_pi_load_Employee_Address_info(objPersonalBo.Comany_Code, objPersonalBo.EmpID, objPersonalBo.Approved_By, objPersonalBo.flag))
        {
            personal_informationBO objBo = new personal_informationBO();
            objBo.ID = vRow.ID;
            objBo.Address_Type = vRow.Address_Type;
            objBo.Address_Typetxt = vRow.addr_tye_text;
            objBo.ResNo = vRow.ResNo + " " + objBo.Street;
            objBo.Street= vRow.Street;
            objBo.Locality=vRow.Locality;
            objBo.District=vRow.District;
            objBo.State=vRow.State;
            objBo.Country=vRow.Country;
            objBo.CountryTxt = vRow.country_text;
            objBo.StateTxt=vRow.state_text;
            objBo.STD_code = vRow.STD_code == 0 ? null : vRow.STD_code;
            objBo.Ward_Num = vRow.Ward_Num == 0 ? null : vRow.Ward_Num;
            //objBo.Pincode = vRow.Pincode;
            objBo.Pincode = vRow.Pincode == 0 ? null : vRow.Pincode; ;
            objBo.Created_By = vRow.Created_By;
            objBo.Created_On = vRow.Created_On;
            objBo.Approved_By = vRow.Approved_By;
            objBo.Approved_On = vRow.Approved_On;
            objBo.startdate = vRow.Start_Date;
            objBo.enddate = vRow.End_Date;
            objPersonaldataList.Add(objBo);
        }
        return objPersonaldataList;
    }

    public personal_informationCollBo Get_EmpBankInfo(personal_informationBO objPersonalBo)
    {
        personal_informationCollBo objPersonaldataList = new personal_informationCollBo();
        foreach (var vRow in objPersonalDataContext.payc_pi_load_Employee_Bank_info(objPersonalBo.Comany_Code, objPersonalBo.EmpID, objPersonalBo.Approved_By, objPersonalBo.flag))
        {
            personal_informationBO objBo = new personal_informationBO();
            objBo.ID = vRow.ID;
            objBo.Bank_Name = vRow.Bank_Name;
            objBo.F_Name = vRow.Acc_Num;
            objBo.IFSC_Code= vRow.IFSC_Code;
            objBo.Bank_Branch= vRow.Bank_Branch;
            objBo.District=vRow.Bank_District;
            objBo.State=vRow.Branch_State;
            objBo.Country=vRow.Branch_Country;
            objBo.CountryTxt=vRow.country_text;
            objBo.StateTxt=vRow.State_text;
            objBo.Created_By = vRow.Created_By;
            objBo.Created_On = vRow.Created_On;
            objBo.Approved_By = vRow.Approved_By;
            objBo.Approved_On = vRow.Approved_On;
            objPersonaldataList.Add(objBo);
        }
        return objPersonaldataList;
    }

    public personal_informationCollBo Get_EmpBenefitsInfo(personal_informationBO objPersonalBo)
    {
        personal_informationCollBo objPersonaldataList = new personal_informationCollBo();
        foreach (var vRow in objPersonalDataContext.payc_pi_load_Employee_Benefits_info(objPersonalBo.Comany_Code, objPersonalBo.EmpID, objPersonalBo.Approved_By, objPersonalBo.flag))
        {
            personal_informationBO objBo = new personal_informationBO();
            objBo.ID = vRow.ID;
            objBo.ESI_Applicable = vRow.ESI_Applicable;
            objBo.M_Name = vRow.ESI_Num;
            objBo.ESI_Dispencary = vRow.ESI_Dispencary;
            objBo.PF_Applicable = vRow.PF_Applicable;
            objBo.PF_Num = vRow.PF_Num;
            objBo.PF_Num_Dept_File = vRow.PF_Num_Dept_File;
            objBo.Restrict_PF = vRow.Restrict_PF;
            objBo.Zero_Pension = vRow.Zero_Pension;
            objBo.Zero_PT = vRow.Zero_PT;
            objBo.Created_By = vRow.Created_By;
            objBo.Created_On = vRow.Created_On;
            objBo.Approved_By = vRow.Approved_By;
            objBo.Approved_On = vRow.Approved_On;
            objPersonaldataList.Add(objBo);
        }
        return objPersonaldataList;
    }

}

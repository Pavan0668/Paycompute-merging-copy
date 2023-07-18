using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.EmpData;
using iEmpPower.Old_App_Code.iEmpPowerBO.EmpData.EmpCollBo;


public class EmpDataBL
{
    EmpDataDALDataContext objDataContext = new EmpDataDALDataContext();
    public int Create_EmpInfo(EmoDataBo objempBo, ref bool? status)
    {
        try
        {
            objDataContext = new EmpDataDALDataContext();
            int iResultCode = objDataContext.payc_create_update_empInfo(
                  objempBo.compCode
                , objempBo.Employee_ID
                , objempBo.Salutation
                , objempBo.First_Name
                , objempBo.Middle_Name
                , objempBo.Last_Name
                , objempBo.Gender
                , objempBo.Date_of_Birth
                , objempBo.Material_Status
                , objempBo.Father_Name
                , objempBo.Mother_Name
                , objempBo.Spouse_Name
                , objempBo.Bank_Name
                , objempBo.Account_Number
                , objempBo.IFSC_Code
                , objempBo.Bank_Branch
                , objempBo.Bank_District
                , objempBo.Branch_Country
                , objempBo.Branch_State
                , objempBo.Address_Type
                , objempBo.Residence_Number
                 , objempBo.Street
                , objempBo.Locality
                , objempBo.District
                , objempBo.Country
                , objempBo.State
                , objempBo.Pincode
                , objempBo.STD_Code
                , objempBo.Ward_Number
                 , objempBo.ESI_Applicable
                , objempBo.ESI_Number
                , objempBo.ESI_Dispencary
                , objempBo.PF_Applicable
                , objempBo.PF_Number
                , objempBo.PF_Number_Dept_File
                , objempBo.Restrict_PF
                , objempBo.Zero_Pension
                , objempBo.Zero_PT
                , objempBo.flg
                , objempBo.AppID
                , objempBo.EDEPT
                , objempBo.EGRADE
                , objempBo.EBRANCH
                , objempBo.EDIVISION
                , objempBo.EDOJ
                , objempBo._1
                , objempBo._10
                , ref status);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }


    public int Create_Doc_Contypes(EmoDataBo objempBo, ref bool? status)
    {
        try
        {
            objDataContext = new EmpDataDALDataContext();
            int iResultCode = objDataContext.payc_create_update_Con_Doc_types(
                  objempBo.compCode
                , objempBo.Employee_ID
                , objempBo.TypeID
                , objempBo.ID
                , objempBo.flg
                , objempBo.loginID
                , objempBo._10
                , ref status);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }
    public EmpCollBo Get_empInfo(string compCode, string empID, int flg)
    {
        try
        {
            EmpDataDALDataContext objConfigurationDataContext = new EmpDataDALDataContext();
            EmpCollBo objConfigurationList = new EmpCollBo();
            foreach (var vRow in objConfigurationDataContext.payc_get_EmpDetails_dept(compCode, empID, flg))
            {
                EmoDataBo objBo = new EmoDataBo();
                objBo.Employee_ID = vRow.EmpID;
                objBo.FullName = vRow.ENAME;
                objBo.EDEPT = vRow.ORGTX;
                objBo.EGRADE = vRow.Grade;
                objBo.EDIVISION = vRow.Division;
                objBo.EBRANCH = vRow.Branch;
                objBo.EDOJ = Convert.ToDateTime(vRow.DOJ).ToString("yyyy-MM-dd");
                objBo.depitid = vRow.Dept_id;
                //objBo.Created_By = vRow.created;
                objConfigurationList.Add(objBo);
            }
            objConfigurationDataContext.Dispose();
            return objConfigurationList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public int update_empInfo(EmoDataBo objempBo, ref bool? status)
    {
        try
        {
            objDataContext = new EmpDataDALDataContext();
            int iResultCode = objDataContext.payc_update_EmpDetails_dept(
                  objempBo.compCode
                , objempBo.Employee_ID
                , objempBo.depitid
                , objempBo.EGRADE
                , objempBo.EDIVISION
                , objempBo.EBRANCH
                , objempBo.dOJ
                , objempBo.flg
                , ref status);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }


    public EmpCollBo viewall_emp(string ccode,string empid,int flag)
    {
        EmpDataDALDataContext objConfigurationDataContext = new EmpDataDALDataContext();
        EmpCollBo objConfigurationList = new EmpCollBo();

        foreach (var ENrow in objConfigurationDataContext.payc_get_all_detailsof_emp_comlogin(ccode, empid,flag))
        {
            EmoDataBo objBo = new EmoDataBo();
            objBo.Employee_ID = ENrow.EmpID;
            objBo.EDIVISION = ENrow.EName;
            objBo._2 = Convert.ToDateTime(ENrow.DOJ).ToString("dd-MMM-yyyy");
            objBo._1 = Convert.ToDateTime(ENrow.DOL).ToString("dd-MMM-yyyy");

            objConfigurationList.Add(objBo);
        }

        return objConfigurationList;
    }


    public EmpCollBo Get_empInfosaral(string compCode, int flg)
    {
        try
        {
            EmpDataDALDataContext objDataContext = new EmpDataDALDataContext();
            EmpCollBo objConfigurationList = new EmpCollBo();
            foreach (var vRow in objDataContext.payc_get_all_detailsof_emp_saral(compCode, flg))
            {
                EmoDataBo objBo = new EmoDataBo();
                objBo._1 = vRow._1.ToString();
                objBo._2 = vRow._2.ToString();
                objBo._3 = vRow._3.ToString();
                objBo._4 = vRow._4.ToString();
                objBo._5 = vRow._5.ToString();
                objBo._6 = vRow._6.ToString();
                objBo._7 = vRow._7.ToString();
                objBo._8 = vRow._8.ToString();
                objBo._9 = vRow._9.ToString();
                objBo._10 = vRow._10.ToString();
                objBo._11 = vRow._11.ToString();
                objBo._12 = vRow._12.ToString();
                objBo._13 = vRow._13.ToString();
                objBo._14 = vRow._14.ToString();
                objBo._15 = vRow._15.ToString();
                objBo._16 = vRow._16.ToString();
                objBo._17 = vRow._17.ToString();
                objBo._18 = vRow._18.ToString();
                objBo._19 = vRow._19.ToString();
                objBo._20 = vRow._20.ToString();
                objBo._21 = vRow._21.ToString();
                objBo._22 = vRow._22.ToString();
                objBo._23 = vRow._23.ToString();
                objBo._24 = vRow._24.ToString();
                objBo._25 = vRow._25.ToString();
                objBo._26 = vRow._26.ToString();
                objBo._27 = vRow._27.ToString();
                objBo._28 = vRow._28.ToString();
                objBo._29 = vRow._29.ToString();
                objBo._30 = vRow._30.ToString();
                objBo._31 = vRow._31.ToString();
                objBo._32 = vRow._32.ToString();
                objBo._33 = vRow._33.ToString();
                objBo._34 = vRow._34.ToString();
                objBo._35 = vRow._35.ToString();
                objBo._36 = vRow._36.ToString();
                objBo._37 = vRow._37.ToString();
                objBo._38 = vRow._38.ToString();
                objBo._39 = vRow._39.ToString();
                objBo._40 = vRow._40.ToString();
                objBo._41 = vRow._41.ToString();
                objBo._42 = vRow._42.ToString();
                objBo._43 = vRow._43.ToString();
                objBo._44 = vRow._44.ToString();
                objBo._45 = vRow._45.ToString();
                objBo._46 = vRow._46.ToString();
                objBo._47 = vRow._47.ToString();
                objBo._48 = vRow._48.ToString();
                objBo._49 = vRow._49.ToString();
                objBo._50 = vRow._50.ToString();
                objBo._51 = vRow._51.ToString();
                objBo._52 = vRow._52.ToString();
                objBo._53 = vRow._53.ToString();
                objBo._54 = vRow._54.ToString();
                objBo._55 = vRow._55.ToString();
                objBo._56 = vRow._56.ToString();
                objBo._57 = vRow._57.ToString();
                objBo._58 = vRow._58.ToString();
                objBo._59 = vRow._59.ToString();
                objBo._60 = vRow._60.ToString();
                objBo._61 = vRow._61.ToString();
                objConfigurationList.Add(objBo);
            }
            objDataContext.Dispose();
            return objConfigurationList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }





    public EmpCollBo Get_empInfofull(string compCode,string empid, int flg)
    {
        try
        {
            EmpDataDALDataContext objDataContext = new EmpDataDALDataContext();
            EmpCollBo objConfigurationList = new EmpCollBo();
            foreach (var vRow in objDataContext.payc_get_all_detailsof_emp_comloginfull(compCode,empid, flg))
            {
                EmoDataBo objBo = new EmoDataBo();
                objBo._1 = vRow.Empid;
                objBo._2 = vRow.Salutation;
                objBo._3 = vRow.F_name;
                objBo._4 = vRow.M_Name;
                objBo._5 = vRow.L_NAme;
                objBo._6 = vRow.Fathername;
                objBo._7 = vRow.Mothername;
                objBo.dt1 = vRow.DOB;
                objBo._9 = vRow.Gender;
                objBo._10 = vRow.Maritialstatus;
                objBo._11 = vRow.Spousename;
                objBo._12 = vRow.Designation;
                objBo._13 = vRow.Department;
                objBo._14 = vRow.Grade;
                objBo._15 = vRow.Branch;
                objBo._16 = vRow.Division;
                objBo._17 = vRow.AccNO;
                objBo._18 = vRow.Bankname;
                objBo._19 = vRow.IFSCcode;
                objBo._20 = vRow.TMPAddressLine1 + "  " + vRow.TMPAddressLine2 + "  " + vRow.TMPaddlocality ;
                objBo._21 = vRow.TMPAddressLine2;
                objBo._22 = vRow.TMPaddlocality;
                objBo._23 = vRow.TMPadddistrict + "  " + vRow.TMPaddstate + "  " + vRow.TMPaddpincode;
                objBo._24 = vRow.TMPaddstate;
                objBo._25 = vRow.TMPaddpincode;
                objBo._26 = vRow.PRMaddLine1 + "  " + vRow.PRMaddLine2 + "  " + vRow.PRMaddlocality;
                objBo._27 = vRow.PRMaddLine2;
                objBo._28 = vRow.PRMaddlocality;
                objBo._29 = vRow.PRMadddsitrict + "  " + vRow.PRMaddstate + "  " + vRow.PRMaddpincode;
                objBo._30 = vRow.PRMaddstate;
                objBo._31 = vRow.PRMaddpincode;
                objBo._32 = vRow.email;
                objBo._33 = vRow.stdcode;
                objBo._34 = vRow.mobile;
                objBo._35 = vRow.ESIapplicability;
                objBo._36 = vRow.ESINUM;
                objBo._37 = vRow.ESIDispensary;
                objBo._38 = vRow.PFapplicability;
                objBo._39 = vRow.PFNUM;
                objBo._40 = vRow.PFDeptfile;
                objBo._41 = vRow.PFRestrict;
                objBo._42 = vRow.ZeroPension;
                objBo._43 = vRow.ZeroPT;
                objConfigurationList.Add(objBo);
            }
            objDataContext.Dispose();
            return objConfigurationList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }



}

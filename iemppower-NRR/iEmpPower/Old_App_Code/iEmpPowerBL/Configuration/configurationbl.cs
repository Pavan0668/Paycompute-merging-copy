using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Configuration;
using iEmpPower.Old_App_Code.iEmpPowerMaster;


/// <summary>
/// Summary description for configurationbl
/// </summary>
public class configurationbl
{
    public configurationbl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
    public int Update(configurationbo objConfigurationeBo)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            int iResultValue = objConfigurationDataContext.sp_conf_update_requires_supervisor_approval(objConfigurationeBo.ALL_REQURIES_IDS,
                                                                                              objConfigurationeBo.MODIFIEDBY,
                                                                                              objConfigurationeBo.MODIFIEDON,
                                                                                              objConfigurationeBo.DESCRIPTION,
                                                                                              objConfigurationeBo.HR_DESCRIPTION,
                                                                                              objConfigurationeBo.FIN_ADMIN_STATUS_DESC);
            objConfigurationDataContext.Dispose();
            return iResultValue;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public configurationcollectionbo Get_Details()
    {
        configurationcollectionbo objConfigurationList = new configurationcollectionbo();
        foreach (var vRow in objConfigurationDataContext.sp_conf_get_requires_supervisor_approval())
        {
            configurationbo objBo = new configurationbo();
            objBo.REQURIES_ID = vRow.requires_id;
            objBo.REQUIRES_STATUS = vRow.requires_status;
            objBo.HR_STATUS = bool.Parse(vRow.hr_status.ToString());
            objBo.DESCRIPTION = vRow.description;
            objBo.FIN_ADMIN_STATUS = bool.Parse(Convert.ToString(vRow.fin_admin_status));
            objConfigurationList.Add(objBo);
        }
        return objConfigurationList;
    }
    public configurationcollectionbo Get_Address_Information()
    {
        configurationcollectionbo objConfigurationList = new configurationcollectionbo();
        foreach (var vRow in objConfigurationDataContext.sp_conf_get_address_information())
        {
            configurationbo objBo = new configurationbo();
            objBo.DESCRIPTION = vRow.name;
            objBo.DEFAULT_VALUE = vRow.default_value.ToString();
            objBo.MANDATORY_VALUE = vRow.ismandatroy.ToString();
            objConfigurationList.Add(objBo);
        }
        return objConfigurationList;
    }
    public int Update_Address_Informations(configurationbo objConfigurationeBo)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            int iResultValue = objConfigurationDataContext.sp_conf_update_address_information(objConfigurationeBo.DESCRIPTION,
                                                                                              objConfigurationeBo.MODIFIEDBY,
                                                                                              objConfigurationeBo.MODIFIEDON,
                                                                                              objConfigurationeBo.MANDATORY_VALUE);
            objConfigurationDataContext.Dispose();
            return iResultValue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public configurationcollectionbo Load_Mail_Server_Details()
    {
        configurationcollectionbo objConfigurationList = new configurationcollectionbo();
        foreach (var vRow in objConfigurationDataContext.sp_conf_load_mail_server_details())
        {
            configurationbo objBo = new configurationbo();
            objBo.POP_SERVER = vRow.pop_Server;
            objBo.IMAP_SERVER = vRow.imap_Server;
            objBo.SMTP_SERVER = vRow.smtp_Server;
            objBo.PASSWORD = vRow.password;
            objBo.USER_NAME = vRow.username;
            objBo.EMAIL_ID = vRow.email_id;
            objBo.PORT = vRow.port;
            objConfigurationList.Add(objBo);

        }
        return objConfigurationList;
    }
    public int Create_MailServer_Details(configurationbo objConfigurationeBo)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            int iResultValue = objConfigurationDataContext.sp_conf_create_mail_server_details(objConfigurationeBo.POP_SERVER, objConfigurationeBo.IMAP_SERVER,
                                                                                              objConfigurationeBo.SMTP_SERVER, objConfigurationeBo.EMAIL_ID,
                                                                                              objConfigurationeBo.USER_NAME, objConfigurationeBo.PASSWORD,
                                                                                              objConfigurationeBo.MODIFIEDBY,
                                                                                              objConfigurationeBo.MODIFIEDON, objConfigurationeBo.PORT);
            objConfigurationDataContext.Dispose();
            return iResultValue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public configurationcollectionbo Get_Bank_Information()
    {
        configurationcollectionbo objConfigurationList = new configurationcollectionbo();
        foreach (var vRow in objConfigurationDataContext.sp_conf_get_bank_information())
        {
            configurationbo objBo = new configurationbo();
            objBo.DESCRIPTION = vRow.name;
            objBo.DEFAULT_VALUE = vRow.default_value.ToString();
            objBo.MANDATORY_VALUE = vRow.ismandatroy.ToString();
            objConfigurationList.Add(objBo);
        }
        return objConfigurationList;
    }
    public int Update_Bank_Informations(configurationbo objConfigurationeBo)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            int iResultValue = objConfigurationDataContext.sp_conf_update_bank_information(objConfigurationeBo.DESCRIPTION,
                                                                                              objConfigurationeBo.MODIFIEDBY,
                                                                                              objConfigurationeBo.MODIFIEDON,
                                                                                              objConfigurationeBo.MANDATORY_VALUE);
            objConfigurationDataContext.Dispose();
            return iResultValue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public configurationcollectionbo Get_Family_Member_Information()
    {
        configurationcollectionbo objConfigurationList = new configurationcollectionbo();
        foreach (var vRow in objConfigurationDataContext.sp_conf_get_family_member_information())
        {
            configurationbo objBo = new configurationbo();
            objBo.DESCRIPTION = vRow.name;
            objBo.DEFAULT_VALUE = vRow.default_value.ToString();
            objBo.MANDATORY_VALUE = vRow.ismandatroy.ToString();
            objConfigurationList.Add(objBo);
        }
        return objConfigurationList;
    }
    public int Update_Family_Member_Informations(configurationbo objConfigurationeBo)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            int iResultValue = objConfigurationDataContext.sp_conf_update_family_member_information(objConfigurationeBo.DESCRIPTION,
                                                                                              objConfigurationeBo.MODIFIEDBY,
                                                                                              objConfigurationeBo.MODIFIEDON,
                                                                                              objConfigurationeBo.MANDATORY_VALUE);
            objConfigurationDataContext.Dispose();
            return iResultValue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public configurationcollectionbo Get_Personal_Data()
    {
        configurationcollectionbo objConfigurationList = new configurationcollectionbo();
        foreach (var vRow in objConfigurationDataContext.sp_conf_get_personal_data())
        {
            configurationbo objBo = new configurationbo();
            objBo.DESCRIPTION = vRow.name;
            objBo.DEFAULT_VALUE = vRow.default_value.ToString();
            objBo.MANDATORY_VALUE = vRow.ismandatroy.ToString();
            objConfigurationList.Add(objBo);
        }
        return objConfigurationList;
    }
    public int Update_Personal_Data(configurationbo objConfigurationeBo)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            int iResultValue = objConfigurationDataContext.sp_conf_update_personal_data(objConfigurationeBo.DESCRIPTION,
                                                                                              objConfigurationeBo.MODIFIEDBY,
                                                                                              objConfigurationeBo.MODIFIEDON,
                                                                                              objConfigurationeBo.MANDATORY_VALUE);
            objConfigurationDataContext.Dispose();
            return iResultValue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public int Create_Logo(configurationbo objConfigurationeBo)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {

            int iResultCode = objConfigurationDataContext.sp_conf_create_logo(objConfigurationeBo.LOGO, objConfigurationeBo.CREATEDBY, objConfigurationeBo.CREATEDON);
            objConfigurationDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public int Create_EmployeePhoto(configurationbo objConfigurationeBo)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {

            int iResultCode = objConfigurationDataContext.sp_conf_create_employee_photo(objConfigurationeBo.EMPLOYEE_PATH,
                                                                                        objConfigurationeBo.CREATEDBY, objConfigurationeBo.CREATEDON);
            objConfigurationDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public configurationcollectionbo Get_EmployeePhoto()
    {
        objConfigurationDataContext = new configurationdalDataContext();
        configurationcollectionbo objConfigurationList = new configurationcollectionbo();
        foreach (var vRow in objConfigurationDataContext.sp_conf_get_employee_photo())
        {
            configurationbo objBo = new configurationbo();
            objBo.EMPLOYEE_PATH = vRow.photo_path;
            objConfigurationList.Add(objBo);
        }
        return objConfigurationList;
    }

    public configurationcollectionbo Load_EmployeePhotoDetails(string sEmployeeId)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        configurationcollectionbo objConfigurationList = new configurationcollectionbo();
        foreach (var vRow in objConfigurationDataContext.payc_loadEmpDetails(sEmployeeId))
        {
            configurationbo objBo = new configurationbo();
            objBo.EMPLOYEE_PATH = vRow.PHOTO;
            objBo.DESCRIPTION = vRow.Name;
            objBo.Company_Code = vRow.Comany_Code == null ? "" : vRow.Comany_Code;
            objBo.iscomp = vRow.CompanyLogin;
            objBo.isAct = vRow.active;
            objBo.isDR = vRow.isDR;
            objBo.CLOGO = vRow.LogoPath;
            objConfigurationList.Add(objBo);
        }
        objConfigurationDataContext.Dispose();
        return objConfigurationList;
    }

    public configurationcollectionbo Get_EncashableLeaves()
    {
        objConfigurationDataContext = new configurationdalDataContext();
        configurationcollectionbo objConfigurationList = new configurationcollectionbo();
        foreach (var vRow in objConfigurationDataContext.sp_conf_get_leave_type_for_encashment())
        {
            configurationbo objBo = new configurationbo();
            objBo.MOAWB = Int32.Parse(vRow.moawb.ToString());
            objBo.AWART = vRow.awart;
            objBo.ATEXT = vRow.atext;
            objBo.ENCASHABLE = vRow.encashable;
            objConfigurationList.Add(objBo);
        }
        objConfigurationDataContext.Dispose();
        return objConfigurationList;
    }

    public int Create_LeaveEncashmentTypes(configurationbo objConfigurationeBo)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            int iResultValue = objConfigurationDataContext.sp_conf_create_leave_encashmet_types(objConfigurationeBo.MOAWB_STRING,
                                                                                              objConfigurationeBo.AWART_STRING,
                                                                                              objConfigurationeBo.ATEXT_STRING,
                                                                                              objConfigurationeBo.ENCASHABLE_STRING);
            objConfigurationDataContext.Dispose();
            return iResultValue;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public void Delete_master_CSKT()
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            objConfigurationDataContext.sp_master_Dublicate_CSKT();
            //return iResultValue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public configurationcollectionbo Get_LastLoginDetails()
    {
        try
        {
            objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.sp_Last_LoginDetails())
            {
                configurationbo objBo = new configurationbo();
                objBo.USER_NAME = vRow.UserName.ToString();
                objBo.NAME = vRow.Name;
                objBo.LASTLOGINDATE = vRow.LastLoginDate.ToString();
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

    ////------------------------------------PayCompute-------------
    public void CompanyCreate(configurationbo objConfigurationeBo, int? flg, ref bool? status)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            objConfigurationDataContext.payc_company_creation(
                                                                                    objConfigurationeBo.Company_Code,
                                                                                    objConfigurationeBo.Company_Name,
                                                                                    objConfigurationeBo.Company_Type,
                                                                                    objConfigurationeBo.Company_Address,
                                                                                    objConfigurationeBo.Country,
                                                                                    objConfigurationeBo.State,
                                                                                    objConfigurationeBo.District,
                                                                                    objConfigurationeBo.Pincode,
                                                                                    objConfigurationeBo.Company_MailID,
                                                                                    objConfigurationeBo.Company_ContactNum,
                                                                                    objConfigurationeBo.CREATEDBY, objConfigurationeBo.EMPLOYEE_PATH,
                                                                                    flg, ref status
                                                                                    );
            objConfigurationDataContext.Dispose();


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public static configurationcollectionbo Get_ComapnyTypes(int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_company_types(flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.Company_Type = vRow.ID;
                objBo.Company_Type_Txt = vRow.Compnay_Types;
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

    public static configurationcollectionbo Get_Country(int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_countrys(flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.Country = vRow.LAND1;
                objBo.CountryTxt = vRow.LANDX;
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

    public static configurationcollectionbo Get_states(string country, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_states(country, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.State = vRow.BLAND;
                objBo.StateTxt = vRow.BEZEI;
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

    public configurationcollectionbo Get_USersLock(string compCode,string eid, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_getUsers_compcode(compCode,eid, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.EMPID = vRow.EmpID;
                objBo.Company_MailID = vRow.Contact_Type_ID;
                objBo.PASSWORD = vRow.DOB.ToString();
                objBo.NAME = vRow.EMPNAME;
                objBo.Created_By = vRow.created;
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


    public void Updatelock_unlockUser(string cmpCode, string empID, int? flg,DateTime exitdate, ref bool? status)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {

            objConfigurationDataContext.payc_update_lock_unlockuser(cmpCode, empID, flg,exitdate, ref status);
            objConfigurationDataContext.Dispose();


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public int tempolock_unlockUser(string cmpCode, string empID, int? flg,ref bool? status)
    {
        
        try
        {
            objConfigurationDataContext = new configurationdalDataContext();
            int check_dbtbl = objConfigurationDataContext.payc_tempolock_user_membership(cmpCode, empID, flg,ref status);
            return check_dbtbl;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public static configurationcollectionbo Get_Salutations(int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_salutation(flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.DDLTYPE = vRow.ANRED.ToString();
                objBo.DDLTYPETEXT = vRow.ATEXT;
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


    public static configurationcollectionbo Get_MaterialStatus(int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_MaterialStatus(flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.DDLTYPE = vRow.FAMST.ToString();
                objBo.DDLTYPETEXT = vRow.FTEXT;
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

    public static configurationcollectionbo Get_AddressTypes(int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_Address_types(flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.DDLTYPE = vRow.ID.ToString();
                objBo.DDLTYPETEXT = vRow.Address_type;
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

    public static configurationcollectionbo Get_ContactTypes(int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_ContactTypes(flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.DDLTYPE = vRow.ID.ToString();
                objBo.DDLTYPETEXT = vRow.Contact_type;
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

    public static configurationcollectionbo Get_DocumtTypes(int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_DocumentTypes(flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.DDLTYPE = vRow.ID.ToString();
                objBo.DDLTYPETEXT = vRow.Proff_type;
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


    public static configurationcollectionbo Get_Employees(string comp, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_employees(comp, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.EMPID = vRow.EMPID;
                objBo.NAME =vRow.eid +'-'+ vRow.Name;
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

    public static configurationcollectionbo Emp_srch(string comp, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_employees(comp, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.EMPID = vRow.EMPID;
                objBo.NAME = vRow.Name;
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

    public int Create_mapping_emp(configurationbo objempBo, ref bool? status)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int iResultCode = objConfigurationDataContext.payc_create_Emp_Mgrmapping(
                  objempBo.Company_Code
                , objempBo.EMPID
                , objempBo.AppID
                 , objempBo.LoginID
                , objempBo.flag
                , ref status);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }

    public int Create_mapping_Designation(configurationbo objempBo, ref bool? status)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int iResultCode = objConfigurationDataContext.payc_create_EmpDesig_Mgrmapping(
                  objempBo.Company_Code
                , objempBo.EMPID
                , objempBo.desig
                , objempBo.flag
                , ref status);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }

    public int Create_designation(configurationbo objempBo, ref bool? status)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int iResultCode = objConfigurationDataContext.payc_create_Designation(
                  objempBo.Company_Code
                , objempBo.desigTEXT
                , objempBo.flag
                , ref status);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }

    public static configurationcollectionbo Get_Designationa(string comp, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_designations(comp, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.desig = vRow.ID;
                objBo.desigTEXT = vRow.Designation_Text;
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


    public static configurationcollectionbo Get_LeaveTypes(string comp, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_LeaveTypes(comp, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.leav = vRow.Att_Code;
                objBo.leavTEXT = vRow.Att_type;
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

    public int Create_leaveQuotas(configurationbo objempBo, ref bool? status)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int iResultCode = objConfigurationDataContext.payc_create_leaveQuotamapping(
                  objempBo.Company_Code
                , objempBo.leav
                , objempBo.period
                , objempBo.qu
                , objempBo.CRYFWRD
                , objempBo.LoginID
                , objempBo.flag
               , objempBo.frmdate
                , objempBo.todate
                , ref status);



            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }

    public int Create_HolidayCal(configurationbo objempBo, ref bool? status)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int iResultCode = objConfigurationDataContext.payc_create_HolidayCalender(
                  objempBo.Company_Code
                , objempBo.year
                , objempBo.Date
                , objempBo.Descrip
                , objempBo.TYPE
                , objempBo.flag
                , ref status);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }

    public void generate_leaveQuota(string cmpCode, int? flg, ref bool? status)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            objConfigurationDataContext.payc_gnerateLeaveQuoa_optn(cmpCode, flg, ref status);
            objConfigurationDataContext.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public void Update_leaveQuota(string cmpCode, int? flg, ref bool? status)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            objConfigurationDataContext.payc_updateLeaveQuoa(cmpCode, flg, ref status);
            objConfigurationDataContext.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public static configurationcollectionbo Get_Departments(string cc, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_Departments(cc,flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.deptdesc = vRow.Dept;
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

    public configurationcollectionbo Get_EmpManagerManpping(string compCode, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_MAnager_employee(compCode, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.EMPID;
                objBo.AppID = vRow.ManagerID;
                objBo.NAME = vRow.ENAME;
                objBo.USER_NAME = vRow.MGRENAME;
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

    public configurationcollectionbo Get_EmpDesignationManpping(string compCode, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_designations_employee(compCode, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.EMPID;
                objBo.desig = vRow.Designation;
                objBo.NAME = vRow.ENAME;
                objBo.desigTEXT = vRow.Designation_Text;
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

    public configurationcollectionbo Get_CompLeavManpping(string compCode, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_companyleavequotas(compCode, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.id1 = vRow.Leave_type;
                objBo.leav = vRow.Att_type;
                objBo.leavTEXT = vRow.Att_type;
                objBo.period = vRow.period;
                objBo.qu = vRow.quotaupdate;
                objBo.CRYFWRD = vRow.validity;
                objBo.frmdate = vRow.Period_From;
                objBo.todate = vRow.Period_End;
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

    public configurationcollectionbo LoadAllCompnydeatils(string compCode, int flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_allCompanydetails(compCode, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.Company_Code = vRow.Company_Code;
                objBo.Company_Name = vRow.Company_Name;
                objBo.Company_Type = vRow.Company_Type;
                objBo.Company_Type_Txt = vRow.Company_Typetxt;
                objBo.Company_Address = vRow.Company_Address;
                objBo.Country = vRow.Country;
                objBo.CountryTxt = vRow.countrytxt;
                objBo.State = vRow.State;
                objBo.StateTxt = vRow.statetxt;
                objBo.District = vRow.District;
                objBo.Pincode = vRow.Pincode;
                objBo.Company_MailID = vRow.Company_MailID;
                objBo.Company_ContactNum = vRow.Company_ContactNum;
                objBo.Created_By = vRow.Created_By;
                objBo.Created_On = vRow.Created_On;
                objBo.IsLocked = vRow.IsLocked;
                objBo.EMPLOYEE_PATH = vRow.LogoPath;
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

    public void setWeekend(string cmpCode, int wk, int? flg)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            objConfigurationDataContext.payc_get_set_Weekends(cmpCode, wk, flg);
            objConfigurationDataContext.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public configurationcollectionbo GetWeekend(string cmpCode, int wk, int? flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_set_Weekends(cmpCode, wk, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.Company_Code = vRow.Compcode;
                objBo.WK = vRow.WeekendID;
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

    public configurationcollectionbo Get_HolidayCalender(string compCode, string year, int? flg)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_HolidayCalender(compCode, year, flg))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.Date = vRow.DATE;
                //objBo.NAME = vRow.ENAME;
                objBo.HR_DESCRIPTION = vRow.TXT_LONG;
                objBo.H_type = vRow.KLASS.Trim(); 
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

    public void DeleteRw(string cmpCode, int ID, string type, int? flg,int subty,DateTime Frmdate,DateTime Todate,ref bool? sts)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            objConfigurationDataContext.payc_row_deletefrom_compConfig(cmpCode, ID, type, flg,subty,Frmdate,Todate,ref sts);
            objConfigurationDataContext.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public int formvalidation(string cmpCode,string type, string empid, ref bool? sts, int flg)
    {
        objConfigurationDataContext = new configurationdalDataContext();
        try
        {
            int check_types = objConfigurationDataContext.payc_validation_userinfoform(cmpCode,type, empid, ref sts, flg);
            return check_types;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public int Add_deptmasters(configurationbo objempBo,ref bool? sts)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int iResultCode = objConfigurationDataContext.payc_get_addDepartments(                                
                     objempBo.Descrip,
                     objempBo.Company_Code,
                     objempBo.flag,
                 ref sts);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }


    public configurationcollectionbo Get_empmgrdesig_config(string compCode,string eid,int? flag)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_emp_designmgrconfig(compCode, eid, flag))
            {
                configurationbo objBo = new configurationbo();
                objBo.EMPID = vRow.EmpID;
                objBo.NAME = vRow.Ename;
                objBo.desigTEXT = vRow.Designation;
                objBo.CRYFWRD = vRow.DesignID;
                objBo.deptid = vRow.Mgrid;
                objBo.DESCRIPTION = vRow.Mgrname;
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


    public configurationcollectionbo Get_empmgrdesig_config_full(string compCode, string empid, int? flag)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_emp_designmgrconfig_full(compCode, empid, flag))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.eid;
                objBo.desigTEXT = vRow.txt;
                objBo.CRYFWRD = vRow.TID;
                objBo.DESCRIPTION = vRow.ENAME;
                objBo.Date = vRow.Startdate;
                objBo.enddate = vRow.enddate;
                objBo.Created_On = vRow.updated_on;
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

    public configurationcollectionbo Get_mgrdesig_config_full(string compCode, string empid, int? flag)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_emp_mgrconfig_full(compCode, empid, flag))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.eid;
                objBo.NAME = vRow.ENAME;
                objBo.desigTEXT = vRow.txt;
                objBo.DESCRIPTION = vRow.ENAME;
                objBo.deptid = vRow.MNAME;
                objBo.Date = vRow.Startdate;
                objBo.enddate = vRow.enddate;
                objBo.Created_On = vRow.updated_on;
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

    public int create_mgr_desig(configurationbo objempBo,ref bool? desigsts, ref bool? mgrstatus)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int record = objConfigurationDataContext.payc_create_emp_designmgrconfig(
                                                                        objempBo.Company_Code
                                                                        , objempBo.EMPID
                                                                        , objempBo.EMAIL_ID
                                                                        , objempBo.ID
                                                                        , objempBo.flag
                                                                        , ref desigsts
                                                                        , ref mgrstatus);


            return record;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public configurationcollectionbo Get_empinfo_admin(string compCode, string empid, int? flag)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_empinfo_cadmin(compCode, empid, flag))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.CLOGO = vRow.Emp_Photo;
                objBo.EMPID = vRow.EmpID;
                objBo.desig = vRow.salutationid;
                objBo.DDLTYPETEXT = vRow.salutationtxt;
                objBo.NAME = vRow.ENAME;
                objBo.DESCRIPTION = vRow.Gender;
                objBo.WK = vRow.genderid;

                objBo.Created_By = vRow.Marital_stsid;
                objBo.deptid = vRow.Marital_sts;
                objBo.Company_Type_Txt = vRow.Mother_Name;
                objBo.Company_Name = vRow.Spouse_Name;
                objBo.IMAP_SERVER = vRow.Father_Name;
                
                objBo.desig =Convert.ToInt32(vRow.Dept_id);
                objBo.Company_Address = vRow.Dept;
                objBo.AWART = vRow.Grade;
                objBo.ATEXT_STRING = vRow.Branch;
                objBo.ATEXT = vRow.Division;    

                objBo.Date = vRow.DOJ;
                objBo.enddate = vRow.DOL;
                objBo.Created_On = vRow.Created_On;
                objBo.dt1 = vRow.DOB;
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


    public configurationcollectionbo Get_empcommunication_admin(string compCode, string empid, int? flag)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_empcommunication_cadmin(compCode, empid, flag))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.EmpID;
                objBo.NAME = vRow.ENAME;
                objBo.DESCRIPTION = vRow.Contact_typetxt;
                objBo.DDLTYPETEXT = vRow.Contact_Type_ID;
                objBo.desig = vRow.contactid;

                
                objBo.Date = vRow.Created_On;
                objBo.enddate = vRow.Approved_On;
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

    public configurationcollectionbo Get_empdocument_admin(string compCode, string empid, int? flag)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_empdocument_cadmin(compCode, empid, flag))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.EmpID;
                objBo.NAME = vRow.ENAME;
                objBo.DESCRIPTION = vRow.Proff_type;
                objBo.DDLTYPETEXT = vRow.Doc_Type_ID;
                objBo.desig = vRow.Doc_Types;
                objBo.H_type = vRow.Status;
                objBo.Date = vRow.Created_On;
                objBo.enddate = vRow.Approved_On;
                objBo.CLOGO = vRow.docpath;
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


    public configurationcollectionbo Get_empbank_admin(string compCode, string empid, int? flag)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_empbank_cadmin(compCode, empid, flag))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.EmpID;
                objBo.NAME = vRow.ENAME;
                objBo.DESCRIPTION = vRow.Bank_Name;
                objBo.DDLTYPETEXT = vRow.IFSC_Code;
                objBo.deptid = vRow.Acc_Num;
                objBo.H_type = vRow.Bank_Branch;
                objBo.ATEXT_STRING = vRow.Bank_District;
                objBo.Company_Type_Txt = vRow.countrytxt;
                objBo.Company_Name = vRow.countyid;
                objBo.Company_Address = vRow.stateid;
                objBo.IMAP_SERVER = vRow.state;
                objBo.Date = vRow.Created_On;
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


    public configurationcollectionbo Get_empbeneinfo_admin(string compCode, string empid, int? flag)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_empbeneinfo_cadmin(compCode, empid, flag))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.EmpID;
                objBo.NAME = vRow.ENAME;
                objBo.DESCRIPTION = vRow.esiapplicable;
                objBo.DDLTYPETEXT = vRow.ESI_Num.ToString();
               // objBo.desig = vRow.esiappid;
                objBo.H_type = vRow.ESI_Dispencary;
                objBo.ATEXT_STRING = vRow.PFapplible;
                //objBo.Company_Type_Txt = vRow.pfappid;
                objBo.Company_Name = vRow.PF_Num;
                objBo.Company_Address = vRow.Restrict_PF;
                objBo.IMAP_SERVER = vRow.Zero_Pension;
                objBo.AWART = vRow.Zero_PT;
                objBo.Date = vRow.Created_On;
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

    public configurationcollectionbo Get_empaddress_admin(string compCode, string empid, int? flag)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_empaddinfo_cadmin(compCode, empid, flag))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.EmpID;
                objBo.NAME = vRow.ENAME;
                objBo.desig = vRow.typid;
                objBo.DESCRIPTION = vRow.Address_type;
                objBo.DDLTYPETEXT = vRow.AddLine1;
                objBo.Company_Type_Txt = vRow.AddLine2;
                objBo.Company_Name = vRow.locality;
                objBo.Company_Address = vRow.district;
                objBo.AWART = vRow.countyid;
                objBo.ATEXT_STRING = vRow.country;
                objBo.ATEXT = vRow.stateid;
                objBo.CLOGO = vRow.state;
                objBo.TYPE = vRow.pincode;
                objBo.H_type = vRow.Status;

                objBo.Date = vRow.Created_On;
                objBo.dt1 = vRow.valid_from;
                objBo.enddate = vRow.valid_to;
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


    public int Update_Empinfo(configurationbo objempBo, ref bool? status,ref string hrmail,ref string empmail)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int upresult = objConfigurationDataContext.payc_Update_EmpInfo(
                                                                              objempBo.EMPID
                                                                            , objempBo.ID
                                                                             , objempBo.NAME
                                                                            , objempBo.PASSWORD
                                                                            , objempBo.MODIFIEDBY
                                                                            , objempBo.PORT
                                                                            , objempBo.leavTEXT
                                                                            , objempBo.IMAP_SERVER
                                                                            , objempBo.H_type
                                                                            , objempBo.HR_DESCRIPTION
                                                                            , objempBo.Date
                                                                            , objempBo.Created_On
                                                                            , objempBo.flag
                                                                            , objempBo.Company_Code
                                                                            , ref status
                                                                            , ref hrmail
                                                                            , ref empmail);

            return upresult;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }


    

    public void Load_punchinfileconfig_data(string comp, int? flag, ref string eidlen, ref string eid_substringlen, ref string datelen,
        ref string date_substringlen, ref string timelen, ref string time_substringlen, ref string dateformat, ref string timeformat)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int iResultCode = objConfigurationDataContext.payc_PunchInOut_AdminConfig(comp, flag, ref eidlen, ref eid_substringlen, ref datelen, ref date_substringlen, ref timelen, ref time_substringlen, ref dateformat, ref timeformat);

        }
        catch (Exception ex)
        { }
    }


    public int add_punchinfiles(configurationbo objempBo, ref bool? result)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int data = objConfigurationDataContext.payc_add_punchinfiles(
                                                                        objempBo.Company_Code
                                                                        , objempBo.PASSWORD
                                                                        , objempBo.flag
                                                                        , ref result);


            return data;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public int Create_punchin(configurationbo objo, ref bool? status)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            int checkin = objConfigurationDataContext.payc_insert_punchindata(
                  objo.EMPID
                , objo.Company_Address
                , objo.CountryTxt
                , objo.Company_Type_Txt
                , objo.Company_Code
                , objo.ATEXT
                , objo.flag
                , ref status);

            return checkin;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }

    public configurationcollectionbo Get_punchin_files(string compCode, int? flag)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_punchinfiles(compCode, flag))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.File_Path;
                objBo.NAME = vRow.Created_By;
                objBo.DESCRIPTION = vRow.Updated_By;
                objBo.Date = vRow.Created_On;
                objBo.enddate = vRow.Updated_On;
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


    public configurationcollectionbo Get_empname(int? flag, string compCode)
    {
        try
        {
            configurationdalDataContext objConfigurationDataContext = new configurationdalDataContext();
            configurationcollectionbo objConfigurationList = new configurationcollectionbo();
            foreach (var vRow in objConfigurationDataContext.payc_get_allemps(flag, compCode))
            {
                configurationbo objBo = new configurationbo();
                objBo.ID = vRow.ID;
                objBo.EMPID = vRow.EmpId;
                objBo.NAME = vRow.ENAME;
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

}
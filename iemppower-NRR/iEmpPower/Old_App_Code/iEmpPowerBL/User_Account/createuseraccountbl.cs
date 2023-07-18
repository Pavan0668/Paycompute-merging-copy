using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.User_Account;
using System.Data;

/// <summary>
/// Summary description for createuseraccountbl
/// </summary>
public class createuseraccountbl
{
	public createuseraccountbl()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    createuseraccountdalDataContext objCreateUserAccountDataContext = new createuseraccountdalDataContext();
    
    public int Save_Super_User_Type(createuseraccountbo objUserAccountBo)
    {
        try
        {
            objCreateUserAccountDataContext = new createuseraccountdalDataContext();

            int iResultCode = objCreateUserAccountDataContext.sp_save_super_user_type(objUserAccountBo.USERID);

            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public int Get_Super_User_Type(createuseraccountbo objUserAccountBo)
    {
        try
        {
            objCreateUserAccountDataContext = new createuseraccountdalDataContext();

            int iResultCode = objCreateUserAccountDataContext.sp_get_super_user_type(objUserAccountBo.USERID);

            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public void Delete_Super_User_Type(createuseraccountbo objUserAccountBo)
    {
        try
        {
            objCreateUserAccountDataContext = new createuseraccountdalDataContext();
            objCreateUserAccountDataContext.sp_delete_super_user_type(objUserAccountBo.USERID);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
}
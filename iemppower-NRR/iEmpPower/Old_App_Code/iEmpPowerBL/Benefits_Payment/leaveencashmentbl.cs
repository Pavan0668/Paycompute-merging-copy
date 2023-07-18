using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;

/// <summary>
/// Summary description for leaveencashmentbl
/// </summary>
public class leaveencashmentbl
{
	public leaveencashmentbl()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    leaveencashmentdalDataContext objLeaveEncashmentDataContext = new leaveencashmentdalDataContext();

    public void Create_LeaveEncashment(leaveencashmentcolumnsbo objBo, 
                                        ref bool? IS_SUPERVISR_APPROVAL_REQ, 
                                        ref bool? IS_HR_APPROVAL_REQ, 
                                        ref bool? IS_PAYROLLADMIN_APPROVAL_REQ,
                                        ref string Super_Pernr,
                                        ref string Super_Name,
                                        ref string Super_Mail,
                                        ref string Hr_Pernr,
                                        ref string Hr_Name,
                                        ref string Hr_Mail,
                                        ref string Payroll_Pernr,
                                        ref string Payroll_Name,
                                        ref string Payroll_Mail,
                                        ref string Pernr_Mail,
                                        ref bool? SaveStatus)
    {
        try
        {
            objLeaveEncashmentDataContext.sp_le_create_leave_encashment(objBo.PERNR,
                                                                        objBo.LEAVE_TYPE,
                                                                        objBo.DAYS_OR_HOURS,
                                                                        objBo.REQUIRED_BY_DATE,
                                                                        objBo.CREATED_BY,
                                                                        objBo.CREATED_ON,
                                                                        objBo.CURRENT_STATUS,
                                                                        ref IS_SUPERVISR_APPROVAL_REQ,
                                                                        ref IS_HR_APPROVAL_REQ,
                                                                        ref IS_PAYROLLADMIN_APPROVAL_REQ,
                                                                        ref Super_Pernr,
                                                                        ref Super_Name,
                                                                        ref Super_Mail,
                                                                        ref Hr_Pernr,
                                                                        ref Hr_Name,
                                                                        ref Hr_Mail,
                                                                        ref Payroll_Pernr,
                                                                        ref Payroll_Name,
                                                                        ref Payroll_Mail,
                                                                        ref Pernr_Mail,
                                                                        ref SaveStatus);
            objLeaveEncashmentDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    public leaveencashmentcollection Get_LeaveEncashmentDetails(leaveencashmentcolumnsbo objBo)
    {
        leaveencashmentcollection objList = new leaveencashmentcollection();
        foreach (var vRow in objLeaveEncashmentDataContext.sp_le_get_leave_encashment_details_for_employee(objBo.PERNR,objBo.LEAVE_TYPE))
        {
            leaveencashmentcolumnsbo objColumnsBo = new leaveencashmentcolumnsbo();
            objColumnsBo.REFERENCE_ID = vRow.Reference_ID;            
            objColumnsBo.LEAVE_TYPE = vRow.SUBTY;
            objColumnsBo.LEAVE_TYPE_DESC = vRow.ATEXT;
            objColumnsBo.DAYS_OR_HOURS = vRow.NUMBR;
            if (vRow.BEGDA != null)
            {
                objColumnsBo.REQUIRED_BY_DATE = Convert.ToDateTime(vRow.BEGDA.ToString());
            }
            else
            {
                objColumnsBo.REQUIRED_BY_DATE = Convert.ToDateTime("01/01/1900");
            }
            objColumnsBo.CURRENT_STATUS = vRow.current_status;

            objList.Add(objColumnsBo);
        }
        objLeaveEncashmentDataContext.Dispose();
        return objList;
    }

    public void Update_LeaveEncashment(leaveencashmentcolumnsbo objBo,
                                        ref bool? IS_SUPERVISR_APPROVAL_REQ,
                                        ref bool? IS_HR_APPROVAL_REQ,
                                        ref bool? IS_PAYROLLADMIN_APPROVAL_REQ,
                                        ref string Super_Pernr,
                                        ref string Super_Name,
                                        ref string Super_Mail,
                                        ref string Hr_Pernr,
                                        ref string Hr_Name,
                                        ref string Hr_Mail,
                                        ref string Payroll_Pernr,
                                        ref string Payroll_Name,
                                        ref string Payroll_Mail,
                                        ref string Pernr_Mail,
                                        ref bool? SaveStatus)
    {
        try
        {
            objLeaveEncashmentDataContext.sp_le_update_leave_encashment(objBo.REFERENCE_ID,
                                                                                            objBo.PERNR,
                                                                                            objBo.LEAVE_TYPE,
                                                                                            objBo.OLD_LEAVE_TYPE,
                                                                                            objBo.DAYS_OR_HOURS,
                                                                                            objBo.REQUIRED_BY_DATE,
                                                                                            objBo.CREATED_BY,
                                                                                            objBo.CREATED_ON,
                                                                                            objBo.MODIFIED_BY,
                                                                                            objBo.MODIFIED_ON,
                                                                                            ref IS_SUPERVISR_APPROVAL_REQ,
                                                                                            ref IS_HR_APPROVAL_REQ,
                                                                                            ref IS_PAYROLLADMIN_APPROVAL_REQ,
                                                                                            ref Super_Pernr,
                                                                                            ref Super_Name,
                                                                                            ref Super_Mail,
                                                                                            ref Hr_Pernr,
                                                                                            ref Hr_Name,
                                                                                            ref Hr_Mail,
                                                                                            ref Payroll_Pernr,
                                                                                            ref Payroll_Name,
                                                                                            ref Payroll_Mail,
                                                                                            ref Pernr_Mail,
                                                                                            ref SaveStatus);
            objLeaveEncashmentDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
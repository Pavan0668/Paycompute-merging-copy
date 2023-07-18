using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service;

/// <summary>
/// Summary description for msassignedtorolebl
/// </summary>
public class msassignedtorolebl
{
	public msassignedtorolebl()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    msassignedtoroledalDataContext objPIAssignTRDataContext = new msassignedtoroledalDataContext();
    public msassignedtomecollectionbo Load_Pending_AssignedTR(msassignedtomebo objAssginTRBo)
    {
        msassignedtomecollectionbo objAssginTRList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTRDataContext.sp_ms_load_pending_assigned_to_role(objAssginTRBo.PERNR))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PKEY = vRow.pkey;
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLSXT = vRow.PLSXT;
            objBo.USRID = vRow.USRID;
            objBo.REVIEW = vRow.Status;
            objBo.CHANGE_APPROVAL = vRow.change_approval;
            objBo.LAST_ACTIVITY_DATE = Convert.ToDateTime(vRow.Last_Activity_Date);
            objAssginTRList.Add(objBo);
        }
        objPIAssignTRDataContext.Dispose();
        return objAssginTRList;
    }
    public msassignedtomecollectionbo Load_Completed_AssignedTR(msassignedtomebo objAssginTRBo)
    {
        msassignedtomecollectionbo objAssginTRList = new msassignedtomecollectionbo();
        foreach (var vRow in objPIAssignTRDataContext.sp_ms_load_completed_assigned_to_role(objAssginTRBo.PERNR))
        {
            msassignedtomebo objBo = new msassignedtomebo();
            objBo.PKEY = vRow.pkey;
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ENAME = vRow.ENAME;
            objBo.PLSXT = vRow.PLSXT;
            objBo.USRID = vRow.USRID;
            objBo.REVIEW = vRow.Status;
            objBo.CHANGE_APPROVAL = vRow.change_approval;
            objBo.LAST_ACTIVITY_DATE = Convert.ToDateTime(vRow.Last_Activity_Date);
            objAssginTRList.Add(objBo);
        }
        objPIAssignTRDataContext.Dispose();
        return objAssginTRList;
    }
}
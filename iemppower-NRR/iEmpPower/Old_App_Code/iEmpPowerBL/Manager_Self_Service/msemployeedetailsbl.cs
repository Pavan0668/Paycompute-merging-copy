using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service;

/// <summary>
/// Summary description for msemployeedetailsbl
/// </summary>
public class msemployeedetailsbl
{
	public msemployeedetailsbl()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    msemployeedetailsdalDataContext objDataContext = new msemployeedetailsdalDataContext();
    public msemployeedetailscollectionbo Get_EmployeeDetails(msemployeedetailsbo objPersonalIDBo)
    {   
        msemployeedetailscollectionbo objPersonalIDsLst = new msemployeedetailscollectionbo();
        foreach (var vRow in objDataContext.sp_ms_load_employee_details_for_proof(objPersonalIDBo.PERNR))
        {
            msemployeedetailsbo objBo = new msemployeedetailsbo();
            objBo.BEGDA = vRow.BEGDA;
            objPersonalIDsLst.Add(objBo);
        }
        objDataContext.Dispose();
        return objPersonalIDsLst;
    }
}
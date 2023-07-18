using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Performance_Management_System; 

/// <summary>
/// Summary description for PMSbl
/// </summary>
public class PMSbl
{
	public PMSbl()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    ////method to load appraisal grid for employees
    //public PMScollectionbo Load_Appraisal_Templates(string strAppraisee)
    //{
    //    PMSDataContext objDataContext = new PMSDataContext();
    //    PMScollectionbo objList = new PMScollectionbo();
    //    foreach (var vRow in objDataContext.sp_apr_load_Templates(strAppraisee))
    //    {
    //        PMSbo objBo = new PMSbo();
    //        objBo.APPRAISAL_ID = vRow.APPRAISAL_ID.Trim().ToString();
    //        objBo.APPRAISAL_NAME = vRow.APPRAISAL_NAME.Trim().ToString();
    //        objBo.APPRAISEE_ID = vRow.APPRAISEE_ID.Trim().ToString();
    //        objBo.APPRAISER_ID = vRow.APPRAISER_ID.Trim().ToString();
    //        objBo.STATUSAPPR = vRow.STATUSAPPR.Trim().ToString();
    //        objList.Add(objBo);
    //    }
    //    objDataContext.Dispose();
    //    return objList;
    //}

    ////method to load appraisal grid for employees to approve
    //public PMScollectionbo Load_Appraisal_Templates_Approve(string strAppraisee)
    //{
    //    PMSDataContext objDataContext = new PMSDataContext();
    //    PMScollectionbo objList = new PMScollectionbo();
    //    foreach (var vRow in objDataContext.sp_apr_load_Templates(strAppraisee))
    //    {
    //        PMSbo objBo = new PMSbo();
    //        objBo.APPRAISAL_ID = vRow.APPRAISAL_ID.Trim().ToString();
    //        objBo.APPRAISAL_NAME = vRow.APPRAISAL_NAME.Trim().ToString();
    //        objBo.APPRAISEE_ID = vRow.APPRAISEE_ID.Trim().ToString();
    //        objBo.APPRAISER_ID = vRow.APPRAISER_ID.Trim().ToString();
    //        objList.Add(objBo);
    //    }
    //    objDataContext.Dispose();
    //    return objList;
    //}
    
    //    old

    //method to load appraisee templtes for employees
    //public PMScollectionbo Load_Appraisal_Templates_For_Employees(string strAppraiseeId)
    //{
    //    PMSDataContext objDataContext = new PMSDataContext();
    //    PMScollectionbo objList = new PMScollectionbo();
    //    foreach (var vRow in objDataContext.sp_apr_load_Templates (strAppraiseeId))
    //    {
    //         PMSbo objBo = new PMSbo();
    //         objBo.APPRAISAL_ID = vRow.APPRAISAL_ID.Trim().ToString();
    //         objBo.APPRAISAL_NAME = vRow.APPRAISAL_NAME.Trim().ToString();          
    //         objBo.APPRAISEE_ID = vRow.APPRAISEE_ID.Trim().ToString();
    //         objBo.APPRAISEE_NAME = vRow.APPRAISEE_NAME.Trim().ToString(); 
    //         objBo.APPRAISER_ID = vRow.APPRAISER_ID.Trim().ToString();
    //         objBo.APPRAISER_NAME = vRow.APPRAISER_NAME.Trim().ToString();

    //         if (vRow.PART_ID1!= null)
    //             objBo.PART_ID1 = Convert.ToString(vRow.PART_ID1.Trim());
    //         else
    //             objBo.PART_ID1 = string.Empty;
    //         if (vRow.PART_ID2 != null)
    //            objBo.PART_ID2 = Convert.ToString(vRow.PART_ID2.Trim());
    //         else
    //             objBo.PART_ID2 = string.Empty;
    //         if (vRow.PART_ID3!= null)
    //         objBo.PART_ID3 = Convert.ToString(vRow.PART_ID3.Trim());
    //         else
    //             objBo.PART_ID3 = string.Empty;
    //         if (vRow.PART_ID4!= null)
    //         objBo.PART_ID4 = Convert.ToString(vRow.PART_ID4.Trim());
    //         else
    //             objBo.PART_ID4 = string.Empty;
    //         if (vRow.PART_ID5!= null)
    //         objBo.PART_ID5 = Convert.ToString(vRow.PART_ID5.Trim());
    //         else
    //             objBo.PART_ID5 = string.Empty;
    //         if (vRow.PART_ID6!= null)
    //         objBo.PART_ID6 = Convert.ToString(vRow.PART_ID6.Trim());
    //         else
    //             objBo.PART_ID6 = string.Empty;
    //         if (vRow.PART_ID7!= null)
    //         objBo.PART_ID7 = Convert.ToString(vRow.PART_ID7.Trim());
    //         else
    //             objBo.PART_ID7 = string.Empty;
    //         if (vRow.OTHERS_ID!= null)
    //         objBo.OTHERS_ID =Convert.ToString(vRow.OTHERS_ID.Trim());
    //         else
    //             objBo.OTHERS_ID = string.Empty;
    //         objBo.StartDate = Convert.ToDateTime( vRow.StartDate);
    //         objBo.EndDate = Convert.ToDateTime(vRow.EndDate );
    //         objBo.STATUS = Convert.ToInt32(vRow.STATUS);
    //         objBo.SUBSTAT = Convert.ToInt32(vRow.SUBSTAT);

    //        objList.Add(objBo);
    //    }
    //    objDataContext.Dispose();
    //    return objList;
    //}

    ////method to load appraisee templtes for Managers
    //public PMScollectionbo Load_Appraisal_Templates_For_Managers(string strAppraiseeId)
    //{
    //    PMSDataContext objDataContext = new PMSDataContext();
    //    PMScollectionbo objList = new PMScollectionbo();
    //    foreach (var vRow in objDataContext.sp_apr_load_Templates_For_Managers (strAppraiseeId))
    //    {
    //        PMSbo objBo = new PMSbo();
    //        objBo.APPRAISAL_ID = vRow.APPRAISAL_ID.Trim().ToString();
    //        objBo.APPRAISAL_NAME = vRow.APPRAISAL_NAME.Trim().ToString();
    //        objBo.APPRAISEE_ID = vRow.APPRAISEE_ID.Trim().ToString();
    //        objBo.APPRAISEE_NAME = vRow.APPRAISEE_NAME.Trim().ToString();
    //        objBo.APPRAISER_ID = vRow.APPRAISER_ID.Trim().ToString();
    //        objBo.APPRAISER_NAME = vRow.APPRAISER_NAME.Trim().ToString();
    //        if (vRow.PART_ID1 != null)
    //            objBo.PART_ID1 = Convert.ToString(vRow.PART_ID1.Trim());
    //        else
    //            objBo.PART_ID1 = string.Empty;
    //        if (vRow.PART_ID2 != null)
    //            objBo.PART_ID2 = Convert.ToString(vRow.PART_ID2.Trim());
    //        else
    //            objBo.PART_ID2 = string.Empty;
    //        if (vRow.PART_ID3 != null)
    //            objBo.PART_ID3 = Convert.ToString(vRow.PART_ID3.Trim());
    //        else
    //            objBo.PART_ID3 = string.Empty;
    //        if (vRow.PART_ID4 != null)
    //            objBo.PART_ID4 = Convert.ToString(vRow.PART_ID4.Trim());
    //        else
    //            objBo.PART_ID4 = string.Empty;
    //        if (vRow.PART_ID5 != null)
    //            objBo.PART_ID5 = Convert.ToString(vRow.PART_ID5.Trim());
    //        else
    //            objBo.PART_ID5 = string.Empty;
    //        if (vRow.PART_ID6 != null)
    //            objBo.PART_ID6 = Convert.ToString(vRow.PART_ID6.Trim());
    //        else
    //            objBo.PART_ID6 = string.Empty;
    //        if (vRow.PART_ID7 != null)
    //            objBo.PART_ID7 = Convert.ToString(vRow.PART_ID7.Trim());
    //        else
    //            objBo.PART_ID7 = string.Empty;
    //        if (vRow.OTHERS_ID != null)
    //            objBo.OTHERS_ID = Convert.ToString(vRow.OTHERS_ID.Trim());
    //        else
    //            objBo.OTHERS_ID = string.Empty;
    //        objBo.StartDate = Convert.ToDateTime(vRow.StartDate);
    //        objBo.EndDate = Convert.ToDateTime(vRow.EndDate);
    //        objBo.STATUS = Convert.ToInt32(vRow.STATUS);
    //        objBo.SUBSTAT = Convert.ToInt32(vRow.SUBSTAT);

    //        objList.Add(objBo);
    //    }
    //    objDataContext.Dispose();
    //    return objList;
    //}
   
    //old

    //method to update status of template for employees
    //public int Update_Status_Appraisal_Templates(string strAppraiseeId,int istatus,int isubstatus)
    //{
    //    PMSDataContext objDataContext = new PMSDataContext();

    //    int iResultCode = objDataContext.sp_apr_update_Templates_status(strAppraiseeId, istatus, isubstatus);

    //    return iResultCode;
    //}

    //public PMScollectionbo Get_PARTID(string strAPPRAISAL_ID)
    //{
    //    PMSDataContext objDataContext = new PMSDataContext();
    //    PMScollectionbo objList = new PMScollectionbo();
    //    foreach (var vRow in objDataContext.sp_Get_PARTID(strAPPRAISAL_ID))
    //    {
    //        PMSbo objBo = new PMSbo();
    //        objBo.APPRAISAL_ID = vRow.APPRAISAL_ID.Trim().ToString();
    //        objBo.APPRAISAL_NAME = vRow.APPRAISAL_NAME.Trim().ToString();
    //        objBo.APPRAISEE_ID = vRow.APPRAISEE_ID.Trim().ToString();
    //        objBo.APPRAISEE_NAME = vRow.APPRAISEE_NAME.Trim().ToString();
    //        objBo.APPRAISER_ID = vRow.APPRAISER_ID.Trim().ToString();
    //        objBo.APPRAISER_NAME = vRow.APPRAISER_NAME.Trim().ToString();

    //        if (vRow.PART_ID1 != null)
    //            objBo.PART_ID1 = Convert.ToString(vRow.PART_ID1.Trim());
    //        else
    //            objBo.PART_ID1 = string.Empty;
    //        if (vRow.PART_ID2 != null)
    //            objBo.PART_ID2 = Convert.ToString(vRow.PART_ID2.Trim());
    //        else
    //            objBo.PART_ID2 = string.Empty;
    //        if (vRow.PART_ID3 != null)
    //            objBo.PART_ID3 = Convert.ToString(vRow.PART_ID3.Trim());
    //        else
    //            objBo.PART_ID3 = string.Empty;
    //        if (vRow.PART_ID4 != null)
    //            objBo.PART_ID4 = Convert.ToString(vRow.PART_ID4.Trim());
    //        else
    //            objBo.PART_ID4 = string.Empty;
    //        if (vRow.PART_ID5 != null)
    //            objBo.PART_ID5 = Convert.ToString(vRow.PART_ID5.Trim());
    //        else
    //            objBo.PART_ID5 = string.Empty;
    //        if (vRow.PART_ID6 != null)
    //            objBo.PART_ID6 = Convert.ToString(vRow.PART_ID6.Trim());
    //        else
    //            objBo.PART_ID6 = string.Empty;
    //        if (vRow.PART_ID7 != null)
    //            objBo.PART_ID7 = Convert.ToString(vRow.PART_ID7.Trim());
    //        else
    //            objBo.PART_ID7 = string.Empty;
    //        if (vRow.OTHERS_ID != null)
    //            objBo.OTHERS_ID = Convert.ToString(vRow.OTHERS_ID.Trim());
    //        else
    //            objBo.OTHERS_ID = string.Empty;
    //        objBo.StartDate = Convert.ToDateTime(vRow.StartDate);
    //        objBo.EndDate = Convert.ToDateTime(vRow.EndDate);
    //        objBo.STATUS = Convert.ToInt32(vRow.STATUS);
    //        objBo.SUBSTAT = Convert.ToInt32(vRow.SUBSTAT);

    //        objList.Add(objBo);
    //    }
    //    objDataContext.Dispose();
    //    return objList;
    //}


    //method to load appraisal grid for employees to approve
    //public PMScollectionbo Load_Appraisal_Templates_For_Managers(string strAppraiserId, DateTime dtStartDate)
    //{
    //    PMSDataContext objDataContext = new PMSDataContext();
    //    PMScollectionbo objList = new PMScollectionbo();
    //    foreach (var vRow in objDataContext.sp_apr_load_Templates_For_Managers(strAppraiserId,dtStartDate))
    //    {
    //        PMSbo objBo = new PMSbo();
    //        objBo.APPRAISAL_ID = vRow.APPRAISAL_ID.Trim().ToString();
    //        objBo.APPRAISAL_NAME = vRow.APPRAISAL_NAME.Trim().ToString();
    //        objBo.APPRAISEE_ID = vRow.APPRAISEE_ID.Trim().ToString();
    //        objBo.APPRAISEE_NAME = vRow.APPRAISEE_NAME.Trim().ToString();
    //        objBo.APPRAISER_ID = vRow.APPRAISER_ID.Trim().ToString();
    //        objBo.APPRAISER_NAME = vRow.APPRAISER_NAME.Trim().ToString();
    //        objBo.STATUS = vRow.STATUS.Trim().ToString();
    //        objBo.StartDate =Convert.ToDateTime (vRow.StartDate);
            
    //        objList.Add(objBo);
    //    }
    //    objDataContext.Dispose();
    //    return objList;
    //}


    //Method to load appraisee templtes for employees
    public PMScollectionbo Load_Appraisal_Templates_For_Employees(string strAppraiseeId)
    {
     
        PMSDataContext objDataContext = new PMSDataContext();
        PMScollectionbo objList = new PMScollectionbo();
        foreach (var vRow in objDataContext.sp_apr_load_Templates(strAppraiseeId))
        {
            PMSbo objBo = new PMSbo();
            objBo.id = vRow.id;
            objBo.APPRAISAL_ID = vRow.APPRAISAL_ID.Trim().ToString();
            objBo.APPRAISAL_NAME = vRow.APPRAISAL_NAME.Trim().ToString();
            objBo.APPRAISEE_ID = vRow.APPRAISEE_ID.Trim().ToString();
            objBo.APPRAISEE_NAME = vRow.APPRAISEE_NAME.Trim().ToString();
            objBo.APPRAISER_ID = vRow.APPRAISER_ID.Trim().ToString();
            objBo.APPRAISER_NAME = vRow.APPRAISER_NAME.Trim().ToString();

            //if (vRow.PART_ID != null)
            //    objBo.PART_ID1 = Convert.ToString(vRow.PART_ID1.Trim());
            //else
            //    objBo.PART_ID1 = string.Empty;
            //if (vRow.PART_ID2 != null)
            //    objBo.PART_ID2 = Convert.ToString(vRow.PART_ID2.Trim());
            //else
            //    objBo.PART_ID2 = string.Empty;
            //if (vRow.PART_ID3 != null)
            //    objBo.PART_ID3 = Convert.ToString(vRow.PART_ID3.Trim());
            //else
            //    objBo.PART_ID3 = string.Empty;
            //if (vRow.PART_ID4 != null)
            //    objBo.PART_ID4 = Convert.ToString(vRow.PART_ID4.Trim());
            //else
            //    objBo.PART_ID4 = string.Empty;
            //if (vRow.PART_ID5 != null)
            //    objBo.PART_ID5 = Convert.ToString(vRow.PART_ID5.Trim());
            //else
            //    objBo.PART_ID5 = string.Empty;
            //if (vRow.PART_ID6 != null)
            //    objBo.PART_ID6 = Convert.ToString(vRow.PART_ID6.Trim());
            //else
            //    objBo.PART_ID6 = string.Empty;
            //if (vRow.PART_ID7 != null)
            //    objBo.PART_ID7 = Convert.ToString(vRow.PART_ID7.Trim());
            //else
            //    objBo.PART_ID7 = string.Empty;
            //if (vRow.OTHERS_ID != null)
            //    objBo.OTHERS_ID = Convert.ToString(vRow.OTHERS_ID.Trim());
            //else
            //    objBo.OTHERS_ID = string.Empty;
            objBo.StartDate = Convert.ToDateTime(vRow.StartDate);
            objBo.EndDate = Convert.ToDateTime(vRow.EndDate);
            objBo.STATUS = Convert.ToInt32(vRow.STATUS);
            objBo.SUBSTAT = Convert.ToInt32(vRow.SUBSTAT);

            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    //method to load appraisee templtes for Managers
    public PMScollectionbo Load_Appraisal_Templates_For_Managers(string strAppraiseeId)
    {
        PMSDataContext objDataContext = new PMSDataContext();
        PMScollectionbo objList = new PMScollectionbo();
        foreach (var vRow in objDataContext.sp_apr_load_Templates_For_Managers(strAppraiseeId))
        {
            PMSbo objBo = new PMSbo();
            objBo.APPRAISAL_ID = vRow.APPRAISAL_ID.Trim().ToString();
            objBo.APPRAISAL_NAME = vRow.APPRAISAL_NAME.Trim().ToString();
            objBo.APPRAISEE_ID = vRow.APPRAISEE_ID.Trim().ToString();
            objBo.APPRAISEE_NAME = vRow.APPRAISEE_NAME.Trim().ToString();
            objBo.APPRAISER_ID = vRow.APPRAISER_ID.Trim().ToString();
            objBo.APPRAISER_NAME = vRow.APPRAISER_NAME.Trim().ToString();
            //if (vRow.PART_ID1 != null)
            //    objBo.PART_ID1 = Convert.ToString(vRow.PART_ID1.Trim());
            //else
            //    objBo.PART_ID1 = string.Empty;
            //if (vRow.PART_ID2 != null)
            //    objBo.PART_ID2 = Convert.ToString(vRow.PART_ID2.Trim());
            //else
            //    objBo.PART_ID2 = string.Empty;
            //if (vRow.PART_ID3 != null)
            //    objBo.PART_ID3 = Convert.ToString(vRow.PART_ID3.Trim());
            //else
            //    objBo.PART_ID3 = string.Empty;
            //if (vRow.PART_ID4 != null)
            //    objBo.PART_ID4 = Convert.ToString(vRow.PART_ID4.Trim());
            //else
            //    objBo.PART_ID4 = string.Empty;
            //if (vRow.PART_ID5 != null)
            //    objBo.PART_ID5 = Convert.ToString(vRow.PART_ID5.Trim());
            //else
            //    objBo.PART_ID5 = string.Empty;
            //if (vRow.PART_ID6 != null)
            //    objBo.PART_ID6 = Convert.ToString(vRow.PART_ID6.Trim());
            //else
            //    objBo.PART_ID6 = string.Empty;
            //if (vRow.PART_ID7 != null)
            //    objBo.PART_ID7 = Convert.ToString(vRow.PART_ID7.Trim());
            //else
            //    objBo.PART_ID7 = string.Empty;
            //if (vRow.OTHERS_ID != null)
            //    objBo.OTHERS_ID = Convert.ToString(vRow.OTHERS_ID.Trim());
            //else
            //    objBo.OTHERS_ID = string.Empty;
            objBo.StartDate = Convert.ToDateTime(vRow.StartDate);
            objBo.EndDate = Convert.ToDateTime(vRow.EndDate);
            objBo.STATUS = Convert.ToInt32(vRow.STATUS);
            objBo.SUBSTAT = Convert.ToInt32(vRow.SUBSTAT);

            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }
}
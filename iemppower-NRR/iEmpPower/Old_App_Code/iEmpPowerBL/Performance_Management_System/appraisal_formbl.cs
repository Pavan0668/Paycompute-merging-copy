using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Performance_Management_System; 

/// <summary>
/// Summary description for appraisal_formbl
/// </summary>
public class appraisal_formbl
{
	public appraisal_formbl()
	{
		//
		// TODO: Add constructor logic here
		//
	}
       
    
    //method to load drop down for perticular template and element name
    public appraisal_formcollectionbo Load_DropDown_Templates(string strAppraisalName,string strElementName)
    {
        appraisal_formDataContext objDataContext = new appraisal_formDataContext();
         appraisal_formcollectionbo objList = new appraisal_formcollectionbo();
        foreach (var vRow in objDataContext.sp_apr_Load_DropDown(strAppraisalName,strElementName))
        {
            appraisal_formbo objBo = new appraisal_formbo();
            objBo.VALUE_TEXT = vRow.Column1.Trim().ToString();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    //method to save appraisal data of employees
    public int Insert_Appraisal_Data(appraisal_formbo objBo,ref bool? bStatus)
    {
        appraisal_formDataContext objDataContext = new appraisal_formDataContext();

      int iResultCode = objDataContext.sp_apr_insert_appraisal(objBo.APPRAISAL_ID,objBo.APPRAISAL_NAME,objBo.ELEMENT_NAME,
          objBo.VALUE_TEXT_DRP,objBo.TDLINE,objBo.APPRAISEE_ID,objBo.APPRAISER_ID,objBo.STATUSAPPR ,ref bStatus);

        return iResultCode;
    }

    //method to load appraisal data for perticular template 
    public appraisal_formcollectionbo Load_Appraisal_Data(string strAppraisalID,string strAppraisalName,string strAppraisee,string strAppraiser)
    {
        appraisal_formDataContext objDataContext = new appraisal_formDataContext();
        appraisal_formcollectionbo objList = new appraisal_formcollectionbo();
        foreach (var vRow in objDataContext.sp_apr_load_appraisal(strAppraisalID,strAppraisalName,strAppraisee,strAppraiser))
        {
            appraisal_formbo objBo = new appraisal_formbo();
            objBo.APPRAISAL_ID = vRow.APPRAISAL_ID.Trim().ToString();
            objBo.APPRAISAL_NAME = vRow.APPRAISAL_NAME.Trim().ToString();
            objBo.APPRAISEE_ID = vRow.APPRAISEE_ID.Trim().ToString();
            objBo.APPRAISER_ID = vRow.APPRAISER_ID.Trim().ToString();
            objBo.ELEMENT_NAME = vRow.ELEMENT_NAME.Trim().ToString();
            objBo.VALUE_TEXT_DRP = vRow.VALUE_TEXT.Trim().ToString();
            objBo.TDLINE = vRow.TDLINE.Trim().ToString();
            
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    //method to load appraisal data for perticular template 
    public string Load_AppraiseeName(string strAppraisee)
    {
        string strName = "";
        appraisal_formDataContext objDataContext = new appraisal_formDataContext();

        foreach (var vRow in objDataContext.sp_apr_load_appraiseeName( strAppraisee))
        {
            strName = vRow.Column1.ToString();
        }
        objDataContext.Dispose();
        return strName;
    }
 
}
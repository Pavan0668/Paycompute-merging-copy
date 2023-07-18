using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;
/// <summary>
/// Summary description for pipersonalidsbl
/// </summary>
public class pipersonalidsbl
{
    public pipersonalidsbl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    pipersonalidsdalDataContext objPersonalIdsDataContext = new pipersonalidsdalDataContext();

    public pipersonalidscollectionbo Get_PersonalIDSDetails(pipersonalidsbo objPersonalIDBo)
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPersonalIdsDataContext.sp_pi_get_personal_ids(objPersonalIDBo.PERNR, objPersonalIDBo.PageIndex,objPersonalIDBo.PageSize))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();
            objBo.RowNumber = (int)vRow.RowNumber;
            objBo.ID = vRow.ID;
            objBo.ICTYPE = vRow.ICTYPE;
            objBo.ID_TYPE_TEXT = vRow.ICTXT;
            objBo.ICNUM = vRow.ICNUM;
            objBo.RecordCnt = vRow.RecordCnt;
            
            objPersonalIDsLst.Add(objBo);
        }
        objPersonalIdsDataContext.Dispose();
        return objPersonalIDsLst;
    }

//------------------------------------------------------------------------------ ESI PF DETAILS --------------------------------------------------
    public pipersonalidscollectionbo Get_ESIPF_details(pipersonalidsbo objPersonalIDsBo)
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPersonalIdsDataContext.payc_Load_benefitsbased_onEmpid(objPersonalIDsBo.AGENT_NAME,objPersonalIDsBo.COMMENTS, objPersonalIDsBo.Flag))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();

          
            objBo.ICTYPE = vRow.TEXT;
            objBo.ID_TYPE_TEXT = vRow.VALUE;
           




            objPersonalIDsLst.Add(objBo);
        }
        objPersonalIdsDataContext.Dispose();
        return objPersonalIDsLst;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------

    public pipersonalidscollectionbo Get_PersonalIDSDetails_Full(pipersonalidsbo objPersonalIDBo)
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPersonalIdsDataContext.sp_pi_get_personal_ids_full(objPersonalIDBo.ID, objPersonalIDBo.PERNR))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();
         
            objBo.ID = vRow.ID;
            objBo.ICTYPE = vRow.ICTYPE;
            objBo.ID_TYPE_TEXT = vRow.ICTXT;
            objBo.ICNUM = vRow.ICNUM;
            objBo.STATUS = vRow.STATUS;
            objBo.TRANSSTATUS = vRow.TransStatus == null ? "" : vRow.TransStatus;
           

           

            objPersonalIDsLst.Add(objBo);
        }
        objPersonalIdsDataContext.Dispose();
        return objPersonalIDsLst;
    }

    public void Create_PersonalIDs_car(pipersonalidsbo objPersonalIDBo, int flg, ref bool? status, ref int? carcc, ref DateTime? rgd, ref string file, ref string RCNAME, ref string RCNO)
    {
        try
        {
            objPersonalIdsDataContext.usp_set_car_details_pids(objPersonalIDBo.PERNR, objPersonalIDBo.CC, objPersonalIDBo.FPDAT,
                                                                                  objPersonalIDBo.AUTH1, objPersonalIDBo.RCNO, objPersonalIDBo.RCNAME, flg
                                                                                 , ref status, ref carcc, ref rgd, ref file, ref RCNO, ref RCNAME);
            objPersonalIdsDataContext.Dispose();





        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public pipersonalidscollectionbo Get_ALLPersonalIDSDetails_TD()
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPersonalIdsDataContext.sp_get_All_personal_ids())
        {
            pipersonalidsbo objBo = new pipersonalidsbo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.ICTYPE = vRow.ICTYPE;
            objBo.ID_TYPE_TEXT = vRow.ICTXT;
            objBo.ICNUM = vRow.ICNUM;
            objBo.STATUS = vRow.status;
            objBo.ISUPDATE = vRow.isupdate;
            objBo.CREATED_ON = Convert.ToDateTime(vRow.ENDDA);
            objPersonalIDsLst.Add(objBo);
        }
        objPersonalIdsDataContext.Dispose();
        return objPersonalIDsLst;
    }


    public pipersonalidscollectionbo Get_FLYER_NUMBER(pipersonalidsbo objPersonalIDBo)
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPersonalIdsDataContext.sp_get_master_ZTR_FLYER_NUMBER(objPersonalIDBo.PERNR))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.FRFLYNUM = vRow.FRFLYNUM;
            objBo.EMPNAME = vRow.EMPNAME;
            objBo.AIRLINE = vRow.AIRLINE;
            objBo.VALSTATUS = vRow.VALSTATUS;
            objPersonalIDsLst.Add(objBo);
        }
        objPersonalIdsDataContext.Dispose();
        return objPersonalIDsLst;
    }

    public pipersonalidscollectionbo Get_PASSPORT(pipersonalidsbo objPersonalIDBo)
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPersonalIdsDataContext.sp_get_master_ZTR_PASSPORT(objPersonalIDBo.PERNR))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.EMPNAME = vRow.EMPNAME;
            objBo.PASNUM = vRow.PASNUM;
            objBo.DOI = Convert.ToDateTime(vRow.DOI);
            objBo.DOE = Convert.ToDateTime(vRow.DOE);
            objBo.PLISS = vRow.PLISS;
          
            objPersonalIDsLst.Add(objBo);
        }
        objPersonalIdsDataContext.Dispose();
        return objPersonalIDsLst;
    }

    public pipersonalidscollectionbo Get_TRAVEL_INS(pipersonalidsbo objPersonalIDBo)
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPersonalIdsDataContext.sp_get_master_ZTR_TRAVEL_INS(objPersonalIDBo.PERNR))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.TRINSNO = vRow.TRINSNO;
            objBo.EMPNAME = vRow.EMPNAME;
            objBo.DOI = Convert.ToDateTime(vRow.DOI);
            objBo.DOE = Convert.ToDateTime(vRow.DOE);
            objBo.PLAN1 = vRow.PLAN1;
            objBo.PREMIUM = vRow.PREMIUM;
            objBo.AGENT_NAME = vRow.AGENT_NAME;
            objPersonalIDsLst.Add(objBo);
        }
        objPersonalIdsDataContext.Dispose();
        return objPersonalIDsLst;
    }

    public pipersonalidscollectionbo Get_VISA(pipersonalidsbo objPersonalIDBo)
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPersonalIdsDataContext.sp_get_master_ZTR_VISA(objPersonalIDBo.PERNR))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.VINUM = vRow.VINUM;
            objBo.PASNUM = vRow.PASNUM;
            objBo.EMPNAME= vRow.EMPNAME;
            objBo.COUNTRY = vRow.COUNTRY;
            objBo.DOI = Convert.ToDateTime(vRow.DOI);
            objBo.DOE = Convert.ToDateTime(vRow.DOE);
            objBo.VISA_TYPE = vRow.VISA_TYPE;
            objPersonalIDsLst.Add(objBo);
        }
        objPersonalIdsDataContext.Dispose();
        return objPersonalIDsLst;
    }

    public int Update_PersonalIDs(pipersonalidsbo objPersonalIDBo, ref bool? SuperVisorstatus, ref bool? HRstatus, ref string SuperVisorPernr,
                                   ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrEMail, ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {
            int iResultCode = objPersonalIdsDataContext.sp_pi_update_personal_ids(objPersonalIDBo.PERNR,objPersonalIDBo.ICTYPE, objPersonalIDBo.ICNUM,
                                                                                  objPersonalIDBo.OLD_ICNUM, objPersonalIDBo.OLD_ICTYPE,
                                                                                  objPersonalIDBo.MODIFIED_BY,objPersonalIDBo.MODIFIEDON,
                                                                                  ref SuperVisorstatus,ref HRstatus,
                                                                                  ref HRPernr,ref SuperVisorPernr,
                                                                                  ref HRMail,ref SuperVisorMail,
                                                                                  ref PernrEMail, ref SuperVisorPhn, ref HRPhn);
            objPersonalIdsDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public pipersonalidscollectionbo CheckValidation_PersonalIDSDetails()
    {
        pipersonalidscollectionbo objPersonalIDsLst = new pipersonalidscollectionbo();
        foreach (var vRow in objPersonalIdsDataContext.sp_conf_checkvalidation_for_personal_ids())
        {
            pipersonalidsbo objBo = new pipersonalidsbo();
            objBo.DESCRIPTION = vRow.name;
            objBo.VALUE = vRow.values;

            objPersonalIDsLst.Add(objBo);
        }
        objPersonalIdsDataContext.Dispose();
        return objPersonalIDsLst;
    }
    public int Create_PersonalIDs(pipersonalidsbo objPersonalIDBo, ref bool? SuperVisorstatus, ref bool? HRstatus, ref string SuperVisorPernr,
                                   ref string SuperVisorMail, ref string HRPernr, ref string HRMail,ref string PernrName, ref string PernrEMail,
                                    ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {
            int iResultCode = objPersonalIdsDataContext.sp_pi_create_personal_ids(objPersonalIDBo.PERNR, objPersonalIDBo.ICTYPE, objPersonalIDBo.ICNUM, 
                                                                                  objPersonalIDBo.CREATED_BY, objPersonalIDBo.CREATED_ON,
                                                                                  ref SuperVisorstatus,ref HRstatus,ref HRPernr,
                                                                                  ref SuperVisorPernr,ref HRMail,ref SuperVisorMail,
                                                                                  ref PernrName,ref PernrEMail, ref SuperVisorPhn, ref HRPhn);
            objPersonalIdsDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }




    public int Add_Update_Del_PersonalIDsDetails(pipersonalidsbo objPersonalIDBo, ref string HRMail, ref string SuperVisorMail
       , ref string PernrName, ref string PernrEMail)
    {
        try
        {
            int iResultCode = objPersonalIdsDataContext.usp_pi_personal_id_details(objPersonalIDBo.ID
                                                                                    , objPersonalIDBo.PERNR
                                                                                    , objPersonalIDBo.ICTYPE
                                                                                    , objPersonalIDBo.ICNUM
                                                                                    , objPersonalIDBo.docpath
                                                                                    , objPersonalIDBo.BEGDA
                                                                                    , objPersonalIDBo.ENDDA
                                                                                    , objPersonalIDBo.CREATED_BY
                                                                                    , objPersonalIDBo.CREATED_ON
                                                                                    , objPersonalIDBo.MODIFIED_BY
                                                                                    , objPersonalIDBo.MODIFIEDON
                                                                                    , objPersonalIDBo.STATUS
                                                                                    , objPersonalIDBo.Flag
                                                                                    , ref HRMail
                                                                                    , ref SuperVisorMail
                                                                                   , ref PernrName
                                                                                   , ref PernrEMail);
            objPersonalIdsDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public List<pipersonalidsbo> Approval_PIDetails_Mail(string pkey, int Id, string sts) 
    {
        pipersonalidsdalDataContext objPersonalIdsDataContext = new pipersonalidsdalDataContext();

        List<pipersonalidsbo> PIobjList = new List<pipersonalidsbo>();
        foreach (var vRow in objPersonalIdsDataContext.sp_pi_get_Personalidinfo_mail(pkey, Id, sts))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();

            objBo.ID = vRow.ID;
            objBo.ICTYPE = vRow.ICTYPE;
            objBo.ID_TYPE_TEXT = vRow.ICTXT;
            objBo.ICNUM = vRow.ICNUM;

            PIobjList.Add(objBo);

        }
        return PIobjList;
    }



    public pipersonalidscollectionbo Get_PANPERNR(string pernr)
    {
        pipersonalidsdalDataContext objPersonalIdsDataContext = new pipersonalidsdalDataContext();
        pipersonalidscollectionbo objList = new pipersonalidscollectionbo();
        foreach (var vRow in objPersonalIdsDataContext.sp_get_PERNRPAN(pernr))
        {
            pipersonalidsbo objBo = new pipersonalidsbo();
            objBo.ID_TYPE_TEXT = vRow.ICNUM.ToString().Trim();
            objList.Add(objBo);
        }
        objPersonalIdsDataContext.Dispose();
        return objList;
    }
}
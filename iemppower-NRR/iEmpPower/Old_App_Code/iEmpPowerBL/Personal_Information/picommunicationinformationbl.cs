using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;

/// <summary>
/// Summary description for picommunicationinformationbl
/// </summary>
public class picommunicationinformationbl
{
    public picommunicationinformationbl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    picommunicationinformationdalDataContext objComInfoDataContext = new picommunicationinformationdalDataContext();
    public int Update_CommInfo(picommunicationinformationbo objComInfoBo, ref bool? SuperVisorstatus, ref bool? HRstatus, ref string SuperVisorPernr,
                                   ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrEMail, ref string PernrName,
                                    ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {
            objComInfoDataContext = new picommunicationinformationdalDataContext();
            int iResultCode = objComInfoDataContext.sp_pi_update_communication_info(objComInfoBo.EMPLOYEE_ID,
                                                                                    objComInfoBo.USR2,
                                                                                    objComInfoBo.USR2ID,
                                                                                    objComInfoBo.USR3,
                                                                                    objComInfoBo.USR3ID,
                                                                                    objComInfoBo.USR4,
                                                                                    objComInfoBo.USR4ID,
                                                                                    objComInfoBo.USR5,
                                                                                    objComInfoBo.USR5ID,
                                                                                    objComInfoBo.USR6,
                                                                                    objComInfoBo.USR6ID, objComInfoBo.MPHN, objComInfoBo.MPHN_ID,
                                                                                    objComInfoBo.MODIFIEDON,
                                                                                    objComInfoBo.MODIFIED_BY,
                                                                                     ref  SuperVisorstatus, ref HRstatus,
                                                                                     ref HRPernr, ref SuperVisorPernr,
                                                                                     ref HRMail, ref SuperVisorMail,
                                                                                     ref PernrEMail, ref PernrName,
                                                                                     ref SuperVisorPhn, ref HRPhn
                                                                                   );


            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public picommunicationinformationcollectionbo Get_Details(picommunicationinformationbo objCommInfoBo)
    {
        picommunicationinformationcollectionbo objComInfoLst = new picommunicationinformationcollectionbo();
        foreach (var vRow in objComInfoDataContext.sp_pi_get_communication_info_s(objCommInfoBo.EMPLOYEE_ID))
        {
            picommunicationinformationbo objBo = new picommunicationinformationbo();



            //if (vRow.USRTY == "0002")
            //{
            //    objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
            //    objBo.BUILDING_NO = vRow.USRID;
            //    objBo.USRTY = vRow.USRTY;
            //    objBo.STATUS = vRow.status;
            //    objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            //}
            if (vRow.USRTY == "0005")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.BUILDING_NO = vRow.USRID;
                objBo.USRTY = vRow.USRTY;
                objBo.STATUS = vRow.STATUS;
                //objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            }
            else if (vRow.USRTY == "0003")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.ROOM_NO = vRow.USRID;
                objBo.USRTY = vRow.USRTY;
                objBo.STATUS = vRow.STATUS;
                //objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            }
            else if (vRow.USRTY == "0004")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.EXTENSION = vRow.USRID;
                objBo.USRTY = vRow.USRTY;
                objBo.STATUS = vRow.STATUS;
                //objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            }
            else if (vRow.USRTY == "0010")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.EMAIL = vRow.USRID;
                objBo.USRTY = vRow.USRTY;
                objBo.STATUS = vRow.STATUS;
                //objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            }
            //else if (vRow.USRTY == "0006")
            else if (vRow.USRTY == "0021")    //USe as Mobile2
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.LICENCE_NO = vRow.USRID;
                objBo.USRTY = vRow.USRTY;
                objBo.STATUS = vRow.STATUS;
                //objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            }
            //else if (vRow.USRTY == "MPHN")
            else if (vRow.USRTY == "0020")
            {
                objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
                objBo.MPHN_ID = vRow.USRID;
                objBo.MPHN = vRow.USRTY;
                objBo.STATUS = vRow.STATUS;
                //objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            }





            objComInfoLst.Add(objBo);
        }
        return objComInfoLst;
    }

    public picommunicationinformationcollectionbo Get_Communication_Details(picommunicationinformationbo objCommInfoBo)
    {
        picommunicationinformationcollectionbo objComInfoLst = new picommunicationinformationcollectionbo();
        foreach (var vRow in objComInfoDataContext.sp_pi_get_communication_info(objCommInfoBo.EMPLOYEE_ID, objCommInfoBo.PageIndex, objCommInfoBo.PageSize))
        {
            picommunicationinformationbo objBo = new picommunicationinformationbo();
            objBo.RowNumber = (int)vRow.RowNumber;
            objBo.ID = vRow.ID;
            objBo.USRTY = vRow.USRTY;
            objBo.USTXT = vRow.USTXT;
            objBo.USRID = vRow.USRID;
            objBo.RecordCnt = vRow.RecordCnt;



            //foreach (var vRow in objComInfoDataContext.sp_pi_get_communication_info(objCommInfoBo.EMPLOYEE_ID))
            //{
            //    picommunicationinformationbo objBo = new picommunicationinformationbo();
            //    if (vRow.USRTY == "0002")
            //    {
            //        objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
            //        objBo.BUILDING_NO = vRow.USRID;
            //        objBo.USRTY = vRow.USRTY;
            //        objBo.STATUS = vRow.status;
            //        objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            //    }
            //    else if (vRow.USRTY == "0003")
            //    {
            //        objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
            //        objBo.ROOM_NO = vRow.USRID;
            //        objBo.USRTY = vRow.USRTY;
            //        objBo.STATUS = vRow.status;
            //        objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            //    }
            //    else if (vRow.USRTY == "0004")
            //    {
            //        objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
            //        objBo.EXTENSION = vRow.USRID;
            //        objBo.USRTY = vRow.USRTY;
            //        objBo.STATUS = vRow.status;
            //        objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            //    }
            //    else if (vRow.USRTY == "0010")
            //    {
            //        objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
            //        objBo.EMAIL = vRow.USRID;
            //        objBo.USRTY = vRow.USRTY;
            //        objBo.STATUS = vRow.status;
            //        objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            //    }
            //    else if (vRow.USRTY == "0006")
            //    {
            //        objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
            //        objBo.LICENCE_NO = vRow.USRID;
            //        objBo.USRTY = vRow.USRTY;
            //        objBo.STATUS = vRow.status;
            //        objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            //    }
            //    else if (vRow.USRTY == "MPHN")
            //    {
            //        objBo.EMPLOYEE_ID = vRow.PERNR.ToString();
            //        objBo.MPHN_ID = vRow.USRID;
            //        objBo.MPHN = vRow.USRTY;
            //        objBo.STATUS = vRow.status;
            //        objBo.ISUPDATE = bool.Parse(vRow.isupdate.ToString());
            //    }
            objComInfoLst.Add(objBo);
        }
        return objComInfoLst;
    }

    public picommunicationinformationcollectionbo Get_Communication_Details_Full(picommunicationinformationbo objCommInfoBo)
    {
        picommunicationinformationcollectionbo objComInfoLst = new picommunicationinformationcollectionbo();
        foreach (var vRow in objComInfoDataContext.sp_pi_get_communication_info_full(objCommInfoBo.ID, objCommInfoBo.EMPLOYEE_ID))
        {
            picommunicationinformationbo objBo = new picommunicationinformationbo();

            objBo.ID = vRow.ID;
            objBo.USRTY = vRow.USRTY;
            objBo.USTXT = vRow.USTXT;
            objBo.USRID = vRow.USRID;
            objBo.STATUS = vRow.STATUS;
            objBo.TRANSSTATUS = vRow.TransStatus == null ? "" : vRow.TransStatus;

            objComInfoLst.Add(objBo);
        }
        return objComInfoLst;
    }

    public picommunicationinformationcollectionbo Who_Is_Who_Details(string cc, int flg, picommunicationinformationbo objWhoIsWhoBo)
    {
        picommunicationinformationcollectionbo objComInfoLst = new picommunicationinformationcollectionbo();
        foreach (var vRow in objComInfoDataContext.sp_ms_load_who_is_who(objWhoIsWhoBo.EMPLOYEE_ID, objWhoIsWhoBo.DESIGNATION, cc, 1))
        {
            picommunicationinformationbo objBo = new picommunicationinformationbo();
            objBo.PERNR = vRow.PERNR.ToString();
            objBo.EMPLOYEE_ID = vRow.ENAME;
            if (vRow.PHOTO == "")
            {
                objBo.EMPLOYEE_PHOTO_PATH = "~/images/default-emp.jpg";
            }
            else
            {
                objBo.EMPLOYEE_PHOTO_PATH = vRow.PHOTO;
            }
            objBo.DESIGNATION = vRow.PLSXT;
            objBo.ALTEMAILID = vRow.Altemail;
            objBo.EMAIL = vRow.emailid;
            objBo.MPHN = vRow.mobileno;

            objBo.DEPDETAILS = vRow.depdetails;
            objBo.JOININGDATE = DateTime.Parse(vRow.joiningdate.ToString()).ToString("dd-MMM-yyyy");
            objBo.PERAREADESC = vRow.perslareadesc;
            objBo.MODULE = vRow.EMP_DEPART;
            objBo.MNGRNAME = vRow.mngrname;

            objComInfoLst.Add(objBo);
        }
        return objComInfoLst;
    }
    public picommunicationinformationcollectionbo Get_HR_Approval(picommunicationinformationbo objPersonalIDBo)
    {
        picommunicationinformationcollectionbo objPersonalIDsLst = new picommunicationinformationcollectionbo();
        foreach (var vRow in objComInfoDataContext.sp_ms_load_hr_approval_for_employee(objPersonalIDBo.PERNR))
        {
            picommunicationinformationbo objBo = new picommunicationinformationbo();
            objBo.EMAIL = vRow.USRID;
            objPersonalIDsLst.Add(objBo);
        }
        objComInfoDataContext.Dispose();
        return objPersonalIDsLst;
    }

    public int Add_Update_Del_CommDetails(picommunicationinformationbo objComInfoBo, ref string HRMail, ref string SuperVisorMail, ref string PernrName, ref string PernrEMail)
    {


        try
        {
            objComInfoDataContext = new picommunicationinformationdalDataContext();
            int iResultCode = objComInfoDataContext.usp_pi_communication_details(objComInfoBo.ID,
                                                                                   objComInfoBo.EMPLOYEE_ID,
                                                                                   objComInfoBo.USRTY,
                                                                                   objComInfoBo.USRID,
                                                                                   objComInfoBo.BEGDA,
                                                                                   objComInfoBo.ENDDA,
                                                                                   objComInfoBo.CREATED_BY,
                                                                                   objComInfoBo.CREATED_ON,
                                                                                    objComInfoBo.MODIFIED_BY,
                                                                                  objComInfoBo.MODIFIEDON,
                                                                                   objComInfoBo.STATUS,
                                                                                   objComInfoBo.Flag
                                                                                   , ref HRMail
                                                                                   , ref SuperVisorMail
                                                                                   , ref PernrName
                                                                                   , ref PernrEMail);
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public List<picommunicationinformationbo> Approval_APIDetails_Mail(string pkey, int Id, string sts)
    {
        objComInfoDataContext = new picommunicationinformationdalDataContext();
        List<picommunicationinformationbo> ComobjList = new List<picommunicationinformationbo>();
        foreach (var vRow in objComInfoDataContext.sp_pi_get_Communicationinfo_mail(pkey, Id, sts))
        {
            picommunicationinformationbo PIComObj = new picommunicationinformationbo();

            PIComObj.ID = vRow.ID;
            PIComObj.USRTY = vRow.USRTY;
            PIComObj.USTXT = vRow.USTXT;
            PIComObj.USRID = vRow.USRID;

            ComobjList.Add(PIComObj);

        }
        return ComobjList;
    }
}
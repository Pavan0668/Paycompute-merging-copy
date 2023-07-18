using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Working_Time;
using System.Data;

/// <summary>
/// Summary description for leaverequestbl
/// </summary>
public class leaverequestbl
{
    public leaverequestbl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    leaverequestdalDataContext objLeaveReuqestDataContext = new leaverequestdalDataContext();
    public int Create_Leave_Request_Details(string compcode, leaverequestbo objLeaveRquestBo, int HDType, ref string HR_Email, ref string Supervisor_Email, ref string PERNR_Name,
                                   ref string PERNR_Email, ref int? LID, ref string PKEYO)
    {
        try
        {
            objLeaveReuqestDataContext = new leaverequestdalDataContext();

            int iResultCode = objLeaveReuqestDataContext.sp_wt_create_leave_request(compcode, objLeaveRquestBo.LEAVE_REQ_ID
                , objLeaveRquestBo.PKEY
                , objLeaveRquestBo.PERNR
                , objLeaveRquestBo.BEGDA
                , objLeaveRquestBo.ENDDA
                , TimeSpan.Parse(objLeaveRquestBo.BEGUZ)
                , TimeSpan.Parse(objLeaveRquestBo.ENDUZ)
                , objLeaveRquestBo.AWART
                , objLeaveRquestBo.STDAZ
                , objLeaveRquestBo.NOTE
                , objLeaveRquestBo.APPROVED_BY
                , objLeaveRquestBo.STATUS
                , objLeaveRquestBo.PERNR
                , objLeaveRquestBo.Flag
                , objLeaveRquestBo.TABLETYPE
                , HDType
                , ref HR_Email
                , ref Supervisor_Email
                , ref PERNR_Name
                , ref PERNR_Email
                , ref LID
                , ref PKEYO);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }



    //public int Get_Noofleaves(leaverequestbo objLeaveRquestBo,ref DateTime? firstday, ref DateTime? lastday, ref int? count)
    //{
    //    try
    //    {
    //        objLeaveReuqestDataContext = new leaverequestdalDataContext();
    //        DateTime dt = DateTime.MinValue;

    //        int iResultCode = objLeaveReuqestDataContext.sp_Get_Noofleaves(objLeaveRquestBo.PERNR,
    //                                                                            Convert.ToDateTime(objLeaveRquestBo.BEGDA),
    //                                                                            Convert.ToDateTime(objLeaveRquestBo.ENDDA),
    //                                                                            objLeaveRquestBo.AWART,
    //                                                                            ref firstday, ref lastday,
    //                                                                            ref count);

    //        return iResultCode;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message.ToString());
    //    }
    //}

    public int Get_Noofleaves(leaverequestbo objLeaveRquestBo, ref DateTime? firstday, ref DateTime? lastday, ref int? count)
    {
        try
        {
            objLeaveReuqestDataContext = new leaverequestdalDataContext();
            DateTime dt = DateTime.MinValue;

            int iResultCode = objLeaveReuqestDataContext.sp_Get_Noofleaves(objLeaveRquestBo.PERNR,
                                                                                Convert.ToDateTime(objLeaveRquestBo.BEGDA),
                                                                                Convert.ToDateTime(objLeaveRquestBo.ENDDA),
                                                                                objLeaveRquestBo.AWART,
                                                                                ref firstday, ref lastday,
                                                                                ref count);

            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public int Update_Leave_Request_Details(leaverequestbo objLeaveRquestBo, ref bool? SuperVisorstatus, ref bool? HRstatus, ref string SuperVisorPernr,
                                   ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrName, ref string PernrEMail,
                                    ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {
            objLeaveReuqestDataContext = new leaverequestdalDataContext();
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(objLeaveRquestBo.ENDDA.ToString(), out dt))
            { }
            int iResultCode = 0;
            //int iResultCode = objLeaveReuqestDataContext.sp_wt_update_leave_request(Guid.Parse(objLeaveRquestBo.LEAVE_REQ_ID.ToString()),
            //                                                                    objLeaveRquestBo.PERNR,
            //                                                                    objLeaveRquestBo.BEGDA,
            //                                                                    dt,
            //                                                                    objLeaveRquestBo.BEGUZ,
            //                                                                    objLeaveRquestBo.ENDUZ,
            //                                                                    objLeaveRquestBo.AWART,
            //                                                                    objLeaveRquestBo.STDAZ,
            //                                                                    objLeaveRquestBo.NOTE,
            //                                                                    objLeaveRquestBo.APPROVED_BY,
            //                                                                   ref SuperVisorstatus, ref HRstatus,
            //                                                                    ref HRPernr, ref SuperVisorPernr,
            //                                                                    ref HRMail, ref SuperVisorMail,
            //                                                                    ref PernrName, ref PernrEMail,
            //                                                                    ref SuperVisorPhn, ref HRPhn);
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public leaverequestcollectionbo Get_LeaveOverview(string cc, leaverequestbo objLeaveRequestBo, ref int? RowCount)
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
        foreach (var vRow in objLeaveReuqestDataContext.sp_wt_get_leave_overview(cc, objLeaveRequestBo.PERNR.ToString()
            , objLeaveRequestBo.LEAVESINCE, objLeaveRequestBo.PageIndex, objLeaveRequestBo.PageSize, objLeaveRequestBo.Flag, ref RowCount))
        {
            leaverequestbo objBo = new leaverequestbo();
            objBo.RowNumber = (int)vRow.RowNumber;
            objBo.PERNR = vRow.PERNR;
            objBo.LEAVE_REQ_ID = vRow.ID;
            objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
            objBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
            objBo.STATUS = vRow.STATUS;
            objBo.AWART = vRow.AWART;
            objBo.ATEXT = vRow.ATEXT;
            //objBo.ANZHL = vRow.ANZHL.ToString();
            //objBo.KVERB = vRow.KVERB.ToString();
            objBo.TotalDays = vRow.TTL;
            //objBo.RecordCnt = vRow.RecordCnt;
            objBo.LTYPE = vRow.Ltype.ToString();
            objBo.BEGUZ = vRow.BEGUZ.ToString();
            objBo.ENDUZ = vRow.ENDUZ.ToString();
            objLeaveRequestLst.Add(objBo);
        }
        // objLeaveReuqestDataContext.Dispose();
        return objLeaveRequestLst;
    }

    public leaverequestcollectionbo Get_Leaveattd_srch(string cc, leaverequestbo objLeaveRequestBo)
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
        foreach (var vRow in objLeaveReuqestDataContext.sp_wt_get_leaveaddt_overviewmainsrch(cc, objLeaveRequestBo.PERNR.ToString()
            , objLeaveRequestBo.LEAVESINCE,objLeaveRequestBo.Flag))
        {
            leaverequestbo objBo = new leaverequestbo();
            objBo.PERNR = vRow.PERNR;
            objBo.LEAVE_REQ_ID = vRow.ID;
            objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
            objBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
            objBo.STATUS = vRow.STATUS;
            objBo.AWART = vRow.AWART;
            objBo.ATEXT = vRow.ATEXT;
            objBo.TotalDays = vRow.TTL;
            objBo.LTYPE = vRow.Ltype.ToString();
            objBo.BEGUZ = vRow.BEGUZ.ToString();
            objBo.ENDUZ = vRow.ENDUZ.ToString();
            objLeaveRequestLst.Add(objBo);
        }
        return objLeaveRequestLst;
    }

    public leaverequestcollectionbo Get_LeaveQuota(leaverequestbo objLeaveRequestBo, string compcode)
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
        foreach (var vRow in objLeaveReuqestDataContext.sp_wt_get_leave_quota(objLeaveRequestBo.PERNR.ToString(), objLeaveRequestBo.YEAR.ToString(), compcode))
        {
            //leaverequestbo objBo = new leaverequestbo();
            //objBo.LEAVE_QUOTA_START_DATE = Convert.ToDateTime(vRow.BEGDA);
            //objBo.LEAVE_QUOTA_END_DATE = Convert.ToDateTime(vRow.ENDDA);
            //objBo.ANZHL = Convert.ToInt32(vRow.ANZHL);
            //objBo.KVERB = Convert.ToInt32(vRow.KVERB);
            //objBo.ATEXT = vRow.Leave_type;
            //objBo.AVAILABLE_DAYS = Convert.ToDouble(vRow.Available);
            //objBo.AWART = vRow.AWART;

            leaverequestbo objBo = new leaverequestbo();
            objBo.RowNumber = (int)vRow.RowNumber;
            objBo.LEAVE_QUOTA_START_DATE = Convert.ToDateTime(vRow.DESTA);
            objBo.LEAVE_QUOTA_END_DATE = Convert.ToDateTime(vRow.DEEND);
            objBo.ANZHL = vRow.ANZHL.ToString();
            objBo.KVERB = vRow.KVERB.ToString();
            objBo.ATEXT = vRow.KTEXT;
            objBo.AVAILABLE_DAYS = vRow.available.ToString();
            //objBo.AWART = vRow.AWART;

            objLeaveRequestLst.Add(objBo);
        }
        // objLeaveReuqestDataContext.Dispose();
        return objLeaveRequestLst;
    }
    public leaverequestcollectionbo Get_Calendar_Leave_Markings(leaverequestbo objLeaveRequestBo)
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
        foreach (var vRow in objLeaveReuqestDataContext.sp_wt_get_calendar_leave_markings(objLeaveRequestBo.PERNR, objLeaveRequestBo.FROM_DATE, objLeaveRequestBo.TO_DATE))
        {
            leaverequestbo objBo = new leaverequestbo();
            objBo.PERNR = vRow.PERNR;
            objBo.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
            objBo.STATUS = vRow.STATUS;
            objBo.DATUM = vRow.DATUM;
            objBo.AWART = vRow.AWART;
            objBo.ATEXT = vRow.ATEXT;
            //objBo.HR_STATUS = vRow.hr_status;
            objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
            objBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
            DateTime dt = DateTime.MinValue;
            objBo.PKEY = vRow.PKEY;
            objBo.NOTE = vRow.NOTE;
            objBo.BEGUZ = vRow.BEGUZ.ToString();
            objBo.ENDUZ = vRow.ENDUZ.ToString();
            objBo.DURATIONTEXT = vRow.duration;

            objLeaveRequestLst.Add(objBo);
        }
        //objLeaveReuqestDataContext.Dispose();
        return objLeaveRequestLst;
    }



    public leaverequestcollectionbo Get_Calendar_Leave_Markings_ForRWT(leaverequestbo objLeaveRequestBo, int Type)
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
        foreach (var vRow in objLeaveReuqestDataContext.sp_wt_get_calendar_leave_markings_forRWT(objLeaveRequestBo.PERNR, objLeaveRequestBo.COMMENTS, objLeaveRequestBo.FROM_DATE, objLeaveRequestBo.TO_DATE, Type))
        {
            leaverequestbo objBo = new leaverequestbo();
            objBo.PERNR = vRow.PERNR;
            objBo.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
            objBo.STATUS = vRow.STATUS;
            objBo.DATUM = vRow.DATUM;
            objBo.AWART = vRow.AWART;
            objBo.ATEXT = vRow.ATEXT;
            objBo.STDAZ = vRow.STDAZ;
            //objBo.HR_STATUS = vRow.hr_status;
            objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
            objBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
            DateTime dt = DateTime.MinValue;
            objBo.PKEY = vRow.PKEY;
            objBo.NOTE = vRow.NOTE;
            objBo.DURATIONTEXT = vRow.duration;



            objLeaveRequestLst.Add(objBo);
        }
        //objLeaveReuqestDataContext.Dispose();
        return objLeaveRequestLst;
    }

    ////public leaverequestcollectionbo Get_Calendar_Leave_Markings(leaverequestbo objLeaveRequestBo)
    ////{
    ////    leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
    ////    foreach (var vRow in objLeaveReuqestDataContext.sp_wt_get_calendar_leave_markings(objLeaveRequestBo.PERNR, objLeaveRequestBo.FROM_DATE, objLeaveRequestBo.TO_DATE))
    ////    {
    ////        leaverequestbo objBo = new leaverequestbo();
    ////        objBo.PERNR = vRow.PERNR;
    ////        objBo.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
    ////        objBo.STATUS = vRow.STATUS;
    ////        objBo.DATUM = vRow.DATUM;
    ////        objBo.AWART = vRow.AWART;
    ////        objBo.ATEXT = vRow.ATEXT;
    ////        objBo.HR_STATUS = vRow.hr_status;
    ////        objBo.STDAZ = vRow.STDAZ;
    ////        objBo.NOTE = vRow.NOTE;
    ////        objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
    ////        DateTime dt = DateTime.MinValue;
    ////        if (vRow.ENDDA == dt)
    ////        {
    ////            objBo.ENDDA = string.Empty;
    ////        }
    ////        else
    ////        {
    ////            objBo.ENDDA = Convert.ToDateTime(vRow.ENDDA).ToString(); ;
    ////        }
    ////        objLeaveRequestLst.Add(objBo);
    ////    }
    ////    objLeaveReuqestDataContext.Dispose();
    ////    return objLeaveRequestLst;
    ////}






    public leaverequestcollectionbo Get_Individual_Leave_Dtls(string cc, leaverequestbo objLeaveRequestBo)
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();

        foreach (var vRow in objLeaveReuqestDataContext.sp_wt_get_individual_leave_details(cc, objLeaveRequestBo.PERNR))
        {
            leaverequestbo objBo = new leaverequestbo();
            objBo.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
            objBo.AWART = vRow.AWART;
            objBo.ATEXT = vRow.ATEXT;
            objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
            objBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
            objBo.STATUS = vRow.STATUS;
            objBo.TABLETYPE = vRow.TABLE_TYPE;
            objBo.TotalDays = vRow.Ttl;
            objBo.BEGUZ = vRow.BEGUZ.ToString();
            objBo.ENDUZ = vRow.ENDUZ.ToString();
            //objBo.STDAZ = vRow.STDAZ.ToString();
            //objBo.APPROVED_BY = vRow.APPROVED_BY;
            //objBo.NOTE = vRow.NOTE;
            // objLeaveRequestLst.Add(objBo);
            objLeaveRequestLst.Add(objBo);
        }

        //objLeaveReuqestDataContext.Dispose();

        return objLeaveRequestLst;
    }




    public leaverequestcollectionbo Get_Individual_Leave_Dtls_RWT(leaverequestbo objLeaveRequestBo)
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();



        foreach (var vRow in objLeaveReuqestDataContext.sp_wt_get_individual_leave_detailsRWT(objLeaveRequestBo.PERNR, objLeaveRequestBo.COMMENTS, objLeaveRequestBo.DATUM))
        {
            leaverequestbo objBo = new leaverequestbo();
            objBo.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
            objBo.AWART = vRow.AWART;
            objBo.ATEXT = vRow.ATEXT;
            objBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
            objBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
            objBo.STATUS = vRow.STATUS;
            objBo.TABLETYPE = vRow.TABLE_TYPE;
            objBo.TotalDays = vRow.Ttl;
            //objBo.BEGUZ = vRow.BEGUZ;
            //objBo.ENDUZ = vRow.ENDUZ;
            //objBo.STDAZ = vRow.STDAZ.ToString();
            //objBo.APPROVED_BY = vRow.APPROVED_BY;
            //objBo.NOTE = vRow.NOTE;
            // objLeaveRequestLst.Add(objBo);
            objLeaveRequestLst.Add(objBo);
        }



        // objLeaveReuqestDataContext.Dispose();



        return objLeaveRequestLst;
    }

    public void Update_Leave_Request_As_Deletion_Requested(leaverequestbo objLeaveRquestBo)
    {
        try
        {
            objLeaveReuqestDataContext = new leaverequestdalDataContext();
            objLeaveReuqestDataContext.sp_wt_delete_leave_req(objLeaveRquestBo.PERNR, Guid.Parse(objLeaveRquestBo.LEAVE_REQ_ID.ToString()));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public leaverequestcollectionbo Get_Team_Calendar_Leave_Markings(leaverequestbo objLeaveRequestBo, string MngrPernr)
    {
        leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
        foreach (var vRow in objLeaveReuqestDataContext.sp_wt_get_team_calendar_leave_markings(objLeaveRequestBo.PERNR, objLeaveRequestBo.FROM_DATE, objLeaveRequestBo.TO_DATE, MngrPernr))
        {
            leaverequestbo objBo = new leaverequestbo();
            objBo.PERNR = vRow.PERNR;
            objBo.EMPLOYEE_NAME = vRow.ENAME;
            objBo.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
            objBo.STATUS = vRow.STATUS;
            objBo.DATUM = vRow.DATUM;
            objLeaveRequestLst.Add(objBo);
        }
        //objLeaveReuqestDataContext.Dispose();
        return objLeaveRequestLst;
    }


    //-------------------- LEAVE VALIDATION ---------------
    public bool? LeaveValidation(string compcode, leaverequestbo ObjLeaveReq, int HDType, ref bool? IsValid)
    {
        leaverequestdalDataContext objLeaveReuqestDataContext = new leaverequestdalDataContext();
        objLeaveReuqestDataContext.sp_wt_leave_validation_Prism(compcode, ObjLeaveReq.PERNR, ObjLeaveReq.BEGDA, ObjLeaveReq.ENDDA, ObjLeaveReq.AWART, ObjLeaveReq.BEGUZ, ObjLeaveReq.ENDUZ, ObjLeaveReq.STDAZ, ObjLeaveReq.Flag, HDType, ref IsValid);

        //objLeaveReuqestDataContext.Dispose();
        return IsValid;
    }

    



    public List<leaverequestbo> Approval_LeaveDetails_Mail(string pkey, int Id, string sts, string tbltyp)
    {
        leaverequestdalDataContext objLeaveDataContext = new leaverequestdalDataContext();
        List<leaverequestbo> LeaveobjList = new List<leaverequestbo>();
        foreach (var vRow in objLeaveDataContext.sp_pi_get_Leaveinfo_mail(pkey, Id, sts, tbltyp))
        {
            leaverequestbo LeaveObj = new leaverequestbo();

            LeaveObj.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
            LeaveObj.PKEY = vRow.PKEY;
            LeaveObj.PERNR = vRow.created_by;
            LeaveObj.BEGDA = vRow.BEGDA;
            LeaveObj.ENDDA = vRow.ENDDA;
            LeaveObj.BEGUZ = vRow.BEGUZ.ToString();
            LeaveObj.ENDUZ = vRow.ENDUZ.ToString();
            LeaveObj.AWART = vRow.AWART;
            LeaveObj.STDAZ = vRow.STDAZ;
            LeaveObj.NOTE = vRow.NOTE;
            LeaveObj.APPROVED_BY = vRow.approved_by;
            LeaveObj.CREATED_ON = vRow.created_on;
            LeaveObj.APPROVED_ON = vRow.approved_on;
            LeaveObj.ATEXT = vRow.ATEXT;
            LeaveObj.REMARKS = vRow.approver_comments;
            LeaveObj.EMPLOYEE_NAME = vRow.empname == null ? "" : vRow.empname;
            LeaveObj.APPROVED_BY_NAME = vRow.mgrpnanme == null ? "" : vRow.mgrpnanme;
            LeaveObj.DURATION = vRow.Duration;

            LeaveobjList.Add(LeaveObj);

        }
        // objLeaveReuqestDataContext.Dispose();
        return LeaveobjList;
    }


    public List<leaverequestbo> Deletion_LeaveDetails_Mail(string pkey, int Id, string sts, string tbltyp)
    {
        leaverequestdalDataContext objLeaveDataContext = new leaverequestdalDataContext();
        List<leaverequestbo> LeaveobjList = new List<leaverequestbo>();
        foreach (var vRow in objLeaveDataContext.sp_pi_get_DeleteLeaveinfo_mail(pkey, Id, sts, tbltyp))
        {
            leaverequestbo LeaveObj = new leaverequestbo();

            LeaveObj.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
            LeaveObj.PKEY = vRow.PKEY;
            LeaveObj.PERNR = vRow.created_by;
            LeaveObj.BEGDA = vRow.BEGDA;
            LeaveObj.ENDDA = vRow.ENDDA;
            LeaveObj.BEGUZ = vRow.BEGUZ.ToString();
            LeaveObj.ENDUZ = vRow.ENDUZ.ToString();
            LeaveObj.AWART = vRow.AWART;
            LeaveObj.STDAZ = vRow.STDAZ;
            LeaveObj.NOTE = vRow.NOTE;
            LeaveObj.APPROVED_BY = vRow.approved_by;
            LeaveObj.CREATED_ON = vRow.created_on;
            LeaveObj.APPROVED_ON = vRow.approved_on;
            LeaveObj.ATEXT = vRow.ATEXT;
            LeaveObj.REMARKS = vRow.approver_comments;
            LeaveObj.EMPLOYEE_NAME = vRow.empname;
            LeaveObj.APPROVED_BY_NAME = vRow.mgrpnanme;
            LeaveObj.DURATION = vRow.Duration;

            LeaveobjList.Add(LeaveObj);

        }
        //objLeaveReuqestDataContext.Dispose();
        return LeaveobjList;
    }

    public List<leaverequestbo> Load_HolidayCalendar(string year, string comp)
    {
        leaverequestdalDataContext objLeaveReuqestDataContext = new leaverequestdalDataContext();
        List<leaverequestbo> LeaveReqboList = new List<leaverequestbo>();
        foreach (var vRow in objLeaveReuqestDataContext.sp_load_HolidayCalendar(year, comp))
        {
            leaverequestbo LeaveReqboObj = new leaverequestbo();
            LeaveReqboObj.HOLIDAYDATE = vRow.DATE;
            LeaveReqboObj.HOLIDAYS = vRow.holiday;
            //LeaveReqboObj.KLASS = vRow.KLASS;
            LeaveReqboObj.KLASSTXT = vRow.KLASSTXT;
            //LeaveReqboObj.HCALLID = vRow.HCALID;
            LeaveReqboList.Add(LeaveReqboObj);
        }
        // objLeaveReuqestDataContext.Dispose();
        return LeaveReqboList;
    }


    //--------------- CLOCK-IN ---- CLOCK-OUT ----------------------------
    public leaverequestcollectionbo Get_Emp_ClockIn_ClockOut(string PERNR, string MngrPernr, DateTime FromDate, DateTime ToDate, int PageIndex, int PageSize, int flag, string ccode)
    {
        try
        {
            leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
            foreach (var vRow in objLeaveReuqestDataContext.sp_ms_Load_ClockInOutReview_new(PERNR, MngrPernr, FromDate, ToDate, PageIndex, PageSize, flag, ccode))
            {
                leaverequestbo objBo = new leaverequestbo();

                objBo.ROWNUMBERCICO = long.Parse(vRow.RowNumber.ToString());
                objBo.PERNR = vRow.PERNR;
                objBo.DATES = vRow.DATES;
                objBo.DATES1 = vRow.DATES1;
                objBo.AVAILABLE_DAYS = vRow.DAY;
                objBo.PUNCH_IN = vRow.Punch_In;
                objBo.PUNCH_OUT = vRow.Punch_Out;
                objBo.TOTAL_HOURS = vRow.Total_No_of_Hours;
                objBo.brkhrs = vRow.Total_Break_Hours;
                objBo.STATUS = vRow.STATUS;
                objBo.TXT_LONG = vRow.TXT_LONG;
                objBo.TOTAL_DAYS = int.Parse(vRow.TtlDays.ToString());
                objBo.PIPORECCOUNT = long.Parse(vRow.RecordCnt.ToString());
                objBo.count = vRow.count;
                objLeaveRequestLst.Add(objBo);
            }
            objLeaveReuqestDataContext.Dispose();
            return objLeaveRequestLst;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public leaverequestcollectionbo Get_Emp_ClockIn_ClockOutfull(string PERNR, DateTime FromDate, DateTime ToDate, DateTime rowdate, int flag, string ccode)
    {
        try
        {
            leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
            foreach (var vRow in objLeaveReuqestDataContext.sp_ms_Load_ClockInOutReview_full(PERNR, FromDate, rowdate, ToDate, flag, ccode))
            {
                leaverequestbo objBo = new leaverequestbo();

                //objBo.PERNR = vRow.PERNR1;
                objBo.DATES = vRow.dt;
                //objBo.DATES1 = vRow.DateNames1;
                objBo.PUNCH_IN = vRow.Punch_In;
                objBo.PUNCH_OUT = vRow.Punch_Out;
                objBo.TOTAL_HOURS = vRow.Total_No_of_Hours;
                //objBo.KVERB = vRow.Day;
                objLeaveRequestLst.Add(objBo);
            }
            objLeaveReuqestDataContext.Dispose();
            return objLeaveRequestLst;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    //--------------- PUPNCH-IN -OUT REPORT----------------------------
    public leaverequestcollectionbo Get_PunchInOutReport(string cc,string PERNR, string MngrPernr, DateTime FromDate, DateTime ToDate, int PageIndex, int PageSize, int flag, ref decimal? WeekoffCOunt, ref decimal? TotalDAys, ref  decimal? TotalHolidays, ref decimal? TotalWrkingDAys, ref decimal? TotalHours, ref decimal? HrsDay, ref int? RecCount)
    {
        try
        {
            leaverequestcollectionbo objLeaveRequestLst = new leaverequestcollectionbo();
            foreach (var vRow in objLeaveReuqestDataContext.sp_ms_get_PunchInOutDetails_Rprt(cc,PERNR, MngrPernr, FromDate, ToDate, PageIndex, PageSize, flag, ref WeekoffCOunt, ref TotalDAys, ref TotalHolidays, ref TotalWrkingDAys, ref TotalHours, ref HrsDay, ref RecCount))
            {
                leaverequestbo objBo = new leaverequestbo();

                objBo.ROWNUMBERREPORT = long.Parse(vRow.RowNumber.ToString());
                objBo.PERNR = vRow.EmpPernnr;
                objBo.EMPLOYEE_NAME = vRow.ENAME;
                objBo.TWORKINGDAYS = vRow.Tworkingdays;
                objBo.TOTALHRS = vRow.totalhrs;
                objBo.TOLHRSBYEMP = vRow.tolhrsbyemp;
                objBo.AVGAVAILABILITY = vRow.avgavailability;
                //objBo.PUNCHIN = vRow.punchin;
                //objBo.PUNCHOUT = vRow.punchout;
                objBo.MISSEDPUNCH = vRow.MissedPunch;
                objBo.LEAVECOUNTD = vRow.Leavecountd;
                objBo.REGULARATTDNCE = vRow.ReqAttendanceCount;
                objBo.DATEOFJOINING = DateTime.Parse(vRow.DateofJoining.ToString()).ToString("yyyy-MM-dd");


                objLeaveRequestLst.Add(objBo);
            }
            objLeaveReuqestDataContext.Dispose();
            return objLeaveRequestLst;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }




}
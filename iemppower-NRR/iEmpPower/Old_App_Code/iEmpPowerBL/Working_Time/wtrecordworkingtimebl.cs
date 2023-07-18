using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Working_Time;

/// <summary>
/// Summary description for recordworkingtimebl
/// </summary>
public class wtrecordworkingtimebl
{
    public wtrecordworkingtimebl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    wtrecordworkingtimedalDataContext objRecordDataContex = new wtrecordworkingtimedalDataContext();
    public int Create_RecordWorkingTime(wtrecordworkingtimebo objRecordBo, ref bool? SuperVisorstatus, ref string SuperVisorPernr,
                                   ref string SuperVisorMail, ref string PernrName, ref string PernrEMail, ref string ErrorMessage, ref string Pkey)
    {
        try
        {
            int iResultCode = objRecordDataContex.sp_wt_create_recordworking_time(objRecordBo.PERNR, objRecordBo.AWART, objRecordBo.CATSHOURS, objRecordBo.WORKING_DATE, objRecordBo.COST_CENTER,
                                                                               objRecordBo.ORDER, objRecordBo.RPROJ, objRecordBo.RNPLNR, objRecordBo.LSTAR, objRecordBo.REMARKS, objRecordBo.TID, objRecordBo.PLNDHRS, objRecordBo.Ccode,
                                                                               ref SuperVisorstatus, ref SuperVisorPernr, ref SuperVisorMail, ref PernrName,
                                                                               ref PernrEMail, ref  ErrorMessage, ref Pkey);

            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public wtrecordworkingtimecollectionbo Get_RecordDetails(wtrecordworkingtimebo objRecordBo)
    {
        wtrecordworkingtimecollectionbo objRecordLst = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objRecordDataContex.sp_wt_get_recordworking_time(objRecordBo.PERNR,objRecordBo.COMMENTS, objRecordBo.FROM_DATE, objRecordBo.TO_DATE))
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.CURRENTRECORD_NO = vRow.CURRECORDNO.ToString();
            objBo.AWART = vRow.AWART.Trim();
            objBo.CATSHOURS = vRow.CATSHOURS;
            objBo.WORKING_DATE = vRow.WORKINGDATE.ToString();
            objBo.DAYS = vRow.day;
            objBo.ORDER = vRow.order_type;
            objBo.COST_CENTER = vRow.cost_center;
            objBo.RPROJ = vRow.RPROJ;
            objBo.RNPLNR = vRow.RNPLNR;
            objBo.LSTAR = vRow.VORNR;
            objBo.STS = vRow.status;
            objBo.PLNDHRS = vRow.Plndhrs;
            objBo.TID = vRow.TID.ToString();
            //objBo.KTEXT = vRow.KTEXT;
            //objBo.LTEXT = vRow.LTEXT;
            objRecordLst.Add(objBo);
        }
        return objRecordLst;
    }


    public int Update_RecordWorkingTime(wtrecordworkingtimebo objRecordBo, ref bool? SuperVisorstatus, ref string SuperVisorPernr,
                                  ref string SuperVisorMail, ref string PernrName, ref string PernrEMail, ref string ErrorMessage)
    {
        try
        {
            int iResultCodee = objRecordDataContex.sp_wt_update_recordworking_time(objRecordBo.CURRENTRECORD_NO.ToString(),
                                                                                objRecordBo.PERNR, objRecordBo.AWART, objRecordBo.CATSHOURS, objRecordBo.WORKING_DATE, objRecordBo.COST_CENTER,
                                                                                objRecordBo.ORDER, objRecordBo.RPROJ, objRecordBo.RNPLNR, objRecordBo.LSTAR, objRecordBo.REMARKS, objRecordBo.TID, objRecordBo.PLNDHRS, objRecordBo.Ccode,
                                                                                ref SuperVisorstatus, ref SuperVisorPernr, ref SuperVisorMail, ref PernrName, ref PernrEMail, ref ErrorMessage);






            return iResultCodee;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public wtrecordworkingtimecollectionbo Loak_WorkingHours(wtrecordworkingtimebo objRecordBo)
    {
        wtrecordworkingtimecollectionbo objRecordLst = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objRecordDataContex.sp_wt_load_working_hours_for_employee(objRecordBo.PERNR))
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.ARBST = vRow.ARBST.ToString();
            objRecordLst.Add(objBo);
        }
        return objRecordLst;
    }
    public wtrecordworkingtimecollectionbo Get_RecordDetails_Week(wtrecordworkingtimebo objRecordBo)
    {
        wtrecordworkingtimecollectionbo objRecordLst = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objRecordDataContex.sp_wt_get_recordworking_time_week(objRecordBo.PERNR,objRecordBo.COMMENTS, objRecordBo.FROM_DATE, objRecordBo.TO_DATE))
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.CURRENTRECORD_NO = vRow.CURRECORDNO.ToString();
            objBo.AWART = vRow.AWART.Trim();
            objBo.SUNDAY = vRow.Sunday;
            objBo.MONDAY = vRow.Monday;
            objBo.TUESDAY = vRow.Tuesday;
            objBo.WEDNESDAY = vRow.Wednesday;
            objBo.THURSDAY = vRow.Thursday;
            objBo.FRIDAY = vRow.Friday;
            objBo.SATURDAY = vRow.Saturday;
            objBo.ORDER = vRow.order_type;
            objBo.COST_CENTER = vRow.cost_center;
            objBo.RPROJ = vRow.RPROJ;
            objBo.RNPLNR = vRow.RNPLNR;
            objBo.LSTAR = vRow.VORNR;
            objBo.REMARKS = vRow.REMARKS;
            objBo.TID = vRow.TID.ToString();
            objBo.PLNDHRS = vRow.Plndhrs;

            objRecordLst.Add(objBo);
        }
        return objRecordLst;
    }



    public wtrecordworkingtimecollectionbo Get_RecordDetails_ForMail(wtrecordworkingtimebo objRecordBo, ref string SupervisorMail, ref string EmpMail, ref string EmpName, ref string SupervisiorName, ref string EmpId)
    {
        // objLstOne = objBl.Get_RecordDetails_ForMail(objBo, ref SupervisiorName, ref EmpMail, ref EmpName, ref SupervisorMail, ref EmpId);
        wtrecordworkingtimecollectionbo objRecordLst = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objRecordDataContex.sp_wt_get_recordworking_time_Mail(objRecordBo.PKEY,objRecordBo.COMMENTS, ref SupervisorMail, ref EmpMail, ref EmpName, ref SupervisiorName, ref EmpId))
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.MINDATERWT = vRow.MinDate.ToString();
            objBo.MAXDATERWT = vRow.MaxDate.ToString();

            objRecordLst.Add(objBo);
        }
        return objRecordLst;
    }



    public wtrecordworkingtimecollectionbo LoadTeamEmpGridTask(string MngrID, string empid, DateTime fromdate, DateTime todate)
    {
        wtrecordworkingtimedalDataContext objRecordDataContex = new wtrecordworkingtimedalDataContext();
        wtrecordworkingtimecollectionbo ActivityboList = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objRecordDataContex.sp_Assigntask_SubEmp(MngrID, empid, fromdate, todate))
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.EMPID = vRow.PERNR;
            objBo.ACTIVITYID = vRow.ACTIVITYID;
            objBo.MONWORKDATE = vRow.Monday;
            objBo.MONHOURS = string.IsNullOrEmpty(vRow.MondayHours) ? 0 : decimal.Parse(vRow.MondayHours);
            objBo.MONTASK = string.IsNullOrEmpty(vRow.MondayTask) ? "" : vRow.MondayTask;
            objBo.TUEWORKDATE = vRow.Tuesday;
            objBo.TUEHOURS = string.IsNullOrEmpty(vRow.TuesdayHours) ? 0 : decimal.Parse(vRow.TuesdayHours);
            objBo.TUETASK = string.IsNullOrEmpty(vRow.TuesdayTask) ? "" : vRow.TuesdayTask;
            objBo.WEDWORKDATE = vRow.Wednesday;
            objBo.WEDHOURS = string.IsNullOrEmpty(vRow.WednesdayHours) ? 0 : decimal.Parse(vRow.WednesdayHours);
            objBo.WEDTASK = string.IsNullOrEmpty(vRow.WednesdayTask) ? "" : vRow.WednesdayTask;
            objBo.THURWORKDATE = vRow.Thursday;
            objBo.THURHOURS = string.IsNullOrEmpty(vRow.ThursdayHours) ? 0 : decimal.Parse(vRow.ThursdayHours);
            objBo.THURTASK = string.IsNullOrEmpty(vRow.ThursdayTask) ? "" : vRow.ThursdayTask;
            objBo.FRIWORKDATE = vRow.Friday;
            objBo.FRIHOURS = string.IsNullOrEmpty(vRow.FridayHours) ? 0 : decimal.Parse(vRow.FridayHours);
            objBo.FRITASK = string.IsNullOrEmpty(vRow.FridayTask) ? "" : vRow.FridayTask;
            //objBo.ENAME = vRow.ENAME;
            //objBo.MODULE = vRow.EMP_DEPART;
            //LeaveReqboObj.KLASS = vRow.KLASS;
            //LeaveReqboObj.KLASSTXT = vRow.KLASSTXt;
            //LeaveReqboObj.HCALLID = vRow.HCALID;
            ActivityboList.Add(objBo);
        }
        // objLeaveReuqestDataContext.Dispose();
        return ActivityboList;
    }


    public wtrecordworkingtimecollectionbo LoadEmpGridTask(string empid, DateTime fromdate, DateTime todate)
    {
        wtrecordworkingtimedalDataContext objRecordDataContex = new wtrecordworkingtimedalDataContext();
        wtrecordworkingtimecollectionbo ActivityboList = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objRecordDataContex.sp_getEmpAssigndtask(empid, fromdate, todate))
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.EMPID = vRow.PERNR;
            objBo.ACTIVITYID = vRow.ACTIVITYID;
            objBo.MONWORKDATE = vRow.Tuesday;
            objBo.MONHOURS = decimal.Parse(vRow.MondayHours);
            objBo.MONTASK = vRow.MondayTask;
            objBo.TUEWORKDATE = vRow.Tuesday;
            objBo.TUEHOURS = decimal.Parse(vRow.TuesdayHours);
            objBo.TUETASK = vRow.TuesdayTask;
            objBo.WEDWORKDATE = vRow.Wednesday;
            objBo.WEDHOURS = decimal.Parse(vRow.WednesdayHours);
            objBo.WEDTASK = vRow.WednesdayTask;
            objBo.THURWORKDATE = vRow.Thursday;
            objBo.THURHOURS = decimal.Parse(vRow.ThursdayHours);
            objBo.THURTASK = vRow.ThursdayTask;
            objBo.FRIWORKDATE = vRow.Friday;
            objBo.FRIHOURS = decimal.Parse(vRow.FridayHours);
            objBo.FRITASK = vRow.FridayTask;
            objBo.TASKACTIVITY = vRow.AWART.ToString().Trim();
            //objBo.ENAME = vRow.ENAME;
            //objBo.MODULE = vRow.EMP_DEPART;
            //LeaveReqboObj.KLASS = vRow.KLASS;
            //LeaveReqboObj.KLASSTXT = vRow.KLASSTXt;
            //LeaveReqboObj.HCALLID = vRow.HCALID;
            ActivityboList.Add(objBo);
        }
        // objLeaveReuqestDataContext.Dispose();
        return ActivityboList;
    }


    public wtrecordworkingtimecollectionbo LoadViewPlannedActivities(string empid, DateTime fromdate, DateTime todate, string mngPernr)
    {
        wtrecordworkingtimedalDataContext objRecordDataContex = new wtrecordworkingtimedalDataContext();
        wtrecordworkingtimecollectionbo ActivityboList = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objRecordDataContex.sp_Load_PlannedActivities(empid, fromdate, todate, mngPernr))
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.EMPID = vRow.PERNR;
            objBo.ENAME = vRow.ENAME;
            //objBo.ACTIVITYID = vRow.ACTIVITYID;
            //objBo.TASKACTIVITY = vRow.awart;
            objBo.MODULE = vRow.EMP_DEPART;
            objBo.TASKWORKDATE = DateTime.Parse(vRow.WORKINGDATE.ToString()).ToString("dd-MMM-yyyy");
            objBo.TASKHOURS = string.IsNullOrEmpty(vRow.CATSHOURS) ? "0" : vRow.CATSHOURS;
            objBo.TASKWORK = string.IsNullOrEmpty(vRow.WORKASSIGNED) ? "" : vRow.WORKASSIGNED;
            ActivityboList.Add(objBo);
        }
        // objLeaveReuqestDataContext.Dispose();
        return ActivityboList;
    }


    public wtrecordworkingtimecollectionbo LoadTeamEmpGridTasktoCalendar(string MngrID, DateTime fromdate, DateTime todate)
    {
        wtrecordworkingtimedalDataContext objRecordDataContex = new wtrecordworkingtimedalDataContext();
        wtrecordworkingtimecollectionbo ActivityboList = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objRecordDataContex.sp_loadAssigntask_tocalendar(MngrID, fromdate, todate))
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.TASKWORKDATE = vRow.WORKINGDATE.ToString();
            ActivityboList.Add(objBo);
        }
        // objLeaveReuqestDataContext.Dispose();
        return ActivityboList;
    }

    public wtrecordworkingtimecollectionbo LoadTaskEmpGridTasktoCalendar(string EmpID, DateTime fromdate, DateTime todate)
    {
        wtrecordworkingtimedalDataContext objRecordDataContex = new wtrecordworkingtimedalDataContext();
        wtrecordworkingtimecollectionbo ActivityboList = new wtrecordworkingtimecollectionbo();
        foreach (var vRow in objRecordDataContex.sp_loadEmpAssigntask_tocalendar(EmpID, fromdate, todate))
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.TASKWORKDATE = vRow.WORKINGDATE.ToString();
            ActivityboList.Add(objBo);
        }
        // objLeaveReuqestDataContext.Dispose();
        return ActivityboList;
    }


    public int Create_AssignTaskActivities(wtrecordworkingtimebo objRecordBo, int flag)
    {
        try
        {
            int iResultCode = objRecordDataContex.sp_wt_create_AssignTaskActivities(objRecordBo.TaskID, objRecordBo.PERNR, objRecordBo.ACTIVITYID, objRecordBo.TASKWORKDATE,
                objRecordBo.TASKWORK, objRecordBo.TASKHOURS, objRecordBo.CREATEDONDATE, objRecordBo.CREATED_BYPERNR, objRecordBo.MNGRUPDATEDON, objRecordBo.MNGRUPDATEDBY,
                objRecordBo.EMPUPDATEON, objRecordBo.EMPUPDATEDBY, objRecordBo.TASKSTATUS, flag);

            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }


    public int Create_ActualTaskHours(wtrecordworkingtimebo objRecordBo)
    {
        try
        {
            int iResultCode = objRecordDataContex.sp_wt_create_ActualTaskHour(objRecordBo.TaskID, objRecordBo.PERNR, objRecordBo.TASKWORKDATE,
               objRecordBo.TASKHOURS, objRecordBo.CREATEDONDATE, objRecordBo.CREATED_BYPERNR, objRecordBo.MNGRUPDATEDON, objRecordBo.MNGRUPDATEDBY,
                objRecordBo.EMPUPDATEON, objRecordBo.EMPUPDATEDBY);

            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
}

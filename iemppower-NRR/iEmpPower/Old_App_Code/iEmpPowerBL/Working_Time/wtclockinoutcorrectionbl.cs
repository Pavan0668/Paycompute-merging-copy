using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Working_Time;
/// <summary>
/// Summary description for wtclockinoutcorrectionbl
/// </summary>
public class wtclockinoutcorrectionbl
{
    public wtclockinoutcorrectionbl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    wtclockinoutcorrectiondalDataContext objClockInOutrDataContext = new wtclockinoutcorrectiondalDataContext();
    public int Create_ClockInOutr(wtclockinoutcorrectionbo objClockIOBo, ref bool? SuperVisorstatus, ref bool? HRstatus, ref string SuperVisorPernr,
                                   ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrName, ref string PernrEMail,
                                    ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {

            int iResultCode = objClockInOutrDataContext.sp_wt_create_clock_in_out(objClockIOBo.PERNR, objClockIOBo.ALLDATES,
                                                                                  objClockIOBo.ALLTIMES, objClockIOBo.ALLTIME_TYPES,
                                                                                  objClockIOBo.NOTE, objClockIOBo.APPROVEDBY,
                                                                                  objClockIOBo.CREATEDON,
                                                                                  ref SuperVisorstatus, ref HRstatus,
                                                                                  ref HRPernr, ref SuperVisorPernr,
                                                                                  ref HRMail, ref SuperVisorMail,
                                                                                  ref PernrName, ref PernrEMail,
                                                                                  ref SuperVisorPhn, ref HRPhn);
            objClockInOutrDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public wtclockinoutcorrectioncollectionbo Get_ClockSDetails(wtclockinoutcorrectionbo objClockIOBo)
    {
        wtclockinoutcorrectioncollectionbo obClockIOLst = new wtclockinoutcorrectioncollectionbo();
        foreach (var vRow in objClockInOutrDataContext.sp_wt_get_clock_in_out(objClockIOBo.PERNR, objClockIOBo.FROMDATE, objClockIOBo.TODATE))
        {
            wtclockinoutcorrectionbo objBo = new wtclockinoutcorrectionbo();
            objBo.PDSNR = vRow.PDSNR;
            objBo.LTIME = vRow.LTIME;
            objBo.LDATE = Convert.ToDateTime(vRow.LDATE);
            objBo.SATZA = vRow.SATZA;
            objBo.SATZA_TYPE = vRow.ZTEXT;
            objBo.NOTE = vRow.NOTE;
            objBo.APPROVEDBY = vRow.APPROVED_BY.ToString();
            objBo.STATUS = vRow.status;
            objBo.ISUPDATE = vRow.isupdated;
            obClockIOLst.Add(objBo);
        }
        objClockInOutrDataContext.Dispose();
        return obClockIOLst;
    }
    public int Update_ClockInOutr(wtclockinoutcorrectionbo objClockIOBo, ref bool? SuperVisorstatus, ref bool? HRstatus, ref string SuperVisorPernr,
                                   ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrName, ref string PernrEMail,
                                    ref string SuperVisorPhn, ref string HRPhn)
    {
        try
        {
            wtclockinoutcorrectiondalDataContext objClockInOutrDataContext = new wtclockinoutcorrectiondalDataContext();
            int iResultCode = objClockInOutrDataContext.sp_wt_update_clock_in_out(objClockIOBo.ALLNUMBERS, objClockIOBo.PERNR, objClockIOBo.MODIFIEDON,
                                                                                  objClockIOBo.ALLDATES, objClockIOBo.ALLTIMES, objClockIOBo.ALLTIME_TYPES,
                                                                                  objClockIOBo.NOTE, objClockIOBo.APPROVEDBY, objClockIOBo.MODIFIEDON,
                                                                                  ref SuperVisorstatus, ref HRstatus,
                                                                                  ref HRPernr, ref SuperVisorPernr,
                                                                                  ref HRMail, ref SuperVisorMail,
                                                                                  ref PernrName, ref PernrEMail,
                                                                                  ref SuperVisorPhn, ref HRPhn); 
            objClockInOutrDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public int Delete_ClockInOutr(wtclockinoutcorrectionbo objClockIOBo, ref bool? SuperVisorstatus, ref bool? HRstatus, ref string SuperVisorPernr,
                                   ref string SuperVisorMail, ref string HRPernr, ref string HRMail, ref string PernrName, ref string PernrEMail)
    {
        try
        {
            wtclockinoutcorrectiondalDataContext objClockInOutrDataContext = new wtclockinoutcorrectiondalDataContext();
            int iResultCode = objClockInOutrDataContext.sp_wt_delete_clock_in_out(objClockIOBo.ALLNUMBERS, objClockIOBo.PERNR, objClockIOBo.MODIFIEDON,
                                                                                  objClockIOBo.ALLDATES, objClockIOBo.ALLTIMES, objClockIOBo.ALLTIME_TYPES,
                                                                                  objClockIOBo.NOTE, objClockIOBo.APPROVEDBY, objClockIOBo.MODIFIEDON,
                                                                                  ref SuperVisorstatus, ref HRstatus,
                                                                                  ref HRPernr, ref SuperVisorPernr,
                                                                                  ref HRMail, ref SuperVisorMail,
                                                                                  ref PernrName, ref PernrEMail); objClockInOutrDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
}
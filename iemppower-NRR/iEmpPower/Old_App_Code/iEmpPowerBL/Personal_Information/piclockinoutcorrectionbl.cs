using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;
/// <summary>
/// Summary description for piclockinoutcorrectionbl
/// </summary>
public class piclockinoutcorrectionbl
{
	public piclockinoutcorrectionbl()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    piclockinoutcorrectiondalDataContext objClockInOutrDataContext = new piclockinoutcorrectiondalDataContext();
    public int Create_ClockInOutr(piclockinoutcorrectionbo objClockIOBo)
    {
        try
        {

            int iResultCode = objClockInOutrDataContext.sp_pi_create_clock_in_out(objClockIOBo.PENNR,objClockIOBo.ALLDATES,
                                                                                  objClockIOBo.ALLTIMES,objClockIOBo.CREATEDON);
            objClockInOutrDataContext.Dispose();
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
}
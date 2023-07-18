using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Working_Time;
using System.Data;

/// <summary>
/// Summary description for wtpayslipbl
/// </summary>
public class wtpayslipbl
{
	public wtpayslipbl()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    wtpayslipDataContext objPaySlipDataContext = new wtpayslipDataContext();

    public wtpayslipcollectionbo Get_PayslipDetails(wtpayslipbo objPayslipBo)
    {
        wtpayslipcollectionbo objPayslipLst = new wtpayslipcollectionbo();
        foreach (var vRow in objPaySlipDataContext.sp_wt_load_payslip(objPayslipBo.PERNR, objPayslipBo.YEAR, objPayslipBo.MONTH)) 

        {
            wtpayslipbo objBo = new wtpayslipbo();
            objBo.PERNR = vRow.PERNR;
            objBo.MONTH = vRow.month.ToString();
            objBo.YEAR = vRow.year.ToString();
            objBo.PAYSLIP = vRow.payslip_content.ToString();
            objPayslipLst.Add(objBo);
        }

        return objPayslipLst;
    }

    public wtpayslipcollectionbo Get_TimeStatements(wtpayslipbo objPayslipBo)
    {
        wtpayslipcollectionbo objPayslipLst = new wtpayslipcollectionbo();
        foreach (var vRow in objPaySlipDataContext.sp_wt_load_timestatement(objPayslipBo.PERNR.ToString(), objPayslipBo.YEAR, objPayslipBo.MONTH))
        {
            wtpayslipbo objBo = new wtpayslipbo();
            objBo.PERNR = vRow.PERNR;
            objBo.MONTH = vRow.month.ToString();
            objBo.YEAR = vRow.year.ToString();
            objBo.PAYSLIP = vRow.timestatement.ToString();
            objPayslipLst.Add(objBo);
        }

        return objPayslipLst;
    }

    public wtpayslipcollectionbo Get_Form16Details(wtpayslipbo objPayslipBo)
    {
        wtpayslipcollectionbo objPayslipLst = new wtpayslipcollectionbo();
        foreach (var vRow in objPaySlipDataContext.sp_wt_load_form16(objPayslipBo.PERNR, objPayslipBo.YEAR))
        {
            wtpayslipbo objBo = new wtpayslipbo();
            objBo.PERNR = vRow.PERNR;
            objBo.YEAR = vRow.year.ToString();
            objBo.PAYSLIP = vRow.form16.ToString();
            objPayslipLst.Add(objBo);
        }

        return objPayslipLst;
    }

    public wtpayslipcollectionbo Get_Claim_Statement_Details(wtpayslipbo objPayslipBo)
    {
        wtpayslipcollectionbo objPayslipLst = new wtpayslipcollectionbo();
        foreach (var vRow in objPaySlipDataContext.sp_wt_load_claim_stmt(objPayslipBo.PERNR, objPayslipBo.YEAR,objPayslipBo.MONTH))
        {
            wtpayslipbo objBo = new wtpayslipbo();
            objBo.PERNR = vRow.PERNR;
            objBo.YEAR = vRow.Year.ToString();
            objBo.MONTH = vRow.Month.ToString();
            objBo.PAYSLIP = vRow.Statement.ToString();
            objPayslipLst.Add(objBo);
        }

        return objPayslipLst;
    }
}
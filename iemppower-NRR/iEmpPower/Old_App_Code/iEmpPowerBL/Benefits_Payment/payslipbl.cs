using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment
{
    public class payslipbl
    {
        payslipDataContext ObjDataContext = new payslipDataContext();
        public payslipbl()
        {
        }

        public payslipcollectionbo Get_Payslip(iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO.payslipbo ObjBo)
        {
            payslipcollectionbo ObjPayslipCollBo = new payslipcollectionbo();
            foreach (var vRow in ObjDataContext.usp_wt_load_payslip(ObjBo.Pernr, ObjBo.Year, ObjBo.FilePath, ObjBo.Flag))
            {
                iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO.payslipbo objBoPayslip = new iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO.payslipbo();
                objBoPayslip.Rownumber = (int)vRow.Rownumber;
                objBoPayslip.FileName = vRow.FileName;
                objBoPayslip.FilePath = vRow.FilePath;
                objBoPayslip.Name = vRow.Name;

                ObjPayslipCollBo.Add(objBoPayslip);
            }
            return ObjPayslipCollBo;
        }
    }
}
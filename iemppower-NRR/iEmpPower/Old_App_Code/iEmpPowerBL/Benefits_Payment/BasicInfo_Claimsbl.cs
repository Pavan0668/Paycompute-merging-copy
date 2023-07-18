using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment
{
    public class BasicInfo_Claimsbl
    {
        public BasicInfo_Claimsbl()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        public BasicInfo_Claimscollection Get_BasicInfo_Claims_Details(string strPERNR)
        {
            ExpenseDataContext objBasicClaimsDataContext = new ExpenseDataContext();

            BasicInfo_Claimscollection objList = new BasicInfo_Claimscollection();

            foreach (var vRow in objBasicClaimsDataContext.sp_Get_BasicInfo_Claims(strPERNR))
            {
                BasicInfo_Claimsbo objBasicClaimsBo = new BasicInfo_Claimsbo();

                objBasicClaimsBo.PERNR = vRow.PERNR;
                objBasicClaimsBo.ENAME = vRow.ENAME;
                objBasicClaimsBo.PLANS = vRow.PLANS;
                objBasicClaimsBo.WERKS =vRow.WERKS ;
                objBasicClaimsBo.PLSXT = vRow.PLSXT;
                objBasicClaimsBo.USRID = vRow.USRID;
                objBasicClaimsBo.S_PLANS = vRow.OBJID;
                objBasicClaimsBo.S_PERNR = vRow.S_PERNR;
                objBasicClaimsBo.S_NAME = vRow.S_NAME;
                objBasicClaimsBo.S_PLSXT = vRow.S_PLSXT;
                objBasicClaimsBo.S_USRID = vRow.S_USRID;

                objList.Add(objBasicClaimsBo);
            }
            objBasicClaimsDataContext.Dispose();

            return objList;
        }

        public string Get_HQ(string strPERNR)
        {
            string strState = string.Empty;
            string strHQ = string.Empty;

            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();


            objExpenseDataContext.sp_Get_HQ_and_State(strPERNR, ref strHQ, ref strState);

            objExpenseDataContext.Dispose();

            return strHQ;
        }

        public string Get_State(string strPERNR)
        {
            string strState = string.Empty;
            string strHQ = string.Empty;

            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();


            objExpenseDataContext.sp_Get_HQ_and_State(strPERNR, ref strHQ, ref strState);

            objExpenseDataContext.Dispose();

            return strState;
        }
    }
}
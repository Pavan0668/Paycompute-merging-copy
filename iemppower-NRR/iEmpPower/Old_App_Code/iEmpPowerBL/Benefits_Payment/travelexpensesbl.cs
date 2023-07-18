using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;

/// <summary>
/// Summary description for travelexpensesbl
/// </summary>
public class travelexpensesbl
{
	public travelexpensesbl()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void Create_TravelExpenses(travelexpensecolumnsbo objBo,
                                        ref bool? IS_SUPERVISR_APPROVAL_REQ,
                                        ref bool? IS_HR_APPROVAL_REQ,
                                        ref bool? IS_PAYROLLADMIN_APPROVAL_REQ,
                                        ref string Super_Pernr,
                                        ref string Super_Name,
                                        ref string Super_Mail,
                                        ref string Hr_Pernr,
                                        ref string Hr_Name,
                                        ref string Hr_Mail,
                                        ref string Payroll_Pernr,
                                        ref string Payroll_Name,
                                        ref string Payroll_Mail,
                                        ref string Pernr_Mail,
                                        ref bool? SaveStatus)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            //objTravelRequestDataContext.sp_bnp_create_travel_expenses(objBo.TRAVEL_REQUEST_NO,
            //                                                        objBo.TRAVEL_PERNR,
            //                                                        objBo.ER_EXPENSE_RECEIPTS_LIST,
            //                                                        objBo.ER_PAPER_RECEIPT_LIST,
            //                                                        objBo.ER_AMOUNT_LIST,
            //                                                        objBo.ER_NUMBER_LIST,
            //                                                        objBo.ER_DESCRIPTION_LIST,
            //                                                        objBo.ER_ADDITIONAL_INFO_COUNTRY_LIST,
            //                                                        objBo.ER_ADDITIONAL_INFO_CITY_LIST,
            //                                                        objBo.ER_NO_OF_EMPLOYEES_LIST,
            //                                                        objBo.ER_NO_OF_GUESTS_LIST,
            //                                                        ref IS_SUPERVISR_APPROVAL_REQ,
            //                                                        ref IS_HR_APPROVAL_REQ,
            //                                                        ref IS_PAYROLLADMIN_APPROVAL_REQ,
            //                                                        ref Super_Pernr,
            //                                                        ref Super_Name,
            //                                                        ref Super_Mail,
            //                                                        ref Hr_Pernr,
            //                                                        ref Hr_Name,
            //                                                        ref Hr_Mail,
            //                                                        ref Payroll_Pernr,
            //                                                        ref Payroll_Name,
            //                                                        ref Payroll_Mail,
            //                                                        ref Pernr_Mail,
            //                                                        ref SaveStatus);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
}
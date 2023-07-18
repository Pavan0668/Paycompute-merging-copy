using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment
{
    public class Expensebl
    {

        public Expensebl()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public void Create_Expense(Expensebo objBo, ref bool? SaveStatus)
        {
            try
            {
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

                objExpenseDataContext.sp_create_expense(objBo.PERNR, objBo.ENAME, objBo.WERKS, objBo.PLSXT, objBo.STEXT, objBo.REPORT_TO, objBo.BEGDA, objBo.ENDDA,
                                                         objBo.DA_DEDUCTION, objBo.TA_DEDUCTION, objBo.OTR_EXP_DEDUCT, objBo.TOT_INCHARGE_PERSON_DEDUC, objBo.CORPORATE_OFFICE_DEDUC,
                                                         objBo.TOT_DEDUC, objBo.AMOUNT_ALLOWED, objBo.STATIONARY, objBo.COURIER, objBo.PANDT, objBo.EMAIL, objBo.COMP_PRODUCT_PURCHASE,
                                                         objBo.OTHERS, objBo.MARK_DEVELOP_EXPENCE, objBo.BUS_PASS, objBo.CONVEYANCE, objBo.JC_MEETINGS, objBo.GRAND_TOTAL,
                                                         objBo.Status, objBo.entered_by, objBo.approved_on, ref SaveStatus);

                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public void Create_Expense_Inward_Entry(Expensebo objBo, ref bool? SaveStatus)
        {
            try
            {
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

                objExpenseDataContext.sp_create_expense_inward_entry(objBo.PERNR, objBo.ENAME, objBo.WERKS, objBo.PLSXT, objBo.STEXT, objBo.REPORT_TO, objBo.BEGDA, objBo.ENDDA, objBo.Status, objBo.approved_by, objBo.approved_on, objBo.AMOUNT,
                                                         ref SaveStatus);

                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public void Create_Expense_payment_reimburse(Expensebo objBo, ref bool? SaveStatus)
        {
            try
            {
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

                objExpenseDataContext.sp_create_Expense_payment_reimburse(objBo.PERNR, objBo.ENAME, objBo.BEGDA, objBo.CLAIMED, objBo.AMOUNT_ALLOWED, objBo.TOT_DEDUC, objBo.REIMBURSE, objBo.approved_by, objBo.approved_on, ref SaveStatus);

                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public void Create_Expense_Statement(Expense_statementbo objBo, ref bool? SaveStatus)
        {
            try
            {
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

                objExpenseDataContext.sp_create_expense_statement(objBo.PERNR, objBo.BEGIN_DATE, objBo.END_DATE, objBo.DATE1, objBo.PLACE_WORK, objBo.NIGHT_HALT, objBo.CATEGORY, objBo.CATEGORYofplace, objBo.LODGE_BILLS,
                                                                  objBo.BILL_AMOUNT, objBo.TRAVEL_FROM, objBo.TRAVEL_Via, objBo.TRAVEL_TO, objBo.DISTANCE, objBo.ZMODE, objBo.TICKETS_PROD,
                                                                  objBo.FARE, objBo.DA, objBo.TOTAL, objBo.REMARKS, objBo.status, objBo.entered_by, objBo.approved_on, ref SaveStatus);

                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public Expensecollection Get_ExpenseDetails(string strPERNR, DateTime dtBeginDate, DateTime dtEndDate)
        {
            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

            Expensecollection objList = new Expensecollection();
            foreach (var vRow in objExpenseDataContext.sp_Get_Expense_Details(strPERNR, dtBeginDate, dtEndDate))
            {
                Expensebo objEXPAddBo = new Expensebo();

                objEXPAddBo.PERNR = vRow.PERNR;
                objEXPAddBo.ENAME = vRow.ENAME;
                objEXPAddBo.WERKS = vRow.WERKS;
                objEXPAddBo.PLSXT = vRow.PLSXT;
                objEXPAddBo.STEXT = vRow.STEXT;
                objEXPAddBo.REPORT_TO = vRow.REPORT_TO;
                objEXPAddBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
                objEXPAddBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
                objEXPAddBo.DA_DEDUCTION = vRow.DA_DEDUCTION;
                objEXPAddBo.TA_DEDUCTION = vRow.TA_DEDUCTION;
                objEXPAddBo.OTR_EXP_DEDUCT = vRow.OTR_EXP_DEDUCT;
                objEXPAddBo.TOT_INCHARGE_PERSON_DEDUC = vRow.TOT_INCHARGE_PERSON_DEDUC;
                objEXPAddBo.CORPORATE_OFFICE_DEDUC = vRow.CORPORATE_OFFICE_DEDUC;
                objEXPAddBo.TOT_DEDUC = vRow.TOT_DEDUC;
                objEXPAddBo.AMOUNT_ALLOWED = vRow.AMOUNT_ALLOWED;
                objEXPAddBo.STATIONARY = vRow.STATIONARY;
                objEXPAddBo.COURIER = vRow.COURIER;
                objEXPAddBo.PANDT = vRow.PANDT;
                objEXPAddBo.EMAIL = vRow.EMAIL;
                objEXPAddBo.COMP_PRODUCT_PURCHASE = vRow.COMP_PRODUCT_PURCHASE;
                objEXPAddBo.OTHERS = vRow.OTHERS;
                objEXPAddBo.MARK_DEVELOP_EXPENCE = vRow.MARK_DEVELOP_EXPENCE;
                objEXPAddBo.BUS_PASS = vRow.BUS_PASS;
                objEXPAddBo.CONVEYANCE = vRow.CONVEYANCE;
                objEXPAddBo.JC_MEETINGS = vRow.JC_MEETINGS;
                objEXPAddBo.GRAND_TOTAL = vRow.GRAND_TOTAL;
                objEXPAddBo.Status = vRow.Status;

                objEXPAddBo.approved_by = vRow.entered_by;
                objEXPAddBo.approved_on = Convert.ToDateTime(vRow.approved_on);

                objList.Add(objEXPAddBo);
            }
            objExpenseDataContext.Dispose();

            return objList;
        }

        public Expense_statementcollection Get_ExpenseStatementDetails(string strPERNR, DateTime dtBeginDate, DateTime dtEndDate)
        {
            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

            Expense_statementcollection objList = new Expense_statementcollection();
            foreach (var vRow in objExpenseDataContext.sp_Get_Expense_Statement_Details(strPERNR, dtBeginDate, dtEndDate))
            {
                Expense_statementbo objEXPStatemntAddBo = new Expense_statementbo();

                objEXPStatemntAddBo.PERNR = vRow.PERNR;
                objEXPStatemntAddBo.BEGIN_DATE = Convert.ToDateTime(vRow.BEGIN_DATE);
                objEXPStatemntAddBo.END_DATE = Convert.ToDateTime(vRow.END_DATE);
                objEXPStatemntAddBo.DATE1 = Convert.ToDateTime(vRow.DATE1);
                objEXPStatemntAddBo.PLACE_WORK = vRow.PLACE_WORK;
                objEXPStatemntAddBo.NIGHT_HALT = vRow.NIGHT_HALT;
                //objEXPStatemntAddBo.STATES = vRow
                objEXPStatemntAddBo.CATEGORY = vRow.CATEGORY;

                objEXPStatemntAddBo.LODGE_BILLS = vRow.LODGE_BILLS;
                objEXPStatemntAddBo.BILL_AMOUNT = vRow.BILL_AMOUNT;
                objEXPStatemntAddBo.TRAVEL_FROM = vRow.TRAVEL_FROM;
                objEXPStatemntAddBo.TRAVEL_Via = vRow.TRAVEL_VIA;
                objEXPStatemntAddBo.TRAVEL_TO = vRow.TRAVEL_TO;
                objEXPStatemntAddBo.DISTANCE = vRow.DISTANCE;
                objEXPStatemntAddBo.ZMODE = vRow.ZMODE;
                objEXPStatemntAddBo.TICKETS_PROD = vRow.TICKETS_PROD;
                objEXPStatemntAddBo.FARE = vRow.FARE;
                objEXPStatemntAddBo.DA = vRow.DA;
                //objEXPStatemntAddBo.OTHERS = vRow.OTHERS ;
                objEXPStatemntAddBo.TOTAL = vRow.TOTAL;
                objEXPStatemntAddBo.REMARKS = vRow.REMARKS;

                objList.Add(objEXPStatemntAddBo);
            }
            objExpenseDataContext.Dispose();

            return objList;
        }

        public void Approve_Expense(Expensebo objBo, ref bool? SaveStatus)
        {
            try
            {
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

                objExpenseDataContext.sp_approve_expense(objBo.PERNR, objBo.BEGDA, objBo.ENDDA, objBo.TOT_INCHARGE_PERSON_DEDUC, objBo.CORPORATE_OFFICE_DEDUC,
                                                         objBo.TOT_DEDUC, objBo.AMOUNT_ALLOWED, objBo.STATIONARY, objBo.COURIER, objBo.PANDT, objBo.EMAIL, objBo.COMP_PRODUCT_PURCHASE,
                                                         objBo.OTHERS, objBo.MARK_DEVELOP_EXPENCE, objBo.BUS_PASS, objBo.CONVEYANCE, objBo.JC_MEETINGS, objBo.GRAND_TOTAL,
                                                         objBo.Status, objBo.approved_by, objBo.approved_on, ref SaveStatus);

                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public void Approve_Expense_Statement(Expense_statementbo objBo, ref bool? SaveStatus)
        {
            try
            {
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

                objExpenseDataContext.sp_approve_expense_statement(objBo.PERNR, objBo.BEGIN_DATE, objBo.END_DATE, objBo.DATE1, objBo.CATEGORY,
                                                                    objBo.ZMODE, objBo.FARE, objBo.DA, objBo.status, objBo.approved_by, objBo.approved_on, ref SaveStatus);

                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }


        public void payment_payment_Expense(Expensebo objBo, ref bool? SaveStatus)
        {
            try
            {
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

                objExpenseDataContext.sp_payment_payment_expense(objBo.PERNR, objBo.BEGDA, objBo.ENDDA,
                                                         objBo.Status, objBo.approved_by, objBo.approved_on, ref SaveStatus);

                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public void payment_payment_Expense_Statement(Expense_statementbo objBo, ref bool? SaveStatus)
        {
            try
            {
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

                objExpenseDataContext.sp_payment_payment_expense_statement(objBo.PERNR, objBo.BEGIN_DATE, objBo.END_DATE, objBo.status, objBo.approved_by, objBo.approved_on, ref SaveStatus);

                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }


        //public string Get_Rate_Fare_ForDistance(string strToPlace)
        //{
        //     string strRate = string.Empty;

        //        ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

        //        objExpenseDataContext.sp_Get_Place_Rate(Convert.ToInt32(strToPlace),ref strRate );

        //        objExpenseDataContext.Dispose();

        //        return strRate;

        //}

        public string Get_Rate_Fare_ForDistance(string strToPlace)
        {
            string strRate = string.Empty;

            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

            //// objExpenseDataContext.sp_Get_Fare_Rate(strPERNR, strToPlace, strCatg, ref strRate);
            objExpenseDataContext.sp_Get_Place_Rate(strToPlace, ref strRate);

            objExpenseDataContext.Dispose();

            return strRate;

        }

        //public string Get_Amount_ForDA(string strPERNR, string strCatg, string strToPlace, int? Days)
        //{
        //    string strRate = string.Empty;

        //    ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

        //    objExpenseDataContext.sp_Get_DA_Rate(strPERNR, strToPlace, strCatg, Days, ref strRate);

        //    objExpenseDataContext.Dispose();

        //    return strRate;

        //}

        //=========================== EXPENSE TYPE =====

        ////public string Get_Amount_ForClaimExpType(string PERNR, string CountryID, string Region, string ExpenseType)
        ////{
        ////    decimal? strRate = 0;
        ////    string strCurrency = "";
        ////    ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

        ////    objExpenseDataContext.sp_Get_DA_Rate(PERNR, CountryID, Region, ExpenseType, ref strRate, ref strCurrency);

        ////    objExpenseDataContext.Dispose();

        ////    return strRate.ToString() + "" + strCurrency;

        ////}

        //=========================== EXPENSE TYPE =====

        public string Get_Amount_ForClaimExpType_International(string strPERNR, string strExpType, string strToPlace, int? Days)
        {
            string strRate = string.Empty;

            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

            objExpenseDataContext.sp_Get_DA_Rate_International(strPERNR, strToPlace, strExpType, Days, ref strRate);

            objExpenseDataContext.Dispose();

            return strRate;

        }

        public string Get_Amount_ForExpenseType(string strPERNR, string strExpenseType)
        {
            string strRate = string.Empty;

            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

            objExpenseDataContext.sp_Get_ExpenseType_Rate(strPERNR, strExpenseType, ref strRate);

            objExpenseDataContext.Dispose();

            return strRate;
        }

        public DataSet Get_States()
        {
            DataSet ds = new DataSet();
            DataTable objTable = ds.Tables.Add();
            objTable.Columns.Add("ID", typeof(int));
            objTable.Columns.Add("STATES", typeof(string));

            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

            foreach (var vrow in objExpenseDataContext.sp_Get_States())
            {
                objTable.Rows.Add(vrow.ID, vrow.STATES);
            }

            objExpenseDataContext.Dispose();

            return ds;
        }

        //public DataSet Get_statesforplace()
        //{
        //    DataSet ds = new DataSet();
        //    DataTable objTable = ds.Tables.Add();
        //    objTable.Columns.Add("state_code", typeof(string));
        //    objTable.Columns.Add("state_name", typeof(string));

        //    ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

        //    foreach (var vrow in objExpenseDataContext.sp_get_statesforplace())
        //    {
        //        objTable.Rows.Add(vrow.state_code, vrow.state_name);
        //    }

        //    objExpenseDataContext.Dispose();

        //    return ds;
        //}

        public void Create_Corporate_Claims(Corporate_Claimsbo objBo, ref bool? SaveStatus)
        {
            try
            {
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

                int i = objExpenseDataContext.sp_create_corporate_claims(objBo.PERNR, objBo.REQUISITION_ID, objBo.REQ_SEGMENT_ID, Convert.ToInt32(objBo.TRIP_NUMBER), objBo.DATE1, objBo.PLACE_FROM, objBo.PLACE_TO,
                    objBo.MODE_OF_TRANSPORTATION, objBo.FARE, objBo.DAILY_ALLOWANCE, objBo.LODGING_CHARGES, objBo.LOCAL_CONVEYANCE, objBo.DETAILS_MISC_EXP,
                    objBo.AMT_MISC_EXP, objBo.TOTAL, objBo.created_on, "Review", ref SaveStatus, objBo.SPKZL, objBo.WAERS, objBo.DATE2);

                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

    }
}
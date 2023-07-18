using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class Expensebo
    {
        public Expensebo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int _EXPENSE_ID = 0;
        private string _strPernr = string.Empty;
        private string _strEname = string.Empty;
        private string _strWerks = string.Empty;
        private string _strPlsxt = string.Empty;
        private string _strStext = string.Empty;
        private string _strReportTo = string.Empty;
        private DateTime _dtBegda = Convert.ToDateTime("01/01/1900");
        private DateTime _dtEndda = Convert.ToDateTime("01/01/1900");
        private string _strDA = string.Empty;
        private string _strTA = string.Empty;
        private string _strOtherExp = string.Empty;
        private string _strTotInchgPersonDeduction = string.Empty;
        private string _strCorporateOffDeduction = string.Empty;
        private string _strTotalDeduction = string.Empty;
        private string _strAmountAllowed = string.Empty;
        private string _strStationary = string.Empty;
        private string _strCourier = string.Empty;
        private string _strPandt = string.Empty;
        private string _strEmail = string.Empty;
        private string _strCompProdPurchase = string.Empty;
        private string _strOthers = string.Empty;
        private string _strMarkDevExpense = string.Empty;
        private string _strBusPass = string.Empty;
        private string _strConveyance = string.Empty;
        private string _strJcMeetings = string.Empty;
        private string _strGrandTotal = string.Empty;
        private string _strStatus = string.Empty;
        private string _strEntered_By = string.Empty;
        private string _strApproved_By = string.Empty;
        private DateTime _dtApproved_On = DateTime.Now;
        private string _placeid = string.Empty;
        private string _placename = string.Empty;


        private string _strCLAIMED = string.Empty;
        private string _strREIMBURSE = string.Empty;

        private string _strAmount = string.Empty;

        public int EXPENSE_ID
        {
            get { return _EXPENSE_ID; }
            set { _EXPENSE_ID = value; }
        }

        public string PERNR
        {
            get { return _strPernr; }
            set { _strPernr = value; }
        }

        public string ENAME
        {
            get { return _strEname; }
            set { _strEname = value; }
        }

        public string placeid
        {
            get { return _placeid; }
            set { _placeid = value; }
        }

        public string placename
        {
            get { return _placename; }
            set { _placename = value; }
        }

        public string WERKS
        {
            get { return _strWerks; }
            set { _strWerks = value; }
        }

        public string PLSXT
        {
            get { return _strPlsxt; }
            set { _strPlsxt = value; }
        }

        public string STEXT
        {
            get { return _strStext; }
            set { _strStext = value; }
        }

        public string REPORT_TO
        {
            get { return _strReportTo; }
            set { _strReportTo = value; }
        }

        public DateTime BEGDA
        {
            get { return _dtBegda; }
            set { _dtBegda = value; }
        }

        public DateTime ENDDA
        {
            get { return _dtEndda; }
            set { _dtEndda = value; }
        }

        public string DA_DEDUCTION
        {
            get { return _strDA; }
            set { _strDA = value; }
        }

        public string TA_DEDUCTION
        {
            get { return _strTA; }
            set { _strTA = value; }
        }

        public string OTR_EXP_DEDUCT
        {
            get { return _strOtherExp; }
            set { _strOtherExp = value; }
        }

        public string TOT_INCHARGE_PERSON_DEDUC
        {
            get { return _strTotInchgPersonDeduction; }
            set { _strTotInchgPersonDeduction = value; }
        }

        public string CORPORATE_OFFICE_DEDUC
        {
            get { return _strCorporateOffDeduction; }
            set { _strCorporateOffDeduction = value; }
        }

        public string TOT_DEDUC
        {
            get { return _strTotalDeduction; }
            set { _strTotalDeduction = value; }
        }

        public string AMOUNT_ALLOWED
        {
            get { return _strAmountAllowed; }
            set { _strAmountAllowed = value; }
        }

        public string STATIONARY
        {
            get { return _strStationary; }
            set { _strStationary = value; }
        }

        public string COURIER
        {
            get { return _strCourier; }
            set { _strCourier = value; }
        }

        public string PANDT
        {
            get { return _strPandt; }
            set { _strPandt = value; }
        }

        public string EMAIL
        {
            get { return _strEmail; }
            set { _strEmail = value; }
        }

        public string COMP_PRODUCT_PURCHASE
        {
            get { return _strCompProdPurchase; }
            set { _strCompProdPurchase = value; }
        }

        public string OTHERS
        {
            get { return _strOthers; }
            set { _strOthers = value; }
        }

        public string MARK_DEVELOP_EXPENCE
        {
            get { return _strMarkDevExpense; }
            set { _strMarkDevExpense = value; }
        }

        public string BUS_PASS
        {
            get { return _strBusPass; }
            set { _strBusPass = value; }
        }

        public string CONVEYANCE
        {
            get { return _strConveyance; }
            set { _strConveyance = value; }
        }

        public string JC_MEETINGS
        {
            get { return _strJcMeetings; }
            set { _strJcMeetings = value; }
        }

        public string GRAND_TOTAL
        {
            get { return _strGrandTotal; }
            set { _strGrandTotal = value; }
        }

        public string Status
        {
            get { return _strStatus; }
            set { _strStatus = value; }
        }

        public string entered_by
        {
            get { return _strEntered_By; }
            set { _strEntered_By = value; }
        }

        public string approved_by
        {
            get { return _strApproved_By; }
            set { _strApproved_By = value; }
        }

        public DateTime approved_on
        {
            get { return _dtApproved_On; }
            set { _dtApproved_On = value; }
        }

        public string CLAIMED
        {
            get { return _strCLAIMED; }
            set { _strCLAIMED = value; }
        }

        public string REIMBURSE
        {
            get { return _strREIMBURSE; }
            set { _strREIMBURSE = value; }
        }

        public string AMOUNT
        {
            get { return _strAmount; }
            set { _strAmount = value; }
        }
    }
}
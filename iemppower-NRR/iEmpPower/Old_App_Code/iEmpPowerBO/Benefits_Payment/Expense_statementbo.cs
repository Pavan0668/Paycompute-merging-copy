using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class Expense_statementbo
    {
        public Expense_statementbo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string _strPernr = string.Empty;
        private DateTime _dtBegindate = Convert.ToDateTime("01/01/1900");
        private DateTime _dtEnddate = Convert.ToDateTime("01/01/1900");
        private DateTime _dtDate1 = Convert.ToDateTime("01/01/1900");
        private string _strPlaceWork = string.Empty;
        private string _strNightHalt = string.Empty;
        private string _strStates = string.Empty;
        private string _strCategory = string.Empty;
        private string _strCategoryofplace = string.Empty;
        private string _strLodgeBills = string.Empty;
        private string _strBillAmount = string.Empty;
        private string _strTravelFrom = string.Empty;
        private string _strTravelVia = string.Empty;
        private string _strTravelTo = string.Empty;
        private string _strDistance = string.Empty;
        private string _strZmode = string.Empty;
        private string _strTicketsProd = string.Empty;
        private string _strFare = string.Empty;
        private string _strDA = string.Empty;
        //private string _strOthers = string.Empty;
        private string _strTotal = string.Empty;
        private string _strRemarks = string.Empty;
        private string _strStatus = string.Empty;
        private string _strEntered_By = string.Empty;
        private string _strApproved_By = string.Empty;
        private DateTime _dtApproved_On = DateTime.Now;
        public string PERNR
        {
            get { return _strPernr; }
            set { _strPernr = value; }
        }

        public DateTime BEGIN_DATE
        {
            get { return _dtBegindate; }
            set { _dtBegindate = value; }
        }

        public DateTime END_DATE
        {
            get { return _dtEnddate; }
            set { _dtEnddate = value; }
        }

        public DateTime DATE1
        {
            get { return _dtDate1; }
            set { _dtDate1 = value; }
        }

        public string PLACE_WORK
        {
            get { return _strPlaceWork; }
            set { _strPlaceWork = value; }
        }

        public string NIGHT_HALT
        {
            get { return _strNightHalt; }
            set { _strNightHalt = value; }
        }

        public string STATES
        {
            get { return _strStates; }
            set { _strStates = value; }
        }

        public string CATEGORY
        {
            get { return _strCategory; }
            set { _strCategory = value; }
        }

        public string CATEGORYofplace
        {
            get { return _strCategoryofplace; }
            set { _strCategoryofplace = value; }
        }

        public string LODGE_BILLS
        {
            get { return _strLodgeBills; }
            set { _strLodgeBills = value; }
        }

        public string BILL_AMOUNT
        {
            get { return _strBillAmount; }
            set { _strBillAmount = value; }
        }

        public string TRAVEL_FROM
        {
            get { return _strTravelFrom; }
            set { _strTravelFrom = value; }
        }
        public string TRAVEL_Via
        {
            get { return _strTravelVia; }
            set { _strTravelVia = value; }
        }
        public string TRAVEL_TO
        {
            get { return _strTravelTo; }
            set { _strTravelTo = value; }
        }

        public string DISTANCE
        {
            get { return _strDistance; }
            set { _strDistance = value; }
        }

        public string ZMODE
        {
            get { return _strZmode; }
            set { _strZmode = value; }
        }

        public string TICKETS_PROD
        {
            get { return _strTicketsProd; }
            set { _strTicketsProd = value; }
        }

        public string FARE
        {
            get { return _strFare; }
            set { _strFare = value; }
        }

        public string DA
        {
            get { return _strDA; }
            set { _strDA = value; }
        }

        //public string OTHERS
        //{
        //    get { return _strOthers ; }
        //    set { _strOthers = value; }
        //}

        public string TOTAL
        {
            get { return _strTotal; }
            set { _strTotal = value; }
        }

        public string REMARKS
        {
            get { return _strRemarks; }
            set { _strRemarks = value; }
        }

        public string status
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

    }
}
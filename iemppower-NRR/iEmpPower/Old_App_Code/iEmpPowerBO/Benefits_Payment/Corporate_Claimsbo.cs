using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class Corporate_Claimsbo
    {
        public Corporate_Claimsbo()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        private string _strPernr = string.Empty;
        private int _iREQUISITION_ID = 0;
        private int _iREQ_SEGMENT_ID = 0;
        private string _strTripNumber = string.Empty;
        private DateTime _dtDate1 = Convert.ToDateTime("01/01/1900");
        private DateTime _dtDate2 = Convert.ToDateTime("01/01/1900");
        private string _strPlaceFrom = string.Empty;
        private string _strPlaceTo = string.Empty;
        private string _strModeOfTransportation = string.Empty;
        private string _strFare = string.Empty;
        private string _strDailyAllowance = string.Empty;
        private string _strLodgingCharges = string.Empty;
        private string _strLocalConveyance = string.Empty;
        private string _strDetailsMiscExp = string.Empty;
        private string _strAmtMiscExp = string.Empty;
        private string _strTotal = string.Empty;
        private DateTime _dtcreatedOn = Convert.ToDateTime("01/01/1900");
        private string _strStatus = string.Empty;
        private string _strSPKZL= string.Empty;

        private string _strWAERS = string.Empty;


        public string PERNR
        {
            get{ return _strPernr; }
            set{_strPernr=value;}
        }

        public int REQUISITION_ID
        {
            get { return _iREQUISITION_ID; }
            set { _iREQUISITION_ID = value; }
        }

        public int REQ_SEGMENT_ID
        {
            get { return _iREQ_SEGMENT_ID; }
            set { _iREQ_SEGMENT_ID = value; }
        }

        public string TRIP_NUMBER
        {
            get { return _strTripNumber; }
            set { _strTripNumber = value; }
        }

        public DateTime DATE1
        {
            get { return _dtDate1 ; }
            set { _dtDate1 = value; }
        }
        public DateTime DATE2
        {
            get { return _dtDate2; }
            set { _dtDate2 = value; }
        }

        public string PLACE_FROM
        {
            get { return _strPlaceFrom; }
            set { _strPlaceFrom = value; }
        }

        public string PLACE_TO
        {
            get { return _strPlaceTo; }
            set { _strPlaceTo = value; }
        }

        public string MODE_OF_TRANSPORTATION
        {
            get{ return _strModeOfTransportation   ; }
            set{_strModeOfTransportation  =value;}
        }
        
        public string FARE
        {
            get{ return _strFare  ; }
            set{_strFare = value;}
        }

        public string DAILY_ALLOWANCE
        {
            get { return _strDailyAllowance; }
            set { _strDailyAllowance = value; }
        }

        public string LODGING_CHARGES
        {
            get { return _strLodgingCharges; }
            set { _strLodgingCharges = value; }
        }

        public string LOCAL_CONVEYANCE
        {
            get { return _strLocalConveyance; }
            set { _strLocalConveyance = value; }
        }

        public string DETAILS_MISC_EXP
        {
            get { return _strDetailsMiscExp; }
            set { _strDetailsMiscExp = value; }
        }

        public string AMT_MISC_EXP
        {
            get { return _strAmtMiscExp ; }
            set { _strAmtMiscExp = value; }
        }

        public string TOTAL
        {
            get{ return _strTotal    ; }
            set{_strTotal   = value;}
        }

        public DateTime created_on
        {
            get { return _dtcreatedOn; }
            set { _dtcreatedOn = value; }
        }

        public string status
        {
            get { return _strStatus ; }
            set { _strStatus = value; }
        }

        public string SPKZL
        {
            get { return _strSPKZL; }
            set { _strSPKZL = value; }
        }

        public string WAERS
        {
            get { return _strWAERS; }
            set { _strWAERS = value; }
        }
    }
}
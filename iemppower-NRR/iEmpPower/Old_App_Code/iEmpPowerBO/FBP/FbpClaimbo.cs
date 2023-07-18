using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.FBP
{
    public class FbpClaimbo
    {



        private DateTime? created_on;
        private string _ename = string.Empty;
        private string _created_by = string.Empty;
        private int? _fbpc_id;
        private string _lgart = string.Empty;
        private string _betrg = string.Empty;
        private string _status = string.Empty;
        private string _ovramt = string.Empty;
        private string _remarks = string.Empty;
        private DateTime? _begda;
        private DateTime? _approvedon;
        private string _TotalFBPamt = string.Empty;
        private string _TotalClaimamt = string.Empty;
        private string entity = string.Empty;

        public int? FID { get; set; }

        public bool? Mob { get; set; }
        public bool? CC { get; set; }

        public string ENAME
        {
            get { return _ename; }
            set { _ename = value; }
        }

        public string CREATED_BY
        {
            get { return _created_by; }
            set { _created_by = value; }
        }

        public int? FBPC_IC
        {
            get { return _fbpc_id; }
            set { _fbpc_id = value; }
        }

        public string LGART
        {
            get { return _lgart; }
            set { _lgart = value; }
        }
        public string BETRG
        {
            get { return _betrg; }
            set { _betrg = value; }
        }
        public string STATUS
        {
            get { return _status; }
            set { _status = value; }
        }
        public string OVERRIDE_AMT
        {
            get { return _ovramt; }
            set { _ovramt = value; }
        }
        public string REMARKS
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

        public string TotalFBPamt
        {
            get { return _TotalFBPamt; }
            set { _TotalFBPamt = value; }
        }

        public string TotalClaimamt
        {
            get { return _TotalClaimamt; }
            set { _TotalClaimamt = value; }
        }
        public DateTime? CREATED_ON
        {
            get { return created_on; }
            set { created_on = value; }
        }
        public DateTime? BEGDA
        {
            get { return _begda; }
            set { _begda = value; }
        }



        public DateTime? APPROVEDON
        {
            get { return _approvedon; }
            set { _approvedon = value; }
        }
        private DateTime? _billdate;
        private int? _id;
        private string _receiptfid = string.Empty;
        private string _receiptfile = string.Empty;
        private string _receiptpath = string.Empty;
        private string _relationship = string.Empty;
        private string _billamt = string.Empty;
        private string _billno = string.Empty;


        public DateTime? BILL_DATE
        {
            get { return _billdate; }
            set { _billdate = value; }
        }

        public int? ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string RECEIPT_FID
        {
            get { return _receiptfid; }
            set { _receiptfid = value; }
        }

        public string RECEIPT_FILE
        {
            get { return _receiptfile; }
            set { _receiptfile = value; }
        }


        public string RECEIPT_FPATH
        {
            get { return _receiptpath; }
            set { _receiptpath = value; }
        }

        public string RELATIONSHIP
        {
            get { return _relationship; }
            set { _relationship = value; }
        }

        public string BILL_AMT
        {
            get { return _billamt; }
            set { _billamt = value; }
        }

        public string BILL_NO
        {
            get { return _billno; }
            set { _billno = value; }
        }




        //declaration----

        private string pernr;
        private DateTime date;
        private string Plan;
        private string Exit;
        private string BasketTotal;
        private string Allowanceid;
        private string Allowancetext;
        private string Amount;
        private string monthly;
        private string annual;
        private string accrued;
        private string aa_amt01;
        private string aa_amt02;
        private string aa_amt03;
        private string aa_amt04;
        private string aa_amt05;
        private string aa_amt06;
        private string aa_amt07;
        private string aa_amt08;
        private string aa_amt09;
        private string aa_amt10;
        private string aa_amt11;
        private string aa_amt12;
        private string LastUpdatedDate;
        private int count;
        private string sap_id;
        private DateTime? doj;
        private DateTime? dol;

        public string PERNR
        {
            get { return pernr; }
            set { pernr = value; }
        }


        public DateTime DATE
        {
            get { return date; }
            set { date = value; }
        }


        public string PLAN
        {
            get { return Plan; }
            set { Plan = value; }
        }

        public string EXIT
        {
            get { return Exit; }
            set { Exit = value; }
        }

        public string BASKETTOTAL
        {
            get { return BasketTotal; }
            set { BasketTotal = value; }


        }

        public string ALLOWANCEID
        {
            get { return Allowanceid; }
            set { Allowanceid = value; }
        }

        public string ALLOWANCETEXT
        {
            get { return Allowancetext; }
            set { Allowancetext = value; }
        }

        public string AMOUNT
        {
            get { return Amount; }
            set { Amount = value; }
        }


        public string MONTHLY
        {
            get { return monthly; }
            set { monthly = value; }
        }


        public string ANNUAL
        {
            get { return annual; }
            set { annual = value; }
        }

        public string ACCRUED
        {
            get { return accrued; }
            set { accrued = value; }
        }


        public string AA_AMT01
        {
            get { return aa_amt01; }
            set { aa_amt01 = value; }
        }

        public string AA_AMT02
        {
            get { return aa_amt02; }
            set { aa_amt02 = value; }
        }

        public string AA_AMT03
        {
            get { return aa_amt03; }
            set { aa_amt03 = value; }
        }

        public string AA_AMT04
        {
            get { return aa_amt04; }
            set { aa_amt04 = value; }
        }

        public string AA_AMT05
        {
            get { return aa_amt05; }
            set { aa_amt05 = value; }
        }

        public string AA_AMT06
        {
            get { return aa_amt06; }
            set { aa_amt06 = value; }
        }

        public string AA_AMT07
        {
            get { return aa_amt07; }
            set { aa_amt07 = value; }
        }


        public string AA_AMT08
        {
            get { return aa_amt08; }
            set { aa_amt08 = value; }
        }

        public string AA_AMT09
        {
            get { return aa_amt09; }
            set { aa_amt09 = value; }
        }

        public string AA_AMT10
        {
            get { return aa_amt10; }
            set { aa_amt10 = value; }
        }

        public string AA_AMT11
        {
            get { return aa_amt11; }
            set { aa_amt11 = value; }
        }

        public string AA_AMT12
        {
            get { return aa_amt12; }
            set { aa_amt12 = value; }
        }

        public string LASTUPDATEDDATE
        {
            get { return LastUpdatedDate; }
            set { LastUpdatedDate = value; }
        }

        public int COUNT
        {
            get { return count; }
            set { count = value; }
        }


        public string SAP_ID
        {
            get { return sap_id; }
            set { sap_id = value; }
        }


        public DateTime? DOJ
        {
            get { return doj; }
            set { doj = value; }
        }

        public DateTime? DOL
        {
            get { return dol; }
            set { dol = value; }
        }

        //
        private string _PendingAmt;
        private string _Balance;

        public string BALANCE
        {
            get { return _Balance; }
            set { _Balance = value; }
        }
        public string PENDINGAMT
        {
            get { return _PendingAmt; }
            set { _PendingAmt = value; }
        }


        public long? SLNO { get; set; }

        public bool? FBPLOCK
        {
            get;
            set;
        }


        public string APPAMT { get; set; }

        public string ENTITY
        {
            get { return entity; }
            set { entity = value; }
        }

        // public int ID { get; set; }
        public string SUBTY { get; set; }
        public DateTime? JBGDT { get; set; }
        public DateTime? JENDT { get; set; }
        public string STPNT { get; set; }
        public string DESTN { get; set; }
        public string MTRVL { get; set; }
        public string CTRVL { get; set; }
        public string TKTNO { get; set; }
        public char? SLFTR { get; set; }
        public DateTime UPDATED_ON { get; set; }
        public decimal? KM_TRVLD { get; set; }
        public decimal? AMOUNTLTA { get; set; }



        public string STEXT { get; set; }
        public string FAMTX { get; set; }
        public string FCNAM { get; set; }
        public DateTime? FGBDT { get; set; }
        public char? FASEX { get; set; }
        public char? DEPDT { get; set; }
        public string FAMTX_text { get; set; }

        public int LINEID
        {
            get;
            set;
        }


        public string CBPY1 { get; set; }
        public string CBPY2 { get; set; }
        public string CBPY3 { get; set; }
        public string CBPY4 { get; set; }
        public string CLYear { get; set; }
        public string CLY1 { get; set; }
        public string CLY2 { get; set; }
        public string CLY3 { get; set; }
        public string CLY4 { get; set; }
    }
}
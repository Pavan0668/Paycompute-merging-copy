using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class OtherReimbursementsbo
    {
        private decimal? ukurs;
        private string projid = string.Empty;
        private string task = string.Empty;
        private string purpose = string.Empty;
        private string rcurr = string.Empty;
        private DateTime? created_on;
        private string created_by = string.Empty;
        private DateTime? approved_on1;
        private string approved_by1 = string.Empty;
        private string remarks1 = string.Empty;
        private DateTime? approved_on2;
        private string approved_by2 = string.Empty;
        private string remarks2 = string.Empty;
        private DateTime? approved_on3;
        private string approved_by3 = string.Empty;
        private string remarks3 = string.Empty;
        private DateTime? approved_on4;
        private string approved_by4 = string.Empty;
        private string remarks4 = string.Empty;
        private DateTime? approved_on5;
        private string approved_by5 = string.Empty;
        private string remarks5 = string.Empty;
        private DateTime? approved_on6;
        private string approved_by6 = string.Empty;
        private string remarks6 = string.Empty;
        private DateTime? approved_on7;
        private string approved_by7 = string.Empty;
        private string remarks7 = string.Empty;
        private DateTime? approved_on8;
        private string approved_by8 = string.Empty;
        private string remarks8 = string.Empty;
        private DateTime? approved_on9;
        private string approved_by9 = string.Empty;
        private string remarks9 = string.Empty;
        private string status = string.Empty;


        private string approved_by1n = string.Empty;
        private string approved_by2n = string.Empty;
        private string approved_by3n = string.Empty;
        private string approved_by4n = string.Empty;
        private string approved_by5n = string.Empty;
        private string approved_by6n = string.Empty;
        private string approved_by7n = string.Empty;


        // Expense Types
        private Guid? IExp_typeid = Guid.Parse("00000000-0000-0000-0000-000000000000");
        private int? IExp_Id;
        private string Exp_Type = string.Empty;
        private DateTime? sdate;
        private string nodays = string.Empty;
        private string ExptAmt = string.Empty;
        private string Exptcurr = string.Empty;
        private string ExcRate = string.Empty;
        private string reamt = string.Empty;
        private string justify = string.Empty;
        private string recpfile = string.Empty;
        private string recpfid = string.Empty;
        private string recpfpath = string.Empty;
        private int? id;

        private decimal? _totalamount;

        private decimal _TotalAmount = 0;

        private string post1 = string.Empty;
        private string ename = string.Empty;

        private string lgtxt = string.Empty;

        private decimal? settleamt;
        private string settlecurr = string.Empty;
        private string entity = string.Empty;

        private string exp_typ_txt = string.Empty;
        private string bukrs = string.Empty;
        private DateTime? pdate;


        public string EXP_TYPE_TEXT
        {
            get { return exp_typ_txt; }
            set { exp_typ_txt = value; }
        }

        public string BUKRS
        {
            get { return bukrs; }
            set { bukrs = value; }
        }

        public DateTime? P_DATE
        {
            get { return pdate; }
            set { pdate = value; }
        }

        public int? ID
        {
            get { return id; }
            set { id = value; }
        }

        public decimal? UKURS
        {
            get { return ukurs; }
            set { ukurs = value; }
        }

        public string PROJID
        {
            get { return projid; }
            set { projid = value; }
        }

        public string TASK
        {
            get { return task; }
            set { task = value; }
        }
        public string PURPOSE
        {
            get { return purpose; }
            set { purpose = value; }
        }
        public string RCURR
        {
            get { return rcurr; }
            set { rcurr = value; }
        }

        public string LGTXT
        {
            get { return lgtxt; }
            set { lgtxt = value; }

        }
        public DateTime? CREATED_ON
        {
            get { return created_on; }
            set { created_on = value; }
        }

        public string CREATED_BY
        {
            get { return created_by; }
            set { created_by = value; }
        }

        public DateTime? APPROVED_ON1
        {
            get { return approved_on1; }
            set { approved_on1 = value; }
        }
        public string APPROVED_BY1
        {
            get { return approved_by1; }
            set { approved_by1 = value; }
        }
        public string REMARKS1
        {
            get { return remarks1; }
            set { remarks1 = value; }
        }


        public DateTime? APPROVED_ON2
        {
            get { return approved_on2; }
            set { approved_on2 = value; }
        }
        public string APPROVED_BY2
        {
            get { return approved_by2; }
            set { approved_by2 = value; }
        }
        public string REMARKS2
        {
            get { return remarks2; }
            set { remarks2 = value; }
        }


        public DateTime? APPROVED_ON3
        {
            get { return approved_on3; }
            set { approved_on3 = value; }
        }
        public string APPROVED_BY3
        {
            get { return approved_by3; }
            set { approved_by3 = value; }
        }
        public string REMARKS3
        {
            get { return remarks3; }
            set { remarks3 = value; }
        }


        public DateTime? APPROVED_ON4
        {
            get { return approved_on4; }
            set { approved_on4 = value; }
        }
        public string APPROVED_BY4
        {
            get { return approved_by4; }
            set { approved_by4 = value; }
        }
        public string REMARKS4
        {
            get { return remarks4; }
            set { remarks4 = value; }
        }

        public DateTime? APPROVED_ON5
        {
            get { return approved_on5; }
            set { approved_on5 = value; }
        }
        public string APPROVED_BY5
        {
            get { return approved_by5; }
            set { approved_by5 = value; }
        }
        public string REMARKS5
        {
            get { return remarks5; }
            set { remarks5 = value; }
        }

        public DateTime? APPROVED_ON6
        {
            get { return approved_on6; }
            set { approved_on6 = value; }
        }
        public string APPROVED_BY6
        {
            get { return approved_by6; }
            set { approved_by6 = value; }
        }
        public string REMARKS6
        {
            get { return remarks6; }
            set { remarks6 = value; }
        }
        public DateTime? APPROVED_ON7
        {
            get { return approved_on7; }
            set { approved_on7 = value; }
        }
        public string APPROVED_BY7
        {
            get { return approved_by7; }
            set { approved_by7 = value; }
        }
        public string REMARKS7
        {
            get { return remarks7; }
            set { remarks7 = value; }
        }

        public DateTime? APPROVED_ON8
        {
            get { return approved_on8; }
            set { approved_on8 = value; }
        }
        public string APPROVED_BY8
        {
            get { return approved_by8; }
            set { approved_by8 = value; }
        }
        public string REMARKS8
        {
            get { return remarks8; }
            set { remarks8 = value; }
        }

        public DateTime? APPROVED_ON9
        {
            get { return approved_on9; }
            set { approved_on9 = value; }
        }
        public string APPROVED_BY9
        {
            get { return approved_by9; }
            set { approved_by9 = value; }
        }
        public string REMARKS9
        {
            get { return remarks9; }
            set { remarks9 = value; }
        }

        public string STATUS
        {
            get { return status; }
            set { status = value; }

        }



        public string APPROVED_BY1N
        {
            get { return approved_by1n; }
            set { approved_by1n = value; }

        }

        public string APPROVED_BY2N
        {
            get { return approved_by2n; }
            set { approved_by2n = value; }

        }
        public string APPROVED_BY3N
        {
            get { return approved_by3n; }
            set { approved_by3n = value; }

        }
        public string APPROVED_BY4N
        {
            get { return approved_by4n; }
            set { approved_by4n = value; }

        }
        public string APPROVED_BY5N
        {
            get { return approved_by5n; }
            set { approved_by5n = value; }

        }
        public string APPROVED_BY6N
        {
            get { return approved_by6n; }
            set { approved_by6n = value; }

        }
        public string APPROVED_BY7N
        {
            get { return approved_by7n; }
            set { approved_by7n = value; }

        }

        public Guid? IEXP_TYPID
        {
            get { return IExp_typeid; }
            set { IExp_typeid = value; }
        }

        public int? IEXP_ID
        {
            get { return IExp_Id; }
            set { IExp_Id = value; }
        }

        public string EXP_TYPE
        {
            get { return Exp_Type; }
            set { Exp_Type = value; }

        }

        public DateTime? S_DATE
        {
            get { return sdate; }
            set { sdate = value; }
        }

        public string NO_DAYS
        {
            get { return nodays; }
            set { nodays = value; }

        }
        public string EXPT_AMT
        {
            get { return ExptAmt; }
            set { ExptAmt = value; }

        }
        public string EXPT_CURR
        {
            get { return Exptcurr; }
            set { Exptcurr = value; }

        }
        public string EXC_RATE
        {
            get { return ExcRate; }
            set { ExcRate = value; }

        }
        public string RE_AMT
        {
            get { return reamt; }
            set { reamt = value; }

        }
        public string JUSTIFY
        {
            get { return justify; }
            set { justify = value; }
        }

        public string RECEIPT_FILE
        {
            get { return recpfile; }
            set { recpfile = value; }
        }
        public string RECEIPT_FID
        {
            get { return recpfid; }
            set { recpfid = value; }
        }
        public string RECEIPT_FPATH
        {
            get { return recpfpath; }
            set { recpfpath = value; }
        }

        public decimal? TOTAL_AMOUNT
        {
            get { return _totalamount; }
            set { _totalamount = value; }
        }

        public string ENAME
        {
            get { return ename; }
            set { ename = value; }

        }

        public string POST1
        {
            get { return post1; }
            set { post1 = value; }
        }

        public decimal? SETTLEAMT
        {
            get { return settleamt; }
            set { settleamt = value; }
        }


        public string SETTLECURR
        {
            get { return settlecurr; }
            set { settlecurr = value; }
        }

        public string ENTITY
        {
            get { return entity; }
            set { entity = value; }
        }
        public decimal TotalAmount { get { return _TotalAmount; } set { _TotalAmount = value; } }

        public long? RwNum
        {
            get;
            set;
        }
    }
}
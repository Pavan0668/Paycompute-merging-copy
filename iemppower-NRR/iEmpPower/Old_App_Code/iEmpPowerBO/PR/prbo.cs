using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class prbo
{

    private int? prid;
    private string Pernr;
    private string ename;

    private int? ibanfn_ext;
    private string iPernr = string.Empty;
    private string rPernr = string.Empty;
    private string pFunc_Area = string.Empty;
    private string pBtext = string.Empty;
    private string mis_grpc = string.Empty;
    private string mis_grpa = string.Empty;
    private string mis_grpb = string.Empty;
    private string ekgrp = string.Empty;
    private string Bwerks = string.Empty;
    private string Swerks = string.Empty;
    private string Sug_supplier = string.Empty;
    private string Sup_Address = string.Empty;
    private string Sup_phone = string.Empty;
    private string pIn_Budget = string.Empty;
    private string pCapitalized = string.Empty;
    private string pCap_Text = string.Empty;
    private string pservice_burea = string.Empty;
    private string pCriticality = string.Empty;
    private string pspnr = string.Empty;
    private string vernr = string.Empty;
    private string billable = string.Empty;

    private string proposal = string.Empty;
    private string pfid = string.Empty;
    private string pfpath = string.Empty;
    private string agreement = string.Empty;
    private string afid = string.Empty;
    private string afpath = string.Empty;
    private string email_com = string.Empty;
    private string efid = string.Empty;
    private string efpath = string.Empty;
    private string invoice = string.Empty;
    private string ifid = string.Empty;
    private string ifpath = string.Empty;
    private string spart = string.Empty;
    private string justification = string.Empty;
    private string spl_notes = string.Empty;
    private DateTime? created_on1;
    private string createdby = string.Empty;
    private DateTime? app_on1;
    private string approvedby1 = string.Empty;
    private DateTime? hold_on1;
    private DateTime? Released_on1;
    private string comments1 = string.Empty;
    private DateTime? app_on2;
    private string approvedby2 = string.Empty;
    private DateTime? hold_on2;
    private DateTime? Released_on2;
    private string comments2 = string.Empty;
    private DateTime? app_on3;
    private string approvedby3 = string.Empty;
    private DateTime? hold_on3;
    private DateTime? Released_on3;
    private string comments3 = string.Empty;
    private DateTime? app_on4;
    private string approvedby4 = string.Empty;
    private DateTime? hold_on4;
    private DateTime? Released_on4;
    private string comments4 = string.Empty;
    private DateTime? app_on5;
    private string approvedby5 = string.Empty;
    private DateTime? hold_on5;
    private DateTime? Released_on5;
    private string comments5 = string.Empty;
    private DateTime? app_on6;
    private string approvedby6 = string.Empty;
    private DateTime? hold_on6;
    private DateTime? Released_on6;
    private string comments6 = string.Empty;
    private string status = string.Empty;
    private string total = string.Empty;

    private string approved_by1n = string.Empty;
    private string approved_by2n = string.Empty;
    private string approved_by3n = string.Empty;
    private string approved_by4n = string.Empty;
    private string approved_by5n = string.Empty;
    private string approved_by6n = string.Empty;
   

    //---- ADD PR ITEMS -----
    private Guid? ID = Guid.Parse("00000000-0000-0000-0000-000000000000");
    private int? _BANFN_EXT;
    private string _BNFPO = string.Empty;
    private string _MATNR = string.Empty;
    private string _TXZ01 = string.Empty;
    private string _PART_NO = string.Empty;
    private string _MTART = string.Empty;
    private string _MEINS = string.Empty;
    private string _NO_OF_UNITS = string.Empty;
    private string _UNIT_PRICE = string.Empty;
    private string _WAERS = string.Empty;
    private string _TAXABLE = string.Empty;
    private string _ITEM_NOTE = string.Empty;

    private string rstatus = string.Empty;//to get released stauts
 
        private string maktx = string.Empty;
        private string saknr = string.Empty;

        private string TotalInAmt = string.Empty;
        private string INRCurrency = string.Empty;



        private string miscid = string.Empty;
        private string bwerksid = string.Empty;
        private string swerskid = string.Empty;
        private string pspnrid = string.Empty;
        private string spartid = string.Empty;
        private string Regionid = string.Empty;
        private string Regiontxt = string.Empty;



        public string MISCIDID
        {
            get { return miscid; }
            set { miscid = value; }
        }


        public string BWERKSID
        {
            get { return bwerksid; }
            set { bwerksid = value; }
        }
        public string SWERKSID
        {
            get { return swerskid; }
            set { swerskid = value; }
        }
        public string PSPNRID
        {
            get { return pspnrid; }
            set { pspnrid = value; }
        }
        public string SPARTID
        {
            get { return spartid; }
            set { spartid = value; }
        } 



    public int? PRID
    {
        get { return prid; }
        set { prid = value; }
    }

    public string PERNR
    {
        get { return Pernr; }
        set { Pernr = value; }
    }

    public string ENAME
    {
        get { return ename; }
        set { ename = value; }
    }

    //----- CREATE PR REQ ------
    public int? IBANFN_EXT
    {
        get { return ibanfn_ext; }
        set { ibanfn_ext = value; }
    }

    public string IPERNR
    {
        get { return iPernr; }
        set { iPernr = value; }
    }


    public string RPERNR
    {
        get { return rPernr; }
        set { rPernr = value; }
    }


    public string PFUNC_AREA
    {
        get { return pFunc_Area; }
        set { pFunc_Area = value; }
    }






    public string BTEXT
    {
        get { return pBtext; }
        set { pBtext = value; }
    }


    public string MIS_GRPC
    {
        get { return mis_grpc; }
        set { mis_grpc = value; }
    }



    public string MIS_GRPA
    {
        get { return mis_grpa; }
        set { mis_grpa = value; }
    }


    public string MIS_GRPB
    {
        get { return mis_grpb; }
        set { mis_grpb = value; }
    }


    public string EKGRP
    {
        get { return ekgrp; }
        set { ekgrp = value; }
    }


    public string BWERKS
    {
        get { return Bwerks; }
        set { Bwerks = value; }
    }


    public string SWERKS
    {
        get { return Swerks; }
        set { Swerks = value; }
    }


    public string SUG_SUPP
    {
        get { return Sug_supplier; }
        set { Sug_supplier = value; }
    }



    public string SUP_ADDRESS
    {
        get { return Sup_Address; }
        set { Sup_Address = value; }
    }


    public string SUP_PHONE
    {
        get { return Sup_phone; }
        set { Sup_phone = value; }
    }


    public string IN_BUDGET
    {
        get { return pIn_Budget; }
        set { pIn_Budget = value; }
    }

    public string CAPITALIZED
    {
        get { return pCapitalized; }
        set { pCapitalized = value; }
    }

    public string CAP_TEXT
    {
        get { return pCap_Text; }
        set { pCap_Text = value; }
    }


    public string SERVICE_BUREA
    {
        get { return pservice_burea; }
        set { pservice_burea = value; }
    }



    public string CRITICALITY
    {
        get { return pCriticality; }
        set { pCriticality = value; }
    }

    public string PSPNR
    {
        get { return pspnr; }
        set { pspnr = value; }
    }


    public string VERNR
    {
        get { return vernr; }
        set { vernr = value; }
    }


    public string BILLABLE
    {
        get { return billable; }
        set { billable = value; }
    }


    public string PROPOSAL
    {
        get { return proposal; }
        set { proposal = value; }
    }


    public string PFID
    {
        get { return pfid; }
        set { pfid = value; }
    }


    public string PFPATH
    {
        get { return pfpath; }
        set { pfpath = value; }
    }


    public string AGREEMENT
    {
        get { return agreement; }
        set { agreement = value; }
    }


    public string AFID
    {
        get { return afid; }
        set { afid = value; }
    }


    public string AFPATH
    {
        get { return afpath; }
        set { afpath = value; }
    }

    public string EMAIL_COM
    {
        get { return email_com; }
        set { email_com = value; }
    }


    public string EFID
    {
        get { return efid; }
        set { efid = value; }
    }


    public string EFPATH
    {
        get { return efpath; }
        set { efpath = value; }
    }


    public string INVOICE
    {
        get { return invoice; }
        set { invoice = value; }
    }

    public string IFID
    {
        get { return ifid; }
        set { ifid = value; }
    }


    public string IFPATH
    {
        get { return ifpath; }
        set { ifpath = value; }
    }

    public string SPART
    {
        get { return spart; }
        set { spart = value; }
    }


    public string JUSTIFICATION
    {
        get { return justification; }
        set { justification = value; }
    }

    public string SPL_NOTES
    {
        get { return spl_notes; }
        set { spl_notes = value; }
    }

    public DateTime? CREATED_ON1
    {
        get { return created_on1; }
        set { created_on1 = value; }
    }


    public string CREATEDBY
    {
        get { return createdby; }
        set { createdby = value; }
    }
    public DateTime? APP_ON1
    {
        get { return app_on1; }
        set { app_on1 = value; }
    }

    public string APPROVEDBY1
    {
        get { return approvedby1; }
        set { approvedby1 = value; }
    }


    public DateTime? HOLD_ON1
    {
        get { return hold_on1; }
        set { hold_on1 = value; }
    }


    public DateTime? RELEASED_ON1
    {
        get { return Released_on1; }
        set { Released_on1 = value; }
    }

    public string COMMENTS1
    {
        get { return comments1; }
        set { comments1 = value; }
    }

    public DateTime? APP_ON2
    {
        get { return app_on2; }
        set { app_on2 = value; }
    }

    public string APPROVEDBY2
    {
        get { return approvedby2; }
        set { approvedby2 = value; }
    }


    public DateTime? HOLD_ON2
    {
        get { return hold_on2; }
        set { hold_on2 = value; }
    }

    public DateTime? RELEASED_ON2
    {
        get { return Released_on2; }
        set { Released_on2 = value; }
    }

    public string COMMENTS2
    {
        get { return comments2; }
        set { comments2 = value; }
    }

    public DateTime? APP_ON3
    {
        get { return app_on3; }
        set { app_on3 = value; }
    }

    public string APPROVEDBY3
    {
        get { return approvedby3; }
        set { approvedby3 = value; }
    }

    public DateTime? HOLD_ON3
    {
        get { return hold_on3; }
        set { hold_on3 = value; }
    }

    public DateTime? RELEASED_ON3
    {
        get { return Released_on3; }
        set { Released_on3 = value; }
    }

    public string COMMENTS3
    {
        get { return comments3; }
        set { comments3 = value; }
    }

    public DateTime? APP_ON4
    {
        get { return app_on4; }
        set { app_on4 = value; }
    }

    public string APPROVEDBY4
    {
        get { return approvedby4; }
        set { approvedby4 = value; }
    }

    public DateTime? HOLD_ON4
    {
        get { return hold_on4; }
        set { hold_on4 = value; }
    }

    public DateTime? RELEASED_ON4
    {
        get { return Released_on4; }
        set { Released_on4 = value; }
    }

    public string COMMENTS4
    {
        get { return comments4; }
        set { comments4 = value; }
    }

    public DateTime? APP_ON5
    {
        get { return app_on5; }
        set { app_on5 = value; }
    }

    public string APPROVEDBY5
    {
        get { return approvedby5; }
        set { approvedby5 = value; }
    }

    public DateTime? HOLD_ON5
    {
        get { return hold_on5; }
        set { hold_on5 = value; }
    }

    public DateTime? RELEASED_ON5
    {
        get { return Released_on5; }
        set { Released_on5 = value; }
    }

    public string COMMENTS5
    {
        get { return comments5; }
        set { comments5 = value; }
    }

    public DateTime? APP_ON6
    {
        get { return app_on6; }
        set { app_on6 = value; }
    }

    public string APPROVEDBY6
    {
        get { return approvedby6; }
        set { approvedby6 = value; }
    }

    public DateTime? HOLD_ON6
    {
        get { return hold_on6; }
        set { hold_on6 = value; }
    }

    public DateTime? RELEASED_ON6
    {
        get { return Released_on6; }
        set { Released_on6 = value; }
    }

    public string COMMENTS6
    {
        get { return comments6; }
        set { comments6 = value; }
    }

    public string STATUS
    {
        get { return status; }
        set { status = value; }
    }

    public string APPROVEDBY1N
    {
        get { return approved_by1n; }
        set { approved_by1n = value; }

    }

    public string APPROVEDBY2N
    {
        get { return approved_by2n; }
        set { approved_by2n = value; }

    }
    public string APPROVEDBY3N
    {
        get { return approved_by3n; }
        set { approved_by3n = value; }

    }
    public string APPROVEDBY4N
    {
        get { return approved_by4n; }
        set { approved_by4n = value; }

    }
    public string APPROVEDBY5N
    {
        get { return approved_by5n; }
        set { approved_by5n = value; }

    }
    public string APPROVEDBY6N
    {
        get { return approved_by6n; }
        set { approved_by6n = value; }

    }

    //----- ADD PR ITEMS --------
    public Guid? id
    {
        get { return ID; }
        set { ID = value; }
    }


    public int? BANFN_EXT
    {
        get { return _BANFN_EXT; }
        set { _BANFN_EXT = value; }
    }
    public string BNFPO
    {
        get { return _BNFPO; }
        set { _BNFPO = value; }
    }


    public string MATNR
    {
        get { return _MATNR; }
        set { _MATNR = value; }
    }
    public string TXZ01
    {
        get { return _TXZ01; }
        set { _TXZ01 = value; }
    }
    public string PART_NO
    {
        get { return _PART_NO; }
        set { _PART_NO = value; }
    }
    public string MTART
    {
        get { return _MTART; }
        set { _MTART = value; }
    }


    public string MAKTX
    {
        get { return maktx; }
        set { maktx = value; }
    }
    public string MEINS
    {
        get { return _MEINS; }
        set { _MEINS = value; }
    }


    public string NO_OF_UNITS
    {
        get { return _NO_OF_UNITS; }
        set { _NO_OF_UNITS = value; }
    }


    public string UNIT_PRICE
    {
        get { return _UNIT_PRICE; }
        set { _UNIT_PRICE = value; }
    }


    public string WAERS
    {
        get { return _WAERS; }
        set { _WAERS = value; }
    }


    public string TAXABLE
    {
        get { return _TAXABLE; }
        set { _TAXABLE = value; }
    }
    public string ITEM_NOTE
    {
        get { return _ITEM_NOTE; }
        set { _ITEM_NOTE = value; }
    }

    public string TOTAL
    {
        get { return total; }
        set { total = value; }
    }

    public string RSTATUS
    {

        get { return rstatus; }
        set { rstatus = value; }
    }


    public string SAKNR
    {
        get { return saknr; }
        set { saknr = value; }
    }

    public string TAINRAmt
    {
        get { return TotalInAmt; }
        set { TotalInAmt = value; }
    }
    public string INRCURR
    {
        get { return INRCurrency; }
        set { INRCurrency = value; }
    }

    public string REGIONID
    {
        get { return Regionid; }
        set { Regionid = value; }
    }
    public string REGIONTXT
    {
        get { return Regiontxt; }
        set { Regiontxt = value; }
    }

    public long? RowNum { get; set; }
}

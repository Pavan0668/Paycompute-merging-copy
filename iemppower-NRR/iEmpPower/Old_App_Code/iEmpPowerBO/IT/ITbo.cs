using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.IT
{
    public class ITbo
    {
        // select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM

        public string CONACTPROP { get; set; }
        public decimal? SBSEC { get; set; }
        public decimal? SBDIV { get; set; }
        public string SBDDS { get; set; }
        public decimal? SDVLT { get; set; }
        public decimal? TXEXM { get; set; }
        public string CURR { get; set; }
        public decimal? PROPCONTR { get; set; }
        public decimal? ACTCONTR { get; set; }
        public string REMARKS { get; set; }
        public string RECEIPT_FID { get; set; }
        public string RECEIPT_FILE { get; set; }
        public string RECEIPT_FPATH { get; set; }
        public string EMPCOMMENTS { get; set; }
        public string EMPCOMMENTS2 { get; set; }

        public bool? CA80 { get; set; }
        public bool? CA80C { get; set; }

        //t2.ICODE, t2.ITTXT , t4.ITLMT, t4.CTGRY

        public decimal? ICODE { get; set; }
        public decimal? ITLMT { get; set; }
        public string ITTXT { get; set; }
        public string CTGRY { get; set; }
        public decimal? PROPINVST { get; set; }
        public decimal? ACTINVST { get; set; }



        public string ACCOM { get; set; }
        public string METRO { get; set; }
        public decimal? RTAMT { get; set; }
        public int? HRTXE { get; set; }
        public string LDAD1 { get; set; }
        public string LDAID { get; set; }
        public string LDADE { get; set; }
        public string CREATED_BY { get; set; }
        public string PERNR { get; set; }
        public string APPROVED_BY { get; set; }
        public string STATUS { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public DateTime? BEGDA { get; set; }
        public DateTime? ENDDA { get; set; }
        public DateTime? APPROVEDON { get; set; }
        public int? ID { get; set; }
        public string MODIFIED_BY { get; set; }
        public DateTime? MODIFIED_ON { get; set; }
        public int? Flag { get; set; }

        public string PROPTYP { get; set; }
        public string RENTO { get; set; }
        public decimal? INT24 { get; set; }
        public decimal? LETVL { get; set; }
        public decimal? REP24 { get; set; }
        public decimal? OTH24 { get; set; }
        public decimal? TDSOT { get; set; }

        public decimal? BSPFT { get; set; }
        public decimal? CPGLN { get; set; }
        public decimal? CPGLS { get; set; }
        public decimal? CPGNS { get; set; }
        public decimal? CPGSS { get; set; }
        public decimal? DVDND { get; set; }
        public decimal? INTRS { get; set; }
        public decimal? UNSPI { get; set; }
        public decimal? TDSAT { get; set; }

        public int? LID { get; set; }

        public string ITTYP { get; set; }

        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string LDNAM { get; set; }


        public bool? ITLOCK
        {
            get;
            set;
        }

        public string ENAME { get; set; }

        public int? ITSLAB
        {
            get;
            set;
        }

        public string LENDNAME { get; set; }
        public string LENDPAN { get; set; }
        public string LENDADD { get; set; }
        public string ADDPROPTY { get; set; }
        public string PUPSHSLN { get; set; }
        public DateTime? LNSAC_DATE { get; set; }
        public DateTime? STMPCHR_DATE { get; set; }
        public decimal? PERCNT { get; set; }
        public int? RID
        {
            get;
            set;
        }



        public string NAME
        {
            get;
            set;
        }

        ///New
        ///
        public string PreEmpr { get; set; }
        public string PreEmprPAN { get; set; }
        public string PreEmprTAN { get; set; }



        public decimal? VPRQS { get; set; }
        public decimal? PRSAL { get; set; }
        public decimal? EXS10 { get; set; }
        public decimal? PRTAX { get; set; }
        public decimal? PRFND { get; set; }
        public decimal? TXDED { get; set; }
        public decimal? SCDED { get; set; }
        public decimal? GRSAL { get; set; }
        public decimal? ECDED { get; set; }
    }
}
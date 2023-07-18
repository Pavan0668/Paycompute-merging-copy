using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.RRF
{
    public class RrfBo
    {
        public RrfBo()
        {
        }
        public string STATUS { get; set; }
        public string DESIG { get; set; }
        public string DEPT { get; set; }
        public string PERNR { get; set; }
        public string ENAME { get; set; }
        public string RGION { get; set; }
        public string TEXT25 { get; set; }
        public string PSPNR { get; set; }
        public string POST1 { get; set; }
        public string PLANS { get; set; }
        public string PLSXT { get; set; }
        public decimal? ORGEH { get; set; }
        public string ORGTX { get; set; }

        public int QID { get; set; }
        public string QNAME { get; set; }

        public string INDNTR_NAME { get; set; }
        public string IND_ENAME{ get; set; }
        public string REQTR_NAME { get; set; }
        public string REQT_ENAME { get; set; }
        public string DES_RECUTD { get; set; }
        public string DESRTEXT{ get; set; }
        public bool? REP_EXT_EMP { get; set; }
        public string REP_EXT_EMP_ID { get; set; }
        public string REP_EXT_EMP_ENAME { get; set; }
        public bool? REQ_POS_BUDGT { get; set; }
        public string REQ_POS_BUDGT_FRM_MONTH { get; set; }
        public double? REQ_POS_BUDGT_COST { get; set; }
        public string PURPS_HIRNG { get; set; }
        public string PURPS_HIRNG_LOC { get; set; }
        public string PURPS_HIRNG_LOC_TEXT { get; set; }
        public string PURPS_HIRNG_PROJ { get; set; }
        public string PURPS_HIRNG_PROJ_TEXT { get; set; }
        public string POS_REPT_TO_ID { get; set; }
        public string POS_REPT_TO_ID_ENAME { get; set; }
        public int? MIN_EDU_QLAFTN { get; set; }
        public string MIN_CERTIFNTN { get; set; }
        public string TOT_EXP { get; set; }
        public string TOT_DOMAIN_EXP { get; set; }
        public string AREA_EXPRTSE { get; set; }
        public string OTHER_SPC_REQ { get; set; }
        public string JOB_DISP { get; set; }
        public string DISP_FILE { get; set; }
        public DateTime TENTTIVE_DATE { get; set; }
        public int? NORESOURCE { get; set; }

        public DateTime CREATED_ON { get; set; }
        public DateTime MODIFIED_ON { get; set; }

        public int ID { get; set; }
        public int RID { get; set; }
        public string APPROVED1_ID { get; set; }
        public DateTime APPROVED1_ON { get; set; }
        public string APPROVED1_REMARKS { get; set; }
        public string APPROVED2_ID { get; set; }
        public DateTime APPROVED2_ON { get; set; }
        public string APPROVED2_REMARKS { get; set; }
        public string APPROVED3_ID { get; set; }
        public DateTime APPROVED3_ON { get; set; }
        public string APPROVED3_REMARKS { get; set; }
        public string APPROVED4_ID { get; set; }
        public DateTime APPROVED4_ON { get; set; }
        public string APPROVED4_REMARKS { get; set; }
        public string APPROVED5_ID { get; set; }
        public DateTime APPROVED5_ON { get; set; }
        public string APPROVED5_REMARKS { get; set; }

        public string APPROVED1_ID_ENAME { get; set; }
        public string APPROVED2_ID_ENAME { get; set; }
        public string APPROVED3_ID_ENAME { get; set; }
        public string APPROVED4_ID_ENAME { get; set; }
        public string APPROVED5_ID_ENAME { get; set; }

        public string APP_PERNR { get; set; }
        public int FLG { get; set; }
        public string REMARKS { get; set; }
        public string EDU_QLATEXT { get; set; }

    }

}
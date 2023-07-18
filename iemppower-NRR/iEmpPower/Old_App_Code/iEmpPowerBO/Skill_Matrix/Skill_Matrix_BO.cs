using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Skill_Matrix
{
    public class Skill_Matrix_BO
    {
        //----------------------------- Skill module DDL ------------------------------------------
        public int MID { get; set; }
        public string MODULE { get; set; }


        //------------------------------ Skill sub module DDL -------------------------------------
        public int SID { get; set; }
        public string S_MODULE { get; set; }

        //---------------------------------Skill Ename DDL ----------------------------------------
        public string EmpIDPA0001 { get; set; }
        public string EmpNamePA0001 { get; set; }

        //-------------------------------- Skill Save deatails to tbl -----------------------------
        public decimal marks { get; set; }
        public string comments { get; set; }

        public string LOGPERNR { get; set; }
        public int LblID { get; set; }

        public string For_month { get; set; }


        //--------------------------------- View Records from db --------------------------------------

        public string PERNR { get; set; }
        public int V_MODULE { get; set; }
        public int SUB_MODULE { get; set; }
        public string MOD_name { get; set; }
        public string ENAME { get; set; }
        public string SUB__MOD_NAME { get; set; }
        public decimal SCORE { get; set; }
        public string REMARKS { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Modified_On { get; set; }
        public string login { get; set; }
        public string mod_modifiedby { get; set; }
        public string submod_modifiedby { get; set; }
        public string SbCreated_By { get; set; }

        public string PHN { get; set; }
        public string PLSXT { get; set; }
        public string USRID { get; set; }
    }

}
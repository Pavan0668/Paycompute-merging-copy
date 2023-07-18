using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBO.RRF;
using iEmpPower.Old_App_Code.iEmpPowerBO.RRF.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.RRF;
namespace iEmpPower.Old_App_Code.iEmpPowerBL.RRF
{
    public class RrfBl
    {
        public static RrfCollectionBo Load_Departmnts()
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            RrfCollectionBo objList = new RrfCollectionBo();
            foreach (var vRow in objDataContext.sp_get_Organizationunit())
            {
                RrfBo objBo = new RrfBo();
                objBo.ORGEH = vRow.ORGEH;
                objBo.ORGTX = vRow.ORGTX.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static RrfCollectionBo Load_reqtr_dtls()
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            RrfCollectionBo objList = new RrfCollectionBo();
            foreach (var vRow in objDataContext.usp_rrf_get_requestor_details())
            {
                RrfBo objBo = new RrfBo();
                objBo.PERNR = vRow.PERNR.Trim();
                objBo.ENAME = vRow.ENAME.Trim();
                objBo.DEPT = vRow.dept.Trim();
                objBo.DESIG = vRow.Desig.Trim();
                objBo.PLANS = vRow.DESIGPLNS;
                objBo.ORGEH = vRow.deptID;
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static RrfCollectionBo Load_desig_dtls()
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            RrfCollectionBo objList = new RrfCollectionBo();
            foreach (var vRow in objDataContext.usp_rrf_get_designations_details())
            {
                RrfBo objBo = new RrfBo();
                objBo.PLANS = vRow.PLANS.Trim();
                objBo.PLSXT = vRow.PLSXT.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static RrfCollectionBo Load_regions()
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            RrfCollectionBo objList = new RrfCollectionBo();
            foreach (var vRow in objDataContext.usp_rrf_load_regions())
            {
                RrfBo objBo = new RrfBo();
                objBo.RGION = vRow.RGION.Trim();
                objBo.TEXT25 = vRow.TEXT25.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static RrfCollectionBo Load_projects()
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            RrfCollectionBo objList = new RrfCollectionBo();
            foreach (var vRow in objDataContext.usp_rrf_load_projects())
            {
                RrfBo objBo = new RrfBo();
                objBo.PSPNR = vRow.PSPNR.Trim();
                objBo.POST1 = vRow.POST1.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static RrfCollectionBo Load_Employees()
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            RrfCollectionBo objList = new RrfCollectionBo();
            foreach (var vRow in objDataContext.usp_Emp_names_id_rrf())
            {
                RrfBo objBo = new RrfBo();
                objBo.PERNR = vRow.PERNR.Trim();
                objBo.ENAME = vRow.PERNR.Trim() + " - " + vRow.ENAME.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static RrfCollectionBo Load_Pos_manager(string PLANS)
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            RrfCollectionBo objList = new RrfCollectionBo();
            foreach (var vRow in objDataContext.usp_rrf_pos_rep_manager(PLANS))
            {
                RrfBo objBo = new RrfBo();
                objBo.PERNR = vRow.PERNR.Trim();
                objBo.ENAME = vRow.ENAME.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }

        public static RrfCollectionBo Load_Edu_Qlatn()
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            RrfCollectionBo objList = new RrfCollectionBo();
            foreach (var vRow in objDataContext.usp_rrf_eductn_qualiftn())
            {
                RrfBo objBo = new RrfBo();
                objBo.QID = vRow.qualification_id;
                objBo.QNAME = vRow.qualification_name.Trim();
                objList.Add(objBo);
            }
            objDataContext.Dispose();
            return objList;
        }


        public int CREATE_RRF_REC(RrfBo objBo, int flg, ref int? ErrorCode, ref string reqmail,ref string supmail,ref int? RID)
        {
            try
            {
                RrfDALDataContext objTicketingToolDataContext = new RrfDALDataContext();

                int iResultCode = objTicketingToolDataContext.usp_rrf_create_rec(objBo.INDNTR_NAME
                    , objBo.REQTR_NAME
                    , objBo.DES_RECUTD
                    , objBo.REP_EXT_EMP
                    , objBo.REP_EXT_EMP_ID
                    , objBo.REQ_POS_BUDGT
                    , objBo.REQ_POS_BUDGT_FRM_MONTH
                    , objBo.REQ_POS_BUDGT_COST
                    , objBo.PURPS_HIRNG
                    , objBo.PURPS_HIRNG_LOC
                    , objBo.PURPS_HIRNG_PROJ
                    , objBo.POS_REPT_TO_ID
                    , objBo.MIN_EDU_QLAFTN
                    , objBo.MIN_CERTIFNTN
                    , objBo.TOT_EXP
                    , objBo.TOT_DOMAIN_EXP
                    , objBo.AREA_EXPRTSE
                    , objBo.OTHER_SPC_REQ
                    , objBo.JOB_DISP
                    , objBo.DISP_FILE
                    , objBo.TENTTIVE_DATE
                    , objBo.NORESOURCE
                    , flg
                    , ref ErrorCode
                    ,ref reqmail
                    ,ref supmail,
                    ref RID);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }



        public List<RrfBo> Get_RRFreq(string Pernr, int flg)
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            List<RrfBo> objList = new List<RrfBo>();
            foreach (var vRow in objDataContext.usp_rrf_get_request(Pernr, flg))
            {
                RrfBo ObjBo = new RrfBo();
                ObjBo.ID = vRow.RRF_ID;
                ObjBo.INDNTR_NAME = vRow.INDNTR_NAME;
                ObjBo.IND_ENAME = vRow.IND_ENAME;
                ObjBo.REQTR_NAME = vRow.REQTR_NAME;
                ObjBo.REQT_ENAME = vRow.REQT_ENAME;
                ObjBo.DES_RECUTD = vRow.DES_RECUTD;
                ObjBo.DESRTEXT = vRow.DESREQ_TEXT;
                ObjBo.REP_EXT_EMP = vRow.REP_EXT_EMP;
                ObjBo.REP_EXT_EMP_ID = vRow.REP_EXT_EMP_ID;
                ObjBo.REP_EXT_EMP_ENAME = vRow.EXT_EMP_ENAME;
                ObjBo.REQ_POS_BUDGT = vRow.REQ_POS_BUDGT;
                ObjBo.REQ_POS_BUDGT_FRM_MONTH = vRow.REQ_POS_BUDGT_FRM_MONTH;
                ObjBo.REQ_POS_BUDGT_COST = vRow.REQ_POS_BUDGT_COST;
                ObjBo.PURPS_HIRNG = vRow.PURPS_HIRNG;
                ObjBo.PURPS_HIRNG_LOC = vRow.PURPS_HIRNG_LOC;
                ObjBo.PURPS_HIRNG_LOC_TEXT = vRow.LOCTEXT;
                ObjBo.PURPS_HIRNG_PROJ = vRow.PURPS_HIRNG_PROJ;
                ObjBo.PURPS_HIRNG_PROJ_TEXT = vRow.PROJTEXT;
                ObjBo.POS_REPT_TO_ID = vRow.POS_REPT_TO_ID;
                ObjBo.POS_REPT_TO_ID_ENAME = vRow.POSREP_ENAME;
                ObjBo.MIN_EDU_QLAFTN = vRow.MIN_EDU_QLAFTN;
                ObjBo.MIN_CERTIFNTN = vRow.MIN_CERTIFNTN;
                ObjBo.TOT_EXP = vRow.TOT_EXP;
                ObjBo.TOT_DOMAIN_EXP = vRow.TOT_DOMAIN_EXP;
                ObjBo.AREA_EXPRTSE = vRow.AREA_EXPRTSE;
                ObjBo.OTHER_SPC_REQ = vRow.OTHER_SPC_REQ;
                ObjBo.JOB_DISP = vRow.JOB_DISP;
                ObjBo.DISP_FILE = vRow.DISP_FILE;
                ObjBo.TENTTIVE_DATE = vRow.TENTTIVE_DATE;
                ObjBo.NORESOURCE = vRow.NORESOURCE;

                ObjBo.APPROVED1_ID = vRow.APPROVED1_ID;
                ObjBo.APPROVED1_ON = Convert.ToDateTime(vRow.APPROVED1_ON);
                ObjBo.APPROVED1_REMARKS = vRow.APPROVED1_REMARKS;
                ObjBo.APPROVED2_ID = vRow.APPROVED2_ID;
                ObjBo.APPROVED2_ON = Convert.ToDateTime(vRow.APPROVED2_ON);
                ObjBo.APPROVED2_REMARKS = vRow.APPROVED2_REMARKS;
                ObjBo.APPROVED3_ID = vRow.APPROVED3_ID;
                ObjBo.APPROVED3_ON = Convert.ToDateTime(vRow.APPROVED3_ON);
                ObjBo.APPROVED3_REMARKS = vRow.APPROVED3_REMARKS;
                ObjBo.APPROVED4_ID = vRow.APPROVED4_ID;
                ObjBo.APPROVED4_ON = Convert.ToDateTime(vRow.APPROVED4_ON);
                ObjBo.APPROVED4_REMARKS = vRow.APPROVED4_REMARKS;
                ObjBo.APPROVED5_ID = vRow.APPROVED5_ID;
                ObjBo.APPROVED5_ON = Convert.ToDateTime(vRow.APPROVED5_ON);
                ObjBo.APPROVED5_REMARKS = vRow.APPROVED5_REMARKS;

                ObjBo.APPROVED1_ID_ENAME = vRow.APP1ENAME;
                ObjBo.APPROVED2_ID_ENAME = vRow.APP2ENAME;
                ObjBo.APPROVED3_ID_ENAME = vRow.APP3ENAME;
                ObjBo.APPROVED4_ID_ENAME = vRow.APP4ENAME;
                ObjBo.APPROVED5_ID_ENAME = vRow.APP5ENAME;
                ObjBo.STATUS = vRow.STATUS;
                ObjBo.EDU_QLATEXT = vRow.qualification_name;
                objList.Add(ObjBo);
            }
            //objDataContext.Dispose();
            return objList;
        }

        public List<RrfBo> Get_RRF_ID_Dtls(int RID, int flg)
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            List<RrfBo> objList = new List<RrfBo>();
            foreach (var vRow in objDataContext.usp_rrf_get_details(RID, flg))
            {
                RrfBo ObjBo = new RrfBo();
                ObjBo.ID = vRow.ID;
                ObjBo.INDNTR_NAME = vRow.INDNTR_NAME;
                ObjBo.REQTR_NAME = vRow.REQTR_NAME;
                ObjBo.DES_RECUTD = vRow.DES_RECUTD;
                ObjBo.REP_EXT_EMP = vRow.REP_EXT_EMP;
                ObjBo.REP_EXT_EMP_ID = vRow.REP_EXT_EMP_ID;
                ObjBo.REQ_POS_BUDGT = vRow.REQ_POS_BUDGT;
                ObjBo.REQ_POS_BUDGT_FRM_MONTH = vRow.REQ_POS_BUDGT_FRM_MONTH;
                ObjBo.REQ_POS_BUDGT_COST = vRow.REQ_POS_BUDGT_COST;
                ObjBo.PURPS_HIRNG = vRow.PURPS_HIRNG;
                ObjBo.PURPS_HIRNG_LOC = vRow.PURPS_HIRNG_LOC;
                ObjBo.PURPS_HIRNG_PROJ = vRow.PURPS_HIRNG_PROJ;
                ObjBo.POS_REPT_TO_ID = vRow.POS_REPT_TO_ID;
                ObjBo.MIN_EDU_QLAFTN = vRow.MIN_EDU_QLAFTN;
                ObjBo.MIN_CERTIFNTN = vRow.MIN_CERTIFNTN;
                ObjBo.TOT_EXP = vRow.TOT_EXP;
                ObjBo.TOT_DOMAIN_EXP = vRow.TOT_DOMAIN_EXP;
                ObjBo.AREA_EXPRTSE = vRow.AREA_EXPRTSE;
                ObjBo.OTHER_SPC_REQ = vRow.OTHER_SPC_REQ;
                ObjBo.JOB_DISP = vRow.JOB_DISP;
                ObjBo.DISP_FILE = vRow.DISP_FILE;
                ObjBo.TENTTIVE_DATE = vRow.TENTTIVE_DATE;
                ObjBo.NORESOURCE = vRow.NORESOURCE;

                //ObjBo.APPROVED1_ID = vRow.APPROVED1_ID;
                //ObjBo.APPROVED1_ON = Convert.ToDateTime(vRow.APPROVED1_ON);
                //ObjBo.APPROVED1_REMARKS = vRow.APPROVED1_REMARKS;
                //ObjBo.APPROVED2_ID = vRow.APPROVED2_ID;
                //ObjBo.APPROVED2_ON = Convert.ToDateTime(vRow.APPROVED2_ON);
                //ObjBo.APPROVED2_REMARKS = vRow.APPROVED2_REMARKS;
                //ObjBo.APPROVED3_ID = vRow.APPROVED3_ID;
                //ObjBo.APPROVED3_ON = Convert.ToDateTime(vRow.APPROVED3_ON);
                //ObjBo.APPROVED3_REMARKS = vRow.APPROVED3_REMARKS;
                //ObjBo.APPROVED4_ID = vRow.APPROVED4_ID;
                //ObjBo.APPROVED4_ON = Convert.ToDateTime(vRow.APPROVED4_ON);
                //ObjBo.APPROVED4_REMARKS = vRow.APPROVED4_REMARKS;
                //ObjBo.APPROVED5_ID = vRow.APPROVED5_ID;
                //ObjBo.APPROVED5_ON = Convert.ToDateTime(vRow.APPROVED5_ON);
                //ObjBo.APPROVED5_REMARKS = vRow.APPROVED5_REMARKS;
                objList.Add(ObjBo);
            }
            //objDataContext.Dispose();
            return objList;
        }


        public int UPDATE_RRF_REC(RrfBo objBo, int flg, ref int? ErrorCode,ref string reqmail,ref string supmail)
        {
            try
            {
                RrfDALDataContext objTicketingToolDataContext = new RrfDALDataContext();

                int iResultCode = objTicketingToolDataContext.usp_rrf_update_rec(objBo.RID, objBo.INDNTR_NAME
                    , objBo.REQTR_NAME
                    , objBo.DES_RECUTD
                    , objBo.REP_EXT_EMP
                    , objBo.REP_EXT_EMP_ID
                    , objBo.REQ_POS_BUDGT
                    , objBo.REQ_POS_BUDGT_FRM_MONTH
                    , objBo.REQ_POS_BUDGT_COST
                    , objBo.PURPS_HIRNG
                    , objBo.PURPS_HIRNG_LOC
                    , objBo.PURPS_HIRNG_PROJ
                    , objBo.POS_REPT_TO_ID
                    , objBo.MIN_EDU_QLAFTN
                    , objBo.MIN_CERTIFNTN
                    , objBo.TOT_EXP
                    , objBo.TOT_DOMAIN_EXP
                    , objBo.AREA_EXPRTSE
                    , objBo.OTHER_SPC_REQ
                    , objBo.JOB_DISP
                    , objBo.DISP_FILE
                    , objBo.TENTTIVE_DATE
                    , objBo.NORESOURCE
                    , flg
                    , ref ErrorCode
                    ,ref reqmail,
                    ref supmail);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }


        public int RRF_App_rej_sup(RrfBo objBo, ref bool? status, ref string Req_Eml, ref string Cur_Eml, ref string Nxt_Eml, ref string Indntr, ref string Desig_name, ref string Req_name,ref string curr_name)
        {
            try
            {
                RrfDALDataContext objTicketingToolDataContext = new RrfDALDataContext();

                int iResultCode = objTicketingToolDataContext.usp_rrf_Approver_app_rej_update(objBo.ID
                    ,objBo.APP_PERNR
                    ,objBo.REMARKS
                    ,objBo.FLG
                    , ref status
                    ,ref Req_Eml
                    ,ref Cur_Eml
                    ,ref Nxt_Eml
                    ,ref Indntr
                    ,ref Desig_name
                    , ref Req_name, ref curr_name);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public List<RrfBo> Get_RRFreq_through_id(int id, int flg)
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            List<RrfBo> objList = new List<RrfBo>();
            foreach (var vRow in objDataContext.usp_rrf_get_request_through_id(id, flg))
            {
                RrfBo ObjBo = new RrfBo();
                ObjBo.ID = vRow.RRF_ID;
                ObjBo.INDNTR_NAME = vRow.INDNTR_NAME;
                ObjBo.IND_ENAME = vRow.IND_ENAME;
                ObjBo.REQTR_NAME = vRow.REQTR_NAME;
                ObjBo.REQT_ENAME = vRow.REQT_ENAME;
                ObjBo.DES_RECUTD = vRow.DES_RECUTD;
                ObjBo.DESRTEXT = vRow.DESREQ_TEXT;
                ObjBo.REP_EXT_EMP = vRow.REP_EXT_EMP;
                ObjBo.REP_EXT_EMP_ID = vRow.REP_EXT_EMP_ID;
                ObjBo.REP_EXT_EMP_ENAME = vRow.EXT_EMP_ENAME;
                ObjBo.REQ_POS_BUDGT = vRow.REQ_POS_BUDGT;
                ObjBo.REQ_POS_BUDGT_FRM_MONTH = vRow.REQ_POS_BUDGT_FRM_MONTH;
                ObjBo.REQ_POS_BUDGT_COST = vRow.REQ_POS_BUDGT_COST;
                ObjBo.PURPS_HIRNG = vRow.PURPS_HIRNG;
                ObjBo.PURPS_HIRNG_LOC = vRow.PURPS_HIRNG_LOC;
                ObjBo.PURPS_HIRNG_LOC_TEXT = vRow.LOCTEXT;
                ObjBo.PURPS_HIRNG_PROJ = vRow.PURPS_HIRNG_PROJ;
                ObjBo.PURPS_HIRNG_PROJ_TEXT = vRow.PROJTEXT;
                ObjBo.POS_REPT_TO_ID = vRow.POS_REPT_TO_ID;
                ObjBo.POS_REPT_TO_ID_ENAME = vRow.POSREP_ENAME;
                ObjBo.MIN_EDU_QLAFTN = vRow.MIN_EDU_QLAFTN;
                ObjBo.MIN_CERTIFNTN = vRow.MIN_CERTIFNTN;
                ObjBo.TOT_EXP = vRow.TOT_EXP;
                ObjBo.TOT_DOMAIN_EXP = vRow.TOT_DOMAIN_EXP;
                ObjBo.AREA_EXPRTSE = vRow.AREA_EXPRTSE;
                ObjBo.OTHER_SPC_REQ = vRow.OTHER_SPC_REQ;
                ObjBo.JOB_DISP = vRow.JOB_DISP;
                ObjBo.DISP_FILE = vRow.DISP_FILE;
                ObjBo.TENTTIVE_DATE = vRow.TENTTIVE_DATE;
                ObjBo.NORESOURCE = vRow.NORESOURCE;

                ObjBo.APPROVED1_ID = vRow.APPROVED1_ID;
                ObjBo.APPROVED1_ON = Convert.ToDateTime(vRow.APPROVED1_ON);
                ObjBo.APPROVED1_REMARKS = vRow.APPROVED1_REMARKS;
                ObjBo.APPROVED2_ID = vRow.APPROVED2_ID;
                ObjBo.APPROVED2_ON = Convert.ToDateTime(vRow.APPROVED2_ON);
                ObjBo.APPROVED2_REMARKS = vRow.APPROVED2_REMARKS;
                ObjBo.APPROVED3_ID = vRow.APPROVED3_ID;
                ObjBo.APPROVED3_ON = Convert.ToDateTime(vRow.APPROVED3_ON);
                ObjBo.APPROVED3_REMARKS = vRow.APPROVED3_REMARKS;
                ObjBo.APPROVED4_ID = vRow.APPROVED4_ID;
                ObjBo.APPROVED4_ON = Convert.ToDateTime(vRow.APPROVED4_ON);
                ObjBo.APPROVED4_REMARKS = vRow.APPROVED4_REMARKS;
                ObjBo.APPROVED5_ID = vRow.APPROVED5_ID;
                ObjBo.APPROVED5_ON = Convert.ToDateTime(vRow.APPROVED5_ON);
                ObjBo.APPROVED5_REMARKS = vRow.APPROVED5_REMARKS;

                ObjBo.APPROVED1_ID_ENAME = vRow.APP1ENAME;
                ObjBo.APPROVED2_ID_ENAME = vRow.APP2ENAME;
                ObjBo.APPROVED3_ID_ENAME = vRow.APP3ENAME;
                ObjBo.APPROVED4_ID_ENAME = vRow.APP4ENAME;
                ObjBo.APPROVED5_ID_ENAME = vRow.APP5ENAME;
                ObjBo.STATUS = vRow.STATUS;
                ObjBo.EDU_QLATEXT = vRow.qualification_name;
                objList.Add(ObjBo);
            }
            //objDataContext.Dispose();
            return objList;
        }


        public List<RrfBo> Get_RRFreq_search(string Pernr, int flg,int type,int sear_type,string sear_text)
        {
            RrfDALDataContext objDataContext = new RrfDALDataContext();
            List<RrfBo> objList = new List<RrfBo>();
            foreach (var vRow in objDataContext.usp_rrf_search(sear_type,sear_text,type,Pernr,flg))
            {
                RrfBo ObjBo = new RrfBo();
                ObjBo.ID = vRow.RRF_ID;
                ObjBo.INDNTR_NAME = vRow.INDNTR_NAME;
                ObjBo.IND_ENAME = vRow.IND_ENAME;
                ObjBo.REQTR_NAME = vRow.REQTR_NAME;
                ObjBo.REQT_ENAME = vRow.REQT_ENAME;
                ObjBo.DES_RECUTD = vRow.DES_RECUTD;
                ObjBo.DESRTEXT = vRow.DESREQ_TEXT;
                ObjBo.REP_EXT_EMP = vRow.REP_EXT_EMP;
                ObjBo.REP_EXT_EMP_ID = vRow.REP_EXT_EMP_ID;
                ObjBo.REP_EXT_EMP_ENAME = vRow.EXT_EMP_ENAME;
                ObjBo.REQ_POS_BUDGT = vRow.REQ_POS_BUDGT;
                ObjBo.REQ_POS_BUDGT_FRM_MONTH = vRow.REQ_POS_BUDGT_FRM_MONTH;
                ObjBo.REQ_POS_BUDGT_COST = vRow.REQ_POS_BUDGT_COST;
                ObjBo.PURPS_HIRNG = vRow.PURPS_HIRNG;
                ObjBo.PURPS_HIRNG_LOC = vRow.PURPS_HIRNG_LOC;
                ObjBo.PURPS_HIRNG_LOC_TEXT = vRow.LOCTEXT;
                ObjBo.PURPS_HIRNG_PROJ = vRow.PURPS_HIRNG_PROJ;
                ObjBo.PURPS_HIRNG_PROJ_TEXT = vRow.PROJTEXT;
                ObjBo.POS_REPT_TO_ID = vRow.POS_REPT_TO_ID;
                ObjBo.POS_REPT_TO_ID_ENAME = vRow.POSREP_ENAME;
                ObjBo.MIN_EDU_QLAFTN = vRow.MIN_EDU_QLAFTN;
                ObjBo.MIN_CERTIFNTN = vRow.MIN_CERTIFNTN;
                ObjBo.TOT_EXP = vRow.TOT_EXP;
                ObjBo.TOT_DOMAIN_EXP = vRow.TOT_DOMAIN_EXP;
                ObjBo.AREA_EXPRTSE = vRow.AREA_EXPRTSE;
                ObjBo.OTHER_SPC_REQ = vRow.OTHER_SPC_REQ;
                ObjBo.JOB_DISP = vRow.JOB_DISP;
                ObjBo.DISP_FILE = vRow.DISP_FILE;
                ObjBo.TENTTIVE_DATE = vRow.TENTTIVE_DATE;
                ObjBo.NORESOURCE = vRow.NORESOURCE;

                ObjBo.APPROVED1_ID = vRow.APPROVED1_ID;
                ObjBo.APPROVED1_ON = Convert.ToDateTime(vRow.APPROVED1_ON);
                ObjBo.APPROVED1_REMARKS = vRow.APPROVED1_REMARKS;
                ObjBo.APPROVED2_ID = vRow.APPROVED2_ID;
                ObjBo.APPROVED2_ON = Convert.ToDateTime(vRow.APPROVED2_ON);
                ObjBo.APPROVED2_REMARKS = vRow.APPROVED2_REMARKS;
                ObjBo.APPROVED3_ID = vRow.APPROVED3_ID;
                ObjBo.APPROVED3_ON = Convert.ToDateTime(vRow.APPROVED3_ON);
                ObjBo.APPROVED3_REMARKS = vRow.APPROVED3_REMARKS;
                ObjBo.APPROVED4_ID = vRow.APPROVED4_ID;
                ObjBo.APPROVED4_ON = Convert.ToDateTime(vRow.APPROVED4_ON);
                ObjBo.APPROVED4_REMARKS = vRow.APPROVED4_REMARKS;
                ObjBo.APPROVED5_ID = vRow.APPROVED5_ID;
                ObjBo.APPROVED5_ON = Convert.ToDateTime(vRow.APPROVED5_ON);
                ObjBo.APPROVED5_REMARKS = vRow.APPROVED5_REMARKS;

                ObjBo.APPROVED1_ID_ENAME = vRow.APP1ENAME;
                ObjBo.APPROVED2_ID_ENAME = vRow.APP2ENAME;
                ObjBo.APPROVED3_ID_ENAME = vRow.APP3ENAME;
                ObjBo.APPROVED4_ID_ENAME = vRow.APP4ENAME;
                ObjBo.APPROVED5_ID_ENAME = vRow.APP5ENAME;
                ObjBo.STATUS = vRow.STATUS;
                ObjBo.EDU_QLATEXT = vRow.qualification_name;
                objList.Add(ObjBo);
            }
            //objDataContext.Dispose();
            return objList;
        }
    }
}
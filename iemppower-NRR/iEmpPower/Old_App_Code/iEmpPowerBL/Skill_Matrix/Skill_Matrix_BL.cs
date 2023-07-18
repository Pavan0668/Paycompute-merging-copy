using iEmpPower.Old_App_Code.iEmpPowerBO.Personal_Information.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerBO.Skill_Matrix;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Skill_Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.Skill_Matrix
{
    public class Skill_Matrix_BL
    {
        skill_moduleDataContext objSkillmatrixContext = new skill_moduleDataContext();


        //---------------------------------SKILL MATRIX EMPLOYEE DROPDOWN LIST--------------------------------------------- 


        public Skill_Matrix_Collectionbo Set_subordinate_DDL(string PERNR, int flag)
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();

            foreach (var ENrow in objSkillmatrixContext.usp_Skill_Ename_DDL(PERNR, flag))
            {

                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();

                objskillbo.EmpNamePA0001 = ENrow.ENAME;
                objskillbo.EmpIDPA0001 = ENrow.PERNR;

                objskilllst.Add(objskillbo);
            }
            return objskilllst;
        }


        //---------------------------------SKILL MATRIX MODULE DROPDOWN LIST--------------------------------------------- 

        public Skill_Matrix_Collectionbo Set_Skill_Details()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();

            foreach (var MRow in objSkillmatrixContext.usp_Skill_Module_DDL())
            {
                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();

                objskillbo.MID = MRow.MOD_ID;
                objskillbo.MODULE = MRow.MOD_NAME;

                objskilllst.Add(objskillbo);
            }
            return objskilllst;
        }



        //---------------------------------SKILL MATRIX SUB-MODULE DROPDOWN LIST--------------------------------------------- 

        public Skill_Matrix_Collectionbo Set_submodule_DDL(string MID)
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            foreach (var Subrow in objSkillmatrixContext.usp_Skill_Sub_Module_DDL(MID))
            {
                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();

                objskillbo.S_MODULE = Subrow.SUB__MOD_NAME;
                objskillbo.SID = Subrow.SUB_ID;

                objskilllst.Add(objskillbo);
            }
            return objskilllst;
        }


        //-----------------------------------SKILL MATRIX SAVE SCORE RECORD TO DB TBL-----------------------------------------------

        public int Save_Score_Records(Skill_Matrix_BO objskillbo, ref bool? status)
        {
            try
            {
                objSkillmatrixContext = new skill_moduleDataContext();
                int insertcodebl = objSkillmatrixContext.usp_Skill_Save_Score_Record(
                                                                                       objskillbo.EmpIDPA0001,
                                                                                       objskillbo.MID,
                                                                                       objskillbo.SID,
                                                                                       objskillbo.marks,
                                                                                       objskillbo.comments,
                                                                                       objskillbo.LOGPERNR,
                                                                                       objskillbo.Modified_On,
                                                                                       objskillbo.For_month,
                                                                                       ref status);


                return insertcodebl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }




        //--------------------------------------------- VIEW RECORDS FROM DB -----------------------------------------------------------

        public Skill_Matrix_Collectionbo view_saved_scores(string login, string PERNR, int MODULE, string Month, int Selected_type)
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            foreach (var vwscores in objSkillmatrixContext.usp_Skill_View_Score_Record(login, PERNR, MODULE, Month, Selected_type))
            {
                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();
                objskillbo.PERNR = vwscores.PERNR;
                objskillbo.ENAME = vwscores.ENAME;
                objskillbo.MOD_name = vwscores.MOD_NAME;
                objskillbo.SUB__MOD_NAME = vwscores.SUB__MOD_NAME;
                objskillbo.V_MODULE = vwscores.MODULE;
                objskillbo.SUB_MODULE = vwscores.SUB_MODULE;
                objskillbo.For_month = vwscores.Month_Of_Year;
                objskillbo.SCORE = vwscores.SCORE;
                objskillbo.REMARKS = vwscores.REMARKS;
                objskillbo.Created_By = vwscores.managr;
                objskillbo.Created_On = vwscores.Created_On;

                objskilllst.Add(objskillbo);
            }
            return objskilllst;
        }


        //--------------------------------------------- CHECK RECORDS FROM DB -----------------------------------------------------------
        public int Check_saved_scores(string PERNR, int V_MODULE, int SUB_MODULE, string V_month_yr, decimal SCORE, ref bool? status)
        {
            try
            {
                objSkillmatrixContext = new skill_moduleDataContext();
                int check_tbl = objSkillmatrixContext.usp_Skill_check_Row_Record(PERNR, V_MODULE, SUB_MODULE, V_month_yr, SCORE, ref status);



                return check_tbl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        //----------------------------------------------------------------- GET SUBORDINATES ---------------------------------------------------------
        public Skill_Matrix_Collectionbo get_subordinate_list(string PERNR)
        {

            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            foreach (var SubEmp in objSkillmatrixContext.usp_Skill_get_subordinates(PERNR))
            {
                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();



                objskillbo.PERNR = SubEmp.PERNR.ToString();
                objskillbo.ENAME = SubEmp.ENAME;
                objskillbo.PLSXT = SubEmp.PLSXT;
                objskillbo.USRID = SubEmp.USRID;
                objskillbo.PHN = SubEmp.PHN;



                objskilllst.Add(objskillbo);
            }
            objSkillmatrixContext.Dispose();
            return objskilllst;
        }


        //------------------------------------------------------ GET HR & MANAGEMENT ---------------------------------------------------------------
        public int checkHR_mngt(string PERNR, ref bool? status)
        {
            try
            {
                objSkillmatrixContext = new skill_moduleDataContext();
                int check_hrmng = objSkillmatrixContext.usp_CheckHR_Mnagmt(PERNR, ref status);



                return check_hrmng;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        // -------------------------------------------------- CHECK FOR SUBMODULE EXISTENCE ------------------------------------------------------
        public int Check_exist_submodules(string MODULE, string SUB_MODULE, ref bool? status)
        {
            try
            {
                objSkillmatrixContext = new skill_moduleDataContext();
                int check_dbtbl = objSkillmatrixContext.usp_Skill_check_submodule(MODULE, SUB_MODULE, ref status);



                return check_dbtbl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        //------------------------------------------------ ADD NEW SUBMODULES -----------------------------------------------------------
        public int add_submodules(Skill_Matrix_BO objskillbo)
        {
            try
            {
                objSkillmatrixContext = new skill_moduleDataContext();
                int add_submod = objSkillmatrixContext.usp_Skill_add_submodl(objskillbo.Created_By,
                                                                                       objskillbo.MID,
                                                                                       objskillbo.SUB__MOD_NAME);


                return add_submod;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        //------------------------------------------------ ADD MODULES ------------------------------------------------------------------
        public int check_add_modls(Skill_Matrix_BO objskillbo, ref int? result)
        {
            try
            {
                objSkillmatrixContext = new skill_moduleDataContext();
                int insertcodebl = objSkillmatrixContext.usp_Skill_add_modls(
                                                                          objskillbo.MODULE,
                                                                                objskillbo.Created_By,
                                                                                ref result);


                return insertcodebl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        //---------------------------------------------------------CHECK MODULES BEFORE ADD ---------------------------------------------------------
        public int Check_exist_modules(string MODULE, string Created_by, ref bool? status)
        {
            try
            {
                objSkillmatrixContext = new skill_moduleDataContext();
                int check_dbtbl = objSkillmatrixContext.usp_Skill_exists_modls(MODULE, Created_by, ref status);



                return check_dbtbl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        //-------------------------------------------------------------- VIEW MODULES & SUB MODULES -----------------------------------------------------------------
        public Skill_Matrix_Collectionbo viw_allmodls()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            foreach (var viewall in objSkillmatrixContext.usp_Skill_View_modlsubmodl())
            {
                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();
                //objskillbo.PERNR = viewall.Created_By;
                //objskillbo.ENAME = vwscores.ENAME;
                objskillbo.MID = viewall.MOD_ID;
                objskillbo.SID = viewall.SUB_ID;
                objskillbo.MOD_name = viewall.MOD_NAME;
                objskillbo.SUB__MOD_NAME = viewall.SUB__MOD_NAME;

                objskillbo.Created_By = viewall.Mod_createdBy;
                objskillbo.SbCreated_By = viewall.SubM_createdBy;

                objskillbo.mod_modifiedby = viewall.Mod_modifiedby;
                objskillbo.submod_modifiedby = viewall.SMod_modifiedby;

                objskilllst.Add(objskillbo);
            }
            return objskilllst;
        }
        //--------------------------------------------------------- CHECK SCORES ------------------------------------------------------------------
        //public int Check_saved_scores(string PERNR, int V_MODULE, int SUB_MODULE, string V_month_yr, decimal SCORE, ref bool? status)
        //{
        //    try
        //    {
        //        objSkillmatrixContext = new skill_moduleDataContext();
        //        int check_tbl = objSkillmatrixContext.usp_Skill_check_Row_Record(PERNR, V_MODULE, SUB_MODULE, V_month_yr, SCORE, ref status);



        //        return check_tbl;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //}
        //------------------------------------------------------- CHECK MODULES & SUBMODULES TO UPDATE ---------------------------------------------
        public int check_modsub_toupdate(string Module, string Submodule, ref bool? m_status, ref bool? s_status)
        {
            try
            {
                objSkillmatrixContext = new skill_moduleDataContext();
                int check_data = objSkillmatrixContext.usp_Skill_check_modsub_updt(Module, Submodule, ref m_status, ref s_status);



                return check_data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }


        public int update_modsub(Skill_Matrix_BO objskillbo, int flag)
        {
            try
            {
                objSkillmatrixContext = new skill_moduleDataContext();
                int updatemodls = objSkillmatrixContext.usp_Skill_update_modsub(objskillbo.MID,
                                                                                       objskillbo.SID,
                                                                                       objskillbo.MOD_name,
                                                                                       objskillbo.SUB__MOD_NAME,
                                                                                       objskillbo.PERNR,
                                                                                       objskillbo.LOGPERNR,
                                                                                       flag);


                return updatemodls;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public Skill_Matrix_Collectionbo srch_mod_submod(string loginID, string EmpId, int Module, int Submod, int Selected_type)
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            foreach (var search in objSkillmatrixContext.usp_Skill_srchall_modsubmod_Record(loginID, EmpId, Module, Submod, Selected_type))
            {
                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();
                //objskillbo.PERNR = viewall.Created_By;
                //objskillbo.ENAME = vwscores.ENAME;
                objskillbo.MID = search.MOD_ID;
                objskillbo.SID = search.SUB_ID;
                objskillbo.MOD_name = search.MOD_NAME;
                objskillbo.SUB__MOD_NAME = search.SUB__MOD_NAME;



                objskillbo.Created_By = search.Mod_createdBy;
                objskillbo.SbCreated_By = search.SubM_createdBy;



                objskillbo.mod_modifiedby = search.Mod_modifiedby;
                objskillbo.submod_modifiedby = search.SMod_modifiedby;



                objskilllst.Add(objskillbo);
            }
            return objskilllst;
        }

        public Skill_Matrix_Collectionbo irf_allemp_DDL()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            //IRF_DALDataContext objDataContextirf = new IRF_DALDataContext();
            foreach (var column in objSkillmatrixContext.usp_skill_srchmodsub_emplst())
            {
                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();
                objskillbo.PERNR = column.PERNR1;
                objskillbo.ENAME = column.ENAME1;
                objskilllst.Add(objskillbo);
            }
            return objskilllst;
        }
    }
}

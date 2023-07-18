using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute.SPayc_Collection_BO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.SPaycompute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.SPaycompute
{
    public class SPayc_BL
    {
        SPaycomputeDataContext objSPayDatacontext = new SPaycomputeDataContext();

        //-------------------------------------------------- SAVE SALARY RATES OF EMPLOYEES TO DB-------------------------------------------------------
        public int Save_Salary_EmpRates(SPaycompute_BO objSpayc, int flag)
        {
            try
            {
                objSPayDatacontext = new SPaycomputeDataContext();
                int insertcodebl = objSPayDatacontext.payc_save_salary_rates(
                                                                                       objSpayc.TXT,
                                                                                       objSpayc.col1,
                                                                                       objSpayc.CCD,
                                                                                       objSpayc.MNTH,
                                                                                       objSpayc.LOGIN_PERNR,
                                                                                       objSpayc.EID,
                                                                                       objSpayc.id1,
                                                                                        flag);




                return insertcodebl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        //-------------------------------------------------- SALARY RATES UPLOAD HISTORY-------------------------------------------------------

        public SPayc_Collection_BO set_createdbyDDL(string month, string USERID, string empid, string Ccode, int flag)
        {
            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();

            foreach (var ENrow in objSPayDatacontext.payc_salary_history(month, USERID, empid, Ccode, flag))
            {
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                objSpayc.col11 = ENrow.Company_Code;
                objSpayc.Created_BY = ENrow.Created_By;
                objSpayc.MNTH = ENrow.Salary_Month;
                objSpayc.TXT = ENrow.EmpID;
                objSpayc.begda = ENrow.Created_On;
                objSpayc.col10 = ENrow.Salary_Rate;
                objSpayc.CCD = ENrow.Salary_Component;
                objSpayc.col1 = ENrow.Ename;
                objSpayc.col5 = ENrow.Company_Name;
                objSpayc.id1 = ENrow.ID;
                objspaylst.Add(objSpayc);
            }
            return objspaylst;
        }

        //--------------------------------------------------- ADMIN SALARY EXPORT --------------------------------------------------------------

        public SPayc_Collection_BO export_admin(string company_code, string salary_month)
        {
            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();

            foreach (var ENrow in objSPayDatacontext.payc_get_salary_rates_toadmin(company_code, salary_month))
            {

                SPaycompute_BO objSpayc = new SPaycompute_BO();

                objSpayc.EID = ENrow.Emp_ID_Ref_No_;
                objSpayc.TXT = ENrow.Name;
                objSpayc.col1 = ENrow.Col_1;
                objSpayc.col2 = ENrow.Col_2;
                objSpayc.col3 = ENrow.Col_3;
                objSpayc.col4 = ENrow.Col_4;
                objSpayc.col5 = ENrow.Col_5;
                objSpayc.col6 = ENrow.Col_6;
                objSpayc.col7 = ENrow.Col_7;
                objSpayc.col8 = ENrow.Col_8;
                objSpayc.col9 = ENrow.Col_9;
                objSpayc.col10 = ENrow.Col_10;
                objSpayc.col11 = ENrow.Col_11;
                objSpayc.col12 = ENrow.Col_12;
                objSpayc.col13 = ENrow.Col_13;
                objSpayc.col14 = ENrow.Col_14;
                objSpayc.col15 = ENrow.Col_15;
                objSpayc.col16 = ENrow.Col_16;
                objSpayc.col17 = ENrow.Col_17;
                objSpayc.col18 = ENrow.Col_18;
                objSpayc.col19 = ENrow.Col_19;
                objSpayc.col20 = ENrow.Col_20;
                objSpayc.CCD = ENrow.Remarks;
                objspaylst.Add(objSpayc);
            }
            return objspaylst;
        }



        //============================================================== OPERATE MASTERS ===============================================================================

        //------------------------------------------ SALARY MASTERS DDL ----------------------------------------------------------
        public SPayc_Collection_BO Operate_Masters(string Salary_Compo, string Created_by, string Ccode, int flag)
        {
            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();

            foreach (var ENrow in objSPayDatacontext.payc_operate_masters(Salary_Compo, Created_by, Ccode, flag))
            {

                SPaycompute_BO objSpayc = new SPaycompute_BO();
                objSpayc.col11 = ENrow.Company_Code;
                objSpayc.Created_BY = ENrow.Created_By;
                objSpayc.createdon = ENrow.Created_On;
                objSpayc.CCD = ENrow.Salary_Component;
                objSpayc.SRTS = ENrow.ID;
                objspaylst.Add(objSpayc);
            }
            return objspaylst;
        }


        //------------------------------------------CHECK SALARY MASTERS TO ADD ----------------------------------------------------------
        public int Check_Scompo_toadd(SPaycompute_BO objmstrs,ref bool? sts)
        {
            try
            {
                SPaycomputeDataContext objSPayDatacontext = new SPaycomputeDataContext();
                int check_dbtbl = objSPayDatacontext.payc_save_masters(
                                                                        objmstrs.col1
                                                                        , objmstrs.col10
                                                                        , objmstrs.col11
                                                                        , objmstrs.col12
                                                                        , objmstrs.col13
                                                                        , objmstrs.ID
                                                                        , objmstrs.col14
                                                                        , objmstrs.begda
                                                                        , objmstrs.endda
                                                                        , objmstrs.begda1
                                                                        , objmstrs.endda1
                                                                        , objmstrs.col15
                                                                        , ref sts);



                return check_dbtbl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }



        //------------------------------------------SAVE SALARY MASTERS----------------------------------------------------------
        //public int Save_Scompo_mstrs(string Component, string txt, string txtA, string Created_by, string Companycode, ref bool? status, int flag)
        //{
        //    try
        //    {
        //        objSPayDatacontext = new SPaycomputeDataContext();
        //        int insertcodebl = objSPayDatacontext.payc_save_masters(Component, txt, txtA, Created_by, Companycode, ref status, flag);


        //        return insertcodebl;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //}

        public SPayc_Collection_BO Attdnce_exprt(string company_code, int flag, string fromdate, string todate)
        {
            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();

            foreach (var ENrow in objSPayDatacontext.payc_Working_days_calculation(company_code, flag, fromdate, todate))
            {
                SPaycompute_BO objSpayc = new SPaycompute_BO();

                objSpayc.EID = ENrow.EmpID;
                objSpayc.TXT = ENrow.Emp_Name;
                objSpayc.col1 = ENrow.FromDate;
                objSpayc.col2 = ENrow.ToDate;
                objSpayc.ID = ENrow.Total_Days;
                objSpayc.col4 = ENrow.WeekEnds;
                objSpayc.col5 = ENrow.Public_Holidays;
                objSpayc.col6 = ENrow.Paydays;
                objspaylst.Add(objSpayc);

            }

            return objspaylst;
        }


 //------------------------------------------CHECK PAYROLL ACTIVITY----------------------------------------------------------

        public int Check_paroll_activity(string ccode, string month, ref bool? status)
        {
            try
            {
                objSPayDatacontext = new SPaycomputeDataContext();
                int check_tbl = objSPayDatacontext.payc_payroll_activity(ccode, month, ref status);



                return check_tbl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }



 //------------------------------------------SAVE PAYROLL ACTIVITY----------------------------------------------------------
        public int Save_payroll_activity(SPaycompute_BO objSpayc, int flag)
        {
            try
            {
                objSPayDatacontext = new SPaycomputeDataContext();
                int insertcodebl = objSPayDatacontext.payc_Insert_payroll_activity(objSpayc.CCD,
                                                                                     objSpayc.id1, 
                                                                                     objSpayc.MNTH, 
                                                                                     objSpayc.TXT, 
                                                                                     objSpayc.id2,
                                                                                     flag  
                                                                                     );


                return insertcodebl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
//--------------------------------------------------------- CHECK MONTH TO UPLOAD MONTHLY SALARY -----------------------------------------------------------
        public int Check_paroll_checkmonth(string ccode, string month, ref bool? status)
        {
            try
            {
                objSPayDatacontext = new SPaycomputeDataContext();
                int check_mnth = objSPayDatacontext.payc_payroll_checkmonth(ccode, month, ref status);



                return check_mnth;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
//--------------------------------------------------------- CHECK MONTH TO UPLOAD MONTHLY SALARY -----------------------------------------------------------
        public int Check_paroll_checkmonthxlup(string ccode, string month, ref bool? status)
        {
            try
            {
                objSPayDatacontext = new SPaycomputeDataContext();
                int check_mnthxlup = objSPayDatacontext.payc_payroll_checkmnthxlup(ccode, month, ref status);



                return check_mnthxlup;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
 //--------------------------------------------------------- CHECK EMP ACTIVE TO LOGIN -----------------------------------------------------------
        public int Check_isactive_tologin(string empid,ref bool? status, int flag)
         {
            try
            {
                objSPayDatacontext = new SPaycomputeDataContext();
                int active = objSPayDatacontext.payc_get_employeeis_activelogin(empid,ref status, flag);



                return active;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

//------------------------------------------------------------ PAYROLL IEMP ADMIN DETAILS ----------------------------------------------------------------
        public SPayc_Collection_BO set_createdbyDDL()
        {
            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();

            foreach (var ENrow in objSPayDatacontext.payc_get_iemppayroll_admin())
            {                
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                objSpayc.id1 = ENrow.ID;
                objSpayc.col11 = ENrow.Admin_ID;
                objSpayc.Created_BY = ENrow.Admin_Email;
                objSpayc.TXT = ENrow.Admin_Name;
               
                objspaylst.Add(objSpayc);
            }
           
            return objspaylst;
        }

        public SPayc_Collection_BO get_projects(string cc,int? flag,string pspnr,string begda,string endda)
        {
            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();

            foreach (var vrow in objSPayDatacontext.payc_get_projects(cc, flag, pspnr, begda, endda))
            {
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                objSpayc.id1 = vrow.PSPNR;
                objSpayc.col11 = vrow.POST1;
                objSpayc.TXT  = vrow.txt;
                objSpayc.begda = vrow.Start_Date;
                objSpayc.endda = vrow.End_Date;
                objSpayc.begda1 = vrow.Created_On;
                objSpayc.endda1 = vrow.Updated_On;
                objSpayc.col15 = vrow.pjct;
                objSpayc.col16 = vrow.actywbs;
                objSpayc.col17 = vrow.Created_By;
                objSpayc.col18 = vrow.Updated_By;
                objspaylst.Add(objSpayc);
            }

            return objspaylst;
        }

        public SPayc_Collection_BO load_prjctwbsact(string cc, int? flag)
        {
            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();

            foreach (var vrow in objSPayDatacontext.payc_load_projectswbs(cc, flag))
            {
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                objSpayc.id1 = vrow.PSPNR;
                objSpayc.col11 = vrow.PSPNRTXT;
                objspaylst.Add(objSpayc);
            }

            return objspaylst;
        }

        public void chk_projects(string cc, int? id, int? flag, ref int? pid,ref DateTime? bdate, ref DateTime? endda)
        {
            try
            {

             objSPayDatacontext = new SPaycomputeDataContext();
             int active = objSPayDatacontext.payc_check_projects(cc,id,flag,ref pid,ref bdate,ref endda);
             //return active;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

           
        

        public int update_projectdtls(SPaycompute_BO objSpayc, ref bool? status)
        {
            try
            {
                objSPayDatacontext = new SPaycomputeDataContext();
                int insertcodebl = objSPayDatacontext.payc_update_projectdtls(objSpayc.CCD,
                                                                                     objSpayc.begda
                                                                                     ,objSpayc.endda
                                                                                     ,objSpayc.ID
                                                                                     ,objSpayc.col1
                                                                                     ,objSpayc.col2
                                                                                     ,objSpayc.id2
                                                                                     , objSpayc.col13
                                                                                     , objSpayc.col14
                                                                                     ,ref status
                                                                                     );


                return insertcodebl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }


        public SPayc_Collection_BO get_compomapping(string company_code, int flag)
        {
            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();

            foreach (var ENrow in objSPayDatacontext.payc_get_salary_compo_mapping(company_code, flag))
            {
                SPaycompute_BO objSpayc = new SPaycompute_BO();

                objSpayc.ID = ENrow.ID;
                objSpayc.TXT = ENrow.comp;
                objSpayc.begda = ENrow.CreatedOn;
                objSpayc.begda1 = ENrow.Updated_On;
                objSpayc.col4 = ENrow.Salary_Component;
                objSpayc.id5 = ENrow.Component_Type;
                objSpayc.col1 = ENrow.typtxt;
                objspaylst.Add(objSpayc);

            }

            return objspaylst;
        }

        public int map_salry(SPaycompute_BO objSpayc,int? flag, ref bool? status)
        {
            try
            {
                objSPayDatacontext = new SPaycomputeDataContext();
                int insertcodebl = objSPayDatacontext.payc_salary_compo_mapping(
                                                                                       objSpayc.CCD,
                                                                                       objSpayc.id3,
                                                                                       objSpayc.id1,
                                                                                       objSpayc.ID,
                                                                                      flag,
                                                                                       ref status);




                return insertcodebl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }


        public int Check_comp_salmap(string ccode, int? flag, ref bool? status)
        {
            try
            {
                objSPayDatacontext = new SPaycomputeDataContext();
                int check_map = objSPayDatacontext.payc_check_comp_salrymap(ccode, flag, ref status);



                return check_map;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }
}
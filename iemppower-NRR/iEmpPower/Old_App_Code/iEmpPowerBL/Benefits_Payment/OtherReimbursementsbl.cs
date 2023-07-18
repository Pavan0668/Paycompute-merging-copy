using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;


namespace iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment
{
    public class OtherReimbursementsbl
    {

        OtherReimbursementsdalDataContext objPRDataContext = new OtherReimbursementsdalDataContext();
        OtherReimbursementCollectionbo PRBo = new OtherReimbursementCollectionbo();


        public OtherReimbursementCollectionbo Load_ExchangeRate(string ExpCurrency, string ReCurrency)
        {
            OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
            OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();
            foreach (var vRow in objcontext.sp_load_ExchangeType(ExpCurrency, ReCurrency))
            {
                OtherReimbursementsbo objBo = new OtherReimbursementsbo();

                objBo.UKURS = vRow.UKURS;
                objList.Add(objBo);
            }
            objcontext.Dispose();
            return objList;
        }

        public void Create_IExpense(OtherReimbursementsbo OtherReBO, ref int? IEXpType_ID, ref bool? IexpStatus)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();
                objcontext.sp_Create_Iexp(0, OtherReBO.PROJID, OtherReBO.TASK, OtherReBO.PURPOSE, OtherReBO.RCURR, OtherReBO.CREATED_ON,
                    OtherReBO.CREATED_BY, OtherReBO.APPROVED_ON1, OtherReBO.APPROVED_BY1, OtherReBO.REMARKS1,
                    OtherReBO.APPROVED_ON2, OtherReBO.APPROVED_BY2, OtherReBO.REMARKS2,
                    OtherReBO.APPROVED_ON3, OtherReBO.APPROVED_BY3, OtherReBO.REMARKS3,
                    OtherReBO.APPROVED_ON4, OtherReBO.APPROVED_BY4, OtherReBO.REMARKS4,
                    OtherReBO.APPROVED_ON5, OtherReBO.APPROVED_BY5, OtherReBO.REMARKS5,
                    OtherReBO.APPROVED_ON6, OtherReBO.APPROVED_BY6, OtherReBO.REMARKS6,
                    OtherReBO.APPROVED_ON7, OtherReBO.APPROVED_BY7, OtherReBO.REMARKS7,
                    OtherReBO.APPROVED_ON8, OtherReBO.APPROVED_BY8, OtherReBO.REMARKS8,
                    OtherReBO.APPROVED_ON9, OtherReBO.APPROVED_BY9, OtherReBO.REMARKS9,
                    OtherReBO.STATUS, OtherReBO.TOTAL_AMOUNT, ref IEXpType_ID, ref IexpStatus);

                objcontext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }

        }

        public void Save_IExpense(OtherReimbursementsbo OtherReBO, ref int? IEXpType_ID, ref bool? IexpStatus)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();
                objcontext.sp_Save_Iexp(OtherReBO.IEXP_ID, OtherReBO.PROJID, OtherReBO.TASK, OtherReBO.PURPOSE, OtherReBO.RCURR, OtherReBO.CREATED_ON,
                    OtherReBO.CREATED_BY, OtherReBO.APPROVED_ON1, OtherReBO.APPROVED_BY1, OtherReBO.REMARKS1,
                    OtherReBO.APPROVED_ON2, OtherReBO.APPROVED_BY2, OtherReBO.REMARKS2,
                    OtherReBO.APPROVED_ON3, OtherReBO.APPROVED_BY3, OtherReBO.REMARKS3,
                    OtherReBO.APPROVED_ON4, OtherReBO.APPROVED_BY4, OtherReBO.REMARKS4,
                    OtherReBO.APPROVED_ON5, OtherReBO.APPROVED_BY5, OtherReBO.REMARKS5,
                    OtherReBO.APPROVED_ON6, OtherReBO.APPROVED_BY6, OtherReBO.REMARKS6,
                    OtherReBO.APPROVED_ON7, OtherReBO.APPROVED_BY7, OtherReBO.REMARKS7,
                    OtherReBO.APPROVED_ON8, OtherReBO.APPROVED_BY8, OtherReBO.REMARKS8,
                    OtherReBO.APPROVED_ON9, OtherReBO.APPROVED_BY9, OtherReBO.REMARKS9,
                    OtherReBO.STATUS, OtherReBO.TOTAL_AMOUNT, ref IEXpType_ID, ref IexpStatus);

                objcontext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }

        }

        public void IexpReq_fivalUpdate(OtherReimbursementsbo objBo)
        {
            try
            {
                OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
                objDataContext.sp_Iexp_req_fiadvalUpdate(objBo.ID, objBo.IEXP_ID, objBo.RECEIPT_FILE, objBo.RECEIPT_FID, objBo.RECEIPT_FPATH, objBo.CREATED_BY);
                objDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }


        //public void Update_IExpense_Approver(int IEXpType_ID)
        // {
        //     try
        //     {
        //         OtherReimbursementsbo OtherReBO = new OtherReimbursementsbo();
        //         OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
        //         OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();
        //         objcontext.sp_Update_Iexp_Approver(0, OtherReBO.PROJID, OtherReBO.TASK, OtherReBO.PURPOSE, OtherReBO.RCURR, OtherReBO.CREATED_ON,
        //             OtherReBO.CREATED_BY, OtherReBO.APPROVED_ON1, OtherReBO.APPROVED_BY1, OtherReBO.REMARKS1,
        //             OtherReBO.APPROVED_ON2, OtherReBO.APPROVED_BY2, OtherReBO.REMARKS2,
        //             OtherReBO.APPROVED_ON3, OtherReBO.APPROVED_BY3, OtherReBO.REMARKS3,
        //             OtherReBO.APPROVED_ON4, OtherReBO.APPROVED_BY4, OtherReBO.REMARKS4,
        //             OtherReBO.APPROVED_ON5, OtherReBO.APPROVED_BY5, OtherReBO.REMARKS5,
        //             OtherReBO.APPROVED_ON6, OtherReBO.APPROVED_BY6, OtherReBO.REMARKS6,
        //             OtherReBO.APPROVED_ON7, OtherReBO.APPROVED_BY7, OtherReBO.REMARKS7, 
        //             OtherReBO.STATUS);

        //         objcontext.Dispose();
        //     }   
        //     catch (Exception Ex)
        //     { throw Ex; }

        // }

        public void Create_IExpTypes_Add(OtherReimbursementsbo OtherReBO, ref bool? IexpStatus)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();

                objcontext.sp_IexpTypes_Add(OtherReBO.IEXP_TYPID, OtherReBO.IEXP_ID, OtherReBO.ID, OtherReBO.EXP_TYPE, OtherReBO.S_DATE, OtherReBO.NO_DAYS, OtherReBO.EXPT_AMT, OtherReBO.EXPT_CURR,
                    OtherReBO.EXC_RATE, OtherReBO.RE_AMT, OtherReBO.JUSTIFY, OtherReBO.RECEIPT_FILE, OtherReBO.RECEIPT_FID, OtherReBO.RECEIPT_FPATH, ref IexpStatus);

                objcontext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public void IexpReq_fivalUpdatePdate(OtherReimbursementsbo objBo)
        {
            try
            {
                OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
                objDataContext.sp_Iexp_req_fiadvalUpdatePdate(objBo.IEXP_ID, objBo.P_DATE);
                objDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }



        public List<OtherReimbursementsbo> Load_ExpenseDetails(string APPROVER_NO, string EmployeeName)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexpense(APPROVER_NO, EmployeeName))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;
                iexpboObj.POST1 = vRow.POST1;

                iexpboObj.ENTITY = vRow.entity;
                iexpboObj.P_DATE = vRow.P_DATE;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_ExpenseDetails_AllCurrentLastmonth(string APPROVER_NO, string EmployeeName, string month)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexpense_month(APPROVER_NO, EmployeeName, month))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;
                iexpboObj.POST1 = vRow.POST1;

                iexpboObj.ENTITY = vRow.entity;
                iexpboObj.P_DATE = vRow.P_DATE;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }


        public List<OtherReimbursementsbo> Load_ExpenseDetails_MC(string APPROVER_NO, string EmployeeName)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexpense_MC(APPROVER_NO, EmployeeName))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;
                iexpboObj.POST1 = vRow.POST1;

                iexpboObj.ENTITY = vRow.entity;
                iexpboObj.P_DATE = vRow.P_DATE;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_ExpenseDetails_MC_AllCurrentLastmonth(string APPROVER_NO, string EmployeeName, string month)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexpense_MC_month(APPROVER_NO, EmployeeName, month))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;
                iexpboObj.POST1 = vRow.POST1;

                iexpboObj.ENTITY = vRow.entity;
                iexpboObj.P_DATE = vRow.P_DATE;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_IEXPDetails(int IEXP_ID)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexp_Types(IEXP_ID))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();


                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.EXP_TYPE = vRow.EXP_TYPE;
                iexpboObj.ID = vRow.ID;
                iexpboObj.S_DATE = vRow.S_DATE;
                iexpboObj.NO_DAYS = vRow.NO_DAYS;
                iexpboObj.EXPT_AMT = vRow.EXPT_AMT;
                iexpboObj.EXPT_CURR = vRow.EXPT_CURR;
                iexpboObj.EXC_RATE = vRow.EXC_RATE;
                iexpboObj.RE_AMT = vRow.RE_AMT;
                iexpboObj.JUSTIFY = vRow.JUSTIFY;
                iexpboObj.RECEIPT_FILE = vRow.RECEIPT_FILE;
                iexpboObj.RECEIPT_FID = vRow.RECEIPT_FID;
                iexpboObj.RECEIPT_FPATH = vRow.RECEIPT_FPATH;

                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;


                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.APPROVED_ON8 = vRow.APPROVED_ON8;
                iexpboObj.APPROVED_BY8 = vRow.APPROVED_BY8;
                iexpboObj.REMARKS8 = vRow.REMARKS8;

                iexpboObj.LGTXT = vRow.LGTXT;
                iexpboObj.EXP_TYPE_TEXT = vRow.LGTXT;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.ENTITY = vRow.entity;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.PURPOSE = vRow.PURPOSE;

                iexpboList.Add(iexpboObj);

            }
            return iexpboList;
        }





        public List<OtherReimbursementsbo> Load_SavedClaimDetails(int IEXP_ID, ref decimal? CalcReAmt, ref string reamtcurr)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_SavedExpTypes(IEXP_ID, ref CalcReAmt, ref reamtcurr))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();


                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.EXP_TYPE = vRow.EXP_TYPE;
                iexpboObj.ID = vRow.ID;
                iexpboObj.S_DATE = vRow.S_DATE;
                iexpboObj.NO_DAYS = vRow.NO_DAYS;
                iexpboObj.EXPT_AMT = vRow.EXPT_AMT;
                iexpboObj.EXPT_CURR = vRow.EXPT_CURR;
                iexpboObj.EXC_RATE = vRow.EXC_RATE;
                iexpboObj.RE_AMT = vRow.RE_AMT;
                iexpboObj.JUSTIFY = vRow.JUSTIFY;
                iexpboObj.RECEIPT_FILE = vRow.RECEIPT_FILE;
                iexpboObj.RECEIPT_FID = vRow.RECEIPT_FID;
                iexpboObj.RECEIPT_FPATH = vRow.RECEIPT_FPATH;

                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;


                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.APPROVED_ON8 = vRow.APPROVED_ON8;
                iexpboObj.APPROVED_BY8 = vRow.APPROVED_BY8;
                iexpboObj.REMARKS8 = vRow.REMARKS8;

                iexpboObj.LGTXT = vRow.LGTXT;
                iexpboObj.EXP_TYPE_TEXT = vRow.LGTXT;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.ENTITY = vRow.entity;

                iexpboList.Add(iexpboObj);

            }
            return iexpboList;
        }






        public List<OtherReimbursementsbo> Load_IexpenseStatusDetails(int IEXP_ID)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_IexpenseStatusDetails(IEXP_ID))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.APPROVED_BY1N = vRow.ENAME1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;


                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.APPROVED_BY2N = vRow.ENAME2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.APPROVED_BY3N = vRow.ENAME3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.APPROVED_BY4N = vRow.ENAME4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.APPROVED_BY5N = vRow.ENAME5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.APPROVED_BY6N = vRow.ENAME6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.APPROVED_BY7N = vRow.ENAME7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.APPROVED_ON8 = vRow.APPROVED_ON8;
                iexpboObj.APPROVED_BY8 = vRow.APPROVED_BY8;

                iexpboObj.REMARKS8 = vRow.REMARKS8;

                iexpboObj.APPROVED_ON9 = vRow.APPROVED_ON9;
                iexpboObj.APPROVED_BY9 = vRow.APPROVED_BY9;

                iexpboObj.REMARKS9 = vRow.REMARKS9;

                iexpboObj.STATUS = vRow.STATUS;

                iexpboList.Add(iexpboObj);

            }
            return iexpboList;
        }

        public void Update_IExpense_Status(OtherReimbursementsbo Iexpbo, ref bool? Status)
        {
            try
            {
                OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();

                objDataContext.sp_update_Iexpense_Status(Iexpbo.IEXP_ID, Iexpbo.APPROVED_BY1, Iexpbo.REMARKS1, Iexpbo.STATUS, ref Status);
                objDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public List<OtherReimbursementsbo> Load_ExpenseDetails(string APPROVER_NO)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexpense_View(APPROVER_NO))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.POST1 = vRow.POST1;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;


                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }


        public List<OtherReimbursementsbo> Load_ExpenseDetails_AllCurrentLastmonth(string APPROVER_NO, string month)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexpense_View_month(APPROVER_NO, month))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.POST1 = vRow.POST1;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;


                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_ExpenseDetails_forfinance(string APPROVER_NO)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_AllIexpense_View_forFinance(APPROVER_NO))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.POST1 = vRow.POST1;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.ENAME = vRow.NAME;
                iexpboObj.CREATED_BY = vRow.CREATED_BY;

                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }


        public List<OtherReimbursementsbo> Get_IexpensePendingMnrAppDetails(string Perner)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_get_Iexpense_PendingMngrApp(Perner))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.POST1 = vRow.POST1;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.ENAME = vRow.NAME;
                iexpboObj.CREATED_BY = vRow.CREATED_BY;

                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Get_IexpensePendingFinAppDetails(string Perner)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_get_Iexpense_PendingFinApp(Perner))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.POST1 = vRow.POST1;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.ENAME = vRow.NAME;
                iexpboObj.CREATED_BY = vRow.CREATED_BY;

                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }


        public List<OtherReimbursementsbo> Load_ExpenseDetails_forsaved(string APPROVER_NO)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexpense_View_forsaved(APPROVER_NO))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.POST1 = vRow.POST1;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.PURPOSE = vRow.PURPOSE;

                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_ParticularIexpDetailsforEmployee(string APPROVER_NO, string SelectedType, string textSearch)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_loadParticular_exp_foremp(APPROVER_NO, SelectedType, textSearch))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.POST1 = vRow.POST1;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;
                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_ParticularIexpDetailsforSaved(string Perner, string SelectedType, string textSearch)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_loadParticular_exp_forSaved(Perner, SelectedType, textSearch))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.POST1 = vRow.POST1;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_ParticularAllIexpDetailsforFinance(string APPROVER_NO, string SelectedType, string textSearch)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_loadParticular_Allexp_forFinance(APPROVER_NO, SelectedType, textSearch))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.POST1 = vRow.POST1;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.ENAME = vRow.NAME;
                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_ParticularIexpDetailsforManager(string APPROVER_NO, string SelectedType, string textSearch)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_loadParticular_exp_forManager(APPROVER_NO, SelectedType, textSearch))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;


                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;
                iexpboObj.POST1 = vRow.POST1;

                iexpboObj.ENTITY = vRow.entity;
                iexpboObj.P_DATE = vRow.P_DATE;
                iexpboObj.STATUS = vRow.STATUS;


                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_IexpDetailsforManager(string APPROVER_NO, string SelectedType, string textSearch)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_load_iexp_forManager(APPROVER_NO, SelectedType, textSearch))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;


                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;
                iexpboObj.POST1 = vRow.POST1;

                iexpboObj.ENTITY = vRow.entity;
                iexpboObj.P_DATE = vRow.P_DATE;
                iexpboObj.STATUS = vRow.STATUS;


                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public OtherReimbursementCollectionbo Load_SettlementAmount(string createdby, decimal total, string currency)
        {
            OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
            OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();
            foreach (var vRow in objcontext.sp_load_SettlementAmount(createdby, total, currency))
            {
                OtherReimbursementsbo objBo = new OtherReimbursementsbo();
                objBo.SETTLEAMT = vRow.SettelmentAmount;
                objBo.SETTLECURR = vRow.SettelmentCurrancy;

                objList.Add(objBo);
            }
            objcontext.Dispose();
            return objList;
        }

        public void UpdateSavedReject_ExpItems(OtherReimbursementsbo OtherReBO, ref bool? IexpStatus, string fileupdate)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();

                objcontext.sp_UpdateSaved_Exp_AddItems(OtherReBO.IEXP_ID, OtherReBO.ID, OtherReBO.EXP_TYPE, OtherReBO.S_DATE, OtherReBO.NO_DAYS, OtherReBO.EXPT_AMT, OtherReBO.EXPT_CURR,
                    OtherReBO.EXC_RATE, OtherReBO.RE_AMT, OtherReBO.JUSTIFY, OtherReBO.RECEIPT_FILE, OtherReBO.RECEIPT_FID, OtherReBO.RECEIPT_FPATH, ref IexpStatus, fileupdate.Trim());

                objcontext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public void DeleteFileFromSaveIexp(OtherReimbursementsbo objBo, ref bool? statusf)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();
                objcontext.sp_DeleteFile_Saved_Iexp(objBo.IEXP_ID, objBo.ID, ref statusf);
                objcontext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public void DeleteSaveIexp(OtherReimbursementsbo objBo, ref bool? status1)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();
                objcontext.sp_Delete_Saved_Iexpense(objBo.IEXP_ID, objBo.ID, ref status1);
                objcontext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public void IEXP_ClaimItems(OtherReimbursementsbo objBo, ref bool? status1)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();
                objcontext.sp_IEXP_AddItems(objBo.IEXP_ID, objBo.ID, objBo.EXP_TYPE, objBo.S_DATE, objBo.NO_DAYS
                    , objBo.EXPT_AMT, objBo.EXPT_CURR, objBo.EXC_RATE, objBo.RE_AMT, objBo.JUSTIFY, objBo.RECEIPT_FILE, objBo.RECEIPT_FID
                    , objBo.RECEIPT_FPATH, ref status1);
                objcontext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public void UpdateCreateIexp(OtherReimbursementsbo objBo, ref bool? status1)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                OtherReimbursementCollectionbo objList = new OtherReimbursementCollectionbo();
                objcontext.sp_CreateSaved_Iexp(objBo.IEXP_ID, objBo.PROJID, objBo.TASK, objBo.PURPOSE, objBo.RCURR, objBo.CREATED_ON,
                     objBo.CREATED_BY, objBo.APPROVED_ON1, objBo.APPROVED_BY1, objBo.REMARKS1,
                        objBo.APPROVED_ON2, objBo.APPROVED_BY2, objBo.REMARKS2,
                        objBo.APPROVED_ON3, objBo.APPROVED_BY3, objBo.REMARKS3,
                        objBo.APPROVED_ON4, objBo.APPROVED_BY4, objBo.REMARKS4,
                        objBo.APPROVED_ON5, objBo.APPROVED_BY5, objBo.REMARKS5,
                        objBo.APPROVED_ON6, objBo.APPROVED_BY6, objBo.REMARKS6,
                        objBo.APPROVED_ON7, objBo.APPROVED_BY7, objBo.REMARKS7,
                        objBo.APPROVED_ON8, objBo.APPROVED_BY8, objBo.REMARKS8,
                        objBo.APPROVED_ON9, objBo.APPROVED_BY9, objBo.REMARKS9,
                        objBo.STATUS, objBo.TotalAmount, ref status1);
                objcontext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }


        public List<OtherReimbursementsbo> Load_IEXPStatusDetails(int IEXPID)
        {
            OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboObjbo = new List<OtherReimbursementsbo>();
            foreach (var vRow in objcontext.sp_Get_IEXPTypesStatus(IEXPID))
            {
                OtherReimbursementsbo objColumnsBo = new OtherReimbursementsbo();


                objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
                objColumnsBo.APPROVED_BY1 = vRow.APPROVED_BY1;
                objColumnsBo.APPROVED_BY1N = vRow.ENAME1;
                objColumnsBo.REMARKS1 = vRow.REMARKS1;
                objColumnsBo.APPROVED_ON2 = vRow.APPROVED_ON2;
                objColumnsBo.APPROVED_BY2 = vRow.APPROVED_BY2;
                objColumnsBo.APPROVED_BY2N = vRow.ENAME2;
                objColumnsBo.REMARKS2 = vRow.REMARKS2;
                objColumnsBo.APPROVED_ON3 = vRow.APPROVED_ON3;
                objColumnsBo.APPROVED_BY3 = vRow.APPROVED_BY3;
                objColumnsBo.APPROVED_BY3N = vRow.ENAME3;
                objColumnsBo.REMARKS3 = vRow.REMARKS3;
                objColumnsBo.APPROVED_ON4 = vRow.APPROVED_ON4;
                objColumnsBo.APPROVED_BY4 = vRow.APPROVED_BY4;
                objColumnsBo.APPROVED_BY4N = vRow.ENAME4;
                objColumnsBo.REMARKS4 = vRow.REMARKS4;
                objColumnsBo.APPROVED_ON5 = vRow.APPROVED_ON5;
                objColumnsBo.APPROVED_BY5 = vRow.APPROVED_BY5;
                objColumnsBo.APPROVED_BY5N = vRow.ENAME5;
                objColumnsBo.REMARKS5 = vRow.REMARKS5;
                objColumnsBo.APPROVED_ON6 = vRow.APPROVED_ON6;
                objColumnsBo.APPROVED_BY6 = vRow.APPROVED_BY6;
                objColumnsBo.APPROVED_BY6N = vRow.ENAME6;
                objColumnsBo.REMARKS6 = vRow.REMARKS6;
                objColumnsBo.APPROVED_ON7 = vRow.APPROVED_ON7;
                objColumnsBo.APPROVED_BY7 = vRow.APPROVED_BY7;
                objColumnsBo.APPROVED_BY7N = vRow.ENAME7;
                objColumnsBo.REMARKS7 = vRow.REMARKS7;
                objColumnsBo.APPROVED_ON8 = vRow.APPROVED_ON8;
                objColumnsBo.APPROVED_BY8 = vRow.APPROVED_BY8;
                objColumnsBo.REMARKS8 = vRow.REMARKS8;
                objColumnsBo.APPROVED_ON9 = vRow.APPROVED_ON9;
                objColumnsBo.APPROVED_BY9 = vRow.APPROVED_BY9;
                objColumnsBo.REMARKS9 = vRow.REMARKS9;
                objColumnsBo.STATUS = vRow.STATUS;
                objColumnsBo.BUKRS = vRow.BUKRS;


                iexpboObjbo.Add(objColumnsBo);
            }
            objcontext.Dispose();
            return iexpboObjbo;
        }


        public void Update_IexpClaim_Approvers(OtherReimbursementsbo objBo, ref bool? Status)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                objcontext.sp_Iexp_claim_UpdateApprovers(objBo.IEXP_ID, objBo.APPROVED_BY1, objBo.APPROVED_BY2, objBo.APPROVED_BY3, objBo.APPROVED_BY4,
                    objBo.APPROVED_BY5, objBo.APPROVED_BY6, objBo.APPROVED_BY7, objBo.APPROVED_BY8, objBo.APPROVED_BY9, ref Status);
                objcontext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }


        public void IexpenseReq_fivalDelete(OtherReimbursementsbo objBo)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                objcontext.sp_iexpense_req_fiadvalDelete(objBo.IEXP_ID, objBo.ID, objBo.RECEIPT_FILE, objBo.RECEIPT_FID, objBo.RECEIPT_FPATH, objBo.CREATED_BY);
                objcontext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public List<OtherReimbursementsbo> Load_ExpenseDetails_AllCurrentLastmonth_Rpager(string APPROVER_NO, string month, int Pindex, int Pagesz, ref int? Rcnt)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexpense_View_month_Rpager(APPROVER_NO, month, Pindex, Pagesz, ref Rcnt))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.RwNum = vRow.RowNumber;
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.POST1 = vRow.POST1;
                iexpboObj.STATUS = vRow.STATUS;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;
                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_ExpenseDetails_AllCurrentLastmonth_Rpager(string APPROVER_NO, string EmployeeName, string month, int Pindex, int Pagesz, ref int? Rcnt)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexpense_month_Rpager(APPROVER_NO, EmployeeName, month, Pindex, Pagesz, ref Rcnt))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.RwNum = vRow.RowNumber;
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;
                iexpboObj.POST1 = vRow.POST1;

                iexpboObj.ENTITY = vRow.entity;
                iexpboObj.P_DATE = vRow.P_DATE;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }

        public List<OtherReimbursementsbo> Load_ExpenseDetails_MC_AllCurrentLastmonth_Rpager(string APPROVER_NO, string EmployeeName, string month, int Pindex, int Pagesz, ref int? Rcnt)
        {
            OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();
            List<OtherReimbursementsbo> iexpboList = new List<OtherReimbursementsbo>();
            foreach (var vRow in objDataContext.sp_Get_Iexpense_MC_month_Rpager(APPROVER_NO, EmployeeName, month, Pindex, Pagesz, ref Rcnt))
            {
                OtherReimbursementsbo iexpboObj = new OtherReimbursementsbo();
                iexpboObj.RwNum = vRow.RowNumber;
                iexpboObj.IEXP_ID = vRow.IEXP_ID;
                iexpboObj.PROJID = vRow.PROJ_ID;
                iexpboObj.TASK = vRow.TASK;
                iexpboObj.RCURR = vRow.RCURR;
                iexpboObj.RE_AMT = vRow.total.ToString();
                iexpboObj.CREATED_ON = vRow.CREATED_ON;
                iexpboObj.APPROVED_ON1 = vRow.APPROVED_ON1;
                iexpboObj.APPROVED_BY1 = vRow.APPROVED_BY1;
                iexpboObj.REMARKS1 = vRow.REMARKS1;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboObj.APPROVED_ON2 = vRow.APPROVED_ON2;
                iexpboObj.APPROVED_BY2 = vRow.APPROVED_BY2;
                iexpboObj.REMARKS2 = vRow.REMARKS2;


                iexpboObj.APPROVED_ON3 = vRow.APPROVED_ON3;
                iexpboObj.APPROVED_BY3 = vRow.APPROVED_BY3;
                iexpboObj.REMARKS3 = vRow.REMARKS3;


                iexpboObj.APPROVED_ON4 = vRow.APPROVED_ON4;
                iexpboObj.APPROVED_BY4 = vRow.APPROVED_BY4;
                iexpboObj.REMARKS4 = vRow.REMARKS4;


                iexpboObj.APPROVED_ON5 = vRow.APPROVED_ON5;
                iexpboObj.APPROVED_BY5 = vRow.APPROVED_BY5;
                iexpboObj.REMARKS5 = vRow.REMARKS5;


                iexpboObj.APPROVED_ON6 = vRow.APPROVED_ON6;
                iexpboObj.APPROVED_BY6 = vRow.APPROVED_BY6;
                iexpboObj.REMARKS6 = vRow.REMARKS6;


                iexpboObj.APPROVED_ON7 = vRow.APPROVED_ON7;
                iexpboObj.APPROVED_BY7 = vRow.APPROVED_BY7;
                iexpboObj.REMARKS7 = vRow.REMARKS7;

                iexpboObj.CREATED_BY = vRow.CREATED_BY;
                iexpboObj.ENAME = vRow.ENAME;
                iexpboObj.POST1 = vRow.POST1;

                iexpboObj.ENTITY = vRow.entity;
                iexpboObj.P_DATE = vRow.P_DATE;
                iexpboObj.PURPOSE = vRow.PURPOSE;
                iexpboObj.STATUS = vRow.STATUS;

                iexpboList.Add(iexpboObj);
            }
            return iexpboList;
        }
    }
}
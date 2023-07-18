using iEmpPower.Old_App_Code.iEmpPowerBO.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.FBP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.FBP
{
    public class Fbp_Claimbl
    {
        FbpClaimsdalDataContext objFBPDataContext = new FbpClaimsdalDataContext();
        FbpClaimscollectionbo FBPBo = new FbpClaimscollectionbo();

        public List<FbpClaimbo> Load_FbpClaimsDetails()
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_LoadClaimDetails())
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.CREATED_BY = vRow.PERNR;
                fbpboObj.ENAME = vRow.ENAME;
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;
                fbpboObj.BETRG = vRow.BETRG;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.STATUS = vRow.STATUS;
                fbpboObj.OVERRIDE_AMT = vRow.OVERRIDE_AMT;
                fbpboObj.REMARKS = vRow.REMARKS;
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;
                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }



        public List<FbpClaimbo> Get_BillDetails(int FBPC_ID)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_GetBillDetails(FBPC_ID))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.ID = vRow.ID;
                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.RECEIPT_FID = vRow.RECEIPT_FID;
                fbpboObj.RECEIPT_FILE = vRow.RECEIPT_FILE;
                fbpboObj.RECEIPT_FPATH = vRow.RECEIPT_FPATH;
                fbpboObj.RELATIONSHIP = vRow.RELATIONSHIP;
                fbpboObj.BILL_AMT = vRow.BILL_AMT;
                fbpboObj.BILL_DATE = vRow.BILL_DATE;
                fbpboObj.BILL_NO = vRow.BILL_NO;



                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }


        public void Update_FbpClaim_Status(FbpClaimbo fbpbo, ref bool? Status)
        {
            try
            {
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();

                objDataContext.usp_Fbp_update_fbp_Status(fbpbo.FBPC_IC, fbpbo.OVERRIDE_AMT, fbpbo.REMARKS, fbpbo.STATUS, ref Status);
                objDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }


        public void Create_FbpClaims_Header(FbpClaimbo fbpbo, string type, ref int? FBP_ID, ref bool? FbpStatus)
        {
            try
            {
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();

                objDataContext.usp_Fbp_CreateClaims_Header(fbpbo.FBPC_IC, fbpbo.CREATED_BY, fbpbo.LGART, fbpbo.BETRG, fbpbo.BEGDA, fbpbo.CREATED_ON,
                    fbpbo.STATUS, fbpbo.BILL_NO, fbpbo.BILL_DATE, fbpbo.RELATIONSHIP, fbpbo.BILL_AMT,
                    fbpbo.RECEIPT_FILE, fbpbo.RECEIPT_FID, fbpbo.RECEIPT_FPATH, fbpbo.ID, type.Trim(),
                    ref FBP_ID, ref FbpStatus);

                objDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }

        }

        public void Create_FbpClaims_footer(FbpClaimbo fbpbo, ref bool? FbpStatus)
        {
            try
            {
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();

                objDataContext.usp_Fbp_CreateClaims_Footer(fbpbo.FBPC_IC, fbpbo.BILL_NO, fbpbo.BILL_DATE, fbpbo.RELATIONSHIP, fbpbo.BILL_AMT,
                    fbpbo.RECEIPT_FILE, fbpbo.RECEIPT_FID, fbpbo.RECEIPT_FPATH, ref FbpStatus);

                objDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }

        }


        public List<FbpClaimbo> Load_Saved_fbpclaims(string pernr)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_GetSavedClaims(pernr))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;
                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;
                fbpboObj.BETRG = vRow.BETRG;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.STATUS = vRow.STATUS;
                //fbpboObj.BILL_AMT = vRow.BILL_AMT;
                //fbpboObj.BILL_DATE = vRow.BILL_DATE;
                //fbpboObj.BILL_NO = vRow.BILL_NO;



                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }


        public void Submit_FbpClaims(FbpClaimbo fbpbo, ref bool? Status)
        {

            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();

            objDataContext.usp_Fbp_Submit_Fbpclaims(fbpbo.FBPC_IC, fbpbo.STATUS, fbpbo.LGART, ref Status);
            objDataContext.Dispose();

        }


        public List<FbpClaimbo> Load_Particular_FbpClaimsDetails(string APPROVER_ID, string SelectedType, string textSearch)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_Load_ParticularClaimDetails(APPROVER_ID, SelectedType, textSearch))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.CREATED_BY = vRow.PERNR;
                fbpboObj.ENAME = vRow.ENAME;
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;
                fbpboObj.BETRG = vRow.BETRG;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.STATUS = vRow.STATUS;
                fbpboObj.OVERRIDE_AMT = vRow.OVERRIDE_AMT;
                fbpboObj.REMARKS = vRow.REMARKS;
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;

                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }


        public List<FbpClaimbo> Load_Particular_FbpClaimsDetails_EMP(string EMPLOYEE_ID, string SelectedType, string textSearch)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_Load_ClaimsDetailsEmp(EMPLOYEE_ID, SelectedType, textSearch))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.CREATED_BY = vRow.PERNR;
                fbpboObj.ENAME = vRow.ENAME;
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;
                fbpboObj.BETRG = vRow.BETRG;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.STATUS = vRow.STATUS;
                fbpboObj.OVERRIDE_AMT = vRow.OVERRIDE_AMT;
                fbpboObj.REMARKS = vRow.REMARKS;
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;

                fbpboObj.APPROVEDON = vRow.APPROVED_ON;

                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }

        public FbpClaimscollectionbo GetTotalAmount(string CREATED_BY, string LGART, decimal tamt)
        {
            FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();
            FbpClaimscollectionbo objFbpList = new FbpClaimscollectionbo();
            foreach (var vRow in objcontext.usp_Fbp_GetTotalAmount(CREATED_BY, LGART, tamt))
            {
                FbpClaimbo objBo = new FbpClaimbo();
                objBo.TotalFBPamt = vRow.Column1;
                objFbpList.Add(objBo);
            }
            objcontext.Dispose();
            return objFbpList;
        }




        public List<FbpClaimbo> Load_Particular_FbpSavedClaimsDetails(string APPROVER_ID, string SelectedType, string textSearch)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_Load_ParticularSavedClaimDetails(APPROVER_ID, SelectedType, textSearch))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;
                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;
                fbpboObj.BETRG = vRow.BETRG;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.STATUS = vRow.STATUS;
                //fbpboObj.BILL_AMT = vRow.BILL_AMT;
                //fbpboObj.BILL_DATE = vRow.BILL_DATE;
                //fbpboObj.BILL_NO = vRow.BILL_NO;



                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }



        // FbpClaimsdalDataContext objFBPDataContext = new FbpClaimsdalDataContext();
        FbpClaimscollectionbo objFbpList = new FbpClaimscollectionbo();

        public FbpClaimscollectionbo GetPlans(string sEmployeeId)
        {
            FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();
            FbpClaimscollectionbo objFbpList = new FbpClaimscollectionbo();
            foreach (var vRow in objcontext.usp_Fbp_Get_Plans(sEmployeeId))
            {
                FbpClaimbo objBo = new FbpClaimbo();
                DateTime dt = (DateTime)vRow.BEGDA;
                objBo.PLAN = dt.ToString("dd-MMM-yyyy");
                DateTime dt1 = (DateTime)vRow.ENDDA;
                objBo.EXIT = dt1.ToString("dd-MMM-yyyy");
                objFbpList.Add(objBo);
            }
            objcontext.Dispose();
            return objFbpList;
        }

        public FbpClaimscollectionbo GetBasketTotal(string sEmployeeId)
        {
            FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();
            FbpClaimscollectionbo objFbpList = new FbpClaimscollectionbo();
            foreach (var vRow in objcontext.usp_Fbp_Get_BasketTotal(sEmployeeId))
            {
                FbpClaimbo objBo = new FbpClaimbo();
                objBo.BASKETTOTAL = vRow.AN_FBP;
                objFbpList.Add(objBo);
            }
            objcontext.Dispose();
            return objFbpList;
        }

        public List<FbpClaimbo> GetHeadsofAllowances(string sEmployeeId)
        {
            FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> requisitionboList = new List<FbpClaimbo>();
            foreach (var vRow in objcontext.usp_Fbp_Get_HeadsofAllowances(sEmployeeId))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.ALLOWANCEID = vRow.LGART;
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;
                fbpboObj.AMOUNT = vRow.AMUNT.Replace(",", "");
                fbpboObj.MONTHLY = vRow.MONTHLY.ToString();
                fbpboObj.ANNUAL = vRow.ANNUAL.Replace(",", "");
                requisitionboList.Add(fbpboObj);
            }
            return requisitionboList;
        }

        public void createFbpDeclarebl(FbpClaimbo objBo, ref bool? SaveStatus)
        {
            try
            {
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();

                objDataContext.usp_Fbp_Create_FbpDeclare(objBo.PERNR, objBo.DATE
                , objBo.AA_AMT01, objBo.AA_AMT02, objBo.AA_AMT03, objBo.AA_AMT04, objBo.AA_AMT05
                , objBo.AA_AMT06, objBo.AA_AMT07, objBo.AA_AMT08, objBo.AA_AMT09, objBo.AA_AMT10, objBo.AA_AMT11, objBo.AA_AMT12, ref SaveStatus);

                objDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public FbpClaimscollectionbo GetLastUpdatedDate(string sEmployeeId)
        {
            FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();
            FbpClaimscollectionbo objFbpList = new FbpClaimscollectionbo();
            foreach (var vRow in objcontext.usp_Fbp_Get_LastUpdatedDate(sEmployeeId))
            {
                FbpClaimbo objBo = new FbpClaimbo();
                DateTime dt = (DateTime)vRow.CREATED_ON;
                objBo.LASTUPDATEDDATE = dt.ToString("dd-MMM-yyyy");
                objFbpList.Add(objBo);
            }
            objcontext.Dispose();
            return objFbpList;
        }

        public List<FbpClaimbo> GetFBPDeclared(string sEmployeeId)
        {
            FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> requisitionboList = new List<FbpClaimbo>();
            foreach (var vRow in objcontext.usp_Fbp_Get_GetFBPDeclared(sEmployeeId))
            {
                //fbpbo requisitionboObj = new fbpbo();
                //requisitionboObj.ALLOWANCEID = vRow.LGART;
                //requisitionboObj.ALLOWANCETEXT = vRow.LTEXT;
                //requisitionboObj.AMOUNT = vRow.AMUNT;

                //requisitionboList.Add(requisitionboObj);
            }
            return requisitionboList;
        }

        public List<FbpClaimbo> LoadAllowances(string Empid)
        {
            List<FbpClaimbo> fbboList = new List<FbpClaimbo>();
            FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();
            foreach (var vRow in objcontext.usp_Fbp_LoadAllowances(Empid))
            {
                FbpClaimbo fbpbo = new FbpClaimbo();
                fbpbo.ALLOWANCEID = vRow.LGART;
                fbpbo.ALLOWANCETEXT = vRow.LTEXT;//vRow.LGART +" - "+ vRow.LTEXT;
                fbboList.Add(fbpbo);
            }
            return fbboList;
        }

        public FbpClaimscollectionbo GetLTAMobPurCount(string sEmployeeId, string LGART)
        {
            FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();
            FbpClaimscollectionbo objFbpList = new FbpClaimscollectionbo();
            foreach (var vRow in objcontext.usp_Fbp_Get_LTAMbPur(sEmployeeId, LGART))
            {
                FbpClaimbo objBo = new FbpClaimbo();
                objBo.COUNT = (int)vRow.count;
                objFbpList.Add(objBo);
            }
            objcontext.Dispose();
            return objFbpList;
        }

        public FbpClaimscollectionbo GetClaimTotal(string sEmployeeId, string LGART)
        {
            FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();
            FbpClaimscollectionbo objFbpList = new FbpClaimscollectionbo();
            foreach (var vRow in objcontext.usp_Fbp_Get_ClaimTotal(sEmployeeId, LGART))
            {
                FbpClaimbo objBo = new FbpClaimbo();
                objBo.COUNT = (int)vRow.count;
                objBo.TotalClaimamt = vRow.TotalClaimamt.ToString();
                objFbpList.Add(objBo);
            }
            objcontext.Dispose();
            return objFbpList;
        }



        public List<FbpClaimbo> Load_FbpClaim_Details(string pernr, string lgart)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_GetClaimsPending(pernr, lgart))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;

                fbpboObj.ANNUAL = vRow.ANNUAL;
                fbpboObj.BETRG = vRow.Paid.ToString();
                fbpboObj.BALANCE = vRow.balance.ToString();
                fbpboObj.PENDINGAMT = vRow.claimsPending.ToString();

                fbpboObj.ACCRUED = vRow.ACCRUED.ToString();


                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }

        public List<FbpClaimbo> Load_fbpclaims_History(string pernr)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_ClaimsHistory(pernr))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();

                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;
                fbpboObj.BETRG = vRow.BETRG;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.STATUS = vRow.STATUS;
                fbpboObj.OVERRIDE_AMT = string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? "0.00" : decimal.Parse(vRow.OVERRIDE_AMT).ToString("F");
                fbpboObj.REMARKS = vRow.REMARKS;
                fbpboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;
                // fbpboObj.APPAMT = string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? vRow.BETRG : vRow.OVERRIDE_AMT;
                fbpboObj.APPAMT = vRow.STATUS == "Approved" ? (string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? vRow.BETRG : vRow.OVERRIDE_AMT) : "0.00";
                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }

        public List<FbpClaimbo> Load_fbpclaims_History_AllCurrentLastmonth(string pernr, string month)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_ClaimsHistory_month(pernr, month))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();

                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;
                fbpboObj.BETRG = vRow.BETRG;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.STATUS = vRow.STATUS;
                fbpboObj.OVERRIDE_AMT = string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? "0.00" : decimal.Parse(vRow.OVERRIDE_AMT).ToString("F");
                fbpboObj.REMARKS = vRow.REMARKS;
                fbpboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;
                // fbpboObj.APPAMT = string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? vRow.BETRG : vRow.OVERRIDE_AMT;
                fbpboObj.APPAMT = vRow.STATUS == "Approved" ? (string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? vRow.BETRG : vRow.OVERRIDE_AMT) : "0.00";
                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }


        public List<FbpClaimbo> Load_Particularfbpclaims_History(string pernr, string SelectedType, string textSearch, DateTime createdon)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_ParticularClaimsHistory(pernr, SelectedType, textSearch, createdon))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();

                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;
                fbpboObj.BETRG = vRow.BETRG;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.STATUS = vRow.STATUS;
                fbpboObj.OVERRIDE_AMT = string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? "0.00" : decimal.Parse(vRow.OVERRIDE_AMT).ToString("F");
                fbpboObj.REMARKS = vRow.REMARKS;
                fbpboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;
                // fbpboObj.APPAMT = string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? vRow.BETRG : vRow.OVERRIDE_AMT;
                fbpboObj.APPAMT = vRow.STATUS == "Approved" ? (string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? vRow.BETRG : vRow.OVERRIDE_AMT) : "0.00";

                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }



        //public List<FbpClaimbo> Load_Fbp_Locking(int PageIndex, int PageSize,  ref int? RecordCount) 
        //{
        //    FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
        //    List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
        //    foreach (var vRow in objDataContext.usp_Fbp_LoadLocking(PageIndex, PageSize, ref RecordCount))  
        //    {
        //        FbpClaimbo iexpboObj = new FbpClaimbo();
        //        iexpboObj.PERNR = vRow.PERNR;
        //        iexpboObj.ENAME = vRow.ENAME;
        //        iexpboObj.FBPLOCK = vRow.LOCK;
        //        iexpboObj.SLNO = vRow.RowNumber;


        //        FbpboList.Add(iexpboObj);
        //    }
        //    return FbpboList;
        //}


        public List<FbpClaimbo> Load_Fbp_Locking(string perner)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_LoadLocking(perner))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.PERNR = vRow.PERNR;
                fbpboObj.ENAME = vRow.ENAME;
                fbpboObj.FBPLOCK = vRow.LOCK;
                // iexpboObj.SLNO = vRow.RowNumber;


                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }
        public List<FbpClaimbo> Load_Allfbpclaims()
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_LoadAllClaims())
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();

                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;
                fbpboObj.BETRG = vRow.BETRG;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.STATUS = vRow.STATUS;
                fbpboObj.OVERRIDE_AMT = string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? "0.00" : decimal.Parse(vRow.OVERRIDE_AMT).ToString("F");
                fbpboObj.REMARKS = vRow.REMARKS;
                fbpboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;
                fbpboObj.CREATED_BY = vRow.PERNR;
                fbpboObj.ENAME = vRow.ENAME;
                fbpboObj.APPAMT = vRow.STATUS == "Approved" ? (string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? vRow.BETRG : vRow.OVERRIDE_AMT) : "0.00";
                //string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? vRow.BETRG : vRow.OVERRIDE_AMT;


                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }


        public List<FbpClaimbo> Load_AllfbpDeclarationclaims(string pernr)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_GetDeclarationDetails(pernr))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();

                fbpboObj.AA_AMT01 = vRow.AA_AMT01 == "" || vRow.AA_AMT01 == ".0" ? "0.0" : vRow.AA_AMT01;
                fbpboObj.AA_AMT02 = vRow.AA_AMT02 == "" || vRow.AA_AMT02 == ".0" ? "0.0" : vRow.AA_AMT02;
                fbpboObj.AA_AMT03 = vRow.AA_AMT03 == "" || vRow.AA_AMT03 == ".0" ? "0.0" : vRow.AA_AMT03;
                fbpboObj.AA_AMT04 = vRow.AA_AMT04 == "" || vRow.AA_AMT04 == ".0" ? "0.0" : vRow.AA_AMT04;
                fbpboObj.AA_AMT05 = vRow.AA_AMT05 == "" || vRow.AA_AMT05 == ".0" ? "0.0" : vRow.AA_AMT05;
                fbpboObj.AA_AMT06 = vRow.AA_AMT06 == "" || vRow.AA_AMT06 == ".0" ? "0.0" : vRow.AA_AMT06;
                fbpboObj.AA_AMT07 = vRow.AA_AMT07 == "" || vRow.AA_AMT07 == ".0" ? "0.0" : vRow.AA_AMT07;
                fbpboObj.AA_AMT08 = vRow.AA_AMT08 == "" || vRow.AA_AMT08 == ".0" ? "0.0" : vRow.AA_AMT08;
                fbpboObj.AA_AMT09 = vRow.AA_AMT09 == "" || vRow.AA_AMT09 == ".0" ? "0.0" : vRow.AA_AMT09;
                fbpboObj.AA_AMT10 = vRow.AA_AMT10 == "" || vRow.AA_AMT10 == ".0" ? "0.0" : vRow.AA_AMT10;
                fbpboObj.AA_AMT11 = vRow.AA_AMT11 == "" || vRow.AA_AMT11 == ".0" ? "0.0" : vRow.AA_AMT11;
                fbpboObj.AA_AMT12 = vRow.AA_AMT12 == "" || vRow.AA_AMT12 == ".0" ? "0.0" : vRow.AA_AMT12;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;

                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }



        public List<FbpClaimbo> Load_ParticularfbpclaimsAdmin(string SelectedType, string textSearch, DateTime? createdon, DateTime? createdonto, string type)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_ParticularClaimsAdmin(SelectedType, textSearch, createdon, createdonto, type))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();



                fbpboObj.FBPC_IC = vRow.FBPC_ID;
                fbpboObj.LGART = vRow.LGART;
                fbpboObj.CREATED_ON = vRow.CREATED_ON;
                fbpboObj.BETRG = vRow.BETRG;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.STATUS = vRow.STATUS;
                fbpboObj.OVERRIDE_AMT = string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? "0.00" : decimal.Parse(vRow.OVERRIDE_AMT).ToString("F");
                fbpboObj.REMARKS = vRow.REMARKS;
                fbpboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                fbpboObj.ALLOWANCETEXT = vRow.LTEXT;
                fbpboObj.CREATED_BY = vRow.PERNR;
                fbpboObj.ENAME = vRow.ENAME;
                //fbpboObj.APPAMT = string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? vRow.BETRG : vRow.OVERRIDE_AMT;
                fbpboObj.APPAMT = vRow.STATUS == "Approved" ? (string.IsNullOrEmpty(vRow.OVERRIDE_AMT) ? vRow.BETRG : vRow.OVERRIDE_AMT) : "0.00";



                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }

        public List<FbpClaimbo> Load_AllfbpDeclarationclaims_admin(string pernr, string txtsearch, int filt, string newRec, string frmdt, string todat)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            try
            {
                
                foreach (var vRow in objDataContext.usp_Fbp_GetDeclarationDetails_admin(pernr, txtsearch, filt, newRec, frmdt, todat))
                {
                    FbpClaimbo fbpboObj = new FbpClaimbo();



                    fbpboObj.AA_AMT01 = Convert.ToDecimal(vRow.AA_AMT01 == "" || vRow.AA_AMT01 == ".0" ? "0.0" : vRow.AA_AMT01).ToString("N2");
                    fbpboObj.AA_AMT02 = Convert.ToDecimal(vRow.AA_AMT02 == "" || vRow.AA_AMT02 == ".0" ? "0.0" : vRow.AA_AMT02).ToString("N2");
                    fbpboObj.AA_AMT03 = Convert.ToDecimal(vRow.AA_AMT03 == "" || vRow.AA_AMT03 == ".0" ? "0.0" : vRow.AA_AMT03).ToString("N2");
                    fbpboObj.AA_AMT04 = Convert.ToDecimal(vRow.AA_AMT04 == "" || vRow.AA_AMT04 == ".0" ? "0.0" : vRow.AA_AMT04).ToString("N2");
                    fbpboObj.AA_AMT05 = Convert.ToDecimal(vRow.AA_AMT05 == "" || vRow.AA_AMT05 == ".0" ? "0.0" : vRow.AA_AMT05).ToString("N2");
                    fbpboObj.AA_AMT06 = Convert.ToDecimal(vRow.AA_AMT06 == "" || vRow.AA_AMT06 == ".0" ? "0.0" : vRow.AA_AMT06).ToString("N2");
                    fbpboObj.AA_AMT07 = Convert.ToDecimal(vRow.AA_AMT07 == "" || vRow.AA_AMT07 == ".0" ? "0.0" : vRow.AA_AMT07).ToString("N2");
                    fbpboObj.AA_AMT08 = Convert.ToDecimal(vRow.AA_AMT08 == "" || vRow.AA_AMT08 == ".0" ? "0.0" : vRow.AA_AMT08).ToString("N2");
                    fbpboObj.AA_AMT09 = Convert.ToDecimal(vRow.AA_AMT09 == "" || vRow.AA_AMT09 == ".0" ? "0.0" : vRow.AA_AMT09).ToString("N2");
                    fbpboObj.AA_AMT10 = Convert.ToDecimal(vRow.AA_AMT10 == "" || vRow.AA_AMT10 == ".0" ? "0.0" : vRow.AA_AMT10).ToString("N2");
                    fbpboObj.AA_AMT11 = Convert.ToDecimal(vRow.AA_AMT11 == "" || vRow.AA_AMT11 == ".0" ? "0.0" : vRow.AA_AMT11).ToString("N2");
                    fbpboObj.AA_AMT12 = Convert.ToDecimal(vRow.AA_AMT12 == "" || vRow.AA_AMT12 == ".0" ? "0.0" : vRow.AA_AMT12).ToString("N2");
                    fbpboObj.BEGDA = Convert.ToDateTime(vRow.BEGDA);
                    fbpboObj.CREATED_ON = vRow.CREATED_ON;
                    fbpboObj.PERNR = vRow.PERNR;
                    fbpboObj.ENAME = vRow.ENAME;
                    fbpboObj.ENTITY = vRow.entity;
                    fbpboObj.SAP_ID = vRow.SAP_ID;
                    fbpboObj.DOJ =  vRow.DOJ;
                    fbpboObj.DOL =  vRow.DOL;
                    FbpboList.Add(fbpboObj);
                }
               
            }
            catch (Exception ex)
            {
            }
            return FbpboList;
        }

        public List<FbpClaimbo> Load_IDs_NOTDeclrd(int flg)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_Fbp_check_not_declaredIDs(flg))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.PERNR = vRow.PERNR;
                fbpboObj.ENAME = vRow.ENAME;
                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }

        public List<FbpClaimbo> LoadMobNo(string Empid, int flg)
        {
            List<FbpClaimbo> fbboList = new List<FbpClaimbo>();
            FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();
            foreach (var vRow in objcontext.usp_FBP_get_user_mob_nos(Empid, flg))
            {
                FbpClaimbo fbpbo = new FbpClaimbo();
                //fbpbo.ALLOWANCEID = vRow.USRTY.Trim();
                fbpbo.ALLOWANCETEXT = vRow.USRID.Trim();
                fbboList.Add(fbpbo);
            }
            return fbboList;
        }

        public void AddLTA(FbpClaimbo fbpbo, int flg, ref bool? Status)
        {
            try
            {
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();





                objDataContext.usp_FBP_LTA_Set_records(
                    fbpbo.FID,
                    fbpbo.ID, fbpbo.LINEID,
                    fbpbo.PERNR, fbpbo.JBGDT, fbpbo.JENDT, fbpbo.STPNT,
                    fbpbo.DESTN, fbpbo.MTRVL,
                    fbpbo.CTRVL, fbpbo.TKTNO,
                    fbpbo.KM_TRVLD,
                    fbpbo.AMOUNTLTA,
                    flg
                    , ref Status);
                objDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }



        public void AddLTAHead(FbpClaimbo fbpbo, int flg, ref bool? Status, ref int? LID)
        {
            try
            {
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();



                objDataContext.usp_FBP_LTA_Set_records_head(
                    fbpbo.FID, fbpbo.ID,
                    fbpbo.PERNR, fbpbo.JBGDT, fbpbo.JENDT, fbpbo.STPNT,
                    fbpbo.DESTN, fbpbo.MTRVL,
                    fbpbo.CTRVL, fbpbo.TKTNO,
                    fbpbo.SLFTR
                    , fbpbo.CBPY1
                    , fbpbo.CBPY2
                    , fbpbo.CBPY3
                    , fbpbo.CBPY4
                    , fbpbo.CLYear
                    , fbpbo.CLY1
                    , fbpbo.CLY2
                    , fbpbo.CLY3
                    , fbpbo.CLY4
                    , flg, ref Status, ref LID);
                objDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }



        public void AddLTAReltps(FbpClaimbo fbpbo, int flg, ref bool? Status)
        {
            try
            {
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();





                objDataContext.usp_FBP_LTA_Set_records_Relationships(
                    fbpbo.ID,
                    fbpbo.PERNR,
                    fbpbo.FID,
                    fbpbo.LINEID,
                fbpbo.FAMTX,
                    fbpbo.FCNAM,
                    fbpbo.FGBDT,
                    fbpbo.FASEX,
                    fbpbo.DEPDT,
                    flg
                    , ref Status);
                objDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }



        public FbpClaimscollectionbo Get_FamilyMember_Types()
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            foreach (var vRow in objDataContext.usp_FBP_LTA_get_family_members_type())
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.SUBTY = vRow.SUBTY;
                fbpboObj.STEXT = vRow.STEXT;
                FBPBo.Add(fbpboObj);
            }
            objDataContext.Dispose();
            return FBPBo;
        }



        public List<FbpClaimbo> Load_LTAHead(string PERNR, int LID, int FID, int flg, ref string status)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_FBP_LTA_get_records(PERNR, LID, FID, flg, ref status))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.ID = vRow.ID;
                fbpboObj.PERNR = vRow.PERNR;
                fbpboObj.JBGDT = vRow.JBGDT;
                fbpboObj.JENDT = vRow.JENDT;
                fbpboObj.STPNT = vRow.STPNT;
                fbpboObj.DESTN = vRow.DESTN;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.MTRVL = vRow.MTRVL;
                fbpboObj.CTRVL = vRow.CTRVL;
                fbpboObj.TKTNO = vRow.TKTNO;
                fbpboObj.SLFTR = vRow.SLFTR;
                fbpboObj.CBPY1 = vRow.CBPY1;
                fbpboObj.CBPY2 = vRow.CBPY2;
                fbpboObj.CBPY3 = vRow.CBPY3;
                fbpboObj.CBPY4 = vRow.CBPY4;
                fbpboObj.CLYear = vRow.CLYear;
                fbpboObj.CLY1 = vRow.CLY1;
                fbpboObj.CLY2 = vRow.CLY2;
                fbpboObj.CLY3 = vRow.CLY3;
                fbpboObj.CLY4 = vRow.CLY4;
                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }



        public List<FbpClaimbo> Load_LTArel(string PERNR, int LID, int ID, int flg)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_FBP_LTA_get_records_Relationships(PERNR, LID, ID, flg))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.ID = vRow.ID;
                fbpboObj.PERNR = vRow.PERNR;
                fbpboObj.FAMTX = vRow.FAMTX;
                fbpboObj.FCNAM = vRow.FCNAM;
                fbpboObj.FGBDT = vRow.FGBDT;
                fbpboObj.FASEX = vRow.FASEX;
                fbpboObj.DEPDT = vRow.DEPDT;
                fbpboObj.FAMTX_text = vRow.STEXT;
                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }



        public List<FbpClaimbo> Load_LTATRVL(string PERNR, int LID, int ID, int flg)
        {
            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
            foreach (var vRow in objDataContext.usp_FBP_LTA_get_records_LineItems(PERNR, LID, ID, flg))
            {
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.ID = vRow.ID;
                fbpboObj.PERNR = vRow.PERNR;
                fbpboObj.JBGDT = vRow.JBGDT;
                fbpboObj.JENDT = vRow.JENDT;
                fbpboObj.STPNT = vRow.STPNT;
                fbpboObj.DESTN = vRow.DESTN;
                fbpboObj.BEGDA = vRow.BEGDA;
                fbpboObj.MTRVL = vRow.MTRVL;
                fbpboObj.CTRVL = vRow.CTRVL;
                fbpboObj.TKTNO = vRow.TKTNO;
                fbpboObj.KM_TRVLD = vRow.KM_TRVLD;
                fbpboObj.AMOUNTLTA = vRow.AMOUNT;



                FbpboList.Add(fbpboObj);
            }
            return FbpboList;
        }

        public void SetFBPLock_Bit(FbpClaimbo fbpbo, int flg, ref bool? Mob, ref bool? CC)
        {
            try
            {
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();



                objDataContext.usp_FBP_get_lockDetailsBits(fbpbo.PERNR, fbpbo.ENAME, flg, fbpbo.Mob, fbpbo.CC, ref Mob, ref CC);



                objDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }



        }
    }
}
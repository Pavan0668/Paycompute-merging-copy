using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Services;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using System.Globalization;
using iEmpPower.Old_App_Code.iEmpPowerBO.Training_Event_Management;
using iEmpPower.Old_App_Code.iEmpPowerBL.Training_Event_Management;
using iEmpPower.Old_App_Code.iEmpPowerMaster;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerMaster;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service;

namespace iEmpPower.WebService
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class Service : System.Web.Services.WebService
    {
        [System.Web.Script.Services.ScriptMethod]
        //[WebMethod]
        //public string HelloWorlds()
        //{
        //    return "Hello World";
        //}

        //******************************************** GET COUNRY NAMES *********************************************************
        #region GET COUNRY NAMES

        [WebMethod()]
        public CascadingDropDownNameValue[] GetCountryNames(string knownCategoryValues, string category)
        {
            try
            {
                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();

                var Nationality = objDataContext.sp_master_load_country_nationality();

                List<CascadingDropDownNameValue> CountryNames = new List<CascadingDropDownNameValue>();
                foreach (var CountryItems in Nationality)
                { CountryNames.Add(new CascadingDropDownNameValue(CountryItems.LANDX, CountryItems.LAND1)); }
                return CountryNames.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        //******************************************** GET STATE NAME FROM COUNTRY ********************************
        #region GET STATE NAME FROM COUNTRY
        [WebMethod()]
        public CascadingDropDownNameValue[] GetStateName(string knownCategoryValues, string category)
        {
            try
            {

                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();
                StringDictionary CountryID = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
                masterbo mBo = new masterbo();
                mBo.LAND1 = CountryID["Country"].ToString();

                var VarStateNames = objDataContext.sp_master_load_slctd_country_states(mBo.LAND1);
                List<CascadingDropDownNameValue> StateNames = new List<CascadingDropDownNameValue>();
                foreach (var StateItems in VarStateNames)
                { StateNames.Add(new CascadingDropDownNameValue(StateItems.BEZEI, StateItems.BLAND)); }
                return StateNames.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        //******************************************** GET LEAVE TYPES ********************************
        #region GET LEAVE TYPES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetLeaveType(string knownCategoryValues, string category)
        {
            try
            {
                StringDictionary TYPELEAVE = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
                masterbo mBo = new masterbo();
                string typ = TYPELEAVE["LeaveReqTyp"].ToString();

                //var LT = iEmpPowerMaster_Load.masterbl.Load_Attendence_abs_Types(User.Identity.Name.ToString()).Where(t => t.AWART.Contains(LevTypeFilter));
                var LT = from i in iEmpPowerMaster_Load.masterbl.Load_Attendence_abs_Types_Leave(typ, User.Identity.Name.ToString(), Session["CompCode"].ToString())
                         //where !LevTypeFilter.Contains(i.AWART)
                         select i;
                List<CascadingDropDownNameValue> LeaveTypeList = new List<CascadingDropDownNameValue>();

                foreach (var LeaveType in LT)
                { LeaveTypeList.Add(new CascadingDropDownNameValue(LeaveType.ATEXT, LeaveType.AWART)); }

                return LeaveTypeList.ToArray();

                //-------------------------------------------------------------------------------------------------------
                //masterdalDataContext objDataContext = new masterdalDataContext();
                //mastercollectionbo objList = new mastercollectionbo();
                //StringDictionary Veh_Id = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
                //masterbo mBo1 = new masterbo();
                //mBo1.KZPMF = Veh_Id["Vehicle"].ToString();

                //var V_sub = objDataContext.sp_master_load_att_abs_Leave(mBo1.KZPMF, User.Identity.Name.ToString());
                //List<CascadingDropDownNameValue> Vec_sub = new List<CascadingDropDownNameValue>();
                //foreach (var v_s in V_sub)
                //{ Vec_sub.Add(new CascadingDropDownNameValue(v_s.ATEXT, v_s.AWART)); }
                //return Vec_sub.ToArray();
            }
            catch (Exception Ex)
            { string a = Ex.Message; return null; }
        }
        #endregion
        //******************************************** GET NATIONALITY NAMES *********************************************************
        #region GET NATIONALITY NAMES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetNationalityNames(string knownCategoryValues, string category)
        {
            try
            {
                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();

                var Nationality = objDataContext.sp_master_load_country_nationality();

                List<CascadingDropDownNameValue> CountryNames = new List<CascadingDropDownNameValue>();
                foreach (var CountryItems in Nationality)
                { CountryNames.Add(new CascadingDropDownNameValue(CountryItems.NATIO, CountryItems.LAND1)); }
                return CountryNames.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion


        //******************************************** GET COMMUNICATION TYPE ********************************
        #region GET COMMUNICATION TYPE
        [WebMethod()]
        public CascadingDropDownNameValue[] GetCommType(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> CommType = new List<CascadingDropDownNameValue>();
                using (DataTable Dt = new DataTable())
                {
                    Dt.Columns.Add("USTXT", typeof(string));
                    Dt.Columns.Add("USRTY", typeof(string));

                    //Dt.Rows.Add("0002", "Building Number");
                    //Dt.Rows.Add("0003", "Room Number");
                    //Dt.Rows.Add("0004", "Extention");
                    //Dt.Rows.Add("0010", "Email ID");
                    Dt.Rows.Add("CELL", "Emergency Contact Mobile Number");
                    Dt.Rows.Add("MAIL", "Alternative Email ID");
                    Dt.Rows.Add("MPHN", "Mobile Number");

                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        CommType.Add(new CascadingDropDownNameValue(Dt.Rows[i]["USRTY"].ToString(), Dt.Rows[i]["USTXT"].ToString()));
                    }
                }
                return CommType.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion


        //******************************************** GET COMMUNICATION TYPE ********************************
        #region GET LEAVE REQUEST TYPE
        [WebMethod()]
        public CascadingDropDownNameValue[] GetLeaveReqType(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> LeaveReqType = new List<CascadingDropDownNameValue>();
                using (DataTable Dt = new DataTable())
                {
                    Dt.Columns.Add("LTYPE", typeof(string));
                    Dt.Columns.Add("LID", typeof(string));


                    //Dt.Rows.Add("0002", "Building Number");
                    //Dt.Rows.Add("0003", "Room Number");
                    //Dt.Rows.Add("0004", "Extention");
                    //Dt.Rows.Add("0010", "Email ID");

                    Dt.Rows.Add("1", "Leave");
                    Dt.Rows.Add("2", "Attendance");

                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        LeaveReqType.Add(new CascadingDropDownNameValue(Dt.Rows[i]["LID"].ToString(), Dt.Rows[i]["LTYPE"].ToString()));
                    }
                }
                return LeaveReqType.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion


        //******************************************** GET FAMILY MEMBER TYPES ********************************
        #region GET FAMILY MEMBER TYPES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetFamilyMemTypes(string knownCategoryValues, string category)
        {
            try
            {
                pifamilymembersbl objFamilyBl = new pifamilymembersbl();
                List<CascadingDropDownNameValue> FamilyTypes = new List<CascadingDropDownNameValue>();
                pifamilymemberscollectionbo objPIAddBoLst = objFamilyBl.Get_FamilyMember_Types();
                foreach (var FamTypes in objPIAddBoLst)
                {
                    { FamilyTypes.Add(new CascadingDropDownNameValue(FamTypes.STEXT, FamTypes.SUBTY)); }
                }
                return FamilyTypes.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        //******************************************** GET PERSONAL ID TYPES ********************************

        #region GET PERSONAL TYPES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetPersonalIDTypes(string knownCategoryValues, string category)
        {
            try
            {
                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();
                List<CascadingDropDownNameValue> AddressTypes = new List<CascadingDropDownNameValue>();
                masterbo objBo = new masterbo();
                var PerID = objDataContext.sp_master_load_personal_id_type();
                foreach (var vRow in PerID)
                { AddressTypes.Add(new CascadingDropDownNameValue(vRow.ICTXT, vRow.ICTYP)); }

                return AddressTypes.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        //******************************************** GET ADDRESS TYPES ********************************
        #region GET ADDRESS TYPES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetAddressTypes(string knownCategoryValues, string category)
        {
            try
            {

                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();
                List<CascadingDropDownNameValue> AddressType = new List<CascadingDropDownNameValue>();
                var AddrType = objDataContext.sp_master_load_address_type();
                foreach (var AddTyp in AddrType)
                {
                    AddressType.Add(new CascadingDropDownNameValue(AddTyp.STEXT, AddTyp.SUBTY));
                }
                return AddressType.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        //******************************************** GET VEHICLE CATEGORY ********************************
        #region GET VEHICLE CATEGORY - VEHICLE REQUISITION
        [WebMethod()]
        public CascadingDropDownNameValue[] GetVehCategoryVehReq(string knownCategoryValues, string category)
        {
            try
            {
                StringDictionary VehTypeID = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
                travelrequestbl objtravelrequestbl = new travelrequestbl();
                List<CascadingDropDownNameValue> VehCategory = new List<CascadingDropDownNameValue>();
                var VehCat = objtravelrequestbl.GetVehCategory(User.Identity.Name, VehTypeID["VehType"]);
                foreach (var VehC in VehCat)
                {
                    VehCategory.Add(new CascadingDropDownNameValue(VehC.MEDIA_OF_CATEGORY_TEXT25, VehC.MEDIA_OF_CATEGORY_PKWKL));
                }
                return VehCategory.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        //******************************************** GET PLACE NAMES ******************************************************************
        #region GET PLACE NAMES
        [WebMethod()]
        public CascadingDropDownNameValue[] GePlaceNames(string knownCategoryValues, string category, string contextKey)
        {
            try
            {
                //StringDictionary ck = CascadingDropDown.ParseKnownCategoryValuesString(contextKey);
                travelrequestbl objtravelrequestbl = new travelrequestbl();
                var PlaceNames = objtravelrequestbl.GetRegionName();
                List<CascadingDropDownNameValue> FrmToPlace = new List<CascadingDropDownNameValue>();

                foreach (var Plc in PlaceNames)
                {
                    FrmToPlace.Add(new CascadingDropDownNameValue(Plc.REGION_TEXT25_FROM, Plc.REGION_RGION_FROM));
                }
                return FrmToPlace.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        //******************************************** GET EXPENSE TYPE ******************************************************************
        #region GET EXPENSE TYPES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetExpenseType(string knownCategoryValues, string category, string contextKey)
        {
            try
            {
                StringDictionary ck = CascadingDropDown.ParseKnownCategoryValuesString(contextKey);
                travelrequestbl objtravelrequestbl = new travelrequestbl();
                List<CascadingDropDownNameValue> ExpType = new List<CascadingDropDownNameValue>();
                var ExTyList = objtravelrequestbl.GetExpenseTypeAll(contextKey);
                foreach (var ExT in ExTyList)
                {
                    ExpType.Add(new CascadingDropDownNameValue(ExT.SPTXT, ExT.SPKZL));
                }
                return ExpType.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        //******************************************** GET EXPENSE CLAIM AMOUNT ******************************************************************

        //#region GET EXPENSE CLAIM AMOUNT
        //[WebMethod()]
        //public string GetExpClaimAmount(string FrmDt, string ToDt, string FrmPlace, string ToPlace, string ExpType, string TrvlType)
        //{
        //    try
        //    {
        //        Expensebl objBl = new Expensebl();
        //        //return  objBl.Get_Amount_ForClaimExpType(User.Identity.Name, ExpType, ToPlace);
        //        double Amt = 0;
        //        double RetAmt = 0;
        //        DateTime DtFrm = DateTime.ParseExact(FrmDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        DateTime DtT0 = DateTime.ParseExact(ToDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        TimeSpan DtDiff = new TimeSpan(0, 0, 0, 0, 0);
        //        if (DtT0 >= DtFrm)
        //        {
        //            DtDiff = DtT0 - DtFrm;
        //            if (TrvlType == "1")
        //            {
        //                if (ExpType == "BORD" || ExpType == "LODG" || ExpType == "INCT")
        //                {
        //                    if (double.TryParse(objBl.Get_Amount_ForClaimExpType(User.Identity.Name, ExpType, ToPlace, DtDiff.Days) != null ? objBl.Get_Amount_ForClaimExpType(User.Identity.Name, ExpType, ToPlace, DtDiff.Days) : "0", out Amt))
        //                    { RetAmt = ((DtDiff.Days) + 1) * Amt; }
        //                    else
        //                    { RetAmt = ((DtDiff.Days) + 1) * Amt; }
        //                }
        //                else
        //                {
        //                    if (double.TryParse(objBl.Get_Amount_ForClaimExpType(User.Identity.Name, ExpType, ToPlace, DtDiff.Days) != null ? objBl.Get_Amount_ForClaimExpType(User.Identity.Name, ExpType, ToPlace, DtDiff.Days) : "0", out Amt))
        //                    { RetAmt = Amt; }
        //                    else
        //                    { RetAmt = Amt; }
        //                }
        //            }
        //            if (TrvlType == "2")
        //            {
        //                if (ExpType == "BOID" || ExpType == "LOIG" || ExpType == "INCT" || ExpType == "INIT")
        //                {
        //                    if (ExpType == "INCT")
        //                    {
        //                        ExpType = "INIT";
        //                    }
        //                    if (double.TryParse(objBl.Get_Amount_ForClaimExpType_International(User.Identity.Name, ExpType, ToPlace, DtDiff.Days) != null ? objBl.Get_Amount_ForClaimExpType_International(User.Identity.Name, ExpType, ToPlace, DtDiff.Days) : "0", out Amt))
        //                    { RetAmt = ((DtDiff.Days) + 1) * Amt; }
        //                    else
        //                    { RetAmt = ((DtDiff.Days) + 1) + 1 * Amt; }
        //                }
        //                else
        //                {
        //                    if (double.TryParse(objBl.Get_Amount_ForClaimExpType_International(User.Identity.Name, ExpType, ToPlace, DtDiff.Days) != null ? objBl.Get_Amount_ForClaimExpType_International(User.Identity.Name, ExpType, ToPlace, DtDiff.Days) : "0", out Amt))
        //                    { RetAmt = Amt; }
        //                    else
        //                    { RetAmt = Amt; }
        //                }
        //            }
        //        }
        //        return RetAmt.ToString();
        //    }
        //    catch (Exception Ex)
        //    {
        //        return "Failure :" + Ex.Message;
        //    }
        //}
        //#endregion

        //******************************************** GET CANCEL REASON ******************************************************************
        #region GET PLACE NAMES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetTrainingCancleReasons(string knownCategoryValues, string category)
        {
            try
            {
                Attendancebl Attendancebl = new Attendancebl();
                Attendancebo Attendancebo = new Attendancebo();
                var CancelReasons = Attendancebl.GetCancellationReason();
                List<CascadingDropDownNameValue> CanReasons = new List<CascadingDropDownNameValue>();

                foreach (var Crsn in CancelReasons)
                {
                    CanReasons.Add(new CascadingDropDownNameValue(Crsn.Cancellationreason, Crsn.CancellationID));
                }
                return CanReasons.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion
        //******************************************** GET QUOTA YEAR ******************************************************************
        #region Get Quota Year
        [WebMethod()]
        public CascadingDropDownNameValue[] GetQuotaYear(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> LeaveQuotaYear = new List<CascadingDropDownNameValue>();
                for (int i = DateTime.Now.AddYears(-1).Year; i < DateTime.Now.AddYears(2).Year; i++)
                { LeaveQuotaYear.Add(new CascadingDropDownNameValue(i.ToString(), i.ToString())); }

                return LeaveQuotaYear.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion


        //******************************************** GET YEAR FOR PLAYSLIP ******************************************************************
        #region Get Payslip Year
        [WebMethod()]
        public CascadingDropDownNameValue[] GetYearPayslip(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> PayslipYear = new List<CascadingDropDownNameValue>();
                //for (int i = DateTime.Now.AddYears(-5).Year; i < DateTime.Now.AddYears(1).Year; i++)
                for (int i = DateTime.Now.Year; i >= DateTime.Now.AddYears(-4).Year; i--)
                { PayslipYear.Add(new CascadingDropDownNameValue(i.ToString(), i.ToString())); }

                return PayslipYear.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        //******************************************** GET APPROVER LIST ********************************
        #region GET LEAVE TYPES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetApprover(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> ApproverList = new List<CascadingDropDownNameValue>();

                msassignedtomebo objAssginTMBo = new msassignedtomebo();
                objAssginTMBo.PERNR = User.Identity.Name;
                objAssginTMBo.COMMENTS = Session["CompCode"].ToString();
                msassignedtomebl objAssginTMBl = new msassignedtomebl();
                msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
                objAssginTMList = objAssginTMBl.Get_EmployeeDetails(objAssginTMBo);

                foreach (var AppNames in objAssginTMList)
                { ApproverList.Add(new CascadingDropDownNameValue(AppNames.S_NAME, AppNames.S_PERNR)); }

                return ApproverList.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion
        //******************************************** GET LANGUAGE TYPES ********************************
        #region GET LANGUAGE TYPES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetLanguageNames(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> LanguageList = new List<CascadingDropDownNameValue>();

                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();
                var VarLanguage = objDataContext.sp_master_load_language_dropdown();

                foreach (var LangNames in VarLanguage)
                { LanguageList.Add(new CascadingDropDownNameValue(LangNames.SPTXT.Trim(), LangNames.SPRSL.ToString().Trim())); }

                return LanguageList.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion
        //******************************************** GET RELIGION TYPES ********************************
        #region GET RELIGION TYPES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetReligionNames(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> ReligionList = new List<CascadingDropDownNameValue>();

                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();
                var VarReligion = objDataContext.sp_master_load_religion();

                foreach (var LangNames in VarReligion)
                { ReligionList.Add(new CascadingDropDownNameValue(LangNames.KTEXT.Trim(), LangNames.KITXT.ToString().Trim())); }

                return ReligionList.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion
        //******************************************** GET MARITIAL STATUS ********************************
        #region GET RELIGION TYPES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetMaritialStatus(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> MaritialList = new List<CascadingDropDownNameValue>();

                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();
                var VarMaritial = objDataContext.sp_master_load_marital_status();

                foreach (var MaritialNames in VarMaritial)
                { MaritialList.Add(new CascadingDropDownNameValue(MaritialNames.FTEXT.Trim(), MaritialNames.FAMST.ToString().Trim())); }

                return MaritialList.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion


        #region GET Employees for team calendar
        [WebMethod()]
        public CascadingDropDownNameValue[] GetEmpTC(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> EmpTCList = new List<CascadingDropDownNameValue>();
                msassignedtomebo objAssginTMBo = new msassignedtomebo();
                objAssginTMBo.PERNR = User.Identity.Name;
                objAssginTMBo.COMMENTS = "";
                msassignedtomebl objAssginTMBl = new msassignedtomebl();
                msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
                objAssginTMList = objAssginTMBl.Get_Sub_Employees_Of_Manager_Details(objAssginTMBo);

                foreach (var AppNames in objAssginTMList)
                { EmpTCList.Add(new CascadingDropDownNameValue(AppNames.ENAME, AppNames.PERNR)); }

                return EmpTCList.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        #region GET Employees for Payslip Admin
        [WebMethod()]
        public CascadingDropDownNameValue[] GetEmpList(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> EmpList = new List<CascadingDropDownNameValue>();

                msassignedtomebo objAssginTMBo = new msassignedtomebo();
                msassignedtomebl objAssginTMBl = new msassignedtomebl();
                msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
                objAssginTMList = objAssginTMBl.Get_AllEmployees_Details();

                foreach (var AppNames in objAssginTMList)
                { EmpList.Add(new CascadingDropDownNameValue(AppNames.ENAME, AppNames.PERNR)); }

                return EmpList.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion

        [WebMethod()]
        public string GetExcRateManagerEdit(string Createdby, string CountryID, string Region, string ExpenseType)
        {
            try
            {

                decimal? strRate = 0;
                string strCurrency = "";
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();
                objExpenseDataContext.sp_Get_DA_Rate_Mgr(Createdby, CountryID, Region, ExpenseType, ref strRate, ref strCurrency);
                objExpenseDataContext.Dispose();
                return strRate.ToString() + " ~ " + strCurrency;
            }
            catch (Exception Ex)
            { return null; }
        }


        [WebMethod()]
        public CascadingDropDownNameValue[] GetGL_Account(string knownCategoryValues, string category, string contextKey)
        {
            try
            {

                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();
                // StringDictionary rperner = CascadingDropDown.ParseKnownCategoryValuesString(contextKey);
                var GLname = objDataContext.sp_master_load_GLAccount(contextKey);
                List<CascadingDropDownNameValue> FrmToPlace = new List<CascadingDropDownNameValue>();
                foreach (var GL in GLname)
                { FrmToPlace.Add(new CascadingDropDownNameValue(GL.TXT50, GL.SAKNR)); }
                return FrmToPlace.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        [WebMethod()]
        public string GetExcRate(string CountryID, string Region, string ExpenseType, DateTime ExpenseDate)
        {
            try
            {

                decimal? strRate = 0;
                string strCurrency = "";
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();
                objExpenseDataContext.sp_Get_DA_Rate(User.Identity.Name, CountryID, Region, ExpenseType, ExpenseDate, ref strRate, ref strCurrency);
                objExpenseDataContext.Dispose();
                return strRate.ToString() + " ~ " + strCurrency;
            }
            catch (Exception Ex)
            { return null; }
        }


        [WebMethod]
        public string[] GetEmployeeNamesAndId(string prefixText)
        {
            msassignedtomebl objAssginTMBl = new msassignedtomebl();
            DataTable EmplyeeDataTable = new DataTable();
            string[] EmplyeeNameItems = null;
            List<msassignedtomebo> objAssginTMBoList = objAssginTMBl.Get_Employee_Names(prefixText);
            EmplyeeDataTable.Columns.Add("EmpName");
            EmplyeeDataTable.Columns.Add("EmpNo");
            foreach (var item in objAssginTMBoList)
            {
                EmplyeeDataTable.Rows.Add(item.EMPLOYEE_NAME, item.EMPLOYEE_NO);
            }
            if (EmplyeeDataTable != null)
            {
                EmplyeeNameItems = new string[EmplyeeDataTable.Rows.Count];
            }
            if (EmplyeeDataTable != null)
            {
                int i = 0;
                foreach (DataRow dr in EmplyeeDataTable.Rows)
                {
                    EmplyeeNameItems.SetValue(dr["EmpNo"].ToString() + "-" + dr["EmpName"].ToString(), i);
                    i++;
                }
            }
            return EmplyeeNameItems;
        }


        #region Get Team Calendar Year
        [WebMethod()]
        public CascadingDropDownNameValue[] GetTCYear(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> TCYear = new List<CascadingDropDownNameValue>();
                for (int i = DateTime.Now.AddYears(-1).Year; i < DateTime.Now.AddYears(2).Year; i++)
                { TCYear.Add(new CascadingDropDownNameValue(i.ToString(), i.ToString())); }

                return TCYear.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion


        #region Get Form16
        [WebMethod()]
        public CascadingDropDownNameValue[] GetYearForm16(string knownCategoryValues, string category)
        {
            try
            {
                List<CascadingDropDownNameValue> Form16 = new List<CascadingDropDownNameValue>();
                //for (int i = DateTime.Now.AddYears(-5).Year; i < DateTime.Now.AddYears(1).Year; i++)
                for (int i = DateTime.Now.Year; i >= DateTime.Now.AddYears(-4).Year; i--)
                { Form16.Add(new CascadingDropDownNameValue((i - 1).ToString() + "-" + ((i) % 100).ToString(), (i - 1).ToString() + "-" + ((i) % 100).ToString())); }

                return Form16.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion



        #region ACTIVITIES
        [WebMethod()]
        public CascadingDropDownNameValue[] GetActivities(string knownCategoryValues, string category)
        {
            try
            {
                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();
                var Activity = objDataContext.sp_get_Activities();
                List<CascadingDropDownNameValue> Activities = new List<CascadingDropDownNameValue>();
                foreach (var Acty in Activity)
                { Activities.Add(new CascadingDropDownNameValue(Acty.AWART, Acty.ID.ToString())); }
                return Activities.ToArray();
            }
            catch (Exception Ex)
            { throw Ex; }
        }
        #endregion


        //for mailids

        //////Main Search



        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] getIDs(string prefixText)
        {
            masterdalDataContext objTravelRequestDataContext = new masterdalDataContext();
            DataTable EmplyeeDataTable = new DataTable();
            string[] EmplyeeNameItems = null;
            //List<string> requisitionboList = objTravelRequestDataContext.usp_load_basedon_mainSearch(HttpContext.Current.User.Identity.Name, prefixText, 1);
            EmplyeeDataTable.Columns.Add("Details");

            foreach (var item in objTravelRequestDataContext.payc_load_basedon_mainSearch(HttpContext.Current.User.Identity.Name, prefixText, 1))
            {
                EmplyeeDataTable.Rows.Add(item.Details);
            }
            if (EmplyeeDataTable != null)
            {
                EmplyeeNameItems = new string[EmplyeeDataTable.Rows.Count];
            }
            if (EmplyeeDataTable != null)
            {
                int i = 0;
                foreach (DataRow dr in EmplyeeDataTable.Rows)
                {
                    EmplyeeNameItems.SetValue(dr["Details"].ToString(), i);
                    i++;
                }
            }
            return EmplyeeNameItems;
        }



        [WebMethod]
        public string[] getrwtActy(string prefixText)
        {
            TicketingToolbl TicketingToolbl = new TicketingToolbl();
            DataTable EmplyeeDataTable = new DataTable();
            string[] EmplyeeNameItems = null;
            List<TicketingToolbo> TicketingboLst = TicketingToolbl.Get_Employee_MailIDS(prefixText);
            EmplyeeDataTable.Columns.Add("EmpNameMailID");
            EmplyeeDataTable.Columns.Add("EmpNoMailID");
            foreach (var item in TicketingboLst)
            {
                EmplyeeDataTable.Rows.Add(item.EMPMAILIDS, item.EMPMAILIDS);
            }
            if (EmplyeeDataTable != null)
            {
                EmplyeeNameItems = new string[EmplyeeDataTable.Rows.Count];
            }
            if (EmplyeeDataTable != null)
            {
                int i = 0;
                foreach (DataRow dr in EmplyeeDataTable.Rows)
                {
                    EmplyeeNameItems.SetValue(dr["EmpNameMailID"].ToString(), i);
                    i++;
                }
            }
            return EmplyeeNameItems;
        }



        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] GetEmployeeMailId(string Actytxt)
        {
            masterdalDataContext mstrcontxt = new masterdalDataContext();
            DataTable tbl = new DataTable();
            string[] itms = null;
            tbl.Columns.Add("Activity");

            foreach (var lst in mstrcontxt.payc_master_load_acty("itch", Actytxt, ""))
            {
                tbl.Rows.Add(lst.Activity);
            }
            if (tbl != null)
            {
                itms = new string[tbl.Rows.Count];
            }
            if (tbl != null)
            {
                int i = 0;
                foreach (DataRow dr in tbl.Rows)
                {
                    itms.SetValue(dr["Activity"].ToString(), i);
                    i++;
                }
            }
            return itms;
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public string[] getactivits(string prefixText)
        {
            masterdalDataContext objTravelRequestDataContext = new masterdalDataContext();
            DataTable EmplyeeDataTable = new DataTable();
            string[] EmplyeeNameItems = null;
            EmplyeeDataTable.Columns.Add("Details");


            foreach (var item in objTravelRequestDataContext.payc_master_load_acty(Session["CompCode"].ToString().Trim(), prefixText.Trim(), Session["WBSVal"].ToString().Trim()))
            {
                EmplyeeDataTable.Rows.Add(item.Activity);
            }
            if (EmplyeeDataTable != null)
            {
                EmplyeeNameItems = new string[EmplyeeDataTable.Rows.Count];
            }
            if (EmplyeeDataTable != null)
            {
                int i = 0;
                foreach (DataRow dr in EmplyeeDataTable.Rows)
                {
                    EmplyeeNameItems.SetValue(dr["Details"].ToString(), i);
                    i++;
                }

                Session["AddVal"] = i;//== 0 ? 0 : 1;
                Session["TxtprefixText"] = prefixText;
            }
            return EmplyeeNameItems;
        }


    }
}

using System;
using System.Collections.Generic;
//using System.Collections.IEnumerable;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using System.Globalization;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for travelrequestbl
/// </summary>
public class travelrequestbl
{
    public travelrequestbl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int update_requisition_status_for_hod_proposals_approve(requisitionbo objBo)
    {
        int iResult = -1;
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            iResult = objTravelRequestDataContext.sp_update_requisition_status_for_hod_proposals_approve(objBo.FTPT_REQUEST_ID, objBo.REQ_SEGMENT_ID, objBo.CURRENT_STATUS);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public int update_requisition_status_for_hod_proposals_reject(requisitionbo objBo)
    {
        int iResult = -1;
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            iResult = objTravelRequestDataContext.sp_update_requisition_status_for_hod_proposals_reject(objBo.FTPT_REQUEST_ID, objBo.REQ_SEGMENT_ID, objBo.CURRENT_STATUS);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public List<requisitionbo> Get_Employee_Names(string prefixText)
    {
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        foreach (var vRow in objTravelRequestDataContext.sp_get_employee_names(prefixText))
        {
            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.EMPLOYEE_NAME = vRow.ENAME;
            requisitionboObj.EMPLOYEE_NO = vRow.PERNR;
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }
    public List<RequisitionAndProposalCountBO> GetRequisitionAndProposalsCount(string PERNR, string strRole)
    {
        List<RequisitionAndProposalCountBO> requisitionboList = new List<RequisitionAndProposalCountBO>();
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        foreach (var vRow in objTravelRequestDataContext.sp_get_RequisitionAndProposalsCount(PERNR, strRole))
        {
            RequisitionAndProposalCountBO requisitionboObj = new RequisitionAndProposalCountBO();
            requisitionboObj.LocalAccommodationRequisitionCount = (int)vRow.LocalAccommodationRequisitionCount;
            requisitionboObj.LocalTravelRequisitionCount = (int)vRow.LocalTravelRequisitionCount;

            requisitionboObj.LocalAccommodationProposalsCount = (int)vRow.LocalAccommodationProposalsCount;
            requisitionboObj.LocalTravelProposalsCount = (int)vRow.LocalTravelProposalsCount;

            requisitionboObj.OutStationRequisitionCount = (int)vRow.OutStatinRequisitionCount;
            requisitionboObj.OutStationProposalsCount = (int)vRow.OutStationProposalsCount;
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public int Create_TravelRequest(requisitionbo objBo, ref int? RequisitionID, ref string RequisitionSegmentIDs)
    {
        int iResult = -1;
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            iResult = objTravelRequestDataContext.sp_create_requisition_mst_dtl(objBo.EMPLOYEE_NO, objBo.CREATED_BY, objBo.CHK, objBo.TRAVEL_DATE_ALL,
            objBo.TRAVEL_TIME, objBo.ISACTIVE, objBo.CURRENT_STATUS, objBo.MODE_OF_TRANSPOPRT_KZPMF, objBo.MEDIA_OF_CATEGORY_PKWKL, objBo.VEHICLE_NAME_VHNUM_ALL, objBo.REGION_RGION_FROM,
            objBo.REGION_RGION_TO, objBo.FLYNUM, objBo.ADVANCE, objBo.AIRLINE, objBo.VISA_REQUIRED_ALL, objBo.FR_EXCHANGE, objBo.INSUR_MEDICLAIM, objBo.SEAT_PREFERENCE,
            objBo.MEAL_PREFERENCE, objBo.BAGGAGE, objBo.HAND, objBo.REMARKS, ref RequisitionID, ref RequisitionSegmentIDs, objBo.ARRIVAL_DATE.ToString(), objBo.ARRIVAL_TIME, objBo.REASON);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iResult;
    }

    public int Update_TravelRequest(requisitionbo objBo)
    {
        int iResult = -1;
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            iResult = objTravelRequestDataContext.sp_update_requisition_mst_dtl(objBo.FTPT_REQUEST_ID, "", objBo.CHK, objBo.REQ_SEGMENT_ID,
            objBo.TRAVEL_DATE, objBo.TRAVEL_TIME, objBo.MODE_OF_TRANSPOPRT_KZPMF, objBo.MEDIA_OF_CATEGORY_PKWKL,
            0, objBo.REGION_RGION_FROM, objBo.REGION_RGION_TO, objBo.FLYNUM, objBo.ADVANCE, objBo.AIRLINE,
            objBo.VISA_REQUIRED_ALL, objBo.FR_EXCHANGE, objBo.INSUR_MEDICLAIM, objBo.SEAT_PREFERENCE, objBo.MEAL_PREFERENCE, objBo.BAGGAGE,
            objBo.HAND, "", objBo.REMARKS, objBo.CURRENT_STATUS, objBo.ARRIVAL_DATE.ToString(), objBo.ARRIVAL_TIME, objBo.REASON);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public int HODFirstLevelStatusUpdate(requisitionbo objBo)
    {
        int iResult = -1;
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            iResult = objTravelRequestDataContext.sp_update_HOD_First_Level_Status(objBo.FTPT_REQUEST_ID, objBo.EMPLOYEE_NO, objBo.REQ_SEGMENT_ID, objBo.REMARKS, objBo.REASON_FOR_CANCEL, objBo.CURRENT_STATUS);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public int HODSecondLevelStatusUpdate(requisitionbo objBo)
    {
        int iResult = -1;
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            iResult = objTravelRequestDataContext.sp_update_HOD_First_Level_Status(objBo.FTPT_REQUEST_ID, objBo.EMPLOYEE_NO, objBo.REQ_SEGMENT_ID,
                objBo.REMARKS, objBo.REASON_FOR_CANCEL, objBo.CURRENT_STATUS);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public int UpdateReasonForCancelFromTraveller(requisitionbo objBo)
    {
        int iResult = -1;
        try
        {
            ticketbookingDataContext objTravelRequestDataContext = new ticketbookingDataContext();
            iResult = objTravelRequestDataContext.sp_UpdateRemarksForTravellerCancel(objBo.FTPT_REQUEST_ID, objBo.REQ_SEGMENT_ID, objBo.EMPLOYEE_NO, objBo.REASON_FOR_CANCEL);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public int UpdateAccommReasonForCancelFromTraveller(int iAccreqId, string strModifBy, string strReasonForCancel, string strrole)
    {
        int iResult = -1;
        try
        {
            ticketbookingDataContext objTravelRequestDataContext = new ticketbookingDataContext();
            iResult = objTravelRequestDataContext.sp_UpdateRemarksAccomForTravellerCancel(iAccreqId, strModifBy, strReasonForCancel, strrole);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public int UpdateVehiReasonForCancelFromTraveller(int iVehireqId, string strModifBy, string strReasonForCancel, string strrole)
    {
        int iResult = -1;
        try
        {
            ticketbookingDataContext objTravelRequestDataContext = new ticketbookingDataContext();
            iResult = objTravelRequestDataContext.sp_UpdateRemarksVehiForTravellerCancel(iVehireqId, strModifBy, strReasonForCancel, strrole);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public void Update_TravelRequisition(requisitionbo objBo)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            //objTravelRequestDataContext.sp_update_travel_requisition_details(objBo.CHK,objBo.TRAVEL_DATE, objBo.TRAVEL_TIME, objBo.MODE_OF_TRANSPOPRT_KZPMF,
            //objBo.MEDIA_OF_CATEGORY_PKWKL, objBo.VEHICLE_NAME_VHNUM, objBo.REGION_RGION_FROM, objBo.REGION_RGION_TO, objBo.FTPT_REQUEST_ID, objBo.AIRLINE, objBo.VISA_REQUIRED, objBo.FR_EXCHANGE,
            //objBo.INSUR_MEDICLAIM, objBo.SEAT_PREFERENCE, objBo.MEAL_PREFERENCE, objBo.BAGGAGE, objBo.HAND, objBo.ADVANCE);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    public int Update_ApproveRejClaims(string SlNos, string ClaimStatus, string Remarks)
    {
        int iResult = -1;
        try
        {
            using (travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext())
            {
                iResult = objTravelRequestDataContext.sp_approve_rej_claims(ClaimStatus, Remarks, SlNos);
            }
        }
        catch (Exception ex)
        { ex.Message.ToString(); }
        return iResult;
    }

    public travelrequestcolumnscollectionbo Get_TravelRequestDetails(travelrequestcolumnsbo objBo)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();
        foreach (var vRow in objTravelRequestDataContext.sp_bnp_search_travel_request(objBo.TRAVEL_PERNR))
        {
            travelrequestcolumnsbo objColumnsBo = new travelrequestcolumnsbo();
            objColumnsBo.TRAVEL_REQUEST_NO = Int32.Parse(vRow.Trip_number.ToString());
            objColumnsBo.TRAVEL_END_LOCATION = vRow.First_Destination;
            objColumnsBo.TRAVEL_START_DATE = Convert.ToDateTime(vRow.Trip_start_Date.ToString());
            objColumnsBo.TRAVEL_REASON = vRow.Reason_for_trip;
            objColumnsBo.CURRENT_STATUS = vRow.current_status;
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }
    public List<requisitionbo> Load_TravelRequestionDetails(string TravelType_Id, string TravelDate, string EmployeeId, string EmployeeName)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_HOD_travel_requisitions(TravelType_Id, TravelDate, EmployeeId, EmployeeName))
        {

            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = vRow.REQUISITION_ID;
            requisitionboObj.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID;
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
            requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
            requisitionboObj.REASON_FOR_CANCEL = vRow.REASON_FOR_CANCEL;
            requisitionboObj.EMPLOYEE_NAME = Convert.ToString(vRow.ENAME);
            requisitionboObj.EMPLOYEE_NO = Convert.ToString(vRow.EMPLOYEE_NO);
            requisitionboObj.EMAIL = Convert.ToString(vRow.Email);
            requisitionboObj.ARRIVAL_DATE = vRow.TRIP_END_DATE.ToString();
            requisitionboObj.ARRIVAL_TIME = vRow.ARRIVAL_TIME;
            requisitionboObj.REASON = vRow.REASON;
            if (vRow.PhoneNumber != null)
            {
                requisitionboObj.PHONE_NUMBER = Convert.ToString(vRow.PhoneNumber);
            }
            requisitionboObj.DESIGNATION = Convert.ToString(vRow.Designation);
            if (!string.IsNullOrEmpty(vRow.current_status))
            {
                ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            }
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    // ----- CLAIMS -----

    //public List<requisitionbo> Load_TravelClaims(string EmployeeId, string EmployeeName)
    //{
    //    travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
    //    List<requisitionbo> requisitionboList = new List<requisitionbo>();
    //    foreach (var vRow in objTravelRequestDataContext.sp_get_HOD_travel_claims(EmployeeId, EmployeeName))
    //    {

    //        requisitionbo requisitionboObj = new requisitionbo();
    //        requisitionboObj.FTPT_REQUEST_ID = vRow.REQUISITION_ID.Value;
    //        requisitionboObj.ClaimSlNo = vRow.slno;
    //        requisitionboObj.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID.Value;
    //        requisitionboObj.EMPLOYEE_NO = vRow.PERNR;
    //        requisitionboObj.TRAVEL_DATE = DateTime.ParseExact(vRow.DATE, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //        requisitionboObj.REGION_TEXT25_FROM = vRow.From_Place;  //from text
    //        requisitionboObj.REGION_TEXT25_TO = vRow.To_Place;       //to text
    //        requisitionboObj.FLYNUM = vRow.EXPENSE_TYPE;
    //        requisitionboObj.ADVANCE = vRow.AMOUNT;
    //        requisitionboObj.FR_EXCHANGE = vRow.CURRENCY_NAME;
    //        requisitionboObj.ARRIVAL_DATE = vRow.DATE2;
    //        requisitionboList.Add(requisitionboObj);
    //    }
    //    return requisitionboList;
    //}
    //--------- TRAVEL DESK CLAIM APPROVAL -------
    public List<requisitionbo> Load_TravelClaimsForTD(string EmployeeId, string EmployeeName)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_TD_approval_travel_claims(EmployeeId, EmployeeName))
        {

            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = vRow.REQUISITION_ID.Value;
            requisitionboObj.REQ_SEGMENT_ID = vRow.slno;
            requisitionboObj.EMPLOYEE_NO = vRow.PERNR;
            requisitionboObj.TRAVEL_DATE = DateTime.ParseExact(vRow.DATE, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            requisitionboObj.REGION_TEXT25_FROM = vRow.From_Place;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To_Place;       //to text
            requisitionboObj.FLYNUM = vRow.EXPENSE_TYPE;
            requisitionboObj.ADVANCE = vRow.AMOUNT;
            requisitionboObj.FR_EXCHANGE = vRow.CURRENCY_NAME;
            requisitionboObj.ARRIVAL_DATE = vRow.DATE2;
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<requisitionbo> Load_TravelRequestionDetails_TD(string TravelType_Id, string TravelDate, string EmployeeId, string EmployeeName)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_TD_travel_requisitions(TravelType_Id, TravelDate, EmployeeId, EmployeeName))
        {

            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = vRow.REQUISITION_ID;
            requisitionboObj.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID;
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
            requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
            requisitionboObj.REASON_FOR_CANCEL = vRow.REASON_FOR_CANCEL;
            requisitionboObj.EMPLOYEE_NAME = Convert.ToString(vRow.ENAME);
            requisitionboObj.EMPLOYEE_NO = Convert.ToString(vRow.EMPLOYEE_NO);
            requisitionboObj.EMAIL = Convert.ToString(vRow.Email);
            requisitionboObj.ARRIVAL_DATE = vRow.TRIP_END_DATE.ToString();
            requisitionboObj.ARRIVAL_TIME = vRow.ARRIVAL_TIME;
            requisitionboObj.REASON = vRow.REASON;
            if (vRow.PhoneNumber != null)
            {
                requisitionboObj.PHONE_NUMBER = Convert.ToString(vRow.PhoneNumber);
            }
            requisitionboObj.DESIGNATION = Convert.ToString(vRow.Designation);
            if (!string.IsNullOrEmpty(vRow.current_status))
            {
                ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            }
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<requisitionbo> Load_EmployeeTravelRequestionDetails(string TRAVEL_TYPE, string LoggedInUser) // (string TravelType_Id,string TravelDate,string EmployeeId,string EmployeeName)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_travel_requisition_details(TRAVEL_TYPE, LoggedInUser))          //sp_get_HOD_travel_requisitions(TravelType_Id, TravelDate, EmployeeId, EmployeeName))
        {
            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = (int)vRow.REQUISITION_ID;
            requisitionboObj.REQ_SEGMENT_ID = (int)vRow.REQ_SEGMENT_ID;
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
            requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
            requisitionboObj.REASON_FOR_CANCEL = vRow.REASON_FOR_CANCEL;
            //-----
            requisitionboObj.ARRIVAL_DATE = Convert.ToString(vRow.TRIP_END_DATE);
            requisitionboObj.TRAVEL_VEH_TYPE = vRow.FZTXT;
            requisitionboObj.TRAVEL_VEH_CATEGORY = vRow.TEXT25;
            requisitionboObj.ARRIVAL_TIME = vRow.ARRIVAL_TIME;
            requisitionboObj.REASON = vRow.REASON;

            if (!string.IsNullOrEmpty(vRow.current_status))
            {
                ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            }
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }


    public List<requisitionbo> Load_EmployeeTravelRequestionDetails_ToCancel(string LoggedInUser) // (string TravelType_Id,string TravelDate,string EmployeeId,string EmployeeName)
    {
        ticketbookingDataContext objTravelRequestDataContext = new ticketbookingDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_travelFTP_details_ForTraveller_ToCancel(LoggedInUser))          //sp_get_HOD_travel_requisitions(TravelType_Id, TravelDate, EmployeeId, EmployeeName))
        {
            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = (int)vRow.REQUISITION_ID;
            requisitionboObj.REQ_SEGMENT_ID = (int)vRow.REQ_SEGMENT_ID;
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
            requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
            requisitionboObj.REASON_FOR_CANCEL = vRow.REASON_FOR_CANCEL;
            if (!string.IsNullOrEmpty(vRow.current_status))
            {
                ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            }
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<requisitionbo> Load_EmployeeTravelRequestionDetailsNotBooked() // (string TravelType_Id,string TravelDate,string EmployeeId,string EmployeeName)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_travel_requisition_details_not_booked())          //sp_get_HOD_travel_requisitions(TravelType_Id, TravelDate, EmployeeId, EmployeeName))
        {
            requisitionbo requisitionboObj = new requisitionbo();
            //requisitionboObj.FTPT_REQUEST_ID = vRow.REQUISITION_ID;
            //requisitionboObj.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID;
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.REMARKS;
            if (!string.IsNullOrEmpty(vRow.current_status))
            {
                ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            }
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<requisitionbo> Load_NewProposalDetails(string TravelType_Id, string TravelDate, string EmployeeId, string EmployeeName)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_new_proposal_details(TravelType_Id, TravelDate, EmployeeId, EmployeeName))
        {
            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = Convert.ToInt32(vRow.REQUISITION_ID);
            requisitionboObj.REQ_SEGMENT_ID = Convert.ToInt32(vRow.REQ_SEGMENT_ID);
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
            requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
            requisitionboObj.REASON_FOR_CANCEL = vRow.REASON_FOR_CANCEL;
            requisitionboObj.EMPLOYEE_NAME = vRow.ENAME;
            requisitionboObj.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<requisitionbo> Load_NewProposalDetails_ForTraveller(string TravelType_Id, string TravelDate, string EmployeeId, string EmployeeName)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_proposal_details_For_Traveller(TravelType_Id, TravelDate, EmployeeId, EmployeeName))
        {
            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = Convert.ToInt32(vRow.REQUISITION_ID);
            requisitionboObj.REQ_SEGMENT_ID = Convert.ToInt32(vRow.REQ_SEGMENT_ID);
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
            requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
            requisitionboObj.EMPLOYEE_NAME = vRow.ENAME;
            requisitionboObj.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
            //if (!string.IsNullOrEmpty(vRow))
            //{
            //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
            //    requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            //}
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<requisitionbo> Load_NewProposalDetails_ForTraveller_ToCancel(string EmployeeId)
    {
        ticketbookingDataContext objTravelRequestDataContext = new ticketbookingDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_proposal_For_Traveller_TOCancel(EmployeeId))
        {
            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = Convert.ToInt32(vRow.REQUISITION_ID);
            requisitionboObj.REQ_SEGMENT_ID = Convert.ToInt32(vRow.REQ_SEGMENT_ID);
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
            requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
            requisitionboObj.EMPLOYEE_NAME = vRow.ENAME;
            requisitionboObj.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
            if (!string.IsNullOrEmpty(vRow.current_status))
            {
                ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            }
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public List<requisitionbo> Load_Employee_Proposals()
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_proposals_employee())
        {
            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = vRow.PROPOSAL_ID;
            requisitionboObj.REQ_SEGMENT_ID = vRow.PRO_SEGMENT_ID;
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            requisitionboObj.CURRENT_STATUS = vRow.current_status;
            if (!string.IsNullOrEmpty(vRow.current_status))
            {
                ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            }

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public travelrequestcolumnscollectionbo Load_TravelRequestDetails(string strTripNo, ref short? count)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();
        foreach (var vRow in objTravelRequestDataContext.sp_bnp_load_travel_request_details(strTripNo, ref count))
        {
            travelrequestcolumnsbo objColumnsBo = new travelrequestcolumnsbo();
            objColumnsBo.TRAVEL_REQUEST_NO = Int32.Parse(vRow.REINR.ToString());
            objColumnsBo.TRAVEL_REQUEST = Int16.Parse(vRow.REQUEST.ToString());
            objColumnsBo.TRAVEL_ININERARY_NUMBER = Int16.Parse(vRow.ITINERARY_NUMBER.ToString());

            string[] startHourMnt = { "00:00" };
            string[] endHourMnt = { "00:00" };

            startHourMnt = vRow.TIME_BEG.Split(':');
            endHourMnt = vRow.TIME_END.Split(':');

            if (vRow.REQUEST_TYPE == null)
            {
                if (vRow.DATE_BEG == Convert.ToDateTime("01/01/0001"))
                {
                    objColumnsBo.TRAVEL_START_DATE = Convert.ToDateTime("01/01/0001");
                }
                else
                {
                    objColumnsBo.TRAVEL_START_DATE = Convert.ToDateTime(vRow.DATE_BEG.ToString());
                }

                if (startHourMnt[0] == "" || startHourMnt[0] == null)
                {
                    objColumnsBo.TRAVEL_START_HOUR = "00";
                }
                else
                {
                    objColumnsBo.TRAVEL_START_HOUR = startHourMnt[0];
                }

                if (startHourMnt[1] == "" || startHourMnt[1] == null)
                {
                    objColumnsBo.TRAVEL_START_MINUTE = "00";
                }
                else
                {
                    objColumnsBo.TRAVEL_START_MINUTE = startHourMnt[1];
                }

                if (vRow.DATE_END == Convert.ToDateTime("01/01/0001"))
                {
                    objColumnsBo.TRAVEL_END_DATE = Convert.ToDateTime("01/01/0001");
                }
                else
                {
                    objColumnsBo.TRAVEL_END_DATE = Convert.ToDateTime(vRow.DATE_END.ToString());
                }

                if (endHourMnt[0] == "" || endHourMnt[0] == null)
                {
                    objColumnsBo.TRAVEL_END_HOUR = "00";
                }
                else
                {
                    objColumnsBo.TRAVEL_END_HOUR = endHourMnt[0];
                }

                if (endHourMnt[1] == "" || endHourMnt[1] == null)
                {
                    objColumnsBo.TRAVEL_END_MINUTE = "00";
                }
                else
                {
                    objColumnsBo.TRAVEL_END_MINUTE = endHourMnt[1];
                }

                if (vRow.LOCATION_END == null)
                {
                    objColumnsBo.TRAVEL_END_LOCATION = string.Empty;
                }
                else
                {
                    objColumnsBo.TRAVEL_END_LOCATION = vRow.LOCATION_END;
                }

                if (vRow.COUNTRY_END == null)
                {
                    objColumnsBo.TRAVEL_END_COUNTRY = "IN";
                }
                else
                {
                    objColumnsBo.TRAVEL_END_COUNTRY = vRow.COUNTRY_END;
                }
            }

            if (vRow.DATE_BEG == null)
            {
                objColumnsBo.TRANS_START_DATE = Convert.ToDateTime("01/01/0001");
            }
            else
            {
                objColumnsBo.TRANS_START_DATE = Convert.ToDateTime(vRow.DATE_BEG.ToString());
            }

            if (startHourMnt[0] == "" || startHourMnt[0] == null)
            {
                objColumnsBo.TRANS_START_HOUR = "00";
            }
            else
            {
                objColumnsBo.TRANS_START_HOUR = startHourMnt[0];
            }

            if (startHourMnt[1] == "" || startHourMnt[1] == null)
            {
                objColumnsBo.TRANS_START_MINUTE = "00";
            }
            else
            {
                objColumnsBo.TRANS_START_MINUTE = startHourMnt[1];
            }

            if (vRow.LOCATION_BEG == null)
            {
                objColumnsBo.TRANS_START_LOCATION = string.Empty;
            }
            else
            {
                objColumnsBo.TRANS_START_LOCATION = vRow.LOCATION_BEG;
            }

            if (vRow.COUNTRY_BEG == null)
            {
                objColumnsBo.TRANS_START_COUNTRY = "IN";
            }
            else
            {
                objColumnsBo.TRANS_START_COUNTRY = vRow.COUNTRY_BEG;
            }

            if (vRow.DATE_END == null)
            {
                objColumnsBo.TRANS_END_DATE = Convert.ToDateTime("01/01/0001");
            }
            else
            {
                objColumnsBo.TRANS_END_DATE = Convert.ToDateTime(vRow.DATE_END);
            }

            if (endHourMnt[0] == "" || endHourMnt[0] == null)
            {
                objColumnsBo.TRANS_END_HOUR = "00";
            }
            else
            {
                objColumnsBo.TRANS_END_HOUR = endHourMnt[0];
            }

            if (endHourMnt[1] == "" || endHourMnt[1] == null)
            {
                objColumnsBo.TRANS_END_MINUTE = "00";
            }
            else
            {
                objColumnsBo.TRANS_END_MINUTE = endHourMnt[1];
            }

            if (vRow.LOCATION_END == null)
            {
                objColumnsBo.TRANS_END_LOCATION = string.Empty;
            }
            else
            {
                objColumnsBo.TRANS_END_LOCATION = vRow.LOCATION_END;
            }

            if (vRow.COUNTRY_END == null)
            {
                objColumnsBo.TRANS_END_COUNTRY = string.Empty;
            }
            else
            {
                objColumnsBo.TRANS_END_COUNTRY = vRow.COUNTRY_END;
            }

            if (vRow.ACTIVITY == " " || vRow.ACTIVITY == null)
            {
                objColumnsBo.TRAVEL_ACTIVITY = "";
            }
            else
            {
                objColumnsBo.TRAVEL_ACTIVITY = vRow.ACTIVITY.ToString();
            }

            if (vRow.REASON == null)
            {
                objColumnsBo.TRAVEL_REASON = string.Empty;
            }
            else
            {
                objColumnsBo.TRAVEL_REASON = vRow.REASON;
            }

            objColumnsBo.TRANS_REQUEST_TYPE = vRow.REQUEST_TYPE;

            if (vRow.VORSC == null)
            {
                objColumnsBo.ADVANCE_AMOUNT = 0L;
            }
            else
            {
                objColumnsBo.ADVANCE_AMOUNT = Convert.ToDouble(vRow.VORSC.ToString());
            }

            if (vRow.DATVS == null || vRow.DATVS == Convert.ToDateTime("01/01/1900"))
            {
                objColumnsBo.REQUIRED_BY_DATE = Convert.ToDateTime("01/01/0001");
            }
            else
            {
                objColumnsBo.REQUIRED_BY_DATE = Convert.ToDateTime(vRow.DATVS.ToString());
            }

            if (vRow.TEXTS == null)
            {
                objColumnsBo.COMMENTS = string.Empty;
            }
            else
            {
                objColumnsBo.COMMENTS = vRow.TEXTS;
            }

            if (vRow.ESTIMATED_COST != null)
            {
                objColumnsBo.ESTIMATED_COST = Convert.ToDouble(vRow.ESTIMATED_COST.ToString());
            }
            else
            {
                objColumnsBo.ESTIMATED_COST = 0L;
            }
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }


    public void Update_TravelRequest(travelrequestcolumnsbo objBo,
                                                            ref bool? IS_SUPERVISR_APPROVAL_REQ,
                                                            ref bool? IS_HR_APPROVAL_REQ,
                                                            ref string Super_Pernr,
                                                            ref string Super_Name,
                                                            ref string Super_Mail,
                                                            ref string Hr_Pernr,
                                                            ref string Hr_Name,
                                                            ref string Hr_Mail,
                                                            ref string Pernr_Mail,
                                                            ref bool? SaveStatus)
    {
        //try
        //{
        //    travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        //    objTravelRequestDataContext.sp_bnp_update_travel_request(objBo.TRAVEL_PERNR,
        //                                                            objBo.TRAVEL_START_DATE,
        //                                                            objBo.TRAVEL_START_HOUR,
        //                                                            objBo.TRAVEL_START_MINUTE,
        //                                                            objBo.TRAVEL_END_DATE,
        //                                                            objBo.TRAVEL_END_HOUR,
        //                                                            objBo.TRAVEL_END_MINUTE,
        //                                                            objBo.TRAVEL_SL_NO_LIST,
        //                                                            objBo.TRAVEL_START_LOCATION_LIST,
        //                                                            objBo.TRAVEL_START_COUNTRY_LIST,
        //                                                            objBo.TRAVEL_ACTIVITY_LIST,
        //                                                            objBo.TRAVEL_REASON_LIST,
        //                                                            objBo.TRAVEL_START_DATE_LIST,
        //                                                            objBo.TRAVEL_START_HOUR_LIST,
        //                                                            objBo.TRAVEL_START_MINUTE_LIST,
        //                                                            Decimal.Parse(objBo.ADVANCE_AMOUNT.ToString()),
        //                                                            objBo.REQUIRED_BY_DATE,
        //                                                            Int16.Parse(objBo.COST_SL_NO.ToString()),
        //                                                            Decimal.Parse(objBo.COST_PERCENTAGE.ToString()),
        //                                                            objBo.COST_COSTCENTER,
        //                                                            Decimal.Parse(objBo.ESTIMATED_COST.ToString()),
        //                                                            objBo.COMMENTS,
        //                                                            objBo.TRANS_SL_NO_LIST,
        //                                                            objBo.TRANS_START_DATE_LIST,
        //                                                            objBo.TRANS_START_HOUR_LIST,
        //                                                            objBo.TRANS_START_MINUTE_LIST,
        //                                                            objBo.TRANS_START_LOCATION_LIST,
        //                                                            objBo.TRANS_START_COUNTRY_LIST,
        //                                                            objBo.TRANS_END_DATE_LIST,
        //                                                            objBo.TRANS_END_HOUR_LIST,
        //                                                            objBo.TRANS_END_MINUTE_LIST,
        //                                                            objBo.TRANS_END_LOCATION_LIST,
        //                                                            objBo.TRANS_END_COUNTRY_LIST,
        //                                                            objBo.TRANS_REQUEST_TYPE_LIST,
        //                                                            objBo.TRAVEL_REQUEST_NO,
        //                                                            ref IS_SUPERVISR_APPROVAL_REQ,
        //                                                            ref IS_HR_APPROVAL_REQ,
        //                                                            ref Super_Pernr,
        //                                                            ref Super_Name,
        //                                                            ref Super_Mail,
        //                                                            ref Hr_Pernr,
        //                                                            ref Hr_Name,
        //                                                            ref Hr_Mail,
        //                                                            ref Pernr_Mail,
        //                                                            ref SaveStatus);
        //    objTravelRequestDataContext.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    ex.Message.ToString();
        //}
    }

    public void Delete_TravelRequestDetails(travelrequestcolumnsbo objBo)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();

        objTravelRequestDataContext.sp_bnp_delete_travel_request(objBo.TRAVEL_REQUEST_NO);

        objTravelRequestDataContext.Dispose();
    }

    public travelrequestcolumnsbo Get_CostCenterDetails(string strUser)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        travelrequestcolumnsbo objColumnsBo = new travelrequestcolumnsbo();
        foreach (var vRow in objTravelRequestDataContext.sp_bnp_get_costcenter_details(strUser))
        {
            if (vRow.KOSTL == null || vRow.KOSTL.Length == 0)
            {
                objColumnsBo.COST_COSTCENTER = string.Empty;
            }
            else
            {
                objColumnsBo.COST_COSTCENTER = vRow.KOSTL;
            }
        }
        objTravelRequestDataContext.Dispose();
        return objColumnsBo;
    }

    public travelrequestcolumnscollectionbo Get_CostCenterSavedDetails(string strTripNo)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();
        foreach (var vRow in objTravelRequestDataContext.sp_bnp_get_costcenter_details_of_travel_request(strTripNo))
        {
            travelrequestcolumnsbo objColumnsBo = new travelrequestcolumnsbo();
            objColumnsBo.COST_SL_NO = vRow.ACCOUNT.ToString();
            objColumnsBo.COST_PERCENTAGE = double.Parse(vRow.PERCENTS.ToString());
            objColumnsBo.COST_COSTCENTER = vRow.KOSTL;

            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }
    public travelrequestcolumnscollectionbo Load_TravelRequestDetails_HR_Payroll(string strTripNo)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();
        foreach (var vRow in objTravelRequestDataContext.sp_bnp_load_travel_request_details_hr_payroll(strTripNo))
        {
            travelrequestcolumnsbo objColumnsBo = new travelrequestcolumnsbo();
            objColumnsBo.TRANS_REQUEST_TYPE = vRow.REQUEST_TYPE;

            if (vRow.DATE_BEG == Convert.ToDateTime("01/01/0001"))
            {
                objColumnsBo.TRANS_START_DATE = Convert.ToDateTime("01/01/0001");
            }
            else
            {
                objColumnsBo.TRANS_START_DATE = Convert.ToDateTime(vRow.DATE_BEG.ToString());
            }
            if (vRow.DATE_END == null)
            {
                objColumnsBo.TRANS_END_DATE = Convert.ToDateTime("01/01/0001");
            }
            else
            {
                objColumnsBo.TRANS_END_DATE = Convert.ToDateTime(vRow.DATE_END);
            }
            if (vRow.LOCATION_BEG == null)
            {
                objColumnsBo.TRANS_START_LOCATION = string.Empty;
            }
            else
            {
                objColumnsBo.TRANS_START_LOCATION = vRow.LOCATION_BEG;
            }
            if (vRow.LOCATION_END == null)
            {
                objColumnsBo.TRANS_END_LOCATION = string.Empty;
            }
            else
            {
                objColumnsBo.TRANS_END_LOCATION = vRow.LOCATION_END;
            }
            if (vRow.COUNTRY_BEG == null)
            {
                objColumnsBo.TRANS_START_COUNTRY = "IN";
            }
            else
            {
                objColumnsBo.TRANS_START_COUNTRY = vRow.COUNTRY_BEG;
            }
            if (vRow.COUNTRY_END == null)
            {
                objColumnsBo.TRANS_END_COUNTRY = "IN";
            }
            else
            {
                objColumnsBo.TRANS_END_COUNTRY = vRow.COUNTRY_END;
            }

            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }
    public List<requisitionbo> GetModeofTransport()
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_mode_of_transport())
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.MODE_OF_TRANSPOPRT_KZPMF = vRow.KZPMF.ToString();
            objColumnsBo.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }

    //------------ with eligibility  --

    public List<requisitionbo> GetVehType(string PERNR)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_veh_type_eligible(PERNR))
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.MODE_OF_TRANSPOPRT_KZPMF = vRow.KZPMF.ToString();
            objColumnsBo.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }

    public List<requisitionbo> GetModeofCategory(string MediumOfTransport)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_medium_of_category(MediumOfTransport))
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.MEDIA_OF_CATEGORY_PKWKL = vRow.PKWKL.ToString();
            objColumnsBo.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }

    //------------ with eligibility  -

    public List<requisitionbo> GetVehCategory(string PERNR, string VehType)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_veh_cat_eligible(PERNR, VehType))
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.MEDIA_OF_CATEGORY_PKWKL = vRow.PKWKL.ToString();
            objColumnsBo.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }

    public List<requisitionbo> GetVehicleName(string MediumOfTransport)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_vehicle_name(MediumOfTransport))
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHNUM);
            objColumnsBo.VEHICLE_NAME_ZZVEHNAM = vRow.ZZVEHNAM.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }

    public List<requisitionbo> GetRegionName()
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_region_name(""))
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.REGION_RGION_FROM = vRow.RGION.ToString();
            objColumnsBo.REGION_TEXT25_FROM = vRow.TEXT25.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }
    //public List<requisitionbo> GetRegionNameBPO(string statecode)
    //{
    //    travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
    //    List<requisitionbo> objList = new List<requisitionbo>();
    //    foreach (var vRow in objTravelRequestDataContext.sp_get_region_name_BPO(statecode))
    //    {
    //        requisitionbo objColumnsBo = new requisitionbo();
    //        objColumnsBo.REGION_RGION_FROM = vRow.RGION.ToString();
    //        objColumnsBo.REGION_TEXT25_FROM = vRow.TEXT25.ToString();
    //        objList.Add(objColumnsBo);
    //    }
    //    objTravelRequestDataContext.Dispose();
    //    return objList;
    //}



    public List<requisitionbo> GetRegionName_Fromplace(string pernr)
    {
        //travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        //List<requisitionbo> objList = new List<requisitionbo>();
        //foreach (var vRow in objTravelRequestDataContext.sp_get_region_name_From_Place())
        //{
        //    requisitionbo objColumnsBo = new requisitionbo();
        //    objColumnsBo.REGION_RGION_FROM = vRow.RGION.ToString();
        //    objColumnsBo.REGION_TEXT25_FROM = vRow.TEXT25.ToString();
        //    objList.Add(objColumnsBo);
        //}
        //objTravelRequestDataContext.Dispose();
        //return objList;
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_perwise_places(pernr))
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.REGION_RGION_FROM = vRow.place_id.ToString();
            objColumnsBo.REGION_TEXT25_FROM = vRow.place_name.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }
    public List<requisitionbo> GetRegionNameFiltered(string dori)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_region_name_filtered(dori))
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.REGION_RGION_FROM = vRow.RGION.ToString();
            objColumnsBo.REGION_TEXT25_FROM = vRow.TEXT25.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }

    public List<requisitionbo> GetExpenseType(string traveltype, string PERNR)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_ExpenseType(traveltype, PERNR))
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.SPKZL = vRow.SPKZL.ToString();
            objColumnsBo.SPTXT = vRow.SPTXT.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }

    public List<requisitionbo> GetExpenseTypeAll(string TravelType)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_ExpenseTypeAll(TravelType))
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.SPKZL = vRow.SPKZL.ToString();
            objColumnsBo.SPTXT = vRow.TEXT_SPKZL.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }

    public List<requisitionbo> GetCurrency()
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_Currency())
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.WAERS = vRow.WAERS.ToString();
            objColumnsBo.LTEXT = vRow.LTEXT.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }


    public List<requisitionbo> GetModeofpayment()
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> objList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_master_load_Modeofpayment())
        {
            requisitionbo objColumnsBo = new requisitionbo();
            objColumnsBo.mode = vRow.mode.ToString();
            objColumnsBo.mode = vRow.mode.ToString();
            objList.Add(objColumnsBo);
        }
        objTravelRequestDataContext.Dispose();
        return objList;
    }
    public List<requisitionbo> GetPurposeOfTravel()
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            List<requisitionbo> objList = new List<requisitionbo>();
            foreach (var vRow in objTravelRequestDataContext.sp_get_purpose_of_travel())
            {
                requisitionbo objColumnsBo = new requisitionbo();
                objColumnsBo.PURPOSE_OF_TRAVEL_ACTIVITY = vRow.ACTIVITY.ToString();
                objColumnsBo.PURPOSE_OF_TRAVEL_NAME = vRow.NAME.ToString();
                objList.Add(objColumnsBo);
            }
            objTravelRequestDataContext.Dispose();
            return objList;
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public int check_designation(requisitionbo objBo)
    {
        int iResult = -1;
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            iResult = objTravelRequestDataContext.sp_check_designation(objBo.FTPT_REQUEST_ID);
            objTravelRequestDataContext.Dispose();

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public List<requisitionbo> Get_emp_details_booked(string PERNR)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_travel_requisition_details_booked(PERNR))
        {
            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = (int)vRow.REQUISITION_ID;
            requisitionboObj.REQ_SEGMENT_ID = (int)vRow.REQ_SEGMENT_ID;
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.ARRIVAL_DATE = vRow.TRIP_END_DATE.ToString();
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.JourneyParticulars = vRow.From + " " + "to" + " " + vRow.To;
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            requisitionboObj.CURRENT_STATUS = vRow.current_status;
            requisitionboObj.TICKET_FARE = vRow.TICKET_FARE;
            requisitionboObj.TravelType = vRow.TravelType;
            requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
            requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
            requisitionboObj.EMPLOYEE_NAME = vRow.EMPLOYEE_NAME;

            if (!string.IsNullOrEmpty(vRow.current_status))
            {
                ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            }

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    public int fun_Delete_requisitionbl(int RequisitionID, int RequisitionsegmentID)
    {
        int iResult = -1;
        try
        {
            travelrequestdalDataContext objcontext = new travelrequestdalDataContext();
            iResult = objcontext.sp_delete_requisition_mst_dtl(RequisitionID, RequisitionsegmentID);

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public List<requisitionbo> Load_ALL_TravelRequestionDetails(string TravelType_Id, string TravelDate, string EmployeeId, string EmployeeName)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_ALL_travel_requisitions(TravelType_Id, TravelDate, EmployeeId, EmployeeName))
        {

            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = (int)vRow.REQUISITION_ID;
            requisitionboObj.REQ_SEGMENT_ID = (int)vRow.REQ_SEGMENT_ID;
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            //requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            //requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
            //requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
            requisitionboObj.EMPLOYEE_NAME = Convert.ToString(vRow.ENAME);
            requisitionboObj.EMPLOYEE_NO = Convert.ToString(vRow.EMPLOYEE_NO);
            requisitionboObj.EMAIL = Convert.ToString(vRow.Email);
            if (vRow.PhoneNumber != null)
            {
                requisitionboObj.PHONE_NUMBER = Convert.ToString(vRow.PhoneNumber);
            }
            requisitionboObj.DESIGNATION = Convert.ToString(vRow.Designation);
            if (!string.IsNullOrEmpty(vRow.current_status))
            {
                ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            }
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }


    public int Create_Group_Requeat_Traveller(groupRequisitionTravelbo objBo)
    {
        int iResult = -1;
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            iResult = objTravelRequestDataContext.sp_create_group_req_Traveller(objBo.memeber_id, objBo.emp_code, objBo.emp_name, objBo.age, objBo.gender, objBo.contact_no, objBo.idproof_no,
                                            objBo.email_id, objBo.fly_no, objBo.REQUISITION_ID, objBo.REQ_SEGMENT_ID, objBo.PROPOSAL_ID, objBo.PRO_SEGMENT_ID, objBo.Ticket_ID);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return iResult;
    }

    public List<groupRequisitionTravelbo> Get_Group_Employee_Traveller(int strRequID)
    {
        List<groupRequisitionTravelbo> requisitionboList = new List<groupRequisitionTravelbo>();
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        foreach (var vRow in objTravelRequestDataContext.sp_Get_group_request_traveller(strRequID))
        {
            groupRequisitionTravelbo requisitionboObj = new groupRequisitionTravelbo();
            requisitionboObj.memeber_id = Convert.ToInt32(vRow.member_id);
            requisitionboObj.emp_code = vRow.emp_code;
            requisitionboObj.emp_name = vRow.emp_name;
            requisitionboObj.age = Convert.ToInt32(vRow.age);
            requisitionboObj.gender = vRow.gender;
            requisitionboObj.contact_no = vRow.contact_no;
            requisitionboObj.idproof_no = vRow.idproof_no;
            requisitionboObj.email_id = vRow.email_id;
            requisitionboObj.fly_no = vRow.fly_no;
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }


    //------------ VALIDATE TRAVEL DATE BEFORE QUEING ------
    public bool Get_validTravalDate(DateTime StrtDt, DateTime EndDt, string Pernr)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            List<ValidTrvlDt> TrvResult = new List<ValidTrvlDt>();
            ValidTrvlDt Obj = new ValidTrvlDt();
            objTravelRequestDataContext.sp_validate_travel_date(StrtDt, EndDt, Pernr);
            foreach (var vRow in objTravelRequestDataContext.sp_validate_travel_date(StrtDt, EndDt, Pernr))
            {
                Obj.ValidResult = Convert.ToBoolean(Convert.ToInt16(vRow.RESULT.ToString()));

            }
            return Obj.ValidResult;
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public List<ClaimViewBo> GetCorpClaimsDisp(int ReqID, int ReqSegID, string PERNR)
    {
        List<ClaimViewBo> requisitionboList = new List<ClaimViewBo>();
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        foreach (var vRow in objTravelRequestDataContext.sp_get_corp_claim_display(ReqID, ReqSegID, PERNR))
        {
            ClaimViewBo requisitionboObj = new ClaimViewBo();
            requisitionboObj.Slno = vRow.slno;
            requisitionboObj.PERNR = vRow.PERNR;
            requisitionboObj.REQUISITION_ID = vRow.REQUISITION_ID.Value;
            requisitionboObj.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID.Value;
            requisitionboObj.DATE1 = vRow.DATE1;
            requisitionboObj.From_Place = vRow.From_Place;
            requisitionboObj.To_Place = vRow.To_Place;
            requisitionboObj.FARE = Convert.ToDouble(vRow.FARE);
            requisitionboObj.Exp_type = vRow.Exp_type;
            requisitionboObj.Currency_Type = vRow.Currency_Type;

            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    //---------------- GET TRAVEL HISTORY -------

    public List<requisitionbo> GetTravel_history(string PERNR)
    {
        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        List<requisitionbo> requisitionboList = new List<requisitionbo>();
        foreach (var vRow in objTravelRequestDataContext.sp_get_travel_history(PERNR))          //sp_get_HOD_travel_requisitions(TravelType_Id, TravelDate, EmployeeId, EmployeeName))
        {
            requisitionbo requisitionboObj = new requisitionbo();
            requisitionboObj.FTPT_REQUEST_ID = (int)vRow.REQUISITION_ID;
            requisitionboObj.REQ_SEGMENT_ID = (int)vRow.REQ_SEGMENT_ID;
            requisitionboObj.CHK = vRow.TravelType;
            requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
            requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
            requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
            requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
            requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
            requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
            requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
            requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
            requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
            requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
            requisitionboObj.VEHICLE_NAME_VHNUM = Convert.ToString(vRow.VEHICLE_NUM);
            requisitionboObj.FLYNUM = vRow.FLYNUM;
            requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
            requisitionboObj.AIRLINE = vRow.AIRLINE;
            requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
            requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
            requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
            requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
            requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
            requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
            requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
            requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
            requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
            requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
            requisitionboObj.REASON_FOR_CANCEL = vRow.REASON_FOR_CANCEL;
            //-----
            requisitionboObj.ARRIVAL_DATE = Convert.ToString(vRow.TRIP_END_DATE);
            requisitionboObj.TRAVEL_VEH_TYPE = vRow.FZTXT;
            requisitionboObj.TRAVEL_VEH_CATEGORY = vRow.TEXT25;
            requisitionboObj.ARRIVAL_TIME = vRow.ARRIVAL_TIME;
            requisitionboObj.REASON = vRow.REASON;

            if (!string.IsNullOrEmpty(vRow.current_status))
            {
                ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                requisitionboObj.CURRENT_STATUS = Convert.ToString(Status);
            }
            requisitionboList.Add(requisitionboObj);
        }
        return requisitionboList;
    }

    //------------------ TRAVEL REQUEST UPDATE ----------------------
    public List<TrvlReqDetails> Get_TravelReqDetails(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_req_update(PERNR))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objColumnsBo.SETTLED = vRow.SETTLED;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }



    public List<TrvlReqDetails> Load_ParticularTravelDetailsNew(string PERNR, string SelectedType, string textSearch)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_LoadParticular_travel_req_updateDeatils(PERNR, SelectedType, textSearch))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objColumnsBo.SETTLED = vRow.SETTLED;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }


    public void Update_TravelReq(TrvlReqDetails objBo)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_update_travel_req_update(objBo.PERNR, objBo.REINR, objBo.DATV1, objBo.DATB1, objBo.CURRENCY, objBo.ADDIT_AMNT);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    //------------------ TRAVEL APPROVE /REJECT ------------------
    public List<TrvlReqDetails> Get_TravelReqAppRejDetails(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_req_app_rej(PERNR))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.CREATED_BY = vRow.Createdby;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }




    public List<TrvlReqDetails> Get_TravelHistoryDetails(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_ViewLoad(PERNR))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.STATUS = vRow.STATUS;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }
    //------------------ MANAGER TRAVEL APPROVE /REJECT ---------------------
    public void TravelReq_MngrAppRej(TrvlReqDetails objBo, string prjid)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_req_apprej(objBo.REINR, objBo.STATUS, objBo.PERNR,prjid); 
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    //------------------ MANAGER TRAVEL CLAIM APPROVE /REJECT ---------------------
    public void TravelClaimReq_MngrAppRej(TrvlReqDetails objBo)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_req_apprej(objBo.ID, objBo.APPROVED_BY, objBo.COMMENTS, objBo.STATUS);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    //---------------------------- CLAIM REQUEST -----------------------
    public void Travel_ClaimReq(TrvlReqDetails objBo, ref bool? status1)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_req(0, objBo.REINR, objBo.WBS_ELEMT, objBo.ACTIVITY, objBo.RCURR, objBo.EXP_TYPE, objBo.S_DATE, objBo.NO_DAYS
                , objBo.DAILY_RATE, objBo.EXPT_AMT, objBo.EXPT_CURR, objBo.EXC_RATE, objBo.RE_AMT, objBo.JUSTIFY, objBo.RECEIPT_FILE, objBo.RECEIPT_FID
                , objBo.RECEIPT_FPATH, objBo.CREATED_ON, objBo.CREATED_BY, objBo.ZLAND, objBo.ZORT1, objBo.TotalAmount, ref status1);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    //------------------------- CLAIM REQUEST VIEW FOR APPROVAL SCREEN --------------------

    //------------------ TRAVEL REQUEST UPDATE ----------------------
    public List<TrvlReqDetails> Get_TravelClaimForApproval(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_forApproval(PERNR, ""))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.ID = vRow.ID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.EXP_TYPE = vRow.EXP_TYPE;
            objColumnsBo.S_DATE = vRow.S_DATE;
            objColumnsBo.NO_DAYS = vRow.NO_DAYS;
            objColumnsBo.DAILY_RATE = vRow.DAILY_RATE;
            objColumnsBo.EXPT_AMT = vRow.EXPT_AMT;
            objColumnsBo.EXPT_CURR = vRow.EXPT_CURR;
            objColumnsBo.EXC_RATE = vRow.EXC_RATE;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.RE_AMT).ToString("0.00");
            objColumnsBo.JUSTIFY = vRow.JUSTIFY;
            objColumnsBo.RECEIPT_FILE = vRow.RECEIPT_FILE;
            objColumnsBo.RECEIPT_FID = vRow.RECEIPT_FID.ToString();
            objColumnsBo.RECEIPT_FPATH = vRow.RECEIPT_FPATH.ToString();
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.ZORT1 = vRow.ZORT1;

            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.ENAME = vRow.ENAME;

            // objColumnsBo.RCURR = vRow.RCURR;

            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.APPROVED_BY1 = vRow.APPROVED_BY1;
            //objColumnsBo.REMARKS1 = vRow.REMARKS1;


            //objColumnsBo.APPROVED_ON2 = vRow.APPROVED_ON2;
            //objColumnsBo.APPROVED_BY2 = vRow.APPROVED_BY2;
            //objColumnsBo.REMARKS2 = vRow.REMARKS2;


            //objColumnsBo.APPROVED_ON3 = vRow.APPROVED_ON3;
            //objColumnsBo.APPROVED_BY3 = vRow.APPROVED_BY3;
            //objColumnsBo.REMARKS3 = vRow.REMARKS3;


            //objColumnsBo.APPROVED_ON4 = vRow.APPROVED_ON4;
            //objColumnsBo.APPROVED_BY4 = vRow.APPROVED_BY4;
            //objColumnsBo.REMARKS4 = vRow.REMARKS4;


            //objColumnsBo.APPROVED_ON5 = vRow.APPROVED_ON5;
            //objColumnsBo.APPROVED_BY5 = vRow.APPROVED_BY5;
            //objColumnsBo.REMARKS5 = vRow.REMARKS5;


            //objColumnsBo.APPROVED_ON6 = vRow.APPROVED_ON6;
            //objColumnsBo.APPROVED_BY6 = vRow.APPROVED_BY6;
            //objColumnsBo.REMARKS6 = vRow.REMARKS6;


            //objColumnsBo.APPROVED_ON7 = vRow.APPROVED_ON7;
            //objColumnsBo.APPROVED_BY7 = vRow.APPROVED_BY7;
            //objColumnsBo.REMARKS7 = vRow.REMARKS7;


            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.TRIP_TOTAL =   vRow.TRIP_TOTAL.ToString() ;
            //objColumnsBo.CURRENCY = vRow.CURRENCY;
            //objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            //objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            //objColumnsBo.CO_AREA = vRow.CO_AREA;
            //objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            //objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            //objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            //objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            //objColumnsBo.NETWORK = vRow.NETWORK;
            //objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }


    public List<TrvlReqDetails> Get_TravelClaimDetails(string reinr, string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_Deatils(reinr, PERNR))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.ID = vRow.ID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.EXP_TYPE = vRow.EXP_TYPE;
            objColumnsBo.S_DATE = vRow.S_DATE;
            objColumnsBo.NO_DAYS = vRow.NO_DAYS;
            objColumnsBo.DAILY_RATE = vRow.DAILY_RATE;
            objColumnsBo.EXPT_AMT = vRow.EXPT_AMT + " ( " + vRow.EXPT_CURR + " ) ";
            objColumnsBo.EXPT_CURR = vRow.EXPT_CURR;
            objColumnsBo.EXC_RATE = vRow.EXC_RATE;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.RE_AMT).ToString("0.00") + " ( " + vRow.RCURR + " ) ";
            objColumnsBo.JUSTIFY = vRow.JUSTIFY;
            objColumnsBo.RECEIPT_FILE = vRow.RECEIPT_FILE;
            objColumnsBo.RECEIPT_FID = vRow.RECEIPT_FID.ToString();
            objColumnsBo.RECEIPT_FPATH = vRow.RECEIPT_FPATH.ToString();
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.ZORT1 = vRow.ZORT1;

            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.DATB1 = vRow.DATB1;
            // objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            // objColumnsBo.APPROVED_BY1 = vRow.APPROVED_BY1;
            // objColumnsBo.REMARKS1 = vRow.REMARKS1;
            //objColumnsBo.APPROVED_ON2 = vRow.APPROVED_ON2;
            //objColumnsBo.APPROVED_BY2 = vRow.APPROVED_BY2;
            //objColumnsBo.REMARKS2 = vRow.REMARKS2;
            //objColumnsBo.APPROVED_ON3 = vRow.APPROVED_ON3;
            //objColumnsBo.APPROVED_BY3 = vRow.APPROVED_BY3;
            //objColumnsBo.REMARKS3 = vRow.REMARKS3;
            //objColumnsBo.APPROVED_ON4 = vRow.APPROVED_ON4;
            //objColumnsBo.APPROVED_BY4 = vRow.APPROVED_BY4;
            //objColumnsBo.REMARKS4 = vRow.REMARKS4;
            //objColumnsBo.APPROVED_ON5 = vRow.APPROVED_ON5;
            //objColumnsBo.APPROVED_BY5 = vRow.APPROVED_BY5;
            //objColumnsBo.REMARKS5 = vRow.REMARKS5;
            // objColumnsBo.APPROVED_ON6 = vRow.APPROVED_ON6;
            //objColumnsBo.APPROVED_BY6 = vRow.APPROVED_BY6;
            //objColumnsBo.REMARKS6 = vRow.REMARKS6;
            // objColumnsBo.APPROVED_ON7 = vRow.APPROVED_ON7;
            //objColumnsBo.APPROVED_BY7 = vRow.APPROVED_BY7;
            //objColumnsBo.REMARKS7 = vRow.REMARKS7;
            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.TRIP_TOTAL = vRow.TRIP_TOTAL.ToString();
            //objColumnsBo.CURRENCY = vRow.CURRENCY;
            //objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            //objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            //objColumnsBo.CO_AREA = vRow.CO_AREA;
            //objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            //objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            //objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            //objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            //objColumnsBo.NETWORK = vRow.NETWORK;
            //objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.STATUS = vRow.STATUS;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }


    public List<TrvlReqDetails> Get_TravelClaimAppRejDetails(int ID,string pernr)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_AppRejDeatils(ID,pernr))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.ID = vRow.ID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.EXP_TYPE = vRow.EXP_TYPE;
            objColumnsBo.S_DATE = vRow.S_DATE;
            objColumnsBo.NO_DAYS = vRow.NO_DAYS;
            objColumnsBo.DAILY_RATE = vRow.DAILY_RATE;
            objColumnsBo.EXPT_AMT = vRow.EXPT_AMT;
            objColumnsBo.EXPT_CURR = vRow.EXPT_CURR;
            objColumnsBo.EXC_RATE = vRow.EXC_RATE;
            objColumnsBo.RE_AMT = vRow.RE_AMT;
            objColumnsBo.JUSTIFY = vRow.JUSTIFY;
            objColumnsBo.RECEIPT_FILE = vRow.RECEIPT_FILE;
            objColumnsBo.RECEIPT_FID = vRow.RECEIPT_FID.ToString();
            objColumnsBo.RECEIPT_FPATH = vRow.RECEIPT_FPATH.ToString();
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.ZORT1 = vRow.ZORT1;

            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.DATB1 = vRow.DATB1;
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
            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.TRIP_TOTAL = vRow.TRIP_TOTAL.ToString();
            //objColumnsBo.CURRENCY = vRow.CURRENCY;
            //objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            //objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            //objColumnsBo.CO_AREA = vRow.CO_AREA;
            //objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            //objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            //objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            //objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            //objColumnsBo.NETWORK = vRow.NETWORK;
            //objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.STATUS = vRow.STATUS;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }



    public List<TrvlReqDetails> Load_ParticularTravelDetails(string emplno, string SelectedType, string textSearch, DateTime Fromdate, DateTime Todate)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_Get_ParticularTravel_Details(emplno, SelectedType, textSearch, Fromdate, Todate))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.STATUS = vRow.STATUS;
            objList.Add(objColumnsBo);

        }
        objTravelReqDataContext.Dispose();
        return objList;
    }



    public travelrequestcolumnscollectionbo Travel_ClaimTotalAmt(string Perner, string reinr, string Rcurr)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();
        foreach (var vRow in objTravelReqDataContext.usp_Get_TotalclaimAmt(Perner, reinr, Rcurr))
        {
            travelrequestcolumnsbo objColumnsBo = new travelrequestcolumnsbo();
            objColumnsBo.ClaimTotalAmount = vRow.TotalRAmount;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public void TravelClaimReq_fivalUpdate(TrvlReqDetails objBo)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_req_fiadvalUpdate(objBo.ID, objBo.REINR, objBo.RECEIPT_FILE, objBo.RECEIPT_FID, objBo.RECEIPT_FPATH, objBo.CREATED_BY);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public List<TrvlReqDetails> Get_TravelAdvanceDetails(string REINR, string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_advance_details(REINR, PERNR))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.ADV_AMOUNT = vRow.ADV_AMOUNT;
            objColumnsBo.ADV_CURR = vRow.ADV_CURR;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }


    ////new travel claim/////

    public List<TrvlReqDetails> Get_TravelReqDetails_forClaim(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_req_updatefor_Claim(PERNR))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objColumnsBo.SETTLED = vRow.SETTLED;
            objColumnsBo.STATUS = vRow.STATUS;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelReqDetails_forClaim_AllCurrentLastmonth(string PERNR,string month)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_req_updatefor_Claim_month(PERNR, month))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objColumnsBo.SETTLED = vRow.SETTLED;
            objColumnsBo.STATUS = vRow.STATUS;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }


    public List<TrvlReqDetails> Get_TravelDetails(string PERNR, string SelectedType, string textSearch)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_SearchTrip_details(PERNR, SelectedType, textSearch))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objColumnsBo.SETTLED = vRow.SETTLED;
            objColumnsBo.STATUS = vRow.STATUS;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public travelrequestcolumnscollectionbo Travel_ClaimTotalAmtNew(string Perner, string reinr, string Rcurr)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();
        foreach (var vRow in objTravelReqDataContext.usp_Get_TotalclaimAmtNew(Perner, reinr, Rcurr))
        {
            travelrequestcolumnsbo objColumnsBo = new travelrequestcolumnsbo();
            objColumnsBo.ClaimTotalAmount = vRow.TotalRAmount;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public void CreateTravelClaim(TrvlReqDetails objBo, ref int? TravelClaim_ID, ref bool? status1)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_reqnew(0, objBo.REINR, objBo.WBS_ELEMT, objBo.ACTIVITY, objBo.RCURR, objBo.TotalAmount,
                objBo.CREATED_ON, objBo.CREATED_BY, objBo.APPROVED_ON1, objBo.APPROVED_BY1, objBo.REMARKS1,
                    objBo.APPROVED_ON2, objBo.APPROVED_BY2, objBo.REMARKS2,
                    objBo.APPROVED_ON3, objBo.APPROVED_BY3, objBo.REMARKS3,
                    objBo.APPROVED_ON4, objBo.APPROVED_BY4, objBo.REMARKS4,
                    objBo.APPROVED_ON5, objBo.APPROVED_BY5, objBo.REMARKS5,
                    objBo.APPROVED_ON6, objBo.APPROVED_BY6, objBo.REMARKS6,
                    objBo.APPROVED_ON7, objBo.APPROVED_BY7, objBo.REMARKS7,
                    objBo.APPROVED_ON8, objBo.APPROVED_BY8, objBo.REMARKS8,
                    objBo.APPROVED_ON9, objBo.APPROVED_BY9, objBo.REMARKS9,
                    objBo.STATUS, ref TravelClaim_ID, ref status1);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }
    public void Travel_ClaimItems(TrvlReqDetails objBo, ref bool? status1)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_AddItems(objBo.CID, objBo.LID, objBo.EXP_TYPE, objBo.S_DATE, objBo.NO_DAYS
                , objBo.DAILY_RATE, objBo.EXPT_AMT, objBo.EXPT_CURR, objBo.EXC_RATE, objBo.RE_AMT, objBo.JUSTIFY, objBo.RECEIPT_FILE, objBo.RECEIPT_FID
                , objBo.RECEIPT_FPATH, objBo.ZLAND, objBo.ZORT1, objBo.DEVIATION_AMT, objBo.DEVIATION_CURR, objBo.DAILY_CURR, ref status1);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public void SaveTravelClaim(TrvlReqDetails objBo, ref int? TravelClaim_ID, ref bool? status1)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_reqSave(objBo.CID, objBo.REINR, objBo.WBS_ELEMT, objBo.ACTIVITY, objBo.RCURR, objBo.TotalAmount,
                objBo.CREATED_ON, objBo.CREATED_BY, objBo.APPROVED_ON1, objBo.APPROVED_BY1, objBo.REMARKS1,
                    objBo.APPROVED_ON2, objBo.APPROVED_BY2, objBo.REMARKS2,
                    objBo.APPROVED_ON3, objBo.APPROVED_BY3, objBo.REMARKS3,
                    objBo.APPROVED_ON4, objBo.APPROVED_BY4, objBo.REMARKS4,
                    objBo.APPROVED_ON5, objBo.APPROVED_BY5, objBo.REMARKS5,
                    objBo.APPROVED_ON6, objBo.APPROVED_BY6, objBo.REMARKS6,
                    objBo.APPROVED_ON7, objBo.APPROVED_BY7, objBo.REMARKS7,
                    objBo.APPROVED_ON8, objBo.APPROVED_BY8, objBo.REMARKS8,
                    objBo.APPROVED_ON9, objBo.APPROVED_BY9, objBo.REMARKS9,
                    objBo.STATUS, ref TravelClaim_ID, ref status1);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public List<TrvlReqDetails> Load_ParticularTravelDetailsNew_forClaims(string PERNR, string SelectedType, string textSearch)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_LoadParticular_travel_req_updateDeatils_forClaim(PERNR, SelectedType, textSearch))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objColumnsBo.SETTLED = vRow.SETTLED;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelReqDetailsSaved(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_req_update_forSaved(PERNR))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objColumnsBo.SETTLED = vRow.SETTLED;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }


    public List<TrvlReqDetails> Get_Traveldetails(string REINR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_Get_Traveldetails(REINR))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objColumnsBo.STATUS = vRow.STATUS;
            objColumnsBo.APPROVED_BY = vRow.APPROVED_BY;
            objColumnsBo.APPROVED_ON = vRow.APPROVED_ON;
            objColumnsBo.APPROVED_BY1N = vRow.ENAME;
           // objColumnsBo.SETTLED = vRow.SETTLED;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_SavedTravelClaimForDetails(string PERNR, string tripno)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_Savedtravel_claim_forDetails(PERNR, tripno))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;
            objColumnsBo.PRJID = vRow.prjid;

            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Load_ClaimDetails(int CID)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_Get_ClaimsTypes(CID))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();

            objColumnsBo.CID = vRow.CID;
            objColumnsBo.LID = vRow.LID;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.EXP_TYPE = vRow.EXP_TYPE;
            objColumnsBo.EXP_TYPE_NAME = vRow.EXP_TYPE;//Newly added
            objColumnsBo.S_DATE = vRow.S_DATE;
            objColumnsBo.NO_DAYS = vRow.NO_DAYS;
            objColumnsBo.DAILY_RATE = vRow.DAILY_RATE;
            objColumnsBo.EXPT_AMT = vRow.EXPT_AMT;
            objColumnsBo.EXPT_CURR = vRow.EXPT_CURR;
            objColumnsBo.EXC_RATE = vRow.EXC_RATE;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.RE_AMT).ToString("0.000");
            objColumnsBo.JUSTIFY = vRow.JUSTIFY;
            objColumnsBo.RECEIPT_FILE = vRow.RECEIPT_FILE;
            objColumnsBo.RECEIPT_FID = vRow.RECEIPT_FID.ToString();
            objColumnsBo.RECEIPT_FIID = vRow.RECEIPT_FID.ToString();
            objColumnsBo.RECEIPT_FPATH = vRow.RECEIPT_FPATH.ToString();
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;

            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.DEVIATION_AMT = decimal.Parse(vRow.DEVIATION_AMT).ToString("0.000");
            objColumnsBo.DEVIATION_CURR = vRow.DEVIATION_CURR;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.CountryID = vRow.CountryID;
            objColumnsBo.RegoinID = vRow.RegionID;
            objColumnsBo.EXPID = vRow.ExpesetypeId;
            objColumnsBo.DAILY_CURR = vRow.DAILY_CURR;
            objColumnsBo.TASK = vRow.TASK;
            objColumnsBo.PRJID = vRow.PROJ_ID;

            objColumnsBo.REINR = vRow.REINR;//newly added on 16-03-2021
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;//newly added on 16-03-2021

            //objColumnsBo.RCURR = vRow.RCURR;

            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.APPROVED_BY1 = vRow.APPROVED_BY1;
            //objColumnsBo.REMARKS1 = vRow.REMARKS1;


            //objColumnsBo.APPROVED_ON2 = vRow.APPROVED_ON2;
            //objColumnsBo.APPROVED_BY2 = vRow.APPROVED_BY2;
            //objColumnsBo.REMARKS2 = vRow.REMARKS2;


            //objColumnsBo.APPROVED_ON3 = vRow.APPROVED_ON3;
            //objColumnsBo.APPROVED_BY3 = vRow.APPROVED_BY3;
            //objColumnsBo.REMARKS3 = vRow.REMARKS3;


            //objColumnsBo.APPROVED_ON4 = vRow.APPROVED_ON4;
            //objColumnsBo.APPROVED_BY4 = vRow.APPROVED_BY4;
            //objColumnsBo.REMARKS4 = vRow.REMARKS4;


            //objColumnsBo.APPROVED_ON5 = vRow.APPROVED_ON5;
            //objColumnsBo.APPROVED_BY5 = vRow.APPROVED_BY5;
            //objColumnsBo.REMARKS5 = vRow.REMARKS5;


            //objColumnsBo.APPROVED_ON6 = vRow.APPROVED_ON6;
            //objColumnsBo.APPROVED_BY6 = vRow.APPROVED_BY6;
            //objColumnsBo.REMARKS6 = vRow.REMARKS6;


            //objColumnsBo.APPROVED_ON7 = vRow.APPROVED_ON7;
            //objColumnsBo.APPROVED_BY7 = vRow.APPROVED_BY7;
            //objColumnsBo.REMARKS7 = vRow.REMARKS7;


            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.TRIP_TOTAL = vRow.TRIP_TOTAL.ToString();
            //objColumnsBo.CURRENCY = vRow.CURRENCY;
            //objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            //objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            //objColumnsBo.CO_AREA = vRow.CO_AREA;
            //objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            //objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            //objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            //objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            //objColumnsBo.NETWORK = vRow.NETWORK;
            //objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public void DeleteFileFromSaveTravelClaim(TrvlReqDetails objBo, ref bool? status1)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_DeleteFile_Saved_travel_claim(objBo.CID, objBo.LID, ref status1);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public List<TrvlReqDetails> Load_SavedClaimDetails(int CID, ref decimal? CalcReAmt, ref string reamtcurr)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_Get_SavedClaimsTypes(CID, ref CalcReAmt, ref reamtcurr))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();

            objColumnsBo.CID = vRow.CID;
            objColumnsBo.LID = vRow.LID;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.EXP_TYPE = vRow.EXP_TYPE;
            objColumnsBo.S_DATE = vRow.S_DATE;
            objColumnsBo.NO_DAYS = vRow.NO_DAYS;
            objColumnsBo.DAILY_RATE = vRow.DAILY_RATE;
            objColumnsBo.EXPT_AMT = vRow.EXPT_AMT;
            objColumnsBo.EXPT_CURR = vRow.EXPT_CURR;
            objColumnsBo.EXC_RATE = vRow.EXC_RATE;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.RE_AMT).ToString("0.000");
            objColumnsBo.JUSTIFY = vRow.JUSTIFY;
            objColumnsBo.RECEIPT_FILE = vRow.RECEIPT_FILE;
            objColumnsBo.RECEIPT_FID = vRow.RECEIPT_FID.ToString();
            objColumnsBo.RECEIPT_FPATH = vRow.RECEIPT_FPATH.ToString();
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;

            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.DEVIATION_AMT = decimal.Parse(vRow.DEVIATION_AMT).ToString("0.000");
            objColumnsBo.DEVIATION_CURR = vRow.DEVIATION_CURR;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.CountryID = vRow.CountryID;
            objColumnsBo.RegoinID = vRow.RegionID;
            objColumnsBo.EXPID = vRow.ExpesetypeId;
            objColumnsBo.DAILY_CURR = vRow.DAILY_CURR;


            // objColumnsBo.RCURR = vRow.RCURR;

            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.APPROVED_BY1 = vRow.APPROVED_BY1;
            //objColumnsBo.REMARKS1 = vRow.REMARKS1;


            //objColumnsBo.APPROVED_ON2 = vRow.APPROVED_ON2;
            //objColumnsBo.APPROVED_BY2 = vRow.APPROVED_BY2;
            //objColumnsBo.REMARKS2 = vRow.REMARKS2;


            //objColumnsBo.APPROVED_ON3 = vRow.APPROVED_ON3;
            //objColumnsBo.APPROVED_BY3 = vRow.APPROVED_BY3;
            //objColumnsBo.REMARKS3 = vRow.REMARKS3;


            //objColumnsBo.APPROVED_ON4 = vRow.APPROVED_ON4;
            //objColumnsBo.APPROVED_BY4 = vRow.APPROVED_BY4;
            //objColumnsBo.REMARKS4 = vRow.REMARKS4;


            //objColumnsBo.APPROVED_ON5 = vRow.APPROVED_ON5;
            //objColumnsBo.APPROVED_BY5 = vRow.APPROVED_BY5;
            //objColumnsBo.REMARKS5 = vRow.REMARKS5;


            //objColumnsBo.APPROVED_ON6 = vRow.APPROVED_ON6;
            //objColumnsBo.APPROVED_BY6 = vRow.APPROVED_BY6;
            //objColumnsBo.REMARKS6 = vRow.REMARKS6;


            //objColumnsBo.APPROVED_ON7 = vRow.APPROVED_ON7;
            //objColumnsBo.APPROVED_BY7 = vRow.APPROVED_BY7;
            //objColumnsBo.REMARKS7 = vRow.REMARKS7;


            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.TRIP_TOTAL =   vRow.TRIP_TOTAL.ToString() ;
            //objColumnsBo.CURRENCY = vRow.CURRENCY;
            //objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            //objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            //objColumnsBo.CO_AREA = vRow.CO_AREA;
            //objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            //objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            //objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            //objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            //objColumnsBo.NETWORK = vRow.NETWORK;
            //objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }


    public void DeleteSaveTravelClaim(TrvlReqDetails objBo, ref bool? status1)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_Delete_Saved_travel_claim(objBo.CID, objBo.LID, ref status1);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public void UpdateCreateTravelClaim(TrvlReqDetails objBo, ref bool? status1)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_reqnewUpdate(objBo.CID, objBo.REINR, objBo.WBS_ELEMT, objBo.ACTIVITY, objBo.RCURR, objBo.TotalAmount,
                objBo.CREATED_ON, objBo.CREATED_BY, objBo.APPROVED_ON1, objBo.APPROVED_BY1, objBo.REMARKS1,
                    objBo.APPROVED_ON2, objBo.APPROVED_BY2, objBo.REMARKS2,
                    objBo.APPROVED_ON3, objBo.APPROVED_BY3, objBo.REMARKS3,
                    objBo.APPROVED_ON4, objBo.APPROVED_BY4, objBo.REMARKS4,
                    objBo.APPROVED_ON5, objBo.APPROVED_BY5, objBo.REMARKS5,
                    objBo.APPROVED_ON6, objBo.APPROVED_BY6, objBo.REMARKS6,
                    objBo.APPROVED_ON7, objBo.APPROVED_BY7, objBo.REMARKS7,
                    objBo.APPROVED_ON8, objBo.APPROVED_BY8, objBo.REMARKS8,
                    objBo.APPROVED_ON9, objBo.APPROVED_BY9, objBo.REMARKS9,
                    objBo.STATUS, ref status1);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }


    public void UpdateSavedReject_Travel_ClaimItems(TrvlReqDetails objBo, ref bool? status1, string fileupdate)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_UpdateSavedReject_travel_claim_AddItems(objBo.CID, objBo.LID, objBo.EXP_TYPE, objBo.S_DATE, objBo.NO_DAYS
                , objBo.DAILY_RATE, objBo.EXPT_AMT, objBo.EXPT_CURR, objBo.EXC_RATE, objBo.RE_AMT, objBo.JUSTIFY, objBo.RECEIPT_FILE, objBo.RECEIPT_FID
                , objBo.RECEIPT_FPATH, objBo.ZLAND, objBo.ZORT1, objBo.DEVIATION_AMT, objBo.DEVIATION_CURR, objBo.DAILY_CURR, ref status1, fileupdate.Trim());
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }


    public List<TrvlReqDetails> Load_ParticularTravelDetailsNew_forSaved(string PERNR, string SelectedType, string textSearch)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_LoadParticular_travel_req_updateDeatils_forsaved(PERNR, SelectedType, textSearch))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objColumnsBo.SETTLED = vRow.SETTLED;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelClaimAllDetails(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_allDEtails(PERNR, ""))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.DATB1=vRow.DATB1;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }
    public List<TrvlReqDetails> Load_ClaimStatusDetails(int CID)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_Get_ClaimsTypesStatus(CID))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();

            objColumnsBo.CID = CID;
            objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            objColumnsBo.APPROVED_BY1 = vRow.APPROVED_BY1 == null ? "" : vRow.APPROVED_BY1;
            objColumnsBo.APPROVED_BY1N = vRow.ENAME1;
            objColumnsBo.REMARKS1 = vRow.REMARKS1;
            objColumnsBo.APPROVED_ON2 = vRow.APPROVED_ON2;
            objColumnsBo.APPROVED_BY2 = vRow.APPROVED_BY2 == null ? "" : vRow.APPROVED_BY2;
            objColumnsBo.APPROVED_BY2N = vRow.ENAME2;
            objColumnsBo.REMARKS2 = vRow.REMARKS2;
            objColumnsBo.APPROVED_ON3 = vRow.APPROVED_ON3;
            objColumnsBo.APPROVED_BY3 = vRow.APPROVED_BY3 == null ? "" : vRow.APPROVED_BY3;
            objColumnsBo.APPROVED_BY3N = vRow.ENAME3;
            objColumnsBo.REMARKS3 = vRow.REMARKS3;
            objColumnsBo.APPROVED_ON4 = vRow.APPROVED_ON4;
            objColumnsBo.APPROVED_BY4 = vRow.APPROVED_BY4 == null ? "" : vRow.APPROVED_BY4;
            objColumnsBo.APPROVED_BY4N = vRow.ENAME4;
            objColumnsBo.REMARKS4 = vRow.REMARKS4;
            objColumnsBo.APPROVED_ON5 = vRow.APPROVED_ON5;
            objColumnsBo.APPROVED_BY5 = vRow.APPROVED_BY5 == null ? "" : vRow.APPROVED_BY5;
            objColumnsBo.APPROVED_BY5N = vRow.ENAME5;
            objColumnsBo.REMARKS5 = vRow.REMARKS5;
            objColumnsBo.APPROVED_ON6 = vRow.APPROVED_ON6;
            objColumnsBo.APPROVED_BY6 = vRow.APPROVED_BY6 == null ? "" : vRow.APPROVED_BY6;
            objColumnsBo.APPROVED_BY6N = vRow.ENAME6;
            objColumnsBo.REMARKS6 = vRow.REMARKS6;
            objColumnsBo.APPROVED_ON7 = vRow.APPROVED_ON7;
            objColumnsBo.APPROVED_BY7 = vRow.APPROVED_BY7 == null ? "" : vRow.APPROVED_BY7;
            objColumnsBo.APPROVED_BY7N = vRow.ENAME7;
            objColumnsBo.REMARKS7 = vRow.REMARKS7;
            objColumnsBo.APPROVED_ON8 = vRow.APPROVED_ON8;
            objColumnsBo.APPROVED_BY8 = vRow.APPROVED_BY8 == null ? "" : vRow.APPROVED_BY8;
            objColumnsBo.REMARKS8 = vRow.REMARKS8;
            objColumnsBo.APPROVED_ON9 = vRow.APPROVED_ON9;
            objColumnsBo.APPROVED_BY9 = vRow.APPROVED_BY9 == null ? "" : vRow.APPROVED_BY9;
            objColumnsBo.REMARKS9 = vRow.REMARKS9;
            objColumnsBo.STATUS = vRow.STATUS;
            objColumnsBo.BUKRS = vRow.BUKRS;


            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }
    public List<TrvlReqDetails> Load_ParticularTravelDetailsforFinance(string PERNR, string SelectedType, string textSearch)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_Get_ParticularTravel_Details_ForFinance(PERNR, SelectedType, textSearch))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.DATB1 = vRow.DATB1;

            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }
    public List<TrvlReqDetails> Get_TravelClaimPendingMnrAppDetails(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_PendingMngrApp(PERNR))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.DATB1 = vRow.DATB1;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelClaimPendingFinAppDetails(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_PendingFinApp(PERNR))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.DATB1 = vRow.DATB1;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelClaimForDetailsNew(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_forDetailsNew(PERNR, ""))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;

            // objColumnsBo.RCURR = vRow.RCURR;

            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.APPROVED_BY1 = vRow.APPROVED_BY1;
            //objColumnsBo.REMARKS1 = vRow.REMARKS1;


            //objColumnsBo.APPROVED_ON2 = vRow.APPROVED_ON2;
            //objColumnsBo.APPROVED_BY2 = vRow.APPROVED_BY2;
            //objColumnsBo.REMARKS2 = vRow.REMARKS2;


            //objColumnsBo.APPROVED_ON3 = vRow.APPROVED_ON3;
            //objColumnsBo.APPROVED_BY3 = vRow.APPROVED_BY3;
            //objColumnsBo.REMARKS3 = vRow.REMARKS3;


            //objColumnsBo.APPROVED_ON4 = vRow.APPROVED_ON4;
            //objColumnsBo.APPROVED_BY4 = vRow.APPROVED_BY4;
            //objColumnsBo.REMARKS4 = vRow.REMARKS4;


            //objColumnsBo.APPROVED_ON5 = vRow.APPROVED_ON5;
            //objColumnsBo.APPROVED_BY5 = vRow.APPROVED_BY5;
            //objColumnsBo.REMARKS5 = vRow.REMARKS5;


            //objColumnsBo.APPROVED_ON6 = vRow.APPROVED_ON6;
            //objColumnsBo.APPROVED_BY6 = vRow.APPROVED_BY6;
            //objColumnsBo.REMARKS6 = vRow.REMARKS6;


            //objColumnsBo.APPROVED_ON7 = vRow.APPROVED_ON7;
            //objColumnsBo.APPROVED_BY7 = vRow.APPROVED_BY7;
            //objColumnsBo.REMARKS7 = vRow.REMARKS7;


            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.TRIP_TOTAL =   vRow.TRIP_TOTAL.ToString() ;
            //objColumnsBo.CURRENCY = vRow.CURRENCY;
            //objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            //objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            //objColumnsBo.CO_AREA = vRow.CO_AREA;
            //objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            //objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            //objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            //objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            //objColumnsBo.NETWORK = vRow.NETWORK;
            //objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelClaimForDetailsNew_AllCurrentLastmonth(string PERNR,string month)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_forDetailsNew_month(PERNR, "", month))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Load_ParticularTravelDetailsforEmployee(string PERNR, string SelectedType, string textSearch)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_Get_ParticularTravel_Details_ForEmployee(PERNR, SelectedType, textSearch))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;

            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelClaimForApprovalNew(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_forApprovalNew(PERNR, ""))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.DATV1 = (DateTime)vRow.DATV1;
            objColumnsBo.DATB1 = (DateTime)vRow.DATB1;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable";
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;

            // objColumnsBo.RCURR = vRow.RCURR;

            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.APPROVED_BY1 = vRow.APPROVED_BY1;
            //objColumnsBo.REMARKS1 = vRow.REMARKS1;


            //objColumnsBo.APPROVED_ON2 = vRow.APPROVED_ON2;
            //objColumnsBo.APPROVED_BY2 = vRow.APPROVED_BY2;
            //objColumnsBo.REMARKS2 = vRow.REMARKS2;


            //objColumnsBo.APPROVED_ON3 = vRow.APPROVED_ON3;
            //objColumnsBo.APPROVED_BY3 = vRow.APPROVED_BY3;
            //objColumnsBo.REMARKS3 = vRow.REMARKS3;


            //objColumnsBo.APPROVED_ON4 = vRow.APPROVED_ON4;
            //objColumnsBo.APPROVED_BY4 = vRow.APPROVED_BY4;
            //objColumnsBo.REMARKS4 = vRow.REMARKS4;


            //objColumnsBo.APPROVED_ON5 = vRow.APPROVED_ON5;
            //objColumnsBo.APPROVED_BY5 = vRow.APPROVED_BY5;
            //objColumnsBo.REMARKS5 = vRow.REMARKS5;


            //objColumnsBo.APPROVED_ON6 = vRow.APPROVED_ON6;
            //objColumnsBo.APPROVED_BY6 = vRow.APPROVED_BY6;
            //objColumnsBo.REMARKS6 = vRow.REMARKS6;


            //objColumnsBo.APPROVED_ON7 = vRow.APPROVED_ON7;
            //objColumnsBo.APPROVED_BY7 = vRow.APPROVED_BY7;
            //objColumnsBo.REMARKS7 = vRow.REMARKS7;


            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.TRIP_TOTAL =   vRow.TRIP_TOTAL.ToString() ;
            //objColumnsBo.CURRENCY = vRow.CURRENCY;
            //objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            //objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            //objColumnsBo.CO_AREA = vRow.CO_AREA;
            //objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            //objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            //objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            //objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            //objColumnsBo.NETWORK = vRow.NETWORK;
            //objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelClaimForApprovalNew_AllCurrentLastmonth(string PERNR,string month)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_forApprovalNew_month(PERNR, "", month))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.DATV1 = (DateTime)vRow.DATV1;
            objColumnsBo.DATB1 = (DateTime)vRow.DATB1;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable";
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;

           
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelClaimForApprovalNewMC(string PERNR)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_forApprovalNewMC(PERNR, ""))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.DATV1 = (DateTime)vRow.DATV1;
            objColumnsBo.DATB1 = (DateTime)vRow.DATB1;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable";
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;

            // objColumnsBo.RCURR = vRow.RCURR;

            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.APPROVED_BY1 = vRow.APPROVED_BY1;
            //objColumnsBo.REMARKS1 = vRow.REMARKS1;


            //objColumnsBo.APPROVED_ON2 = vRow.APPROVED_ON2;
            //objColumnsBo.APPROVED_BY2 = vRow.APPROVED_BY2;
            //objColumnsBo.REMARKS2 = vRow.REMARKS2;


            //objColumnsBo.APPROVED_ON3 = vRow.APPROVED_ON3;
            //objColumnsBo.APPROVED_BY3 = vRow.APPROVED_BY3;
            //objColumnsBo.REMARKS3 = vRow.REMARKS3;


            //objColumnsBo.APPROVED_ON4 = vRow.APPROVED_ON4;
            //objColumnsBo.APPROVED_BY4 = vRow.APPROVED_BY4;
            //objColumnsBo.REMARKS4 = vRow.REMARKS4;


            //objColumnsBo.APPROVED_ON5 = vRow.APPROVED_ON5;
            //objColumnsBo.APPROVED_BY5 = vRow.APPROVED_BY5;
            //objColumnsBo.REMARKS5 = vRow.REMARKS5;


            //objColumnsBo.APPROVED_ON6 = vRow.APPROVED_ON6;
            //objColumnsBo.APPROVED_BY6 = vRow.APPROVED_BY6;
            //objColumnsBo.REMARKS6 = vRow.REMARKS6;


            //objColumnsBo.APPROVED_ON7 = vRow.APPROVED_ON7;
            //objColumnsBo.APPROVED_BY7 = vRow.APPROVED_BY7;
            //objColumnsBo.REMARKS7 = vRow.REMARKS7;


            //objColumnsBo.APPROVED_ON1 = vRow.APPROVED_ON1;
            //objColumnsBo.TRIP_TOTAL =   vRow.TRIP_TOTAL.ToString() ;
            //objColumnsBo.CURRENCY = vRow.CURRENCY;
            //objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            //objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            //objColumnsBo.CO_AREA = vRow.CO_AREA;
            //objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            //objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            //objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            //objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            //objColumnsBo.NETWORK = vRow.NETWORK;
            //objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelClaimForApprovalNewMC_AllCurrentLastmonth(string PERNR,string month)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_forApprovalNewMC_month(PERNR, "", month))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.DATV1 = (DateTime)vRow.DATV1;
            objColumnsBo.DATB1 = (DateTime)vRow.DATB1;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable";
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;

           
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public void TravelClaimReq_fivalUpdate1(TrvlReqDetails objBo)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_req_fiadvalUpdate1(objBo.CID, objBo.LID, objBo.RECEIPT_FILE, objBo.RECEIPT_FID, objBo.RECEIPT_FPATH, objBo.CREATED_BY);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public void TravelClaimReq_fivalDelete(TrvlReqDetails objBo)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_req_fiadvalDelete(objBo.CID, objBo.LID, objBo.RECEIPT_FILE, objBo.RECEIPT_FID, objBo.RECEIPT_FPATH, objBo.CREATED_BY);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public void TravelClaimReq_fivalUpdateExptype(TrvlReqDetails objBo)
    {
        try
        {

            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_req_fiadvalUpdateExpTyp(objBo.CID, objBo.LID, objBo.DAILY_RATE, objBo.EXP_TYPE, objBo.DAILY_CURR, objBo.DEVIATION_AMT, objBo.DEVIATION_CURR);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    public void Update_TravelClaim_Status(TrvlReqDetails objBo, ref bool? Status)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_req_apprej_new(objBo.CID, objBo.APPROVED_BY1, objBo.COMMENTS, objBo.STATUS, ref Status);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }


    //public List<TrvlReqDetails> Load_ParticularTravelDetailsforAppRej(string PERNR, string SelectedType, string textSearch)
    //{
    //    travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
    //    List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
    //    foreach (var vRow in objTravelReqDataContext.usp_Get_ParticularTravel_Details_ForApproval(PERNR, SelectedType, textSearch))
    //    {
    //        TrvlReqDetails objColumnsBo = new TrvlReqDetails();
    //        objColumnsBo.CID = vRow.CID;
    //        objColumnsBo.REINR = vRow.REINR;
    //        objColumnsBo.DATV1 = (DateTime)vRow.DATV1;
    //        objColumnsBo.DATB1 = (DateTime)vRow.DATB1;
    //        objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
    //        objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
    //        objColumnsBo.RCURR = vRow.RCURR;
    //        objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
    //        objColumnsBo.CREATED_ON = vRow.CREATED_ON;
    //        objColumnsBo.CREATED_BY = vRow.CREATED_BY;
    //        objColumnsBo.ENAME = vRow.ENAME;

    //        objList.Add(objColumnsBo);
    //    }
    //    objTravelReqDataContext.Dispose();
    //    return objList;
    //}


    public List<TrvlReqDetails> Load_ParticularTravelDetailsforApprover(string PERNR, string SelectedType, string textSearch)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_Get_ParticularTravel_Details_ForApprover(PERNR, SelectedType, textSearch))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.DATV1 = (DateTime)vRow.DATV1;
            objColumnsBo.DATB1 = (DateTime)vRow.DATB1;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;

            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }


    public List<TrvlReqDetails> Load_TravelClaimDetailsforAppRej(string PERNR, string SelectedType, string textSearch)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_Get_Travelclaims_Details_ForApproval(PERNR, SelectedType, textSearch))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.DATV1 = (DateTime)vRow.DATV1;
            objColumnsBo.DATB1 = (DateTime)vRow.DATB1;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;

            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public void Update_TravelClaim_Approvers(TrvlReqDetails objBo, ref bool? Status)
    {
        try
        {
            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
            objTravelRequestDataContext.usp_travel_claim_UpdateApprovers(objBo.CID, objBo.APPROVED_BY1, objBo.APPROVED_BY2, objBo.APPROVED_BY3, objBo.APPROVED_BY4,
                objBo.APPROVED_BY5, objBo.APPROVED_BY6, objBo.APPROVED_BY7, objBo.APPROVED_BY8, objBo.APPROVED_BY9, ref Status);
            objTravelRequestDataContext.Dispose();
        }
        catch (Exception Ex)
        { throw Ex; }
    }

    ////////////
    public List<TrvlReqDetails> Get_TravelReqAppRejDetails_ID(string ID)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_req_app_rej_from_ID(ID))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2;
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.CREATED_BY = vRow.Createdby;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelClaimForDetailsNew_AllCurrentLastmonth_Rpager(string PERNR, string month, int Pindx, int Psz, ref int? rCnt)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_forDetailsNew_month_Rpager(PERNR, "", month, Pindx, Psz, ref rCnt))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.Rnum = vRow.RowNumber;
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable"; ;
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelReqDetails_forClaim_AllCurrentLastmonth_Rpager(string PERNR, string month, int Pindx, int Psz, ref int? rCnt)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_req_updatefor_Claim_month_Rpager(PERNR, month, Pindx, Psz, ref rCnt))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.Rnum = vRow.RowNumber;
            objColumnsBo.PERNR = vRow.PERNR;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.MOLGA = vRow.MOLGA;
            objColumnsBo.MOREI = vRow.MOREI;
            objColumnsBo.SCHEM = vRow.SCHEM;
            objColumnsBo.KZREA = vRow.KZREA.ToString() == "A" ? "Domestic" : "International";
            objColumnsBo.ZORT1 = vRow.ZORT1;
            objColumnsBo.ZLAND = vRow.ZLAND;
            objColumnsBo.KUNDE = vRow.KUNDE;
            objColumnsBo.DATC1 = vRow.DATC1.ToString();
            objColumnsBo.DATV1 = vRow.DATV1;
            objColumnsBo.UHRV1 = vRow.UHRV1;
            objColumnsBo.DATC2 = vRow.DATC2.ToString();
            objColumnsBo.DATB1 = vRow.DATB1;
            objColumnsBo.UHRB1 = vRow.UHRB1;
            objColumnsBo.PERIO = vRow.PERIO;
            objColumnsBo.ADDIT_AMNT = decimal.Parse(vRow.ADDIT_AMNT.ToString());
            objColumnsBo.SUM_REIMBU = decimal.Parse(vRow.SUM_REIMBU.ToString());
            objColumnsBo.SUM_ADVANC = decimal.Parse(vRow.SUM_ADVANC.ToString());
            objColumnsBo.SUM_PAYOUT = decimal.Parse(vRow.SUM_PAYOUT.ToString());
            objColumnsBo.SUM_PAIDCO = decimal.Parse(vRow.SUM_PAIDCO.ToString());
            objColumnsBo.TRIP_TOTAL = decimal.Parse(vRow.TRIP_TOTAL.ToString());
            objColumnsBo.CURRENCY = vRow.CURRENCY;
            objColumnsBo.COMP_CODE = vRow.COMP_CODE;
            objColumnsBo.BUS_AREA = vRow.BUS_AREA;
            objColumnsBo.CO_AREA = vRow.CO_AREA;
            objColumnsBo.COSTCENTER = vRow.COSTCENTER;
            objColumnsBo.INTERNAL_ORDER = vRow.INTERNAL_ORDER;
            objColumnsBo.COST_OBJ = vRow.COST_OBJ;
            objColumnsBo.WBS_ELEMT = vRow.WBS_ELEMT;
            objColumnsBo.NETWORK = vRow.NETWORK;
            objColumnsBo.ACTIVITY = vRow.ACTIVITY;
            objColumnsBo.PURPOSE = vRow.PURPOSE;
            objColumnsBo.SETTLED = vRow.SETTLED;
            objColumnsBo.STATUS = vRow.STATUS;
            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }

    public List<TrvlReqDetails> Get_TravelClaimForApprovalNew_AllCurrentLastmonth_Rpager(string PERNR, string month, int Pindx, int Psz, ref int? rCnt)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_forApprovalNew_month_Rpager(PERNR, "", month, Pindx, Psz, ref rCnt))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.Rnum = vRow.RowNumber;
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.DATV1 = (DateTime)vRow.DATV1;
            objColumnsBo.DATB1 = (DateTime)vRow.DATB1;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable";
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;

            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }


    public List<TrvlReqDetails> Get_TravelClaimForApprovalNewMC_AllCurrentLastmonth_Rpager(string PERNR, string month, int Pindx, int Psz, ref int? rCnt)
    {
        travelrequestdalDataContext objTravelReqDataContext = new travelrequestdalDataContext();
        List<TrvlReqDetails> objList = new List<TrvlReqDetails>();
        foreach (var vRow in objTravelReqDataContext.usp_get_travel_claim_forApprovalNewMC_month_Rpager(PERNR, "", month, Pindx, Psz, ref rCnt))
        {
            TrvlReqDetails objColumnsBo = new TrvlReqDetails();
            objColumnsBo.Rnum = vRow.RowNumber;
            objColumnsBo.CID = vRow.CID;
            objColumnsBo.REINR = vRow.REINR;
            objColumnsBo.DATV1 = (DateTime)vRow.DATV1;
            objColumnsBo.DATB1 = (DateTime)vRow.DATB1;
            objColumnsBo.WBS_ELEMT = vRow.PROJ_ID;
            objColumnsBo.ACTIVITY = vRow.TASK.ToString().Trim() == "B" ? "Billable" : "Non-Billable";
            objColumnsBo.RCURR = vRow.RCURR;
            objColumnsBo.RE_AMT = decimal.Parse(vRow.TOTAL_REAMT).ToString("0.000");
            objColumnsBo.CREATED_ON = vRow.CREATED_ON;
            objColumnsBo.CREATED_BY = vRow.CREATED_BY;
            objColumnsBo.ENAME = vRow.ENAME;
            objColumnsBo.STATUS = vRow.STATUS;

            objList.Add(objColumnsBo);
        }
        objTravelReqDataContext.Dispose();
        return objList;
    }
}
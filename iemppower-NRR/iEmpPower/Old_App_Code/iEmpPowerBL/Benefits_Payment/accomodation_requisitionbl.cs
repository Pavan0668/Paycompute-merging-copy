using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Local_requisition;
namespace iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment
{
    public class accomodation_requisitionbl
    {
        public int Create_accomodation_requisitionbl(accomodation_requisitionbo objbo)
        {
            int iResult = -1;
            try
            {
                Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
                iResult = objcontext.sp_create_accomodation_requisition(objbo.REQUEST_ID, objbo.REQ_SEGMENT_ID_FROM_TRAVEL_REQUEST, objbo.EMPLOYEE_NO, objbo.Date_of_travel, objbo.Check_in_date, objbo.Check_out_date,
                objbo.Arrival_time, objbo.Departure_time, objbo.HotelPlaceCity, objbo.HOTEL_CAT_CODE, objbo.ROOM_CODE, objbo.HOTEL_CODE, objbo.Additional_service, objbo.current_status, objbo.STATUS_UPDATED_BY, objbo.CRAETEDBY, objbo.CREATEDON,
                objbo.status, objbo.Remarks);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return iResult;
        }

        public int Create_Separate_accomodation_Proposalbl(accomodation_requisitionbo objbo)
        {
            int iResult = -1;
            try
            {
                Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
                iResult = objcontext.sp_create_Sep_accomodation_proposal(objbo.Accommadation_req_id, objbo.REQUEST_ID, objbo.REQ_SEGMENT_ID_FROM_TRAVEL_REQUEST, objbo.EMPLOYEE_NO, objbo.Date_of_travel, objbo.Check_in_date, objbo.Check_out_date,
                objbo.Arrival_time, objbo.Departure_time, objbo.HOTEL_CAT_CODE, objbo.ROOM_CODE, objbo.HOTEL_CODE, objbo.Additional_service, objbo.current_status, objbo.STATUS_UPDATED_BY, objbo.CRAETEDBY, objbo.CREATEDON,
                objbo.status, objbo.Remarks);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return iResult;
        }


        public int Delete_accomodation_requisitionbl(int RequisitionID, int RequisitionsegmentID, int TravelType)
        {
            int iResult = -1;
            try
            {
                Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
                iResult = objcontext.sp_delete_accomodation_requisition(RequisitionID, RequisitionsegmentID, TravelType);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return iResult;
        }

        public List<accomodation_requisitionbo> Get_Accommodation_Details(int RequisitionID, int RequisitionSegmentID)
        {
            int IntAccomID = 1;//This is used for the updated the particular accomodation.
            List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
            Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_accommodation_details(RequisitionID, RequisitionSegmentID))
            {
                accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
                accomodation_requisitionBO.Accommadation_req_id = vRow.Accommadation_req_id;// IntAccomID;
                accomodation_requisitionBO.Additional_service = vRow.Additional_service;
                accomodation_requisitionBO.Arrival_time = vRow.Arrival_time;
                accomodation_requisitionBO.Check_in_date = vRow.Check_in_date;
                accomodation_requisitionBO.Check_out_date = vRow.Check_out_date;
                accomodation_requisitionBO.current_status = vRow.current_status;
                accomodation_requisitionBO.Departure_time = vRow.Departure_time;
                accomodation_requisitionBO.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                accomodation_requisitionBO.HotelPlaceCity = vRow.HOTEL_PLACE_CITY;
                accomodation_requisitionBO.HOTEL_CAT_CODE = vRow.HOTEL_CAT_CODE;
                accomodation_requisitionBO.HOTEL_CODE = vRow.HOTEL_CODE; ;
                accomodation_requisitionBO.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID;
                accomodation_requisitionBO.REQUEST_ID = vRow.REQUISITION_ID;
                accomodation_requisitionBO.Remarks = vRow.REMARKS;
                accomodation_requisitionBO.RoomCategoryName = vRow.ROOM_CODE;
                accomodation_requisitionBO.ROOM_CODE = vRow.ROOM_CODE;
                accomodation_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                accomodation_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                //if (!string.IsNullOrEmpty(vRow.current_status))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                //    accomodation_requisitionBO.current_status = Convert.ToString(Status);
                //}
                IntAccomID++;
                AccommodationRequisitionboList.Add(accomodation_requisitionBO);
            }
            return AccommodationRequisitionboList;
        }

        public List<accomodation_requisitionbo> Get_OutAccomm_Details_TicketBooked(int RequisitionID, int RequisitionSegmentID)
        {
            int IntAccomID = 1;//This is used for the updated the particular accomodation.
            List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
            Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_OutLocalAccomm_requisition_details(RequisitionID, RequisitionSegmentID))
            {
                accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
                accomodation_requisitionBO.Accommadation_req_id = vRow.Accommadation_req_id;// IntAccomID;
                accomodation_requisitionBO.Additional_service = vRow.Additional_service;
                accomodation_requisitionBO.Arrival_time = vRow.Arrival_time;
                accomodation_requisitionBO.Check_in_date = vRow.Check_in_date;
                accomodation_requisitionBO.Check_out_date = vRow.Check_out_date;
                accomodation_requisitionBO.current_status = vRow.current_status;
                accomodation_requisitionBO.Departure_time = vRow.Departure_time;
                accomodation_requisitionBO.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                accomodation_requisitionBO.HotelPlaceCity = vRow.HOTEL_PLACE_CITY;
                accomodation_requisitionBO.HOTEL_CAT_CODE = vRow.HOTEL_CAT_CODE;
                accomodation_requisitionBO.HOTEL_CODE = vRow.HOTEL_CODE; ;
                accomodation_requisitionBO.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID;
                accomodation_requisitionBO.REQUEST_ID = vRow.REQUISITION_ID;
                accomodation_requisitionBO.Remarks = vRow.REMARKS;
                accomodation_requisitionBO.RoomCategoryName = vRow.ROOM_CODE;
                accomodation_requisitionBO.ROOM_CODE = vRow.ROOM_CODE;
                accomodation_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                accomodation_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                if (!string.IsNullOrEmpty(vRow.current_status))
                {
                    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                    accomodation_requisitionBO.current_status = Convert.ToString(Status);
                }
                IntAccomID++;
                AccommodationRequisitionboList.Add(accomodation_requisitionBO);
            }
            return AccommodationRequisitionboList;
        }

        public List<accomodation_requisitionbo> Get_TravelReq_Accomm_Details_TicketBooked(int RequisitionID, int RequisitionSegmentID)
        {
            int IntAccomID = 1;//This is used for the updated the particular accomodation.
            List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
            Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_TravelReq_accommodation_details(RequisitionID, RequisitionSegmentID))
            {
                accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
                accomodation_requisitionBO.Accommadation_req_id = vRow.Accommadation_req_id;// IntAccomID;
                accomodation_requisitionBO.Additional_service = vRow.Additional_service;
                accomodation_requisitionBO.Arrival_time = vRow.Arrival_time;
                accomodation_requisitionBO.Check_in_date = vRow.Check_in_date;
                accomodation_requisitionBO.Check_out_date = vRow.Check_out_date;
                accomodation_requisitionBO.current_status = vRow.current_status;
                accomodation_requisitionBO.Departure_time = vRow.Departure_time;
                accomodation_requisitionBO.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                accomodation_requisitionBO.HotelPlaceCity = vRow.HOTEL_PLACE_CITY;
                accomodation_requisitionBO.HOTEL_CAT_CODE = vRow.HOTEL_CAT_CODE;
                accomodation_requisitionBO.HOTEL_CODE = vRow.HOTEL_CODE; ;
                accomodation_requisitionBO.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID;
                accomodation_requisitionBO.REQUEST_ID = vRow.REQUISITION_ID;
                accomodation_requisitionBO.Remarks = vRow.REMARKS;
                accomodation_requisitionBO.RoomCategoryName = vRow.ROOM_CODE;
                accomodation_requisitionBO.ROOM_CODE = vRow.ROOM_CODE;
                accomodation_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                accomodation_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                if (!string.IsNullOrEmpty(vRow.current_status))
                {
                    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                    accomodation_requisitionBO.current_status = Convert.ToString(Status);
                }
                IntAccomID++;
                AccommodationRequisitionboList.Add(accomodation_requisitionBO);
            }
            return AccommodationRequisitionboList;
        }

        public List<accomodation_requisitionbo> Get_Accommodation_Separate_Details(int RequisitionID, int RequisitionSegmentID, string strEmployeeID, string strStatus)
        {
            int IntAccomID = 1;//This is used for the updated the particular accomodation.
            List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
            Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_accommo_SeparateReq_details(RequisitionID, RequisitionSegmentID, strEmployeeID, strStatus))
            {
                accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
                accomodation_requisitionBO.Accommadation_req_id = vRow.Accommadation_req_id;// IntAccomID;
                accomodation_requisitionBO.Additional_service = vRow.Additional_service;
                accomodation_requisitionBO.Arrival_time = vRow.Arrival_time;
                accomodation_requisitionBO.Check_in_date = vRow.Check_in_date;
                accomodation_requisitionBO.Check_out_date = vRow.Check_out_date;
                accomodation_requisitionBO.current_status = vRow.current_status;
                accomodation_requisitionBO.Departure_time = vRow.Departure_time;
                accomodation_requisitionBO.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                accomodation_requisitionBO.HOTEL_CAT_CODE = vRow.HOTEL_CAT_CODE;
                accomodation_requisitionBO.HOTEL_CODE = vRow.HOTEL_CODE;
                accomodation_requisitionBO.RoomCategoryName = vRow.ROOM_CODE;
                accomodation_requisitionBO.ROOM_CODE = vRow.ROOM_CODE;
                accomodation_requisitionBO.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID;
                accomodation_requisitionBO.REQUEST_ID = vRow.REQUISITION_ID;
                accomodation_requisitionBO.Remarks = vRow.REMARKS;
                accomodation_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                accomodation_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                accomodation_requisitionBO.REASON_FOR_CANCEL = vRow.Reason_for_cancel;
                if (!string.IsNullOrEmpty(vRow.current_status))
                {
                    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                    accomodation_requisitionBO.current_status = Convert.ToString(Status);
                }

                if (strStatus == "1" || strStatus == "6")
                {
                    Local_travel_requisitionDataContext obj = new Local_travel_requisitionDataContext();

                    foreach (var vRow1 in obj.sp_load_hotel_cat_code(vRow.HOTEL_CAT_CODE))
                    {
                        accomodation_requisitionBO.HOTEL_CAT_CODE = vRow1.HOTEL_CATEGORY;
                    }
                    foreach (var vRow2 in obj.sp_load_hotel_names_code(vRow.HOTEL_CODE))
                    {
                        accomodation_requisitionBO.HOTEL_CODE = vRow2.HOTEL_NAME;
                    }
                    foreach (var vRow3 in obj.sp_load_room_names_code(vRow.ROOM_CODE))
                    {
                        accomodation_requisitionBO.ROOM_CODE = vRow3.ROOM_CATEGORY;
                    }
                }
                IntAccomID++;
                AccommodationRequisitionboList.Add(accomodation_requisitionBO);
            }
            return AccommodationRequisitionboList;
        }

        public List<accomodation_requisitionbo> Get_Accommodation_Details_History(int RequisitionID, int RequisitionSegmentID)
        {
            int IntAccomID = 1;//This is used for the updated the particular accomodation.
            List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
            Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_accommodation_details_History(RequisitionID, RequisitionSegmentID))
            {
                accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
                accomodation_requisitionBO.Accommadation_req_id = vRow.Accommadation_req_id;// IntAccomID;
                accomodation_requisitionBO.Additional_service = vRow.Additional_service;
                accomodation_requisitionBO.Arrival_time = vRow.Arrival_time;
                accomodation_requisitionBO.Check_in_date = vRow.Check_in_date;
                accomodation_requisitionBO.Check_out_date = vRow.Check_out_date;
                accomodation_requisitionBO.current_status = vRow.current_status;
                accomodation_requisitionBO.Departure_time = vRow.Departure_time;
                accomodation_requisitionBO.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                accomodation_requisitionBO.HOTEL_CAT_CODE = vRow.HOTEL_CAT_CODE;
                accomodation_requisitionBO.HOTEL_CODE = vRow.HOTEL_CODE; ;
                accomodation_requisitionBO.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID;
                accomodation_requisitionBO.REQUEST_ID = vRow.REQUISITION_ID;
                accomodation_requisitionBO.Remarks = vRow.REMARKS;
                accomodation_requisitionBO.RoomCategoryName = vRow.ROOM_CODE;
                accomodation_requisitionBO.ROOM_CODE = vRow.ROOM_CODE;
                accomodation_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                accomodation_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                if (!string.IsNullOrEmpty(vRow.current_status))
                {
                    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                    accomodation_requisitionBO.current_status = Convert.ToString(Status);
                }
                IntAccomID++;
                AccommodationRequisitionboList.Add(accomodation_requisitionBO);
            }
            return AccommodationRequisitionboList;
        }

        public List<accomodation_requisitionbo> Get_Traveller_Accommodation_Details(string EMPLOYEE_NO)
        {
            List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
            Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_traveller_accommodation(EMPLOYEE_NO))
            {
                accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
                accomodation_requisitionBO.Accommadation_req_id = vRow.Accommadation_req_id;// vRow.Accommadation_req_id;
                accomodation_requisitionBO.REQUEST_ID = vRow.REQUISITION_ID;
                accomodation_requisitionBO.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID;
                accomodation_requisitionBO.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                accomodation_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                accomodation_requisitionBO.Check_in_date = vRow.Check_in_date;
                accomodation_requisitionBO.Check_out_date = vRow.Check_out_date;
                accomodation_requisitionBO.Arrival_time = vRow.Arrival_time;
                accomodation_requisitionBO.Departure_time = vRow.Departure_time;
                accomodation_requisitionBO.Additional_service = vRow.Additional_service;
                accomodation_requisitionBO.current_status = vRow.current_status;
                accomodation_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                accomodation_requisitionBO.CRAETEDBY = vRow.created_by;
                accomodation_requisitionBO.CREATEDON = Convert.ToDateTime(vRow.created_on);
                accomodation_requisitionBO.MODIFIEDBY = vRow.modified_by;
                accomodation_requisitionBO.MODIFIEDON = Convert.ToDateTime(vRow.modified_on);
                accomodation_requisitionBO.isactive = Convert.ToBoolean(vRow.isActive);
                accomodation_requisitionBO.Remarks = vRow.REMARKS;
                accomodation_requisitionBO.HotelPlaceCity = vRow.HOTEL_PLACE_CITY;
                accomodation_requisitionBO.HOTEL_CAT_CODE = vRow.HOTEL_CAT_CODE;
                accomodation_requisitionBO.HOTEL_CODE = vRow.HOTEL_CODE; ;
                accomodation_requisitionBO.RoomCategoryName = vRow.ROOM_CODE;
                accomodation_requisitionBO.ROOM_CODE = vRow.ROOM_CODE;

                if (!string.IsNullOrEmpty(vRow.current_status))
                {
                    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                    accomodation_requisitionBO.current_status = Convert.ToString(Status);
                }
                AccommodationRequisitionboList.Add(accomodation_requisitionBO);
            }
            return AccommodationRequisitionboList;
        }

        public List<accomodation_requisitionbo> GetHotelCategory(string pernr, string placecode)
        {
            List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
            Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_hotel_category(pernr, placecode))
            {
                accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
                accomodation_requisitionBO.HOTEL_CAT_CODE = vRow.HOTEL_CAT_CODE;
                accomodation_requisitionBO.HOTEL_CATEGORY = vRow.HOTEL_CATEGORY;
                AccommodationRequisitionboList.Add(accomodation_requisitionBO);
            }
            return AccommodationRequisitionboList;
        }

        public List<accomodation_requisitionbo> GetHotelCategory_TD(string PERNR)
        {
            List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
            Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_hotel_category_TD(PERNR))
            {
                accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
                accomodation_requisitionBO.HOTEL_CAT_CODE = vRow.HOTEL_CAT_CODE;
                accomodation_requisitionBO.HOTEL_CATEGORY = vRow.HOTEL_CATEGORY;
                AccommodationRequisitionboList.Add(accomodation_requisitionBO);
            }
            return AccommodationRequisitionboList;
        }

        public List<accomodation_requisitionbo> GetHotelNames(string HC, string placecode)
        {
            List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
            Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_hotel_names(HC, placecode))
            {
                accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
                accomodation_requisitionBO.HOTEL_CODE = vRow.CODE;
                accomodation_requisitionBO.HOTEL_NAME = vRow.HOTEL_NAME;
                AccommodationRequisitionboList.Add(accomodation_requisitionBO);
            }
            return AccommodationRequisitionboList;
        }

        public List<accomodation_requisitionbo> GetRoomNames(string HC)
        {
            List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
            Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_room_names(HC))
            {
                accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
                accomodation_requisitionBO.ROOM_CODE = vRow.ROOM_CODE;
                accomodation_requisitionBO.RoomCategoryName = vRow.ROOM_CATEGORY;
                AccommodationRequisitionboList.Add(accomodation_requisitionBO);
            }
            return AccommodationRequisitionboList;
        }

        public int HODStatusUpdateSeparateAccomm(accomodation_requisitionbo objBo, string strOutLocal)
        {
            int iResult = -1;
            try
            {
                Accomodation_requisitionDataContext objDataContext = new Accomodation_requisitionDataContext();
                iResult = objDataContext.sp_update_HOD_status_SeparateAccomm(objBo.Accommadation_req_id, objBo.Remarks, objBo.current_status, objBo.MODIFIEDBY, strOutLocal);
                objDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }
    }
}
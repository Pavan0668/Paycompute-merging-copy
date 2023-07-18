using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Local_requisition;
using System.Globalization;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
namespace iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment
{
    public class local_accommodation_requisitionbl
    {
        public int Create_Local_accomodation_requisitionbl(accomodation_requisitionbo objbo)
        {
            int iResult = -1;
            try
            {
                Local_accommodation_requisitionDataContext objcontext = new Local_accommodation_requisitionDataContext();
                iResult = objcontext.sp_create_local_acc_requisition(objbo.EMPLOYEE_NO, objbo.LOCAL_ACCOMMODATION_TYPE, objbo.Check_in_date, objbo.Check_out_date,
                objbo.Arrival_time, objbo.Departure_time, objbo.HOTEL_CAT_CODE, objbo.ROOM_CODE, objbo.HOTEL_CODE, objbo.HotelPlaceCity, objbo.Additional_service, objbo.Number_of_members, objbo.Name_of_members, objbo.CRAETEDBY, objbo.CREATEDON,
               objbo.current_status, objbo.isactive, objbo.Remarks);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return iResult;
        }


        public int Update_Local_accomodation_requisitionbl(accomodation_requisitionbo objbo)
        {
            int iResult = -1;
            try
            {
                Local_accommodation_requisitionDataContext objcontext = new Local_accommodation_requisitionDataContext();
                iResult = objcontext.sp_update_local_accommodation_requisition(objbo.local_acc_req_id,objbo.EMPLOYEE_NO, objbo.LOCAL_ACCOMMODATION_TYPE, objbo.Check_in_date, objbo.Check_out_date,
                objbo.Arrival_time, objbo.Departure_time, objbo.HOTEL_CAT_CODE, objbo.ROOM_CODE, objbo.HOTEL_CODE, objbo.HotelPlaceCity, objbo.Additional_service, objbo.Number_of_members, objbo.Name_of_members, objbo.CRAETEDBY, objbo.CREATEDON,
               objbo.current_status, objbo.isactive, objbo.Remarks);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return iResult;
        }

        public List<accomodation_requisitionbo> LoadAccommodationRequestionDetails(string LOCAL_REQUISITION_TYPE, string EmployeeNo,string strRole)
        {
            List<accomodation_requisitionbo> AccomRequiBoListObject = new List<accomodation_requisitionbo>();
            Local_accommodation_requisitionDataContext LocalAccomRequiContextObject = new Local_accommodation_requisitionDataContext();
            foreach (var vRow in LocalAccomRequiContextObject.sp_get_local_accommodation_requisition_details(LOCAL_REQUISITION_TYPE, EmployeeNo,strRole))
            {
                accomodation_requisitionbo AccomRequiBoObject = new accomodation_requisitionbo();
                AccomRequiBoObject.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                AccomRequiBoObject.LOCAL_ACCOMMODATION_TYPE = Convert.ToString(vRow.LOCAL_ACCOMMODATION_TYPE);
                AccomRequiBoObject.Check_in_date = Convert.ToDateTime(vRow.Check_in_date);   //DateTime.ParseExact(vRow.Check_in_date.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                AccomRequiBoObject.Check_out_date = vRow.Check_out_date;

                AccomRequiBoObject.Arrival_time = vRow.Arrival_time;
                AccomRequiBoObject.Departure_time = vRow.Departure_time;
                AccomRequiBoObject.Additional_service = vRow.Additional_service;

                AccomRequiBoObject.HotelPlaceCity = vRow.Hotel_place_city;
                AccomRequiBoObject.current_status = vRow.Current_status;
                AccomRequiBoObject.Number_of_members = (int)vRow.Number_of_members;
                AccomRequiBoObject.local_acc_req_id = vRow.Local_Accommadation_Requistion_Id;
                AccomRequiBoObject.current_status = Convert.ToString(Enum.Parse(typeof(ReuisitionStatus), vRow.Current_status));
                AccomRequiBoObject.Remarks = vRow.Remarks;
                AccomRequiBoListObject.Add(AccomRequiBoObject);
            }
            return AccomRequiBoListObject;
        }

       public List<accomodation_requisitionbo> GetTheMemberDetails(int LOCAL_ACC_REQUISITION_ID)
        {
            List<accomodation_requisitionbo> AccomRequiBoListObject = new List<accomodation_requisitionbo>();
            Local_accommodation_requisitionDataContext LocalAccomRequiContextObject = new Local_accommodation_requisitionDataContext();
            foreach (var vRow in LocalAccomRequiContextObject.sp_get_local_accom_requi_member(LOCAL_ACC_REQUISITION_ID))
            {
                accomodation_requisitionbo AccomRequiBoObject = new accomodation_requisitionbo();
               if (vRow.ENAME == null)
                {
                    AccomRequiBoObject.Name_of_members = vRow.Member_Id;
                    AccomRequiBoObject.EMPLOYEE_NO = "";
                }
                else
                {
                    AccomRequiBoObject.EMPLOYEE_NO = vRow.Member_Id;
                    AccomRequiBoObject.Name_of_members = vRow.ENAME;
                }

                AccomRequiBoListObject.Add(AccomRequiBoObject);
            }
            return AccomRequiBoListObject;
        }

       public List<accomodation_requisitionbo> Get_Local_Accommodation_Separate_Details(string strEmployeeID, string strStatus)
       {
           List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
           Local_accommodation_requisitionDataContext objcontext = new Local_accommodation_requisitionDataContext();
           foreach (var vRow in objcontext.sp_get_Local_accommo_SeparateReq_details( strEmployeeID, strStatus))
           {
               accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
               accomodation_requisitionBO.Accommadation_req_id = vRow.Local_Accommadation_Requistion_Id;// IntAccomID;
               accomodation_requisitionBO.Additional_service = vRow.Additional_service;
               accomodation_requisitionBO.Arrival_time = vRow.Arrival_time;
               accomodation_requisitionBO.Check_in_date = Convert.ToDateTime( vRow.Check_in_date);
               accomodation_requisitionBO.Check_out_date = vRow.Check_out_date;
               accomodation_requisitionBO.current_status = vRow.Current_status;
               accomodation_requisitionBO.Departure_time = vRow.Departure_time;
               accomodation_requisitionBO.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
               accomodation_requisitionBO.HOTEL_CAT_CODE = vRow.HOTEL_CAT_CODE;
               accomodation_requisitionBO.HOTEL_CODE = vRow.HOTEL_CODE; 
               accomodation_requisitionBO.RoomCategoryName = vRow.ROOM_CODE;
               accomodation_requisitionBO.ROOM_CODE = vRow.ROOM_CODE;
               accomodation_requisitionBO.LOCAL_ACCOMMODATION_TYPE =Convert.ToString( vRow.LOCAL_ACCOMMODATION_TYPE);
               accomodation_requisitionBO.HotelPlaceCity = vRow.Hotel_place_city;
               accomodation_requisitionBO.Number_of_members= Convert.ToInt32( vRow.Number_of_members);
               accomodation_requisitionBO.CRAETEDBY=vRow.Created_by;
               accomodation_requisitionBO.CREATEDON=Convert.ToDateTime( vRow.Created_on);
               accomodation_requisitionBO.MODIFIEDBY=vRow.Modified_by;
               accomodation_requisitionBO.MODIFIEDON=Convert.ToDateTime( vRow.Modified_on);
               accomodation_requisitionBO.isactive=Convert.ToBoolean( vRow.Is_active);
               accomodation_requisitionBO.REASON_FOR_CANCEL = vRow.Reason_for_cancel;

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
               AccommodationRequisitionboList.Add(accomodation_requisitionBO);            
           }
           return AccommodationRequisitionboList;
       }

       public List<accomodation_requisitionbo> Get_Local_Accommodation_Details_TicketBooked(int AccReqID, int RequisitionSegmentID)
       {
           List<accomodation_requisitionbo> AccommodationRequisitionboList = new List<accomodation_requisitionbo>();
           Accomodation_requisitionDataContext objcontext = new Accomodation_requisitionDataContext();
           foreach (var vRow in objcontext.sp_get_LocalAccomm_requisition_details(AccReqID, RequisitionSegmentID))
           {
               accomodation_requisitionbo accomodation_requisitionBO = new accomodation_requisitionbo();
               accomodation_requisitionBO.Accommadation_req_id = vRow.Local_Accommadation_Requistion_Id;// IntAccomID;
               accomodation_requisitionBO.Additional_service = vRow.Additional_service;
               accomodation_requisitionBO.Arrival_time = vRow.Arrival_time;
               accomodation_requisitionBO.Check_in_date = Convert.ToDateTime(vRow.Check_in_date);
               accomodation_requisitionBO.Check_out_date = vRow.Check_out_date;
               accomodation_requisitionBO.current_status = vRow.Current_status;
               accomodation_requisitionBO.Departure_time = vRow.Departure_time;
               accomodation_requisitionBO.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
               accomodation_requisitionBO.HOTEL_CAT_CODE = vRow.HOTEL_CAT_CODE;
               accomodation_requisitionBO.HOTEL_CODE = vRow.HOTEL_CODE;
               accomodation_requisitionBO.RoomCategoryName = vRow.ROOM_CODE;
               accomodation_requisitionBO.ROOM_CODE = vRow.ROOM_CODE;
               accomodation_requisitionBO.LOCAL_ACCOMMODATION_TYPE = Convert.ToString(vRow.LOCAL_ACCOMMODATION_TYPE);
               accomodation_requisitionBO.HotelPlaceCity = vRow.Hotel_place_city;
               accomodation_requisitionBO.Number_of_members = Convert.ToInt32(vRow.Number_of_members);
               accomodation_requisitionBO.CRAETEDBY = vRow.Created_by;
               accomodation_requisitionBO.CREATEDON = Convert.ToDateTime(vRow.Created_on);
               accomodation_requisitionBO.MODIFIEDBY = vRow.Modified_by;
               accomodation_requisitionBO.MODIFIEDON = Convert.ToDateTime(vRow.Modified_on);
               accomodation_requisitionBO.isactive = Convert.ToBoolean(vRow.Is_active);

               AccommodationRequisitionboList.Add(accomodation_requisitionBO);
           }
           return AccommodationRequisitionboList;
       }
    }
}
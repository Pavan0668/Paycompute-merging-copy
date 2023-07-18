using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class accomodation_requisitionbo
    {
        private int? _FTPT_REQUEST_ID = 0;
        private int? _REQ_SEGMENT_ID = 0;
        private string _EMPLOYEE_NO = string.Empty;
        public string EmployeeName { get; set; }
        private DateTime _Date_of_travel = Convert.ToDateTime("01/01/0001");
    private DateTime _Check_in_date ;
    private DateTime _Check_out_date;
   private string _Arrival_time =string.Empty ;
   private string _Departure_time =string.Empty;
   private string _Additional_service = string.Empty;
   private bool _status;
   private string _sCreatedBy = string.Empty;
   private DateTime _dCreatedOn;
   private string _sModifiedBy = string.Empty;
   private DateTime _dModifiedOn;
   private string _current_status;
   private string _STATUS_UPDATED_BY;
   public string Remarks { get; set; }
   public int Accommadation_req_id { get; set; }
   public int REQ_SEGMENT_ID_FROM_TRAVEL_REQUEST { get; set; }
   public string HOTEL_CAT_CODE { get; set; }
   public string HOTEL_CATEGORY { get; set; }
   public string HOTEL_CODE { get; set; }
   public string HOTEL_NAME { get; set; }
   public string ROOM_CODE { get; set; }
   public string RoomCategoryName { get; set; }
   public int local_acc_req_id { get; set; }
   public int Number_of_members { get; set; }
   public char TRAVEL_TYPE { get; set; }
   public string Name_of_members { get; set; }
   public bool isactive { get; set; }
   public string HotelPlaceCity { get; set; }
   public string LOCAL_ACCOMMODATION_TYPE { get; set; }
   public List<string> MemberIdList { get; set; }
   public bool IS_EMPLOYEE_CHECKD { get; set; }
   public string REASON_FOR_CANCEL { get; set; }

   public string STATUS_UPDATED_BY
   {
       get
       {
           return _STATUS_UPDATED_BY;
       }
       set
       {
           _STATUS_UPDATED_BY = value;
       }
   }
   public string current_status
   {
       get
       {
           return _current_status;
       }
       set
       {
           _current_status = value;
       }
   }
   public bool status
   {
       get
       {
           return _status;
       }
       set
       {
           _status = value;
       }
   }
   public int? REQ_SEGMENT_ID
   {
       get
       {
           return _REQ_SEGMENT_ID;
       }
       set
       {
           _REQ_SEGMENT_ID = value;
       }
   }
   public int? REQUEST_ID
   {
       get
       {
           return _FTPT_REQUEST_ID == null ? 0 : _FTPT_REQUEST_ID.Value;
       }
       set
       {
           _FTPT_REQUEST_ID = value;
       }
   }

   public string EMPLOYEE_NO
   {
       get
       {
           return _EMPLOYEE_NO;
       }
       set
       {
           _EMPLOYEE_NO = value;
       }
   }
   public DateTime Date_of_travel
   {
       get
       {
           return _Date_of_travel;
       }
       set
       {
           _Date_of_travel = value;
       }
   }
   public DateTime Check_in_date
   {
       get
       {
           return _Check_in_date;
       }
       set
       {
           _Check_in_date = value;
       }
   }

   public DateTime Check_out_date
   {
       get
       {
           return _Check_out_date;
       }
       set
       {
           _Check_out_date = value;
       }
   }

   public String Arrival_time
   {
       get
       {
           return _Arrival_time;
       }
       set
       {
           _Arrival_time = value;
       }
   }

   public String Departure_time
   {
       get
       {
           return _Departure_time;
       }
       set
       {
           _Departure_time = value;
       }
   }
         

   public string Additional_service
   {
       get
       {
           return _Additional_service;
       }
       set
       {
           _Additional_service = value;
       }
   }

   public string CRAETEDBY
   {
       get { return _sCreatedBy; }
       set { _sCreatedBy = value; }
   }
   public DateTime CREATEDON
   {
       get { return _dCreatedOn; }
       set { _dCreatedOn = value; }
   }
   public string MODIFIEDBY
   {
       get { return _sModifiedBy; }
       set { _sModifiedBy = value; }
   }
   public DateTime MODIFIEDON
   {
       get { return _dModifiedOn; }
       set { _dModifiedOn = value; }
   }
    }
}
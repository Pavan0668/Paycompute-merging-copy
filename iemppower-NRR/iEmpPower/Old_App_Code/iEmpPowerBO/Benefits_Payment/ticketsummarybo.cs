using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class ticketsummarybo
    {
        public Int32 _req_id;
        public string _employee_no;
        public string _EMPLOYEE_Name= string.Empty;
        public string _designation = string.Empty;
     public Int32  _REQUISITION_ID;
     public DateTime  _TRAVEL_DATE;
     public string _TIME_OF_DEPT;
     public string _MODE_OF_TRANSPOPRT;
    public string _MEDIA_OF_CATEGORY;
     public int _VEHICLE_NUM;
     public string _FROM;
    public string _TO;
    public string _FLYNUM;
    public string _ADVANCE_AMOUNT;
    public string _AIRLINE;
    public string _VISA_REQUIRED;
   public string  _FR_EXCHANGE;
   public string  _INSUR_MEDICLAIM;
   public string  _SEAT_PREFERENCE;
   public string  _MEAL_PREFERENCE;
   public string  _BAGGAGE_COUNT;
   public string  _HAND_BAGGAGE_COUNT;
  public string _current_status;
    public string _STATUS_UPDATED_BY;
   public string _modified_by;
   public DateTime  _modified_on;
   public Byte  _isActive;
   public  string _REMARKS;
   public int _REQ_SEGMENT_ID;

   public string employee_no
   {
       get
       {
           return _employee_no;
       }
       set
       {
           _employee_no = value;
       }
   }
   public Int32 req_id
   {
       get
       {
           return _req_id;
       }
       set
       {
           _req_id = value;
       }
   }
   public Int32 REQUISITION_ID
   {
       get
       {
           return _REQUISITION_ID;
       }
       set
       {
           _REQUISITION_ID = value;
       }
   }
   public int VEHICLE_NUM
   {
       get
       {
           return _VEHICLE_NUM;
       }
       set
       {
           _VEHICLE_NUM = value;
       }
   }
   public Int32 REQ_SEGMENT_ID
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
   public DateTime TRAVEL_DATE
   {
       get
       {
           return _TRAVEL_DATE;
       }
       set
       {
           _TRAVEL_DATE = value;
       }
   }
        public DateTime modified_on
   {
       get
       {
           return _modified_on;
       }
       set
       {
           _modified_on = value;
       }
   }
         public string VISA_REQUIRED
   {
       get
       {
           return _VISA_REQUIRED;
       }
       set
       {
           _VISA_REQUIRED = value;
       }
   }

         public Byte isActive
         {
             get
             {
                 return _isActive;
             }
             set
             {
                 _isActive = value;
             }
         }
   public string TIME_OF_DEPT
   {
       get
       {
           return _TIME_OF_DEPT;
       }
       set
       {
           _TIME_OF_DEPT = value;
       }
   }
   public string MODE_OF_TRANSPOPRT
   {
       get
       {
           return _MODE_OF_TRANSPOPRT;
       }
       set
       {
           _MODE_OF_TRANSPOPRT = value;
       }
   }
   public string MEDIA_OF_CATEGORY
   {
       get
       {
           return _MEDIA_OF_CATEGORY;
       }
       set
       {
           _MEDIA_OF_CATEGORY = value;
       }
   }
   public string FROM
   {
       get
       {
           return _FROM;
       }
       set
       {
           _FROM = value;
       }
   }
   public string TO
   {
       get
       {
           return _TO;
       }
       set
       {
           _TO = value;
       }
   }
   public string FLYNUM
   {
       get
       {
           return _FLYNUM;
       }
       set
       {
           _FLYNUM = value;
       }
   }
   public string ADVANCE_AMOUNT
   {
       get
       {
           return _ADVANCE_AMOUNT;
       }
       set
       {
           _ADVANCE_AMOUNT = value;
       }
   }
   public string AIRLINE
   {
       get
       {
           return _AIRLINE;
       }
       set
       {
           _AIRLINE = value;
       }
   }
   public string FR_EXCHANGE
   {
       get
       {
           return _FR_EXCHANGE;
       }
       set
       {
           _FR_EXCHANGE = value;
       }
   }
   public string INSUR_MEDICLAIM
   {
       get
       {
           return _INSUR_MEDICLAIM;
       }
       set
       {
           _INSUR_MEDICLAIM = value;
       }
   }
  
   public string SEAT_PREFERENCE
   {
       get
       {
           return _SEAT_PREFERENCE;
       }
       set
       {
           _SEAT_PREFERENCE = value;
       }
   }
   public string MEAL_PREFERENCE
   {
       get
       {
           return _MEAL_PREFERENCE;
       }
       set
       {
           _MEAL_PREFERENCE = value;
       }
   }

   public string BAGGAGE_COUNT
        {
            get
            {
                return _BAGGAGE_COUNT;
            }
            set
            {
                _BAGGAGE_COUNT = value;
            }
        }
   public string HAND_BAGGAGE_COUNT
        {
            get
            {
                return _HAND_BAGGAGE_COUNT;
            }
            set
            {
                _HAND_BAGGAGE_COUNT = value;
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
   public string modified_by
        {
            get
            {
                return _modified_by;
            }
            set
            {
                _modified_by = value;
            }
        }
   public string REMARKS
        {
            get
            {
                return _REMARKS;
            }
            set
            {
                _REMARKS = value;
            }
        }
        public string EMPLOYEE_Name
        {
            get
            {
                return _EMPLOYEE_Name;
            }
            set
            {
                _EMPLOYEE_Name = value;
            }
        }
        public string Designation
        {
            get
            {
                return _designation;
            }
            set
            {
                _designation = value;
            }
        }

    }
    }

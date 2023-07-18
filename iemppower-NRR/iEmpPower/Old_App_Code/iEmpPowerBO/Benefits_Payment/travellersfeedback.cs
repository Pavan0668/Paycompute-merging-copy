using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.travellersfeedback
{
    public class travellersfeedback
    {
         private Int32 _FeedBack_Id=0;
         private int _iREQUISITION_ID=0;
         private int _iREQ_SEGMENT_ID = 0;
         private string _PERNR = string.Empty;
         private string _Traveller_name = string.Empty;
         private Byte _Travel_duration = 0;
         private Byte _travel_arrangement = 0;
         private Byte _Accommodation_arrangement = 0;
         private Byte _Taxi_arrangement = 0;
         private Byte _Visa_Passport_arrangement = 0;
         private Byte _Overall_travel_experience = 0;
         private Byte _communication = 0;
        private string _Feedback = string.Empty;


        public Int32 feedBack_Id
        {
            get
            {
                return _FeedBack_Id;
            }
            set
            {
                _FeedBack_Id = value;
            }
        }

        public int REQUISITION_ID
        {
            get { return _iREQUISITION_ID; }
            set { _iREQUISITION_ID = value; }
        }

        public int REQ_SEGMENT_ID
        {
            get{ return _iREQ_SEGMENT_ID; }
            set { _iREQ_SEGMENT_ID = value; }
        }


        public string pERNR
        {
            get
            {
                return _PERNR;
            }
            set
            {
                _PERNR = value;
            }
        }
        public string traveller_name
        {
            get
            {
                return _Traveller_name;
            }
            set
            {
                _Traveller_name = value;
            }
        }
        public Byte travel_duration
        {
            get
            {
                return _Travel_duration;
            }
            set
            {
                _Travel_duration = value;
            }
        }
        public Byte accommodation_arrangement
        {
            get
            {
                return _Accommodation_arrangement;
            }
            set
            {
                _Accommodation_arrangement = value;
            }
        }
        public Byte travel_arrangement
        {
            get
            {
                return _travel_arrangement;
            }
            set
            {
                _travel_arrangement = value;
            }
        }
        public Byte Taxi_arrangement
        {
            get
            {
                return _Taxi_arrangement;
            }
            set
            {
                _Taxi_arrangement = value;
            }
        }
        public Byte Visa_Passport_arrangement
        {
            get
            {
                return _Visa_Passport_arrangement;
            }
            set
            {
                _Visa_Passport_arrangement = value;
            }
        }
        public Byte Overall_travel_experience
        {
            get
            {
                return _Overall_travel_experience;
            }
            set
            {
                _Overall_travel_experience = value;
            }
        }
        public Byte communication
        {
            get
            {
                return _communication;
            }
            set
            {
                _communication = value;
            }
        }
        public string Feedback
        {
            get
            {
                return _Feedback;
            }
            set
            {
                _Feedback = value;
            }
        }
    }
}
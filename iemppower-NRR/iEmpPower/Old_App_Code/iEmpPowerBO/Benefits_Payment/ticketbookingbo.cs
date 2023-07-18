using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class ticketbookingbo
    {
        public int Ticket_id { get; set; }
        public string _EMPLOYEE_Name= string.Empty;
        public string _designation = string.Empty;
        public string EMPLOYEE_NO { get; set; }
        public int REQUISITION_ID { get; set; }
        public int REQUISITION_SEGMENT_ID { get; set; }
        public int REQ_SEGMENT_ID { get; set; }
        public int PROPOSAL_ID { get; set; }
        public int PRO_SEGMENT_ID { get; set; }
        public string TICKET_NO { get; set; }
        public string PNR_NO { get; set; }
        public string COACH { get; set; }
        public string SEAT_BIRTH { get; set; }
        public string CLASS { get; set; }
        public string TICKET_FARE { get; set; }
        public string ARRIVAL_TIME { get; set; }
        public string DEPARTURE_TIME { get; set; }
        public string current_status { get; set; }
        public string STATUS_UPDATED_BY { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public bool isActive { get; set; }
        public string flight_train_bus_No { get; set; }
        public string Vehicle_type { get; set; }
        public string Vehicle_category { get; set; }
        public string Vehicle_name { get; set; }
        public string DEPARTURE_FROM { get; set; }
        public DateTime TRAVEL_DATE { get; set; }        
         public string Bill_No	{ get; set; }
         public DateTime Bill_Date { get; set; }
         public string Bill_Amnt{ get; set; }
         public string CN_No	{ get; set; }
         public DateTime CN_Date { get; set; }
         public string CN_Amnt	{ get; set; }
         public string Net_Amnt { get; set; }
         public string Agent_Name { get; set; }
         public string Expense_Type { get; set; }
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
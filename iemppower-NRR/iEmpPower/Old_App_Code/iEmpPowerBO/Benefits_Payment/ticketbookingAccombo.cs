using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class ticketbookingAccombo
    {
        public int Ticket_ID { get; set; }
        public string EMPLOYEE_NO { get; set; }
        public int Accommadation_req_id { get; set; } 
        public int REQUISITION_ID { get; set; } 
        public int REQ_SEGMENT_ID { get; set; }
        public int PROPOSAL_ID { get; set; }
        public int PRO_SEGMENT_ID { get; set; }
        public string Payment { get; set; }
        public string Tariff { get; set; }
        public string Booking_given_to { get; set; }
        public string Hotel_Invoice_Num { get; set; }
        public DateTime Bill_date { get; set; }
        public string Amount { get; set; }
        public string created_by { get; set; } 
        public DateTime created_on { get; set; }
        public bool isActive { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_on { get; set; }
        public string CurrentStatus { get; set; }
        public int OutLocal { get; set; }
        public string Net_Amnt { get; set; }
        public string Agent_Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class ticketbookingVehibo
    {

        public int Vehi_Ticket_id { get; set; }
        public int Vehicle_req_id	{ get; set; }
        public int REQUISITION_ID{ get; set; }
        public int REQ_SEGMENT_ID	{ get; set; }
        public int PROPOSAL_ID	{ get; set; }
        public int PRO_SEGMENT_ID	{ get; set; }
        public string EMPLOYEE_NO	{ get; set; }
        public string Vehicle_Source	{ get; set; }
        public string Agent_Name	{ get; set; }
        public string Booking_passed_to	{ get; set; }
        public string Vehicle_Num	{ get; set; }
        public string Rate_kms	{ get; set; }
        public string Driver_Name	{ get; set; }
        public string Contact_Number	{ get; set; }
        public string Statutory_Req	{ get; set; }
        public string Agent_BillNum	{ get; set; }
        public DateTime Bill_Date	{ get; set; }
        public string Agent_BillAmnt	{ get; set; }
        public string Driver_Batta	{ get; set; }
        public string Total_km	{ get; set; }
        public string Total_Cost	{ get; set; }
        public string created_by	{ get; set; }
        public DateTime created_on	{ get; set; }
        public string modified_by	{ get; set; }
        public DateTime modified_on	{ get; set; }
        public bool  isActive { get; set; }
        public string CurrentStatus { get; set; }
        public int OutLocal { get; set; }
        public string Net_Amnt { get; set; }
    }
}
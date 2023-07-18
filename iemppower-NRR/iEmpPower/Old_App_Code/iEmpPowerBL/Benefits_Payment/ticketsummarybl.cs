using System;
using System.Collections.Generic;
using System.Linq;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment
{
    public class ticketsummarybl
    {

        public ticketbookingsummarycollectionbo Get_empname_notbooked()
        {
           ticketbookingsummaryDataContext objdatacontext=new ticketbookingsummaryDataContext();
        
          ticketbookingsummarycollectionbo objlist = new ticketbookingsummarycollectionbo();

          foreach (var vRow in objdatacontext.sp_get_emp_summary_ticket_booked())
          {
              ticketsummarybo objbo = new ticketsummarybo();
              objbo.EMPLOYEE_Name = vRow.ENAME;
              objbo.Designation = vRow.PLSXT;
              objlist.Add(objbo);
          }

          return objlist;
        }

        public ticketbookingsummarycollectionbo Get_emp_ticket_booking_details(string name)
        {
            ticketbookingsummaryDataContext objdatacontext = new ticketbookingsummaryDataContext();

            ticketbookingsummarycollectionbo objlist = new ticketbookingsummarycollectionbo();


            foreach (var vRow in objdatacontext.sp_get_emp_summary_ticketbooking_details(name))
            {
                ticketsummarybo objbo = new ticketsummarybo();
               
                objbo.REQUISITION_ID =(int) vRow.REQUISITION_ID;
                objbo.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
                objbo.MODE_OF_TRANSPOPRT = vRow.MODE_OF_TRANSPOPRT;
                objbo.MEDIA_OF_CATEGORY = vRow.MEDIA_OF_CATEGORY;
                objbo.FROM = vRow.FROM;
                objbo.TO = vRow.TO;
                objbo.VEHICLE_NUM =Convert.ToInt32(vRow.VEHICLE_NUM);
                objbo.AIRLINE = vRow.AIRLINE;
                objbo.BAGGAGE_COUNT = vRow.BAGGAGE_COUNT;
                objbo.HAND_BAGGAGE_COUNT = vRow.HAND_BAGGAGE_COUNT;
                objbo.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
                objbo.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
                objbo.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
                objbo.FLYNUM = vRow.FLYNUM;
                objbo.VISA_REQUIRED = vRow.VISA_REQUIRED;
                objbo.FR_EXCHANGE = vRow.FR_EXCHANGE;
                objbo.current_status = vRow.current_status;
                objbo.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                objbo.modified_by=vRow.modified_by;
                objbo.modified_on =Convert.ToDateTime( vRow.modified_on);
             //   objbo.REMARKS = vRow.REMARKS;
                objbo.REQ_SEGMENT_ID =(int) vRow.REQ_SEGMENT_ID;
                objbo.TIME_OF_DEPT = vRow.TIME_OF_DEPT;
                objlist.Add(objbo);
            }
            return objlist;
        }

        public ticketbookingsummarycollectionbo Get_empid_by_name(string name)
        {
            ticketbookingsummaryDataContext objdatacontext = new ticketbookingsummaryDataContext();

            ticketbookingsummarycollectionbo objlist = new ticketbookingsummarycollectionbo();

            foreach (var vRow in objdatacontext.sp_get_emp_id(name))
            {
                ticketsummarybo objbo = new ticketsummarybo();
                objbo.employee_no= vRow.PERNR;
                objbo.REQUISITION_ID = (int)vRow.REQUISITION_ID;
                objbo.EMPLOYEE_Name = vRow.ENAME;
                objlist.Add(objbo);
            }
            return objlist;
        }

    }
}
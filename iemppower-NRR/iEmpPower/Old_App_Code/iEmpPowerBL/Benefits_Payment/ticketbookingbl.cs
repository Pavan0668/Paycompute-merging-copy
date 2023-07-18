using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
namespace iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment
{
    public class ticketbookingbl
    {
        public List<ticketbookingbo> Get_emp_details_notbooked(string chk)
        {
            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();
            List<ticketbookingbo> objlist = new List<ticketbookingbo>();

            foreach (var vRow in objdatacontext.sp_get_emp_details_notbooked(chk))
            {
                ticketbookingbo objbo = new ticketbookingbo();
                objbo.REQUISITION_ID = (int)vRow.REQUISITION_ID;
                objbo.REQUISITION_SEGMENT_ID = (int)vRow.REQ_SEGMENT_ID;
                objbo.REQ_SEGMENT_ID = (int)vRow.REQ_SEGMENT_ID;
                objbo.PROPOSAL_ID = vRow.PROPOSAL_ID;
                objbo.PRO_SEGMENT_ID = (int)vRow.PRO_SEGMENT_ID;
                objbo.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                objbo.EMPLOYEE_Name = vRow.ENAME;
                objbo.Designation = vRow.PLSXT;
                objbo.Vehicle_type = vRow.MODE_OF_TRANSPOPRT;
                objbo.DEPARTURE_FROM = vRow.FROM;
                objbo.DEPARTURE_TIME = vRow.To;
                objbo.Vehicle_category = vRow.MEDIA_OF_CATEGORY;
                objbo.Vehicle_name = vRow.VEHICLE_NUM;
                objbo.TRAVEL_DATE =(DateTime) vRow.TRAVEL_DATE;

                objlist.Add(objbo);
            }
            return objlist;
        }

        public int Create_ticket_booking_details(ticketbookingbo objbo,ref int? TicketID)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_create_ticket_booked_details(objbo.REQUISITION_ID, objbo.REQUISITION_SEGMENT_ID,objbo.PROPOSAL_ID,objbo.PRO_SEGMENT_ID, objbo.EMPLOYEE_NO,objbo.flight_train_bus_No, objbo.TICKET_NO, objbo.PNR_NO,
                objbo.COACH, objbo.SEAT_BIRTH, objbo.CLASS, objbo.TICKET_FARE,
                objbo.ARRIVAL_TIME, objbo.DEPARTURE_TIME, objbo.current_status, objbo.STATUS_UPDATED_BY, objbo.created_by, objbo.created_on,objbo.TRAVEL_DATE,
                objbo.isActive, objbo.Bill_No, objbo.Bill_Date, objbo.Bill_Amnt, objbo.CN_No, objbo.CN_Date, objbo.CN_Amnt, objbo.Net_Amnt,ref TicketID);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public int Create_gropemployees_ticket_booking(int iMemberID,int iReqid,int iReqSegID,int iTicketID)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_create_groupReq_Ticket(iMemberID,iReqid,iReqSegID,iTicketID);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public int Create_Accommadation_ticket_booking_details( ticketbookingAccombo objbo)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_create_Accom_ticket_booked_details(objbo.Accommadation_req_id, objbo.REQUISITION_ID, objbo.REQ_SEGMENT_ID, objbo.PROPOSAL_ID,
                             objbo.PRO_SEGMENT_ID, objbo.EMPLOYEE_NO, objbo.Payment, objbo.Tariff,objbo.Booking_given_to, objbo.Hotel_Invoice_Num, objbo.Bill_date,
                             objbo.Amount, objbo.created_by, objbo.created_on, objbo.modified_by, objbo.modified_on, objbo.isActive);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public int Create_Vehicle_ticket_booking_details(ticketbookingVehibo objbo)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_create_Vehi_ticket_booked_details(objbo. Vehicle_req_id,objbo. REQUISITION_ID,objbo.REQ_SEGMENT_ID,objbo.PROPOSAL_ID,
                        objbo.PRO_SEGMENT_ID,objbo.EMPLOYEE_NO,objbo.Vehicle_Source,objbo.Agent_Name,objbo.Booking_passed_to,objbo.Vehicle_Num,objbo.Rate_kms,objbo.Driver_Name,
                        objbo.Contact_Number,objbo.Statutory_Req,objbo.Agent_BillNum,objbo.Bill_Date,objbo.Agent_BillAmnt,objbo.Driver_Batta,objbo.Total_km,objbo.Total_Cost,
                        objbo.created_by,objbo.created_on,objbo.modified_by,objbo.modified_on,objbo.CurrentStatus,objbo.isActive);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public int Create_OutLocal_Vehicle_ticket_booking_details(ticketbookingVehibo objbo,int strOutLocal)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_create_OutLocal_Vehi_ticket_booked_details(objbo.Vehicle_req_id, objbo.EMPLOYEE_NO, objbo.Vehicle_Source, objbo.Agent_Name, objbo.Booking_passed_to, objbo.Vehicle_Num, objbo.Rate_kms, objbo.Driver_Name,
                        objbo.Contact_Number, objbo.Statutory_Req, objbo.Agent_BillNum, objbo.Bill_Date, objbo.Agent_BillAmnt, objbo.Driver_Batta, objbo.Total_km, objbo.Total_Cost,
                        objbo.created_by, objbo.created_on, objbo.modified_by, objbo.modified_on, objbo.CurrentStatus, objbo.isActive,strOutLocal);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public int Create_OutLocal_Accommadation_ticket_booking_details(ticketbookingAccombo objbo, int strOutLocal)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_create_OutLocal_Accom_ticket_booked_details(objbo.Accommadation_req_id, objbo.EMPLOYEE_NO, objbo.Payment, objbo.Tariff, objbo.Booking_given_to, objbo.Hotel_Invoice_Num, objbo.Bill_date,
                             objbo.Amount, objbo.created_by, objbo.created_on, objbo.modified_by, objbo.modified_on,objbo.CurrentStatus, objbo.isActive,strOutLocal);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public DataSet fun_Get_Vendorlist()
        {
            DataSet ds = new DataSet();
            DataTable objTable = ds.Tables.Add();
            objTable.Columns.Add("VENDOR_NAME", typeof(string));
            objTable.Columns.Add("EXPENSE_TYPE", typeof(string));

            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

            foreach (var vrow in objdatacontext.sp_get_Vendorlist())
            {
                objTable.Rows.Add(vrow.VENDOR_NAME, vrow.EXPENSE_TYPE);
            }

            objdatacontext.Dispose();

            return ds;
        }


        public List<ticketbookingbo> fun_Get_Ticket_Booked_Details()
        {
            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();
            List<ticketbookingbo> objlist = new List<ticketbookingbo>();

            foreach (var vRow in objdatacontext.sp_get_Ticket_Booked_details())
            {
                ticketbookingbo objbo = new ticketbookingbo();
                objbo.Ticket_id = vRow.TICKET_ID;
                objbo.EMPLOYEE_NO=vRow.EMPLOYEE_NO;
                objbo.REQUISITION_ID=(int)vRow.REQUISITION_ID;
                objbo.REQUISITION_SEGMENT_ID=vRow. REQUISITION_SEGMENT_ID;
                objbo.PROPOSAL_ID=vRow.PROPOSAL_ID;
                objbo.PRO_SEGMENT_ID =vRow. PRO_SEGMENT_ID;
                objbo.flight_train_bus_No  =vRow. FLIGHT_TRAIN_BUS_NO;
                objbo.TICKET_NO  =vRow. TICKET_NO;
                objbo.PNR_NO  =vRow. PNR_NO;
                objbo.COACH=vRow. COACH;
                objbo.SEAT_BIRTH=vRow. SEAT_BIRTH;
                objbo.CLASS=vRow. CLASS;
                objbo.TICKET_FARE=vRow. TICKET_FARE;
                objbo.ARRIVAL_TIME=vRow. ARRIVAL_TIME;
                objbo.DEPARTURE_TIME=vRow. DEPARTURE_TIME;
                objbo.current_status=vRow. current_status;
                objbo.STATUS_UPDATED_BY=vRow.STATUS_UPDATED_BY;
                objbo.created_by=vRow.created_by;
                objbo.created_on=(DateTime)vRow.created_on;
                objbo.isActive=(bool)vRow.isActive;
                objbo.Bill_No = vRow.Bill_No;
                objbo.Bill_Date = (DateTime)vRow.Bill_Date;
                objbo.Bill_Amnt = vRow.Bill_Amnt;
                objbo.Agent_Name = vRow.Agent_Name;
                
                //if (!string.IsNullOrEmpty(vRow.current_status))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                //    objbo.current_status = Convert.ToString(Status);
                //}
                objlist.Add(objbo);
            }
            return objlist;
        }

        public List<ticketbookingbo> fun_Get_Vendor_Ins()
        {
            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();
            List<ticketbookingbo> objlist = new List<ticketbookingbo>();

            foreach (var vRow in objdatacontext.sp_get_Vendor_Ins())
            {
                ticketbookingbo objbo = new ticketbookingbo();

                objbo.EMPLOYEE_NO = vRow.PERNR;

                objbo.PROPOSAL_ID = Convert.ToInt32(vRow.PROPOSAL_ID);

                string vname = "";
                foreach (var vRow1 in objdatacontext.sp_Get_VendorName(vRow.AGENT_NAME))
                 {
                     vname = vRow1.VENDOR_NAME;
                 }
             
                objbo.Agent_Name = vname;
                objbo.Bill_No = vRow.AGENT_BILLNUM;

                objbo.Bill_Date = (DateTime)vRow.BILL_DATE;
                objbo.Bill_Amnt = vRow.AGENT_BILLAMNT;
                objbo.created_on = (DateTime)vRow.created_on;

                objlist.Add(objbo);
            }
            return objlist;
        }

        public List<ticketbookingbo> fun_Get_Vendor_Visa()
        {
            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();
            List<ticketbookingbo> objlist = new List<ticketbookingbo>();

            foreach (var vRow in objdatacontext.sp_get_Vendor_Visa())
            {
                ticketbookingbo objbo = new ticketbookingbo();

                objbo.EMPLOYEE_NO = vRow.PERNR;

                objbo.PROPOSAL_ID = Convert.ToInt32(vRow.PROPOSAL_ID);

                string vname = "";
                foreach (var vRow1 in objdatacontext.sp_Get_VendorName(vRow.AGENT_NAME))
                {
                    vname = vRow1.VENDOR_NAME;
                }

                objbo.Agent_Name = vname;
                objbo.Bill_No = vRow.AGENT_BILLNUM;

                objbo.Bill_Date = (DateTime)vRow.BILL_DATE;
                objbo.Bill_Amnt = vRow.AGENT_BILLAMNT;
                objbo.created_on = (DateTime)vRow.created_on;

                objlist.Add(objbo);
            }
            return objlist;
        }

        public List<ticketbookingbo> fun_Get_ALL_Ticket_Booked_Details()
        {
            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();
            List<ticketbookingbo> objlist = new List<ticketbookingbo>();

            foreach (var vRow in objdatacontext.sp_get_ALL_Ticket_Booked_details())
            {
                ticketbookingbo objbo = new ticketbookingbo();

                objbo.Ticket_id = vRow.TICKET_ID;
                objbo.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                objbo.REQUISITION_ID = (int)vRow.REQUISITION_ID;
                objbo.REQUISITION_SEGMENT_ID = vRow.REQUISITION_SEGMENT_ID;
                objbo.PROPOSAL_ID = vRow.PROPOSAL_ID;
                objbo.PRO_SEGMENT_ID = vRow.PRO_SEGMENT_ID;
                objbo.flight_train_bus_No = vRow.FLIGHT_TRAIN_BUS_NO;
                objbo.TICKET_NO = vRow.TICKET_NO;
                objbo.PNR_NO = vRow.PNR_NO;
                objbo.COACH = vRow.COACH;
                objbo.SEAT_BIRTH = vRow.SEAT_BIRTH;
                objbo.CLASS = vRow.CLASS;
                objbo.TICKET_FARE = vRow.TICKET_FARE;
                objbo.ARRIVAL_TIME = vRow.ARRIVAL_TIME;
                objbo.DEPARTURE_TIME = vRow.DEPARTURE_TIME;
                objbo.current_status = vRow.current_status;
                objbo.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                objbo.created_by = vRow.created_by;
                objbo.created_on = (DateTime)vRow.created_on;
                objbo.TRAVEL_DATE =(DateTime) vRow.TRAVEL_DATE;
                objbo.Net_Amnt = vRow.Net_Amnt;
                
                //if (!string.IsNullOrEmpty(vRow.current_status))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                //    objbo.current_status = Convert.ToString(Status);
                //}
                objbo.isActive = (bool)vRow.isActive;

                objlist.Add(objbo);
            }
            return objlist;
        }

        public int Cancel_Travel_ticket_(int iRequisiID,int iRequisiSegmnID,string strCNNo , DateTime dtCNDate , string strCNAmnt ,string strNetAmnt )
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_cancel_ticket_updateStatus(iRequisiID,iRequisiSegmnID,strCNNo,dtCNDate,strCNAmnt,strNetAmnt );
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public int Ticket_Bill_Updates(int iTicketID, string strBillNo, DateTime dtBillDate, string strBillAmnt, string strNetAmnt,string strAgent_Name,string strRole)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_Ticket_Bill_Updates(iTicketID, strBillNo, dtBillDate, strBillAmnt, strNetAmnt, strAgent_Name,strRole);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public int Ticket_Bill_Updates_Visa(string empid, string PROPOSAL_ID, string strAgent_Name, string strBillNo, DateTime dtBillDate, string strBillAmnt, ref bool? status1, DateTime createdon)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_create_Vendor_Visa(empid, PROPOSAL_ID, strAgent_Name, strBillNo, dtBillDate, strBillAmnt, ref status1, createdon);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public int Ticket_Bill_Updates_Ins(string empid, string PROPOSAL_ID, string strAgent_Name, string strBillNo, DateTime dtBillDate, string strBillAmnt, ref bool? status1, DateTime createdon)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_create_Vendor_Ins(empid, PROPOSAL_ID, strAgent_Name, strBillNo, dtBillDate, strBillAmnt, ref status1, createdon);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }


        public List<ticketbookingVehibo> fun_Get_Vehi_Ticket_Booked_Details()
        {
            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();
            List<ticketbookingVehibo> objlist = new List<ticketbookingVehibo>();

            foreach (var vRow in objdatacontext.sp_get_Vehi_Ticket_Booked_details())
            {
                ticketbookingVehibo objbo = new ticketbookingVehibo();
                objbo.Vehi_Ticket_id = vRow.Veh_Ticket_ID;
                objbo.Vehicle_req_id= vRow.Vehicle_req_id;
                objbo.REQUISITION_ID= (int)vRow.REQUISITION_ID;
                objbo.REQ_SEGMENT_ID=(int) vRow.REQ_SEGMENT_ID;
                objbo.PROPOSAL_ID= vRow.PROPOSAL_ID;
                objbo.PRO_SEGMENT_ID= vRow.PRO_SEGMENT_ID;
                objbo.EMPLOYEE_NO= vRow.EMPLOYEE_NO;
                objbo.Vehicle_Source= vRow.Vehicle_Source;
                objbo.Agent_Name= vRow.Agent_Name;
                objbo.Booking_passed_to= vRow.Booking_passed_to;
                objbo.Vehicle_Num= vRow.Vehicle_Num;
                objbo.Rate_kms= vRow.Rate_kms;
                objbo.Driver_Name= vRow.Driver_Name;
                objbo.Contact_Number= vRow.Contact_Number;
                objbo.Statutory_Req= vRow.Statutory_Req;
                objbo.Agent_BillNum= vRow.Agent_BillNum;
                objbo.Bill_Date=(DateTime)vRow.Bill_Date;
                objbo.Agent_BillAmnt= vRow.Agent_BillAmnt;
                objbo.Driver_Batta= vRow.Driver_Batta;
                objbo.Total_km= vRow.Total_km;
                objbo.Total_Cost= vRow.Total_Cost;
                objbo.created_by= vRow.created_by;
                objbo.created_on=(DateTime) vRow.created_on;
                objbo.modified_by= vRow.modified_by;
                objbo.modified_on=(DateTime) vRow.modified_on;
                objbo.isActive= (bool)vRow.isActive;
                objbo.CurrentStatus = vRow.CurrentStatus;
                //if (!string.IsNullOrEmpty(vRow.CurrentStatus))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.CurrentStatus);
                //    objbo.CurrentStatus = Convert.ToString(Status);
                //}
                objlist.Add(objbo);
            }
            return objlist;
        }

        public List<ticketbookingVehibo> fun_Get_ALL_Vehi_Ticket_Booked_Details(string strRole,int iVehiID)
        {
            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();
            List<ticketbookingVehibo> objlist = new List<ticketbookingVehibo>();

            foreach (var vRow in objdatacontext.sp_get_ALL_Vehi_Ticket_Booked_details(strRole, iVehiID))
            {
                ticketbookingVehibo objbo = new ticketbookingVehibo();

                objbo.Vehi_Ticket_id = vRow.Veh_Ticket_ID;
                objbo.Vehicle_req_id = vRow.Vehicle_req_id;
                objbo.REQUISITION_ID = (int)vRow.REQUISITION_ID;
                objbo.REQ_SEGMENT_ID = (int)vRow.REQ_SEGMENT_ID;
                objbo.PROPOSAL_ID = vRow.PROPOSAL_ID;
                objbo.PRO_SEGMENT_ID = vRow.PRO_SEGMENT_ID;
                objbo.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                objbo.Vehicle_Source = vRow.Vehicle_Source;
                objbo.Agent_Name = vRow.Agent_Name;
                objbo.Booking_passed_to = vRow.Booking_passed_to;
                objbo.Vehicle_Num = vRow.Vehicle_Num;
                objbo.Rate_kms = vRow.Rate_kms;
                objbo.Driver_Name = vRow.Driver_Name;
                objbo.Contact_Number = vRow.Contact_Number;
                objbo.Statutory_Req = vRow.Statutory_Req;
                objbo.Agent_BillNum = vRow.Agent_BillNum;
                objbo.Bill_Date = (DateTime)vRow.Bill_Date;
                objbo.Agent_BillAmnt = vRow.Agent_BillAmnt;
                objbo.Driver_Batta = vRow.Driver_Batta;
                objbo.Total_km = vRow.Total_km;
                objbo.Total_Cost = vRow.Total_Cost;
                objbo.created_by = vRow.created_by;
                objbo.created_on = (DateTime)vRow.created_on;
                objbo.modified_by = vRow.modified_by;
                objbo.modified_on = (DateTime)vRow.modified_on;
                objbo.isActive = (bool)vRow.isActive;
                objbo.CurrentStatus = vRow.CurrentStatus;
                objbo.Net_Amnt = vRow.Net_Amnt;
                //if (!string.IsNullOrEmpty(vRow.CurrentStatus))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.CurrentStatus);
                //    objbo.CurrentStatus = Convert.ToString(Status);
                //}
                objlist.Add(objbo);
            }
            return objlist;
        }

        public int Cancel_Vehicle_Travel_ticket_(int iRequisiID, int iRequisiSegmnID, string strCNNo, DateTime dtCNDate, string strCNAmnt, string strNetAmnt)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_cancel_VehiTicket_updateStatus(iRequisiID, iRequisiSegmnID, strCNNo, dtCNDate, strCNAmnt, strNetAmnt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public List<ticketbookingAccombo> fun_Get_Accm_Ticket_Booked_Details()
        {
            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();
            List<ticketbookingAccombo> objlist = new List<ticketbookingAccombo>();

            foreach (var vRow in objdatacontext.sp_get_Accomm_Ticket_Booked_details())
            {
                ticketbookingAccombo objbo = new ticketbookingAccombo();

                objbo.Ticket_ID = vRow.Ticket_ID;
                objbo.Accommadation_req_id = vRow.Accommadation_req_id;
                objbo.REQUISITION_ID = (int)vRow.REQUISITION_ID;
                objbo.REQ_SEGMENT_ID = (int)vRow.REQ_SEGMENT_ID;
                objbo.PROPOSAL_ID = vRow.PROPOSAL_ID;
                objbo.PRO_SEGMENT_ID = vRow.PRO_SEGMENT_ID;
                objbo.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                objbo.Payment = vRow.Payment;
                objbo.Tariff = vRow.Tariff;
                objbo.Booking_given_to = vRow.Booking_given_to;
                objbo.Hotel_Invoice_Num = vRow.Hotel_Invoice_Num;
                objbo.Bill_date =(DateTime) vRow.Bill_date;
                objbo.Amount = vRow.Amount;
                objbo.created_by = vRow.created_by;
                objbo.created_on = (DateTime)vRow.created_on;
                objbo.modified_by = vRow.modified_by;
                objbo.modified_on = (DateTime)vRow.modified_on;
                objbo.isActive = (bool)vRow.isActive;
                objbo.CurrentStatus = vRow.CurrentStatus;
                objbo.Agent_Name = vRow.Agent_Name;


                //if (!string.IsNullOrEmpty(vRow.CurrentStatus))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.CurrentStatus);
                //    objbo.CurrentStatus = Convert.ToString(Status);
                //}
                objlist.Add(objbo);
            }
            return objlist;
        }

        public List<ticketbookingAccombo> fun_Get_ALL_Accm_Ticket_Booked_Details()
        {
            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();
            List<ticketbookingAccombo> objlist = new List<ticketbookingAccombo>();

            foreach (var vRow in objdatacontext.sp_get_ALL_Accomm_Ticket_Booked_details())
            {
                ticketbookingAccombo objbo = new ticketbookingAccombo();

                objbo.Ticket_ID = vRow.Ticket_ID;
                objbo.Accommadation_req_id = vRow.Accommadation_req_id;
                objbo.REQUISITION_ID = (int)vRow.REQUISITION_ID;
                objbo.REQ_SEGMENT_ID = (int)vRow.REQ_SEGMENT_ID;
                objbo.PROPOSAL_ID = vRow.PROPOSAL_ID;
                objbo.PRO_SEGMENT_ID = vRow.PRO_SEGMENT_ID;
                objbo.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                objbo.Payment = vRow.Payment;
                objbo.Tariff = vRow.Tariff;
                objbo.Booking_given_to = vRow.Booking_given_to;
                objbo.Hotel_Invoice_Num = vRow.Hotel_Invoice_Num;
                objbo.Bill_date = (DateTime)vRow.Bill_date;
                objbo.Amount = vRow.Amount;
                objbo.created_by = vRow.created_by;
                objbo.created_on = (DateTime)vRow.created_on;
                objbo.modified_by = vRow.modified_by;
                objbo.modified_on = (DateTime)vRow.modified_on;
                objbo.isActive = (bool)vRow.isActive;
                objbo.CurrentStatus = vRow.CurrentStatus;
                objbo.Net_Amnt = vRow.Net_Amnt;

                //if (!string.IsNullOrEmpty(vRow.CurrentStatus))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.CurrentStatus);
                //    objbo.CurrentStatus = Convert.ToString(Status);
                //}
                objlist.Add(objbo);
            }
            return objlist;
        }

        public int Cancel_Accomm_Travel_ticket_(int iRequisiID, int iRequisiSegmnID, string strCNNo, DateTime dtCNDate, string strCNAmnt, string strNetAmnt)
        {
            int iResult = -1;
            try
            {
                ticketbookingDataContext objdatacontext = new ticketbookingDataContext();

                iResult = objdatacontext.sp_cancel_AccmmTicket_updateStatus(iRequisiID, iRequisiSegmnID, strCNNo, dtCNDate, strCNAmnt, strNetAmnt);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public string Get_TravelRemarks_ForCancelation(int iReqID,int iREeSegID)
        {
            ticketbookingDataContext objdatacontext = new ticketbookingDataContext();
            string strReasonToCancel = "";

            foreach (var vRow in objdatacontext.sp_get_TravelRemarks(iReqID,iREeSegID))
            {
                strReasonToCancel = vRow.REASON_FOR_CANCEL;
            }
            return strReasonToCancel;
        }
    }
}
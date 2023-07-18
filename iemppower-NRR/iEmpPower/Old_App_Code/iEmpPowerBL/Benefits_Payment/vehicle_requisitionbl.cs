using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Local_requisition;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment
{
    public class vehicle_requisitionbl
    {
        public int Create_vehicle_requisitionbl(vehicle_requisitionbo objbo)
        {
            int iResult = -1;
            try
            {
                Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();

                iResult = objcontext.sp_create_vehicle_requisition(objbo.REQUEST_ID, objbo.REQ_SEGMENT_ID_FROM_TRAVEL_REQUEST , objbo.EMPLOYEE_NO, objbo.Date_of_travel, objbo.Duration_from,
                objbo.Duration_to, objbo.Departure_from, objbo.Destination_to, objbo.Purpose_of_travel, 
                objbo.Carrying_any_materials, objbo.Pickup_time,objbo.Pickup_address,objbo.Drop_time,objbo.Drop_address,
                objbo.Vehicle_type, objbo.Vehicle_category, objbo.Vehicle_name, objbo.Additional_services, objbo.remarks,objbo.current_status, objbo.STATUS_UPDATED_BY, objbo.CRAETEDBY, objbo.CREATEDON,
                objbo.status);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public int Create_Sepvehicle_Proposalbl(vehicle_requisitionbo objbo)
        {
            int iResult = -1;
            try
            {
                Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();

                iResult = objcontext.sp_create_Sep_vehicle_proposal(objbo.Vehicle_req_id, objbo.REQUEST_ID, objbo.REQ_SEGMENT_ID_FROM_TRAVEL_REQUEST, objbo.EMPLOYEE_NO, objbo.Date_of_travel, objbo.Duration_from,
                objbo.Duration_to, objbo.Departure_from, objbo.Destination_to, objbo.Purpose_of_travel,
                objbo.Carrying_any_materials, objbo.Pickup_time, objbo.Pickup_address, objbo.Drop_time, objbo.Drop_address,
                objbo.Vehicle_type, objbo.Vehicle_category, objbo.Vehicle_name, objbo.Additional_services, objbo.remarks, objbo.current_status, objbo.STATUS_UPDATED_BY, objbo.CRAETEDBY, objbo.CREATEDON,
                objbo.status);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public int Delete_vehicle_requisitionbl(int RequisitionID, int RequisitionsegmentID, int TravelType)
        {
            int iResult = -1;
            try
            {
                Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
                iResult = objcontext.sp_delete_vehicle_requisition(RequisitionID, RequisitionsegmentID, TravelType);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public List<vehicle_requisitionbo> Get_Vehicle_Details(int RequisitionID, int RequisitionSegmentID)
        {
            int VehicleID = 1;
            List<vehicle_requisitionbo> VehicleRequisitionboList = new List<vehicle_requisitionbo>();
            Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_vehicle_requisition_details(RequisitionID, RequisitionSegmentID))
            {
                vehicle_requisitionbo vehicle_requisitionBO = new vehicle_requisitionbo();
                vehicle_requisitionBO.Vehicle_req_id = vRow.Vehicle_req_id;// VehicleID;
                vehicle_requisitionBO.Additional_services = vRow.Additional_services;
                vehicle_requisitionBO.Carrying_any_materials = vRow.Carrying_any_materials;
                vehicle_requisitionBO.CRAETEDBY = vRow.created_by;
                vehicle_requisitionBO.CREATEDON =Convert.ToDateTime(vRow.created_on);
                vehicle_requisitionBO.current_status = vRow.current_status;
                vehicle_requisitionBO.Departure_from = vRow.DEPARTURE_FROM;
                vehicle_requisitionBO.Destination_to = vRow.DEPARTURE_TO;
                vehicle_requisitionBO.Drop_address = vRow.DROP_ADDRESS;
                vehicle_requisitionBO.Drop_time = vRow.Drop_time;
                vehicle_requisitionBO.Duration_from = vRow.DURATION_FROM;
                vehicle_requisitionBO.Duration_to = vRow.DURATION_TO;
                vehicle_requisitionBO.status = (bool)vRow.isActive;
                vehicle_requisitionBO.MODIFIEDBY = vRow.modified_by;
                vehicle_requisitionBO.MODIFIEDON = Convert.ToDateTime(vRow.modified_on);
                vehicle_requisitionBO.Pickup_address = vRow.PICKUP_ADDRESS;
                vehicle_requisitionBO.Pickup_time = vRow.PICKUP_TIME;
                vehicle_requisitionBO.Purpose_of_travel = vRow.PURPOSE_OF_TRAVEL;
                vehicle_requisitionBO.remarks = vRow.Remarks;
                vehicle_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                vehicle_requisitionBO.REQUEST_ID = vRow.TRIP_NO;
                vehicle_requisitionBO.REQ_SEGMENT_ID = (int)vRow.TRIP_SEGMENT_ID;
                vehicle_requisitionBO.Vehicle_name = vRow.VEC_NAME;
                //vehicle_requisitionBO.VehicleNameAssigned = vRow.ZZVEHNAM;
                vehicle_requisitionBO.Vehicle_type = vRow.VEC_TYPE;
                vehicle_requisitionBO.Vehicle_category = vRow.VEC_CLASS;
                vehicle_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                vehicle_requisitionBO.EMPLOYEE_NO = vRow.PERNR;
                //if (!string.IsNullOrEmpty(vRow.current_status))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                //    vehicle_requisitionBO.current_status = Convert.ToString(Status);
                //}
                VehicleID++;
                VehicleRequisitionboList.Add(vehicle_requisitionBO);
            }
            return VehicleRequisitionboList;
        }

        public List<vehicle_requisitionbo> Get_OutVehicle_Details_TicketBooked(int VehiReqID, int RequisitionSegmentID )
        {
            int VehicleID = 1;
            List<vehicle_requisitionbo> VehicleRequisitionboList = new List<vehicle_requisitionbo>();
            Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_OutLocalVehicle_requisition_details(VehiReqID, RequisitionSegmentID ))
            {
                vehicle_requisitionbo vehicle_requisitionBO = new vehicle_requisitionbo();
                vehicle_requisitionBO.Vehicle_req_id = vRow.Vehicle_req_id;// VehicleID;
                vehicle_requisitionBO.Additional_services = vRow.Additional_services;
                vehicle_requisitionBO.Carrying_any_materials = vRow.Carrying_any_materials;
                vehicle_requisitionBO.CRAETEDBY = vRow.created_by;
                vehicle_requisitionBO.CREATEDON = Convert.ToDateTime(vRow.created_on);
                vehicle_requisitionBO.current_status = vRow.current_status;
                vehicle_requisitionBO.Departure_from = vRow.DEPARTURE_FROM;
                vehicle_requisitionBO.Destination_to = vRow.DEPARTURE_TO;
                vehicle_requisitionBO.Drop_address = vRow.DROP_ADDRESS;
                vehicle_requisitionBO.Drop_time = vRow.Drop_time;
                vehicle_requisitionBO.Duration_from = vRow.DURATION_FROM;
                vehicle_requisitionBO.Duration_to = vRow.DURATION_TO;
                vehicle_requisitionBO.status = (bool)vRow.isActive;
                vehicle_requisitionBO.MODIFIEDBY = vRow.modified_by;
                vehicle_requisitionBO.MODIFIEDON = Convert.ToDateTime(vRow.modified_on);
                vehicle_requisitionBO.Pickup_address = vRow.PICKUP_ADDRESS;
                vehicle_requisitionBO.Pickup_time = vRow.PICKUP_TIME;
                vehicle_requisitionBO.Purpose_of_travel = vRow.PURPOSE_OF_TRAVEL;
                vehicle_requisitionBO.remarks = vRow.Remarks;
                vehicle_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                vehicle_requisitionBO.REQUEST_ID = vRow.TRIP_NO;
                vehicle_requisitionBO.REQ_SEGMENT_ID = (int)vRow.TRIP_SEGMENT_ID;
                vehicle_requisitionBO.Vehicle_name = vRow.VEC_NAME;
                //vehicle_requisitionBO.VehicleNameAssigned = vRow.ZZVEHNAM;
                vehicle_requisitionBO.Vehicle_type = vRow.VEC_TYPE;
                vehicle_requisitionBO.Vehicle_category = vRow.VEC_CLASS;
                vehicle_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                vehicle_requisitionBO.EMPLOYEE_NO = vRow.PERNR;
                //if (!string.IsNullOrEmpty(vRow.current_status))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                //    vehicle_requisitionBO.current_status = Convert.ToString(Status);
                //}
                VehicleID++;
                VehicleRequisitionboList.Add(vehicle_requisitionBO);
            }
            return VehicleRequisitionboList;
        }

        public List<vehicle_requisitionbo> Get_TravelReq_Vehicle_Details_TicketBooked(int VehiReqID, int RequisitionSegmentID)
        {
            int VehicleID = 1;
            List<vehicle_requisitionbo> VehicleRequisitionboList = new List<vehicle_requisitionbo>();
            Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_vehicle_TravelReq_details(VehiReqID, RequisitionSegmentID))
            {
                vehicle_requisitionbo vehicle_requisitionBO = new vehicle_requisitionbo();
                vehicle_requisitionBO.Vehicle_req_id = vRow.Vehicle_req_id;// VehicleID;
                vehicle_requisitionBO.Additional_services = vRow.Additional_services;
                vehicle_requisitionBO.Carrying_any_materials = vRow.Carrying_any_materials;
                vehicle_requisitionBO.CRAETEDBY = vRow.created_by;
                vehicle_requisitionBO.CREATEDON = Convert.ToDateTime(vRow.created_on);
                vehicle_requisitionBO.current_status = vRow.current_status;
                vehicle_requisitionBO.Departure_from = vRow.DEPARTURE_FROM;
                vehicle_requisitionBO.Destination_to = vRow.DEPARTURE_TO;
                vehicle_requisitionBO.Drop_address = vRow.DROP_ADDRESS;
                vehicle_requisitionBO.Drop_time = vRow.Drop_time;
                vehicle_requisitionBO.Duration_from = vRow.DURATION_FROM;
                vehicle_requisitionBO.Duration_to = vRow.DURATION_TO;
                vehicle_requisitionBO.status = (bool)vRow.isActive;
                vehicle_requisitionBO.MODIFIEDBY = vRow.modified_by;
                vehicle_requisitionBO.MODIFIEDON = Convert.ToDateTime(vRow.modified_on);
                vehicle_requisitionBO.Pickup_address = vRow.PICKUP_ADDRESS;
                vehicle_requisitionBO.Pickup_time = vRow.PICKUP_TIME;
                vehicle_requisitionBO.Purpose_of_travel = vRow.PURPOSE_OF_TRAVEL;
                vehicle_requisitionBO.remarks = vRow.Remarks;
                vehicle_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                vehicle_requisitionBO.REQUEST_ID = vRow.TRIP_NO;
                vehicle_requisitionBO.REQ_SEGMENT_ID = (int)vRow.TRIP_SEGMENT_ID;
                vehicle_requisitionBO.Vehicle_name = vRow.VEC_NAME;
                //vehicle_requisitionBO.VehicleNameAssigned = vRow.ZZVEHNAM;
                vehicle_requisitionBO.Vehicle_type = vRow.VEC_TYPE;
                vehicle_requisitionBO.Vehicle_category = vRow.VEC_CLASS;
                vehicle_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                vehicle_requisitionBO.EMPLOYEE_NO = vRow.PERNR;
                //if (!string.IsNullOrEmpty(vRow.current_status))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                //    vehicle_requisitionBO.current_status = Convert.ToString(Status);
                //}
                VehicleID++;
                VehicleRequisitionboList.Add(vehicle_requisitionBO);
            }
            return VehicleRequisitionboList;
        }

        public List<vehicle_requisitionbo> Get_Vehicle_Separate_Details(int RequisitionID, int RequisitionSegmentID, string strEmploeeNo,string strStatus)
        {
            int VehicleID = 1;
            List<vehicle_requisitionbo> VehicleRequisitionboList = new List<vehicle_requisitionbo>();
            Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_vehicle_SeparateReq_details(RequisitionID, RequisitionSegmentID, strEmploeeNo,strStatus))
            {
                vehicle_requisitionbo vehicle_requisitionBO = new vehicle_requisitionbo();
                vehicle_requisitionBO.Vehicle_req_id = vRow.Vehicle_req_id;// VehicleID;
                vehicle_requisitionBO.Additional_services = vRow.Additional_services;
                vehicle_requisitionBO.Carrying_any_materials = vRow.Carrying_any_materials;
                vehicle_requisitionBO.CRAETEDBY = vRow.created_by;
                vehicle_requisitionBO.CREATEDON = Convert.ToDateTime(vRow.created_on);
                vehicle_requisitionBO.current_status = vRow.current_status;
                vehicle_requisitionBO.Departure_from = vRow.DEPARTURE_FROM;
                vehicle_requisitionBO.Destination_to = vRow.DEPARTURE_TO;
                vehicle_requisitionBO.Drop_address = vRow.DROP_ADDRESS;
                vehicle_requisitionBO.Drop_time = vRow.Drop_time;
                vehicle_requisitionBO.Duration_from = vRow.DURATION_FROM;
                vehicle_requisitionBO.Duration_to = vRow.DURATION_TO;
                vehicle_requisitionBO.status = (bool)vRow.isActive;
                vehicle_requisitionBO.MODIFIEDBY = vRow.modified_by;
                vehicle_requisitionBO.MODIFIEDON = Convert.ToDateTime(vRow.modified_on);
                vehicle_requisitionBO.Pickup_address = vRow.PICKUP_ADDRESS;
                vehicle_requisitionBO.Pickup_time = vRow.PICKUP_TIME;
                vehicle_requisitionBO.Purpose_of_travel = vRow.PURPOSE_OF_TRAVEL;
                vehicle_requisitionBO.remarks = vRow.Remarks;
                vehicle_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                vehicle_requisitionBO.REQUEST_ID = vRow.TRIP_NO;
                vehicle_requisitionBO.REQ_SEGMENT_ID = (int)vRow.TRIP_SEGMENT_ID;
                vehicle_requisitionBO.Vehicle_name = vRow.VEC_NAME;
                //vehicle_requisitionBO.VehicleNameAssigned = vRow.ZZVEHNAM;
                vehicle_requisitionBO.Vehicle_type = vRow.VEC_TYPE;
                vehicle_requisitionBO.Vehicle_category = vRow.VEC_CLASS;
                vehicle_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                vehicle_requisitionBO.EMPLOYEE_NO = vRow.PERNR;
                vehicle_requisitionBO.REASON_FOR_CANCEL = vRow.Reason_for_cancel;
                //if (!string.IsNullOrEmpty(vRow.current_status))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                //    vehicle_requisitionBO.current_status = Convert.ToString(Status);
                //}
                if (strStatus == "New" || strStatus == "6")
                {
                    Local_travel_requisitionDataContext obj = new Local_travel_requisitionDataContext();

                    foreach (var vRow1 in obj.sp_load_vehiceType_code(vRow.VEC_TYPE))
                    {
                        vehicle_requisitionBO.Vehicle_type = vRow1.FZTXT;
                    }
                    foreach (var vRow2 in obj.sp_load_VehicleClass_code(vRow.VEC_CLASS))
                    {
                        vehicle_requisitionBO.Vehicle_category = vRow2.TEXT25;
                    }
                    if (!(vRow.VEC_NAME == "" || vRow.VEC_NAME == string.Empty || vRow.VEC_NAME == "&nbsp;"))
                    {
                        foreach (var vRow3 in obj.sp_load_vehicle_names_code(Convert.ToInt32(vRow.VEC_NAME)))
                        {
                            vehicle_requisitionBO.Vehicle_name = vRow3.ZZVEHNAM;
                        }
                    }
                }

                VehicleID++;
                VehicleRequisitionboList.Add(vehicle_requisitionBO);
            }
            return VehicleRequisitionboList;
        }

        public List<vehicle_requisitionbo> Get_Vehicle_Details_History(int RequisitionID, int RequisitionSegmentID)
        {
            int VehicleID = 1;
            List<vehicle_requisitionbo> VehicleRequisitionboList = new List<vehicle_requisitionbo>();
            Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_vehicle_requisition_History(RequisitionID, RequisitionSegmentID))
            {
                vehicle_requisitionbo vehicle_requisitionBO = new vehicle_requisitionbo();
                vehicle_requisitionBO.Vehicle_req_id = vRow.Vehicle_req_id;// VehicleID;
                vehicle_requisitionBO.Additional_services = vRow.Additional_services;
                vehicle_requisitionBO.Carrying_any_materials = vRow.Carrying_any_materials;
                vehicle_requisitionBO.CRAETEDBY = vRow.created_by;
                vehicle_requisitionBO.CREATEDON = Convert.ToDateTime(vRow.created_on);
                vehicle_requisitionBO.current_status = vRow.current_status;
                vehicle_requisitionBO.Departure_from = vRow.DEPARTURE_FROM;
                vehicle_requisitionBO.Destination_to = vRow.DEPARTURE_TO;
                vehicle_requisitionBO.Drop_address = vRow.DROP_ADDRESS;
                vehicle_requisitionBO.Drop_time = vRow.Drop_time;
                vehicle_requisitionBO.Duration_from = vRow.DURATION_FROM;
                vehicle_requisitionBO.Duration_to = vRow.DURATION_TO;
                vehicle_requisitionBO.status = (bool)vRow.isActive;
                vehicle_requisitionBO.MODIFIEDBY = vRow.modified_by;
                vehicle_requisitionBO.MODIFIEDON = Convert.ToDateTime(vRow.modified_on);
                vehicle_requisitionBO.Pickup_address = vRow.PICKUP_ADDRESS;
                vehicle_requisitionBO.Pickup_time = vRow.PICKUP_TIME;
                vehicle_requisitionBO.Purpose_of_travel = vRow.PURPOSE_OF_TRAVEL;
                vehicle_requisitionBO.remarks = vRow.Remarks;
                vehicle_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                vehicle_requisitionBO.REQUEST_ID = vRow.TRIP_NO;
                vehicle_requisitionBO.REQ_SEGMENT_ID = (int)vRow.TRIP_SEGMENT_ID;
                vehicle_requisitionBO.Vehicle_name = vRow.VEC_NAME;
                //vehicle_requisitionBO.VehicleNameAssigned = vRow.ZZVEHNAM;
                vehicle_requisitionBO.Vehicle_type = vRow.VEC_TYPE;
                vehicle_requisitionBO.Vehicle_category = vRow.VEC_CLASS;
                vehicle_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                vehicle_requisitionBO.EMPLOYEE_NO = vRow.PERNR;
                //if (!string.IsNullOrEmpty(vRow.current_status))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                //    vehicle_requisitionBO.current_status = Convert.ToString(Status);
                //}
                VehicleID++;
                VehicleRequisitionboList.Add(vehicle_requisitionBO);
            }
            return VehicleRequisitionboList;
        }

        public List<vehicle_requisitionbo> Get_Traveller_Vehicle_Details(string strPERNR )
        { 
            List<vehicle_requisitionbo> VehicleRequisitionboList = new List<vehicle_requisitionbo>();
            Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_traveller_vehicle(strPERNR))
            {
                vehicle_requisitionbo vehicle_requisitionBO = new vehicle_requisitionbo();
                vehicle_requisitionBO.Vehicle_req_id = vRow.Vehicle_req_id;              
                vehicle_requisitionBO.REQUEST_ID = vRow.TRIP_NO;
                vehicle_requisitionBO.REQ_SEGMENT_ID = (int)vRow.TRIP_SEGMENT_ID;
                vehicle_requisitionBO.EMPLOYEE_NO = vRow.PERNR;
                vehicle_requisitionBO.Date_of_travel = vRow.DATE_BEG;
                vehicle_requisitionBO.Duration_from = vRow.DURATION_FROM;
                vehicle_requisitionBO.Duration_to = vRow.DURATION_TO;
                vehicle_requisitionBO.Departure_from = vRow.DEPARTURE_FROM;
                vehicle_requisitionBO.Destination_to = vRow.DEPARTURE_TO;
                vehicle_requisitionBO.Purpose_of_travel = vRow.PURPOSE_OF_TRAVEL;
                vehicle_requisitionBO.Carrying_any_materials = vRow.Carrying_any_materials;
                vehicle_requisitionBO.Pickup_time = vRow.PICKUP_TIME;
                vehicle_requisitionBO.Pickup_address = vRow.PICKUP_ADDRESS;
                vehicle_requisitionBO.Drop_time = vRow.Drop_time;
                vehicle_requisitionBO.Drop_address = vRow.DROP_ADDRESS;
                vehicle_requisitionBO.Vehicle_type = vRow.VEC_TYPE;
                vehicle_requisitionBO.Vehicle_category = vRow.VEC_CLASS;
                vehicle_requisitionBO.Vehicle_name = vRow.VEC_NAME;
                vehicle_requisitionBO.Additional_services = vRow.Additional_services;
                vehicle_requisitionBO.remarks = vRow.Remarks;
                vehicle_requisitionBO.current_status = vRow.current_status;
                vehicle_requisitionBO.STATUS_UPDATED_BY = vRow.STATUS_UPDATED_BY;
                vehicle_requisitionBO.CRAETEDBY = vRow.created_by;
                vehicle_requisitionBO.CREATEDON = Convert.ToDateTime(vRow.created_on);
                vehicle_requisitionBO.MODIFIEDBY = vRow.modified_by;
                vehicle_requisitionBO.MODIFIEDON = Convert.ToDateTime(vRow.modified_on);
                vehicle_requisitionBO.status = (bool)vRow.isActive;
                //if (!string.IsNullOrEmpty(vRow.current_status))
                //{
                //    ReuisitionStatus Status = (ReuisitionStatus)Convert.ToInt32(vRow.current_status);
                //    vehicle_requisitionBO.current_status = Convert.ToString(Status);
                //}
                VehicleRequisitionboList.Add(vehicle_requisitionBO);
            }
            return VehicleRequisitionboList;
        }

        public int HODStatusUpdateSeparateVehi(vehicle_requisitionbo objBo, string strOutLocal)
        {
            int iResult = -1;
            try
            {
                Vehicle_requisitionDataContext objDataContext = new Vehicle_requisitionDataContext();
                iResult = objDataContext.sp_update_HOD_status_SeparateVehi(objBo.Vehicle_req_id, objBo.remarks, objBo.current_status, objBo.MODIFIEDBY, strOutLocal);
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
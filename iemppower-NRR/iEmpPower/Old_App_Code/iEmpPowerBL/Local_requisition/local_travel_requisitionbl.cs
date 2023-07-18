using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Local_requisition;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
namespace iEmpPower.Old_App_Code.iEmpPowerBL.Local_requisition
{
    public class local_travel_requisitionbl
    {
        public int Create_Local_travel_requisitionbl(vehicle_requisitionbo objbo)
        {
            int iResult = -1;
            try
            {
                Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();

                iResult = objcontext.sp_create_local_travel_requisition(objbo.EMPLOYEE_NO, objbo.TRAVEL_TYPE, objbo.Departure_from, objbo.Destination_to, 
                    objbo.Vehicle_type, objbo.Vehicle_category, objbo.Vehicle_name,
               objbo.Pickup_time, objbo.Pickup_address, objbo.Drop_time, objbo.Drop_address,
                objbo.Purpose_of_travel, objbo.Number_of_members, objbo.Name_of_members, objbo.remarks, objbo.current_status, objbo.STATUS_UPDATED_BY,
                objbo.CRAETEDBY, objbo.CREATEDON, objbo.IsActive,objbo.Date_of_travel,objbo.To_Date);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public List<vehicle_requisitionbo> Get_Local_travel_Details(string traveltype, string empno,string strRole)
        {
            List<vehicle_requisitionbo> VehicleRequisitionboList = new List<vehicle_requisitionbo>();
            Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_Local_travel_details(traveltype, empno,strRole))
            {
                vehicle_requisitionbo vehicle_requisitionBO = new vehicle_requisitionbo();
                vehicle_requisitionBO.EMPLOYEE_NO = vRow.Employee_no;
                vehicle_requisitionBO.local_travel_req_id = vRow.Local_travel_req_Id;
                vehicle_requisitionBO.TRAVEL_TYPE = vRow.Type_of_travel;
                vehicle_requisitionBO.Departure_from = vRow.From;
                vehicle_requisitionBO.Destination_to = vRow.To;
                vehicle_requisitionBO.Drop_time = vRow.Drop_time;
                vehicle_requisitionBO.Drop_address = vRow.Drop_adress;
                vehicle_requisitionBO.Pickup_address = vRow.PICKUP_ADDRESS;
                vehicle_requisitionBO.Pickup_time = vRow.PICKUP_TIME;
                vehicle_requisitionBO.remarks = vRow.Remarks;
                vehicle_requisitionBO.Vehicle_name = vRow.VehicleName;
                vehicle_requisitionBO.VehicleId = vRow.VehicleId ;
                vehicle_requisitionBO.Vehicle_type = vRow.VehicleTypeName;
                vehicle_requisitionBO.VehicleTypeId = vRow.VehicleTypeId ;
                vehicle_requisitionBO.Vehicle_category = vRow.VehicleCategoryId;
                vehicle_requisitionBO.VehicleCategoryId = vRow.VehicleCategoryId; 
                vehicle_requisitionBO.Number_of_members = Convert.ToInt32(vRow.Number_of_members);
                vehicle_requisitionBO.Purpose_of_travel = vRow.Purpose_of_travel;
                vehicle_requisitionBO.current_status =Convert.ToString(Enum.Parse(typeof(ReuisitionStatus),vRow.current_status));
                vehicle_requisitionBO.Date_of_travel = Convert.ToDateTime(vRow.From_date);
                vehicle_requisitionBO.To_Date = Convert.ToDateTime(vRow.To_date);
                VehicleRequisitionboList.Add(vehicle_requisitionBO);
            }
            return VehicleRequisitionboList;
        }

        public int Update_local_requisition_details(vehicle_requisitionbo objbo)
        {
            int iResult = -1;
            try
            {
                Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
                iResult = objcontext.sp_update_Local_travel_details(objbo.local_travel_req_id, objbo.EMPLOYEE_NO, objbo.TRAVEL_TYPE, objbo.Departure_from,
                objbo.Destination_to, objbo.Vehicle_type, objbo.Vehicle_category, objbo.Vehicle_name,objbo.Pickup_time, objbo.Pickup_address, objbo.Drop_time,
                objbo.Drop_address,objbo.Purpose_of_travel, objbo.Number_of_members,objbo.Name_of_members, objbo.remarks, objbo.current_status, objbo.STATUS_UPDATED_BY,
                objbo.CRAETEDBY, objbo.CREATEDON);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }

        public List<vehicle_requisitionbo> Get_Local_travel_Group_members(int trave_req_id)
        {

            List<vehicle_requisitionbo> VehicleRequisitionboList = new List<vehicle_requisitionbo>();
            Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_local_travel_requistion_group_member_names(trave_req_id))
            {
                vehicle_requisitionbo vehicle_requisitionBO = new vehicle_requisitionbo();
               
                if (vRow.ENAME == null)
                {
                    vehicle_requisitionBO.Emp_name = vRow.Member_Id;
                    vehicle_requisitionBO.EMPLOYEE_NO = "";
                }
                else
                {
                    vehicle_requisitionBO.EMPLOYEE_NO = vRow.Member_Id;
                    vehicle_requisitionBO.Emp_name = vRow.ENAME;
                }
                VehicleRequisitionboList.Add(vehicle_requisitionBO);
            }
            return VehicleRequisitionboList;
        }

        public List<vehicle_requisitionbo> Get_Local_Vehicle_Separate_Details(  string strEmploeeNo, string strStatus)
        {
            List<vehicle_requisitionbo> VehicleRequisitionboList = new List<vehicle_requisitionbo>();
            Local_travel_requisitionDataContext objcontext = new Local_travel_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_Local_vehicle_SeparateReq_details( strEmploeeNo, strStatus))
            {
               vehicle_requisitionbo vehicle_requisitionBO = new vehicle_requisitionbo();
                vehicle_requisitionBO.EMPLOYEE_NO = vRow.Employee_no;
                vehicle_requisitionBO.local_travel_req_id = vRow.Local_travel_req_Id;
                vehicle_requisitionBO.TRAVEL_TYPE = vRow.Type_of_travel;
                vehicle_requisitionBO.Departure_from = vRow.From;
                vehicle_requisitionBO.Destination_to = vRow.To;
                vehicle_requisitionBO.Drop_time = vRow.Drop_time;
                vehicle_requisitionBO.Drop_address = vRow.Drop_adress;
                vehicle_requisitionBO.Pickup_address = vRow.PICKUP_ADDRESS;
                vehicle_requisitionBO.Pickup_time = vRow.PICKUP_TIME;
                vehicle_requisitionBO.remarks = vRow.Remarks;
                vehicle_requisitionBO.Vehicle_name = vRow.VEC_NAME;
                vehicle_requisitionBO.Vehicle_type = vRow.VEC_TYPE;
                vehicle_requisitionBO.Vehicle_category = vRow.VEC_CLASS;
                vehicle_requisitionBO.Number_of_members = Convert.ToInt32(vRow.Number_of_members);
                vehicle_requisitionBO.Purpose_of_travel = vRow.Purpose_of_travel;
                vehicle_requisitionBO.current_status = Convert.ToString(Enum.Parse(typeof(ReuisitionStatus), vRow.current_status));
                vehicle_requisitionBO.Duration_from = Convert.ToDateTime(vRow.From_date);
                vehicle_requisitionBO.Duration_to = Convert.ToDateTime(vRow.To_date);
                vehicle_requisitionBO.REASON_FOR_CANCEL = vRow.Reason_for_cancel;

                if (strStatus == "1" || strStatus == "6")
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
                VehicleRequisitionboList.Add(vehicle_requisitionBO);

            }
            return VehicleRequisitionboList;
        }

        public List<vehicle_requisitionbo> Get_Local_travel_Details_TicketBooked(int VehiReqID, int RequisitionSegmentID)
        {
            List<vehicle_requisitionbo> VehicleRequisitionboList = new List<vehicle_requisitionbo>();
            Vehicle_requisitionDataContext objcontext = new Vehicle_requisitionDataContext();
            foreach (var vRow in objcontext.sp_get_LocalVehicle_requisition_details(VehiReqID, RequisitionSegmentID))
            {
                vehicle_requisitionbo vehicle_requisitionBO = new vehicle_requisitionbo();
                vehicle_requisitionBO.EMPLOYEE_NO = vRow.Employee_no;
                vehicle_requisitionBO.local_travel_req_id = vRow.Local_travel_req_Id;
                vehicle_requisitionBO.TRAVEL_TYPE = vRow.Type_of_travel;
                vehicle_requisitionBO.Departure_from = vRow.From;
                vehicle_requisitionBO.Destination_to = vRow.To;
                vehicle_requisitionBO.Drop_time = vRow.Drop_time;
                vehicle_requisitionBO.Drop_address = vRow.Drop_adress;
                vehicle_requisitionBO.Pickup_address = vRow.PICKUP_ADDRESS;
                vehicle_requisitionBO.Pickup_time = vRow.PICKUP_TIME;
                vehicle_requisitionBO.remarks = vRow.Remarks;
                vehicle_requisitionBO.Vehicle_name = vRow.VEC_NAME;
                vehicle_requisitionBO.Vehicle_type = vRow.VEC_TYPE;
                vehicle_requisitionBO.Vehicle_category = vRow.VEC_CLASS;
                vehicle_requisitionBO.Number_of_members = Convert.ToInt32(vRow.Number_of_members);
                vehicle_requisitionBO.Purpose_of_travel = vRow.Purpose_of_travel;
                vehicle_requisitionBO.current_status = Convert.ToString(Enum.Parse(typeof(ReuisitionStatus), vRow.current_status));
                vehicle_requisitionBO.Duration_from = Convert.ToDateTime(vRow.From_date);
                vehicle_requisitionBO.Duration_to = Convert.ToDateTime(vRow.To_date);
                VehicleRequisitionboList.Add(vehicle_requisitionBO);
            }
            return VehicleRequisitionboList;
        }

   }
}
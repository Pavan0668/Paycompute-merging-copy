using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using System.Globalization;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;

  public class requisitions_traveldeskbl
    {
        public requisitions_traveldeskbl()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        public List<requisitionbo> Load_TravelRequistionDetailsforTravelDesk(string TravelType_Id,string TravelDate,string EmployeeName,string EmployeeId)
        {

            requisitions_traveldeskdalDataContext objrequisitions_traveldeskdalDataContex = new requisitions_traveldeskdalDataContext();
            List<requisitionbo> requisitionboList = new List<requisitionbo>();
            foreach (var vRow in objrequisitions_traveldeskdalDataContex.sp_get_travel_requisition_details_for_traveldesk(TravelType_Id, TravelDate,EmployeeId,EmployeeName))
            {
                requisitionbo requisitionboObj = new requisitionbo();
                requisitionboObj.FTPT_REQUEST_ID = vRow.REQUISITION_ID;
                requisitionboObj.REQ_SEGMENT_ID = vRow.REQ_SEGMENT_ID;
                requisitionboObj.CHK = vRow.TravelType;
                requisitionboObj.TRAVEL_DATE = Convert.ToDateTime(vRow.TRAVEL_DATE);
                requisitionboObj.MODE_OF_TRANSPOPRT_KZPMF = Convert.ToString(vRow.MODE_OF_TRANSPOPRT);
                requisitionboObj.MODE_OF_TRANSPOPRT_FZTXT = vRow.FZTXT;
                requisitionboObj.MEDIA_OF_CATEGORY_PKWKL = Convert.ToString(vRow.MEDIA_OF_CATEGORY);
                requisitionboObj.MEDIA_OF_CATEGORY_TEXT25 = vRow.TEXT25;
                requisitionboObj.REGION_TEXT25_FROM = vRow.From;  //from text
                requisitionboObj.REGION_TEXT25_TO = vRow.To;       //to text
                requisitionboObj.REGION_RGION_FROM = vRow.FromValue;    //from value
                requisitionboObj.REGION_RGION_TO = vRow.ToValue;        //to value
                requisitionboObj.TRAVEL_TIME = vRow.TIME_OF_DEPT;
                requisitionboObj.VEHICLE_NAME_VHNUM = vRow.VEHICLE_NUM;
                requisitionboObj.FLYNUM = vRow.FLYNUM;
                requisitionboObj.ADVANCE = vRow.ADVANCE_AMOUNT;
                requisitionboObj.AIRLINE = vRow.AIRLINE;
                requisitionboObj.VISA_REQUIRED_ALL = vRow.VISA_REQUIRED;
                requisitionboObj.FR_EXCHANGE = vRow.FR_EXCHANGE;
                requisitionboObj.INSUR_MEDICLAIM = vRow.INSUR_MEDICLAIM;
                requisitionboObj.SEAT_PREFERENCE = vRow.SEAT_PREFERENCE;
                requisitionboObj.MEAL_PREFERENCE = vRow.MEAL_PREFERENCE;
                requisitionboObj.BAGGAGE = vRow.BAGGAGE_COUNT;
                requisitionboObj.HAND = vRow.HAND_BAGGAGE_COUNT;
                requisitionboObj.REMARKS = vRow.TRAVELER_REMARKS;
                requisitionboObj.HOD_REMARKS = vRow.HOD_REMARKS;
                requisitionboObj.TD_REMARKS = vRow.TD_REMARKS;
                requisitionboObj.REASON_FOR_CANCEL = vRow.REASON_FOR_CANCEL;
                requisitionboObj.EMPLOYEE_NO = vRow.EMPLOYEE_NO;
                requisitionboObj.EMPLOYEE_NAME = Convert.ToString(vRow.ENAME);
                requisitionboObj.EMAIL = Convert.ToString(vRow.Email);
                requisitionboObj.ARRIVAL_DATE=vRow.TRIP_END_DATE.ToString();
                requisitionboObj.ARRIVAL_TIME = vRow.ARRIVAL_TIME;
                requisitionboObj.REASON = vRow.REASON;
                if (vRow.PhoneNumber != null)
                {
                    requisitionboObj.PHONE_NUMBER = Convert.ToString(vRow.PhoneNumber);
                }
                requisitionboObj.DESIGNATION = Convert.ToString(vRow.Designation);
                requisitionboList.Add(requisitionboObj);
            }
            return requisitionboList;
        }

        public int Update_TravelRequest_for_TravelDesk(requisitionbo objBo)
        {
            int iResult = -1;
            try
            {
                requisitions_traveldeskdalDataContext objrequisitions_traveldeskdalDataContex = new requisitions_traveldeskdalDataContext();
                iResult = objrequisitions_traveldeskdalDataContex.sp_update_requisition_mst_dtl_for_traveldesk(objBo.FTPT_REQUEST_ID, objBo.EMPLOYEE_NO, objBo.CHK,
                    objBo.REQ_SEGMENT_ID, objBo.TRAVEL_DATE, objBo.TRAVEL_TIME, objBo.MODE_OF_TRANSPOPRT_KZPMF, objBo.MEDIA_OF_CATEGORY_PKWKL,
                    Convert.ToInt32(objBo.VEHICLE_NAME_VHNUM), objBo.REGION_RGION_FROM, objBo.REGION_RGION_TO, objBo.FLYNUM, objBo.ADVANCE, objBo.AIRLINE,
                    objBo.VISA_REQUIRED_ALL, objBo.FR_EXCHANGE, objBo.INSUR_MEDICLAIM, objBo.SEAT_PREFERENCE, objBo.MEAL_PREFERENCE, objBo.BAGGAGE,
                    objBo.HAND, objBo.EMPLOYEE_NO, objBo.REMARKS,objBo.ARRIVAL_DATE,objBo.ARRIVAL_TIME,objBo.REASON);
                objrequisitions_traveldeskdalDataContex.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return iResult;
        }
        
        public visaPassportcollectionbo Load_VisaPassport_Expiry_DetailsforTravelDesk()
        {
            requisitions_traveldeskdalDataContext objRequisitionDataContext = new requisitions_traveldeskdalDataContext();
            visaPassportcollectionbo objVisaPassportList = new visaPassportcollectionbo();
            foreach (var vRow in objRequisitionDataContext.sp_Get_Visa_Passport_Expiry())
            {
                visaPassportBo objBo = new visaPassportBo();
                objBo.PERNR = vRow.PERNR;
                objBo.ICTYPE = vRow.ICTYPE;
                objBo.ICNUM = vRow.ICNUM;
                objBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
                objVisaPassportList.Add(objBo);
            }
            objRequisitionDataContext.Dispose();
            return objVisaPassportList;
        }
       
        public visaPassportcollectionbo Load_VisaPassport_DetailsforTravelDesk(string strempid)
        {
            pipersonalidsdalDataContext objRequisitionDataContext = new pipersonalidsdalDataContext();
            visaPassportcollectionbo objVisaPassportList = new visaPassportcollectionbo();
            foreach (var vRow in objRequisitionDataContext.sp_Get_Visa_Passport_Details(strempid))
            {
                visaPassportBo objBo = new visaPassportBo();
                objBo.PERNR = vRow.PERNR;
                objBo.ICTYPE = vRow.ICTXT;
                objBo.ICNUM = vRow.ICNUM;
                objBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
                objVisaPassportList.Add(objBo);
            }
            objRequisitionDataContext.Dispose();
            return objVisaPassportList;
        }


        public visaPassportcollectionbo Get_Visa(string strempid)
        {
            pipersonalidsdalDataContext objRequisitionDataContext = new pipersonalidsdalDataContext();
            visaPassportcollectionbo objVisaPassportList = new visaPassportcollectionbo();
            foreach (var vRow in objRequisitionDataContext.sp_get_master_ZTR_VISA(strempid))
            {
                visaPassportBo objBo = new visaPassportBo();
                objBo.PERNR = vRow.PERNR.ToString();
                objBo.VINUM = vRow.VINUM;
                objBo.PASNUM = vRow.PASNUM;
                objBo.EMPNAME = vRow.EMPNAME;
                objBo.COUNTRY = vRow.COUNTRY;
                objBo.DOI = Convert.ToDateTime(vRow.DOI);
                objBo.DOE = Convert.ToDateTime(vRow.DOE);
                objBo.VISA_TYPE = vRow.VISA_TYPE;
                objVisaPassportList.Add(objBo);
            }
            objRequisitionDataContext.Dispose();
            return objVisaPassportList;
        }

        public visaPassportcollectionbo Get_PASSPORT(string strempid)
        {
            pipersonalidsdalDataContext objRequisitionDataContext = new pipersonalidsdalDataContext();
            visaPassportcollectionbo objVisaPassportList = new visaPassportcollectionbo();
            foreach (var vRow in objRequisitionDataContext.sp_get_master_ZTR_PASSPORT(strempid))
            {
                visaPassportBo objBo = new visaPassportBo();
                objBo.PERNR = vRow.PERNR.ToString();
                objBo.EMPNAME = vRow.EMPNAME;
                objBo.PASNUM = vRow.PASNUM;
                objBo.DOI = Convert.ToDateTime(vRow.DOI);
                objBo.DOE = Convert.ToDateTime(vRow.DOE);
                objBo.PLISS = vRow.PLISS;
                objVisaPassportList.Add(objBo);
            }
            objRequisitionDataContext.Dispose();
            return objVisaPassportList;
        }

        public visaPassportcollectionbo Get_FLYER_NUMBER(string strempid)
        {
            pipersonalidsdalDataContext objRequisitionDataContext = new pipersonalidsdalDataContext();
            visaPassportcollectionbo objVisaPassportList = new visaPassportcollectionbo();
            foreach (var vRow in objRequisitionDataContext.sp_get_master_ZTR_FLYER_NUMBER(strempid))
            {
                visaPassportBo objBo = new visaPassportBo();
                objBo.PERNR = vRow.PERNR.ToString();
                objBo.FRFLYNUM = vRow.FRFLYNUM;
                objBo.EMPNAME = vRow.EMPNAME;
                objBo.AIRLINE = vRow.AIRLINE;
                objBo.VALSTATUS = vRow.VALSTATUS;
                objVisaPassportList.Add(objBo);
            }
            objRequisitionDataContext.Dispose();
            return objVisaPassportList;
        }

        public visaPassportcollectionbo Get_TRAVEL_INS(string strempid)
        {
            pipersonalidsdalDataContext objRequisitionDataContext = new pipersonalidsdalDataContext();
            visaPassportcollectionbo objVisaPassportList = new visaPassportcollectionbo();
            foreach (var vRow in objRequisitionDataContext.sp_get_master_ZTR_TRAVEL_INS(strempid))
            {
                visaPassportBo objBo = new visaPassportBo();
                objBo.PERNR = vRow.PERNR.ToString();
                objBo.TRINSNO = vRow.TRINSNO;
                objBo.EMPNAME = vRow.EMPNAME;
                objBo.DOI = Convert.ToDateTime(vRow.DOI);
                objBo.DOE = Convert.ToDateTime(vRow.DOE);
                objBo.PLAN1 = vRow.PLAN1;
                objBo.PREMIUM = vRow.PREMIUM;
                objBo.AGENT_NAME = vRow.AGENT_NAME;
                objVisaPassportList.Add(objBo);
            }
            objRequisitionDataContext.Dispose();
            return objVisaPassportList;
        }
        

    }

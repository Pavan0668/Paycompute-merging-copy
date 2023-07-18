using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.travellersfeedback;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.travellersfeedbackbl
{
    public class travellersfeedbackbl
    {
        public void Create_TravelFeedback(travellersfeedback objBo)
        {
            try
            {
                travellersfeedbackDataContext objcontext = new travellersfeedbackDataContext();
               int i= objcontext.sp_create_travellers_feedback(objBo.REQUISITION_ID,objBo.REQ_SEGMENT_ID ,objBo.pERNR,objBo.traveller_name, objBo.travel_duration, objBo.travel_arrangement,
                    objBo.accommodation_arrangement, objBo.Taxi_arrangement, objBo.Visa_Passport_arrangement, objBo.communication,
                    objBo.Overall_travel_experience, objBo.Feedback);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public List<travellersfeedback> Get_TravelFeedback()
        {
            travellersfeedbackDataContext objcontext = new travellersfeedbackDataContext();                
            List<travellersfeedback> objlist = new List<travellersfeedback>();

                foreach (var vRow in objcontext.sp_Get_travellers_feedback())
                {
                    travellersfeedback objbo = new travellersfeedback();
                    //objbo.FeedBack_Id = vRow.FeedBack_Id;
                     objbo.REQUISITION_ID =(int) vRow.REQUISITION_ID;
                     objbo.REQ_SEGMENT_ID =(int) vRow.REQ_SEGMENT_ID;
                     objbo.pERNR = vRow.PERNR;
                     objbo.traveller_name = vRow.Traveller_Name;
                     objbo.travel_duration =(byte) vRow.Travel_duration;
                     objbo.travel_arrangement =(byte) vRow.Travel_arrangement;
                     objbo.accommodation_arrangement =(byte) vRow.Accommodation_arrangement;
                     objbo.Taxi_arrangement =(byte) vRow.Taxi_arrangement;
                     objbo.Visa_Passport_arrangement =(byte) vRow.Visa_Passport_arrangement;
                     objbo.communication =(byte) vRow.Communication;
                     objbo.Overall_travel_experience =(byte) vRow.Overall_travel_experience;
                     objbo.Feedback = vRow.Feedback;

                    objlist.Add(objbo);
                }
                return objlist;
        }
    }
}

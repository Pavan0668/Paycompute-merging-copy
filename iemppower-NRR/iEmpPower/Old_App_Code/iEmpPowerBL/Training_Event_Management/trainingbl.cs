using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBO.Training_Event_Management;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Training_Event_Management;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerBO.Training_Event_Management.CollectionBO;


namespace iEmpPower.Old_App_Code.iEmpPowerBL.Training_Event_Management
{
    public class trainingbl
    {

        public List<Trainingbo> GetGroup()
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_group())
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.GROUP_ID = vRow.OBJID;
                Trainingbo.GROUP_TEXT = vRow.STEXT;
                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetSubgroup(string objid)
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_SubGroup(ref objid))
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.SUBGROUP_ID = vRow.OBJID;
                Trainingbo.SUBGROUP_TEXT = vRow.STEXT;
                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetEvent(string objid)
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_Event(ref objid))
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.EVENT_ID = vRow.OBJID;
                Trainingbo.EVENT_TEXT = vRow.STEXT;
                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetLocation()
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_Location())
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.LOCATION_ID = vRow.OBJID;
                Trainingbo.LOCATION_TEXT = vRow.STEXT;
                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetNoOfAttendees(string objid)
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_NoofAttendees(ref objid))
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.KAPZ1 = vRow.KAPZ1;
                Trainingbo.KAPZ2 = vRow.KAPZ2;
                Trainingbo.KAPZ3 = vRow.KAPZ3;
                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetPrice()
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_Internalprice())
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.IWAERS = vRow.WAERS;
                Trainingbo.EWAERS = vRow.LTEXT;
                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetControllingarea()
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_Controlling_Area())
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.KOKRS = vRow.KOKRS;

                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetCostcenter(string kokrs)
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_Cost_Center(ref kokrs))
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.KOKRS = vRow.KOKRS;
                Trainingbo.KOSTL = vRow.KOSTL;
                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetOrganizer_type()
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_Organizer_Type())
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.ORGNIZER_TYPE = vRow.OTEXT;
                Trainingbo.ORGNIZER_NAME = vRow.OTYPE;
                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetOrganizer_Name(string otype)
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_Organizer_Name(ref otype))
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.ORGNIZER_TYPE = vRow.OBJID;
                Trainingbo.ORGNIZER_NAME = vRow.STEXT;
                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetSchedule()
        {
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_Schedule())
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.AMUST = vRow.AMUST;
                //Trainingbo.AMUST = vRow.AMUST;
                TrainingboList.Add(Trainingbo);
            }
            return TrainingboList;
        }

        public List<Trainingbo> GetDurationdays(string amust)
        {
            string zblid;
            List<Trainingbo> TrainingboList = new List<Trainingbo>();
            TrainingDataContext objcontext = new TrainingDataContext();
            foreach (var vRow in objcontext.sp_get_Durationdays(ref amust))
            {
                Trainingbo Trainingbo = new Trainingbo();
                Trainingbo.TAGNR = vRow.TAGNR;
                zblid = vRow.ZBLID.ToString();
                TrainingboList.Add(Trainingbo);
                foreach (var vRow1 in objcontext.sp_get_Durationhours(ref zblid))
                {

                    Trainingbo.DAUER = vRow1.DAUER;

                    TrainingboList.Add(Trainingbo);
                }
            }


            return TrainingboList;
        }

        public void Create_Training(Trainingbo objBo, ref bool? SaveStatus)
        {
            try
            {
                TrainingDataContext objExpenseDataContext = new TrainingDataContext();

                objExpenseDataContext.sp_create_training(objBo.GROUP_ID, objBo.SUBGROUP_ID, objBo.EVENT_ID, objBo.IORE, objBo.START_DATE, objBo.END_DATE, objBo.LOCATION_ID, objBo.KAPZ1
                , objBo.KAPZ2, objBo.KAPZ3, objBo.IPRICE, objBo.IWAERS, objBo.EPRICE, objBo.EWAERS, objBo.KOKRS, objBo.KOSTL, objBo.ORGNIZER_TYPE, objBo.ORGNIZER_NAME,
                objBo.AMUST, objBo.TAGNR, objBo.DAUER, objBo.STARTDAY, objBo.MULTIPLE_DATES, objBo.NO_OF_TIMES, objBo.NO_OF_WORM, objBo.WORM, objBo.ENTEREDBy, objBo.ENTEREDON, objBo.FIRMLY_BOOK, objBo.LOCK, objBo.STATUS, ref SaveStatus);

                objExpenseDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public Training Get_Training(string strGroupid, string strSubgroupid, string strEventid, string strStartdate, string strEnddate)
        {
            TrainingDataContext objTrainingDataContext = new TrainingDataContext();

            Training objList = new Training();
            foreach (var vRow in objTrainingDataContext.sp_Get_Training(strGroupid, strSubgroupid, strEventid, strStartdate, strEnddate))
            {
                Trainingbo objTrainingAddBo = new Trainingbo();

                objTrainingAddBo.GROUP_ID = vRow.GROUP_ID;
                objTrainingAddBo.SUBGROUP_ID = vRow.SUBGROUP_ID;
                objTrainingAddBo.EVENT_ID = vRow.EVENT_ID;
                objTrainingAddBo.IORE = vRow.INTERNAL_EXTERNAL;
                objTrainingAddBo.START_DATE = vRow.START_DATE;
                objTrainingAddBo.END_DATE = vRow.END_DATE;
                objTrainingAddBo.LOCATION_ID = vRow.LOCATION;
                objTrainingAddBo.KAPZ1 = vRow.KAPZ1;
                objTrainingAddBo.KAPZ2 = vRow.KAPZ2;
                objTrainingAddBo.KAPZ3 = vRow.KAPZ3;
                objTrainingAddBo.IPRICE = vRow.IPRICE;
                objTrainingAddBo.IWAERS = vRow.IWAERS;
                objTrainingAddBo.EPRICE = vRow.EPRICE;
                objTrainingAddBo.EWAERS = vRow.EWAERS;
                objTrainingAddBo.KOKRS = vRow.KOKRS;
                objTrainingAddBo.KOSTL = vRow.KOSTL;
                objTrainingAddBo.ORGNIZER_TYPE = vRow.ORGANIZER_TYPE;
                objTrainingAddBo.ORGNIZER_NAME = vRow.ORGANIZER_NAME;
                objTrainingAddBo.AMUST = vRow.AMUST;
                objTrainingAddBo.TAGNR = vRow.TAGNR;
                objTrainingAddBo.DAUER = vRow.DAUER;
                objTrainingAddBo.STARTDAY = vRow.START_DAY;
                objTrainingAddBo.MULTIPLE_DATES = vRow.MULTIPLE_DATES;
                objTrainingAddBo.NO_OF_TIMES = vRow.NO_OF_TIMES;
                objTrainingAddBo.NO_OF_WORM = vRow.NO_OF_WORM;
                objTrainingAddBo.WORM = vRow.WORM;
                objTrainingAddBo.ENTEREDBy = vRow.ENTERED_BY;
                objTrainingAddBo.ENTEREDON = Convert.ToDateTime(vRow.ENTERED_ON);
                objTrainingAddBo.FIRMLY_BOOK = vRow.FIRMLY_BOOK;
                objTrainingAddBo.LOCK = vRow.LOCK;
                objTrainingAddBo.STATUS = vRow.STATUS;

                objList.Add(objTrainingAddBo);
            }
            objTrainingDataContext.Dispose();

            return objList;
        }

    }
}
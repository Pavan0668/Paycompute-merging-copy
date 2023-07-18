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
    public class Attendancebl
    {
        public List<Attendancebo> GetOrganizationunit()
        {
            List<Attendancebo> AttendanceboList = new List<Attendancebo>();
            AttendanceDataContext objcontext = new AttendanceDataContext();
            foreach (var vRow in objcontext.sp_get_Organizationunit())
            {
                Attendancebo Attendancebo = new Attendancebo();
                Attendancebo.ORGEH = vRow.ORGEH;
                Attendancebo.ORGTX = vRow.ORGTX;
                AttendanceboList.Add(Attendancebo);
            }
            return AttendanceboList;
        }

        public List<Attendancebo> GetExternalperson()
        {
            List<Attendancebo> AttendanceboList = new List<Attendancebo>();
            AttendanceDataContext objcontext = new AttendanceDataContext();
            foreach (var vRow in objcontext.sp_get_Externalperson())
            {
                Attendancebo Attendancebo = new Attendancebo();
                Attendancebo.OBJID = vRow.OBJID;
                Attendancebo.STEXT = vRow.STEXT;
                AttendanceboList.Add(Attendancebo);
            }
            return AttendanceboList;
        }

        public List<Attendancebo> GetOrganizationPerson(string objid)
        {
            List<Attendancebo> AttendanceboList = new List<Attendancebo>();
            AttendanceDataContext objcontext = new AttendanceDataContext();
            foreach (var vRow in objcontext.sp_get_Org_Person(ref objid))
            {
                Attendancebo Attendancebo = new Attendancebo();
                Attendancebo.PERNRVORNA = vRow.PERNR + "-" + vRow.VORNA + " " + vRow.NACHN;
                Attendancebo.VORNANACHN = vRow.VORNA + " " + vRow.NACHN;
                Attendancebo.PERNR = vRow.PERNR;
                Attendancebo.VORNA = vRow.VORNA;
                Attendancebo.NACHN = vRow.NACHN;
                AttendanceboList.Add(Attendancebo);
            }
            return AttendanceboList;
        }

        public List<Attendancebo> GetContactPerson()
        {
            List<Attendancebo> AttendanceboList = new List<Attendancebo>();
            AttendanceDataContext objcontext = new AttendanceDataContext();
            foreach (var vRow in objcontext.sp_get_Contact_Person())
            {
                Attendancebo Attendancebo = new Attendancebo();
                Attendancebo.PERNRVORNA = vRow.PERNR + "-" + vRow.VORNA + " " + vRow.NACHN;
                Attendancebo.VORNANACHN = vRow.VORNA + " " + vRow.NACHN;
                Attendancebo.PERNR = vRow.PERNR;
                Attendancebo.VORNA = vRow.VORNA;
                Attendancebo.NACHN = vRow.NACHN;
                AttendanceboList.Add(Attendancebo);
            }
            return AttendanceboList;
        }

        public Attendance Get_Training_All(Attendancebo ObjAttBO)
        {
            AttendanceDataContext objAttendanceDataContext = new AttendanceDataContext();

            Attendance objList = new Attendance();
            foreach (var vRow in objAttendanceDataContext.sp_Get_Training_All(ObjAttBO.ID, ObjAttBO.TRAINING_ID, ObjAttBO.Flag, ObjAttBO.PageIndex, ObjAttBO.PageSize))
            {
                Attendancebo objAttendanceAddBo = new Attendancebo();

                objAttendanceAddBo.RowNumber = (int)vRow.RowNumber;
                objAttendanceAddBo.ID = vRow.ID;
                objAttendanceAddBo.TRAINING_ID = vRow.TRAINING_ID;
                objAttendanceAddBo.GROUP_ID = vRow.GROUP_ID;
                objAttendanceAddBo.GROUP_TEXT = vRow.GROUP_TEXT;//GetGroupName(vRow.GROUP_ID.ToString());
                objAttendanceAddBo.SUBGROUP_ID = vRow.SUBGROUP_ID;
                objAttendanceAddBo.SUBGROUP_TEXT = vRow.SUBGROUP_TEXT;//GetGroupName(vRow.SUBGROUP_ID.ToString());
                objAttendanceAddBo.EVENT_ID = vRow.EVENT_ID;
                objAttendanceAddBo.EVENT_TEXT = vRow.EVENT_TEXT;//GetEventName(vRow.EVENT_ID.ToString());
                objAttendanceAddBo.IORE = vRow.INTERNAL_EXTERNAL;
                objAttendanceAddBo.START_DATE = vRow.START_DATE;
                objAttendanceAddBo.END_DATE = vRow.END_DATE;
                objAttendanceAddBo.LOCATION_ID = vRow.LOCATION;//GetLocationName(vRow.LOCATION.ToString());
                objAttendanceAddBo.LOCATION_TEXT = vRow.LOCATION_TEXT;
                objAttendanceAddBo.KAPZ1 = vRow.KAPZ1;
                objAttendanceAddBo.KAPZ2 = vRow.KAPZ2;
                objAttendanceAddBo.KAPZ3 = vRow.KAPZ3;
                //objAttendanceAddBo.IPRICE = vRow.IPRICE;
                //objAttendanceAddBo.IWAERS = vRow.IWAERS;
                //objAttendanceAddBo.EPRICE = vRow.EPRICE;
                //objAttendanceAddBo.EWAERS = vRow.EWAERS;
                //objAttendanceAddBo.KOKRS = vRow.KOKRS;
                //objAttendanceAddBo.KOSTL = vRow.KOSTL;
                //objAttendanceAddBo.ORGNIZER_TYPE = vRow.ORGANIZER_TYPE;
                objAttendanceAddBo.ORGNIZER_NAME = vRow.ORGANIZER_NAME;//GetOrganizerName(vRow.ORGANIZER_NAME.ToString());
                //objAttendanceAddBo.AMUST = vRow.AMUST;
                objAttendanceAddBo.TAGNR = vRow.TAGNR.ToString();
                objAttendanceAddBo.DAUER = vRow.DAUER;
                //objAttendanceAddBo.STARTDAY = vRow.START_DAY;
                //objAttendanceAddBo.MULTIPLE_DATES = vRow.MULTIPLE_DATES;
                //objAttendanceAddBo.NO_OF_TIMES = vRow.NO_OF_TIMES;
                //objAttendanceAddBo.NO_OF_WORM = vRow.NO_OF_WORM;
                //objAttendanceAddBo.WORM = vRow.WORM;
                //objAttendanceAddBo.ENTEREDBy = vRow.ENTERED_BY;
                //objAttendanceAddBo.ENTEREDON = Convert.ToDateTime(vRow.ENTERED_ON);
                //objAttendanceAddBo.FIRMLY_BOOK = vRow.FIRMLY_BOOK;
                //objAttendanceAddBo.LOCK = vRow.LOCK;
                //objAttendanceAddBo.STATUS = vRow.STATUS; 
                objAttendanceAddBo.BOOKED_STATUS = vRow.BOOKED_STATUS;
                objAttendanceAddBo.RecordCnt = vRow.RecordCnt;

                objList.Add(objAttendanceAddBo);
            }
            objAttendanceDataContext.Dispose();

            return objList;
        }

        //public string GetGroupName(string objid)
        //{
        //    string gname = "";
        //    AttendanceDataContext objcontext = new AttendanceDataContext();
        //    foreach (var vRow in objcontext.sp_get_GroupName(ref objid))
        //    {
        //        gname = vRow.STEXT;
        //    }
        //    return gname;
        //}

        //public string GetEventName(string objid)
        //{
        //    string ename = "";
        //    AttendanceDataContext objcontext = new AttendanceDataContext();
        //    foreach (var vRow in objcontext.sp_get_EventName(ref objid))
        //    {
        //        ename = vRow.STEXT;
        //    }
        //    return ename;
        //}

        //public string GetLocationName(string objid)
        //{
        //    string lname = "";
        //    AttendanceDataContext objcontext = new AttendanceDataContext();
        //    foreach (var vRow in objcontext.sp_get_LocationName(ref objid))
        //    {
        //        lname = vRow.STEXT;
        //    }
        //    return lname;
        //}

        //public string GetOrganizerName(string objid)
        //{
        //    string oname = "";
        //    AttendanceDataContext objcontext = new AttendanceDataContext();
        //    foreach (var vRow in objcontext.sp_get_OrganizerName(ref objid))
        //    {
        //        oname = vRow.STEXT;
        //    }
        //    return oname;
        //}

        public void Create_Attendance(Attendancebo objBo)
        {
            try
            {
                AttendanceDataContext objAttendanceDataContext = new AttendanceDataContext();

                objAttendanceDataContext.sp_create_attendance(objBo.ID, objBo.TRAININGID, objBo.TRAINING_ID, objBo.ORGTX, objBo.PERNR,
                objBo.Noofattendees, objBo.Contactperson, objBo.STEXT, objBo.bookingpriority,
                objBo.ENTEREDBy, objBo.ENTEREDON, objBo.Cancellationreason, objBo.STATUS, objBo.Flag);

                objAttendanceDataContext.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Attendance Get_Attendance()
        {
            AttendanceDataContext objAttendanceDataContext = new AttendanceDataContext();

            Attendance objList = new Attendance();
            foreach (var vRow in objAttendanceDataContext.sp_Get_Attendance())
            {
                Attendancebo objAttendanceAddBo = new Attendancebo();
                objAttendanceAddBo.TRAINING_ID = vRow.TRAINING_ID;
                objAttendanceAddBo.START_DATE = DateTime.Parse(vRow.PERIOD_FROM);
                objAttendanceAddBo.END_DATE = DateTime.Parse(vRow.PERIOD_TO);


                objAttendanceAddBo.ORGEH = vRow.ORG_UNIT;
                string Oname = vRow.ORG_UNIT;
                objAttendanceAddBo.ORGTX = GetOrganizatinal_unit_name(Oname);

                objAttendanceAddBo.PERNR = vRow.PERSON;
                string Pernr = vRow.PERSON;
                objAttendanceAddBo.VORNANACHN = GetPerson_name(Pernr);
                objAttendanceAddBo.PERNRVORNA = objAttendanceAddBo.PERNR + "-" + objAttendanceAddBo.VORNANACHN;

                objAttendanceAddBo.Noofattendees = vRow.NO_OF_ATTENDEES;
                objAttendanceAddBo.ENTEREDBy = vRow.ENTERED_BY;
                objAttendanceAddBo.ENTEREDON = Convert.ToDateTime(vRow.ENTERED_ON);
                if (vRow.CANCELLATION_REASON != "")
                {
                    string cr = GetCancellaionreason_Text(vRow.CANCELLATION_REASON);
                    objAttendanceAddBo.Cancellationreason = cr;
                }
                else
                    objAttendanceAddBo.Cancellationreason = vRow.CANCELLATION_REASON;

                objAttendanceAddBo.STATUS = vRow.STATUS;

                objList.Add(objAttendanceAddBo);
            }
            objAttendanceDataContext.Dispose();

            return objList;
        }

        public Attendance Get_Attendance_TrainingIdwise(string TrainingId)
        {
            AttendanceDataContext objAttendanceDataContext = new AttendanceDataContext();

            Attendance objList = new Attendance();
            foreach (var vRow in objAttendanceDataContext.sp_Get_Attendance_TrainingIdwise(TrainingId))
            {
                Attendancebo objAttendanceAddBo = new Attendancebo();
                objAttendanceAddBo.TRAINING_ID = vRow.TRAINING_ID;
                objAttendanceAddBo.START_DATE = DateTime.Parse(vRow.PERIOD_FROM);
                objAttendanceAddBo.END_DATE = DateTime.Parse(vRow.PERIOD_TO);


                objAttendanceAddBo.ORGEH = vRow.ORG_UNIT;
                string Oname = vRow.ORG_UNIT;
                objAttendanceAddBo.ORGTX = GetOrganizatinal_unit_name(Oname);

                objAttendanceAddBo.PERNR = vRow.PERSON;
                string Pernr = vRow.PERSON;
                objAttendanceAddBo.VORNANACHN = GetPerson_name(Pernr);
                objAttendanceAddBo.PERNRVORNA = objAttendanceAddBo.PERNR + "-" + objAttendanceAddBo.VORNANACHN;

                objAttendanceAddBo.Noofattendees = vRow.NO_OF_ATTENDEES;
                objAttendanceAddBo.ENTEREDBy = vRow.ENTERED_BY;
                objAttendanceAddBo.ENTEREDON = Convert.ToDateTime(vRow.ENTERED_ON);
                if (vRow.CANCELLATION_REASON != "")
                {
                    string cr = GetCancellaionreason_Text(vRow.CANCELLATION_REASON);
                    objAttendanceAddBo.Cancellationreason = cr;
                }
                else
                    objAttendanceAddBo.Cancellationreason = vRow.CANCELLATION_REASON;

                objAttendanceAddBo.STATUS = vRow.STATUS;

                objList.Add(objAttendanceAddBo);
            }
            objAttendanceDataContext.Dispose();

            return objList;
        }



        public Attendance Get_Attendance_Employeewise(Attendancebo AttBo)
        {
            AttendanceDataContext objAttendanceDataContext = new AttendanceDataContext();

            Attendance objList = new Attendance();
            foreach (var vRow in objAttendanceDataContext.sp_Get_Attendance_Employeewise(AttBo.ID, AttBo.TRAINING_ID, AttBo.PERNR, AttBo.Flag, AttBo.PageIndex, AttBo.PageSize))
            {
                Attendancebo objAttendanceAddBo = new Attendancebo();

                objAttendanceAddBo.RowNumber = (int)vRow.RowNumber;
                objAttendanceAddBo.ID = vRow.ID;
                objAttendanceAddBo.TRAINING_ID = vRow.TRAINING_ID;
                objAttendanceAddBo.GROUP_TEXT = vRow.GROUP_TEXT;
                objAttendanceAddBo.SUBGROUP_TEXT = vRow.SUBGROUP_TEXT;
                objAttendanceAddBo.EVENT_TEXT = vRow.EVENT_TEXT;
                objAttendanceAddBo.IORE = vRow.INTERNAL_EXTERNAL;
                objAttendanceAddBo.LOCATION_TEXT = vRow.LOCATION_TEXT;
                objAttendanceAddBo.START_DATE = vRow.START_DATE;
                objAttendanceAddBo.END_DATE = vRow.END_DATE;
                objAttendanceAddBo.REGCOUNT = vRow.Attendees_Count;
                objAttendanceAddBo.RecordCnt = vRow.RecordCnt;

                //objAttendanceAddBo.ORGEH = vRow.ORG_UNIT;
                //string Oname = vRow.ORG_UNIT;
                //objAttendanceAddBo.ORGTX = GetOrganizatinal_unit_name(Oname);

                //objAttendanceAddBo.PERNR = vRow.PERSON;
                //string Pernr = vRow.PERSON;
                //objAttendanceAddBo.VORNANACHN = GetPerson_name(Pernr);
                //objAttendanceAddBo.PERNRVORNA = objAttendanceAddBo.PERNR + "-" + objAttendanceAddBo.VORNANACHN;

                //objAttendanceAddBo.Noofattendees = vRow.NO_OF_ATTENDEES;
                //objAttendanceAddBo.ENTEREDBy = vRow.ENTERED_BY;
                //objAttendanceAddBo.ENTEREDON = Convert.ToDateTime(vRow.ENTERED_ON);
                //if (vRow.CANCELLATION_REASON != "")
                //{
                //    string cr = GetCancellaionreason_Text(vRow.CANCELLATION_REASON);
                //    objAttendanceAddBo.Cancellationreason = cr;
                //}
                //else
                //    objAttendanceAddBo.Cancellationreason = vRow.CANCELLATION_REASON;

                //objAttendanceAddBo.STATUS = vRow.STATUS;

                objList.Add(objAttendanceAddBo);
            }
            objAttendanceDataContext.Dispose();

            return objList;
        }


        public List<Attendancebo> Get_Attendance_Requested(string EmployeeId, string EmployeeName)
        {
            AttendanceDataContext objAttendanceDataContext = new AttendanceDataContext();

            List<Attendancebo> objList = new List<Attendancebo>();
            foreach (var vRow in objAttendanceDataContext.sp_Get_Attendance_Requested(EmployeeId, EmployeeName))
            {
                Attendancebo objAttendanceAddBo = new Attendancebo();
                objAttendanceAddBo.TRAINING_ID = vRow.TRAINING_ID;
                objAttendanceAddBo.START_DATE = DateTime.Parse(vRow.PERIOD_FROM);
                objAttendanceAddBo.END_DATE = DateTime.Parse(vRow.PERIOD_TO);


                objAttendanceAddBo.ORGEH = vRow.ORG_UNIT;
                string Oname = vRow.ORG_UNIT;
                objAttendanceAddBo.ORGTX = GetOrganizatinal_unit_name(Oname);

                objAttendanceAddBo.PERNR = vRow.PERSON;
                string Pernr = vRow.PERSON;
                objAttendanceAddBo.VORNANACHN = GetPerson_name(Pernr);
                objAttendanceAddBo.PERNRVORNA = objAttendanceAddBo.PERNR + "-" + objAttendanceAddBo.VORNANACHN;

                objAttendanceAddBo.Noofattendees = vRow.NO_OF_ATTENDEES;
                objAttendanceAddBo.ENTEREDBy = vRow.ENTERED_BY;
                objAttendanceAddBo.ENTEREDON = Convert.ToDateTime(vRow.ENTERED_ON);
                if (vRow.CANCELLATION_REASON != "")
                {
                    string cr = GetCancellaionreason_Text(vRow.CANCELLATION_REASON);
                    objAttendanceAddBo.Cancellationreason = cr;
                }
                else
                    objAttendanceAddBo.Cancellationreason = vRow.CANCELLATION_REASON;

                objAttendanceAddBo.STATUS = vRow.STATUS;

                objList.Add(objAttendanceAddBo);
            }
            objAttendanceDataContext.Dispose();

            return objList;
        }


        public Attendance Get_Attendance_Approved()
        {
            AttendanceDataContext objAttendanceDataContext = new AttendanceDataContext();

            Attendance objList = new Attendance();
            foreach (var vRow in objAttendanceDataContext.sp_Get_Attendance_Approved())
            {
                Attendancebo objAttendanceAddBo = new Attendancebo();
                objAttendanceAddBo.TRAINING_ID = vRow.TRAINING_ID;
                objAttendanceAddBo.START_DATE = DateTime.Parse(vRow.PERIOD_FROM);
                objAttendanceAddBo.END_DATE = DateTime.Parse(vRow.PERIOD_TO);


                objAttendanceAddBo.ORGEH = vRow.ORG_UNIT;
                string Oname = vRow.ORG_UNIT;
                objAttendanceAddBo.ORGTX = GetOrganizatinal_unit_name(Oname);

                objAttendanceAddBo.PERNR = vRow.PERSON;
                string Pernr = vRow.PERSON;
                objAttendanceAddBo.VORNANACHN = GetPerson_name(Pernr);
                objAttendanceAddBo.PERNRVORNA = objAttendanceAddBo.PERNR + "-" + objAttendanceAddBo.VORNANACHN;

                objAttendanceAddBo.Noofattendees = vRow.NO_OF_ATTENDEES;
                objAttendanceAddBo.ENTEREDBy = vRow.ENTERED_BY;
                objAttendanceAddBo.ENTEREDON = Convert.ToDateTime(vRow.ENTERED_ON);
                if (vRow.CANCELLATION_REASON != "")
                {
                    string cr = GetCancellaionreason_Text(vRow.CANCELLATION_REASON);
                    objAttendanceAddBo.Cancellationreason = cr;
                }
                else
                    objAttendanceAddBo.Cancellationreason = vRow.CANCELLATION_REASON;

                objAttendanceAddBo.STATUS = vRow.STATUS;

                objList.Add(objAttendanceAddBo);
            }
            objAttendanceDataContext.Dispose();

            return objList;
        }


        public string GetOrganizatinal_unit_name(string objid)
        {
            string oname = "";
            AttendanceDataContext objcontext = new AttendanceDataContext();
            foreach (var vRow in objcontext.sp_get_Organizationunit_Name(ref objid))
            {
                oname = vRow.ORGTX;
            }
            return oname;
        }

        public string GetOrganizatinal_unit_id(string oname)
        {
            string oid = "";
            AttendanceDataContext objcontext = new AttendanceDataContext();
            foreach (var vRow in objcontext.sp_get_Organizationunit_ID(ref oname))
            {
                oid = vRow.ORGEH;
            }
            return oid;
        }

        public string GetPerson_name(string pernr)
        {
            string pname = "";
            AttendanceDataContext objcontext = new AttendanceDataContext();
            foreach (var vRow in objcontext.sp_get_Person_Name(ref pernr))
            {
                pname = vRow.VORNA + " " + vRow.NACHN;
            }
            return pname;
        }

        public string GetCancellaionreason_Text(string crno)
        {
            string cr = "";
            AttendanceDataContext objcontext = new AttendanceDataContext();
            foreach (var vRow in objcontext.sp_get_CancellationReason_Text(ref crno))
            {
                cr = vRow.CAATRT;
            }
            return cr;
        }

        public List<Attendancebo> GetCancellationReason()
        {
            List<Attendancebo> AttendanceboList = new List<Attendancebo>();
            AttendanceDataContext objcontext = new AttendanceDataContext();
            foreach (var vRow in objcontext.sp_get_CancellationReason())
            {
                Attendancebo Attendancebo = new Attendancebo();
                Attendancebo.CancellationID = vRow.CAATR;
                Attendancebo.Cancellationreason = vRow.CAATRT;
                AttendanceboList.Add(Attendancebo);
            }
            return AttendanceboList;
        }

        public void Update_Attendance_Status(Attendancebo objBo)
        {
            try
            {
                AttendanceDataContext objAttendanceDataContext = new AttendanceDataContext();
                objAttendanceDataContext.sp_update_Attendance_Status(objBo.ID, objBo.Cancellationreason, objBo.STATUS, objBo.Flag);
                objAttendanceDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public void Update_Attendance_Person(Attendancebo objBo)
        {
            try
            {
                AttendanceDataContext objAttendanceDataContext = new AttendanceDataContext();

                objAttendanceDataContext.sp_update_Attendance_Person(objBo.TRAINING_ID, objBo.START_DATE.ToString(), objBo.END_DATE.ToString(), objBo.ORGTX, objBo.ORGTX_old, objBo.PERNR, objBo.PERNR_old,
                objBo.Noofattendees, objBo.Cancellationreason, objBo.STATUS);

                objAttendanceDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

    }
}
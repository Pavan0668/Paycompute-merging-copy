using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Training_Event_Management
{
    public class Attendancebo
    {
        private int? _TRAINING_ID = 0;
        private Guid _TRAININGID = new Guid();
        private string _ORGEH = string.Empty;
        private string _ORGTX = string.Empty;
        private string _ORGTX_old = string.Empty;
        private string _OBJID = string.Empty;
        private string _STEXT = string.Empty;
        private string _PERNR = string.Empty;
        private string _PERNR_old = string.Empty;
        private string _VORNA = string.Empty;
        private string _NACHN = string.Empty;
        private string _PERNRVORNA = string.Empty;
        private string _VORNANACHN = string.Empty;
        private string _Noofattendees = string.Empty;
        private string _Contactperson = string.Empty;
        private string _bookingpriority = string.Empty;
        private string _Cancellationreason = string.Empty;
        private string _CancellationID = string.Empty;


        private string _GROUP_ID = string.Empty;
        private string _GROUP_TEXT = string.Empty;
        private string _SUBGROUP_ID = string.Empty;
        private string _SUBGROUP_TEXT = string.Empty;
        private string _EVENT_ID = string.Empty;
        private string _EVENT_TEXT = string.Empty;
        private string _IORE = string.Empty;
        private DateTime? _START_DATE = DateTime.Now;
        private DateTime? _END_DATE = DateTime.Now;
        private string _LOCATION_ID = string.Empty;
        private string _LOCATION_TEXT = string.Empty;
        private string _KAPZ1 = string.Empty;
        private string _KAPZ2 = string.Empty;
        private string _KAPZ3 = string.Empty;
        private string _IPRICE = string.Empty;
        private string _IWAERS = string.Empty;
        private string _EPRICE = string.Empty;
        private string _EWAERS = string.Empty;
        private string _KOKRS = string.Empty;
        private string _KOSTL = string.Empty;
        private string _ORGNIZER_TYPE = string.Empty;
        private string _ORGNIZER_NAME = string.Empty;
        private string _AMUST = string.Empty;
        private string _TAGNR = string.Empty;
        private string _DAUER = string.Empty;
        private string _STARTDAY = string.Empty;
        private string _MULTIPLE_DATES = string.Empty;
        private string _NO_OF_TIMES = string.Empty;
        private string _NO_OF_WORM = string.Empty;
        private string _WORM = string.Empty;
        private string _ENTEREDBy = string.Empty;
        private DateTime _ENTEREDON;
        private string _FIRMLY_BOOK = string.Empty;
        private string _LOCK = string.Empty;
        private string _STATUS = string.Empty;
        private int? _Count = 0;

        private Guid _ID = new Guid();
        private int? _Flag = 0;

        private int? _RecordCnt = 0;
        private int? _RowNumber = 0;
        private int? _PageIndex = 0;
        private int? _PageSize = 0;

        private string _BookedStatus = string.Empty;



        public int? TRAINING_ID
        {
            get
            {
                return _TRAINING_ID == null ? 0 : _TRAINING_ID.Value;
            }
            set
            {
                _TRAINING_ID = value;
            }
        }
        public Guid TRAININGID
        {
            get { return _TRAININGID; }
            set { _TRAININGID = value; }
        }

        public string ORGEH
        {
            get
            {
                return _ORGEH;
            }
            set
            {
                _ORGEH = value;
            }
        }

        public string ORGTX
        {
            get
            {
                return _ORGTX;
            }
            set
            {
                _ORGTX = value;
            }
        }

        public string ORGTX_old
        {
            get
            {
                return _ORGTX_old;
            }
            set
            {
                _ORGTX_old = value;
            }
        }

        public string OBJID
        {
            get
            {
                return _OBJID;
            }
            set
            {
                _OBJID = value;
            }
        }

        public string STEXT
        {
            get
            {
                return _STEXT;
            }
            set
            {
                _STEXT = value;
            }
        }

        public string PERNR
        {
            get
            {
                return _PERNR;
            }
            set
            {
                _PERNR = value;
            }
        }

        public string PERNR_old
        {
            get
            {
                return _PERNR_old;
            }
            set
            {
                _PERNR_old = value;
            }
        }

        public string VORNA
        {
            get
            {
                return _VORNA;
            }
            set
            {
                _VORNA = value;
            }
        }

        public string NACHN
        {
            get
            {
                return _NACHN;
            }
            set
            {
                _NACHN = value;
            }
        }

        public string PERNRVORNA
        {
            get
            {
                return _PERNRVORNA;
            }
            set
            {
                _PERNRVORNA = value;
            }
        }

        public string VORNANACHN
        {
            get
            {
                return _VORNANACHN;
            }
            set
            {
                _VORNANACHN = value;
            }
        }

        public string Noofattendees
        {
            get
            {
                return _Noofattendees;
            }
            set
            {
                _Noofattendees = value;
            }
        }

        public string Contactperson
        {
            get
            {
                return _Contactperson;
            }
            set
            {
                _Contactperson = value;
            }
        }

        public string bookingpriority
        {
            get
            {
                return _bookingpriority;
            }
            set
            {
                _bookingpriority = value;
            }
        }

        public string Cancellationreason
        {
            get
            {
                return _Cancellationreason;
            }
            set
            {
                _Cancellationreason = value;
            }
        }

        public string CancellationID
        {
            get
            {
                return _CancellationID;
            }
            set
            {
                _CancellationID = value;
            }
        }





        public string GROUP_ID
        {
            get
            {
                return _GROUP_ID;
            }
            set
            {
                _GROUP_ID = value;
            }
        }

        public string GROUP_TEXT
        {
            get
            {
                return _GROUP_TEXT;
            }
            set
            {
                _GROUP_TEXT = value;
            }
        }

        public string SUBGROUP_ID
        {
            get
            {
                return _SUBGROUP_ID;
            }
            set
            {
                _SUBGROUP_ID = value;
            }
        }

        public string SUBGROUP_TEXT
        {
            get
            {
                return _SUBGROUP_TEXT;
            }
            set
            {
                _SUBGROUP_TEXT = value;
            }
        }

        public string EVENT_ID
        {
            get
            {
                return _EVENT_ID;
            }
            set
            {
                _EVENT_ID = value;
            }
        }

        public string EVENT_TEXT
        {
            get
            {
                return _EVENT_TEXT;
            }
            set
            {
                _EVENT_TEXT = value;
            }
        }

        public string IORE
        {
            get
            {
                return _IORE;
            }
            set
            {
                _IORE = value;
            }
        }

        public DateTime? START_DATE
        {
            get
            {
                return _START_DATE;
            }
            set
            {
                _START_DATE = value;
            }
        }

        public DateTime? END_DATE
        {
            get
            {
                return _END_DATE;
            }
            set
            {
                _END_DATE = value;
            }
        }

        public string LOCATION_ID
        {
            get
            {
                return _LOCATION_ID;
            }
            set
            {
                _LOCATION_ID = value;
            }
        }

        public string LOCATION_TEXT
        {
            get
            {
                return _LOCATION_TEXT;
            }
            set
            {
                _LOCATION_TEXT = value;
            }
        }

        public string KAPZ1
        {
            get
            {
                return _KAPZ1;
            }
            set
            {
                _KAPZ1 = value;
            }
        }

        public string KAPZ2
        {
            get
            {
                return _KAPZ2;
            }
            set
            {
                _KAPZ2 = value;
            }
        }

        public string KAPZ3
        {
            get
            {
                return _KAPZ3;
            }
            set
            {
                _KAPZ3 = value;
            }
        }

        public string IPRICE
        {
            get
            {
                return _IPRICE;
            }
            set
            {
                _IPRICE = value;
            }
        }

        public string IWAERS
        {
            get
            {
                return _IWAERS;
            }

            set
            {
                _IWAERS = value;
            }
        }

        public string EPRICE
        {
            get
            {
                return _EPRICE;
            }
            set
            {
                _EPRICE = value;
            }
        }

        public string EWAERS
        {
            get
            {
                return _EWAERS;
            }

            set
            {
                _EWAERS = value;
            }
        }

        public string KOKRS
        {
            get
            {
                return _KOKRS;
            }

            set
            {
                _KOKRS = value;
            }
        }

        public string KOSTL
        {
            get
            {
                return _KOSTL;
            }

            set
            {
                _KOSTL = value;
            }
        }

        public string ORGNIZER_TYPE
        {
            get
            {
                return _ORGNIZER_TYPE;
            }

            set
            {
                _ORGNIZER_TYPE = value;
            }
        }

        public string ORGNIZER_NAME
        {
            get
            {
                return _ORGNIZER_NAME;
            }

            set
            {
                _ORGNIZER_NAME = value;
            }
        }

        public string AMUST
        {
            get
            {
                return _AMUST;
            }

            set
            {
                _AMUST = value;
            }
        }

        public string TAGNR
        {
            get
            {
                return _TAGNR;
            }

            set
            {
                _TAGNR = value;
            }
        }

        public string DAUER
        {
            get
            {
                return _DAUER;
            }

            set
            {
                _DAUER = value;
            }
        }

        public string STARTDAY
        {
            get
            {
                return _STARTDAY;
            }

            set
            {
                _STARTDAY = value;
            }
        }

        public string MULTIPLE_DATES
        {
            get
            {
                return _MULTIPLE_DATES;
            }

            set
            {
                _MULTIPLE_DATES = value;
            }
        }

        public string NO_OF_TIMES
        {
            get
            {
                return _NO_OF_TIMES;
            }

            set
            {
                _NO_OF_TIMES = value;
            }
        }

        public string NO_OF_WORM
        {
            get
            {
                return _NO_OF_WORM;
            }

            set
            {
                _NO_OF_WORM = value;
            }
        }

        public string WORM
        {
            get
            {
                return _WORM;
            }

            set
            {
                _WORM = value;
            }
        }

        public string ENTEREDBy
        {
            get { return _ENTEREDBy; }
            set { _ENTEREDBy = value; }
        }

        public DateTime ENTEREDON
        {
            get { return _ENTEREDON; }
            set { _ENTEREDON = value; }
        }

        public string FIRMLY_BOOK
        {
            get
            {
                return _FIRMLY_BOOK;
            }

            set
            {
                _FIRMLY_BOOK = value;
            }
        }

        public string LOCK
        {
            get
            {
                return _LOCK;
            }

            set
            {
                _LOCK = value;
            }
        }

        public string STATUS
        {
            get
            {
                return _STATUS;
            }

            set
            {
                _STATUS = value;
            }
        }

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int? Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        public int? PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }

        public int? PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        public int? RecordCnt
        {
            get { return _RecordCnt; }
            set { _RecordCnt = value; }
        }

        public int? RowNumber
        {
            get { return _RowNumber; }
            set { _RowNumber = value; }
        }

        public string BOOKED_STATUS
        {
            get { return _BookedStatus; }
            set { _BookedStatus = value; }
        }


        public int? REGCOUNT
        {
            get { return _Count; }
            set { _Count = value; }
        }
    }
}
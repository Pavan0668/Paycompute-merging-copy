using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
/// <summary>
/// Summary description for travelrequestcolumnsbo
/// </summary>
/// 

public class travelrequestcolumnsbo
{
    public travelrequestcolumnsbo()
    {

    }

    //Travel start and end details.
    private bool _sChk;
    public bool CHK { get { return _sChk; } set { _sChk = value; } }

    private string _iTravelSlNo = string.Empty;
    private long _iTravelRequestNo = 0;
    private string _strCurrent_Status = string.Empty;
    private string _strTravelPernr = string.Empty;
    private short _sTravelRequest = 0;
    private short _sTravelItinerary = 0;
    private DateTime _dtTravelStartDate = Convert.ToDateTime("01/01/0001");
    private string _strTravelStartHour = string.Empty;
    private string _strTravelStartMinute = string.Empty;
    private string _strTravelStartLoc = string.Empty;
    private string _strTravelStartCountry = string.Empty;
    private DateTime _dtTravelEndDate = Convert.ToDateTime("01/01/0001");
    private string _strTravelEndHour = string.Empty;
    private string _strTravelEndMinute = string.Empty;
    private string _strTravelEndLoc = string.Empty;
    private string _strTravelEndCountry = string.Empty;
    private string _strTravelActivity = string.Empty;
    private string _strTravelReason = string.Empty;
    private string _strRemarks = string.Empty;

    private string _strTravelSlNoList = string.Empty;
    private string _strTravelRequestNoList = string.Empty;
    private string _strTravelPernrList = string.Empty;
    private string _strTravelRequestList = string.Empty;
    private string _strTravelItineraryList = string.Empty;
    private string _strTravelStartDateList = string.Empty;
    private string _strTravelStartHourList = string.Empty;
    private string _strTravelStartMinuteList = string.Empty;
    private string _strTravelStartLocList = string.Empty;
    private string _strTravelStartCountryList = string.Empty;
    private string _strTravelEndDateList = string.Empty;    
    private string _strTravelEndHourList = string.Empty;
    private string _strTravelEndMinuteList = string.Empty;
    private string _strTravelEndLocList = string.Empty;
    private string _strTravelEndCountryList = string.Empty;
    private string _strTravelActivityList = string.Empty;
    private string _strTravelReasonList = string.Empty;
    private string _strApprovedBy = string.Empty;
    private decimal? Claim_TotalAmount = 0;// travel claim 
    public decimal? ClaimTotalAmount { get { return Claim_TotalAmount; } set { Claim_TotalAmount = value; } }


    public string TRAVEL_SL_NO { get { return _iTravelSlNo; } set { _iTravelSlNo = value; } }
    public long TRAVEL_REQUEST_NO { get { return _iTravelRequestNo; } set { _iTravelRequestNo = value; } }
    public string CURRENT_STATUS { get { return _strCurrent_Status; } set { _strCurrent_Status = value; } }
    public string TRAVEL_PERNR { get { return _strTravelPernr; } set { _strTravelPernr = value; } }
    public short TRAVEL_REQUEST { get { return _sTravelRequest; } set { _sTravelRequest = value; } }
    public short TRAVEL_ININERARY_NUMBER { get { return _sTravelItinerary; } set { _sTravelItinerary = value; } }
    public DateTime TRAVEL_START_DATE { get { return _dtTravelStartDate; } set { _dtTravelStartDate = value; } }
    public string TRAVEL_START_HOUR { get { return _strTravelStartHour; } set { _strTravelStartHour = value; } }
    public string TRAVEL_START_MINUTE { get { return _strTravelStartMinute; } set { _strTravelStartMinute = value; } }
    public string TRAVEL_START_LOCATION { get { return _strTravelStartLoc; } set { _strTravelStartLoc = value; } }
    public string TRAVEL_START_COUNTRY { get { return _strTravelStartCountry; } set { _strTravelStartCountry = value; } }
    public DateTime TRAVEL_END_DATE { get { return _dtTravelEndDate; } set { _dtTravelEndDate = value; } }
    public string TRAVEL_END_HOUR { get { return _strTravelEndHour; } set { _strTravelEndHour = value; } }
    public string TRAVEL_END_MINUTE { get { return _strTravelEndMinute; } set { _strTravelEndMinute = value; } }
    public string TRAVEL_REASON { get { return _strTravelReason; } set { _strTravelReason = value; } }
    public string TRAVEL_ACTIVITY { get { return _strTravelActivity; } set { _strTravelActivity = value; } }
    public string TRAVEL_END_LOCATION { get { return _strTravelEndLoc; } set { _strTravelEndLoc = value; } }
    public string TRAVEL_END_COUNTRY { get { return _strTravelEndCountry; } set { _strTravelEndCountry = value; } }
    public string TRAVEL_REMARKS { get { return _strRemarks; } set { _strRemarks = value; } }
    public string APPROVED_BY { get { return _strApprovedBy; } set { _strApprovedBy = value; } }

    public string TRAVEL_SL_NO_LIST { get { return _strTravelSlNoList; } set { _strTravelSlNoList = value; } }
    public string TRAVEL_REQUEST_NO_LIST { get { return _strTravelRequestNoList; } set { _strTravelRequestNoList = value; } }
    public string TRAVEL_PERNR_LIST { get { return _strTravelPernrList; } set { _strTravelPernrList = value; } }
    public string TRAVEL_REQUEST_LIST { get { return _strTravelRequestList; } set { _strTravelRequestList = value; } }
    public string TRAVEL_ININERARY_NUMBER_LIST { get { return _strTravelItineraryList; } set { _strTravelItineraryList = value; } }
    public string TRAVEL_START_DATE_LIST { get { return _strTravelStartDateList; } set { _strTravelStartDateList = value; } }
    public string TRAVEL_START_HOUR_LIST { get { return _strTravelStartHourList; } set { _strTravelStartHourList = value; } }
    public string TRAVEL_START_MINUTE_LIST { get { return _strTravelStartMinuteList; } set { _strTravelStartMinuteList = value; } }
    public string TRAVEL_START_LOCATION_LIST { get { return _strTravelStartLocList; } set { _strTravelStartLocList = value; } }
    public string TRAVEL_START_COUNTRY_LIST { get { return _strTravelStartCountryList; } set { _strTravelStartCountryList = value; } }
    public string TRAVEL_END_DATE_LIST { get { return _strTravelEndDateList; } set { _strTravelEndDateList = value; } }
    public string TRAVEL_END_HOUR_LIST { get { return _strTravelEndHourList; } set { _strTravelEndHourList = value; } }
    public string TRAVEL_END_MINUTE_LIST { get { return _strTravelEndMinuteList; } set { _strTravelEndMinuteList = value; } }
    public string TRAVEL_REASON_LIST { get { return _strTravelReasonList; } set { _strTravelReasonList = value; } }
    public string TRAVEL_ACTIVITY_LIST { get { return _strTravelActivityList; } set { _strTravelActivityList = value; } }
    public string TRAVEL_END_LOCATION_LIST { get { return _strTravelEndLocList; } set { _strTravelEndLocList = value; } }
    public string TRAVEL_END_COUNTRY_LIST { get { return _strTravelEndCountryList; } set { _strTravelEndCountryList = value; } }

    //Itinerary details
    private string _strRequest = string.Empty;
    private string _strItinerary = string.Empty;
    private DateTime _dtStartDate = Convert.ToDateTime("01/01/0001");
    private string _strStartHour = string.Empty;
    private string _strStartMinute = string.Empty;
    private DateTime _dtEndDate = Convert.ToDateTime("01/01/0001");
    private string _strEndHour = string.Empty;
    private string _strEndMinute = string.Empty;
    private string _strStartLoc = string.Empty;
    private string _strStartCountry = string.Empty;
    private string _strEndLoc = string.Empty;
    private string _strEndCountry = string.Empty;
    private string _strRequestType = string.Empty;    

    private string _strRequestList = string.Empty;
    private string _strItineraryList = string.Empty;
    private string _dtStartDateList = string.Empty;
    private string _strStartHourList = string.Empty;
    private string _strStartMinuteList = string.Empty;
    private string _dtEndDateList = string.Empty;
    private string _strEndHourList = string.Empty;
    private string _strEndMinuteList = string.Empty;
    private string _strStartLocList = string.Empty;
    private string _strStartCountryList = string.Empty;
    private string _strEndLocList = string.Empty;
    private string _strEndCountryList = string.Empty;
    private string _strRequestTypeList = string.Empty;

    public string TRANS_REQUEST { get { return _strRequest; } set { _strRequest = value; } }
    public string TRANS_ITINERARY { get { return _strItinerary; } set { _strItinerary = value; } }
    public DateTime TRANS_START_DATE { get { return _dtStartDate; } set { _dtStartDate = value; } }
    public string TRANS_START_HOUR { get { return _strStartHour; } set { _strStartHour = value; } }
    public string TRANS_START_MINUTE { get { return _strStartMinute; } set { _strStartMinute = value; } }
    public DateTime TRANS_END_DATE { get { return _dtEndDate; } set { _dtEndDate = value; } }
    public string TRANS_END_HOUR { get { return _strEndHour; } set { _strEndHour = value; } }
    public string TRANS_END_MINUTE { get { return _strEndMinute; } set { _strEndMinute = value; } }
    public string TRANS_START_LOCATION { get { return _strStartLoc; } set { _strStartLoc = value; } }
    public string TRANS_START_COUNTRY { get { return _strStartCountry; } set { _strStartCountry = value; } }
    public string TRANS_END_LOCATION { get { return _strEndLoc; } set { _strEndLoc = value; } }
    public string TRANS_END_COUNTRY { get { return _strEndCountry; } set { _strEndCountry = value; } }
    public string TRANS_REQUEST_TYPE { get { return _strRequestType; } set { _strRequestType = value; } }

    public string TRANS_REQUEST_LIST { get { return _strRequestList; } set { _strRequestList = value; } }
    public string TRANS_ITINERARY_LIST { get { return _strItineraryList; } set { _strItineraryList = value; } }
    public string TRANS_START_DATE_LIST { get { return _dtStartDateList; } set { _dtStartDateList = value; } }
    public string TRANS_START_HOUR_LIST { get { return _strStartHourList; } set { _strStartHourList = value; } }
    public string TRANS_START_MINUTE_LIST { get { return _strStartMinuteList; } set { _strStartMinuteList = value; } }
    public string TRANS_END_DATE_LIST { get { return _dtEndDateList; } set { _dtEndDateList = value; } }
    public string TRANS_END_HOUR_LIST { get { return _strEndHourList; } set { _strEndHourList = value; } }
    public string TRANS_END_MINUTE_LIST { get { return _strEndMinuteList; } set { _strEndMinuteList = value; } }
    public string TRANS_START_LOCATION_LIST { get { return _strStartLocList; } set { _strStartLocList = value; } }
    public string TRANS_START_COUNTRY_LIST { get { return _strStartCountryList; } set { _strStartCountryList = value; } }
    public string TRANS_END_LOCATION_LIST { get { return _strEndLocList; } set { _strEndLocList = value; } }
    public string TRANS_END_COUNTRY_LIST { get { return _strEndCountryList; } set { _strEndCountryList = value; } }
    public string TRANS_REQUEST_TYPE_LIST { get { return _strRequestTypeList; } set { _strRequestTypeList = value; } }

    //Advance details
    private double _dblAdvanceAmount = 0d;
    private DateTime _dtRequiredByDate = Convert.ToDateTime("01/01/0001");

    public double ADVANCE_AMOUNT { get { return _dblAdvanceAmount; } set { _dblAdvanceAmount = value; } }
    public DateTime REQUIRED_BY_DATE { get { return _dtRequiredByDate; } set { _dtRequiredByDate = value; } }

    //Costcenter details
    private string _strCostSlNo = string.Empty;
    private double _dblCostPercentage = 0d;
    private string _strCostCostCenter = string.Empty;

    public string COST_SL_NO { get { return _strCostSlNo; } set { _strCostSlNo = value; } }
    public double COST_PERCENTAGE { get { return _dblCostPercentage; } set { _dblCostPercentage = value; } }
    public string COST_COSTCENTER { get { return _strCostCostCenter; } set { _strCostCostCenter = value; } }

    //Multiple Costcenter list
    private string _strCostSlNoList = string.Empty;
    private string _strCostPercentageList = string.Empty;
    private string _strCostCostCenterList = string.Empty;

    public string COST_SL_NO_LIST { get { return _strCostSlNoList; } set { _strCostSlNoList = value; } }
    public string COST_PERCENTAGE_LIST { get { return _strCostPercentageList; } set { _strCostPercentageList = value; } }
    public string COST_COSTCENTER_LIST { get { return _strCostCostCenterList; } set { _strCostCostCenterList = value; } }

    //Comments...
    private string _strComments = string.Empty;
    private double _dblEstimatedCost = 0d;

    public string COMMENTS { get { return _strComments; } set { _strComments = value; } }
    public double ESTIMATED_COST { get { return _dblEstimatedCost; } set { _dblEstimatedCost = value; } }

    private string _strTransSlNoList = string.Empty;
    public string TRANS_SL_NO_LIST { get { return _strTransSlNoList; } set { _strTransSlNoList = value; } }
}

public class flightonwardbo
{
    public flightonwardbo()
    {
    }

    private string _strSlNo = string.Empty;
    private DateTime _dtFlightTripStartDate = Convert.ToDateTime("01/01/1900");
    private string _strFlightStartHour = string.Empty;
    private string _strFlightStartMin = string.Empty;
    private string _strFlightFromCity = string.Empty;
    private string _strFlightStartCountry = string.Empty;
    private string _strFlightDestinationCity = string.Empty;
    private string _strFlightDestinationCountry = string.Empty;
    private bool _blFlightIsOnward = true;

    public string SL_NO
    {
        get { return _strSlNo; }
        set { _strSlNo = value; }
    }
    
    public DateTime FLIGHT_START_DATE
    {
        get { return _dtFlightTripStartDate; }
        set { _dtFlightTripStartDate = value; }
    }

    public string FLIGHT_START_HOUOR
    {
        get { return _strFlightStartHour; }
        set { _strFlightStartHour = value; }
    }

    public string FLIGHT_START_MIN
    {
        get { return _strFlightStartMin; }
        set { _strFlightStartMin = value; }
    }

    public string FLIGHT_FROM_CITY
    {
        get { return _strFlightFromCity; }
        set { _strFlightFromCity = value; }
    }

    public string FLIGHT_FROM_COUNTRY
    {
        get { return _strFlightStartCountry; }
        set { _strFlightStartCountry = value; }
    }

    public string FLIGHT_DESTINATION_CITY
    {
        get { return _strFlightDestinationCity; }
        set { _strFlightDestinationCity = value; }
    }

    public string FLIGHT_DESTINATION_COUNTRY
    {
        get { return _strFlightDestinationCountry; }
        set { _strFlightDestinationCountry = value; }
    }

    public bool FLIGHT_IS_ONWARD
    {
        get { return _blFlightIsOnward; }
        set { _blFlightIsOnward = value; }
    }
}

public class hotelbo
{
    public hotelbo()
    {
    }

    private string _strSlNo = string.Empty;
    private DateTime _dtArrivalDate = DateTime.Now.Date;
    private DateTime _dtDepartureDate = DateTime.Now.Date;
    private string _strCity = string.Empty;
    private string _strCountry = string.Empty;

    public string SL_NO
    {
        get { return _strSlNo; }
        set { _strSlNo = value; }
    }

    public DateTime HOTEL_ARRIVAL_DATE
    {
        get { return _dtArrivalDate; }
        set { _dtArrivalDate = value; }
    }

    public DateTime HOTEL_DEPARTURE_DATE
    {
        get { return _dtDepartureDate; }
        set { _dtDepartureDate = value; }
    }

    public string HOTEL_CITY
    {
        get { return _strCity; }
        set { _strCity = value; }
    }

    public string HOTEL_COUNTRY
    {
        get { return _strCountry; }
        set { _strCountry = value; }
    }
}

public class carbo
{
    public carbo()
    {

    }

    private string _strSlNo = string.Empty;
    private DateTime _dtPickDate = DateTime.Now.Date;
    private string _strPickHour = "00";
    private string _strPickMin = "00";
    private string _strPickCity = string.Empty;
    private string _strPickCountry = string.Empty;
    private DateTime _dtDropDate = DateTime.Now.Date;
    private string _strDropHour = "00";
    private string _strDropMin = "00";
    private string _strDropCity = string.Empty;
    private string _strDropCountry = string.Empty;

    public string SL_NO
    {
        get { return _strSlNo; }
        set { _strSlNo = value; }
    }

    public DateTime CAR_PICK_DATE
    {
        get { return _dtPickDate; }
        set { _dtPickDate = value; }
    }

    public string CAR_PICK_HOUR
    {
        get { return _strPickHour; }
        set { _strPickHour = value; }
    }

    public string CAR_PICK_MIN
    {
        get { return _strPickMin; }
        set { _strPickMin = value; }
    }

    public string CAR_PICK_CITY
    {
        get { return _strPickCity; }
        set { _strPickCity = value; }
    }

    public string CAR_PICK_COUNTRY
    {
        get { return _strPickCountry; }
        set { _strPickCountry = value; }
    }

    public DateTime CAR_DROP_DATE
    {
        get { return _dtDropDate; }
        set { _dtDropDate = value; }
    }

    public string CAR_DROP_HOUR
    {
        get { return _strDropHour; }
        set { _strDropHour = value; }
    }

    public string CAR_DROP_MIN
    {
        get { return _strDropMin; }
        set { _strDropMin = value; }
    }

    public string CAR_DROP_CITY
    {
        get { return _strDropCity; }
        set { _strDropCity = value; }
    }

    public string CAR_DROP_COUNTRY
    {
        get { return _strDropCountry; }
        set { _strDropCountry = value; }
    }
}

public class trainbo
{
    public trainbo()
    {

    }

    private string _strSlNo = string.Empty;
    private DateTime _dtTrainDepDate = DateTime.Now.Date;
    private string _strTrainStartHour = string.Empty;
    private string _strTrainStartMin = string.Empty;
    private string _strTrainStartCity = string.Empty;
    private string _strTrainStartCountry = string.Empty;
    private string _strTrainToCity = string.Empty;
    private string _strTrainToCountry = string.Empty;
    private bool _blTrainInOnward = true;

    public string SL_NO
    {
        get { return _strSlNo; }
        set { _strSlNo = value; }
    }

    public DateTime TRAIN_START_DATE
    {
        get { return _dtTrainDepDate; }
        set { _dtTrainDepDate = value; }
    }

    public string TRAIN_START_HOUR
    {
        get { return _strTrainStartHour; }
        set { _strTrainStartHour = value; }
    }

    public string TRAIN_START_MIN
    {
        get { return _strTrainStartMin; }
        set { _strTrainStartMin = value; }
    }

    public string TRAIN_FROM_CITY
    {
        get { return _strTrainStartCity; }
        set { _strTrainStartCity = value; }
    }

    public string TRAIN_FROM_COUNTRY
    {
        get { return _strTrainStartCountry; }
        set { _strTrainStartCountry = value; }
    }

    public string TRAIN_TO_CITY
    {
        get { return _strTrainToCity; }
        set { _strTrainToCity = value; }
    }

    public string TRAIN_TO_COUNTRY
    {
        get { return _strTrainToCountry; }
        set { _strTrainToCountry = value; }
    }

    public bool TRAIN_IS_ONWARD
    {
        get { return _blTrainInOnward; }
        set { _blTrainInOnward = value; }
    }
}


public class TrvlReqDetails
{
    public TrvlReqDetails()
    { }

    //---------------------- TRAVEL REQUEST ----------------
    private string _pernr = string.Empty;
    private string _reinr = string.Empty;
    private string _molga = string.Empty;
    private string _morei = string.Empty;
    private string _schem = string.Empty;
    private string _kzrea = string.Empty;
    private string _zort1 = string.Empty;
    private string _zland = string.Empty;
    private string _kunde = string.Empty;
    private string _datc1 = string.Empty;
    private DateTime? _datv1 = Convert.ToDateTime("01/01/0001");
    private string _uhrv1 = string.Empty;
    private string _datc2 = string.Empty;
    private DateTime? _datb1 = Convert.ToDateTime("01/01/0001");
    private string _uhrb1 = string.Empty;
    private string _perio = string.Empty;
    private decimal _addit_amnt = 0;
    private decimal _sum_reimbu = 0;
    private decimal _sum_advanc = 0;
    private decimal _sum_payout = 0;
    private decimal _sum_paidco = 0;
    private decimal _trip_total = 0;
    private string _currency = string.Empty;
    private string _comp_code = string.Empty;
    private string _bus_area = string.Empty;
    private string _co_area = string.Empty;
    private string _costcenter = string.Empty;
    private string _internal_order = string.Empty;
    private string _cost_obj = string.Empty;
    private string _wbs_elemt = string.Empty;
    private string _network = string.Empty;
    private string _activity = string.Empty;

    private DateTime? _updated_on = Convert.ToDateTime("01/01/0001");
    private string _approved_by = string.Empty;
    private DateTime? _approved_on = Convert.ToDateTime("01/01/0001");
    private string _status = string.Empty;
    private string _comments = string.Empty;


    private string _rcurr = string.Empty;
    private string _exp_type = string.Empty;
    private string _exp_type_name = string.Empty;
    private DateTime? _s_date = Convert.ToDateTime("01/01/0001");
    private string _no_days = string.Empty;

    private string _daily_rate = string.Empty;
    private string _expt_amt = string.Empty;
    private string _expt_curr = string.Empty;

    private string _exc_rate = string.Empty;
    private string _re_amt = string.Empty;
    private string _justify = string.Empty;
    private string _receipt_file = string.Empty;
    private string _receipt_fid = string.Empty;
    private string _receipt_fiid = string.Empty;
    private string _receipt_fpath = string.Empty;
    private DateTime? _created_on = Convert.ToDateTime("01/01/0001");
    private string _created_by = string.Empty;

    private decimal _TotalAmount = 0;

    private int _id = 0;

    private DateTime? approved_on1;
    private string approved_by1 = string.Empty;
    private string remarks1 = string.Empty;
    private DateTime? approved_on2;
    private string approved_by2 = string.Empty;
    private string remarks2 = string.Empty;
    private DateTime? approved_on3;
    private string approved_by3 = string.Empty;
    private string remarks3 = string.Empty;
    private DateTime? approved_on4;
    private string approved_by4 = string.Empty;
    private string remarks4 = string.Empty;
    private DateTime? approved_on5;
    private string approved_by5 = string.Empty;
    private string remarks5 = string.Empty;
    private DateTime? approved_on6;
    private string approved_by6 = string.Empty;
    private string remarks6 = string.Empty;
    private DateTime? approved_on7;
    private string approved_by7 = string.Empty;
    private string remarks7 = string.Empty;
    private DateTime? approved_on8;
    private string approved_by8= string.Empty;
    private string remarks8 = string.Empty;
    private DateTime? approved_on9;
    private string approved_by9 = string.Empty;
    private string remarks9 = string.Empty;
    private string approved_by1n = string.Empty;
    private string approved_by2n = string.Empty;
    private string approved_by3n = string.Empty;
    private string approved_by4n = string.Empty;
    private string approved_by5n = string.Empty;
    private string approved_by6n = string.Empty;
    private string approved_by7n = string.Empty;
    private string ename = string.Empty;
    private string adv_amt = string.Empty;
    private string adv_curr = string.Empty;
    private string settled = string.Empty;
    private string purpose = string.Empty;
    private int? cid = 0;
    private int? lid = 0;
    private string deviation_amt = string.Empty;
    private string deviation_curr = string.Empty;
    private string dailyrate_curr = string.Empty;
    private string countryID = string.Empty;
    private string regionID = string.Empty;
    private string expid = string.Empty;

    private string prjid = string.Empty;
    private string bukrs = string.Empty;
    private string task = string.Empty;

    //----------------- TRAVEL REQUEST UPDATE ------------------
    public int ID { get { return _id; } set { _id = value; } }
    public string PERNR { get { return _pernr; } set { _pernr = value; } }
    public string REINR { get { return _reinr; } set { _reinr = value; } }
    public string MOLGA { get { return _molga; } set { _molga = value; } }
    public string MOREI { get { return _morei; } set { _morei = value; } }
    public string SCHEM { get { return _schem; } set { _schem = value; } }
    public string KZREA { get { return _kzrea; } set { _kzrea = value; } }
    public string ZORT1 { get { return _zort1; } set { _zort1 = value; } }
    public string ZLAND { get { return _zland; } set { _zland = value; } }
    public string KUNDE { get { return _kunde; } set { _kunde = value; } }
    public string DATC1 { get { return _datc1; } set { _datc1 = value; } }
    public DateTime? DATV1 { get { return _datv1; } set { _datv1 = value; } }
    public string UHRV1 { get { return _uhrv1; } set { _uhrv1 = value; } }
    public string DATC2 { get { return _datc2; } set { _datc2 = value; } }
    public DateTime? DATB1 { get { return _datb1; } set { _datb1 = value; } }
    public string UHRB1 { get { return _uhrb1; } set { _uhrb1 = value; } }
    public string PERIO { get { return _perio; } set { _perio = value; } }
    public decimal ADDIT_AMNT { get { return _addit_amnt; } set { _addit_amnt = value; } }
    public decimal SUM_REIMBU { get { return _sum_reimbu; } set { _sum_reimbu = value; } }
    public decimal SUM_ADVANC { get { return _sum_advanc; } set { _sum_advanc = value; } }
    public decimal SUM_PAYOUT { get { return _sum_payout; } set { _sum_payout = value; } }
    public decimal SUM_PAIDCO { get { return _sum_paidco; } set { _sum_paidco = value; } }
    public decimal TRIP_TOTAL { get { return _trip_total; } set { _trip_total = value; } }
    public string CURRENCY { get { return _currency; } set { _currency = value; } }
    public string COMP_CODE { get { return _comp_code; } set { _comp_code = value; } }
    public string BUS_AREA { get { return _bus_area; } set { _bus_area = value; } }
    public string CO_AREA { get { return _co_area; } set { _co_area = value; } }
    public string COSTCENTER { get { return _costcenter; } set { _costcenter = value; } }
    public string INTERNAL_ORDER { get { return _internal_order; } set { _internal_order = value; } }
    public string COST_OBJ { get { return _cost_obj; } set { _cost_obj = value; } }
    public string WBS_ELEMT { get { return _wbs_elemt; } set { _wbs_elemt = value; } }
    public string NETWORK { get { return _network; } set { _network = value; } }
    public string ACTIVITY { get { return _activity; } set { _activity = value; } }

    public DateTime? UPDATED_ON { get { return _updated_on; } set { _updated_on = value; } }
    public string APPROVED_BY { get { return _approved_by; } set { _approved_by = value; } }
    public DateTime? APPROVED_ON { get { return _approved_on; } set { _approved_on = value; } }
    public string STATUS { get { return _status; } set { _status = value; } }
    public string COMMENTS { get { return _comments; } set { _comments = value; } }

    public string RCURR { get { return _rcurr; } set { _rcurr = value; } }
    public string EXP_TYPE { get { return _exp_type; } set { _exp_type = value; } }
    public string EXP_TYPE_NAME { get { return _exp_type_name; } set { _exp_type_name = value; } }
    public DateTime? S_DATE { get { return _s_date; } set { _s_date = value; } }
    public string NO_DAYS { get { return _no_days; } set { _no_days = value; } }
    public string DAILY_RATE { get { return _daily_rate; } set { _daily_rate = value; } }
    public string EXPT_AMT { get { return _expt_amt; } set { _expt_amt = value; } }
    public string EXPT_CURR { get { return _expt_curr; } set { _expt_curr = value; } }
    public string EXC_RATE { get { return _exc_rate; } set { _exc_rate = value; } }
    public string RE_AMT { get { return _re_amt; } set { _re_amt = value; } }
    public string JUSTIFY { get { return _justify; } set { _justify = value; } }
    public string RECEIPT_FILE { get { return _receipt_file; } set { _receipt_file = value; } }
    public string RECEIPT_FID { get { return _receipt_fid; } set { _receipt_fid = value; } }
    public string RECEIPT_FIID { get { return _receipt_fiid; } set { _receipt_fiid = value; } }
    public string RECEIPT_FPATH { get { return _receipt_fpath; } set { _receipt_fpath = value; } }
    public DateTime? CREATED_ON { get { return _created_on; } set { _created_on = value; } }
    public string CREATED_BY { get { return _created_by; } set { _created_by = value; } }
    public decimal TotalAmount { get { return _TotalAmount; } set { _TotalAmount = value; } }

    public DateTime? APPROVED_ON1 { get { return approved_on1; } set { approved_on1 = value; } }
    public string APPROVED_BY1 { get { return approved_by1; } set { approved_by1 = value; } }
    public string REMARKS1 { get { return remarks1; } set { remarks1 = value; } }
    public DateTime? APPROVED_ON2 { get { return approved_on2; } set { approved_on2 = value; } }
    public string APPROVED_BY2 { get { return approved_by2; } set { approved_by2 = value; } }
    public string REMARKS2 { get { return remarks2; } set { remarks2 = value; } }
    public DateTime? APPROVED_ON3 { get { return approved_on3; } set { approved_on3 = value; } }
    public string APPROVED_BY3 { get { return approved_by3; } set { approved_by3 = value; } }
    public string REMARKS3 { get { return remarks3; } set { remarks3 = value; } }
    public DateTime? APPROVED_ON4 { get { return approved_on4; } set { approved_on4 = value; } }
    public string APPROVED_BY4 { get { return approved_by4; } set { approved_by4 = value; } }
    public string REMARKS4 { get { return remarks4; } set { remarks4 = value; } }
    public DateTime? APPROVED_ON5 { get { return approved_on5; } set { approved_on5 = value; } }
    public string APPROVED_BY5 { get { return approved_by5; } set { approved_by5 = value; } }
    public string REMARKS5 { get { return remarks5; } set { remarks5 = value; } }
    public DateTime? APPROVED_ON6 { get { return approved_on6; } set { approved_on6 = value; } }
    public string APPROVED_BY6 { get { return approved_by6; } set { approved_by6 = value; } }
    public string REMARKS6 { get { return remarks6; } set { remarks6 = value; } }
    public DateTime? APPROVED_ON7 { get { return approved_on7; } set { approved_on7 = value; } }
    public string APPROVED_BY7 { get { return approved_by7; } set { approved_by7 = value; } }
    public string REMARKS7 { get { return remarks7; } set { remarks7 = value; } }

    public DateTime? APPROVED_ON8 { get { return approved_on8; } set { approved_on8 = value; } }
    public string APPROVED_BY8 { get { return approved_by8; } set { approved_by8 = value; } }
    public string REMARKS8 { get { return remarks8; } set { remarks8 = value; } }
    public DateTime? APPROVED_ON9 { get { return approved_on9; } set { approved_on9 = value; } }
    public string APPROVED_BY9 { get { return approved_by9; } set { approved_by9 = value; } }
    public string REMARKS9 { get { return remarks9; } set { remarks9 = value; } }

    public string ENAME { get { return ename; } set { ename = value; } }

    public string ADV_AMOUNT { get { return adv_amt; } set { adv_amt = value; } }
    public string ADV_CURR { get { return adv_curr; } set { adv_curr = value; } }
    public string SETTLED { get { return settled; } set { settled = value; } }
    public string PURPOSE { get { return purpose; } set { purpose = value; } }
    public int? CID { get { return cid; } set { cid = value; } }
    public int? LID { get { return lid; } set { lid = value; } }
    public string DEVIATION_AMT { get { return deviation_amt; } set { deviation_amt = value; } }
    public string DEVIATION_CURR { get { return deviation_curr; } set { deviation_curr = value; } }
    public string DAILY_CURR { get { return dailyrate_curr; } set { dailyrate_curr = value; } }
    public string TASK { get { return task; } set { task = value; } }
    public string BUKRS { get { return bukrs; } set { bukrs = value; } }

    public string CountryID { get { return countryID; } set { countryID = value; } }
    public string RegoinID { get { return regionID; } set { regionID = value; } }
    public string EXPID { get { return expid; } set { expid = value; } }

    public string PRJID { get { return prjid; } set { prjid = value; } }


    public string APPROVED_BY1N
    {
        get { return approved_by1n; }
        set { approved_by1n = value; }

    }

    public string APPROVED_BY2N
    {
        get { return approved_by2n; }
        set { approved_by2n = value; }

    }
    public string APPROVED_BY3N
    {
        get { return approved_by3n; }
        set { approved_by3n = value; }

    }
    public string APPROVED_BY4N
    {
        get { return approved_by4n; }
        set { approved_by4n = value; }

    }
    public string APPROVED_BY5N
    {
        get { return approved_by5n; }
        set { approved_by5n = value; }

    }
    public string APPROVED_BY6N
    {
        get { return approved_by6n; }
        set { approved_by6n = value; }

    }
    public string APPROVED_BY7N
    {
        get { return approved_by7n; }
        set { approved_by7n = value; }

    }

    public long? Rnum
    {
        get;
        set;
    }

}

public class ValidTrvlDt
{
   // public bool Result { get; set; }
    private bool _Res = false;
    public bool ValidResult
    {
        get { return _Res; }
        set { _Res = value; }
    }
}

public class requisitionbo
{
    public string mode { get; set; }
    public string TICKET_FARE { get; set; }
    public string JourneyParticulars { get; set; }
    public string FTPT_REQUEST_ID_FOR_PROPOSAL { get; set; }
    public string REQ_SEGMENT_ID_FOR_PROPOSAL { get; set; }
    public int FTPT_REQUEST_ID { get; set; }
    public string FTPT_REQUEST_ID1 { get; set; }
    public int REQ_SEGMENT_ID { get; set; }
    public string EMPLOYEE_NO { get; set; }
    public string EMPLOYEE_NAME { get; set; }
    public string CHK { get; set; }
    public DateTime TRAVEL_DATE{ get; set; }
    public string TRAVEL_DATE_ALL { get; set; }
    public string TRAVEL_TIME { get; set; }
    public string ARRIVAL_DATE { get; set;}
    public string ARRIVAL_TIME { get; set; }
    public string REASON{ get; set; }
    public string TravelType { get; set; }
    
    public string MODE_OF_TRANSPOPRT_KZPMF { get; set; }
    public string MODE_OF_TRANSPOPRT_FZTXT { get; set; }
    public string MEDIA_OF_CATEGORY_PKWKL { get; set; }
    public string MEDIA_OF_CATEGORY_TEXT25 { get; set; }
    public string VEHICLE_NAME_VHNUM { get; set; }
    public string VEHICLE_NAME_VHNUM_ALL { get; set; }
    public string VEHICLE_NAME_ZZVEHNAM { get; set; }
    public string REGION_RGION_FROM { get; set; }
    public string REGION_RGION_TO { get; set; }
    public string REGION_TEXT25_FROM { get; set; }
    public string REGION_TEXT25_TO { get; set; }
    public string PURPOSE_OF_TRAVEL_ACTIVITY { get; set; }
    public string PURPOSE_OF_TRAVEL_NAME { get; set; }
    public string FLYNUM { get; set; }
    public string SUP_COMMENT { get; set; }
    public string AIRLINE { get; set; }
    public bool VISA_REQUIRED { get; set; }
    public string VISA_REQUIRED_ALL { get; set;}
    public string FR_EXCHANGE { get; set; }
    public string INSUR_MEDICLAIM { get; set; }
    public string SEAT_PREFERENCE { get; set; }
    public string SEAT_PREFERENCE_TEXT { get; set; }
    public string MEAL_PREFERENCE { get; set; }
    public string MEAL_PREFERENCE_TEXT { get; set; }
    public string BAGGAGE { get; set; }
    public string HAND { get; set; }
    public string ADVANCE { get; set; }
    public string CREATED_BY { get; set; }
    public string CURRENT_STATUS { get; set; }
    public string REMARKS { get; set; }
    public string HOD_REMARKS { get; set; }
    public string TD_REMARKS { get; set; }
    public string REASON_FOR_CANCEL { get; set; }
    public byte ISACTIVE { get; set; }
    public string  EMAIL { get; set; }
    public string PHONE_NUMBER{ get; set; }
    public string DESIGNATION { get; set; }
    public bool IS_EMPLOYEE_CHECKD { get; set; }

    public string SPKZL { get; set; }
    public string SPTXT { get; set; }

    public string WAERS { get; set; }
    public string LTEXT { get; set; }

    //----- ADDITIONAL FIELDS -----

    public string TRAVEL_VEH_TYPE { get; set; }
    public string TRAVEL_VEH_CATEGORY { get; set; }

    public int ClaimSlNo { get; set; }
}
public class RequisitionAndProposalCountBO
{
    public string PERNR { get; set; }
    public int LocalAccommodationProposalsCount { get; set; }
    public int LocalAccommodationRequisitionCount { get; set; }

    public int LocalTravelProposalsCount { get; set; }
    public int LocalTravelRequisitionCount { get; set; }

    public int OutStationProposalsCount { get; set; }
    public int OutStationRequisitionCount { get; set; }
}
public class ClaimViewBo
{
    public int Slno { get; set; }
    public string PERNR { get; set; }
    public int REQUISITION_ID { get; set; }
    public int REQ_SEGMENT_ID { get; set; }
    public string DATE1 { get; set; }
    public string From_Place { get; set; }
    public string To_Place { get; set; }
    public double FARE { get; set; }
    public string Exp_type { get; set; }
    public string Currency_Type { get; set; }
    public string TravelType { get; set; }
}

public class visaPassportBo
{
    public visaPassportBo()
    {
    }
 
    private string _strPERNR = string.Empty; 
    private string _strICTYPE = string.Empty;
    private string _strICNUM = string.Empty;
    private DateTime _dtENDDA = DateTime.Now.Date;
    private string _strFRFLYNUM = string.Empty;
    private string _strEMPNAME = string.Empty;
    private string _strAIRLINE = string.Empty;
    private string _strVALSTATUS = string.Empty;
    private string _strPASNUM = string.Empty;
    private DateTime _strDOI;
    private DateTime _strDOE;
    private string _strPLISS;
    private string _strTRINSNO;
    private string _strPLAN1;
    private string _strPREMIUM;
    private string _strAGENT_NAME;
    private string _strVINUM;
    private string _strCOUNTRY;
    private string _strVISA_TYPE;


    public string VINUM
    {
        get { return _strVINUM; }
        set { _strVINUM = value; }
    }

    public string COUNTRY
    {
        get { return _strCOUNTRY; }
        set { _strCOUNTRY = value; }
    }

    public string VISA_TYPE
    {
        get { return _strVISA_TYPE; }
        set { _strVISA_TYPE = value; }
    }


    public string TRINSNO
    {
        get { return _strTRINSNO; }
        set { _strTRINSNO = value; }
    }

    public string PLAN1
    {
        get { return _strPLAN1; }
        set { _strPLAN1 = value; }
    }

    public string PREMIUM
    {
        get { return _strPREMIUM; }
        set { _strPREMIUM = value; }
    }

    public string AGENT_NAME
    {
        get { return _strAGENT_NAME; }
        set { _strAGENT_NAME = value; }
    }


    public string PASNUM
    {
        get { return _strPASNUM; }
        set { _strPASNUM = value; }
    }

    public DateTime DOI
    {
        get { return _strDOI; }
        set { _strDOI = value; }
    }
    public DateTime DOE
    {
        get { return _strDOE; }
        set { _strDOE = value; }
    }

    public string PLISS
    {
        get { return _strPLISS; }
        set { _strPLISS = value; }
    }



    public string FRFLYNUM
    {
        get { return _strFRFLYNUM; }
        set { _strFRFLYNUM = value; }
    }

    public string EMPNAME
    {
        get { return _strEMPNAME; }
        set { _strEMPNAME = value; }
    }

    public string AIRLINE
    {
        get { return _strAIRLINE; }
        set { _strAIRLINE = value; }
    }

    public string VALSTATUS
    {
        get { return _strVALSTATUS; }
        set { _strVALSTATUS = value; }
    }



    public string PERNR
    {
        get { return _strPERNR; }
        set { _strPERNR = value; }
    } 

    public string ICTYPE
    {
        get { return _strICTYPE; }
        set { _strICTYPE = value; }
    }

    public string ICNUM
    {
        get { return _strICNUM; }
        set { _strICNUM = value; }
    }

    public DateTime ENDDA
    {
        get { return _dtENDDA; }
        set { _dtENDDA = value; }
    }


}
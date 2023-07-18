using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for travelexpensecolumnsbo
/// </summary>
public class travelexpensecolumnsbo
{
	public travelexpensecolumnsbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _strTravelPernr = string.Empty;
    public string TRAVEL_PERNR { get { return _strTravelPernr; } set { _strTravelPernr = value; } }
    private long _iTravelRequestNo = 0;
    public long TRAVEL_REQUEST_NO { get { return _iTravelRequestNo; } set { _iTravelRequestNo = value; } }

    private long _lGTRequestNo = 0L;

    public long ER_REQUEST_NUMBER 
    { 
        get 
        { 
            return _lGTRequestNo; 
        } 
        set 
        { 
            _lGTRequestNo = value; 
        } 
    }

    private DateTime _dtGTTripStartDate = Convert.ToDateTime("01/01/0001");

    public DateTime ER_TRIP_START_DATE 
    { 
        get 
        { 
            return _dtGTTripStartDate; 
        } 
        set 
        { 
            _dtGTTripStartDate = value; 
        } 
    }

    private string _strGTTripStartHour = string.Empty;

    public string ER_TRIP_START_HOUR 
    { 
        get 
        { 
            return _strGTTripStartHour; 
        } 
        set 
        { 
            _strGTTripStartHour = value; 
        } 
    }

    private string _strGTTripStartMin = string.Empty;

    public string ER_TRIP_START_MINUTE 
    { 
        get 
        { 
            return _strGTTripStartMin; 
        } 
        set 
        { 
            _strGTTripStartMin = value; 
        } 
    }

    private DateTime _dtGTTripEndDate = Convert.ToDateTime("01/01/0001");

    public DateTime ER_TRIP_END_DATE 
    { 
        get 
        { 
            return _dtGTTripEndDate; 
        } 
        set 
        { 
            _dtGTTripEndDate = value; 
        } 
    }

    private string _strGTTripEndHour = string.Empty;

    public string ER_TRIP_END_HOUR 
    { 
        get 
        { 
            return _strGTTripEndHour; 
        } 
        set 
        { 
            _strGTTripEndHour = value; 
        } 
    }

    private string _strGTTripEndMin = string.Empty;

    public string ER_TRIP_END_MINUTE 
    { 
        get 
        { 
            return _strGTTripEndMin; 
        } 
        set 
        { 
            _strGTTripEndMin = value; 
        } 
    }

    //Grid columns
    private string _strGTEvent = string.Empty;

    public string GT_EVENT 
    { 
        get 
        { 
            return _strGTEvent; 
        } 
        set 
        { 
            _strGTEvent = value; 
        } 
    }

    private DateTime _dtGTDate = Convert.ToDateTime("01/01/0001");

    public DateTime GT_DATE 
    { 
        get 
        { 
            return _dtGTDate; 
        } 
        set 
        { 
            _dtGTDate = value; 
        } 
    }

    private string _strGTStartHour = string.Empty;

    public string GT_START_HOUR 
    { 
        get 
        { 
            return _strGTStartHour; 
        } 
        set 
        { 
            _strGTStartHour = value; 
        } 
    }
    
    private string _strGTStartMin = string.Empty;

    public string GT_START_MINUTE 
    { 
        get 
        { 
            return _strGTStartMin; 
        } 
        set 
        { 
            _strGTStartMin = value; 
        } 
    }

    private string _strGTLocation = string.Empty;

    public string GT_LOCATION 
    { 
        get 
        { 
            return _strGTLocation; 
        } 
        set 
        { 
            _strGTLocation = value; 
        } 
    }

    private string _strGTCountry = string.Empty;

    public string GT_COUNTRY 
    { 
        get 
        { 
            return _strGTCountry; 
        } 
        set 
        { 
            _strGTCountry = value; 
        } 
    }

    private string _strGTRegion = string.Empty;

    public string GT_REGION 
    { 
        get 
        { 
            return _strGTRegion; 
        } 
        set 
        { 
            _strGTRegion = value; 
        } 
    }

    private string _strGTReason = string.Empty;

    public string GT_REASON 
    { 
        get 
        { 
            return _strGTReason; 
        } 
        set 
        { 
            _strGTReason = value;
        } 
    }

    private string _strGTStatutory = string.Empty;

    public string GT_STATUTORY
    {
        get
        {
            return _strGTStatutory;
        }
        set
        {
            _strGTStatutory = value;
        }
    }

    private string _strGTEnterprise = string.Empty;

    public string GT_ENTERPRISE
    {
        get
        {
            return _strGTEnterprise;
        }
        set
        {
            _strGTEnterprise = value;
        }
    }

    private string _strGTActivity = string.Empty;

    public string GT_ACTIVITY
    {
        get
        {
            return _strGTActivity;
        }
        set
        {
            _strGTActivity = value;
        }
    }

    //Grid columns string
    private string _strGTEventList = string.Empty;

    public string GT_EVENT_LIST
    {
        get
        {
            return _strGTEventList;
        }
        set
        {
            _strGTEventList = value;
        }
    }

    private string _strGTDateList = string.Empty;

    public string GT_DATE_LIST
    {
        get
        {
            return _strGTDateList;
        }
        set
        {
            _strGTDateList = value;
        }
    }

    private string _strGTStartHourList = string.Empty;

    public string GT_START_HOUR_LIST
    {
        get
        {
            return _strGTStartHourList;
        }
        set
        {
            _strGTStartHourList = value;
        }
    }

    private string _strGTStartMinList = string.Empty;

    public string GT_START_MINUTE_LIST
    {
        get
        {
            return _strGTStartMinList;
        }
        set
        {
            _strGTStartMinList = value;
        }
    }

    private string _strGTLocationList = string.Empty;

    public string GT_LOCATION_LIST
    {
        get
        {
            return _strGTLocationList;
        }
        set
        {
            _strGTLocationList = value;
        }
    }

    private string _strGTCountryList = string.Empty;

    public string GT_COUNTRY_LIST
    {
        get
        {
            return _strGTCountryList;
        }
        set
        {
            _strGTCountryList = value;
        }
    }

    private string _strGTRegionList = string.Empty;

    public string GT_REGION_LIST
    {
        get
        {
            return _strGTReasonList;
        }
        set
        {
            _strGTReasonList = value;
        }
    }

    private string _strGTReasonList = string.Empty;

    public string GT_REASON_LIST
    {
        get
        {
            return _strGTReasonList;
        }
        set
        {
            _strGTReasonList = value;
        }
    }

    private string _strGTStatutoryList = string.Empty;

    public string GT_STATUTORY_LIST
    {
        get
        {
            return _strGTStatutoryList;
        }
        set
        {
            _strGTStatutoryList = value;
        }
    }

    private string _strGTEnterpriseList = string.Empty;

    public string GT_ENTERPRISE_LIST
    {
        get
        {
            return _strGTEnterpriseList;
        }
        set
        {
            _strGTEnterpriseList = value;
        }
    }

    private string _strGTActivityList = string.Empty;

    public string GT_ACTIVITY_LIST
    {
        get { return _strGTActivityList; }
        set { _strGTActivityList = value; }
    }

    //Advance details
    private double _dblGTAdvanceAmount = 0D;

    public double GT_ADVANCE_AMOUNT
    {
        get { return _dblGTAdvanceAmount; }
        set { _dblGTAdvanceAmount = value; }
    }

    private DateTime _dtGTAdvanceRequiredByDate = Convert.ToDateTime("01/01/0001");

    public DateTime GT_ADVANCE_REQUIRED_BY_DATE
    {
        get { return _dtGTAdvanceRequiredByDate; }
        set { _dtGTAdvanceRequiredByDate = value; }
    }

    //Cost assignment details
    private short _shrtGTSlNo = 0;

    public short GT_SL_NO
    {
        get { return _shrtGTSlNo; }
        set { _shrtGTSlNo = value; }
    }

    private float _fGTPercent = 0F;

    public float GT_PERCENT
    {
        get { return _fGTPercent; }
        set { _fGTPercent = value; }
    }

    private string _strGTCostCenter = string.Empty;

    public string GT_COSTCENTER
    {
        get { return _strGTCostCenter; }
        set { _strGTCostCenter = value; }
    }

    private string _strGTComments = string.Empty;

    public string GT_COMMENTS
    {
        get { return _strGTComments; }
        set { _strGTComments = value; }
    }

    //Mileage
    private double _dblMLKm = 0D;

    public double ML_KILOMETER
    {
        get { return _dblMLKm; }
        set { _dblMLKm = value; }
    }

    private string _strVehicleClass = string.Empty;

    //public string ML_VEHICLE_CLASS
    //{
    //    get { return _strVehicleClass; }
    //    set { _strVehicleClass = value; }
    //}

    //Mileage Grid columns
    private DateTime _dtMLDate = Convert.ToDateTime("01/01/0001");

    public DateTime ML_DATE
    {
        get { return _dtMLDate; }
        set { _dtMLDate = value; }
    }

    private double _dblMLTotalKm = 0D;

    public double ML_TOTAL_KM
    {
        get { return _dblMLTotalKm; }
        set { _dblMLTotalKm = value; }
    }

    private string _strMLCountry = string.Empty;

    public string ML_COUNTRY
    {
        get { return _strMLCountry; }
        set { _strMLCountry = value; }
    }

    private string _strMLRegion = string.Empty;

    public string ML_REGION
    {
        get { return _strMLRegion; }
        set { _strMLRegion = value; }
    }

    private string _strMLTripTypeEnterprise = string.Empty;

    public string ML_TRIP_TYPE_ENTERPRISE
    {
        get { return _strMLTripTypeEnterprise; }
        set { _strMLTripTypeEnterprise = value; }
    }

    private string _strMLLicensePlateNo = string.Empty;

    public string ML_LICENSE_PLATE_NO
    {
        get { return _strMLLicensePlateNo; }
        set { _strMLLicensePlateNo = value; }
    }

    private string _strMLVehicleType = string.Empty;

    public string ML_VEHICLE_TYPE
    {
        get { return _strMLVehicleType; }
        set { _strMLVehicleType = value; }
    }

    private string _strMLVehicleClass = string.Empty;

    public string ML_VEHICLE_CLASS
    {
        get { return _strMLVehicleClass; }
        set { _strMLVehicleClass = value; }
    }
        
    private string _strMLCarMake = string.Empty;

    public string ML_CAR_MAKE
    {
        get { return _strMLCarMake; }
        set { _strMLCarMake = value; }
    }

    private string _strMLStartLocation = string.Empty;

    public string ML_START_LOCATION
    {
        get { return _strMLStartLocation; }
        set { _strMLStartLocation = value; }
    }

    private string _strMLEndLocation = string.Empty;

    public string ML_END_LOCATION
    {
        get { return _strMLEndLocation; }
        set { _strMLEndLocation = value; }
    }

    //Mileage grid columns list
    private string _strMLDateList = string.Empty;

    public string ML_DATE_LIST
    {
        get { return _strMLDateList; }
        set { _strMLDateList = value; }
    }

    private string _strMLTotalKmList = string.Empty;

    public string ML_TOTAL_KM_LIST
    {
        get { return _strMLTotalKmList; }
        set { _strMLTotalKmList = value; }
    }

    private string _strMLCountryList = string.Empty;

    public string ML_COUNTRY_LIST
    {
        get { return _strMLCountryList; }
        set { _strMLCountryList = value; }
    }

    private string _strMLRegionList = string.Empty;

    public string ML_REGION_LIST
    {
        get { return _strMLRegionList; }
        set { _strMLRegionList = value; }
    }

    private string _strMLTripTypeEnterpriseList = string.Empty;

    public string ML_TRIP_TYPE_ENTERPRISE_LIST
    {
        get { return _strMLTripTypeEnterpriseList; }
        set { _strMLTripTypeEnterpriseList = value; }
    }

    private string _strMLLicensePlateNoList = string.Empty;

    public string ML_LICENSE_PLATE_NO_LIST
    {
        get { return _strMLLicensePlateNoList; }
        set { _strMLLicensePlateNoList = value; }
    }

    private string _strMLVehicleTypeList = string.Empty;

    public string ML_VEHICLE_TYPE_LIST
    {
        get { return _strMLVehicleTypeList; }
        set { _strMLVehicleTypeList = value; }
    }

    private string _strMLVehicleClassList = string.Empty;

    public string ML_VEHICLE_CLASS_LIST
    {
        get { return _strMLVehicleClassList; }
        set { _strMLVehicleClassList = value; }
    }

    private string _strMLCarMakeList = string.Empty;

    public string ML_CAR_MAKE_LIST
    {
        get { return _strMLCarMakeList; }
        set { _strMLCarMakeList = value; }
    }

    private string _strMLStartLocationList = string.Empty;

    public string ML_START_LOCATION_LIST
    {
        get { return _strMLStartLocationList; }
        set { _strMLStartLocationList = value; }
    }

    private string _strMLEndLocationList = string.Empty;

    public string ML_END_LOCATION_LIST
    {
        get { return _strMLEndLocationList; }
        set { _strMLEndLocationList = value; }
    }

    //Meals and Accommodation reimbursements
    private bool _blMAPerDiemMeals = false;

    public bool MA_PER_DIEM_MEALS
    {
        get { return _blMAPerDiemMeals; }
        set { _blMAPerDiemMeals = value; }
    }

    private bool _blMAPerDiemAccommodation = false;

    public bool MA_PER_DIEM_ACCOMMODATION
    {
        get { return _blMAPerDiemAccommodation; }
        set { _blMAPerDiemAccommodation = value; }
    }

    //Deductions
    private string _strMATripTypeStatutory = string.Empty;

    public string MA_TRIP_TYPE_STATUTORY
    {
        get { return _strMATripTypeStatutory; }
        set { _strMATripTypeStatutory = value; }
    }

    private string _strMATripTypeCoSpecific = string.Empty;

    public string MA_TRIP_TYPE_COM_SPECIFIC
    {
        get { return _strMATripTypeCoSpecific; }
        set { _strMATripTypeCoSpecific = value; }
    }

    private string _strMAActivity = string.Empty;

    public string MA_ACTIVITY
    {
        get { return _strMAActivity; }
        set { _strMAActivity = value; }
    }

    //Deductions grid columns
    private string _strMADay = string.Empty;

    public string MA_DAY
    {
        get { return _strMADay; }
        set { _strMADay = value; }
    }

    private DateTime _dtMADate = Convert.ToDateTime("01/01/0001");

    public DateTime MA_DATE
    {
        get { return _dtMADate; }
        set { _dtMADate = value; }
    }

    private bool _blMALunch = false;

    public bool MA_LUNCH
    {
        get { return _blMALunch; }
        set { _blMALunch = value; }
    }

    private bool _blMADinner = false;

    public bool MA_DINNER
    {
        get { return _blMADinner; }
        set { _blMADinner = value; }
    }

    //Deductions list
    private string _strMADayList = string.Empty;

    public string MA_DAY_LIST
    {
        get { return _strMADayList; }
        set { _strMADayList = value; }
    }

    private string _strMADateList = string.Empty;

    public string MA_DATE_LIST
    {
        get { return _strMADateList; }
        set { _strMADateList = value; }
    }

    private string _strMALunchList = string.Empty;

    public string MA_LUNCH_LIST
    {
        get { return _strMALunchList; }
        set { _strMALunchList = value; }
    }

    private string _strMADinnerList = string.Empty;

    public string MA_DINNER_LIST
    {
        get { return _strMADinnerList; }
        set { _strMADinnerList = value; }
    }

    //Expense Receipts
    private short _shrtERSlNo = 0;

    public short ER_SL_NO
    {
        get { return _shrtERSlNo; }
        set { _shrtERSlNo = value; }
    }

    private bool _blERPaperReceipts = false;

    public bool ER_PAPER_RECEIPTS
    {
        get { return _blERPaperReceipts; }
        set { _blERPaperReceipts = value; }
    }

    private string _strERExpenseReceipts = string.Empty;

    public string ER_EXPENSE_RECEIPTS
    {
        get { return _strERExpenseReceipts; }
        set { _strERExpenseReceipts = value; }
    }

    private double _dblERAmount = 0D;

    public double ER_AMOUNT
    {
        get { return _dblERAmount; }
        set { _dblERAmount = value; }
    }

    private DateTime _dtERExpenseOn = Convert.ToDateTime("01/01/0001");

    public DateTime ER_EXPENSE_DATE
    {
        get { return _dtERExpenseOn; }
        set { _dtERExpenseOn = value; }
    }

    private DateTime _dtERExpenseFrom = Convert.ToDateTime("01/01/0001");

    public DateTime ER_EXPENSE_FROM
    {
        get { return _dtERExpenseFrom; }
        set { _dtERExpenseFrom = value; }
    }

    private DateTime _dtERExpenseTill = Convert.ToDateTime("01/01/0001");

    public DateTime ER_EXPENSE_TILL
    {
        get { return _dtERExpenseTill; }
        set { _dtERExpenseTill = value; }
    }

    private short _shrtERNumber = 0;

    public short ER_NUMBER
    {
        get { return _shrtERNumber; }
        set { _shrtERNumber = value; }
    }

    private string _strERShortInfo = string.Empty;

    public string ER_SHORT_INFO
    {
        get { return _strERShortInfo; }
        set { _strERShortInfo = value; }
    }

    //Additional information under Expense receipts
    private short _shrtERNoOfBreakFasts = 0;

    public short ER_NO_OF_BREAKFASTS
    {
        get { return _shrtERNoOfBreakFasts; }
        set { _shrtERNoOfBreakFasts = value; }
    }

    private short _shrtERNoOfLunches = 0;

    public short ER_NO_OF_LUNCHES
    {
        get { return _shrtERNoOfLunches; }
        set { _shrtERNoOfLunches = value; }
    }

    private short _shrtERNoOfDinners = 0;

    public short ER_NO_OF_DINNERS
    {
        get { return _shrtERNoOfDinners; }
        set { _shrtERNoOfDinners = value; }
    }

    private string _strERDescription = string.Empty;

    public string ER_DESCRIPTION
    {
        get { return _strERDescription; }
        set { _strERDescription = value; }
    }

    private string _strERParticipants = string.Empty;

    public string ER_PARTICIPANTS
    {
        get { return _strERParticipants; }
        set { _strERParticipants = value; }
    }

    private string _strERAdditionalInfoReason = string.Empty;

    public string ER_ADDITIONAL_INFO_REASON
    {
        get { return _strERAdditionalInfoReason; }
        set { _strERAdditionalInfoReason = value; }
    }

    private short? _shrtERNoOfEmployees = 0;

    public short? ER_NO_OF_EMPLOYEES
    {
        get { return _shrtERNoOfEmployees; }
        set { _shrtERNoOfEmployees = value; }
    }

    private short _shrtERNoOfPartners = 0;

    public short ER_NO_OF_PARTNERS
    {
        get { return _shrtERNoOfPartners; }
        set { _shrtERNoOfPartners = value; }
    }

    private short? _shrtERNoOfGuests = 0;

    public short? ER_NO_OF_GUESTS
    {
        get { return _shrtERNoOfGuests; }
        set { _shrtERNoOfGuests = value; }
    }

    private string _strERAdditionalInfoCity = string.Empty;

    public string ER_ADDITIONAL_INFO_CITY
    {
        get { return _strERAdditionalInfoCity; }
        set { _strERAdditionalInfoCity = value; }
    }

    private string _strERAdditionalInfoCountry = string.Empty;

    public string ER_ADDITIONAL_INFO_COUNTRY
    {
        get { return _strERAdditionalInfoCountry; }
        set { _strERAdditionalInfoCountry = value; }
    }

    private string _strErAdditionalInfoRegion = string.Empty;

    public string ER_ADDITIONAL_INFO_REGION
    {
        get { return _strErAdditionalInfoRegion; }
        set { _strErAdditionalInfoRegion = value; }
    }

    private string _strERDocumentNo = string.Empty;

    public string ER_DOCUMENT_NO
    {
        get { return _strERDocumentNo; }
        set { _strERDocumentNo = value; }
    }

    private string _strERTripTypeEnterprise = string.Empty;

    public string ER_TRIP_TYPE_ENTERPRISE
    {
        get { return _strERTripTypeEnterprise; }
        set { _strERTripTypeEnterprise = value; }
    }
    private string _strERTripTypeEnterpriseID = string.Empty;

    public string ER_TRIP_TYPE_ENTERPRISE_ID
    {
        get { return _strERTripTypeEnterpriseID; }
        set { _strERTripTypeEnterpriseID = value; }
    }
    private string _strErProviderCategory = string.Empty;

    public string ER_PROVIDER_CATEGORY
    {
        get { return _strErProviderCategory; }
        set { _strErProviderCategory = value; }
    }

    private string _strERProvider = string.Empty;

    public string ER_PROVIDER
    {
        get { return _strERProvider; }
        set { _strERProvider = value; }
    }

    private string _strERComments = string.Empty;

    public string ER_COMMENTS
    {
        get { return _strERComments; }
        set { _strERComments = value; }
    }

    //CostCenter details under Expense Receipts
    private float _fERPercent = 0F;

    public float ER_PERCENT
    {
        get { return _fERPercent; }
        set { _fERPercent = value; }
    }

    private string _strERCostCenter = string.Empty;

    public string ER_COSTCENTER
    {
        get { return _strERCostCenter; }
        set { _strERCostCenter = value; }
    }

    //Expense receipts grid columns List
    private string _strERSlNoList = string.Empty;

    public string ER_SL_NO_LIST
    {
        get { return _strERSlNoList; }
        set { _strERSlNoList = value; }
    }

    private string _strERPaperReceiptsList = string.Empty;

    public string ER_PAPER_RECEIPT_LIST
    {
        get { return _strERPaperReceiptsList; }
        set { _strERPaperReceiptsList = value; }
    }

    private string _strERExpenseReceiptsList = string.Empty;

    public string ER_EXPENSE_RECEIPTS_LIST
    {
        get { return _strERExpenseReceiptsList; }
        set { _strERExpenseReceiptsList = value; }
    }

    private string _strERAmountList = string.Empty;

    public string ER_AMOUNT_LIST
    {
        get { return _strERAmountList; }
        set { _strERAmountList = value; }
    }

    private string _strERExpenseOnList = string.Empty;

    public string ER_EXPENSE_DATE_LIST
    {
        get { return _strERExpenseOnList; }
        set { _strERExpenseOnList = value; }
    }

    private string _strERExpenseFromList = string.Empty;

    public string ER_EXPENSE_FROM_LIST
    {
        get { return _strERExpenseFromList; }
        set { _strERExpenseFromList = value; }
    }

    private string _strERExpenseTillList = string.Empty;

    public string ER_EXPENSE_TILL_LIST
    {
        get { return _strERExpenseTillList; }
        set { _strERExpenseTillList = value; }
    }

    private string _strERNumberList = string.Empty;

    public string ER_NUMBER_LIST
    {
        get { return _strERNumberList; }
        set { _strERNumberList = value; }
    }

    private string _strERShortInfoList = string.Empty;

    public string ER_SHORT_INFO_LIST
    {
        get { return _strERShortInfoList; }
        set { _strERShortInfoList = value; }
    }

    private string _strERNoOfBreakFastsList = string.Empty;

    public string ER_NO_OF_BREAKFASTS_LIST
    {
        get { return _strERNoOfBreakFastsList; }
        set { _strERNoOfBreakFastsList = value; }
    }

    private string _strERNoOfLunchesList = string.Empty;

    public string ER_NO_OF_LUNCHES_LIST
    {
        get { return _strERNoOfLunchesList; }
        set { _strERNoOfLunchesList = value; }
    }

    private string _strERNoOfDinnersList = string.Empty;

    public string ER_NO_OF_DINNERS_LIST
    {
        get { return _strERNoOfDinnersList; }
        set { _strERNoOfDinnersList = value; }
    }

    private string _strERDescriptionList = string.Empty;

    public string ER_DESCRIPTION_LIST
    {
        get { return _strERDescriptionList; }
        set { _strERDescriptionList = value; }
    }

    private string _strERParticipantsList = string.Empty;

    public string ER_PARTCIPANTS_LIST
    {
        get { return _strERPaperReceiptsList; }
        set { _strERPaperReceiptsList = value; }
    }

    private string _strERAdditionalInfoReasonList = string.Empty;

    public string ER_ADDITIONAL_INFO_REASONS_LIST
    {
        get { return _strERAdditionalInfoReasonList; }
        set { _strERAdditionalInfoReasonList = value; }
    }

    private string _strERNoOfEmployeesList = string.Empty;

    public string ER_NO_OF_EMPLOYEES_LIST
    {
        get { return _strERNoOfEmployeesList; }
        set { _strERNoOfEmployeesList = value; }
    }

    private string _strERNoOfPartnersList = string.Empty;

    public string ER_NO_OF_PARTNERS_LIST
    {
        get { return _strERNoOfPartnersList; }
        set { _strERNoOfPartnersList = value; }
    }

    private string _strERNoOfGuestsList = string.Empty;

    public string ER_NO_OF_GUESTS_LIST
    {
        get { return _strERNoOfGuestsList; }
        set { _strERNoOfGuestsList = value; }
    }

    private string _strERAdditionalInfoCityList = string.Empty;

    public string ER_ADDITIONAL_INFO_CITY_LIST
    {
        get { return _strERAdditionalInfoCityList; }
        set { _strERAdditionalInfoCityList = value; }
    }

    private string _strERAdditionalInfoCountryList = string.Empty;

    public string ER_ADDITIONAL_INFO_COUNTRY_LIST
    {
        get { return _strERAdditionalInfoCountryList; }
        set { _strERAdditionalInfoCountryList = value; }
    }

    private string _strErAdditionalInfoRegionList = string.Empty;

    public string ER_ADDITIONAL_INFO_REGION_LIST
    {
        get { return _strErAdditionalInfoRegionList; }
        set { _strErAdditionalInfoRegionList = value; }
    }

    private string _strERDocumentNoList = string.Empty;

    public string ER_DOCUMENT_NO_LIST
    {
        get { return _strERDocumentNoList; }
        set { _strERDocumentNoList = value; }
    }

    private string _strERTripTypeEnterpriseList = string.Empty;

    public string ER_TRIP_TYPE_ENTERPRISE_LIST
    {
        get { return _strERTripTypeEnterpriseList; }
        set { _strERTripTypeEnterpriseList = value; }
    }

    private string _strErProviderCategoryList = string.Empty;

    public string ER_PROVIDER_CATEGORY_LIST
    {
        get { return _strErProviderCategoryList; }
        set { _strErProviderCategoryList = value; }
    }

    private string _strERProviderList = string.Empty;

    public string ER_PROVIDER_LIST
    {
        get { return _strERProviderList; }
        set { _strERProviderList = value; }
    }

    private string _strCurrentStatus = string.Empty;

    public string CURRENT_STATUS
    {
        get { return _strCurrentStatus; }
        set { _strCurrentStatus = value; }
    }

    private bool _is_Sup_Appr_Req = false;

    public bool IS_SUPERVISR_APPROVAL_REQ
    {
        get { return _is_Sup_Appr_Req; }
        set { _is_Sup_Appr_Req = value; }
    }

    private bool _is_Hr_Appr_Req = false;

    public bool IS_HR_APPROVAL_REQ
    {
        get { return _is_Hr_Appr_Req; }
        set { _is_Hr_Appr_Req = value; }
    }

    private bool _is_PayrollAdmin_Appr_Req = false;

    public bool IS_PAYROLLADMIN_APPROVAL_REQ
    {
        get { return _is_PayrollAdmin_Appr_Req; }
        set { _is_PayrollAdmin_Appr_Req = value; }
    }    

    private string _strApproved_By = string.Empty;

    public string APPROVED_BY
    {
        get { return _strApproved_By; }
        set { _strApproved_By = value; }
    }

    private string _strEmpName = string.Empty;

    public string EMPLOYEE_NAME
    {
        get { return _strEmpName; }
        set { _strEmpName = value; }
    }    

    private string _strEmpMail = string.Empty;

    public string EMPLOYEE_MAIL
    {
        get { return _strEmpMail; }
        set { _strEmpMail = value; }
    }
}
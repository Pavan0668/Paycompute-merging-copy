using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class vehicle_requisitionbo
    {
        public bool IS_EMPLOYEE_CHECKD { get; set; }
        private string _remarks = string.Empty;
        private int? _FTPT_REQUEST_ID = 0;
        private Int32 _REQ_SEGMENT_ID = 0;
        private Int32 _iREQ_SEGMENT_ID = 0;
        private string _EMPLOYEE_NO = string.Empty;
private DateTime _Date_of_travel =Convert.ToDateTime("01/01/0001");
private DateTime _travel_end_date = Convert.ToDateTime("01/01/0001");
private DateTime _Duration_from=Convert.ToDateTime("01/01/0001");
private DateTime _Duration_to =Convert.ToDateTime("01/01/0001");
private string _Departure_from =string.Empty ;
private string _Destination_to=string.Empty ;
private string _Purpose_of_travel =string.Empty ;
private string _Carrying_any_materials =string.Empty ;
private string _Pickup_time=string.Empty ;
private string _Pickup_address =string.Empty ;
private string _Drop_time=string.Empty ;
private string _Drop_address=string.Empty ;
private string _Vehicle_type;
private string _Vehicle_category;
private string _Vehicle_name;
private string _Additional_services=string.Empty ;
private bool _status;
private string _current_status;
private string _STATUS_UPDATED_BY = string.Empty;
private string _sCreatedBy = string.Empty;
private DateTime _dCreatedOn;
private string _sModifiedBy = string.Empty;
private DateTime _dModifiedOn;
public int Vehicle_req_id { get; set; }
public string VehicleNameAssigned { get; set; }
public int REQ_SEGMENT_ID_FROM_TRAVEL_REQUEST { get; set; }

public int local_travel_req_id { get; set; }
public int Number_of_members { get; set; }
public string TRAVEL_TYPE { get; set; }
public string Name_of_members { get; set; }
public bool IsActive { get; set; }
public string VehicleId { get; set; }
public string VehicleCategoryId { get; set; }
public string VehicleTypeId { get; set; }
public string Emp_name { get; set; }
private DateTime _To_Date = Convert.ToDateTime("01/01/0001");
public string REASON_FOR_CANCEL { get; set; }

public int iREQ_SEGMENT_ID
{
    get
    {
        return _iREQ_SEGMENT_ID;
    }
    set
    {
        _iREQ_SEGMENT_ID = value;
    }
}
public string remarks
{
    get
    {
        return _remarks;
    }
    set
    {
        _remarks = value;
    }
}
public string STATUS_UPDATED_BY
{
    get
    {
        return _STATUS_UPDATED_BY;
    }
    set
    {
        _STATUS_UPDATED_BY = value;
    }
}
public string current_status
{
    get
    {
        return _current_status;
    }
    set
    {
        _current_status = value;
    }
}
public bool status
{
    get
    {
        return _status;
    }
    set
    {
        _status = value;
    }
}
public Int32 REQ_SEGMENT_ID
{
    get
    {
        return _REQ_SEGMENT_ID;
    }
    set
    {
        _REQ_SEGMENT_ID = value;
    }
}
public int? REQUEST_ID
{
    get
    {
        return _FTPT_REQUEST_ID;
    }
    set
    {
        _FTPT_REQUEST_ID = value;
    }
}

public string EMPLOYEE_NO
{
    get
    {
        return _EMPLOYEE_NO;
    }
    set
    {
        _EMPLOYEE_NO = value;
    }
}
public DateTime Date_of_travel
{
    get
    {
        return _Date_of_travel;
    }
    set
    {
        _Date_of_travel = value;
    }
}

public DateTime trave_end_time
{
    get
    {
        return _travel_end_date;
    }
    set
    {
        _travel_end_date = value;
    }
}
public DateTime Duration_from
{
    get
    {
        return _Duration_from;
    }
    set
    {
        _Duration_from = value;
    }
}
public DateTime Duration_to
{
    get
    {
        return _Duration_to;
    }
    set
    {
        _Duration_to = value;
    }
}
public string Departure_from
{
    get
    {
        return _Departure_from;
    }
    set
    {
        _Departure_from = value;
    }
}
public string Destination_to
{
    get
    {
        return _Destination_to;
    }
    set
    {
        _Destination_to = value;
    }
}
public string Purpose_of_travel
{
    get
    {
        return _Purpose_of_travel;
    }
    set
    {
        _Purpose_of_travel = value;
    }
}
public string Carrying_any_materials
{
    get
    {
        return _Carrying_any_materials;
    }
    set
    {
        _Carrying_any_materials = value;
    }
}
public string Pickup_time
{
    get
    {
        return _Pickup_time;
    }
    set
    {
        _Pickup_time = value;
    }
}
public string Pickup_address
{
    get
    {
        return _Pickup_address;
    }
    set
    {
        _Pickup_address = value;
    }
}
public string Drop_time
{
    get
    {
        return _Drop_time;
    }
    set
    {
        _Drop_time = value;
    }
}
public string Drop_address
{
    get
    {
        return _Drop_address;
    }
    set
    {
        _Drop_address = value;
    }
}
public string Vehicle_type
{
    get
    {
        return _Vehicle_type;
    }
    set
    {
        _Vehicle_type = value;
    }
}
public string Vehicle_type_name
{
    get;
    set;

}
public string Vehicle_category
{
    get
    {
        return _Vehicle_category;
    }
    set
    {
        _Vehicle_category = value;
    }
}
public string Vehicle_name
{
    get
    {
        return _Vehicle_name;
    }
    set
    {
        _Vehicle_name = value;
    }
}
public string Additional_services
{
    get
    {
        return _Additional_services;
    }
    set
    {
        _Additional_services = value;
    }
}

public string CRAETEDBY
{
    get { return _sCreatedBy; }
    set { _sCreatedBy = value; }
}
public DateTime CREATEDON
{
    get { return _dCreatedOn; }
    set { _dCreatedOn = value; }
}
public string MODIFIEDBY
{
    get { return _sModifiedBy; }
    set { _sModifiedBy = value; }
}
public DateTime MODIFIEDON
{
    get { return _dModifiedOn; }
    set { _dModifiedOn = value; }
}

public DateTime To_Date
{
    get
    {
        return _To_Date;
    }
    set
    {
        _To_Date = value;
    }
}


    }
}
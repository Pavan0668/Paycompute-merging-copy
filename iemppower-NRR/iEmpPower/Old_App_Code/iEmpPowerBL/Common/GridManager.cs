using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
namespace iEmpPower.Old_App_Code.iEmpPowerBL.Common
{
    public class GridManager
    {
        //Sorting travel requisition search grid
        public static List<requisitionbo> SortTravelRequestGridView(string sortExpression, bool objSortOrder, List<requisitionbo> objRequisitionBoList)
        {
            switch (sortExpression)
            {
                case "FTPT_REQUEST_ID":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {
                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.FTPT_REQUEST_ID.ToString(),objSecondBo.FTPT_REQUEST_ID.ToString(),StringComparison.CurrentCultureIgnoreCase);
                            });

                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {
                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                               return string.Compare(objSecondBo.FTPT_REQUEST_ID.ToString() , objFirstBo.FTPT_REQUEST_ID.ToString() , StringComparison.CurrentCultureIgnoreCase);
                            });

                        }
                    }
                    break;
                case "REQ_SEGMENT_ID":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                               return string.Compare(objFirstBo.REQ_SEGMENT_ID.ToString(), objSecondBo.REQ_SEGMENT_ID.ToString() , StringComparison.CurrentCultureIgnoreCase);
                            });

                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.REQ_SEGMENT_ID.ToString(), objFirstBo.REQ_SEGMENT_ID.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    break;
                case "TRAVEL_DATE":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.TRAVEL_DATE.ToString(), objSecondBo.TRAVEL_DATE.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.TRAVEL_DATE.ToString(), objFirstBo.TRAVEL_DATE.ToString(), StringComparison.CurrentCultureIgnoreCase);

                            });
                        }
                    }
                    break;
                case "MODE_OF_TRANSPOPRT_FZTXT":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort( delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                               return string.Compare(objFirstBo.MODE_OF_TRANSPOPRT_FZTXT , objSecondBo.MODE_OF_TRANSPOPRT_FZTXT , StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                               return string.Compare(objSecondBo.MODE_OF_TRANSPOPRT_FZTXT, objFirstBo.MODE_OF_TRANSPOPRT_FZTXT, StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    break;
                case "MEDIA_OF_CATEGORY_TEXT25":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                               return string.Compare(objFirstBo.MEDIA_OF_CATEGORY_TEXT25 , objSecondBo.MEDIA_OF_CATEGORY_TEXT25, StringComparison.CurrentCultureIgnoreCase);

                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.MEDIA_OF_CATEGORY_TEXT25 , objFirstBo.MEDIA_OF_CATEGORY_TEXT25, StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    break;
                case "REGION_TEXT25_FROM":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.REGION_TEXT25_FROM , objSecondBo.REGION_TEXT25_FROM , StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                return objSecondBo.REGION_TEXT25_FROM.CompareTo(objFirstBo.REGION_TEXT25_FROM);
                            });
                        }
                    }
                    break;
                case "REGION_TEXT25_TO":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                
                                return string.Compare(objFirstBo.REGION_TEXT25_TO , objSecondBo.REGION_TEXT25_TO , StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                return objSecondBo.REGION_TEXT25_TO.CompareTo(objFirstBo.REGION_TEXT25_TO );
                                
                            });
                        }
                    }
                    break;

                case "EMPLOYEE_NO":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {

                                return string.Compare(objFirstBo.EMPLOYEE_NO, objSecondBo.EMPLOYEE_NO, StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                return objSecondBo.EMPLOYEE_NO.CompareTo(objFirstBo.EMPLOYEE_NO);

                            });
                        }
                    }
                    break;

                case "REASON_FOR_CANCEL":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {

                                return string.Compare(objFirstBo.REASON_FOR_CANCEL, objSecondBo.REASON_FOR_CANCEL, StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(requisitionbo objFirstBo, requisitionbo objSecondBo)
                            {
                                return objSecondBo.REASON_FOR_CANCEL.CompareTo(objFirstBo.REASON_FOR_CANCEL);

                            });
                        }
                    }
                    break;
            }
            return objRequisitionBoList;
        }

        //Sorting local accommodation requistion  search grid
        public static List<accomodation_requisitionbo> SortAccommodationRequestGridView(string sortExpression, bool objSortOrder, List<accomodation_requisitionbo> objRequisitionBoList)
        {
            switch (sortExpression)
            {
                case "local_acc_req_id":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {
                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.local_acc_req_id.ToString(), objSecondBo.local_acc_req_id.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });

                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {
                            objRequisitionBoList.Sort(
                            delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.local_acc_req_id.ToString(), objFirstBo.local_acc_req_id.ToString(),StringComparison.CurrentCultureIgnoreCase);
                            }
                            );
                        }
                    }
                    break;
                case "Check_in_date":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.Check_in_date.ToString(), objSecondBo.Check_in_date.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });

                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                               return string.Compare(objSecondBo.Check_in_date.ToString(), objFirstBo.Check_in_date.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    break;
                case "Check_out_date":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                               return string.Compare(objFirstBo.Check_out_date.ToString(), objSecondBo.Check_out_date.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.Check_out_date.ToString(), objFirstBo.Check_out_date.ToString(), StringComparison.CurrentCultureIgnoreCase);

                            });
                        }
                    }
                    break;
                case "HotelPlaceCity":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                                  return string.Compare(objFirstBo.HotelPlaceCity, objSecondBo.HotelPlaceCity, StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                                    return string.Compare(objSecondBo.HotelPlaceCity, objFirstBo.HotelPlaceCity, StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    break;
                case "Number_of_members":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                                  return string.Compare(Convert.ToString(objFirstBo.Number_of_members), Convert.ToString(objSecondBo.Number_of_members), StringComparison.CurrentCultureIgnoreCase);

                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                                return string.Compare(Convert.ToString(objFirstBo.Number_of_members), Convert.ToString(objSecondBo.Number_of_members), StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    break;
                case "current_status":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.current_status , objSecondBo.current_status , StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(accomodation_requisitionbo objFirstBo, accomodation_requisitionbo objSecondBo)
                            {
                                return objSecondBo.current_status.CompareTo(objFirstBo.current_status);
                              
                            });
                        }
                    }
                    break;
          
            }
            return objRequisitionBoList;
        }

        //Sorting local travel requistion search grid
        public static List<vehicle_requisitionbo> SortLocalTravelRequestGridView(string sortExpression, bool objSortOrder, List<vehicle_requisitionbo> objRequisitionBoList)
        {
            switch (sortExpression)
            {
                case "local_travel_req_id":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {
                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.local_travel_req_id.ToString(), objSecondBo.local_travel_req_id.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });

                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {
                            objRequisitionBoList.Sort(
                            delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.local_travel_req_id.ToString(), objFirstBo.local_travel_req_id.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            }
                            );
                        }
                    }
                    break;

                case "Date_of_travel":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {
                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.Date_of_travel.ToString(), objSecondBo.Date_of_travel.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });

                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {
                            objRequisitionBoList.Sort(
                            delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.Date_of_travel.ToString(), objFirstBo.Date_of_travel.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            }
                            );
                        }
                    }
                    break;

                case "To_Date":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {
                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.To_Date.ToString(), objSecondBo.To_Date.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });

                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {
                            objRequisitionBoList.Sort(
                            delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.To_Date.ToString(), objFirstBo.To_Date.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            }
                            );
                        }
                    }
                    break;
                case "Departure_from":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.Departure_from.ToString(), objSecondBo.Departure_from.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });

                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.Departure_from.ToString(), objFirstBo.Departure_from.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    break;
                case "Destination_to":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.Destination_to.ToString(), objSecondBo.Destination_to.ToString(), StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.Destination_to.ToString(), objFirstBo.Destination_to.ToString(), StringComparison.CurrentCultureIgnoreCase);

                            });
                        }
                    }
                    break;
                case "Pickup_time":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.Pickup_time, objSecondBo.Pickup_time, StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objSecondBo.Pickup_time, objFirstBo.Pickup_time, StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    break;
                case "Drop_time":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(Convert.ToString(objFirstBo.Drop_time), Convert.ToString(objSecondBo.Drop_time), StringComparison.CurrentCultureIgnoreCase);

                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(Convert.ToString(objFirstBo.Drop_time), Convert.ToString(objSecondBo.Drop_time), StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    break;
                case "current_status":
                    if (objSortOrder)
                    {

                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return string.Compare(objFirstBo.current_status, objSecondBo.current_status, StringComparison.CurrentCultureIgnoreCase);
                            });
                        }
                    }
                    else
                    {
                        if (objRequisitionBoList != null)
                        {

                            objRequisitionBoList.Sort(delegate(vehicle_requisitionbo objFirstBo, vehicle_requisitionbo objSecondBo)
                            {
                                return objSecondBo.current_status.CompareTo(objFirstBo.current_status);

                            });
                        }
                    }
                    break;

            }
            return objRequisitionBoList;
        }

    }
}
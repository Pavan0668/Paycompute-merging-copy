using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Local_requisition;

namespace iEmpPower.UI.Local_requisition
{
    public partial class TicketBook_accvehi_Outst : System.Web.UI.Page
    {
        protected MembershipUser memUser;
        int AccomId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                lblMessage.Text = "";

                Loadmtport();
                funLoadHotelCatDropdowns();//Intializing drop down method should be place at first.

                //To check the status and to disable controls
                LoadProposalAccommoDetails();                            
            } 
        }
        
        protected void tcDefalut_ActiveTabChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            ClearcontrolAccomm();
            ClearcontrolVehi();
            clearcontrol();

            if (tcDefalut.ActiveTab == tabAccommodation)
            {
                lblMessage.Text = "";
                AccomodationRequisitionGridView.SelectedIndex = -1;
                LoadProposalAccommoDetails();
            }
            else
            {
                lblMessage.Text = "";
                VehicleRequisitionGridView.SelectedIndex = -1;
                LoadProposalVehiDetails();
            }
        }

        public void LoadProposalAccommoDetails()
        {
            try
            {
                //Bind domestic gridview
                AccomodationRequisitionGridView.DataSource = null;
                AccomodationRequisitionGridView.DataBind();
                //Bind domestic accommodation gridview
                List<accomodation_requisitionbo> accomodation_requisitionboList = new List<accomodation_requisitionbo>();
                accomodation_requisitionbl accomodation_requisitionblObj = new accomodation_requisitionbl();

                accomodation_requisitionboList = accomodation_requisitionblObj.Get_Accommodation_Separate_Details(0, 0, "", "8");

                if (accomodation_requisitionboList.Count != 0)
                {
                    AccomodationRequisitionGridView.DataSource = accomodation_requisitionboList;
                    AccomodationRequisitionGridView.DataBind();
                    AccomodationRequisitionGridView.Visible = true;
                    upPnlAcc.Visible = true;
                }
                else
                {
                    AccomodationRequisitionGridView.DataSource = null;
                    AccomodationRequisitionGridView.DataBind();
                    AccomodationRequisitionGridView.Visible = false;
                    upPnlAcc.Visible = false;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "No Data To Book Ticket";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void LoadProposalVehiDetails()
        {
            try
            {
                //Bind domestic gridview
                VehicleRequisitionGridView.DataSource = null;
                VehicleRequisitionGridView.DataBind();
                //Bind domestic accommodation gridview
                List<vehicle_requisitionbo> vehicle_requisitionboboList = new List<vehicle_requisitionbo>();
                vehicle_requisitionbl vehicle_requisitionblObj = new vehicle_requisitionbl();

                vehicle_requisitionboboList = vehicle_requisitionblObj.Get_Vehicle_Separate_Details(0, 0, "", "8");

                if (vehicle_requisitionboboList.Count != 0)
                {
                    VehicleRequisitionGridView.DataSource = vehicle_requisitionboboList;
                    VehicleRequisitionGridView.DataBind();
                    VehicleRequisitionGridView.Visible = true;
                    upPnlVehi.Visible = true;
                }
                else
                {
                    VehicleRequisitionGridView.DataSource = null;
                    VehicleRequisitionGridView.DataBind();
                    VehicleRequisitionGridView.Visible = false;
                    upPnlVehi.Visible = false;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "No Data To Book Ticket";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void AccomodationRequisitionGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            try
            {
                //   LoadDropdowns();
                DomesticTravelRequiLinkButton();
                funLoadAccSelectedGrid();
                pnlAccomTicketbooking.Visible = true;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                lblMessage.Text = error;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void DomesticTravelRequiLinkButton()
        {

            GridViewRow row = AccomodationRequisitionGridView.SelectedRow;
            

            string EmploID = AccomodationRequisitionGridView.Rows[row.RowIndex].Cells[3].Text;
            

            string RequisitionID = AccomodationRequisitionGridView.DataKeys[row.RowIndex].Value.ToString(); ////AccomodationRequisitionGridView.Rows[row.RowIndex].Cells[0].Text;
            Session["RequisitionSelectedID"] = RequisitionID;
            BindControlsForEdit(Convert.ToInt32(RequisitionID));
           
        }


        protected void DomesticTravelRequiVehLinkButton()
        {

            GridViewRow row = VehicleRequisitionGridView.SelectedRow;

            string EmploID = VehicleRequisitionGridView.Rows[row.RowIndex].Cells[3].Text;


            string RequisitionID = VehicleRequisitionGridView.DataKeys[row.RowIndex].Value.ToString(); ////AccomodationRequisitionGridView.Rows[row.RowIndex].Cells[0].Text;
            Session["RequisitionSelectedID"] = RequisitionID;
            BindControlsForEdit(Convert.ToInt32(RequisitionID));
           
        }


        //protected void DomesticTravelRequiLinkButton_Click(object sender, CommandEventArgs e)
        //{
        //    LinkButton lb = (LinkButton)sender;
        //    GridViewRow row = (GridViewRow)lb.NamingContainer;
        //    //GridView grdAcc = (GridView)TicketGridView.Rows[row.RowIndex].FindControl("AccomodationRequisitionGridViewDomestic");
        //    //GridView grdVehi = (GridView)TicketGridView.Rows[row.RowIndex].FindControl("VehicleRequisitionGridViewDomestic");

        //    //if ((grdAcc.Rows.Count == 0) || (grdVehi.Rows.Count == 0))
        //    //{
        //    string EmploID = AccomodationRequisitionGridView.Rows[row.RowIndex].Cells[3].Text;
        //    ////LoadEmployeeVisaPassportDetails(EmploID);

        //    string RequisitionID = AccomodationRequisitionGridView.DataKeys[row.RowIndex].Value.ToString(); ////AccomodationRequisitionGridView.Rows[row.RowIndex].Cells[0].Text;
        //    Session["RequisitionSelectedID"] = RequisitionID;
        //    BindControlsForEdit(Convert.ToInt32(RequisitionID));
        //    //}
        //    //else
        //    //{
        //    //    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
        //    //    lblMessageBoard.Text = "Please Book the segments(Accommodation/Vehicle).";
        //    //}
        //}


        //protected void DomesticTravelRequiVehLinkButton_Click(object sender, CommandEventArgs e)
        //{
        //    LinkButton lb = (LinkButton)sender;
        //    GridViewRow row = (GridViewRow)lb.NamingContainer;
        //    //GridView grdAcc = (GridView)TicketGridView.Rows[row.RowIndex].FindControl("AccomodationRequisitionGridViewDomestic");
        //    //GridView grdVehi = (GridView)TicketGridView.Rows[row.RowIndex].FindControl("VehicleRequisitionGridViewDomestic");

        //    //if ((grdAcc.Rows.Count == 0) || (grdVehi.Rows.Count == 0))
        //    //{
        //    string EmploID = AccomodationRequisitionGridView.Rows[row.RowIndex].Cells[3].Text;
        //    ////LoadEmployeeVisaPassportDetails(EmploID);

        //    string RequisitionID = AccomodationRequisitionGridView.DataKeys[row.RowIndex].Value.ToString(); ////AccomodationRequisitionGridView.Rows[row.RowIndex].Cells[0].Text;
        //    Session["RequisitionSelectedID"] = RequisitionID;
        //    BindControlsForEdit(Convert.ToInt32(RequisitionID));
        //    //}
        //    //else
        //    //{
        //    //    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
        //    //    lblMessageBoard.Text = "Please Book the segments(Accommodation/Vehicle).";
        //    //}
        //}

        protected void BindControlsForEdit(int RequisitionSegmentId)
        {
            travelrequestbl travelrequestblObj = new travelrequestbl();
            List<ticketbookingbo> ticketlist = new List<ticketbookingbo>();
            ticketlist = (List<ticketbookingbo>)Session["DomesticSegmentList"];
            if (ticketlist == null)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Unknown error occured.";
            }
            else
            {
                if (ticketlist.Count != 0)
                {
                    ticketbookingbo objbo = ticketlist.Find(delegate(ticketbookingbo obj)
                    {
                        return obj.REQ_SEGMENT_ID == RequisitionSegmentId;
                    });
                    Session["Emp_name"] = objbo.EMPLOYEE_Name;
                    Session["req_id"] = objbo.REQUISITION_ID;
                    Session["req_seg_id"] = objbo.REQUISITION_SEGMENT_ID;
                    Session["pro_id"] = objbo.PROPOSAL_ID;
                    Session["pro_seg_id"] = objbo.PRO_SEGMENT_ID;
                    LabelEmpName.Text = objbo.EMPLOYEE_Name;
                    LabelDesign.Text = objbo.Designation;
                    //LabelVehicleCategory.Text = objbo.Vehicle_category;
                    //LabelVehicleName.Text = objbo.Vehicle_name;
                    //LabelVehicleType.Text = objbo.Vehicle_type;
                    LabelPANCard.Text = objbo.DEPARTURE_FROM;
                    LabelVoterID.Text = objbo.DEPARTURE_TIME;
                    LabelTravelDate.Text = Convert.ToString(objbo.TRAVEL_DATE);
                    // LabelDeparture.Text = objbo.DEPARTURE_FROM;

                    ticketbookingbl objj = new ticketbookingbl();

                    //foreach (var vRow2 in objj.Get_TravelRemarks_ForCancelation(objbo.REQUISITION_ID, objbo.REQUISITION_SEGMENT_ID))
                    //{
                    ////  lblReasonForCancel.Text = objj.Get_TravelRemarks_ForCancelation(objbo.REQUISITION_ID, objbo.REQUISITION_SEGMENT_ID);
                    //}

                    Local_travel_requisitionDataContext objtd = new Local_travel_requisitionDataContext();
                    foreach (var vRow4 in objtd.sp_load_FromTo_name_code(objbo.DEPARTURE_FROM))
                    {
                        LabelPANCard.Text = vRow4.TEXT25;

                    } foreach (var vRow5 in objtd.sp_load_FromTo_name_code(objbo.DEPARTURE_TIME))
                    {
                        LabelVoterID.Text = vRow5.TEXT25;
                    }

                    foreach (var vRow1 in objtd.sp_load_vehiceType_code(objbo.Vehicle_type))
                    {
                        LabelVehicleType.Text = vRow1.FZTXT;
                    }
                    foreach (var vRow2 in objtd.sp_load_VehicleClass_code(objbo.Vehicle_category))
                    {
                        LabelVehicleCategory.Text = vRow2.TEXT25;
                    }
                    if (!(objbo.Vehicle_name == "" || objbo.Vehicle_name == string.Empty || objbo.Vehicle_name == "&nbsp;"))
                    {
                        foreach (var vRow3 in objtd.sp_load_vehicle_names_code(Convert.ToInt32(objbo.Vehicle_name)))
                        {
                            LabelVehicleName.Text = vRow3.ZZVEHNAM;
                        }
                    }

                    //// bIsSave = true;

                    ////funGet_GroupRequest_Traveller(); //group requisition
                }
            }
        }



        protected void AccomodationRequisitionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.AccomodationRequisitionGridView, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                lblMessage.Text = error;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void funLoadAccSelectedGrid()
        {
            lblMessage.Text = "";
            try
            {
                GridViewRow grdRow = AccomodationRequisitionGridView.SelectedRow;
                int Out_Acc_REQUISITION_ID = Convert.ToInt32(grdRow.Cells[0].Text.ToString().Trim());
                Session.Add("Out_Acc_REQUISITION_ID", Out_Acc_REQUISITION_ID);

                string strTravelDate = grdRow.Cells[4].Text.ToString().Trim();
                string strchkInDate = grdRow.Cells[5].Text.ToString().Trim();
                string strchkOutDate = grdRow.Cells[6].Text.ToString().Trim();
                string strArrivalTime = grdRow.Cells[7].Text.ToString().Trim();
                string strDepTime = grdRow.Cells[8].Text.ToString().Trim();
                string strAdditServ = grdRow.Cells[9].Text.ToString().Trim();
                string strRemarks = grdRow.Cells[17].Text.ToString().Trim();
                string strHotelCat = grdRow.Cells[18].Text.ToString();
                string strHotelName = grdRow.Cells[19].Text.ToString();
                string strRoomCat = grdRow.Cells[20].Text.ToString();

                TextTripStartDat.Text = strTravelDate;
                TextStartDate.Text = strchkInDate;
                TextCheckout_date.Text = strchkOutDate;
                txtArrival_time.Text = strArrivalTime;
                TextDeparture_time.Text = strDepTime;
                TextAdditional_services.Text = strAdditServ;
                RemarksTextBox.Text = strRemarks;

                DropDownHotel_category.SelectedValue = strHotelCat;
                DropDownHotel_name.SelectedValue = strHotelName;
                DropDownRoom_category.SelectedValue = strRoomCat;

                //If status  is new then only show update button modify requisiton other wise only view.
                if (grdRow.RowIndex > -1)
                {
                    int iSelectedRow = Convert.ToInt32(grdRow.RowIndex, CultureInfo.CurrentCulture);
                    int iPageRelatedToSelectedRow = Convert.ToInt32(AccomodationRequisitionGridView.PageIndex, CultureInfo.CurrentCulture);
                    ViewState.Add("SelectedRow", iSelectedRow);
                    ViewState.Add("PageRelatedToSelectedRow", iPageRelatedToSelectedRow);

                    //TextTripStartDat.Enabled = false;
                    //TextStartDate.Enabled = false;
                    //TextCheckout_date.Enabled = false;
                    //BirthDateImageButton.Enabled = false;
                    //BirthDateImageButton1.Enabled = false;
                    //BirthDateImageButtonnew.Enabled = false;
                    RemarksTextBox.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                lblMessage.Text = error;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void VehicleRequisitionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.VehicleRequisitionGridView, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void VehicleRequisitionGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            try
            {
                DomesticTravelRequiVehLinkButton();
                funLoadVehiSelectedGrid();
                pnlVehiTicketbooking.Visible = true;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                lblMessage.Text = error;
            }
        }

        public void funLoadVehiSelectedGrid()
        {
            lblMessage.Text = "";
            try
            {
                GridViewRow grdRow = VehicleRequisitionGridView.SelectedRow;
                int Out_Vehi_REQUISITION_ID = Convert.ToInt32(grdRow.Cells[0].Text.ToString().Trim());
                Session.Add("Out_Vehi_REQUISITION_ID", Out_Vehi_REQUISITION_ID);

                string strTextTripStartDat = grdRow.Cells[4].Text.ToString().Trim();
                string strTextDurationfrom = grdRow.Cells[5].Text.ToString().Trim();
                string strTextDurationto = grdRow.Cells[6].Text.ToString().Trim();
                string strTextDeparturefrom = grdRow.Cells[7].Text.ToString().Trim();
                string strTextDestinationto = grdRow.Cells[8].Text.ToString().Trim();
                string strTextPurposeoftravel = grdRow.Cells[9].Text.ToString().Trim();
                string strTextCarryinganymaterials = grdRow.Cells[10].Text.ToString().Trim();
                string strTextPickuptime = grdRow.Cells[11].Text.ToString().Trim();
                string strTextPickupaddress = grdRow.Cells[12].Text.ToString().Trim();
                string strTextDroptime = grdRow.Cells[13].Text.ToString().Trim();
                string strTextDropaddress = grdRow.Cells[14].Text.ToString().Trim();
                string strDropDownVehicletype = grdRow.Cells[15].Text.ToString().Trim();
                string strDropDownVehiclecategory = grdRow.Cells[16].Text.ToString().Trim();
                string strDropDownVehiclename = grdRow.Cells[17].Text.ToString().Trim();
                string strTextAdditionalservices = grdRow.Cells[18].Text.ToString().Trim();
                string strTextRemarks = grdRow.Cells[19].Text.ToString().Trim();


                // CalenderExtender.SelectedDate =Convert.ToDateTime(strTextTripStartDat);
                TextTripStartDatV.Text = strTextTripStartDat;
                TextDurationfrom.Text = strTextDurationfrom;
                // CalendarExtender1
                TextDurationto.Text = strTextDurationto;
                //  CalendarExtender2
                TextDeparturefrom.Text = strTextDeparturefrom;
                TextDestinationto.Text = strTextDestinationto;
                TextPurposeoftravel.Text = strTextPurposeoftravel;
                TextCarryinganymaterials.Text = strTextCarryinganymaterials;
                TextPickuptime.Text = strTextPickuptime;
                TextPickupaddress.Text = strTextPickupaddress;
                TextDroptime.Text = strTextDroptime;
                TextDropaddress.Text = strTextDropaddress;

                DropDownVehicletype.Text = strDropDownVehicletype;
                loadmcate(strDropDownVehicletype);

                DropDownVehiclecategory.Text = strDropDownVehiclecategory;
                loadvehiclename(DropDownVehicletype.SelectedValue);

                DropDownVehiclename.Text = strDropDownVehiclename;

                TextAdditionalservices.Text = strTextAdditionalservices;
                TextRemarks.Text = strTextRemarks;

                //If status  is new then only show update button modify requisiton other wise only view.
                if (grdRow.RowIndex > -1)
                {
                    int iSelectedRow = Convert.ToInt32(grdRow.RowIndex, CultureInfo.CurrentCulture);
                    int iPageRelatedToSelectedRow = Convert.ToInt32(VehicleRequisitionGridView.PageIndex, CultureInfo.CurrentCulture);
                    // TextTripStartDat.Enabled = false;
                    //TextDurationfrom.Enabled = false;
                    //TextDurationto.Enabled = false;
                    TextRemarks.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                lblMessage.Text = error;
            }
        }

        protected void Loadmtport()
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();

            var vOnlyFirstCal = (from col in objtravelrequestbl.GetModeofTransport()
                                 where (col.MODE_OF_TRANSPOPRT_KZPMF == "X")
                                 select col);

            DropDownVehicletype.DataSource = vOnlyFirstCal;
            DropDownVehicletype.DataTextField = "MODE_OF_TRANSPOPRT_FZTXT";
            DropDownVehicletype.DataValueField = "MODE_OF_TRANSPOPRT_KZPMF";
            DropDownVehicletype.DataBind();
            ListItem drpDefaultItem1 = new ListItem("Select", "0", true);
            DropDownVehicletype.Items.Add(drpDefaultItem1);
            DropDownVehicletype.SelectedValue = "0";
        }

        protected void loadmcate(string MediumOfTransport)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            DropDownVehiclecategory.DataSource = objtravelrequestbl.GetModeofCategory(MediumOfTransport);
            DropDownVehiclecategory.DataTextField = "MEDIA_OF_CATEGORY_TEXT25";
            DropDownVehiclecategory.DataValueField = "MEDIA_OF_CATEGORY_PKWKL";
            DropDownVehiclecategory.DataBind();
            ListItem drpDefaultItem2 = new ListItem("Select medium of category", "0", true);
            DropDownVehiclecategory.Items.Add(drpDefaultItem2);
            DropDownVehiclecategory.SelectedValue = "0";
        }

        protected void loadvehiclename(string MediumOfCategory)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            DropDownVehiclename.DataSource = objtravelrequestbl.GetVehicleName(MediumOfCategory);
            DropDownVehiclename.DataTextField = "VEHICLE_NAME_ZZVEHNAM";
            DropDownVehiclename.DataValueField = "VEHICLE_NAME_VHNUM";
            DropDownVehiclename.DataBind();
            ListItem drpDefaultItem9 = new ListItem("Select vehicle", "0", true);
            DropDownVehiclename.Items.Add(drpDefaultItem9);
            DropDownVehiclename.SelectedValue = "0";
        }

        public void funLoadHotelCatDropdowns()
        {

            accomodation_requisitionbl AccRequisitionBl = new accomodation_requisitionbl();
            accomodation_requisitionbo AccRequisitionBo = new accomodation_requisitionbo();


            //loading the Hotel Place drop down.
            DropDownHotelPlaceCity.Items.Clear();
            travelrequestbl objtravelrequestbl1 = new travelrequestbl();
            DropDownHotelPlaceCity.DataSource = objtravelrequestbl1.GetRegionName();
            DropDownHotelPlaceCity.DataValueField = "REGION_RGION_FROM";
            DropDownHotelPlaceCity.DataTextField = "REGION_TEXT25_FROM";
            DropDownHotelPlaceCity.DataBind();
            ListItem HotelPalceItem = new ListItem("Select", "0");
            DropDownHotelPlaceCity.Items.Add(HotelPalceItem);
            DropDownHotelPlaceCity.SelectedValue = "0";


            //accomodation_requisitionbl AccRequisitionBl = new accomodation_requisitionbl();
            //accomodation_requisitionbo AccRequisitionBo = new accomodation_requisitionbo();
            //DropDownHotel_category.Items.Clear();
            ////loading the HotelCategory drop down.
            //DropDownHotel_category.DataSource = AccRequisitionBl.GetHotelCategory();
            //DropDownHotel_category.DataTextField = "HOTEL_CATEGORY";
            //DropDownHotel_category.DataValueField = "HOTEL_CAT_CODE";
            //DropDownHotel_category.DataBind();
            //ListItem Item = new ListItem("Select", "0", true);
            //DropDownHotel_category.Items.Add(Item);
            //DropDownHotel_category.SelectedValue = "0";
        }

        //public void funLoadHotelNames()
        //{
        //    accomodation_requisitionbl AccRequisitionBl = new accomodation_requisitionbl();
        //    accomodation_requisitionbo AccRequisitionBo = new accomodation_requisitionbo();
        //    //loading the Hotel name drop down.
        //    DropDownHotel_name.Items.Clear();
        //    DropDownHotel_name.DataSource = AccRequisitionBl.GetHotelNames();
        //    DropDownHotel_name.DataTextField = "HOTEL_NAME";
        //    DropDownHotel_name.DataValueField = "HOTEL_CODE";
        //    DropDownHotel_name.DataBind();
        //    ListItem HotelNameItem = new ListItem("Select", "0", true);
        //    DropDownHotel_name.Items.Add(HotelNameItem);
        //    DropDownHotel_name.SelectedValue = "0";
        //}

        //public void funLoadRoomNames()
        //{
        //    accomodation_requisitionbl AccRequisitionBl = new accomodation_requisitionbl();
        //    accomodation_requisitionbo AccRequisitionBo = new accomodation_requisitionbo();
        //    //loading the Room category drop down.
        //    DropDownRoom_category.Items.Clear();
        //    DropDownRoom_category.DataSource = AccRequisitionBl.GetRoomNames();
        //    DropDownRoom_category.DataTextField = "RoomCategoryName";
        //    DropDownRoom_category.DataValueField = "ROOM_CODE";
        //    DropDownRoom_category.DataBind();
        //    ListItem RoomNameItem = new ListItem("Select", "0", true);
        //    DropDownRoom_category.Items.Add(RoomNameItem);
        //    DropDownRoom_category.SelectedValue = "0";
        //}
        
        protected void domesticDropDownMCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownVehiclename.Items.Clear();
            if (DropDownVehicletype.SelectedValue != "")
            {
                loadvehiclename(DropDownVehicletype.SelectedValue);
                DropDownVehiclename.Enabled = true;
            }
            else
            {
                DropDownVehiclename.Enabled = false;
            }
        }

        protected void domesticDropDownMTPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadmcate(DropDownVehicletype.SelectedValue);
        }

        public void ClearcontrolAccomm()
        {
            TextRemarks.Text = "";
            TextTripStartDat.Text = "";
            TextTripStartDat.Enabled = true;
            TextDurationfrom.Text = "";
            TextDurationfrom.Enabled = true;
            TextDurationto.Text = "";
            TextDurationto.Enabled = true;
            TextPurposeoftravel.Text = "";
            TextCarryinganymaterials.Text = "";
            TextPickuptime.Text = "";
            TextPickupaddress.Text = "";
            TextDroptime.Text = "";
            TextAdditionalservices.Text = "";
            TextDeparturefrom.Text = "";
            TextDropaddress.Text = "";
            DropDownVehicletype.SelectedIndex = 0;
            DropDownVehiclename.SelectedIndex = -1;
            DropDownVehiclecategory.SelectedIndex = -1;
            TextTripStartDat.Enabled = true;
            CalenderExtender.Enabled = true;
        }

        public void ClearcontrolVehi()
        {
            TextTripStartDatV.Text = "";
            TextTripStartDatV.Enabled = true;
            TextStartDate.Text = "";
            TextStartDate.Enabled = true;
            TextCheckout_date.Text = "";
            TextCheckout_date.Enabled = true;
            txtArrival_time.Text = "";
            TextDeparture_time.Text = "";
            TextAdditional_services.Text = "";
            RemarksTextBox.Text = "";
            DropDownHotel_category.SelectedIndex = 0;
            DropDownRoom_category.SelectedIndex = -1;
            DropDownHotel_name.SelectedIndex = -1;
        }


        //=======================ticket boooking=====================
        
        protected void btnAccTicketBook_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Session["RequisitionSelectedID"] != null)
                //{
                ticketbookingAccombo objbo = new ticketbookingAccombo();
                ticketbookingAccomcollectionbo objcollectionbo = new ticketbookingAccomcollectionbo();
                ticketbookingbl objbl = new ticketbookingbl();
                objbo.EMPLOYEE_NO = "";
                objbo.Accommadation_req_id =(int)Session["Out_Acc_REQUISITION_ID"];  
                objbo.REQUISITION_ID = Convert.ToInt32("0");
                objbo.REQ_SEGMENT_ID = Convert.ToInt32("0");
                objbo.PROPOSAL_ID = Convert.ToInt32("0");
                objbo.PRO_SEGMENT_ID = Convert.ToInt32("0");
                objbo.Payment = txtPayment.Text.ToString();
                objbo.Tariff = txtTariff.Text.ToString();
                objbo.Booking_given_to = txtBookingGivenTo.Text.ToString();
                objbo.Hotel_Invoice_Num = txtHotelInvoiceNo.Text.ToString();
                if (txtBillDate.Text == "" || txtBillDate.Text == string.Empty)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Select Bill Date of ticket booking.";
                    return;
                }
                else
                {
                    objbo.Bill_date = DateTime.ParseExact(txtBillDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                objbo.Amount = txtAmount.Text.ToString();
                objbo.created_by = User.Identity.Name;
                objbo.created_on = System.DateTime.Now;
                objbo.isActive = true;
                objbo.modified_by = User.Identity.Name;
                objbo.modified_on = System.DateTime.Now;
                objbo.CurrentStatus = "Booked";

                int result = objbl.Create_OutLocal_Accommadation_ticket_booking_details(objbo, 0);
                if (result == 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Ticket booked successfully";
                    SendMail();
                    clearcontrol();
                    ClearcontrolAccomm();
                    LoadProposalAccommoDetails();
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Unknown error occured";
                }
                Session["RequisitionSelectedID"] = null;
                //}
                //else
                //{
                //    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                //    lblMessageBoard.Text = "Select book a ticket to submit ticket booking.";
                //}
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                lblMessage .Text = error;
                lblMessage .ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnVehiTicketBook_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Session["RequisitionSelectedID"] != null)
                //{
                ticketbookingVehibo objbo = new ticketbookingVehibo();
                ticketbookingVehicollectionbo objcollectionbo = new ticketbookingVehicollectionbo();
                ticketbookingbl objbl = new ticketbookingbl();
                objbo.Vehicle_req_id = (int)Session["Out_Vehi_REQUISITION_ID"]; ;
                objbo.REQUISITION_ID = Convert.ToInt32("0");
                objbo.REQ_SEGMENT_ID = Convert.ToInt32("0");
                objbo.PROPOSAL_ID = Convert.ToInt32("0");
                objbo.PRO_SEGMENT_ID = Convert.ToInt32("0");
                objbo.EMPLOYEE_NO = "";
                objbo.Vehicle_Source = drpdwnVehicleSource.Text.ToString();
                objbo.Agent_Name = drpdwnAgentName.Text.ToString();
                objbo.Booking_passed_to = txtBookingPassed.Text.ToString();
                objbo.Vehicle_Num = txtVehicleNumb.Text.ToString();
                objbo.Rate_kms = txtRateKms.Text.ToString();
                objbo.Driver_Name = txtDriverName.Text.ToString();
                objbo.Contact_Number = txtContactNumber.Text.ToString();
                objbo.Statutory_Req = txtStatutory.Text.ToString();
                objbo.Agent_BillNum = txtAgentBillNumb.Text.ToString();
                if (txtBillDateV.Text == "" || txtBillDateV.Text == string.Empty)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Select Bill Date of ticket booking.";
                    return;
                }
                else
                {
                    objbo.Bill_Date = DateTime.ParseExact(txtBillDateV.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                objbo.Agent_BillAmnt = txtAgentBillAmnt.Text.ToString();
                objbo.Driver_Batta = txtDriverBatta.Text.ToString();
                objbo.Total_km = txtTotalKm.Text.ToString();
                objbo.Total_Cost = txtTotalCost.Text.ToString();
                objbo.created_by = User.Identity.Name;
                objbo.created_on = System.DateTime.Now;
                objbo.isActive = true;
                objbo.modified_by = User.Identity.Name;
                objbo.modified_on = System.DateTime.Now;
                objbo.CurrentStatus = "Booked";

                int result = objbl.Create_OutLocal_Vehicle_ticket_booking_details(objbo, 0);
                if (result == 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Ticket booked successfully";
                    SendMail();
                    clearcontrol();
                    ClearcontrolVehi();
                    LoadProposalVehiDetails();
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Unknown error occured";
                }
                Session["RequisitionSelectedID"] = null;
                //}
                //else
                //{
                //    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                //    lblMessageBoard.Text = "Select book a ticket to submit ticket booking.";
                //}
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                lblMessage.Text = error;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        
        void clearcontrol()
        {
            txtEmployeeName.Text = "";
            txtDepartment.Text = "";
            txtDesignation.Text = "";
            txtContactNumb.Text = "";
            txtPayment.Text = "";
            txtTariff.Text = "";
            txtBookingGivenTo.Text = "";
            txtHotelInvoiceNo.Text = "";
            txtBillDate.Text = "";
            txtAmount.Text = "";
            pnlAccomTicketbooking.Visible = false; 
        }

        private void SendMail()
        {
            try
            {
                string strMailToList = string.Empty;
                string strSubject = "Ticket Booking details";
                string strPernr_Mail = string.Empty;
                string strBodyMsg = PrepareMailBody();
                iEmpPowerMaster_Load.masterbl.DispatchMail(strMailToList, User.Identity.Name, strSubject, strPernr_Mail, strBodyMsg);
                lblMessage .ForeColor = System.Drawing.Color.Green;
                lblMessage .Text = "Ticket booked and Requisition details sent successfully.";
                clearcontrol();                
            }
            catch
            {
                lblMessage .ForeColor = System.Drawing.Color.Red;
                lblMessage .Text = "Unknown error occured. Please contact your system administrator.";
                return;
            }
        }

        protected string PrepareMailBody()
        {
            string body = "";

            body = "<table style='width: 700px' border='1' cellpadding='1' cellspacing='0'>";

            body += "<tr>";
            body += "<td ><strong>Employee Name </strong></td>";
            body += "<td>" + txtEmployeeName.Text + "</td>";
            body += "</tr>";

            body += "<tr>";
            body += "<td ><strong>Ticket Generated Date</strong></td>";
            body += "<td>" + System.DateTime.Now + "</td>";
            body += "</tr>";

            body += "<tr>";
            body += "<td ><strong>Requisition ID </strong></td>";
            body += "<td>" + Convert.ToInt32(Session["req_id"]) + "</td>";
            body += "</tr>";

            body += "<tr>";
            body += "<td ><strong>Payment</strong></td>";
            body += "<td>" + txtPayment.Text + "</td>";
            body += "</tr>";

            body += "<tr>";
            body += "<td  ><strong>Tariff</strong></td>";
            body += "<td>" + txtTariff.Text + "</td>";
            body += "</tr>";

            body += "<tr>";
            body += "<td  ><strong>Booking Given To </strong></td>";
            body += "<td>" + txtBookingGivenTo.Text + "</td>";
            body += "</tr>";

            body += "<tr>";
            body += "<td  ><strong>Hotel Invoice Number</strong></td>";
            body += "<td>" + txtHotelInvoiceNo.Text + "</td>";
            body += "</tr>";

            body += "<tr>";
            body += "<td  ><strong>Bill Date</strong></td>";
            body += "<td>" + txtBillDate.Text + "</td>";
            body += "</tr>";

            body += "<tr>";
            body += "<td  ><strong>Amount </strong></td>";
            body += "<td>" + txtAmount.Text + " </td>";
            body += "</tr>";

            body += "<tr>";
            body += "<td><strong>Note: </strong>Please carry your photo ID during travel</td>";
            body += "</tr>";

            body += "<tr>";
            body += "<td><strong>**********************HAPPY JOURNEY*******************</strong></td>";
            body += "</tr>";

            body += "</table>";

            return body;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearcontrol();
            ClearcontrolVehi();
        }
        protected void btnAccCancel_Click(object sender, EventArgs e)
        {
            clearcontrol();
            ClearcontrolAccomm();
        }

        protected void DropDownHotelPlaceCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            accomodation_requisitionbl AccRequisitionBl = new accomodation_requisitionbl();
            accomodation_requisitionbo AccRequisitionBo = new accomodation_requisitionbo();
            string placecode = DropDownHotelPlaceCity.SelectedItem.Value;
            string pernr = Session["sEmploreeId"].ToString();
            DropDownHotel_category.Items.Clear();
            //loading the HotelCategory drop down.
            DropDownHotel_category.DataSource = AccRequisitionBl.GetHotelCategory(pernr, placecode);
            DropDownHotel_category.DataTextField = "HOTEL_CATEGORY";
            DropDownHotel_category.DataValueField = "HOTEL_CAT_CODE";
            DropDownHotel_category.DataBind();
            ListItem Item = new ListItem("Select", "0", true);
            DropDownHotel_category.Items.Add(Item);
            DropDownHotel_category.SelectedValue = "0";
        }

        protected void DropDownHotel_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            accomodation_requisitionbl AccRequisitionBl = new accomodation_requisitionbl();
            accomodation_requisitionbo AccRequisitionBo = new accomodation_requisitionbo();

            string HC = DropDownHotel_category.SelectedItem.Value;
            string placecode = DropDownHotelPlaceCity.SelectedItem.Value;


            //loading the Hotel name drop down.
            DropDownHotel_name.Items.Clear();
            DropDownHotel_name.DataSource = AccRequisitionBl.GetHotelNames(HC, placecode);
            DropDownHotel_name.DataValueField = "HOTEL_CODE";
            DropDownHotel_name.DataTextField = "HOTEL_NAME";
            DropDownHotel_name.DataBind();
            ListItem HotelNameItem = new ListItem("Select", "0");
            DropDownHotel_name.Items.Add(HotelNameItem);
            DropDownHotel_name.SelectedValue = "0";

            //loading the Room category drop down.
            DropDownRoom_category.Items.Clear();
            DropDownRoom_category.DataSource = AccRequisitionBl.GetRoomNames(HC);
            DropDownRoom_category.DataValueField = "ROOM_CODE";
            DropDownRoom_category.DataTextField = "RoomCategoryName";
            DropDownRoom_category.DataBind();
            ListItem RoomNameItem = new ListItem("Select", "0");
            DropDownRoom_category.Items.Add(RoomNameItem);
            DropDownRoom_category.SelectedValue = "0";


        }
    }
}
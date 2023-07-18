using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerBL.Local_requisition;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Local_requisition;

namespace iEmpPower.UI.Local_requisition
{
    public partial class TicketBook_accvehi_outlocal : System.Web.UI.Page
    {
        protected MembershipUser memUser;
        public bool TravelRequestSortedOrder_Bool = false;
        public int TravelRequestGridSelectedIndexChange = -1;
        public bool AccomRequestSortedOrder_Bool = false;
        public int AccomRequestGridSelectedIndexChange = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                memUser = Membership.GetUser();
                string empname = memUser.ToString();
                lblMessageBoard.Text = "";
               // LoadTravelType();
                RadioButtontraveltype.SelectedValue = "1";
                Loadmtport(DropDownVehicletype); //loding the drop down  vehicle type.
                loadlocaltravelrequisitiondetails();
                Session.Add("TravelRequestSortedOrder_Bool", TravelRequestSortedOrder_Bool);

                //====accommodation===
                LoadDropdowns();//Intializing drop down method should be place at first.
                LoadLocalRequisitionGridView("0");
                Session.Add("AccomRequestSortedOrder_Bool", AccomRequestSortedOrder_Bool);
            }
        }


        protected void DomesticTravelRequiLinkButton()
        {
            GridViewRow row = LocalRequisitionGridView.SelectedRow;
          
            string EmploID = LocalRequisitionGridView.Rows[row.RowIndex].Cells[6].Text;
            

            string RequisitionID = LocalRequisitionGridView.DataKeys[row.RowIndex].Value.ToString(); ////LocalRequisitionGridView.Rows[row.RowIndex].Cells[0].Text;

            Session["RequisitionSelectedID"] = RequisitionID;
            BindControlsForEdit(Convert.ToInt32(RequisitionID));
            
        }

        protected void DomesticTravelRequiVehLinkButton()
        {
            GridViewRow row = LocalTravelRequisitionGridView.SelectedRow;
           
            string EmploID = LocalTravelRequisitionGridView.Rows[row.RowIndex].Cells[8].Text;


            string RequisitionID = LocalTravelRequisitionGridView.Rows[row.RowIndex].Cells[0].Text;//LocalTravelRequisitionGridView.DataKeys[row.RowIndex].Value.ToString(); ////LocalRequisitionGridView.Rows[row.RowIndex].Cells[0].Text;

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
        //    string EmploID = LocalRequisitionGridView.Rows[row.RowIndex].Cells[6].Text;
        //    ////LoadEmployeeVisaPassportDetails(EmploID);

        //    string RequisitionID = LocalRequisitionGridView.DataKeys[row.RowIndex].Value.ToString(); ////LocalRequisitionGridView.Rows[row.RowIndex].Cells[0].Text;

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
        //    string EmploID = LocalTravelRequisitionGridView.Rows[row.RowIndex].Cells[8].Text;
        //    ////LoadEmployeeVisaPassportDetails(EmploID);

        //    string RequisitionID = LocalTravelRequisitionGridView.DataKeys[row.RowIndex].Value.ToString(); ////LocalRequisitionGridView.Rows[row.RowIndex].Cells[0].Text;

        //    Session["RequisitionSelectedID"] = RequisitionID;
        //    BindControlsForEdit(Convert.ToInt32(RequisitionID));
        //    //}
        //    //else
        //    //{
        //    //    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
        //    //    lblMessageBoard.Text = "Please Book the segments(Accommodation/Vehicle).";
        //    //}
        //}

        ////public void LoadEmployeeVisaPassportDetails(string EmploID)
        //{
        //    try
        //    {
        //        visaPassportBo objVisaPassportBo = new visaPassportBo();
        //        requisitions_traveldeskbl objrequisitionBl = new requisitions_traveldeskbl();
        //        visaPassportcollectionbo objVisaPassportLst = objrequisitionBl.Load_VisaPassport_DetailsforTravelDesk(EmploID);

        //        grdVisaPassportExpiry.DataSource = objVisaPassportLst;
        //        grdVisaPassportExpiry.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblMessage.Text = "Unknown Error( " + ex.Message + ")";
        //        //lblMessage.ForeColor = System.Drawing.Color.Red;
        //    }
        //}

        protected void BindControlsForEdit(int RequisitionSegmentId)
        {
            travelrequestbl travelrequestblObj = new travelrequestbl();
            List<ticketbookingbo> ticketlist = new List<ticketbookingbo>();
            ticketlist = (List<ticketbookingbo>)Session["DomesticSegmentList"];
            if (ticketlist == null)
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Unknown error occured.";
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

        protected void tcDefalut_ActiveTabChanged(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            Clear();
            ClearControl();

            if (tcDefalut.ActiveTab == tabVehicle)
            {
                //LoadTravelType();
                RadioButtontraveltype.SelectedValue = "1";
                Loadmtport(DropDownVehicletype); //loding the drop down  vehicle type.
                loadlocaltravelrequisitiondetails();
                Session.Add("TravelRequestSortedOrder_Bool", TravelRequestSortedOrder_Bool);
            }
            else
            {
                //====accommodation===
                LoadDropdowns();//Intializing drop down method should be place at first.
                LoadLocalRequisitionGridView("0");
                Session.Add("AccomRequestSortedOrder_Bool", AccomRequestSortedOrder_Bool);
            }
        }
        
        protected void Loadmtport(DropDownList ddl)
        {

            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetModeofTransport();
            ddl.DataTextField = "MODE_OF_TRANSPOPRT_FZTXT";
            ddl.DataValueField = "MODE_OF_TRANSPOPRT_KZPMF";
            ddl.DataBind();
            ListItem drpDefaultItem1 = new ListItem("Select vehicle type", "0", true);
            ddl.Items.Add(drpDefaultItem1);
            ddl.SelectedValue = "0";
        }

        void loadlocaltravelrequisitiondetails()
        {
            vehicle_requisitionbo objbo = new vehicle_requisitionbo();
            local_travel_requisitionbl objbl = new local_travel_requisitionbl();
            List<vehicle_requisitionbo> objlist = new List<vehicle_requisitionbo>();
            memUser = Membership.GetUser();
            string empno = "0";
            string selectedvalue = "";
            objlist = objbl.Get_Local_travel_Details(selectedvalue, empno, "8");//"10");
            Session.Add("LocalTravelDetails", objlist);
            if (objlist.Count != 0)
            {
                LocalTravelRequisitionGridView.DataSource = objlist;
                LocalTravelRequisitionGridView.DataBind();
            }
            else
            {
                LocalTravelRequisitionGridView.DataSource = null;
                LocalTravelRequisitionGridView.DataBind();
            }
        }

        protected void LocalTravelRequisitionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.LocalTravelRequisitionGridView, "Select$" + e.Row.RowIndex);
            }
        }

        protected void LocalTravelRequisitionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            LocalTravelRequisitionGridView.PageIndex = e.NewPageIndex;
            List<vehicle_requisitionbo> requisitionboList = (List<vehicle_requisitionbo>)Session["LocalTravelDetails"];
            LocalTravelRequisitionGridView.DataSource = requisitionboList;
            int SelectedId = Convert.ToInt32(ViewState["SelectedRow"], CultureInfo.InvariantCulture);
            int pagindex = Convert.ToInt32(ViewState["PageRelatedToSelectedRow"], CultureInfo.InvariantCulture);
            LocalTravelRequisitionGridView.SelectedIndex = -1;
            LocalTravelRequisitionGridView.DataBind();
            if (pageindex == pagindex)
            {
                LocalTravelRequisitionGridView.SelectedIndex = SelectedId;
            }
        }

        protected void LocalTravelRequisitionGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<vehicle_requisitionbo> RequiBoListObject = (List<vehicle_requisitionbo>)Session["LocalTravelDetails"];
            bool objSortOrder = (bool)Session["TravelRequestSortedOrder_Bool"];
            string SortExpression = e.SortExpression.ToString().Trim();
            RequiBoListObject = GridManager.SortLocalTravelRequestGridView(SortExpression, objSortOrder, RequiBoListObject);
            LocalTravelRequisitionGridView.DataSource = RequiBoListObject;
            LocalTravelRequisitionGridView.DataBind();
            // Add negation of sort order to session for next time use.
            Session.Add("TravelRequestSortedOrder_Bool", !objSortOrder);
            // Add sorted list to the session
            Session.Add("LocalTravelDetails", RequiBoListObject);
        }

        protected void LocalTravelRequisitionGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            try
            {

                pnlVehiTicketbooking.Visible = true;
                DomesticTravelRequiVehLinkButton();
                GridViewRow grdRow = LocalTravelRequisitionGridView.SelectedRow;
                int local_travel_req_id = Convert.ToInt32(grdRow.Cells[0].Text.ToString().Trim());
                Session.Add("LOCAL_TRAVEL_REQUISITION_ID", local_travel_req_id);
                List<vehicle_requisitionbo> requisitionboList = new List<vehicle_requisitionbo>();
                requisitionboList = (List<vehicle_requisitionbo>)Session["LocalTravelDetails"];
                vehicle_requisitionbo RequisationBo = requisitionboList.Find(delegate(vehicle_requisitionbo obj)
                {
                    return obj.local_travel_req_id == local_travel_req_id;
                });
                RadioButtontraveltype.SelectedValue = RequisationBo.TRAVEL_TYPE;
                FromTextBox.Text = RequisationBo.Departure_from;
                ToTextBox.Text = RequisationBo.Destination_to;

                DropDownVehicletype.SelectedValue = RequisationBo.VehicleTypeId;
                if (RequisationBo.VehicleTypeId != "0")
                {
                    loadmcate(DropDownVehiclecategory, RequisationBo.VehicleTypeId);
                    DropDownVehiclecategory.SelectedValue = RequisationBo.VehicleCategoryId;
                }
                if (RequisationBo.VehicleCategoryId != "0")
                {
                    DropDownVehiclename.Items.Clear();
                    loadvehiclename(DropDownVehiclename, DropDownVehiclecategory.SelectedValue);
                    DropDownVehiclename.Enabled = true;
                    DropDownVehiclename.SelectedValue = RequisationBo.VehicleId;
                }

                PickuptimeTextBox.Text = RequisationBo.Pickup_time;
                PickupaddressTextBox.Text = RequisationBo.Pickup_address;
                DroptimeTextBox.Text = RequisationBo.Drop_time;
                DropaddressTextBox.Text = RequisationBo.Drop_address;
                PurposeoftravelTextBox.Text = RequisationBo.Purpose_of_travel;

                RemarksTextBox.Text = RequisationBo.remarks;
                PurposeoftravelTextBox.Text = RequisationBo.Purpose_of_travel;
                CheckInDateTextBox.Text = RequisationBo.Date_of_travel.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                //CheckOutDateTextBox.Text = Convert.ToString(RequisationBo.Check_out_date);
                CheckOutDateTextBox.Text = RequisationBo.To_Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                CheckInDateTextBox.Enabled = false;
                CheckOutDateTextBox.Enabled = false;
                CheckInDateImageButton.Enabled = false;
                CheckOutDateImageButton.Enabled = false;

                if (RequisationBo.Number_of_members > 0)
                {
                    Panelmember.Visible = true;
                    vehicle_requisitionbo objbo = new vehicle_requisitionbo();
                    local_travel_requisitionbl objbl = new local_travel_requisitionbl();
                    List<vehicle_requisitionbo> objlist = new List<vehicle_requisitionbo>();

                    objlist = objbl.Get_Local_travel_Group_members(local_travel_req_id);
                    NumberofmembersTextBox.Text = RequisationBo.Number_of_members.ToString();
                    InitialiseAddRemoveEmployeeSegments(objlist, objlist.Count - 1);
                }

                if (grdRow.RowIndex > -1)
                {
                    int iSelectedRow = Convert.ToInt32(grdRow.RowIndex, CultureInfo.CurrentCulture);
                    int iPageRelatedToSelectedRow = Convert.ToInt32(LocalTravelRequisitionGridView.PageIndex, CultureInfo.CurrentCulture);
                    ViewState.Add("SelectedRow", iSelectedRow);
                    ViewState.Add("PageRelatedToSelectedRow", iPageRelatedToSelectedRow);
                }


            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            string s = ToTextBox.Text;
        }

        protected void loadmcate(DropDownList ddl, string MediumOfTransport)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetModeofCategory(MediumOfTransport);
            ddl.DataTextField = "MEDIA_OF_CATEGORY_TEXT25";
            ddl.DataValueField = "MEDIA_OF_CATEGORY_PKWKL";
            ddl.DataBind();
            ListItem drpDefaultItem2 = new ListItem("Select vehicle category", "0", true);
            ddl.Items.Add(drpDefaultItem2);
            ddl.SelectedValue = "0";
        }

        protected void loadvehiclename(DropDownList ddl, string MediumOfCategory)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetVehicleName(MediumOfCategory);
            ddl.DataTextField = "VEHICLE_NAME_ZZVEHNAM";
            ddl.DataValueField = "VEHICLE_NAME_VHNUM";
            ddl.DataBind();
            ListItem drpDefaultItem9 = new ListItem("Select vehicle name", "0", true);
            ddl.Items.Add(drpDefaultItem9);
            ddl.SelectedValue = "0";
        }

        void InitialiseAddRemoveEmployeeSegments(List<vehicle_requisitionbo> objCollection, int count)
        {
            List<vehicle_requisitionbo> ListObject = new List<vehicle_requisitionbo>();
            for (int i = 0; i < objCollection.Count; i++)
            {

                vehicle_requisitionbo obj = new vehicle_requisitionbo();
                ListObject.Add(obj);
            }
            EmployeeDetailsGridView.DataSource = ListObject;
            EmployeeDetailsGridView.DataBind();

            for (int i = 0; i < objCollection.Count; i++)
            {
                CheckBox EmployeeNameCheckBox = (CheckBox)EmployeeDetailsGridView.Rows[i].FindControl("EmployeeNameCheckBox");
                Label EmployeeNameLabel = (Label)EmployeeDetailsGridView.Rows[i].FindControl("EmployeeNameLabel");
                Label EmployeeIdLabel = (Label)EmployeeDetailsGridView.Rows[i].FindControl("EmployeeIdLabel");
                if (objCollection[i].IS_EMPLOYEE_CHECKD == true)
                {
                    EmployeeNameCheckBox.Checked = true;
                }
                else
                {
                    EmployeeNameCheckBox.Checked = false;
                }

                EmployeeNameLabel.Text = objCollection[i].Emp_name;
                EmployeeIdLabel.Text = objCollection[i].EMPLOYEE_NO;
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                loadlocaltravelrequisitiondetails();
                ClearControl();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        void ClearControl()
        {
            FromTextBox.Text = "";
            ToTextBox.Text = "";
            DropDownVehicletype.SelectedIndex = -1;
            DropDownVehiclename.SelectedIndex = -1;
            DropDownVehiclecategory.SelectedIndex = -1;
            DropDownVehiclecategory.Enabled = false;
            DropDownVehiclename.Enabled = false;
            PickuptimeTextBox.Text = "";
            PickupaddressTextBox.Text = "";
            DroptimeTextBox.Text = "";
            DropaddressTextBox.Text = "";
            NameOfMembersTextBox.Text = "";
            NumberofmembersTextBox.Text = "";
            PurposeoftravelTextBox.Text = "";
            RemarksTextBox.Text = "";
            CheckInDateTextBox.Text = "";
            CheckOutDateTextBox.Text = "";
            CheckInDateTextBox.Enabled = false;
            CheckOutDateTextBox.Enabled = false;
            CheckInDateImageButton.Enabled = false;
            CheckOutDateImageButton.Enabled = false;

            Session["LOCAL_TRAVEL_REQUISITION_ID"] = null;
            EmployeeDetailsGridView.DataSource = null;
            EmployeeDetailsGridView.DataBind();
            txtRateKms.Text = "";
            txtAgentBillNumb.Text = "";
            txtDriverName.Text = "";
            txtBillDate.Text = "";
            txtBookingPassed.Text = "";
            txtContactNumber.Text = "";
            txtAgentBillAmnt.Text = "";
            txtVehicleNumb.Text = "";
            txtDriverBatta.Text = "";
            txtStatutory.Text = "";
            txtTotalCost.Text = "";
            txtTotalKm.Text = "";
      //      pnlVehiTicketbooking.Enabled = false;
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
                objbo.Vehicle_req_id = (int)Session["LOCAL_TRAVEL_REQUISITION_ID"];
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

                if (txtBillDate.Text == "" || txtBillDate.Text == string.Empty)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Select Bill Date of ticket booking.";
                    return;
                }
                else
                {
                    objbo.Bill_Date = DateTime.ParseExact(txtBillDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
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

                int result = objbl.Create_OutLocal_Vehicle_ticket_booking_details(objbo,1);
                if (result == 0)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    lblMessageBoard.Text = "Ticket booked successfully";
                    SendMail();
                    loadlocaltravelrequisitiondetails();
                    ClearControl();
                }
                else
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Unknown error occured";
                }
                Session["RequisitionSelectedID"] = null;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                lblMessageBoard.Text = error;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            }
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
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Ticket booked and Requisition details sent successfully.";
                ClearControl();
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Unknown error occured. Please contact your system administrator.";
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
            ClearControl();
        }

        //================================================================ACCOMMODATION==========================================================
 
        protected void BindControlsForUpdate()
        {
            GridViewRow grdRow = LocalRequisitionGridView.SelectedRow;
            int LOCAL_ACC_REQUISITION_ID = Convert.ToInt32(grdRow.Cells[0].Text.ToString().Trim());
            Session.Add("LOCAL_ACC_REQUISITION_ID", LOCAL_ACC_REQUISITION_ID);
            List<accomodation_requisitionbo> AccomRequiBoForMembers = new List<accomodation_requisitionbo>();
            accomodation_requisitionbo requisitionboObj = new accomodation_requisitionbo();
            local_accommodation_requisitionbl travelrequestblObj = new local_accommodation_requisitionbl();
            List<accomodation_requisitionbo> requisitionboList = new List<accomodation_requisitionbo>();

            //Filtering the selected row the grid
            requisitionboList = (List<accomodation_requisitionbo>)Session["LocalAccommodationRequisitionDetails"];
            accomodation_requisitionbo RequisationBo = requisitionboList.Find(
                delegate(accomodation_requisitionbo obj)
                {    
                    
                    return obj.local_acc_req_id == LOCAL_ACC_REQUISITION_ID;
                }
            );
            //Selecting accommodation type radio buttons
            AccommodationTypeRadioButtonList.SelectedValue = RequisationBo.LOCAL_ACCOMMODATION_TYPE;
            if (AccommodationTypeRadioButtonList.SelectedValue == "1")
            {
                VisibilityFalse();
           //     Clear();
            }
            else if (AccommodationTypeRadioButtonList.SelectedValue == "2")
            {
                VisibilityTure();
            //    Clear();
            }
            else if (AccommodationTypeRadioButtonList.SelectedValue == "3")
            {
                VisibilityTure();
           //     Clear();
            }
            //Assigning members to the respective fields
            CheckInDateTextBoxA.Text =RequisationBo.Check_in_date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            //CheckOutDateTextBox.Text = Convert.ToString(RequisationBo.Check_out_date);
            CheckOutDateTextBoxA.Text = RequisationBo.Check_out_date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            ArrivalTimeTextBox.Text = RequisationBo.Arrival_time;
            DepartureTimeTextBox.Text = RequisationBo.Departure_time;
            DropDownHotel_category.SelectedValue = Convert.ToString(RequisationBo.HOTEL_CAT_CODE);
            DropDownRoom_category.SelectedValue = Convert.ToString(RequisationBo.ROOM_CODE);
            DropDownHotel_name.SelectedValue = Convert.ToString(RequisationBo.HOTEL_CODE);
            HotelPlaceCityTextBox.Text = RequisationBo.HotelPlaceCity;
            AdditionalServicesTextBox.Text = RequisationBo.Additional_service;
            RemarksTextBoxA.Text = RequisationBo.Remarks;
            //Get the details of the members like member id and name
            if (RequisationBo.LOCAL_ACCOMMODATION_TYPE != "1")
            {
                VisibilityTure();
                if (RequisationBo.Number_of_members > 0)
                {
                    AccomRequiBoForMembers = travelrequestblObj.GetTheMemberDetails(RequisationBo.local_acc_req_id);
                    NumberOfMembersTextBoxA.Text = RequisationBo.Number_of_members.ToString();
                    InitialiseAddRemoveEmployeeSegments(AccomRequiBoForMembers, AccomRequiBoForMembers.Count - 1);
                }
            }
            //If status  is new then only show update button modify requisiton other wise only view.
            if (grdRow.RowIndex > -1)
            {
                int iSelectedRow = Convert.ToInt32(grdRow.RowIndex, CultureInfo.CurrentCulture);
                int iPageRelatedToSelectedRow = Convert.ToInt32(LocalRequisitionGridView.PageIndex, CultureInfo.CurrentCulture);
                ViewState.Add("SelectedRow", iSelectedRow);
                ViewState.Add("PageRelatedToSelectedRow", iPageRelatedToSelectedRow);
            }
        }
        
        void VisibilityTure()
        {
            NumbersOfMembers.Visible = true;
            NumberOfMembersTextBoxA.Text = "";
            NameOfMemberPanel.Visible =true;
            NameOfMembersTextBoxA.Text = "";
            EmployeeNamePanel.Visible = true;
        }
        void VisibilityFalse()
        {
            NumbersOfMembers.Visible = false;
            NumberOfMembersTextBoxA.Text = "";
            NameOfMemberPanel.Visible = false;
            NameOfMembersTextBoxA.Text = "";
            EmployeeNamePanel.Visible = false;
            EmployeeDetailsGridViewA.DataSource = null;
            EmployeeDetailsGridViewA.DataBind();
        }
        void Clear()
        {
            CheckInDateTextBoxA.Text = "";
            CheckOutDateTextBoxA.Text = "";
            ArrivalTimeTextBox.Text = "";
            DepartureTimeTextBox.Text = "";
            HotelPlaceCityTextBox.Text = "";
            AdditionalServicesTextBox.Text = "";
            NumberOfMembersTextBoxA.Text = "";
            NameOfMembersTextBoxA.Text = "";
            RemarksTextBoxA.Text = "";
            Session["LOCAL_ACC_REQUISITION_ID"] = null;
            EmployeeDetailsGridViewA.DataSource = null;
            EmployeeDetailsGridViewA.DataBind();
            DropDownHotel_category.SelectedIndex = -1;
            DropDownRoom_category.SelectedIndex = -1;
            DropDownHotel_name.SelectedIndex = -1;

            txtEmployeeName.Text = "";
            txtDepartment.Text = "";
            txtDesignation.Text = "";
            txtContactNumb.Text = "";
            txtPayment.Text = "";
            txtTariff.Text = "";
            txtBookingGivenTo.Text = "";
            txtHotelInvoiceNo.Text = "";
            txtBillDateA.Text = "";
            txtAmount.Text = "";
      //      pnlAccomTicketbooking.Enabled = false; 
        }
        protected List<accomodation_requisitionbo> GetGridViewAllRowsControlValues()
        {
            List<accomodation_requisitionbo> MemberNameDetailsListObject = new List<accomodation_requisitionbo>();
            for (int i = 0; i < EmployeeDetailsGridViewA.Rows.Count; i++)
            {
                accomodation_requisitionbo obj = new accomodation_requisitionbo();
                CheckBox EmployeeNameCheckBox = (CheckBox)EmployeeDetailsGridViewA.Rows[i].FindControl("EmployeeNameCheckBox");
                Label EmployeeNameLabel = (Label)EmployeeDetailsGridViewA.Rows[i].FindControl("EmployeeNameLabel");
                Label EmployeeIdLabel = (Label)EmployeeDetailsGridViewA.Rows[i].FindControl("EmployeeIdLabel");

                if (EmployeeNameCheckBox.Checked)
                {
                    obj.IS_EMPLOYEE_CHECKD = true;
                }
                else
                {
                    obj.IS_EMPLOYEE_CHECKD = false;
                }
                obj.Name_of_members = EmployeeNameLabel.Text;
                obj.EMPLOYEE_NO = EmployeeIdLabel.Text;

                MemberNameDetailsListObject.Add(obj);
            }
            return MemberNameDetailsListObject;
        }

        protected void CreateEmployeeGridViewRows(List<accomodation_requisitionbo> objList, bool isAddRemoveRange)
        {
            if (isAddRemoveRange == true)
            {
                accomodation_requisitionbo obj = new accomodation_requisitionbo();
                objList.Add(obj);
            }
            EmployeeDetailsGridViewA.DataSource = objList;
            EmployeeDetailsGridViewA.DataBind();
        }

        void InitialiseAddRemoveEmployeeSegments(List<accomodation_requisitionbo> objCollection, int count)
        {
            List<accomodation_requisitionbo> ListObject = new List<accomodation_requisitionbo>();
            for (int i = 0; i < objCollection.Count; i++)
            {

                accomodation_requisitionbo obj = new accomodation_requisitionbo();
                ListObject.Add(obj);
            }
            EmployeeDetailsGridViewA.DataSource = ListObject;
            EmployeeDetailsGridViewA.DataBind();
         
            for (int i = 0; i < objCollection.Count; i++)
            {
                CheckBox EmployeeNameCheckBox = (CheckBox)EmployeeDetailsGridViewA.Rows[i].FindControl("EmployeeNameCheckBox");
                Label EmployeeNameLabel = (Label)EmployeeDetailsGridViewA.Rows[i].FindControl("EmployeeNameLabel");
                Label EmployeeIdLabel = (Label)EmployeeDetailsGridViewA.Rows[i].FindControl("EmployeeIdLabel");
                    if (objCollection[i].IS_EMPLOYEE_CHECKD == true)
                    {
                        EmployeeNameCheckBox.Checked = true;
                    }
                    else
                    {
                        EmployeeNameCheckBox.Checked = false;
                    }

                    EmployeeNameLabel.Text = objCollection[i].Name_of_members ;
                    EmployeeIdLabel.Text = objCollection[i].EMPLOYEE_NO ;
            }
        }

        void LoadEmployeeGridViewFirstRow()
        {
            List<accomodation_requisitionbo> objCollection = new List<accomodation_requisitionbo>();
            CreateEmployeeGridViewRows(objCollection, true);
            InitialiseAddRemoveEmployeeSegments(objCollection, 0);
        }

        //Loading the local accommodation requisiton.
        public void LoadLocalRequisitionGridView(string LOCAL_REQUISITION_TYPE)
        {
            memUser = Membership.GetUser();
            string EmployeeNo = memUser.ToString();
            accomodation_requisitionbo requisitionboObj = new accomodation_requisitionbo();
            local_accommodation_requisitionbl travelrequestblObj = new local_accommodation_requisitionbl();
            List<accomodation_requisitionbo> requisitionboList = new List<accomodation_requisitionbo>();
            requisitionboList = travelrequestblObj.LoadAccommodationRequestionDetails(LOCAL_REQUISITION_TYPE, "0", "8");
           
            if (requisitionboList == null || requisitionboList.Count == 0)
            {
                LocalRequisitionGridView.Visible = false;
                LocalRequisitionGridView.DataSource = null; 
            }
            else
            {
                LocalRequisitionGridView.Visible = true;
                LocalRequisitionGridView.DataSource = requisitionboList;
                Session.Add("LocalAccommodationRequisitionDetails", requisitionboList);
                LocalRequisitionGridView.SelectedIndex = -1;
            }
            LocalRequisitionGridView.DataBind();
        }

        bool Validated()
        {
            if (Convert.ToInt32(NumberOfMembersTextBoxA.Text) == EmployeeDetailsGridViewA.Rows.Count)
            {
                return true;
            }
            return false;
        }
         void LoadDropdowns()
        {
            accomodation_requisitionbl AccRequisitionBl = new accomodation_requisitionbl();
            accomodation_requisitionbo AccRequisitionBo = new accomodation_requisitionbo();
            //DropDownHotel_category.Items.Clear();
            ////loading the HotelCategory drop down.
            //DropDownHotel_category.DataSource = AccRequisitionBl.GetHotelCategory();
            //DropDownHotel_category.DataValueField = "HOTEL_CAT_CODE";
            //DropDownHotel_category.DataTextField = "HOTEL_CATEGORY";
            //DropDownHotel_category.DataBind();
            //ListItem Item = new ListItem("Select", "0");
            //DropDownHotel_category.Items.Add(Item);
            //DropDownHotel_category.SelectedValue = "0";

            ////loading the Hotel name drop down.
            //DropDownHotel_name.Items.Clear();
            //DropDownHotel_name.DataSource = AccRequisitionBl.GetHotelNames();
            //DropDownHotel_name.DataValueField = "HOTEL_CODE";
            //DropDownHotel_name.DataTextField = "HOTEL_NAME";
            //DropDownHotel_name.DataBind();
            //ListItem HotelNameItem = new ListItem("Select", "0");
            //DropDownHotel_name.Items.Add(HotelNameItem);
            //DropDownHotel_name.SelectedValue = "0";

            ////loading the Room category drop down.
            //DropDownRoom_category.Items.Clear();
            //DropDownRoom_category.DataSource = AccRequisitionBl.GetRoomNames();
            //DropDownRoom_category.DataValueField = "ROOM_CODE";
            //DropDownRoom_category.DataTextField = "RoomCategoryName";
            //DropDownRoom_category.DataBind();
            //ListItem RoomNameItem = new ListItem("Select", "0");
            //DropDownRoom_category.Items.Add(RoomNameItem);
            //DropDownRoom_category.SelectedValue = "0";
        }

        bool ValidateControl()
        {
            try
            {


                DateTime dt = DateTime.ParseExact(CheckInDateTextBoxA.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime dt1 = DateTime.ParseExact(CheckOutDateTextBoxA.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                if (ArrivalTimeTextBox.Text != "")
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([0-1][0-9]|2[0-3]):([0-5][0-9])$");
                    if (regex.IsMatch(ArrivalTimeTextBox.Text))
                    {
                        lblMessageBoard.Text = "";
                    }
                    else
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Please enter correct arrival time. The time should be in 24 hour format";
                        SetFocus(ArrivalTimeTextBox);
                        return false;
                    }
                }
                if (DepartureTimeTextBox.Text != "")
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([0-1][0-9]|2[0-3]):([0-5][0-9])$");
                    if (regex.IsMatch(DepartureTimeTextBox.Text))
                    {
                        lblMessageBoard.Text = "";
                    }
                    else
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Please enter correct arrival time. The time should be in 24 hour format";
                        SetFocus(ArrivalTimeTextBox);
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Enter valid date (DD-MM-YYYY).";
                return false;
            }
        }
  
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void AccommodationTypeRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (AccommodationTypeRadioButtonList.SelectedValue == "1")
                {
                    VisibilityFalse();
                    Clear();
                }
                else if (AccommodationTypeRadioButtonList.SelectedValue == "2")
                {
                    VisibilityTure();
                    Clear();
                }
                else if (AccommodationTypeRadioButtonList.SelectedValue == "3")
                {
                    VisibilityTure();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    

        protected void AddToGridButton_Click(object sender, EventArgs e)
        {
            accomodation_requisitionbo RequiBoObject = new accomodation_requisitionbo();
            List<accomodation_requisitionbo> RequiBoListObject = new List<accomodation_requisitionbo>();
            try
            {
                if (!(NameOfMembersTextBoxA.Text == "" || NameOfMembersTextBoxA.Text == string.Empty))
                {
                    //Collecting the displayed values from the grid
                    RequiBoListObject = GetGridViewAllRowsControlValues();
                    //Validating the number of members and added members to the grid.
                    if (Convert.ToInt32(NumberOfMembersTextBoxA.Text) >= RequiBoListObject.Count + 1)
                    {
                        // CreateEmployeeGridViewRows(RequiBoListObject, true);
                        //---start--------get the details of the selected employee.
                        string EmployeeDetail = NameOfMembersTextBoxA.Text;
                        string[] split = EmployeeDetail.Split(new Char[] { ' ' });
                        RequiBoObject.IS_EMPLOYEE_CHECKD = false;

                        //if (RequiBoObject.Emp_name == "" || RequiBoObject.Emp_name == string.Empty)
                        //{
                        if (!(NameOfMembersTextBoxA.Text.Contains("0")))
                        {
                            RequiBoObject.Name_of_members = NameOfMembersTextBoxA.Text;
                            RequiBoObject.EMPLOYEE_NO = "";
                        }
                        else
                        {
                            RequiBoObject.EMPLOYEE_NO = split[split.Length - 1].ToString();
                            RequiBoObject.Name_of_members = EmployeeDetail.Substring(0, (EmployeeDetail.Length - RequiBoObject.EMPLOYEE_NO.Length));       // split[0].ToString();
                        }
                        //}

                        RequiBoListObject.Add(RequiBoObject);
                        //---end--------get the details of the selected employee.

                        InitialiseAddRemoveEmployeeSegments(RequiBoListObject, RequiBoListObject.Count - 1);
                        NameOfMembersTextBoxA.Text = "";
                    }
                    else
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Number of member added to the list should be equal to number of members entered.";
                    }
                }
                else
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Enter correct Name.";
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text =  ex.Message.ToString() ;
            }
         
        }

        protected void EmployeeDetailsGridViewA_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindControlsForUpdate();
        }

        protected void EmployeeDetailsGridViewA_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            accomodation_requisitionbo RequiBoObject = new accomodation_requisitionbo();
            List<accomodation_requisitionbo> RequiBoListObject = new List<accomodation_requisitionbo>();
            try
            {
                //Collecting the displayed values from the grid
                RequiBoListObject = GetGridViewAllRowsControlValues();

                foreach (GridViewRow row in EmployeeDetailsGridViewA.Rows)
                {
                    CheckBox RowToDelete = (CheckBox)row.FindControl("EmployeeNameCheckBox");
                    if (RowToDelete.Checked == true && EmployeeDetailsGridViewA.Rows.Count > 0)
                    {
                        int CheckedRowIndex = Convert.ToInt32(row.RowIndex.ToString());
                        RequiBoListObject.RemoveAt(CheckedRowIndex);
                    }
                }

                //Reassigning
                InitialiseAddRemoveEmployeeSegments(RequiBoListObject, RequiBoListObject.Count - 1);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void LocalRequisitionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.LocalRequisitionGridView, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void LocalRequisitionGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            try
            {
                DomesticTravelRequiLinkButton();
                BindControlsForUpdate();
                pnlAccomTicketbooking.Visible = true;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }
       
        protected void LocalRequisitionGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //List<accomodation_requisitionbo> RequiBoListObject = new List<accomodation_requisitionbo>();
            List<accomodation_requisitionbo> RequiBoListObject = (List<accomodation_requisitionbo>)Session["LocalAccommodationRequisitionDetails"];
            bool objSortOrder = (bool)Session["AccomRequestSortedOrder_Bool"];
            string SortExpression = e.SortExpression.ToString().Trim();
            RequiBoListObject = GridManager.SortAccommodationRequestGridView(SortExpression, objSortOrder, RequiBoListObject);
            LocalRequisitionGridView.DataSource = RequiBoListObject;
            LocalRequisitionGridView.DataBind();
            // Add negation of sort order to session for next time use.
            Session.Add("AccomRequestSortedOrder_Bool", !objSortOrder);
            // Add sorted list to the session
            Session.Add("LocalAccommodationRequisitionDetails", RequiBoListObject);
        }

        protected void LocalRequisitionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int NewPageIndex = e.NewPageIndex;
            LocalRequisitionGridView.PageIndex = e.NewPageIndex;
            List<accomodation_requisitionbo> RequiBoListObject = (List<accomodation_requisitionbo>)Session["LocalAccommodationRequisitionDetails"];
            LocalRequisitionGridView.DataSource = RequiBoListObject;
            LocalRequisitionGridView.SelectedIndex = -1;
            LocalRequisitionGridView.DataBind();
            int SelectedRow = Convert.ToInt32(ViewState["SelectedRow"], CultureInfo.InvariantCulture);
            int PagIndex = Convert.ToInt32(ViewState["PageRelatedToSelectedRow"], CultureInfo.InvariantCulture);
            if (NewPageIndex == PagIndex)
            {
                LocalRequisitionGridView.SelectedIndex = SelectedRow;
            }
        }
        
        protected void btnAccTicketBool_Click(object sender, EventArgs e)
        {
            try
            {
                ticketbookingAccombo objbo = new ticketbookingAccombo();
                ticketbookingAccomcollectionbo objcollectionbo = new ticketbookingAccomcollectionbo();
                ticketbookingbl objbl = new ticketbookingbl();
                objbo.EMPLOYEE_NO = "";

                objbo.Accommadation_req_id = (int)Session["LOCAL_ACC_REQUISITION_ID"];               
                objbo.Payment = txtPayment.Text.ToString();
                objbo.Tariff = txtTariff.Text.ToString();
                objbo.Booking_given_to = txtBookingGivenTo.Text.ToString();
                objbo.Hotel_Invoice_Num = txtHotelInvoiceNo.Text.ToString();
                if (txtBillDateA.Text == "" || txtBillDateA.Text == string.Empty)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Select Bill Date of ticket booking.";
                    return;
                }
                else
                {
                    objbo.Bill_date = DateTime.ParseExact(txtBillDateA.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                objbo.Amount = txtAmount.Text.ToString();
                objbo.created_by = User.Identity.Name;
                objbo.created_on = System.DateTime.Now;
                objbo.isActive = true;
                objbo.modified_by = User.Identity.Name;
                objbo.modified_on = System.DateTime.Now;
                objbo.CurrentStatus = "Booked";

                int result = objbl.Create_OutLocal_Accommadation_ticket_booking_details(objbo,1);
                if (result == 0)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    lblMessageBoard.Text = "Ticket booked successfully";
                    SendMail();
                    LoadLocalRequisitionGridView("0");
                    Clear();
                }
                else
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Unknown error occured";
                }
                Session["RequisitionSelectedID"] = null;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                lblMessageBoard.Text = error;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
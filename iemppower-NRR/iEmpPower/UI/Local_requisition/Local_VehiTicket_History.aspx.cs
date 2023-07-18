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

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class Local_VehiTicket_History : System.Web.UI.Page
    {
        protected MembershipUser memUser;
        public bool TravelRequestSortedOrder_Bool = false;
        public int TravelRequestGridSelectedIndexChange = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                memUser = Membership.GetUser();
                string empname = memUser.ToString();
                lblMessageBoard.Text = "";
                LoadTravelType();
                RadioButtontraveltype.SelectedValue = "1";
                Loadmtport(DropDownVehicletype); //loding the drop down  vehicle type.
                loadlocaltravelrequisitiondetails();
                Session.Add("TravelRequestSortedOrder_Bool", TravelRequestSortedOrder_Bool);
            }
        }

        void LoadTravelType()
        {
            LocalRequisitionDropDownList.Items.Clear();
            string[] EnumLocalRequisition = Enum.GetNames(typeof(LocalRequisitionType));
            foreach (string LocalRequisition in EnumLocalRequisition)
            {
                int Value = (int)Enum.Parse(typeof(LocalRequisitionType), LocalRequisition);
                ListItem Item = new ListItem(LocalRequisition, Value.ToString(CultureInfo.CurrentCulture));
                LocalRequisitionDropDownList.Items.Add(Item);
            }
            LocalRequisitionDropDownList.SelectedIndex = 0;
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
            string selectedvalue = LocalRequisitionDropDownList.SelectedValue.ToString();
            objlist = objbl.Get_Local_travel_Details(selectedvalue, empno,"9");
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

                LoadgridVehiRequi(); //ticket book history
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            string s = ToTextBox.Text;
        }

        protected void LoadgridVehiRequi()
        {
            try
            {
                int iVehi_ID = (int)Session["LOCAL_TRAVEL_REQUISITION_ID"];
                ticketbookingbl travelrequestblObj = new ticketbookingbl();
                List<ticketbookingVehibo> requisitionboList = new List<ticketbookingVehibo>();
                requisitionboList = travelrequestblObj.fun_Get_ALL_Vehi_Ticket_Booked_Details("Transport", iVehi_ID);
                grdVehicleRequisition.DataSource = requisitionboList;
                grdVehicleRequisition.DataBind();

                if (!(requisitionboList.Count > 0))
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                   // lblMessageBoard.Text = "NO DATA FOUND";
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Unknown error occured. Please contact your system administrator." + "( " + ex.Message + " )";
                return;
            }
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
            DropDownVehicletype.SelectedValue = "0";
            DropDownVehiclename.SelectedValue = "0";
            DropDownVehiclecategory.SelectedValue = "0";
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
        }
    }
}
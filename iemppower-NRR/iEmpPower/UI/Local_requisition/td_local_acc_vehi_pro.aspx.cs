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

namespace iEmpPower.UI.Local_requisition
{
    public partial class td_local_acc_vehi_pro : System.Web.UI.Page
    {
        #region "Fields"
        protected MembershipUser memUser;
        public bool AccomRequestSortedOrder_Bool = false;
        public int AccomRequestGridSelectedIndexChange = -1;
        public bool TravelRequestSortedOrder_Bool = false;
        public int TravelRequestGridSelectedIndexChange = -1;

        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Clear();
                ClearControlV();
                //accommodation
                LoadDropdowns();//Intializing drop down method should be place at first.
                LoadTravelType();
                LoadLocalRequisitionGridView("0");
                Session.Add("AccomRequestSortedOrder_Bool", AccomRequestSortedOrder_Bool);

                ////vehicle
                //RadioButtontraveltype.SelectedValue = "1";
                //Loadmtport(DropDownVehicletype); //loding the drop down  vehicle type.
                //loadlocaltravelrequisitiondetails();
                //Session.Add("TravelRequestSortedOrder_Bool", TravelRequestSortedOrder_Bool);
            }
            CheckInDateTextBox.Attributes.Add("onkeyup", "CheckInDateTextBoxChanged()");
            CheckOutDateTextBox.Attributes.Add("onkeyup", "CheckOutDateTextBoxChanged()");
            ArrivalTimeTextBox.Attributes.Add("onkeyup", "ArrivalTimeTextBoxChanged()");
            DepartureTimeTextBox.Attributes.Add("onkeyup", "DepartureTimeTextBoxChanged()");
            PickuptimeTextBox.Attributes.Add("onkeyup", "PickuptimeTextBoxChanged()");
            DroptimeTextBox.Attributes.Add("onkeyup", "DroptimeTextBoxChanged()");
        }

        protected void tcDefalut_ActiveTabChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Clear();
            ClearControlV();            

            if (tcDefalut.ActiveTab == tabAccommodation)
            {
                LoadDropdowns();//Intializing drop down method should be place at first.
                LoadTravelType();
                LoadLocalRequisitionGridView("0");
                Session.Add("AccomRequestSortedOrder_Bool", AccomRequestSortedOrder_Bool);
            }
            else
            {
                RadioButtontraveltype.SelectedValue = "1";
                Loadmtport(DropDownVehicletype); //loding the drop down  vehicle type.
                loadlocaltravelrequisitiondetails();
                Session.Add("TravelRequestSortedOrder_Bool", TravelRequestSortedOrder_Bool);
            }
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

            ////loading the Hotel Place drop down.
            //DropDownHotelPlaceCity.Items.Clear();
            //travelrequestbl objtravelrequestbl = new travelrequestbl();
            //DropDownHotelPlaceCity.DataSource = objtravelrequestbl.GetRegionName();
            //DropDownHotelPlaceCity.DataValueField = "REGION_RGION_FROM";
            //DropDownHotelPlaceCity.DataTextField = "REGION_TEXT25_FROM";
            //DropDownHotelPlaceCity.DataBind();
            //ListItem HotelPalceItem = new ListItem("Select", "0");
            //DropDownHotelPlaceCity.Items.Add(HotelNameItem);
            //DropDownHotelPlaceCity.SelectedValue = "0";

            //loading the Room category drop down.
            //DropDownRoom_category.Items.Clear();
            //DropDownRoom_category.DataSource = AccRequisitionBl.GetRoomNames();
            //DropDownRoom_category.DataValueField = "ROOM_CODE";
            //DropDownRoom_category.DataTextField = "RoomCategoryName";
            //DropDownRoom_category.DataBind();
            //ListItem RoomNameItem = new ListItem("Select", "0");
            //DropDownRoom_category.Items.Add(RoomNameItem);
            //DropDownRoom_category.SelectedValue = "0";
        }

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

                    return obj.Accommadation_req_id == LOCAL_ACC_REQUISITION_ID;
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
            CheckInDateTextBox.Text = RequisationBo.Check_in_date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            //CheckOutDateTextBox.Text = Convert.ToString(RequisationBo.Check_out_date);
            CheckOutDateTextBox.Text = RequisationBo.Check_out_date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            ArrivalTimeTextBox.Text = RequisationBo.Arrival_time;
            DepartureTimeTextBox.Text = RequisationBo.Departure_time;
            DropDownHotel_category.SelectedValue = Convert.ToString(RequisationBo.HOTEL_CAT_CODE);
            DropDownRoom_category.SelectedValue = Convert.ToString(RequisationBo.ROOM_CODE);
            DropDownHotel_name.SelectedValue = Convert.ToString(RequisationBo.HOTEL_CODE);
            ////HotelPlaceCityTextBox.Text = RequisationBo.HotelPlaceCity;
            DropDownHotelPlaceCity.SelectedValue = Convert.ToString(RequisationBo.HotelPlaceCity);
            AdditionalServicesTextBox.Text = RequisationBo.Additional_service;
            RemarksTextBox.Text = RequisationBo.Remarks;
            //Get the details of the members like member id and name
            if (RequisationBo.LOCAL_ACCOMMODATION_TYPE != "1")
            {
                VisibilityTure();
                if (RequisationBo.Number_of_members > 0)
                {
                    AccomRequiBoForMembers = travelrequestblObj.GetTheMemberDetails(RequisationBo.Accommadation_req_id);
                    NumberOfMembersTextBox.Text = RequisationBo.Number_of_members.ToString();
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

        void VisibilityTure()
        {
            NumbersOfMembers.Visible = true;
            NumberOfMembersTextBox.Text = "";
            NameOfMemberPanel.Visible = true;
            NameOfMembersTextBox.Text = "";
            EmployeeNamePanel.Visible = true;
        }
       
        void VisibilityFalse()
        {
            NumbersOfMembers.Visible = false;
            NumberOfMembersTextBox.Text = "";
            NameOfMemberPanel.Visible = false;
            NameOfMembersTextBox.Text = "";
            EmployeeNamePanel.Visible = false;
            EmployeeDetailsGridView.DataSource = null;
            EmployeeDetailsGridView.DataBind();
        }
     
        void Clear()
        {
            CheckInDateTextBox.Text = "";
            CheckOutDateTextBox.Text = "";
            ArrivalTimeTextBox.Text = "";
            DepartureTimeTextBox.Text = "";
            ////HotelPlaceCityTextBox.Text = "";
            DropDownHotelPlaceCity.SelectedIndex = -1;
            AdditionalServicesTextBox.Text = "";
            NumberOfMembersTextBox.Text = "";
            NameOfMembersTextBox.Text = "";
            RemarksTextBox.Text = "";
         //   SaveButton.Text = "Save";
            Session["LOCAL_ACC_REQUISITION_ID"] = null;
            EmployeeDetailsGridView.DataSource = null;
            EmployeeDetailsGridView.DataBind();
            DropDownHotel_category.SelectedIndex = -1;
            DropDownRoom_category.SelectedIndex = -1;
            DropDownHotel_name.SelectedIndex = -1;

            LocalRequisitionGridView.SelectedIndex = -1;
        }
   
        protected List<accomodation_requisitionbo> GetGridViewAllRowsControlValues()
        {
            List<accomodation_requisitionbo> MemberNameDetailsListObject = new List<accomodation_requisitionbo>();
            for (int i = 0; i < EmployeeDetailsGridView.Rows.Count; i++)
            {
                accomodation_requisitionbo obj = new accomodation_requisitionbo();
                CheckBox EmployeeNameCheckBox = (CheckBox)EmployeeDetailsGridView.Rows[i].FindControl("EmployeeNameCheckBox");
                Label EmployeeNameLabel = (Label)EmployeeDetailsGridView.Rows[i].FindControl("EmployeeNameLabel");
                Label EmployeeIdLabel = (Label)EmployeeDetailsGridView.Rows[i].FindControl("EmployeeIdLabel");

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
            EmployeeDetailsGridView.DataSource = objList;
            EmployeeDetailsGridView.DataBind();
        }

        void InitialiseAddRemoveEmployeeSegments(List<accomodation_requisitionbo> objCollection, int count)
        {
            List<accomodation_requisitionbo> ListObject = new List<accomodation_requisitionbo>();
            for (int i = 0; i < objCollection.Count; i++)
            {

                accomodation_requisitionbo obj = new accomodation_requisitionbo();
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

                EmployeeNameLabel.Text = objCollection[i].Name_of_members;
                EmployeeIdLabel.Text = objCollection[i].EMPLOYEE_NO;
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

            requisitionboList = travelrequestblObj.Get_Local_Accommodation_Separate_Details("", "3");
            
            if (requisitionboList == null || requisitionboList.Count == 0)
            {
                LocalRequisitionGridView.Visible = false;
                LocalRequisitionGridView.DataSource = null;
                return;
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
            if (Convert.ToInt32(NumberOfMembersTextBox.Text) == EmployeeDetailsGridView.Rows.Count)
            {
                return true;
            }
            return false;
        }

        bool ValidateControlV()
        {
            if (PickuptimeTextBox.Text != "")
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex((@"^([0-1][0-9]|2[0-3]):([0-5][0-9])$"));
                if (regex.IsMatch(PickuptimeTextBox.Text))
                {
                    lblMessageBoard.Text = "";
                }
                else
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Please enter correct arrival time. The time should be in 24 hour format";
                    SetFocus(PickuptimeTextBox);
                    return false;
                }
            }
            if (DroptimeTextBox.Text != "")
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex((@"^([0-1][0-9]|2[0-3]):([0-5][0-9])$"));
                if (regex.IsMatch(DroptimeTextBox.Text))
                {
                    lblMessageBoard.Text = "";

                }
                else
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Please enter correct arrival time. The time should be in 24 hour format";
                    SetFocus(DroptimeTextBox);
                    return false;
                }
            }
            return true;
        }
        bool ValidateControl()
        {
            try
            {
                DateTime dt = DateTime.ParseExact(CheckInDateTextBox.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime dt1 = DateTime.ParseExact(CheckOutDateTextBox.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
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

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidateControl())
            {
                memUser = Membership.GetUser();
                string member_name = string.Empty;
                accomodation_requisitionbo objbo = new accomodation_requisitionbo();
                local_accommodation_requisitionbl objbl = new local_accommodation_requisitionbl();
                try
                {
                    GridViewRow grdrow = (GridViewRow)LocalRequisitionGridView.SelectedRow;

                    objbo.EMPLOYEE_NO = grdrow.Cells[6].Text.ToString();
                    objbo.Check_in_date = DateTime.ParseExact(CheckInDateTextBox.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture); 
                    objbo.Check_out_date = DateTime.ParseExact(CheckOutDateTextBox.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture); 
                    objbo.Arrival_time = ArrivalTimeTextBox.Text;
                    objbo.Departure_time = DepartureTimeTextBox.Text;
                    ////objbo.HotelPlaceCity = HotelPlaceCityTextBox.Text;
                    objbo.HotelPlaceCity = DropDownHotelPlaceCity.SelectedValue;
                    objbo.HOTEL_CAT_CODE = DropDownHotel_category.SelectedValue;
                    objbo.ROOM_CODE = DropDownRoom_category.SelectedValue;
                    objbo.RoomCategoryName = DropDownRoom_category.SelectedItem.Text.ToString();
                    objbo.HOTEL_CODE = DropDownHotel_name.SelectedValue;
                    objbo.Additional_service = AdditionalServicesTextBox.Text;
                    objbo.LOCAL_ACCOMMODATION_TYPE = AccommodationTypeRadioButtonList.SelectedValue.ToString();
                    if (AccommodationTypeRadioButtonList.SelectedValue != "1")
                    {
                        if (Validated())
                        {
                            objbo.Number_of_members = Convert.ToInt32(NumberOfMembersTextBox.Text);
                            for (int i = 0; i < EmployeeDetailsGridView.Rows.Count; i++)
                            {
                                Label ddlMTPort = (Label)EmployeeDetailsGridView.Rows[i].FindControl("EmployeeIdLabel");
                                Label lblName = (Label)EmployeeDetailsGridView.Rows[i].FindControl("EmployeeNameLabel");
                                if (Convert.ToInt32(NumberOfMembersTextBox.Text) > 0)
                                {
                                    if (member_name == string.Empty || member_name.Length == 0)
                                    {
                                        if (!(ddlMTPort.Text == string.Empty || ddlMTPort.Text == ""))
                                        {
                                            member_name = ddlMTPort.Text;
                                        }
                                        else
                                        {
                                            member_name = lblName.Text;
                                        }
                                    }
                                    else
                                    {
                                        if (!(ddlMTPort.Text == string.Empty || ddlMTPort.Text == ""))
                                        {
                                            member_name = member_name + "|" + ddlMTPort.Text;
                                        }
                                        else
                                        {
                                            member_name = member_name + "|" + lblName.Text;
                                        } 
                                    }
                                }
                                objbo.Name_of_members = member_name;
                            }
                            objbo.Number_of_members = Convert.ToInt16(EmployeeDetailsGridView.Rows.Count);
                        }
                        else
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            lblMessageBoard.Text = "Number of member added to the list should be equal to number of members entered.";
                            return;
                        }
                    }
                    objbo.current_status = Convert.ToString("6");
                    objbo.Remarks = RemarksTextBox.Text;
                    objbo.isactive = true;
                    objbo.CRAETEDBY = User.Identity.Name;
                    objbo.CREATEDON = System.DateTime.Now;

                    objbo.local_acc_req_id =Convert.ToInt32( grdrow.Cells[0].Text.ToString());
                        int result = objbl.Update_Local_accomodation_requisitionbl(objbo);
                        if (result == 0)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = "Local requisition Approved successfully.";
                            Clear();
                            // return;
                        }
                        else
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            lblMessageBoard.Text = "Unknown Error.";
                            //  return;
                        }

                    LoadLocalRequisitionGridView("0");
                    LocalRequisitionDropDownList.SelectedValue = "0";
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                //if (txtHOD_RemarksAcc.Text == "" || txtHOD_RemarksAcc.Text == string.Empty)
                //{
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    lblMessage.Text = "Please enter Remarks";
                //    return;
                //}

                int count = UpdateAccommRequisitionStatus((int)ReuisitionStatus.ProposalReject);
                if (count > 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = " Proposal Rejected successfully.";
                    //txtHOD_RemarksAcc.Text = "";

                    //ClearcontrolAccomm();
                    //LoadProposalAccommoDetails();
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Unknown error occured.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Error. Please contact admministrator " + ex.Message;
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
                if (!(NameOfMembersTextBox.Text == "" || NameOfMembersTextBox.Text == string.Empty))
                {
                    //Collecting the displayed values from the grid
                    RequiBoListObject = GetGridViewAllRowsControlValues();
                    //Validating the number of members and added members to the grid.
                    if (Convert.ToInt32(NumberOfMembersTextBox.Text) >= RequiBoListObject.Count + 1)
                    {
                        // CreateEmployeeGridViewRows(RequiBoListObject, true);
                        //---start--------get the details of the selected employee.
                        string EmployeeDetail = NameOfMembersTextBox.Text;
                        string[] split = EmployeeDetail.Split(new Char[] { ' ' });
                        RequiBoObject.IS_EMPLOYEE_CHECKD = false;

                        //if (RequiBoObject.Emp_name == "" || RequiBoObject.Emp_name == string.Empty)
                        //{
                        if (!(NameOfMembersTextBox.Text.Contains("0")))
                        {
                            RequiBoObject.Name_of_members = NameOfMembersTextBox.Text;
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
                        NameOfMembersTextBox.Text = "";
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

        protected void EmployeeDetailsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindControlsForUpdate();
        }

        protected void EmployeeDetailsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            accomodation_requisitionbo RequiBoObject = new accomodation_requisitionbo();
            List<accomodation_requisitionbo> RequiBoListObject = new List<accomodation_requisitionbo>();
            try
            {
                //Collecting the displayed values from the grid
                RequiBoListObject = GetGridViewAllRowsControlValues();

                foreach (GridViewRow row in EmployeeDetailsGridView.Rows)
                {
                    CheckBox RowToDelete = (CheckBox)row.FindControl("EmployeeNameCheckBox");
                    if (RowToDelete.Checked == true && EmployeeDetailsGridView.Rows.Count > 0)
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

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                LoadLocalRequisitionGridView(LocalRequisitionDropDownList.SelectedValue.ToString());
                Clear();
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
                BindControlsForUpdate();
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

        int UpdateAccommRequisitionStatus(int UpdateStatus)
        {
            memUser = Membership.GetUser();

            int iResult = 0;

            //Accommodation Approval
            GridViewRow grdrowAcc = LocalRequisitionGridView.SelectedRow;
            accomodation_requisitionbo objbo = new accomodation_requisitionbo();
            accomodation_requisitionbl objbl = new accomodation_requisitionbl();

            objbo.Accommadation_req_id = Convert.ToInt32(grdrowAcc.Cells[0].Text.ToString().Trim());
          //  objbo.Remarks = grdrowAcc.Cells[16].Text.ToString().Trim() + "|" +  txtHOD_RemarksAcc.Text;
            objbo.MODIFIEDBY = memUser.UserName;

            if (UpdateStatus == (int)ReuisitionStatus.NotBooked)
            {
                //get the enum value of 'New' status
                objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.NotBooked.ToString()));
                iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
            }
            else if (UpdateStatus == (int)ReuisitionStatus.ProposalReject)
            {
                //get the enum value of 'New' status
                objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.ProposalReject.ToString()));
                iResult = objbl.HODStatusUpdateSeparateAccomm(objbo, "local");
            }

            return iResult;
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

        void loadlocaltravelrequisitiondetails()
        {
            local_travel_requisitionbl objbl = new local_travel_requisitionbl();
            List<vehicle_requisitionbo> objlist = new List<vehicle_requisitionbo>();

            objlist = objbl.Get_Local_Vehicle_Separate_Details("", "3");
          
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

        protected void Travel_type_selection(object sender, EventArgs e)
        {
            if (RadioButtontraveltype.SelectedValue != "1")
            {
                Panelmember.Visible = true;
            }
            else
            {
                Panelmember.Visible = false;
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

        protected void DropDownVehicletype_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            if (DropDownVehicletype.SelectedValue != "")
            {
                loadmcate(DropDownVehiclecategory, DropDownVehicletype.SelectedValue);
                DropDownVehiclecategory.Enabled = true;
                DropDownVehiclename.Enabled = false;
            }
            else
            {
                DropDownVehiclecategory.Enabled = false;
            }
        }

        protected void DropDownVehiclecategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownVehiclecategory.SelectedValue != "")
            {
                loadvehiclename(DropDownVehiclename, DropDownVehiclecategory.SelectedValue);
                DropDownVehiclename.Enabled = true;
            }
            else
            {
                DropDownVehiclename.Enabled = false;
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

        protected void btnVehiApprove_Click(object sender, EventArgs e)
        {
            if (ValidateControlV())
            {
                string member_name = string.Empty;
                vehicle_requisitionbo objbo = new vehicle_requisitionbo();
                local_travel_requisitionbl objbl = new local_travel_requisitionbl();
                memUser = Membership.GetUser();
                GridViewRow grdrow = (GridViewRow)LocalTravelRequisitionGridView.SelectedRow;

                objbo.EMPLOYEE_NO = grdrow.Cells[6].Text.ToString();
                objbo.TRAVEL_TYPE = RadioButtontraveltype.SelectedValue;
                objbo.Departure_from = FromTextBox.Text;
                objbo.Destination_to = ToTextBox.Text;
                objbo.Vehicle_type = DropDownVehicletype.SelectedValue;
                objbo.Vehicle_category = DropDownVehiclecategory.SelectedValue;
                objbo.Vehicle_name = DropDownVehiclename.SelectedValue;
                objbo.Pickup_time = PickuptimeTextBox.Text;
                objbo.Pickup_address = PickupaddressTextBox.Text;
                objbo.Drop_time = DroptimeTextBox.Text;
                objbo.Drop_address = DropaddressTextBox.Text;
                objbo.CRAETEDBY = User.Identity.Name;
                objbo.CREATEDON = System.DateTime.Now;
                objbo.Purpose_of_travel = PurposeoftravelTextBox.Text;
                objbo.IsActive = true;
                objbo.current_status = Convert.ToString((int)ReuisitionStatus.New);
                objbo.Date_of_travel = DateTime.ParseExact(CheckInDateTextBoxV.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                objbo.To_Date = DateTime.ParseExact(CheckOutDateTextBoxV.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                objbo.current_status = "6";

                if (RadioButtontraveltype.SelectedValue != "1")
                {
                    objbo.Number_of_members = Convert.ToInt32(NumberofmembersTextBoxV.Text);
                    for (int i = 0; i < EmployeeDetailsGridViewV.Rows.Count; i++)
                    {
                        //After binding the gridview, we can then extract and fill the DropDownList with Data 
                        Label ddlMTPort = (Label)EmployeeDetailsGridViewV.Rows[i].FindControl("EmployeeIdLabel");
                        Label lblName = (Label)EmployeeDetailsGridViewV.Rows[i].FindControl("EmployeeNameLabel");
                        if (Convert.ToInt32(NumberofmembersTextBoxV.Text) > 0)
                        {
                            if (member_name == string.Empty || member_name.Length == 0)
                            {
                                if (!(ddlMTPort.Text == string.Empty || ddlMTPort.Text == ""))
                                {
                                    member_name = ddlMTPort.Text;
                                }
                                else
                                {
                                    member_name = lblName.Text;
                                }

                            }
                            else
                            {
                                if (!(ddlMTPort.Text == string.Empty || ddlMTPort.Text == ""))
                                {
                                    member_name = member_name + "|" + ddlMTPort.Text;
                                }
                                else
                                {
                                    member_name = member_name + "|" + lblName.Text;
                                }
                            }
                        }

                    }
                    objbo.Name_of_members = member_name;
                    objbo.Number_of_members = Convert.ToInt16(EmployeeDetailsGridViewV.Rows.Count);
                }
                objbo.remarks = RemarksTextBox.Text;
                 objbo.local_travel_req_id = Convert.ToInt32(grdrow.Cells[0].Text.ToString());
                    //--start-------filtering the selected record from the session.
                    vehicle_requisitionbo VehicleRequiBo = new vehicle_requisitionbo();
                    //local_travel_requisitionbl objbl = new local_travel_requisitionbl();
                    List<vehicle_requisitionbo> VehicleRequiBoList = new List<vehicle_requisitionbo>();
                    VehicleRequiBoList = (List<vehicle_requisitionbo>)Session["LocalTravelDetails"];
                    VehicleRequiBo = VehicleRequiBoList.Find(delegate(vehicle_requisitionbo ObjVehRequi)
                    {
                        return ObjVehRequi.local_travel_req_id == objbo.local_travel_req_id;
                    });
                    //--end-------filtering the selected record from the session.
                    objbo.EMPLOYEE_NO = VehicleRequiBo.EMPLOYEE_NO;
                    if (objbo.Name_of_members == "0")
                    {
                        objbo.Name_of_members = null;
                    }
                    int result = objbl.Update_local_requisition_details(objbo);
                    if (result == 0)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = "Local Requisition Updated successfully.";
                        loadlocaltravelrequisitiondetails();
                        ClearControlV();
                       // return;
                    }
                    else
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Unknown Error.";
                        return;
                    }
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
                if (RadioButtontraveltype.SelectedValue == "1")
                {
                    VisibilityFalseV();
                    //   ClearControl();
                }
                else
                {
                    VisibilityTureV();
                    //    ClearControl();
                }
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
                CheckInDateTextBoxV.Text = RequisationBo.Duration_from.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                //CheckOutDateTextBox.Text = Convert.ToString(RequisationBo.Check_out_date);
                CheckOutDateTextBoxV.Text = RequisationBo.Duration_to.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                CheckInDateTextBoxV.Enabled = false;
                CheckOutDateTextBoxV.Enabled = false;
                CheckInDateImageButton.Enabled = false;
                CheckOutDateImageButton.Enabled = false;

                if (RequisationBo.Number_of_members > 0)
                {
                    Panelmember.Visible = true;
                    vehicle_requisitionbo objbo = new vehicle_requisitionbo();
                    local_travel_requisitionbl objbl = new local_travel_requisitionbl();
                    List<vehicle_requisitionbo> objlist = new List<vehicle_requisitionbo>();

                    objlist = objbl.Get_Local_travel_Group_members(local_travel_req_id);
                    NumberofmembersTextBoxV.Text = RequisationBo.Number_of_members.ToString();
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

        void ClearControlV()
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
            NameOfMembersTextBoxV.Text = "";
            NumberofmembersTextBoxV.Text = "";
            PurposeoftravelTextBox.Text = "";
            RemarksTextBox.Text = "";
            CheckInDateTextBoxV.Text = "";
            CheckOutDateTextBoxV.Text = "";
            CheckInDateTextBoxV.Enabled = true;
            CheckOutDateTextBoxV.Enabled = true;
            CheckInDateImageButton.Enabled = true;
            CheckOutDateImageButton.Enabled = true;

            Session["LOCAL_TRAVEL_REQUISITION_ID"] = null;
            EmployeeDetailsGridViewV.DataSource = null;
            EmployeeDetailsGridViewV.DataBind();
            LocalTravelRequisitionGridView.SelectedIndex = -1;
        }

        protected List<vehicle_requisitionbo> GetGridViewAllRowsControlValuesV()
        {
            List<vehicle_requisitionbo> MemberNameDetailsListObject = new List<vehicle_requisitionbo>();
            for (int i = 0; i < EmployeeDetailsGridViewV.Rows.Count; i++)
            {
                vehicle_requisitionbo obj = new vehicle_requisitionbo();
                CheckBox EmployeeNameCheckBox = (CheckBox)EmployeeDetailsGridViewV.Rows[i].FindControl("EmployeeNameCheckBox");
                Label EmployeeNameLabel = (Label)EmployeeDetailsGridViewV.Rows[i].FindControl("EmployeeNameLabel");
                Label EmployeeIdLabel = (Label)EmployeeDetailsGridViewV.Rows[i].FindControl("EmployeeIdLabel");

                if (EmployeeNameCheckBox.Checked)
                {
                    obj.IS_EMPLOYEE_CHECKD = true;
                }
                else
                {
                    obj.IS_EMPLOYEE_CHECKD = false;
                }
                obj.Emp_name = EmployeeNameLabel.Text;
                obj.EMPLOYEE_NO = EmployeeIdLabel.Text;

                MemberNameDetailsListObject.Add(obj);
            }
            return MemberNameDetailsListObject;
        }

        protected void AddToGridButtonV_Click(object sender, EventArgs e)
        {
            vehicle_requisitionbo RequiBoObject = new vehicle_requisitionbo();
            List<vehicle_requisitionbo> RequiBoListObject = new List<vehicle_requisitionbo>();
            try
            {
                if (!(NameOfMembersTextBoxV.Text == "" || NameOfMembersTextBoxV.Text == string.Empty))
                {
                    //Collecting the displayed values from the grid
                    RequiBoListObject = GetGridViewAllRowsControlValuesV();
                    //Validating the number of members and added members to the grid.
                    if (Convert.ToInt32(NumberofmembersTextBoxV.Text) >= RequiBoListObject.Count + 1)
                    {
                        // CreateEmployeeGridViewRows(RequiBoListObject, true);
                        //---start--------get the details of the selected employee.
                        string EmployeeDetail = NameOfMembersTextBoxV.Text;
                        string[] split = EmployeeDetail.Split(new Char[] { ' ' });
                        RequiBoObject.IS_EMPLOYEE_CHECKD = false;

                        //if (RequiBoObject.Emp_name == "" || RequiBoObject.Emp_name == string.Empty)
                        //{
                        if (!(NameOfMembersTextBox.Text.Contains("0")))
                        {
                            RequiBoObject.Emp_name = NameOfMembersTextBox.Text;
                            RequiBoObject.EMPLOYEE_NO = "";
                        }
                        else
                        {
                            RequiBoObject.EMPLOYEE_NO = split[split.Length - 1].ToString();
                            RequiBoObject.Emp_name = EmployeeDetail.Substring(0, (EmployeeDetail.Length - RequiBoObject.EMPLOYEE_NO.Length));       // split[0].ToString();
                        }
                        //}

                        RequiBoListObject.Add(RequiBoObject);
                        //---end--------get the details of the selected employee.

                        InitialiseAddRemoveEmployeeSegments(RequiBoListObject, RequiBoListObject.Count - 1);
                        NameOfMembersTextBoxV.Text = "";
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
                lblMessageBoard.Text = ex.Message.ToString();
            }

        }

        void InitialiseAddRemoveEmployeeSegments(List<vehicle_requisitionbo> objCollection, int count)
        {
            List<vehicle_requisitionbo> ListObject = new List<vehicle_requisitionbo>();
            for (int i = 0; i < objCollection.Count; i++)
            {

                vehicle_requisitionbo obj = new vehicle_requisitionbo();
                ListObject.Add(obj);
            }
            EmployeeDetailsGridViewV.DataSource = ListObject;
            EmployeeDetailsGridViewV.DataBind();

            for (int i = 0; i < objCollection.Count; i++)
            {
                CheckBox EmployeeNameCheckBox = (CheckBox)EmployeeDetailsGridViewV.Rows[i].FindControl("EmployeeNameCheckBox");
                Label EmployeeNameLabel = (Label)EmployeeDetailsGridViewV.Rows[i].FindControl("EmployeeNameLabel");
                Label EmployeeIdLabel = (Label)EmployeeDetailsGridViewV.Rows[i].FindControl("EmployeeIdLabel");
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

        protected void EmployeeDetailsGridViewV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void EmployeeDetailsGridViewV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            vehicle_requisitionbo RequiBoObject = new vehicle_requisitionbo();
            List<vehicle_requisitionbo> RequiBoListObject = new List<vehicle_requisitionbo>();
            //Collecting the displayed values from the grid
            RequiBoListObject = GetGridViewAllRowsControlValuesV();

            foreach (GridViewRow row in EmployeeDetailsGridViewV.Rows)
            {
                CheckBox RowToDelete = (CheckBox)row.FindControl("EmployeeNameCheckBox");
                if (RowToDelete.Checked == true && EmployeeDetailsGridViewV.Rows.Count > 0)
                {
                    int CheckedRowIndex = Convert.ToInt32(row.RowIndex.ToString());
                    RequiBoListObject.RemoveAt(CheckedRowIndex);
                }
            }

            //Reassigning
            InitialiseAddRemoveEmployeeSegments(RequiBoListObject, RequiBoListObject.Count - 1);
        }

        protected void btnVehiReject_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                //if (txtHOD_RemarksVehicle.Text == "" || txtHOD_RemarksVehicle.Text == string.Empty)
                //{
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    lblMessage.Text = "Please enter Remarks";
                //    return;
                //}

                int count = UpdateVehiRequisitionStatus((int)ReuisitionStatus.ProposalReject);
                if (count > 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = " Proposal Rejected successfully.";
                    //txtHOD_RemarksAcc.Text = "";
                    //ClearcontrolAccomm();
                    //LoadProposalVehiDetails();
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Unknown error occured.";
                }

            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Error. Please contact admministrator " + ex.Message;
            }
        }

        int UpdateVehiRequisitionStatus(int UpdateStatus)
        {
            memUser = Membership.GetUser();

            int iResult = 0;

            //Vehicle Approval
            GridViewRow grdrowVehi = LocalTravelRequisitionGridView.SelectedRow;

            vehicle_requisitionbo objbo = new vehicle_requisitionbo();
            vehicle_requisitionbl objbl = new vehicle_requisitionbl();

            objbo.Vehicle_req_id = Convert.ToInt32(grdrowVehi.Cells[0].Text.ToString().Trim());
      //      objbo.remarks = grdrowVehi.Cells[18].Text.ToString().Trim() + "|" + txtHOD_RemarksVehicle.Text;
            objbo.MODIFIEDBY = memUser.UserName;

            if (UpdateStatus == (int)ReuisitionStatus.NotBooked)
            {
                //get the enum value of 'New' status
                objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.NotBooked.ToString()));
                iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
            }
            else if (UpdateStatus == (int)ReuisitionStatus.ProposalReject)
            {
                //get the enum value of 'New' status
                objbo.current_status = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.ProposalReject.ToString()));
                iResult = objbl.HODStatusUpdateSeparateVehi(objbo, "local");
            }

            return iResult;
        }

        protected void SearchButtonV_Click(object sender, EventArgs e)
        {
            try
            {
                loadlocaltravelrequisitiondetails();
                ClearControlV();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void RadioButtontraveltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RadioButtontraveltype.SelectedValue == "1")
                {
                    VisibilityFalseV();
                    ClearControlV();
                }
                else
                {
                    VisibilityTureV();
                    ClearControlV();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        void VisibilityFalseV()
        {
            Panelmember.Visible = false;
            EmployeeDetailsGridViewV.DataSource = null;
            EmployeeDetailsGridViewV.DataBind();
        }

        void VisibilityTureV()
        {
            Panelmember.Visible = true;
        }
       



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using System.Globalization;
using System.Web.Security;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using System.Text.RegularExpressions;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class local_accommodation_requisition : System.Web.UI.Page
    {
        #region "Fields"
        protected MembershipUser memUser;
        public bool AccomRequestSortedOrder_Bool = false;
        public int  AccomRequestGridSelectedIndexChange = -1;
        #endregion

        #region "Helpers"

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
            CheckInDateTextBox.Text =RequisationBo.Check_in_date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            //CheckOutDateTextBox.Text = Convert.ToString(RequisationBo.Check_out_date);
            CheckOutDateTextBox.Text = RequisationBo.Check_out_date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            ArrivalTimeTextBox.Text = RequisationBo.Arrival_time;
            DepartureTimeTextBox.Text = RequisationBo.Departure_time;
            DropDownHotel_category.SelectedValue = Convert.ToString(RequisationBo.HOTEL_CAT_CODE);
            DropDownRoom_category.SelectedValue = Convert.ToString(RequisationBo.ROOM_CODE);
            DropDownHotel_name.SelectedValue = Convert.ToString(RequisationBo.HOTEL_CODE);
            ////HotelPlaceCityTextBox.Text = RequisationBo.HotelPlaceCity;
            DropDownHotelPlaceCity.SelectedValue =Convert.ToString(RequisationBo.HotelPlaceCity);
            ////AdditionalServicesTextBox.Text = RequisationBo.Additional_service;
            DropDownAdditionalServices.Text = Convert.ToString(RequisationBo.Additional_service);
            RemarksTextBox.Text = RequisationBo.Remarks;
            //Get the details of the members like member id and name
            if (RequisationBo.LOCAL_ACCOMMODATION_TYPE != "1")
            {
                VisibilityTure();
                if (RequisationBo.Number_of_members > 0)
                {
                    AccomRequiBoForMembers = travelrequestblObj.GetTheMemberDetails(RequisationBo.local_acc_req_id);
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
            NameOfMemberPanel.Visible =true;
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
            ////AdditionalServicesTextBox.Text = "";
            DropDownAdditionalServices.SelectedIndex = -1;
            NumberOfMembersTextBox.Text = "";
            NameOfMembersTextBox.Text = "";
            RemarksTextBox.Text = "";
            SaveButton.Text = "Save";
            Session["LOCAL_ACC_REQUISITION_ID"] = null;
            EmployeeDetailsGridView.DataSource = null;
            EmployeeDetailsGridView.DataBind();
            DropDownHotel_category.SelectedIndex = -1;
            DropDownRoom_category.SelectedIndex = -1;
            DropDownHotel_name.SelectedIndex = -1;
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
            requisitionboList = travelrequestblObj.LoadAccommodationRequestionDetails(LOCAL_REQUISITION_TYPE, EmployeeNo,"1");
           
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

        #endregion

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdowns();//Intializing drop down method should be place at first.
                LoadTravelType();
                LoadLocalRequisitionGridView("0");
                Session.Add("AccomRequestSortedOrder_Bool", AccomRequestSortedOrder_Bool);
            }
            CheckInDateTextBox.Attributes.Add("onkeyup", "CheckInDateTextBoxChanged()");
            CheckOutDateTextBox.Attributes.Add("onkeyup", "CheckOutDateTextBoxChanged()");
            ArrivalTimeTextBox.Attributes.Add("onkeyup", "ArrivalTimeTextBoxChanged()");
            DepartureTimeTextBox.Attributes.Add("onkeyup", "DepartureTimeTextBoxChanged()");
        }

        void LoadDropdowns()
        {
            accomodation_requisitionbl AccRequisitionBl = new accomodation_requisitionbl();
            accomodation_requisitionbo AccRequisitionBo = new accomodation_requisitionbo();

            DropDownHotel_category.Items.Clear();
            //loading the HotelCategory drop down.
            DropDownHotel_category.DataSource = AccRequisitionBl.GetHotelCategory();
            DropDownHotel_category.DataValueField = "HOTEL_CAT_CODE";
            DropDownHotel_category.DataTextField = "HOTEL_CATEGORY";
            DropDownHotel_category.DataBind();
            ListItem Item = new ListItem("Select", "0");
            DropDownHotel_category.Items.Add(Item);
            DropDownHotel_category.SelectedValue = "0";

            //loading the Hotel name drop down.
            DropDownHotel_name.Items.Clear();
            DropDownHotel_name.DataSource = AccRequisitionBl.GetHotelNames();
            DropDownHotel_name.DataValueField = "HOTEL_CODE";
            DropDownHotel_name.DataTextField = "HOTEL_NAME";
            DropDownHotel_name.DataBind();
            ListItem HotelNameItem = new ListItem("Select", "0");
            DropDownHotel_name.Items.Add(HotelNameItem);
            DropDownHotel_name.SelectedValue = "0";


            
            
            //loading the Hotel Place drop down.
            DropDownHotelPlaceCity.Items.Clear();
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            DropDownHotelPlaceCity.DataSource = objtravelrequestbl.GetRegionName();
            DropDownHotelPlaceCity.DataValueField = "REGION_RGION_FROM";
            DropDownHotelPlaceCity.DataTextField = "REGION_TEXT25_FROM";
            DropDownHotelPlaceCity.DataBind();
            ListItem HotelPalceItem = new ListItem("Select", "0");
            DropDownHotelPlaceCity.Items.Add(HotelPalceItem);
            DropDownHotelPlaceCity.SelectedValue = "0";

            //loading the AdditionalServices drop down.
            DropDownAdditionalServices.Items.Clear();
           // travelrequestbl objtravelrequestbl = new travelrequestbl();
            DropDownAdditionalServices.DataSource = objtravelrequestbl.GetModeofpayment();
            DropDownAdditionalServices.DataValueField = "mode";
            DropDownAdditionalServices.DataTextField = "mode";
            DropDownAdditionalServices.DataBind();
            ListItem AdditionalServicesItem = new ListItem("Select", "0");
            DropDownAdditionalServices.Items.Add(AdditionalServicesItem);
            DropDownAdditionalServices.SelectedValue = "0";

            //loading the Room category drop down.
            DropDownRoom_category.Items.Clear();
            DropDownRoom_category.DataSource = AccRequisitionBl.GetRoomNames();
            DropDownRoom_category.DataValueField = "ROOM_CODE";
            DropDownRoom_category.DataTextField = "RoomCategoryName";
            DropDownRoom_category.DataBind();
            ListItem RoomNameItem = new ListItem("Select", "0");
            DropDownRoom_category.Items.Add(RoomNameItem);
            DropDownRoom_category.SelectedValue = "0";
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
                    objbo.EMPLOYEE_NO = memUser.ToString();
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
                    ////objbo.Additional_service = AdditionalServicesTextBox.Text;
                    objbo.Additional_service = DropDownAdditionalServices.SelectedItem.Text.ToString();
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
                    objbo.current_status = Convert.ToString((int)ReuisitionStatus.New);
                    objbo.Remarks = RemarksTextBox.Text;
                    objbo.isactive = true;
                    objbo.CRAETEDBY = User.Identity.Name;
                    objbo.CREATEDON = System.DateTime.Now;
                    if (SaveButton.Text == "Save")
                    {
                        int result = objbl.Create_Local_accomodation_requisitionbl(objbo);
                        if (result == 0)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = "Local requisition created successfully.";
                            Clear();
                            // return;
                        }
                        else
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            lblMessageBoard.Text = "Unknown Error.";
                            //  return;
                        }
                    }
                    else if (SaveButton.Text == "Update")
                    {
                        objbo.local_acc_req_id = (int)Session["LOCAL_ACC_REQUISITION_ID"];
                        int result = objbl.Update_Local_accomodation_requisitionbl(objbo);
                        if (result == 0)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = "Local requisition updated successfully.";
                            Clear();
                            // return;
                        }
                        else
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            lblMessageBoard.Text = "Unknown Error.";
                            //  return;
                        }
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
        #endregion

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
                SaveButton.Text = "Update";
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
    }
}
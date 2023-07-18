using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using System.Web.Security;
using System.Globalization;
using iEmpPower.Old_App_Code.iEmpPowerBL.Local_requisition;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using System.Text.RegularExpressions;
namespace iEmpPower.UI.Benefits_Payment
{
    public partial class local_travel_requisition : System.Web.UI.Page
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
                Session.Add("TravelRequestSortedOrder_Bool",TravelRequestSortedOrder_Bool);
            }
            PickuptimeTextBox.Attributes.Add("onkeyup", "PickuptimeTextBoxChanged()");
            DroptimeTextBox.Attributes.Add("onkeyup", "DroptimeTextBoxChanged()");
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
            vehicle_requisitionbo objbo = new vehicle_requisitionbo();
            local_travel_requisitionbl objbl = new local_travel_requisitionbl();
            List<vehicle_requisitionbo> objlist = new List<vehicle_requisitionbo>();
            memUser = Membership.GetUser();
            string empno = memUser.ToString();
            string selectedvalue=LocalRequisitionDropDownList.SelectedValue.ToString();
            objlist = objbl.Get_Local_travel_Details(selectedvalue, empno, "1");
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
                loadvehiclename(DropDownVehiclename, DropDownVehicletype.SelectedValue);
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

        bool ValidateControl()
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
      
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidateControl())
            {
                string member_name = string.Empty;
                vehicle_requisitionbo objbo = new vehicle_requisitionbo();
                local_travel_requisitionbl objbl = new local_travel_requisitionbl();
                memUser = Membership.GetUser();
                objbo.EMPLOYEE_NO = memUser.ToString();
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
                objbo.Date_of_travel = DateTime.ParseExact(CheckInDateTextBox.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                objbo.To_Date = DateTime.ParseExact(CheckOutDateTextBox.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture); 
                  
                if (RadioButtontraveltype.SelectedValue != "1")
                {
                    objbo.Number_of_members = Convert.ToInt32(NumberofmembersTextBox.Text);
                    for (int i = 0; i < EmployeeDetailsGridView.Rows.Count; i++)
                    {
                        //After binding the gridview, we can then extract and fill the DropDownList with Data 
                        Label ddlMTPort = (Label)EmployeeDetailsGridView.Rows[i].FindControl("EmployeeIdLabel");
                        Label lblName = (Label)EmployeeDetailsGridView.Rows[i].FindControl("EmployeeNameLabel");
                        if (Convert.ToInt32(NumberofmembersTextBox.Text) > 0)
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
                    objbo.Number_of_members = Convert.ToInt16(EmployeeDetailsGridView.Rows.Count);
                }
                objbo.remarks = RemarksTextBox.Text;
                if (SaveButton.Text != "Update")
                {
                    int result = objbl.Create_Local_travel_requisitionbl(objbo);
                    if (result == 0)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = "Local Requisition created successfully.";
                        loadlocaltravelrequisitiondetails();
                        ClearControl();
                        return;
                    }
                    else
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Unknown Error.";
                        return;
                    }
                }
                else
                {
                    objbo.local_travel_req_id = Convert.ToInt32(Session["LOCAL_TRAVEL_REQUISITION_ID"]);
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
                        ClearControl();
                        return;
                    }
                    else
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Unknown Error.";
                        return;
                    }
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
                    VisibilityFalse();
                 //   ClearControl();
                }
                else
                {
                    VisibilityTure();
                //    ClearControl();
                }
                FromTextBox.Text = RequisationBo.Departure_from;
                ToTextBox.Text = RequisationBo.Destination_to;

                DropDownVehicletype.SelectedValue = RequisationBo.VehicleTypeId;
                if (RequisationBo.VehicleTypeId != "0")
                {
                    loadmcate(DropDownVehiclecategory, RequisationBo.VehicleTypeId);
                    DropDownVehiclecategory.Enabled = true;
                     DropDownVehiclecategory.SelectedValue = RequisationBo.VehicleCategoryId ;
                }
                if (RequisationBo.VehicleCategoryId != "0")
                {
                    //DropDownVehiclename.Items.Clear();
                    //loadvehiclename(DropDownVehiclename, DropDownVehiclecategory.SelectedValue);
                    //DropDownVehiclename.Enabled = true;
                    //DropDownVehiclename.SelectedValue = RequisationBo.VehicleId;


                    //monica start

                    DropDownVehiclename.Items.Clear();
                    loadvehiclename(DropDownVehiclename, DropDownVehicletype.SelectedValue);
                    DropDownVehiclename.Enabled = true;
                    DropDownVehiclename.SelectedValue = RequisationBo.VehicleId;

                    //monica end
                }
            
                PickuptimeTextBox.Text = RequisationBo.Pickup_time;
                PickupaddressTextBox.Text = RequisationBo.Pickup_address;
                DroptimeTextBox.Text = RequisationBo.Drop_time;
                DropaddressTextBox.Text = RequisationBo.Drop_address;
                PurposeoftravelTextBox.Text = RequisationBo.Purpose_of_travel;
              
                RemarksTextBox.Text = RequisationBo.remarks;
                //PurposeoftravelTextBox.Text = RequisationBo.Purpose_of_travel;
                CheckInDateTextBox.Text = RequisationBo.Date_of_travel.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                //CheckOutDateTextBox.Text = Convert.ToString(RequisationBo.Check_out_date);
                CheckOutDateTextBox.Text = RequisationBo.To_Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                CheckInDateTextBox.Enabled = false;
                CheckOutDateTextBox.Enabled = false;
                CheckInDateImageButton.Enabled = false;
                CheckOutDateImageButton.Enabled = false;
         
                SaveButton.Text = "Update";
                if (RequisationBo.Number_of_members > 0)
                {
                    Panelmember.Visible = true;
                    vehicle_requisitionbo objbo = new vehicle_requisitionbo();
                    local_travel_requisitionbl objbl = new local_travel_requisitionbl();
                    List<vehicle_requisitionbo> objlist = new List<vehicle_requisitionbo>();

                    objlist = objbl.Get_Local_travel_Group_members(local_travel_req_id);
                    NumberofmembersTextBox.Text = RequisationBo.Number_of_members.ToString();
                    InitialiseAddRemoveEmployeeSegments(objlist, objlist.Count-1);
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

        void ClearControl()
        {
            SaveButton.Text = "Save";
            FromTextBox.Text = "";
            ToTextBox.Text = "";
            DropDownVehicletype.SelectedValue ="0";
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
            CheckInDateTextBox.Enabled = true;
            CheckOutDateTextBox.Enabled = true;
            CheckInDateImageButton.Enabled = true;
            CheckOutDateImageButton.Enabled = true;

            Session["LOCAL_TRAVEL_REQUISITION_ID"] = null;
            EmployeeDetailsGridView.DataSource = null;
            EmployeeDetailsGridView.DataBind();
        }

        protected List<vehicle_requisitionbo> GetGridViewAllRowsControlValues()
        {
            List<vehicle_requisitionbo> MemberNameDetailsListObject = new List<vehicle_requisitionbo>();
            for (int i = 0; i < EmployeeDetailsGridView.Rows.Count; i++)
            {
                vehicle_requisitionbo obj = new vehicle_requisitionbo();
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
                obj.Emp_name = EmployeeNameLabel.Text;
                obj.EMPLOYEE_NO = EmployeeIdLabel.Text;

                MemberNameDetailsListObject.Add(obj);
            }
            return MemberNameDetailsListObject;
        }

        protected void AddToGridButton_Click(object sender, EventArgs e)
        {
            vehicle_requisitionbo RequiBoObject = new vehicle_requisitionbo();
            List<vehicle_requisitionbo> RequiBoListObject = new List<vehicle_requisitionbo>();
            try
            {
                if (!(NameOfMembersTextBox.Text == "" || NameOfMembersTextBox.Text == string.Empty))
                { 
                      //Collecting the displayed values from the grid
                    RequiBoListObject = GetGridViewAllRowsControlValues();
                    //Validating the number of members and added members to the grid.
                    if (Convert.ToInt32(NumberofmembersTextBox.Text) >= RequiBoListObject.Count + 1)
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
        
        protected void EmployeeDetailsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }        

        protected void EmployeeDetailsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            vehicle_requisitionbo RequiBoObject = new vehicle_requisitionbo();
            List<vehicle_requisitionbo> RequiBoListObject = new List<vehicle_requisitionbo>();
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

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ClearControl();
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

        protected void RadioButtontraveltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RadioButtontraveltype.SelectedValue == "1")
                {
                    VisibilityFalse();
                    ClearControl();
                }
                else 
                {
                    VisibilityTure();
                    ClearControl();
                }
               
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        void VisibilityFalse()
        {
            Panelmember.Visible = false;
            EmployeeDetailsGridView.DataSource = null;
            EmployeeDetailsGridView.DataBind();
        }

        void VisibilityTure()
        {
            Panelmember.Visible = true;
        }
       
    }
}
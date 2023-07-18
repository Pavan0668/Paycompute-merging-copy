using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BasicFrame.WebControls;
using System.Globalization;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using System.Text.RegularExpressions;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class td_requisitions : System.Web.UI.Page
    {
        #region "Fields"
            string TravelType_Id ="";
            string TravelDate = "";
            string EmployeeName ="";
            string EmployeeId ="";
            public bool TravelRequestSortedOrder_Bool = false;
            public int TrvelRequestGridSelectedIndexChange = -1;
        #endregion

        #region "Helper"
        void LoadTravelType()
        {
            TravelTypeDropDownList.Items.Clear();
            string[] EnumTravelType = Enum.GetNames(typeof(TravelType));
            foreach (string TravelType in EnumTravelType)
            {
                int Value = (int)Enum.Parse(typeof(TravelType), TravelType);
                ListItem Item = new ListItem(TravelType, Value.ToString(CultureInfo.CurrentCulture));
                TravelTypeDropDownList.Items.Add(Item);
            }
            TravelTypeDropDownList.SelectedIndex = 0;
        }

        void Search()
        {
            travelrequestbl travelrequestblObj = new travelrequestbl();
            List<requisitionbo> requisitionboList = new List<requisitionbo>();
            requisitionboList = travelrequestblObj.Load_TravelRequestionDetails(TravelType_Id, TravelDate, EmployeeId, EmployeeName);
            Session.Add("HODRequisitionDetails", requisitionboList);
            if (requisitionboList == null || requisitionboList.Count == 0)
            {
                TravelRequestGridView.Visible = false;
                TravelRequestGridView.DataSource = null;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "No records found.";
                return;
            }
            else
            {
                TravelRequestGridView.Visible = true;
                TravelRequestGridView.DataSource = requisitionboList;
                TravelRequestGridView.SelectedIndex = -1;
            }
            TravelRequestGridView.DataBind();
        }

        public enum SeatPreference
        {
            ASILE = 1,
            MIDDLE = 2,
            WINDOW = 3
        }

        public enum MealPreference
        {
            [Description("ASIAN VEG MEAL")]
            ASIANVEGMEAL = 1,
            [Description("CHILD MEAL")]
            CHILDMEAL = 2,
            [Description("DIABATIC MEAL")]
            DIABATICMEAL = 3,
            [Description("HINDU MEAL")]
            HINDUMEAL = 4,
            [Description("LOCAL MEAL")]
            LOCALMEAL = 5,
            [Description("NON VEG MEAL")]
            NONVEGMEAL = 6,
            [Description("VEG MEAL")]
            VEGMEAL = 7
        }

        public enum Baggage
        {
            [Description("2KG")]
            KG2 = 1,
            [Description("4KG")]
            KG4 = 2,
            [Description("6KG")]
            KG6 = 3,
            [Description("8KG")]
            KG8 = 4,
            [Description("10KG")]
            KG10 = 5
        }

        public enum Hand
        {
            [Description("2KG")]
            KG2 = 1,
            [Description("4KG")]
            KG4 = 2,
            [Description("6KG")]
            KG6 = 3,
            [Description("8KG")]
            KG8 = 4,
            [Description("10KG")]
            KG10 = 5
        }

        public static IEnumerable<T> EnumToList<T>()
        {
            Type enumType = typeof(T);
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");
            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);
            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }
            return enumValList;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        #endregion

        #region "Events"
        public static Control GetPostBackControl(Page thePage)
        {
            Control myControl = null;
            string ctrlName = thePage.Request.Params.Get("__EVENTTARGET");
            if (((ctrlName != null) & (ctrlName != string.Empty)))
            {
                myControl = thePage.FindControl(ctrlName);
            }
            else
            {
                foreach (string Item in thePage.Request.Form)
                {
                    Control c = thePage.FindControl(Item);
                    if (((c) is System.Web.UI.WebControls.Button))
                    {
                        myControl = c;
                    }
                }
            }
            return myControl;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            if (!IsPostBack)
            {
                //If page is redirecting from veh/acc requiisition do not clear the session. 
                if (Request.QueryString["Redirected"] == null)
                {
                    ClearControls();
                }
                else
                {
                    if (Request.QueryString["Redirected"].ToString() == "1")
                    {
                        tcDefalutRequisition.ActiveTabIndex = 0;
                    }
                    else
                    {
                        tcDefalutRequisition.ActiveTabIndex = 1;
                    }
                }

                LoadTravelRequestGridView(string.Empty, string.Empty, string.Empty, string.Empty);
                LoadDomesticGridViewFirstRow();
                LoadInternationalGridViewFirstRow();
                LoadTravelType();
                //load the travel requisiton.
                LoadDomesticRequiSegmentGridView();
                LoadInternationalRequiSegmentGridView();
                LoadVehicleRequisition();
                LoadAccomodationRequisition();
                DomesticGridView.Visible = false;
                InternationalGridView.Visible = false;
                Session.Add("TravelRequestSortedOrder_Bool", TravelRequestSortedOrder_Bool);
            }
        }

        void LoadInternationalRequiSegmentGridView()
        {
            List<requisitionbo> objCollection1 = new List<requisitionbo>();
            if (Session["InternationalSegmentList"] != null)
            {
                objCollection1 = (List<requisitionbo>)Session["InternationalSegmentList"];
                InternationalRequisitionSegmentGridView.DataSource = objCollection1;
                InternationalRequisitionSegmentGridView.DataBind();
            }
            else
            {
                InternationalRequisitionSegmentGridView.DataSource = null;
                InternationalRequisitionSegmentGridView.DataBind();
            }
        }

        void LoadDomesticRequiSegmentGridView()
        {
            List<requisitionbo> objCollection1 = new List<requisitionbo>();
            if (Session["DomesticSegmentList"] != null)
            {
                objCollection1 = (List<requisitionbo>)Session["DomesticSegmentList"];
                DomesticRequiSegmentGridView.DataSource = objCollection1;
                DomesticRequiSegmentGridView.DataBind();
            }
            else
            {
                DomesticRequiSegmentGridView.DataSource = null;
                DomesticRequiSegmentGridView.DataBind();
            }
        }

        void LoadAccomodationRequisition()
        {
            if (Session["DomesticSegmentList"] != null)
            {
                if (Session["AccomodationRequisitionDomestic"] != null)
                {
                    GridView DomesticAccGrid = (GridView)DomesticRequiSegmentGridView.Rows[0].FindControl("AccomodationRequisitionGridViewDomestic");
                    List<accomodation_requisitionbo> AccomodationList = new List<accomodation_requisitionbo>();
                    AccomodationList = (List<accomodation_requisitionbo>)Session["AccomodationRequisitionDomestic"];
                    DomesticAccGrid.DataSource = AccomodationList;
                    DomesticAccGrid.DataBind();
                    buttoncontrols.Visible = true;
                }
                else if (Session["AccomodationRequisitionDomestic"] == null)
                {
                    GridView DomesticAccGrid = (GridView)DomesticRequiSegmentGridView.Rows[0].FindControl("AccomodationRequisitionGridViewDomestic");
                    DomesticAccGrid.DataSource = null;
                    DomesticAccGrid.DataBind();
                }
            }
            if (Session["InternationalSegmentList"] != null)
            {
                if (Session["AccomodationRequisitionInternational"] != null)
                {
                    GridView InternationalAccGrid = (GridView)InternationalRequisitionSegmentGridView.Rows[0].FindControl("AccomodationRequisitionGridViewInternational");
                    List<accomodation_requisitionbo> AccomodationList = new List<accomodation_requisitionbo>();
                    AccomodationList = (List<accomodation_requisitionbo>)Session["AccomodationRequisitionInternational"];
                    InternationalAccGrid.DataSource = AccomodationList;
                    InternationalAccGrid.DataBind();
                    buttoncontrols.Visible = true;
                }
                else if (Session["AccomodationRequisitionInternational"] == null)
                {
                    GridView InternationalAccGrid = (GridView)InternationalRequisitionSegmentGridView.Rows[0].FindControl("AccomodationRequisitionGridViewInternational");
                    InternationalAccGrid.DataSource = null;
                    InternationalAccGrid.DataBind();
                }
            }
        }

        void LoadVehicleRequisition()
        {
            if (Session["DomesticSegmentList"] != null)
            {
                if (Session["VehicleRequisitionDomestic"] != null)
                {
                    GridView DomesticVehGrid = (GridView)DomesticRequiSegmentGridView.Rows[0].FindControl("VehicleRequisitionGridViewDomestic");
                    List<vehicle_requisitionbo> AccomodationList = new List<vehicle_requisitionbo>();
                    AccomodationList = (List<vehicle_requisitionbo>)Session["VehicleRequisitionDomestic"];
                    DomesticVehGrid.DataSource = AccomodationList;
                    DomesticVehGrid.DataBind();
                    buttoncontrols.Visible = true;
                }
                else if (Session["VehicleRequisitionDomestic"] == null)
                {
                    GridView DomesticVehGrid = (GridView)DomesticRequiSegmentGridView.Rows[0].FindControl("VehicleRequisitionGridViewDomestic");
                    DomesticVehGrid.DataSource = null;
                    DomesticVehGrid.DataBind();
                }
            }

            if (Session["InternationalSegmentList"] != null)
            {
                if (Session["VehicleRequisitionInternational"] != null)
                {
                    GridView InternationalVehGrid = (GridView)InternationalRequisitionSegmentGridView.Rows[0].FindControl("VehicleRequisitionGridViewInternational");
                    List<vehicle_requisitionbo> AccomodationList = new List<vehicle_requisitionbo>();
                    AccomodationList = (List<vehicle_requisitionbo>)Session["VehicleRequisitionInternational"];
                    InternationalVehGrid.DataSource = AccomodationList;
                    InternationalVehGrid.DataBind();
                    buttoncontrols.Visible = true;
                }
                else if (Session["VehicleRequisitionInternational"] == null)
                {
                    GridView InternationalVehGrid = (GridView)InternationalRequisitionSegmentGridView.Rows[0].FindControl("VehicleRequisitionGridViewInternational");
                    InternationalVehGrid.DataSource = null;
                    InternationalVehGrid.DataBind();
                }
            }
        }

        void LoadDomesticGridViewFirstRow()
        {
            List<requisitionbo> objCollection = new List<requisitionbo>();
            CreateDomesticGridViewRows(objCollection, true);
        }

        void LoadInternationalGridViewFirstRow()
        {
            List<requisitionbo> objCollection = new List<requisitionbo>();
            CreateInternationalGridViewRows(objCollection, true);
            InitialiseAddRemoveInternationalSegments(objCollection, 0);
        }

        protected void domesticDropDownMTPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DomesticGridView.Rows.Count; i++)
            {
                DropDownList ddlMC = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownMCate");
                ddlMC.Items.Clear();
                DropDownList ddlVehName = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownVehName");
                ddlVehName.Items.Clear();

                DropDownList ddlMT = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownMTPort");
                if (ddlMT.SelectedValue != "")
                {
                    loadmcate(ddlMC, ddlMT.SelectedValue);
                    ddlMC.Enabled = true;
                    ddlVehName.Enabled = false;
                }
                else
                {
                    ddlMC.Enabled = false;
                }
                TextBox ddlflynum = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextFFlyNo");
                if (ddlMT.SelectedValue == "A")
                {

                    ddlflynum.Enabled = true;
                }
                else
                {
                    ddlflynum.Enabled = false;
                    ddlflynum.Text = "";
                }
            }
        }

        protected void domesticDropDownMCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DomesticGridView.Rows.Count; i++)
            {
                DropDownList ddlVehName = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownVehName");
                ddlVehName.Items.Clear();
                DropDownList ddlMT = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownMTPort");
                if (ddlMT.SelectedValue != "")
                {
                    loadvehiclename(ddlVehName, ddlMT.SelectedValue);
                    ddlVehName.Enabled = true;
                }
                else
                {
                    ddlVehName.Enabled = false;
                }
            }
        }

        protected void DropDownMTPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < InternationalGridView.Rows.Count; i++)
            {
                DropDownList ddlMC = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownMCate");
                ddlMC.Items.Clear();
                DropDownList ddlMT = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownMTPort");
                if (ddlMT.SelectedValue != "")
                {
                    loadmcate(ddlMC, ddlMT.SelectedValue);
                    ddlMC.Enabled = true;
                }
                else
                {
                    ddlMC.Enabled = false;
                }
                TextBox ddlflynum = (TextBox)InternationalGridView.Rows[i].FindControl("TextFFlyNo");
                if (ddlMT.SelectedValue == "A")
                {

                    ddlflynum.Enabled = true;
                }
                else
                {
                    ddlflynum.Enabled = false;
                    ddlflynum.Text = "";
                }
            }
        }

        public void LoadTravelRequestGridView(string TravelType_Id,string TravelDate,string EmployeeId,string EmployeeName)
        {
            requisitionbo requisitionboObj = new requisitionbo();
            requisitions_traveldeskbl travelrequestblObj = new requisitions_traveldeskbl();
            List<requisitionbo> requisitionboList = new List<requisitionbo>();
            requisitionboList = travelrequestblObj.Load_TravelRequistionDetailsforTravelDesk(TravelType_Id, TravelDate, EmployeeName, EmployeeId);
            Session.Add("TravelDeskRequisitionDetails", requisitionboList);
            if (requisitionboList == null || requisitionboList.Count == 0)
            {
                TravelRequestGridView.Visible = false;
                TravelRequestGridView.DataSource = null;
                return;
            }
            else
            {
                if (requisitionboList.Count == 0)
                {
                    TravelRequestGridView.Visible = false;
                    TravelRequestGridView.DataSource = null;
                    return;
                }
                else
                {
                    TravelRequestGridView.Visible = true;
                    TravelRequestGridView.DataSource = requisitionboList;
                    TravelRequestGridView.SelectedIndex = -1;
                }
            }
            TravelRequestGridView.DataBind();

           // //Menu Count
           // funMenuCount();
        }

        //public void funMenuCount()
        //{
        //    MenuEventArgs e;
        //    SiteMapNode mapNode = (SiteMapNode)e.Item.DataItem;
        //    SiteMapNodeCollection mapNode2 = (SiteMapNodeCollection)mapNode.ChildNodes;
        //    foreach (SiteMapNode smn in mapNode2)
        //    {
        //        if (smn.Title == "Travel Request" || smn.Key == "0b46ffe5-acb6-40c2-a3a4-4c36201a4397")
        //        {
        //            SiteMapNodeCollection mapNode3 = (SiteMapNodeCollection)(smn.ChildNodes);
        //            foreach (SiteMapNode smn1 in mapNode3)
        //            {
        //                if (smn1.Title == "Requisitions" || smn1.Url == "/UI/Benefits_Payment/td_requisitions.aspx")
        //                {
        //                    smn1.ReadOnly = false;
        //                    smn1.Title = "Requisitions" + "(" + Convert.ToString(TravelRequestGridView.Rows.Count) + ")";
        //                }
        //            }
        //        }
        //    }
        //}

        protected void TravelRequestGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            try
            {
                Session["SelectedRequisitionFromGrid"] = "true";
                BindControlsForUpdate();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }

            DomesticGridView.Visible = false;
            InternationalGridView.Visible = false;
            remarks.Visible = true;
            buttoncontrols.Visible = true;
            CancelPanel.Visible = false;
        }

        protected void TravelRequestGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<requisitionbo> objRequisitionBoList = new List<requisitionbo>();
            objRequisitionBoList = (List<requisitionbo>)Session["TravelDeskRequisitionDetails"];
            bool objSortOrder = (bool)Session["TravelRequestSortedOrder_Bool"];
            string SortExpression = e.SortExpression.ToString().Trim();
            objRequisitionBoList = GridManager.SortTravelRequestGridView(SortExpression, objSortOrder, objRequisitionBoList);
            TravelRequestGridView.DataSource = objRequisitionBoList;
            TravelRequestGridView.DataBind();
            // Add negation of sort order to session for next time use.
            Session.Add("TravelRequestSortedOrder_Bool", !objSortOrder);
            // Add sorted list to the session
            Session.Add("TravelDeskRequisitionDetails", objRequisitionBoList);
        }

        protected void TravelRequestGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            TravelRequestGridView.PageIndex = e.NewPageIndex;
            List<requisitionbo> requisitionboList = (List<requisitionbo>)Session["TravelDeskRequisitionDetails"];
            TravelRequestGridView.DataSource = requisitionboList;
            int SelectedId = Convert.ToInt32(ViewState["indexchang"], CultureInfo.InvariantCulture);
            int pagindex = Convert.ToInt32(ViewState["pageindex"], CultureInfo.InvariantCulture);
            TravelRequestGridView.SelectedIndex = -1;
            TravelRequestGridView.DataBind();
            if (pageindex == pagindex)
            {
                TravelRequestGridView.SelectedIndex = SelectedId;
            }
        }

        protected void TravelRequestGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.TravelRequestGridView, "Select$" + e.Row.RowIndex);
            }
        }

        void ClearControls()
        {
            TD_RemarksTextBox.Text = "";
            DomesticGridView.Visible = true;
            InternationalGridView.Visible = true;
            Session["DomesticSegmentList"] = null;
            Session["DomesticSegmentCount"] = null;
            Session["InternationalSegmentList"] = null;
            Session["InternationalSegmentCount"] = null;
            Session["AccomodationRequisitionDomestic"] = null;
            Session["SelectedRequisitionFromGrid"] = null;
            Session["AccomodationRequisitionInternational"] = null;
            Session["VehicleRequisitionDomestic"] = null;
            Session["VehicleRequisitionInternational"] = null;
            Session["AccomID"] = null; //Used for maintain the AccomID slNo in the new_accomodation_requisition form
            Session["REQ_SEGMENT_ID"] = null; //Passed this through the query string to identify the REQ_SEGMENT_ID in the new_accomodation_requisition form
            Session["TravelType"] = null; //Passed this through the query string to identify the Travel Type in the new_accomodation_requisition form
            LoadDomesticGridViewFirstRow();
            LoadInternationalGridViewFirstRow();
            LoadDomesticRequiSegmentGridView();
            LoadInternationalRequiSegmentGridView();
        }

        protected void BindControlsForUpdate()
        {
            GridViewRow grdRow = TravelRequestGridView.SelectedRow;
            int FTPT_REQUISITION_ID = Convert.ToInt32(grdRow.Cells[0].Text.ToString().Trim());
            int FTPT_REQ_SEGMENT_ID = Convert.ToInt32(grdRow.Cells[1].Text.ToString().Trim());
            Session.Add("FTPT_REQUISITION_ID", FTPT_REQUISITION_ID);
            Session.Add("FTPT_REQ_SEGMENT_ID", FTPT_REQ_SEGMENT_ID);
            List<requisitionbo> requisitionboList = new List<requisitionbo>();
            requisitionboList = (List<requisitionbo>)Session["TravelDeskRequisitionDetails"];
            requisitionbo RequisationBo = requisitionboList.Find(delegate(requisitionbo obj)
            {
                return obj.FTPT_REQUEST_ID == FTPT_REQUISITION_ID && obj.REQ_SEGMENT_ID == FTPT_REQ_SEGMENT_ID;
            });
            //----start----Traveller personal details code
            DesignationLabel.Text = RequisationBo.DESIGNATION;
            PhoneNumberLabel.Text = RequisationBo.PHONE_NUMBER;
            EmailLabel.Text = RequisationBo.EMAIL;
            //----end----Traveller personal details code
            List<requisitionbo> RequisitionList = new List<requisitionbo>();
            RequisitionList.Add(RequisationBo);
            if (Convert.ToInt16(RequisationBo.CHK) == Convert.ToInt16(TravelType.Domestic))
            {
                tcDefalutRequisition.ActiveTabIndex = 0;
                DomesticGridView.Visible = true;
                InternationalGridView.Visible = false;
                ClearDomesticControl();
                if (RequisationBo == null)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Unknown error occured.";
                }
                CreateDomesticGridViewRows(RequisitionList, false);
                if (Session["SelectedRequisitionFromGrid"] != null)
                {
                    LoadDomesticSegmentRelatedGrids(RequisitionList);
                    List<requisitionbo> RequisitionList1 = new List<requisitionbo>();
                    RequisitionList1 = null;
                    LoadInternationalSegmentRelatedGrids(RequisitionList1);
                }
                Session.Add("DomesticSegmentGridView", RequisitionList);
            }
            else if (Convert.ToInt16(RequisationBo.CHK) == Convert.ToInt16(TravelType.International))
            {
                tcDefalutRequisition.ActiveTabIndex = 1;
                DomesticGridView.Visible = false;
                InternationalGridView.Visible = true;
                ClearInternationalControl();
                if (RequisationBo == null)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Unknown error occured.";
                }
                CreateInternationalGridViewRows(RequisitionList, false);
                if (Session["SelectedRequisitionFromGrid"] != null)
                {
                    LoadInternationalSegmentRelatedGrids(RequisitionList);
                    List<requisitionbo> RequisitionList1 = new List<requisitionbo>();
                    RequisitionList1 = null;
                    LoadDomesticSegmentRelatedGrids(RequisitionList1);
                }
                Session.Add("InternationalSegmentGridView", RequisitionList);
            }
            //If status  is new then only show update button modify requisiton other wise only view.
            if (grdRow.RowIndex > -1)
            {
                int iSelectedIndex = Convert.ToInt32(grdRow.RowIndex, CultureInfo.CurrentCulture);
                int ipageindex = Convert.ToInt32(TravelRequestGridView.PageIndex, CultureInfo.CurrentCulture);
                ViewState.Add("indexchang", iSelectedIndex);
                ViewState.Add("pageindex", ipageindex);
            }
        }

        protected void ClearDomesticControl()
        {
            DomesticGridView.DataSource = null;
            DomesticGridView.DataBind();
        }

        protected void ClearInternationalControl()
        {
            InternationalGridView.DataSource = null;
            InternationalGridView.DataBind();
        }

        void InitialiseAddRemoveDomesticSegments(List<requisitionbo> objCollection, int count)
        {
            for (int i = 0; i < objCollection.Count; i++)
            {
                requisitionbo obj = new requisitionbo();
                TextBox boxDateTravel = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextDateTravel");
                TextBox boxtimedeparture = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextTimeDeparture");
                TextBox boxFFlyNo = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextFFlyNo");
                TextBox boxRemarks = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextRemarks");
                TextBox TextBoxHODRemarks = (TextBox)DomesticGridView.Rows[i].FindControl("txtHODRemarks");
                TextBox TextBoxTDRemarks = (TextBox)DomesticGridView.Rows[i].FindControl("txtTDRemarks");
                TextBox boxAdvance = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextAdvance");
                DropDownList ddlMTPort = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownMTPort");
                DropDownList ddlMCate = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownMCate");
                DropDownList ddlVehName = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownVehName");
                DropDownList ddlFrom = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownFrom");
                DropDownList ddlTo = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownTo");
                if (i == count)
                {
                }
                else
                {
                    boxDateTravel.Text = objCollection[i].TRAVEL_DATE.ToString("dd-MM-yyyy");
                    boxtimedeparture.Text = objCollection[i].TRAVEL_TIME;
                    boxFFlyNo.Text = objCollection[i].FLYNUM;
                    boxRemarks.Text = objCollection[i].REMARKS;
                    TextBoxHODRemarks.Text = objCollection[i].HOD_REMARKS;
                    TextBoxTDRemarks.Text = objCollection[i].TD_REMARKS;
                    boxAdvance.Text = objCollection[i].ADVANCE;
                    ddlMTPort.SelectedValue = objCollection[i].MODE_OF_TRANSPOPRT_KZPMF;
                    loadmcate(ddlMCate, objCollection[i].MODE_OF_TRANSPOPRT_KZPMF);
                    ddlMCate.Enabled = true;
                    ddlMCate.SelectedItem.Text = objCollection[i].MEDIA_OF_CATEGORY_TEXT25;
                    loadvehiclename(ddlVehName, objCollection[i].MODE_OF_TRANSPOPRT_KZPMF);
                    ddlVehName.Enabled = true;
                    ddlVehName.SelectedValue = objCollection[i].VEHICLE_NAME_VHNUM ;
                    ddlFrom.SelectedValue = objCollection[i].REGION_RGION_FROM;
                    ddlTo.SelectedValue = objCollection[i].REGION_RGION_TO;
                }
            }
        }

        void InitialiseAddRemoveInternationalSegments(List<requisitionbo> objCollection, int count)
        {
            for (int i = 0; i < objCollection.Count; i++)
            {
                requisitionbo obj = new requisitionbo();
                TextBox boxDateTravel = (TextBox)InternationalGridView.Rows[i].FindControl("TextDateTravel");
                TextBox boxAirline = (TextBox)InternationalGridView.Rows[i].FindControl("TextAirline");
                TextBox boxInsurMediclaim = (TextBox)InternationalGridView.Rows[i].FindControl("TextInsurMediclaim");
                TextBox boxFFlyNo = (TextBox)InternationalGridView.Rows[i].FindControl("TextFFlyNo");
                TextBox boxRemarks = (TextBox)InternationalGridView.Rows[i].FindControl("TextRemarks");
                TextBox TextBoxHODRemarks = (TextBox)DomesticGridView.Rows[i].FindControl("txtHODRemarks");
                TextBox TextBoxTDRemarks = (TextBox)DomesticGridView.Rows[i].FindControl("txtTDRemarks");
                TextBox boxAdvance = (TextBox)InternationalGridView.Rows[i].FindControl("TextAdvance");
                DropDownList ddlMTP = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownMTPort");
                DropDownList ddlMC = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownMCate");
                DropDownList ddlFrom = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownFrom");
                DropDownList ddlTo = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownTo");
                DropDownList ddlSeatPreference = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownSeatPreference");
                DropDownList ddlMealPreference = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownMealPreference");
                DropDownList ddlBaggage = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownBaggage");
                DropDownList ddlHand = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownHand");
                if (i == count)
                {
                }
                else
                {
                    boxDateTravel.Text = objCollection[i].TRAVEL_DATE.ToString("dd-MM-yyyy");
                    boxAirline.Text = objCollection[i].AIRLINE;
                    boxInsurMediclaim.Text = objCollection[i].INSUR_MEDICLAIM;
                    boxFFlyNo.Text = objCollection[i].FLYNUM;
                    boxRemarks.Text = objCollection[i].REMARKS;
                    TextBoxHODRemarks.Text = objCollection[i].HOD_REMARKS;
                    TextBoxTDRemarks.Text = objCollection[i].TD_REMARKS;
                    boxAdvance.Text = objCollection[i].ADVANCE;
                    ddlMTP.SelectedValue = objCollection[i].MODE_OF_TRANSPOPRT_KZPMF;
                    loadmcate(ddlMC, objCollection[i].MODE_OF_TRANSPOPRT_KZPMF);
                    ddlMC.Enabled = true;
                    ddlMC.SelectedItem.Text = objCollection[i].MEDIA_OF_CATEGORY_TEXT25;
                    ddlFrom.SelectedValue = objCollection[i].REGION_RGION_FROM;
                    ddlTo.SelectedValue = objCollection[i].REGION_RGION_TO;
                    ddlSeatPreference.SelectedValue = objCollection[i].SEAT_PREFERENCE;
                    ddlMealPreference.SelectedValue = objCollection[i].MEAL_PREFERENCE;
                    ddlBaggage.SelectedValue = objCollection[i].BAGGAGE;
                    ddlHand.SelectedValue = objCollection[i].HAND;
                }
            }
        }

        protected void CreateDomesticGridViewRows(List<requisitionbo> objList, bool isAddRemoveRange)
        {
            if (isAddRemoveRange == true)
            {
                requisitionbo obj = new requisitionbo();
                objList.Add(obj);
            }
            DomesticGridView.DataSource = objList;
            DomesticGridView.DataBind();
            for (int i = 0; i < objList.Count; i++)
            {
                //After binding the gridview, we can then extract and fill the DropDownList with Data 
                DropDownList ddlMTPort = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownMTPort");
                Loadmtport(ddlMTPort);
                DropDownList ddlMCate = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownMCate");
                ddlMCate.Enabled = false;
                DropDownList ddlVehName = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownVehName");
                ddlVehName.Enabled = false;
                DropDownList ddlFrom = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownFrom");
                loadfrom(ddlFrom);
                DropDownList ddlTo = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownTo");
                loadto(ddlTo);
            }
        }

        protected void CreateInternationalGridViewRows(List<requisitionbo> objList, bool isAddRemoveRange)
        {
            if (isAddRemoveRange == true)
            {
                requisitionbo obj = new requisitionbo();
                objList.Add(obj);
            }
            InternationalGridView.DataSource = objList;
            InternationalGridView.DataBind();
            for (int i = 0; i < objList.Count; i++)
            {
                //After binding the gridview, we can then extract and fill the DropDownList with Data 
                DropDownList ddlMTP = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownMTPort");
                Loadmtport(ddlMTP);
                DropDownList ddlMC = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownMCate");
                ddlMC.Enabled = false;
                DropDownList ddlFrom = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownFrom");
                loadfrom(ddlFrom);
                DropDownList ddlTo = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownTo");
                loadto(ddlTo);
                DropDownList ddlSeatPreference = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownSeatPreference");
                loadseatpreference(ddlSeatPreference);
                DropDownList ddlMealPreference = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownMealPreference");
                loadmealpreference(ddlMealPreference);
                DropDownList ddlBaggage = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownBaggage");
                loadbaggage(ddlBaggage);
                DropDownList ddlHand = (DropDownList)InternationalGridView.Rows[i].FindControl("DropDownHand");
                loadhand(ddlHand);
            }
        }

        protected void Loadmtport(DropDownList ddl)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetModeofTransport();
            ddl.DataTextField = "MODE_OF_TRANSPOPRT_FZTXT";
            ddl.DataValueField = "MODE_OF_TRANSPOPRT_KZPMF";
            ddl.DataBind();
            ListItem drpDefaultItem1 = new ListItem("Select medium of transport", "0", true);
            ddl.Items.Add(drpDefaultItem1);
            ddl.SelectedValue = "0";
        }

        protected void loadmcate(DropDownList ddl, string MediumOfTransport)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetModeofCategory(MediumOfTransport);
            ddl.DataTextField = "MEDIA_OF_CATEGORY_TEXT25";
            ddl.DataValueField = "MEDIA_OF_CATEGORY_PKWKL";
            ddl.DataBind();
            ListItem drpDefaultItem2 = new ListItem("Select medium of category", "0", true);
            ddl.Items.Add(drpDefaultItem2);
            ddl.SelectedValue = "0";
        }

        protected void loadfrom(DropDownList ddl)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetRegionName();
            ddl.DataTextField = "REGION_TEXT25_FROM";
            ddl.DataValueField = "REGION_RGION_FROM";
            ddl.DataBind();
            ListItem drpDefaultItem3 = new ListItem("Select from place", "0", true);
            ddl.Items.Add(drpDefaultItem3);
            ddl.SelectedValue = "0";
        }

        protected void loadvehiclename(DropDownList ddl, string MediumOfCategory)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetVehicleName(MediumOfCategory);
            ddl.DataTextField = "VEHICLE_NAME_ZZVEHNAM";
            ddl.DataValueField = "VEHICLE_NAME_VHNUM";
            ddl.DataBind();
            ListItem drpDefaultItem9 = new ListItem("Select vehicle", "0", true);
            ddl.Items.Add(drpDefaultItem9);
            ddl.SelectedValue = "0";
        }

        protected void loadto(DropDownList ddl)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetRegionName();
            ddl.DataTextField = "REGION_TEXT25_FROM";
            ddl.DataValueField = "REGION_RGION_FROM";
            ddl.DataBind();
            ListItem drpDefaultItem4 = new ListItem("Select to place", "0", true);
            ddl.Items.Add(drpDefaultItem4);
            ddl.SelectedValue = "0";
        }

        protected void loadseatpreference(DropDownList ddl)
        {
            //get enum items to get the respective enum value 
            string[] enumNames = Enum.GetNames(typeof(SeatPreference));
            foreach (string item in enumNames)
            {
                int Value = (int)Enum.Parse(typeof(SeatPreference), item);
                SeatPreference HC = ((SeatPreference)Value);
                ListItem Item = new ListItem(AllEnum.GetEnumDescription(HC), Value.ToString(CultureInfo.CurrentCulture));
                ddl.Items.Add(Item);
            }
            ListItem drpDefaultItem4 = new ListItem("Select preference", "0", true);
            ddl.Items.Add(drpDefaultItem4);
            ddl.SelectedValue = "0";
        }

        protected void loadmealpreference(DropDownList ddl)
        {
            string[] enumNames = Enum.GetNames(typeof(MealPreference));
            foreach (string items in enumNames)
            {
                int Value = (int)Enum.Parse(typeof(MealPreference), items);
                MealPreference HC = ((MealPreference)Value);
                ListItem Item = new ListItem(AllEnum.GetEnumDescription(HC), Value.ToString(CultureInfo.CurrentCulture));
                ddl.Items.Add(Item);
            }
            ListItem drpDefaultItem5 = new ListItem("Select meal preference", "0", true);
            ddl.Items.Add(drpDefaultItem5);
            ddl.SelectedValue = "0";
        }

        protected void loadbaggage(DropDownList ddl)
        {
            string[] enumNames = Enum.GetNames(typeof(Baggage));
            foreach (string items in enumNames)
            {
                int Value = (int)Enum.Parse(typeof(Baggage), items);
                Baggage HC = ((Baggage)Value);
                ListItem Item = new ListItem(AllEnum.GetEnumDescription(HC), Value.ToString(CultureInfo.CurrentCulture));
                ddl.Items.Add(Item);
            }
            ListItem drpDefaultItem6 = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItem6);
            ddl.SelectedValue = "0";
        }

        protected void loadhand(DropDownList ddl)
        {
            string[] enumNames = Enum.GetNames(typeof(Hand));
            foreach (string items in enumNames)
            {
                int Value = (int)Enum.Parse(typeof(Hand), items);
                Hand HC = ((Hand)Value);
                ListItem Item = new ListItem(AllEnum.GetEnumDescription(HC), Value.ToString(CultureInfo.CurrentCulture));
                ddl.Items.Add(Item);
            }
            ListItem drpDefaultItem6 = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItem6);
            ddl.SelectedValue = "0";
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            List<requisitionbo> requisitionboList = new List<requisitionbo>();
            requisitionboList = (List<requisitionbo>)Session["TravelDeskRequisitionDetails"];
            requisitionbo RequisationBo = requisitionboList.Find(delegate(requisitionbo obj)
            {
                return obj.FTPT_REQUEST_ID == Convert.ToInt32(Session["FTPT_REQUISITION_ID"]) && obj.REQ_SEGMENT_ID == Convert.ToInt32(Session["FTPT_REQ_SEGMENT_ID"]);
            });
            //------start---Initializing the cancel popup from and to details-------------
            FromLabel.Text = RequisationBo.REGION_TEXT25_FROM;
            ToLabel.Text = RequisationBo.REGION_TEXT25_TO;
            ReasonForCancelTextBox.Text = RequisationBo.REGION_TEXT25_TO;
            CancelButtonModalPopupExtender.Show();
            //------end---Initializing the cancel popup from and to details-------------
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            try{
                requisitionbo requisitionboObject = new requisitionbo();
                requisitions_traveldeskbl travelrequestBl = new requisitions_traveldeskbl();
                string TravelDate = string.Empty;
                string DepartureTime = string.Empty;
                string MediaOfTransport = string.Empty;
                string MediumOfCategory = string.Empty;
                string Airline = string.Empty;
                string VehicleNumber = string.Empty;
                string VehicleName = string.Empty;
                string From = string.Empty;
                string FromName = string.Empty;
                string To = string.Empty;
                string ToName = string.Empty;
                string VisaRequired = string.Empty;
                string ForeignExchange = string.Empty;
                string InsurMediclaim = string.Empty;
                string FFlyNo = string.Empty;
                string SeatPreference = string.Empty;
                string MealPreference = string.Empty;
                string Baggage = string.Empty;
                string HandBagWeight = string.Empty;
                string Remarks = string.Empty;
                Remarks = TD_RemarksTextBox.Text;
                string AdvanceAmount = string.Empty;
                string TypeOfTravel = string.Empty;
                //Common fields for each record.
                requisitionboObject.CREATED_BY = User.Identity.Name.Trim();
                requisitionboObject.EMPLOYEE_NO = User.Identity.Name.Trim();
                requisitionboObject.CURRENT_STATUS = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.New.ToString()));  //get the enum value of 'New' status
                requisitionboObject.ISACTIVE = 1;

                #region "Constructing the domestic piped string"
                List<requisitionbo> DomesticRowsCollection = new List<requisitionbo>();
                if (Session["DomesticSegmentList"] != null)
                {
                    DomesticRowsCollection = (List<requisitionbo>)Session["DomesticSegmentList"];
                }
                for (int i = 0; i < DomesticRowsCollection.Count; i++)
                {
                    //Validating the domestic date fields before constructin piped string. Skip the row if date is empty .
                    if (!string.IsNullOrEmpty(Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE_ALL)))
                    {
                        //Construction travel type piped string.
                        if (TypeOfTravel == string.Empty || TypeOfTravel.Length == 0)
                        {
                            TypeOfTravel = Convert.ToString((int)TravelType.Domestic);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE) != "")
                            {
                                TypeOfTravel = TypeOfTravel + "|" + Convert.ToString((int)TravelType.Domestic);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing travel date piped string 
                        if (TravelDate == string.Empty || TravelDate.Length == 0)
                        {
                            TravelDate = Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE) != "")
                            {
                                TravelDate = TravelDate + "|" + Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing travel time piped string 
                        if (DepartureTime == string.Empty || DepartureTime.Length == 0)
                        {
                            DepartureTime = !string.IsNullOrEmpty(DomesticRowsCollection[i].TRAVEL_TIME) ? DomesticRowsCollection[i].TRAVEL_TIME : "";
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].TRAVEL_TIME).Length > 0 || Convert.ToString(DomesticRowsCollection[i].TRAVEL_TIME) != "")
                            {
                                DepartureTime = DepartureTime + "|" + Convert.ToString(DomesticRowsCollection[i].TRAVEL_TIME);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing media of transport piped string 
                        if (MediaOfTransport == string.Empty || MediaOfTransport.Length == 0)
                        {
                            MediaOfTransport = Convert.ToString(DomesticRowsCollection[i].MODE_OF_TRANSPOPRT_KZPMF);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].MODE_OF_TRANSPOPRT_KZPMF).Length > 0 || Convert.ToString(DomesticRowsCollection[i].MODE_OF_TRANSPOPRT_KZPMF) != "")
                            {
                                MediaOfTransport = MediaOfTransport + "|" + Convert.ToString(DomesticRowsCollection[i].MODE_OF_TRANSPOPRT_KZPMF);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing medium of category piped string 
                        if (MediumOfCategory == string.Empty || MediumOfCategory.Length == 0)
                        {
                            MediumOfCategory = Convert.ToString(DomesticRowsCollection[i].MEDIA_OF_CATEGORY_PKWKL);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].MEDIA_OF_CATEGORY_PKWKL).Length > 0 || Convert.ToString(DomesticRowsCollection[i].MEDIA_OF_CATEGORY_PKWKL) != "")
                            {
                                MediumOfCategory = MediumOfCategory + "|" + Convert.ToString(DomesticRowsCollection[i].MEDIA_OF_CATEGORY_PKWKL);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing airline piped string 
                        if (Airline == string.Empty || Airline.Length == 0)
                        {
                            Airline = !string.IsNullOrEmpty(DomesticRowsCollection[i].AIRLINE) ? DomesticRowsCollection[i].AIRLINE : "";

                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].AIRLINE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].AIRLINE) != "")
                            {
                                Airline = Airline + "|" + Convert.ToString(DomesticRowsCollection[i].AIRLINE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing vehicle number piped string 
                        if (VehicleNumber == string.Empty || VehicleNumber.Length == 0)
                        {
                            VehicleNumber = Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_VHNUM);
                            VehicleName = Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_ZZVEHNAM);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_VHNUM).Length > 0 || Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_VHNUM) != "")
                            {
                                VehicleNumber = VehicleNumber + "|" + Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_VHNUM);
                                VehicleName = VehicleName + "|" + Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_ZZVEHNAM);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing from place piped string 
                        if (From == string.Empty || From.Length == 0)
                        {
                            From = Convert.ToString(DomesticRowsCollection[i].REGION_RGION_FROM);
                            FromName = Convert.ToString(DomesticRowsCollection[i].REGION_TEXT25_FROM);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].REGION_RGION_FROM).Length > 0 || Convert.ToString(DomesticRowsCollection[i].REGION_RGION_FROM) != "")
                            {
                                From = From + "|" + Convert.ToString(DomesticRowsCollection[i].REGION_RGION_FROM);
                                FromName = FromName + "|" + Convert.ToString(DomesticRowsCollection[i].REGION_TEXT25_FROM);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing 'To' place piped string 
                        if (To == string.Empty || To.Length == 0)
                        {
                            To = Convert.ToString(DomesticRowsCollection[i].REGION_RGION_TO);
                            ToName = Convert.ToString(DomesticRowsCollection[i].REGION_TEXT25_TO);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].REGION_RGION_TO).Length > 0 || Convert.ToString(DomesticRowsCollection[i].REGION_RGION_TO) != "")
                            {
                                To = To + "|" + Convert.ToString(DomesticRowsCollection[i].REGION_RGION_TO);
                                ToName = ToName + "|" + Convert.ToString(DomesticRowsCollection[i].REGION_TEXT25_TO);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing visa required piped string 
                        if (VisaRequired == string.Empty || VisaRequired.Length == 0)
                        {
                            if (DomesticRowsCollection[i].VISA_REQUIRED == true)
                            {
                                VisaRequired = "true";
                            }
                            else
                                VisaRequired = "false";
                        }
                        else
                        {
                            if (DomesticRowsCollection[i].VISA_REQUIRED == true)
                            {
                                VisaRequired = VisaRequired + "|" + "true";
                            }
                            else
                            {
                                VisaRequired = VisaRequired + "|" + "false";
                            }
                        }
                        //Constructing foreign exchange piped string 
                        if (ForeignExchange == string.Empty || ForeignExchange.Length == 0)
                        {
                            ForeignExchange = !string.IsNullOrEmpty(DomesticRowsCollection[i].FR_EXCHANGE) ? DomesticRowsCollection[i].FR_EXCHANGE : "";
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].FR_EXCHANGE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].FR_EXCHANGE) != "")
                            {
                                ForeignExchange = ForeignExchange + "|" + Convert.ToString(DomesticRowsCollection[i].FR_EXCHANGE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing insurance mediclaim piped string 
                        if (InsurMediclaim == string.Empty || InsurMediclaim.Length == 0)
                        {
                            InsurMediclaim = !string.IsNullOrEmpty(DomesticRowsCollection[i].INSUR_MEDICLAIM) ? DomesticRowsCollection[i].INSUR_MEDICLAIM : "";
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].INSUR_MEDICLAIM).Length > 0 || Convert.ToString(DomesticRowsCollection[i].INSUR_MEDICLAIM) != "")
                            {
                                InsurMediclaim = InsurMediclaim + "|" + Convert.ToString(DomesticRowsCollection[i].INSUR_MEDICLAIM);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing frequently fly over number piped string 
                        if (FFlyNo == string.Empty || FFlyNo.Length == 0)
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].FLYNUM).Length == 0 || Convert.ToString(DomesticRowsCollection[i].FLYNUM) == "")
                            {
                                FFlyNo = "_";
                            }
                            else
                            {
                                FFlyNo = Convert.ToString(DomesticRowsCollection[i].FLYNUM);
                            }
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].FLYNUM).Length > 0 || Convert.ToString(DomesticRowsCollection[i].FLYNUM) != "")
                            {
                                FFlyNo = FFlyNo + "|" + Convert.ToString(DomesticRowsCollection[i].FLYNUM);
                            }
                            else
                            {
                                FFlyNo = FFlyNo + "|" + "_"; //Underscore indicates the user not entered that value.
                                // continue;
                            }
                        }
                        //Constructing seat prefrence  piped string 
                        if (SeatPreference == string.Empty || SeatPreference.Length == 0)
                        {
                            SeatPreference = !string.IsNullOrEmpty(DomesticRowsCollection[i].SEAT_PREFERENCE) ? DomesticRowsCollection[i].SEAT_PREFERENCE : "";
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].SEAT_PREFERENCE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].SEAT_PREFERENCE) != "")
                            {
                                SeatPreference = SeatPreference + "|" + Convert.ToString(DomesticRowsCollection[i].SEAT_PREFERENCE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing meal prefrence  piped string 
                        if (MealPreference == string.Empty || MealPreference.Length == 0)
                        {
                            MealPreference = !string.IsNullOrEmpty(DomesticRowsCollection[i].MEAL_PREFERENCE) ? DomesticRowsCollection[i].MEAL_PREFERENCE : "";
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].MEAL_PREFERENCE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].MEAL_PREFERENCE) != "")
                            {
                                MealPreference = MealPreference + "|" + Convert.ToString(DomesticRowsCollection[i].MEAL_PREFERENCE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing baggage  piped string 
                        if (Baggage == string.Empty || Baggage.Length == 0)
                        {
                            Baggage = !string.IsNullOrEmpty(DomesticRowsCollection[i].BAGGAGE) ? DomesticRowsCollection[i].BAGGAGE : "";
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].BAGGAGE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].BAGGAGE) != "")
                            {
                                Baggage = Baggage + "|" + Convert.ToString(DomesticRowsCollection[i].BAGGAGE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing hand bag weight  piped string 
                        if (HandBagWeight == string.Empty || HandBagWeight.Length == 0)
                        {
                            HandBagWeight = !string.IsNullOrEmpty(DomesticRowsCollection[i].HAND) ? DomesticRowsCollection[i].HAND : "";
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].HAND).Length > 0 || Convert.ToString(DomesticRowsCollection[i].HAND) != "")
                            {
                                HandBagWeight = HandBagWeight + "|" + Convert.ToString(DomesticRowsCollection[i].HAND);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing hand bag weight  piped string 
                        if (Remarks == string.Empty || Remarks.Length == 0)
                        {
                            Remarks = Convert.ToString(DomesticRowsCollection[i].REMARKS);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].REMARKS).Length > 0 || Convert.ToString(DomesticRowsCollection[i].REMARKS) != "")
                            {
                                Remarks = Remarks + "|" + Convert.ToString(DomesticRowsCollection[i].REMARKS);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing hand bag weight  piped string 
                        if (AdvanceAmount == string.Empty || AdvanceAmount.Length == 0)
                        {
                            AdvanceAmount = Convert.ToString(DomesticRowsCollection[i].ADVANCE);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].ADVANCE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].ADVANCE) != "")
                            {
                                AdvanceAmount = AdvanceAmount + "|" + Convert.ToString(DomesticRowsCollection[i].ADVANCE);
                            }
                            else
                            {
                                continue;
                            }
                        }

                    }
                }
                #endregion
                ////-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region "Constructing the international piped string"
                List<requisitionbo> InternationalRowsCollections = new List<requisitionbo>();
                if (Session["InternationalSegmentList"] != null)
                {
                    InternationalRowsCollections = (List<requisitionbo>)Session["InternationalSegmentList"];
                }
                for (int i = 0; i < InternationalRowsCollections.Count; i++)
                {
                    //Validating the international date fields before constructin piped string. Skip the row if date is empty .
                    if (!string.IsNullOrEmpty(Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE_ALL)))
                    {
                        //Construction travel type piped string.
                        if (TypeOfTravel == string.Empty || TypeOfTravel.Length == 0)
                        {
                            TypeOfTravel = Convert.ToString((int)TravelType.International);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE) != "")
                            {
                                TypeOfTravel = TypeOfTravel + "|" + Convert.ToString((int)TravelType.International);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        //Constructing travel date piped string 
                        if (TravelDate == string.Empty || TravelDate.Length == 0)
                        {
                            TravelDate = Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE) != "")
                            {
                                TravelDate = TravelDate + "|" + Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing media of transport piped string 
                        if (MediaOfTransport == string.Empty || MediaOfTransport.Length == 0)
                        {
                            MediaOfTransport = Convert.ToString(InternationalRowsCollections[i].MODE_OF_TRANSPOPRT_KZPMF);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].MODE_OF_TRANSPOPRT_KZPMF).Length > 0 || Convert.ToString(InternationalRowsCollections[i].MODE_OF_TRANSPOPRT_KZPMF) != "")
                            {
                                MediaOfTransport = MediaOfTransport + "|" + Convert.ToString(InternationalRowsCollections[i].MODE_OF_TRANSPOPRT_KZPMF);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing medium of category piped string 
                        if (MediumOfCategory == string.Empty || MediumOfCategory.Length == 0)
                        {
                            MediumOfCategory = Convert.ToString(InternationalRowsCollections[i].MEDIA_OF_CATEGORY_PKWKL);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].MEDIA_OF_CATEGORY_PKWKL).Length > 0 || Convert.ToString(InternationalRowsCollections[i].MEDIA_OF_CATEGORY_PKWKL) != "")
                            {
                                MediumOfCategory = MediumOfCategory + "|" + Convert.ToString(InternationalRowsCollections[i].MEDIA_OF_CATEGORY_PKWKL);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing airline piped string 
                        if (Airline == string.Empty || Airline.Length == 0)
                        {
                            Airline = !string.IsNullOrEmpty(InternationalRowsCollections[i].AIRLINE) ? InternationalRowsCollections[i].AIRLINE : "";

                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].AIRLINE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].AIRLINE) != "")
                            {
                                Airline = Airline + "|" + Convert.ToString(InternationalRowsCollections[i].AIRLINE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing from place piped string 
                        if (From == string.Empty || From.Length == 0)
                        {
                            From = Convert.ToString(InternationalRowsCollections[i].REGION_RGION_FROM);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].REGION_RGION_FROM).Length > 0 || Convert.ToString(InternationalRowsCollections[i].REGION_RGION_FROM) != "")
                            {
                                From = From + "|" + Convert.ToString(InternationalRowsCollections[i].REGION_RGION_FROM);

                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing 'To' place piped string 
                        if (To == string.Empty || To.Length == 0)
                        {
                            To = Convert.ToString(InternationalRowsCollections[i].REGION_RGION_TO);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].REGION_RGION_TO).Length > 0 || Convert.ToString(InternationalRowsCollections[i].REGION_RGION_TO) != "")
                            {
                                To = To + "|" + Convert.ToString(InternationalRowsCollections[i].REGION_RGION_TO);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing visa required piped string 
                        if (VisaRequired == string.Empty || VisaRequired.Length == 0)
                        {
                            if (InternationalRowsCollections[i].VISA_REQUIRED == true)
                            {
                                VisaRequired = "true";
                            }
                            else
                                VisaRequired = "false";
                        }
                        else
                        {
                            if (InternationalRowsCollections[i].VISA_REQUIRED == true)
                            {
                                VisaRequired = VisaRequired + "|" + "true";
                            }
                            else
                            {
                                VisaRequired = VisaRequired + "|" + "false";
                            }
                        }
                        //Constructing foreign exchange piped string 
                        if (ForeignExchange == string.Empty || ForeignExchange.Length == 0)
                        {
                            ForeignExchange = !string.IsNullOrEmpty(InternationalRowsCollections[i].FR_EXCHANGE) ? InternationalRowsCollections[i].FR_EXCHANGE : "";
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].FR_EXCHANGE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].FR_EXCHANGE) != "")
                            {
                                ForeignExchange = ForeignExchange + "|" + Convert.ToString(InternationalRowsCollections[i].FR_EXCHANGE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing insurance mediclaim piped string 
                        if (InsurMediclaim == string.Empty || InsurMediclaim.Length == 0)
                        {
                            InsurMediclaim = !string.IsNullOrEmpty(InternationalRowsCollections[i].INSUR_MEDICLAIM) ? InternationalRowsCollections[i].INSUR_MEDICLAIM : "";
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].INSUR_MEDICLAIM).Length > 0 || Convert.ToString(InternationalRowsCollections[i].INSUR_MEDICLAIM) != "")
                            {
                                InsurMediclaim = InsurMediclaim + "|" + Convert.ToString(InternationalRowsCollections[i].INSUR_MEDICLAIM);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing frequently fly over number piped string 
                        if (FFlyNo == string.Empty || FFlyNo.Length == 0)
                        {
                            FFlyNo = Convert.ToString(InternationalRowsCollections[i].FLYNUM);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].FLYNUM).Length > 0 || Convert.ToString(InternationalRowsCollections[i].FLYNUM) != "")
                            {
                                FFlyNo = FFlyNo + "|" + Convert.ToString(InternationalRowsCollections[i].FLYNUM);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing seat prefrence  piped string 
                        if (SeatPreference == string.Empty || SeatPreference.Length == 0)
                        {
                            SeatPreference = !string.IsNullOrEmpty(InternationalRowsCollections[i].SEAT_PREFERENCE) ? InternationalRowsCollections[i].SEAT_PREFERENCE : "";
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].SEAT_PREFERENCE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].SEAT_PREFERENCE) != "")
                            {
                                SeatPreference = SeatPreference + "|" + Convert.ToString(InternationalRowsCollections[i].SEAT_PREFERENCE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing meal prefrence  piped string 
                        if (MealPreference == string.Empty || MealPreference.Length == 0)
                        {
                            MealPreference = !string.IsNullOrEmpty(InternationalRowsCollections[i].MEAL_PREFERENCE) ? InternationalRowsCollections[i].MEAL_PREFERENCE : "";
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].MEAL_PREFERENCE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].MEAL_PREFERENCE) != "")
                            {
                                MealPreference = MealPreference + "|" + Convert.ToString(InternationalRowsCollections[i].MEAL_PREFERENCE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing baggage  piped string 
                        if (Baggage == string.Empty || Baggage.Length == 0)
                        {
                            Baggage = !string.IsNullOrEmpty(InternationalRowsCollections[i].BAGGAGE) ? InternationalRowsCollections[i].BAGGAGE : "";
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].BAGGAGE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].BAGGAGE) != "")
                            {
                                Baggage = Baggage + "|" + Convert.ToString(InternationalRowsCollections[i].BAGGAGE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing hand bag weight  piped string 
                        if (HandBagWeight == string.Empty || HandBagWeight.Length == 0)
                        {
                            HandBagWeight = !string.IsNullOrEmpty(InternationalRowsCollections[i].HAND) ? InternationalRowsCollections[i].HAND : "";
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].HAND).Length > 0 || Convert.ToString(InternationalRowsCollections[i].HAND) != "")
                            {
                                HandBagWeight = HandBagWeight + "|" + Convert.ToString(InternationalRowsCollections[i].HAND);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing hand bag weight  piped string 
                        if (Remarks == string.Empty || Remarks.Length == 0)
                        {
                            Remarks = Convert.ToString(InternationalRowsCollections[i].REMARKS);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].REMARKS).Length > 0 || Convert.ToString(InternationalRowsCollections[i].REMARKS) != "")
                            {
                                Remarks = Remarks + "|" + Convert.ToString(InternationalRowsCollections[i].REMARKS);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing hand bag weight  piped string 
                        if (AdvanceAmount == string.Empty || AdvanceAmount.Length == 0)
                        {
                            AdvanceAmount = Convert.ToString(InternationalRowsCollections[i].ADVANCE);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].ADVANCE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].ADVANCE) != "")
                            {
                                AdvanceAmount = AdvanceAmount + "|" + Convert.ToString(InternationalRowsCollections[i].ADVANCE);
                            }
                            else
                            {
                                continue;
                            }
                        }

                    }
                }
                #endregion
                ///-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                requisitionboObject.CHK = TypeOfTravel;
                requisitionboObject.TRAVEL_DATE_ALL = TravelDate;
                requisitionboObject.TRAVEL_TIME = DepartureTime;
                requisitionboObject.MODE_OF_TRANSPOPRT_KZPMF = MediaOfTransport;
                requisitionboObject.MEDIA_OF_CATEGORY_PKWKL = MediumOfCategory;
                requisitionboObject.AIRLINE = Airline;
                requisitionboObject.VEHICLE_NAME_VHNUM_ALL = VehicleNumber;
                requisitionboObject.REGION_RGION_FROM = From;
                requisitionboObject.REGION_RGION_TO = To;
                requisitionboObject.VISA_REQUIRED_ALL = VisaRequired;
                requisitionboObject.FR_EXCHANGE = ForeignExchange;
                requisitionboObject.INSUR_MEDICLAIM = InsurMediclaim;
                requisitionboObject.FLYNUM = FFlyNo;
                requisitionboObject.SEAT_PREFERENCE = SeatPreference;
                requisitionboObject.MEAL_PREFERENCE = MealPreference;
                requisitionboObject.BAGGAGE = Baggage;
                requisitionboObject.HAND = HandBagWeight;
                requisitionboObject.REMARKS = Remarks;
                requisitionboObject.ADVANCE = AdvanceAmount;
                requisitionboObject.REQ_SEGMENT_ID = Convert.ToInt32(Session["FTPT_REQ_SEGMENT_ID"].ToString());
                requisitionboObject.FTPT_REQUEST_ID = Convert.ToInt32(Session["FTPT_REQUISITION_ID"].ToString());
                requisitionboObject.TRAVEL_DATE = Convert.ToDateTime(TravelDate);
                requisitionboObject.VEHICLE_NAME_VHNUM = Convert.ToString(VehicleNumber);
                requisitionboObject.VISA_REQUIRED = Convert.ToBoolean(VisaRequired);
                int iResult = travelrequestBl.Update_TravelRequest_for_TravelDesk(requisitionboObject);
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                if (iResult == 0)
                {
                    ClearControls();
                    lblMessageBoard.Text = "Requisition updated successfully.";
                }
                else
                {
                    lblMessageBoard.Text = "Unknown error occured.";
                }
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Unknown error occured. Please contact your system administrator.";
                return;
            }
        }
        bool ValidateControl()
        {
            ////----start---------  Domestic and international date validation.
            try
            {
                for (int j = 0; j < InternationalGridView.Rows.Count; j++)
                {
                    TextBox InternationalTextDateTravel = (TextBox)InternationalGridView.Rows[0].FindControl("TextDateTravel");
                    if (InternationalTextDateTravel.Text != "")
                    {
                        DateTime dt = DateTime.ParseExact(InternationalTextDateTravel.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        lblMessageBoard.Text = "";

                    }
                }
                for (int j = 0; j < DomesticGridView.Rows.Count; j++)
                {
                    TextBox domesticTextDateTravel = (TextBox)DomesticGridView.Rows[0].FindControl("domesticTextDateTravel");
                    if (domesticTextDateTravel.Text != "")
                    {
                        DateTime dt = DateTime.ParseExact(domesticTextDateTravel.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        lblMessageBoard.Text = "";
                    }

                }
            }
            catch
            {
                for (int j = 0; j < InternationalGridView.Rows.Count; j++)
                {
                    TextBox InternationalTextDateTravel = (TextBox)InternationalGridView.Rows[0].FindControl("TextDateTravel");

                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Enter valid  date (DD-MM-YYYY).";
                    SetFocus(InternationalTextDateTravel);
                    return false;
                }

                for (int j = 0; j < DomesticGridView.Rows.Count; j++)
                {
                    TextBox domesticTextDateTravel = (TextBox)DomesticGridView.Rows[0].FindControl("domesticTextDateTravel");
                    if (domesticTextDateTravel.Text != "")
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Enter valid date (DD-MM-YYYY).";
                        SetFocus(domesticTextDateTravel);
                    }
                    return false;
                }
            }
            ////----end---------  Domestic and international date validation.
            //----start---------  Domestic and international time validation.
            try
            {
                for (int j = 0; j < DomesticGridView.Rows.Count; j++)
                {
                    TextBox domesticTextTimeDeparture = (TextBox)DomesticGridView.Rows[0].FindControl("domesticTextTimeDeparture");
                    if (domesticTextTimeDeparture.Text != "")
                    {
                        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([0-1][0-9]|2[0-3]):([0-5][0-9])$");
                        if (regex.IsMatch(domesticTextTimeDeparture.Text))
                        {
                            lblMessageBoard.Text = "";
                        }
                        else
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            lblMessageBoard.Text = "Please enter correct arrival time. The time should be in 24 hour format";
                            SetFocus(domesticTextTimeDeparture);
                            return false;
                        }
                    }

                }
            }
            catch
            {
                for (int j = 0; j < DomesticGridView.Rows.Count; j++)
                {
                    TextBox domesticTextTimeDeparture = (TextBox)DomesticGridView.Rows[0].FindControl("domesticTextTimeDeparture");
                    if (domesticTextTimeDeparture.Text != "")
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Please enter correct time. The time should be in 24 hour format";
                        SetFocus(domesticTextTimeDeparture);
                    }
                    return false;
                }
            }
            ////----end---------  Domestic and international time validation.

            return true;
        }
        protected void SendProposal_Click(object sender, EventArgs e)
        {
            if (TD_RemarksTextBox.Text == "" || TD_RemarksTextBox.Text == string.Empty)
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Please enter Remarks";
                return;
            }
            
            if (ValidateControl())
            {
                requisitionbo requisitionboObject = new requisitionbo();
                td_requisitionbl td_requisitionBl = new td_requisitionbl();
                string RequisitionID = string.Empty;
                string RequisitionSegmentID = string.Empty;
                string EmployeeNo = string.Empty;
                string TravelDate = string.Empty;
                string DepartureTime = string.Empty;
                string MediaOfTransport = string.Empty;
                string MediumOfCategory = string.Empty;
                string Airline = string.Empty;
                string VehicleNumber = string.Empty;
                string VehicleName = string.Empty;
                string From = string.Empty;
                string FromName = string.Empty;
                string To = string.Empty;
                string ToName = string.Empty;
                string VisaRequired = string.Empty;
                string ForeignExchange = string.Empty;
                string InsurMediclaim = string.Empty;
                string FFlyNo = string.Empty;
                string SeatPreference = string.Empty;
                string MealPreference = string.Empty;
                string Baggage = string.Empty;
                string HandBagWeight = string.Empty;
                string Remarks = string.Empty;
                Remarks = TD_RemarksTextBox.Text;
                string AdvanceAmount = string.Empty;
                string TypeOfTravel = string.Empty;
                int DomesticTravelType = -1; //Used for mail
                int InternationalTravelType = -1; //Used for mail
                int DomesticSegmentCount = 0; //Used for Accomodation and Vehicle requisition save
                int InternationalSegmentCount = 0;//Used for Accomodation and Vehicle requisition save
                //Common fields for each record.
                requisitionboObject.CREATED_BY = User.Identity.Name.Trim();
                requisitionboObject.CURRENT_STATUS = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.NewProposal.ToString()));  //get the enum value of 'New' status
                requisitionboObject.ISACTIVE = 1;
                #region "Constructing the domestic piped string"
                List<requisitionbo> DomesticRowsCollection = new List<requisitionbo>();
                if (Session["DomesticSegmentList"] != null)
                {
                    DomesticRowsCollection = (List<requisitionbo>)Session["DomesticSegmentList"];
                }
                DomesticSegmentCount = DomesticRowsCollection.Count;
                for (int i = 0; i < DomesticRowsCollection.Count; i++)
                {
                    //Validating the domestic date fields before constructin piped string. Skip the row if date is empty .
                    if (!string.IsNullOrEmpty(Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE)))
                    {
                        DomesticTravelType = (int)TravelType.Domestic;
                        //Construction travel type piped string.
                        if (TypeOfTravel == string.Empty || TypeOfTravel.Length == 0)
                        {
                            TypeOfTravel = Convert.ToString((int)TravelType.Domestic);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE) != "")
                            {
                                TypeOfTravel = TypeOfTravel + "|" + Convert.ToString((int)TravelType.Domestic);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing RequisitionID piped string
                        if (RequisitionID == string.Empty || RequisitionID.Length == 0)
                        {
                            RequisitionID = Convert.ToString(DomesticRowsCollection[i].FTPT_REQUEST_ID);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].FTPT_REQUEST_ID).Length > 0 || Convert.ToString(DomesticRowsCollection[i].FTPT_REQUEST_ID) != "")
                            {
                                RequisitionID = RequisitionID + "|" + Convert.ToString(DomesticRowsCollection[i].FTPT_REQUEST_ID);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing RequisitionSegmentID piped string
                        if (RequisitionSegmentID == string.Empty || RequisitionSegmentID.Length == 0)
                        {
                            RequisitionSegmentID = Convert.ToString(DomesticRowsCollection[i].REQ_SEGMENT_ID);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].REQ_SEGMENT_ID).Length > 0 || Convert.ToString(DomesticRowsCollection[i].REQ_SEGMENT_ID) != "")
                            {
                                RequisitionSegmentID = RequisitionSegmentID + "|" + Convert.ToString(DomesticRowsCollection[i].REQ_SEGMENT_ID);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing EmployeeNo piped string
                        if (EmployeeNo == string.Empty || EmployeeNo.Length == 0)
                        {
                            EmployeeNo = Convert.ToString(DomesticRowsCollection[i].EMPLOYEE_NO);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].EMPLOYEE_NO).Length > 0 || Convert.ToString(DomesticRowsCollection[i].EMPLOYEE_NO) != "")
                            {
                                EmployeeNo = EmployeeNo + "|" + Convert.ToString(DomesticRowsCollection[i].EMPLOYEE_NO);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing travel date piped string 
                        if (TravelDate == string.Empty || TravelDate.Length == 0)
                        {
                            TravelDate = Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE);
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE) != "")
                            {
                                TravelDate = TravelDate + "|" + Convert.ToString(DomesticRowsCollection[i].TRAVEL_DATE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing travel time piped string 
                        if (DepartureTime == string.Empty || DepartureTime.Length == 0)
                        {
                            DepartureTime = !string.IsNullOrEmpty(DomesticRowsCollection[i].TRAVEL_TIME) ? DomesticRowsCollection[i].TRAVEL_TIME : "_";
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].TRAVEL_TIME).Length > 0 || Convert.ToString(DomesticRowsCollection[i].TRAVEL_TIME) != "")
                            {
                                DepartureTime = DepartureTime + "|" + Convert.ToString(DomesticRowsCollection[i].TRAVEL_TIME);
                            }
                            else
                            {
                                DepartureTime = DepartureTime + "|" + "_";
                            }
                        }
                        //Constructing media of transport piped string 
                        if (MediaOfTransport == string.Empty || MediaOfTransport.Length == 0)
                        {
                            //If selected index is zero or nothing selected then assig "_" to piped string. 
                            if (Convert.ToString(DomesticRowsCollection[i].MODE_OF_TRANSPOPRT_KZPMF).Length > 0 || Convert.ToString(DomesticRowsCollection[i].MODE_OF_TRANSPOPRT_KZPMF) != "")
                            {
                                MediaOfTransport = Convert.ToString(DomesticRowsCollection[i].MODE_OF_TRANSPOPRT_KZPMF);
                            }
                            else
                            {
                                MediaOfTransport = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].MODE_OF_TRANSPOPRT_KZPMF).Length > 0 || Convert.ToString(DomesticRowsCollection[i].MODE_OF_TRANSPOPRT_KZPMF) != "")
                            {
                                MediaOfTransport = MediaOfTransport + "|" + Convert.ToString(DomesticRowsCollection[i].MODE_OF_TRANSPOPRT_KZPMF);
                            }
                            else
                            {
                                MediaOfTransport = MediaOfTransport + "|" + "_";
                            }
                        }
                        //Constructing medium of category piped string 
                        if (MediumOfCategory == string.Empty || MediumOfCategory.Length == 0)
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].MEDIA_OF_CATEGORY_PKWKL).Length > 0 || Convert.ToString(DomesticRowsCollection[i].MEDIA_OF_CATEGORY_PKWKL) != "")
                            {
                                MediumOfCategory = Convert.ToString(DomesticRowsCollection[i].MEDIA_OF_CATEGORY_PKWKL);
                            }
                            else
                            {
                                MediumOfCategory = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].MEDIA_OF_CATEGORY_PKWKL).Length > 0 || Convert.ToString(DomesticRowsCollection[i].MEDIA_OF_CATEGORY_PKWKL) != "")
                            {
                                MediumOfCategory = MediumOfCategory + "|" + Convert.ToString(DomesticRowsCollection[i].MEDIA_OF_CATEGORY_PKWKL);
                            }
                            else
                            {
                                MediumOfCategory = MediumOfCategory + "|" + "_";
                            }
                        }
                        //Constructing airline piped string 
                        if (Airline == string.Empty || Airline.Length == 0)
                        {
                            Airline = !string.IsNullOrEmpty(DomesticRowsCollection[i].AIRLINE) ? DomesticRowsCollection[i].AIRLINE : "_";
                        }
                        else
                        {

                            Airline = Airline + "|" + "_";

                        }
                        //Constructing vehicle number piped string 
                        if (VehicleNumber == string.Empty || VehicleNumber.Length == 0)
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_VHNUM).Length > 0 || Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_VHNUM) != "")
                            {
                                VehicleNumber = Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_VHNUM);
                                VehicleName = Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_ZZVEHNAM);
                            }
                            else
                            {
                                VehicleNumber = "_";
                                VehicleName = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_VHNUM).Length > 0 || Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_VHNUM) != "")
                            {
                                VehicleNumber = VehicleNumber + "|" + Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_VHNUM);
                                VehicleName = VehicleName + "|" + Convert.ToString(DomesticRowsCollection[i].VEHICLE_NAME_ZZVEHNAM);
                            }
                            else
                            {
                                VehicleNumber = VehicleNumber + "|" + "_";
                                VehicleName = VehicleName + "|" + "_";
                            }
                        }
                        //Constructing from place piped string 
                        if (From == string.Empty || From.Length == 0)
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].REGION_RGION_FROM).Length > 0 || Convert.ToString(DomesticRowsCollection[i].REGION_RGION_FROM) != "")
                            {
                                From = Convert.ToString(DomesticRowsCollection[i].REGION_RGION_FROM);
                                FromName = Convert.ToString(DomesticRowsCollection[i].REGION_TEXT25_FROM);
                            }
                            else
                            {
                                From = "_";
                                FromName = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].REGION_RGION_FROM).Length > 0 || Convert.ToString(DomesticRowsCollection[i].REGION_RGION_FROM) != "")
                            {
                                From = From + "|" + Convert.ToString(DomesticRowsCollection[i].REGION_RGION_FROM);
                                FromName = FromName + "|" + Convert.ToString(DomesticRowsCollection[i].REGION_TEXT25_FROM);
                            }
                            else
                            {
                                From = From + "|" + "_";
                                FromName = FromName + "|" + "_";
                            }
                        }
                        //Constructing 'To' place piped string 
                        if (To == string.Empty || To.Length == 0)
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].REGION_RGION_TO).Length > 0 || Convert.ToString(DomesticRowsCollection[i].REGION_RGION_TO) != "")
                            {
                                To = Convert.ToString(DomesticRowsCollection[i].REGION_RGION_TO);
                                ToName = Convert.ToString(DomesticRowsCollection[i].REGION_TEXT25_TO);
                            }
                            else
                            {
                                To = "_";
                                ToName = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].REGION_RGION_TO).Length > 0 || Convert.ToString(DomesticRowsCollection[i].REGION_RGION_TO) != "")
                            {
                                To = To + "|" + Convert.ToString(DomesticRowsCollection[i].REGION_RGION_TO);
                                ToName = ToName + "|" + Convert.ToString(DomesticRowsCollection[i].REGION_TEXT25_TO);
                            }
                            else
                            {
                                To = To + "|" + "_";
                                ToName = ToName + "|" + "_";
                            }
                        }
                        //Constructing insurance mediclaim piped string 
                        if (InsurMediclaim == string.Empty || InsurMediclaim.Length == 0)
                        {
                            InsurMediclaim = !string.IsNullOrEmpty(DomesticRowsCollection[i].INSUR_MEDICLAIM) ? DomesticRowsCollection[i].INSUR_MEDICLAIM : "_";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(DomesticRowsCollection[i].INSUR_MEDICLAIM))
                            {
                                InsurMediclaim = InsurMediclaim + "|" + "_";
                            }
                        }
                        //Constructing frequently fly over number piped string 
                        if (FFlyNo == string.Empty || FFlyNo.Length == 0)
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].FLYNUM).Length == 0 || Convert.ToString(DomesticRowsCollection[i].FLYNUM) == "")
                            {
                                FFlyNo = "_";
                            }
                            else
                            {
                                FFlyNo = Convert.ToString(DomesticRowsCollection[i].FLYNUM);
                            }
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].FLYNUM).Length > 0 || Convert.ToString(DomesticRowsCollection[i].FLYNUM) != "")
                            {
                                FFlyNo = FFlyNo + "|" + Convert.ToString(DomesticRowsCollection[i].FLYNUM);
                            }
                            else
                            {
                                FFlyNo = FFlyNo + "|" + "_"; //Underscore indicates the user not entered that value.
                            }
                        }
                        //Constructing seat prefrence  piped string 
                        if (SeatPreference == string.Empty || SeatPreference.Length == 0)
                        {
                            SeatPreference = "_";
                        }
                        else
                        {
                            SeatPreference = SeatPreference + "|" + "_";
                        }
                        //Constructing meal prefrence  piped string 
                        if (MealPreference == string.Empty || MealPreference.Length == 0)
                        {
                            MealPreference = "_";
                        }
                        else
                        {
                            MealPreference = MealPreference + "|" + "_";
                        }
                        //Constructing baggage  piped string 
                        if (Baggage == string.Empty || Baggage.Length == 0)
                        {
                            Baggage = "_";
                        }
                        else
                        {
                            Baggage = Baggage + "|" + "_";
                        }
                        //Constructing hand bag weight  piped string 
                        if (HandBagWeight == string.Empty || HandBagWeight.Length == 0)
                        {
                            HandBagWeight = "_";
                        }
                        else
                        {
                            HandBagWeight = HandBagWeight + "|" + "_";
                        }
                        //Constructing hand bag weight  piped string 
                        if (Remarks == string.Empty || Remarks.Length == 0)
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].REMARKS).Length > 0 || Convert.ToString(DomesticRowsCollection[i].REMARKS) != "")
                            {
                                Remarks = Convert.ToString(DomesticRowsCollection[i].REMARKS);
                            }
                            else
                            {
                                Remarks = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].REMARKS).Length > 0 || Convert.ToString(DomesticRowsCollection[i].REMARKS) != "")
                            {
                                Remarks = Remarks + "|" + Convert.ToString(DomesticRowsCollection[i].REMARKS);
                            }
                            else
                            {
                                Remarks = Remarks + "|" + "_";
                            }
                        }
                        //Constructing hand bag weight  piped string 
                        if (AdvanceAmount == string.Empty || AdvanceAmount.Length == 0)
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].ADVANCE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].ADVANCE) != "")
                            {
                                AdvanceAmount = Convert.ToString(DomesticRowsCollection[i].ADVANCE);
                            }
                            else
                            {
                                AdvanceAmount = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(DomesticRowsCollection[i].ADVANCE).Length > 0 || Convert.ToString(DomesticRowsCollection[i].ADVANCE) != "")
                            {
                                AdvanceAmount = AdvanceAmount + "|" + Convert.ToString(DomesticRowsCollection[i].ADVANCE);
                            }
                            else
                            {
                                AdvanceAmount = AdvanceAmount + "|" + "_";
                            }
                        }

                    }
                }
                #endregion
                ////-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region "Constructing the international piped string"
                List<requisitionbo> InternationalRowsCollections = new List<requisitionbo>();
                if (Session["InternationalSegmentList"] != null)
                {
                    InternationalRowsCollections = (List<requisitionbo>)Session["InternationalSegmentList"];
                }
                InternationalSegmentCount = InternationalRowsCollections.Count;
                for (int i = 0; i < InternationalRowsCollections.Count; i++)
                {
                    //Validating the international date fields before constructin piped string. Skip the row if date is empty .
                    if (!string.IsNullOrEmpty(Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE)))
                    {
                        InternationalTravelType = (int)TravelType.International;
                        //Construction travel type piped string.
                        if (TypeOfTravel == string.Empty || TypeOfTravel.Length == 0)
                        {
                            TypeOfTravel = Convert.ToString((int)TravelType.International);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE) != "")
                            {
                                TypeOfTravel = TypeOfTravel + "|" + Convert.ToString((int)TravelType.International);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing RequisitionID piped string
                        if (RequisitionID == string.Empty || RequisitionID.Length == 0)
                        {
                            RequisitionID = Convert.ToString(InternationalRowsCollections[i].FTPT_REQUEST_ID);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].FTPT_REQUEST_ID).Length > 0 || Convert.ToString(InternationalRowsCollections[i].FTPT_REQUEST_ID) != "")
                            {
                                RequisitionID = RequisitionID + "|" + Convert.ToString(InternationalRowsCollections[i].FTPT_REQUEST_ID);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing RequisitionSegmentID piped string
                        if (RequisitionSegmentID == string.Empty || RequisitionSegmentID.Length == 0)
                        {
                            RequisitionSegmentID = Convert.ToString(InternationalRowsCollections[i].REQ_SEGMENT_ID);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].REQ_SEGMENT_ID).Length > 0 || Convert.ToString(InternationalRowsCollections[i].REQ_SEGMENT_ID) != "")
                            {
                                RequisitionSegmentID = RequisitionSegmentID + "|" + Convert.ToString(InternationalRowsCollections[i].REQ_SEGMENT_ID);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing EmployeeNo piped string
                        if (EmployeeNo == string.Empty || EmployeeNo.Length == 0)
                        {
                            EmployeeNo = Convert.ToString(InternationalRowsCollections[i].EMPLOYEE_NO);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].EMPLOYEE_NO).Length > 0 || Convert.ToString(InternationalRowsCollections[i].EMPLOYEE_NO) != "")
                            {
                                EmployeeNo = EmployeeNo + "|" + Convert.ToString(InternationalRowsCollections[i].EMPLOYEE_NO);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing travel date piped string 
                        if (TravelDate == string.Empty || TravelDate.Length == 0)
                        {
                            TravelDate = Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE);
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE) != "")
                            {
                                TravelDate = TravelDate + "|" + Convert.ToString(InternationalRowsCollections[i].TRAVEL_DATE);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        //Constructing travel time piped string 
                        if (DepartureTime == string.Empty || DepartureTime.Length == 0)
                        {
                            DepartureTime = !string.IsNullOrEmpty(InternationalRowsCollections[i].TRAVEL_TIME) ? InternationalRowsCollections[i].TRAVEL_TIME : "_";
                        }
                        else
                        {
                            DepartureTime = DepartureTime + "|" + "_";
                        }
                        //Constructing media of transport piped string 
                        if (MediaOfTransport == string.Empty || MediaOfTransport.Length == 0)
                        {
                            //If selected index is zero or nothing selected then assig "_" to piped string. 
                            if (Convert.ToString(InternationalRowsCollections[i].MODE_OF_TRANSPOPRT_KZPMF).Length > 0 || Convert.ToString(InternationalRowsCollections[i].MODE_OF_TRANSPOPRT_KZPMF) != "")
                            {
                                MediaOfTransport = Convert.ToString(InternationalRowsCollections[i].MODE_OF_TRANSPOPRT_KZPMF);
                            }
                            else
                            {
                                MediaOfTransport = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].MODE_OF_TRANSPOPRT_KZPMF).Length > 0 || Convert.ToString(InternationalRowsCollections[i].MODE_OF_TRANSPOPRT_KZPMF) != "")
                            {
                                MediaOfTransport = MediaOfTransport + "|" + Convert.ToString(InternationalRowsCollections[i].MODE_OF_TRANSPOPRT_KZPMF);
                            }
                            else
                            {
                                MediaOfTransport = MediaOfTransport + "|" + "_";
                            }
                        }
                        //Constructing medium of category piped string 
                        if (MediumOfCategory == string.Empty || MediumOfCategory.Length == 0)
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].MEDIA_OF_CATEGORY_PKWKL).Length > 0 || Convert.ToString(InternationalRowsCollections[i].MEDIA_OF_CATEGORY_PKWKL) != "")
                            {
                                MediumOfCategory = Convert.ToString(InternationalRowsCollections[i].MEDIA_OF_CATEGORY_PKWKL);
                            }
                            else
                            {
                                MediumOfCategory = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].MEDIA_OF_CATEGORY_PKWKL).Length > 0 || Convert.ToString(InternationalRowsCollections[i].MEDIA_OF_CATEGORY_PKWKL) != "")
                            {
                                MediumOfCategory = MediumOfCategory + "|" + Convert.ToString(InternationalRowsCollections[i].MEDIA_OF_CATEGORY_PKWKL);
                            }
                            else
                            {
                                MediumOfCategory = MediumOfCategory + "|" + "_";
                            }
                        }
                        //Constructing airline piped string 
                        if (Airline == string.Empty || Airline.Length == 0)
                        {
                            Airline = !string.IsNullOrEmpty(InternationalRowsCollections[i].AIRLINE) ? InternationalRowsCollections[i].AIRLINE : "_";
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].AIRLINE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].AIRLINE) != "")
                            {
                                Airline = Airline + "|" + Convert.ToString(InternationalRowsCollections[i].AIRLINE);
                            }
                            else
                            {
                                Airline = Airline + "|" + "_";
                            }
                        }
                        //Constructing vehicle number piped string 
                        if (VehicleNumber == string.Empty || VehicleNumber.Length == 0)
                        {
                            VehicleNumber = "_";
                            VehicleName = "_";
                        }
                        else
                        {
                            VehicleNumber = VehicleNumber + "|" + "_";
                            VehicleName = VehicleName + "|" + "_";
                        }
                        //Constructing from place piped string 
                        if (From == string.Empty || From.Length == 0)
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].REGION_RGION_FROM).Length > 0 || Convert.ToString(InternationalRowsCollections[i].REGION_RGION_FROM) != "")
                            {
                                From = Convert.ToString(InternationalRowsCollections[i].REGION_RGION_FROM);
                                FromName = Convert.ToString(InternationalRowsCollections[i].REGION_TEXT25_FROM);
                            }
                            else
                            {
                                From = "_";
                                FromName = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].REGION_RGION_FROM).Length > 0 || Convert.ToString(InternationalRowsCollections[i].REGION_RGION_FROM) != "")
                            {
                                From = From + "|" + Convert.ToString(InternationalRowsCollections[i].REGION_RGION_FROM);
                                FromName = FromName + "|" + Convert.ToString(InternationalRowsCollections[i].REGION_TEXT25_FROM);
                            }
                            else
                            {
                                From = From + "|" + "_";
                                FromName = FromName + "|" + "_";
                            }
                        }
                        //Constructing 'To' place piped string 
                        if (To == string.Empty || To.Length == 0)
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].REGION_RGION_TO).Length > 0 || Convert.ToString(InternationalRowsCollections[i].REGION_RGION_TO) != "")
                            {
                                To = Convert.ToString(InternationalRowsCollections[i].REGION_RGION_TO);
                                ToName = Convert.ToString(InternationalRowsCollections[i].REGION_TEXT25_TO);
                            }
                            else
                            {
                                To = "_";
                                ToName = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].REGION_RGION_TO).Length > 0 || Convert.ToString(InternationalRowsCollections[i].REGION_RGION_TO) != "")
                            {
                                To = To + "|" + Convert.ToString(InternationalRowsCollections[i].REGION_RGION_TO);
                                ToName = ToName + "|" + Convert.ToString(InternationalRowsCollections[i].REGION_TEXT25_TO);
                            }
                            else
                            {
                                To = To + "|" + "_";
                                ToName = ToName + "|" + "_";
                            }
                        }
                        //Constructing insurance mediclaim piped string 
                        if (InsurMediclaim == string.Empty || InsurMediclaim.Length == 0)
                        {
                            InsurMediclaim = !string.IsNullOrEmpty(InternationalRowsCollections[i].INSUR_MEDICLAIM) ? InternationalRowsCollections[i].INSUR_MEDICLAIM : "_";
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].INSUR_MEDICLAIM).Length > 0 || Convert.ToString(InternationalRowsCollections[i].INSUR_MEDICLAIM) != "")
                            {
                                InsurMediclaim = InsurMediclaim + "|" + Convert.ToString(InternationalRowsCollections[i].INSUR_MEDICLAIM);
                            }
                            else
                            {
                                InsurMediclaim = InsurMediclaim + "|" + "_";
                            }
                        }
                        //Constructing frequently fly over number piped string 
                        if (FFlyNo == string.Empty || FFlyNo.Length == 0)
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].FLYNUM).Length == 0 || Convert.ToString(InternationalRowsCollections[i].FLYNUM) == "")
                            {
                                FFlyNo = "_";
                            }
                            else
                            {
                                FFlyNo = Convert.ToString(InternationalRowsCollections[i].FLYNUM);
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].FLYNUM).Length > 0 || Convert.ToString(InternationalRowsCollections[i].FLYNUM) != "")
                            {
                                FFlyNo = FFlyNo + "|" + Convert.ToString(InternationalRowsCollections[i].FLYNUM);
                            }
                            else
                            {
                                FFlyNo = FFlyNo + "|" + "_"; //Underscore indicates the user not entered that value.
                                // continue;
                            }
                        }
                        //Constructing seat prefrence  piped string 
                        if (SeatPreference == string.Empty || SeatPreference.Length == 0)
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].SEAT_PREFERENCE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].SEAT_PREFERENCE) != "")
                            {
                                SeatPreference = Convert.ToString(InternationalRowsCollections[i].SEAT_PREFERENCE);
                            }
                            else
                            {
                                SeatPreference = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].SEAT_PREFERENCE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].SEAT_PREFERENCE) != "")
                            {
                                SeatPreference = SeatPreference + "|" + Convert.ToString(InternationalRowsCollections[i].SEAT_PREFERENCE);
                            }
                            else
                            {
                                SeatPreference = SeatPreference + "|" + "_";
                            }
                        }
                        //Constructing meal prefrence  piped string 
                        if (MealPreference == string.Empty || MealPreference.Length == 0)
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].MEAL_PREFERENCE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].MEAL_PREFERENCE) != "")
                            {
                                MealPreference = Convert.ToString(InternationalRowsCollections[i].MEAL_PREFERENCE);
                            }
                            else
                            {
                                MealPreference = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].MEAL_PREFERENCE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].MEAL_PREFERENCE) != "")
                            {
                                MealPreference = MealPreference + "|" + Convert.ToString(InternationalRowsCollections[i].MEAL_PREFERENCE);
                            }
                            else
                            {
                                MealPreference = MealPreference + "|" + "_";
                            }
                        }
                        //Constructing baggage  piped string 
                        if (Baggage == string.Empty || Baggage.Length == 0)
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].BAGGAGE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].BAGGAGE) != "")
                            {
                                Baggage = Convert.ToString(InternationalRowsCollections[i].BAGGAGE);
                            }
                            else
                            {
                                Baggage = Baggage + "|" + "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].BAGGAGE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].BAGGAGE) != "")
                            {
                                Baggage = Baggage + "|" + Convert.ToString(InternationalRowsCollections[i].BAGGAGE);
                            }
                            else
                            {
                                Baggage = Baggage + "|" + "_";
                            }
                        }
                        //Constructing hand bag weight  piped string 
                        if (HandBagWeight == string.Empty || HandBagWeight.Length == 0)
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].HAND).Length > 0 || Convert.ToString(InternationalRowsCollections[i].HAND) != "")
                            {
                                HandBagWeight = Convert.ToString(InternationalRowsCollections[i].HAND);
                            }
                            else
                            {
                                HandBagWeight = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].HAND).Length > 0 || Convert.ToString(InternationalRowsCollections[i].HAND) != "")
                            {
                                HandBagWeight = HandBagWeight + "|" + Convert.ToString(InternationalRowsCollections[i].HAND);
                            }
                            else
                            {
                                HandBagWeight = HandBagWeight + "|" + "_";
                            }
                        }
                        //Constructing hand bag weight  piped string 
                        if (Remarks == string.Empty || Remarks.Length == 0)
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].REMARKS).Length > 0 || Convert.ToString(InternationalRowsCollections[i].REMARKS) != "")
                            {
                                Remarks = Convert.ToString(InternationalRowsCollections[i].REMARKS);
                            }
                            else
                            {
                                Remarks = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].REMARKS).Length > 0 || Convert.ToString(InternationalRowsCollections[i].REMARKS) != "")
                            {
                                Remarks = Remarks + "|" + Convert.ToString(InternationalRowsCollections[i].REMARKS);
                            }
                            else
                            {
                                Remarks = Remarks + "|" + "_";
                            }
                        }
                        //Constructing hand bag weight  piped string 
                        if (AdvanceAmount == string.Empty || AdvanceAmount.Length == 0)
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].ADVANCE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].ADVANCE) != "")
                            {
                                AdvanceAmount = Convert.ToString(InternationalRowsCollections[i].ADVANCE);
                            }
                            else
                            {
                                AdvanceAmount = "_";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(InternationalRowsCollections[i].ADVANCE).Length > 0 || Convert.ToString(InternationalRowsCollections[i].ADVANCE) != "")
                            {
                                AdvanceAmount = AdvanceAmount + "|" + Convert.ToString(InternationalRowsCollections[i].ADVANCE);
                            }
                            else
                            {
                                AdvanceAmount = AdvanceAmount + "|" + "_";
                            }
                        }

                    }
                }
                #endregion
                ///-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                requisitionboObject.EMPLOYEE_NO = EmployeeNo;
                requisitionboObject.FTPT_REQUEST_ID_FOR_PROPOSAL = RequisitionID;
                requisitionboObject.REQ_SEGMENT_ID_FOR_PROPOSAL = RequisitionSegmentID;
                requisitionboObject.CHK = TypeOfTravel;
                requisitionboObject.TRAVEL_DATE_ALL = TravelDate;
                requisitionboObject.TRAVEL_TIME = DepartureTime;
                requisitionboObject.MODE_OF_TRANSPOPRT_KZPMF = MediaOfTransport;
                requisitionboObject.MEDIA_OF_CATEGORY_PKWKL = MediumOfCategory;
                requisitionboObject.AIRLINE = Airline;
                requisitionboObject.VEHICLE_NAME_VHNUM_ALL = VehicleNumber;
                requisitionboObject.REGION_RGION_FROM = From;
                requisitionboObject.REGION_RGION_TO = To;
                requisitionboObject.VISA_REQUIRED_ALL = VisaRequired;
                requisitionboObject.FR_EXCHANGE = ForeignExchange;
                requisitionboObject.INSUR_MEDICLAIM = InsurMediclaim;
                requisitionboObject.FLYNUM = FFlyNo;
                requisitionboObject.SEAT_PREFERENCE = SeatPreference;
                requisitionboObject.MEAL_PREFERENCE = MealPreference;
                requisitionboObject.BAGGAGE = Baggage;
                requisitionboObject.HAND = HandBagWeight;
                requisitionboObject.REMARKS = Remarks;
                requisitionboObject.ADVANCE = AdvanceAmount;
                int? ProposalID = 0;
                string ProposalSegmentID = null;
                int iResult = td_requisitionBl.Create_ProposalRequest(requisitionboObject, ref ProposalID, ref ProposalSegmentID);
                if (iResult == 0)
                {
                    td_requisitionBl.UpdateRequistionStatus(requisitionboObject);
                    //Updating accomodation requisiton
                    UpdateAccomodationRequisition(Convert.ToInt32(requisitionboObject.FTPT_REQUEST_ID_FOR_PROPOSAL), Convert.ToInt32(requisitionboObject.REQ_SEGMENT_ID_FOR_PROPOSAL), Convert.ToInt32(requisitionboObject.CHK));
                    //Updating  vehicle requisiton
                    UpdateVehicleRequisition(Convert.ToInt32(requisitionboObject.FTPT_REQUEST_ID_FOR_PROPOSAL), Convert.ToInt32(requisitionboObject.REQ_SEGMENT_ID_FOR_PROPOSAL), Convert.ToInt32(requisitionboObject.CHK));
                    string TripS = Convert.ToString(TravelTypeDropDownList.SelectedItem.Text.ToString());
                    ClearControls();
                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    lblMessageBoard.Text = "Proposal sent successfully.";
                    ClearDomesticControl();
                    ClearInternationalControl();
                    Session["TD_REQ_SEGMENT_ID"] = null;
                    Session["TD_REQ_AccomID"] = null;
                    Session["TD_REQ_VehicleID"] = null;
                    LoadTravelRequestGridView(string.Empty, string.Empty, string.Empty, string.Empty);
                }
                else
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Unknown error occured.";
                }
            }
        }

        void UpdateAccomodationRequisition(int FTPT_REQUEST_ID, int REQ_SEGMENT_ID, int TravelType)
        {
            //updating accomodation details-----domestic-------
            if (Session["AccomodationRequisitionDomestic"] != null)
            {
                List<accomodation_requisitionbo> AccomodationList = new List<accomodation_requisitionbo>();
                accomodation_requisitionbl objbl = new accomodation_requisitionbl();
                AccomodationList = (List<accomodation_requisitionbo>)Session["AccomodationRequisitionDomestic"];
                int iResultAccommodationDomesticDelete = objbl.Delete_accomodation_requisitionbl(FTPT_REQUEST_ID, REQ_SEGMENT_ID, TravelType);
                foreach (accomodation_requisitionbo objBO in AccomodationList)
                {
                    objBO.CREATEDON = System.DateTime.Now;
                    objBO.REQUEST_ID = FTPT_REQUEST_ID;
                    objBO.REQ_SEGMENT_ID_FROM_TRAVEL_REQUEST = (int)objBO.REQ_SEGMENT_ID;
                    int iResultAccommodationDomestic = objbl.Create_accomodation_requisitionbl(objBO);

                    if (iResultAccommodationDomestic == 0)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = "Requisition updated successfully.";
                    }
                    else
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Unknown error occured.";
                    }
                }
            }

            //Update Accommodation details -------international--------
            if (Session["AccomodationRequisitionInternational"] != null)
            {
                List<accomodation_requisitionbo> AccomodationList = new List<accomodation_requisitionbo>();
                accomodation_requisitionbl objbl = new accomodation_requisitionbl();
                AccomodationList = (List<accomodation_requisitionbo>)Session["AccomodationRequisitionInternational"];
                int iResultAccommodationInternationalDelete = objbl.Delete_accomodation_requisitionbl(FTPT_REQUEST_ID, REQ_SEGMENT_ID,TravelType);
                foreach (accomodation_requisitionbo objBO in AccomodationList)
                {
                    objBO.CREATEDON = System.DateTime.Now;
                    objBO.REQUEST_ID = FTPT_REQUEST_ID;
                    objBO.REQ_SEGMENT_ID_FROM_TRAVEL_REQUEST = (int)objBO.REQ_SEGMENT_ID;
                    int iResultAccommodationInternational = objbl.Create_accomodation_requisitionbl(objBO);
                    if (iResultAccommodationInternational == 0)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = "Requisition updated successfully.";
                    }
                    else
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Unknown error occured.";
                    }
                }
            }
        }

        void UpdateVehicleRequisition(int FTPT_REQUEST_ID, int REQ_SEGMENT_ID, int TravelType)
        {
            //updating vehicle details ---- domestic------------------
            if (Session["VehicleRequisitionDomestic"] != null)
            {
                List<vehicle_requisitionbo> VehicleList = new List<vehicle_requisitionbo>();
                vehicle_requisitionbl objbl = new vehicle_requisitionbl();
                VehicleList = (List<vehicle_requisitionbo>)Session["VehicleRequisitionDomestic"];
                int iResultVehiclenDomesticDelete = objbl.Delete_vehicle_requisitionbl(FTPT_REQUEST_ID, REQ_SEGMENT_ID, TravelType);
                foreach (vehicle_requisitionbo objBO in VehicleList)
                {
                    objBO.CREATEDON = System.DateTime.Now;
                    objBO.REQUEST_ID = FTPT_REQUEST_ID;
                    objBO.REQ_SEGMENT_ID_FROM_TRAVEL_REQUEST = (int)objBO.REQ_SEGMENT_ID;
                    int iResultVehicleRequisitionDomestic = objbl.Create_vehicle_requisitionbl(objBO);
                    if (iResultVehicleRequisitionDomestic == 0)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = "Requisition updated successfully.";
                    }
                    else
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Unknown error occured.";
                    }
                }
            }


            //Update Vehicle details into DB for International
            if (Session["VehicleRequisitionInternational"] != null)
            {
                List<vehicle_requisitionbo> VehicleList = new List<vehicle_requisitionbo>();
                vehicle_requisitionbl objbl = new vehicle_requisitionbl();
                VehicleList = (List<vehicle_requisitionbo>)Session["VehicleRequisitionInternational"];
                int iResultVehiclenInternationalDelete = objbl.Delete_vehicle_requisitionbl(FTPT_REQUEST_ID, REQ_SEGMENT_ID, TravelType);
                foreach (vehicle_requisitionbo objBO in VehicleList)
                {
                    objBO.CREATEDON = System.DateTime.Now;
                    objBO.REQUEST_ID = FTPT_REQUEST_ID;
                    objBO.REQ_SEGMENT_ID_FROM_TRAVEL_REQUEST = (int)objBO.REQ_SEGMENT_ID;
                    int iResultVehicleRequisitionInternational = objbl.Create_vehicle_requisitionbl(objBO);
                    if (iResultVehicleRequisitionInternational == 0)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = "Requisition updated successfully.";
                    }
                    else
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Unknown error occured.";
                    }
                }
            }
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            TravelType_Id = TravelTypeDropDownList.SelectedItem.Value.ToString();
            if (DateTextBox.Text != "")
            {
                DateTime parsedDateTime = DateTime.ParseExact(DateTextBox.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                TravelDate = parsedDateTime.ToString("MM/dd/yyyy");
            }
            else
            {
                TravelDate = "";
            }
            EmployeeName = EmployeeNameTextBox.Text;
            EmployeeId = EmployeeIdTextBox.Text;
            LoadTravelRequestGridView(TravelType_Id, TravelDate, EmployeeId, EmployeeName);
        }

        protected void DomesticTravelRequiLinkButton_Click(object sender, CommandEventArgs e)
        {
            DomesticGridView.Visible = true;
           
            LinkButton lb = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lb.NamingContainer;
            //string t = e.CommandArgument.ToString();
            string RequisitionID = DomesticRequiSegmentGridView.DataKeys[row.RowIndex].Value.ToString();
            Session["DomesticRequisitionEditID"] = RequisitionID;
            BindControlsForEdit(Convert.ToInt32(RequisitionID), (int)TravelType.Domestic);
        }

        protected void BindControlsForEdit(int RequisitionSegmentId, int TypeOfTravel)
        {
            if (TypeOfTravel == Convert.ToInt32(TravelType.International))
            {
                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<requisitionbo> requisitionboCollection = new List<requisitionbo>();
                requisitionboCollection = (List<requisitionbo>)Session["InternationalSegmentList"];
                requisitionbo RequisationBo = requisitionboCollection.Find(delegate(requisitionbo obj)
                {
                    return obj.REQ_SEGMENT_ID == RequisitionSegmentId;
                });
                List<requisitionbo> RequisitionList = new List<requisitionbo>();
                RequisitionList.Add(RequisationBo);
                ClearInternationalControl();
                if (RequisationBo == null)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Unknown error occured.";
                }
                else
                {
                    CreateInternationalGridViewRows(RequisitionList, false);
                    InitialiseAddRemoveInternationalSegments(RequisitionList, -1);
                }
            }

            else if (TypeOfTravel == Convert.ToInt16(TravelType.Domestic))
            {
                List<requisitionbo> requisitionboList = new List<requisitionbo>();
                requisitionboList = (List<requisitionbo>)Session["DomesticSegmentList"];

                requisitionbo RequisationBo = requisitionboList.Find(delegate(requisitionbo obj)
                {
                    return obj.REQ_SEGMENT_ID == RequisitionSegmentId;
                });
                List<requisitionbo> RequisitionList = new List<requisitionbo>();
                RequisitionList.Add(RequisationBo);
                //---------------
                DomesticGridView.Visible = true;
                InternationalGridView.Visible = true;
                ClearInternationalControl();
                if (RequisationBo == null)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = "Unknown error occured.";
                }
                CreateDomesticGridViewRows(RequisitionList, false);
                InitialiseAddRemoveDomesticSegments(RequisitionList, -1);
            }
        }

        protected void DomesticRequiSegmentGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int RequisitionID = 0;
                Label lblRequisitionSegmentID = (Label)e.Row.FindControl("RequisitionID");
                string Requisition = lblRequisitionSegmentID.Text;
                if (!string.IsNullOrEmpty(Requisition))
                {
                    RequisitionID = Convert.ToInt32(Requisition);
                }
                GridView AccomodationRequisitionDomesticGridView = (GridView)e.Row.FindControl("AccomodationRequisitionGridViewDomestic");
                GridView VehicleRequisitionDomesticGridView = (GridView)e.Row.FindControl("VehicleRequisitionGridViewDomestic");
                Label AccomodationDetailLabel = (Label)e.Row.FindControl("AccomodationDetailLabel");
                Label VehicleDetailsLabel = (Label)e.Row.FindControl("VehicleDetailsLabel");
                if (Session["AccomodationRequisitionDomestic"] != null)
                {
                    tcDefalutRequisition.ActiveTabIndex = 0;
                    List<accomodation_requisitionbo> AccomodationDomesticList = new List<accomodation_requisitionbo>();
                    AccomodationDomesticList = (List<accomodation_requisitionbo>)Session["AccomodationRequisitionDomestic"];
                    AccomodationDomesticList = AccomodationDomesticList.Where(item => item.REQ_SEGMENT_ID == RequisitionID).ToList();
                    AccomodationRequisitionDomesticGridView.DataSource = AccomodationDomesticList;
                    AccomodationRequisitionDomesticGridView.DataBind();
                    if (AccomodationDomesticList.Count == 0)
                    {
                        AccomodationDetailLabel.Visible = false;
                    }
                    else
                    {
                        AccomodationDetailLabel.Visible = true;
                    }
                }
                else
                {
                    AccomodationDetailLabel.Visible = false;
                    AccomodationRequisitionDomesticGridView.DataSource = null;
                    AccomodationRequisitionDomesticGridView.DataBind();
                }
                //LoadVehicleRequisitionGridViewDomestic
                if (Session["VehicleRequisitionDomestic"] != null)
                {
                    tcDefalutRequisition.ActiveTabIndex = 0;
                    List<vehicle_requisitionbo> VehicleDomesticList = new List<vehicle_requisitionbo>();
                    VehicleDomesticList = (List<vehicle_requisitionbo>)Session["VehicleRequisitionDomestic"];
                    VehicleDomesticList = VehicleDomesticList.Where(item => item.REQ_SEGMENT_ID == RequisitionID).ToList();
                    VehicleRequisitionDomesticGridView.DataSource = VehicleDomesticList;
                    VehicleRequisitionDomesticGridView.DataBind();
                    if (VehicleDomesticList.Count == 0)
                    {
                        VehicleDetailsLabel.Visible = false;
                    }
                    else
                    {
                        VehicleDetailsLabel.Visible = true;
                    }
                }
                else
                {
                    VehicleDetailsLabel.Visible = false;
                    VehicleRequisitionDomesticGridView.DataSource = null;
                    VehicleRequisitionDomesticGridView.DataBind();
                }
            }
        }

        protected void InternationalRequisitionSegmentGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int RequisitionID = 0;
                Label lblRequisitionSegmentID = (Label)e.Row.FindControl("RequisitionID");
                string Requisition = lblRequisitionSegmentID.Text;
                if (!string.IsNullOrEmpty(Requisition))
                {
                    RequisitionID = Convert.ToInt32(Requisition);
                }
                GridView AccomodationRequisitionGridViewInternational = (GridView)e.Row.FindControl("AccomodationRequisitionGridViewInternational");
                GridView VehicleRequisitionGridViewInternational = (GridView)e.Row.FindControl("VehicleRequisitionGridViewInternational");
                Label InternationalAccomodationDetialsLabel = (Label)e.Row.FindControl("InternationalAccomodationDetialsLabel");
                Label InternationalVehicleDetialsLabel = (Label)e.Row.FindControl("InternationalVehicleDetialsLabel");
                //Load Accommodation International
                if (Session["AccomodationRequisitionInternational"] != null)
                {
                    tcDefalutRequisition.ActiveTabIndex = 1;
                    List<accomodation_requisitionbo> AccommodationInternationalList = new List<accomodation_requisitionbo>();
                    AccommodationInternationalList = (List<accomodation_requisitionbo>)Session["AccomodationRequisitionInternational"];
                    AccommodationInternationalList = AccommodationInternationalList.Where(item => item.REQ_SEGMENT_ID == RequisitionID).ToList();
                    AccomodationRequisitionGridViewInternational.DataSource = AccommodationInternationalList;
                    AccomodationRequisitionGridViewInternational.DataBind();
                    if (AccommodationInternationalList.Count == 0)
                    {
                        InternationalAccomodationDetialsLabel.Visible = false;
                    }
                    else
                    {
                        InternationalAccomodationDetialsLabel.Visible = true;
                    }
                }
                else
                {
                    InternationalAccomodationDetialsLabel.Visible = false;
                    AccomodationRequisitionGridViewInternational.DataSource = null;
                    AccomodationRequisitionGridViewInternational.DataBind();
                }
                //Load Vehicle International
                if (Session["VehicleRequisitionInternational"] != null)
                {
                    tcDefalutRequisition.ActiveTabIndex = 1;
                    List<vehicle_requisitionbo> VehicleInternationalList = new List<vehicle_requisitionbo>();
                    VehicleInternationalList = (List<vehicle_requisitionbo>)Session["VehicleRequisitionInternational"];
                    VehicleInternationalList = VehicleInternationalList.Where(item => item.REQ_SEGMENT_ID == RequisitionID).ToList();
                    VehicleRequisitionGridViewInternational.DataSource = VehicleInternationalList;
                    VehicleRequisitionGridViewInternational.DataBind();
                    if (VehicleInternationalList.Count == 0)
                    {
                        InternationalVehicleDetialsLabel.Visible = false;
                    }
                    else
                    {
                        InternationalVehicleDetialsLabel.Visible = true;
                    }
                }
                else
                {
                    InternationalVehicleDetialsLabel.Visible = false;
                    VehicleRequisitionGridViewInternational.DataSource = null;
                    VehicleRequisitionGridViewInternational.DataBind();
                }
            }
        }

        public void LoadDomesticSegmentRelatedGrids(List<requisitionbo> objList)
        {
            //Bind domestic gridview
            DomesticRequiSegmentGridView.DataSource = objList;
            DomesticRequiSegmentGridView.DataBind();
            if (objList != null)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    Label AccomodationDetailLabel = (Label)DomesticRequiSegmentGridView.Rows[i].FindControl("AccomodationDetailLabel");
                    Label VehicleDetailsLabel = (Label)DomesticRequiSegmentGridView.Rows[i].FindControl("VehicleDetailsLabel");
                    //Bind domestic accommodation gridview
                    List<accomodation_requisitionbo> accomodation_requisitionboList = new List<accomodation_requisitionbo>();
                    accomodation_requisitionbl accomodation_requisitionblObj = new accomodation_requisitionbl();
                    accomodation_requisitionboList = accomodation_requisitionblObj.Get_Accommodation_Details(objList[i].FTPT_REQUEST_ID, objList[i].REQ_SEGMENT_ID);
                    GridView AccommodationGrid = (GridView)DomesticRequiSegmentGridView.Rows[i].FindControl("AccomodationRequisitionGridViewDomestic");
                    if (accomodation_requisitionboList.Count != 0)
                    {
                        AccommodationGrid.DataSource = accomodation_requisitionboList;
                        AccommodationGrid.DataBind();
                        AccommodationGrid.Visible = true;
                        AccomodationDetailLabel.Visible = true;
                        Session.Add("AccomodationRequisitionDomestic", accomodation_requisitionboList);
                    }
                    else
                    {
                        AccommodationGrid.DataSource = null;
                        AccommodationGrid.DataBind();
                        AccomodationDetailLabel.Visible = false;
                    }
                    //Bind domestic vehicle gridview
                    List<vehicle_requisitionbo> vehicle_requisitionboboList = new List<vehicle_requisitionbo>();
                    vehicle_requisitionbl vehicle_requisitionblObj = new vehicle_requisitionbl();
                    vehicle_requisitionboboList = vehicle_requisitionblObj.Get_Vehicle_Details(objList[i].FTPT_REQUEST_ID, objList[i].REQ_SEGMENT_ID);
                    GridView VehicleGrid = (GridView)DomesticRequiSegmentGridView.Rows[i].FindControl("VehicleRequisitionGridViewDomestic");
                    if (vehicle_requisitionboboList.Count != 0)
                    {
                        VehicleGrid.DataSource = vehicle_requisitionboboList;
                        VehicleGrid.DataBind();
                        VehicleDetailsLabel.Visible = true;
                        Session.Add("VehicleRequisitionDomestic", vehicle_requisitionboboList);
                    }
                    else
                    {
                        VehicleGrid.DataSource = null;
                        VehicleGrid.DataBind();
                        VehicleDetailsLabel.Visible = false;
                    }

                    Session["DomesticSegmentList"] = objList;
                }
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            td_requisitionbl td_requisitionBl = new td_requisitionbl();
            requisitionbo requisitionboObject = new requisitionbo();
            requisitionboObject.REQ_SEGMENT_ID = Convert.ToInt32(Session["FTPT_REQ_SEGMENT_ID"].ToString());
            requisitionboObject.FTPT_REQUEST_ID = Convert.ToInt32(Session["FTPT_REQUISITION_ID"].ToString());
            requisitionboObject.EMPLOYEE_NO = User.Identity.Name.Trim();
            requisitionboObject.REMARKS = TD_RemarksTextBox.Text;
            requisitionboObject.REASON_FOR_CANCEL = ReasonForCancelTextBox.Text;
            requisitionboObject.CURRENT_STATUS = Convert.ToString((int)Enum.Parse(typeof(ReuisitionStatus), ReuisitionStatus.RequisitionCancelledByTD.ToString()));
            int iResult =td_requisitionBl.UpdateRequistionStatus(requisitionboObject);
            if (iResult == 0)
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Proposal cancelled successfully.";
                TD_RemarksTextBox.Text = "";
                ClearControls();
                LoadTravelRequestGridView(string.Empty, string.Empty, string.Empty, string.Empty);
            }
            else
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Unknown error occured.";
            }
        }

        public void LoadInternationalSegmentRelatedGrids(List<requisitionbo> objList)
        {
            //Bind international gridview
            InternationalRequisitionSegmentGridView.DataSource = objList;
            InternationalRequisitionSegmentGridView.DataBind();
            if (objList != null)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    Label AccomodationDetailLabel = (Label)InternationalRequisitionSegmentGridView.Rows[i].FindControl("InternationalAccomodationDetialsLabel");
                    Label VehicleDetailsLabel = (Label)InternationalRequisitionSegmentGridView.Rows[i].FindControl("InternationalVehicleDetialsLabel");
                    //Bind domestic accommodation gridview
                    List<accomodation_requisitionbo> accomodation_requisitionboList = new List<accomodation_requisitionbo>();
                    accomodation_requisitionbl accomodation_requisitionblObj = new accomodation_requisitionbl();
                    accomodation_requisitionboList = accomodation_requisitionblObj.Get_Accommodation_Details(objList[i].FTPT_REQUEST_ID, objList[i].REQ_SEGMENT_ID);
                    GridView AccommodationGrid = (GridView)InternationalRequisitionSegmentGridView.Rows[i].FindControl("AccomodationRequisitionGridViewInternational");
                    if (accomodation_requisitionboList.Count != 0)
                    {
                        AccommodationGrid.DataSource = accomodation_requisitionboList;
                        AccommodationGrid.DataBind();
                        AccomodationDetailLabel.Visible = true;
                        Session.Add("AccomodationRequisitionInternational", accomodation_requisitionboList);
                    }
                    else
                    {
                        AccommodationGrid.DataSource = null;
                        AccommodationGrid.DataBind();
                        AccomodationDetailLabel.Visible = false;
                    }
                    //Bind domestic vehicle gridview
                    List<vehicle_requisitionbo> vehicle_requisitionboboList = new List<vehicle_requisitionbo>();
                    vehicle_requisitionbl vehicle_requisitionblObj = new vehicle_requisitionbl();
                    vehicle_requisitionboboList = vehicle_requisitionblObj.Get_Vehicle_Details(objList[i].FTPT_REQUEST_ID, objList[i].REQ_SEGMENT_ID);
                    GridView VehicleGrid = (GridView)InternationalRequisitionSegmentGridView.Rows[i].FindControl("VehicleRequisitionGridViewInternational");
                    if (vehicle_requisitionboboList.Count != 0)
                    {
                        VehicleGrid.DataSource = vehicle_requisitionboboList;
                        VehicleGrid.DataBind();
                        VehicleDetailsLabel.Visible = true;
                        Session.Add("VehicleRequisitionInternational", vehicle_requisitionboboList);
                    }
                    else
                    {
                        VehicleGrid.DataSource = null;
                        VehicleGrid.DataBind();
                        VehicleDetailsLabel.Visible = false;
                    }
                    Session["InternationalSegmentList"] = objList;
                }
            }
        }

        protected void InternationalTravelRequiLinkButton_Click(object sender, CommandEventArgs e)
        {
            InternationalGridView.Visible = true;
            LinkButton lb = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lb.NamingContainer;
            //string t = e.CommandArgument.ToString();
            string RequisitionID = InternationalRequisitionSegmentGridView.DataKeys[row.RowIndex].Value.ToString();
            Session["InternationalRequisitionEditID"] = RequisitionID;
            BindControlsForEdit(Convert.ToInt32(RequisitionID), (int)TravelType.International);
        }
        #endregion
    ////functions to add segments from travel desk...
    ////=============================================
    //    protected void DomesticButtonAdd_Click(object sender, EventArgs e)
    //    {
    //            try
    //            {                   
    //                if (ValidateControl())
    //                {
    //                    // validation to check date is btw last 7days.
    //                    for (int i = 0; i < DomesticGridView.Rows.Count; i++)
    //                    {
    //                        TextBox domesticTextDateTravel = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextDateTravel");
    //                          //lblMessageBoard.Text =lblMessageBoard.Text +" "+ domesticTextDateTravel.Text;
    //                        string sdt = domesticTextDateTravel.Text;
    //                        DateTime dtDate = DateTime.ParseExact(sdt, "dd-MM-yyyy", CultureInfo.InvariantCulture); 
    //                        //lblMessageBoard.Text = "E " + lblMessageBoard.Text + " " + domesticTextDateTravel.Text;
    //                        if (dtDate <= DateTime.Now.AddDays(-7))
    //                        {
    //                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
    //                            lblMessageBoard.Text = "Date of Travel must not be less than 7 days from present date";
    //                            SetFocus(domesticTextDateTravel);
    //                            return;
    //                        }
    //                    }                      

    //                    lblMessageBoard.Text = "";
    //                    EditDomesticRequisitionGrid();
    //                    LoadDomesticGridViewFirstRow();
    //                }   
    //            }
    //            catch (Exception ex)
    //            {
    //                lblMessageBoard.Text = lblMessageBoard.Text+" " + "Error. Please contact administrator, " + ex.Message;
    //            }        
    //    }

    //    public void EditDomesticRequisitionGrid()
    //    {
    //        requisitionbo objCollection = GetDomesticGridViewAllRowsControlValues();
    //        List<requisitionbo> objCollection1 = new List<requisitionbo>();
    //        if (Session["DomesticRequisitionEditID"] != null)
    //        {
    //            if (Session["DomesticSegmentList"] != null)
    //            {
    //                objCollection1 = (List<requisitionbo>)Session["DomesticSegmentList"];

    //                for (int i = 0; i < objCollection1.Count; i++)
    //                {
    //                    if (objCollection1[i].REQ_SEGMENT_ID == Convert.ToInt32(Session["DomesticRequisitionEditID"]))
    //                    {
    //                        objCollection1[i].TRAVEL_DATE = objCollection.TRAVEL_DATE;
    //                        objCollection1[i].TRAVEL_DATE_ALL = objCollection.TRAVEL_DATE_ALL;
    //                        objCollection1[i].TRAVEL_TIME = objCollection.TRAVEL_TIME;
    //                        objCollection1[i].FLYNUM = objCollection.FLYNUM;
    //                        objCollection1[i].REMARKS = objCollection.REMARKS;
    //                        objCollection1[i].ADVANCE = objCollection.ADVANCE;
    //                        objCollection1[i].MODE_OF_TRANSPOPRT_KZPMF = objCollection.MODE_OF_TRANSPOPRT_KZPMF;
    //                        objCollection1[i].MEDIA_OF_CATEGORY_PKWKL = objCollection.MEDIA_OF_CATEGORY_PKWKL;
    //                        objCollection1[i].VEHICLE_NAME_VHNUM = objCollection.VEHICLE_NAME_VHNUM;
    //                        objCollection1[i].REGION_RGION_FROM = objCollection.REGION_RGION_FROM;
    //                        objCollection1[i].REGION_RGION_TO = objCollection.REGION_RGION_TO;
    //                        objCollection1[i].MEDIA_OF_CATEGORY_TEXT25 = objCollection.MEDIA_OF_CATEGORY_TEXT25;
    //                        objCollection1[i].REGION_TEXT25_FROM = objCollection.REGION_TEXT25_FROM;
    //                        objCollection1[i].REGION_TEXT25_TO = objCollection.REGION_TEXT25_TO;
    //                        objCollection1[i].VEHICLE_NAME_ZZVEHNAM = objCollection.VEHICLE_NAME_ZZVEHNAM;
    //                        Session["DomesticRequisitionEditID"] = null;
    //                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
    //                        lblMessageBoard.Text = "Requisition details updated successfully.";
    //                    }
    //                }
    //            }
    //            else if (Session["DomesticSegmentList"] == null)
    //            {
    //                objCollection = GetDomesticGridViewAllRowsControlValues();
    //            }
    //        }
    //        else
    //        {
    //            if (Session["DomesticSegmentList"] != null)
    //            {
    //                objCollection1 = (List<requisitionbo>)Session["DomesticSegmentList"];
    //                objCollection1.Add(objCollection);
    //            }
    //            else
    //            {
    //                objCollection1.Add(objCollection);
    //            }
    //        }
    //        Session.Add("DomesticSegmentList", objCollection1);
    //        DomesticRequiSegmentGridView.DataSource = objCollection1;
    //        DomesticRequiSegmentGridView.DataBind();
    //        Session["SelectedRequisitionFromGrid"] = null;
    //    }

    //    protected requisitionbo GetDomesticGridViewAllRowsControlValues()
    //    {
    //        requisitionbo obj = new requisitionbo();
    //        for (int i = 0; i < DomesticGridView.Rows.Count; i++)
    //        {
    //            //requisitionbo obj = new requisitionbo();
    //            TextBox TextBoxDateTravel = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextDateTravel");
    //            TextBox TextBoxtimedeparture = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextTimeDeparture");
    //            TextBox TextBoxFFlyNo = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextFFlyNo");
    //            TextBox TextBoxRemarks = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextRemarks");
    //            TextBox TextBoxHODRemarks = (TextBox)DomesticGridView.Rows[i].FindControl("txtHODRemarks");
    //            TextBox TextBoxTDRemarks = (TextBox)DomesticGridView.Rows[i].FindControl("txtTDRemarks");
    //            TextBox TextBoxAdvance = (TextBox)DomesticGridView.Rows[i].FindControl("domesticTextAdvance");
    //            DropDownList ddlMTPort = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownMTPort");
    //            DropDownList ddlMCate = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownMCate");
    //            DropDownList ddlVehName = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownVehName");
    //            DropDownList ddlFrom = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownFrom");
    //            DropDownList ddlTo = (DropDownList)DomesticGridView.Rows[i].FindControl("domesticDropDownTo");
    //            Button AddButton = (Button)DomesticGridView.Rows[i].FindControl("DomesticButtonAdd");

    //            if (TextBoxDateTravel.Text != "")
    //            {
    //                obj.TRAVEL_DATE = DateTime.ParseExact(TextBoxDateTravel.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
    //                obj.TRAVEL_DATE_ALL = Convert.ToString(TextBoxDateTravel.Text);
    //            }
    //            obj.TRAVEL_TIME = TextBoxtimedeparture.Text;
    //            obj.FLYNUM = TextBoxFFlyNo.Text;
    //            obj.REMARKS = TextBoxRemarks.Text;
    //            obj.ADVANCE = TextBoxAdvance.Text;
    //            if (ddlMTPort.SelectedValue.ToString() != "0")
    //            {
    //                obj.MODE_OF_TRANSPOPRT_KZPMF = ddlMTPort.SelectedValue.ToString();
    //            }
    //            else
    //            {
    //                obj.MODE_OF_TRANSPOPRT_KZPMF = "0";
    //            }
    //            if (ddlMCate.SelectedValue.ToString() != "0")
    //            {
    //                obj.MEDIA_OF_CATEGORY_PKWKL = ddlMCate.SelectedValue.ToString();
    //            }
    //            else
    //            {
    //                obj.MEDIA_OF_CATEGORY_PKWKL = "0";
    //            }

    //            if (ddlVehName.SelectedValue.ToString() != "0")
    //            {
    //                obj.VEHICLE_NAME_VHNUM = ddlVehName.SelectedValue.ToString();
    //            }
    //            else
    //            {
    //                obj.VEHICLE_NAME_VHNUM = "0";
    //            }

    //            if (ddlFrom.SelectedValue.ToString() != "0")
    //            {
    //                obj.REGION_RGION_FROM = ddlFrom.SelectedValue.ToString();
    //            }
    //            else
    //            {
    //                obj.REGION_RGION_FROM = "0";
    //            }

    //            if (ddlTo.SelectedValue.ToString() != "0")
    //            {
    //                obj.REGION_RGION_TO = ddlTo.SelectedValue.ToString();
    //            }
    //            else
    //            {
    //                obj.REGION_RGION_TO = "0";
    //            }

    //            if (!string.IsNullOrEmpty(ddlVehName.SelectedValue))
    //            {
    //                if (ddlVehName.SelectedValue.ToString() != "0")
    //                {
    //                    obj.VEHICLE_NAME_ZZVEHNAM = ddlVehName.SelectedItem.Text;
    //                }
    //                else
    //                {
    //                    obj.VEHICLE_NAME_ZZVEHNAM = "";
    //                }
    //            }

    //            if (!string.IsNullOrEmpty(ddlMCate.SelectedValue))
    //            {
    //                if (ddlMCate.SelectedValue.ToString() != "0")
    //                {
    //                    obj.MEDIA_OF_CATEGORY_TEXT25 = ddlMCate.SelectedItem.Text;
    //                }
    //                else
    //                {
    //                    obj.MEDIA_OF_CATEGORY_TEXT25 = "";
    //                }
    //            }


    //            if (ddlFrom.SelectedItem != null)
    //            {
    //                if (ddlFrom.SelectedValue.ToString() != "0")
    //                {
    //                    obj.REGION_TEXT25_FROM = ddlFrom.SelectedItem.Text;
    //                }
    //                else
    //                {
    //                    obj.REGION_TEXT25_FROM = "";
    //                }
    //            }
    //            else
    //            {
    //                obj.REGION_TEXT25_FROM = "";
    //            }

    //            if (ddlTo.SelectedItem != null)
    //            {
    //                if (ddlTo.SelectedValue.ToString() != "0")
    //                {
    //                    obj.REGION_TEXT25_TO = ddlTo.SelectedItem.Text;
    //                }
    //                else
    //                {
    //                    obj.REGION_TEXT25_TO = "";
    //                }
    //            }
    //            else
    //            {
    //                obj.REGION_TEXT25_TO = "";
    //            }

    //            if (ddlVehName.SelectedItem != null)
    //            {
    //                if (ddlVehName.SelectedValue.ToString() != "0")
    //                {
    //                    obj.VEHICLE_NAME_ZZVEHNAM = ddlVehName.SelectedItem.Text;
    //                }
    //                else
    //                {
    //                    obj.VEHICLE_NAME_ZZVEHNAM = "";
    //                }
    //            }
    //            else
    //            {
    //                obj.VEHICLE_NAME_ZZVEHNAM = "";
    //            }

    //            if (AddButton.Text != "Update")
    //            {
    //                if (Session["DomesticSegmentCount"] != null)
    //                {
    //                    obj.REQ_SEGMENT_ID = Convert.ToInt32(Session["DomesticSegmentCount"]) + 1;
    //                    Session["DomesticSegmentCount"] = obj.REQ_SEGMENT_ID;
    //                }
    //                else
    //                {
    //                    Session["DomesticSegmentCount"] = 1;
    //                    obj.REQ_SEGMENT_ID = 1;
    //                }
    //            }
    //        }
    //        return obj;
    //    }
    ////=============================================
  
    }

}
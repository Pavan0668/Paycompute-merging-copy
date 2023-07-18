using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using iEmpPower.Old_App_Code.iEmpPowerBL.Common;
using iEmpPower.Old_App_Code.iEmpPowerBO.travellersfeedback;
using iEmpPower.Old_App_Code.iEmpPowerBL.travellersfeedbackbl;
using System.Web.Security;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class EmpClaim : System.Web.UI.Page
    {
        protected MembershipUser memUser;
        public bool TravelRequestSortedOrder_Bool = false;
        public int TrvelRequestGridSelectedIndexChange = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grids.Visible = false;

                var xmlFilePath1 = Server.MapPath("~/UI/Benefits_Payment/XMLFile1.xml");//Location of the XML file.
                DataSet dsCameraDetails1 = new DataSet();
                dsCameraDetails1.ReadXml(xmlFilePath1);// Load XML in dataset
                grdCorporateClaims.DataSource = dsCameraDetails1.Tables[0].DefaultView;
                grdCorporateClaims.DataBind();// Bind the DataSet to Grid that will display all xml data.               
                LoadTravelRequestGridView();
                Session.Add("TravelRequestSortedOrder_Bool", TravelRequestSortedOrder_Bool);

                for (int i = 0; i < grdCorporateClaims.Rows.Count; i++)
                {
                    using (DropDownList ddlMTPort = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwnMode"))
                    using (DropDownList ddlfrom = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwnFrom"))
                    using (DropDownList ddlto = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwnTo"))
                    using (DropDownList ddlEXPTYPE = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwnEXPTYPE"))
                    using (DropDownList ddlcurrency = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwncurrency"))
                    {
                        Loadmtport(ddlMTPort);
                        loadfrom(ddlfrom);
                        loadto(ddlto);
                        //loadExptypeAll(ddlEXPTYPE);
                        //loadcurrency(ddlcurrency);
                    }
                }
            }
        }

        protected void LoadTravelRequestGridView()
        {
            requisitionbo requisitionboObj = new requisitionbo();
            travelrequestbl travelrequestblObj = new travelrequestbl();
            List<requisitionbo> requisitionboList = new List<requisitionbo>();
            if (Session["sEmploreeId"] != null)
            {
                requisitionboList = travelrequestblObj.Get_emp_details_booked(Session["sEmploreeId"].ToString());
                Session.Add("TravelRequestRequisitionDetails", requisitionboList);
                if (requisitionboList == null || requisitionboList.Count == 0)
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
                TravelRequestGridView.DataBind();
            }
            else
            {
                Session.Abandon();
                Response.Redirect("~/sessionout.aspx", false);
            }
        }

        protected void loadfrom(DropDownList ddl)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetRegionName();
            ddl.DataTextField = "REGION_TEXT25_FROM";
            ddl.DataValueField = "REGION_RGION_FROM";
            ddl.DataBind();
            ListItem drpDefaultItem3 = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItem3);
            ddl.SelectedValue = "0";
        }

        protected void loadto(DropDownList ddl)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetRegionName();
            ddl.DataTextField = "REGION_TEXT25_FROM";
            ddl.DataValueField = "REGION_RGION_FROM";
            ddl.DataBind();
            ListItem drpDefaultItem4 = new ListItem("Select ", "0", true);
            ddl.Items.Add(drpDefaultItem4);
            ddl.SelectedValue = "0";

        }
        protected void Loadmtport(DropDownList ddl)
        {

            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetModeofTransport();
            ddl.DataTextField = "MODE_OF_TRANSPOPRT_FZTXT";
            ddl.DataValueField = "MODE_OF_TRANSPOPRT_KZPMF";
            ddl.DataBind();
            ListItem drpDefaultItem1 = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItem1);
            ddl.SelectedValue = "0";
        }

        protected void loadExptypeAll(DropDownList ddl, string TravelType)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetExpenseTypeAll(int.Parse(TravelType).ToString("00"));
            ddl.DataTextField = "SPTXT";
            ddl.DataValueField = "SPKZL";
            ddl.DataBind();
            ListItem drpDefaultItem3 = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItem3);
            ddl.SelectedValue = "0";
            //ListItem MisList = ddl.Items.FindByValue("MISC");
            //ddl.Items.Remove(MisList);
        }

        protected void loadExptype(DropDownList ddl, string traveltype, string PERNR)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetExpenseType(traveltype, PERNR);
            ddl.DataTextField = "SPTXT";
            ddl.DataValueField = "SPKZL";
            ddl.DataBind();
            ListItem drpDefaultItem3 = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItem3);
            ddl.SelectedValue = "0";
        }

        protected void loadcurrency(DropDownList ddl, string TravelTYpe)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            string[] LevTypeFilter = { "INR" };
            string[] LevTypeFilterUSD = { "USD" };

            switch (TravelTYpe)
            {
                case "1":

                    var CurType = from i in objtravelrequestbl.GetCurrency()
                                  where LevTypeFilter.Contains(i.WAERS)
                                  select i;
                    ddl.DataSource = CurType;
                    ddl.DataTextField = "LTEXT";
                    ddl.DataValueField = "WAERS";
                    ddl.DataBind();
                    ListItem drpDefaultItem3 = new ListItem("Select", "0", true);
                    ddl.Items.Add(drpDefaultItem3);
                    ddl.SelectedValue = "0";
                    break;
                case "2":
                    var CurType1 = from i in objtravelrequestbl.GetCurrency()
                                   where LevTypeFilterUSD.Contains(i.WAERS)
                                   select i;
                    ddl.DataSource = CurType1;
                    ddl.DataTextField = "LTEXT";
                    ddl.DataValueField = "WAERS";
                    ddl.DataBind();
                    ListItem drpDefaultItem1 = new ListItem("Select", "0", true);
                    ddl.Items.Add(drpDefaultItem1);
                    ddl.SelectedValue = "0";
                    break;
                default:
                    break;
            }

        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            JourneyDetailsGridView.PageIndex = e.NewPageIndex;
        }
        protected void OnPageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            grdCorporateClaims.PageIndex = e.NewPageIndex;
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

        protected void TravelRequestGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            TravelRequestGridView.PageIndex = e.NewPageIndex;
            List<requisitionbo> requisitionboList = (List<requisitionbo>)Session["TravelRequestRequisitionDetails"];
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

        protected void TravelRequestGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            grids.Visible = true;
            //lblMessageBoard.Text = "";
            try
            {
                Session["SelectedRequisitionFromGrid"] = "true";
                BindControlsForUpdate();
                LabelTravellerName.Text = TravelRequestGridView.DataKeys[TravelRequestGridView.SelectedIndex]["EMPLOYEE_NAME"].ToString();//Session["EmployeeName"].ToString();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }

        protected void BindControlsForUpdate()
        {
            try
            {
                using (GridViewRow grdRow = TravelRequestGridView.SelectedRow)
                {
                    int FTPT_REQUISITION_ID = Convert.ToInt32(grdRow.Cells[0].Text.ToString().Trim());
                    Session.Add("Travel_Request_ID", FTPT_REQUISITION_ID);
                    int FTPT_REQ_SEGMENT_ID = Convert.ToInt32(grdRow.Cells[1].Text.ToString().Trim());
                    Session.Add("Travel_Request_SEGMENT_ID", FTPT_REQ_SEGMENT_ID);

                    List<requisitionbo> requisitionboList = new List<requisitionbo>();
                    requisitionboList = (List<requisitionbo>)Session["TravelRequestRequisitionDetails"];
                    requisitionbo RequisationBo = requisitionboList.Find(delegate(requisitionbo obj)
                    {
                        return obj.FTPT_REQUEST_ID == FTPT_REQUISITION_ID && obj.REQ_SEGMENT_ID == FTPT_REQ_SEGMENT_ID;
                    });

                    List<requisitionbo> RequisitionList = new List<requisitionbo>();
                    RequisitionList.Add(RequisationBo);
                    JourneyDetailsGridView.DataSource = RequisitionList;
                    JourneyDetailsGridView.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('" + Ex.Message + "');", true); }
        }

        protected void TravelRequestGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<requisitionbo> objRequisitionBoList = new List<requisitionbo>();
            objRequisitionBoList = (List<requisitionbo>)Session["TravelRequestRequisitionDetails"];
            bool objSortOrder = (bool)Session["TravelRequestSortedOrder_Bool"];
            string SortExpression = e.SortExpression.ToString().Trim();
            // Call Printer grid sort function from Elan grid manager namespace.
            objRequisitionBoList = GridManager.SortTravelRequestGridView(SortExpression, objSortOrder, objRequisitionBoList);
            TravelRequestGridView.DataSource = objRequisitionBoList;
            TravelRequestGridView.DataBind();
            // Add negation of sort order to session for next time use.
            Session.Add("TravelRequestSortedOrder_Bool", !objSortOrder);
            // Add sorted list to the session
            Session.Add("TravelRequestRequisitionDetails", objRequisitionBoList);
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            travellersfeedback objBO = new travellersfeedback();
            travellersfeedbackbl objBL = new travellersfeedbackbl();
            memUser = Membership.GetUser();
            int FTPT_REQUISITION_ID = (int)Session["Travel_Request_ID"];
            int FTPT_REQ_SEGMENT_ID = (int)Session["Travel_Request_SEGMENT_ID"];
            objBO.REQUISITION_ID = FTPT_REQUISITION_ID;
            objBO.REQ_SEGMENT_ID = FTPT_REQ_SEGMENT_ID;
            objBO.pERNR = memUser.ToString();
            objBO.traveller_name = LabelTravellerName.Text;
            objBO.travel_duration = Convert.ToByte(txtCounter1.Text);
            objBO.travel_arrangement = Convert.ToByte(txtCounter2.Text);
            objBO.accommodation_arrangement = Convert.ToByte(txtCounter3.Text);
            objBO.Taxi_arrangement = Convert.ToByte(txtCounter4.Text);
            objBO.Visa_Passport_arrangement = Convert.ToByte(txtCounter5.Text);
            objBO.communication = Convert.ToByte(txtCounter6.Text);
            objBO.Overall_travel_experience = Convert.ToByte(string.IsNullOrEmpty(txtCounter7.Text) ? "0" : txtCounter7.Text);

            if (TxtFeedbackorComments.Text == "")
            {
                //lblMessageBoard.Text = "Please give feedback";
                ModelErrorMessage("Please give feedback", System.Drawing.Color.Red);
                return;
            }
            else
            {
                objBO.Feedback = TxtFeedbackorComments.Text;
            }

            objBL.Create_TravelFeedback(objBO);
            SendMail();
            ClearControls();

            //LoadEType();

            using (GridViewRow grdRow = TravelRequestGridView.SelectedRow)
            {
                string Traveltype = grdRow.Cells[7].Text.ToString();
                Session["TravelType"] = Traveltype;
                for (int i = 0; i < grdCorporateClaims.Rows.Count; i++)
                {
                    using (DropDownList ddlEXPTYPE = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwnEXPTYPE"))
                    using (DropDownList ddlcurrency = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwncurrency"))
                    {
                        loadExptypeAll(ddlEXPTYPE, Traveltype);
                        loadcurrency(ddlcurrency, Traveltype);
                    }
                }
            }
        }

        public void LoadEType()
        {
            using (GridViewRow grdRow = TravelRequestGridView.SelectedRow)
            {
                string Traveltype = grdRow.Cells[7].Text.ToString();
                for (int i = 0; i < grdCorporateClaims.Rows.Count; i++)
                {
                    using (DropDownList ddlEXPTYPE = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwnEXPTYPE"))
                        loadExptype(ddlEXPTYPE, Traveltype, Session["sEmploreeId"].ToString());
                }
            }
        }

        private void SendMail()
        {
            try
            {
                string strMailToList = string.Empty;
                string strSubject = "Feedback details";
                string strPernr_Mail = string.Empty;
                string strBodyMsg = PrepareMailBody();
                iEmpPowerMaster_Load.masterbl.DispatchMail(strMailToList, User.Identity.Name, strSubject, strPernr_Mail, strBodyMsg);
                //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                //lblMessageBoard.Text = "Requisition details send successfully.";
                ClearControls();
            }
            catch
            {
                //lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                //lblMessageBoard.Text = "Unknown error occured. Please contact your system administrator.";
                return;
            }
        }

        public void ClearControls()
        {
            txtCounter1.Text = "";
            txtCounter2.Text = "";
            txtCounter3.Text = "";
            txtCounter4.Text = "";
            txtCounter5.Text = "";
            txtCounter6.Text = "";
            txtCounter7.Text = "";
            TxtFeedbackorComments.Text = "";
            divFeedbackForm.Visible = false;
            divClaims.Visible = true;
        }

        protected string PrepareMailBody()
        {
            string body = "";
            body = "<table style='width: 700px' border='1' cellpadding='1' cellspacing='0'>";
            body += "<tr>";

            body += "<td ><strong>Employee Name </strong></td>";

            body += "<td>" + LabelTravellerName.Text + "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td colspan='2' align='center' style=' color: #006699;'><strong>RATINGS [EXCELLENT= 4  GOOD= 3  AVERAGE= 2  POOR= 1  HORRIBLE= 0 </strong></td>";

            body += "</tr>";
            body += "<tr>";
            body += "<td  ><strong>Travel Arrangement </strong></td>";

            body += "<td>" + txtCounter1.Text + "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td  ><strong>Accomodation arrangement</strong></td>";

            body += "<td>" + txtCounter2.Text + "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td  ><strong>Taxi arrangement</strong></td>";

            body += "<td>" + txtCounter3.Text + "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td  ><strong>Visa & Passport Arrangement</strong></td>";

            body += "<td>" + txtCounter4.Text + "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td  ><strong>Communication</strong></td>";

            body += "<td>" + txtCounter5.Text + " </td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td  ><strong>Overall Travel Experience</strong></td>";

            body += "<td>" + txtCounter6.Text + "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td  ><strong>Feedback</strong></td>";

            body += "<td>" + txtCounter7.Text + "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td  ><strong>F/Fly No</strong></td>";

            body += "<td>" + TxtFeedbackorComments.Text + "</td>";
            body += "</tr>";
            body += "<tr>";

            body += "</table>";


            return body;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                funSaveCorporateClaims();
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        public void funSaveCorporateClaims()
        {
            try
            {
                int iCnt = 0;

                if (grdCorporateClaims.Rows.Count >= 0)
                {
                    Corporate_Claimsbo objClaimsAddBo = new Corporate_Claimsbo();
                    Expensebl objEXPAddBl = new Expensebl();

                    for (int i = 0; i <= grdCorporateClaims.Rows.Count - 1; i++)
                    {
                        int FTPT_REQUISITION_ID = 0;
                        int FTPT_REQ_SEGMENT_ID = 0;
                        FTPT_REQUISITION_ID = (int)Session["Travel_Request_ID"];
                        FTPT_REQ_SEGMENT_ID = (int)Session["Travel_Request_SEGMENT_ID"];

                        //Session.Add("Travel_Request_ID", FTPT_REQUISITION_ID);
                        //Session.Add("Travel_Request_SEGMENT_ID", FTPT_REQ_SEGMENT_ID);
                        if (objClaimsAddBo != null)
                        {


                            objClaimsAddBo.REQUISITION_ID = FTPT_REQUISITION_ID;
                            objClaimsAddBo.REQ_SEGMENT_ID = FTPT_REQ_SEGMENT_ID;

                            objClaimsAddBo.PERNR = User.Identity.Name;
                            int iTravelRequestID = (int)Session["Travel_Request_ID"];
                            objClaimsAddBo.TRIP_NUMBER = Convert.ToString(iTravelRequestID);

                            using (TextBox TextDateTravel = (TextBox)grdCorporateClaims.Rows[i].FindControl("TextDateTravel"))
                            using (TextBox TextDatetOTravel = (TextBox)grdCorporateClaims.Rows[i].FindControl("TextDateTravelTo"))
                            using (DropDownList drpdwnPlaceFrom = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwnFrom"))
                            using (DropDownList drpdwnPlaceTo = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwnTo"))
                            using (DropDownList drpdwnMode = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwnMode"))
                            using (TextBox txtFares = (TextBox)grdCorporateClaims.Rows[i].FindControl("txtFares"))
                            //using (TextBox txtDA = (TextBox)grdCorporateClaims.Rows[i].FindControl("txtDA"))
                            //using (TextBox TxtActualLoadgingconveyanceRS = (TextBox)grdCorporateClaims.Rows[i].FindControl("TxtActualLoadgingconveyanceRS"))
                            //using (TextBox TxtLocalconveyanceRs = (TextBox)grdCorporateClaims.Rows[i].FindControl("TxtLocalconveyanceRs"))
                            //using (TextBox TxtMiscExpencesParticulars = (TextBox)grdCorporateClaims.Rows[i].FindControl("TxtMiscExpencesParticulars"))
                            //using (TextBox TxtMiscExpencesParticularsRs = (TextBox)grdCorporateClaims.Rows[i].FindControl("TxtMiscExpencesParticularsRs"))
                            using (TextBox TxttTotalRs = (TextBox)grdCorporateClaims.Rows[i].FindControl("TxttTotalRs"))
                            using (DropDownList drpdwnEXPTYPE = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwnEXPTYPE"))
                            using (DropDownList drpdwncurrency = (DropDownList)grdCorporateClaims.Rows[i].FindControl("drpdwncurrency"))
                            {

                                if (!string.IsNullOrEmpty(TextDateTravel.Text) && !string.IsNullOrEmpty(TextDatetOTravel.Text)
                                    && drpdwnPlaceFrom.SelectedValue != "0" && drpdwnPlaceTo.SelectedValue != "0" && drpdwnMode.SelectedValue != "0")
                                {
                                    string strExpTYpe = drpdwnEXPTYPE.SelectedValue;
                                    string strToTravel = drpdwnPlaceTo.SelectedValue;
                                    string strRate = "";
                                    if (Session["Traveltype"].ToString().Equals("1"))
                                    { strRate = objEXPAddBl.Get_Amount_ForClaimExpType(User.Identity.Name, strExpTYpe, strToTravel); }
                                    else
                                    { strRate = objEXPAddBl.Get_Amount_ForClaimExpType_International(User.Identity.Name, strExpTYpe); }
                                    DateTime DtFrm = DateTime.ParseExact(TextDateTravel.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                    DateTime DtTo = DateTime.ParseExact(TextDatetOTravel.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                                    TimeSpan TimDiff = DtTo - DtFrm;
                                    int DaysDiff = TimDiff.Days;
                                    double Rate = double.Parse(string.IsNullOrEmpty(strRate) ? "0" : strRate) * DaysDiff;
                                    if (Rate >= double.Parse(string.IsNullOrEmpty(txtFares.Text) ? "0" : txtFares.Text))
                                    {
                                        objClaimsAddBo.DATE1 = Convert.ToDateTime(TextDateTravel.Text.ToString());
                                        objClaimsAddBo.DATE2 = Convert.ToDateTime(TextDatetOTravel.Text.ToString());
                                        objClaimsAddBo.PLACE_FROM = drpdwnPlaceFrom.SelectedValue.ToString();
                                        objClaimsAddBo.PLACE_TO = drpdwnPlaceTo.SelectedValue.ToString();
                                        objClaimsAddBo.MODE_OF_TRANSPORTATION = "";// drpdwnMode.SelectedValue.ToString();
                                        objClaimsAddBo.FARE = txtFares.Text.ToString();
                                        objClaimsAddBo.DAILY_ALLOWANCE = "";//txtDA.Text.ToString();
                                        objClaimsAddBo.LODGING_CHARGES = "";//TxtActualLoadgingconveyanceRS.Text.ToString();
                                        objClaimsAddBo.LOCAL_CONVEYANCE = "";// TxtLocalconveyanceRs.Text.ToString();
                                        objClaimsAddBo.DETAILS_MISC_EXP = "";// TxtMiscExpencesParticulars.Text.ToString();
                                        objClaimsAddBo.AMT_MISC_EXP = "";// TxtMiscExpencesParticularsRs.Text.ToString();
                                        objClaimsAddBo.TOTAL = "";// TxttTotalRs.Text.ToString();
                                        objClaimsAddBo.created_on = Convert.ToDateTime(System.DateTime.Now.ToString());
                                        bool? dd1 = true;
                                        objClaimsAddBo.status = "Review";
                                        objClaimsAddBo.SPKZL = drpdwnEXPTYPE.SelectedValue.ToString();
                                        objClaimsAddBo.WAERS = drpdwncurrency.SelectedValue.ToString();

                                        objEXPAddBl.Create_Corporate_Claims(objClaimsAddBo, ref dd1);

                                        if ((dd1 == true) || (dd1 == false))
                                        {
                                            iCnt = 1;
                                        }
                                    }
                                    else
                                    {
                                        ModelErrorMessage("Excess claimed for date " + TextDateTravel.Text + " to " + TextDatetOTravel.Text, System.Drawing.Color.Red);
                                        objClaimsAddBo = null;
                                    }
                                }
                            }
                        }
                    }
                }

                if (iCnt == 1)
                {
                    lblMessageBoard.Text = "Expense submitted successfully";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    divClaims.Visible = false;
                    //FuncClearData(); //to clear controls
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        //protected void drpdwnTo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (grdCorporateClaims.Rows.Count > 0)
        //    {
        //        Expensebl objBl = new Expensebl();

        //        GridViewRow row1 = (sender as DropDownList).Parent.Parent as GridViewRow;

        //        int a = row1.RowIndex;

        //        foreach (GridViewRow row in grdCorporateClaims.Rows)
        //        {
        //            if (row.RowIndex == a)
        //            {
        //                DropDownList drpdwnTo = (DropDownList)row.FindControl("drpdwnTo");
        //                string strToTravel = drpdwnTo.SelectedValue;

        //                string strRate = objBl.Get_Amount_ForDA(User.Identity.Name, "BORD", strToTravel);

        //                if ((strRate == string.Empty) || (strRate == "") || (strRate == null))
        //                {
        //                    strRate = "0";
        //                }

        //                TextBox txtDA = (TextBox)row.FindControl("txtDA");
        //                txtDA.Text = strRate;

        //                string strRate1 = objBl.Get_Amount_ForDA(User.Identity.Name, "LODG", strToTravel);

        //                if ((strRate1 == string.Empty) || (strRate1 == "") || (strRate1 == null))
        //                {
        //                    strRate1 = "0";
        //                }

        //                TextBox TxtLoadgingconveyanceRs = (TextBox)row.FindControl("TxtLoadgingconveyanceRs");
        //                TxtLoadgingconveyanceRs.Text = strRate1;
        //            }
        //        }
        //    }
        //}



        //protected void TxtMiscExpencesParticularsRs_TextChanged(object sender, EventArgs e)
        //{
        //    if (grdCorporateClaims.Rows.Count > 0)
        //    {
        //        Expensebl objBl = new Expensebl();

        //        for (int i = 0; i <= grdCorporateClaims.Rows.Count - 1; i++)
        //        {
        //            double iFares = 0.0;
        //            double iDA = 0.0;
        //            double iActualLoadgingconveyanceRS = 0.0;
        //            double iLocalconveyanceRS = 0.0;
        //            double iMiscExpencesParticularsRs = 0.0;

        //            TextBox txtFares = (TextBox)grdCorporateClaims.Rows[i].FindControl("txtFares");
        //            if ((txtFares.Text != "") || (txtFares.Text != string.Empty))
        //            {
        //                iFares = Convert.ToDouble(txtFares.Text);
        //            }

        //            TextBox txtDA = (TextBox)grdCorporateClaims.Rows[i].FindControl("txtDA");
        //            if ((txtDA.Text != "") || (txtDA.Text != string.Empty))
        //            {
        //                iDA = Convert.ToDouble(txtDA.Text);
        //            }

        //            TextBox TxtActualLoadgingconveyanceRS = (TextBox)grdCorporateClaims.Rows[i].FindControl("TxtActualLoadgingconveyanceRS");
        //            if ((TxtActualLoadgingconveyanceRS.Text != "" || TxtActualLoadgingconveyanceRS.Text != string.Empty))
        //            {
        //                iActualLoadgingconveyanceRS = Convert.ToDouble(TxtActualLoadgingconveyanceRS.Text);
        //            }

        //            TextBox TxtLocalconveyanceRs = (TextBox)grdCorporateClaims.Rows[i].FindControl("TxtLocalconveyanceRs");
        //            if ((TxtLocalconveyanceRs.Text != "" || TxtLocalconveyanceRs.Text != string.Empty))
        //            {
        //                iLocalconveyanceRS = Convert.ToDouble(TxtLocalconveyanceRs.Text);
        //            }

        //            TextBox TxtMiscExpencesParticularsRs = (TextBox)grdCorporateClaims.Rows[i].FindControl("TxtMiscExpencesParticularsRs");
        //            if ((TxtMiscExpencesParticularsRs.Text != "" || TxtMiscExpencesParticularsRs.Text != string.Empty))
        //            {
        //                iMiscExpencesParticularsRs = Convert.ToDouble(TxtMiscExpencesParticularsRs.Text);
        //            }

        //            TextBox TxttTotalRs = (TextBox)grdCorporateClaims.Rows[i].FindControl("TxttTotalRs");
        //            TxttTotalRs.Text = Convert.ToString(iFares + iDA + iActualLoadgingconveyanceRS + iLocalconveyanceRS + iMiscExpencesParticularsRs);
        //        }
        //    }
        //}


        protected void btnOveralltravel_Click(object sender, EventArgs e)
        {
            try
            {
                int i = Convert.ToInt32(txtCounter2.Text) +
                                 Convert.ToInt32(txtCounter3.Text) +
                                 Convert.ToInt32(txtCounter4.Text) +
                                 Convert.ToInt32(txtCounter5.Text) +
                                 Convert.ToInt32(txtCounter6.Text);
                if (i > 0)
                    txtCounter7.Text = (i / 5).ToString();
                else
                    txtCounter7.Text = "0";
            }
            catch (Exception ex)
            {

            }
        }

        //------------------------

        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod]
        //public static List<string> GetAmount(string prefixText, string contextKey)
        //{
        //    try
        //    {
        //        string a = prefixText;
        //        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString());
        //        //con.Open();
        //        //SqlCommand cmd = new SqlCommand("select * from Country where CountryName like @Name+'%'", con);
        //        //cmd.Parameters.AddWithValue("@Name", prefixText);
        //        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        //DataTable dt = new DataTable();
        //        //da.Fill(dt);
        //        List<string> CountryNames = new List<string>();
        //        for (int i = 0; i < 1; i++)
        //        {


        //            CountryNames.Add(prefixText);
        //        }

        //        return CountryNames;
        //    }
        //    catch (Exception Ex)
        //    { string Msg = Ex.Message; return null; }            
        //}

        protected void txtFares_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdCorporateClaims.Rows.Count > 0)
                {
                    Expensebl objBl = new Expensebl();
                    using (GridViewRow row1 = (sender as TextBox).Parent.Parent as GridViewRow)
                    {
                        int a = row1.RowIndex;

                        foreach (GridViewRow row in grdCorporateClaims.Rows)
                        {
                            if (row.RowIndex == a)
                            {
                                using (TextBox TxtTrvlFrmDt = (TextBox)row.FindControl("TextDateTravel"))
                                using (TextBox TxtTrvlToDt = (TextBox)row.FindControl("TextDateTravelTo"))
                                using (DropDownList drpdwnTo = (DropDownList)row.FindControl("drpdwnTo"))
                                using (DropDownList DDLExpType = (DropDownList)row.FindControl("drpdwnEXPTYPE"))
                                {
                                    DateTime DtFrm = DateTime.ParseExact(TxtTrvlFrmDt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                    DateTime DtTo = DateTime.ParseExact(TxtTrvlToDt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                                    TimeSpan TimDiff = DtTo - DtFrm;
                                    int DaysDiff = TimDiff.Days;

                                    string strToTravel = drpdwnTo.SelectedValue;

                                    //string strRate = objBl.Get_Amount_ForDA(User.Identity.Name, "BORD", strToTravel);

                                    //if ((strRate == string.Empty) || (strRate == "") || (strRate == null))
                                    //{
                                    //    strRate = "0";
                                    //}

                                    string strRate = "";
                                    string strExpTYpe = DDLExpType.SelectedValue;
                                    if (Session["Traveltype"].ToString().Equals("1"))
                                    {
                                        strRate = objBl.Get_Amount_ForClaimExpType(User.Identity.Name, strExpTYpe, strToTravel);
                                    }
                                    else
                                    {
                                        strRate = objBl.Get_Amount_ForClaimExpType_International(User.Identity.Name, strExpTYpe);
                                    }


                                    if ((strRate == string.Empty) || (strRate == "") || (strRate == null))
                                    {
                                        strRate = "0";
                                    }


                                    double Rate = double.Parse(strRate) * DaysDiff;

                                    using (TextBox txtFares = (TextBox)row.FindControl("txtFares"))
                                    {
                                        using (Label LblMsg = (Label)row.FindControl("LblMsg"))
                                        {
                                            if (double.Parse(txtFares.Text) >= Rate)
                                            {
                                                LblMsg.Text = "Max. Amount - " + Rate;
                                                //btnSave.Enabled = false;
                                            }
                                            else
                                            {
                                                LblMsg.Text = "Max. Amount - " + Rate;
                                                //btnSave.Enabled = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        #region Error Message
        private void ModelErrorMessage(string Msg, System.Drawing.Color MsgColor)
        {
            try
            {
                lblMessageBoard.Text = string.Empty;
                lblMessageBoard.Text = Msg;
                lblMessageBoard.ForeColor = MsgColor;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
        #endregion
        #region Get Empty Claim DataTable
        private DataTable GetEmptyClaimDt()
        {
            try
            {
                var XMLPath = Server.MapPath("~/UI/Benefits_Payment/XMLFile1.xml");//Location of the XML file.
                DataSet Ds = new DataSet();
                Ds.ReadXml(XMLPath);// Load XML in dataset               
                Ds.Tables[0].Clear();
                return Ds.Tables[0];
            }
            catch (Exception Ex)
            { throw Ex; return null; }
        }


        protected void Gv_Nodata(GridView Gv, DataTable Dt)
        {
            try
            {
                //Add blank row to the the resultset
                Dt.Rows.Add(Dt.NewRow());
                Gv.DataSource = Dt;
                Gv.DataBind();
                //Hide empty row
                Gv.Rows[0].Visible = false;
                Gv.Rows[0].Controls.Clear();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }
        #endregion
    }
}

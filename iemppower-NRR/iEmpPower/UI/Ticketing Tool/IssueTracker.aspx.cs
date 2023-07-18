using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Ticketing_Tool;

namespace iEmpPower.UI.Ticketing_Tool
{
    public partial class IssueTracker : System.Web.UI.Page
    {

        protected int PendingPageIndex = 1;
        protected int TaskPageIndex = 1;
        static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSLA();
                if (Request.UrlReferrer != null)
                {
                    prevPage = Request.UrlReferrer.AbsolutePath.ToString();
                    if (!prevPage.Contains("CreateIssueTicket"))
                    {
                        Session["PendingPageIndex"] = "0";
                        Session["DDLStatusSearch"] = "0";
                        Session["DDLCustomerList"] = "0";
                        Session["TxtFromDate"] = "";
                        Session["TxtToDate"] = "";
                        Session["pagesize"] = "0";
                    }
                }
                else
                {
                    Session["PendingPageIndex"] = "0";
                    Session["DDLStatusSearch"] = "0";
                    Session["DDLCustomerList"] = "0";
                    Session["TxtFromDate"] = "";
                    Session["TxtToDate"] = "";
                    Session["pagesize"] = "0";
                }
                
                LoadStatus();
                LoadCustomerSrch();

              

                if (Session["TktStutsID"] == null || Session["TktStutsID"].ToString() == "0")
                {
                    if (Session["PendingPageIndex"] == null || (Session["PendingPageIndex"].ToString() == "0"))
                    {
                        if (Session["DDLStatusSearch"] == null || (Session["DDLStatusSearch"].ToString() == "0"))
                        {
                            if (Session["DDLCustomerList"] == null || (Session["DDLCustomerList"].ToString() == "0"))
                            {
                                if ((Session["TxtFromDate"] == null || (Session["TxtFromDate"].ToString().Trim() == "")) || (Session["TxtToDate"] == null || (Session["TxtToDate"].ToString().Trim() == "")))
                                {
                                    DDLStatusSearch.SelectedValue = "1";
                                    DDLCustomerList.SelectedValue = "4";
                                    Session["DDLStatusSearch"] = DDLStatusSearch.SelectedValue.ToString().Trim();
                                    Session["DDLCustomerList"] = DDLCustomerList.SelectedValue.ToString().Trim();
                                    Session["TxtFromDate"] = string.IsNullOrEmpty(TxtFromDate.Text) ? "" : DateTime.Parse(TxtFromDate.Text).ToString();
                                    Session["TxtToDate"] = string.IsNullOrEmpty(TxtToDate.Text) ? "" : DateTime.Parse(TxtToDate.Text).ToString();
                                    DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
                                    DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                                    LoadAllTickets(1, 1, DDLCustomerList.SelectedValue.ToString().Trim(), Fromdate, Todate);
                                }
                                else
                                {
                                    DDLStatusSearch.SelectedValue = "1";
                                    DDLCustomerList.SelectedValue = "4";
                                    Session["DDLStatusSearch"] = DDLStatusSearch.SelectedValue.ToString().Trim();
                                    Session["DDLCustomerList"] = DDLCustomerList.SelectedValue.ToString().Trim();
                                    DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
                                    DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
                                    LoadAllTickets(1, 1, DDLCustomerList.SelectedValue.ToString().Trim(), Fromdate, Todate);
                                }
                            }
                            else
                            {
                                DDLStatusSearch.SelectedValue = "1";
                                DDLCustomerList.SelectedValue = Session["DDLCustomerList"].ToString().Trim();
                                Session["TxtFromDate"] = string.IsNullOrEmpty(TxtFromDate.Text) ? "" : DateTime.Parse(TxtFromDate.Text).ToString();
                                Session["TxtToDate"] = string.IsNullOrEmpty(TxtToDate.Text) ? "" : DateTime.Parse(TxtToDate.Text).ToString();
                                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
                                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                                LoadAllTickets(1, 1, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
                            }
                        }
                        else
                        {
                            DDLStatusSearch.SelectedValue = Session["DDLStatusSearch"].ToString().Trim();
                            DDLCustomerList.SelectedValue = Session["DDLCustomerList"].ToString().Trim();
                            TxtFromDate.Text = Session["TxtFromDate"].ToString().Trim();
                            TxtToDate.Text = Session["TxtToDate"].ToString().Trim();
                            DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
                            DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
                            LoadAllTickets(int.Parse(Session["DDLStatusSearch"].ToString().Trim()), 1, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
                        }
                    }
                    else
                    {
                        DDLStatusSearch.SelectedValue = Session["DDLStatusSearch"].ToString().Trim();
                        DDLCustomerList.SelectedValue = Session["DDLCustomerList"].ToString().Trim();
                        TxtFromDate.Text = Session["TxtFromDate"].ToString().Trim();
                        TxtToDate.Text = Session["TxtToDate"].ToString().Trim();
                        DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
                        DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
                        LoadAllTickets(int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), int.Parse(Session["PendingPageIndex"].ToString().Trim()), Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
                    }
                }
                else if (Session["TktStutsID"].ToString() != "0")
                {
                    DDLStatusSearch.SelectedValue = "0";
                    if (Session["PendingPageIndex"] == null || (Session["PendingPageIndex"].ToString() == "0"))
                    {
                        DDLCustomerList.SelectedValue = "4";
                        Session["DDLCustomerList"] = DDLCustomerList.SelectedValue.ToString().Trim();
                        Session["TxtFromDate"] = string.IsNullOrEmpty(TxtFromDate.Text) ? "" : DateTime.Parse(TxtFromDate.Text).ToString();
                        Session["TxtToDate"] = string.IsNullOrEmpty(TxtToDate.Text) ? "" : DateTime.Parse(TxtToDate.Text).ToString();
                        DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
                        DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                        LoadAllTickets(0, 1, DDLCustomerList.SelectedValue.ToString().Trim(), Fromdate, Todate);
                    }
                    else
                    {
                        Session["DDLCustomerList"] = DDLCustomerList.SelectedValue.ToString().Trim();
                        Session["TxtFromDate"] = string.IsNullOrEmpty(TxtFromDate.Text) ? "" : DateTime.Parse(TxtFromDate.Text).ToString();
                        Session["TxtToDate"] = string.IsNullOrEmpty(TxtToDate.Text) ? "" : DateTime.Parse(TxtToDate.Text).ToString();
                        DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
                        DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                        LoadAllTickets(0, int.Parse(Session["PendingPageIndex"].ToString().Trim()), DDLCustomerList.SelectedValue.ToString().Trim(), Fromdate, Todate);
                    }

                    bool bSortedOrder = false;
                    Session.Add("bSortedOrder", bSortedOrder);
                }
                LoadAllTask(1, 1);
            }
            if (User.Identity.Name == "cssteam")
            {
                BtnNewTicket.Visible = false;
            }

            if (DDLStatusSearch.SelectedValue == "2")
            {
                if (TxtFromDate.Text == "" && TxtToDate.Text == "")
                {
                    TxtFromDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).ToString("dd/MM/yyyy");
                    TxtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                Session["TxtFromDate"] = Fromdate;
                Session["TxtToDate"] = Todate;
                LoadAllTickets(2, 1, DDLCustomerList.SelectedValue.ToString().Trim(), Fromdate, Todate);
            }
            else
            {
                 Session["TxtFromDate"]=TxtFromDate.Text = "";
                 Session["TxtToDate"]=TxtToDate.Text = "";
            }
        }

        protected void LoadStatus()
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_StatusSearch(User.Identity.Name);
            DDLStatusSearch.DataSource = objLst;
            DDLStatusSearch.DataTextField = "SearchStatusTxt";
            DDLStatusSearch.DataValueField = "SearchStatusID";
            DDLStatusSearch.DataBind();
            DDLStatusSearch.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
        }

        protected void LoadCustomerSrch()
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_customerwiseSrch(User.Identity.Name);
            DDLCustomerList.DataSource = objLst;
            DDLCustomerList.DataTextField = "customertxt";
            DDLCustomerList.DataValueField = "customerId";
            DDLCustomerList.DataBind();
            //DDLCustomerList.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
        }


        public void LoadAllTickets(int StatusTyp, int PageIndex, string custtype, DateTime Fromdate, DateTime Todate)
        {
            try
            {
                int? RecordCnt = 0;
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
                if (Session["pagesize"] == null || (Session["pagesize"].ToString() == "0"))
                {
                    TicketingObjBo.PageSize = int.Parse(ddlPagesize.SelectedValue.ToString().Trim());
                }
                else
                {
                    TicketingObjBo.PageSize = int.Parse(Session["pagesize"].ToString().Trim());
                }
                TicketingObjBo.PageIndex = PageIndex;
                if (Session["chartNo"].ToString() == "0" && Session["TktStutsID"].ToString() == "0")
                {
                    TicketingboList = TicketingObjBl.Load_AllTickets(User.Identity.Name, StatusTyp, custtype, Fromdate, Todate, string.IsNullOrEmpty(TxtTID.Text.Trim()) ? 0 : long.Parse(TxtTID.Text.Trim()), TicketingObjBo, Convert.ToInt32(ViewState["SLATmPerID"]), ref RecordCnt);
                }
                else
                {
                    // DDLStatusSearch.SelectedValue = "0";
                    TicketingboList = TicketingObjBl.Load_AllTickets_BasdOnChart(User.Identity.Name, Convert.ToInt32(Session["chartNo"]), Session["TktStutsID"].ToString(), Convert.ToDateTime(Session["TTFRMDATE"]), Convert.ToDateTime(Session["TTTODATE"]), TicketingObjBo, ref RecordCnt);
                }
                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    MsgCls("No Records Found !", LblTicket, System.Drawing.Color.Red);
                    GV_Tickets.Visible = true;
                    GV_Tickets.DataSource = null;
                    GV_Tickets.DataBind();
                    ticketdiv.Visible = true;
                    pnlgrid.Visible = false;
                    return;
                }
                else
                {
                    MsgCls("", LblTicket, System.Drawing.Color.Transparent);
                    ticketdiv.Visible = true;
                    pnlgrid.Visible = true;
                    GV_Tickets.Visible = true;
                    GV_Tickets.DataSource = null;
                    GV_Tickets.DataBind();
                    GV_Tickets.DataSource = TicketingboList;
                    GV_Tickets.SelectedIndex = -1;
                    GV_Tickets.DataBind();
                }
                Session.Add("VSTicketsData", TicketingboList);
                PopulatePendingPager(TicketingboList.Count > 0 ? int.Parse(RecordCnt.ToString()) : 0, PageIndex);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }
        #region Pending Populate pager
        private void PopulatePendingPager(int RecordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = (Session["pagesize"] == null || (Session["pagesize"].ToString() == "0")) ? int.Parse(ddlPagesize.SelectedValue.ToString().Trim()) : int.Parse(Session["pagesize"].ToString().Trim());

                //Calculate the Start and End Index of pages to be displayed.
                double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal(pagerSpan));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
                endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
                if (currentPage > pagerSpan % 2)
                {
                    if (currentPage == 2)
                    { endIndex = 5; }
                    else
                    { endIndex = currentPage + 2; }
                }
                else
                { endIndex = (pagerSpan - currentPage) + 1; }

                if (endIndex - (pagerSpan - 1) > startIndex)
                { startIndex = endIndex - (pagerSpan - 1); }

                if (endIndex > pageCount)
                {
                    endIndex = pageCount;
                    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
                }

                //Add the First Page Button.
                if (currentPage > 1)
                { pages.Add(new ListItem("First", "1")); }

                //Add the Previous Button.
                if (currentPage > 1)
                { pages.Add(new ListItem("<<", (currentPage - 1).ToString())); }

                for (int i = startIndex; i <= endIndex; i++)
                { pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage)); }

                //Add the Next Button.
                if (currentPage < pageCount)
                { pages.Add(new ListItem(">>", (currentPage + 1).ToString())); }

                //Add the Last Button.
                if (currentPage != pageCount)
                { pages.Add(new ListItem("Last", pageCount.ToString())); }
                RptrPendingPager.DataSource = pages;
                RptrPendingPager.DataBind();

                //GV_ClockInClockOut.FooterRow.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;<b>Page " + currentPage + " of " + pageCount + "<b/>";

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void PendingPage_Changed(object sender, EventArgs e)
        {
            try
            {
                Session["PendingPageIndex"] = int.Parse((sender as LinkButton).CommandArgument);
                int pageIndex = PendingPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
                LoadAllTickets(int.Parse(DDLStatusSearch.SelectedValue.ToString()), pageIndex, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        #endregion

        public void LoadAllTask(int StatusTyp, int PageIndex)
        {
            try
            {
                int? TaskRecordCnt = 0;
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
                TicketingObjBo.PageSize = int.Parse(ddlPaseSizeTAsk.SelectedValue.ToString().Trim());
                TicketingObjBo.PageIndex = TaskPageIndex;
                TicketingboList = TicketingObjBl.Load_AllTask(User.Identity.Name, StatusTyp, TicketingObjBo, ref TaskRecordCnt);


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    MsgCls("No Records Found !", LblTask, System.Drawing.Color.Red);
                    GV_Task.Visible = false;
                    taskdiv.Visible = true;
                    GV_Task.DataSource = null;
                    GV_Task.DataBind();
                    Pnltask.Visible = false;
                    TicketingToolbo TicketingObjBo2 = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl2 = new TicketingToolbl();
                    msassignedtomebo objTicketingToolbo2 = new msassignedtomebo();
                    msassignedtomebl objPIDashBoardB2 = new msassignedtomebl();
                    objTicketingToolbo2.PERNR = HttpContext.Current.User.Identity.Name;
                    msassignedtomecollectionbo objPIDashBoardLst2 = objPIDashBoardB2.Get_Sub_Employees_Of_ManagerForMSS(objTicketingToolbo2);
                    string Status = "";
                    TicketingObjBl2.CheckIfclients(1, User.Identity.Name, ref Status);
                    if (Status == "True") // client
                    {

                        GV_Task.Visible = false;
                        taskdiv.Visible = false;
                        Taskfs.Visible = false;
                    }
                    else if (User.Identity.Name == "cssteam")
                    {
                        GV_Task.Visible = false;
                        taskdiv.Visible = false;
                        Taskfs.Visible = false;
                    }

                    else if (objPIDashBoardLst2.Count > 0)
                    {
                        GV_Task.Visible = false;
                        taskdiv.Visible = true;
                        Taskfs.Visible = true;
                    }
                    else
                    {
                        GV_Task.Visible = false;
                        taskdiv.Visible = true;
                        Taskfs.Visible = true;
                    }


                    return;
                }
                else
                {
                    MsgCls("", LblTask, System.Drawing.Color.Transparent);
                    GV_Task.Visible = true;
                    taskdiv.Visible = true;
                    GV_Task.DataSource = null;
                    GV_Task.DataBind();
                    GV_Task.DataSource = TicketingboList;
                    GV_Task.SelectedIndex = -1;
                    GV_Task.DataBind();
                    Pnltask.Visible = true;
                }

                TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                TicketingToolbl TicketingObjBl1 = new TicketingToolbl();
                msassignedtomebo objTicketingToolbo = new msassignedtomebo();
                msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                objTicketingToolbo.PERNR = HttpContext.Current.User.Identity.Name;
                msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objTicketingToolbo);
                string Status1 = "";
                TicketingObjBl1.CheckIfclients(1, User.Identity.Name, ref Status1);
                if (Status1 == "True") // client
                {
                    GV_Task.Visible = false;
                    taskdiv.Visible = false;
                    Taskfs.Visible = false;
                }
                else if (User.Identity.Name == "cssteam")
                {
                    GV_Task.Visible = false;
                    taskdiv.Visible = false;
                    Taskfs.Visible = false;
                }

                else if (objPIDashBoardLst.Count > 0)
                {
                    GV_Task.Visible = true;
                    taskdiv.Visible = true;
                    Taskfs.Visible = true;
                }
                else
                {
                    GV_Task.Visible = true;
                    taskdiv.Visible = true;
                    Taskfs.Visible = true;
                }
                Session.Add("VSTaskData", TicketingboList);
                PopulateTaskPager(TicketingboList.Count > 0 ? int.Parse(TaskRecordCnt.ToString()) : 0, TaskPageIndex);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        #region Task Populate pager
        private void PopulateTaskPager(int RecordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = int.Parse(ddlPaseSizeTAsk.SelectedValue.ToString().Trim());

                //Calculate the Start and End Index of pages to be displayed.
                double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal(pagerSpan));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
                endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
                if (currentPage > pagerSpan % 2)
                {
                    if (currentPage == 2)
                    { endIndex = 5; }
                    else
                    { endIndex = currentPage + 2; }
                }
                else
                { endIndex = (pagerSpan - currentPage) + 1; }

                if (endIndex - (pagerSpan - 1) > startIndex)
                { startIndex = endIndex - (pagerSpan - 1); }

                if (endIndex > pageCount)
                {
                    endIndex = pageCount;
                    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
                }

                //Add the First Page Button.
                if (currentPage > 1)
                { pages.Add(new ListItem("First", "1")); }

                //Add the Previous Button.
                if (currentPage > 1)
                { pages.Add(new ListItem("<<", (currentPage - 1).ToString())); }

                for (int i = startIndex; i <= endIndex; i++)
                { pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage)); }

                //Add the Next Button.
                if (currentPage < pageCount)
                { pages.Add(new ListItem(">>", (currentPage + 1).ToString())); }

                //Add the Last Button.
                if (currentPage != pageCount)
                { pages.Add(new ListItem("Last", pageCount.ToString())); }
                RepeaterTask.DataSource = pages;
                RepeaterTask.DataBind();

                //GV_ClockInClockOut.FooterRow.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;<b>Page " + currentPage + " of " + pageCount + "<b/>";

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        protected void lnkPage_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = TaskPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                LoadAllTask(int.Parse(DDLStatusSearch.SelectedValue.ToString()), pageIndex);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        #endregion

        private void MsgCls(string Msg, Label Lbl, Color Clr)
        {
            try
            {
                Lbl.Text = string.Empty;
                Lbl.Text = Msg;
                Lbl.ForeColor = Clr;
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void BtnNewTicket_Click(object sender, EventArgs e)
        {
            Session["TransferdTicketId"] = "New";
            Session["TransferdValue"] = "0";
            Response.Redirect("~/UI/Ticketing Tool/CreateIssueTicket.aspx");
        }

        protected void GV_Tickets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
                        long TicketId = int.Parse(GV_Tickets.DataKeys[int.Parse(e.CommandArgument.ToString())]["TID"].ToString().Trim());
                        Session["TransferdTicketId"] = int.Parse(GV_Tickets.DataKeys[int.Parse(e.CommandArgument.ToString())]["TID"].ToString().Trim());
                        Session["TransferdValue"] = "1";
                        Response.Redirect("~/UI/Ticketing Tool/CreateIssueTicket.aspx");
                        //Response.Redirect("~/UI/Ticketing Tool/CreateIssueTicket.aspx?Type=" + TicketId + "&Value=" + 1);
                        break;

                    case "FBVIEW":
                        mp1.Show();
                        long TicketIdrating = int.Parse(GV_Tickets.DataKeys[int.Parse(e.CommandArgument.ToString())]["TID"].ToString().Trim());
                        RTID.Text = GV_Tickets.DataKeys[int.Parse(e.CommandArgument.ToString())]["TID"].ToString().Trim();
                        int? rate1 = 0;
                        int? rate2 = 0;
                        int? rate3 = 0;
                        int? rate4 = 0;
                        int? rate5 = 0;
                        string sug = "";
                        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
                        objTicketingToolDataContext.usp_tcikety_Get_Custrating(TicketIdrating, ref rate1, ref rate2, ref rate3, ref rate4, ref rate5, ref sug);

                        Rating1.CurrentRating = int.Parse(rate1.ToString().Trim());
                        Rating2.CurrentRating = int.Parse(rate2.ToString().Trim());
                        Rating3.CurrentRating = int.Parse(rate3.ToString().Trim());
                        Rating4.CurrentRating = int.Parse(rate4.ToString().Trim());
                        Rating5.CurrentRating = int.Parse(rate5.ToString().Trim());
                        RatingSug.Text = sug;

                        Rating1.ReadOnly = true;
                        Rating2.ReadOnly = true;
                        Rating3.ReadOnly = true;
                        Rating4.ReadOnly = true;
                        Rating5.ReadOnly = true;

                        break;
                    default:
                        break;
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["SLATmPerID"] = "0";
                Session["chartNo"] = "0";
                Session["TktStutsID"] = "0";
                Session["DDLStatusSearch"] = int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim());
                Session["PendingPageIndex"] = 1;
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
                LoadAllTickets(int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), 1, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
                LoadAllTask(int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), 1);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void GV_Task_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
                        long TicketId = int.Parse(GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TICKETID"].ToString().Trim());
                        long TaskId = int.Parse(GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TASKID"].ToString().Trim());
                        Session["TransferdTicketId"] = int.Parse(GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TICKETID"].ToString().Trim());
                        Session["TransferdValue"] = "2";
                        Response.Redirect("~/UI/Ticketing Tool/CreateIssueTicket.aspx");
                        //Response.Redirect("~/UI/Ticketing Tool/CreateIssueTicket.aspx?Type=" + TicketId + "&Value=" + 2);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        //protected void GV_Tickets_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {

        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            string Status = GV_Tickets.DataKeys[e.Row.RowIndex].Values[2].ToString();

        //            if (Status.ToString().Trim() != "9" && Status.ToString().Trim() != "11" && Status.ToString().Trim() != "12" && Status.ToString().Trim() != "13")
        //            {
        //                //Get the value of column from the DataKeys using the RowIndex.     
        //                string group = GV_Tickets.DataKeys[e.Row.RowIndex].Values[1].ToString();

        //                if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 0) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 40)
        //                {
        //                    e.Row.Cells[10].BackColor = System.Drawing.Color.Green;
        //                }
        //                else if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 40) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 80)
        //                {
        //                    e.Row.Cells[10].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBF00");

        //                }
        //                else if ((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 80)
        //                {
        //                    e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
        //                }
        //                else
        //                {
        //                    e.Row.Cells[10].BackColor = System.Drawing.Color.Transparent;
        //                }

        //            }

        //            //GV_Tickets.DataKeys[int.Parse(e.CommandArgument.ToString())]["PERCENTAGE"].ToString().Trim()
        //            //if ((string.IsNullOrEmpty(e.Row.Cells[11].Text)?0:decimal.Parse(e.Row.Cells[11].Text)) <=40)
        //            //{
        //            //    e.Row.Cells[10].BackColor = System.Drawing.Color.Green;
        //            //}
        //            //else if (((string.IsNullOrEmpty(e.Row.Cells[11].Text) ? 0 : decimal.Parse(e.Row.Cells[11].Text)) > 40) && (string.IsNullOrEmpty(e.Row.Cells[11].Text)?0:decimal.Parse(e.Row.Cells[11].Text)) <= 80)
        //            //{
        //            //    e.Row.Cells[10].BackColor =  System.Drawing.ColorTranslator.FromHtml("#FFBF00");

        //            //}
        //            //else if ((string.IsNullOrEmpty(e.Row.Cells[11].Text) ? 0 : decimal.Parse(e.Row.Cells[11].Text)) > 80)
        //            //{
        //            //    e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
        //            //}
        //            //else
        //            //{
        //            //    e.Row.Cells[10].BackColor = System.Drawing.Color.Transparent;
        //            //}
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        //}

        //protected void OnDataBound(object sender, EventArgs e)
        //{
        //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //    for (int i = 0; i < GV_Tickets.Columns.Count - 2; i++)
        //    {
        //        TableHeaderCell cell = new TableHeaderCell();
        //        TextBox txtSearch = new TextBox();
        //        txtSearch.Attributes["placeholder"] = GV_Tickets.Columns[i].HeaderText;
        //        txtSearch.CssClass = "search_textbox";
        //        cell.Controls.Add(txtSearch);
        //        row.Controls.Add(cell);
        //    }
        //    GV_Tickets.HeaderRow.Parent.Controls.AddAt(1, row);
        //}

        //protected void GV_Tickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    { 

        //        int pageindex = e.NewPageIndex;
        //        GV_Tickets.PageIndex = e.NewPageIndex;
        //        LoadAllTickets(int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()),1);
        //        List<TicketingToolbo> objPIDashBoardLst = (List<TicketingToolbo>)Session["VSTicketsData"];
        //        GV_Tickets.DataSource = objPIDashBoardLst;
        //        GV_Tickets.DataBind();

        //        // LoadAllTask(int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()));
        //        GV_Tickets.SelectedIndex = -1;
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

        //protected void GV_Task_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        int pageindex = e.NewPageIndex;
        //        GV_Task.PageIndex = e.NewPageIndex;
        //        //LoadAllTickets(int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()));
        //        LoadAllTask(int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()),1);
        //        List<TicketingToolbo> objPIDashBoardLst = (List<TicketingToolbo>)Session["VSTaskData"];
        //        GV_Task.DataSource = objPIDashBoardLst;
        //        GV_Task.DataBind();
        //        GV_Task.SelectedIndex = -1;
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

        protected void GV_Tickets_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<TicketingToolbo> objPIDashBoardLst = (List<TicketingToolbo>)Session["VSTicketsData"];
            bool objSortOrder = (bool)Session["bSortedOrder"];
            switch (e.SortExpression.ToString().Trim())
            {
                case "TID":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((long.Parse(objBo1.TID.ToString())).CompareTo(long.Parse(objBo2.TID.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((long.Parse(objBo2.TID.ToString())).CompareTo(long.Parse(objBo1.TID.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "TITLE":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.TITLE.CompareTo(objBo2.TITLE)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.TITLE.CompareTo(objBo1.TITLE)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;


                case "CLIENT":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.CLIENT.CompareTo(objBo2.CLIENT)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.CLIENT.CompareTo(objBo1.CLIENT)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "FRMUSR":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.FRMUSR.CompareTo(objBo2.FRMUSR)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.FRMUSR.CompareTo(objBo1.FRMUSR)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;


                case "ASSIGNEE":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.ASSIGNEE.CompareTo(objBo2.ASSIGNEE)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.ASSIGNEE.CompareTo(objBo1.ASSIGNEE)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "PriorityTxt":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.PriorityTxt.CompareTo(objBo2.PriorityTxt)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.PriorityTxt.CompareTo(objBo1.PriorityTxt)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CategoryTxt":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.CategoryTxt.CompareTo(objBo2.CategoryTxt)); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.CategoryTxt.CompareTo(objBo1.CategoryTxt)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "IssueType":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.IssueTypeTxt.CompareTo(objBo2.IssueTypeTxt)); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.IssueTypeTxt.CompareTo(objBo1.IssueTypeTxt)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "AGENT":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.AGENT.CompareTo(objBo2.AGENT)); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.AGENT.CompareTo(objBo1.AGENT)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "TIDREF":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.TIDREF.ToString()).CompareTo(objBo2.TIDREF.ToString()); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.TIDREF.ToString()).CompareTo(objBo1.TIDREF.ToString()); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "StatusTxt":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.StatusTxt.CompareTo(objBo2.StatusTxt)); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.StatusTxt.CompareTo(objBo1.StatusTxt)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CREATED_ON":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (DateTime.Parse(objBo1.CREATED_ON.ToString()).CompareTo(DateTime.Parse(objBo2.CREATED_ON.ToString()))); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (DateTime.Parse(objBo2.CREATED_ON.ToString()).CompareTo(DateTime.Parse(objBo1.CREATED_ON.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "LASTMODIFIED_BY":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.LASTMODIFIED_BY.CompareTo(objBo2.LASTMODIFIED_BY)); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.LASTMODIFIED_BY.CompareTo(objBo1.LASTMODIFIED_BY)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "LASTMODIFIED_ON":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (DateTime.Parse(objBo1.LASTMODIFIED_ON.ToString()).CompareTo(DateTime.Parse(objBo2.LASTMODIFIED_ON.ToString()))); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (DateTime.Parse(objBo2.LASTMODIFIED_ON.ToString()).CompareTo(DateTime.Parse(objBo1.LASTMODIFIED_ON.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
            }
            GV_Tickets.DataSource = objPIDashBoardLst;
            GV_Tickets.DataBind();
            Session.Add("VSTicketsData", objPIDashBoardLst);
        }

        protected void GV_Tickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {


                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='none';";
                    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

                    for (int i = 0; i < e.Row.Cells.Count - 2; i++)
                    {
                        e.Row.Cells[i].ToolTip = "Click to Edit";
                        e.Row.Cells[i].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GV_Tickets, "Select$" + e.Row.RowIndex);
                    }

                    string Status = GV_Tickets.DataKeys[e.Row.RowIndex].Values[2].ToString();
                    string CATEGORY = GV_Tickets.DataKeys[e.Row.RowIndex].Values[3].ToString().Trim();
                    if (Status.ToString().Trim() != "13" && Status.ToString().Trim() != "14" && Status.ToString().Trim() != "4" && Status.ToString().Trim() != "12" && Status.ToString().Trim() != "11")
                    {
                        if (Convert.ToDouble(GV_Tickets.DataKeys[e.Row.RowIndex].Values[4].ToString().Trim()) > 0)
                        {
                            TimeSpan sla_time = TimeSpan.FromSeconds(Convert.ToDouble(GV_Tickets.DataKeys[e.Row.RowIndex].Values[4].ToString().Trim()));
                            //sla_Brchtime = sla_time.ToString(@"hh\:mm\:ss\:fff");
                            e.Row.Cells[17].Text = sla_time.ToString();
                            e.Row.Cells[16].Text = "Yes";
                        }
                        else
                        {
                            e.Row.Cells[17].Text = "-";
                            e.Row.Cells[16].Text = "No";
                        }
                    }
                    else
                    {
                        e.Row.Cells[17].Text = "-";
                        e.Row.Cells[16].Text = "-";
                    }
                    if (CATEGORY.ToString().Trim() == "2" || CATEGORY.ToString().Trim() == "3")
                    {

                        if (Status.ToString().Trim() != "1" && Status.ToString().Trim() != "14" && Status.ToString().Trim() != "4" && Status.ToString().Trim() != "7" && Status.ToString().Trim() != "8" && Status.ToString().Trim() != "9" && Status.ToString().Trim() != "11" && Status.ToString().Trim() != "12" && Status.ToString().Trim() != "13")
                        {
                            //Get the value of column from the DataKeys using the RowIndex.     
                            string group = GV_Tickets.DataKeys[e.Row.RowIndex].Values[1].ToString();

                            if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 0) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 40)
                            {
                                e.Row.Cells[12].BackColor = System.Drawing.Color.Green;
                                e.Row.Cells[12].ForeColor = System.Drawing.Color.White;

                            }
                            else if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 40) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 80)
                            {
                                e.Row.Cells[12].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBF00");
                                e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;

                            }
                            else if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 80) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) < 100)
                            {
                                e.Row.Cells[12].BackColor = System.Drawing.ColorTranslator.FromHtml("#f75f0b");
                                e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;

                            }
                            //else if ((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 80)
                            //{
                            //    e.Row.Cells[11].BackColor = System.Drawing.ColorTranslator.FromHtml("#f75f0b");   
                            //    e.Row.Cells[11].ForeColor = System.Drawing.Color.White;
                            //}
                            else if ((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) >= 100)
                            {
                                e.Row.Cells[12].BackColor = System.Drawing.ColorTranslator.FromHtml("#ea0d14");
                                e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                e.Row.Cells[12].BackColor = System.Drawing.Color.White;
                                e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                            }

                        }
                        else
                        {
                            e.Row.Cells[12].BackColor = System.Drawing.Color.White;
                            e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                        }
                    }
                    else
                    {
                        e.Row.Cells[12].BackColor = System.Drawing.Color.White;
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                    }

                    //GV_Tickets.DataKeys[int.Parse(e.CommandArgument.ToString())]["PERCENTAGE"].ToString().Trim()
                    //if ((string.IsNullOrEmpty(e.Row.Cells[11].Text)?0:decimal.Parse(e.Row.Cells[11].Text)) <=40)
                    //{
                    //    e.Row.Cells[10].BackColor = System.Drawing.Color.Green;
                    //}
                    //else if (((string.IsNullOrEmpty(e.Row.Cells[11].Text) ? 0 : decimal.Parse(e.Row.Cells[11].Text)) > 40) && (string.IsNullOrEmpty(e.Row.Cells[11].Text)?0:decimal.Parse(e.Row.Cells[11].Text)) <= 80)
                    //{
                    //    e.Row.Cells[10].BackColor =  System.Drawing.ColorTranslator.FromHtml("#FFBF00");

                    //}
                    //else if ((string.IsNullOrEmpty(e.Row.Cells[11].Text) ? 0 : decimal.Parse(e.Row.Cells[11].Text)) > 80)
                    //{
                    //    e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
                    //}
                    //else
                    //{
                    //    e.Row.Cells[10].BackColor = System.Drawing.Color.Transparent;
                    //}
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        }

        protected void GV_Task_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<TicketingToolbo> objPIDashBoardLst = (List<TicketingToolbo>)Session["VSTaskData"];
            bool objSortOrder = (bool)Session["bSortedOrder"];
            switch (e.SortExpression.ToString().Trim())
            {
                case "TICKETID":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((long.Parse(objBo1.TICKETID.ToString())).CompareTo(long.Parse(objBo2.TICKETID.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((long.Parse(objBo2.TICKETID.ToString())).CompareTo(long.Parse(objBo1.TICKETID.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "TASKID":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((long.Parse(objBo1.TASKLINEID.ToString())).CompareTo(long.Parse(objBo2.TASKLINEID.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((long.Parse(objBo2.TASKLINEID.ToString())).CompareTo(long.Parse(objBo1.TASKLINEID.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "TASKTITLE":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.TASKTITLE.CompareTo(objBo2.TASKTITLE)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.TASKTITLE.CompareTo(objBo1.TASKTITLE)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "TASKAGENT":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.TASKAGENT.CompareTo(objBo2.TASKAGENT)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.TASKAGENT.CompareTo(objBo1.TASKAGENT)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "TASKACTUALAGENT":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.TASKACTUALAGENT.CompareTo(objBo2.TASKACTUALAGENT)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.TASKACTUALAGENT.CompareTo(objBo1.TASKACTUALAGENT)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "TASKCREATED_BY":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.TASKCREATED_BY.CompareTo(objBo2.TASKCREATED_BY)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.TASKCREATED_BY.CompareTo(objBo1.TASKCREATED_BY)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "StatusTxt":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.StatusTxt.CompareTo(objBo2.StatusTxt)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.StatusTxt.CompareTo(objBo1.StatusTxt)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "TASKCREATED_ON":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (DateTime.Parse(objBo1.TASKCREATED_ON.ToString()).CompareTo(DateTime.Parse(objBo2.TASKCREATED_ON.ToString()))); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (DateTime.Parse(objBo2.TASKCREATED_ON.ToString()).CompareTo(DateTime.Parse(objBo1.TASKCREATED_ON.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "TASKMODIFIED_BY":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (objBo1.TASKMODIFIED_BY.CompareTo(objBo2.TASKMODIFIED_BY)); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (objBo2.TASKMODIFIED_BY.CompareTo(objBo1.TASKMODIFIED_BY)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "LASTMODIFIED_ON":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return (DateTime.Parse(objBo1.LASTMODIFIED_ON.ToString()).CompareTo(DateTime.Parse(objBo2.LASTMODIFIED_ON.ToString()))); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return (DateTime.Parse(objBo2.LASTMODIFIED_ON.ToString()).CompareTo(DateTime.Parse(objBo1.LASTMODIFIED_ON.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
            }
            GV_Task.DataSource = objPIDashBoardLst;
            GV_Task.DataBind();
            Session.Add("VSTaskData", objPIDashBoardLst);
        }

        protected void ddlPagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PendingPageIndex"] = 1;
            Session["pagesize"] = ddlPagesize.SelectedValue.ToString().Trim();
            DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
            DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
            LoadAllTickets(int.Parse(DDLStatusSearch.SelectedValue.ToString()), 1, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);

        }

        protected void ddlPaseSizeTAsk_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllTask(int.Parse(DDLStatusSearch.SelectedValue.ToString()), 1);
        }

        protected void DDLCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["DDLCustomerList"] = DDLCustomerList.SelectedValue.ToString().Trim();
            DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
            DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
            LoadAllTickets(int.Parse(Session["DDLStatusSearch"].ToString().Trim()), 1, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
        }

        protected void TxtFromDate_TextChanged(object sender, EventArgs e)
        {
            ViewState["SLATmPerID"] = "0";
            DateTime DtFrom = new DateTime(1900, 01, 01);
            if (DateTime.TryParse(TxtFromDate.Text, out DtFrom))
            {
                Session["TxtFromDate"] = DtFrom.ToString().Trim();
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
                Fromdate = DtFrom;
                //Todate = DtTo;
                LoadAllTickets(int.Parse(Session["DDLStatusSearch"].ToString().Trim()), 1, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
            }
            else
            {
                TxtFromDate.Text = "";
            }
        }

        protected void TxtToDate_TextChanged(object sender, EventArgs e)
        {
            ViewState["SLATmPerID"] = "0";
            DateTime DtTo = new DateTime(1900, 01, 01);
            if (DateTime.TryParse(TxtToDate.Text, out DtTo))
            {
                Session["TxtToDate"] = TxtToDate.Text.ToString().Trim();
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
                LoadAllTickets(int.Parse(Session["DDLStatusSearch"].ToString().Trim()), 1, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
            }
            else
            {
                TxtToDate.Text = "";
            }
        }

        protected void TxtTID_TextChanged(object sender, EventArgs e)
        {
            ViewState["SLATmPerID"] = "0";
            DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
            DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
            LoadAllTickets(int.Parse(Session["DDLStatusSearch"].ToString().Trim()), 1, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            DDLStatusSearch.SelectedValue = "1";
            int parsedValue;
            if (!int.TryParse(HttpContext.Current.User.Identity.Name, out parsedValue) && (User.Identity.Name != "cssteam"))
            {
                DDLCustomerList.SelectedValue = User.Identity.Name;
                Session["DDLCustomerList"] = User.Identity.Name;
            }
            else
            {
                DDLCustomerList.SelectedValue = "4";
                Session["DDLCustomerList"] = "4";
            }

            TxtFromDate.Text = "";
            TxtToDate.Text = "";
            Session["DDLStatusSearch"] = "1";
            ViewState["SLATmPerID"] = "0";
            Session["TxtFromDate"] = "";
            Session["TxtToDate"] = "";
            TxtTID.Text = "";
            DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text.ToString().Trim()) ? "01/01/0001" : TxtFromDate.Text.ToString().Trim());
            DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text.ToString().Trim()) ? "01/01/0001" : TxtToDate.Text.ToString().Trim());
            LoadAllTickets(int.Parse(Session["DDLStatusSearch"].ToString().Trim()), 1, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
        }

        protected void BTnBackdashboard_Click(object sender, EventArgs e)
        {
            // Session["chartNo"] = Session["TktStutsID"] = "0";
            Server.Transfer("~/UI/Ticketing Tool/TicketingTDashBoard.aspx");
        }

        public void LoadSLA()
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.LoadSLADtls();

                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    MsgCls("No Records Found !", LblTicket, System.Drawing.Color.Red);
                    GV_SLADetls.Visible = true;
                    GV_SLADetls.DataSource = null;
                    GV_SLADetls.DataBind();
                    return;
                }
                else
                {
                    MsgCls("", LblTicket, System.Drawing.Color.Transparent);

                    GV_SLADetls.DataSource = null;
                    GV_SLADetls.DataBind();
                    GV_SLADetls.DataSource = TicketingboList;
                    GV_SLADetls.SelectedIndex = -1;
                    GV_SLADetls.DataBind();

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void GV_Tickets_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                long TicketId = int.Parse(GV_Tickets.DataKeys[GV_Tickets.SelectedIndex]["TID"].ToString().Trim());
                Session["TransferdTicketId"] = int.Parse(GV_Tickets.DataKeys[GV_Tickets.SelectedIndex]["TID"].ToString().Trim());
                Session["TransferdValue"] = "1";
                Response.Redirect("~/UI/Ticketing Tool/CreateIssueTicket.aspx");
                //Response.Redirect("~/UI/Ticketing Tool/CreateIssueTicket.aspx?Type=" + TicketId + "&Value=" + 1);                                  
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void GV_Task_SelectedIndexChanged(object sender, EventArgs e)
        {
            long TicketId = int.Parse(GV_Task.DataKeys[GV_Task.SelectedIndex]["TICKETID"].ToString().Trim());
            long TaskId = int.Parse(GV_Task.DataKeys[GV_Task.SelectedIndex]["TASKID"].ToString().Trim());
            Session["TransferdTicketId"] = int.Parse(GV_Task.DataKeys[GV_Task.SelectedIndex]["TICKETID"].ToString().Trim());
            Session["TransferdValue"] = "2";
            Response.Redirect("~/UI/Ticketing Tool/CreateIssueTicket.aspx");
        }

        protected void GV_Task_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GV_Task, "Select$" + e.Row.RowIndex);
            }
        }

        protected void lnkBtnSLA1_Click(object sender, EventArgs e)
        {

            ViewState["SLATmPerID"] = "40";
            LoadSLAonLegendClick();
        }

        protected void lnkBtnSLA2_Click(object sender, EventArgs e)
        {

            ViewState["SLATmPerID"] = "80";
            LoadSLAonLegendClick();
        }

        protected void lnkBtnSLA3_Click(object sender, EventArgs e)
        {
            ViewState["SLATmPerID"] = "100";
            LoadSLAonLegendClick();
        }

        protected void lnkBtnSLA4_Click(object sender, EventArgs e)
        {
            ViewState["SLATmPerID"] = "101";
            LoadSLAonLegendClick();
        }

        protected void LoadSLAonLegendClick()
        {
            try
            {
                Session["chartNo"] = "0";
                Session["TktStutsID"] = "0";
                DDLStatusSearch.SelectedValue = "2";
                Session["DDLStatusSearch"] = DDLStatusSearch.SelectedValue.ToString().Trim();
                TxtFromDate.Text = "";
                TxtToDate.Text = "";
                TxtTID.Text = "";
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtFromDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtFromDate"].ToString().Trim());
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(Session["TxtToDate"].ToString().Trim()) ? "01/01/0001" : Session["TxtToDate"].ToString().Trim());
                LoadAllTickets(int.Parse(Session["DDLStatusSearch"].ToString().Trim()), 1, Session["DDLCustomerList"].ToString().Trim(), Fromdate, Todate);
            }
            catch (Exception exx) { }
        }
    }
}
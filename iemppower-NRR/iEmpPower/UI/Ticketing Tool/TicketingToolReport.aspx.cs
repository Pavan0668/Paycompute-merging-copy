
using iTextSharp.text.html.simpleparser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Ticketing_Tool;

namespace iEmpPower.UI.Ticketing_Tool
{
    public partial class TicketingToolReport : System.Web.UI.Page
    {
        protected int PendingPageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSLA();
                Session["PendingPageIndex"] = "0";
                Session["DDLStatusSearch"] = "0";
                LoadStatus();
                LoadCustomerSrch();
                int result;
                if (int.TryParse(User.Identity.Name.ToString().Trim(), out result)) // Agent or associate managers
                {
                    DDLCustomerList.SelectedValue = "4";
                }
                else
                {
                    if (User.Identity.Name == "cssteam")
                    {
                        DDLCustomerList.SelectedValue = "4";
                    }
                    else
                    {
                        DDLCustomerList.SelectedValue = User.Identity.Name;
                    }
                }
                //DDLCustomerList.SelectedValue = "4";
                DDLStatusSearch.SelectedValue = "9";
                TxtFromDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).ToString("dd/MM/yyyy");
                TxtToDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("dd/MM/yyyy");

                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                //LoadAllTickets(1);
                LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, 1);
                //LoadAllTask(1);
                //divAccordion.Visible = false;
                //FormTicket.Visible = false;
                //viewcheck.Value = "NO";
                //this.RegisterPostBackControl();
            }


            int parsedValue;
            if (!int.TryParse(HttpContext.Current.User.Identity.Name, out parsedValue))
            {
                if (User.Identity.Name.ToString().ToLower() != "cssteam")
                {
                    GV_Tickets.Columns[11].Visible = false;
                }
            }
            this.RegisterPostBackControl();
        }
        private void RegisterPostBackControl()
        {
            foreach (GridViewRow row in GV_Tickets.Rows)
            {
                LinkButton lnkFull = row.FindControl("LbtnFeedbackView") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);
            }
        }

        protected void LoadStatus()
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_StatusSearch_Rprt(User.Identity.Name);
            DDLStatusSearch.DataSource = objLst;
            DDLStatusSearch.DataTextField = "RptStatusTxt";
            DDLStatusSearch.DataValueField = "RptStatusID";
            DDLStatusSearch.DataBind();
            //DDLStatusSearch.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
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

        public void LoadAllTickets(int flag, string custtype, int StatusTyp, DateTime Fromdate, DateTime Todate, int pageindex)
        {
            try
            {
                int? RecordCnt = 0;
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
                TicketingObjBo.PageSize = int.Parse(ddlPagesize.SelectedValue.ToString().Trim());
                TicketingObjBo.PageIndex = PendingPageIndex;
                TicketingboList = TicketingObjBl.Load_AllTickets_Reports(flag, User.Identity.Name, custtype, StatusTyp, Fromdate, Todate, TicketingObjBo, Convert.ToInt32(ViewState["SLATmPerID"]), ref RecordCnt);

                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    MsgCls("No Records Found !", LblTicket, System.Drawing.Color.Red);
                    GV_Tickets.Visible = true;
                    GV_Tickets.DataSource = null;
                    GV_Tickets.DataBind();
                    ticketdiv.Visible = true;
                    TicketExports.Visible = false;
                    pnlgrid.Visible = false;
                    return;
                }
                else
                {
                    MsgCls("", LblTicket, System.Drawing.Color.Transparent);
                    ticketdiv.Visible = true;
                    GV_Tickets.Visible = true;
                    pnlgrid.Visible = true;
                    GV_Tickets.DataSource = null;
                    GV_Tickets.DataBind();
                    GV_Tickets.DataSource = TicketingboList;
                    GV_Tickets.SelectedIndex = -1;
                    GV_Tickets.DataBind();
                    TicketExports.Visible = true;
                }
                Session.Add("VSTicketsData", TicketingboList);

                if (flag == 1)
                {
                    PopulatePendingPager(TicketingboList.Count > 0 ? int.Parse(RecordCnt.ToString()) : 0, PendingPageIndex);
                }
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
                int pagerSpan = int.Parse(ddlPagesize.SelectedValue.ToString().Trim());

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
                int pageIndex = PendingPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, PendingPageIndex);

                this.RegisterPostBackControl();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        #endregion

        //public void LoadAllTask(int StatusTyp)
        //{
        //    try
        //    {
        //        TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //        TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

        //        TicketingboList = TicketingObjBl.Load_AllTask(User.Identity.Name, StatusTyp);


        //        if (TicketingboList == null || TicketingboList.Count == 0)
        //        {
        //            MsgCls("No Records Found !", LblTask, System.Drawing.Color.Red);
        //            GV_Task.Visible = false;
        //            taskdiv.Visible = true;
        //            GV_Task.DataSource = null;
        //            GV_Task.DataBind();

        //            TicketingToolbo TicketingObjBo2 = new TicketingToolbo();
        //            TicketingToolbl TicketingObjBl2 = new TicketingToolbl();
        //            msassignedtomebo objPIDashBoardBo2 = new msassignedtomebo();
        //            msassignedtomebl objPIDashBoardB2 = new msassignedtomebl();
        //            objPIDashBoardBo2.PERNR = HttpContext.Current.User.Identity.Name;
        //            msassignedtomecollectionbo objPIDashBoardLst2 = objPIDashBoardB2.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo2);
        //            string Status = "";
        //            TicketingObjBl2.CheckIfclients(1, User.Identity.Name, ref Status);
        //            if (Status == "True") // client
        //            {

        //                GV_Task.Visible = false;
        //                taskdiv.Visible = false;
        //                Taskfs.Visible = false;
        //            }
        //            else if (User.Identity.Name == "cssteam")
        //            {
        //                GV_Task.Visible = false;
        //                taskdiv.Visible = false;
        //                Taskfs.Visible = false;
        //            }

        //            else if (objPIDashBoardLst2.Count > 0)
        //            {
        //                GV_Task.Visible = false;
        //                taskdiv.Visible = true;
        //                Taskfs.Visible = true;
        //            }
        //            else
        //            {
        //                GV_Task.Visible = false;
        //                taskdiv.Visible = true;
        //                Taskfs.Visible = true;
        //            }


        //            return;
        //        }
        //        else
        //        {
        //            MsgCls("", LblTask, System.Drawing.Color.Transparent);
        //            GV_Task.Visible = true;
        //            taskdiv.Visible = true;
        //            GV_Task.DataSource = null;
        //            GV_Task.DataBind();
        //            GV_Task.DataSource = TicketingboList;
        //            GV_Task.SelectedIndex = -1;
        //            GV_Task.DataBind();
        //        }

        //        TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
        //        TicketingToolbl TicketingObjBl1 = new TicketingToolbl();
        //        msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
        //        msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
        //        objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
        //        msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
        //        string Status1 = "";
        //        TicketingObjBl1.CheckIfclients(1, User.Identity.Name, ref Status1);
        //        if (Status1 == "True") // client
        //        {
        //            GV_Task.Visible = false;
        //            taskdiv.Visible = false;
        //            Taskfs.Visible = false;
        //        }
        //        else if (User.Identity.Name == "cssteam")
        //        {
        //            GV_Task.Visible = false;
        //            taskdiv.Visible = false;
        //            Taskfs.Visible = false;
        //        }

        //        else if (objPIDashBoardLst.Count > 0)
        //        {
        //            GV_Task.Visible = true;
        //            taskdiv.Visible = true;
        //            Taskfs.Visible = true;
        //        }
        //        else
        //        {
        //            GV_Task.Visible = true;
        //            taskdiv.Visible = true;
        //            Taskfs.Visible = true;
        //        }

        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

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


        //protected void GV_Tickets_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        switch (e.CommandName.ToUpper())
        //        {
        //            case "VIEW":
        //                viewcheck.Value = "YES";
        //                int row = int.Parse(e.CommandArgument.ToString());
        //                ViewState["rowid"] = row;
        //                long TicketId = int.Parse(GV_Tickets.DataKeys[int.Parse(e.CommandArgument.ToString())]["TID"].ToString().Trim());
        //                BindTicketData(TicketId);
        //                LoadTicketAttachments(TicketId);
        //                LoadTicketStatus(TicketId);
        //                LoadTicketComments(TicketId);
        //                divAccordion.Visible = true;
        //                FormTicket.Visible = true;
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

        protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["SLATmPerID"] = "0";
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, PendingPageIndex);
                this.RegisterPostBackControl();
                //LoadAllTickets(int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()));
                //LoadAllTickets(1);
                //divAccordion.Visible = false;
                //FormTicket.Visible = false;
                //viewcheck.Value = "NO";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        //protected void GV_Task_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        switch (e.CommandName.ToUpper())
        //        {
        //            case "VIEW":
        //                long TicketId = int.Parse(GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TICKETID"].ToString().Trim());
        //                long TaskId = int.Parse(GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TASKID"].ToString().Trim());


        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}


        //public void BindTicketData(long TicketId)
        //{
        //    try
        //    {
        //        TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //        TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

        //        TicketingboList = TicketingObjBl.Load_TicketForReport(TicketId);


        //        if (TicketingboList == null || TicketingboList.Count == 0)
        //        {
        //            MsgCls("No Records Found !", LblMsg, System.Drawing.Color.Red);
        //            FormTicket.Visible = false;
        //            FormTicket.DataSource = null;
        //            FormTicket.DataBind();
        //            return;
        //        }
        //        else
        //        {
        //            MsgCls("", LblMsg, System.Drawing.Color.Transparent);
        //            FormTicket.Visible = true;
        //            FormTicket.DataSource = null;
        //            FormTicket.DataBind();
        //            FormTicket.DataSource = TicketingboList;
        //            FormTicket.DataBind();

        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

        //public void LoadTicketComments(long TicketID)
        //{
        //    try
        //    {
        //        TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //        TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

        //        TicketingboList = TicketingObjBl.Load_Ticket_Comments(TicketID, User.Identity.Name);


        //        if (TicketingboList == null || TicketingboList.Count == 0)
        //        {
        //            grdTicketsComments.Visible = false;
        //            grdTicketsComments.DataSource = null;
        //            grdTicketsComments.DataBind();
        //            return;
        //        }
        //        else
        //        {
        //            grdTicketsComments.Visible = true;
        //            grdTicketsComments.DataSource = null;
        //            grdTicketsComments.DataBind();
        //            grdTicketsComments.DataSource = TicketingboList;
        //            grdTicketsComments.DataBind();
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

        //public void LoadTicketStatus(long TicketID)
        //{
        //    try
        //    {
        //        TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //        TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

        //        TicketingboList = TicketingObjBl.Load_Ticket_Status(TicketID);


        //        if (TicketingboList == null || TicketingboList.Count == 0)
        //        {
        //            GrdTicketStatus.Visible = false;
        //            GrdTicketStatus.DataSource = null;
        //            GrdTicketStatus.DataBind();
        //            return;
        //        }
        //        else
        //        {
        //            GrdTicketStatus.Visible = true;
        //            GrdTicketStatus.DataSource = null;
        //            GrdTicketStatus.DataBind();
        //            GrdTicketStatus.DataSource = TicketingboList;
        //            GrdTicketStatus.DataBind();
        //        }


        //        string Status1 = "";
        //        TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status1);

        //        if (Status1 == "True") // client
        //        {
        //            AccordionPane3.Visible = false;
        //        }
        //        else
        //        {
        //            AccordionPane3.Visible = true;
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

        //public void LoadTicketAttachments(long TicketID)
        //{
        //    try
        //    {
        //        TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //        TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

        //        TicketingboList = TicketingObjBl.Load_Ticket_Attachments(TicketID, User.Identity.Name);


        //        if (TicketingboList == null || TicketingboList.Count == 0)
        //        {
        //            GrdTicketsAttachments.Visible = false;
        //            GrdTicketsAttachments.DataSource = null;
        //            GrdTicketsAttachments.DataBind();
        //            return;
        //        }
        //        else
        //        {
        //            GrdTicketsAttachments.Visible = true;
        //            GrdTicketsAttachments.DataSource = null;
        //            GrdTicketsAttachments.DataBind();
        //            GrdTicketsAttachments.DataSource = TicketingboList;
        //            GrdTicketsAttachments.DataBind();
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

        //protected void GrdTicketsAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        switch (e.CommandName.ToUpper())
        //        {
        //            case "DOWNLOAD":
        //                string filePath = e.CommandArgument.ToString();
        //                Response.ContentType = "application/octet-stream";
        //                //Response.ContentType = ContentType;
        //                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        //                Response.WriteFile(filePath);
        //                Response.End();
        //                break;
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        //}

        protected void BtnExporttoXl_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void BtnExporttoPDF_Click(object sender, EventArgs e)
        {
            ExportGridToPDF();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        //protected void ExportToExcel()
        //{

        //    if (viewcheck.Value == "YES")
        //    {

        //        int rowid = int.Parse(ViewState["rowid"].ToString());

        //        System.IO.StringWriter sw = new System.IO.StringWriter();
        //        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        //        // Render grid view control.
        //        htw.WriteBreak();

        //        string colHeads = "Ticket Issue Details";
        //        htw.WriteEncodedText(colHeads);

        //        FormTicket.RenderControl(htw);
        //        htw.WriteBreak();

        //        colHeads = "Comments History";
        //        htw.WriteEncodedText(colHeads);
        //        grdTicketsComments.RenderControl(htw);
        //        htw.WriteBreak();
        //        colHeads = "Attachement History";
        //        htw.WriteEncodedText(colHeads);
        //        GrdTicketsAttachments.RenderControl(htw);
        //        htw.WriteBreak();
        //        colHeads = "Status History";
        //        htw.WriteEncodedText(colHeads);
        //        GrdTicketStatus.RenderControl(htw);
        //        htw.WriteBreak();


        //        // Write the rendered content to a file.
        //        string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();

        //        renderedGridView += sw.ToString() + "<br/>";
        //        Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_TravelClaim.xls");
        //        Response.ContentType = "Application/vnd.ms-excel";
        //        Response.Write(renderedGridView);
        //        Response.End();
        //    }


        //    else
        //    {
        //        System.IO.StringWriter sw = new System.IO.StringWriter();
        //        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        //        // Render grid view control.
        //        htw.WriteBreak();
        //        string colHeads = "Tickets Details";
        //        htw.WriteEncodedText(colHeads);               
        //        GV_Tickets.RenderControl(htw);
        //        htw.WriteBreak();
        //        string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
        //        renderedGridView += sw.ToString() + "<br/>";
        //        Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_TravelClaim.xls");
        //        Response.ContentType = "Application/vnd.ms-excel";
        //        Response.Write(renderedGridView);
        //        Response.End();

        //    }
        //}

        //private void ExportGridToPDF()
        //{

        //    if (viewcheck.Value == "YES")
        //    {


        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_TravelClaim.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //        StringWriter s_tw = new StringWriter();
        //        HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
        //        h_textw.AddStyleAttribute("font-size", "8pt");
        //        h_textw.AddStyleAttribute("color", "Black");


        //        string colHeads = "Summary_Report";
        //        h_textw.WriteEncodedText(colHeads);
        //        h_textw.WriteBreak();

        //        // h_textw.WriteEncodedText(colHeads);
        //        h_textw.WriteBreak();
        //        colHeads = "Ticket Details";
        //        h_textw.WriteEncodedText(colHeads);
        //        h_textw.WriteBreak();
        //        FormTicket.RenderControl(h_textw);
        //        h_textw.WriteBreak();

        //        colHeads = "Comments Details";
        //        h_textw.WriteEncodedText(colHeads);
        //        h_textw.WriteBreak();
        //        grdTicketsComments.RenderControl(h_textw);
        //        h_textw.WriteBreak();

        //        colHeads = "Attachements Details";
        //        h_textw.WriteEncodedText(colHeads);
        //        h_textw.WriteBreak();
        //        GrdTicketsAttachments.RenderControl(h_textw);
        //        h_textw.WriteBreak();

        //        colHeads = "Status Details";
        //        h_textw.WriteEncodedText(colHeads);
        //        h_textw.WriteBreak();
        //        GrdTicketStatus.RenderControl(h_textw);
        //        h_textw.WriteBreak();



        //        //  Document doc = new Document(PageSize.A2, 1f, 1f, 1f, 0.0f);
        //        iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

        //        //  Document doc = new Document();
        //        iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
        //        doc.Open();
        //        StringReader s_tr = new StringReader(s_tw.ToString());
        //        HTMLWorker html_worker = new HTMLWorker(doc);
        //        html_worker.Parse(s_tr);
        //        doc.Close();
        //        Response.Write(doc);
        //    }

        //    else
        //    {

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_TravelClaim.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //        StringWriter s_tw = new StringWriter();
        //        HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
        //        h_textw.AddStyleAttribute("font-size", "8pt");
        //        h_textw.AddStyleAttribute("color", "Black");

        //        ////gvVehicle.RenderControl(h_textw);//Name of the Panel

        //        string colHeads = "Summary_Report";
        //        h_textw.WriteEncodedText(colHeads);
        //        h_textw.WriteBreak();
        //        colHeads = "ticket Details";

        //        h_textw.WriteEncodedText(colHeads);
        //        h_textw.WriteBreak();
        //        GV_Tickets.RenderControl(h_textw);
        //        h_textw.WriteBreak();
        //        //  Document doc = new Document(PageSize.A2, 1f, 1f, 1f, 0.0f);
        //        iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

        //        //  Document doc = new Document();
        //        iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
        //        doc.Open();
        //        StringReader s_tr = new StringReader(s_tw.ToString());
        //        HTMLWorker html_worker = new HTMLWorker(doc);
        //        html_worker.Parse(s_tr);
        //        doc.Close();
        //        Response.Write(doc);
        //    }
        //}

        protected void ExportToExcel()
        {


            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            // Render grid view control.
            htw.WriteBreak();
            string colHeads = "Tickets Details";
            htw.WriteEncodedText(colHeads);
            //GV_Tickets.AllowPaging = false;
            GV_Tickets.AllowSorting = false;
            GV_Tickets.Columns[22].Visible = false;

            DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
            DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
            LoadAllTickets(2, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, PendingPageIndex);
            GV_Tickets.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
            GV_Tickets.RenderControl(htw);
            //GV_Tickets.AllowPaging = true;
            GV_Tickets.Columns[22].Visible = true;
            GV_Tickets.AllowSorting = true;
            htw.WriteBreak();
            string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
            renderedGridView += sw.ToString() + "<br/>";
            Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_TicketingReport.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();
            Clear();

        }

        private void ExportGridToPDF()
        {


            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_TicketingReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter s_tw = new StringWriter();
            HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
            h_textw.AddStyleAttribute("font-size", "8pt");
            h_textw.AddStyleAttribute("color", "Black");

            ////gvVehicle.RenderControl(h_textw);//Name of the Panel

            string colHeads = "Summary_Report";
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            colHeads = "ticket Details";

            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            //GV_Tickets.AllowPaging = false;
            GV_Tickets.AllowSorting = false;
            GV_Tickets.Columns[21].Visible = false;
            DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
            DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
            LoadAllTickets(2, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, PendingPageIndex);

            GV_Tickets.RenderControl(h_textw);
            //GV_Tickets.AllowPaging = true;
            GV_Tickets.AllowSorting = true;
            GV_Tickets.Columns[21].Visible = true;
            h_textw.WriteBreak();
            //  Document doc = new Document(PageSize.A2, 1f, 1f, 1f, 0.0f);
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

            //  Document doc = new Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();
            StringReader s_tr = new StringReader(s_tw.ToString());
            HTMLWorker html_worker = new HTMLWorker(doc);
            html_worker.Parse(s_tr);
            doc.Close();
            Response.Write(doc);
            Clear();
        }


        protected void GV_Tickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string Status = GV_Tickets.DataKeys[e.Row.RowIndex].Values[2].ToString();
                    string CATEGORY = GV_Tickets.DataKeys[e.Row.RowIndex].Values[3].ToString().Trim();
                    if (Status.ToString().Trim() != "13" && Status.ToString().Trim() != "14" && Status.ToString().Trim() != "4" && Status.ToString().Trim() != "12" && Status.ToString().Trim() != "11")
                    {
                        if (Convert.ToDouble(GV_Tickets.DataKeys[e.Row.RowIndex].Values[4].ToString().Trim()) > 0)
                        {
                            TimeSpan sla_time = TimeSpan.FromSeconds(Convert.ToDouble(GV_Tickets.DataKeys[e.Row.RowIndex].Values[4].ToString().Trim()));
                            //sla_Brchtime = sla_time.ToString(@"hh\:mm\:ss\:fff");
                            e.Row.Cells[20].Text = sla_time.ToString();
                            e.Row.Cells[19].Text = "Yes";
                        }
                        else
                        {
                            e.Row.Cells[20].Text = "-";
                            e.Row.Cells[19].Text = "No";
                        }
                    }
                    else
                    {
                        e.Row.Cells[20].Text = "-";
                        e.Row.Cells[19].Text = "-";
                    }
                    if (CATEGORY.ToString().Trim() == "2" || CATEGORY.ToString().Trim() == "3")
                    {
                        if (Status.ToString().Trim() != "1" && Status.ToString().Trim() != "14" && Status.ToString().Trim() != "4" && Status.ToString().Trim() != "7" && Status.ToString().Trim() != "8" && Status.ToString().Trim() != "9" && Status.ToString().Trim() != "11" && Status.ToString().Trim() != "12" && Status.ToString().Trim() != "13")
                        {
                            //Get the value of column from the DataKeys using the RowIndex.     
                            string group = GV_Tickets.DataKeys[e.Row.RowIndex].Values[1].ToString();

                            if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 0) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 40)
                            {
                                e.Row.Cells[15].BackColor = System.Drawing.Color.Green;
                                e.Row.Cells[15].ForeColor = System.Drawing.Color.White;
                            }
                            else if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 40) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 80)
                            {
                                e.Row.Cells[15].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBF00");
                                e.Row.Cells[15].ForeColor = System.Drawing.Color.Black;

                            }
                            else if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 80) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) < 100)
                            {
                                e.Row.Cells[15].BackColor = System.Drawing.ColorTranslator.FromHtml("#f75f0b");
                                e.Row.Cells[15].ForeColor = System.Drawing.Color.Black;

                            }
                            //else if ((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 80)
                            //{
                            //    e.Row.Cells[11].BackColor = System.Drawing.ColorTranslator.FromHtml("#f75f0b");   
                            //    e.Row.Cells[11].ForeColor = System.Drawing.Color.White;
                            //}
                            else if ((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) >= 100)
                            {
                                e.Row.Cells[15].BackColor = System.Drawing.ColorTranslator.FromHtml("#ea0d14");
                                e.Row.Cells[15].ForeColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                e.Row.Cells[15].BackColor = System.Drawing.Color.White;
                                e.Row.Cells[15].ForeColor = System.Drawing.Color.Black;
                            }

                        }
                        else
                        {
                            e.Row.Cells[15].BackColor = System.Drawing.Color.White;
                            e.Row.Cells[15].ForeColor = System.Drawing.Color.Black;
                        }
                    }
                    else
                    {
                        e.Row.Cells[15].BackColor = System.Drawing.Color.White;
                        e.Row.Cells[15].ForeColor = System.Drawing.Color.Black;
                    }
                }

                int result;
                if ((!int.TryParse(User.Identity.Name.ToString().Trim(), out result)) && (User.Identity.Name != "cssteam"))
                {
                    e.Row.Cells[12].Visible = false;
                    e.Row.Cells[13].Visible = false;
                }
                //else if (User.Identity.Name == "cssteam")
                //{
                //    e.Row.Cells[11].Visible = true;
                //    e.Row.Cells[12].Visible = true;
                //}
                else
                {
                    e.Row.Cells[12].Visible = true;
                    e.Row.Cells[13].Visible = true;
                }
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{


                //    //Get the value of column from the DataKeys using the RowIndex.

                //    string group = GV_Tickets.DataKeys[e.Row.RowIndex].Values[1].ToString();

                //    if ((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 40)
                //    {
                //        e.Row.Cells[10].BackColor = System.Drawing.Color.Green;
                //    }
                //    else if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 40) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 80)
                //    {
                //        e.Row.Cells[10].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBF00");

                //    }
                //    else if ((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 80)
                //    {
                //        e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
                //    }
                //    else
                //    {
                //        e.Row.Cells[10].BackColor = System.Drawing.Color.Transparent;
                //    }

                //}
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
        //                    e.Row.Cells[11].BackColor = System.Drawing.Color.Green;
        //                }
        //                else if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 40) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 80)
        //                {
        //                    e.Row.Cells[11].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBF00");

        //                }
        //                else if ((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 80)
        //                {
        //                    e.Row.Cells[11].BackColor = System.Drawing.Color.Red;
        //                }
        //                else
        //                {
        //                    e.Row.Cells[11].BackColor = System.Drawing.Color.Transparent;
        //                }

        //            }
        //        }
        //        //if (e.Row.RowType == DataControlRowType.DataRow)
        //        //{


        //        //    //Get the value of column from the DataKeys using the RowIndex.

        //        //    string group = GV_Tickets.DataKeys[e.Row.RowIndex].Values[1].ToString();

        //        //    if ((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 40)
        //        //    {
        //        //        e.Row.Cells[10].BackColor = System.Drawing.Color.Green;
        //        //    }
        //        //    else if (((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 40) && (string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) <= 80)
        //        //    {
        //        //        e.Row.Cells[10].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBF00");

        //        //    }
        //        //    else if ((string.IsNullOrEmpty(group.ToString()) ? 0 : decimal.Parse(group.ToString())) > 80)
        //        //    {
        //        //        e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
        //        //    }
        //        //    else
        //        //    {
        //        //        e.Row.Cells[10].BackColor = System.Drawing.Color.Transparent;
        //        //    }

        //        //}
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        //}

        protected void DDLCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, PendingPageIndex);
                this.RegisterPostBackControl();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void TxtFromDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["SLATmPerID"] = "0";
                DateTime DtFrm = new DateTime(1900, 01, 01);
                if (DateTime.TryParse(TxtFromDate.Text, out DtFrm))
                {
                    TxtToDate.Text = DtFrm.ToString().Trim();
                    DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(DtFrm.ToString()) ? "01/01/0001" : DtFrm.ToString());
                    DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                    LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, PendingPageIndex);
                }
                else
                {
                    TxtToDate.Text = TxtFromDate.Text = "";
                }
                this.RegisterPostBackControl();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void TxtToDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["SLATmPerID"] = "0";
                DateTime DtFrm = new DateTime(1900, 01, 01);
                DateTime DtTo = new DateTime(1900, 01, 01);
                if (DateTime.TryParse(TxtToDate.Text, out DtTo))
                {
                    if (DateTime.TryParse(TxtFromDate.Text, out DtFrm))
                    {
                        DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(DtFrm.ToString()) ? "01/01/0001" : DtFrm.ToString());
                        DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(DtTo.ToString()) ? "01/01/0001" : DtTo.ToString());
                        LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, PendingPageIndex);
                    }
                    else
                    {
                        TxtToDate.Text = TxtFromDate.Text = "";
                    }
                }

                else
                {
                    TxtToDate.Text = TxtFromDate.Text = "";
                }
                this.RegisterPostBackControl();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        //protected void GV_Tickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        int pageindex = e.NewPageIndex;
        //        GV_Tickets.PageIndex = e.NewPageIndex;
        //        DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
        //        DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
        //        //LoadAllTickets(DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate);
        //        //LoadAllTask(int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()));
        //        List<TicketingToolbo> objPIDashBoardLst = (List<TicketingToolbo>)Session["VSTicketsData"];
        //        GV_Tickets.DataSource = objPIDashBoardLst;
        //        GV_Tickets.DataBind();
        //        GV_Tickets.SelectedIndex = -1;
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
                case "Plndhrs":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((decimal.Parse(objBo1.Plndhrs.ToString())).CompareTo(decimal.Parse(objBo2.Plndhrs.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((decimal.Parse(objBo2.Plndhrs.ToString())).CompareTo(decimal.Parse(objBo1.Plndhrs.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "Actulhrs":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((decimal.Parse(objBo1.Actualhrs.ToString())).CompareTo(decimal.Parse(objBo2.Actualhrs.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((decimal.Parse(objBo2.Actualhrs.ToString())).CompareTo(decimal.Parse(objBo1.Actualhrs.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "Module":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((decimal.Parse(objBo1.ISSCATEGORYCSSTxt.ToString())).CompareTo(decimal.Parse(objBo2.ISSCATEGORYCSSTxt.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((decimal.Parse(objBo2.ISSCATEGORYCSSTxt.ToString())).CompareTo(decimal.Parse(objBo1.ISSCATEGORYCSSTxt.ToString()))); });
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
            this.RegisterPostBackControl();
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {

            try
            {
                Clear();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void Clear()
        {
            try
            {

                int result;
                if (int.TryParse(User.Identity.Name.ToString().Trim(), out result)) // Agent or associate managers
                {
                    DDLCustomerList.SelectedValue = "4";
                }
                else
                {
                    if (User.Identity.Name == "cssteam")
                    {
                        DDLCustomerList.SelectedValue = "4";
                    }
                    else
                    {
                        DDLCustomerList.SelectedValue = User.Identity.Name;
                    }
                }
                ViewState["SLATmPerID"] = "0";
                //DDLCustomerList.SelectedValue = "4";
                DDLStatusSearch.SelectedValue = "9";
                TxtFromDate.Text = "";
                TxtToDate.Text = "";
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
                //LoadAllTickets(1);
                PendingPageIndex = 1;
                LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, PendingPageIndex);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void ddlPagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(TxtFromDate.Text) ? "01/01/0001" : TxtFromDate.Text);
            DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(TxtToDate.Text) ? "01/01/0001" : TxtToDate.Text);
            PendingPageIndex = 1;
            LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, PendingPageIndex);
            this.RegisterPostBackControl();
        }

        protected void BTnBackdashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Ticketing Tool/TicketingTDashBoard.aspx");
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

        protected void GV_Tickets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

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

        protected void BtnFeedbackReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Ticketing Tool/TicketCustomerFeedback.aspx");
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
                DDLStatusSearch.SelectedValue = "9";
                Session["DDLStatusSearch"] = DDLStatusSearch.SelectedValue.ToString().Trim();
                TxtFromDate.Text = "";
                TxtToDate.Text = "";
                DateTime DtFrm = new DateTime(0001, 01, 01);
                DateTime DtTo = new DateTime(0001, 01, 01);
                DateTime Fromdate = DateTime.Parse(string.IsNullOrEmpty(DtFrm.ToString()) ? "01/01/0001" : DtFrm.ToString());
                DateTime Todate = DateTime.Parse(string.IsNullOrEmpty(DtTo.ToString()) ? "01/01/0001" : DtTo.ToString());
                LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), int.Parse(DDLStatusSearch.SelectedValue.ToString().Trim()), Fromdate, Todate, PendingPageIndex);
            }
            catch (Exception exx) { }
        }
    }
}
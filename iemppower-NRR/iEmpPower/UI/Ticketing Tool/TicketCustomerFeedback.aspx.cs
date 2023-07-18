using iTextSharp.text.html.simpleparser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.Ticketing_Tool
{
    public partial class TicketCustomerFeedback : System.Web.UI.Page
    {
        protected int PendingPageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadEmployees();
                LoadCustomer();
               
                int result;
                if (int.TryParse(User.Identity.Name.ToString().Trim(), out result)) // Agent or associate managers
                {
                    DDEmpPnl.Visible = true;
                    DDLEmployee.SelectedValue = User.Identity.Name;
                    DDLCustomerList.SelectedValue = "4";
                }
                else
                {
                    if (User.Identity.Name == "cssteam")
                    {
                        DDEmpPnl.Visible = true;
                        DDLEmployee.SelectedValue = "ALL";
                        DDLCustomerList.SelectedValue = "4";
                    }
                    else
                    {
                        DDLCustomerList.SelectedValue = User.Identity.Name;
                        DDLEmployee.SelectedValue = "ALL";
                        DDEmpPnl.Visible = false;
                    }
                }
                LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), DDLEmployee.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void LoadEmployees()
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_EmployeeForRating(User.Identity.Name);
            DDLEmployee.DataSource = objLst;
            DDLEmployee.DataTextField = "EMPLOYEE_NAME";
            DDLEmployee.DataValueField = "EMPLOYEE_NO";
            DDLEmployee.DataBind();
            //DDLStatusSearch.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
        }

        protected void LoadCustomer()
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_customerForRating(User.Identity.Name);
            DDLCustomerList.DataSource = objLst;
            DDLCustomerList.DataTextField = "customertxt";
            DDLCustomerList.DataValueField = "customerId";
            DDLCustomerList.DataBind();
            //DDLCustomerList.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
        }

        public void LoadAllTickets(int flag, string custtype, string EmpTyp, int pageindex)
        {
            try
            {
                int? RecordCnt = 0;
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
                TicketingObjBo.PageSize = int.Parse(ddlPagesize.SelectedValue.ToString().Trim());
                TicketingObjBo.PageIndex = PendingPageIndex;
                TicketingboList = TicketingObjBl.Load_CustomerFeedbackRating(flag, User.Identity.Name, custtype, EmpTyp, string.IsNullOrEmpty(TxtTID.Text.Trim()) ? 0 : long.Parse(TxtTID.Text.Trim()), TicketingObjBo, ref RecordCnt);

                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    MsgCls("No Records Found !", LblTicket, System.Drawing.Color.Red);
                    GV_TicketsRating.Visible = true;
                    GV_TicketsRating.DataSource = null;
                    GV_TicketsRating.DataBind();
                    ticketdiv.Visible = true;
                    TicketExports.Visible = false;
                    pnlgrid.Visible = false;
                    return;
                }
                else
                {
                    MsgCls("", LblTicket, System.Drawing.Color.Transparent);
                    ticketdiv.Visible = true;
                    GV_TicketsRating.Visible = true;
                    pnlgrid.Visible = true;
                    GV_TicketsRating.DataSource = null;
                    GV_TicketsRating.DataBind();
                    GV_TicketsRating.DataSource = TicketingboList;
                    GV_TicketsRating.SelectedIndex = -1;
                    GV_TicketsRating.DataBind();
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
                LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), DDLEmployee.SelectedValue.ToString().Trim(), PendingPageIndex);
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

        protected void GV_TicketsRating_Sorting(object sender, GridViewSortEventArgs e)
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
                case "Q1":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((decimal.Parse(objBo1.Q1.ToString())).CompareTo(decimal.Parse(objBo2.Q1.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((decimal.Parse(objBo2.Q1.ToString())).CompareTo(decimal.Parse(objBo1.Q1.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "Q2":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((decimal.Parse(objBo1.Q2.ToString())).CompareTo(decimal.Parse(objBo2.Q2.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((decimal.Parse(objBo2.Q2.ToString())).CompareTo(decimal.Parse(objBo1.Q2.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "Q3":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((decimal.Parse(objBo1.Q3.ToString())).CompareTo(decimal.Parse(objBo2.Q3.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((decimal.Parse(objBo2.Q3.ToString())).CompareTo(decimal.Parse(objBo1.Q3.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "Q4":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((decimal.Parse(objBo1.Q4.ToString())).CompareTo(decimal.Parse(objBo2.Q4.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((decimal.Parse(objBo2.Q4.ToString())).CompareTo(decimal.Parse(objBo1.Q4.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "Q5":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((decimal.Parse(objBo1.Q5.ToString())).CompareTo(decimal.Parse(objBo2.Q5.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((decimal.Parse(objBo2.Q5.ToString())).CompareTo(decimal.Parse(objBo1.Q5.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "TRATING":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((decimal.Parse(objBo1.SUMRATING.ToString())).CompareTo(decimal.Parse(objBo2.SUMRATING.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((decimal.Parse(objBo2.SUMRATING.ToString())).CompareTo(decimal.Parse(objBo1.SUMRATING.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                    case "AVGATING":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                            { return ((decimal.Parse(objBo1.TRAVERAGE.ToString())).CompareTo(decimal.Parse(objBo2.TRAVERAGE.ToString()))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(TicketingToolbo objBo1, TicketingToolbo objBo2)
                        { return ((decimal.Parse(objBo2.TRAVERAGE.ToString())).CompareTo(decimal.Parse(objBo1.TRAVERAGE.ToString()))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                    
                        
            }
            GV_TicketsRating.DataSource = objPIDashBoardLst;
            GV_TicketsRating.DataBind();
            Session.Add("VSTicketsData", objPIDashBoardLst);
        }

        protected void DDLEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), DDLEmployee.SelectedValue.ToString().Trim(), PendingPageIndex);
        }

        protected void DDLCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), DDLEmployee.SelectedValue.ToString().Trim(), PendingPageIndex);
        }

        protected void TxtTID_TextChanged(object sender, EventArgs e)
        {
            LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), DDLEmployee.SelectedValue.ToString().Trim(), PendingPageIndex);
        }

        protected void BTnBackdashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Ticketing Tool/TicketingToolReport.aspx");
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
                    DDEmpPnl.Visible = true;
                    DDLEmployee.SelectedValue = User.Identity.Name;
                    DDLCustomerList.SelectedValue = "4";
                }
                else
                {
                    if (User.Identity.Name == "cssteam")
                    {
                        DDEmpPnl.Visible = true;
                        DDLEmployee.SelectedValue = "ALL";
                        DDLCustomerList.SelectedValue = "4";
                    }
                    else
                    {
                        DDLCustomerList.SelectedValue = User.Identity.Name;
                        DDLEmployee.SelectedValue = "ALL";
                        DDEmpPnl.Visible = false;
                       
                    }
                }
                PendingPageIndex = 1;
                TxtTID.Text = "";
                LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), DDLEmployee.SelectedValue.ToString().Trim(), 1);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void ddlPagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllTickets(1, DDLCustomerList.SelectedValue.ToString().Trim(), DDLEmployee.SelectedValue.ToString().Trim(), PendingPageIndex);
        }

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

        protected void ExportToExcel()
        {
            string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            // Render grid view control.
            htw.WriteBreak();
            //string colHeads = "Tickets Details";
            //htw.WriteEncodedText(colHeads);
            GV_TicketsRating.AllowPaging = false;
            GV_TicketsRating.AllowSorting = false;
            LoadAllTickets(2, DDLCustomerList.SelectedValue.ToString().Trim(), DDLEmployee.SelectedValue.ToString().Trim(), PendingPageIndex);
            GV_TicketsRating.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
            GV_TicketsRating.RenderControl(htw);
            GV_TicketsRating.AllowPaging = true;
            GV_TicketsRating.AllowSorting = true;
            htw.WriteBreak();
            string renderedGridView = "Ticketing Customer Feedback Report" + "<br>"; //+ sw.ToString();
            renderedGridView += sw.ToString() + "<br/>";
            Response.AppendHeader("content-disposition", "attachment; filename=" + "TicketingCustomerFeedback" + date1 + "_Report.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();
            Clear();
        }

        private void ExportGridToPDF()
        {
            string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "TicketingCustomerFeedback" + date1 + "_Report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter s_tw = new StringWriter();
            HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
            h_textw.AddStyleAttribute("font-size", "8pt");
            h_textw.AddStyleAttribute("color", "Black");
            string colHeads = "Ticketing Customer Feedback Report";
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            h_textw.WriteBreak();
            //GV_Tickets.AllowPaging = false;
            GV_TicketsRating.AllowSorting = false;
            GV_TicketsRating.AllowPaging = false;
            LoadAllTickets(2, DDLCustomerList.SelectedValue.ToString().Trim(), DDLEmployee.SelectedValue.ToString().Trim(), PendingPageIndex);
            GV_TicketsRating.RenderControl(h_textw);
            //GV_Tickets.AllowPaging = true;
            GV_TicketsRating.AllowSorting = true;
            GV_TicketsRating.AllowPaging = true;
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
    }
}
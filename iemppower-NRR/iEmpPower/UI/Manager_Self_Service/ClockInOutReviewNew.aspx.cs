using iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.Manager_Self_Service
{
    public partial class ClockInOutReviewNew : System.Web.UI.Page
    {
        protected int PageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    LoadDDLEmpList();
                    PageLoadEvents();
                }
                RV_TxtToDate.MaximumValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-1).ToString("dd/MM/yyyy");
                RV_TxtToDate.ErrorMessage = string.Format("To date should be less than or equal to {0}.", RV_TxtToDate.MaximumValue);

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }


        protected void LoadDDLEmpList()
        {
            try
            {
                msassignedtomebo ObjAssginTMBo = new msassignedtomebo();
                ObjAssginTMBo.PERNR = User.Identity.Name;
                ObjAssginTMBo.COMMENTS = Session["CompCode"].ToString();
                msassignedtomebl ObjAssginTMBl = new msassignedtomebl();
                msassignedtomecollectionbo ObjAssginTMList = new msassignedtomecollectionbo();
                ObjAssginTMList = ObjAssginTMBl.Get_Sub_Employees_Of_Manager_Details(ObjAssginTMBo);

                DDLEmpNames.DataSource = ObjAssginTMList;
                DDLEmpNames.DataTextField = "ENAME";
                DDLEmpNames.DataValueField = "PERNR";
                DDLEmpNames.DataBind();
                DDLEmpNames.Items.Insert(0, new ListItem("- SELECT - ", "0"));
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        #region Page Load Events
        private void PageLoadEvents()
        {
            try
            {
                ClearFields();
                DDLEmpNames.SelectedValue = User.Identity.Name;
                //CCD_DDLEmpNames.SelectedValue = User.Identity.Name;
                TxtFrmDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).ToString("dd/MM/yyyy");
                TxtToDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("dd/MM/yyyy");
                Bind_GV_ClockInClockOut(1);
                Bind_GV_PunchInOutReport(1);
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #endregion

        #region User Defined Method

        protected void GVNodata(GridView Gv, DataTable Dt)
        {
            try
            {
                //Add blank row to the the resultset
                Dt.Rows.Add(Dt.NewRow());
                Gv.DataSource = Dt;
                Gv.DataBind();
                //hide empty row
                Gv.Rows[0].Visible = false;
                Gv.Rows[0].Controls.Clear();

            }
            catch (Exception Ex)
            { throw Ex; }
        }

        private void MsgCls(string LblTxt, Label Lbl, Color Clr)
        {
            try
            {
                Lbl.Text = string.Empty;
                Lbl.Text = LblTxt;
                Lbl.ForeColor = Clr;
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #region Clear Fields

        private void ClearFields()
        {
            try
            {
                DDLEmpNames.SelectedValue = "0";
                //CCD_DDLEmpNames.SelectedValue = "0";
                TxtFrmDate.Text = string.Empty;
                TxtToDate.Text = string.Empty;
                MsgCls("", LblMsg, Color.Transparent);
                GV_ClockInClockOut.DataSource = null;
                GV_ClockInClockOut.DataBind();
                DivPaging.Visible = false;
                MsgCls("", LblPunchInOutReport, Color.Transparent);
                PunchInOutReport.DataSource = null;
                PunchInOutReport.DataBind();
                DivPunchInOutReport.Visible = false;
                DivReport.Visible = false;
                btnExportToExcel.Visible = false;
                BtnExptoExclPunchInOutReport.Visible = false;


            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #endregion
        #endregion

        #region Bind GV_Error
        private void Bind_GV_ClockInClockOut(int PageIndex)
        {
            try
            {

                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                if (DateTime.TryParse(TxtFrmDate.Text, out FromDate))
                {
                    if (DateTime.TryParse(TxtToDate.Text, out ToDate))
                    {
                        leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                        leaverequestcollectionbo ObjClkInClkOutLst = new leaverequestcollectionbo();

                        ObjClkInClkOutLst = ObjLeaveRequestBl.Get_Emp_ClockIn_ClockOut(DDLEmpNames.SelectedValue, User.Identity.Name, FromDate, ToDate, PageIndex, 10, 1, Session["CompCode"].ToString().Trim());
                        if (ObjClkInClkOutLst.Count > 0)
                        {

                            MsgCls("", LblMsg, Color.Transparent);
                            GV_ClockInClockOut.DataSource = ObjClkInClkOutLst;
                            GV_ClockInClockOut.DataBind();
                            DivPaging.Visible = true;
                            PopulatePager(int.Parse(ObjClkInClkOutLst[0].PIPORECCOUNT.ToString()), PageIndex);
                            btnExportToExcel.Visible = true;
                            punchinmaingv();

                        }
                        else
                        {
                            MsgCls("No Records Found!", LblMsg, Color.Red);
                            GV_ClockInClockOut.DataSource = null;
                            GV_ClockInClockOut.DataBind();
                            DivPaging.Visible = false;
                            btnExportToExcel.Visible = false;
                        }

                        string frow = "", lrow = "";  ////Row count

                        foreach (GridViewRow row in GV_ClockInClockOut.Rows)
                        {
                            for (int i = 0; i < GV_ClockInClockOut.Rows.Count; i++)
                            {
                                Label lblRowNumber = (Label)GV_ClockInClockOut.Rows[i].FindControl("lblRowNumber");
                                if (i == 0)
                                {
                                    frow = GV_ClockInClockOut.Rows[i].Cells[0].Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                                }
                                if (i == GV_ClockInClockOut.Rows.Count - 1)
                                {
                                    lrow = GV_ClockInClockOut.Rows[i].Cells[0].Text;
                                }
                            }
                        }
                        divpendingrecordcount.InnerHtml = "Showing " + frow + " to " + lrow + " of " + ObjClkInClkOutLst[0].PIPORECCOUNT.ToString() + " entries";
                        DivPaging.Visible = GV_ClockInClockOut.Rows.Count > 0 ? true : false;


                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }
        private void Bind_GV_PunchInOutReport(int PageIndex)
        {
            try
            {
                decimal? WeekoffCOunt = 0;
                decimal? TotalDAys = 0;
                decimal? TotalHolidays = 0; ;
                decimal? TotalWrkingDAys = 0;
                decimal? TotalHours = 0;
                decimal? HrsDay = 0;
                int? RecCount = 0;
                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                if (DateTime.TryParse(TxtFrmDate.Text, out FromDate))
                {
                    if (DateTime.TryParse(TxtToDate.Text, out ToDate))
                    {

                        leaverequestbl ObjLeaveRequestBlRpt = new leaverequestbl();
                        leaverequestcollectionbo ObjClkInClkOutLstRpt = new leaverequestcollectionbo();
                        ObjClkInClkOutLstRpt = ObjLeaveRequestBlRpt.Get_PunchInOutReport(Session["CompCode"].ToString(),DDLEmpNames.SelectedValue, User.Identity.Name, FromDate, ToDate, PageIndex, 10, 1, ref WeekoffCOunt, ref TotalDAys, ref TotalHolidays, ref TotalWrkingDAys, ref TotalHours, ref HrsDay, ref RecCount);
                        if (ObjClkInClkOutLstRpt.Count > 0)
                        {
                            MsgCls("", LblPunchInOutReport, Color.Transparent);
                            LblTWeekOffs.Text = WeekoffCOunt.ToString();
                            LblTotalDays.Text = TotalDAys.ToString();
                            LblTHolidays.Text = TotalHolidays.ToString();
                            LblTWorkingDays.Text = TotalWrkingDAys.ToString();
                            LblTotalHours.Text = TotalHours.ToString();
                            LblTHoursDay.Text = HrsDay.ToString();

                            PunchInOutReport.DataSource = ObjClkInClkOutLstRpt;
                            PunchInOutReport.DataBind();
                            DivReport.Visible = true;
                            DivPunchInOutReport.Visible = true;
                            PopulatePagerPunchInOutReport(int.Parse(RecCount.ToString().Trim()), PageIndex);
                            BtnExptoExclPunchInOutReport.Visible = true;
                        }
                        else
                        {

                            PunchInOutReport.DataSource = null;
                            PunchInOutReport.DataBind();
                            DivReport.Visible = false;
                            DivPunchInOutReport.Visible = false;
                            BtnExptoExclPunchInOutReport.Visible = false;
                        }
                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        #endregion

        #region Submit Button click event

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                if (!string.IsNullOrEmpty(TxtFrmDate.Text) && !string.IsNullOrEmpty(TxtToDate.Text))
                {
                    if (DDLEmpNames.SelectedValue != "0")
                    {
                        if (DateTime.TryParse(TxtFrmDate.Text, out FromDate))
                        {
                            if (DateTime.TryParse(TxtToDate.Text, out ToDate))
                            {
                                DetaildRpt.Visible = true;
                                SummaryRpt.Visible = true;
                                Bind_GV_ClockInClockOut(1);
                                Bind_GV_PunchInOutReport(1);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #endregion

        #region Populate pager
        private void PopulatePager(int RecordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 10;

                //Calculate the Start and End Index of pages to be displayed.
                double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal("10"));
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
                RptrClkInClkOutPager.DataSource = pages;
                RptrClkInClkOutPager.DataBind();

                GV_ClockInClockOut.FooterRow.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;<b>Page " + currentPage + " of " + pageCount + "<b/>";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        protected void Page_Changed(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = PageIndex = int.Parse((sender as LinkButton).CommandArgument);
                this.Bind_GV_ClockInClockOut(pageIndex);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }
        #endregion

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            DetaildRpt.Visible = false;
            SummaryRpt.Visible = false;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {

            ExportToExcel();

        }

        protected void ExportToExcel()
        {

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


            htw.WriteBreak();
            string colHeads = "Punch IN - Punch Out Details";
            htw.WriteEncodedText(colHeads);
            GV_ClockInClockOut.GridLines = GridLines.Both;
            GV_ClockInClockOut.Columns[9].Visible = false;
            EXPORT_Bind_GV_ClockInClockOut();
            GV_ClockInClockOut.RenderControl(htw);

            htw.WriteBreak();
            GV_ClockInClockOut.Columns[9].Visible = true;
            // Write the rendered content to a file.
            string renderedGridView = "<br/>"; renderedGridView += sw.ToString() + "<br/>";
            Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_PunchInOutDetails.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();

            Bind_GV_ClockInClockOut(1);


        }

        private void EXPORT_Bind_GV_ClockInClockOut()
        {
            try
            {
                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                if (DateTime.TryParse(TxtFrmDate.Text, out FromDate))
                {
                    if (DateTime.TryParse(TxtToDate.Text, out ToDate))
                    {
                        leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                        leaverequestcollectionbo ObjClkInClkOutLst = new leaverequestcollectionbo();

                        ObjClkInClkOutLst = ObjLeaveRequestBl.Get_Emp_ClockIn_ClockOut(DDLEmpNames.SelectedValue, User.Identity.Name, FromDate, ToDate, 0, 0, 2, Session["CompCode"].ToString().Trim());
                        if (ObjClkInClkOutLst.Count > 0)
                        {
                            MsgCls("", LblMsg, Color.Transparent);
                            GV_ClockInClockOut.DataSource = ObjClkInClkOutLst;
                            GV_ClockInClockOut.DataBind();
                            punchinmaingv();
                        }
                        else
                        {
                            MsgCls("No Records Found!", LblMsg, Color.Red);
                            GV_ClockInClockOut.DataSource = null;
                            GV_ClockInClockOut.DataBind();
                        }

                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        private void EXPORT_Bind_GV_PunchInOutReport()
        {
            try
            {
                decimal? WeekoffCOunt = 0;
                decimal? TotalDAys = 0;
                decimal? TotalHolidays = 0; ;
                decimal? TotalWrkingDAys = 0;
                decimal? TotalHours = 0;
                decimal? HrsDay = 0;
                int? RecCount = 0;
                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                if (DateTime.TryParse(TxtFrmDate.Text, out FromDate))
                {
                    if (DateTime.TryParse(TxtToDate.Text, out ToDate))
                    {

                        leaverequestbl ObjLeaveRequestBlRpt = new leaverequestbl();
                        leaverequestcollectionbo ObjClkInClkOutLstRpt = new leaverequestcollectionbo();
                        ObjClkInClkOutLstRpt = ObjLeaveRequestBlRpt.Get_PunchInOutReport(Session["CompCode"].ToString(), DDLEmpNames.SelectedValue, User.Identity.Name, FromDate, ToDate, 0, 0, 2, ref WeekoffCOunt, ref TotalDAys, ref TotalHolidays, ref TotalWrkingDAys, ref TotalHours, ref HrsDay, ref RecCount);
                        if (ObjClkInClkOutLstRpt.Count > 0)
                        {
                            MsgCls("", LblPunchInOutReport, Color.Transparent);
                            LblTWeekOffs.Text = WeekoffCOunt.ToString();
                            LblTotalDays.Text = TotalDAys.ToString();
                            LblTHolidays.Text = TotalHolidays.ToString();
                            LblTWorkingDays.Text = TotalWrkingDAys.ToString();
                            LblTotalHours.Text = TotalHours.ToString();
                            LblTHoursDay.Text = HrsDay.ToString();

                            PunchInOutReport.DataSource = ObjClkInClkOutLstRpt;
                            PunchInOutReport.DataBind();
                        }
                        else
                        {
                            MsgCls("No Records Found!", LblPunchInOutReport, Color.Red);
                            PunchInOutReport.DataSource = null;
                            PunchInOutReport.DataBind();
                        }
                        PunchInOutReport.GridLines = GridLines.Both;
                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }


        private void PopulatePagerPunchInOutReport(int RecordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 10;

                //Calculate the Start and End Index of pages to be displayed.
                double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal("10"));
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
                RepeaterPunchInOutReport.DataSource = pages;
                RepeaterPunchInOutReport.DataBind();

                PunchInOutReport.FooterRow.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;<b>Page " + currentPage + " of " + pageCount + "<b/>";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void lnkPagePunchInOutReport_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = PageIndex = int.Parse((sender as LinkButton).CommandArgument);
                this.Bind_GV_PunchInOutReport(pageIndex);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void BtnExptoExclPunchInOutReport_Click(object sender, EventArgs e)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


            htw.WriteBreak();
            //string colHeads = "Punch IN - Punch Out Report";
            //htw.WriteEncodedText(colHeads);
            //htw.WriteBreak();


            EXPORT_Bind_GV_PunchInOutReport();
            PunchInOutReport.GridLines = GridLines.Both;
            PunchInOutReport.RenderControl(htw);


            string renderedGridView = "Punch IN - Punch OUT Report" + "<br/>"; //+ sw.ToString();
            htw.WriteBreak();
            renderedGridView += "<table><tr><td align=left>Total Days</td><td align=left>:</td><td align=left>" + LblTotalDays.Text.ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Total Week Offs</td><td align=left>:</td><td align=left>" + LblTWeekOffs.Text.ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Total Holidays</td><td align=left>:</td><td align=left>" + LblTHolidays.Text.ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Total Working Days</td><td align=left>:</td><td align=left>" + LblTWorkingDays.Text.ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Hours/Day</td><td align=left>:</td><td align=left>" + LblTHoursDay.Text.ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Total Hours</td><td align=left>:</td><td align=left>" + LblTotalHours.Text.ToString() + "</td></tr></table>";
            htw.WriteBreak();
            // Write the rendered content to a file.
            renderedGridView += sw.ToString() + "<br/>";

            Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_PunchInOutReport.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();

            Bind_GV_PunchInOutReport(1);


        }

        public void punchinmaingv()
        {
            try
            {
                foreach (GridViewRow row in GV_ClockInClockOut.Rows)
                {
                    LinkButton view = (LinkButton)row.FindControl("lnkviewindetail");
                    bool? count = Convert.ToBoolean(GV_ClockInClockOut.DataKeys[row.RowIndex].Values[2].ToString());

                    if (count == true)
                    {
                        view.Visible = true;
                    }
                    else
                    {
                        view.Visible = false;
                    }


                }
            }

            catch (Exception ex)
            {

            }
        }

        protected void GV_ClockInClockOut_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)e.Row.FindControl("lblID");
                    string ccode = Session["CompCode"].ToString();
                    string emplogin = lbl.Text;
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    e.Row.Cells[1].Text = emplogin.Trim().ToUpper();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_ClockInClockOut_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = GV_ClockInClockOut.Rows[rowIndex];
                LinkButton lnklock = (LinkButton)grdRow.FindControl("lnktemplock");
                switch (e.CommandName.ToUpper())
                {

                    case "VIEW":
                        modal1.Show();
                        string pernr = GV_ClockInClockOut.DataKeys[grdRow.RowIndex].Values["PERNR"].ToString();
                        DateTime FromDate = Convert.ToDateTime(GV_ClockInClockOut.DataKeys[grdRow.RowIndex].Values["DATES"].ToString());
                        DateTime ToDate = Convert.ToDateTime(GV_ClockInClockOut.DataKeys[grdRow.RowIndex].Values["DATES"].ToString());
                        DateTime rowdate = Convert.ToDateTime(GV_ClockInClockOut.DataKeys[grdRow.RowIndex].Values["DATES"].ToString());

                        leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                        leaverequestcollectionbo ObjClkInClkOutLst = new leaverequestcollectionbo();

                        ObjClkInClkOutLst = ObjLeaveRequestBl.Get_Emp_ClockIn_ClockOutfull(pernr.ToString().Trim(), FromDate, ToDate, rowdate, 1, Session["CompCode"].ToString());
                        GV_punchinfulldetails.DataSource = ObjClkInClkOutLst;
                        GV_punchinfulldetails.DataBind();
                        Bindtotalhrs();

                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }


        protected void GV_punchinfulldetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                
                foreach (GridViewRow gvr in GV_punchinfulldetails.Rows)
                {
                    Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                    TimeSpan total = TimeSpan.Parse("00:00");
                    Label lblbrk = (Label)e.Row.FindControl("lblbrk");
                    total = total + TimeSpan.Parse(lblbrk.Text.ToString().Trim());
                    lblTotal.Text = total.ToString();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void Bindtotalhrs()
        {
            try
            {

                TimeSpan total = TimeSpan.Parse("00:00");
                for (int i = 0; i < GV_punchinfulldetails.Rows.Count; i++)
                {

                    Label hrs = (Label)GV_punchinfulldetails.Rows[i].FindControl("lblbrk");
                    TimeSpan calc = TimeSpan.Parse(hrs.Text == "" ? "00:00" : hrs.Text);
                    total = total + calc;

                }
                Label thrs = ((Label)GV_punchinfulldetails.FooterRow.FindControl("lblTotal"));
                thrs.Text = total.ToString() == "0.00" ? "" : total.ToString();

                ViewState["Totalscore"] = thrs.Text;
            }
            catch (Exception ex)
            {

            }
        }

    }
}
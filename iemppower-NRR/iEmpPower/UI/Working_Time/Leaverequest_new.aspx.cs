using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerMaster;

namespace iEmpPower.UI.Working_Time
{
    public partial class Leaverequest_new : System.Web.UI.Page
    {
        leaverequestbo objBo = new leaverequestbo();
        leaverequestbl objBl = new leaverequestbl();
        leaverequestcollectionbo objLst = new leaverequestcollectionbo();
        DateTime dtTodate = new DateTime();
        DateTime dtFromdate = new DateTime();
        DateTime DtMillanium = new DateTime(2000, 01, 01);

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                MinMaxDate();
                DDLApprover.Attributes.Add("readonly", "readonly");
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        void Page_PreRender(object obj, EventArgs e)
        {
            ViewState["update"] = Session["update"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    string id = string.Empty;
                    if (Request.QueryString["id"] != null)
                    {
                        id = Request.QueryString["id"];
                    }

                    if (id == "Emplv")
                    {
                        HideTabs();
                        view2.Visible = true;
                        Tab2.CssClass = "nav-link active p-2";
                        CE_TxtLeaveOverView.SelectedDate =DateTime.Parse(Session["_MainSearchValue"].ToString().Trim());
                        TxtLeaveOverView.Text = CE_TxtLeaveOverView.SelectedDate.ToString().Trim();
                        TxtLeaveOverView.Focus();
                        leave_mainsrch();
                        DefaultRunFunction();

                        load_leavType();
                        Load_GV_SelectedDateLeaveView();
                        DropDownReqTyp.SelectedValue = "1";
                        load_leavSubType(DropDownReqTyp.SelectedValue);
                        load_Approver();
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        if (Session["isDR"].ToString().Trim() == "True")
                        {
                            Pnl_Approver.Visible = false;
                            divApprover.Visible = false;
                        }
                        else
                        {
                            Pnl_Approver.Visible = true;
                            divApprover.Visible = true;
                        }
                        PageLoadEventsformainsrch();

                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        DDLLeaveQuotaYear.SelectedValue = DateTime.Today.Year.ToString();
                        Bind_GV_LeaveQuota("D");
                        DropDownReqTyp.Focus();
                        Pnl_2HalfDay.Visible = false;
                        TxtFromDate.Attributes.Add("readonly", "readonly");
                        TxtToDate.Attributes.Add("readonly", "readonly");
                        TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        TxtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        CDD_DDLEmpList.SelectedValue = User.Identity.Name;
                        DDLMonth.SelectedValue = int.Parse(DateTime.Now.ToString("MM")).ToString();
                        DDLYears.SelectedValue = DateTime.Now.ToString("yyyy");
                        CCD_DDLYears.SelectedValue = DateTime.Now.ToString("yyyy");
                    }

                    else if (id == "Empattd")
                    {
                        HideTabs();
                        view2.Visible = true;
                        Tab2.CssClass = "nav-link active p-2";
                        CE_TxtLeaveOverView.SelectedDate =DateTime.Parse(Session["_MainSearchValue"].ToString().Trim());
                        TxtLeaveOverView.Text = CE_TxtLeaveOverView.SelectedDate.ToString().Trim();
                        TxtLeaveOverView.Focus();
                        attd_mainsrch();
                        DefaultRunFunction();

                        load_leavType();
                        Load_GV_SelectedDateLeaveView();
                        DropDownReqTyp.SelectedValue = "1";
                        load_leavSubType(DropDownReqTyp.SelectedValue);
                        load_Approver();
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        if (Session["isDR"].ToString().Trim() == "True")
                        {
                            Pnl_Approver.Visible = false;
                            divApprover.Visible = false;
                        }
                        else
                        {
                            Pnl_Approver.Visible = true;
                            divApprover.Visible = true;
                        }
                        PageLoadEventsformainsrch();

                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        DDLLeaveQuotaYear.SelectedValue = DateTime.Today.Year.ToString();
                        Bind_GV_LeaveQuota("D");
                        DropDownReqTyp.Focus();
                        Pnl_2HalfDay.Visible = false;
                        TxtFromDate.Attributes.Add("readonly", "readonly");
                        TxtToDate.Attributes.Add("readonly", "readonly");
                        TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        TxtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        CDD_DDLEmpList.SelectedValue = User.Identity.Name;
                        DDLMonth.SelectedValue = int.Parse(DateTime.Now.ToString("MM")).ToString();
                        DDLYears.SelectedValue = DateTime.Now.ToString("yyyy");
                        CCD_DDLYears.SelectedValue = DateTime.Now.ToString("yyyy");
                    }
                    else
                    {

                        HideTabs();
                        view1.Visible = true;
                        Tab1.CssClass = "nav-link active p-2";
                        load_leavType();
                        Load_GV_SelectedDateLeaveView();
                        DropDownReqTyp.SelectedValue = "1";
                        load_leavSubType(DropDownReqTyp.SelectedValue);
                        load_Approver();
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        if (Session["isDR"].ToString().Trim() == "True")
                        {
                            Pnl_Approver.Visible = false;
                            divApprover.Visible = false;
                        }
                        else
                        {
                            Pnl_Approver.Visible = true;
                            divApprover.Visible = true;
                        }
                        PageLoadEvents();

                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        DDLLeaveQuotaYear.SelectedValue = DateTime.Today.Year.ToString();
                        Bind_GV_LeaveQuota("D");
                        DropDownReqTyp.Focus();
                        Pnl_2HalfDay.Visible = false;
                        TxtFromDate.Attributes.Add("readonly", "readonly");
                        TxtToDate.Attributes.Add("readonly", "readonly");
                        TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        TxtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        CDD_DDLEmpList.SelectedValue = User.Identity.Name;
                        DDLMonth.SelectedValue = int.Parse(DateTime.Now.ToString("MM")).ToString();
                        DDLYears.SelectedValue = DateTime.Now.ToString("yyyy");
                        CCD_DDLYears.SelectedValue = DateTime.Now.ToString("yyyy");
                    }
                }
                MinMaxDate();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        void BindYearDropdown()
        {
            try
            {
                for (int i = -1; i < 2; i++)
                {

                    string date = DateTime.Now.AddYears(i).ToString("yyyy");
                    ddlHolidayYear.Items.Add(date);
                    DDLLeaveQuotaYear.Items.Add(date);

                }
                ddlHolidayYear.SelectedValue = DateTime.Now.AddYears(0).ToString("yyyy");
                LoadHolidayCalendar();
                DDLLeaveQuotaYear.SelectedValue = DateTime.Now.AddYears(0).ToString("yyyy");
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void ddlHolidayYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadHolidayCalendar();
                LoadCalendarsWithLeaveMarkings(); 

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadHolidayCalendar()
        {
            try
            {
                MsgCls(string.Empty, Label2, System.Drawing.Color.Transparent);
                leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                List<leaverequestbo> LeaveReqboList1 = new List<leaverequestbo>();

                LeaveReqboList1 = ObjLeaveRequestBl.Load_HolidayCalendar(ddlHolidayYear.SelectedValue.ToString().Trim(), Session["CompCode"].ToString());
                Session.Add("IexpGrdInfo", LeaveReqboList1);

                if (LeaveReqboList1 == null || LeaveReqboList1.Count == 0)
                {
                    //MsgCls("No Records Found !", Label2, System.Drawing.Color.Red);
                    //Grd_HolidayCalendar.Visible = false;
                    Grd_HolidayCalendar.DataSource = null;
                    Grd_HolidayCalendar.DataBind();
                    return;
                }
                else
                {
                    MsgCls("", Label2, System.Drawing.Color.Transparent);
                    Grd_HolidayCalendar.Visible = true;
                    Grd_HolidayCalendar.DataSource = null;
                    Grd_HolidayCalendar.DataBind();
                    Grd_HolidayCalendar.DataSource = LeaveReqboList1;
                    Grd_HolidayCalendar.SelectedIndex = -1;
                    Grd_HolidayCalendar.DataBind();
                }

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in Grd_HolidayCalendar.Rows)
                {
                    for (int i = 0; i < Grd_HolidayCalendar.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)Grd_HolidayCalendar.Rows[i].FindControl("lblRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == Grd_HolidayCalendar.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divHCcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + LeaveReqboList1.Count + " entries";
                divHCcnt.Visible = Grd_HolidayCalendar.Rows.Count > 0 ? true : false;  
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void MinMaxDate()
        {
            try
            {
                //if ((DDL_LeaveType.SelectedValue.Trim() == "1051") || (DDL_LeaveType.SelectedValue.Trim() == "1050") || (DDL_LeaveType.SelectedValue.Trim() == "1030")
                //    || (DDL_LeaveType.SelectedValue.Trim() == "2100") || (DDL_LeaveType.SelectedValue.Trim() == "2101"))
                if ((DDL_LeaveType.SelectedValue.Trim() == "1030") || (DDL_LeaveType.SelectedValue.Trim() == "1040"))
                {
                    RV_TxtFromDate.Enabled = false;
                }


                else if ((DDL_LeaveType.SelectedValue.Trim() == "1017") || (DDL_LeaveType.SelectedValue.Trim() == "1080")
                     || (DDL_LeaveType.SelectedValue.Trim() == "1081") || (DDL_LeaveType.SelectedValue.Trim() == "1016")
                    || (DDL_LeaveType.SelectedValue.Trim() == "1051") || (DDL_LeaveType.SelectedValue.Trim() == "1050"))
                {

                    RV_TxtFromDate.Enabled = true;
                    RV_TxtFromDate.MinimumValue = new DateTime(DateTime.Today.Year, 1, 1).AddMonths(-1).ToString("dd/MM/yyyy");
                    RV_TxtFromDate.MaximumValue = new DateTime(DateTime.Today.Year, 12, 31).AddMonths(+1).ToString("dd/MM/yyyy");
                    RV_TxtFromDate.ErrorMessage = string.Format("Leave dates should be between {0} and {1}.", RV_TxtFromDate.MinimumValue, RV_TxtFromDate.MaximumValue);

                }

                else if ((DDL_LeaveType.SelectedValue.Trim() == "2100") || (DDL_LeaveType.SelectedValue.Trim() == "2101") || (DDL_LeaveType.SelectedValue.Trim() == "1021"))
                {

                    RV_TxtFromDate.Enabled = true;
                    RV_TxtFromDate.MinimumValue = new DateTime(DateTime.Today.Year, 1, 1).AddMonths(-1).ToString("dd/MM/yyyy");
                    RV_TxtFromDate.MaximumValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).ToString("dd/MM/yyyy");
                    RV_TxtFromDate.ErrorMessage = string.Format("Attendance dates should be between {0} and {1}.", RV_TxtFromDate.MinimumValue, RV_TxtFromDate.MaximumValue);

                }

                else if (string.IsNullOrEmpty(DDL_LeaveType.SelectedValue.Trim()))
                {
                    RV_TxtFromDate.Enabled = false;
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        private void PageLoadEventsformainsrch()
        {
            try
            {
                //------ LEAVE MARKINGS -----------
                BindCalendars("");
                LoadCalendarsWithLeaveMarkings();
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                BindYearDropdown();

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Transparent); }
        }

        #region Page Load Events
        private void PageLoadEvents()
        {
            try
            {
                //------ LEAVE MARKINGS -----------
                BindCalendars("");
                LoadCalendarsWithLeaveMarkings();
                Bind_GV_LeaveOverView(1);
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                BindYearDropdown();

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Transparent); }
        }
        #endregion

        #region User Defined Methods

        private void GVNodata(GridView GV, DataTable Dt)
        {
            try
            {
                Dt.Rows.Add(Dt.NewRow());
                GV.DataSource = Dt;
                GV.DataBind();
                GV.Rows[0].Visible = false;
                GV.Rows[0].Controls.Clear();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

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

        private void BindCalendars(string Type)
        {
            try
            {
                switch (Type)
                {
                    case "PREVIOUS":
                        DateTime DtmP = DateTime.Parse(DateTime.ParseExact(ViewState["Prev"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy"));
                        ViewState["Curr"] = DtmP.ToString("dd/MM/yyyy");
                        ViewState["Prev"] = DtmP.AddMonths(-1).ToString("dd/MM/yyyy");
                        ViewState["Next"] = DtmP.AddMonths(1).ToString("dd/MM/yyyy");
                        break;
                    case "NEXT":
                        DateTime DtmN = DateTime.Parse(DateTime.ParseExact(ViewState["Next"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy"));
                        ViewState["Curr"] = DtmN.ToString("dd/MM/yyyy");
                        ViewState["Prev"] = DtmN.AddMonths(-1).ToString("dd/MM/yyyy");
                        ViewState["Next"] = DtmN.AddMonths(1).ToString("dd/MM/yyyy");
                        break;
                    default:
                        DateTime DtmC = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                        ViewState["Curr"] = DtmC.ToString("dd/MM/yyyy");
                        ViewState["Prev"] = DtmC.AddMonths(-1).ToString("dd/MM/yyyy");
                        ViewState["Next"] = DtmC.AddMonths(1).ToString("dd/MM/yyyy");
                        break;
                }

                Cal_Current.TodaysDate = DateTime.Parse(ViewState["Curr"].ToString());
                Cal_Previous.TodaysDate = DateTime.Parse(ViewState["Prev"].ToString());
                Cal_Next.TodaysDate = DateTime.Parse(ViewState["Next"].ToString());
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        private void LoadCalendarsWithLeaveMarkings()
        {
            try
            {
                DateTime dtFormGivenDate = DateTime.Parse(ViewState["Prev"].ToString());
                DateTime dtToGivenDate = DateTime.Parse(ViewState["Next"].ToString());
                GetFirstInMonth(dtFormGivenDate, out dtFromdate);
                LastDayOfMonth(dtToGivenDate, out dtTodate);
                objBo.PERNR = User.Identity.Name;
                objBo.FROM_DATE = dtFromdate;
                objBo.TO_DATE = dtTodate;
                if (objBo != null) { objLst = objBl.Get_Calendar_Leave_Markings(objBo); }

                //Session.Add("objLst", objLst);
                //Cal_Previous_DayRender(null, null);
                //Cal_Current_DayRender(null, null);
                //Cal_Next_DayRender(null, null);

                Cal_Previous.DayRender += new DayRenderEventHandler(Cal_Previous_DayRender);
                Cal_Current.DayRender += new DayRenderEventHandler(Cal_Current_DayRender);
                Cal_Next.DayRender += new DayRenderEventHandler(Cal_Next_DayRender);
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        private DateTime GetFirstInMonth(DateTime dt, out DateTime dtFromDate)
        {
            DateTime dtRet = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
            dtFromDate = dtRet;
            return dtRet;
        }

        //This method will give last day of the selected calendar
        private DateTime LastDayOfMonth(DateTime dtGivendate, out DateTime dtLastDat)
        {
            DateTime dtRet = new DateTime(dtGivendate.Year, dtGivendate.Month, 1).AddMonths(1).AddDays(-1);
            dtLastDat = dtRet;
            return dtRet;
        }

        #endregion

        //-------------------------------------- CALENDAR EVENTS ------------------ BEGIN ----------------
        #region Calendar Next - Previous Button Click event
        protected void ImgBtnNext_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                BindCalendars("PREVIOUS");
                LoadCalendarsWithLeaveMarkings();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        protected void ImgBtnPrevious_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                BindCalendars("NEXT");
                LoadCalendarsWithLeaveMarkings();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region Calendar Day Render Events
        protected void Cal_Previous_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                

                if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    e.Cell.BackColor = Color.FromArgb(255, 255, 204);
                    //e.Cell.ForeColor = Color.Red;
                    e.Cell.Font.Bold = true;
                    // e.Cell.Controls.Clear();
                    e.Cell.BorderColor = Color.Black;
                    e.Cell.ForeColor = Color.Black;
                    if (!e.Day.IsOtherMonth)
                    {
                        //DateTime Dt = new DateTime(2000, 1, 1);
                        //TimeSpan Tm = e.Day.Date - Dt;
                        //e.Cell.Text = " <a href=\"javascript: return false;\">" + e.Day.Date.Date.ToString("dd")
                        //    //" <a href=\"javascript:__doPostBack('" + Cal_Previous.ClientID + "','" + Tm.Days + "')\">" + e.Day.Date.Date.ToString("dd")
                        //    + "<span><img class=\"callout\" src='../../Images/callout.gif' alt='' />"
                        //    + "Date : " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd") + "<br />"
                        //+ "Most Light-weight Tooltip<br />"
                        //+ "This is the easy-to-use Tooltip driven purely by CSS.</span></a>";
                        //e.Cell.Text = " <a href=\"javascript: return false;\">" + e.Day.Date.Date.ToString("dd") + "</a>";
                    }
                }
                e.Cell.ToolTip = "";
                if (e.Day.IsOtherMonth)
                {
                   // e.Cell.Controls.Clear();
                   // e.Cell.Text = "&nbsp;";
                }
                else
                {
                    DateTime dtGivenDate = DateTime.Parse(ViewState["Prev"].ToString());
                    GetFirstInMonth(dtGivenDate, out dtFromdate);
                    var vOnlyFirstCal = (from col in objLst
                                         where (col.DATUM) >= dtFromdate &&
                                         (col.DATUM) <= dtTodate
                                         select col);
                    foreach (leaverequestbo objBo in vOnlyFirstCal)
                    {

                        string PERNR = objBo.PERNR;
                        string ccode = Session["CompCode"].ToString();
                        string emplogin = PERNR.ToString();
                        int cnt = ccode.Length;
                        emplogin = emplogin.Substring(cnt);
                        string empid = emplogin.Trim();
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT")//// && objBo.HR_STATUS == null)
                        {
                            e.Cell.BackColor = Color.CornflowerBlue;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {
                                



                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT")// && objBo.HR_STATUS == "Review")
                        {
                            e.Cell.BackColor = Color.CornflowerBlue;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {

                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT")// && objBo.HR_STATUS == "Approved")
                        { e.Cell.BackColor = Color.CornflowerBlue; e.Cell.ForeColor = Color.White; }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")// && objBo.HR_STATUS == "Review")
                        { e.Cell.BackColor = Color.CornflowerBlue; e.Cell.ForeColor = Color.White; }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")//&& objBo.HR_STATUS == null)
                        {
                            e.Cell.BackColor = Color.Green;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "DELETION REQUESTED")
                        {
                            e.Cell.BackColor = Color.Gray;
                            e.Cell.ForeColor = Color.White;

                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")// && objBo.HR_STATUS == "Approved")
                        { e.Cell.BackColor = Color.FromArgb(35, 186, 53); e.Cell.ForeColor = Color.White; }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")////&& objBo.HR_STATUS == null)
                        {
                            e.Cell.ForeColor = Color.White;
                            e.Cell.BackColor = Color.FromArgb(35, 186, 53);
                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        protected void Cal_Current_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    e.Cell.BackColor = Color.FromArgb(255, 255, 204);
                    e.Cell.Font.Bold = true;
                    e.Cell.BorderColor = Color.Black;
                    e.Cell.ForeColor = Color.Black;
                    if (!e.Day.IsOtherMonth)
                    {
                    }
                }
                if (e.Day.IsOtherMonth)
                {
                }
                else
                {
                    DateTime dtGivenDate = DateTime.Parse(ViewState["Curr"].ToString());
                    GetFirstInMonth(dtGivenDate, out dtFromdate);
                    var vOnlyFirstCal = (from col in objLst
                                         where (col.DATUM) >= dtFromdate &&
                                         (col.DATUM) <= dtTodate
                                         select col);
                    foreach (leaverequestbo objBo in vOnlyFirstCal)
                    {
                        string PERNR = objBo.PERNR;
                        string ccode = Session["CompCode"].ToString();
                        string emplogin = PERNR.ToString();
                        int cnt = ccode.Length;
                        emplogin = emplogin.Substring(cnt);
                        string empid = emplogin.Trim();
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT")//// && objBo.HR_STATUS == null)
                        {
                            e.Cell.BackColor = Color.CornflowerBlue;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);

                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT")// && objBo.HR_STATUS == "Review")
                        {
                            e.Cell.BackColor = Color.CornflowerBlue;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {

                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT")// && objBo.HR_STATUS == "Approved")
                        { e.Cell.BackColor = Color.CornflowerBlue; e.Cell.ForeColor = Color.White; }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")// && objBo.HR_STATUS == "Review")
                        { e.Cell.BackColor = Color.CornflowerBlue; e.Cell.ForeColor = Color.White; }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")// && objBo.HR_STATUS == null)
                        {
                            e.Cell.BackColor = Color.Green;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                             + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                             + "\nEmp ID\t\t: " + empid.ToString()
                                                             + "\nLeave Type\t: " + objBo.ATEXT
                                                             + "\nDuration\t: " + objBo.DURATIONTEXT
                                                             + "\nStatus\t\t: " + objBo.STATUS
                                                             + "\nNote\t\t: " + objBo.NOTE);

                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "DELETION REQUESTED")
                        {
                            e.Cell.BackColor = Color.Gray;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);

                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")// && objBo.HR_STATUS == "Approved")
                        { e.Cell.BackColor = Color.FromArgb(35, 186, 53); e.Cell.ForeColor = Color.White; }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")////&& objBo.HR_STATUS == null)
                        {
                            e.Cell.ForeColor = Color.White;
                            e.Cell.BackColor = Color.FromArgb(35, 186, 53);
                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);

                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        protected void Cal_Next_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {

                if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    e.Cell.BackColor = Color.FromArgb(255, 255, 204);
                    e.Cell.Font.Bold = true;
                    e.Cell.BorderColor = Color.Black;
                    e.Cell.ForeColor = Color.Black;
                    if (!e.Day.IsOtherMonth)
                    {
                       
                    }
                }

                if (e.Day.IsOtherMonth)
                {
                   
                }
                else
                {
                    DateTime dtGivenDate = DateTime.Parse(ViewState["Next"].ToString());
                    GetFirstInMonth(dtGivenDate, out dtFromdate);
                    var vOnlyFirstCal = (from col in objLst
                                         where (col.DATUM) >= dtFromdate &&
                                         (col.DATUM) <= dtTodate
                                         select col);
                    foreach (leaverequestbo objBo in vOnlyFirstCal)
                    {
                        string PERNR = objBo.PERNR;
                        string ccode = Session["CompCode"].ToString();
                        string emplogin = PERNR.ToString();
                        int cnt = ccode.Length;
                        emplogin = emplogin.Substring(cnt);
                        string empid = emplogin.Trim();
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT")//// && objBo.HR_STATUS == null)
                        {
                            e.Cell.BackColor = Color.CornflowerBlue;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT")// && objBo.HR_STATUS == "Review")
                        {
                            e.Cell.BackColor = Color.CornflowerBlue;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {

                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT")// && objBo.HR_STATUS == "Approved")
                        { e.Cell.BackColor = Color.CornflowerBlue; e.Cell.ForeColor = Color.White; }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")// && objBo.HR_STATUS == "Review")
                        { e.Cell.BackColor = Color.CornflowerBlue; e.Cell.ForeColor = Color.White; }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")// && objBo.HR_STATUS == null)
                        {
                            e.Cell.BackColor = Color.Green;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "DELETION REQUESTED")
                        {
                            e.Cell.BackColor = Color.Gray;
                            e.Cell.ForeColor = Color.White;
                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")// && objBo.HR_STATUS == "Approved")
                        { e.Cell.BackColor = Color.FromArgb(35, 186, 53); e.Cell.ForeColor = Color.White; }
                        if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")//// && objBo.HR_STATUS == null)
                        {
                            e.Cell.ForeColor = Color.White;
                            e.Cell.BackColor = Color.FromArgb(35, 186, 53);
                            if (!e.Day.IsOtherMonth)
                            {
                                TimeSpan Tm = e.Day.Date - DtMillanium;
                                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
                            }
                        }
                    }

                }

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        protected void WZ_LeaveReq_ActiveStepChanged(object sender, EventArgs e)
        {
            try
            {
                //e.Cancel = true;     
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                int Index = WZ_LeaveReq.ActiveStepIndex;

                switch (Index)
                {
                    case 0: //Create Leave Req
                        break;
                    case 1: // Confirm Leave Req 
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReqFinish);

                        //if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "2100") || (DDL_LeaveType.SelectedValue.ToString().Trim() == "2101"))
                        if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "2100"))
                        {
                            if (divft is System.Web.UI.HtmlControls.HtmlControl)
                            {
                                if (divtt is System.Web.UI.HtmlControls.HtmlControl)
                                {
                                    if (divmol is System.Web.UI.HtmlControls.HtmlControl)
                                    {
                                        divft.Visible = true;
                                        divtt.Visible = true;
                                        divmol.Visible = false;
                                    }
                                }
                            }

                        }

                        else
                        {
                            if (divft is System.Web.UI.HtmlControls.HtmlControl)
                            {
                                if (divtt is System.Web.UI.HtmlControls.HtmlControl)
                                {
                                    if (divmol is System.Web.UI.HtmlControls.HtmlControl)
                                    {
                                        divmol.Visible = true;
                                        divtt.Visible = false;
                                        divft.Visible = false;
                                    }
                                }
                            }

                        }

                        DefaultRunFunction();
                        break;
                    case 2:
                        // WS_EduQualification
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Key", "setTimeout(function(){window.location.href = window.location},7000);      window.location.href=window.location;", true);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                switch (Ex.Message)
                {
                    case "-01":
                        MsgCls("Working time does not exist !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-02":
                        MsgCls("Leave cannot be applied on Company Weekends and Public holidays !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);

                        break;
                    default:
                        MsgCls(Ex.Message, LblMsg, Color.Red);
                        break;
                }
                DefaultRunFunction();
            }
        }

        protected void WZ_LeaveReq_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                int HDType = 0;
                string HR_Email = string.Empty;
                string Supervisor_Email = string.Empty;
                string PERNR_Name = string.Empty;
                string PERNR_Email = string.Empty;
                string PKEYO = string.Empty;
                int? LID = 0;
                string Tbltype = string.Empty;

                DateTime DtFrom = new DateTime(1900, 01, 01);
                DateTime DtTo = new DateTime(1900, 01, 01);
                if (DateTime.TryParse(TxtFromDate.Text, out DtFrom))
                {
                    if (DateTime.TryParse(TxtToDate.Text, out DtTo))
                    {
                        TimeSpan TsFrom = TimeSpan.Parse("09:30");
                        TimeSpan TsTo = TimeSpan.Parse(rbtnHalfDay.Checked ? "14:00" : "18:30");

                        TimeSpan TsTtlDays = DtTo - DtFrom;

                        leaverequestbo objLeaveRequestBo = new leaverequestbo();
                        leaverequestbl objLeaveRequestBl = new leaverequestbl();
                        objLeaveRequestBo.PERNR = User.Identity.Name;//70297;
                        objLeaveRequestBo.BEGDA = DtFrom;
                        objLeaveRequestBo.ENDDA = DtTo;
                        //if (DropDownReqTyp.SelectedValue == "1")
                        //{
                        //    objLeaveRequestBo.BEGUZ = TsFrom.ToString();
                        //    objLeaveRequestBo.ENDUZ = TsTo.ToString();
                        //}
                        //else if (DropDownReqTyp.SelectedValue == "2")
                        //{
                        //    objLeaveRequestBo.BEGUZ = TxtFromTime.Text;
                        //    objLeaveRequestBo.ENDUZ = TxtToTime.Text;
                        //}

                        if (DDL_LeaveType.SelectedValue.ToString().Trim() != "2100")
                        {
                            objLeaveRequestBo.BEGUZ = "00:00";
                            objLeaveRequestBo.ENDUZ = "00:00";
                        }
                        else if (DDL_LeaveType.SelectedValue.ToString().Trim() == "2100")
                        {
                            objLeaveRequestBo.BEGUZ = TxtFromTime.Text;
                            objLeaveRequestBo.ENDUZ = TxtToTime.Text;
                        }

                        //objLeaveRequestBo.BEGUZ = TsFrom.ToString();
                        //objLeaveRequestBo.ENDUZ = TsTo.ToString();
                        objLeaveRequestBo.AWART = DDL_LeaveType.SelectedValue.ToString().Trim();
                        objLeaveRequestBo.ATEXT = DDL_LeaveType.SelectedItem.Text.ToString();
                        objLeaveRequestBo.STDAZ = DtFrom == DtTo ?rbtnHalfDay.Checked ? "0.5" : TsTtlDays.TotalDays.ToString() : TsTtlDays.TotalDays.ToString();
                        objLeaveRequestBo.NOTE = TxtNoteForApprover.Text.Trim();
                        objLeaveRequestBo.APPROVED_BY = DDLApprover.SelectedValue;
                        objLeaveRequestBo.STATUS = "sent";
                        objLeaveRequestBo.PERNR = User.Identity.Name;
                        if (DropDownReqTyp.SelectedValue == "1")
                        {
                            objLeaveRequestBo.Flag = 1;
                            Tbltype = "PA2001";
                        }
                        else if (DropDownReqTyp.SelectedValue == "2")
                        {
                            objLeaveRequestBo.Flag = 5;
                            Tbltype = "PA2002";
                        }
                        //objLeaveRequestBo.Flag = 1;
                        objLeaveRequestBo.TABLETYPE = DropDownReqTyp.SelectedItem.Text.ToString();

                        if (rbtn1Half.Checked)
                        {
                            HDType = 1;
                        }
                        else if (rbtn2Half.Checked)
                        {
                            HDType = 2;
                        }

                        if (rbtnFullDay.Checked)
                        {
                            HDType = 0;
                        }

                        DefaultRunFunction();
                        if (Session["update"].ToString() == ViewState["update"].ToString())
                        {
                            objLeaveRequestBl.Create_Leave_Request_Details(Session["CompCode"].ToString(), objLeaveRequestBo, HDType, ref HR_Email, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, ref LID, ref PKEYO);
                            //string[] MsgCC = { HR_Email, Supervisor_Email };
                            //string[] MsgCC = { "monica.ks@itchamps.com", "latha.mg@itchamps.com" };
                            //iEmpPowerMaster_Load.masterbl.SendMail(PERNR_Email, MsgCC, DDL_LeaveType.SelectedItem.Text.ToString() + " - request by - " + PERNR_Name + " (" + User.Identity.Name + ")"
                            //    , GetMailBody(objLeaveRequestBo, User.Identity.Name.ToString()));

                            List<leaverequestbo> objList = new List<leaverequestbo>();
                            objList = objBl.Approval_LeaveDetails_Mail(PKEYO.ToString().Trim(), int.Parse(LID.ToString().Trim()), "", Tbltype);
                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            SendMailLeave(objList, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, Tbltype);
                            //Response.Redirect(Request.Url.AbsoluteUri);   

                            if (Session["isDR"].ToString().Trim() == "True")
                            {

                                ScriptManager.RegisterStartupScript(this, GetType(), "ScriptKey",
                                                        "alert('Leave / Attendance request has been successfully created and has been self approved');window.location='../Working_Time/Leaverequest_new.aspx';", true);
                            }

                            else
                            {
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ScriptKey", "alert('Leave request has been successfully created and sent for approval');window.location='../UI/Working_Time/Leaverequest_new.aspx'; ", true);    
                                ScriptManager.RegisterStartupScript(this,GetType(), "ScriptKey",
                                                        "alert('Leave / Attendance request has been successfully created and sent for approval');window.location='../Working_Time/Leaverequest_new.aspx';", true);
                            }
                        }



                    }
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        private void SendMailLeave(List<leaverequestbo> objList, ref string Supervisor_Email, ref string HR_Email, ref string PERNR_Name, ref string PERNR_Email, string tbltyp)
        {
            try
            {

                leaverequestbo objPIFddBo = new leaverequestbo();
                leaverequestbl objPIFddBl = new leaverequestbl();

                if (objList.Count > 0)
                {


                    string PERNR = objList[0].PERNR == null ? "" : objList[0].PERNR.ToString();
                    string BEGDA = objList[0].BEGDA == null ? "" : objList[0].BEGDA.ToString();
                    string ENDDA = objList[0].ENDDA == null ? "" : objList[0].ENDDA.ToString();
                    string BEGUZ = objList[0].BEGUZ == null ? "" : objList[0].BEGUZ.ToString();
                    string ENDUZ = objList[0].ENDUZ == null ? "" : objList[0].ENDUZ.ToString();
                    string AWART = objList[0].AWART == null ? "" : objList[0].AWART.ToString();
                    string STDAZ = objList[0].STDAZ == null ? "" : objList[0].STDAZ.ToString();
                    string NOTE = objList[0].NOTE == null ? "" : objList[0].NOTE.ToString();

                    string APPROVED_BY = objList[0].APPROVED_BY == null ? "" : objList[0].APPROVED_BY.ToString();
                    string CREATED_ON = objList[0].CREATED_ON == null ? "" : objList[0].CREATED_ON.ToString();
                    string APPROVED_ON = objList[0].APPROVED_ON == null ? "" : objList[0].APPROVED_ON.ToString();
                    string ATEXT = objList[0].ATEXT == null ? "" : objList[0].ATEXT.ToString();
                    string REMARKS = objList[0].REMARKS == null ? "" : objList[0].REMARKS.ToString();
                    string EMPLOYEE_NAME = objList[0].EMPLOYEE_NAME == null ? "" : objList[0].EMPLOYEE_NAME.ToString();
                    string APPROVED_BY_NAME = objList[0].APPROVED_BY_NAME == null ? "" : objList[0].APPROVED_BY_NAME.ToString();
                    string DURATION = objList[0].DURATION == null ? "" : objList[0].DURATION.ToString();


                    string strSubject = string.Empty;

                    string ccode = Session["CompCode"].ToString();
                    string empid = User.Identity.Name;
                    int cnt = ccode.Length;
                    empid = empid.Substring(cnt).ToUpper();

                    if (Session["isDR"].ToString().Trim() == "True")
                    {
                        strSubject = ATEXT + " request has been raised by " + PERNR_Name + "  | " + empid.ToString().Trim() + ".This has been Self Approved, No Action Required.";
                    }

                    else
                    {
                        strSubject = ATEXT + " request has been raised by " + PERNR_Name + "  | " + empid.ToString().Trim() + ".";
                    }

                    //strSubject = ATEXT + " request has been raised by " + PERNR_Name + "  | " + User.Identity.Name + ".";



                    string RecipientsString = Supervisor_Email;
                    string strPernr_Mail = PERNR_Email + "," + HR_Email;

                    //    //Preparing the mail body--------------------------------------------------

                    if (tbltyp.Trim() == "PA2001")
                    {

                        string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                        //body = body + "<b style= 'font-size: 14px';>Family details : </b><hr>";
                        //body += "to-from " + RecipientsString + "-" + strPernr_Mail + ".";
                        body += "<b><table style=border-collapse:collapse;><tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + PERNR_Name.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + empid.ToString().Trim() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Request Type </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ATEXT.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(BEGDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(ENDDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Time </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + BEGUZ.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Time </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ENDUZ.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Duration(Days) </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DURATION.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Reason </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + NOTE.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Applied on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(CREATED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr></table></b>";
                        //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>" + sts.Trim() + " on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(APPROVED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Remarks </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + REMARKS.ToString() + "</td></tr></table></b>";


                        body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, empid.ToString().Trim(), strSubject, strPernr_Mail, body);

                    }

                    else if (tbltyp.Trim() == "PA2002")
                    {

                        string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                        //body = body + "<b style= 'font-size: 14px';>Family details : </b><hr>";
                        body += "<b><table style=border-collapse:collapse;><tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + PERNR_Name.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + empid.ToString().Trim() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Request Type </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ATEXT.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(BEGDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(ENDDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Time </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + BEGUZ.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Time </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ENDUZ.ToString() + "</td></tr>";

                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Duration(Days) </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DURATION.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Reason </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + NOTE.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Applied on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(CREATED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr></table></b>";
                        //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>" + sts.Trim() + " on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(APPROVED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Remarks </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + REMARKS.ToString() + "</td></tr></table></b>";


                        body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, empid.ToString().Trim(), strSubject, strPernr_Mail, body);

                    }

                    // body += "</br>" + sw1.ToString() + "<br/>";
                    //    //End of preparing the mail body-------------------------------------------
                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                }


                //break;

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void WZ_LeaveReq_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                int HDType = 0;

                if (DDL_LeaveType.SelectedValue.ToString().Trim() == "2100")
                {
                    DateTime DtFrmCompff = new DateTime(1900, 01, 01);
                    DateTime DtToCompff = new DateTime(1900, 01, 01);

                    if (DateTime.TryParse(TxtFromTime.Text, out DtFrmCompff))
                    {
                        if (DateTime.TryParse(TxtToTime.Text, out DtToCompff))
                        {
                            TimeSpan ts = DtToCompff - DtFrmCompff;
                            double result = ts.TotalHours;

                            if (result < 4.0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Comp off should be applied for more than four hours');", true);
                                e.Cancel = true;
                                return;
                            }
                        }
                    }
                }

                //if ((DDL_LeaveType.SelectedValue.ToString().Trim() != "2100") && (DDL_LeaveType.SelectedValue.ToString().Trim() != "2101"))
                if ((DDL_LeaveType.SelectedValue.ToString().Trim() != "2100"))
                {
                    PnlFrmToTime.Visible = false;
                }
                //else if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "2100") || (DDL_LeaveType.SelectedValue.ToString().Trim() == "2101"))
                else if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "2100"))
                {
                    PnlFrmToTime.Visible = true;
                }

                bool? IsValid = false;
                DateTime DtFrm = new DateTime(1900, 01, 01);
                DateTime DtTo = new DateTime(1900, 01, 01);
                leaverequestbo objLeaveRequestBo = new leaverequestbo();
                leaverequestbl objLeaveRequestBl = new leaverequestbl();
                if (DateTime.TryParse(TxtFromDate.Text, out DtFrm))
                {
                    if (DateTime.TryParse(TxtToDate.Text, out DtTo))
                    {
                        objLeaveRequestBo.PERNR = User.Identity.Name;//70297;
                        objLeaveRequestBo.BEGDA = DtFrm;
                        objLeaveRequestBo.ENDDA = DtTo;
                        objLeaveRequestBo.AWART = DDL_LeaveType.SelectedValue.ToString().Trim();

                        //if ((DDL_LeaveType.SelectedValue.ToString().Trim() != "2100") && (DDL_LeaveType.SelectedValue.ToString().Trim() != "2101"))
                        if ((DDL_LeaveType.SelectedValue.ToString().Trim() != "2100"))
                        {
                            objLeaveRequestBo.BEGUZ = "00:00";
                            objLeaveRequestBo.ENDUZ = "00:00";
                        }
                        //else if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "2100") || (DDL_LeaveType.SelectedValue.ToString().Trim() == "2101"))
                        else if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "2100"))
                        {
                            objLeaveRequestBo.BEGUZ = TxtFromTime.Text;
                            objLeaveRequestBo.ENDUZ = TxtToTime.Text;
                        }

                        //objLeaveRequestBo.STDAZ = rbtnHalfDay.Checked ? "0.5" : "8.0";
                        objLeaveRequestBo.STDAZ = rbtnHalfDay.Checked ? "0.5" : "1.0";
                        if (DropDownReqTyp.SelectedValue == "1")
                        {
                            objLeaveRequestBo.Flag = 1;
                        }
                        else if (DropDownReqTyp.SelectedValue == "2")
                        {
                            objLeaveRequestBo.Flag = 2;
                        }

                        if (rbtn1Half.Checked)
                        {
                            HDType = 1;
                        }
                        else if (rbtn2Half.Checked)
                        {
                            HDType = 2;
                        }

                        if (rbtnFullDay.Checked)
                        {
                            HDType = 0;
                        }

                        objLeaveRequestBl.LeaveValidation(Session["CompCode"].ToString(), objLeaveRequestBo, HDType, ref IsValid);

                        if ((bool)IsValid)
                        { e.Cancel = false; }
                        else { e.Cancel = true; }
                    }
                }
            }
            catch (Exception Ex)
            {
                switch (Ex.Message)
                {
                    case "-01":
                        MsgCls("Working time does not exist !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-02":
                        MsgCls("Start Date should be less than End date", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-03":
                        MsgCls("Does'nt have leave Quota !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-03A":
                        MsgCls("Cannot apply half day for this leave type !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-04":
                        MsgCls("Leave cannot be applied on Company Weekends and Public holidays !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-05":
                        MsgCls("Leave Quota exceeded !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-06":
                        MsgCls("Leave / Attendance already exists for these dates !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-07":
                        MsgCls("Maternity leave cannot be applied more than 2 times !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-08":
                        MsgCls("Pre Comp-Off should be applied for 1 day & should be between Calendar year of " + DateTime.Today.Year.ToString(), LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-11":
                        MsgCls("Attendance can be applied only on Sunday and Public holidays !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-12":
                        MsgCls("From Time should be less than To Time", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-10":
                        MsgCls("Leave Quota Exists. Please make use of it", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-16":
                        MsgCls("Regular Attendance / Work From Home cannot be applied on Public holidays !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-17":
                        MsgCls("This Leave type cannot be applied for half a day !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-20":
                        MsgCls("Working Time Rule is not maintained for selected dates !", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-21":
                        MsgCls("The Restricted Leave can be applied only on Restricted Holiday!", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;
                    case "-22":
                        MsgCls("Leave / attd. cannot be applied for these dates, since payroll has been run!", LblMsg, Color.Red);
                        WZ_LeaveReq.MoveTo(this.WS_CreateLeaveReq);
                        break;

                    default:
                        MsgCls(Ex.Message, LblMsg, Color.Red);
                        break;
                }
                DefaultRunFunction();
                e.Cancel = true;
            }
        }

        protected void WZ_LeaveReq_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                DefaultRunFunction();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #region Default Run Methods
        private void DefaultRunFunction()
        {
            try
            {
                LoadCalendarsWithLeaveMarkings();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Transparent); }
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
                RptrLeaveOverViewPager.DataSource = pages;
                RptrLeaveOverViewPager.DataBind();

                GV_LeaveOverView.FooterRow.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;<b>Page " + currentPage + " of " + pageCount + "<b/>";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        protected void Page_Changed(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
                this.Bind_GV_LeaveOverView(pageIndex);
                DefaultRunFunction();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }
        #endregion

        #region Reset Button click
        protected void BtnRestLeaveOverview_Click(object sender, EventArgs e)
        {
            try
            {
                TxtLeaveOverView.Text = string.Empty;
                Bind_GV_LeaveOverView(1);
                DefaultRunFunction();
                LblMsg.Text = "";
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region Calendar Selection Changed event

        protected void Cal_Previous_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Load_GV_SelectedDateLeaveView();
                Cal_Previous.SelectedDates.Clear();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void Cal_Current_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Load_GV_SelectedDateLeaveView();
                Cal_Current.SelectedDates.Clear();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void Cal_Next_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Load_GV_SelectedDateLeaveView();
                Cal_Next.SelectedDates.Clear();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #endregion
        //----------------------- BIND LEAVE OVERVIEW ---------------------------
        #region Bind GV_LeaveOverview
        private void Bind_GV_LeaveOverView(int PageIndex)
        {
            try
            {
                leaverequestbo ObjLeaveRequestBo = new leaverequestbo();
                leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                leaverequestcollectionbo ObjLeaveReqLst = new leaverequestcollectionbo();

                int? RecordCount = 0;
                DateTime DtLeaveFrm = new DateTime();
                if (DateTime.TryParse(TxtLeaveOverView.Text.Trim(), out DtLeaveFrm))
                { }
                else { DtLeaveFrm = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01).AddMonths(-1); }
                // else
                // { MsgCls("Invalid Date", LnlLeaveOverviewErrMsg, Color.Red); }
                ObjLeaveRequestBo.PERNR = User.Identity.Name;
                ObjLeaveRequestBo.LEAVESINCE = DtLeaveFrm;
                ObjLeaveRequestBo.PageIndex = PageIndex;
                ObjLeaveRequestBo.PageSize = 10;
                ObjLeaveRequestBo.Flag = 1;
                ObjLeaveReqLst = ObjLeaveRequestBl.Get_LeaveOverview(Session["CompCode"].ToString(), ObjLeaveRequestBo, ref RecordCount);
                Session.Add("LeaveOvrView", ObjLeaveReqLst);
                if (ObjLeaveReqLst.Count > 0)
                {
                    MsgCls(string.Empty, LnlLeaveOverviewErrMsg, Color.Transparent);
                    GV_LeaveOverView.DataSource = ObjLeaveReqLst;
                    GV_LeaveOverView.DataBind();
                    PopulatePager(int.Parse(RecordCount == null ? "0" : RecordCount.ToString()), PageIndex);
                    MsgCls(string.Empty, LnlLeaveOverviewErrMsg, Color.Transparent);

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_LeaveOverView.Rows)
                    {
                        for (int i = 0; i < GV_LeaveOverView.Rows.Count; i++)
                        {
                            //Label lblRowNumber = (Label)GV_LeaveOverView.Rows[i].FindControl("lblRowNumber");
                            if (i == 0)
                            {
                                frow = GV_LeaveOverView.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_LeaveOverView.Rows.Count - 1)
                            {
                                lrow = GV_LeaveOverView.Rows[i].Cells[0].Text;
                            }
                        }
                    }
                    divpendingrecordcount.InnerHtml = "Showing " + frow + " to " + lrow + " of " + RecordCount + " entries";
                    divpendingrecordcount.Visible = GV_LeaveOverView.Rows.Count > 0 ? true : false;

                }
                else
                {
                    GV_LeaveOverView.DataSource = null;
                    GV_LeaveOverView.DataBind();
                   // MsgCls("No Data found", LnlLeaveOverviewErrMsg, Color.Red);
                }

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LnlLeaveOverviewErrMsg, Color.Transparent); }
        }
        #endregion
        //------------- LEAVE OVERVIEW CLICK -----------------
        #region Leave Overview
        protected void BtnViewLeaveOverview_Click(object sender, EventArgs e)
        {
            try
            {
                Bind_GV_LeaveOverView(1);
                DefaultRunFunction();
            }
            catch (Exception Ex)
            { }
        }
        #endregion

        public void leave_mainsrch()
        {
        try
            {
                leaverequestbo ObjLeaveRequestBo = new leaverequestbo();
                leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                leaverequestcollectionbo ObjLeaveReqLst = new leaverequestcollectionbo();

                DateTime DtLeaveFrm = new DateTime();
                if (DateTime.TryParse(TxtLeaveOverView.Text.Trim(), out DtLeaveFrm))
                { }
                else { DtLeaveFrm = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01).AddMonths(-1); }
               
                ObjLeaveRequestBo.PERNR = User.Identity.Name;
                ObjLeaveRequestBo.LEAVESINCE = DtLeaveFrm;
                ObjLeaveRequestBo.Flag = 2;
                ObjLeaveReqLst = ObjLeaveRequestBl.Get_Leaveattd_srch(Session["CompCode"].ToString(), ObjLeaveRequestBo);
                Session.Add("LeaveOvrView", ObjLeaveReqLst);
                if (ObjLeaveReqLst.Count > 0)
                {
                    GV_LeaveOverView.DataSource = ObjLeaveReqLst;
                    GV_LeaveOverView.DataBind();
                }
                else
                {
                   MsgCls("No Data found", LnlLeaveOverviewErrMsg, Color.Red);
                }

            }
            catch (Exception Ex)
            { }
        }


        public void attd_mainsrch()
        {
            try
               {
                leaverequestbo ObjLeaveRequestBo = new leaverequestbo();
                leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                leaverequestcollectionbo ObjLeaveReqLst = new leaverequestcollectionbo();

                DateTime DtLeaveFrm = new DateTime();
                if (DateTime.TryParse(TxtLeaveOverView.Text.Trim(), out DtLeaveFrm))
                { }
                else { DtLeaveFrm = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01).AddMonths(-1); }
               
                ObjLeaveRequestBo.PERNR = User.Identity.Name;
                ObjLeaveRequestBo.LEAVESINCE = DtLeaveFrm;
                ObjLeaveRequestBo.Flag = 1;
                ObjLeaveReqLst = ObjLeaveRequestBl.Get_Leaveattd_srch(Session["CompCode"].ToString(), ObjLeaveRequestBo);
                Session.Add("LeaveOvrView", ObjLeaveReqLst);
                if (ObjLeaveReqLst.Count > 0)
                {
                    GV_LeaveOverView.DataSource = ObjLeaveReqLst;
                    GV_LeaveOverView.DataBind();
                }
                else
                {
                   MsgCls("No Data found", LnlLeaveOverviewErrMsg, Color.Red);
                }

            }
            catch (Exception Ex)
            { }
            
        }


        //---------- GET APPLIED LEAVE ----------------------------
        #region Load GV_SelectedDataLeaveView

        private void Load_GV_SelectedDateLeaveView()
        {
            try
            {
                DateTime Dt = new DateTime();
                //if (DateTime.TryParse(Date, out Dt))
                //{
                    leaverequestbo ObjLeaveRequestBo = new leaverequestbo();
                    leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                    leaverequestcollectionbo ObjLeaveReqLst = new leaverequestcollectionbo();
                    ObjLeaveRequestBo.PERNR = User.Identity.Name;
                    //ObjLeaveRequestBo.DATUM = Dt;
                    ObjLeaveReqLst = ObjLeaveRequestBl.Get_Individual_Leave_Dtls(Session["CompCode"].ToString(), ObjLeaveRequestBo);

                    GV_SelectedDateLeaveView.DataSource = ObjLeaveReqLst;
                    GV_SelectedDateLeaveView.DataBind();
                    RegisterPostBackControl();
                    DefaultRunFunction();

                //}
                //else
                //{ MsgCls("Select date in invalid !", LblMsg, Color.Red); }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #endregion
        //----------------------------------------------------------

        private void SendMailLeaveDel(List<leaverequestbo> objList, ref string Supervisor_Email, ref string HR_Email, ref string PERNR_Name, ref string PERNR_Email, string tbltyp)
        {
            try
            {

                leaverequestbo objPIFddBo = new leaverequestbo();
                leaverequestbl objPIFddBl = new leaverequestbl();

                if (objList.Count > 0)
                {


                    string PERNR = objList[0].PERNR == null ? "" : objList[0].PERNR.ToString();
                    string BEGDA = objList[0].BEGDA == null ? "" : objList[0].BEGDA.ToString();
                    string ENDDA = objList[0].ENDDA == null ? "" : objList[0].ENDDA.ToString();
                    string BEGUZ = objList[0].BEGUZ == null ? "" : objList[0].BEGUZ.ToString();
                    string ENDUZ = objList[0].ENDUZ == null ? "" : objList[0].ENDUZ.ToString();
                    string AWART = objList[0].AWART == null ? "" : objList[0].AWART.ToString();
                    string STDAZ = objList[0].STDAZ == null ? "" : objList[0].STDAZ.ToString();
                    string NOTE = objList[0].NOTE == null ? "" : objList[0].NOTE.ToString();

                    string APPROVED_BY = objList[0].APPROVED_BY == null ? "" : objList[0].APPROVED_BY.ToString();
                    string CREATED_ON = objList[0].CREATED_ON == null ? "" : objList[0].CREATED_ON.ToString();
                    string APPROVED_ON = objList[0].APPROVED_ON == null ? "" : objList[0].APPROVED_ON.ToString();
                    string ATEXT = objList[0].ATEXT == null ? "" : objList[0].ATEXT.ToString();
                    string REMARKS = objList[0].REMARKS == null ? "" : objList[0].REMARKS.ToString();
                    string EMPLOYEE_NAME = objList[0].EMPLOYEE_NAME == null ? "" : objList[0].EMPLOYEE_NAME.ToString();
                    string APPROVED_BY_NAME = objList[0].APPROVED_BY_NAME == null ? "" : objList[0].APPROVED_BY_NAME.ToString();
                    string DURATION = objList[0].DURATION == null ? "" : objList[0].DURATION.ToString();


                    string strSubject = string.Empty;

                    string cc = Session["CompCode"].ToString();
                    string empid = User.Identity.Name;
                    int ct = cc.Length;
                    empid = empid.Substring(ct).ToUpper();


                    if (Session["isDR"].ToString().Trim() == "True")
                    {
                        strSubject = "Deletion request for " + ATEXT + " has been raised by " + PERNR_Name + "  | " + empid.ToString().Trim() + ".This has been Self Approved, No Action Required.";
                    }

                    else
                    {
                        strSubject = "Deletion request for " + ATEXT + " has been raised by " + PERNR_Name + "  | " + empid.ToString().Trim() + ".";
                    }
                    //strSubject = "Deletion request for " + ATEXT + " has been raised by " + PERNR_Name + "  | " + User.Identity.Name + ".";



                    string RecipientsString = Supervisor_Email;
                    string strPernr_Mail = PERNR_Email + "," + HR_Email;

                    //    //Preparing the mail body--------------------------------------------------

                    if (tbltyp.Trim() == "PA2001")
                    {

                        string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                        //body = body + "<b style= 'font-size: 14px';>Family details : </b><hr>";
                        body += "<b><table style=border-collapse:collapse;><tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + PERNR_Name.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + empid.ToString().Trim() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Request Type </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ATEXT.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(BEGDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(ENDDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Duration(Days) </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DURATION.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Reason </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + NOTE.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Applied on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(CREATED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr></table></b>";
                        //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>" + sts.Trim() + " on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(APPROVED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Remarks </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + REMARKS.ToString() + "</td></tr></table></b>";


                        body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, empid.ToString().Trim(), strSubject, strPernr_Mail, body);

                    }

                    else if (tbltyp.Trim() == "PA2002")
                    {

                        string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                        //body = body + "<b style= 'font-size: 14px';>Family details : </b><hr>";
                        body += "<b><table style=border-collapse:collapse;><tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + PERNR_Name.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + PERNR.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Request Type </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ATEXT.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(BEGDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(ENDDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Time </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + BEGUZ.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Time </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ENDUZ.ToString() + "</td></tr>";

                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Duration(Days) </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DURATION.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Reason </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + NOTE.ToString() + "</td></tr>";
                        body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Applied on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(CREATED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr></table></b>";
                        //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>" + sts.Trim() + " on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(APPROVED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                        //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Remarks </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + REMARKS.ToString() + "</td></tr></table></b>";


                        body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                    }

                    // body += "</br>" + sw1.ToString() + "<br/>";
                    //    //End of preparing the mail body-------------------------------------------
                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                }


                //break;

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        #region  GV_SelectedDateLeaveView Events
        protected void GV_SelectedDateLeaveView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "DELETE":
                        int HDType = 0;
                        int @LEAVE_REQ_ID = int.Parse(GV_SelectedDateLeaveView.DataKeys[int.Parse(e.CommandArgument.ToString())]["LEAVE_REQ_ID"].ToString());
                        string @table_name = GV_SelectedDateLeaveView.DataKeys[int.Parse(e.CommandArgument.ToString())]["TABLETYPE"].ToString();
                        string sts = GV_SelectedDateLeaveView.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString();
                        string HR_Email = string.Empty;
                        string Supervisor_Email = string.Empty;
                        string PERNR_Name = string.Empty;
                        string PERNR_Email = string.Empty;
                        string PKEYO = string.Empty;
                        int? LID = 0;

                        leaverequestbo objLeaveRequestBo = new leaverequestbo();
                        leaverequestbl objLeaveRequestBl = new leaverequestbl();
                        objLeaveRequestBo.LEAVE_REQ_ID = @LEAVE_REQ_ID;
                        objLeaveRequestBo.PERNR = User.Identity.Name;//70297;
                        objLeaveRequestBo.BEGDA = new DateTime();
                        objLeaveRequestBo.ENDDA = new DateTime();
                        objLeaveRequestBo.BEGUZ = "09:30:00";
                        objLeaveRequestBo.ENDUZ = "18:30:00";
                        objLeaveRequestBo.AWART = "";
                        objLeaveRequestBo.STDAZ = "";
                        objLeaveRequestBo.NOTE = "";
                        objLeaveRequestBo.APPROVED_BY = "";
                        objLeaveRequestBo.STATUS = "sent";
                        objLeaveRequestBo.Flag = 3;
                        objLeaveRequestBo.TABLETYPE = @table_name;
                        objLeaveRequestBl.Create_Leave_Request_Details(Session["CompCode"].ToString(), objLeaveRequestBo, HDType, ref HR_Email, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, ref LID, ref PKEYO);
                        //---------------------------------------------------------

                        if (sts.ToString().Trim() == "Approved" || sts.ToString().Trim() == "Sent" || sts.ToString().Trim() == "Pending")
                        {
                            List<leaverequestbo> objList = new List<leaverequestbo>();
                            objList = objBl.Approval_LeaveDetails_Mail(PKEYO.ToString().Trim(), int.Parse(LID.ToString().Trim()), "", @table_name);


                            SendMailLeaveDel(objList, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, @table_name);

                        }
                        Load_GV_SelectedDateLeaveView();
                        Bind_GV_LeaveOverView(1);
                        Bind_GV_LeaveQuota("D");
                        MsgCls(string.Empty, LblMsg, Color.Transparent);
                        break;
                    default:
                        break;
                }
                //DefaultRunFunction();
            }
            catch (Exception Ex)
            {
                switch (Ex.Message)
                {
                    case "-0":
                        MsgCls("Cannot delete this leave request. It has been approved / rejected by your manager !", LblMsg, Color.Red);
                        break;
                    default:
                        MsgCls(Ex.Message, LblMsg, Color.Red);
                        break;
                }
                DefaultRunFunction();
            }
        }

        protected void GV_SelectedDateLeaveView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        private void RegisterPostBackControl()
        {
            foreach (GridViewRow row in GV_SelectedDateLeaveView.Rows)
            {
                using (LinkButton lnkFull = row.FindControl("LbtnLeaveAttDelete") as LinkButton)
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);
            }
        }
        #endregion

        #region Bind GV_LeaveQuota
        private void Bind_GV_LeaveQuota(string Type)
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                int Year = DateTime.Today.Year;
                leaverequestbo ObjLeaveRequestBo = new leaverequestbo();
                leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                leaverequestcollectionbo ObjLeaveReqLst = new leaverequestcollectionbo();
                switch (Type)
                {
                    case "D":
                        if (int.TryParse(string.IsNullOrEmpty(DDLLeaveQuotaYear.SelectedValue) ? DateTime.Today.Year.ToString() : DDLLeaveQuotaYear.SelectedValue, out Year))
                        { }
                        break;
                    default:
                        Year = DateTime.Today.Year;
                        break;
                }


                ObjLeaveRequestBo.PERNR = User.Identity.Name;
                ObjLeaveRequestBo.YEAR = Year;
                ObjLeaveReqLst = ObjLeaveRequestBl.Get_LeaveQuota(ObjLeaveRequestBo, Session["CompCode"].ToString());
                if (ObjLeaveReqLst.Count > 0)
                {
                    GV_LeaveQuota.DataSource = ObjLeaveReqLst;
                    GV_LeaveQuota.DataBind();
                    MsgCls(string.Empty, LblMsg, Color.Transparent);
                }
                else
                {
                    GV_LeaveQuota.DataSource = null;
                    GV_LeaveQuota.DataBind();
                }

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_LeaveQuota.Rows)
                {
                    for (int i = 0; i < GV_LeaveQuota.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_LeaveQuota.Rows[i].FindControl("LblSlno");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == GV_LeaveQuota.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + ObjLeaveReqLst.Count + " entries";
                divcnt.Visible = GV_LeaveQuota.Rows.Count > 0 ? true : false;
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion
        //----------------------- GET MAIL BODY ---------------------
        #region Get Mail Body
        private string GetMailBody(leaverequestbo objLeaveRequestBo, string EmpName)
        {
            try
            {
                StringBuilder Sb = new StringBuilder();
                string Mailbody = string.Empty;
                string AddressInfoFilePath = Server.MapPath(@"." + "/EmailTemplates/EmpLeaveReqEmailTemplate.html");

                using (LoginView LVMpageEmpName = (LoginView)Master.FindControl("HeadLoginView"))
                using (Label LblEmpName = LVMpageEmpName.FindControl("lblEmployyeName") as Label)
                {
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    Mailbody = Mailbody.Replace("##EMPNAME##", LblEmpName.Text.Contains('|') ? LblEmpName.Text.Split('|')[0] : LblEmpName.Text);
                    Mailbody = Mailbody.Replace("##EMPPERNR##", User.Identity.Name);
                    Mailbody = Mailbody.Replace("##LEAVETYPE##", objLeaveRequestBo.ATEXT);
                    Mailbody = Mailbody.Replace("##FROMDT##", objLeaveRequestBo.BEGDA.ToString());
                    Mailbody = Mailbody.Replace("##TODT##", objLeaveRequestBo.ENDDA.ToString());
                    //if (DDL_LeaveType.SelectedValue.ToString().Trim() == "2100" || DDL_LeaveType.SelectedValue.ToString().Trim() == "2101")
                    if (DDL_LeaveType.SelectedValue.ToString().Trim() == "2100")
                    {
                        Mailbody = Mailbody.Replace("##FROMTIME##", objLeaveRequestBo.BEGUZ.ToString());
                        Mailbody = Mailbody.Replace("##TOTIME##", objLeaveRequestBo.ENDUZ.ToString());
                    }
                    else
                    {
                        Mailbody = Mailbody.Replace("##FROMTIME##", "-");
                        Mailbody = Mailbody.Replace("##TOTIME##", "-");
                    }
                    Mailbody = Mailbody.Replace("##FROMTIME##", objLeaveRequestBo.BEGUZ.ToString());
                    Mailbody = Mailbody.Replace("##TOTIME##", objLeaveRequestBo.ENDUZ.ToString());

                    //if (DDL_LeaveType.SelectedValue.ToString().Trim() == "2100")
                    //{
                    //    TimeSpan tsfrom = TimeSpan.Parse(objLeaveRequestBo.BEGUZ.ToString());
                    //    TimeSpan tsto = TimeSpan.Parse(objLeaveRequestBo.ENDUZ.ToString());
                    //    TimeSpan tsduration = tsto - tsfrom;
                    //    TimeSpan baseInterval = new TimeSpan(6, 0, 0);

                    //    if (tsduration > baseInterval)
                    //    {
                    //        Mailbody = Mailbody.Replace("##DURATION_DETAILS##", "1.0");
                    //    }
                    //    else
                    //    {
                    //        Mailbody = Mailbody.Replace("##DURATION_DETAILS##", "0.5");
                    //    }
                    //}
                    //else
                    //{
                    //    Mailbody = Mailbody.Replace("##DURATION_DETAILS##", objLeaveRequestBo.STDAZ);
                    //}
                    //Mailbody = Mailbody.Replace("##DURATION_DETAILS##", objLeaveRequestBo.STDAZ);
                    Mailbody = Mailbody.Replace("##NOTE##", objLeaveRequestBo.NOTE);
                    Mailbody = Mailbody.Replace("##SENTDATE##", DateTime.Now.ToString("dddd, dd MMM yyyy - hh:mm:ss"));
                    //Mailbody = Mailbody.Replace("##DURATION##", string.Format("{0}", Rbtn_LeaveMode.SelectedValue == "0" ? "Duration" : ""));
                    //Mailbody = Mailbody.Replace("##DURATION_DETAILS##", rbtnHalfDay.Checked ? "Half Day" : "Full Day");
                    //Mailbody = Mailbody.Replace("##EMAILPATH##", ConfigurationManager.AppSettings["IEmpEmailSummayPath"].ToString());
                }


                return Mailbody;
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); return string.Empty; }
        }

        #endregion

        protected void DDLLeaveQuotaYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                Bind_GV_LeaveQuota("D");
                DefaultRunFunction();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void BtnViewLeaveQuotaReset_Click(object sender, EventArgs e)
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                DDLLeaveQuotaYear.SelectedValue = DateTime.Today.Year.ToString();
                Bind_GV_LeaveQuota("R");
                DefaultRunFunction();
                LblMsg.Text = "";
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void BtnViewLeaveQuotaView_Click(object sender, EventArgs e)
        {
            try
            {
                //CCD_DDLLeaveQuotaYear.SelectedValue = DateTime.Today.Year.ToString();
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                Bind_GV_LeaveQuota("D");
                DefaultRunFunction();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void DDL_LeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (DropDownReqTyp.SelectedValue.ToString().Trim() == "1")
                //{
                rbtnHalfDay.Checked = false;
                rbtnFullDay.Checked = true;
                Pnl_2HalfDay.Visible = false;

                //if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "2100") || (DDL_LeaveType.SelectedValue.ToString().Trim() == "2101"))
                if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "2100"))
                {
                    PnlFrmToTime.Visible = true;
                    PnlMode.Visible = false;
                    TxtToDate.Enabled = false;
                    Pnl_2HalfDay.Visible = false;

                }
                else if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "1080") || (DDL_LeaveType.SelectedValue.ToString().Trim() == "2101"))
                {
                    PnlFrmToTime.Visible = false;
                    PnlMode.Visible = true;
                    TxtToDate.Text = TxtFromDate.Text;
                    TxtToDate.Enabled = false;


                }
                else if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "1021"))
                {
                    PnlFrmToTime.Visible = false;
                    PnlMode.Visible = true;
                    TxtToDate.Text = TxtFromDate.Text;
                    TxtToDate.Enabled = true;
                }
                else
                {
                    TxtToDate.Enabled = true;
                    PnlFrmToTime.Visible = false;
                    PnlMode.Visible = true;

                }
                MinMaxDate();
                LoadCalendarsWithLeaveMarkings();

                if (DDL_LeaveType.SelectedValue.ToString().Trim() == "1030")
                {

                    rbtnHalfDay.Visible = false;
                    rbtnFullDay.Visible = true;
                }
                else
                {
                    rbtnHalfDay.Visible = true;
                    rbtnFullDay.Visible = true;

                }

                if (DDL_LeaveType.SelectedValue.ToString().Trim() == "2100")
                {
                    pnote.Visible = true;

                }
                else
                {
                    pnote.Visible = false;

                }

                TxtFromDate.Focus();

                //}

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void TxtFromDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "2100") || (DDL_LeaveType.SelectedValue.ToString().Trim() == "2101"))
                {
                    TxtToDate.Text = TxtFromDate.Text;
                    TxtToDate.Enabled = false;
                }
                else if ((DDL_LeaveType.SelectedValue.ToString().Trim() == "1080") || (DDL_LeaveType.SelectedValue.ToString().Trim() == "1020"))
                {
                    TxtToDate.Text = TxtFromDate.Text;
                    TxtToDate.Enabled = false;
                }
                else
                {
                    TxtToDate.Text = TxtFromDate.Text;
                    TxtToDate.Enabled = true;
                }


                DefaultRunFunction();


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void GV_LeaveOverView_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                //   Session.Add("LeaveOvrView", ObjLeaveReqLst);
                List<leaverequestbo> LeaveboList = (List<leaverequestbo>)Session["LeaveOvrView"];
                bool objSortOrder = (bool)Session["bSortedOrder"];
                switch (e.SortExpression.ToString().Trim())
                {

                    case "RowNumber":
                        if (objSortOrder)
                        {
                            if (LeaveboList != null)
                            {
                                LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                                { return (objBo1.RowNumber.Value.CompareTo(objBo2.RowNumber.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                            { return (objBo2.RowNumber.Value.CompareTo(objBo1.RowNumber.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "BEGDA":
                        if (objSortOrder)
                        {
                            if (LeaveboList != null)
                            {
                                LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                                { return (objBo1.BEGDA.Value.CompareTo(objBo2.BEGDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                            { return (objBo2.BEGDA.Value.CompareTo(objBo1.BEGDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "ENDDA":
                        if (objSortOrder)
                        {
                            if (LeaveboList != null)
                            {
                                LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                                { return (objBo1.ENDDA.Value.CompareTo(objBo2.ENDDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                            { return (objBo2.ENDDA.Value.CompareTo(objBo1.ENDDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;


                    case "ATEXT":
                        if (objSortOrder)
                        {
                            if (LeaveboList != null)
                            {
                                LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                                { return (objBo1.ATEXT.ToString().CompareTo(objBo2.ATEXT.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                            { return (objBo2.ATEXT.ToString().CompareTo(objBo1.ATEXT.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "LTYPE":
                        if (objSortOrder)
                        {
                            if (LeaveboList != null)
                            {
                                LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                                { return (objBo1.LTYPE.ToString().CompareTo(objBo2.LTYPE.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                            { return (objBo2.LTYPE.ToString().CompareTo(objBo1.LTYPE.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;


                    case "TotalDays":
                        if (objSortOrder)
                        {
                            if (LeaveboList != null)
                            {
                                LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                                { return (double.Parse(objBo1.TotalDays.ToString()).CompareTo(double.Parse(objBo2.TotalDays.ToString()))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                            { return (double.Parse(objBo2.TotalDays.ToString()).CompareTo(double.Parse(objBo1.TotalDays.ToString()))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "STATUS":
                        if (objSortOrder)
                        {
                            if (LeaveboList != null)
                            {
                                LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                                { return (objBo1.STATUS.ToString().CompareTo(objBo2.STATUS.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            LeaveboList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                            { return (objBo2.STATUS.ToString().CompareTo(objBo1.STATUS.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;






                }

                GV_LeaveOverView.DataSource = LeaveboList;
                GV_LeaveOverView.DataBind();

                Session.Add("LeaveOvrView", LeaveboList);

                DefaultRunFunction();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnShowTeamCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                GV_TeamCalendar.Columns.Clear();
                GV_TeamCalendar.DataSource = null;
                GV_TeamCalendar.DataBind();
                int _iNoOfDays = DateTime.DaysInMonth(int.Parse(DDLYears.SelectedItem.Text), int.Parse(DDLMonth.SelectedValue));

                for (int i = 0; i <= _iNoOfDays; i++)
                {
                    BoundField b = new BoundField();
                    if (i == 0)
                    {
                        b.DataField = "EMPLOYEE_NAME";
                        b.HeaderText = "Employee";
                        GV_TeamCalendar.Columns.Add(b);
                    }
                    else
                    {
                        //// Now decide the week day name.
                        DateTime dateValue = new DateTime(int.Parse(DDLYears.SelectedItem.Text), int.Parse(DDLMonth.SelectedValue), i);
                        string _strDayName = dateValue.ToString("ddd"); // Display day name for column header.
                        b.DataField = i.ToString();
                        b.HeaderText = _strDayName;
                        GV_TeamCalendar.Columns.Add(b);
                    }
                }
                Bind_GV_TeamCalendar();
                //PageLoadEvents();
                LoadCalendarsWithLeaveMarkings();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        #region Bind GV_TeamCalendar
        protected void Bind_GV_TeamCalendar()
        {
            try
            {
                leaverequestbo objLeaveRequestBo = new leaverequestbo();
                leaverequestbl objLeaveRequestBl = new leaverequestbl();
                leaverequestcollectionbo objLeaveReqLst = new leaverequestcollectionbo();
                // Need to send selected employee id. If all emps option selected then 0.
                //if (DDLEmpList.SelectedValue == "0")
                //{ objLeaveRequestBo.PERNR = "0"; }
                //else
                //{
                //   objLeaveRequestBo.PERNR = DDLEmpList.SelectedValue.ToString(); 

                //}//Convert.ToInt32(User.Identity.Name);

                if (DDLEmpList.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(DDLEmpList.SelectedValue.ToString().Trim()))
                {
                    MsgCls("Please select the value", TClbl, Color.Red);
                    DDLEmpList.Focus();
                    GV_TeamCalendar.DataSource = null;
                    GV_TeamCalendar.DataBind();
                }
                else
                {
                    MsgCls("", TClbl, Color.Transparent);

                    //else
                    //{
                    //}

                    objLeaveRequestBo.PERNR = DDLEmpList.SelectedValue.ToString();
                    // Get selected month year combination's first day date(i.e from date for range)
                    DateTime dtSlctdMonthFirstDate = new DateTime(int.Parse(DDLYears.SelectedItem.Text), int.Parse(DDLMonth.SelectedValue), 1);
                    objLeaveRequestBo.FROM_DATE = dtSlctdMonthFirstDate;
                    // Get selected month year combination's last day date(i.e to date for range)
                    DateTime dtSlctdMonthLastDate = new DateTime(int.Parse(DDLYears.SelectedItem.Text), int.Parse(DDLMonth.SelectedValue), 1).AddMonths(1).AddDays(-1);
                    objLeaveRequestBo.TO_DATE = dtSlctdMonthLastDate;
                    objLeaveReqLst = objLeaveRequestBl.Get_Team_Calendar_Leave_Markings(objLeaveRequestBo, User.Identity.Name.ToString().Trim());

                    if (objLeaveReqLst.Count != 0)
                    {
                        MsgCls(string.Empty, TClbl, Color.Transparent);
                        // This number of days decides the number of grid columns.
                        int _iNoOfDays = DateTime.DaysInMonth(int.Parse(DDLYears.SelectedItem.Text), int.Parse(DDLMonth.SelectedValue));
                        // Using the result list and no of days(columns) construct data table and bind this table to grid.
                        // get no of rows of the table - i.e list length.
                        int _iNoOfRows = objLeaveReqLst.Count;
                        DataTable tblTeamClndr = new DataTable();
                        // first create table skeleton(Columns)

                        for (int j = 0; j <= _iNoOfDays; j++)
                        {
                            if (j == 0)
                            {
                                tblTeamClndr.Columns.Add("EMPLOYEE_NAME");
                            }
                            // Display day name for column header. 
                            else
                            {
                                // Now decide the week day name.
                                DateTime dateValue = new DateTime(int.Parse(DDLYears.SelectedItem.Text), int.Parse(DDLMonth.SelectedValue), j);
                                //string _strDayName = dateValue.ToString("ddd");
                                int _iDayNo = dateValue.Day;
                                tblTeamClndr.Columns.Add(_iDayNo.ToString());
                            }
                        }

                        if (tblTeamClndr.Rows.Count == 0)
                        {
                            DataRow dr;
                            dr = tblTeamClndr.NewRow();
                            tblTeamClndr.Rows.Add(dr);
                            for (int j = 0; j <= _iNoOfDays; j++)
                            {
                                if (j == 0)
                                {
                                    tblTeamClndr.Rows[0][j] = "";
                                }
                                else
                                {
                                    tblTeamClndr.Rows[0][j] = j;
                                }
                            }
                        }

                        if (objLeaveReqLst.Count > 0)
                        {
                            var distinctRecords = objLeaveReqLst
                                            .GroupBy(c => new { c.PERNR })
                                            .Select(g => new { Qty = g.Count(), First = g.OrderBy(c => c.PERNR).First() })
                                            .Select(p => new
                                            {
                                                Id = p.First.PERNR
                                            });

                            if (distinctRecords.Count() > 0)
                            {
                                for (int i = 0; i < distinctRecords.Count(); i++)
                                {
                                    DataRow dr;
                                    dr = tblTeamClndr.NewRow();
                                    tblTeamClndr.Rows.Add(dr);
                                }
                            }

                            string strCurrentEmpID = string.Empty;
                            short index = 1;

                            //changed by shruthi for index out of bound exception
                            for (int i = 0; i < distinctRecords.Count(); i++)
                            {
                                string str = distinctRecords.ElementAt(i).Id.ToString();

                                if (strCurrentEmpID == string.Empty)
                                {
                                    strCurrentEmpID = distinctRecords.ElementAt(i).Id.ToString();
                                    var emploName = from col in objLeaveReqLst
                                                    where col.PERNR == strCurrentEmpID
                                                    select col;

                                    tblTeamClndr.Rows[index][0] = emploName.ElementAt(0).EMPLOYEE_NAME;
                                }
                                else if (strCurrentEmpID == str)
                                {
                                    //  tblTeamClndr.Rows[index][0] = ""; //changed by shruthi...from [i] to [0] -> index out of bound exception
                                }
                                else if (strCurrentEmpID != str)
                                {
                                    ++index;
                                    strCurrentEmpID = distinctRecords.ElementAt(i).Id.ToString();
                                    var emploName = from col in objLeaveReqLst
                                                    where col.PERNR == strCurrentEmpID
                                                    select col;

                                    tblTeamClndr.Rows[index][0] = emploName.ElementAt(0).EMPLOYEE_NAME;
                                }
                            }
                        }



                        GV_TeamCalendar.Visible = true;
                        //grdTeamClndr.Columns[0].Visible = true;
                        GV_TeamCalendar.DataSource = tblTeamClndr;
                        GV_TeamCalendar.DataBind();
                        GV_TeamCalendar.Visible = true;
                        // Add datatable to the session to use in paging and sorting.

                        //grdTeamClndr.Columns[0].Visible = false;
                        // first get all the cells to be marked in an array by comparing leave date and grid's first row value.
                        // grid's first row which holds date numbers.(1...31)
                        GridViewRow grdFirstRow = GV_TeamCalendar.Rows[0];

                        for (int i = 0; i < GV_TeamCalendar.Rows.Count; i++)
                        {
                            if (i != 0)
                            {
                                GridViewRow row = GV_TeamCalendar.Rows[i];
                                ChangeBackColorOfCells(row, objLeaveReqLst, grdFirstRow);
                            }
                        }
                    }

                    else
                    {
                        GV_TeamCalendar.DataSource = null;
                        GV_TeamCalendar.DataBind();
                        MsgCls("No Record Found", TClbl, Color.Red);
                        // TClbl.Text = "No Record Found";

                    }
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        }

        protected void ChangeBackColorOfCells(GridViewRow row, leaverequestcollectionbo objLst, GridViewRow grdFirstRow)
        {
            // first get all the cells to be marked in an array by comparing leave date and grid's first row value.
            try
            {
                foreach (leaverequestbo objBo in objLst)
                {
                    // Get datevalue from DATUM
                    int _iDayNo = Convert.ToDateTime(objBo.DATUM).Day;
                    for (int i = 1; i < grdFirstRow.Cells.Count; i++)
                    {
                        if (_iDayNo == i && objBo.EMPLOYEE_NAME == row.Cells[0].Text && objBo.STATUS.ToUpper() == "SENT")
                        {
                            row.Cells[i].BackColor = System.Drawing.Color.Blue;
                        }
                        if (_iDayNo == i && objBo.EMPLOYEE_NAME == row.Cells[0].Text && objBo.STATUS.ToUpper() == "DELETION REQUESTED")
                        {
                            row.Cells[i].BackColor = System.Drawing.Color.Gray;
                        }
                        if (_iDayNo == i && objBo.EMPLOYEE_NAME == row.Cells[0].Text && objBo.STATUS.ToUpper() == "APPROVED")
                        {
                            row.Cells[i].BackColor = System.Drawing.Color.Green;
                        }
                    }
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }
        #endregion

        protected void TxtToDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtFromDate.Text) && !string.IsNullOrEmpty(TxtToDate.Text))
                {
                    if ((DDL_LeaveType.SelectedValue.ToString().Trim() != "1030"))
                    {
                        if ((DateTime.Parse(TxtFromDate.Text) == DateTime.Parse(TxtToDate.Text)))
                        {
                            rbtnHalfDay.Visible = true;
                            rbtnFullDay.Visible = true;
                        }
                        else
                        {
                            rbtnFullDay.Checked = true;
                            rbtnHalfDay.Visible = false;
                            rbtnFullDay.Visible = true;
                            Pnl_2HalfDay.Visible = false;
                        }
                    }

                    else
                    {
                        rbtnHalfDay.Visible = false;
                        rbtnFullDay.Visible = true;

                    }
                }

                DefaultRunFunction();
                TxtToDate.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void TxtToTime_TextChanged(object sender, EventArgs e)
        {
            try
            {

                CalHoursCompOffAtndnce();
                DefaultRunFunction();
                TxtToTime.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void TxtFromTime_TextChanged(object sender, EventArgs e)
        {
            try
            {

                CalHoursCompOffAtndnce();
                DefaultRunFunction();
                TxtToTime.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void CalHoursCompOffAtndnce()
        {
            try
            {
                if (DDL_LeaveType.SelectedValue.ToString().Trim() == "2100")
                {
                    DateTime DtFrm = new DateTime(1900, 01, 01);
                    DateTime DtTo = new DateTime(1900, 01, 01);

                    if (DateTime.TryParse(TxtFromTime.Text, out DtFrm))
                    {
                        if (DateTime.TryParse(TxtToTime.Text, out DtTo))
                        {
                            TimeSpan ts = DtTo - DtFrm;
                            double result = ts.TotalHours;

                            if (result < 4.0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Comp off should be applied for more than four hours');", true);

                            }
                        }
                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void rbtnHalfDay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Pnl_2HalfDay.Visible = true;
                DefaultRunFunction();
                rbtn1Half.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void rbtnFullDay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Pnl_2HalfDay.Visible = false;
                DefaultRunFunction();
                DDLApprover.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void load_leavType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LTYPE", typeof(string));
            dt.Columns.Add("LID", typeof(string));

            dt.Rows.Add("1", "Leave");
            dt.Rows.Add("2", "Attendance");
            DropDownReqTyp.DataTextField = "LID";
            DropDownReqTyp.DataValueField = "LTYPE";
            DropDownReqTyp.DataSource = dt;
            DropDownReqTyp.DataBind();


        }

        protected void load_leavSubType(string leavType)
        {
            try
            {
                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = iEmpPowerMaster_Load.masterbl.Load_Attendence_abs_Types_Leave(leavType, User.Identity.Name, Session["CompCode"].ToString());
                DDL_LeaveType.DataSource = null;
                DDL_LeaveType.DataBind();
                DDL_LeaveType.DataSource = objList;
                DDL_LeaveType.DataTextField = "ATEXT";
                DDL_LeaveType.DataValueField = "AWART";
                DDL_LeaveType.DataBind();
                DDL_LeaveType.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
            }
            catch (Exception e)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + e.Message + "');", true);
            }
        }

        protected void load_Approver()
        {
            try
            {
                msassignedtomebo objAssginTMBo = new msassignedtomebo();
                objAssginTMBo.PERNR = User.Identity.Name;
                objAssginTMBo.COMMENTS= Session["CompCode"].ToString();
                msassignedtomebl objAssginTMBl = new msassignedtomebl();
                msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
                objAssginTMList = objAssginTMBl.Get_EmployeeDetails(objAssginTMBo);
                DDLApprover.DataSource = null;
                DDLApprover.DataBind();
                DDLApprover.DataSource = objAssginTMList;
                DDLApprover.DataTextField = "S_NAME";
                DDLApprover.DataValueField = "S_PERNR";
                DDLApprover.DataBind();
                // DDLApprover.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
            }
            catch (Exception e)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + e.Message + "');", true);
            }
        }

        protected void DropDownReqTyp_SelectedIndexChanged1(object sender, EventArgs e)
        {
            load_leavSubType(DropDownReqTyp.SelectedValue.ToString().Trim());
            DefaultRunFunction();
        }



        protected void Tab1_Click(object sender, EventArgs e)
        {
            HideTabs();
            LoadCalendarsWithLeaveMarkings(); 
            view1.Visible = true;
            Tab1.CssClass = "nav-link active p-2";
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            HideTabs();
            LoadCalendarsWithLeaveMarkings(); 
            view2.Visible = true;
            Tab2.CssClass = "nav-link active p-2";
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            HideTabs();
            LoadCalendarsWithLeaveMarkings(); 
            view3.Visible = true;
            Tab3.CssClass = "nav-link active p-2";
        }

        protected void Tab4_Click(object sender, EventArgs e)
        {
            HideTabs();
            LoadCalendarsWithLeaveMarkings(); 
            view4.Visible = true;
            Tab4.CssClass = "nav-link active p-2";
        }

        protected void Tab5_Click(object sender, EventArgs e)
        {
            HideTabs();
            LoadCalendarsWithLeaveMarkings(); 
            view5.Visible = true;
            Tab5.CssClass = "nav-link active p-2";
        }

        protected void HideTabs()
        {
            view1.Visible = false;
            view2.Visible = false;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = false;
            Tab1.CssClass = "nav-link  p-2";
            Tab2.CssClass = "nav-link  p-2";
            Tab3.CssClass = "nav-link  p-2";
            Tab4.CssClass = "nav-link  p-2";
            Tab5.CssClass = "nav-link  p-2";
        }

        protected void GV_SelectedDateLeaveView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DateTime Dt = new DateTime();
                leaverequestbo ObjLeaveRequestBo = new leaverequestbo();
                leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                leaverequestcollectionbo ObjLeaveReqLst = new leaverequestcollectionbo();
                int pageindex = e.NewPageIndex;
                GV_SelectedDateLeaveView.PageIndex = e.NewPageIndex;
                ObjLeaveRequestBo.PERNR = User.Identity.Name;
                ObjLeaveReqLst = ObjLeaveRequestBl.Get_Individual_Leave_Dtls(Session["CompCode"].ToString(), ObjLeaveRequestBo);

                GV_SelectedDateLeaveView.DataSource = ObjLeaveReqLst;
                GV_SelectedDateLeaveView.DataBind();
                RegisterPostBackControl();
                DefaultRunFunction();
            }
            catch (Exception ex)
            { }
        }

       
    }
}
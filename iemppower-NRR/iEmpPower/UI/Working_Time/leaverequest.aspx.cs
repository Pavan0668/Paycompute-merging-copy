using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Net;
using System.IO;
using System.Linq.Expressions;

public partial class UI_Working_Time_leaverequest : System.Web.UI.Page
{
    bool bSortedOrder = false;
    bool _bIsCalendarShown;
    bool _bIsLOShown;
    bool _bIsLeaveQuotaShown;
    bool _bIsTeamClndrShown;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Form.DefaultButton = btnEntryKey.UniqueID;

        lblMessageBoard.Text = "";
        if (!IsPostBack)
        {
            bool bIsSend = true;
            Session.Add("IsSend", bIsSend);
            btnCalendar.Text = "Hide calendar";
            _bIsCalendarShown = true;
            Session.Add("IsCalendarShown", _bIsCalendarShown);
            pnlLeaveOverview.Visible = false;
            btnLeaveOverview.Text = "Show overview of leave";
            _bIsLOShown = false;
            Session.Add("IsLOShown", _bIsLOShown);
            pnlLeaveQuota.Visible = false;
            btnLeaveQuota.Text = "Show leave quota";
            _bIsLeaveQuotaShown = false;
            Session.Add("IsLeaveQuotaShown", _bIsLeaveQuotaShown);
            pnlTeamCalendar.Visible = false;
            btnShowTeamClndr.Text = "Show team calendar";
            Session.Add("IsTeamClndrShown", _bIsTeamClndrShown);
            // Hide color code panel
            pnlColorCode.Visible = false;
            pnlCalendar.Visible = true;
            Calendar2.VisibleDate = DateTime.Today;
            Calendar1.VisibleDate = Calendar2.VisibleDate.AddMonths(-1);
            Calendar3.VisibleDate = Calendar2.VisibleDate.AddMonths(1);
            // Load all subordinates for logged in manager in show team calendar mode.
            //LoadSubordinateEmpDropDown();
            LoadLeaveTypeDrpdwn(User.Identity.Name.ToString());
            ShowLeaveQuota();
            LoadApproveByDropDown();
            btnPreviousStep.Visible = false;
            btnPreviousStep.Enabled = false;
            btnSend.Visible = false;
            btnDelete.Visible = false;
            HiddenField1.Value = "";
            Session.Add("bSortedOrder", bSortedOrder);
            lblMessageBoard.Text = "";
        }
        //From BDPDatePicker Control fetch the textbox, then add the attribute to that textbox .
        TextBox txtbdpLeaveSince = bdpLeaveSince.FindControl("TextBox") as TextBox;
        txtbdpLeaveSince.Attributes.Add("OnChange", "ClientLeaveSinceDateChanged()");
        TextBox txtbdpFromDate = bdpFromDate.FindControl("TextBox") as TextBox;
        txtbdpFromDate.ReadOnly = true;
        txtbdpFromDate.Attributes.Add("OnChange", "ClientFromDateChanged()");
        TextBox txtbdpToDate = bdpToDate.FindControl("TextBox") as TextBox;
        txtbdpToDate.ReadOnly = true;
        txtbdpToDate.Attributes.Add("OnChange", "ClientToDateChanged()");
        txtFromTime.Attributes.Add("OnChange", "ClientFromTimeChanged()");
        txtToTime.Attributes.Add("OnChange", "ClientToTimeChanged()");
        txtDuration.Attributes.Add("OnChange", "ClientDurationChanged()");
        LoadCalendarsWithLeaveMarkings();
        txtDuration.Text = HiddenField1.Value.ToString();
    }
    //Fetching approver for the loged in user
    protected void LoadApproveByDropDown()
    {
        msassignedtomebo objAssginTMBo = new msassignedtomebo();
        objAssginTMBo.PERNR = User.Identity.Name;
        msassignedtomebl objAssginTMBl = new msassignedtomebl();
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        objAssginTMList = objAssginTMBl.Get_EmployeeDetails(objAssginTMBo);

        var sLeaveTypes = (from col in objAssginTMList
                           select col).Distinct().ToList();

        drpdwnApprover.DataSource = objAssginTMList;
        drpdwnApprover.DataTextField = "S_NAME";
        drpdwnApprover.DataValueField = "S_PERNR";
        drpdwnApprover.DataBind();
    }
    //Fetching subordinate employees for the loged in user
    protected void LoadSubordinateEmpDropDown()
    {
        msassignedtomebo objAssginTMBo = new msassignedtomebo();
        objAssginTMBo.PERNR = User.Identity.Name;
        msassignedtomebl objAssginTMBl = new msassignedtomebl();
        msassignedtomecollectionbo objAssginTMList = new msassignedtomecollectionbo();
        objAssginTMList = objAssginTMBl.Get_Sub_Employees_Of_Manager(objAssginTMBo);
        drpdwnEmpList.DataSource = objAssginTMList;
        drpdwnEmpList.DataTextField = "ENAME";
        drpdwnEmpList.DataValueField = "PERNR";
        drpdwnEmpList.DataBind();
        drpdwnEmpList.Items.Insert(0, new ListItem("All employees", "0"));
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Calendar1.VisibleDate = Calendar2.VisibleDate;
        Calendar2.VisibleDate = Calendar3.VisibleDate;
        Calendar3.VisibleDate = Calendar3.VisibleDate.AddMonths(1);
        LoadCalendarsWithLeaveMarkings();
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        Calendar3.VisibleDate = Calendar2.VisibleDate;
        Calendar2.VisibleDate = Calendar1.VisibleDate;
        Calendar1.VisibleDate = Calendar1.VisibleDate.AddMonths(-1);
        LoadCalendarsWithLeaveMarkings();
    }
    protected void LoadLeaveTypeDrpdwn(string PERNR)
    {
        try
        {
            string[] LevTypeFilter = { "0420", "2010", "2020", "2030", "2040", "2050", "1400" };
            //mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Attendence_abs_Types(PERNR);

            //var sLeaveTypes = from col in objLst
            //                  where col.TC == "1"
            //                  select col;
            var LT = from i in iEmpPowerMaster_Load.masterbl.Load_Attendence_abs_Types(User.Identity.Name.ToString())
                     where !LevTypeFilter.Contains(i.AWART)
                     select i;

            drpdwnTypeOfLeave.DataSource = LT;
            drpdwnTypeOfLeave.DataTextField = "ATEXT";
            drpdwnTypeOfLeave.DataValueField = "AWART";
            drpdwnTypeOfLeave.Items.Insert(0, new ListItem("- SELECT LEAVE TYPE -", "0"));
            drpdwnTypeOfLeave.DataBind();
            //Load leave quota panel's leave type dropdown if this panel is visible.
            //if (!_bIsLeaveQuotaShown)
            //{
            //    drpdwnLeaveQuotaType.DataSource = sLeaveTypes.ToList();
            //    drpdwnLeaveQuotaType.DataTextField = "ATEXT";
            //    drpdwnLeaveQuotaType.DataValueField = "AWART";
            //    drpdwnLeaveQuotaType.DataBind();
            //    drpdwnLeaveQuotaType.Items.Insert(0, new ListItem("All types", "0"));
            //}
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
    }
    protected void btnReview_Click(object sender, EventArgs e)
    {
        cusCustom_ServerValidate();
        ViewState["LeaveTyp"] = drpdwnTypeOfLeave.SelectedValue;
        if (lblvalidation.Text == "")
        {
            btnSend.Visible = true;
            btnReview.Visible = false;
            //btnDelete.Visible = true;
            if (txtFromTime.Text.Length != 0 && txtToTime.Text.Length != 0)
            {
                txtDuration.Text = HiddenField1.Value.ToString();
            }
            bool IsTrue = true;
            DisableControls(IsTrue);
        }
    }


    protected void cusCustom_ServerValidate()
    {
        try
        {
            int iReturnValue = 0;
            DateTime? firstday = Convert.ToDateTime("01/01/0001");
            DateTime? lastday = Convert.ToDateTime("01/01/0001");
            int? count = 0;
            int noofleavestaken = 0;
            int CLdays = 0;




            leaverequestbo objLeaveRequestBo = new leaverequestbo();
            leaverequestbl objLeaveRequestBl = new leaverequestbl();
            DateTime dt1 = bdpFromDate.SelectedDate;
            DateTime dt2 = bdpToDate.SelectedDate;
            int days = (dt2 - dt1).Days;
            days = days + 1;

            objLeaveRequestBo.PERNR = User.Identity.Name;//70297;
            if (bdpFromDate.SelectedValue == null)
            {
                objLeaveRequestBo.BEGDA = Convert.ToDateTime("01/01/0001");
            }
            else
            {
                objLeaveRequestBo.BEGDA = bdpFromDate.SelectedDate;
            }
            objLeaveRequestBo.ENDDA = new DateTime() ;
            if (bdpToDate.Visible == true)
            {
                if (bdpToDate.SelectedValue == null)
                {
                    DateTime dt = DateTime.MinValue;
                    objLeaveRequestBo.ENDDA = dt; //Convert.ToDateTime("01/01/0001");
                }
                else
                {
                    objLeaveRequestBo.ENDDA = bdpToDate.SelectedDate;
                }
            }

            if (drpdwnTypeOfLeave.SelectedValue == "0")
            {
                objLeaveRequestBo.AWART = null;
            }
            else
            {
                objLeaveRequestBo.AWART = drpdwnTypeOfLeave.SelectedValue;
                objLeaveRequestBo.ATEXT = drpdwnTypeOfLeave.SelectedItem.Text.ToString();
            }

            iReturnValue = objLeaveRequestBl.Get_Noofleaves(objLeaveRequestBo, ref  firstday, ref lastday, ref count);

            if (iReturnValue == 0)
            {
                noofleavestaken = Convert.ToInt32(count);
            }


            CLdays = days + noofleavestaken;




            //newly added for validation of CL,PL & SL starts

            if (drpdwnTypeOfLeave.SelectedItem.Text == "Casual Leave" && CLdays > 3)
            {
                lblValidateTypeOfLeave.Text = "Casual Leave-Maximum 3 days allowed in a month,already " + noofleavestaken + " day's taken in this month";
                lblvalidation.Text = "Change Date";
            }

            else if (drpdwnTypeOfLeave.SelectedItem.Text == "Privilege Leave" && days < 3)
            {
                lblValidateTypeOfLeave.Text = "Privilege Leave-Minimum 3 days allowed in a month";
                lblvalidation.Text = "Change Date";
            }

            else if (drpdwnTypeOfLeave.SelectedItem.Text == "Sick Leave")
            {
                lblValidateTypeOfLeave.Text = "Sick Leave- if more than 2 days then doctor certificate to be submitted";
                lblvalidation.Text = "";
            }

            else
            {
                lblValidateTypeOfLeave.Text = "";
                lblvalidation.Text = "";
            }

            //newly added for validation of CL,PL & SL ends


            //if (e.Value.Length == 8)
            //    e.IsValid = true;
            //else
            //    e.IsValid = false;
        }
        catch (Exception ex)
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }

    }

    protected void DisableControls(bool IsTrue)
    {
        if (IsTrue)
        {
            drpdwnTypeOfLeave.Enabled = false;
            bdpFromDate.Enabled = false;
            bdpToDate.Enabled = false;
            bdpFromDate.TextBoxStyle.Width = Unit.Pixel(200);
            bdpToDate.TextBoxStyle.Width = Unit.Pixel(200);
            TextBox txtbdpFromDate = bdpFromDate.FindControl("TextBox") as TextBox;
            // txtbdpFromDate.ApplyStyle("label"); 
            txtbdpFromDate.ReadOnly = true;
            TextBox txtbdpToDate = bdpToDate.FindControl("TextBox") as TextBox;
            txtbdpToDate.ReadOnly = true;
            Image imgBDPFromDate = bdpFromDate.FindControl("Image") as Image;
            imgBDPFromDate.Visible = false;
            Image imgBDPToDate = bdpToDate.FindControl("Image") as Image;
            imgBDPToDate.Visible = false;

            txtFromTime.Enabled = false;
            txtToTime.Enabled = false;
            txtDuration.Enabled = false;
            //CCD_drpdwnTypeOfLeave.SelectedValue = ViewState["LeaveTyp"].ToString();
            drpdwnApprover.Enabled = false;
            txtNoteForApprover.Enabled = false;
            btnPreviousStep.Visible = true;
            btnPreviousStep.Enabled = true;
        }
        else
        {
            drpdwnTypeOfLeave.Enabled = true;
            bdpFromDate.Enabled = true;
            bdpToDate.Enabled = true;
            bdpFromDate.TextBoxStyle.Width = Unit.Pixel(200);
            bdpToDate.TextBoxStyle.Width = Unit.Pixel(200);
            TextBox txtbdpFromDate = bdpFromDate.FindControl("TextBox") as TextBox;
            // txtbdpFromDate.ApplyStyle("label"); 
            txtbdpFromDate.ReadOnly = true;
            TextBox txtbdpToDate = bdpToDate.FindControl("TextBox") as TextBox;
            txtbdpToDate.ReadOnly = true;
            Image imgBDPFromDate = bdpFromDate.FindControl("Image") as Image;
            imgBDPFromDate.Visible = true;
            Image imgBDPToDate = bdpToDate.FindControl("Image") as Image;
            imgBDPToDate.Visible = true;

            txtFromTime.Enabled = true;
            txtToTime.Enabled = true;
            txtDuration.Enabled = false;
            txtDuration.Text = HiddenField1.Value.ToString();
            drpdwnApprover.Enabled = true;
            txtNoteForApprover.Enabled = true;
            btnPreviousStep.Visible = true;
            btnPreviousStep.Enabled = false;
            btnSend.Visible = false;
            btnReview.Visible = true;
        }
    }
    // Call this method after update.
    protected void EnableControls()
    {
        drpdwnTypeOfLeave.Enabled = true;
        bdpFromDate.Enabled = true;
        if (drpdwnTypeOfLeave.SelectedValue != "0110")
        {
            bdpToDate.Enabled = true;
        }
        bdpFromDate.TextBoxStyle.Width = Unit.Pixel(175);
        bdpToDate.TextBoxStyle.Width = Unit.Pixel(175);
        TextBox txtbdpFromDate = bdpFromDate.FindControl("TextBox") as TextBox;
        txtbdpFromDate.ReadOnly = true;
        TextBox txtbdpToDate = bdpToDate.FindControl("TextBox") as TextBox;
        txtbdpToDate.ReadOnly = true;
        Image imgBDPFromDate = bdpFromDate.FindControl("Image") as Image;
        imgBDPFromDate.Visible = true;
        Image imgBDPToDate = bdpToDate.FindControl("Image") as Image;
        imgBDPToDate.Visible = true;
        txtFromTime.Enabled = true;
        txtToTime.Enabled = true;
        txtDuration.Enabled = true;
        drpdwnApprover.Enabled = true;
        txtNoteForApprover.Enabled = true;
    }
    protected void SendLeaveRequestDetails()
    {
        try
        {
            string strMailToList = string.Empty;
            bool? SuperVisorstatus = true;
            bool? HRStatus = true;
            string Pernr = "";
            string SuperVisorPernr = "";
            string HRPernr = "";
            string PernrEmail = "";
            string SuperVisorEmail = "";
            string HREmail = "";
            int iReturnValue = 8;
            string HRPhn = "";
            string SuperVisorPhn = "";

            bool bIsSend = (bool)Session["IsSend"];
            leaverequestbo objLeaveRequestBo = new leaverequestbo();
            leaverequestbl objLeaveRequestBl = new leaverequestbl();
            objLeaveRequestBo.PERNR = User.Identity.Name;//70297;
            if (bdpFromDate.SelectedValue == null)
            {
                objLeaveRequestBo.BEGDA = Convert.ToDateTime("01/01/0001");
            }
            else
            {
                objLeaveRequestBo.BEGDA = bdpFromDate.SelectedDate;
            }
            objLeaveRequestBo.ENDDA = new DateTime();
            if (bdpToDate.Visible == true)
            {
                if (bdpToDate.SelectedValue == null)
                {
                    DateTime dt = DateTime.MinValue;
                    objLeaveRequestBo.ENDDA = dt; //Convert.ToDateTime("01/01/0001");
                }
                else
                {
                    objLeaveRequestBo.ENDDA = bdpToDate.SelectedDate;
                }
            }
            DateTime startTime = DateTime.MinValue, endTime = DateTime.MinValue;
            if (txtFromTime.Text.Trim() == "")
            {
                objLeaveRequestBo.BEGUZ = null;
            }
            else
            {
                objLeaveRequestBo.BEGUZ = txtFromTime.Text.Trim();
                startTime = Convert.ToDateTime(objLeaveRequestBo.BEGUZ);
            }

            if (txtToTime.Text.Trim() == "")
            {
                objLeaveRequestBo.ENDUZ = null;
            }
            else
            {
                objLeaveRequestBo.ENDUZ = txtToTime.Text.Trim();
                endTime = Convert.ToDateTime(objLeaveRequestBo.ENDUZ);
            }
            if (drpdwnTypeOfLeave.SelectedValue == "0")
            {
                objLeaveRequestBo.AWART = null;
            }
            else
            {
                objLeaveRequestBo.AWART = drpdwnTypeOfLeave.SelectedValue;
                objLeaveRequestBo.ATEXT = drpdwnTypeOfLeave.SelectedItem.Text.ToString();
            }
            //-----------------------------------------------------------
            // DateTime startTime, endTime;
            //string strStartTime = txtFromTime.Text.Trim().ToString();
            //string strEndTime = txtToTime.Text.Trim().ToString();
            //startTime = Convert.ToDateTime(objLeaveRequestBo.BEGUZ);
            //endTime = Convert.ToDateTime(objLeaveRequestBo.ENDUZ);
            //var timeDiff = new TimeSpan(endTime.Ticks - startTime.Ticks);
            // txtDuration.Text = timeDiff.Hours.ToString() + ":" + timeDiff.Minutes.ToString(); //objReturnBo.STDAZ.ToString();
            //-----------------------------------------------------------
            if (txtFromTime.Text.Trim() == "" && txtToTime.Text.Trim() == "")
            {
                objLeaveRequestBo.STDAZ = HiddenField1.Value.ToString();//txtDuration.Text.Trim();
                // objLeaveRequestBo.STDAZ = txtDuration.Text;//txtDuration.Text.Trim();

            }
            else
            {
                string ss = HiddenField1.Value.ToString();
                objLeaveRequestBo.STDAZ = HiddenField1.Value.ToString();
                //txtDuration.Text = timeDiff.Hours.ToString() + ":" + timeDiff.Minutes.ToString();
                //var timeDiff = new TimeSpan(endTime.Ticks - startTime.Ticks);
                //if (timeDiff.Minutes < 10)
                //{
                //    objLeaveRequestBo.STDAZ = timeDiff.Hours.ToString() + ":" + "0" + timeDiff.Minutes.ToString();
                //} //txtDuration.Text.Trim();
                //else
                //{
                //    objLeaveRequestBo.STDAZ = timeDiff.Hours.ToString() + ":" + timeDiff.Minutes.ToString();
                //}
            }

            if (txtNoteForApprover.Text.Trim() == "")
            {
                objLeaveRequestBo.NOTE = null;
            }
            else
            {
                objLeaveRequestBo.NOTE = txtNoteForApprover.Text.Trim();
            }

            if (drpdwnApprover.SelectedValue == "0")
            {
                objLeaveRequestBo.APPROVED_BY = null;
            }
            else
            {
                objLeaveRequestBo.APPROVED_BY = drpdwnApprover.SelectedValue.ToString();
                objLeaveRequestBo.APPROVED_BY_NAME = drpdwnApprover.SelectedItem != null ? drpdwnApprover.SelectedItem.Text.ToString() : "";
            }

            if (bIsSend)
            {
                try
                {
                    iReturnValue = objLeaveRequestBl.Create_Leave_Request_Details(objLeaveRequestBo, ref  SuperVisorstatus, ref HRStatus, ref  SuperVisorPernr,
                                       ref  SuperVisorEmail, ref  HRPernr, ref  HREmail, ref Pernr, ref  PernrEmail, ref SuperVisorPhn, ref HRPhn);
                    if (iReturnValue == 1)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = GetLocalResourceObject("OutsideLeaveQuota").ToString();
                        return;
                    }
                    if (iReturnValue == 2)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = GetLocalResourceObject("LeaveQuotaExceeded").ToString();
                        return;
                    }
                    if (iReturnValue == 0)
                    {
                        LoadCalendarsWithLeaveMarkings();

                        string strSMS = PrepareSMSBody();

                        //string SupervisorEmail = b
                        ClearEntryControls();
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        btnReview.Visible = true;
                        btnSend.Visible = false;
                        if (SuperVisorstatus == true)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = GetLocalResourceObject("ApprovalSuccess").ToString() + " " + SuperVisorPernr + ". ";

                            //try
                            //{
                            //    if (SuperVisorPhn != null && SuperVisorPhn.Length > 0)
                            //    {
                            //        WebClient client = new WebClient();
                            //   //     string baseurl = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=itchamps&password=387485442&sendername=iEmpPowe&mobileno=91" + SuperVisorPhn + "&message=" + strSMS;
                            //        SuperVisorPhn = "9986889576"; strSMS ="HI";
                            //        string baseurl = "http://api.clickatell.com/http/sendmsg?api_id=3497419&user=shruthi&password=DJXfQFFUZAENGW&to=91" + SuperVisorPhn + "&message=" + strSMS;
                            //        Stream data = client.OpenRead(baseurl);
                            //        StreamReader reader = new StreamReader(data);
                            //        string s = reader.ReadToEnd();
                            //        data.Close();
                            //        reader.Close();
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    ex.Message.ToString();
                            //}
                        }
                        if (HRStatus == true)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = GetLocalResourceObject("HRSuccess").ToString() + HRPernr + ".";

                            //try
                            //{
                            //    if (HRPhn != null && HRPhn.Length > 0)
                            //    {
                            //        WebClient client = new WebClient();
                            //        string baseurl = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=itchamps&password=387485442&sendername=iEmpPowe&mobileno=91" + HRPhn + "&message=" + strSMS;
                            //        Stream data = client.OpenRead(baseurl);
                            //        StreamReader reader = new StreamReader(data);
                            //        string s = reader.ReadToEnd();
                            //        data.Close();
                            //        reader.Close();
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    ex.Message.ToString();
                            //}
                        }
                        if (SuperVisorstatus == true && HRStatus == true)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = GetLocalResourceObject("ApprovalSuccess").ToString() + " " + SuperVisorPernr + " and HR admin " + HRPernr + ".";
                        }
                        if (SuperVisorstatus == false && HRStatus == false)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = GetLocalResourceObject("SaveSuccess").ToString();
                        }
                    }
                }
                catch (Exception Ex)
                {
                    switch (Ex.Message)
                    {
                        case "-0":
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            lblMessageBoard.Text = "Outside leave quota date ";
                            break;

                        case "-01":
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            lblMessageBoard.Text = "Outside leave quota date ";
                            break;

                        case "-1":
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            lblMessageBoard.Text = "Leave quota exceeded.";
                            break;
                        default:
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
                            break;
                    }
                    if (Ex.Message.Contains("-03"))
                    {
                        lblMessageBoard.Text = string.Empty;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Leave request already exists for these dates";
                    }
                    return;
                }
            }
            else
            {
                try
                {
                    Int32 Leave_Req_ID = (Int32)Session["Leave_Req_ID"];
                    //objLeaveRequestBo.LEAVE_REQ_ID = Leave_Req_ID;
                    string approvalStatus = (string)Session["approvalStatus"];
                    // Vishwa, Please review these messages display for each kind of status
                    // We are disabling the controls in case leave is approved so that user cannot modify the approved leave req details.
                    // Should user be allowed to modify the record in case of other statuses i.e sent and deletion requested? before approval/reject.
                    // Please set the flow right.

                    iReturnValue = objLeaveRequestBl.Update_Leave_Request_Details(objLeaveRequestBo, ref  SuperVisorstatus, ref HRStatus, ref  SuperVisorPernr,
                                       ref  SuperVisorEmail, ref  HRPernr, ref  HREmail, ref Pernr, ref  PernrEmail, ref SuperVisorPhn, ref HRPhn);
                    if (iReturnValue == 1)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = GetLocalResourceObject("OutsideLeaveQuota").ToString();
                        return;
                    }
                    if (iReturnValue == 2)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = GetLocalResourceObject("LeaveQuotaExceeded").ToString();
                        return;
                    }
                    if (iReturnValue == 0)
                    {

                        txtDuration.Text = HiddenField1.Value.ToString();
                        // Don't clear entry controls and also keep the controls enabled so that
                        // user can make further chnages if he wishes.
                        EnableControls();
                        ClearEntryControls();
                        LoadCalendarsWithLeaveMarkings();

                        string strSMS = PrepareSMSBody();

                        if (SuperVisorstatus == true)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = GetLocalResourceObject("ApprovalSuccess").ToString() + " " + SuperVisorPernr + ". ";

                            //try
                            //{
                            //    if (SuperVisorPhn != null && SuperVisorPhn.Length > 0)
                            //    {
                            //        WebClient client = new WebClient();
                            //        string baseurl = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=itchamps&password=387485442&sendername=iEmpPowe&mobileno=91" + SuperVisorPhn + "&message=" + strSMS;
                            //        Stream data = client.OpenRead(baseurl);
                            //        StreamReader reader = new StreamReader(data);
                            //        string s = reader.ReadToEnd();
                            //        data.Close();
                            //        reader.Close();
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    ex.Message.ToString();
                            //}
                        }
                        if (HRStatus == true)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = GetLocalResourceObject("HRSuccess").ToString() + HRPernr + ".";

                            //try
                            //{
                            //    if (HRPhn != null && HRPhn.Length > 0)
                            //    {
                            //        WebClient client = new WebClient();
                            //        string baseurl = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=itchamps&password=387485442&sendername=iEmpPowe&mobileno=91" + HRPhn + "&message=" + strSMS;
                            //        Stream data = client.OpenRead(baseurl);
                            //        StreamReader reader = new StreamReader(data);
                            //        string s = reader.ReadToEnd();
                            //        data.Close();
                            //        reader.Close();
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    ex.Message.ToString();
                            //}
                        }
                        if (SuperVisorstatus == true && HRStatus == true)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = GetLocalResourceObject("ApprovalSuccess").ToString() + " " + SuperVisorPernr + " and HR admin " + HRPernr + ".";
                        }
                        if (SuperVisorstatus == false && HRStatus == false)
                        {
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                            lblMessageBoard.Text = GetLocalResourceObject("UpdatedSuccessfully").ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
                    return;
                }

            }
            #region SendMail
            if (iReturnValue == 0)
            {

                if (SuperVisorstatus == true)
                {
                    if (SuperVisorEmail != string.Empty && SuperVisorEmail != "" && SuperVisorEmail != null)
                    {
                        strMailToList = SuperVisorEmail;
                    }
                }
                if (HRStatus == true)
                {
                    if (HREmail != string.Empty && HREmail != "" && HREmail != null)
                    {
                        if (strMailToList.Length == 0)
                        {
                            strMailToList = HREmail;
                        }
                        else
                        {
                            strMailToList = strMailToList + "," + HREmail;
                        }
                    }
                }
                string strBody = string.Empty;

                string strEmpName = Session["EmployeeName"].ToString();


                strBody = "Leave request has been raised by employee " + strEmpName + " | " + User.Identity.Name.ToString() + ".\n\n";
                strBody = strBody + "Type of leave            :   " + objLeaveRequestBo.ATEXT + "\n";
                strBody = strBody + "From date                :   " + objLeaveRequestBo.BEGDA + "\n";
                strBody = strBody + "To date                  :   " + objLeaveRequestBo.ENDDA.ToString() + "\n";
                if (objLeaveRequestBo.BEGUZ != null)
                {
                    strBody = strBody + "From time                :   " + objLeaveRequestBo.BEGUZ.ToString() + "\n";
                }
                if (objLeaveRequestBo.ENDUZ != null)
                {
                    strBody = strBody + "To time                  :   " + objLeaveRequestBo.ENDUZ.ToString() + "\n";
                }
                if (objLeaveRequestBo.STDAZ != null)
                {
                    strBody = strBody + "Duration                 :   " + objLeaveRequestBo.STDAZ + "\n";
                }
                strBody = strBody + "Approver                 :   " + objLeaveRequestBo.APPROVED_BY_NAME + "\n";
                strBody = strBody + "Note                     :   " + objLeaveRequestBo.NOTE + "\n\n\n";
                strBody = strBody + "This is an autogenerated e-mail, hence do not reply.";
                string strSubject = string.Empty;

                if (strEmpName != null && strEmpName.Length > 0 && strEmpName != "")
                {
                    strSubject = "Leave request by " + strEmpName;
                }
                else
                {
                    strSubject = "Leave request";
                }
                if (strMailToList.Length > 0 && PernrEmail.Length > 0)
                {
                    iEmpPowerMaster_Load.masterbl.DispatchMail(strMailToList, User.Identity.Name, strSubject, PernrEmail, strBody);
                }
            }
            #endregion
        }
        catch (Exception Ex)
        { throw Ex; }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            SendLeaveRequestDetails();
        }
        catch (Exception ex)
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
        }
    }
    protected void btnLeaveOverview_Click(object sender, EventArgs e)
    {
        pnlCalendar.Visible = false;
        btnCalendar.Text = "Show calendar";
        _bIsCalendarShown = false;
        Session.Add("IsCalendarShown", _bIsCalendarShown);

        //pnlLeaveOverview.Visible = true;
        btnLeaveQuota.Text = "Show leave quota";
        pnlLeaveQuota.Visible = false;
        _bIsLeaveQuotaShown = false;
        Session.Add("IsLeaveQuotaShown", _bIsLeaveQuotaShown);

        // Hide team calendar panel.
        pnlTeamCalendar.Visible = false;
        btnShowTeamClndr.Text = "Show team calendar";
        _bIsTeamClndrShown = false;
        Session.Add("IsTeamClndrShown", _bIsTeamClndrShown);
        pnlColorCode.Visible = false;

        if (Session["IsLOShown"] != null)
        {
            bool _bIsLOShown = (bool)Session["IsLOShown"];
            if (_bIsLOShown == true)
            {
                btnLeaveOverview.Text = "Show overview of leave";
                pnlLeaveOverview.Visible = false;
                _bIsLOShown = false;
                Session.Add("IsLOShown", _bIsLOShown);
            }
            else
            {
                btnLeaveOverview.Text = "Hide overview of leave";
                pnlLeaveOverview.Visible = true;
                _bIsLOShown = true;
                Session.Add("IsLOShown", _bIsLOShown);
            }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("~/sessionout.aspx", false);
        }

    }
    protected void btnCalendar_Click(object sender, EventArgs e)
    {
        // Leave overview hiding.
        btnLeaveOverview.Text = "Show overview of leave";
        pnlLeaveOverview.Visible = false;
        _bIsLOShown = false;
        Session.Add("IsLOShown", _bIsLOShown);

        // Leave quota hiding
        btnLeaveQuota.Text = "Show leave quota";
        pnlLeaveQuota.Visible = false;
        _bIsLeaveQuotaShown = false;
        Session.Add("IsLeaveQuotaShown", _bIsLeaveQuotaShown);

        // Team calendar hiding
        btnShowTeamClndr.Text = "Show team calendar";
        pnlTeamCalendar.Visible = false;
        _bIsTeamClndrShown = false;
        Session.Add("IsTeamClndrShown", _bIsTeamClndrShown);
        pnlColorCode.Visible = false;

        bool _bIsCalendarShown = (bool)Session["IsCalendarShown"];
        if (_bIsCalendarShown == true)
        {
            btnCalendar.Text = "Show Calendar";
            pnlCalendar.Visible = false;
            _bIsCalendarShown = false;
            Session.Add("IsCalendarShown", _bIsCalendarShown);
        }
        else
        {
            btnCalendar.Text = "Hide Calendar";
            pnlCalendar.Visible = true;
            _bIsCalendarShown = true;
            Session.Add("IsCalendarShown", _bIsCalendarShown);
            grdLeaveDetails.Visible = false;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        leaverequestbl objBl = new leaverequestbl();
        leaverequestbo objBo = (leaverequestbo)Session["SelectedLeaveRecordBo"];
        // call delete method in bl to update leave request status as Deletion requested in ess.PTREQ_HEADER table.
        objBl.Update_Leave_Request_As_Deletion_Requested(objBo);
        // update calendar markings with new changes.
        LoadCalendarsWithLeaveMarkings();
        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
        string sSpervisorEmailId = (string)Session["sSpervisorEmailId"];
        lblMessageBoard.Text = GetLocalResourceObject("DeleteReqSent").ToString() + " " + drpdwnApprover.SelectedItem.Value.ToString() + " at " + sSpervisorEmailId;
        ClearEntryControls();
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        ClearEntryControls();
        leaverequestbl objBl = new leaverequestbl();
        leaverequestcollectionbo objLst = (leaverequestcollectionbo)Session["objLst"];
        leaverequestbo objBo = objLst.Find(delegate(leaverequestbo obj) { return obj.DATUM == Calendar1.SelectedDate; });
        // ObjBo is the selected record.Add this to session to use during update.
        Session.Add("SelectedLeaveRecordBo", objBo);
        var sList = (from col in objLst
                     where col.DATUM == Calendar2.SelectedDate
                     select col).ToList();
        leaverequestcollectionbo objMultipleList = new leaverequestcollectionbo();
        if (sList.Count > 1)
        {
            foreach (var vRow in sList)
            {
                leaverequestbo objBo1 = new leaverequestbo();
                objBo1.PERNR = vRow.PERNR;
                objBo1.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
                objBo1.STATUS = vRow.STATUS;
                objBo1.DATUM = vRow.DATUM;
                objBo1.AWART = vRow.AWART;
                objBo1.ATEXT = vRow.ATEXT;
                objBo1.HR_STATUS = vRow.HR_STATUS;
                objBo1.BEGDA = vRow.BEGDA;
                objBo1.ENDDA = vRow.ENDDA;
                if (objBo1.STATUS == "Approved" && (objBo1.HR_STATUS == "Approved" || objBo1.HR_STATUS == null))
                {
                    objBo1.STATUS = vRow.STATUS;
                }
                if (objBo1.STATUS == "Deletion requested" && (objBo1.HR_STATUS == "Deletion requested" || objBo1.HR_STATUS == null))
                {
                    objBo1.STATUS = vRow.STATUS;
                }
                if (objBo1.STATUS.ToUpper() == "APPROVED" && objBo1.HR_STATUS == "Review")
                {
                    objBo1.STATUS = "Sent";
                }
                if (objBo1.STATUS.ToUpper() == "APPROVED" && objBo1.HR_STATUS == null)
                {
                    objBo1.STATUS = vRow.STATUS;
                }
                if (objBo1.STATUS == "Sent" && objBo1.HR_STATUS == "Approved")
                {
                    objBo1.STATUS = "Sent";
                }
                objMultipleList.Add(objBo1);
            }
            grdCal.Visible = true;
            grdCal.Columns[0].Visible = true;
            grdCal.DataSource = objMultipleList;
            grdCal.DataBind();
            grdCal.Columns[0].Visible = false;
            Session.Add("objMultipleList", objMultipleList);
        }
        else
        {
            //ClearEntryControls();
            grdCal.Visible = false;
            grdCal.Columns[0].Visible = true;
            grdCal.DataSource = null;
            grdCal.DataBind();
            grdCal.Columns[0].Visible = false;
            if (objBo != null)
            {
                string selectedRecordStatus = objBo.STATUS;
                leaverequestbo objReturnBo = objBl.Get_Individual_Leave_Dtls(objBo);
                objReturnBo.STATUS = objBo.STATUS;
                objReturnBo.HR_STATUS = objBo.HR_STATUS;
                AssignLeaveReqDtlsToCntrls(objReturnBo);
            }
        }
        bool bIsSend = (bool)Session["IsSend"];
        // If new leave request being entered.
        if (bIsSend)
        {
            bdpFromDate.SelectedDate = Calendar1.SelectedDate;
        }
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        ClearEntryControls();
        leaverequestbl objBl = new leaverequestbl();
        leaverequestcollectionbo objLst = (leaverequestcollectionbo)Session["objLst"];
        leaverequestbo objBo = objLst.Find(delegate(leaverequestbo obj) { return obj.DATUM == Calendar2.SelectedDate; });
        // ObjBo is the selected record.Add this to session to use during update.
        Session.Add("SelectedLeaveRecordBo", objBo);
        var sList = (from col in objLst
                     where col.DATUM == Calendar2.SelectedDate
                     select col).ToList();
        leaverequestcollectionbo objMultipleList = new leaverequestcollectionbo();
        if (sList.Count > 1)
        {
            foreach (var vRow in sList)
            {
                leaverequestbo objBo1 = new leaverequestbo();
                objBo1.PERNR = vRow.PERNR.ToString();
                objBo1.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
                objBo1.STATUS = vRow.STATUS;
                objBo1.DATUM = vRow.DATUM;
                objBo1.AWART = vRow.AWART;
                objBo1.ATEXT = vRow.ATEXT;
                objBo1.HR_STATUS = vRow.HR_STATUS;
                objBo1.BEGDA = vRow.BEGDA;
                objBo1.ENDDA = vRow.ENDDA;
                if (objBo1.STATUS == "Approved" && (objBo1.HR_STATUS == "Approved" || objBo1.HR_STATUS == null))
                {
                    objBo1.STATUS = vRow.STATUS;
                }
                if (objBo1.STATUS == "Deletion requested" && (objBo1.HR_STATUS == "Deletion requested" || objBo1.HR_STATUS == null))
                {
                    objBo1.STATUS = vRow.STATUS;
                }
                if (objBo1.STATUS.ToUpper() == "APPROVED" && objBo1.HR_STATUS == "Review")
                {
                    objBo1.STATUS = "Sent";
                }
                if (objBo1.STATUS.ToUpper() == "APPROVED" && objBo1.HR_STATUS == null)
                {
                    objBo1.STATUS = vRow.STATUS;
                }
                if (objBo1.STATUS == "Sent" && objBo1.HR_STATUS == "Approved")
                {
                    objBo1.STATUS = "Sent";
                }
                objMultipleList.Add(objBo1);
            }
            grdCal.Visible = true;
            grdCal.Columns[0].Visible = true;
            grdCal.DataSource = objMultipleList.ToList().Distinct();
            grdCal.DataBind();
            grdCal.Columns[0].Visible = false;
            Session.Add("objMultipleList", objMultipleList);
        }
        else
        {
            //ClearEntryControls();
            grdCal.Visible = false;
            grdCal.Columns[0].Visible = true;
            grdCal.DataSource = null;
            grdCal.DataBind();
            grdCal.Columns[0].Visible = false;
            if (objBo != null)
            {
                string selectedRecordStatus = objBo.STATUS;
                leaverequestbo objReturnBo = objBl.Get_Individual_Leave_Dtls(objBo);
                objReturnBo.STATUS = objBo.STATUS;
                objReturnBo.HR_STATUS = objBo.HR_STATUS;
                AssignLeaveReqDtlsToCntrls(objReturnBo);
            }
        }
        //if (e.Cell.BackColor == System.Drawing.Color.White)
        //{
        //}
        bool bIsSend = (bool)Session["IsSend"];
        // If new leave request being entered.
        if (bIsSend)
        {
            bdpFromDate.SelectedDate = Calendar2.SelectedDate;
        }
    }
    protected void Calendar3_SelectionChanged(object sender, EventArgs e)
    {
        ClearEntryControls();
        leaverequestbl objBl = new leaverequestbl();
        leaverequestcollectionbo objLst = (leaverequestcollectionbo)Session["objLst"];
        leaverequestbo objBo = objLst.Find(delegate(leaverequestbo obj) { return obj.DATUM == Calendar3.SelectedDate; });
        // ObjBo is the selected record.Add this to session to use during update.
        Session.Add("SelectedLeaveRecordBo", objBo);
        var sList = (from col in objLst
                     where col.DATUM == Calendar2.SelectedDate
                     select col).ToList();
        leaverequestcollectionbo objMultipleList = new leaverequestcollectionbo();
        if (sList.Count > 1)
        {
            foreach (var vRow in sList)
            {
                leaverequestbo objBo1 = new leaverequestbo();
                objBo1.PERNR = vRow.PERNR;
                objBo1.LEAVE_REQ_ID = vRow.LEAVE_REQ_ID;
                objBo1.STATUS = vRow.STATUS;
                objBo1.DATUM = vRow.DATUM;
                objBo1.AWART = vRow.AWART;
                objBo1.ATEXT = vRow.ATEXT;
                objBo1.HR_STATUS = vRow.HR_STATUS;
                objBo1.BEGDA = vRow.BEGDA;
                objBo1.ENDDA = vRow.ENDDA;
                if (objBo1.STATUS == "Approved" && (objBo1.HR_STATUS == "Approved" || objBo1.HR_STATUS == null))
                {
                    objBo1.STATUS = vRow.STATUS;
                }
                if (objBo1.STATUS == "Deletion requested" && (objBo1.HR_STATUS == "Deletion requested" || objBo1.HR_STATUS == null))
                {
                    objBo1.STATUS = vRow.STATUS;
                }
                if (objBo1.STATUS.ToUpper() == "APPROVED" && objBo1.HR_STATUS == "Review")
                {
                    objBo1.STATUS = "Sent";
                }
                if (objBo1.STATUS.ToUpper() == "APPROVED" && objBo1.HR_STATUS == null)
                {
                    objBo1.STATUS = vRow.STATUS;
                }
                if (objBo1.STATUS == "Sent" && objBo1.HR_STATUS == "Approved")
                {
                    objBo1.STATUS = "Sent";
                }
                objMultipleList.Add(objBo1);
            }
            grdCal.Visible = true;
            grdCal.Columns[0].Visible = true;
            grdCal.DataSource = objMultipleList;
            grdCal.DataBind();
            grdCal.Columns[0].Visible = false;
            Session.Add("objMultipleList", objMultipleList);
        }
        else
        {
            //ClearEntryControls();
            grdCal.Visible = false;
            grdCal.Columns[0].Visible = true;
            grdCal.DataSource = null;
            grdCal.DataBind();
            grdCal.Columns[0].Visible = false;
            if (objBo != null)
            {
                string selectedRecordStatus = objBo.STATUS;
                leaverequestbo objReturnBo = objBl.Get_Individual_Leave_Dtls(objBo);
                objReturnBo.STATUS = objBo.STATUS;
                objReturnBo.HR_STATUS = objBo.HR_STATUS;
                AssignLeaveReqDtlsToCntrls(objReturnBo);
            }
        }
        bool bIsSend = (bool)Session["IsSend"];
        // If new leave request being entered.
        if (bIsSend)
        {
            bdpFromDate.SelectedDate = Calendar3.SelectedDate;
        }
        // Need to handle when update.

    }
    protected void AssignLeaveReqDtlsToCntrls(leaverequestbo objReturnBo)
    {
        if (objReturnBo.STATUS.ToUpper() == "APPROVED" && objReturnBo.HR_STATUS == "Approved")
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = "Selected leave request record has been approved,can not be modified ";
            //return;
            bool IsTrue = true;
            DisableControls(IsTrue);
            btnReview.Enabled = false;
            btnDelete.Visible = true;
        }
        if (objReturnBo.STATUS == "Deletion requested" && (objReturnBo.HR_STATUS == "Deletion requested" || objReturnBo.HR_STATUS == null))
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = "Selected leave request record has been sent for deletion.";

            bool IsTrue = true;
            DisableControls(IsTrue);
            btnReview.Enabled = false;
            //return;
        }
        if (objReturnBo.STATUS.ToUpper() == "APPROVED" && objReturnBo.HR_STATUS == "Review")
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = "Selected leave request record has been sent approval.";
            bool IsTrue = true;
            DisableControls(IsTrue);
            btnReview.Enabled = false;
            //return;
        }
        if (objReturnBo.STATUS.ToUpper() == "APPROVED" && objReturnBo.HR_STATUS == null)
        {
            lblMessageBoard.Text = "Selected leave request record has been approved,can not be modified ";
            //return;
            bool IsTrue = true;
            DisableControls(IsTrue);
            btnReview.Enabled = false;
            btnDelete.Visible = true;
            //return;
        }
        if (objReturnBo.STATUS == "Sent" && objReturnBo.HR_STATUS == "Approved")
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = "Selected leave request record has been sent approval.";

            bool IsTrue = true;
            DisableControls(IsTrue);
            btnReview.Enabled = false;
            //return;
        }
        Int32 leave_req_id = Convert.ToInt32(objReturnBo.LEAVE_REQ_ID);
        Session.Add("Leave_Req_ID", leave_req_id);
        string approvalStatus = objReturnBo.STATUS.ToString();
        Session.Add("approvalStatus", approvalStatus);
        drpdwnTypeOfLeave.SelectedValue = objReturnBo.AWART;
        bdpFromDate.SelectedDate = Convert.ToDateTime(objReturnBo.BEGDA);
        if (objReturnBo.AWART == "0110")
        {
            bdpToDate.Visible = false;
            lblToDate.Visible = false;
        }
        else
        {
            bdpToDate.Visible = true;
            lblToDate.Visible = true;
            bdpToDate.SelectedDate = Convert.ToDateTime(objReturnBo.ENDDA);
        }
        // if from and to dates are different then keep, 
        // from time, to time and duration fields disabled.
        if (bdpFromDate.SelectedDate != bdpToDate.SelectedDate)
        {
            txtFromTime.Enabled = false;
            txtToTime.Enabled = false;
            txtDuration.Enabled = false;
        }
        if (objReturnBo.BEGUZ != null)
        {
            txtFromTime.Text = objReturnBo.BEGUZ;
        }
        if (objReturnBo.ENDUZ != null)
        {
            txtToTime.Text = objReturnBo.ENDUZ;
        }
        if (txtFromTime.Text.Length != 0 && txtToTime.Text.Length != 0)
        {
            txtDuration.Text = objReturnBo.STDAZ;
            HiddenField1.Value = objReturnBo.STDAZ;
            //txtDuration.Enabled = false;
        }
        if (objReturnBo.BEGUZ == null && objReturnBo.ENDUZ == null && objReturnBo.STDAZ != null)
        {
            txtDuration.Text = objReturnBo.STDAZ;
        }
        drpdwnApprover.SelectedValue = objReturnBo.APPROVED_BY.ToString();
        txtNoteForApprover.Text = objReturnBo.NOTE;
        bool bIsSend = false;
        Session.Add("IsSend", bIsSend);
        // btnDelete.Visible = true;
    }
    protected void drpdwnTypeOfLeave_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtDuration.Text = HiddenField1.Value;
        if (drpdwnTypeOfLeave.SelectedValue == "0110")
        {
            lblToDate.Visible = false;
            bdpToDate.Visible = false;
            txtFromTime.Enabled = true;
            txtToTime.Enabled = true;
        }

            //newly added for validation of CL,PL & SL starts

        else if (drpdwnTypeOfLeave.SelectedItem.Text == "Casual Leave")
        {
            lblValidateTypeOfLeave.Text = "Casual Leave-Maximum 3 days allowed in a month";
        }

        else if (drpdwnTypeOfLeave.SelectedItem.Text == "Privilege Leave")
        {
            lblValidateTypeOfLeave.Text = "Privilege Leave-Minimum 3 days allowed in a month";
        }

        else if (drpdwnTypeOfLeave.SelectedItem.Text == "Sick Leave")
        {
            lblValidateTypeOfLeave.Text = "Sick Leave- if more than 2 days then doctor certificate to be submitted";
        }
        //newly added for validation of CL,PL & SL ends

        else
        {
            lblToDate.Visible = true;
            bdpToDate.Visible = true;
            lblValidateTypeOfLeave.Text = "";
        }
        SetFocus(bdpFromDate);
    }
    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        lblMessageBoard.Text = "";
        leaverequestbo objLeaveRequestBo = new leaverequestbo();
        leaverequestbl objLeaveRequestBl = new leaverequestbl();
        leaverequestcollectionbo objLeaveReqLst = new leaverequestcollectionbo();
        objLeaveRequestBo.PERNR = User.Identity.Name;

        if (bdpLeaveSince.SelectedValue == null)
        {
            objLeaveRequestBo.LEAVESINCE = null;
        }
        else
        {
            objLeaveRequestBo.LEAVESINCE = bdpLeaveSince.SelectedDate;
        }
        objLeaveReqLst = objLeaveRequestBl.Get_LeaveOverview(objLeaveRequestBo);
        Session.Add("LeaveReqList", objLeaveReqLst);

        grdLeaveDetails.Visible = true;
        grdLeaveDetails.Columns[0].Visible = true;
        grdLeaveDetails.DataSource = objLeaveReqLst;
        grdLeaveDetails.DataBind();
        grdLeaveDetails.Columns[0].Visible = false;
    }
    //This method will give first day of the selected calendar
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
    //Here we get all 3 months dates from database
    protected void LoadCalendarsWithLeaveMarkings()
    {
        DateTime dtFormGivenDate = Calendar1.VisibleDate.Date;
        DateTime dtToGivenDate = Calendar3.VisibleDate.Date;
        DateTime dtTodate, dtFromdate;

        GetFirstInMonth(dtFormGivenDate, out dtFromdate);
        LastDayOfMonth(dtToGivenDate, out dtTodate);
        leaverequestbo objBo = new leaverequestbo();
        leaverequestbl objBl = new leaverequestbl();
        leaverequestcollectionbo objLst = new leaverequestcollectionbo();
        objBo.PERNR = User.Identity.Name;
        objBo.FROM_DATE = dtFromdate;
        objBo.TO_DATE = dtTodate;
        objLst = objBl.Get_Calendar_Leave_Markings(objBo);
        Session.Add("objLst", objLst);

        Calendar1.DayRender += new DayRenderEventHandler(Calendar1_DayRender);
        Calendar2.DayRender += new DayRenderEventHandler(Calendar2_DayRender);
        Calendar3.DayRender += new DayRenderEventHandler(Calendar3_DayRender);
    }
    //Here will assign saved dates to calendar 
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        DateTime dtGivenDate = Calendar1.VisibleDate.Date;
        DateTime dtTodate, dtFromdate;

        GetFirstInMonth(dtGivenDate, out dtFromdate);
        LastDayOfMonth(dtFromdate, out dtTodate);
        leaverequestcollectionbo objLst = (leaverequestcollectionbo)Session["objLst"];
        var vOnlyFirstCal = (from col in objLst
                             where (col.DATUM) >= dtFromdate &&
                             (col.DATUM) <= dtTodate
                             select col);
        foreach (leaverequestbo objBo in vOnlyFirstCal)
        {
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT" && objBo.HR_STATUS == null)
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT" && objBo.HR_STATUS == "Review")
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT" && objBo.HR_STATUS == "Approved")
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == "Review")
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == null)
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "DELETION REQUESTED")
            {
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == "Approved")
            {
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == null)
            {
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
        }
        // Mark multiple entries
        // First get only those records that belong to this particular month from objList.
        for (int i = 0; i < objLst.Count; i++)
        {
            leaverequestbo objBoOuter = new leaverequestbo();
            objBoOuter = objLst[i];

            for (int j = i + 1; j < objLst.Count; j++)
            {
                leaverequestbo objBoInner = new leaverequestbo();
                objBoInner = objLst[j];

                if (objBoOuter.DATUM == objBoInner.DATUM)
                {
                    if (e.Day.DayNumberText.ToString() == objBoInner.DATUM.Value.Day.ToString())
                    {
                        if (Calendar1.VisibleDate.Month == objBoInner.DATUM.Value.Month)
                        {
                            if (!e.Day.IsOtherMonth)
                            {
                                e.Cell.BackColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
        }
        Session.Add("vOnlyFirstCal", vOnlyFirstCal);
    }
    //Here will assign saved dates to calendar
    protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
    {
        DateTime dtGivenDate = Calendar2.VisibleDate.Date;
        DateTime dtTodate, dtFromdate;

        GetFirstInMonth(dtGivenDate, out dtFromdate);
        LastDayOfMonth(dtFromdate, out dtTodate);
        leaverequestcollectionbo objLst = (leaverequestcollectionbo)Session["objLst"];
        var vOnlySecondCal = (from col in objLst
                              where (col.DATUM) >= dtFromdate &&
                              (col.DATUM) <= dtTodate
                              select col);
        foreach (leaverequestbo objBo in vOnlySecondCal)
        {
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT" && objBo.HR_STATUS == null)
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT" && objBo.HR_STATUS == "Review")
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT" && objBo.HR_STATUS == "Approved")
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == "Review")
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == null)
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "DELETION REQUESTED")
            {
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == "Approved")
            {
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == null)
            {
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
        }
        //Mark multiple entries
        for (int i = 0; i < objLst.Count; i++)
        {
            leaverequestbo objBoOuter = new leaverequestbo();
            objBoOuter = objLst[i];

            for (int j = i + 1; j < objLst.Count; j++)
            {
                leaverequestbo objBoInner = new leaverequestbo();
                objBoInner = objLst[j];
                if (objBoOuter.DATUM == objBoInner.DATUM)
                {
                    if (e.Day.DayNumberText.ToString() == objBoInner.DATUM.Value.Day.ToString())
                    {
                        if (Calendar2.VisibleDate.Month == objBoInner.DATUM.Value.Month)
                        {
                            if (!e.Day.IsOtherMonth)
                            {
                                e.Cell.BackColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
        }
        Session.Add("vOnlySecondCal", vOnlySecondCal);
    }
    //Here will assign saved dates to calendar
    protected void Calendar3_DayRender(object sender, DayRenderEventArgs e)
    {
        DateTime dtGivenDate = Calendar3.VisibleDate.Date;
        DateTime dtTodate, dtFromdate;

        GetFirstInMonth(dtGivenDate, out dtFromdate);
        LastDayOfMonth(dtFromdate, out dtTodate);
        leaverequestcollectionbo objLst = (leaverequestcollectionbo)Session["objLst"];
        var vOnlyLastCal = (from col in objLst
                            where (col.DATUM) >= dtFromdate &&
                            (col.DATUM) <= dtTodate
                            select col);
        foreach (leaverequestbo objBo in vOnlyLastCal)
        {
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT" && objBo.HR_STATUS == null)
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT" && objBo.HR_STATUS == "Review")
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "SENT" && objBo.HR_STATUS == "Approved")
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == "Review")
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == null)
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "DELETION REQUESTED")
            {
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == "Approved")
            {
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED" && objBo.HR_STATUS == null)
            {
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
        }
        // Mark multiple entries
        for (int i = 0; i < objLst.Count; i++)
        {
            leaverequestbo objBoOuter = new leaverequestbo();
            objBoOuter = objLst[i];

            for (int j = i + 1; j < objLst.Count; j++)
            {
                leaverequestbo objBoInner = new leaverequestbo();
                objBoInner = objLst[j];
                if (objBoOuter.DATUM == objBoInner.DATUM)
                {
                    if (e.Day.DayNumberText.ToString() == objBoInner.DATUM.Value.Day.ToString())
                    {
                        if (Calendar3.VisibleDate.Month == objBoInner.DATUM.Value.Month)
                        {
                            if (!e.Day.IsOtherMonth)
                            {
                                e.Cell.BackColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
        }
        Session.Add("vOnlyLastCal", vOnlyLastCal);
    }
    protected void ClearEntryControls()
    {
        drpdwnTypeOfLeave.SelectedIndex = 0;
        drpdwnTypeOfLeave.Enabled = true;
        bdpFromDate.SelectedValue = null;
        bdpFromDate.Enabled = true;
        bdpToDate.Visible = true;
        lblToDate.Visible = true;
        bdpToDate.SelectedValue = null;
        bdpToDate.Enabled = true; Image imgBDPFromDate = bdpFromDate.FindControl("Image") as Image;
        imgBDPFromDate.Visible = true;
        Image imgBDPToDate = bdpToDate.FindControl("Image") as Image;
        imgBDPToDate.Visible = true;
        txtFromTime.Text = "";
        txtFromTime.Enabled = true;
        txtToTime.Text = "";
        txtToTime.Enabled = true;
        txtDuration.Text = "";
        txtDuration.Enabled = true;
        //drpdwnApprover.SelectedIndex = 0;
        drpdwnApprover.Enabled = true;
        txtNoteForApprover.Text = "";
        txtNoteForApprover.Enabled = true;
        btnReview.Visible = true;
        btnReview.Enabled = true;
        btnSend.Visible = false;
        btnDelete.Visible = false;
        lblMessageBoard.Text = "";
        HiddenField1.Value = "";
        btnPreviousStep.Visible = true;
        btnPreviousStep.Enabled = false;
        bool bIsSend = true;
        Session.Add("IsSend", bIsSend);
    }
    protected void grdLeaveDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageindex = e.NewPageIndex;
        grdLeaveDetails.PageIndex = e.NewPageIndex;
        leaverequestcollectionbo objLeaveReqList = (leaverequestcollectionbo)Session["LeaveReqList"];
        grdLeaveDetails.DataSource = objLeaveReqList;
        int pagindex = Convert.ToInt32(ViewState["pageindex"]);
        grdLeaveDetails.Columns[0].Visible = true;
        grdLeaveDetails.DataSource = objLeaveReqList;
        grdLeaveDetails.SelectedIndex = -1;
        grdLeaveDetails.DataBind();
        grdLeaveDetails.Columns[0].Visible = false;
    }
    protected void grdLeaveDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        leaverequestcollectionbo objLeaveReqList = (leaverequestcollectionbo)Session["LeaveReqList"];
        bool objSortOrder = (bool)Session["bSortedOrder"];
        switch (e.SortExpression.ToString().Trim())
        {
            case "ATEXT":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.ATEXT.CompareTo(objBo2.ATEXT)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.ATEXT.CompareTo(objBo1.ATEXT)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "BEGDA":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.BEGDA.CompareTo(objBo2.BEGDA)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.BEGDA.CompareTo(objBo1.BEGDA)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "ENDDA":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.ENDDA.CompareTo(objBo2.ENDDA)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.ENDDA.CompareTo(objBo1.ENDDA)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "STATUS":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.STATUS.CompareTo(objBo2.STATUS)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.STATUS.CompareTo(objBo1.STATUS)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "KVERB":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.KVERB.CompareTo(objBo2.KVERB)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.KVERB.CompareTo(objBo1.KVERB)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
        }

        grdLeaveDetails.DataSource = objLeaveReqList;
        grdLeaveDetails.DataBind();

        Session.Add("LeaveReqList", objLeaveReqList);
    }
    protected void grdCal_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow grdRow = grdCal.SelectedRow;
        // grdRow.Cells[0]
        leaverequestbl objBl = new leaverequestbl();
        leaverequestcollectionbo objLst = (leaverequestcollectionbo)Session["objMultipleList"];
        leaverequestbo objBo = objLst.Find(delegate(leaverequestbo obj) { return obj.LEAVE_REQ_ID == Guid.Parse(grdRow.Cells[0].Text); });
        // ObjBo is the selected record.Add this to session to use during update.
        Session.Add("SelectedLeaveRecordBo", objBo);

        ClearEntryControls();
        if (objBo != null)
        {
            string selectedRecordStatus = objBo.STATUS;
            leaverequestbo objReturnBo = objBl.Get_Individual_Leave_Dtls(objBo);
            objReturnBo.HR_STATUS = objBo.HR_STATUS;
            objReturnBo.STATUS = objBo.STATUS;
            AssignLeaveReqDtlsToCntrls(objReturnBo);
        }
    }
    protected void grdCal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdCal, "Select$" + e.Row.RowIndex);
        }
    }
    protected void btnLeaveQuota_Click(object sender, EventArgs e)
    {
        // Hide calendar panel 
        pnlCalendar.Visible = false;
        btnCalendar.Text = "Show calendar";
        _bIsCalendarShown = false;
        Session.Add("IsCalendarShown", _bIsCalendarShown);

        // Hide leave overview panel.
        pnlLeaveOverview.Visible = false;
        btnLeaveOverview.Text = "Show overview of leave";
        _bIsLOShown = false;
        Session.Add("IsLOShown", _bIsLOShown);

        // Hide team calendar panel.
        pnlTeamCalendar.Visible = false;
        btnShowTeamClndr.Text = "Show team calendar";
        _bIsTeamClndrShown = false;
        Session.Add("IsTeamClndrShown", _bIsTeamClndrShown);
        pnlColorCode.Visible = false;

        if (Session["IsLeaveQuotaShown"] != null)
        {
            _bIsLeaveQuotaShown = (bool)Session["IsLeaveQuotaShown"];
            if (_bIsLeaveQuotaShown == true)
            {
                btnLeaveQuota.Text = "Show leave quota";
                pnlLeaveQuota.Visible = false;
                _bIsLeaveQuotaShown = false;
                Session.Add("IsLeaveQuotaShown", _bIsLeaveQuotaShown);
            }
            else
            {
                ShowLeaveQuota();
                btnLeaveQuota.Text = "Hide leave quota";
                pnlLeaveQuota.Visible = true;
                _bIsLeaveQuotaShown = true;
                Session.Add("IsLeaveQuotaShown", _bIsLeaveQuotaShown);
            }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("~/sessionout.aspx", false);
        }
    }

    protected void ShowLeaveQuota()
    {
        leaverequestbo objLeaveRequestBo = new leaverequestbo();
        leaverequestbl objLeaveRequestBl = new leaverequestbl();
        leaverequestcollectionbo objLeaveReqLst = new leaverequestcollectionbo();
        objLeaveRequestBo.PERNR = User.Identity.Name;

        objLeaveReqLst = objLeaveRequestBl.Get_LeaveQuota(objLeaveRequestBo);
        if (objLeaveReqLst.Count > 0)
        {
            Session.Add("LeaveQuotaList", objLeaveReqLst);
            grdLeaveQuotaDtls.Visible = true;
            grdLeaveQuotaDtls.DataSource = objLeaveReqLst;
            grdLeaveQuotaDtls.DataBind();
        }
        else
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("NoQuotaFound").ToString();
            return;
        }
    }

    protected void grdLeaveQuotaDtls_sorting(object sender, GridViewSortEventArgs e)
    {
        leaverequestcollectionbo objLeaveReqList = (leaverequestcollectionbo)Session["LeaveQuotaList"];
        bool objSortOrder = (bool)Session["bSortedOrder"];
        switch (e.SortExpression.ToString().Trim())
        {
            case "ATEXT":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.ATEXT.CompareTo(objBo2.ATEXT)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.ATEXT.CompareTo(objBo1.ATEXT)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "LEAVE_QUOTA_START_DATE":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.LEAVE_QUOTA_START_DATE.CompareTo(objBo2.LEAVE_QUOTA_START_DATE)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.LEAVE_QUOTA_START_DATE.CompareTo(objBo1.LEAVE_QUOTA_START_DATE)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "LEAVE_QUOTA_END_DATE":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.LEAVE_QUOTA_END_DATE.CompareTo(objBo2.LEAVE_QUOTA_END_DATE)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.LEAVE_QUOTA_END_DATE.CompareTo(objBo1.LEAVE_QUOTA_END_DATE)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "ANZHL":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.ANZHL.CompareTo(objBo2.ANZHL)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.ANZHL.CompareTo(objBo1.ANZHL)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "KVERB":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.KVERB.CompareTo(objBo2.KVERB)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.KVERB.CompareTo(objBo1.KVERB)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;

            case "AVAILABLE_DAYS":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.AVAILABLE_DAYS.CompareTo(objBo2.AVAILABLE_DAYS)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.AVAILABLE_DAYS.CompareTo(objBo1.AVAILABLE_DAYS)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
        }
        grdLeaveQuotaDtls.DataSource = objLeaveReqList;
        grdLeaveQuotaDtls.DataBind();
        Session.Add("LeaveQuotaList", objLeaveReqList);
    }

    protected void btnShowTeamClndr_Click(object sender, EventArgs e)
    {
        pnlCalendar.Visible = false;
        btnCalendar.Text = "Show calendar";
        _bIsCalendarShown = false;
        Session.Add("IsCalendarShown", _bIsCalendarShown);

        //pnlLeaveOverview.Visible = true;
        btnLeaveQuota.Text = "Show leave quota";
        pnlLeaveQuota.Visible = false;
        _bIsLeaveQuotaShown = false;
        Session.Add("IsLeaveQuotaShown", _bIsLeaveQuotaShown);

        // Hide leave overview panel.
        pnlLeaveOverview.Visible = false;
        btnLeaveOverview.Text = "Show overview of leave";
        _bIsLOShown = false;
        Session.Add("IsLOShown", _bIsLOShown);

        if (Session["IsTeamClndrShown"] != null)
        {
            bool _bIsTeamClndrShown = (bool)Session["IsTeamClndrShown"];
            if (_bIsTeamClndrShown == true)
            {
                btnShowTeamClndr.Text = "Show team calendar";
                pnlTeamCalendar.Visible = false;
                _bIsTeamClndrShown = false;
                Session.Add("IsTeamClndrShown", _bIsTeamClndrShown);
            }
            else
            {
                btnShowTeamClndr.Text = "Hide team calendar";
                pnlTeamCalendar.Visible = true;
                _bIsTeamClndrShown = true;
                Session.Add("IsTeamClndrShown", _bIsTeamClndrShown);
                if (grdTeamClndr.Visible == true && grdTeamClndr.Columns.Count > 0)
                {
                    pnlColorCode.Visible = true;
                }
                LoadSubordinateEmpDropDown();
            }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("~/sessionout.aspx", false);
        }
    }
    protected void LoadTeamCalendarGrid()
    {
        leaverequestbo objLeaveRequestBo = new leaverequestbo();
        leaverequestbl objLeaveRequestBl = new leaverequestbl();
        leaverequestcollectionbo objLeaveReqLst = new leaverequestcollectionbo();
        // Need to send selected employee id. If all emps option selected then 0.
        if (drpdwnEmpList.SelectedValue == "0")
        {
            objLeaveRequestBo.PERNR = "0";
        }
        else
        {
            objLeaveRequestBo.PERNR = drpdwnEmpList.SelectedValue.ToString(); //Convert.ToInt32(User.Identity.Name);
        }

        // Get selected month year combination's first day date(i.e from date for range)
        DateTime dtSlctdMonthFirstDate = new DateTime(int.Parse(drpdwnYears.SelectedValue), int.Parse(drpdwnMonths.SelectedValue), 1);
        objLeaveRequestBo.FROM_DATE = dtSlctdMonthFirstDate;
        // Get selected month year combination's last day date(i.e to date for range)
        DateTime dtSlctdMonthLastDate = new DateTime(int.Parse(drpdwnYears.SelectedValue), int.Parse(drpdwnMonths.SelectedValue), 1).AddMonths(1).AddDays(-1);
        objLeaveRequestBo.TO_DATE = dtSlctdMonthLastDate;
        objLeaveReqLst = objLeaveRequestBl.Get_Team_Calendar_Leave_Markings(objLeaveRequestBo);

        Session.Add("TeamCalendarLeaveMarkings", objLeaveReqLst);
        if (objLeaveReqLst.Count != 0)
        {
            // This number of days decides the number of grid columns.
            int _iNoOfDays = DateTime.DaysInMonth(int.Parse(drpdwnYears.SelectedValue), int.Parse(drpdwnMonths.SelectedValue));
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
                    DateTime dateValue = new DateTime(int.Parse(drpdwnYears.SelectedValue), int.Parse(drpdwnMonths.SelectedValue), j);
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
            }

            string strCurrentEmpID = string.Empty;
            short index = 1;

            for (int i = 0; i < objLeaveReqLst.Count; i++)
            {
                if (strCurrentEmpID == string.Empty)
                {
                    strCurrentEmpID = objLeaveReqLst[i].PERNR;
                    tblTeamClndr.Rows[index][0] = objLeaveReqLst[i].EMPLOYEE_NAME;
                }
                else if (strCurrentEmpID == objLeaveReqLst[i].PERNR)
                {
                    tblTeamClndr.Rows[index][i] = "";
                }
                else if (strCurrentEmpID != objLeaveReqLst[i].PERNR)
                {
                    ++index;
                    strCurrentEmpID = objLeaveReqLst[i].PERNR;
                    tblTeamClndr.Rows[index][0] = objLeaveReqLst[i].EMPLOYEE_NAME;
                }
            }

            grdTeamClndr.Visible = true;
            //grdTeamClndr.Columns[0].Visible = true;
            grdTeamClndr.DataSource = tblTeamClndr;
            grdTeamClndr.DataBind();
            pnlColorCode.Visible = true;
            // Add datatable to the session to use in paging and sorting.
            Session.Add("tblTeamClndr", tblTeamClndr);
            //grdTeamClndr.Columns[0].Visible = false;
            // first get all the cells to be marked in an array by comparing leave date and grid's first row value.
            // grid's first row which holds date numbers.(1...31)
            GridViewRow grdFirstRow = grdTeamClndr.Rows[0];

            for (int i = 0; i < grdTeamClndr.Rows.Count; i++)
            {
                if (i != 0)
                {
                    GridViewRow row = grdTeamClndr.Rows[i];
                    ChangeBackColorOfCells(row, objLeaveReqLst, grdFirstRow);
                }
            }
            LabelPnlTeamCal.ForeColor = System.Drawing.Color.White;
            LabelPnlTeamCal.Text = "Leave Details";
        }

        else
        {
            LabelPnlTeamCal.ForeColor = System.Drawing.Color.Red;
            LabelPnlTeamCal.Text = "Employee has not applied leave in this month";
        }
    }

    protected static void ChangeBackColorOfCells(GridViewRow row, leaverequestcollectionbo objLst, GridViewRow grdFirstRow)
    {
        // first get all the cells to be marked in an array by comparing leave date and grid's first row value.

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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        grdTeamClndr.Columns.Clear();
        grdTeamClndr.DataSource = null;
        grdTeamClndr.DataBind();
        pnlColorCode.Visible = false;

        // First get number of days in selected combination of month and year.
        // This number of days decides the number of grid columns.
        int _iNoOfDays = DateTime.DaysInMonth(int.Parse(drpdwnYears.SelectedValue), int.Parse(drpdwnMonths.SelectedValue));

        // Construct the grid depending on number of columns and column header text as Day name.
        for (int i = 0; i <= _iNoOfDays; i++)
        {
            BoundField b = new BoundField();
            if (i == 0)
            {
                b.DataField = "EMPLOYEE_NAME";
                b.HeaderText = "Employee";
                grdTeamClndr.Columns.Add(b);
            }
            else
            {
                //// Now decide the week day name.
                DateTime dateValue = new DateTime(int.Parse(drpdwnYears.SelectedValue), int.Parse(drpdwnMonths.SelectedValue), i);
                string _strDayName = dateValue.ToString("ddd"); // Display day name for column header.
                b.DataField = i.ToString();
                b.HeaderText = _strDayName;
                grdTeamClndr.Columns.Add(b);
            }
        }
        // After constructing the grid - Load grid with data.
        LoadTeamCalendarGrid();

    }
    protected void grdTeamClndr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageindex = e.NewPageIndex;
        grdTeamClndr.PageIndex = e.NewPageIndex;
        leaverequestcollectionbo objLeaveReqList = (leaverequestcollectionbo)Session["TeamCalendarLeaveMarkings"];
        DataTable dttblTeamClndr = (DataTable)Session["tblTeamClndr"];
        grdTeamClndr.DataSource = dttblTeamClndr;
        int pagindex = Convert.ToInt32(ViewState["pageindex"]);
        //grdLeaveDetails.Columns[0].Visible = true;
        //grdTeamClndr.DataSource = objLeaveReqList;
        grdTeamClndr.SelectedIndex = -1;

        grdTeamClndr.DataBind();
        LoadTeamCalendarGrid();
        //grdLeaveDetails.Columns[0].Visible = false;
    }
    protected void grdTeamClndr_Sorting(object sender, GridViewSortEventArgs e)
    {
        leaverequestcollectionbo objLeaveReqList = (leaverequestcollectionbo)Session["TeamCalendarLeaveMarkings"];
        bool objSortOrder = (bool)Session["bSortedOrder"];
        switch (e.SortExpression.ToString().Trim())
        {
            case "EMPLOYEE_NAME":
                if (objSortOrder)
                {
                    if (objLeaveReqList != null)
                    {
                        objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                        { return (objBo1.EMPLOYEE_NAME.CompareTo(objBo2.EMPLOYEE_NAME)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objLeaveReqList.Sort(delegate(leaverequestbo objBo1, leaverequestbo objBo2)
                    { return (objBo2.EMPLOYEE_NAME.CompareTo(objBo1.EMPLOYEE_NAME)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
        }
        grdTeamClndr.DataSource = objLeaveReqList;
        grdTeamClndr.DataBind();
        Session.Add("TeamCalendarLeaveMarkings", objLeaveReqList);
    }

    protected string PrepareSMSBody()
    {
        string strBody = string.Empty;
        string strEmployeeName = Session["EmployeeName"].ToString();

        bool bIsSend = (bool)Session["IsSend"];

        if (bIsSend)
        {
            strBody = "Leave request has been raised by " + strEmployeeName + " | " + User.Identity.Name.ToString() + ".\n\n";
        }
        else
        {
            strBody = "Leave request has been modified by " + strEmployeeName + " | " + User.Identity.Name.ToString() + ".\n\n";
        }
        strBody = strBody + " Please approve it ASAP.";

        return strBody;
    }

    private String readHtmlPage(string url)
    {
        String result = "";
        String strPost = "x=1&y=2&z=YouPostedOk";
        StreamWriter myWriter = null;

        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url.ToString());
        objRequest.Method = "POST";
        objRequest.ContentLength = strPost.Length;
        objRequest.ContentType = "application/x-www-form-urlencoded";

        try
        {
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(strPost);
            myWriter.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Make sure you are connected to internet");
            return e.Message;
        }

        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();
            Console.WriteLine(result.ToString());

            // Close and clean up the StreamReader
            sr.Close();
        }
        return result;
    }
    protected void btnPreviousStep_Click(object sender, EventArgs e)
    {

        if (btnReview.Enabled == false && btnDelete.Enabled == true)
        {
            ClearEntryControls();
        }
        else
        {
            bool IsTrue = false;
            DisableControls(IsTrue);
        }
    }


}


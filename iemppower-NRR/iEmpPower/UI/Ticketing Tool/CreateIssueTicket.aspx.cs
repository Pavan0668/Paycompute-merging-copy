using iEmpPower.Old_App_Code.iEmpPowerDAL.Ticketing_Tool;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.Ticketing_Tool
{
    public partial class CreateIssueTicket : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int result1;
                if (!int.TryParse(User.Identity.Name.ToString().Trim(), out result1) && User.Identity.Name.ToString().Trim() != "cssteam") // client
                {
                    divCbtr.Visible = false;
                }
                if (Session["TransferdTicketId"] != null)
                {
                    if (Session["TransferdValue"] != null)
                    {
                        //if (Request.QueryString["Type"].ToString().Trim() == "New")
                        if (Session["TransferdTicketId"].ToString().Trim() == "New")
                        {
                            ViewState["TicketID"] = Session["TransferdTicketId"].ToString().Trim();//Request.QueryString["Type"].ToString().Trim();
                            TicketDiv.Visible = true;
                            TaskOuterDiv.Visible = false;
                            UpdatePanel1.Visible = false;
                            ViewState["BackBtn"] = "New";
                            //PageLoadEvents(1, Request.QueryString["Type"].ToString().Trim());
                            PageLoadEvents(1, Session["TransferdTicketId"].ToString().Trim());
                            DivTicketForm.Visible = true;
                            BtnBack.Visible = true;
                            LinkBtnBack.Visible = true;
                            BtnSubmit.Visible = true;
                            BtnEdit.Visible = false;
                            BtnUpdate.Visible = false;
                            BtnCancel.Visible = false;
                            BtnRework.Visible = false;
                            int result;
                            if (int.TryParse(User.Identity.Name.ToString().Trim(), out result)) // Agent or associate managers
                            {
                                DDLIssueStatus.SelectedValue = "2";
                                DDLIssueStatus.Enabled = false;
                                divCbtr.Visible = true;
                            }
                            else // client
                            {
                                DDLIssueStatus.SelectedValue = "1";
                                DDLIssueStatus.Enabled = false;
                                divCbtr.Visible = false;

                            }

                            TxtRaisedby.Text = User.Identity.Name;
                            TxtRaisedby.Enabled = false;
                            FormTicket.Visible = false;
                            divTblTicket1.Visible = true;
                            divTblticket2.Visible = true;
                            UserAccordion.Visible = false;
                            BtnCompleted.Visible = false;
                            //BtnPending.Visible = false;
                            BtnConfirm.Visible = false;
                            BtnConfirmUAT.Visible = false;
                            BtnDeny.Visible = false;

                            TicketingToolbo TicketingObjBo = new TicketingToolbo();
                            TicketingToolbl TicketingObjBl = new TicketingToolbl();

                            string Status = "";
                            TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                            if (Status == "True") // client
                            {
                                ddlAssignee.SelectedValue = "cssteam";
                                ddlAssignee.Enabled = false;
                                divtrCategory.Visible = false;
                                divtrIssueCatCss.Visible = false;
                                //divtrIssueType.Visible = false;
                                divTrPlndhrs.Visible = false;
                                TxtPlndHrs.Enabled = false;
                            }
                            else
                            {
                                ddlAssignee.SelectedValue = "cssteam";
                                ddlAssignee.Enabled = true;
                                divtrCategory.Visible = true;
                                divtrIssueCatCss.Visible = true;
                                //divtrIssueType.Visible = true;
                                divTrPlndhrs.Visible = true;
                                TxtPlndHrs.Enabled = true;
                            }

                            LblCreateTask.Visible = false;
                            divTrActhrs.Visible = false;
                            TxtTrActhrs.Enabled = false;


                            int resulti;
                            if (int.TryParse(User.Identity.Name.ToString().Trim(), out resulti)) // Agent or associate managers
                            {
                                RFV_DDLIssueCategoryCSS.Enabled = true;
                            }
                            else
                            {
                                RFV_DDLIssueCategoryCSS.Enabled = false;
                            }
                            mp1.Hide();
                        }
                        else
                        {
                            TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                            TicketingToolbl TicketingObjBl1 = new TicketingToolbl();
                            List<TicketingToolbo> TicketingboList1 = new List<TicketingToolbo>();

                            TicketingboList1 = TicketingObjBl1.Load_Ticket(long.Parse(Session["TransferdTicketId"].ToString().Trim())); //Load_Ticket(long.Parse(Request.QueryString["Type"].ToString().Trim()));
                            ViewState["TIMERSTS"] = TicketingboList1[0].STATUS == null ? "0" : TicketingboList1[0].STATUS.ToString().Trim();
                            ViewState["CATEGORYIDSLA"] = TicketingboList1[0].CATEGORY == null ? "0" : TicketingboList1[0].CATEGORY.ToString().Trim();
                            if (ViewState["CATEGORYIDSLA"].ToString().Trim() == "2" || ViewState["CATEGORYIDSLA"].ToString().Trim() == "3")
                            {
                                if (ViewState["TIMERSTS"].ToString().Trim() != "1" && ViewState["TIMERSTS"].ToString().Trim() != "4" && ViewState["TIMERSTS"].ToString().Trim() != "7" 
                                    && ViewState["TIMERSTS"].ToString().Trim() != "8" && ViewState["TIMERSTS"].ToString().Trim() != "9" && ViewState["TIMERSTS"].ToString().Trim() != "11"
                                    && ViewState["TIMERSTS"].ToString().Trim() != "12" && ViewState["TIMERSTS"].ToString().Trim() != "13" && ViewState["TIMERSTS"].ToString().Trim() != "14")
                                {

                                    BindTimerData(Session["TransferdTicketId"].ToString().Trim());
                                    //BindTimerData(Request.QueryString["Type"].ToString().Trim());
                                    UpdatePanel1.Visible = true;
                                }
                                else
                                {
                                    UpdatePanel1.Visible = false;
                                }
                            }
                            else
                            {
                                UpdatePanel1.Visible = false;
                            }
                            if (Session["TransferdValue"].ToString().Trim() == "1")//Request.QueryString["Value"].ToString().Trim()
                            {
                                TaskOuterDiv.Visible = false;
                                TicketDiv.Visible = true;
                                LblCreateTask.Visible = true;
                                ViewState["TicketID"] = Session["TransferdTicketId"].ToString().Trim();// Request.QueryString["Type"].ToString().Trim();
                                ViewState["TypeValue"] = Session["TransferdValue"].ToString().Trim();// Request.QueryString["Value"].ToString().Trim();
                                LblCreateTask.Visible = true;
                                ViewState["BackBtn"] = "View";
                                PageLoadEvents(2, Session["TransferdTicketId"].ToString().Trim());//Request.QueryString["Type"].ToString().Trim()
                                divTblTicket1.Visible = false;
                                divTblticket2.Visible = true;
                                DivTicketForm.Visible = true;
                                BtnBack.Visible = true;
                                LinkBtnBack.Visible = true;
                                BtnSubmit.Visible = false;

                                // BtnEdit.Visible = true;
                                BtnUpdate.Visible = false;
                                BtnCancel.Visible = false;
                                BtnCompleted.Visible = false;
                                //BtnPending.Visible = false;
                                BtnConfirm.Visible = false;
                                BtnConfirmUAT.Visible = false;


                                BtnDeny.Visible = false;
                                DisableFields(false);
                                UserAccordion.Visible = true;
                                BindTicketData(Session["TransferdTicketId"].ToString().Trim());//Request.QueryString["Type"].ToString().Trim()

                                //if (ViewState["TASKSSTS"].ToString().Trim() == "TaskPending")
                                //{
                                //    BtnEdit.Visible = false;
                                //}
                                //else
                                //{
                                //    BtnEdit.Visible = true;
                                //}

                                string Status1 = "";
                                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                                TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status1);

                                if (Status1 == "True") // client
                                {
                                    if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                                    {
                                        divtrIssueCatCss.Visible = false;
                                        //divtrIssueType.Visible = false;
                                        divtrCategory.Visible = false;
                                        LblCreateTask.Visible = false;
                                        BtnEdit.Visible = true;
                                        divCbtr.Visible = false;
                                    }
                                    else if (ViewState["PrevStatusID"].ToString().Trim() != "4" && ViewState["PrevStatusID"].ToString().Trim() != "7" && ViewState["PrevStatusID"].ToString().Trim() != "13")
                                    {
                                        LblCreateTask.Visible = false;
                                        if (ViewState["PrevStatusID"].ToString().Trim() == "8" || ViewState["PrevStatusID"].ToString().Trim() == "9" || ViewState["PrevStatusID"].ToString().Trim() == "11" || ViewState["PrevStatusID"].ToString().Trim() == "12")
                                        {
                                            BtnEdit.Visible = false;
                                        }
                                        else
                                        {
                                            BtnEdit.Visible = true;
                                        }
                                        divCbtr.Visible = false;
                                    }

                                }

                                //Closed and Closed TR tickets and Cancellled tickets cannot be editted
                                if (ViewState["PrevStatusID"].ToString().Trim() == "9" || ViewState["PrevStatusID"].ToString().Trim() == "12" || ViewState["PrevStatusID"].ToString().Trim() == "11")
                                {
                                    BtnEdit.Visible = false;
                                }

                                int result;
                                if (int.TryParse(ViewState["CLIENT"].ToString().Trim(), out result))
                                {
                                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status1);

                                    if (Status1 == "True") // client
                                    {
                                        LblCreateTask.Visible = false;
                                    }
                                    else if (User.Identity.Name == "cssteam")
                                    {
                                        LblCreateTask.Visible = true;
                                        BtnTaskCreate.Visible = false;
                                    }
                                    else
                                    {
                                        msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                                        msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                                        objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
                                        msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
                                        if (objPIDashBoardLst.Count > 0)
                                        {
                                            LblCreateTask.Visible = true;
                                            BtnTaskCreate.Visible = true;

                                        }

                                        else
                                        {
                                            LblCreateTask.Visible = true;
                                            BtnTaskCreate.Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status1);

                                    if (Status1 == "True") // client
                                    {
                                        LblCreateTask.Visible = false;
                                    }
                                    else if (User.Identity.Name == "cssteam")
                                    {
                                        LblCreateTask.Visible = true;
                                        BtnTaskCreate.Visible = false;
                                    }
                                    else
                                    {
                                        msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                                        msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                                        objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
                                        msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
                                        if (objPIDashBoardLst.Count > 0)
                                        {
                                            if (string.IsNullOrEmpty(ViewState["AGENTPERNR"].ToString().Trim()) || ViewState["AGENTPERNR"].ToString().Trim() == "0")
                                            {
                                                LblCreateTask.Visible = true;
                                                BtnTaskCreate.Visible = false;
                                            }
                                            else
                                            {
                                                LblCreateTask.Visible = true;
                                                BtnTaskCreate.Visible = true;
                                            }
                                        }
                                        else
                                        {
                                            LblCreateTask.Visible = true;
                                            BtnTaskCreate.Visible = false;
                                        }
                                    }
                                }

                                TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status1);

                                if (Status1 != "True")
                                {
                                    if (ViewState["PrevStatusID"].ToString().Trim() == "4" || ViewState["PrevStatusID"].ToString().Trim() == "7" || ViewState["PrevStatusID"].ToString().Trim() == "8" || ViewState["PrevStatusID"].ToString().Trim() == "9" || ViewState["PrevStatusID"].ToString().Trim() == "12" || ViewState["PrevStatusID"].ToString().Trim() == "11")
                                    {
                                        LblCreateTask.Visible = true;
                                        BtnTaskCreate.Visible = false;
                                        TaskEdit.Visible = false;
                                    }
                                }

                                if (ViewState["PrevStatusID"].ToString().Trim() == "9")
                                {
                                    DisableFields(false);
                                    TxtComments.Enabled = true;
                                    FU_Isssue.Enabled = true;
                                }
                                //else
                                //{
                                //    DisableFields(false);
                                //}

                                if (ViewState["TIMERSTS"].ToString().Trim() == "9")
                                {
                                    if (ViewState["CLIENT"].ToString().Trim() == User.Identity.Name)
                                    {
                                        BtnRework.Visible = true;
                                    }
                                    else
                                    { BtnRework.Visible = false; }
                                }
                                else
                                {
                                    BtnRework.Visible = false;
                                }

                                TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status1);

                                if (Status1 == "True")
                                {
                                    divTrPlndhrs.Visible = false;
                                    TxtPlndHrs.Enabled = false;
                                    divTrActhrs.Visible = false;
                                    TxtTrActhrs.Enabled = false;
                                }
                                else
                                {
                                    divTrPlndhrs.Visible = true;
                                    TxtPlndHrs.Enabled = false;
                                    divTrActhrs.Visible = true;
                                    TxtTrActhrs.Enabled = false;
                                }


                            }
                            else if (Session["TransferdValue"].ToString().Trim() == "2")//Request.QueryString["Value"].ToString().Trim()
                            {
                                ViewState["TicketID"] = Session["TransferdTicketId"].ToString().Trim();// Request.QueryString["Type"].ToString().Trim();
                                ViewState["TypeValue"] = Session["TransferdValue"].ToString().Trim();// Request.QueryString["Value"].ToString().Trim();
                                TicketDiv.Visible = false;
                                LblCreateTask.Visible = false;
                                TaskOuterDiv.Visible = true;
                                LoadTaskAssignee(0, 0);
                                LoadTaskStatus(2, User.Identity.Name, long.Parse(ViewState["TicketID"].ToString().Trim()));
                                BindTaskDataofTicket(long.Parse(ViewState["TicketID"].ToString().Trim()), User.Identity.Name);
                                divTaskLoad.Visible = true;
                                TaskCreateUpdate.Visible = false;
                                AccordionTask.Visible = false;
                                ViewState["TaskBackBtn"] = "DirectView";

                                string StatusT = "";
                                TicketingToolbo TicketingObjBoT = new TicketingToolbo();
                                TicketingToolbl TicketingObjBlT = new TicketingToolbl();
                                TicketingObjBlT.CheckIfclients(1, User.Identity.Name, ref StatusT);

                                if (StatusT == "True") // client
                                {
                                    LblCreateTask.Visible = false;
                                }
                                else if (User.Identity.Name == "cssteam")
                                {
                                    LblCreateTask.Visible = true;
                                    BtnTaskCreate.Visible = false;
                                }
                                else
                                {
                                    msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                                    msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                                    objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
                                    msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
                                    if (objPIDashBoardLst.Count > 0)
                                    {

                                        LblCreateTask.Visible = true;
                                        BtnTaskCreate.Visible = true;

                                    }
                                    else
                                    {
                                        LblCreateTask.Visible = true;
                                        BtnTaskCreate.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                }
            }
            LoadfileuploadTicket();
            //LoadfileuploadTask();


            foreach (ListItem itm in DDLIssueStatus.Items)
            {
                if (itm.Value == "12")
                {
                    itm.Attributes.Add("disabled", "true");
                }
            }
        }

        public void BindTimerData(string ticketId)
        {
            try
            {

                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
                TimeSpan rtime = new TimeSpan(00, 00, 00);
                DateTime Begindatetime = DateTime.Now;
                DateTime Enddatetime = DateTime.Now;
                TicketingboList = TicketingObjBl.GetRemainingTime(long.Parse(ticketId.ToString().Trim()));

                if ((TicketingboList[0].CREATED_ON == null ? DateTime.Now.ToString() : TicketingboList[0].CREATED_ON.ToString().Trim()) != "1900-01-01 00:00:00")
                {
                    if (TicketingboList == null || TicketingboList.Count == 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('SLA not maintained!..')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('SLA not maintained!..')", true);
                        return;
                    }
                    else
                    {
                        ViewState["CREATEDONDATE"] = TicketingboList[0].CREATED_ON == null ? DateTime.Now.ToString() : TicketingboList[0].CREATED_ON.ToString().Trim();
                        ViewState["ENDDATE"] = TicketingboList[0].ENDDATE == null ? DateTime.Now.ToString() : TicketingboList[0].ENDDATE.ToString().Trim();
                        ViewState["NOOFDAYS"] = TicketingboList[0].NOOFDAYS == null ? "0" : TicketingboList[0].NOOFDAYS.ToString().Trim();

                        DateTime OUTDateTime = DateTime.Parse(ViewState["ENDDATE"].ToString());
                        DateTime INDateTime = DateTime.Now;
                        TimeSpan span = OUTDateTime - INDateTime;

                        //int hours = span.Days * 24 + span.Hours;
                        int minutes = span.Minutes < 0 ? 0 : span.Minutes;  //EDITED
                        int hours = span.Hours < 0 ? 0 : span.Hours; //EDITED
                        int days = span.Days < 0 ? 0 : span.Days; //EDITED
                        //if (Session["CountdownTimer"] == null)
                        //{
                        Session["CountdownTimer"] = new CountDownTimer(TimeSpan.Parse(days + "." + hours + ":" + minutes + ":" + "00"));
                        (Session["CountdownTimer"] as CountDownTimer).Start();
                        //}
                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        //public void LoadfileuploadTask()
        //{
        //    if (Session["fuAttachments"] == null && FU_TaskAttachments.HasFile)
        //    {
        //        Session["fuAttachments"] = FU_TaskAttachments;
        //        fuAttachmentsfname.Text = FU_TaskAttachments.FileName;
        //    }
        //    // Next time submit and Session has values but FileUpload is Blank
        //    // Return the values from session to FileUpload
        //    else if (Session["fuAttachments"] != null && (!FU_TaskAttachments.HasFile))
        //    {
        //        FU_TaskAttachments = (FileUpload)Session["fuAttachments"];
        //        fuAttachmentsfname.Text = FU_TaskAttachments.FileName;
        //    }
        //    // Now there could be another sictution when Session has File but user want to change the file
        //    // In this case we have to change the file in session object
        //    else if (FU_TaskAttachments.HasFile)
        //    {
        //        Session["fuAttachments"] = FU_TaskAttachments;
        //        fuAttachmentsfname.Text = FU_TaskAttachments.FileName;
        //    }
        //}

        public void LoadfileuploadTicket()
        {
            if (Session["fuTicketAttachments"] == null && FU_Isssue.HasFile)
            {
                Session["fuTicketAttachments"] = FU_Isssue;
                FU_IsssueName.Text = FU_Isssue.FileName;
            }
            // Next time submit and Session has values but FileUpload is Blank
            // Return the values from session to FileUpload
            else if (Session["fuTicketAttachments"] != null && (!FU_Isssue.HasFile))
            {
                FU_Isssue = (FileUpload)Session["fuTicketAttachments"];
                FU_IsssueName.Text = FU_Isssue.FileName;
            }
            // Now there could be another sictution when Session has File but user want to change the file
            // In this case we have to change the file in session object
            else if (FU_Isssue.HasFile)
            {
                Session["fuTicketAttachments"] = FU_Isssue;
                FU_IsssueName.Text = FU_Isssue.FileName;
            }
        }

        public void DisableEditBtn()
        {
            try
            {
                int result;
                if (int.TryParse(ViewState["CLIENT"].ToString().Trim(), out result))
                {
                    BtnEdit.Visible = true;
                }
                else
                {
                    msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                    msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                    objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
                    msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
                    if (objPIDashBoardLst.Count > 0)
                    {
                        if (ViewState["PrevStatusID"].ToString().Trim() == "8")
                        {
                            BtnEdit.Visible = false;
                        }
                        else
                        {
                            BtnEdit.Visible = true;
                        }
                    }
                    else
                    {
                        if (ViewState["ASSIGNEE"].ToString().Trim() == User.Identity.Name)
                        {
                            BtnEdit.Visible = true;
                        }
                        else
                        {
                            BtnEdit.Visible = false;
                        }

                    }

                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();

                    string Status = "";
                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                    if (Status == "True") // client
                    {
                        BtnEdit.Visible = true;
                    }
                    else if (User.Identity.Name == "cssteam")
                    {
                        BtnEdit.Visible = true;
                    }


                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void PageLoadEvents(int type, string ticketid)
        {
            int resulti;
            if (int.TryParse(User.Identity.Name.ToString().Trim(), out resulti)) // Agent or associate managers
            {
                RFV_DDLIssueCategoryCSS.Enabled = true;
            }
            else
            {
                RFV_DDLIssueCategoryCSS.Enabled = false;
            }
            mp1.Hide();
            try
            {
                if (type == 1)
                {
                    LoadStatus(type, User.Identity.Name, 0);
                    LoadIssueCategoryCSS(type, User.Identity.Name, 0);
                    LoadIssueType(type, User.Identity.Name, 0);
                    LoadAssignee(type);
                }

                else if (type == 2)
                {
                    LoadStatus(type, User.Identity.Name, long.Parse(ticketid.ToString().Trim()));
                    LoadIssueCategoryCSS(type, User.Identity.Name, long.Parse(ticketid.ToString().Trim()));
                    LoadIssueType(type, User.Identity.Name, long.Parse(ticketid.ToString().Trim()));
                    LoadAssignee(type);
                }
                LoadPriority();
                LoadCategory();
                //LoadAssignee();
                ClearFields();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        }

        public void DisableFields(bool status)
        {
            try
            {
                if (status == true)
                {

                    msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                    msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                    objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
                    msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
                    if (objPIDashBoardLst.Count > 0)
                    {

                        TxtUsrMailID.Enabled = false;
                        divAssindTotr.Visible = true;
                        ddlAssignee.Enabled = true;
                        TxtComments.Enabled = true;
                        FU_Isssue.Enabled = true;
                        DDLIssueCategory.Enabled = true;
                        DDLIssueStatus.Enabled = true;
                        DDLIssuePriority.Enabled = true;
                        Cb_InReviewNeed.Enabled = true;
                        DDLIssueCategoryCSS.Enabled = true;
                        DDLIssueType.Enabled = true;
                        divtrIssueCatCss.Visible = true;
                        //divtrIssueType.Visible = true;
                        divtrCategory.Visible = true;
                        divTrPlndhrs.Visible = true;
                        TxtPlndHrs.Enabled = true;
                        divTrActhrs.Visible = true;
                        TxtTrActhrs.Enabled = false;
                        TxtCCMailID.Enabled = true;
                    }
                    else
                    {
                        TicketingToolbo TicketingObjBo = new TicketingToolbo();
                        TicketingToolbl TicketingObjBl = new TicketingToolbl();

                        string Status = "";
                        TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                        if (Status == "True") // client
                        {
                            if (ViewState["PrevStatusID"].ToString().Trim() == "4" || ViewState["PrevStatusID"].ToString().Trim() == "7" || ViewState["PrevStatusID"].ToString().Trim() == "13")
                            {
                                if (User.Identity.Name == ViewState["CLIENT"].ToString().Trim())
                                {
                                    TxtUsrMailID.Enabled = true;
                                }
                                else
                                {
                                    TxtUsrMailID.Enabled = false;
                                }
                                divAssindTotr.Visible = true;
                                ddlAssignee.Enabled = false;
                                TxtComments.Enabled = true;
                                FU_Isssue.Enabled = true;
                                DDLIssueCategory.Enabled = false;
                                DDLIssueCategoryCSS.Enabled = false;
                                DDLIssueType.Enabled = true;
                                DDLIssueStatus.Enabled = false;
                                DDLIssuePriority.Enabled = false;
                                Cb_InReviewNeed.Enabled = false;
                                divtrIssueCatCss.Visible = true;
                                //divtrIssueType.Visible = true;
                                divtrCategory.Visible = true;
                                TxtCCMailID.Enabled = true;
                            }
                            else if ((ViewState["PrevStatusID"].ToString().Trim() != "1") && (ViewState["PrevStatusID"].ToString().Trim() != "4") && ViewState["PrevStatusID"].ToString().Trim() != "7" && ViewState["PrevStatusID"].ToString().Trim() != "13")
                            {
                                if (User.Identity.Name == ViewState["CLIENT"].ToString().Trim())
                                {
                                    TxtUsrMailID.Enabled = true;
                                }
                                else
                                {
                                    TxtUsrMailID.Enabled = false;
                                }
                                divAssindTotr.Visible = false;
                                ddlAssignee.Enabled = false;
                                TxtComments.Enabled = true;
                                FU_Isssue.Enabled = true;
                                DDLIssueCategory.Enabled = false;
                                DDLIssueCategoryCSS.Enabled = false;
                                DDLIssueType.Enabled = true;
                                DDLIssueStatus.Enabled = false;
                                DDLIssuePriority.Enabled = false;
                                Cb_InReviewNeed.Enabled = false;
                                divtrIssueCatCss.Visible = true;
                                //divtrIssueType.Visible = true;
                                divtrCategory.Visible = true;
                                TxtCCMailID.Enabled = true;
                            }

                            else if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                            {
                                if (User.Identity.Name == ViewState["CLIENT"].ToString().Trim())
                                {
                                    TxtUsrMailID.Enabled = true;
                                }
                                else
                                {
                                    TxtUsrMailID.Enabled = false;
                                }
                                divAssindTotr.Visible = false;
                                ddlAssignee.Enabled = false;
                                TxtComments.Enabled = true;
                                FU_Isssue.Enabled = true;
                                DDLIssueCategory.Enabled = false;
                                DDLIssueCategoryCSS.Enabled = false;
                                DDLIssueType.Enabled = true;
                                divtrIssueCatCss.Visible = false;
                                //divtrIssueType.Visible = false;
                                divtrCategory.Visible = false;
                                DDLIssueStatus.Enabled = false;
                                DDLIssuePriority.Enabled = true;
                                Cb_InReviewNeed.Enabled = false;
                                TxtCCMailID.Enabled = true;
                            }
                            divTrPlndhrs.Visible = false;
                            TxtPlndHrs.Enabled = false;
                            divTrActhrs.Visible = false;
                            TxtTrActhrs.Enabled = false;
                        }
                        else if (User.Identity.Name == "cssteam")
                        {
                            if (User.Identity.Name == ViewState["CLIENT"].ToString().Trim())
                            {
                                TxtUsrMailID.Enabled = true;
                            }
                            else
                            {
                                TxtUsrMailID.Enabled = false;
                            }
                            divAssindTotr.Visible = true;
                            ddlAssignee.Enabled = true;
                            TxtComments.Enabled = true;
                            FU_Isssue.Enabled = true;
                            DDLIssueCategory.Enabled = true;
                            DDLIssueStatus.Enabled = true;
                            DDLIssuePriority.Enabled = true;
                            Cb_InReviewNeed.Enabled = false;
                            DDLIssueCategoryCSS.Enabled = true;
                            DDLIssueType.Enabled = true;
                            divtrIssueCatCss.Visible = true;
                            //divtrIssueType.Visible = true;
                            divtrCategory.Visible = true;
                            divTrPlndhrs.Visible = true;
                            TxtPlndHrs.Enabled = false;
                            divTrActhrs.Visible = true;
                            TxtTrActhrs.Enabled = false;
                            TxtCCMailID.Enabled = true;
                        }
                        else
                        {

                            if (User.Identity.Name == ViewState["CLIENT"].ToString().Trim())
                            {
                                TxtUsrMailID.Enabled = true;
                            }
                            else
                            {
                                TxtUsrMailID.Enabled = false;
                            }
                            divAssindTotr.Visible = true;
                            ddlAssignee.Enabled = true;
                            TxtComments.Enabled = true;
                            FU_Isssue.Enabled = true;
                            DDLIssueCategory.Enabled = false;
                            DDLIssueStatus.Enabled = true;
                            DDLIssuePriority.Enabled = false;
                            Cb_InReviewNeed.Enabled = false;
                            DDLIssueCategoryCSS.Enabled = false;
                            DDLIssueType.Enabled = false;
                            divtrIssueCatCss.Visible = true;
                            //divtrIssueType.Visible = true;
                            divtrCategory.Visible = true;
                            TxtCCMailID.Enabled = true;
                            int result;
                            if (int.TryParse(ViewState["CLIENT"].ToString().Trim(), out result))
                            {
                                if (ViewState["CLIENT"].ToString().Trim() == User.Identity.Name)
                                {
                                    divTrPlndhrs.Visible = true;
                                    TxtPlndHrs.Enabled = true;
                                }
                                else
                                {
                                    divTrPlndhrs.Visible = true;
                                    TxtPlndHrs.Enabled = false;
                                }
                            }
                            else
                            {
                                divTrPlndhrs.Visible = true;
                                TxtPlndHrs.Enabled = false;
                            }

                            if (DDLIssueStatus.SelectedValue.ToString().Trim() == "6" || DDLIssueStatus.SelectedValue.ToString().Trim() == "7")
                            {
                                if (ViewState["AGENTPERNR"].ToString().Trim() == User.Identity.Name)
                                {
                                    divTrActhrs.Visible = true;
                                    TxtTrActhrs.Enabled = true;
                                }
                                else
                                {
                                    divTrActhrs.Visible = true;
                                    TxtTrActhrs.Enabled = false;
                                }
                            }
                            else
                            {
                                divTrActhrs.Visible = true;
                                TxtTrActhrs.Enabled = false;
                            }


                        }

                    }
                }
                else
                {
                    TxtUsrMailID.Enabled = false;
                    divAssindTotr.Visible = true;
                    ddlAssignee.Enabled = false;
                    TxtComments.Enabled = false;
                    FU_Isssue.Enabled = false;
                    DDLIssueCategory.Enabled = false;
                    DDLIssueCategoryCSS.Enabled = false;
                    DDLIssueType.Enabled = false;
                    DDLIssueStatus.Enabled = false;
                    DDLIssuePriority.Enabled = false;
                    Cb_InReviewNeed.Enabled = false;
                    string Status1 = "";
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();
                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status1);

                    if (Status1 == "True") // client
                    {
                        divTrPlndhrs.Visible = false;
                        TxtPlndHrs.Enabled = false;
                        divTrActhrs.Visible = false;
                        TxtTrActhrs.Enabled = false;
                    }
                    else
                    {
                        divTrPlndhrs.Visible = true;
                        TxtPlndHrs.Enabled = false;
                        divTrActhrs.Visible = true;
                        TxtTrActhrs.Enabled = false;
                    }
                    TxtCCMailID.Enabled = false;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void BindTicketData(string TicketNo)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_Ticket(long.Parse(TicketNo.ToString().Trim()));


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    MsgCls("No Records Found !", LblMsg, System.Drawing.Color.Red);
                    FormTicket.Visible = false;
                    FormTicket.DataSource = null;
                    FormTicket.DataBind();
                    return;
                }
                else
                {
                    MsgCls("", LblMsg, System.Drawing.Color.Transparent);
                    FormTicket.Visible = true;
                    FormTicket.DataSource = null;
                    FormTicket.DataBind();
                    FormTicket.DataSource = TicketingboList;
                    FormTicket.DataBind();

                    TxtUsrMailID.Text = TicketingboList[0].USRMAILID == null ? "" : TicketingboList[0].USRMAILID.ToString().Trim();
                    TxtCCMailID.Text = TicketingboList[0].CCMAILID == null ? "" : TicketingboList[0].CCMAILID.ToString().Trim();
                    DDLIssuePriority.SelectedValue = TicketingboList[0].PRIORITY == null ? "0" : TicketingboList[0].PRIORITY.ToString().Trim();
                    DDLIssueCategory.SelectedValue = TicketingboList[0].CATEGORY == null ? "0" : TicketingboList[0].CATEGORY.ToString().Trim();
                    DDLIssueCategoryCSS.SelectedValue = TicketingboList[0].ISSCATEGORYCSS == null ? "0" : TicketingboList[0].ISSCATEGORYCSS.ToString().Trim();
                    DDLIssueType.SelectedValue = TicketingboList[0].ISSTYPE == null ? "0" : TicketingboList[0].ISSTYPE.ToString().Trim();
                    DDLIssueStatus.SelectedValue = TicketingboList[0].STATUS == null ? "0" : TicketingboList[0].STATUS.ToString().Trim();
                    TxtPlndHrs.Text = TicketingboList[0].Plndhrs == null ? "0" : TicketingboList[0].Plndhrs.ToString().Trim();
                    TxtTrActhrs.Text = TicketingboList[0].Actualhrs == null ? "0" : TicketingboList[0].Actualhrs.ToString().Trim();
                    Cb_InReviewNeed.Checked = TicketingboList[0].CBINREVIEWNEEDED == "Yes" ? true : false;
                    if (!Cb_InReviewNeed.Checked)
                    {
                        LoadStatus(3, User.Identity.Name, long.Parse(ViewState["TicketID"].ToString().Trim()));
                    }
                    // ddlAssignee.SelectedValue = TicketingboList[0].ASSIGNEE == null ? "0" : TicketingboList[0].ASSIGNEE.ToString().Trim();
                    ViewState["ASSIGNEE"] = TicketingboList[0].ASSIGNEE == null ? "0" : TicketingboList[0].ASSIGNEE.ToString().Trim();
                    ViewState["PrevStatusID"] = TicketingboList[0].STATUS == null ? "0" : TicketingboList[0].STATUS.ToString().Trim();
                    ViewState["OldPriorityID"] = TicketingboList[0].PRIORITY == null ? "0" : TicketingboList[0].PRIORITY.ToString().Trim();
                    ViewState["CLIENT"] = TicketingboList[0].CLIENT == null ? "" : TicketingboList[0].CLIENT.ToString().Trim();
                    ViewState["AGENTPERNR"] = TicketingboList[0].AGENT == null ? "" : TicketingboList[0].AGENT.ToString().Trim();
                    ViewState["TASKSSTS"] = TicketingboList[0].TASKSSTS == null ? "" : TicketingboList[0].TASKSSTS.ToString().Trim();
                    ViewState["CbInReviewNeed"] = TicketingboList[0].CBINREVIEWNEEDED == null ? "No" : TicketingboList[0].CBINREVIEWNEEDED.ToString().Trim();
                    LoadTicketAttachments(long.Parse(TicketNo.ToString().Trim()));
                    LoadTicketStatus(long.Parse(TicketNo.ToString().Trim()));
                    LoadTicketComments(long.Parse(TicketNo.ToString().Trim()));

                    DisableEditBtn();

                    TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                    string Status1 = "";
                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status1);

                    if (Status1 == "True") // client
                    {
                        if (ViewState["PrevStatusID"].ToString().Trim() == "4" || ViewState["PrevStatusID"].ToString().Trim() == "7" || ViewState["PrevStatusID"].ToString().Trim() == "13")
                        {
                            divAssindTotr.Visible = true;
                            ddlAssignee.Enabled = false;
                        }
                        else
                        {
                            divAssindTotr.Visible = false;
                            ddlAssignee.Enabled = false;
                        }
                    }
                    else
                    {
                        divAssindTotr.Visible = true;
                        //DDLIssueStatus.Enabled = true;
                    }

                    string Status = "";
                    msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                    msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                    objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
                    msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);

                    if (ViewState["PrevStatusID"].ToString().Trim() == "4" || ViewState["PrevStatusID"].ToString().Trim() == "7")
                    {
                        TicketingObjBl1.CheckIfclients(1, User.Identity.Name, ref Status);

                        if (Status == "True") // client
                        {
                            ddlAssignee.SelectedValue = TicketingboList[0].AGENT == null ? "0" : TicketingboList[0].AGENT.ToString().Trim();
                            BtnBack.Visible = true;
                            LinkBtnBack.Visible = true;
                            BtnSubmit.Visible = false;
                            BtnEdit.Visible = false;
                            BtnUpdate.Visible = false;
                            BtnCancel.Visible = false;
                            //BtnPending.Visible = false;
                            BtnCompleted.Visible = false;
                            //BtnConfirm.Visible = true;

                            if (ViewState["PrevStatusID"].ToString().Trim() == "4")
                            {
                                BtnConfirm.Visible = false;
                                BtnConfirmUAT.Visible = true;
                            }
                            else
                            {
                                BtnConfirm.Visible = true;
                                BtnConfirmUAT.Visible = false;
                            }


                            BtnDeny.Visible = true;
                            DisableFields(true);
                        }

                        else if (User.Identity.Name == "cssteam")
                        {
                            ddlAssignee.SelectedValue = TicketingboList[0].ASSIGNEE == null ? "0" : TicketingboList[0].ASSIGNEE.ToString().Trim();
                            BtnBack.Visible = true;
                            LinkBtnBack.Visible = true;
                            BtnSubmit.Visible = false;
                            BtnEdit.Visible = true;
                            BtnUpdate.Visible = false;
                            BtnCancel.Visible = false;
                            //BtnPending.Visible = false;
                            BtnCompleted.Visible = false;
                            BtnConfirm.Visible = false;
                            BtnConfirmUAT.Visible = false;
                            BtnDeny.Visible = false;
                            DisableFields(false);
                        }


                        else if (objPIDashBoardLst.Count > 0) // Associate Managers
                        {
                            ddlAssignee.SelectedValue = TicketingboList[0].ASSIGNEE == null ? "0" : TicketingboList[0].ASSIGNEE.ToString().Trim();
                            BtnBack.Visible = true;
                            LinkBtnBack.Visible = true;
                            BtnSubmit.Visible = false;
                            BtnEdit.Visible = true;
                            BtnUpdate.Visible = false;
                            BtnCancel.Visible = false;
                            //BtnPending.Visible = false;
                            BtnCompleted.Visible = false;
                            BtnConfirm.Visible = false;
                            BtnConfirmUAT.Visible = false;
                            BtnDeny.Visible = false;
                            DisableFields(false);
                        }


                        else
                        {
                            ddlAssignee.SelectedValue = TicketingboList[0].ASSIGNEE == null ? "0" : TicketingboList[0].ASSIGNEE.ToString().Trim();
                            BtnBack.Visible = true;
                            LinkBtnBack.Visible = true;
                            BtnSubmit.Visible = false;

                            int result;
                            if (int.TryParse(ViewState["CLIENT"].ToString().Trim(), out result))
                            {
                                if (ddlAssignee.SelectedValue.ToString().Trim() == User.Identity.Name)
                                {
                                    BtnEdit.Visible = true;
                                }
                                else
                                {
                                    BtnEdit.Visible = false;
                                }
                            }
                            else
                            {
                                BtnEdit.Visible = false;
                            }

                            //BtnEdit.Visible = false;

                            BtnUpdate.Visible = false;
                            BtnCancel.Visible = false;
                            //BtnPending.Visible = false;
                            BtnCompleted.Visible = false;
                            BtnConfirm.Visible = false;
                            BtnConfirmUAT.Visible = false;
                            BtnDeny.Visible = false;
                            DisableFields(false);
                        }
                    }
                    else if (ViewState["PrevStatusID"].ToString().Trim() == "13")
                    {

                        TicketingObjBl1.CheckIfclients(1, User.Identity.Name, ref Status);

                        if (Status == "True") // client
                        {
                            ddlAssignee.SelectedValue = (TicketingboList[0].AGENT == null || string.IsNullOrEmpty(TicketingboList[0].AGENT)) ? (TicketingboList[0].LASTMODIFIED_BY == null || string.IsNullOrEmpty(TicketingboList[0].LASTMODIFIED_BY)) ? "0" : TicketingboList[0].LASTMODIFIED_BY.ToString().Trim() : TicketingboList[0].AGENT.ToString().Trim();
                            BtnBack.Visible = true;
                            LinkBtnBack.Visible = true;
                            BtnSubmit.Visible = false;
                            BtnEdit.Visible = false;
                            BtnUpdate.Visible = false;
                            BtnCancel.Visible = false;
                            //BtnPending.Visible = true;
                            BtnCompleted.Visible = true;
                            BtnConfirmUAT.Visible = false;
                            BtnConfirm.Visible = false;
                            BtnDeny.Visible = false;
                            DisableFields(true);
                        }

                        else if (User.Identity.Name == "cssteam")
                        {
                            ddlAssignee.SelectedValue = TicketingboList[0].ASSIGNEE == null ? "0" : TicketingboList[0].ASSIGNEE.ToString().Trim();
                            BtnBack.Visible = true;
                            LinkBtnBack.Visible = true;
                            BtnSubmit.Visible = false;
                            BtnEdit.Visible = true;
                            BtnUpdate.Visible = false;
                            BtnCancel.Visible = false;
                            //BtnPending.Visible = false;
                            BtnCompleted.Visible = false;
                            BtnConfirm.Visible = false;
                            BtnConfirmUAT.Visible = false;
                            BtnDeny.Visible = false;
                            DisableFields(false);
                        }


                        else if (objPIDashBoardLst.Count > 0) // Associate Managers
                        {
                            ddlAssignee.SelectedValue = TicketingboList[0].ASSIGNEE == null ? "0" : TicketingboList[0].ASSIGNEE.ToString().Trim();
                            BtnBack.Visible = true;
                            LinkBtnBack.Visible = true;
                            BtnSubmit.Visible = false;
                            BtnEdit.Visible = true;
                            BtnUpdate.Visible = false;
                            BtnCancel.Visible = false;
                            //BtnPending.Visible = false;
                            BtnCompleted.Visible = false;
                            BtnConfirm.Visible = false;
                            BtnConfirmUAT.Visible = false;
                            BtnDeny.Visible = false;
                            DisableFields(false);
                        }


                        else
                        {
                            ddlAssignee.SelectedValue = TicketingboList[0].ASSIGNEE == null ? "0" : TicketingboList[0].ASSIGNEE.ToString().Trim();
                            BtnBack.Visible = true;
                            LinkBtnBack.Visible = true;
                            BtnSubmit.Visible = false;
                            int result;
                            if (int.TryParse(ViewState["CLIENT"].ToString().Trim(), out result))
                            {
                                if (ddlAssignee.SelectedValue.ToString().Trim() == User.Identity.Name)
                                {
                                    BtnEdit.Visible = true;
                                }
                                else
                                {
                                    BtnEdit.Visible = false;
                                }
                            }
                            else
                            {
                                BtnEdit.Visible = false;
                            }

                            //BtnEdit.Visible = false;
                            BtnUpdate.Visible = false;
                            BtnCancel.Visible = false;
                            //BtnPending.Visible = false;
                            BtnCompleted.Visible = false;
                            BtnConfirm.Visible = false;
                            BtnConfirmUAT.Visible = false;
                            BtnDeny.Visible = false;
                            DisableFields(false);
                        }


                    }

                    RTID.Text = TicketNo;

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void LoadTicketComments(long TicketID)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_Ticket_Comments(TicketID, User.Identity.Name);


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    grdTicketsComments.Visible = false;
                    grdTicketsComments.DataSource = null;
                    grdTicketsComments.DataBind();
                    return;
                }
                else
                {
                    grdTicketsComments.Visible = true;
                    grdTicketsComments.DataSource = null;
                    grdTicketsComments.DataBind();
                    grdTicketsComments.DataSource = TicketingboList;
                    grdTicketsComments.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void LoadTicketStatus(long TicketID)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_Ticket_Status(TicketID);


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    GrdTicketStatus.Visible = false;
                    GrdTicketStatus.DataSource = null;
                    GrdTicketStatus.DataBind();
                    return;
                }
                else
                {
                    GrdTicketStatus.Visible = true;
                    GrdTicketStatus.DataSource = null;
                    GrdTicketStatus.DataBind();
                    GrdTicketStatus.DataSource = TicketingboList;
                    GrdTicketStatus.DataBind();
                }


                string Status1 = "";
                TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status1);

                if (Status1 == "True") // client
                {
                    AccordionPane3.Visible = false;
                }
                else
                {
                    AccordionPane3.Visible = true;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void LoadTicketAttachments(long TicketID)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_Ticket_Attachments(TicketID, User.Identity.Name);


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    GrdTicketsAttachments.Visible = false;
                    GrdTicketsAttachments.DataSource = null;
                    GrdTicketsAttachments.DataBind();
                    return;
                }
                else
                {
                    GrdTicketsAttachments.Visible = true;
                    GrdTicketsAttachments.DataSource = null;
                    GrdTicketsAttachments.DataBind();
                    GrdTicketsAttachments.DataSource = TicketingboList;
                    GrdTicketsAttachments.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
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

        public void ClearFields()
        {
            if (FU_Isssue != null)
            {
                if (FU_Isssue.HasFile)
                {
                    FU_Isssue.Attributes.Clear();
                }
            }
            Session["fuTicketAttachments"] = null;
            FU_IsssueName.Text = string.Empty;
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            backfunction();
        }

        protected void LinkBtnBack_Click(object sender, EventArgs e)
        {
            backfunction();
        }

        protected void backfunction()
        {
            if (ViewState["BackBtn"].ToString().Trim() == "New" || ViewState["BackBtn"].ToString().Trim() == "View")
            {
                Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
            }

            else if (ViewState["BackBtn"].ToString().Trim() == "Edit")
            {
                ViewState["BackBtn"] = "View";
                divTblTicket1.Visible = false;
                divTblticket2.Visible = true;
                DivTicketForm.Visible = true;
                BtnBack.Visible = true;
                LinkBtnBack.Visible = true;
                BtnSubmit.Visible = false;
                BtnEdit.Visible = true;
                BtnUpdate.Visible = false;
                BtnCancel.Visible = false;
                BtnCompleted.Visible = false;
                //BtnPending.Visible = false;
                BtnConfirm.Visible = false;
                BtnConfirmUAT.Visible = false;
                BtnDeny.Visible = false;
                ClearFields();
                RFV_TxtUsrMailID.Text = "";
                ddlAssignee.SelectedValue = "0";
                DDLIssuePriority.SelectedValue = "0";
                DDLIssueCategory.SelectedValue = "0";
                DDLIssueCategoryCSS.SelectedValue = "0";
                DDLIssueType.SelectedValue = "0";
                TxtComments.Text = "";
                DDLIssueStatus.SelectedValue = "0";
                DisableFields(false);
                UserAccordion.Visible = true;
                BindTicketData(ViewState["TicketID"].ToString().Trim());
            }
        }

        protected void LoadPriority()
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_Priority();
            DDLIssuePriority.DataSource = objLst;
            DDLIssuePriority.DataTextField = "PriorityTxt";
            DDLIssuePriority.DataValueField = "PriorityID";
            DDLIssuePriority.DataBind();
            DDLIssuePriority.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
        }

        protected void LoadCategory()
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_Category();
            DDLIssueCategory.DataSource = objLst;
            DDLIssueCategory.DataTextField = "CategoryTxt";
            DDLIssueCategory.DataValueField = "CategoryID";
            DDLIssueCategory.DataBind();
            DDLIssueCategory.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

        }

        protected void LoadIssueCategoryCSS(int type, string pernr, long ticketid)
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_IssueCategoryCSS(type, pernr, ticketid);
            DDLIssueCategoryCSS.DataSource = objLst;
            DDLIssueCategoryCSS.DataTextField = "IssueCategoryTxt";
            DDLIssueCategoryCSS.DataValueField = "IssueCategoryID";
            DDLIssueCategoryCSS.DataBind();
            DDLIssueCategoryCSS.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

        }

        protected void LoadIssueType(int type, string pernr, long ticketid)
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_IssueType(type, pernr, ticketid);
            DDLIssueType.DataSource = objLst;
            DDLIssueType.DataTextField = "IssueTypeTxt";
            DDLIssueType.DataValueField = "IssueTypeID";
            DDLIssueType.DataBind();
            DDLIssueType.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

        }

        protected void LoadAssignee(int type)
        {

            TicketingToolCollectionbo objLst = TicketingToolbl.GetTickety_Load_Employee_Names(User.Identity.Name, long.Parse(ViewState["TicketID"].ToString().Trim() == "New" ? "0" : ViewState["TicketID"].ToString().Trim()), type);
            ddlAssignee.DataSource = objLst;
            ddlAssignee.DataTextField = "EMPLOYEE_NONAME";
            ddlAssignee.DataValueField = "EMPLOYEE_NO";
            ddlAssignee.DataBind();
            ddlAssignee.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

        }

        protected void LoadTaskAssignee(long ticketid, long taskid)
        {
            DDLTaskAssignee.DataSource = null;
            DDLTaskAssignee.DataBind();
            TicketingToolCollectionbo objLst = TicketingToolbl.GetTickety_Load_TaskEmployee_Names(User.Identity.Name, ticketid, taskid, 3);
            DDLTaskAssignee.DataSource = objLst;
            DDLTaskAssignee.DataTextField = "EMPLOYEE_NONAME";
            DDLTaskAssignee.DataValueField = "EMPLOYEE_NO";
            DDLTaskAssignee.DataBind();
            DDLTaskAssignee.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

        }

        protected void LoadStatus(int type, string pernr, long ticketid)
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_Status(type, pernr, ticketid);
            DDLIssueStatus.DataSource = objLst;
            DDLIssueStatus.DataTextField = "StatusTxt";
            DDLIssueStatus.DataValueField = "StatusID";
            DDLIssueStatus.DataBind();
            DDLIssueStatus.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

            foreach (ListItem itm in DDLIssueStatus.Items)
            {
                if (itm.Value == "12")
                {
                    itm.Attributes.Add("disabled", "true");
                }
            }
        }

        protected void LoadTaskStatus(int type, string pernr, long ticketid)
        {
            TicketingToolCollectionbo objLst = TicketingToolbl.Load_StatusForTask(type, pernr, ticketid);
            DDLTaskStatus.DataSource = objLst;
            DDLTaskStatus.DataTextField = "StatusTxt";
            DDLTaskStatus.DataValueField = "StatusID";
            DDLTaskStatus.DataBind();
            DDLTaskStatus.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                long? TicketRefIdOut = 0;
                if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                {
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please select to Assignee!..')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select to Assignee!..')", true);
                }
                else
                {
                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();


                    TicketingObjBo.TID = 0;
                    TicketingObjBo.TITLE = TxtTite.Text.ToString().Trim();
                    TicketingObjBo.ISSDESC = TxtIssDesc.Text.ToString().Trim();
                    TicketingObjBo.CLIENT = TxtRaisedby.Text.ToString().Trim();
                    TicketingObjBo.FRMUSR = TxtFrmUser.Text.ToString().Trim();
                    TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                    TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                    //TicketingObjBo.ASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                    TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER

                    //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];

                    TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                    TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                    TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                    TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                    TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                    //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                    TicketingObjBo.FRMSTATUS = 0;
                    TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status


                    if (FU_Isssue.HasFile)
                    {
                        foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                        {
                            TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                            TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                            TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                            //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                            uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                        }
                    }

                    //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                    //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                    //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                    TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                    TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                    TicketingObjBo.CREATED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_ON = new DateTime(1900, 01, 01);
                    TicketingObjBo.LASTMODIFIED_BY = "";
                    TicketingObjBo.Flag = 1;
                    TicketingObjBo.TICKETACTION = "";
                    TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                    TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                    TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                    SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                    ClearFields();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Created Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                    // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Created Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", "alert('Ticket Created Successfully !')", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Created Successfully !')", true);
                    //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                    Session["PendingPageIndex"] = "0";
                    Session["DDLStatusSearch"] = "0";
                    Session["DDLCustomerList"] = "0";
                    Session["TxtFromDate"] = "";
                    Session["TxtToDate"] = "";
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                msassignedtomebo objPIDashBoardBoISSTyp = new msassignedtomebo();
                msassignedtomebl objPIDashBoardBlISSTyp = new msassignedtomebl();
                objPIDashBoardBoISSTyp.PERNR = HttpContext.Current.User.Identity.Name;
                msassignedtomecollectionbo objPIDashBoardLstISSTyp = objPIDashBoardBlISSTyp.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBoISSTyp);
                string StatusISSTyp = "";
                TicketingToolbo TicketingObjBoISSTyp = new TicketingToolbo();
                TicketingToolbl TicketingObjBlISSTyp = new TicketingToolbl();
                TicketingObjBlISSTyp.CheckIfclients(1, User.Identity.Name, ref StatusISSTyp);
                if (StatusISSTyp == "True") // client
                {
                    RFV_DDLIssueType.Enabled = false;
                    RFV_DDLIssueCategoryCSS.Enabled = false;
                }
                else if (User.Identity.Name == "cssteam")
                {
                    RFV_DDLIssueType.Enabled = false;
                    RFV_DDLIssueCategoryCSS.Enabled = true;
                }
                else if (objPIDashBoardLstISSTyp.Count > 0)
                {
                    RFV_DDLIssueType.Enabled = true;
                    RFV_DDLIssueCategoryCSS.Enabled = false;
                }
                else
                {
                    RFV_DDLIssueType.Enabled = false;
                    RFV_DDLIssueCategoryCSS.Enabled = false;
                }

                //if (RFV_DDLIssueType)


                ViewState["BackBtn"] = "Edit";
                DisableFields(true);
                BtnEdit.Visible = false;
                BtnUpdate.Visible = true;

                if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                {
                    string Status = "";
                    msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                    msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                    objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
                    msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();
                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                    if (Status == "True") // client
                    {
                        BtnCancel.Visible = true;
                    }

                    else //if (User.Identity.Name == "cssteam")
                    {
                        //BtnCancel.Visible = true;
                        BtnCancel.Visible = false;
                    }
                }

                else if (ViewState["PrevStatusID"].ToString().Trim() == "2")
                {
                    //if (string.IsNullOrEmpty(ViewState["AGENTPERNR"].ToString().Trim()))
                    //{
                    string Status = "";
                    msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                    msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                    objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
                    msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();
                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                    if (Status == "True") // client
                    {
                        BtnCancel.Visible = true;
                    }

                    else //if (User.Identity.Name == "cssteam")
                    {
                        // BtnCancel.Visible = true;
                        BtnCancel.Visible = false;
                    }
                    //else
                    //{

                    //    if (objPIDashBoardLst.Count > 0)
                    //    {
                    //        BtnCancel.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        BtnCancel.Visible = false;
                    //    }
                    //}
                    if (TxtPlndHrs.Enabled == true)
                    {
                        TxtPlndHrs.Text = TxtPlndHrs.Text == "0.00" ? "" : TxtPlndHrs.Text;
                        RV_TxtPlndHrs.Enabled = TxtPlndHrs.Enabled == true ? true : false;
                        RF_plannedHr.Enabled = TxtPlndHrs.Enabled == true ? true : false;
                        using (HiddenField HFC = (HiddenField)FormTicket.FindControl("HF_clint"))
                        {
                            int parsedValue;
                            if (int.TryParse(HFC.Value, out parsedValue))
                            {
                                RV_TxtPlndHrs.Enabled = false;
                                RF_plannedHr.Enabled = false;
                            }
                        }
                    }
                    //}}

                }
                else if (ViewState["PrevStatusID"].ToString().Trim() == "13")
                {
                    string Status2 = "";
                    int? prvcasts = 0;
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();
                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status2);

                    if (Status2 != "True") // not client client
                    {
                        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
                        objTicketingToolDataContext.usp_tcikety_get_PrvStsofCA(long.Parse(ViewState["TicketID"].ToString().Trim()), ref prvcasts);

                        if (ViewState["CLIENT"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                        {
                            DDLIssueStatus.SelectedValue = prvcasts.ToString();
                            DDLIssueStatus.Enabled = false;
                        }
                        else
                        {
                            DDLIssueStatus.SelectedValue = "13";
                            DDLIssueStatus.Enabled = false;
                        }


                    }
                }
                else if (ViewState["PrevStatusID"].ToString().Trim() != "2" && ViewState["PrevStatusID"].ToString().Trim() != "1")
                {
                    BtnCancel.Visible = false;
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int parsedValue, parv;

                if ((int.TryParse(HttpContext.Current.User.Identity.Name, out parv)) && (!int.TryParse(ddlAssignee.SelectedValue.ToString().Trim(), out parsedValue)) &&
                    ((DDLIssueStatus.SelectedValue.ToString().Trim() != "4") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "7") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "8")))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Client or CSSTeam cannot process this status further, please select correct status..!')", true); //'" + DDLIssueStatus.SelectedItem.ToString().Trim() + "'
                    DDLIssueStatus.SelectedValue = "0";
                }
                else
                {

                    long? TicketRefIdOut = 0;

                    TicketingToolbo TicketingObjBoC = new TicketingToolbo();
                    TicketingToolbl TicketingObjBC = new TicketingToolbl();
                    string StatusC = "";
                    TicketingObjBC.CheckIfclients(1, User.Identity.Name, ref StatusC);

                    if (Cb_InReviewNeed.Checked)
                    {
                        if ((DDLIssueStatus.SelectedValue.ToString().Trim() == "3" || DDLIssueStatus.SelectedValue.ToString().Trim() == "6"))
                        {
                            if ((StatusC != "True") && (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim())))
                            {
                                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please select to Assignee!..')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select to Assignee!..')", true);
                            }
                            else
                            {
                                if (FU_Isssue.HasFile || (StatusC == "True"))
                                {
                                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                    TicketingToolbl TicketingObjBl = new TicketingToolbl();

                                    //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                                    //{
                                    TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                                    TicketingObjBo.TITLE = "";
                                    TicketingObjBo.ISSDESC = "";
                                    TicketingObjBo.CLIENT = "";
                                    TicketingObjBo.FRMUSR = "";
                                    TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                                    TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                                    //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                                    TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                                    //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                                    //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                                    TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                                    TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                                    string Status = "";
                                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                                    if (Status == "True") // client
                                    {
                                        TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                                    {
                                        TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                                    {
                                        if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                        {
                                            TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                        }
                                        else
                                        {
                                            TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                        }
                                    }
                                    else
                                    {
                                        TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                    }


                                    //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                    TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                                    TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                                    TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                                    TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                                    //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                                    TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                                    TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                                    if (FU_Isssue.HasFile)
                                    {
                                        foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                        {
                                            TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                            TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                            TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                            //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                            uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                        }
                                    }

                                    //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                                    //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                                    //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                                    TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                                    TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                                    TicketingObjBo.CREATED_ON = DateTime.Now;
                                    TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                                    TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                                    TicketingObjBo.Flag = 2;
                                    TicketingObjBo.TICKETACTION = "";
                                    TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                                    TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                                    TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                                    SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                                    ClearFields();
                                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                    //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                                    //}
                                    Session["PendingPageIndex"] = "0";
                                    Session["DDLStatusSearch"] = "0";
                                    Session["DDLCustomerList"] = "0";
                                    Session["TxtFromDate"] = "";
                                    Session["TxtToDate"] = "";
                                }
                                else
                                {
                                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please attach the file..')", true);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please attach the file!..')", true);
                                }
                            }
                        } // if sts not 3 or 6
                        else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "13") // customer action start
                        {
                            if ((StatusC != "True") && (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim())))
                            {
                                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please select to Assignee!..')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select to Assignee!..')", true);
                            }
                            else
                            {
                                if (int.Parse(ViewState["PrevStatusID"].ToString().Trim()) == 2 || int.Parse(ViewState["PrevStatusID"].ToString().Trim()) == 13 || int.Parse(ViewState["PrevStatusID"].ToString().Trim()) == 10)
                                {
                                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                    TicketingToolbl TicketingObjBl = new TicketingToolbl();

                                    //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                                    //{
                                    TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                                    TicketingObjBo.TITLE = "";
                                    TicketingObjBo.ISSDESC = "";
                                    TicketingObjBo.CLIENT = "";
                                    TicketingObjBo.FRMUSR = "";
                                    TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                                    TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                                    //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                                    TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                                    //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                                    //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                                    TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                                    TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                                    string Status = "";
                                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                                    if (Status == "True") // client
                                    {
                                        TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                                    {
                                        TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                                    {
                                        if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                        {
                                            TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                        }
                                        else
                                        {
                                            TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                        }
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "8") // Confirm status
                                    {
                                        TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
                                    }
                                    else
                                    {
                                        TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                    }


                                    //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                    TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                                    TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                                    TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                                    TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                                    //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                                    TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                                    TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                                    if (FU_Isssue.HasFile)
                                    {
                                        foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                        {
                                            TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                            TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                            TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                            //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                            uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                        }
                                    }

                                    //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                                    //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                                    //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                                    TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                                    TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                                    TicketingObjBo.CREATED_ON = DateTime.Now;
                                    TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                                    TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                                    TicketingObjBo.Flag = 2;
                                    TicketingObjBo.TICKETACTION = "";
                                    TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                                    TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                                    TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                                    SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                                    ClearFields();
                                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !');parent.location.href='IssueTracker.aspx'", true);
                                    //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                                    //}
                                    Session["PendingPageIndex"] = "0";
                                    Session["DDLStatusSearch"] = "0";
                                    Session["DDLCustomerList"] = "0";
                                    Session["TxtFromDate"] = "";
                                    Session["TxtToDate"] = "";
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Customer Action can be done only in inprogress status !')", true);
                                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Customer Action can be done in only inprogress status')", true);
                                }
                            }
                        }// customer action close
                        else
                        {
                            if (DDLIssueStatus.SelectedValue.ToString().Trim() != "9" && DDLIssueStatus.SelectedValue.ToString().Trim() != "11")
                            {
                                if (User.Identity.Name.ToLower() == "cssteam")
                                {
                                    if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                                    {
                                        if (DDLIssueStatus.SelectedValue.ToString().Trim() != "2" && DDLIssueStatus.SelectedValue.ToString().Trim() != "11" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
                                        {

                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InProgress,Cancelled or Customer Action.');", true);
                                            DDLIssueStatus.SelectedValue = "0";

                                        }
                                        else
                                        {
                                            if ((StatusC != "True") && (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim())))
                                            {
                                                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please select to Assignee!..')", true);
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select to Assignee!..')", true);
                                            }
                                            else
                                            {
                                                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                                TicketingToolbl TicketingObjBl = new TicketingToolbl();

                                                //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                                                //{
                                                TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                                                TicketingObjBo.TITLE = "";
                                                TicketingObjBo.ISSDESC = "";
                                                TicketingObjBo.CLIENT = "";
                                                TicketingObjBo.FRMUSR = "";
                                                TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                                                TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                                                //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                                                TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                                                //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                                                //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                                                TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                                                TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                                                string Status = "";
                                                TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                                                if (Status == "True") // client
                                                {
                                                    TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                                }
                                                else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                                                {
                                                    TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                                }
                                                else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                                                {
                                                    if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                                    {
                                                        TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                                    }
                                                    else
                                                    {
                                                        TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                                    }
                                                }
                                                else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "8") // Confirm status
                                                {
                                                    TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
                                                }
                                                else
                                                {
                                                    TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                                }


                                                //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                                TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                                                TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                                                TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                                                TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                                                //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                                                TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                                                TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                                                if (FU_Isssue.HasFile)
                                                {
                                                    foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                                    {
                                                        TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                                        TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                                        TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                                        //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                                        uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                                    }
                                                }
                                                //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                                                //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                                                //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                                                TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                                                TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                                                TicketingObjBo.CREATED_ON = DateTime.Now;
                                                TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                                                TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                                                TicketingObjBo.Flag = 2;
                                                TicketingObjBo.TICKETACTION = "";
                                                TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                                                TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                                                TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                                                SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                                                ClearFields();
                                                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !..');parent.location.href='IssueTracker.aspx'", true);
                                                //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                                                //}
                                                Session["PendingPageIndex"] = "0";
                                                Session["DDLStatusSearch"] = "0";
                                                Session["DDLCustomerList"] = "0";
                                                Session["TxtFromDate"] = "";
                                                Session["TxtToDate"] = "";
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if ((StatusC != "True") && (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim())))
                                        {
                                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please select to Assignee!..')", true);
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select to Assignee!..')", true);
                                        }
                                        else
                                        {
                                            string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                            TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                            TicketingToolbl TicketingObjBl = new TicketingToolbl();

                                            //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                                            //{
                                            TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                                            TicketingObjBo.TITLE = "";
                                            TicketingObjBo.ISSDESC = "";
                                            TicketingObjBo.CLIENT = "";
                                            TicketingObjBo.FRMUSR = "";
                                            TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                                            TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                                            //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                                            TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                                            //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                                            //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                                            TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                                            TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                                            string Status = "";
                                            TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                                            if (Status == "True") // client
                                            {
                                                TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                            }
                                            else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                                            {
                                                TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                            }
                                            else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                                            {
                                                if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                                {
                                                    TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                                }
                                                else
                                                {
                                                    TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                                }
                                            }
                                            else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "8") // Confirm status
                                            {
                                                TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
                                            }
                                            else
                                            {
                                                TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                            }


                                            //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                            TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                                            TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                                            TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                                            TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                                            //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                                            TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                                            TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                                            if (FU_Isssue.HasFile)
                                            {
                                                foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                                {
                                                    TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                                    TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                                    TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                                    //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                                    uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                                }
                                            }
                                            //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                                            //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                                            //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                                            TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                                            TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                                            TicketingObjBo.CREATED_ON = DateTime.Now;
                                            TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                                            TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                                            TicketingObjBo.Flag = 2;
                                            TicketingObjBo.TICKETACTION = "";
                                            TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                                            TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                                            TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                                            SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                                            ClearFields();
                                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !..');parent.location.href='IssueTracker.aspx'", true);
                                            //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                                            //}
                                            Session["PendingPageIndex"] = "0";
                                            Session["DDLStatusSearch"] = "0";
                                            Session["DDLCustomerList"] = "0";
                                            Session["TxtFromDate"] = "";
                                            Session["TxtToDate"] = "";
                                        }
                                    }

                                }
                                else
                                {
                                    if ((StatusC != "True") && (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim())))
                                    {
                                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please select to Assignee!..')", true);
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select to Assignee!..')", true);
                                    }
                                    else
                                    {
                                        string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                        TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                        TicketingToolbl TicketingObjBl = new TicketingToolbl();

                                        //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                                        //{
                                        TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                                        TicketingObjBo.TITLE = "";
                                        TicketingObjBo.ISSDESC = "";
                                        TicketingObjBo.CLIENT = "";
                                        TicketingObjBo.FRMUSR = "";
                                        TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                                        TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                                        //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                                        TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                                        //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                                        //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                                        TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                                        TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                                        string Status = "";
                                        TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                                        if (Status == "True") // client
                                        {
                                            TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                        }
                                        else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                                        {
                                            TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                        }
                                        else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                                        {
                                            if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                            {
                                                TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                            }
                                            else
                                            {
                                                TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                            }
                                        }
                                        else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "8") // Confirm status
                                        {
                                            TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
                                        }
                                        else
                                        {
                                            TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                        }


                                        //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                        TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                                        TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                                        TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                                        TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                                        //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                                        TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                                        TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                                        if (FU_Isssue.HasFile)
                                        {
                                            foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                            {
                                                TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                                TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                                TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                                //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                                uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                            }
                                        }
                                        //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                                        //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                                        //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                                        TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                                        TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                                        TicketingObjBo.CREATED_ON = DateTime.Now;
                                        TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                                        TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                                        TicketingObjBo.Flag = 2;
                                        TicketingObjBo.TICKETACTION = "";
                                        TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                                        TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                                        TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                                        SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                                        ClearFields();
                                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !..');parent.location.href='IssueTracker.aspx'", true);
                                        //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                                        //}
                                        Session["PendingPageIndex"] = "0";
                                        Session["DDLStatusSearch"] = "0";
                                        Session["DDLCustomerList"] = "0";
                                        Session["TxtFromDate"] = "";
                                        Session["TxtToDate"] = "";
                                    }
                                }

                            } //  if sts not in 9 or 11
                            else
                            {
                                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                TicketingToolbl TicketingObjBl = new TicketingToolbl();

                                //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                                //{
                                TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                                TicketingObjBo.TITLE = "";
                                TicketingObjBo.ISSDESC = "";
                                TicketingObjBo.CLIENT = "";
                                TicketingObjBo.FRMUSR = "";
                                TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                                TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                                //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                                TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                                //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                                //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                                TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                                TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                                string Status = "";
                                TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                                if (Status == "True") // client
                                {
                                    TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                }
                                else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                                {
                                    TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                }
                                else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                                {
                                    if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                    {
                                        TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                    }
                                    else
                                    {
                                        TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                    }
                                }
                                else
                                {
                                    TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                }


                                //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                                TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                                TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                                TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                                //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                                TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                                TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                                if (FU_Isssue.HasFile)
                                {
                                    foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                    {
                                        TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                        TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                        TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                        //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                        uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                    }
                                }
                                //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                                //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                                //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                                TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                                TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                                TicketingObjBo.CREATED_ON = DateTime.Now;
                                TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                                TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                                TicketingObjBo.Flag = 2;
                                TicketingObjBo.TICKETACTION = "";
                                TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                                TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                                TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                                SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                                ClearFields();
                                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully!..');parent.location.href='IssueTracker.aspx'", true);
                                //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                                //}
                                Session["PendingPageIndex"] = "0";
                                Session["DDLStatusSearch"] = "0";
                                Session["DDLCustomerList"] = "0";
                                Session["TxtFromDate"] = "";
                                Session["TxtToDate"] = "";
                            }
                        }
                    }//if checked closed

                    else
                    {

                        if (DDLIssueStatus.SelectedValue.ToString().Trim() == "13") // customer action start
                        {
                            if ((StatusC != "True") && (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim())))
                            {
                                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please select to Assignee!..')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select to Assignee!..')", true);
                            }
                            else
                            {
                                if (int.Parse(ViewState["PrevStatusID"].ToString().Trim()) == 2 || int.Parse(ViewState["PrevStatusID"].ToString().Trim()) == 13 || int.Parse(ViewState["PrevStatusID"].ToString().Trim()) == 10)
                                {
                                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                    TicketingToolbl TicketingObjBl = new TicketingToolbl();

                                    //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                                    //{
                                    TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                                    TicketingObjBo.TITLE = "";
                                    TicketingObjBo.ISSDESC = "";
                                    TicketingObjBo.CLIENT = "";
                                    TicketingObjBo.FRMUSR = "";
                                    TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                                    TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                                    //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                                    TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                                    //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                                    //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                                    TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                                    TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                                    string Status = "";
                                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                                    if (Status == "True") // client
                                    {
                                        TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                                    {
                                        TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                                    {
                                        if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                        {
                                            TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                        }
                                        else
                                        {
                                            TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                        }
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "8") // Confirm status
                                    {
                                        TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
                                    }
                                    else
                                    {
                                        TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                    }


                                    //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                    TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                                    TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                                    TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                                    TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                                    //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                                    TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                                    TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                                    if (FU_Isssue.HasFile)
                                    {
                                        foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                        {
                                            TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                            TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                            TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                            //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                            uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                        }
                                    }

                                    //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                                    //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                                    //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                                    TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                                    TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                                    TicketingObjBo.CREATED_ON = DateTime.Now;
                                    TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                                    TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                                    TicketingObjBo.Flag = 2;
                                    TicketingObjBo.TICKETACTION = "";
                                    TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                                    TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                                    TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                                    SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                                    ClearFields();
                                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !');parent.location.href='IssueTracker.aspx'", true);
                                    //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                                    //}
                                    Session["PendingPageIndex"] = "0";
                                    Session["DDLStatusSearch"] = "0";
                                    Session["DDLCustomerList"] = "0";
                                    Session["TxtFromDate"] = "";
                                    Session["TxtToDate"] = "";
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Customer Action can be done only in inprogress status!')", true);
                                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Customer Action can be done in only inprogress status')", true);
                                }
                            }
                        }// customer action close


                        else if (DDLIssueStatus.SelectedValue.ToString().Trim() != "9" && DDLIssueStatus.SelectedValue.ToString().Trim() != "11")
                        {
                            if (User.Identity.Name.ToLower() == "cssteam")
                            {
                                if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                                {
                                    if (DDLIssueStatus.SelectedValue.ToString().Trim() != "2" && DDLIssueStatus.SelectedValue.ToString().Trim() != "11" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
                                    {

                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InProgress,Cancelled or Customer Action.');", true);
                                        DDLIssueStatus.SelectedValue = "0";
                                    }
                                    else
                                    {
                                        if ((StatusC != "True") && (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim())))
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select to Assignee!..')", true);
                                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please select to Assignee!..')", true);
                                        }
                                        else
                                        {
                                            string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                            TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                            TicketingToolbl TicketingObjBl = new TicketingToolbl();

                                            //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                                            //{
                                            TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                                            TicketingObjBo.TITLE = "";
                                            TicketingObjBo.ISSDESC = "";
                                            TicketingObjBo.CLIENT = "";
                                            TicketingObjBo.FRMUSR = "";
                                            TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                                            TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                                            //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                                            TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                                            //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                                            //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                                            TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                                            TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                                            string Status = "";
                                            TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                                            if (Status == "True") // client
                                            {
                                                TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                            }
                                            else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                                            {
                                                TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                            }
                                            else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                                            {
                                                if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                                {
                                                    TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                                }
                                                else
                                                {
                                                    TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                                }
                                            }
                                            else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "8") // confirm status
                                            {
                                                TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
                                            }
                                            else
                                            {
                                                TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                            }


                                            //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                            TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                                            TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                                            TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                                            TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                                            //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                                            TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                                            TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                                            if (FU_Isssue.HasFile)
                                            {
                                                foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                                {
                                                    TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                                    TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                                    TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                                    //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                                    uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                                }
                                            }
                                            //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                                            //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                                            //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                                            TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                                            TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                                            TicketingObjBo.CREATED_ON = DateTime.Now;
                                            TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                                            TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                                            TicketingObjBo.Flag = 2;
                                            TicketingObjBo.TICKETACTION = "";
                                            TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                                            TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                                            TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                                            SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                                            ClearFields();
                                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !'); parent.location.href='IssueTracker.aspx'", true);
                                            //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                                            //}
                                            Session["PendingPageIndex"] = "0";
                                            Session["DDLStatusSearch"] = "0";
                                            Session["DDLCustomerList"] = "0";
                                            Session["TxtFromDate"] = "";
                                            Session["TxtToDate"] = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if ((StatusC != "True") && (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim())))
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select to Assignee!..')", true);
                                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please select to Assignee!..')", true);
                                    }
                                    else
                                    {
                                        string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                        TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                        TicketingToolbl TicketingObjBl = new TicketingToolbl();

                                        //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                                        //{
                                        TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                                        TicketingObjBo.TITLE = "";
                                        TicketingObjBo.ISSDESC = "";
                                        TicketingObjBo.CLIENT = "";
                                        TicketingObjBo.FRMUSR = "";
                                        TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                                        TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                                        //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                                        TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                                        //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                                        //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                                        TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                                        TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                                        string Status = "";
                                        TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                                        if (Status == "True") // client
                                        {
                                            TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                        }
                                        else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                                        {
                                            TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                        }
                                        else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                                        {
                                            if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                            {
                                                TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                            }
                                            else
                                            {
                                                TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                            }
                                        }
                                        else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "8") // confirm status
                                        {
                                            TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
                                        }
                                        else
                                        {
                                            TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                        }


                                        //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                        TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                                        TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                                        TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                                        TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                                        //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                                        TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                                        TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                                        if (FU_Isssue.HasFile)
                                        {
                                            foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                            {
                                                TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                                TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                                TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                                //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                                uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                            }
                                        }
                                        //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                                        //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                                        //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                                        TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                                        TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                                        TicketingObjBo.CREATED_ON = DateTime.Now;
                                        TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                                        TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                                        TicketingObjBo.Flag = 2;
                                        TicketingObjBo.TICKETACTION = "";
                                        TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                                        TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                                        TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                                        SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                                        ClearFields();
                                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !'); parent.location.href='IssueTracker.aspx'", true);
                                        //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                                        //}
                                        Session["PendingPageIndex"] = "0";
                                        Session["DDLStatusSearch"] = "0";
                                        Session["DDLCustomerList"] = "0";
                                        Session["TxtFromDate"] = "";
                                        Session["TxtToDate"] = "";
                                    }
                                }
                            }
                            else
                            {
                                if ((StatusC != "True") && (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim())))
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select to Assignee!..')", true);
                                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please select to Assignee!..')", true);
                                }
                                else
                                {
                                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                                    TicketingToolbl TicketingObjBl = new TicketingToolbl();

                                    //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                                    //{
                                    TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                                    TicketingObjBo.TITLE = "";
                                    TicketingObjBo.ISSDESC = "";
                                    TicketingObjBo.CLIENT = "";
                                    TicketingObjBo.FRMUSR = "";
                                    TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                                    TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                                    //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                                    TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                                    //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                                    //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                                    TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                                    TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                                    string Status = "";
                                    TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                                    if (Status == "True") // client
                                    {
                                        TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                                    {
                                        TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                                    {
                                        if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                        {
                                            TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                        }
                                        else
                                        {
                                            TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                        }
                                    }
                                    else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "8") // confirm status
                                    {
                                        TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
                                    }
                                    else
                                    {
                                        TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                    }


                                    //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                                    TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                                    TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                                    TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                                    TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                                    //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                                    TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                                    TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                                    if (FU_Isssue.HasFile)
                                    {
                                        foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                        {
                                            TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                            TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                            TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                            //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                            uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                        }
                                    }
                                    //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                                    //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                                    //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                                    TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                                    TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                                    TicketingObjBo.CREATED_ON = DateTime.Now;
                                    TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                                    TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                                    TicketingObjBo.Flag = 2;
                                    TicketingObjBo.TICKETACTION = "";
                                    TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                                    TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                                    TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                                    SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                                    ClearFields();
                                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !'); parent.location.href='IssueTracker.aspx'", true);
                                    //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                                    //}
                                    Session["PendingPageIndex"] = "0";
                                    Session["DDLStatusSearch"] = "0";
                                    Session["DDLCustomerList"] = "0";
                                    Session["TxtFromDate"] = "";
                                    Session["TxtToDate"] = "";
                                }
                            }

                        } // not in 9 or 11
                        else
                        {
                            string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                            TicketingToolbo TicketingObjBo = new TicketingToolbo();
                            TicketingToolbl TicketingObjBl = new TicketingToolbl();

                            //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                            //{
                            TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                            TicketingObjBo.TITLE = "";
                            TicketingObjBo.ISSDESC = "";
                            TicketingObjBo.CLIENT = "";
                            TicketingObjBo.FRMUSR = "";
                            TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                            TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                            //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                            TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER
                            //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];
                            //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                            TicketingToolbo TicketingObjBo1 = new TicketingToolbo();
                            TicketingToolbl TicketingObjBl1 = new TicketingToolbl();

                            string Status = "";
                            TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

                            if (Status == "True") // client
                            {
                                TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                            }
                            else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                            {
                                TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                            }
                            else if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                            {
                                if (ddlAssignee.SelectedValue.ToString().Trim() == "0" || string.IsNullOrEmpty(ddlAssignee.SelectedValue.ToString().Trim()))
                                {
                                    TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                                }
                                else
                                {
                                    TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim();
                                }
                            }
                            else
                            {
                                TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                            }


                            //TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                            TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                            TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                            TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                            TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                            //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                            TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                            TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                            if (FU_Isssue.HasFile)
                            {
                                foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                                {
                                    TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                                    TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                                    TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                                    //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                    uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                                }
                            }

                            //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                            //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                            //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                            TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                            TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                            TicketingObjBo.CREATED_ON = DateTime.Now;
                            TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                            TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                            TicketingObjBo.Flag = 2;
                            TicketingObjBo.TICKETACTION = "";
                            TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                            TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                            TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                            SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                            ClearFields();
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !'); parent.location.href='IssueTracker.aspx'", true);
                            //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                            //}
                            Session["PendingPageIndex"] = "0";
                            Session["DDLStatusSearch"] = "0";
                            Session["DDLCustomerList"] = "0";
                            Session["TxtFromDate"] = "";
                            Session["TxtToDate"] = "";
                        }
                    }

                    TicketingToolbo TicketingObjBo2 = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl2 = new TicketingToolbl();
                    List<TicketingToolbo> TicketingboList2 = new List<TicketingToolbo>();

                    TicketingboList2 = TicketingObjBl2.Load_Ticket(long.Parse(ViewState["TicketID"].ToString().Trim()));
                    ViewState["TIMERSTS"] = TicketingboList2[0].STATUS == null ? "0" : TicketingboList2[0].STATUS.ToString().Trim();
                    ViewState["CATEGORYIDSLA"] = TicketingboList2[0].CATEGORY == null ? "0" : TicketingboList2[0].CATEGORY.ToString().Trim();
                    if (ViewState["CATEGORYIDSLA"].ToString().Trim() == "2" || ViewState["CATEGORYIDSLA"].ToString().Trim() == "3")
                    {
                        if (ViewState["TIMERSTS"].ToString().Trim() != "1" && ViewState["TIMERSTS"].ToString().Trim() != "4" && ViewState["TIMERSTS"].ToString().Trim() != "7" && ViewState["TIMERSTS"].ToString().Trim() != "8" && ViewState["TIMERSTS"].ToString().Trim() != "9" && ViewState["TIMERSTS"].ToString().Trim() != "11" && ViewState["TIMERSTS"].ToString().Trim() != "12" && ViewState["TIMERSTS"].ToString().Trim() != "13")
                        {
                            Session["CountdownTimer"] = null;
                            BindTimerData(long.Parse(ViewState["TicketID"].ToString().Trim()).ToString());
                            UpdatePanel1.Visible = true;
                        }
                        else
                        {
                            UpdatePanel1.Visible = false;
                        }
                    }
                    else
                    {
                        UpdatePanel1.Visible = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                switch (Ex.Message)
                {
                    case "-1":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only InProgess Tickets can be update to HOLD Status..!!');", true);
                        break;
                    case "-2":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only Support or Incident Category Tickets can be update to HOLD Status..!!');", true);
                        break;
                    case "-3":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('CSSTeam cannot process this ticket Status that you have selected..!!');", true);
                        break;
                    case "-4":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('CSSTeam cannot process this ticket Status that you have selected..!!');", true);
                        break;
                    case "-5":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Agent cannot process this Status futher..!!');", true);
                        break;
                    case "-6":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Client cannot process this Status futher..!!');", true);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true);
                        break;
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                long? TicketRefIdOut = 0;
                if (string.IsNullOrEmpty(TxtComments.Text.ToString().Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter the comments!..');", true);
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please enter the comments!..')", true);
                }
                else
                {
                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();

                    TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                    TicketingObjBo.TITLE = "";
                    TicketingObjBo.ISSDESC = "";
                    TicketingObjBo.CLIENT = "";
                    TicketingObjBo.FRMUSR = "";
                    TicketingObjBo.USRMAILID = "";
                    TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                    //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                    TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER

                    TicketingObjBo.TOASSIGNEE = ViewState["ASSIGNEE"].ToString().Trim(); // Assignee ie Assigned to 
                    TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                    TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                    TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                    TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                    //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                    TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                    TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                    if (FU_Isssue.HasFile)
                    {
                        foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                        {
                            TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                            TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                            TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                            //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                            uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                        }
                    }

                    //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                    //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                    //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                    TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                    TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                    TicketingObjBo.CREATED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                    TicketingObjBo.Flag = 2;
                    TicketingObjBo.TICKETACTION = "CANCELLED";
                    TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                    TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                    TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                    ClearFields();
                    SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket cancelled Successfully!..');  parent.location.href='IssueTracker.aspx'", true);

                    TicketingToolbo TicketingObjBo2 = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl2 = new TicketingToolbl();
                    List<TicketingToolbo> TicketingboList2 = new List<TicketingToolbo>();

                    TicketingboList2 = TicketingObjBl2.Load_Ticket(long.Parse(ViewState["TicketID"].ToString().Trim()));
                    ViewState["TIMERSTS"] = TicketingboList2[0].STATUS == null ? "0" : TicketingboList2[0].STATUS.ToString().Trim();
                    ViewState["CATEGORYIDSLA"] = TicketingboList2[0].CATEGORY == null ? "0" : TicketingboList2[0].CATEGORY.ToString().Trim();
                    if (ViewState["CATEGORYIDSLA"].ToString().Trim() == "2" || ViewState["CATEGORYIDSLA"].ToString().Trim() == "3")
                    {
                        if (ViewState["TIMERSTS"].ToString().Trim() != "1" && ViewState["TIMERSTS"].ToString().Trim() != "4" && ViewState["TIMERSTS"].ToString().Trim() != "7" && ViewState["TIMERSTS"].ToString().Trim() != "8" && ViewState["TIMERSTS"].ToString().Trim() != "9" && ViewState["TIMERSTS"].ToString().Trim() != "11" && ViewState["TIMERSTS"].ToString().Trim() != "12" && ViewState["TIMERSTS"].ToString().Trim() != "13")
                        {
                            Session["CountdownTimer"] = null;
                            BindTimerData(long.Parse(ViewState["TicketID"].ToString().Trim()).ToString());
                            UpdatePanel1.Visible = true;
                        }
                        else
                        {
                            UpdatePanel1.Visible = false;
                        }
                    }
                    else
                    {
                        UpdatePanel1.Visible = false;
                    }
                    Session["PendingPageIndex"] = "0";
                    Session["DDLStatusSearch"] = "0";
                    Session["DDLCustomerList"] = "0";
                    Session["TxtFromDate"] = "";
                    Session["TxtToDate"] = "";
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        //protected void BtnConfirm_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        long? TicketRefIdOut = 0;
        //        if (string.IsNullOrEmpty(TxtComments.Text.ToString().Trim()))
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter the comments!..')", true);
        //            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please enter the comments!..')", true);
        //        }
        //        else
        //        {
        //            string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
        //            TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //            TicketingToolbl TicketingObjBl = new TicketingToolbl();

        //            //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
        //            //{
        //            TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
        //            TicketingObjBo.TITLE = "";
        //            TicketingObjBo.ISSDESC = "";
        //            TicketingObjBo.CLIENT = "";
        //            TicketingObjBo.FRMUSR = "";
        //            TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
        //            TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
        //            //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
        //            TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER

        //            //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];

        //            //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

        //            if (ViewState["PrevStatusID"].ToString().Trim() == "7") // Resolved status
        //            {
        //                TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
        //            }
        //            else if (ViewState["PrevStatusID"].ToString().Trim() == "4") // UAT Status
        //            {
        //                TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
        //            }


        //            TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
        //            TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
        //            TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
        //            TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
        //            //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
        //            TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
        //            TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status
        //            if (FU_Isssue.HasFile)
        //            {
        //                foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
        //                {
        //                    TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
        //                    TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
        //                    TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
        //                    //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
        //                    uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
        //                }
        //            }

        //            //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
        //            //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
        //            //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
        //            TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
        //            TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
        //            TicketingObjBo.CREATED_ON = DateTime.Now;
        //            TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
        //            TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
        //            TicketingObjBo.Flag = 2;
        //            TicketingObjBo.TICKETACTION = "CONFIRM";
        //            TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
        //            TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
        //            TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
        //            SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
        //            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !')", true);
        //            //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
        //            ClearFields();
        //            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !'); parent.location.href='IssueTracker.aspx'", true);
        //            //}

        //            TicketingToolbo TicketingObjBo2 = new TicketingToolbo();
        //            TicketingToolbl TicketingObjBl2 = new TicketingToolbl();
        //            List<TicketingToolbo> TicketingboList2 = new List<TicketingToolbo>();

        //            TicketingboList2 = TicketingObjBl2.Load_Ticket(long.Parse(ViewState["TicketID"].ToString().Trim()));
        //            ViewState["TIMERSTS"] = TicketingboList2[0].STATUS == null ? "0" : TicketingboList2[0].STATUS.ToString().Trim();
        //            ViewState["CATEGORYIDSLA"] = TicketingboList2[0].CATEGORY == null ? "0" : TicketingboList2[0].CATEGORY.ToString().Trim();
        //            if (ViewState["CATEGORYIDSLA"].ToString().Trim() == "2" || ViewState["CATEGORYIDSLA"].ToString().Trim() == "3")
        //            {
        //                if (ViewState["TIMERSTS"].ToString().Trim() != "1" && ViewState["TIMERSTS"].ToString().Trim() != "8" && ViewState["TIMERSTS"].ToString().Trim() != "9" && ViewState["TIMERSTS"].ToString().Trim() != "11" && ViewState["TIMERSTS"].ToString().Trim() != "12" && ViewState["TIMERSTS"].ToString().Trim() != "13")
        //                {
        //                    Session["CountdownTimer"] = null;
        //                    BindTimerData(long.Parse(ViewState["TicketID"].ToString().Trim()).ToString());
        //                    UpdatePanel1.Visible = true;
        //                }
        //                else
        //                {
        //                    UpdatePanel1.Visible = false;
        //                }
        //            }
        //            else
        //            {
        //                UpdatePanel1.Visible = false;
        //            }
        //            Session["PendingPageIndex"] = "0";
        //            Session["DDLStatusSearch"] = "0";
        //            Session["DDLCustomerList"] = "0";
        //            Session["TxtFromDate"] = "";
        //            Session["TxtToDate"] = "";
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

        protected void BtnDeny_Click(object sender, EventArgs e)
        {
            try
            {
                long? TicketRefIdOut = 0;
                if (string.IsNullOrEmpty(TxtComments.Text.ToString().Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter the comments!..');", true);
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please enter the comments!..')", true);
                }
                else
                {
                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();

                    //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                    //{
                    TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                    TicketingObjBo.TITLE = "";
                    TicketingObjBo.ISSDESC = "";
                    TicketingObjBo.CLIENT = "";
                    TicketingObjBo.FRMUSR = "";
                    TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                    TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                    //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                    TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER

                    //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];

                    //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 
                    TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                    TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                    TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                    TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                    TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                    //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                    TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                    TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

                    if (FU_Isssue.HasFile)
                    {
                        foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                        {
                            TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                            TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                            TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                            //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                            uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                        }
                    }

                    //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                    //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                    //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                    TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                    TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                    TicketingObjBo.CREATED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                    TicketingObjBo.Flag = 2;
                    TicketingObjBo.TICKETACTION = "DENY";
                    TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                    TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                    TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                    SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !')", true);
                    //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                    ClearFields();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !'); parent.location.href='IssueTracker.aspx'", true);
                    // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                    TicketingToolbo TicketingObjBo2 = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl2 = new TicketingToolbl();
                    List<TicketingToolbo> TicketingboList2 = new List<TicketingToolbo>();

                    TicketingboList2 = TicketingObjBl2.Load_Ticket(long.Parse(ViewState["TicketID"].ToString().Trim()));
                    ViewState["TIMERSTS"] = TicketingboList2[0].STATUS == null ? "0" : TicketingboList2[0].STATUS.ToString().Trim();
                    ViewState["CATEGORYIDSLA"] = TicketingboList2[0].CATEGORY == null ? "0" : TicketingboList2[0].CATEGORY.ToString().Trim();
                    if (ViewState["CATEGORYIDSLA"].ToString().Trim() == "2" || ViewState["CATEGORYIDSLA"].ToString().Trim() == "3")
                    {
                        if (ViewState["TIMERSTS"].ToString().Trim() != "1" && ViewState["TIMERSTS"].ToString().Trim() != "4" && ViewState["TIMERSTS"].ToString().Trim() != "7" && ViewState["TIMERSTS"].ToString().Trim() != "8" && ViewState["TIMERSTS"].ToString().Trim() != "9" && ViewState["TIMERSTS"].ToString().Trim() != "11" && ViewState["TIMERSTS"].ToString().Trim() != "12" && ViewState["TIMERSTS"].ToString().Trim() != "13")
                        {
                            Session["CountdownTimer"] = null;
                            BindTimerData(long.Parse(ViewState["TicketID"].ToString().Trim()).ToString());
                            UpdatePanel1.Visible = true;
                        }
                        else
                        {
                            UpdatePanel1.Visible = false;
                        }
                    }
                    else
                    {
                        UpdatePanel1.Visible = false;
                    }
                    Session["PendingPageIndex"] = "0";
                    Session["DDLStatusSearch"] = "0";
                    Session["DDLCustomerList"] = "0";
                    Session["TxtFromDate"] = "";
                    Session["TxtToDate"] = "";
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        //protected void BtnPending_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        long? TicketRefIdOut = 0;
        //        if (string.IsNullOrEmpty(TxtComments.Text.ToString().Trim()))
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please enter the comments!..')", true);
        //        }
        //        else
        //        {
        //            string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
        //            TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //            TicketingToolbl TicketingObjBl = new TicketingToolbl();

        //            //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
        //            //{
        //            TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
        //            TicketingObjBo.TITLE = "";
        //            TicketingObjBo.ISSDESC = "";
        //            TicketingObjBo.CLIENT = "";
        //            TicketingObjBo.FRMUSR = "";
        //            TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
        //            //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
        //            TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER

        //            //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];

        //            //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 
        //            TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
        //            TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
        //            TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
        //            TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
        //            //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
        //            TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
        //            TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status

        //            if (FU_Isssue.HasFile)
        //            {
        //                foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
        //                {
        //                    TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
        //                    TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
        //                    TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
        //                    //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
        //                    uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
        //                }
        //            }

        //            //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
        //            //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
        //            //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
        //            TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
        //            TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
        //            TicketingObjBo.CREATED_ON = DateTime.Now;
        //            TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
        //            TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
        //            TicketingObjBo.Flag = 2;
        //            TicketingObjBo.TICKETACTION = "PENDING";
        //            TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
        //            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !')", true);
        //            //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
        //            SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
        //            ClearFields();
        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);

        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

        protected void BtnCompleted_Click(object sender, EventArgs e)
        {
            try
            {
                long? TicketRefIdOut = 0;
                if (string.IsNullOrEmpty(TxtComments.Text.ToString().Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter the comments!..');", true);
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please enter the comments!..')", true);
                }
                else
                {
                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();

                    //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                    //{
                    TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                    TicketingObjBo.TITLE = "";
                    TicketingObjBo.ISSDESC = "";
                    TicketingObjBo.CLIENT = "";
                    TicketingObjBo.FRMUSR = "";
                    TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                    TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                    //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                    TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER

                    //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];

                    //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 
                    TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                    TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                    TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                    TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                    TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                    //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                    TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                    TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status
                    if (FU_Isssue.HasFile)
                    {
                        foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                        {
                            TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                            TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                            TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                            //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                            uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                        }
                    }

                    //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                    //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                    //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                    TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                    TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                    TicketingObjBo.CREATED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                    TicketingObjBo.Flag = 2;
                    TicketingObjBo.TICKETACTION = "COMPLETED";
                    TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                    TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                    TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !')", true);
                    //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                    SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                    ClearFields();
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Updated Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !'); parent.location.href='IssueTracker.aspx'", true);

                    TicketingToolbo TicketingObjBo2 = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl2 = new TicketingToolbl();
                    List<TicketingToolbo> TicketingboList2 = new List<TicketingToolbo>();

                    TicketingboList2 = TicketingObjBl2.Load_Ticket(long.Parse(ViewState["TicketID"].ToString().Trim()));
                    ViewState["TIMERSTS"] = TicketingboList2[0].STATUS == null ? "0" : TicketingboList2[0].STATUS.ToString().Trim();
                    ViewState["CATEGORYIDSLA"] = TicketingboList2[0].CATEGORY == null ? "0" : TicketingboList2[0].CATEGORY.ToString().Trim();
                    if (ViewState["CATEGORYIDSLA"].ToString().Trim() == "2" || ViewState["CATEGORYIDSLA"].ToString().Trim() == "3")
                    {
                        if (ViewState["TIMERSTS"].ToString().Trim() != "1" && ViewState["TIMERSTS"].ToString().Trim() != "4" && ViewState["TIMERSTS"].ToString().Trim() != "7" && ViewState["TIMERSTS"].ToString().Trim() != "8" && ViewState["TIMERSTS"].ToString().Trim() != "9" && ViewState["TIMERSTS"].ToString().Trim() != "11" && ViewState["TIMERSTS"].ToString().Trim() != "12" && ViewState["TIMERSTS"].ToString().Trim() != "13")
                        {
                            Session["CountdownTimer"] = null;
                            BindTimerData(long.Parse(ViewState["TicketID"].ToString().Trim()).ToString());
                            UpdatePanel1.Visible = true;
                        }
                        else
                        {
                            UpdatePanel1.Visible = false;
                        }
                    }
                    else
                    {
                        UpdatePanel1.Visible = false;
                    }
                    Session["PendingPageIndex"] = "0";
                    Session["DDLStatusSearch"] = "0";
                    Session["DDLCustomerList"] = "0";
                    Session["TxtFromDate"] = "";
                    Session["TxtToDate"] = "";
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        //protected void DDLIssueStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        FU_Isssue = (FileUpload)Session["fuTicketAttachments"];

        //        if (ViewState["PrevStatusID"].ToString().Trim() == "8")
        //        {
        //            if (DDLIssueStatus.SelectedValue.ToString().Trim() != "9")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The ticket has to be closed if the ticket is in confimred status');", true);
        //                DDLIssueStatus.SelectedValue = "0";
        //            }
        //        }

        //        if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
        //        {
        //            if (User.Identity.Name != "cssteam")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status. Only cssteam can close the ticket');", true);
        //                DDLIssueStatus.SelectedValue = "0";
        //            }
        //        }
        //        if (DDLIssueStatus.SelectedValue.ToString().Trim() == "12")
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.');", true);
        //            DDLIssueStatus.SelectedValue = "0";
        //        }

        //        if (DDLIssueStatus.SelectedValue.ToString().Trim() == "4" || DDLIssueStatus.SelectedValue.ToString().Trim() == "7")
        //        {
        //            ddlAssignee.SelectedValue = ViewState["CLIENT"].ToString().Trim();
        //        }

        //        string Status = "";
        //        msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
        //        msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
        //        objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
        //        msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
        //        TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //        TicketingToolbl TicketingObjBl = new TicketingToolbl();

        //        TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);

        //        if (Status == "True") // client
        //        {
        //            //pending work
        //        }

        //        else if (User.Identity.Name == "cssteam")
        //        {
        //            //pending work
        //        }

        //        else if (objPIDashBoardLst.Count > 0) // Associate Managers
        //        {
        //            int result;
        //            if (int.TryParse(ViewState["CLIENT"].ToString().Trim(), out result))
        //            {
        //                if (ViewState["AGENTPERNR"].ToString().Trim() == User.Identity.Name)
        //                {
        //                    if (Cb_InReviewNeed.Checked)
        //                    {
        //                        //clients
        //                        if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "3" && DDLIssueStatus.SelectedValue.ToString().Trim() != "6" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InReview QAS,InReviewPRD or Customer Action.');", true);
        //                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                            DDLIssueStatus.SelectedValue = "0";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "3" && DDLIssueStatus.SelectedValue.ToString().Trim() != "6" && DDLIssueStatus.SelectedValue.ToString().Trim() != "7" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InReview QAS,InReviewPRD,Resolved or Customer Action.');", true);
        //                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                            DDLIssueStatus.SelectedValue = "0";
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "2" && DDLIssueStatus.SelectedValue.ToString().Trim() != "4" && DDLIssueStatus.SelectedValue.ToString().Trim() != "5" && DDLIssueStatus.SelectedValue.ToString().Trim() != "11" && DDLIssueStatus.SelectedValue.ToString().Trim() != "7" && DDLIssueStatus.SelectedValue.ToString().Trim() != "8" && DDLIssueStatus.SelectedValue.ToString().Trim() != "10" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13" &&  DDLIssueStatus.SelectedValue.ToString().Trim() != "11")
        //                    {
        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InProgress,UAT,Completed,Resolved,Confirmed,Reopen or Customer Action.');", true);
        //                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                        DDLIssueStatus.SelectedValue = "0";
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                if (ViewState["AGENTPERNR"].ToString().Trim() == User.Identity.Name)
        //                {
        //                    if (Cb_InReviewNeed.Checked)
        //                    {
        //                        //clients
        //                        if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "3" && DDLIssueStatus.SelectedValue.ToString().Trim() != "6" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InReview QAS,InReviewPRD or Customer Action.');", true);
        //                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                            DDLIssueStatus.SelectedValue = "0";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "3" && DDLIssueStatus.SelectedValue.ToString().Trim() != "6" && DDLIssueStatus.SelectedValue.ToString().Trim() != "7" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InReview QAS,InReviewPRD,Resolved or Customer Action.');", true);
        //                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                            DDLIssueStatus.SelectedValue = "0";
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "2" && DDLIssueStatus.SelectedValue.ToString().Trim() != "4" && DDLIssueStatus.SelectedValue.ToString().Trim() != "11" && DDLIssueStatus.SelectedValue.ToString().Trim() != "7" && DDLIssueStatus.SelectedValue.ToString().Trim() != "10" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
        //                    {
        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InProgress,UAT,Resolved,Reopen or Customer Action.');", true);
        //                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                        DDLIssueStatus.SelectedValue = "0";
        //                    }
        //                }
        //            }

        //        }


        //        else // Agent
        //        {
        //            int result;
        //            if (int.TryParse(ViewState["CLIENT"].ToString().Trim(), out result))
        //            {
        //                //numbers 
        //                //internal
        //                if (ViewState["AGENTPERNR"].ToString().Trim() == User.Identity.Name)
        //                {
        //                    if (Cb_InReviewNeed.Checked)
        //                    {
        //                        //clients
        //                        if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "3" && DDLIssueStatus.SelectedValue.ToString().Trim() != "6" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InReview QAS or InReviewPRD or Customer Action.');", true);
        //                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                            DDLIssueStatus.SelectedValue = "0";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "3" && DDLIssueStatus.SelectedValue.ToString().Trim() != "6" && DDLIssueStatus.SelectedValue.ToString().Trim() != "7" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InReview QAS or InReviewPRD or Resolved or Customer Action.');", true);
        //                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                            DDLIssueStatus.SelectedValue = "0";
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "2" && DDLIssueStatus.SelectedValue.ToString().Trim() != "4" && DDLIssueStatus.SelectedValue.ToString().Trim() != "5" && DDLIssueStatus.SelectedValue.ToString().Trim() != "11" && DDLIssueStatus.SelectedValue.ToString().Trim() != "7" && DDLIssueStatus.SelectedValue.ToString().Trim() != "8" && DDLIssueStatus.SelectedValue.ToString().Trim() != "10" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
        //                    {
        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InProgress,UAT,Completed,Resolved,Confirmed,Reopen or Customer Action.');", true);
        //                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                        DDLIssueStatus.SelectedValue = "0";
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                //string
        //                if (ViewState["CLIENT"].ToString().Trim() == "cssteam")
        //                {
        //                    //cssteam
        //                }
        //                else
        //                {
        //                    if (Cb_InReviewNeed.Checked)
        //                    {
        //                        //clients
        //                        if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "3" && DDLIssueStatus.SelectedValue.ToString().Trim() != "6" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InReview QAS or InReviewPRD or Customer Action.');", true);
        //                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                            DDLIssueStatus.SelectedValue = "0";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (DDLIssueStatus.SelectedValue.ToString().Trim() != "0" && DDLIssueStatus.SelectedValue.ToString().Trim() != "3" && DDLIssueStatus.SelectedValue.ToString().Trim() != "6" && DDLIssueStatus.SelectedValue.ToString().Trim() != "7" && DDLIssueStatus.SelectedValue.ToString().Trim() != "13")
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status! Please select either InReview QAS or InReviewPRD or Resolved or Customer Action.');", true);
        //                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Invalid Status!..')", true);
        //                            DDLIssueStatus.SelectedValue = "0";
        //                        }
        //                    }
        //                }
        //            }


        //        }

        //        if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
        //        {

        //            if (ViewState["PrevStatusID"].ToString().Trim() != "1" && ViewState["PrevStatusID"].ToString().Trim() != "2")
        //            {

        //                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Cancellation of ticket can be done before assigning to agent(new and inprogress) ..')", true);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Cancellation of ticket can be done only in new and inprogress status ..')", true);
        //                DDLIssueStatus.SelectedValue = "0";
        //            }
        //        }

        //        if (DDLIssueStatus.SelectedValue.ToString().Trim() == "6" || DDLIssueStatus.SelectedValue.ToString().Trim() == "7")
        //        {
        //            if (ViewState["AGENTPERNR"].ToString().Trim() == User.Identity.Name)
        //            {
        //                divTrActhrs.Visible = true;
        //                TxtTrActhrs.Enabled = true;
        //            }
        //            else
        //            {
        //                divTrActhrs.Visible = true;
        //                TxtTrActhrs.Enabled = false;
        //            }
        //        }
        //        else
        //        {
        //            //if (ViewState["AGENTPERNR"].ToString().Trim() == User.Identity.Name)
        //            //{
        //            //    divTrActhrs.Visible = true;
        //            //    TxtTrActhrs.Enabled = false;                    
        //            //}
        //            //else
        //            //{
        //            divTrActhrs.Visible = true;
        //            TxtTrActhrs.Enabled = false;
        //            // }
        //        }
        //        DDLIssueStatus.Focus();
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        //}

        protected void DDLIssueStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string Status = "";

                msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
                msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                TicketingObjBl.CheckIfclients(1, User.Identity.Name, ref Status);



                FU_Isssue = (FileUpload)Session["fuTicketAttachments"];

                //Confirm tickets has to be closed
                if (ViewState["PrevStatusID"].ToString().Trim() == "8")
                {
                    if (DDLIssueStatus.SelectedValue.ToString().Trim() != "9")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The ticket has to be closed if the ticket is in confimred status');", true);
                        DDLIssueStatus.SelectedValue = "0";
                    }
                }

                //ONly css teeam has to close the tickets
                if (DDLIssueStatus.SelectedValue.ToString().Trim() == "9")
                {
                    if (User.Identity.Name != "cssteam")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status. Only cssteam can close the ticket');", true);
                        DDLIssueStatus.SelectedValue = "0";
                    }
                }

                //no one can select Closed TR
                if (DDLIssueStatus.SelectedValue.ToString().Trim() == "12")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.');", true);
                    DDLIssueStatus.SelectedValue = "0";
                }

                //If UAT and Resolved sts then fill client by default
                if (DDLIssueStatus.SelectedValue.ToString().Trim() == "4" || DDLIssueStatus.SelectedValue.ToString().Trim() == "7" || DDLIssueStatus.SelectedValue.ToString().Trim() == "13")
                {
                    ddlAssignee.SelectedValue = ViewState["CLIENT"].ToString().Trim();
                }


                //if (Status == "True")
                //{
                // client(External Ticket)
                // no Status selection for external client so no need of validation
                //}


                else if ((ViewState["CLIENT"].ToString().Trim() == User.Identity.Name) && (Status == "False"))
                {
                    // internal client(Internal Ticket)

                    if (Cb_InReviewNeed.Checked)
                    {
                        if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "1") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select new status  or cancel the ticket');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "2")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "14"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Inprogress or cancel the ticket');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "3")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "3"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select InReview QAS');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "4")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "5") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Completed or Inprogress');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "5")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "5"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select Completed');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "6")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "6"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select InReview PRD');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }

                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "7")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "10") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "8"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Confirm or Reopen');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "8")
                        {
                            // cannot edit confirm tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "9")
                        {
                            // cannot edit
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "10")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "10"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select Reopen');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "11")
                        {
                            // cannot edit cancelled tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "12")
                        {
                            // cannot edit closed tr tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "13")
                        {
                            // No Selection by default previous status is selected
                        }
                    }
                    else
                    {
                        if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "1") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select new status or cancel the ticket');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "2")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "14"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Inprogress or cancel the ticket');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "3")
                        {
                            // review not needed will not have Inreview Qas status
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "4")
                        {
                            // review not needed will not have UAT status
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "5")
                        {
                            // review not needed will not have Completed status
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "6")
                        {
                            // review not needed will not have Inreview PRD status
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "7")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "10") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "8"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Confirm or Reopen');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "8")
                        {
                            // cannot edit confirm tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "9")
                        {
                            // cannot edit
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "10")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "10"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select Reopen');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "11")
                        {
                            // cannot edit cancelled tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "12")
                        {
                            // cannot edit closed tr tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "13")
                        {
                            // No Selection by default previous status is selected
                        }
                    }

                }
                else if (User.Identity.Name == "cssteam")
                {
                    // CSS Team 

                    if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                    {
                        if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11"))
                        {
                            //Invalid Status msg
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Inprogress or cancel the ticket');", true);
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.SelectedValue = "0";
                        }
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "2")
                    {
                        if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "14"))
                        {
                            //Invalid Status msg
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Inprogress or cancel the ticket');", true);
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.SelectedValue = "0";
                        }
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "3")
                    {
                        if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "3"))
                        {
                            //Invalid Status msg
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select InReview QAS');", true);
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.SelectedValue = "0";
                        }
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "4")
                    {
                        if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "4"))
                        {
                            //Invalid Status msg
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select UAT');", true);
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.SelectedValue = "0";
                        }
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "5")
                    {
                        if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "5"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select Completed');", true);
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.SelectedValue = "0";
                        }
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "6")
                    {
                        if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "6"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select InReview PRD');", true);
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.SelectedValue = "0";
                        }
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "7")
                    {
                        if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "7"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select Resolved');", true);
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.SelectedValue = "0";
                        }
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "8")
                    {
                        if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "9"))
                        {
                            //Invalid Status msg
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select closed to close the ticket');", true);
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.SelectedValue = "0";
                        }
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "9")
                    {
                        //  no validation
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "10")
                    {
                        if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "10"))
                        {
                            //Invalid Status msg
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select Reopen status');", true);
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.SelectedValue = "0";
                        }
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "11")
                    {
                        // no validation
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "12")
                    {
                        // no validation
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "13")
                    {
                        // no validation
                    }
                    if (ViewState["PrevStatusID"].ToString().Trim() == "14")
                    {
                        ddlAssignee.Enabled = true;
                    }
                    int parv;

                    if (!int.TryParse(ddlAssignee.SelectedValue, out parv))
                    {
                        if (DDLIssueStatus.SelectedValue == "2" && DDLIssueStatus.SelectedValue == "14")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status..!');", true);
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.SelectedValue = "0";
                        }
                    }
                }
                else if ((objPIDashBoardLst.Count > 0) && (ViewState["AGENTPERNR"].ToString().Trim() != User.Identity.Name))
                {
                    // Associate Managers and not Agent

                    if (Cb_InReviewNeed.Checked)
                    {
                        if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                        {
                            // no validation since new status tickets cannot be viewd by Managers
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "2")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "14"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Inprogress, customer action or cancel the ticket');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "3")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "4"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Inprogress or UAT');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "4")
                        {
                            // cannot edit msg
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "4"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select UAT');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "5")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "5") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Completed or customer action');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "6")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "7"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Inprogress or Resolved');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "7")
                        {
                            // cannot edit msg
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "7"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select resolved');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "8")
                        {
                            // cannot edit confirm tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "9")
                        {
                            // cannot edit closed tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "10")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "10") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either reopen or customer action');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "11")
                        {
                            // cannot edit cancelled tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "12")
                        {
                            // cannot edit closed TR tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "13")
                        {
                            // cannot edit tickets which are in customer action
                        }

                    }
                    else
                    {
                        if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                        {
                            // no validation since new status tickets cannot be viewd by Managers
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "2")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "14"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Inprogress, Cutomer action or cancel the ticket');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "3")
                        {
                            // no validation since internal tickets does not have InReview QAS 

                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "4")
                        {
                            //  // no validation since internal tickets does not have UAT  
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "5")
                        {
                            //  // no validation since internal tickets does not have Completed
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "6")
                        {
                            //  // no validation since internal tickets does not have InReview PRD 
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "7")
                        {
                            // cannot edit msg
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "7"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select resolved');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "8")
                        {
                            // cannot edit confirm tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "9")
                        {
                            // cannot edit closed tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "10")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "10") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either reopen or customer action');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "11")
                        {
                            // cannot edit cancelled tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "12")
                        {
                            // cannot edit closed TR tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "13")
                        {
                            // cannot edit tickets which are in customer action
                        }

                    }
                    int parsedValue, parv;
                    if ((int.TryParse(HttpContext.Current.User.Identity.Name, out parv)) && (!int.TryParse(ddlAssignee.SelectedValue.ToString().Trim(), out parsedValue)) &&
                        ((DDLIssueStatus.SelectedValue.ToString().Trim() != "4") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "7") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13")))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Client or CSSTeam cannot process this status further, please select correct status..!')", true); //'" + DDLIssueStatus.SelectedItem.ToString().Trim() + "'
                        DDLIssueStatus.SelectedValue = "0";
                    }

                    if (DDLIssueStatus.SelectedValue == "14")
                    {
                        if (ViewState["PrevStatusID"].ToString().Trim() != "2")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only InProgress status Tickets can be update to Hold status..!')", true); //'" + DDLIssueStatus.SelectedItem.ToString().Trim() + "'
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.Enabled = true;
                        }
                        else
                        {
                            if (DDLIssueCategory.SelectedValue == "2" || DDLIssueCategory.SelectedValue == "3")
                            {
                                ddlAssignee.SelectedValue = User.Identity.Name.Trim();
                                ddlAssignee.Enabled = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only Support or Incident category Tickets can be update to Hold status..!')", true); //'" + DDLIssueStatus.SelectedItem.ToString().Trim() + "'
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.Enabled = true;
                            }
                        }
                        RF_plannedHr.Enabled = false;
                    }
                    else { ddlAssignee.Enabled = true; }


                    if (ViewState["PrevStatusID"].ToString().Trim() == "14")
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only InProgress status Tickets can be update to Hold status..!')", true); //'" + DDLIssueStatus.SelectedItem.ToString().Trim() + "'
                        DDLIssueStatus.SelectedValue = "2";
                        ddlAssignee.SelectedValue = User.Identity.Name.Trim();
                        ddlAssignee.Enabled = false;
                        DDLIssueStatus.Enabled = false;
                    }
                }
                else
                {
                    // Agent

                    if (Cb_InReviewNeed.Checked)
                    {
                        if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                        {
                            // no validation since new status tickets cannot be viewd by Managers
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "2")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "3") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "14"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either InReview QAS, Inprogress or Customer Action');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "3")
                        {
                            //cannot edit
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "4")
                        {
                            //cannot edit
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "5")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "6") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "5"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either InReview PRD, Completed or Customer Action');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "6")
                        {
                            //cannot edit
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "7")
                        {
                            //cannot edit
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "8")
                        {
                            //cannot edit
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "9")
                        {
                            //cannot edit
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "10")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "3") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "10"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either InReview QAS, Reopen or Customer Action');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "11")
                        {
                            //cannot edit
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "12")
                        {
                            //cannot edit
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "13")
                        {
                            //cannot edit
                        }
                    }
                    else
                    {
                        if (ViewState["PrevStatusID"].ToString().Trim() == "1")
                        {
                            // no validation since new status tickets cannot be viewd by Managers
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "2")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "7") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "2") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "14"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Resolved, Inprogress or Customer Action');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "3")
                        {
                            // no validation since internal tickets does not have InReview QAS 
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "4")
                        {
                            // no validation since internal tickets does not have UAT
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "5")
                        {
                            // no validation since internal tickets does not have completd
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "6")
                        {
                            // no validation since internal tickets does not have InReview PRD 
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "7")
                        {
                            //cannot edit resolved tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "8")
                        {
                            //cannot edit confirm tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "9")
                        {
                            //cannot edit closed tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "10")
                        {
                            if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "0") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "7") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "10") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13"))
                            {
                                //Invalid Status msg
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Status.. Please select either Resolved, Customer action or Reopen');", true);
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.SelectedValue = "0";
                            }
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "11")
                        {
                            //cannot edit cancelled tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "12")
                        {
                            //cannot edit closed TR Tickets
                        }
                        if (ViewState["PrevStatusID"].ToString().Trim() == "13")
                        {
                            //cannot edit customer action tickets
                        }
                    }
                    int parsedValue, parv;
                    if ((int.TryParse(HttpContext.Current.User.Identity.Name, out parv)) && (!int.TryParse(ddlAssignee.SelectedValue.ToString().Trim(), out parsedValue)) &&
                        ((DDLIssueStatus.SelectedValue.ToString().Trim() != "4") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "7") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "11") && (DDLIssueStatus.SelectedValue.ToString().Trim() != "13")))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Client or CSSTeam cannot process this status further, please select correct status..!')", true); //'" + DDLIssueStatus.SelectedItem.ToString().Trim() + "'
                        DDLIssueStatus.SelectedValue = "0";
                    }

                    if (DDLIssueStatus.SelectedValue == "14")
                    {
                        if (ViewState["PrevStatusID"].ToString().Trim() != "2")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only InProgress status Tickets can be update to Hold status..!')", true); //'" + DDLIssueStatus.SelectedItem.ToString().Trim() + "'
                            DDLIssueStatus.SelectedValue = "0";
                            ddlAssignee.Enabled = true;
                        }
                        else
                        {
                            if (DDLIssueCategory.SelectedValue == "2" || DDLIssueCategory.SelectedValue == "3")
                            {
                                ddlAssignee.SelectedValue = User.Identity.Name.Trim();
                                ddlAssignee.Enabled = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only Support or Incident category Tickets can be update to Hold status..!')", true); //'" + DDLIssueStatus.SelectedItem.ToString().Trim() + "'
                                DDLIssueStatus.SelectedValue = "0";
                                ddlAssignee.Enabled = true;
                            }
                        }
                    }
                    else { ddlAssignee.Enabled = true; }


                    if (ViewState["PrevStatusID"].ToString().Trim() == "14")
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only InProgress status Tickets can be update to Hold status..!')", true); //'" + DDLIssueStatus.SelectedItem.ToString().Trim() + "'
                        DDLIssueStatus.SelectedValue = "2";
                        ddlAssignee.SelectedValue = User.Identity.Name.Trim();
                        ddlAssignee.Enabled = false;
                        DDLIssueStatus.Enabled = false;
                    }
                    RF_plannedHr.Enabled = false;
                }


                // If Previous status is in Inprogress and New only then cancell can be done
                if (DDLIssueStatus.SelectedValue.ToString().Trim() == "11")
                {

                    if (ViewState["PrevStatusID"].ToString().Trim() != "1" && ViewState["PrevStatusID"].ToString().Trim() != "2")
                    {

                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Cancellation of ticket can be done before assigning to agent(new and inprogress) ..')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Cancellation of ticket can be done only in new and inprogress status ..')", true);
                        DDLIssueStatus.SelectedValue = "0";
                    }
                }


                //Actual hrs is editable only in INprogress and Resolved sts for Agents
                if (DDLIssueStatus.SelectedValue.ToString().Trim() == "6" || DDLIssueStatus.SelectedValue.ToString().Trim() == "7")
                {
                    if (ViewState["AGENTPERNR"].ToString().Trim() == User.Identity.Name)
                    {
                        divTrActhrs.Visible = true;
                        TxtTrActhrs.Enabled = true;
                    }
                    else
                    {
                        divTrActhrs.Visible = true;
                        TxtTrActhrs.Enabled = false;
                    }
                }
                else
                {
                    //if (ViewState["AGENTPERNR"].ToString().Trim() == User.Identity.Name)
                    //{
                    //    divTrActhrs.Visible = true;
                    //    TxtTrActhrs.Enabled = false;                    
                    //}
                    //else
                    //{
                    divTrActhrs.Visible = true;
                    TxtTrActhrs.Enabled = false;
                    // }
                }

                //int parsedValue;
                //if (int.TryParse(HttpContext.Current.User.Identity.Name, out parsedValue))
                //{
                //    if (!int.TryParse(ddlAssignee.SelectedValue.ToString().Trim(), out parsedValue))
                //    {
                //        if ((DDLIssueStatus.SelectedValue.ToString().Trim() != "4") || (DDLIssueStatus.SelectedValue.ToString().Trim() != "7") || (DDLIssueStatus.SelectedValue.ToString().Trim() != "11") || (DDLIssueStatus.SelectedValue.ToString().Trim() != "13"))
                //        {
                //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Client or CSSTeam cannot process '" + DDLIssueStatus.SelectedItem.ToString().Trim() + " 'status futhur please select correct status..!')", true);
                //            DDLIssueStatus.SelectedValue = "0";
                //        }
                //    }
                //}
                //if ((DDLIssueStatus.SelectedValue.ToString().Trim() == "8") && (ViewState["CLIENT"].ToString().Trim() == User.Identity.Name))
                //{
                //    mp1.Show();
                //}
                //else
                //{
                //    mp1.Hide();
                //}





                DDLIssueStatus.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        }

        protected void LblCreateTask_Click(object sender, EventArgs e)
        {
            try
            {
                LblCreateTask.Visible = false;
                TicketDiv.Visible = false;
                LblCreateTask.Visible = false;
                TaskOuterDiv.Visible = true;
                LoadTaskAssignee(0, 0);
                LoadTaskStatus(2, User.Identity.Name, long.Parse(ViewState["TicketID"].ToString().Trim())); //LoadTaskStatus(int type, string pernr, long ticketid) 
                BindTaskDataofTicket(long.Parse(ViewState["TicketID"].ToString().Trim()), User.Identity.Name);
                divTaskLoad.Visible = true;
                TaskCreateUpdate.Visible = false;
                AccordionTask.Visible = false;
                ViewState["TaskBackBtn"] = "View";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void TASKADD_Click(object sender, EventArgs e)
        {
            try
            {
                long? TaskRefIdOut = 0;
                bool check = FU_TaskAttachments.HasFile;
                //LoadfileuploadTask();
                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();


                TicketingObjBo.TASKID = 0;
                TicketingObjBo.TICKETID = long.Parse(ViewState["TicketID"].ToString().Trim());
                TicketingObjBo.TASKTITLE = TxtTaskTitle.Text.ToString().Trim();
                TicketingObjBo.TASKDESC = TxtTaskIssDesc.Text.ToString().Trim();
                TicketingObjBo.TASKFRM = User.Identity.Name; //LOGGED USER
                TicketingObjBo.TASKAGENT = DDLTaskAssignee.SelectedValue.ToString().Trim();
                TicketingObjBo.TASKCCMAILID = TXTTaskCCMAILID.Text.ToString().Trim();
                TicketingObjBo.TaskComments = TxtTaskComments.Text.ToString().Trim();
                TicketingObjBo.TASKFROMSTATUS = 0;
                TicketingObjBo.TASKTOSTATUS = int.Parse(DDLTaskStatus.SelectedValue.ToString().Trim());


                if (FU_TaskAttachments.HasFile)
                {
                    foreach (HttpPostedFile uploadedFile in FU_TaskAttachments.PostedFiles)
                    {
                        TicketingObjBo.TASKATTACHEMENT_FILE += string.Format("{0}|", FU_TaskAttachments.HasFile ? "Yes" : "No");
                        TicketingObjBo.TASKATTACHEMENT_FID += string.Format("{0}|", FU_TaskAttachments.HasFile ? uploadedFile.FileName : "");
                        TicketingObjBo.TASKATTACHEMENT_FPATH += string.Format("{0}| ", FU_TaskAttachments.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                        //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                        uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                    }
                }

                //TicketingObjBo.TASKATTACHEMENT_FILE = FU_TaskAttachments.HasFile ? "Yes" : "No";
                //TicketingObjBo.TASKATTACHEMENT_FID = FU_TaskAttachments.HasFile ? FU_TaskAttachments.PostedFile.FileName : "";
                //TicketingObjBo.TASKATTACHEMENT_FPATH = FU_TaskAttachments.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_TaskAttachments.FileName) : "";
                TicketingObjBo.TASKCREATED_ON = DateTime.Now;
                TicketingObjBo.TASKCREATED_BY = User.Identity.Name;
                TicketingObjBo.TASKMODIFIED_ON = new DateTime(1900, 01, 01);
                TicketingObjBo.TASKMODIFIED_BY = "";
                TicketingObjBo.Flag = 1;
                TicketingObjBo.TaskPlndhrs = string.IsNullOrEmpty(TxtTrTaskPlnhrs.Text) ? 0 : decimal.Parse(TxtTrTaskPlnhrs.Text.ToString().Trim());
                TicketingObjBo.TaskActualhrs = string.IsNullOrEmpty(TxtTrTaskActhrs.Text) ? 0 : decimal.Parse(TxtTrTaskActhrs.Text.ToString().Trim());
                TicketingObjBl.CREATE_TICKETTASK(TicketingObjBo, ref TaskRefIdOut);

                SendMailTASK(TicketingObjBo.TICKETID, TaskRefIdOut, 1, TicketingObjBo.TASKFROMSTATUS, TicketingObjBo.TASKTOSTATUS);

                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Task Created Successfully!..')", true);

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Key", "alert('Ticket Created Successfully !')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Task Created Successfully!..')", true);
                //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");

                if (ViewState["TypeValue"].ToString().Trim() == "1")
                {
                    TaskOuterDiv.Visible = true;
                    BindTaskDataofTicket(long.Parse(ViewState["TicketID"].ToString().Trim()), User.Identity.Name);
                    divTaskLoad.Visible = true;
                    TaskCreateUpdate.Visible = false;
                    AccordionTask.Visible = false;
                    ViewState["TaskBackBtn"] = "View";
                }
                else
                {
                    TicketDiv.Visible = false;
                    LblCreateTask.Visible = false;
                    TaskOuterDiv.Visible = true;
                    BindTaskDataofTicket(long.Parse(ViewState["TicketID"].ToString().Trim()), User.Identity.Name);
                    divTaskLoad.Visible = true;
                    TaskCreateUpdate.Visible = false;
                    AccordionTask.Visible = false;
                    ViewState["TaskBackBtn"] = "DirectView";
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void SendMailTASK(long? TICKETID, long? TASKID, int Type, int? TASKFROMSTATUS, int? TASKTOSTATUS)
        {
            try
            {
                string body = "";
                string RecipientsString = "";
                string strPernr_Mail = "";
                string PERNRNAME = "";
                string PERNRMAILID = "";
                string AGENTID = "";
                string AGENTNAME = "";
                string AGENTMAILID = "";
                string MNGRID = "";
                string MNGRNAME = "";
                string MNGRMAILID = "";
                string CSSTEAMMAILID = "";
                string TITLE = "";
                string CCMAILIDS = "";

                string TASKLINEID = "";
                TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
                objTicketingToolDataContext.usp_tcikety_get_MailListTASK(TICKETID, TASKID, Type, User.Identity.Name, ref PERNRNAME, ref PERNRMAILID,
                ref AGENTID, ref AGENTNAME, ref AGENTMAILID, ref MNGRID, ref MNGRNAME, ref MNGRMAILID, ref CSSTEAMMAILID, ref TITLE, ref CCMAILIDS, ref  TASKLINEID);
                PERNRNAME = string.IsNullOrEmpty(PERNRNAME) ? "" : PERNRNAME;
                PERNRMAILID = string.IsNullOrEmpty(PERNRMAILID) ? "" : PERNRMAILID;
                AGENTID = string.IsNullOrEmpty(AGENTID) ? "" : AGENTID;
                AGENTNAME = string.IsNullOrEmpty(AGENTNAME) ? "" : AGENTNAME;
                AGENTMAILID = string.IsNullOrEmpty(AGENTMAILID) ? "" : AGENTMAILID;
                MNGRID = string.IsNullOrEmpty(MNGRID) ? "" : MNGRID;
                MNGRNAME = string.IsNullOrEmpty(MNGRNAME) ? "" : MNGRNAME;
                MNGRMAILID = string.IsNullOrEmpty(MNGRMAILID) ? "" : MNGRMAILID;
                CSSTEAMMAILID = string.IsNullOrEmpty(CSSTEAMMAILID) ? "" : CSSTEAMMAILID;
                TITLE = string.IsNullOrEmpty(TITLE) ? "" : TITLE;
                CCMAILIDS = string.IsNullOrEmpty(CCMAILIDS) ? "" : CCMAILIDS;

                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                string strSubject = string.Empty;
                if (Type == 1)
                {
                    strSubject = "Task " + TASKLINEID + " has been Created by " + PERNRNAME + " | " + User.Identity.Name + ".";
                    RecipientsString = AGENTMAILID;
                    strPernr_Mail = PERNRMAILID + " , " + MNGRMAILID + " , " + CSSTEAMMAILID + "," + CCMAILIDS;
                    body = "Dear " + AGENTNAME + " ,<br/><br/>";
                    body += "Below Task has been created.<br/>";
                    //body += "to = " + RecipientsString + "</br>" + "cc = " + strPernr_Mail + "<br/>";
                    body += "<table><tr><td>Task No</td> <td>: </td> <td>" + TASKLINEID + "</td></tr>";
                    body += "<tr><td>Ref Ticket No</td> <td>: </td> <td>" + TICKETID + "</td></tr>";
                    body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr></table><br/><br/>";
                    body += "Thanks & Regards,<br/>";
                    body += "ITChamps ITSM Team";
                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    // iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });
                    email.IsBackground = true;
                    email.Start();
                }
                else if (Type == 2)
                {
                    if (TASKTOSTATUS == 2)
                    {
                        strSubject = "Task " + TASKLINEID + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + ".";
                        RecipientsString = AGENTMAILID;
                        strPernr_Mail = PERNRMAILID + " , " + MNGRMAILID + " , " + CSSTEAMMAILID + "," + CCMAILIDS;
                        body = "Dear " + AGENTNAME + " ,<br/><br/>";
                        //body += "to = " + RecipientsString + "</br>" + "cc = " + strPernr_Mail + "<br/>";
                        body += "Below Task has been assigned to your queue.<br/>";
                        body += "<table><tr><td>Task No</td> <td>: </td> <td>" + TASKLINEID + "</td></tr>";
                        body += "<tr><td>Ref Ticket No</td> <td>: </td> <td>" + TICKETID + "</td></tr>";
                        body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr></table><br/><br/>";
                        body += "Thanks & Regards,<br/>";
                        body += "ITChamps ITSM Team";
                        body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                        // iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                        Thread email = new Thread(delegate()
                        {
                            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                        });
                        email.IsBackground = true;
                        email.Start();
                    }
                    else if (TASKTOSTATUS == 7)
                    {
                        strSubject = "Task " + TASKLINEID + " has been resolved by " + PERNRNAME + " | " + User.Identity.Name + ".";
                        RecipientsString = AGENTMAILID;
                        strPernr_Mail = PERNRMAILID + " , " + MNGRMAILID + " , " + CSSTEAMMAILID + "," + CCMAILIDS;
                        body = "Dear " + AGENTNAME + " ,<br/><br/>";
                        //body += "to = " + RecipientsString + "</br>" + "cc = " + strPernr_Mail + "<br/>";
                        body += "Below Task has been resolved.<br/>";
                        body += "<table><tr><td>Task No</td> <td>: </td> <td>" + TASKLINEID + "</td></tr>";
                        body += "<tr><td>Ref Ticket No</td> <td>: </td> <td>" + TICKETID + "</td></tr>";
                        body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr></table><br/><br/>";
                        body += "Thanks & Regards,<br/>";
                        body += "ITChamps ITSM Team";
                        body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                        // iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                        Thread email = new Thread(delegate()
                        {
                            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                        });
                        email.IsBackground = true;
                        email.Start();
                    }
                }
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
                        divTaskLoad.Visible = false;
                        TaskCreateUpdate.Visible = true;
                        AccordionTask.Visible = true;
                        ClearTaskFields();
                        long TicketId = int.Parse(GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TICKETID"].ToString().Trim());
                        long TaskId = int.Parse(GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TASKID"].ToString().Trim());
                        LoadTaskAssignee(TicketId, TaskId);
                        //lblTaskTicketID.Text = GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TICKETID"].ToString().Trim();
                        //lblTaskID.Text = GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TASKID"].ToString().Trim();
                        lblTaskID.Text = GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TASKLINEID"].ToString().Trim();
                        ViewState["TicketTaskId"] = int.Parse(GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TICKETID"].ToString().Trim());
                        ViewState["TaskId"] = int.Parse(GV_Task.DataKeys[int.Parse(e.CommandArgument.ToString())]["TASKID"].ToString().Trim());
                        BindTaskData(TicketId, TaskId, User.Identity.Name);
                        TaskTicket.Visible = true;
                        BindTicketdataforTask(TicketId);

                        ViewState["TaskBackBtn"] = "View";
                        TASKBack.Visible = true;
                        LnkTaskBack.Visible = true;
                        TASKADD.Visible = false;
                        TaskUpdate.Visible = false;

                        if (User.Identity.Name == "cssteam")
                        {
                            TaskEdit.Visible = false;
                        }
                        else
                        {
                            TaskEdit.Visible = true;
                        }

                        if (User.Identity.Name != "cssteam")
                        {
                            if (ViewState["PrevTaskStatus"].ToString().Trim() == "7")
                            {
                                TaskEdit.Visible = false;
                            }
                            else
                            {
                                TaskEdit.Visible = true;
                            }
                        }
                        //TaskTicketID.Visible = true;
                        TaskID.Visible = true;



                        break;
                    default:
                        break;
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        }

        public void BindTicketdataforTask(long TicketId)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_Ticket(long.Parse(TicketId.ToString().Trim()));


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    MsgCls("No Records Found !", LblMsg, System.Drawing.Color.Red);
                    FVTICKETTASK.Visible = false;
                    FVTICKETTASK.DataSource = null;
                    FVTICKETTASK.DataBind();
                    return;
                }
                else
                {
                    MsgCls("", LblMsg, System.Drawing.Color.Transparent);
                    FVTICKETTASK.Visible = true;
                    FVTICKETTASK.DataSource = null;
                    FVTICKETTASK.DataBind();
                    FVTICKETTASK.DataSource = TicketingboList;
                    FVTICKETTASK.DataBind();


                    LoadTicketAttachmentsdatafortask(long.Parse(TicketId.ToString().Trim()));

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void LoadTicketAttachmentsdatafortask(long TicketId)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_Ticket_Attachments(TicketId, User.Identity.Name);


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    GrdTaskTickAtt.Visible = false;
                    GrdTaskTickAtt.DataSource = null;
                    GrdTaskTickAtt.DataBind();
                    return;
                }
                else
                {
                    GrdTaskTickAtt.Visible = true;
                    GrdTaskTickAtt.DataSource = null;
                    GrdTaskTickAtt.DataBind();
                    GrdTaskTickAtt.DataSource = TicketingboList;
                    GrdTaskTickAtt.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void BindTaskDataofTicket(long TicketId, string pernr)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_TaskDataofTicket(TicketId, pernr);


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    MsgCls("No Records Found !", LblPopUp, System.Drawing.Color.White);

                    GV_Task.Visible = false;
                    GV_Task.DataSource = null;
                    GV_Task.DataBind();
                    return;
                }
                else
                {
                    MsgCls("", LblPopUp, System.Drawing.Color.Transparent);

                    GV_Task.Visible = true;
                    GV_Task.DataSource = null;
                    GV_Task.DataBind();
                    GV_Task.DataSource = TicketingboList;
                    GV_Task.DataBind();

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void BindTaskData(long TicketId, long TaskId, string pernr)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_TaskData(long.Parse(TicketId.ToString().Trim()), long.Parse(TaskId.ToString().Trim()), pernr);

                TxtTaskTitle.Text = TicketingboList[0].TASKTITLE == null ? "" : TicketingboList[0].TASKTITLE.ToString().Trim();
                TxtTaskIssDesc.Text = TicketingboList[0].TASKDESC == null ? "" : TicketingboList[0].TASKDESC.ToString().Trim();
                DDLTaskAssignee.SelectedValue = TicketingboList[0].TASKAGENT == null ? "0" : TicketingboList[0].TASKAGENT.ToString().Trim();
                TXTTaskCCMAILID.Text = TicketingboList[0].TASKCCMAILID == null ? "" : TicketingboList[0].TASKCCMAILID.ToString().Trim();
                DDLTaskStatus.SelectedValue = TicketingboList[0].TASKTOSTATUS == null ? "0" : TicketingboList[0].TASKTOSTATUS.ToString().Trim();
                TxtTrTaskPlnhrs.Text = TicketingboList[0].TaskPlndhrs == null ? "0" : TicketingboList[0].TaskPlndhrs.ToString().Trim();
                TxtTrTaskActhrs.Text = TicketingboList[0].TaskActualhrs == null ? "0" : TicketingboList[0].TaskActualhrs.ToString().Trim();
                ViewState["PrevTaskStatus"] = TicketingboList[0].TASKTOSTATUS == null ? "0" : TicketingboList[0].TASKTOSTATUS.ToString().Trim();
                ViewState["TaskCreatdBy"] = TicketingboList[0].TASKCREATED_BY == null ? "" : TicketingboList[0].TASKCREATED_BY.ToString().Trim();
                ViewState["TASKACTUALAGENT"] = TicketingboList[0].TASKACTUALAGENT == null ? "0" : TicketingboList[0].TASKACTUALAGENT.ToString().Trim();
                LoadTicketTaskStatus(long.Parse(TicketId.ToString().Trim()), long.Parse(TaskId.ToString().Trim()));
                LoadTicketTaskComments(long.Parse(TicketId.ToString().Trim()), long.Parse(TaskId.ToString().Trim()));
                LoadTicketTaskAttachments(long.Parse(TicketId.ToString().Trim()), long.Parse(TaskId.ToString().Trim()));

                TxtTaskTitle.Enabled = false;
                TxtTaskIssDesc.Enabled = false;
                DDLTaskAssignee.Enabled = false;
                DDLTaskStatus.Enabled = false;
                TxtTaskComments.Enabled = false;
                TxtTrTaskPlnhrs.Enabled = false;
                TXTTaskCCMAILID.Enabled = false;
                TxtTrTaskActhrs.Enabled = false;
                FU_TaskAttachments.Enabled = false;
                if (FU_TaskAttachments != null)
                {
                    if (FU_TaskAttachments.HasFile)
                    {
                        FU_TaskAttachments.Attributes.Clear();
                    }
                }
                Session["fuAttachments"] = null;
                fuAttachmentsfname.Text = string.Empty;
                //}
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void LoadTicketTaskComments(long TicketID, long taskid)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_Task_Comments(TicketID, taskid);


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    Grd_TaskComments.Visible = false;
                    Grd_TaskComments.DataSource = null;
                    Grd_TaskComments.DataBind();
                    return;
                }
                else
                {
                    Grd_TaskComments.Visible = true;
                    Grd_TaskComments.DataSource = null;
                    Grd_TaskComments.DataBind();
                    Grd_TaskComments.DataSource = TicketingboList;
                    Grd_TaskComments.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void LoadTicketTaskAttachments(long TicketID, long taskid)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_Task_Attachments(TicketID, taskid);


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    Grd_TaskAttachments.Visible = false;
                    Grd_TaskAttachments.DataSource = null;
                    Grd_TaskAttachments.DataBind();
                    return;
                }
                else
                {
                    Grd_TaskAttachments.Visible = true;
                    Grd_TaskAttachments.DataSource = null;
                    Grd_TaskAttachments.DataBind();
                    Grd_TaskAttachments.DataSource = TicketingboList;
                    Grd_TaskAttachments.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void LoadTicketTaskStatus(long TicketID, long taskid)
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.Load_Task_Status(TicketID, taskid);


                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                    Grd_TaskStatus.Visible = false;
                    Grd_TaskStatus.DataSource = null;
                    Grd_TaskStatus.DataBind();
                    return;
                }
                else
                {
                    Grd_TaskStatus.Visible = true;
                    Grd_TaskStatus.DataSource = null;
                    Grd_TaskStatus.DataBind();
                    Grd_TaskStatus.DataSource = TicketingboList;
                    Grd_TaskStatus.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void BtnTaskCreate_Click(object sender, EventArgs e)
        {
            try
            {
                divTaskLoad.Visible = false;
                TaskCreateUpdate.Visible = true;
                AccordionTask.Visible = false;
                ClearTaskFields();
                TxtTaskTitle.Enabled = true;
                TxtTaskIssDesc.Enabled = true;
                DDLTaskAssignee.Enabled = true;
                DDLTaskStatus.Enabled = true;
                TxtTaskComments.Enabled = true;
                FU_TaskAttachments.Enabled = true;
                TXTTaskCCMAILID.Enabled = true;
                TxtTrTaskActhrs.Enabled = false;
                if (ViewState["TypeValue"].ToString().Trim() == "1")
                {
                    ViewState["TaskBackBtn"] = "View";
                }
                else
                {
                    ViewState["TaskBackBtn"] = "DirectView";
                }

                TASKBack.Visible = true;
                LnkTaskBack.Visible = true;
                TASKADD.Visible = true;
                TaskUpdate.Visible = false;
                TaskEdit.Visible = false;
                //TaskTicketID.Visible = false;
                TaskID.Visible = false;
                //lblTaskTicketID.Text = "";
                lblTaskID.Text = "";
                TaskTicket.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void ClearTaskFields()
        {
            try
            {
                //lblTaskTicketID.Text = "";
                lblTaskID.Text = "";
                TxtTaskTitle.Text = "";
                TxtTaskIssDesc.Text = "";
                TXTTaskCCMAILID.Text = "";
                DDLTaskAssignee.SelectedValue = "0";
                TxtTaskComments.Text = "";
                DDLTaskStatus.SelectedValue = "0";
                TxtTrTaskActhrs.Text = "";
                Grd_TaskComments.DataSource = null;
                Grd_TaskComments.DataBind();
                Grd_TaskStatus.DataSource = null;
                Grd_TaskStatus.DataBind();
                TaskTicket.Visible = false;
                FVTICKETTASK.DataSource = null;
                FVTICKETTASK.DataBind();
                GrdTaskTickAtt.DataSource = null;
                GrdTaskTickAtt.DataBind();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void TASKBack_Click(object sender, EventArgs e)
        {
            taskbackfunc();

        }

        protected void LnkTaskBack_Click(object sender, EventArgs e)
        {
            taskbackfunc();
        }


        protected void taskbackfunc()
        {
            try
            {
                if (ViewState["TaskBackBtn"].ToString().Trim() == "View")
                {
                    TaskOuterDiv.Visible = true;
                    BindTaskDataofTicket(long.Parse(ViewState["TicketID"].ToString().Trim()), User.Identity.Name);
                    divTaskLoad.Visible = true;
                    TaskCreateUpdate.Visible = false;
                    AccordionTask.Visible = false;
                    if (ViewState["TypeValue"].ToString().Trim() == "1")
                    {
                        ViewState["TaskBackBtn"] = "View";
                    }
                    else
                    {
                        ViewState["TaskBackBtn"] = "DirectView";
                    }

                }
                else if (ViewState["TaskBackBtn"].ToString().Trim() == "Edit")
                {
                    TaskOuterDiv.Visible = true;
                    divTaskLoad.Visible = false;
                    TaskCreateUpdate.Visible = true;
                    AccordionTask.Visible = true;
                    BindTaskData(long.Parse(ViewState["TicketTaskId"].ToString()), long.Parse(ViewState["TaskId"].ToString()), User.Identity.Name);
                    if (ViewState["TypeValue"].ToString().Trim() == "1")
                    {
                        ViewState["TaskBackBtn"] = "View";
                    }
                    else
                    {
                        ViewState["TaskBackBtn"] = "DirectView";
                    }

                    TASKBack.Visible = true;
                    LnkTaskBack.Visible = true;
                    TASKADD.Visible = false;
                    TaskUpdate.Visible = false;
                    TaskEdit.Visible = true;
                }

                else if (ViewState["TaskBackBtn"].ToString().Trim() == "DirectView")
                {
                    TicketDiv.Visible = false;
                    LblCreateTask.Visible = false;
                    TaskOuterDiv.Visible = true;
                    //LoadTaskAssignee();
                    //LoadTaskStatus(2, User.Identity.Name, long.Parse(Request.QueryString["Type"].ToString().Trim())); //LoadTaskStatus(int type, string pernr, long ticketid) 
                    BindTaskDataofTicket(long.Parse(Session["TransferdTicketId"].ToString().Trim()), User.Identity.Name);//Request.QueryString["Type"].ToString().Trim()
                    divTaskLoad.Visible = true;
                    TaskCreateUpdate.Visible = false;
                    AccordionTask.Visible = false;
                    ViewState["TaskBackBtn"] = "DirectView";
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void TaskUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                long? TaskRefIdOut = 0;
                bool check = FU_TaskAttachments.HasFile;
                //LoadfileuploadTask();
                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();


                TicketingObjBo.TASKID = long.Parse(ViewState["TaskId"].ToString().Trim());
                TicketingObjBo.TICKETID = long.Parse(ViewState["TicketID"].ToString().Trim());
                TicketingObjBo.TASKTITLE = TxtTaskTitle.Text.ToString().Trim();
                TicketingObjBo.TASKDESC = TxtTaskIssDesc.Text.ToString().Trim();
                TicketingObjBo.TASKFRM = User.Identity.Name; //LOGGED USER
                TicketingObjBo.TASKAGENT = DDLTaskAssignee.SelectedValue.ToString().Trim();
                TicketingObjBo.TASKCCMAILID = TXTTaskCCMAILID.Text.ToString().Trim();
                TicketingObjBo.TaskComments = TxtTaskComments.Text.ToString().Trim();
                TicketingObjBo.TASKFROMSTATUS = int.Parse(ViewState["PrevTaskStatus"].ToString().Trim());
                TicketingObjBo.TASKTOSTATUS = int.Parse(DDLTaskStatus.SelectedValue.ToString().Trim());

                if (FU_TaskAttachments.HasFile)
                {
                    foreach (HttpPostedFile uploadedFile in FU_TaskAttachments.PostedFiles)
                    {
                        TicketingObjBo.TASKATTACHEMENT_FILE += string.Format("{0}|", FU_TaskAttachments.HasFile ? "Yes" : "No");
                        TicketingObjBo.TASKATTACHEMENT_FID += string.Format("{0}|", FU_TaskAttachments.HasFile ? uploadedFile.FileName : "");
                        TicketingObjBo.TASKATTACHEMENT_FPATH += string.Format("{0}| ", FU_TaskAttachments.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                        //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                        uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                    }
                }

                //TicketingObjBo.TASKATTACHEMENT_FILE = FU_TaskAttachments.HasFile ? "Yes" : "No";
                //TicketingObjBo.TASKATTACHEMENT_FID = FU_TaskAttachments.HasFile ? FU_TaskAttachments.PostedFile.FileName : "";
                //TicketingObjBo.TASKATTACHEMENT_FPATH = FU_TaskAttachments.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_TaskAttachments.FileName) : "";
                TicketingObjBo.TASKCREATED_ON = DateTime.Now;
                TicketingObjBo.TASKCREATED_BY = User.Identity.Name;
                TicketingObjBo.TASKMODIFIED_ON = new DateTime(1900, 01, 01);
                TicketingObjBo.TASKMODIFIED_BY = User.Identity.Name;
                TicketingObjBo.Flag = 2;
                TicketingObjBo.TaskPlndhrs = string.IsNullOrEmpty(TxtTrTaskPlnhrs.Text) ? 0 : decimal.Parse(TxtTrTaskPlnhrs.Text.ToString().Trim());
                TicketingObjBo.TaskActualhrs = string.IsNullOrEmpty(TxtTrTaskActhrs.Text) ? 0 : decimal.Parse(TxtTrTaskActhrs.Text.ToString().Trim());
                TicketingObjBl.CREATE_TICKETTASK(TicketingObjBo, ref TaskRefIdOut);
                SendMailTASK(TicketingObjBo.TICKETID, TaskRefIdOut, 2, TicketingObjBo.TASKFROMSTATUS, TicketingObjBo.TASKTOSTATUS);
                // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Task Updated Successfully!..')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Task Updated Successfully!..');", true);

                if (ViewState["TypeValue"].ToString().Trim() == "1")
                {
                    TaskOuterDiv.Visible = true;
                    BindTaskDataofTicket(long.Parse(ViewState["TicketID"].ToString().Trim()), User.Identity.Name);
                    divTaskLoad.Visible = true;
                    TaskCreateUpdate.Visible = false;
                    AccordionTask.Visible = false;
                    ViewState["TaskBackBtn"] = "View";
                }
                else
                {
                    TicketDiv.Visible = false;
                    LblCreateTask.Visible = false;
                    TaskOuterDiv.Visible = true;
                    BindTaskDataofTicket(long.Parse(ViewState["TicketID"].ToString().Trim()), User.Identity.Name);
                    divTaskLoad.Visible = true;
                    TaskCreateUpdate.Visible = false;
                    AccordionTask.Visible = false;
                    ViewState["TaskBackBtn"] = "DirectView";
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        }

        //public void SendMail(long? TaskRefIdOut, int? FrmSts, int? ToSts, string TicketAct)
        //{
        //    try
        //    {
        //        TicketingToolbo TicketingObjBoC = new TicketingToolbo();
        //        TicketingToolbl TicketingObjBC = new TicketingToolbl();
        //        string StatusC = "";
        //        TicketingObjBC.CheckIfclients(1, User.Identity.Name, ref StatusC);

        //        string body = "";
        //        string RecipientsString = "";
        //        string strPernr_Mail = "";
        //        string PERNRNAME = "";
        //        string PERNRMAILID = "";
        //        string CLIENTID = "";
        //        string CLIENTNAME = "";
        //        string CLIENTMAILID = "";
        //        string FRMUSRID = "";
        //        string FRMUSRNAME = "";
        //        string FRMUSRMAILID = "";
        //        string TOASSIGNEEID = "";
        //        string TOASSIGNEENAME = "";
        //        string TOASSIGNEEMAILID = "";
        //        string AGENTID = "";
        //        string AGENTNAME = "";
        //        string AGENTMAILID = "";
        //        string MNGRID = "";
        //        string MNGRNAME = "";
        //        string MNGRMAILID = "";
        //        string CSSTEAMMAILID = "";
        //        string TITLE = "";
        //        string PRIORITY = "";
        //        string CCMAILIDS = "";

        //        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();

        //        objTicketingToolDataContext.usp_tcikety_get_MailList(TaskRefIdOut, User.Identity.Name, FrmSts, ToSts, ref PERNRNAME, ref PERNRMAILID, ref CLIENTID,
        //        ref CLIENTNAME, ref CLIENTMAILID, ref FRMUSRID, ref FRMUSRNAME, ref FRMUSRMAILID, ref TOASSIGNEEID, ref TOASSIGNEENAME, ref TOASSIGNEEMAILID,
        //        ref AGENTID, ref AGENTNAME, ref AGENTMAILID, ref MNGRID, ref MNGRNAME, ref MNGRMAILID, ref CSSTEAMMAILID, ref TITLE, ref PRIORITY, ref CCMAILIDS);


        //        PERNRNAME = string.IsNullOrEmpty(PERNRNAME) ? "" : PERNRNAME;
        //        PERNRMAILID = string.IsNullOrEmpty(PERNRMAILID) ? "" : PERNRMAILID;
        //        CLIENTID = string.IsNullOrEmpty(CLIENTID) ? "" : CLIENTID;
        //        CLIENTNAME = string.IsNullOrEmpty(CLIENTNAME) ? "" : CLIENTNAME;
        //        CLIENTMAILID = string.IsNullOrEmpty(CLIENTMAILID) ? "" : CLIENTMAILID;
        //        FRMUSRID = string.IsNullOrEmpty(FRMUSRID) ? "" : FRMUSRID;
        //        FRMUSRNAME = string.IsNullOrEmpty(FRMUSRNAME) ? "" : FRMUSRNAME;
        //        FRMUSRMAILID = string.IsNullOrEmpty(FRMUSRMAILID) ? "" : FRMUSRMAILID;
        //        TOASSIGNEEID = string.IsNullOrEmpty(TOASSIGNEEID) ? "" : TOASSIGNEEID;
        //        TOASSIGNEENAME = string.IsNullOrEmpty(TOASSIGNEENAME) ? "" : TOASSIGNEENAME;
        //        TOASSIGNEEMAILID = string.IsNullOrEmpty(TOASSIGNEEMAILID) ? "" : TOASSIGNEEMAILID;
        //        AGENTID = string.IsNullOrEmpty(AGENTID) ? "" : AGENTID;
        //        AGENTNAME = string.IsNullOrEmpty(AGENTNAME) ? "" : AGENTNAME;
        //        AGENTMAILID = string.IsNullOrEmpty(AGENTMAILID) ? "" : AGENTMAILID;
        //        MNGRID = string.IsNullOrEmpty(MNGRID) ? "" : MNGRID;
        //        MNGRNAME = string.IsNullOrEmpty(MNGRNAME) ? "" : MNGRNAME;
        //        MNGRMAILID = string.IsNullOrEmpty(MNGRMAILID) ? "" : MNGRMAILID;
        //        CSSTEAMMAILID = string.IsNullOrEmpty(CSSTEAMMAILID) ? "" : CSSTEAMMAILID;
        //        TITLE = string.IsNullOrEmpty(TITLE) ? "" : TITLE;
        //        PRIORITY = string.IsNullOrEmpty(PRIORITY) ? "" : PRIORITY;
        //        CCMAILIDS = string.IsNullOrEmpty(CCMAILIDS) ? "" : CCMAILIDS;



        //        if (FrmSts==0 && ToSts == 1)
        //        {
        //            TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //            TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //            string strSubject = string.Empty;
        //            int result;
        //            if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been Created by " + CLIENTNAME + " | " + CLIENTID + ".";
        //            }
        //            else
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been Created by " + CLIENTNAME + ".";
        //            }
        //            RecipientsString = CSSTEAMMAILID;
        //            strPernr_Mail = CLIENTMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS;
        //            body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //            body += "Below Ticket has been created.<br/>";
        //            //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
        //            body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
        //            body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
        //            body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
        //            body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
        //            body += "Thanks & Regards,<br/>";
        //            body += "ITChamps ITSM Team";
        //            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //            Thread email = new Thread(delegate()
        //            {
        //                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            });
        //            email.IsBackground = true;
        //            email.Start();
        //            //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //        }

        //        else if (FrmSts == ToSts && StatusC == "True" && TicketAct=="")
        //        {
        //            TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //            TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //            string strSubject = string.Empty;
        //            strSubject = "Customer " + User.Identity.Name +" has updated the ticket  " + TaskRefIdOut + " with comments..";
        //            RecipientsString = TOASSIGNEEMAILID;
        //            if (TOASSIGNEEMAILID.ToUpper().Trim() == CSSTEAMMAILID.ToUpper().Trim())
        //            {
        //                strPernr_Mail = CLIENTMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS + "," + MNGRMAILID + "," + AGENTMAILID;                     
        //            }
        //            else
        //            {
        //                if (TOASSIGNEEMAILID.ToUpper().Trim() == AGENTMAILID.ToUpper().Trim())
        //                {
        //                    strPernr_Mail = CLIENTMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + MNGRMAILID;
        //                }
        //                else
        //                {
        //                    strPernr_Mail = CLIENTMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + MNGRMAILID + ","+AGENTMAILID;
        //                }

        //            }                  
        //            body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //            body += "Below Ticket has been updated by Customer.<br/>";
        //            //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
        //            body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
        //            body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
        //            body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
        //            body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
        //            body += "Thanks & Regards,<br/>";
        //            body += "ITChamps ITSM Team";
        //            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //            Thread email = new Thread(delegate()
        //            {
        //                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            });
        //            email.IsBackground = true;
        //            email.Start();
        //        }

        //        else if ((FrmSts == 1 && ToSts == 2) || ((FrmSts == 10 && ToSts == 2 && User.Identity.Name == "cssteam")))
        //        {
        //            TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //            TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //            string strSubject = string.Empty;
        //            strSubject = "Ticket " + TaskRefIdOut + " has been transfered by " + PERNRNAME + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + ".";
        //            RecipientsString = TOASSIGNEEMAILID;
        //            strPernr_Mail = PERNRMAILID;
        //            body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //            body += "Below Ticket has been assigned to your queue.<br/>";
        //            //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
        //            body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
        //            body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
        //            body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
        //            body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
        //            body += "Thanks & Regards,<br/>";
        //            body += "ITChamps ITSM Team";
        //            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //            Thread email = new Thread(delegate()
        //            {
        //                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            });
        //            email.IsBackground = true;
        //            email.Start();
        //            //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

        //            int result;
        //            if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //            {
        //            }
        //            else
        //            {
        //                if (ViewState["OldPriorityID"].ToString().Trim() != DDLIssuePriority.SelectedValue.ToString().Trim())
        //                {
        //                    string strSubject2 = "Ticket " + TaskRefIdOut + " category changed by  " + PERNRNAME + ".";
        //                    string RecipientsString2 = FRMUSRMAILID;
        //                    string strPernr_Mail2 = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS;
        //                    string body2 = "Dear Mr. / Ms. " + FRMUSRNAME + " ,<br/><br/>";
        //                    if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
        //                    {
        //                        //ViewState["rtime"]
        //                        //sla and priority change
        //                        GetSLATIME(TaskRefIdOut);
        //                        body2 += "We acknowledge receipt of your issue.<br/>";
        //                        //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                        body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                        body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + " as per our SLA with you.</td></tr>";
        //                        body2 += "<tr><td>The issue will be resolved within (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
        //                        body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                        body2 += "Thanks & Regards,<br/>";
        //                        body2 += "ITChamps ITSM Team";
        //                    }
        //                    else
        //                    {
        //                        // priority change
        //                        body2 += "We acknowledge receipt of your issue.<br/>";
        //                        //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                        body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                        body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + ".</td></tr>";
        //                        body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                        body2 += "Thanks & Regards,<br/>";
        //                        body2 += "ITChamps ITSM Team";
        //                    }
        //                    body2 += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //                    Thread email1 = new Thread(delegate()
        //                    {
        //                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString2, User.Identity.Name, strSubject2, strPernr_Mail2, body2);
        //                    });
        //                    email1.IsBackground = true;
        //                    email1.Start();
        //                }
        //            }
        //        }
        //        else if ((FrmSts == 2 && ToSts == 2 && User.Identity.Name == "cssteam"))
        //        {
        //            TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //            TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //            string strSubject = string.Empty;
        //            strSubject = "Ticket " + TaskRefIdOut + " has been updated by " + PERNRNAME + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + ".";
        //            RecipientsString = TOASSIGNEEMAILID;
        //            strPernr_Mail = PERNRMAILID;
        //            body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //            body += "Below Ticket has been assigned to your queue.<br/>";
        //            //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
        //            body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
        //            body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
        //            body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
        //            body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
        //            body += "Thanks & Regards,<br/>";
        //            body += "ITChamps ITSM Team";
        //            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //            Thread email = new Thread(delegate()
        //            {
        //                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            });
        //            email.IsBackground = true;
        //            email.Start();
        //            //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

        //            int result;
        //            if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //            {
        //            }
        //            else
        //            {
        //                if (ViewState["OldPriorityID"].ToString().Trim() != DDLIssuePriority.SelectedValue.ToString().Trim())
        //                {
        //                    string strSubject2 = "Ticket " + TaskRefIdOut + " category changed by " + PERNRNAME + ".";
        //                    string RecipientsString2 = FRMUSRMAILID;
        //                    string strPernr_Mail2 = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS;
        //                    string body2 = "Dear Mr. / Ms. " + FRMUSRNAME + " ,<br/><br/>";
        //                    if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
        //                    {
        //                        //ViewState["rtime"]
        //                        //sla and priority change
        //                        GetSLATIME(TaskRefIdOut);
        //                        body2 += "We acknowledge receipt of your issue.<br/>";
        //                        //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                        body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                        body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + " as per our SLA with you.</td></tr>";
        //                        body2 += "<tr><td>The issue will be resolved within (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
        //                        body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                        body2 += "Thanks & Regards,<br/>";
        //                        body2 += "ITChamps ITSM Team";
        //                    }
        //                    else
        //                    {
        //                        // priority change
        //                        body2 += "We acknowledge receipt of your issue.<br/>";
        //                        //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                        body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                        body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + ".</td></tr>";
        //                        body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                        body2 += "Thanks & Regards,<br/>";
        //                        body2 += "ITChamps ITSM Team";
        //                    }
        //                    body2 += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //                    Thread email1 = new Thread(delegate()
        //                    {
        //                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString2, User.Identity.Name, strSubject2, strPernr_Mail2, body2);
        //                    });
        //                    email1.IsBackground = true;
        //                    email1.Start();
        //                }
        //            }
        //        }
        //        else if (FrmSts == 0 && ToSts == 2)
        //        {
        //            TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //            TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //            string strSubject = string.Empty;
        //            strSubject = "Ticket " + TaskRefIdOut + " has been created by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + ".";
        //            RecipientsString = TOASSIGNEEMAILID;
        //            strPernr_Mail = PERNRMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS;
        //            body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //            body += "Below Ticket has been assigned to your queue.<br/>";
        //            //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
        //            body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
        //            body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
        //            body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr></table><br/><br/>";
        //            body += "Thanks & Regards,<br/>";
        //            body += "ITChamps ITSM Team";
        //            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //            Thread email = new Thread(delegate()
        //            {
        //                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            });
        //            email.IsBackground = true;
        //            email.Start();
        //        }
        //        else if ((FrmSts == 2 && ToSts == 2 && User.Identity.Name != "cssteam"))
        //        {
        //            int result;
        //            if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //            {
        //                TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //                TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //                string strSubject = string.Empty;
        //                if (User.Identity.Name == TOASSIGNEEID)
        //                {
        //                    strSubject = "Ticket " + TaskRefIdOut + " has been self assigned by " + PERNRNAME + " | " + User.Identity.Name + ".";
        //                }
        //                else
        //                {
        //                    strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + ".";

        //                }
        //                RecipientsString = TOASSIGNEEMAILID;
        //                strPernr_Mail = PERNRMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS;
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //                //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
        //                body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
        //                body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
        //                body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr></table><br/><br/>";
        //                body += "Thanks & Regards,<br/>";
        //                body += "ITChamps ITSM Team";
        //                body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //                //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //                Thread email = new Thread(delegate()
        //                {
        //                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //                });
        //                email.IsBackground = true;
        //                email.Start();
        //            }
        //            else
        //            {

        //                // mail to agent
        //                TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //                TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //                string strSubject = string.Empty;
        //                {
        //                    if (User.Identity.Name == TOASSIGNEEID)
        //                    {
        //                        strSubject = "Ticket " + TaskRefIdOut + " has been self assigned.";
        //                    }
        //                    else
        //                    {
        //                        strSubject = "Ticket " + TaskRefIdOut + " has been assigned.";

        //                    }
        //                    //strSubject = "Ticket " + TaskRefIdOut + " has been assigned.";
        //                    RecipientsString = TOASSIGNEEMAILID;
        //                    strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID;
        //                    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                    body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //                    //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
        //                    body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
        //                    body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
        //                    body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
        //                    body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
        //                    body += "Thanks & Regards,<br/>";
        //                    body += "ITChamps ITSM Team";
        //                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //                    Thread email = new Thread(delegate()
        //                    {
        //                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //                    });
        //                    email.IsBackground = true;
        //                    email.Start();
        //                }
        //                {
        //                    //mail to client
        //                    string strSubject2 = "Ticket " + TaskRefIdOut + " assigned to Agent by " + PERNRNAME + " for resolution.";
        //                    string RecipientsString2 = FRMUSRMAILID;
        //                    string strPernr_Mail2 = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + AGENTMAILID;
        //                    string body2 = "Dear Mr. / Ms. " + FRMUSRNAME + ",<br/><br/>";
        //                    if (ViewState["OldPriorityID"].ToString().Trim() != DDLIssuePriority.SelectedValue.ToString().Trim())
        //                    {
        //                        if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
        //                        {
        //                            //sla and priority change
        //                            GetSLATIME(TaskRefIdOut);
        //                            body2 += "We acknowledge receipt of your issue.<br/>";
        //                            //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                            body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                            body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + " as per our SLA with you.</td></tr>";
        //                            body2 += "<tr><td>The issue will be resolved within (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
        //                            body2 += "<tr><td>Our Consultant Mr. / Ms. " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
        //                            body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                            body2 += "Thanks & Regards,<br/>";
        //                            body2 += "ITChamps ITSM Team";
        //                        }
        //                        else
        //                        {
        //                            // priority change
        //                            body2 += "We acknowledge receipt of your issue.<br/>";
        //                           // body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                            body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                            body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + ".</td></tr>";
        //                            body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
        //                            body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                            body2 += "Thanks & Regards,<br/>";
        //                            body2 += "ITChamps ITSM Team";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
        //                        {
        //                            //sla and no priority change
        //                            GetSLATIME(TaskRefIdOut);
        //                            body2 += "We acknowledge receipt of your issue.<br/>";
        //                            //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                            body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                            body2 += "<tr><td>The issue will be resolved within (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
        //                            body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
        //                            body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                            body2 += "Thanks & Regards,<br/>";
        //                            body2 += "ITChamps ITSM Team";
        //                        }
        //                        else
        //                        {
        //                            // no priority change no sla
        //                            body2 += "We acknowledge receipt of your issue.<br/>";
        //                            //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                            body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                            body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
        //                            body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                            body2 += "Thanks & Regards,<br/>";
        //                            body2 += "ITChamps ITSM Team";
        //                        }
        //                    }
        //                    body2 += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //                    Thread email1 = new Thread(delegate()
        //                    {
        //                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString2, User.Identity.Name, strSubject2, strPernr_Mail2, body2);
        //                    });
        //                    email1.IsBackground = true;
        //                    email1.Start();
        //                }
        //            }
        //        }
        //        else if ((FrmSts == 10 && ToSts == 2 && User.Identity.Name != "cssteam"))
        //        {
        //            int result;
        //            if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //            {
        //                TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //                TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //                string strSubject = string.Empty;
        //                strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + ".";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                strPernr_Mail = PERNRMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS;
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //                //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
        //                body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
        //                body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
        //                body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr></table><br/><br/>";
        //                body += "Thanks & Regards,<br/>";
        //                body += "ITChamps ITSM Team";
        //                body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //                //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //                Thread email = new Thread(delegate()
        //                {
        //                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //                });
        //                email.IsBackground = true;
        //                email.Start();
        //            }
        //            else
        //            {

        //                // mail to agent
        //                TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //                TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //                string strSubject = string.Empty;
        //                {
        //                    strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + ".";
        //                    RecipientsString = TOASSIGNEEMAILID;
        //                    strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID;
        //                    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                    body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //                    //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
        //                    body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
        //                    body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
        //                    body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
        //                    body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
        //                    body += "Thanks & Regards,<br/>";
        //                    body += "ITChamps ITSM Team";
        //                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //                    Thread email = new Thread(delegate()
        //                    {
        //                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //                    });
        //                    email.IsBackground = true;
        //                    email.Start();
        //                }
        //                {
        //                    //mail to client
        //                    string strSubject2 = "Ticket " + TaskRefIdOut + " assigned to Agent by " + PERNRNAME + " for resolution.";
        //                    string RecipientsString2 = FRMUSRMAILID;
        //                    string strPernr_Mail2 = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + AGENTMAILID;
        //                    string body2 = "Dear Mr. / Ms. " + FRMUSRNAME + ",<br/><br/>";
        //                    if (ViewState["OldPriorityID"].ToString().Trim() != DDLIssuePriority.SelectedValue.ToString().Trim())
        //                    {
        //                        if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
        //                        {
        //                            //sla and priority change
        //                            GetSLATIME(TaskRefIdOut);
        //                            body2 += "We acknowledge receipt of your issue.<br/>";
        //                            //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                            body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                            body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + " as per our SLA with you.</td></tr>";
        //                            body2 += "<tr><td>The issue will resolved in (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
        //                            body2 += "<tr><td>Our Consultant Mr. / Ms. " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
        //                            body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                            body2 += "Thanks & Regards,<br/>";
        //                            body2 += "ITChamps ITSM Team";
        //                        }
        //                        else
        //                        {
        //                            // priority change
        //                            body2 += "We acknowledge receipt of your issue.<br/>";
        //                            //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                            body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                            body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + ".</td></tr>";
        //                            body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
        //                            body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                            body2 += "Thanks & Regards,<br/>";
        //                            body2 += "ITChamps ITSM Team";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
        //                        {
        //                            //sla and no priority change
        //                            GetSLATIME(TaskRefIdOut);
        //                            body2 += "We acknowledge receipt of your issue.<br/>";
        //                            //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                            body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                            body2 += "<tr><td>The issue will resolved in (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
        //                            body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
        //                            body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                            body2 += "Thanks & Regards,<br/>";
        //                            body2 += "ITChamps ITSM Team";
        //                        }
        //                        else
        //                        {
        //                            // no priority change no sla
        //                            body2 += "We acknowledge receipt of your issue.<br/>";
        //                            //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
        //                            body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
        //                            body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
        //                            body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
        //                            body2 += "Thanks & Regards,<br/>";
        //                            body2 += "ITChamps ITSM Team";
        //                        }
        //                    }
        //                    body2 += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //                    Thread email1 = new Thread(delegate()
        //                    {
        //                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString2, User.Identity.Name, strSubject2, strPernr_Mail2, body2);
        //                    });
        //                    email1.IsBackground = true;
        //                    email1.Start();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            TicketingToolbo TicketingObjBo = new TicketingToolbo();
        //            TicketingToolbl TicketingObjBl = new TicketingToolbl();
        //            string strSubject = string.Empty;
        //            if ((FrmSts == 2 && ToSts == 3) || (FrmSts == 10 && ToSts == 3))
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + " for In Review QAS.";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strPernr_Mail = PERNRMAILID;
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //            }
        //            else if (FrmSts == 3 && ToSts == 2)
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + " for Correction in In Review QAS.";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strPernr_Mail = PERNRMAILID;
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //            }
        //            else if ((FrmSts == 3 && ToSts == 4) || (FrmSts == 2 && ToSts == 4))
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " is in UAT status.";
        //                RecipientsString = FRMUSRMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID)
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + CCMAILIDS + "," + AGENTMAILID;
        //                    }
        //                    else
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + AGENTMAILID;
        //                    }
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + AGENTMAILID;
        //                }
        //                body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue. Please check and provide UAT.<br/>";
        //            }
        //            else if (FrmSts == 3 && ToSts == 5) //?
        //            {
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strSubject = "Ticket " + TaskRefIdOut + " is working fine in Quality.";
        //                    RecipientsString = TOASSIGNEEMAILID;
        //                    strPernr_Mail = PERNRMAILID;
        //                    body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
        //                    body += "Below Ticket " + TaskRefIdOut + " is working fine in Quality.Please move it to Production.<br/>";
        //                }
        //            }
        //            else if ((FrmSts == 4 && TicketAct == "DENY") || (FrmSts == 4 && ToSts == 2))
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " UAT has been denied.";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID;
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CSSTEAMMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID + "," + CLIENTMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //            }
        //            else if ((FrmSts == 4 && TicketAct == "CONFIRM") || (FrmSts == 4 && ToSts == 5))
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " UAT has been provided.";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID;
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CSSTEAMMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID + "," + CLIENTMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //            }
        //            else if ((FrmSts == 5 && ToSts == 6) || (FrmSts == 5 && ToSts == 7))
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + " for In Review PRD.";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strPernr_Mail = PERNRMAILID;
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //            }
        //            else if (FrmSts == 6 && ToSts == 8)//?
        //            {
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strSubject = "Ticket " + TaskRefIdOut + " has been confirmed by " + PERNRNAME + ".";
        //                    RecipientsString = TOASSIGNEEMAILID;
        //                    strPernr_Mail = PERNRMAILID + CCMAILIDS + AGENTMAILID + FRMUSRMAILID;
        //                    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                    body += "Ticket " + TaskRefIdOut + " has been confirmed.<br/>";
        //                }
        //            }
        //            else if (FrmSts == 6 && ToSts == 2)
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + " for Correction in In Review PRD.";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strPernr_Mail = PERNRMAILID;
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //            }
        //            else if (FrmSts == 6 && ToSts == 7)
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " Resolved.";// and Moved to PRD
        //                RecipientsString = FRMUSRMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID)
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + CCMAILIDS + "," + AGENTMAILID;
        //                    }
        //                    else
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + AGENTMAILID;
        //                    }
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + AGENTMAILID;
        //                }
        //                body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.Please check and confirm.<br/>";
        //            }
        //            else if ((FrmSts == 7 && TicketAct == "CONFIRM") || (FrmSts == 7 && ToSts == 8))
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been confirmed by " + PERNRNAME + ".";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID + "," + AGENTMAILID;
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + AGENTMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID + "," + CLIENTMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been confirmed by user.<br/>";
        //            }
        //            else if ((FrmSts == 7 && TicketAct == "DENY") || (FrmSts == 7 && ((ToSts == 2) || (ToSts == 10))))
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been reassinged by user.";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID;
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CSSTEAMMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID + "," + CLIENTMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been re-assigned to your queue.<br/>";
        //            }
        //            else if ((FrmSts == 2 && ToSts == 7) || (FrmSts == 10 && ToSts == 7))// if review not required
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " Resolved.";// and Moved to PRD
        //                RecipientsString = FRMUSRMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID)
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + CCMAILIDS + "," + MNGRMAILID;
        //                    }
        //                    else
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + MNGRMAILID;
        //                    }
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + MNGRMAILID;
        //                }
        //                body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.Please check and confirm.<br/>";
        //            }
        //            //else if (ToSts == 13)
        //            //{
        //            //    strSubject = "Ticket " + TaskRefIdOut + " Waiting for your Action.";
        //            //    RecipientsString = FRMUSRMAILID;
        //            //    int result;
        //            //    if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //            //    {
        //            //        if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID.ToString().Trim())
        //            //        {
        //            //            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS;
        //            //        }
        //            //        else
        //            //        {
        //            //            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CLIENTMAILID;
        //            //        }
        //            //    }
        //            //    else
        //            //    {
        //            //        strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + CLIENTMAILID;
        //            //    }
        //            //    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //            //    body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue for Customer Action.<br/>";
        //            //}

        //            else if (ToSts == 13 && TicketAct == "")
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " Waiting for your Action.";
        //                RecipientsString = FRMUSRMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID.ToString().Trim())
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS;
        //                    }
        //                    else
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CLIENTMAILID;
        //                    }
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + CLIENTMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue for Customer Action.<br/>";
        //            }
        //            else if (ToSts == 13 && (TicketAct == "COMPLETED"))
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " Customer Action Completed.";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID.ToString().Trim())
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS;
        //                    }
        //                    else
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CLIENTMAILID;
        //                    }
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + FRMUSRMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //            }
        //            else if (FrmSts == 13 && (TicketAct == "COMPLETED" || TicketAct == ""))
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " Customer Action Completed.";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID.ToString().Trim())
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS;
        //                    }
        //                    else
        //                    {
        //                        strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CLIENTMAILID;
        //                    }
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + FRMUSRMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
        //            }
        //            else if (ToSts == 9) // closed
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been closed by " + PERNRNAME + ".";
        //                RecipientsString = FRMUSRMAILID;
        //                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + AGENTMAILID + "," + CLIENTMAILID;
        //                body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been closed.<br/>";
        //            }
        //            else if (ToSts == 11) // cancelled
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been cancelled by " + PERNRNAME + ".";
        //                RecipientsString = FRMUSRMAILID;
        //                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + AGENTMAILID + "," + CLIENTMAILID;
        //                body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + " has been cancelled.<br/>";
        //            }
        //            else
        //            {
        //                strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + " .";
        //                RecipientsString = TOASSIGNEEMAILID;
        //                int result;
        //                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + CCMAILIDS;
        //                }
        //                else
        //                {
        //                    strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID;
        //                }
        //                body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
        //                body += "Below Ticket " + TaskRefIdOut + "has been assigned to your queue.<br/>";
        //            }
        //            //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
        //            body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
        //            body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
        //            body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
        //            body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
        //            body += "Thanks & Regards,<br/>";
        //            body += "ITChamps ITSM Team";
        //            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
        //            //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            Thread email = new Thread(delegate()
        //            {
        //                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            });
        //            email.IsBackground = true;
        //            email.Start();
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}

        public void SendMail(long? TaskRefIdOut, int? FrmSts, int? ToSts, string TicketAct)
        {
            try
            {
                TicketingToolbo TicketingObjBoC = new TicketingToolbo();
                TicketingToolbl TicketingObjBC = new TicketingToolbl();
                string StatusC = "";
                TicketingObjBC.CheckIfclients(1, User.Identity.Name, ref StatusC);

                string body = "";
                string RecipientsString = "";
                string strPernr_Mail = "";
                string PERNRNAME = "";
                string PERNRMAILID = "";
                string CLIENTID = "";
                string CLIENTNAME = "";
                string CLIENTMAILID = "";
                string FRMUSRID = "";
                string FRMUSRNAME = "";
                string FRMUSRMAILID = "";
                string TOASSIGNEEID = "";
                string TOASSIGNEENAME = "";
                string TOASSIGNEEMAILID = "";
                string AGENTID = "";
                string AGENTNAME = "";
                string AGENTMAILID = "";
                string MNGRID = "";
                string MNGRNAME = "";
                string MNGRMAILID = "";
                string CSSTEAMMAILID = "";
                string TITLE = "";
                string PRIORITY = "";
                string CCMAILIDS = "";
                string FRMUSRMNGRMAILID = "";
                string TOUSRMNGRMAILID = "";
                string AGENTRPRTNGMNGRMAILID = "";

                TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();

                objTicketingToolDataContext.usp_tcikety_get_MailList(TaskRefIdOut, User.Identity.Name, FrmSts, ToSts, ref PERNRNAME, ref PERNRMAILID, ref CLIENTID,
                ref CLIENTNAME, ref CLIENTMAILID, ref FRMUSRID, ref FRMUSRNAME, ref FRMUSRMAILID, ref TOASSIGNEEID, ref TOASSIGNEENAME, ref TOASSIGNEEMAILID,
                ref AGENTID, ref AGENTNAME, ref AGENTMAILID, ref MNGRID, ref MNGRNAME, ref MNGRMAILID, ref CSSTEAMMAILID, ref TITLE, ref PRIORITY, ref CCMAILIDS,
                ref FRMUSRMNGRMAILID, ref TOUSRMNGRMAILID, ref AGENTRPRTNGMNGRMAILID);


                PERNRNAME = string.IsNullOrEmpty(PERNRNAME) ? "" : PERNRNAME;
                PERNRMAILID = string.IsNullOrEmpty(PERNRMAILID) ? "" : PERNRMAILID;
                CLIENTID = string.IsNullOrEmpty(CLIENTID) ? "" : CLIENTID;
                CLIENTNAME = string.IsNullOrEmpty(CLIENTNAME) ? "" : CLIENTNAME;
                CLIENTMAILID = string.IsNullOrEmpty(CLIENTMAILID) ? "" : CLIENTMAILID;
                FRMUSRID = string.IsNullOrEmpty(FRMUSRID) ? "" : FRMUSRID;
                FRMUSRNAME = string.IsNullOrEmpty(FRMUSRNAME) ? "" : FRMUSRNAME;
                FRMUSRMAILID = string.IsNullOrEmpty(FRMUSRMAILID) ? "" : FRMUSRMAILID;
                TOASSIGNEEID = string.IsNullOrEmpty(TOASSIGNEEID) ? "" : TOASSIGNEEID;
                TOASSIGNEENAME = string.IsNullOrEmpty(TOASSIGNEENAME) ? "" : TOASSIGNEENAME;
                TOASSIGNEEMAILID = string.IsNullOrEmpty(TOASSIGNEEMAILID) ? "" : TOASSIGNEEMAILID;
                AGENTID = string.IsNullOrEmpty(AGENTID) ? "" : AGENTID;
                AGENTNAME = string.IsNullOrEmpty(AGENTNAME) ? "" : AGENTNAME;
                AGENTMAILID = string.IsNullOrEmpty(AGENTMAILID) ? "" : AGENTMAILID;
                MNGRID = string.IsNullOrEmpty(MNGRID) ? "" : MNGRID;
                MNGRNAME = string.IsNullOrEmpty(MNGRNAME) ? "" : MNGRNAME;
                MNGRMAILID = string.IsNullOrEmpty(MNGRMAILID) ? "" : MNGRMAILID;
                CSSTEAMMAILID = string.IsNullOrEmpty(CSSTEAMMAILID) ? "" : CSSTEAMMAILID;
                TITLE = string.IsNullOrEmpty(TITLE) ? "" : TITLE;
                PRIORITY = string.IsNullOrEmpty(PRIORITY) ? "" : PRIORITY;
                CCMAILIDS = string.IsNullOrEmpty(CCMAILIDS) ? "" : CCMAILIDS;
                FRMUSRMNGRMAILID = string.IsNullOrEmpty(FRMUSRMNGRMAILID) ? "" : FRMUSRMNGRMAILID;
                TOUSRMNGRMAILID = string.IsNullOrEmpty(TOUSRMNGRMAILID) ? "" : TOUSRMNGRMAILID;
                AGENTRPRTNGMNGRMAILID = string.IsNullOrEmpty(AGENTRPRTNGMNGRMAILID) ? "" : AGENTRPRTNGMNGRMAILID;


                if (FrmSts == 0 && ToSts == 1)
                {
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();
                    string strSubject = string.Empty;
                    int result;
                    if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been Created by " + CLIENTNAME + " | " + CLIENTID + ".";
                    }
                    else
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been Created by " + CLIENTNAME + ".";
                    }
                    RecipientsString = CSSTEAMMAILID;
                    strPernr_Mail = CLIENTMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS;
                    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                    body += "Below Ticket has been created.<br/>";
                    //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
                    body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
                    body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                    body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
                    body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
                    body += "Thanks & Regards,<br/>";
                    body += "ITChamps ITSM Team";
                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });
                    email.IsBackground = true;
                    email.Start();
                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                }

                else if (FrmSts == ToSts && StatusC == "True" && TicketAct == "")
                {
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();
                    string strSubject = string.Empty;
                    strSubject = "Customer " + User.Identity.Name + " has updated the ticket  " + TaskRefIdOut + " with comments..";
                    RecipientsString = TOASSIGNEEMAILID;
                    if (TOASSIGNEEMAILID.ToUpper().Trim() == CSSTEAMMAILID.ToUpper().Trim())
                    {
                        strPernr_Mail = CLIENTMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS + "," + MNGRMAILID + "," + AGENTMAILID;
                    }
                    else
                    {
                        if (TOASSIGNEEMAILID.ToUpper().Trim() == AGENTMAILID.ToUpper().Trim())
                        {
                            strPernr_Mail = CLIENTMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + MNGRMAILID;
                        }
                        else
                        {
                            strPernr_Mail = CLIENTMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + MNGRMAILID + "," + AGENTMAILID;
                        }

                    }
                    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                    body += "Below Ticket has been updated by Customer.<br/>";
                    //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
                    body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
                    body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                    body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
                    body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
                    body += "Thanks & Regards,<br/>";
                    body += "ITChamps ITSM Team";
                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });
                    email.IsBackground = true;
                    email.Start();
                }

                else if ((FrmSts == 1 && ToSts == 2) || ((FrmSts == 10 && ToSts == 2 && User.Identity.Name == "cssteam")))
                {
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();
                    string strSubject = string.Empty;
                    strSubject = "Ticket " + TaskRefIdOut + " has been transfered by " + PERNRNAME + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + ".";
                    RecipientsString = TOASSIGNEEMAILID;
                    strPernr_Mail = PERNRMAILID;
                    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                    body += "Below Ticket has been assigned to your queue.<br/>";
                    //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
                    body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
                    body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                    body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
                    body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
                    body += "Thanks & Regards,<br/>";
                    body += "ITChamps ITSM Team";
                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });
                    email.IsBackground = true;
                    email.Start();
                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                    int result;
                    if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                    {
                    }
                    else
                    {
                        if (ViewState["OldPriorityID"].ToString().Trim() != DDLIssuePriority.SelectedValue.ToString().Trim())
                        {
                            string strSubject2 = "Ticket " + TaskRefIdOut + " category changed by  " + PERNRNAME + ".";
                            string RecipientsString2 = FRMUSRMAILID;
                            string strPernr_Mail2 = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS;
                            string body2 = "Dear Mr. / Ms. " + FRMUSRNAME + " ,<br/><br/>";
                            if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
                            {
                                //ViewState["rtime"]
                                //sla and priority change
                                GetSLATIME(TaskRefIdOut);
                                body2 += "We acknowledge receipt of your issue.<br/>";
                                //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + " as per our SLA with you.</td></tr>";
                                body2 += "<tr><td>The issue will be resolved within (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
                                body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                body2 += "Thanks & Regards,<br/>";
                                body2 += "ITChamps ITSM Team";
                            }
                            else
                            {
                                // priority change
                                body2 += "We acknowledge receipt of your issue.<br/>";
                                //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + ".</td></tr>";
                                body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                body2 += "Thanks & Regards,<br/>";
                                body2 += "ITChamps ITSM Team";
                            }
                            body2 += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                            Thread email1 = new Thread(delegate()
                            {
                                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString2, User.Identity.Name, strSubject2, strPernr_Mail2, body2);
                            });
                            email1.IsBackground = true;
                            email1.Start();
                        }
                    }
                }
                else if ((FrmSts == 2 && ToSts == 2 && User.Identity.Name == "cssteam"))
                {
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();
                    string strSubject = string.Empty;
                    strSubject = "Ticket " + TaskRefIdOut + " has been updated by " + PERNRNAME + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + ".";
                    RecipientsString = TOASSIGNEEMAILID;
                    strPernr_Mail = PERNRMAILID;
                    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                    body += "Below Ticket has been assigned to your queue.<br/>";
                    //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
                    body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
                    body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                    body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
                    body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
                    body += "Thanks & Regards,<br/>";
                    body += "ITChamps ITSM Team";
                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });
                    email.IsBackground = true;
                    email.Start();
                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                    int result;
                    if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                    {
                    }
                    else
                    {
                        if (ViewState["OldPriorityID"].ToString().Trim() != DDLIssuePriority.SelectedValue.ToString().Trim())
                        {
                            string strSubject2 = "Ticket " + TaskRefIdOut + " category changed by " + PERNRNAME + ".";
                            string RecipientsString2 = FRMUSRMAILID;
                            string strPernr_Mail2 = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS;
                            string body2 = "Dear Mr. / Ms. " + FRMUSRNAME + " ,<br/><br/>";
                            if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
                            {
                                //ViewState["rtime"]
                                //sla and priority change
                                GetSLATIME(TaskRefIdOut);
                                body2 += "We acknowledge receipt of your issue.<br/>";
                                //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + " as per our SLA with you.</td></tr>";
                                body2 += "<tr><td>The issue will be resolved within (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
                                body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                body2 += "Thanks & Regards,<br/>";
                                body2 += "ITChamps ITSM Team";
                            }
                            else
                            {
                                // priority change
                                body2 += "We acknowledge receipt of your issue.<br/>";
                                //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + ".</td></tr>";
                                body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                body2 += "Thanks & Regards,<br/>";
                                body2 += "ITChamps ITSM Team";
                            }
                            body2 += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                            Thread email1 = new Thread(delegate()
                            {
                                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString2, User.Identity.Name, strSubject2, strPernr_Mail2, body2);
                            });
                            email1.IsBackground = true;
                            email1.Start();
                        }
                    }
                }
                else if (FrmSts == 0 && ToSts == 2)
                {
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();
                    string strSubject = string.Empty;
                    strSubject = "Ticket " + TaskRefIdOut + " has been created by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + ".";
                    RecipientsString = TOASSIGNEEMAILID;
                    strPernr_Mail = PERNRMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS;
                    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                    body += "Below Ticket has been assigned to your queue.<br/>";
                    //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
                    body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
                    body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                    body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr></table><br/><br/>";
                    body += "Thanks & Regards,<br/>";
                    body += "ITChamps ITSM Team";
                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });
                    email.IsBackground = true;
                    email.Start();
                }
                else if ((FrmSts == 2 && ToSts == 2 && User.Identity.Name != "cssteam"))
                {
                    int result;
                    if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                    {
                        TicketingToolbo TicketingObjBo = new TicketingToolbo();
                        TicketingToolbl TicketingObjBl = new TicketingToolbl();
                        string strSubject = string.Empty;
                        if (User.Identity.Name == TOASSIGNEEID)
                        {
                            strSubject = "Ticket " + TaskRefIdOut + " has been self assigned by " + PERNRNAME + " | " + User.Identity.Name + ".";
                        }
                        else
                        {
                            strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + ".";

                        }
                        RecipientsString = TOASSIGNEEMAILID;
                        strPernr_Mail = PERNRMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                        //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
                        body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
                        body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                        body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr></table><br/><br/>";
                        body += "Thanks & Regards,<br/>";
                        body += "ITChamps ITSM Team";
                        body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                        //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                        Thread email = new Thread(delegate()
                        {
                            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                        });
                        email.IsBackground = true;
                        email.Start();
                    }
                    else
                    {

                        // mail to agent
                        TicketingToolbo TicketingObjBo = new TicketingToolbo();
                        TicketingToolbl TicketingObjBl = new TicketingToolbl();
                        string strSubject = string.Empty;
                        {
                            if (User.Identity.Name == TOASSIGNEEID)
                            {
                                strSubject = "Ticket " + TaskRefIdOut + " has been self assigned.";
                            }
                            else
                            {
                                strSubject = "Ticket " + TaskRefIdOut + " has been assigned.";

                            }
                            //strSubject = "Ticket " + TaskRefIdOut + " has been assigned.";
                            RecipientsString = TOASSIGNEEMAILID;
                            strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                            body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                            body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                            //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
                            body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
                            body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                            body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
                            body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
                            body += "Thanks & Regards,<br/>";
                            body += "ITChamps ITSM Team";
                            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                            //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                            Thread email = new Thread(delegate()
                            {
                                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                            });
                            email.IsBackground = true;
                            email.Start();
                        }
                        {
                            //mail to client
                            string strSubject2 = "Ticket " + TaskRefIdOut + " assigned to Agent by " + PERNRNAME + " for resolution.";
                            string RecipientsString2 = FRMUSRMAILID;
                            string strPernr_Mail2 = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + AGENTMAILID;
                            string body2 = "Dear Mr. / Ms. " + FRMUSRNAME + ",<br/><br/>";
                            if (ViewState["OldPriorityID"].ToString().Trim() != DDLIssuePriority.SelectedValue.ToString().Trim())
                            {
                                if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
                                {
                                    //sla and priority change
                                    GetSLATIME(TaskRefIdOut);
                                    body2 += "We acknowledge receipt of your issue.<br/>";
                                    //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                    body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                    body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                    body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + " as per our SLA with you.</td></tr>";
                                    body2 += "<tr><td>The issue will be resolved within (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
                                    body2 += "<tr><td>Our Consultant Mr. / Ms. " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
                                    body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                    body2 += "Thanks & Regards,<br/>";
                                    body2 += "ITChamps ITSM Team";
                                }
                                else
                                {
                                    // priority change
                                    body2 += "We acknowledge receipt of your issue.<br/>";
                                    //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                    body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                    body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                    body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + ".</td></tr>";
                                    body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
                                    body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                    body2 += "Thanks & Regards,<br/>";
                                    body2 += "ITChamps ITSM Team";
                                }
                            }
                            else
                            {
                                if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
                                {
                                    //sla and no priority change
                                    GetSLATIME(TaskRefIdOut);
                                    body2 += "We acknowledge receipt of your issue.<br/>";
                                    //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                    body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                    body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                    body2 += "<tr><td>The issue will be resolved within (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
                                    body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
                                    body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                    body2 += "Thanks & Regards,<br/>";
                                    body2 += "ITChamps ITSM Team";
                                }
                                else
                                {
                                    // no priority change no sla
                                    body2 += "We acknowledge receipt of your issue.<br/>";
                                    //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                    body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                    body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                    body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
                                    body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                    body2 += "Thanks & Regards,<br/>";
                                    body2 += "ITChamps ITSM Team";
                                }
                            }
                            body2 += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                            //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                            Thread email1 = new Thread(delegate()
                            {
                                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString2, User.Identity.Name, strSubject2, strPernr_Mail2, body2);
                            });
                            email1.IsBackground = true;
                            email1.Start();
                        }
                    }
                }
                else if ((FrmSts == 10 && ToSts == 2 && User.Identity.Name != "cssteam"))
                {
                    int result;
                    if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                    {
                        TicketingToolbo TicketingObjBo = new TicketingToolbo();
                        TicketingToolbl TicketingObjBl = new TicketingToolbl();
                        string strSubject = string.Empty;
                        strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + ".";
                        RecipientsString = TOASSIGNEEMAILID;
                        strPernr_Mail = PERNRMAILID + "," + FRMUSRMAILID + "," + CCMAILIDS + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                        //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
                        body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
                        body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                        body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr></table><br/><br/>";
                        body += "Thanks & Regards,<br/>";
                        body += "ITChamps ITSM Team";
                        body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                        //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                        Thread email = new Thread(delegate()
                        {
                            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                        });
                        email.IsBackground = true;
                        email.Start();
                    }
                    else
                    {

                        // mail to agent
                        TicketingToolbo TicketingObjBo = new TicketingToolbo();
                        TicketingToolbl TicketingObjBl = new TicketingToolbl();
                        string strSubject = string.Empty;
                        {
                            strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + ".";
                            RecipientsString = TOASSIGNEEMAILID;
                            strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                            body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                            body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                            //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
                            body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
                            body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                            body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
                            body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
                            body += "Thanks & Regards,<br/>";
                            body += "ITChamps ITSM Team";
                            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                            //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                            Thread email = new Thread(delegate()
                            {
                                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                            });
                            email.IsBackground = true;
                            email.Start();
                        }
                        {
                            //mail to client
                            string strSubject2 = "Ticket " + TaskRefIdOut + " assigned to Agent by " + PERNRNAME + " for resolution.";
                            string RecipientsString2 = FRMUSRMAILID;
                            string strPernr_Mail2 = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + AGENTMAILID;
                            string body2 = "Dear Mr. / Ms. " + FRMUSRNAME + ",<br/><br/>";
                            if (ViewState["OldPriorityID"].ToString().Trim() != DDLIssuePriority.SelectedValue.ToString().Trim())
                            {
                                if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
                                {
                                    //sla and priority change
                                    GetSLATIME(TaskRefIdOut);
                                    body2 += "We acknowledge receipt of your issue.<br/>";
                                    //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                    body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                    body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                    body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + " as per our SLA with you.</td></tr>";
                                    body2 += "<tr><td>The issue will resolved in (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
                                    body2 += "<tr><td>Our Consultant Mr. / Ms. " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
                                    body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                    body2 += "Thanks & Regards,<br/>";
                                    body2 += "ITChamps ITSM Team";
                                }
                                else
                                {
                                    // priority change
                                    body2 += "We acknowledge receipt of your issue.<br/>";
                                    //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                    body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                    body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                    body2 += "<tr><td>As per our interaction with you, the issue falls under priority " + PRIORITY + ".</td></tr>";
                                    body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
                                    body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                    body2 += "Thanks & Regards,<br/>";
                                    body2 += "ITChamps ITSM Team";
                                }
                            }
                            else
                            {
                                if (DDLIssueCategory.SelectedValue.ToString().Trim() == "2" || DDLIssueCategory.SelectedValue.ToString().Trim() == "3")
                                {
                                    //sla and no priority change
                                    GetSLATIME(TaskRefIdOut);
                                    body2 += "We acknowledge receipt of your issue.<br/>";
                                    // body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                    body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                    body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                    body2 += "<tr><td>The issue will resolved in (Days.hh:mm:ss) " + ViewState["rtime"].ToString() + " business days as per the SLA.</td></tr>";
                                    body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
                                    body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                    body2 += "Thanks & Regards,<br/>";
                                    body2 += "ITChamps ITSM Team";
                                }
                                else
                                {
                                    // no priority change no sla
                                    body2 += "We acknowledge receipt of your issue.<br/>";
                                    //body2 += "to = " + RecipientsString2 + "</br>" + "  cc = " + strPernr_Mail2 + "<br/>";
                                    body2 += "<table><tr><td>The ticket number is " + TaskRefIdOut + ".</td></tr>";
                                    body2 += "<tr><td>Ticket Title : " + TITLE + "</td></tr>";
                                    body2 += "<tr><td>Our Consultant " + TOASSIGNEENAME + " has been assigned to this issue and will get in touch with you shortly for more clarity on the issue.</td></tr>";
                                    body2 += "<tr><td>Please feel free to contact us for any details.</td></tr></table><br/><br/>";
                                    body2 += "Thanks & Regards,<br/>";
                                    body2 += "ITChamps ITSM Team";
                                }
                            }
                            body2 += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                            //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                            Thread email1 = new Thread(delegate()
                            {
                                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString2, User.Identity.Name, strSubject2, strPernr_Mail2, body2);
                            });
                            email1.IsBackground = true;
                            email1.Start();
                        }
                    }
                }
                else
                {
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();
                    string strSubject = string.Empty;
                    if ((FrmSts == 2 && ToSts == 3) || (FrmSts == 10 && ToSts == 3))
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + " for In Review QAS.";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strPernr_Mail = PERNRMAILID;
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                    }
                    else if (FrmSts == 3 && ToSts == 2)
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + " for Correction in In Review QAS.";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strPernr_Mail = PERNRMAILID;
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                    }
                    else if ((FrmSts == 3 && ToSts == 4) || (FrmSts == 2 && ToSts == 4))
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " is in UAT status.";
                        RecipientsString = FRMUSRMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID)
                            {
                                strPernr_Mail = PERNRMAILID + "," + CCMAILIDS + "," + AGENTMAILID;
                            }
                            else
                            {
                                strPernr_Mail = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + AGENTMAILID;
                            }
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + AGENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue. Please check and provide UAT.<br/>";
                    }
                    else if (FrmSts == 3 && ToSts == 5) //?
                    {
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strSubject = "Ticket " + TaskRefIdOut + " is working fine in Quality.";
                            RecipientsString = TOASSIGNEEMAILID;
                            strPernr_Mail = PERNRMAILID;
                            body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
                            body += "Below Ticket " + TaskRefIdOut + " is working fine in Quality.Please move it to Production.<br/>";
                        }
                    }
                    else if ((FrmSts == 4 && TicketAct == "DENY") || (FrmSts == 4 && ToSts == 2))
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " UAT has been denied.";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID;
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CSSTEAMMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID + "," + CLIENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                    }
                    else if ((FrmSts == 4 && TicketAct == "CONFIRM") || (FrmSts == 4 && ToSts == 5))
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " UAT has been provided.";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID;
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CSSTEAMMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID + "," + CLIENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                    }
                    else if ((FrmSts == 5 && ToSts == 6) || (FrmSts == 5 && ToSts == 7))
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + " for In Review PRD.";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strPernr_Mail = PERNRMAILID;
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                    }
                    else if (FrmSts == 6 && ToSts == 8)//?
                    {
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strSubject = "Ticket " + TaskRefIdOut + " has been confirmed by " + PERNRNAME + ".";
                            RecipientsString = TOASSIGNEEMAILID;
                            strPernr_Mail = PERNRMAILID + CCMAILIDS + AGENTMAILID + FRMUSRMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                            body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                            body += "Ticket " + TaskRefIdOut + " has been confirmed.<br/>";
                        }
                    }
                    else if (FrmSts == 6 && ToSts == 2)
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + " for Correction in In Review PRD.";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strPernr_Mail = PERNRMAILID;
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                    }
                    else if (FrmSts == 6 && ToSts == 7)
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " Resolved.";// and Moved to PRD
                        RecipientsString = FRMUSRMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID)
                            {
                                strPernr_Mail = PERNRMAILID + "," + CCMAILIDS + "," + AGENTMAILID;
                            }
                            else
                            {
                                strPernr_Mail = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + AGENTMAILID;
                            }
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + AGENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.Please check and confirm.<br/>";
                    }
                    else if ((FrmSts == 7 && TicketAct == "CONFIRM") || (FrmSts == 7 && ToSts == 8))
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been confirmed by " + PERNRNAME + ".";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID + "," + AGENTMAILID;
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + AGENTMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID + "," + CLIENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been confirmed by user.<br/>";
                    }
                    else if ((FrmSts == 7 && TicketAct == "DENY") || (FrmSts == 7 && ((ToSts == 2) || (ToSts == 10))))
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been reassinged by user.";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID;
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CSSTEAMMAILID + "," + CCMAILIDS + "," + FRMUSRMAILID + "," + CLIENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been re-assigned to your queue.<br/>";
                    }
                    else if ((FrmSts == 2 && ToSts == 7) || (FrmSts == 10 && ToSts == 7))// if review not required
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " Resolved.";// and Moved to PRD
                        RecipientsString = FRMUSRMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID)
                            {
                                strPernr_Mail = PERNRMAILID + "," + CCMAILIDS + "," + MNGRMAILID;
                            }
                            else
                            {
                                strPernr_Mail = PERNRMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + MNGRMAILID;
                            }
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + CCMAILIDS + "," + MNGRMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.Please check and confirm.<br/>";
                    }
                    //else if (ToSts == 13)
                    //{
                    //    strSubject = "Ticket " + TaskRefIdOut + " Waiting for your Action.";
                    //    RecipientsString = FRMUSRMAILID;
                    //    int result;
                    //    if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                    //    {
                    //        if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID.ToString().Trim())
                    //        {
                    //            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS;
                    //        }
                    //        else
                    //        {
                    //            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CLIENTMAILID;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + CLIENTMAILID;
                    //    }
                    //    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                    //    body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue for Customer Action.<br/>";
                    //}

                    else if (ToSts == 13 && TicketAct == "")
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " Waiting for your Action.";
                        RecipientsString = FRMUSRMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID.ToString().Trim())
                            {
                                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS;
                            }
                            else
                            {
                                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CLIENTMAILID;
                            }
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue for Customer Action.<br/>";
                    }
                    else if (ToSts == 13 && (TicketAct == "COMPLETED"))
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " Customer Action Completed.";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID.ToString().Trim())
                            {
                                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS;
                            }
                            else
                            {
                                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CLIENTMAILID;
                            }
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + FRMUSRMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                    }
                    else if (FrmSts == 13 && (TicketAct == "COMPLETED" || TicketAct == ""))
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " Customer Action Completed.";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID.ToString().Trim())
                            {
                                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS;
                            }
                            else
                            {
                                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CLIENTMAILID;
                            }
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + FRMUSRMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been assigned to your queue.<br/>";
                    }

                    else if (ToSts == 14 && FrmSts == 2)
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " is self assigned to Hold status.";
                        RecipientsString = FRMUSRMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID.ToString().Trim())
                            {
                                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS;
                            }
                            else
                            {
                                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CLIENTMAILID;
                            }
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been self assigned to Hold status.<br/>";
                    }
                    else if (ToSts == 2 && FrmSts == 14)
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " is self assigned.";
                        RecipientsString = FRMUSRMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            if (FRMUSRMAILID.ToString().Trim() == CLIENTMAILID.ToString().Trim())
                            {
                                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS;
                            }
                            else
                            {
                                strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CLIENTMAILID;
                            }
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + CSSTEAMMAILID + "," + CLIENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been self assigned from Hold status.<br/>";
                    }
                    else if (ToSts == 9) // closed
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been closed by " + PERNRNAME + ".";
                        RecipientsString = FRMUSRMAILID;
                        strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + AGENTMAILID + "," + CLIENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been closed.<br/>";
                    }
                    else if (ToSts == 11) // cancelled
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been cancelled by " + PERNRNAME + ".";
                        RecipientsString = FRMUSRMAILID;
                        strPernr_Mail = PERNRMAILID + "," + MNGRMAILID + "," + CCMAILIDS + "," + AGENTMAILID + "," + CLIENTMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        body = "Dear " + FRMUSRNAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + " has been cancelled.<br/>";
                    }
                    else
                    {
                        strSubject = "Ticket " + TaskRefIdOut + " has been assigned by " + PERNRNAME + " | " + User.Identity.Name + " to " + TOASSIGNEENAME + " | " + TOASSIGNEEID + " .";
                        RecipientsString = TOASSIGNEEMAILID;
                        int result;
                        if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                        {
                            strPernr_Mail = PERNRMAILID + "," + CCMAILIDS;
                        }
                        else
                        {
                            strPernr_Mail = PERNRMAILID + "," + CSSTEAMMAILID + "," + FRMUSRMNGRMAILID + "," + TOUSRMNGRMAILID + "," + AGENTRPRTNGMNGRMAILID;
                        }
                        body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                        body += "Below Ticket " + TaskRefIdOut + "has been assigned to your queue.<br/>";
                    }
                    //body += "to = " + RecipientsString + "</br>" + "  cc = " + strPernr_Mail + "<br/>";
                    body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + TaskRefIdOut + "</td></tr>";
                    body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                    body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
                    body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
                    body += "Thanks & Regards,<br/>";
                    body += "ITChamps ITSM Team";
                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });
                    email.IsBackground = true;
                    email.Start();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void GetSLATIME(long? ticketId)
        {
            try
            {

                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
                TimeSpan rtime = new TimeSpan(00, 00, 00);
                DateTime Begindatetime = DateTime.Now;
                DateTime Enddatetime = DateTime.Now;
                TicketingboList = TicketingObjBl.GetRemainingTime(long.Parse(ticketId.ToString().Trim()));

                if ((TicketingboList[0].CREATED_ON == null ? DateTime.Now.ToString() : TicketingboList[0].CREATED_ON.ToString().Trim()) != "1900-01-01 00:00:00")
                {
                    if (TicketingboList == null || TicketingboList.Count == 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('SLA not maintained!..')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('SLA not maintained!..')", true);
                        return;
                    }
                    else
                    {
                        ViewState["ENDDATEMAIL"] = TicketingboList[0].ENDDATE == null ? DateTime.Now.ToString() : TicketingboList[0].ENDDATE.ToString().Trim();
                        DateTime OUTDateTime = DateTime.Parse(ViewState["ENDDATEMAIL"].ToString());
                        DateTime INDateTime = DateTime.Now;
                        TimeSpan span = OUTDateTime - INDateTime;
                        //int hours = span.Days * 24 + span.Hours;
                        int minutes = span.Minutes < 0 ? 0 : span.Minutes;  //EDITED
                        int hours = span.Hours < 0 ? 0 : span.Hours; //EDITED
                        int days = span.Days < 0 ? 0 : span.Days; //EDITED
                        ViewState["rtime"] = TimeSpan.Parse(days + "." + hours + ":" + minutes + ":" + "00");

                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void TaskEdit_Click(object sender, EventArgs e)
        {
            //TxtTaskTitle.Enabled = true;
            //TxtTaskIssDesc.Enabled = true;
            DDLTaskAssignee.Enabled = true;
            DDLTaskStatus.Enabled = true;
            TxtTaskComments.Enabled = true;
            FU_TaskAttachments.Enabled = true;
            ViewState["TaskBackBtn"] = "Edit";
            TASKBack.Visible = true;
            LnkTaskBack.Visible = true;
            TASKADD.Visible = false;
            TaskUpdate.Visible = true;
            TaskEdit.Visible = false;
            TXTTaskCCMAILID.Enabled = true;
            if (ViewState["TaskCreatdBy"].ToString().Trim() == User.Identity.Name)
            {
                TxtTrTaskPlnhrs.Enabled = true;
            }
            else
            {
                TxtTrTaskPlnhrs.Enabled = false;
            }
            if (TxtTrTaskPlnhrs.Enabled == true)
            {
                RF_TxtTrTaskPlnhrs.Enabled = true;
                RV_TxtTrTaskPlnhrs.Enabled = true;
            }
            msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
            msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
            if (objPIDashBoardLst.Count > 0)
            {
                TxtTrTaskPlnhrs.Enabled = true;
            }

            if (User.Identity.Name == "cssteam")
            {
                TxtTrTaskPlnhrs.Enabled = true;
            }

            if (ViewState["TASKACTUALAGENT"].ToString().Trim() == User.Identity.Name)
            {
                if (DDLTaskStatus.SelectedValue.ToString().Trim() == "7")
                {
                    TxtTrTaskActhrs.Enabled = true;
                }
                else
                {
                    TxtTrTaskActhrs.Enabled = false;
                }
            }
            else
            {
                TxtTrTaskActhrs.Enabled = false;
            }

        }

        protected void BtnTaskBAck_Click(object sender, EventArgs e)
        {
            //mp1.Hide();

            if (ViewState["TaskBackBtn"].ToString().Trim() == "DirectView")
            {
                TaskOuterDiv.Visible = false;
                TicketDiv.Visible = false;
                LblCreateTask.Visible = false;
                Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");

            }
            else
            {
                TaskOuterDiv.Visible = false;
                TicketDiv.Visible = true;
                LblCreateTask.Visible = true;
            }
        }

        protected void Cb_InReviewNeed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["TicketID"].ToString().Trim() != "New" && ViewState["PrevStatusID"].ToString().Trim() != "13")
                {
                    if (Cb_InReviewNeed.Checked)
                    {
                        LoadStatus(2, User.Identity.Name, long.Parse(ViewState["TicketID"].ToString().Trim()));
                    }
                    else
                    {
                        LoadStatus(3, User.Identity.Name, long.Parse(ViewState["TicketID"].ToString().Trim()));

                    }
                }
                Cb_InReviewNeed.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void GrdTicketsAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "DOWNLOAD":
                        //string filePath = e.CommandArgument.ToString();
                        //Response.ContentType = "application/octet-stream";
                        ////Response.ContentType = ContentType;
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                        //Response.WriteFile(filePath);
                        //Response.End();
                        //break;
                        string strURL = e.CommandArgument.ToString(); ;
                        WebClient req = new WebClient();
                        HttpResponse response = HttpContext.Current.Response;
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        response.Buffer = true;
                        response.ContentType = "application/" + Path.GetExtension(strURL);
                        response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(strURL) + "\"");
                        byte[] data = req.DownloadData(Server.MapPath(strURL));
                        response.BinaryWrite(data);
                        response.End();
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

        }

        protected void Grd_TaskAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "DOWNLOAD":
                        string strURL = e.CommandArgument.ToString(); ;
                        WebClient req = new WebClient();
                        HttpResponse response = HttpContext.Current.Response;
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        response.Buffer = true;
                        response.ContentType = "application/" + Path.GetExtension(strURL);
                        response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(strURL) + "\"");
                        byte[] data = req.DownloadData(Server.MapPath(strURL));
                        response.BinaryWrite(data);
                        response.End();
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (Session["CountdownTimer"] != null)
            {
                if ((Session["CountdownTimer"] as CountDownTimer).TimeLeft == TimeSpan.Zero)
                {
                    Label1.Text = "SLA Breached";
                    (Session["CountdownTimer"] as CountDownTimer).Stop();
                }
                else
                {
                    Label1.Text = (Session["CountdownTimer"] as CountDownTimer).TimeLeft.ToString();
                }


            }
        }

        protected void BtnRework_Click(object sender, EventArgs e)
        {
            try
            {
                long? REOWRKTICKETIDOUT = 0;
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                if (FU_Isssue.HasFile)
                {
                    foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                    {
                        TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                        TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                        TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                        //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                        uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                    }
                }
                TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                TicketingObjBo.Flag = 1;
                TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                TicketingObjBl.REWORK_TICKET(TicketingObjBo, ref REOWRKTICKETIDOUT);
                SendMailReowrk(TicketingObjBo.TID, REOWRKTICKETIDOUT, "Rework");
                ClearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Rework Created Successfully!..'); parent.location.href='IssueTracker.aspx'", true);
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Ticket Rework Created Successfully!..'); parent.location.href='IssueTracker.aspx'", true);



                TicketingToolbo TicketingObjBo2 = new TicketingToolbo();
                TicketingToolbl TicketingObjBl2 = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList2 = new List<TicketingToolbo>();

                TicketingboList2 = TicketingObjBl2.Load_Ticket(long.Parse(ViewState["TicketID"].ToString().Trim()));
                ViewState["TIMERSTS"] = TicketingboList2[0].STATUS == null ? "0" : TicketingboList2[0].STATUS.ToString().Trim();
                ViewState["CATEGORYIDSLA"] = TicketingboList2[0].CATEGORY == null ? "0" : TicketingboList2[0].CATEGORY.ToString().Trim();
                if (ViewState["CATEGORYIDSLA"].ToString().Trim() == "2" || ViewState["CATEGORYIDSLA"].ToString().Trim() == "3")
                {
                    if (ViewState["TIMERSTS"].ToString().Trim() != "1" && ViewState["TIMERSTS"].ToString().Trim() != "4" && ViewState["TIMERSTS"].ToString().Trim() != "7" && ViewState["TIMERSTS"].ToString().Trim() != "8" && ViewState["TIMERSTS"].ToString().Trim() != "9" && ViewState["TIMERSTS"].ToString().Trim() != "11" && ViewState["TIMERSTS"].ToString().Trim() != "12" && ViewState["TIMERSTS"].ToString().Trim() != "13")
                    {
                        Session["CountdownTimer"] = null;
                        BindTimerData(long.Parse(ViewState["TicketID"].ToString().Trim()).ToString());
                        UpdatePanel1.Visible = true;
                    }
                    else
                    {
                        UpdatePanel1.Visible = false;
                    }
                }
                else
                {
                    UpdatePanel1.Visible = false;
                }


                Session["PendingPageIndex"] = "0";
                Session["DDLStatusSearch"] = "0";
                Session["DDLCustomerList"] = "0";
                Session["TxtFromDate"] = "";
                Session["TxtToDate"] = "";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        public void SendMailReowrk(long? TicketTID, long? REOWRKTICKETIDOUT, string Action)
        {
            try
            {
                string body = "";
                string RecipientsString = "";
                string strPernr_Mail = "";
                string PERNRNAME = "";
                string PERNRMAILID = "";
                string CLIENTID = "";
                string CLIENTNAME = "";
                string CLIENTMAILID = "";
                string FRMUSRID = "";
                string FRMUSRNAME = "";
                string FRMUSRMAILID = "";
                string TOASSIGNEEID = "";
                string TOASSIGNEENAME = "";
                string TOASSIGNEEMAILID = "";
                string AGENTID = "";
                string AGENTNAME = "";
                string AGENTMAILID = "";
                string MNGRID = "";
                string MNGRNAME = "";
                string MNGRMAILID = "";
                string CSSTEAMMAILID = "";
                string TITLE = "";
                string PRIORITY = "";
                string CCMAILIDS = "";

                TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();

                objTicketingToolDataContext.usp_tcikety_get_MailListREWORK(TicketTID, REOWRKTICKETIDOUT, User.Identity.Name, ref PERNRNAME, ref PERNRMAILID, ref CLIENTID,
                ref CLIENTNAME, ref CLIENTMAILID, ref FRMUSRID, ref FRMUSRNAME, ref FRMUSRMAILID, ref TOASSIGNEEID, ref TOASSIGNEENAME, ref TOASSIGNEEMAILID,
                ref AGENTID, ref AGENTNAME, ref AGENTMAILID, ref MNGRID, ref MNGRNAME, ref MNGRMAILID, ref CSSTEAMMAILID, ref TITLE, ref PRIORITY, ref CCMAILIDS);

                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                string strSubject = string.Empty;
                strSubject = "Ticket " + REOWRKTICKETIDOUT + " has been re-assigned for rework.";

                int result;
                if (int.TryParse(CLIENTID.ToString().Trim(), out result))
                {
                    RecipientsString = TOASSIGNEENAME;
                    if (CLIENTMAILID.ToString().Trim() == FRMUSRMAILID.ToString().Trim())
                    {
                        strPernr_Mail = CLIENTMAILID + " , " + CCMAILIDS;
                    }
                    else
                    {
                        strPernr_Mail = CLIENTMAILID + " , " + CCMAILIDS + "," + FRMUSRMAILID;
                    }
                    body = "Dear " + TOASSIGNEENAME + " ,<br/><br/>";
                }
                else
                {
                    RecipientsString = CSSTEAMMAILID;
                    strPernr_Mail = CLIENTMAILID + "," + FRMUSRMAILID + " , " + MNGRMAILID + " , " + AGENTMAILID + " , " + CCMAILIDS;
                    body = "Dear CSSTeam  ,<br/><br/>";
                }
                body += "Below Ticket has been re-assigned for rework.<br/>";
                //body += "to = " + RecipientsString + "</br>" + "cc = " + strPernr_Mail + "<br/>";
                body += "<table><tr><td>Ticket No</td> <td>: </td> <td>" + REOWRKTICKETIDOUT + "</td></tr>";
                body += "<tr><td>Ref Ticket No</td> <td>: </td> <td>" + TicketTID + "</td></tr>";
                body += "<tr><td>Title</td> <td>: </td> <td>" + TITLE + "</td></tr>";
                body += "<tr><td>Priority</td> <td>: </td> <td>" + PRIORITY + "</td></tr>";
                body += "<tr><td>Client</td> <td>: </td> <td>" + CLIENTNAME + "</td></tr></table><br/><br/>";
                body += "Thanks & Regards,<br/>";
                body += "ITChamps ITSM Team";
                body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                // iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                Thread email = new Thread(delegate()
                {
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                });
                email.IsBackground = true;
                email.Start();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void DDLTaskStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDLTaskStatus.SelectedValue.ToString().Trim() == "7")
                {
                    if (ViewState["TASKACTUALAGENT"].ToString().Trim() == User.Identity.Name)
                    {
                        TxtTrTaskActhrs.Enabled = true;
                    }
                    else
                    {
                        TxtTrTaskActhrs.Enabled = false;
                    }
                }
                else
                {
                    TxtTrTaskActhrs.Enabled = false;
                }
                DDLTaskStatus.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void GrdTaskTickAtt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "DOWNLOAD":
                        //string filePath = e.CommandArgument.ToString();
                        //Response.ContentType = "application/octet-stream";
                        ////Response.ContentType = ContentType;
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                        //Response.WriteFile(filePath);
                        //Response.End();
                        //break;

                        string strURL = e.CommandArgument.ToString(); ;
                        WebClient req = new WebClient();
                        HttpResponse response = HttpContext.Current.Response;
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        response.Buffer = true;
                        response.ContentType = "application/" + Path.GetExtension(strURL);
                        response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(strURL) + "\"");
                        byte[] data = req.DownloadData(Server.MapPath(strURL));
                        response.BinaryWrite(data);
                        response.End();
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            mp1.Hide();
        }


        protected void BtnRatingConfirm_Click(object sender, EventArgs e)
        {
            try
            {

                long? TicketRefIdOut = 0;
                if (string.IsNullOrEmpty(TxtComments.Text.ToString().Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter the comments!..')", true);
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please enter the comments!..')", true);
                }
                else
                {
                    int? rate1 = int.Parse(Rating1.CurrentRating.ToString());
                    int? rate2 = int.Parse(Rating2.CurrentRating.ToString());
                    int? rate3 = int.Parse(Rating3.CurrentRating.ToString());
                    int? rate4 = int.Parse(Rating4.CurrentRating.ToString());
                    int? rate5 = int.Parse(Rating5.CurrentRating.ToString());


                    TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
                    objTicketingToolDataContext.usp_tcikety_Add_Custrating(long.Parse(ViewState["TicketID"].ToString().Trim()), rate1, rate2, rate3, rate4, rate5, txtsug.Text.ToString());



                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#mp1').modal('hide')", true);
                    //string script = "window.onload = function() { HideModalPopup(); };";
                    //ClientScript.RegisterStartupScript(this.GetType(), "HideModalPopup", script, true);
                    //ScriptManager.RegisterStartupScript(this, thisGetType(), "Javascript", "javascript:HideModalPopup(); ", true);

                    mp1.Hide();
                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();

                    //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                    //{
                    TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                    TicketingObjBo.TITLE = "";
                    TicketingObjBo.ISSDESC = "";
                    TicketingObjBo.CLIENT = "";
                    TicketingObjBo.FRMUSR = "";
                    TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                    TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                    //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                    TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER

                    //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];

                    //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                    if (ViewState["PrevStatusID"].ToString().Trim() == "7") // Resolved status
                    {
                        TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
                    }
                    else if (ViewState["PrevStatusID"].ToString().Trim() == "4") // UAT Status
                    {
                        TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                    }


                    TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                    TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                    TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                    TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                    //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                    TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                    TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status
                    if (FU_Isssue.HasFile)
                    {
                        foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                        {
                            TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                            TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                            TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                            //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                            uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                        }
                    }

                    //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                    //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                    //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                    TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                    TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                    TicketingObjBo.CREATED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                    TicketingObjBo.Flag = 2;
                    TicketingObjBo.TICKETACTION = "CONFIRM";
                    TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                    TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                    TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                    SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                    //mp1.Hide();
                    ClearFields();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !!..')", true);
                    Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !'); parent.location.href='IssueTracker.aspx'", true);
                    //}

                    TicketingToolbo TicketingObjBo2 = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl2 = new TicketingToolbl();
                    List<TicketingToolbo> TicketingboList2 = new List<TicketingToolbo>();

                    TicketingboList2 = TicketingObjBl2.Load_Ticket(long.Parse(ViewState["TicketID"].ToString().Trim()));
                    ViewState["TIMERSTS"] = TicketingboList2[0].STATUS == null ? "0" : TicketingboList2[0].STATUS.ToString().Trim();
                    ViewState["CATEGORYIDSLA"] = TicketingboList2[0].CATEGORY == null ? "0" : TicketingboList2[0].CATEGORY.ToString().Trim();
                    if (ViewState["CATEGORYIDSLA"].ToString().Trim() == "2" || ViewState["CATEGORYIDSLA"].ToString().Trim() == "3")
                    {
                        if (ViewState["TIMERSTS"].ToString().Trim() != "1" && ViewState["TIMERSTS"].ToString().Trim() != "4" && ViewState["TIMERSTS"].ToString().Trim() != "7" && ViewState["TIMERSTS"].ToString().Trim() != "8" && ViewState["TIMERSTS"].ToString().Trim() != "9" && ViewState["TIMERSTS"].ToString().Trim() != "11" && ViewState["TIMERSTS"].ToString().Trim() != "12" && ViewState["TIMERSTS"].ToString().Trim() != "13")
                        {
                            Session["CountdownTimer"] = null;
                            BindTimerData(long.Parse(ViewState["TicketID"].ToString().Trim()).ToString());
                            UpdatePanel1.Visible = true;
                        }
                        else
                        {
                            UpdatePanel1.Visible = false;
                        }
                    }
                    else
                    {
                        UpdatePanel1.Visible = false;
                    }
                    Session["PendingPageIndex"] = "0";
                    Session["DDLStatusSearch"] = "0";
                    Session["DDLCustomerList"] = "0";
                    Session["TxtFromDate"] = "";
                    Session["TxtToDate"] = "";

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }



        protected void BtnConfirmUAT_Click(object sender, EventArgs e)
        {
            try
            {

                long? TicketRefIdOut = 0;
                if (string.IsNullOrEmpty(TxtComments.Text.ToString().Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter the comments!..')", true);
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Key", "alert('Please enter the comments!..')", true);
                }
                else
                {

                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                    TicketingToolbo TicketingObjBo = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl = new TicketingToolbl();

                    //if (TxtAssignee.Text.ToString().Trim().Contains('-'))
                    //{
                    TicketingObjBo.TID = long.Parse(ViewState["TicketID"].ToString().Trim());
                    TicketingObjBo.TITLE = "";
                    TicketingObjBo.ISSDESC = "";
                    TicketingObjBo.CLIENT = "";
                    TicketingObjBo.FRMUSR = "";
                    TicketingObjBo.USRMAILID = TxtUsrMailID.Text.ToString().Trim();
                    TicketingObjBo.CCMAILID = TxtCCMailID.Text.ToString().Trim();
                    //TicketingObjBo.ASSIGNEE = TxtAssignee.Text.ToString().Trim();
                    TicketingObjBo.FRMASSIGNEE = User.Identity.Name; //LOGGED USER

                    //ViewState["TxtAssignee"] = TxtAssignee.Text.ToString().Trim().Split('-')[1];

                    //TicketingObjBo.TOASSIGNEE = ViewState["TxtAssignee"].ToString().Trim(); // Assignee ie Assigned to 

                    if (ViewState["PrevStatusID"].ToString().Trim() == "7") // Resolved status
                    {
                        TicketingObjBo.TOASSIGNEE = "cssteam"; // Assignee ie Assigned to 
                    }
                    else if (ViewState["PrevStatusID"].ToString().Trim() == "4") // UAT Status
                    {
                        TicketingObjBo.TOASSIGNEE = ddlAssignee.SelectedValue.ToString().Trim(); // Assignee ie Assigned to 
                    }


                    TicketingObjBo.PRIORITY = int.Parse(DDLIssuePriority.SelectedValue);
                    TicketingObjBo.CATEGORY = int.Parse(DDLIssueCategory.SelectedValue);
                    TicketingObjBo.ISSCATEGORYCSS = int.Parse(DDLIssueCategoryCSS.SelectedValue);
                    TicketingObjBo.ISSTYPE = int.Parse(DDLIssueType.SelectedValue);
                    //TicketingObjBo.STATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim());
                    TicketingObjBo.FRMSTATUS = int.Parse(ViewState["PrevStatusID"].ToString().Trim());
                    TicketingObjBo.TOSTATUS = int.Parse(DDLIssueStatus.SelectedValue.ToString().Trim()); // STATUS Assigned to status
                    if (FU_Isssue.HasFile)
                    {
                        foreach (HttpPostedFile uploadedFile in FU_Isssue.PostedFiles)
                        {
                            TicketingObjBo.ATTACHEMENT_FILE += string.Format("{0}|", FU_Isssue.HasFile ? "Yes" : "No");
                            TicketingObjBo.ATTACHEMENT_FID += string.Format("{0}|", FU_Isssue.HasFile ? uploadedFile.FileName : "");
                            TicketingObjBo.ATTACHEMENT_FPATH += string.Format("{0}| ", FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1 + Path.GetExtension(uploadedFile.FileName) : "");
                            //uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                            uploadedFile.SaveAs(Server.MapPath("~/TicketingDoc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uploadedFile.FileName) + "-" + date1) + Path.GetExtension(uploadedFile.FileName));
                        }
                    }

                    //TicketingObjBo.ATTACHEMENT_FILE = FU_Isssue.HasFile ? "Yes" : "No";
                    //TicketingObjBo.ATTACHEMENT_FID = FU_Isssue.HasFile ? FU_Isssue.PostedFile.FileName : "";
                    //TicketingObjBo.ATTACHEMENT_FPATH = FU_Isssue.HasFile ? "~/TicketingDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(FU_Isssue.FileName) : "";
                    TicketingObjBo.COMMENTS = TxtComments.Text.ToString().Trim();
                    TicketingObjBo.CBINREVIEWNEEDED = Cb_InReviewNeed.Checked ? "Yes" : "No";
                    TicketingObjBo.CREATED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_ON = DateTime.Now;
                    TicketingObjBo.LASTMODIFIED_BY = User.Identity.Name;
                    TicketingObjBo.Flag = 2;
                    TicketingObjBo.TICKETACTION = "CONFIRM";
                    TicketingObjBo.Plndhrs = string.IsNullOrEmpty(TxtPlndHrs.Text) ? 0 : decimal.Parse(TxtPlndHrs.Text.ToString().Trim());
                    TicketingObjBo.Actualhrs = string.IsNullOrEmpty(TxtTrActhrs.Text) ? 0 : decimal.Parse(TxtTrActhrs.Text.ToString().Trim());
                    TicketingObjBl.CREATE_TICKET(TicketingObjBo, ref TicketRefIdOut);
                    SendMail(TicketRefIdOut, TicketingObjBo.FRMSTATUS, TicketingObjBo.TOSTATUS, TicketingObjBo.TICKETACTION);
                    //mp1.Hide();
                    ClearFields();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !!..')", true);
                    //Response.Redirect("~/UI/Ticketing Tool/IssueTracker.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Updated Successfully !'); parent.location.href='IssueTracker.aspx'", true);
                    //}

                    TicketingToolbo TicketingObjBo2 = new TicketingToolbo();
                    TicketingToolbl TicketingObjBl2 = new TicketingToolbl();
                    List<TicketingToolbo> TicketingboList2 = new List<TicketingToolbo>();

                    TicketingboList2 = TicketingObjBl2.Load_Ticket(long.Parse(ViewState["TicketID"].ToString().Trim()));
                    ViewState["TIMERSTS"] = TicketingboList2[0].STATUS == null ? "0" : TicketingboList2[0].STATUS.ToString().Trim();
                    ViewState["CATEGORYIDSLA"] = TicketingboList2[0].CATEGORY == null ? "0" : TicketingboList2[0].CATEGORY.ToString().Trim();
                    if (ViewState["CATEGORYIDSLA"].ToString().Trim() == "2" || ViewState["CATEGORYIDSLA"].ToString().Trim() == "3")
                    {
                        if (ViewState["TIMERSTS"].ToString().Trim() != "1" && ViewState["TIMERSTS"].ToString().Trim() != "4" && ViewState["TIMERSTS"].ToString().Trim() != "7" && ViewState["TIMERSTS"].ToString().Trim() != "8" && ViewState["TIMERSTS"].ToString().Trim() != "9" && ViewState["TIMERSTS"].ToString().Trim() != "11" && ViewState["TIMERSTS"].ToString().Trim() != "12" && ViewState["TIMERSTS"].ToString().Trim() != "13")
                        {
                            Session["CountdownTimer"] = null;
                            BindTimerData(long.Parse(ViewState["TicketID"].ToString().Trim()).ToString());
                            UpdatePanel1.Visible = true;
                        }
                        else
                        {
                            UpdatePanel1.Visible = false;
                        }
                    }
                    else
                    {
                        UpdatePanel1.Visible = false;
                    }
                    Session["PendingPageIndex"] = "0";
                    Session["DDLStatusSearch"] = "0";
                    Session["DDLCustomerList"] = "0";
                    Session["TxtFromDate"] = "";
                    Session["TxtToDate"] = "";

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

    }

    public class CountDownTimer
    {
        public TimeSpan TimeLeft;
        System.Threading.Thread thread;
        public CountDownTimer(TimeSpan original)
        {
            this.TimeLeft = original;
        }
        public void Start()
        {
            // Start a background thread to count down time
            thread = new System.Threading.Thread(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                    TimeLeft = TimeLeft.Subtract(TimeSpan.Parse("00:00:01"));
                }
            });

            if (TimeLeft == TimeSpan.Zero)
            {
                thread.Abort();
            }
            else
            { thread.Start(); }

        }

        public void Stop()
        {

            thread.Abort();
        }
    }
}
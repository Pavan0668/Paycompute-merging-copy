using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class TravelReqAppRej : System.Web.UI.Page
    {
        protected MembershipUser memUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                    PageLoadEvents();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #region User Defined Method
        private void MsgCls(string LblTxt, Label Lbl, Color Clr)
        {
            try
            {
                Lbl.Text = string.Empty;
                Lbl.Text = LblTxt;
                Lbl.ForeColor = Clr;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }
        #endregion

        private void PageLoadEvents()
        {
            try
            {
                travelrequestcolumnsbo TrvlBO = new travelrequestcolumnsbo();
                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                TrvlReqboList = travelrequestblObj.Get_TravelReqAppRejDetails(User.Identity.Name);
                GV_TravelReqAppRej.DataSource = TrvlReqboList;
                GV_TravelReqAppRej.DataBind();

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void GV_TravelReqAppRej_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GV_TravelReqAppRej_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_TravelReqAppRej.PageIndex = e.NewPageIndex;
                PageLoadEvents();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void GV_TravelReqAppRej_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                travelrequestbl ObjTrvl = new travelrequestbl();
                TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();

                string @REINR = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                string @TripType = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["KZREA"].ToString();
                string From = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["KUNDE"].ToString();
                string To = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZORT1"].ToString();
                string Country = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZLAND"].ToString();
                string Project = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();
                string TotalAdvance = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["SUM_ADVANC"].ToString();
                string FrmDate = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString();
                string ToDate = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString();
                string Currency = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["CURRENCY"].ToString();
                string AdditonalAdvance = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["ADDIT_AMNT"].ToString();
                string Createdby = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString().Trim();

                string Prjid = GV_TravelReqAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString().Split(':')[0].Trim();
                ViewState["ProjID"] = Prjid;


                switch (e.CommandName.ToUpper())
                {
                    case "APPROVE":
                        ObjTrvlReq.REINR = @REINR;
                        ObjTrvlReq.PERNR = Createdby;

                        ViewState["Createdby"] = Createdby;
                        ObjTrvlReq.KZREA = @TripType;
                        ObjTrvlReq.KUNDE = From;
                        ObjTrvlReq.ZORT1 = To;
                        ObjTrvlReq.ZLAND = Country;
                        ObjTrvlReq.WBS_ELEMT = Project;
                        ObjTrvlReq.SUM_ADVANC = decimal.Parse(TotalAdvance);
                        ObjTrvlReq.DATV1 = DateTime.Parse(FrmDate);
                        ObjTrvlReq.DATB1 = DateTime.Parse(ToDate);
                        ObjTrvlReq.CURRENCY = Currency;
                        ObjTrvlReq.ADDIT_AMNT = decimal.Parse(AdditonalAdvance);
                        ObjTrvlReq.STATUS = "APPROVE";
                        ObjTrvl.TravelReq_MngrAppRej(ObjTrvlReq, ViewState["ProjID"].ToString().Trim());
                        MsgCls("Travel Request Approved successfully !", LblMsg, Color.Green);
                        SendMailMethodtToEmp(ObjTrvlReq);
                        //SendMailMethod(ObjTrvlReq);

                        GV_TravelReqAppRej.EditIndex = -1;
                        PageLoadEvents();
                        break;
                    case "REJECT":
                        ObjTrvlReq.REINR = @REINR;
                        ObjTrvlReq.PERNR = Createdby;
                        ViewState["Createdby"] = Createdby;
                        ObjTrvlReq.KZREA = @TripType;
                        ObjTrvlReq.KUNDE = From;
                        ObjTrvlReq.ZORT1 = To;
                        ObjTrvlReq.ZLAND = Country;
                        ObjTrvlReq.WBS_ELEMT = Project;
                        ObjTrvlReq.SUM_ADVANC = decimal.Parse(TotalAdvance);
                        ObjTrvlReq.DATV1 = DateTime.Parse(FrmDate);
                        ObjTrvlReq.DATB1 = DateTime.Parse(ToDate);
                        ObjTrvlReq.CURRENCY = Currency;
                        ObjTrvlReq.ADDIT_AMNT = decimal.Parse(AdditonalAdvance);
                        ObjTrvlReq.STATUS = "REJECT";
                        ObjTrvl.TravelReq_MngrAppRej(ObjTrvlReq, ViewState["ProjID"].ToString().Trim());
                        MsgCls("Travel Request Rejected successfully !", LblMsg, Color.Green);
                        SendMailMethodtToEmpRej(ObjTrvlReq);
                        GV_TravelReqAppRej.EditIndex = -1;
                        PageLoadEvents();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        private void SendMailMethodtToEmpRej(TrvlReqDetails ObjTrvlReq)
        {
            try
            {

                //StringWriter sw1 = new StringWriter();
                //HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //GV_TravelReqAppRej.RenderControl(hw1);

                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;
                //string APPROVED_BYNext = "";
                //string Approver_Name = "";
                //string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                // string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                string TravelDeskMailId = "";
                string cdate1 = "";
                string cdate2 = "";
                string adamt = "";


                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_TravelApp(ObjTrvlReq.REINR, User.Identity.Name, ObjTrvlReq.STATUS, ViewState["Createdby"].ToString().Trim(), ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref TravelDeskMailId, ref cdate1, ref cdate2, ref adamt, ViewState["ProjID"].ToString());



                strSubject = "Travel Requisition has been rejected by " + PRSNTAPPROVEDBY_Name;


                RecipientsString = EMP_Email;
                strPernr_Mail = PRSNTAPPROVEDBY_Email;

                //GridViewRow selectedrow = grdAppRejIexp.SelectedRow;


                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>Travel Requisition has been rejected by " + PRSNTAPPROVEDBY_Name + "  |  " + User.Identity.Name + "<br/><br/></b>";
                body += "<b>Travel Request Details :<hr /><br/>";
                body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
                body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
                body += "<tr><td>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</td></tr>";
                body += "<tr><td>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</td></tr>";
                body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
                body += "<tr><td>From Date </td><td> : </td><td> " + ObjTrvlReq.DATV1 + "</td></tr>";
                body += "<tr><td>To Date </td><td> : </td><td> " + ObjTrvlReq.DATB1 + "</td></tr>";
                body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
                body += "<tr><td>Additional Advance </td><td> : </td><td> " + ObjTrvlReq.ADDIT_AMNT + "</td></tr>";
                //   body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
                body += "<tr><td>Currency </td><td> : </td><td> " + ObjTrvlReq.CURRENCY + "</td></tr></table>";
                body += "<br/><br/>";

                //    //End of preparing the mail body-------------------------------------------

                ////Newly added Starts
                Thread email = new Thread(delegate()
                {
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                });

                email.IsBackground = true;
                email.Start();
                ////Newly added Ends

                ////iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                LblMsg.ForeColor = System.Drawing.Color.Green;
                LblMsg.Text = "Mail sent successfully.";

            }
            catch
            {
                LblMsg.ForeColor = System.Drawing.Color.Red;
                LblMsg.Text = "Unknown error occured. Please contact your system administrator.";
                return;
            }

        }


        private void SendMailMethodtToEmp(TrvlReqDetails ObjTrvlReq)
        {
            try
            {
                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;
                //string APPROVED_BYNext = "";
                //string Approver_Name = "";
                //string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                // string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                string TravelDeskMailId = "";
                string cdate1 = "";
                string cdate2 = "";
                string adamt = "";


                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_TravelApp(ObjTrvlReq.REINR, User.Identity.Name, ObjTrvlReq.STATUS, ViewState["Createdby"].ToString().Trim(), ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref TravelDeskMailId, ref cdate1, ref cdate2, ref adamt, ViewState["ProjID"].ToString());

                if ((cdate1 == "X" || cdate2 == "X") && (adamt != "0.00"))
                {
                    // Travel ID " + ObjTrvlReq.REINR + " From date and / or To date Updation Requesthas been approved by" + EMP_Name + "  |  " + User.Identity.Name + " is pending for the Approval
                    strSubject = "Your Request for From date , To date and Additional Advance has been approved by " + PRSNTAPPROVEDBY_Name;

                    RecipientsString = EMP_Email + " , " + TravelDeskMailId;
                    strPernr_Mail = PRSNTAPPROVEDBY_Email;

                    //GridViewRow selectedrow = grdAppRejIexp.SelectedRow;


                    //    //Preparing the mail body--------------------------------------------------

                    string body = "Dear " + EMP_Name + " ,<br/><br/>";
                    body += "Please be informed that your request for <b>From Date " + ObjTrvlReq.DATV1 + "</b> , <b>To Date " + ObjTrvlReq.DATB1 + " </b>and <b>" + ObjTrvlReq.CURRENCY + "</b>has been approved.<br/>";
                    body += "Travel Desk along with the Finance team would arrange for the transfer to be done at the earliest.<hr /><br/>";

                    body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
                    body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
                    body += "<tr><td><b>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</b></td></tr>";
                    body += "<tr><td><b>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</b></td></tr>";
                    body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
                    body += "<tr><td><b>From Date </b></td><td> : </td><td><b> " + ObjTrvlReq.DATV1 + "</b></td></tr>";
                    body += "<tr><td><b>To Date </td><td> : </td><td><b> " + ObjTrvlReq.DATB1 + "</b></td></tr>";
                    body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
                    body += "<tr><td><b>Additional Advance </b></td><td> : </td><td><b> " + ObjTrvlReq.ADDIT_AMNT + " " + ObjTrvlReq.CURRENCY + "</b></td></tr></table>";
                    //body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
                    //body += "<tr><td><b>Currency </td><td> : </td><td> " + ObjTrvlReq.CURRENCY + "</b></td></tr></table>";
                    body += "<br/><br/>";

                    //    //End of preparing the mail body-------------------------------------------

                    ////Newly added Starts
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });

                    email.IsBackground = true;
                    email.Start();
                    ////Newly added Ends
                    
                    ////iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                    LblMsg.ForeColor = System.Drawing.Color.Green;
                    LblMsg.Text = "Mail sent successfully.";

                }
                else if (cdate1 == "X" || cdate2 == "X")
                {
                    // Travel ID " + ObjTrvlReq.REINR + " From date and / or To date Updation Requesthas been approved by" + EMP_Name + "  |  " + User.Identity.Name + " is pending for the Approval
                    strSubject = "Your Request for From date and / or To date has been approved by " + PRSNTAPPROVEDBY_Name;

                    RecipientsString = EMP_Email + " , " + TravelDeskMailId;
                    strPernr_Mail = PRSNTAPPROVEDBY_Email;

                    //GridViewRow selectedrow = grdAppRejIexp.SelectedRow;


                    //    //Preparing the mail body--------------------------------------------------

                    string body = "Dear " + EMP_Name + " ,<br/><br/>";
                    body += "Please be informed that your request for <b>From Date " + ObjTrvlReq.DATV1 + "</b> and <b> To Date " + ObjTrvlReq.DATB1 + "</b> has been approved.<br/>";
                    body += "Travel Desk along with the Finance team would arrange for the transfer to be done at the earliest.<hr /><br/>";



                    body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
                    body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
                    body += "<tr><td>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</td></tr>";
                    body += "<tr><td>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</td></tr>";
                    body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
                    body += "<tr><td><b>From Date </b></td><td> : </td><td><b> " + ObjTrvlReq.DATV1 + "</b></td></tr>";
                    body += "<tr><td><b>To Date </td><td> : </td><td><b> " + ObjTrvlReq.DATB1 + "</b></td></tr>";
                    body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
                    body += "<tr><td>Additional Advance </td><td> : </td><td> " + ObjTrvlReq.ADDIT_AMNT + " " + ObjTrvlReq.CURRENCY + "</td></tr></table>";
                    //body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
                    // body += "<tr><td>Currency </td><td> : </td><td> " + ObjTrvlReq.CURRENCY + "</td></tr></table>";
                    body += "<br/><br/>";

                    //    //End of preparing the mail body-------------------------------------------

                    ////Newly added Starts
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });

                    email.IsBackground = true;
                    email.Start();
                    ////Newly added Ends

                    ////iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                    LblMsg.ForeColor = System.Drawing.Color.Green;
                    LblMsg.Text = "Mail sent successfully.";
                }

                else if (adamt != "0.00")
                {
                    strSubject = "Your Request for Additional Advance has been approved by " + PRSNTAPPROVEDBY_Name;

                    RecipientsString = EMP_Email + " , " + TravelDeskMailId;
                    strPernr_Mail = PRSNTAPPROVEDBY_Email;

                    //GridViewRow selectedrow = grdAppRejIexp.SelectedRow;


                    //    //Preparing the mail body--------------------------------------------------

                    string body = "Dear " + EMP_Name + " ,<br/><br/>";
                    body += "Please be informed that your request for additional <b>" + ObjTrvlReq.CURRENCY + "</b> has been approved.<br/>";
                    body += "Travel Desk along with the Finance team would arrange for the transfer to be done at the earliest.<hr /><br/>";


                    body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
                    body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
                    body += "<tr><td>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</td></tr>";
                    body += "<tr><td>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</td></tr>";
                    body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
                    body += "<tr><td>From Date </td><td> : </td><td> " + ObjTrvlReq.DATV1 + "</td></tr>";
                    body += "<tr><td>To Date </td><td> : </td><td> " + ObjTrvlReq.DATB1 + "</td></tr>";
                    body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
                    body += "<tr><td><b>Additional Advance </b></td><td> : </td><td><b> " + ObjTrvlReq.ADDIT_AMNT + " " + ObjTrvlReq.CURRENCY + "</b></td></tr></table>";
                    //body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
                    // body += "<tr><td><b>Currency </td><td> : </td><td> " + ObjTrvlReq.CURRENCY + "</b></td></tr></table>";
                    body += "<br/><br/>";

                    //    //End of preparing the mail body-------------------------------------------

                    ////Newly added Starts
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });

                    email.IsBackground = true;
                    email.Start();
                    ////Newly added Ends

                    ////iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                    LblMsg.ForeColor = System.Drawing.Color.Green;
                    LblMsg.Text = "Mail sent successfully.";
                }

            }
            catch
            {
                LblMsg.ForeColor = System.Drawing.Color.Red;
                LblMsg.Text = "Unknown error occured. Please contact your system administrator.";
                return;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        //private void SendMailMethod(TrvlReqDetails ObjTrvlReq)
        //{
        //    try
        //    {
        //        StringWriter sw1 = new StringWriter();
        //        HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
        //        GV_TravelReqAppRej.RenderControl(hw1);
        //        string strSubject = string.Empty;
        //        string RecipientsString = string.Empty;
        //        string strPernr_Mail = string.Empty;
        //        string APPROVED_BYNext = "";
        //        string Approver_Name = "";
        //        string Approver_Email = "";
        //        string EMP_Name = "";
        //        string EMP_Email = "";
        //        string CREATED_BY = "";
        //        string PRSNTAPPROVEDBY_Email = "";
        //        string PRSNTAPPROVEDBY_Name = "";

        //        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

        //        objTravelRequestDataContext.sp_Get_MailList_TravelClaimApp(ObjTrvlReq.ID, ObjTrvlReq.APPROVED_BY, ObjTrvlReq.STATUS, ref CREATED_BY, ref APPROVED_BYNext, ref Approver_Name,
        //       ref Approver_Email, ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name);


        //        if (Approver_Email != null)
        //        {

        //            strSubject = "Travel Requisition has been Raised by " + EMP_Name;

        //            RecipientsString = Approver_Email;
        //            strPernr_Mail = EMP_Email;

        //            //    //Preparing the mail body--------------------------------------------------
        //            string body = "<b>Travel Requisition has been Raised by " + EMP_Name + "  |  " + CREATED_BY + "<br/><br/>";
        //            body += "<b>Travel Requisition Details :</b><hr />" + sw1.ToString(); ;


        //            //    //End of preparing the mail body-------------------------------------------
        //            // iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            LblMsg.ForeColor = System.Drawing.Color.Green;
        //            LblMsg.Text = "Mail sent successfully.";
        //        }

        //    }
        //    catch
        //    {
        //        LblMsg.ForeColor = System.Drawing.Color.Red;
        //        LblMsg.Text = "Unknown error occured. Please contact your system administrator.";
        //        return;
        //    }
        //}

        protected void GV_TravelReqAppRej_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GV_TravelReqAppRej_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GV_TravelReqAppRej_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
    }
}
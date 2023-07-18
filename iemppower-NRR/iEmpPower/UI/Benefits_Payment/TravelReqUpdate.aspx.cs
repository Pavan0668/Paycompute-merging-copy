using AjaxControlToolkit;
using iEmpPower.Old_App_Code.iEmpPowerMaster;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class TravelReqUpdate : System.Web.UI.Page
    {
        protected MembershipUser memUser;
        protected override void OnPreRender(EventArgs e)
        {
            // add base.OnPreRender(e); at the beginning of the method.
            base.OnPreRender(e);
            // codes to handle with your controls.
        }
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

                TrvlReqboList = travelrequestblObj.Get_TravelReqDetails(User.Identity.Name);
                GV_TravelReqUpdate.DataSource = TrvlReqboList;
                GV_TravelReqUpdate.DataBind();

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void GV_TravelReqUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "UPDATE":
                        string @REINR = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                        string @TripType = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["KZREA"].ToString();
                        string From = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["KUNDE"].ToString();
                        string To = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZORT1"].ToString();
                        string Country = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZLAND"].ToString();
                        string Project = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();
                        string TotalAdvance = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["SUM_ADVANC"].ToString();
                             string Fdate = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString();
                        string Tdate = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString();
                        string Addamt = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ADDIT_AMNT"].ToString();
                        string Currency = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["CURRENCY"].ToString();
                         string Prjid =  GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString().Split(':')[0].Trim();
                        ViewState["ProjID"]=Prjid;

                        DateTime DtFrom;
                        DateTime DtTo;
                        using (TextBox TxtFrmDate = (TextBox)GV_TravelReqUpdate.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("TxtGvFrmDate"))
                        using (TextBox TxtToDate = (TextBox)GV_TravelReqUpdate.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("TxtGvToDate"))
                        using (TextBox TxtAdvance = (TextBox)GV_TravelReqUpdate.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("TxtGvAdditionalAdvance"))
                        using (DropDownList DDLCurrency = (DropDownList)GV_TravelReqUpdate.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLCurrency"))
                        {
                            if (!string.IsNullOrEmpty(TxtFrmDate.Text) && !string.IsNullOrEmpty(TxtToDate.Text) && !string.IsNullOrEmpty(TxtAdvance.Text))
                            {
                                if (DateTime.TryParse(TxtFrmDate.Text, out DtFrom))
                                {
                                    if (DateTime.TryParse(TxtToDate.Text, out DtTo))
                                    {
                                        if (DtTo >= DtFrom)
                                        {
                                            travelrequestbl ObjTrvl = new travelrequestbl();
                                            TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                                            ObjTrvlReq.PERNR = User.Identity.Name;
                                            ObjTrvlReq.REINR = @REINR;
                                            ObjTrvlReq.KZREA = @TripType;
                                            ObjTrvlReq.KUNDE = From;
                                            ObjTrvlReq.ZORT1 = To;
                                            ObjTrvlReq.ZLAND = Country;
                                            ObjTrvlReq.WBS_ELEMT = Project;
                                            ObjTrvlReq.SUM_ADVANC = decimal.Parse(TotalAdvance);
                                            ObjTrvlReq.DATV1 = DtFrom;
                                            ObjTrvlReq.DATB1 = DtTo;
                                            ObjTrvlReq.CURRENCY = DDLCurrency.SelectedValue;
                                            ObjTrvlReq.ADDIT_AMNT = decimal.Parse(TxtAdvance.Text);
                                            ObjTrvl.Update_TravelReq(ObjTrvlReq);
                                            MsgCls("Travel Request updated successfully and sent for Approval !", LblMsg, Color.Green);
                                            SendMailMethod(ObjTrvlReq, Fdate, Tdate, Addamt, Currency);
                                            GV_TravelReqUpdate.EditIndex = -1;
                                            PageLoadEvents();
                                        }
                                        else
                                        { MsgCls("From Date-Time cannot be less than to Date-Time !", LblMsg, Color.Red); }
                                    }
                                    else
                                    { MsgCls("Invalid To Date-Time", LblMsg, Color.Red); }
                                }
                                else
                                { MsgCls("Invalid From Date-Time", LblMsg, Color.Red); }
                            }
                            else
                            { MsgCls("Invalid travel details !", LblMsg, Color.Red); }
                        }

                        break;


                    case "VIEW":
                        string @REINR1 = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                        ////LoadgrdAdvance(@REINR1);
                        try
                        {
                            travelrequestcolumnsbo TrvlBO = new travelrequestcolumnsbo();
                            travelrequestbl travelrequestblObj = new travelrequestbl();
                            List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                            TrvlReqboList = travelrequestblObj.Get_TravelAdvanceDetails(REINR1, User.Identity.Name);
                            // GridView grdAdvance = (GridView)GV_TravelReqUpdate.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("grdAdvance");
                            //grdAdvance.DataSource = TrvlReqboList;
                            //grdAdvance.DataBind();


                            if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                            {
                                grdAdvance.Visible = false;
                                grdAdvance.DataSource = null;
                                MsgCls("Advance not taken for this trip", LblMsg, Color.Red);

                                return;
                            }
                            else
                            {
                                grdAdvance.Visible = true;
                                grdAdvance.DataSource = TrvlReqboList;
                                grdAdvance.SelectedIndex = -1;
                                MsgCls("", LblMsg, Color.Transparent);

                            }
                            grdAdvance.DataBind();

                        }
                        catch (Exception Ex)
                        { MsgCls(Ex.Message, LblMsg, Color.Red); }


                        break;

                    default:
                        break;
                }
            }
            //catch (Exception Ex)
            //{ 
                
            //    //MsgCls(Ex.Message, LblMsg, Color.Red);
            //}
            catch (Exception Ex)
            {
                
                switch (Ex.Message)
                {
                    
                    case "-01":
                        MsgCls("Manager Perner is not there", LblMsg, Color.Red);
                        break;
                    case "-02":
                        MsgCls("Overlaps of date is not allowed", LblMsg, Color.Red);
                        break;
                    case "-03":
                        MsgCls("You haven't changed anything", LblMsg, Color.Red);
                        //PageLoadEvents();
                        break;
                    default:
                        MsgCls("Unknown error occured. Please contact your system administrator.<br/>" + Ex.Message, LblMsg, Color.Red);
                        break;
                }
                //MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }


        //private void SendMailMethod(TrvlReqDetails ObjTrvlReq)
        //{
        //    try
        //    {
        //        string strSubject = string.Empty;
        //        string RecipientsString = string.Empty;
        //        string strPernr_Mail = string.Empty;
        //        string APPROVED_BY1 = "";
        //        string Approver_Name = "";
        //        string Approver_Email = "";
        //        string EMP_Name = "";
        //        string EMP_Email = "";

        //        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

        //        objTravelRequestDataContext.sp_Get_MailList_Travel(ObjTrvlReq.REINR, User.Identity.Name, ref APPROVED_BY1, ref Approver_Name, ref Approver_Email, ref EMP_Name, ref EMP_Email, ViewState["ProjID"].ToString());

        //        strSubject = "Travel Request has been Raised by " + EMP_Name + "  |  " + User.Identity.Name + " is pending for your Approval.";
        //        RecipientsString = Approver_Email;
        //        strPernr_Mail = EMP_Email;

        //        //    //Preparing the mail body--------------------------------------------------
        //        string body = "<b>Travel Request has been Raised by " + EMP_Name + "  |  " + User.Identity.Name + " is pending for your Approval.<br/><br/></b>";
        //        body += "<b>Travel Request Details :<hr /><br/>";
        //        body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
        //        body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
        //        body += "<tr><td>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</td></tr>";
        //        body += "<tr><td>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</td></tr>";
        //        body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
        //        body += "<tr><td>From Date </td><td> : </td><td> " + ObjTrvlReq.DATV1 + "</td></tr>";
        //        body += "<tr><td>To Date </td><td> : </td><td> " + ObjTrvlReq.DATB1 + "</td></tr>";
        //        body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
        //        body += "<tr><td>Additional Advance </td><td> : </td><td> " + ObjTrvlReq.ADDIT_AMNT + "</td></tr>";
        //       // body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
        //        body += "<tr><td>Currency </td><td> : </td><td> " + ObjTrvlReq.CURRENCY + "</td></tr></table>";
        //        body += "<br/><br/>";
        //        //    //End of preparing the mail body-------------------------------------------
        //        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //        LblMsg.ForeColor = System.Drawing.Color.Green;
        //        LblMsg.Text = "Mail sent successfully.";
        //    }
        //    catch
        //    {
        //        LblMsg.ForeColor = System.Drawing.Color.Red;
        //        LblMsg.Text = "Unknown error occured. Please contact your system administrator.";
        //        return;
        //    }
        //}


        private void SendMailMethod(TrvlReqDetails ObjTrvlReq, string Fdate, string Tdate, string Addamt, string Currency)
        {
            try
            {
                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;
                string APPROVED_BY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";

                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_Travel(ObjTrvlReq.REINR, User.Identity.Name, ref APPROVED_BY1, ref Approver_Name, ref Approver_Email, ref EMP_Name, ref EMP_Email, ViewState["ProjID"].ToString());

                if ((DateTime.Parse(Fdate) != ObjTrvlReq.DATV1 || DateTime.Parse(Tdate) != ObjTrvlReq.DATB1) && (decimal.Parse(Addamt) != ObjTrvlReq.ADDIT_AMNT))
                {

                    // strSubject = "Travel ID " + ObjTrvlReq.REINR + " From date, To date and Additional Amount has been Updated by " + EMP_Name + "  |  " + User.Identity.Name + " is pending for the Approval.";


                    strSubject = " From date, To date and Additional Advance has been Updated by " + EMP_Name + "  |  " + User.Identity.Name + " to the existing trip " + ObjTrvlReq.REINR + " and is pending for the Approval.";
                    RecipientsString = Approver_Email;
                    strPernr_Mail = EMP_Email;

                    //    //Preparing the mail body--------------------------------------------------

                    string body = "Dear " + Approver_Name + " ,<br/><br/>";
                    body += "Please be informed that <b>" + EMP_Name + " </b>has requested for <b>From Date " + ObjTrvlReq.DATV1 + "</b> , <b>To Date " + ObjTrvlReq.DATB1 + " </b>and <b>" + ObjTrvlReq.CURRENCY + "</b> to his existing trip <b>" + ObjTrvlReq.REINR + "</b><br/>";
                    body += "Please approve the below request.<hr /><br/>";


                    body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
                    body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
                    body += "<tr><td>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</td></tr>";
                    body += "<tr><td>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</td></tr>";
                    body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
                    body += "<tr><td><b>From Date has been changed from </b></td><td> : </td><td><b> " + Fdate + " to " + ObjTrvlReq.DATV1 + "</b></td></tr>";
                    body += "<tr><td><b>To Date  has been changed from </b></td><td> : </td><td><b> " + Tdate + " to " + ObjTrvlReq.DATB1 + "</b></td></tr>";
                    body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
                    body += "<tr><td><b>Additional Advance has been changed from </b></td><td> : </td><td><b> " + Addamt + " " + Currency + " to " + ObjTrvlReq.ADDIT_AMNT + " " + ObjTrvlReq.CURRENCY + "</b></td></tr></table>";
                    //body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
                    // body += "<tr><td><b>Currency has been changed from </b></td><td> : </td><td><b> " + Currency + " to " + ObjTrvlReq.CURRENCY + "</b></td></tr></table>";
                    body += "<br/><br/>";
                    //    //End of preparing the mail body-------------------------------------------
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    //LblMsg.ForeColor = System.Drawing.Color.Green;
                    //   LblMsg.Text = "Mail sent successfully.";
                }

                else if (DateTime.Parse(Fdate) != ObjTrvlReq.DATV1 || DateTime.Parse(Tdate) != ObjTrvlReq.DATB1)
                {
                    strSubject = " From date and / or To date has been Updated by " + EMP_Name + "  |  " + User.Identity.Name + " to the existing trip " + ObjTrvlReq.REINR + " and is pending for the Approval.";
                    RecipientsString = Approver_Email;
                    strPernr_Mail = EMP_Email;

                    //    //Preparing the mail body--------------------------------------------------
                    string body = "Dear " + Approver_Name + " ,<br/><br/>";
                    body += "Please be informed that <b>" + EMP_Name + " </b>has requested for <b>From Date " + ObjTrvlReq.DATV1 + "</b> and <b>To Date " + ObjTrvlReq.DATB1 + " </b>to his existing trip <b>" + ObjTrvlReq.REINR + "</b><br/>";
                    body += "Please approve the below request.<hr /><br/>";

                    body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
                    body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
                    body += "<tr><td>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</td></tr>";
                    body += "<tr><td>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</td></tr>";
                    body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
                    body += "<tr><td><b>From Date has been changed from </b></td><td> : </td><td><b> " + Fdate + " to " + ObjTrvlReq.DATV1 + "</b></td></tr>";
                    body += "<tr><td><b>To Date  has been changed from </b></td><td> : </td><td><b> " + Tdate + " to " + ObjTrvlReq.DATB1 + "</b></td></tr>";
                    body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
                    body += "<tr><td>Additional Advance </td><td> : </td><td> " + ObjTrvlReq.ADDIT_AMNT + " " + ObjTrvlReq.CURRENCY + "</td></tr></table>";
                    //body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
                    //body += "<tr><td>Currency </td><td> : </td><td> " + ObjTrvlReq.CURRENCY + "</td></tr></table>";
                    body += "<br/><br/>";
                    //    //End of preparing the mail body-------------------------------------------
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    //  LblMsg.ForeColor = System.Drawing.Color.Green;
                    //LblMsg.Text = "Mail sent successfully.";
                }

                else if (decimal.Parse(Addamt) != ObjTrvlReq.ADDIT_AMNT)
                {
                    strSubject = "Additional Advance has been Requested by " + EMP_Name + "  |  " + User.Identity.Name + " to the existing trip " + ObjTrvlReq.REINR + " and is pending for the Approval.";
                    RecipientsString = Approver_Email;
                    strPernr_Mail = EMP_Email;

                    //    //Preparing the mail body--------------------------------------------------

                    string body = "Dear " + Approver_Name + " ,<br/><br/>";
                    body += "Please be informed that <b>" + EMP_Name + " </b>has requested for <b>" + ObjTrvlReq.CURRENCY + "</b> to his existing trip <b>" + ObjTrvlReq.REINR + "</b><br/>";
                    body += "Please approve the below request.<hr /><br/>";


                    body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
                    body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
                    body += "<tr><td>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</td></tr>";
                    body += "<tr><td>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</td></tr>";
                    body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
                    body += "<tr><td>From Date </td><td> : </td><td> " + ObjTrvlReq.DATV1 + "</td></tr>";
                    body += "<tr><td>To Date </td><td> : </td><td> " + ObjTrvlReq.DATB1 + "</td></tr>";
                    body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
                    body += "<tr><td><b>Additional Advance has been changed from </b></td><td> : </td><td><b> " + Addamt + " " + Currency + " to " + ObjTrvlReq.ADDIT_AMNT + " " + ObjTrvlReq.CURRENCY + "<b></td></tr></table>";
                    //body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
                    //body += "<b><tr><td>Currency has been changed from </b></td><td> : </td><td><b> " + Currency + " to " + ObjTrvlReq.CURRENCY + "</b></td></tr></table>";
                    body += "<br/><br/>";
                    //    //End of preparing the mail body-------------------------------------------
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    //  LblMsg.ForeColor = System.Drawing.Color.Green;
                    // LblMsg.Text = "Mail sent successfully.";
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


        protected void GV_TravelReqUpdate_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GV_TravelReqUpdate.EditIndex = e.NewEditIndex;
                PageLoadEvents();

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void GV_TravelReqUpdate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GV_TravelReqUpdate.EditIndex = -1;
                PageLoadEvents();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void GV_TravelReqUpdate_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GV_TravelReqUpdate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }



        protected void GV_TravelReqUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_TravelReqUpdate.PageIndex = e.NewPageIndex;
                PageLoadEvents();
                Search();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        [WebMethod()]
        public static CascadingDropDownNameValue[] GetCurrencyTypes()
        {
            try
            {
                masterdalDataContext objDataContext;
                objDataContext = new masterdalDataContext();
                List<CascadingDropDownNameValue> CurrencyName = new List<CascadingDropDownNameValue>();
                foreach (var vRow in objDataContext.sp_master_load_payment_currency())
                {
                    CurrencyName.Add(new CascadingDropDownNameValue(vRow.WAERS + " - " + vRow.LTEXT, vRow.WAERS));
                }
                return CurrencyName.ToArray();
            }
            catch (Exception Ex)
            { return null; }
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {

            try
            {

                Search();
                //MsgCls(string.Empty, LblMsg, Color.Transparent);
                //string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                //string textSearch = txtsearch.Text;


                //travelrequestbl travelrequestblObj = new travelrequestbl();
                //List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                //TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsNew(User.Identity.Name, SelectedType, textSearch);
                //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //{
                //    MsgCls("No Records found", LblMsg, Color.Red);
                //    GV_TravelReqUpdate.Visible = false;
                //    GV_TravelReqUpdate.DataSource = null;
                //    GV_TravelReqUpdate.DataBind();
                //    return;
                //}
                //else
                //{
                //    MsgCls("", LblMsg, Color.Transparent);
                //    GV_TravelReqUpdate.Visible = true;
                //    GV_TravelReqUpdate.DataSource = TrvlReqboList;
                //    GV_TravelReqUpdate.SelectedIndex = -1;
                //    GV_TravelReqUpdate.DataBind();
                //    //GV_TravelClaimReqAppRej.Visible = false;
                //    grdAdvance.Visible = false;

                //}


            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }


        }


        protected void btnclear_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;

            
            //  GV_TravelClaimReqAppRej.Visible = false;
            grdAdvance.Visible = false;
            MsgCls("", LblMsg, Color.Transparent);
            PageLoadEvents();
            GV_TravelReqUpdate.Visible = true;
        }

        private void Search()
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;


                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsNew(User.Identity.Name, SelectedType, textSearch);
                if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                {
                    MsgCls("No Records found", LblMsg, Color.Red);
                    GV_TravelReqUpdate.Visible = false;
                    GV_TravelReqUpdate.DataSource = null;
                    GV_TravelReqUpdate.DataBind();
                    return;
                }
                else
                {
                    MsgCls("", LblMsg, Color.Transparent);
                    GV_TravelReqUpdate.Visible = true;
                    GV_TravelReqUpdate.DataSource = TrvlReqboList;
                    GV_TravelReqUpdate.SelectedIndex = -1;
                    GV_TravelReqUpdate.DataBind();
                    //GV_TravelClaimReqAppRej.Visible = false;
                    grdAdvance.Visible = false;

                }


            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_Personal_Information_communication_information : System.Web.UI.Page
{
    int CommInfoPageIndex = 1;

    #region Page_Init
    public void Page_Init(object o, EventArgs e)
    {
        try
        {
            if (Session["sEmploreeId"] == null)
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx", false);
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                PageLoadEvents();
                contact1.Visible = GV_CommInfo.Rows.Count > 0 ? true : false;
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

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
            Lbl.Text = Lbl.Text + Msg;
            Lbl.ForeColor = Clr;
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Page Load Events
    private void PageLoadEvents()
    {
        try
        {
            MV_CommInfo.SetActiveView(V_ViewCommInfo);
            BindGV_CommInfo(1);
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Bind GVCommunication Information
    private void BindGV_CommInfo(int PageIndex)
    {
        try
        {
            personal_informationBO objCommuInfoBo = new personal_informationBO();
            personal_informationBl objCommuInfoBl = new personal_informationBl();
            objCommuInfoBo.EmpID = User.Identity.Name;
            objCommuInfoBo.Comany_Code = Session["CompCode"].ToString(); ;
            objCommuInfoBo.flag = 1;
            objCommuInfoBo.Approved_By = "";
            personal_informationCollBo objPIAddBoLst = objCommuInfoBl.Get_EmpContactsInfo(objCommuInfoBo);
            if (objPIAddBoLst.Count > 0)
            {
                MsgCls(string.Empty, LblMsg, Color.White);
                GV_CommInfo.EditIndex = -1;
                GV_CommInfo.DataSource = objPIAddBoLst;
                GV_CommInfo.DataBind();
                //CommInfoviewPager(objPIAddBoLst.Count > 0 ? int.Parse(objPIAddBoLst[0].RecordCnt.ToString()) : 0, CommInfoPageIndex);
                MsgCls(string.Empty, LblMsg, Color.Transparent);
            }
            else
            {
                DataTable Dt = new DataTable();
                Dt.Columns.Add("RowNumber", typeof(int));
                Dt.Columns.Add("ID", typeof(Guid));
                Dt.Columns.Add("USTXT", typeof(string));
                Dt.Columns.Add("USRID", typeof(string));
                Dt.Columns.Add("RecordCnt", typeof(int));

                GVNodata(GV_CommInfo, Dt);
                MsgCls("No records found !", LblMsg, Color.Red);
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region GV_CommInfo Events

    protected void GV_CommInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            switch (e.CommandName.ToUpper())
            {
                case "EDIT":

                    int CommID = int.Parse(GV_CommInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                    ViewState["USERTYPETXT"] = GV_CommInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["Contact_Type_ID"].ToString();
                    ViewState["USERTYPEID"] = GV_CommInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["Contact_Type"].ToString();
                    picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                    picommunicationinformationbl objCommuInfoBl = new picommunicationinformationbl();
                    objCommuInfoBo.ID = CommID;
                    objCommuInfoBo.EMPLOYEE_ID = User.Identity.Name;
                    picommunicationinformationcollectionbo objPIAddBoLst = objCommuInfoBl.Get_Communication_Details_Full(objCommuInfoBo);
                    if (objPIAddBoLst.Count > 0)
                    {
                        FV_CommInfo.ChangeMode(FormViewMode.Edit);
                        FV_CommInfo.DataSource = objPIAddBoLst;
                        FV_CommInfo.DataBind();

                        MV_CommInfo.SetActiveView(V_AddEditCommInfo);
                        MsgCls(string.Empty, LblMsg, Color.Transparent);


                        if ((objPIAddBoLst[0].TRANSSTATUS == null ? "" : objPIAddBoLst[0].TRANSSTATUS.ToString().Trim()) == "Updated")
                        {
                            MsgCls("Editing can be done only after approval from HR of previous record updation", LblMsg, Color.Red);

                        }
                        else
                        {
                            MsgCls(string.Empty, LblMsg, Color.Transparent);
                        }

                    }
                    else
                    { MsgCls("Invalid ID", LblMsg, Color.Red); }

                    using (TextBox TxtCommDetails = (TextBox)FV_CommInfo.FindControl("TxtEditCommDetails"))
                    {
                        if (TxtCommDetails != null)
                        {
                            TxtCommDetails.Focus();
                        }
                    }

                    break;
                default:
                    break;
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    protected void GV_CommInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void GV_CommInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GV_CommInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GV_CommInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                using (LinkButton LbtnView = (LinkButton)e.Row.FindControl("GVEditCommInfo"))
                {
                    if (DataBinder.Eval(e.Row.DataItem, "USRTY").Equals("0010") && LbtnView != null)
                    { LbtnView.Enabled = LbtnView.Visible = false; }
                }
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Communication Info Populate pager
    private void CommInfoviewPager(int RecordCount, int currentPage)
    {
        try
        {
            List<ListItem> pages = new List<ListItem>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal(5));
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
            RptrCommInfoPager.DataSource = pages;
            RptrCommInfoPager.DataBind();
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    protected void RptrCommInfoPagerPage_Changed(object sender, EventArgs e)
    {
        try
        {
            CommInfoPageIndex = int.Parse((sender as LinkButton).CommandArgument);
            BindGV_CommInfo(CommInfoPageIndex);
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion


    #region Add / View Button Event

    protected void LblAddCommInfo_Click(object sender, EventArgs e)
    {
        try
        {
           
            MV_CommInfo.SetActiveView(V_AddEditCommInfo);
            FV_CommInfo.ChangeMode(FormViewMode.Insert);
            MsgCls(string.Empty, LblMsg, Color.Transparent);
            using (DropDownList DDL_Commtype = (DropDownList)FV_CommInfo.FindControl("DDL_Commtype"))
            {
                
                configurationcollectionbo objLst3 = configurationbl.Get_ContactTypes(1);
                DDL_Commtype.DataSource = objLst3;
                DDL_Commtype.DataTextField = "DDLTYPETEXT";
                DDL_Commtype.DataValueField = "DDLTYPE";
                DDL_Commtype.DataBind();
                DDL_Commtype.Items.Insert(0, new ListItem(" - SELECT TYPE - ", "0"));

                if (DDL_Commtype != null)
                {

                    DDL_Commtype.Focus();
                }
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    protected void LbtnBackCommInfoView_Click(object sender, EventArgs e)
    {
        try
        {
            PageLoadEvents();
            Response.Redirect("~/UI/Personal_Information/communication_information.aspx", false);
            //FV_CommInfo.ChangeMode(FormViewMode.ReadOnly);
            MsgCls(string.Empty, LblMsg, Color.Transparent);
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region FV_AddressInfo Events

    protected void FV_CommInfo_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        try
        {
            GetHRPernr();
            DateTime FrmDt = new DateTime(1900, 01, 01);
            DateTime ToDt = new DateTime(1900, 01, 01);
            switch (e.CommandName.ToUpper())
            {
                case "ADDCOMM":

                    using (DropDownList DDL_Commtype = (DropDownList)FV_CommInfo.FindControl("DDL_Commtype"))
                    using (TextBox TxtCommDetails = (TextBox)FV_CommInfo.FindControl("TxtCommDetails"))
                    {
                        if (!string.IsNullOrEmpty(DDL_Commtype.SelectedValue))
                        {
                            if (!string.IsNullOrEmpty(TxtCommDetails.Text.Trim()))
                            {
                                picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                                picommunicationinformationbl objCommuInfoBl = new picommunicationinformationbl();

                                string HRMail = "";
                                string SuperVisorMail = "";
                                string PernrName = "";
                                string PernrEMail = "";


                                //if (DDL_Commtype.SelectedValue.ToString().Trim() == "CELL" || DDL_Commtype.SelectedValue.ToString().Trim() == "MPHN")
                                //{
                                //    if (TxtCommDetails.Text.Trim().Length != 10)
                                //    {
                                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter valid 10 digits Mobile Number')", true);
                                //        return;
                                //    }
                                //}

                                objCommuInfoBo.ID = 0;
                                objCommuInfoBo.EMPLOYEE_ID = User.Identity.Name;
                                objCommuInfoBo.USRTY = DDL_Commtype.SelectedValue;
                                objCommuInfoBo.USRID = TxtCommDetails.Text.Trim();
                                ViewState["USERTYPETXT"] = DDL_Commtype.SelectedItem.Text.ToString();
                                ViewState["USERTYPEID"] = DDL_Commtype.SelectedValue;
                                objCommuInfoBo.BEGDA = DateTime.Now;
                                objCommuInfoBo.ENDDA = new DateTime(9999, 12, 31);
                                objCommuInfoBo.CREATED_BY = User.Identity.Name;
                                objCommuInfoBo.CREATED_ON = DateTime.Now;
                                objCommuInfoBo.MODIFIED_BY = Session["CompCode"].ToString();
                                objCommuInfoBo.MODIFIEDON = DateTime.Now;
                                objCommuInfoBo.STATUS = "";
                                objCommuInfoBo.Flag = 1;

                                objCommuInfoBl.Add_Update_Del_CommDetails(objCommuInfoBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                                SendMail(objCommuInfoBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Saved");

                                //FV_CommInfo.ChangeMode(FormViewMode.ReadOnly);
                                //PageLoadEvents();
                                LblMsg.Text = string.Empty;
                                //Response.Redirect("~/UI/Personal_Information/communication_information.aspx", false);
                                //MsgCls("Communication Information added successfully and sent for approval!", LblMsg, Color.Green);
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Information added successfully and sent for approval!')", true);
                              
                                if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                                {
                                    MsgCls("Communication Information added successfully !", LblMsg, Color.Green);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Information added successfully !');parent.location.href='communication_information.aspx'", true);
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Communication Information added successfully !')", true);
                                    //Response.Redirect("~/UI/Personal_Information/communication_information.aspx", false);
                                    contact1.Visible = GV_CommInfo.Rows.Count > 0 ? true : false;
                                }
                                else
                                {
                                    MsgCls("Communication Information added successfully and sent for approval !", LblMsg, Color.Green);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Information added successfully and sent for approval  !');parent.location.href='communication_information.aspx'", true);
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Communication Information added successfully and sent for approval !')", true);
                                    //Response.Redirect("~/UI/Personal_Information/communication_information.aspx", false);
                                    contact1.Visible = GV_CommInfo.Rows.Count > 0 ? true : false;
                                }

                            }
                            else
                            { MsgCls("Invalid User ID !", LblMsg, Color.Red); }
                        }
                        else
                        { MsgCls("Invalid User ID type !", LblMsg, Color.Red); }
                    }
                    break;

                case "EDITCOMM":
                    FV_CommInfo.ChangeMode(FormViewMode.Edit);
                    LoadCommInfoFull(int.Parse(FV_CommInfo.DataKey["ID"].ToString()));
                    using (TextBox TxtCommDetails = (TextBox)FV_CommInfo.FindControl("TxtCommDetails"))
                    {
                        if (TxtCommDetails != null)
                        {
                            TxtCommDetails.Focus();
                        }
                    }
                    break;

                case "CANCEL":

                    //LoadCommInfoFull(Guid.Parse(FV_CommInfo.DataKey["ID"].ToString()));
                    PageLoadEvents();
                    //FV_CommInfo.ChangeMode(FormViewMode.ReadOnly);
                    Response.Redirect("~/UI/Personal_Information/communication_information.aspx");

                    break;
                case "UPDATECOMM":
                    if (FV_CommInfo.CurrentMode == FormViewMode.Edit)
                    {
                        using (TextBox TxtEditCommDetails = (TextBox)FV_CommInfo.FindControl("TxtEditCommDetails"))
                        {
                            if (!string.IsNullOrEmpty(TxtEditCommDetails.Text.Trim()))
                            {

                                picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                                picommunicationinformationbl objCommuInfoBl = new picommunicationinformationbl();

                                string HRMail = "";
                                string SuperVisorMail = "";
                                string PernrName = "";
                                string PernrEMail = "";



                                if (FV_CommInfo.DataKey["USRID"].ToString().Trim() == "CELL" || FV_CommInfo.DataKey["USRID"].ToString().Trim() == "MPHN")
                                {
                                    if (TxtEditCommDetails.Text.Trim().Length != 10)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter valid 10 digits Mobile Number')", true);
                                        return;
                                    }
                                }

                                objCommuInfoBo.ID = int.Parse(FV_CommInfo.DataKey["ID"].ToString());
                                objCommuInfoBo.EMPLOYEE_ID = User.Identity.Name;
                                //objCommuInfoBo.USRTY = DDL_Commtype.SelectedValue;
                                objCommuInfoBo.USRID = TxtEditCommDetails.Text.Trim();

                                objCommuInfoBo.BEGDA = DateTime.Now;
                                objCommuInfoBo.ENDDA = new DateTime(9999, 12, 31);
                                objCommuInfoBo.CREATED_BY = User.Identity.Name;
                                objCommuInfoBo.CREATED_ON = DateTime.Now;
                                objCommuInfoBo.MODIFIED_BY = Session["CompCode"].ToString();
                                objCommuInfoBo.MODIFIEDON = DateTime.Now;
                                //objCommuInfoBo.STATUS = "";
                                objCommuInfoBo.Flag = 2;

                                objCommuInfoBl.Add_Update_Del_CommDetails(objCommuInfoBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                                SendMail(objCommuInfoBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Updated");

                                //FV_CommInfo.ChangeMode(FormViewMode.ReadOnly);
                                //PageLoadEvents();
                                LblMsg.Text = string.Empty;
                                //Response.Redirect("~/UI/Personal_Information/communication_information.aspx",false);
                                //MsgCls("Communication Information updated successfully and sent for approval!", LblMsg, Color.Green);
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Information updated successfully and sent for approval!')", true);

                                if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                                {
                                    MsgCls("Communication Information updated successfully !", LblMsg, Color.Green);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Information updated successfully !');parent.location.href='communication_information.aspx'", true);
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Communication Information updated successfully !')", true);
                                    //Response.Redirect("~/UI/Personal_Information/communication_information.aspx", false);
                                    contact1.Visible = GV_CommInfo.Rows.Count > 0 ? true : false;
                                }
                                else
                                {
                                    MsgCls("Communication Information updated successfully and sent for approval !", LblMsg, Color.Green);

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Communication Information updated successfully and sent for approval !');parent.location.href='communication_information.aspx'", true);
                                    //Response.Redirect("~/UI/Personal_Information/communication_information.aspx", false);
                                    contact1.Visible = GV_CommInfo.Rows.Count > 0 ? true : false;
                                }

                                
                                string[] MsgCC = { HRMail, SuperVisorMail };
                                // iEmpPowerMaster_Load.masterbl.SendMail(PernrEMail, MsgCC, "Communication Info Added - " + PernrName + " (" + User.Identity.Name + ")", GetMailBody(objCommuInfoBo, PernrName, "Added"));
                               
                            }

                            else
                            { MsgCls("Invalid Communication ID !", LblMsg, Color.Red); }
                            //}
                            //else
                            //{ MsgCls("Invalid Address Type !", LblAddressAddMsg, Color.Red); }
                        }
                        
                    }
                    break;

                default:
                    break;
            }
        }
        catch (Exception Ex)
        {
            switch (Ex.Message)
            {
                case "-0":
                    MsgCls("Communication type already exists !", LblMsg, Color.Red);
                    break;
                default:
                    MsgCls(Ex.Message, LblMsg, Color.Red);
                    break;
            }
        }
    }

    protected void GetHRPernr()
    {
        msassignedtomebo objAssginTMBo = new msassignedtomebo();
        msassignedtomebl objAssginTMBl = new msassignedtomebl();


        msassignedtomecollectionbo objAssginTMBolist = objAssginTMBl.Get_HRPERNR(User.Identity.Name.ToString().Trim(), Session["CompCode"].ToString());
        if (objAssginTMBolist.Count > 0)
        {
            foreach (msassignedtomebo objBo in objAssginTMBolist)
            {
                //LOGINPERNR = objBo.PERNR;
                ViewState["LOGINPERNR"] = objBo.PERNR;
            }


        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('HR Admin information is not maintained in iEmpPower!');", true);
        }
    }
    public void SendMail(picommunicationinformationbo objCommuInfoBo, ref string HRMail, ref string SuperVisorMail, ref string PernrName, ref string PernrEMail, string type)
    {
        try
        {

            GetHRPernr();
            string strSubject = string.Empty;
            string strSubject1 = string.Empty;

            string ccode = Session["CompCode"].ToString();
            string empid = User.Identity.Name;
            int cnt = ccode.Length;
            empid = empid.Substring(cnt).ToUpper();

            if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
            {
                if (type == "Updated")
                {
                    strSubject1 = "IEmpPower Paycompute - Notification";
                    strSubject = "Communication Information has been updated by " + Session["Empname"].ToString() + "  | " + empid.ToString() +".This has been Self Approved, No Action Required.";
                }
                else if (type == "Saved")
                {
                    strSubject1 = "IEmpPower Paycompute - Notification";
                    strSubject = "Communication Information has been created by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".This has been Self Approved, No Action Required.";
                }
            }

            else
            {


                if (type == "Updated")
                {
                    strSubject1 = "IEmpPower Paycompute - Notification";
                    strSubject = "Communication Information has been updated by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".";
                }
                else if (type == "Saved")
                {
                    strSubject1 = "IEmpPower Paycompute - Notification";
                    strSubject = "Communication Information has been created by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".";
                }
            }

            string RecipientsString = HRMail;
            string strPernr_Mail = PernrEMail;

            //    //Preparing the mail body--------------------------------------------------

            string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
            body = body + "<b style= 'font-size: 14px';>Communication details : </b><hr>";
            body += "<b><table style=border-collapse:collapse;><tr><td style='font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Communication Type </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ViewState["USERTYPETXT"].ToString() + "</td></tr>";
            body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Communication ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objCommuInfoBo.USRID.ToString() + "</td></tr></table></b>";
            //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Valid to </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objPIAddBo.DATE_TO.ToString() + "</td></tr>";
            //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Telephone number </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objPIAddBo.PHONENO.ToString() + "</td></tr></table></b>";
            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";

            // body += "</br>" + sw1.ToString() + "<br/>";
            //    //End of preparing the mail body-------------------------------------------
            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject1, strPernr_Mail, body);

        }



        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }


    protected void FV_CommInfo_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        try
        {
            FV_CommInfo.ChangeMode(e.NewMode);
            if (FV_CommInfo.DataKey["ID"] != null)
            { LoadCommInfoFull(int.Parse(FV_CommInfo.DataKey["ID"].ToString())); }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    #endregion

    #region Load Communication information full
    private void LoadCommInfoFull(int ID)
    {
        try
        {

            picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
            picommunicationinformationbl objCommuInfoBl = new picommunicationinformationbl();
            objCommuInfoBo.ID = ID;
            objCommuInfoBo.EMPLOYEE_ID = User.Identity.Name;
            FV_CommInfo.DataSource = null;
            FV_CommInfo.DataBind();
            picommunicationinformationcollectionbo objPIAddBoLst = objCommuInfoBl.Get_Communication_Details_Full(objCommuInfoBo);
            if (objPIAddBoLst.Count > 0)
            {
                FV_CommInfo.DataSource = objPIAddBoLst;
                FV_CommInfo.DataBind();
            }
            else
            { MsgCls("Invalid ID", LblMsg, Color.Red); }

        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    //----------------- EMAIL SENDING -------------------------------------

    #region Get Mail Body
    private string GetMailBody(picommunicationinformationbo objPIAddBo, string EmpName, string AddressStatus)
    {
        try
        {
            StringBuilder Sb = new StringBuilder();
            string Mailbody = string.Empty;
            string AddressInfoFilePath = Server.MapPath(@"." + "/EmailTemplates/EmpCommunicationInfoTemplate.html");

            Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Type</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + objPIAddBo.USTXT + "</td><td></td></tr>");
            Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Detail</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + objPIAddBo.USRID + "</td><td></td></tr>");
            Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Date</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + "</td><td></td></tr>");


            Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
            Mailbody = Mailbody.Replace("##COMMTYPE##", objPIAddBo.USTXT);
            Mailbody = Mailbody.Replace("##EMPNAME##", EmpName);
            Mailbody = Mailbody.Replace("##EMPPERNR##", User.Identity.Name);
            Mailbody = Mailbody.Replace("##STATUS##", AddressStatus);
            Mailbody = Mailbody.Replace("##CONTENT##", Sb.ToString());

            return Mailbody;
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); return string.Empty; }
    }

    #endregion



}

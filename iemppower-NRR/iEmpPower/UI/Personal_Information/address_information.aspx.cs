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

namespace iEmpPower
{
    public partial class UI_Employee_Performance_address_information : System.Web.UI.Page
    {
        int AddressInfoPageIndex = 1;

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
                if (User.Identity.IsAuthenticated == false)
                {
                    Response.Redirect("~/Account/Login.aspx", false);
                }


                if (!this.IsPostBack)
                {
                    PageLoadEvents();
                }

                if (FV_AddressInfo.CurrentMode == FormViewMode.Insert)
                {
                    using (HiddenField stst = (HiddenField)FV_AddressInfo.FindControl("stst"))
                    using (DropDownList ddlAddState = (DropDownList)FV_AddressInfo.FindControl("ddlAddState"))
                    {
                        if (stst != null && ddlAddState != null)
                        {
                            stst.Value = ddlAddState.SelectedValue;
                        }
                    }
                }
                if (FV_AddressInfo.CurrentMode == FormViewMode.Edit)
                {
                    using (HiddenField HF_statept = (HiddenField)FV_AddressInfo.FindControl("HF_statept"))
                    using (DropDownList ddlAddState = (DropDownList)FV_AddressInfo.FindControl("ddlAddState"))
                    {
                        if (HF_statept != null && ddlAddState != null)
                        {
                            HF_statept.Value = ddlAddState.SelectedValue;
                        }
                    }
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
                MV_AddressInfo.SetActiveView(V_ViewAddressInfo);
                BindGV_AddressInfo(1);
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region Bind GVAddress Information
        private void BindGV_AddressInfo(int PageIndex)
        {
            try
            {
                personal_informationBO objPIAddBo = new personal_informationBO();
                personal_informationBl objPIAddBl = new personal_informationBl();
                objPIAddBo.EmpID = User.Identity.Name;
                objPIAddBo.flag = 1;
                objPIAddBo.Comany_Code = Session["CompCode"].ToString(); ;
                objPIAddBo.Approved_By = "";
                personal_informationCollBo objPIAddBoLst = objPIAddBl.Get_EmpAddressInfo(objPIAddBo);
                if (objPIAddBoLst.Count > 0)
                {
                    MsgCls(string.Empty, LblMsg, Color.White);
                    GV_AddressInfo.DataSource = objPIAddBoLst;
                    GV_AddressInfo.DataBind();
                    //AddressInfoviewPager(objPIAddBoLst.Count > 0 ? int.Parse(objPIAddBoLst[0].RecordCnt.ToString()) : 0, AddressInfoPageIndex);
                    MsgCls(string.Empty, LblMsg, Color.Transparent);
                }
                else
                {
                    MsgCls("No records found !", LblMsg, Color.Red);
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region GV_AddressInfo Events

        protected void GV_AddressInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                        int AID = int.Parse(GV_AddressInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());



                        piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                        piaddressinformationbl objPIAddBl = new piaddressinformationbl();
                        objPIAddBo.ID = AID;
                        objPIAddBo.EMPLOYEE_ID = User.Identity.Name;
                        piaddressinformationcollectionbo objPIAddBoLst = objPIAddBl.Get_Address_Details_Full(objPIAddBo);


                        if (objPIAddBoLst.Count > 0)
                        {
                            FV_AddressInfo.ChangeMode(FormViewMode.ReadOnly);
                            FV_AddressInfo.DataSource = objPIAddBoLst;
                            FV_AddressInfo.DataBind();
                            FV_AddressInfo.ChangeMode(FormViewMode.ReadOnly);
                            MV_AddressInfo.SetActiveView(V_AddEditAddressInfo);
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
                        using (Button BtnSubmit = (Button)FV_AddressInfo.FindControl("BtnSubmit"))
                        {
                            if (BtnSubmit != null)
                            {
                                BtnSubmit.Focus();
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

        protected void GV_AddressInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GV_AddressInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }



        #endregion

        #region Address Info Populate pager
        private void AddressInfoviewPager(int RecordCount, int currentPage)
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
                RptrAddressInfoPager.DataSource = pages;
                RptrAddressInfoPager.DataBind();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void RptrLeaveOverviewPagerPage_Changed(object sender, EventArgs e)
        {
            try
            {
                AddressInfoPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                BindGV_AddressInfo(AddressInfoPageIndex);
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region Add / View Button Event

        protected void LblAddAddressInfo_Click(object sender, EventArgs e)
        {
            try
            {
                MV_AddressInfo.SetActiveView(V_AddEditAddressInfo);
                FV_AddressInfo.ChangeMode(FormViewMode.Insert);
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                using (DropDownList DDL_Address = (DropDownList)FV_AddressInfo.FindControl("DDL_Address"))
                {
                    configurationcollectionbo objLst = configurationbl.Get_AddressTypes(1);
                    DDL_Address.DataSource = objLst;
                    DDL_Address.DataTextField = "DDLTYPETEXT";
                    DDL_Address.DataValueField = "DDLTYPE";
                    DDL_Address.DataBind();
                    DDL_Address.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

                    if (DDL_Address != null)
                    {
                        DDL_Address.Focus();
                    }
                }

                using (DropDownList DDL_ctry = (DropDownList)FV_AddressInfo.FindControl("ddlAddCountry"))
                {
                    configurationcollectionbo objLst = configurationbl.Get_Country(1);
                    DDL_ctry.DataSource = objLst;
                    DDL_ctry.DataTextField = "CountryTxt";
                    DDL_ctry.DataValueField = "Country";
                    DDL_ctry.DataBind();
                    DDL_ctry.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

                    if (DDL_ctry != null)
                    {
                        DDL_ctry.Focus();
                    }
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void LbtnBackAddressInfoView_Click(object sender, EventArgs e)
        {
            try
            {
                PageLoadEvents();
                Response.Redirect("~/UI/Personal_Information/address_information.aspx",false);
                //FV_AddressInfo.ChangeMode(FormViewMode.ReadOnly);
                FV_AddressInfo.DataSource = null;
                FV_AddressInfo.DataBind();
                MsgCls(string.Empty, LblMsg, Color.Transparent);
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region FV_AddressInfo Events

        protected void FV_AddressInfo_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            try
            {
                GetHRPernr();
                DateTime FrmDt = new DateTime(1900, 01, 01);
                DateTime ToDt = new DateTime(1900, 01, 01);
                switch (e.CommandName.ToUpper())
                {
                    case "ADDADDRESS":

                        using (DropDownList DDL_Address = (DropDownList)FV_AddressInfo.FindControl("DDL_Address"))
                        using (TextBox txtLocality = (TextBox)FV_AddressInfo.FindControl("txtLocality"))
                        using (TextBox txtAddline1 = (TextBox)FV_AddressInfo.FindControl("txtAddline1"))
                        using (TextBox txtAddline2 = (TextBox)FV_AddressInfo.FindControl("txtAddline2"))
                        using (DropDownList ddlAddCountry = (DropDownList)FV_AddressInfo.FindControl("ddlAddCountry"))
                        using (DropDownList ddlAddState = (DropDownList)FV_AddressInfo.FindControl("ddlAddState"))
                        using (TextBox txtbedda = (TextBox)FV_AddressInfo.FindControl("txtbedda"))
                        using (TextBox txtendda = (TextBox)FV_AddressInfo.FindControl("txtendda"))
                        using (TextBox txtAddDistrict = (TextBox)FV_AddressInfo.FindControl("txtAddDistrict"))
                        using (TextBox txtAddPincode = (TextBox)FV_AddressInfo.FindControl("txtAddPincode"))
                        using (TextBox txtAddStd = (TextBox)FV_AddressInfo.FindControl("txtAddStd"))
                        using (TextBox txtWardNum = (TextBox)FV_AddressInfo.FindControl("txtWardNum"))
                        using (HiddenField stst = (HiddenField)FV_AddressInfo.FindControl("stst"))
                        {
                            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                            piaddressinformationbl objPIAddBl = new piaddressinformationbl();

                            string HRMail = "";
                            string SuperVisorMail = "";
                            string PernrName = "";
                            string PernrEMail = "";
                           
                           
                            objPIAddBo.ID = 0;
                            objPIAddBo.EMPLOYEE_ID = User.Identity.Name;
                            objPIAddBo.ADDRESS_TYPE_ID = DDL_Address.SelectedValue;
                            objPIAddBo.DATE_FROM = DateTime.Parse(txtbedda.Text.ToString().Trim());
                            objPIAddBo.DATE_TO = DateTime.Parse(txtendda.Text.ToString().Trim());
                            objPIAddBo.ADDRESSL1 = txtAddline1.Text.Trim();
                            objPIAddBo.ADDRESSL2 = txtAddline2.Text.Trim();
                            objPIAddBo.GBLND = ddlAddCountry.SelectedValue;
                            objPIAddBo.STATE_ID = stst.Value;
                            objPIAddBo.CITY = txtAddDistrict.Text.Trim();
                            objPIAddBo.POSTAL_CODE = txtAddPincode.Text;
                            objPIAddBo.PHONENO = txtAddStd.Text.Trim();
                            objPIAddBo.CREATED_BY = txtWardNum.Text.Trim();    ////Ward num
                            objPIAddBo.CREATED_ON = DateTime.Now;
                            objPIAddBo.MODIFIED_BY = Session["CompCode"].ToString();
                            objPIAddBo.MODIFIEDON = DateTime.Now;
                            objPIAddBo.STATUS = txtLocality.Text.Trim(); //locality
                            objPIAddBo.Flag = 1;

                            objPIAddBl.Add_Update_Del_AddressDetails(objPIAddBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                            SendMail(objPIAddBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Saved");

                            LblMsg.Text = string.Empty;
                            if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                            {
                                MsgCls("Address Information added successfully !", LblMsg, Color.Green);                                
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Address Information added successfully !');parent.location.href='address_information.aspx'", true);
                            }
                            else
                            {
                                MsgCls("Address Information added successfully and sent for approval !", LblMsg, Color.Green);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Address Information added successfully and sent for approval !');parent.location.href='address_information.aspx'", true);
                                
                            }
                           
                        }
                        break;

                    case "EDITADDRESS":
                        FV_AddressInfo.ChangeMode(FormViewMode.Edit);
                        LoadAddressInfoFull(int.Parse(FV_AddressInfo.DataKey["ID"].ToString()));
                        using (TextBox TxtFromDate = (TextBox)FV_AddressInfo.FindControl("TxtEditFromDate"))
                        {
                            if (TxtFromDate != null)
                            {
                                TxtFromDate.Focus();
                            }
                        }

                        break;

                    case "CANCEL":
                        if (FV_AddressInfo.DataKey["ID"] != null)
                        { LoadAddressInfoFull(int.Parse(FV_AddressInfo.DataKey["ID"].ToString())); }
                        Response.Redirect("~/UI/Personal_Information/address_information.aspx",false);
                        break;
                    case "UPDATEADDRESS":
                        if (FV_AddressInfo.CurrentMode == FormViewMode.Edit)
                        {
                            using (HiddenField ADDRESS_TYPE_TEXT = (HiddenField)FV_AddressInfo.FindControl("ADDRESS_TYPE_TEXT"))
                            using (HiddenField HF_SUBTY = (HiddenField)FV_AddressInfo.FindControl("HF_SUBTY"))
                            using (HiddenField HF_ID = (HiddenField)FV_AddressInfo.FindControl("HF_ID"))
                            using (HiddenField HF_statept = (HiddenField)FV_AddressInfo.FindControl("HF_statept"))
                            //using (TextBox txtLocality = (TextBox)FV_AddressInfo.FindControl("txtLocality"))
                            using (TextBox txtAddline1 = (TextBox)FV_AddressInfo.FindControl("txtAddline1"))
                            using (TextBox txtAddline2 = (TextBox)FV_AddressInfo.FindControl("txtAddline2"))
                            using (DropDownList ddlAddCountry = (DropDownList)FV_AddressInfo.FindControl("ddlAddCountry"))
                            using (DropDownList ddlAddState = (DropDownList)FV_AddressInfo.FindControl("ddlAddState"))
                            using (TextBox txtaddstartdt = (TextBox)FV_AddressInfo.FindControl("txtaddstartdt"))
                            using (TextBox txtaddenddt = (TextBox)FV_AddressInfo.FindControl("txtaddenddt"))
                            using (TextBox txtAddDistrict = (TextBox)FV_AddressInfo.FindControl("txtAddDistrict"))
                            using (TextBox txtAddPincode = (TextBox)FV_AddressInfo.FindControl("txtAddPincode"))
                            using (TextBox txtAddStd = (TextBox)FV_AddressInfo.FindControl("txtAddStd"))
                            
                            //using (TextBox txtWardNum = (TextBox)FV_AddressInfo.FindControl("txtWardNum"))
                            {
                                piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                                piaddressinformationbl objPIAddBl = new piaddressinformationbl();
                                string HRMail = "";
                                string SuperVisorMail = "";
                                string PernrName = "";
                                string PernrEMail = "";
                                ddlAddCountry.SelectedItem.ToString();
                               

                                objPIAddBo.ID = Convert.ToInt32(HF_ID.Value);
                                objPIAddBo.EMPLOYEE_ID = User.Identity.Name;
                                objPIAddBo.ADDRESS_TYPE_ID = HF_SUBTY.Value;
                                objPIAddBo.DATE_FROM = DateTime.Parse(txtaddstartdt.Text.ToString().Trim());
                                objPIAddBo.DATE_TO = DateTime.Parse(txtaddenddt.Text.ToString().Trim()); 
                                objPIAddBo.ADDRESSL1 = txtAddline1.Text.Trim();
                                objPIAddBo.ADDRESSL2 = txtAddline2.Text.Trim();
                                objPIAddBo.GBLND = ddlAddCountry.SelectedValue;
                                objPIAddBo.STATE_ID = HF_statept.Value;
                                objPIAddBo.CITY = txtAddDistrict.Text.Trim();
                                objPIAddBo.POSTAL_CODE = txtAddPincode.Text;
                                objPIAddBo.PHONENO = txtAddStd.Text.Trim();
                                objPIAddBo.CREATED_BY = "";    ////Ward num
                                objPIAddBo.CREATED_ON = DateTime.Now;
                                objPIAddBo.MODIFIED_BY = Session["CompCode"].ToString();
                                objPIAddBo.MODIFIEDON = DateTime.Now;
                                objPIAddBo.STATUS = ""; //locality
                                objPIAddBo.Flag = 2;

                                objPIAddBl.Add_Update_Del_AddressDetails(objPIAddBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                                SendMail(objPIAddBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Updated");
                                
                               
                                LblMsg.Text = string.Empty;                               
                                if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                                {
                                    MsgCls("Address Information updated successfully !", LblMsg, Color.Green);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Address Information updated successfully !');parent.location.href='address_information.aspx'", true);
                                    
                                }
                                else
                                {
                                    MsgCls("Address Information updated successfully and sent for approval !", LblMsg, Color.Green);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Address Information updated successfully !');parent.location.href='address_information.aspx'", true);
                                    
                                }

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
                        MsgCls("Address type already exists !", LblMsg, Color.Red);
                        break;

                    case "-1":
                        MsgCls("Temporary Address type already exists for these dates !", LblMsg, Color.Red);
                        break;
                    case "-3":
                        MsgCls("Approver Missing", LblMsg, Color.Red);
                        break;
                    case "-4":
                        MsgCls("Permanent Address cannot be inserted", LblMsg, Color.Red);
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

        public void SendMail(piaddressinformationbo objPIAddBo, ref string HRMail, ref string SuperVisorMail, ref string PernrName, ref string PernrEMail, string type)
        {
            try
            {
                GetHRPernr();
                string strSubject1 = string.Empty;
                string strSubject = string.Empty;
                string ccode = Session["CompCode"].ToString();
                string empid = User.Identity.Name;
                int cnt = ccode.Length;
                empid = empid.Substring(cnt).ToUpper();

                if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                {

                    if (type == "Updated")
                    {
                        strSubject1 = "IEmpPower Paycompute - Notification !";
                        strSubject = "Address Information has been updated by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".This has been Self Approved, No Action Required.";
                    }
                    else if (type == "Saved")
                    {
                        strSubject1 = "IEmpPower Paycompute - Notification !";
                        strSubject = "Address Information has been created by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".This has been Self Approved, No Action Required.";
                    }
                }
                else
                {


                    if (type == "Updated")
                    {
                        strSubject1 = "IEmpPower Paycompute - Notification !";
                        strSubject = "Address Information has been updated by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".";
                    }
                    else if (type == "Saved")
                    {
                        strSubject1 = "IEmpPower Paycompute - Notification !";
                        strSubject = "Address Information has been created by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".";
                    }
                }

                string RecipientsString = HRMail;
                string strPernr_Mail = PernrEMail;
                //    //Preparing the mail body--------------------------------------------------

                string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                body = body + "<b style= 'font-size: 14px';>Address details : </b><hr>";
                body += "<b><table style=border-collapse:collapse;><tr><td style='font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Address Line1</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objPIAddBo.ADDRESSL1.ToString() + "</td></tr>";
                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Address Line2 </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objPIAddBo.ADDRESSL2.ToString() + "</td></tr>";
                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>City </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objPIAddBo.CITY.ToString() + "</td></tr>";
                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Postal Code</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objPIAddBo.POSTAL_CODE.ToString() + "</td></tr>";                
                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Ward Number </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objPIAddBo.PHONENO.ToString() + "</td></tr></table></b>";

                body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";

                // body += "</br>" + sw1.ToString() + "<br/>";
                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject1, strPernr_Mail, body);

            }

            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }


        protected void FV_AddressInfo_ModeChanging(object sender, FormViewModeEventArgs e)
        {
            try
            {
                FV_AddressInfo.ChangeMode(e.NewMode);
                if (FV_AddressInfo.DataKey["ID"] != null)
                { LoadAddressInfoFull(int.Parse(FV_AddressInfo.DataKey["ID"].ToString())); }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #endregion

        #region Load Address information full
        private void LoadAddressInfoFull(int ID)
        {
            try
            {
                int AddressID;
                if (int.TryParse(FV_AddressInfo.DataKey["ID"].ToString(), out AddressID))
                {
                    piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                    piaddressinformationbl objPIAddBl = new piaddressinformationbl();
                    objPIAddBo.ID = ID;
                    objPIAddBo.EMPLOYEE_ID = User.Identity.Name;
                    FV_AddressInfo.DataSource = null;
                    FV_AddressInfo.DataBind();
                    piaddressinformationcollectionbo objPIAddBoLst = objPIAddBl.Get_Address_Details_Full(objPIAddBo);
                    if (objPIAddBoLst.Count > 0)
                    {
                        FV_AddressInfo.DataSource = objPIAddBoLst;
                        FV_AddressInfo.DataBind();
                        if (FV_AddressInfo.CurrentMode == FormViewMode.Edit)
                        {
                            using (DropDownList ddlAddCountry = (DropDownList)FV_AddressInfo.FindControl("ddlAddCountry"))
                            using (DropDownList ddlAddState = (DropDownList)FV_AddressInfo.FindControl("ddlAddState"))
                            using (HiddenField HF_Cntry = (HiddenField)FV_AddressInfo.FindControl("HF_Cntry"))
                            using (HiddenField HF_State = (HiddenField)FV_AddressInfo.FindControl("HF_State"))
                            {

                                configurationcollectionbo objLst = configurationbl.Get_Country(1);
                                ddlAddCountry.DataSource = objLst;
                                ddlAddCountry.DataTextField = "CountryTxt";
                                ddlAddCountry.DataValueField = "Country";
                                ddlAddCountry.DataBind();
                                ddlAddCountry.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
                                ddlAddCountry.SelectedValue = HF_Cntry.Value;

                                configurationcollectionbo objLst1 = configurationbl.Get_states(ddlAddCountry.SelectedValue.ToString(), 1);
                                ddlAddState.DataSource = objLst1;
                                ddlAddState.DataTextField = "StateTxt";
                                ddlAddState.DataValueField = "State";
                                ddlAddState.DataBind();
                                ddlAddState.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
                                ddlAddState.SelectedValue = HF_State.Value;
                            }
                        }
                    }
                    else
                    { MsgCls("Invalid ID", LblMsg, Color.Red); }
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        //----------------- EMAIL SENDING -------------------------------------

        #region Get Mail Body
        private string GetMailBody(piaddressinformationbo objPIAddBo, string EmpName, string AddressStatus)
        {
            try
            {
                StringBuilder Sb = new StringBuilder();
                string Mailbody = string.Empty;
                string AddressInfoFilePath = Server.MapPath(@"." + "/EmailTemplates/EmpAddressInfoTemplate.html");

                Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Address Type</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + objPIAddBo.ADDRESS_TYPE_TEXT + "</td><td></td></tr>");
                Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Date</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + "</td><td></td></tr>");


                Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                Mailbody = Mailbody.Replace("##LEAVETYPE##", objPIAddBo.ADDRESS_TYPE_TEXT);
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

        protected void ddlAddCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (DropDownList DDL_ctry = (DropDownList)FV_AddressInfo.FindControl("ddlAddCountry"))
                {
                    using (DropDownList DDL_state = (DropDownList)FV_AddressInfo.FindControl("ddlAddState"))
                    {
                        configurationcollectionbo objLst = configurationbl.Get_states(DDL_ctry.SelectedValue.ToString(), 1);
                        DDL_state.DataSource = objLst;
                        DDL_state.DataTextField = "StateTxt";
                        DDL_state.DataValueField = "State";
                        DDL_state.DataBind();
                        DDL_state.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
                        //return objLst;
                    }
                }
            }
            catch (Exception ex) { }
        }



        //protected void FV_AddressInfo_DataBound(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string subtyp = FV_AddressInfo.DataKey["SUBTY"].ToString().Trim();
        //        //if (FV_AddressInfo.CurrentMode == FormViewMode.Edit)
        //        //{

        //            using (TextBox TxtFromDate = (TextBox)FV_AddressInfo.FindControl("TxtEditFromDate"))
        //            using (TextBox TxtToDate = (TextBox)FV_AddressInfo.FindControl("TxtEditToDate"))
        //            {
        //                if (TxtFromDate != null && TxtToDate != null)
        //                {

        //                    if (subtyp.Trim() == "1")
        //                    {
        //                        TxtFromDate.Enabled = false;
        //                        TxtToDate.Enabled = false;
        //                    }

        //                    else
        //                    {
        //                        TxtFromDate.Enabled = true;
        //                        TxtToDate.Enabled = true;
        //                    }

        //                }
        //            }
        //      //  }

        //    }
        //    catch (Exception Ex)
        //    { MsgCls(Ex.Message, LblMsg, Color.Red); }
        //}

    }
}
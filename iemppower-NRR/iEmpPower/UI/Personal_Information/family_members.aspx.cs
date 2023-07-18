using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Security;
using System.Drawing;
using System.Text;


public partial class UI_Personal_Information_family_members : System.Web.UI.Page
{
    int FamilyInfoPageIndex = 1;

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
            MV_FamilyInfo.SetActiveView(V_ViewFamilyInfo);
            BindGV_FamilyInfo(1);
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Bind GV_FamilyInfo Information
    private void BindGV_FamilyInfo(int PageIndex)
    {
        try
        {
            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
            pifamilymembersbl objFamilyBl = new pifamilymembersbl();
            objFamilyBo.PENNR = User.Identity.Name;
            objFamilyBo.PageIndex = PageIndex;
            objFamilyBo.PageSize = 5;
            pifamilymemberscollectionbo objPIAddBoLst = objFamilyBl.Get_FamilyMember_Details(objFamilyBo);
            if (objPIAddBoLst.Count > 0)
            {
                MsgCls(string.Empty, LblMsg, Color.White);
                GV_FamilyInfo.DataSource = objPIAddBoLst;
                GV_FamilyInfo.DataBind();
                FamilyInfoviewPager(objPIAddBoLst.Count > 0 ? int.Parse(objPIAddBoLst[0].RecordCnt.ToString()) : 0, FamilyInfoPageIndex);
                MsgCls(string.Empty, LblMsg, Color.Transparent);
            }
            else
            {
                DataTable Dt = new DataTable();
                Dt.Columns.Add("RowNumber", typeof(int));
                Dt.Columns.Add("ID", typeof(int));
                Dt.Columns.Add("FAMSA", typeof(string));
                Dt.Columns.Add("STEXT", typeof(string));
                Dt.Columns.Add("FASEX_Name", typeof(string));
                Dt.Columns.Add("FAVOR", typeof(string));
                Dt.Columns.Add("FANAM", typeof(string));
                Dt.Columns.Add("FGBDT", typeof(DateTime));
                Dt.Columns.Add("AGE", typeof(string));
                Dt.Columns.Add("RecordCnt", typeof(int));

                GVNodata(GV_FamilyInfo, Dt);
                FamilyInfoviewPager(0, 0);
                MsgCls("No records found !", LblMsg, Color.Red);
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region GV_FamilyInfo Events

    protected void GV_FamilyInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            switch (e.CommandName.ToUpper())
            {
                case "VIEW":

                    int FamilyID = int.Parse(GV_FamilyInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                    ViewState["FamilyTyp"] = GV_FamilyInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["STEXT"].ToString();
                    pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                    pifamilymembersbl objFamilyBl = new pifamilymembersbl();
                    objFamilyBo.ID = FamilyID;
                    objFamilyBo.PENNR = User.Identity.Name;
                    pifamilymemberscollectionbo objPIAddBoLst = objFamilyBl.Get_FamilyMember_Details_Full(objFamilyBo);
                    if (objPIAddBoLst.Count > 0)
                    {
                        FV_FamilyInfo.DataSource = objPIAddBoLst;
                        FV_FamilyInfo.DataBind();
                        FV_FamilyInfo.ChangeMode(FormViewMode.ReadOnly);
                        MV_FamilyInfo.SetActiveView(V_AddEditFamilyInfo);
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

                    break;
                //case "DELETE":

                //     int FamilyIDd= int.Parse(GV_FamilyInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                //        pifamilymembersbo objFamilyBod = new pifamilymembersbo();
                //        pifamilymembersbl objFamilyBld = new pifamilymembersbl();

                //        string HRMail = "";
                //        string SuperVisorMail = "";
                //        string PernrName = "";
                //        string PernrEMail = "";

                //        objFamilyBod.ID = FamilyIDd;

                //        objFamilyBod.PENNR = User.Identity.Name;
                //        objFamilyBod.FAMSA = ""; //-- Type of Family Record
                //        objFamilyBod.OBJPS = ""; //-- Object Identification
                //        objFamilyBod.FAVOR = ""; //-- First Name
                //        objFamilyBod.FANAM = ""; //-- Last Name
                //        objFamilyBod.FGBNA = string.Empty; //-- Initials
                //        objFamilyBod.FINIT = string.Empty; //
                //        objFamilyBod.FNMZU = string.Empty; //-- Other Title
                //        objFamilyBod.FVRSW = string.Empty; //-- Name Prefix
                //        objFamilyBod.FASEX = "1";//-- Gender Key
                //        objFamilyBod.FGBDT = DateTime.Now; //-- Date of Birth
                //        objFamilyBod.FGBOT = string.Empty; //-- Birthplace
                //        objFamilyBod.FGBLD = string.Empty; //-- Country of Birth
                //        objFamilyBod.FANAT = string.Empty; //-- Nationality
                //        objFamilyBod.FANA2 = string.Empty; //-- Second Nationality
                //        objFamilyBod.FANA3 = string.Empty; //-- Third Nationality
                //        objFamilyBod.KDBSL = ""; //-- Allowance Authorization
                //        objFamilyBod.KDBGR = ""; //-- Child Allowance Entitlement
                //        objFamilyBod.KDZUL = ""; //-- Child Allowances
                //        objFamilyBod.BEGDA = new DateTime(DateTime.Now.Year, 01, 01);
                //        objFamilyBod.ENDDA = new DateTime(1900, 01, 01);
                //        objFamilyBod.CREATED_BY = User.Identity.Name;
                //        objFamilyBod.CREATEDON = DateTime.Now;
                //        objFamilyBod.MODIFIED_BY = User.Identity.Name;
                //        objFamilyBod.MODIFIEDON = DateTime.Now;
                //        objFamilyBod.STATUS = "DELETE";
                //        objFamilyBod.Flag = 3;

                //        objFamilyBld.Add_Update_Del_FamilyDetails(objFamilyBod, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                //        PageLoadEvents();
                //        LblMsg.Text = string.Empty;
                //        MsgCls("Address Information Deleted successfully !", LblMsg, Color.Green);
                //        string[] MsgCC = { HRMail, SuperVisorMail };
                //        iEmpPowerMaster_Load.masterbl.SendMail(PernrEMail, MsgCC, "Address Info Deleted - " + PernrName + " (" + User.Identity.Name + ")"
                //            , GetMailBody(objFamilyBod, PernrName, "Deleted"));


                //    break;
                default:
                    break;
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    protected void GV_FamilyInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void GV_FamilyInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GV_FamilyInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    #endregion

    #region Family Info Populate pager
    private void FamilyInfoviewPager(int RecordCount, int currentPage)
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
            RptrFamilyInfoPager.DataSource = pages;
            RptrFamilyInfoPager.DataBind();
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    protected void RptrLeaveOverviewPagerPage_Changed(object sender, EventArgs e)
    {
        try
        {
            FamilyInfoPageIndex = int.Parse((sender as LinkButton).CommandArgument);
            BindGV_FamilyInfo(FamilyInfoPageIndex);
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Add / View Button Event

    protected void LblAddFamilyInfo_Click(object sender, EventArgs e)
    {
        try
        {
            MV_FamilyInfo.SetActiveView(V_AddEditFamilyInfo);
            FV_FamilyInfo.ChangeMode(FormViewMode.Insert);
            MsgCls(string.Empty, LblMsg, Color.Transparent);
            using (DropDownList DDL_FamilyTypes = (DropDownList)FV_FamilyInfo.FindControl("DDL_FamilyTypes"))
            {
                if (DDL_FamilyTypes != null)
                {
                    DDL_FamilyTypes.Focus();
                }
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    protected void LbtnBackFamilyInfoView_Click(object sender, EventArgs e)
    {
        try
        {
            PageLoadEvents();
            FV_FamilyInfo.ChangeMode(FormViewMode.ReadOnly);
            MsgCls(string.Empty, LblMsg, Color.Transparent);
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region FV_FamilyInfo Events

    protected void FV_FamilyInfo_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        try
        {
            GetHRPernr();
            DateTime DtDOB = new DateTime(1900, 01, 01);
            switch (e.CommandName.ToUpper())
            {
                case "ADDFAMILY":

                    using (DropDownList DDL_FamilyTypes = (DropDownList)FV_FamilyInfo.FindControl("DDL_FamilyTypes"))
                    using (TextBox TxtFirstName = (TextBox)FV_FamilyInfo.FindControl("TxtFirstName"))
                    using (TextBox TxtLastName = (TextBox)FV_FamilyInfo.FindControl("TxtLastName"))
                    using (TextBox TxtDOB = (TextBox)FV_FamilyInfo.FindControl("TxtDOB"))
                    using (RadioButtonList RbtnGender = (RadioButtonList)FV_FamilyInfo.FindControl("RbtnGender"))
                    using (CheckBox ChkOtherAllowances = (CheckBox)FV_FamilyInfo.FindControl("ChkOtherAllowances"))
                    using (CheckBox ChkChildHostelAllowances = (CheckBox)FV_FamilyInfo.FindControl("ChkChildHostelAllowances"))
                    using (CheckBox ChkChildEducationalAllowances = (CheckBox)FV_FamilyInfo.FindControl("ChkChildEducationalAllowances"))
                    {
                        if (string.IsNullOrEmpty(TxtDOB.Text.Trim()))
                        {
                            TxtDOB.Text = new DateTime(1900, 01, 01).ToString();
                        }

                        if (!string.IsNullOrEmpty(DDL_FamilyTypes.SelectedValue))
                        {
                            if (!string.IsNullOrEmpty(TxtFirstName.Text.Trim()))
                            {
                                if (!string.IsNullOrEmpty(TxtLastName.Text.Trim()))
                                {
                                    if (!string.IsNullOrEmpty(TxtDOB.Text.Trim()) && DateTime.TryParse(TxtDOB.Text, out DtDOB))
                                    {
                                        if (RbtnGender.SelectedValue != "0")
                                        {
                                            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                            pifamilymembersbl objFamilyBl = new pifamilymembersbl();

                                            string HRMail = "";
                                            string SuperVisorMail = "";
                                            string PernrName = "";
                                            string PernrEMail = "";

                                            objFamilyBo.ID = 0;
                                            objFamilyBo.PENNR = User.Identity.Name;
                                            objFamilyBo.FAMSA = DDL_FamilyTypes.SelectedValue; //-- Type of Family Record
                                            ViewState["FamilyTyp"] = DDL_FamilyTypes.SelectedItem.Text.ToString();
                                            objFamilyBo.OBJPS = ""; //-- Object Identification
                                            objFamilyBo.FAVOR = TxtFirstName.Text.Trim(); //-- First Name
                                            objFamilyBo.FANAM = TxtLastName.Text.Trim(); //-- Last Name
                                            objFamilyBo.FGBNA = string.Empty; //-- Initials
                                            objFamilyBo.FINIT = string.Empty; //
                                            objFamilyBo.FNMZU = string.Empty; //-- Other Title
                                            objFamilyBo.FVRSW = string.Empty; //-- Name Prefix
                                            objFamilyBo.FASEX = RbtnGender.SelectedValue; //-- Gender Key
                                            objFamilyBo.FASEX_Name = RbtnGender.SelectedItem.Text.ToString();
                                            objFamilyBo.FGBDT = DtDOB; //-- Date of Birth
                                            objFamilyBo.FGBOT = string.Empty; //-- Birthplace
                                            objFamilyBo.FGBLD = string.Empty; //-- Country of Birth
                                            objFamilyBo.FANAT = string.Empty; //-- Nationality
                                            objFamilyBo.FANA2 = string.Empty; //-- Second Nationality
                                            objFamilyBo.FANA3 = string.Empty; //-- Third Nationality
                                            objFamilyBo.KDBSL = ChkOtherAllowances.Checked ? "Y" : "N"; //-- Allowance Authorization
                                            objFamilyBo.KDBGR = ChkChildHostelAllowances.Checked ? "Y" : "N"; //-- Child Allowance Entitlement
                                            objFamilyBo.KDZUL = ChkChildEducationalAllowances.Checked ? "Y" : "N"; //-- Child Allowances
                                            objFamilyBo.BEGDA = DateTime.Now;
                                            objFamilyBo.ENDDA = new DateTime(9999, 12, 31);
                                            objFamilyBo.CREATED_BY = User.Identity.Name;
                                            objFamilyBo.CREATEDON = DateTime.Now;
                                            objFamilyBo.MODIFIED_BY = "";
                                            objFamilyBo.MODIFIEDON = DateTime.Now;
                                            objFamilyBo.STATUS = "NEW";
                                            objFamilyBo.Flag = 1;

                                            objFamilyBl.Add_Update_Del_FamilyDetails(objFamilyBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                                            SendMail(objFamilyBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Saved");

                                            FV_FamilyInfo.ChangeMode(FormViewMode.ReadOnly);
                                            PageLoadEvents();
                                            LblMsg.Text = string.Empty;
                                            //MsgCls("Family member Information added successfully and sent for approval!", LblMsg, Color.Green);
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family member Information added successfully and sent for approval!')", true);

                                            if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                                            {
                                                MsgCls("Family member Information added successfully !", LblMsg, Color.Green);
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family member Information added successfully !')", true);
                                            }
                                            else
                                            {
                                                MsgCls("Family member Information added successfully and sent for approval !", LblMsg, Color.Green);
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family member Information added successfully and sent for approval !')", true);
                                            }


                                            // string[] MsgCC = { HRMail, SuperVisorMail };
                                            // iEmpPowerMaster_Load.masterbl.SendMail(PernrEMail, MsgCC, "Family member Info Added - " + PernrName + " (" + User.Identity.Name + ")", GetMailBody(objFamilyBo, PernrName, "Added"));


                                        }
                                        else
                                        { MsgCls("Invalid Gender !", LblMsg, Color.Red); }
                                    }
                                    else
                                    { MsgCls("Invalid Date of birth !", LblMsg, Color.Red); }
                                }
                                else
                                { MsgCls("Invalid Last Name !", LblMsg, Color.Red); }
                            }
                            else
                            { MsgCls("Invalid First Name !", LblMsg, Color.Red); }
                        }
                        else
                        { MsgCls("Invalid Family member Type !", LblMsg, Color.Red); }
                    }
                    break;

                case "EDITFAMILY":
                    FV_FamilyInfo.ChangeMode(FormViewMode.Edit);
                    LoadFamilyInfoFull(int.Parse(FV_FamilyInfo.DataKey["ID"].ToString()));
                    using (TextBox TxtFirstName = (TextBox)FV_FamilyInfo.FindControl("TxtEditFirstName"))
                    {
                        if (TxtFirstName != null)
                        {
                            TxtFirstName.Focus();

                        }
                    }

                    break;

                case "CANCEL":
                    if (FV_FamilyInfo.DataKey["ID"] != null)
                    { LoadFamilyInfoFull(int.Parse(FV_FamilyInfo.DataKey["ID"].ToString())); }
                    FV_FamilyInfo.ChangeMode(FormViewMode.ReadOnly);
                    PageLoadEvents();
                    break;
                case "UPDATEFAMILY":
                    if (FV_FamilyInfo.CurrentMode == FormViewMode.Edit)
                    {
                        using (TextBox TxtFirstName = (TextBox)FV_FamilyInfo.FindControl("TxtEditFirstName"))
                        using (TextBox TxtLastName = (TextBox)FV_FamilyInfo.FindControl("TxtEditLastName"))
                        using (TextBox TxtDOB = (TextBox)FV_FamilyInfo.FindControl("TxtEditDOB"))
                        using (RadioButtonList RbtnGender = (RadioButtonList)FV_FamilyInfo.FindControl("RbtnEditGender"))
                        using (CheckBox ChkOtherAllowances = (CheckBox)FV_FamilyInfo.FindControl("ChkEditOtherAllowances"))
                        using (CheckBox ChkChildHostelAllowances = (CheckBox)FV_FamilyInfo.FindControl("ChkEditChildHostelAllowances"))
                        using (CheckBox ChkChildEducationalAllowances = (CheckBox)FV_FamilyInfo.FindControl("ChkEditChildEducationalAllowances"))
                        {

                            if (!string.IsNullOrEmpty(TxtFirstName.Text.Trim()))
                            {
                                if (!string.IsNullOrEmpty(TxtLastName.Text.Trim()))
                                {
                                    if (!string.IsNullOrEmpty(TxtDOB.Text.Trim()) && DateTime.TryParse(TxtDOB.Text, out DtDOB))
                                    {
                                        if (RbtnGender.SelectedValue != "0")
                                        {
                                            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                            pifamilymembersbl objFamilyBl = new pifamilymembersbl();

                                            string HRMail = "";
                                            string SuperVisorMail = "";
                                            string PernrName = "";
                                            string PernrEMail = "";

                                            objFamilyBo.ID = int.Parse(FV_FamilyInfo.DataKey["ID"].ToString());
                                            objFamilyBo.PENNR = User.Identity.Name;
                                            objFamilyBo.FAMSA = ""; //-- Type of Family Record
                                            objFamilyBo.OBJPS = ""; //-- Object Identification
                                            objFamilyBo.FAVOR = TxtFirstName.Text.Trim(); //-- First Name
                                            objFamilyBo.FANAM = TxtLastName.Text.Trim(); //-- Last Name
                                            objFamilyBo.FGBNA = string.Empty; //-- Initials
                                            objFamilyBo.FINIT = string.Empty; //
                                            objFamilyBo.FNMZU = string.Empty; //-- Other Title
                                            objFamilyBo.FVRSW = string.Empty; //-- Name Prefix
                                            objFamilyBo.FASEX = RbtnGender.SelectedValue; //-- Gender Key
                                            objFamilyBo.FASEX_Name = RbtnGender.SelectedItem.Text.ToString();
                                            objFamilyBo.FGBDT = DtDOB; //-- Date of Birth
                                            objFamilyBo.FGBOT = string.Empty; //-- Birthplace
                                            objFamilyBo.FGBLD = string.Empty; //-- Country of Birth
                                            objFamilyBo.FANAT = string.Empty; //-- Nationality
                                            objFamilyBo.FANA2 = string.Empty; //-- Second Nationality
                                            objFamilyBo.FANA3 = string.Empty; //-- Third Nationality
                                            objFamilyBo.KDBSL = ChkOtherAllowances.Checked ? "Y" : "N"; //-- Allowance Authorization
                                            objFamilyBo.KDBGR = ChkChildHostelAllowances.Checked ? "Y" : "N"; //-- Child Allowance Entitlement
                                            objFamilyBo.KDZUL = ChkChildEducationalAllowances.Checked ? "Y" : "N"; //-- Child Allowances
                                            objFamilyBo.BEGDA = DateTime.Now;
                                            objFamilyBo.ENDDA = new DateTime(9999, 12, 31);
                                            objFamilyBo.CREATED_BY = User.Identity.Name;
                                            objFamilyBo.CREATEDON = DateTime.Now;
                                            objFamilyBo.MODIFIED_BY = User.Identity.Name;
                                            objFamilyBo.MODIFIEDON = DateTime.Now;
                                            objFamilyBo.STATUS = "UPDATE";
                                            objFamilyBo.Flag = 2;

                                            objFamilyBl.Add_Update_Del_FamilyDetails(objFamilyBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                                            SendMail(objFamilyBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Updated");

                                            FV_FamilyInfo.ChangeMode(FormViewMode.ReadOnly);
                                            LoadFamilyInfoFull(int.Parse(FV_FamilyInfo.DataKey["ID"].ToString()));
                                            //MsgCls("Family member Information updated successfully and sent for approval !", LblMsg, Color.Green);
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family member Information updated successfully and sent for approval !')", true);

                                            if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                                            {
                                                MsgCls("Family member Information updated successfully !", LblMsg, Color.Green);
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family member Information updated successfully !')", true);
                                            }
                                            else
                                            {
                                                MsgCls("Family member Information updated successfully and sent for approval !", LblMsg, Color.Green);
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family member Information updated successfully and sent for approval !')", true);
                                            }

                                            //string[] MsgCC = { HRMail, SuperVisorMail };
                                            // iEmpPowerMaster_Load.masterbl.SendMail(PernrEMail, MsgCC, "Family member Info Updated - " + PernrName + " (" + User.Identity.Name + ")", GetMailBody(objFamilyBo, PernrName, "Updated"));


                                        }
                                        else
                                        { MsgCls("Invalid Gender !", LblMsg, Color.Red); }
                                    }
                                    else
                                    { MsgCls("Invalid Date of birth !", LblMsg, Color.Red); }
                                }
                                else
                                { MsgCls("Invalid Last Name !", LblMsg, Color.Red); }
                            }
                            else
                            { MsgCls("Invalid First Name !", LblMsg, Color.Red); }

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
                    MsgCls("Family member type already exists !", LblMsg, Color.Red);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('HR Admin information is not maintained in SAP!');", true);
        }
    }

    public void SendMail(pifamilymembersbo objFamilyBo, ref string HRMail, ref string SuperVisorMail, ref string PernrName, ref string PernrEMail, string type)
    {
        try
        {
            GetHRPernr();
            string strSubject = string.Empty;
            if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
            {

                if (type == "Updated")
                {
                    strSubject = "Family Information has been updated by " + PernrName + "  | " + User.Identity.Name + ".This has been Self Approved, No Action Required.";
                }
                else if (type == "Saved")
                {
                    strSubject = "Family Information has been created by " + PernrName + "  | " + User.Identity.Name + ".This has been Self Approved, No Action Required.";
                }
            }

            else
            {


                if (type == "Updated")
                {
                    strSubject = "Family Information has been updated by " + PernrName + "  | " + User.Identity.Name + ".";
                }
                else if (type == "Saved")
                {
                    strSubject = "Family Information has been created by " + PernrName + "  | " + User.Identity.Name + ".";
                }

            }
            string RecipientsString = HRMail;
            string strPernr_Mail = PernrEMail;

            //    //Preparing the mail body--------------------------------------------------

            if (ViewState["FamilyTyp"].ToString().Trim() == "Child")
            {
                objFamilyBo.KDBSL = objFamilyBo.KDBSL.ToString().Trim() == "Y" ? "Yes" : "No"; //-- Allowance Authorization
                objFamilyBo.KDBGR = objFamilyBo.KDBGR.ToString().Trim() == "Y" ? "Yes" : "No";  //-- Child Allowance Entitlement
                objFamilyBo.KDZUL = objFamilyBo.KDZUL.ToString().Trim() == "Y" ? "Yes" : "No";  //-- Child Allowances
            }

            else
            {
                objFamilyBo.KDBSL = "N/A"; //-- Allowance Authorization
                objFamilyBo.KDBGR = "N/A";  //-- Child Allowance Entitlement
                objFamilyBo.KDZUL = "N/A";  //-- Child Allowances
            }

            string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
            body = body + "<b style= 'font-size: 14px';>Family details : </b><hr>";
            body += "<b><table style=border-collapse:collapse;><tr><td style='font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'> Family Type</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ViewState["FamilyTyp"].ToString() + "</td></tr>";
            body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>First Name </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objFamilyBo.FAVOR.ToString() + "</td></tr>";
            body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Last Name </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objFamilyBo.FANAM.ToString() + "</td></tr>";
            body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Date of Birth </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(objFamilyBo.FGBDT.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
            body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Gender </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objFamilyBo.FASEX_Name.ToString() + "</td></tr>";
            body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Other allowances </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objFamilyBo.KDBSL.ToString() + "</td></tr>";
            body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Child Hostel allowances </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objFamilyBo.KDBGR.ToString() + "</td></tr>";
            body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Child Educational allowances</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objFamilyBo.KDZUL.ToString() + "</td></tr></table></b>";
            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";

            // body += "</br>" + sw1.ToString() + "<br/>";
            //    //End of preparing the mail body-------------------------------------------
            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

        }



        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }


    protected void FV_FamilyInfo_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        try
        {
            FV_FamilyInfo.ChangeMode(e.NewMode);
            if (FV_FamilyInfo.DataKey["ID"] != null)
            { LoadFamilyInfoFull(int.Parse(FV_FamilyInfo.DataKey["ID"].ToString())); }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    #endregion

    #region Load Family information full
    private void LoadFamilyInfoFull(int ID)
    {
        try
        {

            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
            pifamilymembersbl objFamilyBl = new pifamilymembersbl();
            objFamilyBo.ID = ID;
            objFamilyBo.PENNR = User.Identity.Name;
            FV_FamilyInfo.DataSource = null;
            FV_FamilyInfo.DataBind();
            pifamilymemberscollectionbo objPIAddBoLst = objFamilyBl.Get_FamilyMember_Details_Full(objFamilyBo);
            if (objPIAddBoLst.Count > 0)
            {
                FV_FamilyInfo.DataSource = objPIAddBoLst;
                FV_FamilyInfo.DataBind();
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
    private string GetMailBody(pifamilymembersbo objFamilyBo, string EmpName, string AddressStatus)
    {
        try
        {
            StringBuilder Sb = new StringBuilder();
            string Mailbody = string.Empty;
            string AddressInfoFilePath = Server.MapPath(@"." + "/EmailTemplates/EmpAddressInfoTemplate.html");

            Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Address Type</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + objFamilyBo.FAMSA + "</td><td></td></tr>");
            Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Date</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + "</td><td></td></tr>");


            Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
            Mailbody = Mailbody.Replace("##LEAVETYPE##", objFamilyBo.FAMSA);
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

    protected void DDL_FamilyTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            using (Panel pnlChild = (Panel)FV_FamilyInfo.FindControl("pnlChild"))
            using (DropDownList DDL_FamilyTypes = (DropDownList)FV_FamilyInfo.FindControl("DDL_FamilyTypes"))
            {
                if (DDL_FamilyTypes.SelectedValue.Trim() == "2")
                {
                    pnlChild.Visible = true;
                }
                else
                {
                    pnlChild.Visible = false;
                }
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

}
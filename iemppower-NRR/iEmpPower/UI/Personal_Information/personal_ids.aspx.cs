using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPowerMaster_Load;
using System.Net;
using System.IO;
using System.Web.Security;
using System.Drawing;
using System.Data;
using System.Text;

public partial class UI_Personal_Information_personal_ids : System.Web.UI.Page
{
    int PersonalIDPageIndex = 1;
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
                bindesipf_details();
                gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
               
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
            D_ViewPersonalIdInfo.Visible = true;
            D_AddEditPersonalIdInfo.Visible = false;
            BindGV_PersonalIdInfo(1);
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Bind GV_PersonalIdInfo Information
    private void BindGV_PersonalIdInfo(int PageIndex)
    {
        try
        {
            personal_informationBl objPersonalIDsBl = new personal_informationBl();
            personal_informationBO objPersonalIDsBo = new personal_informationBO();
            objPersonalIDsBo.EmpID = User.Identity.Name;
            objPersonalIDsBo.Comany_Code = Session["CompCode"].ToString(); ;
            objPersonalIDsBo.flag = 1;
            objPersonalIDsBo.Approved_By = "";
            personal_informationCollBo objPIAddBoLst = objPersonalIDsBl.Get_EmpDocsInfo(objPersonalIDsBo);
            if (objPIAddBoLst.Count > 0)
            {
                MsgCls(string.Empty, LblMsg, Color.White);
                GV_PersonalIdInfo.DataSource = objPIAddBoLst;
                GV_PersonalIdInfo.DataBind();
                exitdocmod();
                //FamilyInfoviewPager(objPIAddBoLst.Count > 0 ? int.Parse(objPIAddBoLst[0].RecordCnt.ToString()) : 0, PersonalIDPageIndex);
                MsgCls(string.Empty, LblMsg, Color.Transparent);
            }
            else
            {
                DataTable Dt = new DataTable();
                Dt.Columns.Add("RowNumber", typeof(int));
                Dt.Columns.Add("ID", typeof(int));
                Dt.Columns.Add("ICTYPE", typeof(string));
                Dt.Columns.Add("ID_TYPE_TEXT", typeof(string));
                Dt.Columns.Add("ICNUM", typeof(string));
                Dt.Columns.Add("RecordCnt", typeof(int));

                GVNodata(GV_PersonalIdInfo, Dt);
                FamilyInfoviewPager(0, 0);
                MsgCls("No records found !", LblMsg, Color.Red);
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion




    public void bindesipf_details()
    {
        try
        {
           
            pipersonalidscollectionbo objPIAddBoLst = new pipersonalidscollectionbo();
            pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
            pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
           // objPersonalIDsBo.ID = 0;
            objPersonalIDsBo.AGENT_NAME = User.Identity.Name;
            objPersonalIDsBo.COMMENTS=Session["CompCode"].ToString();
            objPersonalIDsBo.Flag=1;
            objPIAddBoLst = objPersonalIDsBl.Get_ESIPF_details(objPersonalIDsBo);

            GV_ESIpf_benifits.DataSource = objPIAddBoLst;
            GV_ESIpf_benifits.DataBind();

            //gvidesi.Visible = GV_ESIpf_benifits.Rows.Count > 0 ? true : false;
            GV_ESIpf_benifits.Visible = GV_ESIpf_benifits.Rows.Count > 0 ? true : false;
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }

    }
    #region GV_PersonalIdInfo Events

    protected void GV_PersonalIdInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int PersonalID;
            switch (e.CommandName.ToUpper())
            {
                case "DOCDOWN":
                    int docget = Convert.ToInt32(e.CommandArgument);

                    GridViewRow gvdocdownload = GV_PersonalIdInfo.Rows[docget];

                    string docfile = GV_PersonalIdInfo.DataKeys[gvdocdownload.RowIndex].Values["docpath"].ToString();

                    string ImgExtn = Path.GetExtension(docfile.ToString().ToUpper());

                    string dofiledown = User.Identity.Name.ToString().ToLower();

                    string doctypfiledown = GV_PersonalIdInfo.DataKeys[gvdocdownload.RowIndex].Values["Doc_Type_TXT"].ToString();

                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + doctypfiledown.ToString().Trim() + '/' + dofiledown.ToString().Trim() + '/' + System.DateTime.Now + ImgExtn.ToString().Trim());
                    Response.TransmitFile(docfile);
                    Response.End();
                    break;



                case "EDITID":

                    PersonalID = int.Parse(GV_PersonalIdInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                    ViewState["ictypp"] = GV_PersonalIdInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["Doc_Type_ID"].ToString();
                    ViewState["ictxt"] = GV_PersonalIdInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["Doc_Type_TXT"].ToString();

                    ViewState["rowid"] = PersonalID.ToString().Trim();
                    pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
                    pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                    objPersonalIDsBo.ID = PersonalID;
                    objPersonalIDsBo.PERNR = User.Identity.Name;
                    pipersonalidscollectionbo objPIAddBoLst = objPersonalIDsBl.Get_PersonalIDSDetails_Full(objPersonalIDsBo);
                    if (objPIAddBoLst.Count > 0)
                    {
                        MsgCls(string.Empty, LblMsg, Color.Transparent);
                        D_ViewPersonalIdInfo.Visible = false;
                        D_AddEditPersonalIdInfo.Visible = true;
                        dvcreatePI.Visible = false;
                        dvupdatePI.Visible = true;
                        MsgCls(string.Empty, LblMsg, Color.Transparent);

                        dvupdatePI.Visible = true;
                        dvcreatePI.Visible = false;
                        lblidtext.Text = ViewState["ictxt"].ToString().Trim();
                        lblidtextin.Text = ViewState["ictxt"].ToString().Trim();
                        lbl_typtext.Text = ViewState["ictxt"].ToString().Trim();
                        TxtEditIdNumber.Text = ViewState["ictypp"].ToString().Trim();

                        if (lblidtext.Text == "Employee Photo")
                        {
                            TxtEditIdNumber.Enabled = false;
                            RFV_TxtEditIdNumber.Enabled = false;
                        }
                        else
                        {
                            TxtEditIdNumber.Enabled = true;
                            RFV_TxtEditIdNumber.Enabled = true;
                        }

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

                   
                        if (TxtEditIdNumber != null)
                        {
                            TxtEditIdNumber.Focus();

                        }
                   
                    break;
               
                default:
                    break;
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    protected void GV_PersonalIdInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void GV_PersonalIdInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GV_PersonalIdInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
            RptrPersonalIdInfoPager.DataSource = pages;
            RptrPersonalIdInfoPager.DataBind();
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    protected void RptrLeaveOverviewPagerPage_Changed(object sender, EventArgs e)
    {
        try
        {
            PersonalIDPageIndex = int.Parse((sender as LinkButton).CommandArgument);
            BindGV_PersonalIdInfo(PersonalIDPageIndex);
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Add / View Button Event

    protected void LblAddPersonalIdInfo_Click(object sender, EventArgs e)
    {
        try
        {
            D_ViewPersonalIdInfo.Visible = false;
            D_AddEditPersonalIdInfo.Visible = true;
            dvupdatePI.Visible = false;
            dvcreatePI.Visible = true;
            MsgCls(string.Empty, LblMsg, Color.Transparent);
                configurationcollectionbo objLst4 = configurationbl.Get_DocumtTypes(1);
                DDL_PersonalIdTypes.DataSource = objLst4;
                DDL_PersonalIdTypes.DataTextField = "DDLTYPETEXT";
                DDL_PersonalIdTypes.DataValueField = "DDLTYPE";
                DDL_PersonalIdTypes.DataBind();
                DDL_PersonalIdTypes.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

                if (DDL_PersonalIdTypes != null)
                {
                    DDL_PersonalIdTypes.Focus();
                }

        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    protected void LbtnBackPersonalIdInfoView_Click(object sender, EventArgs e)
    {
        try
        {
            PageLoadEvents();
             Response.Redirect("~/UI/Personal_Information/personal_ids.aspx",false);
            //FV_PersonalIdInfo.ChangeMode(FormViewMode.ReadOnly);
            MsgCls(string.Empty, LblMsg, Color.Transparent);
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region FV_PersonalIdInfo Events

   

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

    public void SendMail(pipersonalidsbo objPersonalIDsBo, ref string HRMail, ref string SuperVisorMail, ref string PernrName, ref string PernrEMail, string type)
    {
        try
        {
            GetHRPernr();
            string strSubject = string.Empty;
            string strSubject1 = string.Empty;
            strSubject1 = "IEmpPower Paycompute - Notification";
            string ccode = Session["CompCode"].ToString();
            string empid = User.Identity.Name;
            int cnt = ccode.Length;
            empid = empid.Substring(cnt).ToUpper();
            if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
            {

                if (type == "Updated")
                {
                    strSubject = "Personal ID's Information has been updated by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".This has been Self Approved, No Action Required.";
                }
                else if (type == "Saved")
                {
                    strSubject = "Personal ID's Information has been created by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".This has been Self Approved, No Action Required.";
                }
            }

            else
            {


                if (type == "Updated")
                {
                    strSubject = "Personal ID's Information has been updated by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".";
                }
                else if (type == "Saved")
                {
                    strSubject = "Personal ID's Information has been created by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".";
                }

            }

            string RecipientsString = HRMail;
            string strPernr_Mail = PernrEMail;

            //    //Preparing the mail body--------------------------------------------------

            string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
            body = body + "<b style= 'font-size: 14px';>Personal ID's details : </b><hr>";
            body += "<b><table style=border-collapse:collapse;><tr><td style='font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Personal ID Type </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ViewState["ictxt"].ToString() + "</td></tr>";
            body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Personal ID Number </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + objPersonalIDsBo.ICNUM.ToString() + "</td></tr></table></b>";
            body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";

            // body += "</br>" + sw1.ToString() + "<br/>";
            //    //End of preparing the mail body-------------------------------------------
            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject1, strPernr_Mail, body);
        }



        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }






    public void exitdocmod()
    {
        try
        {
            foreach (GridViewRow rowdoci in GV_PersonalIdInfo.Rows)
            {

                LinkButton extdocdown = (LinkButton)rowdoci.FindControl("LK_docdownload");

                string generatests = GV_PersonalIdInfo.DataKeys[rowdoci.RowIndex].Values[3].ToString();

                if (generatests.ToString().Trim() == "" || generatests.ToString().Trim() == null)
                {
                    extdocdown.Visible = false;
                }
                else
                {
                    extdocdown.Visible = true;
                }

            }
        }
        catch (Exception ex) { }
    }


    //protected void FV_PersonalIdInfo_ModeChanging(object sender, FormViewModeEventArgs e)
    //{
    //    try
    //    {
    //        FV_PersonalIdInfo.ChangeMode(e.NewMode);
    //        if (FV_PersonalIdInfo.DataKey["ID"] != null)
    //        { LoadPersonalIDInfoFull(int.Parse(FV_PersonalIdInfo.DataKey["ID"].ToString())); }
    //    }
    //    catch (Exception Ex)
    //    { MsgCls(Ex.Message, LblMsg, Color.Red); }
    //}

    #endregion

   
    //private void LoadPersonalIDInfoFull(int ID)
    //{
    //    try
    //    {
    //        if (int.TryParse(FV_PersonalIdInfo.DataKey["ID"].ToString(), out ID))
    //        {
    //            pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
    //            pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
    //            objPersonalIDsBo.ID = ID;
    //            objPersonalIDsBo.PERNR = User.Identity.Name;
    //            FV_PersonalIdInfo.DataSource = null;
    //            FV_PersonalIdInfo.DataBind();
    //            pipersonalidscollectionbo objPIAddBoLst = objPersonalIDsBl.Get_PersonalIDSDetails_Full(objPersonalIDsBo);
    //            if (objPIAddBoLst.Count > 0)
    //            {
    //                FV_PersonalIdInfo.DataSource = objPIAddBoLst;
    //                FV_PersonalIdInfo.DataBind();
    //            }
    //            else
    //            { MsgCls("Invalid ID", LblMsg, Color.Red); }
    //        }
    //    }
    //    catch (Exception Ex)
    //    { MsgCls(Ex.Message, LblMsg, Color.Red); }
    //}
   

    //----------------- EMAIL SENDING -------------------------------------

    #region Get Mail Body
    private string GetMailBody(pipersonalidsbo objFamilyBo, string EmpName, string AddressStatus)
    {
        try
        {
            StringBuilder Sb = new StringBuilder();
            string Mailbody = string.Empty;
            string AddressInfoFilePath = Server.MapPath(@"." + "/EmailTemplates/EmpAddressInfoTemplate.html");

            Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Address Type</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + objFamilyBo.ICNUM + "</td><td></td></tr>");
            Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Date</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + "</td><td></td></tr>");


            Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
            Mailbody = Mailbody.Replace("##LEAVETYPE##", objFamilyBo.ICTYPE);
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

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
       
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            PageLoadEvents();
            Response.Redirect("~/UI/Personal_Information/personal_ids.aspx", false);
        }
        catch (Exception Ex)
        {

        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
                if (!string.IsNullOrEmpty(DDL_PersonalIdTypes.SelectedValue))
                {
                    
                        pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
                        pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();

                        string HRMail = "";
                        string SuperVisorMail = "";
                        string PernrName = "";
                        string PernrEMail = "";
                        //local path = F:/Paycompute_Leavemodule/PRD02032020/iemppower-NRR/iEmpPower/PayCompute_Files/Payc_Empdocuments/
                        //quality path = C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/
                        //PRD path = C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/
                        string ImgPath = "";
                        if (file_docupload.HasFile)
                        {
                            DateTime dt = DateTime.Now;
                            string dtt = dt.ToString("yyyy-MM-dd hh:mm:ss").Replace(':', '-');
                            string filePath = "";
                            string ImgExt = Path.GetExtension(file_docupload.FileName.ToString().ToUpper());
                            if (ImgExt == ".JPG" | ImgExt == ".JPEG" | ImgExt == ".PDF" | ImgExt == ".PNG")
                            {
                                if (DDL_PersonalIdTypes.SelectedValue == "15")
                                {
                                    if (ImgExt == ".JPG" | ImgExt == ".JPEG" | ImgExt == ".PNG")
                                    {
                                        filePath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                   + User.Identity.Name.ToString().ToLower() + "-" + DDL_PersonalIdTypes.SelectedItem.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(file_docupload.FileName);
                                        ImgPath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                            + User.Identity.Name.ToString().ToString().ToLower() + "-" + DDL_PersonalIdTypes.SelectedItem.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(file_docupload.FileName);
                                        file_docupload.PostedFile.SaveAs(filePath);

                                        
                                        objPersonalIDsBo.ID = 0;
                                        objPersonalIDsBo.PERNR = User.Identity.Name;
                                        objPersonalIDsBo.ICTYPE = DDL_PersonalIdTypes.SelectedValue;
                                        objPersonalIDsBo.ICNUM = TxtIdNumber.Text.Trim().ToUpper();
                                        objPersonalIDsBo.docpath = ImgPath.ToString().Trim();
                                        ViewState["ictypp"] = DDL_PersonalIdTypes.SelectedValue;
                                        ViewState["ictxt"] = DDL_PersonalIdTypes.SelectedItem.Text.ToString();

                                        objPersonalIDsBo.BEGDA = DateTime.Now;
                                        objPersonalIDsBo.ENDDA = new DateTime(9999, 12, 31);
                                        objPersonalIDsBo.CREATED_BY = User.Identity.Name;
                                        objPersonalIDsBo.CREATED_ON = DateTime.Now;
                                        objPersonalIDsBo.MODIFIED_BY = Session["CompCode"].ToString();
                                        objPersonalIDsBo.MODIFIEDON = DateTime.Now;
                                        objPersonalIDsBo.STATUS = "NEW";
                                        objPersonalIDsBo.Flag = 1;

                                        objPersonalIDsBl.Add_Update_Del_PersonalIDsDetails(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                                        SendMail(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Saved");

                                        LblMsg.Text = string.Empty;
                                        if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                                        {
                                            MsgCls("Personal IDs Information added successfully !", LblMsg, Color.Green);
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information added successfully !');parent.location.href='personal_ids.aspx'", true);

                                            gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                                        }
                                        else
                                        {
                                            MsgCls("Personal IDs Information added successfully and sent for approval !", LblMsg, Color.Green);
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information added successfully and sent for approval !');parent.location.href='personal_ids.aspx'", true);

                                            gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Only JPEG,JPG,PNG formats are allowed to upload employee photo..!')", true);
                                    }

                                }
                                else
                                {
                                    if (ImgExt == ".PDF")
                                    {
                                        filePath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                   + User.Identity.Name.ToString().ToLower() + "-" + DDL_PersonalIdTypes.SelectedItem.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(file_docupload.FileName);
                                        ImgPath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                            + User.Identity.Name.ToString().ToString().ToLower() + "-" + DDL_PersonalIdTypes.SelectedItem.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(file_docupload.FileName);
                                        file_docupload.PostedFile.SaveAs(filePath);



                                        objPersonalIDsBo.ID = 0;
                                        objPersonalIDsBo.PERNR = User.Identity.Name;
                                        objPersonalIDsBo.ICTYPE = DDL_PersonalIdTypes.SelectedValue;
                                        objPersonalIDsBo.ICNUM = TxtIdNumber.Text.Trim().ToUpper();
                                        objPersonalIDsBo.docpath = ImgPath.ToString().Trim();
                                        ViewState["ictypp"] = DDL_PersonalIdTypes.SelectedValue;
                                        ViewState["ictxt"] = DDL_PersonalIdTypes.SelectedItem.Text.ToString();

                                        objPersonalIDsBo.BEGDA = DateTime.Now;
                                        objPersonalIDsBo.ENDDA = new DateTime(9999, 12, 31);
                                        objPersonalIDsBo.CREATED_BY = User.Identity.Name;
                                        objPersonalIDsBo.CREATED_ON = DateTime.Now;
                                        objPersonalIDsBo.MODIFIED_BY = Session["CompCode"].ToString();
                                        objPersonalIDsBo.MODIFIEDON = DateTime.Now;
                                        objPersonalIDsBo.STATUS = "NEW";
                                        objPersonalIDsBo.Flag = 1;

                                        objPersonalIDsBl.Add_Update_Del_PersonalIDsDetails(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                                        SendMail(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Saved");

                                        LblMsg.Text = string.Empty;
                                        if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                                        {
                                            MsgCls("Personal IDs Information added successfully !", LblMsg, Color.Green);
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information added successfully !');parent.location.href='personal_ids.aspx'", true);

                                            gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                                        }
                                        else
                                        {
                                            MsgCls("Personal IDs Information added successfully and sent for approval !", LblMsg, Color.Green);
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information added successfully and sent for approval !');parent.location.href='personal_ids.aspx'", true);

                                            gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                                        }
                                    }

                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Only PDF formats are allowed to upload..!')", true);
                                    }

                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only PDF and Image formats are allowed to upload..!')", true);
                            }
                        }

                        else
                        {
                            objPersonalIDsBo.ID = 0;
                            objPersonalIDsBo.PERNR = User.Identity.Name;
                            objPersonalIDsBo.ICTYPE = DDL_PersonalIdTypes.SelectedValue;
                            objPersonalIDsBo.ICNUM = TxtIdNumber.Text.Trim().ToUpper();
                            objPersonalIDsBo.docpath = ImgPath.ToString().Trim();
                            ViewState["ictypp"] = DDL_PersonalIdTypes.SelectedValue;
                            ViewState["ictxt"] = DDL_PersonalIdTypes.SelectedItem.Text.ToString();

                            objPersonalIDsBo.BEGDA = DateTime.Now;
                            objPersonalIDsBo.ENDDA = new DateTime(9999, 12, 31);
                            objPersonalIDsBo.CREATED_BY = User.Identity.Name;
                            objPersonalIDsBo.CREATED_ON = DateTime.Now;
                            objPersonalIDsBo.MODIFIED_BY = Session["CompCode"].ToString();
                            objPersonalIDsBo.MODIFIEDON = DateTime.Now;
                            objPersonalIDsBo.STATUS = "NEW";
                            objPersonalIDsBo.Flag = 1;

                            objPersonalIDsBl.Add_Update_Del_PersonalIDsDetails(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                            SendMail(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Saved");

                            LblMsg.Text = string.Empty;
                            if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                            {
                                MsgCls("Personal IDs Information added successfully !", LblMsg, Color.Green);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information added successfully !');parent.location.href='personal_ids.aspx'", true);

                                gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                            }
                            else
                            {
                                MsgCls("Personal IDs Information added successfully and sent for approval !", LblMsg, Color.Green);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information added successfully and sent for approval !');parent.location.href='personal_ids.aspx'", true);

                                gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                            }
                        }
                       
                    }
                   
                else
                { MsgCls("Please select the ID type !", LblMsg, Color.Red); }
            
        }
        catch (Exception Ex)
        {
            switch (Ex.Message)
            {
                case "-0":
                    MsgCls("Personal ID already exists !", LblMsg, Color.Red);
                    break;
                default:
                    MsgCls(Ex.Message, LblMsg, Color.Red);
                    break;
            }
        }
    }

    protected void btnCancelcrt_Click(object sender, EventArgs e)
    {
        dvcreatePI.Visible = false;
        PageLoadEvents();
        Response.Redirect("~/UI/Personal_Information/personal_ids.aspx", false);
    }

    protected void btn_updatePIdoc_Click(object sender, EventArgs e)
    {
        try
        {
            
                pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
                pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();

                string HRMail = "";
                string SuperVisorMail = "";
                string PernrName = "";
                string PernrEMail = "";


                //local path = F:/Paycompute_Leavemodule/PRD02032020/iemppower-NRR/iEmpPower/PayCompute_Files/Payc_Empdocuments/
                //quality path 80 = C:/inetpub/wwwroot/iEmp_SubexNewUI/PayCompute_Files/Payc_Empdocuments
                //PRD path 90 = C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/
                string ImgPathup = "";
                DateTime dtup = DateTime.Now;
                string dttupdt = dtup.ToString("yyyy-MM-dd hh:mm:ss").Replace(':', '-');
                string filePathup = "";
                string ImgExtup = Path.GetExtension(FileUpload1.FileName.ToString().ToUpper());
                if (FileUpload1.HasFile)
                {
                    if (ImgExtup == ".JPG" | ImgExtup == ".JPEG" | ImgExtup == ".PDF" | ImgExtup == ".PNG")
                    {
                        if (lbl_typtext.Text.Trim() == "Employee Photo")
                        {

                            if (ImgExtup == ".JPG" | ImgExtup == ".JPEG" | ImgExtup == ".PNG")
                            {

                                filePathup = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                   + User.Identity.Name.ToString().ToLower() + "-" + lbl_typtext.Text.ToString().Trim() + "-" + dttupdt.Trim() + Path.GetExtension(FileUpload1.FileName);
                                ImgPathup = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                    + User.Identity.Name.ToString().ToString().ToLower() + "-" + lbl_typtext.Text.ToString().Trim() + "-" + dttupdt.Trim() + Path.GetExtension(FileUpload1.FileName);
                                FileUpload1.PostedFile.SaveAs(filePathup);


                                objPersonalIDsBo.ID = int.Parse(ViewState["rowid"].ToString().Trim());
                                objPersonalIDsBo.PERNR = User.Identity.Name;
                                objPersonalIDsBo.ICTYPE = "";
                                objPersonalIDsBo.ICNUM = TxtEditIdNumber.Text.Trim().ToUpper();
                                objPersonalIDsBo.docpath = ImgPathup.ToString().Trim();
                                objPersonalIDsBo.BEGDA = DateTime.Now;
                                objPersonalIDsBo.ENDDA = new DateTime(9999, 12, 31);
                                objPersonalIDsBo.CREATED_BY = User.Identity.Name;
                                objPersonalIDsBo.CREATED_ON = DateTime.Now;
                                objPersonalIDsBo.MODIFIED_BY = Session["CompCode"].ToString();
                                objPersonalIDsBo.MODIFIEDON = DateTime.Now;
                                objPersonalIDsBo.STATUS = "UPDATE";
                                objPersonalIDsBo.Flag = 2;


                                objPersonalIDsBl.Add_Update_Del_PersonalIDsDetails(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                                SendMail(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Updated");

                                gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                                if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                                {
                                    MsgCls("Personal IDs Information updated successfully !", LblMsg, Color.Green);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information updated successfully !');parent.location.href='personal_ids.aspx'", true);

                                    gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                                }
                                else
                                {
                                    MsgCls("Personal IDs Information updated successfully and sent for approval !", LblMsg, Color.Green);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information updated successfully and sent for approval !');parent.location.href='personal_ids.aspx'", true);

                                    gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                                }

                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Only JPEG,JPG,PNG formats are allowed to upload employee photo..!')", true);
                            }
                        }



                        else
                        {
                            if (ImgExtup == ".PDF")
                            {

                                filePathup = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                  + User.Identity.Name.ToString().ToLower() + "-" + lbl_typtext.Text.ToString().Trim() + "-" + dttupdt.Trim() + Path.GetExtension(FileUpload1.FileName);
                                ImgPathup = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                    + User.Identity.Name.ToString().ToString().ToLower() + "-" + lbl_typtext.Text.ToString().Trim() + "-" + dttupdt.Trim() + Path.GetExtension(FileUpload1.FileName);
                                FileUpload1.PostedFile.SaveAs(filePathup);

                                objPersonalIDsBo.ID = int.Parse(ViewState["rowid"].ToString().Trim());
                                objPersonalIDsBo.PERNR = User.Identity.Name;
                                objPersonalIDsBo.ICTYPE = "";
                                objPersonalIDsBo.ICNUM = TxtEditIdNumber.Text.Trim().ToUpper();
                                objPersonalIDsBo.docpath = ImgPathup.ToString().Trim();
                                objPersonalIDsBo.BEGDA = DateTime.Now;
                                objPersonalIDsBo.ENDDA = new DateTime(9999, 12, 31);
                                objPersonalIDsBo.CREATED_BY = User.Identity.Name;
                                objPersonalIDsBo.CREATED_ON = DateTime.Now;
                                objPersonalIDsBo.MODIFIED_BY = Session["CompCode"].ToString();
                                objPersonalIDsBo.MODIFIEDON = DateTime.Now;
                                objPersonalIDsBo.STATUS = "UPDATE";
                                objPersonalIDsBo.Flag = 2;


                                objPersonalIDsBl.Add_Update_Del_PersonalIDsDetails(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                                SendMail(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Updated");

                                gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                                if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                                {
                                    MsgCls("Personal IDs Information updated successfully !", LblMsg, Color.Green);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information updated successfully !');parent.location.href='personal_ids.aspx'", true);

                                    gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                                }
                                else
                                {
                                    MsgCls("Personal IDs Information updated successfully and sent for approval !", LblMsg, Color.Green);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information updated successfully and sent for approval !');parent.location.href='personal_ids.aspx'", true);

                                    gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                                }
                            }


                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Only PDF formats are allowed to upload..!')", true);
                            }
                        }
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only PDF and Image formats are allowed to upload..!')", true);
                    }
                }
                else
                {
                    objPersonalIDsBo.ID = int.Parse(ViewState["rowid"].ToString().Trim());
                    objPersonalIDsBo.PERNR = User.Identity.Name;
                    objPersonalIDsBo.ICTYPE = "";
                    objPersonalIDsBo.ICNUM = TxtEditIdNumber.Text.Trim().ToUpper();
                    objPersonalIDsBo.docpath = ImgPathup.ToString().Trim();
                    objPersonalIDsBo.BEGDA = DateTime.Now;
                    objPersonalIDsBo.ENDDA = new DateTime(9999, 12, 31);
                    objPersonalIDsBo.CREATED_BY = User.Identity.Name;
                    objPersonalIDsBo.CREATED_ON = DateTime.Now;
                    objPersonalIDsBo.MODIFIED_BY = Session["CompCode"].ToString();
                    objPersonalIDsBo.MODIFIEDON = DateTime.Now;
                    objPersonalIDsBo.STATUS = "UPDATE";
                    objPersonalIDsBo.Flag = 2;


                    objPersonalIDsBl.Add_Update_Del_PersonalIDsDetails(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                    SendMail(objPersonalIDsBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Updated");

                    gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                    if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                    {
                        MsgCls("Personal IDs Information updated successfully !", LblMsg, Color.Green);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information updated successfully !');parent.location.href='personal_ids.aspx'", true);

                        gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                    }
                    else
                    {
                        MsgCls("Personal IDs Information updated successfully and sent for approval !", LblMsg, Color.Green);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal IDs Information updated successfully and sent for approval !');parent.location.href='personal_ids.aspx'", true);

                        gvidesi.Visible = GV_PersonalIdInfo.Rows.Count > 0 ? true : false;
                    }
                }
            
        }
        catch (Exception Ex)
        {
            switch (Ex.Message)
            {
                case "-0":
                    MsgCls("Personal ID already exists !", LblMsg, Color.Red);
                    break;
                default:
                    MsgCls(Ex.Message, LblMsg, Color.Red);
                    break;
            }

        }
    }

    protected void DDL_PersonalIdTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDL_PersonalIdTypes.SelectedValue == "15")
            {
                TxtIdNumber.Enabled = false;
                RFV_TxtIdNumber.Enabled = false;
            }
            else
            {
                TxtIdNumber.Enabled = true;
                RFV_TxtIdNumber.Enabled = true;
            }
        }
        catch (Exception ex)
        {

        }
    }

   
}

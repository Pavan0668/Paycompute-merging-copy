using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Web.Security;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing.Drawing2D;
using iEmpPower.Old_App_Code.iEmpPowerMaster;
public partial class UI_Personal_Information_personal_file : System.Web.UI.Page
{

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
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
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
                //GetMaritialStatus();
                PageLoadEvents();

                FV_PersonalDataInfo.ChangeMode(FormViewMode.ReadOnly);
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    //public void GetMaritialStatus()
    //{
    //   try{
    //       using (DropDownList DDLMaritialStatus = (DropDownList)FV_PersonalDataInfo.FindControl("DDLMaritialStatus"))
    //       {
    //           mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_MArital_Status();
    //           DDLMaritialStatus.DataSource = objLst;
    //           DDLMaritialStatus.DataTextField = "FTEXT";
    //           DDLMaritialStatus.DataValueField = "FAMST";
    //           DDLMaritialStatus.DataBind();
    //       }
    //   }
    //   catch (Exception Ex)
    //   { MsgCls(Ex.Message, LblMsg, Color.Red); }
    //}


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
            personal_informationBO objPersonaldataBo = new personal_informationBO();
            personal_informationBl objPersonaldataBl = new personal_informationBl();
            objPersonaldataBo.Comany_Code = Session["CompCode"].ToString();
            objPersonaldataBo.EmpID = User.Identity.Name.Trim();
            objPersonaldataBo.Approved_By = "";
            objPersonaldataBo.flag = 1;
            personal_informationCollBo objPersonaldataList = objPersonaldataBl.Get_EmpInfo(objPersonaldataBo);
            if (objPersonaldataList.Count > 0)
            {
                FV_PersonalDataInfo.DataSource = objPersonaldataList;
                FV_PersonalDataInfo.DataBind();
                MsgCls(string.Empty, LblMsg, Color.Transparent);


                //if ((objPersonaldataList[0].TRANSSTATUS == null ? "" : objPersonaldataList[0].TRANSSTATUS.ToString().Trim()) == "Updated")
                //{
                //    MsgCls("Editing can be done only after approval from HR of previous record updation", LblMsg, Color.Red);

                //}
                //else
                //{
                //    MsgCls(string.Empty, LblMsg, Color.Transparent);
                //}

                ViewState["MaritialSts"] = objPersonaldataList[0].Material_Status == null ? "" : objPersonaldataList[0].Material_Status.ToString();



            }
            else
            {

                FV_PersonalDataInfo.DataSource = null;
                FV_PersonalDataInfo.DataBind();
                MsgCls("No data found", LblMsg, Color.Red);
                return;
            }




        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion


    #region FV_PersonalDataInfo Events

    protected void FV_PersonalDataInfo_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        try
        {
            GetHRPernr();
            personaldatabl objPiInfoBl = new personaldatabl();
            personaldatabo objPiInfoBo = new personaldatabo();
            DateTime DtDOB = new DateTime(1900, 01, 01);
            DateTime DtMarriedSince = new DateTime(1900, 01, 01);
            switch (e.CommandName.ToUpper())
            {
                case "EDITPERINFO":
                    FV_PersonalDataInfo.ChangeMode(FormViewMode.Edit);
                    PageLoadEvents();
                    using (TextBox TxtNameAtBirth = (TextBox)FV_PersonalDataInfo.FindControl("TxtNameAtBirth"))
                    {
                        if (TxtNameAtBirth != null)
                        {
                            TxtNameAtBirth.Focus();
                        }
                    }

                    using (DropDownList DDLMaritialStatus = (DropDownList)FV_PersonalDataInfo.FindControl("DDLMaritialStatus"))
                    using (TextBox TxtMarriedSince = (TextBox)FV_PersonalDataInfo.FindControl("TxtMarriedSince"))
                    using (TextBox TxtNumberOfChildren = (TextBox)FV_PersonalDataInfo.FindControl("TxtNumberOfChildren"))
                    using (Panel PnlMaritialSts = (Panel)FV_PersonalDataInfo.FindControl("PnlMaritialSts"))
                    {
                        configurationcollectionbo objLst2 = configurationbl.Get_MaterialStatus(1);
                        DDLMaritialStatus.DataSource = objLst2;
                        DDLMaritialStatus.DataTextField = "DDLTYPETEXT";
                        DDLMaritialStatus.DataValueField = "DDLTYPE";
                        DDLMaritialStatus.DataBind();
                        DDLMaritialStatus.Items.Insert(0, new ListItem(" - SELECT - ", "0"));


                        DDLMaritialStatus.SelectedValue = ViewState["MaritialSts"].ToString().Trim();


                    }

                    break;
                case "CANCELPERSINFO":
                    FV_PersonalDataInfo.ChangeMode(FormViewMode.ReadOnly);
                    PageLoadEvents();
                    break;
                case "UPDATEPERSINFO":
                    if (FV_PersonalDataInfo.CurrentMode == FormViewMode.Edit)
                    {
                        using (TextBox txtFathrName = (TextBox)FV_PersonalDataInfo.FindControl("txtFathrName"))
                        using (TextBox txtMthrName = (TextBox)FV_PersonalDataInfo.FindControl("txtMthrName"))
                        using (TextBox TxtDOB = (TextBox)FV_PersonalDataInfo.FindControl("TxtDOB"))
                        using (RadioButtonList RbtnGender = (RadioButtonList)FV_PersonalDataInfo.FindControl("RbtnGender"))
                        using (TextBox txtSpousrName = (TextBox)FV_PersonalDataInfo.FindControl("txtSpousrName"))
                        using (DropDownList DDLMaritialStatus = (DropDownList)FV_PersonalDataInfo.FindControl("DDLMaritialStatus"))
                        {

                            string HRMail = "";
                            string SuperVisorMail = "";
                            string PernrName = "";
                            string PernrEMail = "";


                            objPiInfoBo.ID = int.Parse(FV_PersonalDataInfo.DataKey["ID"].ToString());
                            objPiInfoBo.PERNR = User.Identity.Name;
                            //objPiInfoBo.PHOTO = string.Empty;
                            objPiInfoBo.TITEL = string.Empty;
                            objPiInfoBo.VORNA = string.Empty;
                            objPiInfoBo.NACHN = string.Empty;


                            //TITEL_TEXT,VORNA,NACHN,NATIO_Txt

                            //ViewState["VORNA"] = FV_PersonalDataInfo.DataKey["VORNA"].ToString();

                            //ViewState["NACHN"] = FV_PersonalDataInfo.DataKey["NACHN"].ToString();

                            //ViewState["NATIO_Txt"] = FV_PersonalDataInfo.DataKey["NATIO_Txt"].ToString();
                            //ViewState["NATIO"] = FV_PersonalDataInfo.DataKey["NATIO"].ToString();

                            objPiInfoBo.NAME2 = "";
                            objPiInfoBo.INITS = "";
                            objPiInfoBo.RUFNM = txtFathrName.Text.Trim();
                            objPiInfoBo.SPRSL = txtMthrName.Text.Trim();
                            objPiInfoBo.GESCH = RbtnGender.SelectedValue;
                            objPiInfoBo.GBDAT = Convert.ToDateTime(TxtDOB.Text.Trim());
                            objPiInfoBo.GBORT = txtSpousrName.Text.Trim();
                            objPiInfoBo.GBLND = string.Empty;
                            objPiInfoBo.NATIO = string.Empty;
                            //DDLCountryOfBirth.SelectedValue; //DDLFirstNationality.SelectedValue;
                            objPiInfoBo.GBDEP = string.Empty;
                            objPiInfoBo.NATI2 = "";//DDLSecondNationality.SelectedValue;
                            objPiInfoBo.NATI3 = "";// DDLThirdNationality.SelectedValue;
                            objPiInfoBo.FAMST = DDLMaritialStatus.SelectedValue;
                            objPiInfoBo.FAMDT = Convert.ToDateTime("1900-01-01");
                            objPiInfoBo.ANZKD = 0;
                            objPiInfoBo.KONFE = string.Empty;
                            objPiInfoBo.BEGDA = Convert.ToDateTime("1900-01-01"); // told by prince
                            //objPiInfoBo.BEGDA = DateTime.Now;
                            objPiInfoBo.ENDDA = Convert.ToDateTime("1900-01-01");
                            objPiInfoBo.CREATED_BY = User.Identity.Name;
                            objPiInfoBo.CREATED_ON = DateTime.Now;
                            objPiInfoBo.MODIFIED_BY = Session["CompCode"].ToString();
                            objPiInfoBo.MODIFIEDON = DateTime.Now;
                            objPiInfoBo.Flag = 1;
                            objPiInfoBo.STATUS = "UPDATE";
                            objPiInfoBl.Add_Update_Personal_Data(objPiInfoBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);

                            configurationcollectionbo objLst2 = configurationbl.Get_MaterialStatus(1);
                            DDLMaritialStatus.DataSource = objLst2;
                            DDLMaritialStatus.DataTextField = "DDLTYPETEXT";
                            DDLMaritialStatus.DataValueField = "DDLTYPE";
                            DDLMaritialStatus.DataBind();
                            DDLMaritialStatus.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
                            DDLMaritialStatus.SelectedValue = objPiInfoBo.FAMST;
                            ViewState["Msts"] = DDLMaritialStatus.SelectedItem.ToString().Trim();

                            SendMail(objPiInfoBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail, "Updated");


                            FV_PersonalDataInfo.ChangeMode(FormViewMode.ReadOnly);
                            PageLoadEvents();
                            //MsgCls("Personal information updated successfully  and sent for approval!", LblMsg, Color.Green);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal information updated successfully  and sent for approval!')", true);

                            if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name.ToString().Trim())
                            {
                                MsgCls("Personal information updated successfully !", LblMsg, Color.Green);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal information updated successfully !')", true);
                            }
                            else
                            {
                                MsgCls("Personal information updated successfully  and sent for approval !", LblMsg, Color.Green);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal information updated successfully  and sent for approval !')", true);
                            }
                        }
                    }
                    break;
                case "UPLOADPROFIMAGE":
                    if (FV_PersonalDataInfo.CurrentMode == FormViewMode.Edit || FV_PersonalDataInfo.CurrentMode == FormViewMode.ReadOnly)
                    {
                        string ID = FV_PersonalDataInfo.DataKey["ID"].ToString();
                        using (FileUpload FU_ProfImg = (FileUpload)FV_PersonalDataInfo.FindControl("FU_ProfImg"))
                        {
                            if (FU_ProfImg.PostedFile != null && !string.IsNullOrEmpty(FU_ProfImg.PostedFile.FileName))
                            {
                                string ImgExt = Path.GetExtension(FU_ProfImg.FileName.ToString().ToUpper());
                                if (ImgExt == ".JPG" | ImgExt == ".JPEG" | ImgExt == ".GIF" | ImgExt == ".PNG")
                                {
                                    using (Bitmap OriIMG = new Bitmap(FU_ProfImg.FileContent))
                                    {
                                        // Calculate the new image dimensions
                                        decimal origWidth = OriIMG.Width;
                                        decimal origHeight = OriIMG.Height;
                                        decimal sngRatio = origHeight / origWidth;
                                        int newHeight = 95;  //hight in pixels
                                        decimal newWidth_temp = newHeight / sngRatio;
                                        int newWidth = Convert.ToInt16(newWidth_temp);
                                        using (Bitmap NewIMG = new Bitmap(OriIMG, newWidth, newHeight))
                                        {
                                            using (Graphics OGrap = Graphics.FromImage(OriIMG))
                                            {
                                                OGrap.SmoothingMode = SmoothingMode.AntiAlias;
                                                OGrap.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                                OGrap.DrawImage(OriIMG, 0, 0, newWidth, newHeight);
                                                string DirPath = Server.MapPath(@"~/EmpImages/");
                                                string MilSec = DateTime.Now.Millisecond.ToString();
                                                string IMGFileName = DirPath + "~" + User.Identity.Name + "~" + MilSec + ".jpg";
                                                string ImgPath = @"~/EmpImages/" + "~" + User.Identity.Name + "~" + MilSec + ".jpg";


                                                DirectoryInfo DirInfo = new DirectoryInfo(DirPath);
                                                FileInfo[] FlInfo = DirInfo.GetFiles("*" + User.Identity.Name + "*.*");
                                                foreach (FileInfo Fl in FlInfo)
                                                { Fl.Delete(); }
                                                NewIMG.Save(IMGFileName);

                                                string HRMail = "";
                                                string SuperVisorMail = "";
                                                string PernrName = "";
                                                string PernrEMail = "";


                                                objPiInfoBo.ID = int.Parse(FV_PersonalDataInfo.DataKey["ID"].ToString());
                                                objPiInfoBo.PERNR = User.Identity.Name;
                                                objPiInfoBo.PHOTO = ImgPath;
                                                objPiInfoBo.TITEL = string.Empty;
                                                objPiInfoBo.VORNA = string.Empty;
                                                objPiInfoBo.NACHN = string.Empty;
                                                objPiInfoBo.NAME2 = string.Empty;
                                                objPiInfoBo.INITS = string.Empty;
                                                objPiInfoBo.RUFNM = string.Empty;
                                                objPiInfoBo.SPRSL = string.Empty;
                                                objPiInfoBo.GESCH = string.Empty;
                                                objPiInfoBo.GBDAT = DateTime.Now;
                                                objPiInfoBo.GBORT = string.Empty;
                                                objPiInfoBo.GBLND = string.Empty;
                                                objPiInfoBo.NATIO = string.Empty;
                                                objPiInfoBo.GBDEP = string.Empty;
                                                objPiInfoBo.NATI2 = string.Empty;
                                                objPiInfoBo.NATI3 = string.Empty;
                                                objPiInfoBo.FAMST = string.Empty;
                                                objPiInfoBo.FAMDT = DateTime.Now;
                                                objPiInfoBo.ANZKD = 0;
                                                objPiInfoBo.KONFE = string.Empty;
                                                objPiInfoBo.BEGDA = DateTime.Now;
                                                objPiInfoBo.ENDDA = DateTime.Now;
                                                objPiInfoBo.CREATED_BY = User.Identity.Name;
                                                objPiInfoBo.CREATED_ON = DateTime.Now;
                                                objPiInfoBo.MODIFIED_BY = Session["CompCode"].ToString();
                                                objPiInfoBo.MODIFIEDON = DateTime.Now;
                                                objPiInfoBo.Flag = 2;
                                                objPiInfoBo.STATUS = "UPDATE";

                                                objPiInfoBl.Add_Update_Personal_Data(objPiInfoBo, ref HRMail, ref SuperVisorMail, ref PernrName, ref PernrEMail);
                                                FV_PersonalDataInfo.ChangeMode(FormViewMode.ReadOnly);
                                                PageLoadEvents();
                                                MsgCls("Profile image Updated successfully !", LblMsg, Color.Green);
                                                //string[] MsgCC = { HRMail, SuperVisorMail };
                                                //iEmpPowerMaster_Load.masterbl.SendMail(PernrEMail, MsgCC, "Profile Image Updated - " + PernrName + " (" + User.Identity.Name + ")", GetMailBody(objPiInfoBo, PernrName, "Updated"));
                                                //ScriptManager.RegisterStartupScript(this, GetType(), "Msg", "window.location='~/UI/Personal_Information/personal_data.aspx'", true);
                                                Response.Redirect("~/UI/Personal_Information/personal_data.aspx", false);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "DELETPROFIMAGE":
                    string DirPath1 = Server.MapPath(@"~/EmpImages/");
                    DirectoryInfo DirInfo1 = new DirectoryInfo(DirPath1);
                    FileInfo[] FlInfo1 = DirInfo1.GetFiles("*" + User.Identity.Name + "*.*");
                    foreach (FileInfo Fl in FlInfo1)
                    { Fl.Delete(); }
                    //--------------------------- 

                    string HRMail1 = "";
                    string SuperVisorMail1 = "";
                    string PernrName1 = "";
                    string PernrEMail1 = "";


                    objPiInfoBo.ID = int.Parse(FV_PersonalDataInfo.DataKey["ID"].ToString());
                    objPiInfoBo.PERNR = User.Identity.Name;
                    objPiInfoBo.PHOTO = "~/EmpImages/EmpImage.jpg";
                    objPiInfoBo.TITEL = string.Empty;
                    objPiInfoBo.VORNA = string.Empty;
                    objPiInfoBo.NACHN = string.Empty;
                    objPiInfoBo.NAME2 = string.Empty;
                    objPiInfoBo.INITS = string.Empty;
                    objPiInfoBo.RUFNM = string.Empty;
                    objPiInfoBo.SPRSL = string.Empty;
                    objPiInfoBo.GESCH = string.Empty;
                    objPiInfoBo.GBDAT = DateTime.Now;
                    objPiInfoBo.GBORT = string.Empty;
                    objPiInfoBo.GBLND = string.Empty;
                    objPiInfoBo.NATIO = string.Empty;
                    objPiInfoBo.GBDEP = string.Empty;
                    objPiInfoBo.NATI2 = string.Empty;
                    objPiInfoBo.NATI3 = string.Empty;
                    objPiInfoBo.FAMST = string.Empty;
                    objPiInfoBo.FAMDT = DateTime.Now;
                    objPiInfoBo.ANZKD = 0;
                    objPiInfoBo.KONFE = string.Empty;
                    objPiInfoBo.BEGDA = DateTime.Now;
                    objPiInfoBo.ENDDA = DateTime.Now;
                    objPiInfoBo.CREATED_BY = User.Identity.Name;
                    objPiInfoBo.CREATED_ON = DateTime.Now;
                    objPiInfoBo.MODIFIED_BY = Session["CompCode"].ToString();
                    objPiInfoBo.MODIFIEDON = DateTime.Now;
                    objPiInfoBo.Flag = 2;
                    objPiInfoBo.STATUS = "UPDATE";

                    objPiInfoBl.Add_Update_Personal_Data(objPiInfoBo, ref HRMail1, ref SuperVisorMail1, ref PernrName1, ref PernrEMail1);

                    FV_PersonalDataInfo.ChangeMode(FormViewMode.ReadOnly);
                    PageLoadEvents();
                    MsgCls("Profile image Updated successfully !", LblMsg, Color.Green);
                    //string[] MsgCC = { HRMail, SuperVisorMail };
                    //iEmpPowerMaster_Load.masterbl.SendMail(PernrEMail, MsgCC, "Profile Image Updated - " + PernrName + " (" + User.Identity.Name + ")", GetMailBody(objPiInfoBo, PernrName, "Updated"));
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Msg", "window.location='~/UI/Personal_Information/personal_data.aspx'", true);
                    Response.Redirect("~/UI/Personal_Information/personal_data.aspx", false);
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
                    MsgCls("Personal Data already exists !", LblMsg, Color.Red);
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

    public void SendMail(personaldatabo objPiInfoBo, ref string HRMail, ref string SuperVisorMail, ref string PernrName, ref string PernrEMail, string type)
    {
        try
        {
            using (TextBox txtFathrName = (TextBox)FV_PersonalDataInfo.FindControl("txtFathrName"))
            using (TextBox txtMthrName = (TextBox)FV_PersonalDataInfo.FindControl("txtMthrName"))
            using (TextBox TxtDOB = (TextBox)FV_PersonalDataInfo.FindControl("TxtDOB"))
            using (RadioButtonList RbtnGender = (RadioButtonList)FV_PersonalDataInfo.FindControl("RbtnGender"))
            using (TextBox txtSpousrName = (TextBox)FV_PersonalDataInfo.FindControl("txtSpousrName"))
            using (DropDownList DDLMaritialStatus = (DropDownList)FV_PersonalDataInfo.FindControl("DDLMaritialStatus"))
            {
                string marital = ViewState["Msts"].ToString().Trim() == "- SELECT -" ? "" : ViewState["Msts"].ToString().Trim();
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
                        strSubject = "Personal Data Information has been updated by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".This has been Self Approved, No Action Required.";
                    }
                }
                else
                {
                    if (type == "Updated")
                    {
                        strSubject = "Personal Data Information has been updated by " + Session["Empname"].ToString() + "  | " + empid.ToString() + ".";
                    }
                }
                string RecipientsString = HRMail;
                string strPernr_Mail = PernrEMail;
                string marrdate = string.Empty;

                if (DateTime.Parse(objPiInfoBo.FAMDT.ToString()).ToString("dd-MM-yyyy") == "01-01-1900")
                {
                    marrdate = " - ";
                }
                else
                {
                    marrdate = DateTime.Parse(objPiInfoBo.FAMDT.ToString()).ToString("dd-MMM-yyyy");
                }
                //Preparing the mail body--------------------------------------------------

                string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                body = body + "<b style= 'font-size: 14px';>Personal Data details : </b><hr>";
                body += "<b><table style=border-collapse:collapse;><tr><td style='font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'> Employee ID</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + empid.ToString() + "</td></tr>";
                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + Session["Empname"].ToString() + "</td></tr>";


                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Gender </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + RbtnGender.SelectedItem.ToString() + "</td></tr>";
                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Date of Birth </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(TxtDOB.Text.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Father Name </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + txtFathrName.Text.ToString() + "</td></tr>";
                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Mother Name </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + txtMthrName.Text.ToString() + "</td></tr>";
                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'> Maritial Status </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + marital.ToString().Trim() + "</td></tr>";
                body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Spouse Name </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + txtSpousrName.Text.ToString() + "</td></tr></table></b>";


                body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";


                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject1, strPernr_Mail, body);

            }

        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }


    protected void FV_PersonalDataInfo_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        try
        {
            FV_PersonalDataInfo.ChangeMode(e.NewMode);
            PageLoadEvents();
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    #endregion

    //----------------- EMAIL SENDING -------------------------------------

    #region Get Mail Body
    private string GetMailBody(personaldatabo objPersonalInfoBo, string EmpName, string AddressStatus)
    {
        try
        {
            StringBuilder Sb = new StringBuilder();
            string Mailbody = string.Empty;
            string AddressInfoFilePath = Server.MapPath(@"." + "/EmailTemplates/EmpAddressInfoTemplate.html");

            Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Address Type</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + objPersonalInfoBo.BEGDA + "</td><td></td></tr>");
            Sb.Append("<tr><td style=\"width: 150px; font-weight: bold;\">Date</td><td style=\"width: 15px; text-align: center; font-weight: bold;\">:</td><td>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + "</td><td></td></tr>");


            Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
            Mailbody = Mailbody.Replace("##LEAVETYPE##", objPersonalInfoBo.KONFE);
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

    protected void DDLMaritialStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            using (DropDownList DDLMaritialStatus = (DropDownList)FV_PersonalDataInfo.FindControl("DDLMaritialStatus"))
            using (TextBox TxtMarriedSince = (TextBox)FV_PersonalDataInfo.FindControl("TxtMarriedSince"))
            using (TextBox TxtNumberOfChildren = (TextBox)FV_PersonalDataInfo.FindControl("TxtNumberOfChildren"))
            using (Panel PnlMaritialSts = (Panel)FV_PersonalDataInfo.FindControl("PnlMaritialSts"))
            {
                if (DDLMaritialStatus.SelectedValue.Trim() == "0" || string.IsNullOrEmpty(DDLMaritialStatus.SelectedValue.Trim()))
                {
                    PnlMaritialSts.Visible = false;
                }
                else
                {
                    PnlMaritialSts.Visible = true;
                }

            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }




}
using iEmpPower.Old_App_Code.iEmpPowerDAL.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Configuration;

using System.Globalization;
using iEmpPower.Old_App_Code.iEmpPowerBL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBL.IT;
using iEmpPower.Old_App_Code.iEmpPowerBO.IT;
using iEmpPower.Old_App_Code.iEmpPowerDAL.IT;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;
using System.IO;
using iEmpPowerMaster_Load;

using System.Configuration;
using System.Diagnostics;
using System.Data;


namespace iEmpPower.UI
{
    public partial class SubSiteMaster : System.Web.UI.MasterPage
    {
        protected MembershipUser memUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
                Request.Browser.Adapters.Clear();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);


            if (!IsPostBack)
            {
               Session["CompCode"] = "";
                ReprtingMgnr();
                
            }
            LoadEmployeeDetails();
            load_emp_bday();
            

            //-----------------------Html Menu Filter Starts-----------------------

            menus_enble_disable();

            string[] UserRoles = Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name);
            if (UserRoles.Length > 0)
            {
                switch (UserRoles[0].ToUpper())
                {
                    case "SUPERUSER":
                        MenuPersonalInfo.Visible = false;
                        MenuWorkingTime.Visible = false;
                        MenuMSS.Visible = false;
                        Mega_mgrapprvl.Visible = false;
                        break;

                    default:
                        break;
                }
            }

            else
            {
                MenuConfig.Visible = false;
                MenuMaintainUserAccounts.Visible = false;
                MenuResetEmployeePassword.Visible = false;
            }

            msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
            msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(Session["CompCode"].ToString(), objPIDashBoardBo);
            if (objPIDashBoardLst.Count > 0)
            {
                MenuMSS.Visible = true;
                Mega_mgrapprvl.Visible = true;

            }
            else
            {
                MenuMSS.Visible = false;
                Mega_mgrapprvl.Visible = false;

            }



            //-----------------------Html Menu Filter Ends-----------------------
            GetHRPERNRS();

        }

        protected void LoadCustomerLogo()
        {
            try
            {
                configurationdalDataContext context1 = new configurationdalDataContext();
                var vLogo = (from col in context1.sp_conf_get_logo()
                             select col.img_logo).ToList();
                if (vLogo.Count <= 0)
                {

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void LoadEmployeeDetails()
        {
            //http://shawpnendu.blogspot.com/2010/02/javascript-to-read-master-page-and.html
            try
            {
                memUser = Membership.GetUser();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            try
            {
                bool status = memUser.IsOnline;

                if (memUser != null)
                {
                    configurationbl objBl = new configurationbl();
                    configurationcollectionbo objLst = objBl.Load_EmployeePhotoDetails(memUser.ToString());
                    foreach (configurationbo objBo in objLst)
                    {
                        string ccode = objBo.Company_Code.Trim();
                        string emplogin = memUser.ToString();
                        int cnt = ccode.Length;
                        emplogin = emplogin.Substring(cnt).ToUpper();

                        ViewState["isActive"] = objBo.isAct;
                        Label lName = HeadLoginView.FindControl("lblEmployyeName") as Label;
                        lName.Text = objBo.DESCRIPTION;
                        //lName.Text = objBo.DESCRIPTION + " | " + emplogin.ToString();
                        spnCompCode.InnerHtml = objBo.Company_Code.Trim() == memUser.ToString() ? "" : "Company Code : " + objBo.Company_Code.Trim();
                        Session.Add("CompLogin", objBo.iscomp);
                        Session.Add("isDR", objBo.isDR);
                        imgCustomerLogo.ImageUrl = objBo.CLOGO;
                        string sEmailId = memUser.Email.ToString();
                        Session["Empname"] = objBo.DESCRIPTION;
                        //divemail.InnerText = sEmailId; //memUser.Email;

                        Session.Add("sEmploreeId", memUser.ToString());
                        Session.Add("EmployeeName", objBo.DESCRIPTION);
                        Session.Add("CompCode", objBo.Company_Code.Trim() == "" ? memUser.ToString() : objBo.Company_Code.Trim());
                        if (objBo.EMPLOYEE_PATH.ToString() != "")
                        {

                            imgEmp.ImageUrl = objBo.EMPLOYEE_PATH;
                            newImage.ImageUrl = objBo.EMPLOYEE_PATH;
                        }
                    }
                }
                else if (memUser.UserName == "" || memUser.UserName == null || !status)
                {

                }
                if (memUser.ToString() != "70296")
                {
                    if (((bool)ViewState["isActive"] == true) && (Session["CompLogin"].ToString().Trim() == "1"))
                    {
                        Response.Redirect("~/Account/Login.aspx", false);
                    }
                    if (((bool)ViewState["isActive"] == false) && (Session["CompLogin"].ToString().Trim() == "0"))
                    {
                        Response.Redirect("~/Account/Login.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Account/Login.aspx", false);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            CheckSessionStatus();
        }

        protected void CheckSessionStatus()
        {
            if (Context.Session != null)
            {
                if (Session.IsNewSession)
                {
                    HttpCookie newSessionIdCookie = Request.Cookies["ASP.NET_SessionId"];
                    if (newSessionIdCookie != null)
                    {
                        string newSessionIdCookieValue = newSessionIdCookie.Value;
                        if (newSessionIdCookieValue != string.Empty)
                        {
                            Response.Redirect("~/Account/Login.aspx", false);
                        }
                    }
                }
            }
        }

        protected void imgbtnDirectory_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/UI/Manager_Self_Service/who'swho.aspx", false);
        }

        public void load_emp_bday()
        {
            Label lblEmployyeName = HeadLoginView.FindControl("lblEmployyeName") as Label;
            msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            Announcementbo AnnouncementObjBo = new Announcementbo();
            Announcementbl AnnouncementObjBl = new Announcementbl();
            bool? bDay = false;
            msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Load_emp_Bday(HttpContext.Current.User.Identity.Name, Session["CompCode"].ToString().ToLower().Trim(), ref bDay);
            spanntfCnt.InnerHtml = objPIDashBoardLst[0].ENAME != null && bDay == true ? "2" : objPIDashBoardLst[0].ENAME != null ? "1" : "0";
            aMyBdayHead.InnerHtml = bDay == true ? "Hi, " + Session["Empname"].ToString().Trim() : "";
            aMyBday.Visible = bDay == false ? false : true;
            if (objPIDashBoardLst[0].ENAME != null)
            {
                aOtherBday.Visible = true;
                pNotHead.InnerHtml = "Many More Happy Returns of the Day to " + objPIDashBoardLst[0].ENAME;                
            }
            else
            {
                aOtherBday.Visible = false;
            }
            //AnnouncementToolCollectionbo obLstAnnouncement = AnnouncementObjBl.Load_Announcement();

        }

        public void signOut(object sender, EventArgs e)
        {
            try
            {
                Session.RemoveAll();
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            Response.Redirect("~/Account/Login.aspx", false);
            Response.End();
        }

        public void menus_enble_disable()
        {
            try
            {
                if (memUser.ToString().Trim() == "70296")
                {
                    Mega_PI.Visible = false;
                    Mega_comlog.Visible = false;
                    Mega_itcadmin.Visible = true;
                    MenuConfig.Visible = true;
                    Mega_WTR.Visible = false;
                    Mega_payreports.Visible = false;
                    Mega_mgrapprvl.Visible = false;
                    Mega_pswrest.Visible = true;
                    mega_itcrestpsw.Visible = true;
                    mega_itchaccnts.Visible = true;
                    MegaMenuIT.Visible = false;
                    asli18.Visible = false;

                    myrequests.Visible = false;
                    MenuMaintainDirectory.Visible = false;
                    Compcreate.Visible = true;
                    addUsers.Visible = false;
                    menuUpdateEmpinfo.Visible = false;
                    ConpConfig.Visible = false;
                    Admin_upload_salary.Visible = true;
                    Menuupload_salarydtls_compn.Visible = false;
                    Company_masters.Visible = false;
                    Payroll_Management.Visible = false;
                    MenuPersonalInfo.Visible = false;
                    MenuWorkingTime.Visible = false;
                    MenuMSS.Visible = false;
                    MenuBP.Visible = false;
                    Companies.Visible = true;
                    MenuAdminPayslip.Visible = true;
                    MenuMaintainUserAccounts.Visible = true;
                    MenuResetEmployeePassword.Visible = true;
                    MenuPersonalInfo.Visible = false;
                    MenuMyaccount.Visible = true;
                    MenuDoc.Visible = true;
                    MenuFBP.Visible = false;
                    MenuIExpense.Visible = false;
                    MenuIT.Visible = false;
                    MenuTravel.Visible = false;
                    MenuPR.Visible = false;
                    Li1.Visible = false;

                    MenuMyaccount.Visible = true;

                }
                    //Company Login
                else if (Session["CompLogin"].ToString() == "1")
                {
                    Mega_PI.Visible = false;
                    Mega_comlog.Visible = true;
                    Mega_itcadmin.Visible = false;
                    Mega_WTR.Visible = false;
                    Mega_payreports.Visible = true;
                    megacompayslip.Visible = true;
                    megapayslip.Visible = false;
                    Mega_mgrapprvl.Visible = false;
                    Mega_pswrest.Visible = true;
                    mega_itcrestpsw.Visible = false;
                    mega_itchaccnts.Visible = false;
                    MegaMenuIT.Visible = true;
                    mega_directory.Visible = true;
                    Mega_payreports.Visible = true;
                    megapayslip.Visible = true;
                    megaSR.Visible = true;
                    megaallpay.Visible = true;
                    megastatu.Visible = true;
                    megacompayslip.Visible = true;
                    megapayslip.Visible = false;
                    asli18.Visible = false;

                    imgEmp.Visible = false;
                    MenuMyaccount.Visible = false;
                    myrequests.Visible = false;
                    imgEmp.Visible = false;
                    spnCompCode.Visible = false;
                    MenuConfig.Visible = false;
                    Compcreate.Visible = false;
                    addUsers.Visible = true;
                    menuUpdateEmpinfo.Visible = true;
                    ConpConfig.Visible = true;
                    Companies.Visible = false;
                    Admin_upload_salary.Visible = false;
                    Menuupload_salarydtls_compn.Visible = true;
                    Company_masters.Visible = true;
                    Payroll_Management.Visible = true;
                    MenuPersonalInfo.Visible = false;
                    MenuWorkingTime.Visible = false;
                    MenuMSS.Visible = false;
                    MenuBP.Visible = false;
                    MenuPayslip1.Visible = false;
                    //pnlotherMenu2.Visible = true;
                    MenuDoc.Visible = true;
                    MenuMaintainUserAccounts.Visible = false;
                    MenuResetEmployeePassword.Visible = false;
                    //MenuMyaccount.Visible = true;
                    MenuMaintainUserAccounts.Visible = false;
                    MenuResetEmployeePassword.Visible = false;
                    MenuFinanceViewIExpenseStatus.Visible = true;
                    MenuFinanceViewClaimsStatus.Visible = true;


                    fbpDeclaration.Visible = true;
                    fbpapprej.Visible = true;
                    fbplocking.Visible = true;
                    fbpAllClaims.Visible = true;
                    ITAppRej.Visible = true;
                    ITLock.Visible = true;
                    ITAdminViewAll.Visible = true;
                }
                    //Employee Login
                else if (Session["CompLogin"].ToString() == "0")
                {
                    Mega_PI.Visible = true;
                    Mega_comlog.Visible = false;
                    Mega_WTR.Visible = true;
                    Mega_itcadmin.Visible = false;
                    Mega_payreports.Visible = true;
                    megacompayslip.Visible = false;
                    megapayslip.Visible = true;
                    megaSR.Visible = false;
                    megaallpay.Visible = false;
                    megastatu.Visible = false;
                    megacompayslip.Visible = false;
                    megapayslip.Visible = true;
                    Mega_mgrapprvl.Visible = false;
                    Mega_pswrest.Visible = true;
                    mega_itcrestpsw.Visible = false;
                    mega_itchaccnts.Visible = false;
                    MegaMenuIT.Visible = true;
                    mega_directory.Visible = true;
                    

                    asli18.Visible = true;
                    MenuMyaccount.Visible = false;
                    myrequests.Visible = true;
                    spnCompCode.Visible = false;
                    MenuConfig.Visible = false;
                    Compcreate.Visible = false;
                    addUsers.Visible = false;
                    menuUpdateEmpinfo.Visible = false;
                    ConpConfig.Visible = false;
                    Companies.Visible = false;
                    Admin_upload_salary.Visible = false;
                    Menuupload_salarydtls_compn.Visible = false;
                    Company_masters.Visible = false;
                    Payroll_Management.Visible = false;
                    MenuPersonalInfo.Visible = true;
                    MenuWorkingTime.Visible = true;
                    MenuMSS.Visible = false;
                    MenuBP.Visible = true;
                    MenuPayslip1.Visible = true;
                    //pnlotherMenu2.Visible = true;
                    MenuDoc.Visible = true;
                    Companies.Visible = false;
                   // MenuMyaccount.Visible = true;
                    MenuMaintainUserAccounts.Visible = false;
                    MenuResetEmployeePassword.Visible = false;

                    MenuPRStatusforPT.Visible = false;
                    MenuFinanceViewIExpenseStatus.Visible = false;
                    MenuFinanceViewClaimsStatus.Visible = false;
                    fbpDeclaration.Visible = false;
                    fbpapprej.Visible=false;
                    fbplocking.Visible=false;
                    fbpAllClaims.Visible = false;
                    ITAppRej.Visible = false;
                    ITLock.Visible = false;
                    ITAdminViewAll.Visible = false;

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void ReprtingMgnr()
        {
            try
            {
                msassignedtomebl objAssginTMBl = new msassignedtomebl();
                msassignedtomebo objAssginTMBo = new msassignedtomebo();
                objAssginTMBo.PERNR = HttpContext.Current.User.Identity.Name;
                objAssginTMBo.COMMENTS = Session["CompCode"].ToString();
                msassignedtomecollectionbo objColl = objAssginTMBl.Get_EmployeeDetails(objAssginTMBo);
                if (objColl.Count > 0)
                {
                    foreach (msassignedtomebo obj in objColl)
                    {
                        msassignedtomebo objAssginTMBo1 = new msassignedtomebo();
                       // lblRepPERNR.Text = obj.S_NAME;
                    }
                    MenuMSS.Visible = true;
                    Mega_mgrapprvl.Visible = true;
                    myrequests.Visible = true;
                }

                else
                {
                    //divRep.Visible = false;
                }
            }
            catch (Exception ex)
            {
                switch (ex.Message.ToString().Trim())
                {
                    case "Execution Timeout Expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.":
                        Session.Add("Managers", "Notmaintained");
                        Response.Redirect("~/Account/Login.aspx", false);

                        break;

                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
                        break;
                }
            }
        }

        protected void linklblRepPERNR_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Manager_Self_Service/OrgChart.aspx", false);
        }

        protected void GetHRPERNRS()
        {
            string sts = "";
            configurationdalDataContext objAnnouncementDataContext = new configurationdalDataContext();
            objAnnouncementDataContext.usp_CheckHR(Session["CompCode"].ToString(), HttpContext.Current.User.Identity.Name, ref sts);
            if (sts.Trim().ToUpper() == "TRUE")
            {
                MenuMSS.Visible = true;
                Mega_mgrapprvl.Visible = true;
                myrequests.Visible = true;
                MenuPRStatusforPT.Visible = true;
                //MenuFinanceViewIExpenseStatus.Visible = false;
            }

        }

        //protected void btnImgUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Response.Redirect("~/UI/Personal_Information/personal_data.aspx", true);

        //    }
        //    catch (Exception ex)
        //    { }
        //}

        protected void A1_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Session.RemoveAll();
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            Response.Redirect("~/Account/Login.aspx", false);
            Response.End();
        }

        protected void imgEmp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("~/UI/Personal_Information/personal_data.aspx", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnImgUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/UI/Personal_Information/personal_data.aspx", true);
            }
            catch (Exception ex)
            {
            }
        }


        //public void LoadNotifications()
        //{
        //    try
        //    {
        //        bool? fbplock = false;
        //        FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
        //        if (!string.IsNullOrEmpty(Session["sEmploreeId"].ToString()))
        //        {
        //            objDataContext.usp_Fbp_GetLockStatus(Session["sEmploreeId"].ToString(), ref fbplock);

        //            if (fbplock == false)
        //            {
        //                ltFBPLockStatus.Text = "FBP Declaration has been Unlocked. You can declare FBP.";
        //            }
        //            else if (fbplock == true)
        //            {
        //                ltFBPLockStatus.Text = "FBP Declaration has been locked. Please contact Payroll Admin.";
        //            }


        //            bool? itlock = false;
        //            ITdalDataContext objDataContext1 = new ITdalDataContext();
        //            objDataContext1.usp_IT_GetLockStatus(Session["sEmploreeId"].ToString(), ref itlock);

        //            if (itlock == false)
        //            {
        //                ltITLockStatus.Text = "IT Declaration has been Unlocked. You can declare IT.";
        //            }
        //            else if (itlock == true)
        //            {
        //                ltITLockStatus.Text = "IT Declaration has been locked. Please contact Payroll Admin.";
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}

        protected void Check_ITSlab(string PERNR)
        {
            try
            {
                // string a = HttpContext.Current.Request.Url.AbsolutePath;
                if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/UI/IT/Income_Tax.aspx")
                    || HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/UI/FBP/FBP.aspx")
                    || HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/UI/FBP/FBP_PendingClaims.aspx")
                    || HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/UI/FBP/FBP_Apply_ViewClaimsNew.aspx"))
                {
                    List<ITbo> ITboList = new List<ITbo>();
                    ITbl itobjBL = new ITbl();
                    ITboList = itobjBL.Get_IT_Slab_details(0, PERNR, 1);
                    if (ITboList.Count == 0 || ITboList[0].ITSLAB == 0)
                    {
                        MPE_Pend.Show();
                    }
                    else if (ITboList[0].ITSLAB == 2)
                    {
                        if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/UI/IT/"))
                        {
                            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Not Eligible for IT investment declaration. Since You have opted for New tax regime !'); parent.location.href='../Default.aspx'", true);
                            Response.Write("<script language='javascript'>alert('Not Eligible for IT investment declaration. Since You have opted for New tax regime !');location.href='../Default.aspx';</script>");
                        }
                        else if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/UI/FBP/"))
                        {



                            Response.Write("<script language='javascript'>alert('Not Eligible for FBP Claim. Since You have opted for New tax regime !');location.href='../Default.aspx';</script>");
                            //ScriptManager.RegisterStartupScript(this, GetType(), "Key", "<script language='javascript'>alert('Your Message');location.href='../Default.aspx';</script>)", true);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupScript", "alert('Warning :You have opted FBP slab under the new tax regime !!'); parent.location.href='../Default.aspx'", true);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Image changed Successfully!!');", true);
                            //Response.Redirect("~/UI/Default.aspx", false);
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        //protected void btnSetITSlab_Click(object sender, EventArgs e)
        //{
        //    ITbl itobjBL = new ITbl();
        //    bool? status = false;
        //    itobjBL.IT_Set_Details(0, HttpContext.Current.User.Identity.Name, 1, int.Parse(rbtnSlabSelection.SelectedValue), ref status);
        //    string oldnewregime = rbtnSlabSelection.SelectedItem.Text;

        //    if (status == true)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have opted for " + oldnewregime + " regime');", true);//Tax regime is set for Your ID!!');", true);
        //        Response.Redirect("~/" + HttpContext.Current.Request.Url.AbsolutePath, false);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Something Wrong!');", true);
        //        Response.Redirect("~/UI/Default.aspx", false);
        //    }
        //}
        protected void txtmainSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
       
            }
            catch (Exception ex)
            {
            }

        }

    }
}
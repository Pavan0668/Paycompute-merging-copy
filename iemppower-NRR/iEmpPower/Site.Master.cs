using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Configuration;
using System.Web.Security;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service;
using iEmpPower.Old_App_Code.iEmpPowerDAL.User_Account;

namespace iEmpPower
{
    public partial class Site : System.Web.UI.MasterPage
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
            toyear.Text = System.DateTime.Today.Year.ToString();
            
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

            }
            else
            {
                MenuMSS.Visible = false;

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
                        lName.Text = objBo.DESCRIPTION + " | " + emplogin.ToString();
                        spnCompCode.InnerHtml = objBo.Company_Code.Trim() == memUser.ToString() ? "" : "Company Code : " + objBo.Company_Code.Trim();
                        Session.Add("CompLogin", objBo.iscomp);
                        Session.Add("isDR", objBo.isDR);
                        imgCustomerLogo.ImageUrl = objBo.CLOGO;
                        string sEmailId = memUser.Email.ToString();
                        Session["Empname"] = objBo.DESCRIPTION;
                        divemail.InnerText = sEmailId; //memUser.Email;

                        Session.Add("sEmploreeId", memUser.ToString());
                        Session.Add("EmployeeName", objBo.DESCRIPTION);
                        Session.Add("CompCode", objBo.Company_Code.Trim() == "" ? memUser.ToString() : objBo.Company_Code.Trim());
                        if (objBo.EMPLOYEE_PATH.ToString() != "")
                        {
                           
                            imgEmp.ImageUrl = objBo.EMPLOYEE_PATH;
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
            msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Load_emp_Bday();
            if (objPIDashBoardLst.Count > 0)
            {
                lblPrefix.Visible = true;
                dataBday.Visible = true;
                lblPrefix.Text = "Many More Happy Returns Of the Day to ";
                dataBday.DataSource = null;
                dataBday.DataBind();
                dataBday.DataSource = objPIDashBoardLst;
                dataBday.DataBind();
            }
            else
            {
                lblPrefix.Visible = false;
                dataBday.Visible = false;
            }
            AnnouncementToolCollectionbo obLstAnnouncement = AnnouncementObjBl.Load_Announcement();

            rep_marq.DataSource = obLstAnnouncement;
            rep_marq.DataBind();

            GV_Announcement.DataSource = obLstAnnouncement;
            GV_Announcement.DataBind();
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
                    MenuConfig.Visible = true;
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
                    pnlotherMenu2.Visible = true;

                    MenuMyaccount.Visible = true;
                   
                }
                else if (Session["CompLogin"].ToString() == "1")
                {

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
                    MenuBP.Visible = true;
                    MenuAdminPayslip.Visible = true;
                    MenuPayslip1.Visible = false;
                    pnlotherMenu2.Visible = true;
                    MenuMaintainUserAccounts.Visible = false;
                    MenuResetEmployeePassword.Visible = false;
                    MenuMyaccount.Visible = true;
                    MenuMaintainUserAccounts.Visible = false;
                    MenuResetEmployeePassword.Visible = false;
                }

                else if (Session["CompLogin"].ToString() == "0")
                {
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
                    MenuAdminPayslip.Visible = false;
                    MenuPayslip1.Visible = true;
                    pnlotherMenu2.Visible = true;
                    Companies.Visible = false;
                    MenuMyaccount.Visible = true;
                    MenuMaintainUserAccounts.Visible = false;
                    MenuResetEmployeePassword.Visible = false;
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
                objAssginTMBo.COMMENTS=Session["CompCode"].ToString();
                msassignedtomecollectionbo objColl = objAssginTMBl.Get_EmployeeDetails(objAssginTMBo);
                if (objColl.Count > 0)
                {
                    foreach (msassignedtomebo obj in objColl)
                    {
                        msassignedtomebo objAssginTMBo1 = new msassignedtomebo();
                        lblRepPERNR.Text = obj.S_NAME;
                    }
                    MenuMSS.Visible = true;
                }

                else
                {
                    divRep.Visible = false;
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
            }
            
        }
       
    }
}
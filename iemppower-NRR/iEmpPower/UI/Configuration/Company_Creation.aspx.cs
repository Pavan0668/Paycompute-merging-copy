using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerMaster;
using iEmpPowerMaster_Load;
namespace iEmpPower.UI.Configuration
{
    public partial class Company_Creation : System.Web.UI.Page
    {
        private string sCreateUserLogPath = ConfigurationManager.AppSettings["CreateUserLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        #region Error Log Object
        iEmpPower_DT_Wizard_Utility ObjLogError = new iEmpPower_DT_Wizard_Utility();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Comp_Types();
                Load_country();
                //Load_states(DDL_Ccountry.SelectedValue.ToString().Trim(), 1);
            }
        }

        public void Load_Comp_Types()
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_ComapnyTypes(1);
                DDL_Ctype.DataSource = objLst;
                DDL_Ctype.DataTextField = "Company_Type_Txt";
                DDL_Ctype.DataValueField = "Company_Type";
                DDL_Ctype.DataBind();
                DDL_Ctype.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
            }
            catch (Exception ex) { }
        }

        public void Load_country()
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_Country(1);
                DDL_Ccountry.DataSource = objLst;
                DDL_Ccountry.DataTextField = "CountryTxt";
                DDL_Ccountry.DataValueField = "Country";
                DDL_Ccountry.DataBind();
                DDL_Ccountry.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
            }
            catch (Exception ex) { }
        }

        public void Load_states(string cntry, int flg)
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_states(cntry, flg);
                DDL_Cstate.DataSource = objLst;
                DDL_Cstate.DataTextField = "StateTxt";
                DDL_Cstate.DataValueField = "State";
                DDL_Cstate.DataBind();
                DDL_Cstate.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
            }
            catch (Exception ex) { }
        }

        protected void DDL_Ccountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_states(DDL_Ccountry.SelectedValue.ToString().Trim(), 1);
        }

        protected void btn_Csave_Click(object sender, EventArgs e)
        {
            try
            {
                string _pathdb = "";
                string ap = Path.GetExtension(flLogo.FileName);
                if (ap != "")
                {
                    string _path = Server.MapPath("~/images/CmpLogo/" + txt_Ccode.Text.Trim() + Path.GetExtension(flLogo.FileName));
                    _pathdb = "~/images/CmpLogo/" + txt_Ccode.Text.Trim() + Path.GetExtension(flLogo.FileName);
                    flLogo.SaveAs(_path);
                }
                configurationbo objBo = new configurationbo();
                objBo.Company_Name = txt_Cname.Text.Trim();
                ViewState["Company_Name"] = txt_Ccode.Text.Trim();
                objBo.Company_Code = txt_Ccode.Text.Trim();
                objBo.Company_Address = txt_Caddress.Text.Trim();
                objBo.Company_ContactNum = Convert.ToDecimal(txt_Ccontctno.Text.Trim());
                objBo.Company_MailID = txt_Cemail.Text.Trim();
                objBo.Company_Type = Convert.ToInt32(DDL_Ctype.SelectedValue.ToString().Trim());
                objBo.Country = DDL_Ccountry.SelectedValue.ToString().Trim();
                objBo.State = DDL_Cstate.SelectedValue.ToString().Trim();
                objBo.District = txtDist.Text.Trim();
                objBo.Pincode = Convert.ToDecimal(txt_Cpincode.Text.Trim());
                objBo.CREATEDBY = User.Identity.Name.Trim();
                objBo.EMPLOYEE_PATH = _pathdb;
                configurationbl objBL = new configurationbl();
                bool? status = false;

                MembershipCreateStatus MuUserStatus = new MembershipCreateStatus();
                string pass = (objBo.Company_Code.ToString() + "abcd@" + objBo.Company_ContactNum);
                MembershipUser MuUser = Membership.CreateUser(objBo.Company_Code.ToString(), pass, objBo.Company_MailID.ToString().ToLower().Trim(), "a", "b", true, out MuUserStatus);
                switch (MuUserStatus)
                {
                    case MembershipCreateStatus.DuplicateEmail:
                        ObjLogError.LogError(sCreateUserLogPath, objBo.Company_MailID.ToString().ToLower().Trim() + " Email already exist.");
                        break;
                    case MembershipCreateStatus.DuplicateProviderUserKey:
                        break;
                    case MembershipCreateStatus.DuplicateUserName:
                        ObjLogError.LogError(sCreateUserLogPath, objBo.Company_Code.ToString().ToString().Trim() + " User already exist.");
                        break;
                    case MembershipCreateStatus.InvalidAnswer:
                        break;
                    case MembershipCreateStatus.InvalidEmail:
                        ObjLogError.LogError(sCreateUserLogPath, objBo.Company_MailID.ToString().ToLower().Trim() + " Email is not valid.");
                        break;
                    case MembershipCreateStatus.InvalidPassword:
                        break;
                    case MembershipCreateStatus.InvalidProviderUserKey:
                        break;
                    case MembershipCreateStatus.InvalidQuestion:
                        break;
                    case MembershipCreateStatus.InvalidUserName:
                        ObjLogError.LogError(sCreateUserLogPath, objBo.Company_Code.ToString().ToLower().Trim() + " UserName is not valid.");
                        break;
                    case MembershipCreateStatus.ProviderError:
                        break;
                    case MembershipCreateStatus.Success:
                        objBL.CompanyCreate(objBo, 1, ref status);
                        if (status == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('New Company Creation is Successfull..')", true);

                            clearFeilds();
                            string[] MsgCC = { };

                            //--------------------------- SENDING EMAIL NOTIFICATION - TO USERS ---------------------------------------
                            string Mailbody = string.Empty;
                            string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/CreateUser.html");
                            Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                            masterbl.SendMail(objBo.Company_MailID.ToString(), MsgCC, "Welcome to IEmppower - PayCompute, Company account login details."
                              , Mailbody.Replace("##EMPPERNR##", objBo.Company_Code.ToString().ToString()).Replace("##PASSWORD##", pass.Replace("##EMAILID##", objBo.Company_MailID.ToString())));

                            string a = "~/Salary_Reports/Allemp_Payslip/" + ViewState["Company_Name"].ToString().Trim();

                            bool exists = System.IO.Directory.Exists(Server.MapPath(a));
                            if (!exists)
                                System.IO.Directory.CreateDirectory(Server.MapPath(a));

                            string a7 = "~/Salary_Reports/ESI/" + ViewState["Company_Name"].ToString().Trim();
                            bool exists1 = System.IO.Directory.Exists(Server.MapPath(a7));
                            if (!exists1)
                                System.IO.Directory.CreateDirectory(Server.MapPath(a7));

                            string a1 = "~/Salary_Reports/LWP/" + ViewState["Company_Name"].ToString().Trim();
                            bool exists2 = System.IO.Directory.Exists(Server.MapPath(a1));
                            if (!exists2)
                                System.IO.Directory.CreateDirectory(Server.MapPath(a1));

                            string a2 = "~/Salary_Reports/PaySlips/" + ViewState["Company_Name"].ToString().Trim();
                            bool exists3 = System.IO.Directory.Exists(Server.MapPath(a2));
                            if (!exists3)
                                System.IO.Directory.CreateDirectory(Server.MapPath(a2));

                            string a3 = "~/Salary_Reports/PF/" + ViewState["Company_Name"].ToString().Trim();
                            bool exists4 = System.IO.Directory.Exists(Server.MapPath(a3));
                            if (!exists4)
                                System.IO.Directory.CreateDirectory(Server.MapPath(a3));

                            string a6 = "~/Salary_Reports/PT/" + ViewState["Company_Name"].ToString().Trim();
                            bool exists5 = System.IO.Directory.Exists(Server.MapPath(a6));
                            if (!exists5)
                                System.IO.Directory.CreateDirectory(Server.MapPath(a6));

                            string a4 = "~/Salary_Reports/SalaryRegister/" + ViewState["Company_Name"].ToString().Trim();
                            bool exists6 = System.IO.Directory.Exists(Server.MapPath(a4));
                            if (!exists6)
                                System.IO.Directory.CreateDirectory(Server.MapPath(a4));

                            string a5 = "~/Salary_Reports/TDS/" + ViewState["Company_Name"].ToString().Trim();
                            bool exists7 = System.IO.Directory.Exists(Server.MapPath(a5));
                            if (!exists7)
                                System.IO.Directory.CreateDirectory(Server.MapPath(a5));


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error in Company Creation or Same company code is occured!!..')", true);
                            clearFeilds();
                        }
                        ObjLogError.LogError(sCreateUserLogPath, objBo.Company_Code.ToString().ToString().ToLower().Trim() + " - " + objBo.Company_MailID.ToString().ToLower().Trim() + " - User created successfully.");
                        break;
                    case MembershipCreateStatus.UserRejected:
                        ObjLogError.LogError(sCreateUserLogPath, objBo.Company_Code.ToString().ToString().ToLower().Trim() + " User rejected.");
                        break;
                    default:
                        ObjLogError.LogError(sCreateUserLogPath, objBo.Company_Code.ToString().ToString().ToLower().Trim() + " - " + objBo.Company_MailID.ToString().ToLower().Trim() + " - Unknown Error.");
                        break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
            }

        }

        protected void btn_Ccancel_Click(object sender, EventArgs e)
        {
            clearFeilds();
        }

        protected void clearFeilds()
        {
            try
            {
                txt_Caddress.Text = "";
                txt_Ccode.Text = "";
                txt_Ccontctno.Text = "";
                txt_Cemail.Text = "";
                txt_Cname.Text = "";
                txt_Cpincode.Text = "";
                txtDist.Text = "";
                DDL_Ccountry.SelectedIndex = -1;
                DDL_Cstate.SelectedIndex = -1;
                DDL_Ctype.SelectedIndex = -1;

            }
            catch (Exception ex) { }
        }
    }
}
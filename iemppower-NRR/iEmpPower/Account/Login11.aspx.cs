using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Drawing;

namespace iEmpPower.Account
{
    public partial class LoginNew : System.Web.UI.Page
    {
        protected MembershipUser membershipUser;

        TextBox UserNameCtrl = new TextBox(); // User name textbox control in asp login control.
        TextBox PasswordCtrl = new TextBox(); // Password textbox control in asp login control
        CheckBox RememberMeCtrl = new CheckBox(); // RememberMe checkbox control in as login control.

        string NewResetPassword;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // DirectoryInfo dirPath = new DirectoryInfo(@"I:\iEmpPower_Tempfiles\");
                DirectoryInfo dirPath = new DirectoryInfo(@"D:\iEmpPower_Tempfiles\");
                if (!Directory.Exists(dirPath.ToString()))
                {
                    Directory.CreateDirectory(dirPath.ToString());
                }
                DirectoryInfo dirChildPath = new DirectoryInfo(dirPath + DateTime.Now.Date.ToString("dd-MMM-yyyy"));
                if (!Directory.Exists(dirChildPath.ToString()))
                {
                    Directory.CreateDirectory(dirChildPath.ToString());
                }
                Process[] p = Process.GetProcessesByName("EXCEL");
                foreach (Process pp in p)
                {
                    //pp.Kill();
                    //pp.Close();
                    //pp.Dispose();
                }

                try
                {
                    // string directory = @"I:\iEmpPower_Tempfiles\";
                    string directory = ConfigurationManager.AppSettings["iEmpPower_Tempfiles"].ToString();

                    DirectoryInfo dir = new DirectoryInfo(directory);
                    foreach (DirectoryInfo d in dir.GetDirectories())
                    {

                        if (!d.FullName.Contains(DateTime.Now.ToString("dd-MMM-yyyy")))
                        {
                            //DeleteDirectoryDirs(d.ToString());
                            DeleteDirectoryFiles(d.FullName.ToString());
                            while (Directory.Exists(d.FullName.ToString()))
                            {

                                DeleteDirectoryDirs(d.FullName.ToString());

                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                }
            }
            // SetFocus(Login1);
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            //DataTable loginDetails = new DataTable();
            //DataSet DS = new DataSet();
            //DS.ReadXml(Server.MapPath("~/login_credentials.xml"));
            //if (DS.Tables.Count > 0)
            //{
            //    loginDetails = DS.Tables[0].Clone();

            //    foreach (DataRow row in DS.Tables[0].Rows)
            //    {
            //        if (row[0].ToString().ToLower() == UserName.ToString().ToLower())
            //        {
            //            loginDetails.ImportRow(row);

            //            Session.Add("logindetails", loginDetails);
            //            AuthenticateUser();
            //        }
            //    }
            //}
            try
            {
                using (SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString))
                {
                    if (scon.State != ConnectionState.Open)
                    {
                        if (scon.State != ConnectionState.Open)
                        { scon.Open(); }


                        DataTable loginDetails = new DataTable();
                        DataSet DS = new DataSet();
                        DS.ReadXml(Server.MapPath("~/login_credentials.xml"));
                        if (DS.Tables.Count > 0)
                        {
                            loginDetails = DS.Tables[0].Clone();

                            foreach (DataRow row in DS.Tables[0].Rows)
                            {
                                if (row[0].ToString().ToLower() == Login1.UserName.ToString().ToLower())
                                {
                                    loginDetails.ImportRow(row);

                                    Session.Add("logindetails", loginDetails);
                                    AuthenticateUser(Login1.UserName.ToString(), Login1.Password);
                                }
                            }
                        }
                    }
                }
                //if (!string.IsNullOrEmpty(UserName.Text) && !string.IsNullOrEmpty(Password.Text))
                //{
                //    if (AuthenticateUser(UserName.Text, Password.Text))
                //    {
                //        Response.Redirect("~/Default.aspx?employeenumber=" + UserName.Text + "&supervisor=0", false);
                //    }
                //}

            }
            catch (SqlException ex1)
            {
                Response.Redirect("~/Error.aspx", false);
            }

            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx", false);
            }

        }
        private void AuthenticateUser(string UserName, string PassWord)
        {
            try
            {
                DataTable loginDetails = (DataTable)Session["logindetails"];
                DataRow dRow = loginDetails.Rows[0];
                if (dRow[1].ToString() == PassWord)
                {
                   // Response.Redirect("~/Default.aspx?employeenumber=" + dRow[3].ToString() + "&supervisor=" + dRow[4].ToString(), false);
                    Response.Redirect("~/Dashboard.aspx?employeenumber=" + dRow[3].ToString() + "&supervisor=" + dRow[4].ToString());
                }

                //if (Membership.ValidateUser(UserName, PassWord))
                //{ return true; }
                //else
                //{ return false; }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }
        private static void DeleteDirectoryDirs(string target_dir)
        {
            System.Threading.Thread.Sleep(100);

            if (Directory.Exists(target_dir))
            {

                string[] dirs = Directory.GetDirectories(target_dir);

                if (dirs.Length == 0)
                    Directory.Delete(target_dir, false);
                else
                    foreach (string dir in dirs)
                        DeleteDirectoryDirs(dir);
            }
        }


        private static void DeleteDirectoryFiles(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectoryFiles(dir);
            }
        }

        protected void LbtnResetPw_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Login1.UserName))
                {
                    if (Login1.UserName.Length == 8)
                    {//string a = Membership.GetUser(Login1.UserName).UserName;
                        if (Membership.Providers["iEmpPowerAdminSqlMembershipProvider"].GetUser(Login1.UserName, false) != null)
                        {
                            if (!string.IsNullOrEmpty(Membership.Providers["iEmpPowerAdminSqlMembershipProvider"].GetUser(Login1.UserName, false).UserName))
                            {
                                MembershipUser MemUser = Membership.Providers["iEmpPowerAdminSqlMembershipProvider"].GetUser(Login1.UserName, false);
                                if (MemUser.IsLockedOut)
                                { MemUser.UnlockUser(); }
                                NewResetPassword = MemUser.ResetPassword();
                                if (NewResetPassword != null)
                                {
                                    string Email = MemUser.Email.Substring(0, 8);
                                    int ID = 0;
                                    if (int.TryParse(Email, out  ID))
                                    {
                                        Email = MemUser.Email.Substring(8);
                                    }
                                    else
                                    {
                                        Email = MemUser.Email;
                                    }
                                    iEmpPowerMaster_Load.masterbl.DispatchMail(Email, MemUser.UserName, MemUser.UserName + " - Employee Password reset.", "", GetEmailBody(MemUser.UserName, NewResetPassword));
                                    ErrMsg("New Password has been sent to your Email. Please use that password to login.", Color.White);
                                }
                                else
                                { ErrMsg("Password reset failed. Please re-enter your values and try again.", Color.White); }
                            }
                            else
                            { ErrMsg("Invalid User ID", Color.White); }
                        }
                        else
                        { ErrMsg("Invalid User ID", Color.White); }
                    }
                    else
                    { ErrMsg("Invalid User ID", Color.White); }
                }
                else
                { ErrMsg("Invalid User ID", Color.White); }
            }
            catch (Exception Ex)
            { ErrMsg(Ex.Message, Color.White); }
        }

        private string GetEmailBody(string PERNR, string Password)
        {
            try
            {
                string Mailbody = string.Empty;
                string EmpUploadFilePath = Server.MapPath(@"~" + "/EmailTemplates/ResetPassword.html");
                Mailbody = System.IO.File.ReadAllText(EmpUploadFilePath);
                Mailbody = Mailbody.Replace("##MAILTYP##", "RESET PASSWORD BY EMPLOYEE");
                Mailbody = Mailbody.Replace("##EMPPERNR##", PERNR);
                Mailbody = Mailbody.Replace("##NEWPASSWORD##", Password);
                Mailbody = Mailbody.Replace("##FROMDT##", DateTime.Now.ToString("dd-MMMM-yyyy HH:mm:ss"));

                return Mailbody;
            }
            catch (Exception Ex)
            { ErrMsg(Ex.Message, Color.White); return string.Empty; }
        }


        private void ErrMsg(string Msg, Color Clr)
        {
            try
            {
                using (Label LblMsg = (Label)Login1.FindControl("LblMsg"))
                {
                    LblMsg.Text = string.Empty;
                    LblMsg.Text = Msg;
                    LblMsg.ForeColor = Clr;
                }
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                //using (TextBox TxtUserName = (TextBox)Login1.FindControl("UserName"))
                //using (TextBox TxtPassword = (TextBox)Login1.FindControl("Password"))
                //{
                //    if (TxtUserName != null && TxtPassword != null)
                //    {
                //        TxtUserName.Text = "";
                //        TxtPassword.Text = "";
                //    }
                //}


                ((TextBox)Login1.FindControl("UserName")).Text = "";
                Login1.UserName = "";
                //((TextBox)Login1.FindControl("Password")).Text = "";

                //Login1.Password = "";

            }
            catch (Exception Ex)
            { ErrMsg(Ex.Message, Color.White); }

        }
    }
}
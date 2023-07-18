using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Web.Security;
using System.Configuration;
using System.Drawing;

public partial class login : System.Web.UI.Page
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
            DirectoryInfo dirPath = new DirectoryInfo(ConfigurationManager.AppSettings["FilePath"].ToString());
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
                //foreach (FileInfo fi in dir.GetFiles(DateTime.Now.ToString("dd-MMM-yyyy")))
                //{
                //    if (fi.FullName.Contains(User.Identity.Name))
                //    {
                //        fi.Delete();
                //    }
                //}
            }
            catch (Exception Ex)
            {
            }
        }
        SetFocus(Login1);
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        //if (Membership.ValidateUser(Login1.UserName.ToString().ToLower(), Login1.Password.ToString()))
        //{
        //    FormsAuthentication.SetAuthCookie(Login1.UserName.ToString().ToLower(), true);
        
        //        FormsAuthentication.RedirectFromLoginPage(
        //            Login1.UserName.ToString().ToLower(), false);
        //}
        //else
        //{
        //    //messageLabel.Text = "Invalid user name/password " +
        //    //    "combination provided.";
        //}
        //// After the user is validated, send his employee number and his supervisor ID 
        //DataTable loginDetails = new DataTable();
        //DataSet DS = new DataSet();
        //DS.ReadXml(Server.MapPath("~/login_credentials.xml"));
        //if (DS.Tables.Count > 0)
        //{
        //    loginDetails = DS.Tables[0].Clone();

        //    foreach (DataRow row in DS.Tables[0].Rows)
        //    {
        //        if (row[0].ToString().ToLower() == Login1.UserName.ToString().ToLower())
        //        {
        //            loginDetails.ImportRow(row);

        //            Session.Add("logindetails", loginDetails);
        //            //AuthenticateUser();
        //            Response.Redirect("~/Default.aspx?username=" + row[0].ToString() +"&employeenumber=" + row[3].ToString() + "&supervisor=" + row[4].ToString());
        //        }
        //    }
        //}

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
                    AuthenticateUser();
                }
            }
        }
    }

    private void AuthenticateUser()
    {
        DataTable loginDetails = (DataTable)Session["logindetails"];

        DataRow dRow = loginDetails.Rows[0];
        if (dRow[1].ToString() == Login1.Password.ToString())
        {
            Response.Redirect("~/Default.aspx?employeenumber="+dRow[3].ToString()+"&supervisor="+dRow[4].ToString());
        }
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
                                ErrMsg("New Password has been sent to your Email. Please use that password to login.", Color.Blue);
                            }
                            else
                            { ErrMsg("Password reset failed. Please re-enter your values and try again.", Color.Red); }
                        }
                        else
                        { ErrMsg("Invalid User ID", Color.Red); }
                    }
                    else
                    { ErrMsg("Invalid User ID", Color.Red); }
                }
                else
                { ErrMsg("Invalid User ID", Color.Red); }
            }
            else
            { ErrMsg("Invalid User ID", Color.Red); }
        }
        catch (Exception Ex)
        { ErrMsg(Ex.Message, Color.Red); }
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
        { ErrMsg(Ex.Message, Color.Red); return string.Empty; }
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Configuration;

public partial class UI_UserAccount_passwordrecovery : System.Web.UI.Page
{
    protected MembershipUser u;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessageBoard.Visible = false;
        pnlPasswordReset.Visible = true;
        pnlLogin.Visible = false;
        if (!Membership.EnablePasswordReset)
        {
            FormsAuthentication.RedirectToLoginPage();
        }

        Msg.Text = "";

        if (!IsPostBack)
        {
            Msg.Text = "Enter your User Name to receive your password.";
            Msg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            VerifyUsername();
            
        }
    }
    public  string PasswordHasher(string Password)
    {
       return FormsAuthentication.HashPasswordForStoringInConfigFile(Password, FormsAuthPasswordFormat.SHA1.ToString());
    }
    public void VerifyUsername()
    {
        u = Membership.GetUser(UsernameTextBox.Text, false);

        if (u == null)
        {
            Msg.ForeColor = System.Drawing.Color.Red;
            Msg.Text = "Username " + Server.HtmlEncode(UsernameTextBox.Text) + " not found. Please check the value and re-enter.";

            QuestionLabel.Text = "";
            QuestionLabel.Enabled = false;
            AnswerTextBox.Enabled = false;
            ResetPasswordButton.Enabled = false;
        }
        else
        {
            string sPassword = u.PasswordQuestion;
           
            if (sPassword == "a")
            {
                Msg.ForeColor = System.Drawing.Color.Red;
                Msg.Text = "Your question and answer are not set,Please contact your system administrator. ";

                QuestionLabel.Text = "";
                QuestionLabel.Enabled = false;
                AnswerTextBox.Enabled = false;
                ResetPasswordButton.Enabled = false;
            }
            else
            {
                Msg.ForeColor = System.Drawing.Color.Green;
                Msg.Text = "Identity Confirmation Answer the following question to receive your password. ";
                QuestionLabel.Text = u.PasswordQuestion;
                QuestionLabel.Enabled = true;
                AnswerTextBox.Enabled = true;
                ResetPasswordButton.Enabled = true;
            }
        }
    }
    public void ResetPassword_OnClick(object sender, EventArgs args)
    {
        string newPassword;
        string sEmailId;
        MembershipUser memUser;
        //MembershipProvider hh ;
        //string gg = hh.GetPassword(UsernameTextBox.Text, "");
        memUser = Membership.Providers["iEmpPowerAdminSqlMembershipProvider"].GetUser(UsernameTextBox.Text.Trim(), true);
        u = Membership.GetUser(UsernameTextBox.Text, false);
       // string test = u.GetPassword(AnswerTextBox.Text);

        if (u == null)
        {
            Msg.Text = "Username " + Server.HtmlEncode(UsernameTextBox.Text) + " not found. Please check the value and re-enter.";
            Msg.ForeColor = System.Drawing.Color.Red;
            return;
        }

        try
        {
       // string aa=   Membership.Provider.GetPassword(UsernameTextBox.Text, AnswerTextBox.Text);
            newPassword = u.ResetPassword(AnswerTextBox.Text);
        }
        catch (MembershipPasswordException e)
        {
            Msg.Text = "Invalid password answer. Please re-enter and try again.";
            Msg.ForeColor = System.Drawing.Color.Red;
            return;
        }
        catch (Exception e)
        {
            Msg.Text = e.Message;
            return;
        }

        if (newPassword != null)
        {
            sEmailId = u.Email;
            Msg.Text = "Password has been reset sucessfully and sent to your mail box.";
            Msg.ForeColor = System.Drawing.Color.Green;
            iEmpPowerMaster_Load.masterbl.PasswordReset_SentMail(UsernameTextBox.Text, sEmailId, newPassword, "Employee");
            pnlPasswordReset.Visible = false;
            pnlLogin.Visible = true;
        }
        else
        {
            Msg.Text = "Password reset failed. Please re-enter your values and try again.";
            Msg.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/Login.aspx");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/Login.aspx");
    }
   
}
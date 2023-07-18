using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Drawing;
using System.Data;

public partial class Account_ResetEmployeePassword : System.Web.UI.Page
{
    protected MembershipUser MemUser;
    string[] MsgCC = { };
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                PageLoadEvents();
                ClearFields();
                BindGV_Users();
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

    private void ClearFields()
    {
        try
        {
            TxtEmpId.Text = string.Empty;
            TxtEmpIdSearch.Text = string.Empty;
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
            ClearFields();
            MsgCls(string.Empty, LblMsg, Color.Transparent);
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Load GV_Users
    private void BindGV_Users()
    {
        try
        {
            using (DataTable Dt = new DataTable())
            {
                Dt.Columns.Add("UserID", typeof(Guid));
                Dt.Columns.Add("UserName", typeof(string));
                Dt.Columns.Add("Email", typeof(string));

                if (Dt.Rows.Count == 0)
                { GVNodata(GV_Users, Dt); }
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Reset Password Event
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(TxtEmpId.Text.Trim()))
            {
                MemUser = Membership.Providers["iEmpPowerAdminSqlMembershipProvider"].GetUser(TxtEmpId.Text.Trim(), false);
                if (MemUser != null)
                {
                    string NewPassword, EmailID;
                    //------- RESET PASSWORD ------
                    if (MemUser.IsLockedOut)
                    { MemUser.UnlockUser(); }
                    NewPassword = MemUser.ResetPassword();
                    EmailID = MemUser.Email;
                    //----------------  

                    MsgCls(NewPassword + " is the new password and the same is sent to " + TxtEmpId.Text.Trim() + "'s mail box.", LblMsg, Color.Green);
                    ClearFields();
                    //iEmpPowerMaster_Load.masterbl.PasswordReset_SentMail(TxtEmpId.Text, EmailID, NewPassword);

                    iEmpPowerMaster_Load.masterbl.SendMail(EmailID, MsgCC, "Employee Password Reset - " + MemUser.UserName, GetEmailBody(MemUser.UserName, NewPassword));

                }
                else
                { MsgCls("Invalid PERNR, Please check the PERNER and re-enter. !", LblMsg, Color.Red); }
            }
            else { MsgCls("Password reset failed. Please re-enter your values and try again.", LblMsg, Color.Red); }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }

        #region Commented


        //string newPassword;
        //string sEmailId;
        //memUser = Membership.Providers["iEmpPowerAdminSqlMembershipProvider"].GetUser(TxtEmpId.Text.Trim(), false);

        //if (memUser == null)
        //{
        //    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
        //    lblMessageBoard.Text = "Username " + Server.HtmlEncode(TxtEmpId.Text) + " not found. Please check the PERNER and re-enter.";
        //    return;
        //}
        //else if (memUser.IsLockedOut)
        //{
        //    memUser.UnlockUser();
        //}

        //try
        //{
        //    newPassword = memUser.ResetPassword();
        //}
        //catch (MembershipPasswordException ex)
        //{
        //    ex.Message.ToString();
        //    return;
        //}
        //catch (Exception ex)
        //{
        //    lblMessageBoard.Text = ex.Message;
        //    return;
        //}

        //if (newPassword != null)
        //{
        //    sEmailId = memUser.Email;
        //    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
        //    lblMessageBoard.Text = newPassword + " is the new password and the same is sent to " + TxtEmpId.Text + "'s mail box.";
        //    iEmpPowerMaster_Load.masterbl.PasswordReset_SentMail(TxtEmpId.Text, sEmailId, newPassword);
        //}
        //else
        //{
        //    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
        //    lblMessageBoard.Text = "Password reset failed. Please re-enter your values and try again.";
        //}
        #endregion
    }
    #endregion

    #region Get Email Body
    private string GetEmailBody(string PERNR, string Password)
    {
        try
        {
            string Mailbody = string.Empty;
            string EmpUploadFilePath = Server.MapPath(@"~" + "/EmailTemplates/ResetPassword.html");
            Mailbody = System.IO.File.ReadAllText(EmpUploadFilePath);

            Mailbody = Mailbody.Replace("##MAILTYP##", "RESET PASSWORD BY ADMIN");

            Mailbody = Mailbody.Replace("##EMPPERNR##", PERNR);
            Mailbody = Mailbody.Replace("##NEWPASSWORD##", Password);
            Mailbody = Mailbody.Replace("##FROMDT##", DateTime.Now.ToString("dd-MMMM-yyyy HH:mm:ss"));

            return Mailbody;
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); return string.Empty; }
    }
    #endregion

    //---------------- SEARCH EMPLOYEE ----------------

    #region Employee Search
    protected void BtnEmpSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(TxtEmpIdSearch.Text.Trim()))
            {
                MemUser = Membership.Providers["iEmpPowerAdminSqlMembershipProvider"].GetUser(TxtEmpIdSearch.Text.Trim(), false);
                if (MemUser != null)
                {
                    using (DataTable Dt = new DataTable())
                    {
                        Dt.Columns.Add("UserID", typeof(Guid));
                        Dt.Columns.Add("UserName", typeof(string));
                        Dt.Columns.Add("Email", typeof(string));


                        Dt.Rows.Add(MemUser.ProviderUserKey, MemUser.UserName, MemUser.Email.ToLower().Trim());
                        if (Dt.Rows.Count > 0)
                        {
                            GV_Users.DataSource = Dt;
                            GV_Users.DataBind();
                        }
                    }
                }
                else
                {
                    MsgCls("No User found with ID - " + TxtEmpIdSearch.Text.Trim(), LblMsg, Color.Red);
                    BindGV_Users();
                }
            }
            else
            {
                MsgCls("User Id cannot be blank !", LblMsg, Color.Red);
                BindGV_Users();
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region GV_Users Events
    protected void GV_Users_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            switch (e.CommandName.ToUpper())
            {
                case "RESET":
                    string UserName = GV_Users.DataKeys[int.Parse(e.CommandArgument.ToString())]["UserName"].ToString();

                    using (CheckBox ChkSendMail = (CheckBox)GV_Users.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("ChkSendEmail"))
                    using (TextBox TxtNewPassword = (TextBox)GV_Users.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("TxtUserNewPassword"))
                    {
                        if (ChkSendMail != null && TxtNewPassword != null)
                        {
                            if (!string.IsNullOrEmpty(TxtNewPassword.Text.Trim()))
                            {
                                MemUser = Membership.Providers["iEmpPowerAdminSqlMembershipProvider"].GetUser(UserName, false);
                                if (MemUser != null)
                                {
                                    string EmailID;
                                    //------- RESET PASSWORD ------
                                    if (MemUser.IsLockedOut)
                                    { MemUser.UnlockUser(); }
                                    MemUser.ChangePassword(MemUser.ResetPassword(), TxtNewPassword.Text.Trim());
                                    EmailID = MemUser.Email;
                                    //----------------  
                                    if (ChkSendMail.Checked)
                                    {
                                        MsgCls(TxtNewPassword.Text.Trim() + " is the new password and the same is sent to " + UserName + "'s mail box.", LblMsg, Color.Green);
                                        iEmpPowerMaster_Load.masterbl.SendMail(EmailID, MsgCC, "Employee Password Reset - " + MemUser.UserName, GetEmailBody(UserName, TxtNewPassword.Text.Trim()));
                                    }
                                    else
                                    { MsgCls(TxtNewPassword.Text.Trim() + " is the new password for the employee " + UserName, LblMsg, Color.Green); }
                                    TxtNewPassword.Text = string.Empty;
                                    ClearFields();
                                    GV_Users.DataSource = null;
                                    GV_Users.DataBind();
                                    //iEmpPowerMaster_Load.masterbl.PasswordReset_SentMail(TxtEmpId.Text, EmailID, NewPassword);



                                }
                            }
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

    protected void GV_Users_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GV_Users_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void GV_Users_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    #endregion

}
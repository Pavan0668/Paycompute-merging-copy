using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UI_UserAccount_createuseraccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("SuperUser"))
        {
            Response.Redirect("~/UnauthorizedAccess.aspx");
        }
        lblMessageBoard.Text = "";
        if (!IsPostBack)
        {
            bool bIsSave = true;
            Session.Add("IsSave", bIsSave);
            chkbxActiveUser.Checked = true;
        }
        txtSearchUserName.Attributes.Add("onkeyup", "ClientSearchUserNameChanged()");
        txtUserName.Attributes.Add("onkeyup", "ClientUserNameChanged()");
        txtPassword.Attributes.Add("onkeyup", "ClientPasswordChanged()");
        txtConfirmPassword.Attributes.Add("onkeyup", "ClientConfirmPasswordChanged()");
        txtConfirmPassword.Attributes.Add("OnChange", "ClientConfirmPasswordTabout()");
        txtEmail.Attributes.Add("onkeyup", "ClientEmailChanged()");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bIsSave = (bool)Session["IsSave"];
        if (bIsSave)
        {

            MembershipCreateStatus objCreateStatus = new MembershipCreateStatus();

            MembershipUser objUser = Membership.CreateUser(txtUserName.Text.Trim(),
                                                            txtPassword.Text.Trim(), txtEmail.Text.Trim(), "a", "b",
                                                            (chkbxActiveUser.Checked) ? true : false, out objCreateStatus);
            switch (objCreateStatus)
            {
                case MembershipCreateStatus.Success:
                    // Super user role is created thorugh asp.net configuration.
                    // If user is marked as Super user, then write in aspnet_UsersInRoles table
                    if (chkbxSuperUser.Checked)
                    {
                        string newUserName = txtUserName.Text.Trim();
                        MembershipUser myObject = Membership.GetUser(newUserName);
                        string UserID = myObject.ProviderUserKey.ToString();
                        createuseraccountbo objUserAccountBo = new createuseraccountbo();
                        createuseraccountbl objUserAccountBl = new createuseraccountbl();
                        objUserAccountBo.USERID = new Guid(UserID);
                        int ErrorCodeResult =
                        objUserAccountBl.Save_Super_User_Type(objUserAccountBo);
                    }
                    lblMessageBoard.Text = GetLocalResourceObject("AccountCreated").ToString();
                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    ClearControls();
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    lblMessageBoard.Text = GetLocalResourceObject("UserNameAlreadyExists").ToString();
                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    // txtUserName.Text = "";
                    txtUserName.Focus();
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    lblMessageBoard.Text = GetLocalResourceObject("InvalidPassword").ToString();
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    txtPassword.Text = "";
                    txtPassword.Focus();
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    lblMessageBoard.Text = GetLocalResourceObject("UserEmailAlreadyExists").ToString();
                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    //txtEmail.Text = "";
                    txtEmail.Focus();
                    break;

                case MembershipCreateStatus.InvalidEmail:
                    lblMessageBoard.Text = GetLocalResourceObject("InvalidEmailAddress").ToString();
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    // txtEmail.Text = "";
                    txtEmail.Focus();
                    break;
                default:
                    lblMessageBoard.Text = GetLocalResourceObject("UnknownError").ToString();
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    break;
            }
        }
        else
        {
            GridViewRow grdRow = (GridViewRow)Session["currentSelectedRow"];  //grdGrade.SelectedRow;

            string strUserName = (grdRow.Cells[0].Text.ToString().Trim());
            string strUserOLdEmailID = grdRow.Cells[1].Text.ToString().Trim();
            string strUserNewEmailID = txtEmail.Text.ToString().Trim();
            bool bActiveUser;
            // Disable user name in the details section.
            txtUserName.Enabled = false;
            if (chkbxActiveUser.Checked)
            {
                bActiveUser = true;
            }
            else
            {
                bActiveUser = false;
            }
            
            // Update selected user details in the membership table
            MembershipUserCollection allUsers = null;
            allUsers = Membership.GetAllUsers();
            if (string.Compare(strUserOLdEmailID,strUserNewEmailID) != 0)
            {
                foreach (MembershipUser mUser in allUsers)
                {
                    if (string.Compare(txtEmail.Text.Trim(), mUser.Email) == 0)
                    {
                        lblMessageBoard.Text = GetLocalResourceObject("UserEmailAlreadyExists").ToString();
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        txtEmail.Focus();
                        return;
                    }

                }
            }
            MembershipUser myObject = Membership.GetUser(strUserName);
            myObject.Email = txtEmail.Text.Trim();
            myObject.IsApproved = bActiveUser;
            Membership.UpdateUser(myObject);
            // Check if Super user checkbox marking changed.
            int iResult = (int)Session["iResult"];
            // Case of non super user being marked as super user.
            // In that case, make an entry in aspnet_UsersInRoles table as during save.
            string UserID = myObject.ProviderUserKey.ToString();
            createuseraccountbo objUserAccountBo = new createuseraccountbo();
            createuseraccountbl objUserAccountBl = new createuseraccountbl();
            objUserAccountBo.USERID = new Guid(UserID);
            if (iResult == 0 && chkbxSuperUser.Checked == true)
            {
                objUserAccountBl.Save_Super_User_Type(objUserAccountBo);
            }
            // Case of Super user being changed as non super user.
            // In that case, delete entry from aspnet_UsersInRoles table.
            if (iResult == 1 && chkbxSuperUser.Checked == false)
            {
                objUserAccountBl.Delete_Super_User_Type(objUserAccountBo);
            }
            lblMessageBoard.Text = GetLocalResourceObject("UpdateSuccess").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
            SearchUserName();
            ClearControls();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchUserName(); 
    }
    protected void SearchUserName()
    {
        string sUserName = txtSearchUserName.Text.Trim();
        object bFindUser = Membership.GetUser(sUserName);
        if (bFindUser == null)
        {
            lblMessageBoard.Text = GetLocalResourceObject("NoMatchingRecordFound").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
            grdUsers.DataSource = null;
            grdUsers.DataBind();
            return;
        }
        grdUsers.DataSource = Membership.FindUsersByName(sUserName);
        grdUsers.DataBind();
    }
    protected void grdUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
         BindControlsForUpdation();
    }
    protected void BindControlsForUpdation()
    {
        GridViewRow grdRow = grdUsers.SelectedRow;
        CheckBox chk;
        Session.Add("currentSelectedRow", grdRow);
        txtUserName.Enabled = false;
        string strUserName = (grdRow.Cells[0].Text.ToString().Trim());
        string strEmail = (grdRow.Cells[1].Text.ToString().Trim());
        chk = (CheckBox)(grdRow.FindControl("CheckBox1"));
        lblMessageBoard.Text = string.Empty;
        txtUserName.Text = strUserName;
        //labelPassword.Visible = false;
        txtPassword.Visible = false;
        txtConfirmPassword.Visible = false;
        //lblConfirmPassword.Visible = false;
        txtEmail.Text = strEmail;
        chkbxActiveUser.Checked = chk.Checked;

        // Get if searched user is a super user.
        MembershipUser myObject = Membership.GetUser(strUserName);
        string UserID = myObject.ProviderUserKey.ToString();
        createuseraccountbo objUserAccountBo = new createuseraccountbo();
        createuseraccountbl objUserAccountBl = new createuseraccountbl();
        objUserAccountBo.USERID = new Guid(UserID);
        int iResult = objUserAccountBl.Get_Super_User_Type(objUserAccountBo);
        Session.Add("iResult", iResult);
        if (iResult == 1)
        {
            chkbxSuperUser.Checked = true;
        }
        else
        {
            chkbxSuperUser.Checked = false;
        }
        bool bIsSave = (bool)Session["IsSave"];
        bIsSave = false;
        Session.Add("IsSave", bIsSave);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }
    protected void ClearControls()
    {
        // Reset grid selected index to -1 and add this value to View state to use it in page index change event.
        grdUsers.SelectedIndex = -1;
        ViewState.Add("indexchang", -1);
        txtUserName.Enabled = true;
        txtUserName.Text = string.Empty;
        txtPassword.Visible = true;
        //labelPassword.Visible = true;
        txtPassword.Text = string.Empty;
        txtConfirmPassword.Visible = true;
        //lblConfirmPassword.Visible = true;
        txtConfirmPassword.Text = "";
        txtEmail.Text = string.Empty;
        chkbxActiveUser.Checked = true;
        bool bIsSave = true;
        Session.Add("IsSave", bIsSave);
        chkbxActiveUser.Checked = true;
        chkbxSuperUser.Checked = false;
    }
    protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdUsers, "Select$" + e.Row.RowIndex);
        }
    }
}
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


public partial class UI_UserAccount_createquestionandanswer : System.Web.UI.Page
{
    protected MembershipUser memUser;

    protected void Page_Load(object sender, EventArgs e)
    {

        memUser = Membership.GetUser();
        if (!this.IsPostBack)
        {
            HideTabs();
            view2.Visible = true;
            Tab2.CssClass = "nav-link active p-2";
        }
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool bValidatePassowrd = Membership.ValidateUser(memUser.ToString(), txtPassword.Text);
            if (bValidatePassowrd == true)
            {

                //MembershipUser u = Membership.GetUser(strUserName);
                Boolean result = memUser.ChangePasswordQuestionAndAnswer(txtPassword.Text.Trim(),
                                                     txtSecurityQuestion.Text.Trim(),
                                                     txtSecurityAnswer.Text.Trim());

                lblMessageBoard.Text = GetLocalResourceObject("SavedSucess").ToString();
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                lblMessageBoard.Text = GetLocalResourceObject("Failed").ToString();
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                return;
            }
        }
        catch
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnknownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
        ClearControls();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }
    protected void ClearControls()
    {
        txtSecurityAnswer.Text = "";
        txtSecurityQuestion.Text = "";
    }

    protected void HideTabs()
    {
        
        Tab1.CssClass = "nav-link  p-2";
        Tab2.CssClass = "nav-link  p-2";

    }

    protected void Tab1_Click(object sender, EventArgs e)
    {
        HideTabs();
        Response.Redirect("~/UI/UserAccount/changepassword.aspx", true);
        Tab1.CssClass = "nav-link active p-2";
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        HideTabs();
        view2.Visible = true;
        Tab2.CssClass = "nav-link active p-2";
    }
}
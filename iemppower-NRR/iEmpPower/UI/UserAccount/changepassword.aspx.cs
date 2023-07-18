using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_UserAccount_changepassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            HideTabs();
            view1.Visible = true;
            Tab1.CssClass = "nav-link active p-2";
        }
    }

    protected void HideTabs()
    {
        view1.Visible = false;
        Tab1.CssClass = "nav-link  p-2";
        Tab2.CssClass = "nav-link  p-2";

    }

    protected void Tab1_Click(object sender, EventArgs e)
    {
        HideTabs();
        view1.Visible = true;
        Tab1.CssClass = "nav-link active p-2";
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        HideTabs();
        view1.Visible = false;
        Response.Redirect("~/UI/UserAccount/createquestionandanswer.aspx", true);
        Tab2.CssClass = "nav-link active p-2";
    }
}
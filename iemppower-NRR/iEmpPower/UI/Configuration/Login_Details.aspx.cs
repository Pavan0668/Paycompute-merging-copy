using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_Configuration_Login_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Load_LastLoginGridDetails();
    }

    protected void grdLastLogin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageindex = e.NewPageIndex;
        grdLastLogin.PageIndex = e.NewPageIndex;

        configurationcollectionbo objLastLoginLst = (configurationcollectionbo)Session["objLastLoginLst"];
        grdLastLogin.DataSource = objLastLoginLst;
        grdLastLogin.DataBind();
    }

    protected void Load_LastLoginGridDetails()
    {
        try
        {
            configurationbl objLastLoginBl = new configurationbl();
            configurationcollectionbo objLastLoginLst = objLastLoginBl.Get_LastLoginDetails();
            if (objLastLoginLst.Count <= 0)
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = GetLocalResourceObject("NoMatchingRecordFound").ToString();
            }
            grdLastLogin.DataSource = objLastLoginLst;
            grdLastLogin.DataBind();
            Session.Add("objLastLoginLst", objLastLoginLst);
        }
        catch (Exception ex)
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;            
            //lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
        }
    }
}
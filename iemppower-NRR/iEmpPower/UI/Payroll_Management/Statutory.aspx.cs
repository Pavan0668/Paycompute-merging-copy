using System;
using System.IO;
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
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.Drawing.Design;

namespace iEmpPower.UI.Payroll_Management
{
    public partial class Statutory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            if (!IsPostBack)
            {
                drpdwnMonth.SelectedValue = +0+(DateTime.Now.Month - 1).ToString().Trim();
                drpdwnYear.ClearSelection();
                string year = DateTime.Now.ToString("yyyy");
                drpdwnYear.Items.FindByText(year.ToString()).Selected = true;
                ifrm.Visible = false;
               // btnPrint.Visible = false;
                //btnPrint.Enabled = false;
                this.Page.Form.DefaultButton = Button1.UniqueID;
            }
        }
        protected void btnGeneratePaySlip_Click(object sender, EventArgs e)
        {
            try
            {
                string sFileNAme="";

                sFileNAme = Page.ResolveUrl("~/Salary_Reports/" + ddlReportType.SelectedItem.Text.Trim() + "/" + Session["CompCode"].ToString() + "/" + drpdwnMonth.SelectedValue.ToString().Trim() + "-" + drpdwnYear.SelectedItem.ToString().Trim() + ".pdf ");
                string sFileNAme1 = Server.MapPath("~/Salary_Reports/" + ddlReportType.SelectedItem.Text.Trim() + "/" + Session["CompCode"].ToString() + "/" + drpdwnMonth.SelectedValue.ToString().Trim() + "-" + drpdwnYear.SelectedItem.ToString().Trim() + ".pdf ");
                if (File.Exists(sFileNAme1))
                {
                    //btnPrint.Visible = true;
                    ifrm.Visible = true;
                    ifrm.Attributes.Add("src", sFileNAme);
                    //btnPrint.Enabled = true;
                }
                else
                {
                    ifrm.Visible = false;
                    lblMessageBoard.Text = "No matching details found for the selected Report Typre,year and month.";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }
    }
}
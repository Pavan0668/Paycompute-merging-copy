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

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class AdminPaySlip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            if (!IsPostBack)
            {
                ifrm.Visible = false;
                drpdwnYear.Focus();
                drpdwnMonth.SelectedValue = DateTime.Now.ToString("MM");
            }
        }


        protected void btnGeneratePaySlip_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["EmpID"] = DDLEmpList.Text.Split('-')[0];
                string strPath = ViewState["EmpID"].ToString().Trim() + drpdwnYear.SelectedItem.ToString().Trim() + drpdwnMonth.SelectedValue.ToString().Trim() + ".pdf";
                string sFileNAme = Page.ResolveUrl("Exported/Payslip/" + strPath);
                string sFileNAme1 = Server.MapPath("~/UI/Benefits_Payment/Exported/Payslip/" + strPath);
                //if(File.Exists("~/UI/Benefits_Payment/Exported/Payslip/" + strPath))
                if (File.Exists(sFileNAme1))
                {
                    ifrm.Visible = true;
                    ifrm.Attributes.Add("src", sFileNAme);
                    // btnPrint.Enabled = true;
                }
                else
                {
                    ifrm.Visible = false;
                    //btnPrint.Enabled = false;
                    lblMessageBoard.Text = "No matching details found for the selected year and month.";
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
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
using System.Collections.Generic;


namespace iEmpPower.UI.Payroll_Management
{
    public partial class Payslip_Emp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            if (!IsPostBack)
            {
                //btnPrint.Visible = false;
                ifrm.Visible = false;
                yrload();
                //btnPrint.Enabled = false;
                //this.Page.Form.DefaultButton = Button1.UniqueID;
                

            }
        }

        public void yrload()
        {
            try
            {

                for (int i = DateTime.Now.Year; i >= DateTime.Now.AddYears(-4).Year; i--)
                {
                    drpdwnYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                drpdwnYear.Focus();
                drpdwnMonth.SelectedValue = DateTime.Now.AddMonths(-1).ToString("MM");
                drpdwnYear.SelectedValue = DateTime.Now.ToString("YYYY");

            }
            catch (Exception ex)
            {

            }
        }


        protected void btnGeneratePaySlip_Click(object sender, EventArgs e)
        {
            try
            {
                string ccode = Session["CompCode"].ToString();
                string emplogin = User.Identity.Name;

                int cnt = ccode.Length;

                emplogin = emplogin.Substring(cnt);


                string strPath = Session["CompCode"].ToString() + "/" + drpdwnMonth.SelectedValue.ToString().Trim() + "-" + drpdwnYear.SelectedItem.ToString().Trim() + "/" + Session["EmployeeName"].ToString().Trim() + "(" + emplogin.ToUpper().Trim() + ").pdf";
                strPath = strPath.Replace("  ", " ");
                string sFileNAme = Page.ResolveUrl("~/Salary_Reports/PaySlips/" + strPath);
                sFileNAme = sFileNAme.Replace("  ", " ");
                string sFileNAme1 = Server.MapPath("~/Salary_Reports/PaySlips/" + Session["CompCode"].ToString() + "/" + drpdwnMonth.SelectedValue.ToString().Trim() + "-" + drpdwnYear.SelectedItem.ToString().Trim() + "/" + Session["EmployeeName"].ToString().Trim() + "(" + emplogin.ToUpper().Trim() + ").pdf");
                sFileNAme1 = sFileNAme1.Replace("  ", " ");


                if (File.Exists(sFileNAme1))
                {
                    //btnPrint.Visible = true;
                    ifrm.Visible = true;
                    ifrm.Attributes.Add("src", sFileNAme);



                }
                else
                {
                    ifrm.Visible = false;
                    //btnPrint.Visible = false;

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
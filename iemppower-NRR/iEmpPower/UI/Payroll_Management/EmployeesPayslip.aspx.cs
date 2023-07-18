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
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute.SPayc_Collection_BO;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute;
using iEmpPower.Old_App_Code.iEmpPowerBL.SPaycompute;
using System.Collections.Generic;

namespace iEmpPower.UI.Payroll_Management
{
    public partial class EmployeesPayslip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            lblMessageBoard.Text = "";
            if (!IsPostBack)
            {
                drpdwnMonth.SelectedValue = +0+(DateTime.Now.Month - 1).ToString().Trim();
                drpdwnYear.ClearSelection();
                string year_ps = DateTime.Now.ToString("yyyy");
                drpdwnYear.Items.FindByText(year_ps.ToString()).Selected = true;

                ifrm.Visible = false;
                //btnPrint.Enabled = false;
                this.Page.Form.DefaultButton = Button1.UniqueID;

                bind_emp_DDL();
            }
           
        }


        public void bind_emp_DDL()
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.set_createdbyDDL("", "", "", Session["CompCode"].ToString(), 5);
                DDL_Empname_payslip.DataSource = null;
                DDL_Empname_payslip.DataBind();

                DDL_Empname_payslip.DataSource = objspaylst;
                DDL_Empname_payslip.DataTextField = "col1";
                DDL_Empname_payslip.DataValueField = "TXT";
                DDL_Empname_payslip.DataBind();
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
                string emplogin = DDL_Empname_payslip.SelectedValue.ToString().Trim();

                int cnt = ccode.Length;
               
                emplogin = emplogin.Substring(cnt);
                string strPath = Session["CompCode"].ToString() + "/" + drpdwnMonth.SelectedValue.ToString().Trim() + "-" + drpdwnYear.SelectedItem.ToString().Trim() + "/" + DDL_Empname_payslip.SelectedItem.ToString().Trim() + "(" + emplogin.ToUpper().Trim() + ").pdf";
                strPath = strPath.Replace("  ", " ");
                string sFileNAme = Server.MapPath("~/Salary_Reports/PaySlips/" + Session["CompCode"].ToString() + "/" + drpdwnMonth.SelectedValue.ToString().Trim() + "-" + drpdwnYear.SelectedItem.ToString().Trim() + "/" + DDL_Empname_payslip.SelectedItem.ToString().Trim() + "(" + emplogin.ToUpper().Trim() + ").pdf");
                sFileNAme = sFileNAme.Replace("  ", " ");
                if (File.Exists(sFileNAme))
                {
                    ifrm.Visible = true;
                    ifrm.Attributes.Add("src", "/Salary_Reports/PaySlips/"+strPath);
                    lblMessageBoard.Text = "";


                    //Response.AppendHeader("content-disposition", "attachment; filename=" + strPath);
                    //Response.ContentType = "Application/pdf";
                    //Response.TransmitFile(sFileNAme);
                    //Response.End();
                }



                else
                {
                    lblMessageBoard.Text = "File does not exists";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = ex.Message;////GetLocalResourceObject("UnkownError").ToString();
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }
    }
}
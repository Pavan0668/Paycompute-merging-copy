using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace iEmpPower.UI.Payroll_Management
{
    public partial class SalaryRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
            {
            drpdwnMonth.SelectedValue =+0+(DateTime.Now.Month-1).ToString().Trim();
            drpdwnYear.ClearSelection();
            string year = DateTime.Now.ToString("yyyy");
            drpdwnYear.Items.FindByText(year.ToString()).Selected = true;
            }
        }
        protected void btngenerate_Click(object sender, EventArgs e)
        {
            try
            {
               
                string month = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
                string strPath = "";
                string sFileNAme = "";
                strPath = Session["CompCode"].ToString() + "/" + drpdwnMonth.SelectedValue.ToString() + "-" + drpdwnYear.SelectedItem.ToString() + ".pdf";                 
                 sFileNAme = Server.MapPath("~/Salary_Reports/SalaryRegister/" + Session["CompCode"].ToString() + "/" + drpdwnMonth.SelectedValue.ToString() + "-" + drpdwnYear.SelectedItem.ToString() + ".pdf");
                if (File.Exists(sFileNAme))
                {
                    ifrm.Visible = true;
                    ifrm.Attributes.Add("src", "/Salary_Reports/SalaryRegister/" + strPath);
                    lblMessageBoard.Text = "";

                    //Response.AppendHeader("content-disposition", "attachment; filename=" + strPath);////"SalaryRegister201511.xls");
                   
                    
                    //Response.TransmitFile(sFileNAme);////"SalaryRegister.xls");
                    //Response.End();
                   
                }

                else
                {
                    lblMessageBoard.Text = "File does not exists";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    ifrm.Visible = false;
                }
            }

            catch (Exception ex)
            {
                lblMessageBoard.Text = ex.Message;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
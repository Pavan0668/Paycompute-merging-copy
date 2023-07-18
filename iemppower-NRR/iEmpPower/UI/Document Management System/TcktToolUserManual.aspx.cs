using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.Document_Management_System
{
    public partial class TcktToolUserManual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            if (!IsPostBack)
            {
                lblMessageBoard.Text = "";
                if (!IsPostBack)
                {
                   // UserManualpdf();
                     int parsedValue;
                     if (!int.TryParse(HttpContext.Current.User.Identity.Name, out parsedValue))
                     {
                         if (User.Identity.Name.ToString().ToLower() != "cssteam")
                         {
                             UserManualpdfforClient();
                         }
                         else
                         {
                             UserManualpdf();
                         }
                     }
                     else
                     {
                         UserManualpdf();
                     }
                }
            }
        }

        public void UserManualpdf()
        {
            try
            {

                string strPath = "TicketingTool_User_Manual" + ".pdf";
                string sFileNAme = Page.ResolveUrl("Documents/" + strPath);
                string sFileNAme1 = Server.MapPath("~/UI/Document Management System/Documents/" + strPath);
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
                    lblMessageBoard.Text = "No found.";
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


        public void UserManualpdfforClient()
        {
            try
            {

                string strPath = "TicketingTool_UserManual_Clients" + ".pdf";
                string sFileNAme = Page.ResolveUrl("Documents/" + strPath);
                string sFileNAme1 = Server.MapPath("~/UI/Document Management System/Documents/" + strPath);
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
                    lblMessageBoard.Text = "No found.";
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
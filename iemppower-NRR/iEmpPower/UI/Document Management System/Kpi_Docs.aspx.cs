using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.Document_Management_System
{
    public partial class Kpi_Docs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_empList();
                ddlEmp.SelectedValue = User.Identity.Name.ToString().Trim();
                UserManualdocs(Get_Desig(ddlEmp.SelectedValue.ToString().Trim()));
            }
        }

        public void Load_empList()
        {
            try
            {
                msassignedtomebl objBl = new msassignedtomebl();
                List<msassignedtomebo> boList = new List<msassignedtomebo>();

                boList = objBl.Get_KPIEmp(User.Identity.Name.ToString().Trim());
                if (boList == null || boList.Count == 0)
                {
                    ddlEmp.Visible = false;
                    ddlEmp.DataSource = null;
                    ddlEmp.DataBind();
                    return;
                }
                else
                {
                    ddlEmp.DataSource = null;
                    ddlEmp.DataBind();
                    ddlEmp.DataSource = boList;
                    ddlEmp.DataTextField = "ENAME";
                    ddlEmp.DataValueField = "PERNR";
                    ddlEmp.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void UserManualdocs(string desig)
        {
            try
            {
                string strPath = desig.Trim() + ".pdf";
                string sFileNAme = Page.ResolveUrl("Documents/" + strPath);
                string sFileNAme1 = Server.MapPath("~/UI/Document Management System/Documents/" + strPath);
                //if(File.Exists("~/UI/Benefits_Payment/Exported/Payslip/" + strPath))
                if (File.Exists(sFileNAme1))
                {
                    ifrm.Visible = true;
                    ifrm.Attributes.Add("src", sFileNAme);//+ "&embedded=true"
                    // btnPrint.Enabled = true;
                    lblMessageBoard.Text = "";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;
                }
                else
                {
                    ifrm.Visible = false;
                    //btnPrint.Enabled = false;
                    lblMessageBoard.Text = "File not found..!";
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

        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserManualdocs(Get_Desig(ddlEmp.SelectedValue.ToString().Trim()));
        }

        public string Get_Desig(string PERNR)
        {
            msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Designation(ddlEmp.SelectedValue.ToString().Trim());
            if (objPIDashBoardLst.Count > 0)
            {
                foreach (var a in objPIDashBoardLst)
                {
                    ViewState["desig"] = a.DESIG;
                }

            }
            return ViewState["desig"].ToString();
        }
    }
}
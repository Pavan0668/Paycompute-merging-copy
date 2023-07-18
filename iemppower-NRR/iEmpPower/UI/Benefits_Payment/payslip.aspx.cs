using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Security;
using System.Drawing;
using System.Text;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;

public partial class UI_Working_Time_payslip : System.Web.UI.Page
{

    #region Page_Init
    public void Page_Init(object o, EventArgs e)
    {
        try
        {
            if (Session["sEmploreeId"] == null)
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx", false);
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                GetYears();
                PageLoadEvents();

            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    #region User Defined Methods
    private void GVNodata(GridView GV, DataTable Dt)
    {
        try
        {
            Dt.Rows.Add(Dt.NewRow());
            GV.DataSource = Dt;
            GV.DataBind();
            GV.Rows[0].Visible = false;
            GV.Rows[0].Controls.Clear();
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    private void MsgCls(string Msg, Label Lbl, Color Clr)
    {
        try
        {
            Lbl.Text = string.Empty;
            Lbl.Text = Lbl.Text + Msg;
            Lbl.ForeColor = Clr;
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Page Load Events
    private void PageLoadEvents()
    {
        try
        {
            BindGV_Payslip(int.Parse(DDLPayslipyear.SelectedValue));
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Bind GV_Payslip Information
    private void BindGV_Payslip(int Year)
    {
        try
        {
            iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO.payslipbo objPayBo = new iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO.payslipbo();
            payslipbl objPayBl = new payslipbl();

            objPayBo.Pernr = User.Identity.Name;
            objPayBo.Year = Year;
            objPayBo.FilePath = Server.MapPath("~/UI/Benefits_Payment/Exported/Payslip");
            objPayBo.Flag = 1;

            payslipcollectionbo objPIAddBoLst = objPayBl.Get_Payslip(objPayBo);
            if (objPIAddBoLst.Count > 0)
            {
                MsgCls(string.Empty, LblMsg, Color.White);
                GV_Payslip.DataSource = objPIAddBoLst;
                GV_Payslip.DataBind();
            }
            else
            {
                DataTable Dt = new DataTable();
                Dt.Columns.Add("RowNumber", typeof(int));
                Dt.Columns.Add("FileName", typeof(string));
                Dt.Columns.Add("FilePath", typeof(string));

                GVNodata(GV_Payslip, Dt);
                MsgCls("No records found !", LblMsg, Color.Red);
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region Bind DDLPayslipyear
    private void GetYears()
    {
        try
        {
            int CurrentYear = DateTime.Now.Year;
            for (int i = 0; i <= 1; i++)
            {
                DDLPayslipyear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            DDLPayslipyear.SelectedValue = CurrentYear.ToString();
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region DDLPayslipyear Events
    protected void DDLPayslipyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGV_Payslip(int.Parse(DDLPayslipyear.SelectedValue));
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }
    #endregion

    #region GV_Payslip Events
    protected void GV_Payslip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName.ToUpper())
            {
                case "DOWNLOAD":
                    string FileName = GV_Payslip.DataKeys[int.Parse(e.CommandArgument.ToString())]["FileName"].ToString();




                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("content-disposition", string.Format("attachment; filename={0}{1}",FileName, ".pdf"));
                    byte[] data = req.DownloadData(Server.MapPath("~/UI/Benefits_Payment/Exported/Payslip/" + FileName + ".pdf"));
                    response.BinaryWrite(data);
                    response.End();





                    break;
                default:
                    break;
            }
        }
        catch (Exception Ex)
        { MsgCls(Ex.Message, LblMsg, Color.Red); }
    }

    protected void GV_Payslip_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GV_Payslip_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void GV_Payslip_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    #endregion
}
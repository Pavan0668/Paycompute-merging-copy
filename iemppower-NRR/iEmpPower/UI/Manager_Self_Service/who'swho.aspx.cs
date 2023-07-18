using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service;

public partial class UI_Manager_Self_Service_who_swho : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadGridDetails();
            txtEmployeNmae.Focus();
            HeadCount();
        }
    }
    protected void grdWhoIsWho_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblMessageBoard.Text = "";
        int pageindex = e.NewPageIndex;
        grdWhoIsWho.PageIndex = e.NewPageIndex;

        picommunicationinformationcollectionbo objWhoIsWhoLst = (picommunicationinformationcollectionbo)Session["objWhoIsWhoLst"];
        grdWhoIsWho.DataSource = objWhoIsWhoLst;
        grdWhoIsWho.DataBind();

        string frow = "", lrow = "";  ////Row count

        foreach (GridViewRow row in grdWhoIsWho.Rows)
        {
            for (int i = 0; i < grdWhoIsWho.Rows.Count; i++)
            {
                Label lblRowNumber = (Label)grdWhoIsWho.Rows[i].FindControl("lblRowNumber");
                if (i == 0)
                {
                    frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                }
                if (i == grdWhoIsWho.Rows.Count - 1)
                {
                    lrow = lblRowNumber.Text;
                }
            }
        }
        divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objWhoIsWhoLst.Count + " entries";
        divcnt.Visible = grdWhoIsWho.Rows.Count > 0 ? true : false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGridDetails();
    }
    protected void LoadGridDetails()
    {
        try
        {
            picommunicationinformationbo objWhoIsWhoBo = new picommunicationinformationbo();
            picommunicationinformationbl objWhoIsWhoBl = new picommunicationinformationbl();
            objWhoIsWhoBo.EMPLOYEE_ID = txtEmployeNmae.Text.Trim();
            objWhoIsWhoBo.DESIGNATION = "";// txtDesignation.Text.Trim();
            picommunicationinformationcollectionbo objWhoIsWhoLst = objWhoIsWhoBl.Who_Is_Who_Details(Session["CompCode"].ToString(), 1, objWhoIsWhoBo);
            if (objWhoIsWhoLst.Count <= 0)
            {
                grdWhoIsWho.DataSource = null;
                grdWhoIsWho.DataBind();
                grdWhoIsWho.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = GetLocalResourceObject("NoMatchingRecordFound").ToString();

            }
            else
            {
                grdWhoIsWho.Visible = true;
                lblMessageBoard.Text = "";
                grdWhoIsWho.DataSource = objWhoIsWhoLst;
                grdWhoIsWho.DataBind();
                Session.Add("objWhoIsWhoLst", objWhoIsWhoLst);
            }

            string frow = "", lrow = "";  ////Row count

            foreach (GridViewRow row in grdWhoIsWho.Rows)
            {
                for (int i = 0; i < grdWhoIsWho.Rows.Count; i++)
                {
                    Label lblRowNumber = (Label)grdWhoIsWho.Rows[i].FindControl("lblRowNumber");
                    if (i == 0)
                    {
                        frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                    }
                    if (i == grdWhoIsWho.Rows.Count - 1)
                    {
                        lrow = lblRowNumber.Text;
                    }
                }
            }
            divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objWhoIsWhoLst.Count + " entries";
            divcnt.Visible = grdWhoIsWho.Rows.Count > 0 ? true : false;
        }
        catch (Exception ex)
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            txtEmployeNmae.Text = string.Empty;
            //txtDesignation.Text = string.Empty;
            lblMessageBoard.Text = "";
            LoadGridDetails();
            txtEmployeNmae.Focus();
            HeadCount();
        }
        catch (Exception ex)
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
        }
    }

    protected void BtnExporttoXl_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }

    protected void ExportToExcel()
    {
        string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        // Render grid view control.
        //htw.WriteBreak();
        //string colHeads = "Travel claim Details";
        //htw.WriteEncodedText(colHeads);

        grdWhoIsWho.AllowPaging = false;
        LoadGridDetails();
        grdWhoIsWho.Columns[0].Visible = false;
        grdWhoIsWho.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
        grdWhoIsWho.GridLines = GridLines.Both;
        grdWhoIsWho.RenderControl(htw);
        grdWhoIsWho.Columns[0].Visible = true;
        grdWhoIsWho.AllowPaging = true;
        htw.WriteBreak();

        string renderedGridView = "<h4>Employee Details Report</h4>" + "<br>"; //+ sw.ToString();
        renderedGridView += sw.ToString() + "<br/>";
        //Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_EmployeeDetails.xls");
        Response.AppendHeader("content-disposition", "attachment; filename=" + "EmployeeDetails_" + date1 + ".xls");
        Response.ContentType = "Application/vnd.ms-excel";
        Response.Write(renderedGridView);
        Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void HeadCount()
    {
        msassignedtomebl objAssginTMBl = new msassignedtomebl();
        msassignedtomebo objAssginTMBo = new msassignedtomebo();
        msassignedtomedalDataContext objPIAssignTMDataContext = new msassignedtomedalDataContext();
        // objAssginTMBo.PERNR = HttpContext.Current.User.Identity.Name;
        foreach (var i in objPIAssignTMDataContext.usp_get_head_count_mf())
        {
            HFM.Value = i.Male.ToString();
            HFF.Value = i.Female.ToString();
        }

    }

    protected void grdWhoIsWho_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lbl = (Label)e.Row.FindControl("LBL_empid");
                string ccode = Session["CompCode"].ToString();
                string emplogin = e.Row.Cells[1].Text;//lbl.Text;
                int cnt = ccode.Length;
                emplogin = emplogin.Substring(cnt);
                e.Row.Cells[1].Text = emplogin.Trim().ToUpper();
            }
        }
        catch (Exception ex)
        { }
    }
}
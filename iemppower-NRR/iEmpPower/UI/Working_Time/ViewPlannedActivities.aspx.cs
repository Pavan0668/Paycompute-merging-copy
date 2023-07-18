using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.Working_Time
{
    public partial class ViewPlannedActivities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessageBoard.Text = "";
                if (!IsPostBack)
                {
                    DateTime dtSelectedDate = DateTime.Now;
                    DateTime dtStartDate, dtEndDate;
                    GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
                    TxtFromDate.Text = dtStartDate.ToString();
                    TxtToDate.Text = dtEndDate.ToString();
                    DDLEmpList.Focus();
                    CDD_DDLEmpList.SelectedValue = User.Identity.Name;
                }
                this.Form.DefaultButton = this.btnDisplay.UniqueID;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        void GetCurrentWeekDates(DateTime Input, out DateTime Start, out DateTime End)
        {

            if (Input.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                Start = Input;
                End = Start.AddDays(6);
                return;
            }

            while (Input.Date.DayOfWeek != DayOfWeek.Sunday)

                Input = Input.Date.AddDays(-1);
                Start = Input;
                End = Input.AddDays(6);
        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                List<wtrecordworkingtimebo> objBoList = new List<wtrecordworkingtimebo>();
                wtrecordworkingtimebl objBl = new wtrecordworkingtimebl();
                wtrecordworkingtimecollectionbo objLst = new wtrecordworkingtimecollectionbo();

                    DateTime DtFrm = new DateTime(1900, 01, 01);
                    DateTime DtTo = new DateTime(1900, 01, 01);

                    if (DateTime.TryParse(TxtFromDate.Text, out DtFrm))
                    {
                        if (DateTime.TryParse(TxtToDate.Text, out DtTo))
                        {
                            objLst = objBl.LoadViewPlannedActivities(DDLEmpList.SelectedValue.ToString().Trim(), DtFrm, DtTo,User.Identity.Name);
                            if (objLst == null || objLst.Count == 0)
                            {
                                lblMessageBoard.Text = "No Record Found";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                grdPlannedActivities.DataSource = null;
                                grdPlannedActivities.DataBind();
                                grdPlannedActivities.Visible = false;
                                btnExcel.Visible = false;
                            }
                            else
                            {
                                btnExcel.Visible = true;
                                lblMessageBoard.Text = "";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;
                                grdPlannedActivities.Visible = true;
                                grdPlannedActivities.DataSource = null;
                                grdPlannedActivities.DataBind();
                                grdPlannedActivities.DataSource = objLst;
                                grdPlannedActivities.DataBind();

                            }
                        }
                    }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                TxtFromDate.Text = string.Empty;
                TxtToDate.Text = string.Empty;
                grdPlannedActivities.DataSource = null;
                grdPlannedActivities.DataBind();
                btnExcel.Visible = false;
                lblMessageBoard.Text = string.Empty;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                CDD_DDLEmpList.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                funConvertToExcel();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void funConvertToExcel()
        {
           
            string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            string colHeads = "Planned Activities Details";
            htw.WriteEncodedText(colHeads);
            htw.WriteBreak();



            grdPlannedActivities.HeaderRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#F44336");
            grdPlannedActivities.RenderControl(htw);


            htw.WriteBreak();

            // Write the rendered content to a file.
            // string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
            string renderedGridView = sw.ToString() + "<br/>";
            Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + date1 + "_PlndActivities.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

    }
}
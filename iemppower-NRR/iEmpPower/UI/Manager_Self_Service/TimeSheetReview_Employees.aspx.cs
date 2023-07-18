using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service;
using System.Data;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute.SPayc_Collection_BO;
using iEmpPower.Old_App_Code.iEmpPowerBL.SPaycompute;

namespace iEmpPower.UI.Manager_Self_Service
{
    public partial class TimeSheetReview_Employees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";

            if (!IsPostBack)
            {
                //bdpTimeSheetFrom.Focus();
                //TxtFromDate.Focus();
                TxtFromDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).ToString("dd/MM/yyyy");
                TxtToDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("dd/MM/yyyy");
            }
        }


        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                grdTimeSheetReview.DataSource = null;
                grdTimeSheetReview.DataBind();
                lblMessageBoard.Text = "";
               
                DateTime DtFrm = new DateTime(1900, 01, 01);
                DateTime DtTo = new DateTime(1900, 01, 01);

                if (DateTime.TryParse(TxtFromDate.Text, out DtFrm))
                {
                    if (DateTime.TryParse(TxtToDate.Text, out DtTo))
                    {
                        if (DtTo <= DtFrm)
                        {
                            lblMessageBoard.Text = "From date should be less than To date.!!";
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        DataTable dt = new DataTable();
                        dt.Columns.Add("PERNR");
                        dt.Columns.Add("ENAME");
                        dt.Columns.Add("PROJ");
                        dt.Columns.Add("WBS");
                        dt.Columns.Add("Activity");
                        dt.Columns.Add("Attd");
                        dt.Columns.Add("REMARKS");
                        dt.Columns.Add("status");
                        dt.Columns.Add("WORKINGDATE");
                        dt.Columns.Add("CATSHOURS");
                        string strPernr1 = User.Identity.Name;
                        string a = Session["CompCode"].ToString();

                        string strPernr = "";
                        string strEName = "";
                        string strOrderType = "";
                        string strWBS = "";
                        string strActivity = "";
                        string strAbsenceType = "";
                        string strStstus = "";
                        string strWrkngDate = "";
                        string strremarks = "";
                        string strCatshrs = "";
                        divcnt.Visible = true;
                        if (DDL_srchtyp.SelectedValue == "0")
                      {
                        mstimesheetreviewDataContext objTimesheetReviewDataContext = new mstimesheetreviewDataContext();                        
                        foreach (var vRow in objTimesheetReviewDataContext.sp_ms_Load_TimeSheetReview_Employees(strPernr1,a, DtFrm.ToString("yyyy-MM-dd"), DtTo.ToString("yyyy-MM-dd"),DDL_srchtyps.SelectedValue.ToString().Trim(),1))
                        {                           
                            

                            if (vRow.PERNR != null)
                            {
                                strPernr = vRow.PERNR.Trim().ToString();
                            }

                            if (vRow.ENAME != null)
                            {
                                strEName = vRow.ENAME.Trim().ToString();
                            }

                            if (vRow.PROJ != null)
                            {
                                strOrderType = vRow.PROJ.Trim().ToString();
                            }

                            if (vRow.WBS != null)
                            {
                                strWBS = vRow.WBS.Trim().ToString();
                            }

                            if (vRow.Activity != null)
                            {
                                strActivity = vRow.Activity.Trim().ToString();
                            }

                            if (vRow.Attd != null)
                            {
                                strAbsenceType = vRow.Attd.Trim().ToString();
                            }
                            if (vRow.REMARKS != null)
                            {
                                strremarks = vRow.REMARKS.Trim().ToString();
                            }                            
                            if (vRow.status != null)
                            {
                                strStstus = vRow.status.Trim().ToString();
                            }
                            if (vRow.WORKINGDATE != null)
                            {
                                strWrkngDate = vRow.WORKINGDATE.ToString();
                                DateTime dtt = Convert.ToDateTime(strWrkngDate);
                                string str = dtt.ToString("dd-MMM-yyyy");
                                strWrkngDate = str + " / " + dtt.ToString("ddd");
                            }
                            if (vRow.CATSHOURS != null)
                            {
                                strCatshrs = vRow.CATSHOURS.Trim().ToString();
                            }
                            
                            
                            DataRow dr = dt.NewRow();
                            dr["PERNR"] = strPernr;
                            dr["ENAME"] = strEName;
                            dr["PROJ"] = strOrderType;
                            dr["WBS"] = strWBS;
                            dr["Activity"] = strActivity;                           
                            dr["Attd"] = strAbsenceType;
                            dr["REMARKS"] = strremarks;                           
                            dr["status"] = strStstus;
                            dr["WORKINGDATE"] = strWrkngDate;
                            dr["CATSHOURS"] = strCatshrs;
                            dt.Rows.Add(dr);

                        }

                        if (dt.Rows.Count > 0)
                        {
                            btnExcel.Visible = true;
                            grdTimeSheetReview.DataSource = dt;
                            grdTimeSheetReview.DataBind();
                            Bindtotalhrs();
                        }
                        objTimesheetReviewDataContext.Dispose();
                    }

                        else if (DDL_srchtyp.SelectedValue == "1")
                        {
                            mstimesheetreviewDataContext objTimesheetReviewDataContext = new mstimesheetreviewDataContext();
                            foreach (var vRow in objTimesheetReviewDataContext.sp_ms_Load_TimeSheetReview_Employees(strPernr1, a, DtFrm.ToString("yyyy-MM-dd"), DtTo.ToString("yyyy-MM-dd"), DDL_srchtyps.SelectedValue.ToString().Trim(), 2))
                            {


                                if (vRow.PERNR != null)
                                {
                                    strPernr = vRow.PERNR.Trim().ToString();
                                }

                                if (vRow.ENAME != null)
                                {
                                    strEName = vRow.ENAME.Trim().ToString();
                                }

                                if (vRow.PROJ != null)
                                {
                                    strOrderType = vRow.PROJ.Trim().ToString();
                                }

                                if (vRow.WBS != null)
                                {
                                    strWBS = vRow.WBS.Trim().ToString();
                                }

                                if (vRow.Activity != null)
                                {
                                    strActivity = vRow.Activity.Trim().ToString();
                                }

                                if (vRow.Attd != null)
                                {
                                    strAbsenceType = vRow.Attd.Trim().ToString();
                                }
                                if (vRow.REMARKS != null)
                                {
                                    strremarks = vRow.REMARKS.Trim().ToString();
                                }
                                if (vRow.status != null)
                                {
                                    strStstus = vRow.status.Trim().ToString();
                                }
                                if (vRow.WORKINGDATE != null)
                                {
                                    strWrkngDate = vRow.WORKINGDATE.ToString();
                                    DateTime dtt = Convert.ToDateTime(strWrkngDate);
                                    string str = dtt.ToString("dd-MMM-yyyy");
                                    strWrkngDate = str + " / " + dtt.ToString("ddd");
                                }
                                if (vRow.CATSHOURS != null)
                                {
                                    strCatshrs = vRow.CATSHOURS.Trim().ToString();
                                }


                                DataRow dr = dt.NewRow();
                                dr["PERNR"] = strPernr;
                                dr["ENAME"] = strEName;
                                dr["PROJ"] = strOrderType;
                                dr["WBS"] = strWBS;
                                dr["Activity"] = strActivity;
                                dr["Attd"] = strAbsenceType;
                                dr["REMARKS"] = strremarks;
                                dr["status"] = strStstus;
                                dr["WORKINGDATE"] = strWrkngDate;
                                dr["CATSHOURS"] = strCatshrs;
                                dt.Rows.Add(dr);

                            }

                            if (dt.Rows.Count > 0)
                            {
                                btnExcel.Visible = true;
                                grdTimeSheetReview.DataSource = dt;
                                grdTimeSheetReview.DataBind();
                                Bindtotalhrs();
                            }

                            objTimesheetReviewDataContext.Dispose();
                        }

                        else if (DDL_srchtyp.SelectedValue == "2")
                        {
                            mstimesheetreviewDataContext objTimesheetReviewDataContext = new mstimesheetreviewDataContext();
                            foreach (var vRow in objTimesheetReviewDataContext.sp_ms_Load_TimeSheetReview_Employees(strPernr1, a, DtFrm.ToString("yyyy-MM-dd"), DtTo.ToString("yyyy-MM-dd"), DDL_srchtyps.SelectedValue.ToString().Trim(), 3))
                            {


                                if (vRow.PERNR != null)
                                {
                                    strPernr = vRow.PERNR.Trim().ToString();
                                }

                                if (vRow.ENAME != null)
                                {
                                    strEName = vRow.ENAME.Trim().ToString();
                                }

                                if (vRow.PROJ != null)
                                {
                                    strOrderType = vRow.PROJ.Trim().ToString();
                                }

                                if (vRow.WBS != null)
                                {
                                    strWBS = vRow.WBS.Trim().ToString();
                                }

                                if (vRow.Activity != null)
                                {
                                    strActivity = vRow.Activity.Trim().ToString();
                                }

                                if (vRow.Attd != null)
                                {
                                    strAbsenceType = vRow.Attd.Trim().ToString();
                                }
                                if (vRow.REMARKS != null)
                                {
                                    strremarks = vRow.REMARKS.Trim().ToString();
                                }
                                if (vRow.status != null)
                                {
                                    strStstus = vRow.status.Trim().ToString();
                                }
                                if (vRow.WORKINGDATE != null)
                                {
                                    strWrkngDate = vRow.WORKINGDATE.ToString();
                                    DateTime dtt = Convert.ToDateTime(strWrkngDate);
                                    string str = dtt.ToString("dd-MMM-yyyy");
                                    strWrkngDate = str + " / " + dtt.ToString("ddd");
                                }
                                if (vRow.CATSHOURS != null)
                                {
                                    strCatshrs = vRow.CATSHOURS.Trim().ToString();
                                }


                                DataRow dr = dt.NewRow();
                                dr["PERNR"] = strPernr;
                                dr["ENAME"] = strEName;
                                dr["PROJ"] = strOrderType;
                                dr["WBS"] = strWBS;
                                dr["Activity"] = strActivity;
                                dr["Attd"] = strAbsenceType;
                                dr["REMARKS"] = strremarks;
                                dr["status"] = strStstus;
                                dr["WORKINGDATE"] = strWrkngDate;
                                dr["CATSHOURS"] = strCatshrs;
                                dt.Rows.Add(dr);

                            }

                            if (dt.Rows.Count > 0)
                            {
                                btnExcel.Visible = true;
                                grdTimeSheetReview.DataSource = dt;
                                grdTimeSheetReview.DataBind();
                                Bindtotalhrs();
                            }

                            objTimesheetReviewDataContext.Dispose();
                        }

                        else if (DDL_srchtyp.SelectedValue == "3")
                        {
                            mstimesheetreviewDataContext objTimesheetReviewDataContext = new mstimesheetreviewDataContext();
                            foreach (var vRow in objTimesheetReviewDataContext.sp_ms_Load_TimeSheetReview_Employees(strPernr1, a, DtFrm.ToString("yyyy-MM-dd"), DtTo.ToString("yyyy-MM-dd"), DDL_srchtyps.SelectedValue.ToString().Trim(), 4))
                            {


                                if (vRow.PERNR != null)
                                {
                                    strPernr = vRow.PERNR.Trim().ToString();
                                }

                                if (vRow.ENAME != null)
                                {
                                    strEName = vRow.ENAME.Trim().ToString();
                                }

                                if (vRow.PROJ != null)
                                {
                                    strOrderType = vRow.PROJ.Trim().ToString();
                                }

                                if (vRow.WBS != null)
                                {
                                    strWBS = vRow.WBS.Trim().ToString();
                                }

                                if (vRow.Activity != null)
                                {
                                    strActivity = vRow.Activity.Trim().ToString();
                                }

                                if (vRow.Attd != null)
                                {
                                    strAbsenceType = vRow.Attd.Trim().ToString();
                                }
                                if (vRow.REMARKS != null)
                                {
                                    strremarks = vRow.REMARKS.Trim().ToString();
                                }
                                if (vRow.status != null)
                                {
                                    strStstus = vRow.status.Trim().ToString();
                                }
                                if (vRow.WORKINGDATE != null)
                                {
                                    strWrkngDate = vRow.WORKINGDATE.ToString();
                                    DateTime dtt = Convert.ToDateTime(strWrkngDate);
                                    string str = dtt.ToString("dd-MMM-yyyy");
                                    strWrkngDate = str + " / " + dtt.ToString("ddd");
                                }
                                if (vRow.CATSHOURS != null)
                                {
                                    strCatshrs = vRow.CATSHOURS.Trim().ToString();
                                }


                                DataRow dr = dt.NewRow();
                                dr["PERNR"] = strPernr;
                                dr["ENAME"] = strEName;
                                dr["PROJ"] = strOrderType;
                                dr["WBS"] = strWBS;
                                dr["Activity"] = strActivity;
                                dr["Attd"] = strAbsenceType;
                                dr["REMARKS"] = strremarks;
                                dr["status"] = strStstus;
                                dr["WORKINGDATE"] = strWrkngDate;
                                dr["CATSHOURS"] = strCatshrs;
                                dt.Rows.Add(dr);

                            }

                            if (dt.Rows.Count > 0)
                            {
                                btnExcel.Visible = true;
                                grdTimeSheetReview.DataSource = dt;
                                grdTimeSheetReview.DataBind();
                                Bindtotalhrs();
                            }

                            objTimesheetReviewDataContext.Dispose();
                        }

                        else if (DDL_srchtyp.SelectedValue == "4")
                        {
                            mstimesheetreviewDataContext objTimesheetReviewDataContext = new mstimesheetreviewDataContext();
                            foreach (var vRow in objTimesheetReviewDataContext.sp_ms_Load_TimeSheetReview_Employees(strPernr1, a, DtFrm.ToString("yyyy-MM-dd"), DtTo.ToString("yyyy-MM-dd"), DDL_srchtyps.SelectedValue.ToString().Trim(), 5))
                            {
                                if (vRow.PERNR != null)
                                {
                                    strPernr = vRow.PERNR.Trim().ToString();
                                }

                                if (vRow.ENAME != null)
                                {
                                    strEName = vRow.ENAME.Trim().ToString();
                                }

                                if (vRow.PROJ != null)
                                {
                                    strOrderType = vRow.PROJ.Trim().ToString();
                                }

                                if (vRow.WBS != null)
                                {
                                    strWBS = vRow.WBS.Trim().ToString();
                                }

                                if (vRow.Activity != null)
                                {
                                    strActivity = vRow.Activity.Trim().ToString();
                                }

                                if (vRow.Attd != null)
                                {
                                    strAbsenceType = vRow.Attd.Trim().ToString();
                                }
                                if (vRow.REMARKS != null)
                                {
                                    strremarks = vRow.REMARKS.Trim().ToString();
                                }
                                if (vRow.status != null)
                                {
                                    strStstus = vRow.status.Trim().ToString();
                                }
                                if (vRow.WORKINGDATE != null)
                                {
                                    strWrkngDate = vRow.WORKINGDATE.ToString();
                                    DateTime dtt = Convert.ToDateTime(strWrkngDate);
                                    string str = dtt.ToString("dd-MMM-yyyy");
                                    strWrkngDate = str + " / " + dtt.ToString("ddd");
                                }
                                if (vRow.CATSHOURS != null)
                                {
                                    strCatshrs = vRow.CATSHOURS.Trim().ToString();
                                }


                                DataRow dr = dt.NewRow();
                                dr["PERNR"] = strPernr;
                                dr["ENAME"] = strEName;
                                dr["PROJ"] = strOrderType;
                                dr["WBS"] = strWBS;
                                dr["Activity"] = strActivity;
                                dr["Attd"] = strAbsenceType;
                                dr["REMARKS"] = strremarks;
                                dr["status"] = strStstus;
                                dr["WORKINGDATE"] = strWrkngDate;
                                dr["CATSHOURS"] = strCatshrs;
                                dt.Rows.Add(dr);

                            }

                            if (dt.Rows.Count > 0)
                            {
                                btnExcel.Visible = true;
                                grdTimeSheetReview.DataSource = dt;
                                grdTimeSheetReview.DataBind();
                                Bindtotalhrs();
                            }

                            objTimesheetReviewDataContext.Dispose();
                        }
                        else
                        {
                            btnExcel.Visible = false;
                            grdTimeSheetReview.DataSource = null;
                            grdTimeSheetReview.DataBind();
                            lblMessageBoard.Text = "No Records found !!";
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        }


                        string frow = "", lrow = "";  ////Row count

                        foreach (GridViewRow row in grdTimeSheetReview.Rows)
                        {
                            for (int i = 0; i < grdTimeSheetReview.Rows.Count; i++)
                            {
                                Label lblRowNumber = (Label)grdTimeSheetReview.Rows[i].FindControl("lblRowNumber");
                                if (i == 0)
                                {
                                    frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                                }
                                if (i == grdTimeSheetReview.Rows.Count - 1)
                                {
                                    lrow = lblRowNumber.Text;
                                }
                            }
                        }
                        divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + dt.Rows.Count + " entries";
                        divcnt.Visible = grdTimeSheetReview.Rows.Count > 0 ? true : false;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = lblMessageBoard.Text + " Unknown error occured. Please contact your system administrator." + ex.Message;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
            string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            string colHeads = "Record Working Details";
            htw.WriteEncodedText(colHeads);
            htw.WriteBreak();

            grdTimeSheetReview.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
            grdTimeSheetReview.GridLines = GridLines.Both;
            grdTimeSheetReview.RenderControl(htw);   
            htw.WriteBreak();

            // Write the rendered content to a file.
            // string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
            string renderedGridView = sw.ToString() + "<br/>";
            Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + date1 +"_RWT.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();
                
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "Unknown error occured. Please contact your system administrator.";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                TxtFromDate.Text = string.Empty;
                TxtToDate.Text = string.Empty;
                grdTimeSheetReview.DataSource = null;
                grdTimeSheetReview.DataBind();
                btnExcel.Visible = false;
                lblMessageBoard.Text = string.Empty;
                DDL_srchtyps.Visible = false;
                TxtFromDate.Text = "";
                TxtToDate.Text = "";
                DDL_srchtyp.SelectedValue = "0";
                TxtFromDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).ToString("dd/MM/yyyy");
                TxtToDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("dd/MM/yyyy");
                divcnt.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void grdTimeSheetReview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Label lbl = (Label)e.Row.FindControl("LBL_empid");
                string ccode = Session["CompCode"].ToString();
                string emplogin = lbl.Text.ToString();
                int cnt = ccode.Length;
                emplogin = emplogin.Substring(cnt);
                e.Row.Cells[1].Text = emplogin.Trim().ToUpper();
            }
            catch (Exception Ex)
            { }
        }

        protected void DDL_srchtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_srchtyp.SelectedValue=="0")
                {
                    DDL_srchtyps.Visible = false;
                   
                }
                else if (DDL_srchtyp.SelectedValue == "1")
                {
                    DDL_srchtyps.Visible = true;
                    SPayc_Collection_BO objlst = new SPayc_Collection_BO();
                    SPayc_BL bl = new SPayc_BL();
                    objlst = bl.load_prjctwbsact(Session["CompCode"].ToString(), 1);
                    DDL_srchtyps.DataSource = objlst;
                    DDL_srchtyps.DataTextField = "col11";
                    DDL_srchtyps.DataValueField = "id1";
                    DDL_srchtyps.DataBind();
                    DDL_srchtyps.Items.Insert(0, new ListItem("Select", "0"));

                }
                else if (DDL_srchtyp.SelectedValue == "2")
                {
                    DDL_srchtyps.Visible = true;
                    SPayc_Collection_BO objlst = new SPayc_Collection_BO();
                    SPayc_BL bl = new SPayc_BL();
                    objlst = bl.load_prjctwbsact(Session["CompCode"].ToString(), 2);
                    DDL_srchtyps.DataSource = objlst;
                    DDL_srchtyps.DataTextField = "col11";
                    DDL_srchtyps.DataValueField = "id1";
                    DDL_srchtyps.DataBind();
                    DDL_srchtyps.Items.Insert(0, new ListItem("Select", "0"));

                }
                else if (DDL_srchtyp.SelectedValue == "3")
                {
                    DDL_srchtyps.Visible = true;
                    SPayc_Collection_BO objlst = new SPayc_Collection_BO();
                    SPayc_BL bl = new SPayc_BL();
                    objlst = bl.load_prjctwbsact(Session["CompCode"].ToString(), 3);
                    DDL_srchtyps.DataSource = objlst;
                    DDL_srchtyps.DataTextField = "col11";
                    DDL_srchtyps.DataValueField = "id1";
                    DDL_srchtyps.DataBind();
                    DDL_srchtyps.Items.Insert(0, new ListItem("Select", "0"));

                }
                else if (DDL_srchtyp.SelectedValue == "4")
                {
                    DDL_srchtyps.Visible = true;
                    SPayc_Collection_BO objlst = new SPayc_Collection_BO();
                    SPayc_BL bl = new SPayc_BL();
                    objlst = bl.load_prjctwbsact(Session["CompCode"].ToString(), 4);
                    DDL_srchtyps.DataSource = objlst;
                    DDL_srchtyps.DataTextField = "col11";
                    DDL_srchtyps.DataValueField = "id1";
                    DDL_srchtyps.DataBind();
                    DDL_srchtyps.Items.Insert(0, new ListItem("Select", "0"));

                }
            }
            catch (Exception Ex)
            { }
        }


        public void Bindtotalhrs()
        {
            try
            {

                decimal total = 0;
                decimal avg = 0;
                for (int i = 0; i < grdTimeSheetReview.Rows.Count; i++)
                {

                    Label hrs = (Label)grdTimeSheetReview.Rows[i].FindControl("lbl_hrs");
                    decimal calc = Convert.ToDecimal(hrs.Text == "" ? "0.00" : hrs.Text);
                    total = total + calc;

                }
                Label thrs = ((Label)grdTimeSheetReview.FooterRow.FindControl("lbl_totalhrs"));
                thrs.Text = total.ToString() == "0.00" ? "" : total.ToString();

                ViewState["Totalscore"] = thrs.Text;
                int a = grdTimeSheetReview.Rows.Count;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
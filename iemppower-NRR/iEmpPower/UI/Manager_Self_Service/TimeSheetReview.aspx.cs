using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Manager_Self_Service;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute.SPayc_Collection_BO;
using iEmpPower.Old_App_Code.iEmpPowerBL.SPaycompute;

namespace iEmpPower.UI.Manager_Self_Service
{
    public partial class TimeSheetReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";

            if (!IsPostBack)
            {
                HideTabs();
                view1.Visible = true;
                Tab1.CssClass = "nav-link active p-2";
                string id = string.Empty;
                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"];
                }

                LoadEmployySubOrdinates();
                if (id == "ATRV")
                {
                    DDLEmpList.Focus();
                    DDLEmpList.SelectedValue = "2";

                    DDL_lvemp.Focus();
                    DDL_lvemp.SelectedValue = "2";
                }
                else
                {
                    DDLEmpList.Focus();
                    DDLEmpList.SelectedValue = User.Identity.Name;

                    DDL_lvemp.Focus();
                    DDL_lvemp.SelectedValue = User.Identity.Name;
                }
                TxtFromDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).ToString("dd/MM/yyyy");
                TxtToDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("dd/MM/yyyy");

                txt_lvfrmdate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).ToString("yyyy-MM-dd");
                txt_lvtodate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            this.Form.DefaultButton = this.btnDisplay.UniqueID;
            
        }



        protected void LoadEmployySubOrdinates()
        {
            msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            objPIDashBoardBo.COMMENTS = Session["CompCode"].ToString();
            objPIDashBoardBo.PERNR = User.Identity.Name;
            msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_Manager_Details(objPIDashBoardBo);

            DDLEmpList.DataSource = objPIDashBoardLst;
            DDLEmpList.DataTextField = "ENAME";
            DDLEmpList.DataValueField = "PERNR";
            DDLEmpList.DataBind();

            DDL_lvemp.DataSource = objPIDashBoardLst;
            DDL_lvemp.DataTextField = "ENAME";
            DDL_lvemp.DataValueField = "PERNR";
            DDL_lvemp.DataBind();


        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
               
                lblMessageBoard.Text = "";

                string strPernrName = string.Empty;

                if ((DDLEmpList.SelectedValue.ToString().Trim() == "0") || string.IsNullOrEmpty(DDLEmpList.SelectedValue.ToString().Trim()))
                {
                    grdTimeSheetReview.DataSource = null;
                    grdTimeSheetReview.DataBind();
                    btnExcel.Visible = false;
                    lblMessageBoard.Text = "Please select the value";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                }

                else
                {
                    grdTimeSheetReview.DataSource = null;
                    grdTimeSheetReview.DataBind();
                    btnExcel.Visible = true;
                    lblMessageBoard.Text = "";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;

                   
                    strPernrName = DDLEmpList.SelectedValue.ToString();
                    DateTime DtFrm = new DateTime(1900, 01, 01);
                    DateTime DtTo = new DateTime(1900, 01, 01);

                    if (DateTime.TryParse(TxtFromDate.Text, out DtFrm))
                    {
                        if (DateTime.TryParse(TxtToDate.Text, out DtTo))
                        {
                           

                            DataTable dt = new DataTable();
                            dt.Columns.Add("PERNR");
                            dt.Columns.Add("ENAME");
                            dt.Columns.Add("KTEXT");                           
                            dt.Columns.Add("POST1");
                            dt.Columns.Add("Activity");
                            dt.Columns.Add("ATEXT");
                            dt.Columns.Add("status");
                            dt.Columns.Add("WORKINGDATE"); 
                            dt.Columns.Add("REMARKS");
                            dt.Columns.Add("EMP_DEPT");
                            dt.Columns.Add("CATSHOURS");

                            string strPernr = "";
                            string strEName = "";
                            string strOrderType = "";
                            string strWBS = "";
                            string strActivity = "";
                            string strAbsenceType = "";
                            string strStstus = "";
                            string strWrkngDate = "";
                            string strremarks = "";
                            string dept = "";
                            string strCatshrs = "";

                            if (DDL_mgrsrchtyp.SelectedValue == "0")
                            {
                            mstimesheetreviewDataContext objTimesheetReviewDataContext = new mstimesheetreviewDataContext();
                            foreach (var vRow in objTimesheetReviewDataContext.sp_ms_Load_TimeSheetReview(strPernrName, Session["CompCode"].ToString(), DtFrm, DtTo, User.Identity.Name, DDL_mgrbysrchtyps.SelectedValue.ToString().Trim(),1))
                            {                                                              

                                if (vRow.PERNR != null)
                                {
                                    strPernr = vRow.PERNR.Trim().ToString();
                                }

                                if (vRow.ENAME != null)
                                {
                                    strEName = vRow.ENAME.Trim().ToString();
                                }

                                 if (vRow.KTEXT != null)
                                {
                                    strOrderType = vRow.KTEXT.Trim().ToString();
                                }

                                 if (vRow.POST1 != null)
                                {
                                    strWBS = vRow.POST1.Trim().ToString();
                                }


                                if (vRow.Activity != null)
                                {
                                    strActivity = vRow.Activity.Trim().ToString();
                                }                               

                                if (vRow.ATEXT != null)
                                {
                                    strAbsenceType = vRow.ATEXT.Trim().ToString();
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

                                
                                if (vRow.REMARKS != null)
                                {
                                    strremarks = vRow.REMARKS.Trim().ToString();
                                }
                                if (vRow.EMP_DEPT != null)
                                {
                                    dept = vRow.EMP_DEPT.Trim().ToString();
                                }

                                if (vRow.CATSHOURS != null)
                                {
                                    strCatshrs = vRow.CATSHOURS.Trim().ToString();
                                }


                                DataRow dr = dt.NewRow();
                                dr["PERNR"] = strPernr;
                                dr["ENAME"] = strEName;
                                dr["KTEXT"] = strOrderType;
                                dr["POST1"] = strWBS;
                                dr["Activity"] = strActivity;
                                dr["ATEXT"] = strAbsenceType;
                                dr["status"] = strStstus;
                                dr["WORKINGDATE"] = strWrkngDate;
                                dr["REMARKS"] = strremarks;
                                dr["EMP_DEPT"] = dept;
                                dr["CATSHOURS"] = strCatshrs;
                                dt.Rows.Add(dr);

                            }

                            if (dt.Rows.Count > 0)
                            {
                            Session.Add("dataTable", dt);
                            grdTimeSheetReview.DataSource = dt;
                            grdTimeSheetReview.DataBind();
                            Bindtotalhrs();
                            btnExcel.Visible = true;
                            }
                            else
                            {
                                grdTimeSheetReview.DataSource = null;
                                grdTimeSheetReview.DataBind();
                                btnExcel.Visible = false;
                                lblMessageBoard.Text = "No Records found !!";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            }
                            objTimesheetReviewDataContext.Dispose();
                        }

                           if (DDL_mgrsrchtyp.SelectedValue == "1")
                            {
                            mstimesheetreviewDataContext objTimesheetReviewDataContext = new mstimesheetreviewDataContext();
                            foreach (var vRow in objTimesheetReviewDataContext.sp_ms_Load_TimeSheetReview(strPernrName, Session["CompCode"].ToString(), DtFrm, DtTo, User.Identity.Name, DDL_mgrbysrchtyps.SelectedValue.ToString().Trim(),2))
                            {                                                              

                                if (vRow.PERNR != null)
                                {
                                    strPernr = vRow.PERNR.Trim().ToString();
                                }

                                if (vRow.ENAME != null)
                                {
                                    strEName = vRow.ENAME.Trim().ToString();
                                }

                                 if (vRow.KTEXT != null)
                                {
                                    strOrderType = vRow.KTEXT.Trim().ToString();
                                }

                                 if (vRow.POST1 != null)
                                {
                                    strWBS = vRow.POST1.Trim().ToString();
                                }


                                if (vRow.Activity != null)
                                {
                                    strActivity = vRow.Activity.Trim().ToString();
                                }                               

                                if (vRow.ATEXT != null)
                                {
                                    strAbsenceType = vRow.ATEXT.Trim().ToString();
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

                                
                                if (vRow.REMARKS != null)
                                {
                                    strremarks = vRow.REMARKS.Trim().ToString();
                                }
                                if (vRow.EMP_DEPT != null)
                                {
                                    dept = vRow.EMP_DEPT.Trim().ToString();
                                }

                                if (vRow.CATSHOURS != null)
                                {
                                    strCatshrs = vRow.CATSHOURS.Trim().ToString();
                                }


                                DataRow dr = dt.NewRow();
                                dr["PERNR"] = strPernr;
                                dr["ENAME"] = strEName;
                                dr["KTEXT"] = strOrderType;
                                dr["POST1"] = strWBS;
                                dr["Activity"] = strActivity;
                                dr["ATEXT"] = strAbsenceType;
                                dr["status"] = strStstus;
                                dr["WORKINGDATE"] = strWrkngDate;
                                dr["REMARKS"] = strremarks;
                                dr["EMP_DEPT"] = dept;
                                dr["CATSHOURS"] = strCatshrs;
                                dt.Rows.Add(dr);

                            }

                            if (dt.Rows.Count > 0)
                            {
                            Session.Add("dataTable", dt);
                            grdTimeSheetReview.DataSource = dt;
                            grdTimeSheetReview.DataBind();
                            Bindtotalhrs();
                            btnExcel.Visible = true;
                            }
                            else
                            {
                                grdTimeSheetReview.DataSource = null;
                                grdTimeSheetReview.DataBind();
                                btnExcel.Visible = false;
                                lblMessageBoard.Text = "No Records found !!";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            }
                            objTimesheetReviewDataContext.Dispose();
                        }

                           if (DDL_mgrsrchtyp.SelectedValue == "2")
                            {
                            mstimesheetreviewDataContext objTimesheetReviewDataContext = new mstimesheetreviewDataContext();
                            foreach (var vRow in objTimesheetReviewDataContext.sp_ms_Load_TimeSheetReview(strPernrName, Session["CompCode"].ToString(), DtFrm, DtTo, User.Identity.Name, DDL_mgrbysrchtyps.SelectedValue.ToString().Trim(),3))
                            {                                                              

                                if (vRow.PERNR != null)
                                {
                                    strPernr = vRow.PERNR.Trim().ToString();
                                }

                                if (vRow.ENAME != null)
                                {
                                    strEName = vRow.ENAME.Trim().ToString();
                                }

                                 if (vRow.KTEXT != null)
                                {
                                    strOrderType = vRow.KTEXT.Trim().ToString();
                                }

                                 if (vRow.POST1 != null)
                                {
                                    strWBS = vRow.POST1.Trim().ToString();
                                }


                                if (vRow.Activity != null)
                                {
                                    strActivity = vRow.Activity.Trim().ToString();
                                }                               

                                if (vRow.ATEXT != null)
                                {
                                    strAbsenceType = vRow.ATEXT.Trim().ToString();
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

                                
                                if (vRow.REMARKS != null)
                                {
                                    strremarks = vRow.REMARKS.Trim().ToString();
                                }
                                if (vRow.EMP_DEPT != null)
                                {
                                    dept = vRow.EMP_DEPT.Trim().ToString();
                                }

                                if (vRow.CATSHOURS != null)
                                {
                                    strCatshrs = vRow.CATSHOURS.Trim().ToString();
                                }


                                DataRow dr = dt.NewRow();
                                dr["PERNR"] = strPernr;
                                dr["ENAME"] = strEName;
                                dr["KTEXT"] = strOrderType;
                                dr["POST1"] = strWBS;
                                dr["Activity"] = strActivity;
                                dr["ATEXT"] = strAbsenceType;
                                dr["status"] = strStstus;
                                dr["WORKINGDATE"] = strWrkngDate;
                                dr["REMARKS"] = strremarks;
                                dr["EMP_DEPT"] = dept;
                                dr["CATSHOURS"] = strCatshrs;
                                dt.Rows.Add(dr);

                            }

                            if (dt.Rows.Count > 0)
                            {
                            Session.Add("dataTable", dt);
                            grdTimeSheetReview.DataSource = dt;
                            grdTimeSheetReview.DataBind();
                            Bindtotalhrs();
                            btnExcel.Visible = true;
                            }
                            else
                            {
                                grdTimeSheetReview.DataSource = null;
                                grdTimeSheetReview.DataBind();
                                btnExcel.Visible = false;
                                lblMessageBoard.Text = "No Records found !!";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            }
                            objTimesheetReviewDataContext.Dispose();
                        }

                           if (DDL_mgrsrchtyp.SelectedValue == "3")
                            {
                            mstimesheetreviewDataContext objTimesheetReviewDataContext = new mstimesheetreviewDataContext();
                            foreach (var vRow in objTimesheetReviewDataContext.sp_ms_Load_TimeSheetReview(strPernrName, Session["CompCode"].ToString(), DtFrm, DtTo, User.Identity.Name, DDL_mgrbysrchtyps.SelectedValue.ToString().Trim(),5))
                            {                                                              

                                if (vRow.PERNR != null)
                                {
                                    strPernr = vRow.PERNR.Trim().ToString();
                                }

                                if (vRow.ENAME != null)
                                {
                                    strEName = vRow.ENAME.Trim().ToString();
                                }

                                 if (vRow.KTEXT != null)
                                {
                                    strOrderType = vRow.KTEXT.Trim().ToString();
                                }

                                 if (vRow.POST1 != null)
                                {
                                    strWBS = vRow.POST1.Trim().ToString();
                                }


                                if (vRow.Activity != null)
                                {
                                    strActivity = vRow.Activity.Trim().ToString();
                                }                               

                                if (vRow.ATEXT != null)
                                {
                                    strAbsenceType = vRow.ATEXT.Trim().ToString();
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

                                
                                if (vRow.REMARKS != null)
                                {
                                    strremarks = vRow.REMARKS.Trim().ToString();
                                }
                                if (vRow.EMP_DEPT != null)
                                {
                                    dept = vRow.EMP_DEPT.Trim().ToString();
                                }

                                if (vRow.CATSHOURS != null)
                                {
                                    strCatshrs = vRow.CATSHOURS.Trim().ToString();
                                }


                                DataRow dr = dt.NewRow();
                                dr["PERNR"] = strPernr;
                                dr["ENAME"] = strEName;
                                dr["KTEXT"] = strOrderType;
                                dr["POST1"] = strWBS;
                                dr["Activity"] = strActivity;
                                dr["ATEXT"] = strAbsenceType;
                                dr["status"] = strStstus;
                                dr["WORKINGDATE"] = strWrkngDate;
                                dr["REMARKS"] = strremarks;
                                dr["EMP_DEPT"] = dept;
                                dr["CATSHOURS"] = strCatshrs;
                                dt.Rows.Add(dr);

                            }

                            if (dt.Rows.Count > 0)
                            {
                            Session.Add("dataTable", dt);
                            grdTimeSheetReview.DataSource = dt;
                            grdTimeSheetReview.DataBind();
                            Bindtotalhrs();
                            btnExcel.Visible = true;
                            }
                            else
                            {
                                grdTimeSheetReview.DataSource = null;
                                grdTimeSheetReview.DataBind();
                                btnExcel.Visible = false;
                                lblMessageBoard.Text = "No Records found !!";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            }
                            objTimesheetReviewDataContext.Dispose();
                        }

                          if (DDL_mgrsrchtyp.SelectedValue == "4")
                            {
                            mstimesheetreviewDataContext objTimesheetReviewDataContext = new mstimesheetreviewDataContext();
                            foreach (var vRow in objTimesheetReviewDataContext.sp_ms_Load_TimeSheetReview(strPernrName, Session["CompCode"].ToString(), DtFrm, DtTo, User.Identity.Name, DDL_mgrbysrchtyps.SelectedValue.ToString().Trim(),4))
                            {                                                              

                                if (vRow.PERNR != null)
                                {
                                    strPernr = vRow.PERNR.Trim().ToString();
                                }

                                if (vRow.ENAME != null)
                                {
                                    strEName = vRow.ENAME.Trim().ToString();
                                }

                                 if (vRow.KTEXT != null)
                                {
                                    strOrderType = vRow.KTEXT.Trim().ToString();
                                }

                                 if (vRow.POST1 != null)
                                {
                                    strWBS = vRow.POST1.Trim().ToString();
                                }


                                if (vRow.Activity != null)
                                {
                                    strActivity = vRow.Activity.Trim().ToString();
                                }                               

                                if (vRow.ATEXT != null)
                                {
                                    strAbsenceType = vRow.ATEXT.Trim().ToString();
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

                                
                                if (vRow.REMARKS != null)
                                {
                                    strremarks = vRow.REMARKS.Trim().ToString();
                                }
                                if (vRow.EMP_DEPT != null)
                                {
                                    dept = vRow.EMP_DEPT.Trim().ToString();
                                }

                                if (vRow.CATSHOURS != null)
                                {
                                    strCatshrs = vRow.CATSHOURS.Trim().ToString();
                                }


                                DataRow dr = dt.NewRow();
                                dr["PERNR"] = strPernr;
                                dr["ENAME"] = strEName;
                                dr["KTEXT"] = strOrderType;
                                dr["POST1"] = strWBS;
                                dr["Activity"] = strActivity;
                                dr["ATEXT"] = strAbsenceType;
                                dr["status"] = strStstus;
                                dr["WORKINGDATE"] = strWrkngDate;
                                dr["REMARKS"] = strremarks;
                                dr["EMP_DEPT"] = dept;
                                dr["CATSHOURS"] = strCatshrs;
                                dt.Rows.Add(dr);

                            }

                            if (dt.Rows.Count > 0)
                            {
                            Session.Add("dataTable", dt);
                            grdTimeSheetReview.DataSource = dt;
                            grdTimeSheetReview.DataBind();
                            Bindtotalhrs();
                            btnExcel.Visible = true;
                            }
                            else
                            {
                                grdTimeSheetReview.DataSource = null;
                                grdTimeSheetReview.DataBind();
                                btnExcel.Visible = false;
                                lblMessageBoard.Text = "No Records found !!";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            }
                           
                            objTimesheetReviewDataContext.Dispose();
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
            }
           
            catch (Exception Ex)
            { }

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
            Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + date1 + "_RWT.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();
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
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                DDLEmpList.ClearSelection();
                DDL_mgrbysrchtyps.Visible = false;
                DDLEmpList.SelectedValue = "0";
                DDL_mgrsrchtyp.SelectedValue = "0";
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

        protected void DDL_mgrsrchtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_mgrsrchtyp.SelectedValue == "0")
                {
                    DDL_mgrbysrchtyps.Visible = false;
                }
                else if (DDL_mgrsrchtyp.SelectedValue == "1")
                {
                    DDL_mgrbysrchtyps.Visible = true;
                    SPayc_Collection_BO objlst = new SPayc_Collection_BO();
                    SPayc_BL bl = new SPayc_BL();
                    objlst = bl.load_prjctwbsact(Session["CompCode"].ToString(), 1);
                    DDL_mgrbysrchtyps.DataSource = objlst;
                    DDL_mgrbysrchtyps.DataTextField = "col11";
                    DDL_mgrbysrchtyps.DataValueField = "id1";
                    DDL_mgrbysrchtyps.DataBind();
                    DDL_mgrbysrchtyps.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                }
                else if (DDL_mgrsrchtyp.SelectedValue == "2")
                {
                    DDL_mgrbysrchtyps.Visible = true;
                    SPayc_Collection_BO objlst = new SPayc_Collection_BO();
                    SPayc_BL bl = new SPayc_BL();
                    objlst = bl.load_prjctwbsact(Session["CompCode"].ToString(), 2);
                    DDL_mgrbysrchtyps.DataSource = objlst;
                    DDL_mgrbysrchtyps.DataTextField = "col11";
                    DDL_mgrbysrchtyps.DataValueField = "id1";
                    DDL_mgrbysrchtyps.DataBind();
                    DDL_mgrbysrchtyps.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                }
                else if (DDL_mgrsrchtyp.SelectedValue == "3")
                {
                    DDL_mgrbysrchtyps.Visible = true;
                    SPayc_Collection_BO objlst = new SPayc_Collection_BO();
                    SPayc_BL bl = new SPayc_BL();
                    objlst = bl.load_prjctwbsact(Session["CompCode"].ToString(), 3);
                    DDL_mgrbysrchtyps.DataSource = objlst;
                    DDL_mgrbysrchtyps.DataTextField = "col11";
                    DDL_mgrbysrchtyps.DataValueField = "id1";
                    DDL_mgrbysrchtyps.DataBind();
                    DDL_mgrbysrchtyps.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                }
                else if (DDL_mgrsrchtyp.SelectedValue == "4")
                {
                    DDL_mgrbysrchtyps.Visible = true;
                    SPayc_Collection_BO objlst = new SPayc_Collection_BO();
                    SPayc_BL bl = new SPayc_BL();
                    objlst = bl.load_prjctwbsact(Session["CompCode"].ToString(), 4);
                    DDL_mgrbysrchtyps.DataSource = objlst;
                    DDL_mgrbysrchtyps.DataTextField = "col11";
                    DDL_mgrbysrchtyps.DataValueField = "id1";
                    DDL_mgrbysrchtyps.DataBind();
                    DDL_mgrbysrchtyps.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
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
                for (int i = 0; i < grdTimeSheetReview.Rows.Count; i++)
                {

                    Label hrs = (Label)grdTimeSheetReview.Rows[i].FindControl("lbl_emphrs");
                    decimal calc = Convert.ToDecimal(hrs.Text == "" ? "0.00" : hrs.Text);
                    total = total + calc;

                }
                Label thrs = ((Label)grdTimeSheetReview.FooterRow.FindControl("lbl_emptotalhrs"));
                thrs.Text = total.ToString() == "0.00" ? "" : total.ToString();

                ViewState["Totalscore"] = thrs.Text;
                int a = grdTimeSheetReview.Rows.Count;
            }
            catch (Exception ex)
            {

            }
        }

        protected void HideTabs()
        {
            view1.Visible = false;
            view2.Visible = false;          

            Tab1.CssClass = "nav-link  p-2";
            Tab2.CssClass = "nav-link  p-2";          

        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = true;
            view2.Visible = false;
            Tab1.CssClass = "nav-link active p-2";
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = false;
            view2.Visible = true;
            Tab2.CssClass = "nav-link active p-2";
        }

        protected void GV_leaveattd_review_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int? flg = 0;
                // int? flg = DDL_lvemp.SelectedValue.ToString() == "1" ? 2 : 1;
                if (DDL_lvemp.SelectedValue.ToString() == "1")
                {
                    flg = 2;
                }
                else if (DDL_lvemp.SelectedValue.ToString() == "2")
                {
                    flg = 3;
                }
                else
                {
                    flg = 1;
                }
                msassignedtomebl bl = new msassignedtomebl();
                List<msassignedtomebo> lst = new List<msassignedtomebo>();
                msassignedtomebo bo = new msassignedtomebo();
                bo.ccode = Session["CompCode"].ToString().Trim();
                bo.PERNR = DDL_lvemp.SelectedValue.ToString();
                bo.bdate = DateTime.Parse(txt_lvfrmdate.Text.Trim());
                bo.edate = DateTime.Parse(txt_lvtodate.Text.Trim());
                bo.PLSXT = User.Identity.Name;
                bo.Flag = flg;
                lst = bl.Get_Leaveattd_review(bo);
                GV_leaveattd_review.DataSource = lst;
                GV_leaveattd_review.PageIndex = e.NewPageIndex;
                GV_leaveattd_review.DataBind();
                btnlvexprt.Visible = lst.Count > 0 ? true : false;

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_leaveattd_review.Rows)
                {
                    for (int i = 0; i < GV_leaveattd_review.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_leaveattd_review.Rows[i].FindControl("lbllvRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == GV_leaveattd_review.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divlv.InnerHtml = "Showing " + frow + " to " + lrow + " of " + lst.Count + " entries";
                divlv.Visible = GV_leaveattd_review.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {

            }
        }

        
        protected void btnlvview_Click(object sender, EventArgs e)
        {
            try
            {
                int? flg = 0;
               // int? flg = DDL_lvemp.SelectedValue.ToString() == "1" ? 2 : 1;
                if(DDL_lvemp.SelectedValue.ToString() == "1")
                {
                    flg = 2;
                }
                else if (DDL_lvemp.SelectedValue.ToString() == "2")
                {
                    flg = 3;
                }
                else
                {
                    flg = 1;
                }
                msassignedtomebl bl = new msassignedtomebl();
                List<msassignedtomebo> lst = new List<msassignedtomebo>();
                msassignedtomebo bo = new msassignedtomebo();
                bo.ccode = Session["CompCode"].ToString().Trim();
                bo.PERNR = DDL_lvemp.SelectedValue.ToString();
                bo.bdate = DateTime.Parse(txt_lvfrmdate.Text.Trim());
                bo.edate = DateTime.Parse(txt_lvtodate.Text.Trim());
                bo.PLSXT = User.Identity.Name;
                bo.Flag = flg;
                lst = bl.Get_Leaveattd_review(bo);
                GV_leaveattd_review.DataSource = lst;
                GV_leaveattd_review.DataBind();
                btnlvexprt.Visible = lst.Count > 0 ?  true :  false;

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_leaveattd_review.Rows)
                {
                    for (int i = 0; i < GV_leaveattd_review.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_leaveattd_review.Rows[i].FindControl("lbllvRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == GV_leaveattd_review.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divlv.InnerHtml = "Showing " + frow + " to " + lrow + " of " + lst.Count + " entries";
                divlv.Visible = GV_leaveattd_review.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        protected void btnlvclick_Click(object sender, EventArgs e)
        {
            try
            {
                txt_lvfrmdate.Text = string.Empty;
                txt_lvtodate.Text = string.Empty;
                GV_leaveattd_review.DataSource = null;
                GV_leaveattd_review.DataBind();
                btnlvexprt.Visible = false;
                lblMessageBoard.Text = string.Empty;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                DDLEmpList.ClearSelection();
                DDL_mgrbysrchtyps.Visible = false;
                DDL_lvemp.SelectedValue = "0";
                txt_lvfrmdate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).ToString("yyyy-MM-dd");
                txt_lvtodate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("yyyy-MM-dd");
                divlv.Visible = GV_leaveattd_review.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        protected void btnlvexprt_Click(object sender, EventArgs e)
        {
            try
            {
                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                string colHeads = "Leave / Attendance Details";
                htw.WriteEncodedText(colHeads);
                htw.WriteBreak();
                GV_leaveattd_review.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                GV_leaveattd_review.GridLines = GridLines.Both;
                GV_leaveattd_review.RenderControl(htw);
                htw.WriteBreak();
                string renderedGridView = sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Leave_Summary_Report" + date1 + "_EWT.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();
            }
            catch (Exception ex)
            { }
        }

        protected void GV_leaveattd_review_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Label lbllv = (Label)e.Row.FindControl("LBL_lvempid");
                string ccode = Session["CompCode"].ToString();
                string emplogin = lbllv.Text.ToString();
                int cnt = ccode.Length;
                emplogin = emplogin.Substring(cnt);
                e.Row.Cells[1].Text = emplogin.Trim().ToUpper();
            }
            catch (Exception Ex)
            { }
        }
        
    }
}
using iEmpPower.Old_App_Code.iEmpPowerBL.SPaycompute;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute.SPayc_Collection_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace iEmpPower.UI.SPaycompute
{
    public partial class Admin_salary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["Attendance"] == "4")
                {
                    Attd_Comn_code_DDL();
                    Admin_attendance.Visible = true;
                    PNL_upload_excel.Visible = false;
                    btn_exprt_excel_attnce.Visible = GV_admn_attence.Rows.Count > 0 ? true : false;
                    btn_cnclexprt_excel_attnce.Visible = GV_admn_attence.Rows.Count > 0 ? true : false;
                }
                else
                {
                    Admin_attendance.Visible = false;
                    PNL_upload_excel.Visible = true;
                    disp_mnth.Visible = false;
                    Comn_code_DDL();
                    disp_mnth.Visible = false;
                    txt_ADsalary_month.Text = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
                    btn_exprt_slry.Visible = GV_Admin_Export.Rows.Count > 0 ? true : false;
                    btn_cncl_exprt.Visible = GV_Admin_Export.Rows.Count > 0 ? true : false;
                }
            }
        }



        public void Comn_code_DDL()
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.set_createdbyDDL("", "", "", DDL_ADcompcode.SelectedValue.ToString().Trim(), 6);
                DDL_ADcompcode.DataSource = null;
                DDL_ADcompcode.DataBind();

                DDL_ADcompcode.DataSource = objspaylst;
                DDL_ADcompcode.DataTextField = "col5";
                DDL_ADcompcode.DataValueField = "col11";
                DDL_ADcompcode.DataBind();

            }
            catch (Exception ex)
            {

            }
        }


        public void Attd_Comn_code_DDL()
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.set_createdbyDDL("", "", "", DDL_ADcompcode.SelectedValue.ToString().Trim(), 6);
                DDL_Admin_attnds.DataSource = null;
                DDL_Admin_attnds.DataBind();

                DDL_Admin_attnds.DataSource = objspaylst;
                DDL_Admin_attnds.DataTextField = "col5";
                DDL_Admin_attnds.DataValueField = "col11";
                DDL_Admin_attnds.DataBind();

            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_ADview_todownld_Click(object sender, EventArgs e)
        {
            try
            {

                disp_mnth.Visible = true;
                GV_Admin_Export.Visible = true;
                LBL_ADbind_Smonth.Text = txt_ADsalary_month.Text;

                String strConnString = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;

                SqlConnection con = new SqlConnection(strConnString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "payc_get_salary_rates_toadmin";

                cmd.Parameters.Add("@ccode", SqlDbType.VarChar).Value = DDL_ADcompcode.SelectedValue;

                cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = txt_ADsalary_month.Text;

                cmd.Connection = con;

                try
                {

                    con.Open();

                    GV_Admin_Export.EmptyDataText = "No Records Found";

                    GV_Admin_Export.DataSource = cmd.ExecuteReader();

                    GV_Admin_Export.DataBind();

                    btn_exprt_slry.Visible = GV_Admin_Export.Rows.Count > 0 ? true : false;
                    btn_cncl_exprt.Visible = GV_Admin_Export.Rows.Count > 0 ? true : false;





                }

                catch (Exception ex)
                {

                    throw ex;

                }

                finally
                {

                    con.Close();

                    con.Dispose();

                }
            }

            catch (Exception ex)
            {

            }

        }

        protected void btn_generate_attnd_Click(object sender, EventArgs e)
        {
            try
            {

                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.Attdnce_exprt(DDL_Admin_attnds.SelectedValue, 0, txt_adattence_frmdate.Text.ToString().Trim(), txt_attdn_todate.Text.ToString().Trim());
                GV_viewattnce.DataSource = objspaylst;
                GV_viewattnce.DataBind();
                btn_exprt_excel_attnce.Visible = GV_viewattnce.Rows.Count > 0 ? true : false;
                btn_cnclexprt_excel_attnce.Visible = GV_viewattnce.Rows.Count > 0 ? true : false;
                btn_generate_attnd.Visible = true;
            }
            catch (Exception ex)
            {

            }

        }



        protected void btn_cnclexprt_excel_attnce_Click(object sender, EventArgs e)
        {
            try
            {
                GV_viewattnce.DataSource = null;
                GV_viewattnce.DataBind();
                btn_exprt_excel_attnce.Visible = GV_viewattnce.Rows.Count > 0 ? true : false;
                btn_cnclexprt_excel_attnce.Visible = false;
                txt_adattence_frmdate.Text = "";
                txt_attdn_todate.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_admn_attence_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                GV_admn_attence.PageIndex = e.NewPageIndex;
                objspaylst = objPaycbl.Attdnce_exprt(DDL_Admin_attnds.SelectedValue, 0, txt_adattence_frmdate.Text.ToString().Trim(), txt_attdn_todate.Text.ToString().Trim());
                GV_admn_attence.DataSource = objspaylst;
                GV_admn_attence.DataBind();
            }

            catch (Exception ex)
            {

            }

        }


        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }


        protected void btn_exprt_excel_attnce_Click(object sender, EventArgs e)
        {
            try
            {

                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


                GV_admn_attence.AllowPaging = false;
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();


                objspaylst = objPaycbl.Attdnce_exprt(DDL_Admin_attnds.SelectedValue, 0, txt_adattence_frmdate.Text.ToString().Trim(), txt_attdn_todate.Text.ToString().Trim());
                GV_admn_attence.DataSource = objspaylst;
                GV_admn_attence.DataBind();

                GV_admn_attence.RenderControl(htw);

                GV_admn_attence.AllowPaging = true;

                htw.WriteBreak();

                string renderedGridView = " " + "<br>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + " AdvMonthlyAttendance1.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();
            }
            catch (Exception ex)
            {

            }
        }


        protected void GV_admn_attence_DataBound(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableHeaderCell cell = new TableHeaderCell();
                cell.Text = "1";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "2";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "3";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "4";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "5";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "6";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "7";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "8";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "9";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "10";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "11";
                row.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.Text = "12";
                row.Controls.Add(cell);



                GV_admn_attence.HeaderRow.Parent.Controls.AddAt(1, row);


                
            }



            catch (Exception ex)
            {

            }

        }

        //protected void GV_Admin_Export_DataBound(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        for (int i = 23; i > GV_Admin_Export.Columns.Count; i--)
        //        {

        //            BoundField bfield = new BoundField();
        //            bfield.HeaderText = "Col" + i;

        //            GV_Admin_Export.Columns.Add(bfield);


        //        }

        //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //TableHeaderCell cell = new TableHeaderCell();



        //cell.Text = "Emp ID/Ref No.";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Name";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 1";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 2";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 3";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 4";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 5";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 6";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 7";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 8";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 9";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 10";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 11";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 12";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 13";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 14";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 15";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 16";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 17";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 18";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 19";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Col 20";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "Remarks";
        //row.Controls.Add(cell);



        //GV_Admin_Export.HeaderRow.Parent.Controls.AddAt(0, row);
        //GV_Admin_Export.HeaderRow.Cells[0].Text = "Emp ID/Ref No.";

        //GV_Admin_Export.HeaderRow.Cells[1].Text = "Name";

        //GV_Admin_Export.HeaderRow.Cells[2].Text = "Col 1";
        //GV_Admin_Export.HeaderRow.Cells[3].Text = "Col 2";
        //GV_Admin_Export.HeaderRow.Cells[4].Text = "Col 3";
        //GV_Admin_Export.HeaderRow.Cells[5].Text = "Col 4";
        //GV_Admin_Export.HeaderRow.Cells[6].Text = "Col 5";
        //GV_Admin_Export.HeaderRow.Cells[7].Text = "Col 6";
        //GV_Admin_Export.HeaderRow.Cells[8].Text = "Col 7";
        //GV_Admin_Export.HeaderRow.Cells[9].Text = "Col 8";
        //GV_Admin_Export.HeaderRow.Cells[10].Text = "Col 9";
        //GV_Admin_Export.HeaderRow.Cells[11].Text = "Col 10";
        //GV_Admin_Export.HeaderRow.Cells[12].Text = "Col 11";
        //GV_Admin_Export.HeaderRow.Cells[13].Text = "Col 12";
        //GV_Admin_Export.HeaderRow.Cells[14].Text = "Col 13";
        //GV_Admin_Export.HeaderRow.Cells[15].Text = "Col 14";
        //GV_Admin_Export.HeaderRow.Cells[16].Text = "Col 15";
        //GV_Admin_Export.HeaderRow.Cells[17].Text = "Col 16";
        //GV_Admin_Export.HeaderRow.Cells[18].Text = "Col 17";
        //GV_Admin_Export.HeaderRow.Cells[19].Text = "Col 18";
        //GV_Admin_Export.HeaderRow.Cells[20].Text = "Col 19";
        //GV_Admin_Export.HeaderRow.Cells[21].Text = "Col 20";
        //GV_Admin_Export.HeaderRow.Cells[22].Text = "Remarks";


        //cell.Text = "1";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "2";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "3";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "4";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "5";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "6";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "7";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "8";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "9";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "10";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "11";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "12";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "13";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "14";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "15";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "16";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "17";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "18";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "19";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "20";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "21";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "22";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.Text = "23";
        //row.Controls.Add(cell);

        //GV_Admin_Export.HeaderRow.Parent.Controls.AddAt(1, row);


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void btn_exprt_slry_Click(object sender, EventArgs e)
        {
            try
            {
                string prevmonth = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
                string a5 = "~/Salary_Reports/PaySlips/" + DDL_ADcompcode.SelectedValue.ToString().Trim() + "/" + prevmonth.ToString() + "";
                bool exists7 = System.IO.Directory.Exists(Server.MapPath(a5));
                if (!exists7)
                    System.IO.Directory.CreateDirectory(Server.MapPath(a5));


                SPaycompute_BO objBo2 = new SPaycompute_BO();
                SPayc_BL objPaycbl2 = new SPayc_BL();
                objBo2.CCD = DDL_ADcompcode.SelectedValue;
                objBo2.id1 = 1;
                objBo2.MNTH = txt_ADsalary_month.Text;
                objBo2.TXT = "";
                objBo2.id2 = 0;
                objPaycbl2.Save_payroll_activity(objBo2, 2);


                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


                GV_Admin_Export.AllowPaging = false;

                GV_Admin_Export.RenderControl(htw);

                GV_Admin_Export.AllowPaging = true;

                htw.WriteBreak();

                string renderedGridView = " " + "<br>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + " SalaryRate.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();

                
            }

            catch (Exception ex)
            {

            }
        }

        protected void btn_cncl_exprt_Click(object sender, EventArgs e)
        {
            try
            {
                GV_Admin_Export.DataSource = null;
                GV_Admin_Export.DataBind();
                disp_mnth.Visible = false;
                GV_Admin_Export.Visible = false;
                btn_exprt_slry.Visible = GV_Admin_Export.Rows.Count > 0 ? true : false;
                btn_cncl_exprt.Visible = GV_Admin_Export.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {

            }
        }

       
   
        protected void GV_Admin_Export_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string ccode = DDL_ADcompcode.SelectedValue.ToString().Trim();
                    string emplogin = e.Row.Cells[0].Text.ToString().Trim();
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    e.Row.Cells[0].Text = emplogin.Trim();


                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_admn_attence_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string ccode = DDL_Admin_attnds.SelectedValue.ToString().Trim();
                    string emplogin = e.Row.Cells[0].Text.ToString().Trim();
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    e.Row.Cells[0].Text = emplogin.Trim();


                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void GV_viewattnce_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                GV_viewattnce.PageIndex = e.NewPageIndex;
                objspaylst = objPaycbl.Attdnce_exprt(DDL_Admin_attnds.SelectedValue, 0, txt_adattence_frmdate.Text.ToString().Trim(), txt_attdn_todate.Text.ToString().Trim());
                GV_viewattnce.DataSource = objspaylst;
                GV_viewattnce.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_viewattnce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string ccode = DDL_Admin_attnds.SelectedValue.ToString().Trim();
                    string emplogin = e.Row.Cells[1].Text.ToString().Trim();
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    e.Row.Cells[1].Text = emplogin.Trim();


                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

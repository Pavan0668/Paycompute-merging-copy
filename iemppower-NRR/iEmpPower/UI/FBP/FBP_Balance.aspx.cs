using iEmpPower.Old_App_Code.iEmpPowerDAL.IT;
using iEmpPowerMaster_Load;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.FBP
{
    public partial class FBP_Balance : System.Web.UI.Page
    {
        int MPbtnclick = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlType.SelectedValue = "0";
                LoadDeclarationClaims();
                HFselec.Value = "1";
            }
            if (txttodt.Text == "" && txtfrmdt.Text == "")
            {
                RFV_txttodt.Enabled = false;
                RFV_txttodt.Enabled = false;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        private void MsgCls(string LblTxt, Label Lbl, Color Clr)
        {
            try
            {
                Lbl.Text = string.Empty;
                Lbl.Text = LblTxt;
                Lbl.ForeColor = Clr;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void LoadDeclarationClaims()
        {
            try
            {
                DataTable ds = new DataTable();
                ds.Clear();
                grdFbpBalance.Visible = false;

                if (ddlType.SelectedValue.ToString().Trim() == "1")
                {
                    grdFbpBalance.PageSize = Convert.ToInt32(ddlPagesizeEmp.SelectedValue);
                    ds = GetInfo("usp_FBP_report_balance", 1);
                    grdFbpBalance.DataSource = ds;
                    grdFbpBalance.DataBind();
                    grdFbpBalance.Visible = true;


                    grdFbpBalance.FooterRow.Cells[5].Text = "Total :";
                    grdFbpBalance.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    grdFbpBalance.FooterRow.Cells[5].Font.Bold = true;


                    for (int i = 6; i < grdFbpBalance.HeaderRow.Cells.Count; i++)
                    {
                        grdFbpBalance.FooterRow.Cells[i].Text = ds.AsEnumerable().Sum(item => Convert.ToDecimal(item.Field<string>(grdFbpBalance.HeaderRow.Cells[i].Text))).ToString("N2");
                        grdFbpBalance.FooterRow.Cells[i].Font.Bold = true;
                        grdFbpBalance.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    }





                    btnExpXL.Visible = grdFbpBalance.Rows.Count > 0 ? true : false;
                }

               

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void ddlPagesizeEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDeclarationClaims();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "5")
                chkDeclared.Visible = true;
            else
                chkDeclared.Visible = false;

            MPbtnclick = 1;
            btnCY.CssClass = "btn btn-xs btn-secondary";
            btnLY.CssClass = "btn btn-xs btn-light";
            btnAll.CssClass = "btn btn-xs btn-light";
            ddlPagesizeEmp.SelectedIndex = -1;
            txtfrmdt.Text = txtsearch.Text = txttodt.Text = "";
            LoadDeclarationClaims();
            resetToInitial();
        }

        protected void txtfrmdt_TextChanged(object sender, EventArgs e)
        {
            if (txtfrmdt.Text != "" || txttodt.Text != "")
            {
                RFV_txtfrmdt.Enabled = true;
                RFV_txttodt.Enabled = true;
            }
            else
            {
                RFV_txtfrmdt.Enabled = false;
                RFV_txttodt.Enabled = false;
            }

            if (txttodt.Text == "")
                txttodt.Focus();
            else if (txtfrmdt.Text == "")
                txtfrmdt.Focus();
            if (txtfrmdt.Text != "" && txttodt.Text != "")
            {
                MPbtnclick = 3;
                LoadDeclarationClaims();
            }
            resetToInitial();
        }

        protected void txttodt_TextChanged(object sender, EventArgs e)
        {
            if (txtfrmdt.Text != "" || txttodt.Text != "")
            {
                RFV_txtfrmdt.Enabled = true;
                RFV_txttodt.Enabled = true;
            }
            else
            {
                RFV_txtfrmdt.Enabled = false;
                RFV_txttodt.Enabled = false;
            }
            if (txtfrmdt.Text == "")
                txtfrmdt.Focus();
            else if (txttodt.Text == "")
                txttodt.Focus();

            if (txtfrmdt.Text != "" && txttodt.Text != "")
            {
                MPbtnclick = 3;
                LoadDeclarationClaims();
            }
            resetToInitial();
        }

        protected void Disablemandte()
        {
            if (txttodt.Text == "" && txtfrmdt.Text == "")
            {
                RFV_txttodt.Enabled = false;
                RFV_txtfrmdt.Enabled = false;
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            MPbtnclick = 3;
            btnCY.CssClass = "btn btn-xs btn-light";
            btnLY.CssClass = "btn btn-xs btn-light";
            btnAll.CssClass = "btn btn-xs btn-secondary";

            txttodt.Text = "";
            txtfrmdt.Text = "";
            LoadDeclarationClaims();

            Disablemandte();
            HFselec.Value = "3"; resetToInitial();
        }

        protected void btnLY_Click(object sender, EventArgs e)
        {
            MPbtnclick = 2;
            btnCY.CssClass = "btn btn-xs btn-light";
            btnLY.CssClass = "btn btn-xs btn-secondary";
            btnAll.CssClass = "btn btn-xs btn-light";

            txttodt.Text = "";
            txtfrmdt.Text = "";
            LoadDeclarationClaims();

            Disablemandte();
            HFselec.Value = "2"; resetToInitial();
        }

        protected void btnCY_Click(object sender, EventArgs e)
        {
            MPbtnclick = 1;

            btnCY.CssClass = "btn btn-xs btn-secondary";
            btnLY.CssClass = "btn btn-xs btn-light";
            btnAll.CssClass = "btn btn-xs btn-light";

            txttodt.Text = "";
            txtfrmdt.Text = "";
            LoadDeclarationClaims();

            Disablemandte();
            HFselec.Value = "1"; resetToInitial();
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            LoadDeclarationClaims();
            resetToInitial();
        }

        public DataTable GetInfo(string sp, int flg)
        {
            string connectioString = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ToString();
            SqlConnection con = new SqlConnection(connectioString);
            DataTable ds = new DataTable();
            try
            {
                string Ests = ddlEmpSts.SelectedValue.Trim();
                if (chkDeclared.Checked)
                {
                    if (ddlEmpSts.SelectedValue.Trim() == "1")
                        Ests = "4";
                    if (ddlEmpSts.SelectedValue.Trim() == "2")
                        Ests = "5";
                }
                else Ests = ddlEmpSts.SelectedValue.Trim();



                SqlCommand cmd = new SqlCommand(sp, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                cmd.Parameters.AddWithValue("@BDT", DateTime.Parse(txtfrmdt.Text == "" ? "1900-01-01" : txtfrmdt.Text));
                cmd.Parameters.AddWithValue("@EDT", DateTime.Parse(txttodt.Text == "" ? "1900-01-01" : txttodt.Text));
                cmd.Parameters.AddWithValue("@search", txtsearch.Text);
                cmd.Parameters.AddWithValue("@filt", MPbtnclick);
                cmd.Parameters.AddWithValue("@flg", flg);
                cmd.Parameters.AddWithValue("@Ests", Ests);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }

        protected void grdFbpClaims_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 3; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //for (int i = 3; i < e.Row.Cells.Count; i++)
                //{
                //    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                //}



                e.Row.Cells[e.Row.Cells.Count - 1].Font.Bold = true;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Font.Bold = true;
                    // e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }



            }




        }

        protected void grdFbpClaims_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdFbpClaims.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void resetToInitial()
        {
            grdITSecWise.PageIndex = 0;
            grdIT.PageIndex = 0;
            grdFbpClaims.PageIndex = 0;
            grdHRA.PageIndex = 0;
            grdFbpBalance.PageIndex = 0;
            grdPrevEmp.PageIndex = 0;
            grdIncomefromOtherSources.PageIndex = 0;
            grdIncomefromOtherSources_Letout.PageIndex = 0;
            grdITSecWise_Actuals.PageIndex = 0;
            grdITSecWise80C_Actuals.PageIndex = 0;
            grdITConsolidated.PageIndex = 0;
        }

        protected void grdIT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 8; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }

                ////List<masterbo> boList = new List<masterbo>();
                ////masterbl bl = new masterbl();
                ////boList = bl.Load_DOJ(e.Row.Cells[0].Text.Trim(), 1);
                ////e.Row.Cells[2].Text = DateTime.Parse(boList[0].DOJ.ToString()).ToString("yyyy-MM-dd").Trim();

                ////boList = bl.Load_DOJ(e.Row.Cells[0].Text.Trim(), 2);
                ////e.Row.Cells[3].Text = DateTime.Parse(boList[0].DOJ.ToString()).ToString("yyyy-MM-dd").Trim();
                ////e.Row.Cells[3].Text = e.Row.Cells[3].Text.Trim() == "9999-12-31" ? "" : e.Row.Cells[3].Text.Trim();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double d = 0.0;
                int lastcell = e.Row.Cells.Count - 1;
                //e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                for (int i = 8; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
                for (int i = 8; i < e.Row.Cells.Count - 1; i++)// for (int i = 14; i < e.Row.Cells.Count - 1; i++)
                {
                    d += double.Parse(e.Row.Cells[i].Text);
                }
                e.Row.Cells[lastcell].Text = e.Row.Cells[lastcell].Text = d.ToString("N2");//  e.Row.Cells[12].Text = e.Row.Cells[lastcell].Text = d.ToString("N2");
                e.Row.Cells[lastcell].Font.Bold = true;
            }
        }

        protected void grdIT_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdIT.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void BtnExporttoXl_Click(object sender, EventArgs e)
        {
            try
            {
                string flNm = ddlType.SelectedItem.Text.Trim() + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm");
                if (ddlType.SelectedValue == "1")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdFbpBalance','" + flNm + "')", true);

                }
                else if (ddlType.SelectedValue == "2")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdFbpClaims','" + flNm + "')", true);

                }
                else if (ddlType.SelectedValue == "3")
                {

                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdIT','" + flNm + "')", true);
                    ExpXl(grdIT, ddlType.SelectedItem.Text.Trim());

                }
                else if (ddlType.SelectedValue == "4")
                {

                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdITSecWise','" + flNm + "')", true);
                    ExpXl(grdITSecWise, ddlType.SelectedItem.Text.Trim());

                }
                else if (ddlType.SelectedValue == "5")
                {

                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdHRA','" + flNm + "')", true);
                    ExpXl(grdHRA, ddlType.SelectedItem.Text.Trim());

                }

                else if (ddlType.SelectedValue == "6")
                {

                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdPrevEmp','" + flNm + "')", true);
                    ExpXl(grdPrevEmp, ddlType.SelectedItem.Text.Trim());

                }

                else if (ddlType.SelectedValue == "7")
                {

                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdIncomefromOtherSources','" + flNm + "')", true);
                    ExpXl(grdIncomefromOtherSources, ddlType.SelectedItem.Text.Trim());
                }

                else if (ddlType.SelectedValue == "8")
                {

                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdIncomefromOtherSources','" + flNm + "')", true);
                    ExpXl(grdIncomefromOtherSources_Letout, ddlType.SelectedItem.Text.Trim());
                }

                else if (ddlType.SelectedValue == "9")
                {

                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdIncomefromOtherSources','" + flNm + "')", true);
                    ExpXl(grdITSecWise_Actuals, ddlType.SelectedItem.Text.Trim());
                }

                else if (ddlType.SelectedValue == "10")
                {

                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdIncomefromOtherSources','" + flNm + "')", true);
                    ExpXl(grdITSecWise80C_Actuals, ddlType.SelectedItem.Text.Trim());
                }

                else if (ddlType.SelectedValue == "11")
                {

                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "ExportXL('ContentPlaceHolder1_MainContent_grdIncomefromOtherSources','" + flNm + "')", true);
                    ExpXl(grdITConsolidated, ddlType.SelectedItem.Text.Trim());
                }

                //if (ddlType.SelectedValue == "1")
                //    ExpXl(grdFbpBalance, ddlType.SelectedItem.Text.Trim());
                //else if (ddlType.SelectedValue == "2")
                //    ExpXl(grdFbpClaims, ddlType.SelectedItem.Text.Trim());
                //else if (ddlType.SelectedValue == "3")
                //    ExpXl(grdIT, ddlType.SelectedItem.Text.Trim());
                //else if (ddlType.SelectedValue == "4")
                //    ExpXl(grdITSecWise, ddlType.SelectedItem.Text.Trim());
                //else if (ddlType.SelectedValue == "5")
                //    ExpXl(grdHRA, ddlType.SelectedItem.Text.Trim());

            }
            catch (Exception ex) { }
        }

        protected void ExpXl(GridView a, string flNm)
        {
            string Report = flNm;
            flNm += "_" + DateTime.Now.ToString();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            a.GridLines = GridLines.Both;
            a.AllowPaging = false; LoadDeclarationClaims();
            //grdFbpDeclaration.DataSource = Session["decl_data"];
            //grdFbpDeclaration.DataBind();
            ////LoadDeclarationClaims();
            //List<FbpClaimbo> fbpboObj1 = Session["decl_data"] as List<FbpClaimbo>;
            //load_footerGrid(fbpboObj1);
            // Render grid view control.
            htw.WriteBreak();
            for (int x = 0; x < a.Rows.Count; x++)
            {
                a.Rows[x].Cells[1].Attributes.Add("class", "textmode");
            }
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            //for (int i = 0; i < grdFbpDeclaration.Rows.Count; i++)
            //{
            //    GridViewRow row = grdFbpDeclaration.Rows[i];
            //    row.Cells[1].Attributes.Add("style", "textmode");
            //}
            //for (int i = 0; i < grdFbpDeclaration.Rows.Count; i++)
            //{
            //    GridViewRow row = grdFbpDeclaration.Rows[i];
            //    //Apply text style to each Row
            //    row.Attributes.Add("class", "textmode");
            //}
            a.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
            a.RenderControl(htw);
            htw.WriteBreak();
            string renderedGridView = "<b>" + Report + " Report </b><br/>";//"FBP Declaration Details <br/>";
            renderedGridView += sw.ToString() + "<br/>";
            Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report_" + flNm + ".xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            a.GridLines = GridLines.None;
            a.AllowPaging = true;
            LoadDeclarationClaims();
            Response.End();

            //grdFbpDeclaration.CssClass = "gridviewNew";
            //grdFbpDeclaration.DataSource = Session["decl_data"];
            //grdFbpDeclaration.DataBind();
            ////LoadDeclarationClaims();
            //fbpboObj1 = Session["decl_data"] as List<FbpClaimbo>;
            //load_footerGrid(fbpboObj1);}
        }

        protected void grdFbpBalance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdFbpBalance.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void grdFbpBalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 5; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    List<masterbo> boList = new List<masterbo>();
                    masterbl bl = new masterbl();
                    boList = bl.Load_DOJ(e.Row.Cells[1].Text.Trim(), 1);
                    e.Row.Cells[4].Text = DateTime.Parse(boList[0].DOJ.ToString()).ToString("yyyy-MM-dd").Trim();

                    boList = bl.Load_DOJ(e.Row.Cells[1].Text.Trim(), 2);
                    e.Row.Cells[5].Text = DateTime.Parse(boList[0].DOJ.ToString()).ToString("yyyy-MM-dd").Trim();
                    e.Row.Cells[5].Text = e.Row.Cells[3].Text.Trim() == "9999-12-31" ? "" : e.Row.Cells[3].Text.Trim();

                    DataTable ds = new DataTable();
                    if (txtsearch.Text == "")
                    {
                        txtsearch.Text = e.Row.Cells[0].Text.Trim();
                        ds = GetInfo("usp_FBP_report_balance", 2);
                        int a = ds.Rows.Count;
                        txtsearch.Text = "";
                    }
                    else
                        ds = GetInfo("usp_FBP_report_balance", 1);

                    for (int i = 5; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[i].Text = (ds.Rows[0][i].ToString() == null || ds.Rows[0][i].ToString() == "") ? "0.00" : ds.Rows[0][i].ToString();
                    }

                    double total = 0.0;
                    for (int i = 5; i < 15; i++)
                    {
                        total += double.Parse(e.Row.Cells[i].Text);
                    }
                    e.Row.Cells[15].Text = total.ToString("N2");
                    e.Row.Cells[15].Font.Bold = true;
                    total = 0.0;
                    for (int i = 16; i < 24; i++)
                    {
                        total += double.Parse(e.Row.Cells[i].Text);
                    }
                    e.Row.Cells[24].Text = total.ToString("N2");
                    e.Row.Cells[24].Font.Bold = true;
                    total = 0.0;
                    for (int i = 25; i < 33; i++)
                    {
                        total += double.Parse(e.Row.Cells[i].Text);
                    }
                    e.Row.Cells[33].Text = total.ToString("N2");
                    e.Row.Cells[33].Font.Bold = true;
                }
            }

            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
            }


        }

        protected void grdHRA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdHRA.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void grdHRA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 12; i < e.Row.Cells.Count - 1; i++)//// for (int i = 12; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double d = 0.0;
                int lastcell = e.Row.Cells.Count - 2; ////e.Row.Cells.Count - 1;
                //e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                for (int i = 12; i < e.Row.Cells.Count - 1; i++)//// for (int i = 12; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
                for (int i = 16; i < e.Row.Cells.Count - 2; i++)////for (int i = 16; i < e.Row.Cells.Count - 1; i++)//  for (int i = 14; i < e.Row.Cells.Count - 1; i++)
                {
                    d += double.Parse(e.Row.Cells[i].Text);
                }
                e.Row.Cells[14].Text = e.Row.Cells[lastcell].Text = d.ToString("N2");//  e.Row.Cells[12].Text = e.Row.Cells[lastcell].Text = d.ToString("N2");
                e.Row.Cells[14].Font.Bold = true;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[e.Row.Cells.Count - 1].Font.Bold = true;
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 2; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Font.Bold = true;
                    }
                }

                grdHRA.FooterStyle.Font.Bold = true;
            }

        }

        protected void grdPrevEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdPrevEmp.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void grdPrevEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 12; i < e.Row.Cells.Count - 1; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double d = 0.0;
                int lastcell = e.Row.Cells.Count - 2;
                //e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                for (int i = 12; i < e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
                for (int i = 15; i < e.Row.Cells.Count - 2; i++)// for (int i = 14; i < e.Row.Cells.Count - 1; i++)
                {
                    d += double.Parse(e.Row.Cells[i].Text);
                }
                e.Row.Cells[18].Text = e.Row.Cells[lastcell].Text = d.ToString("N2");//  e.Row.Cells[12].Text = e.Row.Cells[lastcell].Text = d.ToString("N2");
                e.Row.Cells[18].Font.Bold = true;
            }
        }

        protected void grdIncomefromOtherSources_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdIncomefromOtherSources.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void grdIncomefromOtherSources_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 12; i < e.Row.Cells.Count - 1; i++) ////for (int i = 12; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double d = 0.0;
                int lastcell = e.Row.Cells.Count - 2;////int lastcell = e.Row.Cells.Count - 1;
                //e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                for (int i = 12; i < e.Row.Cells.Count - 1; i++)////for (int i = 12; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
                ////for (int i = 14; i < e.Row.Cells.Count - 1; i++)
                ////{
                ////    d += double.Parse(e.Row.Cells[i].Text);
                ////}
                ////e.Row.Cells[12].Text = e.Row.Cells[lastcell].Text = d.ToString("N2");
            }
        }

        protected void ITSecWise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdITSecWise.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void ITSecWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 8; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double d = 0.0;
                int lastcell = e.Row.Cells.Count - 1;
                //e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                for (int i = 8; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
                for (int i = 8; i < e.Row.Cells.Count - 1; i++)// for (int i = 14; i < e.Row.Cells.Count - 1; i++)
                {
                    d += double.Parse(e.Row.Cells[i].Text);
                }
                e.Row.Cells[lastcell].Text = e.Row.Cells[lastcell].Text = d.ToString("N2");//  e.Row.Cells[12].Text = e.Row.Cells[lastcell].Text = d.ToString("N2");
                e.Row.Cells[lastcell].Font.Bold = true;
            }
        }

        protected void ddlEmpSts_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDeclarationClaims();
        }

        protected void chkDeclared_CheckedChanged(object sender, EventArgs e)
        {
            LoadDeclarationClaims();
            resetToInitial();
        } //New Method

        protected void grdIncomefromOtherSources_Letout_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdIncomefromOtherSources_Letout.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void grdIncomefromOtherSources_Letout_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 12; i < e.Row.Cells.Count - 1; i++)//// for (int i = 12; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double d = 0.0;
                int lastcell = e.Row.Cells.Count - 2;//// int lastcell = e.Row.Cells.Count - 1;
                //e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                for (int i = 12; i < e.Row.Cells.Count - 1; i++)////for (int i = 12; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
                ////for (int i = 14; i < e.Row.Cells.Count - 1; i++)
                ////{
                ////    d += double.Parse(e.Row.Cells[i].Text);
                ////}
                ////e.Row.Cells[12].Text = e.Row.Cells[lastcell].Text = d.ToString("N2");
            }
        }

        protected void grdITSecWise_Actuals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdITSecWise_Actuals.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void grdITSecWise_Actuals_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////for (int i = 12; i < e.Row.Cells.Count-1; i++)//// for (int i = 12; i < e.Row.Cells.Count; i++)
            ////{
            ////    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            ////}
            ////if (e.Row.RowType == DataControlRowType.DataRow)
            ////{
            ////    double d = 0.0;
            ////    int lastcell = e.Row.Cells.Count - 2;//// int lastcell = e.Row.Cells.Count - 1;

            ////    for (int i = 12; i < e.Row.Cells.Count-1; i++)////for (int i = 12; i < e.Row.Cells.Count; i++)
            ////    {
            ////        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            ////    }
            ////}
        }


        protected void grdITSecWise80C_Actuals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdITSecWise80C_Actuals.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void grdITSecWise80C_Actuals_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////for (int i = 12; i < e.Row.Cells.Count-1; i++)//// for (int i = 12; i < e.Row.Cells.Count; i++)
            ////{
            ////    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            ////}
            ////if (e.Row.RowType == DataControlRowType.DataRow)
            ////{
            ////    double d = 0.0;
            ////    int lastcell = e.Row.Cells.Count - 2;//// int lastcell = e.Row.Cells.Count - 1;

            ////    for (int i = 12; i < e.Row.Cells.Count-1; i++)////for (int i = 12; i < e.Row.Cells.Count; i++)
            ////    {
            ////        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            ////    }
            ////}
        }

        protected void grdITConsolidated_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdITConsolidated.PageIndex = e.NewPageIndex;
            LoadDeclarationClaims();
        }

        protected void grdITConsolidated_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////for (int i = 12; i < e.Row.Cells.Count-1; i++)//// for (int i = 12; i < e.Row.Cells.Count; i++)
            ////{
            ////    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            ////}
            ////if (e.Row.RowType == DataControlRowType.DataRow)
            ////{
            ////    double d = 0.0;
            ////    int lastcell = e.Row.Cells.Count - 2;//// int lastcell = e.Row.Cells.Count - 1;

            ////    for (int i = 12; i < e.Row.Cells.Count-1; i++)////for (int i = 12; i < e.Row.Cells.Count; i++)
            ////    {
            ////        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            ////    }
            ////}
        }
    }
}
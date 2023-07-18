using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP.CollectionBO;
using iTextSharp.text.html.simpleparser;

namespace iEmpPower.UI.FBP
{
    public partial class FBP_Declarations : System.Web.UI.Page
    {
        int MPbtnclick = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDeclarationClaims();
				HFselec.Value = "1";
            }
            if (txttodt.Text == "" && txtfrmdt.Text == "")
            {
                RFV_txttodt.Enabled = false;
                RFV_txttodt.Enabled = false;
            }
            
        }

        //protected void grdFbpDeclaration_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            ////((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Medical").SingleOrDefault()).Visible = true;
        //            ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "LTA").SingleOrDefault()).Visible = true;
        //            ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Meal Voucher").SingleOrDefault()).Visible = true;
        //            ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car EMI").SingleOrDefault()).Visible = true;
        //            ////((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Fuel").SingleOrDefault()).Visible = true;
        //            ////((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Driver's Salary").SingleOrDefault()).Visible = true;
        //            ////((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Insurance & Maintenance").SingleOrDefault()).Visible = true;
        //            ////((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Mobile & Telephone Reimbursement").SingleOrDefault()).Visible = true;
        //            ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Fuel Reimbursment-Self").SingleOrDefault()).Visible = true;
        //            ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Mobile Purchase").SingleOrDefault()).Visible = true;
        //            ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Related Reimbursement").SingleOrDefault()).Visible = true;
        //            ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Education").SingleOrDefault()).Visible = true;


        //            ////if (DataBinder.Eval(e.Row.DataItem, "AA_AMT01").ToString().Equals(""))
        //            ////{
        //            ////    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Medical").SingleOrDefault()).Visible = false;

        //            ////}

        //            if (DataBinder.Eval(e.Row.DataItem, "AA_AMT02").ToString().Equals(""))
        //            {
        //                ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "LTA").SingleOrDefault()).Visible = false;
        //            }
        //            if (DataBinder.Eval(e.Row.DataItem, "AA_AMT03").ToString().Equals(""))
        //            {
        //                ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Meal Voucher").SingleOrDefault()).Visible = false;

        //            }
        //            if (DataBinder.Eval(e.Row.DataItem, "AA_AMT04").ToString().Equals(""))
        //            {
        //                ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car EMI").SingleOrDefault()).Visible = false;

        //            }
        //            ////if (DataBinder.Eval(e.Row.DataItem, "AA_AMT05").ToString().Equals(""))
        //            ////{
        //            ////    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Fuel").SingleOrDefault()).Visible = false;

        //            ////}
        //            ////if (DataBinder.Eval(e.Row.DataItem, "AA_AMT06").ToString().Equals(""))
        //            ////{
        //            ////    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Driver's Salary").SingleOrDefault()).Visible = false;

        //            ////}
        //            ////if (DataBinder.Eval(e.Row.DataItem, "AA_AMT07").ToString().Equals(""))
        //            ////{
        //            ////    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Insurance & Maintenance").SingleOrDefault()).Visible = false;

        //            ////}
        //            if (DataBinder.Eval(e.Row.DataItem, "AA_AMT08").ToString().Equals(""))
        //            {
        //                ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Mobile & Telephone Reimbursement").SingleOrDefault()).Visible = false;

        //            }
        //            if (DataBinder.Eval(e.Row.DataItem, "AA_AMT09").ToString().Equals(""))
        //            {
        //                ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Fuel Reimbursment-Self").SingleOrDefault()).Visible = false;

        //            }
        //            if (DataBinder.Eval(e.Row.DataItem, "AA_AMT10").ToString().Equals(""))
        //            {
        //                ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Mobile Purchase").SingleOrDefault()).Visible = false;

        //            }
        //            if (DataBinder.Eval(e.Row.DataItem, "AA_AMT11").ToString().Equals(""))
        //            {
        //                ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Related Reimbursement").SingleOrDefault()).Visible = false;

        //            }
        //            if (DataBinder.Eval(e.Row.DataItem, "AA_AMT12").ToString().Equals(""))
        //            {
        //                ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Education").SingleOrDefault()).Visible = false;

        //            }

        //        }

        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        //}

        protected void ExportToExcel()
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);



            grdFbpDeclaration.GridLines = GridLines.Horizontal;
            //grdFbpDeclaration.CssClass = "Grid";
            grdFbpDeclaration.AllowPaging = false;
            grdFbpDeclaration.DataSource = Session["decl_data"];
            grdFbpDeclaration.DataBind();
            //LoadDeclarationClaims();



            List<FbpClaimbo> fbpboObj1 = Session["decl_data"] as List<FbpClaimbo>;
            load_footerGrid(fbpboObj1);



            // Render grid view control.
            htw.WriteBreak();
            for (int x = 0; x < grdFbpDeclaration.Rows.Count; x++)
            {
                grdFbpDeclaration.Rows[x].Cells[1].Attributes.Add("class", "textmode");
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
            grdFbpDeclaration.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
            grdFbpDeclaration.RenderControl(htw);





            htw.WriteBreak();





            // Write the rendered content to a file.





            //renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
            string renderedGridView = "FBP Declaration Details <br/>";
            renderedGridView += sw.ToString() + "<br/>";
            Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_FBPDeclaration.xls");
            Response.ContentType = "Application/vnd.ms-excel";



            Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            Response.Charset = "UTF-8";


            Response.Write(renderedGridView);
            Response.End();
            grdFbpDeclaration.AllowPaging = true;
            grdFbpDeclaration.CssClass = "gridviewNew";
            grdFbpDeclaration.DataSource = Session["decl_data"];
            grdFbpDeclaration.DataBind();
            //LoadDeclarationClaims();
            fbpboObj1 = Session["decl_data"] as List<FbpClaimbo>;
            load_footerGrid(fbpboObj1);





            //Newly added starts
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


            //htw.WriteBreak();

            //string colHeads = "FBP Declaration Details";
            //htw.WriteEncodedText(colHeads);

            //grdFbpDeclaration.GridLines = GridLines.Horizontal;

            //grdFbpDeclaration.AllowPaging = false;

            //grdFbpDeclaration.DataSource = Session["decl_data"];
            //grdFbpDeclaration.DataBind();

            ////searchdetails();
            ////grdFbpDeclaration.Columns[10].Visible = false;
            //grdFbpDeclaration.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
            //grdFbpDeclaration.RenderControl(htw);
            ////grdFbpDeclaration.Columns[10].Visible = true;
            //grdFbpDeclaration.AllowPaging = true;

            //htw.WriteBreak();


            //// Write the rendered content to a file.
            //string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();

            //renderedGridView += sw.ToString() + "<br/>";
            //Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_FBPDeclaration.xls");
            //Response.ContentType = "Application/vnd.ms-excel";

            //Response.ContentEncoding = System.Text.Encoding.Unicode;
            //Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            ////Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            ////Response.Charset = "UTF-8";

            //Response.Write(renderedGridView);
            //Response.End();
            //Newly added ends
            }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        private void ExportGridToPDF()
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_FBPDeclaration.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            grdFbpDeclaration.AllowPaging = false;
            grdFbpDeclaration.GridLines = GridLines.Horizontal;
            grdFbpDeclaration.DataSource = Session["decl_data"];
            grdFbpDeclaration.DataBind();
            List<FbpClaimbo> fbpboObj1 = Session["decl_data"] as List<FbpClaimbo>;
            load_footerGrid(fbpboObj1);
            StringWriter s_tw = new StringWriter();
            HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
            h_textw.AddStyleAttribute("font-size", "8pt");
            h_textw.AddStyleAttribute("color", "Black");





            string colHeads = "FBP Declaration Details";
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            h_textw.WriteBreak();
            grdFbpDeclaration.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
            //Table t = new Table();
            //TableItemStyle ts = new TableItemStyle();
            //ts.CssClass = "Grid";
            //t.Attributes.Add("class", "Grid");
            //t.CssClass = "Grid";
            //foreach (GridViewRow gv in grdFbpDeclaration.Rows)
            //{
            //    t.Rows.Add(gv);
            //}
            //foreach (TableRow rw in t.Rows)
            //    foreach (TableCell cel in rw.Cells)
            //        cel.ApplyStyle(ts);
            grdFbpDeclaration.RenderControl(h_textw);
            h_textw.WriteBreak();



            // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 





            //iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);
            //  Document doc = new Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();



            StringReader s_tr = new StringReader(s_tw.ToString());
            HTMLWorker html_worker = new HTMLWorker(doc);
            html_worker.Parse(s_tr);
            doc.Close();
            Response.Write(doc);
            grdFbpDeclaration.AllowPaging = true;
            grdFbpDeclaration.GridLines = GridLines.None;
            grdFbpDeclaration.DataSource = Session["decl_data"];
            grdFbpDeclaration.DataBind();
            fbpboObj1 = Session["decl_data"] as List<FbpClaimbo>;
            load_footerGrid(fbpboObj1);
        }

        public void LoadDeclarationClaims()
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                //fbpboObj1 = fbpblObj.Load_AllfbpDeclarationclaims_admin(User.Identity.Name.Trim(), txtsearch.Text.Trim(), MPbtnclick, false, txtfrmdt.Text, txttodt.Text);
                string EmpStatus = ddlEmpSts.SelectedValue.ToString();
                fbpboObj1 = fbpblObj.Load_AllfbpDeclarationclaims_admin(User.Identity.Name.Trim(), txtsearch.Text.Trim(), MPbtnclick, EmpStatus, txtfrmdt.Text, txttodt.Text);
                
                MsgCls("", lblmsg, Color.Transparent);
                //grdFbpDeclaration.Visible = true;
                grdFbpDeclaration.DataSource = fbpboObj1;
                grdFbpDeclaration.SelectedIndex = -1;
                grdFbpDeclaration.DataBind();
                Exportbtn.Visible = fbpboObj1.Count > 0 ? true : false;

                Session["decl_data"] = fbpboObj1;
                load_footerGrid(fbpboObj1);

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
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

        protected void BtnExporttoPDF_Click(object sender, EventArgs e)
        {
            try { ExportGridToPDF(); }
            catch (Exception ex) { }
        }

        protected void BtnExporttoXl_Click(object sender, EventArgs e)
        {
            try { ExportToExcel(); }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void grdFbpDeclaration_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdFbpDeclaration.PageIndex = e.NewPageIndex;
            grdFbpDeclaration.DataSource = Session["decl_data"];
            grdFbpDeclaration.DataBind();
            grdFbpDeclaration.SelectedIndex = -1;

            List<FbpClaimbo> fbpboObj1 = Session["decl_data"] as List<FbpClaimbo>;
            load_footerGrid(fbpboObj1);
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            //if (txtsearch.Text == "")
            //{
            LoadDeclarationClaims();
            //}
            //else
            //{
            //    searchdetails();
            //}
        }

        protected void ddlPagesizeEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdFbpDeclaration.PageSize = Convert.ToInt32(ddlPagesizeEmp.SelectedValue);
            //if (txtsearch.Text == "")
            //{
            LoadDeclarationClaims();
            //}
            //else
            //{
            //    searchdetails();
            //}
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
            HFselec.Value = "3";
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
            HFselec.Value = "2";
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
            HFselec.Value = "1";
        }

        protected void btnLastestRec_Click(object sender, EventArgs e)
        {
            try
            {
                txtsearch.Text = "";
                txttodt.Text = "";
                txtfrmdt.Text = "";
                MPbtnclick = Convert.ToInt32(HFselec.Value); //MPbtnclick = 0;
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                string EmpStatus = ddlEmpSts.SelectedValue.ToString();
                fbpboObj1 = fbpblObj.Load_AllfbpDeclarationclaims_admin(User.Identity.Name.Trim(), txtsearch.Text.Trim(), MPbtnclick, EmpStatus, txtfrmdt.Text, txttodt.Text);



                MsgCls("", lblmsg, Color.Transparent);
                //grdFbpDeclaration.Visible = true;
                grdFbpDeclaration.DataSource = fbpboObj1;
                grdFbpDeclaration.SelectedIndex = -1;
                grdFbpDeclaration.DataBind();
                Exportbtn.Visible = fbpboObj1.Count > 0 ? true : false;

                load_footerGrid(fbpboObj1);
                Disablemandte();

                Session["decl_data"] = fbpboObj1; 
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
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
        }

        //void LoadGridfilter()
        //{
        //    if (MPbtnclick == 1)
        //    {
                
        //    }

        //    else if (MPbtnclick == 2)
        //    {
               
        //    }

        //    else if (MPbtnclick == 3)
        //    {
                
        //    }
        //    else
        //    {
        //        btnCY.CssClass = "btn btn-xs btn-secondary";
        //        btnLY.CssClass = "btn btn-xs btn-light";
        //        btnAll.CssClass = "btn btn-xs btn-light";

        //        LoadDeclarationClaims();
        //    }
        //}

        //public void searchdetails()
        //{
        //    try
        //    {
        //        Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
        //        List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
        //        List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
        //        fbpboObj1 = fbpblObj.Load_AllfbpDeclarationclaims_admin(txtsearch.Text.Trim());


        //        MsgCls("", lblmsg, Color.Transparent);
        //        //grdFbpDeclaration.Visible = true;
        //        grdFbpDeclaration.DataSource = fbpboObj1;
        //        grdFbpDeclaration.SelectedIndex = -1;
        //        grdFbpDeclaration.DataBind();
        //        Exportbtn.Visible = true;
        //    }
        //    catch (Exception ex) { }
        //}

        protected void Disablemandte()
        {
            if (txttodt.Text == "" && txtfrmdt.Text == "")
            {
                RFV_txttodt.Enabled = false;
                RFV_txtfrmdt.Enabled = false;
            }
        }

        protected void load_footerGrid(List<FbpClaimbo> a)
        {
            if (a.Count > 0)
            {
                decimal total = 0, T_spll = 0, BasKTot = 0;
                Fbp_Claimbl objBl = new Fbp_Claimbl();
                FbpClaimscollectionbo objLst;
                for (int i = 0; i < a.Count; i++)
                {
                    objLst = objBl.GetBasketTotal(grdFbpDeclaration.Rows[i].Cells[2].Text.ToString().Trim());



                    grdFbpDeclaration.Rows[i].Cells[9].Text = objLst.Count > 0 ? objLst[0].BASKETTOTAL == "" ? "0.00" : Convert.ToDecimal(objLst[0].BASKETTOTAL).ToString("N2") : "0.00";
                    total = 0;
                    for (int j = 10; j < 18; j++)
                    {
                        total += Convert.ToDecimal(grdFbpDeclaration.Rows[i].Cells[j].Text);
                    }
                    grdFbpDeclaration.Rows[i].Cells[18].Text = total.ToString("N2");
                    grdFbpDeclaration.Rows[i].Cells[19].Text = (Convert.ToDecimal(grdFbpDeclaration.Rows[i].Cells[9].Text) - total).ToString("N2");
                }
                total = 0;
                for (int i = 0; i < a.Count; i++)
                {
                    BasKTot += Convert.ToDecimal(grdFbpDeclaration.Rows[i].Cells[9].Text);
                    total += Convert.ToDecimal(grdFbpDeclaration.Rows[i].Cells[18].Text);
                    T_spll += Convert.ToDecimal(grdFbpDeclaration.Rows[i].Cells[19].Text);
                }
                grdFbpDeclaration.FooterRow.Cells[9].Text = BasKTot.ToString("N2");
                grdFbpDeclaration.FooterRow.Cells[18].Text = total.ToString("N2");
                grdFbpDeclaration.FooterRow.Cells[19].Text = T_spll.ToString("N2");



                //decimal total = fbpboObj1.Sum(item => Convert.ToDecimal(item.AA_AMT02));
                grdFbpDeclaration.FooterRow.Cells[8].Text = "Total :";
                grdFbpDeclaration.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                grdFbpDeclaration.FooterRow.Cells[8].Font.Bold =
                    grdFbpDeclaration.FooterRow.Cells[9].Font.Bold =
                    grdFbpDeclaration.FooterRow.Cells[10].Font.Bold = grdFbpDeclaration.FooterRow.Cells[11].Font.Bold =
                    grdFbpDeclaration.FooterRow.Cells[12].Font.Bold = grdFbpDeclaration.FooterRow.Cells[13].Font.Bold =
                    grdFbpDeclaration.FooterRow.Cells[14].Font.Bold = grdFbpDeclaration.FooterRow.Cells[15].Font.Bold =
                    grdFbpDeclaration.FooterRow.Cells[16].Font.Bold = grdFbpDeclaration.FooterRow.Cells[17].Font.Bold =
                    grdFbpDeclaration.FooterRow.Cells[18].Font.Bold =
                    grdFbpDeclaration.FooterRow.Cells[19].Font.Bold = true;




                grdFbpDeclaration.FooterRow.Cells[10].Text = a.Sum(item => Convert.ToDecimal(item.AA_AMT02)).ToString("N2");
                grdFbpDeclaration.FooterRow.Cells[11].Text = a.Sum(item => Convert.ToDecimal(item.AA_AMT03)).ToString("N2");
                grdFbpDeclaration.FooterRow.Cells[12].Text = a.Sum(item => Convert.ToDecimal(item.AA_AMT04)).ToString("N2");
                grdFbpDeclaration.FooterRow.Cells[13].Text = a.Sum(item => Convert.ToDecimal(item.AA_AMT08)).ToString("N2");
                grdFbpDeclaration.FooterRow.Cells[14].Text = a.Sum(item => Convert.ToDecimal(item.AA_AMT09)).ToString("N2");
                grdFbpDeclaration.FooterRow.Cells[15].Text = a.Sum(item => Convert.ToDecimal(item.AA_AMT10)).ToString("N2");
                grdFbpDeclaration.FooterRow.Cells[16].Text = a.Sum(item => Convert.ToDecimal(item.AA_AMT11)).ToString("N2");
                grdFbpDeclaration.FooterRow.Cells[17].Text = a.Sum(item => Convert.ToDecimal(item.AA_AMT12)).ToString("N2");



            }
        }

        protected void ddlEmpSts_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDeclarationClaims();
        }
    }
}
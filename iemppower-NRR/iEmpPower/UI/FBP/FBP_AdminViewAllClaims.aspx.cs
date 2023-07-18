using iEmpPower.Old_App_Code.iEmpPowerBL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP;
using iEmpPower.Old_App_Code.iEmpPowerDAL.FBP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.html.simpleparser;
using System.Configuration;
using System.Data.SqlClient;

namespace iEmpPower.UI.FBP
{
    public partial class FBP_AdminViewAllClaims : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString);
        int MPbtnclick = 1;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                HFselec.Value = "1";
                Loadgrd_CalimsHistory();
                divitems.Visible = false;
                viewcheck.Value = "NO";
                //pageload();

            }



        }


        //void pageload()
        //{
        //    loadgrid();
        //    loaddata();

        //}


        private void Loadgrd_CalimsHistory()
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                fbpboObj1 = fbpblObj.Load_Allfbpclaims();
                Session.Add("FbpGrdInfo", fbpboObj1);

                if (fbpboObj1 == null || fbpboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                    grd_FbpClaims_History.Visible = false;
                    grd_FbpClaims_History.DataSource = null;
                    grd_FbpClaims_History.DataBind();
                    //  Exportbtn.Visible = false;
                    return;
                }
                else
                {
                    grd_FbpClaims_History.Visible = true;
                    grd_FbpClaims_History.DataSource = fbpboObj1;
                    grd_FbpClaims_History.SelectedIndex = -1;
                    grd_FbpClaims_History.DataBind();
                    //Exportbtn.Visible = true;
                }




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


        protected void grd_FbpClaims_History_RowCommand(object sender, GridViewCommandEventArgs e)
        {



            int rowIndex1 = int.Parse(e.CommandArgument.ToString());
            string LTA = (string)this.grd_FbpClaims_History.DataKeys[rowIndex1]["LGART"];

            if (LTA == "1215")
            {
                GridView1.Columns[7].Visible = true;

            }
            else
            {

                GridView1.Columns[7].Visible = false;


            }


            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
                        viewcheck.Value = "YES";
                        divitems.Visible = true;
                        MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in grd_FbpClaims_History.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }
                        Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                        FbpClaimbo fbpboObj = new FbpClaimbo();
                        List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();


                        ViewState["lgart"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["LGART"].ToString().Trim();

                        int fbpid = int.Parse(grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString().Trim());
                        ViewState["FBPID"] = int.Parse(grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString().Trim());
                        ViewState["status"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString().Trim();
                        ViewState["CreatedBy"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString().Trim();
                        ViewState["ENAME"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENAME"].ToString().Trim();
                        ViewState["BETRG"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["BETRG"].ToString().Trim();
                        ViewState["ALLOWANCETEXT"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["ALLOWANCETEXT"].ToString().Trim();
                        ViewState["OVERRIDE_AMT"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["OVERRIDE_AMT"].ToString().Trim();
                        ViewState["CREATED_ON"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString().Trim();
                        ViewState["APPROVEDON"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVEDON"].ToString().Trim();



                        Loadgrd_CalimsItems();


                        //Newly added starts


                        fbpboObj1 = fbpblObj.Get_BillDetails(fbpid);
                        GridView1.DataSource = fbpboObj1;
                        GridView1.DataBind();

                        GridView2.DataSource = fbpboObj1;
                        GridView2.DataBind();

                        ViewState["Remarks"] = fbpboObj1[0].REMARKS == null ? "" : fbpboObj1[0].REMARKS.ToString().Trim();

                        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                        {
                            using (Label lbltype = (Label)GridView2.Rows[i].FindControl("lblItem"))
                            {
                                lbltype.Text = ViewState["ALLOWANCETEXT"].ToString();
                            }
                        }
                        double total = 0.0;
                        Label lblAmt;
                        for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                        {
                            GridViewRow row = GridView2.Rows[i];
                            lblAmt = (Label)row.Cells[4].FindControl("lblamt");
                            total += lblAmt.Text == "" ? 0.0 : Convert.ToDouble(lblAmt.Text);
                        }
                        Label tot = (Label)GridView2.FooterRow.FindControl("lblTotalAmt");
                        tot.Text = total.ToString("#0.00");


                        ClaimDate.InnerHtml = ViewState["CREATED_ON"].ToString();
                        ClaimID.InnerHtml = ViewState["FBPID"].ToString();
                        EmpName.InnerHtml = ViewState["ENAME"].ToString();////Session["EmployeeName"].ToString();
                        EmpPERNR.InnerHtml = ViewState["CreatedBy"].ToString();//// User.Identity.Name.ToString();
                        tdRemarks.InnerHtml = "Remarks : " + ViewState["Remarks"].ToString();
                        //Newly added starts

                        break;
                    default:
                        break;


                }
            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        private void Loadgrd_CalimsItems()
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                string Perner = ViewState["CreatedBy"].ToString().Trim();
                fbpboObj1 = fbpblObj.Load_FbpClaim_Details(Perner.Trim(), ViewState["lgart"].ToString().Trim());


                if (fbpboObj1 == null || fbpboObj1.Count == 0)
                {
                    // MsgCls("No Records Found !", Label1, Color.Red);
                    grd_CalimsItems.Visible = false;
                    grd_CalimsItems.DataSource = null;
                    grd_CalimsItems.DataBind();
                    return;
                }
                else
                {
                    grd_CalimsItems.Visible = true;
                    grd_CalimsItems.DataSource = fbpboObj1;
                    grd_CalimsItems.SelectedIndex = -1;
                    grd_CalimsItems.DataBind();
                }
                Session["dataFBPViewAll"] = fbpboObj1;

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }


        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DOWNLOAD":
                    //  string filename= grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FPATH"].ToString();
                    string filePath = e.CommandArgument.ToString();
                    Response.ContentType = "application/octet-stream";
                    //Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                    Response.WriteFile(filePath);
                    Response.End();
                    break;
                default:
                    break;
            }

        }

        protected void grd_FbpClaims_History_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int pageindex = e.NewPageIndex;
                grd_FbpClaims_History.PageIndex = e.NewPageIndex;
                grd_FbpClaims_History.DataSource = Session["dataFBPViewAll"];
                grd_FbpClaims_History.DataBind();
                //Loadgrd_CalimsHistory();
                //search();
                grd_FbpClaims_History.SelectedIndex = -1;
                divitems.Visible = false;
                viewcheck.Value = "NO";

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void grd_FbpClaims_History_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                Loadgrd_CalimsHistory();
                search();
                viewcheck.Value = "NO";
                List<FbpClaimbo> FbpboList = (List<FbpClaimbo>)Session["FbpGrdInfo"];
                bool objSortOrder = (bool)Session["bSortedOrder"];
                switch (e.SortExpression.ToString().Trim())
                {
                    case "CREATED_BY":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return ((int.Parse(objBo1.CREATED_BY)).CompareTo(int.Parse(objBo2.CREATED_BY))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return ((int.Parse(objBo2.CREATED_BY)).CompareTo(int.Parse(objBo1.CREATED_BY))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "ENAME":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;
                    case "FBPC_IC":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return (objBo1.FBPC_IC.Value.CompareTo(objBo2.FBPC_IC.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return (objBo2.FBPC_IC.Value.CompareTo(objBo1.FBPC_IC.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;



                    case "LGART":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return (objBo1.LGART.ToString().CompareTo(objBo2.LGART.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return (objBo2.LGART.ToString().CompareTo(objBo1.LGART.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "STATUS":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return (objBo1.STATUS.ToString().CompareTo(objBo2.STATUS.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return (objBo2.STATUS.ToString().CompareTo(objBo1.STATUS.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "BETRG":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return ((decimal.Parse(objBo1.BETRG)).CompareTo(decimal.Parse(objBo2.BETRG))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return ((decimal.Parse(objBo2.BETRG)).CompareTo(decimal.Parse(objBo1.BETRG))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "APPAMT":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return ((decimal.Parse(objBo1.APPAMT)).CompareTo(decimal.Parse(objBo2.APPAMT))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return ((decimal.Parse(objBo2.APPAMT)).CompareTo(decimal.Parse(objBo1.APPAMT))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "OVERRIDE_AMT":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return ((decimal.Parse(objBo1.OVERRIDE_AMT)).CompareTo(decimal.Parse(objBo2.OVERRIDE_AMT))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return ((decimal.Parse(objBo2.OVERRIDE_AMT)).CompareTo(decimal.Parse(objBo1.OVERRIDE_AMT))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;
                    case "CREATED_ON":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return (objBo1.CREATED_ON.Value.CompareTo(objBo2.CREATED_ON.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return (objBo2.CREATED_ON.Value.CompareTo(objBo1.CREATED_ON.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                    case "BEGDA":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return (objBo1.BEGDA.Value.CompareTo(objBo2.BEGDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return (objBo2.BEGDA.Value.CompareTo(objBo1.BEGDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                    case "APPROVEDON":
                        if (objSortOrder)
                        {
                            if (FbpboList != null)
                            {
                                FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                                { return (objBo1.APPROVEDON.Value.CompareTo(objBo2.APPROVEDON.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            FbpboList.Sort(delegate(FbpClaimbo objBo1, FbpClaimbo objBo2)
                            { return (objBo2.APPROVEDON.Value.CompareTo(objBo1.APPROVEDON.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                }

                grd_FbpClaims_History.DataSource = FbpboList;
                grd_FbpClaims_History.DataBind();

                Session.Add("FbpGrdInfo", FbpboList);
                divitems.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtCreatedOn.Text = string.Empty;
            Loadgrd_CalimsHistory();
            divitems.Visible = false;
            MsgCls("", lblMessageBoard, Color.Transparent);
            viewcheck.Value = "NO";
            txtsearch.Focus();
        }

        public void search()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;
                DateTime createdon = DateTime.Parse(string.IsNullOrEmpty(txtCreatedOn.Text) ? "01/01/0001" : txtCreatedOn.Text);
                DateTime createdonto = DateTime.Parse(string.IsNullOrEmpty(txtCreatedOn.Text) ? "01/01/0001" : txtCreatedOnto.Text);
                if (SelectedType != "0" && textSearch == "")
                {
                    MsgCls("Please Enter the Text", lblMessageBoard, Color.Red);
                }

                else if (SelectedType == "0" && textSearch != "")
                {
                    MsgCls("Please Select the Type", lblMessageBoard, Color.Red);
                }
                else
                {
                    Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                    List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                    List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                    fbpboObj1 = fbpblObj.Load_ParticularfbpclaimsAdmin(SelectedType, textSearch, createdon, createdonto, MPbtnclick.ToString());



                    if (fbpboObj1 == null || fbpboObj1.Count == 0)
                    {
                        MsgCls("No Records found", lblMessageBoard, Color.Red);
                        grd_FbpClaims_History.Visible = false;
                        grd_FbpClaims_History.DataSource = null;
                        grd_FbpClaims_History.DataBind();
                        return;
                    }
                    else
                    {
                        MsgCls("", lblMessageBoard, Color.Transparent);
                        grd_FbpClaims_History.Visible = true;
                        grd_FbpClaims_History.DataSource = fbpboObj1;
                        grd_FbpClaims_History.SelectedIndex = -1;
                        grd_FbpClaims_History.DataBind();
                        divitems.Visible = false;
                    }
                    Session["dataFBPViewAll"] = fbpboObj1;
                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, Color.Red);
            }

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                search();
                viewcheck.Value = "NO";
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
            txtsearch.Focus();
        }

        protected void ExportToPDF_Click(object sender, EventArgs e)
        {
            ExportGridToPDF();
            txtsearch.Focus();
        }

        protected void ExportToExcel()
        {

            if (viewcheck.Value == "YES")
            {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                GridView1.GridLines = GridLines.Horizontal;
                grd_CalimsItems.GridLines = GridLines.Horizontal;
                // Render grid view control.
                htw.WriteBreak();

                string colHeads = "FBP Claim Pending Details";
                htw.WriteEncodedText(colHeads);
                grd_CalimsItems.RenderControl(htw);
                htw.WriteBreak();

                colHeads = "FBP Claim Details";
                htw.WriteEncodedText(colHeads);
                GridView1.RenderControl(htw);
                htw.WriteBreak();

                // Write the rendered content to a file.
                GridView1.GridLines = GridLines.None;
                grd_CalimsItems.GridLines = GridLines.None;
                // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                string renderedGridView = "FBP Claim Details <br/>";
                renderedGridView += "<table><tr><td align=left>FBP Claim ID</td><td align=left>:</td><td align=left>" + ViewState["FBPID"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Allowance ID</td><td align=left>:</td><td align=left>" + ViewState["lgart"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Allowance Text</td><td align=left>:</td><td align=left>" + ViewState["ALLOWANCETEXT"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["CreatedBy"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["ENAME"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Claimed Amount</td><td align=left>:</td><td align=left>" + ViewState["BETRG"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Override Amount</td><td align=left>:</td><td align=left>" + ViewState["OVERRIDE_AMT"] + "</td></tr>";
                renderedGridView += "<tr><td align=left>Created On</td><td align=left>:</td><td align=left>" + ViewState["CREATED_ON"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Approved On</td><td align=left>:</td><td align=left>" + ViewState["APPROVEDON"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Status</td><td align=left>:</td><td align=left>" + ViewState["status"].ToString() + "</td></tr></table>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_FBPClaim.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();
            }
            else
            {

                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                grd_FbpClaims_History.GridLines = GridLines.Horizontal;
                // Render grid view control.
                htw.WriteBreak();
                grd_FbpClaims_History.AllowPaging = false;
                search();
                grd_FbpClaims_History.Columns[12].Visible = false;
                grd_FbpClaims_History.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                grd_FbpClaims_History.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                grd_FbpClaims_History.RenderControl(htw);
                grd_FbpClaims_History.Columns[12].Visible = true;
                grd_FbpClaims_History.AllowPaging = true;
                grd_FbpClaims_History.GridLines = GridLines.None;
                htw.WriteBreak();


                // Write the rendered content to a file.

                // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                string renderedGridView = "FBP Claim Details <br/>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_FBPClaim.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        private void ExportGridToPDF()
        {
            if (viewcheck.Value == "YES")
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_FBPClaim.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                grd_CalimsItems.GridLines = GridLines.Horizontal;
                GridView1.GridLines = GridLines.Horizontal;
                StringWriter s_tw = new StringWriter();
                HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                h_textw.AddStyleAttribute("font-size", "8pt");
                h_textw.AddStyleAttribute("color", "Black");

                string colHeads = "FBP Claim Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "FBP Claim ID :" + ViewState["FBPID"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Allowance ID :" + ViewState["lgart"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Allowance Text :" + ViewState["ALLOWANCETEXT"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Employee ID :" + ViewState["CreatedBy"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Employee Name :" + ViewState["ENAME"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Claimed Amount :" + ViewState["BETRG"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Override Amount :" + ViewState["OVERRIDE_AMT"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Created On :" + ViewState["CREATED_ON"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Approved On :" + ViewState["APPROVEDON"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Status :" + ViewState["status"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();


                // h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "FBP Claim Pending Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                grd_CalimsItems.RenderControl(h_textw);
                h_textw.WriteBreak();

                colHeads = "FBP Claim Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                GridView1.RenderControl(h_textw);
                h_textw.WriteBreak();


                // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 
                grd_CalimsItems.GridLines = GridLines.None;
                GridView1.GridLines = GridLines.None;
                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

                //  Document doc = new Document();
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                doc.Open();
                StringReader s_tr = new StringReader(s_tw.ToString());
                // iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                HTMLWorker html_worker = new HTMLWorker(doc);

                html_worker.Parse(s_tr);
                doc.Close();
                Response.Write(doc);
            }
            else
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_FBPClaim.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                grd_FbpClaims_History.GridLines = GridLines.Horizontal;
                StringWriter s_tw = new StringWriter();
                HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                h_textw.AddStyleAttribute("font-size", "8pt");
                h_textw.AddStyleAttribute("color", "Black");



                h_textw.WriteBreak();
                string colHeads = "FBP Claim Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                grd_FbpClaims_History.AllowPaging = false;

                search();
                grd_FbpClaims_History.Columns[12].Visible = false;
                grd_FbpClaims_History.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                grd_FbpClaims_History.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                grd_FbpClaims_History.RenderControl(h_textw);
                grd_FbpClaims_History.Columns[12].Visible = true;
                grd_FbpClaims_History.AllowPaging = true;
                grd_FbpClaims_History.GridLines = GridLines.None;
                h_textw.WriteBreak();

                // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                //iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);

                ////  Document doc = new Document();
                //iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                //doc.Open();
                //StringReader s_tr = new StringReader(s_tw.ToString());
                //HTMLWorker html_worker = new HTMLWorker(doc);
                //html_worker.Parse(s_tr);
                //doc.Close();
                //Response.Write(doc);

                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);
                //  Document doc = new Document();
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                doc.Open();

                StringReader s_tr = new StringReader(s_tw.ToString());
                HTMLWorker html_worker = new HTMLWorker(doc);
                html_worker.Parse(s_tr);
                doc.Close();
                Response.Write(doc);



                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_FBPClaim.pdf");
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //StringWriter sw = new StringWriter();
                //HtmlTextWriter hw = new HtmlTextWriter(sw);
                //grd_FbpClaims_History.AllowPaging = false;
                //search();
                //grd_FbpClaims_History.Columns[12].Visible = false;
                //grd_FbpClaims_History.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                //grd_FbpClaims_History.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                //grd_FbpClaims_History.RenderControl(hw);
                //grd_FbpClaims_History.Columns[12].Visible = true;
                //grd_FbpClaims_History.AllowPaging = true;
                //StringReader sr = new StringReader(sw.ToString());
                //iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 0f);
                //iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
                //iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                //pdfDoc.Open();
                //htmlparser.Parse(sr);
                //pdfDoc.Close();
                //Response.Write(pdfDoc);
                //Response.End(); 
            }
        }

        protected void txtCreatedOnto_TextChanged(object sender, EventArgs e)
        {
            if (txtCreatedOnto.Text == "")
            {
                txtCreatedOnto.Text = txtCreatedOn.Text;
            }

            if (txtCreatedOnto.Text != "" && txtCreatedOn.Text != "")
            {
                //MPbtnclick = 3;
                //btnCY.CssClass = "btn btn-xs btn-light";
                //btnLY.CssClass = "btn btn-xs btn-light";
                //btnAll.CssClass = "btn btn-xs btn-secondary";
                //search();
                if (DateTime.Parse(txtCreatedOn.Text.Trim()) < DateTime.Parse(txtCreatedOnto.Text.Trim()))
                {
                    MPbtnclick = 3;
                    btnCY.CssClass = "btn btn-xs btn-light";
                    btnLY.CssClass = "btn btn-xs btn-light";
                    btnAll.CssClass = "btn btn-xs btn-secondary";
                    search();
                }
                else
                {
                    txtCreatedOnto.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('From date must be less than To date!');", true);
                }
            }
        }

        protected void txtCreatedOn_TextChanged(object sender, EventArgs e)
        {
            if (txtCreatedOn.Text == "")
            {
                txtCreatedOn.Text = txtCreatedOnto.Text;
            }

            if (txtCreatedOnto.Text != "" && txtCreatedOn.Text != "")
            {
                //MPbtnclick = 3;
                //btnCY.CssClass = "btn btn-xs btn-light";
                //btnLY.CssClass = "btn btn-xs btn-light";
                //btnAll.CssClass = "btn btn-xs btn-secondary";
                //search();
                if (DateTime.Parse(txtCreatedOn.Text.Trim()) < DateTime.Parse(txtCreatedOnto.Text.Trim()))
                {
                    MPbtnclick = 3;
                    btnCY.CssClass = "btn btn-xs btn-light";
                    btnLY.CssClass = "btn btn-xs btn-light";
                    btnAll.CssClass = "btn btn-xs btn-secondary";
                    search();
                }
                else
                {
                    txtCreatedOnto.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('From date must be less than To date!');", true);
                }
            }
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        protected void btnCY_Click(object sender, EventArgs e)
        {
            txtCreatedOnto.Text = txtCreatedOn.Text = "";
            MPbtnclick = 1;
            btnCY.CssClass = "btn btn-xs btn-secondary";
            btnLY.CssClass = "btn btn-xs btn-light";
            btnAll.CssClass = "btn btn-xs btn-light";

            //txt.Text = "";
            //txtfrmdt.Text = "";
            search();

            //Disablemandte();
            HFselec.Value = "1";
        }

        protected void btnLY_Click(object sender, EventArgs e)
        {
            txtCreatedOnto.Text = txtCreatedOn.Text = "";
            MPbtnclick = 2;
            btnCY.CssClass = "btn btn-xs btn-light";
            btnLY.CssClass = "btn btn-xs btn-secondary";
            btnAll.CssClass = "btn btn-xs btn-light";

            //txt.Text = "";
            //txtfrmdt.Text = "";
            search();

            //Disablemandte();
            HFselec.Value = "2";
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            MPbtnclick = 3;
            btnCY.CssClass = "btn btn-xs btn-light";
            btnLY.CssClass = "btn btn-xs btn-light";
            btnAll.CssClass = "btn btn-xs btn-secondary";

            //txt.Text = "";
            //txtfrmdt.Text = "";
            search();

            //Disablemandte();
            HFselec.Value = "3";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //LinkButton lnbtn = row.FindControl("LnkbtnFbp") as RequiredFieldValidator;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (ViewState["lgart"].ToString().Trim() == "1255")
                    {
                        int lid = (e.Row.Cells[7].Text == "" || e.Row.Cells[7].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[7].Text);
                        FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
                        string data = "";
                        int id = (e.Row.Cells[8].Text == "" || e.Row.Cells[8].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[8].Text);
                        objDataContext.usp_FBP_get_user_mob_no_FBPID(id, lid, 3, "", ref data);




                        Label lblMobNo = e.Row.FindControl("lblMobNo") as Label;
                        lblMobNo.Text = data;



                        GridView1.Columns[4].Visible = true;





                    }
                    else
                        GridView1.Columns[4].Visible = false;
                }


            }




            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }



        }

        void ViewFBPClaims(string fbpid1)
        {


            try
            {
                viewcheck.Value = "YES";
                divitems.Visible = true;
                MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
                //int rowIndex = Convert.ToInt32(e.CommandArgument);



                //foreach (GridViewRow row in grd_FbpClaims_History.Rows)
                //{
                //    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                //    System.Drawing.Color.LightGray :
                //    System.Drawing.Color.White;
                //}
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();




                int fbpid = int.Parse(fbpid1);//int.Parse(grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString().Trim());



                fbpboObj1 = fbpblObj.Get_BillDetails(fbpid);
                GridView1.DataSource = fbpboObj1;
                GridView1.DataBind();
                GridView2.DataSource = fbpboObj1;
                GridView2.DataBind();



                ViewState["lgart"] = Session["fbp_lgart"];//fbpboObj1[0].LGART == null ? "" : fbpboObj1[0].LGART.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["LGART"].ToString().Trim();
                ViewState["FBPID"] = fbpid;// int.Parse(grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString().Trim());
                ViewState["status"] = fbpboObj1[0].STATUS == null ? "" : fbpboObj1[0].STATUS.ToString().Trim();//Newly added//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString().Trim();



                ViewState["BETRG"] = fbpboObj1[0].BETRG == null ? "" : fbpboObj1[0].BETRG.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["BETRG"].ToString().Trim();
                ViewState["ALLOWANCETEXT"] = Session["ALLOWANCETEXT"];// fbpboObj1[0].ALLOWANCETEXT == null ? "" : fbpboObj1[0].ALLOWANCETEXT.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["ALLOWANCETEXT"].ToString().Trim();
                ViewState["OVERRIDE_AMT"] = fbpboObj1[0].OVERRIDE_AMT == null ? "" : fbpboObj1[0].OVERRIDE_AMT.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["OVERRIDE_AMT"].ToString().Trim();
                ViewState["CREATED_ON"] = Session["fbp_createdOn"];//fbpboObj1[0].CREATED_ON == null ? "" : fbpboObj1[0].CREATED_ON.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString().Trim();
                ViewState["APPROVEDON"] = fbpboObj1[0].APPROVEDON == null ? "" : fbpboObj1[0].APPROVEDON.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVEDON"].ToString().Trim();
                ViewState["Remarks"] = fbpboObj1[0].REMARKS == null ? "" : fbpboObj1[0].REMARKS.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["REMARKS"].ToString().Trim();
                Loadgrd_CalimsItems();
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    using (Label lbltype = (Label)GridView2.Rows[i].FindControl("lblItem"))
                    {
                        lbltype.Text = ViewState["ALLOWANCETEXT"].ToString();
                    }
                }
                double total = 0.0;
                Label lblAmt;
                for (int i = 0; i <= GridView2.Rows.Count - 1; i++)
                {
                    GridViewRow row = GridView2.Rows[i];
                    lblAmt = (Label)row.Cells[4].FindControl("lblamt");
                    total += lblAmt.Text == "" ? 0.0 : Convert.ToDouble(lblAmt.Text);
                }
                Label tot = (Label)GridView2.FooterRow.FindControl("lblTotalAmt");
                tot.Text = total.ToString("#0.00");
                ClaimDate.InnerHtml = ViewState["CREATED_ON"].ToString();
                ClaimID.InnerHtml = ViewState["FBPID"].ToString();
                EmpName.InnerHtml = Session["EmployeeName"].ToString();
                EmpPERNR.InnerHtml = User.Identity.Name.ToString();
                tdRemarks.InnerHtml = "Remarks : " + ViewState["Remarks"].ToString();



                //fbpboObj1 = fbpblObj.Get_BillDetails(fbpid);
                //GridView1.DataSource = fbpboObj1;
                //GridView1.DataBind();



                //if (ViewState["status"].ToString().Trim() == "Requested")
                //{
                //    divbutton.Visible = true;
                //    txtRemarks.Focus();
                //}
                //else
                //{
                //    divbutton.Visible = false;
                //}
            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void LnkbtnFbp_Click(object sender, EventArgs e)
        {
            Grdbind.Visible = true;
            divLTA.Visible = true;
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
            int id1 = Convert.ToInt32(grd_FbpClaims_History.DataKeys[rowIndex].Values[0]);
            SqlCommand cmd = new SqlCommand("usp_get_Relationships", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FBPC_ID", id1);


            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            Grdbind.DataSource = ds;
            Grdbind.DataBind();


            SqlCommand cmd1 = new SqlCommand("usp_get_travel_details", con);


            cmd1.CommandType = CommandType.StoredProcedure;
            //  cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
            cmd1.Parameters.AddWithValue("@FBPC_ID", id1);


            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);

            Grdload.DataSource = ds1;
            Grdload.DataBind();
            SqlCommand cmd2 = new SqlCommand("usp_getdetails_of_travel", con);


            cmd2.CommandType = CommandType.StoredProcedure;
            //  cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
            cmd2.Parameters.AddWithValue("@FBPC_ID", id1);


            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            sda2.Fill(ds2);

            Grdltadetails.DataSource = ds2;
            Grdltadetails.DataBind();

        }

    }


}









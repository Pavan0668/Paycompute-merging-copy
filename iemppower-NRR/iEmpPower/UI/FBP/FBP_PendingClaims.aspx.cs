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

namespace iEmpPower.UI.FBP
{
    public partial class FBP_PendingClaims : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Loadgrd_CalimsHistory();
                divitems.Visible = false;
                viewcheck.Value = "NO";

                if (Request.QueryString["NC"] != null)
                {
                    if (Request.QueryString["NC"] == "C")
                    {
                        if (Session["fbpid"] != null)
                        {
                            ViewFBPClaims(Session["fbpid"].ToString());
                            goto displayInfo;
                        }
                    }
                    else if (Request.QueryString["NC"] == "N")
                    {
                        Session["fbpid"] = null;
                        Session.Clear();
                    }
                }
            displayInfo:
                {
                    ////Console.WriteLine("");
                }
            }
        }

        //void ViewFBPClaims(string fbpid1)
        //{
        //    try
        //    {
        //        viewcheck.Value = "YES";
        //        divitems.Visible = true;
        //        MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
        //        //int rowIndex = Convert.ToInt32(e.CommandArgument);

        //        //foreach (GridViewRow row in grd_FbpClaims_History.Rows)
        //        //{
        //        //    row.BackColor = row.RowIndex.Equals(rowIndex) ?
        //        //    System.Drawing.Color.LightGray :
        //        //    System.Drawing.Color.White;
        //        //}
        //        Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
        //        FbpClaimbo fbpboObj = new FbpClaimbo();
        //        List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();


        //        int fbpid = int.Parse(fbpid1);//int.Parse(grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString().Trim());

        //        fbpboObj1 = fbpblObj.Get_BillDetails(fbpid);
        //        GridView1.DataSource = fbpboObj1;
        //        GridView1.DataBind();

        //        ViewState["lgart"] = fbpboObj1[0].LGART == null ? "" : fbpboObj1[0].LGART.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["LGART"].ToString().Trim();
        //        ViewState["FBPID"] = fbpid;// int.Parse(grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString().Trim());
        //        ViewState["status"] = fbpboObj1[0].STATUS == null ? "" : fbpboObj1[0].STATUS.ToString().Trim();//Newly added//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString().Trim();

        //        ViewState["BETRG"] = fbpboObj1[0].BETRG == null ? "" : fbpboObj1[0].BETRG.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["BETRG"].ToString().Trim();
        //        ViewState["ALLOWANCETEXT"] = fbpboObj1[0].ALLOWANCETEXT == null ? "" : fbpboObj1[0].ALLOWANCETEXT.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["ALLOWANCETEXT"].ToString().Trim();
        //        ViewState["OVERRIDE_AMT"] = fbpboObj1[0].OVERRIDE_AMT == null ? "" : fbpboObj1[0].OVERRIDE_AMT.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["OVERRIDE_AMT"].ToString().Trim();
        //        ViewState["CREATED_ON"] = fbpboObj1[0].CREATED_ON == null ? "" : fbpboObj1[0].CREATED_ON.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString().Trim();
        //        ViewState["APPROVEDON"] = fbpboObj1[0].APPROVEDON == null ? "" : fbpboObj1[0].APPROVEDON.ToString().Trim();//grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVEDON"].ToString().Trim();

        //        Loadgrd_CalimsItems();




        //        //fbpboObj1 = fbpblObj.Get_BillDetails(fbpid);
        //        //GridView1.DataSource = fbpboObj1;
        //        //GridView1.DataBind();

        //        if (ViewState["status"].ToString().Trim() == "Requested")
        //        {
        //            divbutton.Visible = true;
        //            txtRemarks.Focus();
        //        }
        //        else
        //        {
        //            divbutton.Visible = false;
        //        }
        //    }

        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}


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



                if (ViewState["status"].ToString().Trim() == "Requested")
                {
                    divbutton.Visible = true;
                    txtRemarks.Focus();
                }
                else
                {
                    divbutton.Visible = false;
                }
            }



            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void Loadgrd_CalimsHistory()
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                string ApproverId = User.Identity.Name;
                fbpboObj1 = fbpblObj.Load_fbpclaims_History(ApproverId.Trim());
                Session.Add("FbpGrdInfo", fbpboObj1);

                if (fbpboObj1 == null || fbpboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                    grd_FbpClaims_History.Visible = false;
                    grd_FbpClaims_History.DataSource = null;
                    grd_FbpClaims_History.DataBind();
                    Exportbtn.Visible = false;
                    return;
                }
                else
                {
                    grd_FbpClaims_History.Visible = true;
                    grd_FbpClaims_History.DataSource = fbpboObj1;
                    grd_FbpClaims_History.SelectedIndex = -1;
                    grd_FbpClaims_History.DataBind();
                    Exportbtn.Visible = true;
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

                        ViewState["BETRG"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["BETRG"].ToString().Trim();
                        ViewState["ALLOWANCETEXT"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["ALLOWANCETEXT"].ToString().Trim();
                        ViewState["OVERRIDE_AMT"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["OVERRIDE_AMT"].ToString().Trim();
                        ViewState["CREATED_ON"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString().Trim();
                        ViewState["APPROVEDON"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVEDON"].ToString().Trim();
                        ViewState["Remarks"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["REMARKS"].ToString().Trim();


                        Loadgrd_CalimsItems();




                        fbpboObj1 = fbpblObj.Get_BillDetails(fbpid);
                        GridView1.DataSource = fbpboObj1;
                        GridView1.DataBind();

                        //-----------------------------------
                        GridView2.DataSource = fbpboObj1;
                        GridView2.DataBind();



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
                        //-----------------------------------



                        if (ViewState["status"].ToString().Trim() == "Requested")
                        {
                            divbutton.Visible = true;
                            txtRemarks.Focus();
                        }
                        else
                        {
                            divbutton.Visible = false;
                        }




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
                string ApproverId = User.Identity.Name;
                fbpboObj1 = fbpblObj.Load_FbpClaim_Details(ApproverId.Trim(), ViewState["lgart"].ToString().Trim());


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
                Loadgrd_CalimsHistory();
                search();
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

        protected void btnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                divbutton.Visible = false;
                divitems.Visible = false;

                bool? Status = true;

                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.FBPC_IC = int.Parse(ViewState["FBPID"].ToString().Trim());
                fbpboObj.STATUS = "Withdraw";
                fbpblObj.Update_FbpClaim_Status(fbpboObj, ref Status);
                if (Status.Equals(false))
                {
                    SendMailMethod(int.Parse(ViewState["FBPID"].ToString().Trim()), "withdraw");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('FBP Claim has been Withdrawed successfully !')", true);
                    Loadgrd_CalimsHistory();
                    divitems.Visible = false;
                    MsgCls("", lblMessageBoard, Color.Transparent);
                    viewcheck.Value = "NO";
                }
                txtsearch.Focus();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void SendMailMethod(int fbpidmail, string status)
        {
            try
            {
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
                GridView1.FooterRow.Visible = false;
                GridView1.RenderControl(hw1);


                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;
                string Entitlement = "";
                string amount = "";
                string bedga = "";
                string EMP_Name = "";
                string EMP_Email = "";
                string Entity = "";
                string ovrrideamt = "";
                string adminremarks = "";
                string allowancetxt = "";
                FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();

                objcontext.usp_Fbp_Get_MailList_Fbp(fbpidmail, User.Identity.Name, "Submit", ref EMP_Name, ref EMP_Email, ref Entity,
                      ref Entitlement, ref amount, ref bedga, ref ovrrideamt, ref adminremarks, ref allowancetxt);

                strSubject = "FBP Claim " + fbpidmail + " has been withdrawn by " + EMP_Name + "  |  " + User.Identity.Name + ".";


                //RecipientsString = "monica.ks@itchamps.com";
                //strPernr_Mail = "latha.mg@itchamps.com";

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>FBP Claim " + fbpidmail + " has been withdrawn by " + EMP_Name + "  |  " + User.Identity.Name + ".<br/><br/></b>";
                body += "<b>FBP Claim Details</b><br/><hr>";
                body += "<table><tr><td>FBP Claim ID </td><td> : </td><td>" + fbpidmail + "</td></tr>";
                body += "<tr><td>Allowance  </td><td>:  </td><td>" + Entitlement + " - " + allowancetxt + "</td></tr>";
                body += "<tr><td>Date   </td><td>  : </td> <td>" + bedga + "</td></tr>";
                body += "<tr><td>Total Amount</td><td>  : </td><td>  " + decimal.Parse(amount).ToString("N2") + "</td></tr>";
                body += "<tr><td>Remarks</td><td>  : </td><td>  " + txtRemarks.Text + "</td></tr></table> <br/>";

                body += sw1.ToString() + "<br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('FBP Claims has been withdrawed successfully. Error in sending mail');", true);
                return;
            }
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtCreatedOn.Text = string.Empty;
            Loadgrd_CalimsHistory();
            divitems.Visible = false;
            MsgCls("", lblMessageBoard, Color.Transparent);
            txtsearch.Focus();
            viewcheck.Value = "NO";
        }



        public void search()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;
                DateTime createdon = DateTime.Parse(string.IsNullOrEmpty(txtCreatedOn.Text) ? "01/01/0001" : txtCreatedOn.Text);

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
                    fbpboObj1 = fbpblObj.Load_Particularfbpclaims_History(User.Identity.Name, SelectedType, textSearch, createdon);



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

                //   renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                string renderedGridView = "FBP Claim Details <br/>";
                renderedGridView += "<table><tr><td align=left>FBP Claim ID</td><td align=left>:</td><td align=left>" + ViewState["FBPID"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Allowance ID</td><td align=left>:</td><td align=left>" + ViewState["lgart"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Allowance Text</td><td align=left>:</td><td align=left>" + ViewState["ALLOWANCETEXT"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["CreatedBy"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["ENAME"].ToString() + "</td></tr>";
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

                // Render grid view control.
                htw.WriteBreak();
                grd_FbpClaims_History.AllowPaging = false;
                search();
                grd_FbpClaims_History.Columns[10].Visible = false;
                grd_FbpClaims_History.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                grd_FbpClaims_History.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                grd_FbpClaims_History.RenderControl(htw);
                grd_FbpClaims_History.Columns[10].Visible = true;
                grd_FbpClaims_History.AllowPaging = true;

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
                //colHeads = "Employee ID :" + ViewState["CreatedBy"].ToString();
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //colHeads = "Employee Name :" + ViewState["ENAME"].ToString();
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
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

                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

                //  Document doc = new Document();
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                doc.Open();
                StringReader s_tr = new StringReader(s_tw.ToString());
                iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                html_worker.Parse(s_tr);
                doc.Close();
                Response.Write(doc);
            }
            else
            {
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_FBPClaim.pdf");
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);

                //StringWriter s_tw = new StringWriter();
                //HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                //h_textw.AddStyleAttribute("font-size", "8pt");
                //h_textw.AddStyleAttribute("color", "Black");

                //string colHeads = "Summary_Report";
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();

                //h_textw.WriteBreak();
                //colHeads = "FBP Claim Details";
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //grd_FbpClaims_History.AllowPaging = false;

                //search();
                //grd_FbpClaims_History.Columns[10].Visible = false;
                //grd_FbpClaims_History.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                //grd_FbpClaims_History.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                //grd_FbpClaims_History.RenderControl(h_textw);
                //grd_FbpClaims_History.Columns[10].Visible = true;
                //grd_FbpClaims_History.AllowPaging = true;
                //h_textw.WriteBreak();

                //// Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                //iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 7f, 7f, 7f, 0f);
                ////doc.Add(TextAlign)


                //              //  Document doc = new Document();
                //iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                //doc.Open();

                //StringReader s_tr = new StringReader(s_tw.ToString());
                //iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                ////HtmlTextWriterStyle.TextAlign

                //html_worker.Parse(s_tr);
                //doc.Close();
                //Response.Write(doc);


                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_FBPClaim.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                hw.AddStyleAttribute("font-size", "8pt");
                hw.AddStyleAttribute("color", "Black");

                string colHeads = "FBP Claim Details";
                hw.WriteEncodedText(colHeads);
                hw.WriteBreak();
                grd_FbpClaims_History.AllowPaging = false;
                search();
                grd_FbpClaims_History.Columns[10].Visible = false;
                grd_FbpClaims_History.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                grd_FbpClaims_History.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                grd_FbpClaims_History.RenderControl(hw);
                grd_FbpClaims_History.Columns[10].Visible = true;
                grd_FbpClaims_History.AllowPaging = true;
                StringReader sr = new StringReader(sw.ToString());
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 0f);
                iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
                iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
            }
        }



    }
}
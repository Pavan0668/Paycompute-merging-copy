using iEmpPower.Old_App_Code.iEmpPowerBL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP.CollectionBO;
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
    public partial class FBP_Apply_ViewClaimsNew : System.Web.UI.Page
    {
        string TotalFBPamt = "0.0";
        protected void Page_PreRender(object sender, EventArgs e)
        {

            minmaxdate();

        }

        public void minmaxdate()
        {
            try
            {
                using (RangeValidator RV_BillDate = (RangeValidator)GridView1.FooterRow.FindControl("RV_txtBillDate"))
                {

                    string month = DateTime.Today.Month.ToString();

                    if (int.Parse(month.Trim()) >= 4)
                    {
                        RV_BillDate.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        RV_BillDate.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                    }

                    ////if (int.Parse(month.Trim()) >= 4 && int.Parse(month.Trim()) <= 12)
                    ////{

                    ////    RV_BillDate.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).AddYears(1).ToString("dd/MM/yyyy");

                    ////}
                    ////else if (int.Parse(month.Trim()) >= 1 && int.Parse(month.Trim()) <= 3)
                    ////{
                    ////    RV_BillDate.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).ToString("dd/MM/yyyy");

                    ////}

                    RV_BillDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                    RV_BillDate.ErrorMessage = "Bill date should be between " + RV_BillDate.MinimumValue + "- " + RV_BillDate.MaximumValue;
                }



                if (GridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        using (RangeValidator RV_BillDatee = (RangeValidator)GridView1.Rows[i].FindControl("RV_txtBillDatee"))
                        {
                            if (RV_BillDatee != null)
                            {
                                string month = DateTime.Today.Month.ToString();

                                if (int.Parse(month.Trim()) >= 4)
                                {
                                    RV_BillDatee.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");

                                }
                                else
                                {
                                    RV_BillDatee.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                                }

                                RV_BillDatee.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                                RV_BillDatee.ErrorMessage = "Bill date should be between " + RV_BillDatee.MinimumValue + "- " + RV_BillDatee.MaximumValue;


                            }
                        }
                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {


                    MV_Fbp.SetActiveView(View1);
                    btnNewFbpclaims.Visible = true;
                    LaodSavedFbpClaims();
                    GV_CorpClaim_NoData();
                    minmaxdate();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LaodSavedFbpClaims()
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                string ApproverId = User.Identity.Name;
                fbpboObj1 = fbpblObj.Load_Saved_fbpclaims(ApproverId.Trim());
                Session.Add("FbpGrdInfo", fbpboObj1);

                if (fbpboObj1 == null || fbpboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", lblmsg, Color.Red);
                    grd_SavedFbpClaims.Visible = false;
                    grd_SavedFbpClaims.DataSource = null;
                    grd_SavedFbpClaims.DataBind();
                    return;
                }
                else
                {
                    grd_SavedFbpClaims.Visible = true;
                    grd_SavedFbpClaims.DataSource = fbpboObj1;
                    grd_SavedFbpClaims.SelectedIndex = -1;
                    grd_SavedFbpClaims.DataBind();
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
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

        protected void btnNewFbpclaims_Click(object sender, EventArgs e)
        {
            try
            {
                ////GridView1.DataSource = null;
                ////GridView1.DataBind();
                GV_CorpClaim_NoData();
                MV_Fbp.SetActiveView(View2);
                btnNewFbpclaims.Visible = false;
                grd_SavedFbpClaims.Visible = false;
                tblreimbursement.Visible = true;
                LoadAllowances();
                ddlPlan.Enabled = true;
                ddlPlan.SelectedValue = "0";
                ddlPlan.Focus();
                MsgCls("", lblmsg, System.Drawing.Color.Transparent);


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadAllowances()
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();
                ddlPlan.Items.Clear();
                ddlPlan.DataSource = fbpblObj.LoadAllowances(User.Identity.Name);
                ddlPlan.DataTextField = "ALLOWANCETEXT";
                ddlPlan.DataValueField = "ALLOWANCEID";
                ddlPlan.DataBind();
                ddlPlan.Items.Insert(0, new ListItem("- SELECT -", "0"));
                ddlPlan.SelectedValue = "0";

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }


        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["fbpid"] = "";
                ViewState["lid"] = "";
                ViewState["tamt"] = "";
                DivClaims.Visible = true;
                Loadgrd_CalimsItems();
                GV_CorpClaim_NoData();
                btnback.Visible = true;

                lblitemsMsg.Text = "";
                lblmsg.Text = "";

                if (ddlPlan.SelectedValue.Trim() == "1215")
                {
                    Session["LTAREL"] = null;
                    Session["LTATRVL"] = null;
                    ViewState["fbpid"] = "";
                    //LoadLTA();
                    LTAClear();
                    DDL_Reltypes();
                    GridView1.Visible = grdLTATrvlDetails.Rows.Count > 0 ? true : false;
                    BtnExporttoPDF.Visible = grdLTATrvlDetails.Rows.Count > 0 ? true : false;
                    divLTA.Visible = true;

                    txtLTAblky1.Text = "2018";
                    txtLTAblky2.Text = (int.Parse(txtLTAblky1.Text) + 1).ToString();
                    txtLTAblky3.Text = (int.Parse(txtLTAblky1.Text) + 2).ToString();
                    txtLTAblky4.Text = (int.Parse(txtLTAblky1.Text) + 3).ToString();
                    txtLTACL1.Text = "2018";
                    txtLTACL2.Text = (int.Parse(txtLTACL1.Text) + 1).ToString();
                    txtLTACL3.Text = (int.Parse(txtLTACL1.Text) + 2).ToString();
                    txtLTACL4.Text = (int.Parse(txtLTACL1.Text) + 3).ToString();

                    LoadLTA();

                }
                else if (ddlPlan.SelectedValue.Trim() == "1200")
                {
                    GridView1.Visible = false;
                    pNote.Visible = false;
                    divLTA.Visible = false;
                    BtnExporttoPDF.Visible = false;
                }
                else
                {
                    //GridView1.Visible = true;
                    GridView1.Visible = ViewState["MobVald"].ToString() == "1" ? true : false;
                    divLTA.Visible = false;
                    BtnExporttoPDF.Visible = false;
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
                fbpboObj1 = fbpblObj.Load_FbpClaim_Details(ApproverId.Trim(), ddlPlan.SelectedValue.Trim());
                Session.Add("FbpGrdInfo", fbpboObj1);

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

        #region GV_CorpClaim No data
        private void GV_CorpClaim_NoData()
        {
            try
            {
                using (DataTable Dt = GetClaimBlankDt())
                {
                    Dt.Rows.Add(Dt.NewRow());
                    GridView1.DataSource = Dt;
                    GridView1.DataBind();
                    GridView1.Rows[0].Visible = false;
                    GridView1.Rows[0].Controls.Clear();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }
        #endregion

        #region Claim Empty DataTable

        private DataTable GetClaimBlankDt()
        {
            try
            {
                DataTable Dt = new DataTable();
                Dt.Columns.Add("ID", typeof(string));
                Dt.Columns.Add("FBPC_IC", typeof(int));
                Dt.Columns.Add("BILL_NO", typeof(string));
                Dt.Columns.Add("BILL_DATE", typeof(string));
                Dt.Columns.Add("RELATIONSHIP", typeof(string));
                Dt.Columns.Add("BILL_AMT", typeof(string));
                Dt.Columns.Add("RECEIPT_FILE", typeof(string));
                Dt.Columns.Add("RECEIPT_FID", typeof(string));
                Dt.Columns.Add("RECEIPT_FPATH", typeof(string));


                return Dt;
            }
            catch (Exception Ex)
            { throw Ex; return null; }
        }
        #endregion




        private DataTable GetClaimDetailsDt(int FBP_ID)
        {
            try
            {
                DataTable dtfbp = GetClaimBlankDt();

                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
                List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
                foreach (var vRow in objDataContext.usp_Fbp_GetBillDetails(FBP_ID))
                {
                    FbpClaimbo iexpboObj = new FbpClaimbo();
                    iexpboObj.ID = vRow.ID;
                    iexpboObj.FBPC_IC = vRow.FBPC_ID;
                    iexpboObj.RECEIPT_FID = vRow.RECEIPT_FID;
                    iexpboObj.RECEIPT_FILE = vRow.RECEIPT_FILE;
                    iexpboObj.RECEIPT_FPATH = vRow.RECEIPT_FPATH;
                    iexpboObj.RELATIONSHIP = vRow.RELATIONSHIP;
                    iexpboObj.BILL_AMT = vRow.BILL_AMT;
                    iexpboObj.BILL_DATE = vRow.BILL_DATE;
                    iexpboObj.BILL_NO = vRow.BILL_NO;

                    dtfbp.Rows.Add(iexpboObj.ID.ToString().Trim(), iexpboObj.FBPC_IC, iexpboObj.BILL_NO, DateTime.Parse(iexpboObj.BILL_DATE.ToString()).ToString("dd/MM/yyyy"), iexpboObj.RELATIONSHIP, iexpboObj.BILL_AMT,
                         iexpboObj.RECEIPT_FILE, iexpboObj.RECEIPT_FID,
                          iexpboObj.RECEIPT_FPATH);


                    FbpboList.Add(iexpboObj);
                }


                return dtfbp;
            }
            catch (Exception Ex)
            { throw Ex; return null; }
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string date1;
                string type = string.Empty;
                int? CountofLid = 0;

                decimal GTotal = 0;
                decimal tamt = 0;




                switch (e.CommandName)
                {
                    case "ADD":
                        type = "ADD";

                        string savedstatus = "";


                        FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();

                        objDataContext.usp_Fbp_GetSavedStatus(User.Identity.Name, ddlPlan.SelectedValue, ref savedstatus);

                        if (savedstatus == "NO" || ViewState["SavedEdit"].ToString() == "1")
                        {
                            ////
                            ////for (int i = 0; i < GridView1.Rows.Count; i++)
                            ////{
                                using (TextBox txtAmount = (TextBox)(GridView1.FooterRow.FindControl("txtAmount")))
                                {
                                    if (txtAmount.Text != "")
                                    {
                                        String total = txtAmount.Text;
                                        GTotal += Convert.ToDecimal(total);

                                        if (!String.IsNullOrEmpty(ViewState["tamt"].ToString()))//ViewState["tamt"] != null)
                                        {
                                            tamt = Convert.ToDecimal(ViewState["tamt"]) + Convert.ToDecimal(total);
                                            ViewState["tamt"] = tamt;
                                        }
                                        else
                                        {
                                            ViewState["tamt"] = GTotal;
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("Please fill mandate feilds!");
                                        break;
                                    }
                                }
                            ////}


                            ////String total = "";//txtAmount.Text;
                            ////using (TextBox txtAmount = (TextBox)(GridView1.FooterRow.FindControl("txtAmount")))
                            ////{
                            ////    if (txtAmount.Text != "")
                            ////    {
                            ////        total = txtAmount.Text;
                            ////        GTotal = Convert.ToDecimal(txtAmount.Text.Trim());
                            ////        ViewState["tamt"] = Convert.ToDecimal(ViewState["tamt"]) + Convert.ToDecimal(total);
                            ////    }
                            ////    else
                            ////    {
                            ////        throw new Exception("Please fill mandate feilds!");
                            ////        break;
                            ////    }
                            ////}


                            ////

                            using (TextBox TxtBillno = (TextBox)GridView1.FooterRow.FindControl("txtBillNo"))
                            using (TextBox TxtBilldate = (TextBox)GridView1.FooterRow.FindControl("txtBillDate"))
                            using (TextBox TxtRelationship = (TextBox)GridView1.FooterRow.FindControl("txtRelationship"))
                            using (TextBox TxtAmount = (TextBox)GridView1.FooterRow.FindControl("txtAmount"))
                            using (FileUpload fu = (FileUpload)GridView1.FooterRow.FindControl("fuAttachments"))
                            using (HiddenField HF_fid = (HiddenField)GridView1.FooterRow.FindControl("HF_Fid"))
                            using (RangeValidator RV_BillDate = (RangeValidator)GridView1.FooterRow.FindControl("RV_txtBillDate"))
                            using (DropDownList ddlMobno = (DropDownList)GridView1.FooterRow.FindControl("ddlEmpMobNo"))
                            {


                                if (GridView1.Rows.Count > 0)
                                {
                                    foreach (GridViewRow row in GridView1.Rows)
                                    {
                                        string str = row.Cells[1].Text.ToString();


                                    }


                                    //for (int i = 0; i < GridView1.Rows.Count; i++)
                                    //{
                                    //    //for (int s = 0; s <= GridView1.Rows.Count - 1; s++)
                                    //    //{
                                    // //  string str = GridView1.Rows[i].Cells[1].;
                                    //     //TextBox TxtBillnoe = (TextBox)GridView1.Rows[i].Cells[1].FindControl("txtBillNoe");
                                    //     //string str = TxtBillnoe.Text;
                                    //    // string str1 = DataGridView1.Rows[s]["ColLedger"].Value();
                                    //   // string str1 = GridView1.Rows[i][""] 
                                    //        //["ColLedger"].Value();
                                    //    if (str.Trim() == TxtBillno.Text.ToString().Trim())
                                    //    {
                                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('not allowed');", true);
                                    //    }
                                    //    //  }
                                    //}

                                }

                                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                                FbpClaimbo fbpboObj = new FbpClaimbo();
                                //FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
                                int? FBP_ID = 0;

                                if (ViewState["fbpid"] != null)
                                {
                                    if (!string.IsNullOrEmpty(ViewState["fbpid"].ToString().Trim()))
                                    {
                                        FBP_ID = int.Parse(ViewState["fbpid"].ToString().Trim());
                                    }
                                    else
                                    {
                                        FBP_ID = 0;
                                    }
                                }
                                objDataContext.usp_Fbp_CountofFbpLID(FBP_ID, ref CountofLid);
                                if (CountofLid != null)
                                {

                                    CountofLid = CountofLid + 1;

                                }

                                else
                                {
                                    CountofLid = 1;
                                }
                                bool? Status = true;


                                fbpboObj.CREATED_BY = User.Identity.Name;
                                fbpboObj.LGART = ddlPlan.SelectedValue.Trim();

                                fbpboObj.BEGDA = DateTime.Now;
                                fbpboObj.CREATED_ON = DateTime.Now;
                                fbpboObj.BETRG = TxtAmount.Text.Trim();
                                fbpboObj.STATUS = "Added";//"Saved";

                                date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                fbpboObj.FBPC_IC = FBP_ID;
                                ViewState["fbpid"] = FBP_ID;
                                fbpboObj.BILL_NO = TxtBillno.Text.Trim();
                                fbpboObj.BILL_DATE = DateTime.Parse(TxtBilldate.Text.Trim());
                                fbpboObj.RELATIONSHIP = TxtRelationship.Text == "" ? "" : TxtRelationship.Text.Trim();
                                fbpboObj.BILL_AMT = TxtAmount.Text.Trim();
                                fbpboObj.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
                                fbpboObj.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                                fbpboObj.RECEIPT_FPATH = fu.HasFile ? "~/FBPDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";
                                fbpboObj.ID = int.Parse(CountofLid.ToString().Trim());
                                fu.SaveAs(Server.MapPath("~/FBPDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fu.FileName));


                                string TotalFBPamt1 = "0.0";

                                tamt = Convert.ToDecimal(ViewState["tamt"]);
                                lblTotalAmount.Text = tamt.ToString();
                                //TotalFBPamt1=(objDataContext.usp_Fbp_GetTotalAmount(fbpboObj.CREATED_BY, fbpboObj.LGART, tamt)).ToString();

                                Fbp_Claimbl objbl = new Fbp_Claimbl();
                                FbpClaimscollectionbo objLst = objbl.GetTotalAmount(fbpboObj.CREATED_BY, fbpboObj.LGART, tamt);
                                foreach (FbpClaimbo objBo in objLst)
                                {
                                    TotalFBPamt1 = objBo.TotalFBPamt;
                                }

                                ViewState["TotalFBPamt1"] = TotalFBPamt1;

                                //TotalFBPamt1 = objbl.GetTotalAmount(fbpboObj.CREATED_BY, fbpboObj.LGART, tamt);

                                if (tamt > Convert.ToDecimal(TotalFBPamt1))
                                {
                                    if (Convert.ToDecimal(TotalFBPamt1) != 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('The Total Claim amount should be less than or equal to " + TotalFBPamt1 + "');", true);
                                        //MsgCls("Please Apply Billdate for the same month", lblmsg, Color.Red);   
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Your Claim eligibility as on this month is " + TotalFBPamt1 + ". So unable to raise the claim request');", true);
                                    }
                                    ViewState["tamt"] = Convert.ToDecimal(ViewState["tamt"]) - GTotal;
                                    break;
                                }
                                else
                                {

                                    fbpblObj.Create_FbpClaims_Header(fbpboObj, type, ref FBP_ID, ref Status);
                                    string dummy = "";

                                    if (ddlPlan.SelectedValue.ToString().Trim() == "1255")
                                        objDataContext.usp_FBP_get_user_mob_no_FBPID(FBP_ID, fbpboObj.ID, 1, ddlMobno.SelectedItem.Text.Trim(), ref dummy);

                                    if (Status == false)
                                    {
                                        ViewState["fbpid"] = FBP_ID.ToString().Trim();
                                        ddlPlan.Enabled = false;
                                        GridView1.DataSource = GetClaimDetailsDt(int.Parse(ViewState["fbpid"].ToString().Trim()));
                                        GridView1.DataBind();
                                        btnCancel.Visible = true;
                                        btnSave.Visible = true;
                                        btnSubmitClaims.Visible = true;
                                        MsgCls("Claim Item added successfully!", lblitemsMsg, Color.Green);


                                    }
                                }


                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please submit the previous saved claim of the " + ddlPlan.SelectedItem.Text + " Allowance/Reimbursement type before submitting the new claim');", true);
                            break;
                        }
                        break;

                    case "DELETE":
                        type = "DELETE";
                        using (TextBox TxtBillno = (TextBox)GridView1.FooterRow.FindControl("txtBillNo"))
                        using (TextBox TxtBilldate = (TextBox)GridView1.FooterRow.FindControl("txtBillDate"))
                        using (TextBox TxtRelationship = (TextBox)GridView1.FooterRow.FindControl("txtRelationship"))
                        using (TextBox TxtAmount = (TextBox)GridView1.FooterRow.FindControl("txtAmount"))
                        using (FileUpload fu = (FileUpload)GridView1.FooterRow.FindControl("fuAttachments"))
                        using (HiddenField HF_fid = (HiddenField)GridView1.FooterRow.FindControl("HF_Fid"))
                        {
                            int? FBP_ID = 0;
                            if (ViewState["fbpid"] != null)
                            {
                                if (!string.IsNullOrEmpty(ViewState["fbpid"].ToString().Trim()))
                                {
                                    FBP_ID = int.Parse(ViewState["fbpid"].ToString().Trim());
                                }

                            }

                            bool? Statusd = true;

                            Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                            FbpClaimbo fbpboObj = new FbpClaimbo();

                            fbpboObj.CREATED_BY = User.Identity.Name;
                            fbpboObj.LGART = ddlPlan.SelectedValue.Trim();

                            fbpboObj.BEGDA = DateTime.Now;
                            fbpboObj.CREATED_ON = DateTime.Now;
                            fbpboObj.BETRG = "";
                            fbpboObj.STATUS = "Saved";

                            date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                            fbpboObj.FBPC_IC = FBP_ID;
                            ViewState["fbpid"] = FBP_ID;
                            fbpboObj.BILL_NO = "";
                            fbpboObj.BILL_DATE = DateTime.Now;
                            fbpboObj.RELATIONSHIP = "";
                            fbpboObj.BILL_AMT = "";
                            fbpboObj.RECEIPT_FILE = "";
                            fbpboObj.RECEIPT_FID = "";
                            fbpboObj.RECEIPT_FPATH = "";
                            fbpboObj.ID = int.Parse(GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());

                            decimal amt = decimal.Parse(GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())]["BILL_AMT"].ToString());

                            fbpblObj.Create_FbpClaims_Header(fbpboObj, type, ref FBP_ID, ref Statusd);
                            if (Statusd == false)
                            {
                                ViewState["fbpid"] = FBP_ID.ToString().Trim();
                                ddlPlan.Enabled = false;

                                ViewState["tamt"] = decimal.Parse(ViewState["tamt"].ToString().Trim()) - amt;

                                GridView1.DataSource = GetClaimDetailsDt(int.Parse(ViewState["fbpid"].ToString().Trim()));
                                GridView1.DataBind();
                                MsgCls("Claim Item deleted successfully!", lblitemsMsg, Color.Green);
                            }

                        }
                        break;

                    case "DOWNLOAD":
                        //  string filename= grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FPATH"].ToString();
                        string filePath = e.CommandArgument.ToString();
                        //Response.ContentType = ContentType;
                        Response.ContentType = "application/octet-stream";
                        // Response.ContentType = "application/x-download";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                        Response.WriteFile(filePath);
                        Response.End();
                        break;


                    case "UPDATE":
                        type = "UPDATE";

                        int rowindex = Convert.ToInt32(e.CommandArgument);

                        using (TextBox TxtBillno = (TextBox)GridView1.Rows[rowindex].FindControl("txtBillNoe"))
                        using (TextBox TxtBilldate = (TextBox)GridView1.Rows[rowindex].FindControl("txtBillDatee"))
                        using (TextBox TxtRelationship = (TextBox)GridView1.Rows[rowindex].FindControl("txtRelationshipe"))
                        using (TextBox TxtAmount = (TextBox)GridView1.Rows[rowindex].FindControl("txtAmounte"))
                        using (FileUpload fu = (FileUpload)GridView1.Rows[rowindex].FindControl("fuAttachmentse"))
                        using (RangeValidator RV_BillDatee = (RangeValidator)GridView1.Rows[rowindex].FindControl("RV_txtBillDatee"))
                        using (DropDownList ddlMobno = (DropDownList)GridView1.Rows[rowindex].FindControl("ddlEmpMobNoe"))
                        {

                            string month = DateTime.Today.Month.ToString();

                            if (int.Parse(month.Trim()) >= 4)
                            {
                                RV_BillDatee.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");

                            }
                            else
                            {
                                RV_BillDatee.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                            }


                            RV_BillDatee.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                            ////if (int.Parse(month.Trim()) >= 4 && int.Parse(month.Trim()) <= 12)
                            ////{
                            ////    RV_BillDatee.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).AddYears(1).ToString("dd/MM/yyyy");

                            ////}
                            ////else if (int.Parse(month.Trim()) >= 1 && int.Parse(month.Trim()) <= 3)
                            ////{
                            ////    RV_BillDatee.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).ToString("dd/MM/yyyy");

                            ////}


                            RV_BillDatee.ErrorMessage = "Bill date should be between " + RV_BillDatee.MinimumValue + "- " + RV_BillDatee.MaximumValue;



                            int? FBP_ID = 0;
                            if (ViewState["fbpid"] != null)
                            {
                                if (!string.IsNullOrEmpty(ViewState["fbpid"].ToString().Trim()))
                                {
                                    FBP_ID = int.Parse(ViewState["fbpid"].ToString().Trim());
                                }

                            }

                            ////------------------------------------------------Newly Added START---------------------------------------------

                            ////
                            for (int i = 0; i < GridView1.Rows.Count; i++)
                            {
                                using (TextBox txtAmount = (TextBox)(GridView1.FooterRow.FindControl("txtAmount")))
                                {
                                    String total = txtAmount.Text;
                                    if (!string.IsNullOrEmpty(total))
                                    {
                                        GTotal += Convert.ToDecimal(total);


                                        if (ViewState["tamt"] != null)
                                        {
                                            tamt = Convert.ToDecimal(ViewState["tamt"]) + Convert.ToDecimal(total);
                                            ViewState["tamt"] = tamt;
                                        }
                                        else
                                        {
                                            ViewState["tamt"] = GTotal;
                                        }
                                    }

                                }
                            }

                            ////

                            String TotalFBPamt1 = "0.0";

                            tamt = Convert.ToDecimal(ViewState["tamt"]);
                            lblTotalAmount.Text = tamt.ToString();



                            ////------------------------------------------------Newly Added ENDS---------------------------------------------



                            bool? Statusd = true;

                            Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                            FbpClaimbo fbpboObj = new FbpClaimbo();

                            fbpboObj.CREATED_BY = User.Identity.Name;
                            fbpboObj.LGART = ddlPlan.SelectedValue.Trim();

                            fbpboObj.BEGDA = DateTime.Now;
                            fbpboObj.CREATED_ON = DateTime.Now;
                            fbpboObj.BETRG = TxtAmount.Text.Trim();
                            fbpboObj.STATUS = "Saved";

                            date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                            fbpboObj.FBPC_IC = FBP_ID;
                            ViewState["fbpid"] = FBP_ID;
                            fbpboObj.BILL_NO = TxtBillno.Text.Trim();
                            fbpboObj.BILL_DATE = DateTime.Parse(TxtBilldate.Text.Trim());
                            fbpboObj.RELATIONSHIP = TxtRelationship.Text.Trim();
                            fbpboObj.BILL_AMT = TxtAmount.Text.Trim();
                            fbpboObj.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
                            fbpboObj.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                            fbpboObj.RECEIPT_FPATH = fu.HasFile ? "~/FBPDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";
                            fbpboObj.ID = int.Parse(GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                            fu.SaveAs(Server.MapPath("~/FBPDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fu.FileName));



                            
                            //fbpboObj.RECEIPT_FPATH = fu.HasFile ? "~/FBPDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";
                            //fbpboObj.ID = int.Parse(CountofLid.ToString().Trim());
                            //fu.SaveAs(Server.MapPath("~/FBPDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fu.FileName));




                            if (Convert.ToDecimal(TxtAmount.Text.Trim()) > Convert.ToDecimal(ViewState["TotalFBPamt1"]))
                            {
                                if (Convert.ToDecimal(ViewState["TotalFBPamt1"]) != 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('The Total Claim amount should be less than or equal to " + ViewState["TotalFBPamt1"] + "');", true);
                                    //MsgCls("Please Apply Billdate for the same month", lblmsg, Color.Red);  
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Your Claim eligibility as on this month is " + ViewState["TotalFBPamt1"] + ". So unable to raise the claim request');", true);
                                }
                                // ViewState["tamt"] = Convert.ToDecimal(ViewState["tamt"]) - GTotal;
                                break;
                            }

                            else
                            {
                                fbpblObj.Create_FbpClaims_Header(fbpboObj, type, ref FBP_ID, ref Statusd);
                                string dummy = "";
                                FbpClaimsdalDataContext objDataContext1 = new FbpClaimsdalDataContext();
                                if (ddlPlan.SelectedValue.ToString().Trim() == "1255")
                                    objDataContext1.usp_FBP_get_user_mob_no_FBPID(FBP_ID, fbpboObj.ID, 2, ddlMobno.SelectedItem.Text.Trim(), ref dummy);
                                if (Statusd == false)
                                {
                                    ViewState["fbpid"] = FBP_ID.ToString().Trim();
                                    ddlPlan.Enabled = false;
                                    GridView1.EditIndex = -1;
                                    GridView1.DataSource = GetClaimDetailsDt(int.Parse(ViewState["fbpid"].ToString().Trim()));
                                    GridView1.DataBind();
                                    btnCancel.Visible = true;
                                    btnSave.Visible = true;
                                    btnSubmitClaims.Visible = true;
                                    MsgCls("Claim Item updated successfully!", lblitemsMsg, Color.Green);
                                    MsgCls("", lblmsg, Color.Green);
                                    if (GridView1.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < GridView1.Rows.Count; i++)
                                        {

                                            using (LinkButton LBtn_Editc = (LinkButton)GridView1.Rows[i].FindControl("LBtn_Edit"))
                                            using (LinkButton LBtn_Deletec = (LinkButton)GridView1.Rows[i].FindControl("LBtn_Delete"))
                                            {
                                                LBtn_Editc.Visible = true;
                                                LBtn_Deletec.Visible = true;

                                            }




                                        }

                                        using (LinkButton LbtnFbpClaimsViewADDc = (LinkButton)GridView1.FooterRow.FindControl("LbtnFbpClaimsViewADD"))
                                        {
                                            LbtnFbpClaimsViewADDc.Visible = true;
                                        }

                                        btnback.Visible = true;
                                    }
                                }
                            }

                        }
                        break;
                    case "DELETEFILE":
                        bool? statusfd = true;

                        int IDDeletefile = int.Parse(GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());


                        int FBP_IDd = 0;
                        if (ViewState["fbpid"] != null)
                        {
                            if (!string.IsNullOrEmpty(ViewState["fbpid"].ToString().Trim()))
                            {
                                FBP_IDd = int.Parse(ViewState["fbpid"].ToString().Trim());
                            }

                        }

                        FbpClaimsdalDataContext objDataContextd = new FbpClaimsdalDataContext();
                        objDataContextd.usp_Fbp_LIDFileDel(FBP_IDd, IDDeletefile, ref statusfd);
                        if (statusfd == false)
                        {
                            GridView1.DataSource = GetClaimDetailsDt(FBP_IDd);
                            GridView1.DataBind();
                            MsgCls("File deleted successfully!", lblitemsMsg, Color.Green);

                        }

                        break;

                    case "EDIT":

                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {

                switch (Ex.Message)
                {

                    case "-01":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please claim for the same month and year of Bill Date');", true);
                        MsgCls("Please claim for the same month and year of Bill Date", lblitemsMsg, Color.Red);
                        break;
                    case "-02":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Line item cannot be deleted since atleast one line item is required')", true);
                        MsgCls("Line item cannot be deleted since atleast one line item is required", lblitemsMsg, Color.Red);
                        break;
                    case "-03":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('This entitlement can be claimed only once in a financial year')", true);
                        MsgCls("This entitlement can be claimed only once in a financial year", lblitemsMsg, Color.Red);
                        break;
                    case "-04":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Not in monthly amount')", true);
                        MsgCls("Not in monthly amount", lblitemsMsg, Color.Red);
                        break;
                    case "-05":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The claim amount should be less than or equal to the total amount')", true);
                        MsgCls("The claim amount should be less than or equal to the total amount", lblitemsMsg, Color.Red);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        MsgCls(Ex.Message, lblitemsMsg, Color.Red);
                        break;
                }

            }

            //catch (Exception Ex)
            //{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {



            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GridView1.EditIndex = -1;
                GridView1.DataSource = GetClaimDetailsDt(int.Parse(ViewState["fbpid"].ToString().Trim()));
                GridView1.DataBind();
                btnCancel.Visible = true;
                btnSave.Visible = true;
                btnSubmitClaims.Visible = true;

                if (GridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {

                        using (LinkButton LBtn_Editc = (LinkButton)GridView1.Rows[i].FindControl("LBtn_Edit"))
                        using (LinkButton LBtn_Deletec = (LinkButton)GridView1.Rows[i].FindControl("LBtn_Delete"))
                        {
                            LBtn_Editc.Visible = true;
                            LBtn_Deletec.Visible = true;



                        }

                    }

                    using (LinkButton LbtnFbpClaimsViewADDc = (LinkButton)GridView1.FooterRow.FindControl("LbtnFbpClaimsViewADD"))
                    {
                        LbtnFbpClaimsViewADDc.Visible = true;

                    }
                    btnback.Visible = true;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                GridView1.EditIndex = e.NewEditIndex;

                int RowEditIndex = e.NewEditIndex;


                using (Label lblMobNo = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblMobNo"))
                {
                    ViewState["MobNo"] = lblMobNo.Text;
                }

                GridView1.DataSource = GetClaimDetailsDt(int.Parse(ViewState["fbpid"].ToString().Trim()));
                GridView1.DataBind();
                btnCancel.Visible = false;
                btnSave.Visible = false;
                btnSubmitClaims.Visible = false;

                // int rowindexe = Convert.ToInt32(e.CommandArgument);
                using (RangeValidator RV_BillDatee = (RangeValidator)GridView1.Rows[e.NewEditIndex].FindControl("RV_txtBillDatee"))
                {

                    string month = DateTime.Today.Month.ToString();

                    if (int.Parse(month.Trim()) >= 4)
                    {
                        RV_BillDatee.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        RV_BillDatee.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                    }



                    //if (int.Parse(month.Trim()) >= 4 && int.Parse(month.Trim()) <= 12)
                    //{
                    //    RV_BillDatee.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).AddYears(1).ToString("dd/MM/yyyy");

                    //}
                    //else if (int.Parse(month.Trim()) >= 1 && int.Parse(month.Trim()) <= 3)
                    //{
                    //    RV_BillDatee.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).ToString("dd/MM/yyyy");

                    //}

                    RV_BillDatee.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                    RV_BillDatee.ErrorMessage = "Bill date should be between " + RV_BillDatee.MinimumValue + "- " + RV_BillDatee.MaximumValue;


                    if (GridView1.Rows.Count > 0)
                    {
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            if (i != RowEditIndex)
                            {
                                using (LinkButton LBtn_Editc = (LinkButton)GridView1.Rows[i].FindControl("LBtn_Edit"))
                                using (LinkButton LBtn_Deletec = (LinkButton)GridView1.Rows[i].FindControl("LBtn_Delete"))
                                {
                                    LBtn_Editc.Visible = false;
                                    LBtn_Deletec.Visible = false;

                                }
                            }
                        }

                        using (LinkButton LbtnFbpClaimsViewADDc = (LinkButton)GridView1.FooterRow.FindControl("LbtnFbpClaimsViewADD"))
                        {
                            LbtnFbpClaimsViewADDc.Visible = false;
                        }
                        btnback.Visible = false;
                    }

                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnSubmitClaims_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();

                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.FBPC_IC = int.Parse(ViewState["fbpid"].ToString().Trim());
                fbpboObj.STATUS = "SUBMIT";
                fbpboObj.LGART = ddlPlan.SelectedValue.Trim();
                objDataContext.usp_Fbp_GetTotalFBPAmount(fbpboObj.FBPC_IC, fbpboObj.LGART, ref TotalFBPamt);
                fbpblObj.Submit_FbpClaims(fbpboObj, ref Status);

                ////////-----LTA
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();



                bool? status = false; int? LID = 0;
                fbpboObj.FID = int.Parse(ViewState["fbpid"].ToString());
                fbpboObj.ID = ViewState["FBPLTA_ID"] == null || ViewState["FBPLTA_ID"].ToString() == "" ? 0 : int.Parse(ViewState["FBPLTA_ID"].ToString());
                fbpblObj.AddLTAHead(fbpboObj, 2, ref status, ref LID);
                ////////-----LTA

                if (Status.Equals(false))
                {
                    SendMailMethod(int.Parse(ViewState["fbpid"].ToString().Trim()));
                    ViewState["fbpid"] = "";
                    ViewState["lid"] = "";
                    btnNewFbpclaims.Visible = true;
                    tblreimbursement.Visible = false;
                    ddlPlan.SelectedValue = "0";
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    DivClaims.Visible = false;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                    btnSubmitClaims.Visible = false;
                    LaodSavedFbpClaims();
                    grd_SavedFbpClaims.Visible = true;
                    btnback.Visible = false;
                    MV_Fbp.SetActiveView(View1);
                    btnNewFbpclaims.Focus();
                    ddlSeachSelect.SelectedValue = "0";
                    txtsearch.Text = string.Empty;
                    MsgCls("", lblmsg, System.Drawing.Color.Transparent);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Kay", "alert('FBP Claims submitted successfully');", true);
                    ddlSeachSelect.Focus();


                }
            }
            catch (Exception Ex)
            {
                switch (Ex.Message)
                {

                    case "-01":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please claim for the full amount that you have declare for this entitlement');", true);
                        //MsgCls("Please Apply Billdate for the same month", lblmsg, Color.Red);
                        break;
                    case "-03":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('The claim amount should be less than or equal to " + TotalFBPamt + "');", true);
                        //MsgCls("Please Apply Billdate for the same month", lblmsg, Color.Red);
                        break;

                    case "-04":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('This entitlement can be claimed only once in a financial year')", true);
                        //MsgCls("This entitlement can be claimed only once in a financial year", lblmsg, Color.Red);
                        break;

                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        break;
                }
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        private void SendMailMethod(int fbpidmail)
        {
            try
            {
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                GridView1.Columns[6].Visible = false;
                GridView1.FooterRow.Visible = false;
                GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                GridView1.GridLines = GridLines.Both;
                GridView1.RenderControl(hw1);
                GridView1.GridLines = GridLines.None;
                GridView1.Columns[6].Visible = true;

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

                strSubject = "FBP Claim Request " + fbpidmail + " has been Raised by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.";


                //RecipientsString = "monica.ks@itchamps.com";
                //strPernr_Mail = "latha.mg@itchamps.com";

                RecipientsString = "payrolladmin@subex.com"; //EMP_Email;
                strPernr_Mail = EMP_Email;// "payrolladmin@subex.com";

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>FBP Claim Request " + fbpidmail + " has been Raised by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.<br/><br/></b>";
                body += "<b>FBP Claim Details</b><br/><hr>";
                body += "<table><tr><td>FBP Claim ID </td><td> : </td><td>" + fbpidmail + "</td></tr>";
                body += "<tr><td>Allowance </td><td> : </td><td>  " + Entitlement + " - " + allowancetxt + "</td></tr>";
                body += "<tr><td>Date    </td><td> : </td><td>  " + bedga + "</td></tr>";
                body += "<tr><td>Total Amount </td><td> : </td><td>  " + decimal.Parse(amount).ToString("N2") + "</td></tr></table></br>";
                body += sw1.ToString() + "<br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('FBP Claims submitted successfully. Error in sending mail');", true);
                return;
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                bool? Status = true;

                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.FBPC_IC = int.Parse(ViewState["fbpid"].ToString().Trim());
                fbpboObj.STATUS = "DELETE";
                fbpblObj.Submit_FbpClaims(fbpboObj, ref Status);
                if (Status.Equals(false))
                {
                    ViewState["tamt"] = 0;
                    ViewState["fbpid"] = "";
                    ViewState["lid"] = "";
                    btnNewFbpclaims.Visible = true;
                    tblreimbursement.Visible = false;
                    ddlPlan.SelectedValue = "0";
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    DivClaims.Visible = false;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                    btnSubmitClaims.Visible = false;
                    LaodSavedFbpClaims();
                    grd_SavedFbpClaims.Visible = true;
                    btnback.Visible = false;
                    MV_Fbp.SetActiveView(View1);
                    btnNewFbpclaims.Focus();
                    ddlSeachSelect.SelectedValue = "0";
                    txtsearch.Text = string.Empty;
                    MsgCls("", lblmsg, System.Drawing.Color.Transparent);
                    ddlSeachSelect.Focus();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
                objDataContext.usp_Fbp_update_Added_to_Savedstatus(int.Parse(ViewState["fbpid"].ToString().Trim()), User.Identity.Name);


                ////////-----LTA
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();



                bool? status = false; int? LID = 0;
                fbpboObj.FID = int.Parse(ViewState["fbpid"].ToString());
                fbpboObj.ID = ViewState["FBPLTA_ID"] == null || ViewState["FBPLTA_ID"].ToString() == "" ? 0 : int.Parse(ViewState["FBPLTA_ID"].ToString());
                fbpblObj.AddLTAHead(fbpboObj, 2, ref status, ref LID);
                ////////-----LTA



                ////ViewState["fbpid"];
                ViewState["fbpid"] = "";
                ViewState["lid"] = "";
                btnNewFbpclaims.Visible = true;
                tblreimbursement.Visible = false;
                ddlPlan.SelectedValue = "0";
                GridView1.DataSource = null;
                GridView1.DataBind();

                DivClaims.Visible = false;
                btnCancel.Visible = false;
                btnSave.Visible = false;
                btnSubmitClaims.Visible = false;
                LaodSavedFbpClaims();
                grd_SavedFbpClaims.Visible = true;
                btnback.Visible = false;
                MV_Fbp.SetActiveView(View1);
                btnNewFbpclaims.Focus();
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                MsgCls("", lblmsg, System.Drawing.Color.Transparent);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('FBP Claims saved successfully');", true);
                ddlSeachSelect.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void grd_SavedFbpClaims_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "EDITFBP":
                        MsgCls("", lblmsg, System.Drawing.Color.Transparent);
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in grd_SavedFbpClaims.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }
                        Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                        FbpClaimbo fbpboObj = new FbpClaimbo();
                        List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                        tblreimbursement.Visible = true;
                        LoadAllowances();
                        ddlPlan.SelectedValue = grd_SavedFbpClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["LGART"].ToString().Trim();
                        ddlPlan.Enabled = false;
                        DivClaims.Visible = true;
                        btnNewFbpclaims.Visible = false;

                        grd_SavedFbpClaims.Visible = false;
                        MV_Fbp.SetActiveView(View2);

                        int fbpid = int.Parse(grd_SavedFbpClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString().Trim());
                        Loadgrd_CalimsItems();
                        GridView1.DataSource = GetClaimDetailsDt(fbpid);
                        GridView1.DataBind();
                        ViewState["fbpid"] = fbpid;

                        if (ddlPlan.SelectedValue.ToString().Trim() == "1215")
                        {
                            divLTA.Visible = true;
                            LoadLTA();
                        }
                        else
                        {
                            divLTA.Visible = false;
                            BtnExporttoPDF.Visible = false;
                        }

                        btnCancel.Visible = true;
                        btnSave.Visible = true;
                        btnSubmitClaims.Visible = true;
                        btnback.Visible = true;


                        using (RangeValidator RV_BillDate = (RangeValidator)GridView1.FooterRow.FindControl("RV_txtBillDate"))
                        {

                            string month = DateTime.Today.Month.ToString();

                            if (int.Parse(month.Trim()) >= 4)
                            {
                                RV_BillDate.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");

                            }
                            else
                            {
                                RV_BillDate.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                            }



                            //if (int.Parse(month.Trim()) >= 4 && int.Parse(month.Trim()) <= 12)
                            //{
                            //    RV_BillDate.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).AddYears(1).ToString("dd/MM/yyyy");

                            //}
                            //else if (int.Parse(month.Trim()) >= 1 && int.Parse(month.Trim()) <= 3)
                            //{
                            //    RV_BillDate.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).ToString("dd/MM/yyyy");

                            //}
                            RV_BillDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");

                            RV_BillDate.ErrorMessage = "Bill date should be between " + RV_BillDate.MinimumValue + "- " + RV_BillDate.MaximumValue;
                        }

                        ViewState["SavedEdit"] = "1";

                        break;

                    default:
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                btnNewFbpclaims.Visible = true;
                LaodSavedFbpClaims();
                DivClaims.Visible = false;
                tblreimbursement.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                ViewState["fbpid"] = "";
                ViewState["lid"] = "";

                ddlPlan.SelectedValue = "0";
                btnCancel.Visible = false;
                btnSave.Visible = false;
                btnSubmitClaims.Visible = false;
                LaodSavedFbpClaims();
                grd_SavedFbpClaims.Visible = true;
                btnback.Visible = false;
                MV_Fbp.SetActiveView(View1);
                btnNewFbpclaims.Focus();
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                MsgCls("", lblmsg, System.Drawing.Color.Transparent);
                ddlSeachSelect.Focus();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void grdSavedFBPclaims_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int pageindex = e.NewPageIndex;
                grd_SavedFbpClaims.PageIndex = e.NewPageIndex;
                LaodSavedFbpClaims();
                searchdetails();
                grd_SavedFbpClaims.SelectedIndex = -1;
                GridView1.DataSource = null;
                GridView1.DataBind();
                GV_CorpClaim_NoData();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void grdSavedFBPclaims_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                LaodSavedFbpClaims();
                searchdetails();
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
                }

                grd_SavedFbpClaims.DataSource = FbpboList;
                grd_SavedFbpClaims.DataBind();

                Session.Add("FbpGrdInfo", FbpboList);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                searchdetails();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void searchdetails()
        {

            try
            {
                //GridView1.DataSource = null;
                //GridView1.DataBind();
                GV_CorpClaim_NoData();
                MsgCls(string.Empty, lblmsg, System.Drawing.Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;


                if (SelectedType != "0" && textSearch == "")
                {
                    MsgCls("Please Enter the Text", lblmsg, System.Drawing.Color.Red);
                }

                else if (SelectedType == "0" && textSearch != "")
                {
                    MsgCls("Please Select the Type", lblmsg, System.Drawing.Color.Red);
                }
                else
                {


                    Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                    List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                    List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();

                    fbpboObj1 = fbpblObj.Load_Particular_FbpSavedClaimsDetails(User.Identity.Name, SelectedType, textSearch);

                    if (fbpboObj1 == null || fbpboObj1.Count == 0)
                    {
                        MsgCls("No Records found", lblmsg, System.Drawing.Color.Red);
                        grd_SavedFbpClaims.Visible = false;
                        grd_SavedFbpClaims.DataSource = null;
                        grd_SavedFbpClaims.DataBind();


                        return;
                    }
                    else
                    {
                        MsgCls("", lblmsg, System.Drawing.Color.Transparent);
                        grd_SavedFbpClaims.Visible = true;
                        grd_SavedFbpClaims.DataSource = fbpboObj1;
                        grd_SavedFbpClaims.SelectedIndex = -1;
                        grd_SavedFbpClaims.DataBind();

                        Session.Add("FbpGrdInfo", fbpboObj1);

                    }

                }

            }

            catch (Exception Ex)
            {

                MsgCls("Please enter valid data", lblmsg, System.Drawing.Color.Red);
            }
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                LaodSavedFbpClaims();
                MsgCls("", lblmsg, System.Drawing.Color.Transparent);
                //GridView1.DataSource = null;
                //GridView1.DataBind();
                GV_CorpClaim_NoData();
                ddlSeachSelect.Focus();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }



        protected void lbtn_View1Back_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("FBP.aspx?PC=C");//("FBP_Claims.aspx");


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ////decimal m = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (ddlPlan.SelectedValue.Trim() == "1255")
                    {
                        int lid = (e.Row.Cells[8].Text == "" || e.Row.Cells[8].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[8].Text);
                        FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
                        string data = "";
                        int id = (e.Row.Cells[9].Text == "" || e.Row.Cells[9].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[9].Text);
                        objDataContext.usp_FBP_get_user_mob_no_FBPID(id, lid, 3, "", ref data);
                        if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                        {



                            DropDownList ddList = e.Row.FindControl("ddlEmpMobNoe") as DropDownList;
                            Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                            FbpClaimbo fbpboObj = new FbpClaimbo();
                            ddList.Items.Clear();
                            ddList.DataSource = fbpblObj.LoadMobNo(User.Identity.Name, 1);
                            ddList.DataTextField = "ALLOWANCETEXT";
                            ddList.DataValueField = "ALLOWANCETEXT";
                            ddList.DataBind();



                            ////DataRowView dr = e.Row.DataItem as DataRowView;
                            //// ddList.SelectedItem.Text = ViewState["MobNo"].ToString().Trim();
                            ddList.SelectedValue = ViewState["MobNo"].ToString().Trim();
                            //string selectedCity = DataBinder.Eval(e.Row.DataItem, "ALLOWANCETEXT").ToString();
                            //ddList.Items.FindByValue(ViewState["MobNo"].ToString()).Selected = true;
                        }



                        else
                        {
                            Label lblMobNo = e.Row.FindControl("lblMobNo") as Label;
                            lblMobNo.Text = data;
                        }



                        GridView1.Columns[5].Visible = true;



                    }
                    else
                    {
                        GridView1.Columns[5].Visible = false;
                        ViewState["MobVald"] = "1";

                    }


                    //new


                    ////Label Amount = (Label)e.Row.FindControl("lblBillAmount");
                    ////    //Label lblUnitsInStock = (Label)e.Row.FindControl("lblUnitsInStock");  
                    ////m = m + int.Parse(Amount.Text);
                        //Table tb = new Table();  
                    //new
                   
                }
                //new
                ////if (e.Row.RowType == DataControlRowType.Footer)
                ////{
                ////    Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                ////    lblTotalAmount.Text = m.ToString();
                ////}
                //new


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }



        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            DropDownList ddlMob = (DropDownList)GridView1.FooterRow.FindControl("ddlEmpMobNo");
            Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
            FbpClaimbo fbpboObj = new FbpClaimbo();
            ddlMob.Items.Clear();
            ddlMob.DataSource = fbpblObj.LoadMobNo(User.Identity.Name, 1);
            ddlMob.DataTextField = "ALLOWANCETEXT";
            ddlMob.DataValueField = "ALLOWANCETEXT";
            ddlMob.DataBind();

            if (ddlPlan.SelectedValue.Trim() == "1255")
            {
                if (ddlMob.Items.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please add mobile numbers in FBP declaration');", true);
                    GridView1.Visible = false;
                    ViewState["MobVald"] = "0";
                }
                else
                    ViewState["MobVald"] = "1";
            }

        }

        protected void lbtnLTAAddRel_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable Dt = Session["LTAREL"] != null ? (DataTable)Session["LTAREL"] : LTAREL())
                {
                    string dpntd = chkLTARelDepend.Checked == true ? "X" : "n";
                    Dt.Rows.Add(ddlLTArelTypes.SelectedValue.Trim(),
                        txtLTARelName.Text.Trim(),
                        DateTime.Parse(txtLTARelDob.Text.Trim()),
                        ddlLTARelGender.SelectedValue.Trim(),
                        dpntd, ddlLTArelTypes.SelectedItem.Text.Trim(), null, 1);
                    Session["LTAREL"] = Dt;
                    grdLTARelps.DataSource = Session["LTAREL"];
                    grdLTARelps.DataBind();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        //protected void lbtnAddLTATrvl_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (DataTable Dt = Session["LTATRVL"] != null ? (DataTable)Session["LTATRVL"] : LTATRVL())
        //        {
        //            Dt.Rows.Add(txtLTAModeTrvl.Text.Trim(),
        //                txtLTAClsTrvl.Text.Trim(),
        //                DateTime.Parse(txtLTADtofDep.Text.Trim()),
        //                txtLTAPlaceDep.Text.Trim(),
        //                DateTime.Parse(txtLTADtArvl.Text.Trim()),
        //                txtLTAPlaceArvl.Text.Trim(),
        //                txtLTAKMTvld.Text.Trim(),
        //                txtLTAAmount.Text.Trim(),
        //                txtLTATicketNo.Text.Trim(), null, 1);
        //            Session["LTATRVL"] = Dt;
        //            grdLTATrvlDetails.DataSource = Session["LTATRVL"];
        //            grdLTATrvlDetails.DataBind();
        //        }
        //        foreach (GridViewRow row in grdLTATrvlDetails.Rows)
        //        {
        //            for (int i = 0; i < grdLTATrvlDetails.Rows.Count; i++)
        //            {
        //                if (i == 0)
        //                {
        //                    txtLTAHeadJStartdt.Text = grdLTATrvlDetails.Rows[i].Cells[3].Text;
        //                }
        //                if (i == grdLTATrvlDetails.Rows.Count - 1)
        //                {
        //                    txtLTAHeadJEnddt.Text = grdLTATrvlDetails.Rows[i].Cells[5].Text;
        //                }
        //            }
        //        }
        //        if (btnLTAUpdate.Visible != true)
        //            btnLTASubmit.Visible = true;

        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}



        protected static DataTable LTATRVL()
        {
            try
            {
                DataTable Dt = new DataTable();
                Dt.Columns.Add("MTRVL", typeof(string));
                Dt.Columns.Add("CTRVL", typeof(string));
                Dt.Columns.Add("JBGDT", typeof(DateTime));
                Dt.Columns.Add("STPNT", typeof(string));
                Dt.Columns.Add("JENDT", typeof(DateTime));
                Dt.Columns.Add("DESTN", typeof(string));
                Dt.Columns.Add("KM_TRVLD", typeof(decimal));
                Dt.Columns.Add("AMOUNTLTA", typeof(decimal));
                Dt.Columns.Add("TKTNO", typeof(string));
                Dt.Columns.Add("ID", typeof(int));
                Dt.Columns["ID"].AutoIncrement = true;
                Dt.Columns["ID"].AutoIncrementSeed = 1;
                Dt.Columns["ID"].AutoIncrementStep = 1;
                Dt.Columns.Add("N_O", typeof(int));
                return Dt;
            }
            catch (Exception Ex)
            { throw Ex; return null; }
        }

        protected static DataTable LTAREL()
        {
            try
            {
                DataTable Dt = new DataTable();
                Dt.Columns.Add("FAMTX", typeof(string));
                Dt.Columns.Add("FCNAM", typeof(string));
                Dt.Columns.Add("FGBDT", typeof(DateTime));
                Dt.Columns.Add("FASEX", typeof(string));
                Dt.Columns.Add("DEPDT", typeof(string));
                Dt.Columns.Add("FAMTX_text", typeof(string));
                Dt.Columns.Add("ID", typeof(int));
                Dt.Columns["ID"].AutoIncrement = true;
                Dt.Columns["ID"].AutoIncrementSeed = 1;
                Dt.Columns["ID"].AutoIncrementStep = 1;
                Dt.Columns.Add("N_O", typeof(int));
                return Dt;
            }
            catch (Exception Ex)
            { throw Ex; return null; }
        }

        protected void grdLTATrvlDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "EDITROW":
                        ViewState["ETRVL_ID"] = grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString().Trim();
                        txtLTAModeTrvl.Text = grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["MTRVL"].ToString().Trim();
                        txtLTAClsTrvl.Text = grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["CTRVL"].ToString().Trim();
                        txtLTADtofDep.Text = grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["JBGDT"].ToString().Trim();
                        txtLTADtArvl.Text = grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["JENDT"].ToString().Trim();
                        txtLTAPlaceDep.Text = grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["STPNT"].ToString().Trim();
                        txtLTAPlaceArvl.Text = grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["DESTN"].ToString().Trim();
                        txtLTAKMTvld.Text = grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["KM_TRVLD"].ToString().Trim();
                        txtLTAAmount.Text = grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["AMOUNTLTA"].ToString().Trim();
                        txtLTATicketNo.Text = grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["TKTNO"].ToString().Trim();
                        lbtnAddLTATrvl.Visible = false;
                        LBTNADDUPDATETRVL.Visible = true;
                        LBTNCNCLLTATRVL.Visible = true;
                        break;

                    case "DELETEROW":

                        bool? s = false;
                        Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                        FbpClaimbo fbpboObj = new FbpClaimbo();
                        fbpboObj.ID = int.Parse(grdLTATrvlDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString().Trim());
                        fbpblObj.AddLTA(fbpboObj, 3, ref s);

                        int index = Convert.ToInt32(e.CommandArgument.ToString());
                        DataTable dt = Session["LTATRVL"] as DataTable;
                        dt.Rows[index].Delete();
                        Session["LTATRVL"] = dt;

                        grdLTATrvlDetails.DataSource = (DataTable)Session["LTATRVL"];
                        grdLTATrvlDetails.DataBind();



                        break;
                }
            }
            catch (Exception Ex)
            { }
        }

        protected void btnLTACancel_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception Ex)
            { }
        }

        protected void btnLTASubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["LTAREL"] != null && Session["LTATRVL"] != null)
                {
                    Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                    FbpClaimbo fbpboObj = new FbpClaimbo();
                    bool? status = false; int? LID = 0;
                    fbpboObj.PERNR = User.Identity.Name.Trim();
                    fbpboObj.JBGDT = DateTime.Parse(txtLTAHeadJStartdt.Text.Trim());
                    fbpboObj.JENDT = DateTime.Parse(txtLTAHeadJEnddt.Text.Trim());
                    fbpboObj.STPNT = txtLTAHeadPD.Text.Trim();
                    fbpboObj.DESTN = txtLTAHEADPA.Text.Trim();
                    fbpboObj.MTRVL = txtLTAHeadModeofTrvl.Text.Trim();
                    fbpboObj.CTRVL = txtLTAHeadClasTrvl.Text.Trim();
                    fbpboObj.TKTNO = "";
                    fbpboObj.SLFTR = char.Parse(rbtnLTAPartofJ.SelectedValue.Trim());
                    fbpboObj.CBPY1 = txtLTAblky1.Text.Trim();
                    fbpboObj.CBPY2 = txtLTAblky2.Text.Trim();
                    fbpboObj.CBPY3 = txtLTAblky3.Text.Trim();
                    fbpboObj.CBPY4 = txtLTAblky4.Text.Trim();
                    fbpboObj.CLYear = txtLTACLkyear.Text.Trim();
                    fbpboObj.CLY1 = txtLTACL1.Text.Trim();
                    fbpboObj.CLY2 = txtLTACL2.Text.Trim();
                    fbpboObj.CLY3 = txtLTACL3.Text.Trim();
                    fbpboObj.CLY4 = txtLTACL4.Text.Trim();
                    fbpblObj.AddLTAHead(fbpboObj, 1, ref status, ref LID);
                    ViewState["FBPLTA_ID"] = LID;

                    using (DataTable Dt = (DataTable)Session["LTATRVL"])
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            fbpboObj.LINEID = i + 1;
                            fbpboObj.ID = 0;
                            fbpboObj.FID = LID;
                            fbpboObj.PERNR = User.Identity.Name.Trim();
                            fbpboObj.MTRVL = Dt.Rows[i]["MTRVL"].ToString();
                            fbpboObj.CTRVL = Dt.Rows[i]["CTRVL"].ToString();
                            fbpboObj.JBGDT = DateTime.Parse(Dt.Rows[i]["JBGDT"].ToString());
                            fbpboObj.STPNT = Dt.Rows[i]["STPNT"].ToString();
                            fbpboObj.JENDT = DateTime.Parse(Dt.Rows[i]["JENDT"].ToString());
                            fbpboObj.DESTN = Dt.Rows[i]["DESTN"].ToString();
                            fbpboObj.KM_TRVLD = decimal.Parse(Dt.Rows[i]["KM_TRVLD"].ToString());
                            fbpboObj.AMOUNTLTA = decimal.Parse(Dt.Rows[i]["AMOUNTLTA"].ToString());
                            fbpboObj.TKTNO = Dt.Rows[i]["TKTNO"].ToString();
                            fbpblObj.AddLTA(fbpboObj, 1, ref status);
                        }
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Submitted successfully');", true);
                    }

                    using (DataTable Dt1 = (DataTable)Session["LTAREL"])
                    {
                        for (int i = 0; i < Dt1.Rows.Count; i++)
                        {
                            fbpboObj.LINEID = i + 1;
                            fbpboObj.FID = LID;
                            fbpboObj.ID = 0;
                            fbpboObj.PERNR = User.Identity.Name.Trim();
                            fbpboObj.FAMTX = Dt1.Rows[i]["FAMTX"].ToString();
                            fbpboObj.FCNAM = Dt1.Rows[i]["FCNAM"].ToString();
                            fbpboObj.FGBDT = DateTime.Parse(Dt1.Rows[i]["FGBDT"].ToString());
                            fbpboObj.FASEX = char.Parse(Dt1.Rows[i]["FASEX"].ToString());
                            fbpboObj.DEPDT = char.Parse(Dt1.Rows[i]["DEPDT"].ToString());
                            fbpblObj.AddLTAReltps(fbpboObj, 1, ref status);
                        }

                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Submitted successfully');", true);
                    btnLTAUpdate.Visible = true;
                    btnLTASubmit.Visible = false;
                    BtnExporttoPDF.Visible = true;
                    GridView1.Visible = true;
                    LoadLTA();

                }
            }
            catch (Exception Ex)
            { }
        }

        public void DDL_Reltypes()
        {
            Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
            FbpClaimbo fbpboObj = new FbpClaimbo();
            FbpClaimscollectionbo FBPBo = new FbpClaimscollectionbo();
            FBPBo = fbpblObj.Get_FamilyMember_Types();
            ddlLTArelTypes.DataSource = null;
            ddlLTArelTypes.DataBind();

            ddlLTArelTypes.DataSource = FBPBo;
            ddlLTArelTypes.DataTextField = "STEXT";
            ddlLTArelTypes.DataValueField = "SUBTY";
            ddlLTArelTypes.DataBind();
            ddlLTArelTypes.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - SELECT - ", "0"));
            //lbl_errmssg.Text = "";
        }

        protected void LoadLTA()
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                ViewState["FBP_Status"] = "";
                string a = "";
                fbpboObj1 = fbpblObj.Load_LTAHead(User.Identity.Name, ViewState["fbpid"].ToString() == "" ? int.Parse(ViewState["FBPLTA_ID"].ToString()) : 0, ViewState["fbpid"].ToString() == "" ? 0 : int.Parse(ViewState["fbpid"].ToString()), 1, ref a);
                if (fbpboObj1.Count > 0)
                {
                    DDL_Reltypes();
                    ViewState["FBP_Status"] = a;
                    ViewState["FBPLTA_ID"] = fbpboObj1[0].ID.ToString();
                    txtLTAHeadJStartdt.Text = DateTime.Parse(fbpboObj1[0].JBGDT.ToString()).ToString("dd/MM/yyyy");
                    txtLTAHeadJEnddt.Text = DateTime.Parse(fbpboObj1[0].JENDT.ToString()).ToString("dd/MM/yyyy");
                    txtLTAHeadClasTrvl.Text = fbpboObj1[0].CTRVL.ToString();
                    txtLTAHeadModeofTrvl.Text = fbpboObj1[0].MTRVL.ToString();
                    txtLTAHeadPD.Text = fbpboObj1[0].DESTN.ToString();
                    txtLTAHEADPA.Text = fbpboObj1[0].STPNT.ToString();
                    rbtnLTAPartofJ.SelectedValue = fbpboObj1[0].SLFTR.ToString();
                    txtLTAblky1.Text = fbpboObj1[0].CBPY1.ToString();
                    txtLTAblky2.Text = fbpboObj1[0].CBPY2.ToString();
                    txtLTAblky3.Text = fbpboObj1[0].CBPY3.ToString();
                    txtLTAblky4.Text = fbpboObj1[0].CBPY4.ToString();
                    txtLTACLkyear.Text = fbpboObj1[0].CLYear.ToString();
                    txtLTACL1.Text = fbpboObj1[0].CLY1.ToString();
                    txtLTACL2.Text = fbpboObj1[0].CLY2.ToString();
                    txtLTACL3.Text = fbpboObj1[0].CLY3.ToString();
                    txtLTACL4.Text = fbpboObj1[0].CLY4.ToString();







                    fbpboObj1 = fbpblObj.Load_LTArel(User.Identity.Name, int.Parse(ViewState["FBPLTA_ID"].ToString()), 0, 1);
                    Session["LTAREL"] = ConvertToDataRow(fbpboObj1, 1);
                    grdLTARelps.DataSource = fbpboObj1;
                    grdLTARelps.DataBind();




                    fbpboObj1 = fbpblObj.Load_LTATRVL(User.Identity.Name, int.Parse(ViewState["FBPLTA_ID"].ToString()), 0, 1);
                    Session["LTATRVL"] = ConvertToDataRow(fbpboObj1, 2);
                    grdLTATrvlDetails.DataSource = fbpboObj1;
                    grdLTATrvlDetails.DataBind();

                    tblLTAReL.Visible = (fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "") ? true : false;
                    tblLTATRvl.Visible = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    //btnLTAPDF.Visible = fbpboObj1.Count <= 0 ? false : true;
                    //btnLTAXl.Visible = fbpboObj1.Count <= 0 ? false : true;
                    BtnExporttoPDF.Visible = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTAHeadClasTrvl.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTAHeadModeofTrvl.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTAHeadPD.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTAHEADPA.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    rbtnLTAPartofJ.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;



                    txtLTAblky1.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTAblky2.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTAblky3.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTAblky4.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTACLkyear.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTACL1.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTACL2.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTACL3.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    txtLTACL4.Enabled = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    grdLTATrvlDetails.Columns[10].Visible = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;
                    grdLTARelps.Columns[7].Visible = fbpboObj1.Count <= 0 || ViewState["FBP_Status"].ToString() == "Saved" || ViewState["FBP_Status"].ToString() == "" ? true : false;


                }
                else
                {
                    LTAClear();
                }



                if (ViewState["FBP_Status"].ToString() == "Saved")
                {
                    btnLTAUpdate.Visible = true;
                    btnLTASubmit.Visible = false;
                    GridView1.Visible = true;
                }
                else
                {
                    btnLTAUpdate.Visible = false;
                    //btnLTASubmit.Visible = true;
                    GridView1.Visible = false;
                }
            }
            catch (Exception Ex)
            { }
        }

        protected void btnLTAXl_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception Ex)
            { }
        }

        protected void btnLTAPDF_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Response.Clear();
            //    Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
            //    Response.Charset = "";
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.ContentType = "application/vnd.xls";
            //    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //    divLTA.RenderControl(htmlWrite);
            //    Response.Write(stringWrite.ToString());
            //    Response.End();
            //}
            //catch (Exception Ex)
            //{ }
        }
        //--
        protected void grdLTARelps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "EDITROW":
                        ViewState["EREL_ID"] = grdLTARelps.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString().Trim();
                        ddlLTArelTypes.SelectedValue = grdLTARelps.DataKeys[int.Parse(e.CommandArgument.ToString())]["FAMTX"].ToString().Trim();
                        txtLTARelName.Text = grdLTARelps.DataKeys[int.Parse(e.CommandArgument.ToString())]["FCNAM"].ToString().Trim();
                        txtLTARelDob.Text = grdLTARelps.DataKeys[int.Parse(e.CommandArgument.ToString())]["FGBDT"].ToString().Trim();
                        ddlLTARelGender.SelectedValue = grdLTARelps.DataKeys[int.Parse(e.CommandArgument.ToString())]["FASEX"].ToString().Trim();
                        chkLTARelDepend.Checked = grdLTARelps.DataKeys[int.Parse(e.CommandArgument.ToString())]["DEPDT"].ToString().Trim() == "X" ? true : false;
                        lbtnLTAAddRel.Visible = false;
                        LBTNLTAUPDATEREL.Visible = true;
                        lbtnLTACNCLRel.Visible = true;
                        break;

                    case "DELETEROW":

                        bool? s = false;
                        Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                        FbpClaimbo fbpboObj = new FbpClaimbo();
                        fbpboObj.ID = int.Parse(grdLTARelps.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString().Trim());
                        fbpblObj.AddLTAReltps(fbpboObj, 3, ref s);

                        int index = Convert.ToInt32(e.CommandArgument.ToString());
                        DataTable dt = Session["LTAREL"] as DataTable;
                        dt.Rows[index].Delete();
                        Session["LTAREL"] = dt;


                        grdLTARelps.DataSource = (DataTable)Session["LTAREL"];
                        grdLTARelps.DataBind();


                        break;
                }
            }
            catch (Exception Ex)
            { }
        }



        protected void lbtnLTACNCLRel_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["EREL_ID"] = "";
                ddlLTArelTypes.SelectedIndex = -1;
                txtLTARelName.Text = "";
                txtLTARelDob.Text = "";
                ddlLTARelGender.SelectedIndex = -1;
                chkLTARelDepend.Checked = false;
                lbtnLTAAddRel.Visible = true;
                LBTNLTAUPDATEREL.Visible = false;
                lbtnLTACNCLRel.Visible = false;
            }
            catch (Exception Ex)
            { }
        }

        protected void LBTNLTAUPDATEREL_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable uptbl = (DataTable)Session["LTAREL"])
                {
                    string dpntd = "";
                    DataRow[] varrow = (from lta in uptbl.AsEnumerable().Cast<DataRow>() where lta.Field<int>("ID") == Convert.ToInt32(ViewState["EREL_ID"]) select lta).ToArray();
                    foreach (DataRow lta in varrow)
                    {
                        lta["FAMTX"] = ddlLTArelTypes.SelectedValue.ToString();
                        lta["FCNAM"] = txtLTARelName.Text.Trim();
                        lta["FGBDT"] = txtLTARelDob.Text.Trim();
                        lta["FASEX"] = ddlLTARelGender.SelectedValue.ToString();

                        dpntd = chkLTARelDepend.Checked == true ? "X" : "n";
                        lta["DEPDT"] = dpntd;
                        lta["FAMTX_text"] = ddlLTArelTypes.SelectedItem.Text;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully');", true);
                    Session["LTAREL"] = uptbl;
                    grdLTARelps.DataSource = (DataTable)Session["LTAREL"];
                    grdLTARelps.DataBind();
                    ViewState["EREL_ID"] = "";
                    ddlLTArelTypes.SelectedIndex = -1;
                    txtLTARelName.Text = "";
                    txtLTARelDob.Text = "";
                    ddlLTARelGender.SelectedIndex = -1;
                    chkLTARelDepend.Checked = false;
                    lbtnLTAAddRel.Visible = true;
                    LBTNLTAUPDATEREL.Visible = false;
                    lbtnLTACNCLRel.Visible = false;

                }
            }
            catch (Exception Ex)
            { }
        }

        protected void LBTNCNCLLTATRVL_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["ETRVL_ID"] = "";
                txtLTAModeTrvl.Text = "";
                txtLTAClsTrvl.Text = "";
                txtLTADtofDep.Text = "";
                txtLTADtArvl.Text = "";
                txtLTAPlaceDep.Text = "";
                txtLTAPlaceArvl.Text = "";
                txtLTAKMTvld.Text = "";
                txtLTAAmount.Text = "";
                txtLTATicketNo.Text = "";
                lbtnAddLTATrvl.Visible = true;
                LBTNADDUPDATETRVL.Visible = false;
                LBTNCNCLLTATRVL.Visible = false;
            }
            catch (Exception Ex)
            { }
        }

        protected void LBTNADDUPDATETRVL_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable uptbl = (DataTable)Session["LTATRVL"])
                {
                    DataRow[] varrow = (from lta in uptbl.AsEnumerable().Cast<DataRow>() where lta.Field<int>("ID") == Convert.ToInt32(ViewState["ETRVL_ID"]) select lta).ToArray();
                    foreach (DataRow lta in varrow)
                    {

                        lta["MTRVL"] = txtLTAModeTrvl.Text.Trim();
                        lta["CTRVL"] = txtLTAClsTrvl.Text.Trim();
                        lta["JBGDT"] = txtLTADtofDep.Text.Trim();
                        lta["STPNT"] = txtLTAPlaceDep.Text.Trim();
                        lta["JENDT"] = txtLTADtArvl.Text.Trim();
                        lta["DESTN"] = txtLTAPlaceArvl.Text.Trim();
                        lta["KM_TRVLD"] = txtLTAKMTvld.Text.Trim();
                        lta["AMOUNTLTA"] = txtLTAAmount.Text.Trim();
                        lta["TKTNO"] = txtLTATicketNo.Text.Trim();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully');", true);
                    Session["LTATRVL"] = uptbl;
                    grdLTATrvlDetails.DataSource = (DataTable)Session["LTATRVL"];
                    grdLTATrvlDetails.DataBind();
                    ViewState["ETRVL_ID"] = "";
                    txtLTAModeTrvl.Text = "";
                    txtLTAClsTrvl.Text = "";
                    txtLTADtofDep.Text = "";
                    txtLTADtArvl.Text = "";
                    txtLTAPlaceDep.Text = "";
                    txtLTAPlaceArvl.Text = "";
                    txtLTAKMTvld.Text = "";
                    txtLTAAmount.Text = "";
                    txtLTATicketNo.Text = "";
                    lbtnAddLTATrvl.Visible = true;
                    LBTNADDUPDATETRVL.Visible = false;
                    LBTNCNCLLTATRVL.Visible = false;

                }
                foreach (GridViewRow row in grdLTATrvlDetails.Rows)
                {
                    for (int i = 0; i < grdLTATrvlDetails.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            txtLTAHeadJStartdt.Text = grdLTATrvlDetails.Rows[i].Cells[3].Text;
                        }
                        if (i == grdLTATrvlDetails.Rows.Count - 1)
                        {
                            txtLTAHeadJEnddt.Text = grdLTATrvlDetails.Rows[i].Cells[5].Text;
                        }
                    }
                }

            }
            catch (Exception Ex)
            { }
        }

        protected void btnLTAUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["LTAREL"] != null && Session["LTATRVL"] != null)
                {
                    Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                    FbpClaimbo fbpboObj = new FbpClaimbo();
                    bool? status = false; int? LID = int.Parse(ViewState["FBPLTA_ID"].ToString());
                    fbpboObj.FID = 0;
                    fbpboObj.ID = int.Parse(ViewState["FBPLTA_ID"].ToString());
                    fbpboObj.PERNR = User.Identity.Name.Trim();
                    fbpboObj.JBGDT = DateTime.Parse(txtLTAHeadJStartdt.Text.Trim());
                    fbpboObj.JENDT = DateTime.Parse(txtLTAHeadJEnddt.Text.Trim());
                    fbpboObj.STPNT = txtLTAHeadPD.Text.Trim();
                    fbpboObj.DESTN = txtLTAHEADPA.Text.Trim();
                    fbpboObj.MTRVL = txtLTAHeadModeofTrvl.Text.Trim();
                    fbpboObj.CTRVL = txtLTAHeadClasTrvl.Text.Trim();
                    fbpboObj.TKTNO = "";
                    fbpboObj.SLFTR = char.Parse(rbtnLTAPartofJ.SelectedValue.Trim());
                    fbpboObj.CBPY1 = txtLTAblky1.Text.Trim();
                    fbpboObj.CBPY2 = txtLTAblky2.Text.Trim();
                    fbpboObj.CBPY3 = txtLTAblky3.Text.Trim();
                    fbpboObj.CBPY4 = txtLTAblky4.Text.Trim();
                    fbpboObj.CLYear = txtLTACLkyear.Text.Trim();
                    fbpboObj.CLY1 = txtLTACL1.Text.Trim();
                    fbpboObj.CLY2 = txtLTACL2.Text.Trim();
                    fbpboObj.CLY3 = txtLTACL3.Text.Trim();
                    fbpboObj.CLY4 = txtLTACL4.Text.Trim();

                    fbpblObj.AddLTAHead(fbpboObj, 3, ref status, ref LID);


                    using (DataTable Dt = (DataTable)Session["LTATRVL"])
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            fbpboObj.LINEID = i + 1;
                            fbpboObj.FID = LID;
                            fbpboObj.ID = int.Parse(Dt.Rows[i]["ID"].ToString());
                            fbpboObj.PERNR = User.Identity.Name.Trim();
                            fbpboObj.MTRVL = Dt.Rows[i]["MTRVL"].ToString();
                            fbpboObj.CTRVL = Dt.Rows[i]["CTRVL"].ToString();
                            fbpboObj.JBGDT = DateTime.Parse(Dt.Rows[i]["JBGDT"].ToString());
                            fbpboObj.STPNT = Dt.Rows[i]["STPNT"].ToString();
                            fbpboObj.JENDT = DateTime.Parse(Dt.Rows[i]["JENDT"].ToString());
                            fbpboObj.DESTN = Dt.Rows[i]["DESTN"].ToString();
                            fbpboObj.KM_TRVLD = decimal.Parse(Dt.Rows[i]["KM_TRVLD"].ToString());
                            fbpboObj.AMOUNTLTA = decimal.Parse(Dt.Rows[i]["AMOUNTLTA"].ToString());
                            fbpboObj.TKTNO = Dt.Rows[i]["TKTNO"].ToString();
                            if (Dt.Rows[i]["N_O"].ToString() == "0")
                                fbpblObj.AddLTA(fbpboObj, 2, ref status);
                            else
                                fbpblObj.AddLTA(fbpboObj, 1, ref status);
                        }
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Submitted successfully');", true);
                    }

                    using (DataTable Dt1 = (DataTable)Session["LTAREL"])
                    {
                        for (int i = 0; i < Dt1.Rows.Count; i++)
                        {
                            fbpboObj.LINEID = i + 1;
                            fbpboObj.FID = LID;
                            fbpboObj.ID = int.Parse(Dt1.Rows[i]["ID"].ToString());
                            fbpboObj.PERNR = User.Identity.Name.Trim();
                            fbpboObj.FAMTX = Dt1.Rows[i]["FAMTX"].ToString();
                            fbpboObj.FCNAM = Dt1.Rows[i]["FCNAM"].ToString();
                            fbpboObj.FGBDT = DateTime.Parse(Dt1.Rows[i]["FGBDT"].ToString());
                            fbpboObj.FASEX = char.Parse(Dt1.Rows[i]["FASEX"].ToString());
                            fbpboObj.DEPDT = char.Parse(Dt1.Rows[i]["DEPDT"].ToString());
                            if (Dt1.Rows[i]["N_O"].ToString() == "0")
                                fbpblObj.AddLTAReltps(fbpboObj, 2, ref status);
                            else
                                fbpblObj.AddLTAReltps(fbpboObj, 1, ref status);
                        }

                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully');", true);
                    LoadLTA();
                    GridView1.Visible = true;
                }
            }
            catch (Exception Ex)
            { }
        }

        public static DataTable ConvertToDataRow(List<FbpClaimbo> objColBo, int a)
        {
            DataTable dt = new DataTable();
            if (a == 1)
            {
                dt = LTAREL();
                foreach (FbpClaimbo objBo in objColBo)
                {
                    DataRow dRow = dt.NewRow();
                    dRow["FAMTX"] = objBo.FAMTX;
                    dRow["FCNAM"] = objBo.FCNAM;
                    dRow["FGBDT"] = objBo.FGBDT;
                    dRow["FASEX"] = objBo.FASEX;
                    dRow["DEPDT"] = objBo.DEPDT;
                    dRow["FAMTX_text"] = objBo.FAMTX_text;
                    dRow["ID"] = objBo.ID;
                    dRow["N_O"] = 0;
                    dt.Rows.Add(dRow);
                }
            }
            else if (a == 2)
            {
                dt = LTATRVL();
                foreach (FbpClaimbo objBo in objColBo)
                {
                    DataRow dRow = dt.NewRow();

                    dRow["MTRVL"] = objBo.MTRVL;
                    dRow["CTRVL"] = objBo.CTRVL;
                    dRow["JBGDT"] = objBo.JBGDT;
                    dRow["STPNT"] = objBo.STPNT;
                    dRow["JENDT"] = objBo.JENDT;
                    dRow["DESTN"] = objBo.DESTN;
                    dRow["KM_TRVLD"] = objBo.KM_TRVLD;
                    dRow["AMOUNTLTA"] = objBo.AMOUNTLTA;
                    dRow["TKTNO"] = objBo.TKTNO;
                    dRow["ID"] = objBo.ID;
                    dRow["N_O"] = 0;
                    dt.Rows.Add(dRow);
                }
            }
            return dt;
        }

        protected void txtLTAblky1_TextChanged(object sender, EventArgs e)
        {
            txtLTAblky2.Text = (int.Parse(txtLTAblky1.Text) + 1).ToString();
            txtLTAblky3.Text = (int.Parse(txtLTAblky1.Text) + 2).ToString();
            txtLTAblky4.Text = (int.Parse(txtLTAblky1.Text) + 3).ToString();
        }



        protected void txtLTACL1_TextChanged(object sender, EventArgs e)
        {
            txtLTACL2.Text = (int.Parse(txtLTACL1.Text) + 1).ToString();
            txtLTACL3.Text = (int.Parse(txtLTACL1.Text) + 2).ToString();
            txtLTACL4.Text = (int.Parse(txtLTACL1.Text) + 3).ToString();
        }

        protected void LTAClear()
        {
            grdLTARelps.DataSource = grdLTATrvlDetails.DataSource = null;
            grdLTATrvlDetails.DataBind();
            grdLTARelps.DataBind();

            txtLTAHeadJStartdt.Text = "";
            txtLTAHeadJEnddt.Text = "";
            txtLTAHeadClasTrvl.Text = "";
            txtLTAHeadModeofTrvl.Text = "";
            txtLTAHeadPD.Text = "";
            txtLTAHEADPA.Text = "";
            rbtnLTAPartofJ.SelectedValue = "Y";
            txtLTAblky1.Text = "";
            txtLTAblky2.Text = "";
            txtLTAblky3.Text = "";
            txtLTAblky4.Text = "";
            txtLTACLkyear.Text = "";
            txtLTACL1.Text = "";
            txtLTACL2.Text = "";
            txtLTACL3.Text = "";
            txtLTACL4.Text = "";
            btnLTAUpdate.Visible = false;
        }

        protected void lbtnAddLTATrvl_Click(object sender, EventArgs e)
        {


             try
            {
                using (DataTable Dt = Session["LTATRVL"] != null ? (DataTable)Session["LTATRVL"] : LTATRVL())
                {
                    Dt.Rows.Add(txtLTAModeTrvl.Text.Trim(),
                        txtLTAClsTrvl.Text.Trim(),
                        DateTime.Parse(txtLTADtofDep.Text.Trim()),
                        txtLTAPlaceDep.Text.Trim(),
                        DateTime.Parse(txtLTADtArvl.Text.Trim()),
                        txtLTAPlaceArvl.Text.Trim(),
                        txtLTAKMTvld.Text.Trim(),
                        txtLTAAmount.Text.Trim(),
                        txtLTATicketNo.Text.Trim(), null, 1);
                    Session["LTATRVL"] = Dt;
                    grdLTATrvlDetails.DataSource = Session["LTATRVL"];
                    grdLTATrvlDetails.DataBind();
                }
                foreach (GridViewRow row in grdLTATrvlDetails.Rows)
                {
                    for (int i = 0; i < grdLTATrvlDetails.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            txtLTAHeadJStartdt.Text = grdLTATrvlDetails.Rows[i].Cells[3].Text;
                        }
                        if (i == grdLTATrvlDetails.Rows.Count - 1)
                        {
                            txtLTAHeadJEnddt.Text = grdLTATrvlDetails.Rows[i].Cells[5].Text;
                        }
                    }
                }
                if (btnLTAUpdate.Visible != true)
                    btnLTASubmit.Visible = true;

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }
        }
    }

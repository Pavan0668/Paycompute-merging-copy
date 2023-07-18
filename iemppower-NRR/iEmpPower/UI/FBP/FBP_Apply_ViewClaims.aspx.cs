using iEmpPower.Old_App_Code.iEmpPowerBL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP;
using iEmpPower.Old_App_Code.iEmpPowerDAL.FBP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.FBP
{
    public partial class FBP_Apply_ViewClaims : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //Loadgrd_CalimsItems();
                    //ViewState["FBPClaimDT"] = null;
                    //GV_CorpClaim_NoData();
                    //LaodSavedFbpClaims();

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

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
                Session.Add("IexpGrdInfo", fbpboObj1);

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

        private void Loadgrd_CalimsItems()
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Opening_Balance");
                dt.Columns.Add("Entitlement");
                dt.Columns.Add("Claims_Paid");
                dt.Columns.Add("Claims_Pending");
                dt.Columns.Add("Balance");
                dt.Rows.Add(new object[] { 0, 12500, 9541, 0, 2959 });

                grd_CalimsItems.DataSource = dt;
                grd_CalimsItems.DataBind();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        ////private void LoadGridView1()
        ////{
        ////    try
        ////    {


        ////        DataTable dt = new DataTable();

        ////        dt.Columns.Add("Bill_no");
        ////        dt.Columns.Add("Bill_date");
        ////        dt.Columns.Add("Relationship");
        ////        dt.Columns.Add("Amount");
        ////        dt.Columns.Add("Upload");


        ////    }
        ////    catch (Exception Ex)
        ////    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        ////}

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

                    ViewState["FBPClaimDT"] = Dt;
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
                Dt.Columns.Add("SLNO", typeof(string));
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string date1;
                switch (e.CommandName)
                {
                    case "ADD":
                        using (TextBox TxtBillno = (TextBox)GridView1.FooterRow.FindControl("txtBillNo"))
                        using (TextBox TxtBilldate = (TextBox)GridView1.FooterRow.FindControl("txtBillDate"))
                        using (TextBox TxtRelationship = (TextBox)GridView1.FooterRow.FindControl("txtRelationship"))
                        using (TextBox TxtAmount = (TextBox)GridView1.FooterRow.FindControl("txtAmount"))
                        using (FileUpload fu = (FileUpload)GridView1.FooterRow.FindControl("fuAttachments"))
                        using (HiddenField HF_fid = (HiddenField)GridView1.FooterRow.FindControl("HF_Fid"))
                        {
                            int? FBP_ID = 0;

                            if (HF_fid != null)
                            {
                                if (!string.IsNullOrEmpty(HF_fid.Value))
                                {
                                    FBP_ID = int.Parse(HF_fid.Value.Trim());
                                }
                                else
                                {
                                    FBP_ID = 0;
                                }
                            }



                            bool? Status = true;



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
                            ViewState["Fid"] = FBP_ID;
                            fbpboObj.BILL_NO = TxtBillno.Text.Trim();
                            fbpboObj.BILL_DATE = DateTime.Parse(TxtBilldate.Text.Trim());
                            fbpboObj.RELATIONSHIP = TxtRelationship.Text.Trim();
                            fbpboObj.BILL_AMT = TxtAmount.Text.Trim();
                            fbpboObj.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
                            fbpboObj.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                            fbpboObj.RECEIPT_FPATH = fu.HasFile ? "~/FBPDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";

                            fbpblObj.Create_FbpClaims_Header(fbpboObj, ref FBP_ID, ref Status);
                            if (Status == false)
                            {
                                HF_fid.Value = FBP_ID.ToString();
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

                                    dtfbp.Rows.Add(dtfbp.Rows.Count + 1, iexpboObj.BILL_NO, iexpboObj.BILL_DATE.ToString(), iexpboObj.RELATIONSHIP, iexpboObj.BILL_AMT,
                                         iexpboObj.RECEIPT_FILE, iexpboObj.RECEIPT_FID,
                                          iexpboObj.RECEIPT_FPATH);


                                    FbpboList.Add(iexpboObj);
                                }

                                GridView1.DataSource = dtfbp;
                                GridView1.DataBind();

                            }



                        }




                        break;
                    case "DELETE":
                        int slno = int.Parse(GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())]["SLNO"].ToString());
                        using (DataTable Dt = (DataTable)ViewState["FBPClaimDT"])
                        {
                            DataRow[] rows = (from t in Dt.AsEnumerable().Cast<DataRow>()
                                              where t.Field<string>("SLNO") == slno.ToString().Trim()
                                              select t).ToArray();

                            foreach (DataRow row in rows)
                            {
                                Dt.Rows.Remove(row);
                            }

                            ViewState["FBPClaimDT"] = null;
                            ViewState["FBPClaimDT"] = Dt;
                            if (Dt.Rows.Count > 0)
                            {
                                GridView1.DataSource = (DataTable)ViewState["FBPClaimDT"];
                                GridView1.DataBind();

                            }
                            else
                            {
                                if (ViewState["FBPClaimDT"] != null)
                                {
                                    using (DataTable Dts = (DataTable)ViewState["FBPClaimDT"])
                                    {
                                        if (Dts.Rows.Count > 0)
                                        {
                                            GridView1.DataSource = (DataTable)ViewState["FBPClaimDT"];
                                            GridView1.DataBind();
                                        }
                                        else
                                        {

                                            ViewState["FBPClaimDT"] = null;
                                            GridView1.DataSource = (DataTable)ViewState["FBPClaimDT"];
                                            GridView1.DataBind();
                                            GV_CorpClaim_NoData();
                                            btnSubmitClaims.Visible = false;
                                            btnSave.Visible = false;
                                        }
                                    }
                                }
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
                        string date2;
                        date2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                        int rowindex = Convert.ToInt32(e.CommandArgument);



                        using (TextBox TxtBillnoe = (TextBox)GridView1.Rows[rowindex].FindControl("txtBillNoe"))
                        using (TextBox TxtBilldatee = (TextBox)GridView1.Rows[rowindex].FindControl("txtBillDatee"))
                        using (TextBox TxtRelationshipe = (TextBox)GridView1.Rows[rowindex].FindControl("txtRelationshipe"))
                        using (TextBox TxtAmounte = (TextBox)GridView1.Rows[rowindex].FindControl("txtAmounte"))
                        using (FileUpload fue = (FileUpload)GridView1.Rows[rowindex].FindControl("fuAttachmentse"))
                        //using (LinkButton ltnfue = (LinkButton)GridView1.Rows[rowindex].FindControl("LbtnUploade"))
                        //using (LinkButton ltndeletee = (LinkButton)GridView1.Rows[rowindex].FindControl("LbtnDeletee"))
                        {

                            using (DataTable Dt = (DataTable)ViewState["FBPClaimDT"])
                            {

                                if (Dt.Rows.Count > 0)
                                {

                                    Dt.Rows[rowindex]["BILL_NO"] = TxtBillnoe.Text;

                                    Dt.Rows[rowindex]["BILL_DATE"] = TxtBilldatee.Text;
                                    Dt.Rows[rowindex]["RELATIONSHIP"] = TxtRelationshipe.Text;
                                    Dt.Rows[rowindex]["BILL_AMT"] = TxtAmounte.Text;


                                    if (fue != null)
                                    {
                                        if (fue.HasFile)
                                        {
                                            Dt.Rows[rowindex]["RECEIPT_FID"] = fue.HasFile ? fue.PostedFile.FileName : "";
                                            Dt.Rows[rowindex]["RECEIPT_FPATH"] = fue.HasFile ? "~/FBPDoc/" + User.Identity.Name + "-" + date2 + Path.GetExtension(fue.FileName) : "";
                                            if (fue.HasFile)
                                            {
                                                fue.SaveAs(Server.MapPath("~/FBPDoc/" + User.Identity.Name + "-" + date2) + Path.GetExtension(fue.FileName));
                                            }


                                        }
                                    }
                                    //else
                                    //{
                                    //    Dt.Rows[rowindex]["RECEIPT_FID"] = ViewState["Receiptfileid"].ToString();
                                    //    Dt.Rows[rowindex]["RECEIPT_FPATH"] = ViewState["Receiptfilepath"].ToString();

                                    //}


                                }
                                GridView1.EditIndex = -1;
                                GridView1.DataSource = Dt;
                                GridView1.DataBind();
                                ViewState["FBPClaimDT"] = Dt;
                                btnSubmitClaims.Visible = true;
                                btnSave.Visible = true;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView1.EditIndex = e.NewEditIndex;

            GridView1.DataSource = (DataTable)ViewState["FBPClaimDT"];
            GridView1.DataBind();
            btnSubmitClaims.Visible = false;
            btnSave.Visible = false;

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataSource = (DataTable)ViewState["FBPClaimDT"];
            GridView1.DataBind();
            btnSubmitClaims.Visible = true;
            btnSave.Visible = true;
        }

        protected void btnSubmitClaims_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;



                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.FBPC_IC = int.Parse(ViewState["Fid"].ToString().Trim());

                fbpblObj.Submit_FbpClaims(fbpboObj, ref Status);
                if (Status.Equals(false))
                {
                    ViewState["Fid"] = null;
              
                    ddlPlan.SelectedValue = "0";

                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    GV_CorpClaim_NoData();
                    btnSubmitClaims.Visible = false;

                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LaodSavedFbpClaims();
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
                        grd_SavedFbpClaims.Visible = true;
                        tblreimbursement.Visible = true;
                        ddlPlan.SelectedValue = grd_SavedFbpClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["LGART"].ToString().Trim();
                        int fbpid = int.Parse(grd_SavedFbpClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString().Trim());
                        Loadgrd_CalimsItems();

                        //// fbpboObj1= fbpblObj.Get_BillDetails(fbpid);


                        DataTable dtfbp = GetClaimBlankDt();

                        ////dtfbp=ConvertToDataTable(fbpboObj1);


                        ////GridView1.DataSource = dtfbp;
                        //// GridView1.DataBind();
                        FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
                        List<FbpClaimbo> FbpboList = new List<FbpClaimbo>();
                        foreach (var vRow in objDataContext.usp_Fbp_GetBillDetails(fbpid))
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

                            dtfbp.Rows.Add(dtfbp.Rows.Count + 1, iexpboObj.BILL_NO, iexpboObj.BILL_DATE.ToString(), iexpboObj.RELATIONSHIP, iexpboObj.BILL_AMT,
                                 iexpboObj.RECEIPT_FILE, iexpboObj.RECEIPT_FID,
                                  iexpboObj.RECEIPT_FPATH);


                            FbpboList.Add(iexpboObj);
                        }

                        GridView1.DataSource = dtfbp;
                        GridView1.DataBind();
                        ViewState["FBPClaimDT"] = dtfbp;


                        break;
                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }




        protected void btnNewFbpclaims_Click(object sender, EventArgs e)
        {
            try
            {
                btnNewFbpclaims.Visible = false;
                grd_SavedFbpClaims.Visible = false;
                tblreimbursement.Visible = true;
                ddlPlan.SelectedValue = "0";
                Loadgrd_CalimsItems();
                ViewState["FBPClaimDT"] = null;
                GV_CorpClaim_NoData();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;

                int? FBP_ID = 0;

                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();

                fbpboObj.CREATED_BY = User.Identity.Name;
                fbpboObj.LGART = ddlPlan.SelectedValue.Trim();

                fbpboObj.BEGDA = DateTime.Now;
                fbpboObj.CREATED_ON = DateTime.Now;
                fbpboObj.STATUS = "Saved";











                //if (GridView1.Rows.Count > 0)
                //{

                //    using (DataTable Dt2 = (DataTable)ViewState["FBPClaimDT"])
                //    {

                //        if (Dt2.Rows.Count > 0)
                //        {
                //            if (string.IsNullOrEmpty(Dt2.Rows[0]["BILLNO"].ToString()))
                //            {
                //                Dt2.Rows.RemoveAt(0);
                //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please add atleast 1 claim before submitting !')", true);
                //                return;
                //            }
                //        }
                //    }
                //}
                if (GridView1.Rows.Count > 0)
                {


                    if (ViewState["FBPClaimDT"] != null)
                    {
                        using (DataTable Dt = (DataTable)ViewState["FBPClaimDT"])
                        {

                            decimal d = 0;
                            decimal total = Dt.AsEnumerable()
                             .Where(r => decimal.TryParse(r.Field<string>("BILL_AMT"), out d))
                             .Sum(r => d);
                            fbpboObj.BETRG = total.ToString();

                        }
                    }
                    fbpblObj.Create_FbpClaims_Header(fbpboObj, ref FBP_ID, ref Status);
                    if (Status.Equals(false))
                    {


                        if (ViewState["FBPClaimDT"] != null)
                        {
                            using (DataTable Dt = (DataTable)ViewState["FBPClaimDT"])
                            {
                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    fbpboObj.FBPC_IC = FBP_ID;
                                    fbpboObj.BILL_NO = Dt.Rows[i]["BILL_NO"].ToString();
                                    fbpboObj.BILL_DATE = DateTime.Parse(Dt.Rows[i]["BILL_DATE"].ToString());
                                    fbpboObj.RELATIONSHIP = Dt.Rows[i]["RELATIONSHIP"].ToString();
                                    fbpboObj.BILL_AMT = Dt.Rows[i]["BILL_AMT"].ToString();
                                    fbpboObj.RECEIPT_FILE = Dt.Rows[i]["RECEIPT_FILE"].ToString();
                                    fbpboObj.RECEIPT_FID = Dt.Rows[i]["RECEIPT_FID"].ToString();
                                    fbpboObj.RECEIPT_FPATH = Dt.Rows[i]["RECEIPT_FPATH"].ToString();

                                    fbpblObj.Create_FbpClaims_footer(fbpboObj, ref Status);
                                    if (Status.Equals(true))
                                    {
                                        ddlPlan.SelectedValue = "0";
                                        ViewState["FBPClaimDT"] = null;
                                        GridView1.DataSource = (DataTable)ViewState["FBPClaimDT"];
                                        GridView1.DataBind();
                                        GV_CorpClaim_NoData();
                                        btnSubmitClaims.Visible = false;
                                        btnSave.Visible = false;

                                    }
                                }
                            }
                        }
                    }




                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to create Fbp Claims !')", true);
                    }

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please add atleast 1 claim before submitting !')", true);
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }
    }
}
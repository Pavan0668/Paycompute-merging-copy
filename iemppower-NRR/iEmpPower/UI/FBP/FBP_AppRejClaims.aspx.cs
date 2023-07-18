using iEmpPower.Old_App_Code.iEmpPowerBL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP;
using iEmpPower.Old_App_Code.iEmpPowerDAL.FBP;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.FBP
{
    public partial class FBP_AppRejClaims : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Loadgrd_CalimsItems();
                viewcheck.Value = "NO";
            }
           
        }


        private void Loadgrd_CalimsItems()
        {
              try
                {
                    Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                    List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                    List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
           

                    fbpboObj1 = fbpblObj.Load_FbpClaimsDetails();
                    fbpboObj.AddRange(fbpboObj1);
                    Session.Add("FbpGrdInfo", fbpboObj);

                    if (fbpboObj == null || fbpboObj.Count == 0)
                    {

                        grdFBP_claims.Visible = false;
                        grdFBP_claims.DataSource = null;
                        
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        return;
                    }
                    else
                    {
                        grdFBP_claims.Visible = true;
                        grdFBP_claims.DataSource = fbpboObj;
                        grdFBP_claims.SelectedIndex = -1;
                        
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                    }
                    grdFBP_claims.DataBind();

                   
                }
                catch (Exception Ex)
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

          

        }

        protected void grdFBPclaims_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            { 
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow gvrow in grdFBP_claims.Rows)
                        {
                            gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        if (grdFBP_claims.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString().Trim() == "Withdraw")
                        {
                            divbutton.Visible = false;
                            tblamtremarks.Visible = false;
                        }
                        else
                        {
                            divbutton.Visible = true;
                            tblamtremarks.Visible = true;
                        }
                        divclaims.Visible = true;
                        GridView1.Visible = true;
                       

                        int FBP_ID = int.Parse(grdFBP_claims.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString());
                        HF_FBPID.Value = grdFBP_claims.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString();
                        ViewState["TotalClaimAmount"] = grdFBP_claims.DataKeys[int.Parse(e.CommandArgument.ToString())]["BETRG"].ToString();
                        txtAmnt.Text = grdFBP_claims.DataKeys[int.Parse(e.CommandArgument.ToString())]["BETRG"].ToString();
                        LblReimbursement.Text = grdFBP_claims.DataKeys[int.Parse(e.CommandArgument.ToString())]["LGART"].ToString();
                     Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                    List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                    List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();


                   fbpboObj1 = fbpblObj.Get_BillDetails(FBP_ID);
                   GridView1.DataSource = fbpboObj1;
                   GridView1.DataBind();

                   txtAmnt.Focus();



                        break;
                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
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

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;

                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.FBPC_IC = int.Parse(HF_FBPID.Value.Trim());
                if (txtAmnt.Text.Trim() != "")
                {
                    if (decimal.Parse(decimal.Parse(txtAmnt.Text.Trim()).ToString("#,##0.00")) <= decimal.Parse(decimal.Parse(ViewState["TotalClaimAmount"].ToString().Trim()).ToString("#,##0.00")))
                    {
                        fbpboObj.OVERRIDE_AMT = txtAmnt.Text.Trim();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter override amount less than the claimed amount !')", true);
                        return;
                    }
                }
                else
                {
                    fbpboObj.OVERRIDE_AMT = txtAmnt.Text.Trim();
                }
                fbpboObj.REMARKS = txtRemarks.Text.Trim();
                fbpboObj.STATUS = "Approved";
                fbpblObj.Update_FbpClaim_Status(fbpboObj, ref Status);
                if (Status.Equals(false))
                {
                   SendMailMethod(int.Parse(HF_FBPID.Value.Trim()),"approved");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('FBP Claim Request Approved successfully !')", true);
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    divclaims.Visible = false;
                    txtAmnt.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    Loadgrd_CalimsItems();
                     viewcheck.Value = "NO";
                     txtsearch.Focus();
                }

                HF_FBPID = null;
               
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void SendMailMethod(int fbpidmail, string status)
        {
            try
            {
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                GridView1.FooterRow.Visible = false;
                GridView1.GridLines = GridLines.Both;
                GridView1.RenderControl(hw1);
                GridView1.GridLines = GridLines.None;
                

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
                string Appamt = "";
                FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();

                objcontext.usp_Fbp_Get_MailList_Fbp(fbpidmail, User.Identity.Name,"AppRej", ref EMP_Name, ref EMP_Email, ref Entity,
                     ref Entitlement, ref amount, ref bedga, ref ovrrideamt, ref adminremarks, ref allowancetxt);

                if(ovrrideamt =="")
                {
                    ovrrideamt = "0.00";
                    Appamt=amount;
                }
                else
                {
                    ovrrideamt = decimal.Parse(ovrrideamt).ToString("N2");
                    Appamt = ovrrideamt;
                }


                strSubject = "FBP Claim Request " + fbpidmail + " has been " + status + " by FBP Admin. ";


                //RecipientsString = "karthik.k@itchamps.com";
                //strPernr_Mail = "latha.mg@itchamps.com";

                RecipientsString = EMP_Email;
                strPernr_Mail = "payrolladmin@subex.com";

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>FBP Claim Request " + fbpidmail + " has been " + status + " by FBP Admin. <br/><br/></b>";
                body += "<b>FBP Claim Details</b><br/><hr>";
                body += "<table><tr><td>FBP Claim ID </td><td> :</td><td> " + fbpidmail + "</td></tr>";
                body += "<tr><td>Allowance  </td><td> :</td><td>  " + Entitlement + " - " + allowancetxt + "</td></tr>";
                body += "<tr><td>Date     </td><td> :</td><td>  " + bedga + "</td></tr>";
                body += "<tr><td>Total Amount </td><td> :</td><td>   " + decimal.Parse(amount).ToString("N2") + "</td></tr>";
                body += "<tr><td>Override Amount  </td><td> :</td><td>   " + ovrrideamt + "</td></tr>";
                body += "<tr><td>Approved Amount  </td><td> :</td><td>  " + decimal.Parse(Appamt).ToString("N2") + "</td></tr>";
                body += "<tr><td>Remarks </td><td> :</td><td>  " + adminremarks + "</td></tr></table> <br/>";
                body += sw1.ToString() + "<br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('FBP Claims " + status + "successfully. Error in sending mail');", true);
                return;
            }
        }

        protected void btnReject_Click(object sender, EventArgs e) 
        {
            try
            {
                bool? Status = true;

                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.FBPC_IC =int.Parse(HF_FBPID.Value.Trim());
                fbpboObj.OVERRIDE_AMT = txtAmnt.Text.Trim();
                fbpboObj.REMARKS = txtRemarks.Text.Trim();
                fbpboObj.STATUS = "Rejected";
                fbpblObj.Update_FbpClaim_Status(fbpboObj, ref Status);
                if (Status.Equals(false))
                {
                    SendMailMethod(int.Parse(HF_FBPID.Value.Trim()), "rejected");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('FBP Claim Request Rejected successfully !')", true);
                    divclaims.Visible = false;
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    divclaims.Visible = false;
                    txtAmnt.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    Loadgrd_CalimsItems();
                    viewcheck.Value = "NO";
                    txtsearch.Focus();
                }
                HF_FBPID = null;
               
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void grdFBPclaims_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int pageindex = e.NewPageIndex;
                grdFBP_claims.PageIndex = e.NewPageIndex;

                GridView1.DataSource = null;
                GridView1.DataBind();
                divclaims.Visible = false;
                txtAmnt.Text = string.Empty;
                txtRemarks.Text = string.Empty;

                if (viewcheck.Value == "NO")
                {

                    Loadgrd_CalimsItems();
                }
                else if (viewcheck.Value == "YES")
                {
                    searchdetails();
                }
                HF_FBPID = null;  
                //searchdetails();
                grdFBP_claims.SelectedIndex = -1;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void grdFBPclaims_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (viewcheck.Value == "NO")
                {

                    Loadgrd_CalimsItems();
                }
                else if (viewcheck.Value == "YES")
                {
                    searchdetails();
                }
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

                grdFBP_claims.DataSource = FbpboList;
                grdFBP_claims.DataBind();

                Session.Add("FbpGrdInfo", FbpboList);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                searchdetails();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }


        }
        
        public void searchdetails()
        {

            try
            {
                viewcheck.Value = "YES";
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

                    fbpboObj1 = fbpblObj.Load_Particular_FbpClaimsDetails(User.Identity.Name, SelectedType, textSearch);

                    if (fbpboObj1 == null || fbpboObj1.Count == 0)
                    {
                        MsgCls("No Records found", lblmsg, System.Drawing.Color.Red);
                        grdFBP_claims.Visible = false;
                        grdFBP_claims.DataSource = null;
                        grdFBP_claims.DataBind();
                        divclaims.Visible = false;

                        return;
                    }
                    else
                    {
                        MsgCls("", lblmsg, System.Drawing.Color.Transparent);
                        grdFBP_claims.Visible = true;
                        grdFBP_claims.DataSource = fbpboObj1;
                        grdFBP_claims.SelectedIndex = -1;
                        grdFBP_claims.DataBind();
                        //GV_TravelClaimReqAppRej.Visible = false;
                        //grdAppRejTravel.Visible = false;
                        //Panel1.Visible = false;
                        divclaims.Visible = false;
                        Session.Add("FbpGrdInfo", fbpboObj1);

                    }

                }

            }

            catch (Exception Ex)
            {

                MsgCls("Please enter valid data", lblmsg, System.Drawing.Color.Red);
            }
        }
        private void MsgCls(string LblTxt, Label Lbl, System.Drawing.Color Clr)
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
        protected void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                viewcheck.Value = "NO";
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                Loadgrd_CalimsItems();
                divclaims.Visible = false;
                txtsearch.Focus();

                MsgCls("", lblmsg, System.Drawing.Color.Transparent);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

     

    }
}
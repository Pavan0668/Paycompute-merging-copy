using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerDAL.PR;

namespace iEmpPower.UI.PR
{
    public partial class Purchase_Request : System.Web.UI.Page
    {
        protected MembershipUser memUser;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //getPurchaseId();
                    //Purchase_ReqForm.SetActiveView(Purchase_Details);
                    LoadEmpPRRequestGridView();
                    GetIndentorName();
                    //LoadDropdowns();

                    LoadCurrencyTypes();
                    LoadBillToAddress();
                    LoadShipToAddress();
                    LoadRequestorRegion();
                    LoadERPProjectCode();
                    //  LoadCategory();
                    ////LoadBusinessUnit();
                    LoadMIS_GrpC();
                    LoadRegion();
                    ViewState["PR_ItemsDT"] = null;
                    //txtRequester.Focus();
                    // Page.SetFocus(txtRequester);

                    //if (Session["PRID"] != null)
                    //{
                    //    CopyPR(Session["PRID"].ToString());
                    //    goto displayInfo;
                    //}

                    if (Request.QueryString["NC"] != null)
                    {
                        if (Request.QueryString["NC"] == "C" || Request.QueryString["NC"] == "E")
                        {
                            if (Session["PRID"] != null)
                            {
                                CopyPR(Session["PRID"].ToString(), Request.QueryString["NC"].ToString());
                                goto displayInfo;
                            }
                        }
                        else if (Request.QueryString["NC"] == "N")
                        {
                            Session["PRID"] = null;
                            Session.Clear();
                        }
                    }
                }
                this.Page.Form.Enctype = "multipart/form-data";


                icollapse.Attributes.Add("class", cpe.ClientState == "true" ? "mdi mdi-plus font-20 text-white" : "mdi mdi-minus font-20 text-white");



                Loadfileupload();
                this.Form.DefaultButton = this.btnSubmit.UniqueID;

            displayInfo:
                {
                    ////Console.WriteLine("");
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        void CopyPR(string PRID1, string EditCopy)
        {
            ltPRnum.Text = (EditCopy == "E") ? PRID1 : "NA";//Newly added
            Session["fuProposal"] = null;
            Session["fuAgreement"] = null;
            Session["fuEmailCommunication"] = null;
            Session["fuInvoice"] = null;
            Label1.Text = string.Empty;
            Label2.Text = string.Empty;
            Label3.Text = string.Empty;
            Label4.Text = string.Empty;
            //fuProposal.Visible = false;
            //fuAgreement.Visible = false;
            //fuEmailCommunication.Visible = false;
            //fuInvoice.Visible = false;
            Attachments.Visible = true;
            //int rowIndex = Convert.ToInt32(e.CommandArgument);

            //foreach (GridViewRow row in grdPurchaseItemDetails.Rows)
            //{
            //    row.BackColor = row.RowIndex.Equals(rowIndex) ?
            //    System.Drawing.Color.LightGray :
            //    System.Drawing.Color.White;
            //}


            //int PRID = int.Parse(grdPurchaseItemDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());

            int PRID = int.Parse(PRID1);
            Session["PRID"] = PRID;
            prbl PrBlObj = new prbl();
            List<prbo> requisitionboList = new List<prbo>();
            requisitionboList = PrBlObj.Load_PRItemDetails(PRID);
            string ipernr = requisitionboList[0].IPERNR == null ? "" : requisitionboList[0].IPERNR.ToString().Trim();
            string rpernr = requisitionboList[0].RPERNR == null ? "" : requisitionboList[0].RPERNR.ToString().Trim();
            ViewState["RequesterID"] = requisitionboList[0].RPERNR == null ? "" : requisitionboList[0].RPERNR.ToString().Trim();
            string rpernrname = requisitionboList[0].ENAME == null ? "" : requisitionboList[0].ENAME.ToString().Trim();
            string PFUNC_AREA = requisitionboList[0].PFUNC_AREA == null ? "" : requisitionboList[0].PFUNC_AREA.ToString().Trim();
            string BTEXT = requisitionboList[0].BTEXT == null ? "" : requisitionboList[0].BTEXT.ToString().Trim();
            string MIS_GRPC = requisitionboList[0].MIS_GRPC == null ? "" : requisitionboList[0].MIS_GRPC.ToString().Trim();
            string MIS_GRPA = requisitionboList[0].MIS_GRPA == null ? "" : requisitionboList[0].MIS_GRPA.ToString().Trim();
            string MIS_GRPB = requisitionboList[0].MIS_GRPB == null ? "" : requisitionboList[0].MIS_GRPB.ToString().Trim();
            string EKGRP = requisitionboList[0].EKGRP == null ? "" : requisitionboList[0].EKGRP.ToString().Trim();
            string BWERKS = requisitionboList[0].BWERKS == null ? "" : requisitionboList[0].BWERKS.ToString().Trim();
            string SWERKS = requisitionboList[0].SWERKS == null ? "" : requisitionboList[0].SWERKS.ToString().Trim();
            string SUG_SUPP = requisitionboList[0].SUG_SUPP == null ? "" : requisitionboList[0].SUG_SUPP.ToString().Trim();
            string SUP_ADDRESS = requisitionboList[0].SUP_ADDRESS == null ? "" : requisitionboList[0].SUP_ADDRESS.ToString().Trim();
            string SUP_PHONE = requisitionboList[0].SUP_PHONE == null ? "" : requisitionboList[0].SUP_PHONE.ToString().Trim();
            string IN_BUDGET = requisitionboList[0].IN_BUDGET == null ? "" : requisitionboList[0].IN_BUDGET.ToString().Trim();
            string CAPITALIZED = requisitionboList[0].CAPITALIZED == null ? "" : requisitionboList[0].CAPITALIZED.ToString().Trim();
            string SERVICE_BUREA = requisitionboList[0].SERVICE_BUREA == null ? "" : requisitionboList[0].SERVICE_BUREA.ToString().Trim();
            string CRITICALITY = requisitionboList[0].CRITICALITY == null ? "" : requisitionboList[0].CRITICALITY.ToString().Trim();
            string PSPNR = requisitionboList[0].PSPNR == null ? "" : requisitionboList[0].PSPNR.ToString().Trim();
            string VERNR = requisitionboList[0].VERNR == null ? "" : requisitionboList[0].VERNR.ToString().Trim();
            string SPART = requisitionboList[0].SPART == null ? "" : requisitionboList[0].SPART.ToString().Trim();
            string JUSTIFICATION = requisitionboList[0].JUSTIFICATION == null ? "" : requisitionboList[0].JUSTIFICATION.ToString().Trim();
            string SPL_NOTES = requisitionboList[0].SPL_NOTES == null ? "" : requisitionboList[0].SPL_NOTES.ToString().Trim();
            string WAERS = requisitionboList[0].WAERS == null ? "" : requisitionboList[0].WAERS.ToString().Trim();
            string MISCIDID = requisitionboList[0].MISCIDID == null ? "" : requisitionboList[0].MISCIDID.ToString().Trim();
            string BWERKSID = requisitionboList[0].BWERKSID == null ? "" : requisitionboList[0].BWERKSID.ToString().Trim();
            string SWERKSID = requisitionboList[0].SWERKSID == null ? "" : requisitionboList[0].SWERKSID.ToString().Trim();
            string PSPNRID = requisitionboList[0].PSPNRID == null ? "" : requisitionboList[0].PSPNRID.ToString().Trim();
            string SPARTID = requisitionboList[0].SPARTID == null ? "" : requisitionboList[0].SPARTID.ToString().Trim();
            string CAP_TEXT = requisitionboList[0].CAP_TEXT == null ? "" : requisitionboList[0].CAP_TEXT.ToString().Trim();
            string REGID = requisitionboList[0].REGIONID == null ? "" : requisitionboList[0].REGIONID.ToString().Trim();

            /*-----File Attachments---Starts------*/


            //PrBO.PROPOSAL = RbtnListProposal.SelectedValue.Equals("1") ? "YES" : "NO";


            //PrBO.PFID = fuProposal.HasFile ? Path.GetExtension(fuProposal.FileName) : "";
            //PrBO.PFPATH = fuProposal.HasFile ? Path.GetExtension(fuProposal.FileName) : "";



            //PrBO.AGREEMENT = RbtnListAgreement.SelectedValue.Equals("1") ? "YES" : "NO";
            //PrBO.AFID = fuAgreement.HasFile ? Path.GetExtension(fuAgreement.FileName) : "";
            //PrBO.AFPATH = fuAgreement.HasFile ? Path.GetExtension(fuAgreement.FileName) : "";

            //PrBO.EMAIL_COM = RbtnListEmailCommunication.SelectedValue.Equals("1") ? "YES" : "NO";
            //PrBO.EFID = fuEmailCommunication.HasFile ? Path.GetExtension(fuEmailCommunication.FileName) : "";
            //PrBO.EFPATH = fuEmailCommunication.HasFile ? Path.GetExtension(fuEmailCommunication.FileName) : "";



            //PrBO.INVOICE = RbtnListInvoice.SelectedValue.Equals("1") ? "YES" : "NO";
            //PrBO.IFID = fuInvoice.HasFile ? Path.GetExtension(fuInvoice.FileName) : "";
            //PrBO.IFPATH = fuInvoice.HasFile ? Path.GetExtension(fuInvoice.FileName) : "";



            string PROPOSAL = requisitionboList[0].PROPOSAL == null ? "" : requisitionboList[0].PROPOSAL.ToString().Trim();
            string PFID = requisitionboList[0].PFID == null ? "" : requisitionboList[0].PFID.ToString().Trim();
            string PFPATH = requisitionboList[0].PFPATH == null ? "" : requisitionboList[0].PFPATH.ToString().Trim();

            RbtnListProposal.SelectedValue = PROPOSAL == "YES" ? "1" : "0";
            //Label1.Text = PFID;
            ////LbtnProposal.Text = PFID;


            string AGREEMENT = requisitionboList[0].AGREEMENT == null ? "" : requisitionboList[0].AGREEMENT.ToString().Trim();
            string AFID = requisitionboList[0].AFID == null ? "" : requisitionboList[0].AFID.ToString().Trim();
            string AFPATH = requisitionboList[0].AFPATH == null ? "" : requisitionboList[0].AFPATH.ToString().Trim();

            RbtnListAgreement.SelectedValue = AGREEMENT == "YES" ? "1" : "0";
            //Label2.Text = AFID;
            ////LbtnAgreement.Text = AFID;

            string EMAIL_COM = requisitionboList[0].EMAIL_COM == null ? "" : requisitionboList[0].EMAIL_COM.ToString().Trim();
            string EFID = requisitionboList[0].EFID == null ? "" : requisitionboList[0].EFID.ToString().Trim();
            string EFPATH = requisitionboList[0].EFPATH == null ? "" : requisitionboList[0].EFPATH.ToString().Trim();

            RbtnListEmailCommunication.SelectedValue = EMAIL_COM == "YES" ? "1" : "0";
            //Label3.Text = EFID;
            ////LbtnEmailCommunication.Text = EFID;

            string INVOICE = requisitionboList[0].INVOICE == null ? "" : requisitionboList[0].INVOICE.ToString().Trim();
            string IFID = requisitionboList[0].IFID == null ? "" : requisitionboList[0].IFID.ToString().Trim();
            string IFPATH = requisitionboList[0].IFPATH == null ? "" : requisitionboList[0].IFPATH.ToString().Trim();

            RbtnListInvoice.SelectedValue = INVOICE == "YES" ? "1" : "0";
            //Label4.Text = IFID;
            ////LbtnInvoice.Text = IFID;



            //Newly added starts
            if (EditCopy == "E")
            {
                LbtnProposal.Text = PFID;
                LbtnAgreement.Text = AFID;
                LbtnEmailCommunication.Text = EFID;
                LbtnInvoice.Text = IFID;
            }
            //Newly added ends

            /*-----File Attachments---Ends------*/


            txtRequester.Text = rpernrname + " - " + rpernr;
            ltRequestor.Text = rpernrname;
            txtMainFunction.Text = PFUNC_AREA;
            txtSubFunction.Text = BTEXT;
            ddlMISGroupC.SelectedValue = MISCIDID;
            LoadMIS_GrpAB();
            ddlRequesterRegion.SelectedValue = EKGRP;
            ddlBillToAddress.SelectedValue = BWERKSID;
            ddlShipToAddress.SelectedValue = ddlBillToAddress.SelectedValue;//SWERKSID;
            txtShipToAddress.Text = ddlShipToAddress.SelectedItem.Text;//Newly Added
            txtSuggestedSupplier.Text = SUG_SUPP;
            txtSuggestedAddress.Text = SUP_ADDRESS;
            txtSupplierPhone.Text = SUP_PHONE;
            if (IN_BUDGET == "NO")
            {
                rbtnBudgetNo.Checked = true;
                rbtnBudgetYes.Checked = false;
                txtWillthisItembeCapitalized.Enabled = txtWillthisItembeCapitalized.Visible = false;
            }
            else
            {
                rbtnBudgetNo.Checked = false;
                rbtnBudgetYes.Checked = true;
                txtWillthisItembeCapitalized.Enabled = txtWillthisItembeCapitalized.Visible = true;
            }

            if (CAPITALIZED == "NO")
            {
                rbtnCapitalizedNo.Checked = true;
                rbtnCapitalizedYes.Checked = false;
                ////txtWillthisItembeCapitalized.Enabled = false;
            }
            else
            {
                rbtnCapitalizedNo.Checked = false;
                rbtnCapitalizedYes.Checked = true;
                txtWillthisItembeCapitalized.Enabled = true;
                txtWillthisItembeCapitalized.Text = CAP_TEXT;
            }
            if (SERVICE_BUREA == "NO")
            {
                rbtnServiceNo.Checked = true;
                rbtnServiceYes.Checked = false;
            }
            else
            {
                rbtnServiceNo.Checked = false;
                rbtnServiceYes.Checked = true;
            }

            ddlCriticality.SelectedValue = CRITICALITY;
            ddlERPProjectCode.SelectedValue = PSPNRID;
            ltProject.Text = ddlERPProjectCode.SelectedValue;
            if (REGID == "")
            {
                ddlRegion.SelectedValue = "0";
            }
            else
            {
                ddlRegion.SelectedValue = REGID;
            }
            LoadERPPrjMngr();
            if (ddlERPProjectCode.SelectedValue.StartsWith("E/"))
            {
                ddlCategory.SelectedValue = "Project";
                ddlCategory.Items[0].Enabled = false;
                ddlCategory.Items[1].Enabled = false;
                ddlCategory.Items[2].Enabled = false;
                ddlCategory.Items[3].Enabled = false;
                ddlCategory.Items[4].Enabled = true;
                ddlCategory.Items[5].Enabled = false;
                ddlCategory.Items[3].Enabled = false;

                ddlCategory.Items[0].Enabled = false;
                ddlCategory.Items[1].Enabled = false;
                ddlCategory.Items[2].Enabled = false;
                ddlCategory.Items[4].Enabled = true;
                ddlCategory.Items[5].Enabled = false;
                ddlItemDescription.Visible = false;
                txtItemDesc.Visible = txtItemDesc.Enabled = true;
                LoadUnitofMeasurements();
                RFV_txtItemDesc.Enabled = true;
                RFV_txtItemDesc.Visible = true;
                RFV_ddlItemDescription.Enabled = false;
                RFV_ddlItemDescription.Visible = false;
                fuProposal = (FileUpload)Session["fuProposal"];
            }

            else if (ddlERPProjectCode.SelectedValue.StartsWith("I/") && (rbtnCapitalizedYes.Checked == true))
            {
                ddlCategory.SelectedValue = "Asset";
                ddlCategory.Items[4].Attributes.Add("enabled", "enabled");
                ddlCategory.Items[0].Enabled = false;
                ddlCategory.Items[1].Enabled = false;
                ddlCategory.Items[2].Enabled = true;
                ddlCategory.Items[3].Enabled = false;
                ddlCategory.Items[4].Enabled = false;
                ddlCategory.Items[5].Enabled = false;
                ddlCategory.Items[4].Enabled = false;
                ddlCategory.Items[0].Enabled = false;
                ddlCategory.Items[1].Enabled = false;
                ddlCategory.Items[2].Enabled = true;
                ddlCategory.Items[3].Enabled = false;
                ddlCategory.Items[5].Enabled = false;
                ddlItemDescription.Visible = false;
                txtItemDesc.Visible = txtItemDesc.Enabled = true;
                LoadUnitofMeasurements();
                RFV_txtItemDesc.Enabled = true;
                RFV_txtItemDesc.Visible = true;
                RFV_ddlItemDescription.Enabled = false;
                RFV_ddlItemDescription.Visible = false;

                fuProposal = (FileUpload)Session["fuProposal"];
            }



            else if (ddlERPProjectCode.SelectedValue.StartsWith("I/") && (rbtnCapitalizedNo.Checked == true))
            {

                ddlCategory.SelectedValue = "0";
                ddlCategory.Items[0].Attributes.Add("enabled", "enabled");
                ddlCategory.Items[1].Attributes.Add("enabled", "enabled");
                ddlCategory.Items[2].Attributes.Add("enabled", "enabled");
                ddlCategory.Items[5].Attributes.Add("enabled", "enabled");
                ddlCategory.Items[0].Enabled = false;
                ddlCategory.Items[1].Enabled = true;
                ddlCategory.Items[2].Enabled = false;
                ddlCategory.Items[3].Enabled = true;
                ddlCategory.Items[4].Enabled = false;
                ddlCategory.Items[5].Enabled = true;
                ddlCategory.Items[0].Enabled = true;
                ddlCategory.Items[1].Enabled = true;
                ddlCategory.Items[2].Enabled = false;
                ddlCategory.Items[5].Enabled = true;
                ddlCategory.Items[3].Enabled = true;
                ddlCategory.Items[4].Enabled = false;
                ddlItemDescription.Visible = false;
                txtItemDesc.Visible = txtItemDesc.Enabled = true;
                LoadUnitofMeasurements();
                RFV_txtItemDesc.Enabled = true;
                RFV_txtItemDesc.Visible = true;
                RFV_ddlItemDescription.Enabled = false;
                RFV_ddlItemDescription.Visible = false;
                fuProposal = (FileUpload)Session["fuProposal"];

            }





            LoadBusinessUnit(ddlBillToAddress.SelectedValue);
            ddlBusinessUnit.SelectedValue = SPARTID;
            txtJustification.Text = JUSTIFICATION;
            txtSpecialNotes.Text = SPL_NOTES;


            requisitionboList = PrBlObj.Load_PRItem(PRID);
            grd_ItemInfo.DataSource = requisitionboList;
            grd_ItemInfo.DataBind();
            dvlineitem.Visible = true;

            //          Dt.Columns.Add("ID", typeof(int));
            //Dt.Columns.Add("BANFN_EXT", typeof(int));
            //Dt.Columns.Add("BNFPO", typeof(string));//MTART
            ////Dt.Columns.Add("MTART", typeof(string));
            //Dt.Columns.Add("MATNR", typeof(string));
            //Dt.Columns.Add("TXZ01", typeof(string));
            //Dt.Columns.Add("PART_NO", typeof(string));
            //Dt.Columns.Add("MTART", typeof(string));
            //Dt.Columns.Add("MEINS", typeof(string));
            //Dt.Columns.Add("NO_OF_UNITS", typeof(string));
            //Dt.Columns.Add("UNIT_PRICE", typeof(string));
            //Dt.Columns.Add("WAERS", typeof(string));
            //Dt.Columns.Add("TAXABLE", typeof(string));
            //Dt.Columns.Add("ITEM_NOTE", typeof(string));
            ViewState["PR_ItemsDT"] = null;
            int listid = 1;
            if (grd_ItemInfo.Rows.Count > 0)
            {
                foreach (GridViewRow row in grd_ItemInfo.Rows)
                {

                    if (ViewState["PR_ItemsDT"] != null)
                    {
                        using (DataTable Dt = (DataTable)ViewState["PR_ItemsDT"])
                        {
                            Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, requisitionboList[listid].MTART.ToString().Trim(), requisitionboList[listid].MATNR.ToString().Trim()
                                    , requisitionboList[listid].TXZ01.ToString().Trim(), requisitionboList[listid].PART_NO.ToString().Trim(),
                                    requisitionboList[listid].MTART.ToString().Trim(),
                                    requisitionboList[listid].MEINS.ToString().Trim(), requisitionboList[listid].NO_OF_UNITS.ToString().Trim()
                                    , requisitionboList[listid].UNIT_PRICE.ToString().Trim(), requisitionboList[listid].WAERS.ToString().Trim(),
                                   requisitionboList[listid].TAXABLE.ToString().Trim(), requisitionboList[listid].ITEM_NOTE.ToString().Trim());
                            grd_ItemInfo.DataSource = Dt;
                            grd_ItemInfo.DataBind();
                            //listid =+1;
                            listid = listid + 1;
                            //ViewState["PR_ItemsDT"] = Dt;

                            //----------------------------------------------------------
                            decimal d = 0;
                            //decimal totalAmount = Dt.AsEnumerable()
                            //         .Where(r => decimal.TryParse(r.Field<string>("UNIT_PRICE"), out d))
                            //         .Sum(r => d);
                            //decimal totalUnits = Dt.AsEnumerable()
                            //         .Where(r => decimal.TryParse(r.Field<string>("NO_OF_UNITS"), out d))
                            //         .Sum(r => d);

                            //decimal totalAmount = Dt.AsEnumerable()
                            //          .Where(r => decimal.TryParse(r.Field<string>("UNIT_PRICE"), out d))
                            //          .Sum(r => r.Field<decimal>("UNIT_PRICE") * r.Field<decimal>("NO_OF_UNITS"));//.Sum(r => d);




                            //decimal total = totalAmount;// * totalUnits;
                            //grd_ItemInfo.FooterRow.Cells[7].Text = "Total";

                            //grd_ItemInfo.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                            //grd_ItemInfo.FooterRow.Cells[8].Text = total.ToString("N2") + "(" + (WAERS) + ")";

                            //----------------------------------------------------------

                            if (ddlCategory.SelectedValue == "Product")
                            {
                                ddlItemDescription.Focus();
                            }
                            else
                            {
                                txtItemDesc.Focus();
                            }
                        }

                    }
                    else
                    {
                        using (DataTable Dt = GetPR_ItemsDt())
                        {
                            //requisitionboList[0].APPROVEDBY1 == null ? "" : requisitionboList[0].APPROVEDBY1.ToString()

                            Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, requisitionboList[0].MTART.ToString().Trim(), requisitionboList[0].MATNR.ToString().Trim()
                                , requisitionboList[0].TXZ01.ToString().Trim(), requisitionboList[0].PART_NO.ToString().Trim(), requisitionboList[0].MTART.ToString().Trim(),
                                requisitionboList[0].MEINS.ToString().Trim(), requisitionboList[0].NO_OF_UNITS.ToString().Trim()
                                , requisitionboList[0].UNIT_PRICE.ToString().Trim(), requisitionboList[0].WAERS.ToString().Trim(),
                               requisitionboList[0].TAXABLE.ToString().Trim(), requisitionboList[0].ITEM_NOTE.ToString().Trim());
                            grd_ItemInfo.DataSource = Dt;
                            grd_ItemInfo.DataBind();
                            ViewState["PR_ItemsDT"] = Dt;

                            //----------------------------------------------------------
                            //decimal d = 0;
                            //decimal totalAmount = Dt.AsEnumerable()
                            //         .Where(r => decimal.TryParse(r.Field<string>("UNIT_PRICE"), out d))
                            //         .Sum(r => d);
                            //decimal totalUnits = Dt.AsEnumerable()
                            //         .Where(r => decimal.TryParse(r.Field<string>("NO_OF_UNITS"), out d))
                            //         .Sum(r => d);
                            //decimal total = totalAmount * totalUnits;
                            //grd_ItemInfo.FooterRow.Cells[7].Text = "Total";

                            //grd_ItemInfo.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                            //grd_ItemInfo.FooterRow.Cells[8].Text = total.ToString("N2") + "(" + (WAERS) + ")";

                            //----------------------------------------------------------

                            if (ddlCategory.SelectedValue == "Product")
                            {
                                ddlItemDescription.Focus();
                            }
                            else
                            {
                                txtItemDesc.Focus();
                            }
                            //rbtnBudgetYes.Enabled = false;
                            //rbtnBudgetNo.Enabled = false;
                            rbtnCapitalizedYes.Enabled = false;
                            rbtnCapitalizedNo.Enabled = false;
                            ////txtWillthisItembeCapitalized.Enabled = false;
                            ddlERPProjectCode.Enabled = false;
                        }
                    }

                }
                totalAmt();
            }
            tdwarning.Visible = (LbtnProposal.Text != "" || LbtnAgreement.Text != "" || LbtnEmailCommunication.Text != "" || LbtnInvoice.Text != "") ? true : false;//newly added
        }


        public void LbtnProposal_Click(Object sender, EventArgs e)
        {
            string filePath = (Server.MapPath("~/PRDoc/" + LbtnProposal.Text));
            Response.ContentType = "application/octet-stream";
            //Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.TransmitFile(filePath); //Response.WriteFile(filePath);
            Response.End();
        }

        public void LbtnAgreement_Click(Object sender, EventArgs e)
        {
            string filePath = (Server.MapPath("~/PRDoc/" + LbtnAgreement.Text));
            Response.ContentType = "application/octet-stream";
            //Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.TransmitFile(filePath); //Response.WriteFile(filePath);
            Response.End();
        }

        public void LbtnEmailCommunication_Click(Object sender, EventArgs e)
        {
            string filePath = (Server.MapPath("~/PRDoc/" + LbtnEmailCommunication.Text));
            Response.ContentType = "application/octet-stream";
            //Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.TransmitFile(filePath); //Response.WriteFile(filePath);
            Response.End();
        }

        public void LbtnInvoice_Click(Object sender, EventArgs e)
        {
            string filePath = (Server.MapPath("~/PRDoc/" + LbtnInvoice.Text));
            Response.ContentType = "application/octet-stream";
            //Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.TransmitFile(filePath); //Response.WriteFile(filePath);
            Response.End();
        }

        private void LoadRegion()
        {
            mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Region();
            ddlRegion.DataSource = objLst;
            ddlRegion.DataTextField = "REGION_TEXT";
            ddlRegion.DataValueField = "REGION_ID";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, new ListItem("- SELECT -", "0"));
            ddlRegion.SelectedValue = "0";
        }

        public void Loadfileupload()
        {
            if (Session["fuProposal"] == null && fuProposal.HasFile)
            {
                Session["fuProposal"] = fuProposal;
                Label1.Text = fuProposal.FileName;
                //fuProposalpath.Text = fuAgreement.HasFile ? Path.GetFullPath(fuProposal.PostedFile.FileName) : "";
            }
            // Next time submit and Session has values but FileUpload is Blank
            // Return the values from session to FileUpload
            else if (Session["fuProposal"] != null && (!fuProposal.HasFile))
            {
                fuProposal = (FileUpload)Session["fuProposal"];
                Label1.Text = fuProposal.FileName;
                //fuProposalpath.Text = fuAgreement.HasFile ? Path.GetFullPath(fuProposal.PostedFile.FileName) : "";
            }
            // Now there could be another sictution when Session has File but user want to change the file
            // In this case we have to change the file in session object
            else if (fuProposal.HasFile)
            {
                Session["fuProposal"] = fuProposal;
                Label1.Text = fuProposal.FileName;
                // fuProposalpath.Text = fuAgreement.HasFile ? Path.GetFullPath(fuProposal.PostedFile.FileName) : "";
            }

            if (Session["fuAgreement"] == null && fuAgreement.HasFile)
            {
                Session["fuAgreement"] = fuAgreement;
                Label2.Text = fuAgreement.FileName;
                //fuAgreementpath.Text = fuAgreement.HasFile ? Path.GetFullPath(fuAgreement.PostedFile.FileName) : "";
            }
            // Next time submit and Session has values but FileUpload is Blank
            // Return the values from session to FileUpload
            else if (Session["fuAgreement"] != null && (!fuAgreement.HasFile))
            {
                fuAgreement = (FileUpload)Session["fuAgreement"];
                Label2.Text = fuAgreement.FileName;
                //fuAgreementpath.Text = fuAgreement.HasFile ? Path.GetFullPath(fuAgreement.PostedFile.FileName) : "";
            }
            // Now there could be another sictution when Session has File but user want to change the file
            // In this case we have to change the file in session object
            else if (fuAgreement.HasFile)
            {
                Session["fuAgreement"] = fuAgreement;
                Label2.Text = fuAgreement.FileName;
                // fuAgreementpath.Text = fuAgreement.HasFile ? Path.GetFullPath(fuAgreement.PostedFile.FileName) : "";
            }



            if (Session["fuEmailCommunication"] == null && fuEmailCommunication.HasFile)
            {
                Session["fuEmailCommunication"] = fuEmailCommunication;
                Label3.Text = fuEmailCommunication.FileName;
                //fuEmailCommunicationpath.Text = fuEmailCommunication.HasFile ? Path.GetFullPath(fuEmailCommunication.PostedFile.FileName) : "";
            }
            // Next time submit and Session has values but FileUpload is Blank
            // Return the values from session to FileUpload
            else if (Session["fuEmailCommunication"] != null && (!fuEmailCommunication.HasFile))
            {
                fuEmailCommunication = (FileUpload)Session["fuEmailCommunication"];
                Label3.Text = fuEmailCommunication.FileName;
                //fuEmailCommunicationpath.Text = fuEmailCommunication.HasFile ? Path.GetFullPath(fuEmailCommunication.PostedFile.FileName) : "";
            }
            // Now there could be another sictution when Session has File but user want to change the file
            // In this case we have to change the file in session object
            else if (fuEmailCommunication.HasFile)
            {
                Session["fuEmailCommunication"] = fuEmailCommunication;
                Label3.Text = fuEmailCommunication.FileName;
                //fuEmailCommunicationpath.Text = fuEmailCommunication.HasFile ? Path.GetFullPath(fuEmailCommunication.PostedFile.FileName) : "";
            }

            if (Session["fuInvoice"] == null && fuInvoice.HasFile)
            {
                Session["fuInvoice"] = fuInvoice;
                Label4.Text = fuInvoice.FileName;
                //fuInvoicepath.Text = fuInvoice.HasFile ? Path.GetFullPath(fuInvoice.PostedFile.FileName) : "";
            }
            // Next time submit and Session has values but FileUpload is Blank
            // Return the values from session to FileUpload
            else if (Session["fuInvoice"] != null && (!fuInvoice.HasFile))
            {
                fuInvoice = (FileUpload)Session["fuInvoice"];
                Label4.Text = fuInvoice.FileName;
                //fuInvoicepath.Text = fuInvoice.HasFile ? Path.GetFullPath(fuInvoice.PostedFile.FileName) : "";
            }
            // Now there could be another sictution when Session has File but user want to change the file
            // In this case we have to change the file in session object
            else if (fuInvoice.HasFile)
            {
                Session["fuInvoice"] = fuInvoice;
                Label4.Text = fuInvoice.FileName;
                //fuInvoicepath.Text = fuInvoice.HasFile ? Path.GetFullPath(fuInvoice.PostedFile.FileName) : "";
            }

        }

        #region PR Items Empty DataTable

        private DataTable GetPR_ItemsDt()
        {
            try
            {
                DataTable Dt = new DataTable("PR_ITEMS");
                Dt.Columns.Add("ID", typeof(int));
                Dt.Columns.Add("BANFN_EXT", typeof(int));
                Dt.Columns.Add("BNFPO", typeof(string));//MTART
                //Dt.Columns.Add("MTART", typeof(string));
                Dt.Columns.Add("MATNR", typeof(string));
                Dt.Columns.Add("TXZ01", typeof(string));
                Dt.Columns.Add("PART_NO", typeof(string));
                Dt.Columns.Add("MTART", typeof(string));
                Dt.Columns.Add("MEINS", typeof(string));
                Dt.Columns.Add("NO_OF_UNITS", typeof(string));
                Dt.Columns.Add("UNIT_PRICE", typeof(string));
                Dt.Columns.Add("WAERS", typeof(string));
                Dt.Columns.Add("TAXABLE", typeof(string));
                Dt.Columns.Add("ITEM_NOTE", typeof(string));

                return Dt;
            }
            catch (Exception Ex)
            { throw Ex; return null; }
        }
        #endregion


        private void LoadMIS_GrpC()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_MIS_GRPC();
                ddlMISGroupC.DataSource = objLst;
                ddlMISGroupC.DataTextField = "C_DESC";
                ddlMISGroupC.DataValueField = "CID";
                ddlMISGroupC.DataBind();
                ddlMISGroupC.Items.Insert(0, new ListItem("- SELECT -", "0"));
                ddlMISGroupC.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadBusinessUnit(string entity)
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Business_Unit(entity);
                ddlBusinessUnit.DataSource = objLst;
                ddlBusinessUnit.DataTextField = "VTEXT";
                ddlBusinessUnit.DataValueField = "SPART";
                ddlBusinessUnit.DataBind();
                ddlBusinessUnit.Items.Insert(0, new ListItem("- SELECT -", "-1"));
                ddlBusinessUnit.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadParticularUnitofMeasurement()
        {
            try
            {
                masterbo mBo = new masterbo();
                mBo.LL = ddlItemDescription.SelectedValue.ToString();

                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Unit_of_Measurements(mBo);
                ddlUnitOfMeasurement.DataSource = objLst;
                ddlUnitOfMeasurement.DataTextField = "MEINS";
                ddlUnitOfMeasurement.DataValueField = "MEINS";
                ddlUnitOfMeasurement.DataBind();
                ddlUnitOfMeasurement.Items.Insert(0, new ListItem("- SELECT -", "0"));
                ddlUnitOfMeasurement.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadItemDesc()
        {
            try
            {
                masterbo mBo = new masterbo();

                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_ItemDesc();
                ddlItemDescription.DataSource = objLst;
                ddlItemDescription.DataTextField = "MAKTX";
                ddlItemDescription.DataValueField = "MATNR";
                ddlItemDescription.DataBind();
                ddlItemDescription.Items.Insert(0, new ListItem("- SEARCH -", "0"));
                ddlItemDescription.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        //private void LoadCategory()
        //{
        //    mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Item_Category();
        //    ddlCategory.DataSource = objLst;
        //    ddlCategory.DataTextField = "MTBEZ";
        //    ddlCategory.DataValueField = "MTART";
        //    ddlCategory.DataBind();
        //    ddlCategory.Items.Insert(0, new ListItem("- SELECT -", "0"));
        //    ddlCategory.SelectedValue = "0";
        //}

        private void LoadERPPrjMngr()
        {
            try
            {
                masterbo mBo = new masterbo();
                mBo.LL = ddlERPProjectCode.SelectedValue.ToString();
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_PrMngr_forPrjCode(mBo);
                //ddlProjectDeliveryHeadName.DataSource = objLst;
                //ddlProjectDeliveryHeadName.DataTextField = "VERNA";
                //ddlProjectDeliveryHeadName.DataValueField = "VERNR";
                // ddlProjectDeliveryHeadName.DataBind();
                foreach (masterbo objBo in objLst)
                {

                    ddlProjectDeliveryHeadName.Text = objBo.VERNA.ToString();
                    txtProjectDeliveryHeadID.Text = objBo.VERNR.ToString();


                }

                //ddlProjectDeliveryHeadName.Items.Insert(0, new ListItem("- SELECT -", "0"));
                //ddlProjectDeliveryHeadName.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadERPProjectCode()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_ERP_ProjectCode();
                ddlERPProjectCode.DataSource = objLst;
                ddlERPProjectCode.DataTextField = "POST1";
                ddlERPProjectCode.DataValueField = "POSID";
                ddlERPProjectCode.DataBind();
                ddlERPProjectCode.Items.Insert(0, new ListItem("- SEARCH -", "0"));
                ddlERPProjectCode.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadRequestorRegion()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Requestor_Region();
                ddlRequesterRegion.DataSource = objLst;
                ddlRequesterRegion.DataTextField = "EKNAM";
                ddlRequesterRegion.DataValueField = "EKGRP";
                ddlRequesterRegion.DataBind();
                ddlRequesterRegion.Items.Insert(0, new ListItem("- SELECT -", "0"));
                ddlRequesterRegion.SelectedValue = "0";



            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadBillToAddress()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_BillShipTo_Address();
                ddlBillToAddress.DataSource = objLst;
                ddlBillToAddress.DataTextField = "NAME1";
                ddlBillToAddress.DataValueField = "WERKS";
                ddlBillToAddress.DataBind();
                ddlBillToAddress.Items.Insert(0, new ListItem("- SELECT -", "0"));
                ddlBillToAddress.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadShipToAddress()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_BillShipTo_Address();
                ddlShipToAddress.DataSource = objLst;
                ddlShipToAddress.DataTextField = "NAME1";
                ddlShipToAddress.DataValueField = "WERKS";
                ddlShipToAddress.DataBind();
                ddlShipToAddress.Items.Insert(0, new ListItem("- SELECT Bill To Address -", "0"));
                ddlShipToAddress.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void GetIndentorName()
        {
            try
            {
                memUser = Membership.GetUser();
                prbl objBl = new prbl();
                prcollectionbo objLst = objBl.Load_employeeName(memUser.ToString());
                foreach (prbo objBo in objLst)
                {

                    //  Label lName = HeadLoginView.FindControl("lblEmployyeName") as Label;
                    txtIndentor.Text = objBo.ENAME;
                    ltIndentor.Text = objBo.ENAME;
                    ltIndentorHeader.Text = objBo.ENAME;
                    Session.Add("sEmploreeId", memUser.ToString());
                    Session.Add("EmployeeName", objBo.ENAME);
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        //public void LoadDropdowns()
        // {
        //     try
        //     {
        //         prbl objprbl = new prbl();
        //         DDL_Indentor.DataSource = objprbl.getIndentorNames();
        //         DDL_Indentor.DataTextField = "ENAME";
        //         DDL_Indentor.DataValueField = "PERNR";
        //         DDL_Indentor.DataBind();
        //         DDL_Indentor.Items.Insert(0, new ListItem("- SELECT -", "0"));
        //         DDL_Indentor.SelectedValue = "0";
        //     }
        //     catch (Exception Ex)
        //     { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }


        // }

        //public void getPurchaseId()
        //{
        //    try
        //    {
        //        Int32? PRID = 0;
        //        // prbo objPRAddBo = new prbo();
        //        prbl objPrAddBl = new prbl();
        //        prcollectionbo prDataList = objPrAddBl.getPurchaseID(ref PRID);
        //        foreach (prbo objPRBo in prDataList)
        //        {
        //            if (objPRBo.PRID == null)
        //            {
        //                lblPurchase_id.Text = "1";
        //            }
        //            else
        //            {
        //                lblPurchase_id.Text = (objPRBo.PRID + 1).ToString();
        //            }
        //            ViewState["Purchase_Id"] = lblPurchase_id.Text;
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        //}

        private void LoadCurrencyTypes()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Payment_Currency();
                ddlCurrency.DataSource = objLst;
                ddlCurrency.DataTextField = "LTEXT";
                ddlCurrency.DataValueField = "WAERS";
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, new ListItem("- SEARCH -", "0"));
                ddlCurrency.SelectedValue = "0";

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }


        }

        //ADD Purchase Details
        //protected void btnPurchaseDetailsAdd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (RbtnBillable.SelectedValue.Equals("1"))
        //        {
        //            if (RbtnListProposal.SelectedValue == "0" && RbtnListAgreement.SelectedValue == "0" && RbtnListEmailCommunication.SelectedValue == "1"
        //                && RbtnListInvoice.SelectedValue == "0")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please attach atleast one document !');", true);
        //                return;
        //            }
        //        }
        //       // Purchase_ReqForm.SetActiveView(Item_Details);
        //        grd_ItemInfo.Visible = true;
        //      //  lblDisplayPurchase_id.Text = ViewState["Purchase_Id"].ToString();

        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}

        protected void txtRequester_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtRequester.Text.Contains('-'))
                {
                    ViewState["RequesterID"] = txtRequester.Text.Split('-')[1];
                    LoadMainFunction(ViewState["RequesterID"].ToString());
                    LoadSubFunction(ViewState["RequesterID"].ToString());
                    ltRequestor.Text = txtRequester.Text;
                    txtRequester.Focus();

                    if (txtProjectDeliveryHeadID.Text != "" && txtProjectDeliveryHeadID.Text != txtRequester.Text.Split('-')[1].Trim() && ddlERPProjectCode.SelectedValue.StartsWith("E/"))
                    {
                        ddlERPProjectCode.SelectedValue = "0";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('External Projects can be added only by the PM of the project.')", true);
                        ddlProjectDeliveryHeadName.Text = "";
                        txtProjectDeliveryHeadID.Text = "";
                    }
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }



        }

        private void LoadSubFunction(string Perner)
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_SubFunction(Perner);
                foreach (masterbo mbo in objLst)
                {
                    txtSubFunction.Text = mbo.BTEXT;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadMainFunction(string Perner)
        {
            try
            {
                // masterbo mbo = new masterbo();
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_MainFunction(Perner);
                foreach (masterbo mbo in objLst)
                {
                    txtMainFunction.Text = mbo.FUNC_AREA;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void ddlERPProjectCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadERPPrjMngr();
            if (ddlERPProjectCode.SelectedValue.StartsWith("E/"))
            {
                if (txtProjectDeliveryHeadID.Text == txtRequester.Text.Split('-')[1])
                {
                    ddlCategory.SelectedValue = "Project";
                    //ddlCategory.Items[0].Attributes.Add("disabled", "disabled");
                    //ddlCategory.Items[1].Attributes.Add("disabled", "disabled");
                    //ddlCategory.Items[2].Attributes.Add("disabled", "disabled");
                    //ddlCategory.Items[4].Attributes.Add("disabled", "disabled");
                    //ddlCategory.Items[5].Attributes.Add("disabled", "disabled");
                    ddlCategory.Items[4].Enabled = true;

                    ddlCategory.Items[0].Enabled = false;
                    ddlCategory.Items[1].Enabled = false;
                    ddlCategory.Items[2].Enabled = false;
                    ddlCategory.Items[3].Enabled = false;
                    ddlCategory.Items[5].Enabled = false;
                    // ddlCategory.Items[3].Attributes.Add("enabled", "enabled");

                    //  ddlCategory.Enabled = false;
                    //LoadItemDesc();
                    //ddlItemDescription.Visible = true;
                    //ddlItemDescription.Enabled = true;
                    //txtItemDesc.Visible = false;
                    ddlItemDescription.Visible = false;
                    txtItemDesc.Visible = txtItemDesc.Enabled = true;
                    LoadUnitofMeasurements();
                    RFV_txtItemDesc.Enabled = true;
                    RFV_txtItemDesc.Visible = true;
                    RFV_ddlItemDescription.Enabled = false;
                    RFV_ddlItemDescription.Visible = false;
                }
                else
                {
                    ddlERPProjectCode.SelectedValue = "0";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('External Projects can be added only by the PM of the project.')", true);
                    ddlProjectDeliveryHeadName.Text = "";
                    txtProjectDeliveryHeadID.Text = "";
                }
            }

            else if (ddlERPProjectCode.SelectedValue.StartsWith("I/") && (rbtnCapitalizedYes.Checked == true))
            {
                ddlCategory.SelectedValue = "Asset";
                //ddlCategory.Items[0].Attributes.Add("disabled", "disabled");
                //ddlCategory.Items[1].Attributes.Add("disabled", "disabled");
                //ddlCategory.Items[2].Attributes.Add("disabled", "disabled");
                //ddlCategory.Items[3].Attributes.Add("disabled", "disabled");
                //ddlCategory.Items[5].Attributes.Add("disabled", "disabled");
                ddlCategory.Items[2].Attributes.Add("enabled", "enabled");
                ddlCategory.Items[2].Enabled = true;
                ddlCategory.Items[0].Enabled = false;
                ddlCategory.Items[1].Enabled = false;
                ddlCategory.Items[4].Enabled = false;
                ddlCategory.Items[3].Enabled = false;
                ddlCategory.Items[5].Enabled = false;

                // ddlCategory.Enabled = false;
                ddlItemDescription.Visible = false;
                txtItemDesc.Visible = txtItemDesc.Enabled = true;
                LoadUnitofMeasurements();
                RFV_txtItemDesc.Enabled = true;
                RFV_txtItemDesc.Visible = true;
                RFV_ddlItemDescription.Enabled = false;
                RFV_ddlItemDescription.Visible = false;

                fuProposal = (FileUpload)Session["fuProposal"];
            }



            else if (ddlERPProjectCode.SelectedValue.StartsWith("I/") && (rbtnCapitalizedNo.Checked == true))
            {

                ddlCategory.SelectedValue = "0";
                //ddlCategory.Items[3].Attributes.Add("disabled", "disabled");
                //ddlCategory.Items[4].Attributes.Add("disabled", "disabled");
                ddlCategory.Items[0].Attributes.Add("enabled", "enabled");
                ddlCategory.Items[1].Attributes.Add("enabled", "enabled");
                ddlCategory.Items[3].Attributes.Add("enabled", "enabled");
                ddlCategory.Items[5].Attributes.Add("enabled", "enabled");
                ddlCategory.Items[0].Enabled = true;
                ddlCategory.Items[1].Enabled = true;
                ddlCategory.Items[3].Enabled = true;
                ddlCategory.Items[5].Enabled = true;
                ddlCategory.Items[2].Enabled = false;
                ddlCategory.Items[4].Enabled = false;
                //ddlCategory.Enabled = false;
                ddlItemDescription.Visible = false;
                txtItemDesc.Visible = txtItemDesc.Enabled = true;
                LoadUnitofMeasurements();
                RFV_txtItemDesc.Enabled = true;
                RFV_txtItemDesc.Visible = true;
                RFV_ddlItemDescription.Enabled = false;
                RFV_ddlItemDescription.Visible = false;

                fuProposal = (FileUpload)Session["fuProposal"];
            }
            ltProject.Text = ddlERPProjectCode.SelectedValue;
            ddlERPProjectCode.Focus();
            // ToString().StartsWith("fiad"))
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCategory.SelectedItem.Text == "Product")
                {

                    LoadItemDesc();
                    ddlItemDescription.Visible = true;
                    ddlItemDescription.Enabled = true;
                    txtItemDesc.Visible = false;

                    //ddlItemDescription.Visible = false;
                    //txtItemDesc.Visible = txtItemDesc.Enabled = true;
                    //LoadUnitofMeasurements();
                    //RFV_txtItemDesc.Enabled = true;
                    //RFV_txtItemDesc.Visible = true;
                    //RFV_ddlItemDescription.Enabled = false;
                    //RFV_ddlItemDescription.Visible = false;
                }
                else
                {

                    ddlItemDescription.Visible = false;
                    txtItemDesc.Visible = txtItemDesc.Enabled = true;
                    LoadUnitofMeasurements();
                    RFV_txtItemDesc.Enabled = true;
                    RFV_txtItemDesc.Visible = true;
                    RFV_ddlItemDescription.Enabled = false;
                    RFV_ddlItemDescription.Visible = false;

                    fuProposal = (FileUpload)Session["fuProposal"];
                }
                ddlCategory.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadUnitofMeasurements()
        {
            try
            {
                masterbo mBo = new masterbo();


                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Unit_of_Measurements();
                ddlUnitOfMeasurement.DataSource = objLst;
                ddlUnitOfMeasurement.DataTextField = "ISOTXT";
                ddlUnitOfMeasurement.DataValueField = "ISOCODE";
                ddlUnitOfMeasurement.DataBind();
                ddlUnitOfMeasurement.Items.Insert(0, new ListItem("- SEARCH -", "0"));
                ddlUnitOfMeasurement.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        protected void ddlItemDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadParticularUnitofMeasurement();
            RFV_ddlItemDescription.Enabled = true;
            RFV_ddlItemDescription.Visible = true;
            RFV_txtItemDesc.Enabled = false;
            RFV_txtItemDesc.Visible = false;
            ddlItemDescription.Focus();

        }

        protected void rbtnCapitalizedYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnCapitalizedYes.Checked)
                {
                    //txtWillthisItembeCapitalized.Visible = true;
                    //txtWillthisItembeCapitalized.Enabled = true;
                    //RFVWillthisItembeCapitalized.Enabled = true;
                    //RFVWillthisItembeCapitalized.Visible = true;


                    if (rbtnBudgetYes.Checked)
                    {
                        txtWillthisItembeCapitalized.Visible = true;
                        txtWillthisItembeCapitalized.Enabled = true;
                        RFVWillthisItembeCapitalized.Enabled = true;
                        RFVWillthisItembeCapitalized.Visible = true;
                    }

                    else
                    {
                        txtWillthisItembeCapitalized.Visible = false;
                        txtWillthisItembeCapitalized.Enabled = false;
                        RFVWillthisItembeCapitalized.Enabled = false;
                        RFVWillthisItembeCapitalized.Visible = false;
                        txtWillthisItembeCapitalized.Text = string.Empty;
                    }



                    if (ddlERPProjectCode.SelectedValue.StartsWith("I/") && (rbtnCapitalizedYes.Checked == true))
                    {
                        ddlCategory.SelectedValue = "Asset";
                        //ddlCategory.Enabled = false;
                        //ddlCategory.Items[0].Attributes.Add("disabled", "disabled");
                        //ddlCategory.Items[1].Attributes.Add("disabled", "disabled");
                        //ddlCategory.Items[2].Attributes.Add("disabled", "disabled");
                        //ddlCategory.Items[3].Attributes.Add("disabled", "disabled");
                        //ddlCategory.Items[5].Attributes.Add("disabled", "disabled");
                        ddlCategory.Items[2].Attributes.Add("enabled", "enabled");
                        ddlCategory.Items[2].Enabled = true;
                        ddlCategory.Items[0].Enabled = false;
                        ddlCategory.Items[1].Enabled = false;
                        ddlCategory.Items[3].Enabled = false;
                        ddlCategory.Items[4].Enabled = false;
                        ddlCategory.Items[5].Enabled = false;

                        ddlItemDescription.Visible = false;
                        txtItemDesc.Visible = txtItemDesc.Enabled = true;
                        LoadUnitofMeasurements();
                        RFV_txtItemDesc.Enabled = true;
                        RFV_txtItemDesc.Visible = true;
                        RFV_ddlItemDescription.Enabled = false;
                        RFV_ddlItemDescription.Visible = false;

                        fuProposal = (FileUpload)Session["fuProposal"];
                    }

                    rbtnCapitalizedYes.Focus();
                }
                else
                {
                    //txtWillthisItembeCapitalized.Visible = false;
                    //RFVWillthisItembeCapitalized.Enabled = false;
                    //RFVWillthisItembeCapitalized.Visible = false;
                    //txtWillthisItembeCapitalized.Text = string.Empty;

                    if (rbtnBudgetYes.Checked)
                    {
                        txtWillthisItembeCapitalized.Visible = true;
                        txtWillthisItembeCapitalized.Enabled = true;
                        RFVWillthisItembeCapitalized.Enabled = false;
                        RFVWillthisItembeCapitalized.Visible = false;
                    }

                    else
                    {
                        txtWillthisItembeCapitalized.Visible = false;
                        txtWillthisItembeCapitalized.Enabled = false;
                        RFVWillthisItembeCapitalized.Enabled = false;
                        RFVWillthisItembeCapitalized.Visible = false;
                        txtWillthisItembeCapitalized.Text = string.Empty;
                    }



                    if (ddlERPProjectCode.SelectedValue.StartsWith("I/") && (rbtnCapitalizedNo.Checked == true))
                    {

                        ddlCategory.SelectedValue = "0";
                        //ddlCategory.Items[3].Attributes.Add("disabled", "disabled");
                        //ddlCategory.Items[4].Attributes.Add("disabled", "disabled");
                        ddlCategory.Items[0].Attributes.Add("enabled", "enabled");
                        ddlCategory.Items[1].Attributes.Add("enabled", "enabled");
                        ddlCategory.Items[3].Attributes.Add("enabled", "enabled");
                        ddlCategory.Items[5].Attributes.Add("enabled", "enabled");
                        ddlCategory.Items[0].Enabled = true;
                        ddlCategory.Items[1].Enabled = true;
                        ddlCategory.Items[3].Enabled = true;
                        ddlCategory.Items[5].Enabled = true;


                        ddlCategory.Items[2].Enabled = false;
                        ddlCategory.Items[4].Enabled = false;

                        // ddlCategory.Enabled = false;
                        ddlItemDescription.Visible = false;
                        txtItemDesc.Visible = txtItemDesc.Enabled = true;
                        LoadUnitofMeasurements();
                        RFV_txtItemDesc.Enabled = true;
                        RFV_txtItemDesc.Visible = true;
                        RFV_ddlItemDescription.Enabled = false;
                        RFV_ddlItemDescription.Visible = false;

                        fuProposal = (FileUpload)Session["fuProposal"];
                    }
                    rbtnCapitalizedNo.Focus();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void ddlMISGroupC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMIS_GrpAB();
            ddlMISGroupC.Focus();
        }

        private void LoadMIS_GrpAB()
        {
            try
            {
                masterbo mBo = new masterbo();
                mBo.LL = ddlMISGroupC.SelectedValue.ToString();
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_MIS_AB(mBo);
                foreach (masterbo mbo in objLst)
                {
                    txtMISGroupA.Text = mbo.A_DESC;
                    txtMISGroupB.Text = mbo.B_DESC;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        protected void rbtnCapitalizedNo_CheckedChanged1(object sender, EventArgs e)
        {
            try
            {
                if (rbtnCapitalizedNo.Checked)
                {
                    //txtWillthisItembeCapitalized.Enabled = false;
                    //RFVWillthisItembeCapitalized.Enabled = false;
                    //RFVWillthisItembeCapitalized.Visible = false;
                    //txtWillthisItembeCapitalized.Text = string.Empty;

                    if (rbtnBudgetYes.Checked)
                    {
                        txtWillthisItembeCapitalized.Visible = true;
                        txtWillthisItembeCapitalized.Enabled = true;
                        RFVWillthisItembeCapitalized.Enabled = false;
                        RFVWillthisItembeCapitalized.Visible = false;
                    }

                    else
                    {
                        txtWillthisItembeCapitalized.Visible = false;
                        txtWillthisItembeCapitalized.Enabled = false;
                        RFVWillthisItembeCapitalized.Enabled = false;
                        RFVWillthisItembeCapitalized.Visible = false;
                        txtWillthisItembeCapitalized.Text = string.Empty;
                    }



                    if (ddlERPProjectCode.SelectedValue.StartsWith("I/") && (rbtnCapitalizedNo.Checked == true))
                    {

                        ddlCategory.SelectedValue = "0";
                        //ddlCategory.Items[3].Attributes.Add("disabled", "disabled");
                        //ddlCategory.Items[4].Attributes.Add("disabled", "disabled");
                        ddlCategory.Items[0].Attributes.Add("enabled", "enabled");
                        ddlCategory.Items[1].Attributes.Add("enabled", "enabled");
                        ddlCategory.Items[3].Attributes.Add("enabled", "enabled");
                        ddlCategory.Items[5].Attributes.Add("enabled", "enabled");
                        ddlCategory.Items[0].Enabled = true;
                        ddlCategory.Items[1].Enabled = true;
                        ddlCategory.Items[3].Enabled = true;
                        ddlCategory.Items[5].Enabled = true;

                        ddlCategory.Items[2].Enabled = false;
                        ddlCategory.Items[4].Enabled = false;
                        // ddlCategory.Enabled = false;
                        ddlItemDescription.Visible = false;
                        txtItemDesc.Visible = txtItemDesc.Enabled = true;
                        LoadUnitofMeasurements();
                        RFV_txtItemDesc.Enabled = true;
                        RFV_txtItemDesc.Visible = true;
                        RFV_ddlItemDescription.Enabled = false;
                        RFV_ddlItemDescription.Visible = false;

                        fuProposal = (FileUpload)Session["fuProposal"];
                    }
                    rbtnCapitalizedNo.Focus();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void RbtnBillable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RbtnBillable.SelectedValue == "1")
                {
                    PnlBillable.Enabled = true;
                }
                else
                {
                    PnlBillable.Enabled = true;
                }
                RbtnBillable.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void RbtnListProposal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (RbtnListProposal.SelectedValue)
                {
                    case "1":
                        fuProposal.Visible = fuProposal.Enabled = RFV_fuProposal.Enabled = true;
                        break;
                    default:
                        fuProposal.Visible = fuProposal.Enabled = RFV_fuProposal.Enabled = false;
                        break;
                }
                RbtnListProposal.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void RbtnListAgreement_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (RbtnListAgreement.SelectedValue)
                {
                    case "1":
                        fuAgreement.Visible = fuAgreement.Enabled = RFV_fuAgreement.Enabled = true;
                        break;
                    default:
                        fuAgreement.Visible = fuAgreement.Enabled = RFV_fuAgreement.Enabled = false;
                        break;
                }
                RbtnListAgreement.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void RbtnListEmailCommunication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (RbtnListEmailCommunication.SelectedValue)
                {
                    case "1":
                        fuEmailCommunication.Visible = fuEmailCommunication.Enabled = RFV_fuEmailCommunication.Enabled = true;
                        break;
                    default:
                        fuEmailCommunication.Visible = fuEmailCommunication.Enabled = RFV_fuEmailCommunication.Enabled = false;
                        break;
                }
                RbtnListEmailCommunication.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void RbtnListInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (RbtnListInvoice.SelectedValue)
                {
                    case "1":
                        fuInvoice.Visible = fuInvoice.Enabled = RFV_fuInvoice.Enabled = true;
                        break;
                    default:
                        fuInvoice.Visible = fuInvoice.Enabled = RFV_fuInvoice.Enabled = false;
                        break;
                }
                RbtnListInvoice.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        //protected void txtItemDesc_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("MEINS", typeof(string));
        //        dt.Rows.Add("EA");
        //        ddlUnitOfMeasurement.DataTextField = "MEINS";
        //        ddlUnitOfMeasurement.DataValueField = "MEINS";
        //        ddlUnitOfMeasurement.DataSource = dt;
        //        ddlUnitOfMeasurement.DataBind();

        //        RFV_txtItemDesc.Enabled = true;
        //        RFV_txtItemDesc.Visible = true;
        //        RFV_ddlItemDescription.Enabled = false;
        //        RFV_ddlItemDescription.Visible = false;

        //        fuProposal = (FileUpload)Session["fuProposal"];
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        //}


        protected void btnItemAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProjectDeliveryHeadID.Text != "" && txtProjectDeliveryHeadID.Text != txtRequester.Text.Split('-')[1].Trim() && ddlERPProjectCode.SelectedValue.StartsWith("E/"))
                {
                    ////ddlERPProjectCode.SelectedValue = "0";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('External Projects can be added only by the PM of the project.')", true);
                    ddlProjectDeliveryHeadName.Text = "";
                    txtProjectDeliveryHeadID.Text = "";
                }

                else
                {

                    //if (grd_ItemInfo.Rows.Count > 0)
                    //{
                    string[] PRCategoryProd = { "Product" };
                    string[] PRCategory = { "Service", "Project", "Asset", "AMC", "Others" };


                    if (ddlCategory.SelectedValue != "0" && ddlUnitOfMeasurement.SelectedValue != "" &&
                        !string.IsNullOrEmpty(txtNoOfUnits.Text.Trim()) && !string.IsNullOrEmpty(txtUnitPrice.Text.Trim()) && ddlCurrency.SelectedValue != "")
                    {

                        if (ddlCategory.SelectedItem.Text.Equals("Product") && ddlItemDescription.SelectedValue == "0")
                        { return; }

                        if (PRCategory.Contains(ddlCategory.SelectedItem.Text) && string.IsNullOrEmpty(txtItemDesc.Text.Trim()))
                        { return; }

                        if (ViewState["PR_ItemsDT"] != null)
                        {
                            using (DataTable Dt = (DataTable)ViewState["PR_ItemsDT"])
                            {
                                ////if (Dt.Rows[0]["BNFPO"].ToString().Equals(ddlCategory.SelectedValue))
                                ////{
                                Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, ddlCategory.SelectedValue, ddlItemDescription.SelectedValue
                               , txtItemDesc.Visible == true ? txtItemDesc.Text.Trim() : ddlItemDescription.SelectedItem.Text, txtPartNo.Text.Trim(), ddlCategory.SelectedValue, ddlUnitOfMeasurement.SelectedValue, txtNoOfUnits.Text.Trim()
                               , txtUnitPrice.Text.Trim(), ddlCurrency.SelectedValue, txtTaxable.Text, txtItemNote.Text.Trim());
                                grd_ItemInfo.DataSource = Dt;
                                grd_ItemInfo.DataBind();
                                ddlCurrency.SelectedValue = ddlCurrency.SelectedValue;
                                ddlCurrency.Enabled = false;


                                //----------------------------------------------------------
                                //decimal d = 0;
                                //decimal totalAmount = Dt.AsEnumerable()
                                //         .Where(r => decimal.TryParse(r.Field<string>("UNIT_PRICE"), out d))
                                //         .Sum(r => d);
                                //decimal totalUnits = Dt.AsEnumerable()
                                //         .Where(r => decimal.TryParse(r.Field<string>("NO_OF_UNITS"), out d))
                                //         .Sum(r => d);
                                //decimal total = totalAmount * totalUnits;
                                //grd_ItemInfo.FooterRow.Cells[7].Text = "Total";

                                //grd_ItemInfo.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                                //grd_ItemInfo.FooterRow.Cells[8].Text = total.ToString("N2") + "(" + (ddlCurrency.SelectedValue) + ")";

                                //----------------------------------------------------------


                                if (ddlCategory.SelectedValue == "Product")
                                {
                                    ddlItemDescription.Focus();
                                }
                                else
                                {
                                    txtItemDesc.Focus();
                                }
                                ////}
                                ////else
                                ////{
                                ////    MsgCls("Please select same indent product category !", lblIndent, Color.Red);
                                ////    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please select same indent product category !');", true);
                                ////    return;
                                ////}
                            }
                        }
                        else
                        {
                            using (DataTable Dt = GetPR_ItemsDt())
                            {

                                Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, ddlCategory.SelectedValue, ddlItemDescription.SelectedValue
                                    , txtItemDesc.Visible == true ? txtItemDesc.Text.Trim() : ddlItemDescription.SelectedItem.Text, txtPartNo.Text.Trim(), ddlCategory.SelectedValue, ddlUnitOfMeasurement.SelectedValue, txtNoOfUnits.Text.Trim()
                                    , txtUnitPrice.Text.Trim(), ddlCurrency.SelectedValue, txtTaxable.Text, txtItemNote.Text.Trim());
                                grd_ItemInfo.DataSource = Dt;
                                grd_ItemInfo.DataBind();
                                ViewState["PR_ItemsDT"] = Dt;
                                ddlCurrency.SelectedValue = ddlCurrency.SelectedValue;
                                ddlCurrency.Enabled = false;


                                //----------------------------------------------------------
                                //decimal d = 0;
                                //decimal totalAmount = Dt.AsEnumerable()
                                //         .Where(r => decimal.TryParse(r.Field<string>("UNIT_PRICE"), out d))
                                //         .Sum(r => d);
                                //decimal totalUnits = Dt.AsEnumerable()
                                //         .Where(r => decimal.TryParse(r.Field<string>("NO_OF_UNITS"), out d))
                                //         .Sum(r => d);
                                //decimal total = totalAmount * totalUnits;
                                //grd_ItemInfo.FooterRow.Cells[7].Text = "Total";

                                //grd_ItemInfo.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                                //grd_ItemInfo.FooterRow.Cells[8].Text = total.ToString("N2") + "(" + (ddlCurrency.SelectedValue) + ")";

                                //----------------------------------------------------------


                                if (ddlCategory.SelectedValue == "Product")
                                {
                                    ddlItemDescription.Focus();
                                }
                                else
                                {
                                    txtItemDesc.Focus();
                                }

                                btnSubmit.Visible = true;
                                Attachments.Visible = true;

                                //rbtnBudgetYes.Enabled = false;
                                //rbtnBudgetNo.Enabled = false;
                                rbtnCapitalizedYes.Enabled = false;
                                rbtnCapitalizedNo.Enabled = false;
                                ////txtWillthisItembeCapitalized.Enabled = false;
                                ddlERPProjectCode.Enabled = false;
                            }

                        }
                        ////ClearPr_ItemsRequest();
                        totalAmt();
                        MsgCls("PR Item added successfully !", lblIndent, Color.Green);
                        dvlineitem.Visible = true;
                        grd_ItemInfo.Columns[7].Visible = true;
                        collapse();
                        ////ddlCategory.Enabled = false;
                        //dvlialert.Visible = true;
                    }
                    else
                    {
                        //dvlialert.Visible = true;
                    }

                    //}
                    //else
                    //{
                    //    dvlialert.Visible = true;
                    //}

                }



            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void Expand()
        {
            cpe.Collapsed = false;
            cpe.ClientState = "false";
            icollapse.Attributes.Add("class", "mdi mdi-minus font-20 text-white");
        }

        public void collapse()
        {
            cpe.Collapsed = true;
            cpe.ClientState = "true";
            icollapse.Attributes.Add("class", "mdi mdi-plus font-20 text-white");

        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "btnSubmit_Click Start')", true);
            if (txtProjectDeliveryHeadID.Text != "" && txtProjectDeliveryHeadID.Text != txtRequester.Text.Split('-')[1].Trim() && ddlERPProjectCode.SelectedValue.StartsWith("E/"))
            {
                ////ddlERPProjectCode.SelectedValue = "0";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('External Projects can be added only by the PM of the project.')", true);
                ddlProjectDeliveryHeadName.Text = "";
                txtProjectDeliveryHeadID.Text = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "btnSubmit_Click ELSE Start')", true);
                SaveorSendPR("Submit");
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "btnSubmit_Click ELSE End')", true);
            }
            ////try
            ////{

            ////    //if (RbtnBillable.SelectedValue.Equals("1"))
            ////    //{ 
            ////    //if (RbtnListProposal.SelectedValue == "0" && RbtnListAgreement.SelectedValue == "0" && RbtnListEmailCommunication.SelectedValue == "0"
            ////    //    && RbtnListInvoice.SelectedValue == "0")
            ////    //{
            ////    //    MsgCls("Please attach atleast one document !", lblMessageBoard, Color.Red);
            ////    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please attach atleast one document !');", true);
            ////    //    return;
            ////    //}
            ////    if (RbtnListProposal.SelectedValue == "0" && RbtnListAgreement.SelectedValue == "0" && RbtnListEmailCommunication.SelectedValue == "0"
            ////            && RbtnListInvoice.SelectedValue == "0")
            ////    {

            ////        MsgCls("Please select yes against attached file !", lblMessageBoard, Color.Red);
            ////        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please select yes against attached file !');", true);
            ////        return;
            ////    }
            ////    else if ((RbtnListProposal.SelectedValue == "1" && fuProposal.HasFile == false)
            ////           || (RbtnListAgreement.SelectedValue == "1" && fuAgreement.HasFile == false)
            ////          || (RbtnListEmailCommunication.SelectedValue == "1" && fuEmailCommunication.HasFile == false)
            ////          || (RbtnListInvoice.SelectedValue == "1" && fuInvoice.HasFile == false))
            ////    {
            ////        MsgCls("Please attach document !", lblMessageBoard, Color.Red);
            ////        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please attach document !');", true);
            ////        return;
            ////    }

            ////    else
            ////    {
            ////        bool? Status = false;
            ////        int? PR_ID = 0;
            ////        // string ProDoc = fuProposal.HasFile ? "~/PRDoc/" + Path.GetFileName(fuProposal.PostedFile.FileName) : "";

            ////        prbo PrBO = new prbo();
            ////        PrBO.IPERNR = User.Identity.Name;
            ////        PrBO.RPERNR = ViewState["RequesterID"].ToString();
            ////        PrBO.PFUNC_AREA = txtMainFunction.Text;
            ////        PrBO.BTEXT = txtSubFunction.Text;
            ////        PrBO.MIS_GRPC = ddlMISGroupC.SelectedItem.Text;
            ////        PrBO.MIS_GRPA = txtMISGroupA.Text;
            ////        PrBO.MIS_GRPB = txtMISGroupB.Text;
            ////        PrBO.EKGRP = ddlRequesterRegion.SelectedValue;
            ////        PrBO.BWERKS = ddlBillToAddress.SelectedValue;
            ////        PrBO.SWERKS = ddlShipToAddress.SelectedValue;
            ////        PrBO.SUG_SUPP = txtSuggestedSupplier.Text;
            ////        PrBO.SUP_ADDRESS = txtSuggestedAddress.Text.Trim();
            ////        PrBO.SUP_PHONE = txtSupplierPhone.Text.Trim();
            ////        PrBO.IN_BUDGET = rbtnBudgetYes.Checked ? "YES" : "NO";
            ////        PrBO.CAPITALIZED = rbtnCapitalizedYes.Checked ? "YES" : "NO";
            ////        PrBO.CAP_TEXT = txtWillthisItembeCapitalized.Text.Trim();
            ////        PrBO.SERVICE_BUREA = rbtnServiceYes.Checked ? "YES" : "NO";
            ////        PrBO.CRITICALITY = ddlCriticality.SelectedItem.Text;
            ////        PrBO.PSPNR = ddlERPProjectCode.SelectedValue;
            ////        //PrBO.VERNR = ddlProjectDeliveryHeadName.SelectedValue;
            ////        PrBO.VERNR = txtProjectDeliveryHeadID.Text.Trim();
            ////        PrBO.BILLABLE = RbtnBillable.SelectedValue.Equals("1") ? "YES" : "NO";
            ////        PrBO.PROPOSAL = RbtnListProposal.SelectedValue.Equals("1") ? "YES" : "NO";


            ////        PrBO.PFID = fuProposal.HasFile ? Path.GetExtension(fuProposal.FileName) : "";
            ////        PrBO.PFPATH = fuProposal.HasFile ? Path.GetExtension(fuProposal.FileName) : "";



            ////        PrBO.AGREEMENT = RbtnListAgreement.SelectedValue.Equals("1") ? "YES" : "NO";
            ////        PrBO.AFID = fuAgreement.HasFile ? Path.GetExtension(fuAgreement.FileName) : "";
            ////        PrBO.AFPATH = fuAgreement.HasFile ? Path.GetExtension(fuAgreement.FileName) : "";

            ////        PrBO.EMAIL_COM = RbtnListEmailCommunication.SelectedValue.Equals("1") ? "YES" : "NO";
            ////        PrBO.EFID = fuEmailCommunication.HasFile ? Path.GetExtension(fuEmailCommunication.FileName) : "";
            ////        PrBO.EFPATH = fuEmailCommunication.HasFile ? Path.GetExtension(fuEmailCommunication.FileName) : "";



            ////        PrBO.INVOICE = RbtnListInvoice.SelectedValue.Equals("1") ? "YES" : "NO";
            ////        PrBO.IFID = fuInvoice.HasFile ? Path.GetExtension(fuInvoice.FileName) : "";
            ////        PrBO.IFPATH = fuInvoice.HasFile ? Path.GetExtension(fuInvoice.FileName) : "";

            ////        PrBO.SPART = ddlBusinessUnit.SelectedValue;

            ////        PrBO.JUSTIFICATION = txtJustification.Text;
            ////        PrBO.SPL_NOTES = txtSpecialNotes.Text;
            ////        PrBO.CREATED_ON1 = DateTime.Now;
            ////        PrBO.CREATEDBY = User.Identity.Name;
            ////        PrBO.APP_ON1 = DateTime.MinValue;
            ////        PrBO.APPROVEDBY1 = "";
            ////        PrBO.HOLD_ON1 = DateTime.Now;
            ////        PrBO.RELEASED_ON1 = DateTime.Now;
            ////        PrBO.COMMENTS1 = string.Empty;

            ////        PrBO.APP_ON2 = DateTime.MinValue;
            ////        PrBO.APPROVEDBY2 = "";
            ////        PrBO.HOLD_ON2 = DateTime.Now;
            ////        PrBO.RELEASED_ON2 = DateTime.Now;
            ////        PrBO.COMMENTS2 = string.Empty;

            ////        PrBO.APP_ON3 = DateTime.MinValue;
            ////        PrBO.APPROVEDBY3 = "";
            ////        PrBO.HOLD_ON3 = DateTime.Now;
            ////        PrBO.RELEASED_ON3 = DateTime.Now;
            ////        PrBO.COMMENTS3 = string.Empty;

            ////        PrBO.APP_ON4 = DateTime.MinValue;
            ////        PrBO.APPROVEDBY4 = "";
            ////        PrBO.HOLD_ON4 = DateTime.Now;
            ////        PrBO.RELEASED_ON4 = DateTime.Now;
            ////        PrBO.COMMENTS4 = string.Empty;

            ////        PrBO.APP_ON5 = DateTime.MinValue;
            ////        PrBO.APPROVEDBY5 = "";
            ////        PrBO.HOLD_ON5 = DateTime.Now;
            ////        PrBO.RELEASED_ON5 = DateTime.Now;
            ////        PrBO.COMMENTS5 = string.Empty;

            ////        PrBO.APP_ON6 = DateTime.MinValue;
            ////        PrBO.APPROVEDBY6 = "";
            ////        PrBO.HOLD_ON6 = DateTime.Now;
            ////        PrBO.RELEASED_ON6 = DateTime.Now;
            ////        PrBO.COMMENTS6 = string.Empty;

            ////        PrBO.STATUS = "";
            ////        PrBO.REGIONID = ddlRegion.SelectedValue.Trim();

            ////        prbl PrBl = new prbl();
            ////        if (grd_ItemInfo.Rows.Count > 0)
            ////        {
            ////            PrBl.Create_PR_Request(PrBO, ref PR_ID, ref Status);
            ////            if (Status.Equals(false))
            ////            {

            ////                fuProposal.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Proposal") + Path.GetExtension(fuProposal.FileName));
            ////                fuAgreement.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Agreement") + Path.GetExtension(fuAgreement.FileName));
            ////                fuEmailCommunication.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Email") + Path.GetExtension(fuEmailCommunication.FileName));
            ////                fuInvoice.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Invoice") + Path.GetExtension(fuInvoice.FileName));


            ////                if (ViewState["PR_ItemsDT"] != null)
            ////                {
            ////                    using (DataTable Dt = (DataTable)ViewState["PR_ItemsDT"])
            ////                    {
            ////                        for (int i = 0; i < Dt.Rows.Count; i++)
            ////                        {
            ////                            PrBO.id = Guid.Parse("00000000-0000-0000-0000-000000000000");
            ////                            PrBO.BANFN_EXT = PR_ID;//int.Parse(Dt.Rows[i]["BANFN_EXT"].ToString());
            ////                            PrBO.BNFPO = (i + 1).ToString();
            ////                            PrBO.MATNR = Dt.Rows[i]["MATNR"].ToString();
            ////                            PrBO.TXZ01 = Dt.Rows[i]["TXZ01"].ToString();
            ////                            PrBO.PART_NO = Dt.Rows[i]["PART_NO"].ToString();
            ////                            PrBO.MTART = Dt.Rows[i]["BNFPO"].ToString();
            ////                            PrBO.MEINS = Dt.Rows[i]["MEINS"].ToString();
            ////                            PrBO.NO_OF_UNITS = Dt.Rows[i]["NO_OF_UNITS"].ToString();
            ////                            PrBO.UNIT_PRICE = Dt.Rows[i]["UNIT_PRICE"].ToString();
            ////                            PrBO.WAERS = Dt.Rows[i]["WAERS"].ToString();
            ////                            PrBO.TAXABLE = Dt.Rows[i]["TAXABLE"].ToString();
            ////                            PrBO.ITEM_NOTE = Dt.Rows[i]["ITEM_NOTE"].ToString();
            ////                            PrBO.SAKNR = string.Empty;
            ////                            PrBl.Create_PR_add_items(PrBO, ref Status);
            ////                            //ClearPr_Request();
            ////                            //ClearPr_ItemsRequest();
            ////                            //ViewState["PR_ItemsDT"] = null;
            ////                            //grd_ItemInfo.DataSource = null;
            ////                            //grd_ItemInfo.DataBind();
            ////                        }
            ////                    }

            ////                }
            ////                MsgCls("PR Request created successfully !", lblMessageBoard, Color.Green);
            ////                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request created successfully !')", true);

            ////                divNewPR.Visible = false;
            ////                divSearch.Visible = true;


            ////                MsgCls("", lblIndent, Color.Transparent);
            ////                SendMailMethod(PR_ID);
            ////                ClearPr_Request();
            ////                ClearPr_ItemsRequest();
            ////                ddlCurrency.ClearSelection();
            ////                ddlCurrency.Enabled = true;
            ////                ddlCategory.Items[0].Enabled = false;
            ////                ddlCategory.Items[1].Enabled = false;
            ////                ddlCategory.Items[2].Enabled = false;
            ////                ddlCategory.Items[3].Enabled = false;
            ////                ddlCategory.Items[4].Enabled = false;
            ////                ddlCategory.Items[5].Enabled = false;
            ////                ViewState["PR_ItemsDT"] = null;
            ////                grd_ItemInfo.DataSource = null;
            ////                grd_ItemInfo.DataBind();
            ////                LoadEmpPRRequestGridView();
            ////                txtRequester.Focus();
            ////                btnSubmit.Visible = false;
            ////                Attachments.Visible = false;



            ////                //rbtnBudgetYes.Enabled = true;
            ////                //rbtnBudgetNo.Enabled = true;
            ////                rbtnCapitalizedYes.Enabled = true;
            ////                rbtnCapitalizedNo.Enabled = true;
            ////                txtWillthisItembeCapitalized.Enabled = true;
            ////                ddlERPProjectCode.Enabled = true;
            ////            }
            ////            else
            ////            {
            ////                MsgCls("Unable to create PR Request !", lblMessageBoard, Color.Red);
            ////                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to create PR Request !')", true);
            ////            }
            ////        }
            ////        else
            ////        {
            ////            MsgCls("Please add atleast 1 item before submitting the Request !", lblMessageBoard, Color.Red);
            ////            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please add atleast 1 item before submitting the Request !')", true);
            ////        }

            ////    }
            ////    //}
            ////    //else
            ////    //{
            ////    //    bool? Status = false;
            ////    //    int? PR_ID = 0;
            ////    //    // string ProDoc = fuProposal.HasFile ? "~/PRDoc/" + Path.GetFileName(fuProposal.PostedFile.FileName) : "";
            ////    //    //SendMailMethod(PR_ID, ViewState["RequesterID"].ToString(), txtMainFunction.Text, txtSubFunction.Text, ddlMISGroupC.SelectedItem.Text, txtMISGroupA.Text,
            ////    //    //    txtMISGroupB.Text, ddlRequesterRegion.SelectedValue, ddlBillToAddress.SelectedValue, ddlShipToAddress.SelectedValue, txtSuggestedSupplier.Text,
            ////    //    //   txtSuggestedAddress.Text.Trim(), txtSupplierPhone.Text.Trim(), rbtnBudgetYes.Checked ? "YES" : "NO", rbtnCapitalizedYes.Checked ? "YES" : "NO",
            ////    //    //   txtWillthisItembeCapitalized.Text.Trim(), rbtnServiceYes.Checked ? "YES" : "NO", ddlCriticality.SelectedItem.Text, ddlERPProjectCode.SelectedValue,
            ////    //    //   );
            ////    //    prbo PrBO = new prbo();
            ////    //    PrBO.IPERNR = User.Identity.Name;
            ////    //    PrBO.RPERNR = ViewState["RequesterID"].ToString();
            ////    //    PrBO.PFUNC_AREA = txtMainFunction.Text;
            ////    //    PrBO.BTEXT = txtSubFunction.Text;
            ////    //    PrBO.MIS_GRPC = ddlMISGroupC.SelectedItem.Text;
            ////    //    PrBO.MIS_GRPA = txtMISGroupA.Text;
            ////    //    PrBO.MIS_GRPB = txtMISGroupB.Text;
            ////    //    PrBO.EKGRP = ddlRequesterRegion.SelectedValue;
            ////    //    PrBO.BWERKS = ddlBillToAddress.SelectedValue;
            ////    //    PrBO.SWERKS = ddlShipToAddress.SelectedValue;
            ////    //    PrBO.SUG_SUPP = txtSuggestedSupplier.Text;
            ////    //    PrBO.SUP_ADDRESS = txtSuggestedAddress.Text.Trim();
            ////    //    PrBO.SUP_PHONE = txtSupplierPhone.Text.Trim();
            ////    //    PrBO.IN_BUDGET = rbtnBudgetYes.Checked ? "YES" : "NO";
            ////    //    PrBO.CAPITALIZED = rbtnCapitalizedYes.Checked ? "YES" : "NO";
            ////    //    PrBO.CAP_TEXT = txtWillthisItembeCapitalized.Text.Trim();
            ////    //    PrBO.SERVICE_BUREA = rbtnServiceYes.Checked ? "YES" : "NO";
            ////    //    PrBO.CRITICALITY = ddlCriticality.SelectedItem.Text;
            ////    //    PrBO.PSPNR = ddlERPProjectCode.SelectedValue;
            ////    //    PrBO.VERNR = ddlProjectDeliveryHeadName.SelectedValue;
            ////    //    PrBO.BILLABLE = RbtnBillable.SelectedValue.Equals("1") ? "YES" : "NO";
            ////    //    PrBO.PROPOSAL = RbtnListProposal.SelectedValue.Equals("1") ? "YES" : "NO";
            ////    //    PrBO.PFID = fuProposal.HasFile ? fuProposal.PostedFile.FileName : "";
            ////    //    PrBO.PFPATH = fuProposal.HasFile ? "~/PRDoc/" + Path.GetFileName(fuProposal.PostedFile.FileName) : "";




            ////    //    PrBO.AGREEMENT = RbtnListAgreement.SelectedValue.Equals("1") ? "YES" : "NO";
            ////    //    PrBO.AFID = fuAgreement.HasFile ? fuAgreement.PostedFile.FileName : "";
            ////    //    PrBO.AFPATH = fuAgreement.HasFile ? "~/PRDoc/" + Path.GetFileName(fuAgreement.PostedFile.FileName) : "";

            ////    //    PrBO.EMAIL_COM = RbtnListEmailCommunication.SelectedValue.Equals("1") ? "YES" : "NO";
            ////    //    PrBO.EFID = fuEmailCommunication.HasFile ? fuEmailCommunication.PostedFile.FileName : "";
            ////    //    PrBO.EFPATH = fuEmailCommunication.HasFile ? "~/PRDoc/" + Path.GetFileName(fuEmailCommunication.PostedFile.FileName) : "";



            ////    //    PrBO.INVOICE = RbtnListInvoice.SelectedValue.Equals("1") ? "YES" : "NO";
            ////    //    PrBO.IFID = fuInvoice.HasFile ? fuInvoice.PostedFile.FileName : "";
            ////    //    PrBO.IFPATH = fuInvoice.HasFile ? "~/PRDoc/" + Path.GetFileName(fuInvoice.PostedFile.FileName) : "";

            ////    //    PrBO.SPART = ddlBusinessUnit.SelectedValue;

            ////    //    PrBO.JUSTIFICATION = txtJustification.Text;
            ////    //    PrBO.SPL_NOTES = txtSpecialNotes.Text;
            ////    //    PrBO.CREATED_ON1 = DateTime.Now;
            ////    //    PrBO.CREATEDBY = User.Identity.Name;
            ////    //    PrBO.APP_ON1 = DateTime.MinValue;
            ////    //    PrBO.APPROVEDBY1 = "";
            ////    //    PrBO.HOLD_ON1 = DateTime.Now;
            ////    //    PrBO.RELEASED_ON1 = DateTime.Now;
            ////    //    PrBO.COMMENTS1 = string.Empty;

            ////    //    PrBO.APP_ON2 = DateTime.MinValue;
            ////    //    PrBO.APPROVEDBY2 = "";
            ////    //    PrBO.HOLD_ON2 = DateTime.Now;
            ////    //    PrBO.RELEASED_ON2 = DateTime.Now;
            ////    //    PrBO.COMMENTS2 = string.Empty;

            ////    //    PrBO.APP_ON3 = DateTime.MinValue;
            ////    //    PrBO.APPROVEDBY3 = "";
            ////    //    PrBO.HOLD_ON3 = DateTime.Now;
            ////    //    PrBO.RELEASED_ON3 = DateTime.Now;
            ////    //    PrBO.COMMENTS3 = string.Empty;

            ////    //    PrBO.APP_ON4 = DateTime.MinValue;
            ////    //    PrBO.APPROVEDBY4 = "";
            ////    //    PrBO.HOLD_ON4 = DateTime.Now;
            ////    //    PrBO.RELEASED_ON4 = DateTime.Now;
            ////    //    PrBO.COMMENTS4 = string.Empty;

            ////    //    PrBO.APP_ON5 = DateTime.MinValue;
            ////    //    PrBO.APPROVEDBY5 = "";
            ////    //    PrBO.HOLD_ON5 = DateTime.Now;
            ////    //    PrBO.RELEASED_ON5 = DateTime.Now;
            ////    //    PrBO.COMMENTS5 = string.Empty;

            ////    //    PrBO.APP_ON6 = DateTime.MinValue;
            ////    //    PrBO.APPROVEDBY6 = "";
            ////    //    PrBO.HOLD_ON6 = DateTime.Now;
            ////    //    PrBO.RELEASED_ON6 = DateTime.Now;
            ////    //    PrBO.COMMENTS6 = string.Empty;

            ////    //    PrBO.STATUS = "";


            ////    //    prbl PrBl = new prbl();
            ////    //    if (grd_ItemInfo.Rows.Count > 0)
            ////    //    {
            ////    //        PrBl.Create_PR_Request(PrBO, ref PR_ID, ref Status);
            ////    //        if (Status.Equals(false))
            ////    //        {
            ////    //            if (ViewState["PR_ItemsDT"] != null)
            ////    //            {
            ////    //                using (DataTable Dt = (DataTable)ViewState["PR_ItemsDT"])
            ////    //                {
            ////    //                    for (int i = 0; i < Dt.Rows.Count; i++)
            ////    //                    {
            ////    //                        PrBO.id = Guid.Parse("00000000-0000-0000-0000-000000000000");
            ////    //                        PrBO.BANFN_EXT = PR_ID;//int.Parse(Dt.Rows[i]["BANFN_EXT"].ToString());
            ////    //                        PrBO.BNFPO = (i + 1).ToString();
            ////    //                        PrBO.MATNR = Dt.Rows[i]["MATNR"].ToString();
            ////    //                        PrBO.TXZ01 = Dt.Rows[i]["TXZ01"].ToString();
            ////    //                        PrBO.PART_NO = Dt.Rows[i]["PART_NO"].ToString();
            ////    //                        PrBO.MTART = Dt.Rows[i]["BNFPO"].ToString();
            ////    //                        PrBO.MEINS = Dt.Rows[i]["MEINS"].ToString();
            ////    //                        PrBO.NO_OF_UNITS = Dt.Rows[i]["NO_OF_UNITS"].ToString();
            ////    //                        PrBO.UNIT_PRICE = Dt.Rows[i]["UNIT_PRICE"].ToString();
            ////    //                        PrBO.WAERS = Dt.Rows[i]["WAERS"].ToString();
            ////    //                        PrBO.TAXABLE = Dt.Rows[i]["TAXABLE"].ToString();
            ////    //                        PrBO.ITEM_NOTE = Dt.Rows[i]["ITEM_NOTE"].ToString();

            ////    //                        PrBl.Create_PR_add_items(PrBO, ref Status);
            ////    //                        //ClearPr_Request();
            ////    //                        //ClearPr_ItemsRequest();
            ////    //                        //ViewState["PR_ItemsDT"] = null;
            ////    //                        //grd_ItemInfo.DataSource = null;
            ////    //                        //grd_ItemInfo.DataBind();
            ////    //                    }
            ////    //                }
            ////    //            }
            ////    //            MsgCls("PR Request created successfully !", lblMessageBoard, Color.Green);
            ////    //            MsgCls("", lblIndent, Color.Transparent);
            ////    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request created successfully !')", true);
            ////    //            SendMailMethod(PR_ID);
            ////    //            ClearPr_Request();
            ////    //            ClearPr_ItemsRequest();
            ////    //            ViewState["PR_ItemsDT"] = null;
            ////    //            grd_ItemInfo.DataSource = null;
            ////    //            grd_ItemInfo.DataBind();
            ////    //        }
            ////    //        else
            ////    //        {
            ////    //            MsgCls("Unable to create PR Request !", lblMessageBoard, Color.Red);
            ////    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to create PR Request !')", true);
            ////    //        }
            ////    //    }
            ////    //    else
            ////    //    {
            ////    //        MsgCls("Please add atleast 1 Material items before submitting PR Request !", lblMessageBoard, Color.Red);
            ////    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please add atleast 1 Material items before submitting PR Request !')", true);
            ////    //    }
            ////    //}
            ////}
            //////catch (Exception Ex)
            //////{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
            ////catch (Exception Ex)
            ////{

            ////    switch (Ex.Message)
            ////    {


            ////        case "-05":
            ////            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
            ////            MsgCls("Approvals are missing", lblMessageBoard, Color.Red);
            ////            //PageLoadEvents();
            ////            break;
            ////        default:
            ////            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
            ////            break;
            ////    }
            ////    //MsgCls(Ex.Message, LblMsg, Color.Red);
            ////}
        }

        protected void SaveorSendPR(string btnStatus)
        {
            try
            {
                string alert;
                if (RbtnListProposal.SelectedValue == "0" && RbtnListAgreement.SelectedValue == "0" && RbtnListEmailCommunication.SelectedValue == "0"
                        && RbtnListInvoice.SelectedValue == "0" && btnStatus == "Submit")
                {

                    MsgCls("Please select yes against attached file !", lblMessageBoard, Color.Red);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please select yes against attached file !');", true);
                    return;
                }
                else if (((RbtnListProposal.SelectedValue == "1" && fuProposal.HasFile == false && LbtnProposal.Text == "")
                       || (RbtnListAgreement.SelectedValue == "1" && fuAgreement.HasFile == false && LbtnAgreement.Text == "")
                      || (RbtnListEmailCommunication.SelectedValue == "1" && fuEmailCommunication.HasFile == false && LbtnEmailCommunication.Text == "")
                      || (RbtnListInvoice.SelectedValue == "1" && fuInvoice.HasFile == false && LbtnInvoice.Text == "")) && btnStatus == "Submit")
                ////else if (((RbtnListProposal.SelectedValue == "1" && fuProposal.HasFile == false)
                ////      || (RbtnListAgreement.SelectedValue == "1" && fuAgreement.HasFile == false)
                ////     || (RbtnListEmailCommunication.SelectedValue == "1" && fuEmailCommunication.HasFile == false)
                ////     || (RbtnListInvoice.SelectedValue == "1" && fuInvoice.HasFile == false)) && btnStatus == "Submit")
                {
                    MsgCls("Please attach document !", lblMessageBoard, Color.Red);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please attach document !');", true);
                    return;
                }

                //My.Computer.FileSystem.CopyFile("C:\TestFolder\test.txt","C:\TestFolder\test2.txt", false);

                else
                {
                    bool? Status = false;
                    int? PR_ID = 0;
                    // string ProDoc = fuProposal.HasFile ? "~/PRDoc/" + Path.GetFileName(fuProposal.PostedFile.FileName) : "";

                    prbo PrBO = new prbo();
                    PrBO.IPERNR = User.Identity.Name;
                    PrBO.RPERNR = ViewState["RequesterID"].ToString();
                    PrBO.PFUNC_AREA = txtMainFunction.Text;
                    PrBO.BTEXT = txtSubFunction.Text;
                    PrBO.MIS_GRPC = ddlMISGroupC.SelectedItem.Text;
                    PrBO.MIS_GRPA = txtMISGroupA.Text;
                    PrBO.MIS_GRPB = txtMISGroupB.Text;
                    PrBO.EKGRP = ddlRequesterRegion.SelectedValue;
                    PrBO.BWERKS = ddlBillToAddress.SelectedValue;
                    PrBO.SWERKS = ddlShipToAddress.SelectedValue;
                    PrBO.SUG_SUPP = txtSuggestedSupplier.Text;
                    PrBO.SUP_ADDRESS = txtSuggestedAddress.Text.Trim();
                    PrBO.SUP_PHONE = txtSupplierPhone.Text.Trim();
                    PrBO.IN_BUDGET = rbtnBudgetYes.Checked ? "YES" : "NO";
                    PrBO.CAPITALIZED = rbtnCapitalizedYes.Checked ? "YES" : "NO";
                    PrBO.CAP_TEXT = txtWillthisItembeCapitalized.Text.Trim();
                    PrBO.SERVICE_BUREA = rbtnServiceYes.Checked ? "YES" : "NO";
                    PrBO.CRITICALITY = ddlCriticality.SelectedItem.Text;
                    PrBO.PSPNR = ddlERPProjectCode.SelectedValue;
                    //PrBO.VERNR = ddlProjectDeliveryHeadName.SelectedValue;
                    PrBO.VERNR = txtProjectDeliveryHeadID.Text.Trim();
                    PrBO.BILLABLE = RbtnBillable.SelectedValue.Equals("1") ? "YES" : "NO";
                    PrBO.PROPOSAL = RbtnListProposal.SelectedValue.Equals("1") ? "YES" : "NO";


                    PrBO.PFID = fuProposal.HasFile ? Path.GetExtension(fuProposal.FileName) : "";
                    PrBO.PFPATH = fuProposal.HasFile ? Path.GetExtension(fuProposal.FileName) : "";



                    PrBO.AGREEMENT = RbtnListAgreement.SelectedValue.Equals("1") ? "YES" : "NO";
                    PrBO.AFID = fuAgreement.HasFile ? Path.GetExtension(fuAgreement.FileName) : "";
                    PrBO.AFPATH = fuAgreement.HasFile ? Path.GetExtension(fuAgreement.FileName) : "";

                    PrBO.EMAIL_COM = RbtnListEmailCommunication.SelectedValue.Equals("1") ? "YES" : "NO";
                    PrBO.EFID = fuEmailCommunication.HasFile ? Path.GetExtension(fuEmailCommunication.FileName) : "";
                    PrBO.EFPATH = fuEmailCommunication.HasFile ? Path.GetExtension(fuEmailCommunication.FileName) : "";



                    PrBO.INVOICE = RbtnListInvoice.SelectedValue.Equals("1") ? "YES" : "NO";
                    PrBO.IFID = fuInvoice.HasFile ? Path.GetExtension(fuInvoice.FileName) : "";
                    PrBO.IFPATH = fuInvoice.HasFile ? Path.GetExtension(fuInvoice.FileName) : "";

                    PrBO.SPART = ddlBusinessUnit.SelectedValue;

                    PrBO.JUSTIFICATION = txtJustification.Text;
                    PrBO.SPL_NOTES = txtSpecialNotes.Text;
                    PrBO.CREATED_ON1 = DateTime.Now;
                    PrBO.CREATEDBY = User.Identity.Name;
                    PrBO.APP_ON1 = DateTime.MinValue;
                    PrBO.APPROVEDBY1 = "";
                    PrBO.HOLD_ON1 = DateTime.Now;
                    PrBO.RELEASED_ON1 = DateTime.Now;
                    PrBO.COMMENTS1 = string.Empty;

                    PrBO.APP_ON2 = DateTime.MinValue;
                    PrBO.APPROVEDBY2 = "";
                    PrBO.HOLD_ON2 = DateTime.Now;
                    PrBO.RELEASED_ON2 = DateTime.Now;
                    PrBO.COMMENTS2 = string.Empty;

                    PrBO.APP_ON3 = DateTime.MinValue;
                    PrBO.APPROVEDBY3 = "";
                    PrBO.HOLD_ON3 = DateTime.Now;
                    PrBO.RELEASED_ON3 = DateTime.Now;
                    PrBO.COMMENTS3 = string.Empty;

                    PrBO.APP_ON4 = DateTime.MinValue;
                    PrBO.APPROVEDBY4 = "";
                    PrBO.HOLD_ON4 = DateTime.Now;
                    PrBO.RELEASED_ON4 = DateTime.Now;
                    PrBO.COMMENTS4 = string.Empty;

                    PrBO.APP_ON5 = DateTime.MinValue;
                    PrBO.APPROVEDBY5 = "";
                    PrBO.HOLD_ON5 = DateTime.Now;
                    PrBO.RELEASED_ON5 = DateTime.Now;
                    PrBO.COMMENTS5 = string.Empty;

                    PrBO.APP_ON6 = DateTime.MinValue;
                    PrBO.APPROVEDBY6 = "";
                    PrBO.HOLD_ON6 = DateTime.Now;
                    PrBO.RELEASED_ON6 = DateTime.Now;
                    PrBO.COMMENTS6 = string.Empty;

                    PrBO.STATUS = Session["PRID"] == null ? btnStatus : Session["PRID"] + "-" + btnStatus;
                    PrBO.REGIONID = ddlRegion.SelectedValue.Trim();

                    prbl PrBl = new prbl();
                    if (grd_ItemInfo.Rows.Count > 0)
                    {
                        PrBl.Create_PR_Request(PrBO, ref PR_ID, ref Status);
                        ltPRnum.Text = PR_ID.ToString();
                        if (Status.Equals(false))
                        {

                            fuProposal.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Proposal") + Path.GetExtension(fuProposal.FileName));
                            fuAgreement.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Agreement") + Path.GetExtension(fuAgreement.FileName));
                            fuEmailCommunication.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Email") + Path.GetExtension(fuEmailCommunication.FileName));
                            fuInvoice.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Invoice") + Path.GetExtension(fuInvoice.FileName));


                            if (ViewState["PR_ItemsDT"] != null)
                            {
                                using (DataTable Dt = (DataTable)ViewState["PR_ItemsDT"])
                                {
                                    for (int i = 0; i < Dt.Rows.Count; i++)
                                    {
                                        PrBO.id = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                        PrBO.BANFN_EXT = PR_ID;//int.Parse(Dt.Rows[i]["BANFN_EXT"].ToString());
                                        PrBO.BNFPO = (i + 1).ToString();
                                        PrBO.MATNR = Dt.Rows[i]["MATNR"].ToString();
                                        PrBO.TXZ01 = Dt.Rows[i]["TXZ01"].ToString();
                                        PrBO.PART_NO = Dt.Rows[i]["PART_NO"].ToString();

                                        ////PrBO.MTART = Dt.Rows[i]["BNFPO"].ToString();
                                        PrBO.MTART = Dt.Rows[i]["MTART"].ToString();

                                        PrBO.MEINS = Dt.Rows[i]["MEINS"].ToString();
                                        PrBO.NO_OF_UNITS = Dt.Rows[i]["NO_OF_UNITS"].ToString();
                                        PrBO.UNIT_PRICE = Dt.Rows[i]["UNIT_PRICE"].ToString();
                                        PrBO.WAERS = Dt.Rows[i]["WAERS"].ToString();
                                        PrBO.TAXABLE = Dt.Rows[i]["TAXABLE"].ToString();
                                        PrBO.ITEM_NOTE = Dt.Rows[i]["ITEM_NOTE"].ToString();
                                        PrBO.SAKNR = string.Empty;
                                        PrBl.Create_PR_add_items(PrBO, btnStatus, ref Status);
                                    }
                                }

                            }

                            if (btnStatus == "Submit")
                            {
                                MsgCls("PR Request Submitted successfully !", lblMessageBoard, Color.Green);
                                lblIndent.Text = "";
                                dvlineitem.Visible = false;
                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Submitted successfully !')", true);
                                alert = "PR Request number " + PR_ID + " Submitted successfully !";
                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='Purchase_Requisitions.aspx';", true);

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + alert + "');window.location ='../../UI/PR/Purchase_Requisitions.aspx';", true);
                                SendMailMethod(PR_ID);
                            }
                            else
                            {
                                MsgCls("PR Request Saved successfully !", lblMessageBoard, Color.Green);
                                lblIndent.Text = "";
                                dvlineitem.Visible = true;
                                lbtnEdit.Visible = true;
                                lbtAddNew.Visible = true;
                                collapse();
                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Saved successfully !')", true);
                                alert = "PR Request number " + PR_ID + "  Saved successfully !";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='Purchase_Requisitions.aspx';", true);
                                //SendMailMethod(PR_ID);
                                goto Skip;
                            }

                            ////divNewPR.Visible = false;
                            ////divSearch.Visible = true;


                            MsgCls("", lblIndent, Color.Transparent);
                            ////SendMailMethod(PR_ID);

                            ClearPr_Request();
                            ClearPr_ItemsRequest();
                            ddlCurrency.ClearSelection();
                            ddlCurrency.Enabled = true;
                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[3].Enabled = false;
                            ddlCategory.Items[4].Enabled = false;
                            ddlCategory.Items[5].Enabled = false;
                            ViewState["PR_ItemsDT"] = null;
                            grd_ItemInfo.DataSource = null;
                            grd_ItemInfo.DataBind();
                            LoadEmpPRRequestGridView();
                            txtRequester.Focus();
                            ////btnSubmit.Visible = false;
                            Attachments.Visible = false;


                            rbtnCapitalizedYes.Enabled = true;
                            rbtnCapitalizedNo.Enabled = true;
                            txtWillthisItembeCapitalized.Enabled = true;
                            ddlERPProjectCode.Enabled = true;

                        Skip: { }
                        }
                        else if (Status.Equals(true))
                        {

                            fuProposal.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Proposal") + Path.GetExtension(fuProposal.FileName));
                            fuAgreement.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Agreement") + Path.GetExtension(fuAgreement.FileName));
                            fuEmailCommunication.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Email") + Path.GetExtension(fuEmailCommunication.FileName));
                            fuInvoice.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Invoice") + Path.GetExtension(fuInvoice.FileName));


                            if (ViewState["PR_ItemsDT"] != null)
                            {
                                using (DataTable Dt = (DataTable)ViewState["PR_ItemsDT"])
                                {
                                    for (int i = 0; i < Dt.Rows.Count; i++)
                                    {
                                        PrBO.id = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                        PrBO.BANFN_EXT = PR_ID;//int.Parse(Dt.Rows[i]["BANFN_EXT"].ToString());
                                        PrBO.BNFPO = (i + 1).ToString();
                                        PrBO.MATNR = Dt.Rows[i]["MATNR"].ToString();
                                        PrBO.TXZ01 = Dt.Rows[i]["TXZ01"].ToString();
                                        PrBO.PART_NO = Dt.Rows[i]["PART_NO"].ToString();
                                        PrBO.MTART = Dt.Rows[i]["BNFPO"].ToString();
                                        PrBO.MEINS = Dt.Rows[i]["MEINS"].ToString();
                                        PrBO.NO_OF_UNITS = Dt.Rows[i]["NO_OF_UNITS"].ToString();
                                        PrBO.UNIT_PRICE = Dt.Rows[i]["UNIT_PRICE"].ToString();
                                        PrBO.WAERS = Dt.Rows[i]["WAERS"].ToString();
                                        PrBO.TAXABLE = Dt.Rows[i]["TAXABLE"].ToString();
                                        PrBO.ITEM_NOTE = Dt.Rows[i]["ITEM_NOTE"].ToString();
                                        PrBO.SAKNR = string.Empty;
                                        PrBl.Create_PR_add_items(PrBO, btnStatus, ref Status);
                                    }
                                }

                            }


                            if (btnStatus == "Submit")
                            {
                                MsgCls("PR Request Submitted successfully !", lblMessageBoard, Color.Green);
                                dvlineitem.Visible = false;
                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Submitted successfully !')", true);
                                alert = "PR Request number " + PR_ID + " Submitted successfully !";
                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='Purchase_Requisitions.aspx';", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + alert + "');window.location ='../../UI/PR/Purchase_Requisitions.aspx';", true);
                                SendMailMethod(PR_ID);
                            }
                            else
                            {
                                MsgCls("PR Request Saved successfully !", lblMessageBoard, Color.Green);
                                dvlineitem.Visible = true;
                                lbtnEdit.Visible = true;
                                lbtAddNew.Visible = true;
                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Saved successfully !')", true);
                                alert = "PR Request number " + PR_ID + "  Saved successfully !";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='Purchase_Requisitions.aspx';", true);
                                //SendMailMethod(PR_ID);
                                goto Skip;
                            }
                            ////MsgCls("PR Request updated successfully !", lblMessageBoard, Color.Green);
                            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request updated successfully !')", true);

                            ////divNewPR.Visible = false;
                            ////divSearch.Visible = true;


                            MsgCls("", lblIndent, Color.Transparent);

                            ////SendMailMethod(PR_ID);
                            ClearPr_Request();
                            ClearPr_ItemsRequest();
                            ddlCurrency.ClearSelection();
                            ddlCurrency.Enabled = true;
                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[3].Enabled = false;
                            ddlCategory.Items[4].Enabled = false;
                            ddlCategory.Items[5].Enabled = false;
                            ViewState["PR_ItemsDT"] = null;
                            grd_ItemInfo.DataSource = null;
                            grd_ItemInfo.DataBind();
                            LoadEmpPRRequestGridView();
                            txtRequester.Focus();
                            ////btnSubmit.Visible = false;
                            Attachments.Visible = false;


                            rbtnCapitalizedYes.Enabled = true;
                            rbtnCapitalizedNo.Enabled = true;
                            txtWillthisItembeCapitalized.Enabled = true;
                            ddlERPProjectCode.Enabled = true;


                        Skip: { }
                        }
                        else
                        {
                            MsgCls("Unable to create PR Request !", lblMessageBoard, Color.Red);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to create PR Request !')", true);
                        }
                        totalAmt();
                    }
                    else
                    {
                        MsgCls("Please add atleast 1 item before submitting the Request !", lblMessageBoard, Color.Red);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please add atleast 1 item before submitting the Request !')", true);
                    }

                }

            }

            catch (Exception Ex)
            {

                switch (Ex.Message)
                {


                    case "-05":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        MsgCls("Approvals are missing", lblMessageBoard, Color.Red);
                        //PageLoadEvents();
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        break;
                }
                //MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }

        private void SendMailMethod(int? PR_ID)
        {
            try
            {

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                grd_ItemInfo.Columns[7].Visible = false;
                grd_ItemInfo.RenderControl(hw1);
                grd_ItemInfo.Columns[7].Visible = true;
                //  grd_ItemInfo.RenderControl(hw1);

                // string strSubject = "PR RRequisition by " + Session["EmployeeName"];
                string strSubject = string.Empty;
                //    "PR RRequisition by " + user;

                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVED_BY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";

                string IEMP_Name = "";
                string IEMP_Email = "";
                string REMP_Name = "";
                string REMP_Email = "";
                string total = "";
                string createdon = "";
                string currency = "";

                prdbmlDataContext objcontext = new prdbmlDataContext();

                objcontext.sp_Get_MailList_PR(PR_ID, User.Identity.Name, ViewState["RequesterID"].ToString(), ref APPROVED_BY1, ref Approver_Name,
                    ref Approver_Email, ref IEMP_Name, ref IEMP_Email, ref REMP_Name, ref REMP_Email, ref total, ref createdon, ref currency);

                if (IEMP_Email == REMP_Email)
                {
                    strSubject = "PR Requisition " + PR_ID + " has been Raised by " + REMP_Name;

                    RecipientsString = Approver_Email;
                    strPernr_Mail = REMP_Email;

                    //    //Preparing the mail body--------------------------------------------------

                    string body = "<b>PR Requisition " + PR_ID + "  has been Raised by " + REMP_Name + "  |  " + User.Identity.Name + "<br/><br/></b>";
                    body += "<b>PR Requisition Details :<hr /><br/>";
                    body += "<table><tr><td>PR No </td> <td>: </td> <td>" + PR_ID + "</td></tr>";
                    body += "<tr><td>Requestor </td><td> : </td><td> " + User.Identity.Name + " " + REMP_Name + "</td></tr>";
                    body += "<tr><td>Main Function </td><td> : </td><td> " + txtMainFunction.Text + "</td></tr>";
                    body += "<tr><td>Sub Function </td><td> : </td><td> " + txtSubFunction.Text + "</td></tr>";
                    body += "<tr><td>MIS Group C </td><td> : </td><td> " + ddlMISGroupC.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>MIS Group A </td><td> : </td><td> " + txtMISGroupA.Text + "</td></tr>";
                    body += "<tr><td>MIS Group B </td><td> : </td><td> " + txtMISGroupB.Text + "</td></tr>";
                    body += "<tr><td>Requester Region </td><td> : </td><td> " + ddlRequesterRegion.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Bill To Address  </td><td> : </td><td> " + ddlBillToAddress.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Ship To Address  </td><td> : </td><td>  " + ddlShipToAddress.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Suggested Supplier  </td><td> : </td><td> " + txtSuggestedSupplier.Text + "</td></tr>";
                    body += "<tr><td>Suggested Address</td><td> : </td><td> " + txtSuggestedAddress.Text.Trim() + "</td></tr>";
                    body += "<tr><td>Supplier Phone </td><td> : </td><td> " + txtSupplierPhone.Text.Trim() + "</td></tr>";
                    body += "<tr><td>Budget </td><td> : </td><td>" + (rbtnBudgetYes.Checked ? "YES" : "NO").ToString() + "</td></tr>";
                    body += "<tr><td>Will this Item be Capitalized</td><td>:</td><td> " + (rbtnCapitalizedYes.Checked ? "YES" : "NO").ToString() + "</td></tr>";
                    //body += "<tr><td>WillthisItembeCapitalized </td><td> : </td><td> " + txtWillthisItembeCapitalized.Text.Trim() + "</td></tr>";
                    body += "<tr><td>Service Bureau  </td><td> : </td><td>" + (rbtnServiceYes.Checked ? "YES" : "NO").ToString() + "</td></tr>";
                    body += "<tr><td>Criticality </td><td> : </td><td> " + ddlCriticality.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>ERP Project Code</td><td> : </td><td> " + ddlERPProjectCode.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Project Delivery Head</td><td> : </td><td> " + ddlProjectDeliveryHeadName.Text + "</td></tr>";
                    body += "<tr><td>Billable </td><td> : </td><td>  " + (RbtnBillable.SelectedValue.Equals("1") ? "YES" : "NO").ToString() + "</td></tr>";
                    body += "<tr><td>File Proposal </td><td> : </td><td> " + (fuProposal.HasFile ? fuProposal.PostedFile.FileName : "NA").ToString() + "</td></tr>";
                    body += "<tr><td>File Agreement </td><td> : </td><td> " + (fuAgreement.HasFile ? fuAgreement.PostedFile.FileName : "NA").ToString() + "</td></tr>";
                    body += "<tr><td>File Email Communication</td><td> : </td><td> " + (fuEmailCommunication.HasFile ? fuEmailCommunication.PostedFile.FileName : "NA").ToString() + "</td></tr>";
                    body += "<tr><td>File Invoice </td><td> : </td><td> " + (fuInvoice.HasFile ? fuInvoice.PostedFile.FileName : "NA").ToString() + "</td></tr>";
                    body += "<tr><td>Business Unit </td><td> : </td><td> " + ddlBusinessUnit.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Region </td><td> : </td><td>  " + ddlRegion.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Justification </td><td> : </td><td>  " + txtJustification.Text + "</td></tr>";
                    body += "<tr><td>Special Notes </td><td> : </td><td> " + txtSpecialNotes.Text + "</td></tr>";
                    body += "<tr><td>Total Amount </td><td> : </td><td> " + decimal.Parse(total).ToString("#,##0.00") + " ( " + currency + " )</td></tr>";
                    body += "<tr><td>Created On </td><td> : </td><td> " + createdon + "</td></tr></table>";
                    body += "</br>" + sw1.ToString() + "<br/>";
                    //    //End of preparing the mail body-------------------------------------------
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    //lblMessageBoard.Text = "Mail sent successfully.";
                }
                else
                {

                    strSubject = "PR Requisition " + PR_ID + "  has been Raised by Indentor " + IEMP_Name + " for the Requestor " + REMP_Name;

                    RecipientsString = Approver_Email;
                    strPernr_Mail = REMP_Email + "," + IEMP_Email;

                    //    //Preparing the mail body--------------------------------------------------

                    string body = "<b>PR Requisition " + PR_ID + "  has been Raised by Indentor " + IEMP_Name + "  |  " + User.Identity.Name + " for the Requestor " + REMP_Name + "  |  " + ViewState["RequesterID"].ToString() + "<br/><br/></b>";
                    body += "<b>PR Requisition Details :<hr /><br/>";
                    body += "<table><tr><td>PR No</td> <td>: </td> <td>" + PR_ID + "</td></tr>";
                    body += "<tr><td>Requestor </td><td>:</td><td>  " + ViewState["RequesterID"].ToString() + " " + REMP_Name + "</td></tr>";
                    body += "<tr><td>Main Function</td><td>:</td><td>  " + txtMainFunction.Text + "</td></tr>";
                    body += "<tr><td>Sub Function </td><td>:</td><td>  " + txtSubFunction.Text + "</td></tr>";
                    body += "<tr><td>MIS GroupC   </td><td>:</td><td>  " + ddlMISGroupC.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>MIS GroupA   </td><td>:</td><td> " + txtMISGroupA.Text + "</td></tr>";
                    body += "<tr><td>MIS GroupB   </td><td>:</td><td>  " + txtMISGroupB.Text + "</td></tr>";
                    body += "<tr><td>Requester Region </td><td>:</td><td>  " + ddlRequesterRegion.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Bill To Address   </td><td>:</td><td>  " + ddlBillToAddress.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Ship To Address  </td><td>:</td><td>  " + ddlShipToAddress.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Suggested Supplier </td><td>:</td><td> " + txtSuggestedSupplier.Text + "</td></tr>";
                    body += "<tr><td>Suggested Address</td><td>:</td><td>  " + txtSuggestedAddress.Text.Trim() + "</td></tr>";
                    body += "<tr><td>Supplier Phone </td><td>:</td><td> " + txtSupplierPhone.Text.Trim() + "</td></tr>";
                    body += "<tr><td>Budget</td><td>:</td><td> " + (rbtnBudgetYes.Checked ? "YES" : "NO").ToString() + "</td></tr>";

                    body += "<tr><td>Will this Item be Capitalized</td><td>:</td><td> " + (rbtnCapitalizedYes.Checked ? "YES" : "NO").ToString() + "</td></tr>";
                    //body += "<tr><td>WillthisItembeCapitalized</td><td>:</td><td> " + txtWillthisItembeCapitalized.Text.Trim() + "</td></tr>";
                    body += "<tr><td>Service Burea  </td><td>:</td><td> " + (rbtnServiceYes.Checked ? "YES" : "NO").ToString() + "</td></tr>";
                    body += "<tr><td>Criticality </td><td>:</td><td>  " + ddlCriticality.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>ERP ProjectCode </td><td>:</td><td>  " + ddlERPProjectCode.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Project Delivery HeadName </td><td>:</td><td>  " + ddlProjectDeliveryHeadName.Text + "</td></tr>";
                    body += "<tr><td>Billable </td><td>:</td><td>  " + (RbtnBillable.SelectedValue.Equals("1") ? "YES" : "NO").ToString() + "</td></tr><br/>";
                    body += "<tr><td>File Proposal </td><td>:</td><td>  " + (fuProposal.HasFile ? fuProposal.PostedFile.FileName : "NA").ToString() + "</td></tr>";
                    body += "<tr><td>File Agreement </td><td>:</td><td>  " + (fuAgreement.HasFile ? fuAgreement.PostedFile.FileName : "NA").ToString() + "</td></tr>";
                    body += "<tr><td>File EmailCommunication </td><td>:</td><td>  " + (fuEmailCommunication.HasFile ? fuEmailCommunication.PostedFile.FileName : "NA").ToString() + "</td></tr>";
                    body += "<tr><td>File Invoice </td><td>:</td><td>  " + (fuInvoice.HasFile ? fuInvoice.PostedFile.FileName : "NA").ToString() + "</td></tr>";
                    body += "<tr><td>Business Unit </td><td>:</td><td>  " + ddlBusinessUnit.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Region </td><td> : </td><td>  " + ddlRegion.SelectedItem.Text + "</td></tr>";
                    body += "<tr><td>Justification</td><td>:</td><td> " + txtJustification.Text + "</td></tr>";
                    body += "<tr><td>Special Notes </td><td>:</td><td>  " + txtSpecialNotes.Text + "</td></tr>";
                    body += "<tr><td>Total Amount </td><td> : </td><td> " + decimal.Parse(total).ToString("#,##0.00") + " ( " + currency + " ) </td></tr>";
                    body += "<tr><td>Created On </td><td>:</td><td>  " + createdon + "</td></tr></table>";
                    body += "<br/>" + sw1.ToString() + "<br/>";
                    //    //End of preparing the mail body-------------------------------------------
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    //lblMessageBoard.Text = "Mail sent successfully.";
                }


            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                //lblMessageBoard.Text = "Unknown error occured. Please contact your system administrator.";
                lblMessageBoard.Text = " PR Request created successfully. Error in sending mail";

                return;
            }
        }

        private void ClearPr_Request()
        {
            try
            {
                txtRequester.Text = string.Empty;
                txtMainFunction.Text = string.Empty;
                txtSubFunction.Text = string.Empty;
                ddlMISGroupC.ClearSelection();
                txtMISGroupA.Text = string.Empty;
                txtMISGroupB.Text = string.Empty;
                ddlRequesterRegion.ClearSelection();
                ddlBillToAddress.ClearSelection();
                ddlShipToAddress.ClearSelection();
                txtSuggestedSupplier.Text = string.Empty;
                txtSuggestedAddress.Text = string.Empty;
                txtSupplierPhone.Text = string.Empty;
                rbtnBudgetNo.Checked = true;
                rbtnCapitalizedNo.Checked = true;
                txtWillthisItembeCapitalized.Text = string.Empty;
                txtWillthisItembeCapitalized.Enabled = false;
                rbtnServiceNo.Checked = true;
                ddlCriticality.ClearSelection();
                ddlERPProjectCode.ClearSelection();
                ddlProjectDeliveryHeadName.Text = string.Empty;
                txtProjectDeliveryHeadID.Text = string.Empty;
                RbtnBillable.SelectedValue = "2";
                RbtnListProposal.SelectedValue = "0";
                RbtnListAgreement.SelectedValue = "0";
                RbtnListEmailCommunication.SelectedValue = "0";
                RbtnListInvoice.SelectedValue = "0";

                Session["fuProposal"] = null;
                Session["fuAgreement"] = null;
                Session["fuEmailCommunication"] = null;
                Session["fuInvoice"] = null;
                Label1.Text = string.Empty;
                Label2.Text = string.Empty;
                Label3.Text = string.Empty;
                Label4.Text = string.Empty;
                ////fuProposal.Visible = false;
                ////fuAgreement.Visible = false;
                ////fuEmailCommunication.Visible = false;
                ////fuInvoice.Visible = false;

                PnlBillable.Visible = true;
                ddlRegion.ClearSelection();
                ddlBusinessUnit.ClearSelection();
                txtJustification.Text = string.Empty;
                txtSpecialNotes.Text = string.Empty;
                LoadCurrencyTypes();
                LoadBillToAddress();
                LoadShipToAddress();
                LoadRequestorRegion();
                LoadERPProjectCode();
                //  LoadCategory();
                ////LoadBusinessUnit();
                LoadMIS_GrpC();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private void ClearPr_ItemsRequest()
        {
            try
            {
                // ddlCategory.ClearSelection();
                ddlItemDescription.ClearSelection();
                txtItemDesc.Text = string.Empty;
                txtPartNo.Text = string.Empty;
                ddlUnitOfMeasurement.ClearSelection();
                txtNoOfUnits.Text = string.Empty;
                txtUnitPrice.Text = string.Empty;
                //   ddlCurrency.ClearSelection();
                txtTaxable.Text = string.Empty;
                txtItemNote.Text = string.Empty;
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

        #region Item details Gridview events

        protected void grd_ItemInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "DELETE":
                        int ID = int.Parse(grd_ItemInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["id"].ToString());
                        using (DataTable Dt = (DataTable)ViewState["PR_ItemsDT"])
                        {
                            DataRow[] rows = (from t in Dt.AsEnumerable().Cast<DataRow>()
                                              where t.Field<int>("id") == ID
                                              select t).ToArray();

                            foreach (DataRow row in rows)
                            {
                                Dt.Rows.Remove(row);
                                MsgCls("PR Item deleted successfully !", lblIndent, Color.Green);

                            }

                            ViewState["PR_ItemsDT"] = null;
                            ViewState["PR_ItemsDT"] = Dt;
                            if (Dt.Rows.Count > 0)
                            {
                                grd_ItemInfo.DataSource = (DataTable)ViewState["PR_ItemsDT"];
                                grd_ItemInfo.DataBind();
                                //rbtnBudgetYes.Enabled = false;
                                //rbtnBudgetNo.Enabled = false;
                                rbtnCapitalizedYes.Enabled = false;
                                rbtnCapitalizedNo.Enabled = false;
                                txtWillthisItembeCapitalized.Enabled = false;
                                ddlERPProjectCode.Enabled = false;
                            }
                            else
                            {
                                if (ViewState["PR_ItemsDT"] != null)
                                {
                                    using (DataTable Dts = (DataTable)ViewState["PR_ItemsDT"])
                                    {
                                        if (Dts.Rows.Count > 0)
                                        {
                                            grd_ItemInfo.DataSource = (DataTable)ViewState["PR_ItemsDT"];
                                            grd_ItemInfo.DataBind();
                                            //rbtnBudgetYes.Enabled = false;
                                            //rbtnBudgetNo.Enabled = false;
                                            rbtnCapitalizedYes.Enabled = false;
                                            rbtnCapitalizedNo.Enabled = false;
                                            txtWillthisItembeCapitalized.Enabled = false;
                                            ddlERPProjectCode.Enabled = false;
                                        }
                                        else
                                        {
                                            ViewState["PR_ItemsDT"] = null;
                                            grd_ItemInfo.DataSource = null;
                                            grd_ItemInfo.DataBind();
                                            ddlCurrency.ClearSelection();
                                            ddlCurrency.Enabled = true;
                                            btnSubmit.Visible = false;
                                            Attachments.Visible = false;

                                            //rbtnBudgetYes.Enabled = true;
                                            //rbtnBudgetNo.Enabled = true;
                                            rbtnCapitalizedYes.Enabled = true;
                                            rbtnCapitalizedNo.Enabled = true;
                                            txtWillthisItembeCapitalized.Enabled = true;
                                            ddlERPProjectCode.Enabled = true;
                                        }
                                    }
                                }
                            }
                            totalAmt();
                        }
                        break;
                    case "EDITITEMS":

                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in grd_ItemInfo.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }
                        int index = Convert.ToInt32(e.CommandArgument);
                        ViewState["RowIndex"] = index;
                        btnItemAdd.Visible = false;
                        btnUpdateItems.Visible = true;
                        ddlCategory.SelectedValue = grd_ItemInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["MTART"].ToString().Trim();
                        if (ddlCategory.SelectedValue == "Asset")
                        {

                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[3].Enabled = false;
                            ddlCategory.Items[4].Enabled = false;
                            ddlCategory.Items[5].Enabled = false;

                            ddlCategory.Items[4].Enabled = false;
                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = true;
                            ddlCategory.Items[3].Enabled = false;
                            ddlCategory.Items[5].Enabled = false;
                        }
                        else if (ddlCategory.SelectedValue == "Project")
                        {
                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[3].Enabled = false;
                            ddlCategory.Items[4].Enabled = false;
                            ddlCategory.Items[5].Enabled = false;

                            ddlCategory.Items[3].Enabled = false;
                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[4].Enabled = true;
                            ddlCategory.Items[5].Enabled = false;
                        }

                        else
                        {
                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[3].Enabled = false;
                            ddlCategory.Items[4].Enabled = false;
                            ddlCategory.Items[5].Enabled = false;

                            ddlCategory.Items[0].Enabled = true;
                            ddlCategory.Items[1].Enabled = true;
                            ddlCategory.Items[3].Enabled = true;
                            ddlCategory.Items[5].Enabled = true;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[4].Enabled = false;
                        }
                        if (ddlCategory.SelectedValue == "Product")
                        {
                            LoadItemDesc();
                            ddlItemDescription.Visible = true;
                            ddlItemDescription.SelectedValue = grd_ItemInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["MATNR"].ToString().Trim();
                            LoadParticularUnitofMeasurement();
                            txtItemDesc.Visible = false;
                        }
                        else
                        {

                            txtItemDesc.Visible = true;
                            txtItemDesc.Text = grd_ItemInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["TXZ01"].ToString().Trim();
                            ddlItemDescription.Visible = false;
                            LoadUnitofMeasurements();
                        }

                        ddlUnitOfMeasurement.SelectedValue = grd_ItemInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["MEINS"].ToString().Trim();
                        txtNoOfUnits.Text = grd_ItemInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["NO_OF_UNITS"].ToString().Trim();
                        txtUnitPrice.Text = grd_ItemInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["UNIT_PRICE"].ToString().Trim();
                        ddlCurrency.SelectedValue = grd_ItemInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["WAERS"].ToString().Trim();
                        txtItemNote.Text = grd_ItemInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ITEM_NOTE"].ToString().Trim();
                        txtTaxable.Text = grd_ItemInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["TAXABLE"].ToString().Trim();

                        grd_ItemInfo.Columns[7].Visible = false;

                        ddlCurrency.Enabled = grd_ItemInfo.Rows.Count == 1 ? true : false;//newly added

                        break;



                    default:
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        #endregion

        protected void grd_ItemInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        //protected void ddlRequesterRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlRequesterRegion.Focus();
        //}

        //protected void ddlBillToAddress_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlBillToAddress.Focus();
        //}

        //protected void ddlShipToAddress_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlShipToAddress.Focus();
        //}



        //protected void ddlBusinessUnit_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlBusinessUnit.Focus();
        //}

        //protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlRegion.Focus();
        //}

        //protected void ddlUnitOfMeasurement_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlUnitOfMeasurement.Focus();
        //}

        //protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlCurrency.Focus();
        //}

        //protected void ddlCriticality_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlCriticality.Focus();
        //}



        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                searchpr();
                //MsgCls(string.Empty, LblMsg, Color.Transparent);
                //string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                //string textSearch = txtsearch.Text;
                //DateTime createdon = DateTime.Parse(string.IsNullOrEmpty(txtCreatedOn.Text) ? "01/01/0001" : txtCreatedOn.Text);

                //prbl prblObj = new prbl();
                //List<prbo> requisitionboList1 = new List<prbo>();
                //EmployeeId = User.Identity.Name;
                //requisitionboList1 = prblObj.Load_ParticularEmpPRDetails(EmployeeId, SelectedType, textSearch, createdon);

                //Session.Add("PRGrdInfo", requisitionboList1);

                //if (requisitionboList1 == null || requisitionboList1.Count == 0)
                //{
                //    MsgCls("No Records found", LblMsg, Color.Red);
                //    grdPurchaseItemDetails.Visible = false;
                //    grdPurchaseItemDetails.DataSource = null;
                //    grdPurchaseItemDetails.DataBind();
                //    return;
                //}
                //else
                //{
                //    MsgCls("", LblMsg, Color.Transparent);
                //    grdPurchaseItemDetails.Visible = true;
                //    grdPurchaseItemDetails.DataSource = requisitionboList1;
                //    grdPurchaseItemDetails.SelectedIndex = -1;
                //    grdPurchaseItemDetails.DataBind();
                //    ViewPRIfo.Visible = false;
                //}


            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }

        }

        public void searchpr()
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;
                DateTime createdon = DateTime.Parse(string.IsNullOrEmpty(txtCreatedOn.Text) ? "01/01/0001" : txtCreatedOn.Text);

                if (SelectedType != "0" && textSearch == "")
                {
                    MsgCls("Please Enter the Text", LblMsg, Color.Red);
                }

                else if (SelectedType == "0" && textSearch != "")
                {
                    MsgCls("Please Select the Type", LblMsg, Color.Red);
                }
                else
                {


                    prbl prblObj = new prbl();
                    List<prbo> requisitionboList1 = new List<prbo>();

                    requisitionboList1 = prblObj.Load_ParticularEmpPRDetails(User.Identity.Name, SelectedType, textSearch, createdon, "EMP");

                    Session.Add("PRGrdInfo", requisitionboList1);

                    if (requisitionboList1 == null || requisitionboList1.Count == 0)
                    {
                        MsgCls("No Records found", LblMsg, Color.Red);
                        grdPurchaseItemDetails.Visible = false;
                        grdPurchaseItemDetails.DataSource = null;
                        grdPurchaseItemDetails.DataBind();
                        return;
                    }
                    else
                    {
                        MsgCls("", LblMsg, Color.Transparent);
                        grdPurchaseItemDetails.Visible = true;
                        grdPurchaseItemDetails.DataSource = requisitionboList1;
                        grdPurchaseItemDetails.SelectedIndex = -1;
                        grdPurchaseItemDetails.DataBind();

                    }
                }

            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }

        }


        protected void btnclear_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtCreatedOn.Text = string.Empty;
            LoadEmpPRRequestGridView();
            // ViewPRIfo.Visible = false;
            MsgCls("", LblMsg, Color.Transparent);
        }


        private void LoadEmpPRRequestGridView()
        {
            try
            {
                prbl prblObj = new prbl();
                List<prbo> requisitionboList1 = new List<prbo>();

                requisitionboList1 = prblObj.Load_EmpPRDetails(User.Identity.Name, "EMP");

                Session.Add("PRGrdInfo", requisitionboList1);

                grdPurchaseItemDetails.Visible = true;
                grdPurchaseItemDetails.DataSource = requisitionboList1;
                grdPurchaseItemDetails.SelectedIndex = -1;
                grdPurchaseItemDetails.DataBind();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }



        protected void grdPurchaseItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "COPY":

                        divNewPR.Visible = true;
                        divSearch.Visible = false;

                        Session["fuProposal"] = null;
                        Session["fuAgreement"] = null;
                        Session["fuEmailCommunication"] = null;
                        Session["fuInvoice"] = null;
                        Label1.Text = string.Empty;
                        Label2.Text = string.Empty;
                        Label3.Text = string.Empty;
                        Label4.Text = string.Empty;
                        //fuProposal.Visible = false;
                        //fuAgreement.Visible = false;
                        //fuEmailCommunication.Visible = false;
                        //fuInvoice.Visible = false;
                        Attachments.Visible = true;
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in grdPurchaseItemDetails.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }


                        int PRID = int.Parse(grdPurchaseItemDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());
                        Session["PRID"] = PRID;
                        prbl PrBlObj = new prbl();
                        List<prbo> requisitionboList = new List<prbo>();
                        requisitionboList = PrBlObj.Load_PRItemDetails(PRID);
                        string ipernr = requisitionboList[0].IPERNR == null ? "" : requisitionboList[0].IPERNR.ToString().Trim();
                        string rpernr = requisitionboList[0].RPERNR == null ? "" : requisitionboList[0].RPERNR.ToString().Trim();
                        ViewState["RequesterID"] = requisitionboList[0].RPERNR == null ? "" : requisitionboList[0].RPERNR.ToString().Trim();
                        string rpernrname = requisitionboList[0].ENAME == null ? "" : requisitionboList[0].ENAME.ToString().Trim();
                        string PFUNC_AREA = requisitionboList[0].PFUNC_AREA == null ? "" : requisitionboList[0].PFUNC_AREA.ToString().Trim();
                        string BTEXT = requisitionboList[0].BTEXT == null ? "" : requisitionboList[0].BTEXT.ToString().Trim();
                        string MIS_GRPC = requisitionboList[0].MIS_GRPC == null ? "" : requisitionboList[0].MIS_GRPC.ToString().Trim();
                        string MIS_GRPA = requisitionboList[0].MIS_GRPA == null ? "" : requisitionboList[0].MIS_GRPA.ToString().Trim();
                        string MIS_GRPB = requisitionboList[0].MIS_GRPB == null ? "" : requisitionboList[0].MIS_GRPB.ToString().Trim();
                        string EKGRP = requisitionboList[0].EKGRP == null ? "" : requisitionboList[0].EKGRP.ToString().Trim();
                        string BWERKS = requisitionboList[0].BWERKS == null ? "" : requisitionboList[0].BWERKS.ToString().Trim();
                        string SWERKS = requisitionboList[0].SWERKS == null ? "" : requisitionboList[0].SWERKS.ToString().Trim();
                        string SUG_SUPP = requisitionboList[0].SUG_SUPP == null ? "" : requisitionboList[0].SUG_SUPP.ToString().Trim();
                        string SUP_ADDRESS = requisitionboList[0].SUP_ADDRESS == null ? "" : requisitionboList[0].SUP_ADDRESS.ToString().Trim();
                        string SUP_PHONE = requisitionboList[0].SUP_PHONE == null ? "" : requisitionboList[0].SUP_PHONE.ToString().Trim();
                        string IN_BUDGET = requisitionboList[0].IN_BUDGET == null ? "" : requisitionboList[0].IN_BUDGET.ToString().Trim();
                        string CAPITALIZED = requisitionboList[0].CAPITALIZED == null ? "" : requisitionboList[0].CAPITALIZED.ToString().Trim();
                        string SERVICE_BUREA = requisitionboList[0].SERVICE_BUREA == null ? "" : requisitionboList[0].SERVICE_BUREA.ToString().Trim();
                        string CRITICALITY = requisitionboList[0].CRITICALITY == null ? "" : requisitionboList[0].CRITICALITY.ToString().Trim();
                        string PSPNR = requisitionboList[0].PSPNR == null ? "" : requisitionboList[0].PSPNR.ToString().Trim();
                        string VERNR = requisitionboList[0].VERNR == null ? "" : requisitionboList[0].VERNR.ToString().Trim();
                        string SPART = requisitionboList[0].SPART == null ? "" : requisitionboList[0].SPART.ToString().Trim();
                        string JUSTIFICATION = requisitionboList[0].JUSTIFICATION == null ? "" : requisitionboList[0].JUSTIFICATION.ToString().Trim();
                        string SPL_NOTES = requisitionboList[0].SPL_NOTES == null ? "" : requisitionboList[0].SPL_NOTES.ToString().Trim();
                        string WAERS = requisitionboList[0].WAERS == null ? "" : requisitionboList[0].WAERS.ToString().Trim();
                        string MISCIDID = requisitionboList[0].MISCIDID == null ? "" : requisitionboList[0].MISCIDID.ToString().Trim();
                        string BWERKSID = requisitionboList[0].BWERKSID == null ? "" : requisitionboList[0].BWERKSID.ToString().Trim();
                        string SWERKSID = requisitionboList[0].SWERKSID == null ? "" : requisitionboList[0].SWERKSID.ToString().Trim();
                        string PSPNRID = requisitionboList[0].PSPNRID == null ? "" : requisitionboList[0].PSPNRID.ToString().Trim();
                        string SPARTID = requisitionboList[0].SPARTID == null ? "" : requisitionboList[0].SPARTID.ToString().Trim();
                        string CAP_TEXT = requisitionboList[0].CAP_TEXT == null ? "" : requisitionboList[0].CAP_TEXT.ToString().Trim();
                        string REGID = requisitionboList[0].REGIONID == null ? "" : requisitionboList[0].REGIONID.ToString().Trim();

                        txtRequester.Text = rpernrname + " - " + rpernr;
                        txtMainFunction.Text = PFUNC_AREA;
                        txtSubFunction.Text = BTEXT;
                        ddlMISGroupC.SelectedValue = MISCIDID;
                        LoadMIS_GrpAB();
                        ddlRequesterRegion.SelectedValue = EKGRP;
                        ddlBillToAddress.SelectedValue = BWERKSID;
                        ddlShipToAddress.SelectedValue = ddlBillToAddress.SelectedValue;//SWERKSID;
                        txtSuggestedSupplier.Text = SUG_SUPP;
                        txtSuggestedAddress.Text = SUP_ADDRESS;
                        txtSupplierPhone.Text = SUP_PHONE;
                        if (IN_BUDGET == "NO")
                        {
                            rbtnBudgetNo.Checked = true;
                            rbtnBudgetYes.Checked = false;
                            txtWillthisItembeCapitalized.Enabled = false;
                        }
                        else
                        {
                            rbtnBudgetNo.Checked = false;
                            rbtnBudgetYes.Checked = true;
                            txtWillthisItembeCapitalized.Enabled = true;
                        }

                        if (CAPITALIZED == "NO")
                        {
                            rbtnCapitalizedNo.Checked = true;
                            rbtnCapitalizedYes.Checked = false;
                            ////txtWillthisItembeCapitalized.Enabled = false;
                        }
                        else
                        {
                            rbtnCapitalizedNo.Checked = false;
                            rbtnCapitalizedYes.Checked = true;
                            txtWillthisItembeCapitalized.Enabled = true;
                            txtWillthisItembeCapitalized.Text = CAP_TEXT;
                        }
                        if (SERVICE_BUREA == "NO")
                        {
                            rbtnServiceNo.Checked = true;
                            rbtnServiceYes.Checked = false;
                        }
                        else
                        {
                            rbtnServiceNo.Checked = false;
                            rbtnServiceYes.Checked = true;
                        }

                        ddlCriticality.SelectedValue = CRITICALITY;
                        ddlERPProjectCode.SelectedValue = PSPNRID;

                        if (REGID == "")
                        {
                            ddlRegion.SelectedValue = "0";
                        }
                        else
                        {
                            ddlRegion.SelectedValue = REGID;
                        }
                        LoadERPPrjMngr();
                        if (ddlERPProjectCode.SelectedValue.StartsWith("E/"))
                        {
                            ddlCategory.SelectedValue = "Project";
                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[3].Enabled = false;
                            ddlCategory.Items[4].Enabled = true;
                            ddlCategory.Items[5].Enabled = false;
                            ddlCategory.Items[3].Enabled = false;

                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[4].Enabled = true;
                            ddlCategory.Items[5].Enabled = false;
                            ddlItemDescription.Visible = false;
                            txtItemDesc.Visible = txtItemDesc.Enabled = true;
                            LoadUnitofMeasurements();
                            RFV_txtItemDesc.Enabled = true;
                            RFV_txtItemDesc.Visible = true;
                            RFV_ddlItemDescription.Enabled = false;
                            RFV_ddlItemDescription.Visible = false;
                            fuProposal = (FileUpload)Session["fuProposal"];
                        }

                        else if (ddlERPProjectCode.SelectedValue.StartsWith("I/") && (rbtnCapitalizedYes.Checked == true))
                        {
                            ddlCategory.SelectedValue = "Asset";
                            ddlCategory.Items[4].Attributes.Add("enabled", "enabled");
                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = true;
                            ddlCategory.Items[3].Enabled = false;
                            ddlCategory.Items[4].Enabled = false;
                            ddlCategory.Items[5].Enabled = false;
                            ddlCategory.Items[4].Enabled = false;
                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = false;
                            ddlCategory.Items[2].Enabled = true;
                            ddlCategory.Items[3].Enabled = false;
                            ddlCategory.Items[5].Enabled = false;
                            ddlItemDescription.Visible = false;
                            txtItemDesc.Visible = txtItemDesc.Enabled = true;
                            LoadUnitofMeasurements();
                            RFV_txtItemDesc.Enabled = true;
                            RFV_txtItemDesc.Visible = true;
                            RFV_ddlItemDescription.Enabled = false;
                            RFV_ddlItemDescription.Visible = false;

                            fuProposal = (FileUpload)Session["fuProposal"];
                        }



                        else if (ddlERPProjectCode.SelectedValue.StartsWith("I/") && (rbtnCapitalizedNo.Checked == true))
                        {

                            ddlCategory.SelectedValue = "0";
                            ddlCategory.Items[0].Attributes.Add("enabled", "enabled");
                            ddlCategory.Items[1].Attributes.Add("enabled", "enabled");
                            ddlCategory.Items[2].Attributes.Add("enabled", "enabled");
                            ddlCategory.Items[5].Attributes.Add("enabled", "enabled");
                            ddlCategory.Items[0].Enabled = false;
                            ddlCategory.Items[1].Enabled = true;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[3].Enabled = true;
                            ddlCategory.Items[4].Enabled = false;
                            ddlCategory.Items[5].Enabled = true;
                            ddlCategory.Items[0].Enabled = true;
                            ddlCategory.Items[1].Enabled = true;
                            ddlCategory.Items[2].Enabled = false;
                            ddlCategory.Items[5].Enabled = true;
                            ddlCategory.Items[3].Enabled = true;
                            ddlCategory.Items[4].Enabled = false;
                            ddlItemDescription.Visible = false;
                            txtItemDesc.Visible = txtItemDesc.Enabled = true;
                            LoadUnitofMeasurements();
                            RFV_txtItemDesc.Enabled = true;
                            RFV_txtItemDesc.Visible = true;
                            RFV_ddlItemDescription.Enabled = false;
                            RFV_ddlItemDescription.Visible = false;
                            fuProposal = (FileUpload)Session["fuProposal"];

                        }
                        LoadBusinessUnit(ddlBillToAddress.SelectedValue);
                        ddlBusinessUnit.SelectedValue = SPARTID;
                        txtJustification.Text = JUSTIFICATION;
                        txtSpecialNotes.Text = SPL_NOTES;


                        requisitionboList = PrBlObj.Load_PRItem(PRID);
                        grd_ItemInfo.DataSource = requisitionboList;
                        grd_ItemInfo.DataBind();



                        //          Dt.Columns.Add("ID", typeof(int));
                        //Dt.Columns.Add("BANFN_EXT", typeof(int));
                        //Dt.Columns.Add("BNFPO", typeof(string));//MTART
                        ////Dt.Columns.Add("MTART", typeof(string));
                        //Dt.Columns.Add("MATNR", typeof(string));
                        //Dt.Columns.Add("TXZ01", typeof(string));
                        //Dt.Columns.Add("PART_NO", typeof(string));
                        //Dt.Columns.Add("MTART", typeof(string));
                        //Dt.Columns.Add("MEINS", typeof(string));
                        //Dt.Columns.Add("NO_OF_UNITS", typeof(string));
                        //Dt.Columns.Add("UNIT_PRICE", typeof(string));
                        //Dt.Columns.Add("WAERS", typeof(string));
                        //Dt.Columns.Add("TAXABLE", typeof(string));
                        //Dt.Columns.Add("ITEM_NOTE", typeof(string));
                        ViewState["PR_ItemsDT"] = null;
                        int listid = 1;
                        if (grd_ItemInfo.Rows.Count > 0)
                        {
                            foreach (GridViewRow row in grd_ItemInfo.Rows)
                            {

                                if (ViewState["PR_ItemsDT"] != null)
                                {
                                    using (DataTable Dt = (DataTable)ViewState["PR_ItemsDT"])
                                    {
                                        Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, requisitionboList[listid].MTART.ToString().Trim(), requisitionboList[listid].MATNR.ToString().Trim()
                                                , requisitionboList[listid].TXZ01.ToString().Trim(), requisitionboList[listid].PART_NO.ToString().Trim(),
                                                requisitionboList[listid].MTART.ToString().Trim(),
                                                requisitionboList[listid].MEINS.ToString().Trim(), requisitionboList[listid].NO_OF_UNITS.ToString().Trim()
                                                , requisitionboList[listid].UNIT_PRICE.ToString().Trim(), requisitionboList[listid].WAERS.ToString().Trim(),
                                               requisitionboList[listid].TAXABLE.ToString().Trim(), requisitionboList[listid].ITEM_NOTE.ToString().Trim());
                                        grd_ItemInfo.DataSource = Dt;
                                        grd_ItemInfo.DataBind();
                                        //listid =+1;
                                        listid = listid + 1;
                                        //ViewState["PR_ItemsDT"] = Dt;


                                        //----------------------------------------------------------
                                        //decimal d = 0;
                                        //decimal totalAmount = Dt.AsEnumerable()
                                        //         .Where(r => decimal.TryParse(r.Field<string>("UNIT_PRICE"), out d))
                                        //         .Sum(r => d);
                                        //decimal totalUnits = Dt.AsEnumerable()
                                        //         .Where(r => decimal.TryParse(r.Field<string>("NO_OF_UNITS"), out d))
                                        //         .Sum(r => d);
                                        //decimal total = totalAmount * totalUnits;
                                        //grd_ItemInfo.FooterRow.Cells[7].Text = "Total";

                                        //grd_ItemInfo.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                                        //grd_ItemInfo.FooterRow.Cells[8].Text = total.ToString("N2") + "(" + (WAERS) + ")";

                                        //----------------------------------------------------------






                                        if (ddlCategory.SelectedValue == "Product")
                                        {
                                            ddlItemDescription.Focus();
                                        }
                                        else
                                        {
                                            txtItemDesc.Focus();
                                        }
                                    }

                                }
                                else
                                {
                                    using (DataTable Dt = GetPR_ItemsDt())
                                    {
                                        //requisitionboList[0].APPROVEDBY1 == null ? "" : requisitionboList[0].APPROVEDBY1.ToString()

                                        Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, requisitionboList[0].MTART.ToString().Trim(), requisitionboList[0].MATNR.ToString().Trim()
                                            , requisitionboList[0].TXZ01.ToString().Trim(), requisitionboList[0].PART_NO.ToString().Trim(), requisitionboList[0].MTART.ToString().Trim(),
                                            requisitionboList[0].MEINS.ToString().Trim(), requisitionboList[0].NO_OF_UNITS.ToString().Trim()
                                            , requisitionboList[0].UNIT_PRICE.ToString().Trim(), requisitionboList[0].WAERS.ToString().Trim(),
                                           requisitionboList[0].TAXABLE.ToString().Trim(), requisitionboList[0].ITEM_NOTE.ToString().Trim());
                                        grd_ItemInfo.DataSource = Dt;
                                        grd_ItemInfo.DataBind();
                                        ViewState["PR_ItemsDT"] = Dt;

                                        //----------------------------------------------------------
                                        //decimal d = 0;
                                        //decimal totalAmount = Dt.AsEnumerable()
                                        //         .Where(r => decimal.TryParse(r.Field<string>("UNIT_PRICE"), out d))
                                        //         .Sum(r => d);
                                        //decimal totalUnits = Dt.AsEnumerable()
                                        //         .Where(r => decimal.TryParse(r.Field<string>("NO_OF_UNITS"), out d))
                                        //         .Sum(r => d);
                                        //decimal total = totalAmount * totalUnits;
                                        //grd_ItemInfo.FooterRow.Cells[7].Text = "Total";

                                        //grd_ItemInfo.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                                        //grd_ItemInfo.FooterRow.Cells[8].Text = total.ToString("N2") + "(" + (WAERS) + ")";

                                        //----------------------------------------------------------




                                        if (ddlCategory.SelectedValue == "Product")
                                        {
                                            ddlItemDescription.Focus();
                                        }
                                        else
                                        {
                                            txtItemDesc.Focus();
                                        }
                                        //rbtnBudgetYes.Enabled = false;
                                        //rbtnBudgetNo.Enabled = false;
                                        rbtnCapitalizedYes.Enabled = false;
                                        rbtnCapitalizedNo.Enabled = false;
                                        ////txtWillthisItembeCapitalized.Enabled = false;
                                        ddlERPProjectCode.Enabled = false;
                                    }
                                }
                            }
                            totalAmt();
                        }
                        //DataTable dtcopy = ConvertToDataTable(requisitionboList);  

                        break;

                    case "CANCEL":


                        int PRID1 = int.Parse(grdPurchaseItemDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());

                        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString);
                        SqlCommand cmd = new SqlCommand("Usp_PR_Cancel", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PR_ID", PRID1);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            LblMsg.Text = "Selected PR : " + PRID1 + " has been Cancelled Successfully";
                            LblMsg.ForeColor = System.Drawing.Color.Green;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + "Selected PR : " + PRID1 + " has been Cancelled Successfully" + "')", true);
                            LoadEmpPRRequestGridView();
                        }
                        else
                        {
                            LblMsg.Text = "Unable to Cancel the selected PR : " + PRID1;
                            LblMsg.ForeColor = System.Drawing.Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + "Unable to Cancel the selected PR : " + PRID1 + "')", true);
                        }
                        con.Close();

                        break;



                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        protected void btnUpdateItems_Click(object sender, EventArgs e)
        {
            try
            {




                int rowindex = int.Parse(ViewState["RowIndex"].ToString());
                // int lid = int.Parse(ViewState["IDtoAdd"].ToString());

                using (DataTable Dt = (DataTable)ViewState["PR_ItemsDT"])
                {

                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        //Dt.Columns.Add("BANFN_EXT", typeof(int));
                        //Dt.Columns.Add("BNFPO", typeof(string));//MTART
                        ////Dt.Columns.Add("MTART", typeof(string));
                        //Dt.Columns.Add("MATNR", typeof(string));
                        //Dt.Columns.Add("TXZ01", typeof(string));
                        //Dt.Columns.Add("PART_NO", typeof(string));
                        //Dt.Columns.Add("MTART", typeof(string));
                        //Dt.Columns.Add("MEINS", typeof(string));
                        //Dt.Columns.Add("NO_OF_UNITS", typeof(string));
                        //Dt.Columns.Add("UNIT_PRICE", typeof(string));
                        //Dt.Columns.Add("WAERS", typeof(string));
                        //Dt.Columns.Add("TAXABLE", typeof(string));
                        //Dt.Columns.Add("ITEM_NOTE", typeof(string));
                        Dt.Rows[rowindex]["MATNR"] = ddlItemDescription.SelectedValue;
                        Dt.Rows[rowindex]["TXZ01"] = txtItemDesc.Text;
                        Dt.Rows[rowindex]["MEINS"] = ddlUnitOfMeasurement.SelectedValue;
                        Dt.Rows[rowindex]["NO_OF_UNITS"] = txtNoOfUnits.Text;
                        Dt.Rows[rowindex]["UNIT_PRICE"] = txtUnitPrice.Text;
                        Dt.Rows[rowindex]["ITEM_NOTE"] = txtItemNote.Text;
                        Dt.Rows[rowindex]["TAXABLE"] = txtTaxable.Text;

                        Dt.Rows[rowindex]["MTART"] = ddlCategory.SelectedValue;
                        Dt.Rows[rowindex]["WAERS"] = ddlCurrency.SelectedValue;


                    }
                    grd_ItemInfo.DataSource = Dt;
                    grd_ItemInfo.DataBind();


                    ViewState["PR_ItemsDT"] = Dt;

                    //ClearClaimLineItemssubmit();
                    ClearPr_ItemsRequest();

                    MsgCls("PR Item Updated Successfully !", lblIndent, Color.Green);

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('PR Item Updated Successfully !')", true);

                    grd_ItemInfo.Columns[7].Visible = true;


                }

                btnItemAdd.Visible = true;
                btnUpdateItems.Visible = false;
                if (ddlCategory.SelectedValue == "Product")
                {
                    ddlItemDescription.Focus();
                }
                else
                {
                    txtItemDesc.Focus();
                }
                totalAmt();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void grdPurchaseItemDeatils_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdPurchaseItemDetails.PageIndex = e.NewPageIndex;

            LoadEmpPRRequestGridView();
            searchpr();
            grdPurchaseItemDetails.SelectedIndex = -1;
        }

        protected void grdPurchaseItemDeatils_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<prbo> PrboList = (List<prbo>)Session["PRGrdInfo"];
            bool objSortOrder = (bool)Session["bSortedOrder"];
            switch (e.SortExpression.ToString().Trim())
            {
                case "PRID":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.PRID.Value.CompareTo(objBo2.PRID.Value)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.PRID.Value.CompareTo(objBo1.PRID.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "SUG_SUPP":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.SUG_SUPP.CompareTo(objBo2.SUG_SUPP)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.SUG_SUPP.CompareTo(objBo1.SUG_SUPP)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;


                case "IN_BUDGET":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.IN_BUDGET.CompareTo(objBo2.IN_BUDGET)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.IN_BUDGET.CompareTo(objBo1.IN_BUDGET)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CRITICALITY":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.CRITICALITY.CompareTo(objBo2.CRITICALITY)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.CRITICALITY.CompareTo(objBo1.CRITICALITY)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "STATUS":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.STATUS.CompareTo(objBo2.STATUS)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.STATUS.CompareTo(objBo1.STATUS)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "PSPNR":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.PSPNR.CompareTo(objBo2.PSPNR)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.PSPNR.CompareTo(objBo1.PSPNR)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "BNFPO":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.BNFPO.CompareTo(objBo2.BNFPO)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.BNFPO.CompareTo(objBo1.BNFPO)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "WAERS":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.WAERS.CompareTo(objBo2.WAERS)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.WAERS.CompareTo(objBo1.WAERS)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "UNIT_PRICE":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return ((decimal.Parse(objBo1.UNIT_PRICE)).CompareTo(decimal.Parse(objBo2.UNIT_PRICE))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return ((decimal.Parse(objBo2.UNIT_PRICE)).CompareTo(decimal.Parse(objBo1.UNIT_PRICE))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "TOTALAMT":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return ((decimal.Parse(objBo1.TAINRAmt)).CompareTo(decimal.Parse(objBo2.TAINRAmt))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return ((decimal.Parse(objBo2.TAINRAmt)).CompareTo(decimal.Parse(objBo1.TAINRAmt))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CREATED_ON1":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.CREATED_ON1.Value.CompareTo(objBo2.CREATED_ON1.Value)); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.CREATED_ON1.Value.CompareTo(objBo1.CREATED_ON1.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "INDENTOR":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.IPERNR.CompareTo(objBo2.IPERNR)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.IPERNR.CompareTo(objBo1.IPERNR)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "REQUESTOR":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.RPERNR.CompareTo(objBo2.RPERNR)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.RPERNR.CompareTo(objBo1.RPERNR)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
            }

            grdPurchaseItemDetails.DataSource = PrboList;
            grdPurchaseItemDetails.DataBind();

            Session.Add("PRGrdInfo", PrboList);
        }

        protected void btnNewPR_Click(object sender, EventArgs e)
        {
            divNewPR.Visible = true;
            divSearch.Visible = false;
            Session["PRID"] = null;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProjectDeliveryHeadID.Text != "" && txtProjectDeliveryHeadID.Text != txtRequester.Text.Split('-')[1].Trim() && ddlERPProjectCode.SelectedValue.StartsWith("E/"))
            {
                ////ddlERPProjectCode.SelectedValue = "0";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('External Projects can be added only by the PM of the project.')", true);
                ddlProjectDeliveryHeadName.Text = "";
                txtProjectDeliveryHeadID.Text = "";
            }
            else
            {
                SaveorSendPR("Save");
            }
        }




        protected void rbtnBudgetYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnBudgetYes.Checked && rbtnCapitalizedYes.Checked)
            {
                txtWillthisItembeCapitalized.Visible = true;
                txtWillthisItembeCapitalized.Enabled = true;
                RFVWillthisItembeCapitalized.Enabled = true;
                RFVWillthisItembeCapitalized.Visible = true;
            }

            else if (rbtnBudgetYes.Checked && rbtnCapitalizedNo.Checked)
            {
                txtWillthisItembeCapitalized.Visible = true;
                txtWillthisItembeCapitalized.Enabled = true;
                RFVWillthisItembeCapitalized.Enabled = false;
                RFVWillthisItembeCapitalized.Visible = false;
            }

            else
            {
                txtWillthisItembeCapitalized.Visible = false;
                txtWillthisItembeCapitalized.Enabled = false;
                RFVWillthisItembeCapitalized.Enabled = false;
                RFVWillthisItembeCapitalized.Visible = false;
                txtWillthisItembeCapitalized.Text = string.Empty;
            }
        }


        protected void grdPurchaseItemDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void rbtnBudgetNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnBudgetNo.Checked && rbtnCapitalizedYes.Checked)
            {
                txtWillthisItembeCapitalized.Visible = false;
                txtWillthisItembeCapitalized.Enabled = false;
                RFVWillthisItembeCapitalized.Enabled = false;
                RFVWillthisItembeCapitalized.Visible = false;
                txtWillthisItembeCapitalized.Text = string.Empty;
            }
            else if (rbtnBudgetNo.Checked && rbtnCapitalizedNo.Checked)
            {
                txtWillthisItembeCapitalized.Visible = false;
                txtWillthisItembeCapitalized.Enabled = false;
                RFVWillthisItembeCapitalized.Enabled = false;
                RFVWillthisItembeCapitalized.Visible = false;
                txtWillthisItembeCapitalized.Text = string.Empty;
            }

            else
            {
                txtWillthisItembeCapitalized.Visible = true;
                txtWillthisItembeCapitalized.Enabled = true;
                RFVWillthisItembeCapitalized.Enabled = false;
                RFVWillthisItembeCapitalized.Visible = false;

            }
        }

        protected void ddlBillToAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBusinessUnit(ddlBillToAddress.SelectedValue);
            ddlShipToAddress.SelectedValue = ddlBillToAddress.SelectedValue;
            txtShipToAddress.Text = ddlShipToAddress.SelectedItem.Text;//Newly Added
            ddlBillToAddress.Focus();
        }

        //protected void ddlSeachSelect_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtsearch.Focus();
        //}

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            dvlineitem.Visible = true;
            ddlCategory.Focus();
        }

        protected void lbtAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Purchase_Request.aspx");
        }


        //decimal total = 0;
        //string curr;
        //protected void grd_ItemInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "UNIT_PRICE")) * Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NO_OF_UNITS"));
        //        curr = (DataBinder.Eval(e.Row.DataItem, "WAERS")).ToString();
        //    }
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        Label lblTotaltxt = (Label)e.Row.FindControl("lblTotaltxt");
        //        lblTotaltxt.Text = "Total";
        //        Label lblTotalAmt = (Label)e.Row.FindControl("lblTotalAmt");
        //        lblTotalAmt.Text = total.ToString("N2") + "(" + curr + ")";
        //    }
        //}

        protected void totalAmt()
        {
            decimal sum = 0; string curr = "";
            grd_ItemInfo.FooterRow.Cells[7].Text = "Total : ";
            grd_ItemInfo.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            foreach (GridViewRow row in grd_ItemInfo.Rows)
            {
                curr = grd_ItemInfo.DataKeys[row.RowIndex].Values["WAERS"].ToString();
                sum += (Convert.ToDecimal(grd_ItemInfo.DataKeys[row.RowIndex].Values["NO_OF_UNITS"].ToString()) * Convert.ToDecimal(grd_ItemInfo.DataKeys[row.RowIndex].Values["UNIT_PRICE"].ToString()));
            }
            grd_ItemInfo.FooterRow.Cells[8].Text = sum.ToString("N2") + " (" + curr + ")";
            ltTotalAmount.Text = sum.ToString("N2") + " (" + curr + ")";

            ddlCurrency.SelectedValue = curr.Trim();
            ddlCurrency.Enabled = grd_ItemInfo.Rows.Count > 0 ? false : true;
        }

        protected string returnSubTypeTxt(string type) //updated
        {
            string txt = "";
            masterbo mBo = new masterbo();
            mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_ItemDesc();
            ddlItemDescription.DataSource = objLst;
            foreach (var a in objLst.AsEnumerable().Where(i => i.MATNR == type))
            {
                txt = a.MAKTX.ToString().Trim();
            }
            // txt = objLst.AsEnumerable().Where(i => i.MATNR == type).Select(i => i.MAKTX).FirstOrDefault();
            return txt;
        }
    }
}
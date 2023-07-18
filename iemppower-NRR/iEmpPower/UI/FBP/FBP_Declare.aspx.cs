using iEmpPower.Old_App_Code.iEmpPowerBL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.FBP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.FBP
{
    public partial class FBP_Declare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    pageLoadEvents();
                    LoadDeclarationClaims();
                }
                GetLastUpdatedDate();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void LoadDeclarationClaims()
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                string ApproverId = User.Identity.Name;
                fbpboObj1 = fbpblObj.Load_AllfbpDeclarationclaims(ApproverId.Trim());
               
                if (fbpboObj1 == null || fbpboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", lblmsg, Color.Red);
                    grdFbpDeclaration.Visible = false;
                    grdFbpDeclaration.DataSource = null;
                    grdFbpDeclaration.DataBind();
                    Exportbtn.Visible = false;
                    return;
                }
                else
                {
                    MsgCls("", lblmsg, Color.Transparent);
                    grdFbpDeclaration.Visible = true;
                    grdFbpDeclaration.DataSource = fbpboObj1;
                    grdFbpDeclaration.SelectedIndex = -1;
                    grdFbpDeclaration.DataBind();
                    Exportbtn.Visible = true;
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void pageLoadEvents()
        {
            try
            {
                bool? fbplock = false;
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
                objDataContext.usp_Fbp_GetLockStatus(User.Identity.Name, ref fbplock);

                if (fbplock == false)
                {
                MsgCls("", lblMessageBoard, Color.Transparent);
                divview.Visible = true;
                GetPlans();
                GetBasketTotal();
                GetHeadsofAllowances();
                GetFinancialDates();
                getVisibility();
                }
                else if (fbplock == true)
                {
                    divview.Visible = false;
                    MsgCls("FBP Declaration has been locked. Please contact Payroll Admin.", lblMessageBoard, Color.Red);
                }
                    


             
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void GetFinancialDates()
        {
            try
            {
                //DateTime dt = DateTime.Now;
                int month = int.Parse(DateTime.Today.Month.ToString());
                if (month > 3)
                {
                    LblFromDate.Text = DateTime.Today.Year.ToString();
                    LblToDate.Text = (DateTime.Today.Year + 1).ToString();
                }
                else if (month <= 3)
                {
                    LblFromDate.Text = (DateTime.Today.Year - 1).ToString();
                    LblToDate.Text = DateTime.Today.Year.ToString();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void GetPlans()
        {
            try
            {
                Fbp_Claimbl objBl = new Fbp_Claimbl();
                FbpClaimscollectionbo objLst = objBl.GetPlans(User.Identity.Name);
                foreach (FbpClaimbo objBo in objLst)
                {
                    DateTime joindate = DateTime.ParseExact(objBo.PLAN, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                    DateTime now = DateTime.Now;
                    DateTime startDate = new DateTime(now.Year, now.Month, 1);
                    if (joindate < startDate)
                    {
                        lblPlanDate.Text = startDate.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lblPlanDate.Text = objBo.PLAN;
                    }
                    lblJoiningDate.Text = objBo.PLAN;
                    lblEffectiveDate.Text = lblPlanDate.Text;
                    lblPayDate.Text = lblPlanDate.Text;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void GetBasketTotal()
        {
            try
            {
                Fbp_Claimbl objBl = new Fbp_Claimbl();
                FbpClaimscollectionbo objLst = objBl.GetBasketTotal(User.Identity.Name);
                foreach (FbpClaimbo objBo in objLst)
                {
                    lblBasketTotalAmount.Text = objBo.BASKETTOTAL;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void GetHeadsofAllowances()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                Fbp_Claimbl objBl = new Fbp_Claimbl();
                List<FbpClaimbo> requisitionboList = new List<FbpClaimbo>();
                List<FbpClaimbo> requisitionboList1 = new List<FbpClaimbo>();

                requisitionboList1 = objBl.GetHeadsofAllowances(User.Identity.Name);
                requisitionboList.AddRange(requisitionboList1);

                if (requisitionboList == null || requisitionboList.Count == 0)
                {
                    GV_FBPDeclare.Visible = false;
                    GV_FBPDeclare.DataSource = null;
                    MsgCls("No Records Found", lblMessageBoard, Color.Red);

                    return;
                }
                else
                {
                    GV_FBPDeclare.Visible = true;
                    GV_FBPDeclare.DataSource = requisitionboList;
                    GV_FBPDeclare.SelectedIndex = -1;
                    MsgCls("", lblMessageBoard, Color.Transparent);

                }
                GV_FBPDeclare.DataBind();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void GetLastUpdatedDate()
        {
            try
            {
                Fbp_Claimbl objBl = new Fbp_Claimbl();
                FbpClaimscollectionbo objLst = objBl.GetLastUpdatedDate(User.Identity.Name);
                foreach (FbpClaimbo objBo in objLst)
                {
                    DateTime LastUpdatedDate = DateTime.ParseExact(objBo.LASTUPDATEDDATE, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                    lblLastUpdatedDate.Text = LastUpdatedDate.ToString("dd-MMM-yyyy");
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void getVisibility()
        {
            foreach (GridViewRow row in GV_FBPDeclare.Rows)
            {
                int Id = int.Parse(row.Cells[0].Text);
                double Entitled = double.Parse(row.Cells[2].Text.Replace(",", ""));
                TextBox txtAnnualAllocation = row.Cells[4].FindControl("txtAnnualAllocation") as TextBox;
                Label lblAnnualAllocation = row.Cells[4].FindControl("lblAnnualAllocation") as Label;
                switch (Id)
                {
                    case 1225://Car EMI
                        txtAnnualAllocation.Visible = false;
                        lblAnnualAllocation.Visible = true;
                        lblAnnualAllocation.Text = txtAnnualAllocation.Text;
                        break;

                    case 1215://LTA

                        int LTAcount = GetLTAMobPurCount("1215");
                        if (LTAcount > 0)
                        {
                            txtAnnualAllocation.Visible = false;
                            lblAnnualAllocation.Visible = true;
                            lblAnnualAllocation.Text=txtAnnualAllocation.Text;
                        }
                        else
                        {
                            txtAnnualAllocation.Visible = true;
                            lblAnnualAllocation.Visible = false;
                        }
                        break;

                    case 1276://Mobile Purchase
                        int MPcount = GetLTAMobPurCount("1276");
                        if (MPcount > 0)
                        {
                            txtAnnualAllocation.Visible = false;
                            lblAnnualAllocation.Visible = true;
                            lblAnnualAllocation.Text = txtAnnualAllocation.Text;
                        }
                        else
                        {
                            txtAnnualAllocation.Visible = true;
                            lblAnnualAllocation.Visible = false;
                        }
                        break;

                    case 1245://Education(Training allowance)
                        int Edcount = GetLTAMobPurCount("1245");
                        if (Edcount > 0)
                        {
                            txtAnnualAllocation.Visible = false;
                            lblAnnualAllocation.Visible = true;
                            lblAnnualAllocation.Text = txtAnnualAllocation.Text;
                        }
                        else
                        {
                            txtAnnualAllocation.Visible = true;
                            lblAnnualAllocation.Visible = false;
                        }
                        break;

                    default:
                        txtAnnualAllocation.Visible = true;
                        lblAnnualAllocation.Visible = false;
                        break;
                }
            }
        }

        public int GetLTAMobPurCount(string LGART)
        {
            int count = 0;
            //try
            //{               
            Fbp_Claimbl objBl = new Fbp_Claimbl();
            FbpClaimscollectionbo objLst = objBl.GetLTAMobPurCount(User.Identity.Name, LGART);
            foreach (FbpClaimbo objBo in objLst)
                {
                    count = objBo.COUNT;
                }
                return count;
            //}
            //catch (Exception Ex)
            //{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                createFbpDeclare();
                LoadDeclarationClaims();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        public void createFbpDeclare()
        {
            try
            {
                string pernr = User.Identity.Name;
                DateTime begda = DateTime.ParseExact(lblPlanDate.Text, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                string AA_AMT01 = "";
                string AA_AMT02 = "";
                string AA_AMT03 = ""; int mvflag = 0;
                string AA_AMT04 = "";
                string AA_AMT05 = "";
                string AA_AMT06 = "";
                string AA_AMT07 = "";
                string AA_AMT08 = "";
                string AA_AMT09 = "";
                string AA_AMT10 = "";
                string AA_AMT11 = "";
                string AA_AMT12 = "";


                RangeValidator RVtxtAnnualAllocation = new RangeValidator();

                HiddenField HFAllocationTotal = (HiddenField)GV_FBPDeclare.FooterRow.FindControl("HFAllocationTotal");
                string AnnualAllocationTotal = HFAllocationTotal.Value;


                foreach (GridViewRow row in GV_FBPDeclare.Rows)
                {
                    int Id = int.Parse(row.Cells[0].Text);
                    double Entitled = double.Parse(row.Cells[2].Text.Replace(",", ""));
                    TextBox txtAnnualAllocation = row.Cells[4].FindControl("txtAnnualAllocation") as TextBox;
                    RVtxtAnnualAllocation = row.Cells[5].FindControl("RVtxtAnnualAllocation") as RangeValidator;
                    string Annual = double.Parse(txtAnnualAllocation.Text).ToString("#.0");
                    switch (Id)
                    {
                        case 1205://Medical
                            AA_AMT01 = Annual; break;
                        case 1215://LTA
                            AA_AMT02 = Annual; break;
                        case 1200://Meal
                            AA_AMT03 = Annual;
                            ////if (Annual == Entitled.ToString("N1").Replace(",", "") || Annual == "0.0" || Annual == "0" || Annual == ".0")
                            if (double.Parse(Annual) <= double.Parse(Entitled.ToString("N1").Replace(",", "")) || Annual == "0.0" || Annual == "0" || Annual == ".0")
                            {
                                mvflag = 0;
                            }
                            else
                            {
                                mvflag = 1;
                            }
                            break;
                        case 1225://Car EMI
                            AA_AMT04 = Annual; break;
                        case 1277://Fuel
                            AA_AMT05 = Annual; break;
                        case 1235://Driver
                            AA_AMT06 = Annual; break;
                        case 1230://Car Insurance
                            AA_AMT07 = Annual; break;
                        case 1255://Mobile
                            AA_AMT08 = Annual; break;
                        case 1260://Car Fuel
                            AA_AMT09 = Annual; break;
                        case 1276://Mobile Purchase
                            AA_AMT10 = Annual; break;
                        case 1280://Car Related Reimbursement
                            AA_AMT11 = Annual; break;
                        case 1245://Education
                            AA_AMT12 = Annual; break;
                        default: break;
                    }
                }
                if (string.IsNullOrEmpty(AnnualAllocationTotal))
                {
                    AnnualAllocationTotal = "0";
                }

                if (mvflag == 0)
                {
                    //if (double.Parse(AnnualAllocationTotal) > 0)
                    //{
                        FbpClaimbo objBo = new FbpClaimbo();
                        Fbp_Claimbl objBl = new Fbp_Claimbl();


                        objBo.PERNR = pernr;
                        objBo.DATE = begda;
                        objBo.AA_AMT01 = AA_AMT01 == null ? "0.0" : AA_AMT01;
                        objBo.AA_AMT02 = AA_AMT02 == null ? "0.0" : AA_AMT02;
                        objBo.AA_AMT03 = AA_AMT03 == null ? "0.0" : AA_AMT03;
                        objBo.AA_AMT04 = AA_AMT04 == null ? "0.0" : AA_AMT04;
                        objBo.AA_AMT05 = AA_AMT05 == null ? "0.0" : AA_AMT05;
                        objBo.AA_AMT06 = AA_AMT06 == null ? "0.0" : AA_AMT06;
                        objBo.AA_AMT07 = AA_AMT07 == null ? "0.0" : AA_AMT07;
                        objBo.AA_AMT08 = AA_AMT08 == null ? "0.0" : AA_AMT08;
                        objBo.AA_AMT09 = AA_AMT09 == null ? "0.0" : AA_AMT09;
                        objBo.AA_AMT10 = AA_AMT10 == null ? "0.0" : AA_AMT10;
                        objBo.AA_AMT11 = AA_AMT11 == null ? "0.0" : AA_AMT11;
                        objBo.AA_AMT12 = AA_AMT12 == null ? "0.0" : AA_AMT12;

                        bool? dd1 = true;
                        int iCnt = 0;
                        objBl.createFbpDeclarebl(objBo, ref dd1);

                        if (dd1 == false)
                        {
                            iCnt = 0;
                            if (iCnt == 0)
                            {
                                lblMessageBoard.Text = "FBP declaration submitted successfully";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                                GetLastUpdatedDate();

                                SendMailMethod();
                            }
                            else
                            {
                                lblMessageBoard.Text = "Unable to submit FBP declaration";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            }
                        }

                        else if (dd1 == true)
                        {
                            iCnt = 1;
                            if (iCnt == 1)
                            {
                                lblMessageBoard.Text = "FBP declaration updated successfully";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                                GetLastUpdatedDate();

                                SendMailMethod();
                            }
                            else
                            {
                                lblMessageBoard.Text = "Unable to update FBP declaration";
                                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    // }
                    //else
                    //{
                    //    lblMessageBoard.Text = "Annual Allocation total should not be Zero or Empty";
                    //    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    //}
                }

                else
                {
                    lblMessageBoard.Text = "Meal voucher can be claimed as <=26400"; //"Meal voucher can be claimed as either 26400 or Zero";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                }
            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void SendMailMethod()
        {
            try
            {
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                //GV_FBPDeclare.HeaderStyle.ForeColor = System.Drawing.Color.White;
                //GV_FBPDeclare.FooterRow.Visible = false;
                GV_FBPDeclare.RenderControl(hw1);


                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;
              
                string EMP_Name = "";
                string EMP_Email = "";
              
                FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();

                objcontext.usp_Fbp_Get_MailList_Fbp_Declare(User.Identity.Name,ref EMP_Name, ref EMP_Email);

                strSubject = "FBP Declaration done by you - "+User.Identity.Name+ " on "+DateTime.Now;

                RecipientsString = EMP_Email;
                strPernr_Mail = "";

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>FBP Declaration done by you - "+User.Identity.Name+ " on "+DateTime.Now+" <br/><br/></b>";
                body += "<b>FBP Claim Details</b>:"+RecipientsString+"<br/>";
                ////body += "<b>FBP Claim Details</b><br/><hr>";
                ////body += "<table><tr><td>FBP Claim ID </td><td> :</td><td> " + fbpidmail + "</td></tr>";
                ////body += "<tr><td>Allowance  </td><td> :</td><td>  " + Entitlement + " - " + allowancetxt + "</td></tr>";
                ////body += "<tr><td>Date     </td><td> :</td><td>  " + bedga + "</td></tr>";
                ////body += "<tr><td>Total Amount </td><td> :</td><td>   " + decimal.Parse(amount).ToString("N2") + "</td></tr>";
                ////body += "<tr><td>Override Amount  </td><td> :</td><td>   " + ovrrideamt + "</td></tr>";
                ////body += "<tr><td>Approved Amount  </td><td> :</td><td>  " + decimal.Parse(Appamt).ToString("N2") + "</td></tr>";
                ////body += "<tr><td>Remarks </td><td> :</td><td>  " + adminremarks + "</td></tr></table> <br/>";
                body += sw1.ToString() + "<br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('FBP Declaration done successfully by you. Error in sending mail');", true);
                return;
            }
        }

        protected void grdFbpDeclaration_RowDataBound(object sender, GridViewRowEventArgs e) 
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Medical").SingleOrDefault()).Visible = true;
                    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "LTA").SingleOrDefault()).Visible = true;
                    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Meal Voucher").SingleOrDefault()).Visible = true;
                    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car EMI").SingleOrDefault()).Visible = true;
                    ////((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Fuel").SingleOrDefault()).Visible = true;
                    ////((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Driver's Salary").SingleOrDefault()).Visible = true;
                    ////((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Insurance & Maintenance").SingleOrDefault()).Visible = true;
                    ////((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Mobile & Telephone Reimbursement").SingleOrDefault()).Visible = true;
                    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Fuel Reimbursment-Self").SingleOrDefault()).Visible = true;
                    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Mobile Purchase").SingleOrDefault()).Visible = true;
                    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Related Reimbursement").SingleOrDefault()).Visible = true;
                    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Education").SingleOrDefault()).Visible = true;


                    if (DataBinder.Eval(e.Row.DataItem, "AA_AMT01").ToString().Equals(""))
                    {
                        ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Medical").SingleOrDefault()).Visible = false;
                       
                    }

                    if (DataBinder.Eval(e.Row.DataItem, "AA_AMT02").ToString().Equals(""))
                    {
                        ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "LTA").SingleOrDefault()).Visible = false;
                    }
                    if (DataBinder.Eval(e.Row.DataItem, "AA_AMT03").ToString().Equals(""))
                    {
                        ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Meal Voucher").SingleOrDefault()).Visible = false;
                        
                    }
                    if (DataBinder.Eval(e.Row.DataItem, "AA_AMT04").ToString().Equals(""))
                    {
                        ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car EMI").SingleOrDefault()).Visible = false;

                    }
                    ////if (DataBinder.Eval(e.Row.DataItem, "AA_AMT05").ToString().Equals(""))
                    ////{
                    ////    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Fuel").SingleOrDefault()).Visible = false;
                       
                    ////}
                    ////if (DataBinder.Eval(e.Row.DataItem, "AA_AMT06").ToString().Equals(""))
                    ////{
                    ////    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Driver's Salary").SingleOrDefault()).Visible = false;
                       
                    ////}
                    ////if (DataBinder.Eval(e.Row.DataItem, "AA_AMT07").ToString().Equals(""))
                    ////{
                    ////    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Insurance & Maintenance").SingleOrDefault()).Visible = false;

                    ////}
                    if (DataBinder.Eval(e.Row.DataItem, "AA_AMT08").ToString().Equals(""))
                    {
                        ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Mobile & Telephone Reimbursement").SingleOrDefault()).Visible = false;

                    }
                    if (DataBinder.Eval(e.Row.DataItem, "AA_AMT09").ToString().Equals(""))
                    {
                        ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Fuel Reimbursment-Self").SingleOrDefault()).Visible = false;

                    }
                    if (DataBinder.Eval(e.Row.DataItem, "AA_AMT10").ToString().Equals(""))
                    {
                        ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Mobile Purchase").SingleOrDefault()).Visible = false;

                    }
                    if (DataBinder.Eval(e.Row.DataItem, "AA_AMT11").ToString().Equals(""))
                    {
                        ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Car Related Reimbursement").SingleOrDefault()).Visible = false;

                    }
                    if (DataBinder.Eval(e.Row.DataItem, "AA_AMT12").ToString().Equals(""))
                    {
                        ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Education").SingleOrDefault()).Visible = false;

                    }

                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void ExportToPDF_Click(object sender, EventArgs e)
        {
            ExportGridToPDF();
        }

        protected void ExportToExcel()
        {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                // Render grid view control.
                htw.WriteBreak();
                grdFbpDeclaration.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                grdFbpDeclaration.RenderControl(htw);
                
                htw.WriteBreak();

                // Write the rendered content to a file.

                //renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                 string renderedGridView = "FBP Declaration Details <br/>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_FBPDeclaration.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();

            
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        private void ExportGridToPDF()
        {
           
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_FBPDeclaration.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                StringWriter s_tw = new StringWriter();
                HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                h_textw.AddStyleAttribute("font-size", "8pt");
                h_textw.AddStyleAttribute("color", "Black");

                string colHeads = "FBP Declaration Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                grdFbpDeclaration.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                grdFbpDeclaration.RenderControl(h_textw);
                h_textw.WriteBreak();

                // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                //iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);
                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 0f); 
                //  Document doc = new Document();
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                doc.Open();
                StringReader s_tr = new StringReader(s_tw.ToString());
                iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                html_worker.Parse(s_tr);
                doc.Close();
                Response.Write(doc);
          
        }
    }
}
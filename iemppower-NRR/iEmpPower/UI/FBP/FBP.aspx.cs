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
    public partial class FBP : System.Web.UI.Page
    {
        int Ebtnclick = 1;
        string EmployeeId = "";
        decimal total = 0;
        decimal BasketTotalAmount = 0;
        bool Claimexists = false;

        ////protected override void OnPreRender(EventArgs e)
        ////{
        ////    // add base.OnPreRender(e); at the beginning of the method.
        ////    base.OnPreRender(e);
        ////    // codes to handle with your controls.
        ////}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["PC"] != null)
                    {
                        if (Request.QueryString["PC"] == "C")
                        {
                            loadTab2();
                            goto displayInfo;
                        }
                        else if (Request.QueryString["PC"] == "H")
                        {
                            PlaceScript.Visible = false;
                            loadTab3();
                            goto displayInfo;
                        }
                    }
                    PlaceScript.Visible = true;
                    Tab1.CssClass = "nav-link active p-2";
                    PlaceScript.Visible = true;
                    MainView.ActiveViewIndex = 0;
                    pageLoadEvents();
                    LoadDeclarationClaims();
                displayInfo: { }

                }
                GetLastUpdatedDate();
                //pageLoadEvents();
                //LoadDeclarationClaims();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "':Page_Load);", true); }
        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            PlaceScript.Visible = true;
            Tab1.CssClass = "nav-link active p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link p-2";
            ////Tab4.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 0;
            ////LoadTravelClaimGridView();
            //GetHeadsofAllowances();

            //Newly added
            pageLoadEvents();
            LoadDeclarationClaims();
            //CalculateTotal();
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            loadTab2();
        }

        void loadTab2()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link active p-2";
            Tab3.CssClass = "nav-link p-2";
            ////Tab4.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 1;
            //LoadPRRequestGridView();
            //LoadIExpenseMPGridView();
            Loadgrd_CalimsItems();

        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            loadTab3();
            PlaceScript.Visible = false;
        }

        void loadTab3()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link active p-2";
            MainView.ActiveViewIndex = 2;
            //LoadPRRequestCompletedGridView();
            //LoadIExpenseMCGridView();
            Loadgrd_CalimsHistory();
        }

        //public void CalculateTotal()
        //{
        //    decimal sum = 0; string curr = "";
        //    grd_ItemInfo.FooterRow.Cells[4] = HorizontalAlign.Right;
        //    foreach (GridViewRow row in grd_ItemInfo.Rows)
        //    {
        //        curr = grd_ItemInfo.DataKeys[row.RowIndex].Values["WAERS"].ToString();
        //        sum += (Convert.ToDecimal(grd_ItemInfo.DataKeys[row.RowIndex].Values["NO_OF_UNITS"].ToString()) * Convert.ToDecimal(grd_ItemInfo.DataKeys[row.RowIndex].Values["UNIT_PRICE"].ToString()));
        //    }
        //    grd_ItemInfo.FooterRow.Cells[8].Text = sum.ToString() + " (" + curr + ")";
        //    ltTotalAmount.Text = sum.ToString() + " (" + curr + ")";
        //}

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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "':Loadgrd_CalimsHistory);", true); }
        }

        private void Loadgrd_CalimsHistory_AllCurrentLastmonth(string month)
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                string ApproverId = User.Identity.Name;
                fbpboObj1 = fbpblObj.Load_fbpclaims_History_AllCurrentLastmonth(ApproverId, month);
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "':Loadgrd_CalimsHistory_AllCurrentLastmonth);", true); }
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "':LoadDeclarationClaims);", true); }
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
                    ////GetPlans();
                    GetBasketTotal();
                    GetHeadsofAllowances();
                    GetFinancialDates();
                    getVisibility();

                    btnSubmit.Visible = btnCancel.Visible = btnApplyView.Visible = true;

                }
                else if (fbplock == true)
                {
                    divview.Visible = false;
                    MsgCls("FBP Declaration has been locked. Please contact Payroll Admin.", lblMessageBoard, Color.Red);
                    btnSubmit.Visible = btnCancel.Visible = false;// = btnApplyView.Visible = false;
                }

                GetPlans();

                LoadasOnDate();
                Loadgrd_CalimsItems();


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "':pageLoadEvents());", true); }
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "':GetFinancialDates)", true); }
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
                    DateTime exitdate = DateTime.ParseExact(objBo.EXIT, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
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
                    lblPayDate.Text = (objBo.EXIT == "31-Dec-9999") ? "" : objBo.EXIT;//lblPlanDate.Text;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "':GetPlans);", true); }
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
                    lblBasketTotalAmount2.Text = Convert.ToDecimal(objBo.BASKETTOTAL).ToString("N2");
                    BasketTotalAmount = Convert.ToDecimal(lblBasketTotalAmount.Text);
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "':GetBasketTotal);", true); }
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
                //Newly added Starts
                foreach (GridViewRow row in GV_FBPDeclare.Rows)
                {
                    GV_FBPDeclare.Columns[0].Visible = true;
                }
                //Newly added Ends
                GV_FBPDeclare.DataBind();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "':GetHeadsofAllowances)", true); }
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "':GetLastUpdatedDate);", true); }
        }

        public void getVisibility()
        {
            foreach (GridViewRow row in GV_FBPDeclare.Rows)
            {
                GV_FBPDeclare.Columns[0].Visible = true;
                int Id = int.Parse(row.Cells[0].Text);
                GV_FBPDeclare.Columns[0].Visible = false;
                double Entitled = double.Parse(row.Cells[2].Text.Replace(",", ""));
                TextBox txtAnnualAllocation = row.Cells[4].FindControl("txtAnnualAllocation") as TextBox;
                ////Label lblAnnualAllocation = row.Cells[4].FindControl("lblAnnualAllocation") as Label;
                RangeValidator RVtxtAnnualAllocation = row.Cells[5].FindControl("RVtxtAnnualAllocation") as RangeValidator;
                CompareValidator CVClaimexists = row.Cells[5].FindControl("CVClaimexists") as CompareValidator;
                switch (Id)
                {
                    case 1225://Car EMI
                        ////txtAnnualAllocation.Visible = false;
                        ////lblAnnualAllocation.Visible = true;
                        ////lblAnnualAllocation.Text = txtAnnualAllocation.Text;
                        txtAnnualAllocation.Enabled = false;
                        RVtxtAnnualAllocation.Enabled = false;
                        break;

                    case 1215://LTA

                        int LTAcount = GetLTAMobPurCount("1215");
                        if (LTAcount > 0)
                        {
                            //txtAnnualAllocation.Visible = false;
                            //lblAnnualAllocation.Visible = true;
                            //lblAnnualAllocation.Text = txtAnnualAllocation.Text;
                            txtAnnualAllocation.Enabled = false;
                        }
                        else
                        {
                            txtAnnualAllocation.Visible = true;
                            ////lblAnnualAllocation.Visible = false;
                        }
                        break;

                    case 1276://Mobile Purchase
                        int MPcount = GetLTAMobPurCount("1276");
                        if (MPcount > 0)
                        {
                            //txtAnnualAllocation.Visible = false;
                            //lblAnnualAllocation.Visible = true;
                            //lblAnnualAllocation.Text = txtAnnualAllocation.Text;
                            txtAnnualAllocation.Enabled = false;
                        }
                        else
                        {
                            txtAnnualAllocation.Visible = true;
                            ////lblAnnualAllocation.Visible = false;
                        }
                        break;

                    case 1245://Education(Training allowance)
                        int Edcount = GetLTAMobPurCount("1245");
                        if (Edcount > 0)
                        {
                            //txtAnnualAllocation.Visible = false;
                            //lblAnnualAllocation.Visible = true;
                            //lblAnnualAllocation.Text = txtAnnualAllocation.Text;
                            txtAnnualAllocation.Enabled = false;
                        }
                        else
                        {
                            txtAnnualAllocation.Visible = true;
                            ////lblAnnualAllocation.Visible = false;
                        }
                        break;

                    //The FBP declaration amount cannot be updated as zero when there are claims raised.- STARTS
                    //case 1255://Mobile & Telephone
                    //    int MTcount = GetLTAMobPurCount("1255");
                    //    if (MTcount > 0)
                    //    {
                    //        if (Convert.ToDecimal(txtAnnualAllocation.Text) == 0)
                    //            CVClaimexists.Enabled = true;
                    //    }
                    //    else
                    //    {
                    //        CVClaimexists.Enabled = false;
                    //    }
                    //    break;

                    //case 1260://Car Fuel Reimburse Self
                    //    int CFRScount = GetLTAMobPurCount("1260");
                    //    if (CFRScount > 0)
                    //    {
                    //        CVClaimexists.Enabled = true;
                    //    }
                    //    else
                    //    {
                    //        CVClaimexists.Enabled = false;
                    //    }
                    //    break;

                    //case 1280://Car Rel Reimbursement
                    //    int CRRcount = GetLTAMobPurCount("1280");
                    //    if (CRRcount > 0)
                    //    {
                    //        CVClaimexists.Enabled = true;
                    //    }
                    //    else
                    //    {
                    //        CVClaimexists.Enabled = false;
                    //    }
                    //    break;
                    //The FBP declaration amount cannot be updated as zero when there are claims raised.- ENDS
                    default:
                        txtAnnualAllocation.Visible = true;
                        ////lblAnnualAllocation.Visible = false;
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

        public decimal GetClaimTotal(string LGART)
        {
            int count = 0;
            decimal TotalClaimamt = 0;
            //try
            //{               
            Fbp_Claimbl objBl = new Fbp_Claimbl();
            FbpClaimscollectionbo objLst = objBl.GetClaimTotal(User.Identity.Name, LGART);
            foreach (FbpClaimbo objBo in objLst)
            {
                count = objBo.COUNT;
                TotalClaimamt = !string.IsNullOrEmpty(objBo.TotalClaimamt) ? Convert.ToDecimal(objBo.TotalClaimamt) : 0;
            }
            return TotalClaimamt;
            //}
            //catch (Exception Ex)
            //{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadasOnDate()
        {
            try
            {
                //   MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                DateTime now = DateTime.Now;
                DateTime startDate = new DateTime(now.Year, now.Month, 1);
                LblDate.Text = startDate.ToString("MMM yyyy");
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "':LoadasOnDate);", true); }
        }
        private void Loadgrd_CalimsItems()
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                string ApproverId = User.Identity.Name;
                fbpboObj1 = fbpblObj.Load_FbpClaim_Details(ApproverId.Trim(), "0");
                Session.Add("FbpGrdInfo", fbpboObj1);

                if (fbpboObj1 == null || fbpboObj1.Count == 0)
                {
                    MsgCls("Please declare the FBP amount before claiming !", lblmsg, Color.Red);
                    grd_CalimsItems.Visible = false;
                    grd_CalimsItems.DataSource = null;
                    grd_CalimsItems.DataBind();
                    ////btnApplyView.Visible = false;
                    return;
                }
                else
                {
                    grd_CalimsItems.Visible = true;
                    grd_CalimsItems.DataSource = fbpboObj1;
                    grd_CalimsItems.SelectedIndex = -1;
                    grd_CalimsItems.DataBind();
                    ////btnApplyView.Visible = true;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "':Loadgrd_CalimsItems)", true); }
        }

        protected void btnApplyView_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("FBP_Apply_ViewClaimsNew.aspx");
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
            Response.Redirect("~/UI/Default.aspx");
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
                string AA_AMT08 = ""; int mvflag8 = 0; decimal Claimamt8 = 0;
                string AA_AMT09 = ""; int mvflag9 = 0; decimal Claimamt9 = 0;
                string AA_AMT10 = "";
                string AA_AMT11 = ""; int mvflag11 = 0; decimal Claimamt11 = 0;
                string AA_AMT12 = "";


                RangeValidator RVtxtAnnualAllocation = new RangeValidator();
                CompareValidator CVClaimexists = new CompareValidator();


                HiddenField HFAllocationTotal = (HiddenField)GV_FBPDeclare.FooterRow.FindControl("HFAllocationTotal");
                string AnnualAllocationTotal = HFAllocationTotal.Value;




                decimal BasketTotal = Convert.ToDecimal(lblBasketTotalAmount.Text);
                ////decimal AnnualTotal = Convert.ToDecimal(AnnualAllocationTotal);
                decimal AnnualTotal = AnnualAllocationTotal == "" ? 0 : Convert.ToDecimal(AnnualAllocationTotal);


                Label lblAnnualAllocationTotal = (Label)GV_FBPDeclare.FooterRow.FindControl("lblAnnualAllocationTotal");

                ////lblAnnualAllocationTotal.Text = Convert.ToDecimal(AnnualAllocationTotal).ToString("N2");
                lblAnnualAllocationTotal.Text = AnnualAllocationTotal == "" ? "0" : Convert.ToDecimal(AnnualAllocationTotal).ToString("N2");

                Label lblFBPSpecialAllowance = (Label)GV_FBPDeclare.FooterRow.FindControl("lblFBPSpecialAllowance");
                lblFBPSpecialAllowance.Text = (BasketTotal - AnnualTotal).ToString("N2");
                if (lblFBPSpecialAllowance.Text.StartsWith("-"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Special Allowance cannot be negative');", true);
                    lblMessageBoard.Text = "Special Allowance cannot be negative";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                }

                else
                {



                    //---------------------------------------------------------------
                    bool exitLoop = false;
                    foreach (GridViewRow row in GV_FBPDeclare.Rows)
                    {
                        GV_FBPDeclare.Columns[0].Visible = true;
                        int Id = int.Parse(row.Cells[0].Text);
                        GV_FBPDeclare.Columns[0].Visible = false;
                        double Entitled = double.Parse(row.Cells[2].Text.Replace(",", ""));
                        TextBox txtAnnualAllocation = row.Cells[4].FindControl("txtAnnualAllocation") as TextBox;
                        RVtxtAnnualAllocation = row.Cells[5].FindControl("RVtxtAnnualAllocation") as RangeValidator;
                        CVClaimexists = row.Cells[5].FindControl("CVClaimexists") as CompareValidator;
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

                                decimal MTcount = GetClaimTotal("1255");
                                if (MTcount > 0)
                                {
                                    if (Convert.ToDecimal(txtAnnualAllocation.Text) < MTcount)// if (Convert.ToDecimal(txtAnnualAllocation.Text) == 0)
                                    {
                                        CVClaimexists.Enabled = true;
                                        exitLoop = true;
                                        mvflag8 = 1;
                                        Claimamt8 = MTcount;


                                        CVClaimexists.ValueToCompare = Claimamt8.ToString();
                                        CVClaimexists.ErrorMessage = "Mobile & Telephone Reimbursement - cannot be < " + Claimamt8 + " claim amount";
                                    }
                                    else
                                    {
                                        CVClaimexists.Enabled = false;
                                        AA_AMT08 = Annual;
                                        mvflag8 = 0;
                                    }
                                }
                                else
                                {
                                    CVClaimexists.Enabled = false;
                                    AA_AMT08 = Annual;
                                    mvflag8 = 0;
                                }



                                //AA_AMT08 = Annual; 



                                break;
                            case 1260://Car Fuel

                                decimal CFRScount = GetClaimTotal("1260");
                                if (CFRScount > 0)
                                {
                                    if (Convert.ToDecimal(txtAnnualAllocation.Text) < CFRScount)//if (Convert.ToDecimal(txtAnnualAllocation.Text) == 0)
                                    {
                                        CVClaimexists.Enabled = true;
                                        exitLoop = true;
                                        mvflag9 = 1;
                                        Claimamt9 = CFRScount;
                                    }
                                    else
                                    {
                                        CVClaimexists.Enabled = false;
                                        AA_AMT09 = Annual;
                                        mvflag9 = 0;
                                    }
                                }
                                else
                                {
                                    CVClaimexists.Enabled = false;
                                    AA_AMT09 = Annual;
                                    mvflag9 = 0;
                                }



                                //AA_AMT09 = Annual; 


                                break;
                            case 1276://Mobile Purchase
                                AA_AMT10 = Annual; break;
                            case 1280://Car Related Reimbursement



                                decimal CRRcount = GetClaimTotal("1280");
                                if (CRRcount > 0)
                                {
                                    if (Convert.ToDecimal(txtAnnualAllocation.Text) < CRRcount)//if (Convert.ToDecimal(txtAnnualAllocation.Text) == 0)
                                    {
                                        CVClaimexists.Enabled = true;
                                        exitLoop = true;
                                        mvflag11 = 1;
                                        Claimamt11 = CRRcount;


                                    }
                                    else
                                    {
                                        CVClaimexists.Enabled = false;
                                        AA_AMT11 = Annual;
                                        mvflag11 = 0;
                                    }
                                }
                                else
                                {
                                    CVClaimexists.Enabled = false;
                                    AA_AMT11 = Annual;
                                    mvflag11 = 0;
                                }
                                //AA_AMT11 = Annual; 



                                break;
                            case 1245://Education
                                AA_AMT12 = Annual; break;
                            default: break;
                        }
                        if (exitLoop) break;
                    }
                    if (string.IsNullOrEmpty(AnnualAllocationTotal))
                    {
                        AnnualAllocationTotal = "0";
                    }

                    if (!exitLoop)
                    {
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
                            if (ViewState["custm1"].ToString() == "true")
                            {

                                objBl.createFbpDeclarebl(objBo, ref dd1);

                                if (dd1 == false)
                                {
                                    iCnt = 0;
                                    if (iCnt == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('FBP declaration submitted successfully');", true);
                                        lblMessageBoard.Text = "Declaration submitted successfully";
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                                        GetLastUpdatedDate();

                                        SendMailMethod();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Unable to submit FBP declaration');", true);
                                        lblMessageBoard.Text = "Unable to submit FBP declaration";
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                    }
                                }

                                else if (dd1 == true)
                                {
                                    iCnt = 1;
                                    if (iCnt == 1)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Declaration updated successfully');", true);
                                        lblMessageBoard.Text = "Declaration updated successfully";
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                                        GetLastUpdatedDate();

                                        SendMailMethod();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Unable to update FBP declaration');", true);
                                        lblMessageBoard.Text = "Unable to update FBP declaration";
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                    }
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please Add details before Submitting!');", true);
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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Meal voucher can be claimed as <=26400');", true);
                            lblMessageBoard.Text = "Meal voucher can be claimed as <=26400"; //"Meal voucher can be claimed as either 26400 or Zero";
                            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        }
                    }

                    //---------------------------------------------------------------

                    if (mvflag8 == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "Mobile & Telephone Reimbursement - cannot be < " + Claimamt8 + " claim amount", true);
                        lblMessageBoard.Text = "Mobile & Telephone Reimbursement - cannot be < " + Claimamt8 + " claim amount";//zero when there are claims exists"; //"Meal voucher can be claimed as either 26400 or Zero";
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    }
                    if (mvflag9 == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "Car Fuel Reimbursment Self - cannot be < " + Claimamt9 + " claim amount", true);
                        lblMessageBoard.Text = "Car Fuel Reimbursment Self - cannot be < " + Claimamt9 + " claim amount";//zero when there are claims exists"; //"Meal voucher can be claimed as either 26400 or Zero";
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    }
                    if (mvflag11 == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "Car Related Reimbursement - cannot be < " + Claimamt11 + " claim amount", true);
                        lblMessageBoard.Text = "Car Related Reimbursement - cannot be < " + Claimamt11 + " claim amount";//zero when there are claims exists"; //"Meal voucher can be claimed as either 26400 or Zero";
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    }




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
                ////GV_FBPDeclare.Columns[5].Visible = false;
                GV_FBPDeclare.Columns[6].Visible = false;

                //-------------------------------------------------------------------------------------------
                //// DataTable dt = new DataTable();
                ////// dt.Columns.Add("userid", typeof(int));
                //// dt.Columns.Add("Heads of Allowances", typeof(string));
                //// dt.Columns.Add("Annual Entitlement	", typeof(string));
                //// dt.Columns.Add("Monthly Allocation	", typeof(string));
                //// dt.Columns.Add("Annual Allocation", typeof(string));
                //// foreach (GridViewRow row in GV_FBPDeclare.Rows)
                //// {
                ////     //int userid = int.Parse(row.Cells[0].Text);
                ////     string txtAnnualAllocation = (TextBox)GV_FBPDeclare.Rows[v].Cells[0].FindControl("txtAnnualAllocation");
                ////     string HeadsofAllowances = row.Cells[1].Text;
                ////     string AnnualEntitlement = row.Cells[2].Text;
                ////     string MonthlyAllocation = row.Cells[3].Text;
                ////     string AnnualAllocation = row.Cells[4].Text;
                ////     dt.Rows.Add(HeadsofAllowances, AnnualEntitlement, MonthlyAllocation, AnnualAllocation);
                //// }



                ////DataTable dt = new DataTable();
                ////for (int i = 0; i < GV_FBPDeclare.Columns.Count; i++)
                ////{
                ////    dt.Columns.Add("column" + i.ToString());
                ////}
                ////foreach (GridViewRow row in GV_FBPDeclare.Rows)
                ////{
                ////    DataRow dr = dt.NewRow();
                ////    for (int j = 0; j < GV_FBPDeclare.Columns.Count; j++)
                ////    {
                ////        dr["column" + j.ToString()] = row.Cells[j].Text;
                ////    }

                ////    dt.Rows.Add(dr);
                ////}

                ////DataGrid dg = new DataGrid();
                ////dg.DataSource = dt;
                ////dg.DataBind();

                ////dg.RenderControl(hw1);

                ////for (int i = 0; i < GV_FBPDeclare.Rows.Count; i++)
                ////{
                ////    AjaxControlToolkit.FilteredTextBoxExtender FilteredTextBoxExtender16 = (AjaxControlToolkit.FilteredTextBoxExtender)GV_FBPDeclare.Rows[i].FindControl("FilteredTextBoxExtender16");
                ////    FilteredTextBoxExtender16.Enabled = false;
                ////}

                //-------------------------------------------------------------------------------------------

                GV_FBPDeclare.RenderControl(hw1);


                ////for (int i = 0; i < GV_FBPDeclare.Rows.Count; i++)
                ////{
                ////    AjaxControlToolkit.FilteredTextBoxExtender FilteredTextBoxExtender16 = (AjaxControlToolkit.FilteredTextBoxExtender)GV_FBPDeclare.Rows[i].FindControl("FilteredTextBoxExtender16");
                ////    FilteredTextBoxExtender16.Enabled = true;
                ////}

                GV_FBPDeclare.Columns[5].Visible = true;
                GV_FBPDeclare.Columns[6].Visible = true;


                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string EMP_Name = "";
                string EMP_Email = "";

                FbpClaimsdalDataContext objcontext = new FbpClaimsdalDataContext();

                objcontext.usp_Fbp_Get_MailList_Fbp_Declare(User.Identity.Name, ref EMP_Name, ref EMP_Email);

                strSubject = "FBP Declaration done by you - " + User.Identity.Name + " on " + DateTime.Now;

                RecipientsString = "karthik.k@itchamps.com"; ////EMP_Email;
                strPernr_Mail = "";

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>FBP Declaration done by you - " + User.Identity.Name + " on " + DateTime.Now + " <br/><br/></b>";
                body += "<b>To Email id</b>:" + RecipientsString + "<br/>";

                body += "<b>Flexible Benefit Plan Declaration:April 1st " + LblFromDate.Text + " To March 31st " + LblToDate.Text + "</b><br/><br/>";
                body += "<b>Plan</b>:" + lblPlanDate.Text + "</b><br/>";
                body += "<b>Basket Total</b>:" + lblBasketTotalAmount.Text + "</b><br/><br/>";
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
            ////catch
            ////{
            ////    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('FBP Declaration done successfully by you. Error in sending mail');", true);
            ////    return;
            ////}
            catch (Exception Ex)
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
                    ////((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Medical").SingleOrDefault()).Visible = true;
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


                    ////if (DataBinder.Eval(e.Row.DataItem, "AA_AMT01").ToString().Equals(""))
                    ////{
                    ////    ((DataControlField)grdFbpDeclaration.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Medical").SingleOrDefault()).Visible = false;

                    ////}

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

        protected void grd_FbpClaims_History_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int pageindex = e.NewPageIndex;
                grd_FbpClaims_History.PageIndex = e.NewPageIndex;
                Loadgrd_CalimsHistory();
                //search();
                grd_FbpClaims_History.SelectedIndex = -1;
                //divitems.Visible = false;
                //viewcheck.Value = "NO";
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
                        //viewcheck.Value = "YES";
                        //divitems.Visible = true;
                        MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        //foreach (GridViewRow row in grd_FbpClaims_History.Rows)
                        //{
                        //    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                        //    System.Drawing.Color.LightGray :
                        //    System.Drawing.Color.White;
                        //}
                        Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                        FbpClaimbo fbpboObj = new FbpClaimbo();
                        List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();

                        //ViewState["lgart"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["LGART"].ToString().Trim();

                        int fbpid = int.Parse(grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString().Trim());


                        Session["fbpid"] = fbpid;

                        //Newly added starts
                        Session["fbp_lgart"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["LGART"].ToString().Trim();
                        Session["fbp_createdOn"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString().Trim();
                        Session["ALLOWANCETEXT"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["ALLOWANCETEXT"].ToString().Trim();

                        //Newly added ends
                        Response.Redirect("FBP_PendingClaims.aspx?NC=" + "C");

                        //ViewState["FBPID"] = int.Parse(grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["FBPC_IC"].ToString().Trim());
                        //ViewState["status"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString().Trim();

                        //ViewState["BETRG"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["BETRG"].ToString().Trim();
                        //ViewState["ALLOWANCETEXT"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["ALLOWANCETEXT"].ToString().Trim();
                        //ViewState["OVERRIDE_AMT"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["OVERRIDE_AMT"].ToString().Trim();
                        //ViewState["CREATED_ON"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString().Trim();
                        //ViewState["APPROVEDON"] = grd_FbpClaims_History.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVEDON"].ToString().Trim();

                        //Loadgrd_CalimsItems();




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




                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void grd_FbpClaims_History_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                Loadgrd_CalimsHistory();
                //search();
                //viewcheck.Value = "NO";
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
                //divitems.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void ddlPagesizeEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            grd_FbpClaims_History.PageSize = Convert.ToInt32(ddlPagesizeEmp.SelectedValue);
            LoadFBPEmpGrid();
        }

        void LoadFBPEmpGrid()
        {
            if (Ebtnclick == 1)
            {
                btnAll.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                Loadgrd_CalimsHistory_AllCurrentLastmonth("all");
            }

            else if (Ebtnclick == 2)
            {
                btnAll.CssClass = "btn btn-xs btn-light";
                btnCurrentMonth.CssClass = "btn btn-xs btn-secondary";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                Loadgrd_CalimsHistory_AllCurrentLastmonth("current");
            }

            else if (Ebtnclick == 3)
            {
                btnAll.CssClass = "btn btn-xs btn-light";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-secondary";

                Loadgrd_CalimsHistory_AllCurrentLastmonth("last");
            }
            else
            {
                btnAll.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                Loadgrd_CalimsHistory_AllCurrentLastmonth("all");
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            Ebtnclick = 1;
            LoadFBPEmpGrid();
        }

        protected void btnCurrentMonth_Click(object sender, EventArgs e)
        {
            Ebtnclick = 2;
            LoadFBPEmpGrid();
        }

        protected void btnLastMonth_Click(object sender, EventArgs e)
        {
            Ebtnclick = 3;
            LoadFBPEmpGrid();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    LoadFBPEmpGrid();
                }
                else
                {
                    searchFBP();
                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, Color.Red);
            }
        }

        public void searchFBP()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                string SelectedType = "1";//ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;
                DateTime createdon = DateTime.Parse("01/01/0001");//DateTime.Parse(string.IsNullOrEmpty(txtCreatedOn.Text) ? "01/01/0001" : txtCreatedOn.Text);

                //if (SelectedType != "0" && textSearch == "")
                //{
                //    MsgCls("Please Enter the Text", LblMsg, Color.Red);
                //}

                //else if (SelectedType == "0" && textSearch != "")
                //{
                //    MsgCls("Please Select the Type", LblMsg, Color.Red);
                //}
                //else
                {
                    Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                    List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                    List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                    EmployeeId = User.Identity.Name;

                    fbpboObj1 = fbpblObj.Load_Particular_FbpClaimsDetails_EMP(User.Identity.Name, SelectedType, textSearch);

                    if (fbpboObj1 == null || fbpboObj1.Count == 0)
                    {
                        MsgCls("No Records found", lblmsg, System.Drawing.Color.Red);
                        grd_FbpClaims_History.Visible = false;
                        grd_FbpClaims_History.DataSource = null;
                        grd_FbpClaims_History.DataBind();

                        return;
                    }
                    else
                    {
                        MsgCls("", lblmsg, System.Drawing.Color.Transparent);
                        grd_FbpClaims_History.Visible = true;
                        grd_FbpClaims_History.DataSource = fbpboObj1;
                        grd_FbpClaims_History.SelectedIndex = -1;
                        grd_FbpClaims_History.DataBind();

                        Session.Add("FbpGrdInfo", fbpboObj1);

                    }
                }

            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, Color.Red);
            }

        }

        protected void GV_FBPDeclare_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                ViewState["custm1"] = "true";



                TextBox Amount = (TextBox)e.Row.FindControl("txtAnnualAllocation");



                decimal Presents = Convert.ToDecimal(Amount.Text);
                total += Presents;




                //--------------------------------------------------------------------------------
                FbpClaimsdalDataContext objFBPDataContext = new FbpClaimsdalDataContext();
                HyperLink hyp_redirct = (HyperLink)e.Row.FindControl("hyp_redirct");
                RangeValidator rgtxtAnnualAllocation = (RangeValidator)e.Row.FindControl("rgtxtAnnualAllocation");
                CustomValidator custmtxtAnnualAllocation = (CustomValidator)e.Row.FindControl("custmtxtAnnualAllocation");
                Label lblError = e.Row.FindControl("lblError") as Label;
                rgtxtAnnualAllocation.MaximumValue = e.Row.Cells[2].Text;
                int id = Convert.ToInt32(GV_FBPDeclare.DataKeys[e.Row.RowIndex].Values[0]);
                if (id == 1200) { rgtxtAnnualAllocation.Enabled = false; } //Meal Voucher
                else if (id == 1215) { hyp_redirct.NavigateUrl = ""; rgtxtAnnualAllocation.Enabled = false; } //LTA
                else if (id == 1225) { hyp_redirct.NavigateUrl = ""; rgtxtAnnualAllocation.Enabled = false; } //Car EMI
                else if (id == 1245) { hyp_redirct.NavigateUrl = ""; rgtxtAnnualAllocation.Enabled = false; } //Education
                else if (id == 1255)
                {
                    rgtxtAnnualAllocation.Enabled = true;
                    hyp_redirct.Visible = true;
                    bool? Status = false;
                    hyp_redirct.NavigateUrl = "~/UI/Personal_Information/communication_information.aspx";
                    hyp_redirct.ToolTip = "Add Mobile Number's";
                    objFBPDataContext.usp_FBP_Check_declaration_val(User.Identity.Name.Trim(), 1255, ref Status);
                    if (Presents > 0)
                    {
                        if (Status == false)
                        {
                            lblError.Text = "Please Add Mobile Numbers";
                            lblError.Visible = true;
                            ViewState["custm1"] = "false";
                            //custmtxtAnnualAllocation.ErrorMessage = "Please Add Mobile Numbers";
                        }
                        else
                        {
                            lblError.Text = "";
                            lblError.Visible = false;
                            ViewState["custm1"] = "true";
                        }
                    }
                } //Mobile & Telephone Reimbursement
                else if (id == 1260)
                {
                    rgtxtAnnualAllocation.Enabled = true;
                    hyp_redirct.Visible = true;
                    hyp_redirct.NavigateUrl = "~/UI/Personal_Information/personal_ids.aspx";
                    hyp_redirct.ToolTip = "Add Car RC Details";



                    int? cc = 0;
                    string file = "", rc = "", n = "";
                    DateTime? dt = DateTime.MinValue;



                    //    if (Presents > 0)
                    //    {
                    //        bool? Status = false;
                    //        pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
                    //        pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                    //        objPersonalIDsBo.PERNR = User.Identity.Name.Trim();
                    //        objPersonalIDsBo.CC = 0;
                    //        objPersonalIDsBo.FPDAT = DateTime.MinValue;
                    //        objPersonalIDsBo.RCNO = "";
                    //        objPersonalIDsBo.RCNAME = "";
                    //        objPersonalIDsBl.Create_PersonalIDs_car(objPersonalIDsBo, 3, ref Status, ref cc, ref dt, ref file, ref rc, ref n);
                    //        rgtxtAnnualAllocation.MaximumValue = cc == 1 ? "21601" : "28801";
                    //        n = cc == 1 ? "21600 for <=1800 CC Car " : ">28800 for >1800 CC Car";
                    //        rgtxtAnnualAllocation.ErrorMessage = "Max eligibility is " + n;

                    //        if (Status == false)
                    //        {
                    //            lblError.Text = "Please Add Car CC";
                    //            lblError.Visible = true;
                    //            ViewState["custm1"] = "false";
                    //            //custmtxtAnnualAllocation.ErrorMessage = "Please Add Mobile Numbers";
                    //        }
                    //        else
                    //        {
                    //            lblError.Text = "";
                    //            lblError.Visible = false;
                    //            ViewState["custm1"] = "true";
                    //        }

                    //    }





                    //} //Car Fuel Reimbursment-Self
                    //else if (id == 1276) { hyp_redirct.NavigateUrl = ""; rgtxtAnnualAllocation.Enabled = false; } //Mobile Purchase
                    //else if (id == 1280) { hyp_redirct.NavigateUrl = ""; rgtxtAnnualAllocation.Enabled = false; } //Car Related Reimbursement



                    ////--------------------------------------------------------------------------------



                }



                if (e.Row.RowType == DataControlRowType.Footer)
                {



                    Label lblAnnualAllocationTotal = (Label)e.Row.FindControl("lblAnnualAllocationTotal");
                    Label lblFBPSpecialAllowance = (Label)e.Row.FindControl("lblFBPSpecialAllowance");
                    lblAnnualAllocationTotal.Text = total.ToString("N2");
                    BasketTotalAmount = Convert.ToDecimal(lblBasketTotalAmount.Text);
                    lblFBPSpecialAllowance.Text = (BasketTotalAmount - total).ToString("N2");



                }
                foreach (GridViewRow row in GV_FBPDeclare.Rows)
                {
                    TextBox txtAnnualAllocation = row.Cells[4].FindControl("txtAnnualAllocation") as TextBox;
                    if (txtAnnualAllocation.Text == "" || txtAnnualAllocation.Text == null || txtAnnualAllocation.Text == ".0")
                        txtAnnualAllocation.Text = "0.0";
                }
            }
        }

        protected void txtAnnualAllocation_TextChanged(object sender, EventArgs e)
        {
            TextBox TextBox1 = sender as TextBox;
            GridViewRow gvr = TextBox1.NamingContainer as GridViewRow;



            int Id = int.Parse(gvr.Cells[0].Text);
            double Entitled = double.Parse(gvr.Cells[2].Text.Replace(",", ""));
            RangeValidator RVtxtAnnualAllocation = gvr.FindControl("RVtxtAnnualAllocation") as RangeValidator;
            FbpClaimsdalDataContext objFBPDataContext = new FbpClaimsdalDataContext();
            CustomValidator custmtxtAnnualAllocation = gvr.FindControl("custmtxtAnnualAllocation") as CustomValidator;
            TextBox txtAnnualAllocation1 = gvr.FindControl("txtAnnualAllocation") as TextBox;
            Label lblError = gvr.FindControl("lblError") as Label;
            RangeValidator rgtxtAnnualAllocation = gvr.FindControl("rgtxtAnnualAllocation") as RangeValidator;
            bool? Status = false;

            txtAnnualAllocation1.Text = txtAnnualAllocation1.Text.Trim() == "" ? "0.0" : txtAnnualAllocation1.Text.Trim();

            if (double.Parse(txtAnnualAllocation1.Text.Trim()) > 0.0)
            {
                switch (Id)
                {
                    case 1255:
                        rgtxtAnnualAllocation.Enabled = true;
                        objFBPDataContext.usp_FBP_Check_declaration_val(User.Identity.Name.Trim(), 1255, ref Status);
                        if (Status == false)
                        {
                            ViewState["custm1"] = "false";
                            lblError.Text = "Please Add Mobile Numbers";
                            lblError.Visible = true;
                            //custmtxtAnnualAllocation.ErrorMessage = "Please Add Mobile Numbers";
                        }
                        else
                        {
                            ViewState["custm1"] = "true";
                            lblError.Text = "";
                            lblError.Visible = false;
                        }
                        break;
                    case 1260:
                        rgtxtAnnualAllocation.Enabled = true;
                        int? cc = 0;
                        string file = "", rc = "", n = "";
                        DateTime? dt = DateTime.MinValue;
                        bool? Status1 = false;
                        pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
                        pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                        objPersonalIDsBo.PERNR = User.Identity.Name.Trim();
                        objPersonalIDsBo.CC = 0;
                        objPersonalIDsBo.FPDAT = DateTime.MinValue;
                        objPersonalIDsBo.RCNO = "";
                        objPersonalIDsBo.RCNAME = "";
                        objPersonalIDsBl.Create_PersonalIDs_car(objPersonalIDsBo, 3, ref Status1, ref cc, ref dt, ref file, ref rc, ref n);
                        rgtxtAnnualAllocation.MaximumValue = cc == 1 ? "21601" : "28801";
                        n = cc == 1 ? "21600 for <=1800 CC Car " : ">28800 for >1800 CC Car";
                        rgtxtAnnualAllocation.ErrorMessage = "Max eligibility is " + n;
                        if (Status1 == false)
                        {
                            lblError.Text = "Please Add Car CC";
                            lblError.Visible = true;
                            ViewState["custm1"] = "false";
                            //custmtxtAnnualAllocation.ErrorMessage = "Please Add Mobile Numbers";
                        }
                        else
                        {
                            lblError.Text = "";
                            lblError.Visible = false;
                            ViewState["custm1"] = "true";
                        }
                        break;
                }
            }
            else
            {
                ViewState["custm1"] = "true";
                lblError.Text = "";
                lblError.Visible = false;
            }
        }

        protected void custmtxtAnnualAllocation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //args.IsValid = ViewState["custm1"].ToString().Trim() == "false" ? false : true;
        }


    }
}
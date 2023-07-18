using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;

namespace iEmpPower.UI.Local_requisition
{
    public partial class Expense_statement : System.Web.UI.Page
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            if (!IsPostBack)
            {
                lblApprovedOn.Text = Convert.ToString(DateTime.Now);
             //   LoadDomesticGridViewFirstRow();
                
                //var xmlFilePath = Server.MapPath("~/UI/Benefits_Payment/XMLFile.xml");//Location of the XML file.
                //DataSet dsCameraDetails = new DataSet();
                //dsCameraDetails.ReadXml(xmlFilePath);// Load XML in dataset
                //GridView1.DataSource = dsCameraDetails.Tables[0].DefaultView;
                //GridView1.DataBind();// Bind the DataSet to Grid that will display all xml data.

            }
        }

        //protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //}   

        void LoadDomesticGridViewFirstRow()
        {
            List<requisitionbo> objCollection = new List<requisitionbo>();
            CreateDomesticGridViewRows(objCollection, true);
        }

        protected void CreateDomesticGridViewRows(List<requisitionbo> objList, bool isAddRemoveRange)
        {
            if (isAddRemoveRange == true)
            {
                requisitionbo obj = new requisitionbo();
                objList.Add(obj);
            }
            grdExpenseStatement.DataSource = objList;
            grdExpenseStatement.DataBind();

            for (int i = 0; i < objList.Count; i++)
            {
                //After binding the gridview, we can then extract and fill the DropDownList with Data 

                DropDownList ddlFrom = (DropDownList)grdExpenseStatement.Rows[i].FindControl("TravelFromDropDownList");
                ////loadfrom(ddlFrom);
                DropDownList ddlTo = (DropDownList)grdExpenseStatement.Rows[i].FindControl("TravelToDropDownList");
               //// loadto(ddlTo);

                DropDownList drpdwnPlaceWork = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnPlaceWork");
                ////loadfrom(drpdwnPlaceWork);
                DropDownList drpdwnNightHalt = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnNightHalt");
                ////loadto(drpdwnNightHalt);
            }
        }
        protected void loadCategory(DropDownList ddl)//Type of place
        {
            DataSet ds = new DataSet();
            DataTable objTable = ds.Tables.Add();
            objTable.Columns.Add("SPKZL", typeof(string));
            
            ExpenseDataContext obj = new ExpenseDataContext();

            foreach (var vrow in obj.sp_Get_Category())
            {
                objTable.Rows.Add(vrow.SPKZL);
            }

            //DataSet oDs = ds;

            ddl.DataValueField = "SPKZL";
            ddl.DataTextField = "SPKZL";
            ddl.DataSource = ds;
            ddl.DataBind();

           
            ListItem drpDefaultItemCategory = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItemCategory);
            ddl.SelectedValue = "0";


        }

        //protected void loadCategoryofplace(DropDownList ddl)//Type of place
        //{
        //    DataSet ds = new DataSet();
        //    DataTable objTable = ds.Tables.Add();
        //    objTable.Columns.Add("SPKZL", typeof(string));

        //    ExpenseDataContext obj = new ExpenseDataContext();

        //    foreach (var vrow in obj.sp_Get_Category())
        //    {
        //        objTable.Rows.Add(vrow.SPKZL);
        //    }

        //    //DataSet oDs = ds;

        //    ddl.DataValueField = "SPKZL";
        //    ddl.DataTextField = "SPKZL";
        //    ddl.DataSource = ds;
        //    ddl.DataBind();


        //    ListItem drpDefaultItemCategory = new ListItem("Select", "0", true);
        //    ddl.Items.Add(drpDefaultItemCategory);
        //    ddl.SelectedValue = "0";


        //}

        protected void loadStates(DropDownList ddl)
        {
            Expensebl objExpensebl = new Expensebl();
            DataSet oDs = objExpensebl.Get_States();

            ddl.DataValueField = "ID";
            ddl.DataTextField = "STATES";
            ddl.DataSource = oDs;
            ddl.DataBind();

            //Expensebl objExpensebl = new Expensebl();
            //ddl.DataSource = objExpensebl.Get_States ();
            //ddl.DataTextField = "";
            //ddl.DataValueField = "STATES";
            //ddl.DataBind();
            ListItem drpDefaultItem3 = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItem3);
            ddl.SelectedValue = "0";
        }

        protected void loadstatesforplace(DropDownList ddl)
        {
            Expensebl objExpensebl = new Expensebl();
            DataSet oDs = objExpensebl.Get_statesforplace();

            ddl.DataValueField = "state_code";
            ddl.DataTextField = "state_name";
            ddl.DataSource = oDs;
            ddl.DataBind();

            //Expensebl objExpensebl = new Expensebl();
            //ddl.DataSource = objExpensebl.Get_States ();
            //ddl.DataTextField = "";
            //ddl.DataValueField = "STATES";
            //ddl.DataBind();
            ListItem drpDefaultItem3 = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItem3);
            ddl.SelectedValue = "0";
        }

        protected void loadfrom(DropDownList ddl, string statecode)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetRegionNameBPO(statecode);
            ddl.DataTextField = "REGION_TEXT25_FROM";
            ddl.DataValueField = "REGION_RGION_FROM";
            ddl.DataBind();
            ListItem drpDefaultItem3 = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItem3);
            ddl.SelectedValue = "0";
        }

        protected void loadNightHalt(DropDownList ddl)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.Items.Add(new ListItem("Yes", "Yes"));
            ddl.Items.Add(new ListItem("No", "No"));
            ddl.DataBind();
            ddl.SelectedValue = "No";
        }

        //protected void loadvia(DropDownList ddl)
        //{
        //    travelrequestbl objtravelrequestbl = new travelrequestbl();
        //    ddl.DataSource = objtravelrequestbl.GetRegionName();
        //    ddl.DataTextField = "REGION_TEXT25_FROM";
        //    ddl.DataValueField = "REGION_RGION_FROM";
        //    ddl.DataBind();
        //    ListItem drpDefaultItem3 = new ListItem("Select", "0", true);
        //    ddl.Items.Add(drpDefaultItem3);
        //    ddl.SelectedValue = "0";
        //}

        //protected void loadto(DropDownList ddl)
        //{
        //    travelrequestbl objtravelrequestbl = new travelrequestbl();
        //    ddl.DataSource = objtravelrequestbl.GetRegionName();
        //    ddl.DataTextField = "REGION_TEXT25_FROM";
        //    ddl.DataValueField = "REGION_RGION_FROM";
        //    ddl.DataBind();
        //    ListItem drpDefaultItem4 = new ListItem("Select", "0", true);
        //    ddl.Items.Add(drpDefaultItem4);
        //    ddl.SelectedValue = "0";
        //}

        protected void loadFromplace(DropDownList ddl)
        {
            //travelrequestbl objtravelrequestbl = new travelrequestbl();
            //ddl.DataSource = objtravelrequestbl.GetRegionName_Fromplace();
            //ddl.DataTextField = "REGION_TEXT25_FROM";
            //ddl.DataValueField = "REGION_RGION_FROM";
            //ddl.DataBind();
            //ListItem drpDefaultItem4 = new ListItem("Select", "0", true);
            //ddl.Items.Add(drpDefaultItem4);
            //ddl.SelectedValue = "0";
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetRegionName_Fromplace(txtEmployeeID.Text);
            ddl.DataTextField = "REGION_TEXT25_FROM";
            ddl.DataValueField = "REGION_RGION_FROM";
            ddl.DataBind();
            ListItem drpDefaultItem4 = new ListItem("Select", "0", true);
            ddl.Items.Add(drpDefaultItem4);
            ddl.SelectedValue = "0";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtApprovedBy.Text != "")
                    funSaveExpense();
                else
                {
                    lblMessageBoard.Text = "Entered By cannot be left blank";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }
        
        public void funSaveExpense()
        {
            try
            {
                //Basic Info
                string strPERNR = txtEmployeeID.Text;
                string strEmployeeName = txtEmpName.Text;
                string strState = txtState.Text;
                string strDesignation = txtDesignation.Text;
                string strHeadQuarters = txtHeadQuarters.Text;
                string strReportTo = txtReportTo.Text;                
                string strFromDate = txtFromDate.Text;
                string strToDate = txtToDate.Text;

                //DA Deductions
                string strDADeductions = txtDA.Text;
                string strTADeductions = txtTA.Text;
                string strOtherExpDeductions = txtOtherExpDeduc.Text;
                string strTotalInchgPersonDeductions = txtTotInchgPersDeduc.Text;

                //Other Details
                string strStationaries = txtStationaries.Text;
                string strCourier = txtCourier.Text;
                string strPandT = txtPAndT.Text;
                string strEmail = txtEmail.Text;
                string strCompProdPurchase = txtCompitatorProductPurchase.Text;
                string strOthers = txtOthers.Text;
                string strMarketDevelopExpense = txtMarketDevelopmentExpences.Text;
                string strBussPass = txtBusPass.Text;
                string strConveyance = txtConveyance.Text;
                string strJCMeetings = txtJcMeetings.Text;

                string strGrandTotal = txtGrandTotal .Text;
                string strEnteredby = txtApprovedBy.Text;
                string strEnteredon = lblApprovedOn.Text;

                
                Expensebo objEXPAddBo = new Expensebo();
                Expensebl objEXPAddBl = new Expensebl();
                Expense_statementbo objEXPStatemntAddBo = new Expense_statementbo();

                objEXPAddBo.PERNR = strPERNR;
                objEXPAddBo.ENAME = strEmployeeName;
                objEXPAddBo.WERKS = strState;
                objEXPAddBo.PLSXT = strDesignation;
                objEXPAddBo.STEXT = strHeadQuarters;
                objEXPAddBo.REPORT_TO = strReportTo;
                objEXPAddBo.BEGDA = Convert.ToDateTime(strFromDate);
                objEXPAddBo.ENDDA =Convert.ToDateTime(strToDate);
                objEXPAddBo.DA_DEDUCTION =strDADeductions;
                objEXPAddBo.TA_DEDUCTION =strTADeductions;
                objEXPAddBo.OTR_EXP_DEDUCT = strOtherExpDeductions;
                objEXPAddBo.TOT_INCHARGE_PERSON_DEDUC  = strTotalInchgPersonDeductions;
                objEXPAddBo.STATIONARY = strStationaries;
                objEXPAddBo.COURIER = strCourier;
                objEXPAddBo.PANDT = strPandT;
                objEXPAddBo.EMAIL = strEmail;
                objEXPAddBo.COMP_PRODUCT_PURCHASE = strCompProdPurchase;
                objEXPAddBo.OTHERS = strOthers;
                objEXPAddBo.MARK_DEVELOP_EXPENCE = strMarketDevelopExpense;
                objEXPAddBo.BUS_PASS = strBussPass;
                objEXPAddBo.CONVEYANCE = strConveyance;
                objEXPAddBo.JC_MEETINGS = strJCMeetings;
                objEXPAddBo.GRAND_TOTAL =strGrandTotal;
                objEXPAddBo.Status = "Review";
                objEXPAddBo.entered_by = strEnteredby;
                objEXPAddBo.approved_on = Convert.ToDateTime(strEnteredon);
                              
                bool? dd = true;
                int iCnt = 0;

                objEXPAddBl.Create_Expense(objEXPAddBo, ref dd);

                if (dd == true)
                {
                    //lblMessageBoard.Text = "Expense updated successfully";
                    //lblMessageBoard.ForeColor = System.Drawing.Color.Green ;
                    iCnt = 1;
                }
                else
                {
                    //lblMessageBoard.Text = "Expense inserted successfully";
                    //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    iCnt = 1;
                }

                if (iCnt == 1)
                {
                    try
                    {
                        if (grdExpenseStatement.Rows.Count >= 0)
                        {
                            for (int i = 0; i <= grdExpenseStatement.Rows.Count - 1; i++)
                            {                                 
                                objEXPStatemntAddBo.PERNR = strPERNR;
                                objEXPStatemntAddBo.BEGIN_DATE = Convert.ToDateTime(strFromDate);
                                objEXPStatemntAddBo.END_DATE = Convert.ToDateTime(strToDate);
                                //lblMessageBoard.Text = lblMessageBoard.Text + strFromDate;
                                //lblMessageBoard.Text = lblMessageBoard.Text + " - "+strToDate;

                                Label lblDate = (Label)grdExpenseStatement.Rows[i].FindControl("lblDate");
                             //   lblMessageBoard.Text = lblMessageBoard.Text + " - " + lblDate.Text.ToString();
                                objEXPStatemntAddBo.DATE1 =  Convert.ToDateTime(lblDate.Text.ToString());
                            //    lblMessageBoard.Text = lblMessageBoard.Text + " - " + Convert.ToString(objEXPStatemntAddBo.DATE1);

                                //TextBox txtPlaceWork = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtPlaceWork");
                                //objEXPStatemntAddBo.PLACE_WORK = txtPlaceWork.Text.ToString();


                                DropDownList StateDropDownList = (DropDownList)grdExpenseStatement.Rows[i].FindControl("StateDropDownList");
                                objEXPStatemntAddBo.TRAVEL_Via = StateDropDownList.SelectedValue.ToString();

                                DropDownList drpdwnPlaceWork = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnPlaceWork");
                                objEXPStatemntAddBo.PLACE_WORK = drpdwnPlaceWork.SelectedValue.ToString();
                                
                                //TextBox txtNightHalt = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtNightHalt");
                                //objEXPStatemntAddBo.NIGHT_HALT = txtNightHalt.Text.ToString();

                                DropDownList drpdwnNightHalt = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnNightHalt");
                                objEXPStatemntAddBo.NIGHT_HALT = drpdwnNightHalt.SelectedValue.ToString();

                               // DropDownList drpdwnStates = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnState");
                                //objEXPStatemntAddBo.STATES = drpdwnStates.SelectedValue.ToString();
                                 
                                DropDownList drpdwnCategory = (DropDownList)grdExpenseStatement.Rows[i].FindControl("CategoryDropDownList");
                                objEXPStatemntAddBo.CATEGORY = drpdwnCategory.SelectedValue.ToString();
                                
                                DropDownList drpdwnCategoryofplace = (DropDownList)grdExpenseStatement.Rows[i].FindControl("CategoryofplaceDropDownList");
                                objEXPStatemntAddBo.CATEGORYofplace = drpdwnCategoryofplace.SelectedValue.ToString();

                                CheckBox chkLodgeBills = (CheckBox)grdExpenseStatement.Rows[i].FindControl("chkLodgeBills");
                                if (chkLodgeBills.Checked == true)
                                {
                                    objEXPStatemntAddBo.LODGE_BILLS = "1";
                                }
                                else
                                {
                                    objEXPStatemntAddBo.LODGE_BILLS = "0";
                                }

                                TextBox txtBillAmount = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtBillAmount");
                                objEXPStatemntAddBo.BILL_AMOUNT = txtBillAmount.Text.ToString();
                                
                                DropDownList drpdwnTravelFrom = (DropDownList)grdExpenseStatement.Rows[i].FindControl("TravelFromDropDownList");
                                objEXPStatemntAddBo.TRAVEL_FROM = drpdwnTravelFrom.SelectedValue.ToString();

                                ////DropDownList drpdwnTravelVia = (DropDownList)grdExpenseStatement.Rows[i].FindControl("ViaDropDownList");
                                ////objEXPStatemntAddBo.TRAVEL_Via = drpdwnTravelVia.SelectedValue.ToString();

                                DropDownList drpdwnTravelTo = (DropDownList)grdExpenseStatement.Rows[i].FindControl("TravelToDropDownList");
                                objEXPStatemntAddBo.TRAVEL_TO = drpdwnTravelTo.SelectedValue.ToString();

                                TextBox txtDistance = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtDistance");
                                objEXPStatemntAddBo.DISTANCE = txtDistance.Text.ToString() ;

                                DropDownList drpdwnMode = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnMode");
                                objEXPStatemntAddBo.ZMODE = drpdwnMode.SelectedValue.ToString();

                                CheckBox chkTicketProd = (CheckBox)grdExpenseStatement.Rows[i].FindControl("chkTicketProd");
                                if (chkTicketProd.Checked == true)
                                {
                                    objEXPStatemntAddBo.TICKETS_PROD = "1";
                                }
                                else
                                {
                                    objEXPStatemntAddBo.TICKETS_PROD = "0";
                                }

                                TextBox txtFare = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtFare");
                                objEXPStatemntAddBo.FARE = txtFare.Text.ToString();

                                TextBox txtDAgrd = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtDAgrd");
                                objEXPStatemntAddBo.DA = txtDAgrd.Text.ToString();

                                //TextBox txtOthers1 = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtOthers");
                                //objEXPStatemntAddBo.OTHERS = txtOthers1.Text.ToString();

                                TextBox txtTotal = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtTotal");
                                objEXPStatemntAddBo.TOTAL = txtTotal.Text.ToString();

                                TextBox txtRemarks = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtRemarks");
                                objEXPStatemntAddBo.REMARKS = txtRemarks.Text.ToString();

                                bool? dd1 = true;
                                objEXPStatemntAddBo.status = "Review";

                                objEXPStatemntAddBo.entered_by = strEnteredby;
                                objEXPStatemntAddBo.approved_on = Convert.ToDateTime(strEnteredon);

                              //  lblMessageBoard.Text = lblMessageBoard.Text + " - " + "1";
                                objEXPAddBl.Create_Expense_Statement(objEXPStatemntAddBo, ref dd1);
                             //     lblMessageBoard.Text = lblMessageBoard.Text + " - " +"2";

                                if ((dd1 == true) || (dd1 == false))
                                {
                                    iCnt = 1;
                                }

                            }                          
                        }

                        if (iCnt == 1)
                        {
                            lblMessageBoard.Text = "Expense inserted successfully";
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;

                            updateinwardentry();


                            FuncClearData(); //to clear controls
                        } 
                    }
                    catch (Exception ex)
                    {
                        lblMessageBoard.Text =  "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }


        public void updateinwardentry()
        {
            try
            {
                ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();
                objExpenseDataContext.sp_Update_Expense_Inward_Entry(txtEmployeeID.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), "Statement Entered");
                objExpenseDataContext.Dispose();
            }

            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }


        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                funLoadExpense_Statement_Details();
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        public void funLoadExpense_Statement_Details()
        {
            try
            {
                string strPernr = txtEmployeeID.Text;
                string strToDate = txtToDate.Text;
                string strFromDate = txtFromDate.Text;

                if (strPernr == string.Empty || strPernr == "")
                {
                    lblMessageBoard.Text = "Please Enter Employee ID";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if ((strToDate == string.Empty || strToDate == "") || ((strFromDate == string.Empty || strFromDate == "")))
                {
                    lblMessageBoard.Text = "Please Select To Date";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    FuncClearData11(); //clear text data

                    //Display basic Info
                    fun_GetBasicInfo();

                    if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                    {
                        lblMessageBoard.Text = "Please select proper Date";
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    List<DateTime> lstgrdDate = new List<DateTime>();
                    lstgrdDate = GetDatesBetween(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));

                    if (lstgrdDate.Count > 0)
                    {
                        grdExpenseStatement.DataSource = lstgrdDate;
                        grdExpenseStatement.DataBind();

                        for (int i = 0; i < lstgrdDate.Count; i++)
                        {
                            //After binding the gridview, we can then extract and fill the DropDownList with Data 

                            Label lblDate = (Label)grdExpenseStatement.Rows[i].FindControl("lblDate");
                            lblDate.Text = (lstgrdDate[i]).ToString("dd-MMM-yyyy");


                            //To check Date with Corporate Claims Starts
                            
                            DropDownList ddlCategory1 = (DropDownList)grdExpenseStatement.Rows[i].FindControl("CategoryDropDownList");
                            DropDownList ddlTo1 = (DropDownList)grdExpenseStatement.Rows[i].FindControl("TravelToDropDownList");
                            DropDownList ddlCategoryofplace1 = (DropDownList)grdExpenseStatement.Rows[i].FindControl("CategoryofplaceDropDownList");
                            DropDownList ddlFrom1 = (DropDownList)grdExpenseStatement.Rows[i].FindControl("TravelFromDropDownList");
                           //// DropDownList ddlVia1 = (DropDownList)grdExpenseStatement.Rows[i].FindControl("ViaDropDownList");
                            DropDownList drpdwnPlaceWork1 = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnPlaceWork");
                            DropDownList drpdwnNightHalt1 = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnNightHalt");
                            DropDownList drpdwnMode1 = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnMode");
                            CheckBox chkLodgeBills1 = (CheckBox)grdExpenseStatement.Rows[i].FindControl("chkLodgeBills");
                            CheckBox chkTicketProd1 = (CheckBox)grdExpenseStatement.Rows[i].FindControl("chkTicketProd");


                            TextBox txtDistance1 = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtDistance");
                            TextBox txtFare1 = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtFare");
                            TextBox txtDAgrd1 = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtDAgrd");
                            TextBox txtBillAmount1 = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtBillAmount");
                            TextBox txtTotal1 = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtTotal");
                            TextBox txtRemarks1 = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtRemarks");


                            string streid = string.Empty;// = txtEmployeeID.Text;
                            DateTime date = DateTime.MinValue; //= Convert.ToDateTime(lblDate.Text);
                            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();
                            foreach (var vRow in objExpenseDataContext.sp_Get_Corporate_Claims(txtEmployeeID.Text, Convert.ToDateTime(lblDate.Text)))
                            {
                                streid = vRow.PERNR;
                                date = Convert.ToDateTime(vRow.Date);


                            }
                            objExpenseDataContext.Dispose();

                            if (lblDate.Text == date.ToString("dd-MMM-yyyy"))
                            {
                                grdExpenseStatement.Rows[i].BackColor = System.Drawing.Color.Red;

                                ddlCategory1.Enabled = false;
                                ddlTo1.Enabled = false;
                                ddlCategoryofplace1.Enabled = false;
                                ddlFrom1.Enabled = false;
                                ////ddlVia1.Enabled = false;
                                drpdwnPlaceWork1.Enabled = false;
                                drpdwnNightHalt1.Enabled = false;
                                drpdwnMode1.Enabled = false;
                                chkLodgeBills1.Enabled = false;
                                chkTicketProd1.Enabled = false;
                                txtDistance1.Enabled = false;
                                txtFare1.Enabled = false;
                                txtDAgrd1.Enabled = false;
                                txtBillAmount1.Enabled = false;
                                txtTotal1.Enabled = false;
                                txtRemarks1.Enabled = false;
                            }
                            //Corporate Claim Ends


                            //DropDownList ddlStates = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnState");
                            // loadStates(ddlStates);


                            DropDownList ddlstate = (DropDownList)grdExpenseStatement.Rows[i].FindControl("StateDropDownList");
                            ////loadstatesforplace(ddlstate);

                            DropDownList ddlCategory = (DropDownList)grdExpenseStatement.Rows[i].FindControl("CategoryDropDownList");
                            loadCategory(ddlCategory);

                            DropDownList ddlCategoryofplace = (DropDownList)grdExpenseStatement.Rows[i].FindControl("CategoryofplaceDropDownList");
                            //loadCategory(ddlCategoryofplace);

                           


                            DropDownList ddlFrom = (DropDownList)grdExpenseStatement.Rows[i].FindControl("TravelFromDropDownList");
                            loadFromplace(ddlFrom);

                            ////DropDownList ddlVia = (DropDownList)grdExpenseStatement.Rows[i].FindControl("ViaDropDownList");
                            ////loadvia(ddlVia);

                            DropDownList ddlTo = (DropDownList)grdExpenseStatement.Rows[i].FindControl("TravelToDropDownList");
                            ////loadto(ddlTo);
                            loadFromplace(ddlTo);
                            DropDownList drpdwnPlaceWork = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnPlaceWork");
                            ////loadfrom(drpdwnPlaceWork);
                            loadFromplace(drpdwnPlaceWork);
                            DropDownList drpdwnNightHalt = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnNightHalt");
                            ////loadto(drpdwnNightHalt);
                            loadNightHalt(drpdwnNightHalt);
                        }
                    }

                    Expensebl objExpBl = new Expensebl();
                    Expensecollection objLstExp = new Expensecollection();
                    Expense_statementcollection objLstExpStatmnt = new Expense_statementcollection();

                    objLstExp = objExpBl.Get_ExpenseDetails(strPernr, Convert.ToDateTime(strFromDate), Convert.ToDateTime(strToDate));

                    if (objLstExp.Count > 0)
                    {
                        foreach (var vrow in objLstExp)
                        {
                            //DA Deductions
                            txtDA.Text = vrow.DA_DEDUCTION.ToString();
                            txtTA.Text = vrow.TA_DEDUCTION.ToString();
                            txtOtherExpDeduc.Text = vrow.OTR_EXP_DEDUCT.ToString();
                            txtTotInchgPersDeduc.Text = vrow.TOT_INCHARGE_PERSON_DEDUC.ToString();

                            //Other Details
                            txtStationaries.Text = vrow.STATIONARY.ToString();
                            txtCourier.Text = vrow.COURIER.ToString();
                            txtPAndT.Text = vrow.PANDT.ToString();
                            txtEmail.Text = vrow.EMAIL.ToString();
                            txtCompitatorProductPurchase.Text = vrow.COMP_PRODUCT_PURCHASE.ToString();
                            txtOthers.Text = vrow.OTHERS.ToString();
                            txtMarketDevelopmentExpences.Text = vrow.MARK_DEVELOP_EXPENCE.ToString();
                            txtBusPass.Text = vrow.BUS_PASS.ToString();
                            txtConveyance.Text = vrow.CONVEYANCE.ToString();
                            txtJcMeetings.Text = vrow.JC_MEETINGS.ToString();

                            //Grand Total
                            txtGrandTotal.Text = vrow.GRAND_TOTAL.ToString();

                            txtApprovedBy.Text = vrow.approved_by.ToString();
                            lblApprovedOn.Text = vrow.approved_on.ToShortDateString();
                        }
                    }

                    objLstExpStatmnt = objExpBl.Get_ExpenseStatementDetails(strPernr, Convert.ToDateTime(strFromDate), Convert.ToDateTime(strToDate));

                    if (objLstExpStatmnt.Count > 0)
                    {
                        grdExpenseStatement.DataSource = objLstExpStatmnt;
                        grdExpenseStatement.DataBind();

                        for (int i = 0; i <= objLstExpStatmnt.Count - 1; i++)
                        {
                            Label lblDate = (Label)grdExpenseStatement.Rows[i].FindControl("lblDate");
                            lblDate.Text = objLstExpStatmnt.ElementAt(i).DATE1.ToString("dd-MMM-yyyy");


                            ////DropDownList StateDropDownList = (DropDownList)grdExpenseStatement.Rows[i].FindControl("StateDropDownList");
                            ////loadstatesforplace(StateDropDownList);
                            ////StateDropDownList.SelectedValue = objLstExpStatmnt.ElementAt(i).TRAVEL_Via.ToString();
                            string statecode = "";////StateDropDownList.SelectedValue;



                            //TextBox txtPlaceWork = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtPlaceWork");
                            //txtPlaceWork.Text = objLstExpStatmnt.ElementAt(i).PLACE_WORK.ToString();

                            DropDownList drpdwnPlaceWork = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnPlaceWork");
                            ////loadfrom(drpdwnPlaceWork);
                            ////loadfrom(drpdwnPlaceWork, statecode);
                            loadFromplace(drpdwnPlaceWork);
                            drpdwnPlaceWork.SelectedValue =  objLstExpStatmnt.ElementAt(i).PLACE_WORK.ToString();

                            //TextBox txtNightHalt = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtNightHalt");
                            //txtNightHalt.Text = objLstExpStatmnt.ElementAt(i).NIGHT_HALT.ToString();

                            DropDownList drpdwnNightHalt = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnNightHalt");
                            ////loadfrom(drpdwnNightHalt, statecode);
                            loadNightHalt(drpdwnNightHalt);
                            drpdwnNightHalt.SelectedValue = objLstExpStatmnt.ElementAt(i).NIGHT_HALT.ToString();

                           // DropDownList drpdwnStates = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnState");
                           // loadStates(drpdwnStates);
                           // drpdwnStates.SelectedValue = objLstExpStatmnt.ElementAt(i).STATES .ToString();

                            DropDownList drpdwnCategory = (DropDownList)grdExpenseStatement.Rows[i].FindControl("CategoryDropDownList");
                            loadCategory(drpdwnCategory);
                            drpdwnCategory.SelectedValue = objLstExpStatmnt.ElementAt(i).CATEGORY.ToString();

                            DropDownList drpdwnCategoryofplace = (DropDownList)grdExpenseStatement.Rows[i].FindControl("CategoryofplaceDropDownList");
                            drpdwnCategoryofplace.SelectedValue = objLstExpStatmnt.ElementAt(i).CATEGORYofplace.ToString();

                            CheckBox chkLodgeBills = (CheckBox)grdExpenseStatement.Rows[i].FindControl("chkLodgeBills");
                            if (objLstExpStatmnt.ElementAt(i).LODGE_BILLS == "1")
                            {
                                chkLodgeBills.Checked = true;
                            }

                            TextBox txtBillAmount = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtBillAmount");
                            txtBillAmount.Text = objLstExpStatmnt.ElementAt(i).BILL_AMOUNT.ToString();

                            DropDownList drpdwnTravelFrom = (DropDownList)grdExpenseStatement.Rows[i].FindControl("TravelFromDropDownList");
                            loadFromplace(drpdwnTravelFrom);
                            drpdwnTravelFrom.SelectedValue =  objLstExpStatmnt.ElementAt(i).TRAVEL_FROM.ToString() ;

                           //// DropDownList drpdwnTravelVia = (DropDownList)grdExpenseStatement.Rows[i].FindControl("ViaDropDownList");
                           //// loadvia(drpdwnTravelVia);
                            ////drpdwnTravelVia.SelectedValue = objLstExpStatmnt.ElementAt(i).TRAVEL_Via.ToString();

                            DropDownList drpdwnTravelTo = (DropDownList)grdExpenseStatement.Rows[i].FindControl("TravelToDropDownList");
                            ////loadto(drpdwnTravelTo);
                            ////loadfrom(drpdwnTravelTo, statecode);
                            loadFromplace(drpdwnTravelTo);
                            drpdwnTravelTo.SelectedValue =objLstExpStatmnt.ElementAt(i).TRAVEL_TO.ToString() ;

                            TextBox txtDistance = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtDistance");
                            txtDistance.Text = objLstExpStatmnt.ElementAt(i).DISTANCE.ToString();

                            DropDownList drpdwnMode = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnMode");
                            drpdwnMode.SelectedValue = objLstExpStatmnt.ElementAt(i).ZMODE.ToString();

                            CheckBox chkTicketProd = (CheckBox)grdExpenseStatement.Rows[i].FindControl("chkTicketProd");
                            if (objLstExpStatmnt.ElementAt(i).TICKETS_PROD == "1")
                            {
                                chkTicketProd.Checked = true;
                            }

                            TextBox txtFare = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtFare");
                            txtFare.Text = objLstExpStatmnt.ElementAt(i).FARE.ToString();

                            TextBox txtDAgrd = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtDAgrd");
                            txtDAgrd.Text = objLstExpStatmnt.ElementAt(i).DA.ToString();

                            //TextBox txtOthers1 = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtOthers");
                            //txtOthers1.Text = objLstExpStatmnt.ElementAt(i).OTHERS .ToString();

                            TextBox txtTotal = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtTotal");
                            txtTotal.Text = objLstExpStatmnt.ElementAt(i).TOTAL.ToString();

                            TextBox txtRemarks = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtRemarks");
                            txtRemarks.Text = objLstExpStatmnt.ElementAt(i).REMARKS.ToString();
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        //protected void highlightrow(object sender, GridViewRowEventArgs e)
        //{

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //           for (int i = 0; i < grdExpenseStatement.Rows.Count; i++)
        //                {
        //                    //After binding the gridview, we can then extract and fill the DropDownList with Data 

        //                    Label lblDate = (Label)grdExpenseStatement.Rows[i].FindControl("lblDate");
        //                    //lblDate.Text = (lstgrdDate[i]).ToString("dd-MMM-yyyy");

        //                    string date = lblDate.Text;//e.Row.Cells[0].Text;
        //                    if (date == "03-Jan-2015")
        //                    {
        //                        lblDate.ForeColor = System.Drawing.Color.Red;
                                
        //                        e.Row.BackColor = System.Drawing.Color.Red;
        //                    }
        //                }

        //    }


        //}

        private List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                allDates.Add(date);

            return allDates;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                divBasicInfo.Visible = false;
                job.Visible = false;
                job1.Visible = false;
                job2.Visible = false;
                btnSave.Visible = false;
                btnClearRow.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text="UnKnown Error Please Contact Administrator ( "+ ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        public void fun_GetBasicInfo()
        {
            try
            {
                string strPERNR = txtEmployeeID.Text;

                BasicInfo_Claimscollection objLst = new BasicInfo_Claimscollection();
                BasicInfo_Claimsbl objLstBl = new BasicInfo_Claimsbl();

                objLst = objLstBl.Get_BasicInfo_Claims_Details(strPERNR);

                foreach (var vrow in objLst)
                {
                    txtEmpName.Text = vrow.ENAME ;
                    txtState.Text = objLstBl.Get_State(strPERNR);
                    txtDesignation.Text = vrow.PLSXT ;
                    txtHeadQuarters.Text = objLstBl.Get_HQ(strPERNR);
                    txtReportTo.Text = vrow.S_NAME;
                    
                    string stremployeeEmailid = vrow.USRID;
                    string strhodEmailid = vrow.S_USRID;

                    Session.Add("employeeEmailid", stremployeeEmailid);
                    Session.Add("hodEmailid", strhodEmailid);              
                }

                //string strName ="Shruthi";
                //string strState="Karnataka";
                //string strDesignation="Software Engineer";
                //string strHeadquarters = "Mysore";               
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void btnClearRow_Click(object sender, EventArgs e)
        {
            FuncClearData();
        }

        public void FuncClearData()
        {
            try
            {
                //Basic Info
                txtEmployeeID.Text = "";
                txtEmpName.Text = "";
                txtState.Text = "";
                txtDesignation.Text = "";
                txtHeadQuarters.Text = "";
                txtFromDate.Text = "";
                txtToDate.Text = "";

                //DA Deductions
                txtDA.Text = "0";
                txtTA.Text = "0";
                txtOtherExpDeduc.Text = "0";
                txtTotInchgPersDeduc.Text = "0";

                //Other Details
                txtStationaries.Text = "0";
                txtCourier.Text = "0";
                txtPAndT.Text = "0";
                txtEmail.Text = "0";
                txtCompitatorProductPurchase.Text = "0";
                txtOthers.Text = "0";
                txtMarketDevelopmentExpences.Text = "0";
                txtBusPass.Text = "0";
                txtConveyance.Text = "0";
                txtJcMeetings.Text = "0";

                txtGrandTotal.Text = "";

                divBasicInfo.Visible = false;
                job.Visible = false;
                job1.Visible = false;
                job2.Visible = false;
                btnSave.Visible = false;
                btnClearRow.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }      
        }

        public void FuncClearData11()
        {
            try
            {
                //Basic Info
                //txtEmployeeID.Text = "";
                txtEmpName.Text = "";
                txtState.Text = "";
                txtDesignation.Text = "";
                txtHeadQuarters.Text = "";
                //txtFromDate.Text = "";
                //txtToDate.Text = "";

                //DA Deductions
                txtDA.Text = "0";
                txtTA.Text = "0";
                txtOtherExpDeduc.Text = "0";
                txtTotInchgPersDeduc.Text = "0";

                //Other Details
                txtStationaries.Text = "0";
                txtCourier.Text = "0";
                txtPAndT.Text = "0";
                txtEmail.Text = "0";
                txtCompitatorProductPurchase.Text = "0";
                txtOthers.Text = "0";
                txtMarketDevelopmentExpences.Text = "0";
                txtBusPass.Text = "0";
                txtConveyance.Text = "0";
                txtJcMeetings.Text = "0";

                txtGrandTotal.Text = "0";

                divBasicInfo.Visible = true;
                job.Visible = true;
                job1.Visible = true;
                job2.Visible = true;
                btnSave.Visible = true;
                btnClearRow.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void txtDA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strDA = txtDA.Text;

                if (!((strDA == "") || (strDA == string.Empty) || (strDA == null)))
                {
                    //  int iDA = Convert.ToInt32(strDA);
                    txtTotInchgPersDeduc.Text = strDA;
                }
                txtTA.Focus();
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void txtTA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strDA = txtDA.Text;
                string strTA = txtTA.Text;

                double iDA = 0;
                double iTA = 0;

                if (!((strDA == "") || (strDA == string.Empty) || (strDA == null)))
                {
                    iDA = Convert.ToDouble(string.Format("{0:0.00}", strDA));
                }

                if (!((strTA == "") || (strTA == string.Empty) || (strTA == null)))
                {
                    iTA = Convert.ToDouble(string.Format("{0:0.00}", (strTA)));
                }

                double iTT = iDA + iTA;
                txtTotInchgPersDeduc.Text = Convert.ToString(iTT);
                txtOtherExpDeduc.Focus();
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void txtOtherExpDeduc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strDA = txtDA.Text;
                string strTA = txtTA.Text;
                string strOED = txtOtherExpDeduc.Text;

                double iDA = 0;
                double iTA = 0;
                double iOED = 0;

                if (!((strDA == "") || (strDA == string.Empty) || (strDA == null)))
                {
                    iDA = Convert.ToDouble(string.Format("{0:0.00}", (strDA)));
                }

                if (!((strTA == "") || (strTA == string.Empty) || (strTA == null)))
                {
                    iTA = Convert.ToDouble(string.Format("{0:0.00}", (strTA)));
                }

                if (!((strOED == "") || (strOED == string.Empty) || (strOED == null)))
                {
                    iOED = Convert.ToDouble(string.Format("{0:0.00}", (strOED)));
                }
                double iTT = iDA + iTA + iOED;
                txtTotInchgPersDeduc.Text = Convert.ToString(iTT);
                txtStationaries.Focus();
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void txtFare_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdExpenseStatement.Rows.Count > 0)
                {
                    Expensebl objBl = new Expensebl();

                    GridViewRow row1 = (sender as TextBox).Parent.Parent as GridViewRow;

                    int a = row1.RowIndex;

                    foreach (GridViewRow row in grdExpenseStatement.Rows)
                    {
                        if (row.RowIndex == a)
                        {
                            TextBox txtFare = (TextBox)row.FindControl("txtFare");
                            double iFare = Convert.ToDouble(string.Format("{0:0.00}", (txtFare.Text)));

                            TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
                            double iDA = Convert.ToDouble(string.Format("{0:0.00}", (txtDAgrd.Text)));

                            TextBox txtLodging = (TextBox)row.FindControl("txtBillAmount");
                            double iLodging = Convert.ToDouble(string.Format("{0:0.00}", (txtLodging.Text)));

                            TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                            txtTotal.Text = Convert.ToString(iFare + iDA + iLodging);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void txtDAgrd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdExpenseStatement.Rows.Count > 0)
                {
                    GridViewRow row1 = (sender as TextBox).Parent.Parent as GridViewRow;

                    int a = row1.RowIndex;

                    foreach (GridViewRow row in grdExpenseStatement.Rows)
                    {
                        if (row.RowIndex == a)
                        {
                            TextBox txtFare = (TextBox)row.FindControl("txtFare");
                            double iFare = Convert.ToDouble(string.Format("{0:0.00}", (txtFare.Text)));

                            TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
                            double iDA = Convert.ToDouble(string.Format("{0:0.00}", (txtDAgrd.Text)));

                            TextBox txtLodging = (TextBox)row.FindControl("txtBillAmount");
                            double iLodging = Convert.ToDouble(string.Format("{0:0.00}", (txtLodging.Text)));

                            CheckBox chkLodgeBills = (CheckBox)row.FindControl("chkLodgeBills");

                            ScriptManager manager = ScriptManager.GetCurrent(this);
                            manager.SetFocus(chkLodgeBills);

                            TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                            txtTotal.Text = Convert.ToString(iFare + iDA + iLodging);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void txtBillAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdExpenseStatement.Rows.Count > 0)
                {
                    GridViewRow row1 = (sender as TextBox).Parent.Parent as GridViewRow;

                    int a = row1.RowIndex;

                    foreach (GridViewRow row in grdExpenseStatement.Rows)
                    {
                        if (row.RowIndex == a)
                        {
                            TextBox txtFare = (TextBox)row.FindControl("txtFare");
                            double iFare = Convert.ToDouble(string.Format("{0:0.00}", (txtFare.Text)));

                            TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
                            double iDA = Convert.ToDouble(string.Format("{0:0.00}", (txtDAgrd.Text)));

                            TextBox txtLodging = (TextBox)row.FindControl("txtBillAmount");
                            double iLodging = Convert.ToDouble(string.Format("{0:0.00}", (txtLodging.Text)));

                            TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                            txtTotal.Text = Convert.ToString(iFare + iDA + iLodging);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        ////protected void txtDistance_TextChanged(object sender, EventArgs e)
        ////{
        ////    //if (grdExpenseStatement.Rows.Count > 0)
        ////    //{
        ////    //    Expensebl objBl = new Expensebl();

        ////    //    GridViewRow row1 = (sender as TextBox).Parent.Parent as GridViewRow;

        ////    //    int a = row1.RowIndex;

        ////    //    foreach (GridViewRow row in grdExpenseStatement.Rows)
        ////    //    {
        ////    //        if (row.RowIndex == a)
        ////    //        {
        ////    //            DropDownList TravelToDropDownList = (DropDownList)row.FindControl("TravelToDropDownList");
        ////    //            string strToTravel = TravelToDropDownList.SelectedValue;

                       
                  
        ////    //            DropDownList drpdwnCategory = (DropDownList)row.FindControl("CategoryDropDownList");
        ////    //            string strCategory = drpdwnCategory.SelectedValue;

        ////    //            string strRate = objBl.Get_Rate_Fare_ForDistance(txtEmployeeID.Text, strCategory, strToTravel);
        ////    //            double iRate = 0;

        ////    //            if (!((strRate == string.Empty) || (strRate == "") || (strRate == null)))
        ////    //            {
        ////    //                iRate = Convert.ToDouble(string.Format("{0:0.00}",(strRate);
        ////    //            }

        ////    //            TextBox txtDistance = (TextBox)row.FindControl("txtDistance");
        ////    //            double iDistance = Convert.ToDouble(string.Format("{0:0.00}",(txtDistance.Text);

        ////    //            TextBox txtFare = (TextBox)row.FindControl("txtFare");
        ////    //            txtFare.Text = Convert.ToString(iDistance * iRate);
        ////    //            double iFare = Convert.ToDouble(string.Format("{0:0.00}",(txtFare.Text);

        ////    //            TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
        ////    //            double iDA = Convert.ToDouble(string.Format("{0:0.00}",(txtDAgrd.Text);

        ////    //            TextBox txtLodging = (TextBox)row.FindControl("txtBillAmount");
        ////    //            double iLodging = Convert.ToDouble(string.Format("{0:0.00}",(txtLodging.Text);

        ////    //            TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
        ////    //            txtTotal.Text = Convert.ToString(iFare + iDA+iLodging);
        ////    //        }
        ////    //    }

        ////    //}
        ////}

        protected void drpdwnMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (grdExpenseStatement.Rows.Count > 0)
            //{
            //    double  iToTal = 0;

            //    for (int i = 0; i <= grdExpenseStatement.Rows.Count - 1; i++)
            //    {
            //        TextBox txtTotal = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtTotal");
            //        iToTal = iToTal + Convert.ToDouble(string.Format("{0:0.00}", (txtTotal.Text);
            //    }

            //    double iStationary = Convert.ToDouble(string.Format("{0:0.00}",(txtStationaries.Text);
            //    double iCourier = Convert.ToDouble(string.Format("{0:0.00}",(txtCourier.Text);
            //    double iPaT = Convert.ToDouble(string.Format("{0:0.00}",(txtPAndT.Text);
            //    double iEmail = Convert.ToDouble(string.Format("{0:0.00}",(txtEmail.Text);
            //    double iComp = Convert.ToDouble(string.Format("{0:0.00}",(txtCompitatorProductPurchase.Text);
            //    double iOthers = Convert.ToDouble(string.Format("{0:0.00}",(txtOthers.Text);
            //    double iMarket = Convert.ToDouble(string.Format("{0:0.00}",(txtMarketDevelopmentExpences.Text);
            //    double iBusPass = Convert.ToDouble(string.Format("{0:0.00}",(txtBusPass.Text);
            //    double iConvey = Convert.ToDouble(string.Format("{0:0.00}",(txtConveyance.Text);
            //    double iJcMeetings = Convert.ToDouble(string.Format("{0:0.00}",(txtJcMeetings.Text);

            //    double  iTT = iStationary + iCourier + iPaT + iEmail + iComp + iOthers + iMarket + iBusPass + iConvey + iJcMeetings;
                
            //    txtGrandTotal.Text = Convert.ToString(iToTal + iTT);
            //}
            try
            {
                if (grdExpenseStatement.Rows.Count > 0)
                {
                    Expensebl objBl = new Expensebl();

                    GridViewRow row1 = (sender as DropDownList).Parent.Parent as GridViewRow;

                    int a = row1.RowIndex;

                    foreach (GridViewRow row in grdExpenseStatement.Rows)
                    {
                        if (row.RowIndex == a)
                        {
                            DropDownList drpdwnMode = (DropDownList)row.FindControl("drpdwnMode");
                            string strMode = drpdwnMode.SelectedItem.Text;

                            if (strMode == "Road")
                            {

                                //DropDownList StateDropDownList = (DropDownList)row.FindControl("StateDropDownList");
                                //string strState = StateDropDownList.SelectedItem.Text;

                                //TextBox txtState = (TextBox)row.FindControl("txtState");
                                string strState = txtState.Text;

                                string strRate = objBl.Get_Rate_Fare_ForDistance(strState);
                                double iRate = 0;

                                if (!((strRate == string.Empty) || (strRate == "") || (strRate == null)))
                                {
                                    iRate = Convert.ToDouble(string.Format("{0:0.00}", (strRate)));
                                }

                                TextBox txtDistance = (TextBox)row.FindControl("txtDistance");
                                double iDistance = Convert.ToDouble(string.Format("{0:0.00}", (txtDistance.Text)));

                                TextBox txtFare = (TextBox)row.FindControl("txtFare");
                                txtFare.Text = Convert.ToString(iDistance * iRate);
                                double iFare = Convert.ToDouble(string.Format("{0:0.00}", (txtFare.Text)));

                                TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
                                double iDA = Convert.ToDouble(string.Format("{0:0.00}", (txtDAgrd.Text)));

                                TextBox txtLodging = (TextBox)row.FindControl("txtBillAmount");
                                double iLodging = Convert.ToDouble(string.Format("{0:0.00}", (txtLodging.Text)));

                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                txtTotal.Text = Convert.ToString(iFare + iDA + iLodging);
                            }

                            else
                            {

                                //DropDownList StateDropDownList = (DropDownList)row.FindControl("StateDropDownList");
                                //string strState = StateDropDownList.SelectedItem.Text;


                                //string strRate = objBl.Get_Rate_Fare_ForDistance(strState);
                                //double iRate = 0;

                                //if (!((strRate == string.Empty) || (strRate == "") || (strRate == null)))
                                //{
                                //    iRate = Convert.ToDouble(string.Format("{0:0.00}",(strRate);
                                //}

                                TextBox txtDistance = (TextBox)row.FindControl("txtDistance");
                                double iDistance = Convert.ToDouble(string.Format("{0:0.00}", (txtDistance.Text)));

                                TextBox txtFare = (TextBox)row.FindControl("txtFare");
                                txtFare.Text = "0";
                                double iFare = Convert.ToDouble(string.Format("{0:0.00}", (txtFare.Text)));

                                TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
                                double iDA = Convert.ToDouble(string.Format("{0:0.00}", (txtDAgrd.Text)));

                                TextBox txtLodging = (TextBox)row.FindControl("txtBillAmount");
                                double iLodging = Convert.ToDouble(string.Format("{0:0.00}", (txtLodging.Text)));

                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                txtTotal.Text = Convert.ToString(iFare + iDA + iLodging);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        //protected void TravelToDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (grdExpenseStatement.Rows.Count > 0)
        //    {
        //        GridViewRow row1 = (sender as DropDownList).Parent.Parent as GridViewRow;

        //        Expensebl objBl = new Expensebl();

        //        int a = row1.RowIndex;

        //        foreach (GridViewRow row in grdExpenseStatement.Rows)
        //        {
        //            if (row.RowIndex == a)
        //            {
        //                //Boarding
        //                //DropDownList CategoryDropDownList = (DropDownList)row.FindControl("CategoryDropDownList");
        //                string strCategory = "BORD"; //CategoryDropDownList.SelectedValue;

        //                DropDownList TravelToDropDownList = (DropDownList)row.FindControl("TravelToDropDownList");
        //                string strToTravel = TravelToDropDownList.SelectedValue;

        //                DropDownList drpdwnPlaceWork = (DropDownList)row.FindControl("drpdwnPlaceWork");
        //                drpdwnPlaceWork.SelectedValue = TravelToDropDownList.SelectedValue;

                       


        //                string strRate = objBl.Get_Amount_ForDA(txtEmployeeID.Text, strCategory, strToTravel);//DA Calculation

        //                if ((strRate == string.Empty) || (strRate == "") || (strRate == null))
        //                {
        //                    strRate = "0";
        //                }

        //                TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
        //                txtDAgrd.Text = strRate;

        //                //Loging

        //                string strCategory1 = "LODG"; //CategoryDropDownList.SelectedValue;

                        

        //                string strRate1 = objBl.Get_Amount_ForDA(txtEmployeeID.Text, strCategory1, strToTravel);//DA Calculation

        //                if ((strRate1 == string.Empty) || (strRate1 == "") || (strRate1 == null))
        //                {
        //                    strRate1 = "0";
        //                }

        //                TextBox txtBillAmount = (TextBox)row.FindControl("txtBillAmount");
        //                txtBillAmount.Text = strRate1;


        //                DropDownList drpdwnNightHalt = (DropDownList)row.FindControl("drpdwnNightHalt");

        //                ////drpdwnNightHalt.SelectedValue = TravelToDropDownList.SelectedValue;
                       
        //                string strNightHalt = drpdwnNightHalt.SelectedValue;
        //                if (strNightHalt == "No")
        //                {
        //                    txtBillAmount.Text = "0";
        //                }


        //                TextBox txtFare = (TextBox)row.FindControl("txtFare");
        //                double iFare = Convert.ToDouble(string.Format("{0:0.00}",(txtFare.Text);
                                    
        //                double iDA = Convert.ToDouble(string.Format("{0:0.00}",(txtDAgrd.Text);

                                    
        //                double iLodging = Convert.ToDouble(string.Format("{0:0.00}",(txtBillAmount.Text);

        //                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
        //                txtTotal.Text = Convert.ToString(iFare+ iDA + iLodging);
                               

        //            }
        //        }
        //    }

           

        //}

        protected void drpdwnNightHalt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdExpenseStatement.Rows.Count > 0)
                {
                    GridViewRow row1 = (sender as DropDownList).Parent.Parent as GridViewRow;

                    Expensebl objBl = new Expensebl();

                    int a = row1.RowIndex;

                    foreach (GridViewRow row in grdExpenseStatement.Rows)
                    {
                        if (row.RowIndex == a)
                        {
                            DropDownList drpdwnNightHalt = (DropDownList)row.FindControl("drpdwnNightHalt");
                            string strNightHalt = drpdwnNightHalt.SelectedValue;
                            if (strNightHalt == "Yes")
                            {
                                //Boarding
                                //DropDownList CategoryDropDownList = (DropDownList)row.FindControl("CategoryDropDownList");
                                string strCategory = "BORD"; //CategoryDropDownList.SelectedValue;

                                DropDownList TravelToDropDownList = (DropDownList)row.FindControl("TravelToDropDownList");
                                string strToTravel = TravelToDropDownList.SelectedValue;

                                DropDownList drpdwnPlaceWork = (DropDownList)row.FindControl("drpdwnPlaceWork");
                                drpdwnPlaceWork.SelectedValue = TravelToDropDownList.SelectedValue;

                                string strRate = objBl.Get_Amount_ForDA(txtEmployeeID.Text, strCategory, strToTravel);//DA Calculation

                                if ((strRate == string.Empty) || (strRate == "") || (strRate == null))
                                {
                                    strRate = "0";
                                }

                                TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
                                txtDAgrd.Text = strRate;

                                //Loging

                                string strCategory1 = "LODG"; //CategoryDropDownList.SelectedValue;



                                string strRate1 = objBl.Get_Amount_ForDA(txtEmployeeID.Text, strCategory1, strToTravel);//DA Calculation

                                if ((strRate1 == string.Empty) || (strRate1 == "") || (strRate1 == null))
                                {
                                    strRate1 = "0";
                                }

                                TextBox txtBillAmount = (TextBox)row.FindControl("txtBillAmount");
                                txtBillAmount.Text = strRate1;



                                ////DropDownList drpdwnNightHalt = (DropDownList)row.FindControl("drpdwnNightHalt");


                                ////string strNightHalt = drpdwnNightHalt.SelectedValue;
                                ////if (strNightHalt == "No")
                                ////{
                                ////    txtBillAmount.Text = "0";
                                ////}


                                TextBox txtFare = (TextBox)row.FindControl("txtFare");
                                double iFare = Convert.ToDouble(string.Format("{0:0.00}", (txtFare.Text)));

                                double iDA = Convert.ToDouble(string.Format("{0:0.00}", (txtDAgrd.Text)));


                                double iLodging = Convert.ToDouble(string.Format("{0:0.00}", (txtBillAmount.Text)));

                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                txtTotal.Text = Convert.ToString(iFare + iDA + iLodging);
                            }

                            else
                            {
                                //Boarding
                                //DropDownList CategoryDropDownList = (DropDownList)row.FindControl("CategoryDropDownList");
                                string strCategory = "BORD"; //CategoryDropDownList.SelectedValue;

                                DropDownList TravelToDropDownList = (DropDownList)row.FindControl("TravelToDropDownList");
                                string strToTravel = TravelToDropDownList.SelectedValue;

                                DropDownList drpdwnPlaceWork = (DropDownList)row.FindControl("drpdwnPlaceWork");
                                drpdwnPlaceWork.SelectedValue = TravelToDropDownList.SelectedValue;

                                string strRate = objBl.Get_Amount_ForDA(txtEmployeeID.Text, strCategory, strToTravel);//DA Calculation

                                if ((strRate == string.Empty) || (strRate == "") || (strRate == null))
                                {
                                    strRate = "0";
                                }

                                TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
                                txtDAgrd.Text = strRate;

                                //Loging

                                string strCategory1 = "LODG"; //CategoryDropDownList.SelectedValue;



                                string strRate1 = objBl.Get_Amount_ForDA(txtEmployeeID.Text, strCategory1, strToTravel);//DA Calculation

                                if ((strRate1 == string.Empty) || (strRate1 == "") || (strRate1 == null))
                                {
                                    strRate1 = "0";
                                }

                                TextBox txtBillAmount = (TextBox)row.FindControl("txtBillAmount");
                                txtBillAmount.Text = strRate1;



                                ////DropDownList drpdwnNightHalt = (DropDownList)row.FindControl("drpdwnNightHalt");


                                ////string strNightHalt = drpdwnNightHalt.SelectedValue;
                                if (strNightHalt == "No")
                                {
                                    txtBillAmount.Text = "0";
                                }


                                TextBox txtFare = (TextBox)row.FindControl("txtFare");
                                double iFare = Convert.ToDouble(string.Format("{0:0.00}", (txtFare.Text)));

                                double iDA = Convert.ToDouble(string.Format("{0:0.00}", (txtDAgrd.Text)));


                                double iLodging = Convert.ToDouble(string.Format("{0:0.00}", (txtBillAmount.Text)));

                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                txtTotal.Text = Convert.ToString(iFare + iDA + iLodging);
                            }

                            ScriptManager manager = ScriptManager.GetCurrent(this);
                            manager.SetFocus(drpdwnNightHalt);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            lblgvtot.Visible = true;
            try
            {
                if (grdExpenseStatement.Rows.Count > 0)
                {
                    double iToTal = 0;
                    double totFare = 0, totDABoard = 0, totDALodg = 0, totgrid = 0;

                    for (int i = 0; i <= grdExpenseStatement.Rows.Count - 1; i++)
                    {
                        TextBox txtTotal = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtTotal");
                        iToTal = iToTal + Convert.ToDouble(string.Format("{0:0.00}", (txtTotal.Text)));


                        TextBox txtFare = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtFare");
                        totFare = totFare + Convert.ToDouble(string.Format("{0:0.00}", (txtFare.Text)));

                        TextBox txtDAgrd = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtDAgrd");
                        totDABoard = totDABoard + Convert.ToDouble(string.Format("{0:0.00}", (txtDAgrd.Text)));

                        TextBox txtBillAmount = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtBillAmount");
                        totDALodg = totDALodg + Convert.ToDouble(string.Format("{0:0.00}", (txtBillAmount.Text)));

                    }



                    lblTotfare.Text = Convert.ToString(totFare);
                    lblTotDABoard.Text = Convert.ToString(totDABoard);
                    lblTotDALodg.Text = Convert.ToString(totDALodg);
                    lblTotGrid.Text = Convert.ToString(iToTal);






                    double iStationary = Convert.ToDouble(string.Format("{0:0.00}", (txtStationaries.Text)));
                    double iCourier = Convert.ToDouble(string.Format("{0:0.00}", (txtCourier.Text)));
                    double iPaT = Convert.ToDouble(string.Format("{0:0.00}", (txtPAndT.Text)));
                    double iEmail = Convert.ToDouble(string.Format("{0:0.00}", (txtEmail.Text)));
                    double iComp = Convert.ToDouble(string.Format("{0:0.00}", (txtCompitatorProductPurchase.Text)));
                    double iOthers = Convert.ToDouble(string.Format("{0:0.00}", (txtOthers.Text)));
                    double iMarket = Convert.ToDouble(string.Format("{0:0.00}", (txtMarketDevelopmentExpences.Text)));
                    double iBusPass = Convert.ToDouble(string.Format("{0:0.00}", (txtBusPass.Text)));
                    double iConvey = Convert.ToDouble(string.Format("{0:0.00}", (txtConveyance.Text)));
                    double iJcMeetings = Convert.ToDouble(string.Format("{0:0.00}", (txtJcMeetings.Text)));

                    double iTT = iStationary + iCourier + iPaT + iEmail + iComp + iOthers + iMarket + iBusPass + iConvey + iJcMeetings;

                    txtGrandTotal.Text = Convert.ToString(iToTal + iTT);
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        //protected void StateDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (grdExpenseStatement.Rows.Count > 0)
        //    {
        //        GridViewRow row1 = (sender as DropDownList).Parent.Parent as GridViewRow;

        //        Expensebl objBl = new Expensebl();

        //        int a = row1.RowIndex;

        //        foreach (GridViewRow row in grdExpenseStatement.Rows)
        //        {
        //            if (row.RowIndex == a)
        //            {
        //                DropDownList StateDropDownList = (DropDownList)row.FindControl("StateDropDownList");
        //                string statecode = StateDropDownList.SelectedValue;

        //                DropDownList drpdwnPlaceWork = (DropDownList)row.FindControl("drpdwnPlaceWork");
        //                loadfrom(drpdwnPlaceWork, statecode);

        //                ////DropDownList drpdwnNightHalt = (DropDownList)row.FindControl("drpdwnNightHalt");
        //                ////loadfrom(drpdwnNightHalt,statecode);
        //                ////loadNightHalt(drpdwnNightHalt);

        //                DropDownList ddlFrom = (DropDownList)row.FindControl("TravelFromDropDownList");
        //                loadFromplace(ddlFrom);

        //                DropDownList ddlTo = (DropDownList)row.FindControl("TravelToDropDownList");
        //                loadfrom(ddlTo, statecode);

        //            }
        //        }
        //    }



        //}

        protected void txtDistance_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdExpenseStatement.Rows.Count > 0)
                {
                    Expensebl objBl = new Expensebl();

                    GridViewRow row1 = (sender as TextBox).Parent.Parent as GridViewRow;

                    int a = row1.RowIndex;

                    foreach (GridViewRow row in grdExpenseStatement.Rows)
                    {
                        if (row.RowIndex == a)
                        {
                            DropDownList drpdwnMode = (DropDownList)row.FindControl("drpdwnMode");
                            string strMode = drpdwnMode.SelectedItem.Text;

                            ScriptManager manager = ScriptManager.GetCurrent(this);
                            manager.SetFocus(drpdwnMode);


                            if (strMode == "Road")
                            {

                                //DropDownList StateDropDownList = (DropDownList)row.FindControl("StateDropDownList");
                                //string strState = StateDropDownList.SelectedItem.Text;

                                //TextBox txtState = (TextBox)row.FindControl("txtState");
                                string strState = txtState.Text;

                                string strRate = objBl.Get_Rate_Fare_ForDistance(strState);
                                double iRate = 0;

                                if (!((strRate == string.Empty) || (strRate == "") || (strRate == null)))
                                {
                                    iRate = Convert.ToDouble(string.Format("{0:0.00}", (strRate)));
                                }

                                TextBox txtDistance = (TextBox)row.FindControl("txtDistance");
                                double iDistance = Convert.ToDouble(string.Format("{0:0.00}", (txtDistance.Text)));

                                TextBox txtFare = (TextBox)row.FindControl("txtFare");
                                txtFare.Text = Convert.ToString(iDistance * iRate);
                                double iFare = Convert.ToDouble(string.Format("{0:0.00}", (txtFare.Text)));

                                TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
                                double iDA = Convert.ToDouble(string.Format("{0:0.00}", (txtDAgrd.Text)));

                                TextBox txtLodging = (TextBox)row.FindControl("txtBillAmount");
                                double iLodging = Convert.ToDouble(string.Format("{0:0.00}", (txtLodging.Text)));

                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                txtTotal.Text = Convert.ToString(iFare + iDA + iLodging);
                            }

                            else
                            {

                                DropDownList StateDropDownList = (DropDownList)row.FindControl("StateDropDownList");
                                string strState = StateDropDownList.SelectedItem.Text;


                                //string strRate = objBl.Get_Rate_Fare_ForDistance(strState);
                                //double iRate = 0;

                                //if (!((strRate == string.Empty) || (strRate == "") || (strRate == null)))
                                //{
                                //    iRate = Convert.ToDouble(string.Format("{0:0.00}",(strRate);
                                //}

                                TextBox txtDistance = (TextBox)row.FindControl("txtDistance");
                                double iDistance = Convert.ToDouble(string.Format("{0:0.00}", (txtDistance.Text)));

                                TextBox txtFare = (TextBox)row.FindControl("txtFare");
                                txtFare.Text = "0";
                                double iFare = Convert.ToDouble(string.Format("{0:0.00}", (txtFare.Text)));

                                TextBox txtDAgrd = (TextBox)row.FindControl("txtDAgrd");
                                double iDA = Convert.ToDouble(string.Format("{0:0.00}", (txtDAgrd.Text)));

                                TextBox txtLodging = (TextBox)row.FindControl("txtBillAmount");
                                double iLodging = Convert.ToDouble(string.Format("{0:0.00}", (txtLodging.Text)));

                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                txtTotal.Text = Convert.ToString(iFare + iDA + iLodging);


                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }
        
    }
}
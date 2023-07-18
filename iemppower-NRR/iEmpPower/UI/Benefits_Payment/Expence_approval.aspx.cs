using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using System.Text;
using System.IO;

namespace iEmpPower.UI.Local_requisition
{
    public partial class Expence_approval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblApprovedOn.Text = Convert.ToString(DateTime.Now);
                //LoadDomesticGridViewFirstRow();
                                 
                //var xmlFilePath = Server.MapPath("~/UI/Benefits_Payment/XMLFile.xml");//Location of the XML file.
                //DataSet dsCameraDetails = new DataSet();
                //dsCameraDetails.ReadXml(xmlFilePath);// Load XML in dataset
                //grdOtherDetails.DataSource = dsCameraDetails.Tables[0].DefaultView;
                //grdOtherDetails.DataBind();// Bind the DataSet to Grid that will display all xml data.

                lblDate.Text = System.DateTime.Today.ToShortDateString();
                Get_ExpenseDetails();
            }
            

        }

        public void Get_ExpenseDetails()
        {

            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();

            Expensecollection objList = new Expensecollection();
            foreach (var vRow in objExpenseDataContext.sp_Get_Inward_Entry())
            {
                Expensebo objEXPAddBo = new Expensebo();

                objEXPAddBo.PERNR = vRow.PERNR;
                objEXPAddBo.ENAME = vRow.ENAME;
                objEXPAddBo.WERKS = vRow.WERKS;
                objEXPAddBo.PLSXT = vRow.PLSXT;
                objEXPAddBo.STEXT = vRow.STEXT;
                objEXPAddBo.REPORT_TO = vRow.REPORT_TO;
                objEXPAddBo.BEGDA = Convert.ToDateTime(vRow.BEGDA);
                objEXPAddBo.ENDDA = Convert.ToDateTime(vRow.ENDDA);
                objEXPAddBo.TA_DEDUCTION = vRow.TA_DEDUCTION;
                objEXPAddBo.DA_DEDUCTION = vRow.DA_DEDUCTION;
                objEXPAddBo.OTR_EXP_DEDUCT = vRow.OTR_EXP_DEDUCT;
                objEXPAddBo.GRAND_TOTAL = vRow.GRAND_TOTAL;
                objEXPAddBo.TOT_DEDUC = vRow.TOT_DEDUC;
                objEXPAddBo.AMOUNT_ALLOWED = vRow.AMOUNT_ALLOWED;
                objEXPAddBo.Status = vRow.Status;
                objEXPAddBo.PANDT = vRow.PANDT;

                objList.Add(objEXPAddBo);
            }
            objExpenseDataContext.Dispose();

            grdSearch.DataSource = objList;
            grdSearch.AllowPaging = false;
            grdSearch.DataBind();
            lblTotalRecords.Text = grdSearch.Rows.Count.ToString();
            grdSearch.AllowPaging = true;
            grdSearch.DataBind();
        }

        //protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grdOtherDetails.PageIndex = e.NewPageIndex;
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

                DropDownList ddlFrom = (DropDownList)grdExpenseStatement.Rows[i].FindControl("ModeDropDownList");
                loadfrom(ddlFrom);               
            }
        }

        protected void loadfrom(DropDownList ddl)
        {
            travelrequestbl objtravelrequestbl = new travelrequestbl();
            ddl.DataSource = objtravelrequestbl.GetRegionName();
            ddl.DataTextField = "REGION_TEXT25_FROM";
            ddl.DataValueField = "REGION_RGION_FROM";
            ddl.DataBind();
            ListItem drpDefaultItem3 = new ListItem("", "0", true);
            ddl.Items.Add(drpDefaultItem3);
            ddl.SelectedValue = "0";
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                funLoadExpense_Statement_Details_ForApproval();
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        public void FuncClearData()
        {
            try
            {
                //Basic Info
                txtEmployeeID.Text = "";
                txtEmpName.Text = "";
                txtDesignation.Text = "";
                txtReportTo.Text = "";
                txtFromDate.Text = "";
                txtToDate.Text = "";

                //DA Deductions
                txtActualAmtClaimed.Text  = "";
                txtInchargePersonDeductions .Text  = "";
                txtCorOfficeDeduction.Text  = "";
                txtTotalDeduction .Text  = "";
                txtAmntAllowed.Text = "";

                //Other Details
                txtStationaries.Text = "";
                txtCourier.Text = "";
                txtPAndT.Text = "";
                txtEmail.Text = "";
                txtCompitatorProductPurchase.Text = "";
                txtOthers.Text = "";
                txtMarketDevelopmentExpences.Text = "";
                txtBusPass.Text = "";
                txtConveyance.Text = "";
                txtJcMeetings.Text = "";

                //txtGrandTotal.Text = "";

                divBasicInfo.Visible = false;
                job.Visible = false;
                job1.Visible = false;
                job2.Visible = false;
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
                txtDesignation.Text = "";
                txtReportTo.Text = "";
                //txtFromDate.Text = "";
                //txtToDate.Text = "";

                //DA Deductions
                txtActualAmtClaimed.Text = "";
                txtInchargePersonDeductions.Text = "";
                txtCorOfficeDeduction.Text = "";
                txtTotalDeduction.Text = "";
                txtAmntAllowed.Text = "";

                //Other Details
                txtStationaries.Text = "";
                txtCourier.Text = "";
                txtPAndT.Text = "";
                txtEmail.Text = "";
                txtCompitatorProductPurchase.Text = "";
                txtOthers.Text = "";
                txtMarketDevelopmentExpences.Text = "";
                txtBusPass.Text = "";
                txtConveyance.Text = "";
                txtJcMeetings.Text = "";

                //txtGrandTotal.Text = "";

                divBasicInfo.Visible = true;
                job.Visible = true;
                job1.Visible = true;
                job2.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        public void funGet_Rate_ExpenseType()
        {
            Expensebl objBl = new Expensebl();

            string strStRate = objBl.Get_Amount_ForExpenseType(txtEmployeeID.Text , "STNR");
            if ((strStRate == string.Empty) || (strStRate == "") || (strStRate == null))
            {
                strStRate = "0";
            } 
            ////lblAllowedStationaries.Text = strStRate;
            txtAllowedStationaries.Text = strStRate;

            string strCoRate = objBl.Get_Amount_ForExpenseType(txtEmployeeID.Text, "CORY");
            if ((strCoRate == string.Empty) || (strCoRate == "") || (strCoRate == null))
            {
                strCoRate = "0";
            } 
            ////lblAllowedCourier.Text   = strCoRate;
            txtAllowedCourier.Text = strCoRate;

            string strPaTRate = objBl.Get_Amount_ForExpenseType(txtEmployeeID.Text, "PNTS");
            if ((strPaTRate == string.Empty) || (strPaTRate == "") || (strPaTRate == null))
            {
                strPaTRate = "0";
            } 
            ////lblAllowedPAndT.Text  = strPaTRate;
            txtAllowedPAndT.Text = strPaTRate;

            string strEmailRate = objBl.Get_Amount_ForExpenseType(txtEmployeeID.Text, "EMAL");
            if ((strEmailRate == string.Empty) || (strEmailRate == "") || (strEmailRate == null))
            {
                strEmailRate = "0";
            } 
            ////lblAllowedEmail.Text = strEmailRate;
            txtAllowedEmail.Text = strEmailRate;

            string strCompProdPurchaseRate = objBl.Get_Amount_ForExpenseType(txtEmployeeID.Text, "CPPS");
            if ((strCompProdPurchaseRate == string.Empty) || (strCompProdPurchaseRate == "") || (strCompProdPurchaseRate == null))
            {
                strCompProdPurchaseRate = "0";
            } 
            ////lblAllowedCompitatorProductPurchase.Text = strCompProdPurchaseRate;
            txtAllowedCompitatorProductPurchase.Text = strCompProdPurchaseRate;

            string strOthers = objBl.Get_Amount_ForExpenseType(txtEmployeeID.Text, "OTHR");
            if ((strOthers == string.Empty) || (strOthers == "") || (strOthers == null))
            {
                strOthers = "0";
            } 
            ////lblAllowedOthers.Text = strOthers;
            txtAllowedOthers.Text = strOthers;

            string strMarketDevExp = objBl.Get_Amount_ForExpenseType(txtEmployeeID.Text, "MDEX");
            if ((strMarketDevExp == string.Empty) || (strMarketDevExp == "") || (strMarketDevExp == null))
            {
                strMarketDevExp = "0";
            } 
            ////lblAllowedMarketDevelopmentExpences.Text = strMarketDevExp;
            txtAllowedMarketDevelopmentExpences.Text = strMarketDevExp;

            string strBusPass = objBl.Get_Amount_ForExpenseType(txtEmployeeID.Text, "BPAS");
            if ((strBusPass == string.Empty) || (strBusPass == "") || (strBusPass == null))
            {
                strBusPass = "0";
            } 
            ////lblAllowedBusPass.Text = strBusPass;
            txtAllowedBusPass.Text = strBusPass;

            string strConveyance = objBl.Get_Amount_ForExpenseType(txtEmployeeID.Text, "CCVY");
            if ((strConveyance == string.Empty) || (strConveyance == "") || (strConveyance == null))
            {
                strConveyance = "0";
            } 
            ////lblAllowedConveyance.Text = strConveyance;
            txtAllowedConveyance.Text = strConveyance;

            string strJcMeetings = objBl.Get_Amount_ForExpenseType(txtEmployeeID.Text, "JCME");
            if ((strJcMeetings == string.Empty) || (strJcMeetings == "") || (strJcMeetings == null))
            {
                strJcMeetings = "0";
            } 
            ////lblAllowedJcMeetings.Text = strJcMeetings;
            txtAllowedJcMeetings.Text = strJcMeetings;
        }


        public void funLoadExpense_Statement_Details_ForApproval()
        {
            try
            {
                lblgvtot.Visible = true;
                int icnt = 0;
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

                    if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                    {
                        lblMessageBoard.Text = "Please select proper Date";
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    funGet_Rate_ExpenseType();
                    
                    Expensebl objExpBl = new Expensebl();
                    Expensecollection objLstExp = new Expensecollection();
                    Expense_statementcollection objLstExpStatmnt = new Expense_statementcollection();

                    objLstExp = objExpBl.Get_ExpenseDetails(strPernr, Convert.ToDateTime(strFromDate), Convert.ToDateTime(strToDate));

                    if (objLstExp.Count > 0)
                    {
                        foreach (var vrow in objLstExp)
                        {
                            //Basic Info
                            txtEmpName.Text = vrow.ENAME;
                            txtDesignation.Text = vrow.PLSXT;
                            txtReportTo.Text = vrow.REPORT_TO;
                            txtApprovedBy.Text = vrow.approved_by;
                            lblApprovedOn.Text=vrow.approved_on.ToString();


                            if (vrow.Status != "Review")
                            {
                                chkApproved.Checked = true;
                            }

                            //DA Deductions
                            txtActualAmtClaimed.Text = vrow.GRAND_TOTAL;
                            txtInchargePersonDeductions.Text  = vrow.TOT_INCHARGE_PERSON_DEDUC;
                            txtCorOfficeDeduction.Text = vrow.CORPORATE_OFFICE_DEDUC;
                            txtTotalDeduction.Text = vrow.TOT_DEDUC;
                            txtAmntAllowed.Text = vrow.AMOUNT_ALLOWED;
                            
                            //Other Details
                            txtStationaries.Text = vrow.STATIONARY;
                            txtCourier.Text = vrow.COURIER;
                            txtPAndT.Text = vrow.PANDT;
                            txtEmail.Text = vrow.EMAIL;
                            txtCompitatorProductPurchase.Text = vrow.COMP_PRODUCT_PURCHASE;
                            txtOthers.Text = vrow.OTHERS;
                            txtMarketDevelopmentExpences.Text = vrow.MARK_DEVELOP_EXPENCE;
                            txtBusPass.Text = vrow.BUS_PASS;
                            txtConveyance.Text = vrow.CONVEYANCE;
                            txtJcMeetings.Text = vrow.JC_MEETINGS;

                            //Newly added for Disallowed 
                            lblDisAllowedStationaries.Text = (Convert.ToDouble(txtStationaries.Text) - Convert.ToDouble(txtAllowedStationaries.Text)).ToString();
                            lblDisAllowedCourier.Text = (Convert.ToDouble(txtCourier.Text) - Convert.ToDouble(txtAllowedCourier.Text)).ToString();
                            lblDisAllowedPAndT.Text = (Convert.ToDouble(txtPAndT.Text) - Convert.ToDouble(txtAllowedPAndT.Text)).ToString();
                            lblDisAllowedEmail.Text = (Convert.ToDouble(txtEmail.Text) - Convert.ToDouble(txtAllowedEmail.Text)).ToString();
                            lblDisAllowedCompitatorProductPurchase.Text = (Convert.ToDouble(txtCompitatorProductPurchase.Text) - Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)).ToString();
                            lblDisAllowedOthers.Text = (Convert.ToDouble(txtOthers.Text) - Convert.ToDouble(txtAllowedOthers.Text)).ToString();
                            lblDisAllowedMarketDevelopmentExpences.Text = (Convert.ToDouble(txtMarketDevelopmentExpences.Text) - Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text)).ToString();
                            lblDisAllowedBusPass.Text = (Convert.ToDouble(txtBusPass.Text) - Convert.ToDouble(txtAllowedBusPass.Text)).ToString();
                            lblDisAllowedConveyance.Text = (Convert.ToDouble(txtConveyance.Text) - Convert.ToDouble(txtAllowedConveyance.Text)).ToString();
                            lblDisAllowedJcMeetings.Text = (Convert.ToDouble(txtJcMeetings.Text) - Convert.ToDouble(txtAllowedJcMeetings.Text)).ToString();

                            txtTotal.Text = (Convert.ToDouble(txtStationaries.Text) + Convert.ToDouble(txtCourier.Text) + Convert.ToDouble(txtPAndT.Text) +
                                Convert.ToDouble(txtEmail.Text) + Convert.ToDouble(txtCompitatorProductPurchase.Text) + Convert.ToDouble(txtOthers.Text) +
                                Convert.ToDouble(txtMarketDevelopmentExpences.Text) + Convert.ToDouble(txtBusPass.Text) + Convert.ToDouble(txtConveyance.Text) +
                                Convert.ToDouble(txtJcMeetings.Text)).ToString();

                           //// lblAllowed.Text 
                            txtAllowed.Text = (Convert.ToDouble(txtAllowedStationaries.Text) + Convert.ToDouble(txtAllowedCourier.Text) + Convert.ToDouble(txtAllowedPAndT.Text) +
                                Convert.ToDouble(txtAllowedEmail.Text) + Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text) + Convert.ToDouble(txtAllowedOthers.Text) +
                                Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(txtAllowedBusPass.Text) + Convert.ToDouble(txtAllowedConveyance.Text) +
                                Convert.ToDouble(txtAllowedJcMeetings.Text)).ToString();
                            lblDisAllowed.Text = (Convert.ToDouble(lblDisAllowedStationaries.Text) + Convert.ToDouble(lblDisAllowedCourier.Text) + Convert.ToDouble(lblDisAllowedPAndT.Text) +
                                Convert.ToDouble(lblDisAllowedEmail.Text) + Convert.ToDouble(lblDisAllowedCompitatorProductPurchase.Text) + Convert.ToDouble(lblDisAllowedOthers.Text) +
                                Convert.ToDouble(lblDisAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(lblDisAllowedBusPass.Text) + Convert.ToDouble(lblDisAllowedConveyance.Text) +
                                Convert.ToDouble(lblDisAllowedJcMeetings.Text)).ToString();

                            icnt = 1;
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

                            TextBox txtCategory = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtCategory");
                            txtCategory.Text = objLstExpStatmnt.ElementAt(i).CATEGORY.ToString();

                            TextBox txtActualDA = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtActualDA");
                            txtActualDA.Text = objLstExpStatmnt.ElementAt(i).DA.ToString();

                            txtActualDA.Text = (Convert.ToDouble(txtActualDA.Text) + Convert.ToDouble(objLstExpStatmnt.ElementAt(i).BILL_AMOUNT)).ToString();//newly added
                        
                            DropDownList drpdwnMode = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnMode");
                            drpdwnMode.SelectedValue = objLstExpStatmnt.ElementAt(i).ZMODE.ToString();

                            TextBox txtActualTA = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtActualTA");
                            txtActualTA.Text = objLstExpStatmnt.ElementAt(i).FARE.ToString();

                            string strCategory = "BORD";//objLstExpStatmnt.ElementAt(i).CATEGORY.ToString();                            
                            string strToTravel = objLstExpStatmnt.ElementAt(i).TRAVEL_TO.ToString();

                            string strRateDA = objExpBl.Get_Amount_ForDA(txtEmployeeID.Text,strCategory,strToTravel); //To get DA from SAP

                            strCategory = "LODG";

                            strRateDA =(Convert.ToDouble(strRateDA) + Convert.ToDouble(objExpBl.Get_Amount_ForDA(txtEmployeeID.Text, strCategory, strToTravel))).ToString();

                            

                            TextBox txtAllowedDA = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtAllowedDA");
                            txtAllowedDA.Text = strRateDA ;
                            txtAllowedDA.Text = txtActualDA.Text;//delete if masters maintained

                            TextBox txtEligibleDA = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtEligibleDA");
                            txtEligibleDA.Text = (Convert.ToDouble(txtActualDA.Text) - Convert.ToDouble(txtAllowedDA.Text)).ToString();//strRateDA;

                          //  string strRateTA = objExpBl.Get_Rate_Fare_ForDistance(strToTravel); //To get TA from SAP
                            TextBox txtAllowedTA = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtAllowedTA");
                            txtAllowedTA.Text = txtActualTA.Text;

                            TextBox txtEligibleTA = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtEligibleTA");
                            txtEligibleTA.Text = (Convert.ToDouble(txtActualTA.Text) - Convert.ToDouble(txtAllowedTA.Text)).ToString();


                            DropDownList drpdwnRemarks = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnRemarks");
                            drpdwnRemarks.SelectedValue = objLstExpStatmnt.ElementAt(i).REMARKS.ToString();

                        }
                        icnt = 1;
                    }

                }

                if (icnt == 0)
                {
                    FuncClearData();
                    lblMessageBoard.Text = "No records Found";
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }


        protected void btnCancel1_Click(object sender, EventArgs e)
        {
            try
            {
                divBasicInfo.Visible = false;
                job.Visible = false;
                job1.Visible = false;
                job2.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkApproved.Checked == true)
                {
                    if (txtApprovedBy.Text!="")
                    {

                        funApproveExpense();
                    }
                    else
                    {
                        lblMessageBoard.Text = "Approved by cannot left blank";
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMessageBoard.Text = "Check Approved Checkbox";
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
        
        public void funApproveExpense()
        {
            try
            {
                //Basic Info
                string strPERNR = txtEmployeeID.Text;
                string strEmployeeName = txtEmpName.Text;
                string strDesignation = txtDesignation.Text;
                string strReportTo = txtReportTo.Text;
                string strFromDate = txtFromDate.Text;
                string strToDate = txtToDate.Text;

                //DA Deductions
                string strTotalInchgPersonDeductions = txtInchargePersonDeductions.Text ;
                string strCorporateOffcDedcutions = txtCorOfficeDeduction.Text;
                string strTotalDeductions = txtTotalDeduction.Text;
                string strAmountAllowed = txtAmntAllowed.Text;

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

                string strGrandTotal = txtActualAmtClaimed .Text ;

                Expensebo objEXPAddBo = new Expensebo();
                Expensebl objEXPAddBl = new Expensebl();
                Expense_statementbo objEXPStatemntAddBo = new Expense_statementbo();

                objEXPAddBo.PERNR = strPERNR;
                objEXPAddBo.ENAME = strEmployeeName;
                objEXPAddBo.PLSXT = strDesignation;
                objEXPAddBo.REPORT_TO = strReportTo;
                objEXPAddBo.BEGDA = Convert.ToDateTime(strFromDate);
                objEXPAddBo.ENDDA = Convert.ToDateTime(strToDate);
                objEXPAddBo.TOT_INCHARGE_PERSON_DEDUC = strTotalInchgPersonDeductions;
                objEXPAddBo.CORPORATE_OFFICE_DEDUC = strCorporateOffcDedcutions;
                objEXPAddBo.TOT_DEDUC = strTotalDeductions;
                objEXPAddBo.AMOUNT_ALLOWED = strAmountAllowed;
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
                objEXPAddBo.GRAND_TOTAL = strGrandTotal;
                objEXPAddBo.Status = "Approved";
                objEXPAddBo.approved_by = txtApprovedBy.Text;
                objEXPAddBo.approved_on =Convert.ToDateTime(lblApprovedOn.Text);

                bool? dd = true;
                int iCnt = 0;

                objEXPAddBl.Approve_Expense (objEXPAddBo, ref dd);

                if (dd == true)
                { 
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

                                Label lblDate = (Label)grdExpenseStatement.Rows[i].FindControl("lblDate");
                                objEXPStatemntAddBo.DATE1 = Convert.ToDateTime(lblDate.Text.ToString());
                                
                                //DropDownList drpdwnCategory = (DropDownList)grdExpenseStatement.Rows[i].FindControl("CategoryDropDownList");
                                //objEXPStatemntAddBo.CATEGORY = drpdwnCategory.SelectedValue.ToString();
                                TextBox txtCategory = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtCategory");
                                objEXPStatemntAddBo.CATEGORY = txtCategory.Text.ToString();

                                DropDownList drpdwnMode = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnMode");
                                objEXPStatemntAddBo.ZMODE = drpdwnMode.SelectedValue.ToString();

                                TextBox txtActualTA = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtActualTA");
                                objEXPStatemntAddBo.FARE = txtActualTA.Text.ToString();

                                TextBox txtActualDA = (TextBox)grdExpenseStatement.Rows[i].FindControl("txtActualDA");
                                objEXPStatemntAddBo.DA = txtActualDA.Text.ToString();

                                DropDownList drpdwnRemarks = (DropDownList)grdExpenseStatement.Rows[i].FindControl("drpdwnRemarks");
                                objEXPStatemntAddBo.REMARKS = drpdwnRemarks.SelectedValue.ToString(); 

                                objEXPStatemntAddBo.approved_by = txtApprovedBy.Text;
                                objEXPStatemntAddBo.approved_on = Convert.ToDateTime(lblApprovedOn.Text);
                                
                                bool? dd1 = true;
                                objEXPStatemntAddBo.status = "Approved";

                                objEXPAddBl.Approve_Expense_Statement (objEXPStatemntAddBo, ref dd1);

                                if ((dd1 == true) || (dd1 == false))
                                {
                                    iCnt = 1;
                                }
                            }
                        }

                        if (iCnt == 1)
                        {
                            lblMessageBoard.Text = "Expense Approved successfully";
                            lblMessageBoard.ForeColor = System.Drawing.Color.Green;

                            updateinwardentry();
                            Get_ExpenseDetails();

                            string stremployeeEmailid = (string)Session["employeeEmailid"];
                            string strhodEmailid = (string)Session["hodEmailid"];
                            string strBodyMsg = string.Empty;
                            strBodyMsg = "Please check below data <br/><br/>";
                            
                            strBodyMsg += funCreateBody(grdExpenseStatement);
                            strBodyMsg += funCreateBodyNext();

                            string strSubject = string.Empty;
                            strSubject = "Claims Report";

                            iEmpPowerMaster_Load.masterbl.DispatchMailBPO(stremployeeEmailid, User.Identity.Name, strSubject, strhodEmailid, strBodyMsg);     //Email to Employee and HOD

                            FuncClearData(); //to clear controls

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
            catch (Exception ex)
            {
                lblMessageBoard.Text = "UnKnown Error Please Contact Administrator ( " + ex.Message + " )";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        public string funCreateBody(GridView grdAccommRequisition)
        {
            try
            {
               
                string strAccTicketSumm = "";
                
                string strAccEntryTicketSumm = "";
                string body = "";

                if (grdAccommRequisition.Rows.Count > 0)
                {
                    //=====HEADER FOR ACC TICKET===
                    strAccTicketSumm += "<h3>Expense Approved Details.</h3><br />";
                    strAccTicketSumm += "<table style='width:900px;font-size:12px; border:1px solid black; padding:0; border-spacing:1px; '>";
                    strAccTicketSumm += "<tr style='background-color:#907184;color:white;padding:2px;'>";
                    strAccTicketSumm += "<th style='width:10%;'><strong> Date </strong></th>";
                    strAccTicketSumm += "<th style='width:10%;'><strong> Category </strong></th>";
                    strAccTicketSumm += "<th style='width:10%;' ><strong> Actual DA </strong></th>";
                    strAccTicketSumm += "<th style='width:10%;' ><strong> Allowed DA </strong></th>";
                    strAccTicketSumm += "<th style='width:20%;' ><strong> Disallowed DA </strong></th>";
                    strAccTicketSumm += "<th style='width:20%;'><strong> Mode </strong></th>";
                    strAccTicketSumm += "<th style='width:20%;' ><strong> Actual TA</strong></th>";
                    strAccTicketSumm += "<th style='width:10%;' ><strong> Allowed TA </strong></th>";
                    strAccTicketSumm += "<th style='width:10%;' ><strong> Disallowed TA </strong></th>";
                    strAccTicketSumm += "<th style='width:10%;' ><strong> Reasons</strong></th>";
                    strAccTicketSumm += "</tr>";

                    for (int i = 0; i <= grdAccommRequisition.Rows.Count - 1; i++)
                    {
                        Label lblDate = (Label)grdAccommRequisition.Rows[i].FindControl("lblDate");
                        string strDate = lblDate.Text.ToString();
                        TextBox txtCategory = (TextBox)grdAccommRequisition.Rows[i].FindControl("txtCategory");
                        string strCategory = txtCategory.Text.ToString();
                        TextBox txtActualDA = (TextBox)grdAccommRequisition.Rows[i].FindControl("txtActualDA");
                        string strActualDA = txtActualDA.Text.ToString();
                        TextBox txtAllowedDA = (TextBox)grdAccommRequisition.Rows[i].FindControl("txtAllowedDA");
                        string strAllowedDA = txtAllowedDA.Text.ToString();
                        TextBox txtEligibleDA = (TextBox)grdAccommRequisition.Rows[i].FindControl("txtEligibleDA");
                        string strDisallowedDA = txtEligibleDA.Text.ToString();
                        DropDownList drpdwnMode = (DropDownList)grdAccommRequisition.Rows[i].FindControl("drpdwnMode");
                        string strMode = drpdwnMode.SelectedValue.ToString();
                        TextBox txtActualTA = (TextBox)grdAccommRequisition.Rows[i].FindControl("txtActualTA");
                        string strActualTA = txtActualTA.Text.ToString();
                        TextBox txtAllowedTA = (TextBox)grdAccommRequisition.Rows[i].FindControl("txtAllowedTA");
                        string strAllowedTA = txtAllowedTA.Text.ToString();
                        TextBox txtEligibleTA = (TextBox)grdAccommRequisition.Rows[i].FindControl("txtEligibleTA");
                        string strDisallowedTA = txtEligibleTA.Text.ToString();
                        DropDownList drpdwnRemarks = (DropDownList)grdAccommRequisition.Rows[i].FindControl("drpdwnRemarks");
                        string strReasons = drpdwnRemarks.SelectedValue.ToString();

                        //Accommodation Ticket Details html
                        strAccEntryTicketSumm += "<tr style='background-color:#E8E8E8;font-size:11px;'>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strDate + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strCategory + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strActualDA + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strAllowedDA + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strDisallowedDA + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strMode + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strActualTA + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strAllowedTA + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strDisallowedTA + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strReasons + "</td>";
                        strAccEntryTicketSumm += "</tr>";

                       
                    
                    }
                    strAccTicketSumm = strAccTicketSumm + strAccEntryTicketSumm;

                    strAccTicketSumm += "</table>";

                    

                    if (!string.IsNullOrEmpty(strAccEntryTicketSumm))
                    {
                        body += "<tr>";
                        body += "<td colspan='2'>";
                        body += strAccTicketSumm;
                        body += "</td>";
                        body += "</tr>";
                    }
                    
                    

                   

                    body += "</table>";

                }


                return body;
                    
                

            }
            catch (Exception ex)
            {
                //lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                //lblMessageBoard.Text = "Unknown error occured. Please contact your system administrator." + "( " + ex.Message + " )";
                return ex.Message;
            }
        }

        public string funCreateBodyNext()
        {
            try
            {

                string strAccTicketSumm = "";

                string strAccEntryTicketSumm = "";

                string strAccTicketSumm1 = "";

                string strAccEntryTicketSumm1 = "";
                string body = "";

                    strAccTicketSumm += "<br />";
                    strAccTicketSumm += "<h3>Other Claims</h3><br />";
                    strAccTicketSumm += "<table style='width:900px;font-size:12px; border:1px solid black; padding:0; border-spacing:1px; '>";
                    strAccTicketSumm += "<tr style='background-color:#907184;color:white;padding:2px;'>";
                    strAccTicketSumm += "<th style='width:10%;'><strong> Actual</strong></th>";
                    strAccTicketSumm += "<th style='width:10%;'><strong> Allowed </strong></th>";
                    strAccTicketSumm += "<th style='width:10%;' ><strong> Disallowed </strong></th>";
              
                    strAccTicketSumm += "</tr>";

                    
                        string strTotal=txtTotal.Text;
                        string strAllowed = txtAllowed.Text; ////lblAllowed.Text;
                        string strDisallowed=lblDisAllowed.Text;
                        

                        
                        strAccEntryTicketSumm += "<tr style='background-color:#E8E8E8;font-size:11px;'>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strTotal + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strAllowed + "</td>";
                        strAccEntryTicketSumm += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strDisallowed + "</td>";
                       
                        strAccEntryTicketSumm += "</tr>";



                    
                    strAccTicketSumm = strAccTicketSumm + strAccEntryTicketSumm;

                    strAccTicketSumm += "</table>";



                    strAccTicketSumm1 += "<br />";
                    strAccTicketSumm1 += "<h3>Grand Total</h3><br />";
                    strAccTicketSumm1 += "<table style='width:900px;font-size:12px; border:1px solid black; padding:0; border-spacing:1px; '>";
                    strAccTicketSumm1 += "<tr style='background-color:#907184;color:white;padding:2px;'>";
                    strAccTicketSumm1 += "<th style='width:10%;'><strong> Actual Amount Claimed</strong></th>";
                    strAccTicketSumm1 += "<th style='width:10%;'><strong> Deduction Recommendation from Reporting Officer </strong></th>";
                    strAccTicketSumm1 += "<th style='width:10%;' ><strong> Eligibility Deduction </strong></th>";
                    strAccTicketSumm1 += "<th style='width:10%;' ><strong> Total Deduction </strong></th>";
                    strAccTicketSumm1 += "<th style='width:10%;' ><strong> Amount Allowed </strong></th>";
                    strAccTicketSumm1 += "</tr>";

                    
                        string strActualClaimed=txtActualAmtClaimed.Text;
                        string strDisRO=txtInchargePersonDeductions.Text;
                        string strEligiDeduc=txtCorOfficeDeduction.Text;
                        string strDeduction=txtTotalDeduction.Text;
                        string strAmtAllowed=txtAmntAllowed.Text;


                        
                        strAccEntryTicketSumm1 += "<tr style='background-color:#E8E8E8;font-size:11px;'>";
                        strAccEntryTicketSumm1 += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strActualClaimed + "</td>";
                        strAccEntryTicketSumm1 += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strDisRO + "</td>";
                        strAccEntryTicketSumm1 += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strEligiDeduc + "</td>";
                        strAccEntryTicketSumm1 += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strDeduction + "</td>";
                        strAccEntryTicketSumm1 += "<td style='white-space:pre-wrap; word-wrap:normal; word-break:break-all;'>" + strAmtAllowed + "</td>";
                        strAccEntryTicketSumm1 += "</tr>";



                    
                    strAccTicketSumm1= strAccTicketSumm1 + strAccEntryTicketSumm1;

                    strAccTicketSumm1 += "</table>";


                    if (!string.IsNullOrEmpty(strAccEntryTicketSumm) && !string.IsNullOrEmpty(strAccEntryTicketSumm1))
                    {
                        body += "<tr>";
                        body += "<td colspan='2'>";
                        body += strAccTicketSumm;
                        body += strAccTicketSumm1;
                        body += "</td>";
                        body += "</tr>";
                    }





                    body += "</table>";

                


                return body;



            }
            catch (Exception ex)
            {
                //lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                //lblMessageBoard.Text = "Unknown error occured. Please contact your system administrator." + "( " + ex.Message + " )";
                return ex.Message;
            }
        }


        public void updateinwardentry()
        {
            ExpenseDataContext objExpenseDataContext = new ExpenseDataContext();
            objExpenseDataContext.sp_Update_Expense_Inward_Entry(txtEmployeeID.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), "Approved");
            objExpenseDataContext.Dispose();
        }

        protected void txtCorOfficeDeduction_TextChanged(object sender, EventArgs e)
        {
            string strInchg = txtInchargePersonDeductions .Text ;
            string strCorpo = txtCorOfficeDeduction.Text ;

            double  iInchg = 0;
            double  iCorpo = 0;

            if (!((strInchg == "") || (strInchg == string.Empty) || (strInchg == null)))
            {
                iInchg = Convert.ToDouble (strInchg);
            }

            if (!((strCorpo == "") || (strCorpo == string.Empty) || (strCorpo == null)))
            {
                iCorpo = Convert.ToDouble(strCorpo);
            }

            double  iTT = iInchg + iCorpo;
            txtTotalDeduction.Text  = Convert.ToString(iTT);

            double iAmtcl = Convert.ToDouble (txtActualAmtClaimed.Text);
            txtAmntAllowed.Text = Convert.ToString(iAmtcl - iTT);               

        }

        protected void chkbxDA_CheckedChanged(object sender, EventArgs e)
        {
            if (grdExpenseStatement.Rows.Count > 0)
            {
                Expensebl objBl = new Expensebl();

                GridViewRow row1 = (sender as CheckBox).Parent.Parent as GridViewRow;

                int a = row1.RowIndex;

                foreach (GridViewRow row in grdExpenseStatement.Rows)
                {
                    if (row.RowIndex == a)
                    {
                        TextBox txtActualDA = (TextBox)row.FindControl("txtActualDA");
                        string strActualDA = txtActualDA.Text;

                        TextBox txtEligibleDA = (TextBox)row.FindControl("txtEligibleDA");
                        string strEligibleDA = txtEligibleDA.Text;

                        TextBox txtAllowedDA = (TextBox)row.FindControl("txtAllowedDA");
                        //string strAllowedDA = txtAllowedDA.Text ;

                        CheckBox chkbxDA = (CheckBox)row.FindControl("chkbxDA");
                        if (chkbxDA.Checked == true)
                        {
                            txtAllowedDA.Text = strActualDA;
                        }
                        else
                        {
                            txtAllowedDA.Text = strEligibleDA;
                        }                  
                    }
                }
            }
        }

        protected void chkbxTA_CheckedChanged(object sender, EventArgs e)
        {
            if (grdExpenseStatement.Rows.Count > 0)
            {
                Expensebl objBl = new Expensebl();

                GridViewRow row1 = (sender as CheckBox).Parent.Parent as GridViewRow;

                int a = row1.RowIndex;

                foreach (GridViewRow row in grdExpenseStatement.Rows)
                {
                    if (row.RowIndex == a)
                    {
                        TextBox txtActualTA = (TextBox)row.FindControl("txtActualTA");
                        string strActualTA = txtActualTA.Text;

                        TextBox txtAllowedTA = (TextBox)row.FindControl("txtAllowedTA");
                        string strAllowedTA = txtAllowedTA.Text;

                        CheckBox chkbxDA = (CheckBox)row.FindControl("chkbxTA");
                        if (chkbxDA.Checked == true)
                        {
                            txtAllowedTA.Text = strActualTA;
                        }
                    }
                }
            }
        }

        protected void btnChkDateTransacReport_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        

        protected void grdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdSearch.PageIndex = e.NewPageIndex;

            Get_ExpenseDetails();
            grdSearch.SelectedIndex = -1;
        }

        protected void chkApproved_CheckedChanged(object sender, EventArgs e)
        {
            lblApprovedOn.Text = Convert.ToString(DateTime.Now);
        }

        //protected override void OnPreRender(EventArgs e)
        //    {

        //          // add base.OnPreRender(e); at the beginning of the method.

        //                                   base.OnPreRender(e);

 

        //           // codes to handle with your controls.


        //    }

        protected void grdSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessageBoard.Text = "";
            //try
            //{
                BindControls();
            //}
            //catch (Exception ex)
            //{
            //    string error = ex.ToString();
            //}
        }

        protected void BindControls()
        {
            GridViewRow grdRow = grdSearch.SelectedRow;
            string eid = grdRow.Cells[0].Text.ToString();
            string fromdate = grdRow.Cells[2].Text.ToString();
            string todate = grdRow.Cells[3].Text.ToString();

            txtEmployeeID.Text = eid;
            txtFromDate.Text = fromdate;
            txtToDate.Text = todate;
            
        }

        protected void grdSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdSearch, "Select$" + e.Row.RowIndex);
            }
        }


        protected void txtAllowedDA_TextChanged(object sender, EventArgs e)
        {
            if (grdExpenseStatement.Rows.Count > 0)
            {
                GridViewRow row1 = (sender as TextBox).Parent.Parent as GridViewRow;

                int a = row1.RowIndex;

                foreach (GridViewRow row in grdExpenseStatement.Rows)
                {
                    if (row.RowIndex == a)
                    {
                        TextBox txtActualDA = (TextBox)row.FindControl("txtActualDA");
                        double ActualDA = Convert.ToDouble(txtActualDA.Text);

                        TextBox txtAllowedDA = (TextBox)row.FindControl("txtAllowedDA");
                        double AllowedDA = Convert.ToDouble(txtAllowedDA.Text);

                        TextBox txtEligibleDA = (TextBox)row.FindControl("txtEligibleDA");

                        txtEligibleDA.Text = Convert.ToString(ActualDA - AllowedDA);

                       
                    }
                }
            }
        }

        protected void txtAllowedTA_TextChanged(object sender, EventArgs e)
        {
            if (grdExpenseStatement.Rows.Count > 0)
            {
                GridViewRow row1 = (sender as TextBox).Parent.Parent as GridViewRow;

                int a = row1.RowIndex;

                foreach (GridViewRow row in grdExpenseStatement.Rows)
                {
                    if (row.RowIndex == a)
                    {
                        TextBox txtActualTA = (TextBox)row.FindControl("txtActualTA");
                        double ActualTA = Convert.ToDouble(txtActualTA.Text);

                        TextBox txtAllowedTA = (TextBox)row.FindControl("txtAllowedTA");
                        double AllowedTA = Convert.ToDouble(txtAllowedTA.Text);

                        TextBox txtEligibleTA = (TextBox)row.FindControl("txtEligibleTA");

                        txtEligibleTA.Text = Convert.ToString(ActualTA - AllowedTA);

                       
                    }
                }
            }
        }
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            string strInchg = txtInchargePersonDeductions.Text;
            double disallowed_amount = 0;
            double ActDATot=0,AllDATot=0,DisDATot=0,ActTATot=0,AllTATot=0,DisTATot=0;


            if (grdExpenseStatement.Rows.Count > 0)
            {


                foreach (GridViewRow row in grdExpenseStatement.Rows)
                {


                        TextBox txtEligibleDA = (TextBox)row.FindControl("txtEligibleDA");
                        TextBox txtEligibleTA = (TextBox)row.FindControl("txtEligibleTA");

                        disallowed_amount +=Convert.ToDouble(txtEligibleDA.Text);
                        disallowed_amount += Convert.ToDouble(txtEligibleTA.Text);


                        TextBox txtActualDA = (TextBox)row.FindControl("txtActualDA");
                        ActDATot+=Convert.ToDouble(txtActualDA.Text);
                        TextBox txtAllowedDA = (TextBox)row.FindControl("txtAllowedDA");
                        AllDATot+=Convert.ToDouble(txtAllowedDA.Text);
                        DisDATot+=Convert.ToDouble(txtEligibleDA.Text);


                        TextBox txtActualTA = (TextBox)row.FindControl("txtActualTA");
                        ActTATot+=Convert.ToDouble(txtActualTA.Text);
                        TextBox txtAllowedTA = (TextBox)row.FindControl("txtAllowedTA");
                        AllTATot+=Convert.ToDouble(txtAllowedTA.Text);
                        DisTATot+=Convert.ToDouble(txtEligibleTA.Text);

                }
            }


            lblActDATot.Text = ActDATot.ToString();
            lblAllDATot.Text = AllDATot.ToString();
            lblDisDATot.Text = DisDATot.ToString();



            lblActTATot.Text = ActTATot.ToString();
            lblAllTATot.Text = AllTATot.ToString();
            lblDisTATot.Text = DisTATot.ToString();





            txtCorOfficeDeduction.Text = disallowed_amount.ToString();


            string strCorpo = txtCorOfficeDeduction.Text;
            string strOtherclaimdisallowed = lblDisAllowed.Text;

            double iInchg = 0;
            double iCorpo = 0;
            double ocd = 0;

            if (!((strInchg == "") || (strInchg == string.Empty) || (strInchg == null)))
            {
                iInchg = Convert.ToDouble(strInchg);
            }

            if (!((strCorpo == "") || (strCorpo == string.Empty) || (strCorpo == null)))
            {
                iCorpo = Convert.ToDouble(strCorpo);
            }

            if (!((strOtherclaimdisallowed == "") || (strOtherclaimdisallowed == string.Empty) || (strOtherclaimdisallowed == null)))
            {
                ocd = Convert.ToDouble(strOtherclaimdisallowed);
            }

            double iTT = iInchg + iCorpo + ocd;
            txtTotalDeduction.Text = Convert.ToString(iTT);

            double iAmtcl = Convert.ToDouble(txtActualAmtClaimed.Text);
            txtAmntAllowed.Text = Convert.ToString(iAmtcl - iTT);     
        }

        protected void txtAllowedStationaries_TextChanged(object sender, EventArgs e)
        {
            lblDisAllowedStationaries.Text = (Convert.ToDouble(txtStationaries.Text) - Convert.ToDouble(txtAllowedStationaries.Text)).ToString();
            txtAllowed.Text= (Convert.ToDouble(txtAllowedStationaries.Text)+Convert.ToDouble(txtAllowedCourier.Text)+ Convert.ToDouble(txtAllowedPAndT.Text)
                             +Convert.ToDouble(txtAllowedEmail.Text)+Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)
                             +Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text)+Convert.ToDouble(txtAllowedBusPass.Text)
                             + Convert.ToDouble(txtAllowedConveyance.Text)+Convert.ToDouble(txtAllowedJcMeetings.Text)
                             +Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            lblDisAllowed.Text = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAllowed.Text)).ToString();
        }

        protected void txtAllowedCourier_TextChanged(object sender, EventArgs e)
        {
            lblDisAllowedCourier.Text = (Convert.ToDouble(txtCourier.Text) - Convert.ToDouble(txtAllowedCourier.Text)).ToString();
            txtAllowed.Text = (Convert.ToDouble(txtAllowedStationaries.Text) + Convert.ToDouble(txtAllowedCourier.Text) + Convert.ToDouble(txtAllowedPAndT.Text)
                           + Convert.ToDouble(txtAllowedEmail.Text) + Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)
                           + Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(txtAllowedBusPass.Text)
                           + Convert.ToDouble(txtAllowedConveyance.Text) + Convert.ToDouble(txtAllowedJcMeetings.Text)
                           + Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            lblDisAllowed.Text = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAllowed.Text)).ToString();
        }

        protected void txtAllowedPAndT_TextChanged(object sender, EventArgs e)
        {
            lblDisAllowedPAndT.Text = (Convert.ToDouble(txtPAndT.Text) - Convert.ToDouble(txtAllowedPAndT.Text)).ToString();
            txtAllowed.Text = (Convert.ToDouble(txtAllowedStationaries.Text) + Convert.ToDouble(txtAllowedCourier.Text) + Convert.ToDouble(txtAllowedPAndT.Text)
                           + Convert.ToDouble(txtAllowedEmail.Text) + Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)
                           + Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(txtAllowedBusPass.Text)
                           + Convert.ToDouble(txtAllowedConveyance.Text) + Convert.ToDouble(txtAllowedJcMeetings.Text)
                           + Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            lblDisAllowed.Text = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAllowed.Text)).ToString();
        }

        protected void txtAllowedEmail_TextChanged(object sender, EventArgs e)
        {
            lblDisAllowedEmail.Text = (Convert.ToDouble(txtEmail.Text) - Convert.ToDouble(txtAllowedEmail.Text)).ToString();
            txtAllowed.Text = (Convert.ToDouble(txtAllowedStationaries.Text) + Convert.ToDouble(txtAllowedCourier.Text) + Convert.ToDouble(txtAllowedPAndT.Text)
                           + Convert.ToDouble(txtAllowedEmail.Text) + Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)
                           + Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(txtAllowedBusPass.Text)
                           + Convert.ToDouble(txtAllowedConveyance.Text) + Convert.ToDouble(txtAllowedJcMeetings.Text)
                           + Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            lblDisAllowed.Text = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAllowed.Text)).ToString();
        }

        protected void txtAllowedCompitatorProductPurchase_TextChanged(object sender, EventArgs e)
        {
            lblDisAllowedCompitatorProductPurchase.Text = (Convert.ToDouble(txtCompitatorProductPurchase.Text) - Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)).ToString();
            txtAllowed.Text = (Convert.ToDouble(txtAllowedStationaries.Text) + Convert.ToDouble(txtAllowedCourier.Text) + Convert.ToDouble(txtAllowedPAndT.Text)
                           + Convert.ToDouble(txtAllowedEmail.Text) + Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)
                           + Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(txtAllowedBusPass.Text)
                           + Convert.ToDouble(txtAllowedConveyance.Text) + Convert.ToDouble(txtAllowedJcMeetings.Text)
                           + Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            lblDisAllowed.Text = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAllowed.Text)).ToString();
        }

        protected void txtAllowedMarketDevelopmentExpences_TextChanged(object sender, EventArgs e)
        {
            lblDisAllowedMarketDevelopmentExpences.Text = (Convert.ToDouble(txtMarketDevelopmentExpences.Text) - Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text)).ToString();
            txtAllowed.Text = (Convert.ToDouble(txtAllowedStationaries.Text) + Convert.ToDouble(txtAllowedCourier.Text) + Convert.ToDouble(txtAllowedPAndT.Text)
                           + Convert.ToDouble(txtAllowedEmail.Text) + Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)
                           + Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(txtAllowedBusPass.Text)
                           + Convert.ToDouble(txtAllowedConveyance.Text) + Convert.ToDouble(txtAllowedJcMeetings.Text)
                           + Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            lblDisAllowed.Text = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAllowed.Text)).ToString();
        }

        protected void txtAllowedBusPass_TextChanged(object sender, EventArgs e)
        {
            lblDisAllowedBusPass.Text = (Convert.ToDouble(txtBusPass.Text) - Convert.ToDouble(txtAllowedBusPass.Text)).ToString();
            txtAllowed.Text = (Convert.ToDouble(txtAllowedStationaries.Text) + Convert.ToDouble(txtAllowedCourier.Text) + Convert.ToDouble(txtAllowedPAndT.Text)
                           + Convert.ToDouble(txtAllowedEmail.Text) + Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)
                           + Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(txtAllowedBusPass.Text)
                           + Convert.ToDouble(txtAllowedConveyance.Text) + Convert.ToDouble(txtAllowedJcMeetings.Text)
                           + Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            lblDisAllowed.Text = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAllowed.Text)).ToString();
        }

        protected void txtAllowedConveyance_TextChanged(object sender, EventArgs e)
        {
            lblDisAllowedConveyance.Text = (Convert.ToDouble(txtConveyance.Text) - Convert.ToDouble(txtAllowedConveyance.Text)).ToString();
            txtAllowed.Text = (Convert.ToDouble(txtAllowedStationaries.Text) + Convert.ToDouble(txtAllowedCourier.Text) + Convert.ToDouble(txtAllowedPAndT.Text)
                           + Convert.ToDouble(txtAllowedEmail.Text) + Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)
                           + Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(txtAllowedBusPass.Text)
                           + Convert.ToDouble(txtAllowedConveyance.Text) + Convert.ToDouble(txtAllowedJcMeetings.Text)
                           + Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            lblDisAllowed.Text = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAllowed.Text)).ToString();
        }

        protected void txtAllowedJcMeetings_TextChanged(object sender, EventArgs e)
        {
            lblDisAllowedJcMeetings.Text = (Convert.ToDouble(txtJcMeetings.Text) - Convert.ToDouble(txtAllowedJcMeetings.Text)).ToString();
            txtAllowed.Text = (Convert.ToDouble(txtAllowedStationaries.Text) + Convert.ToDouble(txtAllowedCourier.Text) + Convert.ToDouble(txtAllowedPAndT.Text)
                           + Convert.ToDouble(txtAllowedEmail.Text) + Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)
                           + Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(txtAllowedBusPass.Text)
                           + Convert.ToDouble(txtAllowedConveyance.Text) + Convert.ToDouble(txtAllowedJcMeetings.Text)
                           + Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            lblDisAllowed.Text = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAllowed.Text)).ToString();
        }

        protected void txtAllowedOthers_TextChanged(object sender, EventArgs e)
        {
            lblDisAllowedOthers.Text = (Convert.ToDouble(txtOthers.Text) - Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            txtAllowed.Text = (Convert.ToDouble(txtAllowedStationaries.Text) + Convert.ToDouble(txtAllowedCourier.Text) + Convert.ToDouble(txtAllowedPAndT.Text)
                           + Convert.ToDouble(txtAllowedEmail.Text) + Convert.ToDouble(txtAllowedCompitatorProductPurchase.Text)
                           + Convert.ToDouble(txtAllowedMarketDevelopmentExpences.Text) + Convert.ToDouble(txtAllowedBusPass.Text)
                           + Convert.ToDouble(txtAllowedConveyance.Text) + Convert.ToDouble(txtAllowedJcMeetings.Text)
                           + Convert.ToDouble(txtAllowedOthers.Text)).ToString();
            lblDisAllowed.Text = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAllowed.Text)).ToString();
        }

    }
}
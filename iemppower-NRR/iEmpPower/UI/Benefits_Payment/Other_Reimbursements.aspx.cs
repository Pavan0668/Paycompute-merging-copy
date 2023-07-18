using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class Other_Reimbursements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CV_txtStartDate.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
                    string createdby = User.Identity.Name;
                    LoadProject(createdby);
                    LoadReimCurrencyTypes();
                    LoadExpenditureCurrency();
                    //LoadExpenseType();

                    ViewState["IEXP_TYPESDT"] = null;//Newly added

                    if (Request.QueryString["NC"] != null)
                    {
                        if (Request.QueryString["NC"] == "C")
                        {
                            if (Session["IEXPID"] != null)
                            {
                                CopyIEXP(Session["IEXPID"].ToString());
                                goto displayInfo;
                            }
                        }
                        else if (Request.QueryString["NC"] == "N")
                        {
                            Session["IEXPID"] = null;
                            Session.Clear();
                        }
                    }
                }


                icollapse.Attributes.Add("class", cpe.ClientState == "true" ? "mdi mdi-plus font-20 text-white" : "mdi mdi-minus font-20 text-white");


                Loadfileupload();
            //HiddenTodayDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
            ////Page.DataBind();
            displayInfo:
                {
                    ////Console.WriteLine("");
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        void CopyIEXP(string IEXPID1)
        {
            //int rowIndex = Convert.ToInt32(e.CommandArgument);

            //foreach (GridViewRow row in grdIexpdetails.Rows)
            //{
            //    row.BackColor = row.RowIndex.Equals(rowIndex) ?
            //    System.Drawing.Color.LightGray :
            //    System.Drawing.Color.White;
            //}
            OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
            List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
            ////pnexpForm.Visible = true;
            int IEXP_ID = int.Parse(IEXPID1);//grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString().Trim());
            ViewState["IEXP_ID"] = IEXP_ID.ToString();

            Session["IEXPID"] = IEXP_ID;

            IexpboList = ExpenseblObj.Load_IEXPDetails(IEXP_ID);
            string purpose = IexpboList[0].PURPOSE == null ? "" : IexpboList[0].PURPOSE.ToString().Trim();//Newly added
            string Task = IexpboList[0].TASK == null ? "" : IexpboList[0].TASK.ToString().Trim();//Newly added
            string rcurr = IexpboList[0].RCURR == null ? "" : IexpboList[0].RCURR.ToString().Trim();//Newly added
            string prjid = IexpboList[0].PROJID == null ? "" : IexpboList[0].PROJID.ToString().Trim();//Newly added


            ddlProjectCode.SelectedValue = prjid;
            ddlTask.SelectedValue = Task;
            LoadExpenseType(ddlTask.SelectedValue.ToString().Trim());
            txtPurpose.Text = purpose;
            DDLCurrency.SelectedValue = rcurr;




            grd_IExpInfo.DataSource = IexpboList;
            grd_IExpInfo.DataBind();
            
            //DataTable dt = ConvertToDataTable(IexpboList);
            //decimal dout = 0;
            //decimal total = dt.AsEnumerable()
            //         .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out dout)).Sum(r => dout);
            //grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";


            //grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (rcurr) + ")";
            int totalRowsCount = grd_IExpInfo.Rows.Count;
            ViewState["totalRowsCount"] = totalRowsCount;


            ////ltiExpID.Text = IEXP_ID.ToString();
            ltTask.Text = ddlTask.SelectedItem.Text;
            ltProject.Text = ddlProjectCode.SelectedItem.Text;
            ltReimbAmt.Text = grd_IExpInfo.FooterRow.Cells[5].Text;



            ddlExpenseType.Focus();
            DDLCurrency.Enabled = false;
            ddlTask.Enabled = false;
            ddlProjectCode.Enabled = false;
            txtPurpose.Enabled = false;

            dvlineitem.Visible = true;



            //--------------------------------------------------------------
            string currency = DDLCurrency.SelectedValue.ToString();
            string date1;

            ViewState["IEXP_TYPESDT"] = null;
            int listid = 1;
            if (grd_IExpInfo.Rows.Count > 0)
            {
                BtnSave.Visible = true;
                btnSubmit.Visible = true;

                foreach (GridViewRow row in grd_IExpInfo.Rows)
                {

                    if (ViewState["IEXP_TYPESDT"] != null)
                    {
                        using (DataTable Dt = (DataTable)ViewState["IEXP_TYPESDT"])
                        {
                            try
                            {
                                // date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                // Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, Dt.Rows.Count + 1, ddlExpenseType.SelectedValue, ddlExpenseType.SelectedItem, txtStartDate.Text
                                //, txtNoofDays.Text.Trim(), txtExpenditureAmount.Text.Trim(), ddlExpenditureCurrency.SelectedValue, txtExchangeRate.Text.Trim()
                                //, lblReimbursableAmount.Text.Trim(), txtJustification.Text.Trim(), cb.Checked ? "YES" : "NO", fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "",
                                //fuAttachments.HasFile ? "~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "");
                                date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, Dt.Rows.Count + 1, IexpboList[listid].EXP_TYPE.ToString(), IexpboList[listid].EXP_TYPE_TEXT.ToString(), IexpboList[listid].S_DATE.ToString()
                              , IexpboList[listid].NO_DAYS.ToString(), IexpboList[listid].EXPT_AMT.ToString(), IexpboList[listid].EXPT_CURR.ToString(), IexpboList[listid].EXC_RATE.ToString()
                              , CalcReAmt(IexpboList[listid].EXPT_AMT.ToString(), IexpboList[listid].EXC_RATE.ToString()), IexpboList[listid].JUSTIFY.ToString(), IexpboList[listid].RECEIPT_FILE.ToString(),
                               IexpboList[listid].RECEIPT_FID.ToString(),
                               IexpboList[listid].RECEIPT_FPATH.ToString(), IexpboList[listid].RCURR.ToString());


                                // string path1 =Server.MapPath("~/IEXPENSEDoc/"+ User.Identity.Name + "-" + date1);
                                // string ext = Path.GetExtension(fuAttachments.FileName);
                                fuAttachments.SaveAs(Server.MapPath("~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName));
                                //decimal total = decimal.Parse(Dt.AsEnumerable().Sum(row => row.Field<double>("RE_AMT")).ToString()); 
                                //grd_IExpInfo.FooterRow.Cells[1].Text = "Total";
                                //grd_IExpInfo.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                                //grd_IExpInfo.FooterRow.Cells[2].Text = total.ToString("N2");
                                grd_IExpInfo.DataSource = Dt;
                                grd_IExpInfo.DataBind();

                                listid = listid + 1;


                                decimal d = 0;
                                ////decimal total = Dt.AsEnumerable()
                                //// .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d))
                                //// .Sum(r => d);
                                ////string createdby = User.Identity.Name;
                                ////decimal SettlementAmt = 0;
                                ////string SettlementCurr = string.Empty;
                                ////OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                                ////OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, currency);
                                ////foreach (OtherReimbursementsbo objBo1 in objLst1)
                                ////{
                                ////    SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                                ////    SettlementCurr = objBo1.SETTLECURR.ToString();
                                ////}
                                ////grd_IExpInfo.FooterRow.Cells[4].Text = "Total";

                                ////grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                                ////grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("N2") + "(" + (currency) + ")";

                                ddlExpenseType.Focus();

                            }
                            catch (Exception Ex)
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                                return;
                            }
                        }
                    }

                    else
                    {

                        using (DataTable Dt = GetIEXp_TypesDt())
                        {
                            date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                            Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, Dt.Rows.Count + 1, IexpboList[0].EXP_TYPE.ToString(), IexpboList[0].EXP_TYPE_TEXT.ToString(), IexpboList[0].S_DATE.ToString()
                              , IexpboList[0].NO_DAYS.ToString(), IexpboList[0].EXPT_AMT.ToString(), IexpboList[0].EXPT_CURR.ToString(), IexpboList[0].EXC_RATE.ToString()
                              , CalcReAmt(IexpboList[0].EXPT_AMT.ToString(), IexpboList[0].EXC_RATE.ToString()), IexpboList[0].JUSTIFY.ToString(), IexpboList[0].RECEIPT_FILE.ToString(),
                               IexpboList[0].RECEIPT_FID.ToString(),
                               IexpboList[0].RECEIPT_FPATH.ToString(), IexpboList[0].RCURR.ToString());

                            //decimal total = decimal.Parse(Dt.AsEnumerable().Sum(row => row.Field<double>("RE_AMT")).ToString());
                            // double Retotal = 0.0;
                            //  string total = Dt.AsEnumerable().Sum(row => double.TryParse(row.Field<double>("RE_AMT"),out Retotal).ToString());

                            //grd_IExpInfo.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                            //grd_IExpInfo.FooterRow.Cells[2].Text = total.ToString("N2");
                            grd_IExpInfo.DataSource = Dt;
                            grd_IExpInfo.DataBind();
                            ////decimal d = 0;
                            ////decimal total = Dt.AsEnumerable()
                            //// .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d))
                            //// .Sum(r => d);
                            ////string createdby = User.Identity.Name;
                            ////decimal SettlementAmt = 0;
                            ////string SettlementCurr = string.Empty;
                            ////OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                            ////OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, currency);
                            ////foreach (OtherReimbursementsbo objBo1 in objLst1)
                            ////{
                            ////    SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                            ////    SettlementCurr = objBo1.SETTLECURR.ToString();
                            ////}
                            ////grd_IExpInfo.FooterRow.Cells[4].Text = "Total";

                            ////grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            ////grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("N2") + "(" + (currency) + ")";



                            ViewState["IEXP_TYPESDT"] = Dt;

                            DDLCurrency.Enabled = false;
                            ddlTask.Enabled = false;
                            ddlProjectCode.Enabled = false;
                            txtPurpose.Enabled = false;
                            fuAttachments.SaveAs(Server.MapPath("~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName));
                            ddlExpenseType.Focus();

                        }
                    }
                }
            }

            //--------------------------------------------------------------

            DataTable dt = ConvertToDataTable(IexpboList);
            decimal dout = 0;
            decimal total = dt.AsEnumerable()
                     .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out dout)).Sum(r => dout);
            grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";
            grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            grd_IExpInfo.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (rcurr) + ")";
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

        public void Loadfileupload()
        {
            if (Session["fuAttachments"] == null && fuAttachments.HasFile)
            {
                Session["fuAttachments"] = fuAttachments;
                fuAttachmentsfname.Text = fuAttachments.FileName;
            }
            // Next time submit and Session has values but FileUpload is Blank
            // Return the values from session to FileUpload
            else if (Session["fuAttachments"] != null && (!fuAttachments.HasFile))
            {
                fuAttachments = (FileUpload)Session["fuAttachments"];
                fuAttachmentsfname.Text = fuAttachments.FileName;
            }
            // Now there could be another sictution when Session has File but user want to change the file
            // In this case we have to change the file in session object
            else if (fuAttachments.HasFile)
            {
                Session["fuAttachments"] = fuAttachments;
                fuAttachmentsfname.Text = fuAttachments.FileName;
            }
        }

        private void LoadExpenseType(string task)
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_ExpenseType(task);
                ddlExpenseType.DataSource = objLst;
                ddlExpenseType.DataTextField = "LGTXT";
                ddlExpenseType.DataValueField = "LGART";
                ddlExpenseType.DataBind();
                ddlExpenseType.Items.Insert(0, new ListItem("-SELECT-", "0"));
                ddlExpenseType.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        private void LoadProject(string createdby)
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Project(createdby);
                ddlProjectCode.DataSource = objLst;
                ddlProjectCode.DataTextField = "WBS";
                ddlProjectCode.DataValueField = "WBSID";
                ddlProjectCode.DataBind();
                ddlProjectCode.Items.Insert(0, new ListItem("-SEARCH-", "0"));
                ddlProjectCode.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        private void LoadReimCurrencyTypes()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Payment_Currency();
                DDLCurrency.DataSource = objLst;
                DDLCurrency.DataTextField = "WARESTXT";
                DDLCurrency.DataValueField = "WAERS";
                DDLCurrency.DataBind();
                DDLCurrency.Items.Insert(0, new ListItem("-SEARCH-", "0"));
                DDLCurrency.SelectedValue = "0";
               
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }


        }

        private void LoadExpenditureCurrency()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Payment_Currency();
                ddlExpenditureCurrency.DataSource = objLst;
                ddlExpenditureCurrency.DataTextField = "WARESTXT";
                ddlExpenditureCurrency.DataValueField = "WAERS";
                ddlExpenditureCurrency.DataBind();
                ddlExpenditureCurrency.Items.Insert(0, new ListItem("-SEARCH-", "0"));
                ddlExpenditureCurrency.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        //public void GetExchangeRate()
        //{
        //    try
        //    {

        //        if (ddlExpenditureCurrency.SelectedValue != "0" && DDLCurrency.SelectedValue != "0")
        //        {
        //            OtherReimbursementsbl objbl = new OtherReimbursementsbl();
        //            OtherReimbursementCollectionbo objLst = objbl.Load_ExchangeRate(DDLCurrency.SelectedValue.ToString(), ddlExpenditureCurrency.SelectedValue.ToString());
        //            foreach (OtherReimbursementsbo objBo in objLst)
        //            {
        //                txtExchangeRate.Text = objBo.UKURS.ToString();

        //            }
        //            LoadExchangeRate();
        //        }
        //        else
        //        {
        //            if(ddlExpenditureCurrency.SelectedValue == "0" )
        //            {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Expenditure Currency !')", true);
        //            }
        //            if (DDLCurrency.SelectedValue == "0")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Reimbursement Currency !')", true);
        //            }

        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}

        public void GetExchangeRate()
        {
            try
            {

                if (ddlExpenditureCurrency.SelectedValue != "0" && DDLCurrency.SelectedValue != "0")
                {
                    if (ddlExpenditureCurrency.SelectedValue != DDLCurrency.SelectedValue)
                    {
                        OtherReimbursementsbl objbl = new OtherReimbursementsbl();
                        OtherReimbursementCollectionbo objLst = objbl.Load_ExchangeRate(ddlExpenditureCurrency.SelectedValue.ToString(), DDLCurrency.SelectedValue.ToString());
                        foreach (OtherReimbursementsbo objBo in objLst)
                        {
                            MsgCls("", lblIndent, Color.White);
                            txtExchangeRate.Text = objBo.UKURS.ToString();

                        }
                        //LoadExchangeRate();
                        if (txtExchangeRate.Text.Trim() == "")
                        {
                            MsgCls("Exchange Rate Cannot be empty!", lblIndent, Color.Red);
                        }
                        else
                        {

                            LoadExchangeRate();
                        }
                    }
                    else
                    {
                        MsgCls("", lblIndent, Color.White);
                        txtExchangeRate.Text = "1";
                        LoadExchangeRate();
                    }
                }
                else
                {

                    if (ddlExpenditureCurrency.SelectedValue == "0")
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Expenditure Currency !')", true);
                        ///// MsgCls("Please select Expenditure Currency !", lblIndent, Color.Red);

                    }
                    if (DDLCurrency.SelectedValue == "0")
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Reimbursement Currency !')", true);
                        /////  MsgCls("Please select Reimbursement Currency !", lblIndent, Color.Red);

                    }

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadExchangeRate()
        {
            double ExchangeRate = 0.0;
            double ExpenditureAmount = 0.0;
            if (txtExchangeRate1.Text.Trim() == "")
            {
                if (double.TryParse(txtExchangeRate.Text, out ExchangeRate))
                {
                    if (double.TryParse(txtExpenditureAmount.Text, out ExpenditureAmount))
                    {
                        double ramt = 0.0;
                        if (ExchangeRate < 0)
                        {
                            ramt = ExpenditureAmount / Math.Abs(ExchangeRate);

                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00");//// (Math.Round(ramt, 2)).ToString();
                            //lblReimbursableAmount.Text = ramt.ToString();
                            LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); ////(Math.Round(ramt, 2)).ToString();
                            // lblReimbursableAmount.Text = ramt.ToString();
                            LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;
                            MsgCls("", lblIndent, Color.White);
                        }
                    }
                    else
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Expenditure Amount!')", true);
                        /////     MsgCls("Please enter correct Expenditure Amount!", lblIndent, Color.Red);


                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Exchange Rate!')", true);
                    /////    MsgCls("Please enter correct Exchange Rate!", lblIndent, Color.Red);

                }
            }

            else
            {
                txtExchangeRate.Text = txtExchangeRate1.Text.Trim();
                if (double.TryParse(txtExchangeRate.Text, out ExchangeRate))
                {
                    if (double.TryParse(txtExpenditureAmount.Text, out ExpenditureAmount))
                    {
                        double ramt = 0.0;
                        if (ExchangeRate < 0)
                        {
                            ramt = ExpenditureAmount / Math.Abs(ExchangeRate);

                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); ////(Math.Round(ramt, 2)).ToString();
                            //lblReimbursableAmount.Text = ramt.ToString();
                            LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); ////(Math.Round(ramt, 2)).ToString();
                            // lblReimbursableAmount.Text = ramt.ToString();
                            LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;
                            MsgCls("", lblIndent, Color.White);
                        }
                    }
                    else
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Expenditure Amount!')", true);
                        /////     MsgCls("Please enter correct Expenditure Amount!", lblIndent, Color.Red);


                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Exchange Rate!')", true);
                    /////    MsgCls("Please enter correct Exchange Rate!", lblIndent, Color.Red);

                }
            }
        }

        public string CalcReAmt(string ExpenditureAmount, string ExchangeRate)
        {
            double ExchangeRate1 = double.Parse(ExchangeRate);
            double ExpenditureAmount1 = double.Parse(ExpenditureAmount);
            double ramt = 0.0;
            if (ExchangeRate1 < 0)
            {
                ramt = ExpenditureAmount1 / Math.Abs(ExchangeRate1);

                //lblReimbursableAmount.Text = (Math.Round(ramt, 2)).ToString();
                lblReimbursableAmount.Text = Convert.ToDecimal(ramt).ToString("#,##0.00");////ramt.ToString();
                LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;

            }
            else
            {
                ramt = ExpenditureAmount1 * Math.Abs(ExchangeRate1);
                //lblReimbursableAmount.Text = (Math.Round(ramt, 2)).ToString();
                lblReimbursableAmount.Text = Convert.ToDecimal(ramt).ToString("#,##0.00");////ramt.ToString();
                LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;

            }
            return lblReimbursableAmount.Text;
        }

        protected void DDLCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            txtExchangeRate.Text = string.Empty;
            lblReimbursableAmount.Text = string.Empty;
            txtExchangeRate.Text = string.Empty;

            LblReimbursableCurrency.Text = string.Empty;
            GetExchangeRate();
            //// txtStartDate.Focus();
            DDLCurrency.Focus();
            ////Expand();
        }

        protected void ddlExpenditureCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            txtExchangeRate.Text = string.Empty;
            lblReimbursableAmount.Text = string.Empty;
            LblReimbursableCurrency.Text = string.Empty;
            GetExchangeRate();
            //// txtExpenditureAmount.Focus();
            ddlExpenditureCurrency.Focus();
            ////Expand();
        }

        protected void txtExchangeRate_TextChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            double ExchangeRate = 0.0;
            double ExpenditureAmount = 0.0;
            if (txtExchangeRate1.Text.Trim() == "")
            {
                MsgCls("Exchange Rate Cannot be empty!", lblIndent, Color.Red);
                GetExchangeRate();//Newly added
            }
            else
            {
                txtExchangeRate.Text = txtExchangeRate1.Text.Trim();
                if (double.TryParse(txtExchangeRate.Text, out ExchangeRate))
                {
                    if (double.TryParse(txtExpenditureAmount.Text, out ExpenditureAmount))
                    {
                        double ramt = 0.0;
                        if (ExchangeRate < 0)
                        {
                            ramt = ExpenditureAmount / Math.Abs(ExchangeRate);

                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); ////(Math.Round(ramt, 2)).ToString();
                            //lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); ////(Math.Round(ramt, 2)).ToString();
                            // lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                    }
                    else
                    {
                        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Expenditure Amount!')", true);
                        /////    MsgCls("Please enter correct Expenditure Rate!", lblIndent, Color.Red);
                    }
                }

                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Exchange Rate!')", true);
                    /////     MsgCls("Please enter correct Exchange Rate!", lblIndent, Color.Red);
                }
            }
            txtExchangeRate1.Focus();
            //Expand();

        }

        #region  IExpense Types Empty DataTable

        private DataTable GetIEXp_TypesDt()
        {
            try
            {

                DataTable Dt = new DataTable("IEXP_TYPES");
                Dt.Columns.Add("IEXP_TYPID", typeof(int));
                Dt.Columns.Add("IEXP_ID", typeof(int));
                Dt.Columns.Add("ID", typeof(int));
                Dt.Columns.Add("EXP_TYPE", typeof(string));
                Dt.Columns.Add("EXP_TYPE_TEXT", typeof(string));
                Dt.Columns.Add("S_DATE", typeof(DateTime));
                Dt.Columns.Add("NO_DAYS", typeof(string));
                Dt.Columns.Add("EXPT_AMT", typeof(string));
                Dt.Columns.Add("EXPT_CURR", typeof(string));
                Dt.Columns.Add("EXC_RATE", typeof(string));
                Dt.Columns.Add("RE_AMT", typeof(string));
                Dt.Columns.Add("JUSTIFY", typeof(string));
                Dt.Columns.Add("RECEIPT_FILE", typeof(string));
                Dt.Columns.Add("RECEIPT_FID", typeof(string));
                Dt.Columns.Add("RECEIPT_FPATH", typeof(string));
                Dt.Columns.Add("RCURR", typeof(string));

                return Dt;
            }
            catch (Exception Ex)
            {
                throw Ex;
                return null;
            }
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                //if ((cb.Checked == true) || ((cb.Checked == false) && (fuAttachments.HasFile == true)))
                //{

                //if (grd_IExpInfo.Rows.Count > 0)
                //{

                if (DDLCurrency.SelectedValue != "0")
                {
                    //if ((cb.Checked == false && fuAttachments.HasFile == true) || (cb.Checked == true && fuAttachments.HasFile == false))
                    //{
                    string currency = DDLCurrency.SelectedValue.ToString();
                    string date1;
                    if (ViewState["IEXP_TYPESDT"] != null)
                    {
                        using (DataTable Dt = (DataTable)ViewState["IEXP_TYPESDT"])
                        {
                            try
                            {
                                // date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                // Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, Dt.Rows.Count + 1, ddlExpenseType.SelectedValue, ddlExpenseType.SelectedItem, txtStartDate.Text
                                //, txtNoofDays.Text.Trim(), txtExpenditureAmount.Text.Trim(), ddlExpenditureCurrency.SelectedValue, txtExchangeRate.Text.Trim()
                                //, lblReimbursableAmount.Text.Trim(), txtJustification.Text.Trim(), cb.Checked ? "YES" : "NO", fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "",
                                //fuAttachments.HasFile ? "~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "");
                                date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                                Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, Dt.Rows.Count + 1, ddlExpenseType.SelectedValue, ddlExpenseType.SelectedItem, txtStartDate.Text
                               , txtNoofDays.Text.Trim(), txtExpenditureAmount.Text.Trim(), ddlExpenditureCurrency.SelectedValue, txtExchangeRate.Text.Trim()
                               , CalcReAmt(txtExpenditureAmount.Text.Trim(), txtExchangeRate.Text.Trim()), txtJustification.Text.Length >= 250 ? txtJustification.Text.Trim().Substring(0, 250) : txtJustification.Text.Trim(),
                               cb.Checked ? "YES" : "NO", fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "",
                               fuAttachments.HasFile ? "~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "", DDLCurrency.SelectedValue.ToString().Trim());


                                // string path1 =Server.MapPath("~/IEXPENSEDoc/"+ User.Identity.Name + "-" + date1);
                                // string ext = Path.GetExtension(fuAttachments.FileName);
                                fuAttachments.SaveAs(Server.MapPath("~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName));
                                //decimal total = decimal.Parse(Dt.AsEnumerable().Sum(row => row.Field<double>("RE_AMT")).ToString()); 
                                //grd_IExpInfo.FooterRow.Cells[1].Text = "Total";
                                //grd_IExpInfo.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                                //grd_IExpInfo.FooterRow.Cells[2].Text = total.ToString("N2");
                                grd_IExpInfo.DataSource = Dt;
                                grd_IExpInfo.DataBind();
                                decimal d = 0;
                                decimal total = Dt.AsEnumerable()
                                 .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d))
                                 .Sum(r => d);
                                string createdby = User.Identity.Name;
                                decimal SettlementAmt = 0;
                                string SettlementCurr = string.Empty;
                                OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                                OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, currency);
                                foreach (OtherReimbursementsbo objBo1 in objLst1)
                                {
                                    SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                                    SettlementCurr = objBo1.SETTLECURR.ToString();
                                }
                                grd_IExpInfo.FooterRow.Cells[4].Text = "Total";

                                grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                                grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("N2") + "(" + (currency) + ")";

                                ddlExpenseType.Focus();

                            }
                            catch (Exception Ex)
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                                return;
                            }
                        }
                    }

                    else
                    {

                        using (DataTable Dt = GetIEXp_TypesDt())
                        {
                            date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                            Dt.Rows.Add(Dt.Rows.Count + 1, Dt.Rows.Count + 1, Dt.Rows.Count + 1, ddlExpenseType.SelectedValue, ddlExpenseType.SelectedItem, txtStartDate.Text
                              , txtNoofDays.Text.Trim(), txtExpenditureAmount.Text.Trim(), ddlExpenditureCurrency.SelectedValue, txtExchangeRate.Text.Trim()
                              , CalcReAmt(txtExpenditureAmount.Text.Trim(), txtExchangeRate.Text.Trim()), txtJustification.Text.Length >= 250 ? txtJustification.Text.Trim().Substring(0, 250) : txtJustification.Text.Trim(), cb.Checked ? "YES" : "NO",
                              fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "",
                              fuAttachments.HasFile ? "~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "", DDLCurrency.SelectedValue.ToString().Trim());

                            //decimal total = decimal.Parse(Dt.AsEnumerable().Sum(row => row.Field<double>("RE_AMT")).ToString());
                            // double Retotal = 0.0;
                            //  string total = Dt.AsEnumerable().Sum(row => double.TryParse(row.Field<double>("RE_AMT"),out Retotal).ToString());

                            //grd_IExpInfo.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                            //grd_IExpInfo.FooterRow.Cells[2].Text = total.ToString("N2");
                            grd_IExpInfo.DataSource = Dt;
                            grd_IExpInfo.DataBind();
                            decimal d = 0;
                            decimal total = Dt.AsEnumerable()
                             .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d))
                             .Sum(r => d);
                            string createdby = User.Identity.Name;
                            decimal SettlementAmt = 0;
                            string SettlementCurr = string.Empty;
                            OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                            OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, currency);
                            foreach (OtherReimbursementsbo objBo1 in objLst1)
                            {
                                SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                                SettlementCurr = objBo1.SETTLECURR.ToString();
                            }
                            grd_IExpInfo.FooterRow.Cells[4].Text = "Total";

                            grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("N2") + "(" + (currency) + ")";



                            ViewState["IEXP_TYPESDT"] = Dt;

                            DDLCurrency.Enabled = false;
                            ddlTask.Enabled = false;
                            ddlProjectCode.Enabled = false;
                            txtPurpose.Enabled = false;
                            fuAttachments.SaveAs(Server.MapPath("~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName));
                            ddlExpenseType.Focus();

                        }
                    }
                    ClearAfterAdd();
                    MsgCls("iExpense Types added successfully !", lblIndent, Color.Green);
                    //---------------------------------
                    ltTask.Text = ddlTask.SelectedItem.Text;
                    ltProject.Text = ddlProjectCode.SelectedItem.Text;
                    ltReimbAmt.Text = grd_IExpInfo.FooterRow.Cells[5].Text;
                    dvlineitem.Visible = true;
                    ////collapse();

                    //---------------------------------
                    BtnSave.Visible = true;
                    btnSubmit.Visible = true;
                    grd_IExpInfo.Columns[10].Visible = true;
                    //}
                    //else
                    //{
                    //    MsgCls("Please attach the file!", lblIndent, Color.Red);
                    //    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please attach the file!')", true);

                    //}
                    //}
                    //else
                    //{
                    //    MsgCls("Please select a file !", lblIndent, Color.Red);
                    //}
                }
                else
                {
                    ///// MsgCls("Please select Reimbursement Currency !", lblIndent, Color.Red);
                    //dvlialert.Visible = true;
                }
                //}
                //else
                //{
                //    dvlialert.Visible = true;
                //}
            }

            catch (Exception Ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
            }

        }

        private void Clear()
        {
            ddlExpenseType.ClearSelection();
            txtStartDate.Text = string.Empty;
            txtNoofDays.Text = string.Empty;
            txtExpenditureAmount.Text = string.Empty;
            ddlExpenditureCurrency.ClearSelection();
            txtExchangeRate.Text = string.Empty;
            txtExchangeRate1.Text = string.Empty;
            lblReimbursableAmount.Text = string.Empty;
            LblReimbursableCurrency.Text = string.Empty;
            txtJustification.Text = string.Empty;
            fuAttachments.Attributes.Clear();
            Session["fuAttachments"] = null;
            fuAttachmentsfname.Text = string.Empty;
            cb.Checked = false;
        }

        private void ClearAfterAdd()
        {

            txtStartDate.Text = string.Empty;
            txtNoofDays.Text = string.Empty;
            txtExpenditureAmount.Text = string.Empty;

            txtExchangeRate.Text = string.Empty;
            txtExchangeRate1.Text = string.Empty;
            lblReimbursableAmount.Text = string.Empty;
            LblReimbursableCurrency.Text = string.Empty;
            txtJustification.Text = string.Empty;
            fuAttachments.Attributes.Clear();
            Session["fuAttachments"] = null;
            fuAttachmentsfname.Text = string.Empty;
            cb.Checked = false;
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
                bool? Status = false;
                int? IEXp_ID = 0;

                decimal Samnt = 0;

                OtherReimbursementsbo OtherReBO = new OtherReimbursementsbo();
                OtherReBO.PROJID = ddlProjectCode.SelectedValue;
                OtherReBO.TASK = ddlTask.SelectedValue;
                OtherReBO.PURPOSE = txtPurpose.Text.Trim();
                OtherReBO.RCURR = DDLCurrency.SelectedValue;
                OtherReBO.CREATED_ON = DateTime.Now;
                OtherReBO.CREATED_BY = User.Identity.Name;
                OtherReBO.APPROVED_ON1 = DateTime.MinValue;
                OtherReBO.APPROVED_BY1 = "";
                OtherReBO.REMARKS1 = string.Empty;
                OtherReBO.APPROVED_ON2 = DateTime.MinValue;
                OtherReBO.APPROVED_BY2 = "";
                OtherReBO.REMARKS2 = string.Empty;
                OtherReBO.APPROVED_ON3 = DateTime.MinValue;
                OtherReBO.APPROVED_BY3 = "";
                OtherReBO.REMARKS3 = string.Empty;
                OtherReBO.APPROVED_ON4 = DateTime.MinValue;
                OtherReBO.APPROVED_BY4 = "";
                OtherReBO.REMARKS4 = string.Empty;
                OtherReBO.APPROVED_ON5 = DateTime.MinValue;
                OtherReBO.APPROVED_BY5 = "";
                OtherReBO.REMARKS5 = string.Empty;
                OtherReBO.APPROVED_ON6 = DateTime.MinValue;
                OtherReBO.APPROVED_BY6 = "";
                OtherReBO.REMARKS6 = string.Empty;
                OtherReBO.APPROVED_ON7 = DateTime.MinValue;
                OtherReBO.APPROVED_BY7 = "";
                OtherReBO.REMARKS7 = string.Empty;
                OtherReBO.APPROVED_ON8 = DateTime.MinValue;
                OtherReBO.APPROVED_BY8 = "";
                OtherReBO.REMARKS8 = string.Empty;
                OtherReBO.APPROVED_ON9 = DateTime.MinValue;
                OtherReBO.APPROVED_BY9 = "";
                OtherReBO.REMARKS9 = string.Empty;
                OtherReBO.STATUS = "";



                OtherReimbursementsbl OtherRebl = new OtherReimbursementsbl();
                if (grd_IExpInfo.Rows.Count > 0)
                {



                    if (ViewState["IEXP_TYPESDT"] != null)
                    {
                        using (DataTable Dt = (DataTable)ViewState["IEXP_TYPESDT"])
                        {
                            //for (int i = 0; i < Dt.Rows.Count; i++)
                            //{
                            //    Samnt =Samnt+ decimal.Parse(Dt.Rows[i]["RE_AMT"].ToString());

                            //}
                            // OtherReBO.TOTAL_AMOUNT = decimal.Parse(Dt.AsEnumerable().Sum(x => double.Parse(x.Field<string>("RE_AMT"))).ToString("0.00"));
                            decimal d = 0;
                            decimal total = Dt.AsEnumerable()
                             .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d))
                             .Sum(r => d);
                            OtherReBO.TOTAL_AMOUNT = total;
                            // OtherReBO.TOTAL_AMOUNT = decimal.Parse(Dt.AsEnumerable().Sum(x => double.Parse(x.Field<string>("RE_AMT"))).ToString());
                            Samnt = decimal.Parse(Dt.AsEnumerable().Sum(x => double.Parse(x.Field<string>("RE_AMT"))).ToString("0.00"));
                        }
                    }
                    OtherRebl.Create_IExpense(OtherReBO, ref IEXp_ID, ref Status);
                    ltiExpID.Text = IEXp_ID.ToString();
                    if (Status.Equals(false))
                    {

                        //fuProposal.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Proposal") + Path.GetExtension(fuProposal.FileName));
                        //fuAgreement.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Agreement") + Path.GetExtension(fuAgreement.FileName));
                        //fuEmailCommunication.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Email") + Path.GetExtension(fuEmailCommunication.FileName));
                        //fuInvoice.SaveAs(Server.MapPath("~/PRDoc/" + PR_ID + "-" + ViewState["RequesterID"].ToString() + "-Invoice") + Path.GetExtension(fuInvoice.FileName));


                        if (ViewState["IEXP_TYPESDT"] != null)
                        {
                            using (DataTable Dt = (DataTable)ViewState["IEXP_TYPESDT"])
                            {
                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    OtherReBO.IEXP_TYPID = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                    OtherReBO.IEXP_ID = IEXp_ID;
                                    OtherReBO.ID = i + 1;
                                    OtherReBO.EXP_TYPE = Dt.Rows[i]["EXP_TYPE"].ToString();
                                    OtherReBO.S_DATE = DateTime.Parse(Dt.Rows[i]["S_DATE"].ToString());
                                    OtherReBO.NO_DAYS = Dt.Rows[i]["NO_DAYS"].ToString();
                                    OtherReBO.EXPT_AMT = Dt.Rows[i]["EXPT_AMT"].ToString();
                                    OtherReBO.EXPT_CURR = Dt.Rows[i]["EXPT_CURR"].ToString();
                                    OtherReBO.EXC_RATE = Dt.Rows[i]["EXC_RATE"].ToString();
                                    string re_amnt = Dt.Rows[i]["RE_AMT"].ToString();
                                    double ramt = double.Parse(re_amnt);
                                    OtherReBO.RE_AMT = (Math.Round(ramt, 3)).ToString();
                                    OtherReBO.JUSTIFY = Dt.Rows[i]["JUSTIFY"].ToString();
                                    OtherReBO.RECEIPT_FILE = Dt.Rows[i]["RECEIPT_FILE"].ToString();
                                    OtherReBO.RECEIPT_FID = Dt.Rows[i]["RECEIPT_FID"].ToString();
                                    OtherReBO.RECEIPT_FPATH = Dt.Rows[i]["RECEIPT_FPATH"].ToString();

                                    OtherRebl.Create_IExpTypes_Add(OtherReBO, ref Status);
                                    //if (Status.Equals(false))
                                    //{
                                    //    fuAttachments.SaveAs(Server.MapPath("~/IEXPENSEDoc/" + IEXp_ID + "-" + User.Identity.Name.ToString() + "-" + OtherReBO.IEXP_TYPID) + Path.GetExtension(fuAttachments.FileName));

                                    //}

                                    //ClearIExpense();
                                    //Clear();
                                    DDLCurrency.Enabled = true;
                                    ddlTask.Enabled = true;
                                    ddlProjectCode.Enabled = true;
                                    txtPurpose.Enabled = true;
                                    //ViewState["IEXP_TYPESDT"] = null;
                                    //grd_IExpInfo.DataSource = null;
                                    //grd_IExpInfo.DataBind();
                                }
                            }

                        }
                        ////MsgCls("iExpense created successfully !", lblMessageBoard, Color.Green);
                        MsgCls("iExpense " + IEXp_ID + "  created successfully !", lblMessageBoard, Color.Green);
                        string alert = "IExpense " + IEXp_ID + " created successfully !";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='iExpense_Request.aspx';", true);
                        lblIndent.Text = "";
                        dvlineitem.Visible = false;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IExpense created successfully !')", true);
                        //  OtherRebl.Update_IExpense_Approver(IEXp_ID);

                        SendMailMethod(IEXp_ID, ddlProjectCode.SelectedItem.Text, ddlTask.SelectedItem.Text, txtPurpose.Text.Trim(), DDLCurrency.SelectedValue, Samnt);
                        ClearIExpense();
                        Clear();
                        ViewState["IEXP_TYPESDT"] = null;
                        grd_IExpInfo.DataSource = null;
                        grd_IExpInfo.DataBind();
                        BtnSave.Visible = false;
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        MsgCls("Unable to create iExpense !", lblMessageBoard, Color.Red);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to create IExpense !')", true);
                    }
                }
                else
                {
                    MsgCls("Please add atleast 1 Expense type before submitting IExpense !", lblMessageBoard, Color.Red);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please add atleast 1 Expense type before submitting IExpense !')", true);
                }

            }


            //catch (Exception Ex)
            //{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

            catch (Exception Ex)
            {

                switch (Ex.Message)
                {


                    case "-05":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Approvals are missing!')", true);
                        MsgCls("Approvals are missing!", lblMessageBoard, Color.Red);
                        //PageLoadEvents();
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        break;
                }
                //MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int? IEXp_ID = 0;

                OtherReimbursementsbl OtherRebl = new OtherReimbursementsbl();

                decimal TRamnt = 0;

                if (ViewState["IEXP_TYPESDT"] != null)
                {
                    if (ViewState["IEXP_TYPESDT"] != null)
                    {
                        using (DataTable Dt = (DataTable)ViewState["IEXP_TYPESDT"])
                        {
                            TRamnt = decimal.Parse(Dt.AsEnumerable().Sum(x => double.Parse(x.Field<string>("RE_AMT"))).ToString("0.000"));

                        }
                    }
                    bool? status = false;

                    OtherReimbursementsbo OtherReBO = new OtherReimbursementsbo();
                    OtherReBO.PROJID = ddlProjectCode.SelectedValue;
                    OtherReBO.TASK = ddlTask.SelectedValue;
                    OtherReBO.PURPOSE = txtPurpose.Text.Trim();
                    OtherReBO.RCURR = DDLCurrency.SelectedValue;
                    OtherReBO.CREATED_ON = DateTime.Now;
                    OtherReBO.CREATED_BY = User.Identity.Name;
                    OtherReBO.APPROVED_ON1 = DateTime.MinValue;
                    OtherReBO.APPROVED_BY1 = "";
                    OtherReBO.REMARKS1 = string.Empty;
                    OtherReBO.APPROVED_ON2 = DateTime.MinValue;
                    OtherReBO.APPROVED_BY2 = "";
                    OtherReBO.REMARKS2 = string.Empty;
                    OtherReBO.APPROVED_ON3 = DateTime.MinValue;
                    OtherReBO.APPROVED_BY3 = "";
                    OtherReBO.REMARKS3 = string.Empty;
                    OtherReBO.APPROVED_ON4 = DateTime.MinValue;
                    OtherReBO.APPROVED_BY4 = "";
                    OtherReBO.REMARKS4 = string.Empty;
                    OtherReBO.APPROVED_ON5 = DateTime.MinValue;
                    OtherReBO.APPROVED_BY5 = "";
                    OtherReBO.REMARKS5 = string.Empty;
                    OtherReBO.APPROVED_ON6 = DateTime.MinValue;
                    OtherReBO.APPROVED_BY6 = "";
                    OtherReBO.REMARKS6 = string.Empty;
                    OtherReBO.APPROVED_ON7 = DateTime.MinValue;
                    OtherReBO.APPROVED_BY7 = "";
                    OtherReBO.REMARKS7 = string.Empty;
                    OtherReBO.APPROVED_ON8 = DateTime.MinValue;
                    OtherReBO.APPROVED_BY8 = "";
                    OtherReBO.REMARKS8 = string.Empty;
                    OtherReBO.APPROVED_ON9 = DateTime.MinValue;
                    OtherReBO.APPROVED_BY9 = "";
                    OtherReBO.REMARKS9 = string.Empty;
                    OtherReBO.STATUS = "Saved";


                    OtherRebl.Save_IExpense(OtherReBO, ref IEXp_ID, ref status);
                    ltiExpID.Text = IEXp_ID.ToString();
                    if (status == false)
                    {
                        if (ViewState["IEXP_TYPESDT"] != null)
                        {
                            using (DataTable Dt = (DataTable)ViewState["IEXP_TYPESDT"])
                            {

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {

                                    OtherReBO.IEXP_TYPID = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                    OtherReBO.IEXP_ID = IEXp_ID;
                                    OtherReBO.ID = i + 1;
                                    OtherReBO.EXP_TYPE = Dt.Rows[i]["EXP_TYPE"].ToString();
                                    OtherReBO.S_DATE = DateTime.Parse(Dt.Rows[i]["S_DATE"].ToString());
                                    OtherReBO.NO_DAYS = Dt.Rows[i]["NO_DAYS"].ToString();
                                    OtherReBO.EXPT_AMT = Dt.Rows[i]["EXPT_AMT"].ToString();
                                    OtherReBO.EXPT_CURR = Dt.Rows[i]["EXPT_CURR"].ToString();
                                    OtherReBO.EXC_RATE = Dt.Rows[i]["EXC_RATE"].ToString();
                                    string re_amnt = Dt.Rows[i]["RE_AMT"].ToString();
                                    double ramt = double.Parse(re_amnt);
                                    OtherReBO.RE_AMT = (Math.Round(ramt, 3)).ToString();
                                    OtherReBO.JUSTIFY = Dt.Rows[i]["JUSTIFY"].ToString();
                                    OtherReBO.RECEIPT_FILE = Dt.Rows[i]["RECEIPT_FILE"].ToString();
                                    OtherReBO.RECEIPT_FID = Dt.Rows[i]["RECEIPT_FID"].ToString();
                                    OtherReBO.RECEIPT_FPATH = Dt.Rows[i]["RECEIPT_FPATH"].ToString();

                                    OtherRebl.Create_IExpTypes_Add(OtherReBO, ref status);

                                    if (status == false)
                                    {
                                        ////MsgCls("iExpense claims saved successfully !", lblMessageBoard, Color.Green);
                                        MsgCls("iExpense ID " + IEXp_ID + " claims saved successfully !", lblMessageBoard, Color.Green);

                                        string alert = "IExpense ID" + IEXp_ID + " claims saved successfully !";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='iExpense_Request.aspx';", true);
                                        lblIndent.Text = "";
                                        dvlineitem.Visible = true;

                                        lbtnEdit.Visible = true;
                                        lbtAddNew.Visible = true;
                                        collapse();
                                    }

                                }
                            }
                        }
                    }
                    ////ViewState["IEXP_TYPESDT"] = null;
                    ////grd_IExpInfo.DataSource = null;
                    ////grd_IExpInfo.DataBind();
                    ////ClearIExpense();
                    ////Clear();
                    ////DDLCurrency.Enabled = true;
                    ////DDLCurrency.SelectedValue = "0";
                    ////ddlTask.Enabled = true;
                    ////ddlTask.SelectedValue = "0";
                    ////ddlProjectCode.Enabled = true;
                    ////ddlProjectCode.SelectedValue = "0";
                    ////txtPurpose.Enabled = true;
                    ////BtnSave.Visible = false;
                    ////btnSubmit.Visible = false;
                    //PnlExpenseAdd.Visible = false;
                    dvlineitem.Visible = true;
                }

                else
                {
                    MsgCls("Please Add Atleast one Claim Item!", lblIndent, Color.Red);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please Add Atleast one Claim Item!');", true);
                }
            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void SendMailMethod(int? IEXp_ID, string Project, string Task, string Purpose, string ReCurrency, decimal Samnt)
        {
            try
            {
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                grd_IExpInfo.Columns[9].Visible = false;
                grd_IExpInfo.Columns[10].Visible = false;
                grd_IExpInfo.FooterRow.ForeColor = System.Drawing.Color.Black;
                grd_IExpInfo.FooterRow.Visible = true;
                grd_IExpInfo.RenderControl(hw1);
                grd_IExpInfo.Columns[9].Visible = true;
                grd_IExpInfo.Columns[10].Visible = true;

                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;
                string APPROVED_BY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                string Entity = "";

                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();

                objcontext.sp_Get_MailList_IExpense(IEXp_ID, User.Identity.Name, ref APPROVED_BY1, ref Approver_Name,
                    ref Approver_Email, ref EMP_Name, ref EMP_Email, ref Entity);

                strSubject = "iExpense Request " + IEXp_ID + " has been Raised by" + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.";

                RecipientsString = Approver_Email;
                strPernr_Mail = EMP_Email;

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>iExpense Request " + IEXp_ID + " has been Raised by" + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.<br/><br/></b>";
                body += "<b>Entity with iExpense ID  :  " + Entity + " : " + IEXp_ID + "</b><br/><br/>";
                body += "<b>iExpense Header Details :<hr /><br/>";
                body += "Project  :  " + Project + "<br/>";
                body += "Task     :  " + Task + "<br/>";
                body += "Purpose  :  " + Purpose + "<br/>";
                body += "Total Reimbursement Amount  :  " + Samnt.ToString("#,##0.00") + " ( " + ReCurrency + " ) <br/> <br/>";
                //body += "Reimbursement Currency  :  " + ReCurrency + " ( " + ReCurrency + " ) <br/>";
                body += "<b>iExpense Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";

                //    //End of preparing the mail body-------------------------------------------


                ////Newly added Starts
                Thread email = new Thread(delegate()
                {
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                });

                email.IsBackground = true;
                email.Start();
                ////Newly added Ends

                ////iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "iExpense created successfully and Mail sent successfully.";
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "iExpense created successfully. Error in Mail sending.";
                return;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private void ClearIExpense()
        {
            ddlProjectCode.ClearSelection();
            ddlTask.ClearSelection();
            txtPurpose.Text = string.Empty;
            DDLCurrency.ClearSelection();
        }

        protected void grd_IExpInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "EDITITEMS":
                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    foreach (GridViewRow row in grd_IExpInfo.Rows)
                    {
                        row.BackColor = row.RowIndex.Equals(rowIndex) ?
                        System.Drawing.Color.LightGray :
                        System.Drawing.Color.White;
                    }

                    MsgCls("", lblIndent, Color.Transparent);
                    btnAdd.Visible = false;
                    btnUpdateItems.Visible = true;

                    string EXP_TYPE = "";
                    string S_DATE = "";
                    string EXPT_AMT = "";
                    string EXPT_CURR = "";
                    string EXC_RATE = "";
                    string RE_AMT = "";
                    string RCURR = "";
                    string JUSTIFY = "";
                    string RECEIPT_FILE = "";
                    string NO_DAYS = "";
                    string RECEIPT_FIID = "";
                    string RECEIPT_FPATH = "";
                    Clear();
                    int index = Convert.ToInt32(e.CommandArgument);
                    ViewState["RowIndex"] = index;


                    int ID1 = int.Parse(grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                    ViewState["IDtoAdd"] = ID1;
                    using (DataTable Dt = (DataTable)ViewState["IEXP_TYPESDT"])
                    {

                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {

                            EXP_TYPE = Dt.Rows[index]["EXP_TYPE"].ToString();
                            S_DATE = Dt.Rows[index]["S_DATE"].ToString();
                            EXPT_AMT = Dt.Rows[index]["EXPT_AMT"].ToString();
                            EXPT_CURR = Dt.Rows[index]["EXPT_CURR"].ToString();
                            EXC_RATE = Dt.Rows[index]["EXC_RATE"].ToString();
                            RE_AMT = Dt.Rows[index]["RE_AMT"].ToString();
                            RCURR = DDLCurrency.SelectedValue;
                            JUSTIFY = Dt.Rows[index]["JUSTIFY"].ToString();
                            RECEIPT_FILE = Dt.Rows[index]["RECEIPT_FILE"].ToString();
                            NO_DAYS = Dt.Rows[index]["NO_DAYS"].ToString();

                            RECEIPT_FIID = Dt.Rows[index]["RECEIPT_FID"].ToString();
                            ViewState["Receiptfileid"] = Dt.Rows[index]["RECEIPT_FID"].ToString();
                            RECEIPT_FPATH = Dt.Rows[index]["RECEIPT_FPATH"].ToString();
                            ViewState["Receiptfilepath"] = Dt.Rows[index]["RECEIPT_FPATH"].ToString();
                        }
                    }
                    //      txtStartDate    txtExpenditureAmount       
                    //txtExchangeRate        txtJustification  
                    ddlExpenseType.SelectedValue = EXP_TYPE;
                    txtStartDate.Text = (DateTime.Parse(S_DATE)).ToString();
                    txtExpenditureAmount.Text = EXPT_AMT;
                    ddlExpenditureCurrency.SelectedValue = EXPT_CURR;
                    txtExchangeRate.Text = EXC_RATE;
                    lblReimbursableAmount.Text = Convert.ToDecimal(RE_AMT).ToString("#,##0.00");////decimal.Parse(RE_AMT).ToString("0.000");
                    LblReimbursableCurrency.Text = RCURR;
                    HF_ReimbursAmnt.Value = RCURR;
                    txtJustification.Text = JUSTIFY;
                    if (RECEIPT_FILE == "NO")
                    {
                        cb.Checked = false;
                    }
                    else
                    {
                        cb.Checked = true;
                    }

                    txtNoofDays.Text = NO_DAYS;
                    grd_IExpInfo.Columns[10].Visible = false;
                    break;


                case "DELETEFILE":


                    int IDDeletefile = int.Parse(grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());

                    int indexdf = Convert.ToInt32(e.CommandArgument);
                    //ViewState["RowIndex"] = index;

                    using (DataTable Dt = (DataTable)ViewState["IEXP_TYPESDT"])
                    {

                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {


                            Dt.Rows[indexdf]["RECEIPT_FID"] = "";
                            Dt.Rows[indexdf]["RECEIPT_FPATH"] = "";
                        }


                        grd_IExpInfo.DataSource = Dt;
                        grd_IExpInfo.DataBind();
                        //ClearClaimLineItems();
                        decimal d = 0;
                        decimal total = Dt.AsEnumerable()
                         .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                        string createdby = User.Identity.Name;
                        decimal SettlementAmt = 0;
                        string SettlementCurr = string.Empty;


                        OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                        OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, DDLCurrency.SelectedValue);
                        foreach (OtherReimbursementsbo objBo1 in objLst1)
                        {
                            SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                            SettlementCurr = objBo1.SETTLECURR.ToString();
                        }

                        grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";

                        grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (DDLCurrency.SelectedValue) + ")";
                    }

                    break;

                case "DOWNLOAD":
                    //  string filename= grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FPATH"].ToString();
                    string filePath = e.CommandArgument.ToString();
                    Response.ContentType = "application/octet-stream";
                    //Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                    Response.WriteFile(filePath);
                    Response.End();
                    break;

                case "DELETE":
                    int ID = int.Parse(grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                    using (DataTable Dt = (DataTable)ViewState["IEXP_TYPESDT"])
                    {
                        DataRow[] rows = (from t in Dt.AsEnumerable().Cast<DataRow>()
                                          where t.Field<int>("ID") == ID
                                          select t).ToArray();

                        foreach (DataRow row in rows)
                        {
                            Dt.Rows.Remove(row);
                        }

                        ViewState["IEXP_TYPESDT"] = null;
                        ViewState["IEXP_TYPESDT"] = Dt;
                        if (Dt.Rows.Count > 0)
                        {

                            grd_IExpInfo.DataSource = (DataTable)ViewState["IEXP_TYPESDT"];
                            grd_IExpInfo.DataBind();
                            string currency = DDLCurrency.SelectedValue.ToString();
                            decimal d = 0;
                            decimal total = Dt.AsEnumerable()
                             .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d))
                             .Sum(r => d);
                            string createdby = User.Identity.Name;
                            decimal SettlementAmt = 0;
                            string SettlementCurr = string.Empty;
                            OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                            OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, currency);
                            foreach (OtherReimbursementsbo objBo1 in objLst1)
                            {
                                SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                                SettlementCurr = objBo1.SETTLECURR.ToString();
                            }
                            grd_IExpInfo.FooterRow.Cells[4].Text = "Total";

                            grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("N2") + "(" + (currency) + ")";
                            MsgCls("iExpense Item Deleted Successfully !", lblIndent, Color.Green);
                            ddlExpenseType.Focus();
                            //grd_IExpInfo.FooterRow.Cells[6].Text = "Settlement Amount";

                            //grd_IExpInfo.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                            //grd_IExpInfo.FooterRow.Cells[7].Text = SettlementAmt + "(" + (SettlementCurr) + ")";
                        }
                        else
                        {
                            if (ViewState["IEXP_TYPESDT"] != null)
                            {
                                using (DataTable Dts = (DataTable)ViewState["IEXP_TYPESDT"])
                                {
                                    if (Dts.Rows.Count > 0)
                                    {
                                        grd_IExpInfo.DataSource = (DataTable)ViewState["IEXP_TYPESDT"];
                                        grd_IExpInfo.DataBind();
                                        string currency = DDLCurrency.SelectedValue.ToString();
                                        decimal d = 0;
                                        decimal total = Dt.AsEnumerable()
                                         .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d))
                                         .Sum(r => d);
                                        string createdby = User.Identity.Name;
                                        decimal SettlementAmt = 0;
                                        string SettlementCurr = string.Empty;
                                        OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                                        OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, currency);
                                        foreach (OtherReimbursementsbo objBo1 in objLst1)
                                        {
                                            SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                                            SettlementCurr = objBo1.SETTLECURR.ToString();
                                        }
                                        grd_IExpInfo.FooterRow.Cells[4].Text = "Total";

                                        grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                                        grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("N2") + "(" + (currency) + ")";
                                        MsgCls("Iexpense Item Deleted Successfully !", lblIndent, Color.Green);
                                        ddlExpenseType.Focus();
                                        //grd_IExpInfo.FooterRow.Cells[6].Text = "Settlement Amount";

                                        //grd_IExpInfo.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                                        //grd_IExpInfo.FooterRow.Cells[7].Text = SettlementAmt + "(" + (SettlementCurr) + ")";

                                    }
                                    else
                                    {
                                        ViewState["IEXP_TYPESDT"] = null;
                                        grd_IExpInfo.DataSource = null;
                                        grd_IExpInfo.DataBind();
                                        LoadExpenseType(ddlTask.SelectedValue.ToString().Trim());
                                        ClearIExpense();
                                        DDLCurrency.Enabled = true;
                                        ddlTask.Enabled = true;
                                        ddlProjectCode.Enabled = true;
                                        txtPurpose.Enabled = true;
                                        ddlProjectCode.Focus();
                                        MsgCls("iExpense Item Deleted Successfully !", lblIndent, Color.Green);
                                        BtnSave.Visible = false;
                                        btnSubmit.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

        }

        protected void btnUpdateItems_Click(object sender, EventArgs e)
        {
            try
            {
                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                DateTime StartDate = new DateTime(0001, 01, 01);
                decimal ExchangeRate = 0.0M;
                string fileupdate = string.Empty;
                if (decimal.TryParse(txtExchangeRate.Text, out ExchangeRate))
                { }
                //int LID = int.Parse(GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                //ViewState["LIDtoAdd"] = LID;
                int rowindex = int.Parse(ViewState["RowIndex"].ToString());
                int lid = int.Parse(ViewState["IDtoAdd"].ToString());

                using (DataTable Dt = (DataTable)ViewState["IEXP_TYPESDT"])
                {

                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        //  Dt.Columns.Add("ID", typeof(int));

                        //ID
                        Dt.Rows[rowindex]["EXP_TYPE"] = ddlExpenseType.SelectedValue;
                        if (DateTime.TryParse(txtStartDate.Text, out StartDate))
                        {
                            Dt.Rows[rowindex]["S_DATE"] = StartDate;
                        }
                        Dt.Rows[rowindex]["EXPT_AMT"] = txtExpenditureAmount.Text;
                        Dt.Rows[rowindex]["EXPT_CURR"] = ddlExpenditureCurrency.SelectedValue;
                        Dt.Rows[rowindex]["EXC_RATE"] = txtExchangeRate.Text;
                        Dt.Rows[rowindex]["RE_AMT"] = Math.Abs(ExchangeRate < 0 ? decimal.Parse(txtExpenditureAmount.Text) / ExchangeRate : decimal.Parse(txtExpenditureAmount.Text) * ExchangeRate);
                        Dt.Rows[rowindex]["JUSTIFY"] = txtJustification.Text;
                        Dt.Rows[rowindex]["RECEIPT_FILE"] = cb.Checked ? "YES" : "NO";

                        if (fuAttachments != null)
                        {
                            if (fuAttachments.HasFile)
                            {
                                Dt.Rows[rowindex]["RECEIPT_FID"] = fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "";
                                Dt.Rows[rowindex]["RECEIPT_FPATH"] = fuAttachments.HasFile ? "~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "";
                                if (fuAttachments.HasFile)
                                {
                                    fuAttachments.SaveAs(Server.MapPath("~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName));
                                }

                                fileupdate = "UpdateFileUpload";
                            }
                        }
                        else
                        {
                            Dt.Rows[rowindex]["RECEIPT_FID"] = ViewState["Receiptfileid"].ToString();
                            Dt.Rows[rowindex]["RECEIPT_FPATH"] = ViewState["Receiptfilepath"].ToString();
                            fileupdate = "UpdateNoFileUpload";
                        }
                        //Dt.Rows[rowindex]["RECEIPT_FIID"] = fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "";
                        //Dt.Rows[rowindex]["RECEIPT_FPATH"] = fuAttachments.HasFile ? "~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "";

                        Dt.Rows[rowindex]["NO_DAYS"] = txtNoofDays.Text;
                        Dt.Rows[rowindex]["EXP_TYPE_TEXT"] = ddlExpenseType.SelectedItem.Text;
                        ////if (fuAttachments != null)
                        ////{
                        ////    if (fuAttachments.HasFile)
                        ////    { fuAttachments.SaveAs(Server.MapPath("~/TravelDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName)); }
                        ////}

                    }
                    grd_IExpInfo.DataSource = Dt;
                    grd_IExpInfo.DataBind();
                    //ClearClaimLineItemssubmit();
                    ClearAfterAdd();
                    decimal d = 0;
                    decimal total = Dt.AsEnumerable()
                     .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                    string createdby = User.Identity.Name;
                    decimal SettlementAmt = 0;
                    string SettlementCurr = string.Empty;


                    OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                    OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, DDLCurrency.SelectedValue);
                    foreach (OtherReimbursementsbo objBo1 in objLst1)
                    {
                        SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                        SettlementCurr = objBo1.SETTLECURR.ToString();
                    }

                    grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";

                    grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (DDLCurrency.SelectedValue) + ")";
                    MsgCls("iExpense Item Updated Successfully !", lblIndent, Color.Green);
                    grd_IExpInfo.Columns[10].Visible = true;
                }

                btnAdd.Visible = true;
                btnUpdateItems.Visible = false;
                ddlExpenseType.Focus();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        protected void grd_IExpInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void txtExpenditureAmount_TextChanged(object sender, EventArgs e)
        {
            txtExchangeRate.Text = string.Empty;
            lblReimbursableAmount.Text = string.Empty;
            LblReimbursableCurrency.Text = string.Empty;
            GetExchangeRate();
            ////txtJustification.Focus();
            txtExpenditureAmount.Focus();
            ////Expand();
        }

        protected void cb_CheckedChanged(object sender, EventArgs e)
        {
            if (cb.Checked)
            {
                fuAttachments.Enabled = false;
                fuAttachmentsfname.Enabled = false;
                ////cb.Focus();
            }
            else
            {
                fuAttachments.Enabled = true;
                fuAttachmentsfname.Enabled = true;
                ////cb.Focus();
            }
        }

        protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadExpenseType(ddlTask.SelectedValue.ToString());
            ////DDLCurrency.Focus();
            lblTask.Text = ddlTask.SelectedItem.Text;
            ddlTask.Focus();
            ////Expand();
        }

        protected void ddlProjectCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProjectCode.Focus();

            ////Newly Added Starts
            checkProject(User.Identity.Name, ddlProjectCode.SelectedItem.Value);
            ////Newly Added Ends
            ////Expand();

        }

        protected void ddlExpenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////ddlExpenditureCurrency.Focus();
            ddlExpenseType.Focus();
        }

        protected void cbEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEdit.Checked)
                txtExchangeRate1.Enabled = true;
            else
                txtExchangeRate1.Enabled = false;
        }

        private void checkProject(string PERNR, string POSID)
        {
            try
            {
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                objcontext.CommandTimeout = 0;////Timeout
                int iReturnValue = objcontext.usp_project_validation(PERNR, POSID);
                if (iReturnValue != 0)
                {
                    return;
                }
                else
                {
                    btnAdd.Visible = true;
                }
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                objcontext.Dispose();
            }

            catch (Exception Ex)
            {
                StringBuilder B = new StringBuilder();
                switch (Ex.Message)
                {
                    case "-01":
                        MsgCls("You're selecting a wrong Entity Internal Project code !", lblMessageBoard, Color.Red);
                        B.Append("<ul class=\"LiNone\">");
                        B.Append("<li>You're selecting a wrong Entity Internal Project code !</li>");
                        B.Append("</ul>");
                        lblMessageBoard.Text = B.ToString();
                        btnAdd.Visible = false;
                        break;
                    case "-02":
                        MsgCls("This project code internal number is not maintained ! Please contact project team", lblMessageBoard, Color.Red);
                        B.Append("<ul class=\"LiNone\">");
                        B.Append("<li>This project code internal number is not maintained ! Please contact project team</li>");
                        B.Append("</ul>");
                        lblMessageBoard.Text = B.ToString();
                        btnAdd.Visible = false;
                        break;
                    case "-03":
                        MsgCls("This project code internal number is not released !  Please contact project team", lblMessageBoard, Color.Red);
                        B.Append("<ul class=\"LiNone\">");
                        B.Append("<li>This project code internal number is not released ! Please contact project team</li>");
                        B.Append("</ul>");
                        lblMessageBoard.Text = B.ToString();
                        btnAdd.Visible = false;
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true);
                        btnAdd.Visible = false;
                        break;
                }
            }

        }

        //protected void Tab1_Click(object sender, EventArgs e)
        //{
        //    Expand();
        //}

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            dvlineitem.Visible = true;
            txtStartDate.Focus();
        }

        protected void lbtAddNew_Click(object sender, EventArgs e)
        {
            ViewState["IEXP_TYPESDT"] = null;
            grd_IExpInfo.DataSource = null;
            grd_IExpInfo.DataBind();
            ClearIExpense();
            Clear();
            DDLCurrency.Enabled = true;
            DDLCurrency.SelectedValue = "0";
            ddlTask.Enabled = true;
            ddlTask.SelectedValue = "0";
            ddlProjectCode.Enabled = true;
            ddlProjectCode.SelectedValue = "0";
            txtPurpose.Enabled = true;
            BtnSave.Visible = false;
            btnSubmit.Visible = false;
            lblMessageBoard.Text = "";

            lbtnEdit.Visible = false;
            lbtAddNew.Visible = false;

            //---------------------------------
            ltTask.Text = "NA";
            ltProject.Text = "NA";
            ltReimbAmt.Text = "NA";
            //---------------------------------
        }
    }
}
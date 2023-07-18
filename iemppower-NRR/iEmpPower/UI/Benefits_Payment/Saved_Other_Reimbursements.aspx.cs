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
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class Saved_Other_Reimbursements : System.Web.UI.Page
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
                    //LoadIExpenseGridView();

                    if (Request.QueryString["NC"] != null)
                    {
                        if (Request.QueryString["NC"] == "E")
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
                Loadfileupload();
                displayInfo:
                {
                    ////Console.WriteLine("");
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
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
            pnexpForm.Visible = true;
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
            DataTable dt = ConvertToDataTable(IexpboList);



            decimal d = 0;
            decimal total = dt.AsEnumerable()
                     .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
            grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";


            grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (rcurr) + ")";
            int totalRowsCount = grd_IExpInfo.Rows.Count;
            ViewState["totalRowsCount"] = totalRowsCount;


            ltiExpID.Text = IEXP_ID.ToString();
            ltTask.Text = ddlTask.SelectedItem.Text;
            ltProject.Text = ddlProjectCode.SelectedItem.Text;
            ltReimbAmt.Text = grd_IExpInfo.FooterRow.Cells[5].Text;



            ddlExpenseType.Focus();
            DDLCurrency.Enabled = false;
            ddlTask.Enabled = false;
            ddlProjectCode.Enabled = false;
            txtPurpose.Enabled = false;
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

        private void LoadIExpenseGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();
                string ApproverId = User.Identity.Name;
                IexpboList1 = ExpenseblObj.Load_ExpenseDetails_forsaved(ApproverId);
                Session.Add("IexpGrdInfo", IexpboList1);

                if (IexpboList1 == null || IexpboList1.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                    grdIexpdetails.Visible = false;
                    grdIexpdetails.DataSource = null;
                    grdIexpdetails.DataBind();
                    return;
                }
                else
                {
                    grdIexpdetails.Visible = true;
                    grdIexpdetails.DataSource = IexpboList1;
                    grdIexpdetails.SelectedIndex = -1;
                    grdIexpdetails.DataBind();
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

        protected void grdIexpdetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<OtherReimbursementsbo> IexpboList = (List<OtherReimbursementsbo>)Session["IexpGrdInfo"];
            bool objSortOrder = (bool)Session["bSortedOrder"];
            switch (e.SortExpression.ToString().Trim())
            {
                case "IEXP_ID":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.IEXP_ID.Value.CompareTo(objBo2.IEXP_ID.Value)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.IEXP_ID.Value.CompareTo(objBo1.IEXP_ID.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "PROJID":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.PROJID.ToString().CompareTo(objBo2.PROJID.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.PROJID.ToString().CompareTo(objBo1.PROJID.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "POST1":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.POST1.ToString().CompareTo(objBo2.POST1.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.POST1.ToString().CompareTo(objBo1.POST1.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "TASK":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.TASK.ToString().CompareTo(objBo2.TASK.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.TASK.ToString().CompareTo(objBo1.TASK.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "STATUS":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.STATUS.ToString().CompareTo(objBo2.STATUS.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.STATUS.ToString().CompareTo(objBo1.STATUS.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "RCURR":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.RCURR.ToString().CompareTo(objBo2.RCURR.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.RCURR.ToString().CompareTo(objBo1.RCURR.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "RE_AMT":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return ((decimal.Parse(objBo1.RE_AMT)).CompareTo(decimal.Parse(objBo2.RE_AMT))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return ((decimal.Parse(objBo2.RE_AMT)).CompareTo(decimal.Parse(objBo1.RE_AMT))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CREATED_ON":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.CREATED_ON.Value.CompareTo(objBo2.CREATED_ON.Value)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.CREATED_ON.Value.CompareTo(objBo1.CREATED_ON.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }

                    break;


            }

            grdIexpdetails.DataSource = IexpboList;
            grdIexpdetails.DataBind();

            Session.Add("IexpGrdInfo", IexpboList);

        }

        protected void grdIexpdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdIexpdetails.PageIndex = e.NewPageIndex;

            LoadIExpenseGridView();
            //searchdetails();
            grdIexpdetails.SelectedIndex = -1;
        }

        protected void grdIexpdetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in grdIexpdetails.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }
                        OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                        List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                        pnexpForm.Visible = true;
                        int IEXP_ID = int.Parse(grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString().Trim());
                        ViewState["IEXP_ID"] = IEXP_ID.ToString();
                        string purpose = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PURPOSE"].ToString().Trim();
                        string Task = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["TASK"].ToString().Trim();
                        string rcurr = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString().Trim();
                        string prjid = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PROJID"].ToString().Trim();


                        ddlProjectCode.SelectedValue = prjid;
                        //if (Task == "Billable")
                        //{
                        //    Task = "B";
                        //}
                        //else
                        //{
                        //    Task = "NB";
                        //}

                        ddlTask.SelectedValue = Task;
                        LoadExpenseType(ddlTask.SelectedValue.ToString().Trim());
                        txtPurpose.Text = purpose;
                        DDLCurrency.SelectedValue = rcurr;


                        IexpboList = ExpenseblObj.Load_IEXPDetails(IEXP_ID);
                        grd_IExpInfo.DataSource = IexpboList;
                        grd_IExpInfo.DataBind();
                        DataTable dt = ConvertToDataTable(IexpboList);
                        decimal d = 0;
                        decimal total = dt.AsEnumerable()
                                 .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                        grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";


                        grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        grd_IExpInfo.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (rcurr) + ")";
                        int totalRowsCount = grd_IExpInfo.Rows.Count;
                        ViewState["totalRowsCount"] = totalRowsCount;
                        ddlExpenseType.Focus();
                        DDLCurrency.Enabled = false;
                        ddlTask.Enabled = false;
                        ddlProjectCode.Enabled = false;
                        txtPurpose.Enabled = false;
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
                        this.Page.Form.Enctype = "multipart/form-data";
                        LoadExchangeRate();
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
                        //   MsgCls("Please select Expenditure Currency !", lblIndent, Color.Red);

                    }
                    if (DDLCurrency.SelectedValue == "0")
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Reimbursement Currency !')", true);
                        // MsgCls("Please select Reimbursement Currency !", lblIndent, Color.Red);

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

                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00");//(Math.Round(ramt, 2)).ToString();
                            LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;
                            //lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); //(Math.Round(ramt, 2)).ToString();
                            LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;
                            // lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                    }
                    else
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Expenditure Amount!')", true);
                        //MsgCls("Please enter correct Expenditure Amount!", lblIndent, Color.Red);


                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Exchange Rate!')", true);
                    //MsgCls("Please enter correct Exchange Rate!", lblIndent, Color.Red);

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

                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00");// (Math.Round(ramt, 2)).ToString();
                            //lblReimbursableAmount.Text = ramt.ToString();
                            LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); //(Math.Round(ramt, 2)).ToString();
                            // lblReimbursableAmount.Text = ramt.ToString();
                            LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;
                            MsgCls("", lblIndent, Color.White);
                        }
                    }
                    else
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Expenditure Amount!')", true);
                        //  MsgCls("Please enter correct Expenditure Amount!", lblIndent, Color.Red);


                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Exchange Rate!')", true);
                    //MsgCls("Please enter correct Exchange Rate!", lblIndent, Color.Red);

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
                lblReimbursableAmount.Text = Convert.ToDecimal(ramt).ToString("#,##0.00"); //ramt.ToString();
                LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;
            }
            else
            {
                ramt = ExpenditureAmount1 * Math.Abs(ExchangeRate1);
                //lblReimbursableAmount.Text = (Math.Round(ramt, 2)).ToString();
                lblReimbursableAmount.Text = Convert.ToDecimal(ramt).ToString("#,##0.00"); //ramt.ToString();
                LblReimbursableCurrency.Text = DDLCurrency.SelectedValue;

            }
            return lblReimbursableAmount.Text;
        }

        protected void DDLCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            txtExchangeRate.Text = string.Empty;
            lblReimbursableAmount.Text = string.Empty;
            GetExchangeRate();
            txtStartDate.Focus();
        }

        protected void ddlExpenditureCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            txtExchangeRate.Text = string.Empty;
            lblReimbursableAmount.Text = string.Empty;
            GetExchangeRate();
            txtExpenditureAmount.Focus();
        }

        protected void txtExchangeRate_TextChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            double ExchangeRate = 0.0;
            double ExpenditureAmount = 0.0;

            if (txtExchangeRate1.Text.Trim() == "")
            {
                MsgCls("Exchange Rate Cannot be empty!", lblIndent, Color.Red);
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

                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); //(Math.Round(ramt, 2)).ToString();
                            //lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            lblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); //(Math.Round(ramt, 2)).ToString();
                            // lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                    }
                    else
                    {
                        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Expenditure Amount!')", true);
                        MsgCls("Please enter correct Expenditure Rate!", lblIndent, Color.Red);
                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Exchange Rate!')", true);
                    MsgCls("Please enter correct Exchange Rate!", lblIndent, Color.Red);

                }
            }

            txtExchangeRate.Focus();

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
            // ddlExpenseType.ClearSelection();
            txtStartDate.Text = string.Empty;
            txtNoofDays.Text = string.Empty;
            txtExpenditureAmount.Text = string.Empty;
            //ddlExpenditureCurrency.ClearSelection();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                OtherReimbursementsbo IexpboList = new OtherReimbursementsbo();
                bool? status = false;
                decimal? CalcReAmt = 0;
                string reamtcurr = "";
                int? CountofLid = 0;

                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                DateTime StartDate = new DateTime(0001, 01, 01);
                decimal ExchangeRate = 0.0M;

                if (decimal.TryParse(txtExchangeRate.Text, out ExchangeRate))
                { }
                MsgCls(string.Empty, lblIndent, Color.Transparent);
                if (DateTime.TryParse(txtStartDate.Text, out StartDate) && ddlTask.SelectedValue != "0")
                {

                    int gvrowscount = int.Parse(ViewState["totalRowsCount"].ToString().Trim());
                    OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();

                    objcontext.sp_Get_CountofId_IexpenseItems(int.Parse(ViewState["IEXP_ID"].ToString()), ref CountofLid);
                    if (CountofLid != null)
                    {
                        CountofLid = CountofLid + 1;
                    }

                    else
                    {
                        CountofLid = 1;
                    }

                    IexpboList.IEXP_ID = int.Parse(ViewState["IEXP_ID"].ToString());
                    IexpboList.ID = CountofLid;
                    IexpboList.EXP_TYPE = ddlExpenseType.SelectedValue;
                    IexpboList.S_DATE = StartDate;
                    IexpboList.NO_DAYS = txtNoofDays.Text;

                    IexpboList.EXPT_AMT = txtExpenditureAmount.Text;
                    IexpboList.EXPT_CURR = ddlExpenditureCurrency.SelectedValue;
                    IexpboList.EXC_RATE = txtExchangeRate.Text;
                    IexpboList.RE_AMT = (Math.Abs(ExchangeRate < 0 ? decimal.Parse(txtExpenditureAmount.Text) / ExchangeRate : decimal.Parse(txtExpenditureAmount.Text) * ExchangeRate)).ToString();
                    IexpboList.JUSTIFY = txtJustification.Text;
                    IexpboList.RECEIPT_FILE = cb.Checked ? "YES" : "NO";
                    IexpboList.RECEIPT_FID = fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "";
                    IexpboList.RECEIPT_FPATH = fuAttachments.HasFile ? "~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "";

                    if (fuAttachments.HasFile)
                    { fuAttachments.SaveAs(Server.MapPath("~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName)); }

                    ExpenseblObj.IEXP_ClaimItems(IexpboList, ref status);
                    if (status == true)
                    {
                        MsgCls("Iexpese types added successfully !", lblIndent, Color.Green);
                        grd_IExpInfo.Columns[10].Visible = true;
                    }
                    OtherReimbursementsbl ExpenseblObjb = new OtherReimbursementsbl();

                    List<OtherReimbursementsbo> IexpboListb = new List<OtherReimbursementsbo>();
                    IexpboListb = ExpenseblObjb.Load_SavedClaimDetails(int.Parse(ViewState["IEXP_ID"].ToString()), ref CalcReAmt, ref reamtcurr);
                    grd_IExpInfo.DataSource = IexpboListb;
                    grd_IExpInfo.DataBind();

                    DDLCurrency.Enabled = false;
                    ddlTask.Enabled = false;
                    ddlProjectCode.Enabled = false;
                    txtPurpose.Enabled = false;
                    ClearAfterAdd();

                    grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";

                    grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    //GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmt + "(" + (DDLReimbursementCurrency.SelectedValue) + ")"; decimal.Parse(CalcReAmtf.ToString()).ToString("#,##0.00") 
                    grd_IExpInfo.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLCurrency.SelectedValue) + ")";
                    ddlExpenseType.Focus();

                    ltTask.Text = ddlTask.SelectedItem.Text;
                    ltProject.Text = ddlProjectCode.SelectedItem.Text;
                    ltReimbAmt.Text = grd_IExpInfo.FooterRow.Cells[5].Text;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Invalid Data');", true);
                }
            }

            catch (Exception Ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                int? IEXp_ID = 0;
                decimal ClaimTotal = 0;
                decimal? CalcReAmt = 0;
                string reamtcurr = "";
                OtherReimbursementsbl ExpenseblObjb = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboListb = new List<OtherReimbursementsbo>();

                OtherReimbursementsbl ExpenseblObjb1 = new OtherReimbursementsbl();
                OtherReimbursementsbo IexpboListb1 = new OtherReimbursementsbo();
                IexpboListb = ExpenseblObjb.Load_SavedClaimDetails(int.Parse(ViewState["IEXP_ID"].ToString()), ref CalcReAmt, ref reamtcurr);
                grd_IExpInfo.FooterRow.Cells[4].Text = "Total";

                grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                grd_IExpInfo.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLCurrency.SelectedValue) + ")";


                if (IexpboListb == null || IexpboListb.Count == 0)
                {
                    MsgCls("Please Add Atleast one Iexpense Item!", lblIndent, Color.Red);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please Add Atleast one Iexpense Item!');", true);
                }
                else
                {

                    bool? status = true;

                    IexpboListb1.IEXP_ID = int.Parse(ViewState["IEXP_ID"].ToString());
                    IexpboListb1.PROJID = ddlProjectCode.SelectedValue;
                    IexpboListb1.TASK = ddlTask.SelectedValue;
                    IexpboListb1.PURPOSE = txtPurpose.Text.Trim();
                    IexpboListb1.RCURR = DDLCurrency.SelectedValue;
                    IexpboListb1.CREATED_ON = DateTime.Now;
                    IexpboListb1.CREATED_BY = User.Identity.Name;
                    IexpboListb1.APPROVED_ON1 = DateTime.MinValue;
                    IexpboListb1.APPROVED_BY1 = "";
                    IexpboListb1.REMARKS1 = string.Empty;
                    IexpboListb1.APPROVED_ON2 = DateTime.MinValue;
                    IexpboListb1.APPROVED_BY2 = "";
                    IexpboListb1.REMARKS2 = string.Empty;
                    IexpboListb1.APPROVED_ON3 = DateTime.MinValue;
                    IexpboListb1.APPROVED_BY3 = "";
                    IexpboListb1.REMARKS3 = string.Empty;
                    IexpboListb1.APPROVED_ON4 = DateTime.MinValue;
                    IexpboListb1.APPROVED_BY4 = "";
                    IexpboListb1.REMARKS4 = string.Empty;
                    IexpboListb1.APPROVED_ON5 = DateTime.MinValue;
                    IexpboListb1.APPROVED_BY5 = "";
                    IexpboListb1.REMARKS5 = string.Empty;
                    IexpboListb1.APPROVED_ON6 = DateTime.MinValue;
                    IexpboListb1.APPROVED_BY6 = "";
                    IexpboListb1.REMARKS6 = string.Empty;
                    IexpboListb1.APPROVED_ON7 = DateTime.MinValue;
                    IexpboListb1.APPROVED_BY7 = "";
                    IexpboListb1.REMARKS7 = string.Empty;
                    IexpboListb1.APPROVED_ON8 = DateTime.MinValue;
                    IexpboListb1.APPROVED_BY8 = "";
                    IexpboListb1.REMARKS8 = string.Empty;
                    IexpboListb1.APPROVED_ON9 = DateTime.MinValue;
                    IexpboListb1.APPROVED_BY9 = "";
                    IexpboListb1.REMARKS9 = string.Empty;
                    IexpboListb1.STATUS = "";
                    IexpboListb1.TotalAmount = decimal.Parse(CalcReAmt.ToString()); //+ ClaimTotal;// ClaimTotal + Dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                    ExpenseblObjb1.UpdateCreateIexp(IexpboListb1, ref status);

                    if (status == false)
                    {
                        SendMailMethod(int.Parse(ViewState["IEXP_ID"].ToString()), ddlProjectCode.SelectedItem.Text, ddlTask.SelectedValue, txtPurpose.Text.Trim(), DDLCurrency.SelectedValue, decimal.Parse(CalcReAmt.ToString()));

                        LoadIExpenseGridView();

                       //// MsgCls("IExpense Created Successfully and Mail sent successfully", lblMessageBoard, Color.Green);
                        MsgCls("iExpense  ID" + ViewState["IEXP_ID"].ToString() + "  Created Successfully!", lblMessageBoard, Color.Green);
                        string alert = "iExpense  ID" + ViewState["IEXP_ID"].ToString() + "  Created Successfully!";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='iExpense_Request.aspx';", true);
                    }
                    //ViewState["TR_CLAIM"] = null;
                    //GV_TravelExpReq.DataSource = null;
                    //GV_TravelExpReq.DataBind();
                    ddlTask.SelectedValue = "0";
                    DDLCurrency.SelectedValue = "0";
                    Clear();
                    ClearIExpense();
                    DDLCurrency.Enabled = true;
                    DDLCurrency.SelectedValue = "0";
                    ddlTask.Enabled = true;
                    ddlTask.SelectedValue = "0";
                    ddlProjectCode.Enabled = true;
                    ddlProjectCode.SelectedValue = "0";
                    txtPurpose.Enabled = true;
                    pnexpForm.Visible = false;
                    //LoadIExpenseGridView();
                    //MsgCls("IExpense Created Successfully and Mail sent successfully", lblMessageBoard, Color.Green);
                }

            }


            catch (Exception Ex)
            {

                switch (Ex.Message)
                {


                    case "-05":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Approvals are missing!')", true);
                        MsgCls("Approvals are missing!", lblMessageBoard, Color.Red);

                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        break;
                }

            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                    int? IEXp_ID = 0;
                    decimal ClaimTotal = 0;
                    decimal? CalcReAmt = 0;
                    string reamtcurr = "";

                    OtherReimbursementsbl ExpenseblObjb = new OtherReimbursementsbl();
                    List<OtherReimbursementsbo> IexpboListb = new List<OtherReimbursementsbo>();

                    OtherReimbursementsbl ExpenseblObjb1 = new OtherReimbursementsbl();
                    OtherReimbursementsbo IexpboListb1 = new OtherReimbursementsbo();

                    IexpboListb = ExpenseblObjb.Load_SavedClaimDetails(int.Parse(ViewState["IEXP_ID"].ToString()), ref CalcReAmt, ref reamtcurr);
                    grd_IExpInfo.FooterRow.Cells[4].Text = "Total";

                    grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    grd_IExpInfo.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLCurrency.SelectedValue) + ")";

                    if (IexpboListb == null || IexpboListb.Count == 0)
                    {
                        MsgCls("Please Add Atleast one Expense Item!", lblIndent, Color.Red);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please Add Atleast one Expense Item!');", true);
                    }
                    else
                    {

                        bool? status = false;
                        IexpboListb1.IEXP_ID = int.Parse(ViewState["IEXP_ID"].ToString());
                        IexpboListb1.PROJID = ddlProjectCode.SelectedValue;
                        IexpboListb1.TASK = ddlTask.SelectedValue;
                        IexpboListb1.PURPOSE = txtPurpose.Text.Trim();
                        IexpboListb1.RCURR = DDLCurrency.SelectedValue;
                        IexpboListb1.CREATED_ON = DateTime.Now;
                        IexpboListb1.CREATED_BY = User.Identity.Name;
                        IexpboListb1.APPROVED_ON1 = DateTime.MinValue;
                        IexpboListb1.APPROVED_BY1 = "";
                        IexpboListb1.REMARKS1 = string.Empty;
                        IexpboListb1.APPROVED_ON2 = DateTime.MinValue;
                        IexpboListb1.APPROVED_BY2 = "";
                        IexpboListb1.REMARKS2 = string.Empty;
                        IexpboListb1.APPROVED_ON3 = DateTime.MinValue;
                        IexpboListb1.APPROVED_BY3 = "";
                        IexpboListb1.REMARKS3 = string.Empty;
                        IexpboListb1.APPROVED_ON4 = DateTime.MinValue;
                        IexpboListb1.APPROVED_BY4 = "";
                        IexpboListb1.REMARKS4 = string.Empty;
                        IexpboListb1.APPROVED_ON5 = DateTime.MinValue;
                        IexpboListb1.APPROVED_BY5 = "";
                        IexpboListb1.REMARKS5 = string.Empty;
                        IexpboListb1.APPROVED_ON6 = DateTime.MinValue;
                        IexpboListb1.APPROVED_BY6 = "";
                        IexpboListb1.REMARKS6 = string.Empty;
                        IexpboListb1.APPROVED_ON7 = DateTime.MinValue;
                        IexpboListb1.APPROVED_BY7 = "";
                        IexpboListb1.REMARKS7 = string.Empty;
                        IexpboListb1.APPROVED_ON8 = DateTime.MinValue;
                        IexpboListb1.APPROVED_BY8 = "";
                        IexpboListb1.REMARKS8 = string.Empty;
                        IexpboListb1.APPROVED_ON9 = DateTime.MinValue;
                        IexpboListb1.APPROVED_BY9 = "";
                        IexpboListb1.REMARKS9 = string.Empty;
                        IexpboListb1.STATUS = "AgainSaved";
                        IexpboListb1.TotalAmount = decimal.Parse(CalcReAmt.ToString()); //+ ClaimTotal;// ClaimTotal + Dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                        ExpenseblObjb1.Save_IExpense(IexpboListb1, ref IEXp_ID, ref status);

                        int? refIEXp_ID = IEXp_ID != 0 ? IEXp_ID : Convert.ToInt32(ViewState["IEXP_ID"].ToString().Trim());

                        ////ltiExpID.Text = IEXp_ID.ToString();

                        ltiExpID.Text = refIEXp_ID.ToString();

                        if (status == true)
                        {
                            ////LoadIExpenseGridView();
                            ////MsgCls("Expense Saved Successfully", lblMessageBoard, Color.Green);
                            //////New added Starts
                            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Expense Saved Successfully');", true);
                            MsgCls("Expense ID " + refIEXp_ID + " Saved Successfully", lblMessageBoard, Color.Green);
                            //New added Starts



                            string alert = "iExpense ID " + refIEXp_ID + " Saved Successfully!";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='iExpense_Request.aspx';", true);

                            lblMessageBoard.Text = "Expense Saved Successfully";
                            lblMessageBoard.ForeColor = Color.Green;
                            //New added Ends
                        }
                        ddlTask.SelectedValue = "0";
                        DDLCurrency.SelectedValue = "0";
                        Clear();
                        ClearIExpense();
                        DDLCurrency.Enabled = true;
                        DDLCurrency.SelectedValue = "0";
                        ddlTask.Enabled = true;
                        ddlTask.SelectedValue = "0";
                        ddlProjectCode.Enabled = true;
                        ddlProjectCode.SelectedValue = "0";
                        txtPurpose.Enabled = true;
                        pnexpForm.Visible = false;
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

                strSubject = "IExpense Request " + IEXp_ID + " has been Raised by " + EMP_Name + "  |  " + User.Identity.Name + " is pending for the Approval.";

                RecipientsString = Approver_Email;
                strPernr_Mail = EMP_Email;

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>IExpense Request " + IEXp_ID + " has been Raised by " + EMP_Name + "  |  " + User.Identity.Name + " is pending for the Approval.<br/><br/></b>";
                body += "<b>Entity with IExpense ID  :  " + Entity + " : " + IEXp_ID + "</b><br/><br/>";
                body += "<b>IExpense Header Details :<hr /><br/>";
                body += "Project  :  " + Project + "<br/>";
                body += "Task     :  " + Task + "<br/>";
                body += "Purpose  :  " + Purpose + "<br/>";
                body += "Total Reimbursement Amount  :   " + Samnt.ToString("#,##0.00") + " ( " + ReCurrency + " ) <br/> <br/>";
                //body += "Reimbursement Currency  :  " + ReCurrency + "<br/><br/>";
                body += "<b>IExpense Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "IExpense created successfully and Mail sent successfully.";
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "IExpense created successfully. Error in sending mail";
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


                    EXP_TYPE = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXP_TYPE"].ToString().Trim();
                    S_DATE = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["S_DATE"].ToString().Trim();
                    EXPT_AMT = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPT_AMT"].ToString().Trim();
                    EXPT_CURR = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPT_CURR"].ToString().Trim();
                    EXC_RATE = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXC_RATE"].ToString().Trim();
                    RE_AMT = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RE_AMT"].ToString().Trim();
                    RCURR = DDLCurrency.SelectedValue;
                    JUSTIFY = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["JUSTIFY"].ToString().Trim();
                    RECEIPT_FILE = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FILE"].ToString().Trim();
                    NO_DAYS = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["NO_DAYS"].ToString().Trim();

                    RECEIPT_FIID = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FID"].ToString().Trim();
                    ViewState["Receiptfileid"] = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FID"].ToString().Trim();
                    RECEIPT_FPATH = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FPATH"].ToString().Trim();
                    ViewState["Receiptfilepath"] = grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FPATH"].ToString().Trim();

                    ddlExpenseType.SelectedValue = EXP_TYPE;
                    txtStartDate.Text = (DateTime.Parse(S_DATE)).ToString();
                    txtExpenditureAmount.Text = EXPT_AMT;
                    ddlExpenditureCurrency.SelectedValue = EXPT_CURR;
                    txtExchangeRate.Text = EXC_RATE;
                    lblReimbursableAmount.Text = Convert.ToDecimal(RE_AMT).ToString("#,##0.00");//decimal.Parse(RE_AMT).ToString("0.000");
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

                    bool? statusf = true;
                    decimal? CalcReAmtf = 0;
                    string reamtcurrf = "";
                    int IEXPIDDeleteFile = int.Parse(ViewState["IEXP_ID"].ToString());
                    int IDDeletefile = int.Parse(grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());

                    OtherReimbursementsbl ExpenseblObjf = new OtherReimbursementsbl();
                    OtherReimbursementsbo IexpboListf = new OtherReimbursementsbo();
                    IexpboListf.IEXP_ID = IEXPIDDeleteFile;
                    IexpboListf.ID = IDDeletefile;


                    ExpenseblObjf.DeleteFileFromSaveIexp(IexpboListf, ref statusf);
                    if (statusf == false)
                    {
                        MsgCls("File deleted successfully !", lblIndent, Color.Green);
                    }
                    OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                    List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                    IexpboList = ExpenseblObj.Load_SavedClaimDetails(IEXPIDDeleteFile, ref CalcReAmtf, ref reamtcurrf);
                    grd_IExpInfo.DataSource = IexpboList;
                    grd_IExpInfo.DataBind();

                    DDLCurrency.Enabled = false;
                    ddlTask.Enabled = false;

                    ddlProjectCode.Enabled = false;
                    txtPurpose.Enabled = false;

                    Clear();
                    grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";

                    grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    // GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmtf + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";

                    grd_IExpInfo.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmtf.ToString()).ToString("#,##0.00") + "(" + (DDLCurrency.SelectedValue) + ")";
                    //TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CIDDelete);
                    //GV_TravelExpReq.DataSource = TrvlReqboList;
                    //GV_TravelExpReq.DataBind();
                    //int totalRowsCount = GV_TravelExpReq.Rows.Count;
                    //ViewState["totalRowsCount"] = totalRowsCount;

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

                    try
                    {
                        bool? status = true;
                        decimal? CalcReAmt = 0;
                        string reamtcurr = "";
                        int ExpIDDelete = int.Parse(ViewState["IEXP_ID"].ToString());
                        int IDdelete = int.Parse(grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());

                        OtherReimbursementsbl ExpenseblObjd = new OtherReimbursementsbl();
                        OtherReimbursementsbo IexpboListd = new OtherReimbursementsbo();

                        IexpboListd.IEXP_ID = ExpIDDelete;
                        IexpboListd.ID = IDdelete;


                        ExpenseblObjd.DeleteSaveIexp(IexpboListd, ref status);
                        if (status == false)
                        {
                            MsgCls("Expense deleted successfully !", lblIndent, Color.Green);
                        }
                        OtherReimbursementsbl ExpenseblObjl = new OtherReimbursementsbl();
                        List<OtherReimbursementsbo> IexpboListl = new List<OtherReimbursementsbo>();


                        IexpboListl = ExpenseblObjl.Load_SavedClaimDetails(ExpIDDelete, ref CalcReAmt, ref reamtcurr);
                        grd_IExpInfo.DataSource = IexpboListl;
                        grd_IExpInfo.DataBind();
                        if (IexpboListl == null || IexpboListl.Count == 0)
                        {
                            LoadReimCurrencyTypes();
                            DDLCurrency.Enabled = true;
                            ddlTask.Enabled = true;
                            ddlProjectCode.Enabled = true;
                            txtPurpose.Enabled = true;
                            Clear();
                            //grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";

                            //grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            //// GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmt + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                            //grd_IExpInfo.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLCurrency.SelectedValue) + ")";
                            ////TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CIDDelete);
                            ////GV_TravelExpReq.DataSource = TrvlReqboList;
                            ////GV_TravelExpReq.DataBind();
                            int totalRowsCount = grd_IExpInfo.Rows.Count;
                            ViewState["totalRowsCount"] = totalRowsCount;
                            ddlExpenseType.Focus();
                        }
                        else
                        {
                            DDLCurrency.Enabled = false;
                            ddlTask.Enabled = false;

                            ddlProjectCode.Enabled = false;
                            txtPurpose.Enabled = false;

                            Clear();
                            grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";

                            grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            // GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmt + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                            grd_IExpInfo.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLCurrency.SelectedValue) + ")";
                            //TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CIDDelete);
                            //GV_TravelExpReq.DataSource = TrvlReqboList;
                            //GV_TravelExpReq.DataBind();
                            int totalRowsCount = grd_IExpInfo.Rows.Count;
                            ViewState["totalRowsCount"] = totalRowsCount;
                            ddlExpenseType.Focus();
                        }

                        //////// DDLCurrency.Enabled = false;
                        //////// Clear();
                        //////// grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";

                        //////// grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        ////////// GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmt + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                        //////// grd_IExpInfo.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLCurrency.SelectedValue) + ")";
                        //////// //TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CIDDelete);
                        //////// //GV_TravelExpReq.DataSource = TrvlReqboList;
                        //////// //GV_TravelExpReq.DataBind();
                        //////// int totalRowsCount = grd_IExpInfo.Rows.Count;
                        //////// ViewState["totalRowsCount"] = totalRowsCount;
                        //////// ddlExpenseType.Focus();
                    }
                    catch (Exception Ex)
                    {

                        switch (Ex.Message)
                        {


                            case "-05":
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Line item cannot be deleted since atleast one line item is required')", true);
                                // MsgCls("Line item cannot be deleted since atleast one line item is required", lblMessageBoard, Color.Red);
                                //PageLoadEvents();
                                break;
                            default:
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                                break;
                        }
                        //MsgCls(Ex.Message, LblMsg, Color.Red);
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
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                OtherReimbursementsbo IexpboList = new OtherReimbursementsbo();
                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                DateTime StartDate = new DateTime(0001, 01, 01);
                decimal ExchangeRate = 0.0M;
                decimal? CalcReAmt = 0;
                string reamtcurr = "";
                bool? status = false;
                string fileupdate = string.Empty;
                if (decimal.TryParse(txtExchangeRate.Text, out ExchangeRate))
                { }
                if (DateTime.TryParse(txtStartDate.Text, out StartDate) && ddlTask.SelectedValue != "0")
                {

                    int rowindex = int.Parse(ViewState["RowIndex"].ToString());
                    int lid = int.Parse(ViewState["IDtoAdd"].ToString());

                    IexpboList.IEXP_ID = int.Parse(ViewState["IEXP_ID"].ToString());
                    IexpboList.ID = lid;
                    IexpboList.EXP_TYPE = ddlExpenseType.SelectedValue;
                    IexpboList.S_DATE = StartDate;
                    IexpboList.NO_DAYS = txtNoofDays.Text;

                    IexpboList.EXPT_AMT = txtExpenditureAmount.Text;
                    IexpboList.EXPT_CURR = ddlExpenditureCurrency.SelectedValue;
                    IexpboList.EXC_RATE = txtExchangeRate.Text;
                    IexpboList.RE_AMT = (Math.Abs(ExchangeRate < 0 ? decimal.Parse(txtExpenditureAmount.Text) / ExchangeRate : decimal.Parse(txtExpenditureAmount.Text) * ExchangeRate)).ToString();
                    IexpboList.JUSTIFY = txtJustification.Text;
                    IexpboList.RECEIPT_FILE = cb.Checked ? "YES" : "NO";
                    if (fuAttachments != null)
                    {
                        if (fuAttachments.HasFile)
                        {
                            IexpboList.RECEIPT_FID = fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "";
                            IexpboList.RECEIPT_FPATH = fuAttachments.HasFile ? "~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "";
                            if (fuAttachments.HasFile)
                            { fuAttachments.SaveAs(Server.MapPath("~/IEXPENSEDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName)); }

                            fileupdate = "UpdateFileUpload";
                        }
                    }
                    else
                    {
                        IexpboList.RECEIPT_FID = ViewState["Receiptfileid"].ToString();
                        IexpboList.RECEIPT_FPATH = ViewState["Receiptfilepath"].ToString();
                        fileupdate = "UpdateNoFileUpload";
                    }
                    ExpenseblObj.UpdateSavedReject_ExpItems(IexpboList, ref status, fileupdate);
                    if (status == true)
                    {
                        MsgCls("Expense updated successfully !", lblIndent, Color.Green);
                        grd_IExpInfo.Columns[10].Visible = true;
                    }
                    OtherReimbursementsbl ExpenseblObj1 = new OtherReimbursementsbl();

                    List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();
                    IexpboList1 = ExpenseblObj1.Load_SavedClaimDetails(int.Parse(ViewState["IEXP_ID"].ToString()), ref CalcReAmt, ref reamtcurr);
                    grd_IExpInfo.DataSource = IexpboList1;
                    grd_IExpInfo.DataBind();

                    DDLCurrency.Enabled = false;
                    ddlTask.Enabled = false;

                    ddlProjectCode.Enabled = false;
                    txtPurpose.Enabled = false;
                    ClearAfterAdd();
                    //Clear();

                    grd_IExpInfo.FooterRow.Cells[4].Text = "Total : ";

                    grd_IExpInfo.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    grd_IExpInfo.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLCurrency.SelectedValue) + ")";
                    ClearAfterAdd();



                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Invalid Data');", true);
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
            GetExchangeRate();
            txtJustification.Focus();
        }

        protected void cb_CheckedChanged(object sender, EventArgs e)
        {
            if (cb.Checked)
            {
                fuAttachments.Enabled = false;
                fuAttachmentsfname.Enabled = false;
                cb.Focus();
            }
            else
            {
                fuAttachments.Enabled = true;
                fuAttachmentsfname.Enabled = true;
                cb.Focus();
            }
        }

        protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadExpenseType(ddlTask.SelectedValue.ToString());
            DDLCurrency.Focus();
        }



        protected void btnclear_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            LoadIExpenseGridView();
            pnexpForm.Visible = false;

            MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                searchdetails();
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, System.Drawing.Color.Red);
            }

        }
        public void searchdetails()
        {

            try
            {
                MsgCls(string.Empty, lblMessageBoard, System.Drawing.Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;


                if (SelectedType != "0" && textSearch == "")
                {
                    MsgCls("Please Enter the Text", lblMessageBoard, System.Drawing.Color.Red);
                }

                else if (SelectedType == "0" && textSearch != "")
                {
                    MsgCls("Please Select the Type", lblMessageBoard, System.Drawing.Color.Red);
                }
                else
                {


                    OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                    List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                    IexpboList = ExpenseblObj.Load_ParticularIexpDetailsforSaved(User.Identity.Name, SelectedType, textSearch);
                    if (IexpboList == null || IexpboList.Count == 0)
                    {
                        MsgCls("No Records found", lblMessageBoard, System.Drawing.Color.Red);
                        grdIexpdetails.Visible = false;
                        grdIexpdetails.DataSource = null;
                        grdIexpdetails.DataBind();
                        pnexpForm.Visible = false;

                        return;
                    }
                    else
                    {
                        MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
                        grdIexpdetails.Visible = true;
                        grdIexpdetails.DataSource = IexpboList;
                        grdIexpdetails.SelectedIndex = -1;
                        grdIexpdetails.DataBind();
                        //GV_TravelClaimReqAppRej.Visible = false;
                        //grdAppRejTravel.Visible = false;
                        //Panel1.Visible = false;
                        pnexpForm.Visible = false;

                    }

                }

            }

            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                //  MsgCls(Ex.Message, lblMessageBoard, System.Drawing.Color.Red);
                MsgCls("Please enter valid data", lblMessageBoard, System.Drawing.Color.Red);
            }
        }

        protected void ddlExpenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlExpenditureCurrency.Focus();
        }

        protected void cbEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEdit.Checked)
                txtExchangeRate1.Enabled = true;
            else
                txtExchangeRate1.Enabled = false;
        }
    }
}
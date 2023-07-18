using AjaxControlToolkit;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Text;
using System.ComponentModel;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class TravelClaimReq1 : System.Web.UI.Page
    {
        WebService.Service ServicesObj = new WebService.Service();
        protected MembershipUser memUser;
        //protected override void OnPreRender(EventArgs e)
        //{
        //    // add base.OnPreRender(e); at the beginning of the method.
        //    base.OnPreRender(e);
        //    // codes to handle with your controls.
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    PageLoadEvents();
                    LoadReimCurrencyTypes();
                    LoadExpenditureCurrency();
                    LoadCountry();
                    divSearch.Visible = true;



                    if (Request.QueryString["NC"] != null)
                    {
                        if (Request.QueryString["NC"] == "T")
                        {
                            if (Session["REINR"] != null)
                            {
                                ViewTC(Session["REINR"].ToString());
                                goto displayInfo;
                            }
                        }
                        else if (Request.QueryString["NC"] == "C")
                        {
                            if (Session["CID"] != null)
                            {
                                CopyTC(Session["CID"].ToString(), Session["REINR"].ToString());
                                goto displayInfo;
                            }
                        }
                        else if (Request.QueryString["NC"] == "N")
                        {
                            Session["REINR"] = null;
                            Session.Clear();
                        }
                    }

                }
                this.Page.Form.Enctype = "multipart/form-data";


                icollapse.Attributes.Add("class", cpe.ClientState == "true" ? "mdi mdi-plus font-20 text-white" : "mdi mdi-minus font-20 text-white");


                Loadfileupload();
            displayInfo:
                {
                    ////Console.WriteLine("");
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }


        void ViewTC(string REINR)
        {
            try
            {
                DateTime Fromdate1;
                DateTime Todate1;
                //int rowIndex = Convert.ToInt32(e.CommandArgument);

                //foreach (GridViewRow row in GV_TravelReqUpdate.Rows)
                //{
                //    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                //    System.Drawing.Color.LightGray :
                //    System.Drawing.Color.White;
                //}
                //MsgCls(string.Empty, LblMsg, Color.Transparent);
                ViewState["@REINR"] = REINR;//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();




                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                TrvlReqboList = travelrequestblObj.Get_Traveldetails(REINR);

                ViewState["@TripType"] = TrvlReqboList[0].KZREA == null ? "" : TrvlReqboList[0].KZREA.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["KZREA"].ToString();

                if (ViewState["@TripType"].ToString() == "Domestic")
                {
                    string schema = "01";
                    LoadDDLExpenseType(schema, User.Identity.Name);
                }
                else
                {
                    string schema = "02";
                    LoadDDLExpenseType(schema, User.Identity.Name);
                }
                ViewState["From"] = TrvlReqboList[0].KUNDE == null ? "" : TrvlReqboList[0].KZREA.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["KUNDE"].ToString();
                ViewState["To"] = TrvlReqboList[0].ZORT1 == null ? "" : TrvlReqboList[0].ZORT1.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZORT1"].ToString();
                ViewState["Country"] = TrvlReqboList[0].ZLAND == null ? "" : TrvlReqboList[0].ZLAND.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZLAND"].ToString();
                ViewState["Project"] = TrvlReqboList[0].WBS_ELEMT == null ? "" : TrvlReqboList[0].WBS_ELEMT.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();
                ViewState["TotalAdvance"] = TrvlReqboList[0].SUM_ADVANC == null ? "" : TrvlReqboList[0].SUM_ADVANC.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["SUM_ADVANC"].ToString();
                ViewState["FrmDate"] = TrvlReqboList[0].DATV1 == null ? "" : TrvlReqboList[0].DATV1.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString();
                ViewState["ToDate"] = TrvlReqboList[0].DATB1 == null ? "" : TrvlReqboList[0].DATB1.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString();
                ViewState["Currency"] = TrvlReqboList[0].CURRENCY == null ? "" : TrvlReqboList[0].CURRENCY.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["CURRENCY"].ToString();
                ViewState["AdditonalAdvance"] = TrvlReqboList[0].ADDIT_AMNT == null ? "" : TrvlReqboList[0].ADDIT_AMNT.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ADDIT_AMNT"].ToString();

                Fromdate1 = DateTime.Parse(TrvlReqboList[0].DATV1 == null ? "" : TrvlReqboList[0].DATV1.ToString().Trim());//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString());
                Todate1 = DateTime.Parse(TrvlReqboList[0].DATB1 == null ? "" : TrvlReqboList[0].DATB1.ToString().Trim());//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString());

                ////lttripdates.Text = TrvlReqboList[0].DATV1.ToString() + "-" + TrvlReqboList[0].DATB1.ToString();
                DateTime d1 = Convert.ToDateTime(TrvlReqboList[0].DATV1.ToString());
                DateTime d2 = Convert.ToDateTime(TrvlReqboList[0].DATB1.ToString());
                lttripdates.Text = d1.Date.ToString("dd/MM/yyyy") + "-" + d2.Date.ToString("dd/MM/yyyy");

                //double cpount=(Todate1 - Fromdate1).TotalDays;
                //int cpount2 = (Todate1 - Fromdate1).Days;
                TimeSpan ts = Todate1 - Fromdate1;
                int days = ts.Days;
                ViewState["Dayscount"] = days;

                ddlTask.SelectedValue = "0";
                DDLReimbursementCurrency.SelectedValue = "0";
                ClearClaimLineItemssubmit();
                //CCD_DDLReimbursementCurrency.SelectedValue = "0";
                ViewState["TR_CLAIM"] = null;
                DateTime FrmDt = new DateTime();
                DateTime ToDt = new DateTime();
                //if (DateTime.TryParse(GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString(), out FrmDt)
                //    && DateTime.TryParse(GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString(), out ToDt))
                if (DateTime.TryParse(TrvlReqboList[0].DATV1 == null ? "" : TrvlReqboList[0].DATV1.ToString().Trim(), out FrmDt)
                   && DateTime.TryParse(TrvlReqboList[0].DATB1 == null ? "" : TrvlReqboList[0].DATB1.ToString().Trim(), out ToDt))
                {
                    //if (GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString().Contains(':'))
                    if (TrvlReqboList[0].WBS_ELEMT.ToString().Contains(':'))
                    {
                        HF_REINR.Value = null;
                        //HF_REINR.Value = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString()
                        //    + "&" + GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString().Split(':')[0];
                        HF_REINR.Value = REINR
                       + "&" + TrvlReqboList[0].WBS_ELEMT.ToString().Split(':')[0];
                        PnlExpenseAdd.Visible = true;
                        divSearch.Visible = false;
                        RGV_txtStartDate.MaximumValue = ToDt.AddDays(1).ToString("dd/MM/yyyy");
                        RGV_txtStartDate.MinimumValue = FrmDt.AddDays(-1).ToString("dd/MM/yyyy");
                        RGV_txtStartDate.Text = "Expense date should be within " + FrmDt.AddDays(-1).ToString("dd/MM/yyyy") + " & " + ToDt.AddDays(1).ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        MsgCls("Doesn't have project to view !", LblMsg, Color.Red);
                        PnlExpenseAdd.Visible = false;
                    }
                }
                //// New Code Starts
                string project = ViewState["Project"].ToString();
                string pcode = project.Substring(0, project.LastIndexOf(" :") + 1);


                ////checkProject(User.Identity.Name, pcode.Trim());
                OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                objcontext.CommandTimeout = 0;////Timeout
                int iReturnValue = objcontext.usp_project_validation(User.Identity.Name, pcode.Trim());
                if (iReturnValue != 0)
                {
                    return;
                }
                else
                {
                    PnlExpenseAdd.Visible = true;
                    MsgCls("", LblMsg, Color.Transparent);
                }

                objcontext.Dispose();

                //// New Code Ends


                ////MsgCls("", LblMsg, Color.Transparent);
                ddlTask.Focus();
            }

            catch (Exception Ex)
            {

                ////MsgCls(Ex.Message, LblMsg, Color.Red);

                StringBuilder B = new StringBuilder();
                switch (Ex.Message)
                {
                    case "-01":
                        MsgCls("You're selecting a wrong Entity Internal Project code !", LblMsg, Color.Red);
                        B.Append("<ul class=\"LiNone\">");
                        B.Append("<li>You're selecting a wrong Entity Internal Project code !</li>");
                        B.Append("</ul>");
                        LblMsg.Text = B.ToString();
                        PnlExpenseAdd.Visible = false;
                        divSearch.Visible = true;
                        break;
                    case "-02":
                        MsgCls("This project code internal number is not maintained ! Please contact project team", LblMsg, Color.Red);
                        B.Append("<ul class=\"LiNone\">");
                        B.Append("<li>This project code internal number is not maintained ! Please contact project team</li>");
                        B.Append("</ul>");
                        LblMsg.Text = B.ToString();
                        PnlExpenseAdd.Visible = false;
                        divSearch.Visible = true;
                        break;
                    case "-03":
                        MsgCls("This project code internal number is not released !  Please contact project team", LblMsg, Color.Red);
                        B.Append("<ul class=\"LiNone\">");
                        B.Append("<li>This project code internal number is not released ! Please contact project team</li>");
                        B.Append("</ul>");
                        LblMsg.Text = B.ToString();
                        PnlExpenseAdd.Visible = false;
                        divSearch.Visible = true;
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true);
                        MsgCls(Ex.Message, LblMsg, Color.Red);
                        PnlExpenseAdd.Visible = false;
                        divSearch.Visible = true;
                        break;
                }
            }
        }

        void CopyTC(string CID1, string REINR)
        {
            travelrequestbl travelrequestblObj = new travelrequestbl();
            List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();


            int CID = int.Parse(CID1);
            TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CID);



            /*----------------------Trip Details*/
            //DateTime Fromdate1;
            //DateTime Todate1;

            //ViewState["@REINR"] = TrvlReqboList[0].REINR == null ? "" : TrvlReqboList[0].REINR.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
            //ViewState["@TripType"] = TrvlReqboList[0].KZREA == null ? "" : TrvlReqboList[0].KZREA.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["KZREA"].ToString();
            //ViewState["FrmtripDate"] = TrvlReqboList[0].DATV1 == null ? "" : TrvlReqboList[0].DATV1.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString();
            //ViewState["TotripDate"] = TrvlReqboList[0].DATB1 == null ? "" : TrvlReqboList[0].DATB1.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString();

            //Fromdate1 = DateTime.Parse(ViewState["FrmtripDate"].ToString());//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString());
            //Todate1 = DateTime.Parse(ViewState["TotripDate"].ToString());//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString());

            //lttripdates.Text = TrvlReqboList[0].DATV1.ToString() + "-" + TrvlReqboList[0].DATB1.ToString();

            //TimeSpan ts = Todate1 - Fromdate1;
            //int days = ts.Days;
            //ViewState["Dayscount"] = days;


            /*----------------------*/


            /*----------------------*/
            ViewState["@REINR"] = REINR;//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();




            travelrequestbl travelrequestblObj1 = new travelrequestbl();
            List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();
            TrvlReqboList1 = travelrequestblObj.Get_Traveldetails(REINR);


            DateTime Fromdate1;
            DateTime Todate1;
            ////int rowIndex = Convert.ToInt32(e.CommandArgument);

            ////foreach (GridViewRow row in GV_TravelReqUpdate.Rows)
            ////{
            ////    row.BackColor = row.RowIndex.Equals(rowIndex) ?
            ////    System.Drawing.Color.LightGray :
            ////    System.Drawing.Color.White;
            ////}
            ////MsgCls(string.Empty, LblMsg, Color.Transparent);
            ViewState["@REINR"] = TrvlReqboList1[0].REINR == null ? "" : TrvlReqboList1[0].REINR.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
            ViewState["@TripType"] = TrvlReqboList1[0].KZREA == null ? "" : TrvlReqboList1[0].KZREA.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["KZREA"].ToString();
            ViewState["FrmtripDate"] = TrvlReqboList1[0].DATV1 == null ? "" : TrvlReqboList1[0].DATV1.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString();
            ViewState["TotripDate"] = TrvlReqboList1[0].DATB1 == null ? "" : TrvlReqboList1[0].DATB1.ToString().Trim();//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString();

            Fromdate1 = DateTime.Parse(ViewState["FrmtripDate"].ToString());//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString());
            Todate1 = DateTime.Parse(ViewState["TotripDate"].ToString());//GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString());

            //lttripdates.Text = TrvlReqboList1[0].DATV1.ToString() + "-" + TrvlReqboList1[0].DATB1.ToString();
            DateTime d1 = Convert.ToDateTime(TrvlReqboList1[0].DATV1.ToString());
            DateTime d2 = Convert.ToDateTime(TrvlReqboList1[0].DATB1.ToString());
            lttripdates.Text = d1.Date.ToString("dd/MM/yyyy") + "-" + d2.Date.ToString("dd/MM/yyyy");

            TimeSpan ts = Todate1 - Fromdate1;
            int days = ts.Days;
            ViewState["Dayscount"] = days;




            /*----------------------*/






            if (ViewState["@TripType"].ToString() == "Domestic")
            {
                string schema = "01";
                LoadDDLExpenseType(schema, User.Identity.Name);
            }
            else
            {
                string schema = "02";
                LoadDDLExpenseType(schema, User.Identity.Name);
            }
            //string Task = TrvlReqboList1[0].ACTIVITY == null ? "" : TrvlReqboList1[0].ACTIVITY.ToString().Trim();//grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["ACTIVITY"].ToString();
            //if (Task == "Billable")
            //{
            //    Task = "B";
            //}
            //else
            //{
            //    Task = "NB";
            //}

            string Task = TrvlReqboList[0].TASK;


            ddlTask.SelectedValue = Task;
            string Rccur = TrvlReqboList[0].RCURR == null ? "" : TrvlReqboList[0].RCURR.ToString().Trim();//grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();

            DDLReimbursementCurrency.SelectedValue = Rccur;//grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();
            //int CID = int.Parse(CID1);//int.Parse(grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
            ViewState["CIDforSubmit"] = CID;
            //travelrequestbl travelrequestblObj = new travelrequestbl();
            //List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();



            //TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CID);
            GV_TravelExpReq.DataSource = TrvlReqboList;
            GV_TravelExpReq.DataBind();

            //DataTable dt = ConvertToDataTable(TrvlReqboList);
            //decimal dout = 0;
            //decimal total = dt.AsEnumerable()
            //         .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out dout)).Sum(r => dout);
            //GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";


            //GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //GV_TravelExpReq.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (Rccur) + ")";
            //int totalRowsCount = GV_TravelExpReq.Rows.Count;
            //ViewState["totalRowsCount"] = totalRowsCount;



            ViewState["TripNO"] = TrvlReqboList1[0].REINR == null ? "" : TrvlReqboList1[0].REINR.ToString().Trim();//grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
            ViewState["PROJID"] = TrvlReqboList1[0].PRJID == null ? "" : TrvlReqboList1[0].PRJID.ToString().Trim();//grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRJID"].ToString();
            ViewState["WBS_ELEMT"] = TrvlReqboList1[0].WBS_ELEMT == null ? "" : TrvlReqboList1[0].WBS_ELEMT.ToString().Trim();//grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();

            ////ltClaimID.Text = CID.ToString();
            ltTask.Text = ddlTask.SelectedItem.Text;
            //ltProject.Text = ddlProjectCode.SelectedItem.Text;
            ltReimbAmt.Text = GV_TravelExpReq.FooterRow.Cells[5].Text;


            MsgCls("", lblIndent, Color.Transparent);
            DDLReimbursementCurrency.Enabled = false;
            ddlTask.Enabled = false;

            divSearch.Visible = false;

            //--------------------------------------------------------------
            string currency = DDLReimbursementCurrency.SelectedValue.ToString();
            //string date1;
            //DateTime StartDate = new DateTime(0001, 01, 01);
            string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
            DateTime StartDate = new DateTime(0001, 01, 01);
            decimal ExchangeRate = 0.0M;
            ////string DailyRate = ServicesObj.GetExcRate(DDLTrClaimCountry.SelectedValue, DDLRegion.SelectedValue, DDLExpenseType.SelectedValue, Convert.ToDateTime(txtStartDate.Text));
            //if (decimal.TryParse(ServicesObj.GetExchangeRate(DDLExpenditureCurrency.SelectedValue, DDLReimbursementCurrency.SelectedValue), out ExchangeRate))
            //{ }
            if (decimal.TryParse(txtExchangeRate.Text.Trim(), out ExchangeRate))
            { }
            MsgCls(string.Empty, LblMsg, Color.Transparent);

            ViewState["TR_CLAIM"] = null;
            int listid = 1;



            if (DDLReimbursementCurrency.SelectedValue != "0" && DDLReimbursementCurrency.SelectedValue != "")
            {
                if (ddlTask.SelectedValue != "0")
                {
                    //if (DateTime.TryParse(txtStartDate.Text, out StartDate))
                    //{
                    if (GV_TravelExpReq.Rows.Count > 0)
                    {
                        BtnSave.Visible = true;
                        BtnSubmit.Visible = true;

                        foreach (GridViewRow row in GV_TravelExpReq.Rows)
                        {
                            //using (DataTable Dt = ViewState["TR_CLAIM"] != null ? (DataTable)ViewState["TR_CLAIM"] : GetTrvlClaim())
                            if (ViewState["TR_CLAIM"] != null)
                            {
                                using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                                {
                                    try
                                    {
                                        //if (DDLTrClaimCountry.SelectedValue != "0")
                                        //{
                                        //    if (((DDLTrClaimCountry.SelectedValue.Trim() == "IN") && (DDLRegion.SelectedValue != "" && DDLRegion.SelectedValue != "0")) || (DDLTrClaimCountry.SelectedValue.Trim() != "IN"))
                                        //    {
                                        Dt.Rows.Add(Dt.Rows.Count + 1, TrvlReqboList[listid].EXPID.ToString(), TrvlReqboList[listid].EXP_TYPE_NAME.ToString(), TrvlReqboList[listid].S_DATE.ToString(), TrvlReqboList[listid].NO_DAYS.ToString(), TrvlReqboList[listid].DAILY_RATE.ToString()
                                                , TrvlReqboList[listid].EXPT_AMT.ToString(), TrvlReqboList[listid].EXPT_CURR.ToString(), TrvlReqboList[listid].EXC_RATE.ToString(), TrvlReqboList[listid].RE_AMT.ToString()
                                               , TrvlReqboList[listid].JUSTIFY.ToString(), TrvlReqboList[listid].RECEIPT_FILE.ToString(), TrvlReqboList[listid].RECEIPT_FID.ToString(), TrvlReqboList[listid].RECEIPT_FPATH.ToString()
                                               , TrvlReqboList[listid].CountryID.ToString(), TrvlReqboList[listid].RegoinID.ToString(), TrvlReqboList[listid].DEVIATION_AMT.ToString(), TrvlReqboList[listid].DEVIATION_CURR.ToString(), TrvlReqboList[listid].RCURR.ToString(), TrvlReqboList[listid].DAILY_CURR.ToString());

                                        if (fuAttachments.HasFile)
                                        { fuAttachments.SaveAs(Server.MapPath("~/TravelDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName)); }

                                        // DDLReimbursementCurrency.Enabled = false;
                                        DDLReimbursementCurrency.Enabled = false;
                                        ddlTask.Enabled = false;
                                        ////ViewState["TR_CLAIM"] = null;
                                        ////ViewState["TR_CLAIM"] = Dt;
                                        GV_TravelExpReq.DataSource = Dt;
                                        GV_TravelExpReq.DataBind();




                                        ClearClaimLineItems();


                                        listid = listid + 1;


                                        decimal d = 0;
                                        ////decimal total = Dt.AsEnumerable()
                                        //// .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                                        ////string createdby = User.Identity.Name;
                                        ////decimal SettlementAmt = 0;
                                        ////string SettlementCurr = string.Empty;


                                        ////OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                                        ////OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, DDLReimbursementCurrency.SelectedValue);
                                        ////foreach (OtherReimbursementsbo objBo1 in objLst1)
                                        ////{
                                        ////    SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                                        ////    SettlementCurr = objBo1.SETTLECURR.ToString();
                                        ////}

                                        ////GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                                        ////GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                                        ////GV_TravelExpReq.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                                        DDLExpenseType.Focus();

                                        ////MsgCls("Claim Item Added Successfully !", lblMessageBoard, Color.Green);



                                        //---------------------------------
                                        ltTask.Text = ddlTask.SelectedItem.Text;
                                        //ltProject.Text = ddlProjectCode.SelectedItem.Text;
                                        ltReimbAmt.Text = GV_TravelExpReq.FooterRow.Cells[5].Text;
                                        dvlineitem.Visible = true;
                                        PnlExpenseAdd.Visible = true;
                                        ////collapse();

                                        BtnSave.Visible = true;
                                        BtnSubmit.Visible = true;
                                        GV_TravelExpReq.Columns[13].Visible = true;
                                        //    }
                                        //    else
                                        //    {
                                        //        MsgCls("Select Region Name !", LblMsg, Color.Red);
                                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Select Region Name !');", true);
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    MsgCls("Select Country Name !", LblMsg, Color.Red);
                                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Select Country Name !');", true);
                                        //}
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
                                using (DataTable Dt = GetTrvlClaim())
                                {

                                    //if (DDLTrClaimCountry.SelectedValue != "0")
                                    //{
                                    //    if (((DDLTrClaimCountry.SelectedValue.Trim() == "IN") && (DDLRegion.SelectedValue != "" && DDLRegion.SelectedValue != "0")) || (DDLTrClaimCountry.SelectedValue.Trim() != "IN"))
                                    //    {
                                    Dt.Rows.Add(Dt.Rows.Count + 1, TrvlReqboList[0].EXPID.ToString(), TrvlReqboList[0].EXP_TYPE_NAME.ToString(), TrvlReqboList[0].S_DATE.ToString(), TrvlReqboList[0].NO_DAYS.ToString(), TrvlReqboList[0].DAILY_RATE.ToString()
                                            , TrvlReqboList[0].EXPT_AMT.ToString(), TrvlReqboList[0].EXPT_CURR.ToString(), TrvlReqboList[0].EXC_RATE.ToString(), TrvlReqboList[0].RE_AMT.ToString()
                                           , TrvlReqboList[0].JUSTIFY.ToString(), TrvlReqboList[0].RECEIPT_FILE.ToString(), TrvlReqboList[0].RECEIPT_FID.ToString(), TrvlReqboList[0].RECEIPT_FPATH.ToString()
                                           , TrvlReqboList[0].CountryID.ToString(), TrvlReqboList[0].RegoinID.ToString(), TrvlReqboList[0].DEVIATION_AMT.ToString(), TrvlReqboList[0].DEVIATION_CURR.ToString(), TrvlReqboList[0].RCURR.ToString(), TrvlReqboList[0].DAILY_CURR.ToString());

                                    if (fuAttachments.HasFile)
                                    { fuAttachments.SaveAs(Server.MapPath("~/TravelDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName)); }

                                    // DDLReimbursementCurrency.Enabled = false;
                                    DDLReimbursementCurrency.Enabled = false;
                                    ddlTask.Enabled = false;
                                    //ViewState["TR_CLAIM"] = null;
                                    //ViewState["TR_CLAIM"] = Dt;
                                    GV_TravelExpReq.DataSource = Dt;
                                    GV_TravelExpReq.DataBind();

                                    ViewState["TR_CLAIM"] = Dt;

                                    ClearClaimLineItems();


                                    ////listid = listid + 1;


                                    decimal d = 0;
                                    ////decimal total = Dt.AsEnumerable()
                                    //// .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                                    ////string createdby = User.Identity.Name;
                                    ////decimal SettlementAmt = 0;
                                    ////string SettlementCurr = string.Empty;


                                    ////OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                                    ////OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, DDLReimbursementCurrency.SelectedValue);
                                    ////foreach (OtherReimbursementsbo objBo1 in objLst1)
                                    ////{
                                    ////    SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                                    ////    SettlementCurr = objBo1.SETTLECURR.ToString();
                                    ////}

                                    ////GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                                    ////GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                                    ////GV_TravelExpReq.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                                    DDLExpenseType.Focus();

                                    ////MsgCls("Claim Item Added Successfully !", lblMessageBoard, Color.Green);



                                    //---------------------------------
                                    ltTask.Text = ddlTask.SelectedItem.Text;
                                    //ltProject.Text = ddlProjectCode.SelectedItem.Text;
                                    ltReimbAmt.Text = GV_TravelExpReq.FooterRow.Cells[5].Text;
                                    dvlineitem.Visible = true;
                                    PnlExpenseAdd.Visible = true;
                                    ////collapse();

                                    BtnSave.Visible = true;
                                    BtnSubmit.Visible = true;
                                    GV_TravelExpReq.Columns[13].Visible = true;
                                    //    }
                                    //    else
                                    //    {
                                    //        MsgCls("Select Region Name !", LblMsg, Color.Red);
                                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Select Region Name !');", true);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    MsgCls("Select Country Name !", LblMsg, Color.Red);
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Select Country Name !');", true);
                                    //}
                                }
                            }
                        }
                    }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Invalid Date');", true);
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please select Task');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please select Reimbursement Currency');", true);
                //dvlialert.Visible = true;
            }
            //--------------------------------------------------------------
            DataTable dt = ConvertToDataTable(TrvlReqboList);
            decimal dout = 0;
            decimal total = dt.AsEnumerable()
                     .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out dout)).Sum(r => dout);
            GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";
            GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            GV_TravelExpReq.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            GV_TravelExpReq.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (Rccur) + ")";
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


        #region User Defined Method
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
        #endregion

        private void PageLoadEvents()
        {
            try
            {
                travelrequestcolumnsbo TrvlBO = new travelrequestcolumnsbo();
                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                TrvlReqboList = travelrequestblObj.Get_TravelReqDetails_forClaim(User.Identity.Name);
                GV_TravelReqUpdate.DataSource = TrvlReqboList;
                GV_TravelReqUpdate.DataBind();


                if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                    GV_TravelReqUpdate.Visible = false;
                    GV_TravelReqUpdate.DataSource = null;
                    //lblRemarks.Visible = false;
                    //TxtRemarks.Visible = false;
                    //btnApprove.Visible = false;
                    //btnReject.Visible = false;
                    return;
                }
                else
                {
                    GV_TravelReqUpdate.Visible = true;
                    GV_TravelReqUpdate.DataSource = TrvlReqboList;
                    // grdAppRejTravel.SelectedIndex = -1;
                    //lblRemarks.Visible = true;
                    //TxtRemarks.Visible = true;
                    //btnApprove.Visible = true;
                    //btnReject.Visible = true;
                }
                GV_TravelReqUpdate.DataBind();

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        private void LoadReimCurrencyTypes()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Payment_Currency();
                DDLReimbursementCurrency.DataSource = objLst;
                DDLReimbursementCurrency.DataTextField = "WARESTXT";
                DDLReimbursementCurrency.DataValueField = "WAERS";
                DDLReimbursementCurrency.DataBind();
                DDLReimbursementCurrency.Items.Insert(0, new ListItem("-SEARCH-", "0"));
                DDLReimbursementCurrency.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }


        }

        private void LoadExpenditureCurrency()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Payment_Currency();
                DDLExpenditureCurrency.DataSource = objLst;
                DDLExpenditureCurrency.DataTextField = "WARESTXT";
                DDLExpenditureCurrency.DataValueField = "WAERS";
                DDLExpenditureCurrency.DataBind();
                DDLExpenditureCurrency.Items.Insert(0, new ListItem("-SEARCH-", "0"));
                DDLExpenditureCurrency.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        private void LoadCountry()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Country();
                DDLTrClaimCountry.DataSource = objLst;
                DDLTrClaimCountry.DataTextField = "LANDX";
                DDLTrClaimCountry.DataValueField = "LAND1";
                DDLTrClaimCountry.DataBind();
                DDLTrClaimCountry.Items.Insert(0, new ListItem("-SEARCH-", "0"));
                DDLTrClaimCountry.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }


        private void LoadRegion(string countryid)
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Region(countryid);
                DDLRegion.DataSource = objLst;
                DDLRegion.DataTextField = "TEXT25";
                DDLRegion.DataValueField = "RGION";
                DDLRegion.DataBind();
                DDLRegion.Items.Insert(0, new ListItem("-SEARCH-", "0"));
                DDLRegion.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        protected void GV_TravelReqUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
                        //ClearClaimLineItems();
                        DateTime Fromdate1;
                        DateTime Todate1;
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in GV_TravelReqUpdate.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }
                        MsgCls(string.Empty, LblMsg, Color.Transparent);
                        ViewState["@REINR"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                        ViewState["@TripType"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["KZREA"].ToString();

                        if (ViewState["@TripType"].ToString() == "Domestic")
                        {
                            string schema = "01";
                            LoadDDLExpenseType(schema, User.Identity.Name);
                        }
                        else
                        {
                            string schema = "02";
                            LoadDDLExpenseType(schema, User.Identity.Name);
                        }
                        ViewState["From"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["KUNDE"].ToString();
                        ViewState["To"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZORT1"].ToString();
                        ViewState["Country"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZLAND"].ToString();
                        ViewState["Project"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();
                        ViewState["TotalAdvance"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["SUM_ADVANC"].ToString();
                        ViewState["FrmDate"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString();
                        ViewState["ToDate"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString();
                        ViewState["Currency"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["CURRENCY"].ToString();
                        ViewState["AdditonalAdvance"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ADDIT_AMNT"].ToString();

                        Fromdate1 = DateTime.Parse(GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString());
                        Todate1 = DateTime.Parse(GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString());

                        //double cpount=(Todate1 - Fromdate1).TotalDays;
                        //int cpount2 = (Todate1 - Fromdate1).Days;
                        TimeSpan ts = Todate1 - Fromdate1;
                        int days = ts.Days;
                        ViewState["Dayscount"] = days;

                        ddlTask.SelectedValue = "0";
                        DDLReimbursementCurrency.SelectedValue = "0";
                        ClearClaimLineItemssubmit();
                        //CCD_DDLReimbursementCurrency.SelectedValue = "0";
                        ViewState["TR_CLAIM"] = null;
                        DateTime FrmDt = new DateTime();
                        DateTime ToDt = new DateTime();
                        if (DateTime.TryParse(GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString(), out FrmDt)
                            && DateTime.TryParse(GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString(), out ToDt))
                        {
                            if (GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString().Contains(':'))
                            {
                                HF_REINR.Value = null;
                                HF_REINR.Value = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString()
                                    + "&" + GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString().Split(':')[0];
                                PnlExpenseAdd.Visible = true;
                                divSearch.Visible = false;
                                RGV_txtStartDate.MaximumValue = ToDt.AddDays(1).ToString("dd/MM/yyyy");
                                RGV_txtStartDate.MinimumValue = FrmDt.AddDays(-1).ToString("dd/MM/yyyy");
                                RGV_txtStartDate.Text = "Expense date should be within " + FrmDt.AddDays(-1).ToString("dd/MM/yyyy") + " & " + ToDt.AddDays(1).ToString("dd/MM/yyyy");

                            }
                            else
                            {
                                MsgCls("Doesn't have project to view !", LblMsg, Color.Red);
                                PnlExpenseAdd.Visible = false;
                            }
                        }
                        //// New Code Starts
                        string project = ViewState["Project"].ToString();
                        string pcode = project.Substring(0, project.LastIndexOf(" :") + 1);


                        ////checkProject(User.Identity.Name, pcode.Trim());
                        OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();
                        objcontext.CommandTimeout = 0;////Timeout
                        int iReturnValue = objcontext.usp_project_validation(User.Identity.Name, pcode.Trim());
                        if (iReturnValue != 0)
                        {
                            return;
                        }
                        else
                        {
                            PnlExpenseAdd.Visible = true;
                            MsgCls("", LblMsg, Color.Transparent);
                        }

                        objcontext.Dispose();

                        //// New Code Ends


                        ////MsgCls("", LblMsg, Color.Transparent);
                        ddlTask.Focus();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {

                ////MsgCls(Ex.Message, LblMsg, Color.Red);

                StringBuilder B = new StringBuilder();
                switch (Ex.Message)
                {
                    case "-01":
                        MsgCls("You're selecting a wrong Entity Internal Project code !", LblMsg, Color.Red);
                        B.Append("<ul class=\"LiNone\">");
                        B.Append("<li>You're selecting a wrong Entity Internal Project code !</li>");
                        B.Append("</ul>");
                        LblMsg.Text = B.ToString();
                        PnlExpenseAdd.Visible = false;
                        divSearch.Visible = true;
                        break;
                    case "-02":
                        MsgCls("This project code internal number is not maintained ! Please contact project team", LblMsg, Color.Red);
                        B.Append("<ul class=\"LiNone\">");
                        B.Append("<li>This project code internal number is not maintained ! Please contact project team</li>");
                        B.Append("</ul>");
                        LblMsg.Text = B.ToString();
                        PnlExpenseAdd.Visible = false;
                        divSearch.Visible = true;
                        break;
                    case "-03":
                        MsgCls("This project code internal number is not released !  Please contact project team", LblMsg, Color.Red);
                        B.Append("<ul class=\"LiNone\">");
                        B.Append("<li>This project code internal number is not released ! Please contact project team</li>");
                        B.Append("</ul>");
                        LblMsg.Text = B.ToString();
                        PnlExpenseAdd.Visible = false;
                        divSearch.Visible = true;
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true);
                        MsgCls(Ex.Message, LblMsg, Color.Red);
                        PnlExpenseAdd.Visible = false;
                        divSearch.Visible = true;
                        break;
                }

            }

        }

        private void LoadDDLExpenseType(string schema, string pernr)
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Expensetype_travel(schema, pernr);
                DDLExpenseType.DataSource = objLst;
                DDLExpenseType.DataTextField = "SPTXT";
                DDLExpenseType.DataValueField = "SPKZL";

                DDLExpenseType.DataBind();
                DDLExpenseType.Items.Insert(0, new ListItem("-SEARCH-", "0"));
                DDLExpenseType.SelectedValue = "0";

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }


        protected void GV_TravelReqUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_TravelReqUpdate.PageIndex = e.NewPageIndex;
                PageLoadEvents();
                Search();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        [WebMethod()]
        public static CascadingDropDownNameValue[] GetCurrencyTypes()
        {
            try
            {
                masterdalDataContext objDataContext = new masterdalDataContext();
                mastercollectionbo objList = new mastercollectionbo();
                List<CascadingDropDownNameValue> CurrencyName = new List<CascadingDropDownNameValue>();
                foreach (var vRow in objDataContext.sp_master_load_payment_currency())
                {
                    CurrencyName.Add(new CascadingDropDownNameValue(vRow.WAERS + " - " + vRow.LTEXT, vRow.WAERS));
                }
                return CurrencyName.ToArray();
            }
            catch (Exception Ex)
            { return null; }
        }


        #region  IExpense Types Empty DataTable

        private DataTable GetTrvlClaim()
        {
            try
            {
                DataTable Dt = new DataTable("TR_CLAIM");
                //Dt.Columns.Add("CID", typeof(int));
                Dt.Columns.Add("ID", typeof(int));
                Dt.Columns.Add("EXP_TYPE", typeof(string));
                Dt.Columns.Add("EXP_TYPE_NAME", typeof(string));
                Dt.Columns.Add("S_DATE", typeof(DateTime));
                Dt.Columns.Add("NO_DAYS", typeof(string));
                Dt.Columns.Add("DAILY_RATE", typeof(string));
                Dt.Columns.Add("EXPT_AMT", typeof(string));
                Dt.Columns.Add("EXPT_CURR", typeof(string));
                Dt.Columns.Add("EXC_RATE", typeof(string));
                Dt.Columns.Add("RE_AMT", typeof(string));
                Dt.Columns.Add("JUSTIFY", typeof(string));
                Dt.Columns.Add("RECEIPT_FILE", typeof(string));
                Dt.Columns.Add("RECEIPT_FIID", typeof(string));
                Dt.Columns.Add("RECEIPT_FPATH", typeof(string));
                Dt.Columns.Add("ZLAND", typeof(string));
                Dt.Columns.Add("ZORT1", typeof(string));
                Dt.Columns.Add("DEVIATION_AMT", typeof(string));
                Dt.Columns.Add("DEVIATION_CURR", typeof(string));
                Dt.Columns.Add("RCURR", typeof(string));
                Dt.Columns.Add("DAILYCURR", typeof(string));





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
                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                DateTime StartDate = new DateTime(0001, 01, 01);
                decimal ExchangeRate = 0.0M;
                string DailyRate = ServicesObj.GetExcRate(DDLTrClaimCountry.SelectedValue, DDLRegion.SelectedValue, DDLExpenseType.SelectedValue, Convert.ToDateTime(txtStartDate.Text));
                //if (decimal.TryParse(ServicesObj.GetExchangeRate(DDLExpenditureCurrency.SelectedValue, DDLReimbursementCurrency.SelectedValue), out ExchangeRate))
                //{ }
                if (decimal.TryParse(txtExchangeRate.Text.Trim(), out ExchangeRate))
                { }
                MsgCls(string.Empty, LblMsg, Color.Transparent);

                //if (GV_TravelExpReq.Rows.Count > 0)
                //{



                if (DDLReimbursementCurrency.SelectedValue != "0" && DDLReimbursementCurrency.SelectedValue != "")
                {
                    if (ddlTask.SelectedValue != "0")
                    {

                        //if (DateTime.TryParse(txtStartDate.Text, out StartDate) && ddlTask.SelectedValue != "0")
                        if (DateTime.TryParse(txtStartDate.Text, out StartDate))
                        {
                            using (DataTable Dt = ViewState["TR_CLAIM"] != null ? (DataTable)ViewState["TR_CLAIM"] : GetTrvlClaim())
                            {
                                if (DDLTrClaimCountry.SelectedValue != "0")
                                {
                                    if (((DDLTrClaimCountry.SelectedValue.Trim() == "IN") && (DDLRegion.SelectedValue != "" && DDLRegion.SelectedValue != "0")) || (DDLTrClaimCountry.SelectedValue.Trim() != "IN"))
                                    {
                                        //decimal DailyRate1 = 0;
                                        //if (HF_DailyRate.Value != null)
                                        //{
                                        //    if (decimal.TryParse(HF_DailyRate.Value, out DailyRate1))
                                        //    {
                                        //        string[] ExpType = { "4002", "4015", "4102", "4119", "4003", "4004", "4103", "4104" };
                                        //        if (DailyRate1 == 0 && ExpType.Contains(DDLExpenseType.SelectedValue))
                                        //        {
                                        //            MsgCls("Daily rate cannot be zero. Please contact your help desk.", LblMsg, Color.Red);
                                        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Daily rate cannot be zero !\\nPlease contact your help desk.');", true);
                                        //        }
                                        //        else
                                        //        {
                                        //Dt.Rows.Add(Dt.Rows.Count + 1, HF_REINR.Value.Split('&')[0], HF_REINR.Value.Split('&')[1], ddlTask.SelectedValue
                                        //    , DDLReimbursementCurrency.SelectedValue, DDLExpenseType.SelectedValue, DDLExpenseType.SelectedItem.Text, StartDate, TxtNoOfDays.Text, DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0"
                                        //    , txtExpenditureAmount.Text, DDLExpenditureCurrency.SelectedValue, txtExchangeRate.Text, Math.Abs(ExchangeRate < 0 ? decimal.Parse(txtExpenditureAmount.Text) / ExchangeRate
                                        //    : decimal.Parse(txtExpenditureAmount.Text) * ExchangeRate), txtJustification.Text, cb.Checked ? "YES" : "NO", fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "", fuAttachments.HasFile ? "~/TravelDoc/"
                                        //    + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "", DateTime.Now, User.Identity.Name, DDLTrClaimCountry.SelectedValue, DDLRegion.SelectedValue);

                                        Dt.Rows.Add(Dt.Rows.Count + 1, DDLExpenseType.SelectedValue, DDLExpenseType.SelectedItem.Text, StartDate, TxtNoOfDays.Text, DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000"
                                        , txtExpenditureAmount.Text, DDLExpenditureCurrency.SelectedValue, txtExchangeRate.Text, Math.Abs(ExchangeRate < 0 ? decimal.Parse(txtExpenditureAmount.Text) / ExchangeRate
                                       : decimal.Parse(txtExpenditureAmount.Text) * ExchangeRate), txtJustification.Text, cb.Checked ? "YES" : "NO", fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "", fuAttachments.HasFile ? "~/TravelDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "",
                                        DDLTrClaimCountry.SelectedValue, DDLRegion.SelectedValue, HF_Deviation.Value, HF_DeCurr.Value, DDLReimbursementCurrency.SelectedValue, DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "");

                                        if (fuAttachments.HasFile)
                                        { fuAttachments.SaveAs(Server.MapPath("~/TravelDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName)); }

                                        // DDLReimbursementCurrency.Enabled = false;
                                        DDLReimbursementCurrency.Enabled = false;
                                        ddlTask.Enabled = false;
                                        ViewState["TR_CLAIM"] = null;
                                        ViewState["TR_CLAIM"] = Dt;
                                        GV_TravelExpReq.DataSource = Dt;
                                        GV_TravelExpReq.DataBind();
                                        ClearClaimLineItems();


                                        decimal d = 0;
                                        decimal total = Dt.AsEnumerable()
                                         .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                                        string createdby = User.Identity.Name;
                                        decimal SettlementAmt = 0;
                                        string SettlementCurr = string.Empty;


                                        OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                                        OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, DDLReimbursementCurrency.SelectedValue);
                                        foreach (OtherReimbursementsbo objBo1 in objLst1)
                                        {
                                            SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                                            SettlementCurr = objBo1.SETTLECURR.ToString();
                                        }

                                        GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                                        GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                                        GV_TravelExpReq.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                                        DDLExpenseType.Focus();
                                        //GV_TravelExpReq.FooterRow.Cells[6].Text = "Settlement Amount";

                                        //GV_TravelExpReq.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                                        //GV_TravelExpReq.FooterRow.Cells[7].Text = SettlementAmt + "(" + (SettlementCurr) + ")";
                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        MsgCls("Daily rate cannot be zero. Please contact your help desk.", LblMsg, Color.Red);
                                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Daily rate cannot be zero !\\nPlease contact your help desk.');", true);
                                        //    }
                                        //}

                                        //else
                                        //{
                                        //    MsgCls("Daily rate cannot be zero/empty. Please contact your help desk.", LblMsg, Color.Red);
                                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Daily rate cannot be zero !\\nPlease contact your help desk.');", true);
                                        //}
                                        MsgCls("Claim Item Added Successfully !", lblMessageBoard, Color.Green);

                                        //---------------------------------
                                        ltTask.Text = ddlTask.SelectedItem.Text;
                                        //ltProject.Text = ddlProjectCode.SelectedItem.Text;
                                        ltReimbAmt.Text = GV_TravelExpReq.FooterRow.Cells[5].Text;
                                        dvlineitem.Visible = true;
                                        ////collapse();

                                        BtnSave.Visible = true;
                                        BtnSubmit.Visible = true;
                                        GV_TravelExpReq.Columns[13].Visible = true;
                                    }
                                    else
                                    {
                                        MsgCls("Select Region Name !", LblMsg, Color.Red);
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Select Region Name !');", true);
                                    }
                                }
                                else
                                {
                                    MsgCls("Select Country Name !", LblMsg, Color.Red);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Select Country Name !');", true);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Invalid Date');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please select Task');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please select Reimbursement Currency');", true);
                    //dvlialert.Visible = true;
                }

                //}
                //else
                //{
                //    dvlialert.Visible = true;
                //}
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

        private void ClearClaimLineItems()
        {
            try
            {
                //DDLExpenseType.SelectedValue = "0";
                txtStartDate.Text = string.Empty;
                txtExpenditureAmount.Text = string.Empty;
                //CCD_DDLExpenditureCurrency.SelectedValue = "0";
                txtExchangeRate.Text = string.Empty;
                LblReimbursableAmount.Text = "0.000";
                HF_ReimbursAmnt.Value = null;
                txtJustification.Text = string.Empty;
                cb.Checked = false;
                LblDailyRate.Text = "0.000";
                TxtNoOfDays.Text = "1";
                //CCD_DDLTrClaimCountry.SelectedValue = "0";
                LblCurrency.Text = "";
                LblDeviation.Text = "0.000";
                LblCurrency1.Text = "";
                HF_DailyRate.Value = "0.000";
                HF_DeCurr.Value = "";
                HF_Deviation.Value = "0.000";
                Session["fuAttachments"] = null;
                fuAttachmentsfname.Text = string.Empty;
                txtExchangeRate1.Text = string.Empty;
                LblReimbursableCurrency.Text = string.Empty;
                if (DDLTrClaimCountry.SelectedValue.Trim() != "IN")
                {
                    DDLRegion.SelectedValue = "0";
                    DDLRegion.Enabled = false;
                }
                else
                {
                    // DDLRegion.SelectedValue = "IN";
                    DDLRegion.Enabled = true;
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void ClearClaimLineItemssubmit()
        {
            try
            {
                DDLExpenseType.SelectedValue = "0";
                txtStartDate.Text = string.Empty;
                txtExpenditureAmount.Text = string.Empty;
                DDLExpenditureCurrency.SelectedValue = "0";
                txtExchangeRate.Text = string.Empty;
                LblReimbursableAmount.Text = "0.000";
                HF_ReimbursAmnt.Value = null;
                txtJustification.Text = string.Empty;
                cb.Checked = false;
                LblDailyRate.Text = "0.000";
                TxtNoOfDays.Text = "1";
                DDLTrClaimCountry.SelectedValue = "0";
                LblCurrency.Text = "";
                LblDeviation.Text = "0.000";
                LblCurrency1.Text = "";
                HF_DailyRate.Value = "0.000";
                HF_DeCurr.Value = "";
                HF_Deviation.Value = "0.000";
                Session["fuAttachments"] = null;
                fuAttachmentsfname.Text = string.Empty;
                txtExchangeRate1.Text = string.Empty;
                LblReimbursableCurrency.Text = string.Empty;
                DDLRegion.SelectedValue = "0";
                DDLRegion.Enabled = false;

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int? TravelClaim_ID = 0;
                decimal ClaimTotal = 0;
                travelrequestbl ObjTrvl = new travelrequestbl();
                TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();

                decimal TRamnt = 0;

                if (ViewState["TR_CLAIM"] != null)
                {
                    if (ViewState["TR_CLAIM"] != null)
                    {
                        using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                        {
                            TRamnt = decimal.Parse(Dt.AsEnumerable().Sum(x => double.Parse(x.Field<string>("RE_AMT"))).ToString("0.000"));

                        }
                    }

                    travelrequestcolumnscollectionbo objList = ObjTrvl.Travel_ClaimTotalAmtNew(User.Identity.Name, ViewState["@REINR"].ToString(), DDLReimbursementCurrency.SelectedValue);
                    foreach (travelrequestcolumnsbo objColumnsB in objList)
                    {
                        ClaimTotal = decimal.Parse(objColumnsB.ClaimTotalAmount.ToString());
                    }




                    bool? status = false;




                    //  OtherReimbursementsbo OtherReBO = new OtherReimbursementsbo();
                    //ObjTrvlReq.PERNR = User.Identity.Name;


                    ObjTrvlReq.REINR = HF_REINR.Value != "" ? HF_REINR.Value.Split('&')[0] : ViewState["@REINR"].ToString();//HF_REINR.Value.Split('&')[0];
                    ObjTrvlReq.WBS_ELEMT = HF_REINR.Value != "" ? HF_REINR.Value.Split('&')[1] : ViewState["WBS_ELEMT"].ToString().Split(':')[0].Trim();//HF_REINR.Value.Split('&')[1];
                    ObjTrvlReq.ACTIVITY = ddlTask.SelectedValue;
                    ObjTrvlReq.RCURR = DDLReimbursementCurrency.SelectedValue;
                    ObjTrvlReq.CREATED_ON = DateTime.Now;
                    ObjTrvlReq.CREATED_BY = User.Identity.Name;
                    ObjTrvlReq.APPROVED_ON1 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY1 = "";
                    ObjTrvlReq.REMARKS1 = string.Empty;
                    ObjTrvlReq.APPROVED_ON2 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY2 = "";
                    ObjTrvlReq.REMARKS2 = string.Empty;
                    ObjTrvlReq.APPROVED_ON3 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY3 = "";
                    ObjTrvlReq.REMARKS3 = string.Empty;
                    ObjTrvlReq.APPROVED_ON4 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY4 = "";
                    ObjTrvlReq.REMARKS4 = string.Empty;
                    ObjTrvlReq.APPROVED_ON5 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY5 = "";
                    ObjTrvlReq.REMARKS5 = string.Empty;
                    ObjTrvlReq.APPROVED_ON6 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY6 = "";
                    ObjTrvlReq.REMARKS6 = string.Empty;
                    ObjTrvlReq.APPROVED_ON7 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY7 = "";
                    ObjTrvlReq.REMARKS7 = string.Empty;
                    ObjTrvlReq.APPROVED_ON8 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY8 = "";
                    ObjTrvlReq.REMARKS8 = string.Empty;
                    ObjTrvlReq.APPROVED_ON9 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY9 = "";
                    ObjTrvlReq.REMARKS9 = string.Empty;
                    ObjTrvlReq.STATUS = "";
                    ObjTrvlReq.TotalAmount = TRamnt + ClaimTotal;// ClaimTotal + Dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                    ObjTrvl.CreateTravelClaim(ObjTrvlReq, ref TravelClaim_ID, ref status);
                    ltClaimID.Text = TravelClaim_ID.ToString();
                    if (status == true)
                    {
                        if (ViewState["TR_CLAIM"] != null)
                        {
                            using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                            {
                                int flag = 0;
                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    decimal d = 0;
                                    // travelrequestbl ObjTrvl = new travelrequestbl();
                                    //TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                                    ObjTrvlReq.CID = TravelClaim_ID;
                                    ObjTrvlReq.LID = i + 1;
                                    ObjTrvlReq.EXP_TYPE = Dt.Rows[i]["EXP_TYPE"].ToString();
                                    ObjTrvlReq.S_DATE = DateTime.Parse(Dt.Rows[i]["S_DATE"].ToString());
                                    ObjTrvlReq.NO_DAYS = Dt.Rows[i]["NO_DAYS"].ToString();
                                    ObjTrvlReq.DAILY_RATE = Dt.Rows[i]["DAILY_RATE"].ToString();
                                    ObjTrvlReq.EXPT_AMT = Dt.Rows[i]["EXPT_AMT"].ToString();
                                    ObjTrvlReq.EXPT_CURR = Dt.Rows[i]["EXPT_CURR"].ToString();
                                    ObjTrvlReq.EXC_RATE = Dt.Rows[i]["EXC_RATE"].ToString();
                                    ObjTrvlReq.RE_AMT = Dt.Rows[i]["RE_AMT"].ToString();
                                    ObjTrvlReq.JUSTIFY = Dt.Rows[i]["JUSTIFY"].ToString();
                                    ObjTrvlReq.RECEIPT_FILE = Dt.Rows[i]["RECEIPT_FILE"].ToString();
                                    ObjTrvlReq.RECEIPT_FID = Dt.Rows[i]["RECEIPT_FIID"].ToString();
                                    ObjTrvlReq.RECEIPT_FPATH = Dt.Rows[i]["RECEIPT_FPATH"].ToString();
                                    ObjTrvlReq.ZLAND = Dt.Rows[i]["ZLAND"].ToString();
                                    if (Dt.Rows[i]["ZLAND"].ToString().Trim() == "IN")
                                    {
                                        ObjTrvlReq.ZORT1 = Dt.Rows[i]["ZORT1"].ToString();
                                    }
                                    else
                                    {
                                        ObjTrvlReq.ZORT1 = "";
                                    }

                                    ObjTrvlReq.DEVIATION_AMT = Dt.Rows[i]["DEVIATION_AMT"].ToString();
                                    ObjTrvlReq.DEVIATION_CURR = Dt.Rows[i]["DEVIATION_CURR"].ToString();
                                    ObjTrvlReq.DAILY_CURR = Dt.Rows[i]["DAILYCURR"].ToString();
                                    // ObjTrvlReq.TotalAmount = decimal.Parse(Dt.Rows[i]["RE_AMT"].ToString());// ClaimTotal + Dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                                    //  ObjTrvlReq.TotalAmount =  Dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);


                                    ObjTrvl.Travel_ClaimItems(ObjTrvlReq, ref status);
                                    if (status == true)
                                    {
                                        ////MsgCls("Travel claims sent successfully !", lblMessageBoard, Color.Green);
                                        MsgCls("Travel claim " + TravelClaim_ID + "sent successfully !", LblMsg, Color.Green);
                                        string alert = "Travel claim " + TravelClaim_ID + " sent successfully !";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='Travel_Requests.aspx';", true);

                                        LblMsg.Text = "";
                                        dvlineitem.Visible = false;
                                        flag = 1;
                                        //// divSearch.Visible = true;

                                        //---------------------------------
                                        ////ltType.Text = ddlExpenseType.SelectedItem.Text;
                                        ////ltProject.Text = ddlProjectCode.SelectedItem.Text;
                                        ////ltReimbAmt.Text = grd_IExpInfo.FooterRow.Cells[5].Text;
                                        //dvlineitem.Visible = false;
                                        ////collapse();
                                        //---------------------------------

                                    }

                                }
                                if (flag == 1)
                                {

                                    //   SendMailMethod(ObjTrvlReq, TRamnt);
                                    ////SendMailMethod(TravelClaim_ID, HF_REINR.Value.Split('&')[1], ddlTask.SelectedValue, DDLReimbursementCurrency.SelectedValue);
                                    SendMailMethod(TravelClaim_ID, HF_REINR.Value != "" ? HF_REINR.Value.Split('&')[1] : ViewState["WBS_ELEMT"].ToString().Split(':')[0].Trim(), ddlTask.SelectedValue, DDLReimbursementCurrency.SelectedValue);//Newly added

                                }

                            }
                        }
                    }
                    ViewState["TR_CLAIM"] = null;
                    GV_TravelExpReq.DataSource = null;
                    GV_TravelExpReq.DataBind();
                    ClearClaimLineItemssubmit();
                    DDLReimbursementCurrency.Enabled = true;
                    DDLReimbursementCurrency.SelectedValue = "0";
                    ddlTask.Enabled = true;
                    ddlTask.SelectedValue = "0";
                    PnlExpenseAdd.Visible = false;
                }
                else
                {
                    MsgCls("Please Add Atleast one Claim Item!", lblMessageBoard, Color.Red);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please Add Atleast one Claim Item!');", true);
                }
            }


            catch (Exception Ex)
            {

                switch (Ex.Message)
                {


                    case "-05":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Approvers are missing!')", true);
                        MsgCls("Approvers are missing!", LblMsg, Color.Red);
                        //PageLoadEvents();
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        break;
                }
                //MsgCls(Ex.Message, LblMsg, Color.Red);
            }
            //catch (Exception Ex)
            //{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void SendMailMethod(int? CID, string Project, string Task, string ReCurrency)
        {
            try
            {

                if (Task == "B")
                {
                    Task = "Billable";
                }
                else
                {
                    Task = "Non-Billable";
                }

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                GV_TravelExpReq.Columns[12].Visible = false;
                GV_TravelExpReq.Columns[13].Visible = false;
                string ClaimTotal = GV_TravelExpReq.FooterRow.Cells[5].Text;
                GV_TravelExpReq.FooterRow.ForeColor = System.Drawing.Color.Black;
                GV_TravelExpReq.FooterRow.Visible = true;
                GV_TravelExpReq.RenderControl(hw1);

                //  GV_TravelExpReq.FooterRow.RenderControl(hw1);
                GV_TravelExpReq.Columns[12].Visible = true;
                GV_TravelExpReq.Columns[13].Visible = true;

                string strSubject = string.Empty;

                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVED_BY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                string CREATED_BY = "";

                string Project_code = "";
                string Entity = "";
                string TAmt = "";


                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_TravelclaimNew(CID, User.Identity.Name, ref APPROVED_BY1, ref Approver_Name,
                 ref Approver_Email, ref EMP_Name, ref EMP_Email, ref Entity, ref Project_code, ref TAmt);


                if (Approver_Email != null)
                {

                    strSubject = "Travel Claim Request " + CID + " has been Raised by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.";


                    RecipientsString = Approver_Email;
                    strPernr_Mail = EMP_Email;
                    //Preparing the mail body--------------------------------------------------
                    string body = "<b>Travel Claim Request " + CID + " has been Raised by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.<br/><br/></b>";
                    body += "<b>Entity with Claim ID  :  " + Entity + " : " + CID + "</b><br/><br/>";
                    body += "<b>Travel Claim Header Details :<hr /><br/>";
                    body += "Trip ID       :  " + ViewState["@REINR"].ToString() + "<br/>";
                    body += "Project       :  " + Project_code + " - " + Project + "<br/>";
                    body += "Task          :  " + Task + "<br/>";
                    body += "Total Current Claim Reimbursement Amount  :  " + ClaimTotal + "<br/>";
                    //body += "Total Trip Claims Reimbursement Amount  :  " + TAmt + " ( " + ReCurrency + " ) <br/>";
                    body += "Total Trip Claims Reimbursement Amount  :  " + decimal.Parse(TAmt).ToString("#,##0.00") + " ( " + ReCurrency + " ) <br/>";
                    // body += "Reimbursement Currency      :  " + ReCurrency + "<br/><br/>";
                    body += "<b>Travel Claim Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";
                    //total.ToString("#,##0.00")

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
                    lblMessageBoard.Text = "Travel claims created successfully and Mail sent successfully.";

                }
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Travel claims created successfully. Error in sending mail";
                return;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnUpdateItems_Click(object sender, EventArgs e)
        {
            try
            {
                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                DateTime StartDate = new DateTime(0001, 01, 01);
                decimal ExchangeRate = 0.0M;
                string fileupdate = string.Empty;

                string DailyRate = ServicesObj.GetExcRate(DDLTrClaimCountry.SelectedValue, DDLRegion.SelectedValue, DDLExpenseType.SelectedValue, Convert.ToDateTime(txtStartDate.Text));
                if (decimal.TryParse(txtExchangeRate.Text, out ExchangeRate))
                { }
                //int LID = int.Parse(GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                //ViewState["LIDtoAdd"] = LID;
                int rowindex = int.Parse(ViewState["RowIndex"].ToString());
                int lid = int.Parse(ViewState["LIDtoAdd"].ToString());

                using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                {

                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        //  Dt.Columns.Add("ID", typeof(int));

                        //ID
                        Dt.Rows[rowindex]["EXP_TYPE"] = DDLExpenseType.SelectedValue;
                        if (DateTime.TryParse(txtStartDate.Text, out StartDate))
                        {
                            Dt.Rows[rowindex]["S_DATE"] = StartDate;
                        }
                        Dt.Rows[rowindex]["EXPT_AMT"] = txtExpenditureAmount.Text;
                        Dt.Rows[rowindex]["EXPT_CURR"] = DDLExpenditureCurrency.SelectedValue;
                        Dt.Rows[rowindex]["EXC_RATE"] = txtExchangeRate.Text;
                        Dt.Rows[rowindex]["RE_AMT"] = Math.Abs(ExchangeRate < 0 ? decimal.Parse(txtExpenditureAmount.Text) / ExchangeRate : decimal.Parse(txtExpenditureAmount.Text) * ExchangeRate);


                        Dt.Rows[rowindex]["DAILY_RATE"] = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000";
                        Dt.Rows[rowindex]["DEVIATION_AMT"] = HF_Deviation.Value;
                        Dt.Rows[rowindex]["DEVIATION_CURR"] = HF_DeCurr.Value;
                        Dt.Rows[rowindex]["JUSTIFY"] = txtJustification.Text;
                        Dt.Rows[rowindex]["RECEIPT_FILE"] = cb.Checked ? "YES" : "NO";

                        if (fuAttachments != null)
                        {
                            if (fuAttachments.HasFile)
                            {
                                Dt.Rows[rowindex]["RECEIPT_FIID"] = fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "";
                                Dt.Rows[rowindex]["RECEIPT_FPATH"] = fuAttachments.HasFile ? "~/TravelDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "";
                                if (fuAttachments.HasFile)
                                { fuAttachments.SaveAs(Server.MapPath("~/TravelDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName)); }

                                fileupdate = "UpdateFileUpload";
                            }
                        }
                        else
                        {
                            Dt.Rows[rowindex]["RECEIPT_FIID"] = ViewState["Receiptfileid"].ToString();
                            Dt.Rows[rowindex]["RECEIPT_FPATH"] = ViewState["Receiptfilepath"].ToString();
                            fileupdate = "UpdateNoFileUpload";
                        }
                        //Dt.Rows[rowindex]["RECEIPT_FIID"] = fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "";
                        //Dt.Rows[rowindex]["RECEIPT_FPATH"] = fuAttachments.HasFile ? "~/TravelDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "";
                        Dt.Rows[rowindex]["ZLAND"] = DDLTrClaimCountry.SelectedValue;
                        Dt.Rows[rowindex]["ZORT1"] = DDLRegion.SelectedValue;
                        Dt.Rows[rowindex]["NO_DAYS"] = TxtNoOfDays.Text;
                        Dt.Rows[rowindex]["RCURR"] = DDLReimbursementCurrency.SelectedValue;
                        Dt.Rows[rowindex]["DAILYCURR"] = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                        Dt.Rows[rowindex]["EXP_TYPE_NAME"] = DDLExpenseType.SelectedItem.Text;
                        if (fuAttachments != null)
                        {
                            if (fuAttachments.HasFile)
                            { fuAttachments.SaveAs(Server.MapPath("~/TravelDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName)); }
                        }

                    }
                    GV_TravelExpReq.DataSource = Dt;
                    GV_TravelExpReq.DataBind();
                    //ClearClaimLineItemssubmit();
                    ClearClaimLineItems();
                    decimal d = 0;
                    decimal total = Dt.AsEnumerable()
                     .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                    string createdby = User.Identity.Name;
                    decimal SettlementAmt = 0;
                    string SettlementCurr = string.Empty;


                    OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                    OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, DDLReimbursementCurrency.SelectedValue);
                    foreach (OtherReimbursementsbo objBo1 in objLst1)
                    {
                        SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                        SettlementCurr = objBo1.SETTLECURR.ToString();
                    }

                    GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                    GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    GV_TravelExpReq.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                    MsgCls("Claim Item Updated Successfully !", LblMsg, Color.Green);
                    GV_TravelExpReq.Columns[13].Visible = true;
                }

                btnAdd.Visible = true;
                btnUpdateItems.Visible = false;

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        protected void GV_TravelExpReq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

                    case "EDITITEMS":
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in GV_TravelExpReq.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        MsgCls("", LblMsg, Color.Transparent);
                        btnAdd.Visible = false;
                        btnUpdateItems.Visible = true;

                        string EXP_TYPE = "";
                        string S_DATE = "";
                        string EXPT_AMT = "";
                        string EXPT_CURR = "";
                        string EXC_RATE = "";
                        string RE_AMT = "";
                        string RCURR = "";

                        string DAILY_RATE = "";
                        string DEVIATION_AMT = "";
                        string DEVIATION_CURR = "";
                        string JUSTIFY = "";
                        string RECEIPT_FILE = "";
                        string CountryID = "";
                        string RegoinID = "";
                        string NO_DAYS = "";
                        string RECEIPT_FIID = "";
                        string RECEIPT_FPATH = "";
                        ClearClaimLineItemssubmit();
                        int index = Convert.ToInt32(e.CommandArgument);
                        ViewState["RowIndex"] = index;


                        int LID = int.Parse(GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        ViewState["LIDtoAdd"] = LID;
                        using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                        {

                            for (int i = 0; i < Dt.Rows.Count; i++)
                            {

                                EXP_TYPE = Dt.Rows[index]["EXP_TYPE"].ToString();
                                S_DATE = Dt.Rows[index]["S_DATE"].ToString();
                                EXPT_AMT = Dt.Rows[index]["EXPT_AMT"].ToString();
                                EXPT_CURR = Dt.Rows[index]["EXPT_CURR"].ToString();
                                EXC_RATE = Dt.Rows[index]["EXC_RATE"].ToString();
                                RE_AMT = Dt.Rows[index]["RE_AMT"].ToString();
                                RCURR = DDLReimbursementCurrency.SelectedValue;

                                DAILY_RATE = Dt.Rows[index]["DAILY_RATE"].ToString();
                                DEVIATION_AMT = Dt.Rows[index]["DEVIATION_AMT"].ToString();
                                DEVIATION_CURR = Dt.Rows[index]["DEVIATION_CURR"].ToString();
                                JUSTIFY = Dt.Rows[index]["JUSTIFY"].ToString();
                                RECEIPT_FILE = Dt.Rows[index]["RECEIPT_FILE"].ToString();
                                CountryID = Dt.Rows[index]["ZLAND"].ToString();
                                RegoinID = Dt.Rows[index]["ZORT1"].ToString();
                                NO_DAYS = Dt.Rows[index]["NO_DAYS"].ToString();
                                RECEIPT_FIID = Dt.Rows[index]["RECEIPT_FIID"].ToString();
                                ViewState["Receiptfileid"] = Dt.Rows[index]["RECEIPT_FIID"].ToString();
                                RECEIPT_FPATH = Dt.Rows[index]["RECEIPT_FPATH"].ToString();
                                ViewState["Receiptfilepath"] = Dt.Rows[index]["RECEIPT_FPATH"].ToString();
                            }
                        }
                        //string EXP_TYPE = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXP_TYPE"].ToString();
                        //string S_DATE = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["S_DATE"].ToString();
                        //string EXPT_AMT = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPT_AMT"].ToString();
                        //string EXPT_CURR = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPT_CURR"].ToString();
                        //string EXC_RATE = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXC_RATE"].ToString();
                        //string RE_AMT = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RE_AMT"].ToString();
                        //string RCURR = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();

                        //string DAILY_RATE = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["DAILY_RATE"].ToString();
                        //string DEVIATION_AMT = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["DEVIATION_AMT"].ToString();
                        //string DEVIATION_CURR = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["DEVIATION_CURR"].ToString();
                        //string JUSTIFY = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["JUSTIFY"].ToString();
                        //string RECEIPT_FILE = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FILE"].ToString();
                        //string CountryID = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["CountryID"].ToString();
                        //string RegoinID = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RegoinID"].ToString();
                        //string EXPID = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPID"].ToString();
                        //string NO_DAYS = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["NO_DAYS"].ToString();

                        DDLExpenseType.SelectedValue = EXP_TYPE;

                        // CE_txtStartDate.SelectedDate = DateTime.Parse(S_DATE);
                        txtStartDate.Text = (DateTime.Parse(S_DATE)).ToString();
                        txtExpenditureAmount.Text = EXPT_AMT;
                        DDLExpenditureCurrency.SelectedValue = EXPT_CURR;
                        txtExchangeRate.Text = EXC_RATE;
                        LblReimbursableAmount.Text = decimal.Parse(RE_AMT).ToString("0.000");
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
                        DDLTrClaimCountry.SelectedValue = CountryID;
                        DDLRegion.SelectedValue = RegoinID.Trim();
                        LblDailyRate.Text = DAILY_RATE;
                        TxtNoOfDays.Text = NO_DAYS;
                        LblDeviation.Text = DEVIATION_AMT;
                        LblCurrency1.Text = DEVIATION_CURR;
                        GV_TravelExpReq.Columns[13].Visible = false;

                        break;

                    case "DELETE":
                        int ID = int.Parse(GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());

                        using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                        {
                            DataRow[] rows = (from t in Dt.AsEnumerable().Cast<DataRow>()
                                              where t.Field<int>("ID") == ID
                                              select t).ToArray();

                            foreach (DataRow row in rows)
                            { Dt.Rows.Remove(row); }

                            ViewState["TR_CLAIM"] = null;
                            ViewState["TR_CLAIM"] = Dt;

                            if (Dt.Rows.Count > 0)
                            {
                                GV_TravelExpReq.DataSource = Dt;
                                GV_TravelExpReq.DataBind();

                                decimal d = 0;
                                decimal total = Dt.AsEnumerable()
                                 .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                                string createdby = User.Identity.Name;
                                decimal SettlementAmt = 0;
                                string SettlementCurr = string.Empty;


                                OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                                OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, DDLReimbursementCurrency.SelectedValue);
                                foreach (OtherReimbursementsbo objBo1 in objLst1)
                                {
                                    SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                                    SettlementCurr = objBo1.SETTLECURR.ToString();
                                }

                                GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                                GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                                GV_TravelExpReq.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                                //GV_TravelExpReq.FooterRow.Cells[6].Text = "Settlement Amount";

                                //GV_TravelExpReq.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                                //GV_TravelExpReq.FooterRow.Cells[7].Text = SettlementAmt + "(" + (SettlementCurr) + ")";
                                MsgCls("Claim Item Deleted Successfully !", LblMsg, Color.Green);
                            }
                            else
                            {

                                ViewState["TR_CLAIM"] = null;
                                GV_TravelExpReq.DataSource = null;
                                GV_TravelExpReq.DataBind();
                                ClearClaimLineItemssubmit();
                                //  DDLReimbursementCurrency.Enabled = true;
                                DDLReimbursementCurrency.Enabled = true;
                                ddlTask.Enabled = true;
                            }
                        }

                        break;


                    case "DELETEFILE":


                        int IDDeletefile = int.Parse(GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());

                        int indexdf = Convert.ToInt32(e.CommandArgument);
                        //ViewState["RowIndex"] = index;

                        using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                        {

                            for (int i = 0; i < Dt.Rows.Count; i++)
                            {


                                Dt.Rows[indexdf]["RECEIPT_FIID"] = "";
                                Dt.Rows[indexdf]["RECEIPT_FPATH"] = "";
                            }


                            GV_TravelExpReq.DataSource = Dt;
                            GV_TravelExpReq.DataBind();
                            //ClearClaimLineItems();
                            decimal d = 0;
                            decimal total = Dt.AsEnumerable()
                             .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                            string createdby = User.Identity.Name;
                            decimal SettlementAmt = 0;
                            string SettlementCurr = string.Empty;


                            OtherReimbursementsbl objbl1 = new OtherReimbursementsbl();
                            OtherReimbursementCollectionbo objLst1 = objbl1.Load_SettlementAmount(createdby, total, DDLReimbursementCurrency.SelectedValue);
                            foreach (OtherReimbursementsbo objBo1 in objLst1)
                            {
                                SettlementAmt = decimal.Parse(objBo1.SETTLEAMT.ToString());
                                SettlementCurr = objBo1.SETTLECURR.ToString();
                            }

                            GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                            GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            GV_TravelExpReq.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                        }

                        break;

                    case "DOWNLOAD":
                        string filePath1 = e.CommandArgument.ToString();
                        Response.ContentType = "application/octet-stream";
                        //Response.ContentType = ContentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath1));
                        Response.WriteFile(filePath1);
                        Response.End();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }


        protected void GV_TravelExpReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int? TravelClaim_ID = 0;
                decimal ClaimTotal = 0;
                travelrequestbl ObjTrvl = new travelrequestbl();
                TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();

                decimal TRamnt = 0;

                if (ViewState["TR_CLAIM"] != null)
                {
                    if (ViewState["TR_CLAIM"] != null)
                    {
                        using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                        {
                            TRamnt = decimal.Parse(Dt.AsEnumerable().Sum(x => double.Parse(x.Field<string>("RE_AMT"))).ToString("0.000"));

                        }
                    }

                    travelrequestcolumnscollectionbo objList = ObjTrvl.Travel_ClaimTotalAmtNew(User.Identity.Name, ViewState["@REINR"].ToString(), DDLReimbursementCurrency.SelectedValue);
                    foreach (travelrequestcolumnsbo objColumnsB in objList)
                    {
                        ClaimTotal = decimal.Parse(objColumnsB.ClaimTotalAmount.ToString());
                    }




                    bool? status = false;




                    //  OtherReimbursementsbo OtherReBO = new OtherReimbursementsbo();
                    //ObjTrvlReq.PERNR = User.Identity.Name;

                    ObjTrvlReq.REINR = HF_REINR.Value != "" ? HF_REINR.Value.Split('&')[0] : ViewState["@REINR"].ToString();
                    ObjTrvlReq.WBS_ELEMT = HF_REINR.Value != "" ? HF_REINR.Value.Split('&')[1] : ViewState["WBS_ELEMT"].ToString().Split(':')[0].Trim();
                    ObjTrvlReq.ACTIVITY = ddlTask.SelectedValue;
                    ObjTrvlReq.RCURR = DDLReimbursementCurrency.SelectedValue;
                    ObjTrvlReq.CREATED_ON = DateTime.Now;
                    ObjTrvlReq.CREATED_BY = User.Identity.Name;
                    ObjTrvlReq.APPROVED_ON1 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY1 = "";
                    ObjTrvlReq.REMARKS1 = string.Empty;
                    ObjTrvlReq.APPROVED_ON2 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY2 = "";
                    ObjTrvlReq.REMARKS2 = string.Empty;
                    ObjTrvlReq.APPROVED_ON3 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY3 = "";
                    ObjTrvlReq.REMARKS3 = string.Empty;
                    ObjTrvlReq.APPROVED_ON4 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY4 = "";
                    ObjTrvlReq.REMARKS4 = string.Empty;
                    ObjTrvlReq.APPROVED_ON5 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY5 = "";
                    ObjTrvlReq.REMARKS5 = string.Empty;
                    ObjTrvlReq.APPROVED_ON6 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY6 = "";
                    ObjTrvlReq.REMARKS6 = string.Empty;
                    ObjTrvlReq.APPROVED_ON7 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY7 = "";
                    ObjTrvlReq.REMARKS7 = string.Empty;
                    ObjTrvlReq.APPROVED_ON8 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY8 = "";
                    ObjTrvlReq.REMARKS8 = string.Empty;
                    ObjTrvlReq.APPROVED_ON9 = DateTime.MinValue;
                    ObjTrvlReq.APPROVED_BY9 = "";
                    ObjTrvlReq.REMARKS9 = string.Empty;
                    ObjTrvlReq.STATUS = "Saved";
                    ObjTrvlReq.TotalAmount = TRamnt + ClaimTotal;// ClaimTotal + Dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                    ObjTrvl.SaveTravelClaim(ObjTrvlReq, ref TravelClaim_ID, ref status);
                    ltClaimID.Text = TravelClaim_ID.ToString();
                    if (status == true)
                    {
                        if (ViewState["TR_CLAIM"] != null)
                        {
                            using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                            {

                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    decimal d = 0;
                                    // travelrequestbl ObjTrvl = new travelrequestbl();
                                    //TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                                    ObjTrvlReq.CID = TravelClaim_ID;
                                    ObjTrvlReq.LID = i + 1;
                                    ObjTrvlReq.EXP_TYPE = Dt.Rows[i]["EXP_TYPE"].ToString();
                                    ObjTrvlReq.S_DATE = DateTime.Parse(Dt.Rows[i]["S_DATE"].ToString());
                                    ObjTrvlReq.NO_DAYS = Dt.Rows[i]["NO_DAYS"].ToString();
                                    ObjTrvlReq.DAILY_RATE = Dt.Rows[i]["DAILY_RATE"].ToString();
                                    ObjTrvlReq.EXPT_AMT = Dt.Rows[i]["EXPT_AMT"].ToString();
                                    ObjTrvlReq.EXPT_CURR = Dt.Rows[i]["EXPT_CURR"].ToString();
                                    ObjTrvlReq.EXC_RATE = Dt.Rows[i]["EXC_RATE"].ToString();
                                    ObjTrvlReq.RE_AMT = Dt.Rows[i]["RE_AMT"].ToString();
                                    ObjTrvlReq.JUSTIFY = Dt.Rows[i]["JUSTIFY"].ToString();
                                    ObjTrvlReq.RECEIPT_FILE = Dt.Rows[i]["RECEIPT_FILE"].ToString();
                                    ObjTrvlReq.RECEIPT_FID = Dt.Rows[i]["RECEIPT_FIID"].ToString();
                                    ObjTrvlReq.RECEIPT_FPATH = Dt.Rows[i]["RECEIPT_FPATH"].ToString();
                                    ObjTrvlReq.ZLAND = Dt.Rows[i]["ZLAND"].ToString();


                                    if (Dt.Rows[i]["ZLAND"].ToString().Trim() == "IN")
                                    {
                                        ObjTrvlReq.ZORT1 = Dt.Rows[i]["ZORT1"].ToString();
                                    }
                                    else
                                    {
                                        ObjTrvlReq.ZORT1 = "";
                                    }


                                    // ObjTrvlReq.ZORT1 = Dt.Rows[i]["ZORT1"].ToString();
                                    ObjTrvlReq.DEVIATION_AMT = Dt.Rows[i]["DEVIATION_AMT"].ToString();
                                    ObjTrvlReq.DEVIATION_CURR = Dt.Rows[i]["DEVIATION_CURR"].ToString();
                                    ObjTrvlReq.DAILY_CURR = Dt.Rows[i]["DAILYCURR"].ToString();
                                    ObjTrvl.Travel_ClaimItems(ObjTrvlReq, ref status);
                                    if (status == true)
                                    {
                                        ////MsgCls("Travel claims saved successfully !", lblMessageBoard, Color.Green);
                                        MsgCls("Travel claim " + TravelClaim_ID + " saved successfully !", lblMessageBoard, Color.Green);
                                        string alert = "Travel claim " + TravelClaim_ID + " saved successfully !";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='Travel_Requests.aspx';", true);

                                        LblMsg.Text = "";
                                        dvlineitem.Visible = true;
                                        divSearch.Visible = false;

                                        lbtnEdit.Visible = true;
                                        lbtAddNew.Visible = true;
                                        collapse();
                                    }

                                }


                            }
                        }
                    }
                    ////ViewState["TR_CLAIM"] = null;
                    ////GV_TravelExpReq.DataSource = null;
                    ////GV_TravelExpReq.DataBind();
                    ////ClearClaimLineItemssubmit();
                    ////DDLReimbursementCurrency.Enabled = true;
                    ////DDLReimbursementCurrency.SelectedValue = "0";
                    ////ddlTask.Enabled = true;
                    ////ddlTask.SelectedValue = "0";
                    ////PnlExpenseAdd.Visible = false;

                }

                else
                {
                    MsgCls("Please Add Atleast one Claim Item!", LblMsg, Color.Red);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please Add Atleast one Claim Item!');", true);
                }
            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }


        protected void DDLCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            txtExchangeRate.Text = string.Empty;
            LblReimbursableAmount.Text = string.Empty;
            LblReimbursableCurrency.Text = string.Empty;
            GetExchangeRate();
            // txtExchangeRate1.Text = string.Empty;
            ////txtStartDate.Focus();
        }

        protected void txtExpenditureAmount_TextChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            txtExchangeRate.Text = string.Empty;
            LblReimbursableAmount.Text = string.Empty;
            LblReimbursableCurrency.Text = string.Empty;
            GetExchangeRate();
            //GetDeviationAmtCurr();
            GetDailyRate();
            ////txtJustification.Focus();
            txtExpenditureAmount.Focus();
        }

        protected void ddlExpenditureCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            txtExchangeRate.Text = string.Empty;
            LblReimbursableAmount.Text = string.Empty;
            LblReimbursableCurrency.Text = string.Empty;
            GetExchangeRate();
            //GetDeviationAmtCurr();
            GetDailyRate();
            //  txtExchangeRate1.Text=string.Empty;
            DDLExpenditureCurrency.Focus();
        }

        protected void txtExchangeRate1_TextChanged(object sender, EventArgs e)
        {
            try
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

                                LblReimbursableAmount.Text = (Math.Round(ramt, 3)).ToString();
                                //lblReimbursableAmount.Text = ramt.ToString();
                                MsgCls("", lblIndent, Color.White);
                            }
                            else
                            {
                                ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                                LblReimbursableAmount.Text = (Math.Round(ramt, 3)).ToString();
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
                ////txtJustification.Focus();
                txtExchangeRate.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }


        public void GetExchangeRate()
        {
            try
            {

                if ((DDLExpenditureCurrency.SelectedValue != "" && DDLExpenditureCurrency.SelectedValue != "0") && (DDLReimbursementCurrency.SelectedValue != "" && DDLReimbursementCurrency.SelectedValue != "0"))
                {
                    if (DDLExpenditureCurrency.SelectedValue != DDLReimbursementCurrency.SelectedValue)
                    {
                        OtherReimbursementsbl objbl = new OtherReimbursementsbl();
                        OtherReimbursementCollectionbo objLst = objbl.Load_ExchangeRate(DDLExpenditureCurrency.SelectedValue.ToString(), DDLReimbursementCurrency.SelectedValue.ToString());
                        foreach (OtherReimbursementsbo objBo in objLst)
                        {
                            MsgCls("", lblIndent, Color.White);
                            txtExchangeRate.Text = objBo.UKURS.ToString();

                        }

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

                    //if (DDLExpenditureCurrency.SelectedValue == "" || DDLExpenditureCurrency.SelectedValue == "0")
                    //{
                    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Expenditure Currency !')", true);
                    //    MsgCls("Please select Expenditure Currency !", lblIndent, Color.Red);

                    //}
                    //if (DDLReimbursementCurrency.SelectedValue == "" || DDLReimbursementCurrency.SelectedValue == "0")
                    //{
                    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Reimbursement Currency !')", true);
                    //    MsgCls("Please select Reimbursement Currency !", lblIndent, Color.Red);

                    //}

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

                            LblReimbursableAmount.Text = (Math.Round(ramt, 3)).ToString();
                            //lblReimbursableAmount.Text = ramt.ToString();
                            LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            LblReimbursableAmount.Text = (Math.Round(ramt, 3)).ToString();
                            LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;
                            // lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                    }
                    else
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Expenditure Amount!')", true);
                        MsgCls("Please enter correct Expenditure Amount!", lblIndent, Color.Red);


                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Exchange Rate!')", true);
                    MsgCls("Please enter correct Exchange Rate!", lblIndent, Color.Red);

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

                            LblReimbursableAmount.Text = (Math.Round(ramt, 3)).ToString();
                            //lblReimbursableAmount.Text = ramt.ToString();
                            LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            LblReimbursableAmount.Text = (Math.Round(ramt, 3)).ToString();
                            LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;
                            // lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                    }
                    else
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Expenditure Amount!')", true);
                        MsgCls("Please enter correct Expenditure Amount!", lblIndent, Color.Red);


                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter correct Exchange Rate!')", true);
                    MsgCls("Please enter correct Exchange Rate!", lblIndent, Color.Red);

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
                LblReimbursableAmount.Text = ramt.ToString();
                LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;

            }
            else
            {
                ramt = ExpenditureAmount1 * Math.Abs(ExchangeRate1);
                //lblReimbursableAmount.Text = (Math.Round(ramt, 2)).ToString();
                LblReimbursableAmount.Text = ramt.ToString();
                LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;

            }
            return LblReimbursableAmount.Text;
        }



        public void GetDailyRate()
        {
            try
            {


                string DailyRate = "";
                LblDailyRate.Text = "";
                ViewState["dailyrate"] = "";
                if ((DDLTrClaimCountry.SelectedValue != "") && DDLExpenseType.SelectedValue != "0")
                {
                    if (TxtNoOfDays.Text.Trim() == "1")
                    {
                        if (DDLTrClaimCountry.SelectedValue.Trim() == "IN" && DDLRegion.SelectedValue != "")
                        {
                            DailyRate = ServicesObj.GetExcRate(DDLTrClaimCountry.SelectedValue, DDLRegion.SelectedValue, DDLExpenseType.SelectedValue, Convert.ToDateTime(txtStartDate.Text));
                            if ((DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000") == " ")
                            {
                                ViewState["dailyrate"] = "";
                                LblDailyRate.Text = "";
                            }
                            else
                            {

                                LblDailyRate.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000";
                            }
                            if ((DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "") == " ")
                            {
                                ViewState["dailyratecurr"] = "";
                                LblCurrency.Text = "";
                            }
                            else
                            {

                                LblCurrency.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                            }
                            //LblDailyRate.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0";
                            //LblCurrency.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                            ViewState["dailyrate"] = LblDailyRate.Text;
                            ViewState["dailyratecurr"] = LblCurrency.Text;
                            // if (DailyRateValue.indexOf("~") >= 0) {
                            //var DailyRate = DailyRateValue.split('~');
                            //var DailyRate1 = parseFloat(isNaN(DailyRate[0]) || $.trim(DailyRate[0]) == '' ? '0.0' : (DailyRate[0] * $.trim(NoOfDays)));
                            // LblDailyRate.Text = DailyRate;
                            GetDeviationAmtCurr();
                        }
                        else if (DDLTrClaimCountry.SelectedValue.Trim() != "IN")
                        {
                            DailyRate = ServicesObj.GetExcRate(DDLTrClaimCountry.SelectedValue, DDLRegion.SelectedValue, DDLExpenseType.SelectedValue, Convert.ToDateTime(txtStartDate.Text));
                            if ((DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000") == " ")
                            {
                                ViewState["dailyrate"] = "";
                                LblDailyRate.Text = "";
                            }
                            else
                            {

                                LblDailyRate.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000";
                            }
                            if ((DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "") == " ")
                            {
                                ViewState["dailyratecurr"] = "";
                                LblCurrency.Text = "";
                            }
                            else
                            {

                                LblCurrency.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                            }
                            //LblDailyRate.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0";
                            //LblCurrency.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                            ViewState["dailyrate"] = LblDailyRate.Text;
                            ViewState["dailyratecurr"] = LblCurrency.Text;
                            // if (DailyRateValue.indexOf("~") >= 0) {
                            //var DailyRate = DailyRateValue.split('~');
                            //var DailyRate1 = parseFloat(isNaN(DailyRate[0]) || $.trim(DailyRate[0]) == '' ? '0.0' : (DailyRate[0] * $.trim(NoOfDays)));
                            // LblDailyRate.Text = DailyRate;
                            GetDeviationAmtCurr();
                        }
                        else
                        {
                            MsgCls("Please select Region !", lblIndent, Color.Red);
                        }

                    }
                    else
                    {
                        DailyRate = ServicesObj.GetExcRate(DDLTrClaimCountry.SelectedValue, DDLRegion.SelectedValue, DDLExpenseType.SelectedValue, Convert.ToDateTime(txtStartDate.Text));

                        if ((DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000") == " ")
                        {
                            ViewState["dailyrate"] = "";
                            LblDailyRate.Text = "";
                            GetDeviationAmtCurr();
                        }
                        else
                        {
                            string dailyratenodays = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000";
                            //LblDailyRate.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0";
                            LblDailyRate.Text = (decimal.Parse(dailyratenodays) * decimal.Parse(TxtNoOfDays.Text.Trim())).ToString();
                            LblCurrency.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                            ViewState["dailyrate"] = LblDailyRate.Text;
                            ViewState["dailyratecurr"] = LblCurrency.Text;
                            // if (DailyRateValue.indexOf("~") >= 0) {
                            //var DailyRate = DailyRateValue.split('~');
                            //var DailyRate1 = parseFloat(isNaN(DailyRate[0]) || $.trim(DailyRate[0]) == '' ? '0.0' : (DailyRate[0] * $.trim(NoOfDays)));
                            // LblDailyRate.Text = DailyRate;
                            GetDeviationAmtCurr();
                        }
                    }

                }
                else
                {

                    //if (DDLTrClaimCountry.SelectedValue == "0")
                    //{
                    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Expenditure Currency !')", true);
                    //    MsgCls("Please select Country !", lblIndent, Color.Red);

                    //}
                    //if (DDLRegion.SelectedValue == "0" || DDLRegion.SelectedValue == "")
                    //{
                    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Reimbursement Currency !')", true);
                    //    MsgCls("Please select Region !", lblIndent, Color.Red);

                    //}

                    //if (DDLExpenseType.SelectedValue == "0")
                    //{
                    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Reimbursement Currency !')", true);
                    //    MsgCls("Please select Expense Type !", lblIndent, Color.Red);

                    //}

                }
                //GetDeviationAmtCurr();
            }
            catch (Exception Ex)
            {

                if (Ex.Message.StartsWith("String"))
                {
                    DDLExpenseType.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please select Expense date before selecting Expense type!');", true);
                    txtStartDate.Focus();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
            }
        }

        protected void DDLTrClaimCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            if (DDLTrClaimCountry.SelectedValue.Trim() == "IN")
            {
                DDLRegion.Enabled = true;
                LoadRegion(DDLTrClaimCountry.SelectedValue.Trim());
                GetDailyRate();
                ////DDLRegion.Focus();
            }
            else
            {
                DDLRegion.SelectedValue = "0";
                DDLRegion.Enabled = false;
                GetDailyRate();
                ////DDLRegion.Focus();
            }

            //GetDailyRate();
            DDLTrClaimCountry.Focus();
        }

        protected void DDLRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            MsgCls("", lblIndent, Color.Transparent);
            fuAttachments = (FileUpload)Session["fuAttachments"];
            GetDailyRate();
            DDLRegion.Focus();
        }

        protected void DDLExpenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //4009	4019	4113	4125
            //company laundry
            // if (DDLExpenseType.SelectedValue == "4009" || DDLExpenseType.SelectedValue == "4019" || DDLExpenseType.SelectedValue == "4113" || DDLExpenseType.SelectedValue == "4125")
            if (DDLExpenseType.SelectedValue == "4009" || DDLExpenseType.SelectedValue == "4113")
            {
                if (int.Parse(ViewState["Dayscount"].ToString()) < 7)
                {
                    MsgCls("Laundry Expense is not allowed for < 7 days of trip!", lblIndent, Color.Red);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Laundry Expense is not allowed for < 7 days of trip!');", true);

                    DDLExpenseType.SelectedValue = "0";
                    DDLExpenseType.Focus();
                }
                else
                {
                    MsgCls("", lblIndent, Color.Transparent);
                    fuAttachments = (FileUpload)Session["fuAttachments"];
                    GetDailyRate();
                    DDLExpenseType.Focus();
                }
            }
            else
            {
                MsgCls("", lblIndent, Color.Transparent);
                fuAttachments = (FileUpload)Session["fuAttachments"];
                GetDailyRate();
                DDLExpenseType.Focus();
            }
        }


        public void GetDeviationAmtCurr()
        {
            try
            {
                string ExchangeRate2 = "";
                if ((ViewState["dailyrate"].ToString() != "") && (txtExpenditureAmount.Text.ToString() != "") && (DDLExpenditureCurrency.SelectedValue != ""))
                {
                    if ((decimal.Parse(ViewState["dailyrate"].ToString())) >= 0)
                    {
                        if (ViewState["dailyratecurr"].ToString().Trim() == DDLExpenditureCurrency.SelectedValue.ToString())
                        {

                            if ((decimal.Parse(ViewState["dailyrate"].ToString())) > decimal.Parse(txtExpenditureAmount.Text.ToString()))
                            {
                                LblDeviation.Text = "0.000";
                                LblCurrency1.Text = "";
                                HF_Deviation.Value = "0.000";
                                HF_DeCurr.Value = "";
                            }
                            else
                            {
                                LblDeviation.Text = (decimal.Parse(txtExpenditureAmount.Text.ToString()) - (decimal.Parse(ViewState["dailyrate"].ToString()))).ToString("0.000");
                                LblCurrency1.Text = ViewState["dailyratecurr"].ToString();
                                HF_Deviation.Value = LblDeviation.Text;
                                HF_DeCurr.Value = LblCurrency1.Text;
                            }
                        }
                        else
                        {

                            OtherReimbursementsbl objbl = new OtherReimbursementsbl();
                            OtherReimbursementCollectionbo objLst = objbl.Load_ExchangeRate(DDLExpenditureCurrency.SelectedValue.ToString(), ViewState["dailyratecurr"].ToString().Trim());
                            foreach (OtherReimbursementsbo objBo in objLst)
                            {

                                ExchangeRate2 = decimal.Parse(objBo.UKURS.ToString()).ToString();



                                //  ExchangeRate2 = txtExchangeRate.Text.ToString().Trim();


                            }

                            if (ExchangeRate2 == "")
                            {
                                MsgCls("Exchange Rate Cannot be empty!", lblIndent, Color.Red);
                                LblDeviation.Text = "0.000";
                                HF_Deviation.Value = LblDeviation.Text;
                                HF_DeCurr.Value = "";
                                LblCurrency1.Text = "";
                            }
                            else
                            {
                                if ((decimal.Parse(ExchangeRate2)) > 0)
                                {
                                    if ((decimal.Parse(ViewState["dailyrate"].ToString())) > ((decimal.Parse(ExchangeRate2)) * (decimal.Parse(txtExpenditureAmount.Text.ToString()))))
                                    {
                                        LblDeviation.Text = "0.000";
                                        LblCurrency1.Text = "";
                                        HF_Deviation.Value = "0.000";
                                        HF_DeCurr.Value = "";
                                    }

                                    else
                                    {
                                        LblDeviation.Text = (((decimal.Parse(ExchangeRate2)) * (decimal.Parse(txtExpenditureAmount.Text.ToString()))) - (decimal.Parse(ViewState["dailyrate"].ToString()))).ToString("0.000");
                                        LblCurrency1.Text = ViewState["dailyratecurr"].ToString();
                                        HF_Deviation.Value = LblDeviation.Text;
                                        HF_DeCurr.Value = LblCurrency1.Text;
                                    }
                                }
                                else
                                {
                                    if ((decimal.Parse(ViewState["dailyrate"].ToString())) > ((decimal.Parse(txtExpenditureAmount.Text.ToString())) / Math.Abs((decimal.Parse(ExchangeRate2)))))
                                    {
                                        LblDeviation.Text = "0.000";
                                        LblCurrency1.Text = "";
                                        HF_Deviation.Value = "0.000";
                                        HF_DeCurr.Value = "";
                                    }

                                    else
                                    {
                                        LblDeviation.Text = (((decimal.Parse(txtExpenditureAmount.Text.ToString())) / Math.Abs((decimal.Parse(ExchangeRate2)))) - (decimal.Parse(ViewState["dailyrate"].ToString()))).ToString("0.000");
                                        LblCurrency1.Text = ViewState["dailyratecurr"].ToString();
                                        HF_Deviation.Value = LblDeviation.Text;
                                        HF_DeCurr.Value = LblCurrency1.Text;
                                    }
                                }
                            }

                        }
                    }

                    else
                    {
                        LblDeviation.Text = "0.000";
                        LblDailyRate.Text = "0.000";
                        HF_DailyRate.Value = LblDailyRate.Text;
                        HF_Deviation.Value = LblDeviation.Text;
                        HF_DeCurr.Value = "";
                        LblCurrency.Text = "";
                        LblCurrency1.Text = "";
                    }
                }
                else
                {
                    LblDeviation.Text = "0.000";
                    LblDailyRate.Text = "0.000";
                    LblCurrency1.Text = "";
                    LblCurrency.Text = "";
                    HF_Deviation.Value = LblDeviation.Text;
                    HF_DeCurr.Value = LblCurrency1.Text;
                    HF_DailyRate.Value = LblDailyRate.Text;

                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void TxtNoOfDays_TextChanged(object sender, EventArgs e)
        {
            try
            {

                fuAttachments = (FileUpload)Session["fuAttachments"];
                GetDailyRate();
                ////TxtNoOfDays.Focus();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }



        private void Search()
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;
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

                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                    TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsNew_forClaims(User.Identity.Name, SelectedType, textSearch);
                    if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                    {
                        MsgCls("No Records found", LblMsg, Color.Red);
                        GV_TravelReqUpdate.Visible = false;
                        GV_TravelReqUpdate.DataSource = null;
                        GV_TravelReqUpdate.DataBind();
                        return;
                    }
                    else
                    {
                        MsgCls("", LblMsg, Color.Transparent);
                        GV_TravelReqUpdate.Visible = true;
                        GV_TravelReqUpdate.DataSource = TrvlReqboList;
                        GV_TravelReqUpdate.SelectedIndex = -1;
                        GV_TravelReqUpdate.DataBind();
                        //GV_TravelClaimReqAppRej.Visible = false;
                        PnlExpenseAdd.Visible = false;

                    }

                }

            }
            ////catch (Exception Ex)
            ////{
            ////    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
            ////    //MsgCls(Ex.Message, LblMsg, Color.Red);
            ////    MsgCls("Please enter valid data", LblMsg, System.Drawing.Color.Red);
            ////}
            catch (Exception Ex)
            {

                switch (Ex.Message)
                {


                    case "-05":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Since the Trip details has been Updated, the Trip has to be Approved by Manager')", true);
                        // MsgCls("Approvals are missing!", LblMsg, Color.Red);
                        //PageLoadEvents();
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        break;
                }
                //MsgCls(Ex.Message, LblMsg, Color.Red);
            }

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            try
            {

                Search();
                //MsgCls(string.Empty, LblMsg, Color.Transparent);
                //string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                //string textSearch = txtsearch.Text;


                //travelrequestbl travelrequestblObj = new travelrequestbl();
                //List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                //TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsNew(User.Identity.Name, SelectedType, textSearch);
                //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //{
                //    MsgCls("No Records found", LblMsg, Color.Red);
                //    GV_TravelReqUpdate.Visible = false;
                //    GV_TravelReqUpdate.DataSource = null;
                //    GV_TravelReqUpdate.DataBind();
                //    return;
                //}
                //else
                //{
                //    MsgCls("", LblMsg, Color.Transparent);
                //    GV_TravelReqUpdate.Visible = true;
                //    GV_TravelReqUpdate.DataSource = TrvlReqboList;
                //    GV_TravelReqUpdate.SelectedIndex = -1;
                //    GV_TravelReqUpdate.DataBind();
                //    //GV_TravelClaimReqAppRej.Visible = false;
                //    grdAdvance.Visible = false;

                //}


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

            PageLoadEvents();
            //  GV_TravelClaimReqAppRej.Visible = false;
            //grdAdvance.Visible = false;
            PnlExpenseAdd.Visible = false;
            MsgCls("", LblMsg, Color.Transparent);
        }

        protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////DDLReimbursementCurrency.Focus();
            ddlTask.Focus();
        }
        protected void cbEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEdit.Checked)
                txtExchangeRate1.Enabled = true;
            else
                txtExchangeRate1.Enabled = false;
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetDailyRate();
                txtStartDate.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            dvlineitem.Visible = true;
            txtStartDate.Focus();
        }

        protected void lbtAddNew_Click(object sender, EventArgs e)
        {
            ViewState["TR_CLAIM"] = null;
            GV_TravelExpReq.DataSource = null;
            GV_TravelExpReq.DataBind();
            ClearClaimLineItemssubmit();
            DDLReimbursementCurrency.Enabled = true;
            DDLReimbursementCurrency.SelectedValue = "0";
            ddlTask.Enabled = true;
            ddlTask.SelectedValue = "0";
            lblMessageBoard.Text = "";

            lbtnEdit.Visible = false;
            lbtAddNew.Visible = false;

            //---------------------------------
            ltTask.Text = "NA";
            //ltProject.Text = "NA";
            ltReimbAmt.Text = "NA";
            //---------------------------------
        }
    }
}
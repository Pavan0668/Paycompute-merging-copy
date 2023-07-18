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
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;

namespace iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment
{
    public partial class SavedTravelClaims : System.Web.UI.Page
    {
        WebService.Service ServicesObj = new WebService.Service();
        protected MembershipUser memUser;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //LoadTravelClaimGridView();
                PageLoadEvents();
                LoadReimCurrencyTypes();
                LoadExpenditureCurrency();
                LoadCountry();
                divSearch.Visible = true;


                if (Request.QueryString["NC"] != null)
                {
                    if (Request.QueryString["NC"] == "E")
                    {
                        if (Session["CID"] != null)
                        {
                            CopyTC(Session["CID"].ToString(), Session["REINR"].ToString());
                            goto displayInfo;
                        }
                    }
                    else if (Request.QueryString["NC"] == "N")
                    {
                        Session["CID"] = null;
                        Session.Clear();
                    }
                }
            }
            Loadfileupload();

        displayInfo:
            {
                ////Console.WriteLine("");
            }

            MembershipUser mu = Membership.GetUser(User.Identity.Name);
            string userEmail = mu.Email.ToString();

        }


        void CopyTC(string CID1, string REINR)
        {

            PnlExpenseAdd1.Visible = true;


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

            ////lttripdates.Text = TrvlReqboList1[0].DATV1.ToString() + "-" + TrvlReqboList1[0].DATB1.ToString();

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
            //string Task = TrvlReqboList[0].ACTIVITY == null ? "" : TrvlReqboList[0].ACTIVITY.ToString().Trim();//grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["ACTIVITY"].ToString();
            //if (Task == "Billable")
            //{
            //    Task = "B";
            //}
            //else
            //{
            //    Task = "NB";
            //}

            string Task = TrvlReqboList[0].TASK == null ? "" : TrvlReqboList[0].TASK.ToString().Trim();


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

            DataTable dt = ConvertToDataTable(TrvlReqboList);
            decimal d = 0;
            decimal total = dt.AsEnumerable()
                     .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
            GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";


            GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            GV_TravelExpReq.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (Rccur) + ")";
            int totalRowsCount = GV_TravelExpReq.Rows.Count;
            ViewState["totalRowsCount"] = totalRowsCount;



            ViewState["TripNO"] = TrvlReqboList1[0].REINR == null ? "" : TrvlReqboList1[0].REINR.ToString().Trim();//grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
            ViewState["PROJID"] = TrvlReqboList[0].PRJID == null ? "" : TrvlReqboList[0].PRJID.ToString().Trim();//grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRJID"].ToString();
            ViewState["WBS_ELEMT"] = TrvlReqboList1[0].WBS_ELEMT == null ? "" : TrvlReqboList1[0].WBS_ELEMT.ToString().Trim();//grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();

            ltClaimID.Text = CID.ToString();
            ltTask.Text = ddlTask.SelectedItem.Text;
            //ltProject.Text = ddlProjectCode.SelectedItem.Text;
            ltReimbAmt.Text = GV_TravelExpReq.FooterRow.Cells[5].Text;


            MsgCls("", lblIndent, Color.Transparent);
            DDLReimbursementCurrency.Enabled = false;
            ddlTask.Enabled = false;

            divSearch.Visible = false;
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

        private void PageLoadEvents()
        {
            try
            {
                travelrequestcolumnsbo TrvlBO = new travelrequestcolumnsbo();
                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                TrvlReqboList = travelrequestblObj.Get_TravelReqDetailsSaved(User.Identity.Name);
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


        //#region  IExpense Types Empty DataTable

        //private DataTable GetTrvlClaim()
        //{
        //    try
        //    {
        //        DataTable Dt = new DataTable("TR_CLAIM");
        //        //Dt.Columns.Add("CID", typeof(int));
        //        Dt.Columns.Add("LID", typeof(int));
        //        Dt.Columns.Add("EXP_TYPE", typeof(string));
        //        Dt.Columns.Add("EXP_TYPE_NAME", typeof(string));
        //        Dt.Columns.Add("S_DATE", typeof(DateTime));
        //        Dt.Columns.Add("NO_DAYS", typeof(string));
        //        Dt.Columns.Add("DAILY_RATE", typeof(string));
        //        Dt.Columns.Add("EXPT_AMT", typeof(string));
        //        Dt.Columns.Add("EXPT_CURR", typeof(string));
        //        Dt.Columns.Add("EXC_RATE", typeof(string));
        //        Dt.Columns.Add("RE_AMT", typeof(string));
        //        Dt.Columns.Add("JUSTIFY", typeof(string));
        //        Dt.Columns.Add("RECEIPT_FILE", typeof(string));
        //        Dt.Columns.Add("RECEIPT_FIID", typeof(string));
        //        Dt.Columns.Add("RECEIPT_FPATH", typeof(string));
        //        Dt.Columns.Add("ZLAND", typeof(string));
        //        Dt.Columns.Add("ZORT1", typeof(string));
        //        Dt.Columns.Add("DEVIATION_AMT", typeof(string));
        //        Dt.Columns.Add("DEVIATION_CURR", typeof(string));
        //        Dt.Columns.Add("RCURR", typeof(string));
        //        Dt.Columns.Add("DAILYCURR", typeof(string));

        //        return Dt;
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //        return null;
        //    }
        //}
        //#endregion
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
        private void LoadTravelClaimGridView(string Tripno)
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();

                TrvlReqboList1 = travelrequestblObj.Get_SavedTravelClaimForDetails(User.Identity.Name, Tripno);
                grdSavedTravelClaims.DataSource = TrvlReqboList1;
                grdSavedTravelClaims.DataBind();



                TrvlReqboList.AddRange(TrvlReqboList1);
                Session.Add("TravelIexpGrdInfo", TrvlReqboList);
                if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                {
                    MsgCls("No Records Found !", LblMsg, Color.Red);
                    grdSavedTravelClaims.Visible = false;
                    grdSavedTravelClaims.DataSource = null;
                    return;
                }
                else
                {
                    grdSavedTravelClaims.Visible = true;
                    grdSavedTravelClaims.DataSource = TrvlReqboList;
                }
                grdSavedTravelClaims.DataBind();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void GV_TravelReqUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
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
                        ViewState["FrmtripDate"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString();
                        ViewState["TotripDate"] = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString();

                        Fromdate1 = DateTime.Parse(GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString());
                        Todate1 = DateTime.Parse(GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString());


                        TimeSpan ts = Todate1 - Fromdate1;
                        int days = ts.Days;
                        ViewState["Dayscount"] = days;

                        LoadTravelClaimGridView(ViewState["@REINR"].ToString());
                        divSearch.Visible = false;
                        break;
                    default:
                        break;
                }
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
        private void LoadDDLExpenseType(string schema, string pernr)
        {

            mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Expensetype_travel(schema, pernr);
            DDLExpenseType.DataSource = objLst;
            DDLExpenseType.DataTextField = "SPTXT";
            DDLExpenseType.DataValueField = "SPKZL";
            DDLExpenseType.DataBind();
            DDLExpenseType.Items.Insert(0, new ListItem("- SELECT -", "0"));
            DDLExpenseType.SelectedValue = "0";
        }

        protected void grdSavedTravelClaims_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":


                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in grdSavedTravelClaims.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }
                        PnlExpenseAdd1.Visible = true;
                        ////PnlExpenseAdd1.Enabled = true;

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
                        string Task = grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["ACTIVITY"].ToString();
                        if (Task == "Billable")
                        {
                            Task = "B";
                        }
                        else
                        {
                            Task = "NB";
                        }

                        ddlTask.SelectedValue = Task;
                        string Rccur = grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();

                        DDLReimbursementCurrency.SelectedValue = grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();
                        // CCD_DDLReimbursementCurrency.Enabled = false;
                        int CID = int.Parse(grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                        ViewState["CIDforSubmit"] = CID;
                        travelrequestbl travelrequestblObj = new travelrequestbl();
                        List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();



                        TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CID);
                        GV_TravelExpReq.DataSource = TrvlReqboList;
                        GV_TravelExpReq.DataBind();

                        DataTable dt = ConvertToDataTable(TrvlReqboList);
                        decimal d = 0;
                        decimal total = dt.AsEnumerable()
                                 .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                        // decimal total = dt.AsEnumerable().Sum(row => row.Field<string>("RE_AMT"));
                        //GridView1.FooterRow.Cells[1].Text = "Total";
                        //GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        //GridView1.FooterRow.Cells[2].Text = total.ToString("N2");
                        // var gridView = GV_TravelExpReq as GridView;
                        //var dataSource = gridView.DataSource as IEnumerable<YourDataObject>;
                        //e.Row.Cells[3].Text = dataSource.Sum(item => item.YourProperty).ToString();
                        GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";


                        GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        GV_TravelExpReq.FooterRow.Cells[5].Text = total.ToString("#,##0.00") + "(" + (Rccur) + ")";
                        int totalRowsCount = GV_TravelExpReq.Rows.Count;
                        ViewState["totalRowsCount"] = totalRowsCount;
                        //if (totalRowsCount > 0)
                        //{
                        //    //CCD_DDLReimbursementCurrency.Enabled = false;
                        //    CCD_DDLReimbursementCurrency.SelectedValue = grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();
                        //    CCD_DDLReimbursementCurrency.Enabled = false;
                        //}
                        //else
                        //{

                        //    CCD_DDLReimbursementCurrency.SelectedValue = grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();
                        //    CCD_DDLReimbursementCurrency.Enabled = true;
                        //}
                        //TrvlReqboList = travelrequestblObj.Load_ClaimDetails_forDatable(CID);
                        //List<TrvlReqDetails> lst = TrvlReqboList;
                        //DataTable dt = ConvertToDataTable(lst);


                        ViewState["TripNO"] = grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                        ViewState["PROJID"] = grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRJID"].ToString();
                        ViewState["WBS_ELEMT"] = grdSavedTravelClaims.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();

                        MsgCls("", lblIndent, Color.Transparent);
                        DDLReimbursementCurrency.Enabled = false;
                        ddlTask.Enabled = false;
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
        protected void grdSavedTravelClaims_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdSavedTravelClaims.PageIndex = e.NewPageIndex;

            LoadTravelClaimGridView(ViewState["@REINR"].ToString());
            grdSavedTravelClaims.SelectedIndex = -1;
        }

        protected void grdSavedTravelClaims_Sorting(object sender, GridViewSortEventArgs e)
        {
            travelrequestbl travelrequestblObj = new travelrequestbl();
            List<TrvlReqDetails> TrvlReqboList = (List<TrvlReqDetails>)Session["TravelIexpGrdInfo"];
            bool objSortOrder = (bool)Session["bSortedOrder"];
            switch (e.SortExpression.ToString().Trim())
            {
                case "CID":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.CID.Value.CompareTo(objBo2.CID.Value)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.CID.Value.CompareTo(objBo1.CID.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "REINR":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return ((long.Parse(objBo1.REINR)).CompareTo(long.Parse(objBo2.REINR))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return ((long.Parse(objBo2.REINR)).CompareTo(long.Parse(objBo1.REINR))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;


                case "WBS_ELEMT":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.WBS_ELEMT.ToString().CompareTo(objBo2.WBS_ELEMT.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.WBS_ELEMT.ToString().CompareTo(objBo1.WBS_ELEMT.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CREATED_BY":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return ((int.Parse(objBo1.CREATED_BY)).CompareTo(int.Parse(objBo2.CREATED_BY))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return ((int.Parse(objBo2.CREATED_BY)).CompareTo(int.Parse(objBo1.CREATED_BY))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "ENAME":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "ACTIVITY":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.ACTIVITY.ToString().CompareTo(objBo2.ACTIVITY.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.ACTIVITY.ToString().CompareTo(objBo1.ACTIVITY.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "RCURR":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.RCURR.ToString().CompareTo(objBo2.RCURR.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.RCURR.ToString().CompareTo(objBo1.RCURR.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "STATUS":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.STATUS.ToString().CompareTo(objBo2.STATUS.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.STATUS.ToString().CompareTo(objBo1.STATUS.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "RE_AMT":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return ((decimal.Parse(objBo1.RE_AMT)).CompareTo(decimal.Parse(objBo2.RE_AMT))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return ((decimal.Parse(objBo2.RE_AMT)).CompareTo(decimal.Parse(objBo1.RE_AMT))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CREATED_ON":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.CREATED_ON.Value.CompareTo(objBo2.CREATED_ON.Value)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.CREATED_ON.Value.CompareTo(objBo1.CREATED_ON.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }

                    break;

            }

            grdSavedTravelClaims.DataSource = TrvlReqboList;
            grdSavedTravelClaims.DataBind();
            Session.Add("TravelIexpGrdInfo", TrvlReqboList);

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
                        MsgCls("", lblIndent, Color.Transparent);

                        btnAdd.Visible = false;
                        btnUpdateItems.Visible = true;

                        ClearClaimLineItems();
                        int index = Convert.ToInt32(e.CommandArgument);
                        ViewState["RowIndex"] = index;
                        int LID = int.Parse(GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());
                        ViewState["LIDtoAdd"] = LID;

                        string EXP_TYPE = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXP_TYPE"].ToString();
                        string S_DATE = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["S_DATE"].ToString();
                        string EXPT_AMT = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPT_AMT"].ToString();
                        string EXPT_CURR = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPT_CURR"].ToString();
                        string EXC_RATE = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXC_RATE"].ToString();
                        string RE_AMT = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RE_AMT"].ToString();
                        string RCURR = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();

                        string DAILY_RATE = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["DAILY_RATE"].ToString();
                        string DAILY_CURR = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["DAILY_CURR"].ToString();

                        string DEVIATION_AMT = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["DEVIATION_AMT"].ToString();
                        string DEVIATION_CURR = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["DEVIATION_CURR"].ToString();
                        string JUSTIFY = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["JUSTIFY"].ToString();
                        string RECEIPT_FILE = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FILE"].ToString();
                        string CountryID = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["CountryID"].ToString();
                        string RegoinID = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RegoinID"].ToString();
                        string EXPID = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPID"].ToString();
                        string NO_DAYS = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["NO_DAYS"].ToString();
                        string Receipt_fid = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FID"].ToString();
                        string Receipt_path = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FPATH"].ToString();
                        ViewState["Receiptfileid"] = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FID"].ToString();

                        ViewState["Receiptfilepath"] = GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FPATH"].ToString();


                        DDLExpenseType.SelectedValue = EXPID;

                        txtStartDate.Text = (DateTime.Parse(S_DATE)).ToString();
                        txtExpenditureAmount.Text = EXPT_AMT;
                        DDLExpenditureCurrency.SelectedValue = EXPT_CURR;
                        txtExchangeRate.Text = EXC_RATE;
                        LblReimbursableAmount.Text = Convert.ToDecimal(RE_AMT).ToString("#,##0.00");//// RE_AMT;
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
                        if (CountryID.Trim() == "IN")
                        {
                            LoadRegion(CountryID.Trim());
                            DDLRegion.Enabled = true;
                            DDLRegion.SelectedValue = RegoinID.Trim();
                        }
                        else
                        {
                            DDLRegion.SelectedValue = "0";
                            DDLRegion.Enabled = false;
                        }
                        // DDLRegion.SelectedValue = RegoinID.Trim();
                        LblDailyRate.Text = DAILY_RATE;
                        LblCurrency.Text = DAILY_CURR;
                        TxtNoOfDays.Text = NO_DAYS;
                        LblDeviation.Text = DEVIATION_AMT;
                        LblCurrency1.Text = DEVIATION_CURR;
                        //   travelrequestbl travelrequestblObj1 = new travelrequestbl();
                        //  List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();
                        //  TrvlReqDetails ObjTrvlReq = new TrvlReqDetails(); 

                        //  TrvlReqboList1 = travelrequestblObj1.Load_ClaimDetails_forDatable(int.Parse(ViewState["CIDforSubmit"].ToString()),LID);
                        //  DDLExpenseType.SelectedValue =ObjTrvlReq.EXP_TYPE.Trim();
                        //  CE_txtStartDate.SelectedDate = ObjTrvlReq.S_DATE;
                        //  txtExpenditureAmount.Text = ObjTrvlReq.EXPT_CURR;
                        //  CCD_DDLExpenditureCurrency.SelectedValue =  ObjTrvlReq.EXPT_CURR;
                        //  txtExchangeRate.Text = ObjTrvlReq.EXC_RATE;
                        //  LblReimbursableAmount.Text = ObjTrvlReq.RE_AMT;
                        //  LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;
                        //  HF_ReimbursAmnt.Value = ObjTrvlReq.RE_AMT;
                        //  txtJustification.Text =  ObjTrvlReq.JUSTIFY;
                        //  if (RECEIPT_FILE == "NO")
                        //  {
                        //      cb.Checked = false;
                        //  }
                        //  else
                        //  {
                        //      cb.Checked = true;
                        //  }
                        //  CCD_DDLTrClaimCountry.SelectedValue =  ObjTrvlReq.ZLAND.Trim();
                        //  CCD_DDLRegion.SelectedValue = ObjTrvlReq.ZORT1.Trim();
                        //  LblDailyRate.Text = ObjTrvlReq.DAILY_RATE;
                        //  TxtNoOfDays.Text = ObjTrvlReq.NO_DAYS;
                        //  LblDeviation.Text = ObjTrvlReq.DEVIATION_AMT;
                        //  LblCurrency1.Text = ObjTrvlReq.DEVIATION_CURR;
                        //  LblCurrency.Text= ObjTrvlReq.DAILY_CURR;
                        ////  fuAttachmentsfname.Text= ObjTrvlReq.RECEIPT_FID;
                        // // fuAttachments. = ObjTrvlReq.RECEIPT_FPATH;
                        MsgCls("", lblIndent, Color.Transparent);
                        GV_TravelExpReq.Columns[13].Visible = false;
                        break;

                    case "DELETEFILE":
                        bool? statusf = true;
                        decimal? CalcReAmtf = 0;
                        string reamtcurrf = "";
                        int CIDDeleteFile = int.Parse(ViewState["CIDforSubmit"].ToString());
                        int LIDDeletefile = int.Parse(GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());

                        travelrequestbl ObjTrvlf = new travelrequestbl();
                        TrvlReqDetails ObjTrvlReqf = new TrvlReqDetails();

                        ObjTrvlReqf.CID = CIDDeleteFile;
                        ObjTrvlReqf.LID = LIDDeletefile;


                        ObjTrvlf.DeleteFileFromSaveTravelClaim(ObjTrvlReqf, ref statusf);
                        if (statusf == false)
                        {
                            MsgCls("File deleted successfully !", lblIndent, Color.Green);
                        }
                        travelrequestbl travelrequestblObjf = new travelrequestbl();
                        List<TrvlReqDetails> TrvlReqboListf = new List<TrvlReqDetails>();

                        TrvlReqboListf = travelrequestblObjf.Load_SavedClaimDetails(CIDDeleteFile, ref CalcReAmtf, ref reamtcurrf);
                        GV_TravelExpReq.DataSource = TrvlReqboListf;
                        GV_TravelExpReq.DataBind();

                        DDLReimbursementCurrency.Enabled = false;
                        ddlTask.Enabled = false;
                        ClearClaimLineItems();
                        GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                        GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        // GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmtf + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";

                        GV_TravelExpReq.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmtf.ToString()).ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                        //TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CIDDelete);
                        //GV_TravelExpReq.DataSource = TrvlReqboList;
                        //GV_TravelExpReq.DataBind();
                        //int totalRowsCount = GV_TravelExpReq.Rows.Count;
                        //ViewState["totalRowsCount"] = totalRowsCount;

                        break;

                    case "DELETE":

                        try
                        {
                            bool? status = true;
                            decimal? CalcReAmt = 0;
                            string reamtcurr = "";
                            int CIDDelete = int.Parse(ViewState["CIDforSubmit"].ToString());
                            int LIDdelete = int.Parse(GV_TravelExpReq.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());

                            travelrequestbl ObjTrvl = new travelrequestbl();
                            TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();

                            ObjTrvlReq.CID = CIDDelete;
                            ObjTrvlReq.LID = LIDdelete;


                            ObjTrvl.DeleteSaveTravelClaim(ObjTrvlReq, ref status);
                            if (status == false)
                            {
                                MsgCls("Travel claims deleted successfully !", lblIndent, Color.Green);
                            }
                            travelrequestbl travelrequestblObj = new travelrequestbl();
                            List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                            TrvlReqboList = travelrequestblObj.Load_SavedClaimDetails(CIDDelete, ref CalcReAmt, ref reamtcurr);
                            GV_TravelExpReq.DataSource = TrvlReqboList;
                            GV_TravelExpReq.DataBind();

                            if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                            {
                                LoadReimCurrencyTypes();
                                DDLReimbursementCurrency.Enabled = true;
                                ddlTask.Enabled = true;

                                ClearClaimLineItems();
                                //GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                                //GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                                //// GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmt + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                                //GV_TravelExpReq.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                                //TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CIDDelete);
                                //GV_TravelExpReq.DataSource = TrvlReqboList;
                                //GV_TravelExpReq.DataBind();
                                int totalRowsCount = GV_TravelExpReq.Rows.Count;
                                ViewState["totalRowsCount"] = totalRowsCount;
                            }
                            else
                            {
                                DDLReimbursementCurrency.Enabled = false;
                                ddlTask.Enabled = false;
                                ClearClaimLineItems();
                                GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                                GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                                // GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmt + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                                GV_TravelExpReq.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                                //TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CIDDelete);
                                //GV_TravelExpReq.DataSource = TrvlReqboList;
                                //GV_TravelExpReq.DataBind();
                                int totalRowsCount = GV_TravelExpReq.Rows.Count;
                                ViewState["totalRowsCount"] = totalRowsCount;
                            }


                            ////// ClearClaimLineItems();
                            ////// GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                            ////// GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            //////// GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmt + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                            ////// GV_TravelExpReq.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                            ////// //TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CIDDelete);
                            ////// //GV_TravelExpReq.DataSource = TrvlReqboList;
                            ////// //GV_TravelExpReq.DataBind();
                            ////// int totalRowsCount = GV_TravelExpReq.Rows.Count;
                            ////// ViewState["totalRowsCount"] = totalRowsCount;


                            //if (totalRowsCount == 0)
                            //{
                            //    CCD_DDLReimbursementCurrency.Enabled = true;
                            //}

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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                travelrequestbl ObjTrvl = new travelrequestbl();
                TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                bool? status = false;
                decimal? CalcReAmt = 0;
                string reamtcurr = "";
                int? CountofLid = 0;

                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                DateTime StartDate = new DateTime(0001, 01, 01);
                decimal ExchangeRate = 0.0M;
                string DailyRate = ServicesObj.GetExcRate(DDLTrClaimCountry.SelectedValue, DDLRegion.SelectedValue, DDLExpenseType.SelectedValue, Convert.ToDateTime(txtStartDate.Text));
                //if (decimal.TryParse(ServicesObj.GetExchangeRate(DDLExpenditureCurrency.SelectedValue, DDLReimbursementCurrency.SelectedValue), out ExchangeRate))
                //{ }
                if (decimal.TryParse(txtExchangeRate.Text, out ExchangeRate))
                { }
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                if (DateTime.TryParse(txtStartDate.Text, out StartDate) && ddlTask.SelectedValue != "0")
                {
                    if (DDLTrClaimCountry.SelectedValue != "0")
                    {
                        if (((DDLTrClaimCountry.SelectedValue.Trim() == "IN") && (DDLRegion.SelectedValue != "" && DDLRegion.SelectedValue != "0")) || (DDLTrClaimCountry.SelectedValue.Trim() != "IN"))
                        {

                            int gvrowscount = int.Parse(ViewState["totalRowsCount"].ToString().Trim());
                            travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                            objTravelRequestDataContext.sp_Get_CountofLId_TravelclaimItems(int.Parse(ViewState["CIDforSubmit"].ToString()), ref CountofLid);
                            if (CountofLid != null)
                            {
                                CountofLid = CountofLid + 1;
                            }

                            else
                            {
                                CountofLid = 1;
                            }

                            ObjTrvlReq.CID = int.Parse(ViewState["CIDforSubmit"].ToString());
                            ObjTrvlReq.LID = CountofLid;
                            ObjTrvlReq.EXP_TYPE = DDLExpenseType.SelectedValue;
                            ObjTrvlReq.S_DATE = StartDate;
                            ObjTrvlReq.NO_DAYS = TxtNoOfDays.Text;
                            ObjTrvlReq.DAILY_RATE = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000";
                            ObjTrvlReq.EXPT_AMT = txtExpenditureAmount.Text;
                            ObjTrvlReq.EXPT_CURR = DDLExpenditureCurrency.SelectedValue;
                            ObjTrvlReq.EXC_RATE = txtExchangeRate.Text;
                            ObjTrvlReq.RE_AMT = (Math.Abs(ExchangeRate < 0 ? decimal.Parse(txtExpenditureAmount.Text) / ExchangeRate : decimal.Parse(txtExpenditureAmount.Text) * ExchangeRate)).ToString();
                            ObjTrvlReq.JUSTIFY = txtJustification.Text;
                            ObjTrvlReq.RECEIPT_FILE = cb.Checked ? "YES" : "NO";
                            ObjTrvlReq.RECEIPT_FID = fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "";
                            ObjTrvlReq.RECEIPT_FPATH = fuAttachments.HasFile ? "~/TravelDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "";
                            ObjTrvlReq.ZLAND = DDLTrClaimCountry.SelectedValue;
                            if (DDLTrClaimCountry.SelectedValue.Trim() == "IN")
                            {
                                ObjTrvlReq.ZORT1 = DDLRegion.SelectedValue;
                            }
                            else
                            {
                                ObjTrvlReq.ZORT1 = "";
                            }
                            //ObjTrvlReq.ZORT1 = DDLRegion.SelectedValue;
                            ObjTrvlReq.DEVIATION_AMT = HF_Deviation.Value;
                            ObjTrvlReq.DEVIATION_CURR = HF_DeCurr.Value;
                            ObjTrvlReq.DAILY_CURR = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                            if (fuAttachments.HasFile)
                            { fuAttachments.SaveAs(Server.MapPath("~/TravelDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName)); }

                            ObjTrvl.Travel_ClaimItems(ObjTrvlReq, ref status);
                            if (status == true)
                            {
                                MsgCls("Travel claims sent successfully !", LblMsg, Color.Green);
                                GV_TravelExpReq.Columns[13].Visible = true;
                            }
                            travelrequestbl travelrequestblObj = new travelrequestbl();
                            List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                            TrvlReqboList = travelrequestblObj.Load_SavedClaimDetails(int.Parse(ViewState["CIDforSubmit"].ToString()), ref CalcReAmt, ref reamtcurr);
                            GV_TravelExpReq.DataSource = TrvlReqboList;
                            GV_TravelExpReq.DataBind();

                            DDLReimbursementCurrency.Enabled = false;
                            ddlTask.Enabled = false;
                            ClearClaimLineItemsAdd();

                            GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                            GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            //GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmt + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                            GV_TravelExpReq.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";

                            ltTask.Text = ddlTask.SelectedItem.Text;
                            //ltProject.Text = ddlProjectCode.SelectedItem.Text;
                            ltReimbAmt.Text = GV_TravelExpReq.FooterRow.Cells[5].Text;
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
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Invalid Data');", true);
                }
                DDLExpenseType.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int? TravelClaim_ID = 0;
                decimal ClaimTotal = 0;
                decimal? CalcReAmt = 0;
                string reamtcurr = "";
                travelrequestbl ObjTrvl = new travelrequestbl();
                TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();

                //decimal TRamnt = 0;
                //if (ViewState["TR_CLAIM"] != null)
                //{
                //    using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                //    {
                //        TRamnt = decimal.Parse(Dt.AsEnumerable().Sum(x => double.Parse(x.Field<string>("RE_AMT"))).ToString("0.00"));

                //    }
                //}
                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                TrvlReqboList = travelrequestblObj.Load_SavedClaimDetails(int.Parse(ViewState["CIDforSubmit"].ToString()), ref CalcReAmt, ref reamtcurr);
                GV_TravelExpReq.FooterRow.Cells[4].Text = "Total";

                GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                GV_TravelExpReq.FooterRow.Cells[5].Text = CalcReAmt + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";

                if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                {
                    MsgCls("Please Add Atleast one Claim Item!", LblMsg, Color.Red);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please Add Atleast one Claim Item!');", true);
                }
                else
                {
                    travelrequestcolumnscollectionbo objList = ObjTrvl.Travel_ClaimTotalAmtNew(User.Identity.Name, ViewState["TripNO"].ToString().Trim(), DDLReimbursementCurrency.SelectedValue);
                    foreach (travelrequestcolumnsbo objColumnsB in objList)
                    {
                        ClaimTotal = decimal.Parse(objColumnsB.ClaimTotalAmount.ToString());
                    }
                    bool? status = false;
                    ObjTrvlReq.CID = int.Parse(ViewState["CIDforSubmit"].ToString());
                    ObjTrvlReq.REINR = ViewState["TripNO"].ToString().Trim();
                    ObjTrvlReq.WBS_ELEMT = ViewState["PROJID"].ToString().Trim();
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
                    ObjTrvlReq.STATUS = "AgainSaved";
                    ObjTrvlReq.TotalAmount = decimal.Parse(CalcReAmt.ToString()) + ClaimTotal;// ClaimTotal + Dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                    ObjTrvl.SaveTravelClaim(ObjTrvlReq, ref TravelClaim_ID, ref status);
                    TravelClaim_ID=TravelClaim_ID == 0 ? int.Parse(ViewState["CIDforSubmit"].ToString().Trim()) : TravelClaim_ID;
                    ltClaimID.Text = TravelClaim_ID.ToString();
                    if (status == true)
                    {

                        ////MsgCls("Travel Claim Saved Successfully", lblMessageBoard, Color.Green);
                        MsgCls("Travel Claim " + TravelClaim_ID + " Saved Successfully", lblMessageBoard, Color.Green);
                        string alert = "Travel Claim " + TravelClaim_ID + " Saved successfully !";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='Travel_Requests.aspx';", true);

                        PageLoadEvents();
                        grdSavedTravelClaims.Visible = false;
                        divSearch.Visible = false;
                        //if (ViewState["TR_CLAIM"] != null)
                        //{
                        //    using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                        //    {

                        //        for (int i = 0; i < Dt.Rows.Count; i++)
                        //        {
                        //            decimal d = 0;
                        //            // travelrequestbl ObjTrvl = new travelrequestbl();
                        //            //TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                        //            ObjTrvlReq.CID = TravelClaim_ID;
                        //            ObjTrvlReq.LID = int.Parse(Dt.Rows[i]["LID"].ToString());
                        //            ObjTrvlReq.EXP_TYPE = Dt.Rows[i]["EXP_TYPE"].ToString();
                        //            ObjTrvlReq.S_DATE = DateTime.Parse(Dt.Rows[i]["S_DATE"].ToString());
                        //            ObjTrvlReq.NO_DAYS = Dt.Rows[i]["NO_DAYS"].ToString();
                        //            ObjTrvlReq.DAILY_RATE = Dt.Rows[i]["DAILY_RATE"].ToString();
                        //            ObjTrvlReq.EXPT_AMT = Dt.Rows[i]["EXPT_AMT"].ToString();
                        //            ObjTrvlReq.EXPT_CURR = Dt.Rows[i]["EXPT_CURR"].ToString();
                        //            ObjTrvlReq.EXC_RATE = Dt.Rows[i]["EXC_RATE"].ToString();
                        //            ObjTrvlReq.RE_AMT = Dt.Rows[i]["RE_AMT"].ToString();
                        //            ObjTrvlReq.JUSTIFY = Dt.Rows[i]["JUSTIFY"].ToString();
                        //            ObjTrvlReq.RECEIPT_FILE = Dt.Rows[i]["RECEIPT_FILE"].ToString();
                        //            ObjTrvlReq.RECEIPT_FID = Dt.Rows[i]["RECEIPT_FIID"].ToString();
                        //            ObjTrvlReq.RECEIPT_FPATH = Dt.Rows[i]["RECEIPT_FPATH"].ToString();
                        //            ObjTrvlReq.ZLAND = Dt.Rows[i]["ZLAND"].ToString();
                        //            ObjTrvlReq.ZORT1 = Dt.Rows[i]["ZORT1"].ToString();
                        //            ObjTrvlReq.DEVIATION_AMT = Dt.Rows[i]["DEVIATION_AMT"].ToString();
                        //            ObjTrvlReq.DEVIATION_CURR = Dt.Rows[i]["DEVIATION_CURR"].ToString();
                        //            ObjTrvlReq.DAILY_CURR = Dt.Rows[i]["DAILYCURR"].ToString();
                        //            ObjTrvl.UpdateSavedReject_Travel_ClaimItems(ObjTrvlReq, ref status);
                        //            if (status == true)
                        //            {
                        //                MsgCls("Travel claims saved successfully !", LblMsg, Color.Green);

                        //            }

                        //        }


                        //    }
                        //}
                    }
                    //ViewState["TR_CLAIM"] = null;
                    //GV_TravelExpReq.DataSource = null;
                    //GV_TravelExpReq.DataBind();
                    ddlTask.SelectedValue = "0";
                    DDLReimbursementCurrency.SelectedValue = "0";
                    ClearClaimLineItems();
                    DDLReimbursementCurrency.Enabled = true;
                    DDLReimbursementCurrency.SelectedValue = "0";
                    ddlTask.Enabled = true;
                    ddlTask.SelectedValue = "0";
                    PnlExpenseAdd1.Visible = false;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int? TravelClaim_ID = int.Parse(ViewState["CIDforSubmit"].ToString());//0;

                // decimal ClaimTotal = 0;
                decimal ClaimTotal = 0;
                decimal? CalcReAmt = 0;
                string reamtcurr = "";
                travelrequestbl ObjTrvl = new travelrequestbl();
                TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                TrvlReqboList = travelrequestblObj.Load_SavedClaimDetails(int.Parse(ViewState["CIDforSubmit"].ToString()), ref CalcReAmt, ref reamtcurr);
                GV_TravelExpReq.FooterRow.Cells[4].Text = "Total";

                GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                GV_TravelExpReq.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";


                if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                {
                    MsgCls("Please Add Atleast one Claim Item!", LblMsg, Color.Red);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please Add Atleast one Claim Item!');", true);
                }
                else
                {

                    travelrequestcolumnscollectionbo objList = ObjTrvl.Travel_ClaimTotalAmtNew(User.Identity.Name, ViewState["TripNO"].ToString().Trim(), DDLReimbursementCurrency.SelectedValue);
                    foreach (travelrequestcolumnsbo objColumnsB in objList)
                    {
                        ClaimTotal = decimal.Parse(objColumnsB.ClaimTotalAmount.ToString());
                    }

                    //decimal TRamnt = 0;
                    //if (ViewState["TR_CLAIM"] != null)
                    //{
                    //    using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                    //    {
                    //        TRamnt = decimal.Parse(Dt.AsEnumerable().Sum(x => double.Parse(x.Field<string>("RE_AMT"))).ToString("0.00"));

                    //    }
                    //}

                    bool? status = false;

                    ObjTrvlReq.CID = int.Parse(ViewState["CIDforSubmit"].ToString());
                    ObjTrvlReq.REINR = ViewState["TripNO"].ToString().Trim();
                    ObjTrvlReq.WBS_ELEMT = ViewState["PROJID"].ToString().Trim();
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
                    ObjTrvlReq.TotalAmount = decimal.Parse(CalcReAmt.ToString()) + ClaimTotal;// ClaimTotal + Dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                    ObjTrvl.UpdateCreateTravelClaim(ObjTrvlReq, ref status);

                    if (status == true)
                    {
                        SendMailMethod(int.Parse(ViewState["CIDforSubmit"].ToString()), ViewState["WBS_ELEMT"].ToString(), ddlTask.SelectedValue, DDLReimbursementCurrency.SelectedValue);

                        PageLoadEvents();
                        grdSavedTravelClaims.Visible = false;
                        ////MsgCls("Travel Claim Submited Successfully ", lblMessageBoard, Color.Green);
                        MsgCls("Travel Claim " + TravelClaim_ID + " Submited Successfully ", lblMessageBoard, Color.Green);
                        string alert = "Travel Claim " + TravelClaim_ID + " Submited Successfully !";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + alert + "');window.location ='Travel_Requests.aspx';", true);


                        divSearch.Visible = true;
                        //if (ViewState["TR_CLAIM"] != null)
                        //{
                        //    using (DataTable Dt = (DataTable)ViewState["TR_CLAIM"])
                        //    {
                        //        int flag = 0;
                        //        for (int i = 0; i < Dt.Rows.Count; i++)
                        //        {
                        //            decimal d = 0;
                        //            // travelrequestbl ObjTrvl = new travelrequestbl();
                        //            //TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                        //            ObjTrvlReq.CID = TravelClaim_ID;
                        //            ObjTrvlReq.LID = i + 1;
                        //            ObjTrvlReq.EXP_TYPE = Dt.Rows[i]["EXP_TYPE"].ToString();
                        //            ObjTrvlReq.S_DATE = DateTime.Parse(Dt.Rows[i]["S_DATE"].ToString());
                        //            ObjTrvlReq.NO_DAYS = Dt.Rows[i]["NO_DAYS"].ToString();
                        //            ObjTrvlReq.DAILY_RATE = Dt.Rows[i]["DAILY_RATE"].ToString();
                        //            ObjTrvlReq.EXPT_AMT = Dt.Rows[i]["EXPT_AMT"].ToString();
                        //            ObjTrvlReq.EXPT_CURR = Dt.Rows[i]["EXPT_CURR"].ToString();
                        //            ObjTrvlReq.EXC_RATE = Dt.Rows[i]["EXC_RATE"].ToString();
                        //            ObjTrvlReq.RE_AMT = Dt.Rows[i]["RE_AMT"].ToString();
                        //            ObjTrvlReq.JUSTIFY = Dt.Rows[i]["JUSTIFY"].ToString();
                        //            ObjTrvlReq.RECEIPT_FILE = Dt.Rows[i]["RECEIPT_FILE"].ToString();
                        //            ObjTrvlReq.RECEIPT_FID = Dt.Rows[i]["RECEIPT_FIID"].ToString();
                        //            ObjTrvlReq.RECEIPT_FPATH = Dt.Rows[i]["RECEIPT_FPATH"].ToString();
                        //            ObjTrvlReq.ZLAND = Dt.Rows[i]["ZLAND"].ToString();
                        //            ObjTrvlReq.ZORT1 = Dt.Rows[i]["ZORT1"].ToString();
                        //            ObjTrvlReq.DEVIATION_AMT = Dt.Rows[i]["DEVIATION_AMT"].ToString();
                        //            ObjTrvlReq.DEVIATION_CURR = Dt.Rows[i]["DEVIATION_CURR"].ToString();
                        //            ObjTrvlReq.DAILY_CURR = Dt.Rows[i]["DAILYCURR"].ToString();
                        //            // ObjTrvlReq.TotalAmount = decimal.Parse(Dt.Rows[i]["RE_AMT"].ToString());// ClaimTotal + Dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);
                        //            //  ObjTrvlReq.TotalAmount =  Dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);


                        //            // ObjTrvl.Travel_ClaimItems(ObjTrvlReq, ref status);
                        //            if (status == true)
                        //            {
                        //                MsgCls("Travel claims sent successfully !", LblMsg, Color.Green);

                        //                flag = 1;

                        //            }

                        //        }
                        //        if (flag == 1)
                        //        {

                        //            //   SendMailMethod(ObjTrvlReq, TRamnt);
                        //            //  SendMailMethod(TravelClaim_ID, HF_REINR.Value.Split('&')[1], ddlTask.SelectedValue, DDLReimbursementCurrency.SelectedValue);


                        //        }

                        //    }
                        //}
                    }
                    //ViewState["TR_CLAIM"] = null;
                    //GV_TravelExpReq.DataSource = null;
                    //GV_TravelExpReq.DataBind();
                    ddlTask.SelectedValue = "0";
                    DDLReimbursementCurrency.SelectedValue = "0";
                    ClearClaimLineItems();
                    DDLReimbursementCurrency.Enabled = true;
                    DDLReimbursementCurrency.SelectedValue = "0";
                    ddlTask.Enabled = true;
                    ddlTask.SelectedValue = "0";
                    PnlExpenseAdd1.Visible = false;
                    LoadTravelClaimGridView(ViewState["@REINR"].ToString());
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
                        MsgCls("Approvals are missing!", LblMsg, Color.Red);
                        //PageLoadEvents();
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        break;
                }
                //MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }


        private void SendMailMethod(int? CID, string Project, string Task, string ReCurrency)
        {
            try
            {

                if (Task.Trim() == "B")
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

                    strSubject = "Travel Claim Request " + CID + "  has been Raised by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.";


                    RecipientsString = Approver_Email;
                    strPernr_Mail = EMP_Email;
                    //Preparing the mail body--------------------------------------------------
                    string body = "<b>Travel Claim Request has been Raised by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.<br/><br/></b>";
                    body += "<b>Entity with Claim ID  :  " + Entity + " : " + CID + "</b><br/><br/>";
                    body += "<b>Travel Claim Header Details :<hr /><br/>";

                    body += "Trip ID       :  " + ViewState["@REINR"].ToString() + "<br/>";
                    body += "Project       :  " + Project_code + " - " + Project + "<br/>";
                    body += "Task          :  " + Task + "<br/>";
                    body += "Total Current Claim Reimbursement Amount  :  " + ClaimTotal + "<br/>";
                    body += "Total Trip Claims Reimbursement Amount  :  " + decimal.Parse(TAmt).ToString("#,##0.00") + " ( " + ReCurrency + " ) <br/>";
                    // body += "Reimbursement Currency      :  " + ReCurrency + "<br/><br/>";
                    body += "<b>Travel Claim Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";


                    //    //End of preparing the mail body-------------------------------------------
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    // lblMessageBoard.Text = "Mail sent successfully.";

                }
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Travel Claim Submited Successfully. Error in sending mail";
                return;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void ClearClaimLineItems()
        {
            try
            {
                //DDLExpenseType.SelectedValue = "0";
                //txtStartDate.Text = string.Empty;
                //txtExpenditureAmount.Text = string.Empty;
                //CCD_DDLExpenditureCurrency.SelectedValue = "0";
                //txtExchangeRate.Text = string.Empty;
                //LblReimbursableAmount.Text = "0.0";
                //HF_ReimbursAmnt.Value = null;
                //txtJustification.Text = string.Empty;
                //cb.Checked = false;
                //LblDailyRate.Text = "0.0";
                //TxtNoOfDays.Text = "1";
                //CCD_DDLTrClaimCountry.SelectedValue = "0";
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


        private void ClearClaimLineItemsAdd()
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




                ////DDLExpenseType.SelectedValue = "0";
                ////txtStartDate.Text = string.Empty;
                ////txtExpenditureAmount.Text = string.Empty;
                ////CCD_DDLExpenditureCurrency.SelectedValue = "0";
                ////txtExchangeRate.Text = string.Empty;
                ////LblReimbursableAmount.Text = "0.0";
                ////HF_ReimbursAmnt.Value = null;
                ////txtJustification.Text = string.Empty;
                ////cb.Checked = false;
                ////LblDailyRate.Text = "0.0";
                ////TxtNoOfDays.Text = "1";
                ////CCD_DDLTrClaimCountry.SelectedValue = "0";
                //DDLExpenseType.SelectedValue = "0";
                //txtStartDate.Text = string.Empty;

                //txtExpenditureAmount.Text = string.Empty;
                //CCD_DDLExpenditureCurrency.SelectedValue = "0";
                //txtExchangeRate.Text = string.Empty;
                //LblReimbursableAmount.Text = "0.0";
                //HF_ReimbursAmnt.Value = null;
                //txtJustification.Text = string.Empty;
                //cb.Checked = false;
                //LblDailyRate.Text = "0.0";
                //TxtNoOfDays.Text = "1";
                //CCD_DDLTrClaimCountry.SelectedValue = "0";
                //LblCurrency.Text = "";
                //LblDeviation.Text = "0.0";
                //LblCurrency1.Text = "";
                //HF_DailyRate.Value = "0.0";
                //HF_DeCurr.Value = "";
                //HF_Deviation.Value = "0.0";
                //Session["fuAttachments"] = null;
                //fuAttachmentsfname.Text = string.Empty;
                //txtExchangeRate1.Text = string.Empty;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void GV_TravelExpReq_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void DDLCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            txtExchangeRate.Text = string.Empty;
            LblReimbursableAmount.Text = string.Empty;
            LblReimbursableCurrency.Text = string.Empty;
            GetExchangeRate();
            txtStartDate.Focus();
        }

        protected void txtExpenditureAmount_TextChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            txtExchangeRate.Text = string.Empty;
            LblReimbursableAmount.Text = string.Empty;
            LblReimbursableCurrency.Text = string.Empty;
            GetExchangeRate();
            GetDailyRate();
            GetDeviationAmtCurr();
            txtJustification.Focus();

        }

        protected void ddlExpenditureCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            txtExchangeRate.Text = string.Empty;
            LblReimbursableAmount.Text = string.Empty;
            LblReimbursableCurrency.Text = string.Empty;
            GetExchangeRate();
            GetDailyRate();
            GetDeviationAmtCurr();
            DDLExpenditureCurrency.Focus();
        }

        protected void txtExchangeRate1_TextChanged(object sender, EventArgs e)
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

                            LblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); //(Math.Round(ramt, 3)).ToString();
                            //lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            LblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); //(Math.Round(ramt, 3)).ToString();
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
            txtExchangeRate1.Focus();

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

                    if (DDLExpenditureCurrency.SelectedValue == "0")
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Expenditure Currency !')", true);
                        MsgCls("Please select Expenditure Currency !", lblIndent, Color.Red);

                    }
                    if (DDLReimbursementCurrency.SelectedValue == "0")
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Reimbursement Currency !')", true);
                        MsgCls("Please select Reimbursement Currency !", lblIndent, Color.Red);

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

                            LblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); //(Math.Round(ramt, 3)).ToString();
                            LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;
                            //lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            LblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); //(Math.Round(ramt, 3)).ToString();
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

                            LblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); //(Math.Round(ramt, 3)).ToString();
                            LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;
                            //lblReimbursableAmount.Text = ramt.ToString();
                            MsgCls("", lblIndent, Color.White);
                        }
                        else
                        {
                            ramt = ExpenditureAmount * Math.Abs(ExchangeRate);
                            LblReimbursableAmount.Text = Convert.ToDecimal(Math.Round(ramt, 3)).ToString("#,##0.00"); //(Math.Round(ramt, 3)).ToString();
                            // lblReimbursableAmount.Text = ramt.ToString();
                            LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;
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
                LblReimbursableAmount.Text = Convert.ToDecimal(ramt).ToString("#,##0.00");

                LblReimbursableCurrency.Text = DDLReimbursementCurrency.SelectedValue;

            }
            else
            {
                ramt = ExpenditureAmount1 * Math.Abs(ExchangeRate1);
                //lblReimbursableAmount.Text = (Math.Round(ramt, 2)).ToString();
                LblReimbursableAmount.Text = Convert.ToDecimal(ramt).ToString("#,##0.00");
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void DDLTrClaimCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fuAttachments = (FileUpload)Session["fuAttachments"];
            if (DDLTrClaimCountry.SelectedValue.Trim() == "IN")
            {
                DDLRegion.Enabled = true;

                LoadRegion(DDLTrClaimCountry.SelectedValue.Trim());
                // DDLRegion.SelectedValue = "0";
                GetDailyRate();
                DDLTrClaimCountry.Focus();
            }
            else
            {
                // DDLRegion.SelectedValue = "0";
                LoadRegion(DDLTrClaimCountry.SelectedValue.Trim());
                DDLRegion.Enabled = false;
                GetDailyRate();
                DDLTrClaimCountry.Focus();
            }
            // GetDailyRate();
            DDLTrClaimCountry.Focus();
        }

        protected void DDLRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MsgCls("", LblMsg, Color.Transparent);
            fuAttachments = (FileUpload)Session["fuAttachments"];
            GetDailyRate();
            MsgCls("", LblMsg, Color.Transparent);
            MsgCls("", lblIndent, Color.Transparent);
            DDLRegion.Focus();


        }

        protected void DDLExpenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fuAttachments = (FileUpload)Session["fuAttachments"];
            //GetDailyRate();
            //4009	4019	4113	4125
            //company laundry
            //  if (DDLExpenseType.SelectedValue == "4009" || DDLExpenseType.SelectedValue == "4019" || DDLExpenseType.SelectedValue == "4113" || DDLExpenseType.SelectedValue == "4125")

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
                        if (ViewState["dailyratecurr"].ToString() == DDLExpenditureCurrency.SelectedValue.ToString())
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
                                //MsgCls("", lblIndent, Color.White);
                                ExchangeRate2 = decimal.Parse(objBo.UKURS.ToString()).ToString();
                                //   objBo.UKURS.ToString();

                            }

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

                    else
                    {
                        LblDeviation.Text = "0.000";
                        LblDailyRate.Text = "0.000";
                        HF_DailyRate.Value = LblDailyRate.Text;
                        HF_Deviation.Value = LblDeviation.Text;
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
                DDLTrClaimCountry.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnUpdateItems_Click(object sender, EventArgs e)
        {
            try
            {
                travelrequestbl ObjTrvl = new travelrequestbl();
                TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                DateTime StartDate = new DateTime(0001, 01, 01);
                decimal ExchangeRate = 0.0M;
                decimal? CalcReAmt = 0;
                string reamtcurr = "";
                bool? status = false;
                string fileupdate = string.Empty;

                string DailyRate = ServicesObj.GetExcRate(DDLTrClaimCountry.SelectedValue, DDLRegion.SelectedValue, DDLExpenseType.SelectedValue, Convert.ToDateTime(txtStartDate.Text));
                if (decimal.TryParse(txtExchangeRate.Text, out ExchangeRate))
                { }
                if (DateTime.TryParse(txtStartDate.Text, out StartDate) && ddlTask.SelectedValue != "0")
                {
                    if (DDLTrClaimCountry.SelectedValue != "0")
                    {

                        if (((DDLTrClaimCountry.SelectedValue.Trim() == "IN") && (DDLRegion.SelectedValue != "" && DDLRegion.SelectedValue != "0")) || (DDLTrClaimCountry.SelectedValue.Trim() != "IN"))
                        {
                            int rowindex = int.Parse(ViewState["RowIndex"].ToString());
                            int lid = int.Parse(ViewState["LIDtoAdd"].ToString());
                            ObjTrvlReq.CID = int.Parse(ViewState["CIDforSubmit"].ToString());
                            ObjTrvlReq.LID = lid;
                            ObjTrvlReq.EXP_TYPE = DDLExpenseType.SelectedValue;
                            ObjTrvlReq.S_DATE = StartDate;
                            ObjTrvlReq.NO_DAYS = TxtNoOfDays.Text;
                            ObjTrvlReq.DAILY_RATE = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000";
                            ObjTrvlReq.EXPT_AMT = txtExpenditureAmount.Text;
                            ObjTrvlReq.EXPT_CURR = DDLExpenditureCurrency.SelectedValue;
                            ObjTrvlReq.EXC_RATE = txtExchangeRate.Text;
                            ObjTrvlReq.RE_AMT = (Math.Abs(ExchangeRate < 0 ? decimal.Parse(txtExpenditureAmount.Text) / ExchangeRate : decimal.Parse(txtExpenditureAmount.Text) * ExchangeRate)).ToString();
                            ObjTrvlReq.JUSTIFY = txtJustification.Text;
                            ObjTrvlReq.RECEIPT_FILE = cb.Checked ? "YES" : "NO";
                            if (fuAttachments != null)
                            {
                                if (fuAttachments.HasFile)
                                {
                                    ObjTrvlReq.RECEIPT_FID = fuAttachments.HasFile ? fuAttachments.PostedFile.FileName : "";
                                    ObjTrvlReq.RECEIPT_FPATH = fuAttachments.HasFile ? "~/TravelDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fuAttachments.FileName) : "";
                                    if (fuAttachments.HasFile)
                                    { fuAttachments.SaveAs(Server.MapPath("~/TravelDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fuAttachments.FileName)); }

                                    fileupdate = "UpdateFileUpload";
                                }
                            }
                            else
                            {
                                ObjTrvlReq.RECEIPT_FID = ViewState["Receiptfileid"].ToString();
                                ObjTrvlReq.RECEIPT_FPATH = ViewState["Receiptfilepath"].ToString();
                                fileupdate = "UpdateNoFileUpload";
                            }
                            ObjTrvlReq.ZLAND = DDLTrClaimCountry.SelectedValue;
                            if (DDLTrClaimCountry.SelectedValue.Trim() == "IN")
                            {
                                ObjTrvlReq.ZORT1 = DDLRegion.SelectedValue;
                            }
                            else
                            {
                                ObjTrvlReq.ZORT1 = "";
                            }
                            ObjTrvlReq.DEVIATION_AMT = HF_Deviation.Value;
                            ObjTrvlReq.DEVIATION_CURR = HF_DeCurr.Value;
                            ObjTrvlReq.DAILY_CURR = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";

                            ObjTrvl.UpdateSavedReject_Travel_ClaimItems(ObjTrvlReq, ref status, fileupdate);
                            if (status == true)
                            {
                                MsgCls("Travel claims updated successfully !", LblMsg, Color.Green);
                                GV_TravelExpReq.Columns[13].Visible = true;
                            }
                            travelrequestbl travelrequestblObj = new travelrequestbl();
                            List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                            TrvlReqboList = travelrequestblObj.Load_SavedClaimDetails(int.Parse(ViewState["CIDforSubmit"].ToString()), ref CalcReAmt, ref reamtcurr);
                            GV_TravelExpReq.DataSource = TrvlReqboList;
                            GV_TravelExpReq.DataBind();

                            DDLReimbursementCurrency.Enabled = false;
                            ddlTask.Enabled = false;
                            //ClearClaimLineItems();
                            ClearClaimLineItemsAdd();

                            GV_TravelExpReq.FooterRow.Cells[4].Text = "Total : ";

                            GV_TravelExpReq.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            GV_TravelExpReq.FooterRow.Cells[5].Text = decimal.Parse(CalcReAmt.ToString()).ToString("#,##0.00") + "(" + (DDLReimbursementCurrency.SelectedValue) + ")";
                            ClearClaimLineItemsAdd();
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
                    //}

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Invalid Data');", true);
                }


                btnAdd.Visible = true;
                btnUpdateItems.Visible = false;
                DDLExpenseType.Focus();

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

                    TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsNew_forSaved(User.Identity.Name, SelectedType, textSearch);
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
                        grdSavedTravelClaims.Visible = false;
                        PnlExpenseAdd1.Visible = false;

                    }
                }


            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                //MsgCls(Ex.Message, LblMsg, Color.Red);
                MsgCls("Please enter valid data", LblMsg, System.Drawing.Color.Red);
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
            grdSavedTravelClaims.Visible = false;
            PnlExpenseAdd1.Visible = false;
            MsgCls("", LblMsg, Color.Transparent);
        }

        protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLReimbursementCurrency.Focus();
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
    }
}
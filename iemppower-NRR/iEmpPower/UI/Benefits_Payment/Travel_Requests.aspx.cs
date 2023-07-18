using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using System.Threading;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class Travel_Requests : System.Web.UI.Page
    {
        int Ebtnclick = 1;
        int ETRbtnclick = 1;
        int MPbtnclick = 1;
        int MCbtnclick = 1;

        protected int PagerSz = 1;
        protected int PendingPageIndex = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LoadEmpPRRequestGridView();
                //LoadPRRequestGridView();
                //LoadPRRequestCompletedGridView();


                if (Request.QueryString["PC"] != null)
                {
                    if (Request.QueryString["PC"] == "R")
                    {
                        loadTab2();
                        goto displayInfo;
                    }
                    if (Request.QueryString["PC"] == "P")
                    {
                        loadTab3();
                        goto displayInfo;
                    }
                    else if (Request.QueryString["PC"] == "C")
                    {
                        loadTab4();
                        goto displayInfo;
                    }
                }
                Tab1.CssClass = "nav-link active p-2";
                MainView.ActiveViewIndex = 0;
                LoadTCEmpGrid(); //LoadTravelClaimGridView();
            displayInfo: { }
                if (Request.QueryString["PC"] == null)
                {
                    if (Session["_MainSearchValue"].ToString() != "")
                    {
                        txtsearch.Text = Session["_MainSearchValue"].ToString();
                        searchdetails();
                        Session["_MainSearchValue"] = "";
                    }
                }
                else if (Request.QueryString["PC"] == "P")
                {
                    if (Session["_MainSearchValue"].ToString() != "")
                    {
                        txtSearchMP.Text = Session["_MainSearchValue"].ToString();
                        searchdetailsMP();
                        Session["_MainSearchValue"] = "";
                    }
                }
                else if (Request.QueryString["PC"] == "C")
                {
                    if (Session["_MainSearchValue"].ToString() != "")
                    {
                        txtSearchMC.Text = Session["_MainSearchValue"].ToString();
                        searchdetailsMC();
                        Session["_MainSearchValue"] = "";
                    }
                }
                else if (Request.QueryString["PC"] == "R")
                {
                    if (Session["_MainSearchValue"].ToString() != "")
                    {
                        txtSearchTR.Text = Session["_MainSearchValue"].ToString();
                        searchdetailsTR();
                        Session["_MainSearchValue"] = "";
                    }
                }
                else
                {
                    Session["_MainSearchValue"] = "";
                }
            }
            int cnt = 0;
            foreach (GridViewRow row in grdAppRejTravelMP.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                if (ChkBoxRows.Checked == true)
                    cnt += 1;
            }
            pnlmassbtn.Visible = (cnt > 0) ? true : false;
        }

        #region Pending Populate pager
        private void PopulatePendingPager(int RecordCount, int currentPage, Repeater gId)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = PagerSz;

                //Calculate the Start and End Index of pages to be displayed.
                double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal(PagerSz));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
                endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
                if (currentPage > pagerSpan % 2)
                {
                    if (currentPage == 2)
                    { endIndex = 5; }
                    else
                    { endIndex = currentPage + 2; }
                }
                else
                { endIndex = (pagerSpan - currentPage) + 1; }

                if (endIndex - (pagerSpan - 1) > startIndex)
                { startIndex = endIndex - (pagerSpan - 1); }

                if (endIndex > pageCount)
                {
                    endIndex = pageCount;
                    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
                }

                //Add the First Page Button.
                if (currentPage > 1)
                { pages.Add(new ListItem("<<", "1")); }

                //Add the Previous Button.
                if (currentPage > 1)
                { pages.Add(new ListItem("<", (currentPage - 1).ToString())); }

                for (int i = startIndex; i <= endIndex; i++)
                { pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage)); }

                //Add the Next Button.
                if (currentPage < pageCount)
                { pages.Add(new ListItem(">", (currentPage + 1).ToString())); }

                //Add the Last Button.
                if (currentPage != pageCount)
                { pages.Add(new ListItem(">>", pageCount.ToString())); }
                gId.DataSource = pages;
                gId.DataBind();

                //GV_ClockInClockOut.FooterRow.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;<b>Page " + currentPage + " of " + pageCount + "<b/>";

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        #endregion

        protected void Tab1_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "nav-link active p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link p-2";
            Tab4.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 0;
            LoadTCEmpGrid(); //LoadTravelClaimGridView();
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
            Tab4.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 1;
            //LoadPRRequestGridView();
            //LoadIExpenseMPGridView();
            LoadTREmpGrid(); //LoadTravelReqDetails();
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            loadTab3();
        }

        void loadTab3()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link active p-2";
            Tab4.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 2;
            //LoadPRRequestCompletedGridView();
            //LoadIExpenseMCGridView();
            LoadTCMgrPGrid(); //LoadTravelClaimMPGridView();

            int cnt = 0;
            foreach (GridViewRow row in grdAppRejTravelMP.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                if (ChkBoxRows.Checked == true)
                    cnt += 1;
            }
            pnlmassbtn.Visible = (cnt > 0) ? true : false;
        }

        protected void Tab4_Click(object sender, EventArgs e)
        {
            loadTab4();
        }

        void loadTab4()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link p-2";
            Tab4.CssClass = "nav-link active p-2";
            MainView.ActiveViewIndex = 3;
            //LoadPRRequestCompletedGridView();
            //LoadIExpenseMCGridView();
            LoadTCMgrCGrid(); //LoadTravelClaimMCGridView();
        }

        private void LoadTravelClaimGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();

                TrvlReqboList1 = travelrequestblObj.Get_TravelClaimForDetailsNew(User.Identity.Name);
                grdAppRejTravel.DataSource = TrvlReqboList1;
                grdAppRejTravel.DataBind();



                TrvlReqboList.AddRange(TrvlReqboList1);
                Session.Add("TravelIexpGrdInfo", TrvlReqboList);
                ////    }
                ////}


                //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdAppRejTravel.Visible = false;
                //    grdAppRejTravel.DataSource = null;
                //    //lblRemarks.Visible = false;
                //    //TxtRemarks.Visible = false;
                //    //btnApprove.Visible = false;
                //    //btnReject.Visible = false;
                //    return;
                //}
                //else
                //{
                    grdAppRejTravel.Visible = true;
                    grdAppRejTravel.DataSource = TrvlReqboList;
                    // grdAppRejTravel.SelectedIndex = -1;
                    //lblRemarks.Visible = true;
                    //TxtRemarks.Visible = true;
                    //btnApprove.Visible = true;
                    //btnReject.Visible = true;
                //}
                grdAppRejTravel.DataBind();

                //  PnlIExpDetalsView.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadTravelClaimGridView_AllCurrentLastmonth(string month)
        {
            try
            {
                int? recCnt = 0;
                PagerSz = Convert.ToInt32(ddlPagesizeEmp.SelectedItem.Text);
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();

                TrvlReqboList1 = travelrequestblObj.Get_TravelClaimForDetailsNew_AllCurrentLastmonth_Rpager(User.Identity.Name, month, PendingPageIndex, PagerSz, ref recCnt);
                grdAppRejTravel.DataSource = TrvlReqboList1;
                grdAppRejTravel.DataBind();



                TrvlReqboList.AddRange(TrvlReqboList1);
                Session.Add("TravelIexpGrdInfo", TrvlReqboList);
                ////    }
                ////}


                //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdAppRejTravel.Visible = false;
                //    grdAppRejTravel.DataSource = null;
                //    //lblRemarks.Visible = false;
                //    //TxtRemarks.Visible = false;
                //    //btnApprove.Visible = false;
                //    //btnReject.Visible = false;
                //    return;
                //}
                //else
                //{
                    grdAppRejTravel.Visible = true;
                    grdAppRejTravel.DataSource = TrvlReqboList;
                    // grdAppRejTravel.SelectedIndex = -1;
                    //lblRemarks.Visible = true;
                    //TxtRemarks.Visible = true;
                    //btnApprove.Visible = true;
                    //btnReject.Visible = true;
                //}
                grdAppRejTravel.DataBind();
                RptrPendingPager.Visible = TrvlReqboList1.Count <= 0 ? false : true;
                PopulatePendingPager(TrvlReqboList1.Count > 0 ? int.Parse(recCnt.ToString()) : 0, PendingPageIndex, RptrPendingPager);

                //  PnlIExpDetalsView.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadTravelReqDetails()
        {
            try
            {
                travelrequestcolumnsbo TrvlBO = new travelrequestcolumnsbo();
                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                TrvlReqboList = travelrequestblObj.Get_TravelReqDetails_forClaim(User.Identity.Name);
                GV_TravelReqUpdate.DataSource = TrvlReqboList;
                GV_TravelReqUpdate.DataBind();


                //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    GV_TravelReqUpdate.Visible = false;
                //    GV_TravelReqUpdate.DataSource = null;
                //    //lblRemarks.Visible = false;
                //    //TxtRemarks.Visible = false;
                //    //btnApprove.Visible = false;
                //    //btnReject.Visible = false;
                //    return;
                //}
                //else
                //{
                    GV_TravelReqUpdate.Visible = true;
                    GV_TravelReqUpdate.DataSource = TrvlReqboList;
                    // grdAppRejTravel.SelectedIndex = -1;
                    //lblRemarks.Visible = true;
                    //TxtRemarks.Visible = true;
                    //btnApprove.Visible = true;
                    //btnReject.Visible = true;
                //}
                GV_TravelReqUpdate.DataBind();

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        private void LoadTravelReqDetails_AllCurrentLastmonth(string month)
        {
            try
            {
                travelrequestcolumnsbo TrvlBO = new travelrequestcolumnsbo();
                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                TrvlReqboList = travelrequestblObj.Get_TravelReqDetails_forClaim_AllCurrentLastmonth(User.Identity.Name, month);
                GV_TravelReqUpdate.DataSource = TrvlReqboList;
                GV_TravelReqUpdate.DataBind();


                //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    GV_TravelReqUpdate.Visible = false;
                //    GV_TravelReqUpdate.DataSource = null;
                //    //lblRemarks.Visible = false;
                //    //TxtRemarks.Visible = false;
                //    //btnApprove.Visible = false;
                //    //btnReject.Visible = false;
                //    return;
                //}
                //else
                //{
                    GV_TravelReqUpdate.Visible = true;
                    GV_TravelReqUpdate.DataSource = TrvlReqboList;
                    // grdAppRejTravel.SelectedIndex = -1;
                    //lblRemarks.Visible = true;
                    //TxtRemarks.Visible = true;
                    //btnApprove.Visible = true;
                    //btnReject.Visible = true;
                //}
                GV_TravelReqUpdate.DataBind();

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        //private void LoadTravelReqDetails_AllCurrentLastmonth(string month)
        //{
        //    try
        //    {
        //        int? rCnt = 0;
        //        PagerSz = Convert.ToInt32(ddlPagesizeEmpTR.SelectedItem.Text);
        //        travelrequestcolumnsbo TrvlBO = new travelrequestcolumnsbo();
        //        travelrequestbl travelrequestblObj = new travelrequestbl();
        //        List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

        //        TrvlReqboList = travelrequestblObj.Get_TravelReqDetails_forClaim_AllCurrentLastmonth_Rpager(User.Identity.Name, month, PendingPageIndex, PagerSz, ref rCnt);
        //        GV_TravelReqUpdate.DataSource = TrvlReqboList;
        //        GV_TravelReqUpdate.DataBind();


        //        if (TrvlReqboList == null || TrvlReqboList.Count == 0)
        //        {
        //            MsgCls("No Records Found !", lblMessageBoard, Color.Red);
        //            GV_TravelReqUpdate.Visible = false;
        //            GV_TravelReqUpdate.DataSource = null;
        //            //lblRemarks.Visible = false;
        //            //TxtRemarks.Visible = false;
        //            //btnApprove.Visible = false;
        //            //btnReject.Visible = false;
        //            return;
        //        }
        //        else
        //        {
        //            GV_TravelReqUpdate.Visible = true;
        //            GV_TravelReqUpdate.DataSource = TrvlReqboList;
        //            // grdAppRejTravel.SelectedIndex = -1;
        //            //lblRemarks.Visible = true;
        //            //TxtRemarks.Visible = true;
        //            //btnApprove.Visible = true;
        //            //btnReject.Visible = true;
        //        }
        //        GV_TravelReqUpdate.DataBind();
        //        RepeatrTRAppPending.Visible = TrvlReqboList.Count <= 0 ? false : true;
        //        PopulatePendingPager(TrvlReqboList.Count > 0 ? int.Parse(rCnt.ToString()) : 0, PendingPageIndex, RepeatrTRAppPending);

        //    }
        //    catch (Exception Ex)
        //    { MsgCls(Ex.Message, LblMsg, Color.Red); }
        //}

        private void LoadTravelClaimMPGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();

                TrvlReqboList1 = travelrequestblObj.Get_TravelClaimForApprovalNew(User.Identity.Name);
                grdAppRejTravelMP.DataSource = TrvlReqboList1;
                grdAppRejTravelMP.DataBind();



                TrvlReqboList.AddRange(TrvlReqboList1);
                Session.Add("TravelIexpGrdInfo", TrvlReqboList);

                //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdAppRejTravelMP.Visible = false;
                //    grdAppRejTravelMP.DataSource = null;
                //    return;
                //}
                //else
                //{
                    grdAppRejTravelMP.Visible = true;
                    grdAppRejTravelMP.DataSource = TrvlReqboList;
                //}
                grdAppRejTravelMP.DataBind();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadTravelClaimMPGridView_AllCurrentLastmonth(string month)
        {
            try
            {

                int? recCnt = 0;
                PagerSz = Convert.ToInt32(ddlPagesizeMgrP.SelectedItem.Text);
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();

                TrvlReqboList1 = travelrequestblObj.Get_TravelClaimForApprovalNew_AllCurrentLastmonth_Rpager(User.Identity.Name, month, PendingPageIndex, PagerSz, ref recCnt);
                grdAppRejTravelMP.DataSource = TrvlReqboList1;
                grdAppRejTravelMP.DataBind();



                TrvlReqboList.AddRange(TrvlReqboList1);
                Session.Add("TravelIexpGrdInfo", TrvlReqboList);

                //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdAppRejTravelMP.Visible = false;
                //    grdAppRejTravelMP.DataSource = null;
                //    return;
                //}
                //else
                //{
                    grdAppRejTravelMP.Visible = true;
                    grdAppRejTravelMP.DataSource = TrvlReqboList;
                //}
                grdAppRejTravelMP.DataBind();
                RepeatrPRAppPending.Visible = TrvlReqboList1.Count <= 0 ? false : true;
                PopulatePendingPager(TrvlReqboList1.Count > 0 ? int.Parse(recCnt.ToString()) : 0, PendingPageIndex, RepeatrPRAppPending);

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadTravelClaimMCGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();

                TrvlReqboList1 = travelrequestblObj.Get_TravelClaimForApprovalNewMC(User.Identity.Name);
                grdAppRejTravelMC.DataSource = TrvlReqboList1;
                grdAppRejTravelMC.DataBind();



                TrvlReqboList.AddRange(TrvlReqboList1);
                Session.Add("TravelIexpGrdInfo", TrvlReqboList);

                //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdAppRejTravelMC.Visible = false;
                //    grdAppRejTravelMC.DataSource = null;
                //    return;
                //}
                //else
                //{
                    grdAppRejTravelMC.Visible = true;
                    grdAppRejTravelMC.DataSource = TrvlReqboList;
                //}
                grdAppRejTravelMC.DataBind();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadTravelClaimMCGridView_AllCurrentLastmonth(string month)
        {
            try
            {
                int? recCnt = 0;
                PagerSz = Convert.ToInt32(ddlPagesizeMgrC.SelectedItem.Text);
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();

                TrvlReqboList1 = travelrequestblObj.Get_TravelClaimForApprovalNewMC_AllCurrentLastmonth_Rpager(User.Identity.Name.ToString(), month, PendingPageIndex, PagerSz, ref recCnt);
                grdAppRejTravelMC.DataSource = TrvlReqboList1;
                grdAppRejTravelMC.DataBind();



                TrvlReqboList.AddRange(TrvlReqboList1);
                Session.Add("TravelIexpGrdInfo", TrvlReqboList);

                //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdAppRejTravelMC.Visible = false;
                //    grdAppRejTravelMC.DataSource = null;
                //    return;
                //}
                //else
                //{
                    grdAppRejTravelMC.Visible = true;
                    grdAppRejTravelMC.DataSource = TrvlReqboList;
                //}
                grdAppRejTravelMC.DataBind();
                RepetrCompl.Visible = TrvlReqboList1.Count <= 0 ? false : true;
                PopulatePendingPager(TrvlReqboList1.Count > 0 ? int.Parse(recCnt.ToString()) : 0, PendingPageIndex, RepetrCompl);

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

        protected void grdAppRejTravel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdAppRejTravel.PageIndex = e.NewPageIndex;

            LoadTCEmpGrid(); //LoadTravelClaimGridView();
            ////searchdetails();
            grdAppRejTravel.SelectedIndex = -1;
        }

        protected void grdAppRejTravel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DOWNLOAD":
                    //  string filename= grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FPATH"].ToString();
                    string filePath = e.CommandArgument.ToString();
                    //Response.ContentType = ContentType;
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                    Response.WriteFile(filePath);
                    Response.End();
                    break;

                case "COPY":
                    int rowIndex1 = Convert.ToInt32(e.CommandArgument);
                    int CID = int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                    string REINR1 = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                    Session["CID"] = CID;
                    Session["REINR"] = REINR1;
                    Response.Redirect("TravelClaimReq1.aspx?NC=" + "C");
                    break;

                case "EDIT":
                    int rowIndexEdit = Convert.ToInt32(e.CommandArgument);
                    int CIDEdit = int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                    string REINREdit = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                    Session["CID"] = CIDEdit;
                    Session["REINR"] = REINREdit;
                    Response.Redirect("SavedTravelClaims.aspx?NC=" + "E");
                    break;

                case "VIEW":

                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    ////foreach (GridViewRow row in grdAppRejTravel.Rows)
                    ////{
                    ////    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                    ////    System.Drawing.Color.LightGray :
                    ////    System.Drawing.Color.White;
                    ////}

                    ////PnlIExpDetalsView.Visible = true;
                    ////grdClaimDetails.Visible = true;
                    ////Exportbtn.Visible = true;
                    int CID3 = int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());


                    Session["CID"] = CID3;
                    Response.Redirect("TravelClaimHistoryStatusNew.aspx?NC=" + "C");

                    ////ViewState["CID"] = int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                    ////ViewState["CREATED_BY"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString();
                    ////ViewState["REINR"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                    ////ViewState["ENAME"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENAME"].ToString();
                    ////ViewState["WBS_ELEMT"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();
                    ////ViewState["ACTIVITY"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["ACTIVITY"].ToString();

                    ////ViewState["RE_AMT"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["RE_AMT"].ToString();

                    ////ViewState["RCURR"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();


                    ////travelrequestbl travelrequestblObj = new travelrequestbl();
                    ////List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                    ////TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CID);
                    ////grdClaimDetails.DataSource = TrvlReqboList;
                    ////grdClaimDetails.DataBind();

                    ////DataTable dt = ConvertToDataTable(TrvlReqboList);
                    ////decimal d = 0;
                    ////decimal total = dt.AsEnumerable()
                    ////         .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                    ////grdClaimDetails.FooterRow.Cells[7].Text = "Total : ";


                    ////grdClaimDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    ////grdClaimDetails.FooterRow.Cells[8].Text = total.ToString("#,##0.00") + "(" + (ViewState["RCURR"].ToString()) + ")";


                    ////TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(CID);

                    ////grdAppRejHistory.DataSource = TrvlReqboList;
                    ////grdAppRejHistory.DataBind();

                    ////ViewState["APPROVEDBY1"] = TrvlReqboList[0].APPROVED_BY1 == null ? "" : TrvlReqboList[0].APPROVED_BY1.ToString();
                    ////ViewState["APPROVEDBY2"] = TrvlReqboList[0].APPROVED_BY2 == null ? "" : TrvlReqboList[0].APPROVED_BY2.ToString();
                    ////ViewState["APPROVEDBY3"] = TrvlReqboList[0].APPROVED_BY3 == null ? "" : TrvlReqboList[0].APPROVED_BY3.ToString();
                    ////ViewState["APPROVEDBY4"] = TrvlReqboList[0].APPROVED_BY4 == null ? "" : TrvlReqboList[0].APPROVED_BY4.ToString();
                    ////ViewState["APPROVEDBY5"] = TrvlReqboList[0].APPROVED_BY5 == null ? "" : TrvlReqboList[0].APPROVED_BY5.ToString();
                    ////ViewState["APPROVEDBY6"] = TrvlReqboList[0].APPROVED_BY6 == null ? "" : TrvlReqboList[0].APPROVED_BY6.ToString();
                    ////ViewState["APPROVEDBY7"] = TrvlReqboList[0].APPROVED_BY7 == null ? "" : TrvlReqboList[0].APPROVED_BY7.ToString();
                    ////ViewState["APPROVEDBY8"] = TrvlReqboList[0].APPROVED_BY8 == null ? "" : TrvlReqboList[0].APPROVED_BY8.ToString();
                    ////ViewState["APPROVEDBY9"] = TrvlReqboList[0].APPROVED_BY9 == null ? "" : TrvlReqboList[0].APPROVED_BY9.ToString();

                    ////PnlIExpDetalsView.Visible = true;
                    break;
                case "STATUS":

                    int rowIndex2 = Convert.ToInt32(e.CommandArgument);

                    ////foreach (GridViewRow gvrow in grdAppRejTravel.Rows)
                    ////{
                    ////    gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex2) ?
                    ////    System.Drawing.Color.LightGray :
                    ////    System.Drawing.Color.White;
                    ////}

                    ////ViewPRIfo.Visible = true;
                    int CID2 = int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());

                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                    TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(CID2);
                    ltClaimid.Text = CID2.ToString();
                    grdAppRejHistory.DataSource = TrvlReqboList;
                    grdAppRejHistory.DataBind();

                    grdApprovalHistory.DataSource = null;
                    grdApprovalHistory.DataBind();


                    ModalPopupExtender1.Show();
                    break;

                default:
                    break;
            }
        }

        protected void grdAppRejTravel_Sorting(object sender, GridViewSortEventArgs e)
        {
            travelrequestbl travelrequestblObj = new travelrequestbl();
            //  List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

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


                //case "POST1":
                //    if (objSortOrder)
                //    {
                //        if (TrvlReqboList != null)
                //        {
                //            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                //            { return (objBo1.POST1.ToString().CompareTo(objBo2.POST1.ToString())); });
                //            objSortOrder = false;
                //            Session.Add("bSortedOrder", objSortOrder);
                //        }
                //    }
                //    else
                //    {
                //        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                //        { return (objBo2.POST1.ToString().CompareTo(objBo1.POST1.ToString())); });
                //        objSortOrder = true;
                //        Session.Add("bSortedOrder", objSortOrder);
                //    }
                //    break;
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
                //case "ENTITY":
                //    if (objSortOrder)
                //    {
                //        if (TrvlReqboList != null)
                //        {
                //            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                //            { return (objBo1.ENTITY.ToString().CompareTo(objBo2.ENTITY.ToString())); });
                //            objSortOrder = false;
                //            Session.Add("bSortedOrder", objSortOrder);
                //        }
                //    }
                //    else
                //    {
                //        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                //        { return (objBo2.ENTITY.ToString().CompareTo(objBo1.ENTITY.ToString())); });
                //        objSortOrder = true;
                //        Session.Add("bSortedOrder", objSortOrder);
                //    }
                //    break;

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

            grdAppRejTravel.DataSource = TrvlReqboList;
            grdAppRejTravel.DataBind();

            Session.Add("TravelIexpGrdInfo", TrvlReqboList);
        }

        protected void grdAppRejTravelMP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdAppRejTravelMP.PageIndex = e.NewPageIndex;

            LoadTCMgrPGrid(); //LoadTravelClaimMPGridView();
            ////SearchRecord();
            grdAppRejTravelMP.SelectedIndex = -1;
        }

        protected void grdAppRejTravelMP_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "VIEW":

                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    ////foreach (GridViewRow row in grdAppRejTravelMP.Rows)
                    ////{
                    ////    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                    ////    System.Drawing.Color.LightGray :
                    ////    System.Drawing.Color.White;
                    ////}
                    int CID = int.Parse(grdAppRejTravelMP.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());


                    Session["CID"] = CID;
                    Response.Redirect("TravelClaimAppRejNew.aspx?NC=" + "C");

                    break;
                case "STATUS":

                    int rowIndex2 = Convert.ToInt32(e.CommandArgument);

                    ////foreach (GridViewRow gvrow in grdAppRejTravelMP.Rows)
                    ////{
                    ////    gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex2) ?
                    ////    System.Drawing.Color.LightGray :
                    ////    System.Drawing.Color.White;
                    ////}

                    ////ViewPRIfo.Visible = true;
                    int CID2 = int.Parse(grdAppRejTravelMP.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());

                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                    TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(CID2);

                    ltClaimid.Text = CID2.ToString();
                    grdAppRejHistory.DataSource = TrvlReqboList;
                    grdAppRejHistory.DataBind();

                    grdApprovalHistory.DataSource = null;
                    grdApprovalHistory.DataBind();



                    ModalPopupExtender3.Show();
                    break;
                default:
                    break;
            }
        }

        protected void grdAppRejTravelMP_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void grdAppRejTravelMC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdAppRejTravelMC.PageIndex = e.NewPageIndex;

            LoadTCMgrCGrid(); //LoadTravelClaimMCGridView();
            ////SearchRecord();
            grdAppRejTravelMC.SelectedIndex = -1;
        }

        protected void grdAppRejTravelMC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "VIEW":

                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    ////foreach (GridViewRow row in grdAppRejTravelMC.Rows)
                    ////{
                    ////    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                    ////    System.Drawing.Color.LightGray :
                    ////    System.Drawing.Color.White;
                    ////}
                    int CID = int.Parse(grdAppRejTravelMC.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());


                    Session["CID"] = CID;
                    Response.Redirect("TravelClaimHistoryStatusNew.aspx?NC=" + "C");

                    break;

                case "STATUS":

                    int rowIndex2 = Convert.ToInt32(e.CommandArgument);

                    ////foreach (GridViewRow gvrow in grdAppRejTravelMC.Rows)
                    ////{
                    ////    gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex2) ?
                    ////    System.Drawing.Color.LightGray :
                    ////    System.Drawing.Color.White;
                    ////}

                    ////ViewPRIfo.Visible = true;
                    int CID2 = int.Parse(grdAppRejTravelMC.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());

                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                    TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(CID2);

                    ltClaimid.Text = CID2.ToString();
                    grdAppRejHistory.DataSource = TrvlReqboList;
                    grdAppRejHistory.DataBind();

                    grdApprovalHistory.DataSource = null;
                    grdApprovalHistory.DataBind();


                    ModalPopupExtender4.Show();
                    break;

                default:
                    break;
            }
        }

        protected void grdAppRejTravelMC_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void GV_TravelReqUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GV_TravelReqUpdate.PageIndex = e.NewPageIndex;
                //PageLoadEvents();
                LoadTREmpGrid(); //LoadTravelReqDetails();
                ////Search();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void GV_TravelReqUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "UPDATE":
                        string @REINR = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                        string @TripType = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["KZREA"].ToString();
                        string From = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["KUNDE"].ToString();
                        string To = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZORT1"].ToString();
                        string Country = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZLAND"].ToString();
                        string Project = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();
                        string TotalAdvance = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["SUM_ADVANC"].ToString();
                        string Fdate = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATV1"].ToString();
                        string Tdate = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["DATB1"].ToString();
                        string Addamt = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["ADDIT_AMNT"].ToString();
                        string Currency = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["CURRENCY"].ToString();
                        string Prjid = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString().Split(':')[0].Trim();
                        ViewState["ProjID"] = Prjid;

                        DateTime DtFrom;
                        DateTime DtTo;
                        using (TextBox TxtFrmDate = (TextBox)GV_TravelReqUpdate.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("TxtGvFrmDate"))
                        using (TextBox TxtToDate = (TextBox)GV_TravelReqUpdate.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("TxtGvToDate"))
                        //using (TextBox TxtAdvance = (TextBox)GV_TravelReqUpdate.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("TxtGvAdditionalAdvance"))
                        // using (DropDownList DDLCurrency = (DropDownList)GV_TravelReqUpdate.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLCurrency"))
                        {
                            if (!string.IsNullOrEmpty(TxtFrmDate.Text) && !string.IsNullOrEmpty(TxtToDate.Text))//// && !string.IsNullOrEmpty(TxtAdvance.Text))
                            {
                                if (DateTime.TryParse(TxtFrmDate.Text, out DtFrom))
                                {
                                    if (DateTime.TryParse(TxtToDate.Text, out DtTo))
                                    {
                                        if (DtTo >= DtFrom)
                                        {
                                            travelrequestbl ObjTrvl = new travelrequestbl();
                                            TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                                            ObjTrvlReq.PERNR = User.Identity.Name;
                                            ObjTrvlReq.REINR = @REINR;
                                            ObjTrvlReq.KZREA = @TripType;
                                            ObjTrvlReq.KUNDE = From;
                                            ObjTrvlReq.ZORT1 = To;
                                            ObjTrvlReq.ZLAND = Country;
                                            ObjTrvlReq.WBS_ELEMT = Project;
                                            ObjTrvlReq.SUM_ADVANC = decimal.Parse(TotalAdvance);
                                            ObjTrvlReq.DATV1 = DtFrom;
                                            ObjTrvlReq.DATB1 = DtTo;
                                            ObjTrvlReq.CURRENCY = Currency;//DDLCurrency.SelectedValue;
                                            ObjTrvlReq.ADDIT_AMNT = decimal.Parse("0.0");//decimal.Parse(TxtAdvance.Text);
                                            ObjTrvl.Update_TravelReq(ObjTrvlReq);
                                            MsgCls("Travel Request updated successfully and sent for Approval !", LblMsg, Color.Green);
                                            //SendMailMethod(ObjTrvlReq, Fdate, Tdate, Addamt, Currency);
                                            GV_TravelReqUpdate.EditIndex = -1;
                                            //PageLoadEvents();
                                            LoadTREmpGrid(); //LoadTravelReqDetails();
                                        }
                                        else
                                        { MsgCls("From Date-Time cannot be less than to Date-Time !", LblMsg, Color.Red); }
                                    }
                                    else
                                    { MsgCls("Invalid To Date-Time", LblMsg, Color.Red); }
                                }
                                else
                                { MsgCls("Invalid From Date-Time", LblMsg, Color.Red); }
                            }
                            else
                            { MsgCls("Invalid travel details !", LblMsg, Color.Red); }
                        }

                        break;


                    case "VIEW":
                        //string @REINR1 = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                        try
                        {
                            //travelrequestcolumnsbo TrvlBO = new travelrequestcolumnsbo();
                            //travelrequestbl travelrequestblObj = new travelrequestbl();
                            //List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                            //TrvlReqboList = travelrequestblObj.Get_TravelAdvanceDetails(REINR1, User.Identity.Name);


                            int rowIndex1 = Convert.ToInt32(e.CommandArgument);
                            string REINR1 = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                            Session["REINR"] = REINR1;
                            Response.Redirect("TravelClaimReq1.aspx?NC=" + "T");
                            break;


                        }
                        catch (Exception Ex)
                        { MsgCls(Ex.Message, LblMsg, Color.Red); }


                        break;

                    case "STATUS":

                        int rowIndex2 = Convert.ToInt32(e.CommandArgument);

                        ////////foreach (GridViewRow gvrow in grdAppRejTravelMC.Rows)
                        ////////{
                        ////////    gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex2) ?
                        ////////    System.Drawing.Color.LightGray :
                        ////////    System.Drawing.Color.White;
                        ////////}

                        ////////ViewPRIfo.Visible = true;
                        string REINR2 = GV_TravelReqUpdate.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();


                        travelrequestbl travelrequestblObj = new travelrequestbl();
                        List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                        TrvlReqboList = travelrequestblObj.Get_Traveldetails(REINR2);

                        grdApprovalHistory.DataSource = TrvlReqboList;
                        grdApprovalHistory.DataBind();

                        ltClaimid.Text = REINR2.ToString();
                        grdAppRejHistory.DataSource = null;
                        grdAppRejHistory.DataBind();



                        ModalPopupExtender2.Show();
                        break;

                    default:
                        break;
                }
            }
            //catch (Exception Ex)
            //{ 

            //    //MsgCls(Ex.Message, LblMsg, Color.Red);
            //}
            catch (Exception Ex)
            {

                switch (Ex.Message)
                {

                    case "-01":
                        MsgCls("Manager Perner is not there", LblMsg, Color.Red);
                        break;
                    case "-02":
                        MsgCls("Overlaps of date is not allowed", LblMsg, Color.Red);
                        break;
                    case "-03":
                        MsgCls("You haven't changed anything", LblMsg, Color.Red);
                        //PageLoadEvents();
                        break;
                    default:
                        MsgCls("Unknown error occured. Please contact your system administrator.<br/>" + Ex.Message, LblMsg, Color.Red);
                        break;
                }
                //MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }

        //private void SendMailMethod(TrvlReqDetails ObjTrvlReq, string Fdate, string Tdate, string Addamt, string Currency)
        //{
        //    try
        //    {
        //        string strSubject = string.Empty;
        //        string RecipientsString = string.Empty;
        //        string strPernr_Mail = string.Empty;
        //        string APPROVED_BY1 = "";
        //        string Approver_Name = "";
        //        string Approver_Email = "";
        //        string EMP_Name = "";
        //        string EMP_Email = "";

        //        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

        //        objTravelRequestDataContext.sp_Get_MailList_Travel(ObjTrvlReq.REINR, User.Identity.Name, ref APPROVED_BY1, ref Approver_Name, ref Approver_Email, ref EMP_Name, ref EMP_Email, ViewState["ProjID"].ToString());

        //        if ((DateTime.Parse(Fdate) != ObjTrvlReq.DATV1 || DateTime.Parse(Tdate) != ObjTrvlReq.DATB1) && (decimal.Parse(Addamt) != ObjTrvlReq.ADDIT_AMNT))
        //        {

        //            // strSubject = "Travel ID " + ObjTrvlReq.REINR + " From date, To date and Additional Amount has been Updated by " + EMP_Name + "  |  " + User.Identity.Name + " is pending for the Approval.";


        //            strSubject = " From date, To date and Additional Advance has been Updated by " + EMP_Name + "  |  " + User.Identity.Name + " to the existing trip " + ObjTrvlReq.REINR + " and is pending for the Approval.";
        //            RecipientsString = Approver_Email;
        //            strPernr_Mail = EMP_Email;

        //            //    //Preparing the mail body--------------------------------------------------

        //            string body = "Dear " + Approver_Name + " ,<br/><br/>";
        //            body += "Please be informed that <b>" + EMP_Name + " </b>has requested for <b>From Date " + ObjTrvlReq.DATV1 + "</b> , <b>To Date " + ObjTrvlReq.DATB1 + " </b>and <b>" + ObjTrvlReq.CURRENCY + "</b> to his existing trip <b>" + ObjTrvlReq.REINR + "</b><br/>";
        //            body += "Please approve the below request.<hr /><br/>";


        //            body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
        //            body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
        //            body += "<tr><td>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</td></tr>";
        //            body += "<tr><td>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</td></tr>";
        //            body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
        //            body += "<tr><td><b>From Date has been changed from </b></td><td> : </td><td><b> " + Fdate + " to " + ObjTrvlReq.DATV1 + "</b></td></tr>";
        //            body += "<tr><td><b>To Date  has been changed from </b></td><td> : </td><td><b> " + Tdate + " to " + ObjTrvlReq.DATB1 + "</b></td></tr>";
        //            body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
        //            body += "<tr><td><b>Additional Advance has been changed from </b></td><td> : </td><td><b> " + Addamt + " " + Currency + " to " + ObjTrvlReq.ADDIT_AMNT + " " + ObjTrvlReq.CURRENCY + "</b></td></tr></table>";
        //            //body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
        //            // body += "<tr><td><b>Currency has been changed from </b></td><td> : </td><td><b> " + Currency + " to " + ObjTrvlReq.CURRENCY + "</b></td></tr></table>";
        //            body += "<br/><br/>";
        //            //    //End of preparing the mail body-------------------------------------------
        //            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            //LblMsg.ForeColor = System.Drawing.Color.Green;
        //            //   LblMsg.Text = "Mail sent successfully.";
        //        }

        //        else if (DateTime.Parse(Fdate) != ObjTrvlReq.DATV1 || DateTime.Parse(Tdate) != ObjTrvlReq.DATB1)
        //        {
        //            strSubject = " From date and / or To date has been Updated by " + EMP_Name + "  |  " + User.Identity.Name + " to the existing trip " + ObjTrvlReq.REINR + " and is pending for the Approval.";
        //            RecipientsString = Approver_Email;
        //            strPernr_Mail = EMP_Email;

        //            //    //Preparing the mail body--------------------------------------------------
        //            string body = "Dear " + Approver_Name + " ,<br/><br/>";
        //            body += "Please be informed that <b>" + EMP_Name + " </b>has requested for <b>From Date " + ObjTrvlReq.DATV1 + "</b> and <b>To Date " + ObjTrvlReq.DATB1 + " </b>to his existing trip <b>" + ObjTrvlReq.REINR + "</b><br/>";
        //            body += "Please approve the below request.<hr /><br/>";

        //            body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
        //            body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
        //            body += "<tr><td>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</td></tr>";
        //            body += "<tr><td>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</td></tr>";
        //            body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
        //            body += "<tr><td><b>From Date has been changed from </b></td><td> : </td><td><b> " + Fdate + " to " + ObjTrvlReq.DATV1 + "</b></td></tr>";
        //            body += "<tr><td><b>To Date  has been changed from </b></td><td> : </td><td><b> " + Tdate + " to " + ObjTrvlReq.DATB1 + "</b></td></tr>";
        //            body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
        //            body += "<tr><td>Additional Advance </td><td> : </td><td> " + ObjTrvlReq.ADDIT_AMNT + " " + ObjTrvlReq.CURRENCY + "</td></tr></table>";
        //            //body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
        //            //body += "<tr><td>Currency </td><td> : </td><td> " + ObjTrvlReq.CURRENCY + "</td></tr></table>";
        //            body += "<br/><br/>";
        //            //    //End of preparing the mail body-------------------------------------------
        //            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            //  LblMsg.ForeColor = System.Drawing.Color.Green;
        //            //LblMsg.Text = "Mail sent successfully.";
        //        }

        //        else if (decimal.Parse(Addamt) != ObjTrvlReq.ADDIT_AMNT)
        //        {
        //            strSubject = "Additional Advance has been Requested by " + EMP_Name + "  |  " + User.Identity.Name + " to the existing trip " + ObjTrvlReq.REINR + " and is pending for the Approval.";
        //            RecipientsString = Approver_Email;
        //            strPernr_Mail = EMP_Email;

        //            //    //Preparing the mail body--------------------------------------------------

        //            string body = "Dear " + Approver_Name + " ,<br/><br/>";
        //            body += "Please be informed that <b>" + EMP_Name + " </b>has requested for <b>" + ObjTrvlReq.CURRENCY + "</b> to his existing trip <b>" + ObjTrvlReq.REINR + "</b><br/>";
        //            body += "Please approve the below request.<hr /><br/>";


        //            body += "<table><tr><td>Trip No </td> <td>: </td> <td>" + ObjTrvlReq.REINR + "</td></tr>";
        //            body += "<tr><td>Trip Type </td><td> : </td><td> " + ObjTrvlReq.KZREA + "</td></tr>";
        //            body += "<tr><td>From </td><td> : </td><td> " + ObjTrvlReq.KUNDE + "</td></tr>";
        //            body += "<tr><td>To </td><td> : </td><td> " + ObjTrvlReq.ZORT1 + "</td></tr>";
        //            body += "<tr><td>Country </td><td> : </td><td> " + ObjTrvlReq.ZLAND + "</td></tr>";
        //            body += "<tr><td>From Date </td><td> : </td><td> " + ObjTrvlReq.DATV1 + "</td></tr>";
        //            body += "<tr><td>To Date </td><td> : </td><td> " + ObjTrvlReq.DATB1 + "</td></tr>";
        //            body += "<tr><td>Project </td><td> : </td><td> " + ObjTrvlReq.WBS_ELEMT + "</td></tr>";
        //            body += "<tr><td><b>Additional Advance has been changed from </b></td><td> : </td><td><b> " + Addamt + " " + Currency + " to " + ObjTrvlReq.ADDIT_AMNT + " " + ObjTrvlReq.CURRENCY + "<b></td></tr></table>";
        //            //body += "<tr><td>Total Advance </td><td> : </td><td> " + ObjTrvlReq.SUM_ADVANC + "</td></tr>";
        //            //body += "<b><tr><td>Currency has been changed from </b></td><td> : </td><td><b> " + Currency + " to " + ObjTrvlReq.CURRENCY + "</b></td></tr></table>";
        //            body += "<br/><br/>";
        //            //    //End of preparing the mail body-------------------------------------------
        //            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
        //            //  LblMsg.ForeColor = System.Drawing.Color.Green;
        //            // LblMsg.Text = "Mail sent successfully.";
        //        }



        //    }
        //    catch
        //    {
        //        LblMsg.ForeColor = System.Drawing.Color.Red;
        //        LblMsg.Text = "Unknown error occured. Please contact your system administrator.";
        //        return;
        //    }
        //}

        protected void GV_TravelReqUpdate_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GV_TravelReqUpdate.EditIndex = e.NewEditIndex;
                //PageLoadEvents();
                LoadTREmpGrid(); //LoadTravelReqDetails();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void GV_TravelReqUpdate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GV_TravelReqUpdate.EditIndex = -1;
                //PageLoadEvents();
                LoadTREmpGrid(); //LoadTravelReqDetails();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void GV_TravelReqUpdate_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GV_TravelReqUpdate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        void LoadTCEmpGrid()
        {
            if (Ebtnclick == 1)
            {
                btnAll.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                LoadTravelClaimGridView_AllCurrentLastmonth("all");
            }

            else if (Ebtnclick == 2)
            {
                btnAll.CssClass = "btn btn-xs btn-light";
                btnCurrentMonth.CssClass = "btn btn-xs btn-secondary";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                LoadTravelClaimGridView_AllCurrentLastmonth("current");
            }

            else if (Ebtnclick == 3)
            {
                btnAll.CssClass = "btn btn-xs btn-light";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-secondary";

                LoadTravelClaimGridView_AllCurrentLastmonth("last");
            }
            else
            {
                btnAll.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                LoadTravelClaimGridView_AllCurrentLastmonth("all");
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            Ebtnclick = 1;
            LoadTCEmpGrid();
        }

        protected void btnCurrentMonth_Click(object sender, EventArgs e)
        {
            Ebtnclick = 2;
            LoadTCEmpGrid();
        }

        protected void btnLastMonth_Click(object sender, EventArgs e)
        {
            Ebtnclick = 3;
            LoadTCEmpGrid();
        }

        void LoadTREmpGrid()
        {
            if (ETRbtnclick == 1)
            {
                btnAllTR.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthTR.CssClass = "btn btn-xs btn-light";
                btnLastMonthTR.CssClass = "btn btn-xs btn-light";

                LoadTravelReqDetails_AllCurrentLastmonth("all");
            }

            else if (ETRbtnclick == 2)
            {
                btnAllTR.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthTR.CssClass = "btn btn-xs btn-secondary";
                btnLastMonthTR.CssClass = "btn btn-xs btn-light";

                LoadTravelReqDetails_AllCurrentLastmonth("current");
            }

            else if (ETRbtnclick == 3)
            {
                btnAllTR.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthTR.CssClass = "btn btn-xs btn-light";
                btnLastMonthTR.CssClass = "btn btn-xs btn-secondary";

                LoadTravelReqDetails_AllCurrentLastmonth("last");
            }
            else
            {
                btnAllTR.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthTR.CssClass = "btn btn-xs btn-light";
                btnLastMonthTR.CssClass = "btn btn-xs btn-light";

                LoadTravelReqDetails_AllCurrentLastmonth("all");
            }
        }

        protected void btnAllTR_Click(object sender, EventArgs e)
        {
            ETRbtnclick = 1;
            LoadTREmpGrid();
        }

        protected void btnCurrentMonthTR_Click(object sender, EventArgs e)
        {
            ETRbtnclick = 2;
            LoadTREmpGrid();
        }

        protected void btnLastMonthTR_Click(object sender, EventArgs e)
        {
            ETRbtnclick = 3;
            LoadTREmpGrid();
        }

        void LoadTCMgrPGrid()
        {
            if (MPbtnclick == 1)
            {
                btnAllMP.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                LoadTravelClaimMPGridView_AllCurrentLastmonth("all");
            }

            else if (MPbtnclick == 2)
            {
                btnAllMP.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-secondary";
                btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                LoadTravelClaimMPGridView_AllCurrentLastmonth("current");
            }

            else if (MPbtnclick == 3)
            {
                btnAllMP.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                btnLastMonthMP.CssClass = "btn btn-xs btn-secondary";

                LoadTravelClaimMPGridView_AllCurrentLastmonth("last");
            }
            else
            {
                btnAllMP.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                LoadTravelClaimMPGridView_AllCurrentLastmonth("all");
            }
        }

        protected void btnAllMP_Click(object sender, EventArgs e)
        {
            MPbtnclick = 1;
            LoadTCMgrPGrid();
        }

        protected void btnCurrentMonthMP_Click(object sender, EventArgs e)
        {
            MPbtnclick = 2;
            LoadTCMgrPGrid();
        }

        protected void btnLastMonthMP_Click(object sender, EventArgs e)
        {
            MPbtnclick = 3;
            LoadTCMgrPGrid();
        }

        void LoadTCMgrCGrid()
        {
            if (MCbtnclick == 1)
            {
                btnAllMC.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                LoadTravelClaimMCGridView_AllCurrentLastmonth("all");
            }

            else if (MCbtnclick == 2)
            {
                btnAllMC.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-secondary";
                btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                LoadTravelClaimMCGridView_AllCurrentLastmonth("current");
            }

            else if (MCbtnclick == 3)
            {
                btnAllMC.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                btnLastMonthMC.CssClass = "btn btn-xs btn-secondary";

                LoadTravelClaimMCGridView_AllCurrentLastmonth("last");
            }
            else
            {
                btnAllMC.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                LoadTravelClaimMCGridView_AllCurrentLastmonth("all");
            }
        }

        protected void btnAllMC_Click(object sender, EventArgs e)
        {
            MCbtnclick = 1;
            LoadTCMgrCGrid();
        }

        protected void btnCurrentMonthMC_Click(object sender, EventArgs e)
        {
            MCbtnclick = 2;
            LoadTCMgrCGrid();
        }

        protected void btnLastMonthMC_Click(object sender, EventArgs e)
        {
            MCbtnclick = 3;
            LoadTCMgrCGrid();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    LoadTCEmpGrid();
                }
                else
                {
                    searchdetails();
                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }

        public void searchdetails()
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
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
                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                    TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsforEmployee(User.Identity.Name, SelectedType, textSearch);
                    //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                    //{
                    //    MsgCls("No Records found", lblMessageBoard, System.Drawing.Color.Red);
                    //    grdAppRejTravel.Visible = false;
                    //    grdAppRejTravel.DataSource = null;
                    //    grdAppRejTravel.DataBind();
                    //    ////PnlIExpDetalsView.Visible = false;
                    //    ////Exportbtn.Visible = false;
                    //    return;
                    //}
                    //else
                    //{
                        MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
                        grdAppRejTravel.Visible = true;
                        grdAppRejTravel.DataSource = TrvlReqboList;
                        grdAppRejTravel.SelectedIndex = -1;
                        grdAppRejTravel.DataBind();
                        //GV_TravelClaimReqAppRej.Visible = false;
                        //grdAppRejTravel.Visible = false;
                        //Panel1.Visible = false;
                        ////PnlIExpDetalsView.Visible = false;
                        ////Exportbtn.Visible = false;
                    //}
                }

            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }

        }

        protected void txtSearchTR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchTR.Text == "")
                {
                    LoadTREmpGrid();
                }
                else
                {
                    searchdetailsTR();
                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }

        public void searchdetailsTR()
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                string SelectedType = "1";//ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtSearchTR.Text;
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

                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                    TrvlReqboList = travelrequestblObj.Get_TravelDetails(User.Identity.Name, SelectedType, textSearch);
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
                        MsgCls("", LblMsg, Color.Transparent);
                        GV_TravelReqUpdate.Visible = true;
                        GV_TravelReqUpdate.DataSource = TrvlReqboList;
                        GV_TravelReqUpdate.SelectedIndex = -1;
                        GV_TravelReqUpdate.DataBind();
                        //GV_TravelClaimReqAppRej.Visible = false;
                        ////PnlExpenseAdd.Visible = false;

                    //}

                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, Color.Red);
            }

        }

        protected void txtSearchMP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchMP.Text == "")
                {
                    LoadTCMgrPGrid();
                }
                else
                {
                    searchdetailsMP();
                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }

        public void searchdetailsMP()
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                string SelectedType = "1";//ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtSearchMP.Text;
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

                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                    TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsforApprover(User.Identity.Name, SelectedType, textSearch);
                    //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                    //{
                    //    MsgCls("No Records found", lblMessageBoard, Color.Red);
                    //    grdAppRejTravelMP.Visible = false;
                    //    grdAppRejTravelMP.DataSource = null;
                    //    grdAppRejTravelMP.DataBind();
                    //    ////PnlIExpDetalsView.Visible = false;
                    //    return;
                    //}
                    //else
                    //{
                        MsgCls("", lblMessageBoard, Color.Transparent);
                        grdAppRejTravelMP.Visible = true;
                        grdAppRejTravelMP.DataSource = TrvlReqboList;
                        grdAppRejTravelMP.SelectedIndex = -1;
                        grdAppRejTravelMP.DataBind();
                        //GV_TravelClaimReqAppRej.Visible = false;
                        //grdAppRejTravel.Visible = false;
                        //Panel1.Visible = false;
                        ////PnlIExpDetalsView.Visible = false;
                    //}

                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, Color.Red);
            }

        }

        protected void txtSearchMC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchMC.Text == "")
                {
                    LoadTCMgrCGrid();
                }
                else
                {
                    searchdetailsMC();
                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }

        public void searchdetailsMC()
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                string SelectedType = "1";//ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtSearchMC.Text;
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

                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                    TrvlReqboList = travelrequestblObj.Load_TravelClaimDetailsforAppRej(User.Identity.Name, SelectedType, textSearch);
                    //if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                    //{
                    //    MsgCls("No Records found", lblMessageBoard, Color.Red);
                    //    grdAppRejTravelMC.Visible = false;
                    //    grdAppRejTravelMC.DataSource = null;
                    //    grdAppRejTravelMC.DataBind();
                    //    ////PnlIExpDetalsView.Visible = false;
                    //    return;
                    //}
                    //else
                    //{
                        MsgCls("", lblMessageBoard, Color.Transparent);
                        grdAppRejTravelMC.Visible = true;
                        grdAppRejTravelMC.DataSource = TrvlReqboList;
                        grdAppRejTravelMC.SelectedIndex = -1;
                        grdAppRejTravelMC.DataBind();
                        //GV_TravelClaimReqAppRej.Visible = false;
                        //grdAppRejTravel.Visible = false;
                        //Panel1.Visible = false;
                        ////PnlIExpDetalsView.Visible = false;
                    //}


                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, Color.Red);
            }
        }

        protected void ddlPagesizeEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAppRejTravel.PageSize = Convert.ToInt32(ddlPagesizeEmp.SelectedValue);
            LoadTCEmpGrid();
        }

        protected void ddlPagesizeEmpTR_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_TravelReqUpdate.PageSize = Convert.ToInt32(ddlPagesizeEmpTR.SelectedValue);
            LoadTREmpGrid();
        }

        protected void ddlPagesizeMgrP_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAppRejTravelMP.PageSize = Convert.ToInt32(ddlPagesizeMgrP.SelectedValue);
            LoadTCMgrPGrid();
        }

        protected void ddlPagesizeMgrC_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAppRejTravelMC.PageSize = Convert.ToInt32(ddlPagesizeMgrC.SelectedValue);
            LoadTCMgrCGrid();
        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = PendingPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                if (Ebtnclick == 1)
                {
                    btnAll.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                    btnLastMonth.CssClass = "btn btn-xs btn-light";

                    LoadTravelClaimGridView_AllCurrentLastmonth("all");
                }

                else if (Ebtnclick == 2)
                {
                    btnAll.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonth.CssClass = "btn btn-xs btn-secondary";
                    btnLastMonth.CssClass = "btn btn-xs btn-light";

                    LoadTravelClaimGridView_AllCurrentLastmonth("current");
                }

                else if (Ebtnclick == 3)
                {
                    btnAll.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                    btnLastMonth.CssClass = "btn btn-xs btn-secondary";

                    LoadTravelClaimGridView_AllCurrentLastmonth("last");
                }
                else
                {
                    btnAll.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                    btnLastMonth.CssClass = "btn btn-xs btn-light";

                    LoadTravelClaimGridView_AllCurrentLastmonth("all");
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void LnkPgPRAppPending_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = PendingPageIndex = int.Parse((sender as LinkButton).CommandArgument);

                if (ETRbtnclick == 1)
                {
                    btnAllTR.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthTR.CssClass = "btn btn-xs btn-light";
                    btnLastMonthTR.CssClass = "btn btn-xs btn-light";

                    LoadTravelReqDetails_AllCurrentLastmonth("all");
                }

                else if (ETRbtnclick == 2)
                {
                    btnAllTR.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthTR.CssClass = "btn btn-xs btn-secondary";
                    btnLastMonthTR.CssClass = "btn btn-xs btn-light";

                    LoadTravelReqDetails_AllCurrentLastmonth("current");
                }

                else if (ETRbtnclick == 3)
                {
                    btnAllTR.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthTR.CssClass = "btn btn-xs btn-light";
                    btnLastMonthTR.CssClass = "btn btn-xs btn-secondary";

                    LoadTravelReqDetails_AllCurrentLastmonth("last");
                }
                else
                {
                    btnAllTR.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthTR.CssClass = "btn btn-xs btn-light";
                    btnLastMonthTR.CssClass = "btn btn-xs btn-light";

                    LoadTravelReqDetails_AllCurrentLastmonth("all");
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void LnkPgPRAppPending_Click1(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = PendingPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                if (MPbtnclick == 1)
                {
                    btnAllMP.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                    LoadTravelClaimMPGridView_AllCurrentLastmonth("all");
                }

                else if (MPbtnclick == 2)
                {
                    btnAllMP.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-secondary";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                    LoadTravelClaimMPGridView_AllCurrentLastmonth("current");
                }

                else if (MPbtnclick == 3)
                {
                    btnAllMP.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-secondary";

                    LoadTravelClaimMPGridView_AllCurrentLastmonth("last");
                }
                else
                {
                    btnAllMP.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                    LoadTravelClaimMPGridView_AllCurrentLastmonth("all");
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void LnkPgPRAppCompl_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = PendingPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                if (MCbtnclick == 1)
                {
                    btnAllMC.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                    LoadTravelClaimMCGridView_AllCurrentLastmonth("all");
                }

                else if (MCbtnclick == 2)
                {
                    btnAllMC.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMC.CssClass = "btn btn-xs btn-secondary";
                    btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                    LoadTravelClaimMCGridView_AllCurrentLastmonth("current");
                }

                else if (MCbtnclick == 3)
                {
                    btnAllMC.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMC.CssClass = "btn btn-xs btn-secondary";

                    LoadTravelClaimMCGridView_AllCurrentLastmonth("last");
                }
                else
                {
                    btnAllMC.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                    LoadTravelClaimMCGridView_AllCurrentLastmonth("all");
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void masschkhead_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdAppRejTravelMP.HeaderRow.FindControl("masschkhead");
            foreach (GridViewRow row in grdAppRejTravelMP.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true; pnlmassbtn.Visible = true;
                }
                else if (ChkBoxHeader.Checked == false)
                { ChkBoxRows.Checked = false; pnlmassbtn.Visible = false; }
            }
        }

        protected void btnMassApprove_Click(object sender, EventArgs e)
        {
            try
            {
                int cnt = 0;
                CheckBox ChkBoxHeader = (CheckBox)grdAppRejTravelMP.HeaderRow.FindControl("masschkhead");
                for (int i = 0; i < grdAppRejTravelMP.Rows.Count; i++)
                {
                    GridViewRow row = grdAppRejTravelMP.Rows[i];
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                    if (ChkBoxRows.Checked == true)
                    {
                        int claimID = Convert.ToInt32(grdAppRejTravelMP.DataKeys[row.RowIndex].Values["CID"].ToString());
                        bool? Status = true;
                        travelrequestbl travelrequestblObj = new travelrequestbl();
                        List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();
                        TrvlReqboList1 = travelrequestblObj.Load_ClaimStatusDetails(claimID);
                        grdAppRejHistory.DataSource = TrvlReqboList1;
                        grdAppRejHistory.DataBind();
                        ViewState["@TripNo"] = TrvlReqboList1[0].REINR.ToString();
                        ViewState["APPROVEDBY1"] = TrvlReqboList1[0].APPROVED_BY1 == null ? "" : TrvlReqboList1[0].APPROVED_BY1.ToString();
                        ViewState["APPROVEDBY2"] = TrvlReqboList1[0].APPROVED_BY2 == null ? "" : TrvlReqboList1[0].APPROVED_BY2.ToString();
                        ViewState["APPROVEDBY3"] = TrvlReqboList1[0].APPROVED_BY3 == null ? "" : TrvlReqboList1[0].APPROVED_BY3.ToString();
                        ViewState["APPROVEDBY4"] = TrvlReqboList1[0].APPROVED_BY4 == null ? "" : TrvlReqboList1[0].APPROVED_BY4.ToString();
                        ViewState["APPROVEDBY5"] = TrvlReqboList1[0].APPROVED_BY5 == null ? "" : TrvlReqboList1[0].APPROVED_BY5.ToString();
                        ViewState["APPROVEDBY6"] = TrvlReqboList1[0].APPROVED_BY6 == null ? "" : TrvlReqboList1[0].APPROVED_BY6.ToString();
                        ViewState["APPROVEDBY7"] = TrvlReqboList1[0].APPROVED_BY7 == null ? "" : TrvlReqboList1[0].APPROVED_BY7.ToString();
                        ViewState["APPROVEDBY8"] = TrvlReqboList1[0].APPROVED_BY8 == null ? "" : TrvlReqboList1[0].APPROVED_BY8.ToString();
                        ViewState["APPROVEDBY9"] = TrvlReqboList1[0].APPROVED_BY9 == null ? "" : TrvlReqboList1[0].APPROVED_BY9.ToString();



                        TrvlReqDetails TrvlReqboList = new TrvlReqDetails();
                        //int claimID = Convert.ToInt32(grdAppRejTravelMP.DataKeys[row.RowIndex].Values["CID"].ToString());
                        string project = grdAppRejTravelMP.DataKeys[row.RowIndex].Values["WBS_ELEMT"].ToString();
                        string Task = grdAppRejTravelMP.DataKeys[row.RowIndex].Values["ACTIVITY"].ToString();
                        string TAmt = grdAppRejTravelMP.DataKeys[row.RowIndex].Values["RE_AMT"].ToString();
                        string ReAmt = grdAppRejTravelMP.DataKeys[row.RowIndex].Values["RE_AMT"].ToString();

                        if (claimID != null)
                        {
                            TrvlReqboList.CID = claimID;
                            TrvlReqboList.APPROVED_BY1 = User.Identity.Name;
                            TrvlReqboList.COMMENTS = "APPROVED";////TxtRemarks.Text.Trim();
                            TrvlReqboList.STATUS = "Approved";

                            if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Approved1";
                            if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Approved2";
                            if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Approved3";
                            if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Approved4";
                            if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Approved5";
                            if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Approved6";
                            if (ViewState["APPROVEDBY7"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Approved7";
                            if (ViewState["APPROVEDBY8"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Approved8";
                            if (ViewState["APPROVEDBY9"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Approved9";



                            travelrequestblObj.Update_TravelClaim_Status(TrvlReqboList, ref Status);
                            if (Status.Equals(false))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Approved successfully !')", true);
                                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                                SendMailMethodtToEmp(TrvlReqboList, "APPROVED", project, Task, TAmt, ReAmt);
                                SendMailMethodmass(TrvlReqboList, project, Task, TAmt, ReAmt);

                            }

                        }
                        //loadTab3();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Approved successfully !')", true);
                        cnt += 1;
                    }
                }
                loadTab3();
                if (cnt > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Approved successfully !')", true);
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void btnMassReject_Click(object sender, EventArgs e)
        {
            try
            {
                int cnt = 0;
                CheckBox ChkBoxHeader = (CheckBox)grdAppRejTravelMP.HeaderRow.FindControl("masschkhead");
                for (int i = 0; i < grdAppRejTravelMP.Rows.Count; i++)
                {
                    GridViewRow row = grdAppRejTravelMP.Rows[i];
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                    if (ChkBoxRows.Checked == true)
                    {
                        int claimID = Convert.ToInt32(grdAppRejTravelMP.DataKeys[row.RowIndex].Values["CID"].ToString());
                        bool? Status = true;
                        travelrequestbl travelrequestblObj = new travelrequestbl();
                        List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();
                        TrvlReqboList1 = travelrequestblObj.Load_ClaimStatusDetails(claimID);
                        grdAppRejHistory.DataSource = TrvlReqboList1;
                        grdAppRejHistory.DataBind();
                        ViewState["@TripNo"] = TrvlReqboList1[0].REINR.ToString();
                        ViewState["APPROVEDBY1"] = TrvlReqboList1[0].APPROVED_BY1 == null ? "" : TrvlReqboList1[0].APPROVED_BY1.ToString();
                        ViewState["APPROVEDBY2"] = TrvlReqboList1[0].APPROVED_BY2 == null ? "" : TrvlReqboList1[0].APPROVED_BY2.ToString();
                        ViewState["APPROVEDBY3"] = TrvlReqboList1[0].APPROVED_BY3 == null ? "" : TrvlReqboList1[0].APPROVED_BY3.ToString();
                        ViewState["APPROVEDBY4"] = TrvlReqboList1[0].APPROVED_BY4 == null ? "" : TrvlReqboList1[0].APPROVED_BY4.ToString();
                        ViewState["APPROVEDBY5"] = TrvlReqboList1[0].APPROVED_BY5 == null ? "" : TrvlReqboList1[0].APPROVED_BY5.ToString();
                        ViewState["APPROVEDBY6"] = TrvlReqboList1[0].APPROVED_BY6 == null ? "" : TrvlReqboList1[0].APPROVED_BY6.ToString();
                        ViewState["APPROVEDBY7"] = TrvlReqboList1[0].APPROVED_BY7 == null ? "" : TrvlReqboList1[0].APPROVED_BY7.ToString();
                        ViewState["APPROVEDBY8"] = TrvlReqboList1[0].APPROVED_BY8 == null ? "" : TrvlReqboList1[0].APPROVED_BY8.ToString();
                        ViewState["APPROVEDBY9"] = TrvlReqboList1[0].APPROVED_BY9 == null ? "" : TrvlReqboList1[0].APPROVED_BY9.ToString();



                        TrvlReqDetails TrvlReqboList = new TrvlReqDetails();
                        //int claimID = Convert.ToInt32(grdAppRejTravelMP.DataKeys[row.RowIndex].Values["CID"].ToString());
                        string project = grdAppRejTravelMP.DataKeys[row.RowIndex].Values["WBS_ELEMT"].ToString();
                        string Task = grdAppRejTravelMP.DataKeys[row.RowIndex].Values["ACTIVITY"].ToString();
                        string TAmt = grdAppRejTravelMP.DataKeys[row.RowIndex].Values["RE_AMT"].ToString();
                        string ReAmt = grdAppRejTravelMP.DataKeys[row.RowIndex].Values["RE_AMT"].ToString();


                        TrvlReqboList.CID = claimID;
                        TrvlReqboList.APPROVED_BY1 = User.Identity.Name;
                        TrvlReqboList.COMMENTS = "Rejected";
                        TrvlReqboList.STATUS = "Rejected";

                        if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Rejected1";
                        if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Rejected2";
                        if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Rejected3";
                        if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Rejected4";
                        if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Rejected5";
                        if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Rejected6";
                        if (ViewState["APPROVEDBY7"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Rejected7";
                        if (ViewState["APPROVEDBY8"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Rejected8";
                        if (ViewState["APPROVEDBY9"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Rejected9";


                        travelrequestblObj.Update_TravelClaim_Status(TrvlReqboList, ref Status);
                        if (Status.Equals(false))
                        {
                            ModalPopupExtender1.Hide();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Rejected successfully !')", true);
                            MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                            RejectSendMailMethodtToEmp(TrvlReqboList, "Rejected", project, Task, TAmt, ReAmt);

                        }
                        //loadTab3();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Rejected successfully !')", true);
                        cnt += 1;
                    }
                }
                loadTab3();
                if (cnt > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Rejected successfully !')", true);
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }



        private void SendMailMethodmass(TrvlReqDetails TrvlReqboList, string project, string Task, string TAmt, string ReAmt)
        {
            try
            {
                pnlHide.Visible = true;
                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();
                TrvlReqboList1 = travelrequestblObj.Load_ClaimDetails(Convert.ToInt32(TrvlReqboList.CID));
                grdClaimDetails.DataSource = TrvlReqboList1;
                grdClaimDetails.DataBind();

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = false;
                    row.Cells[15].FindControl("fuAttachments").Visible = false;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = false;
                    row.Cells[15].FindControl("LbtnUpload").Visible = false;
                    row.Cells[15].FindControl("LbtnDelete").Visible = false;

                }
                grdClaimDetails.Columns[16].Visible = false;
                grdClaimDetails.FooterRow.ForeColor = System.Drawing.Color.Black;
                string ClaimTotal = grdClaimDetails.FooterRow.Cells[8].Text;
                grdClaimDetails.FooterRow.Visible = true;
                grdClaimDetails.RenderControl(hw1);
                grdClaimDetails.Columns[16].Visible = true;
                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = true;
                    row.Cells[15].FindControl("fuAttachments").Visible = true;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = true;
                    row.Cells[15].FindControl("LbtnUpload").Visible = true;
                    row.Cells[15].FindControl("LbtnDelete").Visible = true;

                }

                string strSubject = string.Empty;

                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVEDBY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                string Project_code = "";
                string Entity = "";


                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_TravelclaimAppNew(TrvlReqboList.CID, TrvlReqboList.APPROVED_BY1, TrvlReqboList.STATUS, ref CREATED_BY, ref APPROVEDBY1, ref Approver_Name,
                 ref Approver_Email, ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref Project_code, ref Entity);


                if (Approver_Email != null)
                {

                    strSubject = "Travel Claim Request " + TrvlReqboList.CID + " has been Raised by " + EMP_Name + "  |  " + CREATED_BY + " and is pending for the Approval.";


                    RecipientsString = Approver_Email;
                    strPernr_Mail = EMP_Email;
                    //Preparing the mail body--------------------------------------------------
                    string body = "<b>Travel Claim Request " + TrvlReqboList.CID + " has been Raised by " + EMP_Name + "  |  " + CREATED_BY + " and is pending for the Approval.<br/><br/></b>";
                    body += "<b>Entity with Claim ID  :  " + Entity + " : " + TrvlReqboList.CID + "</b><br/><br/>";
                    body += "<b>Travel Claim Header Details :<hr /><br/>";
                    body += "Trip ID       :  " + ViewState["@TripNo"].ToString() + "<br/>";
                    body += "Project       :  " + Project_code + " - " + project + "<br/>";
                    body += "Task          :  " + Task + "<br/>";
                    body += "Total Current Claim Reimbursement Amount  :  " + ClaimTotal + "<br/>";
                    body += "Total Trip Claims Reimbursement Amount  :  " + decimal.Parse(TAmt).ToString("#,##0.00") + " ( " + ReAmt + " ) <br/>";
                    // body += "Total Trip Claims Reimbursement Amount  :  " + TAmt + "<br/>";
                    //body += "Reimbursement Currency      :  " + ReAmt + "<br/><br/>";
                    body += "<b>Travel Claim Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";


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


                    // lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    //lblMessageBoard.Text = "Mail sent successfully.";

                } pnlHide.Visible = false;
            }
            catch
            {
                pnlHide.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Error in sending mail.";
                return;
            }
        }

        private void SendMailMethodtToEmp(TrvlReqDetails TrvlReqboList, string Remark, string project, string Task, string Tamt, string Reamt)
        {
            try
            {
                pnlHide.Visible = true;
                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();
                TrvlReqboList1 = travelrequestblObj.Load_ClaimDetails(Convert.ToInt32(TrvlReqboList.CID));
                grdClaimDetails.DataSource = TrvlReqboList1;
                grdClaimDetails.DataBind();
                //if (Task == "B")
                //{
                //    Task = "Billable";
                //}
                //else
                //{
                //    Task = "Non-Billable";
                //}

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //StringWriter sw2 = new StringWriter();
                //HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                //  FV_PRInfoDisplay.RenderControl(hw);

                //FV_PRInfoDisplay.RenderControl(hw1);
                //grdClaimDetails.RenderControl(hw1);


                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = false;
                    row.Cells[15].FindControl("fuAttachments").Visible = false;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = false;
                    row.Cells[15].FindControl("LbtnUpload").Visible = false;
                    row.Cells[15].FindControl("LbtnDelete").Visible = false;

                }
                grdClaimDetails.Columns[16].Visible = false;
                string ClaimTotal = grdClaimDetails.FooterRow.Cells[8].Text;
                grdClaimDetails.FooterRow.ForeColor = System.Drawing.Color.Black;
                grdClaimDetails.FooterRow.Visible = true;
                grdClaimDetails.RenderControl(hw1);
                grdClaimDetails.Columns[16].Visible = true;
                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = true;
                    row.Cells[15].FindControl("fuAttachments").Visible = true;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = true;
                    row.Cells[15].FindControl("LbtnUpload").Visible = true;
                    row.Cells[15].FindControl("LbtnDelete").Visible = true;

                }


                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVEDBY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                //string Purpose = "";
                string Project_code = "";
                string Entity = "";
                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_TravelclaimAppNew(TrvlReqboList.CID, TrvlReqboList.APPROVED_BY1, TrvlReqboList.STATUS, ref CREATED_BY, ref APPROVEDBY1, ref Approver_Name,
                ref Approver_Email, ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref Project_code, ref Entity);


                strSubject = "Travel Claim Requisition" + TrvlReqboList.CID + " has been approved by " + PRSNTAPPROVEDBY_Name;

                RecipientsString = EMP_Email;
                strPernr_Mail = PRSNTAPPROVEDBY_Email;

                //GridViewRow selectedrow = grdAppRejIexp.SelectedRow;


                //    //Preparing the mail body--------------------------------------------------

                string body = "<b>Travel Claim Requisition " + TrvlReqboList.CID + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + TrvlReqboList.APPROVED_BY1 + "<br/><br/></b>";
                body += "<b>Entity with Claim ID  :  " + Entity + " : " + TrvlReqboList.CID + "</b><br/><br/>";
                body += "<b>Travel Claim Header Details :<hr /><br/>";
                body += "Trip ID       :  " + ViewState["@TripNo"].ToString() + "<br/>";
                body += "Project       :  " + Project_code + " - " + project + "<br/>";
                body += "Task          :  " + Task + "<br/>";
                // body += "Total Current Claim Reimbursement Amount  :  " + decimal.Parse(ClaimTotal).ToString("#,##0.00") + "<br/>";
                body += "Total Current Claim Reimbursement Amount  :  " + ClaimTotal + "<br/>";
                body += "Total Trip Claims Reimbursement Amount  :  " + decimal.Parse(Tamt).ToString("#,##0.00") + " ( " + Reamt + " ) <br/>";
                //body += "Total Reimbursement Amount  :  " + Tamt + "<br/>";
                //body += "Reimbursement Currency      :  " + Reamt + "<br/><br/>";
                body += "<b>Travel Claim Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";
                body += "<b>Remarks  :  " + Remark + "</b><br/>";




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
                lblMessageBoard.Text = "Travel Claim Request Approved successfully and Mail sent successfully.";
                pnlHide.Visible = false;
            }
            catch
            {
                pnlHide.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Travel Claim Request Approved successfully. Error in sending mail";
                return;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private void RejectSendMailMethodtToEmp(TrvlReqDetails TrvlReqboList, string Remark, string project, string Task, string Tamt, string Reamt)
        {
            try
            {
                pnlHide.Visible = true;
                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();
                TrvlReqboList1 = travelrequestblObj.Load_ClaimDetails(Convert.ToInt32(TrvlReqboList.CID));
                grdClaimDetails.DataSource = TrvlReqboList1;
                grdClaimDetails.DataBind();
                //if (Task == "B")
                //{
                //    Task = "Billable";
                //}
                //else
                //{
                //    Task = "Non-Billable";
                //}

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                //grdClaimDetails.RenderControl(hw1);

                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = false;
                    row.Cells[15].FindControl("fuAttachments").Visible = false;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = false;
                    row.Cells[15].FindControl("LbtnUpload").Visible = false;
                    row.Cells[15].FindControl("LbtnDelete").Visible = false;

                }
                grdClaimDetails.Columns[16].Visible = false;
                string ClaimTotal = grdClaimDetails.FooterRow.Cells[8].Text;
                grdClaimDetails.FooterRow.ForeColor = System.Drawing.Color.Black;
                grdClaimDetails.FooterRow.Visible = true;
                grdClaimDetails.RenderControl(hw1);
                grdClaimDetails.Columns[16].Visible = true;
                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = true;
                    row.Cells[15].FindControl("fuAttachments").Visible = true;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = true;
                    row.Cells[15].FindControl("LbtnUpload").Visible = true;
                    row.Cells[15].FindControl("LbtnDelete").Visible = true;

                }

                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVEDBY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                // string Purpose = "";
                string Project_code = "";
                string Entity = "";
                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_TravelclaimAppNew(TrvlReqboList.CID, TrvlReqboList.APPROVED_BY1, TrvlReqboList.STATUS, ref CREATED_BY, ref APPROVEDBY1, ref Approver_Name,
                ref Approver_Email, ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref Project_code, ref Entity);



                strSubject = "Travel Claim Requisition " + TrvlReqboList.CID + " has been Rejected by " + PRSNTAPPROVEDBY_Name;

                RecipientsString = EMP_Email;
                strPernr_Mail = PRSNTAPPROVEDBY_Email;

                GridViewRow selectedrow = grdClaimDetails.SelectedRow;


                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>Travel Claim Requisition " + TrvlReqboList.CID + " has been Rejected by " + PRSNTAPPROVEDBY_Name + "  |  " + TrvlReqboList.APPROVED_BY1 + "<br/><br/></b>";
                body += "<b>Entity with Claim ID  :  " + Entity + " : " + TrvlReqboList.CID + "</b><br/><br/>";
                body += "<b>Travel Claim Header Details :<hr /><br/>";
                body += "Trip ID       :  " + ViewState["@TripNo"].ToString() + "<br/>";
                body += "Project       :  " + Project_code + " - " + project + "<br/>";
                body += "Task          :  " + Task + "<br/>";
                //body += "Total Reimbursement Amount  :  " + Tamt + "<br/>";
                //body += "Reimbursement Currency      :  " + Reamt + "<br/><br/>";
                body += "Total Current Claim Reimbursement Amount  :  " + ClaimTotal + "<br/>";
                body += "Total Trip Claims Reimbursement Amount  :  " + decimal.Parse(Tamt).ToString("#,##0.00") + " ( " + Reamt + " ) <br/>";
                body += "<b>Travel Claim Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";
                body += "<b>Remarks  :  " + Remark + "</b><br/>";


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
                lblMessageBoard.Text = "Travel Claim Request Rejected and Mail sent successfully.";
                pnlHide.Visible = false;
            }
            catch
            {
                pnlHide.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Travel Claim Request Rejected. Error in sending mail";
                return;
            }
        }

        protected void grdAppRejTravelMP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[8].Text == "Billable")
                    {
                        e.Row.Cells[8].Font.Bold = true;
                        e.Row.Cells[8].BackColor = Color.FromName("#FFF9AE");
                        e.Row.Cells[8].ForeColor = Color.DarkSlateBlue;
                    }
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, lblMessageBoard, Color.Red); }
        }


    }
}
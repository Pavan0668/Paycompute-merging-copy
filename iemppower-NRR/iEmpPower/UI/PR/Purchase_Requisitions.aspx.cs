using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using iEmpPower.Old_App_Code.iEmpPowerDAL.PR;

namespace iEmpPower.UI.PR
{
    public partial class Purchase_Requisitions : System.Web.UI.Page
    {
        bool bSortedOrder = false;
        string EmployeeId = "";
        int Ebtnclick = 1;
        int MPbtnclick = 1;
        int MCbtnclick = 1;

        //protected int CompleatedPageIndex = 1;
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
                    if (Request.QueryString["PC"] == "P")
                    {
                        loadTab2();
                        goto displayInfo;
                    }
                    else if (Request.QueryString["PC"] == "C")
                    {
                        loadTab3();
                        goto displayInfo;
                    }
                }


                Tab1.CssClass = "nav-link active p-2";
                MainView.ActiveViewIndex = 0;
                LoadPREmpGrid();//LoadEmpPRRequestGridView();
            displayInfo: { }
                if (Request.QueryString["PC"] == null)
                {
                    if (Session["_MainSearchValue"].ToString() != "")
                    {
                        txtsearch.Text = Session["_MainSearchValue"].ToString();
                        searchpr();
                        Session["_MainSearchValue"] = "";
                    }
                }
                else if (Request.QueryString["PC"] == "P")
                {
                    if (Session["_MainSearchValue"].ToString() != "")
                    {
                        txtSearchMP.Text = Session["_MainSearchValue"].ToString();
                        searchprMP();
                        Session["_MainSearchValue"] = "";
                    }
                }
                else if (Request.QueryString["PC"] == "C")
                {
                    if (Session["_MainSearchValue"].ToString() != "")
                    {
                        txtSearchMC.Text = Session["_MainSearchValue"].ToString();
                        searchprMC();
                        Session["_MainSearchValue"] = "";
                    }
                }
                else
                {
                    Session["_MainSearchValue"] = "";
                }
            }
            int cnt = 0;
            foreach (GridViewRow row in grdPRAppRej.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                if (ChkBoxRows.Checked == true)
                    cnt += 1;
            }
            pnlmassbtn.Visible = (cnt > 0) ? true : false;

            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" +User.Identity.Name.ToString() + "');", true);
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
            MainView.ActiveViewIndex = 0;
            LoadPREmpGrid();//LoadEmpPRRequestGridView();

            clearSearch();

        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            loadTab2();
            clearSearch();
        }

        void loadTab2()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link active p-2";
            Tab3.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 1;
            LoadPRMgrPGrid();// LoadPRRequestGridView();

            int cnt = 0;
            foreach (GridViewRow row in grdPRAppRej.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                if (ChkBoxRows.Checked == true)
                    cnt += 1;
            }
            pnlmassbtn.Visible = (cnt > 0) ? true : false;
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            loadTab3();
            clearSearch();
        }

        void loadTab3()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link active p-2";
            MainView.ActiveViewIndex = 2;
            LoadPRMgrCGrid(); //LoadPRRequestCompletedGridView();
        }


        private void LoadEmpPRRequestGridView()
        {
            try
            {
                prbl prblObj = new prbl();
                List<prbo> requisitionboList1 = new List<prbo>();
                EmployeeId = User.Identity.Name;
                requisitionboList1 = prblObj.Load_EmpPRDetails(EmployeeId, "EMP");

                Session.Add("PRGrdInfo", requisitionboList1);

                grdPurchaseItemDetails.Visible = true;
                grdPurchaseItemDetails.DataSource = requisitionboList1;
                grdPurchaseItemDetails.SelectedIndex = -1;
                grdPurchaseItemDetails.DataBind();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadEmpPRRequestGridView_AllCurrentLastmonth(string month)
        {
            try
            {
                int? recordcnt = 0;
                prbl prblObj = new prbl();
                List<prbo> requisitionboList1 = new List<prbo>();
                EmployeeId = User.Identity.Name;
                PagerSz = Convert.ToInt32(ddlPagesizeEmp.SelectedItem.Text);
                requisitionboList1 = prblObj.LoadEmpPRRequestGridView_AllCurrentLastmonth_Rpager(EmployeeId, "EMP", month, PendingPageIndex, PagerSz, ref recordcnt);

                Session.Add("PRGrdInfo", requisitionboList1);

                grdPurchaseItemDetails.Visible = true;
                grdPurchaseItemDetails.DataSource = requisitionboList1;
                grdPurchaseItemDetails.SelectedIndex = -1;
                grdPurchaseItemDetails.DataBind();
                RptrPendingPager.Visible = requisitionboList1.Count <= 0 ? false : true;
                PopulatePendingPager(requisitionboList1.Count > 0 ? int.Parse(recordcnt.ToString()) : 0, PendingPageIndex, RptrPendingPager);

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadPRRequestGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                prbl travelrequestblObj = new prbl();
                List<prbo> requisitionboList = new List<prbo>();
                //==============================
                //get sub employees
                ////msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                ////msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                ////objPIDashBoardBo.PERNR = User.Identity.Name;
                ////msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_Manager(objPIDashBoardBo);

                ////if (objPIDashBoardLst.Count > 0)
                ////{
                ////    foreach (var vrow in objPIDashBoardLst)
                ////    {
                List<prbo> requisitionboList1 = new List<prbo>();
                ////EmployeeId = vrow.PERNR;
                ////EmployeeName = vrow.ENAME;

                ////requisitionboList1 = travelrequestblObj.Load_PRDetails(EmployeeId, EmployeeName);
                string ApproverId = User.Identity.Name;
                requisitionboList1 = travelrequestblObj.Load_PRDetails(ApproverId, "");

                List<string> Keys = new List<string> { "Requested", "Approved1", "Approved2", "Approved3", "Approved4", "Approved5", "HOLD1", "RELEASED1", "HOLD2", "RELEASED2", "HOLD3", "RELEASED3", "HOLD4", "RELEASED4", "HOLD5", "RELEASED5", "HOLD6", "RELEASED6" };
                var filteredResult = requisitionboList1.Where(r => Keys.Contains(r.STATUS));
                requisitionboList1 = filteredResult.ToList();
                requisitionboList.AddRange(requisitionboList1);
                Session.Add("PRGrdInfo", requisitionboList);
                ////    }
                ////}


                //if (requisitionboList == null || requisitionboList.Count == 0)
                //{
                //    grdPRAppRej.Visible = false;
                //    grdPRAppRej.DataSource = null;
                //    MsgCls("No Records Found", lblMessageBoard, Color.Red);

                //    return;
                //}
                //else
                //{
                grdPRAppRej.Visible = true;
                grdPRAppRej.DataSource = requisitionboList;
                grdPRAppRej.SelectedIndex = -1;
                MsgCls("", lblMessageBoard, Color.Transparent);

                //}
                grdPRAppRej.DataBind();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void LoadPRRequestGridView_AllCurrentLastmonth(string month)
        {
            try
            {
                PagerSz = Convert.ToInt32(ddlPagesizeMgrP.SelectedItem.Text);
                int? recCnt = 0;
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                prbl travelrequestblObj = new prbl();
                List<prbo> requisitionboList = new List<prbo>();

                List<prbo> requisitionboList1 = new List<prbo>();

                string ApproverId = User.Identity.Name;
                requisitionboList1 = travelrequestblObj.Load_PRDetails_AllCurrentLastmonth_Rpager(ApproverId, "", month, PendingPageIndex, PagerSz, ref recCnt);

                List<string> Keys = new List<string> { "Requested", "Approved1", "Approved2", "Approved3", "Approved4", "Approved5", "HOLD1", "RELEASED1", "HOLD2", "RELEASED2", "HOLD3", "RELEASED3", "HOLD4", "RELEASED4", "HOLD5", "RELEASED5", "HOLD6", "RELEASED6" };
                var filteredResult = requisitionboList1.Where(r => Keys.Contains(r.STATUS));
                requisitionboList1 = filteredResult.ToList();
                requisitionboList.AddRange(requisitionboList1);
                Session.Add("PRGrdInfo", requisitionboList);
                RepeatrPRAppPending.Visible = requisitionboList1.Count <= 0 ? false : true;
                PopulatePendingPager(requisitionboList1.Count > 0 ? int.Parse(recCnt.ToString()) : 0, PendingPageIndex, RepeatrPRAppPending);



                //if (requisitionboList == null || requisitionboList.Count == 0)
                //{
                //    grdPRAppRej.Visible = false;
                //    grdPRAppRej.DataSource = null;
                //    MsgCls("No Records Found", lblMessageBoard, Color.Red);

                //    return;
                //}
                //else
                //{
                grdPRAppRej.Visible = true;
                grdPRAppRej.DataSource = requisitionboList;
                grdPRAppRej.SelectedIndex = -1;
                MsgCls("", lblMessageBoard, Color.Transparent);

                //}
                grdPRAppRej.DataBind();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void LoadPRRequestCompletedGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                prbl travelrequestblObj = new prbl();
                List<prbo> requisitionboList = new List<prbo>();
                List<prbo> requisitionboList1 = new List<prbo>();
                string ApproverId = User.Identity.Name;
                requisitionboList1 = travelrequestblObj.Load_PRDetailsAllApproveRejMC(ApproverId, "");
                requisitionboList.AddRange(requisitionboList1);
                Session.Add("PRGrdInfo", requisitionboList);


                //if (requisitionboList == null || requisitionboList.Count == 0)
                //{
                //    grdPRAppRejC.Visible = false;
                //    grdPRAppRejC.DataSource = null;
                //    MsgCls("No Records Found", lblMessageBoard, Color.Red);

                //    return;
                //}
                //else
                //{
                grdPRAppRejC.Visible = true;
                grdPRAppRejC.DataSource = requisitionboList;
                grdPRAppRejC.SelectedIndex = -1;
                MsgCls("", lblMessageBoard, Color.Transparent);

                //}
                grdPRAppRejC.DataBind();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void LoadPRRequestCompletedGridView_AllCurrentLastmonth(string month)
        {
            try
            {
                int? recCnt = 0;
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                prbl travelrequestblObj = new prbl();
                PagerSz = Convert.ToInt32(ddlPagesizeMgrC.SelectedItem.Text);
                List<prbo> requisitionboList = new List<prbo>();
                List<prbo> requisitionboList1 = new List<prbo>();
                string ApproverId = User.Identity.Name;
                requisitionboList1 = travelrequestblObj.Load_PRDetailsAllApproveRejMC_AllCurrentLastmonth_Rpager(ApproverId, "", month, PendingPageIndex, PagerSz, ref recCnt);
                requisitionboList.AddRange(requisitionboList1);
                Session.Add("PRGrdInfo", requisitionboList);
                RepetrCompl.Visible = requisitionboList1.Count <= 0 ? false : true;
                PopulatePendingPager(requisitionboList1.Count > 0 ? int.Parse(recCnt.ToString()) : 0, PendingPageIndex, RepetrCompl);


                //if (requisitionboList == null || requisitionboList.Count == 0)
                //{
                //    grdPRAppRejC.Visible = false;
                //    grdPRAppRejC.DataSource = null;
                //    MsgCls("No Records Found", lblMessageBoard, Color.Red);

                //    return;
                //}
                //else
                //{
                grdPRAppRejC.Visible = true;
                grdPRAppRejC.DataSource = requisitionboList;
                grdPRAppRejC.SelectedIndex = -1;
                MsgCls("", lblMessageBoard, Color.Transparent);

                //}
                grdPRAppRejC.DataBind();
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

        protected void grdPurchaseItemDeatils_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdPurchaseItemDetails.PageIndex = e.NewPageIndex;

            LoadPREmpGrid();//LoadEmpPRRequestGridView();
            ////searchpr();
            grdPurchaseItemDetails.SelectedIndex = -1;
        }

        protected void grdPurchaseItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "COPY":
                        int rowIndex1 = Convert.ToInt32(e.CommandArgument);
                        int PRID = int.Parse(grdPurchaseItemDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());
                        Session["PRID"] = PRID;
                        Response.Redirect("Purchase_Request.aspx?NC=" + "C");
                        break;

                    case "EDIT":
                        int rowIndexEdit = Convert.ToInt32(e.CommandArgument);
                        int PRIDEdit = int.Parse(grdPurchaseItemDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());
                        Session["PRID"] = PRIDEdit;
                        Response.Redirect("Purchase_Request.aspx?NC=" + "E");
                        break;


                    case "VIEW":


                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        ////foreach (GridViewRow row in grdPurchaseItemDetails.Rows)
                        ////{
                        ////    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                        ////    System.Drawing.Color.LightGray :
                        ////    System.Drawing.Color.White;
                        ////}

                        int PRID1 = int.Parse(grdPurchaseItemDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());
                        Session["PRID"] = PRID1;
                        Response.Redirect("PR_Status.aspx?NC=" + "C");

                        break;

                    case "STATUS":

                        int rowIndex2 = Convert.ToInt32(e.CommandArgument);

                        ////foreach (GridViewRow row in grdPurchaseItemDetails.Rows)
                        ////{
                        ////    row.BackColor = row.RowIndex.Equals(rowIndex2) ?
                        ////    System.Drawing.Color.LightGray :
                        ////    System.Drawing.Color.White;
                        ////}

                        ////ViewPRIfo.Visible = true;
                        int PRID2 = int.Parse(grdPurchaseItemDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());




                        prbl PrBlObj = new prbl();
                        List<prbo> requisitionboList = new List<prbo>();
                        requisitionboList = PrBlObj.Load_PRItemDetails(PRID2);
                        ////FV_EmpPRInfoDisplay.DataSource = requisitionboList;
                        ////FV_EmpPRInfoDisplay.DataBind();

                        ltPRid.Text = PRID2.ToString();
                        grdEmpAppHistory.DataSource = requisitionboList;
                        grdEmpAppHistory.DataBind();

                        ////dvAppHistory.Visible = true;
                        ModalPopupExtender1.Show();


                        ////ViewState["APPROVEDBY1"] = requisitionboList[0].APPROVEDBY1 == null ? "" : requisitionboList[0].APPROVEDBY1.ToString();
                        ////ViewState["APPROVEDBY2"] = requisitionboList[0].APPROVEDBY2 == null ? "" : requisitionboList[0].APPROVEDBY2.ToString();
                        ////ViewState["APPROVEDBY3"] = requisitionboList[0].APPROVEDBY3 == null ? "" : requisitionboList[0].APPROVEDBY3.ToString();
                        ////ViewState["APPROVEDBY4"] = requisitionboList[0].APPROVEDBY4 == null ? "" : requisitionboList[0].APPROVEDBY4.ToString();
                        ////ViewState["APPROVEDBY5"] = requisitionboList[0].APPROVEDBY5 == null ? "" : requisitionboList[0].APPROVEDBY5.ToString();
                        ////ViewState["APPROVEDBY6"] = requisitionboList[0].APPROVEDBY6 == null ? "" : requisitionboList[0].APPROVEDBY6.ToString();


                        ////requisitionboList = PrBlObj.Load_PRItem(PRID);
                        ////GV_EmpPrItems.DataSource = requisitionboList;
                        ////GV_EmpPrItems.DataBind();
                        break;

                    case "CANCEL":


                        int PRID3 = int.Parse(grdPurchaseItemDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());

                        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString);
                        SqlCommand cmd = new SqlCommand("Usp_PR_Cancel", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PR_ID", PRID3);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            LblMsg.Text = "Selected PR : " + PRID3 + " has been Cancelled Successfully";
                            LblMsg.ForeColor = System.Drawing.Color.Green;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + "Selected PR : " + PRID3 + " has been Cancelled Successfully" + "')", true);
                            LoadPREmpGrid();//LoadEmpPRRequestGridView();
                        }
                        else
                        {
                            LblMsg.Text = "Unable to Cancel the selected PR : " + PRID3;
                            LblMsg.ForeColor = System.Drawing.Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + "Unable to Cancel the selected PR : " + PRID3 + "')", true);
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

        protected void grdPurchaseItemDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void grdPRAppRej_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdPRAppRej.PageIndex = e.NewPageIndex;

            LoadPRMgrPGrid();//LoadPRRequestGridView();
            ////searchpr();
            grdPRAppRej.SelectedIndex = -1;
        }

        protected void grdPRAppRej_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        ////foreach (GridViewRow row in grdPRAppRej.Rows)
                        ////{
                        ////    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                        ////    System.Drawing.Color.LightGray :
                        ////    System.Drawing.Color.White;
                        ////}

                        int PRID1 = int.Parse(grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());
                        Session["PRID"] = PRID1;
                        Response.Redirect("PR_ManagerAppRej.aspx");


                        break;

                    case "STATUS":

                        int rowIndex2 = Convert.ToInt32(e.CommandArgument);

                        ////foreach (GridViewRow row in grdPRAppRej.Rows)
                        ////{
                        ////    row.BackColor = row.RowIndex.Equals(rowIndex2) ?
                        ////    System.Drawing.Color.LightGray :
                        ////    System.Drawing.Color.White;
                        ////}

                        ////ViewPRIfo.Visible = true;
                        int PRID2 = int.Parse(grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());

                        prbl PrBlObj = new prbl();
                        List<prbo> requisitionboList = new List<prbo>();
                        requisitionboList = PrBlObj.Load_PRItemDetails(PRID2);
                        ////FV_EmpPRInfoDisplay.DataSource = requisitionboList;
                        ////FV_EmpPRInfoDisplay.DataBind();

                        ltPRid.Text = PRID2.ToString();
                        grdEmpAppHistory.DataSource = requisitionboList;
                        grdEmpAppHistory.DataBind();

                        ////dvAppHistory.Visible = true;
                        ModalPopupExtender2.Show();

                        break;



                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void grdPRAppRej_Sorting(object sender, GridViewSortEventArgs e)
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

            grdPRAppRej.DataSource = PrboList;
            grdPRAppRej.DataBind();

            Session.Add("PRGrdInfo", PrboList);

        }

        protected void grdPRAppRejC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdPRAppRejC.PageIndex = e.NewPageIndex;

            LoadPRRequestCompletedGridView();
            ////searchpr();
            grdPRAppRejC.SelectedIndex = -1;
        }

        protected void grdPRAppRejC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        ////foreach (GridViewRow row in grdPRAppRejC.Rows)
                        ////{
                        ////    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                        ////    System.Drawing.Color.LightGray :
                        ////    System.Drawing.Color.White;
                        ////}




                        int PRID1 = int.Parse(grdPRAppRejC.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());
                        Session["PRID"] = PRID1;
                        Response.Redirect("PR_Status.aspx");//Response.Redirect("PR_Status.aspx?NC=" + "C");


                        break;

                    case "STATUS":

                        int rowIndex2 = Convert.ToInt32(e.CommandArgument);

                        ////foreach (GridViewRow row in grdPRAppRejC.Rows)
                        ////{
                        ////    row.BackColor = row.RowIndex.Equals(rowIndex2) ?
                        ////    System.Drawing.Color.LightGray :
                        ////    System.Drawing.Color.White;
                        ////}

                        ////ViewPRIfo.Visible = true;
                        int PRID2 = int.Parse(grdPRAppRejC.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());

                        prbl PrBlObj = new prbl();
                        List<prbo> requisitionboList = new List<prbo>();
                        requisitionboList = PrBlObj.Load_PRItemDetails(PRID2);
                        ////FV_EmpPRInfoDisplay.DataSource = requisitionboList;
                        ////FV_EmpPRInfoDisplay.DataBind();

                        ltPRid.Text = PRID2.ToString();
                        grdEmpAppHistory.DataSource = requisitionboList;
                        grdEmpAppHistory.DataBind();

                        ////dvAppHistory.Visible = true;
                        ModalPopupExtender3.Show();

                        break;

                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void grdPRAppRejC_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        void LoadPREmpGrid()
        {
            if (Ebtnclick == 1)
            {
                btnAll.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                LoadEmpPRRequestGridView_AllCurrentLastmonth("all");
            }

            else if (Ebtnclick == 2)
            {
                btnAll.CssClass = "btn btn-xs btn-light";
                btnCurrentMonth.CssClass = "btn btn-xs btn-secondary";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                LoadEmpPRRequestGridView_AllCurrentLastmonth("current");
            }

            else if (Ebtnclick == 3)
            {
                btnAll.CssClass = "btn btn-xs btn-light";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-secondary";

                LoadEmpPRRequestGridView_AllCurrentLastmonth("last");
            }
            else
            {
                btnAll.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                LoadEmpPRRequestGridView_AllCurrentLastmonth("all");
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            Ebtnclick = 1;
            LoadPREmpGrid();
        }

        protected void btnCurrentMonth_Click(object sender, EventArgs e)
        {
            Ebtnclick = 2;
            LoadPREmpGrid();
        }

        protected void btnLastMonth_Click(object sender, EventArgs e)
        {
            Ebtnclick = 3;
            LoadPREmpGrid();
        }

        //protected void btnNewPR_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("Purchase_Request.aspx?NC=" + "N");
        //}


        void LoadPRMgrPGrid()
        {
            if (MPbtnclick == 1)
            {
                btnAllMP.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                LoadPRRequestGridView_AllCurrentLastmonth("all");
            }

            else if (MPbtnclick == 2)
            {
                btnAllMP.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-secondary";
                btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                LoadPRRequestGridView_AllCurrentLastmonth("current");
            }

            else if (MPbtnclick == 3)
            {
                btnAllMP.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                btnLastMonthMP.CssClass = "btn btn-xs btn-secondary";

                LoadPRRequestGridView_AllCurrentLastmonth("last");
            }
            else
            {
                btnAllMP.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                LoadPRRequestGridView_AllCurrentLastmonth("all");
            }
        }

        protected void btnAllMP_Click(object sender, EventArgs e)
        {
            MPbtnclick = 1;
            LoadPRMgrPGrid();
        }

        protected void btnCurrentMonthMP_Click(object sender, EventArgs e)
        {
            MPbtnclick = 2;
            LoadPRMgrPGrid();
        }

        protected void btnLastMonthMP_Click(object sender, EventArgs e)
        {
            MPbtnclick = 3;
            LoadPRMgrPGrid();
        }

        void LoadPRMgrCGrid()
        {
            if (MCbtnclick == 1)
            {
                btnAllMC.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                LoadPRRequestCompletedGridView_AllCurrentLastmonth("all");
            }

            else if (MCbtnclick == 2)
            {
                btnAllMC.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-secondary";
                btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                LoadPRRequestCompletedGridView_AllCurrentLastmonth("current");
            }

            else if (MCbtnclick == 3)
            {
                btnAllMC.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                btnLastMonthMC.CssClass = "btn btn-xs btn-secondary";

                LoadPRRequestCompletedGridView_AllCurrentLastmonth("last");
            }
            else
            {
                btnAllMC.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                LoadPRRequestCompletedGridView_AllCurrentLastmonth("all");
            }
        }

        protected void btnAllMC_Click(object sender, EventArgs e)
        {
            MCbtnclick = 1;
            LoadPRMgrCGrid();
        }

        protected void btnCurrentMonthMC_Click(object sender, EventArgs e)
        {
            MCbtnclick = 2;
            LoadPRMgrCGrid();
        }

        protected void btnLastMonthMC_Click(object sender, EventArgs e)
        {
            MCbtnclick = 3;
            LoadPRMgrCGrid();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    LoadPREmpGrid();
                }
                else
                {
                    searchpr();
                    RptrPendingPager.Visible = false;
                }
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
                    prbl prblObj = new prbl();
                    List<prbo> requisitionboList1 = new List<prbo>();
                    EmployeeId = User.Identity.Name;
                    requisitionboList1 = prblObj.Load_ParticularEmpPRDetails(EmployeeId, SelectedType, textSearch, createdon, "EMP");

                    Session.Add("PRGrdInfo", requisitionboList1);

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
                    MsgCls("", LblMsg, Color.Transparent);
                    grdPurchaseItemDetails.Visible = true;
                    grdPurchaseItemDetails.DataSource = requisitionboList1;
                    grdPurchaseItemDetails.SelectedIndex = -1;
                    grdPurchaseItemDetails.DataBind();
                    ////ViewPRIfo.Visible = false;
                    //}
                }

            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }

        }

        protected void txtSearchMP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchMP.Text == "")
                {
                    LoadPRMgrPGrid();
                }
                else
                {
                    searchprMP();
                    RepeatrPRAppPending.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }

        public void searchprMP()
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

                    prbl prblObj = new prbl();
                    List<prbo> requisitionboList1 = new List<prbo>();
                    EmployeeId = User.Identity.Name;
                    requisitionboList1 = prblObj.Load_ManagerParticularEmpPRDetails(EmployeeId, SelectedType, textSearch, createdon);

                    Session.Add("PRGrdInfo", requisitionboList1);

                    //if (requisitionboList1 == null || requisitionboList1.Count == 0)
                    //{
                    //    MsgCls("No Records found", lblMessageBoard, Color.Red);
                    //    grdPRAppRej.Visible = false;
                    //    grdPRAppRej.DataSource = null;
                    //    grdPRAppRej.DataBind();
                    //    return;
                    //}
                    //else
                    //{
                    MsgCls("", lblMessageBoard, Color.Transparent);
                    grdPRAppRej.Visible = true;
                    grdPRAppRej.DataSource = requisitionboList1;
                    grdPRAppRej.SelectedIndex = -1;
                    grdPRAppRej.DataBind();
                    ////ViewPRIfo.Visible = false;
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
                    LoadPRMgrCGrid();
                }
                else
                {
                    searchprMC();
                    RepetrCompl.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }
        }

        public void searchprMC()
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

                    prbl prblObj = new prbl();
                    List<prbo> requisitionboList1 = new List<prbo>();
                    EmployeeId = User.Identity.Name;
                    requisitionboList1 = prblObj.Load_ParticularMngrPRDetails(EmployeeId, SelectedType, textSearch, createdon);

                    Session.Add("PRGrdInfo", requisitionboList1);

                    //if (requisitionboList1 == null || requisitionboList1.Count == 0)
                    //{
                    //    MsgCls("No Records found", lblMessageBoard, Color.Red);
                    //    grdPRAppRejC.Visible = false;
                    //    grdPRAppRejC.DataSource = null;
                    //    grdPRAppRejC.DataBind();
                    //    return;
                    //}
                    //else
                    //{
                    MsgCls("", lblMessageBoard, Color.Transparent);
                    grdPRAppRejC.Visible = true;
                    grdPRAppRejC.DataSource = requisitionboList1;
                    grdPRAppRejC.SelectedIndex = -1;
                    grdPRAppRejC.DataBind();
                    ////ViewPRIfo.Visible = false;
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
            grdPurchaseItemDetails.PageSize = Convert.ToInt32(ddlPagesizeEmp.SelectedValue);
            LoadPREmpGrid();
        }

        protected void ddlPagesizeMgrP_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPRAppRej.PageSize = Convert.ToInt32(ddlPagesizeMgrP.SelectedValue);
            LoadPRMgrPGrid();
        }

        protected void ddlPagesizeMgrC_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPRAppRejC.PageSize = Convert.ToInt32(ddlPagesizeMgrC.SelectedValue);
            LoadPRMgrCGrid();
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

                    LoadEmpPRRequestGridView_AllCurrentLastmonth("all");
                }

                else if (Ebtnclick == 2)
                {
                    btnAll.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonth.CssClass = "btn btn-xs btn-secondary";
                    btnLastMonth.CssClass = "btn btn-xs btn-light";

                    LoadEmpPRRequestGridView_AllCurrentLastmonth("current");
                }

                else if (Ebtnclick == 3)
                {
                    btnAll.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                    btnLastMonth.CssClass = "btn btn-xs btn-secondary";

                    LoadEmpPRRequestGridView_AllCurrentLastmonth("last");
                }
                else
                {
                    btnAll.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                    btnLastMonth.CssClass = "btn btn-xs btn-light";

                    LoadEmpPRRequestGridView_AllCurrentLastmonth("all");
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
                if (MPbtnclick == 1)
                {
                    btnAllMP.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                    LoadPRRequestGridView_AllCurrentLastmonth("all");
                }

                else if (MPbtnclick == 2)
                {
                    btnAllMP.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-secondary";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                    LoadPRRequestGridView_AllCurrentLastmonth("current");
                }

                else if (MPbtnclick == 3)
                {
                    btnAllMP.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-secondary";

                    LoadPRRequestGridView_AllCurrentLastmonth("last");
                }
                else
                {
                    btnAllMP.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                    LoadPRRequestGridView_AllCurrentLastmonth("all");
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

                    LoadPRRequestCompletedGridView_AllCurrentLastmonth("all");
                }

                else if (MCbtnclick == 2)
                {
                    btnAllMC.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMC.CssClass = "btn btn-xs btn-secondary";
                    btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                    LoadPRRequestCompletedGridView_AllCurrentLastmonth("current");
                }

                else if (MCbtnclick == 3)
                {
                    btnAllMC.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMC.CssClass = "btn btn-xs btn-secondary";

                    LoadPRRequestCompletedGridView_AllCurrentLastmonth("last");
                }
                else
                {
                    btnAllMC.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                    LoadPRRequestCompletedGridView_AllCurrentLastmonth("all");
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void masschkhead_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdPRAppRej.HeaderRow.FindControl("masschkhead");
            foreach (GridViewRow row in grdPRAppRej.Rows)
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
                CheckBox ChkBoxHeader = (CheckBox)grdPRAppRej.HeaderRow.FindControl("masschkhead");
                for (int i = 0; i < grdPRAppRej.Rows.Count; i++)
                {
                    GridViewRow row = grdPRAppRej.Rows[i];
                    int iexpid = Convert.ToInt32(grdPRAppRej.DataKeys[row.RowIndex].Values["PRID"].ToString());
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                    if (ChkBoxRows.Checked == true)
                    {

                        prbl PrBlObj = new prbl();
                        List<prbo> requisitionboList = new List<prbo>();
                        requisitionboList = PrBlObj.Load_PRItemDetails(iexpid);
                        ViewState["APPROVEDBY1"] = requisitionboList[0].APPROVEDBY1 == null ? "" : requisitionboList[0].APPROVEDBY1.ToString();
                        ViewState["APPROVEDBY2"] = requisitionboList[0].APPROVEDBY2 == null ? "" : requisitionboList[0].APPROVEDBY2.ToString();
                        ViewState["APPROVEDBY3"] = requisitionboList[0].APPROVEDBY3 == null ? "" : requisitionboList[0].APPROVEDBY3.ToString();
                        ViewState["APPROVEDBY4"] = requisitionboList[0].APPROVEDBY4 == null ? "" : requisitionboList[0].APPROVEDBY4.ToString();
                        ViewState["APPROVEDBY5"] = requisitionboList[0].APPROVEDBY5 == null ? "" : requisitionboList[0].APPROVEDBY5.ToString();
                        ViewState["APPROVEDBY6"] = requisitionboList[0].APPROVEDBY6 == null ? "" : requisitionboList[0].APPROVEDBY6.ToString();

                        FV_PRInfoDisplay.DataSource = requisitionboList;
                        FV_PRInfoDisplay.DataBind();
                        requisitionboList = PrBlObj.Load_PRItem(iexpid);

                        GV_PrItems.DataSource = requisitionboList;
                        GV_PrItems.DataBind();

                        int NoGL = 0;
                        bool? Status = true;
                        //if (grdPRAppRej.Rows.Count > 0)
                        //{
                        //foreach (GridViewRow item in grdPRAppRej.Rows)
                        //{
                        if (iexpid != null)
                        {
                            string ReleasedStatus = string.Empty;
                            string PR_id = iexpid.ToString();
                            prbo objBo = new prbo();
                            prbl objBl = new prbl();
                            objBo.BANFN_EXT = iexpid;
                            objBo.APPROVEDBY1 = User.Identity.Name;
                            objBo.COMMENTS1 = "APPROVED";
                            objBo.STATUS = "Approved";

                            prcollectionbo objLst = objBl.Get_Requested_PRStatus(objBo);
                            foreach (prbo probjBo in objLst)
                            {
                                ReleasedStatus = probjBo.RSTATUS;
                            }

                            if (ReleasedStatus == "RELEASED1" || ReleasedStatus == "RELEASED2" || ReleasedStatus == "RELEASED3" ||
                                ReleasedStatus == "RELEASED4" || ReleasedStatus == "RELEASED5" || ReleasedStatus == "RELEASED6" ||
                                ReleasedStatus == "Approved1" || ReleasedStatus == "Approved2" || ReleasedStatus == "Approved3" ||
                                ReleasedStatus == "Approved4" || ReleasedStatus == "Approved5" || ReleasedStatus == "Approved6" ||
                                ReleasedStatus == "Requested")
                            {
                                if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name && (ReleasedStatus == "Requested" || ReleasedStatus == "RELEASED1"))
                                    objBo.STATUS = "Approved1";
                                if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved1" || ReleasedStatus == "RELEASED2"))
                                    objBo.STATUS = "Approved2";
                                if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved2" || ReleasedStatus == "RELEASED3"))
                                    objBo.STATUS = "Approved3";
                                if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved3" || ReleasedStatus == "RELEASED4"))
                                    objBo.STATUS = "Approved4";
                                if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved4" || ReleasedStatus == "RELEASED5"))
                                    objBo.STATUS = "Approved5";
                                if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved5" || ReleasedStatus == "RELEASED6"))
                                    objBo.STATUS = "Approved6";
                                //--------------------------------

                                if (GV_PrItems.Rows.Count > 0)
                                {
                                    foreach (GridViewRow items in GV_PrItems.Rows)
                                    {
                                        if (ReleasedStatus == "Requested")
                                        {
                                            using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
                                            {
                                                if (ddl != null)
                                                {
                                                    if (string.IsNullOrEmpty(ddl.SelectedValue))
                                                    {
                                                        LoadPRRequestGridView();
                                                        throw new NotImplementedException("Please select all the GL account !");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //--------------------------------

                                if (ReleasedStatus == "Requested")
                                {
                                    if (GV_PrItems.Rows.Count > 0)
                                    {
                                        foreach (GridViewRow items in GV_PrItems.Rows)
                                        {

                                            objBo.BNFPO = GV_PrItems.DataKeys[items.RowIndex]["BNFPO"].ToString();

                                            using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
                                            {
                                                if (ddl != null)
                                                {
                                                    objBo.SAKNR = ddl.SelectedValue;
                                                    if (objBo.SAKNR != null)
                                                    {


                                                        if (objBo.SAKNR != "")
                                                        {
                                                            objBl.Update_PR_Status(objBo, ref Status);

                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select GL Account !')", true);
                                                            MsgCls("Please Select GL Account !", lblMessageBoard, Color.Red);
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select GL Account !')", true);
                                                        MsgCls("Please Select GL Account !", lblMessageBoard, Color.Red);
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select GL Account !')", true);
                                                    MsgCls("Please Select GL Account !", lblMessageBoard, Color.Red);
                                                    return;
                                                }
                                            }
                                        }

                                    }

                                    if (Status.Equals(false))
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !')", true);
                                        MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                                        SendMailMethodtToEmp(objBo);
                                        SendMailMethod(objBo);
                                    }


                                }
                                else
                                {
                                    objBl.Update_PR_Status(objBo, ref Status);
                                    if (Status.Equals(false))
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !')", true);
                                        MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                                        SendMailMethodtToEmp(objBo);
                                        SendMailMethod(objBo);
                                    }
                                }
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Release the PR Request to Approve !')", true);
                                return;
                            }
                        }
                        //loadTab2();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !')", true);
                        cnt += 1;
                    }


                }
                loadTab2();
                if (cnt > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !')", true);
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
                CheckBox ChkBoxHeader = (CheckBox)grdPRAppRej.HeaderRow.FindControl("masschkhead");
                for (int i = 0; i < grdPRAppRej.Rows.Count; i++)
                {
                    GridViewRow row = grdPRAppRej.Rows[i];
                    int iexpid = Convert.ToInt32(grdPRAppRej.DataKeys[row.RowIndex].Values["PRID"].ToString());
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                    if (ChkBoxRows.Checked == true)
                    {
                        prbl PrBlObj = new prbl();
                        List<prbo> requisitionboList = new List<prbo>();
                        requisitionboList = PrBlObj.Load_PRItemDetails(iexpid);
                        ViewState["APPROVEDBY1"] = requisitionboList[0].APPROVEDBY1 == null ? "" : requisitionboList[0].APPROVEDBY1.ToString();
                        ViewState["APPROVEDBY2"] = requisitionboList[0].APPROVEDBY2 == null ? "" : requisitionboList[0].APPROVEDBY2.ToString();
                        ViewState["APPROVEDBY3"] = requisitionboList[0].APPROVEDBY3 == null ? "" : requisitionboList[0].APPROVEDBY3.ToString();
                        ViewState["APPROVEDBY4"] = requisitionboList[0].APPROVEDBY4 == null ? "" : requisitionboList[0].APPROVEDBY4.ToString();
                        ViewState["APPROVEDBY5"] = requisitionboList[0].APPROVEDBY5 == null ? "" : requisitionboList[0].APPROVEDBY5.ToString();
                        ViewState["APPROVEDBY6"] = requisitionboList[0].APPROVEDBY6 == null ? "" : requisitionboList[0].APPROVEDBY6.ToString();
                        FV_PRInfoDisplay.DataSource = requisitionboList;
                        FV_PRInfoDisplay.DataBind();
                        requisitionboList = PrBlObj.Load_PRItem(iexpid);

                        GV_PrItems.DataSource = requisitionboList;
                        GV_PrItems.DataBind();

                        bool? Status = true;

                        if (iexpid != null)
                        {
                            string ReleasedStatus = string.Empty;
                            string PR_id = iexpid.ToString();
                            prbo objBo = new prbo();
                            prbl objBl = new prbl();
                            objBo.BANFN_EXT = iexpid;
                            objBo.COMMENTS1 = "Rejected";
                            objBo.APPROVEDBY1 = User.Identity.Name;
                            objBo.STATUS = "Rejected";
                            objBo.SAKNR = string.Empty;
                            prcollectionbo objLst = objBl.Get_Requested_PRStatus(objBo);
                            foreach (prbo probjBo in objLst)
                            {
                                ReleasedStatus = probjBo.RSTATUS;
                            }

                            if (ReleasedStatus == "RELEASED1" || ReleasedStatus == "RELEASED2" || ReleasedStatus == "RELEASED3" ||
                               ReleasedStatus == "RELEASED4" || ReleasedStatus == "RELEASED5" || ReleasedStatus == "RELEASED6" ||
                               ReleasedStatus == "Approved1" || ReleasedStatus == "Approved2" || ReleasedStatus == "Approved3" ||
                               ReleasedStatus == "Approved4" || ReleasedStatus == "Approved5" || ReleasedStatus == "Approved6" ||
                               ReleasedStatus == "Requested")
                            {
                                if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name && (ReleasedStatus == "Requested" || ReleasedStatus == "RELEASED1"))
                                    objBo.STATUS = "Rejected1";
                                if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved1" || ReleasedStatus == "RELEASED2"))
                                    objBo.STATUS = "Rejected2";
                                if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved2" || ReleasedStatus == "RELEASED3"))
                                    objBo.STATUS = "Rejected3";
                                if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved3" || ReleasedStatus == "RELEASED4"))
                                    objBo.STATUS = "Rejected4";
                                if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved4" || ReleasedStatus == "RELEASED5"))
                                    objBo.STATUS = "Rejected5";
                                if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved5" || ReleasedStatus == "RELEASED6"))
                                    objBo.STATUS = "Rejected6";


                                if (GV_PrItems.Rows.Count > 0)
                                {
                                    foreach (GridViewRow items in GV_PrItems.Rows)
                                    {
                                        objBo.BNFPO = GV_PrItems.DataKeys[items.RowIndex]["BNFPO"].ToString();
                                        using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
                                        {
                                            objBo.SAKNR = ddl.SelectedValue;
                                            objBl.Update_PR_Status(objBo, ref Status);
                                            if (Status.Equals(false))
                                            {
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Rejected successfully !')", true);
                                                RejectSendMailMethodtToEmp(objBo);
                                                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                                            }
                                        }

                                    }
                                }
                            }
                            else
                            {
                                MsgCls("Please Release the PR Request to Reject !", lblMessageBoard, Color.Red);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Release the PR Request to Reject !')", true);
                                return;
                            }
                        }
                        //loadTab2();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Rejected successfully !')", true);
                        cnt += 1;
                    }

                }
                loadTab2();
                if (cnt > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Rejected successfully !')", true);
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void SendMailMethodtToEmp(prbo objBo)
        {
            try
            {
                pnlHide.Visible = true;
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                FV_PRInfoDisplay.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = false;
                GV_PrItems.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = true;

                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVED_BY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string IEMP_Name = "";
                string IEMP_Email = "";
                string REMP_Name = "";
                string REMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                string RequesterID = "";
                string purcharEmailID = "Purchase@subex.com";

                prdbmlDataContext objcontext = new prdbmlDataContext();

                objcontext.sp_Get_MailList_PRApp(objBo.BANFN_EXT, objBo.APPROVEDBY1, objBo.STATUS, ref CREATED_BY, ref APPROVED_BY1, ref Approver_Name,
                    ref Approver_Email, ref IEMP_Name, ref IEMP_Email, ref RequesterID, ref REMP_Name, ref REMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name);

                if (APPROVED_BY1 != "")
                {

                    if (IEMP_Email == REMP_Email)
                    {
                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + " and sent to " + Approver_Name + " for approval";

                        RecipientsString = REMP_Email;
                        strPernr_Mail = PRSNTAPPROVEDBY_Email;

                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + " and sent to " + Approver_Name + "  |  " + APPROVED_BY1 + "  for approval.<br/><br/></b>";
                        body += "<b>PR Requisition Details :<hr /></b>" + sw1.ToString() + "<br/>";

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

                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }
                    else
                    {
                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + " and sent to " + Approver_Name + " for approval";

                        RecipientsString = REMP_Email;
                        strPernr_Mail = PRSNTAPPROVEDBY_Email + "," + IEMP_Email;

                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + " and sent to " + Approver_Name + "  |  " + APPROVED_BY1 + "  for approval.<br/><br/></b>";
                        body += "<b>PR Requisition Details :<hr /></b>" + sw1.ToString() + "<br/>";

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


                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }

                }

                else
                {

                    if (IEMP_Email == REMP_Email)
                    {
                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name;

                        RecipientsString = REMP_Email;
                        strPernr_Mail = PRSNTAPPROVEDBY_Email + "," + purcharEmailID;

                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + " <br/><br/></b>";
                        body += "<b>PR Requisition Details :<hr /></b>" + sw1.ToString() + "<br/>";

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

                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }
                    else
                    {
                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name;

                        RecipientsString = REMP_Email;
                        strPernr_Mail = PRSNTAPPROVEDBY_Email + "," + IEMP_Email + "," + purcharEmailID;

                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + "<br/><br/></b>";
                        body += "<b>PR Requisition Details :<hr /></b>" + sw1.ToString() + "<br/>";

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


                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }
                }
                pnlHide.Visible = false;
            }
            catch
            {
                pnlHide.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "PR Approved Successfully. Error in sending mail.";
                return;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private void SendMailMethod(prbo objBo)
        {
            try
            {
                pnlHide.Visible = true;
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //StringWriter sw2 = new StringWriter();
                //HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                //  FV_PRInfoDisplay.RenderControl(hw);


                FV_PRInfoDisplay.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = false;
                GV_PrItems.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = true;
                //FV_PRInfoDisplay.RenderControl(hw1);
                //GV_PrItems.RenderControl(hw1);
                // StringReader sr = new StringReader(sw1.ToString() + sw2.ToString());

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
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                string RequesterID = "";
                prdbmlDataContext objcontext = new prdbmlDataContext();

                objcontext.sp_Get_MailList_PRApp(objBo.BANFN_EXT, objBo.APPROVEDBY1, objBo.STATUS, ref CREATED_BY, ref APPROVED_BY1, ref Approver_Name,
           ref Approver_Email, ref IEMP_Name, ref IEMP_Email, ref RequesterID, ref REMP_Name, ref REMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name);


                if (IEMP_Email == REMP_Email)
                {
                    if (Approver_Email != null)
                    {

                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been Raised by " + REMP_Name;

                        RecipientsString = Approver_Email;
                        strPernr_Mail = REMP_Email;

                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been Raised by " + REMP_Name + "  |  " + RequesterID + "<br/><br/>";
                        body += "<b>PR Requisition Details :</b><hr />" + sw1.ToString() + "<br/>";


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

                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }
                }
                else
                {
                    if (Approver_Email != null)
                    {

                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been Raised by Indentor " + IEMP_Name + " for the Requestor " + REMP_Name;

                        RecipientsString = Approver_Email;
                        strPernr_Mail = REMP_Email + "," + IEMP_Email;


                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been Raised by Indentor " + IEMP_Name + "  |  " + CREATED_BY + " for the Requestor " + REMP_Name + "  |  " + RequesterID + "<br/><br/>";
                        body += "<b>PR Requisition Details :</b><hr />" + sw1.ToString() + "<br/>";


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


                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }
                } pnlHide.Visible = false;
            }
            catch
            {
                pnlHide.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "PR Approved Successfully. Error in sending mail.";
                return;
            }
        }

        private void RejectSendMailMethodtToEmp(prbo objBo)
        {
            try
            {
                pnlHide.Visible = true;
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //StringWriter sw2 = new StringWriter();
                //HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                //  FV_PRInfoDisplay.RenderControl(hw);

                //FV_PRInfoDisplay.RenderControl(hw1);
                //GV_PrItems.RenderControl(hw1);

                FV_PRInfoDisplay.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = false;
                GV_PrItems.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = true;

                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVED_BY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string IEMP_Name = "";
                string IEMP_Email = "";
                string REMP_Name = "";
                string REMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                string RequesterID = "";

                prdbmlDataContext objcontext = new prdbmlDataContext();

                objcontext.sp_Get_MailList_PRApp(objBo.BANFN_EXT, objBo.APPROVEDBY1, objBo.STATUS, ref CREATED_BY, ref APPROVED_BY1, ref Approver_Name,
                  ref Approver_Email, ref IEMP_Name, ref IEMP_Email, ref RequesterID, ref REMP_Name, ref REMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name);

                if (IEMP_Email == REMP_Email)
                {
                    strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been Rejected by " + PRSNTAPPROVEDBY_Name;

                    RecipientsString = REMP_Email;
                    strPernr_Mail = PRSNTAPPROVEDBY_Email;

                    //    //Preparing the mail body--------------------------------------------------

                    string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been rejected by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + "<br/><br/>";
                    body += "<b>PR Requisition Details :</b><hr />" + sw1.ToString() + "<br/>";


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

                    //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    //lblMessageBoard.Text = "Mail sent successfully.";
                }
                else
                {
                    strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been Rejected by " + PRSNTAPPROVEDBY_Name;

                    RecipientsString = PRSNTAPPROVEDBY_Email;
                    strPernr_Mail = REMP_Email + "," + IEMP_Email;

                    //    //Preparing the mail body--------------------------------------------------
                    string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been rejected by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + "<br/><br/>";
                    body += "<b>PR Requisition Details :<hr />" + sw1.ToString() + "</b><br/>";


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

                    //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    //lblMessageBoard.Text = "Mail sent successfully.";
                }
                pnlHide.Visible = false;
            }
            catch
            {
                pnlHide.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "PR Rejected Successfully. Error in sending mail.";
                return;
            }
        }

        protected void clearSearch()
        {
            txtsearch.Text = "";
            txtSearchMC.Text = "";
            txtSearchMP.Text = "";
        }
    }
}
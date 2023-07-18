using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;
using System.Threading;
using System.IO;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class iExpense_Request : System.Web.UI.Page
    {
        int Ebtnclick = 1;
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
                LoadIEEmpGrid(); //LoadIExpenseGridView();
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
                else
                {
                    Session["_MainSearchValue"] = "";
                }
            }
            int cnt = 0;
            foreach (GridViewRow row in grdAppRejIexp.Rows)
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
            MainView.ActiveViewIndex = 0;
            LoadIEEmpGrid();// LoadIExpenseGridView();
            clearStext();
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            loadTab2();
            clearStext();
        }

        void loadTab2()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link active p-2";
            Tab3.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 1;
            //LoadPRRequestGridView();
            LoadIEMgrPGrid();//LoadIExpenseMPGridView();

            int cnt = 0;
            foreach (GridViewRow row in grdAppRejIexp.Rows)
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
            clearStext();
        }

        void loadTab3()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link active p-2";
            MainView.ActiveViewIndex = 2;
            //LoadPRRequestCompletedGridView();
            LoadIEMgrCGrid(); //LoadIExpenseMCGridView();
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
                IexpboList1 = ExpenseblObj.Load_ExpenseDetails(ApproverId);
                Session.Add("IexpGrdInfo", IexpboList1);

                //if (IexpboList1 == null || IexpboList1.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdIexpdetails.Visible = false;
                //    grdIexpdetails.DataSource = null;
                //    grdIexpdetails.DataBind();
                //    return;
                //}
                //else
                //{
                grdIexpdetails.Visible = true;
                grdIexpdetails.DataSource = IexpboList1;
                grdIexpdetails.SelectedIndex = -1;
                grdIexpdetails.DataBind();
                //}


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadIExpenseGridView_AllCurrentLastmonth(string month)
        {
            try
            {
                int? rCnt = 0;
                PagerSz = Convert.ToInt32(ddlPagesizeEmp.SelectedItem.Text);
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();
                string ApproverId = User.Identity.Name;
                IexpboList1 = ExpenseblObj.Load_ExpenseDetails_AllCurrentLastmonth_Rpager(ApproverId, month, PendingPageIndex, PagerSz, ref rCnt);
                Session.Add("IexpGrdInfo", IexpboList1);

                //if (IexpboList1 == null || IexpboList1.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdIexpdetails.Visible = false;
                //    grdIexpdetails.DataSource = null;
                //    grdIexpdetails.DataBind();
                //    return;
                //}
                //else
                //{
                grdIexpdetails.Visible = true;
                grdIexpdetails.DataSource = IexpboList1;
                grdIexpdetails.SelectedIndex = -1;
                grdIexpdetails.DataBind();
                //}

                RptrPendingPager.Visible = IexpboList1.Count <= 0 ? false : true;
                PopulatePendingPager(IexpboList1.Count > 0 ? int.Parse(rCnt.ToString()) : 0, PendingPageIndex, RptrPendingPager);

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

        protected void grdIexpdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdIexpdetails.PageIndex = e.NewPageIndex;

            LoadIEEmpGrid();//LoadIExpenseGridView();
            ////searchdetails();
            grdIexpdetails.SelectedIndex = -1;
        }

        protected void grdIexpdetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    //case "EDIT":
                    case "COPY":
                        int rowIndex1 = Convert.ToInt32(e.CommandArgument);
                        int IEXP_ID = int.Parse(grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());
                        Session["IEXPID"] = IEXP_ID;
                        Response.Redirect("Other_Reimbursements.aspx?NC=" + "C");
                        break;

                    case "EDIT":
                        int rowIndexEdit = Convert.ToInt32(e.CommandArgument);
                        int IEXP_IDEdit = int.Parse(grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());
                        Session["IEXPID"] = IEXP_IDEdit;
                        Response.Redirect("Saved_Other_Reimbursements.aspx?NC=" + "E");
                        break;


                    case "VIEW":

                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        ////foreach (GridViewRow gvrow in grdIexpdetails.Rows)
                        ////{
                        ////    gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex) ?
                        ////    System.Drawing.Color.LightGray :
                        ////    System.Drawing.Color.White;
                        ////}

                        int row = int.Parse(e.CommandArgument.ToString());

                        ////ViewState["rowid"] = row;

                        int IEXP_ID1 = int.Parse(grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());


                        Session["IEXPID"] = IEXP_ID1;
                        Response.Redirect("Other_Reimbursement_View.aspx?NC=" + "C");

                        break;

                    case "STATUS":

                        int rowIndex2 = Convert.ToInt32(e.CommandArgument);

                        ////foreach (GridViewRow gvrow in grdIexpdetails.Rows)
                        ////{
                        ////    gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex2) ?
                        ////    System.Drawing.Color.LightGray :
                        ////    System.Drawing.Color.White;
                        ////}

                        ////ViewPRIfo.Visible = true;
                        int IEXP_ID2 = int.Parse(grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());

                        OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                        List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                        IexpboList = ExpenseblObj.Load_IexpenseStatusDetails(IEXP_ID2);
                        ltClaimid.Text = IEXP_ID2.ToString();
                        grdAppRejHistory.DataSource = IexpboList;
                        grdAppRejHistory.DataBind();

                        ModalPopupExtender1.Show();
                        break;

                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
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


            }

            grdIexpdetails.DataSource = IexpboList;
            grdIexpdetails.DataBind();

            Session.Add("IexpGrdInfo", IexpboList);
        }

        private void LoadIExpenseMPGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();
                string ApproverId = User.Identity.Name;
                IexpboList1 = ExpenseblObj.Load_ExpenseDetails(ApproverId, "");
                IexpboList.AddRange(IexpboList1);
                Session.Add("IexpGrdInfo", IexpboList);

                //if (IexpboList == null || IexpboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdAppRejIexp.Visible = false;
                //    grdAppRejIexp.DataSource = null;
                //    ////lblRemarks.Visible = false;
                //    ////TxtRemarks.Visible = false;
                //    ////btnApprove.Visible = false;
                //    ////btnReject.Visible = false;
                //    return;
                //}
                //else
                //{
                grdAppRejIexp.Visible = true;
                grdAppRejIexp.DataSource = IexpboList;
                grdAppRejIexp.SelectedIndex = -1;
                ////lblRemarks.Visible = true;
                ////TxtRemarks.Visible = true;
                ////btnApprove.Visible = true;
                ////btnReject.Visible = true;
                //}
                grdAppRejIexp.DataBind();

                ////PnlIExpDetalsView.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadIExpenseMPGridView_AllCurrentLastmonth(string month)
        {
            try
            {
                int? rCnt = 0;
                PagerSz = Convert.ToInt32(ddlPagesizeMgrP.SelectedItem.Text);
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();
                string ApproverId = User.Identity.Name;
                IexpboList1 = ExpenseblObj.Load_ExpenseDetails_AllCurrentLastmonth_Rpager(ApproverId, "", month, PendingPageIndex, PagerSz, ref rCnt);
                IexpboList.AddRange(IexpboList1);
                Session.Add("IexpGrdInfo", IexpboList);

                //if (IexpboList == null || IexpboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdAppRejIexp.Visible = false;
                //    grdAppRejIexp.DataSource = null;
                //    ////lblRemarks.Visible = false;
                //    ////TxtRemarks.Visible = false;
                //    ////btnApprove.Visible = false;
                //    ////btnReject.Visible = false;
                //    return;
                //}
                //else
                //{
                grdAppRejIexp.Visible = true;
                grdAppRejIexp.DataSource = IexpboList;
                grdAppRejIexp.SelectedIndex = -1;
                ////lblRemarks.Visible = true;
                ////TxtRemarks.Visible = true;
                ////btnApprove.Visible = true;
                ////btnReject.Visible = true;
                //}
                grdAppRejIexp.DataBind();
                RepeatrPRAppPending.Visible = IexpboList.Count <= 0 ? false : true;
                PopulatePendingPager(IexpboList.Count > 0 ? int.Parse(rCnt.ToString()) : 0, PendingPageIndex, RepeatrPRAppPending);

                ////PnlIExpDetalsView.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadIExpenseMCGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();
                string ApproverId = User.Identity.Name;
                IexpboList1 = ExpenseblObj.Load_ExpenseDetails_MC(ApproverId, "");
                IexpboList.AddRange(IexpboList1);
                Session.Add("IexpGrdInfo", IexpboList);

                //if (IexpboList == null || IexpboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdAppRejIexpC.Visible = false;
                //    grdAppRejIexpC.DataSource = null;
                //    ////lblRemarks.Visible = false;
                //    ////TxtRemarks.Visible = false;
                //    ////btnApprove.Visible = false;
                //    ////btnReject.Visible = false;
                //    return;
                //}
                //else
                //{
                grdAppRejIexpC.Visible = true;
                grdAppRejIexpC.DataSource = IexpboList;
                grdAppRejIexpC.SelectedIndex = -1;
                ////lblRemarks.Visible = true;
                ////TxtRemarks.Visible = true;
                ////btnApprove.Visible = true;
                ////btnReject.Visible = true;
                //}
                grdAppRejIexpC.DataBind();

                ////PnlIExpDetalsView.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void LoadIExpenseMCGridView_AllCurrentLastmonth(string month)
        {
            try
            {
                int? rCnt = 0;
                PagerSz = Convert.ToInt32(ddlPagesizeMgrC.SelectedItem.Text);
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();
                string ApproverId = User.Identity.Name;
                IexpboList1 = ExpenseblObj.Load_ExpenseDetails_MC_AllCurrentLastmonth_Rpager(ApproverId, "", month, PendingPageIndex, PagerSz, ref rCnt);
                IexpboList.AddRange(IexpboList1);
                Session.Add("IexpGrdInfo", IexpboList);

                //if (IexpboList == null || IexpboList.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    grdAppRejIexpC.Visible = false;
                //    grdAppRejIexpC.DataSource = null;
                //    ////lblRemarks.Visible = false;
                //    ////TxtRemarks.Visible = false;
                //    ////btnApprove.Visible = false;
                //    ////btnReject.Visible = false;
                //    return;
                //}
                //else
                //{
                grdAppRejIexpC.Visible = true;
                grdAppRejIexpC.DataSource = IexpboList;
                grdAppRejIexpC.SelectedIndex = -1;
                ////lblRemarks.Visible = true;
                ////TxtRemarks.Visible = true;
                ////btnApprove.Visible = true;
                ////btnReject.Visible = true;
                //}
                grdAppRejIexpC.DataBind();
                RepetrCompl.Visible = IexpboList.Count <= 0 ? false : true;
                PopulatePendingPager(IexpboList.Count > 0 ? int.Parse(rCnt.ToString()) : 0, PendingPageIndex, RepetrCompl);
                ////PnlIExpDetalsView.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void grdAppRejIexp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdAppRejIexp.PageIndex = e.NewPageIndex;

            LoadIEMgrPGrid();// LoadIExpenseGridView();
            ////searchdetails();
            grdAppRejIexp.SelectedIndex = -1;
        }

        protected void grdAppRejIexp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {


                    case "UPDATEPDATE":
                        OtherReimbursementsbl ExpenseblObj2 = new OtherReimbursementsbl();
                        List<OtherReimbursementsbo> IexpboList2 = new List<OtherReimbursementsbo>();
                        OtherReimbursementsbo IexpboLis2 = new OtherReimbursementsbo();
                        int IEXP_ID2 = int.Parse(grdAppRejIexp.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());

                        using (TextBox txtpdate = (TextBox)grdAppRejIexp.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("txtStartDate"))
                        {

                            if (txtpdate.Text != "")
                            {
                                HF_Pdate.Value = txtpdate.Text;
                                ////IexpboLis2.P_DATE = DateTime.Parse(txtpdate.Text);
                                IexpboLis2.P_DATE = DateTime.ParseExact(txtpdate.Text, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                IexpboLis2.IEXP_ID = IEXP_ID2;
                                ExpenseblObj2.IexpReq_fivalUpdatePdate(IexpboLis2);
                                IexpboList2 = ExpenseblObj2.Load_ExpenseDetails(User.Identity.Name, "");
                                grdAppRejIexp.DataSource = IexpboList2;
                                grdAppRejIexp.DataBind();
                                MsgCls("Posting Date updated successfully!", lblMessageBoard, Color.Green);
                                txtpdate.Visible = true;
                                txtpdate.Enabled = false;
                            }
                            else
                            {
                                MsgCls("Please select Posting Date!", lblMessageBoard, Color.Red);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please select and update Posting Date!');", true);
                            }

                        }

                        break;


                    case "VIEW":
                        try
                        {
                            int rowIndex = Convert.ToInt32(e.CommandArgument);

                            ////foreach (GridViewRow gvrow in grdAppRejIexp.Rows)
                            ////{
                            ////    gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex) ?
                            ////    System.Drawing.Color.LightGray :
                            ////    System.Drawing.Color.White;
                            ////}

                            int row = int.Parse(e.CommandArgument.ToString());

                            ////ViewState["rowid"] = row;

                            int IEXP_ID1 = int.Parse(grdAppRejIexp.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());


                            Session["IEXPID"] = IEXP_ID1;
                            Response.Redirect("OtherReimbursement_Mngr_AppRej.aspx");
                        }
                        catch (Exception Ex)
                        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

                        break;

                    case "STATUS":

                        int rowIndex2 = Convert.ToInt32(e.CommandArgument);

                        ////foreach (GridViewRow gvrow in grdAppRejIexp.Rows)
                        ////{
                        ////    gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex2) ?
                        ////    System.Drawing.Color.LightGray :
                        ////    System.Drawing.Color.White;
                        ////}

                        ////ViewPRIfo.Visible = true;
                        int IEXP_ID3 = int.Parse(grdAppRejIexp.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());

                        OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                        List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                        IexpboList = ExpenseblObj.Load_IexpenseStatusDetails(IEXP_ID3);
                        ltClaimid.Text = IEXP_ID3.ToString();
                        grdAppRejHistory.DataSource = IexpboList;
                        grdAppRejHistory.DataBind();

                        ModalPopupExtender2.Show();
                        break;

                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void grdAppRejIexp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {


                    if (User.Identity.Name.StartsWith("fiadval"))
                    {

                        grdAppRejIexp.Columns[10].Visible = true;
                        grdAppRejIexp.Columns[11].Visible = true;
                        using (TextBox txtdate = (TextBox)e.Row.FindControl("txtStartDate"))
                        using (LinkButton ltnupdatedate = (LinkButton)e.Row.FindControl("LbtnUpdatePdate"))

                        //using (RequiredFieldValidator RFtxtdate = (RequiredFieldValidator)e.Row.FindControl("RFV_txtStartDate"))
                        {
                            txtdate.Visible = true;
                            ltnupdatedate.Visible = true;
                            // RFtxtdate.Enabled = true;
                        }
                    }
                    else
                    {
                        grdAppRejIexp.Columns[10].Visible = false;
                        grdAppRejIexp.Columns[11].Visible = false;

                        using (TextBox txtdate = (TextBox)e.Row.FindControl("txtStartDate"))
                        using (LinkButton ltnupdatedate = (LinkButton)e.Row.FindControl("LbtnUpdatePdate"))

                        // using (RequiredFieldValidator RFtxtdate = (RequiredFieldValidator)e.Row.FindControl("RFV_txtStartDate"))
                        {
                            txtdate.Visible = false;
                            ltnupdatedate.Visible = false;
                            //   RFtxtdate.Enabled = false;
                        }
                    }


                    Label lbl = (Label)e.Row.FindControl("grdlblTask");
                    if (lbl.Text.ToString().Trim().StartsWith("Billable"))
                    {
                        lbl.Font.Bold = true;
                        lbl.ForeColor = Color.DarkSlateBlue;
                        e.Row.Cells[6].BackColor = Color.FromName("#FFF9AE");

                        //e.Row.BackColor = Color.Yellow;
                    }
                }

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, lblMessageBoard, Color.Red); }
        }

        protected void grdAppRejIexp_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void grdAppRejIexpC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

                    case "VIEW":
                        try
                        {
                            int rowIndex = Convert.ToInt32(e.CommandArgument);

                            ////foreach (GridViewRow gvrow in grdAppRejIexpC.Rows)
                            ////{
                            ////    gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex) ?
                            ////    System.Drawing.Color.LightGray :
                            ////    System.Drawing.Color.White;
                            ////}

                            int row = int.Parse(e.CommandArgument.ToString());

                            ////ViewState["rowid"] = row;

                            int IEXP_ID1 = int.Parse(grdAppRejIexpC.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());


                            Session["IEXPID"] = IEXP_ID1;
                            Response.Redirect("Other_Reimbursement_View.aspx?NC=" + "C");
                        }
                        catch (Exception Ex)
                        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

                        break;

                    case "STATUS":

                        int rowIndex2 = Convert.ToInt32(e.CommandArgument);

                        ////foreach (GridViewRow gvrow in grdAppRejIexpC.Rows)
                        ////{
                        ////    gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex2) ?
                        ////    System.Drawing.Color.LightGray :
                        ////    System.Drawing.Color.White;
                        ////}

                        ////ViewPRIfo.Visible = true;
                        int IEXP_ID2 = int.Parse(grdAppRejIexpC.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());

                        OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                        List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                        IexpboList = ExpenseblObj.Load_IexpenseStatusDetails(IEXP_ID2);
                        ltClaimid.Text = IEXP_ID2.ToString();
                        grdAppRejHistory.DataSource = IexpboList;
                        grdAppRejHistory.DataBind();

                        ModalPopupExtender3.Show();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void grdAppRejIexpC_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void grdAppRejIexpC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdAppRejIexpC.PageIndex = e.NewPageIndex;

            LoadIEMgrCGrid(); //LoadIExpenseMCGridView();
            ////searchdetails();
            grdAppRejIexpC.SelectedIndex = -1;
        }

        protected void grdAppRejIexpC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {


                    if (User.Identity.Name.StartsWith("fiadval"))
                    {
                        grdAppRejIexpC.Columns[10].Visible = true;

                        using (TextBox txtdate = (TextBox)e.Row.FindControl("txtStartDate"))
                        ////using (LinkButton ltnupdatedate = (LinkButton)e.Row.FindControl("LbtnUpdatePdate"))

                        //using (RequiredFieldValidator RFtxtdate = (RequiredFieldValidator)e.Row.FindControl("RFV_txtStartDate"))
                        {
                            txtdate.Visible = true;
                            ////ltnupdatedate.Visible = true;
                            // RFtxtdate.Enabled = true;
                        }
                    }
                    else
                    {
                        grdAppRejIexpC.Columns[10].Visible = false;

                        using (TextBox txtdate = (TextBox)e.Row.FindControl("txtStartDate"))
                        ////using (LinkButton ltnupdatedate = (LinkButton)e.Row.FindControl("LbtnUpdatePdate"))

                        // using (RequiredFieldValidator RFtxtdate = (RequiredFieldValidator)e.Row.FindControl("RFV_txtStartDate"))
                        {
                            txtdate.Visible = false;
                            ////ltnupdatedate.Visible = false;
                            //   RFtxtdate.Enabled = false;
                        }
                    }
                }


            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, lblMessageBoard, Color.Red); }
        }

        void LoadIEEmpGrid()
        {
            if (Ebtnclick == 1)
            {
                btnAll.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                LoadIExpenseGridView_AllCurrentLastmonth("all");
            }

            else if (Ebtnclick == 2)
            {
                btnAll.CssClass = "btn btn-xs btn-light";
                btnCurrentMonth.CssClass = "btn btn-xs btn-secondary";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                LoadIExpenseGridView_AllCurrentLastmonth("current");
            }

            else if (Ebtnclick == 3)
            {
                btnAll.CssClass = "btn btn-xs btn-light";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-secondary";

                LoadIExpenseGridView_AllCurrentLastmonth("last");
            }
            else
            {
                btnAll.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                btnLastMonth.CssClass = "btn btn-xs btn-light";

                LoadIExpenseGridView_AllCurrentLastmonth("all");
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            Ebtnclick = 1;
            LoadIEEmpGrid();
        }

        protected void btnCurrentMonth_Click(object sender, EventArgs e)
        {
            Ebtnclick = 2;
            LoadIEEmpGrid();
        }

        protected void btnLastMonth_Click(object sender, EventArgs e)
        {
            Ebtnclick = 3;
            LoadIEEmpGrid();
        }

        void LoadIEMgrPGrid()
        {
            if (MPbtnclick == 1)
            {
                btnAllMP.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                LoadIExpenseMPGridView_AllCurrentLastmonth("all");
            }

            else if (MPbtnclick == 2)
            {
                btnAllMP.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-secondary";
                btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                LoadIExpenseMPGridView_AllCurrentLastmonth("current");
            }

            else if (MPbtnclick == 3)
            {
                btnAllMP.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                btnLastMonthMP.CssClass = "btn btn-xs btn-secondary";

                LoadIExpenseMPGridView_AllCurrentLastmonth("last");
            }
            else
            {
                btnAllMP.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                LoadIExpenseMPGridView_AllCurrentLastmonth("all");
            }
        }

        protected void btnAllMP_Click(object sender, EventArgs e)
        {
            MPbtnclick = 1;
            LoadIEMgrPGrid();
        }

        protected void btnCurrentMonthMP_Click(object sender, EventArgs e)
        {
            MPbtnclick = 2;
            LoadIEMgrPGrid();
        }

        protected void btnLastMonthMP_Click(object sender, EventArgs e)
        {
            MPbtnclick = 3;
            LoadIEMgrPGrid();
        }

        void LoadIEMgrCGrid()
        {
            if (MCbtnclick == 1)
            {
                btnAllMC.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                LoadIExpenseMCGridView_AllCurrentLastmonth("all");
            }

            else if (MCbtnclick == 2)
            {
                btnAllMC.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-secondary";
                btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                LoadIExpenseMCGridView_AllCurrentLastmonth("current");
            }

            else if (MCbtnclick == 3)
            {
                btnAllMC.CssClass = "btn btn-xs btn-light";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                btnLastMonthMC.CssClass = "btn btn-xs btn-secondary";

                LoadIExpenseMCGridView_AllCurrentLastmonth("last");
            }
            else
            {
                btnAllMC.CssClass = "btn btn-xs btn-secondary";
                btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                LoadIExpenseMCGridView_AllCurrentLastmonth("all");
            }
        }

        protected void btnAllMC_Click(object sender, EventArgs e)
        {
            MCbtnclick = 1;
            LoadIEMgrCGrid();
        }

        protected void btnCurrentMonthMC_Click(object sender, EventArgs e)
        {
            MCbtnclick = 2;
            LoadIEMgrCGrid();
        }

        protected void btnLastMonthMC_Click(object sender, EventArgs e)
        {
            MCbtnclick = 3;
            LoadIEMgrCGrid();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text == "")
                {
                    LoadIEEmpGrid();
                }
                else
                {
                    searchdetails();
                    RptrPendingPager.Visible = false;
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
                    OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                    List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                    IexpboList = ExpenseblObj.Load_ParticularIexpDetailsforEmployee(User.Identity.Name, SelectedType, textSearch);
                    //if (IexpboList == null || IexpboList.Count == 0)
                    //{
                    //    MsgCls("No Records found", lblMessageBoard, System.Drawing.Color.Red);
                    //    grdIexpdetails.Visible = false;
                    //    grdIexpdetails.DataSource = null;
                    //    grdIexpdetails.DataBind();
                    //    ////PnlIExpDetalsView.Visible = false;

                    //    return;
                    //}
                    //else
                    //{
                    MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
                    grdIexpdetails.Visible = true;
                    grdIexpdetails.DataSource = IexpboList;
                    grdIexpdetails.SelectedIndex = -1;
                    grdIexpdetails.DataBind();
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
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }

        }

        protected void txtSearchMP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchMP.Text == "")
                {
                    LoadIEMgrPGrid();
                }
                else
                {
                    searchdetailsMP();
                    RepeatrPRAppPending.Visible = false;

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

                    OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                    List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                    IexpboList = ExpenseblObj.Load_ParticularIexpDetailsforManager(User.Identity.Name, SelectedType, textSearch);
                    //if (IexpboList == null || IexpboList.Count == 0)
                    //{
                    //    MsgCls("No Records found", lblMessageBoard, System.Drawing.Color.Red);
                    //    grdAppRejIexp.Visible = false;
                    //    grdAppRejIexp.DataSource = null;
                    //    grdAppRejIexp.DataBind();
                    //    ////PnlIExpDetalsView.Visible = false;

                    //    return;
                    //}
                    //else
                    //{
                    MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
                    grdAppRejIexp.Visible = true;
                    grdAppRejIexp.DataSource = IexpboList;
                    grdAppRejIexp.SelectedIndex = -1;
                    grdAppRejIexp.DataBind();
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
                    LoadIEMgrCGrid();
                }
                else
                {
                    searchdetailsMC();
                    RepetrCompl.Visible = false;
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

                    OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                    List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                    IexpboList = ExpenseblObj.Load_IexpDetailsforManager(User.Identity.Name, SelectedType, textSearch);
                    //if (IexpboList == null || IexpboList.Count == 0)
                    //{
                    //    MsgCls("No Records found", lblMessageBoard, System.Drawing.Color.Red);
                    //    grdAppRejIexpC.Visible = false;
                    //    grdAppRejIexpC.DataSource = null;
                    //    grdAppRejIexpC.DataBind();
                    //    ////PnlIExpDetalsView.Visible = false;

                    //    return;
                    //}
                    //else
                    //{
                    MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
                    grdAppRejIexpC.Visible = true;
                    grdAppRejIexpC.DataSource = IexpboList;
                    grdAppRejIexpC.SelectedIndex = -1;
                    grdAppRejIexpC.DataBind();
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
            grdIexpdetails.PageSize = Convert.ToInt32(ddlPagesizeEmp.SelectedValue);
            LoadIEEmpGrid();
        }

        protected void ddlPagesizeMgrP_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAppRejIexp.PageSize = Convert.ToInt32(ddlPagesizeMgrP.SelectedValue);
            LoadIEMgrPGrid();
        }

        protected void ddlPagesizeMgrC_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAppRejIexpC.PageSize = Convert.ToInt32(ddlPagesizeMgrC.SelectedValue);
            LoadIEMgrCGrid();
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

                    LoadIExpenseGridView_AllCurrentLastmonth("all");
                }

                else if (Ebtnclick == 2)
                {
                    btnAll.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonth.CssClass = "btn btn-xs btn-secondary";
                    btnLastMonth.CssClass = "btn btn-xs btn-light";

                    LoadIExpenseGridView_AllCurrentLastmonth("current");
                }

                else if (Ebtnclick == 3)
                {
                    btnAll.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                    btnLastMonth.CssClass = "btn btn-xs btn-secondary";

                    LoadIExpenseGridView_AllCurrentLastmonth("last");
                }
                else
                {
                    btnAll.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonth.CssClass = "btn btn-xs btn-light";
                    btnLastMonth.CssClass = "btn btn-xs btn-light";

                    LoadIExpenseGridView_AllCurrentLastmonth("all");
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

                    LoadIExpenseMPGridView_AllCurrentLastmonth("all");
                }

                else if (MPbtnclick == 2)
                {
                    btnAllMP.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-secondary";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                    LoadIExpenseMPGridView_AllCurrentLastmonth("current");
                }

                else if (MPbtnclick == 3)
                {
                    btnAllMP.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-secondary";

                    LoadIExpenseMPGridView_AllCurrentLastmonth("last");
                }
                else
                {
                    btnAllMP.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthMP.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMP.CssClass = "btn btn-xs btn-light";

                    LoadIExpenseMPGridView_AllCurrentLastmonth("all");
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

                    LoadIExpenseMCGridView_AllCurrentLastmonth("all");
                }

                else if (MCbtnclick == 2)
                {
                    btnAllMC.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMC.CssClass = "btn btn-xs btn-secondary";
                    btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                    LoadIExpenseMCGridView_AllCurrentLastmonth("current");
                }

                else if (MCbtnclick == 3)
                {
                    btnAllMC.CssClass = "btn btn-xs btn-light";
                    btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMC.CssClass = "btn btn-xs btn-secondary";

                    LoadIExpenseMCGridView_AllCurrentLastmonth("last");
                }
                else
                {
                    btnAllMC.CssClass = "btn btn-xs btn-secondary";
                    btnCurrentMonthMC.CssClass = "btn btn-xs btn-light";
                    btnLastMonthMC.CssClass = "btn btn-xs btn-light";

                    LoadIExpenseMCGridView_AllCurrentLastmonth("all");
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void masschkhead_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdAppRejIexp.HeaderRow.FindControl("masschkhead");
            foreach (GridViewRow row in grdAppRejIexp.Rows)
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
                CheckBox ChkBoxHeader = (CheckBox)grdAppRejIexp.HeaderRow.FindControl("masschkhead");
                for (int i = 0; i < grdAppRejIexp.Rows.Count; i++)
                {
                    GridViewRow row = grdAppRejIexp.Rows[i];
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                    if (ChkBoxRows.Checked == true)
                    {

                        int iexpid = Convert.ToInt32(grdAppRejIexp.DataKeys[row.RowIndex].Values["IEXP_ID"].ToString());//grdAppRejIexp.Columns[1].i int.Parse(ViewState["IEXP_ID"].ToString());
                        string project = grdAppRejIexp.DataKeys[row.RowIndex].Values["POST1"].ToString();
                        string Task = grdAppRejIexp.DataKeys[row.RowIndex].Values["TASK"].ToString();

                        string TAmt = grdAppRejIexp.DataKeys[row.RowIndex].Values["RE_AMT"].ToString();
                        string ReAmt = grdAppRejIexp.DataKeys[row.RowIndex].Values["RE_AMT"].ToString();

                        bool? Status = true;
                        string pdate = string.Empty;
                        OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                        OtherReimbursementsbo Iexpbo = new OtherReimbursementsbo();
                        List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                        IexpboList = ExpenseblObj.Load_IexpenseStatusDetails(iexpid);

                        ViewState["APPROVEDBY1"] = IexpboList[0].APPROVED_BY1 == null ? "" : IexpboList[0].APPROVED_BY1.ToString();
                        ViewState["APPROVEDBY2"] = IexpboList[0].APPROVED_BY2 == null ? "" : IexpboList[0].APPROVED_BY2.ToString();
                        ViewState["APPROVEDBY3"] = IexpboList[0].APPROVED_BY3 == null ? "" : IexpboList[0].APPROVED_BY3.ToString();
                        ViewState["APPROVEDBY4"] = IexpboList[0].APPROVED_BY4 == null ? "" : IexpboList[0].APPROVED_BY4.ToString();
                        ViewState["APPROVEDBY5"] = IexpboList[0].APPROVED_BY5 == null ? "" : IexpboList[0].APPROVED_BY5.ToString();
                        ViewState["APPROVEDBY6"] = IexpboList[0].APPROVED_BY6 == null ? "" : IexpboList[0].APPROVED_BY6.ToString();
                        ViewState["APPROVEDBY7"] = IexpboList[0].APPROVED_BY7 == null ? "" : IexpboList[0].APPROVED_BY7.ToString();
                        ViewState["APPROVEDBY8"] = IexpboList[0].APPROVED_BY8 == null ? "" : IexpboList[0].APPROVED_BY8.ToString();
                        ViewState["APPROVEDBY9"] = IexpboList[0].APPROVED_BY9 == null ? "" : IexpboList[0].APPROVED_BY9.ToString();

                        Iexpbo.IEXP_ID = iexpid;
                        Iexpbo.APPROVED_BY1 = User.Identity.Name;
                        Iexpbo.REMARKS1 = "APPROVED";
                        Iexpbo.STATUS = "Approved";

                        if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name)
                            Iexpbo.STATUS = "Approved1";
                        if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name)
                            Iexpbo.STATUS = "Approved2";
                        if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name)
                            Iexpbo.STATUS = "Approved3";
                        if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name)
                            Iexpbo.STATUS = "Approved4";
                        if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name)
                            Iexpbo.STATUS = "Approved5";
                        if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name)
                            Iexpbo.STATUS = "Approved6";
                        if (ViewState["APPROVEDBY7"].ToString() == User.Identity.Name)
                            Iexpbo.STATUS = "Approved7";
                        if (ViewState["APPROVEDBY8"].ToString() == User.Identity.Name)
                            Iexpbo.STATUS = "Approved8";
                        if (ViewState["APPROVEDBY9"].ToString() == User.Identity.Name)
                            Iexpbo.STATUS = "Approved9";

                        if (User.Identity.Name.StartsWith("fiadval"))
                        {
                            OtherReimbursementsdalDataContext objcontext = new OtherReimbursementsdalDataContext();

                            objcontext.sp_Get_Pdate(iexpid, ref pdate);
                            if (string.IsNullOrEmpty(pdate))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select and Update Posting Date!')", true);
                                return;
                            }

                            else
                            {
                                ExpenseblObj.Update_IExpense_Status(Iexpbo, ref Status);
                                if (Status.Equals(false))
                                {
                                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('IExpense Request Approved successfully !');", true);
                                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Iexpense Request Approved successfully !')", true);
                                    MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                                    SendMailMethodtToEmp(Iexpbo, "APPROVED", project, Task, TAmt, ReAmt);
                                    SendMailMethod(Iexpbo, project, Task, TAmt, ReAmt);
                                    //LoadIExpenseGridView();

                                }
                            }
                        }
                        else
                        {
                            ExpenseblObj.Update_IExpense_Status(Iexpbo, ref Status);
                            if (Status.Equals(false))
                            {
                                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Iexpense Request Approved successfully !');", true);
                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Iexpense Request Approved successfully !')", true);
                                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                                SendMailMethodtToEmp(Iexpbo, "APPROVED", project, Task, TAmt, ReAmt);
                                SendMailMethod(Iexpbo, project, Task, TAmt, ReAmt);
                            }
                        }
                        //loadTab2();
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Iexpense Request Approved successfully !');", true);
                        cnt += 1;
                    }
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select record!')", true);
                    //}
                }
                loadTab2();
                if (cnt > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('iExpense Request Approved successfully !')", true);
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
                CheckBox ChkBoxHeader = (CheckBox)grdAppRejIexp.HeaderRow.FindControl("masschkhead");
                for (int i = 0; i < grdAppRejIexp.Rows.Count; i++)
                {
                    GridViewRow row = grdAppRejIexp.Rows[i];
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("masschkrow");
                    if (ChkBoxRows.Checked == true)
                    {
                        int iexpid = Convert.ToInt32(grdAppRejIexp.DataKeys[row.RowIndex].Values["IEXP_ID"].ToString());//grdAppRejIexp.Columns[1].i int.Parse(ViewState["IEXP_ID"].ToString());
                        string project = grdAppRejIexp.DataKeys[row.RowIndex].Values["POST1"].ToString();
                        string Task = grdAppRejIexp.DataKeys[row.RowIndex].Values["TASK"].ToString();

                        string TAmt = grdAppRejIexp.DataKeys[row.RowIndex].Values["RE_AMT"].ToString();
                        string ReAmt = grdAppRejIexp.DataKeys[row.RowIndex].Values["RE_AMT"].ToString();

                        OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                        OtherReimbursementsbo Iexpbo = new OtherReimbursementsbo();
                        List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                        IexpboList = ExpenseblObj.Load_IexpenseStatusDetails(iexpid);

                        ViewState["APPROVEDBY1"] = IexpboList[0].APPROVED_BY1 == null ? "" : IexpboList[0].APPROVED_BY1.ToString();
                        ViewState["APPROVEDBY2"] = IexpboList[0].APPROVED_BY2 == null ? "" : IexpboList[0].APPROVED_BY2.ToString();
                        ViewState["APPROVEDBY3"] = IexpboList[0].APPROVED_BY3 == null ? "" : IexpboList[0].APPROVED_BY3.ToString();
                        ViewState["APPROVEDBY4"] = IexpboList[0].APPROVED_BY4 == null ? "" : IexpboList[0].APPROVED_BY4.ToString();
                        ViewState["APPROVEDBY5"] = IexpboList[0].APPROVED_BY5 == null ? "" : IexpboList[0].APPROVED_BY5.ToString();
                        ViewState["APPROVEDBY6"] = IexpboList[0].APPROVED_BY6 == null ? "" : IexpboList[0].APPROVED_BY6.ToString();
                        ViewState["APPROVEDBY7"] = IexpboList[0].APPROVED_BY7 == null ? "" : IexpboList[0].APPROVED_BY7.ToString();
                        ViewState["APPROVEDBY8"] = IexpboList[0].APPROVED_BY8 == null ? "" : IexpboList[0].APPROVED_BY8.ToString();
                        ViewState["APPROVEDBY9"] = IexpboList[0].APPROVED_BY9 == null ? "" : IexpboList[0].APPROVED_BY9.ToString();

                        bool? Status = true;

                        if (iexpid != null)
                        {
                            Iexpbo.IEXP_ID = iexpid;
                            Iexpbo.APPROVED_BY1 = User.Identity.Name;
                            Iexpbo.REMARKS1 = "Rejected"; //TxtRemarks.Text.Trim();
                            Iexpbo.STATUS = "Rejected";

                            if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name)
                                Iexpbo.STATUS = "Rejected1";
                            if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name)
                                Iexpbo.STATUS = "Rejected2";
                            if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name)
                                Iexpbo.STATUS = "Rejected3";
                            if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name)
                                Iexpbo.STATUS = "Rejected4";
                            if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name)
                                Iexpbo.STATUS = "Rejected5";
                            if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name)
                                Iexpbo.STATUS = "Rejected6";
                            if (ViewState["APPROVEDBY7"].ToString() == User.Identity.Name)
                                Iexpbo.STATUS = "Rejected7";
                            if (ViewState["APPROVEDBY8"].ToString() == User.Identity.Name)
                                Iexpbo.STATUS = "Rejected8";
                            if (ViewState["APPROVEDBY9"].ToString() == User.Identity.Name)
                                Iexpbo.STATUS = "Rejected9";

                            ExpenseblObj.Update_IExpense_Status(Iexpbo, ref Status);
                            if (Status.Equals(false))
                            {
                                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('iExpense Request Rejected successfully !');", true);
                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Iexpense Request Rejected successfully !')", true);
                                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                                RejectSendMailMethodtToEmp(Iexpbo, "Rejected", project, Task, TAmt, ReAmt);
                            }
                        }

                        //loadTab2();
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('iExpense Request Rejected successfully !');", true);
                        cnt += 1;
                    }
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select record!')", true);
                    //}
                }
                loadTab2();
                if (cnt > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('iExpense Request Rejected successfully !')", true);
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void SendMailMethod(OtherReimbursementsbo Iexpbo, string Project, string Task, string Tamt, string Reamt)
        {
            try
            {
                pnlhide.Visible = true;
                int IEXP_ID = Convert.ToInt32(Iexpbo.IEXP_ID);
                ViewState["IEXP_ID"] = IEXP_ID;//int.Parse(grdAppRejIexp.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());


                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                IexpboList = ExpenseblObj.Load_IEXPDetails(IEXP_ID);
                grdIexpDetails1.DataSource = IexpboList;
                grdIexpDetails1.DataBind();

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                for (int i = 0; i < grdIexpDetails1.Rows.Count; i++)
                {
                    GridViewRow row = grdIexpDetails1.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[10].FindControl("cb").Visible = false;
                    row.Cells[10].FindControl("fuAttachments").Visible = false;
                    row.Cells[10].FindControl("fuAttachmentsfname").Visible = false;
                    row.Cells[10].FindControl("LbtnUpload").Visible = false;

                }

                grdIexpDetails1.RenderControl(hw1);



                for (int i = 0; i < grdIexpDetails1.Rows.Count; i++)
                {
                    GridViewRow row = grdIexpDetails1.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[10].FindControl("cb").Visible = true;
                    row.Cells[10].FindControl("fuAttachments").Visible = true;
                    row.Cells[10].FindControl("fuAttachmentsfname").Visible = true;
                    row.Cells[10].FindControl("LbtnUpload").Visible = true;

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
                string Purpose = "";
                string Project_code = "";
                string Entity = "";
                OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();

                objDataContext.sp_Get_MailList_IExpApp(Iexpbo.IEXP_ID, Iexpbo.APPROVED_BY1, Iexpbo.STATUS, ref CREATED_BY, ref APPROVEDBY1, ref Approver_Name,
                 ref Approver_Email, ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref Purpose, ref Project_code, ref Entity);


                if (Approver_Email != null)
                {

                    strSubject = "IExpense Request " + Iexpbo.IEXP_ID + " has been Raised by " + EMP_Name + "  |  " + CREATED_BY + " and is pending for the Approval.";


                    RecipientsString = Approver_Email;
                    strPernr_Mail = EMP_Email;
                    //Preparing the mail body--------------------------------------------------
                    string body = "<b>IExpense Request " + Iexpbo.IEXP_ID + " has been Raised by " + EMP_Name + "  |  " + CREATED_BY + " and is pending for the Approval.<br/><br/></b>";
                    body += "<b>Entity with IExpense ID  :  " + Entity + " : " + Iexpbo.IEXP_ID + "</b><br/><br/>";
                    body += "<b>IExpense Header Details :<hr /><br/>";
                    body += "Project       :  " + Project_code + " - " + Project + "<br/>";
                    body += "Task          :  " + Task + "<br/>";
                    body += "Purpose  :  " + Purpose + "<br/>";
                    body += "Total Reimbursement Amount  :  " + decimal.Parse(Tamt).ToString("#,##0.00") + " ( " + Reamt + " ) <br/><br/>";
                    //body += "Reimbursement Currency      :  " + Reamt + "<br/><br/>";
                    body += "<b>IExpense Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";


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

                } pnlhide.Visible = false;
            }
            catch
            {
                pnlhide.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Error in sending mail.";
                return;
            }
        }

        private void SendMailMethodtToEmp(OtherReimbursementsbo Iexpbo, string Remark, string Project, string Task, string Tamt, string Reamt)
        {
            try
            {
                pnlhide.Visible = true;
                int IEXP_ID = Convert.ToInt32(Iexpbo.IEXP_ID);
                ViewState["IEXP_ID"] = IEXP_ID;//int.Parse(grdAppRejIexp.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                IexpboList = ExpenseblObj.Load_IEXPDetails(IEXP_ID);
                grdIexpDetails1.DataSource = IexpboList;
                grdIexpDetails1.DataBind();

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //StringWriter sw2 = new StringWriter();
                //HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                //  FV_PRInfoDisplay.RenderControl(hw);

                //FV_PRInfoDisplay.RenderControl(hw1);
                for (int i = 0; i < grdIexpDetails1.Rows.Count; i++)
                {
                    GridViewRow row = grdIexpDetails1.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[10].FindControl("cb").Visible = false;
                    row.Cells[10].FindControl("fuAttachments").Visible = false;
                    row.Cells[10].FindControl("fuAttachmentsfname").Visible = false;
                    row.Cells[10].FindControl("LbtnUpload").Visible = false;

                }

                grdIexpDetails1.RenderControl(hw1);



                for (int i = 0; i < grdIexpDetails1.Rows.Count; i++)
                {
                    GridViewRow row = grdIexpDetails1.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[10].FindControl("cb").Visible = true;
                    row.Cells[10].FindControl("fuAttachments").Visible = true;
                    row.Cells[10].FindControl("fuAttachmentsfname").Visible = true;
                    row.Cells[10].FindControl("LbtnUpload").Visible = true;

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
                string Purpose = "";
                string Project_code = "";
                string Entity = "";
                OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();

                objDataContext.sp_Get_MailList_IExpApp(Iexpbo.IEXP_ID, Iexpbo.APPROVED_BY1, Iexpbo.STATUS, ref CREATED_BY, ref APPROVEDBY1, ref Approver_Name,
                 ref Approver_Email, ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref Purpose, ref Project_code, ref Entity);


                strSubject = "IExpense Requisition " + Iexpbo.IEXP_ID + " has been approved by " + PRSNTAPPROVEDBY_Name;

                RecipientsString = EMP_Email;
                strPernr_Mail = PRSNTAPPROVEDBY_Email;

                //GridViewRow selectedrow = grdAppRejIexp.SelectedRow;


                //    //Preparing the mail body--------------------------------------------------

                string body = "<b>IExpense Requisition " + Iexpbo.IEXP_ID + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + Iexpbo.APPROVED_BY1 + "<br/><br/></b>";
                body += "<b>Entity with IExpense ID  :  " + Entity + " : " + Iexpbo.IEXP_ID + "</b><br/><br/>";
                body += "<b>IExpense Header Details :<hr /><br/>";
                body += "Project       :  " + Project_code + " - " + Project + "<br/>";
                body += "Task          :  " + Task + "<br/>";
                body += "Purpose  :  " + Purpose + "<br/>";
                body += "Total Reimbursement Amount  :  " + decimal.Parse(Tamt).ToString("#,##0.00") + " ( " + Reamt + " ) <br/><br/>";
                //body += "Reimbursement Currency      :  " + Reamt + "<br/><br/>";
                body += "<b>IExpense Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/><br/>";
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
                lblMessageBoard.Text = "Iexpense Request Approved successfully and Mail sent successfully.";
                pnlhide.Visible = false;
            }
            catch
            {
                pnlhide.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Iexpense Request Approved successfully. Error in sending Mail.";
                return;
            }
        }

        private void RejectSendMailMethodtToEmp(OtherReimbursementsbo Iexpbo, string Remark, string Project, string Task, string Tamt, string Reamt)
        {
            try
            {
                pnlhide.Visible = true;
                int IEXP_ID = Convert.ToInt32(Iexpbo.IEXP_ID);
                ViewState["IEXP_ID"] = IEXP_ID;//int.Parse(grdAppRejIexp.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                IexpboList = ExpenseblObj.Load_IEXPDetails(IEXP_ID);
                grdIexpDetails1.DataSource = IexpboList;
                grdIexpDetails1.DataBind();

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //StringWriter sw2 = new StringWriter();
                //HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                //  FV_PRInfoDisplay.RenderControl(hw);

                //  FV_PRInfoDisplay.RenderControl(hw1);
                for (int i = 0; i < grdIexpDetails1.Rows.Count; i++)
                {
                    GridViewRow row = grdIexpDetails1.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[10].FindControl("cb").Visible = false;
                    row.Cells[10].FindControl("fuAttachments").Visible = false;
                    row.Cells[10].FindControl("fuAttachmentsfname").Visible = false;
                    row.Cells[10].FindControl("LbtnUpload").Visible = false;

                }

                grdIexpDetails1.RenderControl(hw1);

                for (int i = 0; i < grdIexpDetails1.Rows.Count; i++)
                {
                    GridViewRow row = grdIexpDetails1.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[10].FindControl("cb").Visible = true;
                    row.Cells[10].FindControl("fuAttachments").Visible = true;
                    row.Cells[10].FindControl("fuAttachmentsfname").Visible = true;
                    row.Cells[10].FindControl("LbtnUpload").Visible = true;
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
                string Purpose = "";
                string Project_code = "";
                string Entity = "";
                OtherReimbursementsdalDataContext objDataContext = new OtherReimbursementsdalDataContext();

                objDataContext.sp_Get_MailList_IExpApp(Iexpbo.IEXP_ID, Iexpbo.APPROVED_BY1, Iexpbo.STATUS, ref CREATED_BY, ref APPROVEDBY1, ref Approver_Name,
                 ref Approver_Email, ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref Purpose, ref Project_code, ref Entity);

                strSubject = "IExpense Requisition " + Iexpbo.IEXP_ID + " has been Rejected by " + PRSNTAPPROVEDBY_Name;

                RecipientsString = EMP_Email;
                strPernr_Mail = PRSNTAPPROVEDBY_Email;

                GridViewRow selectedrow = grdAppRejIexp.SelectedRow;

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>IExpense Requisition " + Iexpbo.IEXP_ID + " has been Rejected by " + PRSNTAPPROVEDBY_Name + "  |  " + Iexpbo.APPROVED_BY1 + "<br/><br/></b>";
                body += "<b>Entity with IExpense ID  :  " + Entity + " : " + Iexpbo.IEXP_ID + "</b><br/><br/>";
                body += "<b>IExpense Header Details :<hr /><br/>";
                body += "Project       :  " + Project_code + " - " + Project + "<br/>";
                body += "Task          :  " + Task + "<br/>";
                body += "Purpose  :  " + Purpose + "<br/>";
                body += "Total Reimbursement Amount  :  " + decimal.Parse(Tamt).ToString("#,##0.00") + " ( " + Reamt + " ) <br/><br/>";
                //body += "Reimbursement Currency      :  " + Reamt + "<br/><br/>";
                body += "<b>IExpense Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/><br/>";
                body += "<b>Remarks  :  " + Remark + "</b><br/>";

                //    //End of preparing the mail body-------------------------------------------

                ////Newly added Starts
                Thread email = new Thread(delegate()
                {
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                });

                email.IsBackground = true;
                email.Start();
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Iexpense Request Rejected successfully.');", true);
                lblMessageBoard.Text = "Iexpense Request Rejected successfully.";

                ////Newly added Ends

                ////iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Iexpense Request Rejected and Mail sent successfully.";
                pnlhide.Visible = false;
            }
            catch
            {
                pnlhide.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Iexpense Request Rejected successfully. Error in sending Mail.";
                return;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void clearStext()
        {
            txtsearch.Text = txtSearchMC.Text = txtSearchMP.Text = "";
        }
    }
}
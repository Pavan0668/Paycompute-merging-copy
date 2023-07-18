using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Net;
using System.Globalization;
using System.Threading;
using iEmpPowerMaster_Load;

public partial class UI_Manager_Self_Service_assignedtome : System.Web.UI.Page
{

    public int rselectedindex = -1;
    string strApprover = string.Empty;
    string strApprover_mail = string.Empty;
    string strRequesterMail = string.Empty;
    protected int PagerSpan = 10;
    protected int PendingPageIndex = 1;
    protected int CompleatedPageIndex = 1;
    //string LOGINPERNR = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Form.DefaultButton = btnEntryKey.UniqueID;
        lblMessageBoard.Text = "";
        if (!IsPostBack)
        {
            HF_TABID.Value = "1";
            string id = string.Empty;
            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
            }
            BindYearDropdown();
            LoadSelDropDown();
            if (id == "Leave")
            {
                DDL_HRTabSel.SelectedValue = "1";
            }
            else if (id == "RWT")
            {
                DDL_HRTabSel.SelectedValue = "2";
            }

            if (id == "Completed")
            {
                HF_TABID.Value = "2";
            }
            GetHRPernr();
            ViewState["PendingPageIndex"] = "0";
            ViewState["PendingPageIndex_Compleated"] = "0";
            AllPnelStatus();
            bool bSortedOrder = false;
            Session.Add("bSortedOrder", bSortedOrder);
            ViewState.Add("indexchang", rselectedindex);

            btnRWApprove.Attributes.Add("onclick", " return ValidateControls('" + txtRWComments.ClientID + "');");
            btnRWReject.Attributes.Add("onclick", " return ValidateControls('" + txtRWComments.ClientID + "');");

            if (HF_TABID.Value == "1")
            {
                divView2.Visible = false;
                divView1.Visible = true;
                Tab1.CssClass = "nav-link active p-2";
                Tab2.CssClass = "nav-link p-2";
            }
            else if (HF_TABID.Value == "2")
            {
                divView1.Visible = false;
                divView2.Visible = true;
                grdCompleted.Visible = true;
                Tab2.CssClass = "nav-link active p-2";
                Tab1.CssClass = "nav-link p-2";
            }

        }
    }

    protected void LoadSelDropDown()
    {
        msassignedtomebo objAssginTMBo = new msassignedtomebo();
        msassignedtomebl objAssginTMBl = new msassignedtomebl();


        msassignedtomecollectionbo objAssginTMBolist = objAssginTMBl.Get_HRPERNR(User.Identity.Name.ToString().Trim(), Session["CompCode"].ToString());
        if (objAssginTMBolist.Count > 0)
        {
            foreach (msassignedtomebo objBo in objAssginTMBolist)
            {
                ViewState["LOGINPERNR"] = objBo.PERNR;
            }

            if (ViewState["LOGINPERNR"].ToString().Trim() == User.Identity.Name)
            {
                DDL_HRTabSel.Items.Add(new ListItem("Personal Information", "0"));
                DDL_HRTabSel.Items.Add(new ListItem("Leave/Attd.", "1"));
                //DDL_HRTabSel.Items.Add(new ListItem("Record Working Time", "2"));
            }

            else
            {

                DDL_HRTabSel.Items.Add(new ListItem("Leave/Attd.", "1"));
                DDL_HRTabSel.Items.Add(new ListItem("Record Working Time", "2"));
            }

            DivAppRej.Visible = true;
            HRTabSel.Visible = true;
        }
        else
        {
            DivAppRej.Visible = false;
            HRTabSel.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('HR Admin information is not maintained in SAP!');", true);
        }
    }



    void BindYearDropdown()
    {
        try
        {
            for (int i = -2; i < 1; i++)
            {

                string date = DateTime.Now.AddYears(i).ToString("yyyy");
                DDLYear.Items.Add(date);

            }
            DDLYear.SelectedValue = DateTime.Now.AddYears(0).ToString("yyyy");
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
    }


    protected void GetHRPernr()
    {
        msassignedtomebo objAssginTMBo = new msassignedtomebo();
        msassignedtomebl objAssginTMBl = new msassignedtomebl();


        msassignedtomecollectionbo objAssginTMBolist = objAssginTMBl.Get_HRPERNR(User.Identity.Name.ToString().Trim(), Session["CompCode"].ToString());
        if (objAssginTMBolist.Count > 0)
        {
            foreach (msassignedtomebo objBo in objAssginTMBolist)
            {
                //LOGINPERNR = objBo.PERNR;
                ViewState["LOGINPERNR"] = objBo.PERNR;
            }

            DivAppRej.Visible = true;
            HRTabSel.Visible = true;
            // SUPTabSel.Visible = false;
            LoadGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());
            LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());


        }
        else
        {
            DivAppRej.Visible = false;
            HRTabSel.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('HR Admin information is not maintained in iEmpPower!');", true);
        }
    }

    protected void DDL_HRTabSel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (HF_TABID.Value == "1")
            {
                LoadGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());

                GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_DashboardDetails.DataBind();
                GridViewDetails.DataBind();
                grdRecordTime.DataBind();
                RWTdiv.Visible = false;
                txtRWComments.Text = string.Empty;
                lblValidateRWCommnets.Text = string.Empty;
                TblRemarks.Visible = false;
                lblRemarksRWT.Text = string.Empty;
                lblRemarksRWT.Visible = false;
            }
            else
            {

                LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());
                GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_DashboardDetails.DataBind();
                GridViewDetails.DataBind();
                grdRecordTime.DataBind();
                RWTdiv.Visible = false;
                txtRWComments.Text = string.Empty;
                lblValidateRWCommnets.Text = string.Empty;
                TblRemarks.Visible = false;
                lblRemarksRWT.Text = string.Empty;
                lblRemarksRWT.Visible = false;
            }

            //LoadGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());
            //LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());
            //GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
            //GV_DashboardCompleatedDetails.DataBind();
            //GV_DashboardDetails.DataBind();
            //GridViewDetails.DataBind();
            //grdRecordTime.DataBind();
            //RWTdiv.Visible = false;
            //txtRWComments.Text = string.Empty;
            //lblValidateRWCommnets.Text = string.Empty;
            //TblRemarks.Visible = false;
            //lblRemarksRWT.Text = string.Empty;
            //lblRemarksRWT.Visible = false;
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }

    //protected void DDL_SUPSEL_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //          LoadGridDetails(DDL_SUPSEL.SelectedValue.ToString().Trim());
    //          LoadCompletedGridDetails(DDL_SUPSEL.SelectedValue.ToString().Trim());
    //          GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
    //          GV_DashboardCompleatedDetails.DataBind();
    //          GV_DashboardDetails.DataBind();
    //          GridViewDetails.DataBind();
    //          grdRecordTime.DataBind();
    //          RWTdiv.Visible = false;
    //          txtRWComments.Text = string.Empty;
    //          lblValidateRWCommnets.Text = string.Empty;
    //          TblRemarks.Visible = false;
    //          lblRemarksRWT.Text = string.Empty;
    //          lblRemarksRWT.Visible = false;
    //    }
    //    catch (Exception Ex)
    //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

    //}


    protected void tcAssignToMe_ActiveTabChanged(object sender, EventArgs e)
    {
        AllPnelStatus();
        if (HF_TABID.Value == "1")
        {
            //LoadGridDetails();

            HRTabSel.Visible = true;

            LoadGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());



            //GetHRPernr();
            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;

        }
        else
        {

            HRTabSel.Visible = true;

            LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());

            //LoadCompletedGridDetails();
            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;
        }
    }
    protected void AllPnelStatus()
    {
        GV_DashboardCompleatedDetails.Visible = false;
        GV_DashboardDetails.Visible = false;
        pnlRecordWorking.Visible = false;
    }
    protected void LoadGridDetails(string seltype)
    {
        msassignedtomebo objAssginTMBo = new msassignedtomebo();
        msassignedtomebl objAssginTMBl = new msassignedtomebl();
        objAssginTMBo.PERNR = User.Identity.Name;
        objAssginTMBo.PageSize = PagerSpan;
        objAssginTMBo.PageIndex = PendingPageIndex;
        objAssginTMBo.Flag = 1;
        int? RecCount = 0;
        msassignedtomecollectionbo objAssginTMLst = objAssginTMBl.Load_Pending_Approvals(Session["CompCode"].ToString(), objAssginTMBo, seltype, DDLYear.SelectedValue.ToString().Trim(), ref RecCount);
        if (objAssginTMLst.Count <= 0)
        {
            //lblMessageBoard.Text = GetLocalResourceObject("NoPendingRecord").ToString();
            //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
        }

        grdPending.DataSource = objAssginTMLst;
        grdPending.DataBind();

        string frow = "", lrow = "";
        foreach (GridViewRow row in grdPending.Rows)
        {
            for (int i = 0; i < grdPending.Rows.Count; i++)
            {
                if (i == 0)
                {
                    frow = grdPending.Rows[i].Cells[0].Text;
                }
                if (i == grdPending.Rows.Count - 1)
                {
                    lrow = grdPending.Rows[i].Cells[0].Text;
                }
            }
        }

        divpendingrecordcount.InnerHtml = objAssginTMLst.Count > 0 ? "Showing " + frow + " to " + lrow + " of " + RecCount + " entries" : "";
        divpageNcnt.Visible = objAssginTMLst.Count > 0 ? true : false;
        PopulatePendingPager(objAssginTMLst.Count > 0 ? int.Parse(RecCount.ToString()) : 0, PendingPageIndex);
        Session.Add("grdLst", objAssginTMLst);
        divMassBtn.Visible = false;
    }
    protected void LoadCompletedGridDetails(string seltypec)
    {
        divView2.Visible = true;
        msassignedtomebo objAssginTMBo = new msassignedtomebo();
        msassignedtomebl objAssginTMBl = new msassignedtomebl();
        objAssginTMBo.PERNR = User.Identity.Name;
        objAssginTMBo.PageSize = PagerSpan;
        objAssginTMBo.PageIndex = CompleatedPageIndex;
        objAssginTMBo.Flag = 1;
        int? RecCount = 0;
        msassignedtomecollectionbo objAssginTMCompletedLst = objAssginTMBl.Load_Completed_Approvals(Session["CompCode"].ToString(), objAssginTMBo, seltypec, DDLYear.SelectedValue.ToString().Trim(), ref RecCount);
        if (objAssginTMCompletedLst.Count <= 0)
        {
            //lblMessageBoard.Text = GetLocalResourceObject("NoCompletedRecord").ToString();
            //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
        }        
        grdCompleted.DataSource = objAssginTMCompletedLst;
        grdCompleted.DataBind();
        grdCompleted.Visible = true;

        string frow = "", lrow = "";
        foreach (GridViewRow row in grdCompleted.Rows)
        {
            for (int i = 0; i < grdCompleted.Rows.Count; i++)
            {
                if (i == 0)
                {
                    frow = grdCompleted.Rows[i].Cells[0].Text;
                }
                if (i == grdCompleted.Rows.Count - 1)
                {
                    lrow = grdCompleted.Rows[i].Cells[0].Text;
                }
            }
        }
        divcntshwingcomp.InnerHtml = objAssginTMCompletedLst.Count > 0 ? "Showing " + frow + " to " + lrow + " of " + RecCount + " entries" : "";
        divpageNcnt2.Visible = objAssginTMCompletedLst.Count > 0 ? true : false;
        PopulateCompleatedPager(objAssginTMCompletedLst.Count > 0 ? int.Parse(RecCount.ToString()) : 0, CompleatedPageIndex);
        Session.Add("grdCmpltdLst", objAssginTMCompletedLst);
        divMassBtn.Visible = false;
               
    }

    protected void grdPending_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        int pageindex = e.NewPageIndex;
        grdPending.PageIndex = e.NewPageIndex;
        msassignedtomecollectionbo objPIDashBoardLst = (msassignedtomecollectionbo)Session["grdLst"];
        grdPending.DataSource = objPIDashBoardLst;
        int rselectedindex = Convert.ToInt32(ViewState["indexchang"]);
        int pagindex = Convert.ToInt32(ViewState["pageindex"]);

        grdPending.DataSource = objPIDashBoardLst;
        grdPending.SelectedIndex = -1;
        grdPending.DataBind();
        if (pageindex == pagindex)
        {
            grdPending.SelectedIndex = rselectedindex;
        }
       

    }
    protected void grdPending_Sorting(object sender, GridViewSortEventArgs e)
    {
        divMassBtn.Visible = false;
        msassignedtomecollectionbo objPIDashBoardLst = (msassignedtomecollectionbo)Session["grdLst"];
        bool objSortOrder = (bool)Session["bSortedOrder"];
        switch (e.SortExpression.ToString().Trim())
        {

            case "PERNR":
                if (objSortOrder)
                {
                    if (objPIDashBoardLst != null)
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return ((long.Parse(objBo1.PERNR)).CompareTo(long.Parse(objBo2.PERNR))); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return ((long.Parse(objBo2.PERNR)).CompareTo(long.Parse(objBo1.PERNR))); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;

            case "ENAME":
                if (objSortOrder)
                {
                    if (objPIDashBoardLst != null)
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;


            case "CHANGE_APPROVAL":
                if (objSortOrder)
                {
                    if (objPIDashBoardLst != null)
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.CHANGE_APPROVAL.CompareTo(objBo2.CHANGE_APPROVAL)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.CHANGE_APPROVAL.CompareTo(objBo1.CHANGE_APPROVAL)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "Subtype":
                if (objSortOrder)
                {
                    if (objPIDashBoardLst != null)
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.Subtype.CompareTo(objBo2.Subtype)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.Subtype.CompareTo(objBo1.Subtype)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;

            case "REVIEW":
                if (objSortOrder)
                {
                    if (objPIDashBoardLst != null)
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.REVIEW.CompareTo(objBo2.REVIEW)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.REVIEW.CompareTo(objBo1.REVIEW)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "LAST_ACTIVITY_DATE":
                if (objSortOrder)
                {
                    if (objPIDashBoardLst != null)
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.LAST_ACTIVITY_DATE.CompareTo(objBo2.LAST_ACTIVITY_DATE)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.LAST_ACTIVITY_DATE.CompareTo(objBo1.LAST_ACTIVITY_DATE)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;

        }

        grdPending.DataSource = objPIDashBoardLst;
        grdPending.DataBind();

        Session.Add("grdLst", objPIDashBoardLst);
        GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
        GV_DashboardCompleatedDetails.DataBind();
        GV_DashboardDetails.DataBind();
        GridViewDetails.DataBind();
        grdRecordTime.DataBind();
        RWTdiv.Visible = false;
        txtRWComments.Text = string.Empty;
        lblValidateRWCommnets.Text = string.Empty;
        TblRemarks.Visible = false;
        lblRemarksRWT.Text = string.Empty;
        lblRemarksRWT.Visible = false;
    }
    protected void grdCompleted_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        AllPnelStatus();
        int pageindex = e.NewPageIndex;
        grdCompleted.PageIndex = e.NewPageIndex;
        msassignedtomecollectionbo objPIDashBoardCmpltdLst = (msassignedtomecollectionbo)Session["grdCmpltdLst"];
        grdCompleted.DataSource = objPIDashBoardCmpltdLst;
        int rselectedindex = Convert.ToInt32(ViewState["indexchang"]);
        int pagindex = Convert.ToInt32(ViewState["pageindex"]);
        grdCompleted.DataSource = objPIDashBoardCmpltdLst;
        grdCompleted.SelectedIndex = -1;
        grdCompleted.DataBind();
        if (pageindex == pagindex)
        {
            grdCompleted.SelectedIndex = rselectedindex;
        }
    }
    protected void grdCompleted_Sorting(object sender, GridViewSortEventArgs e)
    {
        divMassBtn.Visible = false;
        msassignedtomecollectionbo objPIDashBoardCmpltdLst = (msassignedtomecollectionbo)Session["grdCmpltdLst"];
        bool objSortOrder = (bool)Session["bSortedOrder"];
        switch (e.SortExpression.ToString().Trim())
        {
            case "PERNR":
                if (objSortOrder)
                {
                    if (objPIDashBoardCmpltdLst != null)
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return ((long.Parse(objBo1.PERNR)).CompareTo(long.Parse(objBo2.PERNR))); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return ((long.Parse(objBo2.PERNR)).CompareTo(long.Parse(objBo1.PERNR))); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;

            case "ENAME":
                if (objSortOrder)
                {
                    if (objPIDashBoardCmpltdLst != null)
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;

            case "AppByName":
                if (objSortOrder)
                {
                    if (objPIDashBoardCmpltdLst != null)
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.AppByName.ToString().CompareTo(objBo2.AppByName.ToString())); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.AppByName.ToString().CompareTo(objBo1.AppByName.ToString())); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;

            case "CHANGE_APPROVAL":
                if (objSortOrder)
                {
                    if (objPIDashBoardCmpltdLst != null)
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.CHANGE_APPROVAL.CompareTo(objBo2.CHANGE_APPROVAL)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.CHANGE_APPROVAL.CompareTo(objBo1.CHANGE_APPROVAL)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;

            case "Subtype":
                if (objSortOrder)
                {
                    if (objPIDashBoardCmpltdLst != null)
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.Subtype.CompareTo(objBo2.Subtype)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.Subtype.CompareTo(objBo1.Subtype)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;

            case "REVIEW":
                if (objSortOrder)
                {
                    if (objPIDashBoardCmpltdLst != null)
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.REVIEW.CompareTo(objBo2.REVIEW)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.REVIEW.CompareTo(objBo1.REVIEW)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "LAST_ACTIVITY_DATE":
                if (objSortOrder)
                {
                    if (objPIDashBoardCmpltdLst != null)
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.LAST_ACTIVITY_DATE.CompareTo(objBo2.LAST_ACTIVITY_DATE)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.LAST_ACTIVITY_DATE.CompareTo(objBo1.LAST_ACTIVITY_DATE)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
        }


        grdCompleted.DataSource = objPIDashBoardCmpltdLst;
        grdCompleted.DataBind();

        Session.Add("grdCmpltdLst", objPIDashBoardCmpltdLst);
        GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
        GV_DashboardCompleatedDetails.DataBind();
        GV_DashboardDetails.DataBind();
        GridViewDetails.DataBind();
        grdRecordTime.DataBind();
        RWTdiv.Visible = false;
        txtRWComments.Text = string.Empty;
        lblValidateRWCommnets.Text = string.Empty;
        TblRemarks.Visible = false;
        lblRemarksRWT.Text = string.Empty;
        lblRemarksRWT.Visible = false;
    }


    protected void RecordWorkingDetailsControlStatus(Boolean bIsStatus)
    {
        pnlRecordWorking.Visible = true;
        lblValidateRWCommnets.Text = "";
        lblValidateRWCommnets.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        txtRWComments.Text = "";
        if (bIsStatus == true)
        {
            lblValidateRWCommnets.Text = "*";
            lblValidateRWCommnets.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
            btnRWApprove.Visible = true;
            btnRWReject.Visible = true;
            txtRWComments.Visible = true;

        }
        else
        {
            btnRWApprove.Visible = false;
            btnRWReject.Visible = false;
            txtRWComments.Visible = false;
        }
    }


    //protected void grdCompleted_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
    //        e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

    //        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdCompleted, "Select$" + e.Row.RowIndex);
    //    }
    //}
    //protected void grdPending_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
    //        e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

    //        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdPending, "Select$" + e.Row.RowIndex);
    //    }
    //}

    //protected void GridViewDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
    //        e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

    //        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridViewDetails, "Select$" + e.Row.RowIndex);
    //    }
    //}


    protected void btnRWApprove_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        Approve_Reject_RecordWorkingDetails(bIStatus);
        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
        lblMessageBoard.Text = "Approved Successfully !";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Approved Successfully !');", true);
    }
    protected void btnRWReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        Approve_Reject_RecordWorkingDetails(bIStatus);
        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
        lblMessageBoard.Text = "Rejected Successfully !";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Rejected Successfully !');", true);

    }

    protected void Approve_Reject_RecordWorkingDetails(bool bIStatus)
    {
        msassignedtomebo objBo = new msassignedtomebo();
        string sIOPkey = (string)Session["sRWkey"];
        string sRole = (string)Session["sRole"];
        objBo.APPROVED_BY = User.Identity.Name;
        objBo.APPROVED_ON = DateTime.Now;
        objBo.COMMENTS = txtRWComments.Text.Trim();
        objBo.STATUS = bIStatus;
        objBo.PKEY = sIOPkey;
        objBo.PLSXT = sRole;
        msassignedtomebl objPIAddBl = new msassignedtomebl();
        try
        {
            int iResult = objPIAddBl.Approval_RecorWorkingDetails(Session["CompCode"].ToString(), objBo, ref strApprover, ref strApprover_mail, ref strRequesterMail);


            if (iResult == 0)
            {
                SendMailRWT(objBo.PKEY.ToString().Trim(), objBo.STATUS, objBo.COMMENTS, objBo.APPROVED_ON);

                HF_TBLTYPE = null;
                HF_ID.Value = null;
                HF_PKEY = null;
                GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_DashboardDetails.DataBind();
                GridViewDetails.DataBind();
                grdRecordTime.DataBind();
                RWTdiv.Visible = false;
                txtRWComments.Text = string.Empty;
                lblValidateRWCommnets.Text = string.Empty;
                TblRemarks.Visible = false;
                lblRemarksRWT.Text = string.Empty;
                lblRemarksRWT.Visible = false;
                LoadGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());
                pnlRecordWorking.Visible = false;
                RWTdiv.Visible = false;
                divView2.Visible = false;
                grdCompleted.Visible = false;
                
            }
        }
        //catch (Exception ex)
        //{
        //    lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
        //    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
        //    return;
        //}
        catch (Exception Ex) 
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }

    }

    public void SendMailRWT(string RWTPkey, bool status, string COMMENTS, DateTime APPROVED_ON)
    {
        try
        {
            //string HR_Email = string.Empty;
            string SupervisiorName = string.Empty;
            string EmpMail = string.Empty;
            string EmpName = string.Empty;
            string SupervisorMail = string.Empty;
            string EmpId = string.Empty;

            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            wtrecordworkingtimebl objBl = new wtrecordworkingtimebl();
            wtrecordworkingtimecollectionbo objLst = new wtrecordworkingtimecollectionbo();

            objBo.PKEY = RWTPkey.Trim();
            objBo.COMMENTS = Session["CompCode"].ToString();
            wtrecordworkingtimecollectionbo objLstOne = new wtrecordworkingtimecollectionbo();
            objLstOne = objBl.Get_RecordDetails_ForMail(objBo, ref SupervisorMail, ref EmpMail, ref EmpName, ref SupervisiorName, ref EmpId);
            if (objLstOne.Count > 0)
            {

                string DATE_FROM = objLstOne[0].MINDATERWT == null ? "" : DateTime.Parse(objLstOne[0].MINDATERWT.ToString()).ToString("dd-MMM-yyyy");
                string DATE_TO = objLstOne[0].MAXDATERWT == null ? "" : DateTime.Parse(objLstOne[0].MAXDATERWT.ToString()).ToString("dd-MMM-yyyy");
                string Appvdon = DateTime.Parse(APPROVED_ON.ToString()).ToString("dd-MMM-yyyy");
                string strSubject = string.Empty;
                string BodyHeadr = string.Empty;
                string stsTyp = string.Empty;

                string ccode = Session["CompCode"].ToString();
                string mgrid = User.Identity.Name;
                int cnt = ccode.Length;
                mgrid = mgrid.Substring(cnt).ToUpper();


                string cc = Session["CompCode"].ToString();
                string eid = EmpId.ToString().Trim();
                int count = ccode.Length;
                eid = eid.Substring(cnt).ToUpper();

                if (status)
                {

                    //strSubject = "IEmpPower Paycompute - Notification !.";
                    //BodyHeadr = "Working TimeSheet has been Approved by " + SupervisiorName + "  | " + mgrid.ToString().Trim() + " for the week " + DATE_FROM + " to "
                    //+ DATE_TO + ".";
                    //stsTyp = "Approved on";

                }

                else
                {

                    strSubject = "IEmpPower Paycompute - Notification !";
                    BodyHeadr = "Working TimeSheet has been Rejected by " + SupervisiorName + "  | " + mgrid.ToString().Trim() + " for the week " + DATE_FROM + " to "
                    + DATE_TO + ".";
                    stsTyp = "Rejected on";


                    string RecipientsString = EmpMail;
                    string strPernr_Mail = SupervisorMail;

                    //    //Preparing the mail body--------------------------------------------------



                    string body = "<b style= 'font-size: 15px';> " + BodyHeadr + "</b><br/><br/>";

                    body = body + "<b style= 'font-size: 14px';>Record Working Time details : </b><hr>";
                    body += "<b><table style=border-collapse:collapse;><tr><td style='font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + eid.ToString().Trim() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + EmpName.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DATE_FROM.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important';>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DATE_TO.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>" + stsTyp + " </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + Appvdon.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Remarks </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + COMMENTS.ToString() + "</td></tr></table></b>";
                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";

                    // body += "</br>" + sw1.ToString() + "<br/>";
                    //    //End of preparing the mail body-------------------------------------------

                    ////Newly added Starts
                    //Thread email = new Thread(delegate()
                    //{
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    //});

                    //email.IsBackground = true;
                    //email.Start();
                    ////Newly added Ends

                }

                //string RecipientsString = EmpMail;
                //string strPernr_Mail = SupervisorMail;

                ////    //Preparing the mail body--------------------------------------------------



                //string body = "<b style= 'font-size: 15px';> " + BodyHeadr + "</b><br/><br/>";
                //body += "to" + RecipientsString + "from  " + strPernr_Mail;
                //body = body + "<b style= 'font-size: 14px';>Record Working Time details : </b><hr>";
                //body += "<b><table style=border-collapse:collapse;><tr><td style='font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + EmpId.ToString() + "</td></tr>";
                //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + EmpName.ToString() + "</td></tr>";
                //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DATE_FROM.ToString() + "</td></tr>";
                //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important';>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DATE_TO.ToString() + "</td></tr>";
                //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>" + stsTyp + " </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + Appvdon.ToString() + "</td></tr>";
                //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Remarks </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + COMMENTS.ToString() + "</td></tr></table></b>";
                //body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";

                //// body += "</br>" + sw1.ToString() + "<br/>";
                ////    //End of preparing the mail body-------------------------------------------
                //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

            }


            //break;

        }
        catch (Exception Ex) { }
        //{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }




    public static DataTable CreateTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("CostCenter", typeof(string)));//for TextBox value 
        dt.Columns.Add(new DataColumn("Order", typeof(string)));
        dt.Columns.Add(new DataColumn("AttTypes", typeof(string)));//for TextBox value 
        dt.Columns.Add(new DataColumn("Staff", typeof(string)));
        dt.Columns.Add(new DataColumn("Total", typeof(string)));
        dt.Columns.Add(new DataColumn("Sun", typeof(string)));
        dt.Columns.Add(new DataColumn("Mon", typeof(string)));
        dt.Columns.Add(new DataColumn("Tue", typeof(string)));
        dt.Columns.Add(new DataColumn("Wed", typeof(string)));
        dt.Columns.Add(new DataColumn("Thur", typeof(string)));
        dt.Columns.Add(new DataColumn("Fri", typeof(string)));
        dt.Columns.Add(new DataColumn("Sat", typeof(string)));
        dt.Columns.Add(new DataColumn("Remarks", typeof(string)));

        return dt;
    }

    public static DataTable ConvertToDataRow(wtrecordworkingtimecollectionbo objColBo)
    {
        DataTable dt = CreateTable();

        foreach (wtrecordworkingtimebo objBo in objColBo)
        {
            DataRow dRow = dt.NewRow();
            dRow["Sun"] = objBo.SUNDAY;
            dRow["Mon"] = objBo.MONDAY;
            dRow["Tue"] = objBo.TUESDAY;
            dRow["Wed"] = objBo.WEDNESDAY;
            dRow["Thur"] = objBo.THURSDAY;
            dRow["Fri"] = objBo.FRIDAY;
            dRow["Sat"] = objBo.SATURDAY;
            dRow["AttTypes"] = objBo.AWART.Trim();
            dRow["CostCenter"] = objBo.ARBST.Trim();
            dRow["Order"] = objBo.KTEXT.Trim();
            dRow["Staff"] = objBo.LTEXT.Trim();
            dRow["REMARKS"] = objBo.REMARKS;
            dt.Rows.Add(dRow);
        }
        return dt;
    }
    protected void LoadRecordWorking(wtrecordworkingtimecollectionbo objLstOne, wtrecordworkingtimebo objBo)
    {
        DataTable CurrentTable = CreateTable();
        CurrentTable = ConvertToDataRow(objLstOne);
        ViewState["CurrentTable"] = CurrentTable;

        grdRecordTime.DataSource = CurrentTable;
        grdRecordTime.DataBind();

        SetRemoveDatas();
        pidashboardbl objPIDashBl = new pidashboardbl();
        wtrecordworkingtimecollectionbo list = new wtrecordworkingtimecollectionbo();
        list = objPIDashBl.Get_RecordDetails_Date(objBo);
        DateTime dtSelectedDate = DateTime.Now;
        foreach (wtrecordworkingtimebo objReBo in list)
        {
            dtSelectedDate = objReBo.FROM_DATE;
        }

        DateTime dtStartDate, dtEndDate;

        GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
        grdRecordTime.HeaderRow.Cells[5].Text = "SUN ," + dtStartDate.AddDays(0).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[6].Text = "MON ," + dtStartDate.AddDays(1).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[7].Text = "TUE ," + dtStartDate.AddDays(2).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[8].Text = "WED ," + dtStartDate.AddDays(3).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[9].Text = "THU ," + dtStartDate.AddDays(4).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[10].Text = "FRI ," + dtStartDate.AddDays(5).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[11].Text = "SAT ," + dtStartDate.AddDays(6).ToString("d-MMM-yyyy");

    }
    private void SetRemoveDatas()
    {
        decimal sSun = 0;
        decimal sMon = 0;
        decimal sTue = 0;
        decimal sWed = 0;
        decimal sThu = 0;
        decimal sFri = 0;
        decimal sSat = 0;
        int rowIndex = 0;

        decimal sTotalActualHrs = 0;
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    decimal iRowTotalHrs = 0;
                    Label lblCostCenter = (Label)grdRecordTime.Rows[i].FindControl("lblCostCenter");
                    Label lblOrder = (Label)grdRecordTime.Rows[i].FindControl("lblOrder");
                    Label boxStaffGrade = (Label)grdRecordTime.Rows[i].FindControl("txtStaffGrade");
                    Label boxTotal = (Label)grdRecordTime.Rows[i].FindControl("txtTotal");
                    Label boxSUN = (Label)grdRecordTime.Rows[i].FindControl("txtSUN");
                    Label boxMON = (Label)grdRecordTime.Rows[i].FindControl("txtMON");
                    Label boxTUE = (Label)grdRecordTime.Rows[i].FindControl("txtTUE");
                    Label boxWED = (Label)grdRecordTime.Rows[i].FindControl("txtWED");
                    Label boxTHU = (Label)grdRecordTime.Rows[i].FindControl("txtTHU");
                    Label boxFRI = (Label)grdRecordTime.Rows[i].FindControl("txtFRI");
                    Label boxSAT = (Label)grdRecordTime.Rows[i].FindControl("txtSAT");
                    Label boxRemarks = (Label)grdRecordTime.Rows[i].FindControl("txtREMARKS");
                    Label lblHours = ((Label)grdRecordTime.FooterRow.FindControl("lblHours"));
                    Label lblSun = ((Label)grdRecordTime.FooterRow.FindControl("lblSun"));
                    Label lblMon = ((Label)grdRecordTime.FooterRow.FindControl("lblMon"));
                    Label lblTues = ((Label)grdRecordTime.FooterRow.FindControl("lblTues"));
                    Label lblWed = ((Label)grdRecordTime.FooterRow.FindControl("lblWed"));
                    Label lblThu = ((Label)grdRecordTime.FooterRow.FindControl("lblThu"));
                    Label lblFri = ((Label)grdRecordTime.FooterRow.FindControl("lblFri"));
                    Label lblSAt = ((Label)grdRecordTime.FooterRow.FindControl("lblSAt"));
                    Label lblRemarks = ((Label)grdRecordTime.FooterRow.FindControl("lblREMARKS"));
                    Label ddlCostCenter = (Label)grdRecordTime.Rows[rowIndex].FindControl("drpdwnCostCenter");


                    //extract the DropDownList Selected Items 
                    Label ddlOrder = (Label)grdRecordTime.Rows[rowIndex].FindControl("drpdwnOrder");
                    ////Label drpdwnWbs = (Label)grdRecordTime.Rows[rowIndex].FindControl("drpdwnWbs");
                    ////Label drpdwnNetwork = (Label)grdRecordTime.Rows[rowIndex].FindControl("drpdwnNetwork");
                    ////Label drpdwnActtype = (Label)grdRecordTime.Rows[rowIndex].FindControl("drpdwnActtype");

                    Label ddl1 = (Label)grdRecordTime.Rows[rowIndex].FindControl("drpdwnAttabsType");

                    //Fill the DropDownList with Data 

                    if (i < dt.Rows.Count)
                    {

                        //Assign the value from DataTable to the TextBox 
                        ddlCostCenter.Text = dt.Rows[i]["CostCenter"].ToString();
                        ddlOrder.Text = dt.Rows[i]["Order"].ToString();
                        ////drpdwnWbs.Text = dt.Rows[i]["Wbs"].ToString();
                        ////drpdwnNetwork.Text = dt.Rows[i]["Network"].ToString();
                        ////drpdwnActtype.Text = dt.Rows[i]["Acttype"].ToString();
                        ddl1.Text = dt.Rows[i]["AttTypes"].ToString();
                        boxStaffGrade.Text = dt.Rows[i]["Staff"].ToString();
                        boxTotal.Text = dt.Rows[i]["Total"].ToString();
                        boxSUN.Text = dt.Rows[i]["sun"].ToString();
                        boxMON.Text = dt.Rows[i]["Mon"].ToString();
                        boxTUE.Text = dt.Rows[i]["Tue"].ToString();
                        boxWED.Text = dt.Rows[i]["Wed"].ToString();
                        boxTHU.Text = dt.Rows[i]["Thur"].ToString();
                        boxFRI.Text = dt.Rows[i]["Fri"].ToString();
                        boxSAT.Text = dt.Rows[i]["Sat"].ToString();
                        boxRemarks.Text = dt.Rows[i]["Remarks"].ToString();
                        if (boxSUN.Text.Trim() != "")
                        {
                            sSun = decimal.Parse(boxSUN.Text) + sMon;
                            lblSun.Text = sSun.ToString();
                            //  sTotalActualHrs = sMon + sTotalActualHrs;
                            iRowTotalHrs = decimal.Parse(boxSUN.Text) + iRowTotalHrs;
                        }

                        if (boxMON.Text.Trim() != "")
                        {
                            sMon = decimal.Parse(boxMON.Text) + sMon;
                            lblMon.Text = sMon.ToString();
                            //  sTotalActualHrs = sMon + sTotalActualHrs;
                            iRowTotalHrs = decimal.Parse(boxMON.Text) + iRowTotalHrs;
                        }
                        if (boxTUE.Text.Trim() != "")
                        {
                            sTue = decimal.Parse(boxTUE.Text) + sTue;
                            lblTues.Text = sTue.ToString();
                            //  sTotalActualHrs = sTue + sTotalActualHrs;
                            iRowTotalHrs = decimal.Parse(boxTUE.Text) + iRowTotalHrs;
                        } if (boxWED.Text.Trim() != "")
                        {

                            sWed = decimal.Parse(boxWED.Text) + sWed;
                            lblWed.Text = sWed.ToString();
                            //sTotalActualHrs = sWed + sTotalActualHrs;
                            iRowTotalHrs = decimal.Parse(boxWED.Text) + iRowTotalHrs;
                        }
                        if (boxTHU.Text.Trim() != "")
                        {
                            sThu = decimal.Parse(boxTHU.Text) + sThu;
                            lblThu.Text = sThu.ToString();
                            // sTotalActualHrs = sThu + sTotalActualHrs;
                            iRowTotalHrs = decimal.Parse(boxTHU.Text) + iRowTotalHrs;
                        }
                        if (boxFRI.Text.Trim() != "")
                        {
                            sFri = decimal.Parse(boxFRI.Text) + sFri;
                            lblFri.Text = sFri.ToString();
                            //sTotalActualHrs = sFri + sTotalActualHrs;
                            iRowTotalHrs = decimal.Parse(boxFRI.Text) + iRowTotalHrs;
                        }
                        if (boxSAT.Text.Trim() != "")
                        {
                            sSat = decimal.Parse(boxSAT.Text) + sSat;
                            lblSAt.Text = sSat.ToString();
                            // sTotalActualHrs = sSat + sTotalActualHrs;
                            iRowTotalHrs = decimal.Parse(boxSAT.Text) + iRowTotalHrs;
                        }
                        sTotalActualHrs = iRowTotalHrs + sTotalActualHrs;
                        lblHours.Text = sTotalActualHrs.ToString();
                        boxTotal.Text = iRowTotalHrs.ToString();

                        if (boxRemarks.Text.Trim() != "")
                        {
                            lblRemarks.Text = boxRemarks.Text.ToString().Trim();

                        }

                    }
                    rowIndex++;
                }
            }
        }

    }
    void GetCurrentWeekDates(DateTime Input, out DateTime Start, out DateTime End)
    {

        if (Input.Date.DayOfWeek == DayOfWeek.Sunday)
        {
            Start = Input;
            End = Start.AddDays(6);
            return;
        }

        while (Input.Date.DayOfWeek != DayOfWeek.Sunday)

            Input = Input.Date.AddDays(-1);

        Start = Input;
        End = Input.AddDays(6);
    }
    public static bool CreateExcelFile(DataTable dt, string filename)
    {
        try
        {
            string sTableStart = @"<HTML><BODY><TABLE Border=1>";
            string sTableEnd = @"</TABLE></BODY></HTML>";
            string sTHead = "<TR>";
            StringBuilder sTableData = new StringBuilder();
            foreach (DataColumn col in dt.Columns)
            {
                sTHead += @"<TH>" + col.ColumnName + @"</TH>";
            }
            sTHead += @"</TR>";
            foreach (DataRow row in dt.Rows)
            {
                sTableData.Append(@"<TR>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sTableData.Append(@"<TD>" + row[i].ToString() + @"</TD>");
                }
                sTableData.Append(@"</TR>");
            }
            string sTable = sTableStart + sTHead + sTableData.ToString() + sTableEnd;
            System.IO.StreamWriter oExcelWriter = System.IO.File.CreateText(filename);
            oExcelWriter.WriteLine(sTable);
            oExcelWriter.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }


    private void GVNodata(GridView GV)
    {
        try
        {

            DataTable Dt = new DataTable();
            Dt.Columns.Add("Slno", typeof(string));
            Dt.Columns.Add("TEXT", typeof(int));
            Dt.Columns.Add("VALUES", typeof(string));
            Dt.Rows.Add(Dt.NewRow());
            GV.DataSource = Dt;
            GV.DataBind();
            GV.Rows[0].Visible = false;
            GV.Rows[0].Controls.Clear();
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }


    private void SendMailLeaveDel(List<leaverequestbo> objList, ref string Supervisor_Name, ref string Supervisor_Email, ref string HR_Email, ref string PERNR_Name, ref string PERNR_Email, string Sts, string tbltyp)
    {
        try
        {

            leaverequestbo objPIFddBo = new leaverequestbo();
            leaverequestbl objPIFddBl = new leaverequestbl();

            if (objList.Count > 0)
            {


                string PERNR = objList[0].PERNR == null ? "" : objList[0].PERNR.ToString();
                string BEGDA = objList[0].BEGDA == null ? "" : objList[0].BEGDA.ToString();
                string ENDDA = objList[0].ENDDA == null ? "" : objList[0].ENDDA.ToString();
                string BEGUZ = objList[0].BEGUZ == null ? "" : objList[0].BEGUZ.ToString();
                string ENDUZ = objList[0].ENDUZ == null ? "" : objList[0].ENDUZ.ToString();
                string AWART = objList[0].AWART == null ? "" : objList[0].AWART.ToString();
                string STDAZ = objList[0].STDAZ == null ? "" : objList[0].STDAZ.ToString();
                string NOTE = objList[0].NOTE == null ? "" : objList[0].NOTE.ToString();

                string APPROVED_BY = objList[0].APPROVED_BY == null ? "" : objList[0].APPROVED_BY.ToString();
                string CREATED_ON = objList[0].CREATED_ON == null ? "" : objList[0].CREATED_ON.ToString();
                string APPROVED_ON = objList[0].APPROVED_ON == null ? "" : objList[0].APPROVED_ON.ToString();
                string ATEXT = objList[0].ATEXT == null ? "" : objList[0].ATEXT.ToString();
                string REMARKS = objList[0].REMARKS == null ? "" : objList[0].REMARKS.ToString();
                string EMPLOYEE_NAME = objList[0].EMPLOYEE_NAME == null ? "" : objList[0].EMPLOYEE_NAME.ToString();
                string APPROVED_BY_NAME = objList[0].APPROVED_BY_NAME == null ? "" : objList[0].APPROVED_BY_NAME.ToString();
                string DURATION = objList[0].DURATION == null ? "" : objList[0].DURATION.ToString();

                string cc = Session["CompCode"].ToString();
                string empid = PERNR.ToString().Trim();
                int ct = cc.Length;
                empid = empid.Substring(ct).ToUpper();


                string ccode = Session["CompCode"].ToString();
                string appid = User.Identity.Name;
                int cnt = ccode.Length;
                appid = appid.Substring(cnt).ToUpper();
                string strSubject = string.Empty;
                 string strSubject1 = string.Empty;

                 strSubject1 = "IEmpPower Paycompute - Notification !";

                 strSubject = Sts + " for " + ATEXT + " by " + APPROVED_BY_NAME + "  | " + appid.ToString().Trim() + ".";



                string RecipientsString = PERNR_Email;
                string strPernr_Mail = Supervisor_Email + "," + HR_Email;

                //    //Preparing the mail body--------------------------------------------------

                if (tbltyp.Trim() == "PA2001")
                {

                    string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                    //body = body + "<b style= 'font-size: 14px';>Family details : </b><hr>";

                    body += "<b><table style=border-collapse:collapse;><tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + EMPLOYEE_NAME.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + empid.ToString().Trim() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Leave Type </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ATEXT.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(BEGDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(ENDDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Duration(Days) </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DURATION.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Reason for leave </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + NOTE.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Leave applied on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(CREATED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr></table></b>";
                    //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>" + sts.Trim() + " on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(APPROVED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Remarks </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + REMARKS.ToString() + "</td></tr></table></b>";


                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    ////Newly added Starts
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, appid.ToString().Trim(), strSubject1, strPernr_Mail, body);
                    });

                    email.IsBackground = true;
                    email.Start();
                    ////Newly added Ends
                }

                else if (tbltyp.Trim() == "PA2002")
                {

                    string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                    //body = body + "<b style= 'font-size: 14px';>Family details : </b><hr>";

                    body += "<b><table style=border-collapse:collapse;><tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + PERNR_Name.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + empid.ToString().Trim() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Leave Type </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ATEXT.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(BEGDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(ENDDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Time </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + BEGUZ.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Time </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ENDUZ.ToString() + "</td></tr>";

                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Duration(Days) </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DURATION.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Reason for leave </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + NOTE.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Leave applied on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(CREATED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr></table></b>";
                    //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>" + sts.Trim() + " on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(APPROVED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    //body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Remarks </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + REMARKS.ToString() + "</td></tr></table></b>";


                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    ////Newly added Starts
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, appid.ToString().Trim(), strSubject1, strPernr_Mail, body);
                    });

                    email.IsBackground = true;
                    email.Start();
                    ////Newly added Ends
                }

                // body += "</br>" + sw1.ToString() + "<br/>";
                //    //End of preparing the mail body-------------------------------------------
                //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

            }


            //break;

        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }



    #region GV_DashboardDetails Events
    protected void GV_DashboardDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //HF_TBLTYPE.Value = TblTyp.ToString().Trim();
            //HF_ID.Value = id.ToString().Trim();

            if (HF_TBLTYPE != null && HF_ID != null && HF_PKEY != null)
            {
                if (GV_DashboardDetails.Rows.Count > 0)
                {
                    switch (HF_TBLTYPE.Value.ToString().Trim().ToUpper())
                    {

                        case "PA2001":

                            switch (e.CommandName.ToUpper())
                            {
                                case "APPROVE": // Flag - 1
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            ObjBO.TableTyp = "PA2001";
                                            ObjBO.PERNR = ViewState["LACRTDBY"].ToString().Trim();


                                            ObjBL.Mngr_Leave_Req_Approve_Reject(Session["CompCode"].ToString(), ObjBO, ref HR_Email, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);


                                            leaverequestbo objBo = new leaverequestbo();
                                            leaverequestbl objBl = new leaverequestbl();
                                            leaverequestcollectionbo objLst = new leaverequestcollectionbo();

                                            List<leaverequestbo> objList = new List<leaverequestbo>();

                                            if (HF_STS.Value.ToString().Trim().ToUpper() == "DELETION REQUESTED")
                                            {
                                              objList = objBl.Deletion_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Deletion request approved", "PA2001");

                                              SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request approved", "PA2001");

                                            }
                                            else
                                            {
                                              objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved", "PA2001");

                                               SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim(), "PA2001");
                                            }
                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID = null;
                                            HF_PKEY = null;
                                            HF_STS = null;


                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;

                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Leave Request approved successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                case "REJECT": // Flag - 2
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;


                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            ObjBO.TableTyp = "PA2001";
                                            ObjBO.PERNR = ViewState["LACRTDBY"].ToString().Trim();

                                            ObjBL.Mngr_Leave_Req_Approve_Reject(Session["CompCode"].ToString(), ObjBO, ref HR_Email, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);


                                            leaverequestbo objBo = new leaverequestbo();
                                            leaverequestbl objBl = new leaverequestbl();
                                            leaverequestcollectionbo objLst = new leaverequestcollectionbo();

                                            List<leaverequestbo> objList = new List<leaverequestbo>();
                                            //objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected", "PA2001");


                                            //SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim(), "PA2001");


                                            if (HF_STS.Value.ToString().Trim().ToUpper() == "DELETION REQUESTED")
                                            {
                                              objList = objBl.Deletion_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Deletion request rejected", "PA2001");

                                               SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request rejected", "PA2001");

                                            }
                                            else
                                            {
                                               objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected", "PA2001");

                                               SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim(), "PA2001");
                                            }

                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID = null;
                                            HF_PKEY = null;
                                            HF_STS = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;

                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Leave Request rejected successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;

                        // --------------------Attendance

                        case "PA2002":

                            switch (e.CommandName.ToUpper())
                            {
                                case "APPROVE": // Flag - 1
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            ObjBO.TableTyp = "PA2002";
                                            ObjBO.PERNR = ViewState["LACRTDBY"].ToString().Trim();
                                            ObjBL.Mngr_Leave_Req_Approve_Reject(Session["CompCode"].ToString(), ObjBO, ref HR_Email, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);


                                            leaverequestbo objBo = new leaverequestbo();
                                            leaverequestbl objBl = new leaverequestbl();
                                            leaverequestcollectionbo objLst = new leaverequestcollectionbo();

                                            List<leaverequestbo> objList = new List<leaverequestbo>();
                                            if (HF_STS.Value.ToString().Trim().ToUpper() == "DELETION REQUESTED")
                                            {
                                                objList = objBl.Deletion_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Deletion request approved", "PA2002");

                                                SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request approved", "PA2002");

                                            }
                                            else
                                            {
                                                objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved", "PA2002");


                                                SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim(), "PA2002");

                                            }



                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID = null;
                                            HF_PKEY = null;
                                            HF_STS = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Attendance Request approved successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                case "REJECT": // Flag - 2
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;


                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            ObjBO.TableTyp = "PA2002";
                                            ObjBO.PERNR = ViewState["LACRTDBY"].ToString().Trim();
                                            ObjBL.Mngr_Leave_Req_Approve_Reject(Session["CompCode"].ToString(), ObjBO, ref HR_Email, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);

                                            leaverequestbo objBo = new leaverequestbo();
                                            leaverequestbl objBl = new leaverequestbl();
                                            leaverequestcollectionbo objLst = new leaverequestcollectionbo();

                                            List<leaverequestbo> objList = new List<leaverequestbo>();
                                            //objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected", "PA2002");


                                            //SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim(), "PA2002");


                                            if (HF_STS.Value.ToString().Trim().ToUpper() == "DELETION REQUESTED")
                                            {
                                                objList = objBl.Deletion_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Deletion request rejected", "PA2002");

                                                SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request rejected", "PA2002");

                                            }
                                            else
                                            {
                                                objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected", "PA2002");


                                               SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim(), "PA2002");

                                            }


                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID = null;
                                            HF_PKEY = null;
                                            HF_STS = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Attendance Request rejected successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;






                        //-------------Address------------------------------------
                        case "PA0006":

                            switch (e.CommandName.ToUpper())
                            {
                                case "APPROVE": // Flag - 1
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            //ObjBO.TableTyp = "PA0006";
                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                                            piaddressinformationbl objPIAddBl = new piaddressinformationbl();

                                            List<piaddressinformationbo> objList = new List<piaddressinformationbo>();
                                            objList = objPIAddBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                            ObjBL.Approval_AddressDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                           SendMailAddress(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim());
                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID = null;
                                            HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Address Information approved successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                case "REJECT": // Flag - 2
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;


                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            //ObjBO.TableTyp = "PA0006";

                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());


                                            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                                            piaddressinformationbl objPIAddBl = new piaddressinformationbl();
                                            List<piaddressinformationbo> objList = new List<piaddressinformationbo>();
                                            objList = objPIAddBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");

                                            ObjBL.Approval_AddressDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                           SendMailAddress(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim());

                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID = null;
                                            HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Address Information rejected successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;





                        case "PA0105":

                            switch (e.CommandName.ToUpper())
                            {
                                case "APPROVE": // Flag - 1
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            //ObjBO.TableTyp = "PA0006";

                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());


                                            picommunicationinformationbo objPIComBo = new picommunicationinformationbo();
                                            picommunicationinformationbl objPIComBl = new picommunicationinformationbl();
                                            List<picommunicationinformationbo> objList = new List<picommunicationinformationbo>();
                                            objList = objPIComBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                            ObjBL.Approval_CommunticationInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                           SendMailCommunication(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim());

                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID.Value = null;
                                            HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Information approved successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                case "REJECT": // Flag - 2
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;


                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            //ObjBO.TableTyp = "PA0006";

                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());


                                            picommunicationinformationbo objPIComBo = new picommunicationinformationbo();
                                            picommunicationinformationbl objPIComBl = new picommunicationinformationbl();
                                            List<picommunicationinformationbo> objList = new List<picommunicationinformationbo>();
                                            objList = objPIComBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");


                                            ObjBL.Approval_CommunticationInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                         SendMailCommunication(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim());

                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID.Value = null;
                                            HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Information rejected successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;



                        case "PA0002":

                            switch (e.CommandName.ToUpper())
                            {
                                case "APPROVE": // Flag - 1
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            //ObjBO.TableTyp = "PA0006";

                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());


                                            personaldatabo objPIBo = new personaldatabo();
                                            personaldatabl objPIBl = new personaldatabl();
                                            List<personaldatabo> objList = new List<personaldatabo>();
                                            objList = objPIBl.Approval_PDDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");


                                            ObjBL.Approval_PDInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);

                                          SendMailPD(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim());





                                            //sendmail();
                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID.Value = null;
                                            HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal Data Information approved successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                case "REJECT": // Flag - 2
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;


                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            //ObjBO.TableTyp = "PA0006";

                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());


                                            personaldatabo objPIBo = new personaldatabo();
                                            personaldatabl objPIBl = new personaldatabl();
                                            List<personaldatabo> objList = new List<personaldatabo>();
                                            objList = objPIBl.Approval_PDDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");

                                            ObjBL.Approval_PDInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailPD(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim());

                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID.Value = null;
                                            HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal Data Information rejected successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;

                        case "PA0185":

                            switch (e.CommandName.ToUpper())
                            {
                                case "APPROVE": // Flag - 1
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            //ObjBO.TableTyp = "PA0006";
                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();
                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            pipersonalidsbo objPIBo = new pipersonalidsbo();
                                            pipersonalidsbl objPIBl = new pipersonalidsbl();
                                            List<pipersonalidsbo> objList = new List<pipersonalidsbo>();
                                            objList = objPIBl.Approval_PIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                            ObjBL.Approval_PIDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                           SendMailPIDS(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim());

                                            // sendmail();
                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID.Value = null;
                                            HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal ID approved successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                case "REJECT": // Flag - 2
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            //ObjBO.TableTyp = "PA0006";
                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();
                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            pipersonalidsbo objPIBo = new pipersonalidsbo();
                                            pipersonalidsbl objPIBl = new pipersonalidsbl();
                                            List<pipersonalidsbo> objList = new List<pipersonalidsbo>();
                                            objList = objPIBl.Approval_PIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");

                                            ObjBL.Approval_PIDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailPIDS(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim());

                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID.Value = null;
                                            HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal ID rejected successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;

                        case "PA0021":

                            switch (e.CommandName.ToUpper())
                            {
                                case "APPROVE": // Flag - 1
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            //ObjBO.TableTyp = "PA0006";
                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();
                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                            pifamilymembersbl objPIFamBl = new pifamilymembersbl();
                                            List<pifamilymembersbo> objList = new List<pifamilymembersbo>();
                                            objList = objPIFamBl.Approval_FMDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                            ObjBL.Approval_FamilykDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                          /////  SendMailFamily(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim());

                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID.Value = null;
                                            HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family Details approved successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                case "REJECT": // Flag - 2
                                    using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    {
                                        if (TxtGvRemarks != null)
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = TxtGvRemarks.Text.Trim();
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            //ObjBO.TableTyp = "PA0006";
                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();
                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                            pifamilymembersbl objPIFamBl = new pifamilymembersbl();
                                            List<pifamilymembersbo> objList = new List<pifamilymembersbo>();
                                            objList = objPIFamBl.Approval_FMDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");

                                            ObjBL.Approval_FamilykDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                          /////  SendMailFamily(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim());

                                            //LoadGridDetails();
                                            GetHRPernr();
                                            ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            HF_TBLTYPE = null;
                                            HF_ID.Value = null;
                                            HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family Details rejected successfully !')", true);
                                        }
                                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;


                        default:
                            break;
                    }
                }

            }
            divView2.Visible = false;
            grdCompleted.Visible = false;

        }
        catch (Exception Ex)
        {
            switch (Ex.Message)
            {
                case "-0":
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Cannot approve this leave request !');", true);
                    break;
                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true);
                    break;
            }
        }
    }

    private void SendMailLeave(List<leaverequestbo> objList, ref string Supervisor_name, ref string Supervisor_Email, ref string HR_Email, ref string PERNR_Name, ref string PERNR_Email, string sts, string remarks, string tbltyp)
    {
        try
        {

            leaverequestbo objPIFddBo = new leaverequestbo();
            leaverequestbl objPIFddBl = new leaverequestbl();

            if (objList.Count > 0)
            {


                string PERNR = objList[0].PERNR == null ? "" : objList[0].PERNR.ToString();
                string BEGDA = objList[0].BEGDA == null ? "" : objList[0].BEGDA.ToString();
                string ENDDA = objList[0].ENDDA == null ? "" : objList[0].ENDDA.ToString();
                string BEGUZ = objList[0].BEGUZ == null ? "" : objList[0].BEGUZ.ToString();
                string ENDUZ = objList[0].ENDUZ == null ? "" : objList[0].ENDUZ.ToString();
                string AWART = objList[0].AWART == null ? "" : objList[0].AWART.ToString();
                string STDAZ = objList[0].STDAZ == null ? "" : objList[0].STDAZ.ToString();
                string NOTE = objList[0].NOTE == null ? "" : objList[0].NOTE.ToString();

                string APPROVED_BY = objList[0].APPROVED_BY == null ? "" : objList[0].APPROVED_BY.ToString();
                string CREATED_ON = objList[0].CREATED_ON == null ? "" : objList[0].CREATED_ON.ToString();
                string APPROVED_ON = objList[0].APPROVED_ON == null ? "" : objList[0].APPROVED_ON.ToString();
                string ATEXT = objList[0].ATEXT == null ? "" : objList[0].ATEXT.ToString();
                string REMARKS = objList[0].REMARKS == null ? "" : objList[0].REMARKS.ToString();
                string EMPLOYEE_NAME = objList[0].EMPLOYEE_NAME == null ? "" : objList[0].EMPLOYEE_NAME.ToString();
                string APPROVED_BY_NAME = objList[0].APPROVED_BY_NAME == null ? "" : objList[0].APPROVED_BY_NAME.ToString();
                string DURATION = objList[0].DURATION == null ? "" : objList[0].DURATION.ToString();


                string strSubject = string.Empty;
                string strSubject1 = string.Empty;


                string ccode = Session["CompCode"].ToString();
                string approver = User.Identity.Name;
                int cnt = ccode.Length;
                approver = approver.Substring(cnt).ToUpper();  


                string cc = Session["CompCode"].ToString();
                string empid = PERNR.ToString().Trim();
                int ct = ccode.Length;
                empid = empid.Substring(cnt).ToUpper(); 
                

                if (sts.Trim() == "Approved")
                {

                    strSubject1 = " IEmpPower Paycompute - Notification !";
                    strSubject = ATEXT + " has been Approved by " + APPROVED_BY_NAME + "  | " + approver.ToString().Trim() + ".";

                }

                else if (sts.Trim() == "Rejected")
                {
                    strSubject1 = " IEmpPower Paycompute - Notification !";
                    strSubject = ATEXT + " has been Rejected by " + APPROVED_BY_NAME + "  | " + approver.ToString().Trim() + ".";

                }

                string RecipientsString = PERNR_Email;
                string strPernr_Mail = Supervisor_Email + "," + HR_Email;

                //    //Preparing the mail body--------------------------------------------------

                if (tbltyp.Trim() == "PA2001")
                {

                    string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                    //body = body + "<b style= 'font-size: 14px';>Family details : </b><hr>";

                    body += "<b><table style=border-collapse:collapse;><tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + EMPLOYEE_NAME.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + empid.ToString().Trim() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Leave Type </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ATEXT.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(BEGDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(ENDDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Duration(Days) </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DURATION.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Reason for leave </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + NOTE.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Leave applied on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(CREATED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>" + sts.Trim() + " on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(APPROVED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Remarks </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + REMARKS.ToString() + "</td></tr></table></b>";


                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject1, strPernr_Mail, body);
                    });

                    email.IsBackground = true;
                    email.Start();
                    ////Newly added Ends
                }

                else if (tbltyp.Trim() == "PA2002")
                {

                    string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";

                    body += "<b><table style=border-collapse:collapse;><tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee Name</td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + EMPLOYEE_NAME.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Employee ID </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + empid.ToString().Trim() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Leave Type </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + ATEXT.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>From Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(BEGDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>To Date </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(ENDDA.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";

                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Duration(Days) </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DURATION.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Reason for leave </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + NOTE.ToString() + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Leave applied on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(CREATED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>" + sts.Trim() + " on </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + DateTime.Parse(APPROVED_ON.ToString()).ToString("dd-MMM-yyyy") + "</td></tr>";
                    body += "<tr><td style= 'font-size: 13px;width: 190px; padding: 3px;text-align: justify !important;'>Remarks </td><td style= 'font-size: 13px;width: 10px;padding: 8px;text-align: center;line-height: 12px !important;'>:</td><td style= 'font-size: 13px; padding: 3px;text-align: justify !important;'> " + REMARKS.ToString() + "</td></tr></table></b>";


                    body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, approver.ToString().Trim(), strSubject, strPernr_Mail, body);
                   
                }

            }


            //break;

        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }

    private void SendMailFamily(ref string Supervisor_name, ref string Supervisor_Email, ref string PERNR_Name, ref string PERNR_Email, string sts, string remarks)
    {
        try
        {
            string strSubject = string.Empty;
            string RecipientsString = PERNR_Email;
            string approver = Supervisor_Email;
            string[] MsgCC = { };
            //{ Supervisor_Email.ToString().Trim() };

            string ccode = Session["CompCode"].ToString();
            string empid = User.Identity.Name;
            int cnt = ccode.Length;
            empid = empid.Substring(cnt).ToUpper(); 

                if (sts.Trim() == "Approved")
                {
                    string body = string.Empty;
                    string Mailbody = string.Empty;
                    body = "Family Information has been Approved by " + Supervisor_name + "  | " + empid.ToString().Trim() + ".";
                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/EmployeePI.html");
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    masterbl.SendMail(PERNR_Email.ToString(), MsgCC, "iEmpPower Paycompute - Notification"
                      , Mailbody.Replace("##RECIPIENTNAME##", PERNR_Name.ToString().Trim()).Replace("##MAILBODY##", body.ToString()));

                }

                else if (sts.Trim() == "Rejected")
                {
                    string body = string.Empty;
                    string Mailbody = string.Empty;
                    body = "Family Information has been Rejected by " + Supervisor_name + "  | " + empid.ToString().Trim() + ".";
                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/EmployeePI.html");
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    masterbl.SendMail(PERNR_Email.ToString(), MsgCC, "iEmpPower Paycompute - Notification"
                      , Mailbody.Replace("##RECIPIENTNAME##", PERNR_Name.ToString().Trim()).Replace("##MAILBODY##", body.ToString()));

                }                       
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }

    private void SendMailPD(ref string Supervisor_name, ref string Supervisor_Email, ref string PERNR_Name, ref string PERNR_Email, string sts, string remarks)
    {
        try
        {
            string strSubject = string.Empty;
            string RecipientsString = PERNR_Email;
            string approver = Supervisor_Email;
            string[] MsgCC = { };
            //{ Supervisor_Email.ToString().Trim() };

            string ccode = Session["CompCode"].ToString();
            string empid = User.Identity.Name;
            int cnt = ccode.Length;
            empid = empid.Substring(cnt).ToUpper();  

                if (sts.Trim() == "Approved")
                {
                    string body = string.Empty;
                    string Mailbody = string.Empty;
                    body = "Personal Data  Information has been Approved by " + Supervisor_name + "  | " + empid.ToString().Trim() + ".";
                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/EmployeePI.html");
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    masterbl.SendMail(PERNR_Email.ToString(), MsgCC, "IEmpPower Paycompute - Notification !"
                      , Mailbody.Replace("##RECIPIENTNAME##", PERNR_Name.ToString().Trim()).Replace("##MAILBODY##", body.ToString()));

                }

                else if (sts.Trim() == "Rejected")
                {
                    string body = string.Empty;
                    string Mailbody = string.Empty;
                    body = "Personal Data  Information has been Rejected by " + Supervisor_name + "  | " + empid.ToString().Trim() + ".";
                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/EmployeePI.html");
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    masterbl.SendMail(PERNR_Email.ToString(), MsgCC, "IEmpPower Paycompute - Notification !"
                      , Mailbody.Replace("##RECIPIENTNAME##", PERNR_Name.ToString().Trim()).Replace("##MAILBODY##", body.ToString()));

                }

        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }


    public void SendMailAddress(ref string Supervisor_name, ref string Supervisor_Email, ref string PERNR_Name, ref string PERNR_Email, string sts, string remarks)
    {
        try
        {

            string strSubject = string.Empty;
            string RecipientsString = PERNR_Email;
            string approver = Supervisor_Email;
            string[] MsgCC = { };
            //{ Supervisor_Email.ToString().Trim() };

            string ccode = Session["CompCode"].ToString();
            string empid = User.Identity.Name;
            int cnt = ccode.Length;
            empid = empid.Substring(cnt).ToUpper();  

                if (sts.Trim() == "Approved")
                {

                    string body = string.Empty;
                    string Mailbody = string.Empty;
                    body = "Address Information has been Approved by " + Supervisor_name + "  | " + empid.ToString().Trim() + ".";
                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/EmployeePI.html");
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    masterbl.SendMail(PERNR_Email.ToString(), MsgCC, "IEmpPower Paycompute - Notification !"
                      , Mailbody.Replace("##RECIPIENTNAME##", PERNR_Name.ToString().Trim()).Replace("##MAILBODY##", body.ToString()));

                   
                }

                else if (sts.Trim() == "Rejected")
                {
                    string body = string.Empty;
                    string Mailbody = string.Empty;
                    body = "Address Information has been Rejected by " + Supervisor_name + "  | " + empid.ToString().Trim() + ".";
                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/EmployeePI.html");
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    masterbl.SendMail(PERNR_Email.ToString(), MsgCC, "IEmpPower Paycompute - Notification !"
                      , Mailbody.Replace("##RECIPIENTNAME##", PERNR_Name.ToString().Trim()).Replace("##MAILBODY##", body.ToString()));

                }


               
           
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }



    public void SendMailCommunication(ref string Supervisor_name, ref string Supervisor_Email, ref string PERNR_Name, ref string PERNR_Email, string sts, string remarks)
    {
        try
        {
                string strSubject = string.Empty;
                string RecipientsString = PERNR_Email;
                string approver = Supervisor_Email;
                string[] MsgCC = { };
                //{ Supervisor_Email.ToString().Trim() };

                string ccode = Session["CompCode"].ToString();
                string empid = User.Identity.Name;
                int cnt = ccode.Length;
                empid = empid.Substring(cnt).ToUpper();    

                if (sts.Trim() == "Approved")
                {
                    string body = string.Empty;
                    string Mailbody = string.Empty;
                    body = "Your Communication details has been Approved by " + Supervisor_name + "  | " + empid.ToString().Trim() + ".";
                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/EmployeePI.html");
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    masterbl.SendMail(PERNR_Email.ToString(), MsgCC, "IEmpPower Paycompute - Notification !"
                      , Mailbody.Replace("##RECIPIENTNAME##", PERNR_Name.ToString().Trim()).Replace("##MAILBODY##", body.ToString()));

                }

                else if (sts.Trim() == "Rejected")
                {
                    string body = string.Empty;
                    string Mailbody = string.Empty;
                    body = "Your Communication details has been Rejected by " + Supervisor_name + "  | " + empid.ToString().Trim() + ".";
                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/EmployeePI.html");
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    masterbl.SendMail(PERNR_Email.ToString(), MsgCC, "IEmpPower Paycompute - Notification !"
                      , Mailbody.Replace("##RECIPIENTNAME##", PERNR_Name.ToString().Trim()).Replace("##MAILBODY##", body.ToString()));

                }

               
            }


        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }



    public void SendMailPIDS(ref string Supervisor_name, ref string Supervisor_Email, ref string PERNR_Name, ref string PERNR_Email, string sts, string remarks)
    {
        try
        {
            string strSubject = string.Empty;
            string RecipientsString = PERNR_Email;
            string approver = Supervisor_Email;
            string[] MsgCC = { };
            //{ Supervisor_Email.ToString().Trim() };

            string ccode = Session["CompCode"].ToString();
            string empid = User.Identity.Name;
            int cnt = ccode.Length;
            empid = empid.Substring(cnt).ToUpper();
                if (sts.Trim() == "Approved")
                {
                    string body = string.Empty;
                    string Mailbody = string.Empty;
                    body = "Personal ID's Information has been Approved by " + Supervisor_name + "  | " + empid.ToString().Trim() + ".";
                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/EmployeePI.html");
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    masterbl.SendMail(PERNR_Email.ToString(), MsgCC, "IEmpPower Paycompute - Notification !"
                      , Mailbody.Replace("##RECIPIENTNAME##", PERNR_Name.ToString().Trim()).Replace("##MAILBODY##", body.ToString()));
                }

                else if (sts.Trim() == "Rejected")
                {
                    string body = string.Empty;
                    string Mailbody = string.Empty;
                    body = "Personal ID's Information has been Rejected by " + Supervisor_name + "  | " + empid.ToString().Trim() + ".";
                    string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/EmployeePI.html");
                    Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                    masterbl.SendMail(PERNR_Email.ToString(), MsgCC, "IEmpPower Paycompute - Notification !"
                      , Mailbody.Replace("##RECIPIENTNAME##", PERNR_Name.ToString().Trim()).Replace("##MAILBODY##", body.ToString()));

                }
               
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    

    protected void GV_DashboardDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    string[] RestrictRowText = { "LEAVE_REQ_ID", "PKEY", "AWART" };//, "Approver comments"
                    if (RestrictRowText.Contains(DataBinder.Eval(e.Row.DataItem, "TEXT")))
                    {
                        e.Row.Style.Add("display", "none");
                        //e.Row.Visible = false;
                        if (RestrictRowText[0] == DataBinder.Eval(e.Row.DataItem, "TEXT").ToString())//LEAVE_REQ_ID
                        { ViewState["Req_ID"] = DataBinder.Eval(e.Row.DataItem, "VALUE"); }


                    }
                    if (DataBinder.Eval(e.Row.DataItem, "TEXT").ToString() == "PERNR")//
                    { ViewState["Req_PERNR"] = DataBinder.Eval(e.Row.DataItem, "VALUE"); }

                    break;
                case DataControlRowType.EmptyDataRow:
                    break;
                case DataControlRowType.Footer:
                    //using (HiddenField HF_LeaveReqID = (HiddenField)GV_DashboardDetails.FooterRow.FindControl("HF_LeaveReqID"))
                    //{
                    //    if (HF_LeaveReqID != null)
                    //    {
                    //        if (DataBinder.Eval(e.Row.DataItem, "TEXT").Equals("LEAVE_REQ_ID"))
                    //        {
                    //            HF_LeaveReqID.Value = DataBinder.Eval(e.Row.DataItem, "TEXT").ToString();
                    //        }
                    //    }
                    //}
                    break;
                case DataControlRowType.Header:
                    break;
                case DataControlRowType.Pager:
                    break;
                case DataControlRowType.Separator:
                    break;
                default:
                    break;
            }
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }

    protected void GV_DashboardDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GV_DashboardDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void GV_DashboardDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GV_DashboardDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    #endregion


    #region Pending Populate pager
    private void PopulatePendingPager(int? RecordCount, int currentPage)
    {
        try
        {
            List<ListItem> pages = new List<ListItem>();
            int startIndex, endIndex;
            int pagerSpan = PagerSpan;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal(PagerSpan));
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
            { pages.Add(new ListItem("First", "1")); }

            //Add the Previous Button.
            if (currentPage > 1)
            { pages.Add(new ListItem("<<", (currentPage - 1).ToString())); }

            for (int i = startIndex; i <= endIndex; i++)
            { pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage)); }

            //Add the Next Button.
            if (currentPage < pageCount)
            { pages.Add(new ListItem(">>", (currentPage + 1).ToString())); }

            //Add the Last Button.
            if (currentPage != pageCount)
            { pages.Add(new ListItem("Last", pageCount.ToString())); }
            RptrPendingPager.DataSource = pages;
            RptrPendingPager.DataBind();
            
            //GV_ClockInClockOut.FooterRow.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;<b>Page " + currentPage + " of " + pageCount + "<b/>";
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }


    protected void LbtnPendingPage_Changed(object sender, EventArgs e)
    {
        try
        {
            int pageIndex = PendingPageIndex = int.Parse((sender as LinkButton).CommandArgument);
            ViewState["PendingPageIndex"] = pageIndex;
            //LoadGridDetails();
            GetHRPernr();

            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;
            grdCompleted.Visible = false;
            divView2.Visible = false;
            grdRecordTime.Visible = false;
            btnRWApprove.Visible = false;
            btnRWReject.Visible = false;
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }


    #endregion

    #region Compleated Pager
    private void PopulateCompleatedPager(int? RecordCount, int currentPage)
    {
        try
        {
            List<ListItem> pages = new List<ListItem>();
            int startIndex, endIndex;
            int pagerSpan = PagerSpan;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal(PagerSpan));
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
            { pages.Add(new ListItem("First", "1")); }

            //Add the Previous Button.
            if (currentPage > 1)
            { pages.Add(new ListItem("<<", (currentPage - 1).ToString())); }

            for (int i = startIndex; i <= endIndex; i++)
            { pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage)); }

            //Add the Next Button.
            if (currentPage < pageCount)
            { pages.Add(new ListItem(">>", (currentPage + 1).ToString())); }

            //Add the Last Button.
            if (currentPage != pageCount)
            { pages.Add(new ListItem("Last", pageCount.ToString())); }
            RptrCompleatedPager.DataSource = pages;
            RptrCompleatedPager.DataBind();

            //GV_ClockInClockOut.FooterRow.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;<b>Page " + currentPage + " of " + pageCount + "<b/>";
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }

    protected void LbtnCompleatedPage_Changed(object sender, EventArgs e)
    {
        try
        {



            //protected int CompleatedPageIndex = 1;
            int pageIndex = CompleatedPageIndex = int.Parse((sender as LinkButton).CommandArgument);
            ViewState["PendingPageIndex_Compleated"] = pageIndex;
            //LoadCompletedGridDetails();
            GetHRPernr();
            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;
           

        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }
    #endregion


    #region GV_DashboardCompleatedDetails Events
    protected void GV_DashboardCompleatedDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GV_DashboardCompleatedDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GV_DashboardCompleatedDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    string[] RestrictRowText = { "LEAVE_REQ_ID", "PKEY", "AWART" };
                    if (RestrictRowText.Contains(DataBinder.Eval(e.Row.DataItem, "TEXT")))
                    {
                        e.Row.Style.Add("display", "none");
                        //e.Row.Visible = false;
                    }

                    break;
                case DataControlRowType.EmptyDataRow:
                    break;
                case DataControlRowType.Footer:
                    break;
                case DataControlRowType.Header:
                    break;
                case DataControlRowType.Pager:
                    break;
                case DataControlRowType.Separator:
                    break;
                default:
                    break;
            }
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }

    protected void GV_DashboardCompleatedDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GV_DashboardCompleatedDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void GV_DashboardCompleatedDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    #endregion

    public void clearchkbox()
    {
        try
        {
            divMassBtn.Visible = false;
            CheckBox ChkBoxHeader = (CheckBox)grdPending.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in grdPending.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = false;
                    divMassBtn.Visible = false;
                }
                if (ChkBoxRows.Checked == true)
                {
                    ChkBoxRows.Checked = false;
                    divMassBtn.Visible = false;
                }
            }
            ChkBoxHeader.Checked = false;
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }

    protected void grdPending_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName.ToUpper())
        {
            case "VIEW":

                clearchkbox();
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                foreach (GridViewRow row in grdPending.Rows)
                {
                    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                                        System.Drawing.ColorTranslator.FromHtml("#ffe6ba") :
                                       System.Drawing.Color.White;
                }
                //HF_TBLTYPE = null;
                //HF_ID = null;
                GV_DashboardDetails.Visible = false;
                GV_DashboardCompleatedDetails.Visible = false;
                int flag = 1;
                AllPnelStatus();
                GridViewRow grdRow = grdPending.Rows[rowIndex];//grdPending.SelectedRow;
                Session.Add("currentSelectedRow", grdRow);
                //string sName = grdRow.Cells[1].Text;
                string sName = grdPending.DataKeys[grdRow.RowIndex]["ENAME"].ToString();
                //string sEmailId = grdRow.Cells[0].Text;
                //string sEmailId = grdPending.DataKeys[grdRow.RowIndex]["USRID"].ToString();
                string sPernr = grdPending.DataKeys[grdRow.RowIndex]["PERNR"].ToString();
                //  string sPkey = grdRow.Cells[2].Text;
                string sPkey = grdPending.DataKeys[grdRow.RowIndex]["PKEY"].ToString();
                string sApprovalType = grdPending.DataKeys[grdRow.RowIndex]["CHANGE_APPROVAL"].ToString();
                DateTime dtLateDate = DateTime.Parse(grdPending.DataKeys[grdRow.RowIndex]["LAST_ACTIVITY_DATE"].ToString());
                // string sRole = grdRow.Cells[7].Text;
                string sRole = grdPending.DataKeys[grdRow.RowIndex]["PLSXT"].ToString();
                int id = int.Parse(grdPending.DataKeys[grdRow.RowIndex]["ID"].ToString());
                string TblTyp = grdPending.DataKeys[grdRow.RowIndex]["TableTyp"].ToString();
                HF_STS.Value = grdPending.DataKeys[grdRow.RowIndex]["REVIEW"].ToString();
                HF_TBLTYPE.Value = TblTyp.ToString().Trim();
                HF_ID.Value = id.ToString().Trim();
                HF_PKEY.Value = sPkey.ToString().Trim();
                string strRecipientsPhn = string.Empty;
                ViewState["LACRTDBY"] = grdPending.DataKeys[grdRow.RowIndex]["PERNR"].ToString().Trim();
                //if (grdRow.Cells[8].Text != "")
                //{
                //    strRecipientsPhn = grdRow.Cells[8].Text;
                //}
                Session.Add("recipientsPhn", strRecipientsPhn);

                Session.Add("sRole", sRole);
                Session.Add("sPernr", sPernr);
                pidashboardbl objPIDashBl = new pidashboardbl();
                try
                {
                    switch (sApprovalType)
                    {
                        case "Address Change Approval":
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            grdRecordTime.Visible = false;
                            GridViewDetails.Visible = true;
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                            // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objPIAddBo.PKEY = sPkey;
                            objPIAddBo.ID = id;
                            pidashboardbl objPIDashbl = new pidashboardbl();
                            piaddressinformationcollectionbo objPIAddBoLst = objPIDashbl.Get_Address_Details_For_Approval(objPIAddBo, flag);
                            if (objPIAddBoLst.Count > 0)
                            {
                                GridViewDetails.DataSource = objPIAddBoLst;
                                GridViewDetails.DataBind();
                            }
                            else
                            {
                                GridViewDetails.DataSource = null;
                                GridViewDetails.DataBind();
                            }

                            break;


                        case "Communication Change Approval":

                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            grdRecordTime.Visible = false;
                            GridViewDetails.Visible = true;
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                            // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objCommuInfoBo.PKEY = sPkey;
                            objCommuInfoBo.ID = id;
                            pidashboardbl objPIDashblC = new pidashboardbl();
                            picommunicationinformationcollectionbo objCommuInfoLst = objPIDashblC.Get_Communication_Details_For_Approval(objCommuInfoBo, flag);
                            if (objCommuInfoLst.Count > 0)
                            {
                                GridViewDetails.DataSource = objCommuInfoLst;
                                GridViewDetails.DataBind();
                            }
                            else
                            {
                                GridViewDetails.DataSource = null;
                                GridViewDetails.DataBind();
                            }

                            break;


                        case "Personal Data Change Approval":
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            grdRecordTime.Visible = false;
                            GridViewDetails.Visible = true;
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            personaldatabo objPersonaldataBo = new personaldatabo();
                            // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objPersonaldataBo.PKEY = sPkey;
                            objPersonaldataBo.ID = id;
                            pidashboardbl objPIDashblPD = new pidashboardbl();
                            personaldatacollectionbo objPersonaldataList = objPIDashblPD.Get_PersonalData_Details_For_Approval(objPersonaldataBo, flag);
                            if (objPersonaldataList.Count > 0)
                            {
                                GridViewDetails.DataSource = objPersonaldataList;
                                GridViewDetails.DataBind();
                            }
                            else
                            {
                                GridViewDetails.DataSource = null;
                                GridViewDetails.DataBind();
                            }

                            break;

                        case "Personal ID Change Approval":
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            grdRecordTime.Visible = false;
                            GridViewDetails.Visible = true;
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                            // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objPersonalIDsBo.PKEY = sPkey;
                            objPersonalIDsBo.ID = id;
                            pidashboardbl objPIDashblPI = new pidashboardbl();
                            pipersonalidscollectionbo objPersonalIDsLst = objPIDashblPI.Get_PersonalIDS_Details_For_Approval(objPersonalIDsBo, flag);
                            if (objPersonalIDsLst.Count > 0)
                            {
                                GridViewDetails.DataSource = objPersonalIDsLst;
                                GridViewDetails.DataBind();
                            }
                            else
                            {
                                GridViewDetails.DataSource = null;
                                GridViewDetails.DataBind();
                            }

                            
                            break;

                        case "Family Members change approval":
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            grdRecordTime.Visible = false;
                            GridViewDetails.Visible = true;
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                            // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objFamilyBo.PKEY = sPkey;
                            objFamilyBo.ID = id;
                            pidashboardbl objPIDashblFM = new pidashboardbl();
                            pifamilymemberscollectionbo objFamilylst = objPIDashblFM.Get_FamilyMemberDetails_For_Approval(objFamilyBo, flag);
                            if (objFamilylst.Count > 0)
                            {
                                GridViewDetails.DataSource = objFamilylst;
                                GridViewDetails.DataBind();
                            }
                            else
                            {
                                GridViewDetails.DataSource = null;
                                GridViewDetails.DataBind();
                            }


                            break;






                        case "Leave Request":
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            grdRecordTime.Visible = false;
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            //pnlLeaveRequest.Visible = true;
                            leaverequestbo objLeaveRequestBo = new leaverequestbo();
                            objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objLeaveRequestBo.PKEY = sPkey;
                            objLeaveRequestBo.LEAVE_REQ_ID = id;
                            //lblLRNameValue.Text = sName;
                            //lblLREmailValue.Text = sEmailId;
                            pidashboardbl objLeaveRequestBl = new pidashboardbl();
                            leaverequestcollectionbo objLeaveReqLst = objLeaveRequestBl.Get_LeaveRequest_Details_For_Approval(objLeaveRequestBo, HF_TBLTYPE.Value.ToString().Trim());
                            //leaverequestbo objLeaveRqstBo = objLeaveReqLst.Find(delegate(leaverequestbo obj)
                            //{ return true; });
                            //Session.Add("LeaveReqId", sPkey);
                            //lblValidateTypeOfLeave.Text = objLeaveRqstBo.ATEXT.ToString();
                            //lblValidateFromDate.Text = objLeaveRqstBo.BEGDA.ToString();
                            //lblValidateToDate.Text = objLeaveRqstBo.ENDDA.ToString();
                            //lblValidateFromTime.Text = objLeaveRqstBo.BEGUZ;
                            //lblValidateToTime.Text = objLeaveRqstBo.ENDUZ;
                            //lblValidateDuration.Text = objLeaveRqstBo.STDAZ.ToString();
                            //lblValidateApprover.Text = objLeaveRqstBo.APPROVED_BY_NAME.ToString();
                            //lblValidateNoteForApprover.Text = objLeaveRqstBo.NOTE;
                            GV_DashboardDetails.Visible = true;
                            GridViewDetails.DataSource = null;
                            GridViewDetails.DataBind();

                            if (objLeaveReqLst.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = objLeaveReqLst;
                                GV_DashboardDetails.DataBind();
                            }
                            else
                            {
                                GVNodata(GV_DashboardDetails);
                            }
                            break;

                        case "Attendance Request":
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            grdRecordTime.Visible = false;
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            //pnlLeaveRequest.Visible = true;
                            leaverequestbo objLeaveRequestBoA = new leaverequestbo();
                            objLeaveRequestBoA.APPROVED_BY_NAME = sRole;
                            objLeaveRequestBoA.PKEY = sPkey;
                            objLeaveRequestBoA.LEAVE_REQ_ID = id;
                            //lblLRNameValue.Text = sName;
                            //lblLREmailValue.Text = sEmailId;
                            pidashboardbl objLeaveRequestBlA = new pidashboardbl();
                            leaverequestcollectionbo objLeaveReqLstA = objLeaveRequestBlA.Get_LeaveRequest_Details_For_Approval(objLeaveRequestBoA, HF_TBLTYPE.Value.ToString().Trim());
                            //leaverequestbo objLeaveRqstBo = objLeaveReqLst.Find(delegate(leaverequestbo obj)
                            //{ return true; });
                            //Session.Add("LeaveReqId", sPkey);
                            //lblValidateTypeOfLeave.Text = objLeaveRqstBo.ATEXT.ToString();
                            //lblValidateFromDate.Text = objLeaveRqstBo.BEGDA.ToString();
                            //lblValidateToDate.Text = objLeaveRqstBo.ENDDA.ToString();
                            //lblValidateFromTime.Text = objLeaveRqstBo.BEGUZ;
                            //lblValidateToTime.Text = objLeaveRqstBo.ENDUZ;
                            //lblValidateDuration.Text = objLeaveRqstBo.STDAZ.ToString();
                            //lblValidateApprover.Text = objLeaveRqstBo.APPROVED_BY_NAME.ToString();
                            //lblValidateNoteForApprover.Text = objLeaveRqstBo.NOTE;
                            GridViewDetails.DataSource = null;
                            GridViewDetails.DataBind();
                            GV_DashboardDetails.Visible = true;
                            if (objLeaveReqLstA.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = objLeaveReqLstA;
                                GV_DashboardDetails.DataBind();
                            }
                            else
                            {
                                GVNodata(GV_DashboardDetails);
                            }
                            break;

                        case "Recordworking Time Details":
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            RWTdiv.Visible = true;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;

                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            grdRecordTime.Visible = true;

                            GridViewDetails.DataSource = null;
                            GridViewDetails.DataBind();
                            GV_DashboardDetails.DataSource = null;
                            GV_DashboardDetails.DataBind();
                            RecordWorkingDetailsControlStatus(true);
                            wtrecordworkingtimebo objRecordBo = new wtrecordworkingtimebo();
                            objRecordBo.PKEY = sPkey;
                            //lblRWNameValue.Text = sName;
                            //lblRWEmailValue.Text = sEmailId;
                            objRecordBo.ISAPPROVED = false;
                            objRecordBo.APPROVEDBY = sRole;
                            objRecordBo.COMMENTS = Session["CompCode"].ToString();
                            wtrecordworkingtimecollectionbo objRecordLst = objPIDashBl.Get_RecordDetails_For_Approval(Session["CompCode"].ToString(), objRecordBo);
                            LoadRecordWorking(objRecordLst, objRecordBo);
                            Session.Add("sRWkey", objRecordBo.PKEY);
                            txtRWComments.Text = "";
                            break;



                        default:

                            break;
                    }

                    txtRWComments.Focus();
                }
                catch (Exception ex)
                {
                    lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    AllPnelStatus();
                    return;
                }

                break;
            default:
                break;
        }

    }
    
    protected void grdCompleted_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName.ToUpper())
        {
            case "VIEW":
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                foreach (GridViewRow row in grdCompleted.Rows)
                {
                    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                                               System.Drawing.ColorTranslator.FromHtml("#ffe6ba") :
                                              System.Drawing.Color.White;
                }
                pidashboardbl objPIDashBl = new pidashboardbl();
                GV_DashboardDetails.Visible = false;
                GV_DashboardCompleatedDetails.Visible = false;
                AllPnelStatus();
                GridViewRow grdRow = grdCompleted.Rows[rowIndex];//SelectedRow;
                Session.Add("currentSelectedRow", grdRow);
                int flag = 1;
                string sName = grdCompleted.DataKeys[grdRow.RowIndex]["ENAME"].ToString();
                //string sEmailId = grdRow.Cells[0].Text;

                string sPernr = grdCompleted.DataKeys[grdRow.RowIndex]["PERNR"].ToString();
                //  string sPkey = grdRow.Cells[2].Text;
                string sPkey = grdCompleted.DataKeys[grdRow.RowIndex]["PKEY"].ToString();
                string sApprovalType = grdCompleted.DataKeys[grdRow.RowIndex]["CHANGE_APPROVAL"].ToString();
                DateTime dtLateDate = DateTime.Parse(grdCompleted.DataKeys[grdRow.RowIndex]["LAST_ACTIVITY_DATE"].ToString());
                // string sRole = grdRow.Cells[7].Text;
                string sRole = grdCompleted.DataKeys[grdRow.RowIndex]["PLSXT"].ToString();
                int id = int.Parse(grdCompleted.DataKeys[grdRow.RowIndex]["ID"].ToString());
                string TblTyp = grdCompleted.DataKeys[grdRow.RowIndex]["TableTyp"].ToString();
                string sts = grdCompleted.DataKeys[grdRow.RowIndex]["REVIEW"].ToString();
                DateTime dtLateActDate = DateTime.Parse(grdCompleted.DataKeys[grdRow.RowIndex]["LAST_ACTIVITY_DATE"].ToString());
                DateTime ModifiedDate = DateTime.Parse(grdCompleted.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString());
                HF_TBLTYPE.Value = TblTyp.ToString().Trim();
                HF_ID.Value = id.ToString().Trim();
                HF_PKEY.Value = sPkey.ToString().Trim();
                string strRecipientsPhn = string.Empty;

                try
                {
                    switch (sApprovalType)
                    {
                        case "Address Change Approval":

                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            GV_DashboardCompleatedDetails.Visible = true;
                            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                            objPIAddBo.PKEY = sPkey;
                            objPIAddBo.ID = id;
                            pidashboardbl objPIDashblA = new pidashboardbl();
                            piaddressinformationcollectionbo objPIAddBoLst = objPIDashblA.Get_Address_completed_Details_For_Approval(objPIAddBo, sts, dtLateActDate, ModifiedDate);
                            if (objPIAddBoLst.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GV_DashboardCompleatedDetails.DataSource = objPIAddBoLst;
                                GV_DashboardCompleatedDetails.DataBind();
                            }
                            else
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GVNodata(GV_DashboardCompleatedDetails);
                            }
                            break;


                        case "Communication Change Approval":
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            GV_DashboardCompleatedDetails.Visible = true;
                            picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                            objCommuInfoBo.PKEY = sPkey;
                            objCommuInfoBo.ID = id;
                            pidashboardbl objPIDashblC = new pidashboardbl();
                            picommunicationinformationcollectionbo objCommuInfoLst = objPIDashblC.Get_Communication_completed_Details_For_Approval(objCommuInfoBo, sts, dtLateActDate, ModifiedDate);
                            if (objCommuInfoLst.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GV_DashboardCompleatedDetails.DataSource = objCommuInfoLst;
                                GV_DashboardCompleatedDetails.DataBind();
                            }
                            else
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GVNodata(GV_DashboardCompleatedDetails);
                            }

                            break;

                        case "Personal Data Change Approval":

                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            GV_DashboardCompleatedDetails.Visible = true;
                            personaldatabo objPersonaldataBo = new personaldatabo();
                            objPersonaldataBo.PKEY = sPkey;
                            objPersonaldataBo.ID = id;
                            pidashboardbl objPIDashblPD = new pidashboardbl();
                            personaldatacollectionbo objPersonaldataList = objPIDashblPD.Get_PersonalData_completed_Details_For_Approval(objPersonaldataBo, sts, dtLateActDate, ModifiedDate);
                            if (objPersonaldataList.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GV_DashboardCompleatedDetails.DataSource = objPersonaldataList;
                                GV_DashboardCompleatedDetails.DataBind();
                            }
                            else
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GVNodata(GV_DashboardCompleatedDetails);
                            }



                            break;


                        case "Personal ID Change Approval":

                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            GV_DashboardCompleatedDetails.Visible = true;
                            pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                            objPersonalIDsBo.PKEY = sPkey;
                            objPersonalIDsBo.ID = id;
                            pidashboardbl objPIDashblPI = new pidashboardbl();
                            pipersonalidscollectionbo objPersonalIDsLst = objPIDashblPI.Get_PersonalIDS_completed_Details_For_Approval(objPersonalIDsBo, sts, dtLateActDate, ModifiedDate);
                            if (objPersonalIDsLst.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GV_DashboardCompleatedDetails.DataSource = objPersonalIDsLst;
                                GV_DashboardCompleatedDetails.DataBind();
                            }
                            else
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GVNodata(GV_DashboardCompleatedDetails);
                            }

                            break;

                        case "Family Members Change Approval":
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            GV_DashboardCompleatedDetails.Visible = true;
                            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                            objFamilyBo.PKEY = sPkey;
                            objFamilyBo.ID = id;
                            pidashboardbl objPIDashblF = new pidashboardbl();
                            pifamilymemberscollectionbo objFamilylst = objPIDashblF.Get_Family_completed_Details_For_Approval(objFamilyBo, sts, dtLateActDate, ModifiedDate);
                            if (objFamilylst.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GV_DashboardCompleatedDetails.DataSource = objFamilylst;
                                GV_DashboardCompleatedDetails.DataBind();
                            }
                            else
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GVNodata(GV_DashboardCompleatedDetails);
                            }
                            break;

                        case "Deletion Request Approval":
                            //LeaveRequestDetailsControlStatus(true);
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            GV_DashboardCompleatedDetails.Visible = true;
                            leaverequestbo objLeaveRequestBoD = new leaverequestbo();
                            objLeaveRequestBoD.PKEY = sPkey;
                            objLeaveRequestBoD.LEAVE_REQ_ID = id;
                            string TblTypD = grdCompleted.DataKeys[grdRow.RowIndex]["TableTyp"].ToString();
                            pidashboardbl objLeaveRequestBlD = new pidashboardbl();

                            leaverequestcollectionbo objLeaveReqLstD = objLeaveRequestBlD.Get_DeletionRequest_Details_For_Approval_For_Employee(objLeaveRequestBoD, TblTypD);

                            if (objLeaveReqLstD.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GV_DashboardCompleatedDetails.DataSource = objLeaveReqLstD;
                                GV_DashboardCompleatedDetails.DataBind();
                            }
                            else
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GVNodata(GV_DashboardCompleatedDetails);
                            }


                            break;


                        case "Leave Request Approval":
                            //pnlLeaveRequest.Visible = true;
                            //LeaveRequestDetailsControlStatus(false);
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            GV_DashboardCompleatedDetails.Visible = true;
                            leaverequestbo objLeaveRequestBo = new leaverequestbo();
                            objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objLeaveRequestBo.PKEY = sPkey;
                            objLeaveRequestBo.LEAVE_REQ_ID = id;
                            //lblLRNameValue.Text = sName;
                            //lblLREmailValue.Text = sEmailId;
                            pidashboardbl objLeaveRequestBl = new pidashboardbl();
                            leaverequestcollectionbo objLeaveReqLst = objLeaveRequestBl.Get_LeaveRequest_Details_For_Approval(objLeaveRequestBo, HF_TBLTYPE.Value.ToString().Trim());

                            if (objLeaveReqLst.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GV_DashboardCompleatedDetails.DataSource = objLeaveReqLst;
                                GV_DashboardCompleatedDetails.DataBind();
                            }
                            else
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GVNodata(GV_DashboardCompleatedDetails);
                            }


                            break;


                        case "Attendance Request Approval":
                            //pnlLeaveRequest.Visible = true;
                            //LeaveRequestDetailsControlStatus(false);
                            grdRecordTime.DataSource = null;
                            grdRecordTime.DataBind();
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = false;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = false;
                            GV_DashboardCompleatedDetails.Visible = true;
                            leaverequestbo objLeaveRequestBoA = new leaverequestbo();
                            objLeaveRequestBoA.APPROVED_BY_NAME = sRole;
                            objLeaveRequestBoA.PKEY = sPkey;
                            objLeaveRequestBoA.LEAVE_REQ_ID = id;
                            //lblLRNameValue.Text = sName;
                            //lblLREmailValue.Text = sEmailId;
                            pidashboardbl objLeaveRequestBlA = new pidashboardbl();
                            leaverequestcollectionbo objLeaveReqLstA = objLeaveRequestBlA.Get_LeaveRequest_Details_For_Approval(objLeaveRequestBoA, HF_TBLTYPE.Value.ToString().Trim());

                            if (objLeaveReqLstA.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GV_DashboardCompleatedDetails.DataSource = objLeaveReqLstA;
                                GV_DashboardCompleatedDetails.DataBind();
                            }
                            else
                            {
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();
                                GVNodata(GV_DashboardCompleatedDetails);
                            }


                            break;



                        case "Recordworking Time Details":

                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                            GV_DashboardCompleatedDetails.DataBind();
                            GV_DashboardDetails.DataBind();
                            GridViewDetails.DataBind();
                            grdRecordTime.DataBind();
                            RWTdiv.Visible = false;
                            txtRWComments.Text = string.Empty;
                            lblValidateRWCommnets.Text = string.Empty;
                            TblRemarks.Visible = true;
                            lblRemarksRWT.Text = string.Empty;
                            lblRemarksRWT.Visible = true;
                            RecordWorkingDetailsControlStatus(false);
                            wtrecordworkingtimebo objRecordBo = new wtrecordworkingtimebo();
                            objRecordBo.PKEY = sPkey;
                            //lblRWNameValue.Text = sName;
                            //lblRWEmailValue.Text = sEmailId;
                            objRecordBo.ISAPPROVED = true;
                            objRecordBo.APPROVEDBY = sRole;
                            objRecordBo.COMMENTS = Session["CompCode"].ToString();
                            wtrecordworkingtimecollectionbo objRecordLst = objPIDashBl.Get_RecordDetails_For_Approval(Session["CompCode"].ToString(), objRecordBo);
                            wtrecordworkingtimebo objRWBo = objRecordLst.Find(delegate(wtrecordworkingtimebo obj)
                            { return true; });
                            LoadRecordWorking(objRecordLst, objRecordBo);
                            lblRemarksRWT.Text = objRWBo.COMMENTS;
                           
                            break;


                        default:

                            break;
                    }
                    pnlRecordWorking.Focus();
                }
                catch (Exception)
                {
                    lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    AllPnelStatus();
                    return;
                }

                break;
            default:
                break;
        }



    }    

    protected void GridViewDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        switch (e.CommandName.ToUpper())
        {
            case "VIEW":
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                foreach (GridViewRow row in GridViewDetails.Rows)
                {
                    row.BackColor = row.RowIndex.Equals(rowIndex) ?
                                            System.Drawing.ColorTranslator.FromHtml("#ffe6ba") :
                                           System.Drawing.Color.White;
                }
                int flag = 2;
                GridViewRow grdRow = GridViewDetails.Rows[rowIndex];//SelectedRow;SelectedRow;
                string sPkey = GridViewDetails.DataKeys[grdRow.RowIndex]["PKEY"].ToString();
                int id = int.Parse(GridViewDetails.DataKeys[grdRow.RowIndex]["ID"].ToString());
                string sApprovalType = GridViewDetails.DataKeys[grdRow.RowIndex]["CHANGE_APPROVAL"].ToString();
                string statustype = GridViewDetails.DataKeys[grdRow.RowIndex]["STATUS"].ToString().Trim();

                ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();
                //ViewState["LACRTDBY"] = GridViewDetails.DataKeys[grdRow.RowIndex]["CREATED_BY"].ToString().Trim();

                pidashboardbl objPIDashBl = new pidashboardbl();
                try
                {
                    switch (sApprovalType)
                    {
                        case "Address change approval":

                            GridViewDetails.Visible = true;
                            GV_DashboardDetails.Visible = true;
                            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                            // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objPIAddBo.PKEY = sPkey;
                            objPIAddBo.ID = id;
                            objPIAddBo.STATUS = statustype;
                            pidashboardbl objPIDashbl = new pidashboardbl();
                            piaddressinformationcollectionbo objPIAddBoLst = objPIDashbl.Get_Address_Details_For_Approval(objPIAddBo, flag);
                            if (objPIAddBoLst.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = objPIAddBoLst;
                                GV_DashboardDetails.DataBind();

                                using (Button BtnApp = (Button)GV_DashboardDetails.FooterRow.FindControl("BtnGvReqApprove"))
                                using (Button BtnRej = (Button)GV_DashboardDetails.FooterRow.FindControl("BtnGvReqReject"))
                                using (TextBox TxtRemaeks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                using (RequiredFieldValidator RFVRemarks = (RequiredFieldValidator)GV_DashboardDetails.FooterRow.FindControl("RFV_TxtGvRemarks"))
                                {
                                    if (statustype == "Approved")
                                    {
                                        GV_DashboardDetails.ShowFooter = false;
                                        BtnApp.Visible = BtnApp.Enabled = false;
                                        BtnRej.Visible = BtnRej.Enabled = false;
                                        TxtRemaeks.Visible = TxtRemaeks.Enabled = false;
                                        RFVRemarks.Visible = RFVRemarks.Enabled = false;
                                    }

                                    else if (statustype == "Updated")
                                    {
                                        GV_DashboardDetails.ShowFooter = true;
                                        BtnApp.Visible = BtnApp.Enabled = true;
                                        BtnRej.Visible = BtnRej.Enabled = true;
                                        TxtRemaeks.Visible = TxtRemaeks.Enabled = true;
                                        RFVRemarks.Visible = RFVRemarks.Enabled = true;
                                    }
                                }
                                GV_DashboardDetails.DataSource = objPIAddBoLst;
                                GV_DashboardDetails.DataBind();
                            }
                            else
                            {
                                GVNodata(GV_DashboardDetails);
                            }

                            break;


                        case "Communication change approval":

                            GridViewDetails.Visible = true;
                            GV_DashboardDetails.Visible = true;
                            picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                            // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objCommuInfoBo.PKEY = sPkey;
                            objCommuInfoBo.ID = id;
                            objCommuInfoBo.STATUS = statustype;
                            pidashboardbl objPIDashblC = new pidashboardbl();
                            picommunicationinformationcollectionbo objCommuInfoLst = objPIDashblC.Get_Communication_Details_For_Approval(objCommuInfoBo, flag);
                            if (objCommuInfoLst.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = objCommuInfoLst;
                                GV_DashboardDetails.DataBind();

                                using (Button BtnApp = (Button)GV_DashboardDetails.FooterRow.FindControl("BtnGvReqApprove"))
                                using (Button BtnRej = (Button)GV_DashboardDetails.FooterRow.FindControl("BtnGvReqReject"))
                                using (TextBox TxtRemaeks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                using (RequiredFieldValidator RFVRemarks = (RequiredFieldValidator)GV_DashboardDetails.FooterRow.FindControl("RFV_TxtGvRemarks"))
                                {
                                    if (statustype == "Approved")
                                    {
                                        GV_DashboardDetails.ShowFooter = false;
                                        BtnApp.Visible = BtnApp.Enabled = false;
                                        BtnRej.Visible = BtnRej.Enabled = false;
                                        TxtRemaeks.Visible = TxtRemaeks.Enabled = false;
                                        RFVRemarks.Visible = RFVRemarks.Enabled = false;
                                    }

                                    else if (statustype == "Updated")
                                    {
                                        GV_DashboardDetails.ShowFooter = true;
                                        BtnApp.Visible = BtnApp.Enabled = true;
                                        BtnRej.Visible = BtnRej.Enabled = true;
                                        TxtRemaeks.Visible = TxtRemaeks.Enabled = true;
                                        RFVRemarks.Visible = RFVRemarks.Enabled = true;
                                    }
                                }
                                GV_DashboardDetails.DataSource = objCommuInfoLst;
                                GV_DashboardDetails.DataBind();
                            }
                            else
                            {
                                GVNodata(GV_DashboardDetails);
                            }

                            break;

                        case "Personal Data change approval":

                            GridViewDetails.Visible = true;
                            GV_DashboardDetails.Visible = true;
                            personaldatabo objPersonaldataBo = new personaldatabo();
                            // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objPersonaldataBo.PKEY = sPkey;
                            objPersonaldataBo.ID = id;
                            objPersonaldataBo.STATUS = statustype;
                            pidashboardbl objPIDashblPD = new pidashboardbl();
                            personaldatacollectionbo objPersonaldataList = objPIDashblPD.Get_PersonalData_Details_For_Approval(objPersonaldataBo, flag);
                            if (objPersonaldataList.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = objPersonaldataList;
                                GV_DashboardDetails.DataBind();

                                using (Button BtnApp = (Button)GV_DashboardDetails.FooterRow.FindControl("BtnGvReqApprove"))
                                using (Button BtnRej = (Button)GV_DashboardDetails.FooterRow.FindControl("BtnGvReqReject"))
                                using (TextBox TxtRemaeks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                using (RequiredFieldValidator RFVRemarks = (RequiredFieldValidator)GV_DashboardDetails.FooterRow.FindControl("RFV_TxtGvRemarks"))
                                {
                                    if (statustype == "Approved")
                                    {
                                        GV_DashboardDetails.ShowFooter = false;
                                        BtnApp.Visible = BtnApp.Enabled = false;
                                        BtnRej.Visible = BtnRej.Enabled = false;
                                        TxtRemaeks.Visible = TxtRemaeks.Enabled = false;
                                        RFVRemarks.Visible = RFVRemarks.Enabled = false;
                                    }

                                    else if (statustype == "Updated")
                                    {
                                        GV_DashboardDetails.ShowFooter = true;
                                        BtnApp.Visible = BtnApp.Enabled = true;
                                        BtnRej.Visible = BtnRej.Enabled = true;
                                        TxtRemaeks.Visible = TxtRemaeks.Enabled = true;
                                        RFVRemarks.Visible = RFVRemarks.Enabled = true;
                                    }
                                }
                                GV_DashboardDetails.DataSource = objPersonaldataList;
                                GV_DashboardDetails.DataBind();
                            }
                            else
                            {
                                GVNodata(GV_DashboardDetails);
                            }

                            break;

                        case "Personal ID change approval":

                            GridViewDetails.Visible = true;
                            GV_DashboardDetails.Visible = true;
                            pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                            // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objPersonalIDsBo.PKEY = sPkey;
                            objPersonalIDsBo.ID = id;
                            objPersonalIDsBo.STATUS = statustype;
                            pidashboardbl objPIDashblPI = new pidashboardbl();
                            pipersonalidscollectionbo objPersonalIDsLst = objPIDashblPI.Get_PersonalIDS_Details_For_Approval(objPersonalIDsBo, flag);
                            if (objPersonalIDsLst.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = objPersonalIDsLst;
                                GV_DashboardDetails.DataBind();

                                using (Button BtnApp = (Button)GV_DashboardDetails.FooterRow.FindControl("BtnGvReqApprove"))
                                using (Button BtnRej = (Button)GV_DashboardDetails.FooterRow.FindControl("BtnGvReqReject"))
                                using (TextBox TxtRemaeks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                using (RequiredFieldValidator RFVRemarks = (RequiredFieldValidator)GV_DashboardDetails.FooterRow.FindControl("RFV_TxtGvRemarks"))
                                {
                                    if (statustype == "Approved")
                                    {
                                        GV_DashboardDetails.ShowFooter = false;
                                        BtnApp.Visible = BtnApp.Enabled = false;
                                        BtnRej.Visible = BtnRej.Enabled = false;
                                        TxtRemaeks.Visible = TxtRemaeks.Enabled = false;
                                        RFVRemarks.Visible = RFVRemarks.Enabled = false;
                                    }

                                    else if (statustype == "Updated")
                                    {
                                        GV_DashboardDetails.ShowFooter = true;
                                        BtnApp.Visible = BtnApp.Enabled = true;
                                        BtnRej.Visible = BtnRej.Enabled = true;
                                        TxtRemaeks.Visible = TxtRemaeks.Enabled = true;
                                        RFVRemarks.Visible = RFVRemarks.Enabled = true;
                                    }
                                }
                                GV_DashboardDetails.DataSource = objPersonalIDsLst;
                                GV_DashboardDetails.DataBind();
                            }
                            else
                            {
                                GVNodata(GV_DashboardDetails);
                            }

                            break;

                        case "Family Members change approval":

                            GridViewDetails.Visible = true;
                            GV_DashboardDetails.Visible = true;
                            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                            // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                            objFamilyBo.PKEY = sPkey;
                            objFamilyBo.ID = id;
                            objFamilyBo.STATUS = statustype;
                            pidashboardbl objPIDashblFM = new pidashboardbl();
                            pifamilymemberscollectionbo objFamilylst = objPIDashblFM.Get_FamilyMemberDetails_For_Approval(objFamilyBo, flag);
                            if (objFamilylst.Count > 0)
                            {
                                GV_DashboardDetails.DataSource = objFamilylst;
                                GV_DashboardDetails.DataBind();

                                using (Button BtnApp = (Button)GV_DashboardDetails.FooterRow.FindControl("BtnGvReqApprove"))
                                using (Button BtnRej = (Button)GV_DashboardDetails.FooterRow.FindControl("BtnGvReqReject"))
                                using (TextBox TxtRemaeks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                using (RequiredFieldValidator RFVRemarks = (RequiredFieldValidator)GV_DashboardDetails.FooterRow.FindControl("RFV_TxtGvRemarks"))
                                {
                                    if (statustype == "Approved")
                                    {
                                        GV_DashboardDetails.ShowFooter = false;
                                        BtnApp.Visible = BtnApp.Enabled = false;
                                        BtnRej.Visible = BtnRej.Enabled = false;
                                        TxtRemaeks.Visible = TxtRemaeks.Enabled = false;
                                        RFVRemarks.Visible = RFVRemarks.Enabled = false;
                                    }

                                    else if (statustype == "Updated")
                                    {
                                        GV_DashboardDetails.ShowFooter = true;
                                        BtnApp.Visible = BtnApp.Enabled = true;
                                        BtnRej.Visible = BtnRej.Enabled = true;
                                        TxtRemaeks.Visible = TxtRemaeks.Enabled = true;
                                        RFVRemarks.Visible = RFVRemarks.Enabled = true;
                                    }
                                }
                                GV_DashboardDetails.DataSource = objFamilylst;
                                GV_DashboardDetails.DataBind();
                            }
                            else
                            {
                                GVNodata(GV_DashboardDetails);
                            }

                            break;
                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    AllPnelStatus();
                    return;
                }
                break;
            default:
                break;
        }



    }
    protected void DDLYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           if (HF_TABID.Value == "1")
            {
                //LoadGridDetails();

                HRTabSel.Visible = true;
                LoadGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());



                //GetHRPernr();
                GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_DashboardDetails.DataBind();
                GridViewDetails.DataBind();
                grdRecordTime.DataBind();
                RWTdiv.Visible = false;
                txtRWComments.Text = string.Empty;
                lblValidateRWCommnets.Text = string.Empty;
                TblRemarks.Visible = false;
                lblRemarksRWT.Text = string.Empty;
                lblRemarksRWT.Visible = false;

            }
            else
            {

                HRTabSel.Visible = true;
                LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());

                //LoadCompletedGridDetails();
                GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_DashboardDetails.DataBind();
                GridViewDetails.DataBind();
                grdRecordTime.DataBind();
                RWTdiv.Visible = false;
                txtRWComments.Text = string.Empty;
                lblValidateRWCommnets.Text = string.Empty;
                TblRemarks.Visible = false;
                lblRemarksRWT.Text = string.Empty;
                lblRemarksRWT.Visible = false;
            }
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }

    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)grdPending.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in grdPending.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
                divMassBtn.Visible = true;
                //pnlLeaveRequest.Visible = false;
                //pnlFamilyMember.Visible = false;
                GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_DashboardDetails.DataBind();
                GridViewDetails.DataBind();
                grdRecordTime.DataBind();
                RWTdiv.Visible = false;
                txtRWComments.Text = string.Empty;
                lblValidateRWCommnets.Text = string.Empty;
                TblRemarks.Visible = false;
                lblRemarksRWT.Text = string.Empty;
                lblRemarksRWT.Visible = false;
                btnRWApprove.Visible = false;
                btnRWReject.Visible = false;


                GV_DashboardDetails.DataSource = null;
                GV_DashboardDetails.DataBind();
                GV_DashboardDetails.Visible = false;
                GridViewDetails.DataSource = null;
                GridViewDetails.DataBind();
                GridViewDetails.Visible = false;
                btnLRApp.Focus();
            }
            else
            {
                ChkBoxRows.Checked = false;
                divMassBtn.Visible = false;
                GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_DashboardDetails.DataBind();
                GridViewDetails.DataBind();
                grdRecordTime.DataBind();
                RWTdiv.Visible = false;
                txtRWComments.Text = string.Empty;
                lblValidateRWCommnets.Text = string.Empty;
                TblRemarks.Visible = false;
                lblRemarksRWT.Text = string.Empty;
                lblRemarksRWT.Visible = false;

                btnRWApprove.Visible = false;
                btnRWReject.Visible = false;

                GV_DashboardDetails.DataSource = null;
                GV_DashboardDetails.DataBind();
                GV_DashboardDetails.Visible = false;
                GridViewDetails.DataSource = null;
                GridViewDetails.DataBind();
                GridViewDetails.Visible = false;
            }
        }
    }
    protected void chkEmp_CheckedChanged(object sender, EventArgs e)
    {
        int CKcount = 0;
        CheckBox chkbx_select = (CheckBox)sender;
        CheckBox ChkBoxHeader = (CheckBox)grdPending.HeaderRow.FindControl("chkboxSelectAll");


        foreach (GridViewRow row in grdPending.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
            {
                if (ChkBoxRows.Checked)
                {
                    CKcount = CKcount + 1;
                }

            }
        }

        if (CKcount > 0)
        {
            divMassBtn.Visible = true;
            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;
            btnRWApprove.Visible = false;
            btnRWReject.Visible = false;


            GV_DashboardDetails.DataSource = null;
            GV_DashboardDetails.DataBind();
            GV_DashboardDetails.Visible = false;
            GridViewDetails.DataSource = null;
            GridViewDetails.DataBind();
            GridViewDetails.Visible = false;
            //pnlLeaveRequest.Visible = false;
            //pnlFamilyMember.Visible = false;
        }
        else
        {
            divMassBtn.Visible = false;
            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;
            btnRWApprove.Visible = false;
            btnRWReject.Visible = false;

            GV_DashboardDetails.DataSource = null;
            GV_DashboardDetails.DataBind();
            GV_DashboardDetails.Visible = false;
            GridViewDetails.DataSource = null;
            GridViewDetails.DataBind();
            GridViewDetails.Visible = false;
            //pnlLeaveRequest.Visible = false;
            //pnlFamilyMember.Visible = false;
        }


        if (!chkbx_select.Checked)
        {
            // Checkbox was unchecked, 
            // short circuit, set Select All Checkbox.Checked = false and return

            ChkBoxHeader.Checked = false;
            return;
        }

        bool allChecked = true;
        foreach (GridViewRow row in grdPending.Rows)
        {
            // this is pseudocode, find checkbox on each row
            CheckBox rowcheckBox = (CheckBox)row.FindControl("chkEmp");
            if (!rowcheckBox.Checked)
            {
                allChecked = false;
                break;
            }
        }

        ChkBoxHeader.Checked = allChecked;
    }
    protected void btnLRAppApprove_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        if (DDL_HRTabSel.SelectedValue.ToString().Trim() != "2")
        {
            Mass_Approve_Reject_RequestDetails(bIStatus);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Approved successfully !')", true);
        }
        else if (DDL_HRTabSel.SelectedValue.ToString().Trim() == "2")
        {
            Mass_Approve_Reject_RecordWorkingDetails(bIStatus);
            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
            lblMessageBoard.Text = "Approved Successfully !";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Approved Successfully !');", true);
        }

        GetHRPernr();
        ViewState["PendingPageIndex"] = "0";
        HF_TBLTYPE = null;
        HF_ID = null;
        HF_PKEY = null;
        HF_STS = null;
        GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
        GV_DashboardCompleatedDetails.DataBind();
        GV_DashboardDetails.DataBind();
        GridViewDetails.DataBind();
        grdRecordTime.DataBind();
        RWTdiv.Visible = false;
        txtRWComments.Text = string.Empty;
        lblValidateRWCommnets.Text = string.Empty;
        TblRemarks.Visible = false;
        lblRemarksRWT.Text = string.Empty;
        lblRemarksRWT.Visible = false;
        GV_DashboardDetails.DataSource = null;
        GV_DashboardDetails.DataBind();
        GV_DashboardDetails.Visible = false;
        GridViewDetails.DataSource = null;
        GridViewDetails.DataBind();
        GridViewDetails.Visible = false;
        divView2.Visible = false;
        grdCompleted.Visible = false;
    }


    protected void btnLRRejReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        if (DDL_HRTabSel.SelectedValue.ToString().Trim() != "2")
        {
            Mass_Approve_Reject_RequestDetails(bIStatus);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Rejected successfully !')", true);
        }
        else if (DDL_HRTabSel.SelectedValue.ToString().Trim() == "2")
        {
            Mass_Approve_Reject_RecordWorkingDetails(bIStatus);
            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
            lblMessageBoard.Text = "Rejected Successfully !";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Rejected Successfully !');", true);
        }

        GetHRPernr();
        ViewState["PendingPageIndex"] = "0";
        HF_TBLTYPE = null;
        HF_ID = null;
        HF_PKEY = null;
        HF_STS = null;
        GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
        GV_DashboardCompleatedDetails.DataBind();
        GV_DashboardDetails.DataBind();
        GridViewDetails.DataBind();
        grdRecordTime.DataBind();
        RWTdiv.Visible = false;
        txtRWComments.Text = string.Empty;
        lblValidateRWCommnets.Text = string.Empty;
        TblRemarks.Visible = false;
        lblRemarksRWT.Text = string.Empty;
        lblRemarksRWT.Visible = false;
        GV_DashboardDetails.DataSource = null;
        GV_DashboardDetails.DataBind();
        GV_DashboardDetails.Visible = false;
        GridViewDetails.DataSource = null;
        GridViewDetails.DataBind();
        GridViewDetails.Visible = false;
        divView2.Visible = false;
        grdCompleted.Visible = false;

    }
    protected void Mass_Approve_Reject_RequestDetails(bool bIStatus)
    {
        try
        {

            foreach (GridViewRow gvrow in grdPending.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkEmp");
                if (chk != null & chk.Checked)
                {
                    //int IEXP_ID = int.Parse(grdAppRejIexp.DataKeys[item.RowIndex].Values["IEXP_ID"].ToString());
                    //ViewState["IEXP_ID"] = IEXP_ID.ToString();
                    //ViewState["Createdby"] = grdAppRejIexp.DataKeys[item.RowIndex].Values["CREATED_BY"].ToString();
                    //ViewState["ProjectforMail"] = grdAppRejIexp.DataKeys[item.RowIndex].Values["POST1"].ToString();
                    string CaseType = bIStatus == true ? "APPROVE" : "REJECT";

                    string sName = grdPending.DataKeys[gvrow.RowIndex].Values["ENAME"].ToString();
                    //string sEmailId = grdRow.Cells[0].Text;
                    //string sEmailId = grdPending.DataKeys[grdRow.RowIndex]["USRID"].ToString();
                    string sPernr = grdPending.DataKeys[gvrow.RowIndex].Values["PERNR"].ToString();
                    //  string sPkey = grdRow.Cells[2].Text;
                    string sPkey = grdPending.DataKeys[gvrow.RowIndex].Values["PKEY"].ToString();
                    string sApprovalType = grdPending.DataKeys[gvrow.RowIndex].Values["CHANGE_APPROVAL"].ToString();
                    DateTime dtLateDate = DateTime.Parse(grdPending.DataKeys[gvrow.RowIndex].Values["LAST_ACTIVITY_DATE"].ToString());
                    // string sRole = grdRow.Cells[7].Text;
                    string sRole = grdPending.DataKeys[gvrow.RowIndex].Values["PLSXT"].ToString();
                    int id = int.Parse(grdPending.DataKeys[gvrow.RowIndex].Values["ID"].ToString());
                    string TblTyp = grdPending.DataKeys[gvrow.RowIndex].Values["TableTyp"].ToString();
                    HF_STS.Value = grdPending.DataKeys[gvrow.RowIndex].Values["REVIEW"].ToString();

                    HF_TBLTYPE.Value = TblTyp.ToString().Trim();
                    HF_ID.Value = id.ToString().Trim();
                    HF_PKEY.Value = sPkey.ToString().Trim();
                    ViewState["LASTACTIVITYDATE"] = grdPending.DataKeys[gvrow.RowIndex].Values["LAST_ACTIVITY_DATE"].ToString().Trim();
                    ViewState["MMODON"] = grdPending.DataKeys[gvrow.RowIndex].Values["MMODON"].ToString().Trim();

                    ViewState["LACRTDBY"] = grdPending.DataKeys[gvrow.RowIndex].Values["PERNR"].ToString().Trim();

                    if (HF_TBLTYPE != null && HF_ID != null && HF_PKEY != null)
                    {
                        //if (GV_DashboardDetails.Rows.Count > 0)
                        //{
                        switch (HF_TBLTYPE.Value.ToString().Trim().ToUpper())
                        {

                            case "PA2001":

                                switch (CaseType)
                                {
                                    case "APPROVE": // Flag - 1
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Approved";
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            ObjBO.TableTyp = "PA2001";
                                            ObjBO.PERNR = ViewState["LACRTDBY"].ToString().Trim();


                                            ObjBL.Mngr_Leave_Req_Approve_Reject(Session["CompCode"].ToString(), ObjBO, ref HR_Email, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);


                                            leaverequestbo objBo = new leaverequestbo();
                                            leaverequestbl objBl = new leaverequestbl();
                                            leaverequestcollectionbo objLst = new leaverequestcollectionbo();

                                            List<leaverequestbo> objList = new List<leaverequestbo>();

                                            if (HF_STS.Value.ToString().Trim().ToUpper() == "DELETION REQUESTED")
                                            {
                                                objList = objBl.Deletion_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Deletion request approved", "PA2001");

                                                SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request approved", "PA2001");

                                            }
                                            else
                                            {
                                                objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved", "PA2001");

                                                SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Approved", "Approved", "PA2001");
                                            }
                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID = null;
                                            //HF_PKEY = null;
                                            //HF_STS = null;


                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;

                                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Leave Request approved successfully !')", true);

                                        }
                                        //    }
                                        //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                        //}
                                        break;
                                    case "REJECT": // Flag - 2
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;


                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Rejected";
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            ObjBO.TableTyp = "PA2001";
                                            ObjBO.PERNR = ViewState["LACRTDBY"].ToString().Trim();

                                            ObjBL.Mngr_Leave_Req_Approve_Reject(Session["CompCode"].ToString(), ObjBO, ref HR_Email, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);


                                            leaverequestbo objBo = new leaverequestbo();
                                            leaverequestbl objBl = new leaverequestbl();
                                            leaverequestcollectionbo objLst = new leaverequestcollectionbo();

                                            List<leaverequestbo> objList = new List<leaverequestbo>();
                                            //objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected", "PA2001");


                                            //SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim(), "PA2001");


                                            if (HF_STS.Value.ToString().Trim().ToUpper() == "DELETION REQUESTED")
                                            {
                                                objList = objBl.Deletion_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Deletion request rejected", "PA2001");

                                                SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request rejected", "PA2001");

                                            }
                                            else
                                            {
                                                objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected", "PA2001");

                                                SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", "Rejected", "PA2001");
                                            }

                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID = null;
                                            //HF_PKEY = null;
                                            //HF_STS = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;

                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Leave Request rejected successfully !')", true);
                                        }
                                        //    }
                                        //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                        //}
                                        break;
                                    default:
                                        break;
                                }

                                break;

                            // --------------------Attendance

                            case "PA2002":

                                switch (CaseType)
                                {
                                    case "APPROVE": // Flag - 1
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Approved";
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            ObjBO.TableTyp = "PA2002";
                                            ObjBO.PERNR = ViewState["LACRTDBY"].ToString().Trim();

                                            ObjBL.Mngr_Leave_Req_Approve_Reject(Session["CompCode"].ToString(), ObjBO, ref HR_Email, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);


                                            leaverequestbo objBo = new leaverequestbo();
                                            leaverequestbl objBl = new leaverequestbl();
                                            leaverequestcollectionbo objLst = new leaverequestcollectionbo();

                                            List<leaverequestbo> objList = new List<leaverequestbo>();
                                            //objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved", "PA2002");


                                            //SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim(), "PA2002");

                                            if (HF_STS.Value.ToString().Trim().ToUpper() == "DELETION REQUESTED")
                                            {
                                                objList = objBl.Deletion_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Deletion request approved", "PA2002");

                                                SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request approved", "PA2002");

                                            }
                                            else
                                            {
                                                objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved", "PA2002");


                                                SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Approved", "Approved", "PA2002");

                                            }



                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID = null;
                                            //HF_PKEY = null;
                                            //HF_STS = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Attendance Request approved successfully !')", true);
                                        }
                                        //    }
                                        //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                        //}
                                        break;
                                    case "REJECT": // Flag - 2
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;


                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Rejected";
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            ObjBO.TableTyp = "PA2002";
                                            ObjBO.PERNR = ViewState["LACRTDBY"].ToString().Trim();
                                            ObjBL.Mngr_Leave_Req_Approve_Reject(Session["CompCode"].ToString(), ObjBO, ref HR_Email, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);

                                            leaverequestbo objBo = new leaverequestbo();
                                            leaverequestbl objBl = new leaverequestbl();
                                            leaverequestcollectionbo objLst = new leaverequestcollectionbo();

                                            List<leaverequestbo> objList = new List<leaverequestbo>();
                                            //objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected", "PA2002");


                                            //SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim(), "PA2002");


                                            if (HF_STS.Value.ToString().Trim().ToUpper() == "DELETION REQUESTED")
                                            {
                                                objList = objBl.Deletion_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Deletion request rejected", "PA2002");

                                                SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request rejected", "PA2002");

                                            }
                                            else
                                            {
                                                objList = objBl.Approval_LeaveDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected", "PA2002");


                                                SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", "Rejected", "PA2002");

                                            }


                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID = null;
                                            //HF_PKEY = null;
                                            //HF_STS = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Attendance Request rejected successfully !')", true);
                                        }
                                        //    }
                                        //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                        //}
                                        break;
                                    default:
                                        break;
                                }

                                break;






                            //-------------Address------------------------------------
                            case "PA0006":

                                switch (CaseType)
                                {
                                    case "APPROVE": // Flag - 1
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Approved";
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            //ObjBO.TableTyp = "PA0006";
                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["LASTACTIVITYDATE"].ToString());//DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MMODON"].ToString());




                                            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                                            piaddressinformationbl objPIAddBl = new piaddressinformationbl();

                                            List<piaddressinformationbo> objList = new List<piaddressinformationbo>();
                                            objList = objPIAddBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                            ObjBL.Approval_AddressDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailAddress(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", "Approved");
                                            //LoadGridDetails();
                                            // GetHRPernr();
                                            // ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID = null;
                                            //HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Address Information approved successfully !')", true);
                                            //    }
                                            //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                            //}
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;


                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Rejected";
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            //ObjBO.TableTyp = "PA0006";

                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            //ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            //ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["LASTACTIVITYDATE"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MMODON"].ToString());


                                            piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                                            piaddressinformationbl objPIAddBl = new piaddressinformationbl();
                                            List<piaddressinformationbo> objList = new List<piaddressinformationbo>();
                                            objList = objPIAddBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");

                                            ObjBL.Approval_AddressDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailAddress(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", "Rejected");

                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID = null;
                                            //HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Address Information rejected successfully !')", true);
                                            //    }
                                            //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                            //}
                                        }
                                        break;
                                    default:
                                        break;
                                }

                                break;





                            case "PA0105":

                                switch (CaseType)
                                {
                                    case "APPROVE": // Flag - 1
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Approved";
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            //ObjBO.TableTyp = "PA0006";

                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            //ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            //ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["LASTACTIVITYDATE"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MMODON"].ToString());



                                            picommunicationinformationbo objPIComBo = new picommunicationinformationbo();
                                            picommunicationinformationbl objPIComBl = new picommunicationinformationbl();
                                            List<picommunicationinformationbo> objList = new List<picommunicationinformationbo>();
                                            objList = objPIComBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                            ObjBL.Approval_CommunticationInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailCommunication(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", "Approved");

                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID.Value = null;
                                            //HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Information approved successfully !')", true);
                                            //    }
                                            //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                            //}
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;


                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Rejected";
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            //ObjBO.TableTyp = "PA0006";

                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            //ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            //ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["LASTACTIVITYDATE"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MMODON"].ToString());



                                            picommunicationinformationbo objPIComBo = new picommunicationinformationbo();
                                            picommunicationinformationbl objPIComBl = new picommunicationinformationbl();
                                            List<picommunicationinformationbo> objList = new List<picommunicationinformationbo>();
                                            objList = objPIComBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");


                                            ObjBL.Approval_CommunticationInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailCommunication(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", "Rejected");

                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID.Value = null;
                                            //HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Information rejected successfully !')", true);
                                            //    }
                                            //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                            //}
                                        }
                                        break;
                                    default:
                                        break;
                                }

                                break;



                            case "PA0002":

                                switch (CaseType)
                                {
                                    case "APPROVE": // Flag - 1
                                        
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Approved";
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            //ObjBO.TableTyp = "PA0006";

                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            //ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            //ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["LASTACTIVITYDATE"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MMODON"].ToString());



                                            personaldatabo objPIBo = new personaldatabo();
                                            personaldatabl objPIBl = new personaldatabl();
                                            List<personaldatabo> objList = new List<personaldatabo>();
                                            objList = objPIBl.Approval_PDDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");


                                            ObjBL.Approval_PDInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);

                                            SendMailPD(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", "Approved");





                                            //sendmail();
                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID.Value = null;
                                            //HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal Data Information approved successfully !')", true);
                                            //    }
                                            //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                            //}
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;


                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Rejected";
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            //ObjBO.TableTyp = "PA0006";

                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            //ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            //ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["LASTACTIVITYDATE"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MMODON"].ToString());



                                            personaldatabo objPIBo = new personaldatabo();
                                            personaldatabl objPIBl = new personaldatabl();
                                            List<personaldatabo> objList = new List<personaldatabo>();
                                            objList = objPIBl.Approval_PDDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");

                                            ObjBL.Approval_PDInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailPD(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", "Rejected");

                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID.Value = null;
                                            //HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal Data Information rejected successfully !')", true);
                                            //    }
                                            //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                            //}
                                        }
                                        break;
                                    default:
                                        break;
                                }

                                break;

                            case "PA0185":

                                switch (CaseType)
                                {
                                    case "APPROVE": // Flag - 1
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Approved";
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            //ObjBO.TableTyp = "PA0006";
                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();
                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            //ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            //ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["LASTACTIVITYDATE"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MMODON"].ToString());


                                            pipersonalidsbo objPIBo = new pipersonalidsbo();
                                            pipersonalidsbl objPIBl = new pipersonalidsbl();
                                            List<pipersonalidsbo> objList = new List<pipersonalidsbo>();
                                            objList = objPIBl.Approval_PIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                            ObjBL.Approval_PIDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailPIDS(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", "Approved");

                                            // sendmail();
                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID.Value = null;
                                            //HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal ID approved successfully !')", true);
                                            //    }
                                            //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                            //}
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Rejected";
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            //ObjBO.TableTyp = "PA0006";
                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();
                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            //ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            //ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["LASTACTIVITYDATE"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MMODON"].ToString());


                                            pipersonalidsbo objPIBo = new pipersonalidsbo();
                                            pipersonalidsbl objPIBl = new pipersonalidsbl();
                                            List<pipersonalidsbo> objList = new List<pipersonalidsbo>();
                                            objList = objPIBl.Approval_PIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");

                                            ObjBL.Approval_PIDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailPIDS(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", "Rejected");

                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID.Value = null;
                                            //HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal ID rejected successfully !')", true);
                                            //    }
                                            //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                            //}
                                        }
                                        break;
                                    default:
                                        break;
                                }

                                break;

                            case "PA0021":

                                switch (CaseType)
                                {
                                    case "APPROVE": // Flag - 1
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Approved";
                                            ObjBO.Flag = 1; // Flag 1 ----> APPROVE
                                            //ObjBO.TableTyp = "PA0006";
                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();
                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            //ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            //ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["LASTACTIVITYDATE"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MMODON"].ToString());


                                            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                            pifamilymembersbl objPIFamBl = new pifamilymembersbl();
                                            List<pifamilymembersbo> objList = new List<pifamilymembersbo>();
                                            objList = objPIFamBl.Approval_FMDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                            ObjBL.Approval_FamilykDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailFamily(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", "Approved");

                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            //ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID.Value = null;
                                            //HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family Details approved successfully !')", true);
                                            //    }
                                            //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                            //}
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        //using (TextBox TxtGvRemarks = (TextBox)GV_DashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                        //{
                                        //    if (TxtGvRemarks != null)
                                        //    {
                                        {
                                            msassignedtomebl ObjBL = new msassignedtomebl();
                                            msassignedtomebo ObjBO = new msassignedtomebo();
                                            string HR_Email = string.Empty;
                                            string Supervisor_name = string.Empty;
                                            string Supervisor_Email = string.Empty;
                                            string PERNR_Name = string.Empty;
                                            string PERNR_Email = string.Empty;

                                            ObjBO.ID = int.Parse(HF_ID.Value.ToString().Trim());
                                            ObjBO.PKEY = HF_PKEY.Value.ToString().Trim();
                                            ObjBO.APPROVED_BY = User.Identity.Name;
                                            ObjBO.Approver_Comment = "Rejected";
                                            ObjBO.Flag = 2; // Flag 2 ----> REJECT
                                            //ObjBO.TableTyp = "PA0006";
                                            //  ViewState["MODON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();
                                            //ViewState["MODIFDON"] = GridViewDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                                            //ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                            //ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                            ObjBO.MODIFIEDON = DateTime.Parse(ViewState["LASTACTIVITYDATE"].ToString());
                                            ObjBO.MODON = DateTime.Parse(ViewState["MMODON"].ToString());

                                            pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                            pifamilymembersbl objPIFamBl = new pifamilymembersbl();
                                            List<pifamilymembersbo> objList = new List<pifamilymembersbo>();
                                            objList = objPIFamBl.Approval_FMDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");

                                            ObjBL.Approval_FamilykDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                            SendMailFamily(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", "Rejected");

                                            //LoadGridDetails();
                                            //GetHRPernr();
                                            //ViewState["PendingPageIndex"] = "0";
                                            // ViewState["Req_ID"] = ViewState["Req_PERNR"] = null;
                                            //HF_TBLTYPE = null;
                                            //HF_ID.Value = null;
                                            //HF_PKEY = null;
                                            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                                            GV_DashboardCompleatedDetails.DataBind();
                                            GV_DashboardDetails.DataBind();
                                            GridViewDetails.DataBind();
                                            grdRecordTime.DataBind();
                                            RWTdiv.Visible = false;
                                            txtRWComments.Text = string.Empty;
                                            lblValidateRWCommnets.Text = string.Empty;
                                            TblRemarks.Visible = false;
                                            lblRemarksRWT.Text = string.Empty;
                                            lblRemarksRWT.Visible = false;
                                            GV_DashboardDetails.DataSource = null;
                                            GV_DashboardDetails.DataBind();
                                            GV_DashboardDetails.Visible = false;
                                            GridViewDetails.DataSource = null;
                                            GridViewDetails.DataBind();
                                            GridViewDetails.Visible = false;
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family Details rejected successfully !')", true);
                                            //    }
                                            //    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                            //}
                                        }
                                        break;
                                    default:
                                        break;
                                }

                                break;


                            default:
                                break;
                        }
                        //  }

                    }
                }
            }


        }
        catch (Exception Ex)
        {
            switch (Ex.Message)
            {
                case "-0":
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Cannot approve this leave request !');", true);
                    break;
                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true);
                    break;
            }
        }

    }


    protected void Mass_Approve_Reject_RecordWorkingDetails(bool bIStatus)
    {
        foreach (GridViewRow gvrow in grdPending.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkEmp");
            if (chk != null & chk.Checked)
            {
                string sName = grdPending.DataKeys[gvrow.RowIndex].Values["ENAME"].ToString();
                //string sEmailId = grdPending.DataKeys[grdRow.RowIndex]["USRID"].ToString();
                string sPernr = grdPending.DataKeys[gvrow.RowIndex].Values["PERNR"].ToString();
                //  string sPkey = grdRow.Cells[2].Text;
                string sPkey = grdPending.DataKeys[gvrow.RowIndex].Values["PKEY"].ToString();
                string sApprovalType = grdPending.DataKeys[gvrow.RowIndex].Values["CHANGE_APPROVAL"].ToString();
                DateTime dtLateDate = DateTime.Parse(grdPending.DataKeys[gvrow.RowIndex].Values["LAST_ACTIVITY_DATE"].ToString());
                // string sRole = grdRow.Cells[7].Text;
                string sRole = grdPending.DataKeys[gvrow.RowIndex].Values["PLSXT"].ToString();
                int id = int.Parse(grdPending.DataKeys[gvrow.RowIndex].Values["ID"].ToString());
                string TblTyp = grdPending.DataKeys[gvrow.RowIndex].Values["TableTyp"].ToString();
                HF_STS.Value = grdPending.DataKeys[gvrow.RowIndex].Values["REVIEW"].ToString();

                HF_TBLTYPE.Value = TblTyp.ToString().Trim();
                HF_ID.Value = id.ToString().Trim();
                HF_PKEY.Value = sPkey.ToString().Trim();
                ViewState["LACRTDBY"] = grdPending.DataKeys[gvrow.RowIndex].Values["PERNR"].ToString().Trim();



                msassignedtomebo objBo = new msassignedtomebo();
                string sIOPkey = sPkey;
                //string sRole = sRole;
                objBo.APPROVED_BY = User.Identity.Name;
                objBo.APPROVED_ON = DateTime.Now;
                objBo.COMMENTS = bIStatus == true ? "Approved" : "Rejected";
                objBo.STATUS = bIStatus;
                objBo.PKEY = sIOPkey;
                objBo.PLSXT = sRole;
                msassignedtomebl objPIAddBl = new msassignedtomebl();
                try
                {
                    int iResult = objPIAddBl.Approval_RecorWorkingDetails(Session["CompCode"].ToString(), objBo, ref strApprover, ref strApprover_mail, ref strRequesterMail);


                    if (iResult == 0)
                    {
                        SendMailRWT(objBo.PKEY.ToString().Trim(), objBo.STATUS, objBo.COMMENTS, objBo.APPROVED_ON);

                    }
                }
                catch (Exception Ex)
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
            }
        }

    }

    protected void grdPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("LBL_empid");
                string ccode = Session["CompCode"].ToString();
                string emplogin = lbl.Text;
                int cnt = ccode.Length;
                emplogin = emplogin.Substring(cnt);
                e.Row.Cells[1].Text = emplogin.Trim().ToUpper();


            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdCompleted_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
         if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("LBL_empid");
                string ccode = Session["CompCode"].ToString();
                string emplogin = lbl.Text;
                int cnt = ccode.Length;
                emplogin = emplogin.Substring(cnt);
                e.Row.Cells[1].Text = emplogin.Trim().ToUpper();


            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridViewDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("LBL_empid");
                string ccode = Session["CompCode"].ToString();
                string emplogin = lbl.Text;
                int cnt = ccode.Length;
                emplogin = emplogin.Substring(cnt);
                e.Row.Cells[0].Text = emplogin.Trim().ToUpper();


            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void tcAssignToMe_ActiveTabChanged()//object sender, EventArgs e
    {
        AllPnelStatus();
        if (HF_TABID.Value == "1")
        {
            //LoadGridDetails();

            HRTabSel.Visible = true;

            LoadGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());



            //GetHRPernr();
            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;

        }
        else
        {

            HRTabSel.Visible = true;

            LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());

            //LoadCompletedGridDetails();
            GridViewDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;
        }
    }

    protected void Tab1_Click(object sender, EventArgs e)
    {
        HF_TABID.Value = "1";
        divView2.Visible = false;
        divView1.Visible = true;
        tcAssignToMe_ActiveTabChanged();
        Tab1.CssClass = "nav-link active p-2";
        Tab2.CssClass = "nav-link p-2";
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        HF_TABID.Value = "2";
        divView2.Visible = true;
        divView1.Visible = false;
        tcAssignToMe_ActiveTabChanged();
        Tab2.CssClass = "nav-link active p-2";
        Tab1.CssClass = "nav-link p-2";
        LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());
        grdCompleted.Visible = true;
       
    }
}
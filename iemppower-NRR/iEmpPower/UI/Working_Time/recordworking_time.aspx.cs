using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Resources;
using System.Security.Permissions;
using System.Text;
using System.IO;
using System.Net;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Ticketing_Tool;
using System.Globalization;
using iEmpPower.Old_App_Code.iEmpPowerMaster;
using System.Web.Services;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute;
using iEmpPower.Old_App_Code.iEmpPowerBL.SPaycompute;

public partial class UI_Working_Time_record_working_time : System.Web.UI.Page
{
    string sPrevBtnCntrl = string.Empty;
    string sAttendencetype = "";
    string sBillableStatus = "";
    string sDeliverytype = "";
    string scountry = "";
    string sstaffgrade = "";
    string sTotal = "";
    string sCount = "";
    string sWorkingDate = "";
    string sLeaveHours = "";
    bool bStatus = true;
    string sCostCenter = "";
    string sOrder = "";
    //string sNetwork = "";
    string sActivity = "";
    string sWbs = "";
    string sRemarks = "";
    string sTicketID = "";
    string sPlndhrs = "";
    DateTime DtMillanium = new DateTime(2000, 01, 01);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated == false)
        {
            Server.Transfer("~/Account/Login.aspx");
        }
        //else //if (Session["PERSK"].ToString().Trim() == "" || Session["PERSK"] == null)
        //{
        //    Server.Transfer("~/Account/Login.aspx");
        //}
        this.Page.Form.DefaultButton = btnEntryKey.UniqueID;

        lblMessageBoard.Text = "";
        LblPopUp.Text = "";
        if (!IsPostBack)
        {
            // hdCCode.Value = Session["CompCode"].ToString();

            //mp1.Hide();
            pnlpopUp.Visible = false;
            divcal.Visible = true;
            getWorkingHours();
            SetInitialRow();

            //Fetching the current week from current date
            DateTime dtSelectedDate = DateTime.Now;
            DateTime dtStartDate, dtEndDate;

            GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.Text = dtStartDate.ToString("dd/MM/yyyy");
            bdpTo.Text = dtEndDate.ToString("dd/MM/yyyy");
            ControlStatus();
            Session.Add("sStatus", null);

        }

        LoadRecordWorkingToCalendars(1);
        Calendar1.SelectionChanged += new EventHandler(Calendar1_SelectionChanged);
        Calendar2.SelectionChanged += new EventHandler(Calendar2_SelectionChanged);
        Calendar3.SelectionChanged += new EventHandler(Calendar3_SelectionChanged);

    }


    public void getWorkingHours()
    {
        try
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            wtrecordworkingtimebl objBl = new wtrecordworkingtimebl();


            objBo.PERNR = User.Identity.Name.ToString().Trim();
            wtrecordworkingtimecollectionbo objLst = objBl.Loak_WorkingHours(objBo);
            if (objLst.Count > 0)
            {
                foreach (wtrecordworkingtimebo objBo1 in objLst)
                {
                    if (string.IsNullOrEmpty(objBo1.ARBST))
                    {
                        pnlpopUp.Visible = false;
                        DivBtns.Visible = false;
                        lblMessageBoard.Visible = true;
                        lblMessageBoard.Text = "Working hours is not maintained in iEmpPower!";
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;

                        LblPopUp.Visible = true;
                        LblPopUp.Text = "Working hours is not maintained in iEmpPower!";
                        LblPopUp.ForeColor = System.Drawing.Color.Red;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Working hours is not maintained in iEmpPower!');", true);
                        return;
                    }

                    else
                    {
                        lblMessageBoard.Visible = true;
                        lblMessageBoard.Text = "";
                        lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;

                        LblPopUp.Visible = true;
                        LblPopUp.Text = "";
                        LblPopUp.ForeColor = System.Drawing.Color.Transparent;
                        //UPRecordTime.Visible = true;
                        DivBtns.Visible = true;
                        ViewState["WorkingHours"] = objBo1.ARBST;
                    }
                }
            }
            else
            {
                //UPRecordTime.Visible = false;
                DivBtns.Visible = false;
                lblMessageBoard.Visible = true;
                lblMessageBoard.Text = "Working hours is not maintained in iEmpPower!";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;

                LblPopUp.Visible = true;
                LblPopUp.Text = "Working hours is not maintained in iEmpPower!";
                LblPopUp.ForeColor = System.Drawing.Color.Red;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Working hours is not maintained in iEmpPower!');", true);
                return;
            }
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
    }
    //This method will display the next month calendar
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            Calendar1.VisibleDate = Calendar2.VisibleDate;
            Calendar2.VisibleDate = Calendar3.VisibleDate;
            Calendar3.VisibleDate = Calendar3.VisibleDate.AddMonths(1);
            LoadRecordWorkingToCalendars(1);
            SetInitialRow();
            DateTime dtSelectedDate = DateTime.Now;
            DateTime dtStartDate, dtEndDate;

            GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.Text = dtStartDate.ToString("dd/MM/yyyy");
            bdpTo.Text = dtEndDate.ToString("dd/MM/yyyy");
            GV_SelectedDateLeaveView.DataSource = null;
            GV_SelectedDateLeaveView.DataBind();
        }
        catch (Exception Ex)
        {
            switch (Ex.Message.ToString().Trim())
            {
                case "Object reference not set to an instance of an object.":

                    //mp1.Hide();
                    pnlpopUp.Visible = false;
                    divcal.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Master data is missing from iEmpPower')", true);
                    return;
                    break;

                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                    break;
            }


        }
    }
    //This method will display the previous month calendar
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        try
        {
            Calendar3.VisibleDate = Calendar2.VisibleDate;
            Calendar2.VisibleDate = Calendar1.VisibleDate;
            Calendar1.VisibleDate = Calendar1.VisibleDate.AddMonths(-1);
            LoadRecordWorkingToCalendars(1);
            SetInitialRow();
            DateTime dtSelectedDate = DateTime.Now;
            DateTime dtStartDate, dtEndDate;

            GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.Text = dtStartDate.ToString("dd/MM/yyyy");
            bdpTo.Text = dtEndDate.ToString("dd/MM/yyyy");
            GV_SelectedDateLeaveView.DataSource = null;
            GV_SelectedDateLeaveView.DataBind();
        }
        catch (Exception Ex)
        {
            switch (Ex.Message.ToString().Trim())
            {
                case "Object reference not set to an instance of an object.":

                    //mp1.Hide();
                    pnlpopUp.Visible = false;
                    divcal.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Master data is missing from iEmpPower')", true);
                    return;
                    break;

                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                    break;
            }


        }
    }
    //This method will display the dates in gridview from selected week 
    public void datesetting()
    {
        grdRecordTime.HeaderRow.Cells[6].Text = "SUN ," + DateTime.Parse(bdpFrom.Text).AddDays(0).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[7].Text = "MON ," + DateTime.Parse(bdpFrom.Text).AddDays(1).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[8].Text = "TUE ," + DateTime.Parse(bdpFrom.Text).AddDays(2).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[9].Text = "WED ," + DateTime.Parse(bdpFrom.Text).AddDays(3).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[10].Text = "THU ," + DateTime.Parse(bdpFrom.Text).AddDays(4).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[11].Text = "FRI ," + DateTime.Parse(bdpFrom.Text).AddDays(5).ToString("d-MMM-yyyy");
        grdRecordTime.HeaderRow.Cells[12].Text = "SAT ," + DateTime.Parse(bdpFrom.Text).AddDays(6).ToString("d-MMM-yyyy");
    }

    private void SetInitialRow()
    {
        //Create table
        DataTable dt = CreateTable();
        DataRow dr = null;
        dr = dt.NewRow();
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference 

        ViewState["CurrentTable"] = dt;

        //Bind the Gridview 
        grdRecordTime.DataSource = dt;
        grdRecordTime.DataBind();

        //After binding the gridview, we can then extract and fill the DropDownList with Data 

        DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[0].FindControl("drpdwnAttabsType");
        LoadAttabsTypes(ddl1);
        DropDownList ddl3 = (DropDownList)grdRecordTime.Rows[0].FindControl("drpdwnOrder");
        LoadOrder(ddl3);

        DropDownList ddlWbs = (DropDownList)grdRecordTime.Rows[0].FindControl("drpdwnWbs");
        LoadWbs(ddlWbs, ddl3.SelectedValue.ToString());

        DropDownList drpdwnActtype = (DropDownList)grdRecordTime.Rows[0].FindControl("drpdwnActtype");

        //LoadActtype(drpdwnActtype, Convert.ToInt32(ddlWbs.SelectedValue));


        //DropDownList ddlActtype = (DropDownList)grdRecordTime.Rows[0].FindControl("drpdwnActtype");
        //LoadActtype(ddlActtype, Convert.ToInt32(ddlWbs.SelectedValue));

        TextBox acty = (TextBox)grdRecordTime.Rows[0].FindControl("txtmainSearch");
        acty.Text = "";

        hiddenRowCount.Value = dt.Rows.Count.ToString();
    }

    protected void LoadCostCenter(DropDownList ddl)
    {
        mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_CostCenter();
        ddl.DataSource = objLst;
        ddl.DataTextField = "CC_TEXT";
        ddl.DataValueField = "KOSTL";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(" ", "0"));
        Session.Add("CostCenterLst", objLst);
    }

    protected void LoadOrder(DropDownList ddl)
    {
        string a = bdpFrom.Text == "" ? DateTime.Now.ToString() : bdpFrom.Text.Trim();
        string ddt = "";
        ddt = DateTime.Parse(a.ToString().Trim()).ToString("dd-MM-yyyy");
        mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Order(Session["CompCode"].ToString(), Convert.ToDateTime(ddt.ToString().Trim()), DateTime.MinValue);
        ddl.DataSource = objLst;
        ddl.DataTextField = "ORDER_TEXT";
        ddl.DataValueField = "pspnr";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        Session.Add("OrderLst", objLst);
    }

    protected void drpdwnOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblMessageBoard.Text = "";
        try
        {
            if (grdRecordTime.Rows.Count > 0)
            {
                GridViewRow row1 = (sender as DropDownList).Parent.Parent as GridViewRow;

                int a = row1.RowIndex;

                foreach (GridViewRow row in grdRecordTime.Rows)
                {
                    if (row.RowIndex == a)
                    {
                        DropDownList drpdwnOrder = (DropDownList)row.FindControl("drpdwnOrder");
                        DropDownList drpdwnWbs = (DropDownList)row.FindControl("drpdwnWbs");
                        drpdwnWbs.Items.Clear();
                        LoadWbs(drpdwnWbs, drpdwnOrder.SelectedValue.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
        }
    }

    protected void LoadWbs(DropDownList ddl, string project)
    {
        mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Wbs(Session["CompCode"].ToString(), project);
        ddl.DataSource = objLst;
        ddl.DataTextField = "POST1";
        ddl.DataValueField = "pspnr";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        Session.Add("WbsLst", objLst);
    }


    protected void LoadActtype(DropDownList ddl, int wbs)
    {
        mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Acty(Session["CompCode"].ToString(), wbs);
        ddl.DataSource = objLst;
        ddl.DataTextField = "POST1";
        ddl.DataValueField = "pspnr";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

    }

    protected void LoadAttabsTypes(DropDownList ddl)
    {
        mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Attendence_abs_Types(Session["CompCode"].ToString());

        ddl.DataSource = objLst;
        ddl.DataTextField = "ATEXT";
        ddl.DataValueField = "pspnr";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        Session.Add("objDropDownLst", objLst);
    }

    private bool ValidatePreviousRows()
    {
        for (int i = 0; i < grdRecordTime.Rows.Count; i++)
        {
            DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[i].Cells[1].FindControl("drpdwnAttabsType");

            if (ddl1.SelectedIndex == 0)
            {
                ddl1.Focus();
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Select attendance type.";

                LblPopUp.Visible = true;
                LblPopUp.ForeColor = System.Drawing.Color.Red;
                LblPopUp.Text = "Select attendance type.";


                return false;
            }
        }
        return true;
    }
    //Adding the new row to grid view
    private void AddNewRowToGrid()
    {

        if (ViewState["CurrentTable"] != null)
        {
            if (ValidatePreviousRows())
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();

                    //add new row to DataTable 
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState for future reference 

                    ViewState["CurrentTable"] = dtCurrentTable;


                    for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                    {
                        //extract the TextBox values 

                        //TextBox boxBillable = (TextBox)grdRecordTime.Rows[i].Cells[2].FindControl("txtBillable");
                        //TextBox boxDeliveryType = (TextBox)grdRecordTime.Rows[i].Cells[3].FindControl("txtDeliveryType");
                        //Label lblCostCenter = (Label)grdRecordTime.Rows[i].FindControl("lblCostCenter");
                        //Label lblOrder = (Label)grdRecordTime.Rows[i].FindControl("lblOrder");
                        TextBox boxStaffGrade = (TextBox)grdRecordTime.Rows[i].FindControl("txtStaffGrade");
                        TextBox boxTotal = (TextBox)grdRecordTime.Rows[i].FindControl("txtTotal");
                        TextBox boxSUN = (TextBox)grdRecordTime.Rows[i].FindControl("txtSUN");
                        TextBox boxMON = (TextBox)grdRecordTime.Rows[i].FindControl("txtMON");
                        TextBox boxTUE = (TextBox)grdRecordTime.Rows[i].FindControl("txtTUE");
                        TextBox boxWED = (TextBox)grdRecordTime.Rows[i].FindControl("txtWED");
                        TextBox boxTHU = (TextBox)grdRecordTime.Rows[i].FindControl("txtTHU");
                        TextBox boxFRI = (TextBox)grdRecordTime.Rows[i].FindControl("txtFRI");
                        TextBox boxSAT = (TextBox)grdRecordTime.Rows[i].FindControl("txtSAT");
                        TextBox boxRemarks = (TextBox)grdRecordTime.Rows[i].FindControl("txtREMARKS");

                        //dtCurrentTable.Rows[i]["Column2"] = boxBillable.Text;
                        //dtCurrentTable.Rows[i]["Column14"] = lblOrder.Text;
                        //dtCurrentTable.Rows[i]["Column13"] = lblCostCenter.Text;
                        dtCurrentTable.Rows[i]["Staff"] = boxStaffGrade.Text;
                        dtCurrentTable.Rows[i]["Total"] = boxTotal.Text;
                        dtCurrentTable.Rows[i]["Sun"] = boxSUN.Text;
                        dtCurrentTable.Rows[i]["Mon"] = boxMON.Text;
                        dtCurrentTable.Rows[i]["Tue"] = boxTUE.Text;
                        dtCurrentTable.Rows[i]["Wed"] = boxWED.Text;
                        dtCurrentTable.Rows[i]["Thur"] = boxTHU.Text;
                        dtCurrentTable.Rows[i]["Fri"] = boxFRI.Text;
                        dtCurrentTable.Rows[i]["Sat"] = boxSAT.Text;
                        dtCurrentTable.Rows[i]["Remarks"] = boxRemarks.Text;

                        //FOR DELETING COST CENTER
                        //DropDownList ddlCostCenter = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnCostCenter");


                        dtCurrentTable.Rows[i]["CostCenter"] = "0";

                        //extract the DropDownList Selected Items 
                        DropDownList ddlOrder = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnOrder");

                        dtCurrentTable.Rows[i]["Order"] = ddlOrder.SelectedValue.ToString();
                        //extract the DropDownList Selected Items 
                        DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnAttabsType");

                        // Update the DataRow with the DDL Selected Items 
                        dtCurrentTable.Rows[i]["AttTypes"] = ddl1.SelectedValue.ToString();


                        //DropDownList ddlNetwork = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnNetwork");
                        //dtCurrentTable.Rows[i]["Network"] = ddlNetwork.SelectedValue.ToString();

                        //DropDownList ddlActtype = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnActtype");
                        TextBox txtActtype = (TextBox)grdRecordTime.Rows[i].FindControl("txtmainSearch");
                        dtCurrentTable.Rows[i]["Acttype"] = txtActtype.Text.ToString().Trim();


                        DropDownList ddlWbs = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnWbs");
                        dtCurrentTable.Rows[i]["Wbs"] = ddlWbs.SelectedValue.ToString();

                        //DropDownList drpdowntickets = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdowntickets");
                        //dtCurrentTable.Rows[i]["TicketID"] = drpdowntickets.SelectedValue.ToString();

                        //TextBox ticketsPlndhrs = (TextBox)grdRecordTime.Rows[i].FindControl("ticketsPlndhrs");
                        //dtCurrentTable.Rows[i]["Plndhrs"] = ticketsPlndhrs.Text.ToString();

                    }

                    //Rebind the Grid with the current data to reflect changes 
                    grdRecordTime.DataSource = dtCurrentTable;
                    grdRecordTime.DataBind();
                    SetPreviousData();
                }
            }

            //Set Previous Data on Postbacks 

        }
        else
        {
            Response.Write("");
        }
    }

    private void SetPreviousDatadeleting()
    {
        mastercollectionbo objLst = (mastercollectionbo)Session["CostCenterLst"];
        decimal sSun = 0;
        decimal sMon = 0;
        decimal sTue = 0;
        decimal sWed = 0;
        decimal sThu = 0;
        decimal sFri = 0;
        decimal sSat = 0;
        decimal sTotalActualHrs = 0;

        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    decimal iRowTotalHrs = 0;
                    //TextBox boxBillable = (TextBox)grdRecordTime.Rows[i].Cells[2].FindControl("txtBillable");
                    Label lblOrder = (Label)grdRecordTime.Rows[i].FindControl("lblOrder");
                    Label lblCostCenter = (Label)grdRecordTime.Rows[i].FindControl("lblCostCenter");
                    TextBox boxStaffGrade = (TextBox)grdRecordTime.Rows[i].FindControl("txtStaffGrade");
                    TextBox boxTotal = (TextBox)grdRecordTime.Rows[i].FindControl("txtTotal");
                    TextBox boxSUN = (TextBox)grdRecordTime.Rows[i].FindControl("txtSUN");
                    TextBox boxMON = (TextBox)grdRecordTime.Rows[i].FindControl("txtMON");
                    TextBox boxTUE = (TextBox)grdRecordTime.Rows[i].FindControl("txtTUE");
                    TextBox boxWED = (TextBox)grdRecordTime.Rows[i].FindControl("txtWED");
                    TextBox boxTHU = (TextBox)grdRecordTime.Rows[i].FindControl("txtTHU");
                    TextBox boxFRI = (TextBox)grdRecordTime.Rows[i].FindControl("txtFRI");
                    TextBox boxSAT = (TextBox)grdRecordTime.Rows[i].FindControl("txtSAT");
                    TextBox boxRemarks = (TextBox)grdRecordTime.Rows[i].FindControl("txtREMARKS");

                    Label lblHours = ((Label)grdRecordTime.FooterRow.FindControl("lblHours"));
                    Label lblSun = ((Label)grdRecordTime.FooterRow.FindControl("lblSun"));
                    Label lblMon = ((Label)grdRecordTime.FooterRow.FindControl("lblMon"));
                    Label lblTues = ((Label)grdRecordTime.FooterRow.FindControl("lblTues"));
                    Label lblWed = ((Label)grdRecordTime.FooterRow.FindControl("lblWed"));
                    Label lblThu = ((Label)grdRecordTime.FooterRow.FindControl("lblThu"));
                    Label lblFri = ((Label)grdRecordTime.FooterRow.FindControl("lblFri"));
                    Label lblSAt = ((Label)grdRecordTime.FooterRow.FindControl("lblSAt"));
                    Label lblRemarks = ((Label)grdRecordTime.FooterRow.FindControl("lblRemarks"));


                    DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnAttabsType");

                    //Fill the DropDownList with Data 
                    ddl1.Controls.Clear();
                    LoadAttabsTypes(ddl1);

                    DropDownList ddlOrder = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnOrder");
                    ddlOrder.Controls.Clear();
                    LoadOrder(ddlOrder);


                    DropDownList ddlWbs = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnWbs");
                    ddlWbs.Controls.Clear();
                    LoadWbs(ddlWbs, ddlOrder.SelectedValue.ToString());



                    //DropDownList ddlActtype = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnActtype");
                    //ddlActtype.Controls.Clear();
                    //LoadActtype(ddlActtype, Convert.ToInt32(ddlWbs.SelectedValue));

                    TextBox acty = (TextBox)grdRecordTime.Rows[i].FindControl("txtmainSearch");
                    acty.Text = "";





                    if (i < dt.Rows.Count)
                    {

                        //Assign the value from DataTable to the TextBox 

                        boxStaffGrade.Text = dt.Rows[i]["Staff"].ToString();
                        boxTotal.Text = dt.Rows[i]["Total"].ToString();
                        boxSUN.Text = dt.Rows[i]["Sun"].ToString();
                        boxMON.Text = dt.Rows[i]["Mon"].ToString();
                        boxTUE.Text = dt.Rows[i]["Tue"].ToString();
                        boxWED.Text = dt.Rows[i]["Wed"].ToString();
                        boxTHU.Text = dt.Rows[i]["Thur"].ToString();
                        boxFRI.Text = dt.Rows[i]["Fri"].ToString();
                        boxSAT.Text = dt.Rows[i]["Sat"].ToString();
                        boxRemarks.Text = dt.Rows[i]["Remarks"].ToString();

                        //masterbo objBo = objLst.Find(delegate(masterbo obj) { return obj.KOSTL == ddlCostCenter.SelectedItem.Text; });
                        //lblCostCenter.Text = objBo.LTEXT;
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
                            iRowTotalHrs = decimal.Parse(boxMON.Text) + iRowTotalHrs;
                        }
                        if (boxTUE.Text.Trim() != "")
                        {
                            sTue = decimal.Parse(boxTUE.Text) + sTue;
                            lblTues.Text = sTue.ToString();
                            iRowTotalHrs = decimal.Parse(boxTUE.Text) + iRowTotalHrs;
                        }
                        if (boxWED.Text.Trim() != "")
                        {

                            sWed = decimal.Parse(boxWED.Text) + sWed;
                            lblWed.Text = sWed.ToString();
                            iRowTotalHrs = decimal.Parse(boxWED.Text) + iRowTotalHrs;
                        }
                        if (boxTHU.Text.Trim() != "")
                        {
                            sThu = decimal.Parse(boxTHU.Text) + sThu;
                            lblThu.Text = sThu.ToString();
                            iRowTotalHrs = decimal.Parse(boxTHU.Text) + iRowTotalHrs;
                        }
                        if (boxFRI.Text.Trim() != "")
                        {
                            sFri = decimal.Parse(boxFRI.Text) + sFri;
                            lblFri.Text = sFri.ToString();
                            iRowTotalHrs = decimal.Parse(boxFRI.Text) + iRowTotalHrs;
                        }
                        if (boxSAT.Text.Trim() != "")
                        {
                            sSat = decimal.Parse(boxSAT.Text) + sSat;
                            lblSAt.Text = sSat.ToString();
                            iRowTotalHrs = decimal.Parse(boxSAT.Text) + iRowTotalHrs;
                        }
                        sTotalActualHrs = iRowTotalHrs + sTotalActualHrs;
                        lblHours.Text = sTotalActualHrs.ToString();
                        boxTotal.Text = iRowTotalHrs.ToString();

                        //Set the Previous Selected Items on Each DropDownList  on Postbacks 
                        ddl1.ClearSelection();
                        if (dt.Rows[i]["AttTypes"].ToString() == null)
                        {
                            ddl1.Items.FindByText("Select").Selected = true;
                        }
                        else
                        {
                            ddl1.Items.FindByValue(dt.Rows[i]["AttTypes"].ToString()).Selected = true;
                        }

                        ////FOR DELETING COST CENTER
                        //ddlCostCenter.ClearSelection();
                        //if (dt.Rows[i]["CostCenter"].ToString() == null)
                        //{
                        //    ddlCostCenter.Items.FindByText("Select").Selected = true;
                        //}
                        //else
                        //{
                        //    ddlCostCenter.Items.FindByValue(dt.Rows[i]["CostCenter"].ToString()).Selected = true;
                        //}

                        ddlOrder.ClearSelection();
                        if (dt.Rows[i]["Order"].ToString() == null)
                        {
                            ddlOrder.Items.FindByText("Select").Selected = true;
                        }
                        else
                        {
                            ddlOrder.Items.FindByValue(dt.Rows[i]["Order"].ToString()).Selected = true;
                        }

                        ddlWbs.ClearSelection();
                        LoadWbs(ddlWbs, ddlOrder.SelectedValue.ToString());
                        if (dt.Rows[i]["Wbs"].ToString() == null)
                        {
                            ddlWbs.Items.FindByText("Select").Selected = true;
                        }
                        else
                        {
                            ddlWbs.Items.FindByValue(dt.Rows[i]["Wbs"].ToString()).Selected = true;
                        }


                        //ddlActtype.ClearSelection();
                        //DropDownList ddlWbs1 = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnWbs");
                        //LoadActtype(ddlActtype, Convert.ToInt32(ddlWbs1.SelectedValue));
                        TextBox txtActtype = (TextBox)grdRecordTime.Rows[i].FindControl("txtmainSearch");


                        if (dt.Rows[i]["Acttype"].ToString() == null)
                        {
                            txtActtype.Text = "";
                            //ddlActtype.Items.FindByText("Select").Selected = true;
                        }
                        else
                        {
                            txtActtype.Text = (dt.Rows[i]["Acttype"].ToString());
                            //ddlActtype.Items.FindByValue(dt.Rows[i]["Acttype"].ToString()).Selected = true;
                        }



                    }
                    rowIndex++;
                }
            }
        }
    }

    private void SetPreviousData()
    {
        mastercollectionbo objLst = (mastercollectionbo)Session["CostCenterLst"];
        decimal sSun = 0;
        decimal sMon = 0;
        decimal sTue = 0;
        decimal sWed = 0;
        decimal sThu = 0;
        decimal sFri = 0;
        decimal sSat = 0;
        decimal sTotalActualHrs = 0;

        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    decimal iRowTotalHrs = 0;
                    //TextBox boxBillable = (TextBox)grdRecordTime.Rows[i].Cells[2].FindControl("txtBillable");
                    Label lblOrder = (Label)grdRecordTime.Rows[i].FindControl("lblOrder");
                    Label lblCostCenter = (Label)grdRecordTime.Rows[i].FindControl("lblCostCenter");
                    TextBox boxStaffGrade = (TextBox)grdRecordTime.Rows[i].FindControl("txtStaffGrade");
                    TextBox boxTotal = (TextBox)grdRecordTime.Rows[i].FindControl("txtTotal");
                    TextBox boxSUN = (TextBox)grdRecordTime.Rows[i].FindControl("txtSUN");
                    TextBox boxMON = (TextBox)grdRecordTime.Rows[i].FindControl("txtMON");
                    TextBox boxTUE = (TextBox)grdRecordTime.Rows[i].FindControl("txtTUE");
                    TextBox boxWED = (TextBox)grdRecordTime.Rows[i].FindControl("txtWED");
                    TextBox boxTHU = (TextBox)grdRecordTime.Rows[i].FindControl("txtTHU");
                    TextBox boxFRI = (TextBox)grdRecordTime.Rows[i].FindControl("txtFRI");
                    TextBox boxSAT = (TextBox)grdRecordTime.Rows[i].FindControl("txtSAT");
                    TextBox boxRemarks = (TextBox)grdRecordTime.Rows[i].FindControl("txtREMARKS");

                    Label lblHours = ((Label)grdRecordTime.FooterRow.FindControl("lblHours"));
                    Label lblSun = ((Label)grdRecordTime.FooterRow.FindControl("lblSun"));
                    Label lblMon = ((Label)grdRecordTime.FooterRow.FindControl("lblMon"));
                    Label lblTues = ((Label)grdRecordTime.FooterRow.FindControl("lblTues"));
                    Label lblWed = ((Label)grdRecordTime.FooterRow.FindControl("lblWed"));
                    Label lblThu = ((Label)grdRecordTime.FooterRow.FindControl("lblThu"));
                    Label lblFri = ((Label)grdRecordTime.FooterRow.FindControl("lblFri"));
                    Label lblSAt = ((Label)grdRecordTime.FooterRow.FindControl("lblSAt"));
                    Label lblRemarks = ((Label)grdRecordTime.FooterRow.FindControl("lblRemarks"));

                    //TextBox ticketsPlndhrs = (TextBox)grdRecordTime.Rows[i].FindControl("ticketsPlndhrs");

                    DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnAttabsType");
                    ddl1.Controls.Clear();
                    LoadAttabsTypes(ddl1);


                    //extract the DropDownList Selected Items 
                    DropDownList ddlOrder = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnOrder");
                    ddlOrder.Controls.Clear();
                    LoadOrder(ddlOrder);


                    DropDownList ddlWbs = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnWbs");
                    ddlWbs.Controls.Clear();
                    LoadWbs(ddlWbs, ddlOrder.SelectedValue.ToString());


                    TextBox acty = (TextBox)grdRecordTime.Rows[i].FindControl("txtmainSearch");
                    acty.Text = "";


                    if (i < dt.Rows.Count - 1)
                    {

                        //Assign the value from DataTable to the TextBox 

                        boxStaffGrade.Text = dt.Rows[i]["Staff"].ToString();
                        boxTotal.Text = dt.Rows[i]["Total"].ToString();
                        boxSUN.Text = dt.Rows[i]["Sun"].ToString();
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
                            iRowTotalHrs = decimal.Parse(boxSUN.Text) + iRowTotalHrs;
                        }
                        if (boxMON.Text.Trim() != "")
                        {
                            sMon = decimal.Parse(boxMON.Text) + sMon;
                            lblMon.Text = sMon.ToString();
                            iRowTotalHrs = decimal.Parse(boxMON.Text) + iRowTotalHrs;
                        }
                        if (boxTUE.Text.Trim() != "")
                        {
                            sTue = decimal.Parse(boxTUE.Text) + sTue;
                            lblTues.Text = sTue.ToString();
                            iRowTotalHrs = decimal.Parse(boxTUE.Text) + iRowTotalHrs;
                        }
                        if (boxWED.Text.Trim() != "")
                        {

                            sWed = decimal.Parse(boxWED.Text) + sWed;
                            lblWed.Text = sWed.ToString();
                            iRowTotalHrs = decimal.Parse(boxWED.Text) + iRowTotalHrs;
                        }
                        if (boxTHU.Text.Trim() != "")
                        {
                            sThu = decimal.Parse(boxTHU.Text) + sThu;
                            lblThu.Text = sThu.ToString();
                            iRowTotalHrs = decimal.Parse(boxTHU.Text) + iRowTotalHrs;
                        }
                        if (boxFRI.Text.Trim() != "")
                        {
                            sFri = decimal.Parse(boxFRI.Text) + sFri;
                            lblFri.Text = sFri.ToString();
                            iRowTotalHrs = decimal.Parse(boxFRI.Text) + iRowTotalHrs;
                        }
                        if (boxSAT.Text.Trim() != "")
                        {
                            sSat = decimal.Parse(boxSAT.Text) + sSat;
                            lblSAt.Text = sSat.ToString();
                            iRowTotalHrs = decimal.Parse(boxSAT.Text) + iRowTotalHrs;
                        }
                        sTotalActualHrs = iRowTotalHrs + sTotalActualHrs;
                        lblHours.Text = sTotalActualHrs.ToString();
                        boxTotal.Text = iRowTotalHrs.ToString();

                        //Set the Previous Selected Items on Each DropDownList  on Postbacks 
                        ddl1.ClearSelection();
                        if (dt.Rows[i]["AttTypes"].ToString() == null)
                        {
                            ddl1.Items.FindByText("Select").Selected = true;
                        }
                        else
                        {
                            ddl1.Items.FindByValue(dt.Rows[i]["AttTypes"].ToString()).Selected = true;
                        }


                        ddlOrder.ClearSelection();
                        if (dt.Rows[i]["Order"].ToString() == null)
                        {
                            ddlOrder.Items.FindByText("Select").Selected = true;
                        }
                        else
                        {
                            ddlOrder.Items.FindByValue(dt.Rows[i]["Order"].ToString()).Selected = true;
                        }

                        ddlWbs.ClearSelection();
                        LoadWbs(ddlWbs, ddlOrder.SelectedValue.ToString());
                        if (dt.Rows[i]["Wbs"].ToString() == null)
                        {
                            ddlWbs.Items.FindByText("Select").Selected = true;
                        }
                        else
                        {
                            ddlWbs.Items.FindByValue(dt.Rows[i]["Wbs"].ToString()).Selected = true;
                        }


                        TextBox txtActtype = (TextBox)grdRecordTime.Rows[i].FindControl("txtmainSearch");


                        if (dt.Rows[i]["Acttype"].ToString() == null)
                        {
                            txtActtype.Text = "";
                            //ddlActtype.Items.FindByText("Select").Selected = true;
                        }
                        else
                        {
                            txtActtype.Text = (dt.Rows[i]["Acttype"].ToString());
                            //ddlActtype.Items.FindByValue(dt.Rows[i]["Acttype"].ToString()).Selected = true;
                        }

                        //LoadActtype(ddlActtype, Convert.ToInt32(ddlWbs2.SelectedValue));
                        if (dt.Rows[i]["TicketID"].ToString() == null)
                        {
                            //drpdowntickets.Items.FindByText("Select").Selected = true;
                        }
                        else
                        {
                            //drpdowntickets.Items.FindByValue(dt.Rows[i]["TicketID"].ToString()).Selected = true;
                        }

                    }
                    rowIndex++;
                }
            }
        }
    }

    protected void ControlStatus()
    {
        bool bIsSave = true;
        Session.Add("IsSave", bIsSave);
        bool bIsBck = true;
        Session.Add("bIsBck", bIsBck);
        btnCancel.Visible = true;
        btnPreviousStep.Enabled = false;
        btnPreviousStep.Visible = false;
        btnReview.Visible = true;
        btnSave.Visible = false;
        btnExit.Visible = false;
        grdRecordTime.Enabled = true;
        FormButtonStatus(true);
        datesetting();
        Calendar2.VisibleDate = DateTime.Today;
        Calendar1.VisibleDate = Calendar2.VisibleDate.AddMonths(-1);
        Calendar3.VisibleDate = Calendar2.VisibleDate.AddMonths(1);

    }
    //This method will give first day of the selected calendar
    private DateTime GetFirstInMonth(DateTime dt, out DateTime dtFromDate)
    {
        DateTime dtRet = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
        dtFromDate = dtRet;
        return dtRet;
    }
    //This method will give last day of the selected calendar
    private DateTime LastDayOfMonth(DateTime dtGivendate, out DateTime dtLastDat)
    {
        DateTime dtRet = new DateTime(dtGivendate.Year, dtGivendate.Month, 1).AddMonths(1).AddDays(-1);
        dtLastDat = dtRet;
        return dtRet;
    }
    //Here we get all 3 months dates from database
    protected void LoadRecordWorkingToCalendars(int Type)
    {
        DateTime dtFormGivenDate = Calendar1.VisibleDate.Date;
        DateTime dtToGivenDate = Calendar3.VisibleDate.Date;
        DateTime dtTodate, dtFromdate;

        GetFirstInMonth(dtFormGivenDate, out dtFromdate);
        LastDayOfMonth(dtToGivenDate, out dtTodate);
        wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
        wtrecordworkingtimebl objBl = new wtrecordworkingtimebl();
        wtrecordworkingtimecollectionbo objLst = new wtrecordworkingtimecollectionbo();
        objBo.COMMENTS = Session["CompCode"].ToString();
        objBo.PERNR = User.Identity.Name;
        objBo.FROM_DATE = dtFromdate;
        objBo.TO_DATE = dtTodate;
        objLst = objBl.Get_RecordDetails(objBo);
        Session.Add("objLst", objLst);

        leaverequestbo objleaveBo = new leaverequestbo();
        leaverequestbl objleaveBl = new leaverequestbl();
        leaverequestcollectionbo objleaveLst = new leaverequestcollectionbo();
        objleaveBo.PERNR = User.Identity.Name;
        objleaveBo.FROM_DATE = dtFromdate;
        objleaveBo.TO_DATE = dtTodate;
        objleaveBo.COMMENTS = Session["CompCode"].ToString();
        objleaveLst = objleaveBl.Get_Calendar_Leave_Markings_ForRWT(objleaveBo, Type);
        Session.Add("objleaveLst", objleaveLst);

        Calendar1.DayRender += new DayRenderEventHandler(Calendar1_DayRender);
        Calendar2.DayRender += new DayRenderEventHandler(Calendar2_DayRender);
        Calendar3.DayRender += new DayRenderEventHandler(Calendar3_DayRender);
    }
    //Here will assign saved dates to calendar 
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        DateTime dtGivenDate = Calendar1.VisibleDate.Date;
        DateTime dtTodate, dtFromdate;

        GetFirstInMonth(dtGivenDate, out dtFromdate);
        LastDayOfMonth(dtFromdate, out dtTodate);
        wtrecordworkingtimecollectionbo objLst = (wtrecordworkingtimecollectionbo)Session["objLst"];
        var vOnlyFirstCal = (from col in objLst
                             where DateTime.Parse(col.WORKING_DATE) >= dtFromdate &&
                             DateTime.Parse(col.WORKING_DATE) <= dtTodate
                             select col);
        foreach (wtrecordworkingtimebo objBo in vOnlyFirstCal)
        {
            if (e.Day.Date == DateTime.Parse(objBo.WORKING_DATE) && objBo.STS.ToString().Trim().ToUpper() == "REVIEW")
            {
                e.Cell.BackColor = System.Drawing.Color.CornflowerBlue;
                e.Cell.ForeColor = System.Drawing.Color.White;
            }
            else if (e.Day.Date == DateTime.Parse(objBo.WORKING_DATE) && objBo.STS.ToString().Trim().ToUpper() == "APPROVED")
            {
                e.Cell.BackColor = System.Drawing.Color.FromArgb(35, 186, 53);
                e.Cell.ForeColor = System.Drawing.Color.White;
            }
        }

        for (int i = 0; i < objLst.Count; i++)
        {
            wtrecordworkingtimebo objBoOuter = new wtrecordworkingtimebo();
            objBoOuter = objLst[i];

            for (int j = i + 1; j < objLst.Count; j++)
            {
                wtrecordworkingtimebo objBoInner = new wtrecordworkingtimebo();
                objBoInner = objLst[j];
                if (objBoOuter.WORKING_DATE == objBoInner.WORKING_DATE)
                {
                    if (e.Day.DayNumberText.ToString() == DateTime.Parse(objBoInner.WORKING_DATE.ToString()).Day.ToString())
                    {
                        if (Calendar1.VisibleDate.Month == DateTime.Parse(objBoInner.WORKING_DATE).Month)
                        {
                            if (!e.Day.IsOtherMonth)
                            {
                                // e.Cell.BackColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
        }
        leaverequestcollectionbo objLeaveLst = (leaverequestcollectionbo)Session["objleaveLst"];
        var vOnlyLeaveFirstCal = (from col in objLeaveLst
                                  where (col.DATUM) >= dtFromdate &&
                                  (col.DATUM) <= dtTodate
                                  select col);
        foreach (leaverequestbo objBo in vOnlyLeaveFirstCal)
        {
            string PERNR = objBo.PERNR;
            string ccode = Session["CompCode"].ToString();
            string emplogin = PERNR.ToString();
            int cnt = ccode.Length;
            emplogin = emplogin.Substring(cnt);
            string empid = emplogin.Trim();
            // if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() != "REJECTED")
            {
                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#f24343");
                e.Cell.ForeColor = System.Drawing.Color.White;
                TimeSpan Tm = e.Day.Date - DtMillanium;
                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);

            }
        }



        Session.Add("vOnlyFirstCal", vOnlyFirstCal);
    }
    //Here will assign saved dates to calendar
    protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
    {
        DateTime dtGivenDate = Calendar2.VisibleDate.Date;
        DateTime dtTodate, dtFromdate;

        GetFirstInMonth(dtGivenDate, out dtFromdate);
        LastDayOfMonth(dtFromdate, out dtTodate);
        wtrecordworkingtimecollectionbo objLst = (wtrecordworkingtimecollectionbo)Session["objLst"];
        var vOnlySecondCal = (from col in objLst
                              where DateTime.Parse(col.WORKING_DATE) >= dtFromdate &&
                              DateTime.Parse(col.WORKING_DATE) <= dtTodate
                              select col);
        foreach (wtrecordworkingtimebo objBo in vOnlySecondCal)
        {
            if (e.Day.Date == DateTime.Parse(objBo.WORKING_DATE) && objBo.STS.ToString().Trim().ToUpper() == "REVIEW")
            {
                e.Cell.BackColor = System.Drawing.Color.CornflowerBlue;
                e.Cell.ForeColor = System.Drawing.Color.White;
            }
            else if (e.Day.Date == DateTime.Parse(objBo.WORKING_DATE) && objBo.STS.ToString().Trim().ToUpper() == "APPROVED")
            {
                e.Cell.BackColor = System.Drawing.Color.FromArgb(35, 186, 53);
                e.Cell.ForeColor = System.Drawing.Color.White;
            }
        }


        for (int i = 0; i < objLst.Count; i++)
        {
            wtrecordworkingtimebo objBoOuter = new wtrecordworkingtimebo();
            objBoOuter = objLst[i];

            for (int j = i + 1; j < objLst.Count; j++)
            {
                wtrecordworkingtimebo objBoInner = new wtrecordworkingtimebo();
                objBoInner = objLst[j];
                if (objBoOuter.WORKING_DATE == objBoInner.WORKING_DATE)
                {
                    if (e.Day.DayNumberText.ToString() == DateTime.Parse(objBoInner.WORKING_DATE.ToString()).Day.ToString())
                    {
                        if (Calendar2.VisibleDate.Month == DateTime.Parse(objBoInner.WORKING_DATE).Month)
                        {
                            if (!e.Day.IsOtherMonth)
                            {
                                //e.Cell.BackColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
        }
        leaverequestcollectionbo objLeaveLst = (leaverequestcollectionbo)Session["objleaveLst"];
        var vOnlyLeaveFirstCal = (from col in objLeaveLst
                                  where (col.DATUM) >= dtFromdate &&
                                  (col.DATUM) <= dtTodate
                                  select col);
        foreach (leaverequestbo objBo in vOnlyLeaveFirstCal)
        {
            string PERNR = objBo.PERNR;
            string ccode = Session["CompCode"].ToString();
            string emplogin = PERNR.ToString();
            int cnt = ccode.Length;
            emplogin = emplogin.Substring(cnt);
            string empid = emplogin.Trim();
            // if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() != "REJECTED")
            {
                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#f24343");
                e.Cell.ForeColor = System.Drawing.Color.White;
                TimeSpan Tm = e.Day.Date - DtMillanium;
                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
            }
        }
    }
    //Here will assign saved dates to calendar
    protected void Calendar3_DayRender(object sender, DayRenderEventArgs e)
    {
        DateTime dtGivenDate = Calendar3.VisibleDate.Date;
        DateTime dtTodate, dtFromdate;

        GetFirstInMonth(dtGivenDate, out dtFromdate);
        LastDayOfMonth(dtFromdate, out dtTodate);
        wtrecordworkingtimecollectionbo objLst = (wtrecordworkingtimecollectionbo)Session["objLst"];
        var vOnlyLastCal = (from col in objLst
                            where DateTime.Parse(col.WORKING_DATE) >= dtFromdate &&
                            DateTime.Parse(col.WORKING_DATE) <= dtTodate
                            select col);
        foreach (wtrecordworkingtimebo objBo in vOnlyLastCal)
        {
            if (e.Day.Date == DateTime.Parse(objBo.WORKING_DATE) && objBo.STS.ToString().Trim().ToUpper() == "REVIEW")
            {
                e.Cell.BackColor = System.Drawing.Color.CornflowerBlue;
                e.Cell.ForeColor = System.Drawing.Color.White;
            }
            else if (e.Day.Date == DateTime.Parse(objBo.WORKING_DATE) && objBo.STS.ToString().Trim().ToUpper() == "APPROVED")
            {
                e.Cell.BackColor = System.Drawing.Color.FromArgb(35, 186, 53);
                e.Cell.ForeColor = System.Drawing.Color.White;
            }
        }
        for (int i = 0; i < objLst.Count; i++)
        {
            wtrecordworkingtimebo objBoOuter = new wtrecordworkingtimebo();
            objBoOuter = objLst[i];

            for (int j = i + 1; j < objLst.Count; j++)
            {
                wtrecordworkingtimebo objBoInner = new wtrecordworkingtimebo();
                objBoInner = objLst[j];
                if (objBoOuter.WORKING_DATE == objBoInner.WORKING_DATE)
                {
                    if (e.Day.DayNumberText.ToString() == DateTime.Parse(objBoInner.WORKING_DATE.ToString()).Day.ToString())
                    {
                        if (Calendar3.VisibleDate.Month == DateTime.Parse(objBoInner.WORKING_DATE).Month)
                        {
                            if (!e.Day.IsOtherMonth)
                            {
                                // e.Cell.BackColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
        }
        leaverequestcollectionbo objLeaveLst = (leaverequestcollectionbo)Session["objleaveLst"];
        var vOnlyLeaveFirstCal = (from col in objLeaveLst
                                  where (col.DATUM) >= dtFromdate &&
                                  (col.DATUM) <= dtTodate
                                  select col);
        foreach (leaverequestbo objBo in vOnlyLeaveFirstCal)
        {
            string PERNR = objBo.PERNR;
            string ccode = Session["CompCode"].ToString();
            string emplogin = PERNR.ToString();
            int cnt = ccode.Length;
            emplogin = emplogin.Substring(cnt);
            string empid = emplogin.Trim();
            //if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")
            if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() != "REJECTED")
            {
                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#f24343");
                e.Cell.ForeColor = System.Drawing.Color.White;
                TimeSpan Tm = e.Day.Date - DtMillanium;
                string Clr = e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday ? "black" : "white";
                e.Cell.Attributes.Add("title", "Req ID\t\t: " + objBo.LEAVE_REQ_ID
                                                              + "\nDate\t\t: " + e.Day.Date.ToString("dd/MMMM/yyyy, dddd")
                                                              + "\nEmp ID\t\t: " + empid.ToString()
                                                              + "\nLeave Type\t: " + objBo.ATEXT
                                                              + "\nDuration\t: " + objBo.DURATIONTEXT
                                                              + "\nStatus\t\t: " + objBo.STATUS
                                                              + "\nNote\t\t: " + objBo.NOTE);
            }
        }
    }
    #region Weeks selections
    //Week selection is Monday to Saturday
    void GetPreviousWeekDates(DateTime Input, out DateTime Start, out DateTime End)
    {
        if (Input.Date.DayOfWeek == DayOfWeek.Sunday)
        {
            Start = Input.AddDays(-7);
            End = Start.AddDays(6);
            return;
        }
        while (Input.Date.DayOfWeek != DayOfWeek.Sunday)

            Input = Input.Date.AddDays(-1);

        Start = Input;
        End = Input.AddDays(6);
    }
    //Week selection is Monday to Sunday
    void GetNextWeekDates(DateTime Input, out DateTime Start, out DateTime End)
    {
        if (Input.Date.DayOfWeek == DayOfWeek.Sunday)
        {
            Start = Input.AddDays(7);
            End = Start.AddDays(6);
            return;
        }
        while (Input.Date.DayOfWeek != DayOfWeek.Sunday)

            Input = Input.Date.AddDays(-1);

        Start = Input;
        End = Input.AddDays(6);
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
    #endregion

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
        datesetting();
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        int i = dt.Rows.Count;

        if (dt.Rows.Count > 0)
        {
            string plnhrs = dt.Rows[dt.Rows.Count - 2]["Plndhrs"].ToString().Trim();

            if ((string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 2]["Plndhrs"].ToString().Trim()) ? 0 : decimal.Parse(dt.Rows[dt.Rows.Count - 2]["Plndhrs"].ToString().Trim())) > 0)
            {
                decimal sunhrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 2]["Sun"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 2]["Sun"].ToString().Trim()));
                decimal monhrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 2]["Mon"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 2]["Mon"].ToString().Trim()));
                decimal tuehrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 2]["Tue"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 2]["Tue"].ToString().Trim()));
                decimal wedhrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 2]["Wed"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 2]["Wed"].ToString().Trim()));
                decimal thrhrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 2]["Thur"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 2]["Thur"].ToString().Trim()));
                decimal frihrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 2]["Fri"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 2]["Fri"].ToString().Trim()));
                decimal sathrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 2]["Sat"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 2]["Sat"].ToString().Trim()));
                decimal count = sunhrs + monhrs + tuehrs + wedhrs + thrhrs + frihrs + sathrs;

                if ((string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 2]["Plndhrs"].ToString().Trim()) ? 0 : decimal.Parse(dt.Rows[dt.Rows.Count - 2]["Plndhrs"].ToString().Trim())) < count)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Actual hours entered is greater than the planned hours!');", true);
                }
            }
        }
    }

    protected void ButtonRemove_Click(object sender, EventArgs e)
    {
        Session.Add("sStatus", null);
        SaveAndRemoveRowToGrids();
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        //int rowIndex = gvRow.Cells.Count;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            if (dt.Rows.Count == 1)
            {
                // dt.Rows.Remove(dt.Rows[0]);
                //SetInitialRow();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Atleast one record should be present. Please update if required.')", true);
                return;
            }

            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex <= dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);
                    //ResetRowID(dt); 
                    ViewState["CurrentTable"] = dt;

                    grdRecordTime.DataSource = dt;
                    grdRecordTime.DataBind();

                }
            }
            //Store the current data in ViewState for future reference

        }

        //Set Previous Data on Postbacks
        SetRemoveDatas();
        datesetting();
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
                    //TextBox boxBillable = (TextBox)grdRecordTime.Rows[i].Cells[2].FindControl("txtBillable");
                    Label lblCostCenter = (Label)grdRecordTime.Rows[i].FindControl("lblCostCenter");
                    Label lblOrder = (Label)grdRecordTime.Rows[i].FindControl("lblOrder");
                    TextBox boxStaffGrade = (TextBox)grdRecordTime.Rows[i].FindControl("txtStaffGrade");
                    TextBox boxTotal = (TextBox)grdRecordTime.Rows[i].FindControl("txtTotal");
                    TextBox boxSUN = (TextBox)grdRecordTime.Rows[i].FindControl("txtSUN");
                    TextBox boxMON = (TextBox)grdRecordTime.Rows[i].FindControl("txtMON");
                    TextBox boxTUE = (TextBox)grdRecordTime.Rows[i].FindControl("txtTUE");
                    TextBox boxWED = (TextBox)grdRecordTime.Rows[i].FindControl("txtWED");
                    TextBox boxTHU = (TextBox)grdRecordTime.Rows[i].FindControl("txtTHU");
                    TextBox boxFRI = (TextBox)grdRecordTime.Rows[i].FindControl("txtFRI");
                    TextBox boxSAT = (TextBox)grdRecordTime.Rows[i].FindControl("txtSAT");
                    TextBox boxRemarks = (TextBox)grdRecordTime.Rows[i].FindControl("txtREMARKS");


                    // Label lbl1 = (Label)grdRecordTime.Rows[i].Cells[7].FindControl("lblmon");
                    Label lblHours = ((Label)grdRecordTime.FooterRow.FindControl("lblHours"));
                    Label lblSun = ((Label)grdRecordTime.FooterRow.FindControl("lblSun"));
                    Label lblMon = ((Label)grdRecordTime.FooterRow.FindControl("lblMon"));
                    Label lblTues = ((Label)grdRecordTime.FooterRow.FindControl("lblTues"));
                    Label lblWed = ((Label)grdRecordTime.FooterRow.FindControl("lblWed"));
                    Label lblThu = ((Label)grdRecordTime.FooterRow.FindControl("lblThu"));
                    Label lblFri = ((Label)grdRecordTime.FooterRow.FindControl("lblFri"));
                    Label lblSAt = ((Label)grdRecordTime.FooterRow.FindControl("lblSAt"));
                    Label lblRemarks = ((Label)grdRecordTime.FooterRow.FindControl("lblRemarks"));

                    DropDownList ddlOrder = (DropDownList)grdRecordTime.Rows[rowIndex].FindControl("drpdwnOrder");
                    LoadOrder(ddlOrder);

                    DropDownList ddlWbs = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnWbs");
                    LoadWbs(ddlWbs, ddlOrder.SelectedValue.ToString());

                    TextBox ddlActtype = (TextBox)grdRecordTime.Rows[i].FindControl("txtmainSearch");

                    //LoadActtype(ddlActtype, Convert.ToInt32(ddlWbs.SelectedValue));
                    //LoadActtype(ddlActtype, Convert.ToInt32(ddlWbs.SelectedValue));

                    DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[rowIndex].FindControl("drpdwnAttabsType");
                    //Fill the DropDownList with Data 
                    LoadAttabsTypes(ddl1);







                    if (i < dt.Rows.Count)
                    {

                        //Assign the value from DataTable to the TextBox 


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
                            iRowTotalHrs = decimal.Parse(boxSUN.Text) + iRowTotalHrs;
                        }

                        if (boxMON.Text.Trim() != "")
                        {
                            sMon = decimal.Parse(boxMON.Text) + sMon;
                            lblMon.Text = sMon.ToString();
                            iRowTotalHrs = decimal.Parse(boxMON.Text) + iRowTotalHrs;
                        }
                        if (boxTUE.Text.Trim() != "")
                        {
                            sTue = decimal.Parse(boxTUE.Text) + sTue;
                            lblTues.Text = sTue.ToString();
                            //  sTotalActualHrs = sTue + sTotalActualHrs;
                            iRowTotalHrs = decimal.Parse(boxTUE.Text) + iRowTotalHrs;
                        }
                        if (boxWED.Text.Trim() != "")
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
                        //if (boxRemarks.Text.Trim() != "")
                        //{
                        //    sRemarks = boxRemarks.Text.ToString().Trim();

                        //}
                        sTotalActualHrs = iRowTotalHrs + sTotalActualHrs;
                        lblHours.Text = sTotalActualHrs.ToString();
                        boxTotal.Text = iRowTotalHrs.ToString();
                        //Set the Previous Selected Items on Each DropDownList  on Postbacks 

                        if (i == 0)
                        {
                            ////ddl1.ClearSelection();
                            ////ddl1.Items.FindByValue(dt.Rows[i]["AttTypes"].ToString()).Selected = true;

                            if (dt.Rows[i]["AttTypes"].ToString().Length > 0)
                            {
                                ddl1.ClearSelection();
                                ddl1.Items.FindByValue(dt.Rows[i]["AttTypes"].ToString()).Selected = true;

                            }


                            if (dt.Rows[i]["Order"].ToString().Length > 0)
                            {
                                ddlOrder.ClearSelection();
                                ddlOrder.Items.FindByValue(dt.Rows[i]["Order"].ToString()).Selected = true;
                                ////ddlOrder.Items.FindByText(dt.Rows[i]["Order"].ToString()).Selected = true;
                            }

                            LoadWbs(ddlWbs, ddlOrder.SelectedValue.ToString());
                            if (dt.Rows[i]["Wbs"].ToString().Length > 0)
                            {
                                ddlWbs.ClearSelection();
                                ddlWbs.Items.FindByValue(dt.Rows[i]["Wbs"].ToString()).Selected = true;
                            }

                            //////=======================Bench time auto select---------------------------
                            if (ddlWbs.SelectedValue.ToString() == "00000052")
                            {
                                if (ddl1.SelectedValue == "2050")
                                    ddl1.Enabled = false;
                                else
                                    ddl1.Enabled = true;
                            }
                            //////=======================---------------------------======================
                            //DropDownList drpdwnWbs3 = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnWbs");
                            //LoadActtype(ddlActtype, Convert.ToInt32(drpdwnWbs3.SelectedValue));
                            //LoadActtype(ddlActtype, Convert.ToInt32(drpdwnWbs3.SelectedValue));
                            TextBox txtActtype = (TextBox)grdRecordTime.Rows[i].FindControl("txtmainSearch");
                            if (dt.Rows[i]["Acttype"].ToString().Length > 0)
                            {
                                txtActtype.Text = (dt.Rows[i]["Acttype"].ToString());
                                //ddlActtype.ClearSelection();
                                //ddlActtype.Items.FindByValue(dt.Rows[i]["Acttype"].ToString()).Selected = true;
                            }

                            DropDownList drpdwnWbs = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnWbs");
                            sWbs = drpdwnWbs.SelectedValue.ToString();//"PROJECT PREP";


                            //DropDownList drpdwnActtype = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnActtype");
                            //sActivity = drpdwnActtype.SelectedValue.ToString();//"PM";
                            TextBox txtActtyp = (TextBox)grdRecordTime.Rows[i].FindControl("txtmainSearch");
                            sActivity = txtActtyp.Text.ToString().Trim();



                            sAttendencetype = ddl1.SelectedValue.Trim();
                            sCostCenter = txtActtyp.Text.ToString().Trim();
                            sOrder = ddlOrder.SelectedItem.Text.ToString();

                            sstaffgrade = boxStaffGrade.Text.Trim();
                            sTotal = boxTotal.Text.Trim();

                            if (boxSUN.Text.Trim() != "")
                            {
                                if (sCount == "")
                                {

                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = txtActtyp.Text.ToString().Trim();
                                    sOrder = ddlOrder.SelectedValue.ToString();
                                    sWbs = ddlWbs.SelectedValue.ToString();
                                    //sNetwork = ddlNetwork.SelectedValue.ToString();
                                    sActivity = ddlActtype.Text.ToString().Trim();
                                    sAttendencetype = ddl1.SelectedValue.Trim();
                                    sLeaveHours = boxSUN.Text.Trim();
                                    sWorkingDate = DateTime.Parse(bdpFrom.Text).ToString("d-MMM-yyyy");
                                    sCount = "0";
                                    sRemarks = boxRemarks.Text.ToString().Trim();
                                    //sTicketID = drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = ticketsPlndhrs.Text;
                                }
                                else
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                    sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                    sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxSUN.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).ToString("d-MMM-yyyy");
                                    sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                    //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                                }
                            }

                            if (boxMON.Text.Trim() != "")
                            {
                                if (sCount == "")
                                {
                                    sAttendencetype = ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = ddlActtype.Text.ToString().Trim();
                                    sOrder = ddlOrder.SelectedValue.ToString();
                                    //sNetwork = ddlNetwork.SelectedValue.ToString();
                                    sActivity = ddlActtype.Text.ToString().Trim();
                                    sWbs = ddlWbs.SelectedValue.ToString();
                                    sLeaveHours = boxMON.Text.Trim();
                                    sWorkingDate = DateTime.Parse(bdpFrom.Text).AddDays(1).ToString("d-MMM-yyyy");
                                    sRemarks = boxRemarks.Text.ToString().Trim();
                                    sCount = "0";
                                    //sTicketID = drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = ticketsPlndhrs.Text;
                                }
                                else
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                    sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                    sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxMON.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(1).ToString("d-MMM-yyyy");
                                    sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                    //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                                }
                            }
                            if (boxTUE.Text.Trim() != "")
                            {
                                if (sCount == "")
                                {
                                    sAttendencetype = ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = ddlActtype.Text.ToString().Trim();
                                    sOrder = ddlOrder.SelectedValue.ToString();
                                    //sNetwork = ddlNetwork.SelectedValue.ToString();
                                    sActivity = ddlActtype.Text.ToString().Trim();
                                    sWbs = ddlWbs.SelectedValue.ToString();

                                    sLeaveHours = boxTUE.Text.Trim();
                                    sWorkingDate = DateTime.Parse(bdpFrom.Text).AddDays(2).ToString("d-MMM-yyyy");
                                    sRemarks = boxRemarks.Text.ToString().Trim();
                                    sCount = "0";
                                    //sTicketID = drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = ticketsPlndhrs.Text;
                                }
                                else
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                    sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                    sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxTUE.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(2).ToString("d-MMM-yyyy");
                                    sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                    //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                                }
                            }
                            if (boxWED.Text.Trim() != "")
                            {
                                if (sCount == "")
                                {
                                    sAttendencetype = ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = ddlActtype.Text.ToString().Trim();
                                    sOrder = ddlOrder.SelectedValue.ToString();
                                    //sNetwork = ddlNetwork.SelectedValue.ToString();
                                    sActivity = ddlActtype.Text.ToString().Trim();
                                    sWbs = ddlWbs.SelectedValue.ToString();


                                    sLeaveHours = boxWED.Text.Trim();
                                    sWorkingDate = DateTime.Parse(bdpFrom.Text).AddDays(3).ToString("d-MMM-yyyy");
                                    sRemarks = boxRemarks.Text.ToString().Trim();
                                    sCount = "0";
                                    //sTicketID = drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = ticketsPlndhrs.Text;
                                }
                                else
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    // sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                    sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                    sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxWED.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(3).ToString("d-MMM-yyyy");
                                    sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                    ///sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                                }
                            }
                            if (boxTHU.Text.Trim() != "")
                            {
                                if (sCount == "")
                                {
                                    sAttendencetype = ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = ddlActtype.Text.ToString().Trim();
                                    sOrder = ddlOrder.SelectedValue.ToString();
                                    //sNetwork = ddlNetwork.SelectedValue.ToString();
                                    sActivity = ddlActtype.Text.ToString().Trim();
                                    sWbs = ddlWbs.SelectedValue.ToString();

                                    sLeaveHours = boxTHU.Text.Trim();
                                    sWorkingDate = DateTime.Parse(bdpFrom.Text).AddDays(4).ToString("d-MMM-yyyy");
                                    sRemarks = boxRemarks.Text.ToString().Trim();
                                    sCount = "0";
                                    //sTicketID = drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = ticketsPlndhrs.Text;
                                }
                                else
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                    sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                    sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxTHU.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(4).ToString("d-MMM-yyyy");
                                    sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                    //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                                }
                            }
                            if (boxFRI.Text.Trim() != "")
                            {
                                if (sCount == "")
                                {
                                    sAttendencetype = ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = ddlActtype.Text.ToString().Trim();
                                    sOrder = ddlOrder.SelectedValue.ToString();
                                    //sNetwork = ddlNetwork.SelectedValue.ToString();
                                    sActivity = ddlActtype.Text.ToString().Trim();
                                    sWbs = ddlWbs.SelectedValue.ToString();


                                    sLeaveHours = boxFRI.Text.Trim();
                                    sWorkingDate = DateTime.Parse(bdpFrom.Text).AddDays(5).ToString("d-MMM-yyyy");
                                    sRemarks = boxRemarks.Text.ToString().Trim();
                                    sCount = "0";
                                    //sTicketID = drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = ticketsPlndhrs.Text;
                                }
                                else
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                    sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                    sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxFRI.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(5).ToString("d-MMM-yyyy");
                                    sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                    //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                                }
                            }
                            if (boxSAT.Text.Trim() != "")
                            {
                                if (sCount == "")
                                {

                                    //sCostCenter = "0";
                                    //sOrder = ddlOrder.SelectedValue.ToString();
                                    //sNetwork = ddlNetwork.SelectedValue.ToString();
                                    //sActivity = ddlActtype.SelectedValue.ToString();
                                    //sWbs = ddlWbs.SelectedValue.ToString();


                                    sAttendencetype = ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = ddlActtype.Text.ToString().Trim();
                                    sOrder = ddlOrder.SelectedValue.ToString();
                                    //sNetwork = ddlNetwork.SelectedValue.ToString();
                                    sActivity = ddlActtype.Text.ToString().Trim();
                                    sWbs = ddlWbs.SelectedValue.ToString();


                                    sLeaveHours = boxSAT.Text.Trim();
                                    sWorkingDate = DateTime.Parse(bdpFrom.Text).AddDays(6).ToString("d-MMM-yyyy");
                                    sRemarks = boxRemarks.Text.ToString().Trim();
                                    sCount = "0";
                                    //sTicketID = drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = ticketsPlndhrs.Text;
                                }
                                else
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    ////FOR DELETING COST CENTER
                                    //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                    sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                    sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxSAT.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(6).ToString("d-MMM-yyyy");
                                    sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                    //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                    //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                                }
                            }
                        }
                        else
                        {
                            ddl1.ClearSelection();
                            ddl1.Items.FindByValue(dt.Rows[i]["AttTypes"].ToString()).Selected = true;



                            if (dt.Rows[i]["Order"].ToString().Length > 0)
                            {
                                ddlOrder.ClearSelection();
                                ddlOrder.Items.FindByValue(dt.Rows[i]["Order"].ToString()).Selected = true;
                            }
                            LoadWbs(ddlWbs, ddlOrder.SelectedValue.ToString());
                            if (dt.Rows[i]["Wbs"].ToString().Length > 0)
                            {
                                ddlWbs.ClearSelection();
                                ddlWbs.Items.FindByValue(dt.Rows[i]["Wbs"].ToString()).Selected = true;
                            }
                            DropDownList drpdwnWbs4 = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnWbs");
                            //LoadActtype(ddlActtype, Convert.ToInt32(drpdwnWbs4.SelectedValue));
                            //LoadActtype(ddlActtype, Convert.ToInt32(drpdwnWbs4.SelectedValue));

                            TextBox txtActtype = (TextBox)grdRecordTime.Rows[i].FindControl("txtmainSearch");
                            if (dt.Rows[i]["Acttype"].ToString().Length > 0)
                            {
                                txtActtype.Text = (dt.Rows[i]["Acttype"].ToString());
                                //ddlActtype.ClearSelection();
                                //ddlActtype.Items.FindByValue(dt.Rows[i]["Acttype"].ToString()).Selected = true;
                            }

                            ////newly added ends

                            if (ddlWbs.SelectedValue.ToString() == "00000052")
                            {
                                if (ddl1.SelectedValue == "2050")
                                    ddl1.Enabled = false;
                                else
                                    ddl1.Enabled = true;
                            }
                            //////=======================---------------------------======================
                            sstaffgrade = sstaffgrade + "|" + boxStaffGrade.Text.Trim();
                            sTotal = sTotal + "|" + boxTotal.Text.Trim();

                            if (boxSUN.Text.Trim() != "")
                            {
                                sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                ////FOR DELETING COST CENTER
                                //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                sLeaveHours = sLeaveHours + "|" + boxSUN.Text.Trim();
                                sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).ToString("d-MMM-yyyy");
                                sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                            }

                            if (boxMON.Text.Trim() != "")
                            {
                                sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                ////FOR DELETING COST CENTER
                                //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                sLeaveHours = sLeaveHours + "|" + boxMON.Text.Trim();
                                sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(1).ToString("d-MMM-yyyy");
                                sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                            }
                            if (boxTUE.Text.Trim() != "")
                            {
                                sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                ////FOR DELETING COST CENTER
                                //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                sLeaveHours = sLeaveHours + "|" + boxTUE.Text.Trim();
                                sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(2).ToString("d-MMM-yyyy");
                                sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                            }
                            if (boxWED.Text.Trim() != "")
                            {
                                sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                ////FOR DELETING COST CENTER
                                //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                sLeaveHours = sLeaveHours + "|" + boxWED.Text.Trim();
                                sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(3).ToString("d-MMM-yyyy");
                                sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                            }
                            if (boxTHU.Text.Trim() != "")
                            {
                                sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                ////FOR DELETING COST CENTER
                                //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                sLeaveHours = sLeaveHours + "|" + boxTHU.Text.Trim();
                                sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(4).ToString("d-MMM-yyyy");
                                sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                            }
                            if (boxFRI.Text.Trim() != "")
                            {
                                sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                ////FOR DELETING COST CENTER
                                //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                sLeaveHours = sLeaveHours + "|" + boxFRI.Text.Trim();
                                sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(5).ToString("d-MMM-yyyy");
                                sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                            }
                            if (boxSAT.Text.Trim() != "")
                            {
                                sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                ////FOR DELETING COST CENTER
                                //sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                sCostCenter = sCostCenter + "|" + ddlActtype.Text.ToString().Trim();
                                sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                //sNetwork = sNetwork + "|" + ddlNetwork.SelectedValue.ToString();
                                sActivity = sActivity + "|" + ddlActtype.Text.ToString().Trim();
                                sWbs = sWbs + "|" + ddlWbs.SelectedValue.ToString();
                                sLeaveHours = sLeaveHours + "|" + boxSAT.Text.Trim();
                                sWorkingDate = sWorkingDate + "|" + DateTime.Parse(bdpFrom.Text).AddDays(6).ToString("d-MMM-yyyy");
                                sRemarks = sRemarks + "|" + boxRemarks.Text.ToString().Trim();
                                //sTicketID = sTicketID + "|" + drpdowntickets.SelectedValue.Trim();
                                //sPlndhrs = sPlndhrs + "|" + ticketsPlndhrs.Text;
                            }
                        }
                    }
                    rowIndex++;
                }
            }
        }

    }

    private void SaveAndRemoveRowToGrids()
    {
        decimal HalfdayHrs = decimal.Parse(ViewState["WorkingHours"].ToString()) / 2;

        bStatus = true;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                ViewState["CurrentTable"] = dtCurrentTable;

                for (int i = 0; i < grdRecordTime.Rows.Count; i++)
                {

                    //extract the TextBox values 
                    Label lblOrder = (Label)grdRecordTime.Rows[i].FindControl("lblOrder");
                    Label lblCostCenter = (Label)grdRecordTime.Rows[i].FindControl("lblCostCenter");
                    TextBox boxStaffGrade = (TextBox)grdRecordTime.Rows[i].FindControl("txtStaffGrade");
                    TextBox boxTotal = (TextBox)grdRecordTime.Rows[i].FindControl("txtTotal");
                    TextBox boxSUN = (TextBox)grdRecordTime.Rows[i].FindControl("txtSUN");
                    TextBox boxMON = (TextBox)grdRecordTime.Rows[i].FindControl("txtMON");
                    TextBox boxTUE = (TextBox)grdRecordTime.Rows[i].FindControl("txtTUE");
                    TextBox boxWED = (TextBox)grdRecordTime.Rows[i].FindControl("txtWED");
                    TextBox boxTHU = (TextBox)grdRecordTime.Rows[i].FindControl("txtTHU");
                    TextBox boxFRI = (TextBox)grdRecordTime.Rows[i].FindControl("txtFRI");
                    TextBox boxSAT = (TextBox)grdRecordTime.Rows[i].FindControl("txtSAT");
                    TextBox boxRemarks = (TextBox)grdRecordTime.Rows[i].FindControl("txtREMARKS");
                    //dtCurrentTable.Rows[i]["Column14"] = lblOrder.Text;
                    //dtCurrentTable.Rows[i]["Column13"] = lblCostCenter.Text;
                    dtCurrentTable.Rows[i]["Staff"] = boxStaffGrade.Text;
                    dtCurrentTable.Rows[i]["Total"] = boxTotal.Text;
                    dtCurrentTable.Rows[i]["Sun"] = boxSUN.Text;
                    dtCurrentTable.Rows[i]["Mon"] = boxMON.Text;
                    dtCurrentTable.Rows[i]["Tue"] = boxTUE.Text;
                    dtCurrentTable.Rows[i]["Wed"] = boxWED.Text;
                    dtCurrentTable.Rows[i]["Thur"] = boxTHU.Text;
                    dtCurrentTable.Rows[i]["Fri"] = boxFRI.Text;
                    dtCurrentTable.Rows[i]["Sat"] = boxSAT.Text;
                    dtCurrentTable.Rows[i]["Remarks"] = boxRemarks.Text;


                    dtCurrentTable.Rows[i]["CostCenter"] = "0";
                    DropDownList ddlOrder = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnOrder");
                    dtCurrentTable.Rows[i]["Order"] = ddlOrder.SelectedValue.ToString();
                    DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnAttabsType");
                    dtCurrentTable.Rows[i]["AttTypes"] = ddl1.SelectedValue.ToString();
                    //DropDownList ddlActtype = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnActtype");
                    TextBox txtActtype = (TextBox)grdRecordTime.Rows[i].FindControl("txtmainSearch");
                    dtCurrentTable.Rows[i]["Acttype"] = txtActtype.Text.ToString().Trim();
                    DropDownList ddlWbs = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnWbs");
                    dtCurrentTable.Rows[i]["Wbs"] = ddlWbs.SelectedValue.ToString();
                    if ((string)Session["sStatus"] == "Review")
                    {
                        DateTime dtFormGivenDate = Calendar1.VisibleDate.Date;
                        DateTime dtToGivenDate = Calendar3.VisibleDate.Date;
                        DateTime dtTodate, dtFromdate;

                        GetFirstInMonth(dtFormGivenDate, out dtFromdate);
                        LastDayOfMonth(dtToGivenDate, out dtTodate);
                        leaverequestbo objleaveBo = new leaverequestbo();
                        leaverequestbl objleaveBl = new leaverequestbl();
                        leaverequestcollectionbo objLeaveLst = new leaverequestcollectionbo();
                        objleaveBo.PERNR = User.Identity.Name;
                        objleaveBo.FROM_DATE = dtFromdate;
                        objleaveBo.TO_DATE = dtTodate;
                        objleaveBo.COMMENTS = Session["CompCode"].ToString();
                        objLeaveLst = objleaveBl.Get_Calendar_Leave_Markings_ForRWT(objleaveBo, 2);
                        var objLst = from col in objLeaveLst
                                     where (col.STATUS == "APPROVED" || col.STATUS == "SENT")
                                     select col;

                        foreach (leaverequestbo obj in objLeaveLst)
                        {
                            decimal iLeaveHours = 0;

                            if (!((obj.STDAZ == string.Empty) || (obj.STDAZ == "")))
                            {
                                if (obj.STDAZ.Contains(":"))
                                {
                                    string str1 = obj.STDAZ.Split(':').ElementAt(0).ToString();
                                    ////iLeaveHours = Convert.ToInt32(str1);
                                    decimal.TryParse(str1, out iLeaveHours);
                                }
                                if (obj.STDAZ.Contains("."))
                                {
                                    string str1 = obj.STDAZ.Split('.').ElementAt(0).ToString();
                                    ////iLeaveHours = Convert.ToInt32(str1);
                                    decimal.TryParse(str1, out iLeaveHours);
                                }
                            }
                            // string.IsNullOrEmpty(txtPropContr.Text.ToString()) ? 0 : decimal.Parse(txtPropContr.Text.ToString()
                            //if ((boxSUN.Text != "") || (string.IsNullOrEmpty(boxSUN.Text.ToString()) ? 0 : decimal.Parse(boxSUN.Text.ToString().Trim())) > 0)
                            if ((string.IsNullOrEmpty(boxSUN.Text.ToString()) ? 0 : decimal.Parse(boxSUN.Text.ToString().Trim())) > 0)
                            {
                                if (obj.DATUM.ToString() == DateTime.Parse(bdpFrom.Text).ToString() && obj.AWART != ddl1.SelectedValue)
                                {
                                    //if (iLeaveHours >= 8)
                                    if (iLeaveHours > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        //lblMessageBoard.Text = "You can't apply timesheet on " + bdpFrom.SelectedDate.AddDays(1).ToString("d-MMM-yyyy") + " , Since you were on leave";
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied leave on " + DateTime.Parse(bdpFrom.Text).AddDays(1).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        //lblMessageBoard.Text = "Please select " + obj.ATEXT + " for the date " + bdpFrom.SelectedDate.AddDays(1).ToString("d-MMM-yyyy");
                                        bStatus = false;
                                        return;
                                    }
                                    else if (decimal.Parse(boxSUN.Text.Trim()) > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        //lblMessageBoard.Text = "You can't apply timesheet on " + bdpFrom.SelectedDate.AddDays(1).ToString("d-MMM-yyyy") + " for " + boxSUN.Text.Trim() + " hours , Since you had taken half a day leave";
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied half day leave on " + DateTime.Parse(bdpFrom.Text).AddDays(1).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        //lblMessageBoard.Text = "Please select " + obj.ATEXT + " for the date " + bdpFrom.SelectedDate.AddDays(1).ToString("d-MMM-yyyy");
                                        bStatus = false;
                                        return;
                                    }
                                }
                            }

                            if ((string.IsNullOrEmpty(boxMON.Text.ToString()) ? 0 : decimal.Parse(boxMON.Text.ToString().Trim())) > 0)
                            {
                                if (obj.DATUM.ToString() == DateTime.Parse(bdpFrom.Text).AddDays(1).ToString() && obj.AWART != ddl1.SelectedValue)
                                {
                                    if (iLeaveHours > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied leave on " + DateTime.Parse(bdpFrom.Text).AddDays(1).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                    else if (decimal.Parse(boxMON.Text.Trim()) > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied half day leave on" + DateTime.Parse(bdpFrom.Text).AddDays(1).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                }
                            }

                            if ((string.IsNullOrEmpty(boxTUE.Text.ToString()) ? 0 : decimal.Parse(boxTUE.Text.ToString().Trim())) > 0)
                            {
                                if (obj.DATUM.ToString() == DateTime.Parse(bdpFrom.Text).AddDays(2).ToString() && obj.AWART != ddl1.SelectedValue)
                                {
                                    if (iLeaveHours > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied leave on " + DateTime.Parse(bdpFrom.Text).AddDays(2).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                    else if (decimal.Parse(boxTUE.Text.Trim()) > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied half day leave on " + DateTime.Parse(bdpFrom.Text).AddDays(2).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                }
                            }

                            if ((string.IsNullOrEmpty(boxWED.Text.ToString()) ? 0 : decimal.Parse(boxWED.Text.ToString().Trim())) > 0)
                            {
                                if (obj.DATUM.ToString() == DateTime.Parse(bdpFrom.Text).AddDays(3).ToString() && obj.AWART != ddl1.SelectedValue)
                                {
                                    if (iLeaveHours > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied leave on " + DateTime.Parse(bdpFrom.Text).AddDays(3).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                    else if (decimal.Parse(boxWED.Text.Trim()) > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied half day leave on " + DateTime.Parse(bdpFrom.Text).AddDays(3).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                }
                            }

                            if ((string.IsNullOrEmpty(boxTHU.Text.ToString()) ? 0 : decimal.Parse(boxTHU.Text.ToString().Trim())) > 0)
                            {
                                if (obj.DATUM.ToString() == DateTime.Parse(bdpFrom.Text).AddDays(4).ToString() && obj.AWART != ddl1.SelectedValue)
                                {
                                    if (iLeaveHours > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied leave on " + DateTime.Parse(bdpFrom.Text).AddDays(4).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                    else if (decimal.Parse(boxTHU.Text.Trim()) > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied half day leave on " + DateTime.Parse(bdpFrom.Text).AddDays(4).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                }
                            }

                            if ((string.IsNullOrEmpty(boxFRI.Text.ToString()) ? 0 : decimal.Parse(boxFRI.Text.ToString().Trim())) > 0)
                            {
                                if (obj.DATUM.ToString() == DateTime.Parse(bdpFrom.Text).AddDays(5).ToString() && obj.AWART != ddl1.SelectedValue)
                                {
                                    if (iLeaveHours > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied leave on " + DateTime.Parse(bdpFrom.Text).AddDays(5).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                    else if (decimal.Parse(boxFRI.Text.Trim()) > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied half day leave on " + DateTime.Parse(bdpFrom.Text).AddDays(5).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                }
                            }

                            if ((string.IsNullOrEmpty(boxSAT.Text.ToString()) ? 0 : decimal.Parse(boxSAT.Text.ToString().Trim())) > 0)
                            {
                                if (obj.DATUM.ToString() == DateTime.Parse(bdpFrom.Text).AddDays(6).ToString() && obj.AWART != ddl1.SelectedValue)
                                {
                                    if (iLeaveHours > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied leave on " + DateTime.Parse(bdpFrom.Text).AddDays(6).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                    else if (decimal.Parse(boxSAT.Text.Trim()) > HalfdayHrs)
                                    {
                                        LblPopUp.Visible = lblMessageBoard.Visible = true;
                                        LblPopUp.ForeColor = lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        LblPopUp.Text = lblMessageBoard.Text = "Since you had applied half day leave on " + DateTime.Parse(bdpFrom.Text).AddDays(6).ToString("d-MMM-yyyy") + " , You are unable to add time sheet";
                                        bStatus = false;
                                        return;
                                    }
                                }
                            }

                        }
                    }
                }
                //Rebind the Grid with the current data to reflect changes 
                grdRecordTime.DataSource = dtCurrentTable;
                grdRecordTime.DataBind();

            }
        }
        else
        {
            Response.Write("");

        }
        //Set Previous Data on Postbacks 
        SetRemoveDatas();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            LoadSaveUpdate();
            //mp1.Hide();
            //SetInitialRow();
            //DateTime dtSelectedDate = DateTime.Now;
            //DateTime dtStartDate, dtEndDate;

            //GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            //bdpFrom.Text = dtStartDate.ToString();
            //bdpTo.Text = dtEndDate.ToString();
            //ControlStatus();
            //LoadRecordWorkingToCalendars(1);
            //GV_SelectedDateLeaveView.DataSource = null;
            //GV_SelectedDateLeaveView.DataBind();
        }
        catch (Exception Ex)
        {
            //string msg = "";
            if (Ex.Message.StartsWith("3"))
            {
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Last month timesheet can be submit on or before 5th of current month!";

                LblPopUp.Visible = true;
                LblPopUp.ForeColor = System.Drawing.Color.Red;
                LblPopUp.Text = "Last month timesheet can be submit on or before 5th of current month!";
                // mp1.Show();
            }
            else
            {
                switch (Ex.Message)
                {
                    case "3":
                        lblMessageBoard.Visible = true;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Last month timesheet can be submit on or before 5th of current month!";

                        LblPopUp.Visible = true;
                        LblPopUp.ForeColor = System.Drawing.Color.Red;
                        LblPopUp.Text = "Last month timesheet can be submit on or before 5th of current month!";
                        //mp1.Show();
                        break;
                    case "4":
                        lblMessageBoard.Visible = true;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = "Cannot update old records..!";

                        LblPopUp.Visible = true;
                        LblPopUp.ForeColor = System.Drawing.Color.Red;
                        LblPopUp.Text = "Cannot update old records..!";
                        // mp1.Show();
                        break;
                    default:
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();

                        LblPopUp.ForeColor = System.Drawing.Color.Red;
                        LblPopUp.Text = GetLocalResourceObject("UnkownError").ToString();

                        return;
                        break;
                }
            }
        }
    }

    protected void LoadSaveUpdate()
    {
        bool? SuperVisorstatus = true;
        string Pernr = "";
        string SuperVisorPernr = "";
        string PernrEmail = "";
        string SuperVisorEmail = "";
        string ErrorMessage = "";
        string Pkey = "";

        Session.Add("sStatus", null);
        bool IsSave = (bool)Session["IsSave"];
        //Session["CompCode"]="tevi";

        SaveAndRemoveRowToGrids();
        wtrecordworkingtimebo objRecordWrkTimeBo = new wtrecordworkingtimebo();
        wtrecordworkingtimebl objRecordWrkTimeBl = new wtrecordworkingtimebl();
        objRecordWrkTimeBo.PERNR = User.Identity.Name;
        objRecordWrkTimeBo.AWART = sAttendencetype;
        objRecordWrkTimeBo.COST_CENTER = sCostCenter;
        objRecordWrkTimeBo.ORDER = sOrder;
        objRecordWrkTimeBo.CATSHOURS = sLeaveHours;
        objRecordWrkTimeBo.WORKING_DATE = sWorkingDate;
        //objRecordWrkTimeBo.RNPLNR = sNetwork;
        objRecordWrkTimeBo.LSTAR = sActivity;
        objRecordWrkTimeBo.RPROJ = sWbs;
        objRecordWrkTimeBo.REMARKS = sRemarks;
        objRecordWrkTimeBo.TID = sTicketID;
        objRecordWrkTimeBo.PLNDHRS = sPlndhrs;
        //int? iResultCode = 0;
        //============================================================================= COMPANY CODE ===========================================
        objRecordWrkTimeBo.Ccode = Session["CompCode"].ToString();
        //=====================================================================================================================================
        string sModuleName = "Record working is changed";
        string strMailToList = string.Empty;
        if (IsSave)
        {
            // int? iResultCode = 0;
            int iResultCode = objRecordWrkTimeBl.Create_RecordWorkingTime(objRecordWrkTimeBo, ref SuperVisorstatus, ref SuperVisorPernr,
                                  ref SuperVisorEmail, ref Pernr, ref PernrEmail, ref ErrorMessage, ref Pkey);
            lblMessageBoard.Visible = true;
            lblMessageBoard.ForeColor = System.Drawing.Color.Green;

            LblPopUp.Visible = true;
            LblPopUp.ForeColor = System.Drawing.Color.Green;

            if (Session["isDR"].ToString().Trim() == "True")
            {
                lblMessageBoard.Text = "Record Working details submitted successfully and has been self approved";
                LblPopUp.Text = "Record Working details submitted successfully and has been self approved";
            }

            else
            {
                lblMessageBoard.Text = "Record Working details submitted successfully and sent for approval";
                LblPopUp.Text = "Record Working details submitted successfully and sent for approval";
            }

            if (iResultCode == 0)
            {
                //mp1.Hide();
                pnlpopUp.Visible = false;
                divcal.Visible = true;
                SetInitialRow();
                DateTime dtSelectedDate = DateTime.Now;
                DateTime dtStartDate, dtEndDate;

                GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
                bdpFrom.Text = dtStartDate.ToString();
                bdpTo.Text = dtEndDate.ToString();
                ControlStatus();
                LoadRecordWorkingToCalendars(1);
                GV_SelectedDateLeaveView.DataSource = null;
                GV_SelectedDateLeaveView.DataBind();
            }
            if (iResultCode == 1)
            {
                
                LblPopUp.Visible = true;
                LblPopUp.ForeColor = System.Drawing.Color.Red;
                LblPopUp.Text = "Entered details already exist for selected date";

                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Entered details already exist for selected date";

                //mp1.Show();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Entered details already exist for selected date');", true);
                //return;
            }
            if (iResultCode == 2)
            {
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Approver Missing";

                LblPopUp.Visible = true;
                LblPopUp.ForeColor = System.Drawing.Color.Red;
                LblPopUp.Text = "Approver Missing";

                //mp1.Hide();
                pnlpopUp.Visible = false;
                divcal.Visible = true;
                SetInitialRow();
                DateTime dtSelectedDate = DateTime.Now;
                DateTime dtStartDate, dtEndDate;
                GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
                bdpFrom.Text = dtStartDate.ToString();
                bdpTo.Text = dtEndDate.ToString();
                ControlStatus();
                LoadRecordWorkingToCalendars(1);
                GV_SelectedDateLeaveView.DataSource = null;
                GV_SelectedDateLeaveView.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Approver Missing');", true);
                return;
            }

            if (iResultCode == 3)
            {
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Last month timesheet can be submit on or before 5th of current month..!";

                LblPopUp.Visible = true;
                LblPopUp.ForeColor = System.Drawing.Color.Red;
                LblPopUp.Text = "Last month timesheet can be submit on or before 5th of current month!";
                //mp1.Show();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Entered details already exist for selected date');", true);
                //return;
            }
        }
        else
        {
            wtrecordworkingtimecollectionbo objLstOne = (wtrecordworkingtimecollectionbo)Session["objLstOne"];
            string sChangeIds = "";
            int i = 0;
            foreach (wtrecordworkingtimebo obj in objLstOne)
            {
                if (i == 0)
                {
                    sChangeIds = obj.CURRENTRECORD_NO.ToString();
                }
                else
                {
                    sChangeIds = sChangeIds + '|' + obj.CURRENTRECORD_NO.ToString();
                }
                i++;
            }
            objRecordWrkTimeBo.CURRENTRECORD_NO = sChangeIds;

            int iResultCode = objRecordWrkTimeBl.Update_RecordWorkingTime(objRecordWrkTimeBo, ref SuperVisorstatus, ref SuperVisorPernr,
                                   ref SuperVisorEmail, ref Pernr, ref PernrEmail, ref ErrorMessage);
            lblMessageBoard.Visible = true;
            lblMessageBoard.ForeColor = System.Drawing.Color.Green;

            LblPopUp.Visible = true;
            LblPopUp.ForeColor = System.Drawing.Color.Green;


            if (Session["isDR"].ToString().Trim() == "True")
            {
                lblMessageBoard.Text = "Record Working details updated successfully and has been self approved";
                LblPopUp.Text = "Record Working details updated successfully and has been self approved";
            }

            else
            {
                lblMessageBoard.Text = "Record Working details updated successfully and sent for approval";
                LblPopUp.Text = "Record Working details updated successfully and sent for approval";
            }
            //lblMessageBoard.Text = "Record Working details updated successfully and sent for approval";
            if (iResultCode == 0)
            {
                //mp1.Hide();
                pnlpopUp.Visible = false;
                divcal.Visible = true;
                SetInitialRow();
                DateTime dtSelectedDate = DateTime.Now;
                DateTime dtStartDate, dtEndDate;

                GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
                bdpFrom.Text = dtStartDate.ToString();
                bdpTo.Text = dtEndDate.ToString();
                ControlStatus();
                LoadRecordWorkingToCalendars(1);
                GV_SelectedDateLeaveView.DataSource = null;
                GV_SelectedDateLeaveView.DataBind();
            }
            if (iResultCode == 1)
            {
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Entered details already exist for selected date";////ErrorMessage;

                LblPopUp.Visible = true;
                LblPopUp.ForeColor = System.Drawing.Color.Red;
                LblPopUp.Text = "Entered details already exist for selected date";////ErrorMessage;

                //mp1.Show();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Entered details already exist for selected date');", true);
                //return;
            }
            if (iResultCode == 2)
            {
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Approver Missing";////ErrorMessage;

                LblPopUp.Visible = true;
                LblPopUp.ForeColor = System.Drawing.Color.Red;
                LblPopUp.Text = "Approver Missing";////ErrorMessage;

                //mp1.Hide();
                pnlpopUp.Visible = false;
                divcal.Visible = true;
                SetInitialRow();
                DateTime dtSelectedDate = DateTime.Now;
                DateTime dtStartDate, dtEndDate;

                GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
                bdpFrom.Text = dtStartDate.ToString();
                bdpTo.Text = dtEndDate.ToString();
                ControlStatus();
                LoadRecordWorkingToCalendars(1);
                GV_SelectedDateLeaveView.DataSource = null;
                GV_SelectedDateLeaveView.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Approver Missing');", true);
                return;
            }
            if (iResultCode == 3)
            {
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Last month timesheet can be submit on or before 5th of current month..!";

                LblPopUp.Visible = true;
                LblPopUp.ForeColor = System.Drawing.Color.Red;
                LblPopUp.Text = "Last month timesheet can be submit on or before 5th of current month..!";
                //mp1.Show();
            }
        }
    }

    protected void btnNextWeek_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dtSelectedDate = DateTime.Parse(bdpFrom.Text);
            DateTime dtStartDate, dtEndDate;

            GetNextWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.Text = dtStartDate.ToString("dd/MM/yyyy");
            bdpTo.Text = dtEndDate.ToString("dd/MM/yyyy");
            datesetting();
            LoadToGridDeatils(dtStartDate, dtEndDate);
            GV_SelectedDateLeaveView.DataSource = null;
            GV_SelectedDateLeaveView.DataBind();
        }
        catch (Exception Ex)
        {
            switch (Ex.Message.ToString().Trim())
            {
                case "Object reference not set to an instance of an object.":

                    //mp1.Hide();
                    pnlpopUp.Visible = false;
                    divcal.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Master data is missing from iEmpPower')", true);
                    return;
                    break;

                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                    break;
            }


        }
    }

    protected void btnPreviousWeek_Click(object sender, EventArgs e)
    {
        try
        {

            DateTime dtSelectedDate = DateTime.Parse(bdpFrom.Text);
            DateTime dtStartDate, dtEndDate;

            GetPreviousWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.Text = dtStartDate.ToString("dd/MM/yyyy");
            bdpTo.Text = dtEndDate.ToString("dd/MM/yyyy");

            datesetting();


            LoadToGridDeatils(dtStartDate, dtEndDate);
            GV_SelectedDateLeaveView.DataSource = null;
            GV_SelectedDateLeaveView.DataBind();
        }
        catch (Exception Ex)
        {
            switch (Ex.Message.ToString().Trim())
            {
                case "Object reference not set to an instance of an object.":

                    // mp1.Hide();
                    pnlpopUp.Visible = false;
                    divcal.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Master data is missing from iEmpPower')", true);
                    return;
                    break;

                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                    break;
            }


        }
    }

    protected void btnReview_Click(object sender, EventArgs e)
    {

        //---cannot apply for future dates


        DateTime dtSelectedDate = DateTime.Parse(bdpFrom.Text);
        DateTime dtStartDate, dtEndDate;


        int result = DateTime.Compare(dtSelectedDate, DateTime.Today);
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('You cannot apply for future week !');", true);
            return;
        }

        GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);

        for (int y = 0; y < 7; y++)
        {
            int result1 = DateTime.Compare(dtStartDate.AddDays(y), DateTime.Today);
            if (result1 > 0)
            {
                if (y == 0)
                {
                    for (int y0 = 0; y0 < grdRecordTime.Rows.Count; y0++)
                    {
                        TextBox boxSUN = (TextBox)grdRecordTime.Rows[y0].FindControl("txtSUN");
                        if (!string.IsNullOrEmpty(boxSUN.Text))
                        {
                            if (decimal.Parse(boxSUN.Text.Trim()) > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('You cannot apply for future dates !');", true);
                                return;
                            }
                        }
                    }

                }
                else if (y == 1)
                {
                    for (int y1 = 0; y1 < grdRecordTime.Rows.Count; y1++)
                    {
                        TextBox boxMON = (TextBox)grdRecordTime.Rows[y1].FindControl("txtMON");
                        if (!string.IsNullOrEmpty(boxMON.Text))
                        {
                            if (decimal.Parse(boxMON.Text.Trim()) > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('You cannot apply for future dates !');", true);
                                return;
                            }
                        }
                    }
                }
                else if (y == 2)
                {
                    for (int y2 = 0; y2 < grdRecordTime.Rows.Count; y2++)
                    {
                        TextBox boxTUE = (TextBox)grdRecordTime.Rows[y2].FindControl("txtTUE");
                        if (!string.IsNullOrEmpty(boxTUE.Text))
                        {
                            if (decimal.Parse(boxTUE.Text.Trim()) > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('You cannot apply for future dates !');", true);
                                return;
                            }
                        }
                    }
                }
                else if (y == 3)
                {
                    for (int y3 = 0; y3 < grdRecordTime.Rows.Count; y3++)
                    {
                        TextBox boxWED = (TextBox)grdRecordTime.Rows[y3].FindControl("txtWED");
                        if (!string.IsNullOrEmpty(boxWED.Text))
                        {
                            if (decimal.Parse(boxWED.Text.Trim()) > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('You cannot apply for future dates !');", true);
                                return;
                            }
                        }
                    }
                }
                else if (y == 4)
                {
                    for (int y4 = 0; y4 < grdRecordTime.Rows.Count; y4++)
                    {
                        TextBox boxTHU = (TextBox)grdRecordTime.Rows[y4].FindControl("txtTHU");
                        if (!string.IsNullOrEmpty(boxTHU.Text))
                        {
                            if (decimal.Parse(boxTHU.Text.Trim()) > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('You cannot apply for future dates !');", true);
                                return;
                            }
                        }
                    }
                }
                else if (y == 5)
                {
                    for (int y5 = 0; y5 < grdRecordTime.Rows.Count; y5++)
                    {
                        TextBox boxFRI = (TextBox)grdRecordTime.Rows[y5].FindControl("txtFRI");

                        if (!string.IsNullOrEmpty(boxFRI.Text))
                        {
                            if (decimal.Parse(boxFRI.Text.Trim()) > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('You cannot apply for future dates !');", true);
                                return;
                            }
                        }
                    }
                }
                else if (y == 6)
                {
                    for (int y6 = 0; y6 < grdRecordTime.Rows.Count; y6++)
                    {
                        TextBox boxSAT = (TextBox)grdRecordTime.Rows[y6].FindControl("txtSAT");
                        if (!string.IsNullOrEmpty(boxSAT.Text))
                        {
                            if (decimal.Parse(boxSAT.Text.Trim()) > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('You cannot apply for future dates !');", true);
                                return;
                            }
                        }
                    }
                }

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('You cannot apply for future dates !');", true);
                //return;
            }

        }

        string sStatus = "Review";
        Session.Add("sStatus", sStatus);
        SaveAndRemoveRowToGrids();
        decimal sSun = 0;
        decimal sMon = 0;
        decimal sTue = 0;
        decimal sWed = 0;
        decimal sThu = 0;
        decimal sFri = 0;
        decimal sSat = 0;
        decimal sTotalActualHrs = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];


            // Can apply mutiple data changed on 29072017 since they need daily basis remarks start


            //////if (dt.Rows.Count > 0)
            //////{
            //////    for (int x = 0; x < dt.Rows.Count; x++)
            //////    {
            //////        for (int y = x + 1; y < dt.Rows.Count; y++)
            //////        {
            //////            if (dt.Rows[x]["Order"].ToString().Trim() == dt.Rows[y]["Order"].ToString().Trim()
            //////                &&
            //////                dt.Rows[x]["AttTypes"].ToString().Trim() == dt.Rows[y]["AttTypes"].ToString().Trim()
            //////                 &&
            //////                dt.Rows[x]["Network"].ToString().Trim() == dt.Rows[y]["Network"].ToString().Trim()
            //////                 &&
            //////                dt.Rows[x]["Acttype"].ToString().Trim() == dt.Rows[y]["Acttype"].ToString().Trim()
            //////                 &&
            //////                dt.Rows[x]["Wbs"].ToString().Trim() == dt.Rows[y]["Wbs"].ToString().Trim())
            //////            {
            //////                //if (grdRecordTime.Rows.Count > 0)
            //////                //{
            //////                //    DropDownList ddlOrder = (DropDownList)grdRecordTime.Rows[grdRecordTime.Rows.Count - 1].FindControl("drpdwnOrder");
            //////                //    SetFocus(ddlOrder);
            //////                //}
            //////                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Same Data is entered twice !');", true);
            //////                return;

            //////            }
            //////        }
            //////    }
            //////}


            // Can apply mutiple data changed on 29072017 since they need daily basis remarks end



            //////    For intX As Integer = 0 To DataGridView1.Rows.Count - 2
            //////    For intY As Integer = intX + 1 To DataGridView1.Rows.Count - 2
            //////        If DataGridView1.Rows(intX ).Cells(2).Value = DataGridView1.Rows(intY ).Cells(2).Value Then
            //////            DataGridView1.Rows.RemoveAt(intY )
            //////        End If
            //////    Next
            //////Next


            if (dt.Rows.Count > 0)
            {
                //if (dt.Rows.Count == 1)
                //{
                // check the first row if only one row is present -- if multiple rows then check only last row
                string plnhrs = dt.Rows[dt.Rows.Count - 1]["Plndhrs"].ToString().Trim();

                if ((string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 1]["Plndhrs"].ToString().Trim()) ? 0 : decimal.Parse(dt.Rows[dt.Rows.Count - 1]["Plndhrs"].ToString().Trim())) > 0)
                {
                    decimal sunhrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 1]["Sun"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 1]["Sun"].ToString().Trim()));
                    decimal monhrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 1]["Mon"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 1]["Mon"].ToString().Trim()));
                    decimal tuehrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 1]["Tue"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 1]["Tue"].ToString().Trim()));
                    decimal wedhrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 1]["Wed"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 1]["Wed"].ToString().Trim()));
                    decimal thrhrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 1]["Thur"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 1]["Thur"].ToString().Trim()));
                    decimal frihrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 1]["Fri"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 1]["Fri"].ToString().Trim()));
                    decimal sathrs = string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 1]["Sat"].ToString().Trim()) ? 0 : (decimal.Parse(dt.Rows[dt.Rows.Count - 1]["Sat"].ToString().Trim()));
                    decimal count = sunhrs + monhrs + tuehrs + wedhrs + thrhrs + frihrs + sathrs;

                    if ((string.IsNullOrEmpty(dt.Rows[dt.Rows.Count - 1]["Plndhrs"].ToString().Trim()) ? 0 : decimal.Parse(dt.Rows[dt.Rows.Count - 1]["Plndhrs"].ToString().Trim())) < count)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Actual Hrs filled is more than planned hrs!');", true);
                    }
                }
                //}                            
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal iRowTotalHrs = 0;
                TextBox boxTotal = (TextBox)grdRecordTime.Rows[i].FindControl("txtTotal");
                TextBox boxSUN = (TextBox)grdRecordTime.Rows[i].FindControl("txtSUN");
                TextBox boxMON = (TextBox)grdRecordTime.Rows[i].FindControl("txtMON");
                TextBox boxTUE = (TextBox)grdRecordTime.Rows[i].FindControl("txtTUE");
                TextBox boxWED = (TextBox)grdRecordTime.Rows[i].FindControl("txtWED");
                TextBox boxTHU = (TextBox)grdRecordTime.Rows[i].FindControl("txtTHU");
                TextBox boxFRI = (TextBox)grdRecordTime.Rows[i].FindControl("txtFRI");
                TextBox boxSAT = (TextBox)grdRecordTime.Rows[i].FindControl("txtSAT");
                TextBox boxRemarks = (TextBox)grdRecordTime.Rows[i].FindControl("txtREMARKS");
                Label lblHours = ((Label)grdRecordTime.FooterRow.FindControl("lblHours"));
                Label lblSun = ((Label)grdRecordTime.FooterRow.FindControl("lblSun"));
                Label lblMon = ((Label)grdRecordTime.FooterRow.FindControl("lblMon"));
                Label lblTues = ((Label)grdRecordTime.FooterRow.FindControl("lblTues"));
                Label lblWed = ((Label)grdRecordTime.FooterRow.FindControl("lblWed"));
                Label lblThu = ((Label)grdRecordTime.FooterRow.FindControl("lblThu"));
                Label lblFri = ((Label)grdRecordTime.FooterRow.FindControl("lblFri"));
                Label lblSAt = ((Label)grdRecordTime.FooterRow.FindControl("lblSAt"));
                Label lblRemarks = ((Label)grdRecordTime.FooterRow.FindControl("lblRemarks"));

                //Assign the value from DataTable to the TextBox 

                boxTotal.Text = dt.Rows[i]["Total"].ToString();
                boxSUN.Text = dt.Rows[i]["Sun"].ToString();
                boxMON.Text = dt.Rows[i]["Mon"].ToString();
                boxTUE.Text = dt.Rows[i]["Tue"].ToString();
                boxWED.Text = dt.Rows[i]["Wed"].ToString();
                boxTHU.Text = dt.Rows[i]["Thur"].ToString();
                boxFRI.Text = dt.Rows[i]["Fri"].ToString();
                boxSAT.Text = dt.Rows[i]["Sat"].ToString();
                boxRemarks.Text = dt.Rows[i]["Remarks"].ToString();
                if (boxSUN.Text.Trim() != "")
                {
                    sSun = decimal.Parse(boxSUN.Text) + sSun;
                    lblSun.Text = sSun.ToString();
                    if (decimal.Parse(lblSun.Text) > 24)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Total hours a day cannot be more than 24 hours !');", true);
                        return;
                    }
                    //  sTotalActualHrs = sMon + sTotalActualHrs;
                    iRowTotalHrs = decimal.Parse(boxSUN.Text) + iRowTotalHrs;
                }

                if (boxMON.Text.Trim() != "")
                {
                    sMon = decimal.Parse(boxMON.Text) + sMon;
                    lblMon.Text = sMon.ToString();
                    if (decimal.Parse(lblMon.Text) > 24)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Total hours a day cannot be more than 24 hours !');", true);
                        return;
                    }
                    //  sTotalActualHrs = sMon + sTotalActualHrs;
                    iRowTotalHrs = decimal.Parse(boxMON.Text) + iRowTotalHrs;
                }
                if (boxTUE.Text.Trim() != "")
                {
                    sTue = decimal.Parse(boxTUE.Text) + sTue;
                    lblTues.Text = sTue.ToString();
                    if (decimal.Parse(lblTues.Text) > 24)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Total hours a day cannot be more than 24 hours !');", true);
                        return;
                    }
                    //  sTotalActualHrs = sTue + sTotalActualHrs;
                    iRowTotalHrs = decimal.Parse(boxTUE.Text) + iRowTotalHrs;
                }
                if (boxWED.Text.Trim() != "")
                {

                    sWed = decimal.Parse(boxWED.Text) + sWed;
                    lblWed.Text = sWed.ToString();
                    if (decimal.Parse(lblWed.Text) > 24)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Total hours a day cannot be more than 24 hours !');", true);
                        return;
                    }
                    //sTotalActualHrs = sWed + sTotalActualHrs;
                    iRowTotalHrs = decimal.Parse(boxWED.Text) + iRowTotalHrs;
                }
                if (boxTHU.Text.Trim() != "")
                {
                    sThu = decimal.Parse(boxTHU.Text) + sThu;
                    lblThu.Text = sThu.ToString();
                    if (decimal.Parse(lblThu.Text) > 24)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Total hours a day cannot be more than 24 hours !');", true);
                        return;
                    }
                    // sTotalActualHrs = sThu + sTotalActualHrs;
                    iRowTotalHrs = decimal.Parse(boxTHU.Text) + iRowTotalHrs;
                }
                if (boxFRI.Text.Trim() != "")
                {
                    sFri = decimal.Parse(boxFRI.Text) + sFri;
                    lblFri.Text = sFri.ToString();
                    if (decimal.Parse(lblFri.Text) > 24)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Total hours a day cannot be more than 24 hours !');", true);
                        return;
                    }
                    //sTotalActualHrs = sFri + sTotalActualHrs;
                    iRowTotalHrs = decimal.Parse(boxFRI.Text) + iRowTotalHrs;
                }
                if (boxSAT.Text.Trim() != "")
                {
                    sSat = decimal.Parse(boxSAT.Text) + sSat;
                    lblSAt.Text = sSat.ToString();
                    if (decimal.Parse(lblSAt.Text) > 24)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Total hours a day cannot be more than 24 hours !');", true);
                        return;
                    }
                    // sTotalActualHrs = sSat + sTotalActualHrs;
                    iRowTotalHrs = decimal.Parse(boxSAT.Text) + iRowTotalHrs;
                }
                sTotalActualHrs = iRowTotalHrs + sTotalActualHrs;
                lblHours.Text = sTotalActualHrs.ToString();
                boxTotal.Text = iRowTotalHrs.ToString();
                if (iRowTotalHrs == 0)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = GetLocalResourceObject("AtLeastOne").ToString();


                    LblPopUp.ForeColor = System.Drawing.Color.Red;
                    LblPopUp.Text = GetLocalResourceObject("AtLeastOne").ToString();

                    SetFocus(boxMON);
                    boxTotal.Text = "";
                    datesetting();
                    return;
                }
            }
        }
        if (bStatus == true)
        {

            btnPreviousStep.Enabled = true;
            btnPreviousStep.Visible = true;
            btnSave.Visible = true;
            btnExit.Visible = false;
            btnReview.Visible = false;
            btnCancel.Visible = false;
            grdRecordTime.Enabled = false;
            sPrevBtnCntrl = "View";
            Session.Add("sPrevBtnCntrl", sPrevBtnCntrl);
            FormButtonStatus(false);
        }
        datesetting();


    }

    protected void FormButtonStatus(bool bIsStatus)
    {
        if (bIsStatus == false)
        {
            btnNextWeek.Enabled = false;
            btnPreviousWeek.Enabled = false;
            btnGo.Enabled = false;
            Calendar1.Enabled = false;
            Calendar2.Enabled = false;
            Calendar3.Enabled = false;
        }
        else
        {
            btnNextWeek.Enabled = true;
            btnPreviousWeek.Enabled = true;
            btnGo.Enabled = true;
            Calendar1.Enabled = true;
            Calendar2.Enabled = true;
            Calendar3.Enabled = true;
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/Default.aspx");
    }

    protected void btnPreviousStep_Click(object sender, EventArgs e)
    {
        string PrevBtnCntrl = (string)Session["sPrevBtnCntrl"];
        btnExit.Visible = false;
        btnPreviousStep.Enabled = false;
        btnPreviousStep.Visible = false;
        btnSave.Visible = false;
        btnReview.Visible = true;
        btnReview.Enabled = true;
        btnCancel.Visible = true;
        grdRecordTime.Enabled = true;
        FormButtonStatus(true);
        Session["sPrevBtnCntrl"] = "View";

    }

    protected void grdRecordTime_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            wtrecordworkingtimebl objBl = new wtrecordworkingtimebl();
            wtrecordworkingtimecollectionbo objLst = new wtrecordworkingtimecollectionbo();

            objBo.PERNR = User.Identity.Name;
            objLst = objBl.Loak_WorkingHours(objBo);
            wtrecordworkingtimebo objHoursBo = objLst.Find(delegate (wtrecordworkingtimebo obj)
            { return true; });
            if (objHoursBo == null)
            {
                int i = 0;
                hdHours.Value = "0";
                objBo.ARBST = i.ToString();
                //objHoursBo.ARBST = i.ToString();
            }
            else
            {
                hdHours.Value = objHoursBo.ARBST;
                objBo.ARBST = objHoursBo.ARBST;
            }
        }
    }

    protected void grdRecordTime_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    //---------- GET APPLIED LEAVE ----------------------------
    #region Load GV_SelectedDataLeaveView

    private void Load_GV_SelectedDateLeaveView(string Date)
    {
        try
        {
            DateTime Dt = new DateTime();
            if (DateTime.TryParse(Date, out Dt))
            {
                lblMessageBoard.Visible = false;
                lblMessageBoard.Text = "";
                lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;


                LblPopUp.Visible = false;
                LblPopUp.Text = "";
                LblPopUp.ForeColor = System.Drawing.Color.Transparent;

                leaverequestbo ObjLeaveRequestBo = new leaverequestbo();
                leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                leaverequestcollectionbo ObjLeaveReqLst = new leaverequestcollectionbo();
                ObjLeaveRequestBo.PERNR = User.Identity.Name;
                ObjLeaveRequestBo.DATUM = Dt;
                ObjLeaveRequestBo.COMMENTS = Session["CompCode"].ToString();
                ObjLeaveReqLst = ObjLeaveRequestBl.Get_Individual_Leave_Dtls_RWT(ObjLeaveRequestBo);
                if (ObjLeaveReqLst.Count > 0)
                {
                    GV_SelectedDateLeaveView.Visible = true;
                    GV_SelectedDateLeaveView.DataSource = ObjLeaveReqLst;
                    GV_SelectedDateLeaveView.DataBind();
                }
                else
                {
                    GV_SelectedDateLeaveView.Visible = false;
                    GV_SelectedDateLeaveView.DataSource = null;
                    GV_SelectedDateLeaveView.DataBind();
                }

            }
            else
            {
                GV_SelectedDateLeaveView.DataSource = null;
                GV_SelectedDateLeaveView.DataBind();
                lblMessageBoard.Visible = true;
                lblMessageBoard.Text = "Invalid Date";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;


                LblPopUp.Visible = true;
                LblPopUp.Text = "Invalid Date";
                LblPopUp.ForeColor = System.Drawing.Color.Red;

            }
        }
        catch (Exception Ex)
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
    }

    #endregion
    //----------------------------------------------------------

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        try
        {

            //mp1.Show();

            pnlpopUp.Visible = true;
            divcal.Visible = false;
            DateTime dtSelectedDate = Calendar1.SelectedDate;
            DateTime dtStartDate, dtEndDate;

            GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.Text = dtStartDate.ToString("dd/MM/yyyy");
            bdpTo.Text = dtEndDate.ToString("dd/MM/yyyy");

            LoadToGridDeatils(dtStartDate, dtEndDate);
            Load_GV_SelectedDateLeaveView(Calendar1.SelectedDate.ToString("dd/MM/yyyy"));
        }
        catch (Exception Ex)
        {
            switch (Ex.Message.ToString().Trim())
            {
                case "Object reference not set to an instance of an object.":

                    //mp1.Hide();
                    pnlpopUp.Visible = false;
                    divcal.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Master data is missing from iEmpPower')", true);
                    return;
                    break;

                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                    break;
            }


        }
    }

    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            //mp1.Show();
            pnlpopUp.Visible = true;
            divcal.Visible = false;
            DateTime dtSelectedDate = Calendar2.SelectedDate;
            DateTime dtStartDate, dtEndDate;

            GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.Text = dtStartDate.ToString("dd/MM/yyyy");
            bdpTo.Text = dtEndDate.ToString("dd/MM/yyyy");
            LoadToGridDeatils(dtStartDate, dtEndDate);
            Load_GV_SelectedDateLeaveView(Calendar2.SelectedDate.ToString("dd/MM/yyyy"));
        }
        catch (Exception Ex)
        {
            switch (Ex.Message.ToString().Trim())
            {
                case "Object reference not set to an instance of an object.":

                    // mp1.Hide();
                    pnlpopUp.Visible = false;
                    divcal.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Master data is missing from iEmpPower')", true);
                    return;
                    break;

                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                    break;
            }
        }
    }

    protected void Calendar3_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            //mp1.Show();
            pnlpopUp.Visible = false;
            divcal.Visible = true;
            DateTime dtSelectedDate = Calendar3.SelectedDate;
            DateTime dtStartDate, dtEndDate;

            GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.Text = dtStartDate.ToString();
            bdpTo.Text = dtEndDate.ToString();

            LoadToGridDeatils(dtStartDate, dtEndDate);
            Load_GV_SelectedDateLeaveView(Calendar3.SelectedDate.ToString("dd/MM/yyyy"));
        }
        catch (Exception Ex)
        {
            switch (Ex.Message.ToString().Trim())
            {
                case "Object reference not set to an instance of an object.":

                    // mp1.Hide();
                    pnlpopUp.Visible = false;
                    divcal.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Master data is missing from iEmpPower')", true);
                    return;
                    break;

                default:
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                    break;
            }
        }
    }

    protected void LoadToGridDeatils(DateTime dtStartDate, DateTime dtEndDate)
    {
        wtrecordworkingtimebl objBl = new wtrecordworkingtimebl();
        wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
        objBo.COMMENTS = Session["CompCode"].ToString();
        objBo.FROM_DATE = dtStartDate;
        objBo.TO_DATE = dtEndDate;
        objBo.PERNR = User.Identity.Name;
        wtrecordworkingtimecollectionbo objLstOne = new wtrecordworkingtimecollectionbo();
        objLstOne = objBl.Get_RecordDetails_Week(objBo);
        wtrecordworkingtimecollectionbo objLst = (wtrecordworkingtimecollectionbo)Session["objLst"];
        var vOnlyFirstCal = (from col in objLstOne
                             where col.AWART == "2050"
                             select col);
        foreach (wtrecordworkingtimebo j in vOnlyFirstCal)
        {
            foreach (GridViewRow row in grdRecordTime.Rows)
            {
                DropDownList drpdwnattnd = (DropDownList)row.FindControl("drpdwnAttabsType");
                drpdwnattnd.Enabled = false;
            }
        }
        if (objLstOne.Count == 0)
        {
            SetInitialRow();
            ControlStatus();
            return;
        }

        DataTable CurrentTable = CreateTable();
        CurrentTable = ConvertToDataRow(objLstOne);
        // Transpose(CurrentTable);
        ViewState["CurrentTable"] = CurrentTable;
        grdRecordTime.DataSource = CurrentTable;
        grdRecordTime.DataBind();



        SetRemoveDatas();
        datesetting();
        Session.Add("objLstOne", objLstOne);
        bool bIsSave = false;
        Session.Add("IsSave", bIsSave);
    }

    public static DataTable CreateTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("CostCenter", typeof(string)));//for TextBox value 
        dt.Columns.Add(new DataColumn("Order", typeof(string)));
        dt.Columns.Add(new DataColumn("Wbs", typeof(string)));//for TextBox value 
        dt.Columns.Add(new DataColumn("AttTypes", typeof(string)));//for TextBox value 
        dt.Columns.Add(new DataColumn("Network", typeof(string)));//for TextBox value 
        dt.Columns.Add(new DataColumn("Acttype", typeof(string)));//for TextBox value 
        dt.Columns.Add(new DataColumn("TicketID", typeof(string)));//for TextBox value 
        dt.Columns.Add(new DataColumn("Plndhrs", typeof(string)));//for TextBox value 
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
            dRow["CostCenter"] = objBo.COST_CENTER.Trim();
            dRow["Order"] = objBo.ORDER.Trim();
            dRow["Wbs"] = objBo.RPROJ.Trim();
            dRow["Network"] = objBo.RNPLNR.Trim();
            dRow["Acttype"] = objBo.COST_CENTER.Trim();
            dRow["Remarks"] = objBo.REMARKS;
            dRow["TicketID"] = objBo.TID.ToString().Trim();
            dRow["Plndhrs"] = objBo.PLNDHRS.ToString().Trim();
            dt.Rows.Add(dRow);
        }
        return dt;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UI/Default.aspx");
    }

    private DataTable Transpose(DataTable dt)
    {
        DataTable dtNew = new DataTable();

        //adding columns	
        for (int i = 0; i <= dt.Rows.Count; i++)
        {
            dtNew.Columns.Add(i.ToString());
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dtNew.Columns[i + 1].ColumnName == dt.Rows[i].ItemArray[0].ToString())
            {
            }
        }
        //Adding Row Data	
        for (int k = 1; k < dt.Columns.Count; k++)
        {
            DataRow r = dtNew.NewRow();
            r[0] = dt.Columns[k].ToString();
            for (int j = 1; j <= dt.Rows.Count; j++)
                r[j] = dt.Rows[j - 1][k];
            dtNew.Rows.Add(r);
        }

        return dtNew;
    }



    protected void ValidateRows()
    {
    }



    private String readHtmlPage(string url)
    {
        String result = "";
        String strPost = "x=1&y=2&z=YouPostedOk";
        StreamWriter myWriter = null;

        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url.ToString());
        objRequest.Method = "POST";
        objRequest.ContentLength = strPost.Length;
        objRequest.ContentType = "application/x-www-form-urlencoded";

        try
        {
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(strPost);
            myWriter.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Make sure you are connected to internet");
            return e.Message;
        }

        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();
            Console.WriteLine(result.ToString());

            // Close and clean up the StreamReader
            sr.Close();
        }
        return result;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        //mp1.Hide();
        pnlpopUp.Visible = false;
        divcal.Visible = true;
        SetInitialRow();
        DateTime dtSelectedDate = DateTime.Now;
        DateTime dtStartDate, dtEndDate;

        GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
        bdpFrom.Text = dtStartDate.ToString("dd/MM/yyyy");
        bdpTo.Text = dtEndDate.ToString("dd/MM/yyyy");
        ControlStatus();
        LoadRecordWorkingToCalendars(1);
        GV_SelectedDateLeaveView.DataSource = null;
        GV_SelectedDateLeaveView.DataBind();
    }


    protected void HL_RWT_Click(object sender, EventArgs e)
    {
        //mp1.Show();
        pnlpopUp.Visible = true;
        divcal.Visible = false;
        DateTime dtSelectedDate = DateTime.Now.Date;
        DateTime dtStartDate, dtEndDate;

        GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
        bdpFrom.Text = dtStartDate.ToString("dd/MM/yyyy");
        bdpTo.Text = dtEndDate.ToString("dd/MM/yyyy");

        LoadToGridDeatils(dtStartDate, dtEndDate);
        Load_GV_SelectedDateLeaveView(DateTime.Now.Date.ToString("dd/MM/yyyy"));
    }

    protected void drpdwnWbs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (grdRecordTime.Rows.Count > 0)
            {
                GridViewRow row1 = (sender as DropDownList).Parent.Parent as GridViewRow;

                int a = row1.RowIndex;

                foreach (GridViewRow row in grdRecordTime.Rows)
                {
                    if (row.RowIndex == a)
                    {

                        DropDownList drpdwnWbs = (DropDownList)row.FindControl("drpdwnWbs");
                        DropDownList drpdwnActtype = (DropDownList)row.FindControl("drpdwnActtype");
                        drpdwnActtype.Items.Clear();
                        TextBox txtactysrch = (TextBox)row.FindControl("txtmainSearch");
                        txtactysrch.Text = "";
                        //getactivits(txtactysrch.Text.Trim(), Session["CompCode"].ToString().ToLower().Trim(), drpdwnWbs.SelectedValue.ToString());
                        Session["WBSVal"] = drpdwnWbs.SelectedValue.Trim();

                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }



    //[System.Web.Script.Services.ScriptMethod()]
    //[System.Web.Services.WebMethod]
    //public string[] getactivits(string prefixText)
    //{
    //    masterdalDataContext objTravelRequestDataContext = new masterdalDataContext();
    //    DataTable EmplyeeDataTable = new DataTable();
    //    string[] EmplyeeNameItems = null;
    //    EmplyeeDataTable.Columns.Add("Details");

    //    foreach (var item in objTravelRequestDataContext.payc_master_load_acty("itch", prefixText.Trim(), ""))
    //    {
    //        EmplyeeDataTable.Rows.Add(item.Activity);
    //    }
    //    if (EmplyeeDataTable != null)
    //    {
    //        EmplyeeNameItems = new string[EmplyeeDataTable.Rows.Count];
    //    }
    //    if (EmplyeeDataTable != null)
    //    {
    //        int i = 0;
    //        foreach (DataRow dr in EmplyeeDataTable.Rows)
    //        {
    //            EmplyeeNameItems.SetValue(dr["Details"].ToString(), i);
    //            i++;
    //        }
    //    }
    //    return EmplyeeNameItems;
    //}


    protected void txtmainSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (grdRecordTime.Rows.Count > 0)
            {
                GridViewRow row1 = (sender as TextBox).Parent.Parent as GridViewRow;

                int a = row1.RowIndex;
                ViewState["WBSindex"] = a;
                foreach (GridViewRow row in grdRecordTime.Rows)
                {
                    if (row.RowIndex == a)
                    {

                        DropDownList drpdwnWbs = (DropDownList)row.FindControl("drpdwnWbs");
                        DropDownList drpdwnActtype = (DropDownList)row.FindControl("drpdwnActtype");
                        drpdwnActtype.Items.Clear();
                        TextBox txtactysrch = (TextBox)row.FindControl("txtmainSearch");
                        
                        //getactivits(txtactysrch.Text.Trim(), Session["CompCode"].ToString().ToLower().Trim(), drpdwnWbs.SelectedValue.ToString());
                        //configurationbl bl = new configurationbl();
                        //bl.Get_EmployeeActivity(txtactysrch.Text.Trim(), Session["CompCode"].ToString().ToLower().Trim(), drpdwnWbs.SelectedValue.ToString());

                    }
                }
            }

        }
        catch (Exception ex)
        { }
    }

    protected void lnknewAct_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["AddVal"].ToString().Trim() == "1")
            {
                if (grdRecordTime.Rows.Count > 0)
                {
                    GridViewRow row1 = (sender as LinkButton).Parent.Parent as GridViewRow;

                    int a = row1.RowIndex;

                    foreach (GridViewRow row in grdRecordTime.Rows)
                    {
                        if (row.RowIndex == a)
                        {

                            DropDownList drpdwnWbs = (DropDownList)row.FindControl("drpdwnWbs");
                            DropDownList drpdwnActtype = (DropDownList)row.FindControl("drpdwnActtype");
                            drpdwnActtype.Items.Clear();
                            TextBox txtactysrch = (TextBox)row.FindControl("txtmainSearch");
                            string TxtprefixText = Session["TxtprefixText"].ToString().Trim();

                            bool? status = false;
                            SPaycompute_BO objSpayc = new SPaycompute_BO();
                            SPayc_BL objPaycbl = new SPayc_BL();
                            objSpayc.col1 = "";
                            objSpayc.col10 = Session["WBSVal"].ToString().Trim();
                            objSpayc.col11 = "";
                            objSpayc.col12 = User.Identity.Name;
                            objSpayc.col13 = Session["CompCode"].ToString();
                            objSpayc.ID = 5;
                            objSpayc.col14 = "";
                            objSpayc.begda = DateTime.Parse("1900-01-01");
                            objSpayc.endda = DateTime.Parse("1900-01-01");
                            objSpayc.begda1 = DateTime.Parse("1900-01-01");
                            objSpayc.endda1 = DateTime.Parse("1900-01-01");
                            objSpayc.col15 = TxtprefixText;//txtactysrch.Text.ToString().Trim();
                            objPaycbl.Check_Scompo_toadd(objSpayc, ref status);

                            if (status == true)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Activity already exists for WBS');", true);
                                txtactysrch.Text = "";
                                Session["TxtprefixText"] = "";
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Activity has been added succesfully');", true);
                                txtactysrch.Text = "";
                                Session["TxtprefixText"] = "";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex) { }
    }
}


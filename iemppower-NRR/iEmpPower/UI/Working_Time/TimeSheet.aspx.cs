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

namespace iEmpPower.UI.Working_Time
{
    public partial class TimeSheet : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Form.DefaultButton = btnEntryKey.UniqueID;

            lblMessageBoard.Text = "";
            if (!IsPostBack)
            {
                //Creating thr grid controls
                SetInitialRow();

                //Fetching the current week from current date
                DateTime dtSelectedDate = DateTime.Now;
                DateTime dtStartDate, dtEndDate;

                GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
                bdpFrom.SelectedDate = dtStartDate;
                bdpTo.SelectedDate = dtEndDate;
                ControlStatus();
                Session.Add("sStatus", null);

            }
            Calendar1.SelectionChanged += new EventHandler(Calendar1_SelectionChanged);
            Calendar2.SelectionChanged += new EventHandler(Calendar2_SelectionChanged);
            Calendar3.SelectionChanged += new EventHandler(Calendar3_SelectionChanged);
            //Loading the Calendar dates from database 
            LoadRecordWorkingToCalendars();
        }
        //This method will display the next month calendar
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Calendar1.VisibleDate = Calendar2.VisibleDate;
            Calendar2.VisibleDate = Calendar3.VisibleDate;
            Calendar3.VisibleDate = Calendar3.VisibleDate.AddMonths(1);
        }
        //This method will display the previous month calendar
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Calendar3.VisibleDate = Calendar2.VisibleDate;
            Calendar2.VisibleDate = Calendar1.VisibleDate;
            Calendar1.VisibleDate = Calendar1.VisibleDate.AddMonths(-1);
        }
        //This method will display the dates in gridview from selected week 
        public void datesetting()
        {
            grdRecordTime.HeaderRow.Cells[5].Text = "SUN ," + bdpFrom.SelectedDate.AddDays(0).ToString("d-MMM-yyyy");
            grdRecordTime.HeaderRow.Cells[6].Text = "MON ," + bdpFrom.SelectedDate.AddDays(1).ToString("d-MMM-yyyy");
            grdRecordTime.HeaderRow.Cells[7].Text = "TUE ," + bdpFrom.SelectedDate.AddDays(2).ToString("d-MMM-yyyy");
            grdRecordTime.HeaderRow.Cells[8].Text = "WED ," + bdpFrom.SelectedDate.AddDays(3).ToString("d-MMM-yyyy");
            grdRecordTime.HeaderRow.Cells[9].Text = "THU ," + bdpFrom.SelectedDate.AddDays(4).ToString("d-MMM-yyyy");
            grdRecordTime.HeaderRow.Cells[10].Text = "FRI ," + bdpFrom.SelectedDate.AddDays(5).ToString("d-MMM-yyyy");
            grdRecordTime.HeaderRow.Cells[11].Text = "SAT ," + bdpFrom.SelectedDate.AddDays(6).ToString("d-MMM-yyyy");
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
            DropDownList ddl2 = (DropDownList)grdRecordTime.Rows[0].FindControl("drpdwnCostCenter");
            LoadCostCenter(ddl2);
            DropDownList ddl3 = (DropDownList)grdRecordTime.Rows[0].FindControl("drpdwnOrder");
            LoadOrder(ddl3);
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
            mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Order();
            ddl.DataSource = objLst;
            ddl.DataTextField = "ORDER_TEXT";
            ddl.DataValueField = "AUFNR";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(" ", "0"));
            Session.Add("OrderLst", objLst);
        }
        protected void LoadAttabsTypes(DropDownList ddl)
        {
            mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Attendence_abs_Types(User.Identity.Name.ToString());
            ddl.DataSource = objLst;
            ddl.DataTextField = "ATEXT";
            ddl.DataValueField = "AWART";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(" ", "0"));
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

                            DropDownList ddlCostCenter = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnCostCenter");

                            dtCurrentTable.Rows[i]["CostCenter"] = ddlCostCenter.SelectedValue.ToString();

                            //extract the DropDownList Selected Items 
                            DropDownList ddlOrder = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnOrder");

                            dtCurrentTable.Rows[i]["Order"] = ddlOrder.SelectedValue.ToString();
                            //extract the DropDownList Selected Items 
                            DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnAttabsType");

                            // Update the DataRow with the DDL Selected Items 
                            dtCurrentTable.Rows[i]["AttTypes"] = ddl1.SelectedValue.ToString();

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
        //Setting the previuos rows values
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

                        Label lblHours = ((Label)grdRecordTime.FooterRow.FindControl("lblHours"));
                        Label lblSun = ((Label)grdRecordTime.FooterRow.FindControl("lblSun"));
                        Label lblMon = ((Label)grdRecordTime.FooterRow.FindControl("lblMon"));
                        Label lblTues = ((Label)grdRecordTime.FooterRow.FindControl("lblTues"));
                        Label lblWed = ((Label)grdRecordTime.FooterRow.FindControl("lblWed"));
                        Label lblThu = ((Label)grdRecordTime.FooterRow.FindControl("lblThu"));
                        Label lblFri = ((Label)grdRecordTime.FooterRow.FindControl("lblFri"));
                        Label lblSAt = ((Label)grdRecordTime.FooterRow.FindControl("lblSAt"));

                        DropDownList ddlCostCenter = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnCostCenter");
                        ddlCostCenter.Controls.Clear();
                        LoadCostCenter(ddlCostCenter);
                        // dtCurrentTable.Rows[i]["CostCenter"] = ddlCostCenter.SelectedValue.ToString();

                        //extract the DropDownList Selected Items 
                        DropDownList ddlOrder = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnOrder");
                        ddlOrder.Controls.Clear();
                        LoadOrder(ddlOrder);
                        // dtCurrentTable.Rows[i]["Order"] = ddlOrder.SelectedValue.ToString();

                        DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnAttabsType");

                        //Fill the DropDownList with Data 
                        ddl1.Controls.Clear();
                        LoadAttabsTypes(ddl1);

                        if (i < dt.Rows.Count - 1)
                        {

                            //Assign the value from DataTable to the TextBox 
                            //boxBillable.Text = dt.Rows[i]["Column2"].ToString();
                            //boxDeliveryType.Text = dt.Rows[i]["Column3"].ToString();
                            //boxCountry.Text = dt.Rows[i]["Column4"].ToString();
                            //lblCostCenter.Text = dt.Rows[i]["Column13"].ToString();
                            //lblOrder.Text = dt.Rows[i]["Column14"].ToString();
                            boxStaffGrade.Text = dt.Rows[i]["Staff"].ToString();
                            boxTotal.Text = dt.Rows[i]["Total"].ToString();
                            boxSUN.Text = dt.Rows[i]["Sun"].ToString();
                            boxMON.Text = dt.Rows[i]["Mon"].ToString();
                            boxTUE.Text = dt.Rows[i]["Tue"].ToString();
                            boxWED.Text = dt.Rows[i]["Wed"].ToString();
                            boxTHU.Text = dt.Rows[i]["Thur"].ToString();
                            boxFRI.Text = dt.Rows[i]["Fri"].ToString();
                            boxSAT.Text = dt.Rows[i]["Sat"].ToString();

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
                            } if (boxWED.Text.Trim() != "")
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

                            ddlCostCenter.ClearSelection();
                            if (dt.Rows[i]["CostCenter"].ToString() == null)
                            {
                                ddlCostCenter.Items.FindByText("Select").Selected = true;
                            }
                            else
                            {
                                ddlCostCenter.Items.FindByValue(dt.Rows[i]["CostCenter"].ToString()).Selected = true;
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
        protected void LoadRecordWorkingToCalendars()
        {
            DateTime dtFormGivenDate = Calendar1.VisibleDate.Date;
            DateTime dtToGivenDate = Calendar3.VisibleDate.Date;
            DateTime dtTodate, dtFromdate;

            GetFirstInMonth(dtFormGivenDate, out dtFromdate);
            LastDayOfMonth(dtToGivenDate, out dtTodate);
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            wtrecordworkingtimebl objBl = new wtrecordworkingtimebl();
            wtrecordworkingtimecollectionbo objLst = new wtrecordworkingtimecollectionbo();
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
            objleaveLst = objleaveBl.Get_Calendar_Leave_Markings(objleaveBo);
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
                if (e.Day.Date == DateTime.Parse(objBo.WORKING_DATE))
                {
                    e.Cell.BackColor = System.Drawing.Color.Blue;
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
                                    e.Cell.BackColor = System.Drawing.Color.Red;
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
                if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")
                {
                    e.Cell.BackColor = System.Drawing.Color.Green;
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
                if (e.Day.Date == DateTime.Parse(objBo.WORKING_DATE))
                {
                    e.Cell.BackColor = System.Drawing.Color.Blue;
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
                                    e.Cell.BackColor = System.Drawing.Color.Red;
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
                if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")
                {
                    e.Cell.BackColor = System.Drawing.Color.Green;
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
                if (e.Day.Date == DateTime.Parse(objBo.WORKING_DATE))
                {
                    e.Cell.BackColor = System.Drawing.Color.Blue;
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
                                    e.Cell.BackColor = System.Drawing.Color.Red;
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
                if (e.Day.Date == (objBo.DATUM) && objBo.STATUS.ToUpper() == "APPROVED")
                {
                    e.Cell.BackColor = System.Drawing.Color.Green;
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
            DropDownList ddlCostCenter = (DropDownList)grdRecordTime.Rows[i - 1].FindControl("drpdwnCostCenter");
            SetFocus(ddlCostCenter);
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

                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex <= dt.Rows.Count)
                    {
                        //Remove the Selected Row data
                        dt.Rows.Remove(dt.Rows[rowID]);
                        //ResetRowID(dt);

                    }
                }
                //Store the current data in ViewState for future reference
                ViewState["CurrentTable"] = dt;

                grdRecordTime.DataSource = dt;
                grdRecordTime.DataBind();
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
                        // Label lbl1 = (Label)grdRecordTime.Rows[i].Cells[7].FindControl("lblmon");
                        Label lblHours = ((Label)grdRecordTime.FooterRow.FindControl("lblHours"));
                        Label lblSun = ((Label)grdRecordTime.FooterRow.FindControl("lblSun"));
                        Label lblMon = ((Label)grdRecordTime.FooterRow.FindControl("lblMon"));
                        Label lblTues = ((Label)grdRecordTime.FooterRow.FindControl("lblTues"));
                        Label lblWed = ((Label)grdRecordTime.FooterRow.FindControl("lblWed"));
                        Label lblThu = ((Label)grdRecordTime.FooterRow.FindControl("lblThu"));
                        Label lblFri = ((Label)grdRecordTime.FooterRow.FindControl("lblFri"));
                        Label lblSAt = ((Label)grdRecordTime.FooterRow.FindControl("lblSAt"));

                        DropDownList ddlCostCenter = (DropDownList)grdRecordTime.Rows[rowIndex].FindControl("drpdwnCostCenter");

                        LoadCostCenter(ddlCostCenter);

                        //extract the DropDownList Selected Items 
                        DropDownList ddlOrder = (DropDownList)grdRecordTime.Rows[rowIndex].FindControl("drpdwnOrder");
                        LoadOrder(ddlOrder);

                        DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[rowIndex].FindControl("drpdwnAttabsType");

                        //Fill the DropDownList with Data 
                        LoadAttabsTypes(ddl1);

                        if (i < dt.Rows.Count)
                        {

                            //Assign the value from DataTable to the TextBox 

                            //lblCostCenter.Text = dt.Rows[i]["Column13"].ToString();
                            //lblOrder.Text = dt.Rows[i]["Column14"].ToString();
                            boxStaffGrade.Text = dt.Rows[i]["Staff"].ToString();
                            boxTotal.Text = dt.Rows[i]["Total"].ToString();
                            boxSUN.Text = dt.Rows[i]["sun"].ToString();
                            boxMON.Text = dt.Rows[i]["Mon"].ToString();
                            boxTUE.Text = dt.Rows[i]["Tue"].ToString();
                            boxWED.Text = dt.Rows[i]["Wed"].ToString();
                            boxTHU.Text = dt.Rows[i]["Thur"].ToString();
                            boxFRI.Text = dt.Rows[i]["Fri"].ToString();
                            boxSAT.Text = dt.Rows[i]["Sat"].ToString();

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
                            //Set the Previous Selected Items on Each DropDownList  on Postbacks 

                            if (i == 0)
                            {
                                ddl1.ClearSelection();
                                ddl1.Items.FindByValue(dt.Rows[i]["AttTypes"].ToString()).Selected = true;

                                if (dt.Rows[i]["CostCenter"].ToString().Length > 0)
                                {
                                    ddlCostCenter.ClearSelection();
                                    ddlCostCenter.Items.FindByValue(dt.Rows[i]["CostCenter"].ToString()).Selected = true;
                                }

                                if (dt.Rows[i]["Order"].ToString().Length > 0)
                                {
                                    ddlOrder.ClearSelection();
                                    ddlOrder.Items.FindByValue(dt.Rows[i]["Order"].ToString()).Selected = true;
                                }
                                sAttendencetype = ddl1.SelectedValue.Trim();
                                sCostCenter = ddlCostCenter.SelectedItem.Text.ToString();
                                sOrder = ddlOrder.SelectedItem.Text.ToString();
                                //sBillableStatus = boxBillable.Text.Trim();
                                //sDeliverytype = boxDeliveryType.Text.Trim();
                                //scountry = boxCountry.Text.Trim();
                                sstaffgrade = boxStaffGrade.Text.Trim();
                                sTotal = boxTotal.Text.Trim();

                                if (boxSUN.Text.Trim() != "")
                                {
                                    if (sCount == "")
                                    {
                                        sAttendencetype = ddl1.SelectedValue.Trim();
                                        sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                        sOrder = ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = boxSUN.Text.Trim();
                                        sWorkingDate = bdpFrom.SelectedDate.ToString("d-MMM-yyyy");
                                        sCount = "0";
                                    }
                                    else
                                    {
                                        sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                        sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                        sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = sLeaveHours + "|" + boxSUN.Text.Trim();
                                        sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.ToString("d-MMM-yyyy");
                                    }
                                }

                                if (boxMON.Text.Trim() != "")
                                {
                                    if (sCount == "")
                                    {
                                        sAttendencetype = ddl1.SelectedValue.Trim();
                                        sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                        sOrder = ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = boxMON.Text.Trim();
                                        sWorkingDate = bdpFrom.SelectedDate.AddDays(1).ToString("d-MMM-yyyy");
                                        sCount = "0";
                                    }
                                    else
                                    {
                                        sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                        sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                        sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = sLeaveHours + "|" + boxMON.Text.Trim();
                                        sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(1).ToString("d-MMM-yyyy");
                                    }
                                }
                                if (boxTUE.Text.Trim() != "")
                                {
                                    if (sCount == "")
                                    {
                                        sAttendencetype = ddl1.SelectedValue.Trim();
                                        sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                        sOrder = ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = boxTUE.Text.Trim();
                                        sWorkingDate = bdpFrom.SelectedDate.AddDays(2).ToString("d-MMM-yyyy");
                                        sCount = "0";
                                    }
                                    else
                                    {
                                        sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                        sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                        sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = sLeaveHours + "|" + boxTUE.Text.Trim();
                                        sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(2).ToString("d-MMM-yyyy");
                                    }
                                }
                                if (boxWED.Text.Trim() != "")
                                {
                                    if (sCount == "")
                                    {
                                        sAttendencetype = ddl1.SelectedValue.Trim();
                                        sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                        sOrder = ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = boxWED.Text.Trim();
                                        sWorkingDate = bdpFrom.SelectedDate.AddDays(3).ToString("d-MMM-yyyy");
                                        sCount = "0";
                                    }
                                    else
                                    {
                                        sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                        sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                        sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = sLeaveHours + "|" + boxWED.Text.Trim();
                                        sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(3).ToString("d-MMM-yyyy");
                                    }
                                }
                                if (boxTHU.Text.Trim() != "")
                                {
                                    if (sCount == "")
                                    {
                                        sAttendencetype = ddl1.SelectedValue.Trim();
                                        sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                        sOrder = ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = boxTHU.Text.Trim();
                                        sWorkingDate = bdpFrom.SelectedDate.AddDays(4).ToString("d-MMM-yyyy");
                                        sCount = "0";
                                    }
                                    else
                                    {
                                        sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                        sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                        sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = sLeaveHours + "|" + boxTHU.Text.Trim();
                                        sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(4).ToString("d-MMM-yyyy");
                                    }
                                }
                                if (boxFRI.Text.Trim() != "")
                                {
                                    if (sCount == "")
                                    {
                                        sAttendencetype = ddl1.SelectedValue.Trim();
                                        sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                        sOrder = ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = boxFRI.Text.Trim();
                                        sWorkingDate = bdpFrom.SelectedDate.AddDays(5).ToString("d-MMM-yyyy");
                                        sCount = "0";
                                    }
                                    else
                                    {
                                        sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                        sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                        sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = sLeaveHours + "|" + boxFRI.Text.Trim();
                                        sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(5).ToString("d-MMM-yyyy");
                                    }
                                }
                                if (boxSAT.Text.Trim() != "")
                                {
                                    if (sCount == "")
                                    {
                                        sAttendencetype = ddl1.SelectedValue.Trim();
                                        sCostCenter = ddlCostCenter.SelectedValue.ToString();
                                        sOrder = ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = boxSAT.Text.Trim();
                                        sWorkingDate = bdpFrom.SelectedDate.AddDays(6).ToString("d-MMM-yyyy");
                                        sCount = "0";
                                    }
                                    else
                                    {
                                        sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                        sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                        sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                        sLeaveHours = sLeaveHours + "|" + boxSAT.Text.Trim();
                                        sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(6).ToString("d-MMM-yyyy");
                                    }
                                }
                            }
                            else
                            {
                                ddl1.ClearSelection();
                                ddl1.Items.FindByValue(dt.Rows[i]["AttTypes"].ToString()).Selected = true;

                                if (dt.Rows[i]["CostCenter"].ToString().Length > 0)
                                {
                                    ddlCostCenter.ClearSelection();
                                    ddlCostCenter.Items.FindByValue(dt.Rows[i]["CostCenter"].ToString()).Selected = true;
                                }

                                if (dt.Rows[i]["Order"].ToString().Length > 0)
                                {
                                    ddlOrder.ClearSelection();
                                    ddlOrder.Items.FindByValue(dt.Rows[i]["Order"].ToString()).Selected = true;
                                }

                                sstaffgrade = sstaffgrade + "|" + boxStaffGrade.Text.Trim();
                                sTotal = sTotal + "|" + boxTotal.Text.Trim();

                                if (boxSUN.Text.Trim() != "")
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxSUN.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.ToString("d-MMM-yyyy");
                                }

                                if (boxMON.Text.Trim() != "")
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxMON.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(1).ToString("d-MMM-yyyy");
                                }
                                if (boxTUE.Text.Trim() != "")
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxTUE.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(2).ToString("d-MMM-yyyy");
                                }
                                if (boxWED.Text.Trim() != "")
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxWED.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(3).ToString("d-MMM-yyyy");
                                }
                                if (boxTHU.Text.Trim() != "")
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxTHU.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(4).ToString("d-MMM-yyyy");
                                }
                                if (boxFRI.Text.Trim() != "")
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxFRI.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(5).ToString("d-MMM-yyyy");
                                }
                                if (boxSAT.Text.Trim() != "")
                                {
                                    sAttendencetype = sAttendencetype + "|" + ddl1.SelectedValue.Trim();
                                    sCostCenter = sCostCenter + "|" + ddlCostCenter.SelectedValue.ToString();
                                    sOrder = sOrder + "|" + ddlOrder.SelectedValue.ToString();
                                    sLeaveHours = sLeaveHours + "|" + boxSAT.Text.Trim();
                                    sWorkingDate = sWorkingDate + "|" + bdpFrom.SelectedDate.AddDays(6).ToString("d-MMM-yyyy");
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
            bStatus = true;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    //add new row to DataTable 
                    //dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState for future reference 

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


                        //extract the DropDownList Selected Items 
                        DropDownList ddlCostCenter = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnCostCenter");

                        dtCurrentTable.Rows[i]["CostCenter"] = ddlCostCenter.SelectedValue.ToString();

                        //extract the DropDownList Selected Items 
                        DropDownList ddlOrder = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnOrder");

                        dtCurrentTable.Rows[i]["Order"] = ddlOrder.SelectedValue.ToString();

                        DropDownList ddl1 = (DropDownList)grdRecordTime.Rows[i].FindControl("drpdwnAttabsType");

                        // Update the DataRow with the DDL Selected Items 

                        dtCurrentTable.Rows[i]["AttTypes"] = ddl1.SelectedValue.ToString();


                        if ((string)Session["sStatus"] == "Review")
                        {

                            leaverequestcollectionbo objLeaveLst = (leaverequestcollectionbo)Session["objleaveLst"];
                            var objLst = from col in objLeaveLst
                                         where col.STATUS == "APPROVED"
                                         select col;
                            foreach (leaverequestbo obj in objLeaveLst)
                            {
                                if (boxSUN.Text != "")
                                {

                                    if (obj.DATUM.ToString() == bdpFrom.SelectedDate.ToString() && obj.AWART != ddl1.SelectedValue)
                                    {
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        lblMessageBoard.Text = "Please select " + obj.ATEXT + " for the date " + bdpFrom.SelectedDate.AddDays(1).ToString("d-MMM-yyyy");
                                        bStatus = false;
                                        return;
                                    }
                                }
                                if (boxMON.Text != "")
                                {

                                    if (obj.DATUM.ToString() == bdpFrom.SelectedDate.AddDays(1).ToString() && obj.AWART != ddl1.SelectedValue)
                                    {
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        lblMessageBoard.Text = "Please select " + obj.ATEXT + " for the date " + bdpFrom.SelectedDate.AddDays(1).ToString("d-MMM-yyyy");
                                        bStatus = false;
                                        return;
                                    }
                                }
                                if (boxTUE.Text != "")
                                {

                                    if (obj.DATUM.ToString() == bdpFrom.SelectedDate.AddDays(2).ToString() && obj.AWART != ddl1.SelectedValue)
                                    {
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        lblMessageBoard.Text = "Please select " + obj.ATEXT + " for the date " + bdpFrom.SelectedDate.AddDays(2).ToString("d-MMM-yyyy");
                                        bStatus = false;
                                        return;
                                    }
                                }
                                if (boxWED.Text != "")
                                {

                                    if (obj.DATUM.ToString() == bdpFrom.SelectedDate.AddDays(3).ToString() && obj.AWART != ddl1.SelectedValue)
                                    {
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        lblMessageBoard.Text = "Please select " + obj.ATEXT + " for the date " + bdpFrom.SelectedDate.AddDays(3).ToString("d-MMM-yyyy");
                                        bStatus = false;
                                        return;
                                    }
                                }
                                if (boxTHU.Text != "")
                                {

                                    if (obj.DATUM.ToString() == bdpFrom.SelectedDate.AddDays(4).ToString() && obj.AWART != ddl1.SelectedValue)
                                    {
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        lblMessageBoard.Text = "Please select " + obj.ATEXT + " for the date " + bdpFrom.SelectedDate.AddDays(4).ToString("d-MMM-yyyy");
                                        bStatus = false;
                                        return;
                                    }
                                }
                                if (boxFRI.Text != "")
                                {

                                    if (obj.DATUM.ToString() == bdpFrom.SelectedDate.AddDays(5).ToString() && obj.AWART != ddl1.SelectedValue)
                                    {
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        lblMessageBoard.Text = "Please select " + obj.ATEXT + " for the date " + bdpFrom.SelectedDate.AddDays(5).ToString("d-MMM-yyyy");
                                        bStatus = false;
                                        return;
                                    }
                                }
                                if (boxSAT.Text != "")
                                {

                                    if (obj.DATUM.ToString() == bdpFrom.SelectedDate.AddDays(6).ToString() && obj.AWART != ddl1.SelectedValue)
                                    {
                                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                                        lblMessageBoard.Text = "Please select " + obj.ATEXT + " for the date " + bdpFrom.SelectedDate.AddDays(6).ToString("d-MMM-yyyy");
                                        bStatus = false;
                                        return;
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
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
                return;
            }
        }
        protected void LoadSaveUpdate()
        {
            bool blSendMail = true;
            bool? SuperVisorstatus = true;
            bool? HRStatus = true;
            string Pernr = "";
            string SuperVisorPernr = "";
            string HRPernr = "";
            string PernrEmail = "";
            string SuperVisorEmail = "";
            string HREmail = "";
            string ErrorMessage = "";
            string HRPhn = "";
            string SuperVisorPhn = "";

            Session.Add("sStatus", null);
            bool IsSave = (bool)Session["IsSave"];

            SaveAndRemoveRowToGrids();
            wtrecordworkingtimebo objRecordWrkTimeBo = new wtrecordworkingtimebo();
            wtrecordworkingtimebl objRecordWrkTimeBl = new wtrecordworkingtimebl();
            objRecordWrkTimeBo.PERNR = User.Identity.Name;
            objRecordWrkTimeBo.AWART = sAttendencetype;
            objRecordWrkTimeBo.COST_CENTER = sCostCenter;
            objRecordWrkTimeBo.ORDER = sOrder;
            objRecordWrkTimeBo.CATSHOURS = sLeaveHours;
            objRecordWrkTimeBo.WORKING_DATE = sWorkingDate;
            string sModuleName = "Record working is changed";
            string strMailToList = string.Empty;
            if (IsSave)
            {
                int iResultCode = objRecordWrkTimeBl.Create_RecordWorkingTime(objRecordWrkTimeBo, ref  SuperVisorstatus, ref HRStatus, ref  SuperVisorPernr,
                                       ref  SuperVisorEmail, ref  HRPernr, ref  HREmail, ref Pernr, ref  PernrEmail, ref  ErrorMessage, ref SuperVisorPhn, ref HRPhn);
                if (iResultCode == 1)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = ErrorMessage;
                    return;
                }
                if (iResultCode == 0)
                {
                    string strSMS = PrepareSMSBody();

                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    if (SuperVisorstatus == true)
                    {
                        blSendMail = true;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = GetLocalResourceObject("ApprovalSuccess").ToString() + " " + SuperVisorPernr + ".";

                        //try
                        //{
                        //    if (SuperVisorPhn != "" && SuperVisorPhn.Length > 0)
                        //    {
                        //        WebClient client = new WebClient();
                        //        string baseurl = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=itchamps&password=387485442&sendername=iEmpPowe&mobileno=91" + SuperVisorPhn + "&message=" + strSMS;
                        //        Stream data = client.OpenRead(baseurl);
                        //        StreamReader reader = new StreamReader(data);
                        //        string s = reader.ReadToEnd();
                        //        data.Close();
                        //        reader.Close();
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    ex.Message.ToString();
                        //}
                    }
                    if (HRStatus == true)
                    {
                        blSendMail = true;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = GetLocalResourceObject("HRSuccess").ToString() + HRPernr + ".";

                    //    try
                    //    {
                    //        if (HRPhn != null && HRPhn.Length > 0)
                    //        {
                    //            WebClient client = new WebClient();
                    //            string baseurl = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=itchamps&password=387485442&sendername=iEmpPowe&mobileno=91" + HRPhn + "&message=" + strSMS;
                    //            Stream data = client.OpenRead(baseurl);
                    //            StreamReader reader = new StreamReader(data);
                    //            string s = reader.ReadToEnd();
                    //            data.Close();
                    //            reader.Close();
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ex.Message.ToString();
                    //    }
                    }
                    if (SuperVisorstatus == true && HRStatus == true)
                    {
                        blSendMail = true;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = GetLocalResourceObject("ApprovalSuccess").ToString() + " " + SuperVisorPernr + " and HR admin " + HRPernr + ".";

                    }
                    if (SuperVisorstatus == false && HRStatus == false)
                    {
                        blSendMail = false;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = GetLocalResourceObject("SaveSuccess").ToString();
                    }

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

                int iResultCode = objRecordWrkTimeBl.Update_RecordWorkingTime(objRecordWrkTimeBo, ref  SuperVisorstatus, ref HRStatus, ref  SuperVisorPernr,
                                       ref  SuperVisorEmail, ref  HRPernr, ref  HREmail, ref Pernr, ref  PernrEmail, ref  ErrorMessage, ref SuperVisorPhn, ref HRPhn);

                if (iResultCode == 1)
                {
                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = ErrorMessage;
                    return;
                }
                if (iResultCode == 0)
                {
                    string strSMS = PrepareSMSBody();

                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;

                    if (SuperVisorstatus == true)
                    {
                        blSendMail = true;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = GetLocalResourceObject("ApprovalSuccess").ToString() + " " + SuperVisorPernr + ".";

                        //try
                        //{
                        //    if (SuperVisorPhn != null && SuperVisorPhn.Length > 0)
                        //    {
                        //        WebClient client = new WebClient();
                        //        string baseurl = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=itchamps&password=387485442&sendername=iEmpPowe&mobileno=91" + SuperVisorPhn + "&message=" + strSMS;
                        //        Stream data = client.OpenRead(baseurl);
                        //        StreamReader reader = new StreamReader(data);
                        //        string s = reader.ReadToEnd();
                        //        data.Close();
                        //        reader.Close();
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    ex.Message.ToString();
                        //}
                    }
                    if (HRStatus == true)
                    {
                        blSendMail = true;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = GetLocalResourceObject("HRSuccess").ToString() + HRPernr + ".";

                        //try
                        //{
                        //    if (HRPhn != null && HRPhn.Length > 0)
                        //    {
                        //        WebClient client = new WebClient();
                        //        string baseurl = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=itchamps&password=387485442&sendername=iEmpPowe&mobileno=91" + HRPhn + "&message=" + strSMS;
                        //        Stream data = client.OpenRead(baseurl);
                        //        StreamReader reader = new StreamReader(data);
                        //        string s = reader.ReadToEnd();
                        //        data.Close();
                        //        reader.Close();
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    ex.Message.ToString();
                        //}
                    }
                    if (SuperVisorstatus == true && HRStatus == true)
                    {
                        blSendMail = true;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = GetLocalResourceObject("ApprovalSuccess").ToString() + " " + SuperVisorPernr + " and HR admin " + HRPernr + ".";
                    }
                    if (SuperVisorstatus == false && HRStatus == false)
                    {
                        blSendMail = false;
                        lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        lblMessageBoard.Text = GetLocalResourceObject("UpdateSuccess").ToString();
                    }
                }
            }
            #region SendMail
            if (blSendMail)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DirectoryInfo dirPath = new DirectoryInfo(@"I:\iEmpPower_Tempfiles\");
                if (!Directory.Exists(dirPath.ToString()))
                {
                    Directory.CreateDirectory(dirPath.ToString());
                }
                DirectoryInfo dirChildPath = new DirectoryInfo(dirPath + DateTime.Now.Date.ToString("dd-MMM-yyyy"));
                if (!Directory.Exists(dirChildPath.ToString()))
                {
                    Directory.CreateDirectory(dirChildPath.ToString());
                }
                String fileName = dirChildPath + "\\" + "WRTime-" + (User.Identity.Name + "-" + DateTime.Now.ToString("HH:mm:ss").Replace(':', '-') + ".xls");

                if (File.Exists(fileName))
                {
                    File.Create(fileName).Close();

                }
                CreateExcelFile(dtCurrentTable, fileName);
                if (SuperVisorstatus == true)
                {
                    if (SuperVisorEmail != string.Empty && SuperVisorEmail != "" && SuperVisorEmail != null)
                    {
                        strMailToList = SuperVisorEmail;
                    }
                }
                if (HRStatus == true)
                {
                    if (HREmail != string.Empty && HREmail != "" && HREmail != null)
                    {
                        if (strMailToList.Length == 0)
                        {
                            strMailToList = HREmail;
                        }
                        else
                        {
                            strMailToList = strMailToList + "," + HREmail;
                        }
                    }
                }
                string strBodyMsg = "Plesae find file attached";
                string strEmpName = Session["EmployeeName"].ToString();
                string strSubject = string.Empty;

                if (strEmpName != null && strEmpName.Length > 0 && strEmpName != "")
                {
                    strSubject = "Recordworking time request by " + strEmpName;
                }
                else
                {
                    strSubject = "Recordworking time request";
                }
                if (strMailToList.Length > 0 && PernrEmail.Length > 0)
                {
                    iEmpPowerMaster_Load.masterbl.DispatchMailWithAttachment(strMailToList, User.Identity.Name, strSubject, PernrEmail, strBodyMsg, fileName);
                }
            }
            #endregion
            SetInitialRow();
            ControlStatus();
            LoadRecordWorkingToCalendars();
        }
        protected void btnNextWeek_Click(object sender, EventArgs e)
        {
            DateTime dtSelectedDate = bdpFrom.SelectedDate;
            DateTime dtStartDate, dtEndDate;

            GetNextWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.SelectedDate = dtStartDate;
            bdpTo.SelectedDate = dtEndDate;

            datesetting();

        }
        protected void btnPreviousWeek_Click(object sender, EventArgs e)
        {
            DateTime dtSelectedDate = bdpFrom.SelectedDate;
            DateTime dtStartDate, dtEndDate;

            GetPreviousWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.SelectedDate = dtStartDate;
            bdpTo.SelectedDate = dtEndDate;

            datesetting();

        }
        protected void btnReview_Click(object sender, EventArgs e)
        {
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

                    Label lblHours = ((Label)grdRecordTime.FooterRow.FindControl("lblHours"));
                    Label lblSun = ((Label)grdRecordTime.FooterRow.FindControl("lblSun"));
                    Label lblMon = ((Label)grdRecordTime.FooterRow.FindControl("lblMon"));
                    Label lblTues = ((Label)grdRecordTime.FooterRow.FindControl("lblTues"));
                    Label lblWed = ((Label)grdRecordTime.FooterRow.FindControl("lblWed"));
                    Label lblThu = ((Label)grdRecordTime.FooterRow.FindControl("lblThu"));
                    Label lblFri = ((Label)grdRecordTime.FooterRow.FindControl("lblFri"));
                    Label lblSAt = ((Label)grdRecordTime.FooterRow.FindControl("lblSAt"));


                    //Assign the value from DataTable to the TextBox 

                    boxTotal.Text = dt.Rows[i]["Total"].ToString();
                    boxSUN.Text = dt.Rows[i]["Sun"].ToString();
                    boxMON.Text = dt.Rows[i]["Mon"].ToString();
                    boxTUE.Text = dt.Rows[i]["Tue"].ToString();
                    boxWED.Text = dt.Rows[i]["Wed"].ToString();
                    boxTHU.Text = dt.Rows[i]["Thur"].ToString();
                    boxFRI.Text = dt.Rows[i]["Fri"].ToString();
                    boxSAT.Text = dt.Rows[i]["Sat"].ToString();

                    if (boxSUN.Text.Trim() != "")
                    {
                        sSun = decimal.Parse(boxSUN.Text) + sSun;
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
                    if (iRowTotalHrs == 0)
                    {
                        lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                        lblMessageBoard.Text = GetLocalResourceObject("AtLeastOne").ToString();
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
                btnSave.Visible = true;
                btnExit.Visible = true;
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
            Response.Redirect("~/Default.aspx");
        }
        protected void btnPreviousStep_Click(object sender, EventArgs e)
        {
            string PrevBtnCntrl = (string)Session["sPrevBtnCntrl"];
            btnExit.Visible = false;
            btnPreviousStep.Enabled = false;
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
                wtrecordworkingtimebo objHoursBo = objLst.Find(delegate(wtrecordworkingtimebo obj)
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

                //GridViewRow row;
                //TableCell cell, cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, cell13, cell14;
                //row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

                //cell = new TableCell();
                //cell.Text = "";
                //cell.HorizontalAlign = HorizontalAlign.Center;
                //cell.Font.Bold = true;
                //cell.BorderColor = System.Drawing.Color.Maroon;
                //row.Cells.Add(cell);

                //cell1 = new TableCell();
                //cell1.Text = "";
                //cell1.HorizontalAlign = HorizontalAlign.Center;
                //cell1.Font.Bold = true;
                //cell1.BorderColor = System.Drawing.Color.Maroon;
                //row.Cells.Add(cell1);

                //cell2 = new TableCell();
                //cell2.Text = "";
                //cell2.HorizontalAlign = HorizontalAlign.Center;
                //cell2.Font.Bold = true;
                //cell2.BorderColor = System.Drawing.Color.Maroon;
                //row.Cells.Add(cell2);



                //cell5 = new TableCell();
                //// cell4.ColumnSpan = 2;
                //cell5.Text = "Plan";
                //cell5.HorizontalAlign = HorizontalAlign.Center;
                //cell5.BorderColor = System.Drawing.Color.Maroon;
                //cell5.Font.Bold = true;
                //row.Cells.Add(cell5);

                //cell6 = new TableCell();
                //// cell1.ColumnSpan = 3;
                //cell6.Text = ((decimal.Parse(objBo.ARBST) * 5) + 4).ToString();
                //cell6.HorizontalAlign = HorizontalAlign.Center;
                //cell6.Font.Bold = true;
                //cell6.BorderColor = System.Drawing.Color.Maroon;
                //row.Cells.Add(cell6);

                //cell7 = new TableCell();
                //// cell1.ColumnSpan = 3;
                //cell7.Text = "0";
                //cell7.HorizontalAlign = HorizontalAlign.Center;
                //cell7.Font.Bold = true;
                //cell7.BorderColor = System.Drawing.Color.Maroon;
                //row.Cells.Add(cell7);

                //cell8 = new TableCell();
                //// cell1.ColumnSpan = 3;
                //cell8.Text = objBo.ARBST;
                //cell8.HorizontalAlign = HorizontalAlign.Center;
                //cell8.Font.Bold = true;
                //cell8.BorderColor = System.Drawing.Color.Maroon;
                //row.Cells.Add(cell8);

                //cell9 = new TableCell();
                //// cell1.ColumnSpan = 3;
                //cell9.Text = objBo.ARBST;
                //cell9.HorizontalAlign = HorizontalAlign.Center;
                //cell9.Font.Bold = true;
                //cell9.BorderColor = System.Drawing.Color.Maroon;
                //row.Cells.Add(cell9);

                //cell10 = new TableCell();
                //// cell1.ColumnSpan = 3;
                //cell10.Text = objBo.ARBST;
                //cell10.HorizontalAlign = HorizontalAlign.Center;
                ////cell9.Font.Bold = true;
                ////cell9.BorderColor = System.Drawing.Color.Maroon;
                //row.Cells.Add(cell10);

                //cell11 = new TableCell();
                ////   cell1.ColumnSpan = 3;
                //cell11.Text = objBo.ARBST;
                //cell11.HorizontalAlign = HorizontalAlign.Center;
                ////cell10.Font.Bold = true;
                ////cell10.BorderColor = System.Drawing.Color.Maroon;
                //row.Cells.Add(cell11);

                //cell12 = new TableCell();
                //// cell1.ColumnSpan = 3;
                //cell12.Text = objBo.ARBST;
                //cell12.HorizontalAlign = HorizontalAlign.Center;
                ////cell11.Font.Bold = true;
                ////cell11.BorderColor = System.Drawing.Color.Maroon;
                //row.Cells.Add(cell12);

                //cell13 = new TableCell();
                //// cell1.ColumnSpan = 3;
                //cell13.Text = "4";
                //cell13.HorizontalAlign = HorizontalAlign.Center;
                //cell13.Font.Bold = true;
                ////cell12.BorderColor = System.Drawing.Color."#CC0000";
                //row.Cells.Add(cell13);

                //cell14 = new TableCell();
                //// cell1.ColumnSpan = 3;
                //cell14.Text = "";
                //cell14.HorizontalAlign = HorizontalAlign.Center;
                //cell14.Font.Bold = true;
                ////cell12.BorderColor = System.Drawing.Color."#CC0000";
                //row.Cells.Add(cell14);
                //cell3 = new TableCell();
                //cell3.Visible = false;
                //row.Cells.Add(cell3);

                //cell4 = new TableCell();
                //cell4.Visible = false;
                //row.Cells.Add(cell4);
                //grdRecordTime.Controls[0].Controls.AddAt(0, row);
            }
        }
        protected void grdRecordTime_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime dtSelectedDate = Calendar1.SelectedDate;
            DateTime dtStartDate, dtEndDate;

            GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.SelectedDate = dtStartDate;
            bdpTo.SelectedDate = dtEndDate;

            LoadToGridDeatils(dtStartDate, dtEndDate);

        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            DateTime dtSelectedDate = Calendar2.SelectedDate;
            DateTime dtStartDate, dtEndDate;

            GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.SelectedDate = dtStartDate;
            bdpTo.SelectedDate = dtEndDate;
            LoadToGridDeatils(dtStartDate, dtEndDate);
        }
        protected void Calendar3_SelectionChanged(object sender, EventArgs e)
        {
            DateTime dtSelectedDate = Calendar3.SelectedDate;
            DateTime dtStartDate, dtEndDate;

            GetCurrentWeekDates(dtSelectedDate, out dtStartDate, out dtEndDate);
            bdpFrom.SelectedDate = dtStartDate;
            bdpTo.SelectedDate = dtEndDate;

            LoadToGridDeatils(dtStartDate, dtEndDate);
        }

        protected void LoadToGridDeatils(DateTime dtStartDate, DateTime dtEndDate)
        {
            wtrecordworkingtimebl objBl = new wtrecordworkingtimebl();
            wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            objBo.FROM_DATE = dtStartDate;
            objBo.TO_DATE = dtEndDate;
            objBo.PERNR = User.Identity.Name;
            wtrecordworkingtimecollectionbo objLstOne = new wtrecordworkingtimecollectionbo();
            objLstOne = objBl.Get_RecordDetails_Week(objBo);
            //wtrecordworkingtimecollectionbo objLst = (wtrecordworkingtimecollectionbo)Session["objLst"];
            //var vOnlyFirstCal = (from col in objLst
            //                     where DateTime.Parse(col.WORKING_DATE) >= dtStartDate &&
            //                     DateTime.Parse(col.WORKING_DATE) <= dtEndDate
            //                     select col).ToList();
            if (objLstOne.Count == 0)
            {
                SetInitialRow();
                ControlStatus();
                return;
            }
            //wtrecordworkingtimecollectionbo objLstOne = new wtrecordworkingtimecollectionbo();

            //foreach (var row in vOnlyFirstCal)
            //{
            //    wtrecordworkingtimebo objBo = new wtrecordworkingtimebo();
            //    objBo.CURRENTRECORD_NO = row.CURRENTRECORD_NO;
            //    objBo.AWART = row.AWART;
            //    objBo.CATSHOURS = row.CATSHOURS;
            //    objBo.DAYS = row.DAYS;
            //    objBo.WORKING_DATE = row.WORKING_DATE;
            //    objBo.ORDER = row.ORDER;
            //    objBo.COST_CENTER = row.COST_CENTER;
            //    objBo.LTEXT = row.LTEXT;
            //    objBo.KTEXT = row.KTEXT;
            //    objLstOne.Add(objBo);
            //}
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
            //dt.Columns.Add(new DataColumn("Column12", typeof(string)));
            //dt.Columns.Add(new DataColumn("Column13", typeof(string)));
            //dt.Columns.Add(new DataColumn("Column14", typeof(string)));
            //dt.Columns.Add(new DataColumn("Column14", typeof(string)));

            return dt;
        }
        public static DataTable ConvertToDataRow(wtrecordworkingtimecollectionbo objColBo)
        {
            DataTable dt = CreateTable();

            foreach (wtrecordworkingtimebo objBo in objColBo)
            {
                DataRow dRow = dt.NewRow();

                //if (objBo.DAYS == "Sunday")
                //{
                dRow["Sun"] = objBo.SUNDAY;
                //}
                //if (objBo.DAYS == "Monday")
                //{
                dRow["Mon"] = objBo.MONDAY;
                //}
                //if (objBo.DAYS == "Tuesday")
                //{
                dRow["Tue"] = objBo.TUESDAY;
                //}
                //if (objBo.DAYS == "Wednesday")
                //{
                dRow["Wed"] = objBo.WEDNESDAY;
                //}
                //if (objBo.DAYS == "Thursday")
                //{
                dRow["Thur"] = objBo.THURSDAY;
                //}
                //if (objBo.DAYS == "Friday")
                //{
                dRow["Fri"] = objBo.FRIDAY;
                //}
                //if (objBo.DAYS == "Saturday")
                //{
                dRow["Sat"] = objBo.SATURDAY;
                //}
                dRow["AttTypes"] = objBo.AWART.Trim();
                dRow["CostCenter"] = objBo.COST_CENTER.Trim();
                dRow["Order"] = objBo.ORDER.Trim();
                //dRow["Column13"] = objBo.LTEXT;
                //dRow["Column14"] = objBo.KTEXT;
                dt.Rows.Add(dRow);
            }
            return dt;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
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

            //int rowcount = 0; 
            //// to keep track of the row being processed
            //foreach (DataRow dr in dt.Rows)
            //{   
            //    if (! has.contains(dr["Column1"]))  
            //    {          
            //        //loop through all the data to    
            //        for(int i = rowcount; i <  Dataset.Tables[0].Rows.count; i ++  )     
            //        {               
            //            if (dr["StudentName"] == Dataset.Tables[0].Rows[i]["StudentName"])     
            //            {                 
            //                //add the columns to the new dataset each time you get the same student         
            //            }                        
            //        }          
            //        //use hastable to keep track of the all processed names       
            //        HashtblProcesslist.add(dr["StudentName"], null);      
            //    }       rowcount ++;    
            //}
            return dtNew;
        }

        //protected void ddl2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    mastercollectionbo objLst = (mastercollectionbo)Session["CostCenterLst"];

        //    DropDownList ddlTest = sender as DropDownList;
        //    string sStr = ddlTest.SelectedValue.ToString();

        //    foreach (GridViewRow row in grdRecordTime.Rows)
        //    {

        //        Control ctrl = row.FindControl("drpdwnCostCenter") as DropDownList;
        //        if (ctrl != null)
        //        {
        //            DropDownList ddl1 = (DropDownList)ctrl;
        //            string sStr1 = ddl1.SelectedValue.ToString();

        //            if (ddlTest.ClientID == ddl1.ClientID)
        //            {
        //                masterbo objBo = objLst.Find(delegate(masterbo obj) { return obj.KOSTL == sStr1; });
        //                //Label lblCostCenter = row.FindControl("lblCostCenter") as Label;
        //                //lblCostCenter.Text = objBo.LTEXT;
        //            }
        //        }
        //    }
        //}
        //protected void ddl3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    mastercollectionbo objLst = (mastercollectionbo)Session["OrderLst"];
        //    DropDownList ddlTest = sender as DropDownList;
        //    string sStr = ddlTest.SelectedValue.ToString();

        //    foreach (GridViewRow row in grdRecordTime.Rows)
        //    {

        //        Control ctrl = row.FindControl("drpdwnOrder") as DropDownList;
        //        if (ctrl != null)
        //        {
        //            DropDownList ddl1 = (DropDownList)ctrl;
        //            string sStr1 = ddl1.SelectedValue.ToString();

        //            if (ddlTest.ClientID == ddl1.ClientID)
        //            {
        //                masterbo objBo = objLst.Find(delegate(masterbo obj) { return obj.AUFNR == sStr1; });
        //                Label lblOrder = row.FindControl("lblOrder") as Label;
        //                lblOrder.Text = objBo.KTEXT;


        //                //txt1.Text = reader["FirstName"].ToString();

        //            }
        //        }
        //    }

        //}

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

        protected void ValidateRows()
        {
        }

        protected string PrepareSMSBody()
        {
            string strBody = string.Empty;
            string strEmployeeName = Session["EmployeeName"].ToString();

            bool bIsSave = (bool)Session["IsSave"];

            if (bIsSave)
            {
                strBody = "Time sheet entry for the week from " + bdpFrom.SelectedDate.ToShortDateString() + " and " + bdpTo.SelectedDate.ToShortDateString() + " has been added by " + strEmployeeName + " | " + User.Identity.Name.ToString() + ".\n\n";
            }
            else
            {
                strBody = "Time sheet entry for the week from " + bdpFrom.SelectedDate.ToShortDateString() + " and " + bdpTo.SelectedDate.ToShortDateString() + " has been modified by " + strEmployeeName + " | " + User.Identity.Name.ToString() + ".\n\n";
            }
            strBody = strBody + " Please approve it ASAP.";

            return strBody;
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
    }
}
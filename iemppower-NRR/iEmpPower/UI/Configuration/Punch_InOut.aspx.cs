using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.Configuration
{
    public partial class Punch_InOut : System.Web.UI.Page
    {
        protected int PageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    getpunchinfilesdata();
                    dv_gv.Visible = GV_punchinfiles.Rows.Count > 0 ? true : false;
                    TxtFrmDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).ToString("dd/MM/yyyy");
                    TxtToDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("dd/MM/yyyy");
                    LoadDDLEmpList();
                }

                RV_TxtToDate.MaximumValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-1).ToString("dd/MM/yyyy");
                RV_TxtToDate.ErrorMessage = string.Format("To date should be less than or equal to {0}.", RV_TxtToDate.MaximumValue);

            }
            catch (Exception ex) { }
        }

        protected void LoadDDLEmpList()
        {
            try
            {
                configurationbl configbl = new configurationbl();
                configurationcollectionbo objConfigurationList = new configurationcollectionbo();
                msassignedtomecollectionbo ObjAssginTMList = new msassignedtomecollectionbo();

                objConfigurationList = configbl.Get_empname(1, Session["CompCode"].ToString());
                DDLEmpNames.DataSource = objConfigurationList;
                DDLEmpNames.DataTextField = "NAME";
                DDLEmpNames.DataValueField = "EMPID";
                DDLEmpNames.DataBind();
                DDLEmpNames.SelectedValue = "1";

                Bind_GV_ClockInClockOut(1);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void btninoutdataupload_Click(object sender, EventArgs e)
        {
            try
            {
                string ImgExt = Path.GetExtension(FL_punchinoutdata.FileName.ToString().ToUpper());
                if (FL_punchinoutdata.HasFile)
                {
                    if (ImgExt == ".TXT")
                    {

                        string eidlen = "";
                        string eid_substringlen = "";
                        string date_len = "";
                        string date_substringlen = "";
                        string time_len = "";
                        string time_substringlen = "";
                        string dateformat = "";
                        string timeformat = "";

                        configurationbl configbl = new configurationbl();
                        configbl.Load_punchinfileconfig_data(Session["CompCode"].ToString(), 1, ref eidlen, ref eid_substringlen, ref date_len, ref date_substringlen,
                            ref time_len, ref time_substringlen, ref dateformat, ref timeformat);
                        if (eidlen.ToString().Trim() != "" || eidlen.ToString().Trim() != null)
                        {
                            // local path : F:/Paycompute_Punchindump/PRD02032020/iemppower-NRR/iEmpPower/PayCompute_Files/Punchinout_Files/
                            // testing port 80 : F:/Paycompute_NewUI/Paycompute_NewUI withDB/Paycompute_PRDtechvi/PRD02032020/iemppower-NRR/iEmpPower/PayCompute_Files/Punchinout_Files/
                            DateTime dt = DateTime.Now;
                            string dtt = "";
                            string filePath = "";
                            string ImgPath = "";
                            string ImgExtup = Path.GetExtension(FL_punchinoutdata.FileName.ToString().ToUpper());
                            string filename1 = "PunchInOut_Report" + "-" + Session["CompCode"].ToString().Trim();

                            dtt = dt.ToString("yyyy-MM-dd hh:mm:ss").Replace(':', '-');
                            filePath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Punchinout_Files/" + filename1.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(FL_punchinoutdata.FileName);
                            ImgPath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Punchinout_Files/"
                                + filename1.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(FL_punchinoutdata.FileName);
                            FL_punchinoutdata.PostedFile.SaveAs(filePath);



                            bool? result = false;
                            configurationbo objempBo = new configurationbo();
                            configurationbl bl = new configurationbl();
                            objempBo.Company_Code = Session["CompCode"].ToString().Trim();
                            objempBo.PASSWORD = ImgPath.ToString().Trim();
                            objempBo.flag = 1;
                            bl.add_punchinfiles(objempBo, ref result);
                            if (result == true)
                            {
                                string textFile = @"C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Punchinout_Files/" + filename1.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(FL_punchinoutdata.FileName);
                                if (File.Exists(textFile))
                                {
                                    DataTable dtin = new DataTable();
                                    dtin.Columns.Add("Employee_ID", typeof(string));
                                    dtin.Columns.Add("Date", typeof(string));
                                    dtin.Columns.Add("Time", typeof(string));
                                    dtin.Columns.Add("Activity_Type", typeof(string));
                                    dtin.Columns.Add("CCode", typeof(string));
                                    dtin.Columns.Add("Activity_Code", typeof(string));


                                    string eidin = "";
                                    string datein = "";
                                    string timein = "";
                                    string activitycode = "";
                                    string activitytype = "";
                                    string txtin = "IN";
                                    string txtout = "OUT";

                                    int eidln = Convert.ToInt32(eidlen.ToString().Trim());
                                    int eid_substringln = Convert.ToInt32(eid_substringlen.ToString().Trim());
                                    int date_ln = Convert.ToInt32(date_len.ToString().Trim());
                                    int date_substringln = Convert.ToInt32(date_substringlen.ToString().Trim());
                                    int time_ln = Convert.ToInt32(time_len.ToString().Trim());
                                    int time_substringln = Convert.ToInt32(time_substringlen.ToString().Trim());
                                    string dtformat = dateformat.ToString().Trim();
                                    string tmformat = timeformat.ToString().Trim();

                                    // Read entire text file content in one string
                                    string text = File.ReadAllText(textFile);
                                    // Read a text file line by line.  
                                    string[] lines = File.ReadAllLines(textFile);
                                    foreach (string line in lines)
                                    {
                                        if (line.ToString() != "")
                                        {
                                            if (line.ToString().ToLower().Trim().Contains(txtin.ToString().ToLower().Trim()))
                                            {
                                                activitycode = "P10";
                                                activitytype = "Check-In";
                                            }
                                            if (line.ToString().ToLower().Trim().Contains(txtout.ToString().ToLower().Trim()))
                                            {
                                                activitycode = "P20";
                                                activitytype = "Check-Out";
                                            }
                                            // Get login Employee ID
                                            string eid = line.Remove(0, eid_substringln);
                                            eidin = eid != null ? string.Join("", eid.Take(eidln)) : null;

                                            // Substring emp id to get login date
                                            string substeid = line.Remove(0, date_substringln);
                                            datein = substeid != null ? string.Join("", substeid.Take(date_ln)) : null;
                                            DateTime indt = DateTime.ParseExact(datein.ToString().Trim(), dtformat.ToString().Trim(), CultureInfo.InvariantCulture);
                                            string ain = indt.ToString("yyyy-MM-dd");

                                            // substring empid and login date to get login time
                                            string substeiddt = line.Remove(0, time_substringln);
                                            timein = substeiddt != null ? string.Join("", substeiddt.Take(time_ln)) : null;
                                            DateTime intime = DateTime.ParseExact(timein.ToString().Trim(), tmformat.ToString().Trim(), CultureInfo.InvariantCulture);
                                            string tm = intime.ToString("hh:mm:ss tt");

                                            ////substring empid,login date,company login time to get in-note
                                            //string activity = line.Remove(0, action_substringln);
                                            //notein = activity != null ? string.Join("", activity.Take(action_ln)) : null;

                                            string cc1 = Session["CompCode"].ToString().Trim();
                                            DataRow row = dtin.NewRow();
                                            row["Employee_ID"] = cc1 + "" + eidin.ToString().Trim();
                                            row["Date"] = ain.ToString().Trim();
                                            row["Time"] = tm.ToString().Trim();
                                            row["Activity_Type"] = activitytype.ToString().Trim();
                                            row["CCode"] = cc1.ToString().Trim();
                                            row["Activity_Code"] = activitycode.ToString().Trim();
                                            dtin.Rows.Add(row);
                                        }
                                    }

                                    configurationbo bo2 = new configurationbo();
                                    configurationbl bl1 = new configurationbl();
                                    bool? stin = false;
                                    using (dtin)
                                    {
                                        if (dtin.Rows.Count > 0)
                                        {
                                            for (int j = 0; j < dtin.Rows.Count; j++)
                                            {
                                                bo2.EMPID = dtin.Rows[j]["Employee_ID"].ToString().Trim();
                                                bo2.Company_Address = dtin.Rows[j]["Date"].ToString().Trim();
                                                bo2.CountryTxt = dtin.Rows[j]["Time"].ToString().Trim();
                                                bo2.Company_Type_Txt = dtin.Rows[j]["Activity_Type"].ToString().Trim();
                                                bo2.Company_Code = dtin.Rows[j]["CCode"].ToString().Trim();
                                                bo2.ATEXT = dtin.Rows[j]["Activity_Code"].ToString().Trim();
                                                bo2.flag = 1;
                                                bl1.Create_punchin(bo2, ref stin);
                                            }
                                        }
                                    }

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Punch In/Out data has been uploaded successfully..!');", true);

                                }



                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Punch In/Out unable to update..!');", true);
                            }
                            getpunchinfilesdata();

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Unable to update file, please contact your administrator..!');", true);
                        }
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please select text format to upload the data..!');", true);
                    }
                }



                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Please select text file to upload data..!');", true);
                }
            }


            catch (Exception ex)
            {

            }
        }

        protected void GV_punchinfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

                    case "FILEDOWN":
                        int docget = Convert.ToInt32(e.CommandArgument);

                        GridViewRow gvdocdownload = GV_punchinfiles.Rows[docget];

                        string docfile = GV_punchinfiles.DataKeys[gvdocdownload.RowIndex].Values["EMPID"].ToString();

                        string ImgExtupd = Path.GetExtension(docfile.ToString().ToUpper());

                        Response.ContentType = "application/octet-stream";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + '/' + System.DateTime.Now + ImgExtupd.ToString().Trim());
                        Response.TransmitFile(docfile);
                        Response.End();
                        break;
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void getpunchinfilesdata()
        {
            try
            {
                configurationcollectionbo oblst = new configurationcollectionbo();
                configurationbl bl = new configurationbl();
                oblst = bl.Get_punchin_files(Session["CompCode"].ToString(), 1);
                GV_punchinfiles.DataSource = oblst;
                GV_punchinfiles.DataBind();

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_punchinfiles.Rows)
                {
                    for (int i = 0; i < GV_punchinfiles.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_punchinfiles.Rows[i].FindControl("lblRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;
                        }
                        if (i == GV_punchinfiles.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + oblst.Count + " entries";
                divcnt.Visible = GV_punchinfiles.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_punchinfiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                configurationcollectionbo oblst = new configurationcollectionbo();
                configurationbl bl = new configurationbl();
                oblst = bl.Get_punchin_files(Session["CompCode"].ToString(), 1);
                GV_punchinfiles.DataSource = oblst;
                GV_punchinfiles.PageIndex = e.NewPageIndex;
                GV_punchinfiles.DataBind();

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_punchinfiles.Rows)
                {
                    for (int i = 0; i < GV_punchinfiles.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_punchinfiles.Rows[i].FindControl("lblRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;
                        }
                        if (i == GV_punchinfiles.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + oblst.Count + " entries";
                divcnt.Visible = GV_punchinfiles.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                if (!string.IsNullOrEmpty(TxtFrmDate.Text) && !string.IsNullOrEmpty(TxtToDate.Text))
                {
                    if (DDLEmpNames.SelectedValue != "0")
                    {
                        if (DateTime.TryParse(TxtFrmDate.Text, out FromDate))
                        {
                            if (DateTime.TryParse(TxtToDate.Text, out ToDate))
                            {
                                Bind_GV_ClockInClockOut(1);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            { }
        }

        public void punchinmaingv()
        {
            try
            {
                foreach (GridViewRow row in GV_punchindetails.Rows)
                {
                    LinkButton view = (LinkButton)row.FindControl("lnkviewindetail");
                    bool? count = Convert.ToBoolean(GV_punchindetails.DataKeys[row.RowIndex].Values[2].ToString());

                    if (count == true)
                    {
                        view.Visible = true;
                    }
                    else
                    {
                        view.Visible = false;
                    }


                }
            }

            catch (Exception ex)
            {

            }
        }

        private void Bind_GV_ClockInClockOut(int PageIndex)
        {
            try
            {

                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                if (DateTime.TryParse(TxtFrmDate.Text, out FromDate))
                {
                    if (DateTime.TryParse(TxtToDate.Text, out ToDate))
                    {
                        leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                        leaverequestcollectionbo ObjClkInClkOutLst = new leaverequestcollectionbo();

                        ObjClkInClkOutLst = ObjLeaveRequestBl.Get_Emp_ClockIn_ClockOut(DDLEmpNames.SelectedValue, User.Identity.Name, FromDate, ToDate, PageIndex, 10, 1, Session["CompCode"].ToString().Trim());
                        if (ObjClkInClkOutLst.Count > 0)
                        {

                            GV_punchindetails.DataSource = ObjClkInClkOutLst;
                            GV_punchindetails.DataBind();
                            punchinmaingv();
                            btnExportToExcel.Visible = true;
                            PopulatePager(int.Parse(ObjClkInClkOutLst[0].PIPORECCOUNT.ToString()), PageIndex);

                        }
                        else
                        {
                            GV_punchindetails.DataSource = null;
                            GV_punchindetails.DataBind();
                            btnExportToExcel.Visible = false;
                        }

                        string frow = "", lrow = "";  ////Row count

                        foreach (GridViewRow row in GV_punchindetails.Rows)
                        {
                            for (int i = 0; i < GV_punchindetails.Rows.Count; i++)
                            {
                                //Label lblRowNumber = (Label)GV_punchindetails.Rows[i].FindControl("lblRRowNumber");
                                if (i == 0)
                                {
                                    frow = GV_punchindetails.Rows[i].Cells[0].Text;
                                }
                                if (i == GV_punchindetails.Rows.Count - 1)
                                {
                                    lrow = GV_punchindetails.Rows[i].Cells[0].Text;
                                }
                            }
                        }
                        divpendingrecordcount.InnerHtml = "Showing " + frow + " to " + lrow + " of " + ObjClkInClkOutLst[0].PIPORECCOUNT.ToString() + " entries";
                        DivPaging.Visible = GV_punchindetails.Rows.Count > 0 ? true : false;
                        btnExportToExcel.Visible = GV_punchindetails.Rows.Count > 0 ? true : false;

                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void GV_punchindetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                if (DateTime.TryParse(TxtFrmDate.Text, out FromDate))
                {
                    if (DateTime.TryParse(TxtToDate.Text, out ToDate))
                    {
                        int pageindex = e.NewPageIndex;
                        leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                        leaverequestcollectionbo ObjClkInClkOutLst = new leaverequestcollectionbo();
                        ObjClkInClkOutLst = ObjLeaveRequestBl.Get_Emp_ClockIn_ClockOut(DDLEmpNames.SelectedValue, User.Identity.Name, FromDate, ToDate, 1, 10, 1, Session["CompCode"].ToString().Trim());
                        if (ObjClkInClkOutLst.Count > 0)
                        {

                            GV_punchindetails.DataSource = ObjClkInClkOutLst;
                            GV_punchindetails.DataBind();
                            punchinmaingv();
                            btnExportToExcel.Visible = true;
                            PopulatePager(int.Parse(ObjClkInClkOutLst[0].PIPORECCOUNT.ToString()), PageIndex);

                        }
                        else
                        {
                            GV_punchindetails.DataSource = null;
                            GV_punchindetails.DataBind();
                            btnExportToExcel.Visible = false;
                        }

                        string frow = "", lrow = "";  ////Row count

                        foreach (GridViewRow row in GV_punchindetails.Rows)
                        {
                            for (int i = 0; i < GV_punchindetails.Rows.Count; i++)
                            {
                                Label lblRowNumber = (Label)GV_punchindetails.Rows[i].FindControl("lblRRowNumber");
                                if (i == 0)
                                {
                                    frow = GV_punchindetails.Rows[i].Cells[0].Text;
                                }
                                if (i == GV_punchindetails.Rows.Count - 1)
                                {
                                    lrow = GV_punchindetails.Rows[i].Cells[0].Text;
                                }
                            }
                        }
                        divpendingrecordcount.InnerHtml = "Showing " + frow + " to " + lrow + " of " + ObjClkInClkOutLst[0].PIPORECCOUNT.ToString() + " entries";
                        DivPaging.Visible = GV_punchindetails.Rows.Count > 0 ? true : false;
                        btnExportToExcel.Visible = GV_punchindetails.Rows.Count > 0 ? true : false;
                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GV_punchindetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = GV_punchindetails.Rows[rowIndex];
                LinkButton lnklock = (LinkButton)grdRow.FindControl("lnktemplock");
                switch (e.CommandName.ToUpper())
                {

                    case "VIEW":
                        modal1.Show();
                        string pernr = GV_punchindetails.DataKeys[grdRow.RowIndex].Values["PERNR"].ToString();
                        DateTime FromDate = Convert.ToDateTime(GV_punchindetails.DataKeys[grdRow.RowIndex].Values["DATES"].ToString());
                        DateTime ToDate = Convert.ToDateTime(GV_punchindetails.DataKeys[grdRow.RowIndex].Values["DATES"].ToString());
                        DateTime rowdate = Convert.ToDateTime(GV_punchindetails.DataKeys[grdRow.RowIndex].Values["DATES"].ToString());

                        leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                        leaverequestcollectionbo ObjClkInClkOutLst = new leaverequestcollectionbo();

                        ObjClkInClkOutLst = ObjLeaveRequestBl.Get_Emp_ClockIn_ClockOutfull(pernr.ToString().Trim(), FromDate, ToDate, rowdate, 1, Session["CompCode"].ToString());
                        GV_punchinfulldetails.DataSource = ObjClkInClkOutLst;
                        GV_punchinfulldetails.DataBind();
                        Bindtotalhrs();

                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_punchindetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)e.Row.FindControl("lblID");
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

        public void Bindtotalhrs()
        {
            try
            {

                TimeSpan total = TimeSpan.Parse("00:00");
                for (int i = 0; i < GV_punchinfulldetails.Rows.Count; i++)
                {

                    Label hrs = (Label)GV_punchinfulldetails.Rows[i].FindControl("lblbrk");
                    TimeSpan calc = TimeSpan.Parse(hrs.Text == "" ? "00:00" : hrs.Text);
                    total = total + calc;

                }
                Label thrs = ((Label)GV_punchinfulldetails.FooterRow.FindControl("lblTotal"));
                thrs.Text = total.ToString() == "0.00" ? "" : total.ToString();

                ViewState["Totalscore"] = thrs.Text;
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                TxtFrmDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1).ToString("dd/MM/yyyy");
                TxtToDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("dd/MM/yyyy");
                LoadDDLEmpList();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel();
            }
            catch (Exception ex)
            {

            }
        }


        private void EXPORT_Bind_GV_ClockInClockOut()
        {
            try
            {
                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                if (DateTime.TryParse(TxtFrmDate.Text, out FromDate))
                {
                    if (DateTime.TryParse(TxtToDate.Text, out ToDate))
                    {
                        leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                        leaverequestcollectionbo ObjClkInClkOutLst = new leaverequestcollectionbo();

                        ObjClkInClkOutLst = ObjLeaveRequestBl.Get_Emp_ClockIn_ClockOut(DDLEmpNames.SelectedValue, User.Identity.Name, FromDate, ToDate, 0, 0, 2, Session["CompCode"].ToString().Trim());
                        if (ObjClkInClkOutLst.Count > 0)
                        {
                            GV_punchindetails.DataSource = ObjClkInClkOutLst;
                            GV_punchindetails.DataBind();
                            punchinmaingv();
                        }
                        else
                        {
                            GV_punchindetails.DataSource = null;
                            GV_punchindetails.DataBind();
                        }

                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        protected void ExportToExcel()
        {

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


            htw.WriteBreak();
            string colHeads = "Punch IN - Punch Out Details";
            htw.WriteEncodedText(colHeads);
            GV_punchindetails.GridLines = GridLines.Both;
            GV_punchindetails.AllowPaging = false;
            GV_punchindetails.Columns[9].Visible = false;
            EXPORT_Bind_GV_ClockInClockOut();
            GV_punchindetails.RenderControl(htw);
            htw.WriteBreak();
            GV_punchindetails.Columns[9].Visible = true;
            GV_punchindetails.AllowPaging = true;
            string renderedGridView = "<br/>"; renderedGridView += sw.ToString() + "<br/>";
            Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_PunchInOutDetails.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();

            Bind_GV_ClockInClockOut(1);


        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = PageIndex = int.Parse((sender as LinkButton).CommandArgument);
                this.Bind_GV_ClockInClockOut(pageIndex);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        private void PopulatePager(int RecordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 10;

                //Calculate the Start and End Index of pages to be displayed.
                double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal("10"));
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
                RptrClkInClkOutPager.DataSource = pages;
                RptrClkInClkOutPager.DataBind();

                GV_punchindetails.FooterRow.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;<b>Page " + currentPage + " of " + pageCount + "<b/>";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }
    }
}
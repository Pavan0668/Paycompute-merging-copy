using iEmpPower.Old_App_Code.iEmpPowerBL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP;
using iEmpPower.Old_App_Code.iEmpPowerDAL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBL.IT;
using iEmpPower.Old_App_Code.iEmpPowerBO.IT;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using System.Data;
using iTextSharp.text.html.simpleparser;
using System.Web.Services;

namespace iEmpPower.UI.FBP
{
    public partial class FBP_AdminLocking : System.Web.UI.Page
    {
        protected int PageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LoadLockGrid(1);
                LoadLockGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Employee details loaded successfully')", true);
                cb_SearchEnable.Enabled = true;
            }
        }

        // private void LoadLockGrid(int PageIndex)
        private void LoadLockGrid()
        {
            try
            {
                int? RecordCountl = 0;
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                string ApproverId = User.Identity.Name;
                // fbpboObj1 = fbpblObj.Load_Fbp_Locking(PageIndex, 15, ref RecordCountl);
                fbpboObj1 = fbpblObj.Load_Fbp_Locking("NO");

                if (fbpboObj1 == null || fbpboObj1.Count == 0)
                {
                    // MsgCls("No Records Found !", lblmsg, Color.Red);
                    grdLocking.Visible = false;
                    grdLocking.DataSource = null;
                    grdLocking.DataBind();
                    return;
                }
                else
                {
                    grdLocking.Visible = true;
                    grdLocking.DataSource = fbpboObj1;
                    grdLocking.SelectedIndex = -1;
                    grdLocking.DataBind();
                    // PopulatePager(RecordCountl, PageIndex);
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        //#region Populate pager Leave
        //private void PopulatePager(int? RecordCount, int currentPage)
        //{
        //    try
        //    {
        //        List<System.Web.UI.WebControls.ListItem> pages = new List<System.Web.UI.WebControls.ListItem>();
        //        int startIndex, endIndex;
        //        int pagerSpan = 10;

        //        //Calculate the Start and End Index of pages to be displayed.
        //        double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal(15));
        //        int pageCount = (int)Math.Ceiling(dblPageCount);
        //        startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        //        endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        //        if (currentPage > pagerSpan % 2)
        //        {
        //            if (currentPage == 2)
        //            { endIndex = 5; }
        //            else
        //            { endIndex = currentPage + 2; }
        //        }
        //        else
        //        { endIndex = (pagerSpan - currentPage) + 1; }

        //        if (endIndex - (pagerSpan - 1) > startIndex)
        //        { startIndex = endIndex - (pagerSpan - 1); }

        //        if (endIndex > pageCount)
        //        {
        //            endIndex = pageCount;
        //            startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        //        }

        //        //Add the First Page Button.
        //        if (currentPage > 1)
        //        { pages.Add(new System.Web.UI.WebControls.ListItem("First", "1")); }

        //        //Add the Previous Button.
        //        if (currentPage > 1)
        //        { pages.Add(new System.Web.UI.WebControls.ListItem("<<", (currentPage - 1).ToString())); }

        //        for (int i = startIndex; i <= endIndex; i++)
        //        { pages.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString(), i != currentPage)); }

        //        //Add the Next Button.
        //        if (currentPage < pageCount)
        //        { pages.Add(new System.Web.UI.WebControls.ListItem(">>", (currentPage + 1).ToString())); }

        //        //Add the Last Button.
        //        if (currentPage != pageCount)
        //        { pages.Add(new System.Web.UI.WebControls.ListItem("Last", pageCount.ToString())); }
        //        RptrPager.DataSource = pages;
        //        RptrPager.DataBind();

        //        grd_Locking.FooterRow.Cells[2].Text = "&nbsp;<b>Page <b/>" + currentPage + " of " + pageCount + "<b/>";




        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}


        //protected void Page_Changed(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int pageIndex = PageIndex = int.Parse((sender as LinkButton).CommandArgument);


        //        this.LoadLockGrid(pageIndex);


        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        //}
        //#endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
                foreach (GridViewRow gvrow in grdLocking.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("CB_Lock");
                    string perner = gvrow.Cells[1].Text.ToString().Trim();
                    if (chk != null & chk.Checked)
                    {
                        objDataContext.usp_Fbp_UpdateLocking(perner, chk.Checked);
                    }
                    else
                    {
                        objDataContext.usp_Fbp_UpdateLocking(perner, chk.Checked);
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated Successfully')", true);
                LoadLockGrid();
                txtsearch.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void grd_Locking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton l = (LinkButton)e.Row.FindControl("linkresetslab");
                    Label lbl = (Label)e.Row.FindControl("lblstatus");
                    LinkButton lmob = (LinkButton)e.Row.FindControl("linkresetMob");
                    Label lblmob = (Label)e.Row.FindControl("lblMobstatus");
                    LinkButton lCC = (LinkButton)e.Row.FindControl("linkresetCC");
                    Label lblCC = (Label)e.Row.FindControl("lblCCstatus");
                    Control divM = (Control)e.Row.FindControl("divMob");
                    Control divCC = (Control)e.Row.FindControl("divCC");
                    List<ITbo> ITboList = new List<ITbo>();
                    ITbl itobjBL = new ITbl();
                    ITboList = itobjBL.Get_IT_Slab_details(0, grdLocking.DataKeys[e.Row.RowIndex].Values[0].ToString().Trim(), 1);
                    if (ITboList.Count == 0 || ITboList[0].ITSLAB == 0)
                    {
                        lbl.Text = "Slab Not Set";
                        l.Visible = false;
                    }
                    else
                    {
                        lbl.Text = ITboList[0].ITSLAB == 1 ? "Old Slab" : "New Slab";
                        l.Visible = true;
                    }
                    List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                    Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                    FbpClaimbo fbpboObj = new FbpClaimbo();
                    fbpboObj.PERNR = grdLocking.DataKeys[e.Row.RowIndex].Values[0].ToString().Trim();
                    fbpboObj.ENAME = User.Identity.Name.Trim();
                    fbpboObj.Mob = false;
                    fbpboObj.CC = false;
                    bool? m = false;
                    bool? c = false;
                    fbpblObj.SetFBPLock_Bit(fbpboObj, 3, ref m, ref c);
                    if (m == false)
                    {
                        lblmob.Text = "Not Declared";
                        lmob.Visible = false;
                        divM.Visible = false;
                    }
                    else
                    {
                        Label lblM1 = (Label)e.Row.FindControl("lblMob1");
                        Label lblM2 = (Label)e.Row.FindControl("lblMob2");
                        picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                        picommunicationinformationbl objCommuInfoBl = new picommunicationinformationbl();
                        objCommuInfoBo.EMPLOYEE_ID = grdLocking.DataKeys[e.Row.RowIndex].Values[0].ToString().Trim();
                        picommunicationinformationcollectionbo objCommuInfoLst = objCommuInfoBl.Get_Details(objCommuInfoBo);
                        foreach (picommunicationinformationbo objBo in objCommuInfoLst)
                        {
                            if (objBo.MPHN.Trim() == "0020")
                            {
                                lblM1.Text = objBo.MPHN_ID;
                            }
                            else if (objBo.USRTY.Trim() == "0021")      ///0021 as Mob 2
                            {
                                lblM2.Text = objBo.LICENCE_NO;
                            }
                        }
                        divM.Visible = true;
                        lblmob.Text = "";
                        lmob.Visible = true;
                    }

                    if (c == false)
                    {
                        lblCC.Text = "Not Declared";
                        lCC.Visible = false;
                        divCC.Visible = false;
                    }
                    else
                    {
                        lblCC.Text = "";
                        lCC.Visible = true;
                        divCC.Visible = true;
                        pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
                        pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                        objPersonalIDsBo.PERNR = grdLocking.DataKeys[e.Row.RowIndex].Values[0].ToString().Trim();
                        objPersonalIDsBo.CC = 0;
                        objPersonalIDsBo.FPDAT = DateTime.MinValue;
                        int? cc = 0;
                        string file = "", RCNO = "", RCNAME = "";
                        DateTime? dt = DateTime.MinValue;
                        bool? Status = false;
                        objPersonalIDsBl.Create_PersonalIDs_car(objPersonalIDsBo, 3, ref Status, ref cc, ref dt, ref file, ref RCNAME, ref RCNO);

                        Label lblVCC = (Label)e.Row.FindControl("lvlVCC");
                        Label lblRCN = (Label)e.Row.FindControl("lblRCNum");
                        Label lblName = (Label)e.Row.FindControl("lblName");
                        Label lblRdate = (Label)e.Row.FindControl("lblRdt");
                        Label lblRDwnld = (Label)e.Row.FindControl("lblRDwnld");
                        LinkButton lnkRDwnld = (LinkButton)e.Row.FindControl("lnkRDwnld");

                        lblVCC.Text = cc == 1 ? "1800" : ">1800";
                        lblRCN.Text = RCNO;
                        lblName.Text = RCNAME;
                        lblRdate.Text = DateTime.Parse(dt.ToString()).ToString("yyyy-MM-dd");
                        lblRDwnld.Text = file;

                        lnkRDwnld.Visible = lblRDwnld.Text != "" ? true : false;
                    }

                    //foreach (GridViewRow GvRow in grdLocking.Rows)
                    //{
                    //    using (CheckBox Chk = (CheckBox)GvRow.FindControl("CB_Lock"))
                    //    {
                    //        if (Chk != null)
                    //        {
                    //            Chk.Attributes.Add("onclick", "javascript:LockUnlock('" + grdLocking.Rows[GvRow.RowIndex].Cells[1].Text + "', this.checked)");
                    //        }
                    //    }
                    //}

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void btnMark_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvrow in grdLocking.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("CB_Lock");
                    chk.Checked = true;
                }



            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void btnUnmark_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvrow in grdLocking.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("CB_Lock");
                    chk.Checked = false;
                }



            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                searchdetails();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }


        }


        public void searchdetails()
        {

            try
            {
                //  viewcheck.Value = "YES";
                MsgCls(string.Empty, LblMsg, System.Drawing.Color.Transparent);

                string textSearch = txtsearch.Text;


                if (textSearch == "")
                {
                    MsgCls("Please Enter the Text", LblMsg, System.Drawing.Color.Red);
                }


                else
                {


                    Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                    List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                    List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();

                    fbpboObj1 = fbpblObj.Load_Fbp_Locking(textSearch);

                    if (fbpboObj1 == null || fbpboObj1.Count == 0)
                    {
                        // MsgCls("No Records found", LblMsg, System.Drawing.Color.Red);
                        MsgCls("Requested Employee ID is not found", LblMsg, System.Drawing.Color.Red);
                        grdLocking.Visible = false;
                        grdLocking.DataSource = null;
                        grdLocking.DataBind();
                        //divclaims.Visible = false;

                        return;
                    }
                    else
                    {
                        MsgCls("", LblMsg, System.Drawing.Color.Transparent);
                        grdLocking.Visible = true;
                        grdLocking.DataSource = fbpboObj1;
                        grdLocking.SelectedIndex = -1;
                        grdLocking.DataBind();
                        //GV_TravelClaimReqAppRej.Visible = false;
                        //grdAppRejTravel.Visible = false;
                        //Panel1.Visible = false;
                        //divclaims.Visible = false;
                        //Session.Add("FbpGrdInfo", fbpboObj1);

                    }

                }

            }

            catch (Exception Ex)
            {

                MsgCls("Please enter valid data", LblMsg, System.Drawing.Color.Red);
            }
        }

        private void MsgCls(string LblTxt, Label Lbl, System.Drawing.Color Clr)
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
        protected void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                // viewcheck.Value = "NO";

                txtsearch.Text = string.Empty;
                LoadLockGrid();
                // divclaims.Visible = false;
                txtsearch.Focus();

                MsgCls("", LblMsg, System.Drawing.Color.Transparent);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void grdLocking_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                FbpClaimbo fbpboObj = new FbpClaimbo();
                fbpboObj.PERNR = grdLocking.DataKeys[int.Parse(e.CommandArgument.ToString())]["PERNR"].ToString().Trim();
                fbpboObj.ENAME = User.Identity.Name.Trim();
                fbpboObj.Mob = false;
                fbpboObj.CC = false;
                bool? m = false;
                bool? c = false;
                int rowindex = int.Parse(e.CommandArgument.ToString());
                FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
                switch (e.CommandName.ToUpper())
                {
                    //case "LOCK":
                    //    CheckBox chk = (CheckBox)grdLocking.Rows[rowindex].Cells[4].FindControl("CB_Lock");
                    //    if (chk != null && chk.Checked)
                    //    {
                    //        objDataContext.usp_Fbp_UpdateLocking(grdLocking.DataKeys[int.Parse(e.CommandArgument.ToString())]["PERNR"].ToString().Trim(), chk.Checked);
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Locked Successfully!!');", true);
                    //    }
                    //    else
                    //    {
                    //        objDataContext.usp_Fbp_UpdateLocking(grdLocking.DataKeys[int.Parse(e.CommandArgument.ToString())]["PERNR"].ToString().Trim(), chk.Checked);
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Unlocked Successfully!!');", true);
                    //    }
                    //    break;
                    case "RESET":

                        ITbl itobjBL = new ITbl();
                        bool? status = false;
                        itobjBL.IT_Set_Details(0, grdLocking.DataKeys[int.Parse(e.CommandArgument.ToString())]["PERNR"].ToString().Trim(), 1, 0, ref status);
                        if (status == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Slab is reset!!');", true);
                            Label sts = (Label)grdLocking.Rows[rowindex].Cells[4].FindControl("lblstatus");
                            LinkButton lnkreset = (LinkButton)grdLocking.Rows[rowindex].Cells[4].FindControl("linkresetslab");
                            lnkreset.Visible = false;
                            sts.Text = "Slab Not Set";

                            //LoadLockGrid();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Something Wrong!!');", true);
                            // Response.Redirect("~/UI/Default.aspx", false);
                            //LoadLockGrid();
                        }
                        break;
                    case "MOBRESET":
                        fbpblObj.SetFBPLock_Bit(fbpboObj, 1, ref m, ref c);
                        if (m == false)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Mobile Info is reset!!');", true);
                            Label sts = (Label)grdLocking.Rows[rowindex].Cells[5].FindControl("lblMobstatus");
                            sts.Text = "Not Declared";
                            sts.Visible = true;
                            Control divM = (Control)grdLocking.Rows[rowindex].Cells[5].FindControl("divMob");
                            divM.Visible = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Something Wrong!!');", true);
                            // Response.Redirect("~/UI/Default.aspx", false);
                            ///LoadLockGrid();
                        }
                        //LoadLockGrid();
                        break;
                    case "CCRESET":
                        fbpblObj.SetFBPLock_Bit(fbpboObj, 2, ref m, ref c);
                        if (c == false)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Car CC Info is reset!!');", true);
                            Label sts = (Label)grdLocking.Rows[rowindex].Cells[6].FindControl("lblCCstatus");
                            sts.Text = "Not Declared";
                            sts.Visible = true;
                            Control divCC = (Control)grdLocking.Rows[rowindex].Cells[6].FindControl("divCC");
                            divCC.Visible = false;

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Something Wrong!!');", true);
                            // Response.Redirect("~/UI/Default.aspx", false);
                            //LoadLockGrid();
                        }
                        //LoadLockGrid();
                        break;

                    case "CCDWLD":
                        int rowIndex = Convert.ToInt32(e.CommandArgument);
                        GridViewRow row = grdLocking.Rows[rowIndex];
                        string filePath = (row.FindControl("lblRDwnld") as Label).Text;
                        Response.ContentType = "application/octet-stream";
                        //Response.ContentType = ContentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                        Response.WriteFile(filePath);
                        Response.End();
                        break;
                }
            }
            catch (Exception ex) { }
        }


        protected void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                string ApproverId = User.Identity.Name;
                fbpboObj1 = fbpblObj.Load_IDs_NOTDeclrd(1);
                grd_Notdeclared.Visible = true;
                grd_Notdeclared.DataSource = fbpboObj1;
                grd_Notdeclared.SelectedIndex = -1;
                grd_Notdeclared.DataBind();
                MPE_Pend.Show();
            }
            catch (Exception ex) { }
        }

        protected void btnExcelProceed_Click(object sender, EventArgs e)
        {
            try
            {
                string excelPath = Server.MapPath("~/FBPDoc/Temp/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uflEmpData.FileName) + "-" + DateTime.Now.ToString("yyyy_MM_dd") + Path.GetExtension(uflEmpData.FileName));
                ////Server.MapPath("~/PayCompute_Files/Emp_info/") + Path.GetFileName(uflEmpData.PostedFile.FileName);
                uflEmpData.SaveAs(excelPath);


                string conString = string.Empty;
                string extension = Path.GetExtension(uflEmpData.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;
                }

                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    string Emp_Info = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();
                    DataTable dtExcelData = EmpData();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Employee_ID,Lock_Unlock FROM  [Emp_Info$] where 'Employee_ID != '''", excel_con))
                    {
                        oda.Fill(dtExcelData);
                        for (int i = dtExcelData.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                        }
                        dtExcelData.AcceptChanges();
                    }


                    for (int i = 0; i <= dtExcelData.Rows.Count; i++)
                    {
                        for (int j = 0; j <= grdLocking.Rows.Count; j++)
                        {
                            if (grdLocking.Rows[j].Cells[1].Text.ToString().Trim() == dtExcelData.Rows[i]["Employee_ID"].ToString().Trim())
                            {
                                CheckBox chk = grdLocking.Rows[j].Cells[1].FindControl("CB_Lock") as CheckBox;
                                chk.Checked = dtExcelData.Rows[i]["Lock_Unlock"].ToString().Trim() == "1" ? true : false;
                                break;
                            }
                        }
                    }
                    //File.Delete(Path.Combine(excelPath));
                }
            }

            catch (Exception ex) { }
        }

        protected DataTable EmpData()
        {
            DataTable dtExcelData1 = new DataTable();
            dtExcelData1.Columns.AddRange(new DataColumn[2]
                    {
                     new DataColumn("Employee_ID",typeof(string)),
                     new DataColumn("Lock_Unlock",typeof(string))
                     });
            return dtExcelData1;
        }

        protected void lnkExcelTepDwnld_Click(object sender, EventArgs e)
        {
            string filePath = "~/FBPDoc/FBP_IT_Lock_Unlock_Employees.xls";
            //Response.ContentType = ContentType;
            Response.ContentType = "application/octet-stream";
            // Response.ContentType = "application/x-download";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void btnexpPDF_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_FBPClaim.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            grdLocking.GridLines = GridLines.Horizontal;
            StringWriter s_tw = new StringWriter();
            HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
            h_textw.AddStyleAttribute("font-size", "8pt");
            h_textw.AddStyleAttribute("color", "Black");



            h_textw.WriteBreak();
            string colHeads = "FBP Claim Details";
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();

            grdLocking.HeaderRow.ForeColor = System.Drawing.Color.Blue;
            grdLocking.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
            grdLocking.RenderControl(h_textw);

            grdLocking.GridLines = GridLines.None;
            h_textw.WriteBreak();

            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);
            //  Document doc = new Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();

            StringReader s_tr = new StringReader(s_tw.ToString());
            HTMLWorker html_worker = new HTMLWorker(doc);
            html_worker.Parse(s_tr);
            doc.Close();
            Response.Write(doc);
        }

        protected void btnexpXL_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow GvRow in grdLocking.Rows)
                {
                    LinkButton linkresetMob = (LinkButton)GvRow.FindControl("linkresetMob");
                    LinkButton lnkRDwnld = (LinkButton)GvRow.FindControl("lnkRDwnld");
                    LinkButton linkresetCC = (LinkButton)GvRow.FindControl("linkresetCC");
                    LinkButton linkresetslab = (LinkButton)GvRow.FindControl("linkresetslab");
                    Label lblRDwnld = (Label)GvRow.FindControl("lblRDwnld");
                    linkresetslab.Visible = false;
                    linkresetCC.Visible = false;
                    lnkRDwnld.Visible = false;
                    linkresetMob.Visible = false;
                    lblRDwnld.Text = lblRDwnld.Text != "" ? "File uploaded" : "No file uploaded";

                }

                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                grdLocking.GridLines = GridLines.Horizontal;
                // Render grid view control.
                htw.WriteBreak();
                grdLocking.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                grdLocking.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                grdLocking.RenderControl(htw);
                grdLocking.GridLines = GridLines.None;
                htw.WriteBreak();


                // Write the rendered content to a file.

                // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                string renderedGridView = "FBP Declared Details <br/>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_FBPDeclared.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();
            }
            catch (Exception ex) { }
        }
        //protected void grd_Locking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        int pageindex = e.NewPageIndex;
        //        grd_Locking.PageIndex = e.NewPageIndex;

        //        LoadLockGrid();

        //        grd_Locking.SelectedIndex = -1;
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}

        public override void VerifyRenderingInServerForm(Control control)
        { }

        protected void CB_Lock_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)cb.NamingContainer;

            int rowindex = gvr.RowIndex;

            FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
            CheckBox chk = (CheckBox)grdLocking.Rows[rowindex].Cells[3].FindControl("CB_Lock");
            if (chk != null && chk.Checked)
            {
                objDataContext.usp_Fbp_UpdateLocking(grdLocking.DataKeys[rowindex]["PERNR"].ToString().Trim(), chk.Checked);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Locked Successfully!!');", true);

            }
            else
            {
                objDataContext.usp_Fbp_UpdateLocking(grdLocking.DataKeys[rowindex]["PERNR"].ToString().Trim(), chk.Checked);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Unlocked Successfully!!');", true);
            }

        }

        protected void CB_LockHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)grdLocking.HeaderRow.Cells[3].FindControl("CB_LockHeader");
                if (chk != null)
                {
                    foreach (GridViewRow gvrow in grdLocking.Rows)
                    {
                        CheckBox CB_Lock = (CheckBox)gvrow.FindControl("CB_Lock");
                        CB_Lock.Checked = chk.Checked ? true : false;
                    }
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void cb_SearchEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_SearchEnable.Checked)
                txtsearch.Enabled = btnsearch.Enabled = true;
            else
                txtsearch.Enabled = btnsearch.Enabled = false;
        }
        [WebMethod]
        public static void UpdateUserLock(string UserID, bool Status)
        {
            try
            {
                if (true)
                {
                    FbpClaimsdalDataContext objDataContext = new FbpClaimsdalDataContext();
                    objDataContext.usp_Fbp_UpdateLocking(UserID.Trim(), Status);
                    
                }
            }
            catch (Exception Ex)
            {  }
        }
    }
}

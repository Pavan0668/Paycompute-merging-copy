using iEmpPower.Old_App_Code.iEmpPowerBL.IT;
using iEmpPower.Old_App_Code.iEmpPowerBO.IT;
using iEmpPower.Old_App_Code.iEmpPowerDAL.IT;
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
using System.Web.Services;

namespace iEmpPower.UI.IT
{
    public partial class IT_AdminLocking : System.Web.UI.Page
    {
        protected int PageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadLockGrid();
                ////txtsearch.Enabled = btnsearch.Enabled = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Employee details loaded successfully')", true);
                cb_SearchEnable.Enabled = true;
            }
        }

        private void LoadLockGrid()
        {
            try
            {
                int? RecordCountl = 0;
                ITbl itblObj = new ITbl();
                List<ITbo> itboObj = new List<ITbo>();
                List<ITbo> itboObj1 = new List<ITbo>();
                string ApproverId = User.Identity.Name;

                itboObj1 = itblObj.Load_IT_Locking("NO");

                if (itboObj1 == null || itboObj1.Count == 0)
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
                    grdLocking.DataSource = itboObj1;
                    grdLocking.SelectedIndex = -1;
                    grdLocking.DataBind();
                    // PopulatePager(RecordCountl, PageIndex);
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ITdalDataContext objDataContext = new ITdalDataContext();
                foreach (GridViewRow gvrow in grdLocking.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("CB_Lock");
                    CheckBox chk80 = (CheckBox)gvrow.FindControl("CB_CA80");
                    CheckBox chk80c = (CheckBox)gvrow.FindControl("CB_CA80c");
                    string perner = gvrow.Cells[1].Text.ToString().Trim();

                    ////objDataContext.usp_IT_UpdateLocking(perner, chk.Checked, chk80.Checked, chk80c.Checked);


                    if (chk != null & chk.Checked)
                    {

                        objDataContext.usp_IT_UpdateLocking(perner, chk.Checked, chk80.Checked, chk80c.Checked);

                    }
                    else
                    {
                        objDataContext.usp_IT_UpdateLocking(perner, chk.Checked, chk80.Checked, chk80c.Checked);

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


                    ITbl itblObj = new ITbl();
                    List<ITbo> itboObj = new List<ITbo>();
                    List<ITbo> itboObj1 = new List<ITbo>();

                    itboObj1 = itblObj.Load_IT_Locking(textSearch);

                    if (itboObj1 == null || itboObj1.Count == 0)
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
                        grdLocking.DataSource = itboObj1;
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
                switch (e.CommandName.ToUpper())
                {
                    case "RESET":
                        ITbl itobjBL = new ITbl();
                        bool? status = false;
                        int rowindex = int.Parse(e.CommandArgument.ToString());
                        itobjBL.IT_Set_Details(0, grdLocking.DataKeys[int.Parse(e.CommandArgument.ToString())]["PERNR"].ToString().Trim(), 1, 0, ref status);
                        if (status == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Slab is reset!!');", true);
                            Label sts = (Label)grdLocking.Rows[rowindex].Cells[4].FindControl("lblstatus");
                            LinkButton lnkreset = (LinkButton)grdLocking.Rows[rowindex].Cells[4].FindControl("linkresetslab");
                            lnkreset.Visible = false;
                            sts.Text = "Slab Not Set";
                            //Response.Redirect("~/" + HttpContext.Current.Request.Url.AbsolutePath, false);
                            //LoadLockGrid();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Something Wrong!!');", true);
                            // Response.Redirect("~/UI/Default.aspx", false);
                            //LoadLockGrid();
                        }
                        break;
                }
            }
            catch (Exception ex) { }
        }

        protected void lnkExcelTepDwnld_Click(object sender, EventArgs e)
        {

            string filePath = "~/ITDoc/IT_Emp_Excel_Upload.xls";
            //Response.ContentType = ContentType;
            Response.ContentType = "application/octet-stream";
            // Response.ContentType = "application/x-download";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected DataTable EmpData()
        {
            DataTable dtExcelData1 = new DataTable();

            dtExcelData1.Columns.AddRange(new DataColumn[4]
                    {
                     new DataColumn("Employee_ID",typeof(string)),
                     new DataColumn("Lock_Unlock",typeof(string)),
                     new DataColumn("Consider_Actuals_Sec_80",typeof(string)),
                     new DataColumn("Consider_Actuals_Sec_80C",typeof(string))
                     });
            return dtExcelData1;
        }

        protected void btnExcelProceed_Click(object sender, EventArgs e)
        {
            try
            {
                string excelPath = Server.MapPath("~/ITDoc/Temp/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(uflEmpData.FileName) + "-" + DateTime.Now.ToString("yyyy_MM_dd") + Path.GetExtension(uflEmpData.FileName));
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

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Employee_ID,Lock_Unlock,Consider_Actuals_Sec_80,Consider_Actuals_Sec_80C FROM  [Emp_Info$] where 'Employee_ID != '''", excel_con))
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
                                CheckBox chk80 = grdLocking.Rows[j].Cells[1].FindControl("CB_CA80") as CheckBox;
                                CheckBox chk80c = grdLocking.Rows[j].Cells[1].FindControl("CB_CA80c") as CheckBox;
                                chk.Checked = dtExcelData.Rows[i]["Lock_Unlock"].ToString().Trim() == "1" ? true : false;
                                chk80.Checked = dtExcelData.Rows[i]["Consider_Actuals_Sec_80"].ToString().Trim() == "1" ? true : false;
                                chk80c.Checked = dtExcelData.Rows[i]["Consider_Actuals_Sec_80C"].ToString().Trim() == "1" ? true : false;
                                break;
                            }
                        }
                    }
                    //File.Delete(Path.Combine(excelPath));
                }
            }

            catch (Exception ex) { }


        }


        protected void btnexpXL_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow GvRow in grdLocking.Rows)
                {

                    LinkButton linkresetslab = (LinkButton)GvRow.FindControl("linkresetslab");
                    //Label lblRDwnld = (Label)GvRow.FindControl("lblRDwnld");
                    linkresetslab.Visible = false;

                    // lblRDwnld.Text = lblRDwnld.Text != "" ? "File uploaded" : "No file uploaded";

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
                string renderedGridView = "IT Details <br/>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "IT.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();
            }
            catch (Exception ex) { }
        }

        protected void CB_CA80c_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox cb = (CheckBox)sender;
                GridViewRow gvr = (GridViewRow)cb.NamingContainer;
                int rowindex = gvr.RowIndex;

                ITdalDataContext objDataContext = new ITdalDataContext();

                CheckBox chk = (CheckBox)grdLocking.Rows[rowindex].Cells[3].FindControl("CB_Lock");
                CheckBox chk80 = (CheckBox)grdLocking.Rows[rowindex].Cells[5].FindControl("CB_CA80");
                CheckBox chk80c = (CheckBox)grdLocking.Rows[rowindex].Cells[6].FindControl("CB_CA80c");
                objDataContext.usp_IT_UpdateLocking(grdLocking.DataKeys[rowindex]["PERNR"].ToString().Trim(), chk.Checked, chk80.Checked, chk80c.Checked);
                if (chk80c.Checked)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Sec 80C Locked Successfully')", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Sec 80C Unlocked Successfully')", true);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void CB_CA80_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox cb = (CheckBox)sender;
                GridViewRow gvr = (GridViewRow)cb.NamingContainer;
                int rowindex = gvr.RowIndex;

                ITdalDataContext objDataContext = new ITdalDataContext();

                CheckBox chk = (CheckBox)grdLocking.Rows[rowindex].Cells[3].FindControl("CB_Lock");
                CheckBox chk80 = (CheckBox)grdLocking.Rows[rowindex].Cells[5].FindControl("CB_CA80");
                CheckBox chk80c = (CheckBox)grdLocking.Rows[rowindex].Cells[6].FindControl("CB_CA80c");
                objDataContext.usp_IT_UpdateLocking(grdLocking.DataKeys[rowindex]["PERNR"].ToString().Trim(), chk.Checked, chk80.Checked, chk80c.Checked);
                if (chk80.Checked)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Sec 80 Locked Successfully')", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Sec 80 Unlocked Successfully')", true);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void CB_Lock_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox cb = (CheckBox)sender;
                GridViewRow gvr = (GridViewRow)cb.NamingContainer;
                int rowindex = gvr.RowIndex;

                ITdalDataContext objDataContext = new ITdalDataContext();

                CheckBox chk = (CheckBox)grdLocking.Rows[rowindex].Cells[3].FindControl("CB_Lock");
                CheckBox chk80 = (CheckBox)grdLocking.Rows[rowindex].Cells[5].FindControl("CB_CA80");
                CheckBox chk80c = (CheckBox)grdLocking.Rows[rowindex].Cells[6].FindControl("CB_CA80c");
                objDataContext.usp_IT_UpdateLocking(grdLocking.DataKeys[rowindex]["PERNR"].ToString().Trim(), chk.Checked, chk80.Checked, chk80c.Checked);
                if (chk.Checked)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Locked Successfully')", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Unlocked Successfully')", true);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void CB_l80cHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)grdLocking.HeaderRow.Cells[6].FindControl("CB_l80cHeader");
                if (chk != null)
                {
                    foreach (GridViewRow gvrow in grdLocking.Rows)
                    {
                        CheckBox CB_CA80 = (CheckBox)gvrow.FindControl("CB_CA80c");
                        CB_CA80.Checked = chk.Checked ? true : false;
                    }
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void CB_l80Header_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)grdLocking.HeaderRow.Cells[5].FindControl("CB_l80Header");
                if (chk != null)
                {
                    foreach (GridViewRow gvrow in grdLocking.Rows)
                    {
                        CheckBox CB_CA80c = (CheckBox)gvrow.FindControl("CB_CA80");
                        CB_CA80c.Checked = chk.Checked ? true : false;
                    }
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
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
        public static void UpdateUserLock(string UserID, bool LockIT, bool Sec80, bool Sec80C)
        {
            try
            {
                if (true)
                {
                    ITdalDataContext objDataContext = new ITdalDataContext();
                    objDataContext.usp_IT_UpdateLocking(UserID.Trim(), LockIT, Sec80, Sec80C);

                }
            }
            catch (Exception Ex)
            { }
        }
    }
}
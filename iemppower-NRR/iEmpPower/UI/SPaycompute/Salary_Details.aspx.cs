using iEmpPower.Old_App_Code.iEmpPowerBL.SPaycompute;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute.SPayc_Collection_BO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.SPaycompute;
using iEmpPowerMaster_Load;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace iEmpPower.UI.SPaycompute
{
    public partial class Salary_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HideTabs();
                view1.Visible = true;
                Tab1.CssClass = "nav-link active p-2";

                txt_salary_formonth.Text = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
                btn_Save_SalarytoDB.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                disp_mnth.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                btn_cancel_ssave.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                salaryhistory_pgload();



                bool? status = false;

                SPayc_BL objPaycbl = new SPayc_BL();
                objPaycbl.Check_paroll_activity(Session["CompCode"].ToString(), "", ref status);
                if (status == true)
                {
                    pgload_Salary_GV();
                    disp_mnth.Visible = true;
                    buttonset.Visible = false;
                    LBL_bind_Smonth.Visible = true;
                    //GV_enableedit_purpose.Columns[4].Visible = true;
                    //GV_enableedit_purpose.Columns[5].Visible = false;
                    //GV_enableedit_purpose.Columns[6].Visible = true;
                    //exitedit();
                    monttxt.Visible = false;
                    txtslry_saveexisting_mnth.Visible = false;
                    searchdiv.Visible = GV_loademp_slry.Rows.Count > 0 ? true : false;
                    bind_emp_DDL_slry();
                    errormssg_srtpayrol.Visible = false;
                    btnedit.Visible = false;
                    btn_updateslry.Visible = false;
                    btn_slryupdate_cncl.Visible = false;
                }

                else
                {

                    SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                    objspaylst = objPaycbl.set_createdbyDDL("", "", "", Session["CompCode"].ToString(), 7);

                    GV_enableedit_purpose.DataSource = objspaylst;
                    GV_enableedit_purpose.DataBind();
                    

                    Session["GVSLryedit"] = objspaylst;

                    SPayc_Collection_BO objcllbo = new SPayc_Collection_BO();
                    objcllbo = (SPayc_Collection_BO)Session["GVSLryedit"];
                    foreach (SPaycompute_BO objBo in objcllbo)
                    {
                        SPaycompute_BO objBo1 = new SPaycompute_BO();
                        LBL_bind_Smonth.Text = objBo.MNTH;
                    }
                    ViewState["month"] = LBL_bind_Smonth.Text;                   
                    Session["runpayroll"] = objspaylst;

                    LBL_bind_Smonth.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;
                    monttxt.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;
                    buttonset.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;
                    disp_mnth.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;
                    searchdiv.Visible = false;
                    GV_search.Visible = false;
                    GV_viewsalaryindetail.Visible = false;
                    GV_viewsaldedtn.Visible = false;
                    errormssg_srtpayrol.Visible = true;
                    btnedit.Visible = false;
                    btn_updateslry.Visible = false;
                    btn_slryupdate_cncl.Visible = false;

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_enableedit_purpose.Rows)
                    {
                        for (int i = 0; i < GV_enableedit_purpose.Rows.Count; i++)
                        {
                            Label lbledtRowNumber = (Label)GV_enableedit_purpose.Rows[i].FindControl("lbledtRowNumber");
                            if (i == 0)
                            {
                                frow = lbledtRowNumber.Text;
                            }
                            if (i == GV_enableedit_purpose.Rows.Count - 1)
                            {
                                lrow = lbledtRowNumber.Text;
                            }
                        }
                    }
                    dvcntedt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                    dvcntedt.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;
                }
            }
        }



        public void salaryhistory_pgload()
        {

            DDL_userIDs.Visible = false;
            txt_sech_Salary_bymnth.Visible = true;
            txt_sech_Salary_bymnth.Text = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");

            string month = txt_sech_Salary_bymnth.Text.Trim();
            string Createby = DDL_userIDs.SelectedValue.ToString();
            string empid = DDL_userIDs.SelectedValue.ToString();

            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
            SPayc_BL objPaycbl = new SPayc_BL();
            objspaylst = objPaycbl.set_createdbyDDL(month,"","", Session["CompCode"].ToString(),1);

            GV_Salaruy_uploadhstry.DataSource = objspaylst;
            GV_Salaruy_uploadhstry.DataBind();

            string frow = "", lrow = "";  ////Row count

            foreach (GridViewRow row in GV_Salaruy_uploadhstry.Rows)
            {
                for (int i = 0; i < GV_Salaruy_uploadhstry.Rows.Count; i++)
                {
                    Label lblhstryRowNumber = (Label)GV_Salaruy_uploadhstry.Rows[i].FindControl("lblhstryRowNumber");
                    if (i == 0)
                    {
                        frow = lblhstryRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                    }
                    if (i == GV_Salaruy_uploadhstry.Rows.Count - 1)
                    {
                        lrow = lblhstryRowNumber.Text;
                    }
                }
            }
            dvcnthsrty.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
            dvcnthsrty.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
            btn_export_slryhsry.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
        }




        public void pgload_Salary_GV()
        {
            try
            {
                GV_loademp_slry.DataSource = null;
                GV_loademp_slry.DataBind();
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.set_createdbyDDL("", "", "", Session["CompCode"].ToString(), 2);

                GV_loademp_slry.DataSource = objspaylst;
                GV_loademp_slry.DataBind();
                Session["GVSLryedit"] = objspaylst;

                SPayc_Collection_BO objcllbo = new SPayc_Collection_BO();
                objcllbo = (SPayc_Collection_BO)Session["GVSLryedit"];
                foreach (SPaycompute_BO objBo in objcllbo)
                {
                    SPaycompute_BO objBo1 = new SPaycompute_BO();
                    LBL_bind_Smonth.Text = objBo.MNTH;
                }

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_loademp_slry.Rows)
                {
                    for (int i = 0; i < GV_loademp_slry.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_loademp_slry.Rows[i].FindControl("lblRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == GV_loademp_slry.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                divcnt.Visible = GV_loademp_slry.Rows.Count > 0 ? true : false;
         
            }
            catch (Exception ex)
            {

            }

        }

        public void bind_emp_DDL_slry()
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.set_createdbyDDL("", "", "", Session["CompCode"].ToString(), 5);
                DDL_editemp_slry.DataSource = null;
                DDL_editemp_slry.DataBind();

                DDL_editemp_slry.DataSource = objspaylst;
                DDL_editemp_slry.DataTextField = "col1";
                DDL_editemp_slry.DataValueField = "TXT";
                DDL_editemp_slry.DataBind();
                DDL_editemp_slry.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - Select Employee - ", "0"));
            }
            catch (Exception ex)
            {

            }
        }


        protected void GV_enableedit_purpose_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                bool? status = false;

                SPayc_BL objPaycbl = new SPayc_BL();
                objPaycbl.Check_paroll_activity(Session["CompCode"].ToString(), "", ref status);
                if (status == true)
                {
                    SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();                  
                    objspaylst = objPaycbl.set_createdbyDDL("", "", "", Session["CompCode"].ToString(), 7);
                    GV_enableedit_purpose.DataSource = objspaylst;
                    GV_enableedit_purpose.PageIndex = e.NewPageIndex;
                    GV_enableedit_purpose.DataBind();
                    Session["GVSLryeditA"] = objspaylst;

                    SPayc_Collection_BO objcllbo = new SPayc_Collection_BO();
                    objcllbo = (SPayc_Collection_BO)Session["GVSLryeditA"];
                    foreach (SPaycompute_BO objBo in objcllbo)
                    {
                        SPaycompute_BO objBo1 = new SPaycompute_BO();
                        LBL_bind_Smonth.Text = objBo.MNTH;
                    }



                    disp_mnth.Visible = true;
                    buttonset.Visible = false;
                    //txt_editslrygv.Visible = false;
                    LBL_bind_Smonth.Visible = true;
                    //GV_enableedit_purpose.Columns[4].Visible = true;
                    //GV_enableedit_purpose.Columns[5].Visible = false;
                    //GV_enableedit_purpose.Columns[6].Visible = true;
                    //exitedit();
                    monttxt.Visible = false;
                    txtslry_saveexisting_mnth.Visible = false;
                    bind_emp_DDL_slry();
                    searchdiv.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_enableedit_purpose.Rows)
                    {
                        for (int i = 0; i < GV_enableedit_purpose.Rows.Count; i++)
                        {
                            Label lbledtRowNumber = (Label)GV_enableedit_purpose.Rows[i].FindControl("lbledtRowNumber");
                            if (i == 0)
                            {
                                frow = lbledtRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_enableedit_purpose.Rows.Count - 1)
                            {
                                lrow = lbledtRowNumber.Text;
                            }
                        }
                    }
                    dvcntedt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                    dvcntedt.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;
                }

                else
                {

                    SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                    objspaylst = objPaycbl.set_createdbyDDL("", "", "", Session["CompCode"].ToString(), 7);

                    GV_enableedit_purpose.DataSource = objspaylst;
                    GV_enableedit_purpose.PageIndex = e.NewPageIndex;
                    GV_enableedit_purpose.DataBind();
                    //GV_enableedit_purpose.Columns[4].Visible = false;
                    //GV_enableedit_purpose.Columns[5].Visible = true;
                    //GV_enableedit_purpose.Columns[6].Visible = false;
                    LBL_bind_Smonth.Text = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
                    Session["runpayroll"] = objspaylst;
                    disp_mnth.Visible = true;
                    //txt_editslrygv.Visible = true;
                    LBL_bind_Smonth.Visible = true;
                    monttxt.Visible = true;
                    buttonset.Visible = true;
                    searchdiv.Visible = false;
                    GV_search.Visible = false;

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_enableedit_purpose.Rows)
                    {
                        for (int i = 0; i < GV_enableedit_purpose.Rows.Count; i++)
                        {
                            Label lbledtRowNumber = (Label)GV_enableedit_purpose.Rows[i].FindControl("lbledtRowNumber");
                            if (i == 0)
                            {
                                frow = lbledtRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_enableedit_purpose.Rows.Count - 1)
                            {
                                lrow = lbledtRowNumber.Text;
                            }
                        }
                    }
                    dvcntedt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                    dvcntedt.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;


                }

                

            }

              
            catch (Exception ex)
            {

            }
        }


        protected void LK_dwnld_slrytemplt_Click(object sender, EventArgs e)
        {
            try
            {
                 bool? sts = false;
                 SPaycompute_BO ObjBo = new SPaycompute_BO();
                 SPayc_BL ObjBl = new SPayc_BL();
                ObjBl.Check_comp_salmap(Session["CompCode"].ToString(), 1, ref sts);

                if (sts == true)
                {

                    string strURL = "~/Paycompute_Excel_Templates/Dowload_Excel/Salary_Structure_Template.xlsx";
                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.ContentType = "application/" + Path.GetExtension(strURL);
                    response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(strURL) + "\"" + DateTime.Now.ToShortDateString() + "\"");
                    byte[] data = req.DownloadData(Server.MapPath(strURL));
                    response.BinaryWrite(data);
                    response.End();
                }
                
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please map required salary components in masters to download template');", true);
                }


            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_upld_slrytemplt_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_uploadxl_mssg.Text = "";
                GV_upload_slry_details.DataSource = null;
                GV_upload_slry_details.DataBind();

                string strFromDate = txt_salary_formonth.Text;
                string strToDate = DateTime.Now.ToString("MM-yyyy");

                DateTime FromDate = DateTime.ParseExact(strFromDate, "MM-yyyy", null);
                DateTime ToDate = DateTime.ParseExact(strToDate, "MM-yyyy", null);


                int result = DateTime.Compare(FromDate, ToDate);

                if (result <= 0)
                {
                    bool? status = false;
                    SPayc_BL objPaycbl1 = new SPayc_BL();
                    objPaycbl1.Check_paroll_checkmonthxlup(Session["CompCode"].ToString(), txt_salary_formonth.Text, ref status);
                    if (status == true)
                    {
                        lbl_uploadxl_mssg.Text="The selected month's payroll process is already completed";
                        txt_salary_formonth.Text = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
                        btn_Save_SalarytoDB.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                        btn_cancel_ssave.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                        disp_mnth.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                    }
                    else
                    {
                        lbl_uploadxl_mssg.Text = "";
                        string excelPath = Server.MapPath("~/Paycompute_Excel_Templates/Upload_Excel/") + Path.GetFileName(flup_slrydata_mstrs.PostedFile.FileName);
                        flup_slrydata_mstrs.SaveAs(excelPath);

                        string conString = string.Empty;
                        string extension = Path.GetExtension(flup_slrydata_mstrs.PostedFile.FileName);
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
                            string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                            DataTable dtExcelData = new DataTable();

                            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                            dtExcelData.Columns.AddRange(new DataColumn[5]
                    { 
                    new DataColumn("SL_NO", typeof(int)),
                    new DataColumn("Employee_ID", typeof(string)),
                    new DataColumn("Employee_Name", typeof(string)),
                    new DataColumn("Salary_Component",typeof(string)),
                    new DataColumn("Salary_Rate",typeof(int))                   
                     });


                            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT SL_NO,Employee_ID,Employee_Name,Salary_Component,Salary_Rate FROM [" + sheet1 + "] where 'Employee_ID != '''", excel_con))
                            {
                                oda.Fill(dtExcelData);
                                for (int i = dtExcelData.Rows.Count - 1; i >= 0; i += -1)
                                {
                                    DataRow row = dtExcelData.Rows[i];
                                    if (row[0] == null)
                                    {
                                        dtExcelData.Rows.Remove(row);
                                    }
                                    else if (string.IsNullOrEmpty(row[0].ToString()))
                                    {
                                        dtExcelData.Rows.Remove(row);
                                    }
                                }
                                dtExcelData.AcceptChanges();

                            }
                            excel_con.Close();

                            //LBL_bind_Smonth.Text = txt_salary_formonth.Text;
                            GV_upload_slry_details.DataSource = dtExcelData;
                            GV_upload_slry_details.DataBind();

                            btn_Save_SalarytoDB.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                            btn_cancel_ssave.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                            disp_mnth.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;

                            Session["GVBind"] = dtExcelData;
                            ViewState["SD_Month"] = txt_salary_formonth.Text;
                            //txt_salary_formonth.Text = "";


                        }
                    }
                }
                else
                {
                    
                    lbl_uploadxl_mssg.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Payroll cannot be run for the future month');", true);
                    txt_salary_formonth.Text = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
                    btn_Save_SalarytoDB.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                    btn_cancel_ssave.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                    disp_mnth.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_Save_SalarytoDB_Click(object sender, EventArgs e)
         {
            try
            {
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objSpayc.LOGIN_PERNR = User.Identity.Name.Trim();

                if (Session["GVBind"] != null)
                {

                    using (DataTable Dt = (DataTable)Session["GVBind"])
                    {



                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {



                            objSpayc.TXT = Dt.Rows[i]["Salary_Component"].ToString().Trim();




                            objSpayc.col1 = Dt.Rows[i]["Salary_Rate"].ToString().Trim() == null ? "" : Dt.Rows[i]["Salary_Rate"].ToString().Trim() ;



                            objSpayc.CCD = Session["CompCode"].ToString();



                            objSpayc.MNTH = ViewState["SD_Month"].ToString();



                            objSpayc.EID = Dt.Rows[i]["Employee_ID"].ToString().Trim().ToLower();

                            objSpayc.id1 = 0;

                            objPaycbl.Save_Salary_EmpRates(objSpayc, 1);


                        }


                        salaryhistory_pgload();
                        //SPaycompute_BO objBo2 = new SPaycompute_BO();
                        //SPayc_BL objPaycbl2 = new SPayc_BL();
                        //objBo2.CCD = Session["CompCode"].ToString().Trim();
                        //objBo2.id1 = 0;
                        //objBo2.MNTH = ViewState["SD_Month"].ToString();
                        //objBo2.TXT = Session["CompCode"].ToString().Trim();
                        //objBo2.id2 = 1;
                        //objPaycbl2.Save_payroll_activity(objBo2,1);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Salary details for the month of " + ViewState["SD_Month"].ToString() + " has been submited succesfully');", true);

                        
                        //string[] MsgCC = { };
                        //string mail = "";
                        ////SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                        //SPaycomputeDataContext objSPayDatacontext = new SPaycomputeDataContext();
                        //foreach (var ENrow in objSPayDatacontext.payc_get_iemppayroll_admin())
                        //{
                        //    mail = ENrow.Admin_Email;
                            
                        //}
                        //ViewState["mail_salryadmin"] = mail.ToString().Trim();
                        
                        //--------------------------- SENDING EMAIL NOTIFICATION - TO USERS ---------------------------------------

                        //string Mailbody = string.Empty;
                        //string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/PayrollActivity.html");
                        //Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                        //masterbl.SendMail(ViewState["mail_salryadmin"].ToString(), MsgCC, "Payroll Activity - " + Session["Empname"].ToString().Trim() + ".",
                        //Mailbody.Replace("##CLIENT##", Session["Empname"].ToString().Trim()).Replace("##MONTH##", objBo2.MNTH.ToString()));
                     
                        btn_Save_SalarytoDB.Visible = false;
                        btn_cancel_ssave.Visible = false;
                        GV_upload_slry_details.DataSource = null;
                        GV_upload_slry_details.DataBind();
                        disp_mnth.Visible = false;
                        txt_salary_formonth.Text = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");


                        bool? status = false;
                        SPayc_BL objPaycbl1 = new SPayc_BL();
                        objPaycbl1.Check_paroll_activity(Session["CompCode"].ToString(), "", ref status);
                        if (status == true)
                        {
                            pgload_Salary_GV();
                            disp_mnth.Visible = true;
                            buttonset.Visible = false;
                            LBL_bind_Smonth.Visible = true;
                            monttxt.Visible = false;
                            txtslry_saveexisting_mnth.Visible = false;

                        }

                        else
                        {

                            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                            objspaylst = objPaycbl.set_createdbyDDL("", "", "", Session["CompCode"].ToString(), 7);

                            GV_enableedit_purpose.DataSource = objspaylst;
                            GV_enableedit_purpose.DataBind();

                            Session["GVSLryedit"] = objspaylst;

                            SPayc_Collection_BO objcllbo = new SPayc_Collection_BO();
                            objcllbo = (SPayc_Collection_BO)Session["GVSLryedit"];
                            foreach (SPaycompute_BO objBo in objcllbo)
                            {
                                SPaycompute_BO objBo1 = new SPaycompute_BO();
                                LBL_bind_Smonth.Text = objBo.MNTH;
                            }
                            ViewState["month"] = LBL_bind_Smonth.Text;
                            Session["runpayroll"] = objspaylst;

                            LBL_bind_Smonth.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;
                            monttxt.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;
                            buttonset.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;
                            disp_mnth.Visible = GV_enableedit_purpose.Rows.Count > 0 ? true : false;


                        }
                       



                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_cancel_ssave_Click(object sender, EventArgs e)
        {
            try
            {
                GV_upload_slry_details.DataSource = null;
                GV_upload_slry_details.DataBind();

                btn_Save_SalarytoDB.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                disp_mnth.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                btn_cancel_ssave.Visible = GV_upload_slry_details.Rows.Count > 0 ? true : false;
                txt_salary_formonth.Text = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
            }
            catch (Exception ex)
            {

            }

        }

        
        protected void DDL_View_salaryrts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_View_salaryrts.SelectedValue == "1")
                {
                    txt_sech_Salary_bymnth.Visible = true;
                    DDL_userIDs.Visible = false;
                }

                if (DDL_View_salaryrts.SelectedValue == "3")
                {
                    DDL_userIDs.Visible = true;
                    bind_emp_DDL();
                    txt_sech_Salary_bymnth.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }


        //----------------------------------------------------------- BIND USERID TO DDL -----------------------------------------------------
        public void bind_emp_DDL()
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.set_createdbyDDL("", "", "", Session["CompCode"].ToString(), 5);
                DDL_userIDs.DataSource = null;
                DDL_userIDs.DataBind();

                DDL_userIDs.DataSource = objspaylst;
                DDL_userIDs.DataTextField = "col1";
                DDL_userIDs.DataValueField = "TXT";
                DDL_userIDs.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        //----------------------------------------------------------- BIND EMPID TO DDL -----------------------------------------------------
        public void bind_Salaryuserid_DDL()
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.set_createdbyDDL("", "", "", Session["CompCode"].ToString(), 4);
                DDL_userIDs.DataSource = null;
                DDL_userIDs.DataBind();

                DDL_userIDs.DataSource = objspaylst;
                DDL_userIDs.DataTextField = "Created_BY";
                DDL_userIDs.DataValueField = "Created_BY";
                DDL_userIDs.DataBind();
            }
            catch (Exception ex)
            {

            }
        }


        protected void btn_salary_history_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDL_View_salaryrts.SelectedValue == "1")
                {
                    string month = txt_sech_Salary_bymnth.Text.Trim();
                    string Createby = DDL_userIDs.SelectedValue.ToString();
                    string empid = DDL_userIDs.SelectedValue.ToString();

                    SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                    SPayc_BL objPaycbl = new SPayc_BL();
                    objspaylst = objPaycbl.set_createdbyDDL(month, "", "", Session["CompCode"].ToString(), 1);

                    GV_Salaruy_uploadhstry.DataSource = objspaylst;
                    GV_Salaruy_uploadhstry.DataBind();

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_Salaruy_uploadhstry.Rows)
                    {
                        for (int i = 0; i < GV_Salaruy_uploadhstry.Rows.Count; i++)
                        {
                            Label lblhstryRowNumber = (Label)GV_Salaruy_uploadhstry.Rows[i].FindControl("lblhstryRowNumber");
                            if (i == 0)
                            {
                                frow = lblhstryRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_Salaruy_uploadhstry.Rows.Count - 1)
                            {
                                lrow = lblhstryRowNumber.Text;
                            }
                        }
                    }
                    dvcnthsrty.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                    dvcnthsrty.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
                }

                else if (DDL_View_salaryrts.SelectedValue == "2")
                {
                    string month = txt_sech_Salary_bymnth.Text.Trim();
                    string Createby = DDL_userIDs.SelectedValue.ToString();
                    string empid = DDL_userIDs.SelectedValue.ToString();

                    SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                    SPayc_BL objPaycbl = new SPayc_BL();
                    objspaylst = objPaycbl.set_createdbyDDL("", Createby, "", Session["CompCode"].ToString(), 2);

                    GV_Salaruy_uploadhstry.DataSource = objspaylst;
                    GV_Salaruy_uploadhstry.DataBind();

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_Salaruy_uploadhstry.Rows)
                    {
                        for (int i = 0; i < GV_Salaruy_uploadhstry.Rows.Count; i++)
                        {
                            Label lblhstryRowNumber = (Label)GV_Salaruy_uploadhstry.Rows[i].FindControl("lblhstryRowNumber");
                            if (i == 0)
                            {
                                frow = lblhstryRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_Salaruy_uploadhstry.Rows.Count - 1)
                            {
                                lrow = lblhstryRowNumber.Text;
                            }
                        }
                    }
                    dvcnthsrty.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                    dvcnthsrty.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
                }

                else if (DDL_View_salaryrts.SelectedValue == "3")
                {
                    string month = txt_sech_Salary_bymnth.Text.Trim();
                    string Createby = DDL_userIDs.SelectedValue.ToString();
                    string empid = DDL_userIDs.SelectedValue.ToString();

                    SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                    SPayc_BL objPaycbl = new SPayc_BL();
                    objspaylst = objPaycbl.set_createdbyDDL("", "", empid, Session["CompCode"].ToString(), 3);

                    GV_Salaruy_uploadhstry.DataSource = objspaylst;
                    GV_Salaruy_uploadhstry.DataBind();

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_Salaruy_uploadhstry.Rows)
                    {
                        for (int i = 0; i < GV_Salaruy_uploadhstry.Rows.Count; i++)
                        {
                            Label lblhstryRowNumber = (Label)GV_Salaruy_uploadhstry.Rows[i].FindControl("lblhstryRowNumber");
                            if (i == 0)
                            {
                                frow = lblhstryRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_Salaruy_uploadhstry.Rows.Count - 1)
                            {
                                lrow = lblhstryRowNumber.Text;
                            }
                        }
                    }
                    dvcnthsrty.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                    dvcnthsrty.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
                }
                btn_export_slryhsry.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
            }

            catch (Exception ex)
            {

            }
        }

        protected void GV_Salaruy_uploadhstry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (DDL_View_salaryrts.SelectedValue == "1")
                {
                    string month = txt_sech_Salary_bymnth.Text.Trim();
                    string Createby = DDL_userIDs.SelectedValue.ToString();
                    string empid = DDL_userIDs.SelectedValue.ToString();
                    GV_Salaruy_uploadhstry.PageIndex = e.NewPageIndex;
                    SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                    SPayc_BL objPaycbl = new SPayc_BL();
                    objspaylst = objPaycbl.set_createdbyDDL(month, "", "", Session["CompCode"].ToString(), 1);

                    GV_Salaruy_uploadhstry.DataSource = objspaylst;
                    GV_Salaruy_uploadhstry.DataBind();

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_Salaruy_uploadhstry.Rows)
                    {
                        for (int i = 0; i < GV_Salaruy_uploadhstry.Rows.Count; i++)
                        {
                            Label lblhstryRowNumber = (Label)GV_Salaruy_uploadhstry.Rows[i].FindControl("lblhstryRowNumber");
                            if (i == 0)
                            {
                                frow = lblhstryRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_Salaruy_uploadhstry.Rows.Count - 1)
                            {
                                lrow = lblhstryRowNumber.Text;
                            }
                        }
                    }
                    dvcnthsrty.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                    dvcnthsrty.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
                }

                else if (DDL_View_salaryrts.SelectedValue == "2")
                {
                    string month = txt_sech_Salary_bymnth.Text.Trim();
                    string Createby = DDL_userIDs.SelectedValue.ToString();
                    string empid = DDL_userIDs.SelectedValue.ToString();
                    GV_Salaruy_uploadhstry.PageIndex = e.NewPageIndex;
                    SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                    SPayc_BL objPaycbl = new SPayc_BL();
                    objspaylst = objPaycbl.set_createdbyDDL("", Createby, "", Session["CompCode"].ToString(), 2);

                    GV_Salaruy_uploadhstry.DataSource = objspaylst;
                    GV_Salaruy_uploadhstry.DataBind();

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_Salaruy_uploadhstry.Rows)
                    {
                        for (int i = 0; i < GV_Salaruy_uploadhstry.Rows.Count; i++)
                        {
                            Label lblhstryRowNumber = (Label)GV_Salaruy_uploadhstry.Rows[i].FindControl("lblhstryRowNumber");
                            if (i == 0)
                            {
                                frow = lblhstryRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_Salaruy_uploadhstry.Rows.Count - 1)
                            {
                                lrow = lblhstryRowNumber.Text;
                            }
                        }
                    }
                    dvcnthsrty.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                    dvcnthsrty.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
                }

                else if (DDL_View_salaryrts.SelectedValue == "3")
                {
                    string month = txt_sech_Salary_bymnth.Text.Trim();
                    string Createby = DDL_userIDs.SelectedValue.ToString();
                    string empid = DDL_userIDs.SelectedValue.ToString();
                    GV_Salaruy_uploadhstry.PageIndex = e.NewPageIndex;
                    SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                    SPayc_BL objPaycbl = new SPayc_BL();
                    objspaylst = objPaycbl.set_createdbyDDL("", "", empid, Session["CompCode"].ToString(), 3);

                    GV_Salaruy_uploadhstry.DataSource = objspaylst;
                    GV_Salaruy_uploadhstry.DataBind();

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_Salaruy_uploadhstry.Rows)
                    {
                        for (int i = 0; i < GV_Salaruy_uploadhstry.Rows.Count; i++)
                        {
                            Label lblhstryRowNumber = (Label)GV_Salaruy_uploadhstry.Rows[i].FindControl("lblhstryRowNumber");
                            if (i == 0)
                            {
                                frow = lblhstryRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_Salaruy_uploadhstry.Rows.Count - 1)
                            {
                                lrow = lblhstryRowNumber.Text;
                            }
                        }
                    }
                    dvcnthsrty.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                    dvcnthsrty.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
                }
                btn_export_slryhsry.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
            }

            catch (Exception ex)
            {

            }
        }

      
        protected void btn_Saveexisting_slry_Click(object sender, EventArgs e)
        {
            try
            {
                lblerrormssg.Text = "";
                string strFromDate = txtslry_saveexisting_mnth.Text;
                string strToDate = DateTime.Now.ToString("MM-yyyy");

                DateTime FromDate = DateTime.ParseExact(strFromDate, "MM-yyyy", null);
                DateTime ToDate = DateTime.ParseExact(strToDate, "MM-yyyy", null);


                int result = DateTime.Compare(FromDate,ToDate);

                if (result <= 0)
                {
                   bool? status = false;
                   SPayc_BL objPaycbl1 = new SPayc_BL();
                   objPaycbl1.Check_paroll_checkmonth(Session["CompCode"].ToString(),txtslry_saveexisting_mnth.Text, ref status);
                   if (status == true)
                   {
                       //ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('The selected month's payroll process is already completed');", true);
                       lblerrormssg.Text = "The selected month's payroll process is already completed";                      
                       txtslry_saveexisting_mnth.Text = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
                   }
                else
                {
                    lblerrormssg.Text = "";
                    SPaycompute_BO objSpayc = new SPaycompute_BO();
                    SPayc_BL objPaycbl = new SPayc_BL();



                    SPayc_Collection_BO objcllbo = new SPayc_Collection_BO();
                    objcllbo = (SPayc_Collection_BO)Session["runpayroll"];
                    if (objcllbo != null)
                    {
                       // lblerrormssg.Text = "";
                        Session["SD_MonthA"] = txtslry_saveexisting_mnth.Text;
                        foreach (SPaycompute_BO objBo in objcllbo)
                        {
                            SPaycompute_BO objBo1 = new SPaycompute_BO();

                            objBo1.LOGIN_PERNR = User.Identity.Name.Trim();

                            objBo1.TXT = objBo.CCD;

                            objBo1.col1 = objBo.col10;

                            objBo1.CCD = objBo.col11;

                            objBo1.MNTH = Session["SD_MonthA"].ToString();

                            objBo1.EID = objBo.TXT;

                            objBo1.id1 = objBo.id1;

                            objPaycbl.Save_Salary_EmpRates(objBo1, 3);
                        }

                    }
                    SPaycompute_BO objBo3 = new SPaycompute_BO();
                    SPayc_BL objPaycbl3 = new SPayc_BL();
                    objBo3.CCD = Session["CompCode"].ToString().Trim();
                    objBo3.id1 = 0;
                    objBo3.MNTH = Session["SD_MonthA"].ToString();
                    objBo3.TXT = Session["CompCode"].ToString().Trim();
                    objBo3.id2 = 1;
                    objPaycbl3.Save_payroll_activity(objBo3, 1);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Salary details for the month of " + Session["SD_MonthA"].ToString() + " has been submited succesfully');", true);


                    GV_enableedit_purpose.DataSource = null;
                    GV_enableedit_purpose.DataBind();
                    GV_enableedit_purpose.Visible = false;               
                       
                    GV_loademp_slry.Visible = true;
                    GV_search.Visible = false;
                    GV_viewsalaryindetail.Visible = false;
                    GV_viewsaldedtn.Visible = false;
                    pgload_Salary_GV();
                    disp_mnth.Visible = true;
                    buttonset.Visible = false;
                    LBL_bind_Smonth.Visible = true;
                    monttxt.Visible = false;
                    txtslry_saveexisting_mnth.Visible = false;
                    searchdiv.Visible = GV_loademp_slry.Rows.Count > 0 ? true : false;
                    bind_emp_DDL_slry();
                    errormssg_srtpayrol.Visible = false;
                    btnedit.Visible = false;
                    btn_updateslry.Visible = false;
                    btn_slryupdate_cncl.Visible = false;
                    dvcntedt.Visible = false;

                        }
                }
                else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Payroll cannot be run for the future month');", true);
                     txtslry_saveexisting_mnth.Text = DateTime.Now.AddMonths(-1).ToString("MM-yyyy");
                  }
            
            }

            catch (Exception ex)
            {

            }


        }

        protected void GV_upload_slry_details_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable Dt = (DataTable)Session["GVBind"];
                GV_upload_slry_details.PageIndex = e.NewPageIndex;
                GV_upload_slry_details.DataSource = Dt;
                GV_upload_slry_details.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_Salaruy_uploadhstry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string ccode = Session["CompCode"].ToString();
                    string emplogin = e.Row.Cells[1].Text.ToString().Trim();
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    e.Row.Cells[1].Text = emplogin.Trim().ToUpper();


                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_enableedit_purpose_RowDataBound(object sender, GridViewRowEventArgs e)
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

        public void serchpageload()
        {
            GV_loademp_slry.Visible = false;
            errormssg_srtpayrol.Visible = false;
            GV_viewsalaryindetail.Visible = true;
            GV_enableedit_purpose.Visible = false;
            GV_search.Visible = false;
            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
            SPayc_BL objPaycbl = new SPayc_BL();
            objspaylst = objPaycbl.set_createdbyDDL("", "", DDL_editemp_slry.SelectedValue.ToString(), Session["CompCode"].ToString(), 9);

            GV_viewsalaryindetail.DataSource = objspaylst;
            GV_viewsalaryindetail.DataBind();
            binggrosssalary();


            SPayc_Collection_BO objspaylst5 = new SPayc_Collection_BO();
            SPayc_BL objPaycbl5 = new SPayc_BL();
            objspaylst5 = objPaycbl5.set_createdbyDDL("", "", DDL_editemp_slry.SelectedValue.ToString(), Session["CompCode"].ToString(), 10);
            GV_viewsaldedtn.DataSource = objspaylst5;
            GV_viewsaldedtn.DataBind();
            bindnetsalarytot();
            GV_viewsaldedtn.Visible = true;

            LBL_bind_Smonth.Visible = true;
            //Session["GVSLryedit"] = objspaylst;

            //SPayc_Collection_BO objcllbo = new SPayc_Collection_BO();
            //objcllbo = (SPayc_Collection_BO)Session["GVSLryedit"];
            //foreach (SPaycompute_BO objBo in objcllbo)
            //{
            //    SPaycompute_BO objBo1 = new SPaycompute_BO();
            //    LBL_bind_Smonth.Text = objBo.MNTH;
            //}
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
             {
                 divcnt.Visible = false;
                 dvcntedt.Visible = false;
                serchpageload();                 
                disp_mnth.Visible = true;
                buttonset.Visible = false;
                LBL_bind_Smonth.Visible = true;
                btnedit.Visible = true;
                monttxt.Visible = false;
                txtslry_saveexisting_mnth.Visible = false;
                searchdiv.Visible = true;
                GV_viewsalaryindetail.Visible = true;
                GV_viewsaldedtn.Visible = true;
                btn_updateslry.Visible = false;
                btn_slryupdate_cncl.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            try
            {
                    GV_loademp_slry.Visible = true;
                    GV_viewsalaryindetail.Visible = false;
                    GV_viewsaldedtn.Visible = false;
                    GV_search.Visible = false;
                    pgload_Salary_GV();
                    disp_mnth.Visible = true;
                    buttonset.Visible = false;
                    LBL_bind_Smonth.Visible = true;
                   
                    monttxt.Visible = false;
                    txtslry_saveexisting_mnth.Visible = false;
                    searchdiv.Visible = true;
                    bind_emp_DDL_slry();
                    DDL_editemp_slry.SelectedValue = "0";
                    errormssg_srtpayrol.Visible = false;
                    btnedit.Visible = false;
                    btn_updateslry.Visible = false;
                    btn_slryupdate_cncl.Visible = false;

            }
            catch (Exception ex)
            {

            }
        }

      

        protected void GV_search_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)e.Row.FindControl("LBL_empidsrch");
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

        protected void btn_salary_viewclr_Click(object sender, EventArgs e)
        {
            try
            {
                salaryhistory_pgload();
                DDL_View_salaryrts.SelectedValue = "1";
            }
            catch (Exception ex)
            {

            }
        }


        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }

        protected void btn_export_slryhsry_Click(object sender, EventArgs e)
        {
            try
            {
                
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


                GV_Salaruy_uploadhstry.AllowPaging = false;
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                if (DDL_View_salaryrts.SelectedValue == "1")
                {
                   
                    objspaylst = objPaycbl.set_createdbyDDL(txt_sech_Salary_bymnth.Text, "", "", Session["CompCode"].ToString(), 1);

                    GV_Salaruy_uploadhstry.DataSource = objspaylst;
                    GV_Salaruy_uploadhstry.DataBind();
                }

                if (DDL_View_salaryrts.SelectedValue == "3")
                {
                    objspaylst = objPaycbl.set_createdbyDDL("", "", DDL_userIDs.SelectedValue, Session["CompCode"].ToString(),3);

                    GV_Salaruy_uploadhstry.DataSource = objspaylst;
                    GV_Salaruy_uploadhstry.DataBind();
                }


                GV_Salaruy_uploadhstry.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                GV_Salaruy_uploadhstry.HeaderStyle.Font.Bold = true;
                GV_Salaruy_uploadhstry.GridLines = GridLines.Both;
                GV_Salaruy_uploadhstry.RenderControl(htw);

                GV_Salaruy_uploadhstry.AllowPaging = true;
             

                htw.WriteBreak();

                string renderedGridView = " " + "<br>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + " SalaryDetails.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.set_createdbyDDL("", "", DDL_editemp_slry.SelectedValue.ToString(), Session["CompCode"].ToString(), 8);
                GV_search.DataSource = objspaylst;
                GV_search.DataBind();
                LBL_bind_Smonth.Visible=true;
            
                GV_search.Visible = true;
                GV_viewsalaryindetail.Visible = false;
                GV_viewsaldedtn.Visible = false;
                GV_search.Columns[4].Visible = true;
                GV_search.Columns[5].Visible = false;
                searchdiv.Visible = true;
                btn_updateslry.Visible = GV_search.Rows.Count > 0 ? true : false;
                btn_slryupdate_cncl.Visible = GV_search.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_loademp_slry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)e.Row.FindControl("LBL_gvempid");
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

        protected void GV_loademp_slry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.set_createdbyDDL("", "", "", Session["CompCode"].ToString(), 2);

                GV_loademp_slry.DataSource = objspaylst;
                GV_loademp_slry.PageIndex = e.NewPageIndex;
                GV_loademp_slry.DataBind();
                Session["GVSLryedit"] = objspaylst;

                SPayc_Collection_BO objcllbo = new SPayc_Collection_BO();
                objcllbo = (SPayc_Collection_BO)Session["GVSLryedit"];
                foreach (SPaycompute_BO objBo in objcllbo)
                {
                    SPaycompute_BO objBo1 = new SPaycompute_BO();
                    LBL_bind_Smonth.Text = objBo.MNTH;
                }

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_loademp_slry.Rows)
                {
                    for (int i = 0; i < GV_loademp_slry.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_loademp_slry.Rows[i].FindControl("lblRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;
                        }
                        if (i == GV_loademp_slry.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                divcnt.Visible = GV_loademp_slry.Rows.Count > 0 ? true : false;
            }

            catch (Exception ex)
            {

            }
        }

        protected void GV_loademp_slry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
               
                switch (e.CommandName.ToUpper())
                {
                    case "VIEWSALARY":
                        btn_updateslry.Visible = false;
                        btn_slryupdate_cncl.Visible = false;
                        GV_loademp_slry.Visible = true;
                        int indexview = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvview = GV_loademp_slry.Rows[indexview];


                        foreach (GridViewRow row in GV_loademp_slry.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(indexview) ?
                                                System.Drawing.ColorTranslator.FromHtml("#ffe6ba") :
                                               System.Drawing.Color.White;
                        }

                        
                   
                        string empid = (GV_loademp_slry.DataKeys[int.Parse(e.CommandArgument.ToString())]["TXT"].ToString().Trim());

                         SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                          SPayc_BL objPaycbl = new SPayc_BL();
                        objspaylst = objPaycbl.set_createdbyDDL("", "", empid, Session["CompCode"].ToString(), 9);
                        GV_viewsalaryindetail.DataSource = objspaylst;
                        GV_viewsalaryindetail.DataBind();
                        binggrosssalary();                        
                        GV_viewsalaryindetail.Visible = true;

                        SPayc_Collection_BO objspaylst3 = new SPayc_Collection_BO();
                        SPayc_BL objPaycbl3 = new SPayc_BL();
                        objspaylst3 = objPaycbl3.set_createdbyDDL("", "", empid, Session["CompCode"].ToString(), 10);
                        GV_viewsaldedtn.DataSource = objspaylst3;
                        GV_viewsaldedtn.DataBind();
                        bindnetsalarytot();
                        GV_viewsaldedtn.Visible = true;


                       LBL_bind_Smonth.Visible = true;
                        GV_search.Visible = false;
                        btn_updateslry.Visible = false;
                        btn_slryupdate_cncl.Visible = false;
                        break;

                    case "EDITSALARY":
                        
                        GV_loademp_slry.Visible = true;
                        
                        int indexedit = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvedit = GV_loademp_slry.Rows[indexedit];

                        foreach (GridViewRow row in GV_loademp_slry.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(indexedit) ?
                                                System.Drawing.ColorTranslator.FromHtml("#ffe6ba") :
                                               System.Drawing.Color.White;

                            }

                        btn_updateslry.Focus();
                   
                        string empidedit = (GV_loademp_slry.DataKeys[int.Parse(e.CommandArgument.ToString())]["TXT"].ToString().Trim());

                        SPayc_Collection_BO objspaylst1 = new SPayc_Collection_BO();
                          SPayc_BL objPaycbl1 = new SPayc_BL();
                          objspaylst1 = objPaycbl1.set_createdbyDDL("", "", empidedit, Session["CompCode"].ToString(), 8);
                        GV_search.DataSource = objspaylst1;
                        GV_search.DataBind();
                        LBL_bind_Smonth.Visible = true;  
                        GV_search.Visible = true;
                        GV_viewsalaryindetail.Visible = false;
                        GV_viewsaldedtn.Visible = false;
                        btn_updateslry.Visible = GV_search.Rows.Count > 0 ? true : false;
                        btn_slryupdate_cncl.Visible = GV_search.Rows.Count > 0 ? true : false;
                        break;

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_updateslry_Click(object sender, EventArgs e)
        {
            try
            {
               
                foreach (GridViewRow row in GV_search.Rows)
                {
                    TextBox slary_rate = (TextBox)row.FindControl("txt_Salaryratesrch");
                    string allwrate = slary_rate.Text;

                    Label ID = (Label)row.FindControl("lbl_DBID");
                    int RowID = Convert.ToInt32(ID.Text);

                    Label empID = (Label)row.FindControl("LBL_empidsrch");
                    string empyid = empID.Text.ToString().Trim();
                    ViewState["empyid"] = empyid.ToString();

                    Label empname = (Label)row.FindControl("LBL_namesrch");
                    string name = empname.Text.ToString();
                    ViewState["empname"] = name.ToString();

                   
                    Label allo = (Label)row.FindControl("LBL_salary_componentssrch");
                    string allowance = allo.Text.ToString().Trim();
                        

                    SPaycompute_BO objSpayc = new SPaycompute_BO();
                    SPayc_BL objPaycbl1 = new SPayc_BL();
                    objSpayc.LOGIN_PERNR = User.Identity.Name.Trim();

                    objSpayc.EID = empyid.ToString();


                    objSpayc.TXT = allowance.ToString();

                    objSpayc.col1 = allwrate.ToString();

                    objSpayc.CCD = Session["CompCode"].ToString();

                    objSpayc.MNTH = LBL_bind_Smonth.Text.ToString();

                    objSpayc.id1 = RowID;

                    objPaycbl1.Save_Salary_EmpRates(objSpayc, 2);
                    
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Salary details of employee " + ViewState["empname"].ToString()+ " has been updated succesfully');", true);
                string eid = ViewState["empyid"].ToString();
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.set_createdbyDDL("", "", eid.ToString(), Session["CompCode"].ToString(), 9);
                GV_viewsalaryindetail.DataSource = objspaylst;
                GV_viewsalaryindetail.DataBind();
                binggrosssalary();


                SPayc_Collection_BO objspaylst4 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl4 = new SPayc_BL();
                objspaylst4 = objPaycbl4.set_createdbyDDL("", "", eid.ToString(), Session["CompCode"].ToString(), 10);
                GV_viewsaldedtn.DataSource = objspaylst4;
                GV_viewsaldedtn.DataBind();
                bindnetsalarytot();
                GV_viewsaldedtn.Visible = true;

                LBL_bind_Smonth.Visible = true;                
                GV_viewsalaryindetail.Visible = true;
                GV_search.Visible = false;                
                GV_loademp_slry.Visible = true;               
                btn_updateslry.Visible = false;
                btn_slryupdate_cncl.Visible = false;
                btnedit.Visible = false;
                salaryhistory_pgload();
                pgload_Salary_GV();

                
               
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_slryupdate_cncl_Click(object sender, EventArgs e)
        {
            try
            {
                GV_loademp_slry.Visible = true;
                GV_search.Visible = false;
                GV_viewsalaryindetail.Visible = false;
                GV_viewsaldedtn.Visible = false;
                pgload_Salary_GV();
                disp_mnth.Visible = true;
                buttonset.Visible = false;
                LBL_bind_Smonth.Visible = true;
                monttxt.Visible = false;
                txtslry_saveexisting_mnth.Visible = false;
                searchdiv.Visible = GV_loademp_slry.Rows.Count > 0 ? true : false;
                bind_emp_DDL_slry();
                errormssg_srtpayrol.Visible = false;
                btnedit.Visible = false;
                btn_updateslry.Visible = false;               
                btn_slryupdate_cncl.Visible = false;
                
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_viewsalaryindetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)e.Row.FindControl("LBL_empidsrch");
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

        protected void HideTabs()
        {
            view1.Visible = false;
            view2.Visible = false;
            view3.Visible = false;

            Tab1.CssClass = "nav-link  p-2";
            Tab2.CssClass = "nav-link  p-2";
            Tab3.CssClass = "nav-link  p-2";
        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = true;
            view2.Visible = false;
            view3.Visible = false;
            Tab1.CssClass = "nav-link active p-2";
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = false;
            view2.Visible = true;
            view3.Visible = false;
            Tab2.CssClass = "nav-link active p-2";
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = false;
            view2.Visible = false;
            view3.Visible = true;
            Tab3.CssClass = "nav-link active p-2";
            btn_export_slryhsry.Visible = GV_Salaruy_uploadhstry.Rows.Count > 0 ? true : false;
        }

        protected void GV_viewsaldedtn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl1 = (Label)e.Row.FindControl("LBL_empiddtn");
                    string ccodedn = Session["CompCode"].ToString();
                    string emplogindn = lbl1.Text;
                    int cntdn = ccodedn.Length;
                    emplogindn = emplogindn.Substring(cntdn);
                    e.Row.Cells[0].Text = emplogindn.Trim().ToUpper();


                }
            }
            catch (Exception ex)
            {

            }
        }


        public void binggrosssalary()
        {
            try
            {

                decimal total = 0;
                decimal avg = 0;
                for (int i = 0; i < GV_viewsalaryindetail.Rows.Count; i++)
                {

                    Label earnings = (Label)GV_viewsalaryindetail.Rows[i].FindControl("lbl_allowance");
                    decimal calc = Convert.ToDecimal(earnings.Text == "" ? "0.00" : earnings.Text);
                    total = total + calc;

                }
                Label gross = ((Label)GV_viewsalaryindetail.FooterRow.FindControl("lbl_grosstotal"));
                gross.Text = total.ToString() == "0.00" ? "" : total.ToString();

                ViewState["Totalscore"] = gross.Text;
                int a = GV_viewsalaryindetail.Rows.Count;

                

            }
            catch (Exception ex)
            {
              
            }
        }


        public void bindnetsalarytot()
        {
            try
            {

                decimal ntotal = 0;
                for (int i = 0; i < GV_viewsaldedtn.Rows.Count; i++)
                {

                    Label deductns = (Label)GV_viewsaldedtn.Rows[i].FindControl("lbl_deductions");
                    decimal ncalc = Convert.ToDecimal(deductns.Text == "" ? "0.00" : deductns.Text);
                    ntotal = ntotal + ncalc;

                }
                Label net = ((Label)GV_viewsaldedtn.FooterRow.FindControl("lbl_nettotal"));

                decimal final = Convert.ToDecimal(ViewState["Totalscore"].ToString().Trim()) - ntotal;
                net.Text = final.ToString() == "0.00" ? "" : final.ToString();

                ViewState["nettotal"] = net.Text;
                int a = GV_viewsaldedtn.Rows.Count;



            }
            catch (Exception ex)
            {
               
            }
        }

        
    }
}
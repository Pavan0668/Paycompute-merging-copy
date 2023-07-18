using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBO.EmpData.EmpCollBo;
using System.IO;

namespace iEmpPower.UI.Configuration
{
    public partial class UpdateEmpInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = string.Empty;
                    if (Request.QueryString["id"] != null)
                    {
                        id = Request.QueryString["id"];
                    }

                    if (id == "CCempinf")
                    {
                        //HideTabs();
                        view2.Visible = true;
                        //Tab2.CssClass = "nav-link active p-2";
                        load_depts();
                        GV_viewempmainsrch();
                        load_grid("", 1);
                        srch.Visible = false;
                    }
                    else
                    {
                        //HideTabs();
                        view1.Visible = false;
                        view2.Visible = true;
                        //Tab1.CssClass = "nav-link active p-2";
                        load_depts();
                        GV_viewemp();
                        load_grid("", 1);
                        srch.Visible = true;
                        Load_Employees();
                    }
            }
        }

        protected void load_grid(string empid, int flg)
        {
            try
            {

                //MsgCls(string.Empty, Label2, System.Drawing.Color.Transparent);
                EmpDataBL ObjLeaveRequestBl = new EmpDataBL();
                List<EmoDataBo> LeaveReqboList1 = new List<EmoDataBo>();

                LeaveReqboList1 = ObjLeaveRequestBl.Get_empInfo(Session["CompCode"].ToString(), empid, flg);
                Session.Add("IexpGrdInfo", LeaveReqboList1);

                if (LeaveReqboList1 == null || LeaveReqboList1.Count == 0)
                {
                    //MsgCls("No Records Found !", Label2, System.Drawing.Color.Red);
                    gv_dept.Visible = false;
                    gv_dept.DataSource = null;
                    gv_dept.DataBind();
                    return;
                }
                else
                {
                    //MsgCls("", Label2, System.Drawing.Color.Transparent);
                    gv_dept.Visible = true;
                    gv_dept.DataSource = null;
                    gv_dept.DataBind();
                    gv_dept.DataSource = LeaveReqboList1;
                    gv_dept.SelectedIndex = -1;
                    gv_dept.DataBind();
                }


            }
            catch (Exception ex) { }
        }

        protected void load_depts()
        {
            configurationcollectionbo objLst5 = configurationbl.Get_Departments(Session["CompCode"].ToString().ToLower().Trim(),1);
            ddleDept.DataSource = objLst5;
            ddleDept.DataTextField = "deptdesc";
            ddleDept.DataValueField = "ID";
            ddleDept.DataBind();
            ddleDept.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
        }

        protected void gv_dept_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = gv_dept.Rows[rowIndex];
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
                        //Employee_ID,FullName,EDEPT,EGRADE,,,
                        //grdRow.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                        ViewState["EMPID"] = gv_dept.DataKeys[grdRow.RowIndex].Values["Employee_ID"].ToString();
                        lblEmpID.Text = "Employee ID : " + gv_dept.DataKeys[grdRow.RowIndex].Values["Employee_ID"].ToString();
                        ddleDept.SelectedValue = gv_dept.DataKeys[grdRow.RowIndex].Values["depitid"].ToString() == "0" ? "1" : gv_dept.DataKeys[grdRow.RowIndex].Values["depitid"].ToString();
                        txteBranch.Text = gv_dept.DataKeys[grdRow.RowIndex].Values["EBRANCH"].ToString();
                        txtedivision.Text = gv_dept.DataKeys[grdRow.RowIndex].Values["EDIVISION"].ToString();
                        txteDOJ.Text = Convert.ToDateTime(gv_dept.DataKeys[grdRow.RowIndex].Values["EDOJ"].ToString()).ToString("yyyy-MM-dd");
                        txteGrade.Text = gv_dept.DataKeys[grdRow.RowIndex].Values["EGRADE"].ToString();
                        pnlCntrls.Visible = true;
                        btnUpdate.Focus();
                        break;
                }
            }   
            catch (Exception ex) { }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EmpDataBL bl = new EmpDataBL();
                EmoDataBo bo = new EmoDataBo();
                bo.compCode = Session["CompCode"].ToString();
                bo.Employee_ID = ViewState["EMPID"].ToString();
                bo.depitid = Convert.ToDecimal(ddleDept.SelectedValue);
                bo.EBRANCH = txteBranch.Text;
                bo.EGRADE = txteGrade.Text;
                bo.EDIVISION = txtedivision.Text;
                bo.dOJ = Convert.ToDateTime(txteDOJ.Text);
                bo.flg = 1;
                bool? st = false;
                bl.update_empInfo(bo, ref st);
                if (st == true)
                {
                    load_grid("", 1);
                    pnlCntrls.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error While Updating!!')", true);
                }
            }
            catch (Exception) { }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                load_grid("", 1);
                txtEmpID.Text = "";
                txteGrade.Text = "";
                txteDOJ.Text = "";
                txtedivision.Text = "";
                txteBranch.Text = "";
                ddleDept.SelectedIndex = -1;
                pnlCntrls.Visible = false;
            }
            catch (Exception) { }
        }

        protected void gv_dept_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            gv_dept.PageIndex = e.NewPageIndex;
            EmpCollBo LeaveReqboList1 = (EmpCollBo)Session["IexpGrdInfo"];
            gv_dept.DataSource = LeaveReqboList1;
            gv_dept.SelectedIndex = -1;
            gv_dept.DataBind();

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                load_grid(txtEmpID.Text.Trim(), 2);
            }
            catch (Exception ex) { }
        }


        protected void gv_dept_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)e.Row.FindControl("lblID");
                    string ccode = Session["CompCode"].ToString();
                    string emplogin = lbl.Text; //e.Row.Cells[7].Text;//
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    e.Row.Cells[0].Text = emplogin.Trim().ToUpper();
                }
            }
            catch (Exception ex)
            { }
        }

        public void GV_viewemp()
        {
            try
            {
                EmpCollBo objspaylst = new EmpCollBo();
                EmpDataBL bl = new EmpDataBL();
                objspaylst = bl.viewall_emp(Session["CompCode"].ToString(), "", 1);
                GV_viewemp_details.DataSource = objspaylst;
                GV_viewemp_details.DataBind();
                srch.Visible = true;

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_viewemp_details.Rows)
                {
                    for (int i = 0; i < GV_viewemp_details.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_viewemp_details.Rows[i].FindControl("lblRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == GV_viewemp_details.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                divcnt.Visible = GV_viewemp_details.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {

            }
        }

        public void GV_viewempmainsrch()
        {
            try
            {
                EmpCollBo objspaylst = new EmpCollBo();
                EmpDataBL bl = new EmpDataBL();
                objspaylst = bl.viewall_emp(Session["CompCode"].ToString(), Session["_MainSearchValue"].ToString().ToLower().Trim(), 2);
                GV_viewemp_details.DataSource = objspaylst;
                GV_viewemp_details.DataBind();
            }
            catch (Exception ex)
            {

            }
        }


        protected void GV_viewemp_details_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)e.Row.FindControl("lbl_viewempid");
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

        protected void GV_viewemp_details_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEWEMP":
                        PNL_allemp_details.Visible = true;
                        
                        int rowIndex = Convert.ToInt32(e.CommandArgument);
                        GridViewRow grdRow = GV_viewemp_details.Rows[rowIndex];
                        ViewState["empidview"]= GV_viewemp_details.DataKeys[grdRow.RowIndex].Values["Employee_ID"].ToString();
                        foreach (GridViewRow row in GV_viewemp_details.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                                                System.Drawing.ColorTranslator.FromHtml("#ffe6ba") :
                                               System.Drawing.Color.White;
                        }


                    string empid = (GV_viewemp_details.DataKeys[int.Parse(e.CommandArgument.ToString())]["Employee_ID"].ToString().Trim());
                    string ccode = Session["CompCode"].ToString();
                    string emplogin = empid.ToString(); 
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    lbl_empid.Text = emplogin.Trim().ToUpper();
                   
                    string ename = (GV_viewemp_details.DataKeys[int.Parse(e.CommandArgument.ToString())]["EDIVISION"].ToString().Trim());
                    ViewState["empname"] = ename;
                    lblename.Text = ViewState["empname"].ToString();

                    
                    

                        GridViewRow rows = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label exidate = (Label)rows.FindControl("lbl_exitdt");
                        Session["exitdate"] = exidate.Text.ToString().Trim();
                        Label joindate = (Label)rows.FindControl("lbljod");
                        string empjod = joindate.Text.ToString();
                        ViewState["exitdate"] = empjod;
                        lbl_JOD.Text = ViewState["exitdate"].ToString();


                        Label exitdt = (Label)rows.FindControl("lbl_exitdt");
                        string exitdate = exitdt.Text.ToString();
                        ViewState["exitdate"] = exitdate;
                        lbl_exitdate.Text = ViewState["exitdate"].ToString();





                       btn_exprt_empinfo.Visible = true;
                       

                        //------- Personal info----------

                        divPI.Visible = true;
                        GV_empPI.DataSource = null;
                        GV_empPI.DataBind();
                        configurationcollectionbo lstPI = new configurationcollectionbo();
                        configurationbl blPI = new configurationbl();
                        lstPI = blPI.Get_empinfo_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                        GV_empPI.DataSource = lstPI;
                        GV_empPI.DataBind();
                        PIexitedit();

                        //--------------- Dept Info --------------------
                        divDI.Visible = true;
                        GV_empdept.DataSource = null;
                        GV_empdept.DataBind();
                        configurationcollectionbo lstDI = new configurationcollectionbo();
                        configurationbl blDI = new configurationbl();
                        lstDI = blDI.Get_empinfo_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                        GV_empdept.DataSource = lstDI;
                        GV_empdept.DataBind();
                        deptexitedit();

                        //--------------- Desig Info --------------------
                        divdesig.Visible = true;
                        GV_empdesightry.DataSource = null;
                        GV_empdesightry.DataBind();
                        configurationbl ObjLeaveRequestBl = new configurationbl();
                        List<configurationbo> LeaveReqboList1 = new List<configurationbo>();
                        LeaveReqboList1 = ObjLeaveRequestBl.Get_empmgrdesig_config_full(Session["CompCode"].ToString().ToLower().Trim().ToLower().Trim(), ViewState["empidview"].ToString(), 1);
                        GV_empdesightry.DataBind();
                        GV_empdesightry.DataSource = LeaveReqboList1;
                        GV_empdesightry.DataBind();

                        //--------------- Mgr Info --------------------
                        dvmgrlst.Visible = true;
                        GV_empmgr.DataSource = null;
                        GV_empmgr.DataBind();
                         configurationbl bl = new configurationbl();
                        List<configurationbo> lst = new List<configurationbo>();
                        lst = bl.Get_mgrdesig_config_full(Session["CompCode"].ToString().ToLower().Trim().ToLower().Trim(), ViewState["empidview"].ToString(), 1);
                        GV_empmgr.DataBind();
                        GV_empmgr.DataSource = lst;
                        GV_empmgr.DataBind();

                        //------- Address ifo----------
                        divAI.Visible = true;
                        GV_empAI.DataSource = null;
                        GV_empAI.DataBind();
                        configurationcollectionbo objspaylstAI = new configurationcollectionbo();
                        configurationbl blAI = new configurationbl();
                        objspaylstAI = blAI.Get_empaddress_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                        GV_empAI.DataSource = objspaylstAI;
                        GV_empAI.DataBind();
                        exitempadd();


                        //------- Contact ifo----------

                        divCI.Visible = true;
                        GV_empCI.DataSource = null;
                        GV_empCI.DataBind();
                        configurationcollectionbo objspaylstCI = new configurationcollectionbo();
                        configurationbl blCI = new configurationbl();
                        objspaylstCI = blCI.Get_empcommunication_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                        GV_empCI.DataSource = objspaylstCI;
                        GV_empCI.DataBind();
                        CIexitedit();


                        //------- Document ifo----------

                        divDCI.Visible = true;
                        GV_docinfo.DataSource = null;
                        GV_docinfo.DataBind();
                        configurationcollectionbo objspaylstDCI = new configurationcollectionbo();
                        configurationbl blDCI = new configurationbl();
                        objspaylstDCI = blDCI.Get_empdocument_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                        GV_docinfo.DataSource = objspaylstDCI;
                        GV_docinfo.DataBind();
                        exitdocmod();


                        //------- Bank ifo----------
                        divBI.Visible = true;
                        GV_empBI.DataSource = null;
                        GV_empBI.DataBind();
                        configurationcollectionbo objspaylstBI = new configurationcollectionbo();
                        configurationbl blBI = new configurationbl();
                        objspaylstBI = blBI.Get_empbank_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                        GV_empBI.DataSource = objspaylstBI;
                        GV_empBI.DataBind();
                        exitbankmode();

                        //------- Benefits ifo----------
                        divBNF.Visible = true;
                        GV_empBENI.DataSource = null;
                        GV_empBENI.DataBind();
                        configurationcollectionbo objspaylstBenI = new configurationcollectionbo();
                        configurationbl blBenI = new configurationbl();
                        objspaylstBenI = blBenI.Get_empbeneinfo_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                        GV_empBENI.DataSource = objspaylstBenI;
                        GV_empBENI.DataBind();
                        exibenemode();

                        btn_exprt_empinfo.Focus();
                        
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }


        
       
        protected void GV_viewemp_details_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                EmpCollBo objspaylst = new EmpCollBo();
                EmpDataBL bl = new EmpDataBL();
                objspaylst = bl.viewall_emp(Session["CompCode"].ToString(), "", 1);
                GV_viewemp_details.DataSource = objspaylst;
                GV_viewemp_details.PageIndex = e.NewPageIndex;
                GV_viewemp_details.DataBind();
                PNL_allemp_details.Visible = false;
                btn_exprt_empinfo.Visible = false;

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_viewemp_details.Rows)
                {
                    for (int i = 0; i < GV_viewemp_details.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_viewemp_details.Rows[i].FindControl("lblRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == GV_viewemp_details.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                divcnt.Visible = GV_viewemp_details.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {

            }
        }

       


        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }

        protected void btn_exprt_empinfo_Click(object sender, EventArgs e)
        {
            try
            {
                pnlexprt.Visible = true;                
                string date1 = DateTime.Now.ToString("MM/yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + " Employee_Info" + date1 + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                emprowtoexprt.Visible = true;               
                exportgvs();                
                using (StringWriter sw = new StringWriter())
                {                 

                    HtmlTextWriter hw = new HtmlTextWriter(sw);                    
                    pnlexprt.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    emprowtoexprt.Visible = false;
                    pnlexprt.Visible = false;
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {

            }
        }

        //protected void Tab1_Click(object sender, EventArgs e)
        //{
        //    HideTabs();
        //    view1.Visible = true;
        //    view2.Visible = false;
        //    Tab1.CssClass = "nav-link active p-2";
        //}

        //protected void Tab2_Click(object sender, EventArgs e)
        //{
        //    HideTabs();
        //    view1.Visible = false;
        //    view2.Visible = true;
        //    Tab2.CssClass = "nav-link active p-2";
        //}

        //protected void HideTabs()
        //{
        //    view1.Visible = false;
        //    view2.Visible = false;           

        //    Tab1.CssClass = "nav-link  p-2";
        //    Tab2.CssClass = "nav-link  p-2";

        //}

        protected void GV_empPI_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

                    case "EDITPI":
                        PIexitedit();
                        int rowpiIndex = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvpiRow = GV_empPI.Rows[rowpiIndex];
                        GridViewRow edtrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);                        

                        Label LBLPIedtdob = (Label)edtrow.FindControl("lbl_pidob");

                        Label LBLPIedtfname = (Label)edtrow.FindControl("lbl_pifname");

                        Label LBLPIedtmname = (Label)edtrow.FindControl("lbl_pimname");

                        Label LBLPIedtmsts = (Label)edtrow.FindControl("lbl_pimsts");

                        Label LBLPIedtspname = (Label)edtrow.FindControl("lbl_pispname");

                        TextBox TXTedtpidob = (TextBox)edtrow.FindControl("txt_pidob");

                        TextBox TXTedtpifnme = (TextBox)edtrow.FindControl("txt_pifname");

                        TextBox TXTedtpimname = (TextBox)edtrow.FindControl("txt_pipimname");

                        DropDownList DDLedtpimsts = (DropDownList)edtrow.FindControl("DDL_pists");

                        TextBox TXTedtpispname = (TextBox)edtrow.FindControl("txt_pispname");

                        LinkButton piedt1 = (LinkButton)edtrow.FindControl("LK_PIedit");

                        LinkButton piudt2 = (LinkButton)edtrow.FindControl("LK_piupdte");

                        LinkButton picnl3 = (LinkButton)edtrow.FindControl("LK_picancel");

                        if (LBLPIedtmsts.Text != "")
                        {

                            string maritalsts = "";

                            maritalsts = GV_empPI.DataKeys[gvpiRow.RowIndex].Values["Created_By"].ToString();

                            DDLedtpimsts.SelectedValue = maritalsts.Trim();
                        }
                            LBLPIedtdob.Visible = false;
                            LBLPIedtfname.Visible = false;
                            LBLPIedtmname.Visible = false;
                            LBLPIedtmsts.Visible = false;
                            LBLPIedtspname.Visible = false;

                            TXTedtpidob.Visible = true;
                            TXTedtpifnme.Visible = true;
                            TXTedtpimname.Visible = true;
                            DDLedtpimsts.Visible = true;
                            TXTedtpispname.Visible = true;

                            piedt1.Visible = false;
                            piudt2.Visible = true;
                            picnl3.Visible = true;

                        break;

                    case "UPDATEPI":

                         int updatepiindex = Convert.ToInt32(e.CommandArgument);

                         GridViewRow gvupdate = GV_empPI.Rows[updatepiindex];

                         TextBox TXTpidobupd = (TextBox)gvupdate.FindControl("txt_pidob");

                         TextBox TXTpifnmeupd = (TextBox)gvupdate.FindControl("txt_pifname");

                         TextBox TXTpimnameupd = (TextBox)gvupdate.FindControl("txt_pipimname");

                         DropDownList DDLpimstsupd = (DropDownList)gvupdate.FindControl("DDL_pists");

                         TextBox TXTpispnameupd = (TextBox)gvupdate.FindControl("txt_pispname");

                         int ID = Convert.ToInt32(GV_empPI.DataKeys[gvupdate.RowIndex].Values["ID"].ToString());

                         string eid = GV_empPI.DataKeys[gvupdate.RowIndex].Values["EMPID"].ToString();


                         if (TXTpidobupd.Text != "")
                         {

                             bool? status = false;
                             string hrmail = "";
                             string empmail = "";
                             configurationbo objempBo = new configurationbo();
                             configurationbl blBenI = new configurationbl();
                             objempBo.EMPID = eid.ToString().ToLower().Trim();
                             objempBo.ID = ID;
                             objempBo.NAME = TXTpifnmeupd.Text.ToString().Trim();
                             objempBo.PASSWORD = TXTpimnameupd.Text.ToString();
                             objempBo.MODIFIEDBY = TXTpispnameupd.Text.ToString().Trim();
                             objempBo.PORT = DDLpimstsupd.SelectedValue.ToString().Trim();
                             objempBo.leavTEXT = "";
                             objempBo.IMAP_SERVER = "";
                             objempBo.H_type = "";
                             objempBo.HR_DESCRIPTION = "";
                             objempBo.Date = Convert.ToDateTime(TXTpidobupd.Text.ToString());
                             objempBo.Created_On = DateTime.Now;
                             objempBo.flag = 1;
                             objempBo.Company_Code = Session["CompCode"].ToString().ToLower().Trim();
                             blBenI.Update_Empinfo(objempBo, ref status, ref hrmail, ref empmail);

                             if (status == true)
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Employee Info updated successfully');", true);
                             }
                             else
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to update, please check the input data');", true);
                             }
                             bindpidata();
                             PIexitedit();
                         }

                         else
                         {
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter valid DOB');", true);
                         }

                         
                        break;

                    case "CANCELPI":
                         int picanceleindex = Convert.ToInt32(e.CommandArgument);
                         GridViewRow gvcancel = GV_empPI.Rows[picanceleindex];
                         bindpidata(); 
                        PIexitedit();
                        break;
                }
            }
            catch (Exception ex) { }
        }

        public void bindpidata()
        {
            configurationcollectionbo lstPI = new configurationcollectionbo();
            configurationbl blPI = new configurationbl();
            lstPI = blPI.Get_empinfo_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
            GV_empPI.DataSource = lstPI;
            GV_empPI.DataBind();
        }



        public void PIexitedit()
        {
            try
            {
                foreach (GridViewRow row in GV_empPI.Rows)
                {

                    Label LBLPIdob = (Label)row.FindControl("lbl_pidob");

                    Label LBLPIfname = (Label)row.FindControl("lbl_pifname");

                    Label LBLPImname = (Label)row.FindControl("lbl_pimname");

                    Label LBLPImsts = (Label)row.FindControl("lbl_pimsts");

                    Label LBLPIspname = (Label)row.FindControl("lbl_pispname");

                    TextBox TXTpidob = (TextBox)row.FindControl("txt_pidob");

                    TextBox TXTpifnme = (TextBox)row.FindControl("txt_pifname");

                    TextBox TXTpimname = (TextBox)row.FindControl("txt_pipimname");

                    DropDownList DDLpimsts = (DropDownList)row.FindControl("DDL_pists");

                    TextBox TXTpispname = (TextBox)row.FindControl("txt_pispname");

                    LinkButton piedt = (LinkButton)row.FindControl("LK_PIedit");

                    LinkButton piudt = (LinkButton)row.FindControl("LK_piupdte");

                    LinkButton picnl = (LinkButton)row.FindControl("LK_picancel");

                    
                        LBLPIdob.Visible = true;
                        LBLPIfname.Visible = true;
                        LBLPImname.Visible = true;
                        LBLPImsts.Visible = true;
                        LBLPIspname.Visible = true;

                        TXTpidob.Visible = false;
                        TXTpifnme.Visible = false;
                        TXTpimname.Visible = false;
                        DDLpimsts.Visible = false;
                        TXTpispname.Visible = false;
                        if (Session["exitdate"].ToString() == "")
                        {
                        piedt.Visible = true;
                        piudt.Visible = false;
                        picnl.Visible = false;
                        }
                   
                        else
                        {
                        piedt.Visible = false;
                        piudt.Visible = false;
                        picnl.Visible = false;
                        }

                    


                }
            }

            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }

        }

        protected void GV_empPI_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlmstus = (e.Row.FindControl("DDL_pists") as DropDownList);
                    configurationcollectionbo objLst = configurationbl.Get_MaterialStatus(1);
                    ddlmstus.DataSource = objLst;
                    ddlmstus.DataTextField = "DDLTYPETEXT";
                    ddlmstus.DataValueField = "DDLTYPE";
                    ddlmstus.DataBind();                   

                    string ccode1 = Session["CompCode"].ToString();
                    string emplogin1 = e.Row.Cells[0].Text;
                    int cnt1 = ccode1.Length;
                    emplogin1 = emplogin1.Substring(cnt1);
                    e.Row.Cells[0].Text = emplogin1.Trim().ToUpper();
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void deptexitedit()
        {
            try
            {
                foreach (GridViewRow rowdpt in GV_empdept.Rows)
                {

                    Label LBLdeptdept = (Label)rowdpt.FindControl("lbl_deptpi");

                    Label LBLdeptgrd = (Label)rowdpt.FindControl("lbl_grdepi");

                    Label LBLdeptbrch = (Label)rowdpt.FindControl("lbl_brnchpi");

                    Label LBLdepdivsn = (Label)rowdpt.FindControl("lbl_dicsnpi");

                    Label LBLdeptdoj = (Label)rowdpt.FindControl("lbl_dojpi");

                    DropDownList DDLdeptdept = (DropDownList)rowdpt.FindControl("DDL_deptpi");

                    TextBox TXTdeptgrd = (TextBox)rowdpt.FindControl("txt_grdepi");

                    TextBox TXTdeptbrch = (TextBox)rowdpt.FindControl("txt_bechpi");

                    TextBox DDLdeptdvsn = (TextBox)rowdpt.FindControl("txt_dvsnpi");

                    TextBox TXTdeptdoj = (TextBox)rowdpt.FindControl("txt_dojpi");

                    LinkButton deptedt = (LinkButton)rowdpt.FindControl("LK_deptedt");

                    LinkButton deptudt = (LinkButton)rowdpt.FindControl("LK_deptupt");

                    LinkButton deptcnl = (LinkButton)rowdpt.FindControl("LK_deptcncl");


                    LBLdeptdept.Visible = true;
                    LBLdeptgrd.Visible = true;
                    LBLdeptbrch.Visible = true;
                    LBLdepdivsn.Visible = true;
                    LBLdeptdoj.Visible = true;

                    DDLdeptdept.Visible = false;
                    TXTdeptgrd.Visible = false;
                    TXTdeptbrch.Visible = false;
                    DDLdeptdvsn.Visible = false;
                    TXTdeptdoj.Visible = false;

                    if (Session["exitdate"].ToString() == "")
                    {
                        deptedt.Visible = true;
                        deptudt.Visible = false;
                        deptcnl.Visible = false;
                    }

                    else
                    {
                        deptedt.Visible = false;
                        deptudt.Visible = false;
                        deptcnl.Visible = false;
                    }

                }
            }

            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }

        }

        protected void GV_empdept_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddldeptpi = (e.Row.FindControl("DDL_deptpi") as DropDownList);
                    configurationcollectionbo objLst = configurationbl.Get_Departments(Session["CompCode"].ToString().ToLower().Trim(), 1);
                    ddldeptpi.DataSource = objLst;
                    ddldeptpi.DataTextField = "deptdesc";
                    ddldeptpi.DataValueField = "ID";
                    ddldeptpi.DataBind();               

                    
                    string ccode1 = Session["CompCode"].ToString();
                    string emplogin1 = e.Row.Cells[0].Text;
                    int cnt1 = ccode1.Length;
                    emplogin1 = emplogin1.Substring(cnt1);
                    e.Row.Cells[0].Text = emplogin1.Trim().ToUpper();
                }
            }

            catch (Exception ex)
            {

            }
        }

        protected void GV_empdept_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

                    case "EDITDPT":
                        deptexitedit();

                        int rowdeptedt = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvdptRow = GV_empdept.Rows[rowdeptedt];
                        GridViewRow edtdeptrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        Label LBLdeptedtdept = (Label)edtdeptrow.FindControl("lbl_deptpi");

                        Label LBLdeptedtgrd = (Label)edtdeptrow.FindControl("lbl_grdepi");

                        Label LBLdeptedtbrch = (Label)edtdeptrow.FindControl("lbl_brnchpi");

                        Label LBLdepedtdivsn = (Label)edtdeptrow.FindControl("lbl_dicsnpi");

                        Label LBLdeptedtdoj = (Label)edtdeptrow.FindControl("lbl_dojpi");

                        DropDownList DDLdeptedtdept = (DropDownList)edtdeptrow.FindControl("DDL_deptpi");

                        TextBox TXTdeptedtgrd = (TextBox)edtdeptrow.FindControl("txt_grdepi");

                        TextBox TXTedtbrch = (TextBox)edtdeptrow.FindControl("txt_bechpi");

                        TextBox TXTedtdvn = (TextBox)edtdeptrow.FindControl("txt_dvsnpi");

                        TextBox TXTedtdoj = (TextBox)edtdeptrow.FindControl("txt_dojpi");

                        LinkButton edtdpt1 = (LinkButton)edtdeptrow.FindControl("LK_deptedt");

                        LinkButton edtudt2 = (LinkButton)edtdeptrow.FindControl("LK_deptupt");

                        LinkButton edtcncl3 = (LinkButton)edtdeptrow.FindControl("LK_deptcncl");

                        if (LBLdeptedtdept.Text != "")
                        {
                            string dept = "";

                            dept = GV_empdept.DataKeys[gvdptRow.RowIndex].Values["desig"].ToString();

                            DDLdeptedtdept.SelectedValue = dept.Trim();
                        }

                        LBLdeptedtdept.Visible = false;
                        LBLdeptedtgrd.Visible = false;
                        LBLdeptedtbrch.Visible = false;
                        LBLdepedtdivsn.Visible = false;
                        LBLdeptedtdoj.Visible = false;

                        DDLdeptedtdept.Visible = true;
                        TXTdeptedtgrd.Visible = true;
                        TXTedtbrch.Visible = true;
                        TXTedtdvn.Visible = true;
                        TXTedtdoj.Visible = true;

                        edtdpt1.Visible = false;
                        edtudt2.Visible = true;
                        edtcncl3.Visible = true;
                        break;

                    case "UPDDEPT":

                        int updateindexdept = Convert.ToInt32(e.CommandArgument);

                        GridViewRow gvupdatedept = GV_empdept.Rows[updateindexdept];

                        DropDownList DDLdeptedtupd = (DropDownList)gvupdatedept.FindControl("DDL_deptpi");

                        TextBox TXTdeptupdgrd = (TextBox)gvupdatedept.FindControl("txt_grdepi");

                        TextBox TXTbrchupd = (TextBox)gvupdatedept.FindControl("txt_bechpi");

                        TextBox TXTdvnupd = (TextBox)gvupdatedept.FindControl("txt_dvsnpi");

                        TextBox TXTdojupd = (TextBox)gvupdatedept.FindControl("txt_dojpi");

                        int IDdept = Convert.ToInt32(GV_empdept.DataKeys[gvupdatedept.RowIndex].Values["ID"].ToString());

                        string eiddept = GV_empdept.DataKeys[gvupdatedept.RowIndex].Values["EMPID"].ToString();


                        if (TXTdojupd.Text != "")
                         {

                             bool? statusdpt = false;
                             string hrmaildpt = "";
                             string empmaildpt = "";
                             configurationbo objempBo = new configurationbo();
                             configurationbl blBenI = new configurationbl();
                             objempBo.EMPID = eiddept.ToString().ToLower().Trim();
                             objempBo.ID = IDdept;
                             objempBo.NAME = TXTdvnupd.Text.ToString().Trim();
                             objempBo.PASSWORD = TXTbrchupd.Text.ToString();
                             objempBo.MODIFIEDBY = "";
                             objempBo.PORT = "";
                             objempBo.leavTEXT = "";
                             objempBo.IMAP_SERVER = TXTdeptupdgrd.Text.ToString().Trim();
                             objempBo.H_type = DDLdeptedtupd.SelectedValue.ToString();
                             objempBo.HR_DESCRIPTION = "";
                             objempBo.Date = Convert.ToDateTime(TXTdojupd.Text.ToString());
                             objempBo.Created_On = DateTime.Now;
                             objempBo.flag = 2;
                             objempBo.Company_Code = Session["CompCode"].ToString().ToLower().Trim();
                             blBenI.Update_Empinfo(objempBo, ref statusdpt, ref hrmaildpt, ref empmaildpt);

                             if (statusdpt == true)
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Employee department info updated successfully');", true);
                             }
                             else
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to update, please check the input data');", true);
                             }
                             loaddeptgv();
                         }

                         else
                         {
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter valid DOJ');", true);
                         }
                        break;

                    case "CNCLDPT":
                         int dptceleindex = Convert.ToInt32(e.CommandArgument);
                         GridViewRow gvdept = GV_empdept.Rows[dptceleindex];
                         loaddeptgv();
                        break;
                }
            }

            catch (Exception ex)
            {

            }
        }

        public void loaddeptgv()
        {
            configurationcollectionbo lstDI = new configurationcollectionbo();
            configurationbl blDI = new configurationbl();
            lstDI = blDI.Get_empinfo_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
            GV_empdept.DataSource = lstDI;
            GV_empdept.DataBind();
            deptexitedit();
        }


        public void CIexitedit()
        {
            try
            {
                foreach (GridViewRow rowdci in GV_empCI.Rows)
                {

                    Label LBLCItyp = (Label)rowdci.FindControl("lbl_citype");

                    TextBox txtcityp = (TextBox)rowdci.FindControl("txt_cityp");

                    LinkButton CIextedt = (LinkButton)rowdci.FindControl("LK_CIedt");

                    LinkButton CIextupdt = (LinkButton)rowdci.FindControl("LK_ciupdate");

                    LinkButton CIextcncl = (LinkButton)rowdci.FindControl("LK_cicncl");

                    LBLCItyp.Visible = true;
                    txtcityp.Visible = false;

                    if (Session["exitdate"].ToString() == "")
                    {
                        CIextedt.Visible = true;
                        CIextupdt.Visible = false;
                        CIextcncl.Visible = false;
                    }

                    else
                    {
                        CIextedt.Visible = false;
                        CIextupdt.Visible = false;
                        CIextcncl.Visible = false;
                    }
                }
            }
            catch (Exception ex) { }
        }
        
        protected void GV_empCI_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                        
                    case "CIEDIT":
                        CIexitedit();

                         int rowedtci = Convert.ToInt32(e.CommandArgument);
                         GridViewRow gvciRow = GV_empCI.Rows[rowedtci];
                        GridViewRow edtcirow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        Label LBLCIedttyp = (Label)edtcirow.FindControl("lbl_citype");

                        TextBox txtciedttyp = (TextBox)edtcirow.FindControl("txt_cityp");

                        LinkButton CIedt1 = (LinkButton)edtcirow.FindControl("LK_CIedt");

                        LinkButton CIedtupdt = (LinkButton)edtcirow.FindControl("LK_ciupdate");

                        LinkButton CIedtcncl = (LinkButton)edtcirow.FindControl("LK_cicncl");

                        LBLCIedttyp.Visible = false;

                        txtciedttyp.Visible = true;

                        CIedt1.Visible = false;
                        CIedtupdt.Visible = true;
                        CIedtcncl.Visible = true;

                        break;
                    case "CIUPDATE" :
                         int CIupdateindex = Convert.ToInt32(e.CommandArgument);

                         GridViewRow gvCIupdate = GV_empCI.Rows[CIupdateindex];

                         TextBox txtcitypupd = (TextBox)gvCIupdate.FindControl("txt_cityp");

                         int IDci = Convert.ToInt32(GV_empCI.DataKeys[gvCIupdate.RowIndex].Values["ID"].ToString());

                         string eidci = GV_empCI.DataKeys[gvCIupdate.RowIndex].Values["EMPID"].ToString();

                         string contid = GV_empCI.DataKeys[gvCIupdate.RowIndex].Values["desig"].ToString();
                        


                         if (txtcitypupd.Text != "")
                         {

                             bool? statusci = false;
                             string hrmailci = "";
                             string empmailci = "";
                             configurationbo objempBo = new configurationbo();
                             configurationbl blBenI = new configurationbl();
                             objempBo.EMPID = eidci.ToString().ToLower().Trim();
                             objempBo.ID = IDci;
                             objempBo.NAME = txtcitypupd.Text.ToString().Trim();
                             objempBo.PASSWORD = contid.ToString().Trim();
                             objempBo.MODIFIEDBY = "";
                             objempBo.PORT = "";
                             objempBo.leavTEXT = "";
                             objempBo.IMAP_SERVER = "";
                             objempBo.H_type = "";
                             objempBo.HR_DESCRIPTION = "";
                             objempBo.Date = DateTime.Now;
                             objempBo.Created_On = DateTime.Now;
                             objempBo.flag = 4;
                             objempBo.Company_Code = Session["CompCode"].ToString().ToLower().Trim();
                             blBenI.Update_Empinfo(objempBo, ref statusci, ref hrmailci, ref empmailci);

                             if (statusci == true)
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Employee Contact Info updated successfully');", true);
                             }
                             else
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to update, please check the input data');", true);
                             }
                             loadcommgv();
                         }

                         else
                         {
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter valid Communication description');", true);
                         }
                        break;
                        case "CICANCL" :
                         int ciedtcncl = Convert.ToInt32(e.CommandArgument);
                         GridViewRow gvci = GV_empCI.Rows[ciedtcncl];
                         loadcommgv();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        
        }

        public void loadcommgv()
        {
            configurationcollectionbo objspaylstCI = new configurationcollectionbo();
            configurationbl blCI = new configurationbl();
            objspaylstCI = blCI.Get_empcommunication_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
            GV_empCI.DataSource = objspaylstCI;
            GV_empCI.DataBind();
            CIexitedit();
        }

        protected void GV_empCI_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string ccode1 = Session["CompCode"].ToString();
                    string emplogin1 = e.Row.Cells[0].Text;
                    int cnt1 = ccode1.Length;
                    emplogin1 = emplogin1.Substring(cnt1);
                    e.Row.Cells[0].Text = emplogin1.Trim().ToUpper();
                }
            }

            catch (Exception ex)
            {

            }
        }




        public void exitempadd()
        {
            try
            {
                foreach (GridViewRow rowadd in GV_empAI.Rows)
                {

                    Label LBLextaddln1 = (Label)rowadd.FindControl("lbl_addline1");

                    Label LBLextaddln2 = (Label)rowadd.FindControl("lbl_addln2");

                    Label LBLextadloc = (Label)rowadd.FindControl("lbl_locality");

                    Label LBLextadddscr = (Label)rowadd.FindControl("lbl_dstrct");

                    Label LBLextaddcnty = (Label)rowadd.FindControl("lbl_addcntry");

                    Label LBLextaddstate = (Label)rowadd.FindControl("lbl_addste");

                    Label LBLextaddpin = (Label)rowadd.FindControl("lbl_addpncode");

                    Label LBLextaddfdt = (Label)rowadd.FindControl("lbl_addfrmdt");

                    Label LBLextaddenda = (Label)rowadd.FindControl("lbl_addtodate");



                    TextBox txtextaddln1 = (TextBox)rowadd.FindControl("txt_addln1");

                    TextBox txtextaddln2 = (TextBox)rowadd.FindControl("txt_addln2");

                    TextBox txtextaddloc = (TextBox)rowadd.FindControl("txt_locality");

                    TextBox txtextadddrt = (TextBox)rowadd.FindControl("txt_dstrct");

                    DropDownList DDLextaddcntry = (DropDownList)rowadd.FindControl("DDL_addcntry");

                    DropDownList DDLextaddstate = (DropDownList)rowadd.FindControl("DDL_addstate");

                    TextBox txtextaddpin = (TextBox)rowadd.FindControl("txt_addpncode");

                    TextBox txtextaddfdt = (TextBox)rowadd.FindControl("txt_addfrmdate");

                    TextBox txtextaddendate = (TextBox)rowadd.FindControl("txt_addtodate");

                    LinkButton addexttedt = (LinkButton)rowadd.FindControl("LK_addedt");

                    LinkButton addextupdt = (LinkButton)rowadd.FindControl("LK_addupdt");

                    LinkButton addedtcncl = (LinkButton)rowadd.FindControl("LK_addcncl");


                    LBLextaddln1.Visible = true;
                    LBLextaddln2.Visible = true;
                    LBLextadloc.Visible = true;
                    LBLextadddscr.Visible = true;
                    LBLextaddcnty.Visible = true;
                    LBLextaddstate.Visible = true;
                    LBLextaddpin.Visible = true;
                    LBLextaddfdt.Visible = true;
                    LBLextaddenda.Visible = true;

                    txtextaddln1.Visible = false;
                    txtextaddln2.Visible = false;
                    txtextaddloc.Visible = false;
                    txtextadddrt.Visible = false;
                    DDLextaddcntry.Visible = false;
                    DDLextaddstate.Visible = false;
                    txtextaddpin.Visible = false;
                    txtextaddfdt.Visible = false;
                    txtextaddendate.Visible = false;

                    if (Session["exitdate"].ToString() == "")
                    {
                        addexttedt.Visible = true;
                        addextupdt.Visible = false;
                        addedtcncl.Visible = false;
                    }

                    else
                    {
                        addexttedt.Visible = false;
                        addextupdt.Visible = false;
                        addedtcncl.Visible = false;
                    }

                }
            }

            catch (Exception ex)
            {
                {  }
            }

        }

       



        protected void GV_empAI_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddladdcntry = (e.Row.FindControl("DDL_addcntry") as DropDownList);
                    configurationcollectionbo objLst = configurationbl.Get_Country(1);
                    ddladdcntry.DataSource = objLst;
                    ddladdcntry.DataTextField = "CountryTxt";
                    ddladdcntry.DataValueField = "Country";
                    ddladdcntry.DataBind();  
                    
                    string ccode1 = Session["CompCode"].ToString();
                    string emplogin1 = e.Row.Cells[0].Text;
                    int cnt1 = ccode1.Length;
                    emplogin1 = emplogin1.Substring(cnt1);
                    e.Row.Cells[0].Text = emplogin1.Trim().ToUpper();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_empAI_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

                    case "ADDEDIT":
                        exitempadd();

                        int rowedtadd = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvaddRow = GV_empAI.Rows[rowedtadd];
                        GridViewRow edtaddrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        Label LBLaddlnedt1 = (Label)edtaddrow.FindControl("lbl_addline1");

                        Label LBLaddlnedt2 = (Label)edtaddrow.FindControl("lbl_addln2");

                        Label LBLadlocedt = (Label)edtaddrow.FindControl("lbl_locality");

                        Label LBLadddscredt = (Label)edtaddrow.FindControl("lbl_dstrct");

                        Label LBLaddcntyedt = (Label)edtaddrow.FindControl("lbl_addcntry");

                        Label LBLaddstateedt = (Label)edtaddrow.FindControl("lbl_addste");

                        Label LBLaddpinedt = (Label)edtaddrow.FindControl("lbl_addpncode");

                        Label LBLaddfdtedt = (Label)edtaddrow.FindControl("lbl_addfrmdt");

                        Label LBLaddendaedt = (Label)edtaddrow.FindControl("lbl_addtodate");



                        TextBox txtaddlnedt1 = (TextBox)edtaddrow.FindControl("txt_addln1");

                        TextBox txtaddlnedt2 = (TextBox)edtaddrow.FindControl("txt_addln2");

                        TextBox txtaddlocedt = (TextBox)edtaddrow.FindControl("txt_locality");

                        TextBox txtadddrtedt = (TextBox)edtaddrow.FindControl("txt_dstrct");

                        DropDownList DDLaddcntryedt = (DropDownList)edtaddrow.FindControl("DDL_addcntry");

                        DropDownList DDLaddstateedt = (DropDownList)edtaddrow.FindControl("DDL_addstate");

                        TextBox txtaddpinedt = (TextBox)edtaddrow.FindControl("txt_addpncode");

                        TextBox txtaddfdtedt = (TextBox)edtaddrow.FindControl("txt_addfrmdate");

                        TextBox txtaddendateedt = (TextBox)edtaddrow.FindControl("txt_addtodate");

                        LinkButton addtendtedt = (LinkButton)edtaddrow.FindControl("LK_addedt");

                        LinkButton addupdtedt = (LinkButton)edtaddrow.FindControl("LK_addupdt");

                        LinkButton addcncledt = (LinkButton)edtaddrow.FindControl("LK_addcncl");

                        if (LBLaddcntyedt.Text != "" && LBLaddstateedt.Text!="")
                        {
                           
                            DDLaddcntryedt.SelectedValue = GV_empAI.DataKeys[gvaddRow.RowIndex].Values["AWART"].ToString();

                            configurationcollectionbo objLst = configurationbl.Get_states(DDLaddcntryedt.SelectedValue.ToString(), 1);
                            DDLaddstateedt.DataSource = objLst;
                            DDLaddstateedt.DataTextField = "StateTxt";
                            DDLaddstateedt.DataValueField = "State";
                            DDLaddstateedt.DataBind();
                            DDLaddstateedt.SelectedValue = GV_empAI.DataKeys[gvaddRow.RowIndex].Values["ATEXT"].ToString();
                        }


                        LBLaddlnedt1.Visible = false;
                        LBLaddlnedt2.Visible = false;
                        LBLadlocedt.Visible = false;
                        LBLadddscredt.Visible = false;
                        LBLaddcntyedt.Visible = false;
                        LBLaddstateedt.Visible = false;
                        LBLaddpinedt.Visible = false;
                        LBLaddfdtedt.Visible = false;
                        LBLaddendaedt.Visible = false;

                        txtaddlnedt1.Visible = true;
                        txtaddlnedt2.Visible = true;
                        txtaddlocedt.Visible = true;
                        txtadddrtedt.Visible = true;
                        DDLaddcntryedt.Visible = true;
                        DDLaddstateedt.Visible = true;
                        txtaddpinedt.Visible = true;
                        txtaddfdtedt.Visible = true;
                        txtaddendateedt.Visible = true;

                        addtendtedt.Visible = false;
                        addupdtedt.Visible = true;
                        addcncledt.Visible = true;

                        break;

                    case "ADDUPDT":
                        int updateindexadd = Convert.ToInt32(e.CommandArgument);

                        GridViewRow gvaddupdate = GV_empAI.Rows[updateindexadd];

                        TextBox txtaddlnedt1upd = (TextBox)gvaddupdate.FindControl("txt_addln1");

                        TextBox txtaddlnedt2upd = (TextBox)gvaddupdate.FindControl("txt_addln2");

                        TextBox txtaddlocupd = (TextBox)gvaddupdate.FindControl("txt_locality");

                        TextBox txtadddrtupd = (TextBox)gvaddupdate.FindControl("txt_dstrct");

                        DropDownList DDLaddcntryupd = (DropDownList)gvaddupdate.FindControl("DDL_addcntry");

                        DropDownList DDLaddstatupd = (DropDownList)gvaddupdate.FindControl("DDL_addstate");

                        TextBox txtaddpinupd = (TextBox)gvaddupdate.FindControl("txt_addpncode");

                        TextBox txtaddfdtupd = (TextBox)gvaddupdate.FindControl("txt_addfrmdate");

                        TextBox txtaddendatupd = (TextBox)gvaddupdate.FindControl("txt_addtodate");

                        int IDAI = Convert.ToInt32(GV_empAI.DataKeys[gvaddupdate.RowIndex].Values["ID"].ToString());

                        string eidAI = GV_empAI.DataKeys[gvaddupdate.RowIndex].Values["EMPID"].ToString();




                        if (txtaddlnedt1upd.Text != "")
                         {

                             bool? statusAI = false;
                             string hrmailAI = "";
                             string empmailAI = "";
                             configurationbo objempBo = new configurationbo();
                             configurationbl blBenI = new configurationbl();
                             objempBo.EMPID = eidAI.ToString().ToLower().Trim();
                             objempBo.ID = IDAI;
                             objempBo.NAME = txtaddlnedt1upd.Text.ToString().Trim();
                             objempBo.PASSWORD = txtaddlnedt2upd.Text.ToString().Trim();
                             objempBo.MODIFIEDBY = txtaddlocupd.Text.ToString().Trim();
                             objempBo.PORT = DDLaddcntryupd.SelectedValue.ToString().Trim();
                             objempBo.leavTEXT = DDLaddstatupd.SelectedValue.ToString().Trim();
                             objempBo.IMAP_SERVER = txtaddpinupd.Text.ToString().Trim();
                             objempBo.H_type = txtadddrtupd.Text.ToString().Trim();
                             objempBo.HR_DESCRIPTION = "";
                             objempBo.Date = Convert.ToDateTime(txtaddfdtupd.Text.ToString().Trim());
                             objempBo.Created_On = Convert.ToDateTime(txtaddendatupd.Text.ToString().Trim());
                             objempBo.flag = 3;
                             objempBo.Company_Code = Session["CompCode"].ToString().ToLower().Trim();
                             blBenI.Update_Empinfo(objempBo, ref statusAI, ref hrmailAI, ref empmailAI);

                             if (statusAI == true)
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Employee Address Info updated successfully');", true);
                             }
                             else
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to update, please check the input data');", true);
                             }
                             loadaddgv();
                         }

                         else
                         {
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter valid address details');", true);
                         }
                        break;

                    case "ADDCNCL":
                        int addedtcncl = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvadd = GV_empAI.Rows[addedtcncl];
                        loadaddgv();
                        break;
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void loadaddgv()
        {
            configurationcollectionbo objspaylstAI = new configurationcollectionbo();
            configurationbl blAI = new configurationbl();
            objspaylstAI = blAI.Get_empaddress_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
            GV_empAI.DataSource = objspaylstAI;
            GV_empAI.DataBind();
            exitempadd();
        }

        protected void DDL_addcntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GV_empAI.Rows)
                {
                    DropDownList ddlcntry1 = (DropDownList)row.FindControl("DDL_addcntry");

                    DropDownList ddlstate2 = (DropDownList)row.FindControl("DDL_addstate");

                    configurationcollectionbo objLst = configurationbl.Get_states(ddlcntry1.SelectedValue.ToString(), 1);
                    ddlstate2.DataSource = objLst;
                    ddlstate2.DataTextField = "StateTxt";
                    ddlstate2.DataValueField = "State";
                    ddlstate2.DataBind();
                }
            }
            catch (Exception ex) { }
        }


        public void exitdocmod()
        {
            try
            {
                foreach (GridViewRow rowdoci in GV_docinfo.Rows)
                {

                    Label LBLdoctyp = (Label)rowdoci.FindControl("lbl_doctype");

                    TextBox txtdoctyp = (TextBox)rowdoci.FindControl("txt_doctype");

                    FileUpload fileupdoc = (FileUpload)rowdoci.FindControl("file_updtdoccopy");

                    LinkButton extdocedt = (LinkButton)rowdoci.FindControl("LK_docedit");

                    LinkButton extdocupdt = (LinkButton)rowdoci.FindControl("LK_docupdt");

                    LinkButton extdoccncl = (LinkButton)rowdoci.FindControl("LK_doccncl");

                    LinkButton extdocdown = (LinkButton)rowdoci.FindControl("LK_docdownload");

                    LBLdoctyp.Visible = true;
                    txtdoctyp.Visible = false;
                    fileupdoc.Visible = false;

                    GV_docinfo.Columns[4].Visible = false;

                    string generatests = GV_docinfo.DataKeys[rowdoci.RowIndex].Values[3].ToString();

                    if (generatests.ToString().Trim() == "" || generatests.ToString().Trim() == null)
                    {
                        extdocdown.Visible = false;
                    }
                    else
                    {
                        extdocdown.Visible = true;
                    }

                    if (Session["exitdate"].ToString() == "")
                    {
                        extdocedt.Visible = true;
                        extdocupdt.Visible = false;
                        extdoccncl.Visible = false;
                    }

                    else
                    {
                        extdocedt.Visible = false;
                        extdocupdt.Visible = false;
                        extdoccncl.Visible = false;
                    }
                }
            }
            catch (Exception ex) { }
        }


        protected void GV_docinfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string ccode1 = Session["CompCode"].ToString();
                    string emplogin1 = e.Row.Cells[0].Text;
                    int cnt1 = ccode1.Length;
                    emplogin1 = emplogin1.Substring(cnt1);
                    e.Row.Cells[0].Text = emplogin1.Trim().ToUpper();
                }
            }
            catch (Exception ex) { }
        }

        protected void GV_docinfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

                    case "DOCDOWN":
                        int docget = Convert.ToInt32(e.CommandArgument);

                        GridViewRow gvdocdownload = GV_docinfo.Rows[docget];

                        string docfile = GV_docinfo.DataKeys[gvdocdownload.RowIndex].Values["CLOGO"].ToString();

                        string ImgExtupd = Path.GetExtension(docfile.ToString().ToUpper());

                        string dofiledown = GV_docinfo.DataKeys[gvdocdownload.RowIndex].Values["EMPID"].ToString();

                        string doctypfiledown = GV_docinfo.DataKeys[gvdocdownload.RowIndex].Values["DESCRIPTION"].ToString();

                        Response.ContentType = "application/octet-stream";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + doctypfiledown.ToString().Trim() + '/' + dofiledown.ToString().Trim() + '/' + System.DateTime.Now + ImgExtupd.ToString().Trim());
                        Response.TransmitFile(docfile);
                        Response.End();
                        break;

                    case "DCEDIT":
                        exitdocmod();

                        int rowedtdoc = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvdocRow = GV_docinfo.Rows[rowedtdoc];
                        GridViewRow edtdocrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        string doctp = GV_docinfo.DataKeys[edtdocrow.RowIndex].Values["DESCRIPTION"].ToString();

                        Label LBLedtdoctyp = (Label)edtdocrow.FindControl("lbl_doctype");

                        TextBox txtedtdoctyp = (TextBox)edtdocrow.FindControl("txt_doctype");

                        LinkButton docedtedt = (LinkButton)edtdocrow.FindControl("LK_docedit");

                        LinkButton docupdtedt = (LinkButton)edtdocrow.FindControl("LK_docupdt");

                        LinkButton doccncledt = (LinkButton)edtdocrow.FindControl("LK_doccncl");

                        LinkButton docdownedt = (LinkButton)edtdocrow.FindControl("LK_docdownload");

                        GV_docinfo.Columns[4].Visible = true;

                        FileUpload fileupedtdoc = (FileUpload)edtdocrow.FindControl("file_updtdoccopy");

                        if (doctp.ToString().Trim() == "Employee Photo")
                        {
                            txtedtdoctyp.Enabled = false;
                        }
                        else
                        {
                            txtedtdoctyp.Enabled = true;
                        }

                        LBLedtdoctyp.Visible = false;
                        txtedtdoctyp.Visible = true;
                        fileupedtdoc.Visible = true;
                        docdownedt.Visible = false;

                        docedtedt.Visible = false;
                        docupdtedt.Visible = true;
                        doccncledt.Visible = true;
                        break;

                    case "DCUPDT":
                        int updatedocindex = Convert.ToInt32(e.CommandArgument);

                        GridViewRow gvudocupdate = GV_docinfo.Rows[updatedocindex];

                        TextBox txtdoctypupd = (TextBox)gvudocupdate.FindControl("txt_doctype");

                        int docID = Convert.ToInt32(GV_docinfo.DataKeys[gvudocupdate.RowIndex].Values["ID"].ToString());

                        string doceid = GV_docinfo.DataKeys[gvudocupdate.RowIndex].Values["EMPID"].ToString();

                        string docid = GV_docinfo.DataKeys[gvudocupdate.RowIndex].Values["desig"].ToString();

                        string docddltyp = GV_docinfo.DataKeys[gvudocupdate.RowIndex].Values["DESCRIPTION"].ToString();

                        GV_docinfo.Columns[4].Visible = true;

                        FileUpload fileupdocupd = (FileUpload)gvudocupdate.FindControl("file_updtdoccopy");

                        string ImgExtup = Path.GetExtension(fileupdocupd.FileName.ToString().ToUpper());

                        //if (txtdoctypupd.Text != "")
                        //{


                        string docPath = "";
                        DateTime dt = DateTime.Now;
                        string dtt = "";
                        string filePath = "";
                        bool? statusdoc = false;
                        string hrmaildoc = "";
                        string empmaildoc = "";
                        configurationbo objempBo = new configurationbo();
                        configurationbl blBenI = new configurationbl();
                        if (fileupdocupd.HasFile)
                        {
                            if (ImgExtup == ".JPG" | ImgExtup == ".JPEG" | ImgExtup == ".PDF" | ImgExtup == ".PNG")
                            {
                                if (docddltyp.ToString().Trim() == "Employee Photo")
                                {

                                    if (ImgExtup == ".JPG" | ImgExtup == ".JPEG" | ImgExtup == ".PNG")
                                    {
                                        dtt = dt.ToString("yyyy-MM-dd hh:mm:ss").Replace(':', '-');
                                        filePath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                           + doceid.ToString().ToLower().Trim() + "-" + docddltyp.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(fileupdocupd.FileName);
                                        docPath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                           + doceid.ToString().ToLower().Trim() + "-" + docddltyp.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(fileupdocupd.FileName);
                                        fileupdocupd.PostedFile.SaveAs(filePath);



                                        objempBo.EMPID = doceid.ToString().ToLower().Trim();
                                        objempBo.ID = docID;
                                        objempBo.NAME = txtdoctypupd.Text.ToString().Trim();
                                        objempBo.PASSWORD = docid.ToString().Trim();
                                        objempBo.MODIFIEDBY = "";
                                        objempBo.PORT = "";
                                        objempBo.leavTEXT = "";
                                        objempBo.IMAP_SERVER = docPath.ToString().Trim();
                                        objempBo.H_type = "";
                                        objempBo.HR_DESCRIPTION = "";
                                        objempBo.Date = DateTime.Now;
                                        objempBo.Created_On = DateTime.Now;
                                        objempBo.flag = 5;
                                        objempBo.Company_Code = Session["CompCode"].ToString().ToLower().Trim();
                                        blBenI.Update_Empinfo(objempBo, ref statusdoc, ref hrmaildoc, ref empmaildoc);

                                        if (statusdoc == true)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Employee Document Info updated successfully');", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to update, please check the input data');", true);
                                        }

                                    }

                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Only JPEG,JPG,PNG formats are allowed to upload employee photo..!')", true);
                                    }
                                }



                                else
                                {
                                    if (ImgExtup == ".PDF")
                                    {
                                        dtt = dt.ToString("yyyy-MM-dd hh:mm:ss").Replace(':', '-');
                                        filePath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                           + doceid.ToString().ToLower().Trim() + "-" + docddltyp.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(fileupdocupd.FileName);
                                        docPath = "C:/inetpub/wwwroot/TicketingTool_Test/PayCompute_Files/Payc_Empdocuments/"
                                           + doceid.ToString().ToLower().Trim() + "-" + docddltyp.ToString().Trim() + "-" + dtt.Trim() + Path.GetExtension(fileupdocupd.FileName);
                                        fileupdocupd.PostedFile.SaveAs(filePath);



                                        objempBo.EMPID = doceid.ToString().ToLower().Trim();
                                        objempBo.ID = docID;
                                        objempBo.NAME = txtdoctypupd.Text.ToString().Trim();
                                        objempBo.PASSWORD = docid.ToString().Trim();
                                        objempBo.MODIFIEDBY = "";
                                        objempBo.PORT = "";
                                        objempBo.leavTEXT = "";
                                        objempBo.IMAP_SERVER = docPath.ToString().Trim();
                                        objempBo.H_type = "";
                                        objempBo.HR_DESCRIPTION = "";
                                        objempBo.Date = DateTime.Now;
                                        objempBo.Created_On = DateTime.Now;
                                        objempBo.flag = 5;
                                        objempBo.Company_Code = Session["CompCode"].ToString().ToLower().Trim();
                                        blBenI.Update_Empinfo(objempBo, ref statusdoc, ref hrmaildoc, ref empmaildoc);

                                        if (statusdoc == true)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Employee Document Info updated successfully');", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to update, please check the input data');", true);
                                        }
                                    }

                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Only PDF formats are allowed to upload..!')", true);
                                    }
                                }
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only PDF and Image formats are allowed to upload..!')", true);
                            }
                        }

                        else
                        {
                            objempBo.EMPID = doceid.ToString().ToLower().Trim();
                            objempBo.ID = docID;
                            objempBo.NAME = txtdoctypupd.Text.ToString().Trim();
                            objempBo.PASSWORD = docid.ToString().Trim();
                            objempBo.MODIFIEDBY = "";
                            objempBo.PORT = "";
                            objempBo.leavTEXT = "";
                            objempBo.IMAP_SERVER = docPath.ToString().Trim();
                            objempBo.H_type = "";
                            objempBo.HR_DESCRIPTION = "";
                            objempBo.Date = DateTime.Now;
                            objempBo.Created_On = DateTime.Now;
                            objempBo.flag = 5;
                            objempBo.Company_Code = Session["CompCode"].ToString().ToLower().Trim();
                            blBenI.Update_Empinfo(objempBo, ref statusdoc, ref hrmaildoc, ref empmaildoc);

                            if (statusdoc == true)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Employee Document Info updated successfully');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to update, please check the input data');", true);
                            }
                        }

                        loaddocgv();
                        //}

                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter valid Document description');", true);
                        //}
                        break;

                    case "DCCNCL":
                        int doccncl = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvdoc = GV_docinfo.Rows[doccncl];
                        loaddocgv();
                        break;
                }
            }
            catch (Exception ex) { }
        }

        public void loaddocgv()
        {
            configurationcollectionbo objspaylstDCI = new configurationcollectionbo();
            configurationbl blDCI = new configurationbl();
            objspaylstDCI = blDCI.Get_empdocument_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
            GV_docinfo.DataSource = objspaylstDCI;
            GV_docinfo.DataBind();
            exitdocmod();
        }



        public void exitbankmode()
        {
            try
            {
                foreach (GridViewRow rowbk in GV_empBI.Rows)
                {

                    Label lblextbkname = (Label)rowbk.FindControl("lbl_bkname");

                    Label lblextbkaccnum = (Label)rowbk.FindControl("lbl_accnum");

                    Label lblextbkifsc = (Label)rowbk.FindControl("lbl_bkifsccode");

                    Label lblextbkbrnch = (Label)rowbk.FindControl("lbl_bkbrnch");

                    Label lblextdsrct = (Label)rowbk.FindControl("lbl_bkdstrct");

                    Label lblextbkcntry = (Label)rowbk.FindControl("lbl_bkcntry");

                    Label lblbkste = (Label)rowbk.FindControl("lbl_bkstste");


                    TextBox txtextbkname = (TextBox)rowbk.FindControl("txt_bkname");

                    TextBox txtextbkacc = (TextBox)rowbk.FindControl("txt_accnum");

                    TextBox txtextbkifsc = (TextBox)rowbk.FindControl("txt_bkifsc");

                    TextBox txtextbkbrch = (TextBox)rowbk.FindControl("txt_bkbrnch");

                    TextBox txtextbkdtrct= (TextBox)rowbk.FindControl("txt_bkdstrct");

                    DropDownList DDLextbkcntry = (DropDownList)rowbk.FindControl("DDL_bkcntry");

                    DropDownList DDLextbkstate = (DropDownList)rowbk.FindControl("DDL_bkstate");


                    LinkButton extbkedt = (LinkButton)rowbk.FindControl("LK_bkedit");

                    LinkButton extbkupdt = (LinkButton)rowbk.FindControl("LK_bkupdt");

                    LinkButton edtbkcncl = (LinkButton)rowbk.FindControl("LK_bkcncl");


                    lblextbkname.Visible = true;
                    lblextbkaccnum.Visible = true;
                    lblextbkifsc.Visible = true;
                    lblextbkbrnch.Visible = true;
                    lblextdsrct.Visible = true;
                    lblextbkcntry.Visible = true;
                    lblbkste.Visible = true;

                    txtextbkname.Visible = false;
                    txtextbkacc.Visible = false;
                    txtextbkifsc.Visible = false;
                    txtextbkbrch.Visible = false;
                    txtextbkdtrct.Visible = false;
                    DDLextbkcntry.Visible = false;
                    DDLextbkstate.Visible = false;

                    if (Session["exitdate"].ToString() == "")
                    {
                        extbkedt.Visible = true;
                        extbkupdt.Visible = false;
                        edtbkcncl.Visible = false;
                    }

                    else
                    {
                        extbkedt.Visible = false;
                        extbkupdt.Visible = false;
                        edtbkcncl.Visible = false;
                    }

                }
            }

            catch (Exception ex)
            {
                { }
            }

        }

        protected void GV_empBI_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlbkcntry = (e.Row.FindControl("DDL_bkcntry") as DropDownList);
                    configurationcollectionbo objLst = configurationbl.Get_Country(1);
                    ddlbkcntry.DataSource = objLst;
                    ddlbkcntry.DataTextField = "CountryTxt";
                    ddlbkcntry.DataValueField = "Country";
                    ddlbkcntry.DataBind();

                    string ccode1 = Session["CompCode"].ToString();
                    string emplogin1 = e.Row.Cells[0].Text;
                    int cnt1 = ccode1.Length;
                    emplogin1 = emplogin1.Substring(cnt1);
                    e.Row.Cells[0].Text = emplogin1.Trim().ToUpper();
                }
            }
            catch (Exception ex) { }
        }

        protected void GV_empBI_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

                    case "BKEDIT":
                        exitbankmode();

                        int rowedtbk= Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvbkRow = GV_empBI.Rows[rowedtbk];
                        GridViewRow edtbkrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        Label lblbknameedt = (Label)edtbkrow.FindControl("lbl_bkname");

                        Label lblbkaccnumedt = (Label)edtbkrow.FindControl("lbl_accnum");

                        Label lblbkifscedt = (Label)edtbkrow.FindControl("lbl_bkifsccode");

                        Label lblbkbrnchedt = (Label)edtbkrow.FindControl("lbl_bkbrnch");

                        Label lbldsrctedt = (Label)edtbkrow.FindControl("lbl_bkdstrct");

                        Label lblbkcntryedt = (Label)edtbkrow.FindControl("lbl_bkcntry");

                        Label lblbksteedt = (Label)edtbkrow.FindControl("lbl_bkstste");


                        TextBox txtbknameedt = (TextBox)edtbkrow.FindControl("txt_bkname");

                        TextBox txtbkaccedt = (TextBox)edtbkrow.FindControl("txt_accnum");

                        TextBox txtbkifscedt = (TextBox)edtbkrow.FindControl("txt_bkifsc");

                        TextBox txtbkbrchedt = (TextBox)edtbkrow.FindControl("txt_bkbrnch");

                        TextBox txtbkdtrctedt = (TextBox)edtbkrow.FindControl("txt_bkdstrct");

                        DropDownList DDLbkcntryedt = (DropDownList)edtbkrow.FindControl("DDL_bkcntry");

                        DropDownList DDLbkstateedt = (DropDownList)edtbkrow.FindControl("DDL_bkstate");


                        LinkButton bkedtedt = (LinkButton)edtbkrow.FindControl("LK_bkedit");

                        LinkButton bkupdtedt = (LinkButton)edtbkrow.FindControl("LK_bkupdt");

                        LinkButton bkcncledt = (LinkButton)edtbkrow.FindControl("LK_bkcncl");


                        if (lblbkcntryedt.Text.Trim() != "" && lblbksteedt.Text.Trim() != "")
                        {
                            
                            DDLbkcntryedt.SelectedValue = GV_empBI.DataKeys[gvbkRow.RowIndex].Values["Company_Name"].ToString();

                            configurationcollectionbo objLst = configurationbl.Get_states(DDLbkcntryedt.SelectedValue.ToString(), 1);
                            DDLbkstateedt.DataSource = objLst;
                            DDLbkstateedt.DataTextField = "StateTxt";
                            DDLbkstateedt.DataValueField = "State";
                            DDLbkstateedt.DataBind();                                                        
                            DDLbkstateedt.SelectedValue = GV_empBI.DataKeys[gvbkRow.RowIndex].Values["Company_Address"].ToString();
                        }


                        lblbknameedt.Visible = false;
                        lblbkaccnumedt.Visible = false;
                        lblbkifscedt.Visible = false;
                        lblbkbrnchedt.Visible = false;
                        lbldsrctedt.Visible = false;
                        lblbkcntryedt.Visible = false;
                        lblbksteedt.Visible = false;

                        txtbknameedt.Visible = true;
                        txtbkaccedt.Visible = true;
                        txtbkifscedt.Visible = true;
                        txtbkbrchedt.Visible = true;
                        txtbkdtrctedt.Visible = true;
                        DDLbkcntryedt.Visible = true;
                        DDLbkstateedt.Visible = true;

                        bkedtedt.Visible = false;
                        bkupdtedt.Visible = true;
                        bkcncledt.Visible = true;

                        break;

                    case "BKUPDATE":
                        int updateindexbk = Convert.ToInt32(e.CommandArgument);

                        GridViewRow gvupdatebk = GV_empBI.Rows[updateindexbk];

                        TextBox txtbknamupd = (TextBox)gvupdatebk.FindControl("txt_bkname");

                        TextBox txtbkaccupd = (TextBox)gvupdatebk.FindControl("txt_accnum");

                        TextBox txtbkifscupd = (TextBox)gvupdatebk.FindControl("txt_bkifsc");

                        TextBox txtbkbrchupd= (TextBox)gvupdatebk.FindControl("txt_bkbrnch");

                        TextBox txtbkdtrctupd = (TextBox)gvupdatebk.FindControl("txt_bkdstrct");

                        DropDownList DDLbkcntryupd = (DropDownList)gvupdatebk.FindControl("DDL_bkcntry");

                        DropDownList DDLbkstateupd = (DropDownList)gvupdatebk.FindControl("DDL_bkstate");

                        int bkID = Convert.ToInt32(GV_empBI.DataKeys[gvupdatebk.RowIndex].Values["ID"].ToString());

                        string bkeid = GV_empBI.DataKeys[gvupdatebk.RowIndex].Values["EMPID"].ToString();

                        //string bkid = GV_docinfo.DataKeys[gvupdatebk.RowIndex].Values["desig"].ToString();



                        if (txtbkaccupd.Text.Trim() != "" && txtbkifscupd.Text.Trim() !="")
                         {

                             bool? statusbk = false;
                             string hrmailbk = "";
                             string empmailbk = "";
                             configurationbo objempBo = new configurationbo();
                             configurationbl blBenI = new configurationbl();
                             objempBo.EMPID = bkeid.ToString().ToLower().Trim();
                             objempBo.ID = bkID;
                             objempBo.NAME = txtbknamupd.Text.ToString().Trim();
                             objempBo.PASSWORD = txtbkaccupd.Text.ToString().Trim();
                             objempBo.MODIFIEDBY = txtbkifscupd.Text.ToString().Trim();
                             objempBo.PORT = DDLbkcntryupd.SelectedValue.ToString().Trim();
                             objempBo.leavTEXT = DDLbkstateupd.SelectedValue.ToString().Trim();
                             objempBo.IMAP_SERVER = txtbkbrchupd.Text.ToString().Trim();
                             objempBo.H_type = txtbkdtrctupd.Text.ToString().Trim();
                             objempBo.HR_DESCRIPTION = "";
                             objempBo.Date = DateTime.Now;
                             objempBo.Created_On = DateTime.Now;
                             objempBo.flag = 6;
                             objempBo.Company_Code = Session["CompCode"].ToString().ToLower().Trim();
                             blBenI.Update_Empinfo(objempBo, ref statusbk, ref hrmailbk, ref empmailbk);

                             if (statusbk == true)
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Employee Bank Info updated successfully');", true);
                             }
                             else
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to update, please check the input data');", true);
                             }
                             loadbankgv();
                         }

                         else
                         {
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter valid Bank details');", true);
                         }
                        break;

                    case "BKCNCL":
                        int bkcncl = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvbk = GV_empBI.Rows[bkcncl];
                        loadbankgv();
                        break;
                }
            }
            catch (Exception ex) { }
        }

        public void loadbankgv()
        {
            configurationcollectionbo objspaylstBI = new configurationcollectionbo();
            configurationbl blBI = new configurationbl();
            objspaylstBI = blBI.Get_empbank_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
            GV_empBI.DataSource = objspaylstBI;
            GV_empBI.DataBind();
            exitbankmode();
        }


        protected void DDL_bkcntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GV_empBI.Rows)
                {
                    DropDownList ddlcntrybk = (DropDownList)row.FindControl("DDL_bkcntry");

                    DropDownList ddlstatebk = (DropDownList)row.FindControl("DDL_bkstate");

                    configurationcollectionbo objLst = configurationbl.Get_states(ddlcntrybk.SelectedValue.ToString(), 1);
                    ddlstatebk.DataSource = objLst;
                    ddlstatebk.DataTextField = "StateTxt";
                    ddlstatebk.DataValueField = "State";
                    ddlstatebk.DataBind();
                }
            }
            catch (Exception ex) { }
        }



        public void exibenemode()
        {
            try
            {
                foreach (GridViewRow rowadd in GV_empBENI.Rows)
                {

                    Label lblextbenesiapp = (Label)rowadd.FindControl("lbl_esiapplicabe");

                    Label lblextbeneesino = (Label)rowadd.FindControl("lbl_esinum");

                    Label lblextesidisp = (Label)rowadd.FindControl("lbl_esidisp");

                    Label lblextpfapp = (Label)rowadd.FindControl("lbl_pfappli");

                    Label lblextbenepfno = (Label)rowadd.FindControl("lbl_pfnum");

                    Label lblextpfrest = (Label)rowadd.FindControl("lbl_pfres");

                    Label lblextzropens = (Label)rowadd.FindControl("lbl_zeropens");

                    Label lblextzropt = (Label)rowadd.FindControl("lbl_zeropt");


                    CheckBox chkextbeneesi = (CheckBox)rowadd.FindControl("CHK_chkesiappli");

                    TextBox txtextextbeneesino = (TextBox)rowadd.FindControl("txt_esinum");

                    TextBox txtextesidisp = (TextBox)rowadd.FindControl("txt_esidispen");

                    CheckBox chkpfallp = (CheckBox)rowadd.FindControl("chk_pfappli");

                    TextBox txtbenepfno = (TextBox)rowadd.FindControl("txt_pfnum");

                    CheckBox chkprf = (CheckBox)rowadd.FindControl("chk_pfres");

                    CheckBox chkzropen = (CheckBox)rowadd.FindControl("chk_zeropen");

                    CheckBox chkextpt = (CheckBox)rowadd.FindControl("chk_zeropt");


                    LinkButton benextedt = (LinkButton)rowadd.FindControl("LK_editben");

                    LinkButton beneextupdt = (LinkButton)rowadd.FindControl("LK_updtben");

                    LinkButton beneextcncl = (LinkButton)rowadd.FindControl("LK_cnclben");


                    lblextbenesiapp.Visible = true;
                    lblextbeneesino.Visible = true;
                    lblextesidisp.Visible = true;
                    lblextpfapp.Visible = true;
                    lblextbenepfno.Visible = true;
                    lblextpfrest.Visible = true;
                    lblextzropens.Visible = true;
                    lblextzropt.Visible = true;

                    chkextbeneesi.Visible = false;
                    txtextextbeneesino.Visible = false;
                    txtextesidisp.Visible = false;
                    chkpfallp.Visible = false;
                    txtbenepfno.Visible = false;
                    chkprf.Visible = false;
                    chkzropen.Visible = false;
                    chkextpt.Visible = false;

                    if (Session["exitdate"].ToString() == "")
                    {
                        benextedt.Visible = true;
                        beneextupdt.Visible = false;
                        beneextcncl.Visible = false;
                    }

                    else
                    {
                        benextedt.Visible = false;
                        beneextupdt.Visible = false;
                        beneextcncl.Visible = false;
                    }

                }
            }

            catch (Exception ex)
            {
                { }
            }

        }

        protected void GV_empBENI_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                 string ccode1 = Session["CompCode"].ToString();
                    string emplogin1 = e.Row.Cells[0].Text;
                    int cnt1 = ccode1.Length;
                    emplogin1 = emplogin1.Substring(cnt1);
                    e.Row.Cells[0].Text = emplogin1.Trim().ToUpper();
                }
            }
            catch (Exception ex) { }
        
        }

        protected void GV_empBENI_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

                    case "EDITBENE":
                        exibenemode();

                        int rowedtbene = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvbeneRow = GV_empBENI.Rows[rowedtbene];
                        GridViewRow edtbenerow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        Label lblbenesiappedtedt = (Label)edtbenerow.FindControl("lbl_esiapplicabe");

                    Label lblbeneesinoedt = (Label)edtbenerow.FindControl("lbl_esinum");

                    Label lblesidispedt = (Label)edtbenerow.FindControl("lbl_esidisp");

                    Label lblpfappedt = (Label)edtbenerow.FindControl("lbl_pfappli");

                    Label lblbenepfnoedt = (Label)edtbenerow.FindControl("lbl_pfnum");

                    Label lblpfrestedt = (Label)edtbenerow.FindControl("lbl_pfres");

                    Label lblzropensedt = (Label)edtbenerow.FindControl("lbl_zeropens");

                    Label lblzroptedt = (Label)edtbenerow.FindControl("lbl_zeropt");


                    CheckBox chkbeneesiedt = (CheckBox)edtbenerow.FindControl("CHK_chkesiappli");

                    TextBox txtbeneesinoedt = (TextBox)edtbenerow.FindControl("txt_esinum");

                    TextBox txtesidispedt = (TextBox)edtbenerow.FindControl("txt_esidispen");

                    CheckBox chkpfallpedt = (CheckBox)edtbenerow.FindControl("chk_pfappli");

                    TextBox txtbeneedtpfno = (TextBox)edtbenerow.FindControl("txt_pfnum");

                    CheckBox chkprfedt = (CheckBox)edtbenerow.FindControl("chk_pfres");

                    CheckBox chkedtzropen = (CheckBox)edtbenerow.FindControl("chk_zeropen");

                    CheckBox chkptedt = (CheckBox)edtbenerow.FindControl("chk_zeropt");


                    LinkButton beneedtedt1 = (LinkButton)edtbenerow.FindControl("LK_editben");

                    LinkButton beneextupdt1 = (LinkButton)edtbenerow.FindControl("LK_updtben");

                    LinkButton benecncledt1 = (LinkButton)edtbenerow.FindControl("LK_cnclben");


                    if (lblbenesiappedtedt.Text == "Yes")
                    {
                        chkbeneesiedt.Checked = true;
                    }
                    else
                    {
                        chkbeneesiedt.Checked = false;
                        chkprfedt.Checked = false;
                    }                    

                    if (lblpfappedt.Text == "Yes")
                    {
                        chkpfallpedt.Checked = true;
                    }
                    else
                    {
                        chkpfallpedt.Checked = false;
                    }

                    if (chkbeneesiedt.Checked == true)
                    {
                        txtbeneesinoedt.Enabled = true;
                        txtesidispedt.Enabled = true;
                    }
                    else
                    {
                        txtbeneesinoedt.Enabled = false;
                        txtesidispedt.Enabled = false;
                    }
                    if (chkpfallpedt.Checked == true)
                    {
                        txtbeneedtpfno.Enabled = true;
                        chkprfedt.Enabled = true;
                    }
                    else
                    {
                        txtbeneedtpfno.Enabled = false;
                        chkprfedt.Enabled = false;
                    }

                    lblbenesiappedtedt.Visible = false;
                    lblbeneesinoedt.Visible = false;
                    lblesidispedt.Visible = false;
                    lblpfappedt.Visible = false;
                    lblbenepfnoedt.Visible = false;
                    lblpfrestedt.Visible = false;
                    lblzropensedt.Visible = false;
                    lblzroptedt.Visible = false;

                    chkbeneesiedt.Visible = true;
                    txtbeneesinoedt.Visible = true;
                    txtesidispedt.Visible = true;
                    chkpfallpedt.Visible = true;
                    txtbeneedtpfno.Visible = true;
                    chkprfedt.Visible = true;
                    chkedtzropen.Visible = true;
                    chkptedt.Visible = true;

                    beneedtedt1.Visible = false;
                    beneextupdt1.Visible = true;
                    benecncledt1.Visible = true;
                        break;

                    case "UPDTBENE":
                       int updateindexbene = Convert.ToInt32(e.CommandArgument);

                       GridViewRow gvupdatebene = GV_empBENI.Rows[updateindexbene];

                       CheckBox chkbeneesiupd = (CheckBox)gvupdatebene.FindControl("CHK_chkesiappli");

                       TextBox txtbeneesinupd = (TextBox)gvupdatebene.FindControl("txt_esinum");

                       TextBox txtesidispupd = (TextBox)gvupdatebene.FindControl("txt_esidispen");

                       CheckBox chkpfallpupd = (CheckBox)gvupdatebene.FindControl("chk_pfappli");

                       TextBox txtbenepfnoupd = (TextBox)gvupdatebene.FindControl("txt_pfnum");

                       CheckBox chkprfupd = (CheckBox)gvupdatebene.FindControl("chk_pfres");

                       CheckBox chkzropenupd = (CheckBox)gvupdatebene.FindControl("chk_zeropen");

                       CheckBox chkptupd = (CheckBox)gvupdatebene.FindControl("chk_zeropt");

                       int IDbene = Convert.ToInt32(GV_empBENI.DataKeys[gvupdatebene.RowIndex].Values["ID"].ToString());

                       string eidbene = GV_empBENI.DataKeys[gvupdatebene.RowIndex].Values["EMPID"].ToString();

                       string esiapp = chkbeneesiupd.Checked == true ? "1" : "0";

                       string pfapp = chkpfallpupd.Checked == true ? "1" : "0";

                       string pfrstr = chkprfupd.Checked == true ? "1" : "0";

                       string zropen = chkzropenupd.Checked == true ? "1" : "0";

                       string zropt = chkptupd.Checked == true ? "1" : "0";


                             bool? statusben = false;
                             string hrmailbene = "";
                             string empmailbene = "";
                             configurationbo objempBo = new configurationbo();
                             configurationbl blBenI = new configurationbl();
                             objempBo.EMPID = eidbene.ToString().ToLower().Trim();
                             objempBo.ID = IDbene;
                             objempBo.NAME = esiapp.ToString().Trim();
                             objempBo.PASSWORD = txtbeneesinupd.Text.ToString().Trim();
                             objempBo.MODIFIEDBY = txtesidispupd.Text.ToString().Trim();
                             objempBo.PORT = txtbenepfnoupd.Text.ToString().Trim();
                             objempBo.leavTEXT = pfrstr.ToString().Trim();
                             objempBo.IMAP_SERVER = pfapp.ToString().Trim();
                             objempBo.H_type = zropen.ToString().Trim();
                             objempBo.HR_DESCRIPTION = zropt.ToString().Trim();
                             objempBo.Date = DateTime.Now;
                             objempBo.Created_On = DateTime.Now;
                             objempBo.flag = 7;
                             objempBo.Company_Code = Session["CompCode"].ToString().ToLower().Trim();
                             blBenI.Update_Empinfo(objempBo, ref statusben, ref hrmailbene, ref empmailbene);

                             if (statusben == true)
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Employee Benefits Info updated successfully');", true);
                             }
                             else
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to update, please check the input data');", true);
                             }
                             loadbenegv();
                         
          
                        break;

                    case "CNCLBENE":
                        int bnecncl = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvbene = GV_empBENI.Rows[bnecncl];
                        loadbenegv();
                        break;
                }
            }
            catch (Exception ex) { }
        }

        public void loadbenegv()
        {
            configurationcollectionbo objspaylstBene = new configurationcollectionbo();
            configurationbl blBene = new configurationbl();
            objspaylstBene = blBene.Get_empbeneinfo_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
            GV_empBENI.DataSource = objspaylstBene;
            GV_empBENI.DataBind();
            exibenemode();
        }

        protected void CHK_chkesiappli_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GV_empBENI.Rows)
                {
                    CheckBox chkesi = (CheckBox)row.FindControl("CHK_chkesiappli");

                    TextBox txtesinum = (TextBox)row.FindControl("txt_esinum");

                    TextBox txtesidisp = (TextBox)row.FindControl("txt_esidispen");


                    if (chkesi.Checked == true)
                    {
                        txtesinum.Enabled = true;
                        txtesidisp.Enabled = true;
                    }
                    else
                    {
                        txtesinum.Enabled = false;
                        txtesidisp.Enabled = false;
                        txtesinum.Text = "";
                        txtesidisp.Text = "";
                    }

                    

                }
            }
            catch (Exception ex) { }
        }

        protected void chk_pfappli_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GV_empBENI.Rows)
                {
                    CheckBox chkpf = (CheckBox)row.FindControl("chk_pfappli");

                    TextBox txtpfnum = (TextBox)row.FindControl("txt_pfnum");

                    CheckBox chk_pfres = (CheckBox)row.FindControl("chk_pfres");


                    if (chkpf.Checked == true)
                    {
                        txtpfnum.Enabled = true;
                        chk_pfres.Enabled = true;
                    }
                    else
                    {
                        txtpfnum.Enabled = false;
                        chk_pfres.Enabled = false;
                        txtpfnum.Text = "";
                        chk_pfres.Checked = false;
                        chk_pfres.Enabled = false;
                    }



                }
            }
            catch (Exception ex) { }
        }

        public void Load_Employees()
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_Employees(User.Identity.Name.Trim(), 1);
                DDL_srchvalupdt.DataSource = objLst;
                DDL_srchvalupdt.DataTextField = "NAME";
                DDL_srchvalupdt.DataValueField = "EMPID";
                DDL_srchvalupdt.DataBind();
                DDL_srchvalupdt.Items.Insert(0, new ListItem(" - Search Employee - ", "0"));
            }
            catch (Exception ex) { }
        }


        

        protected void DDL_srchvalupdt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                    btn_restupdt.Visible = true;
                    EmpCollBo objspaylst = new EmpCollBo();
                    EmpDataBL bl = new EmpDataBL();
                    objspaylst = bl.viewall_emp(Session["CompCode"].ToString(), DDL_srchvalupdt.SelectedValue.ToString().ToLower().Trim(), 3);
                    GV_viewemp_details.DataSource = objspaylst;
                    GV_viewemp_details.DataBind();
                    PNL_allemp_details.Visible = false;
                    btn_exprt_empinfo.Visible = false;

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_viewemp_details.Rows)
                    {
                        for (int i = 0; i < GV_viewemp_details.Rows.Count; i++)
                        {
                            Label lblRowNumber = (Label)GV_viewemp_details.Rows[i].FindControl("lblRowNumber");
                            if (i == 0)
                            {
                                frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_viewemp_details.Rows.Count - 1)
                            {
                                lrow = lblRowNumber.Text;
                            }
                        }
                    }
                    divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                    divcnt.Visible = GV_viewemp_details.Rows.Count > 0 ? true : false;
                
            }
            catch (Exception ex) { }
        }

        protected void btn_restupdt_Click(object sender, EventArgs e)
        {
            try
            {
                btn_restupdt.Visible = false;
                GV_viewemp();
                PNL_allemp_details.Visible = false;
                btn_exprt_empinfo.Visible = false;
            }
            catch (Exception ex) { }
        }

        protected void GV_empdesightry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string ccode1 = Session["CompCode"].ToString();
                    string emplogin1 = e.Row.Cells[0].Text;
                    int cnt1 = ccode1.Length;
                    emplogin1 = emplogin1.Substring(cnt1);
                    e.Row.Cells[0].Text = emplogin1.Trim().ToUpper();
                }
            }
            catch (Exception ex) { }
        }

        protected void GV_empmgr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string cc = Session["CompCode"].ToString();
                    string eid = e.Row.Cells[0].Text;
                    int cnt1 = cc.Length;
                    eid = eid.Substring(cnt1);
                    e.Row.Cells[0].Text = eid.Trim().ToUpper();
                }
            }
            catch (Exception ex) { }
        }

        public void exportgvs()
        {
            try
            {
                //------- Personal info----------

                gv_exp1.DataSource = null;
                gv_exp1.DataBind();
                configurationcollectionbo lstPI1 = new configurationcollectionbo();
                configurationbl blPI1 = new configurationbl();
                lstPI1 = blPI1.Get_empinfo_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                gv_exp1.DataSource = lstPI1;
                gv_exp1.DataBind();

                //--------------- Dept Info --------------------

                gv_expty2.DataSource = null;
                gv_expty2.DataBind();
                configurationcollectionbo lstDI2 = new configurationcollectionbo();
                configurationbl blDI2 = new configurationbl();
                lstDI2 = blDI2.Get_empinfo_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                gv_expty2.DataSource = lstDI2;
                gv_expty2.DataBind();

                //--------------- Desig Info --------------------

                gv_exprt3.DataSource = null;
                gv_exprt3.DataBind();
                configurationbl ObjLeaveRequestBl3 = new configurationbl();
                List<configurationbo> LeaveReqboList13 = new List<configurationbo>();
                LeaveReqboList13 = ObjLeaveRequestBl3.Get_empmgrdesig_config_full(Session["CompCode"].ToString().ToLower().Trim().ToLower().Trim(), ViewState["empidview"].ToString(), 1);
              
                gv_exprt3.DataSource = LeaveReqboList13;
                gv_exprt3.DataBind();

                //--------------- Mgr Info --------------------

                gv_expt4.DataSource = null;
                gv_expt4.DataBind();
                configurationbl bl5 = new configurationbl();
                List<configurationbo> lst5 = new List<configurationbo>();
                lst5 = bl5.Get_mgrdesig_config_full(Session["CompCode"].ToString().ToLower().Trim().ToLower().Trim(), ViewState["empidview"].ToString(), 1);
         
                gv_expt4.DataSource = lst5;
                gv_expt4.DataBind();

                //------- Address ifo----------

                gv_exprt5.DataSource = null;
                gv_exprt5.DataBind();
                configurationcollectionbo objspaylstAI7 = new configurationcollectionbo();
                configurationbl blAI7 = new configurationbl();
                objspaylstAI7 = blAI7.Get_empaddress_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                gv_exprt5.DataSource = objspaylstAI7;
                gv_exprt5.DataBind();


                //------- Contact ifo----------

                gv_exprt6.DataSource = null;
                gv_exprt6.DataBind();
                configurationcollectionbo objspaylstCI8 = new configurationcollectionbo();
                configurationbl blCI8 = new configurationbl();
                objspaylstCI8 = blCI8.Get_empcommunication_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                gv_exprt6.DataSource = objspaylstCI8;
                gv_exprt6.DataBind();


                //------- Document ifo----------

                gv_exprt7.DataSource = null;
                gv_exprt7.DataBind();
                configurationcollectionbo objspaylstDCI9 = new configurationcollectionbo();
                configurationbl blDCI9 = new configurationbl();
                objspaylstDCI9 = blDCI9.Get_empdocument_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                gv_exprt7.DataSource = objspaylstDCI9;
                gv_exprt7.DataBind();

                //------- Bank ifo----------

                gv_exprt8.DataSource = null;
                gv_exprt8.DataBind();
                configurationcollectionbo objspaylstBI10 = new configurationcollectionbo();
                configurationbl blBI10 = new configurationbl();
                objspaylstBI10 = blBI10.Get_empbank_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                gv_exprt8.DataSource = objspaylstBI10;
                gv_exprt8.DataBind();

                //------- Benefits ifo----------

                gv_exprt9.DataSource = null;
                gv_exprt9.DataBind();
                configurationcollectionbo objspaylstBenI11 = new configurationcollectionbo();
                configurationbl blBenI11 = new configurationbl();
                objspaylstBenI11 = blBenI11.Get_empbeneinfo_admin(Session["CompCode"].ToString(), ViewState["empidview"].ToString(), 1);
                gv_exprt9.DataSource = objspaylstBenI11;
                gv_exprt9.DataBind();


                gv_exp1.HeaderRow.BackColor = gv_expty2.HeaderRow.BackColor = gv_exprt3.HeaderRow.BackColor = gv_expt4.HeaderRow.BackColor = gv_exprt5.HeaderRow.BackColor = gv_exprt6.HeaderRow.BackColor = gv_exprt7.HeaderRow.BackColor = gv_exprt8.HeaderRow.BackColor = gv_exprt9.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                gv_exp1.HeaderStyle.Font.Bold = gv_expty2.HeaderStyle.Font.Bold = gv_exprt3.HeaderStyle.Font.Bold = gv_expt4.HeaderStyle.Font.Bold = gv_exprt5.HeaderStyle.Font.Bold = gv_exprt6.HeaderStyle.Font.Bold = gv_exprt7.HeaderStyle.Font.Bold = gv_exprt8.HeaderStyle.Font.Bold = gv_exprt9.HeaderStyle.Font.Bold = true;
                gv_exp1.GridLines = gv_expty2.GridLines = gv_exprt3.GridLines = gv_expt4.GridLines = gv_exprt5.GridLines = gv_exprt6.GridLines = gv_exprt7.GridLines = gv_exprt8.GridLines = gv_exprt9.GridLines = GridLines.Both;
            }

            catch (Exception ex) { }
        }


    }
}
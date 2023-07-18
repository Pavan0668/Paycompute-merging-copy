
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Configuration;
using iEmpPowerMaster_Load;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute.SPayc_Collection_BO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.SPaycompute;
using AjaxControlToolkit;

namespace iEmpPower.UI.Configuration
{
    public partial class lock_lockuser : System.Web.UI.Page
    {
        private string sCreateUserLogPath = ConfigurationManager.AppSettings["CreateUserLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        #region Error Log Object
        iEmpPower_DT_Wizard_Utility ObjLogError = new iEmpPower_DT_Wizard_Utility();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (!IsPostBack)
                {
                    HideTabs();
                    view1.Visible = true;
                    Tab1.CssClass = "nav-link active p-2";
                    loadGrid();
                    Load_Employees();
                    Load_Desig();
                    Load_Leaves();
                    LoadHolidayCalendar();
                    LoadmagrMApping();
                    LoadDesigmappingdb();
                    LoadcompLeavQuota();
                    loadweekend();
                    loadDDL();
                    loadCompanyDeatils();
                    loadleavecompoff();
                    Loadempconfig();
                }
            }
            catch (Exception ex) { }

        }


        public void Load_Desig()
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_Designationa(User.Identity.Name.Trim(), 1);
                ddldesig.DataSource = objLst;
                ddldesig.DataTextField = "desigTEXT";
                ddldesig.DataValueField = "desig";
                ddldesig.DataBind();
                ddldesig.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

                gv_desing.DataSource = objLst;
                gv_desing.DataBind();
            }
            catch (Exception ex) { }
        }

        private void LoadHolidayCalendar()
        {
            try
            {
                //MsgCls(string.Empty, Label2, System.Drawing.Color.Transparent);
                configurationbl ObjLeaveRequestBl = new configurationbl();
                List<configurationbo> LeaveReqboList1 = new List<configurationbo>();
                LeaveReqboList1 = ObjLeaveRequestBl.Get_HolidayCalender(Session["CompCode"].ToString(), DateTime.Now.Year.ToString(), 1);
                // Session.Add("IexpGrdInfo", LeaveReqboList1);

                if (LeaveReqboList1 == null || LeaveReqboList1.Count == 0)
                {
                    //MsgCls("No Records Found !", Label2, System.Drawing.Color.Red);
                    Grd_HolidayCalendar.Visible = false;
                    Grd_HolidayCalendar.DataSource = null;
                    Grd_HolidayCalendar.DataBind();
                    return;
                }
                else
                {
                    //MsgCls("", Label2, System.Drawing.Color.Transparent);
                    Grd_HolidayCalendar.Visible = true;
                    Grd_HolidayCalendar.DataSource = null;
                    Grd_HolidayCalendar.DataBind();
                    Grd_HolidayCalendar.DataSource = LeaveReqboList1;
                    Grd_HolidayCalendar.SelectedIndex = -1;
                    Grd_HolidayCalendar.DataBind();
                }

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in Grd_HolidayCalendar.Rows)
                {
                    for (int i = 0; i < Grd_HolidayCalendar.Rows.Count; i++)
                    {
                        Label lblholiRowNumber = (Label)Grd_HolidayCalendar.Rows[i].FindControl("lblholiRowNumber");
                        if (i == 0)
                        {
                            frow = lblholiRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == Grd_HolidayCalendar.Rows.Count - 1)
                        {
                            lrow = lblholiRowNumber.Text;
                        }
                    }
                }
                divholicnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + LeaveReqboList1.Count + " entries";
                divholicnt.Visible = Grd_HolidayCalendar.Rows.Count > 0 ? true : false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void LoadmagrMApping()
        {
            try
            {
                
                configurationbl ObjLeaveRequestBl = new configurationbl();
                List<configurationbo> LeaveReqboList1 = new List<configurationbo>();

                LeaveReqboList1 = ObjLeaveRequestBl.Get_EmpManagerManpping(Session["CompCode"].ToString(), 1);

                if (LeaveReqboList1 == null || LeaveReqboList1.Count == 0)
                {
                    //MsgCls("No Records Found !", Label2, System.Drawing.Color.Red);
                    gvmgrmappingdb.Visible = false;
                    gvmgrmappingdb.DataSource = null;
                    gvmgrmappingdb.DataBind();
                    return;
                }
                else
                {
                    //MsgCls("", Label2, System.Drawing.Color.Transparent);
                    gvmgrmappingdb.Visible = true;
                    gvmgrmappingdb.DataSource = null;
                    gvmgrmappingdb.DataBind();
                    gvmgrmappingdb.DataSource = LeaveReqboList1;
                    gvmgrmappingdb.SelectedIndex = -1;
                    gvmgrmappingdb.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void loadGrid()
        {

            configurationbo ObjBo = new configurationbo();
            configurationbl ObjBl = new configurationbl();
            configurationcollectionbo objoList = new configurationcollectionbo();
            objoList = ObjBl.Get_USersLock(User.Identity.Name,"", 1);
            if (objoList.Count > 0)
            {
                gv_users.DataSource = objoList;
                gv_users.DataBind();
                gv_users.Visible = true;

                //lblMessageBoard.Text = "";
                //lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;
            }

            else
            {
                gv_users.DataSource = null;
                gv_users.DataBind();
            }
            btn_srchlog_reset.Visible = false;


            string frow = "", lrow = "";  ////Row count

            foreach (GridViewRow row in gv_users.Rows)
            {
                for (int i = 0; i < gv_users.Rows.Count; i++)
                {
                    Label lblRowNumber = (Label)gv_users.Rows[i].FindControl("lblRowNumber");
                    if (i == 0)
                    {
                        frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                    }
                    if (i == gv_users.Rows.Count - 1)
                    {
                        lrow = lblRowNumber.Text;
                    }
                }
            }

            divgencnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objoList.Count + " entries";
            divgencnt.Visible = gv_users.Rows.Count > 0 ? true : false;

        }

        protected void LoadcompLeavQuota()
        {

            configurationbo ObjBo = new configurationbo();
            configurationbl ObjBl = new configurationbl();
            configurationcollectionbo objoList = new configurationcollectionbo();
            objoList = ObjBl.Get_CompLeavManpping(User.Identity.Name, 1);
            if (objoList.Count > 0)
            {
                gvLeavcomquota.DataSource = objoList;
                gvLeavcomquota.DataBind();
                gvLeavcomquota.Visible = true;

                //lblMessageBoard.Text = "";
                //lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;
            }

            else
            {
                gvLeavcomquota.DataSource = null;
                gvLeavcomquota.DataBind();
            }
            btnQuotagenerate.Visible = gvLeavcomquota.Rows.Count > 0 ? true : false;

        }

        protected void LoadDesigmappingdb()
        {

            configurationbo ObjBo = new configurationbo();
            configurationbl ObjBl = new configurationbl();
            configurationcollectionbo objoList = new configurationcollectionbo();
            objoList = ObjBl.Get_EmpDesignationManpping(User.Identity.Name, 1);
            if (objoList.Count > 0)
            {
                gvCrtdesMappDb.DataSource = objoList;
                gvCrtdesMappDb.DataBind();
                gvCrtdesMappDb.Visible = true;
                btnexportdesig.Visible = gvCrtdesMappDb.Rows.Count > 0 ? true : false;
            }

            else
            {
                gvCrtdesMappDb.DataSource = null;
                gvCrtdesMappDb.DataBind();
            }

        }


        protected void gv_users_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string exists = gv_users.DataKeys[e.Row.RowIndex].Values[3].ToString();
                    LinkButton ltnlock = (LinkButton)e.Row.FindControl("lnkLocklogin");
                    LinkButton ltngene = (LinkButton)e.Row.FindControl("lnkGenertelogin");
                    LinkButton ltntemlock = (LinkButton)e.Row.FindControl("lnktemplock");
                    LinkButton ltnunlock = (LinkButton)e.Row.FindControl("lnkunlock");

                    if (exists.ToString().Trim() == "")
                    {
                        ltnlock.Visible = false;
                        ltngene.Visible = true;
                        ltntemlock.Visible = false;
                        ltnunlock.Visible = false;

                    }
                    else
                    {
                        ltnlock.Visible = true;
                        ltntemlock.Visible = false;
                        ltngene.Visible = false;
                        ltnunlock.Visible = false;
                        string LOCKID1 = "";
                        LOCKID1 = gv_users.DataKeys[e.Row.RowIndex].Values["EMPID"].ToString();
                        bool? status = false;
                        configurationbl ObjBl1 = new configurationbl();
                        ObjBl1.tempolock_unlockUser(User.Identity.Name, LOCKID1, 2, ref status);
                        if (status == true)
                        {
                            ltnunlock.Visible = false;
                            ltntemlock.Visible = true;
                        }
                        else
                        {
                            ltnunlock.Visible = true;
                            ltntemlock.Visible = false;
                        }
                    }


                    Label lbl = (Label)e.Row.FindControl("lblID");
                    string ccode = Session["CompCode"].ToString();
                    string emplogin = lbl.Text; //e.Row.Cells[7].Text;//
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    e.Row.Cells[1].Text = emplogin.Trim().ToUpper();

                    Label lbldob = (Label)e.Row.FindControl("lbl_DOB");
                    string DOBstr = lbldob.Text;

                    string DOB = DOBstr.Substring(0, DOBstr.Length - 8);
                    e.Row.Cells[3].Text = DOB.Trim();

                   


                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

       

        public void Load_Employees()
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_Employees(User.Identity.Name.Trim(), 1);
                ddlMEmployee.DataSource = objLst;
                ddlMEmployee.DataTextField = "NAME";
                ddlMEmployee.DataValueField = "EMPID";
                ddlMEmployee.DataBind();
                ddlMEmployee.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

                ddlMManager.DataSource = objLst;
                ddlMManager.DataTextField = "NAME";
                ddlMManager.DataValueField = "EMPID";
                ddlMManager.DataBind();
                ddlMManager.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

                ddldesigEmployee.DataSource = objLst;
                ddldesigEmployee.DataTextField = "NAME";
                ddldesigEmployee.DataValueField = "EMPID";
                ddldesigEmployee.DataBind();
                ddldesigEmployee.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

                ddlEmpCheckQuota.DataSource = objLst;
                ddlEmpCheckQuota.DataTextField = "NAME";
                ddlEmpCheckQuota.DataValueField = "EMPID";
                ddlEmpCheckQuota.DataBind();
                ddlEmpCheckQuota.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

               
            }
            catch (Exception ex) { }
        }

        protected DataTable EmpMgrMapping()
        {
            DataTable dtExcelData1 = new DataTable();
            dtExcelData1.Columns.AddRange(new DataColumn[4]
                    { 
                     new DataColumn("Employee_ID",typeof(string)),
                      new DataColumn("Employee_NAME",typeof(string)),
                       new DataColumn("MGR_ID",typeof(string)),
                     new DataColumn("MGR_NAME",typeof(string))
                     });
            return dtExcelData1;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMEmployee.SelectedValue.Trim() != ddlMManager.SelectedValue.Trim())
                {
                    bool? stats = false;
                    string error = "";
                    ValidationChk(ddlMEmployee.SelectedValue.Trim(), "", 3, 0, System.DateTime.Today, System.DateTime.Today, ref stats, ref error);
                    if (stats == true)
                    {
                        using (DataTable Dt = ViewState["mgrmapping"] != null ? (DataTable)ViewState["mgrmapping"] : EmpMgrMapping())
                        {
                            Dt.Rows.Add(ddlMEmployee.SelectedValue, ddlMEmployee.SelectedItem, ddlMManager.SelectedValue, ddlMManager.SelectedItem);
                            ViewState["mgrmapping"] = Dt;

                            btnSubmit.Visible = Dt.Rows.Count > 0 ? true : false;
                        }
                        GV_empmgr.DataSource = (DataTable)ViewState["mgrmapping"];
                        GV_empmgr.DataBind();
                        ddlMManager.SelectedIndex = -1;
                        ddlMEmployee.SelectedIndex = -1;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('" + error + "')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Manager and Employee Cannot be Same Person!!')", true);
                }
            }
            catch (Exception ex) { }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool? st2 = false;
                configurationbl objBl = new configurationbl();
                configurationbo bo2 = new configurationbo();
                DataTable dt2 = new DataTable();
                using (dt2 = (DataTable)ViewState["mgrmapping"])
                {
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        bo2.Company_Code = Session["CompCode"].ToString();
                        bo2.EMPID = dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                        bo2.AppID = dt2.Rows[j]["MGR_ID"].ToString().Trim();
                        bo2.LoginID = User.Identity.Name.Trim();
                        bo2.flag = 1;
                        objBl.Create_mapping_emp(bo2, ref st2);
                    }
                }
                if (st2 == true)
                {
                    ViewState["mgrmapping"] = null; GV_empmgr.DataSource = (DataTable)ViewState["mgrmapping"];
                    GV_empmgr.DataBind();
                    LoadmagrMApping();
                    btnSubmit.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully')", true);
                }
            }
            catch (Exception ex) { }
        }

        protected void btnDesig_Click(object sender, EventArgs e)
        {
            try
            {
                //bool? stats = false;
                //string error = "";
                //ValidationChk("", Convert.ToDateTime(txtHolDate.Text.Trim()).ToString("yyyy-MM-dd"), 4, ref stats, ref error);
                //if (stats == true)
                //{
                using (DataTable Dt = ViewState["Desigmapping"] != null ? (DataTable)ViewState["Desigmapping"] : EmpMgrMapping())
                {
                    Dt.Rows.Add(ddldesigEmployee.SelectedValue, ddldesigEmployee.SelectedItem, ddldesig.SelectedValue, ddldesig.SelectedItem);
                    ViewState["Desigmapping"] = Dt;

                    btnDesigSubmt.Visible = Dt.Rows.Count > 0 ? true : false;
                }
                gv_desigmapp.DataSource = (DataTable)ViewState["Desigmapping"];
                gv_desigmapp.DataBind();
                ddldesigEmployee.SelectedIndex = -1;
                ddldesig.SelectedIndex = -1;
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('" + error + "')", true);
                //}
            }
            catch (Exception ex) { }
        }

        protected void btnDesigSubmt_Click(object sender, EventArgs e)
        {
            try
            {
                bool? st2 = false;
                configurationbl objBl = new configurationbl();
                configurationbo bo2 = new configurationbo();
                DataTable dt2 = new DataTable();
                using (dt2 = (DataTable)ViewState["Desigmapping"])
                {
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        bo2.Company_Code = Session["CompCode"].ToString();
                        bo2.EMPID = dt2.Rows[j]["Employee_ID"].ToString().Trim().ToLower();
                        bo2.desig = Convert.ToInt32(dt2.Rows[j]["MGR_ID"].ToString().Trim());
                        bo2.flag = 1;
                        objBl.Create_mapping_Designation(bo2, ref st2);
                    }
                }
                if (st2 == true)
                {
                    ViewState["Desigmapping"] = null;
                    gv_desigmapp.DataSource = (DataTable)ViewState["Desigmapping"];
                    gv_desigmapp.DataBind();
                    LoadDesigmappingdb();
                    btnDesigSubmt.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully')", true);
                }
            }
            catch (Exception ex) { }
        }

        protected void btnNewDesig_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewDesig.Text != "")
                {
                    bool? stats = false;
                    string error = "";
                    ValidationChk("", txtNewDesig.Text.Trim(), 1, 0, System.DateTime.Today, System.DateTime.Today, ref stats, ref error);
                    if (stats == true)
                    {
                        bool? st2 = false;
                        configurationbl objBl = new configurationbl();
                        configurationbo bo2 = new configurationbo();
                        bo2.Company_Code = Session["CompCode"].ToString();
                        bo2.desigTEXT = txtNewDesig.Text.Trim();
                        bo2.flag = 1;
                        objBl.Create_designation(bo2, ref st2);
                        if (st2 == true)
                        {
                            txtNewDesig.Text = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully')", true);
                            
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('" + error + "')", true);
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btnAddLeaveQuota_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLeaveType.SelectedValue == "1040")
                {
                    bool? stats = false;
                    string error = "";

                    ValidationChk("1", "", 5, 1040, System.DateTime.Now, System.DateTime.Now, ref stats, ref error);
                    if (stats == true)
                    {
                        using (DataTable Dt = ViewState["leavequota"] != null ? (DataTable)ViewState["leavequota"] : LeavQouta())
                        {
                            Dt.Rows.Add(ddlLeaveType.SelectedValue, ddlLeaveType.SelectedItem, System.DateTime.Now, DateTime.Parse("9999-01-01"), Convert.ToInt32(txtcompoffdays.Text.Trim()), 0, 1);
                            ViewState["leavequota"] = Dt;
                            btnUpdateQuota.Visible = Dt.Rows.Count > 0 ? true : false;
                        }
                        gvleavQuota.DataSource = (DataTable)ViewState["leavequota"];
                        gvleavQuota.DataBind();
                        txtcompoffdays.Text = "";
                        ddlLeaveType.SelectedIndex = -1;
                        chkLQCarryfrwd.Checked = false;

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('" + error + "')", true);
                    }
                }
                else
                {
                    DateTime frm = DateTime.Parse(txtfrmdate.Text);
                    DateTime to = DateTime.Parse(txttodate.Text);
                    int noOfDays = Convert.ToInt32((to.Date - frm.Date).TotalDays);
                    bool? stats = false;
                    string error = "";
                    string validity = "";
                    if (chkLQCarryfrwd.Checked == true)
                    {
                        validity = "1";
                    }
                    else
                    {
                        validity = "0";
                    }


                    ValidationChk(validity.ToString(), "", 5, Convert.ToInt32(ddlLeaveType.SelectedValue.ToString()), DateTime.Parse(txtfrmdate.Text.ToString()), DateTime.Parse(txttodate.Text.ToString()), ref stats, ref error);
                    if (stats == true)
                    {
                        using (DataTable Dt = ViewState["leavequota"] != null ? (DataTable)ViewState["leavequota"] : LeavQouta())
                        {
                            Dt.Rows.Add(ddlLeaveType.SelectedValue, ddlLeaveType.SelectedItem, txtfrmdate.Text, txttodate.Text, noOfDays.ToString().Trim(), txtNoleave.Text, chkLQCarryfrwd.Checked == true ? 1 : 0);
                            ViewState["leavequota"] = Dt;

                            btnUpdateQuota.Visible = Dt.Rows.Count > 0 ? true : false;
                        }
                        gvleavQuota.DataSource = (DataTable)ViewState["leavequota"];
                        gvleavQuota.DataBind();
                        //txtleavMonths.Text = "";
                        txtNoleave.Text = "";
                        ddlLeaveType.SelectedIndex = -1;
                        txtfrmdate.Text = "";
                        txttodate.Text = "";
                        chkLQCarryfrwd.Checked = false;

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('" + error + "')", true);
                    }
                }

            }
            catch (Exception ex) { }
        }

        public void Load_Leaves()
        {
            try
            {
                configurationcollectionbo objLst = configurationbl.Get_LeaveTypes(User.Identity.Name.Trim(), 1);
                ddlLeaveType.DataSource = objLst;
                ddlLeaveType.DataTextField = "leavTEXT";
                ddlLeaveType.DataValueField = "leav";
                ddlLeaveType.DataBind();
                ddlLeaveType.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
            }
            catch (Exception ex) { }
        }

        protected void btnUpdateQuota_Click(object sender, EventArgs e)
        {
            try
            {
                bool? st2 = false;
                configurationbl objBl = new configurationbl();
                configurationbo bo2 = new configurationbo();
                DataTable dt2 = new DataTable();
                using (dt2 = (DataTable)ViewState["leavequota"])
                {
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        bo2.Company_Code = Session["CompCode"].ToString();
                        bo2.leav = dt2.Rows[j]["LeaveType"].ToString().Trim().ToLower();
                        bo2.period = Convert.ToDecimal(dt2.Rows[j]["period"].ToString().Trim());
                        bo2.qu = Convert.ToDecimal(dt2.Rows[j]["qu"].ToString().Trim());
                        bo2.CRYFWRD = Convert.ToInt32(dt2.Rows[j]["CRYFWRD"].ToString().Trim());
                        bo2.LoginID = User.Identity.Name;
                        bo2.frmdate = DateTime.Parse(dt2.Rows[j]["Leavefrom"].ToString().Trim());
                        bo2.todate = DateTime.Parse(dt2.Rows[j]["Leaveto"].ToString().Trim());
                        bo2.flag = 1;
                        objBl.Create_leaveQuotas(bo2, ref st2);
                    }
                }
                if (st2 == true)
                {
                    btnUpdateQuota.Visible = false;
                    ViewState["leavequota"] = null;
                    gvleavQuota.DataSource = (DataTable)ViewState["leavequota"];
                    gvleavQuota.DataBind();
                    LoadcompLeavQuota();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully')", true);
                    gvleavQuota.Visible = false;
                }

            }
            catch (Exception ex) { }
        }

        protected DataTable LeavQouta()
        {
            DataTable dtExcelData1 = new DataTable();
            dtExcelData1.Columns.AddRange(new DataColumn[7]
                    { 
                     new DataColumn("LeaveType",typeof(string)),                     
                     new DataColumn("LeaveTypetxt",typeof(string)),
                     new DataColumn("Leavefrom",typeof(DateTime)),
                     new DataColumn("Leaveto",typeof(DateTime)),
                      new DataColumn("period",typeof(decimal)),
                       new DataColumn("qu",typeof(decimal)),
                       new DataColumn("CRYFWRD",typeof(int))
                     });
            return dtExcelData1;
        }

        protected DataTable HolidayCal()
        {
            DataTable dtExcelData1 = new DataTable();
            dtExcelData1.Columns.AddRange(new DataColumn[4]
                    { 
                     new DataColumn("Year",typeof(string)),
                     new DataColumn("Date",typeof(string)),
                      new DataColumn("Des",typeof(string)),
                       new DataColumn("type",typeof(string))
                     });
            return dtExcelData1;
        }

        protected void btnUpdateHoliday_Click(object sender, EventArgs e)
        {
            try
            {


                bool? st2 = false;
                configurationbl objBl = new configurationbl();
                configurationbo bo2 = new configurationbo();
                DataTable dt2 = new DataTable();
                using (dt2 = (DataTable)ViewState["HolidayCal"])
                {
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        bo2.Company_Code = Session["CompCode"].ToString();
                        bo2.year = dt2.Rows[j]["Year"].ToString().Trim().ToLower();
                        bo2.Date = Convert.ToDateTime(dt2.Rows[j]["Date"].ToString());
                        bo2.Descrip = dt2.Rows[j]["Des"].ToString().Trim();
                        bo2.TYPE = dt2.Rows[j]["type"].ToString().Trim();
                        bo2.flag = 1;
                        objBl.Create_HolidayCal(bo2, ref st2);
                    }
                }
                if (st2 == true)
                {
                    ViewState["HolidayCal"] = null;
                    gvHolidayCaln.DataSource = (DataTable)ViewState["HolidayCal"];
                    gvHolidayCaln.DataBind();
                    btnUpdateHoliday.Visible = false;
                    LoadHolidayCalendar();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully')", true);
                }
            }
            catch (Exception ex) { }
        }

        protected void btnAddtempholi_Click(object sender, EventArgs e)
        {
            try
            {
                bool? stats = false;
                string error = "";
                string restricted = "";
                if (CHK_holical.Checked == true)
                {
                    restricted = "1";
                }
                else
                {
                    restricted = "0";
                }
                ValidationChk("", Convert.ToDateTime(txtHolDate.Text.Trim()).ToString("yyyy-MM-dd"), 4, 0, System.DateTime.Today, System.DateTime.Today, ref stats, ref error);
                if (stats == true)
                {
                    using (DataTable Dt = ViewState["HolidayCal"] != null ? (DataTable)ViewState["HolidayCal"] : HolidayCal())
                    {
                        Dt.Rows.Add(DateTime.Now.Year.ToString(), txtHolDate.Text.Trim(), txtHolDescrip.Text.Trim(), restricted.ToString());
                        ViewState["HolidayCal"] = Dt;

                        btnUpdateHoliday.Visible = Dt.Rows.Count > 0 ? true : false;
                    }
                    gvHolidayCaln.DataSource = (DataTable)ViewState["HolidayCal"];
                    gvHolidayCaln.DataBind();
                    txtHolDate.Text = "";
                    txtHolDescrip.Text = "";
                    CHK_holical.Checked = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('" + error + "')", true);
                }
            }
            catch (Exception ex) { }
        }

        protected void btnQuotagenerate_Click(object sender, EventArgs e)
        {
            try
            {
                bool? st2 = false;
                configurationbl objBl = new configurationbl();
                configurationbo bo2 = new configurationbo();
                objBl.generate_leaveQuota(User.Identity.Name, 1, ref st2);
                if (st2 == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Generated Successfully')", true);
                }
            }
            catch (Exception ex) { }
        }

        protected void btnQuotaUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool? st2 = false;
                configurationbl objBl = new configurationbl();
                configurationbo bo2 = new configurationbo();
                objBl.Update_leaveQuota(User.Identity.Name, 1, ref st2);
                if (st2 == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated Successfully')", true);
                }
            }
            catch (Exception ex) { }
        }


        protected void gv_users_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = gv_users.Rows[rowIndex];
                LinkButton lnklock = (LinkButton)grdRow.FindControl("lnktemplock");
                switch (e.CommandName.ToUpper())
                {
                    case "LOCK":
                        ModalPopupExtender1.Show();
                        txt_exitdate.Text = "";

                        string EID = "";
                        string EMAIL = "";
                        string EMPNAME = "";

                        EID = gv_users.DataKeys[grdRow.RowIndex].Values["EMPID"].ToString();
                        lbl_eid.Text = EID.ToString();
                        Session["EID"] = EID.ToString();

                        EMPNAME = gv_users.DataKeys[grdRow.RowIndex].Values["NAME"].ToString();
                        lbl_ename.Text = EMPNAME.ToString();
                        Session["ENAME"] = EMPNAME.ToString();

                        EMAIL = gv_users.DataKeys[grdRow.RowIndex].Values["Company_MailID"].ToString();
                        lbl_email.Text = EMAIL.ToString();
                        Session["EMAIL"] = EMAIL.ToString();

                        txt_exitdate.Focus();
                        //CE_calendr.Focus();
                        break;

                    case "GENERATE":

                        string EMPID = "";
                        DateTime Password;
                        string mail = "";
                        Session["empid"] = "";
                        Session["mail"] = "";
                        EMPID = gv_users.DataKeys[grdRow.RowIndex]["EMPID"].ToString();
                        Session["empid"] = EMPID.ToString().Trim();
                        Password = Convert.ToDateTime(gv_users.DataKeys[grdRow.RowIndex]["PASSWORD"].ToString());
                        mail = gv_users.DataKeys[grdRow.RowIndex]["Company_MailID"].ToString();
                        Session["mail"] = mail.ToString().Trim();
                        string pass = Password.ToString("dd/MM/yyyy");
                        MembershipCreateStatus MuUserStatus = new MembershipCreateStatus();
                        MembershipUser MuUser = Membership.CreateUser(EMPID.ToString(), pass, mail.ToString().ToLower().Trim(), "a", "b", true, out MuUserStatus);
                        switch (MuUserStatus)
                        {
                            case MembershipCreateStatus.DuplicateEmail:
                                ObjLogError.LogError(sCreateUserLogPath, mail.ToString().ToLower().Trim() + " Email already exist.");
                                break;
                            case MembershipCreateStatus.DuplicateProviderUserKey:
                                break;
                            case MembershipCreateStatus.DuplicateUserName:
                                ObjLogError.LogError(sCreateUserLogPath, EMPID.ToString().Trim() + " User already exist.");
                                break;
                            case MembershipCreateStatus.InvalidAnswer:
                                break;
                            case MembershipCreateStatus.InvalidEmail:
                                ObjLogError.LogError(sCreateUserLogPath, mail.ToString().ToLower().Trim() + " Email is not valid.");
                                break;
                            case MembershipCreateStatus.InvalidPassword:
                                break;
                            case MembershipCreateStatus.InvalidProviderUserKey:
                                break;
                            case MembershipCreateStatus.InvalidQuestion:
                                break;
                            case MembershipCreateStatus.InvalidUserName:
                                ObjLogError.LogError(sCreateUserLogPath, EMPID.ToString().ToLower().Trim() + " UserName is not valid.");
                                break;
                            case MembershipCreateStatus.ProviderError:
                                break;
                            case MembershipCreateStatus.Success:

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Employee Login Creation is Successfull..')", true);
                                loadGrid();                               

                                //--------------------------- SENDING EMAIL NOTIFICATION - TO USERS ---------------------------------------
                                string[] MsgCC = { };
                                string Mailbody = string.Empty;
                                string eid = string.Empty;
                                string mid = string.Empty;
                                eid = Session["empid"].ToString().Trim();
                                mid = Session["mail"].ToString().Trim();
                                string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/CreateUser.html");
                                Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                                masterbl.SendMail(mid.ToString(), MsgCC, "Welcome to IEmppower - PayCompute, User login details ."
                                  , Mailbody.Replace("##EMPPERNR##", eid.ToString()).Replace("##PASSWORD##", pass.ToString().Trim()).Replace("##EMAILID##", mid.ToString()));


                                ObjLogError.LogError(sCreateUserLogPath, eid.ToString().ToLower().Trim() + " - " + mid.ToString().ToLower().Trim() + " - User created successfully.");
                                break;
                            case MembershipCreateStatus.UserRejected:
                                ObjLogError.LogError(sCreateUserLogPath, Session["empid"].ToString().ToLower().Trim() + " User rejected.");
                                break;
                            default:
                                ObjLogError.LogError(sCreateUserLogPath, Session["empid"].ToString().ToLower().Trim() + " - " + Session["mail"].ToString().ToLower().Trim() + " - Unknown Error.");
                                break;
                        }
                        loadGrid();

                        break;

                    case "TEMPLOCK":

                        string LOCKID1 = "";
                        LOCKID1 = gv_users.DataKeys[grdRow.RowIndex].Values["EMPID"].ToString();
                        string EMPNAME1 = "";
                        EMPNAME1 = gv_users.DataKeys[grdRow.RowIndex].Values["NAME"].ToString();
                       
                            bool? status1 = false;

                            configurationbl ObjBl2 = new configurationbl();

                            ObjBl2.tempolock_unlockUser(User.Identity.Name, LOCKID1, 1, ref status1);

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + EMPNAME1.ToString().Trim() + " Locked successfully')", true);

                           
                        loadGrid();
                        break;

                    case "TEMPUNLOCK":

                        string UNLOCKID = "";
                        UNLOCKID = gv_users.DataKeys[grdRow.RowIndex].Values["EMPID"].ToString();
                        string EMPNAME2 = "";
                        EMPNAME2 = gv_users.DataKeys[grdRow.RowIndex].Values["NAME"].ToString();
                       
                          
                            bool? status2 = false;

                            configurationbl ObjBl3 = new configurationbl();

                            ObjBl3.tempolock_unlockUser(User.Identity.Name, UNLOCKID, 1, ref status2);

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + EMPNAME2.ToString().Trim() + " Unlocked successfully')", true);
                        
                        loadGrid();
                        break;


                }
            }
            catch (Exception ex) { }
        }

        protected void btnWeekends_Click(object sender, EventArgs e)
        {
            try
            {
                bool? st2 = false;
                configurationbl objBl = new configurationbl();
                configurationbo bo2 = new configurationbo();
                objBl.setWeekend(User.Identity.Name, 0, 3);
                for (int i = 0; i < chkWeekends.Items.Count; i++)
                {
                    if (chkWeekends.Items[i].Selected)
                    {
                        objBl.setWeekend(User.Identity.Name, i + 1, 2);
                    }
                }

                objBl.Update_leaveQuota(User.Identity.Name, 1, ref st2);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully')", true);
                loadweekend();
            }
            catch (Exception ex) { }
        }

        protected void loadweekend()
        {
            try
            {
                configurationbo ObjBo = new configurationbo();
                configurationbl ObjBl = new configurationbl();
                configurationcollectionbo objoList = new configurationcollectionbo();

                foreach (var a in ObjBl.GetWeekend(User.Identity.Name, 0, 1))
                {
                    chkWeekends.Items[a.WK - 1].Selected = true;
                }
            }
            catch (Exception ex) { }
        }

        public void ValidationChk(string empID, string data, int? flg, int subty, DateTime frmdate, DateTime todate, ref bool? status, ref string error)
        {
            configurationdalDataContext a = new configurationdalDataContext();
            try
            {
                a.payc_validaton_check(Session["CompCode"].ToString(), empID, data, flg, subty, frmdate, todate, ref status, ref error);
                a.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        protected void loadCompanyDeatils()
        {
            configurationbo ObjBo = new configurationbo();
            configurationbl ObjBl = new configurationbl();
            configurationcollectionbo objoList = new configurationcollectionbo();
            objoList = ObjBl.LoadAllCompnydeatils(Session["CompCode"].ToString(), 2);
            if (objoList.Count > 0)
            {
                frmCompDetails.DataSource = objoList;
                frmCompDetails.DataBind();
                frmCompDetails.Visible = true;
                ViewState["ctype"] = objoList[0].Company_Type == null ? "0" : objoList[0].Company_Type.ToString();
                ViewState["cContry"] = objoList[0].Country == null ? "0" : objoList[0].Country.ToString();
                ViewState["cContrytxt"] = objoList[0].CountryTxt == null ? "" : objoList[0].CountryTxt.ToString();
                ViewState["cState"] = objoList[0].State == null ? "0" : objoList[0].State.ToString();


               // lblCCode.Text = objoList[0].Company_Code.ToString();
                lblCNAMe.Text = objoList[0].Company_Name.ToString();

                txt_Caddress.Text = objoList[0].Company_Address.ToString();
                txt_Ccontctno.Text = objoList[0].Company_ContactNum.ToString();
                txt_Cemail.Text = objoList[0].Company_MailID.ToString();
                txt_Cpincode.Text = objoList[0].Pincode.ToString();
                txtDist.Text = objoList[0].District.ToString();


                DDL_Ctype.SelectedValue = objoList[0].Company_Type == null ? "0" : objoList[0].Company_Type.ToString();
                DDL_Ccountry.SelectedValue = objoList[0].Country == null ? "0" : objoList[0].Country.ToString();
                loadStates(DDL_Ccountry.SelectedValue.ToString().Trim(), 1);

                DDL_Cstate.SelectedValue = objoList[0].State == null ? "0" : objoList[0].State.ToString();
            }
        }

        protected void frmCompDetails_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "EDITINFO":
                        pnlcntrls.Visible = true;
                        frmCompDetails.Visible = false;
                        //  loadCompanyDeatils();

                        break;
                    //case "CANCEL":
                    //    frmCompDetails.ChangeMode(FormViewMode.ReadOnly);
                    //    loadCompanyDeatils();
                    //    break;
                }
            }
            catch (Exception ex) { }
        }


        protected void loadStates(string cnty, int flg)
        {
            configurationcollectionbo State = configurationbl.Get_states(cnty.Trim(), flg);
            DDL_Cstate.DataSource = State;
            DDL_Cstate.DataTextField = "StateTxt";
            DDL_Cstate.DataValueField = "State";
            DDL_Cstate.DataBind();
            DDL_Cstate.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

        }

        protected void DDL_Ccountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //using (DropDownList DDL_Ccountry = (DropDownList)frmCompDetails.FindControl("DDL_Ccountry"))
            //{
            loadStates(DDL_Ccountry.SelectedValue.ToString().Trim(), 1);
            //}
        }

        protected void btn_Ccancel_Click(object sender, EventArgs e)
        {
            pnlcntrls.Visible = false;
            frmCompDetails.Visible = true;
        }

        protected void btn_Csave_Click(object sender, EventArgs e)
        {
            try
            {
                string _pathdb = "";
                string ap = Path.GetExtension(flLogo.FileName);
                if (ap != "")
                {
                    string DirPath = Server.MapPath(@"~/images/CmpLogo/");
                    DirectoryInfo DirInfo = new DirectoryInfo(DirPath);
                    FileInfo[] FlInfo = DirInfo.GetFiles(User.Identity.Name + ".*");
                    foreach (FileInfo Fl in FlInfo)
                    { Fl.Delete(); }

                    string _path = Server.MapPath("~/images/CmpLogo/" + Session["CompCode"].ToString().Trim() + Path.GetExtension(flLogo.FileName));
                    _pathdb = "~/images/CmpLogo/" + Session["CompCode"].ToString().Trim() + Path.GetExtension(flLogo.FileName);
                    flLogo.SaveAs(_path);
                }
                configurationbo objBo = new configurationbo();
                objBo.Company_Name = lblCNAMe.Text;
                objBo.Company_Code = Session["CompCode"].ToString();
                objBo.Company_Address = txt_Caddress.Text.Trim();
                objBo.Company_ContactNum = Convert.ToDecimal(txt_Ccontctno.Text.Trim());
                objBo.Company_MailID = txt_Cemail.Text.Trim();
                objBo.Company_Type = Convert.ToInt32(DDL_Ctype.SelectedValue.ToString().Trim());
                objBo.Country = DDL_Ccountry.SelectedValue.ToString().Trim();
                objBo.State = DDL_Cstate.SelectedValue.ToString().Trim();
                objBo.District = txtDist.Text.Trim();
                objBo.Pincode = Convert.ToDecimal(txt_Cpincode.Text.Trim());
                objBo.CREATEDBY = User.Identity.Name.Trim();
                objBo.EMPLOYEE_PATH = _pathdb;
                configurationbl objBL = new configurationbl();
                bool? status = false;
                objBL.CompanyCreate(objBo, 2, ref status);
                if (status == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated Successfully..')", true);
                    pnlcntrls.Visible = false;
                    frmCompDetails.Visible = true;
                }
            }
            catch (Exception ex) { }
        }

        protected void loadDDL()
        {
            configurationcollectionbo Company_Type = configurationbl.Get_ComapnyTypes(1);
            DDL_Ctype.DataSource = Company_Type;
            DDL_Ctype.DataTextField = "Company_Type_Txt";
            DDL_Ctype.DataValueField = "Company_Type";
            DDL_Ctype.DataBind();
            DDL_Ctype.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

            configurationcollectionbo Country = configurationbl.Get_Country(1);
            DDL_Ccountry.DataSource = Country;
            DDL_Ccountry.DataTextField = "CountryTxt";
            DDL_Ccountry.DataValueField = "Country";
            DDL_Ccountry.DataBind();
            DDL_Ccountry.Items.Insert(0, new ListItem(" - SELECT - ", "0"));

        }

        protected void gvmgrmappingdb_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "DELETE":
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = gvmgrmappingdb.Rows[rowIndex];
                string s = "";
                Deleterw(Convert.ToInt32(gvmgrmappingdb.DataKeys[grdRow.RowIndex].Values["ID"].ToString()), 2, 0, System.DateTime.Today, System.DateTime.Today);
                LoadmagrMApping();
                break;
                }

            }
            catch (Exception ex) { }
        }

        protected void gvCrtdesMappDb_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "DELETE":
                        int rowIndex = Convert.ToInt32(e.CommandArgument);
                        GridViewRow grdRow = gvCrtdesMappDb.Rows[rowIndex];

                        string s = "";
                        Deleterw(Convert.ToInt32(gvCrtdesMappDb.DataKeys[grdRow.RowIndex].Values["ID"].ToString()), 1, 0, System.DateTime.Today, System.DateTime.Today);
                        LoadDesigmappingdb();
                        break;
                }

            }
            catch (Exception ex) { }
        }

        protected void gvLeavcomquota_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = gvLeavcomquota.Rows[rowIndex];
                int subty = Convert.ToInt32(gvLeavcomquota.DataKeys[grdRow.RowIndex].Values["id1"].ToString());
                DateTime frm = DateTime.Parse(gvLeavcomquota.DataKeys[grdRow.RowIndex].Values["frmdate"].ToString());
                DateTime todate = DateTime.Parse(gvLeavcomquota.DataKeys[grdRow.RowIndex].Values["todate"].ToString());

                string s = "";
                Deleterw(Convert.ToInt32(gvLeavcomquota.DataKeys[grdRow.RowIndex].Values["ID"].ToString()), 3, subty, frm, todate);
                LoadcompLeavQuota();
            }
            catch (Exception ex) { }
        }

        protected void Grd_HolidayCalendar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = Grd_HolidayCalendar.Rows[rowIndex];

                string s = "";
                Deleterw(Convert.ToInt32(Grd_HolidayCalendar.DataKeys[grdRow.RowIndex].Values["ID"].ToString()), 4, 0, System.DateTime.Today, System.DateTime.Today);
                LoadHolidayCalendar();

            }
            catch (Exception ex) { }
        }

        public string Deleterw(int ID, int flg, int subty, DateTime frmdate, DateTime todate)
        {
            try
            {
                configurationbl objBL = new configurationbl();
                bool? status = false;
                objBL.DeleteRw(User.Identity.Name.Trim(), ID, "", flg, subty, frmdate, todate, ref status);
                if (status == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Deleted Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Error While Deleting')", true);
                }
                return status.ToString();

            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        protected void Grd_HolidayCalendar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvmgrmappingdb_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvCrtdesMappDb_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvLeavcomquota_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvmgrmappingdb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Label lbl = (Label)e.Row.FindControl("LBL_empid");
                    string ccode = Session["CompCode"].ToString();
                    string emplogin = e.Row.Cells[0].Text;//lbl.Text;
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    e.Row.Cells[0].Text = emplogin.Trim().ToUpper();

                    string emplogin1 = e.Row.Cells[2].Text;//lbl.Text;
                    int cnt1 = ccode.Length;
                    emplogin1 = emplogin1.Substring(cnt1);
                    e.Row.Cells[2].Text = emplogin1.Trim().ToUpper();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void gvCrtdesMappDb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Label lbl = (Label)e.Row.FindControl("LBL_empid");
                    string ccode = Session["CompCode"].ToString();
                    string emplogin = e.Row.Cells[0].Text;//lbl.Text;
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    e.Row.Cells[0].Text = emplogin.Trim().ToUpper();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnSearchEmpLeavQota_Click(object sender, EventArgs e)
        {
            try
            {
                leaverequestbo ObjLeaveRequestBo = new leaverequestbo();
                leaverequestbl ObjLeaveRequestBl = new leaverequestbl();
                leaverequestcollectionbo ObjLeaveReqLst = new leaverequestcollectionbo();

                ObjLeaveRequestBo.PERNR = ddlEmpCheckQuota.SelectedValue.Trim();
                ObjLeaveRequestBo.YEAR = Convert.ToInt32(txtYearLeaveQuotachk.Text.Trim());
                ObjLeaveReqLst = ObjLeaveRequestBl.Get_LeaveQuota(ObjLeaveRequestBo, Session["CompCode"].ToString());
                if (ObjLeaveReqLst.Count > 0)
                {
                    GV_LeaveQuota.DataSource = ObjLeaveReqLst;
                    GV_LeaveQuota.DataBind();
                    GV_LeaveQuota.Visible = true;
                    btn_lvreset.Visible = true;

                }
                else
                {
                    GV_LeaveQuota.DataSource = null;
                    GV_LeaveQuota.DataBind();
                    GV_LeaveQuota.Visible = true;
                    btn_lvreset.Visible = false ;
                }
            }
            catch (Exception ex) { }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            try
            {
                GV_exprtdesg.Visible = true;               
                configurationbl bl = new configurationbl();
                List<configurationbo> bolst = new List<configurationbo>();
                bolst = bl.Get_empmgrdesig_config(Session["CompCode"].ToString().ToLower().Trim(), "", 1);
                GV_exprtdesg.DataSource = bolst;
                GV_exprtdesg.DataBind();

                string date1 = DateTime.Now.ToString("MM/yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment; filename=" + " Employees_DesigMgr" + date1 + ".xls");
               
                configurationbl ObjLeaveRequestBl = new configurationbl();
                List<configurationbo> objoList = new List<configurationbo>();
                objoList = ObjLeaveRequestBl.Get_empmgrdesig_config(Session["CompCode"].ToString().ToLower().Trim(), "", 1);
                GV_exprtdesg.DataSource = objoList;
                GV_exprtdesg.DataBind();
                GV_exprtdesg.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                GV_exprtdesg.HeaderStyle.Font.Bold = true;
                GV_exprtdesg.GridLines = GridLines.Both;
                GV_exprtdesg.RenderControl(htmltextwrtter);
                GV_exprtdesg.DataBind();
                Response.Write(strwritter.ToString());
                Response.End();


            }
            catch (Exception ex) { }
        }

        protected void btnexprt_mgrmap_Click(object sender, EventArgs e)
        {
            try
            {
                string date1 = DateTime.Now.ToString("MM/yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment; filename=" + "Reporting_Managers" + date1 + ".xls");
                gvmgrmappingdb.GridLines = GridLines.Both;
                gvmgrmappingdb.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                gvmgrmappingdb.HeaderStyle.Font.Bold = true;

                configurationbl ObjLeaveRequestBl = new configurationbl();
                List<configurationbo> LeaveReqboList1 = new List<configurationbo>();
                LeaveReqboList1 = ObjLeaveRequestBl.Get_EmpManagerManpping(Session["CompCode"].ToString(), 1);               
                gvmgrmappingdb.DataSource = LeaveReqboList1; 
                gvmgrmappingdb.AllowPaging = false;
                gvmgrmappingdb.AllowCustomPaging = false;
                gvmgrmappingdb.Columns[4].Visible = false;
                gvmgrmappingdb.DataBind();

                gvmgrmappingdb.RenderControl(htmltextwrtter);
                gvmgrmappingdb.Columns[4].Visible = true;
                gvmgrmappingdb.AllowPaging = true;
                gvmgrmappingdb.DataBind();
                Response.Write(strwritter.ToString());
                Response.End();


            }
            catch (Exception ex) { }
        }

        protected void btn_exitemp_Click(object sender, EventArgs e)
        {
            try
            {
                lblmssg.Text = "";
                DateTime exitdate = DateTime.Parse(txt_exitdate.Text);
                if (exitdate <= DateTime.Now)
                {
                    lblmssg.Text = "";
                    bool? stat = false;
                    configurationbl ObjBl = new configurationbl();
                    ObjBl.Updatelock_unlockUser(User.Identity.Name, Session["EID"].ToString().Trim(), 1, Convert.ToDateTime(txt_exitdate.Text.ToString().Trim()), ref stat);
                    ViewState["mail_empexitdate"] = txt_exitdate.Text.ToString().Trim();

                    if (stat == true)
                    {
                        txt_exitdate.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('" + Session["ENAME"].ToString().Trim() + " Separated successfully')", true);
                        loadGrid();

                        string[] MsgCC = { };
                        string adminmail = "";
                        SPaycomputeDataContext objSPayDatacontext = new SPaycomputeDataContext();
                        foreach (var ENrow in objSPayDatacontext.payc_get_iemppayroll_admin())
                        {
                            adminmail = ENrow.Admin_Email;
                            //MsgCC = ENrow;

                        }
                        ViewState["mail_admin"] = adminmail.ToString().Trim();

                        string hrmail = "";
                        string hrname = "";
                        string hrid = "";
                        SPaycomputeDataContext objSPayDatacontexthr = new SPaycomputeDataContext();
                        foreach (var hrdetails in objSPayDatacontext.payc_get_HRdetails(Session["CompCode"].ToString().Trim()))
                        {
                            hrmail = hrdetails.HREMAIL;
                            hrname = hrdetails.HRNAME;
                            hrid = hrdetails.HRID;

                        }

                        ViewState["HRmail"] = hrmail.ToString().Trim();


                        //--------------------------- SENDING EMAIL NOTIFICATION - TO ADMIN ---------------------------------------
                        string ccode = Session["CompCode"].ToString();
                        string empid = Session["EID"].ToString().Trim();
                        int cnt = ccode.Length;
                        empid = empid.Substring(cnt).ToUpper();

                        string Mailbody = string.Empty;

                        string AddressInfoFilePath = Server.MapPath(@"~/EmailTemplates/Payrollempexit.html");
                        Mailbody = System.IO.File.ReadAllText(AddressInfoFilePath);
                        masterbl.SendMail(ViewState["mail_admin"].ToString(), MsgCC, "iEmpPower Paycompute Employee Exit Details - " + Session["Empname"].ToString().Trim() + "."
                          , Mailbody.Replace("##COMPANY##", Session["Empname"].ToString().Trim()).Replace("##EMPID##", empid.ToString()).Replace("##EMPNAME##", Session["ENAME"].ToString().Trim()).Replace("##EXITDATE##", ViewState["mail_empexitdate"].ToString().Trim()));


                        string Mailbodyexit = string.Empty;
                        string[] MailCC = { ViewState["HRmail"].ToString().Trim() };
                        string ExitInfoFilePath = Server.MapPath(@"~/EmailTemplates/Employee_Exit.html");
                        Mailbodyexit = System.IO.File.ReadAllText(ExitInfoFilePath);
                        masterbl.SendMail(Session["EMAIL"].ToString(), MailCC, "IEmpPower Paycompute - Notification !"
                          , Mailbodyexit.Replace("##COMPANY##", Session["Empname"].ToString().Trim()).Replace("##RESNAME##", Session["Empname"].ToString().Trim()).Replace("##EMPID##", empid.ToString()).Replace("##EMPNAME##", Session["ENAME"].ToString().Trim()).Replace("##EXITDATE##", ViewState["mail_empexitdate"].ToString().Trim()));



                        loadGrid();
                        txt_exitdate.Text = "";
                    }
                }
                else
                {                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Separation date should not be greater than current date')", true);
                    ModalPopupExtender1.Show();
                   //lblmssg.Text = "";
                   //lblmssg.Text = "Separation date should not be greater than current date";
                }
            }

            catch (Exception ex) { }
        }

        protected void HideTabs()
        {
            view1.Visible = false;
            view2.Visible = false;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = false;
            view6.Visible = false;

            Tab1.CssClass = "nav-link  p-2";
            Tab2.CssClass = "nav-link  p-2";
            Tab3.CssClass = "nav-link  p-2";
            //Tab4.CssClass = "nav-link  p-2";
            Tab5.CssClass = "nav-link  p-2";
            Tab6.CssClass = "nav-link  p-2";

        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = true;
            view2.Visible = false;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = false;
            view6.Visible = false;
            Tab1.CssClass = "nav-link active p-2";
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = false;
            view2.Visible = true;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = false;
            view6.Visible = false;
            Tab2.CssClass = "nav-link active p-2";
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = false;
            view2.Visible = false;
            view3.Visible = true;
            view4.Visible = false;
            view5.Visible = false;
            view6.Visible = false;
            Tab3.CssClass = "nav-link active p-2";
        }

        //protected void Tab4_Click(object sender, EventArgs e)
        //{
        //    HideTabs();
        //    view1.Visible = false;
        //    view2.Visible = false;
        //    view3.Visible = false;
        //    view4.Visible = true;
        //    view5.Visible = false;
        //    view6.Visible = false;
        //   // Tab4.CssClass = "nav-link active p-2";
        //}

        protected void Tab5_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = false;
            view2.Visible = false;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = true;
            view6.Visible = false;
            Tab5.CssClass = "nav-link active p-2";
        }

        protected void Tab6_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = false;
            view2.Visible = false;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = false;
            view6.Visible = true;
            Tab6.CssClass = "nav-link active p-2";
        }

        protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadleavecompoff();
            }
            catch (Exception ex)
            { }

        }

        public void loadleavecompoff()
        {
            try
            {
                if (ddlLeaveType.SelectedValue == "1040")
                {
                    generalleaves.Visible = false;
                    compoff.Visible = true;
                }
                else
                {
                    generalleaves.Visible = true;
                    compoff.Visible = false;
                }
            }
            catch (Exception ex)
            { }

        }

        protected void gvCrtdesMappDb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                configurationbo ObjBo = new configurationbo();
                configurationbl ObjBl = new configurationbl();
                configurationcollectionbo objoList = new configurationcollectionbo();
                objoList = ObjBl.Get_EmpDesignationManpping(User.Identity.Name, 1);
                if (objoList.Count > 0)
                {
                    gvCrtdesMappDb.DataSource = objoList;
                    gvCrtdesMappDb.PageIndex = e.NewPageIndex;
                    gvCrtdesMappDb.DataBind();
                    gvCrtdesMappDb.Visible = true;
                    btnexportdesig.Visible = gvCrtdesMappDb.Rows.Count > 0 ? true : false;
                }

                else
                {
                    gvCrtdesMappDb.DataSource = null;
                    gvCrtdesMappDb.DataBind();
                }
            }
            catch (Exception ex)
            {

            }

        }

       
        protected void gvmgrmappingdb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                configurationbl ObjLeaveRequestBl = new configurationbl();
                List<configurationbo> LeaveReqboList1 = new List<configurationbo>();

                LeaveReqboList1 = ObjLeaveRequestBl.Get_EmpManagerManpping(Session["CompCode"].ToString(), 1);

                if (LeaveReqboList1 == null || LeaveReqboList1.Count == 0)
                {
                    gvmgrmappingdb.Visible = false;
                    gvmgrmappingdb.DataSource = null;
                    gvmgrmappingdb.DataBind();
                    return;
                }
                else
                {
                    gvmgrmappingdb.Visible = true;
                    gvmgrmappingdb.DataSource = null;
                    gvmgrmappingdb.DataBind();
                    gvmgrmappingdb.DataSource = LeaveReqboList1;
                    gvmgrmappingdb.PageIndex = e.NewPageIndex;
                    gvmgrmappingdb.DataBind();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void Loadempconfig()
        {
            try
            {
                
                configurationbl ObjLeaveRequestBl = new configurationbl();
                List<configurationbo> LeaveReqboList1 = new List<configurationbo>();
                LeaveReqboList1 = ObjLeaveRequestBl.Get_empmgrdesig_config(Session["CompCode"].ToString().ToLower().Trim(),"",1);
               
                if (LeaveReqboList1 == null || LeaveReqboList1.Count == 0)
                {
                    GV_desmgrconfig.Visible = false;
                    GV_desmgrconfig.DataSource = null;
                    GV_desmgrconfig.DataBind();
                    return;
                }
                 
                else
                {
                    GV_desmgrconfig.Visible = true;
                    GV_desmgrconfig.DataSource = null;
                    GV_desmgrconfig.DataBind();
                    GV_desmgrconfig.DataSource = LeaveReqboList1;
                    GV_desmgrconfig.DataBind();
                    exitedit();
                }

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_desmgrconfig.Rows)
                {
                    for (int i = 0; i < GV_desmgrconfig.Rows.Count; i++)
                    {
                        Label lblmpRowNumber = (Label)GV_desmgrconfig.Rows[i].FindControl("lblmpRowNumber");
                        if (i == 0)
                        {
                            frow = lblmpRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == GV_desmgrconfig.Rows.Count - 1)
                        {
                            lrow = lblmpRowNumber.Text;
                        }
                    }
                }
                divmapcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + LeaveReqboList1.Count + " entries";
                divmapcnt.Visible = GV_desmgrconfig.Rows.Count > 0 ? true : false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void GV_desmgrconfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {

                    case "VIEW":
                        int indx = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvmain = GV_desmgrconfig.Rows[indx];
                        GridViewRow subrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        divpopupcomp.Visible = true;
                        //ModalPopupExtender modalPopupExtender1 = (ModalPopupExtender)subrow.FindControl("mp1");
                        //ModalPopupExtender modalPopupExtender1 = (ModalPopupExtender)GV_empdesigdetails.Rows[indx].FindControl("mp1");
                        modal1.Show();

                        string empid = GV_desmgrconfig.DataKeys[gvmain.RowIndex].Values["EMPID"].ToString();
                        
                        configurationbl ObjLeaveRequestBl = new configurationbl();
                        List<configurationbo> LeaveReqboList1 = new List<configurationbo>();
                        LeaveReqboList1 = ObjLeaveRequestBl.Get_empmgrdesig_config_full(Session["CompCode"].ToString().ToLower().Trim().ToLower().Trim(), empid.ToString().ToLower().Trim(), 1);
                        GV_empdesigdetails.Visible = true;
                        GV_empdesigdetails.DataSource = null;
                        GV_empdesigdetails.DataBind();
                        GV_empdesigdetails.DataSource = LeaveReqboList1;
                        GV_empdesigdetails.DataBind();


                         configurationbl bl = new configurationbl();
                        List<configurationbo> lst = new List<configurationbo>();
                        lst = bl.Get_mgrdesig_config_full(Session["CompCode"].ToString().ToLower().Trim().ToLower().Trim(), empid.ToString().ToLower().Trim(), 1);
                        GV_empmgrdetails.Visible = true;
                        GV_empmgrdetails.DataSource = null;
                        GV_empmgrdetails.DataBind();
                        GV_empmgrdetails.DataSource = lst;
                        GV_empmgrdetails.DataBind();
                        break;

                    case "EDITCN":
                    exitedit();

                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvRow = GV_desmgrconfig.Rows[rowIndex];
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    Label LB1 = (Label)row.FindControl("lbl_emdesig");

                    Label LB2 = (Label)row.FindControl("lbl_mgrname");

                    DropDownList DDL1 = (DropDownList)row.FindControl("DDL_designtn");

                    DropDownList DDL2 = (DropDownList)row.FindControl("DDL_mgrname");

                    LinkButton lkedit = (LinkButton)row.FindControl("LK_edt");

                    LinkButton lkupdte = (LinkButton)row.FindControl("LK_upd");

                    LinkButton lkcncl = (LinkButton)row.FindControl("LK_cncl");

                    if (LB1.Text != "" && LB2.Text != "")
                    {
                        DDL1.SelectedValue = GV_desmgrconfig.DataKeys[gvRow.RowIndex].Values["CRYFWRD"].ToString() == "0" ? "1" : GV_desmgrconfig.DataKeys[gvRow.RowIndex].Values["CRYFWRD"].ToString();

                        DDL2.SelectedValue = GV_desmgrconfig.DataKeys[gvRow.RowIndex].Values["deptid"].ToString();// == "0" ? GV_desmgrconfig.DataKeys[gvRow.RowIndex].Values["EMPID"].ToString() : GV_desmgrconfig.DataKeys[gvRow.RowIndex].Values["deptid"].ToString();
                    }
                    else
                    {
                        DDL1.SelectedValue = GV_desmgrconfig.DataKeys[gvRow.RowIndex].Values["CRYFWRD"].ToString() == "0" ? "7" : GV_desmgrconfig.DataKeys[gvRow.RowIndex].Values["CRYFWRD"].ToString();
                    }


                    if (GV_desmgrconfig.DataKeys[gvRow.RowIndex].Values["CRYFWRD"].ToString() == "1" || GV_desmgrconfig.DataKeys[gvRow.RowIndex].Values["CRYFWRD"].ToString() == "2")
                    {
                        DDL1.Visible = true;
                        DDL2.Visible = false;

                        LB1.Visible = false;
                        LB2.Visible = true;

                        lkedit.Visible = false;
                        lkupdte.Visible = true;
                        lkcncl.Visible = true;
                    }
                    else
                    {
                        DDL1.Visible = true;
                        DDL2.Visible = true;

                        LB1.Visible = false;
                        LB2.Visible = false;

                        lkedit.Visible = false;
                        lkupdte.Visible = true;
                        lkcncl.Visible = true;
                    }
                    
                    break;

                    case "UPDCON":
                       
                        int updateindex = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvupdate = GV_desmgrconfig.Rows[updateindex];
                        GridViewRow row1 = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        string EmpID = "";
                        EmpID = GV_desmgrconfig.DataKeys[gvupdate.RowIndex].Values["EMPID"].ToString();                       

                        DropDownList DDLD2 = (DropDownList)row1.FindControl("DDL_designtn");

                        DropDownList DDLM2 = (DropDownList)row1.FindControl("DDL_mgrname");

                        string mgrid = "";
                        if (DDLD2.SelectedValue == "1" || DDLD2.SelectedValue == "2")
                        {
                            mgrid = "";
                        }
                        else
                        {
                             mgrid = DDLM2.SelectedValue.ToString().ToLower().Trim();
                        }

                        bool? designation = false;

                        bool? mgr = false;

                        configurationbo ObjBo = new configurationbo();
                        configurationbl ObjBl = new configurationbl();
                        ObjBo.Company_Code=User.Identity.Name.ToLower().Trim();
                        ObjBo.EMPID=EmpID.ToString().Trim();
                        ObjBo.EMAIL_ID = mgrid.ToString().Trim();
                        ObjBo.ID = Convert.ToInt32(DDLD2.SelectedValue);
                        ObjBo.flag=1;
                        ObjBl.create_mgr_desig(ObjBo, ref designation, ref mgr);

                        if (designation == true && mgr == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Employee Designation and Reporting Manager Updated Successfully');", true);
                        }
                        else if (designation == false && mgr == false)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Records Already Exists');", true);
                        }
                        else if (designation == true && mgr == false)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Employee Designation Updated Successfully, Reporting Manager Already Exists');", true);
                        }
                        else if (designation == false && mgr == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Employee Designation Already Exists, Reporting Manager  Updated Successfully');", true);
                        }

                         exitedit();
                        Loadempconfig();
                        break;

                    case "CANCONF":
                        exitedit();
                        Loadempconfig();
                        break;
                }
            }

            catch (Exception Ex)
            {

            }

        }

        public void exitedit()
        {
            try
            {
                foreach (GridViewRow row in GV_desmgrconfig.Rows)
                {

                    Label LBDESIG = (Label)row.FindControl("lbl_emdesig");

                    Label LBMGRNAME = (Label)row.FindControl("lbl_mgrname");

                    DropDownList DDLDESIG = (DropDownList)row.FindControl("DDL_designtn");

                    DropDownList DDLMGR = (DropDownList)row.FindControl("DDL_mgrname");

                    LinkButton edt = (LinkButton)row.FindControl("LK_edt");

                    LinkButton udt = (LinkButton)row.FindControl("LK_upd");

                    LinkButton cnl = (LinkButton)row.FindControl("LK_cncl");

                    DDLDESIG.Visible = false;
                    DDLMGR.Visible = false;

                    LBDESIG.Visible = true;
                    LBMGRNAME.Visible = true;

                    edt.Visible = true;
                    udt.Visible = false;
                    cnl.Visible = false;


                }
            }

            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }

        }

        protected void GV_desmgrconfig_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlDesig = (e.Row.FindControl("DDL_designtn") as DropDownList);
                    configurationcollectionbo objLst = configurationbl.Get_Designationa(User.Identity.Name.Trim(), 1);
                    ddlDesig.DataSource = objLst;
                    ddlDesig.DataTextField = "desigTEXT";
                    ddlDesig.DataValueField = "desig";
                    ddlDesig.DataBind();

                   
                       DropDownList ddlMgr  = (e.Row.FindControl("DDL_mgrname") as DropDownList);
                       configurationcollectionbo objLst1 = configurationbl.Get_Employees(User.Identity.Name.Trim(), 1);
                        ddlMgr.DataSource = objLst1;
                        ddlMgr.DataTextField = "NAME";
                        ddlMgr.DataValueField = "EMPID";
                        ddlMgr.DataBind();


                        string ccode = Session["CompCode"].ToString();
                        string emplogin = e.Row.Cells[1].Text;
                        int cnt = ccode.Length;
                        emplogin = emplogin.Substring(cnt);
                        e.Row.Cells[1].Text = emplogin.Trim().ToUpper();

                        if (e.Row.Cells[4].Text != "&nbsp;")
                        {
                            string ccode1 = Session["CompCode"].ToString();
                            string emplogin1 = e.Row.Cells[4].Text;
                            int cnt1 = ccode1.Length;
                            emplogin1 = emplogin1.Substring(cnt1);
                            e.Row.Cells[4].Text = emplogin1.Trim().ToUpper();
                        }
                        
                }
            }
            catch (Exception ex)
            { }
        }

        protected void DDL_designtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                 foreach (GridViewRow row in GV_desmgrconfig.Rows)
                {
                    DropDownList desn = (DropDownList)row.FindControl("DDL_designtn");

                    DropDownList mgrmap = (DropDownList)row.FindControl("DDL_mgrname");

                    if (desn.SelectedValue == "1" || desn.SelectedValue == "2")
                    {
                        mgrmap.Visible = false;
                    }
                    else
                    {
                        mgrmap.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        protected void gv_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                configurationbo ObjBo = new configurationbo();
                configurationbl ObjBl = new configurationbl();
                configurationcollectionbo objoList = new configurationcollectionbo();
                objoList = ObjBl.Get_USersLock(User.Identity.Name,"", 1);
                gv_users.DataSource = objoList;
                gv_users.PageIndex = e.NewPageIndex;
                gv_users.DataBind();
                gv_users.Visible = true;

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in gv_users.Rows)
                {
                    for (int i = 0; i < gv_users.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)gv_users.Rows[i].FindControl("lblRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == gv_users.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divgencnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objoList.Count + " entries";
                divgencnt.Visible = gv_users.Rows.Count > 0 ? true : false;

            }
            catch (Exception ex)
            { }
        }

        protected void DDL_srchemp_genlogby_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_srchemp_genlogby.SelectedValue == "0")
                {
                    DDL_empsrchgenlog.DataSource = null;
                    DDL_empsrchgenlog.DataBind();
                    DDL_empsrchgenlog.Visible = false;
                    loadGrid();
                }

                else if (DDL_srchemp_genlogby.SelectedValue == "1")
                {
                    DDL_empsrchgenlog.Visible = true;
                    btn_srchlog_reset.Visible = true;

                    configurationcollectionbo objLst = configurationbl.Get_Employees(User.Identity.Name.Trim(), 1);
                    DDL_empsrchgenlog.DataSource = objLst;
                    DDL_empsrchgenlog.DataTextField = "NAME";
                    DDL_empsrchgenlog.DataValueField = "EMPID";
                    DDL_empsrchgenlog.DataBind();
                    DDL_empsrchgenlog.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
                }
                else if (DDL_srchemp_genlogby.SelectedValue == "2")
                {
                    DDL_empsrchgenlog.Visible = true;
                    btn_srchlog_reset.Visible = true;
                    configurationcollectionbo objLst = configurationbl.Get_Employees(User.Identity.Name.Trim(), 2);
                    DDL_empsrchgenlog.DataSource = objLst;
                    DDL_empsrchgenlog.DataTextField = "NAME";
                    DDL_empsrchgenlog.DataValueField = "EMPID";
                    DDL_empsrchgenlog.DataBind();
                    DDL_empsrchgenlog.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
                }

            }
            catch (Exception ex)
            { }
        }

        protected void GV_desmgrconfig_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                
                    if (DDL_configsrch.SelectedValue == "0")
                    {
                        configurationbl ObjLeaveRequestBl4 = new configurationbl();
                        List<configurationbo> LeaveReqboList14 = new List<configurationbo>();
                        LeaveReqboList14 = ObjLeaveRequestBl4.Get_empmgrdesig_config(Session["CompCode"].ToString().ToLower().Trim(), "", 1);

                        GV_desmgrconfig.Visible = true;
                        GV_desmgrconfig.DataSource = null;
                        GV_desmgrconfig.DataBind();
                        GV_desmgrconfig.DataSource = LeaveReqboList14;
                        GV_desmgrconfig.PageIndex = e.NewPageIndex;
                        GV_desmgrconfig.DataBind();
                        exitedit();


                        string frow = "", lrow = "";  ////Row count

                        foreach (GridViewRow row in GV_desmgrconfig.Rows)
                        {
                            for (int i = 0; i < GV_desmgrconfig.Rows.Count; i++)
                            {
                                Label lblmpRowNumber = (Label)GV_desmgrconfig.Rows[i].FindControl("lblmpRowNumber");
                                if (i == 0)
                                {
                                    frow = lblmpRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                                }
                                if (i == GV_desmgrconfig.Rows.Count - 1)
                                {
                                    lrow = lblmpRowNumber.Text;
                                }
                            }
                        }
                        divmapcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + LeaveReqboList14.Count + " entries";
                        divmapcnt.Visible = GV_desmgrconfig.Rows.Count > 0 ? true : false;

                    }

                    else if (DDL_configsrch.SelectedValue == "1")
                    {
                        DDL_srchtype.Visible = true;
                        btn_srchdesig_reset.Visible = true;

                        configurationbl ObjLeaveRequestBl = new configurationbl();
                        List<configurationbo> LeaveReqboList1 = new List<configurationbo>();
                        LeaveReqboList1 = ObjLeaveRequestBl.Get_empmgrdesig_config(Session["CompCode"].ToString().ToLower().Trim(), DDL_srchtype.SelectedValue.ToString().Trim(), 2);

                        if (LeaveReqboList1 == null || LeaveReqboList1.Count == 0)
                        {

                            GV_desmgrconfig.DataSource = null;
                            GV_desmgrconfig.DataBind();
                            return;
                        }

                        else
                        {
                            GV_desmgrconfig.Visible = true;
                            GV_desmgrconfig.DataSource = null;
                            GV_desmgrconfig.DataBind();
                            GV_desmgrconfig.DataSource = LeaveReqboList1;
                            GV_desmgrconfig.PageIndex = e.NewPageIndex;
                            GV_desmgrconfig.DataBind();
                            exitedit();

                        }
                            string frow = "", lrow = "";  ////Row count

                            foreach (GridViewRow row in GV_desmgrconfig.Rows)
                            {
                                for (int i = 0; i < GV_desmgrconfig.Rows.Count; i++)
                                {
                                    Label lblmpRowNumber = (Label)GV_desmgrconfig.Rows[i].FindControl("lblmpRowNumber");
                                    if (i == 0)
                                    {
                                        frow = lblmpRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                                    }
                                    if (i == GV_desmgrconfig.Rows.Count - 1)
                                    {
                                        lrow = lblmpRowNumber.Text;
                                    }
                                }
                            }
                            divmapcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + LeaveReqboList1.Count + " entries";
                            divmapcnt.Visible = GV_desmgrconfig.Rows.Count > 0 ? true : false;
                        
                    }
                    else if (DDL_configsrch.SelectedValue == "2")
                    {
                        DDL_srchtype.Visible = true;
                        btn_srchdesig_reset.Visible = true;

                        configurationbl bl = new configurationbl();
                        List<configurationbo> lst = new List<configurationbo>();
                        lst = bl.Get_empmgrdesig_config(Session["CompCode"].ToString().ToLower().Trim(), DDL_srchtype.SelectedValue.ToString().Trim(), 3);

                        if (lst == null || lst.Count == 0)
                        {
                            GV_desmgrconfig.Visible = false;
                            GV_desmgrconfig.DataSource = null;
                            GV_desmgrconfig.DataBind();
                            return;
                        }

                        else
                        {
                            GV_desmgrconfig.Visible = true;
                            GV_desmgrconfig.DataSource = null;
                            GV_desmgrconfig.DataBind();
                            GV_desmgrconfig.DataSource = lst;
                            GV_desmgrconfig.PageIndex = e.NewPageIndex;
                            GV_desmgrconfig.DataBind();
                            exitedit();

                        }
                            string frow = "", lrow = "";  ////Row count

                            foreach (GridViewRow row in GV_desmgrconfig.Rows)
                            {
                                for (int i = 0; i < GV_desmgrconfig.Rows.Count; i++)
                                {
                                    Label lblmpRowNumber = (Label)GV_desmgrconfig.Rows[i].FindControl("lblmpRowNumber");
                                    if (i == 0)
                                    {
                                        frow = lblmpRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                                    }
                                    if (i == GV_desmgrconfig.Rows.Count - 1)
                                    {
                                        lrow = lblmpRowNumber.Text;
                                    }
                                }
                            }
                            divmapcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + lst.Count + " entries";
                            divmapcnt.Visible = GV_desmgrconfig.Rows.Count > 0 ? true : false;
                        
                    }
                    



            }
            catch (Exception ex)
            { }
        }

        protected void DDL_configsrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_configsrch.SelectedValue == "0")
                {
                    DDL_srchtype.DataSource = null;
                    DDL_srchtype.DataBind();
                    DDL_srchtype.Visible = false;
                    btn_srchdesig_reset.Visible = false;
                    
                }

                else if (DDL_configsrch.SelectedValue == "1")
                {
                    DDL_srchtype.Visible = true;
                    btn_srchdesig_reset.Visible = true;

                    configurationcollectionbo objLst = configurationbl.Get_Employees(User.Identity.Name.Trim(), 1);
                    DDL_srchtype.DataSource = objLst;
                    DDL_srchtype.DataTextField = "NAME";
                    DDL_srchtype.DataValueField = "EMPID";
                    DDL_srchtype.DataBind();
                    DDL_srchtype.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
                }
                else if (DDL_configsrch.SelectedValue == "2")
                {
                    DDL_srchtype.Visible = true;
                    btn_srchdesig_reset.Visible = true;

                    configurationcollectionbo objLst = configurationbl.Emp_srch(User.Identity.Name.Trim(), 3);
                    DDL_srchtype.DataSource = objLst;
                    DDL_srchtype.DataTextField = "NAME";
                    DDL_srchtype.DataValueField = "EMPID";
                    DDL_srchtype.DataBind();
                    DDL_srchtype.Items.Insert(0, new ListItem(" - SELECT - ", "0"));
                }
            }
            catch (Exception ex)
            { }
        }

        protected void DDL_empsrchgenlog_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_srchemp_genlogby.SelectedValue == "0")
                {
                    DDL_empsrchgenlog.DataSource = null;
                    DDL_empsrchgenlog.DataBind();
                    DDL_empsrchgenlog.Visible = false;
                    btn_srchlog_reset.Visible = false;
                }

                else if (DDL_srchemp_genlogby.SelectedValue == "1")
                {
                    DDL_empsrchgenlog.Visible = true;
                    btn_srchlog_reset.Visible = true;

                    configurationbo ObjBo = new configurationbo();
                    configurationbl ObjBl = new configurationbl();
                    configurationcollectionbo objoList = new configurationcollectionbo();
                    objoList = ObjBl.Get_USersLock(User.Identity.Name, DDL_empsrchgenlog.SelectedValue.ToString().Trim(), 2);
                    if (objoList.Count > 0)
                    {
                        gv_users.DataSource = objoList;
                        gv_users.DataBind();
                        gv_users.Visible = true;
                    }

                    else
                    {
                        gv_users.DataSource = null;
                        gv_users.DataBind();
                    }
                    string frow = "", lrow = "";  ////Row count
                    foreach (GridViewRow row in gv_users.Rows)
                    {
                        for (int i = 0; i < gv_users.Rows.Count; i++)
                        {
                            Label lblRowNumber = (Label)gv_users.Rows[i].FindControl("lblRowNumber");
                            if (i == 0)
                            {
                                frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == gv_users.Rows.Count - 1)
                            {
                                lrow = lblRowNumber.Text;
                            }
                        }
                    }
                    divgencnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objoList.Count + " entries";
                    divgencnt.Visible = gv_users.Rows.Count > 0 ? true : false;
                }
                else if (DDL_srchemp_genlogby.SelectedValue == "2")
                {
                    DDL_empsrchgenlog.Visible = true;
                    btn_srchlog_reset.Visible = true;
                    configurationbo bo = new configurationbo();
                    configurationbl bl = new configurationbl();
                    configurationcollectionbo lst = new configurationcollectionbo();
                    lst = bl.Get_USersLock(User.Identity.Name, DDL_empsrchgenlog.SelectedValue.ToString().Trim(), 3);
                    if (lst.Count > 0)
                    {
                        gv_users.DataSource = lst;
                        gv_users.DataBind();
                        gv_users.Visible = true;
                    }

                    else
                    {
                        gv_users.DataSource = null;
                        gv_users.DataBind();
                    }

                    string frow = "", lrow = "";  ////Row count
                    foreach (GridViewRow row in gv_users.Rows)
                    {
                        for (int i = 0; i < gv_users.Rows.Count; i++)
                        {
                            Label lblRowNumber = (Label)gv_users.Rows[i].FindControl("lblRowNumber");
                            if (i == 0)
                            {
                                frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == gv_users.Rows.Count - 1)
                            {
                                lrow = lblRowNumber.Text;
                            }
                        }
                    }
                    divgencnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + lst.Count + " entries";
                    divgencnt.Visible = gv_users.Rows.Count > 0 ? true : false;
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btn_srchlog_reset_Click(object sender, EventArgs e)
        {
            try
            {
                loadGrid();
                DDL_empsrchgenlog.Visible = false;
                btn_srchlog_reset.Visible=false;
            }
            catch (Exception ex)
            { }
        }

        protected void DDL_srchtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_configsrch.SelectedValue == "0")
                {
                    DDL_srchtype.DataSource = null;
                    DDL_srchtype.DataBind();
                    DDL_srchtype.Visible = false;
                    btn_srchdesig_reset.Visible = false;
                    
                }

                else if (DDL_configsrch.SelectedValue == "1")
                {
                    DDL_srchtype.Visible = true;
                    btn_srchdesig_reset.Visible = true;

                    configurationbl ObjLeaveRequestBl = new configurationbl();
                    List<configurationbo> LeaveReqboList1 = new List<configurationbo>();
                    LeaveReqboList1 = ObjLeaveRequestBl.Get_empmgrdesig_config(Session["CompCode"].ToString().ToLower().Trim(), DDL_srchtype.SelectedValue.ToString().Trim(), 2);

                    if (LeaveReqboList1 == null || LeaveReqboList1.Count == 0)
                    {
                        
                        GV_desmgrconfig.DataSource = null;
                        GV_desmgrconfig.DataBind();
                        return;
                    }

                    else
                    {
                        GV_desmgrconfig.Visible = true;
                        GV_desmgrconfig.DataSource = null;
                        GV_desmgrconfig.DataBind();
                        GV_desmgrconfig.DataSource = LeaveReqboList1;
                        GV_desmgrconfig.DataBind();
                        exitedit();
                    }


                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_desmgrconfig.Rows)
                    {
                        for (int i = 0; i < GV_desmgrconfig.Rows.Count; i++)
                        {
                            Label lblmpRowNumber = (Label)GV_desmgrconfig.Rows[i].FindControl("lblmpRowNumber");
                            if (i == 0)
                            {
                                frow = lblmpRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_desmgrconfig.Rows.Count - 1)
                            {
                                lrow = lblmpRowNumber.Text;
                            }
                        }
                    }
                    divmapcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + LeaveReqboList1.Count + " entries";
                    divmapcnt.Visible = GV_desmgrconfig.Rows.Count > 0 ? true : false;
                }
                else if (DDL_configsrch.SelectedValue == "2")
                {
                    DDL_srchtype.Visible = true;
                    btn_srchdesig_reset.Visible = true;

                    configurationbl bl = new configurationbl();
                    List<configurationbo> lst = new List<configurationbo>();
                    lst = bl.Get_empmgrdesig_config(Session["CompCode"].ToString().ToLower().Trim(), DDL_srchtype.SelectedValue.ToString().Trim(), 3);

                    if (lst == null || lst.Count == 0)
                    {
                        GV_desmgrconfig.Visible = false;
                        GV_desmgrconfig.DataSource = null;
                        GV_desmgrconfig.DataBind();
                        return;
                    }

                    else
                    {
                        GV_desmgrconfig.Visible = true;
                        GV_desmgrconfig.DataSource = null;
                        GV_desmgrconfig.DataBind();
                        GV_desmgrconfig.DataSource = lst;
                        GV_desmgrconfig.DataBind();
                        exitedit();
                    }

                    string frow = "", lrow = "";  ////Row count

                    foreach (GridViewRow row in GV_desmgrconfig.Rows)
                    {
                        for (int i = 0; i < GV_desmgrconfig.Rows.Count; i++)
                        {
                            Label lblmpRowNumber = (Label)GV_desmgrconfig.Rows[i].FindControl("lblmpRowNumber");
                            if (i == 0)
                            {
                                frow = lblmpRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                            }
                            if (i == GV_desmgrconfig.Rows.Count - 1)
                            {
                                lrow = lblmpRowNumber.Text;
                            }
                        }
                    }
                    divmapcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + lst.Count + " entries";
                    divmapcnt.Visible = GV_desmgrconfig.Rows.Count > 0 ? true : false;
                }
            }
            

            catch (Exception ex){}
        }

        protected void btn_srchdesig_reset_Click(object sender, EventArgs e)
        {
            try
                {
                    Loadempconfig();
                    DDL_srchtype.Visible = false;
                    btn_srchdesig_reset.Visible = false;
                }
        
           catch (Exception ex){}
        }

        protected void GV_empdesigdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string ccode1 = Session["CompCode"].ToString();
                    string emplogin1 = e.Row.Cells[1].Text;
                    int cnt1 = ccode1.Length;
                    emplogin1 = emplogin1.Substring(cnt1);
                    e.Row.Cells[1].Text = emplogin1.Trim().ToUpper();
                }
            }
            catch (Exception ex) { }
        }

        protected void GV_empmgrdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string cc = Session["CompCode"].ToString();
                    string eid = e.Row.Cells[1].Text;
                    int cnt1 = cc.Length;
                    eid = eid.Substring(cnt1);
                    e.Row.Cells[1].Text = eid.Trim().ToUpper();


                    string cc1 = Session["CompCode"].ToString();
                    string eid1 = e.Row.Cells[3].Text;
                    int cnt2 = cc.Length;
                    eid1 = eid1.Substring(cnt2);
                    e.Row.Cells[3].Text = eid1.Trim().ToUpper();
                }
            }
            catch (Exception ex) { }
        }

        protected void btn_expallmgrdesg_Click(object sender, EventArgs e)
        {
            try
            {
                string date1 = DateTime.Now.ToString("MM/yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + "Employee_DesigMgr" + date1 + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    GV_empdesigdetails.GridLines = GridLines.Both;
                    GV_empdesigdetails.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    GV_empmgrdetails.GridLines = GridLines.Both;
                    GV_empmgrdetails.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    GV_empmgrdetails.HeaderStyle.Font.Bold = true;
                    GV_empdesigdetails.HeaderStyle.Font.Bold = true;
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    pnl_empdesidmgr.RenderControl(hw);                   
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_exprtdesg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string ccode = Session["CompCode"].ToString();
                    string emplogin = e.Row.Cells[1].Text;
                    int cnt = ccode.Length;
                    emplogin = emplogin.Substring(cnt);
                    e.Row.Cells[1].Text = emplogin.Trim().ToUpper();

                    if (e.Row.Cells[4].Text != "&nbsp;")
                    {
                        string ccode1 = Session["CompCode"].ToString();
                        string emplogin1 = e.Row.Cells[4].Text;
                        int cnt1 = ccode1.Length;
                        emplogin1 = emplogin1.Substring(cnt1);
                        e.Row.Cells[4].Text = emplogin1.Trim().ToUpper();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_lvreset_Click(object sender, EventArgs e)
        {
            try
            {
                ddlEmpCheckQuota.SelectedIndex = -1;
                GV_LeaveQuota.DataSource = null;
                GV_LeaveQuota.DataBind();
                GV_LeaveQuota.Visible = false;
                btn_lvreset.Visible = false;
                txtfrmdate.Focus();
            }

            catch (Exception ex)
            {

            }
        }

       


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerMaster;
using iEmpPowerMaster_Load;
using System.Data;
using System.Threading;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Configuration;

namespace iEmpPower.UI
{
    public partial class Default : System.Web.UI.Page
    {
        bool bSortedOrder = false;
        protected MembershipUser memUser;
        public int rselectedindex = -1;
        string strApprover = string.Empty;
        string strApprover_mail = string.Empty;
        string strRequesterMail = string.Empty;
        int? recCnt = 0;
        protected int CompleatedPageIndex = 1;
        protected int PagerSz = 10;
        protected int PendingPageIndex = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Session["CompCode"] = "itch";

            if (User.Identity.IsAuthenticated == false)
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            
           

            if (!IsPostBack)
            {
                
                LoadEmployeeDetails();               
                if (User.Identity.IsAuthenticated == false)
                {
                    Server.Transfer("~/Account/Login.aspx");
                }
                memUser = Membership.GetUser();
                configurationbl objBl = new configurationbl();
                configurationcollectionbo objLst = objBl.Load_EmployeePhotoDetails(memUser.ToString());
                foreach (configurationbo objBo in objLst)
                {
                    Session.Add("CompCode", objBo.Company_Code.Trim() == "" ? memUser.ToString() : objBo.Company_Code.Trim());
                }

                if (objLst.Count == 0)
                {
                    Session.Add("CompCode", User.Identity.Name);
                    Session["CompLogin"] = "Admin";
                    dvUser.Visible = false;
                    Ul1.Visible = false;
                    dvApprover.Visible = false;
                    pending.Visible = false;
                    cbox.Visible = false;
                    plTile1.Visible = false;
                    pnlTileEmpCols.Visible = false;
                }
                cbUser1.Checked = true;
                HFmethdLoader.Value = "1";
                HFsortId.Value = "1";
                BindYearDropdown();
                LoadGridDetails();
                LoadSelDropDown();
                Session["_MainSearchValue"] = "";
            }
            plTile1.Controls.Clear();
            pnlTileEmpCols.Controls.Clear();
            divTileApprv.Controls.Clear();
            divTileCollApprv.Controls.Clear();
           // GetHRPERNRS();
            loadTiledetails();
           
            msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            objPIDashBoardBo.PERNR = User.Identity.Name;
            objPIDashBoardBo.COMMENTS = Session["CompCode"].ToString().Trim().ToLower();
            msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_Manager(objPIDashBoardBo);

            msassignedtomebo bo1 = new msassignedtomebo();
            msassignedtomebl bl1 = new msassignedtomebl();
            bo1.PERNR = User.Identity.Name;
            bo1.COMMENTS = Session["CompCode"].ToString().Trim().ToLower();
            msassignedtomecollectionbo objPIDashBoardLsttyp = bl1.Get_Manager_HR(bo1);

            togglemgr.Visible = objPIDashBoardLsttyp.Count > 0 ? true : false;
            
                //company
            if (Session["CompLogin"].ToString() == "1")
            {
                dvUser.Visible = true;
                Ul1.Visible = false;
                dvApprover.Visible = false;
                pending.Visible = false;
                cbox.Visible = false;
                plTile1.Visible = true;
                pnlTileEmpCols.Visible = true;

            }
               
        }

        //---- MainSearch set Session
        [System.Web.Services.WebMethod]
        public static string SetSession(string name)
        {
            Page objp = new Page();
            //string a = "";
            //a = Session["_MainSearchValue"].ToString().Substring(0, Session["_MainSearchValue"].ToString().IndexOf("("));
            name = name.Substring(0, name.IndexOf("("));
            name = name.StartsWith("LV") ? name.Substring(5) : name.Substring(5);
            objp.Session["_MainSearchValue"] = name.Trim();
            return name;
            //HttpContext.Current.Session["_MainSearchValue"] = name;
            //return "1";
        }

        protected void GetHRPERNRS()
        {
            string sts = "";
            configurationdalDataContext objAnnouncementDataContext = new configurationdalDataContext();
            objAnnouncementDataContext.usp_CheckHR(Session["CompCode"].ToString(), HttpContext.Current.User.Identity.Name, ref sts);
            if (sts.Trim().ToUpper() == "TRUE")
            {
                togglemgr.Visible = true;
                cbUser1.Visible = true;
            }

        }

        public void LoadEmployeeDetails()
        {
            //http://shawpnendu.blogspot.com/2010/02/javascript-to-read-master-page-and.html
            //string userName = Request.QueryString["username"];

            try
            {
                memUser = Membership.GetUser();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            try
            {
                bool status = memUser.IsOnline;

                if (memUser != null)
                {
                    configurationbl objBl = new configurationbl();
                    configurationcollectionbo objLst = objBl.Load_EmployeePhotoDetails(memUser.ToString());
                    foreach (configurationbo objBo in objLst)
                    {
                        ViewState["isActive"] = objBo.isAct;
                        Session.Add("CompLogin", objBo.iscomp);
                        string sEmailId = memUser.Email.ToString();

                        Session.Add("sEmploreeId", memUser.ToString());
                        Session.Add("EmployeeName", objBo.DESCRIPTION);
                        Session.Add("CompCode", objBo.Company_Code.Trim() == "" ? memUser.ToString() : objBo.Company_Code.Trim());
                        if (objBo.EMPLOYEE_PATH.ToString() != "")
                        {

                        }
                    }
                }
                else if (memUser.UserName == "" || memUser.UserName == null || !status)
                {

                }
                if (memUser.ToString() != "70296")
                {
                    if (((bool)ViewState["isActive"] == true) && (Session["CompLogin"].ToString().Trim() == "1"))
                    {
                        Response.Redirect("~/Account/Login.aspx", false);
                    }
                    if (((bool)ViewState["isActive"] == false) && (Session["CompLogin"].ToString().Trim() == "0"))
                    {
                        Response.Redirect("~/Account/Login.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Account/Login.aspx", false);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void cbUser1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUser1.Checked)
            {
                dvUser.Visible = true;
                dvApprover.Visible = false;
                HFmethdLoader.Value = "1"; HFsortId.Value = "1";
                LoadGridDetails();
            }
            else
            {
                dvUser.Visible = false;
                dvApprover.Visible = true;
                HFmethdLoader.Value = "3";
                HFsortId.Value = "1";
                LoadGridDetails(DDL_AppHLP.SelectedValue.ToString().Trim());
                LoadEmployySubOrdinates();

            }
        }

        protected void RecordWorkingDetailsControlStatus(Boolean bIsStatus)
        {
            pnlRecordWorking.Visible = true;
            lblValidateRWCommnets.Text = "";

            if (bIsStatus == true)
            {
                tblRWT.Visible = false;
            }
            else
            {
                tblRWT.Visible = true;
            }
        }


        private void BindBlankGV_DashboardDetails()
        {
            try
            {
                using (DataTable Dt = new DataTable())
                {
                    Dt.Columns.Add("TEXT", typeof(string));
                    Dt.Columns.Add("VALUE", typeof(string));

                    GV_DashboardDetails.DataSource = Dt;
                    GV_DashboardDetails.DataBind();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        protected void LoadSelDropDown()
        {
            msassignedtomebo objAssginTMBo = new msassignedtomebo();
            msassignedtomebl objAssginTMBl = new msassignedtomebl();


            msassignedtomecollectionbo objAssginTMBolist = objAssginTMBl.Get_HRPERNR(User.Identity.Name.ToString().Trim(), Session["CompCode"].ToString().Trim().ToLower());
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
                    
                    DDL_AppHLP.Items.Add(new ListItem("Personal Information", "0"));
                    DDL_AppHLP.Items.Add(new ListItem("Leave/Attd.", "1"));
                    //DDL_AppHLP.Items.Add(new ListItem("Record Working Time", "2"));
                }

                else
                {
                    DDL_AppHLP.Items.Add(new ListItem("Leave/Attd.", "1"));
                    DDL_AppHLP.Items.Add(new ListItem("Record Working Time", "2"));

                    DDL_HRTabSel.Items.Add(new ListItem("Leave/Attd.", "1"));
                    DDL_HRTabSel.Items.Add(new ListItem("Record Working Time", "2"));
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('HR Admin information is not maintained in iEmpPower!');", true);
            }
        }


        protected void btnFavData_Click(object sender, EventArgs e)
        {
            try
            {
                masterbl objbl = new masterbl();
                bool? val = false;
                if (cbUser1.Checked == true)
                {
                    objbl.update_favt(User.Identity.Name.Trim(), 1, true, HF_FavData.Value.Trim(), val);
                }
                if (cbUser1.Checked == false)
                {
                    objbl.update_favt(User.Identity.Name.Trim(), 2, true, HF_FavData.Value.Trim(), val);
                }
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "-1":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Max 4 favorites can be added to main view');", true);
                        break;
                    case "-2":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Min 3 favorites must be in main view');", true);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true);
                        break;
                }
            }
        }

        protected void loadTiledetails()
        {
            try
            {
                int cnt = 0;
                HtmlGenericControl mainDiv = new HtmlGenericControl();
                HtmlGenericControl mainDivcol = new HtmlGenericControl();
                HtmlGenericControl mainAppDiv = new HtmlGenericControl();
                HtmlGenericControl mainAppDivcol = new HtmlGenericControl();
                //HtmlGenericControl compDiv = new HtmlGenericControl();
                //HtmlGenericControl compDivcol = new HtmlGenericControl();
                masterbl objBl = new masterbl();
                masterbo objBo = new masterbo();
                masterdalDataContext objDataContext = new masterdalDataContext();

                if (cbUser1.Checked == true)
                {
                    mainDiv.Attributes.Add("Class", "row");
                    mainDivcol.Attributes.Add("Class", "row");
                    plTile1.Controls.Add(mainDiv);
                    foreach (var bo in objDataContext.usp_load_dashboard_Tile_details(User.Identity.Name.Trim(),Session["CompCode"].ToString().ToLower().Trim(),1, true))
                    {
                        mainDiv.InnerHtml += bo.U_Tile_Path;
                    }
                    pnlTileEmpCols.Controls.Add(mainDivcol);
                    foreach (var bo in objDataContext.usp_load_dashboard_Tile_details(User.Identity.Name.Trim(), Session["CompCode"].ToString().ToLower().Trim(), 1, false))
                    {
                        cnt += 1;
                        mainDivcol.InnerHtml += bo.U_Tile_Path;
                    }
                    divMore.Visible = cnt > 0 ? true : false;

                }

                 if (cbUser1.Checked == false)
                    {
                        cnt = 0;
                        mainAppDiv.Attributes.Add("Class", "row");
                        mainAppDivcol.Attributes.Add("Class", "row");
                        divTileApprv.Controls.Add(mainAppDiv);
                        foreach (var bo in objDataContext.usp_load_dashboard_Tile_details(User.Identity.Name.Trim(), Session["CompCode"].ToString().ToLower().Trim(), 2, true))
                        {
                            mainAppDiv.InnerHtml += bo.U_Tile_Path;
                        }
                        divTileCollApprv.Controls.Add(mainAppDivcol);
                        foreach (var bo in objDataContext.usp_load_dashboard_Tile_details(User.Identity.Name.Trim(), Session["CompCode"].ToString().ToLower().Trim(), 2, false))
                        {
                            cnt += 1;
                            mainAppDivcol.InnerHtml += bo.U_Tile_Path;
                        }
                        divMoreApp.Visible = cnt > 0 ? true : false;
                    }

                    //else if (Session["CompLogin"].ToString() == "1")
                    //{
                    //    cnt = 0;
                    //    compDiv.Attributes.Add("Class", "row");
                    //    compDivcol.Attributes.Add("Class", "row");
                    //    plTile1.Controls.Add(compDiv);
                    //    foreach (var bo in objDataContext.usp_load_dashboard_Tile_details(Session["CompCode"].ToString().ToLower().Trim(), 3, true))
                    //    {
                    //        compDiv.InnerHtml += bo.U_Tile_Path;
                    //    }
                    //    pnlTileEmpCols.Controls.Add(compDivcol);
                    //    foreach (var bo in objDataContext.usp_load_dashboard_Tile_details(Session["CompCode"].ToString().ToLower().Trim(), 3, false))
                    //    {
                    //        cnt += 1;
                    //        compDivcol.InnerHtml += bo.U_Tile_Path;
                    //    }
                    //    divMore.Visible = cnt > 0 ? true : false;
                    //}
                }
            catch (Exception Exception) { }
        }

        protected void LbtTabPending_Click(object sender, EventArgs e)
        {
            LbtTabPending.Attributes.Add("class", "nav-link  p-2 active");
            pending.Attributes.Add("class", "show active");
            LbtTabComplt.Attributes.Add("class", "nav-link  p-2 ");
            pending.Visible = true;
            completed.Visible = false;
            HFmethdLoader.Value = "1";
            HFsortId.Value = "1";
            lnkbtnTodayUPpending.Attributes.Add("class", "btn btn-xs btn-secondary");
            lnkbtnYesterdayUPpending.Attributes.Add("class", "btn btn-xs btn-light");
            lnkbtnTwoDaysUPpending.Attributes.Add("class", "btn btn-xs btn-light");
            LoadGridDetails();
        }

        protected void LbtTabComplt_Click(object sender, EventArgs e)
        {
            LbtTabComplt.Attributes.Add("class", "nav-link  p-2 active");
            LbtTabPending.Attributes.Add("class", "nav-link  p-2 ");
            completed.Attributes.Add("class", " show active");
            pending.Visible = false;
            completed.Visible = true;
            HFmethdLoader.Value = "2";
            HFsortId.Value = "1";
            lnkbtncompToday.Attributes.Add("class", "btn btn-xs btn-secondary");
            lnkbtncompyYestrdy.Attributes.Add("class", "btn btn-xs btn-light");
            lnkbtncompTwodayb4.Attributes.Add("class", "btn btn-xs btn-light");
            LoadCompletedGridDetails();
        }

        protected void btnTodayUPpending_Click(object sender, EventArgs e)
        {
            HFsortId.Value = "1";
            if (HFmethdLoader.Value == "1")
            {
                lnkbtnTodayUPpending.Attributes.Add("class", "btn btn-xs btn-secondary");
                lnkbtnYesterdayUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                lnkbtnTwoDaysUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                LoadGridDetails();
            }
            else if (HFmethdLoader.Value == "2")
            {
                lnkbtncompToday.Attributes.Add("class", "btn btn-xs btn-secondary");
                lnkbtncompyYestrdy.Attributes.Add("class", "btn btn-xs btn-light");
                lnkbtncompTwodayb4.Attributes.Add("class", "btn btn-xs btn-light");
                LoadCompletedGridDetails();
            }
            else if (HFmethdLoader.Value == "3")
            {
                btnAppPendToday.Attributes.Add("class", "btn btn-xs btn-secondary");
                btnAppPendYsterday.Attributes.Add("class", "btn btn-xs btn-light");
                btnAppPendTwodayb4.Attributes.Add("class", "btn btn-xs btn-light");
                LoadGridDetails(DDL_AppHLP.SelectedValue.ToString().Trim());
            }
            else if (HFmethdLoader.Value == "4")
            {
                btnAppCmpToday.Attributes.Add("class", "btn btn-xs btn-secondary");
                btnAppCmpYesterday.Attributes.Add("class", "btn btn-xs btn-light");
                btnAppCmpTwodayb4.Attributes.Add("class", "btn btn-xs btn-light");
                LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString());
            }
            else
            {
                lnkbtnTodayUPpending.Attributes.Add("class", "btn btn-xs btn-secondary");
                lnkbtnYesterdayUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                lnkbtnTwoDaysUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                LoadGridDetails();
            }
        }

        protected void btnYesterdayUPpending_Click(object sender, EventArgs e)
        {
            HFsortId.Value = "2";
            if (HFmethdLoader.Value == "1")
            {
                lnkbtnYesterdayUPpending.Attributes.Add("class", "btn btn-xs btn-secondary");
                lnkbtnTodayUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                lnkbtnTwoDaysUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                LoadGridDetails();
            }
            else if (HFmethdLoader.Value == "2")
            {
                lnkbtncompyYestrdy.Attributes.Add("class", "btn btn-xs btn-secondary");
                lnkbtncompToday.Attributes.Add("class", "btn btn-xs btn-light");
                lnkbtncompTwodayb4.Attributes.Add("class", "btn btn-xs btn-light");
                LoadCompletedGridDetails();
            }
            else if (HFmethdLoader.Value == "3")
            {
                btnAppPendYsterday.Attributes.Add("class", "btn btn-xs btn-secondary");
                btnAppPendToday.Attributes.Add("class", "btn btn-xs btn-light");
                btnAppPendTwodayb4.Attributes.Add("class", "btn btn-xs btn-light");
                LoadGridDetails(DDL_AppHLP.SelectedValue.ToString().Trim());
            }
            else if (HFmethdLoader.Value == "4")
            {
                btnAppCmpYesterday.Attributes.Add("class", "btn btn-xs btn-secondary");
                btnAppCmpToday.Attributes.Add("class", "btn btn-xs btn-light");
                btnAppCmpTwodayb4.Attributes.Add("class", "btn btn-xs btn-light");
                LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString());
            }
            else
            {
                lnkbtnYesterdayUPpending.Attributes.Add("class", "btn btn-xs btn-secondary");
                lnkbtnTodayUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                lnkbtnTwoDaysUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                LoadGridDetails();
            }
        }

        protected void btnTwoDaysUPpending_Click(object sender, EventArgs e)
        {
            HFsortId.Value = "3";

            if (HFmethdLoader.Value == "1")
            {
                lnkbtnTwoDaysUPpending.Attributes.Add("class", "btn btn-xs btn-secondary");
                lnkbtnTodayUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                lnkbtnYesterdayUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                LoadGridDetails();
            }
            else if (HFmethdLoader.Value == "2")
            {
                lnkbtncompTwodayb4.Attributes.Add("class", "btn btn-xs btn-secondary");
                lnkbtncompToday.Attributes.Add("class", "btn btn-xs btn-light");
                lnkbtncompyYestrdy.Attributes.Add("class", "btn btn-xs btn-light");
                LoadCompletedGridDetails();
            }
            else if (HFmethdLoader.Value == "3")
            {
                btnAppPendTwodayb4.Attributes.Add("class", "btn btn-xs btn-secondary");
                btnAppPendToday.Attributes.Add("class", "btn btn-xs btn-light");
                btnAppPendYsterday.Attributes.Add("class", "btn btn-xs btn-light");
                LoadGridDetails(DDL_AppHLP.SelectedValue.ToString().Trim());
            }
            else if (HFmethdLoader.Value == "4")
            {
                btnAppCmpTwodayb4.Attributes.Add("class", "btn btn-xs btn-secondary");
                btnAppCmpToday.Attributes.Add("class", "btn btn-xs btn-light");
                btnAppCmpYesterday.Attributes.Add("class", "btn btn-xs btn-light");
                LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString());
            }
            else
            {
                lnkbtnTwoDaysUPpending.Attributes.Add("class", "btn btn-xs btn-secondary");
                lnkbtnTodayUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                lnkbtnYesterdayUPpending.Attributes.Add("class", "btn btn-xs btn-light");
                LoadGridDetails();
            }
        }



//------------------------------------------------ ME AS USER (MY REQUESTS)---------------------------------------------------------------------


        //---------------------------------------- Pending Start (My Requests) ------------------------------------
        protected void LoadGridDetails()
        {
            int? RecordCnt = 0;
            pidashboardbo objPIDashBoardBo = new pidashboardbo();
            pidashboardbl objPIDashBoardBl = new pidashboardbl();
            objPIDashBoardBo.PERNR = User.Identity.Name;
            objPIDashBoardBo.PageSize = PagerSz;
            objPIDashBoardBo.PageIndex = PendingPageIndex;
            pidashboardcollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Dashboard_Details(objPIDashBoardBo, DDLYear.SelectedValue.ToString().Trim(), Session["CompCode"].ToString().Trim().ToLower(), ref RecordCnt,Convert.ToInt32(HFsortId.Value));
            if (objPIDashBoardLst.Count <= 0)
            {
            }
            else
            {
                
            }

            grdPending.DataSource = objPIDashBoardLst;
            grdPending.DataBind();
            Session.Add("grdLst", objPIDashBoardLst);

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
            divpendingrecordcount.InnerHtml = objPIDashBoardLst.Count > 0 ? "Showing " + frow + " to " + lrow + " of " + RecordCnt + " entries" : "";
            divpageNcnt.Visible = objPIDashBoardLst.Count > 0 ? true : false;
            PopulatePendingPager(objPIDashBoardLst.Count > 0 ? int.Parse(RecordCnt.ToString()) : 0, PendingPageIndex, RptrPendingPager);

        }

        protected void grdPending_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdPending.PageIndex = e.NewPageIndex;
            pidashboardcollectionbo objPIDashBoardLst = (pidashboardcollectionbo)Session["grdLst"];
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

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = PendingPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                LoadGridDetails();
                GridViewDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;

                GV_DashboardDetails.DataBind();
                GridViewDetails.DataBind();
                grdRecordTime.DataBind();
                tblRWT.Visible = false;

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        protected void grdPending_Sorting(object sender, GridViewSortEventArgs e)
        {

            pidashboardcollectionbo objPIDashBoardLst = (pidashboardcollectionbo)Session["grdLst"];
            bool objSortOrder = (bool)Session["bSortedOrder"];
            switch (e.SortExpression.ToString().Trim())
            {
                case "PERNR":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return ((long.Parse(objBo1.PERNR)).CompareTo(long.Parse(objBo2.PERNR))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                        { return ((long.Parse(objBo2.PERNR)).CompareTo(long.Parse(objBo1.PERNR))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "PKEY":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.PKEY.CompareTo(objBo2.PKEY)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                        { return (objBo2.PKEY.CompareTo(objBo1.PKEY)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "MANAGER_APPROVAL":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.MANAGER_APPROVAL.CompareTo(objBo2.MANAGER_APPROVAL)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                        { return (objBo2.MANAGER_APPROVAL.CompareTo(objBo1.MANAGER_APPROVAL)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "CHANGE_APPROVAL":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.CHANGE_APPROVAL.CompareTo(objBo2.CHANGE_APPROVAL)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
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
                            objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.Subtype.CompareTo(objBo2.Subtype)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
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
                            objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.REVIEW.CompareTo(objBo2.REVIEW)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
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
                            objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.LAST_ACTIVITY_DATE.CompareTo(objBo2.LAST_ACTIVITY_DATE)); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                        { return (objBo2.LAST_ACTIVITY_DATE.CompareTo(objBo1.LAST_ACTIVITY_DATE)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

            }


            grdPending.DataSource = objPIDashBoardLst;
            grdPending.DataBind();

            Session.Add("grdLst", objPIDashBoardLst);

            GridViewDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;

            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            tblRWT.Visible = false;
        }

        protected void grdPending_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "VIEW":
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                   
                    GV_DashboardDetails.Visible = false;
                    int flag = 1;
                    //AllPnelStatus();
                    GridViewRow grdRow = grdPending.Rows[rowIndex];//SelectedRow;
                    Session.Add("currentSelectedRow", grdRow);
                    string sPernr = grdPending.DataKeys[grdRow.RowIndex]["PERNR"].ToString();//grdRow.Cells[0].Text;
                    string sPkey = grdPending.DataKeys[grdRow.RowIndex]["PKEY"].ToString(); //grdRow.Cells[1].Text;
                    //string sStatus = grdPending.DataKeys[grdPending.SelectedIndex]["MANAGER_APPROVAL"].ToString(); //grdRow.Cells[2].Text;
                    string sApprovalType = grdPending.DataKeys[grdRow.RowIndex]["CHANGE_APPROVAL"].ToString(); //grdRow.Cells[3].Text;
                    //DateTime dtLateDate = DateTime.Parse(grdPending.DataKeys[grdPending.SelectedIndex]["PKEY"].ToString());//grdRow.Cells[5].Text       
                    DateTime dtLateDate = DateTime.Parse(grdPending.DataKeys[grdRow.RowIndex]["LAST_ACTIVITY_DATE"].ToString()); //DateTime.Parse(grdRow.Cells[3].Text);
                    int id = int.Parse(grdPending.DataKeys[grdRow.RowIndex]["ID"].ToString());
                    string TblTyp = grdPending.DataKeys[grdRow.RowIndex]["TableTyp"].ToString();
                    HF_TBLTYPE.Value = TblTyp.ToString().Trim();
                    HF_ID.Value = id.ToString().Trim();
                    string strRecipientsPhn = string.Empty;


                    pidashboardbl objPIDashBl = new pidashboardbl();
                    try
                    {
                        switch (sApprovalType)
                        {
                            #region Address_Change_Approval
                            case "Address Change Details":


                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GridViewDetails.Visible = true;
                                piaddressinformationbo objPIAddBo = new piaddressinformationbo();
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


                            #endregion


                            #region Communication_Details_Approval

                            case "Communication Change Details":

                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GridViewDetails.Visible = true;
                                picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
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
                            #endregion

                            #region Personal_Data_Details_Approval
                            case "Personal Data Change Details":
                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GridViewDetails.Visible = true;
                                personaldatabo objPersonaldataBo = new personaldatabo();
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
                            #endregion

                            #region Personal_ID_Details_Approval
                            case "Personal ID Change Details":
                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GridViewDetails.Visible = true;
                                pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
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
                            #endregion

                            #region Family_Member_Details_Approval
                            case "Family Members Change Approval":
                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GridViewDetails.Visible = true;
                                pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                objFamilyBo.PKEY = sPkey;
                                objFamilyBo.ID = id;
                                pidashboardbl objPIDashblF = new pidashboardbl();
                                pifamilymemberscollectionbo objFamilylst = objPIDashblF.Get_FamilyMemberDetails_For_Approval(objFamilyBo, flag);
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
                            #endregion



                            #region Leave_Request_Details_Approval


                            case "Leave Request":

                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GV_DashboardDetails.Visible = true;
                                leaverequestbo objLeaveRequestBo = new leaverequestbo();
                                objLeaveRequestBo.PKEY = sPkey;
                                objLeaveRequestBo.LEAVE_REQ_ID = id;
                                pidashboardbl objLeaveRequestBl = new pidashboardbl();
                                leaverequestcollectionbo objLeaveReqLst = objLeaveRequestBl.Get_LeaveRequest_Details_For_Approval_For_Employee(objLeaveRequestBo, "PA2001");

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


                            case "Attendance Request Approval":

                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GV_DashboardDetails.Visible = true;
                                leaverequestbo objLeaveRequestBoA = new leaverequestbo();
                                objLeaveRequestBoA.PKEY = sPkey;
                                objLeaveRequestBoA.LEAVE_REQ_ID = id;
                                pidashboardbl objLeaveRequestBlA = new pidashboardbl();
                                leaverequestcollectionbo objLeaveReqLstA = objLeaveRequestBlA.Get_LeaveRequest_Details_For_Approval_For_Employee(objLeaveRequestBoA, "PA2002");

                                GridViewDetails.DataSource = null;
                                GridViewDetails.DataBind();


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
                            #endregion

                            #region Record_Working_Time_Details_Approval
                            case "Recordworking Time Details":

                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = true;
                                tblRWT.Visible = false;
                                GridViewDetails.DataSource = null;
                                GridViewDetails.DataBind();
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();

                                RecordWorkingDetailsControlStatus(true);
                                wtrecordworkingtimebo objRecordBo = new wtrecordworkingtimebo();
                                objRecordBo.PKEY = sPkey;
                                objRecordBo.COMMENTS = Session["CompCode"].ToString().ToLower().Trim();
                                objRecordBo.ISAPPROVED = false;
                                wtrecordworkingtimecollectionbo objRecordLst = objPIDashBl.Get_RecordDetails_For_Approval_Employee(objRecordBo);
                                LoadRecordWorking(objRecordLst, objRecordBo);


                                break;
                            #endregion



                            default:

                                break;
                        }
                    }
                    catch (Exception)
                    {}

                    break;
                default:
                    break;
            }
            MPE_Pend.Show();
        }
        //------------------------------ Pending end (My Requests)------------------------------------------------------

        //------------------------------ Completed start(My Requests)------------------------------------------------------
        protected void LoadCompletedGridDetails()
        {
            int? RecordCnt = 0;
            pidashboardbo objPIDashBoardBo = new pidashboardbo();
            pidashboardbl objPIDashBoardBl = new pidashboardbl();
            objPIDashBoardBo.PERNR = User.Identity.Name;
            objPIDashBoardBo.PageSize = PagerSz;
            objPIDashBoardBo.PageIndex = CompleatedPageIndex;
            pidashboardcollectionbo objPIDashBoardCmpltdLst = objPIDashBoardBl.Get_Dashboard__Completed_Details(objPIDashBoardBo, Session["CompCode"].ToString().Trim().ToLower(), DDLYear.SelectedValue.ToString().Trim(), ref RecordCnt, Convert.ToInt32(HFsortId.Value));
            if (objPIDashBoardCmpltdLst.Count <= 0)
            {
                //lblMessageBoard.Text = "";// GetLocalResourceObject("NoCompletedRecord").ToString();
                //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                //lblMessage.Text = "";
            }
            else
            {
                //lblMessage.Text = "The below shown request's are raised by you to get approved by supervisor.";
                //lblMessage.ForeColor = System.Drawing.Color.Green;
            }

            grdCompleted.DataSource = objPIDashBoardCmpltdLst;
            grdCompleted.DataBind();

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
            divUcompledCount.InnerHtml = objPIDashBoardCmpltdLst.Count > 0 ? "Showing " + frow + " to " + lrow + " of " + RecordCnt + " entries" : "";
            divpageNcntcomp.Visible = objPIDashBoardCmpltdLst.Count > 0 ? true : false;

            //PopulateCompleatedPager(objPIDashBoardCmpltdLst.Count > 0 ? int.Parse(objPIDashBoardCmpltdLst[0].Total_Record.ToString()) : 0, CompleatedPageIndex);
            PopulatePendingPager(objPIDashBoardCmpltdLst.Count > 0 ? int.Parse(RecordCnt.ToString()) : 0, CompleatedPageIndex, RptrCompletedPager);
            Session.Add("grdCmpltdLst", objPIDashBoardCmpltdLst);

        }

        protected void grdCompleted_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdCompleted.PageIndex = e.NewPageIndex;
            pidashboardcollectionbo objPIDashBoardCmpltdLst = (pidashboardcollectionbo)Session["grdCmpltdLst"];
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

            pidashboardcollectionbo objPIDashBoardCmpltdLst = (pidashboardcollectionbo)Session["grdCmpltdLst"];
            bool objSortOrder = (bool)Session["bSortedOrder"];
            switch (e.SortExpression.ToString().Trim())
            {
                case "PERNR":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardCmpltdLst != null)
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return ((long.Parse(objBo1.PERNR)).CompareTo(long.Parse(objBo2.PERNR))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                        { return ((long.Parse(objBo2.PERNR)).CompareTo(long.Parse(objBo1.PERNR))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "AppByName":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardCmpltdLst != null)
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.AppByName.CompareTo(objBo2.AppByName)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                        { return (objBo2.AppByName.CompareTo(objBo1.AppByName)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "CHANGE_APPROVAL":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardCmpltdLst != null)
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.CHANGE_APPROVAL.CompareTo(objBo2.CHANGE_APPROVAL)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
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
                            objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.Subtype.CompareTo(objBo2.Subtype)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
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
                            objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.REVIEW.CompareTo(objBo2.REVIEW)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
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
                            objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                            { return (objBo1.LAST_ACTIVITY_DATE.CompareTo(objBo2.LAST_ACTIVITY_DATE)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(pidashboardbo objBo1, pidashboardbo objBo2)
                        { return (objBo2.LAST_ACTIVITY_DATE.CompareTo(objBo1.LAST_ACTIVITY_DATE)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

            }


            grdCompleted.DataSource = objPIDashBoardCmpltdLst;
            grdCompleted.DataBind();

            Session.Add("grdCmpltdLst", objPIDashBoardCmpltdLst);
            GridViewDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;

            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            tblRWT.Visible = false;
        }


        protected void grdCompleted_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "VIEW":
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    
                    pidashboardbl objPIDashBl = new pidashboardbl();
                    GV_DashboardDetails.Visible = false;

                    int flag = 1;
                    GridViewRow grdRow = grdCompleted.Rows[rowIndex];//.SelectedRow;
                    Session.Add("currentSelectedRow", grdRow);

                    string sPernr = grdCompleted.DataKeys[grdRow.RowIndex]["PERNR"].ToString();//grdRow.Cells[0].Text;
                    string sPkey = grdCompleted.DataKeys[grdRow.RowIndex]["PKEY"].ToString(); //grdRow.Cells[1].Text;
                    // string sStatus = grdCompleted.DataKeys[grdCompleted.SelectedIndex]["MANAGER_APPROVAL"].ToString(); //grdRow.Cells[2].Text;
                    string sApprovalType = grdCompleted.DataKeys[grdRow.RowIndex]["CHANGE_APPROVAL"].ToString(); //grdRow.Cells[3].Text;
                    //DateTime dtLateDate = DateTime.Parse(grdRow.Cells[5].Text);

                    DateTime dtLateDate = DateTime.Parse(grdCompleted.DataKeys[grdRow.RowIndex]["LAST_ACTIVITY_DATE"].ToString()); //DateTime.Parse(grdRow.Cells[5].Text);
                    // string sRole = grdRow.Cells[7].Text;
                    int id = int.Parse(grdCompleted.DataKeys[grdRow.RowIndex]["ID"].ToString());
                    string TblTyp = grdCompleted.DataKeys[grdRow.RowIndex]["TableTyp"].ToString();
                    string sts = grdCompleted.DataKeys[grdRow.RowIndex]["REVIEW"].ToString();

                    DateTime dtLateActDate = DateTime.Parse(grdCompleted.DataKeys[grdRow.RowIndex]["LAST_ACTIVITY_DATE"].ToString());
                    DateTime ModifiedDate = DateTime.Parse(grdCompleted.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString());

                    HF_TBLTYPE.Value = TblTyp.ToString().Trim();
                    HF_ID.Value = id.ToString().Trim();
                    string strRecipientsPhn = string.Empty;
                    try
                    {
                        switch (sApprovalType)
                        {
                            #region Address_Info
                            case "Address Change Approval":

                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                                objPIAddBo.PKEY = sPkey;
                                objPIAddBo.ID = id;
                                pidashboardbl objPIDashblA = new pidashboardbl();
                                piaddressinformationcollectionbo objPIAddBoLst = objPIDashblA.Get_Address_completed_Details_For_Approval(objPIAddBo, sts, dtLateActDate, ModifiedDate);
                                if (objPIAddBoLst.Count > 0)
                                {
                                    GV_DashboardDetails.Visible = true;
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GV_DashboardDetails.DataSource = objPIAddBoLst;
                                    GV_DashboardDetails.DataBind();
                                }
                                else
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GVNodata(GV_DashboardDetails);
                                }
                                break;
                            #endregion


                            #region Communiation_Info
                            case "Communication Change Approval":
                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GV_DashboardDetails.Visible = true;
                                picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                                objCommuInfoBo.PKEY = sPkey;
                                objCommuInfoBo.ID = id;
                                pidashboardbl objPIDashblC = new pidashboardbl();
                                picommunicationinformationcollectionbo objCommuInfoLst = objPIDashblC.Get_Communication_completed_Details_For_Approval(objCommuInfoBo, sts, dtLateActDate, ModifiedDate);
                                if (objCommuInfoLst.Count > 0)
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GV_DashboardDetails.DataSource = objCommuInfoLst;
                                    GV_DashboardDetails.DataBind();
                                }
                                else
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GVNodata(GV_DashboardDetails);
                                }

                                break;

                            #endregion

                            case "Personal Data Change Approval":

                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GV_DashboardDetails.Visible = true;
                                personaldatabo objPersonaldataBo = new personaldatabo();
                                objPersonaldataBo.PKEY = sPkey;
                                objPersonaldataBo.ID = id;
                                pidashboardbl objPIDashblPD = new pidashboardbl();
                                personaldatacollectionbo objPersonaldataList = objPIDashblPD.Get_PersonalData_completed_Details_For_Approval(objPersonaldataBo, sts, dtLateActDate, ModifiedDate);
                                if (objPersonaldataList.Count > 0)
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GV_DashboardDetails.DataSource = objPersonaldataList;
                                    GV_DashboardDetails.DataBind();
                                }
                                else
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GVNodata(GV_DashboardDetails);
                                }



                                break;


                            case "Personal ID Change Approval":

                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GV_DashboardDetails.Visible = true;
                                pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                                objPersonalIDsBo.PKEY = sPkey;
                                objPersonalIDsBo.ID = id;
                                pidashboardbl objPIDashblPI = new pidashboardbl();
                                pipersonalidscollectionbo objPersonalIDsLst = objPIDashblPI.Get_PersonalIDS_completed_Details_For_Approval(objPersonalIDsBo, sts, dtLateActDate, ModifiedDate);
                                if (objPersonalIDsLst.Count > 0)
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GV_DashboardDetails.DataSource = objPersonalIDsLst;
                                    GV_DashboardDetails.DataBind();
                                }
                                else
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GVNodata(GV_DashboardDetails);
                                }

                                break;



                            #region Family_Member
                            case "Family Members change approval":


                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GV_DashboardDetails.Visible = true;
                                pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                objFamilyBo.PKEY = sPkey;
                                objFamilyBo.ID = id;
                                pidashboardbl objPIDashblF = new pidashboardbl();
                                pifamilymemberscollectionbo objFamilylst = objPIDashblF.Get_Family_completed_Details_For_Approval(objFamilyBo, sts, dtLateActDate, ModifiedDate);
                                if (objFamilylst.Count > 0)
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GV_DashboardDetails.DataSource = objFamilylst;
                                    GV_DashboardDetails.DataBind();
                                }
                                else
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GVNodata(GV_DashboardDetails);
                                }

                                break;
                            #endregion





                            #region Leave_Request


                            #region Leave_Request_Details_Approval


                            //Deletion Request Approval'
                            case "Deletion Request Approval":
                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                //LeaveRequestDetailsControlStatus(true);
                                GV_DashboardDetails.Visible = true;
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
                                    GV_DashboardDetails.DataSource = objLeaveReqLstD;
                                    GV_DashboardDetails.DataBind();

                                    GridViewDetails.DataSource = null;
                                    GridViewDetails.DataBind();
                                }
                                else
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GVNodata(GV_DashboardDetails);
                                }


                                break;

                            case "Leave Request Approval":
                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GV_DashboardDetails.Visible = true;
                                leaverequestbo objLeaveRequestBo = new leaverequestbo();
                                objLeaveRequestBo.PKEY = sPkey;
                                objLeaveRequestBo.LEAVE_REQ_ID = id;
                                pidashboardbl objLeaveRequestBl = new pidashboardbl();
                                leaverequestcollectionbo objLeaveReqLst = objLeaveRequestBl.Get_LeaveRequest_Details_For_Approval_For_Employee(objLeaveRequestBo, "PA2001");

                                if (objLeaveReqLst.Count > 0)
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GV_DashboardDetails.DataSource = objLeaveReqLst;
                                    GV_DashboardDetails.DataBind();
                                    GridViewDetails.DataSource = null;
                                    GridViewDetails.DataBind();
                                }
                                else
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GVNodata(GV_DashboardDetails);
                                }


                                break;


                            case "Attendance Request Approval":
                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                grdRecordTime.Visible = false;
                                tblRWT.Visible = false;
                                GV_DashboardDetails.Visible = true;
                                leaverequestbo objLeaveRequestBoA = new leaverequestbo();
                                objLeaveRequestBoA.PKEY = sPkey;
                                objLeaveRequestBoA.LEAVE_REQ_ID = id;
                                pidashboardbl objLeaveRequestBlA = new pidashboardbl();
                                leaverequestcollectionbo objLeaveReqLstA = objLeaveRequestBlA.Get_LeaveRequest_Details_For_Approval_For_Employee(objLeaveRequestBoA, "PA2002");

                                if (objLeaveReqLstA.Count > 0)
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GV_DashboardDetails.DataSource = objLeaveReqLstA;
                                    GV_DashboardDetails.DataBind();
                                    GridViewDetails.DataSource = null;
                                    GridViewDetails.DataBind();
                                }
                                else
                                {
                                    GV_DashboardDetails.DataSource = null;
                                    GV_DashboardDetails.DataBind();
                                    GVNodata(GV_DashboardDetails);
                                }
                                break;
                            #endregion
                            #endregion

                            #region RecordWorking
                            case "Recordworking Time Details":

                                grdRecordTime.Visible = true;
                                grdRecordTime.DataSource = null;
                                grdRecordTime.DataBind();
                                tblRWT.Visible = true;
                                GridViewDetails.DataSource = null;
                                GridViewDetails.DataBind();
                                GV_DashboardDetails.DataSource = null;
                                GV_DashboardDetails.DataBind();

                                RecordWorkingDetailsControlStatus(false);
                                wtrecordworkingtimebo objRecordBo = new wtrecordworkingtimebo();
                                objRecordBo.PKEY = sPkey;
                                objRecordBo.COMMENTS = Session["CompCode"].ToString().ToLower().Trim();
                                objRecordBo.ISAPPROVED = true;
                                wtrecordworkingtimecollectionbo objRecordLst = objPIDashBl.Get_RecordDetails_For_Approval_Employee(objRecordBo);
                                wtrecordworkingtimebo objRWBo = objRecordLst.Find(delegate(wtrecordworkingtimebo obj)
                                { return true; });
                                LoadRecordWorking(objRecordLst, objRecordBo);
                                lblValidateRWCommnets.Text = objRWBo.COMMENTS;
                                break;
                            #endregion



                            default:
                                break;
                        }
                    }
                    catch (Exception Ex)
                    {}
                    break;
                default:
                    break;
            }
            MPE_Pend.Show();
        }

        protected void lnkPage_Ucompletd_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = CompleatedPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                LoadCompletedGridDetails();
                GridViewDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;

                GV_DashboardDetails.DataBind();
                GridViewDetails.DataBind();
                grdRecordTime.DataBind();
                tblRWT.Visible = false;

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        //------------------------------ Completed End(My Requests)------------------------------------------------------

        protected void GV_DashboardDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string[] RestrictRowText = { "LEAVE_REQ_ID", "PKEY", "AWART" };
                    if (RestrictRowText.Contains(DataBinder.Eval(e.Row.DataItem, "TEXT")))
                    {
                        e.Row.Style.Add("display", "none");
                    }

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        protected void GridViewDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "VIEW":
                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    int flag = 2;
                    GridViewRow grdRow = GridViewDetails.Rows[rowIndex];//SelectedRow;
                    string sPkey = GridViewDetails.DataKeys[grdRow.RowIndex]["PKEY"].ToString();
                    int id = int.Parse(GridViewDetails.DataKeys[grdRow.RowIndex]["ID"].ToString());
                    string sApprovalType = GridViewDetails.DataKeys[grdRow.RowIndex]["CHANGE_APPROVAL"].ToString();
                    string statustype = GridViewDetails.DataKeys[grdRow.RowIndex]["STATUS"].ToString().Trim();




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

                                }
                                else
                                {
                                    GVNodata(GV_DashboardDetails);
                                }

                                break;


                            case "Family Members change approval":

                                GridViewDetails.Visible = true;
                                GV_DashboardDetails.Visible = true;
                                pifamilymembersbo objFamilyInfoBo = new pifamilymembersbo();
                                // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                                objFamilyInfoBo.PKEY = sPkey;
                                objFamilyInfoBo.ID = id;
                                objFamilyInfoBo.STATUS = statustype;
                                pidashboardbl objPIDashblF = new pidashboardbl();
                                pifamilymemberscollectionbo objFamilyInfoLst = objPIDashblF.Get_FamilyMemberDetails_For_Approval(objFamilyInfoBo, flag);
                                if (objFamilyInfoLst.Count > 0)
                                {
                                    GV_DashboardDetails.DataSource = objFamilyInfoLst;
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

                    }
                    break;
                default:
                    break;
            }
            MPE_Pend.Show();
        }

//--------------------------------------------------------- ME AS USER(MY REQUESTS)-----------------END-----------------------------------------------



//--------------------------------------------------------- ME AS Approver (MANAGER)---------START------------------------------------------------------


        //---------------------------------------------- PENDING APPROVAL------------------------------------------
        protected void LoadGridDetails(string seltype)
        {
            msassignedtomebo objAssginTMBo = new msassignedtomebo();
            msassignedtomebl objAssginTMBl = new msassignedtomebl();
            objAssginTMBo.PERNR = User.Identity.Name;
            objAssginTMBo.PageSize = PagerSz;
            objAssginTMBo.PageIndex = PendingPageIndex;
            objAssginTMBo.Flag = 1;
            int? RecCount = 0;
            msassignedtomecollectionbo objAssginTMLst = objAssginTMBl.Load_Pending_Approvals_dashb(Session["CompCode"].ToString(), objAssginTMBo, seltype, DDLYear.SelectedValue.ToString().Trim(), ref RecCount, Convert.ToInt32(HFsortId.Value));
            if (objAssginTMLst.Count <= 0)
            {
                //lblMessageBoard.Text = GetLocalResourceObject("NoPendingRecord").ToString();
                //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
            }

            grdAppPending.DataSource = objAssginTMLst;
            grdAppPending.DataBind();
            string frow = "", lrow = "";
            foreach (GridViewRow row in grdAppPending.Rows)
            {
                for (int i = 0; i < grdAppPending.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        frow = grdAppPending.Rows[i].Cells[0].Text;
                    }
                    if (i == grdAppPending.Rows.Count - 1)
                    {
                        lrow = grdAppPending.Rows[i].Cells[0].Text;
                    }
                }
            }
            divcountgridApppend.InnerHtml = objAssginTMLst.Count > 0 ? "Showing " + frow + " to " + lrow + " of " + RecCount + " entries" : "";
            divcountgridpnl.Visible = objAssginTMLst.Count > 0 ? true : false;
            PopulatePendingPager(objAssginTMLst.Count > 0 ? int.Parse(RecCount.ToString()) : 0, PendingPageIndex, repeAppPending);
            Session.Add("grdLst", objAssginTMLst);
        }

        protected void grdAppPending_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdAppPending.PageIndex = e.NewPageIndex;
            msassignedtomecollectionbo objPIDashBoardLst = (msassignedtomecollectionbo)Session["grdLst"];
            grdAppPending.DataSource = objPIDashBoardLst;
            int rselectedindex = Convert.ToInt32(ViewState["indexchang"]);
            int pagindex = Convert.ToInt32(ViewState["pageindex"]);

            grdAppPending.DataSource = objPIDashBoardLst;
            grdAppPending.SelectedIndex = -1;
            grdAppPending.DataBind();
            if (pageindex == pagindex)
            {
                grdAppPending.SelectedIndex = rselectedindex;
            }
        }


        protected void grdAppPending_RowDataBound(object sender, GridViewRowEventArgs e)
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


        protected void grdAppPending_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {

                case "VIEW":

                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    GV_AppDashboardDetails.Visible = false;
                    GV_DashboardCompleatedDetails.Visible = false;
                    int flag = 1;
                    GridViewRow grdRow = grdAppPending.Rows[rowIndex];//grdPending.SelectedRow;
                    Session.Add("currentSelectedRow", grdRow);
                    string sName = grdAppPending.DataKeys[grdRow.RowIndex]["ENAME"].ToString();
                    string sPernr = grdAppPending.DataKeys[grdRow.RowIndex]["PERNR"].ToString();
                    string sPkey = grdAppPending.DataKeys[grdRow.RowIndex]["PKEY"].ToString();
                    string sApprovalType = grdAppPending.DataKeys[grdRow.RowIndex]["CHANGE_APPROVAL"].ToString();
                    DateTime dtLateDate = DateTime.Parse(grdAppPending.DataKeys[grdRow.RowIndex]["LAST_ACTIVITY_DATE"].ToString());
                    string sRole = grdAppPending.DataKeys[grdRow.RowIndex]["PLSXT"].ToString();
                    int id = int.Parse(grdAppPending.DataKeys[grdRow.RowIndex]["ID"].ToString());
                    string TblTyp = grdAppPending.DataKeys[grdRow.RowIndex]["TableTyp"].ToString();
                    HF_STS.Value = grdAppPending.DataKeys[grdRow.RowIndex]["REVIEW"].ToString();
                    HF_TBLTYPE.Value = TblTyp.ToString().Trim();
                    HF_ID.Value = id.ToString().Trim();
                    HF_PKEY.Value = sPkey.ToString().Trim();
                    string strRecipientsPhn = string.Empty;
                    ViewState["LACRTDBY"] = grdAppPending.DataKeys[grdRow.RowIndex]["PERNR"].ToString().Trim();

                    Session.Add("recipientsPhn", strRecipientsPhn);

                    Session.Add("sRole", sRole);
                    Session.Add("sPernr", sPernr);
                    pidashboardbl objPIDashBl = new pidashboardbl();
                    try
                    {
                        switch (sApprovalType)
                        {
                            case "Address Change Approval":
                               grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                grdappRecordTime.Visible = false;
                                GridViewAppDetails.Visible = true;
                                RWTdivbtn.Visible = false;
                                RWTdiv.Visible = false;
                                txtRWComments.Text = string.Empty;
                                lblValidateRWCommnets.Text = string.Empty;
                                TblRemarks.Visible = false;
                                lblRemarksRWT.Text = string.Empty;
                                lblRemarksRWT.Visible = false;
                                piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                                objPIAddBo.PKEY = sPkey;
                                objPIAddBo.ID = id;
                                pidashboardbl objPIDashbl = new pidashboardbl();
                                piaddressinformationcollectionbo objPIAddBoLst = objPIDashbl.Get_Address_Details_For_Approval(objPIAddBo, flag);
                                if (objPIAddBoLst.Count > 0)
                                {
                                     GridViewAppDetails.DataSource = objPIAddBoLst;
                                    GridViewAppDetails.DataBind();
                                
                                }
                                else
                                {
                                    GridViewAppDetails.DataSource = null;
                                    GridViewAppDetails.DataBind();
                                }

                                break;


                            case "Communication Change Approval":

                                grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                grdappRecordTime.Visible = false;
                                GridViewAppDetails.Visible = true;
                                RWTdiv.Visible = false; RWTdivbtn.Visible = false;
                                txtRWComments.Text = string.Empty;
                                lblValidateRWCommnets.Text = string.Empty;
                                TblRemarks.Visible = false;
                                lblRemarksRWT.Text = string.Empty;
                                lblRemarksRWT.Visible = false;
                                picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                                objCommuInfoBo.PKEY = sPkey;
                                objCommuInfoBo.ID = id;
                                pidashboardbl objPIDashblC = new pidashboardbl();
                                picommunicationinformationcollectionbo objCommuInfoLst = objPIDashblC.Get_Communication_Details_For_Approval(objCommuInfoBo, flag);
                                if (objCommuInfoLst.Count > 0)
                                {
                                    GridViewAppDetails.DataSource = objCommuInfoLst;
                                    GridViewAppDetails.DataBind();
                                }
                                else
                                {
                                    GridViewAppDetails.DataSource = null;
                                    GridViewAppDetails.DataBind();
                                }

                                break;


                            case "Personal Data Change Approval":
                                 grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                grdappRecordTime.Visible = false;
                                GridViewAppDetails.Visible = true;
                                RWTdiv.Visible = false; RWTdivbtn.Visible = false;
                                txtRWComments.Text = string.Empty;
                                lblValidateRWCommnets.Text = string.Empty;
                                TblRemarks.Visible = false;
                                lblRemarksRWT.Text = string.Empty;
                                lblRemarksRWT.Visible = false;
                                personaldatabo objPersonaldataBo = new personaldatabo();
                                objPersonaldataBo.PKEY = sPkey;
                                objPersonaldataBo.ID = id;
                                pidashboardbl objPIDashblPD = new pidashboardbl();
                                personaldatacollectionbo objPersonaldataList = objPIDashblPD.Get_PersonalData_Details_For_Approval(objPersonaldataBo, flag);
                                if (objPersonaldataList.Count > 0)
                                {
                                    GridViewAppDetails.DataSource = objPersonaldataList;
                                    GridViewAppDetails.DataBind();
                                }
                                else
                                {
                                    GridViewAppDetails.DataSource = null;
                                    GridViewAppDetails.DataBind();
                                }

                                break;

                            case "Personal ID Change Approval":
                               grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                grdappRecordTime.Visible = false;
                                GridViewAppDetails.Visible = true;
                                RWTdiv.Visible = false; RWTdivbtn.Visible = false;
                                txtRWComments.Text = string.Empty;
                                lblValidateRWCommnets.Text = string.Empty;
                                TblRemarks.Visible = false;
                                lblRemarksRWT.Text = string.Empty;
                                lblRemarksRWT.Visible = false;
                                pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                                objPersonalIDsBo.PKEY = sPkey;
                                objPersonalIDsBo.ID = id;
                                pidashboardbl objPIDashblPI = new pidashboardbl();
                                pipersonalidscollectionbo objPersonalIDsLst = objPIDashblPI.Get_PersonalIDS_Details_For_Approval(objPersonalIDsBo, flag);
                                if (objPersonalIDsLst.Count > 0)
                                {
                                    GridViewAppDetails.DataSource = objPersonalIDsLst;
                                    GridViewAppDetails.DataBind();
                                }
                                else
                                {
                                    GridViewAppDetails.DataSource = null;
                                    GridViewAppDetails.DataBind();
                                }


                                break;

                            case "Family Members change approval":
                                 grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                grdappRecordTime.Visible = false;
                                GridViewAppDetails.Visible = true;
                                RWTdiv.Visible = false; RWTdivbtn.Visible = false;
                                txtRWComments.Text = string.Empty;
                                lblValidateRWCommnets.Text = string.Empty;
                                TblRemarks.Visible = false;
                                lblRemarksRWT.Text = string.Empty;
                                lblRemarksRWT.Visible = false;
                                pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                objFamilyBo.PKEY = sPkey;
                                objFamilyBo.ID = id;
                                pidashboardbl objPIDashblFM = new pidashboardbl();
                                pifamilymemberscollectionbo objFamilylst = objPIDashblFM.Get_FamilyMemberDetails_For_Approval(objFamilyBo, flag);
                                if (objFamilylst.Count > 0)
                                {
                                    GridViewAppDetails.DataSource = objFamilylst;
                                    GridViewAppDetails.DataBind();
                                }
                                else
                                {
                                    GridViewAppDetails.DataSource = null;
                                    GridViewAppDetails.DataBind();
                                }



                                break;

                            case "Leave Request":
                                grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                grdappRecordTime.Visible = false;
                                RWTdiv.Visible = false; RWTdivbtn.Visible = false; GridViewAppDetails.Visible = false;
                                txtRWComments.Text = string.Empty;
                                lblValidateRWCommnets.Text = string.Empty;
                                TblRemarks.Visible = false;
                                lblRemarksRWT.Text = string.Empty;
                                lblRemarksRWT.Visible = false;
                                leaverequestbo objLeaveRequestBo = new leaverequestbo();
                                objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                                objLeaveRequestBo.PKEY = sPkey;
                                objLeaveRequestBo.LEAVE_REQ_ID = id;                               
                                pidashboardbl objLeaveRequestBl = new pidashboardbl();
                                leaverequestcollectionbo objLeaveReqLst = objLeaveRequestBl.Get_LeaveRequest_Details_For_Approval(objLeaveRequestBo, HF_TBLTYPE.Value.ToString().Trim());
                                ViewState["LID"] = id.ToString().Trim();
                                ViewState["LPKEY"] = sPkey.ToString().Trim();
                                GV_AppDashboardDetails.Visible = true;
                                GridViewAppDetails.DataSource = null;
                                GridViewAppDetails.DataBind();
                                 
                                if (objLeaveReqLst.Count > 0)
                                {
                                    GV_AppDashboardDetails.DataSource = objLeaveReqLst;
                                    GV_AppDashboardDetails.DataBind();
                                }
                                else
                                {
                                    GVNodata(GV_AppDashboardDetails);
                                }
                                break;

                            case "Attendance Request":
                                 grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                grdappRecordTime.Visible = false;
                                RWTdiv.Visible = false; RWTdivbtn.Visible = false; GridViewAppDetails.Visible = false;
                                txtRWComments.Text = string.Empty;
                                lblValidateRWCommnets.Text = string.Empty;
                                TblRemarks.Visible = false;
                                lblRemarksRWT.Text = string.Empty;
                                lblRemarksRWT.Visible = false;
                                leaverequestbo objLeaveRequestBoA = new leaverequestbo();
                                objLeaveRequestBoA.APPROVED_BY_NAME = sRole;
                                objLeaveRequestBoA.PKEY = sPkey;
                                objLeaveRequestBoA.LEAVE_REQ_ID = id;
                                pidashboardbl objLeaveRequestBlA = new pidashboardbl();
                                leaverequestcollectionbo objLeaveReqLstA = objLeaveRequestBlA.Get_LeaveRequest_Details_For_Approval(objLeaveRequestBoA, HF_TBLTYPE.Value.ToString().Trim());
                                 ViewState["AID"] = id.ToString().Trim();
                                ViewState["AtPKEY"] = sPkey.ToString().Trim();
                                GridViewAppDetails.DataSource = null;
                                GridViewAppDetails.DataBind();
                                GV_AppDashboardDetails.Visible = true;
                                if (objLeaveReqLstA.Count > 0)
                                {
                                    GV_AppDashboardDetails.DataSource = objLeaveReqLstA;
                                    GV_AppDashboardDetails.DataBind();
                                }
                                else
                                {
                                    GVNodata(GV_AppDashboardDetails);
                                }
                                break;

                            case "Recordworking Time Details":
                                TblRemarks.Visible = false;
                                lblRemarksRWT.Text = string.Empty; GridViewAppDetails.Visible = false;
                                lblRemarksRWT.Visible = false;
                                RWTdiv.Visible = true;
                                RWTdivbtn.Visible = true;
                                txtRWComments.Text = string.Empty;
                                lblValidateRWCommnets.Text = string.Empty;
                                btnRWApprove.Visible = true;
                                btnRWReject.Visible = true;
                                grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                grdappRecordTime.Visible = true;

                                GridViewAppDetails.DataSource = null;
                                GridViewAppDetails.DataBind();
                                GV_AppDashboardDetails.DataSource = null;
                                GV_AppDashboardDetails.DataBind();
                                RecordWorkingDetailsControlStatus(true);
                                wtrecordworkingtimebo objRecordBo = new wtrecordworkingtimebo();
                                objRecordBo.PKEY = sPkey;
                                objRecordBo.ISAPPROVED = false;
                                objRecordBo.APPROVEDBY = sRole;
                                objRecordBo.COMMENTS = Session["CompCode"].ToString();
                                wtrecordworkingtimecollectionbo objRecordLst = objPIDashBl.Get_RecordDetails_For_Approval(Session["CompCode"].ToString(), objRecordBo);
                                LoadRecordWorkingApp(objRecordLst, objRecordBo);
                                Session.Add("sRWkey", objRecordBo.PKEY);
                                txtRWComments.Text = "";
                                break;



                            default:

                                break;
                        }
                    }
                    catch (Exception ex)
                    { }

                    break;
                default:
                    break;
            } MPE_AppPending.Show();
        }

        protected void LoadRecordWorkingApp(wtrecordworkingtimecollectionbo objLstOne, wtrecordworkingtimebo objBo)
        {
            DataTable CurrentTable = CreateTable();
            CurrentTable = ConvertToDataRow(objLstOne);
            ViewState["CurrentTable"] = CurrentTable;

            grdappRecordTime.DataSource = CurrentTable;
            grdappRecordTime.DataBind();

            SetRemoveDatasapp();
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
            grdappRecordTime.HeaderRow.Cells[5].Text = "SUN ," + dtStartDate.AddDays(0).ToString("d-MMM-yyyy");
            grdappRecordTime.HeaderRow.Cells[6].Text = "MON ," + dtStartDate.AddDays(1).ToString("d-MMM-yyyy");
            grdappRecordTime.HeaderRow.Cells[7].Text = "TUE ," + dtStartDate.AddDays(2).ToString("d-MMM-yyyy");
            grdappRecordTime.HeaderRow.Cells[8].Text = "WED ," + dtStartDate.AddDays(3).ToString("d-MMM-yyyy");
            grdappRecordTime.HeaderRow.Cells[9].Text = "THU ," + dtStartDate.AddDays(4).ToString("d-MMM-yyyy");
            grdappRecordTime.HeaderRow.Cells[10].Text = "FRI ," + dtStartDate.AddDays(5).ToString("d-MMM-yyyy");
            grdappRecordTime.HeaderRow.Cells[11].Text = "SAT ," + dtStartDate.AddDays(6).ToString("d-MMM-yyyy");

        }

        private void SetRemoveDatasapp()
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
                        Label lblCostCenter = (Label)grdappRecordTime.Rows[i].FindControl("lblCostCenter");
                        Label lblOrder = (Label)grdappRecordTime.Rows[i].FindControl("lblOrder");
                        Label boxStaffGrade = (Label)grdappRecordTime.Rows[i].FindControl("txtStaffGrade");
                        Label boxTotal = (Label)grdappRecordTime.Rows[i].FindControl("txtTotal");
                        Label boxSUN = (Label)grdappRecordTime.Rows[i].FindControl("txtSUN");
                        Label boxMON = (Label)grdappRecordTime.Rows[i].FindControl("txtMON");
                        Label boxTUE = (Label)grdappRecordTime.Rows[i].FindControl("txtTUE");
                        Label boxWED = (Label)grdappRecordTime.Rows[i].FindControl("txtWED");
                        Label boxTHU = (Label)grdappRecordTime.Rows[i].FindControl("txtTHU");
                        Label boxFRI = (Label)grdappRecordTime.Rows[i].FindControl("txtFRI");
                        Label boxSAT = (Label)grdappRecordTime.Rows[i].FindControl("txtSAT");
                        Label boxRemarks = (Label)grdappRecordTime.Rows[i].FindControl("txtREMARKS");
                        Label lblHours = ((Label)grdappRecordTime.FooterRow.FindControl("lblHours"));
                        Label lblSun = ((Label)grdappRecordTime.FooterRow.FindControl("lblSun"));
                        Label lblMon = ((Label)grdappRecordTime.FooterRow.FindControl("lblMon"));
                        Label lblTues = ((Label)grdappRecordTime.FooterRow.FindControl("lblTues"));
                        Label lblWed = ((Label)grdappRecordTime.FooterRow.FindControl("lblWed"));
                        Label lblThu = ((Label)grdappRecordTime.FooterRow.FindControl("lblThu"));
                        Label lblFri = ((Label)grdappRecordTime.FooterRow.FindControl("lblFri"));
                        Label lblSAt = ((Label)grdappRecordTime.FooterRow.FindControl("lblSAt"));
                        Label lblRemarks = ((Label)grdappRecordTime.FooterRow.FindControl("lblREMARKS"));
                        Label ddlCostCenter = (Label)grdappRecordTime.Rows[rowIndex].FindControl("drpdwnCostCenter");


                        Label ddlOrder = (Label)grdappRecordTime.Rows[rowIndex].FindControl("drpdwnOrder");
                        Label ddl1 = (Label)grdappRecordTime.Rows[rowIndex].FindControl("drpdwnAttabsType");

                        if (i < dt.Rows.Count)
                        {

                            //Assign the value from DataTable to the TextBox 
                            ddlCostCenter.Text = dt.Rows[i]["CostCenter"].ToString();
                            ddlOrder.Text = dt.Rows[i]["Order"].ToString();
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
        
        
        protected void grdAppPending_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {

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

                grdAppPending.DataSource = objPIDashBoardLst;
                grdAppPending.DataBind();

                Session.Add("grdLst", objPIDashBoardLst);
                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_AppDashboardDetails.DataBind();
                GridViewAppDetails.DataBind();
                grdappRecordTime.DataBind();
                RWTdiv.Visible = false;
                txtRWComments.Text = string.Empty;
                lblValidateRWCommnets.Text = string.Empty;
                TblRemarks.Visible = false;
                lblRemarksRWT.Text = string.Empty;
                lblRemarksRWT.Visible = false;

            }

            catch (Exception ex)
            { }
        }


        protected void lnk_repeAppPending_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = PendingPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                ViewState["PendingPageIndex"] = pageIndex;
                GetHRPernr();

                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_AppDashboardDetails.DataBind();
                GridViewAppDetails.DataBind();
                grdappRecordTime.DataBind();
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



        protected void lnkbtnAppPending_Click(object sender, EventArgs e)
        {
            lnkbtnAppPending.Attributes.Add("class", "nav-link  p-2 active");
            lnkbtnAppComplt.Attributes.Add("class", "nav-link  p-2 ");
            pendingApp.Attributes.Add("class", " show active");
            pendingApp.Visible = true;
            completedApp.Visible = false;
            HFmethdLoader.Value = "3";
            HFsortId.Value = "1";
            btnAppPendToday.Attributes.Add("class", "btn btn-xs btn-secondary");
            btnAppPendYsterday.Attributes.Add("class", "btn btn-xs btn-light");
            btnAppPendTwodayb4.Attributes.Add("class", "btn btn-xs btn-light");
            LoadGridDetails(DDL_AppHLP.SelectedValue.ToString().Trim());
        }
        //---------------------------------------------------PENDING APPROVAL------------------- END-----------------------------

        //----------------------------------------------COMPLETED APPROVAL-----------------------START-----------------------

        protected void LoadCompletedGridDetails(string seltypec)
        {
            msassignedtomebo objAssginTMBo = new msassignedtomebo();
            msassignedtomebl objAssginTMBl = new msassignedtomebl();
            objAssginTMBo.PERNR = User.Identity.Name;
            objAssginTMBo.PageSize = PagerSz;
            objAssginTMBo.PageIndex = CompleatedPageIndex;
            objAssginTMBo.Flag = 1;
            int? RecCount = 0;
            msassignedtomecollectionbo objAssginTMCompletedLst = objAssginTMBl.Load_Completed_Approvals_dashb(Session["CompCode"].ToString(), objAssginTMBo, seltypec, DDLYear.SelectedValue.ToString().Trim(), ref RecCount, Convert.ToInt32(HFsortId.Value));
            if (objAssginTMCompletedLst.Count <= 0)
            {
                //lblMessageBoard.Text = GetLocalResourceObject("NoCompletedRecord").ToString();
                //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
            }
            grdAppCompleted.DataSource = objAssginTMCompletedLst;
            grdAppCompleted.DataBind();


            string frow = "", lrow = "";
            foreach (GridViewRow row in grdAppCompleted.Rows)
            {
                for (int i = 0; i < grdAppCompleted.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        frow = grdAppCompleted.Rows[i].Cells[0].Text;
                    }
                    if (i == grdAppCompleted.Rows.Count - 1)
                    {
                        lrow = grdAppCompleted.Rows[i].Cells[0].Text;
                    }
                }
            }
            PopulatePendingPager(objAssginTMCompletedLst.Count > 0 ? int.Parse(RecCount.ToString()) : 0, CompleatedPageIndex, repeAppCompl);
            divNoofEntriesappComplted.InnerHtml = objAssginTMCompletedLst.Count > 0 ? "Showing " + frow + " to " + lrow + " of " + RecCount + " entries" : "";
            divNoofEntriesappCompltedrow.Visible = objAssginTMCompletedLst.Count > 0 ? true : false;
            Session.Add("grdCmpltdLst", objAssginTMCompletedLst);
        }

        protected void grdAppCompleted_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void grdAppCompleted_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdAppCompleted.PageIndex = e.NewPageIndex;
            msassignedtomecollectionbo objPIDashBoardCmpltdLst = (msassignedtomecollectionbo)Session["grdCmpltdLst"];
            grdAppCompleted.DataSource = objPIDashBoardCmpltdLst;
            int rselectedindex = Convert.ToInt32(ViewState["indexchang"]);
            int pagindex = Convert.ToInt32(ViewState["pageindex"]);
            grdAppCompleted.DataSource = objPIDashBoardCmpltdLst;
            grdAppCompleted.SelectedIndex = -1;
            grdAppCompleted.DataBind();
            if (pageindex == pagindex)
            {
                grdAppCompleted.SelectedIndex = rselectedindex;
            }
        }

        protected void grdAppCompleted_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "VIEW":
                    int rowIndex = Convert.ToInt32(e.CommandArgument);                    
                    pidashboardbl objPIDashBl = new pidashboardbl();
                    GV_AppDashboardDetails.Visible = false;
                    GV_DashboardCompleatedDetails.Visible = false;
                    GridViewRow grdRow = grdAppCompleted.Rows[rowIndex];//SelectedRow;
                    Session.Add("currentSelectedRow", grdRow);
                    int flag = 1;
                    string sName = grdAppCompleted.DataKeys[grdRow.RowIndex]["ENAME"].ToString();
                    string sPernr = grdAppCompleted.DataKeys[grdRow.RowIndex]["PERNR"].ToString();
                    string sPkey = grdAppCompleted.DataKeys[grdRow.RowIndex]["PKEY"].ToString();
                    string sApprovalType = grdAppCompleted.DataKeys[grdRow.RowIndex]["CHANGE_APPROVAL"].ToString();
                    DateTime dtLateDate = DateTime.Parse(grdAppCompleted.DataKeys[grdRow.RowIndex]["LAST_ACTIVITY_DATE"].ToString());
                    string sRole = grdAppCompleted.DataKeys[grdRow.RowIndex]["PLSXT"].ToString();
                    int id = int.Parse(grdAppCompleted.DataKeys[grdRow.RowIndex]["ID"].ToString());
                    string TblTyp = grdAppCompleted.DataKeys[grdRow.RowIndex]["TableTyp"].ToString();
                    string sts = grdAppCompleted.DataKeys[grdRow.RowIndex]["REVIEW"].ToString();
                    DateTime dtLateActDate = DateTime.Parse(grdAppCompleted.DataKeys[grdRow.RowIndex]["LAST_ACTIVITY_DATE"].ToString());
                    DateTime ModifiedDate = DateTime.Parse(grdAppCompleted.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString());
                    HF_TBLTYPE.Value = TblTyp.ToString().Trim();
                    HF_ID.Value = id.ToString().Trim();
                    HF_PKEY.Value = sPkey.ToString().Trim();
                    string strRecipientsPhn = string.Empty;
                    RWTdivbtn.Visible = false;

                    try
                    {
                        GridViewAppDetails.Visible = false;
                        switch (sApprovalType)
                        {
                            case "Address Change Approval":

                                grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                RWTdiv.Visible = false;
                                RWTdivbtn.Visible = false;
                                grdappRecordTime.Visible = false;
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
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GV_DashboardCompleatedDetails.DataSource = objPIAddBoLst;
                                    GV_DashboardCompleatedDetails.DataBind();
                                }
                                else
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GVNodata(GV_DashboardCompleatedDetails);
                                }
                                break;


                            case "Communication Change Approval":
                                 grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                RWTdiv.Visible = false; 
                                RWTdivbtn.Visible = false;
                                grdappRecordTime.Visible = false;
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
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GV_DashboardCompleatedDetails.DataSource = objCommuInfoLst;
                                    GV_DashboardCompleatedDetails.DataBind();
                                }
                                else
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GVNodata(GV_DashboardCompleatedDetails);
                                }

                                break;

                            case "Personal Data Change Approval":

                                grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                RWTdiv.Visible = false; 
                                RWTdivbtn.Visible = false; 
                                grdappRecordTime.Visible = false;
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
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GV_DashboardCompleatedDetails.DataSource = objPersonaldataList;
                                    GV_DashboardCompleatedDetails.DataBind();
                                }
                                else
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GVNodata(GV_DashboardCompleatedDetails);
                                }



                                break;


                            case "Personal ID Change Approval":

                                grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                RWTdiv.Visible = false;
                                RWTdivbtn.Visible = false;
                                grdappRecordTime.Visible = false;
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
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GV_DashboardCompleatedDetails.DataSource = objPersonalIDsLst;
                                    GV_DashboardCompleatedDetails.DataBind();
                                }
                                else
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GVNodata(GV_DashboardCompleatedDetails);
                                }

                                break;

                            case "Family Members change approval":
                                  grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                RWTdiv.Visible = false; 
                                RWTdivbtn.Visible = false; 
                                grdappRecordTime.Visible = false;
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
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GV_DashboardCompleatedDetails.DataSource = objFamilylst;
                                    GV_DashboardCompleatedDetails.DataBind();
                                }
                                else
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GVNodata(GV_DashboardCompleatedDetails);
                                }
                                break;

                            case "Deletion Request Approval":
                                 grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                RWTdiv.Visible = false; 
                                RWTdivbtn.Visible = false; 
                                grdappRecordTime.Visible = false;
                                txtRWComments.Text = string.Empty;
                                lblValidateRWCommnets.Text = string.Empty;
                                TblRemarks.Visible = false;
                                lblRemarksRWT.Text = string.Empty;
                                lblRemarksRWT.Visible = false;
                                GV_DashboardCompleatedDetails.Visible = true;
                                leaverequestbo objLeaveRequestBoD = new leaverequestbo();
                                objLeaveRequestBoD.PKEY = sPkey;
                                objLeaveRequestBoD.LEAVE_REQ_ID = id;
                                string TblTypD = grdAppCompleted.DataKeys[grdRow.RowIndex]["TableTyp"].ToString();
                                pidashboardbl objLeaveRequestBlD = new pidashboardbl();

                                leaverequestcollectionbo objLeaveReqLstD = objLeaveRequestBlD.Get_DeletionRequest_Details_For_Approval_For_Employee(objLeaveRequestBoD, TblTypD);

                                if (objLeaveReqLstD.Count > 0)
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GV_DashboardCompleatedDetails.DataSource = objLeaveReqLstD;
                                    GV_DashboardCompleatedDetails.DataBind();
                                }
                                else
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GVNodata(GV_DashboardCompleatedDetails);
                                }


                                break;


                            case "Leave Request Approval":
                                grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                RWTdiv.Visible = false; 
                                RWTdivbtn.Visible = false; 
                                grdappRecordTime.Visible = false;
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
                                pidashboardbl objLeaveRequestBl = new pidashboardbl();
                                leaverequestcollectionbo objLeaveReqLst = objLeaveRequestBl.Get_LeaveRequest_Details_For_Approval(objLeaveRequestBo, HF_TBLTYPE.Value.ToString().Trim());

                                if (objLeaveReqLst.Count > 0)
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GV_DashboardCompleatedDetails.DataSource = objLeaveReqLst;
                                    GV_DashboardCompleatedDetails.DataBind();
                                }
                                else
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GVNodata(GV_DashboardCompleatedDetails);
                                }

                                break;


                            case "Attendance Request Approval":
                                 grdappRecordTime.DataSource = null;
                                grdappRecordTime.DataBind();
                                RWTdiv.Visible = false; RWTdivbtn.Visible = false;
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
                                grdappRecordTime.Visible = false;
                                pidashboardbl objLeaveRequestBlA = new pidashboardbl();
                                leaverequestcollectionbo objLeaveReqLstA = objLeaveRequestBlA.Get_LeaveRequest_Details_For_Approval(objLeaveRequestBoA, HF_TBLTYPE.Value.ToString().Trim());

                                if (objLeaveReqLstA.Count > 0)
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GV_DashboardCompleatedDetails.DataSource = objLeaveReqLstA;
                                    GV_DashboardCompleatedDetails.DataBind();
                                }
                                else
                                {
                                    GV_AppDashboardDetails.DataSource = null;
                                    GV_AppDashboardDetails.DataBind();
                                    GVNodata(GV_DashboardCompleatedDetails);
                                }


                                break;



                            case "Recordworking Time Details":

                                GridViewDetails.DataSource = null;
                                GridViewDetails.DataBind();
                                GV_DashboardCompleatedDetails.DataSource=null;
                                GV_DashboardCompleatedDetails.DataBind();
                                GV_AppDashboardDetails.DataSource =null;
                                GV_AppDashboardDetails.DataBind();
                                 grdappRecordTime.DataSource=null;
                                grdappRecordTime.DataBind();
                                RWTdiv.Visible = false;
                                txtRWComments.Text = string.Empty;
                                lblValidateRWCommnets.Text = string.Empty;
                                TblRemarks.Visible = true;
                                lblRemarksRWT.Text = string.Empty;
                                lblRemarksRWT.Visible = true;
                                RecordWorkingDetailsControlStatus(false);
                                wtrecordworkingtimebo objRecordBo = new wtrecordworkingtimebo();
                                objRecordBo.PKEY = sPkey;
                                objRecordBo.ISAPPROVED = true;
                                objRecordBo.APPROVEDBY = sRole;
                                grdappRecordTime.Visible = true;
                                objRecordBo.COMMENTS = Session["CompCode"].ToString();
                                wtrecordworkingtimecollectionbo objRecordLst = objPIDashBl.Get_RecordDetails_For_Approval(Session["CompCode"].ToString(), objRecordBo);
                                wtrecordworkingtimebo objRWBo = objRecordLst.Find(delegate(wtrecordworkingtimebo obj)
                                { return true; });
                                LoadRecordWorkingApp(objRecordLst, objRecordBo);
                                lblRemarksRWT.Text = objRWBo.COMMENTS;
                                break;


                            default:

                                break;
                        }
                    }
                    catch (Exception)
                    { }

                    break;
                default:
                    break;
            }

            MPE_AppPending.Show();

        }


        protected void grdAppCompleted_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {

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


                grdAppCompleted.DataSource = objPIDashBoardCmpltdLst;
                grdAppCompleted.DataBind();

                Session.Add("grdCmpltdLst", objPIDashBoardCmpltdLst);
                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_AppDashboardDetails.DataBind();
                GridViewAppDetails.DataBind();
                grdappRecordTime.DataBind();
                RWTdiv.Visible = false;
                txtRWComments.Text = string.Empty;
                lblValidateRWCommnets.Text = string.Empty;
                TblRemarks.Visible = false;
                lblRemarksRWT.Text = string.Empty;
                lblRemarksRWT.Visible = false;
            }
            catch (Exception ex)
            { }
        }

        protected void DDLYearcomp_SelectedIndexChanged(object sender, EventArgs e)
        {
            divsortfilterUcompd.Visible = DDLYearcomp.SelectedValue.ToString().Trim() == DateTime.Now.Year.ToString().Trim() ? true : false;
            LoadCompletedGridDetails();
            GridViewDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;

            GV_DashboardDetails.DataBind();
            GridViewDetails.DataBind();
            grdRecordTime.DataBind();
            tblRWT.Visible = false;
        }


        protected void lnkbtnAppComplt_Click(object sender, EventArgs e)
        {
            lnkbtnAppComplt.Attributes.Add("class", "nav-link  p-2 active");
            lnkbtnAppPending.Attributes.Add("class", "nav-link  p-2 ");
            completedApp.Attributes.Add("class", " show active");
            pendingApp.Visible = false;
            completedApp.Visible = true;
            HFmethdLoader.Value = "4";
            HFsortId.Value = "1";
            btnAppCmpToday.Attributes.Add("class", "btn btn-xs btn-secondary");
            btnAppCmpYesterday.Attributes.Add("class", "btn btn-xs btn-light");
            btnAppCmpTwodayb4.Attributes.Add("class", "btn btn-xs btn-light");
            LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString());
        }



        protected void lnk_repeAppCompl_Click(object sender, EventArgs e)
        {
            try
            {
                //protected int CompleatedPageIndex = 1;
                int pageIndex = CompleatedPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                ViewState["PendingPageIndex_Compleated"] = pageIndex;
                //LoadCompletedGridDetails();
                GetHRPernr();
                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                GV_DashboardCompleatedDetails.DataBind();
                GV_AppDashboardDetails.DataBind();
                GridViewAppDetails.DataBind();
                grdappRecordTime.DataBind();
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


        //-------------------------------------------------------------COMPLETED APPROVAL ------------------------END----------------------------


//---------------------------------------------------------------ME AS APPROVER-------------------------------END----------------------------------------------


       
        void BindYearDropdown()
        {
            try
            {
                for (int i = -2; i < 1; i++)
                {

                    string date = DateTime.Now.AddYears(i).ToString("yyyy");
                    DDLYear.Items.Add(date);
                    DDLYearcomp.Items.Add(date);
                    DDL_APPYear.Items.Add(date);
                    DDL_YearAppP.Items.Add(date);

                }
                DDLYear.SelectedValue = DateTime.Now.AddYears(0).ToString("yyyy");
                DDLYearcomp.SelectedValue = DateTime.Now.AddYears(0).ToString("yyyy");
                DDL_APPYear.SelectedValue = DateTime.Now.AddYears(0).ToString("yyyy");
                DDL_YearAppP.SelectedValue = DateTime.Now.AddYears(0).ToString("yyyy");
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "')", true); }
        }       


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

        protected void GetHRPernr()
        {
            msassignedtomebo objAssginTMBo = new msassignedtomebo();
            msassignedtomebl objAssginTMBl = new msassignedtomebl();


            msassignedtomecollectionbo objAssginTMBolist = objAssginTMBl.Get_HRPERNR(User.Identity.Name.ToString().Trim(), Session["CompCode"].ToString().ToLower().Trim());
            if (objAssginTMBolist.Count > 0)
            {
                foreach (msassignedtomebo objBo in objAssginTMBolist)
                {
                    ViewState["LOGINPERNR"] = objBo.PERNR;
                }

                LoadGridDetails(DDL_AppHLP.SelectedValue.ToString().Trim());
                LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('HR Admin information is not maintained in iEmpPower!');", true);
            }
        }

        protected void DDL_APPYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            filerSrtAppCompd.Visible = DDL_APPYear.SelectedValue.ToString().Trim() == DateTime.Now.Year.ToString().Trim() ? true : false;
            LoadCompletedGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());
            //GetHRPernr();
            GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_AppDashboardDetails.DataBind();
            GridViewAppDetails.DataBind();
            grdappRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;
        }


        protected void DDL_HRTabSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridDetails(DDL_HRTabSel.SelectedValue.ToString().Trim());
            GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_AppDashboardDetails.DataBind();
            GridViewAppDetails.DataBind();
            grdappRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;
        }


        protected void DDL_YearAppP_SelectedIndexChanged(object sender, EventArgs e)
        {
            filsortappP.Visible = DDL_YearAppP.SelectedValue.ToString().Trim() == DateTime.Now.Year.ToString().Trim() ? true : false;
            LoadGridDetails(DDL_AppHLP.SelectedValue.ToString().Trim());
            //GetHRPernr();
            GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_AppDashboardDetails.DataBind();
            GridViewAppDetails.DataBind();
            grdappRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;
        }

        protected void DDL_AppHLP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridDetails(DDL_AppHLP.SelectedValue.ToString().Trim());
            GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
            GV_DashboardCompleatedDetails.DataBind();
            GV_AppDashboardDetails.DataBind();
            GridViewAppDetails.DataBind();
            grdappRecordTime.DataBind();
            RWTdiv.Visible = false;
            txtRWComments.Text = string.Empty;
            lblValidateRWCommnets.Text = string.Empty;
            TblRemarks.Visible = false;
            lblRemarksRWT.Text = string.Empty;
            lblRemarksRWT.Visible = false;
        }

        protected void DDLYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                divsortfilterUPend.Visible = DDLYear.SelectedValue.ToString().Trim() == DateTime.Now.Year.ToString().Trim() ? true : false;
                LoadGridDetails();
                GridViewDetails.DataSource = GV_DashboardDetails.DataSource = grdRecordTime.DataSource = null;
                GV_DashboardDetails.DataBind();
                GridViewDetails.DataBind();
                grdRecordTime.DataBind();
                tblRWT.Visible = false;

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        private void GVNodata(GridView GV)
        {
            try
            {

                DataTable Dt = new DataTable();
                Dt.Columns.Add("Slno", typeof(string));
                Dt.Columns.Add("TEXT", typeof(int));
                Dt.Columns.Add("VALUE", typeof(string));
                Dt.Rows.Add(Dt.NewRow());
                GV.DataSource = Dt;
                GV.DataBind();
                GV.Rows[0].Visible = false;
                GV.Rows[0].Controls.Clear();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
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


                        Label ddlOrder = (Label)grdRecordTime.Rows[rowIndex].FindControl("drpdwnOrder");
                        Label ddl1 = (Label)grdRecordTime.Rows[rowIndex].FindControl("drpdwnAttabsType");
                        if (i < dt.Rows.Count)
                        {

                            //Assign the value from DataTable to the TextBox 
                            ddlCostCenter.Text = dt.Rows[i]["CostCenter"].ToString();
                            ddlOrder.Text = dt.Rows[i]["Order"].ToString();
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

        protected void GridViewAppDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "VIEW":
                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    int flag = 2;
                    GridViewRow grdRow = GridViewAppDetails.Rows[rowIndex];//SelectedRow;SelectedRow;
                    string sPkey = GridViewAppDetails.DataKeys[grdRow.RowIndex]["PKEY"].ToString();
                    int id = int.Parse(GridViewAppDetails.DataKeys[grdRow.RowIndex]["ID"].ToString());
                    string sApprovalType = GridViewAppDetails.DataKeys[grdRow.RowIndex]["CHANGE_APPROVAL"].ToString();
                    string statustype = GridViewAppDetails.DataKeys[grdRow.RowIndex]["STATUS"].ToString().Trim();

                    ViewState["MODON"] = GridViewAppDetails.DataKeys[grdRow.RowIndex]["MODON"].ToString().Trim();

                    ViewState["MODIFDON"] = GridViewAppDetails.DataKeys[grdRow.RowIndex]["MODIFIEDON"].ToString().Trim();

                    pidashboardbl objPIDashBl = new pidashboardbl();
                    try
                    {
                        switch (sApprovalType)
                        {
                            case "Address change approval":

                                GridViewAppDetails.Visible = true;
                                GV_AppDashboardDetails.Visible = true;
                                piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                                // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                                objPIAddBo.PKEY = sPkey;
                                objPIAddBo.ID = id;
                                objPIAddBo.STATUS = statustype;
                                pidashboardbl objPIDashbl = new pidashboardbl();
                                piaddressinformationcollectionbo objPIAddBoLst = objPIDashbl.Get_Address_Details_For_Approval(objPIAddBo, flag);
                                if (objPIAddBoLst.Count > 0)
                                {
                                    GV_AppDashboardDetails.DataSource = objPIAddBoLst;
                                    GV_AppDashboardDetails.DataBind();

                                    using (Button BtnApp = (Button)GV_AppDashboardDetails.FooterRow.FindControl("BtnGvReqApprove"))
                                    using (Button BtnRej = (Button)GV_AppDashboardDetails.FooterRow.FindControl("BtnGvReqReject"))
                                    using (TextBox TxtRemaeks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    using (RequiredFieldValidator RFVRemarks = (RequiredFieldValidator)GV_AppDashboardDetails.FooterRow.FindControl("RFV_TxtGvRemarks"))
                                    {
                                        if (statustype == "Approved")
                                        {
                                            GV_AppDashboardDetails.ShowFooter = false;
                                            BtnApp.Visible = BtnApp.Enabled = false;
                                            BtnRej.Visible = BtnRej.Enabled = false;
                                            TxtRemaeks.Visible = TxtRemaeks.Enabled = false;
                                            RFVRemarks.Visible = RFVRemarks.Enabled = false;
                                        }

                                        else if (statustype == "Updated")
                                        {
                                            GV_AppDashboardDetails.ShowFooter = true;
                                            BtnApp.Visible = BtnApp.Enabled = true;
                                            BtnRej.Visible = BtnRej.Enabled = true;
                                            TxtRemaeks.Visible = TxtRemaeks.Enabled = true;
                                            RFVRemarks.Visible = RFVRemarks.Enabled = true;
                                        }
                                    }
                                    GV_AppDashboardDetails.DataSource = objPIAddBoLst;
                                    GV_AppDashboardDetails.DataBind();
                                }
                                else
                                {
                                    GVNodata(GV_AppDashboardDetails);
                                }

                                break;


                            case "Communication change approval":

                                GridViewAppDetails.Visible = true;
                                GV_AppDashboardDetails.Visible = true;
                                picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                                // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                                objCommuInfoBo.PKEY = sPkey;
                                objCommuInfoBo.ID = id;
                                objCommuInfoBo.STATUS = statustype;
                                pidashboardbl objPIDashblC = new pidashboardbl();
                                picommunicationinformationcollectionbo objCommuInfoLst = objPIDashblC.Get_Communication_Details_For_Approval(objCommuInfoBo, flag);
                                if (objCommuInfoLst.Count > 0)
                                {
                                    GV_AppDashboardDetails.DataSource = objCommuInfoLst;
                                    GV_AppDashboardDetails.DataBind();

                                    using (Button BtnApp = (Button)GV_AppDashboardDetails.FooterRow.FindControl("BtnGvReqApprove"))
                                    using (Button BtnRej = (Button)GV_AppDashboardDetails.FooterRow.FindControl("BtnGvReqReject"))
                                    using (TextBox TxtRemaeks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    using (RequiredFieldValidator RFVRemarks = (RequiredFieldValidator)GV_AppDashboardDetails.FooterRow.FindControl("RFV_TxtGvRemarks"))
                                    {
                                        if (statustype == "Approved")
                                        {
                                            GV_AppDashboardDetails.ShowFooter = false;
                                            BtnApp.Visible = BtnApp.Enabled = false;
                                            BtnRej.Visible = BtnRej.Enabled = false;
                                            TxtRemaeks.Visible = TxtRemaeks.Enabled = false;
                                            RFVRemarks.Visible = RFVRemarks.Enabled = false;
                                        }

                                        else if (statustype == "Updated")
                                        {
                                            GV_AppDashboardDetails.ShowFooter = true;
                                            BtnApp.Visible = BtnApp.Enabled = true;
                                            BtnRej.Visible = BtnRej.Enabled = true;
                                            TxtRemaeks.Visible = TxtRemaeks.Enabled = true;
                                            RFVRemarks.Visible = RFVRemarks.Enabled = true;
                                        }
                                    }
                                    GV_AppDashboardDetails.DataSource = objCommuInfoLst;
                                    GV_AppDashboardDetails.DataBind();
                                }
                                else
                                {
                                    GVNodata(GV_AppDashboardDetails);
                                }

                                break;

                            case "Personal Data change approval":

                                GridViewAppDetails.Visible = true;
                                GV_AppDashboardDetails.Visible = true;
                                personaldatabo objPersonaldataBo = new personaldatabo();
                                // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                                objPersonaldataBo.PKEY = sPkey;
                                objPersonaldataBo.ID = id;
                                objPersonaldataBo.STATUS = statustype;
                                pidashboardbl objPIDashblPD = new pidashboardbl();
                                personaldatacollectionbo objPersonaldataList = objPIDashblPD.Get_PersonalData_Details_For_Approval(objPersonaldataBo, flag);
                                if (objPersonaldataList.Count > 0)
                                {
                                    GV_AppDashboardDetails.DataSource = objPersonaldataList;
                                    GV_AppDashboardDetails.DataBind();

                                    using (Button BtnApp = (Button)GV_AppDashboardDetails.FooterRow.FindControl("BtnGvReqApprove"))
                                    using (Button BtnRej = (Button)GV_AppDashboardDetails.FooterRow.FindControl("BtnGvReqReject"))
                                    using (TextBox TxtRemaeks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    using (RequiredFieldValidator RFVRemarks = (RequiredFieldValidator)GV_AppDashboardDetails.FooterRow.FindControl("RFV_TxtGvRemarks"))
                                    {
                                        if (statustype == "Approved")
                                        {
                                            GV_AppDashboardDetails.ShowFooter = false;
                                            BtnApp.Visible = BtnApp.Enabled = false;
                                            BtnRej.Visible = BtnRej.Enabled = false;
                                            TxtRemaeks.Visible = TxtRemaeks.Enabled = false;
                                            RFVRemarks.Visible = RFVRemarks.Enabled = false;
                                        }

                                        else if (statustype == "Updated")
                                        {
                                            GV_AppDashboardDetails.ShowFooter = true;
                                            BtnApp.Visible = BtnApp.Enabled = true;
                                            BtnRej.Visible = BtnRej.Enabled = true;
                                            TxtRemaeks.Visible = TxtRemaeks.Enabled = true;
                                            RFVRemarks.Visible = RFVRemarks.Enabled = true;
                                        }
                                    }
                                    GV_AppDashboardDetails.DataSource = objPersonaldataList;
                                    GV_AppDashboardDetails.DataBind();
                                }
                                else
                                {
                                    GVNodata(GV_AppDashboardDetails);
                                }

                                break;

                            case "Personal ID change approval":

                                GridViewAppDetails.Visible = true;
                                GV_AppDashboardDetails.Visible = true;
                                pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                                // objLeaveRequestBo.APPROVED_BY_NAME = sRole;
                                objPersonalIDsBo.PKEY = sPkey;
                                objPersonalIDsBo.ID = id;
                                objPersonalIDsBo.STATUS = statustype;
                                pidashboardbl objPIDashblPI = new pidashboardbl();
                                pipersonalidscollectionbo objPersonalIDsLst = objPIDashblPI.Get_PersonalIDS_Details_For_Approval(objPersonalIDsBo, flag);
                                if (objPersonalIDsLst.Count > 0)
                                {
                                    GV_AppDashboardDetails.DataSource = objPersonalIDsLst;
                                    GV_AppDashboardDetails.DataBind();

                                    using (Button BtnApp = (Button)GV_AppDashboardDetails.FooterRow.FindControl("BtnGvReqApprove"))
                                    using (Button BtnRej = (Button)GV_AppDashboardDetails.FooterRow.FindControl("BtnGvReqReject"))
                                    using (TextBox TxtRemaeks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    using (RequiredFieldValidator RFVRemarks = (RequiredFieldValidator)GV_AppDashboardDetails.FooterRow.FindControl("RFV_TxtGvRemarks"))
                                    {
                                        if (statustype == "Approved")
                                        {
                                            GV_AppDashboardDetails.ShowFooter = false;
                                            BtnApp.Visible = BtnApp.Enabled = false;
                                            BtnRej.Visible = BtnRej.Enabled = false;
                                            TxtRemaeks.Visible = TxtRemaeks.Enabled = false;
                                            RFVRemarks.Visible = RFVRemarks.Enabled = false;
                                        }

                                        else if (statustype == "Updated")
                                        {
                                            GV_AppDashboardDetails.ShowFooter = true;
                                            BtnApp.Visible = BtnApp.Enabled = true;
                                            BtnRej.Visible = BtnRej.Enabled = true;
                                            TxtRemaeks.Visible = TxtRemaeks.Enabled = true;
                                            RFVRemarks.Visible = RFVRemarks.Enabled = true;
                                        }
                                    }
                                    GV_AppDashboardDetails.DataSource = objPersonalIDsLst;
                                    GV_AppDashboardDetails.DataBind();
                                }
                                else
                                {
                                    GVNodata(GV_AppDashboardDetails);
                                }

                                break;

                            case "Family Members change approval":

                                GridViewAppDetails.Visible = true;
                                GV_AppDashboardDetails.Visible = true;
                                pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                objFamilyBo.PKEY = sPkey;
                                objFamilyBo.ID = id;
                                objFamilyBo.STATUS = statustype;
                                pidashboardbl objPIDashblFM = new pidashboardbl();
                                pifamilymemberscollectionbo objFamilylst = objPIDashblFM.Get_FamilyMemberDetails_For_Approval(objFamilyBo, flag);
                                if (objFamilylst.Count > 0)
                                {
                                    GV_AppDashboardDetails.DataSource = objFamilylst;
                                    GV_AppDashboardDetails.DataBind();

                                    using (Button BtnApp = (Button)GV_AppDashboardDetails.FooterRow.FindControl("BtnGvReqApprove"))
                                    using (Button BtnRej = (Button)GV_AppDashboardDetails.FooterRow.FindControl("BtnGvReqReject"))
                                    using (TextBox TxtRemaeks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
                                    using (RequiredFieldValidator RFVRemarks = (RequiredFieldValidator)GV_AppDashboardDetails.FooterRow.FindControl("RFV_TxtGvRemarks"))
                                    {
                                        if (statustype == "Approved")
                                        {
                                            GV_AppDashboardDetails.ShowFooter = false;
                                            BtnApp.Visible = BtnApp.Enabled = false;
                                            BtnRej.Visible = BtnRej.Enabled = false;
                                            TxtRemaeks.Visible = TxtRemaeks.Enabled = false;
                                            RFVRemarks.Visible = RFVRemarks.Enabled = false;
                                        }

                                        else if (statustype == "Updated")
                                        {
                                            GV_AppDashboardDetails.ShowFooter = true;
                                            BtnApp.Visible = BtnApp.Enabled = true;
                                            BtnRej.Visible = BtnRej.Enabled = true;
                                            TxtRemaeks.Visible = TxtRemaeks.Enabled = true;
                                            RFVRemarks.Visible = RFVRemarks.Enabled = true;
                                        }
                                    }
                                    GV_AppDashboardDetails.DataSource = objFamilylst;
                                    GV_AppDashboardDetails.DataBind();
                                }
                                else
                                {
                                    GVNodata(GV_AppDashboardDetails);
                                }

                                break;
                            default:

                                break;
                        }
                    }
                    catch (Exception ex)
                    {}
                    break;
                default:
                    break;
            }
            MPE_AppPending.Show();
        }


        protected void GV_AppDashboardDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void GridViewAppDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnRWApprove_Click(object sender, EventArgs e)
        {
            bool bIStatus = true;
            Approve_Reject_RecordWorkingDetails(bIStatus);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Approved Successfully !');", true);
            LoadGridDetails(DDL_AppHLP.SelectedValue.ToString().Trim());
        }

        protected void btnRWReject_Click(object sender, EventArgs e)
        {
            bool bIStatus = false;
            Approve_Reject_RecordWorkingDetails(bIStatus);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Rejected Successfully !');", true);
            LoadGridDetails(DDL_AppHLP.SelectedValue.ToString().Trim());
        }

        protected void Approve_Reject_RecordWorkingDetails(bool bIStatus)
        {
            msassignedtomebo objBo = new msassignedtomebo();
            string sIOPkey = (string)Session["sRWkey"];
            string sRole = (string)Session["sRole"];
            objBo.PKEY = sIOPkey;
            objBo.APPROVED_BY = User.Identity.Name;            
            objBo.COMMENTS = txtRWComments.Text.Trim();
            objBo.APPROVED_ON = DateTime.Now;
            objBo.STATUS = bIStatus;            
            objBo.PLSXT = sRole;
            string ccode=Session["CompCode"].ToString().ToLower().Trim();
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

                    pnlRecordWorking.Visible = false;
                    RWTdiv.Visible = false;


                }
            }
            catch (Exception Ex) 
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }



        protected void GV_AppDashboardDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (HF_TBLTYPE != null && HF_ID != null && HF_PKEY != null)
                {
                    if (GV_AppDashboardDetails.Rows.Count > 0)
                    {
                        switch (HF_TBLTYPE.Value.ToString().Trim().ToUpper())
                        {

                            case "PA2001":

                                switch (e.CommandName.ToUpper())
                                {
                                    case "APPROVE": // Flag - 1
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                    objList = objBl.Deletion_LeaveDetails_Mail(ViewState["LPKEY"].ToString().Trim(), int.Parse(ViewState["LID"].ToString().Trim()), "Deletion request approved", "PA2001");

                                                   SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request approved", "PA2001");

                                                }
                                                else
                                                {
                                                    objList = objBl.Approval_LeaveDetails_Mail(ViewState["LPKEY"].ToString().Trim(), int.Parse(ViewState["LID"].ToString().Trim()), "Approved", "PA2001");

                                                 SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim(), "PA2001");
                                                }
                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID = null;
                                                HF_PKEY = null;
                                                HF_STS = null;


                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;

                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Leave Request approved successfully !')", true);
                                            }
                                            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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

                                                if (HF_STS.Value.ToString().Trim().ToUpper() == "DELETION REQUESTED")
                                                {
                                                    objList = objBl.Deletion_LeaveDetails_Mail(ViewState["LPKEY"].ToString().Trim(), int.Parse(ViewState["LID"].ToString().Trim()), "Deletion request rejected", "PA2001");

                                                   SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request rejected", "PA2001");

                                                }
                                                else
                                                {
                                                    objList = objBl.Approval_LeaveDetails_Mail(ViewState["LPKEY"].ToString().Trim(), int.Parse(ViewState["LID"].ToString().Trim()), "Rejected", "PA2001");

                                                   SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim(), "PA2001");
                                                }

                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID = null;
                                                HF_PKEY = null;
                                                HF_STS = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;

                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
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
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                    objList = objBl.Deletion_LeaveDetails_Mail(ViewState["AtPKEY"].ToString().Trim(), int.Parse(ViewState["AID"].ToString().Trim()), "Deletion request approved", "PA2002");

                                                    SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request approved", "PA2002");

                                                }
                                                else
                                                {
                                                    objList = objBl.Approval_LeaveDetails_Mail(ViewState["AtPKEY"].ToString().Trim(), int.Parse(ViewState["AID"].ToString().Trim()), "Approved", "PA2002");

                                                    SendMailLeave(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim(), "PA2002");

                                                }


                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID = null;
                                                HF_PKEY = null;
                                                HF_STS = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Attendance Request approved successfully !')", true);
                                            }
                                            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Invalid Selection !');", true); }
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                if (HF_STS.Value.ToString().Trim().ToUpper() == "DELETION REQUESTED")
                                                {
                                                    objList = objBl.Deletion_LeaveDetails_Mail(ViewState["AtPKEY"].ToString().Trim(), int.Parse(ViewState["AID"].ToString().Trim()), "Deletion request rejected", "PA2002");

                                                    SendMailLeaveDel(objList, ref Supervisor_name, ref Supervisor_Email, ref HR_Email, ref PERNR_Name, ref PERNR_Email, "Deletion request rejected", "PA2002");

                                                }
                                                else
                                                {
                                                    objList = objBl.Approval_LeaveDetails_Mail(ViewState["AtPKEY"].ToString().Trim(), int.Parse(ViewState["AID"].ToString().Trim()), "Rejected", "PA2002");


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
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
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
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                                ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                                piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                                                piaddressinformationbl objPIAddBl = new piaddressinformationbl();

                                                List<piaddressinformationbo> objList = new List<piaddressinformationbo>();
                                                objList = objPIAddBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                                ObjBL.Approval_AddressDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                                SendMailAddress(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim());
                                              
                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID = null;
                                                HF_PKEY = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Address Information approved successfully !')", true);
                                            }
                                            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                                ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());


                                                piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                                                piaddressinformationbl objPIAddBl = new piaddressinformationbl();
                                                List<piaddressinformationbo> objList = new List<piaddressinformationbo>();
                                                objList = objPIAddBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");

                                                ObjBL.Approval_AddressDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                                SendMailAddress(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim());

                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID = null;
                                                HF_PKEY = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
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
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                                ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());


                                                picommunicationinformationbo objPIComBo = new picommunicationinformationbo();
                                                picommunicationinformationbl objPIComBl = new picommunicationinformationbl();
                                                List<picommunicationinformationbo> objList = new List<picommunicationinformationbo>();
                                                objList = objPIComBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                                ObjBL.Approval_CommunticationInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                                SendMailCommunication(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim());

                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID.Value = null;
                                                HF_PKEY = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Communication Information approved successfully !')", true);
                                            }
                                            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                                ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());


                                                picommunicationinformationbo objPIComBo = new picommunicationinformationbo();
                                                picommunicationinformationbl objPIComBl = new picommunicationinformationbl();
                                                List<picommunicationinformationbo> objList = new List<picommunicationinformationbo>();
                                                objList = objPIComBl.Approval_APIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");


                                                ObjBL.Approval_CommunticationInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                                SendMailCommunication(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim());

                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID.Value = null;
                                                HF_PKEY = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
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
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                                ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());


                                                personaldatabo objPIBo = new personaldatabo();
                                                personaldatabl objPIBl = new personaldatabl();
                                                List<personaldatabo> objList = new List<personaldatabo>();
                                                objList = objPIBl.Approval_PDDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");


                                                ObjBL.Approval_PDInfoDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);

                                                SendMailPD(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim());


                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID.Value = null;
                                                HF_PKEY = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal Data Information approved successfully !')", true);
                                            }
                                            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
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
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                                ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                                pipersonalidsbo objPIBo = new pipersonalidsbo();
                                                pipersonalidsbl objPIBl = new pipersonalidsbl();
                                                List<pipersonalidsbo> objList = new List<pipersonalidsbo>();
                                                objList = objPIBl.Approval_PIDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                                ObjBL.Approval_PIDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                                SendMailPIDS(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim());

                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID.Value = null;
                                                HF_PKEY = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Personal ID approved successfully !')", true);
                                            }
                                            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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

                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID.Value = null;
                                                HF_PKEY = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
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
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                                ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                                pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                                pifamilymembersbl objPIFamBl = new pifamilymembersbl();
                                                List<pifamilymembersbo> objList = new List<pifamilymembersbo>();
                                                objList = objPIFamBl.Approval_FMDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Approved");

                                                ObjBL.Approval_FamilykDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                                //SendMailFamily(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Approved", TxtGvRemarks.Text.Trim());

                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID.Value = null;
                                                HF_PKEY = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Family Details approved successfully !')", true);
                                            }
                                            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Selection !');", true); }
                                        }
                                        break;
                                    case "REJECT": // Flag - 2
                                        using (TextBox TxtGvRemarks = (TextBox)GV_AppDashboardDetails.FooterRow.FindControl("TxtGvRemarks"))
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
                                                ObjBO.MODIFIEDON = DateTime.Parse(ViewState["MODIFDON"].ToString());
                                                ObjBO.MODON = DateTime.Parse(ViewState["MODON"].ToString());

                                                pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                                                pifamilymembersbl objPIFamBl = new pifamilymembersbl();
                                                List<pifamilymembersbo> objList = new List<pifamilymembersbo>();
                                                objList = objPIFamBl.Approval_FMDetails_Mail(HF_PKEY.Value.ToString().Trim(), int.Parse(HF_ID.Value.ToString().Trim()), "Rejected");

                                                ObjBL.Approval_FamilykDetails(ObjBO, ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email);
                                               // SendMailFamily(ref Supervisor_name, ref Supervisor_Email, ref PERNR_Name, ref PERNR_Email, "Rejected", TxtGvRemarks.Text.Trim());

                                                
                                                GetHRPernr();
                                                ViewState["PendingPageIndex"] = "0";
                                                HF_TBLTYPE = null;
                                                HF_ID.Value = null;
                                                HF_PKEY = null;
                                                GridViewAppDetails.DataSource = GV_DashboardCompleatedDetails.DataSource = GV_AppDashboardDetails.DataSource = grdappRecordTime.DataSource = null;
                                                GV_DashboardCompleatedDetails.DataBind();
                                                GV_AppDashboardDetails.DataBind();
                                                GridViewAppDetails.DataBind();
                                                grdappRecordTime.DataBind();
                                                RWTdiv.Visible = false;
                                                txtRWComments.Text = string.Empty;
                                                lblValidateRWCommnets.Text = string.Empty;
                                                TblRemarks.Visible = false;
                                                lblRemarksRWT.Text = string.Empty;
                                                lblRemarksRWT.Visible = false;
                                                GV_AppDashboardDetails.DataSource = null;
                                                GV_AppDashboardDetails.DataBind();
                                                GV_AppDashboardDetails.Visible = false;
                                                GridViewAppDetails.DataSource = null;
                                                GridViewAppDetails.DataBind();
                                                GridViewAppDetails.Visible = false;
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


        //-------------------------------------SEND MAILS-------------------------------------------------------

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


                    string strSubject = string.Empty;
                    string strSubject1 = string.Empty;


                    string cc = Session["CompCode"].ToString();
                    string empid = PERNR.ToString().Trim();
                    int ct = cc.Length;
                    empid = empid.Substring(ct).ToUpper();


                    string ccode = Session["CompCode"].ToString();
                    string appid = User.Identity.Name;
                    int cnt = ccode.Length;
                    appid = appid.Substring(cnt).ToUpper();

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
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, appid.ToString().Trim(), strSubject1, strPernr_Mail, body);
                       
                        //email.IsBackground = true;
                        //email.Start();
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
                       
                       iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, appid.ToString().Trim(), strSubject1, strPernr_Mail, body);
                        
                        //email.IsBackground = true;
                        //email.Start();
                    }

                    // body += "</br>" + sw1.ToString() + "<br/>";
                    //    //End of preparing the mail body-------------------------------------------
                    //iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                }


                //break;

            }
            catch (Exception Ex) { }
           // { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
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
                    int ct = cc.Length;
                    empid = empid.Substring(ct).ToUpper();


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
                            iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, approver.ToString().Trim(), strSubject1, strPernr_Mail, body);
                        });

                        email.IsBackground = true;
                        email.Start();
                        ////Newly added Ends
                    }

                    else if (tbltyp.Trim() == "PA2002")
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
                       iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, approver.ToString().Trim(), strSubject, strPernr_Mail, body);
                       
                    }
                }


                //break;

            }
            catch (Exception Ex) { }
            //{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
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


                        //Thread email = new Thread(delegate()
                        //{
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                        //});



                    }

                }

            }
            catch (Exception Ex) { }
            //{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
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
            catch (Exception Ex) { }
            //{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
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

        protected void GV_AppDashboardDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GV_AppDashboardDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GV_AppDashboardDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }       

        protected void GV_AppDashboardDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {


        }

        

        #region GV_DashboardCompleatedDetails Events
        protected void GV_DashboardCompleatedDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GV_DashboardCompleatedDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

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


        protected void grdEmployeeSubOrdinates_RowDataBound(object sender, GridViewRowEventArgs e)
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


        protected void LoadEmployySubOrdinates()
        {
            msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            objPIDashBoardBo.PERNR = User.Identity.Name;
            objPIDashBoardBo.COMMENTS = Session["CompCode"].ToString().Trim().ToLower();
            msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_Manager(objPIDashBoardBo);
            grdEmployeeSubOrdinates.DataSource = objPIDashBoardLst;
            grdEmployeeSubOrdinates.DataBind();
            Session.Add("EmployeeSubordinateList", objPIDashBoardLst);
        }

        protected void grdEmployeeSubOrdinates_Sorting(object sender, GridViewSortEventArgs e)
        {
            msassignedtomecollectionbo objPIDashBoardLst = (msassignedtomecollectionbo)Session["EmployeeSubordinateList"];
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
                        { return ((long.Parse(objBo2.PERNR)).CompareTo(long.Parse(objBo2.PERNR))); });
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
                            { return (objBo1.ENAME.CompareTo(objBo2.ENAME)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo2.ENAME.CompareTo(objBo1.ENAME)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "USRID":
                    if (objSortOrder)
                    {
                        if (objPIDashBoardLst != null)
                        {
                            objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                            { return (objBo1.USRID.CompareTo(objBo2.USRID)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo2.USRID.CompareTo(objBo1.USRID)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
            }


            grdEmployeeSubOrdinates.DataSource = objPIDashBoardLst;
            grdEmployeeSubOrdinates.DataBind();

            Session.Add("EmployeeSubordinateList", objPIDashBoardLst);

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
            { }
        }

       

    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;
using iEmpPower.Old_App_Code.iEmpPowerMaster;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Configuration;

namespace iEmpPower
{
    public partial class Dashboard : System.Web.UI.Page
    {
        bool bSortedOrder = false;
        protected MembershipUser memUser;
        public int rselectedindex = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CompCode"] = "";
                //Session["CompLogin"] = "";
                LoadEmployeeDetails();
            }
            if (User.Identity.IsAuthenticated == false)
            {
                Server.Transfer("~/Account/Login.aspx");
            }

            int parsedValue;

            memUser = Membership.GetUser();
            Session.Add("bSortedOrder", bSortedOrder);
            ViewState.Add("indexchang", rselectedindex);

            BindPendingCount();
            configurationbl objBl = new configurationbl();
            configurationcollectionbo objLst = objBl.Load_EmployeePhotoDetails(memUser.ToString());
            foreach (configurationbo objBo in objLst)
            {
                Session.Add("CompCode", objBo.Company_Code.Trim() == "" ? memUser.ToString() : objBo.Company_Code.Trim());
            }
            //pnlApprvr.Visible = true;
            msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
            msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(Session["CompCode"].ToString(), objPIDashBoardBo);
            if (memUser.ToString().Trim() == "70296")
            {
                PernlInfo.Visible = false;
                dirct.Visible = false;
                CompBlock.Visible = false;
                pnlApprvr.Visible = false;
                EmpBlock.Visible = false;
                //pinfo.Visible = false;
            }
            else if (Session["CompLogin"].ToString() == "1")
            {
                CompBlock.Visible = true;
                pnlApprvr.Visible = false;
                EmpBlock.Visible = false;
            }
            else if (Session["CompLogin"].ToString() == "0")
            {
                CompBlock.Visible = false;
                pnlApprvr.Visible = false;
                EmpBlock.Visible = true;
            }
            else
            {
                
                CompBlock.Visible = false;
                pnlApprvr.Visible = false;
                EmpBlock.Visible = false;
            }
            if (objPIDashBoardLst.Count > 0)
            {
                pnlApprvr.Visible = true;
            }
            else
            {
                PernlInfo.Visible = false;
                pnlApprvr.Visible = false;
            }

            GetHRPERNRS();
            
        }

        public void BindPendingCount()
        {
            string Leavecount = "", RWTcount = "";
            string TTMYQUEUECOUNT = "";
            string MyPendAllCnt = "";
            string MyPendLeavCnt = "";
            string MyPendTMSCnt = "";
            string MyPendingTask = "";
            string RRF_count = "";
            pidashboarddalDataContext objPIDashBoardDataContext = new pidashboarddalDataContext();
            //objPIDashBoardDataContext.Usp_Pending_for_Approval_Count(User.Identity.Name, ref  Leavecount, ref RWTcount, ref TTMYQUEUECOUNT, ref MyPendAllCnt, ref MyPendLeavCnt, ref MyPendTMSCnt,ref MyPendingTask,ref RRF_count);
            //LEAVE.InnerText = "Pending : " + Leavecount;
            //RWT.InnerText = "Pending : " + RWTcount;
            //TTMYQUEUE.InnerText = "Pending Tickets  : " + TTMYQUEUECOUNT;
            //divMyPendingCnt.InnerText = "Pending : " + MyPendAllCnt;
            //TTMYQUEUETask.InnerText = "Pending Tasks : " + MyPendingTask;
            //RRFCont.InnerText = "Pending Req. :" + RRF_count;
        }

        protected void LoadEmployeeDetails()
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

        protected void GetHRPERNRS()
        {
            string sts = "";
            configurationdalDataContext objAnnouncementDataContext = new configurationdalDataContext();
            objAnnouncementDataContext.usp_CheckHR(Session["CompCode"].ToString(), HttpContext.Current.User.Identity.Name, ref sts);
            if (sts.Trim().ToUpper() == "TRUE")
            {
                pnlApprvr.Visible = true;
            }
            //else
            //{
            //    MenuMSS.Visible = false;
            //}

        }
    }
}
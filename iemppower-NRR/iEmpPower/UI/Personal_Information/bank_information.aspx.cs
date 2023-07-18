using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower
{
    public partial class UI_Employee_Performance_bank_information : System.Web.UI.Page
    {
        int BankInfoPageIndex = 1;

        #region Page_Init
        public void Page_Init(object o, EventArgs e)
        {
            try
            {
                if (Session["sEmploreeId"] == null)
                {
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("~/Account/Login.aspx", false);
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    PageLoadEvents();
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #region User Defined Methods
        private void GVNodata(GridView GV, DataTable Dt)
        {
            try
            {
                Dt.Rows.Add(Dt.NewRow());
                GV.DataSource = Dt;
                GV.DataBind();
                GV.Rows[0].Visible = false;
                GV.Rows[0].Controls.Clear();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        private void MsgCls(string Msg, Label Lbl, Color Clr)
        {
            try
            {
                Lbl.Text = string.Empty;
                Lbl.Text = Lbl.Text + Msg;
                Lbl.ForeColor = Clr;
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region Page Load Events
        private void PageLoadEvents()
        {
            try
            {
                MV_BankInfo.SetActiveView(V_ViewBankInfo);
                BindGV_BankInfo(1);
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region Bind GVAddress Information
        private void BindGV_BankInfo(int PageIndex)
        {
            try
            {
                personal_informationBO objBankInfoBo = new personal_informationBO();
                personal_informationBl objBankInfoBl = new personal_informationBl();
                objBankInfoBo.EmpID = User.Identity.Name;
                objBankInfoBo.Comany_Code = Session["CompCode"].ToString(); ;
                objBankInfoBo.flag= 1;
                objBankInfoBo.Approved_By = "";
                personal_informationCollBo objPIAddBoLst = objBankInfoBl.Get_EmpBankInfo(objBankInfoBo);
                if (objPIAddBoLst.Count > 0)
                {
                    MsgCls(string.Empty, LblMsg, Color.White);
                    GV_BankInfo.DataSource = objPIAddBoLst;
                    GV_BankInfo.DataBind();
                    //BankInfoPager(objPIAddBoLst.Count > 0 ? int.Parse(objPIAddBoLst[0].RecordCnt.ToString()) : 0, BankInfoPageIndex);
                    MsgCls(string.Empty, LblMsg, Color.Transparent);
                }
                else
                {
                    //DataTable Dt = new DataTable();
                    //Dt.Columns.Add("RowNumber", typeof(int));
                    //Dt.Columns.Add("ID", typeof(int));
                    //Dt.Columns.Add("PKEY", typeof(string));
                    //Dt.Columns.Add("BANK_TYPE_NAME", typeof(string));
                    //Dt.Columns.Add("COUNTRY_NAME", typeof(string));
                    //Dt.Columns.Add("BANK_ACCOUNT", typeof(string));
                    //Dt.Columns.Add("PAYMENT_METHOD_NAME", typeof(string));
                    //Dt.Columns.Add("PAYMENT_CURRENCY_NAME", typeof(string));
                    //Dt.Columns.Add("RecordCnt", typeof(int));

                    //GVNodata(GV_BankInfo, Dt);
                    MsgCls("No records found !", LblMsg, Color.Red);
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region GV_BankInfo Events

        protected void GV_BankInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Guid BankID = new Guid();
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
                        //if (Guid.TryParse(GV_BankInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString(), out BankID))

                        // {
                        pibankinformationbo objBankInfoBo = new pibankinformationbo();
                        pibankinformationbl objBankInfoBl = new pibankinformationbl();
                        objBankInfoBo.PKEY = GV_BankInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["PKEY"].ToString().Trim();
                        objBankInfoBo.PERNR = User.Identity.Name;
                        pibankinformationcollectionbo objPIAddBoLst = objBankInfoBl.Get_Bank_Details_Full(objBankInfoBo);
                        if (objPIAddBoLst.Count > 0)
                        {
                            FV_BankInfo.DataSource = objPIAddBoLst;
                            FV_BankInfo.DataBind();
                            FV_BankInfo.ChangeMode(FormViewMode.ReadOnly);
                            MV_BankInfo.SetActiveView(V_AddEditBankInfo);
                            MsgCls(string.Empty, LblMsg, Color.Transparent);
                        }
                        else
                        { MsgCls("Invalid ID", LblMsg, Color.Red); }
                        //}

                        break;
                    case "DELETE":


                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void GV_BankInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GV_BankInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GV_BankInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        #endregion

        #region BankInfo Populate pager
        private void BankInfoPager(int RecordCount, int currentPage)
        {
            try
            {
                List<ListItem> pages = new List<ListItem>();
                int startIndex, endIndex;
                int pagerSpan = 5;

                //Calculate the Start and End Index of pages to be displayed.
                double dblPageCount = (double)((decimal)RecordCount / Convert.ToDecimal(5));
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
                RptrBankInfoPager.DataSource = pages;
                RptrBankInfoPager.DataBind();
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void RptrLeaveOverviewPagerPage_Changed(object sender, EventArgs e)
        {
            try
            {
                BankInfoPageIndex = int.Parse((sender as LinkButton).CommandArgument);
                BindGV_BankInfo(BankInfoPageIndex);
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region Add / View Button Event

        protected void LbtnAddBankInfo_Click(object sender, EventArgs e)
        {
            try
            {
                MV_BankInfo.SetActiveView(V_ViewBankInfo);
                //FV_AddressInfo.ChangeMode(FormViewMode.Insert);
                MsgCls(string.Empty, LblMsg, Color.Transparent);
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        protected void LbtnBackBankInfoView_Click(object sender, EventArgs e)
        {
            try
            {
                PageLoadEvents();
                //FV_AddressInfo.ChangeMode(FormViewMode.ReadOnly);
                MsgCls(string.Empty, LblMsg, Color.Transparent);
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }
        #endregion

        #region FV_AddressInfo Events

        protected void FV_BankInfo_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            try
            {

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }


        protected void FV_BankInfo_ModeChanging(object sender, FormViewModeEventArgs e)
        {
            try
            {
                FV_BankInfo.ChangeMode(e.NewMode);
                //LoadAddressInfoFull(Guid.Parse(FV_AddressInfo.DataKey["ID"].ToString()));
            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, LblMsg, Color.Red); }
        }

        #endregion
    }
}
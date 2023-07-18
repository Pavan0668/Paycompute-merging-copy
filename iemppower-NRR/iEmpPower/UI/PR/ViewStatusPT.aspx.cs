using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace iEmpPower.UI.PR
{
    public partial class ViewStatusPT : System.Web.UI.Page
    {
        string EmployeeId = "";
        bool bSortedOrder = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    LoadEmpPRRequestGridView();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        
        }

        private void LoadEmpPRRequestGridView()
        {
            try
            {
                prbl prblObj = new prbl();
                List<prbo> requisitionboList1 = new List<prbo>();
                EmployeeId = User.Identity.Name;
                requisitionboList1 = prblObj.Load_EmpPRDetails(EmployeeId, "PTM");
                Session.Add("PRGrdInfo", requisitionboList1);

                grdPurchaseItemDeatils.Visible = true;
                grdPurchaseItemDeatils.DataSource = requisitionboList1;
                grdPurchaseItemDeatils.SelectedIndex = -1;
                grdPurchaseItemDeatils.DataBind();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void grdPurchaseItemDeatils_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                          int rowIndex = Convert.ToInt32(e.CommandArgument);

                          foreach (GridViewRow row in grdPurchaseItemDeatils.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray:
                            System.Drawing.Color.White;
                        }

                        ViewPRIfo.Visible = true;
                        int PRID = int.Parse(grdPurchaseItemDeatils.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());
                        prbl PrBlObj = new prbl();
                        List<prbo> requisitionboList = new List<prbo>();
                        requisitionboList = PrBlObj.Load_PRItemDetails(PRID);
                        FV_EmpPRInfoDisplay.DataSource = requisitionboList;
                        FV_EmpPRInfoDisplay.DataBind();


                        grdEmpAppHistory.DataSource = requisitionboList;
                        grdEmpAppHistory.DataBind();

                        ViewState["APPROVEDBY1"] = requisitionboList[0].APPROVEDBY1 == null ? "" : requisitionboList[0].APPROVEDBY1.ToString();
                        ViewState["APPROVEDBY2"] = requisitionboList[0].APPROVEDBY2 == null ? "" : requisitionboList[0].APPROVEDBY2.ToString();
                        ViewState["APPROVEDBY3"] = requisitionboList[0].APPROVEDBY3 == null ? "" : requisitionboList[0].APPROVEDBY3.ToString();
                        ViewState["APPROVEDBY4"] = requisitionboList[0].APPROVEDBY4 == null ? "" : requisitionboList[0].APPROVEDBY4.ToString();
                        ViewState["APPROVEDBY5"] = requisitionboList[0].APPROVEDBY5 == null ? "" : requisitionboList[0].APPROVEDBY5.ToString();
                        ViewState["APPROVEDBY6"] = requisitionboList[0].APPROVEDBY6 == null ? "" : requisitionboList[0].APPROVEDBY6.ToString();


                        requisitionboList = PrBlObj.Load_PRItem(PRID);
                        GV_EmpPrItems.DataSource = requisitionboList;
                        GV_EmpPrItems.DataBind();
                        break;
                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }



        protected void grdPurchaseItemDeatils_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdPurchaseItemDeatils.PageIndex = e.NewPageIndex;


            LoadEmpPRRequestGridView();
            searchpr();
            grdPurchaseItemDeatils.SelectedIndex = -1;
        }

        protected void grdPurchaseItemDeatils_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<prbo> PrboList = (List<prbo>)Session["PRGrdInfo"];
            bool objSortOrder = (bool)Session["bSortedOrder"];
            switch (e.SortExpression.ToString().Trim())
            {
                case "PRID":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.PRID.Value.CompareTo(objBo2.PRID.Value)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.PRID.Value.CompareTo(objBo1.PRID.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "SUG_SUPP":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.SUG_SUPP.CompareTo(objBo2.SUG_SUPP)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.SUG_SUPP.CompareTo(objBo1.SUG_SUPP)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;


                case "IN_BUDGET":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.IN_BUDGET.CompareTo(objBo2.IN_BUDGET)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.IN_BUDGET.CompareTo(objBo1.IN_BUDGET)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "INDENTOR":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.IPERNR.CompareTo(objBo2.IPERNR)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.IPERNR.CompareTo(objBo1.IPERNR)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "REQUESTOR":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.RPERNR.CompareTo(objBo2.RPERNR)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.RPERNR.CompareTo(objBo1.RPERNR)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "CRITICALITY":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.CRITICALITY.CompareTo(objBo2.CRITICALITY)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.CRITICALITY.CompareTo(objBo1.CRITICALITY)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "PSPNR":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.PSPNR.CompareTo(objBo2.PSPNR)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.PSPNR.CompareTo(objBo1.PSPNR)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "BNFPO":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.BNFPO.CompareTo(objBo2.BNFPO)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.BNFPO.CompareTo(objBo1.BNFPO)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "WAERS":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.WAERS.CompareTo(objBo2.WAERS)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.WAERS.CompareTo(objBo1.WAERS)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
                case "UNIT_PRICE":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return ((decimal.Parse(objBo1.UNIT_PRICE)).CompareTo(decimal.Parse(objBo2.UNIT_PRICE))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return ((decimal.Parse(objBo2.UNIT_PRICE)).CompareTo(decimal.Parse(objBo1.UNIT_PRICE))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
               
                case "TOTALAMT":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return ((decimal.Parse(objBo1.TAINRAmt)).CompareTo(decimal.Parse(objBo2.TAINRAmt))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return ((decimal.Parse(objBo2.TAINRAmt)).CompareTo(decimal.Parse(objBo1.TAINRAmt))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "STATUS":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.STATUS.CompareTo(objBo2.STATUS)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.STATUS.CompareTo(objBo1.STATUS)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CREATED_ON1":
                    if (objSortOrder)
                    {
                        if (PrboList != null)
                        {
                            PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                            { return (objBo1.CREATED_ON1.Value.CompareTo(objBo2.CREATED_ON1.Value)); });

                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        PrboList.Sort(delegate(prbo objBo1, prbo objBo2)
                        { return (objBo2.CREATED_ON1.Value.CompareTo(objBo1.CREATED_ON1.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
            }

            grdPurchaseItemDeatils.DataSource = PrboList;
            grdPurchaseItemDeatils.DataBind();

            Session.Add("PRGrdInfo", PrboList);
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                //MsgCls(string.Empty, LblMsg, Color.Transparent);
                //string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                //string textSearch = txtsearch.Text;
                //DateTime createdon = DateTime.Parse(string.IsNullOrEmpty(txtCreatedOn.Text) ? "01/01/0001" : txtCreatedOn.Text);

                //prbl prblObj = new prbl();
                //List<prbo> requisitionboList1 = new List<prbo>();
                //EmployeeId = User.Identity.Name;
                //requisitionboList1 = prblObj.Load_ParticularEmpPRDetails(EmployeeId, SelectedType, textSearch, createdon);

                //Session.Add("PRGrdInfo", requisitionboList1);

                //if (requisitionboList1 == null || requisitionboList1.Count == 0)
                //{
                //    MsgCls("No Records found", LblMsg, Color.Red);
                //    grdPurchaseItemDeatils.Visible = false;
                //    grdPurchaseItemDeatils.DataSource = null;
                //    grdPurchaseItemDeatils.DataBind();
                //    return;
                //}
                //else
                //{
                //    MsgCls("", LblMsg, Color.Transparent);
                //    grdPurchaseItemDeatils.Visible = true;
                //    grdPurchaseItemDeatils.DataSource = requisitionboList1;
                //    grdPurchaseItemDeatils.SelectedIndex = -1;
                //    grdPurchaseItemDeatils.DataBind();
                //    ViewPRIfo.Visible = false;
                //}
                searchpr();

            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, System.Drawing.Color.Red);
            }

        }
        public void searchpr()
        {
            try
            {
                MsgCls(string.Empty, LblMsg, System.Drawing.Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;
                DateTime createdon = DateTime.Parse(string.IsNullOrEmpty(txtCreatedOn.Text) ? "01/01/0001" : txtCreatedOn.Text);
                if (SelectedType != "0" && textSearch == "")
                {
                    MsgCls("Please Enter the Text", LblMsg, System.Drawing.Color.Red);
                }

                else if (SelectedType == "0" && textSearch != "")
                    {
                        MsgCls("Please Select the Type", LblMsg, System.Drawing.Color.Red);
                    }
                else
                {
                    prbl prblObj = new prbl();
                    List<prbo> requisitionboList1 = new List<prbo>();
                    EmployeeId = User.Identity.Name;
                    requisitionboList1 = prblObj.Load_ParticularEmpPRDetails(EmployeeId, SelectedType, textSearch, createdon, "PTM");

                    Session.Add("PRGrdInfo", requisitionboList1);

                    if (requisitionboList1 == null || requisitionboList1.Count == 0)
                    {
                        MsgCls("No Records found", LblMsg, System.Drawing.Color.Red);
                        grdPurchaseItemDeatils.Visible = false;
                        grdPurchaseItemDeatils.DataSource = null;
                        grdPurchaseItemDeatils.DataBind();
                        return;
                    }
                    else
                    {
                        MsgCls("", LblMsg, System.Drawing.Color.Transparent);
                        grdPurchaseItemDeatils.Visible = true;
                        grdPurchaseItemDeatils.DataSource = requisitionboList1;
                        grdPurchaseItemDeatils.SelectedIndex = -1;
                        grdPurchaseItemDeatils.DataBind();
                        ViewPRIfo.Visible = false;
                    }
                }


            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, System.Drawing.Color.Red);
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
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtCreatedOn.Text = string.Empty;
            LoadEmpPRRequestGridView();
            ViewPRIfo.Visible = false;
            MsgCls("", LblMsg, System.Drawing.Color.Transparent);
        }

        protected void grdEmpAppHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-1").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On1").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments1").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-2").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On2").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments2").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-3").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On3").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments3").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-4").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On4").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments4").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-5").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On5").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments5").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-6").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On6").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments6").SingleOrDefault()).Visible = true;

                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY1").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-1").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On1").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments1").SingleOrDefault()).Visible = false;
                //    }

                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY2").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-2").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On2").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments2").SingleOrDefault()).Visible = false;
                //    }
                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY3").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-3").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On3").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments3").SingleOrDefault()).Visible = false;
                //    }
                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY4").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-4").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On4").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments4").SingleOrDefault()).Visible = false;
                //    }
                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY5").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-5").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On5").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments5").SingleOrDefault()).Visible = false;
                //    }
                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY6").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-6").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On6").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdEmpAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments6").SingleOrDefault()).Visible = false;
                //    }

                //}

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }


        protected void FV_PRInfoDisplay_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DOWNLOADP":
                    string filePath = e.CommandArgument.ToString();
                    //Response.ContentType = ContentType;
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                    Response.WriteFile(filePath);
                    Response.End();
                    break;
                case "DOWNLOADA":
                    string filePath1 = e.CommandArgument.ToString();
                    //Response.ContentType = ContentType;
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath1));
                    Response.WriteFile(filePath1);
                    Response.End();
                    break;
                case "DOWNLOADE":
                    string filePath2 = e.CommandArgument.ToString();
                    //Response.ContentType = ContentType;
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath2));
                    Response.WriteFile(filePath2);
                    Response.End();
                    break;
                case "DOWNLOADI":
                    string filePath3 = e.CommandArgument.ToString();
                    //Response.ContentType = ContentType;
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath3));
                    Response.WriteFile(filePath3);
                    Response.End();
                    break;
                default:
                    break;
            }
        }

        protected void BtnExporttoXl_Click(object sender, EventArgs e)
        {
            try { ExportToExcel(); }
            catch (Exception ex) { }
        }

        protected void BtnExporttoPDF_Click(object sender, EventArgs e)
        {
            try { ExportGridToPDF();}
            catch (Exception ex) { }
        }

        protected void BtnExportHeadertoXl_Click(object sender, EventArgs e)
        {
            try { ExportHeaderToExcel(); }
            catch (Exception ex) { }
        }

        protected void BtnExportLineitemtoXl_Click(object sender, EventArgs e)
        {
            try { ExportLineitemToExcel(); }
            catch (Exception ex) { }
        }

        protected void ExportToExcel()
        {
            try
            {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


                htw.WriteBreak();

                string colHeads = "PR Details";
                htw.WriteEncodedText(colHeads);

                grdPurchaseItemDeatils.AllowPaging = false;

                LoadEmpPRRequestGridView();//searchpr();

                grdPurchaseItemDeatils.Columns[13].Visible = false;
                grdPurchaseItemDeatils.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                grdPurchaseItemDeatils.RenderControl(htw);
                grdPurchaseItemDeatils.Columns[13].Visible = true;
                grdPurchaseItemDeatils.AllowPaging = true;

                htw.WriteBreak();


                // Write the rendered content to a file.
                string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_PR.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();

                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        private DataTable getAllPRHeaderList()
        {
            string constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                //using (SqlCommand cmd = new SqlCommand("SELECT BANFN_EXT as 'PR No',R_PERNR as 'Req id',R_PERNR as 'Req id',FUNC_AREA as 'Main Function',BTEXT as 'Sub Function', MIS_GROUP_C as 'MIS Group C',MIS_GROUP_A as 'MIS Group A',MIS_GROUP_B as 'MIS Group B',EKGRP as 'Requestor Region',B_WERKS as 'Bill to address',S_WERKS as 'Ship to Address',SUG_SUPPLIER as 'Suggested Suppli',SUP_ADDRESS as 'Supplier Address',SUP_PHONE as 'Supplier Phone No',IN_BUDGET as 'In Budget',CAPITALIZED as 'Capitalization',CAP_TEXT as 'Budget line item',SERVICE_BUREAU as 'Managed Service',CRITICALITY as 'Criticality',PSPNR as 'ERP Project Code',BILLABLE as 'Billable',SPART as 'Business Unit',REGIONID as 'Region',JUSTIFICATOION as 'Justification',SPECIAL_NOTES as 'Special Notes',CREATED_ON as 'Submit Date',Status FROM ESS.ZMM_PR_REQ order by BANFN_EXT"))
                using (SqlCommand cmd = new SqlCommand("usp_ExportPRHeadertoXl", con)) 
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        DataTable dt = new DataTable();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        private DataTable getAllPRLineitemList()
        {
            string constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select BANFN_EXT as 'PR No',BNFPO as 'Item No',TXZ01 as 'Item Description',NO_OF_UNITS as 'No. of Units',MEINS as 'Unit of Measurements',UNIT_PRICE as 'Unit Price',WAERS as 'Currency',ITEM_NOTE as 'Item Note',MTART as 'Category',TAXABLE as 'Taxable(%)'  from ess.ZMM_PR_ITEM order by BANFN_EXT,BNFPO"))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        DataTable dt = new DataTable();
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public DataSet getDataSetExportToExcel()
        {
            DataSet ds = new DataSet();
            DataTable dtPRHeader = new DataTable("PRHeader");
            dtPRHeader = getAllPRHeaderList();

            DataTable dtPRLineitem = new DataTable("PRLineitem");
            dtPRLineitem = getAllPRLineitemList();
            ds.Tables.Add(dtPRHeader);
            ds.Tables.Add(dtPRLineitem);
            return ds;
        } 

        protected void ExportHeaderToExcel()
        {
            try
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PR_Header.xls"));
                Response.ContentType = "application/ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

                DataTable dt = getAllPRHeaderList();
                string str = string.Empty;
                foreach (DataColumn dtcol in dt.Columns)
                {
                    Response.Write(str + dtcol.ColumnName);
                    str = "\t";
                }
                Response.Write("\n");
                foreach (DataRow dr in dt.Rows)
                {
                    str = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Response.Write(str + Convert.ToString(dr[j]));
                        str = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();

                
               
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void ExportLineitemToExcel()
        {
            try
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PR_Lineitem.xls"));
                Response.ContentType = "application/ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

                DataTable dt1 = getAllPRLineitemList();
                string str1 = string.Empty;
                foreach (DataColumn dtcol in dt1.Columns)
                {
                    Response.Write(str1 + dtcol.ColumnName);
                    str1 = "\t";
                }
                Response.Write("\n");
                foreach (DataRow dr in dt1.Rows)
                {
                    str1 = "";
                    for (int j = 0; j < dt1.Columns.Count; j++)
                    {
                        Response.Write(str1 + Convert.ToString(dr[j]));
                        str1 = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        private void ExportGridToPDF()
        {
            try
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_PR.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                StringWriter s_tw = new StringWriter();
                HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                h_textw.AddStyleAttribute("font-size", "8pt");
                h_textw.AddStyleAttribute("color", "Black");



                string colHeads = "Summary_Report";

                h_textw.WriteBreak();
                colHeads = "PR Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                grdPurchaseItemDeatils.AllowPaging = false;

                LoadEmpPRRequestGridView();//searchpr();
                grdPurchaseItemDeatils.Columns[13].Visible = false;
                grdPurchaseItemDeatils.RenderControl(h_textw);
                grdPurchaseItemDeatils.Columns[13].Visible = true;
                grdPurchaseItemDeatils.AllowPaging = true;

                h_textw.WriteBreak();


                //  Document doc = new Document(PageSize.A2, 1f, 1f, 1f, 0.0f);
                Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0);

                //  Document doc = new Document();
                PdfWriter.GetInstance(doc, Response.OutputStream);
                doc.Open();
                StringReader s_tr = new StringReader(s_tw.ToString());
                HTMLWorker html_worker = new HTMLWorker(doc);
                html_worker.Parse(s_tr);
                doc.Close();
                Response.Write(doc);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
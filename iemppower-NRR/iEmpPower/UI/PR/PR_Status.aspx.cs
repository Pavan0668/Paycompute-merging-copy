using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.PR
{
    public partial class PR_Status : System.Web.UI.Page
    {
        bool bSortedOrder = false;
        string EmployeeId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PRID"] != null)
                {
                    ViewPR(Session["PRID"].ToString());
                    dvPRView.Visible = false;
                }

                ////if (Request.QueryString["NC"] != null)
                ////{
                ////    if (Request.QueryString["NC"] == "C")
                ////    {
                ////        if (Session["PRID"] != null)
                ////        {
                ////            ViewPR(Session["PRID"].ToString());
                ////            dvPRView.Visible = false;
                ////        }
                ////    }
                ////    else if (Request.QueryString["NC"] == "N")
                ////    {
                ////        Session["PRID"] = null;
                ////        Session.Clear();
                ////    }
                ////}



                else
                {
                    LoadEmpPRRequestGridView();
                }
            }
        }

        void ViewPR(string PRID1)
        {
            ////int rowIndex = Convert.ToInt32(e.CommandArgument);

            ////foreach (GridViewRow row in grdPurchaseItemDetails.Rows)
            ////{
            ////    row.BackColor = row.RowIndex.Equals(rowIndex) ?
            ////    System.Drawing.Color.LightGray :
            ////    System.Drawing.Color.White;
            ////}

            ViewPRIfo.Visible = true;
            ////int PRID = int.Parse(grdPurchaseItemDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());

            int PRID = int.Parse(PRID1);
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
        }

        private void LoadEmpPRRequestGridView()
        {
            try
            {
                prbl prblObj = new prbl();
                List<prbo> requisitionboList1 = new List<prbo>();
                EmployeeId = User.Identity.Name;
                requisitionboList1 = prblObj.Load_EmpPRDetails(EmployeeId, "EMP");

                Session.Add("PRGrdInfo", requisitionboList1);

                grdPurchaseItemDetails.Visible = true;
                grdPurchaseItemDetails.DataSource = requisitionboList1;
                grdPurchaseItemDetails.SelectedIndex = -1;
                grdPurchaseItemDetails.DataBind();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void grdPurchaseItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":


                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in grdPurchaseItemDetails.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        ViewPRIfo.Visible = true;
                        int PRID = int.Parse(grdPurchaseItemDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());
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
            }

            grdPurchaseItemDetails.DataSource = PrboList;
            grdPurchaseItemDetails.DataBind();

            Session.Add("PRGrdInfo", PrboList);
        }


        protected void grdPurchaseItemDeatils_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdPurchaseItemDetails.PageIndex = e.NewPageIndex;

            LoadEmpPRRequestGridView();
            searchpr();
            grdPurchaseItemDetails.SelectedIndex = -1;
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                searchpr();
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
                //    grdPurchaseItemDetails.Visible = false;
                //    grdPurchaseItemDetails.DataSource = null;
                //    grdPurchaseItemDetails.DataBind();
                //    return;
                //}
                //else
                //{
                //    MsgCls("", LblMsg, Color.Transparent);
                //    grdPurchaseItemDetails.Visible = true;
                //    grdPurchaseItemDetails.DataSource = requisitionboList1;
                //    grdPurchaseItemDetails.SelectedIndex = -1;
                //    grdPurchaseItemDetails.DataBind();
                //    ViewPRIfo.Visible = false;
                //}


            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }

        }

        public void searchpr()
        {
            try
            {
                MsgCls(string.Empty, LblMsg, Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;
                DateTime createdon = DateTime.Parse(string.IsNullOrEmpty(txtCreatedOn.Text) ? "01/01/0001" : txtCreatedOn.Text);

                if (SelectedType != "0" && textSearch == "")
                {
                    MsgCls("Please Enter the Text", LblMsg, Color.Red);
                }

                else if (SelectedType == "0" && textSearch != "")
                {
                    MsgCls("Please Select the Type", LblMsg, Color.Red);
                }
                else
                {


                    prbl prblObj = new prbl();
                    List<prbo> requisitionboList1 = new List<prbo>();
                    EmployeeId = User.Identity.Name;
                    requisitionboList1 = prblObj.Load_ParticularEmpPRDetails(EmployeeId, SelectedType, textSearch, createdon, "EMP");

                    Session.Add("PRGrdInfo", requisitionboList1);

                    if (requisitionboList1 == null || requisitionboList1.Count == 0)
                    {
                        MsgCls("No Records found", LblMsg, Color.Red);
                        grdPurchaseItemDetails.Visible = false;
                        grdPurchaseItemDetails.DataSource = null;
                        grdPurchaseItemDetails.DataBind();
                        return;
                    }
                    else
                    {
                        MsgCls("", LblMsg, Color.Transparent);
                        grdPurchaseItemDetails.Visible = true;
                        grdPurchaseItemDetails.DataSource = requisitionboList1;
                        grdPurchaseItemDetails.SelectedIndex = -1;
                        grdPurchaseItemDetails.DataBind();
                        ViewPRIfo.Visible = false;
                    }
                }

            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, LblMsg, Color.Red);
            }

        }



        private void MsgCls(string LblTxt, Label Lbl, Color Clr)
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
            MsgCls("", LblMsg, Color.Transparent);
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

        protected void lbtCopyPR_Click(object sender, EventArgs e)
        {
            if (Session["PRID"] != null)
            {
                //string PRID = Session["PRID"].ToString();
                Response.Redirect("Purchase_Request.aspx?NC=" + "C");
            }
        }
    }
}
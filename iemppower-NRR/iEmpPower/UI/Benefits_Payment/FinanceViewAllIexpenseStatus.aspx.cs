using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;


namespace iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment
{
    public partial class FinanceViewAllIexpenseStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadIExpenseGridView();
                    viewcheck.Value = "NO";
                }
                ////ShowcColBasedOnUser();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }


        }


        private void LoadIExpenseGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, System.Drawing.Color.Transparent);
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();
                string ApproverId = User.Identity.Name;
                IexpboList1 = ExpenseblObj.Load_ExpenseDetails_forfinance(ApproverId);
                Session.Add("IexpGrdInfo", IexpboList1);

                if (IexpboList1 == null || IexpboList1.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, System.Drawing.Color.Red);
                    grdIexpdetails.Visible = false;
                    grdIexpdetails.DataSource = null;
                    grdIexpdetails.DataBind();
                    return;
                }
                else
                {
                    grdIexpdetails.Visible = true;
                    grdIexpdetails.DataSource = IexpboList1;
                    grdIexpdetails.SelectedIndex = -1;
                    grdIexpdetails.DataBind();
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void grdIexpdetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
                        viewcheck.Value = "YES";
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow gvrow in grdIexpdetails.Rows)
                        {
                            gvrow.BackColor = gvrow.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        PnlIExpDetalsView.Visible = true;
                        //  grdIexpDetailsView.Visible = true;

                        //Exportbtn.Visible = true;
                        int row = int.Parse(e.CommandArgument.ToString());

                        ViewState["rowid"] = row;

                        int IEXP_ID = int.Parse(grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());

                        ViewState["IEXPID"] = int.Parse(grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());
                        ViewState["CREATED_BY"] = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString();
                        ViewState["ENAME"] = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENAME"].ToString();
                        ViewState["WBS_ELEMT"] = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["POST1"].ToString();
                        ViewState["CREATED_ON"] = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString();
                        string task = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["TASK"].ToString();
                        if (task.Trim() == "B")
                        {
                            task = "Billable";
                        }
                        else
                        {
                            task = "Non-Billable";
                        }
                        ViewState["ACTIVITY"] = task;

                        ViewState["RE_AMT"] = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["RE_AMT"].ToString();

                        ViewState["RCURR"] = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();
                        ViewState["PURPOSE"] = grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["PURPOSE"].ToString();


                        OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                        List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();


                        IexpboList = ExpenseblObj.Load_IEXPDetails(IEXP_ID);
                        grdIexpDetailsView.DataSource = IexpboList;
                        grdIexpDetailsView.DataBind();


                        IexpboList = ExpenseblObj.Load_IexpenseStatusDetails(IEXP_ID);

                        grdAppRejHistory.DataSource = IexpboList;
                        grdAppRejHistory.DataBind();


                        break;
                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
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

        public void Func_PMA()
        {
            try
            {
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();

                IexpboList1 = ExpenseblObj.Get_IexpensePendingMnrAppDetails(User.Identity.Name);
                grdIexpdetails.DataSource = IexpboList1;
                grdIexpdetails.DataBind();
                viewcheck.Value = "PMA";
                IexpboList.AddRange(IexpboList1);
                Session.Add("IexpGrdInfo", IexpboList);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }
        public void Func_PFA()
        {
            try
            {
                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();

                IexpboList1 = ExpenseblObj.Get_IexpensePendingFinAppDetails(User.Identity.Name);
                grdIexpdetails.DataSource = IexpboList1;
                grdIexpdetails.DataBind();
                viewcheck.Value = "PFA";
                IexpboList.AddRange(IexpboList1);
                Session.Add("IexpGrdInfo", IexpboList);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }



        protected void grdIexpdetails_Sorting(object sender, GridViewSortEventArgs e)
        {

            if (viewcheck.Value == "PMA")
            {
                Func_PMA();
            }
            else if (viewcheck.Value == "PFA")
            {
                Func_PFA();
            }
            else
            {
                LoadIExpenseGridView();
                searchdetails();
                viewcheck.Value = "NO";
            }

            List<OtherReimbursementsbo> IexpboList = (List<OtherReimbursementsbo>)Session["IexpGrdInfo"];
            bool objSortOrder = (bool)Session["bSortedOrder"];
            switch (e.SortExpression.ToString().Trim())
            {
                case "IEXP_ID":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.IEXP_ID.Value.CompareTo(objBo2.IEXP_ID.Value)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.IEXP_ID.Value.CompareTo(objBo1.IEXP_ID.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "PROJID":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.PROJID.ToString().CompareTo(objBo2.PROJID.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.PROJID.ToString().CompareTo(objBo1.PROJID.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "POST1":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.POST1.ToString().CompareTo(objBo2.POST1.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.POST1.ToString().CompareTo(objBo1.POST1.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "TASK":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.TASK.ToString().CompareTo(objBo2.TASK.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.TASK.ToString().CompareTo(objBo1.TASK.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CREATED_BY":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return ((int.Parse(objBo1.CREATED_BY)).CompareTo(int.Parse(objBo2.CREATED_BY))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return ((int.Parse(objBo2.CREATED_BY)).CompareTo(int.Parse(objBo1.CREATED_BY))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                ////case "ENAME":
                ////    if (objSortOrder)
                ////    {
                ////        if (IexpboList != null)
                ////        {
                ////            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                ////            { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                ////            objSortOrder = false;
                ////            Session.Add("bSortedOrder", objSortOrder);
                ////        }
                ////    }
                ////    else
                ////    {
                ////        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                ////        { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                ////        objSortOrder = true;
                ////        Session.Add("bSortedOrder", objSortOrder);
                ////    }
                ////    break;


                case "ENAME":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "RCURR":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.RCURR.ToString().CompareTo(objBo2.RCURR.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.RCURR.ToString().CompareTo(objBo1.RCURR.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "RE_AMT":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return ((decimal.Parse(objBo1.RE_AMT)).CompareTo(decimal.Parse(objBo2.RE_AMT))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return ((decimal.Parse(objBo2.RE_AMT)).CompareTo(decimal.Parse(objBo1.RE_AMT))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CREATED_ON":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.CREATED_ON.Value.CompareTo(objBo2.CREATED_ON.Value)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.CREATED_ON.Value.CompareTo(objBo1.CREATED_ON.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }

                    break;

                case "STATUS":
                    if (objSortOrder)
                    {
                        if (IexpboList != null)
                        {
                            IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                            { return (objBo1.STATUS.ToString().CompareTo(objBo2.STATUS.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        IexpboList.Sort(delegate(OtherReimbursementsbo objBo1, OtherReimbursementsbo objBo2)
                        { return (objBo2.STATUS.ToString().CompareTo(objBo1.STATUS.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;
            }

            grdIexpdetails.DataSource = IexpboList;
            grdIexpdetails.DataBind();

            Session.Add("IexpGrdInfo", IexpboList);

        }

        protected void grdIexpdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {


            int pageindex = e.NewPageIndex;
            grdIexpdetails.PageIndex = e.NewPageIndex;
            if (viewcheck.Value == "PMA")
            {
                Func_PMA();
            }
            else if (viewcheck.Value == "PFA")
            {
                Func_PFA();
            }
            else
            {
                LoadIExpenseGridView();
                searchdetails();

                viewcheck.Value = "NO";
            }
            grdIexpdetails.SelectedIndex = -1;



        }

        protected void grdIexpDetailsView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DOWNLOAD":
                    //  string filename= grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FPATH"].ToString();
                    string filePath = e.CommandArgument.ToString();
                    //Response.ContentType = ContentType;
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                    Response.WriteFile(filePath);
                    Response.End();
                    break;
                default:
                    break;
            }

        }



        protected void btnclear_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            LoadIExpenseGridView();
            PnlIExpDetalsView.Visible = false;

            MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
            viewcheck.Value = "NO";
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                searchdetails();
                viewcheck.Value = "NO";
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, System.Drawing.Color.Red);
            }

        }
        public void searchdetails()
        {

            try
            {
                MsgCls(string.Empty, lblMessageBoard, System.Drawing.Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;


                if (SelectedType != "0" && textSearch == "")
                {
                    MsgCls("Please Enter the Text", lblMessageBoard, System.Drawing.Color.Red);
                }

                else if (SelectedType == "0" && textSearch != "")
                {
                    MsgCls("Please Select the Type", lblMessageBoard, System.Drawing.Color.Red);
                }
                else
                {


                    OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                    List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();

                    IexpboList = ExpenseblObj.Load_ParticularAllIexpDetailsforFinance(User.Identity.Name, SelectedType, textSearch);
                    if (IexpboList == null || IexpboList.Count == 0)
                    {
                        MsgCls("No Records found", lblMessageBoard, System.Drawing.Color.Red);
                        grdIexpdetails.Visible = false;
                        grdIexpdetails.DataSource = null;
                        grdIexpdetails.DataBind();
                        PnlIExpDetalsView.Visible = false;

                        return;
                    }
                    else
                    {
                        MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
                        grdIexpdetails.Visible = true;
                        grdIexpdetails.DataSource = IexpboList;
                        grdIexpdetails.SelectedIndex = -1;
                        grdIexpdetails.DataBind();
                        //GV_TravelClaimReqAppRej.Visible = false;
                        //grdAppRejTravel.Visible = false;
                        //Panel1.Visible = false;
                        PnlIExpDetalsView.Visible = false;
                        Session.Add("IexpGrdInfo", IexpboList);

                    }

                }

            }

            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                //MsgCls(Ex.Message, lblMessageBoard, System.Drawing.Color.Red);
                MsgCls("Please enter valid data", lblMessageBoard, System.Drawing.Color.Red);
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void ExportToPDF_Click(object sender, EventArgs e)
        {
            ExportGridToPDF();
        }

        protected void ExportToExcel()
        {
            if (viewcheck.Value == "YES")
            {

                int rowid = int.Parse(ViewState["rowid"].ToString());

                //for (int i = 0; i < grdAppRejTravel.Rows.Count; i++)
                //{
                //    GridViewRow row = grdAppRejTravel.Rows[i];
                //    row.Visible = false;
                //    
                //}

                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                //htw.AddStyleAttribute("font-size", "8pt");
                //htw.AddStyleAttribute("color", "Black");

                htw.WriteBreak();

                string colHeads = "Iexpense Item Details";
                htw.WriteEncodedText(colHeads);
                grdIexpDetailsView.RenderControl(htw);
                htw.WriteBreak();

                colHeads = "Iexpense Approval Details";
                htw.WriteEncodedText(colHeads);
                grdAppRejHistory.RenderControl(htw);
                htw.WriteBreak();


                // Write the rendered content to a file.
                string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                renderedGridView += "Iexpense Details <br/>";
                renderedGridView += "<table><tr><td align=left>Iexpense ID</td><td align=left>:</td><td align=left>" + ViewState["IEXPID"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["CREATED_BY"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["ENAME"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Project ID</td><td align=left>:</td><td align=left>" + ViewState["WBS_ELEMT"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Task</td><td align=left>:</td><td align=left>" + ViewState["ACTIVITY"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Reimbursement Amount</td><td align=left>:</td><td align=left>" + decimal.Parse(ViewState["RE_AMT"].ToString().Trim()).ToString("#,##0.00") + "</td></tr>";
                renderedGridView += "<tr><td align=left>Reimbursement Currency</td><td align=left>:</td><td align=left>" + ViewState["RCURR"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Purpose</td><td align=left>:</td><td align=left>" + ViewState["PURPOSE"].ToString() + "</td></tr>";
                renderedGridView += "<tr><td align=left>Created On</td><td align=left>:</td><td align=left>" + DateTime.Parse(ViewState["CREATED_ON"].ToString().Trim()).ToString("dd-MMM-yyyy") + "</td></tr></table>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_Iexpense.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();
            }
            else if (viewcheck.Value == "PMA")
            {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                htw.WriteBreak();

                string colHeads = "Iexpense Details";
                htw.WriteEncodedText(colHeads);

                grdIexpdetails.AllowPaging = false;

                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();

                IexpboList1 = ExpenseblObj.Get_IexpensePendingMnrAppDetails(User.Identity.Name);
                grdIexpdetails.DataSource = IexpboList1;
                grdIexpdetails.DataBind();
                grdIexpdetails.Columns[10].Visible = false;
                grdIexpdetails.HeaderRow.BackColor = System.Drawing.Color.LightBlue;

                grdIexpdetails.RenderControl(htw);
                grdIexpdetails.Columns[10].Visible = true;
                grdIexpdetails.AllowPaging = true;

                htw.WriteBreak();

                //string colHeads = "Iexpense Item Details";
                //htw.WriteEncodedText(colHeads);
                //grdIexpDetailsView.RenderControl(htw);
                //htw.WriteBreak();

                //colHeads = "Iexpense Approval Details";
                //htw.WriteEncodedText(colHeads);
                //grdAppRejHistory.RenderControl(htw);
                //htw.WriteBreak();


                // Write the rendered content to a file.
                string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                //renderedGridView += "Iexpense Details <br/>";
                //renderedGridView += "<table><tr><td align=left>Iexpense ID</td><td align=left>:</td><td align=left>" + ViewState["IEXPID"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["CREATED_BY"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["ENAME"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Project ID</td><td align=left>:</td><td align=left>" + ViewState["WBS_ELEMT"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Task</td><td align=left>:</td><td align=left>" + ViewState["ACTIVITY"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Reimbursement Amount</td><td align=left>:</td><td align=left>" + ViewState["RE_AMT"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Reimbursement Currency</td><td align=left>:</td><td align=left>" + ViewState["RCURR"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Purpose</td><td align=left>:</td><td align=left>" + ViewState["PURPOSE"].ToString() + "</td></tr></table>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_Iexpense.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();
            }
            else if (viewcheck.Value == "PFA")
            {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


                htw.WriteBreak();

                string colHeads = "Iexpense Details";
                htw.WriteEncodedText(colHeads);

                grdIexpdetails.AllowPaging = false;

                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();

                IexpboList1 = ExpenseblObj.Get_IexpensePendingFinAppDetails(User.Identity.Name);
                grdIexpdetails.DataSource = IexpboList1;
                grdIexpdetails.DataBind();
                grdIexpdetails.Columns[10].Visible = false;
                grdIexpdetails.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                grdIexpdetails.RenderControl(htw);
                grdIexpdetails.Columns[10].Visible = true;
                grdIexpdetails.AllowPaging = true;

                htw.WriteBreak();
                string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_Iexpense.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();
            }
            else
            {


                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


                htw.WriteBreak();

                string colHeads = "Iexpense Details";
                htw.WriteEncodedText(colHeads);

                grdIexpdetails.AllowPaging = false;

                searchdetails();
                grdIexpdetails.Columns[10].Visible = false;
                grdIexpdetails.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                grdIexpdetails.RenderControl(htw);
                grdIexpdetails.Columns[10].Visible = true;
                grdIexpdetails.AllowPaging = true;

                htw.WriteBreak();

                //string colHeads = "Iexpense Item Details";
                //htw.WriteEncodedText(colHeads);
                //grdIexpDetailsView.RenderControl(htw);
                //htw.WriteBreak();

                //colHeads = "Iexpense Approval Details";
                //htw.WriteEncodedText(colHeads);
                //grdAppRejHistory.RenderControl(htw);
                //htw.WriteBreak();


                // Write the rendered content to a file.
                string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                //renderedGridView += "Iexpense Details <br/>";
                //renderedGridView += "<table><tr><td align=left>Iexpense ID</td><td align=left>:</td><td align=left>" + ViewState["IEXPID"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["CREATED_BY"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["ENAME"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Project ID</td><td align=left>:</td><td align=left>" + ViewState["WBS_ELEMT"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Task</td><td align=left>:</td><td align=left>" + ViewState["ACTIVITY"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Reimbursement Amount</td><td align=left>:</td><td align=left>" + ViewState["RE_AMT"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Reimbursement Currency</td><td align=left>:</td><td align=left>" + ViewState["RCURR"].ToString() + "</td></tr>";
                //renderedGridView += "<tr><td align=left>Purpose</td><td align=left>:</td><td align=left>" + ViewState["PURPOSE"].ToString() + "</td></tr></table>";
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_Iexpense.xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        private void ExportGridToPDF()
        {


            if (viewcheck.Value == "YES")
            {


                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_Iexpense.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                StringWriter s_tw = new StringWriter();
                HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                h_textw.AddStyleAttribute("font-size", "8pt");
                h_textw.AddStyleAttribute("color", "Black");

                ////gvVehicle.RenderControl(h_textw);//Name of the Panel

                string colHeads = "Summary_Report";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Iexpense Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Iexpense ID :" + ViewState["IEXPID"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Created By :" + ViewState["CREATED_BY"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Employee Name :" + ViewState["ENAME"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Project ID :" + ViewState["WBS_ELEMT"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Task :" + ViewState["ACTIVITY"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Reimbursement Amount :" + decimal.Parse(ViewState["RE_AMT"].ToString().Trim()).ToString("#,##0.00");
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Reimbursement Currency :" + ViewState["RCURR"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Purpose :" + ViewState["PURPOSE"].ToString();
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Created On :" + DateTime.Parse(ViewState["CREATED_ON"].ToString().Trim()).ToString("dd-MMM-yyyy");
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();



                // h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Iexpense Item Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                grdIexpDetailsView.RenderControl(h_textw);
                h_textw.WriteBreak();

                colHeads = "Iexpense Approval Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                grdAppRejHistory.RenderControl(h_textw);
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
            else if (viewcheck.Value == "PMA")
            {

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_Iexpense.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                StringWriter s_tw = new StringWriter();
                HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                h_textw.AddStyleAttribute("font-size", "8pt");
                h_textw.AddStyleAttribute("color", "Black");

                string colHeads = "Summary_Report";

                h_textw.WriteBreak();
                colHeads = "Iexpense Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                grdIexpdetails.AllowPaging = false;

                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();

                IexpboList1 = ExpenseblObj.Get_IexpensePendingMnrAppDetails(User.Identity.Name);
                grdIexpdetails.DataSource = IexpboList1;
                grdIexpdetails.DataBind();
                grdIexpdetails.Columns[10].Visible = false;
                grdIexpdetails.RenderControl(h_textw);
                grdIexpdetails.Columns[10].Visible = true;
                grdIexpdetails.AllowPaging = true;

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
            else if (viewcheck.Value == "PFA")
            {

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_Iexpense.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                StringWriter s_tw = new StringWriter();
                HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                h_textw.AddStyleAttribute("font-size", "8pt");
                h_textw.AddStyleAttribute("color", "Black");

                string colHeads = "Summary_Report";

                h_textw.WriteBreak();
                colHeads = "Iexpense Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                grdIexpdetails.AllowPaging = false;

                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();

                IexpboList1 = ExpenseblObj.Get_IexpensePendingFinAppDetails(User.Identity.Name);
                grdIexpdetails.DataSource = IexpboList1;
                grdIexpdetails.DataBind();
                grdIexpdetails.Columns[10].Visible = false;
                grdIexpdetails.RenderControl(h_textw);
                grdIexpdetails.Columns[10].Visible = true;
                grdIexpdetails.AllowPaging = true;

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
            else
            {

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_Iexpense.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                StringWriter s_tw = new StringWriter();
                HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                h_textw.AddStyleAttribute("font-size", "8pt");
                h_textw.AddStyleAttribute("color", "Black");

                ////gvVehicle.RenderControl(h_textw);//Name of the Panel

                string colHeads = "Summary_Report";
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //colHeads = "Iexpense Details";
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //colHeads = "Iexpense ID :" + ViewState["IEXPID"].ToString();
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //colHeads = "Created By :" + ViewState["CREATED_BY"].ToString();
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //colHeads = "Employee Name :" + ViewState["ENAME"].ToString();
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //colHeads = "Project ID :" + ViewState["WBS_ELEMT"].ToString();
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //colHeads = "Task :" + ViewState["ACTIVITY"].ToString();
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //colHeads = "Reimbursement Amount :" + ViewState["RE_AMT"].ToString();
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //colHeads = "Reimbursement Currency :" + ViewState["RCURR"].ToString();
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //colHeads = "Purpose :" + ViewState["PURPOSE"].ToString();
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();

                //// h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                colHeads = "Iexpense Details";
                h_textw.WriteEncodedText(colHeads);
                h_textw.WriteBreak();
                grdIexpdetails.AllowPaging = false;

                searchdetails();
                grdIexpdetails.Columns[10].Visible = false;
                grdIexpdetails.RenderControl(h_textw);
                grdIexpdetails.Columns[10].Visible = true;
                grdIexpdetails.AllowPaging = true;

                h_textw.WriteBreak();

                //colHeads = "Iexpense Approval Details";
                //h_textw.WriteEncodedText(colHeads);
                //h_textw.WriteBreak();
                //grdAppRejHistory.RenderControl(h_textw);
                //h_textw.WriteBreak();



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
        }

        protected void btnPendingMnrApp_Click(object sender, EventArgs e)
        {

            try
            {
                PnlIExpDetalsView.Visible = false;
                //Exportbtn.Visible = false;
                MsgCls(string.Empty, lblMessageBoard, System.Drawing.Color.Transparent);

                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();

                IexpboList1 = ExpenseblObj.Get_IexpensePendingMnrAppDetails(User.Identity.Name);
                grdIexpdetails.DataSource = IexpboList1;
                grdIexpdetails.DataBind();



                IexpboList.AddRange(IexpboList1);
                Session.Add("IexpGrdInfo", IexpboList);



                if (IexpboList == null || IexpboList.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, System.Drawing.Color.Red);
                    grdIexpdetails.Visible = false;
                    grdIexpdetails.DataSource = null;

                    return;
                }
                else
                {
                    grdIexpdetails.Visible = true;
                    grdIexpdetails.DataSource = IexpboList;

                }
                grdIexpdetails.DataBind();
                viewcheck.Value = "PMA";
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);

            }
        }

        protected void btnPendingFinApp_Click(object sender, EventArgs e)
        {
            try
            {
                PnlIExpDetalsView.Visible = false;
                //  Exportbtn.Visible = false;
                MsgCls(string.Empty, lblMessageBoard, System.Drawing.Color.Transparent);

                OtherReimbursementsbl ExpenseblObj = new OtherReimbursementsbl();
                List<OtherReimbursementsbo> IexpboList = new List<OtherReimbursementsbo>();
                List<OtherReimbursementsbo> IexpboList1 = new List<OtherReimbursementsbo>();

                IexpboList1 = ExpenseblObj.Get_IexpensePendingFinAppDetails(User.Identity.Name);
                grdIexpdetails.DataSource = IexpboList1;
                grdIexpdetails.DataBind();



                IexpboList.AddRange(IexpboList1);
                Session.Add("IexpGrdInfo", IexpboList);
                ////    }
                ////}


                if (IexpboList == null || IexpboList.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, System.Drawing.Color.Red);
                    grdIexpdetails.Visible = false;
                    grdIexpdetails.DataSource = null;
                    return;
                }
                else
                {
                    grdIexpdetails.Visible = true;
                    grdIexpdetails.DataSource = IexpboList;

                }
                grdIexpdetails.DataBind();
                viewcheck.Value = "PFA";

            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);

            }
        }

        public void ShowcColBasedOnUser()
        {
            if (User.Identity.Name== "finance")
            {
                grdIexpdetails.Columns[3].Visible = false;
            }
            else
            {
                grdIexpdetails.Columns[3].Visible = true;
            }
        }
    }
}
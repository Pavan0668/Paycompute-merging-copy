using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class TravelClaimHistoryStatusNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ////LoadTravelClaimGridView();
                    if (Session["CID"] != null)
                    {
                        ViewTC(Session["CID"].ToString());
                    }

                    else
                    {
                        LoadTravelClaimGridView();
                    }
                }
                MembershipUser mu = Membership.GetUser(User.Identity.Name);
                string userEmail = mu.Email.ToString();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        void ViewTC(string CID1)
        {
            int CID = int.Parse(CID1);///int.Parse(grdIexpdetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["IEXP_ID"].ToString());

            travelrequestbl travelrequestblObj = new travelrequestbl();
            List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

            TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CID);
            grdClaimDetails.DataSource = TrvlReqboList;
            grdClaimDetails.DataBind();


            decimal total = TrvlReqboList.Sum(item => Convert.ToDecimal(item.RE_AMT));
            string currency = TrvlReqboList[0].RCURR.ToString();
            grdClaimDetails.FooterRow.Cells[7].Text = "Total";

            grdClaimDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            grdClaimDetails.FooterRow.Cells[8].Text = total.ToString("N2") + "(" + (currency) + ")";
            grdClaimDetails.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;

            ////TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(CID);

            ////grdAppRejHistory.DataSource = TrvlReqboList;
            ////grdAppRejHistory.DataBind();

            ////ViewState["APPROVEDBY1"] = TrvlReqboList[0].APPROVED_BY1 == null ? "" : TrvlReqboList[0].APPROVED_BY1.ToString();
            ////ViewState["APPROVEDBY2"] = TrvlReqboList[0].APPROVED_BY2 == null ? "" : TrvlReqboList[0].APPROVED_BY2.ToString();
            ////ViewState["APPROVEDBY3"] = TrvlReqboList[0].APPROVED_BY3 == null ? "" : TrvlReqboList[0].APPROVED_BY3.ToString();
            ////ViewState["APPROVEDBY4"] = TrvlReqboList[0].APPROVED_BY4 == null ? "" : TrvlReqboList[0].APPROVED_BY4.ToString();
            ////ViewState["APPROVEDBY5"] = TrvlReqboList[0].APPROVED_BY5 == null ? "" : TrvlReqboList[0].APPROVED_BY5.ToString();
            ////ViewState["APPROVEDBY6"] = TrvlReqboList[0].APPROVED_BY6 == null ? "" : TrvlReqboList[0].APPROVED_BY6.ToString();
            ////ViewState["APPROVEDBY7"] = TrvlReqboList[0].APPROVED_BY7 == null ? "" : TrvlReqboList[0].APPROVED_BY7.ToString();
            ////ViewState["APPROVEDBY8"] = TrvlReqboList[0].APPROVED_BY8 == null ? "" : TrvlReqboList[0].APPROVED_BY8.ToString();
            ////ViewState["APPROVEDBY9"] = TrvlReqboList[0].APPROVED_BY9 == null ? "" : TrvlReqboList[0].APPROVED_BY9.ToString();

            ////PnlIExpDetalsView.Visible = true;
            ////grdAppRejHistory.Visible = true;
        }


        private void LoadTravelClaimGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();

                TrvlReqboList1 = travelrequestblObj.Get_TravelClaimForDetailsNew(User.Identity.Name);
                grdAppRejTravel.DataSource = TrvlReqboList1;
                grdAppRejTravel.DataBind();



                TrvlReqboList.AddRange(TrvlReqboList1);
                Session.Add("TravelIexpGrdInfo", TrvlReqboList);
                ////    }
                ////}


                if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                    grdAppRejTravel.Visible = false;
                    grdAppRejTravel.DataSource = null;
                    //lblRemarks.Visible = false;
                    //TxtRemarks.Visible = false;
                    //btnApprove.Visible = false;
                    //btnReject.Visible = false;
                    return;
                }
                else
                {
                    grdAppRejTravel.Visible = true;
                    grdAppRejTravel.DataSource = TrvlReqboList;
                    // grdAppRejTravel.SelectedIndex = -1;
                    //lblRemarks.Visible = true;
                    //TxtRemarks.Visible = true;
                    //btnApprove.Visible = true;
                    //btnReject.Visible = true;
                }
                grdAppRejTravel.DataBind();

                //  PnlIExpDetalsView.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

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

        protected void grdAppRejTravel_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in grdAppRejTravel.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray : 
                            System.Drawing.Color.White;
                            //if (row.RowIndex.Equals(rowIndex))
                            //{
                            //row.BackColor = System.Drawing.Color.LightGray;
                            //}
                        }

                        PnlIExpDetalsView.Visible = true;
                        grdClaimDetails.Visible = true;
                        Exportbtn.Visible = true;
                        int CID = int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());

                         ViewState["CID"]  = int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                         ViewState["CREATED_BY"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString();
                         ViewState["REINR"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                         ViewState["ENAME"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENAME"].ToString();
                         ViewState["WBS_ELEMT"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();
                         ViewState["ACTIVITY"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["ACTIVITY"].ToString(); 
                      
                         ViewState["RE_AMT"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["RE_AMT"].ToString(); 
                      
                        ViewState["RCURR"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString(); 
                       

                        travelrequestbl travelrequestblObj = new travelrequestbl();
                        List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                        TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CID);
                        grdClaimDetails.DataSource = TrvlReqboList;
                        grdClaimDetails.DataBind();

                         DataTable dt = ConvertToDataTable(TrvlReqboList);
                        decimal d = 0;
                        decimal total = dt.AsEnumerable()
                                 .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                        grdClaimDetails.FooterRow.Cells[7].Text = "Total : ";


                        grdClaimDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                        grdClaimDetails.FooterRow.Cells[8].Text = total.ToString("#,##0.00") + "(" + (ViewState["RCURR"].ToString()) + ")";


                        TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(CID);

                        grdAppRejHistory.DataSource = TrvlReqboList;
                        grdAppRejHistory.DataBind();

                        ViewState["APPROVEDBY1"] = TrvlReqboList[0].APPROVED_BY1 == null ? "" : TrvlReqboList[0].APPROVED_BY1.ToString();
                        ViewState["APPROVEDBY2"] = TrvlReqboList[0].APPROVED_BY2 == null ? "" : TrvlReqboList[0].APPROVED_BY2.ToString();
                        ViewState["APPROVEDBY3"] = TrvlReqboList[0].APPROVED_BY3 == null ? "" : TrvlReqboList[0].APPROVED_BY3.ToString();
                        ViewState["APPROVEDBY4"] = TrvlReqboList[0].APPROVED_BY4 == null ? "" : TrvlReqboList[0].APPROVED_BY4.ToString();
                        ViewState["APPROVEDBY5"] = TrvlReqboList[0].APPROVED_BY5 == null ? "" : TrvlReqboList[0].APPROVED_BY5.ToString();
                        ViewState["APPROVEDBY6"] = TrvlReqboList[0].APPROVED_BY6 == null ? "" : TrvlReqboList[0].APPROVED_BY6.ToString();
                        ViewState["APPROVEDBY7"] = TrvlReqboList[0].APPROVED_BY7 == null ? "" : TrvlReqboList[0].APPROVED_BY7.ToString();
                        ViewState["APPROVEDBY8"] = TrvlReqboList[0].APPROVED_BY8 == null ? "" : TrvlReqboList[0].APPROVED_BY8.ToString();
                        ViewState["APPROVEDBY9"] = TrvlReqboList[0].APPROVED_BY9 == null ? "" : TrvlReqboList[0].APPROVED_BY9.ToString();

                        PnlIExpDetalsView.Visible = true;
                        grdAppRejHistory.Visible = true;
                        break;
                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        protected void grdAppRejTravel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdAppRejTravel.PageIndex = e.NewPageIndex;

            LoadTravelClaimGridView();
            searchdetails();
            grdAppRejTravel.SelectedIndex = -1;
        }

        protected void grdClaimDetails_RowCommand(object sender, GridViewCommandEventArgs e)
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


        protected void grdAppRejTravel_Sorting(object sender, GridViewSortEventArgs e)
        {
            travelrequestbl travelrequestblObj = new travelrequestbl();
          //  List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

            List<TrvlReqDetails> TrvlReqboList = (List<TrvlReqDetails>)Session["TravelIexpGrdInfo"];
            bool objSortOrder = (bool)Session["bSortedOrder"];
            switch (e.SortExpression.ToString().Trim())
            {
                case "CID":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.CID.Value.CompareTo(objBo2.CID.Value)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.CID.Value.CompareTo(objBo1.CID.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "REINR":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return ((long.Parse(objBo1.REINR)).CompareTo(long.Parse(objBo2.REINR))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return ((long.Parse(objBo2.REINR)).CompareTo(long.Parse(objBo1.REINR))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;





                case "WBS_ELEMT":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.WBS_ELEMT.ToString().CompareTo(objBo2.WBS_ELEMT.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.WBS_ELEMT.ToString().CompareTo(objBo1.WBS_ELEMT.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;


                //case "POST1":
                //    if (objSortOrder)
                //    {
                //        if (TrvlReqboList != null)
                //        {
                //            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                //            { return (objBo1.POST1.ToString().CompareTo(objBo2.POST1.ToString())); });
                //            objSortOrder = false;
                //            Session.Add("bSortedOrder", objSortOrder);
                //        }
                //    }
                //    else
                //    {
                //        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                //        { return (objBo2.POST1.ToString().CompareTo(objBo1.POST1.ToString())); });
                //        objSortOrder = true;
                //        Session.Add("bSortedOrder", objSortOrder);
                //    }
                //    break;
                case "CREATED_BY":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return ((int.Parse(objBo1.CREATED_BY)).CompareTo(int.Parse(objBo2.CREATED_BY))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return ((int.Parse(objBo2.CREATED_BY)).CompareTo(int.Parse(objBo1.CREATED_BY))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "ENAME":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                   break;
                //case "ENTITY":
                //    if (objSortOrder)
                //    {
                //        if (TrvlReqboList != null)
                //        {
                //            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                //            { return (objBo1.ENTITY.ToString().CompareTo(objBo2.ENTITY.ToString())); });
                //            objSortOrder = false;
                //            Session.Add("bSortedOrder", objSortOrder);
                //        }
                //    }
                //    else
                //    {
                //        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                //        { return (objBo2.ENTITY.ToString().CompareTo(objBo1.ENTITY.ToString())); });
                //        objSortOrder = true;
                //        Session.Add("bSortedOrder", objSortOrder);
                //    }
                //    break;

                case "ACTIVITY":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.ACTIVITY.ToString().CompareTo(objBo2.ACTIVITY.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.ACTIVITY.ToString().CompareTo(objBo1.ACTIVITY.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "RCURR":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.RCURR.ToString().CompareTo(objBo2.RCURR.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.RCURR.ToString().CompareTo(objBo1.RCURR.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "STATUS":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.STATUS.ToString().CompareTo(objBo2.STATUS.ToString())); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.STATUS.ToString().CompareTo(objBo1.STATUS.ToString())); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "RE_AMT":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return ((decimal.Parse(objBo1.RE_AMT)).CompareTo(decimal.Parse(objBo2.RE_AMT))); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return ((decimal.Parse(objBo2.RE_AMT)).CompareTo(decimal.Parse(objBo1.RE_AMT))); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                    break;

                case "CREATED_ON":
                    if (objSortOrder)
                    {
                        if (TrvlReqboList != null)
                        {
                            TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                            { return (objBo1.CREATED_ON.Value.CompareTo(objBo2.CREATED_ON.Value)); });
                            objSortOrder = false;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                    }
                    else
                    {
                        TrvlReqboList.Sort(delegate(TrvlReqDetails objBo1, TrvlReqDetails objBo2)
                        { return (objBo2.CREATED_ON.Value.CompareTo(objBo1.CREATED_ON.Value)); });
                        objSortOrder = true;
                        Session.Add("bSortedOrder", objSortOrder);
                    }

                    break;


            }

            grdAppRejTravel.DataSource = TrvlReqboList;
            grdAppRejTravel.DataBind();

            Session.Add("TravelIexpGrdInfo", TrvlReqboList);

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

            //int rowid = int.Parse(ViewState["rowid"].ToString());

            //for (int i = 0; i < grdAppRejTravel.Rows.Count; i++)
            //{
            //    GridViewRow row = grdAppRejTravel.Rows[i];
            //    row.Visible = false;
            //    
            //}

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            // Render grid view control.
            htw.WriteBreak();
            //string colHeads = "Travel Details";
            //htw.WriteEncodedText(colHeads);
            ////grdAppRejTravel.Columns[10].Visible = false;
            ////grdAppRejTravel.Rows[rowid].RenderControl(htw);
            ////  //  .RenderControl(htw);
            ////grdAppRejTravel.Columns[10].Visible = false;


            //htw.WriteBreak();

            string colHeads = "Travel Claim Details";
            htw.WriteEncodedText(colHeads);
            grdClaimDetails.RenderControl(htw);
            htw.WriteBreak();

            colHeads = "Travel Claim Approval Details";
            htw.WriteEncodedText(colHeads);
            grdAppRejHistory.RenderControl(htw);
            htw.WriteBreak();


            // Write the rendered content to a file.
            string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
            renderedGridView += "Travel Details <br/>";
            renderedGridView += "<table><tr><td align=left>Claim ID</td><td align=left>:</td><td align=left>" + ViewState["CID"].ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Trip No</td><td align=left>:</td><td align=left>" + ViewState["REINR"].ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["CREATED_BY"].ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["ENAME"].ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Project ID</td><td align=left>:</td><td align=left>" + ViewState["WBS_ELEMT"].ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Task</td><td align=left>:</td><td align=left>" + ViewState["ACTIVITY"].ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Reimbursement Amount</td><td align=left>:</td><td align=left>" + ViewState["RE_AMT"].ToString() + "</td></tr>";
            renderedGridView += "<tr><td align=left>Reimbursement Currency</td><td align=left>:</td><td align=left>" + ViewState["RCURR"].ToString() + "</td></tr></table>";
            renderedGridView += sw.ToString() + "<br/>";
            Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_TravelClaim.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        private void ExportGridToPDF()
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_TravelClaim.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter s_tw = new StringWriter();
            HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
            h_textw.AddStyleAttribute("font-size", "8pt");
            h_textw.AddStyleAttribute("color", "Black");

            ////gvVehicle.RenderControl(h_textw);//Name of the Panel

            string colHeads = "Summary_Report";
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            colHeads = "Travel Details";
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            colHeads = "Claim ID :" + ViewState["CID"].ToString();
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            colHeads = "Trip No :" + ViewState["REINR"].ToString();
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
            colHeads = "Reimbursement Amount :" + ViewState["RE_AMT"].ToString();
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            colHeads = "Reimbursement Currency :" + ViewState["RCURR"].ToString();
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();

            // h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            colHeads = "Travel Claim Details";
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            grdClaimDetails.RenderControl(h_textw);
            h_textw.WriteBreak();

            colHeads = "Travel Claim Approval Details";
            h_textw.WriteEncodedText(colHeads);
            h_textw.WriteBreak();
            grdAppRejHistory.RenderControl(h_textw);
            h_textw.WriteBreak();


           // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

            //  Document doc = new Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();
            StringReader s_tr = new StringReader(s_tw.ToString());
            iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
            html_worker.Parse(s_tr);
            doc.Close();
            Response.Write(doc);
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;

            LoadTravelClaimGridView();
            //  GV_TravelClaimReqAppRej.Visible = false;
            //GV_TravelClaimReqAppRej.Visible = false;
            PnlIExpDetalsView.Visible = false;
            Exportbtn.Visible = false;

            MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                searchdetails();
            //    MsgCls(string.Empty, lblMessageBoard, System.Drawing.Color.Transparent);
            //    string SelectedType = ddlSeachSelect.SelectedValue.ToString();
            //    string textSearch = txtsearch.Text;

            //    travelrequestbl travelrequestblObj = new travelrequestbl();
            //    List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

            //    TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsforEmployee(User.Identity.Name, SelectedType, textSearch);
            //    if (TrvlReqboList == null || TrvlReqboList.Count == 0)
            //    {
            //        MsgCls("No Records found", lblMessageBoard, System.Drawing.Color.Red);
            //        grdAppRejTravel.Visible = false;
            //        grdAppRejTravel.DataSource = null;
            //        grdAppRejTravel.DataBind();
            //        PnlIExpDetalsView.Visible = false;
            //        Exportbtn.Visible = false;
            //        return;
            //    }
            //    else
            //    {
            //        MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
            //        grdAppRejTravel.Visible = true;
            //        grdAppRejTravel.DataSource = TrvlReqboList;
            //        grdAppRejTravel.SelectedIndex = -1;
            //        grdAppRejTravel.DataBind();
            //        //GV_TravelClaimReqAppRej.Visible = false;
            //        //grdAppRejTravel.Visible = false;
            //        //Panel1.Visible = false;
            //        PnlIExpDetalsView.Visible = false;
            //        Exportbtn.Visible = false;
            //    }


            //    //  Session.Add("TravelGrdInfo", TrvlReqboList);




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


                      travelrequestbl travelrequestblObj = new travelrequestbl();
                      List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                      TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsforEmployee(User.Identity.Name, SelectedType, textSearch);
                      if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                      {
                          MsgCls("No Records found", lblMessageBoard, System.Drawing.Color.Red);
                          grdAppRejTravel.Visible = false;
                          grdAppRejTravel.DataSource = null;
                          grdAppRejTravel.DataBind();
                          PnlIExpDetalsView.Visible = false;
                          Exportbtn.Visible = false;
                          return;
                      }
                      else
                      {
                          MsgCls("", lblMessageBoard, System.Drawing.Color.Transparent);
                          grdAppRejTravel.Visible = true;
                          grdAppRejTravel.DataSource = TrvlReqboList;
                          grdAppRejTravel.SelectedIndex = -1;
                          grdAppRejTravel.DataBind();
                          //GV_TravelClaimReqAppRej.Visible = false;
                          //grdAppRejTravel.Visible = false;
                          //Panel1.Visible = false;
                          PnlIExpDetalsView.Visible = false;
                          Exportbtn.Visible = false;
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

    }
}
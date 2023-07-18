using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using iEmpPower.Old_App_Code.iEmpPowerBL.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment;
using iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment;

namespace iEmpPower.UI.Benefits_Payment
{
    public partial class TravelClaimAppRejNew : System.Web.UI.Page
    {
        WebService.Service ServicesObj = new WebService.Service();
        protected MembershipUser memUser;
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
                        ////goto displayInfo;
                    }
                }
                MembershipUser mu = Membership.GetUser(User.Identity.Name);
                string userEmail = mu.Email.ToString();
                this.Form.DefaultButton = btnApprove.UniqueID;
                this.Form.DefaultFocus = btnApprove.UniqueID;
                btnApprove.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        void ViewTC(string CID1)
        {
            try
            {
                //int rowIndex = Convert.ToInt32(e.CommandArgument);

                //        foreach (GridViewRow row in grdAppRejTravel.Rows)
                //        {
                //            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                //            System.Drawing.Color.LightGray:
                //            System.Drawing.Color.White;
                //        }

                        PnlIExpDetalsView.Visible = true;
                        grdClaimDetails.Visible = true;
                           btnApprove.Visible = true;
                           btnReject.Visible = true;

                        //int CID = int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                        int CID = int.Parse(CID1);
                        ViewState["CID"]  = CID;//int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());


                          travelrequestbl travelrequestblObj = new travelrequestbl();
                          List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                          TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CID);
                          grdClaimDetails.DataSource = TrvlReqboList;
                          grdClaimDetails.DataBind();


                          if (TrvlReqboList[0].TASK.ToString().Trim() == "B")
                          {
                              thHigltr.Visible = true;
                              lbltask.Text = "Billable Claim";
                              ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('This Claim is Billable');", true);
                          }



                          ViewState["CREATED_BY"] = TrvlReqboList[0].CREATED_BY.ToString();//grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString();
                          ViewState["@TripNo"] =  TrvlReqboList[0].REINR.ToString();//grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                          HFCID.Value = CID1;//grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString().Trim();
                          ViewState["ProjectforMail"]=TrvlReqboList[0].WBS_ELEMT.ToString();//grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();
                          ViewState["ActivityforMail"]=TrvlReqboList[0].ACTIVITY.ToString();//grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["ACTIVITY"].ToString();
                          ViewState["ReamtforMail"]=TrvlReqboList[0].RE_AMT.ToString();//grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["RE_AMT"].ToString();
                          ViewState["RcurrforMail"]=TrvlReqboList[0].RCURR.ToString();//grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();
                        
                          //// travelrequestbl travelrequestblObj = new travelrequestbl();
                          ////List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                          ////TrvlReqboList = travelrequestblObj.Load_ClaimDetails(CID);
                          ////grdClaimDetails.DataSource = TrvlReqboList;
                          ////grdClaimDetails.DataBind();
                        


                        DataTable dt = ConvertToDataTable(TrvlReqboList);
                        decimal d = 0;
                        decimal total = dt.AsEnumerable()
                                 .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                        grdClaimDetails.FooterRow.Cells[7].Text = "Total : ";


                        grdClaimDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                        grdClaimDetails.FooterRow.Cells[8].Text = total.ToString("#,##0.00") + "(" + (ViewState["RcurrforMail"].ToString()) + ")";
                        ViewState["ReamtforMail"] = total.ToString();

                        //////------------------------------------New code for Pop Up Starts-----------------------
                        decimal d1 = 0;
                        decimal dev = dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("DEVIATION_AMT"), out d1)).Sum(r => d1);

                        ViewState["dev"] = dev;
                        //string dev = "Please be informed that this claim is having deviation amount!";   

                        //if (total1 > 0)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + dev + "')", true);
                        //}


                        //////------------------------------------New code for Pop Up Ends-------------------------



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
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        grdAppRejHistory.Visible = true;
                        grdClaimDetails.Visible = true;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }


        private void LoadTravelClaimGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();

                TrvlReqboList1 = travelrequestblObj.Get_TravelClaimForApprovalNew(User.Identity.Name);
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
                        }

                        PnlIExpDetalsView.Visible = true;
                        grdClaimDetails.Visible = true;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;

                        int CID = int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                        ViewState["CID"] = int.Parse(grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                        ViewState["CREATED_BY"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString();
                        ViewState["@TripNo"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["REINR"].ToString();
                        HFCID.Value = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString().Trim();
                        ViewState["ProjectforMail"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();
                        ViewState["ActivityforMail"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["ACTIVITY"].ToString();
                        ViewState["ReamtforMail"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["RE_AMT"].ToString();
                        ViewState["RcurrforMail"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();

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
                        grdClaimDetails.FooterRow.Cells[8].Text = total.ToString("#,##0.00") + "(" + (ViewState["RcurrforMail"].ToString()) + ")";


                        //////------------------------------------New code for Pop Up Starts-----------------------
                        decimal d1 = 0;
                        decimal dev = dt.AsEnumerable().Where(r => decimal.TryParse(r.Field<string>("DEVIATION_AMT"), out d1)).Sum(r => d1);

                        ViewState["dev"] = dev;
                        //string dev = "Please be informed that this claim is having deviation amount!";   

                        //if (total1 > 0)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + dev + "')", true);
                        //}


                        //////------------------------------------New code for Pop Up Ends-------------------------



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
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        grdAppRejHistory.Visible = true;
                        grdClaimDetails.Visible = true;
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
            SearchRecord();
            grdAppRejTravel.SelectedIndex = -1;
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

                case "UPLOAD":
                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

                    int CID2 = int.Parse(grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                    ViewState["LID"] = int.Parse(grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());

                    ObjTrvlReq.CID = CID2;
                    ObjTrvlReq.LID = int.Parse(ViewState["LID"].ToString());
                    ObjTrvlReq.CREATED_BY = ViewState["CREATED_BY"].ToString();
                    using (CheckBox chk = (CheckBox)grdClaimDetails.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("cb"))
                    using (FileUpload fu = (FileUpload)grdClaimDetails.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("fuAttachments"))
                    {
                        ObjTrvlReq.RECEIPT_FILE = chk.Checked ? "YES" : "NO";
                        ObjTrvlReq.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                        ObjTrvlReq.RECEIPT_FPATH = fu.HasFile ? "~/TravelDoc/"
                            + ViewState["CREATED_BY"].ToString() + "-" + date1 + Path.GetExtension(fu.FileName) : "";


                        if (fu.HasFile)
                        { fu.SaveAs(Server.MapPath("~/TravelDoc/" + ViewState["CREATED_BY"].ToString() + "-" + date1) + Path.GetExtension(fu.FileName)); }
                        travelrequestblObj.TravelClaimReq_fivalUpdate1(ObjTrvlReq);
                        List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                        TrvlReqboList = travelrequestblObj.Load_ClaimDetails(int.Parse(ViewState["CID"].ToString()));
                        grdClaimDetails.DataSource = TrvlReqboList;
                        grdClaimDetails.DataBind();

                        DataTable dt = ConvertToDataTable(TrvlReqboList);
                        decimal d = 0;
                        decimal total = dt.AsEnumerable()
                                 .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                        grdClaimDetails.FooterRow.Cells[7].Text = "Total : ";


                        grdClaimDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                        grdClaimDetails.FooterRow.Cells[8].Text = total + "(" + (ViewState["RcurrforMail"].ToString()) + ")";
                        //  PageLoadEvents();
                        using (LinkButton ltnfu = (LinkButton)grdClaimDetails.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("LbtnUpload"))
                        {
                            ltnfu.Visible = false;
                        }
                    }
                    List<TrvlReqDetails> TrvlReqboList1 = new List<TrvlReqDetails>();

                    TrvlReqboList1 = travelrequestblObj.Load_ClaimDetails(int.Parse(ViewState["CID"].ToString()));
                    grdClaimDetails.DataSource = TrvlReqboList1;
                    grdClaimDetails.DataBind();

                    DataTable dt1 = ConvertToDataTable(TrvlReqboList1);
                    decimal d1 = 0;
                    decimal total1 = dt1.AsEnumerable()
                             .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d1)).Sum(r => d1);

                    grdClaimDetails.FooterRow.Cells[7].Text = "Total : ";


                    grdClaimDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdClaimDetails.FooterRow.Cells[8].Text = total1 + "(" + (ViewState["RcurrforMail"].ToString()) + ")";

                    break;


                case "DELETE":


                    travelrequestbl travelrequestblObj1 = new travelrequestbl();
                    TrvlReqDetails ObjTrvlReq1 = new TrvlReqDetails();
                    //string date2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

                    int CID3 = int.Parse(grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                    ViewState["LID"] = int.Parse(grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());

                    ObjTrvlReq1.CID = CID3;
                    ObjTrvlReq1.LID = int.Parse(ViewState["LID"].ToString());
                    ObjTrvlReq1.CREATED_BY = ViewState["CREATED_BY"].ToString();
                    List<TrvlReqDetails> TrvlReqboList2 = new List<TrvlReqDetails>();

                    travelrequestblObj1.TravelClaimReq_fivalDelete(ObjTrvlReq1);

                    TrvlReqboList2 = travelrequestblObj1.Load_ClaimDetails(int.Parse(ViewState["CID"].ToString()));
                    grdClaimDetails.DataSource = TrvlReqboList2;
                    grdClaimDetails.DataBind();

                    DataTable dt2 = ConvertToDataTable(TrvlReqboList2);
                    decimal d3 = 0;
                    decimal total2 = dt2.AsEnumerable()
                             .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d3)).Sum(r => d3);

                    grdClaimDetails.FooterRow.Cells[7].Text = "Total : ";


                    grdClaimDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdClaimDetails.FooterRow.Cells[8].Text = total2 + "(" + (ViewState["RcurrforMail"].ToString()) + ")";

                    //if (User.Identity.Name.StartsWith("fiadval"))
                    //{
                    //    using (CheckBox chk = (CheckBox)e.Row.FindControl("cb"))
                    //    using (FileUpload fu = (FileUpload)e.Row.FindControl("fuAttachments"))
                    //    using (LinkButton ltnfu = (LinkButton)e.Row.FindControl("LbtnUpload"))
                    //    using (LinkButton ltndelete = (LinkButton)e.Row.FindControl("LbtnDelete"))
                    //    {
                    //        string rceiptfile = DataBinder.Eval(e.Row.DataItem, "RECEIPT_FID").ToString();
                    //        if (rceiptfile == "")
                    //        {
                    //            chk.Visible = true;
                    //            fu.Visible = true;
                    //            ltnfu.Visible = true;
                    //            ltndelete.Visible = false;
                    //        }
                    //        else
                    //        {
                    //            chk.Visible = false;
                    //            fu.Visible = false;
                    //            ltnfu.Visible = false;
                    //            ltndelete.Visible = true;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    using (CheckBox chk = (CheckBox)e.Row.FindControl("cb"))
                    //    using (FileUpload fu = (FileUpload)e.Row.FindControl("fuAttachments"))
                    //    using (LinkButton ltnfu = (LinkButton)e.Row.FindControl("LbtnUpload"))
                    //    using (LinkButton ltndelete = (LinkButton)e.Row.FindControl("LbtnDelete"))
                    //    {
                    //        chk.Visible = false;
                    //        fu.Visible = false;
                    //        ltnfu.Visible = false;
                    //        ltndelete.Visible = false;
                    //    }
                    //}

                    using (LinkButton ltnfu = (LinkButton)grdClaimDetails.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("LbtnUpload"))
                    {
                        ltnfu.Visible = false;
                    }



                    TrvlReqboList1 = travelrequestblObj1.Load_ClaimDetails(int.Parse(ViewState["CID"].ToString()));
                    grdClaimDetails.DataSource = TrvlReqboList1;
                    grdClaimDetails.DataBind();

                    DataTable dt4 = ConvertToDataTable(TrvlReqboList1);
                    decimal d4 = 0;
                    decimal total4 = dt4.AsEnumerable()
                             .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d4)).Sum(r => d4);

                    grdClaimDetails.FooterRow.Cells[7].Text = "Total : ";


                    grdClaimDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdClaimDetails.FooterRow.Cells[8].Text = total4 + "(" + (ViewState["RcurrforMail"].ToString()) + ")";
                    break;



                case "EDITEXPTYPE":

                    int CIDEdit = int.Parse(grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                    int LIDEdit = int.Parse(grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());
                    ViewState["CIDEdit"] = int.Parse(grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["CID"].ToString());
                    ViewState["LIDEdit"] = int.Parse(grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());

                    string EXP_TYPEEdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPID"].ToString();
                    string DAILY_RATEEdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["DAILY_RATE"].ToString();
                    string DEVIATION_AMTEdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["DEVIATION_AMT"].ToString();
                    string DEVIATION_CURREdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["DEVIATION_CURR"].ToString();
                    string EXPT_AMTEdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPT_AMT"].ToString();
                    string EXPT_CURREdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["EXPT_CURR"].ToString();
                    string CountryIDEdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["CountryID"].ToString();
                    string RegoinIDEdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["RegoinID"].ToString();
                    string CountryEdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZLAND"].ToString();
                    string RegoinEdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["ZORT1"].ToString();
                    string NO_DAYSEdit = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["NO_DAYS"].ToString();
                    string DAILY_CURR = grdClaimDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["DAILY_CURR"].ToString();
                    ViewState["NO_DAYSEdit"] = NO_DAYSEdit;
                    ViewState["CountryEdit"] = CountryIDEdit;
                    ViewState["RegoinEdit"] = RegoinIDEdit;
                    ViewState["EXPT_CURREdit"] = EXPT_CURREdit;

                    string triptype = string.Empty;

                    travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                    objTravelRequestDataContext.usp_Get_TriptypeforTravel(CIDEdit, LIDEdit, ViewState["@TripNo"].ToString(), ref triptype);

                    if (triptype == "01")
                    {
                        string schema = "01";
                        LoadDDLExpenseType(schema, ViewState["CREATED_BY"].ToString());
                    }
                    else
                    {
                        string schema = "02";
                        LoadDDLExpenseType(schema, ViewState["CREATED_BY"].ToString());
                    }

                    ExpEDIT.Visible = true;
                    DDLExpenseType.SelectedValue = EXP_TYPEEdit;
                    LblExpenditureAmount.Text = EXPT_AMTEdit;
                    LblExptCurr.Text = EXPT_CURREdit;
                    LblCountry.Text = CountryEdit;
                    LblRegion.Text = RegoinEdit;
                    LblDailyRate.Text = DAILY_RATEEdit;
                    LblCurrency.Text = DAILY_CURR;
                    LblDeviation.Text = DEVIATION_AMTEdit;
                    LblCurrency1.Text = DEVIATION_CURREdit;


                    break;
                default:
                    break;
            }
        }


        public void GetDailyRate()
        {
            try
            {
                string DailyRate = "";
                LblDailyRate.Text = "";
                ViewState["dailyrate"] = "";
                string dailyratenodays = "";
                if ((LblCountry.Text != "") && DDLExpenseType.SelectedValue != "0")
                {
                    if (ViewState["NO_DAYSEdit"].ToString().Trim() == "1")
                    {
                        if (ViewState["CountryEdit"].ToString().Trim() == "IN" && ViewState["RegoinEdit"].ToString() != "")
                        {
                            DailyRate = ServicesObj.GetExcRateManagerEdit(ViewState["CREATED_BY"].ToString(), ViewState["CountryEdit"].ToString().Trim(), ViewState["RegoinEdit"].ToString().Trim(), DDLExpenseType.SelectedValue);
                            if ((DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0") == " ")
                            {
                                ViewState["dailyrate"] = "";
                            }
                            else
                            {

                                LblDailyRate.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0";
                            }
                            if ((DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "") == " ")
                            {
                                ViewState["dailyratecurr"] = "";
                            }
                            else
                            {

                                LblCurrency.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                            }
                            ViewState["dailyrate"] = LblDailyRate.Text;
                            ViewState["dailyratecurr"] = LblCurrency.Text;
                            // if (DailyRateValue.indexOf("~") >= 0) {
                            //var DailyRate = DailyRateValue.split('~');
                            //var DailyRate1 = parseFloat(isNaN(DailyRate[0]) || $.trim(DailyRate[0]) == '' ? '0.0' : (DailyRate[0] * $.trim(NoOfDays)));
                            // LblDailyRate.Text = DailyRate;
                            GetDeviationAmtCurr();
                        }
                        else if (ViewState["CountryEdit"].ToString().Trim() != "IN")
                        {
                            DailyRate = ServicesObj.GetExcRateManagerEdit(ViewState["CREATED_BY"].ToString(), ViewState["CountryEdit"].ToString().Trim(), ViewState["RegoinEdit"].ToString().Trim(), DDLExpenseType.SelectedValue);

                            if ((DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0") == " ")
                            {
                                ViewState["dailyrate"] = "";
                                //LblDailyRate.Text="0.0";
                            }
                            else
                            {

                                LblDailyRate.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0";
                            }
                            if ((DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "") == " ")
                            {
                                ViewState["dailyratecurr"] = "";
                                LblCurrency.Text = "";
                            }
                            else
                            {

                                LblCurrency.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                            }

                            //LblDailyRate.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0";
                            //LblCurrency.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                            ViewState["dailyrate"] = LblDailyRate.Text;
                            ViewState["dailyratecurr"] = LblCurrency.Text;
                            // if (DailyRateValue.indexOf("~") >= 0) {
                            //var DailyRate = DailyRateValue.split('~');
                            //var DailyRate1 = parseFloat(isNaN(DailyRate[0]) || $.trim(DailyRate[0]) == '' ? '0.0' : (DailyRate[0] * $.trim(NoOfDays)));
                            // LblDailyRate.Text = DailyRate;
                            GetDeviationAmtCurr();
                        }
                        else
                        {
                            //MsgCls("Please select Region !", lblMessageBoard, Color.Red);
                        }

                    }
                    else
                    {
                        DailyRate = ServicesObj.GetExcRateManagerEdit(ViewState["CREATED_BY"].ToString(), ViewState["CountryEdit"].ToString().Trim(), ViewState["RegoinEdit"].ToString().Trim(), DDLExpenseType.SelectedValue);
                        if ((DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000") == " ")
                        {
                            ViewState["dailyrate"] = "0.000";
                            LblDailyRate.Text = "0.000";
                            dailyratenodays = "0.000";
                        }
                        else
                        {

                            LblDailyRate.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000";
                            dailyratenodays = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.000";
                        }
                        if ((DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "") == " ")
                        {
                            ViewState["dailyratecurr"] = "";
                            LblCurrency.Text = "";
                        }
                        else
                        {

                            LblCurrency.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                        }
                        //string dailyratenodays = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0";
                        //LblDailyRate.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[0] : "0.0";

                        LblDailyRate.Text = (decimal.Parse(dailyratenodays) * decimal.Parse(ViewState["NO_DAYSEdit"].ToString().Trim())).ToString();
                        // LblCurrency.Text = DailyRate.Contains('~') ? DailyRate.Split('~')[1] : "";
                        ViewState["dailyrate"] = LblDailyRate.Text;
                        ViewState["dailyratecurr"] = LblCurrency.Text;
                        // if (DailyRateValue.indexOf("~") >= 0) {
                        //var DailyRate = DailyRateValue.split('~');
                        //var DailyRate1 = parseFloat(isNaN(DailyRate[0]) || $.trim(DailyRate[0]) == '' ? '0.0' : (DailyRate[0] * $.trim(NoOfDays)));
                        // LblDailyRate.Text = DailyRate;
                        GetDeviationAmtCurr();


                    }

                }
                else
                {


                    if (DDLExpenseType.SelectedValue == "0")
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Reimbursement Currency !')", true);
                        MsgCls("Please select Expense Type !", lblMessageBoard, Color.Red);

                    }

                }
                //GetDeviationAmtCurr();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void GetDeviationAmtCurr()
        {
            try
            {
                string ExchangeRate2 = "";
                if ((ViewState["dailyrate"].ToString() != "") && (LblExpenditureAmount.Text.ToString() != "") && (LblExptCurr.Text.ToString() != ""))
                {
                    if ((decimal.Parse(ViewState["dailyrate"].ToString())) >= 0)
                    {
                        if (ViewState["dailyratecurr"].ToString() == LblExptCurr.Text.ToString())
                        {

                            if ((decimal.Parse(ViewState["dailyrate"].ToString())) > decimal.Parse(LblExpenditureAmount.ToString()))
                            {
                                LblDeviation.Text = "0";
                                LblCurrency1.Text = "";
                            }
                            else
                            {
                                LblDeviation.Text = (decimal.Parse(LblExpenditureAmount.Text.ToString()) - (decimal.Parse(ViewState["dailyrate"].ToString()))).ToString("0.00");
                                LblCurrency1.Text = ViewState["dailyratecurr"].ToString();
                            }
                        }
                        else
                        {

                            OtherReimbursementsbl objbl = new OtherReimbursementsbl();
                            OtherReimbursementCollectionbo objLst = objbl.Load_ExchangeRate(LblExptCurr.Text.ToString(), ViewState["dailyratecurr"].ToString().Trim());
                            foreach (OtherReimbursementsbo objBo in objLst)
                            {
                                //MsgCls("", lblIndent, Color.White);
                                ExchangeRate2 = Math.Abs(decimal.Parse(objBo.UKURS.ToString())).ToString();
                                //   objBo.UKURS.ToString();

                            }
                            if ((decimal.Parse(ViewState["dailyrate"].ToString())) > ((decimal.Parse(string.IsNullOrEmpty(ExchangeRate2) ? "0" : ExchangeRate2)) * (decimal.Parse(LblExpenditureAmount.Text.ToString()))))
                            {
                                LblDeviation.Text = "0";
                                LblCurrency1.Text = "";
                            }

                            else
                            {
                                LblDeviation.Text = (((decimal.Parse(string.IsNullOrEmpty(ExchangeRate2) ? "0" : ExchangeRate2)) * (decimal.Parse(LblExpenditureAmount.Text.ToString()))) - (decimal.Parse(ViewState["dailyrate"].ToString()))).ToString("0.00");
                                LblCurrency1.Text = ViewState["dailyratecurr"].ToString();
                            }

                        }
                    }

                    else
                    {
                        LblDeviation.Text = "0";
                        LblDailyRate.Text = "0";
                    }
                }

                else
                {
                    LblDeviation.Text = "0";
                    LblDailyRate.Text = "0";
                    LblCurrency1.Text = "";
                    LblCurrency.Text = "";

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void LbtnUpdateExpTyp_Click(object sender, EventArgs e)
        {
            try
            {
                travelrequestbl travelrequestblObj = new travelrequestbl();
                TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                string exptyp = DDLExpenseType.SelectedValue;
                string dailyrate = LblDailyRate.Text;
                string dailycurr = LblCurrency.Text.Trim();
                string deviaamt = LblDeviation.Text;
                string deviacurr = LblCurrency1.Text.Trim();


                // HF_DailyRate.Value = dailyrate;
                // HF_Deviation.Value = deviaamt;
                // HF_DeCurr.Value = deviacurr;
                //HF_LblRegion.Value=ViewState["RegoinEdit"].ToString();
                //  HF_LblCountry.Value=ViewState["CountryEdit"].ToString();
                //  HF_LblExptCurr.Value = ViewState["EXPT_CURREdit"].ToString();


                ObjTrvlReq.CID = int.Parse(ViewState["CIDEdit"].ToString());
                ObjTrvlReq.LID = int.Parse(ViewState["LIDEdit"].ToString());
                ObjTrvlReq.DAILY_RATE = dailyrate;
                ObjTrvlReq.EXP_TYPE = exptyp;
                ObjTrvlReq.DAILY_CURR = dailycurr;
                ObjTrvlReq.DEVIATION_AMT = deviaamt;
                ObjTrvlReq.DEVIATION_CURR = deviacurr;

                travelrequestblObj.TravelClaimReq_fivalUpdateExptype(ObjTrvlReq);
                ExpEDIT.Visible = false;
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                TrvlReqboList = travelrequestblObj.Load_ClaimDetails(int.Parse(ViewState["CIDEdit"].ToString()));
                grdClaimDetails.DataSource = TrvlReqboList;
                grdClaimDetails.DataBind();

                DataTable dt = ConvertToDataTable(TrvlReqboList);
                decimal d = 0;
                decimal total = dt.AsEnumerable()
                         .Where(r => decimal.TryParse(r.Field<string>("RE_AMT"), out d)).Sum(r => d);

                grdClaimDetails.FooterRow.Cells[7].Text = "Total  :  ";


                grdClaimDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                grdClaimDetails.FooterRow.Cells[8].Text = total.ToString("#,##0.00") + "(" + (ViewState["RcurrforMail"].ToString()) + ")";


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }


        private void LoadDDLExpenseType(string schema, string pernr)
        {

            mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Expensetype_travel(schema, pernr);
            DDLExpenseType.DataSource = objLst;
            DDLExpenseType.DataTextField = "SPTXT";
            DDLExpenseType.DataValueField = "SPKZL";
            DDLExpenseType.DataBind();
            DDLExpenseType.Items.Insert(0, new ListItem("- SELECT -", "0"));
            DDLExpenseType.SelectedValue = "0";
        }


        //public void OnConfirm()
        //{
        //    // int Chkd = 0;
        //    bool? Status = true;
        //    if (grdAppRejTravel.Rows.Count > 0)
        //    {
        //        foreach (GridViewRow item in grdAppRejTravel.Rows)
        //        {
        //            //using (CheckBox chk = (CheckBox)item.FindControl("chkSelected"))
        //            //    if (chk.Checked)
        //            //    {
        //            //        Chkd = Chkd + 1;

        //            travelrequestbl travelrequestblObj = new travelrequestbl();
        //            TrvlReqDetails TrvlReqboList = new TrvlReqDetails();
        //            //ViewState["ProjectforMail"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["WBS_ELEMT"].ToString();
        //            //ViewState["ActivityforMail"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["ACTIVITY"].ToString();
        //            //ViewState["ReamtforMail"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["RE_AMT"].ToString();
        //            //ViewState["RcurrforMail"] = grdAppRejTravel.DataKeys[int.Parse(e.CommandArgument.ToString())]["RCURR"].ToString();
        //            string project = ViewState["ProjectforMail"].ToString();
        //            string Task = ViewState["ActivityforMail"].ToString();
        //            //if (Task == "B")
        //            //{
        //            //    Task = "Billable";
        //            //}
        //            //else
        //            //{
        //            //    Task = "Non-Billable";
        //            //}
        //            string TAmt = ViewState["ReamtforMail"].ToString();
        //            string ReAmt = ViewState["RcurrforMail"].ToString();

        //            if (HFCID != null)
        //            {
        //                TrvlReqboList.CID = int.Parse(HFCID.Value.Trim());
        //                TrvlReqboList.APPROVED_BY1 = User.Identity.Name;
        //                TrvlReqboList.COMMENTS = TxtRemarks.Text == "" ? "APPROVED" : TxtRemarks.Text.Trim();////TxtRemarks.Text.Trim();
        //                TrvlReqboList.STATUS = "Approved";

        //                if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name)
        //                    TrvlReqboList.STATUS = "Approved1";
        //                if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name)
        //                    TrvlReqboList.STATUS = "Approved2";
        //                if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name)
        //                    TrvlReqboList.STATUS = "Approved3";
        //                if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name)
        //                    TrvlReqboList.STATUS = "Approved4";
        //                if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name)
        //                    TrvlReqboList.STATUS = "Approved5";
        //                if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name)
        //                    TrvlReqboList.STATUS = "Approved6";
        //                if (ViewState["APPROVEDBY7"].ToString() == User.Identity.Name)
        //                    TrvlReqboList.STATUS = "Approved7";
        //                if (ViewState["APPROVEDBY8"].ToString() == User.Identity.Name)
        //                    TrvlReqboList.STATUS = "Approved8";
        //                if (ViewState["APPROVEDBY9"].ToString() == User.Identity.Name)
        //                    TrvlReqboList.STATUS = "Approved9";



        //                travelrequestblObj.Update_TravelClaim_Status(TrvlReqboList, ref Status);
        //                if (Status.Equals(false))
        //                {
        //                    ////pnlConfirmation.Visible=false;
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Approved successfully !')", true);
        //                    MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

        //                    SendMailMethodtToEmp(TrvlReqboList, TxtRemarks.Text == "" ? "APPROVED" : TxtRemarks.Text.Trim(), project, Task, TAmt, ReAmt);
        //                    SendMailMethod(TrvlReqboList, project, Task, TAmt, ReAmt);
        //                    LoadTravelClaimGridView();
        //                    TxtRemarks.Text = string.Empty;
        //                    grdClaimDetails.DataSource = null;
        //                    grdClaimDetails.DataBind();
        //                    PnlIExpDetalsView.Visible = false;
        //                }
        //                //Exportbtn.Visible = false;
        //                //  }
        //            }
        //            //if (Chkd == 0)
        //            //{
        //            //    MsgCls("Please select one or more Travel Claim Request to approve !", lblMessageBoard, Color.Red);
        //            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select one or more Travel Claim Request to approve !')", true);
        //            //    return;
        //            //}
        //            LoadTravelClaimGridView();
        //            TxtRemarks.Text = string.Empty;
        //            grdClaimDetails.DataSource = null;
        //            grdClaimDetails.DataBind();
        //            PnlIExpDetalsView.Visible = false;
        //            HFCID = null;
        //        }
        //    }
        //    else
        //    {
        //        lblRemarks.Visible = false;
        //        TxtRemarks.Visible = false;
        //        btnApprove.Visible = false;
        //        btnReject.Visible = false;
        //        MsgCls("There are no Travel Claim Request to Approve !", lblMessageBoard, Color.Red);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no Travel Claim Request to Approve !')", true);
        //        return;
        //    }
        //}

        public void OnConfirm()
        {
            bool? Status = true;
            //if (grdAppRejTravel.Rows.Count > 0)
            //{
            //    foreach (GridViewRow item in grdAppRejTravel.Rows)
            //    {

                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    TrvlReqDetails TrvlReqboList = new TrvlReqDetails();
                    string project = ViewState["ProjectforMail"].ToString();
                    string Task = ViewState["ActivityforMail"].ToString();
                    string TAmt = ViewState["ReamtforMail"].ToString();
                    string ReAmt = ViewState["RcurrforMail"].ToString();

                    if (HFCID != null)
                    {
                        TrvlReqboList.CID = int.Parse(HFCID.Value.Trim());
                        TrvlReqboList.APPROVED_BY1 = User.Identity.Name;
                        TrvlReqboList.COMMENTS = TxtRemarks.Text == "" ? "APPROVED" : TxtRemarks.Text.Trim();////TxtRemarks.Text.Trim();
                        TrvlReqboList.STATUS = "Approved";

                        if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Approved1";
                        if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Approved2";
                        if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Approved3";
                        if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Approved4";
                        if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Approved5";
                        if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Approved6";
                        if (ViewState["APPROVEDBY7"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Approved7";
                        if (ViewState["APPROVEDBY8"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Approved8";
                        if (ViewState["APPROVEDBY9"].ToString() == User.Identity.Name)
                            TrvlReqboList.STATUS = "Approved9";



                        travelrequestblObj.Update_TravelClaim_Status(TrvlReqboList, ref Status);
                        if (Status.Equals(false))
                        {
                            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Approved successfully !')", true);
                            MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                            SendMailMethodtToEmp(TrvlReqboList, TxtRemarks.Text == "" ? "APPROVED" : TxtRemarks.Text.Trim(), project, Task, TAmt, ReAmt);
                            SendMailMethod(TrvlReqboList, project, Task, TAmt, ReAmt);
                            LoadTravelClaimGridView();
                            TxtRemarks.Text = string.Empty;
                            grdClaimDetails.DataSource = null;
                            grdClaimDetails.DataBind();
                            PnlIExpDetalsView.Visible = false;

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Approved successfully !');window.location ='Travel_Requests.aspx?PC=P';", true);
                        }

                    }

                    LoadTravelClaimGridView();
                    TxtRemarks.Text = string.Empty;
                    grdClaimDetails.DataSource = null;
                    grdClaimDetails.DataBind();
                    PnlIExpDetalsView.Visible = false;
                    HFCID = null;
            //    }
            //}
            //else
            //{
            //    lblRemarks.Visible = false;
            //    TxtRemarks.Visible = false;
            //    btnApprove.Visible = false;
            //    btnReject.Visible = false;
            //    MsgCls("There are no Travel Claim Request to Approve !", lblMessageBoard, Color.Red);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no Travel Claim Request to Approve !')", true);
            //    return;
            //}
        }

        protected void btnOK1_Click(object sender, EventArgs e)
        {
            //If ok some code here to execute
            pnlConfirmation.Visible = true;
            OnConfirm();
            //divModalPopup.Visible=false;
            ModalPopupExtender1.Hide();
        }
        ////protected void btncancel_Click(object sender, EventArgs e)
        ////{
        ////    //hide modal popup
        ////    //divModalPopup.Visible = false;
        ////    ModalPopupExtender1.Hide();
        ////    //pnlConfirmation.Visible = false;
        ////}



        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------------------------New code for Pop Up Starts-----------------------
                ////decimal d1 = 0;
                decimal total1 = Convert.ToDecimal(ViewState["dev"].ToString());

                string dev = "Please be informed that this claim is having deviation amount!";


                if (total1 > 0)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirm", "confirm('" + dev + "')", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:if(confirm('Are you sure you want to approve?') == true) return true;", false);
                    //ScriptManager.RegisterStartupScript(this, typeof(string), "confirm","myTestFunction();", true);


                    //divModalPopup.Visible=true;
                    ModalPopupExtender1.Show();
                }
                else
                {
                    OnConfirm();
                }

                //------------------------------------New code for Pop Up Ends-------------------------




            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        //protected void btnReject_Click(object sender, EventArgs e)
        //{
        //    //hide modal popup
        //    pnlConfirmation.Visible = false;

        //    try
        //    {
        //        //int Chkd = 0;
        //        bool? Status = true;
        //        if (grdAppRejTravel.Rows.Count > 0)
        //        {
        //            foreach (GridViewRow item in grdAppRejTravel.Rows)
        //            {
        //                //using (CheckBox chk = (CheckBox)item.FindControl("chkSelected"))
        //                //    if (chk.Checked)
        //                //    {
        //                //        Chkd = Chkd + 1;



        //                travelrequestbl travelrequestblObj = new travelrequestbl();
        //                TrvlReqDetails TrvlReqboList = new TrvlReqDetails();
        //                //string project = item.Cells[4].Text;
        //                //string Task = item.Cells[5].Text;
        //                //if (Task == "B")
        //                //{
        //                //    Task = "Billable";
        //                //}
        //                //else
        //                //{
        //                //    Task = "Non-Billable";
        //                //}
        //                //string TAmt = item.Cells[6].Text;
        //                //string ReAmt = item.Cells[7].Text;
        //                string project = ViewState["ProjectforMail"].ToString();
        //                string Task = ViewState["ActivityforMail"].ToString();
        //                //if (Task == "B")
        //                //{
        //                //    Task = "Billable";
        //                //}
        //                //else
        //                //{
        //                //    Task = "Non-Billable";
        //                //}
        //                string TAmt = ViewState["ReamtforMail"].ToString();
        //                string ReAmt = ViewState["RcurrforMail"].ToString();

        //                if (HFCID != null)
        //                {
        //                    TrvlReqboList.CID = int.Parse(HFCID.Value.Trim());
        //                    TrvlReqboList.APPROVED_BY1 = User.Identity.Name;
        //                    TrvlReqboList.COMMENTS = TxtRemarks.Text.Trim();
        //                    TrvlReqboList.STATUS = "Rejected";

        //                    if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name)
        //                        TrvlReqboList.STATUS = "Rejected1";
        //                    if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name)
        //                        TrvlReqboList.STATUS = "Rejected2";
        //                    if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name)
        //                        TrvlReqboList.STATUS = "Rejected3";
        //                    if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name)
        //                        TrvlReqboList.STATUS = "Rejected4";
        //                    if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name)
        //                        TrvlReqboList.STATUS = "Rejected5";
        //                    if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name)
        //                        TrvlReqboList.STATUS = "Rejected6";
        //                    if (ViewState["APPROVEDBY7"].ToString() == User.Identity.Name)
        //                        TrvlReqboList.STATUS = "Rejected7";
        //                    if (ViewState["APPROVEDBY8"].ToString() == User.Identity.Name)
        //                        TrvlReqboList.STATUS = "Rejected8";
        //                    if (ViewState["APPROVEDBY9"].ToString() == User.Identity.Name)
        //                        TrvlReqboList.STATUS = "Rejected9";


        //                    travelrequestblObj.Update_TravelClaim_Status(TrvlReqboList, ref Status);
        //                    if (Status.Equals(false))
        //                    {
        //                        //divModalPopup.Visible = false;
        //                        ModalPopupExtender1.Hide();
        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Rejected successfully !')", true);
        //                        MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

        //                        RejectSendMailMethodtToEmp(TrvlReqboList, TxtRemarks.Text.Trim(), project, Task, TAmt, ReAmt);
        //                        LoadTravelClaimGridView();
        //                        TxtRemarks.Text = string.Empty;

        //                        grdClaimDetails.DataSource = null;
        //                        grdClaimDetails.DataBind();
        //                        PnlIExpDetalsView.Visible = false;

        //                    }

        //                    // }
        //                }
        //                LoadTravelClaimGridView();
        //                TxtRemarks.Text = string.Empty;

        //                grdClaimDetails.DataSource = null;
        //                grdClaimDetails.DataBind();
        //                PnlIExpDetalsView.Visible = false;
        //                HFCID = null;
        //                //if (Chkd == 0)
        //                //{
        //                //    MsgCls("Please select one Travel Claim Request to reject !", lblMessageBoard, Color.Red);
        //                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select one Travel Claim Request to reject !')", true);
        //                //    return;
        //                //}
        //            }
        //        }
        //        else
        //        {
        //            lblRemarks.Visible = false;
        //            TxtRemarks.Visible = false;
        //            btnApprove.Visible = false;
        //            btnReject.Visible = false;
        //            MsgCls("There are no Travel Claim Request to Reject !", lblMessageBoard, Color.Red);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no Travel Claim Request to Reject !')", true);
        //            return;
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        //}

        protected void btnReject_Click(object sender, EventArgs e)
        {
            pnlConfirmation.Visible = false;

            try
            {
                bool? Status = true;
                //if (grdAppRejTravel.Rows.Count > 0)
                //{
                //    foreach (GridViewRow item in grdAppRejTravel.Rows)
                //    {

                        travelrequestbl travelrequestblObj = new travelrequestbl();
                        TrvlReqDetails TrvlReqboList = new TrvlReqDetails();

                        string project = ViewState["ProjectforMail"].ToString();
                        string Task = ViewState["ActivityforMail"].ToString();

                        string TAmt = ViewState["ReamtforMail"].ToString();
                        string ReAmt = ViewState["RcurrforMail"].ToString();

                        if (HFCID != null)
                        {
                            TrvlReqboList.CID = int.Parse(HFCID.Value.Trim());
                            TrvlReqboList.APPROVED_BY1 = User.Identity.Name;
                            TrvlReqboList.COMMENTS = TxtRemarks.Text.Trim();
                            TrvlReqboList.STATUS = "Rejected";

                            if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Rejected1";
                            if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Rejected2";
                            if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Rejected3";
                            if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Rejected4";
                            if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Rejected5";
                            if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Rejected6";
                            if (ViewState["APPROVEDBY7"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Rejected7";
                            if (ViewState["APPROVEDBY8"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Rejected8";
                            if (ViewState["APPROVEDBY9"].ToString() == User.Identity.Name)
                                TrvlReqboList.STATUS = "Rejected9";


                            travelrequestblObj.Update_TravelClaim_Status(TrvlReqboList, ref Status);
                            if (Status.Equals(false))
                            {
                                ModalPopupExtender1.Hide();
                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Rejected successfully !')", true);
                                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);

                                RejectSendMailMethodtToEmp(TrvlReqboList, TxtRemarks.Text.Trim(), project, Task, TAmt, ReAmt);
                                LoadTravelClaimGridView();
                                TxtRemarks.Text = string.Empty;

                                grdClaimDetails.DataSource = null;
                                grdClaimDetails.DataBind();
                                PnlIExpDetalsView.Visible = false;

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Travel Claim Request Rejected successfully !');window.location ='Travel_Requests.aspx?PC=P';", true);
                            }

                        }
                        LoadTravelClaimGridView();
                        TxtRemarks.Text = string.Empty;

                        grdClaimDetails.DataSource = null;
                        grdClaimDetails.DataBind();
                        PnlIExpDetalsView.Visible = false;
                        HFCID = null;

                //    }
                //}
                //else
                //{
                //    lblRemarks.Visible = false;
                //    TxtRemarks.Visible = false;
                //    btnApprove.Visible = false;
                //    btnReject.Visible = false;
                //    MsgCls("There are no Travel Claim Request to Reject !", lblMessageBoard, Color.Red);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no Travel Claim Request to Reject !')", true);
                //    return;
                //}
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        private void SendMailMethod(TrvlReqDetails TrvlReqboList, string project, string Task, string TAmt, string ReAmt)
        {
            try
            {
                //if (Task == "B")
                //{
                //    Task = "Billable";
                //}
                //else
                //{
                //    Task = "Non-Billable";
                //}
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = false;
                    row.Cells[15].FindControl("fuAttachments").Visible = false;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = false;
                    row.Cells[15].FindControl("LbtnUpload").Visible = false;
                    row.Cells[15].FindControl("LbtnDelete").Visible = false;

                }
                grdClaimDetails.Columns[16].Visible = false;
                grdClaimDetails.FooterRow.ForeColor = System.Drawing.Color.Black;
                string ClaimTotal = grdClaimDetails.FooterRow.Cells[8].Text;
                grdClaimDetails.FooterRow.Visible = true;
                grdClaimDetails.RenderControl(hw1);
                grdClaimDetails.Columns[16].Visible = true;
                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = true;
                    row.Cells[15].FindControl("fuAttachments").Visible = true;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = true;
                    row.Cells[15].FindControl("LbtnUpload").Visible = true;
                    row.Cells[15].FindControl("LbtnDelete").Visible = true;

                }

                string strSubject = string.Empty;

                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVEDBY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                string Project_code = "";
                string Entity = "";


                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_TravelclaimAppNew(TrvlReqboList.CID, TrvlReqboList.APPROVED_BY1, TrvlReqboList.STATUS, ref CREATED_BY, ref APPROVEDBY1, ref Approver_Name,
                 ref Approver_Email, ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref Project_code, ref Entity);


                if (Approver_Email != null)
                {

                    strSubject = "Travel Claim Request " + TrvlReqboList.CID + " has been Raised by " + EMP_Name + "  |  " + CREATED_BY + " and is pending for the Approval.";


                    RecipientsString = Approver_Email;
                    strPernr_Mail = EMP_Email;
                    //Preparing the mail body--------------------------------------------------
                    string body = "<b>Travel Claim Request " + TrvlReqboList.CID + " has been Raised by " + EMP_Name + "  |  " + CREATED_BY + " and is pending for the Approval.<br/><br/></b>";
                    body += "<b>Entity with Claim ID  :  " + Entity + " : " + TrvlReqboList.CID + "</b><br/><br/>";
                    body += "<b>Travel Claim Header Details :<hr /><br/>";
                    body += "Trip ID       :  " + ViewState["@TripNo"].ToString() + "<br/>";
                    body += "Project       :  " + Project_code + " - " + project + "<br/>";
                    body += "Task          :  " + Task + "<br/>";
                    body += "Total Current Claim Reimbursement Amount  :  " + ClaimTotal + "<br/>";
                    body += "Total Trip Claims Reimbursement Amount  :  " + decimal.Parse(TAmt).ToString("#,##0.00") + " ( " + ReAmt + " ) <br/>";
                    // body += "Total Trip Claims Reimbursement Amount  :  " + TAmt + "<br/>";
                    //body += "Reimbursement Currency      :  " + ReAmt + "<br/><br/>";
                    body += "<b>Travel Claim Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";


                    //    //End of preparing the mail body-------------------------------------------

                    ////Newly added Starts
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                    });

                    email.IsBackground = true;
                    email.Start();
                    ////Newly added Ends

                    ////iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);


                    // lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    //lblMessageBoard.Text = "Mail sent successfully.";

                }
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Error in sending mail.";
                return;
            }
        }

        private void SendMailMethodtToEmp(TrvlReqDetails TrvlReqboList, string Remark, string project, string Task, string Tamt, string Reamt)
        {
            try
            {

                //if (Task == "B")
                //{
                //    Task = "Billable";
                //}
                //else
                //{
                //    Task = "Non-Billable";
                //}

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //StringWriter sw2 = new StringWriter();
                //HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                //  FV_PRInfoDisplay.RenderControl(hw);

                //FV_PRInfoDisplay.RenderControl(hw1);
                //grdClaimDetails.RenderControl(hw1);


                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = false;
                    row.Cells[15].FindControl("fuAttachments").Visible = false;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = false;
                    row.Cells[15].FindControl("LbtnUpload").Visible = false;
                    row.Cells[15].FindControl("LbtnDelete").Visible = false;

                }
                grdClaimDetails.Columns[16].Visible = false;
                string ClaimTotal = grdClaimDetails.FooterRow.Cells[8].Text;
                grdClaimDetails.FooterRow.ForeColor = System.Drawing.Color.Black;
                grdClaimDetails.FooterRow.Visible = true;
                grdClaimDetails.RenderControl(hw1);
                grdClaimDetails.Columns[16].Visible = true;
                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = true;
                    row.Cells[15].FindControl("fuAttachments").Visible = true;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = true;
                    row.Cells[15].FindControl("LbtnUpload").Visible = true;
                    row.Cells[15].FindControl("LbtnDelete").Visible = true;

                }


                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVEDBY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                //string Purpose = "";
                string Project_code = "";
                string Entity = "";
                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_TravelclaimAppNew(TrvlReqboList.CID, TrvlReqboList.APPROVED_BY1, TrvlReqboList.STATUS, ref CREATED_BY, ref APPROVEDBY1, ref Approver_Name,
                ref Approver_Email, ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref Project_code, ref Entity);


                strSubject = "Travel Claim Requisition" + TrvlReqboList.CID + " has been approved by " + PRSNTAPPROVEDBY_Name;

                RecipientsString = EMP_Email;
                strPernr_Mail = PRSNTAPPROVEDBY_Email;

                //GridViewRow selectedrow = grdAppRejIexp.SelectedRow;


                //    //Preparing the mail body--------------------------------------------------

                string body = "<b>Travel Claim Requisition " + TrvlReqboList.CID + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + TrvlReqboList.APPROVED_BY1 + "<br/><br/></b>";
                body += "<b>Entity with Claim ID  :  " + Entity + " : " + TrvlReqboList.CID + "</b><br/><br/>";
                body += "<b>Travel Claim Header Details :<hr /><br/>";
                body += "Trip ID       :  " + ViewState["@TripNo"].ToString() + "<br/>";
                body += "Project       :  " + Project_code + " - " + project + "<br/>";
                body += "Task          :  " + Task + "<br/>";
                // body += "Total Current Claim Reimbursement Amount  :  " + decimal.Parse(ClaimTotal).ToString("#,##0.00") + "<br/>";
                body += "Total Current Claim Reimbursement Amount  :  " + ClaimTotal + "<br/>";
                body += "Total Trip Claims Reimbursement Amount  :  " + decimal.Parse(Tamt).ToString("#,##0.00") + " ( " + Reamt + " ) <br/>";
                //body += "Total Reimbursement Amount  :  " + Tamt + "<br/>";
                //body += "Reimbursement Currency      :  " + Reamt + "<br/><br/>";
                body += "<b>Travel Claim Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";
                body += "<b>Remarks  :  " + Remark + "</b><br/>";




                //    //End of preparing the mail body-------------------------------------------


                ////Newly added Starts
                Thread email = new Thread(delegate()
                {
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                });

                email.IsBackground = true;
                email.Start();
                ////Newly added Ends

                ////iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);


                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Travel Claim Request Approved successfully and Mail sent successfully.";

            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Travel Claim Request Approved successfully. Error in sending mail";
                return;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private void RejectSendMailMethodtToEmp(TrvlReqDetails TrvlReqboList, string Remark, string project, string Task, string Tamt, string Reamt)
        {
            try
            {

                //if (Task == "B")
                //{
                //    Task = "Billable";
                //}
                //else
                //{
                //    Task = "Non-Billable";
                //}

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                //grdClaimDetails.RenderControl(hw1);

                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = false;
                    row.Cells[15].FindControl("fuAttachments").Visible = false;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = false;
                    row.Cells[15].FindControl("LbtnUpload").Visible = false;
                    row.Cells[15].FindControl("LbtnDelete").Visible = false;

                }
                grdClaimDetails.Columns[16].Visible = false;
                string ClaimTotal = grdClaimDetails.FooterRow.Cells[8].Text;
                grdClaimDetails.FooterRow.ForeColor = System.Drawing.Color.Black;
                grdClaimDetails.FooterRow.Visible = true;
                grdClaimDetails.RenderControl(hw1);
                grdClaimDetails.Columns[16].Visible = true;
                for (int i = 0; i < grdClaimDetails.Rows.Count; i++)
                {
                    GridViewRow row = grdClaimDetails.Rows[i];
                    //row.Cells[0].Visible = false;
                    row.Cells[15].FindControl("cb").Visible = true;
                    row.Cells[15].FindControl("fuAttachments").Visible = true;
                    row.Cells[15].FindControl("fuAttachmentsfname").Visible = true;
                    row.Cells[15].FindControl("LbtnUpload").Visible = true;
                    row.Cells[15].FindControl("LbtnDelete").Visible = true;

                }

                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVEDBY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                // string Purpose = "";
                string Project_code = "";
                string Entity = "";
                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_TravelclaimAppNew(TrvlReqboList.CID, TrvlReqboList.APPROVED_BY1, TrvlReqboList.STATUS, ref CREATED_BY, ref APPROVEDBY1, ref Approver_Name,
                ref Approver_Email, ref EMP_Name, ref EMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name, ref Project_code, ref Entity);



                strSubject = "Travel Claim Requisition " + TrvlReqboList.CID + " has been Rejected by " + PRSNTAPPROVEDBY_Name;

                RecipientsString = EMP_Email;
                strPernr_Mail = PRSNTAPPROVEDBY_Email;

                GridViewRow selectedrow = grdClaimDetails.SelectedRow;


                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>Travel Claim Requisition " + TrvlReqboList.CID + " has been Rejected by " + PRSNTAPPROVEDBY_Name + "  |  " + TrvlReqboList.APPROVED_BY1 + "<br/><br/></b>";
                body += "<b>Entity with Claim ID  :  " + Entity + " : " + TrvlReqboList.CID + "</b><br/><br/>";
                body += "<b>Travel Claim Header Details :<hr /><br/>";
                body += "Trip ID       :  " + ViewState["@TripNo"].ToString() + "<br/>";
                body += "Project       :  " + Project_code + " - " + project + "<br/>";
                body += "Task          :  " + Task + "<br/>";
                //body += "Total Reimbursement Amount  :  " + Tamt + "<br/>";
                //body += "Reimbursement Currency      :  " + Reamt + "<br/><br/>";
                body += "Total Current Claim Reimbursement Amount  :  " + ClaimTotal + "<br/>";
                body += "Total Trip Claims Reimbursement Amount  :  " + decimal.Parse(Tamt).ToString("#,##0.00") + " ( " + Reamt + " ) <br/>";
                body += "<b>Travel Claim Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";
                body += "<b>Remarks  :  " + Remark + "</b><br/>";


                //    //End of preparing the mail body-------------------------------------------


                ////Newly added Starts
                Thread email = new Thread(delegate()
                {
                    iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                });

                email.IsBackground = true;
                email.Start();
                ////Newly added Ends

                ////iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);

                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Travel Claim Request Rejected and Mail sent successfully.";

            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Travel Claim Request Rejected. Error in sending mail";
                return;
            }
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;

            LoadTravelClaimGridView();
            //  GV_TravelClaimReqAppRej.Visible = false;
            //GV_TravelClaimReqAppRej.Visible = false;
            PnlIExpDetalsView.Visible = false;

            MsgCls("", lblMessageBoard, Color.Transparent);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {


                SearchRecord();
                //MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                //string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                //string textSearch = txtsearch.Text;

                //travelrequestbl travelrequestblObj = new travelrequestbl();
                //List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                //    TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsforAppRej(User.Identity.Name, SelectedType, textSearch);
                //    if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                //    {
                //        MsgCls("No Records found", lblMessageBoard, Color.Red);
                //        grdAppRejTravel.Visible = false;
                //        grdAppRejTravel.DataSource = null;
                //        grdAppRejTravel.DataBind();
                //        PnlIExpDetalsView.Visible = false;
                //        return;
                //    }
                //    else
                //    {
                //        MsgCls("", lblMessageBoard, Color.Transparent);
                //        grdAppRejTravel.Visible = true;
                //        grdAppRejTravel.DataSource = TrvlReqboList;
                //        grdAppRejTravel.SelectedIndex = -1;
                //        grdAppRejTravel.DataBind();
                //        //GV_TravelClaimReqAppRej.Visible = false;
                //        //grdAppRejTravel.Visible = false;
                //        //Panel1.Visible = false;
                //        PnlIExpDetalsView.Visible = false;
                //    }


                //  Session.Add("TravelGrdInfo", TrvlReqboList);




            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, Color.Red);
            }
        }

        public void SearchRecord()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;


                if (SelectedType != "0" && textSearch == "")
                {
                    MsgCls("Please Enter the Text", lblMessageBoard, Color.Red);
                }

                else if (SelectedType == "0" && textSearch != "")
                {
                    MsgCls("Please Select the Type", lblMessageBoard, Color.Red);
                }
                else
                {


                    travelrequestbl travelrequestblObj = new travelrequestbl();
                    List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                    //TrvlReqboList = travelrequestblObj.Load_ParticularTravelDetailsforAppRej(User.Identity.Name, SelectedType, textSearch);
                    if (TrvlReqboList == null || TrvlReqboList.Count == 0)
                    {
                        MsgCls("No Records found", lblMessageBoard, Color.Red);
                        grdAppRejTravel.Visible = false;
                        grdAppRejTravel.DataSource = null;
                        grdAppRejTravel.DataBind();
                        PnlIExpDetalsView.Visible = false;
                        return;
                    }
                    else
                    {
                        MsgCls("", lblMessageBoard, Color.Transparent);
                        grdAppRejTravel.Visible = true;
                        grdAppRejTravel.DataSource = TrvlReqboList;
                        grdAppRejTravel.SelectedIndex = -1;
                        grdAppRejTravel.DataBind();
                        //GV_TravelClaimReqAppRej.Visible = false;
                        //grdAppRejTravel.Visible = false;
                        //Panel1.Visible = false;
                        PnlIExpDetalsView.Visible = false;
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

        protected void grdClaimDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                LinkButton Lbtndownload = e.Row.FindControl("Lbtndownload") as LinkButton;
                if (Lbtndownload != null)
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(Lbtndownload);

                LinkButton LbtnUpload = e.Row.FindControl("LbtnUpload") as LinkButton;
                if (LbtnUpload != null)
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(LbtnUpload);

                LinkButton LbtnDelete = e.Row.FindControl("LbtnDelete") as LinkButton;
                if (LbtnDelete != null)
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(LbtnDelete);








                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DropDownList ddlMyList = (DropDownList)e.Row.FindControl("ddlMyList");

                    //// Do some logic to populate my ddlMyList with data

                    //ddlMyList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                    if (User.Identity.Name.StartsWith("fiad"))
                    {
                        using (LinkButton ltnedit = (LinkButton)e.Row.FindControl("LbtnEDIT"))
                        {
                            ltnedit.Visible = true;
                            // ExpEDIT.Visible = true;
                        }
                    }
                    else
                    {
                        using (LinkButton ltnedit = (LinkButton)e.Row.FindControl("LbtnEDIT"))
                        {
                            ltnedit.Visible = false;
                            // ExpEDIT.Visible = false;
                        }
                    }


                    if (User.Identity.Name.StartsWith("fiadval"))
                    {
                        using (CheckBox chk = (CheckBox)e.Row.FindControl("cb"))
                        using (FileUpload fu = (FileUpload)e.Row.FindControl("fuAttachments"))
                        using (LinkButton ltnfu = (LinkButton)e.Row.FindControl("LbtnUpload"))
                        using (LinkButton ltndelete = (LinkButton)e.Row.FindControl("LbtnDelete"))
                        {
                            string rceiptfile = DataBinder.Eval(e.Row.DataItem, "RECEIPT_FID").ToString();
                            if (rceiptfile == "")
                            {
                                chk.Visible = true;
                                fu.Visible = true;
                                ltnfu.Visible = true;
                                ltndelete.Visible = false;
                            }
                            else
                            {
                                chk.Visible = false;
                                fu.Visible = false;
                                ltnfu.Visible = false;
                                ltndelete.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        using (CheckBox chk = (CheckBox)e.Row.FindControl("cb"))
                        using (FileUpload fu = (FileUpload)e.Row.FindControl("fuAttachments"))
                        using (LinkButton ltnfu = (LinkButton)e.Row.FindControl("LbtnUpload"))
                        using (LinkButton ltndelete = (LinkButton)e.Row.FindControl("LbtnDelete"))
                        {
                            chk.Visible = false;
                            fu.Visible = false;
                            ltnfu.Visible = false;
                            ltndelete.Visible = false;
                        }
                    }
                }


            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, lblMessageBoard, Color.Red); }
        }

        protected void grdClaimDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdClaimDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void DDLExpenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDailyRate();
        }

        protected void grdAppRejHistory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                btnApprove.Visible = false;
                btnReject.Visible = false;
                grdAppRejHistory.EditIndex = e.NewEditIndex;

                travelrequestbl travelrequestblObj = new travelrequestbl();
                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();


                TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                grdAppRejHistory.DataSource = TrvlReqboList;
                grdAppRejHistory.DataBind();



                ViewState["App1formailredirected"] = TrvlReqboList[0].APPROVED_BY1 == null ? "" : TrvlReqboList[0].APPROVED_BY1.ToString();
                ViewState["App2formailredirected"] = TrvlReqboList[0].APPROVED_BY2 == null ? "" : TrvlReqboList[0].APPROVED_BY2.ToString();
                ViewState["App3formailredirected"] = TrvlReqboList[0].APPROVED_BY3 == null ? "" : TrvlReqboList[0].APPROVED_BY3.ToString();
                ViewState["App4formailredirected"] = TrvlReqboList[0].APPROVED_BY4 == null ? "" : TrvlReqboList[0].APPROVED_BY4.ToString();
                ViewState["App5formailredirected"] = TrvlReqboList[0].APPROVED_BY5 == null ? "" : TrvlReqboList[0].APPROVED_BY5.ToString();
                ViewState["App6formailredirected"] = TrvlReqboList[0].APPROVED_BY6 == null ? "" : TrvlReqboList[0].APPROVED_BY6.ToString();
                ViewState["App7formailredirected"] = TrvlReqboList[0].APPROVED_BY7 == null ? "" : TrvlReqboList[0].APPROVED_BY7.ToString();
                ViewState["App8formailredirected"] = TrvlReqboList[0].APPROVED_BY8 == null ? "" : TrvlReqboList[0].APPROVED_BY8.ToString();
                ViewState["App9formailredirected"] = TrvlReqboList[0].APPROVED_BY9 == null ? "" : TrvlReqboList[0].APPROVED_BY9.ToString();




                //ViewState["APPROVEDBY1"] = TrvlReqboList[0].APPROVED_BY1 == null ? "" : TrvlReqboList[0].APPROVED_BY1.ToString();
                //ViewState["APPROVEDBY2"] = TrvlReqboList[0].APPROVED_BY2 == null ? "" : TrvlReqboList[0].APPROVED_BY2.ToString();
                //ViewState["APPROVEDBY3"] = TrvlReqboList[0].APPROVED_BY3 == null ? "" : TrvlReqboList[0].APPROVED_BY3.ToString();
                //ViewState["APPROVEDBY4"] = TrvlReqboList[0].APPROVED_BY4 == null ? "" : TrvlReqboList[0].APPROVED_BY4.ToString();
                //ViewState["APPROVEDBY5"] = TrvlReqboList[0].APPROVED_BY5 == null ? "" : TrvlReqboList[0].APPROVED_BY5.ToString();
                //ViewState["APPROVEDBY6"] = TrvlReqboList[0].APPROVED_BY6 == null ? "" : TrvlReqboList[0].APPROVED_BY6.ToString();
                //ViewState["APPROVEDBY7"] = TrvlReqboList[0].APPROVED_BY7 == null ? "" : TrvlReqboList[0].APPROVED_BY7.ToString();
                //ViewState["APPROVEDBY8"] = TrvlReqboList[0].APPROVED_BY8 == null ? "" : TrvlReqboList[0].APPROVED_BY8.ToString();
                //ViewState["APPROVEDBY9"] = TrvlReqboList[0].APPROVED_BY9 == null ? "" : TrvlReqboList[0].APPROVED_BY9.ToString();
                // PageLoadEvents();

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, lblMessageBoard, Color.Red); }
        }

        //[WebMethod]
        //public CascadingDropDownNameValue[] GetEmployeeNamesAndId(string knownCategoryValues, string category)
        //{
        //    try
        //    {
        //        //masterdalDataContext objDataContext = new masterdalDataContext();
        //        //   mastercollectionbo ObjList = new mastercollectionbo();
        //        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        //        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();
        //        List<CascadingDropDownNameValue> Approvers = new List<CascadingDropDownNameValue>();
        //        var ApproversNames = objTravelRequestDataContext.sp_get_employee_names_forApprover();
        //        foreach (var vRow in ApproversNames)
        //        { Approvers.Add(new CascadingDropDownNameValue(vRow.ENAME, vRow.PERNER)); }
        //        return Approvers.ToArray();
        //    }
        //    catch (Exception Ex)
        //    { throw Ex; }
        //}
        //[WebMethod()]
        //public static CascadingDropDownNameValue[] GetEmployeeNamesAndId()
        //{
        //    try
        //    {
        //        travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();
        //        travelrequestcolumnscollectionbo objList = new travelrequestcolumnscollectionbo();
        //        List<CascadingDropDownNameValue> Approvers = new List<CascadingDropDownNameValue>();
        //        foreach (var vRow in objTravelRequestDataContext.sp_get_employee_names_forApprover())
        //        {
        //            Approvers.Add(new CascadingDropDownNameValue(vRow.ENAME + " - " + vRow.PERNER , vRow.PERNER));
        //        }
        //        return Approvers.ToArray();
        //    }
        //    catch (Exception Ex)
        //     { throw Ex; }
        //}

        protected void grdAppRejHistory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, lblMessageBoard, Color.Red); }
        }

        protected void grdAppRejHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "UPDATE":

                        bool? Status = true;
                        travelrequestbl ObjTrvl = new travelrequestbl();
                        TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                        travelrequestbl travelrequestblObj = new travelrequestbl();

                        List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                        if (ViewState["APPROVEDBY1"] != null)
                        {
                            if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name)
                            {
                                using (DropDownList ddlApp1 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover1"))
                                {
                                    if (ddlApp1 != null)
                                    {
                                        if ((ViewState["APPROVEDBY1"].ToString() != ddlApp1.SelectedValue) && (ViewState["APPROVEDBY2"].ToString() != ddlApp1.SelectedValue)
                                            && (ViewState["APPROVEDBY3"].ToString() != ddlApp1.SelectedValue) && (ViewState["APPROVEDBY4"].ToString() != ddlApp1.SelectedValue)
                                            && (ViewState["APPROVEDBY5"].ToString() != ddlApp1.SelectedValue) && (ViewState["APPROVEDBY6"].ToString() != ddlApp1.SelectedValue)
                                            && (ViewState["APPROVEDBY7"].ToString() != ddlApp1.SelectedValue) && (ViewState["APPROVEDBY8"].ToString() != ddlApp1.SelectedValue)
                                            && (ViewState["APPROVEDBY9"].ToString() != ddlApp1.SelectedValue))
                                        {
                                            ObjTrvlReq.APPROVED_BY1 = ddlApp1.SelectedValue;
                                            ObjTrvlReq.APPROVED_BY2 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY2"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY3 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY3"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY4 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY4"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY5 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY5"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY6 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY6"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY7 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY7"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY8 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY8"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY9 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY9"].ToString().Trim();
                                            ObjTrvlReq.CID = int.Parse(ViewState["CID"].ToString());

                                            travelrequestblObj.Update_TravelClaim_Approvers(ObjTrvlReq, ref Status);

                                            if (Status.Equals(false))
                                            {
                                                grdAppRejHistory.EditIndex = -1;
                                                //TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                                                //grdAppRejHistory.DataSource = TrvlReqboList;
                                                //grdAppRejHistory.DataBind();


                                                if (ViewState["App1formailredirected"].ToString().Trim() != ddlApp1.SelectedValue)
                                                {
                                                    SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp1.SelectedValue);
                                                    btnApprove.Visible = false;
                                                    btnReject.Visible = false;
                                                    LoadTravelClaimGridView();
                                                    PnlIExpDetalsView.Visible = false;
                                                    grdAppRejHistory.Visible = false;
                                                    grdClaimDetails.Visible = false;
                                                    //  MsgCls("Travel Claim Request has been redirected", lblMessageBoard, Color.Green);
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Travel Claim Request has been redirected');", true);
                                                }
                                                else
                                                {
                                                    btnApprove.Visible = true;
                                                    btnReject.Visible = true;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Approver already exists');", true);

                                        }
                                    }

                                }
                            }

                        }

                        if (ViewState["APPROVEDBY2"] != null)
                        {
                            if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name)
                            {
                                using (DropDownList ddlApp2 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover2"))
                                {
                                    if (ddlApp2 != null)
                                    {
                                        //if (ViewState["APPROVEDBY2"].ToString() != ddlApp2.SelectedValue)
                                        if ((ViewState["APPROVEDBY1"].ToString() != ddlApp2.SelectedValue) && (ViewState["APPROVEDBY2"].ToString() != ddlApp2.SelectedValue)
                                            && (ViewState["APPROVEDBY3"].ToString() != ddlApp2.SelectedValue) && (ViewState["APPROVEDBY4"].ToString() != ddlApp2.SelectedValue)
                                            && (ViewState["APPROVEDBY5"].ToString() != ddlApp2.SelectedValue) && (ViewState["APPROVEDBY6"].ToString() != ddlApp2.SelectedValue)
                                            && (ViewState["APPROVEDBY7"].ToString() != ddlApp2.SelectedValue) && (ViewState["APPROVEDBY8"].ToString() != ddlApp2.SelectedValue)
                                            && (ViewState["APPROVEDBY9"].ToString() != ddlApp2.SelectedValue))
                                        {
                                            ObjTrvlReq.APPROVED_BY1 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY1"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY2 = ddlApp2.SelectedValue;
                                            ObjTrvlReq.APPROVED_BY3 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY3"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY4 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY4"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY5 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY5"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY6 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY6"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY7 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY7"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY8 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY8"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY9 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY9"].ToString().Trim();
                                            ObjTrvlReq.CID = int.Parse(ViewState["CID"].ToString());


                                            travelrequestblObj.Update_TravelClaim_Approvers(ObjTrvlReq, ref Status);

                                            if (Status.Equals(false))
                                            {
                                                grdAppRejHistory.EditIndex = -1;
                                                //TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                                                //grdAppRejHistory.DataSource = TrvlReqboList;
                                                //grdAppRejHistory.DataBind();


                                                if (ViewState["App2formailredirected"].ToString().Trim() != ddlApp2.SelectedValue)
                                                {
                                                    SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp2.SelectedValue);
                                                    btnApprove.Visible = false;
                                                    btnReject.Visible = false;
                                                    LoadTravelClaimGridView();
                                                    PnlIExpDetalsView.Visible = false;
                                                    grdAppRejHistory.Visible = false;
                                                    grdClaimDetails.Visible = false;
                                                    //  MsgCls("Travel Claim Request has been redirected", lblMessageBoard, Color.Green);
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Travel Claim Request has been redirected');", true);
                                                }
                                                else
                                                {
                                                    btnApprove.Visible = true;
                                                    btnReject.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                    ///

                                }
                            }

                        }


                        if (ViewState["APPROVEDBY3"] != null)
                        {
                            if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name)
                            {
                                using (DropDownList ddlApp3 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover3"))
                                {
                                    if (ddlApp3 != null)
                                    {
                                        //if (ViewState["APPROVEDBY3"].ToString() != ddlApp3.SelectedValue)
                                        if ((ViewState["APPROVEDBY1"].ToString() != ddlApp3.SelectedValue) && (ViewState["APPROVEDBY2"].ToString() != ddlApp3.SelectedValue)
                                          && (ViewState["APPROVEDBY3"].ToString() != ddlApp3.SelectedValue) && (ViewState["APPROVEDBY4"].ToString() != ddlApp3.SelectedValue)
                                          && (ViewState["APPROVEDBY5"].ToString() != ddlApp3.SelectedValue) && (ViewState["APPROVEDBY6"].ToString() != ddlApp3.SelectedValue)
                                          && (ViewState["APPROVEDBY7"].ToString() != ddlApp3.SelectedValue) && (ViewState["APPROVEDBY8"].ToString() != ddlApp3.SelectedValue)
                                          && (ViewState["APPROVEDBY9"].ToString() != ddlApp3.SelectedValue))
                                        {
                                            ObjTrvlReq.APPROVED_BY1 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY1"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY2 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY2"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY3 = ddlApp3.SelectedValue;
                                            ObjTrvlReq.APPROVED_BY4 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY4"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY5 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY5"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY6 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY6"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY7 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY7"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY8 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY8"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY9 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY9"].ToString().Trim();
                                            ObjTrvlReq.CID = int.Parse(ViewState["CID"].ToString());


                                            travelrequestblObj.Update_TravelClaim_Approvers(ObjTrvlReq, ref Status);

                                            if (Status.Equals(false))
                                            {
                                                grdAppRejHistory.EditIndex = -1;
                                                //TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                                                //grdAppRejHistory.DataSource = TrvlReqboList;
                                                //grdAppRejHistory.DataBind();


                                                if (ViewState["App3formailredirected"].ToString().Trim() != ddlApp3.SelectedValue)
                                                {
                                                    SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp3.SelectedValue);
                                                    btnApprove.Visible = false;
                                                    btnReject.Visible = false;
                                                    LoadTravelClaimGridView();
                                                    PnlIExpDetalsView.Visible = false;
                                                    grdAppRejHistory.Visible = false;
                                                    grdClaimDetails.Visible = false;
                                                    //  MsgCls("Travel Claim Request has been redirected", lblMessageBoard, Color.Green);
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Travel Claim Request has been redirected');", true);
                                                }
                                                else
                                                {
                                                    btnApprove.Visible = true;
                                                    btnReject.Visible = true;
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }


                        if (ViewState["APPROVEDBY4"] != null)
                        {
                            if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name)
                            {
                                using (DropDownList ddlApp4 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover4"))
                                {
                                    if (ddlApp4 != null)
                                    {
                                        //if (ViewState["APPROVEDBY4"].ToString() != ddlApp4.SelectedValue)
                                        if ((ViewState["APPROVEDBY1"].ToString() != ddlApp4.SelectedValue) && (ViewState["APPROVEDBY2"].ToString() != ddlApp4.SelectedValue)
                                          && (ViewState["APPROVEDBY3"].ToString() != ddlApp4.SelectedValue) && (ViewState["APPROVEDBY4"].ToString() != ddlApp4.SelectedValue)
                                          && (ViewState["APPROVEDBY5"].ToString() != ddlApp4.SelectedValue) && (ViewState["APPROVEDBY6"].ToString() != ddlApp4.SelectedValue)
                                          && (ViewState["APPROVEDBY7"].ToString() != ddlApp4.SelectedValue) && (ViewState["APPROVEDBY8"].ToString() != ddlApp4.SelectedValue)
                                          && (ViewState["APPROVEDBY9"].ToString() != ddlApp4.SelectedValue))
                                        {
                                            ObjTrvlReq.APPROVED_BY1 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY1"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY2 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY2"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY3 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY3"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY4 = ddlApp4.SelectedValue;
                                            ObjTrvlReq.APPROVED_BY5 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY5"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY6 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY6"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY7 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY7"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY8 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY8"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY9 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY9"].ToString().Trim();
                                            ObjTrvlReq.CID = int.Parse(ViewState["CID"].ToString());


                                            travelrequestblObj.Update_TravelClaim_Approvers(ObjTrvlReq, ref Status);

                                            if (Status.Equals(false))
                                            {
                                                grdAppRejHistory.EditIndex = -1;
                                                //TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                                                //grdAppRejHistory.DataSource = TrvlReqboList;
                                                //grdAppRejHistory.DataBind();


                                                if (ViewState["App4formailredirected"].ToString().Trim() != ddlApp4.SelectedValue)
                                                {
                                                    SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp4.SelectedValue);
                                                    btnApprove.Visible = false;
                                                    btnReject.Visible = false;
                                                    LoadTravelClaimGridView();
                                                    PnlIExpDetalsView.Visible = false;
                                                    grdAppRejHistory.Visible = false;
                                                    grdClaimDetails.Visible = false;
                                                    //  MsgCls("Travel Claim Request has been redirected", lblMessageBoard, Color.Green);
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Travel Claim Request has been redirected');", true);
                                                }
                                                else
                                                {
                                                    btnApprove.Visible = true;
                                                    btnReject.Visible = true;
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }

                        if (ViewState["APPROVEDBY5"] != null)
                        {
                            if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name)
                            {
                                using (DropDownList ddlApp5 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover5"))
                                {
                                    if (ddlApp5 != null)
                                    {
                                        //if (ViewState["APPROVEDBY5"].ToString() != ddlApp5.SelectedValue)
                                        if ((ViewState["APPROVEDBY1"].ToString() != ddlApp5.SelectedValue) && (ViewState["APPROVEDBY2"].ToString() != ddlApp5.SelectedValue)
                                          && (ViewState["APPROVEDBY3"].ToString() != ddlApp5.SelectedValue) && (ViewState["APPROVEDBY4"].ToString() != ddlApp5.SelectedValue)
                                          && (ViewState["APPROVEDBY5"].ToString() != ddlApp5.SelectedValue) && (ViewState["APPROVEDBY6"].ToString() != ddlApp5.SelectedValue)
                                          && (ViewState["APPROVEDBY7"].ToString() != ddlApp5.SelectedValue) && (ViewState["APPROVEDBY8"].ToString() != ddlApp5.SelectedValue)
                                          && (ViewState["APPROVEDBY9"].ToString() != ddlApp5.SelectedValue))
                                        {

                                            ObjTrvlReq.APPROVED_BY1 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY1"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY2 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY2"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY3 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY3"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY4 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY4"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY5 = ddlApp5.SelectedValue;
                                            ObjTrvlReq.APPROVED_BY6 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY6"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY7 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY7"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY8 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY8"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY9 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY9"].ToString().Trim();
                                            ObjTrvlReq.CID = int.Parse(ViewState["CID"].ToString());


                                            travelrequestblObj.Update_TravelClaim_Approvers(ObjTrvlReq, ref Status);

                                            if (Status.Equals(false))
                                            {
                                                grdAppRejHistory.EditIndex = -1;
                                                //TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                                                //grdAppRejHistory.DataSource = TrvlReqboList;
                                                //grdAppRejHistory.DataBind();


                                                if (ViewState["App5formailredirected"].ToString().Trim() != ddlApp5.SelectedValue)
                                                {
                                                    SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp5.SelectedValue);
                                                    btnApprove.Visible = false;
                                                    btnReject.Visible = false;
                                                    LoadTravelClaimGridView();
                                                    PnlIExpDetalsView.Visible = false;
                                                    grdAppRejHistory.Visible = false;
                                                    grdClaimDetails.Visible = false;
                                                    //  MsgCls("Travel Claim Request has been redirected", lblMessageBoard, Color.Green);
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Travel Claim Request has been redirected');", true);
                                                }
                                                else
                                                {
                                                    btnApprove.Visible = true;
                                                    btnReject.Visible = true;
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }

                        if (ViewState["APPROVEDBY6"] != null)
                        {
                            if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name)
                            {
                                using (DropDownList ddlApp6 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover6"))
                                {
                                    if (ddlApp6 != null)
                                    {
                                        //if (ViewState["APPROVEDBY6"].ToString() != ddlApp6.SelectedValue)
                                        if ((ViewState["APPROVEDBY1"].ToString() != ddlApp6.SelectedValue) && (ViewState["APPROVEDBY2"].ToString() != ddlApp6.SelectedValue)
                                          && (ViewState["APPROVEDBY3"].ToString() != ddlApp6.SelectedValue) && (ViewState["APPROVEDBY4"].ToString() != ddlApp6.SelectedValue)
                                          && (ViewState["APPROVEDBY5"].ToString() != ddlApp6.SelectedValue) && (ViewState["APPROVEDBY6"].ToString() != ddlApp6.SelectedValue)
                                          && (ViewState["APPROVEDBY7"].ToString() != ddlApp6.SelectedValue) && (ViewState["APPROVEDBY8"].ToString() != ddlApp6.SelectedValue)
                                          && (ViewState["APPROVEDBY9"].ToString() != ddlApp6.SelectedValue))
                                        {
                                            ObjTrvlReq.APPROVED_BY1 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY1"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY2 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY2"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY3 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY3"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY4 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY4"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY5 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY5"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY6 = ddlApp6.SelectedValue;
                                            ObjTrvlReq.APPROVED_BY7 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY7"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY8 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY8"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY9 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY9"].ToString().Trim();
                                            ObjTrvlReq.CID = int.Parse(ViewState["CID"].ToString());


                                            travelrequestblObj.Update_TravelClaim_Approvers(ObjTrvlReq, ref Status);

                                            if (Status.Equals(false))
                                            {
                                                grdAppRejHistory.EditIndex = -1;
                                                //TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                                                //grdAppRejHistory.DataSource = TrvlReqboList;
                                                //grdAppRejHistory.DataBind();


                                                if (ViewState["App6formailredirected"].ToString().Trim() != ddlApp6.SelectedValue)
                                                {
                                                    SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp6.SelectedValue);
                                                    btnApprove.Visible = false;
                                                    btnReject.Visible = false;
                                                    LoadTravelClaimGridView();
                                                    PnlIExpDetalsView.Visible = false;
                                                    grdAppRejHistory.Visible = false;
                                                    grdClaimDetails.Visible = false;
                                                    //  MsgCls("Travel Claim Request has been redirected", lblMessageBoard, Color.Green);
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Travel Claim Request has been redirected');", true);
                                                }
                                                else
                                                {
                                                    btnApprove.Visible = true;
                                                    btnReject.Visible = true;
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }

                        if (ViewState["APPROVEDBY7"] != null)
                        {
                            if (ViewState["APPROVEDBY7"].ToString() == User.Identity.Name)
                            {
                                using (DropDownList ddlApp7 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover7"))
                                {
                                    if (ddlApp7 != null)
                                    {
                                        //if (ViewState["APPROVEDBY7"].ToString() != ddlApp7.SelectedValue)
                                        if ((ViewState["APPROVEDBY1"].ToString() != ddlApp7.SelectedValue) && (ViewState["APPROVEDBY2"].ToString() != ddlApp7.SelectedValue)
                                          && (ViewState["APPROVEDBY3"].ToString() != ddlApp7.SelectedValue) && (ViewState["APPROVEDBY4"].ToString() != ddlApp7.SelectedValue)
                                          && (ViewState["APPROVEDBY5"].ToString() != ddlApp7.SelectedValue) && (ViewState["APPROVEDBY6"].ToString() != ddlApp7.SelectedValue)
                                          && (ViewState["APPROVEDBY7"].ToString() != ddlApp7.SelectedValue) && (ViewState["APPROVEDBY8"].ToString() != ddlApp7.SelectedValue)
                                          && (ViewState["APPROVEDBY9"].ToString() != ddlApp7.SelectedValue))
                                        {
                                            ObjTrvlReq.APPROVED_BY1 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY1"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY2 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY2"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY3 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY3"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY4 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY4"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY5 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY5"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY6 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY6"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY7 = ddlApp7.SelectedValue;
                                            ObjTrvlReq.APPROVED_BY8 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY8"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY9 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY9"].ToString().Trim();
                                            ObjTrvlReq.CID = int.Parse(ViewState["CID"].ToString());


                                            travelrequestblObj.Update_TravelClaim_Approvers(ObjTrvlReq, ref Status);

                                            if (Status.Equals(false))
                                            {
                                                grdAppRejHistory.EditIndex = -1;
                                                //TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                                                //grdAppRejHistory.DataSource = TrvlReqboList;
                                                //grdAppRejHistory.DataBind();


                                                if (ViewState["App7formailredirected"].ToString().Trim() != ddlApp7.SelectedValue)
                                                {
                                                    SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp7.SelectedValue);
                                                    btnApprove.Visible = false;
                                                    btnReject.Visible = false;
                                                    LoadTravelClaimGridView();
                                                    PnlIExpDetalsView.Visible = false;
                                                    grdAppRejHistory.Visible = false;
                                                    grdClaimDetails.Visible = false;
                                                    //  MsgCls("Travel Claim Request has been redirected", lblMessageBoard, Color.Green);
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Travel Claim Request has been redirected');", true);
                                                }
                                                else
                                                {
                                                    btnApprove.Visible = true;
                                                    btnReject.Visible = true;
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }

                        if (ViewState["APPROVEDBY8"] != null)
                        {
                            if (ViewState["APPROVEDBY8"].ToString() == User.Identity.Name)
                            {
                                using (DropDownList ddlApp8 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover8"))
                                {
                                    if (ddlApp8 != null)
                                    {
                                        //if (ViewState["APPROVEDBY8"].ToString() != ddlApp8.SelectedValue)
                                        if ((ViewState["APPROVEDBY1"].ToString() != ddlApp8.SelectedValue) && (ViewState["APPROVEDBY2"].ToString() != ddlApp8.SelectedValue)
                                          && (ViewState["APPROVEDBY3"].ToString() != ddlApp8.SelectedValue) && (ViewState["APPROVEDBY4"].ToString() != ddlApp8.SelectedValue)
                                          && (ViewState["APPROVEDBY5"].ToString() != ddlApp8.SelectedValue) && (ViewState["APPROVEDBY6"].ToString() != ddlApp8.SelectedValue)
                                          && (ViewState["APPROVEDBY7"].ToString() != ddlApp8.SelectedValue) && (ViewState["APPROVEDBY8"].ToString() != ddlApp8.SelectedValue)
                                          && (ViewState["APPROVEDBY9"].ToString() != ddlApp8.SelectedValue))
                                        {
                                            ObjTrvlReq.APPROVED_BY1 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY1"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY2 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY2"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY3 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY3"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY4 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY4"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY5 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY5"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY6 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY6"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY7 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY7"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY8 = ddlApp8.SelectedValue;
                                            ObjTrvlReq.APPROVED_BY9 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY9"].ToString().Trim();
                                            ObjTrvlReq.CID = int.Parse(ViewState["CID"].ToString());


                                            travelrequestblObj.Update_TravelClaim_Approvers(ObjTrvlReq, ref Status);

                                            if (Status.Equals(false))
                                            {
                                                grdAppRejHistory.EditIndex = -1;
                                                //TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                                                //grdAppRejHistory.DataSource = TrvlReqboList;
                                                //grdAppRejHistory.DataBind();


                                                if (ViewState["App8formailredirected"].ToString().Trim() != ddlApp8.SelectedValue)
                                                {
                                                    SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp8.SelectedValue);
                                                    btnApprove.Visible = false;
                                                    btnReject.Visible = false;
                                                    LoadTravelClaimGridView();
                                                    PnlIExpDetalsView.Visible = false;
                                                    grdAppRejHistory.Visible = false;
                                                    grdClaimDetails.Visible = false;
                                                    //  MsgCls("Travel Claim Request has been redirected", lblMessageBoard, Color.Green);
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Travel Claim Request has been redirected');", true);
                                                }
                                                else
                                                {
                                                    btnApprove.Visible = true;
                                                    btnReject.Visible = true;
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }


                        if (ViewState["APPROVEDBY9"] != null)
                        {
                            if (ViewState["APPROVEDBY9"].ToString() == User.Identity.Name)
                            {
                                using (DropDownList ddlApp9 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover9"))
                                {
                                    if (ddlApp9 != null)
                                    {
                                        //if (ViewState["APPROVEDBY9"].ToString() != ddlApp9.SelectedValue)
                                        if ((ViewState["APPROVEDBY1"].ToString() != ddlApp9.SelectedValue) && (ViewState["APPROVEDBY2"].ToString() != ddlApp9.SelectedValue)
                                          && (ViewState["APPROVEDBY3"].ToString() != ddlApp9.SelectedValue) && (ViewState["APPROVEDBY4"].ToString() != ddlApp9.SelectedValue)
                                          && (ViewState["APPROVEDBY5"].ToString() != ddlApp9.SelectedValue) && (ViewState["APPROVEDBY6"].ToString() != ddlApp9.SelectedValue)
                                          && (ViewState["APPROVEDBY7"].ToString() != ddlApp9.SelectedValue) && (ViewState["APPROVEDBY8"].ToString() != ddlApp9.SelectedValue)
                                          && (ViewState["APPROVEDBY9"].ToString() != ddlApp9.SelectedValue))
                                        {
                                            ObjTrvlReq.APPROVED_BY1 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY1"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY2 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY2"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY3 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY3"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY4 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY4"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY5 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY5"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY6 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY6"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY7 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY7"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY8 = grdAppRejHistory.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVED_BY8"].ToString().Trim();
                                            ObjTrvlReq.APPROVED_BY9 = ddlApp9.SelectedValue;
                                            ObjTrvlReq.CID = int.Parse(ViewState["CID"].ToString());


                                            travelrequestblObj.Update_TravelClaim_Approvers(ObjTrvlReq, ref Status);

                                            if (Status.Equals(false))
                                            {
                                                grdAppRejHistory.EditIndex = -1;
                                                //TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                                                //grdAppRejHistory.DataSource = TrvlReqboList;
                                                //grdAppRejHistory.DataBind();


                                                if (ViewState["App9formailredirected"].ToString().Trim() != ddlApp9.SelectedValue)
                                                {
                                                    SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp9.SelectedValue);
                                                    btnApprove.Visible = false;
                                                    btnReject.Visible = false;
                                                    LoadTravelClaimGridView();
                                                    PnlIExpDetalsView.Visible = false;
                                                    grdAppRejHistory.Visible = false;
                                                    grdClaimDetails.Visible = false;
                                                    //  MsgCls("Travel Claim Request has been redirected", lblMessageBoard, Color.Green);
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Travel Claim Request has been redirected');", true);
                                                }
                                                else
                                                {
                                                    btnApprove.Visible = true;
                                                    btnReject.Visible = true;
                                                }

                                            }
                                        }
                                    }

                                }
                            }

                        }

                        grdAppRejHistory.EditIndex = -1;
                        TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));
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



                        ////case "UPDATE":
                        ////    bool? Status = true;
                        ////    travelrequestbl ObjTrvl = new travelrequestbl();
                        ////   TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                        ////   travelrequestbl travelrequestblObj = new travelrequestbl();

                        ////   List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();

                        ////    using (DropDownList ddlApp1 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover1"))
                        ////    using (DropDownList ddlApp2 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover2"))
                        ////    using (DropDownList ddlApp3 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover3"))
                        ////    using (DropDownList ddlApp4 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover4"))
                        ////    using (DropDownList ddlApp5 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover5"))
                        ////    using (DropDownList ddlApp6 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover6"))
                        ////    using (DropDownList ddlApp7 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover7"))
                        ////    using (DropDownList ddlApp8 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover8"))
                        ////    using (DropDownList ddlApp9 = (DropDownList)grdAppRejHistory.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("DDLApprover9"))

                        ////    {



                        ////        //if (ddlApp1 != null && ddlApp2 != null && ddlApp3 != null && ddlApp4 != null && ddlApp5 != null && ddlApp6 != null && ddlApp7 != null && ddlApp8 != null && ddlApp9 != null)
                        ////        //{
                        ////        if(ddlApp1 != null)
                        ////        {

                        ////        ObjTrvlReq.APPROVED_BY1 = ddlApp1.SelectedValue;
                        ////        ObjTrvlReq.APPROVED_BY2 = ddlApp2.SelectedValue;
                        ////        ObjTrvlReq.APPROVED_BY3 = ddlApp3.SelectedValue;
                        ////        ObjTrvlReq.APPROVED_BY4 = ddlApp4.SelectedValue;
                        ////        ObjTrvlReq.APPROVED_BY5 = ddlApp5.SelectedValue;
                        ////        ObjTrvlReq.APPROVED_BY6 = ddlApp6.SelectedValue;
                        ////        ObjTrvlReq.APPROVED_BY7 = ddlApp7.SelectedValue;
                        ////        ObjTrvlReq.APPROVED_BY8 = ddlApp8.SelectedValue;
                        ////        ObjTrvlReq.APPROVED_BY9 = ddlApp9.SelectedValue;
                        ////        ObjTrvlReq.CID = int.Parse(ViewState["CID"].ToString());

                        ////      //  TrvlReqDetails TrvlReqboList = new TrvlReqDetails();

                        ////        travelrequestblObj.Update_TravelClaim_Approvers(ObjTrvlReq, ref Status);

                        ////        if (Status.Equals(false))
                        ////        {

                        ////            grdAppRejHistory.EditIndex = -1;
                        ////            TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));

                        ////            grdAppRejHistory.DataSource = TrvlReqboList;
                        ////            grdAppRejHistory.DataBind();


                        ////            if ( ViewState["App1formailredirected"].ToString().Trim() != ddlApp1.SelectedValue)
                        ////            {
                        ////                SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(),ddlApp1.SelectedValue);
                        ////            }
                        ////            else if (ViewState["App2formailredirected"].ToString().Trim() != ddlApp2.SelectedValue)
                        ////            {
                        ////                SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp2.SelectedValue);
                        ////            }

                        ////            else if (ViewState["App3formailredirected"].ToString().Trim() != ddlApp3.SelectedValue)
                        ////            {
                        ////                SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp3.SelectedValue);
                        ////            }

                        ////            else if (ViewState["App4formailredirected"].ToString().Trim() != ddlApp4.SelectedValue)
                        ////            {
                        ////                SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp4.SelectedValue);
                        ////            }
                        ////            else if (ViewState["App5formailredirected"].ToString().Trim() != ddlApp5.SelectedValue)
                        ////            {
                        ////                SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp5.SelectedValue);
                        ////            }
                        ////            else if (ViewState["App6formailredirected"].ToString().Trim() != ddlApp6.SelectedValue)
                        ////            {
                        ////                SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp6.SelectedValue);
                        ////            }
                        ////            else if (ViewState["App7formailredirected"].ToString().Trim() != ddlApp7.SelectedValue)
                        ////            {
                        ////                SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp7.SelectedValue);
                        ////            }
                        ////            else if (ViewState["App8formailredirected"].ToString().Trim() != ddlApp8.SelectedValue)
                        ////            {
                        ////                SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp8.SelectedValue);
                        ////            }
                        ////            else if (ViewState["App9formailredirected"].ToString().Trim() != ddlApp9.SelectedValue)
                        ////            {
                        ////                SendMailMethodChngeApprovers(int.Parse(ViewState["CID"].ToString()), ViewState["ProjectforMail"].ToString(), ViewState["ActivityforMail"].ToString(), ViewState["RcurrforMail"].ToString(), ddlApp9.SelectedValue);
                        ////            }

                        ////            }
                        ////        }
                        ////    }
                        ////       grdAppRejHistory.EditIndex = -1;
                        ////       TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));
                        ////       grdAppRejHistory.DataSource = TrvlReqboList;
                        ////       grdAppRejHistory.DataBind();
                        ////       ViewState["APPROVEDBY1"] = TrvlReqboList[0].APPROVED_BY1 == null ? "" : TrvlReqboList[0].APPROVED_BY1.ToString();
                        ////       ViewState["APPROVEDBY2"] = TrvlReqboList[0].APPROVED_BY2 == null ? "" : TrvlReqboList[0].APPROVED_BY2.ToString();
                        ////       ViewState["APPROVEDBY3"] = TrvlReqboList[0].APPROVED_BY3 == null ? "" : TrvlReqboList[0].APPROVED_BY3.ToString();
                        ////       ViewState["APPROVEDBY4"] = TrvlReqboList[0].APPROVED_BY4 == null ? "" : TrvlReqboList[0].APPROVED_BY4.ToString();
                        ////       ViewState["APPROVEDBY5"] = TrvlReqboList[0].APPROVED_BY5 == null ? "" : TrvlReqboList[0].APPROVED_BY5.ToString();
                        ////       ViewState["APPROVEDBY6"] = TrvlReqboList[0].APPROVED_BY6 == null ? "" : TrvlReqboList[0].APPROVED_BY6.ToString();
                        ////       ViewState["APPROVEDBY7"] = TrvlReqboList[0].APPROVED_BY7 == null ? "" : TrvlReqboList[0].APPROVED_BY7.ToString();
                        ////       ViewState["APPROVEDBY8"] = TrvlReqboList[0].APPROVED_BY8 == null ? "" : TrvlReqboList[0].APPROVED_BY8.ToString();
                        ////       ViewState["APPROVEDBY9"] = TrvlReqboList[0].APPROVED_BY9 == null ? "" : TrvlReqboList[0].APPROVED_BY9.ToString();

                        break;

                    default:
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void SendMailMethodChngeApprovers(int? CID, string Project, string Task, string ReCurrency, string Approvedby)
        {
            try
            {

                //if (Task == "B")
                //{
                //    Task = "Billable";
                //}
                //else
                //{
                //    Task = "Non-Billable";
                //}

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

                grdClaimDetails.Columns[16].Visible = false;
                string ClaimTotal = grdClaimDetails.FooterRow.Cells[8].Text;
                grdClaimDetails.FooterRow.ForeColor = System.Drawing.Color.Black;
                grdClaimDetails.FooterRow.Visible = true;
                grdClaimDetails.RenderControl(hw1);
                grdClaimDetails.Columns[16].Visible = true;


                string strSubject = string.Empty;

                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                //string APPROVED_BY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";
                //string CREATED_BY = "";

                string Project_code = "";
                string Entity = "";
                string TAmt = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";

                travelrequestdalDataContext objTravelRequestDataContext = new travelrequestdalDataContext();

                objTravelRequestDataContext.sp_Get_MailList_TravelclaimChangeApprovers(CID, ViewState["CREATED_BY"].ToString().Trim(), Approvedby, User.Identity.Name, ref Approver_Name,
                 ref Approver_Email, ref EMP_Name, ref EMP_Email, ref Entity, ref Project_code, ref TAmt, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name);


                if (Approver_Email != null)
                {

                    //strSubject = "Travel Claim Request has been Raised by " + EMP_Name + "  |  " + ViewState["CREATED_BY"].ToString().Trim() + " and is pending for the Approval.";
                    strSubject = "Travel Claim Request " + CID + " has been Redirected by " + User.Identity.Name + " |  " + PRSNTAPPROVEDBY_Name + " to you.";
                    //strSubject += "Travel Claim Request has been Raised by " + EMP_Name + "  |  " + ViewState["CREATED_BY"].ToString().Trim() + " and is pending for the Approval.";

                    RecipientsString = Approver_Email;
                    strPernr_Mail = EMP_Email;
                    //Preparing the mail body--------------------------------------------------
                    string body = "<b>Travel Claim Request " + CID + " has been Redirected by " + User.Identity.Name + " |  " + PRSNTAPPROVEDBY_Name + " to you.<br/><br/></b>";

                    body += "<b>Travel Claim Request has been Raised by " + EMP_Name + "  |  " + ViewState["CREATED_BY"].ToString().Trim() + " and is pending for the Approval.<br/><br/></b>";
                    body += "<b>Entity with Claim ID  :  " + Entity + " : " + CID + "</b><br/><br/>";
                    body += "<b>Travel Claim Header Details :<hr /><br/>";
                    body += "Trip ID       :  " + ViewState["@TripNo"].ToString() + "<br/>";
                    body += "Project       :  " + Project_code + " - " + Project + "<br/>";
                    body += "Task          :  " + Task + "<br/>";
                    body += "Total Current Claim Reimbursement Amount  :  " + ClaimTotal + "<br/>";
                    body += "Total Trip Claims Reimbursement Amount  :  " + decimal.Parse(TAmt).ToString("#,##0.00") + " ( " + ReCurrency + " ) <br/>";
                    //body += "Total Reimbursement Amount  :  " + TAmt + "<br/>";
                    //body += "Reimbursement Currency      :  " + ReCurrency + "<br/><br/>";
                    body += "<b>Travel Claim Types Details :</b><hr /><br/>" + sw1.ToString() + "<br/>";


                    //    //End of preparing the mail body-------------------------------------------

                    ////Newly added Starts
                    Thread email = new Thread(delegate()
                    {
                        iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, ViewState["CREATED_BY"].ToString().Trim(), strSubject, strPernr_Mail, body);
                    });

                    email.IsBackground = true;
                    email.Start();
                    ////Newly added Ends

                    ////iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, ViewState["CREATED_BY"].ToString().Trim(), strSubject, strPernr_Mail, body);


                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    lblMessageBoard.Text = "Mail sent successfully.";

                }
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "Unknown error occured. Please contact your system administrator.";
                return;
            }
        }

        protected void grdAppRejHistory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                //travelrequestbl ObjTrvl = new travelrequestbl();
                //TrvlReqDetails ObjTrvlReq = new TrvlReqDetails();
                travelrequestbl travelrequestblObj = new travelrequestbl();

                List<TrvlReqDetails> TrvlReqboList = new List<TrvlReqDetails>();
                grdAppRejHistory.EditIndex = -1;
                TrvlReqboList = travelrequestblObj.Load_ClaimStatusDetails(int.Parse(ViewState["CID"].ToString()));
                grdAppRejHistory.DataSource = TrvlReqboList;
                grdAppRejHistory.DataBind();
                btnApprove.Visible = true;
                btnReject.Visible = true;

            }
            catch (Exception Ex)
            { MsgCls(Ex.Message, lblMessageBoard, Color.Red); }
        }

        protected void grdAppRejHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Edit)
                {
                    if (ViewState["APPROVEDBY1"] != null)
                    {
                        if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name)
                        {
                            using (DropDownList ddla1 = (DropDownList)e.Row.FindControl("DDLApprover1"))
                            {
                                ddla1.Visible = true;
                                ddla1.Enabled = true;


                            }
                        }
                        else
                        {
                            using (Label lbla1 = (Label)e.Row.FindControl("LblApprover1"))
                            {
                                lbla1.Visible = true;
                                lbla1.Enabled = true;
                            }
                        }
                    }

                    if (ViewState["APPROVEDBY2"] != null)
                    {
                        if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name)
                        {
                            using (DropDownList ddla2 = (DropDownList)e.Row.FindControl("DDLApprover2"))
                            {
                                ddla2.Visible = true;
                                ddla2.Enabled = true;

                            }
                        }
                        else
                        {
                            using (Label lbla2 = (Label)e.Row.FindControl("LblApprover2"))
                            {
                                lbla2.Visible = true;
                                lbla2.Enabled = true;
                            }
                        }
                    }

                    if (ViewState["APPROVEDBY3"] != null)
                    {
                        if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name)
                        {
                            using (DropDownList ddla3 = (DropDownList)e.Row.FindControl("DDLApprover3"))
                            {
                                ddla3.Visible = true;
                                ddla3.Enabled = true;

                            }
                        }
                        else
                        {
                            using (Label lbla3 = (Label)e.Row.FindControl("LblApprover3"))
                            {
                                lbla3.Visible = true;
                                lbla3.Enabled = true;
                            }
                        }
                    }
                    if (ViewState["APPROVEDBY4"] != null)
                    {
                        if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name)
                        {
                            using (DropDownList ddla4 = (DropDownList)e.Row.FindControl("DDLApprover4"))
                            {
                                ddla4.Visible = true;
                                ddla4.Enabled = true;

                            }
                        }
                        else
                        {
                            using (Label lbla4 = (Label)e.Row.FindControl("LblApprover4"))
                            {
                                lbla4.Visible = true;
                                lbla4.Enabled = true;
                            }
                        }
                    }
                    if (ViewState["APPROVEDBY5"] != null)
                    {
                        if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name)
                        {
                            using (DropDownList ddla5 = (DropDownList)e.Row.FindControl("DDLApprover5"))
                            {
                                ddla5.Visible = true;
                                ddla5.Enabled = true;

                            }
                        }
                        else
                        {
                            using (Label lbla5 = (Label)e.Row.FindControl("LblApprover5"))
                            {
                                lbla5.Visible = true;
                                lbla5.Enabled = true;
                            }
                        }
                    }
                    if (ViewState["APPROVEDBY6"] != null)
                    {
                        if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name)
                        {
                            using (DropDownList ddla6 = (DropDownList)e.Row.FindControl("DDLApprover6"))
                            {
                                ddla6.Visible = true;
                                ddla6.Enabled = true;

                            }
                        }
                        else
                        {
                            using (Label lbla6 = (Label)e.Row.FindControl("LblApprover6"))
                            {
                                lbla6.Visible = true;
                                lbla6.Enabled = true;
                            }
                        }
                    }
                    if (ViewState["APPROVEDBY7"] != null)
                    {
                        if (ViewState["APPROVEDBY7"].ToString() == User.Identity.Name)
                        {
                            using (DropDownList ddla7 = (DropDownList)e.Row.FindControl("DDLApprover7"))
                            {
                                ddla7.Visible = true;
                                ddla7.Enabled = true;

                            }
                        }
                        else
                        {
                            using (Label lbla7 = (Label)e.Row.FindControl("LblApprover7"))
                            {
                                lbla7.Visible = true;
                                lbla7.Enabled = true;
                            }
                        }
                    }

                    if (ViewState["APPROVEDBY8"] != null)
                    {
                        if (ViewState["APPROVEDBY8"].ToString() == User.Identity.Name)
                        {
                            using (DropDownList ddla8 = (DropDownList)e.Row.FindControl("DDLApprover8"))
                            {
                                ddla8.Visible = true;
                                ddla8.Enabled = true;

                            }
                        }
                        else
                        {
                            using (Label lbla8 = (Label)e.Row.FindControl("LblApprover8"))
                            {
                                lbla8.Visible = true;
                                lbla8.Enabled = true;
                            }
                        }
                    }
                    if (ViewState["APPROVEDBY9"] != null)
                    {
                        if (ViewState["APPROVEDBY9"].ToString() == User.Identity.Name)
                        {
                            using (DropDownList ddla9 = (DropDownList)e.Row.FindControl("DDLApprover9"))
                            {
                                ddla9.Visible = true;
                                ddla9.Enabled = true;

                            }
                        }
                        else
                        {
                            using (Label lbla9 = (Label)e.Row.FindControl("LblApprover9"))
                            {
                                lbla9.Visible = true;
                                lbla9.Enabled = true;
                            }
                        }
                    }

                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        ////protected void grdAppRejTravel_RowDataBound(object sender, GridViewRowEventArgs e)
        ////{
        ////    try
        ////    {
        ////    LinkButton LbtnIExpenseView = e.Row.FindControl("LbtnIExpenseView") as LinkButton;
        ////    if (LbtnIExpenseView != null)
        ////        ScriptManager.GetCurrent(this).RegisterPostBackControl(LbtnIExpenseView);
        ////    }
        ////    catch (Exception Ex)
        ////    { MsgCls(Ex.Message, lblMessageBoard, Color.Red); }
        ////}
    }
}
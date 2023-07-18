using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.RRF;
using iEmpPower.Old_App_Code.iEmpPowerBO.RRF;

namespace iEmpPower.UI.RRF
{
    public partial class RRF_App_rejt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page_init();
                Load_Request_details(3);//3 approve rej details
            }
        }

        protected void Page_init()
        {
            try
            {


                txtsearch_rrf.Text = "";
                DDL_search_RRF.SelectedIndex = -1;
                //Load_pos_to_recut();
                //Load_Edun_Qualitn();
                //Load_Regions();
                //Load_Desig_requs();
                //Load_Projects();
                //Load_Departmnts_requs();
                //Load_Requstr();
                //Load_Emp_Existing();
                //lblIndtrName.Text = Session["EmployeeName"].ToString() + " | " + User.Identity.Name.ToString();
                //pnlPURPSIR.Visible = true;
                //pnlBudgt.Visible = false;
                //btnSubmit.Visible = true;
                //btnClear.Visible = true;

                //clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true);
            }
        }

        public void Load_Request_details(int flg)
        {
            GRD_RRF.DataSource = null;
            GRD_RRF.DataBind();
            RrfBo ObjBo = new RrfBo();
            RrfBl ObjBl = new RrfBl();
            List<RrfBo> objoList = new List<RrfBo>();
            objoList = ObjBl.Get_RRFreq(User.Identity.Name, flg);
            if (objoList.Count > 0)
            {
                GRD_RRF.DataSource = objoList;
                GRD_RRF.DataBind();
                lblMessageBoard.Text = "";
                lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;
                GRD_RRF.Visible = true;
            }
            else
            {
                GRD_RRF.Visible = false;
                lblMessageBoard.Text = "No Records Found...!";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            }
            Session.Add("RRFREQ", objoList);
            ViewState["RecCount"] = objoList.Count;
            ViewState["RecFLG"] = flg;
            FV_RRF_MyReq.Visible = false;
            //EditbtnED();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            RrfBl objBl = new RrfBl();
            RrfBo objBo = new RrfBo();
            TextBox txtAR_rmks = (TextBox)FV_RRF_MyReq.FindControl("txtApprRemarks");
            objBo.REMARKS = txtAR_rmks.Text.Trim();
            objBo.ID = GetReqID(); //Convert.ToInt32(ViewState["RID"]);
            objBo.FLG = 1;
            objBo.APP_PERNR = User.Identity.Name.ToString().Trim();
            string _reqMail = "", _curMail = "", _nxtMail = "", Indntr = "", Desig_name = "", Req_name = "", cur_name = "";
            bool? status = false;
            objBl.RRF_App_rej_sup(objBo, ref status, ref _reqMail, ref _curMail, ref _nxtMail, ref Indntr, ref Desig_name, ref Req_name, ref cur_name);
            if (status == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Request Approved Successfully..!');", true);

                SendMail(1, 1, objBo.ID, Desig_name, _nxtMail, _reqMail, _curMail, Indntr, Desig_name, Req_name, cur_name);
                Load_Request_details(3);//clear();
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            RrfBl objBl = new RrfBl();
            RrfBo objBo = new RrfBo();
            TextBox txtAR_rmks = (TextBox)FV_RRF_MyReq.FindControl("txtApprRemarks");
            objBo.REMARKS = txtAR_rmks.Text.Trim();
            objBo.ID = GetReqID();// Convert.ToInt32(ViewState["RID"]);
            objBo.FLG = 2;
            objBo.APP_PERNR = User.Identity.Name.ToString().Trim();
            string _reqMail = "", _curMail = "", _nxtMail = "", Indntr = "", Desig_name = "", Req_name = "", cur_name = "";
            bool? status = false;
            objBl.RRF_App_rej_sup(objBo, ref status, ref _reqMail, ref _curMail, ref _nxtMail, ref Indntr, ref Desig_name, ref Req_name, ref cur_name); if (status == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Request Rejected Successfully..!');", true);
                SendMail(2, 1, objBo.ID, Desig_name, _nxtMail, _reqMail, _curMail, Indntr, Desig_name, Req_name, cur_name);
                Load_Request_details(3);
                //clear();
            }
        }

        protected int GetReqID()
        {
            Label lblid = (Label)FV_RRF_MyReq.FindControl("lblRRfID");
            int id = Convert.ToInt32(lblid.Text);
            ViewState["RID"] = id;
            return id;
        }

        protected void FV_RRF_MyReq_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {
            //Mp2.Show();
            FV_RRF_MyReq.PageIndex = e.NewPageIndex;

            // int pageindex = e.NewPageIndex;
            // FV_RRF_MyReq.PageIndex = e.NewPageIndex;
            List<RrfBo> objoList = (List<RrfBo>)Session["RRFREQ"];
            // //RrfCollectionBo objPIDashBoardLst = (RrfCollectionBo)Session["RRFREQ"];
            FV_RRF_MyReq.DataSource = objoList;
            // int rselectedindex = Convert.ToInt32(ViewState["indexchang"]);
            // int pagindex = Convert.ToInt32(ViewState["pageindex"]);
            // FV_RRF_MyReq.DataSource = objoList;
            //// FV_RRF_MyReq.SetPageIndex(-1);
            FV_RRF_MyReq.DataBind();
            //if (pageindex == pagindex)
            //{
            //    FV_RRF_MyReq.SetPageIndex( rselectedindex);
            //}
            if ((int)ViewState["RecFLG"] == 3)
            {
                if ((int)ViewState["RecCount"] > 0)
                {
                    using (Panel Pnl = (Panel)FV_RRF_MyReq.FindControl("divAppRej"))
                    { Pnl.Visible = true; }
                }
            }
            else
            {
                if ((int)ViewState["RecCount"] > 0)
                {
                    using (Panel Pnl = (Panel)FV_RRF_MyReq.FindControl("divAppRej"))
                    { Pnl.Visible = false; }
                }
            }
            // EditbtnED();
        }

        protected void FV_RRF_MyReq_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "EDIT":
                        int id = int.Parse(FV_RRF_MyReq.DataKey["ID"].ToString());
                        ViewState["R_ID"] = int.Parse(FV_RRF_MyReq.DataKey["ID"].ToString());

                        // lblIndtrName.Text = FV_RRF_MyReq.DataKey["INDNTR_NAME"].ToString();
                        //ddl_ReqName.SelectedValue= FV_RRF_MyReq.DataKey["REQTR_NAME"].ToString();
                        //ddl_rectPosDesig.SelectedValue = FV_RRF_MyReq.DataKey["DES_RECUTD"].ToString();

                        //rbtnReplceExt.SelectedValue = FV_RRF_MyReq.DataKey["REP_EXT_EMP"].ToString();
                        //DDL_ExtEmpList.SelectedValue = FV_RRF_MyReq.DataKey["REP_EXT_EMP_ID"].ToString();
                        //rbtnBudgted.SelectedValue = FV_RRF_MyReq.DataKey["REQ_POS_BUDGT"].ToString();
                        //txtBudgFrmMonth.Text = FV_RRF_MyReq.DataKey["REQ_POS_BUDGT_FRM_MONTH"].ToString();
                        //txtBudgCost.Text = FV_RRF_MyReq.DataKey["REQ_POS_BUDGT_COST"].ToString();
                        //rtnpurpsOfHrng.SelectedValue = FV_RRF_MyReq.DataKey["PURPS_HIRNG"].ToString();
                        //ddl_PurpsIRLocctn.SelectedValue = FV_RRF_MyReq.DataKey["PURPS_HIRNG_LOC"].ToString();
                        //ddl_PurpsSFProj.SelectedValue = FV_RRF_MyReq.DataKey["PURPS_HIRNG_PROJ"].ToString();
                        //ddl_ReprtsTo.SelectedValue = FV_RRF_MyReq.DataKey["POS_REPT_TO_ID"].ToString();
                        //ddl_MinQuaEdu.SelectedValue = FV_RRF_MyReq.DataKey["MIN_EDU_QLAFTN"].ToString();
                        //txtMinCertifin.Text = FV_RRF_MyReq.DataKey["MIN_CERTIFNTN"].ToString();
                        //txtMinTlExp.Text = FV_RRF_MyReq.DataKey["TOT_EXP"].ToString();
                        //txtMinDomExp.Text = FV_RRF_MyReq.DataKey["TOT_DOMAIN_EXP"].ToString();
                        //txtAraExpRequrd.Text = FV_RRF_MyReq.DataKey["AREA_EXPRTSE"].ToString();
                        //txtOthSpecfiReqmt.Text = FV_RRF_MyReq.DataKey["OTHER_SPC_REQ"].ToString();
                        //txtJobDesp.Text = FV_RRF_MyReq.DataKey["JOB_DISP"].ToString();
                        //txtTentDate.Text = FV_RRF_MyReq.DataKey["TENTTIVE_DATE"].ToString();
                        //txtNoofResource.Text = FV_RRF_MyReq.DataKey["NORESOURCE"].ToString();
                        //load_details_edit((int)ViewState["R_ID"]);
                        break;

                    case "DOWNLOAD":
                        using (HiddenField HF_jobDisp = (HiddenField)FV_RRF_MyReq.FindControl("HF_jobDisp"))
                        {
                            //HF_jobDisp.Visible = false;
                            //lblMessageBoard.Visible = false;

                            string strURL = HF_jobDisp.Value;//e.CommandArgument.ToString(); ;
                            WebClient req = new WebClient();
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ClearContent();
                            response.ClearHeaders();
                            response.Buffer = true;
                            response.ContentType = "application/" + Path.GetExtension(strURL);
                            response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(strURL) + "\"");
                            byte[] data = req.DownloadData(Server.MapPath(strURL));
                            response.BinaryWrite(data);
                            response.End();
                            break;
                        }

                    //string strURL = e.CommandArgument.ToString(); ;
                    //WebClient req = new WebClient();
                    //HttpResponse response = HttpContext.Current.Response;
                    //response.Clear();
                    //response.ClearContent();
                    //response.ClearHeaders();
                    //response.Buffer = true;
                    //response.ContentType = "application/" + Path.GetExtension(strURL);
                    //response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(strURL) + "\"");
                    //byte[] data = req.DownloadData(Server.MapPath(strURL));
                    //response.BinaryWrite(data);
                    //response.End();
                    //break;
                }
            }
            catch (Exception ex) { }
        }

        private void SendMail(int flg, int flg2, int RID, string desig, string nxt_eml, string PERNR_Eml, string curr_eml, string Indntr, string Desig_name, string Req_name, string cur_name)
        {
            try
            {
                string RecipientsString = "";
                string strPernr_Mail = "";
                string strSubject = string.Empty;
                string body = "";
                if (flg2 == 1)//next Approval
                {
                    if (nxt_eml != "")
                    {
                        body = "";
                        //if (flg == 1)
                        strSubject = "RRF " + RID + " has been Raised by " + Req_name + " and is pending for the Approval..";
                        //else
                        //    strSubject = "RRF ID=" + RID + " has been Updated by " + Req_name + " and is pending for the Approval..";

                        RecipientsString = nxt_eml;
                        strPernr_Mail = PERNR_Eml;

                        //    //Preparing the mail body--------------------------------------------------

                        //body = "TO : " + RecipientsString + " CC: " + strPernr_Mail + "<br>";
                        body += "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                        body += "<b>Request Details :</b><br /><hr />";
                        body += "<table style=border-collapse:collapse;>";
                        //if (flg == 1)
                        body += "<tr><td>RRF ID : </td><td>" + RID + "</td></tr>";
                        body += "<tr><td>Indentor : </td><td>" + Indntr + "</td></tr>";
                        body += "<tr><td>Requestor : </td><td>" + Req_name + "</td></tr>";
                        body += "<tr><td>For Designation : </td><td>" + Desig_name + "</td></tr></table>";
                        //else
                        //    body += "<tr><td>R. R. F. has been updated for " + desig + " with requet ID=" + RID + " and waiting for your action.</td></tr></table></b>";

                        body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                 //       Thread email = new Thread(delegate()
                 //{
                     iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                 //});
                 //       email.IsBackground = true;
                 //       email.Start();
                    }
                    //}

                    //else if (flg2 == 2)//Approved mail 
                    //{
                    if (PERNR_Eml != "")
                    {
                        body = "";
                        if (flg == 1)
                            strSubject = "RRF " + RID + " has been Approved by " + cur_name + ".";
                        else
                            strSubject = "RRF " + RID + " has been Rejected by " + cur_name + ".";

                        RecipientsString = PERNR_Eml;
                        strPernr_Mail = curr_eml;

                        //    //Preparing the mail body--------------------------------------------------

                        //body = "TO : " + RecipientsString + " CC: " + strPernr_Mail + "<br>";
                        body += "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                        body += "<b>Request Details :</b><br /><hr />";
                        body += "<table style=border-collapse:collapse;>";
                        //if (flg == 1)
                        body += "<tr><td>RRF ID : </td><td>" + RID + "</td></tr>";
                        body += "<tr><td>Indentor : </td><td>" + Indntr + "</td></tr>";
                        body += "<tr><td>Requestor : </td><td>" + Req_name + "</td></tr>";
                        body += "<tr><td>For Designation : </td><td>" + Desig_name + "</td></tr></table>";
                        //else
                        //    body += "<tr><td>R. R. F. has been updated for " + desig + " with requet ID=" + RID + " and waiting for your action.</td></tr></table></b>";

                        body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                   //     Thread email = new Thread(delegate()
                   //{
                       iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                   //});
                   //     email.IsBackground = true;
                   //     email.Start();
                   }
                }
                //   iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }


        protected void GRD_RRF_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            GRD_RRF.PageIndex = e.NewPageIndex;
            List<RrfBo> objoList = (List<RrfBo>)Session["RRFREQ"];
            GRD_RRF.DataSource = objoList;
            GRD_RRF.SelectedIndex = -1;
            GRD_RRF.DataBind();
            FV_RRF_MyReq.Visible = false;
            //GRD_RRF.PageIndex = e.NewPageIndex;
            //List<RrfBo> objoList = (List<RrfBo>)Session["RRFREQ"];
            //GRD_RRF.DataSource = objoList;
            //GRD_RRF.DataBind();
            //selectiontype();
        }

        protected void GRD_RRF_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                List<RrfBo> objPIDashBoardCmpltdLst = (List<RrfBo>)Session["RRFREQ"];
                // RrfCollectionBo objPIDashBoardCmpltdLst = (RrfCollectionBo)Session["RRFREQ"];
                bool objSortOrder = (bool)Session["bSortedOrder"];
                switch (e.SortExpression.ToString().Trim())
                {
                    case "ID":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                                { return (objBo1.ID.CompareTo(objBo2.ID)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                            { return (objBo2.ID.CompareTo(objBo1.ID)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "IND_ENAME":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                                { return (objBo1.IND_ENAME.CompareTo(objBo2.IND_ENAME)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                            { return (objBo2.IND_ENAME.CompareTo(objBo1.IND_ENAME)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "REQT_ENAME":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                                { return (objBo1.REQT_ENAME.CompareTo(objBo2.REQT_ENAME)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                            { return (objBo2.REQT_ENAME.CompareTo(objBo1.REQT_ENAME)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "DESRTEXT":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                                { return (objBo1.DESRTEXT.CompareTo(objBo2.DESRTEXT)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                            { return (objBo2.DESRTEXT.CompareTo(objBo1.DESRTEXT)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "POS_REPT_TO_ID_ENAME":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                                { return (objBo1.POS_REPT_TO_ID_ENAME.CompareTo(objBo2.POS_REPT_TO_ID_ENAME)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                            { return (objBo2.POS_REPT_TO_ID_ENAME.CompareTo(objBo1.POS_REPT_TO_ID_ENAME)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "STATUS":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                                { return (objBo1.STATUS.CompareTo(objBo2.STATUS)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(RrfBo objBo1, RrfBo objBo2)
                            { return (objBo2.STATUS.CompareTo(objBo1.STATUS)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;
                }


                GRD_RRF.DataSource = objPIDashBoardCmpltdLst;
                GRD_RRF.DataBind();
                Session.Add("RRFREQ", objPIDashBoardCmpltdLst);
                FV_RRF_MyReq.DataSource = null;
                FV_RRF_MyReq.DataBind();
                FV_RRF_MyReq.Visible = false;
                //Session["RRFREQ"] = objPIDashBoardCmpltdLst;
            }
            catch (Exception ex) { }
        }

        protected void GRD_RRF_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
                        int id = int.Parse(GRD_RRF.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString().Trim());
                        FV_RRF_MyReq.DataSource = null;
                        FV_RRF_MyReq.DataBind();
                        RrfBo ObjBo = new RrfBo();
                        RrfBl ObjBl = new RrfBl();
                        List<RrfBo> objoList = new List<RrfBo>();
                        objoList = ObjBl.Get_RRFreq_through_id(id, 1);

                        if (objoList.Count > 0)
                        {
                            FV_RRF_MyReq.DataSource = objoList;
                            FV_RRF_MyReq.DataBind();
                            FV_RRF_MyReq.Visible = true;
                        }
                        ViewState["FVbindcount"] = objoList.Count;
                        //EditbtnED();
                        break;
                }

            }
            catch (Exception ex) { }
        }

        protected void txtsearch_rrf_TextChanged(object sender, EventArgs e)
        {
            GRD_RRF.DataSource = null;
            GRD_RRF.DataBind();
            RrfBo ObjBo = new RrfBo();
            RrfBl ObjBl = new RrfBl();
            List<RrfBo> objoList = new List<RrfBo>();
            objoList = ObjBl.Get_RRFreq_search(User.Identity.Name, 2, 0, Convert.ToInt32(DDL_search_RRF.SelectedValue), txtsearch_rrf.Text.Trim());
            if (objoList.Count > 0)
            {
                GRD_RRF.DataSource = objoList;
                GRD_RRF.DataBind();
                GRD_RRF.Visible = true;
                lblMessageBoard.Text = "";
                lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;
            }
            else
            {
                lblMessageBoard.Text = "No Records Found...!";
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                GRD_RRF.Visible = false;
            }
            Session.Add("RRFREQ", objoList);
            ViewState["RecCount"] = objoList.Count;
            //ViewState["RecFLG"] = flg;
            FV_RRF_MyReq.Visible = false;
            //EditbtnED();
        }

        protected void btnSearchClear_Click(object sender, EventArgs e)
        {
            Load_Request_details(3);
            Page_init();
        }
    }
}
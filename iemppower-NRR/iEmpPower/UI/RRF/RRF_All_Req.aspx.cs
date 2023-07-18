using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.RRF;
using iEmpPower.Old_App_Code.iEmpPowerBO.RRF;
using iEmpPower.Old_App_Code.iEmpPowerBO.RRF.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.RRF;

namespace iEmpPower.UI.RRF
{
    public partial class RRF_All_Req : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page_init();
                Load_Request_details(1);   //1 all details
                bool bSortedOrder = false;
                Session.Add("bSortedOrder", bSortedOrder);
            }
        }

        protected void Page_init()
        {
            try
            {
                txtsearch_rrf.Text = "";
                DDL_search_RRF.SelectedIndex = -1;
                ViewState["FVbindcount"] = 0;

                //GetHRPERNRS();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true);
            }
        }


        public void Load_Request_details(int flg)
        {
            //FV_RRF_MyReq.DataSource = null;
            //FV_RRF_MyReq.DataBind();
            //RrfBo ObjBo = new RrfBo();
            //RrfBl ObjBl = new RrfBl();
            //List<RrfBo> objoList = new List<RrfBo>();
            //objoList = ObjBl.Get_RRFreq(User.Identity.Name, flg);
            //if (objoList.Count > 0)
            //{
            //    FV_RRF_MyReq.DataSource = objoList;
            //    FV_RRF_MyReq.DataBind();
            //}
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
            EditbtnED();
        }

        protected void EditbtnED()
        {
            if ((int)ViewState["FVbindcount"] > 0)
            {
                Label lblstatus = (Label)FV_RRF_MyReq.FindControl("lblStatus");
                if (lblstatus.Text == "Requested" || lblstatus.Text == "Approved" || lblstatus.Text == "Updated")
                    BtnEdit.Visible = true;
                else
                    BtnEdit.Visible = false;
            }
            else
                BtnEdit.Visible = false;
        }

        //protected void btnApprove_Click(object sender, EventArgs e)
        //{
        //    RrfBl objBl = new RrfBl();
        //    RrfBo objBo = new RrfBo();
        //    TextBox txtAR_rmks = (TextBox)FV_RRF_MyReq.FindControl("txtApprRemarks");
        //    objBo.REMARKS = txtAR_rmks.Text.Trim();
        //    objBo.ID = GetReqID(); //Convert.ToInt32(ViewState["RID"]);
        //    objBo.FLG = 1;
        //    objBo.APP_PERNR = User.Identity.Name.ToString().Trim();
        //    string _reqMail = "", _curMail = "", _nxtMail = "";
        //    bool? status = false;
        //    objBl.RRF_App_rej_sup(objBo, ref status, ref _reqMail, ref _curMail, ref _nxtMail);
        //    if (status == true)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Requirment Approved Successfully..!');", true);
        //        //clear();
        //    }
        //}

        //protected void btnReject_Click(object sender, EventArgs e)
        //{
        //    RrfBl objBl = new RrfBl();
        //    RrfBo objBo = new RrfBo();
        //    TextBox txtAR_rmks = (TextBox)FV_RRF_MyReq.FindControl("txtApprRemarks");
        //    objBo.REMARKS = txtAR_rmks.Text.Trim();
        //    objBo.ID = GetReqID();// Convert.ToInt32(ViewState["RID"]);
        //    objBo.FLG = 2;
        //    objBo.APP_PERNR = User.Identity.Name.ToString().Trim();
        //    string _reqMail = "", _curMail = "", _nxtMail = "";
        //    bool? status = false;
        //    objBl.RRF_App_rej_sup(objBo, ref status, ref _reqMail, ref _curMail, ref _nxtMail);
        //    if (status == true)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Requirment Rejected Successfully..!');", true);
        //        //clear();
        //    }
        //}

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
            //if ((int)ViewState["RecFLG"] == 3)
            //{
            //    if ((int)ViewState["RecCount"] > 0)
            //    {
            //        using (Panel Pnl = (Panel)FV_RRF_MyReq.FindControl("divAppRej"))
            //        { Pnl.Visible = true; }
            //    }
            //}
            //else
            //{
            //    if ((int)ViewState["RecCount"] > 0)
            //    {
            //        using (Panel Pnl = (Panel)FV_RRF_MyReq.FindControl("divAppRej"))
            //        { Pnl.Visible = false; }
            //    }
            //}
            //selectiontype();
            EditbtnED();
        }

        protected void rbtnReqTypeStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Mp2.Show();

            // RadioButtonList rbtnReqTypeStatus = (RadioButtonList)FV_RRF_MyReq.FindControl("rbtnReqTypeStatus");
            try
            {
                //selectiontype();
                //lblMessageBoard.Text = ((int)ViewState["RecCount"] > 0) ? "" : "No Records found..!";
            }
            catch (Exception ex) { }
        }

        //protected void selectiontype()
        //{
        //    if (rbtnReqTypeStatus.SelectedValue == "1")
        //    {
        //        Load_Request_details(2);
        //    }
        //    if (rbtnReqTypeStatus.SelectedValue == "2")
        //    {
        //        Load_Request_details(1);
        //    }
        //    //if ((int)ViewState["RecCount"] > 0)
        //    //{
        //    //    //using (Panel Pnl = (Panel)FV_RRF_MyReq.FindControl("divAppRej"))
        //    //    //{
        //    //    //Pnl.Visible = false;
        //    //    lblMessageBoard.Visible = false;
        //    //    //}
        //    //}
        //    //else
        //    //    lblMessageBoard.Text = "No Records found..!";
        //    //lblMessageBoard.Text = ((int)ViewState["RecCount"] > 0) ? "" : "No Records found..!";
        //}

        //protected void GetHRPERNRS()
        //{
        //    string sts = "";
        //    RrfDALDataContext objDataContext = new RrfDALDataContext();
        //    objDataContext.usp_CheckHR(HttpContext.Current.User.Identity.Name, ref sts);
        //    if (sts.Trim().ToUpper() == "TRUE")
        //    {
        //        divreqType.Visible = true;
        //    }
        //    else
        //    {
        //        divreqType.Visible = false;
        //    }

        //}


        protected void FV_RRF_MyReq_ItemCommand1(object sender, FormViewCommandEventArgs e)
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
                }
            }
            catch (Exception ex) { }
        }

        protected void lnkDwnldjobDisp_Click(object sender, EventArgs e)
        {
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
                //break;
            }
        }

        protected void BtnEdit_Click1(object sender, EventArgs e)
        {
            Session.Add("RRF_TRNSURL", HttpContext.Current.Request.Url.AbsolutePath);
            Session.Add("RID_EDIT", GetReqID());
            Response.Redirect("~/UI/RRF/Recmt_Req_Form.aspx", false);
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
            BtnEdit.Visible = false;
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
                BtnEdit.Visible = false;
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
                        GRD_RRF.Visible = true;
                        ViewState["FVbindcount"] = objoList.Count;
                        EditbtnED();
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
            objoList = ObjBl.Get_RRFreq_search(User.Identity.Name, 1, 0, Convert.ToInt32(DDL_search_RRF.SelectedValue), txtsearch_rrf.Text.Trim());
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
            EditbtnED();
        }

        protected void btnSearchClear_Click(object sender, EventArgs e)
        {
            Page_init();
            Load_Request_details(1);   //1 all details
        }
    }
}
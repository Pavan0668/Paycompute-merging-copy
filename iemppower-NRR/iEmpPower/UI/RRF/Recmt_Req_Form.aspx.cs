using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBL.RRF;
using iEmpPower.Old_App_Code.iEmpPowerBO.RRF;
using iEmpPower.Old_App_Code.iEmpPowerBO.RRF.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.RRF;

namespace iEmpPower.UI.RRF
{
    public partial class Recmt_Req_Form : System.Web.UI.Page
    {
        public int rselectedindex = -1;
        protected int PendingPageIndex = 1;
        protected int CompleatedPageIndex = 1;
        //  int _Rid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page_init();

            }
            if (Session["RID_EDIT"] != null)
            {
                ViewState["_RIDEDIT"] = Convert.ToInt32(Session["RID_EDIT"]);
                load_details_edit(Convert.ToInt32(Session["RID_EDIT"]));
                //ViewState["_RIDEDIT"] = Convert.ToInt32(Session["RID_EDIT"]);
                //HF_RID.Value = Session["RID_EDIT"].ToString();
                btnSubmit.Visible = false;
                btnClear.Visible = false;
                btnCancl.Visible = true;
                btnUpdate.Visible = true;
            }

        }

        protected void Page_init()
        {
            try
            {
                ViewState["RecCount"] = 0;
                //Load_Request_details(1);
                Load_pos_to_recut();
                Load_Edun_Qualitn();
                Load_Regions();
                Load_Desig_requs();
                Load_Projects();
                Load_Departmnts_requs();
                Load_Requstr();
                Load_Emp_Existing();
                lblIndtrName.Text = Session["EmployeeName"].ToString() + " | " + User.Identity.Name.ToString();
                pnlPURPSIR.Visible = true;
                pnlBudgt.Visible = false;
                btnSubmit.Visible = true;
                btnClear.Visible = true;

                clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true);
            }
        }

        protected void rbtnBudgted_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnBudgted.SelectedValue == "False")
            {
                pnlBudgt.Visible = false;
            }
            else if (rbtnBudgted.SelectedValue == "True")
            {
                pnlBudgt.Visible = true;
            }
        }

        protected void rtnpurpsOfHrng_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rtnpurpsOfHrng.SelectedValue == "IR")
            {
                pnlPURPSIR.Visible = true;
                pnlPurpsSF.Visible = false;
            }
            else if (rtnpurpsOfHrng.SelectedValue == "SF")
            {
                pnlPurpsSF.Visible = true;
                pnlPURPSIR.Visible = true;
            }
        }

        protected void Load_Requstr()
        {
            RrfCollectionBo objLst = RrfBl.Load_reqtr_dtls();
            ddl_ReqName.DataSource = objLst;
            ddl_ReqName.DataTextField = "ENAME";
            ddl_ReqName.DataValueField = "PERNR";
            ddl_ReqName.DataBind();
            ddl_ReqName.Items.Insert(0, new ListItem(" - Select Requestor - ", "0"));
        }

        protected void Load_Departmnts_requs()
        {
            RrfCollectionBo objLst = RrfBl.Load_Departmnts();
            ddl_dept.DataSource = objLst;
            ddl_dept.DataTextField = "ORGTX";
            ddl_dept.DataValueField = "ORGEH";
            ddl_dept.DataBind();
            ddl_dept.Items.Insert(0, new ListItem(" - Select Department - ", "0"));
        }

        protected void Load_Desig_requs()
        {
            RrfCollectionBo objLst = RrfBl.Load_desig_dtls();
            ddl_req_desig.DataSource = objLst;
            ddl_req_desig.DataTextField = "PLSXT";
            ddl_req_desig.DataValueField = "PLANS";
            ddl_req_desig.DataBind();
            ddl_req_desig.Items.Insert(0, new ListItem(" - Select Designation - ", "0"));
        }

        protected void Load_Projects()
        {
            RrfCollectionBo objLst = RrfBl.Load_projects();
            ddl_PurpsSFProj.DataSource = objLst;
            ddl_PurpsSFProj.DataTextField = "POST1";
            ddl_PurpsSFProj.DataValueField = "PSPNR";
            ddl_PurpsSFProj.DataBind();
            ddl_PurpsSFProj.Items.Insert(0, new ListItem("- 8.2  Select Projects -", "0"));
        }

        protected void Load_Regions()
        {
            RrfCollectionBo objLst = RrfBl.Load_regions();
            ddl_PurpsIRLocctn.DataSource = objLst;
            ddl_PurpsIRLocctn.DataTextField = "TEXT25";
            ddl_PurpsIRLocctn.DataValueField = "RGION";
            ddl_PurpsIRLocctn.DataBind();
            ddl_PurpsIRLocctn.Items.Insert(0, new ListItem("- 8.1 Select Location -", "0"));
        }

        protected void Load_pos_to_recut()
        {
            RrfCollectionBo objLst = RrfBl.Load_desig_dtls();
            ddl_rectPosDesig.DataSource = objLst;
            ddl_rectPosDesig.DataTextField = "PLSXT";
            ddl_rectPosDesig.DataValueField = "PLANS";
            ddl_rectPosDesig.DataBind();
            ddl_rectPosDesig.Items.Insert(0, new ListItem(" - Select Designation - ", "0"));
        }

        //protected void Load_Rects_to()
        //{
        //    RrfCollectionBo objLst = RrfBl.Load_reqtr_dtls();
        //    ddl_ReprtsTo.DataSource = objLst;
        //    ddl_ReprtsTo.DataTextField = "ENAME";
        //    ddl_ReprtsTo.DataValueField = "PERNR";
        //    ddl_ReprtsTo.DataBind();
        //    ddl_ReprtsTo.Items.Insert(0, new ListItem(" - Select Reporting To - ", "0"));
        //}

        protected void Load_requstr_detailsonChange(string DDL_PERNR)
        {
            RrfCollectionBo objLst = RrfBl.Load_reqtr_dtls();
            var res = from row in objLst//dt.AsEnumerable()
                      where row.PERNR == DDL_PERNR
                      select row;
            foreach (RrfBo j in res)
            {
                ddl_dept.SelectedValue = j.ORGEH.ToString();
                ddl_req_desig.SelectedValue = j.PLANS;
            }
        }

        protected void ddl_ReqName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_requstr_detailsonChange(ddl_ReqName.SelectedValue.ToString().Trim());
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
            }
            catch (Exception ex) { }
        }

        protected void clear()
        {
            try
            {
                ddl_ReqName.SelectedIndex = -1;
                ddl_req_desig.SelectedIndex = -1;
                ddl_dept.SelectedIndex = -1;
                ddl_rectPosDesig.SelectedIndex = -1;
                rbtnReplceExt.SelectedValue = "False";
                rbtnBudgted.SelectedValue = "False";
                rtnpurpsOfHrng.SelectedValue = "IR";
                ddl_ReprtsTo.SelectedIndex = -1;
                ddl_MinQuaEdu.SelectedIndex = -1;
                txtMinCertifin.Text = "";
                txtMinTlExp.Text = "";
                txtMinDomExp.Text = "";
                txtAraExpRequrd.Text = "";
                txtOthSpecfiReqmt.Text = "";
                txtTentDate.Text = "";
                DDL_ExtEmpList.SelectedIndex = -1;
                txtJobDesp.Text = "";
                if (rbtnReplceExt.SelectedValue == "True")
                {
                    PanelExtEmp.Visible = true;
                }
                else if (rbtnReplceExt.SelectedValue == "False")
                {
                    PanelExtEmp.Visible = false;
                    DDL_ExtEmpList.Visible = false;
                    RRF_DDL_ExtEmpList.Visible = false;
                }

                if (rbtnBudgted.SelectedValue == "False")
                {
                    pnlBudgt.Visible = false;
                }
                else if (rbtnBudgted.SelectedValue == "True")
                {
                    pnlBudgt.Visible = true;
                }

                if (rtnpurpsOfHrng.SelectedValue == "IR")
                {
                    pnlPURPSIR.Visible = true;
                    pnlPurpsSF.Visible = false;
                }
                else if (rtnpurpsOfHrng.SelectedValue == "SF")
                {
                    pnlPurpsSF.Visible = true;
                    pnlPURPSIR.Visible = true;
                }
                DDL_ExtEmpList.SelectedIndex = -1;
                txtBudgCost.Text = "";
                txtBudgFrmMonth.Text = "";
                ddl_PurpsIRLocctn.SelectedIndex = -1;
                ddl_PurpsSFProj.SelectedIndex = -1;
                lnkViewfile.Visible = false;
            }
            catch (Exception ex) { }
        }

        protected void rbtnReplceExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnReplceExt.SelectedValue == "True")
            {
                PanelExtEmp.Visible = true;
                DDL_ExtEmpList.Visible = true;
                RRF_DDL_ExtEmpList.Visible = true;
            }
            else if (rbtnReplceExt.SelectedValue == "False")
            {
                PanelExtEmp.Visible = false;
                DDL_ExtEmpList.Visible = false;
                RRF_DDL_ExtEmpList.Visible = false;
            }
        }

        protected void Load_Emp_Existing()
        {
            RrfCollectionBo objLst = RrfBl.Load_Employees();
            DDL_ExtEmpList.DataSource = objLst;
            DDL_ExtEmpList.DataTextField = "ENAME";
            DDL_ExtEmpList.DataValueField = "PERNR";
            DDL_ExtEmpList.DataBind();
            DDL_ExtEmpList.Items.Insert(0, new ListItem(" - 6.1 Select Existing Emp - ", "0"));
        }

        protected void Load_Rects_to(string PLANS)
        {
            RrfCollectionBo objLst = RrfBl.Load_Pos_manager(PLANS);
            ddl_ReprtsTo.DataSource = objLst;
            ddl_ReprtsTo.DataTextField = "ENAME";
            ddl_ReprtsTo.DataValueField = "PERNR";
            ddl_ReprtsTo.DataBind();
            ddl_ReprtsTo.Items.Insert(0, new ListItem(" - Select Reporting To - ", "0"));
        }

        protected void ddl_rectPosDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Load_Rects_to(ddl_rectPosDesig.SelectedValue.ToString().Trim());
            }
            catch (Exception ex) { }
        }

        protected void Load_Edun_Qualitn()
        {
            RrfCollectionBo objLst = RrfBl.Load_Edu_Qlatn();
            ddl_MinQuaEdu.DataSource = objLst;
            ddl_MinQuaEdu.DataTextField = "QNAME";
            ddl_MinQuaEdu.DataValueField = "QID";
            ddl_MinQuaEdu.DataBind();
            ddl_MinQuaEdu.Items.Insert(0, new ListItem(" - Select Edu. Qualification - ", "0"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                RrfBo objBo = new RrfBo();
                objBo.INDNTR_NAME = User.Identity.Name.ToString().Trim();
                objBo.REQTR_NAME = ddl_ReqName.SelectedValue.ToString().Trim();
                objBo.DES_RECUTD = ddl_rectPosDesig.SelectedValue.ToString().Trim();
                objBo.REP_EXT_EMP = Convert.ToBoolean(rbtnReplceExt.SelectedValue.ToString().Trim());
                objBo.REP_EXT_EMP_ID = DDL_ExtEmpList.SelectedValue.ToString().Trim();
                objBo.REQ_POS_BUDGT = Convert.ToBoolean(rbtnBudgted.SelectedValue.ToString().Trim());
                objBo.REQ_POS_BUDGT_FRM_MONTH = txtBudgFrmMonth.Text.ToString().Trim();
                objBo.REQ_POS_BUDGT_COST = txtBudgCost.Text == "" ? 0 : Convert.ToDouble(txtBudgCost.Text);
                objBo.PURPS_HIRNG = rtnpurpsOfHrng.SelectedValue.ToString().Trim();
                objBo.PURPS_HIRNG_LOC = ddl_PurpsIRLocctn.SelectedValue.ToString().Trim();
                objBo.PURPS_HIRNG_PROJ = ddl_PurpsSFProj.SelectedValue.ToString().Trim();
                objBo.POS_REPT_TO_ID = ddl_ReprtsTo.SelectedValue.ToString().Trim();
                objBo.MIN_EDU_QLAFTN = Convert.ToInt32(ddl_MinQuaEdu.SelectedValue);
                objBo.MIN_CERTIFNTN = txtMinCertifin.Text.ToString().Trim();
                objBo.TOT_EXP = txtMinTlExp.Text.ToString().Trim();
                objBo.TOT_DOMAIN_EXP = txtMinDomExp.Text.ToString().Trim();
                objBo.AREA_EXPRTSE = txtAraExpRequrd.Text.ToString().Trim();
                objBo.OTHER_SPC_REQ = txtOthSpecfiReqmt.Text.ToString().Trim();
                objBo.JOB_DISP = txtJobDesp.Text.ToString().Trim();

                if (FU_JobDesp.HasFile)
                {
                    string date1 = DateTime.Now.ToString("dd_MM_yyyy");
                    objBo.DISP_FILE += string.Format(FU_JobDesp.HasFile ? "~/RRF_Doc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(FU_JobDesp.FileName) + "-" + date1 + Path.GetExtension(FU_JobDesp.FileName) : "");
                    FU_JobDesp.SaveAs(Server.MapPath("~/RRF_Doc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(FU_JobDesp.FileName) + "-" + date1) + Path.GetExtension(FU_JobDesp.FileName));
                }

                //  objBo.DISP_FILE = FU_JobDesp.FileName.ToString().Trim();
                objBo.TENTTIVE_DATE = Convert.ToDateTime(txtTentDate.Text);
                objBo.NORESOURCE = Convert.ToInt32(txtNoofResource.Text);

                RrfBl objBl = new RrfBl();
                int? ErrorCode = 0, RID = 0;
                string Reqmail = "", Supmail = "";
                objBl.CREATE_RRF_REC(objBo, 1, ref ErrorCode, ref Reqmail, ref Supmail, ref RID);
                if (ErrorCode == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Request Raised Successfully..!');", true);
                    SendMail(1, Convert.ToInt32(RID), ddl_rectPosDesig.SelectedItem.ToString().Trim(), Supmail, Reqmail);
                    clear();
                }
            }
            catch (Exception ex) { }
        }

        //public void Load_Request_details(int flg)
        //{
        //    FV_RRF_MyReq.DataSource = null;
        //    FV_RRF_MyReq.DataBind();
        //    RrfBo ObjBo = new RrfBo();
        //    RrfBl ObjBl = new RrfBl();
        //    List<RrfBo> objoList = new List<RrfBo>();
        //    objoList = ObjBl.Get_RRFreq(User.Identity.Name, flg);
        //    if (objoList.Count > 0)
        //    {
        //        FV_RRF_MyReq.DataSource = objoList;
        //        FV_RRF_MyReq.DataBind();
        //    }
        //    Session.Add("RRFREQ", objoList);
        //    ViewState["RecCount"] = objoList.Count;
        //    ViewState["RecFLG"] = flg;
        //    EditbtnED();
        //}

        //protected void FV_RRF_MyReq_PageIndexChanging(object sender, FormViewPageEventArgs e)
        //{
        //   // Mp2.Show();
        //    FV_RRF_MyReq.PageIndex = e.NewPageIndex;

        //    // int pageindex = e.NewPageIndex;
        //    // FV_RRF_MyReq.PageIndex = e.NewPageIndex;
        //    List<RrfBo> objoList = (List<RrfBo>)Session["RRFREQ"];
        //    // //RrfCollectionBo objPIDashBoardLst = (RrfCollectionBo)Session["RRFREQ"];
        //    FV_RRF_MyReq.DataSource = objoList;
        //    // int rselectedindex = Convert.ToInt32(ViewState["indexchang"]);
        //    // int pagindex = Convert.ToInt32(ViewState["pageindex"]);
        //    // FV_RRF_MyReq.DataSource = objoList;
        //    //// FV_RRF_MyReq.SetPageIndex(-1);
        //    FV_RRF_MyReq.DataBind();
        //    //if (pageindex == pagindex)
        //    //{
        //    //    FV_RRF_MyReq.SetPageIndex( rselectedindex);
        //    //}
        //    if ((int)ViewState["RecFLG"] == 3)
        //    {
        //        if ((int)ViewState["RecCount"] > 0)
        //        {
        //            using (Panel Pnl = (Panel)FV_RRF_MyReq.FindControl("divAppRej"))
        //            { Pnl.Visible = true; }
        //        }
        //    }
        //    else
        //    {
        //        if ((int)ViewState["RecCount"] > 0)
        //        {
        //            using (Panel Pnl = (Panel)FV_RRF_MyReq.FindControl("divAppRej"))
        //            { Pnl.Visible = false; }
        //        }
        //    }
        //    EditbtnED();
        //}

        protected void FV_RRF_MyReq_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                //case "EDIT":
                //    int id = int.Parse(FV_RRF_MyReq.DataKey["ID"].ToString());
                //    ViewState["R_ID"] = int.Parse(FV_RRF_MyReq.DataKey["ID"].ToString());

                //    lblIndtrName.Text = FV_RRF_MyReq.DataKey["INDNTR_NAME"].ToString();
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
                // break;

                case "DOWNLOAD":

                    string strURL = e.CommandArgument.ToString(); ;
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                RrfBo objBo = new RrfBo();
                objBo.RID = Convert.ToInt32(ViewState["_RIDEDIT"]);//Convert.ToInt32(Session["RID_EDIT"]);
                objBo.INDNTR_NAME = User.Identity.Name.ToString().Trim();
                objBo.REQTR_NAME = ddl_ReqName.SelectedValue.ToString().Trim();
                objBo.DES_RECUTD = ddl_rectPosDesig.SelectedValue.ToString().Trim();
                objBo.REP_EXT_EMP = Convert.ToBoolean(rbtnReplceExt.SelectedValue.ToString().Trim());
                objBo.REP_EXT_EMP_ID = DDL_ExtEmpList.SelectedValue.ToString().Trim();
                objBo.REQ_POS_BUDGT = Convert.ToBoolean(rbtnBudgted.SelectedValue.ToString().Trim());
                objBo.REQ_POS_BUDGT_FRM_MONTH = txtBudgFrmMonth.Text.ToString().Trim();
                objBo.REQ_POS_BUDGT_COST = txtBudgCost.Text == "" ? 0 : Convert.ToDouble(txtBudgCost.Text);
                objBo.PURPS_HIRNG = rtnpurpsOfHrng.SelectedValue.ToString().Trim();
                objBo.PURPS_HIRNG_LOC = ddl_PurpsIRLocctn.SelectedValue.ToString().Trim();
                objBo.PURPS_HIRNG_PROJ = ddl_PurpsSFProj.SelectedValue.ToString().Trim();
                objBo.POS_REPT_TO_ID = ddl_ReprtsTo.SelectedValue.ToString().Trim();
                objBo.MIN_EDU_QLAFTN = Convert.ToInt32(ddl_MinQuaEdu.SelectedValue);
                objBo.MIN_CERTIFNTN = txtMinCertifin.Text.ToString().Trim();
                objBo.TOT_EXP = txtMinTlExp.Text.ToString().Trim();
                objBo.TOT_DOMAIN_EXP = txtMinDomExp.Text.ToString().Trim();
                objBo.AREA_EXPRTSE = txtAraExpRequrd.Text.ToString().Trim();
                objBo.OTHER_SPC_REQ = txtOthSpecfiReqmt.Text.ToString().Trim();
                objBo.JOB_DISP = txtJobDesp.Text.ToString().Trim();

                if (FU_JobDesp.HasFile)
                {
                    string date1 = DateTime.Now.ToString("dd_MM_yyyy");
                    objBo.DISP_FILE += string.Format(FU_JobDesp.HasFile ? "~/RRF_Doc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(FU_JobDesp.FileName) + "-" + date1 + Path.GetExtension(FU_JobDesp.FileName) : "");
                    FU_JobDesp.SaveAs(Server.MapPath("~/RRF_Doc/" + User.Identity.Name + "-" + Path.GetFileNameWithoutExtension(FU_JobDesp.FileName) + "-" + date1) + Path.GetExtension(FU_JobDesp.FileName));
                }

                //  objBo.DISP_FILE = FU_JobDesp.FileName.ToString().Trim();
                objBo.TENTTIVE_DATE = Convert.ToDateTime(txtTentDate.Text);
                objBo.NORESOURCE = Convert.ToInt32(txtNoofResource.Text);

                RrfBl objBl = new RrfBl();
                int? ErrorCode = 0;
                string Reqmail = "", Supmail = "";
                objBl.UPDATE_RRF_REC(objBo, 1, ref ErrorCode, ref Reqmail, ref Supmail);
                if (ErrorCode == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Request Updated Successfully..!');", true);
                    SendMail(2, Convert.ToInt32(ViewState["_RIDEDIT"]), ddl_rectPosDesig.SelectedItem.ToString().Trim(), Supmail, Reqmail);
                    clear();
                    btnSubmit.Visible = true;
                    btnClear.Visible = true;
                    btnCancl.Visible = false;
                    btnUpdate.Visible = false;
                    Response.Redirect(Session["RRF_TRNSURL"].ToString(), false);
                }
            }
            catch (Exception ex) { }
        }

        public void load_details_edit(int RID)
        {
            //Label lblid = (Label)FV_RRF_MyReq.FindControl("lblRRfID");
            int id = RID; //Convert.ToInt32(lblid.Text);
            //ViewState["RID"] = id;
            RrfBl ObjBl = new RrfBl();
            //  objBl.Get_RRF_ID_Dtls(id, 1);
            List<RrfBo> objoList = new List<RrfBo>();
            objoList = ObjBl.Get_RRF_ID_Dtls(id, 1);
            foreach (RrfBo obj in objoList)
            {
                ddl_ReqName.SelectedValue = obj.REQTR_NAME.ToString();
                Load_requstr_detailsonChange(ddl_ReqName.SelectedValue.ToString().Trim());
                ddl_rectPosDesig.SelectedValue = obj.DES_RECUTD.ToString();
                //Load_Rects_to(obj.DES_RECUTD.ToString());
                Load_Rects_to(ddl_rectPosDesig.SelectedValue.ToString().Trim());
                rbtnReplceExt.SelectedValue = obj.REP_EXT_EMP.ToString();
                if (rbtnReplceExt.SelectedValue == "True")
                {
                    PanelExtEmp.Visible = true;
                    DDL_ExtEmpList.Visible = true;
                    RRF_DDL_ExtEmpList.Visible = true;
                }
                else if (rbtnReplceExt.SelectedValue == "False")
                {
                    PanelExtEmp.Visible = false;
                    DDL_ExtEmpList.Visible = false;
                    RRF_DDL_ExtEmpList.Visible = false;
                }

                DDL_ExtEmpList.SelectedValue = obj.REP_EXT_EMP_ID.ToString();
                rbtnBudgted.SelectedValue = obj.REQ_POS_BUDGT.ToString();
                if (rbtnBudgted.SelectedValue == "False")
                {
                    pnlBudgt.Visible = false;
                }
                else if (rbtnBudgted.SelectedValue == "True")
                {
                    pnlBudgt.Visible = true;
                }

                txtBudgFrmMonth.Text = obj.REQ_POS_BUDGT_FRM_MONTH.ToString();
                txtBudgCost.Text = obj.REQ_POS_BUDGT_COST.ToString();
                rtnpurpsOfHrng.SelectedValue = obj.PURPS_HIRNG.ToString();
                if (rtnpurpsOfHrng.SelectedValue == "IR")
                {
                    pnlPURPSIR.Visible = true;
                    pnlPurpsSF.Visible = false;
                }
                else if (rtnpurpsOfHrng.SelectedValue == "SF")
                {
                    pnlPurpsSF.Visible = true;
                    pnlPURPSIR.Visible = true;
                }
                ddl_PurpsIRLocctn.SelectedValue = obj.PURPS_HIRNG_LOC.ToString();
                ddl_PurpsSFProj.SelectedValue = obj.PURPS_HIRNG_PROJ.ToString();
                ddl_ReprtsTo.SelectedValue = obj.POS_REPT_TO_ID.ToString();
                ddl_MinQuaEdu.SelectedValue = obj.MIN_EDU_QLAFTN.ToString();
                txtMinCertifin.Text = obj.MIN_CERTIFNTN.ToString();
                txtMinTlExp.Text = obj.TOT_EXP.ToString();
                txtMinDomExp.Text = obj.TOT_DOMAIN_EXP.ToString();
                txtAraExpRequrd.Text = obj.AREA_EXPRTSE.ToString();
                txtOthSpecfiReqmt.Text = obj.OTHER_SPC_REQ.ToString();
                txtJobDesp.Text = obj.JOB_DISP.ToString();
                txtTentDate.Text = obj.TENTTIVE_DATE.ToString();
                txtNoofResource.Text = obj.NORESOURCE.ToString();
                HF_viewfile.Value = obj.DISP_FILE;
                lnkViewfile.Visible = true;
                Session["RID_EDIT"] = null;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //load_details_edit();
                btnSubmit.Visible = false;
                btnClear.Visible = false;
                btnCancl.Visible = true;
                btnUpdate.Visible = true;
            }
            catch (Exception ex) { }
        }

        protected void btnCancl_Click(object sender, EventArgs e)
        {
            try
            {
                Page_init();
                //btnSubmit.Visible = true;
                //btnClear.Visible = true;
                btnCancl.Visible = false;
                btnUpdate.Visible = false;
                clear();
                Response.Redirect(Session["RRF_TRNSURL"].ToString(),false);
            }
            catch (Exception ex) { }
        }

        //protected void lnkAppRej_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        clear();
        //        //Mp2.Show();
        //        BtnEdit.Visible = false;
        //        divreqType.Visible = false;

        //        //Button btnapp = (Button)FV_RRF_MyReq.Row.FindControl("btnApprove");
        //        //Button btnRej = (Button)FV_RRF_MyReq.Row.FindControl("btnReject");
        //        Load_Request_details(3);
        //        if ((int)ViewState["RecCount"] > 0)
        //        {
        //            using (Panel Pnl = (Panel)FV_RRF_MyReq.FindControl("divAppRej"))
        //            { Pnl.Visible = true; }
        //        }
        //        else
        //            lblErrorMsg.Text = "No Records found..!";
        //    }
        //    catch (Exception Ex) { }
        //}

        //protected void lnkBtnSowReq_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        clear();
        //        //Mp2.Show();
        //        rbtnReqTypeStatus.SelectedValue = "1";
        //        BtnEdit.Visible = true;
        //        Load_Request_details(2);
        //        if ((int)ViewState["RecCount"] > 0)
        //        {
        //            using (Panel Pnl = (Panel)FV_RRF_MyReq.FindControl("divAppRej"))
        //            {
        //                Pnl.Visible = false;
        //                lblErrorMsg.Visible = false;
        //            }
        //        }

        //        RrfDALDataContext RRF_DAL = new RrfDALDataContext();
        //        bool? status = false;
        //        RRF_DAL.usp_rrf_en_dsble_req_status(User.Identity.Name.Trim(), ref status);
        //        divreqType.Visible = (status == true) ? true : false;
        //    }
        //    catch (Exception Ex) { }
        //}

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
        //        clear();
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
        //        clear();
        //    }
        //}

        //protected int GetReqID()
        //{
        //    Label lblid = (Label)FV_RRF_MyReq.FindControl("lblRRfID");
        //    int id = (int)Session["RID_EDIT"];//Convert.ToInt32(lblid.Text);
        //    ViewState["RID"] = id;
        //    return id;
        //}



        //protected void rbtnReqTypeStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Mp2.Show();

        //    // RadioButtonList rbtnReqTypeStatus = (RadioButtonList)FV_RRF_MyReq.FindControl("rbtnReqTypeStatus");
        //    if (rbtnReqTypeStatus.SelectedValue == "1")
        //    {
        //        Load_Request_details(2);
        //    }
        //    if (rbtnReqTypeStatus.SelectedValue == "2")
        //    {
        //        Load_Request_details(1);
        //    }
        //    if ((int)ViewState["RecCount"] > 0)
        //    {
        //        using (Panel Pnl = (Panel)FV_RRF_MyReq.FindControl("divAppRej"))
        //        {
        //            Pnl.Visible = false;
        //            lblErrorMsg.Visible = false;
        //        }
        //    }
        //    else
        //        lblErrorMsg.Text = "No Records found..!";
        //}

        protected void lnkViewfile_Click(object sender, EventArgs e)
        {
            try
            {
                string strURL = HF_viewfile.Value;
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
            }
            catch (Exception ex) { }
        }

        //protected void EditbtnED()
        //{
        //    if ((int)ViewState["RecCount"] > 0)
        //    {
        //        Label lblstatus = (Label)FV_RRF_MyReq.FindControl("lblStatus");
        //        if (lblstatus.Text == "Requested" || lblstatus.Text == "Approved" || lblstatus.Text == "Updated")
        //            BtnEdit.Visible = true;
        //        else
        //            BtnEdit.Visible = false;
        //    }
        //}

        private void SendMail(int flg, int RID, string desig, string Supervisor_Email, string PERNR_Eml)
        {
            try
            {
                string strSubject = string.Empty;
                if (flg == 1)
                    strSubject = "RRF " + RID + " has been Raised by " + ddl_ReqName.SelectedItem + " | " + ddl_ReqName.SelectedValue + " and is pending for the Approval..";
                else
                    strSubject = "RRF " + RID + " has been Updated by " + ddl_ReqName.SelectedItem + " | " + ddl_ReqName.SelectedValue + " and is pending for the Approval..";

                string RecipientsString = Supervisor_Email;
                string strPernr_Mail = PERNR_Eml;

                //    //Preparing the mail body--------------------------------------------------

                //string body = "TO : " + RecipientsString + " CC: " + strPernr_Mail + "<br>";
                string body = "<b style= 'font-size: 15px';> " + strSubject + "</b><br/><br/>";
                body += "<b>Request Details :</b><br /><hr />";
                body += "<b><table style=border-collapse:collapse;>";
                //if (flg == 1)
                body += "<tr><td>R. R. F. ID : </td><td>" + RID + "</td></tr>";
                body += "<tr><td>Indentor : </td><td>" + lblIndtrName.Text + "</td></tr>";
                body += "<tr><td>Requestor : </td><td>" + ddl_ReqName.SelectedItem + " | " + ddl_ReqName.SelectedValue + "</td></tr>";
                body += "<tr><td>For Designation : </td><td>" + desig + "</td></tr></table>";
                //else
                //    body += "<tr><td>R. R. F. has been updated for " + desig + " with requet ID=" + RID + " and waiting for your action.</td></tr></table></b>";

                body += "<br/><b>This is an autogenerated e-mail, hence do not reply.</b>";
                Thread email = new Thread(delegate()
                   {
                       iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                   });
                email.IsBackground = true;
                email.Start();
            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerDAL.PR;

namespace iEmpPower.UI.PR
{
    public partial class PR_ManagerAppRej : System.Web.UI.Page
    {
        bool bSortedOrder = false;
        string EmployeeName = "";
        string EmployeeId = "";


        protected override void OnPreRender(EventArgs e)
        {

            // add base.OnPreRender(e); at the beginning of the method.

            base.OnPreRender(e);
            // codes to handle with your controls.

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ////LoadPRRequestGridView();

                    if (Session["PRID"] != null)
                    {
                        ViewPR(Session["PRID"].ToString());
                        ////goto displayInfo;
                    }

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        void ViewPR(string PRID1)
        {
            ////int rowIndex = Convert.ToInt32(e.CommandArgument);

            ////foreach (GridViewRow row in grdPRAppRej.Rows)
            ////{
            ////    row.BackColor = row.RowIndex.Equals(rowIndex) ?
            ////    System.Drawing.Color.LightGray :
            ////    System.Drawing.Color.White;
            ////}

            ////ViewPRIfo.Visible = true;

            ////int PRID = int.Parse(grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());
            int PRID = int.Parse(PRID1);

            HF_PRID.Value = PRID.ToString().Trim();////grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString().Trim();

            ////string Inbudget = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["IN_BUDGET"].ToString();
            ////string misgrpc = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["MIS_GRPC"].ToString();
            ////string misgrpa = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["MIS_GRPA"].ToString();
            ////string misgrpb = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["MIS_GRPB"].ToString();
            ////string billadd = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["BWERKS"].ToString();
            ////string shipadd = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["SWERKS"].ToString();
            ////string capitalized = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["CAPITALIZED"].ToString();
            ////string captxt = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["CAP_TEXT"].ToString();
            ////string createduser = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATEDBY"].ToString();
            ////string status = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString();

            ////if (status == "Requested")
            ////{

            ////    LbtnEdit.Visible = true;
            ////    // LbtnSave.Visible = true;

            ////}
            ////else
            ////{

            ////    LbtnEdit.Visible = false;
            ////    LbtnSave.Visible = false;

            ////}

            ////ViewState["Inbudget"] = Inbudget;
            ////ViewState["misgrpc"] = misgrpc;
            ////ViewState["misgrpa"] = misgrpa;
            ////ViewState["misgrpb"] = misgrpb;
            ////ViewState["billadd"] = billadd;
            ////ViewState["shipadd"] = shipadd;
            ////ViewState["capitalized"] = capitalized;
            ////ViewState["captxt"] = captxt;
            ////ViewState["createduser"] = createduser;
            ViewState["PRID"] = PRID;


            prbl PrBlObj = new prbl();
            List<prbo> requisitionboList = new List<prbo>();
            requisitionboList = PrBlObj.Load_PRItemDetails(PRID);
            FV_PRInfoDisplay.DataSource = requisitionboList;
            FV_PRInfoDisplay.DataBind();

            ////var item = requisitionboList.Select(e=>e.STATUS).ToString();//requisitionboList.Select(e => e.STATUS).Distinct();
            string status = requisitionboList[0].STATUS.ToString();




            ////ViewState["Inbudget"] = requisitionboList[0].IN_BUDGET.ToString(); ////Inbudget;
            ////ViewState["misgrpc"] = requisitionboList[0].MIS_GRPC.ToString(); //// misgrpc;
            ////ViewState["misgrpa"] = requisitionboList[0].MIS_GRPA.ToString(); ////misgrpa;
            ////ViewState["misgrpb"] = requisitionboList[0].MIS_GRPB.ToString(); ////misgrpb;
            ////ViewState["billadd"] = requisitionboList[0].BWERKS.ToString(); ////billadd;
            ////ViewState["shipadd"] = requisitionboList[0].SWERKS.ToString(); ////shipadd;
            ////ViewState["capitalized"] = requisitionboList[0].CAPITALIZED.ToString(); ////capitalized;
            ////ViewState["captxt"] = requisitionboList[0].CAP_TEXT.ToString(); ////captxt;
            ////ViewState["createduser"] = requisitionboList[0].CREATEDBY.ToString(); ////createduser;

            ViewState["Inbudget"] = requisitionboList[0].IN_BUDGET.ToString(); ////Inbudget;
            ViewState["misgrpc"] = requisitionboList[0].MISCIDID.ToString(); //// misgrpc;
            ViewState["misgrpa"] = requisitionboList[0].MIS_GRPA.ToString(); ////misgrpa;
            ViewState["misgrpb"] = requisitionboList[0].MIS_GRPB.ToString(); ////misgrpb;
            ViewState["billadd"] = requisitionboList[0].BWERKSID.ToString(); ////billadd;
            ViewState["shipadd"] = requisitionboList[0].SWERKSID.ToString(); ////shipadd;
            ViewState["capitalized"] = requisitionboList[0].CAPITALIZED.ToString(); ////capitalized;
            ViewState["captxt"] = requisitionboList[0].CAP_TEXT.ToString(); ////captxt;
            ViewState["createduser"] = requisitionboList[0].CREATEDBY.ToString(); ////createduser;
     
            if (status == "Requested")
            {

                LbtnEdit.Visible = true;
                // LbtnSave.Visible = true;
            }
            else
            {

                LbtnEdit.Visible = false;
                LbtnSave.Visible = false;

            }



            grdAppHistory.DataSource = requisitionboList;
            grdAppHistory.DataBind();



            ViewState["APPROVEDBY1"] = requisitionboList[0].APPROVEDBY1 == null ? "" : requisitionboList[0].APPROVEDBY1.ToString();
            ViewState["APPROVEDBY2"] = requisitionboList[0].APPROVEDBY2 == null ? "" : requisitionboList[0].APPROVEDBY2.ToString();
            ViewState["APPROVEDBY3"] = requisitionboList[0].APPROVEDBY3 == null ? "" : requisitionboList[0].APPROVEDBY3.ToString();
            ViewState["APPROVEDBY4"] = requisitionboList[0].APPROVEDBY4 == null ? "" : requisitionboList[0].APPROVEDBY4.ToString();
            ViewState["APPROVEDBY5"] = requisitionboList[0].APPROVEDBY5 == null ? "" : requisitionboList[0].APPROVEDBY5.ToString();
            ViewState["APPROVEDBY6"] = requisitionboList[0].APPROVEDBY6 == null ? "" : requisitionboList[0].APPROVEDBY6.ToString();


            requisitionboList = PrBlObj.Load_PRItem(PRID);
            GV_PrItems.DataSource = requisitionboList;
            GV_PrItems.DataBind();

            if (ViewState["APPROVEDBY1"] != null)
            {

                if (User.Identity.Name == ViewState["APPROVEDBY1"].ToString())
                {
                    GV_PrItems.Columns[9].Visible = true;
                    //    createdby = requisitionboList[0].RPERNR.ToString();
                    //    lblGLAcc.Visible = true;
                    //    ddlGLAcc.Visible = true;
                    //    LoadGLAccount(createdby);

                }
                else
                {
                    GV_PrItems.Columns[9].Visible = false;
                }
            }
        }


        //private void LoadGLAccount(string createdby)
        //{
        //    mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.LoadGlAccount(createdby);
        //    ddlGLAcc.DataSource = objLst;
        //    ddlGLAcc.DataTextField = "TXT50";
        //    ddlGLAcc.DataValueField = "SAKNR";
        //    ddlGLAcc.DataBind();
        //    ddlGLAcc.Items.Insert(0, new ListItem("- SELECT -", "0"));
        //    ddlGLAcc.SelectedValue = "0";
        //}


        private void LoadPRRequestGridView()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                prbl travelrequestblObj = new prbl();
                List<prbo> requisitionboList = new List<prbo>();
                //==============================
                //get sub employees
                ////msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                ////msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                ////objPIDashBoardBo.PERNR = User.Identity.Name;
                ////msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_Manager(objPIDashBoardBo);

                ////if (objPIDashBoardLst.Count > 0)
                ////{
                ////    foreach (var vrow in objPIDashBoardLst)
                ////    {
                List<prbo> requisitionboList1 = new List<prbo>();
                ////EmployeeId = vrow.PERNR;
                ////EmployeeName = vrow.ENAME;

                ////requisitionboList1 = travelrequestblObj.Load_PRDetails(EmployeeId, EmployeeName);
                string ApproverId = User.Identity.Name;
                requisitionboList1 = travelrequestblObj.Load_PRDetails(ApproverId, "");
                requisitionboList.AddRange(requisitionboList1);
                Session.Add("PRGrdInfo", requisitionboList);
                ////    }
                ////}


                if (requisitionboList == null || requisitionboList.Count == 0)
                {
                    grdPRAppRej.Visible = false;
                    grdPRAppRej.DataSource = null;
                    MsgCls("No Records Found", lblMessageBoard, Color.Red);

                    return;
                }
                else
                {
                    grdPRAppRej.Visible = true;
                    grdPRAppRej.DataSource = requisitionboList;
                    grdPRAppRej.SelectedIndex = -1;
                    MsgCls("", lblMessageBoard, Color.Transparent);

                }
                grdPRAppRej.DataBind();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        //protected void btnApprove_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        //  int Chkd = 0;
        //        int NoGL = 0;
        //        bool? Status = true;
        //        if (grdPRAppRej.Rows.Count > 0)
        //        {
        //            foreach (GridViewRow item in grdPRAppRej.Rows)
        //            {
        //                //using (CheckBox chk = (CheckBox)item.FindControl("chkSelected"))
        //                //    if (chk.Checked)
        //                //    {
        //                if (HF_PRID != null)
        //                {
        //                    string ReleasedStatus = string.Empty;
        //                    //Chkd = Chkd + 1;
        //                    //string PR_id = item.Cells[0].Text;
        //                    string PR_id = HF_PRID.Value.ToString().Trim();
        //                    prbo objBo = new prbo();
        //                    prbl objBl = new prbl();
        //                    //  objBo.BANFN_EXT = int.Parse(item.Cells[0].Text);
        //                    objBo.BANFN_EXT = int.Parse(HF_PRID.Value.ToString().Trim());


        //                    objBo.APPROVEDBY1 = User.Identity.Name;
        //                    objBo.COMMENTS1 = TxtRemarks.Text == "" ? "APPROVED" : TxtRemarks.Text.Trim();////TxtRemarks.Text.Trim();
        //                    objBo.STATUS = "Approved";

        //                    prcollectionbo objLst = objBl.Get_Requested_PRStatus(objBo);
        //                    foreach (prbo probjBo in objLst)
        //                    {
        //                        ReleasedStatus = probjBo.RSTATUS;

        //                    }

        //                    if (ReleasedStatus == "RELEASED1" || ReleasedStatus == "RELEASED2" || ReleasedStatus == "RELEASED3" ||
        //                        ReleasedStatus == "RELEASED4" || ReleasedStatus == "RELEASED5" || ReleasedStatus == "RELEASED6" ||
        //                        ReleasedStatus == "Approved1" || ReleasedStatus == "Approved2" || ReleasedStatus == "Approved3" ||
        //                        ReleasedStatus == "Approved4" || ReleasedStatus == "Approved5" || ReleasedStatus == "Approved6" ||
        //                        ReleasedStatus == "Requested")
        //                    {
        //                        if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name && (ReleasedStatus == "Requested" || ReleasedStatus == "RELEASED1"))
        //                            objBo.STATUS = "Approved1";

        //                        if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved1" || ReleasedStatus == "RELEASED2"))
        //                            objBo.STATUS = "Approved2";
        //                        if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved2" || ReleasedStatus == "RELEASED3"))
        //                            objBo.STATUS = "Approved3";
        //                        if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved3" || ReleasedStatus == "RELEASED4"))
        //                            objBo.STATUS = "Approved4";
        //                        if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved4" || ReleasedStatus == "RELEASED5"))
        //                            objBo.STATUS = "Approved5";
        //                        if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved5" || ReleasedStatus == "RELEASED6"))
        //                            objBo.STATUS = "Approved6";
        //                        //--------------------------------

        //                        if (GV_PrItems.Rows.Count > 0)
        //                        {
        //                            foreach (GridViewRow items in GV_PrItems.Rows)
        //                            {
        //                                if (ReleasedStatus == "Requested")
        //                                {
        //                                    using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
        //                                    {
        //                                        if (ddl != null)
        //                                        {
        //                                            if (string.IsNullOrEmpty(ddl.SelectedValue))
        //                                            {
        //                                                LoadPRRequestGridView();
        //                                                // ddlGLAcc.SelectedValue = "0";                                                      
        //                                                throw new NotImplementedException("Please select all the GL account !");
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        //--------------------------------



        //                        if (ReleasedStatus == "Requested")
        //                        {
        //                            if (GV_PrItems.Rows.Count > 0)
        //                            {
        //                                foreach (GridViewRow items in GV_PrItems.Rows)
        //                                {

        //                                    objBo.BNFPO = GV_PrItems.DataKeys[items.RowIndex]["BNFPO"].ToString();

        //                                    using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
        //                                    {
        //                                        if (ddl != null)
        //                                        {
        //                                            objBo.SAKNR = ddl.SelectedValue;
        //                                            if (objBo.SAKNR != null)
        //                                            {


        //                                                if (objBo.SAKNR != "")
        //                                                {
        //                                                    objBl.Update_PR_Status(objBo, ref Status);
        //                                                    //if (Status.Equals(false))
        //                                                    //{

        //                                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !')", true);
        //                                                    //    MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
        //                                                    //    SendMailMethodtToEmp(objBo);
        //                                                    //    SendMailMethod(objBo);
        //                                                    //    LoadPRRequestGridView();
        //                                                    //    // ddlGLAcc.SelectedValue = "0";

        //                                                    //    TxtRemarks.Text = string.Empty;
        //                                                    //    ViewPRIfo.Visible = false;
        //                                                    //    FV_PRInfoDisplay.DataSource = null;
        //                                                    //    FV_PRInfoDisplay.DataBind();
        //                                                    //    GV_PrItems.DataSource = null;
        //                                                    //    GV_PrItems.DataBind();
        //                                                    //    grdAppHistory.DataSource = null;
        //                                                    //    grdAppHistory.DataBind();
        //                                                    //}
        //                                                }
        //                                                else
        //                                                {
        //                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select GL Account !')", true);
        //                                                    MsgCls("Please Select GL Account !", lblMessageBoard, Color.Red);
        //                                                    return;
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select GL Account !')", true);
        //                                                MsgCls("Please Select GL Account !", lblMessageBoard, Color.Red);
        //                                                return;
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select GL Account !')", true);
        //                                            MsgCls("Please Select GL Account !", lblMessageBoard, Color.Red);
        //                                            return;
        //                                        }
        //                                    }
        //                                }

        //                            }

        //                            if (Status.Equals(false))
        //                            {

        //                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !')", true);
        //                                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
        //                                SendMailMethodtToEmp(objBo);
        //                                SendMailMethod(objBo);
        //                                LoadPRRequestGridView();
        //                                // ddlGLAcc.SelectedValue = "0";

        //                                TxtRemarks.Text = string.Empty;
        //                                ViewPRIfo.Visible = false;
        //                                FV_PRInfoDisplay.DataSource = null;
        //                                FV_PRInfoDisplay.DataBind();
        //                                GV_PrItems.DataSource = null;
        //                                GV_PrItems.DataBind();
        //                                grdAppHistory.DataSource = null;
        //                                grdAppHistory.DataBind();
        //                            }


        //                        }
        //                        else
        //                        {
        //                            objBl.Update_PR_Status(objBo, ref Status);
        //                            if (Status.Equals(false))
        //                            {

        //                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !')", true);
        //                                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
        //                                SendMailMethodtToEmp(objBo);
        //                                SendMailMethod(objBo);
        //                                LoadPRRequestGridView();
        //                                // ddlGLAcc.SelectedValue = "0";

        //                                TxtRemarks.Text = string.Empty;
        //                                ViewPRIfo.Visible = false;
        //                                FV_PRInfoDisplay.DataSource = null;
        //                                FV_PRInfoDisplay.DataBind();
        //                                GV_PrItems.DataSource = null;
        //                                GV_PrItems.DataBind();
        //                                grdAppHistory.DataSource = null;
        //                                grdAppHistory.DataBind();
        //                            }
        //                        }

        //                        //if (Status.Equals(false))
        //                        //{

        //                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !')", true);
        //                        //    MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
        //                        //    SendMailMethodtToEmp(objBo);
        //                        //    SendMailMethod(objBo);
        //                        //    LoadPRRequestGridView();
        //                        //    // ddlGLAcc.SelectedValue = "0";

        //                        //    TxtRemarks.Text = string.Empty;
        //                        //    ViewPRIfo.Visible = false;
        //                        //    FV_PRInfoDisplay.DataSource = null;
        //                        //    FV_PRInfoDisplay.DataBind();
        //                        //    GV_PrItems.DataSource = null;
        //                        //    GV_PrItems.DataBind();
        //                        //    grdAppHistory.DataSource = null;
        //                        //    grdAppHistory.DataBind();
        //                        //}

        //                        //if (ddl != null)
        //                        //{
        //                        //    objBo.SAKNR = ddl.SelectedValue;
        //                        //    objBl.Update_PR_Status(objBo);
        //                        //}
        //                        //else
        //                        //{
        //                        //    objBo.SAKNR = string.Empty;
        //                        //    objBl.Update_PR_Status(objBo);
        //                        //}

        //                    }


        //                    else
        //                    {
        //                        //MsgCls("Please Release the PR Request to Approve !", lblMessageBoard, Color.Red);
        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Release the PR Request to Approve !')", true);
        //                        return;
        //                    }
        //                }



        //                LoadPRRequestGridView();
        //                // ddlGLAcc.SelectedValue = "0";

        //                TxtRemarks.Text = string.Empty;
        //                ViewPRIfo.Visible = false;
        //                FV_PRInfoDisplay.DataSource = null;
        //                FV_PRInfoDisplay.DataBind();
        //                GV_PrItems.DataSource = null;
        //                GV_PrItems.DataBind();
        //                grdAppHistory.DataSource = null;
        //                grdAppHistory.DataBind();
        //                HF_PRID = null;
        //            }
        //        }
        //        else
        //        {
        //            MsgCls("There are no PR Request to Approve !", lblMessageBoard, Color.Red);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no PR Request to Approve !')", true);
        //            return;
        //        }

        //    }
        //    catch (Exception Ex)
        //    {

        //        switch (Ex.Message)
        //        {


        //            case "-05":
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Approvals are missing!')", true);
        //                MsgCls("Approvals are missing", lblMessageBoard, Color.Red);
        //                //PageLoadEvents();
        //                break;

        //            case "-06":
        //                //   ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
        //                MsgCls("Currency conversion rates are missing", lblMessageBoard, Color.Red);
        //                //PageLoadEvents();
        //                break;

        //            default:
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
        //                break;
        //        }
        //        //MsgCls(Ex.Message, LblMsg, Color.Red);
        //    }
        //}

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {

                //  int Chkd = 0;
                int NoGL = 0;
                bool? Status = true;
                //if (grdPRAppRej.Rows.Count > 0)
                //{
                    //foreach (GridViewRow item in grdPRAppRej.Rows)
                    //{
                        if (HF_PRID != null)
                        {
                            string ReleasedStatus = string.Empty;
                            string PR_id = HF_PRID.Value.ToString().Trim();
                            prbo objBo = new prbo();
                            prbl objBl = new prbl();
                            objBo.BANFN_EXT = int.Parse(HF_PRID.Value.ToString().Trim());


                            objBo.APPROVEDBY1 = User.Identity.Name;
                            objBo.COMMENTS1 = TxtRemarks.Text == "" ? "APPROVED" : TxtRemarks.Text.Trim();////TxtRemarks.Text.Trim();
                            objBo.STATUS = "Approved";

                            prcollectionbo objLst = objBl.Get_Requested_PRStatus(objBo);
                            foreach (prbo probjBo in objLst)
                            {
                                ReleasedStatus = probjBo.RSTATUS;

                            }

                            if (ReleasedStatus == "RELEASED1" || ReleasedStatus == "RELEASED2" || ReleasedStatus == "RELEASED3" ||
                                ReleasedStatus == "RELEASED4" || ReleasedStatus == "RELEASED5" || ReleasedStatus == "RELEASED6" ||
                                ReleasedStatus == "Approved1" || ReleasedStatus == "Approved2" || ReleasedStatus == "Approved3" ||
                                ReleasedStatus == "Approved4" || ReleasedStatus == "Approved5" || ReleasedStatus == "Approved6" ||
                                ReleasedStatus == "Requested")
                            {
                                if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name && (ReleasedStatus == "Requested" || ReleasedStatus == "RELEASED1"))
                                    objBo.STATUS = "Approved1";

                                if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved1" || ReleasedStatus == "RELEASED2"))
                                    objBo.STATUS = "Approved2";
                                if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved2" || ReleasedStatus == "RELEASED3"))
                                    objBo.STATUS = "Approved3";
                                if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved3" || ReleasedStatus == "RELEASED4"))
                                    objBo.STATUS = "Approved4";
                                if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved4" || ReleasedStatus == "RELEASED5"))
                                    objBo.STATUS = "Approved5";
                                if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved5" || ReleasedStatus == "RELEASED6"))
                                    objBo.STATUS = "Approved6";
                        //--------------------------------

                        if (GV_PrItems.Rows.Count > 0)
                        {
                            foreach (GridViewRow items in GV_PrItems.Rows)
                            {
                                if (ReleasedStatus == "Requested")
                                {
                                    using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
                                    {
                                        if (ddl != null)
                                        {
                                            if (string.IsNullOrEmpty(ddl.SelectedValue))
                                            {
                                                LoadPRRequestGridView();
                                                throw new NotImplementedException("Please select all the GL account !");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //--------------------------------



                        if (ReleasedStatus == "Requested")
                                {
                                    if (GV_PrItems.Rows.Count > 0)
                                    {
                                        foreach (GridViewRow items in GV_PrItems.Rows)
                                        {

                                            objBo.BNFPO = GV_PrItems.DataKeys[items.RowIndex]["BNFPO"].ToString();

                                            using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
                                            {
                                                if (ddl != null)
                                                {
                                                    objBo.SAKNR = ddl.SelectedValue;
                                                    if (objBo.SAKNR != null)
                                                    {


                                                        if (objBo.SAKNR != "")
                                                        {
                                                            objBl.Update_PR_Status(objBo, ref Status);

                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select GL Account !')", true);
                                                            MsgCls("Please Select GL Account !", lblMessageBoard, Color.Red);
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select GL Account !')", true);
                                                        MsgCls("Please Select GL Account !", lblMessageBoard, Color.Red);
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select GL Account !')", true);
                                                    MsgCls("Please Select GL Account !", lblMessageBoard, Color.Red);
                                                    return;
                                                }
                                            }
                                        }

                                    }

                                    if (Status.Equals(false))
                                    {

                                        ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !')", true);
                                        MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                                        SendMailMethodtToEmp(objBo);
                                        SendMailMethod(objBo);
                                        LoadPRRequestGridView();

                                        TxtRemarks.Text = string.Empty;
                                        ViewPRIfo.Visible = false;
                                        FV_PRInfoDisplay.DataSource = null;
                                        FV_PRInfoDisplay.DataBind();
                                        GV_PrItems.DataSource = null;
                                        GV_PrItems.DataBind();
                                        grdAppHistory.DataSource = null;
                                        grdAppHistory.DataBind();

                                        ////Response.Redirect("Purchase_Requisitions.aspx?PC=P");
                             
    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !');window.location ='Purchase_Requisitions.aspx?PC=P';", true);

                                    }


                                }
                                else
                                {
                                    objBl.Update_PR_Status(objBo, ref Status);
                                    if (Status.Equals(false))
                                    {

                                        ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !')", true);
                                        MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                                        SendMailMethodtToEmp(objBo);
                                        SendMailMethod(objBo);
                                        LoadPRRequestGridView();

                                        TxtRemarks.Text = string.Empty;
                                        ViewPRIfo.Visible = false;
                                        FV_PRInfoDisplay.DataSource = null;
                                        FV_PRInfoDisplay.DataBind();
                                        GV_PrItems.DataSource = null;
                                        GV_PrItems.DataBind();
                                        grdAppHistory.DataSource = null;
                                        grdAppHistory.DataBind();

                                        ////Response.Redirect("Purchase_Requisitions.aspx?PC=P");
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Approved successfully !');window.location ='Purchase_Requisitions.aspx?PC=P';", true);
                                    }
                                }

                               
                            }


                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Release the PR Request to Approve !')", true);
                                return;
                            }
                        }



                        LoadPRRequestGridView();


                        TxtRemarks.Text = string.Empty;
                        ViewPRIfo.Visible = false;
                        FV_PRInfoDisplay.DataSource = null;
                        FV_PRInfoDisplay.DataBind();
                        GV_PrItems.DataSource = null;
                        GV_PrItems.DataBind();
                        grdAppHistory.DataSource = null;
                        grdAppHistory.DataBind();
                        HF_PRID = null;
                    //}
                //}
                //else
                //{
                //    MsgCls("There are no PR Request to Approve !", lblMessageBoard, Color.Red);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no PR Request to Approve !')", true);
                //    return;
                //}

            }
            catch (Exception Ex)
            {

                switch (Ex.Message)
                {


                    case "-05":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Approvals are missing!')", true);
                        MsgCls("Approvals are missing", lblMessageBoard, Color.Red);
                        //PageLoadEvents();
                        break;

                    case "-06":
                        //   ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        MsgCls("Currency conversion rates are missing", lblMessageBoard, Color.Red);
                        //PageLoadEvents();
                        break;

                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        break;
                }
            }
        }


        private void SendMailMethodtToEmp(prbo objBo)
        {
            try
            {
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //StringWriter sw2 = new StringWriter();
                //HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                //  FV_PRInfoDisplay.RenderControl(hw);

                FV_PRInfoDisplay.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = false;
                GV_PrItems.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = true;


                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVED_BY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string IEMP_Name = "";
                string IEMP_Email = "";
                string REMP_Name = "";
                string REMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                string RequesterID = "";
                string purcharEmailID = "Purchase@subex.com";

                prdbmlDataContext objcontext = new prdbmlDataContext();

                objcontext.sp_Get_MailList_PRApp(objBo.BANFN_EXT, objBo.APPROVEDBY1, objBo.STATUS, ref CREATED_BY, ref APPROVED_BY1, ref Approver_Name,
                    ref Approver_Email, ref IEMP_Name, ref IEMP_Email, ref RequesterID, ref REMP_Name, ref REMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name);

                if (APPROVED_BY1 != "")
                {

                    if (IEMP_Email == REMP_Email)
                    {
                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + " and sent to " + Approver_Name + " for approval";

                        RecipientsString = REMP_Email;
                        strPernr_Mail = PRSNTAPPROVEDBY_Email;

                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + " and sent to " + Approver_Name + "  |  " + APPROVED_BY1 + "  for approval.<br/><br/></b>";
                        body += "<b>PR Requisition Details :<hr /></b>" + sw1.ToString() + "<br/>";

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

                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }
                    else
                    {
                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + " and sent to " + Approver_Name + " for approval";

                        RecipientsString = REMP_Email;
                        strPernr_Mail = PRSNTAPPROVEDBY_Email + "," + IEMP_Email;

                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + " and sent to " + Approver_Name + "  |  " + APPROVED_BY1 + "  for approval.<br/><br/></b>";
                        body += "<b>PR Requisition Details :<hr /></b>" + sw1.ToString() + "<br/>";

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


                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }

                }

                else
                {

                    if (IEMP_Email == REMP_Email)
                    {
                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name;

                        RecipientsString = REMP_Email;
                        strPernr_Mail = PRSNTAPPROVEDBY_Email + "," + purcharEmailID;

                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + " <br/><br/></b>";
                        body += "<b>PR Requisition Details :<hr /></b>" + sw1.ToString() + "<br/>";

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

                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }
                    else
                    {
                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name;

                        RecipientsString = REMP_Email;
                        strPernr_Mail = PRSNTAPPROVEDBY_Email + "," + IEMP_Email + "," + purcharEmailID;

                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been approved by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + "<br/><br/></b>";
                        body += "<b>PR Requisition Details :<hr /></b>" + sw1.ToString() + "<br/>";

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


                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }
                }

            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "PR Approved Successfully. Error in sending mail.";
                return;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void SendMailMethod(prbo objBo)
        {
            try
            {
                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //StringWriter sw2 = new StringWriter();
                //HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                //  FV_PRInfoDisplay.RenderControl(hw);


                FV_PRInfoDisplay.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = false;
                GV_PrItems.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = true;
                //FV_PRInfoDisplay.RenderControl(hw1);
                //GV_PrItems.RenderControl(hw1);
                // StringReader sr = new StringReader(sw1.ToString() + sw2.ToString());

                // string strSubject = "PR RRequisition by " + Session["EmployeeName"];
                string strSubject = string.Empty;
                //    "PR RRequisition by " + user;

                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVED_BY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string IEMP_Name = "";
                string IEMP_Email = "";
                string REMP_Name = "";
                string REMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                string RequesterID = "";
                prdbmlDataContext objcontext = new prdbmlDataContext();

                objcontext.sp_Get_MailList_PRApp(objBo.BANFN_EXT, objBo.APPROVEDBY1, objBo.STATUS, ref CREATED_BY, ref APPROVED_BY1, ref Approver_Name,
           ref Approver_Email, ref IEMP_Name, ref IEMP_Email, ref RequesterID, ref REMP_Name, ref REMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name);


                if (IEMP_Email == REMP_Email)
                {
                    if (Approver_Email != null)
                    {

                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been Raised by " + REMP_Name;

                        RecipientsString = Approver_Email;
                        strPernr_Mail = REMP_Email;

                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been Raised by " + REMP_Name + "  |  " + RequesterID + "<br/><br/>";
                        body += "<b>PR Requisition Details :</b><hr />" + sw1.ToString() + "<br/>";


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

                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }
                }
                else
                {
                    if (Approver_Email != null)
                    {

                        strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been Raised by Indentor " + IEMP_Name + " for the Requestor " + REMP_Name;

                        RecipientsString = Approver_Email;
                        strPernr_Mail = REMP_Email + "," + IEMP_Email;


                        //    //Preparing the mail body--------------------------------------------------
                        string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been Raised by Indentor " + IEMP_Name + "  |  " + CREATED_BY + " for the Requestor " + REMP_Name + "  |  " + RequesterID + "<br/><br/>";
                        body += "<b>PR Requisition Details :</b><hr />" + sw1.ToString() + "<br/>";


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


                        //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                        //lblMessageBoard.Text = "Mail sent successfully.";
                    }
                }
            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "PR Approved Successfully. Error in sending mail.";
                return;
            }
        }

        //protected void btnReject_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //int Chkd = 0;
        //        bool? Status = true;
        //        if (grdPRAppRej.Rows.Count > 0)
        //        {
        //            foreach (GridViewRow item in grdPRAppRej.Rows)
        //            {
        //                if (HF_PRID != null)
        //                {
        //                    //using (CheckBox chk = (CheckBox)item.FindControl("chkSelected"))
        //                    //    if (chk.Checked)
        //                    //    {
        //                    string ReleasedStatus = string.Empty;
        //                    //  Chkd = Chkd + 1;
        //                    // string PR_id = item.Cells[0].Text;
        //                    string PR_id = HF_PRID.Value.ToString().Trim();
        //                    prbo objBo = new prbo();
        //                    prbl objBl = new prbl();
        //                    //objBo.BANFN_EXT = int.Parse(item.Cells[0].Text);
        //                    objBo.BANFN_EXT = int.Parse(HF_PRID.Value.ToString().Trim());
        //                    objBo.COMMENTS1 = TxtRemarks.Text.Trim();
        //                    objBo.APPROVEDBY1 = User.Identity.Name;
        //                    objBo.STATUS = "Rejected";
        //                    objBo.SAKNR = string.Empty;
        //                    prcollectionbo objLst = objBl.Get_Requested_PRStatus(objBo);
        //                    foreach (prbo probjBo in objLst)
        //                    {
        //                        ReleasedStatus = probjBo.RSTATUS;

        //                    }

        //                    if (ReleasedStatus == "RELEASED1" || ReleasedStatus == "RELEASED2" || ReleasedStatus == "RELEASED3" ||
        //                       ReleasedStatus == "RELEASED4" || ReleasedStatus == "RELEASED5" || ReleasedStatus == "RELEASED6" ||
        //                       ReleasedStatus == "Approved1" || ReleasedStatus == "Approved2" || ReleasedStatus == "Approved3" ||
        //                       ReleasedStatus == "Approved4" || ReleasedStatus == "Approved5" || ReleasedStatus == "Approved6" ||
        //                       ReleasedStatus == "Requested")
        //                    {
        //                        if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name && (ReleasedStatus == "Requested" || ReleasedStatus == "RELEASED1"))
        //                            objBo.STATUS = "Rejected1";
        //                        if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved1" || ReleasedStatus == "RELEASED2"))
        //                            objBo.STATUS = "Rejected2";
        //                        if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved2" || ReleasedStatus == "RELEASED3"))
        //                            objBo.STATUS = "Rejected3";
        //                        if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved3" || ReleasedStatus == "RELEASED4"))
        //                            objBo.STATUS = "Rejected4";
        //                        if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved4" || ReleasedStatus == "RELEASED5"))
        //                            objBo.STATUS = "Rejected5";
        //                        if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved5" || ReleasedStatus == "RELEASED6"))
        //                            objBo.STATUS = "Rejected6";


        //                        if (GV_PrItems.Rows.Count > 0)
        //                        {
        //                            foreach (GridViewRow items in GV_PrItems.Rows)
        //                            {
        //                                objBo.BNFPO = GV_PrItems.DataKeys[items.RowIndex]["BNFPO"].ToString();
        //                                using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
        //                                {
        //                                    objBo.SAKNR = ddl.SelectedValue;
        //                                    objBl.Update_PR_Status(objBo, ref Status);
        //                                    if (Status.Equals(false))
        //                                    {
        //                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Rejected successfully !')", true);
        //                                        RejectSendMailMethodtToEmp(objBo);
        //                                        MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
        //                                        LoadPRRequestGridView();
        //                                        TxtRemarks.Text = string.Empty;
        //                                        FV_PRInfoDisplay.DataSource = null;
        //                                        FV_PRInfoDisplay.DataBind();
        //                                        GV_PrItems.DataSource = null;
        //                                        GV_PrItems.DataBind();
        //                                        grdAppHistory.DataSource = null;
        //                                        grdAppHistory.DataBind();
        //                                    }
        //                                    //if (ddl != null)
        //                                    //{
        //                                    //    objBo.SAKNR = ddl.SelectedValue;
        //                                    //    objBl.Update_PR_Status(objBo);
        //                                    //}
        //                                    //else
        //                                    //{
        //                                    //    objBo.SAKNR = string.Empty;
        //                                    //    objBl.Update_PR_Status(objBo);
        //                                    //}
        //                                }

        //                            }
        //                        }


        //                        // objBl.Update_PR_Status(objBo);
        //                        //commented monica for status of approve/rej
        //                        ////////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Rejected successfully !')", true);
        //                        ////////RejectSendMailMethodtToEmp(objBo);
        //                        ////////MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
        //                        ////////LoadPRRequestGridView();
        //                        ////////TxtRemarks.Text = string.Empty;
        //                        ////////FV_PRInfoDisplay.DataSource = null;
        //                        ////////FV_PRInfoDisplay.DataBind();
        //                        ////////GV_PrItems.DataSource = null;
        //                        ////////GV_PrItems.DataBind();
        //                        ////////grdAppHistory.DataSource = null;
        //                        ////////grdAppHistory.DataBind();
        //                    }
        //                    else
        //                    {
        //                        MsgCls("Please Release the PR Request to Reject !", lblMessageBoard, Color.Red);
        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Release the PR Request to Reject !')", true);
        //                        return;
        //                    }
        //                }
        //                LoadPRRequestGridView();
        //                TxtRemarks.Text = string.Empty;
        //                ViewPRIfo.Visible = false;
        //                FV_PRInfoDisplay.DataSource = null;
        //                FV_PRInfoDisplay.DataBind();
        //                GV_PrItems.DataSource = null;
        //                GV_PrItems.DataBind();
        //                grdAppHistory.DataSource = null;
        //                grdAppHistory.DataBind();
        //                HF_PRID = null;
        //                //  }
        //            }

        //            //if (Chkd == 0)
        //            //{
        //            //    MsgCls("Please select one or more PR request to Reject !", lblMessageBoard, Color.Red);
        //            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select one or more PR request to reject !')", true);
        //            //    return;
        //            //}
        //        }
        //        else
        //        {
        //            MsgCls("There are no PR Request to Reject !", lblMessageBoard, Color.Red);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no PR Request to Reject !')", true);
        //            return;
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        //}

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {;
                bool? Status = true;
                //if (grdPRAppRej.Rows.Count > 0)
                //{
                //    foreach (GridViewRow item in grdPRAppRej.Rows)
                //    {
                        if (HF_PRID != null)
                        {
                            string ReleasedStatus = string.Empty;
                            string PR_id = HF_PRID.Value.ToString().Trim();
                            prbo objBo = new prbo();
                            prbl objBl = new prbl();
                            objBo.BANFN_EXT = int.Parse(HF_PRID.Value.ToString().Trim());
                            objBo.COMMENTS1 = TxtRemarks.Text.Trim();
                            objBo.APPROVEDBY1 = User.Identity.Name;
                            objBo.STATUS = "Rejected";
                            objBo.SAKNR = string.Empty;
                            prcollectionbo objLst = objBl.Get_Requested_PRStatus(objBo);
                            foreach (prbo probjBo in objLst)
                            {
                                ReleasedStatus = probjBo.RSTATUS;

                            }

                            if (ReleasedStatus == "RELEASED1" || ReleasedStatus == "RELEASED2" || ReleasedStatus == "RELEASED3" ||
                               ReleasedStatus == "RELEASED4" || ReleasedStatus == "RELEASED5" || ReleasedStatus == "RELEASED6" ||
                               ReleasedStatus == "Approved1" || ReleasedStatus == "Approved2" || ReleasedStatus == "Approved3" ||
                               ReleasedStatus == "Approved4" || ReleasedStatus == "Approved5" || ReleasedStatus == "Approved6" ||
                               ReleasedStatus == "Requested")
                            {
                                if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name && (ReleasedStatus == "Requested" || ReleasedStatus == "RELEASED1"))
                                    objBo.STATUS = "Rejected1";
                                if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved1" || ReleasedStatus == "RELEASED2"))
                                    objBo.STATUS = "Rejected2";
                                if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved2" || ReleasedStatus == "RELEASED3"))
                                    objBo.STATUS = "Rejected3";
                                if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved3" || ReleasedStatus == "RELEASED4"))
                                    objBo.STATUS = "Rejected4";
                                if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved4" || ReleasedStatus == "RELEASED5"))
                                    objBo.STATUS = "Rejected5";
                                if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved5" || ReleasedStatus == "RELEASED6"))
                                    objBo.STATUS = "Rejected6";


                                if (GV_PrItems.Rows.Count > 0)
                                {
                                    foreach (GridViewRow items in GV_PrItems.Rows)
                                    {
                                        objBo.BNFPO = GV_PrItems.DataKeys[items.RowIndex]["BNFPO"].ToString();
                                        using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
                                        {
                                            objBo.SAKNR = ddl.SelectedValue;
                                            objBl.Update_PR_Status(objBo, ref Status);
                                            if (Status.Equals(false))
                                            {
                                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Rejected successfully !')", true);
                                                RejectSendMailMethodtToEmp(objBo);
                                                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                                                LoadPRRequestGridView();
                                                TxtRemarks.Text = string.Empty;
                                                FV_PRInfoDisplay.DataSource = null;
                                                FV_PRInfoDisplay.DataBind();
                                                GV_PrItems.DataSource = null;
                                                GV_PrItems.DataBind();
                                                grdAppHistory.DataSource = null;
                                                grdAppHistory.DataBind();


                                                ////Response.Redirect("Purchase_Requisitions.aspx?PC=P");
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request Rejected successfully !');window.location ='Purchase_Requisitions.aspx?PC=P';", true);
                                            }
                                        }

                                    }
                                }
                            }
                            else
                            {
                                MsgCls("Please Release the PR Request to Reject !", lblMessageBoard, Color.Red);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Release the PR Request to Reject !')", true);
                                return;
                            }
                        }
                        LoadPRRequestGridView();
                        TxtRemarks.Text = string.Empty;
                        ViewPRIfo.Visible = false;
                        FV_PRInfoDisplay.DataSource = null;
                        FV_PRInfoDisplay.DataBind();
                        GV_PrItems.DataSource = null;
                        GV_PrItems.DataBind();
                        grdAppHistory.DataSource = null;
                        grdAppHistory.DataBind();
                        HF_PRID = null;
                //    }
                //}
                //else
                //{
                //    MsgCls("There are no PR Request to Reject !", lblMessageBoard, Color.Red);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no PR Request to Reject !')", true);
                //    return;
                //}
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void RejectSendMailMethodtToEmp(prbo objBo)
        {
            try
            {

                StringWriter sw1 = new StringWriter();
                HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //StringWriter sw2 = new StringWriter();
                //HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                //  FV_PRInfoDisplay.RenderControl(hw);

                //FV_PRInfoDisplay.RenderControl(hw1);
                //GV_PrItems.RenderControl(hw1);

                FV_PRInfoDisplay.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = false;
                GV_PrItems.RenderControl(hw1);
                GV_PrItems.Columns[9].Visible = true;

                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;

                string APPROVED_BY1 = "";
                string Approver_Name = "";
                string Approver_Email = "";
                string IEMP_Name = "";
                string IEMP_Email = "";
                string REMP_Name = "";
                string REMP_Email = "";
                string CREATED_BY = "";
                string PRSNTAPPROVEDBY_Email = "";
                string PRSNTAPPROVEDBY_Name = "";
                string RequesterID = "";

                prdbmlDataContext objcontext = new prdbmlDataContext();

                objcontext.sp_Get_MailList_PRApp(objBo.BANFN_EXT, objBo.APPROVEDBY1, objBo.STATUS, ref CREATED_BY, ref APPROVED_BY1, ref Approver_Name,
                  ref Approver_Email, ref IEMP_Name, ref IEMP_Email, ref RequesterID, ref REMP_Name, ref REMP_Email, ref PRSNTAPPROVEDBY_Email, ref PRSNTAPPROVEDBY_Name);

                if (IEMP_Email == REMP_Email)
                {
                    strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been Rejected by " + PRSNTAPPROVEDBY_Name;

                    RecipientsString = REMP_Email;
                    strPernr_Mail = PRSNTAPPROVEDBY_Email;

                    //    //Preparing the mail body--------------------------------------------------

                    string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been rejected by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + "<br/><br/>";
                    body += "<b>PR Requisition Details :</b><hr />" + sw1.ToString() + "<br/>";


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

                    //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    //lblMessageBoard.Text = "Mail sent successfully.";
                }
                else
                {
                    strSubject = "PR Requisition " + objBo.BANFN_EXT + " has been Rejected by " + PRSNTAPPROVEDBY_Name;

                    RecipientsString = PRSNTAPPROVEDBY_Email;
                    strPernr_Mail = REMP_Email + "," + IEMP_Email;

                    //    //Preparing the mail body--------------------------------------------------
                    string body = "<b>PR Requisition " + objBo.BANFN_EXT + " has been rejected by " + PRSNTAPPROVEDBY_Name + "  |  " + objBo.APPROVEDBY1 + "<br/><br/>";
                    body += "<b>PR Requisition Details :<hr />" + sw1.ToString() + "</b><br/>";


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

                    //lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    //lblMessageBoard.Text = "Mail sent successfully.";
                }

            }
            catch
            {
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = "PR Rejected Successfully. Error in sending mail.";
                return;
            }
        }

        protected void grdPRAppRej_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string createdby = string.Empty;
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in grdPRAppRej.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        ViewPRIfo.Visible = true;

                        int PRID = int.Parse(grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString());
                        HF_PRID.Value = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["PRID"].ToString().Trim();


                        string Inbudget = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["IN_BUDGET"].ToString();
                        string misgrpc = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["MIS_GRPC"].ToString();
                        string misgrpa = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["MIS_GRPA"].ToString();
                        string misgrpb = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["MIS_GRPB"].ToString();
                        string billadd = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["BWERKS"].ToString();
                        string shipadd = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["SWERKS"].ToString();
                        string capitalized = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["CAPITALIZED"].ToString();
                        string captxt = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["CAP_TEXT"].ToString();
                        string createduser = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATEDBY"].ToString();
                        string status = grdPRAppRej.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString();

                        if (status == "Requested")
                        {

                            LbtnEdit.Visible = true;
                            // LbtnSave.Visible = true;

                        }
                        else
                        {

                            LbtnEdit.Visible = false;
                            LbtnSave.Visible = false;

                        }

                        ViewState["Inbudget"] = Inbudget;
                        ViewState["misgrpc"] = misgrpc;
                        ViewState["misgrpa"] = misgrpa;
                        ViewState["misgrpb"] = misgrpb;
                        ViewState["billadd"] = billadd;
                        ViewState["shipadd"] = shipadd;
                        ViewState["capitalized"] = capitalized;
                        ViewState["captxt"] = captxt;
                        ViewState["createduser"] = createduser;
                        ViewState["PRID"] = PRID;


                        prbl PrBlObj = new prbl();
                        List<prbo> requisitionboList = new List<prbo>();
                        requisitionboList = PrBlObj.Load_PRItemDetails(PRID);
                        FV_PRInfoDisplay.DataSource = requisitionboList;
                        FV_PRInfoDisplay.DataBind();


                        grdAppHistory.DataSource = requisitionboList;
                        grdAppHistory.DataBind();



                        ViewState["APPROVEDBY1"] = requisitionboList[0].APPROVEDBY1 == null ? "" : requisitionboList[0].APPROVEDBY1.ToString();
                        ViewState["APPROVEDBY2"] = requisitionboList[0].APPROVEDBY2 == null ? "" : requisitionboList[0].APPROVEDBY2.ToString();
                        ViewState["APPROVEDBY3"] = requisitionboList[0].APPROVEDBY3 == null ? "" : requisitionboList[0].APPROVEDBY3.ToString();
                        ViewState["APPROVEDBY4"] = requisitionboList[0].APPROVEDBY4 == null ? "" : requisitionboList[0].APPROVEDBY4.ToString();
                        ViewState["APPROVEDBY5"] = requisitionboList[0].APPROVEDBY5 == null ? "" : requisitionboList[0].APPROVEDBY5.ToString();
                        ViewState["APPROVEDBY6"] = requisitionboList[0].APPROVEDBY6 == null ? "" : requisitionboList[0].APPROVEDBY6.ToString();


                        requisitionboList = PrBlObj.Load_PRItem(PRID);
                        GV_PrItems.DataSource = requisitionboList;
                        GV_PrItems.DataBind();

                        if (ViewState["APPROVEDBY1"] != null)
                        {

                            if (User.Identity.Name == ViewState["APPROVEDBY1"].ToString())
                            {
                                GV_PrItems.Columns[9].Visible = true;
                                //    createdby = requisitionboList[0].RPERNR.ToString();
                                //    lblGLAcc.Visible = true;
                                //    ddlGLAcc.Visible = true;
                                //    LoadGLAccount(createdby);

                            }
                            else
                            {
                                GV_PrItems.Columns[9].Visible = false;
                            }
                        }


                        break;
                    default:
                        break;
                }


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


        //protected void DownloadFile(object sender, EventArgs e)
        //{
        //    string filePath = (sender as LinkButton).CommandArgument;
        //    Response.ContentType = ContentType;
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        //    Response.WriteFile(filePath);
        //    Response.End();
        //}



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

        protected void grdPRAppRej_Sorting(object sender, GridViewSortEventArgs e)
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

            grdPRAppRej.DataSource = PrboList;
            grdPRAppRej.DataBind();

            Session.Add("PRGrdInfo", PrboList);

        }

        protected void grdPRAppRej_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int pageindex = e.NewPageIndex;
            grdPRAppRej.PageIndex = e.NewPageIndex;

            LoadPRRequestGridView();
            searchpr();
            grdPRAppRej.SelectedIndex = -1;
        }


        //protected void btnHold_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string ReleasedStatus = string.Empty;
        //        // int Chkd = 0;
        //        bool? Status = true;
        //        if (grdPRAppRej.Rows.Count > 0)
        //        {
        //            foreach (GridViewRow item in grdPRAppRej.Rows)
        //            {
        //                //using (CheckBox chk = (CheckBox)item.FindControl("chkSelected"))
        //                //    if (chk.Checked)
        //                //    {
        //                //        Chkd = Chkd + 1;
        //                if (HF_PRID != null)
        //                {
        //                    //string PR_id = item.Cells[0].Text;
        //                    string PR_id = HF_PRID.Value.ToString().Trim();
        //                    prbo objBo = new prbo();
        //                    prbl objBl = new prbl();
        //                    //objBo.BANFN_EXT = int.Parse(item.Cells[0].Text);
        //                    objBo.BANFN_EXT = int.Parse(HF_PRID.Value.ToString().Trim());
        //                    objBo.APPROVEDBY1 = User.Identity.Name;
        //                    objBo.COMMENTS1 = TxtRemarks.Text.Trim();
        //                    objBo.STATUS = "HOLD1";
        //                    prcollectionbo objLst = objBl.Get_Requested_PRStatus(objBo);
        //                    foreach (prbo probjBo in objLst)
        //                    {
        //                        ReleasedStatus = probjBo.RSTATUS;

        //                    }


        //                    if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name && (ReleasedStatus == "Requested" || ReleasedStatus == "RELEASED1"))
        //                        objBo.STATUS = "HOLD1";
        //                    if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved1" || ReleasedStatus == "RELEASED2"))
        //                        objBo.STATUS = "HOLD2";
        //                    if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved2" || ReleasedStatus == "RELEASED3"))
        //                        objBo.STATUS = "HOLD3";
        //                    if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved3" || ReleasedStatus == "RELEASED4"))
        //                        objBo.STATUS = "HOLD4";
        //                    if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved4" || ReleasedStatus == "RELEASED5"))
        //                        objBo.STATUS = "HOLD5";
        //                    if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved5" || ReleasedStatus == "RELEASED6"))
        //                        objBo.STATUS = "HOLD6";

        //                    if (GV_PrItems.Rows.Count > 0)
        //                    {
        //                        foreach (GridViewRow items in GV_PrItems.Rows)
        //                        {
        //                            objBo.BNFPO = GV_PrItems.DataKeys[items.RowIndex]["BNFPO"].ToString();
        //                            using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
        //                            {
        //                                objBo.SAKNR = ddl.SelectedValue;
        //                                objBl.Update_PR_Status(objBo, ref Status);
        //                                if (Status.Equals(false))
        //                                {
        //                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request is on hold !')", true);
        //                                    MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
        //                                    //SendMailMethod(trainingid, startdate, enddate, pernr, status);
        //                                    LoadPRRequestGridView();

        //                                    TxtRemarks.Text = string.Empty;
        //                                    FV_PRInfoDisplay.DataSource = null;
        //                                    FV_PRInfoDisplay.DataBind();
        //                                    GV_PrItems.DataSource = null;
        //                                    GV_PrItems.DataBind();
        //                                    grdAppHistory.DataSource = null;
        //                                    grdAppHistory.DataBind();
        //                                }
        //                                //if (ddl != null)
        //                                //{
        //                                //    objBo.SAKNR = ddl.SelectedValue;
        //                                //    objBl.Update_PR_Status(objBo);
        //                                //}
        //                                //else
        //                                //{
        //                                //    objBo.SAKNR = string.Empty;
        //                                //    objBl.Update_PR_Status(objBo);
        //                                //}
        //                            }

        //                        }
        //                    }


        //                    //objBl.Update_PR_Status(objBo);
        //                    //commented monica for status of approve/rej
        //                    ////////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request is on hold !')", true);
        //                    ////////MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
        //                    //////////SendMailMethod(trainingid, startdate, enddate, pernr, status);
        //                    ////////LoadPRRequestGridView();

        //                    ////////TxtRemarks.Text = string.Empty;
        //                    ////////FV_PRInfoDisplay.DataSource = null;
        //                    ////////FV_PRInfoDisplay.DataBind();
        //                    ////////GV_PrItems.DataSource = null;
        //                    ////////GV_PrItems.DataBind();
        //                    ////////grdAppHistory.DataSource = null;
        //                    ////////grdAppHistory.DataBind();
        //                    //  }
        //                }
        //                //if (Chkd == 0)
        //                //{
        //                //    MsgCls("Please select one or more PR request to hold !", lblMessageBoard, Color.Red);
        //                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select one or more PR request to hold !')", true);
        //                //    return;
        //                //}
        //                LoadPRRequestGridView();
        //                TxtRemarks.Text = string.Empty;
        //                ViewPRIfo.Visible = false;
        //                FV_PRInfoDisplay.DataSource = null;
        //                FV_PRInfoDisplay.DataBind();
        //                GV_PrItems.DataSource = null;
        //                GV_PrItems.DataBind();
        //                grdAppHistory.DataSource = null;
        //                grdAppHistory.DataBind();
        //                HF_PRID = null;
        //            }

        //        }
        //        else
        //        {
        //            MsgCls("There are no PR Request to hold !", lblMessageBoard, Color.Red);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no PR Request to hold !')", true);
        //            return;
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        //}

        protected void btnHold_Click(object sender, EventArgs e)
        {
            try
            {
                string ReleasedStatus = string.Empty;
                bool? Status = true;
                //if (grdPRAppRej.Rows.Count > 0)
                //{
                //    foreach (GridViewRow item in grdPRAppRej.Rows)
                //    {
                        if (HF_PRID != null)
                        {
                            string PR_id = HF_PRID.Value.ToString().Trim();
                            prbo objBo = new prbo();
                            prbl objBl = new prbl();
                            objBo.BANFN_EXT = int.Parse(HF_PRID.Value.ToString().Trim());
                            objBo.APPROVEDBY1 = User.Identity.Name;
                            objBo.COMMENTS1 = TxtRemarks.Text.Trim();
                            objBo.STATUS = "HOLD1";
                            prcollectionbo objLst = objBl.Get_Requested_PRStatus(objBo);
                            foreach (prbo probjBo in objLst)
                            {
                                ReleasedStatus = probjBo.RSTATUS;

                            }


                            if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name && (ReleasedStatus == "Requested" || ReleasedStatus == "RELEASED1"))
                                objBo.STATUS = "HOLD1";
                            if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved1" || ReleasedStatus == "RELEASED2"))
                                objBo.STATUS = "HOLD2";
                            if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved2" || ReleasedStatus == "RELEASED3"))
                                objBo.STATUS = "HOLD3";
                            if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved3" || ReleasedStatus == "RELEASED4"))
                                objBo.STATUS = "HOLD4";
                            if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved4" || ReleasedStatus == "RELEASED5"))
                                objBo.STATUS = "HOLD5";
                            if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name && (ReleasedStatus == "Approved5" || ReleasedStatus == "RELEASED6"))
                                objBo.STATUS = "HOLD6";

                            if (GV_PrItems.Rows.Count > 0)
                            {
                                foreach (GridViewRow items in GV_PrItems.Rows)
                                {
                                    objBo.BNFPO = GV_PrItems.DataKeys[items.RowIndex]["BNFPO"].ToString();
                                    using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
                                    {
                                        objBo.SAKNR = ddl.SelectedValue;
                                        objBl.Update_PR_Status(objBo, ref Status);
                                        if (Status.Equals(false))
                                        {
                                            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request is on hold !')", true);

                                            MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                                            LoadPRRequestGridView();

                                            TxtRemarks.Text = string.Empty;
                                            FV_PRInfoDisplay.DataSource = null;
                                            FV_PRInfoDisplay.DataBind();
                                            GV_PrItems.DataSource = null;
                                            GV_PrItems.DataBind();
                                            grdAppHistory.DataSource = null;
                                            grdAppHistory.DataBind();

                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request is on hold !');window.location ='Purchase_Requisitions.aspx?PC=P';", true);
                                        }
                                    }

                                }
                            }

                        }

                        LoadPRRequestGridView();
                        TxtRemarks.Text = string.Empty;
                        ViewPRIfo.Visible = false;
                        FV_PRInfoDisplay.DataSource = null;
                        FV_PRInfoDisplay.DataBind();
                        GV_PrItems.DataSource = null;
                        GV_PrItems.DataBind();
                        grdAppHistory.DataSource = null;
                        grdAppHistory.DataBind();
                        HF_PRID = null;
                //    }

                //}
                //else
                //{
                //    MsgCls("There are no PR Request to hold !", lblMessageBoard, Color.Red);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no PR Request to hold !')", true);
                //    return;
                //}
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        //protected void btnRelease_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // int Chkd = 0;
        //        bool? Status = true;
        //        if (grdPRAppRej.Rows.Count > 0)
        //        {
        //            foreach (GridViewRow item in grdPRAppRej.Rows)
        //            {
        //                //using (CheckBox chk = (CheckBox)item.FindControl("chkSelected"))
        //                //    if (chk.Checked)
        //                //    {
        //                if (HF_PRID != null)
        //                {

        //                    string ReleasedStatus = string.Empty;
        //                    //Chkd = Chkd + 1;
        //                    //string PR_id = item.Cells[0].Text;
        //                    string PR_id = HF_PRID.Value.ToString().Trim();
        //                    prbo objBo = new prbo();
        //                    prbl objBl = new prbl();
        //                    // objBo.BANFN_EXT = int.Parse(item.Cells[0].Text);
        //                    objBo.BANFN_EXT = int.Parse(HF_PRID.Value.ToString().Trim());
        //                    objBo.APPROVEDBY1 = User.Identity.Name;
        //                    objBo.COMMENTS1 = TxtRemarks.Text.Trim();
        //                    objBo.STATUS = "RELEASED";

        //                    prcollectionbo objLst = objBl.Get_Requested_PRStatus(objBo);
        //                    foreach (prbo probjBo in objLst)
        //                    {
        //                        ReleasedStatus = probjBo.RSTATUS;

        //                    }

        //                    if (ReleasedStatus == "HOLD1" || ReleasedStatus == "HOLD2" || ReleasedStatus == "HOLD3" ||
        //                        ReleasedStatus == "HOLD4" || ReleasedStatus == "HOLD5" || ReleasedStatus == "HOLD6")
        //                    {


        //                        if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD1")
        //                            objBo.STATUS = "RELEASED1";
        //                        if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD2")
        //                            objBo.STATUS = "RELEASED2";
        //                        if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD3")
        //                            objBo.STATUS = "RELEASED3";
        //                        if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD4")
        //                            objBo.STATUS = "RELEASED4";
        //                        if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD5")
        //                            objBo.STATUS = "RELEASED5";
        //                        if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD6")
        //                            objBo.STATUS = "RELEASED6";

        //                        if (GV_PrItems.Rows.Count > 0)
        //                        {
        //                            foreach (GridViewRow items in GV_PrItems.Rows)
        //                            {
        //                                objBo.BNFPO = GV_PrItems.DataKeys[items.RowIndex]["BNFPO"].ToString();
        //                                using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
        //                                {
        //                                    objBo.SAKNR = ddl.SelectedValue;
        //                                    objBl.Update_PR_Status(objBo, ref Status);

        //                                    if (Status.Equals(false))
        //                                    {

        //                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request is Released!')", true);
        //                                        MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
        //                                        //SendMailMethod(trainingid, startdate, enddate, pernr, status);
        //                                        LoadPRRequestGridView();
        //                                        TxtRemarks.Text = string.Empty;
        //                                        FV_PRInfoDisplay.DataSource = null;
        //                                        FV_PRInfoDisplay.DataBind();
        //                                        GV_PrItems.DataSource = null;
        //                                        GV_PrItems.DataBind();
        //                                        grdAppHistory.DataSource = null;
        //                                        grdAppHistory.DataBind();
        //                                    }
        //                                    //if (ddl != null)
        //                                    //{
        //                                    //    objBo.SAKNR = ddl.SelectedValue;
        //                                    //    objBl.Update_PR_Status(objBo);
        //                                    //}
        //                                    //else
        //                                    //{
        //                                    //    objBo.SAKNR = string.Empty;
        //                                    //    objBl.Update_PR_Status(objBo);
        //                                    //}
        //                                }

        //                            }
        //                        }

        //                        // objBl.Update_PR_Status(objBo);

        //                        //commented monica for status of approve/rej
        //                        //////////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request is Released!')", true);
        //                        //////////MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
        //                        ////////////SendMailMethod(trainingid, startdate, enddate, pernr, status);
        //                        //////////LoadPRRequestGridView();
        //                        //////////TxtRemarks.Text = string.Empty;
        //                        //////////FV_PRInfoDisplay.DataSource = null;
        //                        //////////FV_PRInfoDisplay.DataBind();
        //                        //////////GV_PrItems.DataSource = null;
        //                        //////////GV_PrItems.DataBind();
        //                        //////////grdAppHistory.DataSource = null;
        //                        //////////grdAppHistory.DataBind();
        //                    }

        //                    else
        //                    {
        //                        MsgCls("In order to Release the PR Request it should have been in Hold State!", lblMessageBoard, Color.Red);
        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('In order to Release the PR Request it should have been in Hold State!')", true);
        //                        return;
        //                    }
        //                    // }
        //                }
        //                LoadPRRequestGridView();
        //                TxtRemarks.Text = string.Empty;
        //                ViewPRIfo.Visible = false;
        //                FV_PRInfoDisplay.DataSource = null;
        //                FV_PRInfoDisplay.DataBind();
        //                GV_PrItems.DataSource = null;
        //                GV_PrItems.DataBind();
        //                grdAppHistory.DataSource = null;
        //                grdAppHistory.DataBind();
        //                HF_PRID = null;
        //            }
        //            //if (Chkd == 0)
        //            //{
        //            //    MsgCls("Please select one or more PR request to Release !", lblMessageBoard, Color.Red);
        //            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select one or more PR request to Release !')", true);
        //            //    return;
        //            //}
        //        }
        //        else
        //        {
        //            MsgCls("There are no PR Request to Release !", lblMessageBoard, Color.Red);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no PR Request to Release !')", true);
        //            return;
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }


        //}

        protected void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;
                //if (grdPRAppRej.Rows.Count > 0)
                //{
                //    foreach (GridViewRow item in grdPRAppRej.Rows)
                //    {
                        if (HF_PRID != null)
                        {

                            string ReleasedStatus = string.Empty;
                            string PR_id = HF_PRID.Value.ToString().Trim();
                            prbo objBo = new prbo();
                            prbl objBl = new prbl();
                            objBo.BANFN_EXT = int.Parse(HF_PRID.Value.ToString().Trim());
                            objBo.APPROVEDBY1 = User.Identity.Name;
                            objBo.COMMENTS1 = TxtRemarks.Text.Trim();
                            objBo.STATUS = "RELEASED";

                            prcollectionbo objLst = objBl.Get_Requested_PRStatus(objBo);
                            foreach (prbo probjBo in objLst)
                            {
                                ReleasedStatus = probjBo.RSTATUS;

                            }

                            if (ReleasedStatus == "HOLD1" || ReleasedStatus == "HOLD2" || ReleasedStatus == "HOLD3" ||
                                ReleasedStatus == "HOLD4" || ReleasedStatus == "HOLD5" || ReleasedStatus == "HOLD6")
                            {


                                if (ViewState["APPROVEDBY1"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD1")
                                    objBo.STATUS = "RELEASED1";
                                if (ViewState["APPROVEDBY2"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD2")
                                    objBo.STATUS = "RELEASED2";
                                if (ViewState["APPROVEDBY3"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD3")
                                    objBo.STATUS = "RELEASED3";
                                if (ViewState["APPROVEDBY4"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD4")
                                    objBo.STATUS = "RELEASED4";
                                if (ViewState["APPROVEDBY5"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD5")
                                    objBo.STATUS = "RELEASED5";
                                if (ViewState["APPROVEDBY6"].ToString() == User.Identity.Name && ReleasedStatus == "HOLD6")
                                    objBo.STATUS = "RELEASED6";

                                if (GV_PrItems.Rows.Count > 0)
                                {
                                    foreach (GridViewRow items in GV_PrItems.Rows)
                                    {
                                        objBo.BNFPO = GV_PrItems.DataKeys[items.RowIndex]["BNFPO"].ToString();
                                        using (DropDownList ddl = (DropDownList)GV_PrItems.Rows[items.RowIndex].FindControl("ddlGLAcc"))
                                        {
                                            objBo.SAKNR = ddl.SelectedValue;
                                            objBl.Update_PR_Status(objBo, ref Status);

                                            if (Status.Equals(false))
                                            {

                                                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request is Released!')", true);
                                                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                                                LoadPRRequestGridView();
                                                TxtRemarks.Text = string.Empty;
                                                FV_PRInfoDisplay.DataSource = null;
                                                FV_PRInfoDisplay.DataBind();
                                                GV_PrItems.DataSource = null;
                                                GV_PrItems.DataBind();
                                                grdAppHistory.DataSource = null;
                                                grdAppHistory.DataBind();

                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('PR Request is Released!');window.location ='Purchase_Requisitions.aspx?PC=P';", true);
                                            }
                                        }

                                    }
                                }
                            }

                            else
                            {
                                MsgCls("In order to Release the PR Request it should have been in Hold State!", lblMessageBoard, Color.Red);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('In order to Release the PR Request it should have been in Hold State!')", true);
                                return;
                            }
                        }
                        LoadPRRequestGridView();
                        TxtRemarks.Text = string.Empty;
                        ViewPRIfo.Visible = false;
                        FV_PRInfoDisplay.DataSource = null;
                        FV_PRInfoDisplay.DataBind();
                        GV_PrItems.DataSource = null;
                        GV_PrItems.DataBind();
                        grdAppHistory.DataSource = null;
                        grdAppHistory.DataBind();
                        HF_PRID = null;
                //    }
                //}
                //else
                //{
                //    MsgCls("There are no PR Request to Release !", lblMessageBoard, Color.Red);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no PR Request to Release !')", true);
                //    return;
                //}
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }


        }

        private void LoadMIS_GrpC()
        {
            try
            {
                using (DropDownList ddlmisgroupc = (DropDownList)FV_PRInfoDisplay.FindControl("ddlMISGroupC"))
                {
                    mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_MIS_GRPC();
                    ddlmisgroupc.DataSource = objLst;
                    ddlmisgroupc.DataTextField = "C_DESC";
                    ddlmisgroupc.DataValueField = "CID";
                    ddlmisgroupc.DataBind();
                    ddlmisgroupc.Items.Insert(0, new ListItem("- SELECT -", "0"));
                    ddlmisgroupc.SelectedValue = "0";
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadBillToAddress()
        {
            try
            {
                using (DropDownList shiptoadd = (DropDownList)FV_PRInfoDisplay.FindControl("ddlShipToAddress"))
                {
                    mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_BillShipTo_Address();
                    shiptoadd.DataSource = objLst;
                    shiptoadd.DataTextField = "NAME1";
                    shiptoadd.DataValueField = "WERKS";
                    shiptoadd.DataBind();
                    shiptoadd.Items.Insert(0, new ListItem("- SELECT -", "0"));
                    shiptoadd.SelectedValue = "0";
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadShipToAddress()
        {
            try
            {
                using (DropDownList billtoaddd = (DropDownList)FV_PRInfoDisplay.FindControl("ddlBillToAddress"))
                {
                    mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_BillShipTo_Address();
                    billtoaddd.DataSource = objLst;
                    billtoaddd.DataTextField = "NAME1";
                    billtoaddd.DataValueField = "WERKS";
                    billtoaddd.DataBind();
                    billtoaddd.Items.Insert(0, new ListItem("- SELECT -", "0"));
                    billtoaddd.SelectedValue = "0";
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void LbtnEdit_Click(object sender, EventArgs e)
        {

            LbtnSave.Visible = true;
            LbtnEdit.Visible = false;
            using (DropDownList ddlmisgroupc = (DropDownList)FV_PRInfoDisplay.FindControl("ddlMISGroupC"))
            using (TextBox txtgroupa = (TextBox)FV_PRInfoDisplay.FindControl("txtMISGroupA"))
            using (TextBox txtgroupb = (TextBox)FV_PRInfoDisplay.FindControl("txtMISGroupB"))
            using (DropDownList billtoaddd = (DropDownList)FV_PRInfoDisplay.FindControl("ddlBillToAddress"))

            using (DropDownList shiptoadd = (DropDownList)FV_PRInfoDisplay.FindControl("ddlShipToAddress"))
            using (RadioButton rbtbudgetno = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnBudgetNo"))
            using (RadioButton rbtbudgetyes = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnBudgetYes"))
            using (RadioButton rbtcapitalizedno = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnCapitalizedNo"))
            using (RadioButton rbtcapitalizedyes = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnCapitalizedYes"))
            using (TextBox txtdesccapitalized = (TextBox)FV_PRInfoDisplay.FindControl("txtWillthisItembeCapitalized"))
            {

                LoadBillToAddress();
                LoadShipToAddress();
                LoadMIS_GrpC();

                ddlmisgroupc.Visible = true;
                txtgroupa.Visible = true;
                txtgroupb.Visible = true;
                billtoaddd.Visible = true;
                shiptoadd.Visible = true;
                rbtbudgetno.Visible = true;
                rbtbudgetyes.Visible = true;
                rbtcapitalizedno.Visible = true;
                rbtcapitalizedyes.Visible = true;
                txtdesccapitalized.Visible = true;
                // Budget

                //ViewState["Inbudget"] = Inbudget;


                //ViewState["capitalized"] = capitalized;
                //ViewState["captxt"] = captxt;

                ddlmisgroupc.SelectedValue = ViewState["misgrpc"].ToString();
                txtgroupa.Text = ViewState["misgrpa"].ToString();
                txtgroupb.Text = ViewState["misgrpb"].ToString();
                billtoaddd.SelectedValue = ViewState["billadd"].ToString();
                shiptoadd.SelectedValue = ViewState["shipadd"].ToString();
                if (ViewState["Inbudget"].ToString() == "YES")
                {

                    rbtbudgetyes.Checked = true;
                }
                else
                {
                    rbtbudgetno.Checked = true;

                }
                if (ViewState["capitalized"].ToString() == "YES")
                {

                    rbtcapitalizedyes.Checked = true;
                    txtdesccapitalized.Enabled = true;
                }
                else
                {
                    rbtcapitalizedno.Checked = true;
                    txtdesccapitalized.Enabled = false;

                }
                txtdesccapitalized.Text = ViewState["captxt"].ToString();


            }
        }

        protected void LbtnSave_Click(object sender, EventArgs e)
        {
            LbtnSave.Visible = false;
            LbtnEdit.Visible = true;
            using (DropDownList ddlmisgroupc = (DropDownList)FV_PRInfoDisplay.FindControl("ddlMISGroupC"))
            using (TextBox txtgroupa = (TextBox)FV_PRInfoDisplay.FindControl("txtMISGroupA"))
            using (TextBox txtgroupb = (TextBox)FV_PRInfoDisplay.FindControl("txtMISGroupB"))
            using (DropDownList billtoaddd = (DropDownList)FV_PRInfoDisplay.FindControl("ddlBillToAddress"))

            using (DropDownList shiptoadd = (DropDownList)FV_PRInfoDisplay.FindControl("ddlShipToAddress"))
            using (RadioButton rbtbudgetno = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnBudgetNo"))
            using (RadioButton rbtbudgetyes = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnBudgetYes"))
            using (RadioButton rbtcapitalizedno = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnCapitalizedNo"))
            using (RadioButton rbtcapitalizedyes = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnCapitalizedYes"))
            using (TextBox txtdesccapitalized = (TextBox)FV_PRInfoDisplay.FindControl("txtWillthisItembeCapitalized"))
            {



                prbo PrBO = new prbo();
                prbl PrBl = new prbl();

                PrBO.CREATEDBY = ViewState["createduser"].ToString();
                PrBO.PRID = int.Parse(ViewState["PRID"].ToString());


                PrBO.MIS_GRPC = ddlmisgroupc.SelectedItem.Text;
                PrBO.MIS_GRPA = txtgroupa.Text;
                PrBO.MIS_GRPB = txtgroupb.Text;

                PrBO.BWERKS = billtoaddd.SelectedValue;
                PrBO.SWERKS = shiptoadd.SelectedValue;

                PrBO.IN_BUDGET = rbtbudgetyes.Checked ? "YES" : "NO";
                PrBO.CAPITALIZED = rbtcapitalizedyes.Checked ? "YES" : "NO";
                PrBO.CAP_TEXT = txtdesccapitalized.Text.Trim();

                ViewState["Inbudget"] = PrBO.IN_BUDGET;
                ViewState["misgrpc"] = ddlmisgroupc.SelectedValue;
                ViewState["misgrpa"] = PrBO.MIS_GRPA;
                ViewState["misgrpb"] = PrBO.MIS_GRPB;
                ViewState["billadd"] = PrBO.BWERKS;
                ViewState["shipadd"] = PrBO.SWERKS;
                ViewState["capitalized"] = PrBO.CAPITALIZED;
                ViewState["captxt"] = PrBO.CAP_TEXT;



                PrBl.FinanceUpdate_PR_Request(PrBO);
                prbl PrBlObj = new prbl();
                List<prbo> requisitionboList = new List<prbo>();
                requisitionboList = PrBlObj.Load_PRItemDetails(int.Parse(ViewState["PRID"].ToString()));
                FV_PRInfoDisplay.DataSource = requisitionboList;
                FV_PRInfoDisplay.DataBind();


                ddlmisgroupc.Visible = false;
                txtgroupa.Visible = false;
                txtgroupb.Visible = false;
                billtoaddd.Visible = false;
                shiptoadd.Visible = false;
                rbtbudgetno.Visible = false;
                rbtbudgetyes.Visible = false;
                rbtcapitalizedno.Visible = false;
                rbtcapitalizedyes.Visible = false;
                txtdesccapitalized.Visible = false;


            }
            prbl PrBlObj2 = new prbl();
            List<prbo> requisitionboList2 = new List<prbo>();
            requisitionboList2 = PrBlObj2.Load_PRItemDetails(int.Parse(ViewState["PRID"].ToString()));
            FV_PRInfoDisplay.DataSource = requisitionboList2;
            FV_PRInfoDisplay.DataBind();

        }

        protected void ddlMISGroupC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMIS_GrpAB();
            using (DropDownList ddlmisgroupc = (DropDownList)FV_PRInfoDisplay.FindControl("ddlMISGroupC"))
            {
                ddlmisgroupc.Focus();
            }
        }
        private void LoadMIS_GrpAB()
        {
            try
            {
                masterbo mBo = new masterbo();
                using (DropDownList ddlmisgroupc = (DropDownList)FV_PRInfoDisplay.FindControl("ddlMISGroupC"))
                using (TextBox txtgroupa = (TextBox)FV_PRInfoDisplay.FindControl("txtMISGroupA"))
                using (TextBox txtgroupb = (TextBox)FV_PRInfoDisplay.FindControl("txtMISGroupB"))
                {
                    mBo.LL = ddlmisgroupc.SelectedValue.ToString();


                    mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_MIS_AB(mBo);
                    foreach (masterbo mbo in objLst)
                    {
                        txtgroupa.Text = mbo.A_DESC;
                        txtgroupb.Text = mbo.B_DESC;
                    }
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        protected void rbtnCapitalizedYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (RadioButton rbtcapitalizedno = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnCapitalizedNo"))
                using (RadioButton rbtcapitalizedyes = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnCapitalizedYes"))
                using (TextBox txtdesccapitalized = (TextBox)FV_PRInfoDisplay.FindControl("txtWillthisItembeCapitalized"))
                using (RequiredFieldValidator rfvtxtcapitalized = (RequiredFieldValidator)FV_PRInfoDisplay.FindControl("RFVWillthisItembeCapitalized"))
                {
                    if (rbtcapitalizedyes.Checked)
                    {
                        txtdesccapitalized.Visible = true;
                        txtdesccapitalized.Enabled = true;
                        rfvtxtcapitalized.Enabled = true;
                        rfvtxtcapitalized.Visible = true;
                        rbtcapitalizedyes.Focus();
                    }
                    else
                    {
                        txtdesccapitalized.Visible = false;
                        txtdesccapitalized.Enabled = false;
                        rfvtxtcapitalized.Visible = false;
                        txtdesccapitalized.Text = string.Empty;
                        txtdesccapitalized.Text = "";
                        rbtcapitalizedno.Focus();
                    }


                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }


        protected void rbtnCapitalizedNo_CheckedChanged1(object sender, EventArgs e)
        {
            try
            {
                using (RadioButton rbtcapitalizedno = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnCapitalizedNo"))
                using (RadioButton rbtcapitalizedyes = (RadioButton)FV_PRInfoDisplay.FindControl("rbtnCapitalizedYes"))
                using (TextBox txtdesccapitalized = (TextBox)FV_PRInfoDisplay.FindControl("txtWillthisItembeCapitalized"))
                using (RequiredFieldValidator rfvtxtcapitalized = (RequiredFieldValidator)FV_PRInfoDisplay.FindControl("RFVWillthisItembeCapitalized"))
                {
                    if (rbtcapitalizedno.Checked)
                    {
                        //txtWillthisItembeCapitalized.Visible = false;
                        txtdesccapitalized.Enabled = false;
                        // txtWillthisItembeCapitalized.Visible = false;
                        rfvtxtcapitalized.Enabled = false;
                        rfvtxtcapitalized.Visible = false;
                        txtdesccapitalized.Text = string.Empty;
                        txtdesccapitalized.Text = "";
                        rbtcapitalizedno.Focus();
                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                searchpr();
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, Color.Red);
            }

        }

        public void searchpr()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;
                DateTime createdon = DateTime.Parse(string.IsNullOrEmpty(txtCreatedOn.Text) ? "01/01/0001" : txtCreatedOn.Text);

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

                    prbl prblObj = new prbl();
                    List<prbo> requisitionboList1 = new List<prbo>();
                    EmployeeId = User.Identity.Name;
                    requisitionboList1 = prblObj.Load_ManagerParticularEmpPRDetails(EmployeeId, SelectedType, textSearch, createdon);

                    Session.Add("PRGrdInfo", requisitionboList1);

                    if (requisitionboList1 == null || requisitionboList1.Count == 0)
                    {
                        MsgCls("No Records found", lblMessageBoard, Color.Red);
                        grdPRAppRej.Visible = false;
                        grdPRAppRej.DataSource = null;
                        grdPRAppRej.DataBind();
                        return;
                    }
                    else
                    {
                        MsgCls("", lblMessageBoard, Color.Transparent);
                        grdPRAppRej.Visible = true;
                        grdPRAppRej.DataSource = requisitionboList1;
                        grdPRAppRej.SelectedIndex = -1;
                        grdPRAppRej.DataBind();
                        ViewPRIfo.Visible = false;
                    }

                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                MsgCls(Ex.Message, lblMessageBoard, Color.Red);
            }

        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtCreatedOn.Text = string.Empty;
            LoadPRRequestGridView();
            ViewPRIfo.Visible = false;
            MsgCls("", lblMessageBoard, Color.Transparent);
        }

        protected void grdAppHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-1").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On1").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments1").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-2").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On2").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments2").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-3").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On3").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments3").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-4").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On4").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments4").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-5").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On5").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments5").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-6").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On6").SingleOrDefault()).Visible = true;
                //    ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments6").SingleOrDefault()).Visible = true;

                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY1").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-1").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On1").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments1").SingleOrDefault()).Visible = false;
                //    }

                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY2").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-2").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On2").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments2").SingleOrDefault()).Visible = false;
                //    }
                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY3").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-3").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On3").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments3").SingleOrDefault()).Visible = false;
                //    }
                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY4").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-4").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On4").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments4").SingleOrDefault()).Visible = false;
                //    }
                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY5").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-5").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On5").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments5").SingleOrDefault()).Visible = false;
                //    }
                //    if (DataBinder.Eval(e.Row.DataItem, "APPROVEDBY6").ToString().Equals(""))
                //    {
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approver-6").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Approved On6").SingleOrDefault()).Visible = false;
                //        ((DataControlField)grdAppHistory.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "Comments6").SingleOrDefault()).Visible = false;
                //    }

                //}

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

    }
}
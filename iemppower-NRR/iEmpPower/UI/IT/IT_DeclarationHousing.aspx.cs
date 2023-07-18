using iEmpPower.Old_App_Code.iEmpPowerBL.IT;
using iEmpPower.Old_App_Code.iEmpPowerBO.IT;
using iEmpPower.Old_App_Code.iEmpPowerDAL.IT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace iEmpPower.UI.IT
{
    public partial class IT_DeclarationHousing : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            minmaxdate();
        }

        public void minmaxdate()
        {
            try
            {
                    string month = DateTime.Today.Month.ToString();

                    if (int.Parse(month.Trim()) >= 4)
                    {
                        RV_txtFromDate.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");
                        RV_txtToDate.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        RV_txtFromDate.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                        RV_txtToDate.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                    }


                    RV_txtFromDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                    RV_txtFromDate.ErrorMessage = "From date should be between " + RV_txtFromDate.MinimumValue + "- " + RV_txtFromDate.MaximumValue;
                    RV_txtToDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                    RV_txtToDate.ErrorMessage = "To date should be between " + RV_txtToDate.MinimumValue + "- " + RV_txtToDate.MaximumValue;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    pageLoadEvents();
                    GetFinancialDates();
                    ////GetDeclarationHousing();
                    minmaxdate();
                    LoadSlctdCountryStatesDropDown();
                   
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void pageLoadEvents()
        {
            try
            {
                bool? itlock = false;
                ITdalDataContext objDataContext = new ITdalDataContext();
                objDataContext.usp_IT_GetLockStatus(User.Identity.Name, ref itlock);

                if (itlock == false)
                {
                    MsgCls("", LblLockSts, Color.Transparent);
                    DivHosuing.Visible = true;
                    GetFinancialDates();
                    //GetDeclarationHousing();
                    BindGrid();
                }
                else if (itlock == true)
                {
                    DivHosuing.Visible = false;
                    MsgCls("IT Declaration has been locked. Please contact Payroll Admin.", LblLockSts, Color.Red);
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void BindGrid()
        {
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_IT_GetHousing"))
                {
                    cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvHRA.DataSource = dt;
                            gvHRA.DataBind();
                        }
                    }
                }
            }
        }

        private void LoadSlctdCountryStatesDropDown()
        {
            masterbo mBo = new masterbo();
            mBo.LL = "IN";
            mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_States_For_Slctd_Country(mBo);
            drpdwnState.DataSource = objLst;
            drpdwnState.DataTextField = "BEZEI";
            drpdwnState.DataValueField = "BLAND";
            drpdwnState.DataBind();
            drpdwnState.Items.Insert(0, new ListItem("--Select State--", "0"));
        }

        public void GetDeclarationHousing()
        {
            try
            {
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                // string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_IT_HousingDetails(User.Identity.Name);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    ////NewEntry();

                    return;
                }
                else
                {
                    ////UpDateEntry(ITboObj1);
                    ///loadDetails(ITboObj1);
                    BindGrid();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void loadDetails(List<ITbo> ITboObj1)
        {
            try
            {
                gvHRA.DataSource = ITboObj1;
                gvHRA.DataBind();


                ////ViewState["ID"] = ITboObj1[0].ID.ToString();
                ////BtnCancel.Visible = btnUpdate.Enabled = false;
                ////btnSubmit.Visible = btnSubmit.Enabled = false;
                ////BtnEDIT.Visible = BtnEDIT.Enabled = true;
                ////btnUpdate.Visible = btnUpdate.Enabled = false;
                ////DDl_AccomTyp.SelectedValue = ITboObj1[0].ACCOM.ToString();
                ////DDL_CityCat.SelectedValue = string.IsNullOrEmpty(ITboObj1[0].METRO.ToString().Trim()) ? "-1" : ITboObj1[0].METRO.ToString();
                ////TXT_ActAmount.Text = ITboObj1[0].RTAMT.ToString();
                ////if (ITboObj1[0].HRTXE.ToString() == "1")
                ////{
                ////    CB_ConsAct.Checked = true;
                ////}
                ////if (ITboObj1[0].HRTXE.ToString() == "0")
                ////{
                ////    CB_ConsAct.Checked = false;
                ////}
                ////TXTLandLordAddr.Text = ITboObj1[0].LDAD1.ToString();
                ////TXTPANLAndLord.Text = ITboObj1[0].LDAID.ToString();
                ////if (ITboObj1[0].LDADE.ToString() == "1")
                ////{
                ////    CHK_LLDECLARATION.Checked = true;
                ////}
                ////if (ITboObj1[0].LDADE.ToString() == "0")
                ////{
                ////    CHK_LLDECLARATION.Checked = false;
                ////}
                ////TXTCOMMENTS.Text = ITboObj1[0].EMPCOMMENTS.ToString();


                ////DDl_AccomTyp.Enabled = false;
                ////DDL_CityCat.Enabled = false;
                ////TXT_ActAmount.Enabled = false;
                ////CB_ConsAct.Enabled = false;
                ////TXTLandLordAddr.Enabled = false;
                ////TXTPANLAndLord.Enabled = false;
                ////CHK_LLDECLARATION.Enabled = false;
                ////TXTCOMMENTS.Enabled = false;


                ////if (DDl_AccomTyp.SelectedValue.Trim() == "4")
                ////{
                ////    HousingTab.Visible = false;
                ////}

                ////else if (DDl_AccomTyp.SelectedValue.Trim() == "1")
                ////{
                ////    HousingTab.Visible = true;
                ////}

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        //private void UpDateEntry(List<ITbo> ITboObj1)
        //{
        //    try
        //    {
        //        //ITboObj1.ACCOM = DDl_AccomTyp.SelectedValue.ToString().Trim();
        //        //ITboObj1.METRO = DDL_CityCat.SelectedValue.ToString().Trim() == "1" ? "1" : "0";
        //        //ITboObj1.RTAMT = decimal.Parse(TXT_ActAmount.Text.ToString().Trim());
        //        //ITboObj1.HRTXE = CB_ConsAct.Checked ? int.Parse("1") : int.Parse("0");
        //        //ITboObj1.LDAD1 = TXTLandLordAddr.Text.ToString().Trim();
        //        //ITboObj1.LDAID = TXTPANLAndLord.Text.ToString().Trim();
        //        //ITboObj1.LDADE = CHK_LLDECLARATION.Checked ? "1" : "0";
        //        //ITboObj1.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();
        //        ViewState["ID"] = ITboObj1[0].ID.ToString();
        //        BtnCancel.Visible = btnUpdate.Enabled = false;
        //        btnSubmit.Visible = btnSubmit.Enabled = false;
        //        BtnEDIT.Visible = BtnEDIT.Enabled = true;
        //        btnUpdate.Visible = btnUpdate.Enabled = false;
        //        DDl_AccomTyp.SelectedValue = ITboObj1[0].ACCOM.ToString();
        //        DDL_CityCat.SelectedValue = string.IsNullOrEmpty(ITboObj1[0].METRO.ToString().Trim()) ? "-1" : ITboObj1[0].METRO.ToString();
        //        TXT_ActAmount.Text = ITboObj1[0].RTAMT.ToString();
        //        if (ITboObj1[0].HRTXE.ToString() == "1")
        //        {
        //            CB_ConsAct.Checked = true;
        //        }
        //        if (ITboObj1[0].HRTXE.ToString() == "0")
        //        {
        //            CB_ConsAct.Checked = false;
        //        }
        //        TXTLandLordAddr.Text = ITboObj1[0].LDAD1.ToString();
        //        TXTPANLAndLord.Text = ITboObj1[0].LDAID.ToString();
        //        if (ITboObj1[0].LDADE.ToString() == "1")
        //        {
        //            CHK_LLDECLARATION.Checked = true;
        //        }
        //        if (ITboObj1[0].LDADE.ToString() == "0")
        //        {
        //            CHK_LLDECLARATION.Checked = false;
        //        }
        //        TXTCOMMENTS.Text = ITboObj1[0].EMPCOMMENTS.ToString();


        //        DDl_AccomTyp.Enabled = false;
        //        DDL_CityCat.Enabled = false;
        //        TXT_ActAmount.Enabled = false;
        //        CB_ConsAct.Enabled = false;
        //        TXTLandLordAddr.Enabled = false;
        //        TXTPANLAndLord.Enabled = false;
        //        CHK_LLDECLARATION.Enabled = false;
        //        TXTCOMMENTS.Enabled = false;


        //        if (DDl_AccomTyp.SelectedValue.Trim() == "4")
        //        {
        //            HousingTab.Visible = false;
        //        }

        //        else if (DDl_AccomTyp.SelectedValue.Trim() == "1")
        //        {
        //            HousingTab.Visible = true;
        //        }

        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        //}

        //private void NewEntry()
        //{
        //    try
        //    {
        //        DDl_AccomTyp.SelectedValue = "";
        //        DDL_CityCat.SelectedValue = "";
        //        TXT_ActAmount.Text = "";
        //        CB_ConsAct.Checked = false;
        //        TXTLandLordAddr.Text = "";
        //        TXTPANLAndLord.Text = "";
        //        CHK_LLDECLARATION.Checked = false;
        //        TXTCOMMENTS.Text = "";
        //        btnSubmit.Visible = btnSubmit.Enabled = true;
        //        BtnEDIT.Visible = BtnEDIT.Enabled = false;
        //        btnUpdate.Visible = btnUpdate.Enabled = false;
        //        BtnCancel.Visible = btnUpdate.Enabled = false;

        //        DDl_AccomTyp.Enabled = true;
        //        DDL_CityCat.Enabled = true;
        //        TXT_ActAmount.Enabled = true;
        //        CB_ConsAct.Enabled = true;
        //        TXTLandLordAddr.Enabled = true;
        //        TXTPANLAndLord.Enabled = true;
        //        CHK_LLDECLARATION.Enabled = true;
        //        TXTCOMMENTS.Enabled = true;

        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        //}

        private void GetFinancialDates()
        {
            try
            {
                //DateTime dt = DateTime.Now;
                int month = int.Parse(DateTime.Today.Month.ToString());
                if (month > 3)
                {
                    LblFromDate.Text = DateTime.Today.Year.ToString();
                    LblToDate.Text = (DateTime.Today.Year + 1).ToString();
                }
                else if (month <= 3)
                {
                    LblFromDate.Text = (DateTime.Today.Year - 1).ToString();
                    LblToDate.Text = DateTime.Today.Year.ToString();
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

        //protected void DDl_AccomTyp_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (DDl_AccomTyp.SelectedValue.ToString().Trim() == "1")
        //        {

        //            ////HousingTab.Visible = true;
        //            ////LandLordsTab.Visible = true;
        //            RF_DDL_CityCat.Enabled = true;
        //            RFV_TXT_ActAmount.Enabled = true;
        //            RFV_TXTLandLordAddr.Enabled = true;
        //            RFV_TXTPANLAndLord.Enabled = true;
        //            REVTXT_ActAmount.Enabled = true;

        //        }
        //        else if (DDl_AccomTyp.SelectedValue.ToString().Trim() == "4")
        //        {
        //            ////HousingTab.Visible = false;
        //            ////LandLordsTab.Visible = true;
        //            RF_DDL_CityCat.Enabled = false;
        //            RFV_TXT_ActAmount.Enabled = false;
        //            REVTXT_ActAmount.Enabled = false;
        //            RFV_TXTLandLordAddr.Enabled = true;
        //            RFV_TXTPANLAndLord.Enabled = true;

        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}

        //protected void btnSubmitITHousing_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool? sts = false;
        //        int? ITHOUSINGIT = 0;
        //        ITbl ITblObj = new ITbl();
        //        ITbo ITboObj = new ITbo();

        //        ITboObj.CREATED_BY = User.Identity.Name;
        //        ITboObj.PERNR = User.Identity.Name;
        //        ITboObj.STATUS = "Requested";
        //        ITboObj.CREATED_ON = DateTime.Now;
        //        ITboObj.BEGDA = DateTime.Parse(txtFromDate.Text);////DateTime.Now;
        //        ITboObj.ENDDA = DateTime.Parse(txtToDate.Text);////DateTime.Now;
        //        ITboObj.MODIFIED_BY = "";
        //        ITboObj.Flag = 1;
        //        ITboObj.ID = 0;
        //        ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();

        //        if (DDl_AccomTyp.SelectedValue.ToString().Trim() == "1")
        //        {

        //            ITboObj.ACCOM = DDl_AccomTyp.SelectedValue.ToString().Trim();
        //            ITboObj.METRO = DDL_CityCat.SelectedValue.ToString().Trim() == "1" ? "1" : "0";
        //            ITboObj.RTAMT = decimal.Parse(TXT_ActAmount.Text.ToString().Trim());
        //            ITboObj.HRTXE = CB_ConsAct.Checked ? int.Parse("1") : int.Parse("0");
        //            ITboObj.LDAD1 = TXTLandLordAddr.Text.ToString().Trim();
        //            ITboObj.LDAID = TXTPANLAndLord.Text.ToString().Trim();
        //            ITboObj.LDADE = CHK_LLDECLARATION.Checked ? "1" : "0";

        //            ITblObj.Create_ITHousing(ITboObj, ref ITHOUSINGIT, ref sts);

        //            GetDeclarationHousing();
        //            if (sts == true)
        //            {
        //                SendMailSec80C(ITHOUSINGIT, "Submit"); 
        //                MsgCls("IT Housing created successfully !", lblMessageBoard, Color.Green);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Housing created successfully !')", true);
        //            }

        //        }
        //        else if (DDl_AccomTyp.SelectedValue.ToString().Trim() == "4")
        //        {
        //            ITboObj.ACCOM = DDl_AccomTyp.SelectedValue.ToString().Trim();
        //            ITboObj.METRO = "";
        //            ITboObj.RTAMT = decimal.Parse("0.00");
        //            ITboObj.HRTXE = int.Parse("0");
        //            ITboObj.LDAD1 = TXTLandLordAddr.Text.ToString().Trim();
        //            ITboObj.LDAID = TXTPANLAndLord.Text.ToString().Trim();
        //            ITboObj.LDADE = CHK_LLDECLARATION.Checked ? "1" : "0";

        //            ITblObj.Create_ITHousing(ITboObj, ref ITHOUSINGIT, ref sts);
        //            GetDeclarationHousing();
        //            if (sts == true)
        //            {
        //                SendMailSec80C(ITHOUSINGIT, "Submit"); 
        //                MsgCls("IT Housing created successfully !", lblMessageBoard, Color.Green);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Housing created successfully !')", true);
        //            }

        //        }
        //    }
        //    catch (Exception Ex)
        //    {

        //        switch (Ex.Message)
        //        {


        //            case "-10":
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Record already exists for this financial year');", true);
        //                MsgCls("Record already exists for this financial year", lblMessageBoard, Color.Red);
                      
        //                break;
        //            default:
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
        //                break;
        //        }
                
        //    }
        //}

        private void SendMailSec80C(int? ITHID, string type)
        {
            try
            {
                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;
                //string APPROVED_BY1 = "";
                //string Approver_Name = "";
                //string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";

                ITdalDataContext objcontext = new ITdalDataContext();

                objcontext.usp_IT_Get_MailListSec80(ITHID, User.Identity.Name, ref EMP_Name, ref EMP_Email);

                strSubject = "Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.";

                RecipientsString = "monica.ks@itchamps.com";
                strPernr_Mail = "latha.mg@itchamps.com";

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.<br/><br/></b>";
                body += "<b>Income Tax ID  :  " + ITHID + "</b><br/><br/>";
                body += "<b>Income Tax Type :  " + "Housing" + "</b><br/><br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Income Tax Request submitted successfully and Mail sent successfully.";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        } 

        //protected void BtnEDIT_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        BtnCancel.Visible = btnUpdate.Enabled = true;
        //        BtnEDIT.Visible = BtnEDIT.Enabled = false;
        //        btnUpdate.Visible = btnUpdate.Enabled = true;
        //        DDl_AccomTyp.Enabled = true;
        //        DDL_CityCat.Enabled = true;
        //        TXT_ActAmount.Enabled = true;
        //        CB_ConsAct.Enabled = true;
        //        TXTLandLordAddr.Enabled = true;
        //        TXTPANLAndLord.Enabled = true;
        //        CHK_LLDECLARATION.Enabled = true;
        //        TXTCOMMENTS.Enabled = true;

        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}

        //protected void btnUpdateITHousing_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ITbl ITblObj = new ITbl();
        //        ITbo ITboObj = new ITbo();

        //        bool? sts = false;
        //        int? ITHOUSINGIT = 0;

        //        ITboObj.CREATED_BY = User.Identity.Name;
        //        ITboObj.PERNR = User.Identity.Name;
        //        ITboObj.STATUS = "Updated";
        //        ITboObj.CREATED_ON = DateTime.Now;
        //        ITboObj.BEGDA = DateTime.Parse(txtFromDate.Text);////DateTime.Now;
        //        ITboObj.ENDDA = DateTime.Parse(txtToDate.Text);////DateTime.Now;
        //        ITboObj.MODIFIED_BY = User.Identity.Name;
        //        ITboObj.Flag = 2;
        //        ITboObj.ID = int.Parse(ViewState["ID"].ToString().Trim());
        //        ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();

        //        if (DDl_AccomTyp.SelectedValue.ToString().Trim() == "1")
        //        {

        //            ITboObj.ACCOM = DDl_AccomTyp.SelectedValue.ToString().Trim();
        //            ITboObj.METRO = DDL_CityCat.SelectedValue.ToString().Trim() == "1" ? "1" : "0";
        //            ITboObj.RTAMT = decimal.Parse(TXT_ActAmount.Text.ToString().Trim());
        //            ITboObj.HRTXE = CB_ConsAct.Checked ? int.Parse("1") : int.Parse("0");
        //            ITboObj.LDAD1 = TXTLandLordAddr.Text.ToString().Trim();
        //            ITboObj.LDAID = TXTPANLAndLord.Text.ToString().Trim();
        //            ITboObj.LDADE = CHK_LLDECLARATION.Checked ? "1" : "0";

        //            ITblObj.Create_ITHousing(ITboObj,ref ITHOUSINGIT, ref sts); 
        //            GetDeclarationHousing();
        //            if (sts == true)
        //            {
        //                SendMailSec80C(ITHOUSINGIT, "Updated"); 
        //                MsgCls("IT Housing updated successfully !", lblMessageBoard, Color.Green);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Housing updated successfully !')", true);
        //            }


        //        }
        //        else if (DDl_AccomTyp.SelectedValue.ToString().Trim() == "4")
        //        {
        //            ITboObj.ACCOM = DDl_AccomTyp.SelectedValue.ToString().Trim();
        //            ITboObj.METRO = "";
        //            ITboObj.RTAMT = decimal.Parse("0.00");
        //            ITboObj.HRTXE = int.Parse("0");
        //            ITboObj.LDAD1 = TXTLandLordAddr.Text.ToString().Trim();
        //            ITboObj.LDAID = TXTPANLAndLord.Text.ToString().Trim();
        //            ITboObj.LDADE = CHK_LLDECLARATION.Checked ? "1" : "0";

        //            ITblObj.Create_ITHousing(ITboObj, ref ITHOUSINGIT, ref sts); 
        //            GetDeclarationHousing();
        //            if (sts == true)
        //            {
        //                SendMailSec80C(ITHOUSINGIT, "Updated");
        //                MsgCls("IT Housing updated successfully !", lblMessageBoard, Color.Green);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Housing updated successfully !')", true);
        //            }

        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                GetDeclarationHousing();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void Insert(object sender, EventArgs e)
        {
            try
            {
                bool? sts = false;
                int? ITHOUSINGIT = 0;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();

                ITboObj.CREATED_BY = User.Identity.Name;
                ITboObj.PERNR = User.Identity.Name;
                ITboObj.STATUS = "Requested";
                ITboObj.CREATED_ON = DateTime.Now;
                ITboObj.BEGDA = DateTime.Parse(txtFromDate.Text);////DateTime.Now;
                ITboObj.ENDDA = DateTime.Parse(txtToDate.Text);////DateTime.Now;
                ITboObj.MODIFIED_BY = "";
                ITboObj.Flag = 1;
                ITboObj.ID = 0;
                ITboObj.EMPCOMMENTS = "";//// TXTCOMMENTS.Text.ToString().Trim();

                ////if (DDl_AccomTyp.SelectedValue.ToString().Trim() == "1")
                {

                    ITboObj.ACCOM = "1";//// DDl_AccomTyp.SelectedValue.ToString().Trim();
                    ITboObj.METRO = DDL_CityCat.SelectedValue.ToString().Trim() == "1" ? "1" : "0";
                    ITboObj.RTAMT = decimal.Parse(TXT_ActAmount.Text.ToString().Trim());
                    ITboObj.HRTXE = 0;//// CB_ConsAct.Checked ? int.Parse("1") : int.Parse("0");
                    ITboObj.LDAD1 = TXTLandLordAddr.Text.ToString().Trim();
                    ITboObj.LDAID = TXTPANLAndLord.Text.ToString().Trim();
                    ITboObj.LDADE = "0";//// CHK_LLDECLARATION.Checked ? "1" : "0";
                    ITboObj.Address = txtAddress.Text;
                    ITboObj.State = drpdwnState.SelectedValue.ToString();
                    ITboObj.City = txtCity.Text;
                    ITboObj.LDNAM = TXTLandLordName.Text;

                    ITblObj.Create_ITHousing(ITboObj, ref ITHOUSINGIT, ref sts);

                    ////GetDeclarationHousing();
                    this.BindGrid();
                    if (sts == true)
                    {
                        SendMailSec80C(ITHOUSINGIT, "Submit");
                        MsgCls("IT Housing created successfully !", lblMessageBoard, Color.Green);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Housing created successfully !')", true);
                    }

                }

            }
            catch (Exception Ex)
            {

                switch (Ex.Message)
                {


                    case "-10":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Record already exists for this financial year');", true);
                        MsgCls("Record already exists for this financial year", lblMessageBoard, Color.Red);

                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        break;
                }

            }


            //string name = txtName.Text;
            //string country = txtCountry.Text;
            //string constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd = new SqlCommand("usp_IT_Submit_Housing"))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@Action", "INSERT");
            //        cmd.Parameters.AddWithValue("@Name", name);
            //        cmd.Parameters.AddWithValue("@Country", country);
            //        cmd.Connection = con;
            //        con.Open();
            //        cmd.ExecuteNonQuery();
            //        con.Close();
            //    }
            //}
            //this.BindGrid();



        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvHRA.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        //protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {

        //        bool? sts = false;
        //        int? ITHOUSINGIT = 0;
        //        //------------------------------------------------------------------
        //        GridViewRow row = gvHRA.Rows[e.RowIndex];
        //        int Id = Convert.ToInt32(gvHRA.DataKeys[e.RowIndex].Values[0]);


        //        //string name = (row.FindControl("txtName") as TextBox).Text;
        //        //string country = (row.FindControl("txtCountry") as TextBox).Text;

             

        //        string CREATED_BY = User.Identity.Name;
        //        string PERNR = User.Identity.Name;
        //        string STATUS = "Updated";
        //        DateTime CREATED_ON = DateTime.Now;
        //        DateTime BEGDA = DateTime.Parse((row.FindControl("txtFromDate") as TextBox).Text);////DateTime.Now;
        //        DateTime ENDDA = DateTime.Parse((row.FindControl("txtToDate") as TextBox).Text);////DateTime.Now;
        //        string MODIFIED_BY = User.Identity.Name;
        //        int Flag = 2;
        //        ////int ID = int.Parse(ViewState["ID"].ToString().Trim());
        //        string EMPCOMMENTS = "";//// TXTCOMMENTS.Text.ToString().Trim();
        //        string ACCOM = "1"; ////DDl_AccomTyp.SelectedValue.ToString().Trim();
        //        string METRO = DDL_CityCat.SelectedValue.ToString().Trim() == "1" ? "1" : "0";
        //        decimal RTAMT = decimal.Parse((row.FindControl("txtRentPerMonth") as TextBox).Text.ToString().Trim());
        //        int HRTXE = 0;////CB_ConsAct.Checked ? int.Parse("1") : int.Parse("0");
        //        string LDAD1 = (row.FindControl("txtLandLordsAddress") as TextBox).Text.ToString().Trim();
        //        string LDAID = (row.FindControl("txtPANofLandLord") as TextBox).Text.ToString().Trim();
        //        string LDADE = "0";//// CHK_LLDECLARATION.Checked ? "1" : "0";
        //        string Address = (row.FindControl("txtAddress") as TextBox).Text.ToString();
        //        string State = (row.FindControl("drpdwnState") as DropDownList).SelectedValue.ToString();
        //        string City = (row.FindControl("txtCity") as TextBox).Text;
        //        string LDNAM = (row.FindControl("txtLandLordsName") as TextBox).Text;


        //        using (SqlConnection con = new SqlConnection(constr))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("usp_IT_Submit_Housing"))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@ID", Id);
        //                cmd.Parameters.AddWithValue("@PERNR", CREATED_BY);
        //                cmd.Parameters.AddWithValue("@BEGDA", BEGDA);
        //                cmd.Parameters.AddWithValue("@ENDDA", ENDDA);
        //                cmd.Parameters.AddWithValue("@ACCOM", ACCOM);
        //                cmd.Parameters.AddWithValue("@METRO", METRO);
        //                cmd.Parameters.AddWithValue("@RTAMT", RTAMT);
        //                cmd.Parameters.AddWithValue("@HRTXE", HRTXE);
        //                cmd.Parameters.AddWithValue("@LDAD1", LDAD1);
        //                cmd.Parameters.AddWithValue("@LDAID", LDAID);
        //                cmd.Parameters.AddWithValue("@LDADE", LDADE);
        //                cmd.Parameters.AddWithValue("@CREATED_BY", CREATED_BY);
        //                cmd.Parameters.AddWithValue("@CREATED_ON", CREATED_ON);
        //                cmd.Parameters.AddWithValue("@MODIFIED_BY", MODIFIED_BY);
        //                cmd.Parameters.AddWithValue("@STATUS", STATUS);
        //                cmd.Parameters.AddWithValue("@FLAG", Flag);
        //                cmd.Parameters.AddWithValue("@EMPCOMMENTS", EMPCOMMENTS);
        //                cmd.Parameters.AddWithValue("@Address", Address);
        //                cmd.Parameters.AddWithValue("@State", State);
        //                cmd.Parameters.AddWithValue("@City", City);
        //                cmd.Parameters.AddWithValue("@LDNAM", LDNAM);
        //                cmd.Parameters.AddWithValue("@HID", ref ITHOUSINGIT);
        //                cmd.Parameters.AddWithValue("@STS", ref sts);
        //                cmd.Connection = con;
        //                con.Open();
        //                cmd.ExecuteNonQuery();
        //                con.Close();
        //                if (sts == true)
        //                {
        //                    SendMailSec80C(ITHOUSINGIT, "Updated");
        //                    MsgCls("IT Housing updated successfully !", lblMessageBoard, Color.Green);
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Housing updated successfully !')", true);
        //                }
        //            }
        //        }
        //        gvHRA.EditIndex = -1;
        //        this.BindGrid();


        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                bool? sts = false;
                int? ITHOUSINGIT = 0;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();


                GridViewRow row = gvHRA.Rows[e.RowIndex];
                int Id = Convert.ToInt32(gvHRA.DataKeys[e.RowIndex].Values[0]);


                //string name = (row.FindControl("txtName") as TextBox).Text;
                //string country = (row.FindControl("txtCountry") as TextBox).Text;


                ITboObj.CREATED_BY = User.Identity.Name;
                ITboObj.PERNR = User.Identity.Name;
                ITboObj.STATUS = "Updated";
                ITboObj.CREATED_ON = DateTime.Now;
                ITboObj.BEGDA = DateTime.Parse((row.FindControl("txtFromDate") as TextBox).Text);////DateTime.Now;
                ITboObj.ENDDA = DateTime.Parse((row.FindControl("txtToDate") as TextBox).Text);////DateTime.Now;
                ITboObj.MODIFIED_BY = User.Identity.Name;
                ITboObj.Flag = 2;
                ITboObj.ID = Id;
                ITboObj.EMPCOMMENTS = "";//// TXTCOMMENTS.Text.ToString().Trim();

                ////if (DDl_AccomTyp.SelectedValue.ToString().Trim() == "1")
                {

                    ITboObj.ACCOM = "1";//// DDl_AccomTyp.SelectedValue.ToString().Trim();
                    ITboObj.METRO = DDL_CityCat.SelectedValue.ToString().Trim() == "1" ? "1" : "0";
                    ITboObj.RTAMT = decimal.Parse((row.FindControl("txtRentPerMonth") as TextBox).Text.ToString().Trim());
                    ITboObj.HRTXE = 0;//// CB_ConsAct.Checked ? int.Parse("1") : int.Parse("0");
                    ITboObj.LDAD1 = (row.FindControl("txtLandLordsAddress") as TextBox).Text.ToString().Trim();
                    ITboObj.LDAID = (row.FindControl("txtPANofLandLord") as TextBox).Text.ToString().Trim();
                    ITboObj.LDADE = "0";//// CHK_LLDECLARATION.Checked ? "1" : "0";
                    ITboObj.Address = (row.FindControl("txtAddress") as TextBox).Text.ToString();
                    ITboObj.State = (row.FindControl("drpdwnState") as DropDownList).SelectedValue.ToString();
                    ITboObj.City = (row.FindControl("txtCity") as TextBox).Text;
                    ITboObj.LDNAM = (row.FindControl("txtLandLordsName") as TextBox).Text;

                    ITblObj.Create_ITHousing(ITboObj, ref ITHOUSINGIT, ref sts);

                    ////GetDeclarationHousing();
                    ////this.BindGrid();
                    if (sts == true)
                    {
                        SendMailSec80C(ITHOUSINGIT, "Updated");
                        MsgCls("IT Housing updated successfully !", lblMessageBoard, Color.Green);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Housing updated successfully !')", true);
                    }

                }

                gvHRA.EditIndex = -1;
                this.BindGrid();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            gvHRA.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(gvHRA.DataKeys[e.RowIndex].Values[0]);
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_IT_Submit_Housing_Delete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            this.BindGrid();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }

            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvHRA.EditIndex)
            {
                (e.Row.Cells[10].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }

            ////if (e.Row.RowType == DataControlRowType.DataRow)
            ////{
            ////    DropDownList drpdwnState = (e.Row.FindControl("drpdwnState") as DropDownList);


            ////    masterbo mBo = new masterbo();
            ////    mBo.LL = "IN";
            ////    mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_States_For_Slctd_Country(mBo);
            ////    drpdwnState.DataSource = objLst;
            ////    drpdwnState.DataTextField = "BEZEI";
            ////    drpdwnState.DataValueField = "BLAND";
            ////    drpdwnState.DataBind();
            ////    drpdwnState.Items.Insert(0, new ListItem("--Select State--", "0"));
            ////}   

            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                // BIND THE "DROPDOWNLIST" WITH THE DATASET FILLED WITH "QUALIFICATION" DETAILS.
                DropDownList drpdwnState = new DropDownList();
                drpdwnState = (DropDownList)e.Row.FindControl("drpdwnState");

                if (drpdwnState != null)
                {
                    masterbo mBo = new masterbo();
                    mBo.LL = "IN";
                    mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_States_For_Slctd_Country(mBo);
                    drpdwnState.DataSource = objLst;
                    drpdwnState.DataTextField = "BEZEI";
                    drpdwnState.DataValueField = "BLAND";
                    drpdwnState.DataBind();
                    drpdwnState.Items.Insert(0, new ListItem("--Select State--", "0"));

                    // ASSIGN THE SELECTED ROW VALUE ("QUALIFICATION CODE") TO THE DROPDOWNLIST SELECTED VALUE.
                    ((DropDownList)e.Row.FindControl("drpdwnState")).SelectedValue = DataBinder.Eval(e.Row.DataItem, "state").ToString();
                }
            }
        }

        protected void TXT_ActAmount_TextChanged(object sender, EventArgs e)
        {
            DateTime FromDate = DateTime.Parse(txtFromDate.Text);
            DateTime ToDate = DateTime.Parse(txtToDate.Text);
            int m1 = (ToDate.Month - FromDate.Month);//for years
            int m2 = (ToDate.Year - FromDate.Year) * 12; //for months
            int months = m1 + m2+1;
            lblPeriodRent.Text = (months * int.Parse(TXT_ActAmount.Text)).ToString();
        }

        protected void OnTextChanged(object sender, EventArgs e)
        {
            try
        {
            TextBox thisTextBox = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;
            TextBox txtRentPerMonth = (TextBox)currentRow.FindControl("txtRentPerMonth");
            TextBox txtFromDate = (TextBox)currentRow.FindControl("txtFromDate");
            TextBox txtToDate = (TextBox)currentRow.FindControl("txtToDate");
            Label lblPeriodRent = (Label)currentRow.FindControl("lblPeriodRent");
            DateTime FromDate = DateTime.Parse(txtFromDate.Text);
            DateTime ToDate = DateTime.Parse(txtToDate.Text);
            int m1 = (ToDate.Month - FromDate.Month);//for years
            int m2 = (ToDate.Year - FromDate.Year) * 12; //for months
            int months = m1 + m2 + 1;
            lblPeriodRent.Text = (months * int.Parse(txtRentPerMonth.Text)).ToString();     
        }
       catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

    }
}
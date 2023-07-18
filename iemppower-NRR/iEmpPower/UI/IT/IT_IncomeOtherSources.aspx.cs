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

namespace iEmpPower.UI.IT
{
    public partial class IT_IncomeOtherSources : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    pageLoadEvents();
                    GetFinancialDates();


                    //DIVCOMMENTS.Visible = false;
                    //BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                    //BtnEDIT.Visible = BtnEDIT.Enabled = false;
                    //btnUpdate.Visible = btnUpdate.Enabled = false;
                    //BtnCancel.Visible = BtnCancel.Enabled = false;
                    //MV_IncomeSources.Visible = false;

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
                    DivOthers.Visible = true;
                    GetFinancialDates();
                    DIVCOMMENTS.Visible = false;
                    BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                    BtnEDIT.Visible = BtnEDIT.Enabled = false;
                    btnUpdate.Visible = btnUpdate.Enabled = false;
                    BtnCancel.Visible = BtnCancel.Enabled = false;
                    MV_IncomeSources.Visible = false;

                }
                else if (itlock == true)
                {
                    DivOthers.Visible = false;
                    MsgCls("IT Declaration has been locked. Please contact Payroll Admin.", LblLockSts, Color.Red);
                }




            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

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
                    LblFrom.Text = DateTime.Today.Year.ToString();
                    LblTo.Text = (DateTime.Today.Year + 1).ToString();
                }
                else if (month <= 3)
                {
                    LblFromDate.Text = (DateTime.Today.Year - 1).ToString();
                    LblToDate.Text = DateTime.Today.Year.ToString();
                    LblFrom.Text = (DateTime.Today.Year - 1).ToString();
                    LblTo.Text = DateTime.Today.Year.ToString();
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

        protected void DDl_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MV_IncomeSources.Visible = true;
                if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
                {
                    DIVINCOMETYP.Visible = true;
                    //MV_IncomeSources.SetActiveView(ViewHousing);
                    ClearControls();
                    ClearControlsView2();

                    BTNSubmitHousingOthers.Visible = true;
                    GetDeclarationHousingOthers();
                    DIVCOMMENTS.Visible = true;
                    MV_IncomeSources.SetActiveView(ViewHousing);

                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
                {
                    DIVINCOMETYP.Visible = true;

                    ClearControls();
                    ClearControlsView2();

                    BTNSubmitHousingOthers.Visible = true;
                    GetDeclarationHousingOthers();
                    MV_IncomeSources.SetActiveView(ViewOtherSources);
                    DIVCOMMENTS.Visible = true;
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "0")
                {
                    DIVINCOMETYP.Visible = false;
                    ClearControls();
                    ClearControlsView2();
                    DIVCOMMENTS.Visible = false;
                    BTNSubmitHousingOthers.Visible = false;
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }


        public void GetDeclarationHousingOthers()
        {
            try
            {
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                // string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_IT_HousingOthersDetails(User.Identity.Name, DDl_TYPE.SelectedValue.ToString().Trim());

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    NewEntry();

                    return;
                }
                else
                {
                    UpDateEntry(ITboObj1);
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void HousingProprtyType(string type)
        {
            try
            {
                if (type.ToString().Trim() == "1")
                {
                    Div1.Visible = true;
                    Div2.Visible = false;
                    RFV_TXT_DedIntr.Enabled = true;
                    ClearControls();
                }
                else if (type.ToString().Trim() == "2")
                {
                    Div1.Visible = false;
                    Div2.Visible = true;
                    RFV_TXT_DedIntr.Enabled = false;
                    ClearControls();
                }
                else if (type.ToString().Trim() == "3")
                {
                    Div1.Visible = false;
                    Div2.Visible = true;
                    RFV_TXT_DedIntr.Enabled = false;
                    ClearControls();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }
        protected void RB_PropTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RB_PropTyp.SelectedValue.ToString().Trim() == "1")
                {
                    Div1.Visible = true;
                    Div2.Visible = false;
                    RFV_TXT_DedIntr.Enabled = true;
                    ClearControls();
                }
                else if (RB_PropTyp.SelectedValue.ToString().Trim() == "2")
                {
                    Div1.Visible = false;
                    Div2.Visible = true;
                    RFV_TXT_DedIntr.Enabled = false;
                    ClearControls();
                }
                else if (RB_PropTyp.SelectedValue.ToString().Trim() == "3")
                {
                    Div1.Visible = false;
                    Div2.Visible = true;
                    RFV_TXT_DedIntr.Enabled = false;
                    ClearControls();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void ClearControls()
        {
            try
            {
                TXT_DedIntr.Text = "";
                TXT_DEDINT.Text = "";
                TXT_FNLLETVALUE.Text = "";
                Lbl_DEDREPAIR.Text = "0";
                TXTDECOTHERS.Text = "";
                TXTTDS.Text = "";
                TXTCOMMENTS.Text = "";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void ClearControlsView2()
        {
            try
            {
                TXT_BusnProfits.Text = "";
                TXT_LTCG.Text = "";
                TXT_LTCGS.Text = "";
                TXT_STCG.Text = "";
                TXT_STCGLS.Text = "";
                TXT_IFD.Text = "";
                TXT_IFI.Text = "";
                TXT_OI.Text = "";
                TXT_TDSI.Text = "";
                TXTCOMMENTS2.Text = "";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void BTNSubmitHousingOthers_Click(object sender, EventArgs e)
        {
            try
            {
                bool? sts = false;
                int? ITOTHERSID = 0;

                if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
                {
                    if (RB_PropTyp.SelectedValue.ToString().Trim() == "1")
                    {
                        decimal amt = string.IsNullOrEmpty(TXT_DedIntr.Text) ? 0 : decimal.Parse(TXT_DedIntr.Text.ToString().Trim());
                        // decimal amt = decimal.Parse(TXT_DedIntr.Text.ToString().Trim());
                        if ((amt > 200000) || (amt <= 0))
                        {
                            lblMessageBoard.Text = "Deduce Interest u/s 24 should be greater than 0 and less than or equal to 2 lakhs";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Deduce Interest u/s 24 should be greater than 0 and less than or equal to 2 lakhs');", true);
                            return;
                        }
                        else
                        {
                            ITbl ITblObj = new ITbl();
                            ITbo ITboObj = new ITbo();
                            //------Housing property
                            ITboObj.PROPTYP = "1";
                            ITboObj.RENTO = RB_PropTyp.SelectedValue.ToString().Trim();
                            ITboObj.INT24 = string.IsNullOrEmpty(TXT_DedIntr.Text) ? 0 : decimal.Parse(TXT_DedIntr.Text.ToString().Trim());
                            ITboObj.LETVL = 0;
                            ITboObj.REP24 = 0;
                            ITboObj.OTH24 = 0;
                            ITboObj.TDSOT = 0;
                            //------Others
                            ITboObj.BSPFT = 0;
                            ITboObj.CPGLN = 0;
                            ITboObj.CPGLS = 0;
                            ITboObj.CPGNS = 0;
                            ITboObj.CPGSS = 0;
                            ITboObj.DVDND = 0;
                            ITboObj.INTRS = 0;
                            ITboObj.UNSPI = 0;
                            ITboObj.TDSAT = 0;
                            ITboObj.CREATED_BY = User.Identity.Name;
                            ITboObj.PERNR = User.Identity.Name;
                            ITboObj.STATUS = "Requested";
                            ITboObj.CREATED_ON = DateTime.Now;
                            ITboObj.BEGDA = DateTime.Now;
                            ITboObj.ENDDA = DateTime.Now;
                            ITboObj.MODIFIED_BY = "";
                            ITboObj.Flag = 1;
                            ITboObj.ID = 0;
                            ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();
                            ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSID, ref sts);

                            if (sts == true)
                            {
                                SendMailSec80C(ITOTHERSID, "Submit", "Income from Other Sources - House Property");
                                MsgCls("IT House Property created successfully !", lblMessageBoard, Color.Green);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT House Property created successfully !')", true);
                            }
                            CancelFunc();
                        }

                    }
                    else if ((RB_PropTyp.SelectedValue.ToString().Trim() == "2") || (RB_PropTyp.SelectedValue.ToString().Trim() == "3"))
                    {

                        if (((string.IsNullOrEmpty(TXT_DEDINT.Text)) && ((string.IsNullOrEmpty(TXT_DEDINT.Text) ? 0 : decimal.Parse(TXT_DEDINT.Text.ToString().Trim())) <= 0)) &&
                            ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text)) && ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) <= 0)) &&
                            ((string.IsNullOrEmpty(Lbl_DEDREPAIR.Text)) && ((string.IsNullOrEmpty(Lbl_DEDREPAIR.Text) ? 0 : decimal.Parse(Lbl_DEDREPAIR.Text.ToString().Trim())) <= 0)) &&
                            ((string.IsNullOrEmpty(TXTDECOTHERS.Text)) && ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) <= 0)) &&
                            ((string.IsNullOrEmpty(TXTTDS.Text)) && ((string.IsNullOrEmpty(TXTTDS.Text) ? 0 : decimal.Parse(TXTTDS.Text.ToString().Trim())) <= 0)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter details before submiting');", true);
                            MsgCls("Please enter details before submiting", lblMessageBoard, System.Drawing.Color.Red);
                            return;
                        }


                        ITbl ITblObj = new ITbl();
                        ITbo ITboObj = new ITbo();
                        //------Housing property
                        ITboObj.PROPTYP = "1";
                        ITboObj.RENTO = RB_PropTyp.SelectedValue.ToString().Trim();
                        ITboObj.INT24 = string.IsNullOrEmpty(TXT_DEDINT.Text) ? 0 : decimal.Parse(TXT_DEDINT.Text.ToString().Trim());
                        ITboObj.LETVL = string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim());
                        ITboObj.REP24 = string.IsNullOrEmpty(Lbl_DEDREPAIR.Text) ? 0 : decimal.Parse(Lbl_DEDREPAIR.Text.ToString().Trim());
                        ITboObj.OTH24 = string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim());
                        ITboObj.TDSOT = string.IsNullOrEmpty(TXTTDS.Text) ? 0 : decimal.Parse(TXTTDS.Text.ToString().Trim());
                        //------Others
                        ITboObj.BSPFT = 0;
                        ITboObj.CPGLN = 0;
                        ITboObj.CPGLS = 0;
                        ITboObj.CPGNS = 0;
                        ITboObj.CPGSS = 0;
                        ITboObj.DVDND = 0;
                        ITboObj.INTRS = 0;
                        ITboObj.UNSPI = 0;
                        ITboObj.TDSAT = 0;
                        ITboObj.CREATED_BY = User.Identity.Name;
                        ITboObj.PERNR = User.Identity.Name;
                        ITboObj.STATUS = "Requested";
                        ITboObj.CREATED_ON = DateTime.Now;
                        ITboObj.BEGDA = DateTime.Now;
                        ITboObj.ENDDA = DateTime.Now;
                        ITboObj.MODIFIED_BY = "";
                        ITboObj.Flag = 1;
                        ITboObj.ID = 0;
                        ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();
                        ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSID, ref sts);

                        if (sts == true)
                        {
                            SendMailSec80C(ITOTHERSID, "Submit", "Income from Other Sources - House Property");
                            MsgCls("IT House Property created successfully !", lblMessageBoard, Color.Green);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT House Property created successfully !')", true);
                        }
                        CancelFunc();
                    }
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
                {

                    if (((string.IsNullOrEmpty(TXT_BusnProfits.Text)) && ((string.IsNullOrEmpty(TXT_BusnProfits.Text) ? 0 : decimal.Parse(TXT_BusnProfits.Text.ToString().Trim())) <= 0)) &&
                           ((string.IsNullOrEmpty(TXT_LTCG.Text)) && ((string.IsNullOrEmpty(TXT_LTCG.Text) ? 0 : decimal.Parse(TXT_LTCG.Text.ToString().Trim())) <= 0)) &&
                           ((string.IsNullOrEmpty(TXT_LTCGS.Text)) && ((string.IsNullOrEmpty(TXT_LTCGS.Text) ? 0 : decimal.Parse(TXT_LTCGS.Text.ToString().Trim())) <= 0)) &&
                           ((string.IsNullOrEmpty(TXT_STCG.Text)) && ((string.IsNullOrEmpty(TXT_STCG.Text) ? 0 : decimal.Parse(TXT_STCG.Text.ToString().Trim())) <= 0)) ||
                            ((string.IsNullOrEmpty(TXT_STCGLS.Text)) && ((string.IsNullOrEmpty(TXT_STCGLS.Text) ? 0 : decimal.Parse(TXT_STCGLS.Text.ToString().Trim())) <= 0)) &&
                             ((string.IsNullOrEmpty(TXT_IFD.Text)) && ((string.IsNullOrEmpty(TXT_IFD.Text) ? 0 : decimal.Parse(TXT_IFD.Text.ToString().Trim())) <= 0)) &&
                              ((string.IsNullOrEmpty(TXT_IFI.Text)) && ((string.IsNullOrEmpty(TXT_IFI.Text) ? 0 : decimal.Parse(TXT_IFI.Text.ToString().Trim())) <= 0)) &&
                               ((string.IsNullOrEmpty(TXT_OI.Text)) && ((string.IsNullOrEmpty(TXT_OI.Text) ? 0 : decimal.Parse(TXT_OI.Text.ToString().Trim())) <= 0)) &&
                           ((string.IsNullOrEmpty(TXT_TDSI.Text)) && ((string.IsNullOrEmpty(TXT_TDSI.Text) ? 0 : decimal.Parse(TXT_TDSI.Text.ToString().Trim())) <= 0)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter details before submiting');", true);
                        MsgCls("Please enter details before submiting", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                    }


                    ITbl ITblObj = new ITbl();
                    ITbo ITboObj = new ITbo();
                    ITboObj.PROPTYP = "2";
                    ITboObj.RENTO = "";
                    ITboObj.INT24 = 0;
                    ITboObj.LETVL = 0;
                    ITboObj.REP24 = 0;
                    ITboObj.OTH24 = 0;
                    ITboObj.TDSOT = 0;
                    //-----others
                    ITboObj.BSPFT = string.IsNullOrEmpty(TXT_BusnProfits.Text) ? 0 : decimal.Parse(TXT_BusnProfits.Text.ToString().Trim());
                    ITboObj.CPGLN = string.IsNullOrEmpty(TXT_LTCG.Text) ? 0 : decimal.Parse(TXT_LTCG.Text.ToString().Trim());
                    ITboObj.CPGLS = string.IsNullOrEmpty(TXT_LTCGS.Text) ? 0 : decimal.Parse(TXT_LTCGS.Text.ToString().Trim());
                    ITboObj.CPGNS = string.IsNullOrEmpty(TXT_STCG.Text) ? 0 : decimal.Parse(TXT_STCG.Text.ToString().Trim());
                    ITboObj.CPGSS = string.IsNullOrEmpty(TXT_STCGLS.Text) ? 0 : decimal.Parse(TXT_STCGLS.Text.ToString().Trim());
                    ITboObj.DVDND = string.IsNullOrEmpty(TXT_IFD.Text) ? 0 : decimal.Parse(TXT_IFD.Text.ToString().Trim());
                    ITboObj.INTRS = string.IsNullOrEmpty(TXT_IFI.Text) ? 0 : decimal.Parse(TXT_IFI.Text.ToString().Trim());
                    ITboObj.UNSPI = string.IsNullOrEmpty(TXT_OI.Text) ? 0 : decimal.Parse(TXT_OI.Text.ToString().Trim());
                    ITboObj.TDSAT = string.IsNullOrEmpty(TXT_TDSI.Text) ? 0 : decimal.Parse(TXT_TDSI.Text.ToString().Trim());
                    ITboObj.CREATED_BY = User.Identity.Name;
                    ITboObj.PERNR = User.Identity.Name;
                    ITboObj.STATUS = "Requested";
                    ITboObj.CREATED_ON = DateTime.Now;
                    ITboObj.BEGDA = DateTime.Now;
                    ITboObj.ENDDA = DateTime.Now;
                    ITboObj.MODIFIED_BY = "";
                    ITboObj.Flag = 1;
                    ITboObj.ID = 0;
                    ITboObj.EMPCOMMENTS2 = TXTCOMMENTS2.Text.ToString().Trim();
                    ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSID, ref sts);

                    if (sts == true)
                    {
                        SendMailSec80C(ITOTHERSID, "Submit", "Income from Other Sources");
                        MsgCls("IT Other Sources created successfully !", LblMsg, Color.Green);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Other Sources created successfully !')", true);
                    }
                    CancelFunc();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }


        private void SendMailSec80C(int? ITHID, string type, string subtyp)
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
                body += "<b>Income Tax Type :  " + subtyp + "</b><br/><br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Income Tax Request submitted successfully and Mail sent successfully.";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        protected void TXT_FNLLETVALUE_TextChanged(object sender, EventArgs e)
        {
            try
            {

                GetDEDREPAIR();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void GetDEDREPAIR()
        {
            try
            {
                if ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) > 0)
                {
                    if ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) == 0)
                    {
                        Lbl_DEDREPAIR.Text = (((decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) * 30) / 100).ToString();
                    }

                    else if (((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) > 0)
                        && ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) <= (string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim()))))
                    {
                        Lbl_DEDREPAIR.Text = ((((decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) * 30) / 100) - (((decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) * 30) / 100)).ToString();
                    }

                    else if (((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) > 0)
                        && ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) > (string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim()))))
                    {
                        Lbl_DEDREPAIR.Text = "0";
                    }
                }
                else
                {
                    Lbl_DEDREPAIR.Text = "0";

                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void TXTDECOTHERS_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) > 0)
                {
                    if ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) == 0)
                    {
                        Lbl_DEDREPAIR.Text = "0";
                    }
                    else if (((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) > 0)
                        && ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim()))>=
                        ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())))))
                    {
                        Lbl_DEDREPAIR.Text = ((((decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) * 30) / 100) - (((decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) * 30) / 100)).ToString();
                    }

                    else if (((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) > 0)
                        && ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) <
                        ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())))))
                    {
                        Lbl_DEDREPAIR.Text = "0";
                    }

                }

                else if ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) == 0)
                {

                    if ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) == 0)
                    {
                        Lbl_DEDREPAIR.Text = "0";
                    }

                    else if ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) > 0)
                    {
                        Lbl_DEDREPAIR.Text = (((decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) * 30) / 100).ToString();
                    }

                }



                ////// if (decimal.Parse(TXTDECOTHERS.Text.ToString().Trim()) > 0)
                ////if ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) <= (string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())))
                ////{

                ////    if ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) > 0)
                ////    {
                ////        //TXT_FNLLETVALUE.Text = ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) - (decimal.Parse(TXTDECOTHERS.Text.ToString().Trim()))).ToString();
                ////        //GetDEDREPAIR();
                ////        Lbl_DEDREPAIR.Text = ((((decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) * 30) / 100) - (((decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) * 30) / 100)).ToString();

                ////    }
                ////}
                ////else
                ////{
                ////    TXT_FNLLETVALUE.Text = (string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())).ToString();
                ////}

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }
        private void UpDateEntry(List<ITbo> ITboObj1)
        {
            try
            {
                if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
                {

                    //ITboObj.RENTO = vRow.RENTO.ToString();
                    //ITboObj.INT24 = vRow.INT24;
                    //ITboObj.LETVL = vRow.LETVL;
                    //ITboObj.REP24 = vRow.REP24;
                    //ITboObj.OTH24 = vRow.OTH24;
                    //ITboObj.TDSOT = vRow.TDSOT;
                    ViewState["Housing"] = ITboObj1[0].ID.ToString().Trim();
                    RB_PropTyp.SelectedValue = ITboObj1[0].RENTO.ToString().Trim();
                    HousingProprtyType(ITboObj1[0].RENTO.ToString().Trim());
                    if (ITboObj1[0].RENTO.ToString().Trim() == "1")
                    {
                        TXT_DedIntr.Text = ITboObj1[0].INT24.ToString().Trim();
                    }
                    if ((ITboObj1[0].RENTO.ToString().Trim() == "2") || (ITboObj1[0].RENTO.ToString().Trim() == "3"))
                    {
                        TXT_DEDINT.Text = ITboObj1[0].INT24.ToString().Trim();
                        TXT_FNLLETVALUE.Text = ITboObj1[0].LETVL.ToString().Trim();
                        Lbl_DEDREPAIR.Text = ITboObj1[0].REP24.ToString().Trim();
                        TXTDECOTHERS.Text = ITboObj1[0].OTH24.ToString().Trim();
                        TXTTDS.Text = ITboObj1[0].TDSOT.ToString().Trim();

                    }

                    TXTCOMMENTS.Text = ITboObj1[0].EMPCOMMENTS.ToString().Trim();

                    RB_PropTyp.Enabled = false;
                    TXT_DedIntr.Enabled = false;
                    TXT_DEDINT.Enabled = false;
                    TXT_FNLLETVALUE.Enabled = false;
                    Lbl_DEDREPAIR.Enabled = false;
                    TXTDECOTHERS.Enabled = false;
                    TXTTDS.Enabled = false;


                    btnUpdate.Visible = btnUpdate.Enabled = false;
                    BtnEDIT.Visible = BtnEDIT.Enabled = true;
                    BtnCancel.Visible = BtnCancel.Enabled = true;
                    BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
                {

                    //ITboObj.BSPFT = vRow.BSPFT;
                    //ITboObj.CPGLN = vRow.CPGLN;
                    //ITboObj.CPGLS = vRow.CPGLS;
                    //ITboObj.CPGNS = vRow.CPGNS;
                    //ITboObj.CPGSS = vRow.CPGSS;
                    //ITboObj.DVDND = vRow.DVDND;
                    //ITboObj.INTRS = vRow.INTRS;
                    //ITboObj.UNSPI = vRow.UNSPI;
                    //ITboObj.TDSAT = vRow.TDSAT;
                    ViewState["Others"] = ITboObj1[0].ID.ToString().Trim();
                    TXT_BusnProfits.Text = ITboObj1[0].BSPFT.ToString().Trim();
                    TXT_LTCG.Text = ITboObj1[0].CPGLN.ToString().Trim();
                    TXT_LTCGS.Text = ITboObj1[0].CPGLS.ToString().Trim();
                    TXT_STCG.Text = ITboObj1[0].CPGNS.ToString().Trim();
                    TXT_STCGLS.Text = ITboObj1[0].CPGSS.ToString().Trim();
                    TXT_IFD.Text = ITboObj1[0].DVDND.ToString().Trim();
                    TXT_IFI.Text = ITboObj1[0].INTRS.ToString().Trim();
                    TXT_OI.Text = ITboObj1[0].UNSPI.ToString().Trim();
                    TXT_TDSI.Text = ITboObj1[0].TDSAT.ToString().Trim();
                    TXTCOMMENTS2.Text = ITboObj1[0].EMPCOMMENTS2.ToString().Trim();

                    TXT_BusnProfits.Enabled = false;
                    TXT_LTCG.Enabled = false;
                    TXT_LTCGS.Enabled = false;
                    TXT_STCG.Enabled = false;
                    TXT_STCGLS.Enabled = false;
                    TXT_IFD.Enabled = false;
                    TXT_IFI.Enabled = false;
                    TXT_OI.Enabled = false;
                    TXT_TDSI.Enabled = false;


                    btnUpdate.Visible = btnUpdate.Enabled = false;
                    BtnEDIT.Visible = BtnEDIT.Enabled = true;
                    BtnCancel.Visible = BtnCancel.Enabled = true;
                    BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void NewEntry()
        {
            try
            {
                if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
                {
                    RB_PropTyp.SelectedValue = "1";
                    ClearControls();
                    btnUpdate.Visible = btnUpdate.Enabled = false;
                    BtnEDIT.Visible = BtnEDIT.Enabled = false;
                    BtnCancel.Visible = BtnCancel.Enabled = true;
                    BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = true;
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
                {
                    ClearControlsView2();
                    btnUpdate.Visible = btnUpdate.Enabled = false;
                    BtnEDIT.Visible = BtnEDIT.Enabled = false;
                    BtnCancel.Visible = BtnCancel.Enabled = true;
                    BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = true;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }



        protected void BtnEDIT_Click(object sender, EventArgs e)
        {
            if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
            {

                RB_PropTyp.Visible = RB_PropTyp.Enabled = true;
                TXT_DedIntr.Visible = TXT_DedIntr.Enabled = true;
                TXT_DEDINT.Visible = TXT_DEDINT.Enabled = true;
                TXT_FNLLETVALUE.Visible = TXT_FNLLETVALUE.Enabled = true;
                Lbl_DEDREPAIR.Visible = Lbl_DEDREPAIR.Enabled = true;
                TXTDECOTHERS.Visible = TXTDECOTHERS.Enabled = true;
                TXTTDS.Visible = TXTTDS.Enabled = true;

                btnUpdate.Visible = btnUpdate.Enabled = true;
                BtnEDIT.Visible = BtnEDIT.Enabled = false;
                BtnCancel.Visible = BtnCancel.Enabled = true;
                BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                Div_Proptyp.Visible = false;
            }
            else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
            {


                TXT_BusnProfits.Visible = TXT_BusnProfits.Enabled = true;
                TXT_LTCG.Visible = TXT_LTCG.Enabled = true;
                TXT_LTCGS.Visible = TXT_LTCGS.Enabled = true;
                TXT_STCG.Visible = TXT_STCG.Enabled = true;
                TXT_STCGLS.Visible = TXT_STCGLS.Enabled = true;
                TXT_IFD.Visible = TXT_IFD.Enabled = true;
                TXT_IFI.Visible = TXT_IFI.Enabled = true;
                TXT_OI.Visible = TXT_OI.Enabled = true;
                TXT_TDSI.Visible = TXT_TDSI.Enabled = true;


                btnUpdate.Visible = btnUpdate.Enabled = true;
                BtnEDIT.Visible = BtnEDIT.Enabled = false;
                BtnCancel.Visible = BtnCancel.Enabled = true;
                BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                Div_Proptyp.Visible = false;
            }

        }

        protected void btnUpdateITHousingOthers_Click(object sender, EventArgs e)
        {
            try
            {
                bool? stus = false;
                int? ITOTHERSIDU = 0;

                if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
                {
                    if (RB_PropTyp.SelectedValue.ToString().Trim() == "1")
                    {
                        decimal amt = string.IsNullOrEmpty(TXT_DedIntr.Text) ? 0 : decimal.Parse(TXT_DedIntr.Text.ToString().Trim());
                        // decimal amt = decimal.Parse(TXT_DedIntr.Text.ToString().Trim());
                        //if (amt > 200000)
                        //{
                        //    lblMessageBoard.Text = "Deduce Interest u/s 24 should be less than or equal to 2 lakhs";
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Deduce Interest u/s 24 should be less than or equal to 2 lakhs');", true);
                        //    return;
                        //}

                        if ((amt > 200000) || (amt <= 0))
                        {
                            lblMessageBoard.Text = "Deduce Interest u/s 24 should be greater than 0 and less than or equal to 2 lakhs";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Deduce Interest u/s 24 should be greater than 0 and less than or equal to 2 lakhs');", true);
                            return;
                        }
                        else
                        {
                            ITbl ITblObj = new ITbl();
                            ITbo ITboObj = new ITbo();
                            //------Housing property
                            ITboObj.PROPTYP = "1";
                            ITboObj.RENTO = RB_PropTyp.SelectedValue.ToString().Trim();
                            ITboObj.INT24 = string.IsNullOrEmpty(TXT_DedIntr.Text) ? 0 : decimal.Parse(TXT_DedIntr.Text.ToString().Trim());
                            ITboObj.LETVL = 0;
                            ITboObj.REP24 = 0;
                            ITboObj.OTH24 = 0;
                            ITboObj.TDSOT = 0;
                            //------Others
                            ITboObj.BSPFT = 0;
                            ITboObj.CPGLN = 0;
                            ITboObj.CPGLS = 0;
                            ITboObj.CPGNS = 0;
                            ITboObj.CPGSS = 0;
                            ITboObj.DVDND = 0;
                            ITboObj.INTRS = 0;
                            ITboObj.UNSPI = 0;
                            ITboObj.TDSAT = 0;
                            ITboObj.CREATED_BY = User.Identity.Name;
                            ITboObj.PERNR = User.Identity.Name;
                            ITboObj.STATUS = "Updated";
                            ITboObj.CREATED_ON = DateTime.Now;
                            ITboObj.BEGDA = DateTime.Now;
                            ITboObj.ENDDA = DateTime.Now;
                            ITboObj.MODIFIED_BY = User.Identity.Name;
                            ITboObj.Flag = 2;
                            ITboObj.ID = int.Parse(ViewState["Housing"].ToString().Trim());
                            ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();
                            ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSIDU, ref stus);

                            if (stus == true)
                            {
                                SendMailSec80C(ITOTHERSIDU, "Updated", "Income from Other Sources - House Property");
                                MsgCls("IT House Property updated successfully !", lblMessageBoard, Color.Green);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT House Property updated successfully !')", true);
                            }
                            CancelFunc();
                        }

                    }
                    else if ((RB_PropTyp.SelectedValue.ToString().Trim() == "2") || (RB_PropTyp.SelectedValue.ToString().Trim() == "3"))
                    {

                        if (((string.IsNullOrEmpty(TXT_DEDINT.Text)) && ((string.IsNullOrEmpty(TXT_DEDINT.Text) ? 0 : decimal.Parse(TXT_DEDINT.Text.ToString().Trim())) <= 0)) ||
                            ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text)) && ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) <= 0)) ||
                            ((string.IsNullOrEmpty(Lbl_DEDREPAIR.Text)) && ((string.IsNullOrEmpty(Lbl_DEDREPAIR.Text) ? 0 : decimal.Parse(Lbl_DEDREPAIR.Text.ToString().Trim())) <= 0)) ||
                            ((string.IsNullOrEmpty(TXTDECOTHERS.Text)) && ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) <= 0)) ||
                            ((string.IsNullOrEmpty(TXTTDS.Text)) && ((string.IsNullOrEmpty(TXTTDS.Text) ? 0 : decimal.Parse(TXTTDS.Text.ToString().Trim())) <= 0)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter details before submiting');", true);
                            MsgCls("Please enter details before submiting", lblMessageBoard, System.Drawing.Color.Red);
                            return;
                        }



                        ITbl ITblObj = new ITbl();
                        ITbo ITboObj = new ITbo();
                        //------Housing property
                        ITboObj.PROPTYP = "1";
                        ITboObj.RENTO = RB_PropTyp.SelectedValue.ToString().Trim();
                        ITboObj.INT24 = string.IsNullOrEmpty(TXT_DEDINT.Text) ? 0 : decimal.Parse(TXT_DEDINT.Text.ToString().Trim());
                        ITboObj.LETVL = string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim());
                        ITboObj.REP24 = string.IsNullOrEmpty(Lbl_DEDREPAIR.Text) ? 0 : decimal.Parse(Lbl_DEDREPAIR.Text.ToString().Trim());
                        ITboObj.OTH24 = string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim());
                        ITboObj.TDSOT = string.IsNullOrEmpty(TXTTDS.Text) ? 0 : decimal.Parse(TXTTDS.Text.ToString().Trim());
                        //------Others
                        ITboObj.BSPFT = 0;
                        ITboObj.CPGLN = 0;
                        ITboObj.CPGLS = 0;
                        ITboObj.CPGNS = 0;
                        ITboObj.CPGSS = 0;
                        ITboObj.DVDND = 0;
                        ITboObj.INTRS = 0;
                        ITboObj.UNSPI = 0;
                        ITboObj.TDSAT = 0;
                        ITboObj.CREATED_BY = User.Identity.Name;
                        ITboObj.PERNR = User.Identity.Name;
                        ITboObj.STATUS = "Updated";
                        ITboObj.CREATED_ON = DateTime.Now;
                        ITboObj.BEGDA = DateTime.Now;
                        ITboObj.ENDDA = DateTime.Now;
                        ITboObj.MODIFIED_BY = User.Identity.Name;
                        ITboObj.Flag = 2;
                        ITboObj.ID = int.Parse(ViewState["Housing"].ToString().Trim());
                        ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();
                        ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSIDU, ref stus);
                        if (stus == true)
                        {
                            SendMailSec80C(ITOTHERSIDU, "Updated", "Income from Other Sources - House Property");
                            MsgCls("IT House Property updated successfully !", lblMessageBoard, Color.Green);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT House Property updated successfully !')", true);
                        }
                        CancelFunc();
                    }
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
                {



                    if (((string.IsNullOrEmpty(TXT_BusnProfits.Text)) && ((string.IsNullOrEmpty(TXT_BusnProfits.Text) ? 0 : decimal.Parse(TXT_BusnProfits.Text.ToString().Trim())) <= 0)) ||
                           ((string.IsNullOrEmpty(TXT_LTCG.Text)) && ((string.IsNullOrEmpty(TXT_LTCG.Text) ? 0 : decimal.Parse(TXT_LTCG.Text.ToString().Trim())) <= 0)) ||
                           ((string.IsNullOrEmpty(TXT_LTCGS.Text)) && ((string.IsNullOrEmpty(TXT_LTCGS.Text) ? 0 : decimal.Parse(TXT_LTCGS.Text.ToString().Trim())) <= 0)) ||
                           ((string.IsNullOrEmpty(TXT_STCG.Text)) && ((string.IsNullOrEmpty(TXT_STCG.Text) ? 0 : decimal.Parse(TXT_STCG.Text.ToString().Trim())) <= 0)) ||
                            ((string.IsNullOrEmpty(TXT_STCGLS.Text)) && ((string.IsNullOrEmpty(TXT_STCGLS.Text) ? 0 : decimal.Parse(TXT_STCGLS.Text.ToString().Trim())) <= 0)) ||
                             ((string.IsNullOrEmpty(TXT_IFD.Text)) && ((string.IsNullOrEmpty(TXT_IFD.Text) ? 0 : decimal.Parse(TXT_IFD.Text.ToString().Trim())) <= 0)) ||
                              ((string.IsNullOrEmpty(TXT_IFI.Text)) && ((string.IsNullOrEmpty(TXT_IFI.Text) ? 0 : decimal.Parse(TXT_IFI.Text.ToString().Trim())) <= 0)) ||
                               ((string.IsNullOrEmpty(TXT_OI.Text)) && ((string.IsNullOrEmpty(TXT_OI.Text) ? 0 : decimal.Parse(TXT_OI.Text.ToString().Trim())) <= 0)) ||
                           ((string.IsNullOrEmpty(TXT_TDSI.Text)) && ((string.IsNullOrEmpty(TXT_TDSI.Text) ? 0 : decimal.Parse(TXT_TDSI.Text.ToString().Trim())) <= 0)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter details before submiting');", true);
                        MsgCls("Please enter details before submiting", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                    }


                    ITbl ITblObj = new ITbl();
                    ITbo ITboObj = new ITbo();
                    ITboObj.PROPTYP = "2";
                    ITboObj.RENTO = "";
                    ITboObj.INT24 = 0;
                    ITboObj.LETVL = 0;
                    ITboObj.REP24 = 0;
                    ITboObj.OTH24 = 0;
                    ITboObj.TDSOT = 0;
                    //-----others
                    ITboObj.BSPFT = string.IsNullOrEmpty(TXT_BusnProfits.Text) ? 0 : decimal.Parse(TXT_BusnProfits.Text.ToString().Trim());
                    ITboObj.CPGLN = string.IsNullOrEmpty(TXT_LTCG.Text) ? 0 : decimal.Parse(TXT_LTCG.Text.ToString().Trim());
                    ITboObj.CPGLS = string.IsNullOrEmpty(TXT_LTCGS.Text) ? 0 : decimal.Parse(TXT_LTCGS.Text.ToString().Trim());
                    ITboObj.CPGNS = string.IsNullOrEmpty(TXT_STCG.Text) ? 0 : decimal.Parse(TXT_STCG.Text.ToString().Trim());
                    ITboObj.CPGSS = string.IsNullOrEmpty(TXT_STCGLS.Text) ? 0 : decimal.Parse(TXT_STCGLS.Text.ToString().Trim());
                    ITboObj.DVDND = string.IsNullOrEmpty(TXT_IFD.Text) ? 0 : decimal.Parse(TXT_IFD.Text.ToString().Trim());
                    ITboObj.INTRS = string.IsNullOrEmpty(TXT_IFI.Text) ? 0 : decimal.Parse(TXT_IFI.Text.ToString().Trim());
                    ITboObj.UNSPI = string.IsNullOrEmpty(TXT_OI.Text) ? 0 : decimal.Parse(TXT_OI.Text.ToString().Trim());
                    ITboObj.TDSAT = string.IsNullOrEmpty(TXT_TDSI.Text) ? 0 : decimal.Parse(TXT_TDSI.Text.ToString().Trim());
                    ITboObj.CREATED_BY = User.Identity.Name;
                    ITboObj.PERNR = User.Identity.Name;
                    ITboObj.STATUS = "Updated";
                    ITboObj.CREATED_ON = DateTime.Now;
                    ITboObj.BEGDA = DateTime.Now;
                    ITboObj.ENDDA = DateTime.Now;
                    ITboObj.MODIFIED_BY = User.Identity.Name;
                    ITboObj.Flag = 2;
                    ITboObj.ID = int.Parse(ViewState["Others"].ToString().Trim());
                    ITboObj.EMPCOMMENTS2 = TXTCOMMENTS2.Text.ToString().Trim();
                    ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSIDU, ref stus);
                    if (stus == true)
                    {
                        SendMailSec80C(ITOTHERSIDU, "Updated", "Income from Other Sources");
                        MsgCls("IT Other Sources updated successfully !", LblMsg, Color.Green);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Other Sources updated successfully !')", true);
                    }
                    CancelFunc();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                //GetFinancialDates();
                //ClearControls();
                //ClearControlsView2();
                //DIVCOMMENTS.Visible = false;
                //BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                //BtnEDIT.Visible = BtnEDIT.Enabled = false;
                //btnUpdate.Visible = btnUpdate.Enabled = false;
                //BtnCancel.Visible = BtnCancel.Enabled = false;
                //MV_IncomeSources.Visible = false;
                //Div_Proptyp.Visible = true;
                //DDl_TYPE.SelectedValue = "0";
                CancelFunc();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        public void CancelFunc()
        {
            try
            {
                //Page.ClientScript.RegisterClientScriptBlock(

                GetFinancialDates();
                ClearControls();
                ClearControlsView2();
                DIVCOMMENTS.Visible = false;
                BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                BtnEDIT.Visible = BtnEDIT.Enabled = false;
                btnUpdate.Visible = btnUpdate.Enabled = false;
                BtnCancel.Visible = BtnCancel.Enabled = false;
                MV_IncomeSources.Visible = false;
                Div_Proptyp.Visible = true;
                DDl_TYPE.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


    }
}
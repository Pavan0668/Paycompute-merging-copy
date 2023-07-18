using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Resources;
using System.Security.Permissions;
using System.Data.Linq;
using System.IO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Configuration;

public partial class UI_Configuration_Configuration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("Superuser"))
        {
            Response.Redirect("~/UnauthorizedAccess.aspx");
        }
        lblGridMessage.Text = "";
        lblMessageBoard.Text = "";
        lblSeverMessage.Text = "";
        lblLogoMessage.Text = "";
        lblPhotoMessage.Text = "";
        if (!IsPostBack)
        {
            LoadGridDetails();
            LoadMailServerDetails();
            tabConfiguration.ActiveTab = tabAddressInformation;
            LoadAddressInformationDetails();
            LoadImageLogo();
            LoadEmployeePhotoPath();
            LoadLeavesEncashable();
        }
        chkPDMandatoryCOB.Attributes.Add("OnClick", "PersonalDataClientStateChanged();");
        txtEmployePhotoPath.Attributes.Add("onkeyup", "ClientEmployePhotoPathChanged()");
        this.Page.Form.DefaultButton = Button4.UniqueID;
    }
    #region Gridcontrls
    protected void LoadGridDetails()
    {
        lblMessageBoard.Text = "";
        lblSeverMessage.Text = "";
        lblLogoMessage.Text = "";
        lblPhotoMessage.Text = "";
        configurationbl objBl = new configurationbl();
        configurationcollectionbo objLst = new configurationcollectionbo();
        objLst = objBl.Get_Details();
        grdReqSupApproval.DataSource = objLst;
        grdReqSupApproval.DataBind();
        Session.Add("objLst", objLst);
        int iCount = objLst.Count;
        var iActive = from col in objLst
                      where col.REQUIRES_STATUS == true
                      select col;
        int iRequicount = iActive.Count();
        if (iCount == iRequicount)
        {
            btnStatus.Text = "Uncheck all";
        }
        else
        {
            btnStatus.Text = "Check all";
        }

    }
    protected void btnStatus_Click(object sender, EventArgs e)
    {
        CheckBox chkSupervisor;
        CheckBox chkHr;
        CheckBox chkPayroll;

        if (btnStatus.Text == "Check all")
        {
            foreach (GridViewRow rowItem in grdReqSupApproval.Rows)
            {

                chkSupervisor = (CheckBox)(rowItem.FindControl("chkBxRequiresStatus"));
                chkHr = (CheckBox)(rowItem.FindControl("chkBxHRStatus"));
                chkPayroll = (CheckBox)(rowItem.FindControl("chkBxFinAdminStatus"));

                chkSupervisor.Checked = true;
                chkHr.Checked = true;
                chkPayroll.Checked = true;

            }
            btnStatus.Text = "Uncheck all";
        }
        else
        {
            foreach (GridViewRow rowItem in grdReqSupApproval.Rows)
            {
                chkSupervisor = (CheckBox)(rowItem.FindControl("chkBxRequiresStatus"));
                chkHr = (CheckBox)(rowItem.FindControl("chkBxHRStatus"));
                chkPayroll = (CheckBox)(rowItem.FindControl("chkBxFinAdminStatus"));

                chkSupervisor.Checked = false;
                chkHr.Checked = false;
                chkPayroll.Checked = false;
            }
            btnStatus.Text = "Check all";
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        LoadGridDetails();
       // btnStatus.Text = "Check all";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        configurationbl objBl = new configurationbl();
        configurationbo objBo = new configurationbo();

        string sRequiresIds = string.Empty;
        string sRequiresStatus = string.Empty;
        string sHRStatus = string.Empty;
        string sPayrollStatus = string.Empty;

        foreach (GridViewRow row in grdReqSupApproval.Rows)
        {
            CheckBox myCheckBox = (CheckBox)row.FindControl("chkBxRequiresStatus");
            CheckBox chkHRStatus = (CheckBox)row.FindControl("chkBxHRStatus");
            CheckBox chkPayrollStatus = (CheckBox)row.FindControl("chkBxFinAdminStatus");
            Label hidden = (Label)row.FindControl("hdnRequireId");
           
            sRequiresStatus = sRequiresStatus + "|" + myCheckBox.Checked;
            sHRStatus = sHRStatus + "|" + chkHRStatus.Checked;
            sRequiresIds = sRequiresIds + "|" + hidden.Text;
            sPayrollStatus = sPayrollStatus + "|" + chkPayrollStatus.Checked;
        }
      
        objBo.DESCRIPTION = sRequiresStatus;
        objBo.HR_DESCRIPTION = sHRStatus;
        objBo.ALL_REQURIES_IDS = sRequiresIds;
        objBo.MODIFIEDBY = User.Identity.Name.Trim();
        objBo.MODIFIEDON = DateTime.Now;
        objBo.FIN_ADMIN_STATUS_DESC = sPayrollStatus;
        try
        {
            int iResultCode = objBl.Update(objBo);
            if (iResultCode == 0)
            {
                lblGridMessage.Visible = true;
                lblGridMessage.ForeColor = System.Drawing.Color.Green;
                lblGridMessage.Text = GetLocalResourceObject("SaveSuccess").ToString();

            }


        }
        catch
        {
            lblGridMessage.Visible = true;
            lblGridMessage.ForeColor = System.Drawing.Color.Red;
            lblGridMessage.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
        LoadGridDetails();
    }

    #endregion
    #region Mail server details
    protected void LoadMailServerDetails()
    {
        if (Request.Form["txtIMAPServer"] != null)
        {

            String selectedLanguage = Request.Form["txtIMAPServer"];
            UICulture = selectedLanguage;
            Culture = selectedLanguage;
        } 
        //Thread.CurrentThread.CurrentCulture =
        //    CultureInfo.CreateSpecificCulture(selectedLanguage);
        //Thread.CurrentThread.CurrentUICulture = new
        //    CultureInfo(selectedLanguage);

        lblGridMessage.Text = "";
        lblMessageBoard.Text = "";
        lblLogoMessage.Text = "";
        lblPhotoMessage.Text = "";
        configurationbl objBl = new configurationbl();
        configurationcollectionbo objLst = objBl.Load_Mail_Server_Details();
        if (objLst.Count == 0)
        {
            txtIMAPServer.Text = "";
            txtPOPServer.Text = "";
            txtSMTPServer.Text = "";
            txtPort.Text = "";
            txtEmailId.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";

        }
        else
        {
            foreach (configurationbo obj in objLst)
            {
                txtIMAPServer.Text = obj.IMAP_SERVER;
                txtPOPServer.Text = obj.POP_SERVER;
                txtSMTPServer.Text = obj.SMTP_SERVER;
                txtEmailId.Text = obj.EMAIL_ID;
                txtPassword.Text = obj.PASSWORD;
                txtUserName.Text = obj.USER_NAME;
                txtPort.Text = obj.PORT;
            }
        }

    }
    protected void btnMailServerDetails_Click(object sender, EventArgs e)
    {
        configurationbl objBl = new configurationbl();
        configurationbo objBo = new configurationbo();
        objBo.POP_SERVER = txtPOPServer.Text.Trim();
        objBo.IMAP_SERVER = txtIMAPServer.Text.Trim();
        objBo.SMTP_SERVER = txtSMTPServer.Text.Trim();
        objBo.EMAIL_ID = txtEmailId.Text.Trim();
        objBo.USER_NAME = txtUserName.Text.Trim();
        objBo.PASSWORD = txtPassword.Text.Trim();
        objBo.MODIFIEDBY = User.Identity.Name.Trim();
        objBo.MODIFIEDON = DateTime.Now;
        objBo.PORT = txtPort.Text.Trim();
        try
        {
            int iResultCode = objBl.Create_MailServer_Details(objBo);
            if (iResultCode == 0)
            {
               
                lblSeverMessage.Visible = true;
                lblSeverMessage.ForeColor = System.Drawing.Color.Green;
                lblSeverMessage.Text = GetLocalResourceObject("SaveSuccess").ToString();
            }
        }
        catch
        {
            lblSeverMessage.Visible = true;
            lblSeverMessage.ForeColor = System.Drawing.Color.Red;
            lblSeverMessage.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
        LoadMailServerDetails();
    }
    #endregion
    protected void tabConfiguration_ActiveTabChanged(object sender, EventArgs e)
    {
        lblGridMessage.Text = "";
        lblSeverMessage.Text = "";
        lblLogoMessage.Text = "";
        lblPhotoMessage.Text = "";
        if (tabConfiguration.ActiveTab == tabAddressInformation)
        {
            LoadAddressInformationDetails();
            SetFocus(tabAddressInformation);
        }
        if (tabConfiguration.ActiveTab == tabBankInformation)
        {
            LoadBankInformationDetails();
            SetFocus(tabBankInformation);
        }
        if (tabConfiguration.ActiveTab == tabFamilyMemberInformation)
        {
            LoadFamilyMemberDetails();
            SetFocus(tabFamilyMemberInformation);
        }
        if (tabConfiguration.ActiveTab == tabPersonalData)
        {
            LoadPersonalDataDetails();
            SetFocus(tabPersonalData);
        }
    }
    #region Address information
    protected void LoadAddressInformationDetails()
    {
        
        configurationbl objBl = new configurationbl();
        configurationcollectionbo objLst = new configurationcollectionbo();
        objLst = objBl.Get_Address_Information();

        addressinformationcolumnsbo objAddresInfoBo = new addressinformationcolumnsbo();
        foreach (configurationbo obj in objLst)
        {
            if (obj.DESCRIPTION == objAddresInfoBo.SUBTY)
            {
                chkMandatorySubtype.Checked = false;
                chkMandatorySubtype.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkMandatorySubtype.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkMandatorySubtype.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkMandatorySubtype.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }

            } if (obj.DESCRIPTION == objAddresInfoBo.BEGDA)
            {
                chkMandatoryValidFromDate.Checked = false;
                chkMandatoryValidFromDate.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkMandatoryValidFromDate.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkMandatoryValidFromDate.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkMandatorySubtype.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objAddresInfoBo.ENDDA)
            {
                chkMandatoryValidToDate.Checked = false;
                chkMandatoryValidToDate.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkMandatoryValidToDate.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkMandatoryValidToDate.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkMandatoryValidToDate.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objAddresInfoBo.STRAS)
            {
                chkMandatoryAddressLine1.Checked = false;
                chkMandatoryAddressLine1.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkMandatoryAddressLine1.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkMandatoryAddressLine1.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkMandatoryAddressLine1.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objAddresInfoBo.LOCAT)
            {
                chkMandatoryAddressLine2.Checked = false;
                chkMandatoryAddressLine2.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkMandatoryAddressLine2.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkMandatoryAddressLine2.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkMandatoryAddressLine2.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objAddresInfoBo.STATE)
            {
                chkMandatoryRegion.Checked = false;
                chkMandatoryRegion.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkMandatoryRegion.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkMandatoryRegion.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkMandatoryRegion.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objAddresInfoBo.ORT01)
            {
                chkMandatoryCity.Checked = false;
                chkMandatoryCity.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkMandatoryCity.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkMandatoryCity.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkMandatoryCity.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objAddresInfoBo.PSTLZ)
            {
                chkMandatoryPostalCode.Checked = false;
                chkMandatoryPostalCode.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkMandatoryPostalCode.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkMandatoryPostalCode.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkMandatoryPostalCode.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            }
            if (obj.DESCRIPTION == objAddresInfoBo.TELNR)
            {
                chkMandatoryPhone.Checked = false;
                chkMandatoryPhone.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkMandatoryPhone.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkMandatoryPhone.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkMandatoryPhone.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            }
        }
    }
    protected void btnAddressInformation_Click(object sender, EventArgs e)
    {
        configurationbl objBl = new configurationbl();
        configurationbo objBo = new configurationbo();
        addressinformationcolumnsbo objAddresInfoBo = new addressinformationcolumnsbo();
        objBo.DESCRIPTION =  objAddresInfoBo.SUBTY + '|' + objAddresInfoBo.BEGDA
                            + '|' + objAddresInfoBo.ENDDA + '|' + objAddresInfoBo.STRAS + '|' + objAddresInfoBo.LOCAT + '|' +
                            objAddresInfoBo.STATE + '|' + objAddresInfoBo.ORT01 + '|' + objAddresInfoBo.PSTLZ + '|' + objAddresInfoBo.TELNR;

        objBo.MANDATORY_VALUE = chkMandatorySubtype.Checked + "|" + chkMandatoryValidFromDate.Checked + '|' +
                                chkMandatoryValidToDate.Checked + '|' + chkMandatoryAddressLine1.Checked + '|' + chkMandatoryAddressLine2.Checked + '|' +
                                chkMandatoryRegion.Checked + '|' + chkMandatoryCity.Checked + '|' + chkMandatoryPostalCode.Checked + '|' + chkMandatoryPhone.Checked;
        objBo.MODIFIEDBY = User.Identity.Name.Trim();
        objBo.MODIFIEDON = DateTime.Now;
        try
        {
            int iResultCode = objBl.Update_Address_Informations(objBo);
            if (iResultCode == 0)
            {
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = GetLocalResourceObject("SaveSuccess").ToString();
            }
        }
        catch
        {
            lblMessageBoard.Visible = true;
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
    }
    #endregion
    #region Bank Information
    protected void LoadBankInformationDetails()
    {
        configurationbl objBl = new configurationbl();
        configurationcollectionbo objLst = new configurationcollectionbo();
        objLst = objBl.Get_Bank_Information();

        bankinformationcolumnsbo objBankInfoBo = new bankinformationcolumnsbo();
        foreach (configurationbo obj in objLst)
        {
            if (obj.DESCRIPTION == objBankInfoBo.SUBTY)
            {

                chkBankMandatorySubtype.Checked = false;
                chkBankMandatorySubtype.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkBankMandatorySubtype.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkBankMandatorySubtype.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkBankMandatorySubtype.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objBankInfoBo.EMFTX)
            {
                chkBankMandatoryRecipientText.Checked = false;
                chkBankMandatoryRecipientText.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkBankMandatoryRecipientText.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkBankMandatoryRecipientText.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkBankMandatoryRecipientText.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objBankInfoBo.BKPLZ)
            {
                chkBankMandatoryPostalCode.Checked = false;
                chkBankMandatoryPostalCode.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkBankMandatoryPostalCode.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkBankMandatoryPostalCode.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkBankMandatoryPostalCode.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objBankInfoBo.BKORT)
            {
                chkBankMandatoryCity.Checked = false;
                chkBankMandatoryCity.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkBankMandatoryCity.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkBankMandatoryCity.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkBankMandatoryCity.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objBankInfoBo.BANKS)
            {
                chkBankMandatoryBankCountryKey.Checked = false;
                chkBankMandatoryBankCountryKey.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkBankMandatoryBankCountryKey.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkBankMandatoryBankCountryKey.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkBankMandatoryBankCountryKey.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objBankInfoBo.BANKL)
            {
                chkBankMandatoryBankKey.Checked = false;
                chkBankMandatoryBankKey.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkBankMandatoryBankKey.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkBankMandatoryBankKey.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkBankMandatoryBankKey.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objBankInfoBo.BANKN)
            {
                chkBankMandatoryBankAccountNo.Checked = false;
                chkBankMandatoryBankAccountNo.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkBankMandatoryBankAccountNo.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkBankMandatoryBankAccountNo.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkBankMandatoryBankAccountNo.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
                // Always set default and mandatory value for acc no as true as it is always a mandatory field
                // and should not be allowed to be modified by the user.
                //chkBankDefaultBankAccountNo.Checked = true; //bool.Parse(obj.DEFAULT_VALUE);
                //chkBankMandatoryBankAccountNo.Checked = true; //bool.Parse(obj.MANDATORY_VALUE);

            } if (obj.DESCRIPTION == objBankInfoBo.ZLSCH)
            {
                chkBankMandatoryPaymentMethod.Checked = false;
                chkBankMandatoryPaymentMethod.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkBankMandatoryPaymentMethod.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkBankMandatoryPaymentMethod.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkBankMandatoryPaymentMethod.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            }
            if (obj.DESCRIPTION == objBankInfoBo.ZWECK)
            {
                chkBankMandatoryBankTransfers.Checked = false;
                chkBankMandatoryBankTransfers.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkBankMandatoryBankTransfers.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkBankMandatoryBankTransfers.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkBankMandatoryBankTransfers.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            }
            if (obj.DESCRIPTION == objBankInfoBo.WAERS)
            {
                chkBankMandatoryCurrencyKey.Checked = false;
                chkBankMandatoryCurrencyKey.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkBankMandatoryCurrencyKey.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkBankMandatoryCurrencyKey.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkBankMandatoryCurrencyKey.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            }
        }
    }
    protected void btnBankInformation_Click(object sender, EventArgs e)
    {
        configurationbl objBl = new configurationbl();
        configurationbo objBo = new configurationbo();
        bankinformationcolumnsbo objBankInfoBo = new bankinformationcolumnsbo();
        // Always set default and mandatory value for acc no as true as it is always a mandatory field
        // and should not be allowed to be modified by the user.
        // replace objBankInfoBo.BANKN by 'true'

        objBo.DESCRIPTION = objBankInfoBo.SUBTY + '|' + objBankInfoBo.EMFTX
                            + '|' + objBankInfoBo.BKPLZ + '|' + objBankInfoBo.BKORT + '|' + objBankInfoBo.BANKS + '|' +
                            objBankInfoBo.BANKL + '|' + objBankInfoBo.BANKN + '|' + objBankInfoBo.ZLSCH +
                            '|' + objBankInfoBo.ZWECK + '|' + objBankInfoBo.WAERS;

        objBo.MANDATORY_VALUE =  chkBankMandatorySubtype.Checked + "|" + chkBankMandatoryRecipientText.Checked + '|' +
                                chkBankMandatoryPostalCode.Checked + '|' + chkBankMandatoryCity.Checked + '|' + chkBankMandatoryBankCountryKey.Checked + '|' +
                                chkBankMandatoryBankKey.Checked + '|' + chkBankMandatoryBankAccountNo.Checked + '|' + chkBankMandatoryPaymentMethod.Checked + '|' +
                                chkBankMandatoryBankTransfers.Checked + '|' + chkBankMandatoryCurrencyKey.Checked;
        objBo.MODIFIEDBY = User.Identity.Name.Trim();
        objBo.MODIFIEDON = DateTime.Now;
        try
        {
            int iResultCode = objBl.Update_Bank_Informations(objBo);
            if (iResultCode == 0)
            {
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = GetLocalResourceObject("SaveSuccess").ToString();
            }
        }
        catch
        {
            lblMessageBoard.Visible = true;
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
    }
    #endregion
    #region Family member information
    protected void LoadFamilyMemberDetails()
    {
        configurationbl objBl = new configurationbl();
        configurationcollectionbo objLst = new configurationcollectionbo();
        objLst = objBl.Get_Family_Member_Information();
        familymemberinformationcolumnsbo objFMIColumnNameBo = new familymemberinformationcolumnsbo();
        foreach (configurationbo obj in objLst)
        {
            if (obj.DESCRIPTION == objFMIColumnNameBo.FAMSA)
            {
                chkFMIMandatorySubtype.Checked = false;
                chkFMIMandatorySubtype.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatorySubtype.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatorySubtype.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatorySubtype.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FAVOR)
            {
                chkFMIMandatoryFirstName.Checked = false;
                chkFMIMandatoryFirstName.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryFirstName.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryFirstName.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryFirstName.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FANAM)
            {
                chkFMIFMIMandatoryLastName.Checked = false;
                chkFMIFMIMandatoryLastName.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIFMIMandatoryLastName.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIFMIMandatoryLastName.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIFMIMandatoryLastName.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FGBNA)
            {
                chkFMIMandatoryNOB.Checked = false;
                chkFMIMandatoryNOB.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryNOB.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryNOB.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryNOB.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FINIT)
            {
                chkFMIMandatoryInitials.Checked = false;
                chkFMIMandatoryInitials.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryInitials.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryInitials.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryInitials.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FNMZU)
            {
                chkFMIMandatoryOtherTitle.Checked = false;
                chkFMIMandatoryOtherTitle.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryOtherTitle.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryOtherTitle.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryOtherTitle.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FVRSW)
            {
                chkFMIMandatoryNamePrefix.Checked = false;
                chkFMIMandatoryNamePrefix.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryNamePrefix.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryNamePrefix.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryNamePrefix.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FASEX)
            {
                chkFMIMandatoryGender.Checked = false;
                chkFMIMandatoryGender.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryGender.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryGender.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryGender.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FGBDT)
            {
                chkFMIMandatoryDOB.Checked = false;
                chkFMIMandatoryDOB.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryDOB.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryDOB.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryDOB.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FGBOT)
            {
                chkFMIMandatoryPOB.Checked = false;
                chkFMIMandatoryPOB.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryPOB.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryPOB.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryPOB.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            }
            if (obj.DESCRIPTION == objFMIColumnNameBo.FGBLD)
            {
                chkFMIMandatoryCOB.Checked = false;
                chkFMIMandatoryCOB.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryCOB.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryCOB.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryCOB.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FANAT)
            {
                chkFMIMandatoryNationality.Checked = false;
                chkFMIMandatoryNationality.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryNationality.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryNationality.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryNationality.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objFMIColumnNameBo.FANA2)
            {
                chkFMIMandatoryNationality2.Checked = false;
                chkFMIMandatoryNationality2.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryNationality2.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryNationality2.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryNationality2.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION.Trim() == objFMIColumnNameBo.FANA3)
            {
                chkFMIMandatoryNationality3.Checked = false;
                chkFMIMandatoryNationality3.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkFMIMandatoryNationality3.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkFMIMandatoryNationality3.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkFMIMandatoryNationality3.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            }

        }
    }
    protected void btnFamilyMemberInformation_Click(object sender, EventArgs e)
    {
        configurationbl objBl = new configurationbl();
        configurationbo objBo = new configurationbo();
        familymemberinformationcolumnsbo objFMIInfoBo = new familymemberinformationcolumnsbo();
        objBo.DESCRIPTION =  objFMIInfoBo.FAMSA + '|' + objFMIInfoBo.FAVOR
                            + '|' + objFMIInfoBo.FANAM + '|' + objFMIInfoBo.FGBNA + '|' + objFMIInfoBo.FINIT + '|' +
                            objFMIInfoBo.FNMZU + '|' + objFMIInfoBo.FVRSW + '|' + objFMIInfoBo.FASEX +
                            '|' + objFMIInfoBo.FGBDT + '|' + objFMIInfoBo.FGBOT + '|' + objFMIInfoBo.FGBLD
                            + '|' + objFMIInfoBo.FANAT + '|' + objFMIInfoBo.FANA2 + '|' + objFMIInfoBo.FANA3;
        
        objBo.MANDATORY_VALUE = chkFMIMandatorySubtype.Checked + "|" + chkFMIMandatoryFirstName.Checked + '|' +
                                chkFMIFMIMandatoryLastName.Checked + '|' + chkFMIMandatoryNOB.Checked + '|' + chkFMIMandatoryInitials.Checked + '|' +
                                chkFMIMandatoryOtherTitle.Checked + '|' + chkFMIMandatoryNamePrefix.Checked + '|' + chkFMIMandatoryGender.Checked + '|' +
                                chkFMIMandatoryDOB.Checked + '|' + chkFMIMandatoryPOB.Checked + '|' + chkFMIMandatoryCOB.Checked
                                + '|' + chkFMIMandatoryNationality.Checked + '|' + chkFMIMandatoryNationality2.Checked + '|' + chkFMIMandatoryNationality3.Checked;
        objBo.MODIFIEDBY = User.Identity.Name.Trim();
        objBo.MODIFIEDON = DateTime.Now;
        try
        {
            int iResultCode = objBl.Update_Family_Member_Informations(objBo);
            if (iResultCode == 0)
            {
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = GetLocalResourceObject("SaveSuccess").ToString();
            }
        }
        catch
        {
            lblMessageBoard.Visible = true;
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
    }
#endregion
    #region Personal Data Information
    protected void LoadPersonalDataDetails()
    {
        configurationbl objBl = new configurationbl();
        configurationcollectionbo objLst = new configurationcollectionbo();
        objLst = objBl.Get_Personal_Data();
        personaldatacolumnsbo objPersonalIdColumnNameBo = new personaldatacolumnsbo();
        foreach (configurationbo obj in objLst)
        {
            if (obj.DESCRIPTION == objPersonalIdColumnNameBo.TITEL)
            {
                chkPDMandatoryTitle.Checked = false;
                chkPDMandatoryTitle.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryTitle.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryTitle.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryTitle.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            }
            if (obj.DESCRIPTION == objPersonalIdColumnNameBo.VORNA)
            {
                chkPDMandatoryFirstName.Checked = false;
                chkPDMandatoryFirstName.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryFirstName.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryFirstName.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryFirstName.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.NACHN)
            {
                chkPDMandatoryLastName.Checked = false;
                chkPDMandatoryLastName.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryLastName.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryLastName.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryLastName.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.NAME2)
            {
                chkPDMandatoryNOB.Checked = false;
                chkPDMandatoryNOB.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryNOB.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryNOB.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryNOB.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.INITS)
            {
                chkPDMandatoryInitials.Checked = false;
                chkPDMandatoryInitials.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryInitials.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryInitials.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryInitials.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.RUFNM)
            {
                chkPDMandatoryKnownAs.Checked = false;
                chkPDMandatoryKnownAs.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryKnownAs.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryKnownAs.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryKnownAs.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.SPRSL)
            {
                chkPDMandatoryLanguage.Checked = false;
                chkPDMandatoryLanguage.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryLanguage.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryLanguage.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryLanguage.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.GESCH)
            {
                chkPDMandatoryGender.Checked = false;
                chkPDMandatoryGender.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryGender.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryGender.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryGender.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.GBDAT)
            {
                chkPDMandatoryDOB.Checked = false;
                chkPDMandatoryDOB.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryDOB.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryDOB.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryDOB.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.GBORT)
            {
                chkPDMandatoryPOB.Checked = false;
                chkPDMandatoryPOB.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryPOB.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryPOB.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryPOB.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
              
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.GBLND)
            {
                chkPDMandatoryCOB.Checked = false;
                chkPDMandatoryCOB.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryCOB.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryCOB.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryCOB.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.NATIO)
            {
                chkPDMandatoryNationality.Checked = false;
                chkPDMandatoryNationality.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryNationality.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryNationality.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryNationality.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.GBDEP)
            {
                chkPDMandatoryState.Checked = false;
                chkPDMandatoryState.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryState.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryState.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryState.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.NATI2)
            {
                chkPDMandatoryNationality2.Checked = false;
                chkPDMandatoryNationality2.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryNationality2.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryNationality2.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryNationality2.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.NATI3)
            {
                chkPDMandatoryNationality3.Checked = false;
                chkPDMandatoryNationality3.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryNationality3.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryNationality3.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryNationality3.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.FAMST)
            {
                chkPDMandatoryMartial.Checked = false;
                chkPDMandatoryMartial.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryMartial.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryMartial.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryMartial.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            }
            if (obj.DESCRIPTION == objPersonalIdColumnNameBo.FAMDT)
            {
                chkPDMandatoryValidFromMaritalStatus.Checked = false;
                chkPDMandatoryValidFromMaritalStatus.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryValidFromMaritalStatus.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryValidFromMaritalStatus.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryValidFromMaritalStatus.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.ANZKD)
            {
                chkPDMandatoryNoOfChildren.Checked = false;
                chkPDMandatoryNoOfChildren.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryNoOfChildren.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryNoOfChildren.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryNoOfChildren.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            } if (obj.DESCRIPTION == objPersonalIdColumnNameBo.KITXT)
            {
                chkPDMandatoryReligious.Checked = false;
                chkPDMandatoryReligious.Enabled = true;
                if (bool.Parse(obj.DEFAULT_VALUE) == true)
                {
                    chkPDMandatoryReligious.Checked = bool.Parse(obj.DEFAULT_VALUE);
                    chkPDMandatoryReligious.Enabled = false;
                }
                if (bool.Parse(obj.MANDATORY_VALUE) == true)
                {
                    chkPDMandatoryReligious.Checked = bool.Parse(obj.MANDATORY_VALUE);
                }
            }
        }

    }
    protected void btnPersonalData_Click(object sender, EventArgs e)
    {
        configurationbl objBl = new configurationbl();
        configurationbo objBo = new configurationbo();
        personaldatacolumnsbo objPersonalDataColumnNameBo = new personaldatacolumnsbo();
        objBo.DESCRIPTION =  objPersonalDataColumnNameBo.TITEL + '|' + objPersonalDataColumnNameBo.VORNA + '|' +
                             objPersonalDataColumnNameBo.NACHN + '|' + objPersonalDataColumnNameBo.NAME2 + '|' + objPersonalDataColumnNameBo.INITS + '|' +
                             objPersonalDataColumnNameBo.RUFNM + '|' + objPersonalDataColumnNameBo.SPRSL + '|' + objPersonalDataColumnNameBo.GESCH + '|' +
                             objPersonalDataColumnNameBo.GBDAT + '|' + objPersonalDataColumnNameBo.GBORT + '|' + objPersonalDataColumnNameBo.GBLND + '|' +
                             objPersonalDataColumnNameBo.NATIO + '|' + objPersonalDataColumnNameBo.GBDEP + '|' + objPersonalDataColumnNameBo.NATI2 + '|' +
                             objPersonalDataColumnNameBo.NATI3 + '|' + objPersonalDataColumnNameBo.FAMST + '|' + objPersonalDataColumnNameBo.FAMDT + '|' +
                             objPersonalDataColumnNameBo.ANZKD + '|' + objPersonalDataColumnNameBo.KITXT;
        objBo.MANDATORY_VALUE =  chkPDMandatoryTitle.Checked + "|" + chkPDMandatoryFirstName.Checked + '|' +
                                chkPDMandatoryLastName.Checked + '|' + chkPDMandatoryNOB.Checked + '|' + chkPDMandatoryInitials.Checked + '|' +
                                chkPDMandatoryKnownAs.Checked + '|' + chkPDMandatoryLanguage.Checked + '|' + chkPDMandatoryGender.Checked + '|' +
                                chkPDMandatoryDOB.Checked + '|' + chkPDMandatoryPOB.Checked + '|' + chkPDMandatoryCOB.Checked + '|' +
                                chkPDMandatoryNationality.Checked + '|' + chkPDMandatoryState.Checked + '|' + chkPDMandatoryNationality2.Checked + '|' +
                                chkPDMandatoryNationality3.Checked + '|' + chkPDMandatoryMartial.Checked + '|' + chkPDMandatoryValidFromMaritalStatus.Checked + '|' +
                                chkPDMandatoryNoOfChildren.Checked + '|' + chkPDMandatoryReligious.Checked;
        objBo.MODIFIEDBY = User.Identity.Name.Trim();
        objBo.MODIFIEDON = DateTime.Now;
        try
        {
            int iResultCode = objBl.Update_Personal_Data(objBo);
            if (iResultCode == 0)
            {
                lblMessageBoard.Visible = true;
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = GetLocalResourceObject("SaveSuccess").ToString();
            }
        }
        catch
        {
            lblMessageBoard.Visible = true;
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
    }
    #endregion
    #region Company logo 
   
    protected void btnLogoSave_Click(object sender, EventArgs e)
    {
        configurationbo objBo = new configurationbo();
        configurationbl objBl = new configurationbl();
         // limitation of maximum file size
        int intFileSizeLimit = 10;

        // get the full path of your computer 
        string strFileNameWithPath = fulLogo.PostedFile.FileName;
        // get the extension name of the file
        string strExtensionName = System.IO.Path.GetExtension(strFileNameWithPath);
        // get the filename of user file
        string strFileName = System.IO.Path.GetFileName(strFileNameWithPath);
        // get the file size
        int intFileSize = fulLogo.PostedFile.ContentLength / 1024; // convert into byte

        // Restrict the user to upload only .gif or .jpg file
        strExtensionName = strExtensionName.ToLower();
        if (strExtensionName.Equals(".jpg") || strExtensionName.Equals(".gif") || strExtensionName.Equals(".jpeg") || strExtensionName.Equals(".png"))
        {

            if (fulLogo.HasFile && fulLogo.PostedFile.ContentLength > 0)
            {
                string fileName = fulLogo.FileName;
                byte[] fileByte = fulLogo.FileBytes;
                Binary binaryObj = new Binary(fileByte);
                objBo.LOGO = binaryObj.ToArray();
                objBo.CREATEDBY = User.Identity.Name.Trim();
                objBo.CREATEDON = DateTime.Now;
                try
                {
                    int iResultCode = objBl.Create_Logo(objBo);
                    if (iResultCode == 0)
                    {
                        lblLogoMessage.Visible = true;
                        lblLogoMessage.ForeColor = System.Drawing.Color.Green;
                        lblLogoMessage.Text = GetLocalResourceObject("SaveSuccess").ToString();
                    }
                }
                catch
                {
                    lblLogoMessage.Visible = true;
                    lblLogoMessage.ForeColor = System.Drawing.Color.Red;
                    lblLogoMessage.Text = GetLocalResourceObject("UnkownError").ToString();
                    return;
                }
            }
        }
        else
        {
            lblLogoMessage.Text = "Only .jpg , .jpeg , .png or .gif file are allowed, try again!";
            lblLogoMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }
        LoadImageLogo();
    }
    protected void LoadImageLogo()
    {
        lblGridMessage.Text = "";
        lblMessageBoard.Text = "";
        lblSeverMessage.Text = "";
        lblPhotoMessage.Text = "";
        configurationdalDataContext context1 = new configurationdalDataContext();
        var vLogo = (from col in context1.sp_conf_get_logo()
                     select col.img_logo).ToList();
        if (vLogo.Count <= 0)
        {
            imageLogo.Visible = false;
        }
        else
        {

            imageLogo.Visible = true;
            imageLogo.ImageUrl = "~/imagelogo.ashx";
        }
    }
    #endregion
    #region Employee photo path
    protected void btnEmployePhotoPath_Click(object sender, EventArgs e)
    {
        configurationbo objBo = new configurationbo();
        configurationbl objBl = new configurationbl();
        objBo.EMPLOYEE_PATH = txtEmployePhotoPath.Text.Trim();
        objBo.CREATEDBY = User.Identity.Name.Trim();
        objBo.CREATEDON = DateTime.Now;
        try
        {
            int iResultCode = objBl.Create_EmployeePhoto(objBo);
            if (iResultCode == 0)
            {
                lblPhotoMessage.Visible = true;
                lblPhotoMessage.ForeColor = System.Drawing.Color.Green;
                lblPhotoMessage.Text = GetLocalResourceObject("SaveSuccess").ToString();
            }
        }
        catch
        {
            lblPhotoMessage.Visible = true;
            lblPhotoMessage.ForeColor = System.Drawing.Color.Red;
            lblPhotoMessage.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
        LoadEmployeePhotoPath();
    }
     protected void LoadEmployeePhotoPath()
     {
         lblGridMessage.Text = "";
         lblMessageBoard.Text = "";
         lblSeverMessage.Text = "";
         lblLogoMessage.Text = "";
         configurationbl objBl = new configurationbl();
         configurationcollectionbo objConfigurationList = new configurationcollectionbo();
         objConfigurationList = objBl.Get_EmployeePhoto();
         if (objConfigurationList.Count <=0 )
         {
             lblEmployePhotoPath.Visible = false;
         }
         else
         {

             lblEmployePhotoPath.Visible = true;
             foreach (configurationbo objBo in objConfigurationList)
             {
                 lblEmployePhotoPath.Text = objBo.EMPLOYEE_PATH;
             }
         }
     }

     protected void LoadLeavesEncashable()
     {
         configurationbl objBl = new configurationbl();
         configurationcollectionbo objList = new configurationcollectionbo();

         objList = objBl.Get_EncashableLeaves();
         if (objList != null && objList.Count > 0)
         {
             grdLeaveEncashable.DataSource = objList;
             grdLeaveEncashable.DataBind();
             grdLeaveEncashable.Columns[0].Visible = false;
             grdLeaveEncashable.Columns[1].Visible = false;
             lblLeaveEncashment.Text = string.Empty;
             btnLESave.Visible = true;
             btnLESelectAll.Visible = true;
             btnLEClearAll.Visible = true;
         }
         else
         {
             grdLeaveEncashable.DataSource = null;
             grdLeaveEncashable.DataBind();
             lblLeaveEncashment.Text = "Leave type details not found";
             btnLESave.Visible = false;
             btnLESelectAll.Visible = false;
             btnLEClearAll.Visible = false;
         }
     }
    #endregion

     protected void btnLESave_Click(object sender, EventArgs e)
     {
         string sMoawb = string.Empty;
         string sAwart = string.Empty;
         string sAtext = string.Empty;
         string sEncashable = string.Empty;

         try
         {
             configurationbo objBo = new configurationbo();

             grdLeaveEncashable.Columns[0].Visible = true;
             grdLeaveEncashable.Columns[1].Visible = true;

             foreach (GridViewRow row in grdLeaveEncashable.Rows)
             {
                 if (sMoawb == string.Empty)
                 {
                     sMoawb = row.Cells[0].Text.Trim();
                 }
                 else
                 {
                     sMoawb = sMoawb + "|" + row.Cells[0].Text.Trim();
                 }

                 if (sAwart == string.Empty)
                 {
                     sAwart = row.Cells[1].Text.Trim();
                 }
                 else
                 {
                     sAwart = sAwart + "|" + row.Cells[1].Text.Trim();
                 }

                 if (sAtext == string.Empty)
                 {
                     sAtext = row.Cells[2].Text.Trim();
                 }
                 else
                 {
                     sAtext = sAtext + "|" + row.Cells[2].Text.Trim();
                 }

                 CheckBox chk = (CheckBox)row.FindControl("chkBxEncashStatus");
                 if (sEncashable == string.Empty)
                 {
                     if (chk.Checked)
                     {
                         sEncashable = "1";
                     }
                     else
                     {
                         sEncashable = "0";
                     }
                 }
                 else
                 {
                     if (chk.Checked)
                     {
                         sEncashable = sEncashable + "|" + "1";
                     }
                     else
                     {
                         sEncashable = sEncashable + "|" + "0";
                     }
                 }
                 objBo.MOAWB_STRING = sMoawb;
                 objBo.AWART_STRING = sAwart;
                 objBo.ATEXT_STRING = sAtext;
                 objBo.ENCASHABLE_STRING = sEncashable;                 
             }
             configurationbl objBl = new configurationbl();

             int iResult = objBl.Create_LeaveEncashmentTypes(objBo);

             if (iResult == 0)
             {
                 lblLeaveEncashment.Visible = true;
                 lblLeaveEncashment.ForeColor = System.Drawing.Color.Green;
             }
         }
         catch (Exception ex)
         {
             lblLeaveEncashment.Visible = true;
             lblLeaveEncashment.ForeColor = System.Drawing.Color.Red;
             lblLeaveEncashment.Text = "Unknown error occured. Please contact your system administrator.";
         }

         LoadLeavesEncashable();
         //Page.SetFocus(updtPnlLeaveEncashment);
     }
     protected void btnLESelectAll_Click(object sender, EventArgs e)
     {
         if (grdLeaveEncashable.Rows.Count > 0)
         {
             foreach(GridViewRow grdRow in grdLeaveEncashable.Rows)
             {
                 CheckBox chkBox = (CheckBox)grdRow.FindControl("chkBxEncashStatus");
                 chkBox.Checked = true;
             }
             //Page.SetFocus(updtPnlLeaveEncashment);
         }
     }
     protected void btnLEClearAll_Click(object sender, EventArgs e)
     {
         foreach (GridViewRow grdRow in grdLeaveEncashable.Rows)
         {
             CheckBox chkBox = (CheckBox)grdRow.FindControl("chkBxEncashStatus");
             chkBox.Checked = false;
         }
         //Page.SetFocus(updtPnlLeaveEncashment);
     }
}
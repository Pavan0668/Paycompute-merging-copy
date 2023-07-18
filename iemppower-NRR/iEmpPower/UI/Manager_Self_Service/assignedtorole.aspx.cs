using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_Manager_Self_Service_assignedtorole : System.Web.UI.Page
{
  
    bool bSortedOrder = false;
    public int rselectedindex = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tcAssignToMe.ActiveTab = tabPending;
            LoadGridDetails();
            AllPnelStatus();
            Session.Add("bSortedOrder", bSortedOrder);
            ViewState.Add("indexchang", rselectedindex);

            btnAddressInfoApprove.Attributes.Add("onclick", " return ValidateControls('" + txtAddressInfoComments.ClientID + "');");
            btnReject.Attributes.Add("onclick", " return ValidateControls('" + txtAddressInfoComments.ClientID + "');");
            btnBankDetailsApprove.Attributes.Add("onclick", " return ValidateControls('" + txtBankComments.ClientID + "');");
            btnBankDetailsReject.Attributes.Add("onclick", " return ValidateControls('" + txtBankComments.ClientID + "');");

            btnClockInOut.Attributes.Add("onclick", " return ValidateControls('" + txtClockInOutComments.ClientID + "');");
            btnClockInOutReject.Attributes.Add("onclick", " return ValidateControls('" + txtClockInOutComments.ClientID + "');");
            btnCommunicationInfo.Attributes.Add("onclick", " return ValidateControls('" + txtCommunicationInfoCommnets.ClientID + "');");
            btnCommunicationInfoReject.Attributes.Add("onclick", " return ValidateControls('" + txtCommunicationInfoCommnets.ClientID + "');");

            btnFMApprove.Attributes.Add("onclick", " return ValidateControls('" + txtFMComments.ClientID + "');");
            btnFMReject.Attributes.Add("onclick", " return ValidateControls('" + txtFMComments.ClientID + "');");
            btnPDInfoApprove.Attributes.Add("onclick", " return ValidateControls('" + txtPDCommnets.ClientID + "');");
            btnPDInfoReject.Attributes.Add("onclick", " return ValidateControls('" + txtPDCommnets.ClientID + "');");

            btnPIDetailsApprove.Attributes.Add("onclick", " return ValidateControls('" + txtPIComments.ClientID + "');");
            btnPIDetailsReject.Attributes.Add("onclick", " return ValidateControls('" + txtPIComments.ClientID + "');");
            btnRWApprove.Attributes.Add("onclick", " return ValidateControls('" + txtRWComments.ClientID + "');");
            btnRWReject.Attributes.Add("onclick", " return ValidateControls('" + txtRWComments.ClientID + "');");
            btnLeaveRequest.Attributes.Add("onclick", " return ValidateControls('" + txtLRComments.ClientID + "');");
            btnLRReject.Attributes.Add("onclick", " return ValidateControls('" + txtLRComments.ClientID + "');");
        }
    }
    protected void tcAssignToMe_ActiveTabChanged(object sender, EventArgs e)
    {
        AllPnelStatus();
        if (tcAssignToMe.ActiveTab == tabPending)
        {
            //LoadGridDetails();
        }
        else
        {
            LoadCompletedGridDetails();
        }
    }
    protected void AllPnelStatus()
    {
        lblMessageBoard.Text = "";
        pnlRecordWorking.Visible = false;
        pnlAddressInfo.Visible = false;
        pnlPersonalIdInfo.Visible = false;
        pnlBankInfo.Visible = false;
        pnlClockInOut.Visible = false;
        pnlCommunicationInfo.Visible = false;
        pnlpersonaldataInfo.Visible = false;
        pnlLeaveRequest.Visible = false;
        pnlFamilyMember.Visible = false;
    }
    protected void LoadGridDetails()
    {
        msassignedtomebo objAssginTMBo = new msassignedtomebo();
        msassignedtorolebl objAssginTMBl = new msassignedtorolebl();
        objAssginTMBo.PERNR = User.Identity.Name;
        msassignedtomecollectionbo objAssginTMLst = objAssginTMBl.Load_Pending_AssignedTR(objAssginTMBo);
        if (objAssginTMLst.Count <= 0)
        {
            lblMessageBoard.Text = GetLocalResourceObject("NoPendingRecord").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
        }
        grdPending.Columns[0].Visible = true;
        grdPending.Columns[1].Visible = true;
        grdPending.Columns[2].Visible = true;
        grdPending.DataSource = objAssginTMLst;
        grdPending.DataBind();
        grdPending.Columns[0].Visible = false;
        grdPending.Columns[1].Visible = false;
        grdPending.Columns[2].Visible = false;
        Session.Add("grdLst", objAssginTMLst);

    }
    protected void LoadCompletedGridDetails()
    {
        msassignedtomebo objAssginTMBo = new msassignedtomebo();
        msassignedtorolebl objAssginTMBl = new msassignedtorolebl();
        objAssginTMBo.PERNR = User.Identity.Name;
        msassignedtomecollectionbo objAssginTMCompletedLst = objAssginTMBl.Load_Completed_AssignedTR(objAssginTMBo);
        if (objAssginTMCompletedLst.Count <= 0)
        {
            lblMessageBoard.Text = GetLocalResourceObject("NoCompletedRecord").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Green;
        }
        grdCompleted.Columns[0].Visible = true;
        grdCompleted.Columns[1].Visible = true;
        grdCompleted.Columns[2].Visible = true;
        grdCompleted.DataSource = objAssginTMCompletedLst;
        grdCompleted.DataBind();
        grdCompleted.Columns[0].Visible = false;
        grdCompleted.Columns[1].Visible = false;
        grdCompleted.Columns[2].Visible = false;
        Session.Add("grdCmpltdLst", objAssginTMCompletedLst);

    }
    protected void grdPending_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        AllPnelStatus();
        int pageindex = e.NewPageIndex;
        grdPending.PageIndex = e.NewPageIndex;
        msassignedtomecollectionbo objPIDashBoardLst = (msassignedtomecollectionbo)Session["grdLst"];
        grdPending.DataSource = objPIDashBoardLst;
        int rselectedindex = Convert.ToInt32(ViewState["indexchang"]);
        int pagindex = Convert.ToInt32(ViewState["pageindex"]);
        grdPending.Columns[0].Visible = true;
        grdPending.Columns[1].Visible = true;
        grdPending.Columns[2].Visible = true;
        grdPending.DataSource = objPIDashBoardLst;
        grdPending.SelectedIndex = -1;
        grdPending.DataBind();
        grdPending.Columns[0].Visible = false;
        grdPending.Columns[1].Visible = false;
        grdPending.Columns[2].Visible = false;
        if (pageindex == pagindex)
        {
            grdPending.SelectedIndex = rselectedindex;
        }
    }
    protected void grdPending_Sorting(object sender, GridViewSortEventArgs e)
    {
        msassignedtomecollectionbo objPIDashBoardLst = (msassignedtomecollectionbo)Session["grdLst"];
        bool objSortOrder = (bool)Session["bSortedOrder"];
        switch (e.SortExpression.ToString().Trim())
        {
            
            case "CHANGE_APPROVAL":
                if (objSortOrder)
                {
                    if (objPIDashBoardLst != null)
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.CHANGE_APPROVAL.CompareTo(objBo2.CHANGE_APPROVAL)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.CHANGE_APPROVAL.CompareTo(objBo1.CHANGE_APPROVAL)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "REVIEW":
                if (objSortOrder)
                {
                    if (objPIDashBoardLst != null)
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.REVIEW.CompareTo(objBo2.REVIEW)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.REVIEW.CompareTo(objBo1.REVIEW)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "LAST_ACTIVITY_DATE":
                if (objSortOrder)
                {
                    if (objPIDashBoardLst != null)
                    {
                        objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.LAST_ACTIVITY_DATE.CompareTo(objBo2.LAST_ACTIVITY_DATE)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.LAST_ACTIVITY_DATE.CompareTo(objBo1.LAST_ACTIVITY_DATE)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;

        }

        grdPending.DataSource = objPIDashBoardLst;
        grdPending.DataBind();

        Session.Add("grdLst", objPIDashBoardLst);
    }
    protected void grdCompleted_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        AllPnelStatus();
        int pageindex = e.NewPageIndex;
        grdCompleted.PageIndex = e.NewPageIndex;
        msassignedtomecollectionbo objPIDashBoardCmpltdLst = (msassignedtomecollectionbo)Session["grdCmpltdLst"];
        grdCompleted.DataSource = objPIDashBoardCmpltdLst;
        int rselectedindex = Convert.ToInt32(ViewState["indexchang"]);
        int pagindex = Convert.ToInt32(ViewState["pageindex"]);

        grdCompleted.Columns[0].Visible = true;
        grdCompleted.Columns[1].Visible = true;
        grdCompleted.Columns[2].Visible = true;
        grdCompleted.DataSource = objPIDashBoardCmpltdLst;
        grdCompleted.SelectedIndex = -1;
        grdCompleted.DataBind();
        grdCompleted.Columns[0].Visible = false;
        grdCompleted.Columns[1].Visible = false;
        grdCompleted.Columns[2].Visible = false;
        if (pageindex == pagindex)
        {
            grdCompleted.SelectedIndex = rselectedindex;
        }
    }
    protected void grdCompleted_Sorting(object sender, GridViewSortEventArgs e)
    {
        msassignedtomecollectionbo objPIDashBoardCmpltdLst = (msassignedtomecollectionbo)Session["grdCmpltdLst"];
        bool objSortOrder = (bool)Session["bSortedOrder"];
        switch (e.SortExpression.ToString().Trim())
        {
           
            case "CHANGE_APPROVAL":
                if (objSortOrder)
                {
                    if (objPIDashBoardCmpltdLst != null)
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.CHANGE_APPROVAL.CompareTo(objBo2.CHANGE_APPROVAL)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.CHANGE_APPROVAL.CompareTo(objBo1.CHANGE_APPROVAL)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "REVIEW":
                if (objSortOrder)
                {
                    if (objPIDashBoardCmpltdLst != null)
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.REVIEW.CompareTo(objBo2.REVIEW)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.REVIEW.CompareTo(objBo1.REVIEW)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
            case "LAST_ACTIVITY_DATE":
                if (objSortOrder)
                {
                    if (objPIDashBoardCmpltdLst != null)
                    {
                        objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                        { return (objBo1.LAST_ACTIVITY_DATE.CompareTo(objBo2.LAST_ACTIVITY_DATE)); });
                        objSortOrder = false;
                        Session.Add("bSortedOrder", objSortOrder);
                    }
                }
                else
                {
                    objPIDashBoardCmpltdLst.Sort(delegate(msassignedtomebo objBo1, msassignedtomebo objBo2)
                    { return (objBo2.LAST_ACTIVITY_DATE.CompareTo(objBo1.LAST_ACTIVITY_DATE)); });
                    objSortOrder = true;
                    Session.Add("bSortedOrder", objSortOrder);
                }
                break;
        }
        grdCompleted.Columns[0].Visible = true;
        grdCompleted.Columns[1].Visible = true;
        grdCompleted.DataSource = objPIDashBoardCmpltdLst;
        grdCompleted.DataBind();
        grdCompleted.Columns[0].Visible = false;
        grdCompleted.Columns[1].Visible = false;

        Session.Add("grdCmpltdLst", objPIDashBoardCmpltdLst);
    }
    protected void AddressControlStatus(Boolean bIsStatus)
    {
        pnlAddressInfo.Visible = true;
        lblValidateAddressInfoComments.Text = "";
        lblValidateAddressInfoComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        lblValidateNewIDType.Text = "";
        lblValidateNewPhoneNo.Text = "";
        lblValidateNewState.Text = "";
        lblValidateNewDateTo.Text = "";
        lblValidateNewDateFrom.Text = "";
        lblValidateNewCountry.Text = "";
        lblValidateNewCity.Text = "";
        lblValidateNewAddress2.Text = "";
        lblValidateNewAddress1.Text = "";
        txtAddressInfoComments.Text = "";
        if (bIsStatus == true)
        {
            lblValidateAddressInfoComments.Text = "*";
            lblValidateAddressInfoComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
            lblValidateNewIDType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPhoneNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewState.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewDateTo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewDateFrom.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewCountry.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewCity.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewAddress2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewAddress1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            txtAddressInfoComments.Visible = true;
            btnAddressInfoApprove.Visible = true;
            btnReject.Visible = true;
            
            
            lblValidateIDType.Text = "-";
            lblValidatePhoneNo.Text = "-";
            lblValidateState.Text = "-";
            lblValidateDateTo.Text = "-";
            lblValidateDateFrom.Text = "-";
            lblValidateCountry.Text = "-";
            lblValidateCity.Text = "-";
            lblValidateAddress2.Text = "-";
            lblValidateAddress1.Text = "-";
        }
        else
        {
            txtAddressInfoComments.Visible = false;
            btnAddressInfoApprove.Visible = false;
            btnReject.Visible = false;
        }
    }
    protected void BankDetailsControlStatus(Boolean bIsStatus)
    {
        
        pnlBankInfo.Visible = true;
        lblValidateBankComments.Text = "";
        lblValidateBankComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        txtBankComments.Text = "";
        lblValidateNewBankType.Text = "";
        lblValidateNewPayee.Text = "";
        lblValidateNewPostalCode.Text = "";
        lblValidateNewBankCountry.Text = "";
        lblValidateNewBankKey.Text = "";
        lblValidateNewBankAccount.Text = "";
        lblValidateNewPaymentMethod.Text = "";
        lblValidateNewPurpose.Text = "";
        lblValidateNewCurrency.Text = "";
        if (bIsStatus == true)
        {
            lblValidateBankComments.Text = "*";
            lblValidateClockInOutComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
            lblValidateNewBankType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPayee.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPostalCode.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewBankCountry.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewBankKey.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewBankAccount.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPaymentMethod.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPurpose.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewCurrency.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            btnBankDetailsApprove.Visible = true;
            btnBankDetailsReject.Visible = true;
            txtBankComments.Visible = true;
            lblValidateBankType.Text = "-";
            lblValidatePayee.Text = "-";
            lblValidatePostalCode.Text = "-";
            lblValidateBankCountry.Text = "-";
            lblValidateBankKey.Text = "-";
            lblValidateBankAccount.Text = "-";
            lblValidatePaymentMethod.Text = "-";
            lblValidatePurpose.Text = "-";
            lblValidateCurrency.Text = "-";

        }
        else
        {
            btnBankDetailsApprove.Visible = false;
            btnBankDetailsReject.Visible = false;
            txtBankComments.Visible = false;
        }
    }
    protected void CommmunicationDetailsControlStatus(Boolean bIsStatus)
    {
       
        lblValidateCommunicationInfoCommnets.Text = "";
        pnlCommunicationInfo.Visible = true;
        lblValidateCommunicationInfoCommnets.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        lblValidateNewBuildingNumber.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        lblValidateNewRoomNumber.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        lblValidateNewExtension.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        lblValidateNewEmail.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        lblValidateNewLicencePlateNumber.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        lblValidateNewMobileNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");

        lblValidateNewMobileNo.Text = "";
        lblValidateNewBuildingNumber.Text = "";
        lblValidateNewRoomNumber.Text = "";
        lblValidateNewExtension.Text = "";
        lblValidateNewEmail.Text = "";
        lblValidateNewLicencePlateNumber.Text = "";
        if (bIsStatus == true)
        {
            lblValidateCommunicationInfoCommnets.Text = "*";
            lblValidateCommunicationInfoCommnets.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
            btnCommunicationInfo.Visible = true;
            btnCommunicationInfoReject.Visible = true;
            txtCommunicationInfoCommnets.Visible = true;
            lblValidateBuildingNumber.Text = "-";
            lblValidateRoomNumber.Text = "-";
            lblValidateExtension.Text = "-";
            lblValidateEmail.Text = "-";
            lblValidateLicencePlateNumber.Text = "-";
            lblValidateMobileNo.Text = "-";
        }
        else
        {
            btnCommunicationInfo.Visible = false;
            btnCommunicationInfoReject.Visible = false;
            txtCommunicationInfoCommnets.Visible = false;
        }
    }
    protected void PersonalDataDetailsControlStatus(Boolean bIsStatus)
    {  
        lblValidatePDCommnets.Text = "";
        lblValidatePDCommnets.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        pnlpersonaldataInfo.Visible = true;
        lblValidateNewPDTitle.Text = "";
        lblValidateNewPDFirstName.Text = "";
        lblValidateNewPDLastName.Text = "";
        lblValidateNewPDBirthName.Text = "";
        lblValidateNewPDInitials.Text = "";
        lblValidateNewPDKnownAs.Text = "";
        lblValidateNewPDLanguage.Text = ""; ;
        lblValidateNewPDGender.Text = "";
        lblValidateNewPDDOB.Text = "";
        lblValidateNewPDPOB.Text = "";
        lblValidateNewPDCOB.Text = "";
        lblValidateNewPDNationality.Text = "";
        lblValidateNewPDState.Text = "";
        lblValidateNewPD2ndNtnlty.Text = "";
        lblValidateNewPD3rdNtnlty.Text = "";
        lblValidateNewPDMaritalStatus.Text = "";
        lblValidateNewPDMarriedSince.Text = "";
        lblValidateNewPDNoOfChildren.Text = "";
        lblValidateNewPDReligion.Text = "";
        if (bIsStatus == true)
        {
            lblValidatePDCommnets.Text = "*";
            lblValidatePDCommnets.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
            btnPDInfoApprove.Visible = true;
            btnPDInfoReject.Visible = true;
            txtPDCommnets.Visible = true;

            lblValidateNewPDTitle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDFirstName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDLastName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDBirthName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDInitials.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDKnownAs.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDLanguage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDGender.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDDOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDPOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDCOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDNationality.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDState.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPD2ndNtnlty.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPD3rdNtnlty.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDMaritalStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDMarriedSince.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDNoOfChildren.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewPDReligion.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");

            lblValidatePDTitle.Text = "-";
            lblValidatePDFirstName.Text = "-";
            lblValidatePDLastName.Text = "-";
            lblValidatePDBirthName.Text = "-";
            lblValidatePDInitials.Text = "-";
            lblValidatePDKnownAs.Text = "-";
            lblValidatePDLanguage.Text = "-";
            lblValidatePDGender.Text = "-";
            lblValidatePDDOB.Text = "-";
            lblValidatePDPOB.Text = "-";
            lblValidatePDCOB.Text = "-";
            lblValidatePDNationality.Text = "-";
            lblValidatePDState.Text = "-";
            lblValidatePD2ndNtnlty.Text = "-";
            lblValidatePD3rdNtnlty.Text = "-";
            lblValidatePDMaritalStatus.Text = "-";
            lblValidatePDMarriedSince.Text = "-";
            lblValidatePDNoOfChildren.Text = "-";
            lblValidatePDReligion.Text = "-";
        }
        else
        {
            btnPDInfoApprove.Visible = false;
            btnPDInfoReject.Visible = false;
            txtPDCommnets.Visible = false;
            
        }
    }
    protected void PersonalIDsDetailsControlStatus(Boolean bIsStatus)
    {
       
        lblValidatePIComments.Text = "";
        lblValidatePIComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        pnlPersonalIdInfo.Visible = true;
        txtPIComments.Text = "";
        lblValidateNewIDype.Text = "";
        lblValidateNewIDNo.Text = "";
        if (bIsStatus == true)
        {
            lblValidatePIComments.Text = "*";
            lblValidatePIComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
            btnPIDetailsApprove.Visible = true;
            btnPIDetailsReject.Visible = true;
            txtPIComments.Visible = true;

            lblValidateIDype.Text = "-";
            lblValidateIDNo.Text = "-";

            lblValidateNewIDype.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewIDNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        }
        else
        {
            btnPIDetailsApprove.Visible = false;
            btnPIDetailsReject.Visible = false;
            txtPIComments.Visible = false;
        }
    }
    protected void ClockIODetailsControlStatus(Boolean bIsStatus)
    {
        lblValidateClockInOutComments.Text = "";
        lblValidateClockInOutComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        pnlClockInOut.Visible = true;
        txtClockInOutComments.Text = "";
        lblValidateNewRequestStatus.Text = "";
        lblValidateNewDate.Text = "";
        lblValidateNewTime.Text = "";
        lblValidateNewClockInOut.Text = "";
        lblValidateNewApprovedBy.Text = "";
        lblValidateNewNote.Text = "";
        if (bIsStatus == true)
        {
            lblValidateClockInOutComments.Text = "*";
            lblValidateClockInOutComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
            btnClockInOut.Visible = true;
            btnClockInOutReject.Visible = true;
            txtClockInOutComments.Visible = true;
            lblValidateRequestStatus.Text = "-";
            lblValidateDate.Text = "-";
            lblValidateTime.Text = "-";
            lblValidateClockInOut.Text = "-";
            lblValidateApprovedBy.Text = "-";
            lblValidateNote.Text = "-";

            lblValidateNewRequestStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewTime.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewClockInOut.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewApprovedBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewNote.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        }
        else
        {
            btnClockInOut.Visible = false;
            btnClockInOutReject.Visible = false;
            txtClockInOutComments.Visible = false;
        }
    }
    protected void RecordWorkingDetailsControlStatus(Boolean bIsStatus)
    {
        pnlRecordWorking.Visible = true;
        lblValidateRWCommnets.Text = "";
        lblValidateRWCommnets.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        txtRWComments.Text = "";
        lblValidateNewRWType.Text = "";
        lblValidateNewRWDate.Text = "";
        lblValidateNewRWHours.Text = "";
        lblValidateNewRWCostCenter.Text = "";
        lblValidateNewRWOrder.Text = "";
        if (bIsStatus == true)
        {
            lblValidateRWCommnets.Text = "*";
            lblValidateRWCommnets.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
            btnRWApprove.Visible = true;
            btnRWReject.Visible = true;
            txtRWComments.Visible = true;

            lblValidateRWType.Text = "-";
            lblValidateRWDate.Text = "-";
            lblValidateRWHours.Text = "-";
            lblValidateRWCostCenter.Text = "-";
            lblValidateRWOrder.Text = "-";

            lblValidateNewRWType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewRWDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewRWHours.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewRWOrder.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewRWCostCenter.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");

        }
        else
        {
            btnRWApprove.Visible = false;
            btnRWReject.Visible = false;
            txtRWComments.Visible = false;
        }
    }
    protected void FamilyMemberDetailsControlStatus(Boolean bIsStatus)
    {
        pnlFamilyMember.Visible = true;
        lblValidateFMComments.Text = "";
        lblValidateFMComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        
        txtFMComments.Text = "";

        if (bIsStatus == true)
        {
            lblValidateFMComments.Text = "*";
            lblValidateFMComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
            btnFMReject.Visible = true;
            txtFMComments.Visible = true;
            btnFMApprove.Visible = true;

            lblValidateNewFMCOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMDOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMFirstName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMGender.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMInitials.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMLastName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMMembertype.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMNameOfBirth.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMNamePrefix.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMNationality.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMNationality2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMNationality3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMOtherTitle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFMPOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");

            lblValidateNewHostelAllowance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewOtherAllowance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewEducationalAllowance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateFMCOB.Text = "-";
            lblValidateFMDOB.Text = "-";
            lblValidateFMFirstName.Text = "-";
            lblValidateFMGender.Text = "-";
            lblValidateFMInitials.Text = "-";
            lblValidateFMLastName.Text = "-";
            lblValidateFMMembertype.Text = "-";
            lblValidateFMNameOfBirth.Text = "-";
            lblValidateFMNamePrefix.Text = "-";
            lblValidateFMNationality.Text = "-";
            lblValidateFMNationality2.Text = "-";
            lblValidateFMNationality3.Text = "-"; ;
            lblValidateFMOtherTitle.Text = "-"; ;
            lblValidateFMPOB.Text = "-"; ;
        }
        else
        {
            btnFMApprove.Visible = false;
            btnFMReject.Visible = false;
            txtFMComments.Visible = false;
            lblValidateNewFMCOB.Text = "";
            lblValidateNewFMDOB.Text = "";
            lblValidateNewFMFirstName.Text = "";
            lblValidateNewFMGender.Text = "";
            lblValidateNewFMInitials.Text = "";
            lblValidateNewFMLastName.Text = "";
            lblValidateNewFMMembertype.Text = "";
            lblValidateNewFMNameOfBirth.Text = "";
            lblValidateNewFMNamePrefix.Text = "";
            lblValidateNewFMNationality.Text = "";
            lblValidateNewFMNationality2.Text = "";
            lblValidateNewFMNationality3.Text = "";
            lblValidateNewFMOtherTitle.Text = "";
            lblValidateNewFMPOB.Text = "";
            lblValidateNewHostelAllowance.Text = "";
            lblValidateNewOtherAllowance.Text = "";
            lblValidateNewEducationalAllowance.Text = "";
        }
    }
    protected void LeaveRequestDetailsControlStatus(Boolean bIsStatus)
    {
        pnlLeaveRequest.Visible = true;
        lblLRComments.Text = "";
        lblLRComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
        txtLRNewComments.Text = "";

        if (bIsStatus == true)
        {
            lblLRComments.Text = "*";
            lblLRComments.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF3300");
            btnLRReject.Visible = true;
            txtLRComments.Visible = true;
            btnLeaveRequest.Visible = true;

            lblValidateNewTypeOfLeave.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFromDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewToDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewFromTime.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewToTime.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewDuration.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewApprover.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");
            lblValidateNewNoteForApprover.ForeColor = System.Drawing.ColorTranslator.FromHtml("#004080");

            lblValidateTypeOfLeave.Text = "-";
            lblValidateFromDate.Text = "-";
            lblValidateToDate.Text = "-";
            lblValidateFromTime.Text = "-";
            lblValidateToTime.Text = "-";
            lblValidateDuration.Text = "-";
            lblValidateApprover.Text = "-";
            lblValidateNoteForApprover.Text = "-";
        }
        else
        {
            btnLeaveRequest.Visible = false;
            btnLRReject.Visible = false;
            txtLRComments.Visible = false;
            lblValidateNewTypeOfLeave.Text = "";
            lblValidateNewFromDate.Text = "";
            lblValidateNewToDate.Text = "";
            lblValidateNewFromTime.Text = "";
            lblValidateNewToTime.Text = "";
            lblValidateNewDuration.Text = "";
            lblValidateNewApprover.Text = "";
            lblValidateNewNoteForApprover.Text = "";
        }
    }

    protected void grdPending_SelectedIndexChanged(object sender, EventArgs e)
    {
        AllPnelStatus();
        GridViewRow grdRow = grdPending.SelectedRow;
        Session.Add("currentSelectedRow", grdRow);
        string sName = grdRow.Cells[1].Text;
        string sEmailId = grdRow.Cells[0].Text;
        string sPernr = grdRow.Cells[3].Text;
        string sPkey = grdRow.Cells[2].Text;
        string sApprovalType = grdRow.Cells[4].Text;
        DateTime dtLateDate = DateTime.Parse(grdRow.Cells[6].Text);
        pidashboardbl objPIDashBl = new pidashboardbl();
        try
        {
            switch (sApprovalType)
            {
                case "Address change approval":
                    AddressControlStatus(true);
                    piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                    objPIAddBo.PKEY = sPkey;
                    objPIAddBo.APPROVED_BY = User.Identity.Name;
                    lblAINameValue.Text = sName;
                    lblAIEmailValue.Text = sEmailId;

                    piaddressinformationcollectionbo objPIAddBoLst = objPIDashBl.Get_Address_Details_For_Approval(objPIAddBo);
                    piaddressinformationbo objOldBo = objPIAddBoLst.Find(delegate(piaddressinformationbo obj)
                    { return obj.ISAPPROVED == true; });
                    piaddressinformationbo objNewBo = objPIAddBoLst.Find(delegate(piaddressinformationbo obj)
                    { return obj.ISAPPROVED == false; });
                    Session.Add("objBoAddressDetails", objNewBo.PKEY);
                    //Old address details
                    if (objOldBo != null)
                    {
                        lblValidateIDType.Text = objOldBo.ADDRESS_TYPE_ID.ToString();
                        lblValidatePhoneNo.Text = objOldBo.PHONENO;
                        lblValidateState.Text = objOldBo.STATE_ID;
                        lblValidateDateTo.Text = objOldBo.DATE_TO.ToString();
                        lblValidateDateFrom.Text = objOldBo.DATE_FROM.ToString();
                        lblValidateCountry.Text = objOldBo.GBLND;
                        lblValidateCity.Text = objOldBo.CITY;
                        lblValidateAddress2.Text = objOldBo.ADDRESSL2;
                        lblValidateAddress1.Text = objOldBo.ADDRESSL1;
                        txtAddressInfoComments.Text = "";

                        // new address details
                        lblValidateNewIDType.Text = objNewBo.ADDRESS_TYPE_ID.ToString();
                        if (objOldBo.PHONENO != objNewBo.PHONENO)
                            lblValidateNewPhoneNo.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewPhoneNo.Text = objNewBo.PHONENO;
                        if (objOldBo.STATE_ID != objNewBo.STATE_ID)
                            lblValidateNewState.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewState.Text = objNewBo.STATE_ID;
                        if (objOldBo.DATE_TO.ToString() != objNewBo.DATE_TO.ToString())
                            lblValidateNewDateTo.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewDateTo.Text = objNewBo.DATE_TO.ToString();
                        if (objOldBo.DATE_FROM.ToString() != objNewBo.DATE_FROM.ToString())
                            lblValidateNewDateFrom.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewDateFrom.Text = objNewBo.DATE_FROM.ToString();
                        if (objOldBo.GBLND != objNewBo.GBLND)
                            lblValidateNewCountry.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewCountry.Text = objNewBo.GBLND;
                        if (objOldBo.CITY != objNewBo.CITY)
                            lblValidateNewCity.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewCity.Text = objNewBo.CITY;
                        if (objOldBo.ADDRESSL2 != objNewBo.ADDRESSL2)
                            lblValidateNewAddress2.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewAddress2.Text = objNewBo.ADDRESSL2;
                        if (objOldBo.ADDRESSL1 != objNewBo.ADDRESSL1)
                            lblValidateNewAddress1.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewAddress1.Text = objNewBo.ADDRESSL1;
                    }
                    else
                    {
                        lblValidateNewIDType.Text = objNewBo.ADDRESS_TYPE_ID.ToString();
                        lblValidateNewPhoneNo.Text = objNewBo.PHONENO;
                        lblValidateNewState.Text = objNewBo.STATE_ID;
                        lblValidateNewDateTo.Text = objNewBo.DATE_TO.ToString();
                        lblValidateNewDateFrom.Text = objNewBo.DATE_FROM.ToString();
                        lblValidateNewCountry.Text = objNewBo.GBLND;
                        lblValidateNewCity.Text = objNewBo.CITY;
                        lblValidateNewAddress2.Text = objNewBo.ADDRESSL2;
                        lblValidateNewAddress1.Text = objNewBo.ADDRESSL1;
                        txtAddressInfoComments.Text = objNewBo.COMMENTS;
                    }
                    break;
                case "Bank details approval":
                    BankDetailsControlStatus(true);
                    pibankinformationbo objBankInfoBo = new pibankinformationbo();
                    objBankInfoBo.PKEY = sPkey;
                    objBankInfoBo.APPROVED_BY = User.Identity.Name;
                    lblBINameValue.Text = sName;
                    lblBIEmailValue.Text = sEmailId;
                    pibankinformationcollectionbo objBankInfoLst = objPIDashBl.Get_Bank_Details_For_Approval(objBankInfoBo);
                    pibankinformationbo objOldBankBo = objBankInfoLst.Find(delegate(pibankinformationbo obj)
                    { return obj.ISAPPROVED == true; });
                    pibankinformationbo objNewBankBo = objBankInfoLst.Find(delegate(pibankinformationbo obj)
                    { return obj.ISAPPROVED == false; });
                    Session.Add("objBoBankDetails", objNewBankBo.PKEY);
                    if (objOldBankBo != null)
                    {
                        lblValidateBankType.Text = objOldBankBo.BANK_TYPE_ID.ToString();
                        lblValidatePayee.Text = objOldBankBo.PAYEE.ToString();
                        lblValidatePostalCode.Text = objOldBankBo.POSTAL_CODE + "-" + objOldBankBo.CITY;
                        lblValidateBankCountry.Text = objOldBankBo.COUNTRY_NAME;
                        lblValidateBankKey.Text = objOldBankBo.BANK_TYPE_NAME;
                        lblValidateBankAccount.Text = objOldBankBo.BANK_ACCOUNT;
                        lblValidatePaymentMethod.Text = objOldBankBo.PAYMENT_METHOD_NAME;
                        lblValidatePurpose.Text = objOldBankBo.PURPOSE;
                        lblValidateCurrency.Text = objOldBankBo.PAYMENT_CURRENCY_NAME;
                        txtBankComments.Text = "";

                        if (objOldBankBo.BANK_TYPE_ID != objNewBankBo.BANK_TYPE_ID)
                            lblValidateNewBankType.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewBankType.Text = objNewBankBo.BANK_TYPE_ID.ToString();
                        if (objOldBankBo.PAYEE != objNewBankBo.PAYEE)
                            lblValidateNewPayee.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewPayee.Text = objNewBankBo.PAYEE;
                        if (objOldBankBo.POSTAL_CODE != objNewBankBo.POSTAL_CODE)
                            lblValidateNewPostalCode.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewPostalCode.Text = objNewBankBo.POSTAL_CODE;
                        if (objOldBankBo.CITY != objNewBankBo.CITY)
                            lblValidateNewPostalCode.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewPostalCode.Text = objNewBankBo.CITY;
                        lblValidateNewPostalCode.Text = objNewBankBo.POSTAL_CODE + "-" + objNewBankBo.CITY;
                        if (objOldBankBo.COUNTRY_NAME != objNewBankBo.COUNTRY_NAME)
                            lblValidateNewBankCountry.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewBankCountry.Text = objNewBankBo.COUNTRY_NAME;
                        if (objOldBankBo.BANK_TYPE_NAME != objNewBankBo.BANK_TYPE_NAME)
                            lblValidateNewBankKey.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewBankKey.Text = objNewBankBo.BANK_TYPE_NAME;
                        if (objOldBankBo.BANK_ACCOUNT != objNewBankBo.BANK_ACCOUNT)
                            lblValidateNewBankAccount.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewBankAccount.Text = objNewBankBo.BANK_ACCOUNT;
                        if (objOldBankBo.PAYMENT_METHOD_NAME != objNewBankBo.PAYMENT_METHOD_NAME)
                            lblValidateNewPaymentMethod.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewPaymentMethod.Text = objNewBankBo.PAYMENT_METHOD_NAME;
                        if (objOldBankBo.PURPOSE != objNewBankBo.PURPOSE)
                            lblValidateNewPurpose.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewPurpose.Text = objNewBankBo.PURPOSE;
                        if (objOldBankBo.PAYMENT_CURRENCY_NAME != objNewBankBo.PAYMENT_CURRENCY_NAME)
                            lblValidateNewCurrency.ForeColor = System.Drawing.Color.Red;
                        lblValidateNewCurrency.Text = objNewBankBo.PAYMENT_CURRENCY_NAME;

                    }
                    else
                    {
                        lblValidateNewBankType.Text = objNewBankBo.BANK_TYPE_ID.ToString();
                        lblValidateNewPayee.Text = objNewBankBo.PAYEE.ToString();
                        lblValidateNewPostalCode.Text = objNewBankBo.POSTAL_CODE + "-" + objNewBankBo.CITY;
                        lblValidateNewBankCountry.Text = objNewBankBo.COUNTRY_NAME;
                        lblValidateNewBankKey.Text = objNewBankBo.BANK_TYPE_NAME;
                        lblValidateNewBankAccount.Text = objNewBankBo.BANK_ACCOUNT;
                        lblValidateNewPaymentMethod.Text = objNewBankBo.PAYMENT_METHOD_NAME;
                        lblValidateNewPurpose.Text = objNewBankBo.PURPOSE;
                        lblValidateNewCurrency.Text = objNewBankBo.PAYMENT_CURRENCY_NAME;
                        txtBankComments.Text = objNewBankBo.COMMENTS;
                    }
                    break;
                case "Family member details approval":
                    FamilyMemberDetailsControlStatus(true);
                    pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                    objFamilyBo.PKEY = sPkey;
                    lblFMNameValue.Text = sName;
                    lblFMEmail.Text = sEmailId;
                    objFamilyBo.ISAPPROVED = false;
                    objFamilyBo.APPROVED_BY = User.Identity.Name;
                    pifamilymemberscollectionbo objFamilylst = objPIDashBl.Get_FamilyMemberDetails_For_Approval(objFamilyBo);
                    pifamilymembersbo objOldFMBo = objFamilylst.Find(delegate(pifamilymembersbo obj)
                    { return obj.ISAPPROVED == true; });
                    pifamilymembersbo objNewFMBo = objFamilylst.Find(delegate(pifamilymembersbo obj)
                    { return obj.ISAPPROVED == false; });
                    Session.Add("objBoFMDetails", objNewFMBo.PKEY);
                    if (objOldFMBo != null)
                    {
                        lblValidateFMCOB.Text = objOldFMBo.FGBLD;
                        lblValidateFMDOB.Text = objOldFMBo.FGBDT.ToString();
                        lblValidateFMFirstName.Text = objOldFMBo.FAVOR;
                        lblValidateFMGender.Text = objOldFMBo.FASEX;
                        lblValidateFMInitials.Text = objOldFMBo.FINIT;
                        lblValidateFMLastName.Text = objOldFMBo.FANAM;
                        lblValidateFMMembertype.Text = objOldFMBo.FAMSA;
                        lblValidateFMNameOfBirth.Text = objOldFMBo.FGBNA;
                        lblValidateFMNamePrefix.Text = objOldFMBo.FVRSW;
                        lblValidateFMNationality.Text = objOldFMBo.FANAT;
                        lblValidateFMNationality2.Text = objOldFMBo.FANA2;
                        lblValidateFMNationality3.Text = objOldFMBo.FANA3;
                        lblValidateFMOtherTitle.Text = objOldFMBo.FNMZU;
                        lblValidateFMPOB.Text = objOldFMBo.FGBOT;
                        txtFMComments.Text = "";
                        if (objOldFMBo.FAMSA.Trim() == "Child")
                        {
                            pnlChildAllowance.Visible = true;
                            lblValidateHostelAllowance.Text = objOldFMBo.KDBGR;
                            lblValidateOtherAllowance.Text = objOldFMBo.KDBSL;
                            lblValidateEducationalAllowance.Text = objOldFMBo.KDZUL;
                        }
                        else
                        {
                            pnlChildAllowance.Visible = false;
                        }

                        if (objOldFMBo.FGBLD != objNewFMBo.FGBLD)
                            lblValidateNewFMCOB.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FGBDT != objNewFMBo.FGBDT)
                            lblValidateNewFMDOB.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FAVOR != objNewFMBo.FAVOR)
                            lblValidateNewFMFirstName.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FASEX != objNewFMBo.FASEX)
                            lblValidateNewFMGender.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FINIT != objNewFMBo.FINIT)
                            lblValidateNewFMInitials.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FANAM != objNewFMBo.FANAM)
                            lblValidateNewFMLastName.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FGBNA != objNewFMBo.FGBNA)
                            lblValidateNewFMNameOfBirth.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FVRSW != objNewFMBo.FVRSW)
                            lblValidateNewFMNamePrefix.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FANAT != objNewFMBo.FANAT)
                            lblValidateNewFMNationality.ForeColor = System.Drawing.Color.Red;

                        if (objOldFMBo.FANA2 != objNewFMBo.FANA2)
                            lblValidateNewFMNationality2.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FANA3 != objNewFMBo.FANA3)
                            lblValidateNewFMNationality3.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FNMZU != objNewFMBo.FNMZU)
                            lblValidateNewFMOtherTitle.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.FGBOT != objNewFMBo.FGBOT)
                            lblValidateNewFMPOB.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.KDBGR != objNewFMBo.KDBGR)
                            lblValidateNewHostelAllowance.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.KDBSL != objNewFMBo.KDBSL)
                            lblValidateNewOtherAllowance.ForeColor = System.Drawing.Color.Red;
                        if (objOldFMBo.KDZUL != objNewFMBo.KDZUL)
                            lblValidateNewEducationalAllowance.ForeColor = System.Drawing.Color.Red;
                    }


                    lblValidateNewFMCOB.Text = objNewFMBo.FGBLD;
                    lblValidateNewFMDOB.Text = objNewFMBo.FGBDT.ToString();
                    lblValidateNewFMFirstName.Text = objNewFMBo.FAVOR;
                    lblValidateNewFMGender.Text = objNewFMBo.FASEX;
                    lblValidateNewFMInitials.Text = objNewFMBo.FINIT;
                    lblValidateNewFMLastName.Text = objNewFMBo.FANAM;
                    lblValidateNewFMMembertype.Text = objNewFMBo.FAMSA;
                    lblValidateNewFMNameOfBirth.Text = objNewFMBo.FGBNA;
                    lblValidateNewFMNamePrefix.Text = objNewFMBo.FVRSW;
                    lblValidateNewFMNationality.Text = objNewFMBo.FANAT;
                    lblValidateNewFMNationality2.Text = objNewFMBo.FANA2;
                    lblValidateNewFMNationality3.Text = objNewFMBo.FANA3;
                    lblValidateNewFMOtherTitle.Text = objNewFMBo.FNMZU;
                    lblValidateNewFMPOB.Text = objNewFMBo.FGBOT;
                    txtFMComments.Text = objNewFMBo.COMMENTS;
                    if (objNewFMBo.FAMSA.Trim() == "Child")
                    {
                        pnlChildAllowance.Visible = true;
                        lblValidateNewHostelAllowance.Text = objNewFMBo.KDBGR;
                        lblValidateNewOtherAllowance.Text = objNewFMBo.KDBSL;
                        lblValidateNewEducationalAllowance.Text = objNewFMBo.KDZUL;
                    }
                    else
                    {
                        pnlChildAllowance.Visible = false;
                    }

                    break;
                case "Communication details approval":
                    CommmunicationDetailsControlStatus(true);
                    picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                    objCommuInfoBo.EMPLOYEE_ID = sPernr.ToString();
                    //objCommuInfoBo.pk = sPkey;
                    lblCINameValue.Text = sName;
                    //lblFMEmail.Text = sEmailId;
                    objCommuInfoBo.ISAPPROVED = false;
                    objCommuInfoBo.APPROVED_BY = User.Identity.Name;

                    picommunicationinformationcollectionbo objCommuInfoLst = objPIDashBl.Get_Communication_Details_For_Approval(objCommuInfoBo);
                    var OldLst = from col in objCommuInfoLst
                                 where col.ISAPPROVED == true
                                 select col;
                    var NewLst = from col in objCommuInfoLst
                                 where col.ISAPPROVED == false
                                 select col;
                    picommunicationinformationbo objOldCMBo = OldLst.ToList().Find(delegate(picommunicationinformationbo obj)
                    { return obj.ISAPPROVED == true; });
                    picommunicationinformationbo objNewCMBo = NewLst.ToList().Find(delegate(picommunicationinformationbo obj)
                    { return obj.ISAPPROVED == false; });

                    Session.Add("objCOmmuPernrBO", sPernr);
                    foreach (picommunicationinformationbo objNewCommunicationInfoBo in NewLst.ToList())
                    {
                        if (objNewCommunicationInfoBo.USRTY == "0002")
                        {
                            lblValidateNewBuildingNumber.Text = objNewCommunicationInfoBo.BUILDING_NO;
                        } if (objNewCommunicationInfoBo.USRTY == "0003")
                        {
                            lblValidateNewRoomNumber.Text = objNewCommunicationInfoBo.ROOM_NO;
                        }
                        if (objNewCommunicationInfoBo.USRTY == "0004")
                        {

                            lblValidateNewExtension.Text = objNewCommunicationInfoBo.EXTENSION;
                        }
                        if (objNewCommunicationInfoBo.USRTY == "0010")
                        {

                            lblValidateNewEmail.Text = objNewCommunicationInfoBo.EMAIL;
                        }
                        if (objNewCommunicationInfoBo.USRTY == "0006")
                        {

                            lblValidateNewLicencePlateNumber.Text = objNewCommunicationInfoBo.LICENCE_NO;
                        }
                        if (objNewCommunicationInfoBo.MPHN == "MPHN")
                        {

                            lblValidateNewMobileNo.Text = objNewCommunicationInfoBo.MPHN_ID;
                        }
                        txtCommunicationInfoCommnets.Text = objNewCommunicationInfoBo.COMMENTS;

                    }
                    if (objOldCMBo != null)
                    {
                        foreach (picommunicationinformationbo objOldCommunicationInfoBo in OldLst.ToList())
                        {
                            if (objOldCommunicationInfoBo.USRTY == "0002")
                            {
                                lblValidateBuildingNumber.Text = objOldCommunicationInfoBo.BUILDING_NO;
                            } if (objOldCommunicationInfoBo.USRTY == "0003")
                            {
                                lblValidateRoomNumber.Text = objOldCommunicationInfoBo.ROOM_NO;
                            }
                            if (objOldCommunicationInfoBo.USRTY == "0004")
                            {

                                lblValidateExtension.Text = objOldCommunicationInfoBo.EXTENSION;
                            }
                            if (objOldCommunicationInfoBo.USRTY == "0010")
                            {

                                lblValidateEmail.Text = objOldCommunicationInfoBo.EMAIL;
                            }
                            if (objOldCommunicationInfoBo.USRTY == "0006")
                            {

                                lblValidateLicencePlateNumber.Text = objOldCommunicationInfoBo.LICENCE_NO;
                            }
                            if (objOldCommunicationInfoBo.MPHN == "MPHN")
                            {

                                lblValidateMobileNo.Text = objOldCommunicationInfoBo.MPHN_ID;
                            }
                            txtCommunicationInfoCommnets.Text = "";

                        }
                        if (lblValidateNewBuildingNumber.Text != lblValidateBuildingNumber.Text)
                            lblValidateNewBuildingNumber.ForeColor = System.Drawing.Color.Red;
                        if (lblValidateNewRoomNumber.Text != lblValidateRoomNumber.Text)
                            lblValidateNewRoomNumber.ForeColor = System.Drawing.Color.Red;
                        if (lblValidateNewExtension.Text != lblValidateExtension.Text)
                            lblValidateNewExtension.ForeColor = System.Drawing.Color.Red;
                        if (lblValidateNewEmail.Text != lblValidateEmail.Text)
                            lblValidateNewEmail.ForeColor = System.Drawing.Color.Red;
                        if (lblValidateNewLicencePlateNumber.Text != lblValidateLicencePlateNumber.Text)
                            lblValidateNewLicencePlateNumber.ForeColor = System.Drawing.Color.Red;
                        if (lblValidateNewMobileNo.Text != lblValidateMobileNo.Text)
                            lblValidateNewMobileNo.ForeColor = System.Drawing.Color.Red;
                    }

                    break;
                case "Personal data details approval":
                    PersonalDataDetailsControlStatus(true);
                    personaldatabo objPersonaldataBo = new personaldatabo();
                    objPersonaldataBo.EMPLOYEE_ID = (sPernr);
                    lblPDINameValue.Text = sName;
                    lblPDIEmailValue.Text = sEmailId;
                    objPersonaldataBo.ISAPPROVED = false;
                    objPersonaldataBo.APPROVED_BY = User.Identity.Name;

                    personaldatacollectionbo objPersonaldataList = objPIDashBl.Get_PersonalData_Details_For_Approval(objPersonaldataBo);
                    personaldatabo objOldPDaBo = objPersonaldataList.Find(delegate(personaldatabo obj)
                    { return obj.ISAPPROVED == true; });
                    personaldatabo objNewPDaBo = objPersonaldataList.Find(delegate(personaldatabo obj)
                    { return obj.ISAPPROVED == false; });
                    Session.Add("objPDBo", sPernr);
                    if (objOldPDaBo != null)
                    {
                        lblValidatePDTitle.Text = objOldPDaBo.TITEL;
                        lblValidatePDFirstName.Text = objOldPDaBo.VORNA;
                        lblValidatePDLastName.Text = objOldPDaBo.NACHN;
                        lblValidatePDBirthName.Text = objOldPDaBo.NAME2;
                        lblValidatePDInitials.Text = objOldPDaBo.INITS;
                        lblValidatePDKnownAs.Text = objOldPDaBo.RUFNM;
                        lblValidatePDLanguage.Text = objOldPDaBo.SPRSL;
                        lblValidatePDGender.Text = objOldPDaBo.GESCH;
                        lblValidatePDDOB.Text = objOldPDaBo.GBDAT.ToString();
                        lblValidatePDPOB.Text = objOldPDaBo.GBORT;
                        lblValidatePDCOB.Text = objOldPDaBo.GBLND;
                        lblValidatePDNationality.Text = objOldPDaBo.NATIO;
                        lblValidatePDState.Text = objOldPDaBo.GBDEP;
                        lblValidatePD2ndNtnlty.Text = objOldPDaBo.NATI2;
                        lblValidatePD3rdNtnlty.Text = objOldPDaBo.NATI3;
                        lblValidatePDMaritalStatus.Text = objOldPDaBo.FAMST;
                        lblValidatePDMarriedSince.Text = objOldPDaBo.FAMDT.ToString();
                        lblValidatePDNoOfChildren.Text = objOldPDaBo.ANZKD.ToString();
                        lblValidatePDReligion.Text = objOldPDaBo.KITXT;
                        txtPDCommnets.Text = "";


                        if (objOldPDaBo.TITEL != objNewPDaBo.TITEL)
                            lblValidateNewPDTitle.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.VORNA != objNewPDaBo.VORNA)
                            lblValidateNewPDFirstName.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.NACHN != objNewPDaBo.NACHN)
                            lblValidateNewPDLastName.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.NAME2 != objNewPDaBo.NAME2)
                            lblValidateNewPDBirthName.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.INITS != objNewPDaBo.INITS)
                            lblValidateNewPDInitials.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.RUFNM != objNewPDaBo.RUFNM)
                            lblValidateNewPDKnownAs.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.SPRSL != objNewPDaBo.SPRSL)
                            lblValidateNewPDLanguage.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.GESCH != objNewPDaBo.GESCH)
                            lblValidateNewPDGender.ForeColor = System.Drawing.Color.Red;

                        if (objOldPDaBo.GBDAT != objNewPDaBo.GBDAT)
                            lblValidateNewPDDOB.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.GBORT != objNewPDaBo.GBORT)
                            lblValidateNewPDPOB.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.GBLND != objNewPDaBo.GBLND)
                            lblValidateNewPDCOB.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.NATIO != objNewPDaBo.NATIO)
                            lblValidateNewPDNationality.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.GBDEP != objNewPDaBo.GBDEP)
                            lblValidateNewPDState.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.NATI2 != objNewPDaBo.NATI2)
                            lblValidateNewPD2ndNtnlty.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.NATI3 != objNewPDaBo.NATI3)
                            lblValidateNewPD3rdNtnlty.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.FAMST != objNewPDaBo.FAMST)
                            lblValidateNewPDMaritalStatus.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.FAMDT != objNewPDaBo.FAMDT)
                            lblValidateNewPDMarriedSince.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.ANZKD != objNewPDaBo.ANZKD)
                            lblValidateNewPDNoOfChildren.ForeColor = System.Drawing.Color.Red;
                        if (objOldPDaBo.KITXT != objNewPDaBo.KITXT)
                            lblValidateNewPDReligion.ForeColor = System.Drawing.Color.Red;
                    }

                    lblValidateNewPDTitle.Text = objNewPDaBo.TITEL;
                    lblValidateNewPDFirstName.Text = objNewPDaBo.VORNA;
                    lblValidateNewPDLastName.Text = objNewPDaBo.NACHN;
                    lblValidateNewPDBirthName.Text = objNewPDaBo.NAME2;
                    lblValidateNewPDInitials.Text = objNewPDaBo.INITS;
                    lblValidateNewPDKnownAs.Text = objNewPDaBo.RUFNM;
                    lblValidateNewPDLanguage.Text = objNewPDaBo.SPRSL;
                    lblValidateNewPDGender.Text = objNewPDaBo.GESCH;
                    lblValidateNewPDDOB.Text = objNewPDaBo.GBDAT.ToString();
                    lblValidateNewPDPOB.Text = objNewPDaBo.GBORT;
                    lblValidateNewPDCOB.Text = objNewPDaBo.GBLND;
                    lblValidateNewPDNationality.Text = objNewPDaBo.NATIO;
                    lblValidateNewPDState.Text = objNewPDaBo.GBDEP;
                    lblValidateNewPD2ndNtnlty.Text = objNewPDaBo.NATI2;
                    lblValidateNewPD3rdNtnlty.Text = objNewPDaBo.NATI3;
                    lblValidateNewPDMaritalStatus.Text = objNewPDaBo.FAMST;
                    lblValidateNewPDMarriedSince.Text = objNewPDaBo.FAMDT.ToString();
                    lblValidateNewPDNoOfChildren.Text = objNewPDaBo.ANZKD.ToString();
                    lblValidateNewPDReligion.Text = objNewPDaBo.KITXT;
                    txtPDCommnets.Text = objNewPDaBo.COMMENTS;

                    break;
                case "Personal id details approval":
                    PersonalIDsDetailsControlStatus(true);
                    pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                    objPersonalIDsBo.PERNR = sPernr.ToString();
                    lblPIdInfoNameValue.Text = sName;
                    lblPIdInfoEmailValue.Text = sEmailId;
                    objPersonalIDsBo.ISAPPROVED = false;
                    objPersonalIDsBo.ICTYPE = sPkey;
                    objPersonalIDsBo.APPROVED_BY = User.Identity.Name;

                    pipersonalidscollectionbo objPersonalIDsLst = objPIDashBl.Get_PersonalIDS_Details_For_Approval(objPersonalIDsBo);
                    pipersonalidsbo objOldPIDBo = objPersonalIDsLst.Find(delegate(pipersonalidsbo obj)
                    { return obj.ISAPPROVED == true; });
                    pipersonalidsbo objNewPIDBo = objPersonalIDsLst.Find(delegate(pipersonalidsbo obj)
                    { return obj.ISAPPROVED == false; });
                    Session.Add("objPIDBo", objNewPIDBo.ICTYPE);
                    Session.Add("sPIDPernr", sPernr);
                    if (objOldPIDBo != null)
                    {
                        lblValidateIDype.Text = objOldPIDBo.DESCRIPTION.Trim();
                        lblValidateIDNo.Text = objOldPIDBo.ICNUM;
                        txtPIComments.Text = "";

                        if (objOldPIDBo.DESCRIPTION != objNewPIDBo.DESCRIPTION)
                            lblValidateNewIDype.ForeColor = System.Drawing.Color.Red;
                        if (objOldPIDBo.ICNUM != objNewPIDBo.ICNUM)
                            lblValidateNewIDNo.ForeColor = System.Drawing.Color.Red;

                    }

                    lblValidateNewIDype.Text = objNewPIDBo.DESCRIPTION.Trim();
                    lblValidateNewIDNo.Text = objNewPIDBo.ICNUM;
                    txtPIComments.Text = objNewPIDBo.COMMENTS;
                    break;
                case "Clockin out details approval":
                    ClockIODetailsControlStatus(true);
                    wtclockinoutcorrectionbo objClockBo = new wtclockinoutcorrectionbo();
                    objClockBo.PKEY = sPkey;
                    lblCIONameValue.Text = sName;
                    lblCIOEmailValue.Text = sEmailId;
                    objClockBo.ISAPPROVED = false;
                    objClockBo.APPROVEDBY = User.Identity.Name;

                    wtclockinoutcorrectioncollectionbo objClockLst = objPIDashBl.Get_ClockIO_Details_For_Approval(objClockBo);
                    wtclockinoutcorrectionbo objOldClockinBo = objClockLst.Find(delegate(wtclockinoutcorrectionbo obj)
                    { return obj.ISAPPROVED == true; });
                    wtclockinoutcorrectionbo objNewClockinBo = objClockLst.Find(delegate(wtclockinoutcorrectionbo obj)
                    { return obj.ISAPPROVED == false; });
                    Session.Add("sIOPkey", objNewClockinBo.PKEY);
                    Session.Add("sEntryStatus", objNewClockinBo.ENTRY_STATUS);
                    if (objOldClockinBo != null)
                    {
                        lblValidateRequestStatus.Text = objOldClockinBo.ENTRY_STATUS;
                        lblValidateDate.Text = objOldClockinBo.LDATE.ToString();
                        lblValidateTime.Text = objOldClockinBo.LTIME;
                        lblValidateClockInOut.Text = objOldClockinBo.SATZA_TYPE;
                        lblValidateApprovedBy.Text = objOldClockinBo.APPROVEDBY;
                        lblValidateNote.Text = objOldClockinBo.NOTE;
                        txtClockInOutComments.Text = "";
                        if (objOldClockinBo.ENTRY_STATUS != objNewClockinBo.ENTRY_STATUS)
                            lblValidateNewRequestStatus.ForeColor = System.Drawing.Color.Red;
                        if (objOldClockinBo.LDATE != objNewClockinBo.LDATE)
                            lblValidateNewDate.ForeColor = System.Drawing.Color.Red;
                        if (objOldClockinBo.LTIME != objNewClockinBo.LTIME)
                            lblValidateNewTime.ForeColor = System.Drawing.Color.Red;
                        if (objOldClockinBo.SATZA_TYPE != objNewClockinBo.SATZA_TYPE)
                            lblValidateNewClockInOut.ForeColor = System.Drawing.Color.Red;
                        if (objOldClockinBo.APPROVEDBY != objNewClockinBo.APPROVEDBY)
                            lblValidateNewApprovedBy.ForeColor = System.Drawing.Color.Red;
                        if (objOldClockinBo.NOTE != objNewClockinBo.NOTE)
                            lblValidateNewNote.ForeColor = System.Drawing.Color.Red;
                    }
                    lblValidateNewRequestStatus.Text = objNewClockinBo.ENTRY_STATUS;
                    lblValidateNewDate.Text = objNewClockinBo.LDATE.ToString();
                    lblValidateNewTime.Text = objNewClockinBo.LTIME;
                    lblValidateNewClockInOut.Text = objNewClockinBo.SATZA_TYPE;
                    lblValidateNewApprovedBy.Text = objNewClockinBo.APPROVEDBY;
                    lblValidateNewNote.Text = objNewClockinBo.NOTE;
                    txtClockInOutComments.Text = objNewClockinBo.COMMENTS;
                    break;
                case "Leave request details approval":
                    //pnlLeaveRequest.Visible = true;
                    LeaveRequestDetailsControlStatus(true);
                    leaverequestbo objLeaveRequestBo = new leaverequestbo();
                    objLeaveRequestBo.PKEY = sPkey;
                    lblLRNameValue.Text = sName;
                    lblLREmailValue.Text = sEmailId;
                    pidashboardbl objLeaveRequestBl = new pidashboardbl();
                    leaverequestcollectionbo objLeaveReqLst = objLeaveRequestBl.Get_LeaveRequest_Details_For_Approval(objLeaveRequestBo);
                    leaverequestbo objLeaveRqstBo = objLeaveReqLst.Find(delegate(leaverequestbo obj)
                    { return true; });
                    Session.Add("LeaveReqId", sPkey);
                    lblValidateTypeOfLeave.Text = objLeaveRqstBo.ATEXT.ToString();
                    lblValidateFromDate.Text = objLeaveRqstBo.BEGDA.ToString();
                    lblValidateToDate.Text = objLeaveRqstBo.ENDDA.ToString();
                    lblValidateFromTime.Text = objLeaveRqstBo.BEGUZ;
                    lblValidateToTime.Text = objLeaveRqstBo.ENDUZ;
                    lblValidateDuration.Text = objLeaveRqstBo.STDAZ.ToString();
                    lblValidateApprover.Text = objLeaveRqstBo.APPROVED_BY_NAME.ToString();
                    lblValidateNoteForApprover.Text = objLeaveRqstBo.NOTE;
                    break;
                case "Recordworking time details approval":
                    RecordWorkingDetailsControlStatus(true);
                    wtrecordworkingtimebo objRecordBo = new wtrecordworkingtimebo();
                    objRecordBo.PKEY = sPkey;
                    lblRWNameValue.Text = sName;
                    lblRWEmailValue.Text = sEmailId;
                    objRecordBo.ISAPPROVED = false;
                    objRecordBo.APPROVEDBY = User.Identity.Name;

                    wtrecordworkingtimecollectionbo objRecordLst = objPIDashBl.Get_RecordDetails_For_Approval(objRecordBo);
                    wtrecordworkingtimebo objOldRWBo = objRecordLst.Find(delegate(wtrecordworkingtimebo obj)
                    { return obj.ISAPPROVED == true; });
                    wtrecordworkingtimebo objNewRWBo = objRecordLst.Find(delegate(wtrecordworkingtimebo obj)
                    { return obj.ISAPPROVED == false; });

                    Session.Add("sRWkey", objNewRWBo.PKEY);
                    if (objOldRWBo != null)
                    {
                        lblValidateRWType.Text = objOldRWBo.AWART.ToString();
                        lblValidateRWDate.Text = objOldRWBo.WORKING_DATE;
                        lblValidateRWHours.Text = objOldRWBo.CATSHOURS;
                        lblValidateRWCostCenter.Text = objOldRWBo.LTEXT;
                        lblValidateRWOrder.Text = objOldRWBo.KTEXT;
                        txtRWComments.Text = "";

                        if (objOldRWBo.AWART != objNewRWBo.AWART)
                            lblValidateNewRWType.ForeColor = System.Drawing.Color.Red;
                        if (objOldRWBo.WORKING_DATE != objNewRWBo.WORKING_DATE)
                            lblValidateNewRWDate.ForeColor = System.Drawing.Color.Red;
                        if (objOldRWBo.CATSHOURS != objNewRWBo.CATSHOURS)
                            lblValidateNewRWHours.ForeColor = System.Drawing.Color.Red;
                        if (objOldRWBo.KTEXT != objNewRWBo.KTEXT)
                            lblValidateNewRWOrder.ForeColor = System.Drawing.Color.Red;
                        if (objOldRWBo.LTEXT != objNewRWBo.LTEXT)
                            lblValidateNewRWCostCenter.ForeColor = System.Drawing.Color.Red;
                    }

                    lblValidateNewRWType.Text = objNewRWBo.AWART.ToString();
                    lblValidateNewRWDate.Text = objNewRWBo.WORKING_DATE;
                    lblValidateNewRWHours.Text = objNewRWBo.CATSHOURS;
                    lblValidateNewRWCostCenter.Text = objNewRWBo.LTEXT;
                    lblValidateNewRWOrder.Text = objNewRWBo.KTEXT;
                    txtRWComments.Text = objNewRWBo.COMMENTS;
                    break;
                default:

                    break;
            }
        }
        catch (Exception)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            AllPnelStatus();
            return;
        }
    }
    protected void grdCompleted_SelectedIndexChanged(object sender, EventArgs e)
    {
        AllPnelStatus();
        GridViewRow grdRow = grdCompleted.SelectedRow;
        Session.Add("currentSelectedRow", grdRow);
        pidashboardbl objPIDashBl = new pidashboardbl();
        string sName = grdRow.Cells[1].Text;
        string sEmailId = grdRow.Cells[0].Text;
        string sPernr = grdRow.Cells[3].Text;
        string sPkey = grdRow.Cells[2].Text;
        string sApprovalType = grdRow.Cells[4].Text;
        DateTime dtLateDate = DateTime.Parse(grdRow.Cells[6].Text);
        try
        {
            switch (sApprovalType)
            {
                case "Address change approval":
                    AddressControlStatus(false);
                    piaddressinformationbo objPIAddBo = new piaddressinformationbo();
                    objPIAddBo.PKEY = sPkey;
                    lblAINameValue.Text = sName;
                    lblAIEmailValue.Text = sEmailId;
                    objPIAddBo.APPROVED_BY = User.Identity.Name;
                    piaddressinformationcollectionbo objPIAddBoLst = objPIDashBl.Get_Address_completed_Details_For_Approval(objPIAddBo);
                    piaddressinformationbo objBo = objPIAddBoLst.Find(delegate(piaddressinformationbo obj)
                    { return true; });
                    lblValidateIDType.Text = objBo.ADDRESS_TYPE_ID.ToString();
                    lblValidatePhoneNo.Text = objBo.PHONENO;
                    lblValidateState.Text = objBo.STATE_ID;
                    lblValidateDateTo.Text = objBo.DATE_TO.ToString();
                    lblValidateDateFrom.Text = objBo.DATE_FROM.ToString();
                    lblValidateCountry.Text = objBo.GBLND;
                    lblValidateCity.Text = objBo.CITY;
                    lblValidateAddress2.Text = objBo.ADDRESSL2;
                    lblValidateAddress1.Text = objBo.ADDRESSL1;
                    lblValidateAddressInfoComments.Text = objBo.COMMENTS;
                    break;
                case "Bank details approval":
                    BankDetailsControlStatus(false);
                    pibankinformationbo objBankInfoBo = new pibankinformationbo();
                    objBankInfoBo.PKEY = sPkey;
                    lblBINameValue.Text = sName;
                    lblBIEmailValue.Text = sEmailId;
                    objBankInfoBo.APPROVED_BY = User.Identity.Name;
                    pibankinformationcollectionbo objBankInfoLst = objPIDashBl.Get_Bank_completed_Details_For_Approval(objBankInfoBo);
                    pibankinformationbo objBankBo = objBankInfoLst.Find(delegate(pibankinformationbo obj)
                    { return true; });
                    lblValidateBankType.Text = objBankBo.BANK_TYPE_ID.ToString();
                    lblValidatePayee.Text = objBankBo.PAYEE.ToString();
                    lblValidatePostalCode.Text = objBankBo.POSTAL_CODE + "-" + objBankBo.CITY;
                    lblValidateBankCountry.Text = objBankBo.COUNTRY_NAME;
                    lblValidateBankKey.Text = objBankBo.BANK_TYPE_NAME;
                    lblValidateBankAccount.Text = objBankBo.BANK_ACCOUNT;
                    lblValidatePaymentMethod.Text = objBankBo.PAYMENT_METHOD_NAME;
                    lblValidatePurpose.Text = objBankBo.PURPOSE;
                    lblValidateCurrency.Text = objBankBo.PAYMENT_CURRENCY_NAME;
                    lblValidateBankComments.Text = objBankBo.COMMENTS;
                    break;
                case "Family member details approval":
                    FamilyMemberDetailsControlStatus(false);
                    pifamilymembersbo objFamilyBo = new pifamilymembersbo();
                    objFamilyBo.PKEY = sPkey;
                    lblFMNameValue.Text = sName;
                    lblFMEmail.Text = sEmailId;
                    objFamilyBo.ISAPPROVED = true;
                    objFamilyBo.APPROVED_BY = User.Identity.Name;
                    pifamilymemberscollectionbo objFamilylst = objPIDashBl.Get_FamilyMemberDetails_For_Approval(objFamilyBo);
                    pifamilymembersbo objFMBo = objFamilylst.Find(delegate(pifamilymembersbo obj)
                    { return true; });
                    lblValidateFMCOB.Text = objFMBo.FGBLD;
                    lblValidateFMDOB.Text = objFMBo.FGBDT.ToString();
                    lblValidateFMFirstName.Text = objFMBo.FAVOR;
                    lblValidateFMGender.Text = objFMBo.FASEX;
                    lblValidateFMInitials.Text = objFMBo.FINIT;
                    lblValidateFMLastName.Text = objFMBo.FANAM;
                    lblValidateFMMembertype.Text = objFMBo.FAMSA;
                    lblValidateFMNameOfBirth.Text = objFMBo.FGBNA;
                    lblValidateFMNamePrefix.Text = objFMBo.FVRSW;
                    lblValidateFMNationality.Text = objFMBo.FANAT;
                    lblValidateFMNationality2.Text = objFMBo.FANA2;
                    lblValidateFMNationality3.Text = objFMBo.FANA3;
                    lblValidateFMOtherTitle.Text = objFMBo.FNMZU;
                    lblValidateFMPOB.Text = objFMBo.FGBOT;
                    if (objFMBo.FAMSA.Trim() == "Child")
                    {
                        pnlChildAllowance.Visible = true;
                        lblValidateHostelAllowance.Text = objFMBo.KDBGR;
                        lblValidateOtherAllowance.Text = objFMBo.KDBSL;
                        lblValidateEducationalAllowance.Text = objFMBo.KDZUL;
                    }
                    else
                    {
                        pnlChildAllowance.Visible = false;
                    }
                    lblValidateFMComments.Text = objFMBo.COMMENTS;
                    break;
                case "Communication details approval":
                    CommmunicationDetailsControlStatus(false);
                    picommunicationinformationbo objCommuInfoBo = new picommunicationinformationbo();
                    objCommuInfoBo.EMPLOYEE_ID = sPernr.ToString();
                    objCommuInfoBo.USR2 = sPkey;
                    lblCINameValue.Text = sName;
                    //lblFMEmail.Text = sEmailId;
                    objCommuInfoBo.ISAPPROVED = true;
                    objCommuInfoBo.APPROVED_BY = User.Identity.Name;
                    picommunicationinformationcollectionbo objCommuInfoLst = objPIDashBl.Get_Communication_Details_For_Approval(objCommuInfoBo);

                    foreach (picommunicationinformationbo objCommunicationInfoBo in objCommuInfoLst)
                    {
                        if (objCommunicationInfoBo.USRTY == "0002")
                        {
                            lblValidateBuildingNumber.Text = objCommunicationInfoBo.BUILDING_NO;
                        } if (objCommunicationInfoBo.USRTY == "0003")
                        {
                            lblValidateRoomNumber.Text = objCommunicationInfoBo.ROOM_NO;
                        }
                        if (objCommunicationInfoBo.USRTY == "0004")
                        {
                            lblValidateExtension.Text = objCommunicationInfoBo.EXTENSION;
                        }
                        if (objCommunicationInfoBo.USRTY == "0010")
                        {
                            lblValidateEmail.Text = objCommunicationInfoBo.EMAIL;
                        }
                        if (objCommunicationInfoBo.USRTY == "0006")
                        {
                            lblValidateLicencePlateNumber.Text = objCommunicationInfoBo.LICENCE_NO;
                        }
                        if (objCommunicationInfoBo.MPHN == "MPHN")
                        {
                            lblValidateMobileNo.Text = objCommunicationInfoBo.MPHN_ID;
                        }
                        lblValidateCommunicationInfoCommnets.Text = objCommunicationInfoBo.COMMENTS;

                    }
                    break;
                case "Personal data details approval":
                    PersonalDataDetailsControlStatus(false);
                    personaldatabo objPersonaldataBo = new personaldatabo();
                    objPersonaldataBo.EMPLOYEE_ID = sPernr;
                    lblPDINameValue.Text = sName;
                    lblPDIEmailValue.Text = sEmailId;
                    objPersonaldataBo.ISAPPROVED = true;
                    objPersonaldataBo.APPROVED_BY = User.Identity.Name;
                    personaldatacollectionbo objPersonaldataList = objPIDashBl.Get_PersonalData_Details_For_Approval(objPersonaldataBo);
                    personaldatabo objPerdataBo = objPersonaldataList.Find(delegate(personaldatabo obj)
                    { return true; });
                    lblValidatePDTitle.Text = objPerdataBo.TITEL;
                    lblValidatePDFirstName.Text = objPerdataBo.VORNA;
                    lblValidatePDLastName.Text = objPerdataBo.NACHN;
                    lblValidatePDBirthName.Text = objPerdataBo.NAME2;
                    lblValidatePDInitials.Text = objPerdataBo.INITS;
                    lblValidatePDKnownAs.Text = objPerdataBo.RUFNM;
                    lblValidatePDLanguage.Text = objPerdataBo.SPRSL;
                    lblValidatePDGender.Text = objPerdataBo.GESCH;
                    lblValidatePDDOB.Text = objPerdataBo.GBDAT.ToString();
                    lblValidatePDPOB.Text = objPerdataBo.GBORT;
                    lblValidatePDCOB.Text = objPerdataBo.GBLND;
                    lblValidatePDNationality.Text = objPerdataBo.NATIO;
                    lblValidatePDState.Text = objPerdataBo.GBDEP;
                    lblValidatePD2ndNtnlty.Text = objPerdataBo.NATI2;
                    lblValidatePD3rdNtnlty.Text = objPerdataBo.NATI3;
                    lblValidatePDMaritalStatus.Text = objPerdataBo.FAMST;
                    lblValidatePDMarriedSince.Text = objPerdataBo.FAMDT.ToString();
                    lblValidatePDNoOfChildren.Text = objPerdataBo.ANZKD.ToString();
                    lblValidatePDReligion.Text = objPerdataBo.KITXT;
                    lblValidatePDCommnets.Text = objPerdataBo.COMMENTS;
                    break;
                case "Personal id details approval":
                    PersonalIDsDetailsControlStatus(false);
                    pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
                    objPersonalIDsBo.PERNR = sPernr.ToString();
                    lblPIdInfoNameValue.Text = sName;
                    lblPIdInfoEmailValue.Text = sEmailId;
                    objPersonalIDsBo.ISAPPROVED = true;
                    objPersonalIDsBo.ICTYPE = sPkey;
                    objPersonalIDsBo.APPROVED_BY = User.Identity.Name;
                    pipersonalidscollectionbo objPersonalIDsLst = objPIDashBl.Get_PersonalIDS_Details_For_Approval(objPersonalIDsBo);
                    pipersonalidsbo objPIDBo = objPersonalIDsLst.Find(delegate(pipersonalidsbo obj)
                    { return true; });
                    lblValidateIDype.Text = objPIDBo.DESCRIPTION.Trim();
                    lblValidateIDNo.Text = objPIDBo.ICNUM;
                    lblValidatePIComments.Text = objPIDBo.COMMENTS;
                    break;
                case "Clockin out details approval":
                    ClockIODetailsControlStatus(false);
                    wtclockinoutcorrectionbo objClockBo = new wtclockinoutcorrectionbo();
                    objClockBo.PKEY = sPkey;
                    lblCIONameValue.Text = sName;
                    lblCIOEmailValue.Text = sEmailId;
                    objClockBo.ISAPPROVED = true;
                    objClockBo.APPROVEDBY = User.Identity.Name;
                    wtclockinoutcorrectioncollectionbo objClockLst = objPIDashBl.Get_ClockIO_Details_For_Approval(objClockBo);
                    wtclockinoutcorrectionbo objClockinBo = objClockLst.Find(delegate(wtclockinoutcorrectionbo obj)
                    { return true; });
                    lblValidateRequestStatus.Text = objClockinBo.ENTRY_STATUS;
                    lblValidateDate.Text = objClockinBo.LDATE.ToString();
                    lblValidateTime.Text = objClockinBo.LTIME;
                    lblValidateClockInOut.Text = objClockinBo.SATZA_TYPE;
                    lblValidateApprovedBy.Text = objClockinBo.APPROVEDBY;
                    lblValidateNote.Text = objClockinBo.NOTE;
                    lblValidateClockInOutComments.Text = objClockinBo.COMMENTS;
                    break;
                case "Leave request details approval":
                    //pnlLeaveRequest.Visible = true;
                    LeaveRequestDetailsControlStatus(false);
                    leaverequestbo objLeaveRequestBo = new leaverequestbo();
                    //objLeaveRequestBo.PERNR = Int64.Parse(sPernr);
                    objLeaveRequestBo.PKEY = sPkey;
                    lblLRNameValue.Text = sName;
                    lblLREmailValue.Text = sEmailId;
                    pidashboardbl objLeaveRequestBl = new pidashboardbl();
                    leaverequestcollectionbo objLeaveReqLst = objLeaveRequestBl.Get_LeaveRequest_Details_For_Approval(objLeaveRequestBo);
                    leaverequestbo objLeaveRqstBo = objLeaveReqLst.Find(delegate(leaverequestbo obj)
                    { return true; });
                    Session.Add("LeaveReqId", sPkey);
                    lblValidateTypeOfLeave.Text = objLeaveRqstBo.ATEXT.ToString();
                    lblValidateFromDate.Text = objLeaveRqstBo.BEGDA.ToString();
                    lblValidateToDate.Text = objLeaveRqstBo.ENDDA.ToString();
                    lblValidateFromTime.Text = objLeaveRqstBo.BEGUZ;
                    lblValidateToTime.Text = objLeaveRqstBo.ENDUZ;
                    lblValidateDuration.Text = objLeaveRqstBo.STDAZ.ToString();
                    lblValidateApprover.Text = objLeaveRqstBo.APPROVED_BY.ToString();
                    lblValidateNoteForApprover.Text = objLeaveRqstBo.NOTE;
                    break;
                case "Recordworking time details approval":
                    RecordWorkingDetailsControlStatus(false);
                    wtrecordworkingtimebo objRecordBo = new wtrecordworkingtimebo();
                    objRecordBo.PKEY = sPkey;
                    lblRWNameValue.Text = sName;
                    lblRWEmailValue.Text = sEmailId;
                    objRecordBo.ISAPPROVED = true;
                    objRecordBo.APPROVEDBY = User.Identity.Name;

                    wtrecordworkingtimecollectionbo objRecordLst = objPIDashBl.Get_RecordDetails_For_Approval(objRecordBo);
                    wtrecordworkingtimebo objRWBo = objRecordLst.Find(delegate(wtrecordworkingtimebo obj)
                    { return true; });
                    lblValidateRWType.Text = objRWBo.AWART.ToString();
                    lblValidateRWDate.Text = objRWBo.WORKING_DATE;
                    lblValidateRWHours.Text = objRWBo.CATSHOURS;
                    lblValidateRWOrder.Text = objRWBo.KTEXT;
                    lblValidateRWCostCenter.Text = objRWBo.LTEXT;
                    lblValidateRWCommnets.Text = objRWBo.COMMENTS;
                    break;
                default:

                    break;
            }
        }
        catch (Exception)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            AllPnelStatus();
            return;
        }
    }
    protected void grdCompleted_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdCompleted, "Select$" + e.Row.RowIndex);
        }
    }
    protected void grdPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdPending, "Select$" + e.Row.RowIndex);
        }
    }
    protected void btnAddressInfoApprove_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        Approve_Reject_AddressDetails(bIStatus);
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        Approve_Reject_AddressDetails(bIStatus);
    }
    protected void Approve_Reject_AddressDetails(bool bIStatus)
    {
        msassignedtomebo objPIAddBo = new msassignedtomebo();
        string sPkey = (string)Session["objBoAddressDetails"];
        objPIAddBo.APPROVED_BY = User.Identity.Name;
        objPIAddBo.PKEY = sPkey;
        objPIAddBo.APPROVED_ON = DateTime.Now;
        objPIAddBo.COMMENTS = txtAddressInfoComments.Text.Trim();
        objPIAddBo.STATUS = bIStatus;
        msassignedtomebl objPIAddBl = new msassignedtomebl();
        try
        {
           // int iResult = objPIAddBl.Approval_AddressDetails(objPIAddBo);
            //ucSendMails.SendMail("70297");

        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
        LoadGridDetails();
        pnlAddressInfo.Visible = false;
    }
    protected void btnBankDetailsApprove_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        Approve_Reject_BankDetails(bIStatus);
    }
    protected void btnBankDetailsReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        Approve_Reject_BankDetails(bIStatus);
    }
    protected void Approve_Reject_BankDetails(bool bIStatus)
    {
        msassignedtomebo objBankInfoBo = new msassignedtomebo();
        string sPkey = (string)Session["objBoBankDetails"];
        objBankInfoBo.APPROVED_BY = User.Identity.Name;
        objBankInfoBo.PKEY = sPkey;
        objBankInfoBo.APPROVED_ON = DateTime.Now;
        objBankInfoBo.COMMENTS = txtBankComments.Text.Trim();
        objBankInfoBo.STATUS = bIStatus;
        msassignedtomebl objPIAddBl = new msassignedtomebl();
        try
        {
            //int iResult = objPIAddBl.Approval_BankDetails(objBankInfoBo);

        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
        LoadGridDetails();
        pnlBankInfo.Visible = false;
    }

    protected void btnFMApprove_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        Approve_Reject_FamilyDetails(bIStatus);
    }
    protected void btnFMReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        Approve_Reject_FamilyDetails(bIStatus);
    }
    protected void Approve_Reject_FamilyDetails(bool bIStatus)
    {
        msassignedtomebo objBankInfoBo = new msassignedtomebo();
        string sPkey = (string)Session["objBoFMDetails"];
        objBankInfoBo.APPROVED_BY = User.Identity.Name;
        objBankInfoBo.PKEY = sPkey;
        objBankInfoBo.APPROVED_ON = DateTime.Now;
        objBankInfoBo.COMMENTS = txtFMComments.Text.Trim();
        objBankInfoBo.STATUS = bIStatus;
        msassignedtomebl objPIAddBl = new msassignedtomebl();
        try
        {
            //int iResult = objPIAddBl.Approval_FamilykDetails(objBankInfoBo);

        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
        LoadGridDetails();
        pnlFamilyMember.Visible = false;

    }

    protected void btnPIDetailsApprove_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        Approve_Reject_PIDetails(bIStatus);
    }
    protected void btnPIDetailsReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        Approve_Reject_PIDetails(bIStatus);
    }
    protected void Approve_Reject_PIDetails(bool bIStatus)
    {
        msassignedtomebo objBo = new msassignedtomebo();
        string sPkey = (string)Session["objPIDBo"];
        string sPernr = (string)Session["sPIDPernr"];
        objBo.APPROVED_BY = User.Identity.Name;
        objBo.PKEY = sPkey;
        objBo.PERNR = sPernr;
        objBo.APPROVED_ON = DateTime.Now;
        objBo.COMMENTS = txtPIComments.Text.Trim();
        objBo.STATUS = bIStatus;
        msassignedtomebl objPIAddBl = new msassignedtomebl();
        try
        {
            //int iResult = objPIAddBl.Approval_PIDetails(objBo);

        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
        LoadGridDetails();
        pnlPersonalIdInfo.Visible = false;

    }
    protected void btnPDInfoApprove_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        Approve_Reject_PDInfoDetails(bIStatus);
    }
    protected void btnPDInfoReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        Approve_Reject_PDInfoDetails(bIStatus);
    }
    protected void Approve_Reject_PDInfoDetails(bool bIStatus)
    {
        msassignedtomebo objBo = new msassignedtomebo();
        string sPernr = (string)Session["objPDBo"];
        objBo.PERNR = sPernr;
        objBo.APPROVED_BY = User.Identity.Name;
        objBo.APPROVED_ON = DateTime.Now;
        objBo.COMMENTS = txtPDCommnets.Text.Trim();
        objBo.STATUS = bIStatus;
        msassignedtomebl objPIAddBl = new msassignedtomebl();
        try
        {
            //int iResult = objPIAddBl.Approval_PDInfoDetails(objBo);

        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
        LoadGridDetails();
        pnlpersonaldataInfo.Visible = false;

    }

    protected void btnCommunicationInfo_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        Approve_Reject_CommunticationInfoDetails(bIStatus);
    }
    protected void btnCommunicationInfoReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        Approve_Reject_CommunticationInfoDetails(bIStatus);
    }
    protected void Approve_Reject_CommunticationInfoDetails(bool bIStatus)
    {
        msassignedtomebo objBo = new msassignedtomebo();
        string sPernr = (string)Session["objCOmmuPernrBO"];
        objBo.PERNR = sPernr;
        objBo.APPROVED_BY = User.Identity.Name;
        objBo.APPROVED_ON = DateTime.Now;
        objBo.COMMENTS = txtCommunicationInfoCommnets.Text.Trim();
        objBo.STATUS = bIStatus;
        msassignedtomebl objPIAddBl = new msassignedtomebl();
        try
        {
           // int iResult = objPIAddBl.Approval_CommunticationInfoDetails(objBo);

        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
        LoadGridDetails();
        pnlCommunicationInfo.Visible = false;

    }

    protected void btnClockInOut_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        Approve_Reject_ClockIODetails(bIStatus);
    }
    protected void btnClockInOutReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        Approve_Reject_ClockIODetails(bIStatus);
    }
    protected void Approve_Reject_ClockIODetails(bool bIStatus)
    {
        msassignedtomebo objBo = new msassignedtomebo();
        string sIOPkey = (string)Session["sIOPkey"];
        string sEntryStatus = (string)Session["sEntryStatus"];
        objBo.APPROVED_BY = User.Identity.Name;
        objBo.APPROVED_ON = DateTime.Now;
        objBo.COMMENTS = txtClockInOutComments.Text.Trim();
        objBo.STATUS = bIStatus;
        objBo.PKEY = sIOPkey;
        objBo.ENTRY_STATUS = sEntryStatus;
        msassignedtomebl objPIAddBl = new msassignedtomebl();
        try
        {
            //int iResult = objPIAddBl.Approval_ClockIODetails(objBo);

        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
        LoadGridDetails();
        pnlClockInOut.Visible = false;

    }
   
    protected void btnRWApprove_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        Approve_Reject_RecordWorkingDetails(bIStatus);
    }
    protected void btnRWReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        Approve_Reject_RecordWorkingDetails(bIStatus);
    }
    protected void Approve_Reject_RecordWorkingDetails(bool bIStatus)
    {
        msassignedtomebo objBo = new msassignedtomebo();
        string sIOPkey = (string)Session["sRWkey"];
        objBo.APPROVED_BY = User.Identity.Name;
        objBo.APPROVED_ON = DateTime.Now;
        objBo.COMMENTS = txtRWComments.Text.Trim();
        objBo.STATUS = bIStatus;
        objBo.PKEY = sIOPkey;
        msassignedtomebl objPIAddBl = new msassignedtomebl();
        try
        {
           // int iResult = objPIAddBl.Approval_RecorWorkingDetails(objBo);
        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
        LoadGridDetails();
        pnlRecordWorking.Visible = false;
    }

    protected void btnLRReject_Click(object sender, EventArgs e)
    {
        bool bIStatus = false;
        Approve_Reject_LeaveRequestDetails(bIStatus);
    }
    protected void btnLRApprove_Click(object sender, EventArgs e)
    {
        bool bIStatus = true;
        Approve_Reject_LeaveRequestDetails(bIStatus);
    }
    protected void Approve_Reject_LeaveRequestDetails(bool bIStatus)
    {
        msassignedtomebo objBo = new msassignedtomebo();
        string sIOPkey = (string)Session["LeaveReqId"];
        objBo.APPROVED_BY = User.Identity.Name;
        objBo.APPROVED_ON = DateTime.Now;
        objBo.COMMENTS = txtRWComments.Text.Trim();
        objBo.STATUS = bIStatus;
        objBo.PKEY = sIOPkey;
        msassignedtomebl objPIAddBl = new msassignedtomebl();
        try
        {
           // int iResult = objPIAddBl.Approval_LeaveRequestDetails(objBo);
        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
        LoadGridDetails();
        pnlLeaveRequest.Visible = false;
    }

}
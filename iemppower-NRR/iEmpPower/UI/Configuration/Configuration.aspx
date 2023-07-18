<%@ Page Title="Configuration" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    Inherits="UI_Configuration_Configuration" Theme="SkinFile" Culture="en-GB" UICulture="auto" CodeBehind="Configuration.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../../Utilities/ValidationMessages.js" type="text/javascript"></script>
    <script src="../../Utilities/Validations.js" type="text/javascript"></script>
    <script type="text/javascript">

        function setDirty() {
            document.body.onbeforeunload = showMessage;
            //debugger;      
            document.getElementById("DirtyLabel").className = "show";
        }
        function clearDirty() {
            document.body.onbeforeunload = "";
            document.getElementById("DirtyLabel").className = "hide";
        }

        function showMessage() {
            return "If you click OK, the changes you have made will be lost."
        }
        function setControlChange() {
            if (typeof (event.srcElement) != 'undefined') {
                event.srcElement.onchange = setDirty;
            }
        }

        document.body.onclick = setControlChange;
        document.body.onkeyup = setControlChange;


        // Validation to validate if country is set to mandatory when state is being
        //  set to mandatory in Personal data module.
        function PersonalDataClientStateChanged() {
            var chkPDMandatoryState = document.getElementById("<%= chkPDMandatoryState.ClientID %>")
            var chkPDMandatoryCOB = document.getElementById("<%= chkPDMandatoryCOB.ClientID %>")
            document.getElementById("<%= lblMsgBrdPersonalData.ClientID %>").style.color = "red";
            if (chkPDMandatoryCOB.checked == true) {
                document.getElementById("<%= chkPDMandatoryState.ClientID %>").disabled = false;
                chkPDMandatoryState.checked = false;
            }
            if (chkPDMandatoryCOB.checked == false) {
                document.getElementById("<%= chkPDMandatoryState.ClientID %>").disabled = true;
                chkPDMandatoryState.checked = false;
            }
        }

        function showDirectory() {
            newWin = window.showModalDialog("/iEmpPower/UI/Configuration/BrowseDirectory.aspx", 'jain', "dialogHeight: 560px; dialogWidth: 360px; edge: Raised; center: Yes; help: Yes; resizable: Yes; status: No;");
            document.getElementById("<%= txtEmployePhotoPath.ClientID %>").value = newWin;
            return false;
        }
        function ClientEmployePhotoPathChanged() {
            var txtEmployePhotoPath = document.getElementById("<%= txtEmployePhotoPath.ClientID %>")
            if (txtEmployePhotoPath.value.length > 0 || txtEmployePhotoPath.value.length == 0)
                document.getElementById('<%= lblPhotoMessage.ClientID %>').innerText = "";
        }

        function ValidateControls() {

            document.getElementById("<%= lblPhotoMessage.ClientID %>").style.color = "red";
        document.getElementById('<%= lblLogoMessage.ClientID %>').innerText = "";
        var txtEmployePhotoPath = document.getElementById("<%=txtEmployePhotoPath.ClientID %>");
        if (txtEmployePhotoPath.value.trim() == "") {
            document.getElementById("<%= lblPhotoMessage.ClientID %>").innerText = msgEmployeePhotoPathEmpty;
            return false;
        }
        else {
            return true
        }
    }
    </script>
    <span class="hidden">
        <asp:Button ID="Button4" runat="server" Text="" /></span>


    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Configuration</li>
                    </ol>
                </div>
                <h4 class="page-title">Configuration
                    <asp:Label ID="Label3" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

    <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">
                <div class="divfr" id="divbrdr">    
                    <asp:Label ID="lblSeverMessage" runat="server" CssClass="lblMsg"></asp:Label>                   
                    <div class="search-section">
                                     <div class="row">
                                       <div class="col-sm-2 htCr">POP Server&nbsp;<b>:</b></div>
                                        <div class="col-sm-2">
                                                <asp:TextBox ID="txtPOPServer" runat="server" placeholder="Enter POP Server Name" meta:resourcekey="txtPOPServerResource1" CssClass="txtDropDownwidth"></asp:TextBox>
                                            </div>
                                        </div>
                        <br />
                                       <div class="row">
                                       <div class="col-sm-2 htCr">IMAP Server&nbsp;<b>:</b></div>
                                        <div class="col-sm-2">
                                                <asp:TextBox ID="txtIMAPServer" runat="server" placeholder="Enter IMAP Server Name" meta:resourcekey="txtIMAPServerResource1" CssClass="txtDropDownwidth"></asp:TextBox>
                                            </div>
                                        </div>
                        <br />
                                       <div class="row">
                                       <div class="col-sm-2 htCr"> SMTP Server&nbsp;<b>:</b></div>
                                        <div class="col-sm-2">
                                                <asp:TextBox ID="txtSMTPServer" runat="server" placeholder="Enter SMTP Server Name" meta:resourcekey="txtSMTPServerResource1" CssClass="txtDropDownwidth"></asp:TextBox><br />
                                            </div>
                                        </div>
                        <br />
                                         <div class="row">
                                       <div class="col-sm-2 htCr"> Port&nbsp;<b>:</b></div>
                                        <div class="col-sm-2">
                                                <asp:TextBox ID="txtPort" runat="server" placeholder="Enter Port No." CssClass="txtDropDownwidth"></asp:TextBox>
                                            </div>
                                        </div>
                        <br />
                                       <div class="row">
                                       <div class="col-sm-2 htCr">E-Mail Id&nbsp;<b>:</b></div>
                                        <div class="col-sm-2">
                                                <asp:TextBox ID="txtEmailId" runat="server" placeholder="Enter E-Mail Id" CssClass="txtDropDownwidth"></asp:TextBox>
                                            </div>
                                        </div>
                        <br />
                                         <div class="row">
                                       <div class="col-sm-2 htCr">User Name&nbsp;<b>:</b></div>
                                        <div class="col-sm-2">
                                                <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter User Name" CssClass="txtDropDownwidth"></asp:TextBox>
                                            </div>
                                        </div>
                        <br />
                                         <div class="row">
                                       <div class="col-sm-2 htCr">Password&nbsp;<b>:</b></div>
                                        <div class="col-sm-2">
                                                <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter Password" CssClass="txtDropDownwidth" TextMode="Password"></asp:TextBox>
                                            </div>
                                        </div>
                        <br />
                                       <div class="row">
                                       <div class="col-sm-2 htCr">
                                                <asp:Button ID="btnMailServerDetails" runat="server" Width="60px" Text="Save" OnClientClick="clearDirty();" OnClick="btnMailServerDetails_Click" meta:resourcekey="btnMailServerDetailsResource1" />
                                            </div>
                                        </div>
                                    </div>
                    <br />

                    <div>
                        <asp:Label ID="lblLogoMessage" runat="server" CssClass="lblMsg"></asp:Label><br />
                        <h4 style="text-decoration: underline">Configure company logo</h4>
                        <div>
                            <div class="row">
                                    <div class="col-sm-3">
                                        <asp:FileUpload ID="fulLogo" runat="server" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnLogoSave" runat="server" Width="60px" Text="Save" OnClick="btnLogoSave_Click" OnClientClick="clearDirty();" />
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Image ID="imageLogo" runat="server" Height="100px" Width="220px" />
                                    </div>
                                </div>

                        </div>
                    </div>
                    <br />
                    <div>
                                <div>
                                    <asp:Label ID="lblPhotoMessage" Width="100%" CssClass="lblMsg"
                                        runat="server"></asp:Label><br />
                                    <h4 style="text-decoration: underline">Configure Employee photo path</h4>
                                    <br />
                                </div>
                                <div class="row">
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtEmployePhotoPath" runat="server" Enabled="False" CssClass="txtDropDownwidth"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button OnClientClick="showDirectory();" ID="Button1" runat="server" Text="Browse" Width="60px"/>
                                            <asp:Button ID="btnEmployePhotoPath" runat="server" Text="Save" OnClientClick="return ValidateControls()"
                                                OnClick="btnEmployePhotoPath_Click" Width="60px"/>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Label ID="lblEmployePhotoPath" runat="server" Text="" CssClass="lblValidation"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                  
       
    <div runat="server" visible="false">
                <asp:Label ID="lblGridMessage" runat="server"></asp:Label><br />

                <p>Select the modules by checking the boxes which follow supervisor's approval flow.</p>
                <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                    <div class="respovrflw">
                        <asp:GridView ID="grdReqSupApproval" runat="server" AutoGenerateColumns="False" PageSize="5"
                            Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None"
                            CssClass="gridview" meta:resourcekey="grdReqSupApprovalResource1">
                            <AlternatingRowStyle BackColor="White" />

                            <Columns>

                                <asp:BoundField DataField="DESCRIPTION" HeaderText="Modules"
                                    meta:resourcekey="BoundFieldResource1" />
                                <asp:TemplateField HeaderText="Requires supervisor approval"
                                    meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBxRequiresStatus" runat="server"
                                            Checked='<%# Bind("REQUIRES_STATUS") %>'
                                            meta:resourcekey="chkBxRequiresStatusResource1" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField meta:resourcekey="TemplateFieldResource2" Visible="false" ControlStyle-Width="0px">
                                    <ItemTemplate>

                                        <asp:Label ID="hdnRequireId" runat="server" Text='<%# Eval("REQURIES_ID") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="hdnRequireId" runat="server" Text='<%# Eval("REQURIES_ID") %>' />
                                    </EditItemTemplate>
                                    <ControlStyle Width="0px" />
                                    <ItemStyle Width="0px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Requires HR approval">

                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBxHRStatus" runat="server"
                                            Checked='<%# Bind("HR_STATUS") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Requires Payroll Admin Approval">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBxFinAdminStatus" runat="server"
                                            Checked='<%# Bind("FIN_ADMIN_STATUS") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle Font-Bold="False" CssClass="gridHeader" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#DEEBF7" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </div>
                </asp:Panel>
                <div class="btn-group-sm">
                    <div class="col-sm-1">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="clearDirty();"/>
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnStatus" runat="server" Text="Check all" OnClick="btnStatus_Click"/>
                    </div>
                    <div class="col-sm-1">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" OnClientClick="clearDirty();"/>
                    </div>
                </div>

                <asp:Label ID="lblMessageBoard" Width="100%"
                    runat="server" meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />
                <p>Set the fields for modules which need to be made mandatory in the user interface.</p>
                <br />
                <cc1:TabContainer ID="tabConfiguration" runat="server" ActiveTabIndex="10"
                    OnActiveTabChanged="tabConfiguration_ActiveTabChanged" AutoPostBack="True"
                    meta:resourcekey="tabConfigurationResource1">
                    <cc1:TabPanel runat="server" HeaderText="Address information"
                        ID="tabAddressInformation" meta:resourcekey="tabAddressInformationResource1">
                        <ContentTemplate>
                            <div>
                                <div class="tbHeader">
                                    <asp:Label ID="lblADdressDescription" runat="server" Text="Fields" Width="150px" meta:resourcekey="lblADdressDescriptionResource1">
                                    </asp:Label>
                                    <asp:Label ID="lblADdressMandatory" runat="server" Text="Mandatory" Width="150px" meta:resourcekey="lblADdressMandatoryResource1"></asp:Label>
                                </div>
                                <br />
                                <asp:Label ID="lblSubtype" runat="server" Text="Subtype " Width="150px" meta:resourcekey="lblSubtypeResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkMandatorySubtype" runat="server" meta:resourcekey="chkMandatorySubtypeResource1" /><br />
                                <asp:Label ID="lblValidFromDate" runat="server" Text="Valid from date" Width="150px" meta:resourcekey="lblValidFromDateResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkMandatoryValidFromDate" runat="server" meta:resourcekey="chkMandatoryValidFromDateResource1" /><br />
                                <asp:Label ID="lblValidToDate" runat="server" Text="Valid To Date" Width="150px" meta:resourcekey="lblValidToDateResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkMandatoryValidToDate" runat="server" meta:resourcekey="chkMandatoryValidToDateResource1" /><br />
                                <asp:Label ID="lblAddressLine1" runat="server" Text="House number and street" Width="150px" meta:resourcekey="lblAddressLine1Resource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkMandatoryAddressLine1" runat="server" meta:resourcekey="chkMandatoryAddressLine1Resource1" /><br />
                                <asp:Label ID="lblAddressLine2" runat="server" Text="2nd Address Line" Width="150px" meta:resourcekey="lblAddressLine2Resource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkMandatoryAddressLine2" runat="server" meta:resourcekey="chkMandatoryAddressLine2Resource1" /><br />
                                <asp:Label ID="lblRegion" runat="server" Text="Region" Width="150px" meta:resourcekey="lblRegionResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkMandatoryRegion" runat="server" meta:resourcekey="chkMandatoryRegionResource1" /><br />
                                <asp:Label ID="lblCity" runat="server" Text="City" Width="150px" meta:resourcekey="lblCityResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkMandatoryCity" runat="server" meta:resourcekey="chkMandatoryCityResource1" /><br />
                                <asp:Label ID="lblPostalCode" runat="server" Text="Postal code" Width="150px" meta:resourcekey="lblPostalCodeResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkMandatoryPostalCode" runat="server" meta:resourcekey="chkMandatoryPostalCodeResource1" /><br />
                                <asp:Label ID="lblPhone" runat="server" Text="Phone" Width="150px" meta:resourcekey="lblPhoneResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkMandatoryPhone" runat="server" meta:resourcekey="chkMandatoryPhoneResource1" /><br />
                            </div>
                            <div class="buttonrow">
                                <asp:Button ID="btnAddressInformation" runat="server" Text="Save" OnClick="btnAddressInformation_Click" OnClientClick="clearDirty();" meta:resourcekey="btnAddressInformationResource1" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Bank information" ID="tabBankInformation" meta:resourcekey="tabBankInformationResource1">
                        <ContentTemplate>
                            <div>
                                <div class="tbHeader">
                                    <asp:Label ID="lblBankDescription" runat="server" Text="Fields" Width="150px" meta:resourcekey="lblBankDescriptionResource1"></asp:Label><asp:Label ID="lblBankMandatory" runat="server" Text="Mandatory" Width="150px" meta:resourcekey="lblBankMandatoryResource1"></asp:Label>
                                </div>
                                <br />
                                <asp:Label ID="lblBankSubtype" runat="server" Text="Subtype " Width="150px" meta:resourcekey="lblBankSubtypeResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkBankMandatorySubtype" runat="server" meta:resourcekey="chkBankMandatorySubtypeResource1" /><br />
                                <asp:Label ID="lblRecipientText" runat="server" Text="Recipient text" Width="150px" meta:resourcekey="lblRecipientTextResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkBankMandatoryRecipientText" runat="server" meta:resourcekey="chkBankMandatoryRecipientTextResource1" /><br />
                                <asp:Label ID="lblBankPostalCode" runat="server" Text="Postal code" Width="150px" meta:resourcekey="lblBankPostalCodeResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkBankMandatoryPostalCode" runat="server" meta:resourcekey="chkBankMandatoryPostalCodeResource1" /><br />
                                <asp:Label ID="lblBankCity" runat="server" Text="City" Width="150px" meta:resourcekey="lblBankCityResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkBankMandatoryCity" runat="server" meta:resourcekey="chkBankMandatoryCityResource1" /><br />
                                <asp:Label ID="lblBankCountryKey" runat="server" Text="Bank country key" Width="150px" meta:resourcekey="lblBankCountryKeyResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkBankMandatoryBankCountryKey" runat="server" meta:resourcekey="chkBankMandatoryBankCountryKeyResource1" /><br />
                                <asp:Label ID="lblBankBankKey" runat="server" Text="Bank Key" Width="150px" meta:resourcekey="lblBankBankKeyResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkBankMandatoryBankKey" runat="server" meta:resourcekey="chkBankMandatoryBankKeyResource1" /><br />
                                <asp:Label ID="lblBankBankAccountNo" runat="server" Text="Bank account number" Width="150px" meta:resourcekey="lblBankBankAccountNoResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkBankMandatoryBankAccountNo" runat="server" meta:resourcekey="chkBankMandatoryBankAccountNoResource1" /><br />
                                <asp:Label ID="lblBankPaymentMethod" runat="server" Text="Payment method" Width="150px" meta:resourcekey="lblBankPaymentMethodResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkBankMandatoryPaymentMethod" runat="server" meta:resourcekey="chkBankMandatoryPaymentMethodResource1" /><br />
                                <asp:Label ID="lblBankBankTransfers" runat="server" Text="Purpose of bank transfers" Width="150px" meta:resourcekey="lblBankBankTransfersResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkBankMandatoryBankTransfers" runat="server" meta:resourcekey="chkBankMandatoryBankTransfersResource1" /><br />
                                <asp:Label ID="lblBankCurrencyKey" runat="server" Text="Currency Key" Width="150px" meta:resourcekey="lblBankCurrencyKeyResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkBankMandatoryCurrencyKey" runat="server" meta:resourcekey="chkBankMandatoryCurrencyKeyResource1" />
                            </div>
                            <div class="buttonrow">
                                <asp:Button ID="btnBankInformation" runat="server" Text="Save" OnClick="btnBankInformation_Click" OnClientClick="clearDirty();" meta:resourcekey="btnBankInformationResource1" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tabFamilyMemberInformation" runat="server" HeaderText="Family member" meta:resourcekey="tabFamilyMemberInformationResource1">
                        <ContentTemplate>
                            <div>
                                <div class="tbHeader">
                                    <asp:Label ID="lblFMIDescription" runat="server" Text="Fields" Width="150px" meta:resourcekey="lblFMIDescriptionResource1"></asp:Label><asp:Label ID="lblFMIMandatory" runat="server" Text="Mandatory" Width="150px" meta:resourcekey="lblFMIMandatoryResource1"></asp:Label>
                                </div>
                                <br />
                                <asp:Label ID="lblFMISubtype" runat="server" Text="Family member type" Width="150px" meta:resourcekey="lblFMISubtypeResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatorySubtype" runat="server" meta:resourcekey="chkFMIMandatorySubtypeResource1" /><br />
                                <asp:Label ID="lblFMIFirstName" runat="server" Text="First name" Width="150px" meta:resourcekey="lblFMIFirstNameResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryFirstName" runat="server" meta:resourcekey="chkFMIMandatoryFirstNameResource1" /><br />
                                <asp:Label ID="lblFMILastName" runat="server" Text="Last name" Width="150px" meta:resourcekey="lblFMILastNameResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIFMIMandatoryLastName" runat="server" meta:resourcekey="chkFMIFMIMandatoryLastNameResource1" /><br />
                                <asp:Label ID="lblFMINOB" runat="server" Text="Name at birth" Width="150px" meta:resourcekey="lblFMINOBResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryNOB" runat="server" meta:resourcekey="chkFMIMandatoryNOBResource1" /><br />
                                <asp:Label ID="lblFMIInitials" runat="server" Text="Employee's initials" Width="150px" meta:resourcekey="lblFMIInitialsResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryInitials" runat="server" meta:resourcekey="chkFMIMandatoryInitialsResource1" /><br />
                                <asp:Label ID="lblFMIOtherTitle" runat="server" Text="Other title" Width="150px" meta:resourcekey="lblFMIOtherTitleResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryOtherTitle" runat="server" meta:resourcekey="chkFMIMandatoryOtherTitleResource1" /><br />
                                <asp:Label ID="lblFMINamePrefix" runat="server" Text="Name prefix" Width="150px" meta:resourcekey="lblFMINamePrefixResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryNamePrefix" runat="server" meta:resourcekey="chkFMIMandatoryNamePrefixResource1" /><br />
                                <asp:Label ID="lblFMIGender" runat="server" Text="Gender key" Width="150px" meta:resourcekey="lblFMIGenderResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryGender" runat="server" meta:resourcekey="chkFMIMandatoryGenderResource1" /><br />
                                <asp:Label ID="lblFMIDOB" runat="server" Text="Date of birth" Width="150px" meta:resourcekey="lblFMIDOBResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryDOB" runat="server" meta:resourcekey="chkFMIMandatoryDOBResource1" /><br />
                                <asp:Label ID="lblFMIPOB" runat="server" Text="Birthplace" Width="150px" meta:resourcekey="lblFMIPOBResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryPOB" runat="server" meta:resourcekey="chkFMIMandatoryPOBResource1" /><br />
                                <asp:Label ID="lblFMICOB" runat="server" Text="Country of Birth" Width="150px" meta:resourcekey="lblFMICOBResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryCOB" runat="server" meta:resourcekey="chkFMIMandatoryCOBResource1" /><br />
                                <asp:Label ID="lblFMINationality" runat="server" Text="Nationality" Width="150px" meta:resourcekey="lblFMINationalityResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryNationality" runat="server" meta:resourcekey="chkFMIMandatoryNationalityResource1" /><br />
                                <asp:Label ID="lblFMINationality2" runat="server" Text="Second nationality" Width="150px" meta:resourcekey="lblFMINationality2Resource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryNationality2" runat="server" meta:resourcekey="chkFMIMandatoryNationality2Resource1" /><br />
                                <asp:Label ID="lblFMINationality3" runat="server" Text="Third nationality" Width="150px" meta:resourcekey="lblFMINationality3Resource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkFMIMandatoryNationality3" runat="server" meta:resourcekey="chkFMIMandatoryNationality3Resource1" /><br />
                            </div>
                            <div class="buttonrow">
                                <asp:Button ID="btnFamilyMemberInformation" runat="server" Text="Save" OnClick="btnFamilyMemberInformation_Click" OnClientClick="clearDirty();" meta:resourcekey="btnFamilyMemberInformationResource1" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tabPersonalData" runat="server" HeaderText="Personal data" meta:resourcekey="tabPersonalDataResource1">
                        <ContentTemplate>
                            <asp:Label ID="lblMsgBrdPersonalData" runat="server" Text="" Width="500px"></asp:Label><div>
                                <div class="tbHeader">
                                    <asp:Label ID="lblPDDescription" runat="server" Text="Fields" Width="150px" meta:resourcekey="lblPDDescriptionResource1"></asp:Label><asp:Label ID="lblPDMandatory" runat="server" Text="Mandatory" Width="150px" meta:resourcekey="lblPDMandatoryResource1"></asp:Label>
                                </div>
                                <br />
                                <asp:Label ID="lblPDTitle" runat="server" Text="Title" Width="150px" meta:resourcekey="lblPDTitleResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryTitle" runat="server" meta:resourcekey="chkPDMandatoryTitleResource1" /><br />
                                <asp:Label ID="lblPDFirstName" runat="server" Text="First name" Width="150px" meta:resourcekey="lblPDFirstNameResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryFirstName" runat="server" meta:resourcekey="chkPDMandatoryFirstNameResource1" /><br />
                                <asp:Label ID="lblPDLastName" runat="server" Text="Last name" Width="150px" meta:resourcekey="lblPDLastNameResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryLastName" runat="server" meta:resourcekey="chkPDMandatoryLastNameResource1" /><br />
                                <asp:Label ID="lblPDNOB" runat="server" Text="Name at birth" Width="150px" meta:resourcekey="lblPDNOBResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryNOB" runat="server" meta:resourcekey="chkPDMandatoryNOBResource1" /><br />
                                <asp:Label ID="lblPDInitials" runat="server" Text="Employee's initials" Width="150px" meta:resourcekey="lblPDInitialsResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryInitials" runat="server" meta:resourcekey="chkPDMandatoryInitialsResource1" /><br />
                                <asp:Label ID="lblPDKnownAs" runat="server" Text="Known As" Width="150px" meta:resourcekey="lblPDKnownAsResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryKnownAs" runat="server" meta:resourcekey="chkPDMandatoryKnownAsResource1" /><br />
                                <asp:Label ID="lblPDLanguage" runat="server" Text="Language" Width="150px" meta:resourcekey="lblPDLanguageResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryLanguage" runat="server" meta:resourcekey="chkPDMandatoryLanguageResource1" /><br />
                                <asp:Label ID="lblPDGender" runat="server" Text="Gender key" Width="150px" meta:resourcekey="lblPDGenderResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryGender" runat="server" meta:resourcekey="chkPDMandatoryGenderResource1" /><br />
                                <asp:Label ID="lblPDDOB" runat="server" Text="Date of birth" Width="150px" meta:resourcekey="lblPDDOBResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryDOB" runat="server" meta:resourcekey="chkPDMandatoryDOBResource1" /><br />
                                <asp:Label ID="lblPDPOB" runat="server" Text="Birthplace" Width="150px" meta:resourcekey="lblPDPOBResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryPOB" runat="server" meta:resourcekey="chkPDMandatoryPOBResource1" /><br />
                                <asp:Label ID="lblPDCOB" runat="server" Text="Country of Birth" Width="150px" meta:resourcekey="lblPDCOBResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryCOB" runat="server" meta:resourcekey="chkPDMandatoryCOBResource1" /><br />
                                <asp:Label ID="lblPDNationality" runat="server" Text="Nationality" Width="150px" meta:resourcekey="lblPDNationalityResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryNationality" runat="server" meta:resourcekey="chkPDMandatoryNationalityResource1" /><br />
                                <asp:Label ID="lblPDState" runat="server" Text="State" Width="150px" meta:resourcekey="lblPDStateResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryState" runat="server" meta:resourcekey="chkPDMandatoryStateResource1" /><br />
                                <asp:Label ID="lblPDNationality2" runat="server" Text="Second nationality" Width="150px" meta:resourcekey="lblPDNationality2Resource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryNationality2" runat="server" meta:resourcekey="chkPDMandatoryNationality2Resource1" /><br />
                                <asp:Label ID="lblPDNationality3" runat="server" Text="Third nationality" Width="150px" meta:resourcekey="lblPDNationality3Resource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryNationality3" runat="server" meta:resourcekey="chkPDMandatoryNationality3Resource1" /><br />
                                <asp:Label ID="lblPDMarital" runat="server" Text="Marital status key" Width="150px" meta:resourcekey="lblPDMaritalResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryMartial" runat="server" meta:resourcekey="chkPDMandatoryMartialResource1" /><br />
                                <asp:Label ID="lblPDValidFromMaritalStatus" runat="server" Text="Valid from date of current marital status" Width="150px" meta:resourcekey="lblPDValidFromMaritalStatusResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryValidFromMaritalStatus" runat="server" meta:resourcekey="chkPDMandatoryValidFromMaritalStatusResource1" /><br />
                                <asp:Label ID="lblPDNoOfChildren" runat="server" Text="Number of children" Width="150px" meta:resourcekey="lblPDNoOfChildrenResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryNoOfChildren" runat="server" meta:resourcekey="chkPDMandatoryNoOfChildrenResource1" /><br />
                                <asp:Label ID="lblPDReligious" runat="server" Text="Religious denomination key" Width="150px" meta:resourcekey="lblPDReligiousResource1" CssClass="label"></asp:Label><asp:CheckBox ID="chkPDMandatoryReligious" runat="server" meta:resourcekey="chkPDMandatoryReligiousResource1" /><br />
                            </div>
                            <div class="buttonrow">
                                <asp:Button ID="btnPersonalData" runat="server" Text="Save" OnClick="btnPersonalData_Click" OnClientClick="clearDirty();" meta:resourcekey="btnPersonalDataResource1" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>

        <div  Visible="false">

           
                    <div>
                        <asp:Label ID="lblTitle" Width="100%" runat="server"></asp:Label><br />
                        <p>Configure Company Policies path.</p>
                        <br />
                    </div>
                    <asp:TextBox ID="TextBox1" runat="server" Enabled="False" Font-Bold="True"></asp:TextBox>
                    <asp:Button OnClientClick="showDirectory();" ID="Button2" runat="server" Text="Browse" Width="60px"/>
                    <asp:Button ID="Button3" runat="server" Width="80px" Text="Save" OnClientClick="return ValidateControls()" OnClick="btnEmployePhotoPath_Click" />
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    
        </div>

        <div>           
                    <div>
                        <asp:Label ID="lblLeaveEncashment" Width="100%" runat="server"></asp:Label><br />
                        <p>Configure Leaves to be encashed.</p>
                        <br />
                    </div>
                    <br />
                    <asp:GridView ID="grdLeaveEncashable" runat="server" AutoGenerateColumns="False" Width="50%">
                        <Columns>
                            <asp:BoundField DataField="MOAWB" HeaderText="MOAWB" />
                            <asp:BoundField DataField="AWART" HeaderText="AWART" />
                            <asp:BoundField DataField="ATEXT" HeaderText="Leave Type" />
                            <asp:TemplateField HeaderText="Encashable">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBxEncashStatus" runat="server"
                                        Checked='<%# Bind("ENCASHABLE") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Font-Bold="False" CssClass="gridHeader" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#DEEBF7" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>

                    </div>
                    <br />
                    <div>
                        <asp:Button ID="btnLESave" runat="server" Text="Save" width="60px" OnClick="btnLESave_Click" />
                        <asp:Button ID="btnLESelectAll" runat="server" width="60px" Text="Check All" OnClick="btnLESelectAll_Click" />
                        <asp:Button ID="btnLEClearAll" runat="server" Text="Clear All" width="60px" OnClick="btnLEClearAll_Click" />
                    </div>

        </div>
    </div>
</div>
            </div>
</asp:Content>


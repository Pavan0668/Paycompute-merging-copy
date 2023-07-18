<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="UI_UserAccount_createuseraccount" Culture="auto" UICulture="auto"
    Theme="SkinFile" CodeBehind="createuseraccount.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../../Utilities/ValidationMessages.js" type="text/javascript"></script>
    <script src="../../Utilities/Validations.js" type="text/javascript"></script>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
        FilterType="Custom, Numbers"
        runat="server" Enabled="True" TargetControlID="txtUserName">
    </cc1:FilteredTextBoxExtender>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=".,_,@,-"
        runat="server" Enabled="True" TargetControlID="txtPassword"
        InvalidChars=" ">
    </cc1:FilteredTextBoxExtender>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars=".,_,@,-"
        runat="server" Enabled="True" TargetControlID="txtConfirmPassword"
        InvalidChars=" ">
    </cc1:FilteredTextBoxExtender>
    <script type="text/javascript">
        function ClientUserNameChanged() {
            var txtUserNameCtrl = document.getElementById("<%= txtUserName.ClientID %>");
            if (txtUserNameCtrl.value.length > 2 || txtUserNameCtrl.value.length == 0) {
                document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
            }
        }
        function ClientSearchUserNameChanged() {
            var txtSearchUserName = document.getElementById("<%= txtSearchUserName.ClientID %>");
                if (txtSearchUserName.value.length > 2 || txtSearchUserName.value.length == 0) {
                    document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
                }
            }
            function ClientPasswordChanged() {
                var txtPasswordCtrl = document.getElementById("<%= txtPassword.ClientID %>");
                if (txtPasswordCtrl.value.length >= 7 || txtPasswordCtrl.value.length == 0) {
                    document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
                }
            }
            function ClientPasswordTabout() {
                document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
                var txtConfirmPasswordCtrl = document.getElementById("<%= txtConfirmPassword.ClientID %>");
                var txtPasswordCtrl = document.getElementById("<%= txtPassword.ClientID %>");
                if (txtPasswordCtrl.value.length < 7) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgPwdInvalidLength;
                    txtPasswordCtrl.focus();
                    return false;
                }
                if (txtConfirmPasswordCtrl.value.length != 0) {
                    if (txtPasswordCtrl.value != txtConfirmPasswordCtrl.value) {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgPwdsDoNotMatch;
                        txtConfirmPasswordCtrl.focus();

                        return false;
                    }
                    else {
                        document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
                        return true;

                    }
                }
            }
            function ClientConfirmPasswordChanged() {
                var txtConfirmPasswordCtrl = document.getElementById("<%= txtConfirmPassword.ClientID %>");

                if (txtConfirmPasswordCtrl.value.length >= 7 || txtConfirmPasswordCtrl.value.length == 0) {
                    document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
                }
            }
            function ClientConfirmPasswordTabout() {
                document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
                var txtConfirmPasswordCtrl = document.getElementById("<%= txtConfirmPassword.ClientID %>");
                var txtPasswordCtrl = document.getElementById("<%= txtPassword.ClientID %>");
                if (!TextBoxEmpty(txtPasswordCtrl)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterPassword;
                    txtPasswordCtrl.focus();
                    return false;
                }
                if (txtPasswordCtrl.value != txtConfirmPasswordCtrl.value) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgPwdsDoNotMatch;
                    txtConfirmPasswordCtrl.focus();
                    return false;
                }
                else {
                    document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
                    return true;

                }
            }
            function ClientEmailChanged() {
                var TextEmail = document.getElementById("<%= txtEmail.ClientID %>");
                var EmailErrorCode = validateEmail(TextEmail.value)
                if ((EmailErrorCode == 0) || (TextEmail.value.lengh == 0)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = "";
                }
            }
            function ValidateSearchControls() {
                document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
                var txtSearchUserName = document.getElementById("<%= txtSearchUserName.ClientID %>")

                if (!TextBoxEmpty(txtSearchUserName)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterUserName;

                    return false;
                }
            }

            function ValidateControls() {
                document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
                var txtUserNameCtrl = document.getElementById("<%= txtUserName.ClientID %>")
                if (txtUserNameCtrl.disabled == false) {
                    if (!TextBoxEmpty(txtUserNameCtrl)) {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterUserName;
                        txtUserNameCtrl.focus();
                        return false;
                    }
                }
                var txtPasswordCtrl = document.getElementById("<%= txtPassword.ClientID %>")
                if (txtPasswordCtrl != null) {
                    if (!TextBoxEmpty(txtPasswordCtrl)) {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterPassword;
                        txtPasswordCtrl.focus();
                        return false;
                    }
                    if (txtPasswordCtrl.value.length < 7) {

                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgPwdInvalidLength;
                        txtPasswordCtrl.focus();
                        return false;
                    }
                    var reg = /[^A-Za-z0-9]/;
                    //var address = document.forms[form_id].elements[email].value;
                    if (reg.test(txtPasswordCtrl.value) == false) {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgPwdNANChar;
                        txtPasswordCtrl.focus();
                        return false;
                    }

                    var txtConfirmPasswordCtrl = document.getElementById("<%= txtConfirmPassword.ClientID %>")

                    if (!TextBoxEmpty(txtConfirmPasswordCtrl)) {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterConfirmPassword;
                        txtConfirmPasswordCtrl.focus();
                        return false;
                    }
                    if (txtPasswordCtrl.value != txtConfirmPasswordCtrl.value) {

                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgPwdsDoNotMatch;
                        txtConfirmPasswordCtrl.focus();
                        return false;
                    }
                }
                // validate if password contains atleast one nonalphanumeric character.



                // Validation for email textbox control.
                var EmailCtrl = document.getElementById("<%=txtEmail.ClientID%>");
                var Email = document.getElementById("<%=txtEmail.ClientID%>").value;
                if (EmailCtrl.value == "") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterEmail; // This msg string under emp regn section 
                    // in validation messages files  
                    EmailCtrl.focus();
                    return false;
                }
                var EmailErrorCode = 0;
                EmailErrorCode = validateEmail(Email)
                if (EmailErrorCode == 1) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgInvalidEmail; // This msg string under emp regn section 
                    // in validation messages files
                    EmailCtrl.focus();
                    return false;
                }

            }
            // Email validation function.
            function validateEmail(email) {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                //var address = document.forms[form_id].elements[email].value;
                if (reg.test(email) == false) {
                    return 1;
                }
                else {
                    return 0;
                }
            }
    </script>


     <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Create / Find user account</li>
                    </ol>
                </div>
                <h4 class="page-title">Create / Find user account
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

    
        <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">
                <div class="divfr" id="divbrdr">

                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg"></asp:Label>
                   
                    <div class="search-section">

                        <div class="row">
                    <div class="col-sm-1 htCr">User ID&nbsp;<b>:</b></div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtSearchUserName" placeholder="Enter User ID" runat="server" CssClass="txtDropDownwidth"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="btnSearch" runat="server" Text="Find user" OnClientClick="return ValidateSearchControls()" OnClick="btnSearch_Click"/>
                                </div>
                            </div>
                        </div>
                    <br />
                        <div class="respovrflw">
                            <asp:GridView ID="grdUsers" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="5" OnRowDataBound="grdUsers_RowDataBound" Width="40%"
                                OnSelectedIndexChanged="grdUsers_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="UserName" HeaderText="User" ReadOnly="True"/>
                                    <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True"/>
                                    <asp:TemplateField HeaderText="Active user">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsApproved") %>' CssClass="chkboxStyl1" Enabled="False"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    <br />
                    <br />
                       <div class="row">
                    <div class="col-sm-2 htCr">User ID&nbsp;<b>:</b></div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter User ID"
                                        meta:resourcekey="txtUserNameResource1" CssClass="txtDropDownwidth"></asp:TextBox>
                                </div>
                            </div>
                    <br />
                            <div class="row">
                            <div class="col-sm-2 htCr">Password&nbsp;<b>:</b></div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPassword" runat="server" Columns="15" MaxLength="15" placeholder="Enter Password"
                                            TextMode="Password" meta:resourcekey="txtPasswordResource1" CssClass="txtDropDownwidth"></asp:TextBox>
                                    </div>
                                </div>
                    <br />
                        <div class="row">
                            <div class="col-sm-2 htCr">Confirm Password&nbsp;<b>:</b></div>
                                    <div class="col-sm-3">
                                      <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txtDropDownwidth" TextMode="Password" placeholder="Enter Confirm Password"></asp:TextBox>
                                    </div>
                                </div>
                    <br />
                            <div class="row">
                            <div class="col-sm-2 htCr">Email ID&nbsp;<b>:</b></div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtEmail" runat="server" Columns="50" MaxLength="50" placeholder="Enter Email ID"
                                        meta:resourcekey="txtEmailResource1" CssClass="txtDropDownwidth"></asp:TextBox>
                                </div>
                            </div>
                    <br />
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:CheckBox ID="chkbxActiveUser" runat="server" CssClass="grid" Text="Active user" />
                                    &nbsp;&nbsp; 
                                    <asp:CheckBox ID="chkbxSuperUser" runat="server" CssClass="grid" Text="Super user" />
                                </div>
                            </div>
                    <br />
                       <div class="row">
                                <div class="col-sm-6">
                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="65px" OnClientClick="return ValidateControls()" OnClick="btnSave_Click" />                           
                                <asp:Button ID="btnClear" runat="server" Text="Clear" Width="65px" OnClick="btnClear_Click" />
                            </div>
                        </div>

                    </div>
            </div>
        </div>
</asp:Content>



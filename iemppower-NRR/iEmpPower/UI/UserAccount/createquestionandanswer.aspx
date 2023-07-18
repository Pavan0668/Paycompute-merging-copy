<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" Inherits="UI_UserAccount_createquestionandanswer" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" Theme="SkinFile" CodeBehind="createquestionandanswer.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

     <style>
        .HrCls
        {
            width: 100%;
            border: 0;
            height: 1px;
            background: #333;
            background-image: linear-gradient(to right, #333, #333, #ccc);
            padding: 0;
            margin: 3px 0;
        }
</style>

    <script src="../../Utilities/ValidationMessages.js" type="text/javascript"></script>
    <script src="../../Utilities/Validations.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ClientPasswordChanged() {
            var txtPasswordCtrl = document.getElementById("<%= txtPassword.ClientID %>");
               if (txtPasswordCtrl.value.length >= 7 || txtPasswordCtrl.value.length == 0) {
                   document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
                }
            }
            function ClientSecurityQuestionChanged() {
                var txtSecurityQuestionCtrl = document.getElementById("<%= txtSecurityQuestion.ClientID %>");
                if (txtSecurityQuestionCtrl.value.length > 3 || txtSecurityQuestionCtrl.value.length == 0) {
                    document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
                }
            }
            function ClientSecurityAnswerChanged() {
                var txtSecurityAnswerCtrl = document.getElementById("<%= txtSecurityAnswer.ClientID %>");
                if (txtSecurityAnswerCtrl.value.length > 3 || txtSecurityAnswerCtrl.value.length == 0) {
                    document.getElementById('<%= lblMessageBoard.ClientID %>').innerText = "";
                }
            }
            function ValidateControls() {
                document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
                var txtPasswordCtrl = document.getElementById("<%= txtPassword.ClientID %>")

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

                if (reg.test(txtPasswordCtrl.value) == false) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgPwdNANChar;
                    txtPasswordCtrl.focus();
                    return false;
                }
                // Validation for security question control.
                var txtSecurityQuestionCtrl = document.getElementById("<%= txtSecurityQuestion.ClientID %>")

                if (!TextBoxEmpty(txtSecurityQuestionCtrl)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterSecurityQuestion;
                    txtSecurityQuestionCtrl.focus();
                    return false;
                }

                // Validation for security answer control.
                var txtSecurityAnswerCtrl = document.getElementById("<%= txtSecurityAnswer.ClientID %>")

                if (!TextBoxEmpty(txtSecurityAnswerCtrl)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgEnterSecurityAnswer;
                    txtSecurityAnswerCtrl.focus();
                    return false;
                }
            }


    </script>
    <div>

         <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Password</li>
                    </ol>
                </div>
                <h4 class="page-title">Password                  
                </h4>
            </div>
        </div>
    </div>

       <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
            <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg"></asp:Label>
    </div>

        <div class=" card-box">
        <div id="real_time_chart" class="row">
                <div style="width: 99%; margin: 0 auto; padding: 5px 0 40px 0;">
                     <div class="col-sm-12"  style="width:100%">
                    <div  style="width:100%">
                        <ul class="nav nav-pills navtab-bg" >
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab1" class="nav-link p-2" OnClick="Tab1_Click" CausesValidation="false"><i class="fe-lock" ></i>
                            Reset Employee Password</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab2" class="nav-link  p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-shield"></i>
                            Account Security Question</asp:LinkButton></li>                           
                   </ul>
                            <div class="tabcontents">
                         <div id="view2" runat="server" visible="false"  style="width:100%">
                             <br />
                                <div class="header-title">&nbsp;&nbsp;Account Security Question</div>
                                 <hr class="HrCls"/>
                            <br />
                <div id="divbrdr" class="divfr">
                    <p>Please provide your security question details in the fields below </p>
                    
                      <div class="row">
                    <div class="col-sm-2 htCr"> <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label> &nbsp;</div>
                    <div class="col-sm-6">  
                     <asp:TextBox ID="txtPassword" runat="server" TabIndex="1" placeholder="Enter Password"
                     TextMode="Password" meta:resourcekey="txtPasswordResource1" CssClass="txtDropDownwidth"></asp:TextBox>
                      </div>
                      </div>
                    
                    <br /> 

                    <div class="row">
                    <div class="col-sm-2 htCr"><asp:Label ID="lblSecurityQuestion" runat="server" Text="Security question"></asp:Label>&nbsp;</div>
                    <div class="col-sm-6"> 
                     <asp:TextBox ID="txtSecurityQuestion" runat="server" Columns="50" TabIndex="2" placeholder="Enter Security Question"
                     MaxLength="50" CssClass="txtDropDownwidth"></asp:TextBox>
                      </div>
                      </div>

                     <br /> 

                    <div class="row">
                    <div class="col-sm-2 htCr"><asp:Label ID="lblSecurityAnswer" runat="server" Text="Security answer"></asp:Label>&nbsp;</div>
                    <div class="col-sm-6"> 
                     <asp:TextBox ID="txtSecurityAnswer" runat="server" Columns="50" MaxLength="50" TabIndex="3" CssClass="txtDropDownwidth" placeholder="Enter Security Answer"></asp:TextBox>
                     </div>
                     </div>
                   
                    <br />
                    <br />

                      <div class="row">
                    <div class="col-sm-12 htCr">
                     <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return ValidateControls()" TabIndex="4" OnClick="btnSave_Click"/>
                     <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" TabIndex="5" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</div>
 </div>
</div>
</div>
    
</asp:Content>


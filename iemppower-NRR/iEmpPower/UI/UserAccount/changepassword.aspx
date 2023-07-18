<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" Inherits="UI_UserAccount_changepassword"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" Theme="SkinFile" CodeBehind="changepassword.aspx.cs"  MaintainScrollPositionOnPostback="true"%>


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

   <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Password Reset</li>
                    </ol>
                </div>
                <h4 class="page-title">Password Reset
                </h4>
            </div>
        </div>
    </div>

    <div class="header">
            <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
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
                         <div id="view1" runat="server" visible="false"  style="width:100%">
                             <br />
                                <div class="header-title">&nbsp;&nbsp;Reset Employee Password</div>
                                 <hr class="HrCls"/>
                            <br />
                <div id="divbrdr" class="divfr">
                    <asp:ChangePassword ID="ChangePassword1" runat="server" BorderPadding="4"
                        Font-Names="Verdana" Font-Size="0.9em" Width="100%"
                        meta:resourcekey="ChangePassword1Resource1"
                        ContinueDestinationPageUrl="~/UI/Default.aspx">
                        <LabelStyle Width="220px" Font-Bold="True" />
                        <CancelButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.9em" ForeColor="#F44336" />
                        <PasswordHintStyle Font-Italic="True" ForeColor="#F44336" />
                        <ContinueButtonStyle BackColor="White" BorderColor="#F44336"
                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em"
                            ForeColor="#F44336" />
                        <ChangePasswordButtonStyle BackColor="White" BorderColor="#507CD1"
                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.9em"
                            ForeColor="#F44336" />
                        <TitleTextStyle Font-Bold="True" Font-Size="12px"
                            ForeColor="#333333" Height="30" />
                        <ChangePasswordTemplate>
                            <div>
                                <asp:CompareValidator ID="NewPasswordCompare" runat="server" CssClass="lblValidation"
                                    ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                    Display="Dynamic"
                                    ErrorMessage="The Confirm New Password must match the New Password entry."
                                    ValidationGroup="ChangePassword1"
                                    meta:resourcekey="NewPasswordCompareResource1"></asp:CompareValidator>
                            </div>

                            <div>
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"
                                    meta:resourcekey="FailureTextResource1"></asp:Literal>
                            </div>
                            <br />

                    <div class="row">
                    <div class="col-sm-2 htCr"><asp:Label ID="Label1" runat="server" AssociatedControlID="CurrentPassword" Text="Old password"></asp:Label> &nbsp;</div>
                    <div class="col-sm-6">
                     <asp:TextBox ID="CurrentPassword" placeholder="Enter Old Password" runat="server" CssClass="txtDropDownwidth" TextMode="Password" TabIndex="1" meta:resourcekey="CurrentPasswordResource1"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" ErrorMessage="Password is required." ToolTip="Password is required." ForeColor="Red"
                       ValidationGroup="ChangePassword1" CssClass="lblValidation">Please enter old password</asp:RequiredFieldValidator>
                       </div>
                       </div>
                            <br />
                    <div class="row">
                    <div class="col-sm-2 htCr"><asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword" Text="New password"></asp:Label> &nbsp;</div>
                    <div class="col-sm-6">
                    <asp:TextBox ID="NewPassword" runat="server" placeholder="Enter New Password" CssClass="txtDropDownwidth" TextMode="Password" TabIndex="2" meta:resourcekey="NewPasswordResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" ErrorMessage="New Password is required."
                    ToolTip="New Password is required." ForeColor="Red" ValidationGroup="ChangePassword1" CssClass="lblValidation">Please enter new password</asp:RequiredFieldValidator>
                    </div>
                    </div>
                        <br />
                    <div class="row">
                    <div class="col-sm-2 htCr"><asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword" Text="Confirm new password"></asp:Label> &nbsp;</div>
                    <div class="col-sm-6">
                    <asp:TextBox ID="ConfirmNewPassword" runat="server" placeholder="Enter Confirm New Passwd" CssClass="txtDropDownwidth" TextMode="Password" TabIndex="3" meta:resourcekey="ConfirmNewPasswordResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ForeColor="Red" ControlToValidate="ConfirmNewPassword"
                    ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1" CssClass="lblValidation">Please enter confirm new password</asp:RequiredFieldValidator>
                    </div>
                    </div>
                    <br />
                    <br />

                   <div class="row">
                    <div class="col-sm-12 htCr">
                    <asp:Button ID="ChangePasswordPushButton" runat="server" Width="70px" CommandName="ChangePassword" Text="Change" ValidationGroup="ChangePassword1" TabIndex="4"  meta:resourcekey="ChangePasswordPushButtonResource1" />
                    <asp:Button ID="CancelPushButton" runat="server" Width="70px"  CausesValidation="False" CommandName="Cancel" Text="Cancel" TabIndex="5" meta:resourcekey="CancelPushButtonResource1" />
                    </div>
                    </div>

                    </ChangePasswordTemplate>
                    <TextBoxStyle CssClass="textbox" Font-Size="1em" />
                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                    </asp:ChangePassword>
                </div>
            </div>
        </div>
       </div>
      </div>
     </div>
    </div>
 </div>

</asp:Content>


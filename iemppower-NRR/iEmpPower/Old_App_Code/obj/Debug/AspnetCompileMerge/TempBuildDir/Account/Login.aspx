﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="login" Codebehind="Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login page</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: #CCCCCC">
    <form id="form1" runat="server">
    <div class="loginPage" 
        style="border: 1px solid #003366; padding: 40px; margin: 80px; background-color: #FFFFFF; display: block; height: 400px;">
    
    <div style="float: right; padding: 2px; ">
    <div align="left" style="padding: 10px"><asp:Image ID="Image1" runat="server" 
            ImageUrl="~/images/iemp-small-logo.jpg" AlternateText="iEmpPower logo" 
            ToolTip="iEmpPower " Width="130px" /></div>
 
        <asp:Login ID="Login1" runat="server"  Font-Names="Calibri" 
            Font-Size="11pt" ForeColor="#333333" Width="300px">
            <TextBoxStyle Font-Size="0.8em" />
            <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" 
                BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
            <LayoutTemplate>
                                     <div class="login-block">

                                      <div class="row" style="margin-bottom: 8px; font-weight: bold; font-size: 16px">LogIn here</div>  
                                        
                                        <div class="row">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="label">User ID:</asp:Label>
                                   
                                        <asp:TextBox ID="UserName" runat="server"  CssClass="textbox" Width="170px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server"  ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                            ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </div>

                                        <div class="row" >
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="label">Password:</asp:Label>
                                    
                                        <asp:TextBox ID="Password" runat="server"  TextMode="Password"  CssClass="textbox"
                                         
                                                Width="170px"   ></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." 
                                            ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                       </div>

                                       <div class="row">
                                       <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." CssClass="gncontrols" />
                                       </div>
                                                   
                                       <div class="row">
                                       <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/UI/UserAccount/passwordrecovery.aspx" CssClass="gncontrols">Reset Password</asp:HyperLink>
                                       </div>
                                        <div class="row" style="color: #DF0000">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False" ></asp:Literal>
                                        </div>                                     
                                    
                                        <div class="row" align="right" style="padding-right: 40px">
                                        <asp:Button ID="LoginButton" runat="server" BackColor="#0080C0" 
                                            BorderColor="#0080C0" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" 
                                            Font-Names="Verdana" Font-Size="0.9em" ForeColor="White" Text="Log In" 
                                            ValidationGroup="Login1" onclick="LoginButton_Click" Font-Bold="True" />
                                        </div>
                                  </div>
                                 
            </LayoutTemplate>
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <TitleTextStyle  Font-Bold="True" Font-Size="0.9em" 
                ForeColor="White" />
        </asp:Login>
        </div>
    </div>
    </form>
</body>
</html>


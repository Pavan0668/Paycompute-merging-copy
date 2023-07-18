<%@ Page Title="" Language="C#"  AutoEventWireup="true"
     CodeBehind="Login.aspx.cs" Inherits="iEmpPower.Account.LoginNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>iEmpPower</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="Bubble SignUp Form template Responsive, Login form web template,Flat Pricing tables,Flat Drop downs  Sign up Web Templates, Flat Web Templates, Login sign up Responsive web template, SmartPhone Compatible web template, free web designs for Nokia, Samsung, LG, SonyEricsson, Motorola web design" />
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!-- Custom Theme files -->
    <link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
    <!-- //Custom Theme files -->
    <!-- web font -->
    <link href="//fonts.googleapis.com/css?family=Roboto+Condensed:300,300i,400,400i,700,700i" rel="stylesheet">
    <!-- //web font -->
</head>
 <script type="text/javascript">
         if (window.history.forward(1) != null)
             window.history.forward(1);

         if (window.history.backward(1) != null)
             window.history.backward(1);
</script>
<body class="main-w3layouts wrapper">
    <div>
        <center style="font-size:xx-large;color:white;">iEmpPower Sign In Form</center>
        
        <div class="main-agileinfo">
            <div class="agileits-top">
                <form id="form1" runat="server" action="#" method="post">
                    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                    <asp:Login ID="Login1" runat="server">
                        <LayoutTemplate>
                            <asp:TextBox ID="UserName" runat="server" Class="text" MaxLength="8" placeholder="Username" Width="380px"></asp:TextBox>
                            <%--    <input class="text" type="text" name="UserName" placeholder="Username" required="" runat="server">--%>
                            <asp:RegularExpressionValidator ID="REV_UserName" runat="server" Display="Dynamic" CssClass="VdtrCls" ControlToValidate="UserName" ForeColor="Red"
                                ValidationExpression="^[\s\S]{5,8}$" ValidationGroup="Login1" ToolTip="Enter 8 digit PERNR Number">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." Display="Dynamic" ForeColor="Red"
                                ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>


                            <asp:RegularExpressionValidator ID="REV_UserName2" runat="server" Display="Dynamic" CssClass="VdtrCls" ControlToValidate="UserName" ForeColor="Red"
                                ValidationExpression="^[\s\S]{5,8}$" ValidationGroup="Lvg" ToolTip="Enter 8 digit PERNR Number">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFV_UserName2" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." Display="Dynamic" ForeColor="Red"
                                ToolTip="User Name is required." ValidationGroup="Lvg">*</asp:RequiredFieldValidator>
                            <br />
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Class="text" placeholder="Password" Width="380px"></asp:TextBox>
                            <%--<input class="text" type="password" name="Password" placeholder="Password" required="">--%>

                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required."
                                ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                            <div class="wthree-text">
                                <label class="anim">
                                    <asp:CheckBox ID="RememberMe" runat="server" CssClass="gncontrols" />
                                    <span>Remember me</span>
                                </label>
                                <div class="clear"></div>
                                <asp:Label ID="LblMsg" runat="server" EnableViewState="False" ></asp:Label>
                            </div>
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="SIGN IN" ValidationGroup="Login1" OnClick="LoginButton_Click" />

                        </LayoutTemplate>
                    </asp:Login>
                </form>
                <p>Forgot your Password? <a href="../../UI/UserAccount/passwordrecovery.aspx">Reset Now!</a></p>
            </div>
        </div>
        <!-- copyright -->
        <div class="w3copyright-agile">
            <p>© 2010 - <asp:Literal ID="toyear" runat="server"></asp:Literal> ITChamps. All rights reserved | Designed by <a href="http://itchamps.com/" target="_blank">ITChamps</a></p>
        </div>
        <!-- //copyright -->
        <ul class="w3lsg-bubbles">
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
        </ul>
    </div>
</body>
</html>

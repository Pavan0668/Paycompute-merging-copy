<%@ Page Title="Password Recovery" Language="C#" AutoEventWireup="true" Inherits="UI_UserAccount_passwordrecovery" culture="auto" meta:resourcekey="PageResource1"
     uiculture="auto" Theme="SkinFile" Codebehind="passwordrecovery.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>Password recovery</title>
   
</head>

<body style="background-color: #CCCCCC">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div style="width: 45%; display: block; margin-right: auto; margin-left: auto; margin-top: 10%;">
    <h2>Password recovery</h2>
    <asp:Label ID="lblMessageBoard" runat="server" Text="Label"></asp:Label><br />
        <div style="border: 1px solid #003366; border-color: #000000; padding: 10px; margin-top: 10px; background-color: #FFFFFF;">
                 <br />   
        <asp:Label id="Msg" runat="server"  /><br /><br />

           <asp:Panel ID="pnlPasswordReset" runat="server">
               <asp:Label ID="Label1" runat="server" Text="Username: " CssClass="label"></asp:Label>  
 <br /> <asp:Textbox id="UsernameTextBox" Columns="30" runat="server" AutoPostBack="true" 
                   CssClass="textbox" MaxLength="8" ToolTip="User name " />
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"  TargetControlID="UsernameTextBox" runat="server" FilterType="Numbers">
</cc1:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator id="UsernameRequiredValidator" runat="server"
                                        ControlToValidate="UsernameTextBox" ForeColor="red"
                                        Display="Static" ErrorMessage="Please Enter Username" /><br /><br />

  Password Question: <b><asp:Label id="QuestionLabel" runat="server" /></b><br /><br />
  <asp:Label ID="Label2" runat="server" Text=" Answer: " CssClass="label"></asp:Label>  
 <br /><asp:TextBox id="AnswerTextBox" Columns="60" runat="server" Enabled="false" CssClass="textbox" Width="300" />
          <asp:RequiredFieldValidator id="AnswerRequiredValidator" runat="server"
                                      ControlToValidate="AnswerTextBox" ForeColor="red"
                                      Display="Static" ErrorMessage="Required" Enabled="false" /><br /><br />

  <asp:Button id="ResetPasswordButton" Text="Reset Password" 
              OnClick="ResetPassword_OnClick" runat="server" Enabled="false" />
   &nbsp;<asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" PostBackUrl="~/Account/Login.aspx" Text="Back" CausesValidation="False" />
               </asp:Panel><br />
                <asp:Panel ID="pnlLogin" runat="server">
                    <asp:Button ID="btnContinue" runat="server" Text="Continue to login" 
                        onclick="btnContinue_Click" />
    </asp:Panel>

</div>  </div>
   
   
    </form>
    
</body>
</html>

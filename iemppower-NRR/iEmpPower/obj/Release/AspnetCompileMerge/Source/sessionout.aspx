<%@ Page Language="C#" AutoEventWireup="true" Inherits="sessionout" Codebehind="sessionout.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
         function ShowCurrentTime() {
             var dt = new Date();
             document.getElementById("lblTime").innerHTML = dt.toLocaleTimeString();
             window.setTimeout("ShowCurrentTime()", 1000); // Here 1000(milliseconds) means one 1 Sec  
         }
</script>
</head>
<body onload="ShowCurrentTime()">
    <form id="form1" runat="server">
    <div>
    <p>Your login session has expired. Please click <asp:Button ID="btnHere" 
            runat="server" Text="here" onclick="btnHere_Click"   /> to login.</p>
            <asp:Label ID="lblTime" runat="server" Visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
